using System;
#nullable enable
using System.Diagnostics;
using System.Linq;
using Serilog;
//using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Reflection.Metadata;
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
        public enum EnumLogLib { Serilog, NLog }
        /// <summary>
        /// Log library
        /// </summary>
        public static EnumLogLib LogLib { get; set; } = EnumLogLib.Serilog;
        /// <summary>
        /// Use stack deepness for messages
        /// </summary>
        public static bool UseStackIndent { get; set; } = true;
        /// <summary>
        /// Log file path
        /// </summary>
        public static string? LogFilePath { get; set; } = ".\\Logs\\log.txt";
        /// <summary>
        /// Use console logger
        /// </summary>
        public static bool UseConsole { get; set; } = false;
        /// <summary>
        /// Use debug logger
        /// </summary>
        public static bool UseDebug { get; set; } = true;
        /// <summary>
        /// Default log level 
        /// </summary>
        public static LogLevel LogLevel { get; set; } = LogLevel.None;
        /// <summary>
        /// Log level for debug logger. Override default log level.
        /// </summary>
        public static LogLevel? LogLevelDebug { get; set; } = null;
        /// <summary>
        /// Log level for console logger. Override default log level.
        /// </summary>
        public static LogLevel? LogLevelConsole { get; set; } = null;
        /// <summary>
        /// Indent message log. If equal '-1' when logger factory is creating, then it indent will show deepness relative log factory creation call. 
        /// </summary>
        public static int IndentShift { get; internal set; } = -1;
        /// <summary>
        /// If true, full file path wiil be used in logging messages
        /// </summary>
        public static bool IsFullFilePath { get; set; } = true;
        /// <summary>
        /// Logger factory to create loggers. Use next sample code: '_logger = AppLogger.CreateLogger(nameof(<Your-class-name>));'.
        /// </summary>
        public static Microsoft.Extensions.Logging.ILoggerFactory? LoggerFactory
        {
            get
            {
                if (_LoggerFactory == null)
                {
                    string call_from = "";
                    if (AppLogger.UseStackIndent)
                    {
                        var lst = Environment.StackTrace.Split(Environment.NewLine);
                        call_from = lst[3];
                        int n = lst.Length - 1;
                        if (AppLogger.IndentShift == -1)
                            AppLogger.IndentShift = n;
                    }
                    Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
                    var logCfg = new Serilog.LoggerConfiguration();
                    switch (AppLogger.LogLevel)
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
                    if (AppLogger.LogLevel != LogLevel.None)
                    {
                        if (AppLogger.LogFilePath?.Length > 0)
                        {
                            logCfg.WriteTo.Async(a => a.File(AppLogger.LogFilePath,
                            retainedFileTimeLimit: TimeSpan.FromDays(3),
                            //retainedFileCountLimit: 5,
                            rollingInterval: Serilog.RollingInterval.Day,
                            rollOnFileSizeLimit: true
                            ));
                        }
                    }
                    if (AppLogger.UseDebug)
                    {
                        if (AppLogger.LogLevelDebug == null)
                            AppLogger.LogLevelDebug = LogLevel;
                        switch (AppLogger.LogLevelDebug)
                        {
                            case LogLevel.Trace:
                                logCfg.WriteTo.Debug().MinimumLevel.Verbose();
                                break;
                            case LogLevel.Debug:
                                logCfg.WriteTo.Debug().MinimumLevel.Debug();
                                break;
                            case LogLevel.Information:
                                logCfg.WriteTo.Debug().MinimumLevel.Information();
                                break;
                            case LogLevel.Warning:
                                logCfg.WriteTo.Debug().MinimumLevel.Warning();
                                break;
                            case LogLevel.Error:
                                logCfg.WriteTo.Debug().MinimumLevel.Error();
                                break;
                            case LogLevel.Critical:
                                logCfg.WriteTo.Debug().MinimumLevel.Fatal();
                                break;
                        }
                    }
                    if (AppLogger.UseConsole)
                    {
                        if (AppLogger.LogLevelConsole == null)
                            AppLogger.LogLevelConsole = LogLevel;
                        switch (AppLogger.LogLevelConsole)
                        {
                            case LogLevel.Trace:
                                logCfg.WriteTo.Console().MinimumLevel.Verbose();
                                break;
                            case LogLevel.Debug:
                                logCfg.WriteTo.Console().MinimumLevel.Debug();
                                break;
                            case LogLevel.Information:
                                logCfg.WriteTo.Console().MinimumLevel.Information();
                                break;
                            case LogLevel.Warning:
                                logCfg.WriteTo.Console().MinimumLevel.Warning();
                                break;
                            case LogLevel.Error:
                                logCfg.WriteTo.Console().MinimumLevel.Error();
                                break;
                            case LogLevel.Critical:
                                logCfg.WriteTo.Console().MinimumLevel.Fatal();
                                break;
                        }
                    }
                    Serilog.Log.Logger = logCfg.CreateLogger();
                    if (AppLogger.LogLevel != LogLevel.None || AppLogger.UseDebug || UseConsole)
                    {
                        _LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
                            .SetMinimumLevel(AppLogger.LogLevel)
                            .AddSerilog());
                    }
                    StringBuilder log = new StringBuilder();
                    if (!string.IsNullOrEmpty(AppLogger.LogFilePath))
                    {
                        log.Append("LogFilePath: '");
                        log.Append(AppLogger.LogFilePath);
                        log.Append("'");
                        log.Append(", LogLevel: '");
                        log.Append(AppLogger.LogLevel);
                        log.Append("'");
                    }
                    if (AppLogger.UseDebug)
                    {
                        log.Append(", LogLevelDebug: '");
                        log.Append(AppLogger.LogLevelDebug);
                        log.Append("'");
                    }
                    else
                    {
                        log.Append(", LogDebug: 'false'");
                    }
                    if (UseConsole)
                    {
                        log.Append(", LogLevelConsole: '");
                        log.Append(AppLogger.LogLevelConsole);
                        log.Append("'");
                    }
                    else
                    {
                        log.Append(", LogConsole: 'false'");
                    }
                    Debug.WriteLine("##########################################  L O G G E R  ###################################################");
                    Debug.WriteLine("##### " + log.ToString());
                    Debug.WriteLine("############################################################################################################");
                    var _logger = _LoggerFactory?.CreateLogger(nameof(AppLogger));
                    call_from = "LoggerFactory is created " + call_from.TrimStart();
                    _logger?.Information(call_from);
                }
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
                    int n = Environment.StackTrace.Split(Environment.NewLine).Length;
                    if (IndentShift == -1 || IndentShift > n) IndentShift = n;
                }
                _LoggerFactory = value;
            }
        }
        private static Microsoft.Extensions.Logging.ILoggerFactory? _LoggerFactory = null;
        /// <summary>
        /// Logger factory to create loggers '_logger = AppLogger.CreateLogger<Your-class-name>();'
        /// </summary>
        public static Microsoft.Extensions.Logging.ILogger? CreateLogger<T>() => LoggerFactory?.CreateLogger(typeof(T).Name);
        /// <summary>
        /// Logger factory to create loggers '_logger = AppLogger.CreateLogger(nameof(Your-class-name));'
        /// </summary>
        public static Microsoft.Extensions.Logging.ILogger? CreateLogger(string category) => LoggerFactory?.CreateLogger(category);
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
