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

namespace ApplicationLogging
{
    public static class Logger
    {
        public static ILoggerProvider? LoggerProvider;
        public static ILogger? CreateLogger<T>() => Logger.LoggerProvider?.CreateLogger(typeof(T).Name);
        public static ILogger? CreateLogger(string category) => Logger.LoggerProvider?.CreateLogger(category);
        public static ILogger? CreateLogger(object obj) => Logger.LoggerProvider?.CreateLogger(obj.GetType().Name);

    }
}
