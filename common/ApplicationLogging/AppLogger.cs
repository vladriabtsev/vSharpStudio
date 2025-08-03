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
    public static class AppLogger
    {
        public static int IndentShift { get; internal set; } = -1;
        public static Microsoft.Extensions.Logging.ILoggerFactory? LoggerFactory
        {
            get
            {
                if (_LoggerFactory == null)
                {
                    int n = Environment.StackTrace.Split(Environment.NewLine).Count();
                    if (IndentShift == -1 || IndentShift > n) IndentShift = n;
                    //SelfLog.Enable(msg => Debug.WriteLine(msg));
                    var logCfg = new LoggerConfiguration()
                        .MinimumLevel.Verbose();
                    //if (category != null)
                    //    logCfg.Filter.ByIncludingOnly(Matching.FromSource(category));
                    var path = ".\\Logs\\log.txt";
                    logCfg.WriteTo.Async(a => a.File(path,
                        retainedFileTimeLimit: TimeSpan.FromDays(3),
                        //retainedFileCountLimit: 5,
                        rollingInterval: Serilog.RollingInterval.Day,
                        rollOnFileSizeLimit: true
                        ));
                    Log.Logger = logCfg.CreateLogger();
                    _LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
                        .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                        .AddSerilog()
                        .AddDebug());
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
