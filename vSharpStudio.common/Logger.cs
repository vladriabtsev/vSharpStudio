using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace vSharpStudio.common
{
    public static class Logger
    {
        public static ServiceProvider ServiceProvider;
        public static Microsoft.Extensions.Logging.ILoggerProvider LogerProvider;
        public static Microsoft.Extensions.Logging.ILogger CreateLogger<T>() => Logger.LogerProvider.CreateLogger(typeof(T).Name);
        public static string CallerInfo(this string message, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string sourceFilePath = "",
                  [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;

            return $"{fileName} {line} [{methodName}] {message}";
        }
        public static string StackInfo(this string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            return $"{fileName} {line} [{methodName}] {message}\n   StackTrace:\n{Environment.StackTrace}";
        }
        public static void LogTrace(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            var msg = $"{fileName} {line} [{methodName}] {message}";
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, null, msg);
        }
        public static void LogDebug(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            var msg = $"{fileName} {line} [{methodName}] {message}";
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, null, msg);
        }
        public static void LogInformation(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            var msg = $"{fileName} {line} [{methodName}] {message}";
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, null, msg);
        }
        public static void LogWarning(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            var msg = $"{fileName} {line} [{methodName}] {message}";
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, null, msg);
        }
        public static void LoggerError(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            var msg = $"{fileName} {line} [{methodName}] {message}";
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, null, msg);
        }
        public static void LogrCritical(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            var msg = $"{fileName} {line} [{methodName}] {message}";
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, null, msg);
        }
    }
}
