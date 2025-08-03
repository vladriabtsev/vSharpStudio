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
//using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Serilog.Debugging;
//using Serilog;
//using Serilog.Filters;

namespace ApplicationLogging
{
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
        public static int IndentShift { get; internal set; } = -1;
        public static Microsoft.Extensions.Logging.ILoggerFactory? LoggerFactory
        {
            get
            {
                if (_LoggerFactory == null)
                {
                    int n = Environment.StackTrace.Split(Environment.NewLine).Count();
                    if (IndentShift == -1 || IndentShift > n) IndentShift = n;
                    if (LogFilePath?.Length > 0)
                    {
                        //logPath = AppDomain.CurrentDomain.BaseDirectory + logPath;
                        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
                        var logCfg = new Serilog.LoggerConfiguration()
                            .MinimumLevel.Verbose();
                        //if (category != null)
                        //    logCfg.Filter.ByIncludingOnly(Matching.FromSource(category));
                        logCfg.WriteTo.Async(a => a.File(LogFilePath,
                            retainedFileTimeLimit: TimeSpan.FromDays(3),
                            //retainedFileCountLimit: 5,
                            rollingInterval: Serilog.RollingInterval.Day,
                            rollOnFileSizeLimit: true
                            ));
                        Serilog.Log.Logger = logCfg.CreateLogger();
                        _LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
                            .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug)
                            .AddSerilog()
                            .AddDebug());
                    }
                    else
                    {
                        _LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
                            .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                            .AddDebug());
                    }
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
}
