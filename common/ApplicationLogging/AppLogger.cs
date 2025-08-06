using System;
using System.Collections.Generic;
#nullable enable
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;
using CommunityToolkit.Diagnostics;
using System.Net.Http.Headers;
using System.Linq;
using Serilog;
using Serilog.Extensions.Logging;
//using Microsoft.Extensions.Logging;
using Serilog.Debugging;
using Microsoft.Extensions.Logging;
using Serilog.Core;
//using Serilog;
//using Serilog.Filters;

namespace ApplicationLogging
{
    // https://learn.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line

    // https://docs.microsoft.com/en-us/aspnet/core/migration/logging-nonaspnetcore?view=aspnetcore-2.2
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2

    // https://msdn.microsoft.com/en-us/magazine/mt694089.aspx

    // https://nblumhardt.com/2017/08/use-serilog/
    // https://andrewlock.net/creating-a-rolling-file-logging-provider-for-asp-net-core-2-0/
    // https://msdn.microsoft.com/en-us/magazine/mt830355.aspx EF
    // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/DiagnosticSourceUsersGuide.md
    public static class AppLogger
    {
        public static string? LogFilePath { get; set; } = ".\\Logs\\log.txt";
        public static bool UseConsole { get; set; } = false;
        public static bool UseDebug { get; set; } = true;
        public static LogLevel LogLevel { get; set; } = LogLevel.None;
        public static int IndentShift { get; internal set; } = -1;
        public static Microsoft.Extensions.Logging.ILoggerFactory? LoggerFactory
        {
            get
            {
                if (_LoggerFactory == null)
                {
                    //Trace.WriteLine("##### LogFilePath: " + LogFilePath);
                    //Trace.WriteLine("##### UseDebug: " + UseDebug);
                    //Trace.WriteLine("##### UseConsole: " + UseConsole);
                    //Trace.WriteLine("##### LogLevel: " + LogLevel);

                    Debug.WriteLine("##### LogFilePath: " + LogFilePath);
                    Debug.WriteLine("##### UseDebug: " + UseDebug);
                    Debug.WriteLine("##### UseConsole: " + UseConsole);
                    Debug.WriteLine("##### LogLevel: " + LogLevel);

                    int n = Environment.StackTrace.Split(Environment.NewLine).Count();
                    if (IndentShift == -1 || IndentShift > n) IndentShift = n;
                    Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
                    var logCfg = new Serilog.LoggerConfiguration();
                    switch (LogLevel)
                    {
                        case LogLevel.Trace:
                            logCfg.MinimumLevel.Verbose();
                            break;
                        case LogLevel.Debug:
                            logCfg.MinimumLevel.Debug();
                            break;
                        case LogLevel.Information:
                            logCfg.MinimumLevel.Information();
                            break;
                        case LogLevel.Warning:
                            logCfg.MinimumLevel.Warning();
                            break;
                        case LogLevel.Error:
                            logCfg.MinimumLevel.Error();
                            break;
                        case LogLevel.Critical:
                            logCfg.MinimumLevel.Fatal();
                            break;
                    }
                    //if (category != null)
                    //    logCfg.Filter.ByIncludingOnly(Matching.FromSource(category));
                    if (LogLevel != LogLevel.None)
                    {
                        if (LogFilePath?.Length > 0)
                        {
                            logCfg.WriteTo.Async(a => a.File(LogFilePath,
                            retainedFileTimeLimit: TimeSpan.FromDays(3),
                            //retainedFileCountLimit: 5,
                            rollingInterval: Serilog.RollingInterval.Day,
                            rollOnFileSizeLimit: true
                            ));
                        }
                        if (UseDebug)
                        {
                            logCfg.WriteTo.Debug();
                        }
                        if (UseConsole)
                        {
                            logCfg.WriteTo.Console();
                        }
                        Serilog.Log.Logger = logCfg.CreateLogger();
                        _LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
                            .SetMinimumLevel(LogLevel)
                            .AddSerilog());
                    }
                    //if (LogFilePath?.Length > 0)
                    //{
                    //    //logPath = AppDomain.CurrentDomain.BaseDirectory + logPath;
                    //    Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
                    //    var logCfg = new Serilog.LoggerConfiguration()
                    //        .MinimumLevel.Verbose();
                    //    //if (category != null)
                    //    //    logCfg.Filter.ByIncludingOnly(Matching.FromSource(category));
                    //    logCfg.WriteTo.Async(a => a.File(LogFilePath,
                    //        retainedFileTimeLimit: TimeSpan.FromDays(3),
                    //        //retainedFileCountLimit: 5,
                    //        rollingInterval: Serilog.RollingInterval.Day,
                    //        rollOnFileSizeLimit: true
                    //        ));
                    //    Serilog.Log.Logger = logCfg.CreateLogger();
                    //    _LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
                    //        .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug)
                    //        .AddSerilog()
                    //        .AddDebug());
                    //}
                    //else
                    //{
                    //    _LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
                    //        .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                    //        .AddDebug());
                    //}
                }
                //var _logger = _LoggerFactory.CreateLogger("AppLogger");
                //_logger?.Trace("### IsModel={IsModel}", o.IsModel);
                return _LoggerFactory;
            }
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException();
                }
                if (_LoggerFactory == null)
                {
                    int n = Environment.StackTrace.Split(Environment.NewLine).Count();
                    if (IndentShift == -1 || IndentShift > n) IndentShift = n;
                }
                _LoggerFactory = value;
            }
        }
        private static Microsoft.Extensions.Logging.ILoggerFactory? _LoggerFactory = null;
        public static Microsoft.Extensions.Logging.ILogger? CreateLogger<T>() => LoggerFactory?.CreateLogger(typeof(T).Name);
        public static Microsoft.Extensions.Logging.ILogger? CreateLogger(string category) => LoggerFactory?.CreateLogger(category);
        public static Microsoft.Extensions.Logging.ILogger? CreateLogger(object obj) => LoggerFactory?.CreateLogger(obj.GetType().Name);
    }

    // https://learn.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line
    // https://learn.microsoft.com/en-us/dotnet/core/extensions/console-log-formatter
    // https://learn.microsoft.com/en-us/dotnet/core/extensions/custom-logging-provider
    //public sealed class ColorConsoleLoggerConfiguration
    //{
    //    public int EventId { get; set; }

    //    public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new()
    //    {
    //        [LogLevel.Information] = ConsoleColor.Green
    //    };
    //}
    //public sealed class ColorConsoleLogger(
    //    string name,
    //    Func<ColorConsoleLoggerConfiguration> getCurrentConfig) : Microsoft.Extensions.Logging.ILogger
    //{
    //    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    //    public bool IsEnabled(LogLevel logLevel) =>
    //        getCurrentConfig().LogLevelToColorMap.ContainsKey(logLevel);

    //    public void Log<TState>(
    //        LogLevel logLevel,
    //        EventId eventId,
    //        TState state,
    //        Exception? exception,
    //        Func<TState, Exception?, string> formatter)
    //    {
    //        if (!IsEnabled(logLevel))
    //        {
    //            return;
    //        }

    //        ColorConsoleLoggerConfiguration config = getCurrentConfig();
    //        if (config.EventId == 0 || config.EventId == eventId.Id)
    //        {
    //            ConsoleColor originalColor = Console.ForegroundColor;

    //            Console.ForegroundColor = config.LogLevelToColorMap[logLevel];
    //            Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

    //            Console.ForegroundColor = originalColor;
    //            Console.Write($"     {name} - ");

    //            Console.ForegroundColor = config.LogLevelToColorMap[logLevel];
    //            Console.Write($"{formatter(state, exception)}");

    //            Console.ForegroundColor = originalColor;
    //            Console.WriteLine();
    //        }
    //    }
    //}
}
