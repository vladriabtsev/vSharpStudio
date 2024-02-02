using System;
using System.Collections.Generic;
#nullable enable
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using CommunityToolkit.Diagnostics;
using System.Net.Http.Headers;
using System.Linq;

namespace ApplicationLogging
{
    public static class Logger
    {
        public static int IndentShift { get; private set; } = -1;
        public static ILoggerProvider? LoggerProvider
        {
            get { return _LoggerProvider; }
            set
            {
                if (_LoggerProvider == null)
                {
                    int n = Environment.StackTrace.Split(Environment.NewLine).Count();
                    if (IndentShift == -1 || IndentShift > n) IndentShift = n;
                }
                _LoggerProvider = value;
            }
        }
        private static ILoggerProvider? _LoggerProvider = null;
        public static ILogger? CreateLogger<T>() => Logger.LoggerProvider?.CreateLogger(typeof(T).Name);
        public static ILogger? CreateLogger(string category) => Logger.LoggerProvider?.CreateLogger(category);
        public static ILogger? CreateLogger(object obj) => Logger.LoggerProvider?.CreateLogger(obj.GetType().Name);

    }
}
