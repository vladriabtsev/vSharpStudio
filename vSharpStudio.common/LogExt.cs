using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public static class LogExt
    {
        public static string CallerInfo(this string message, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string sourceFilePath = "",
                  [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;

            return $"{fileName}.cs {line} [{methodName}] {message}";
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
        public static void Trace(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
        public static void Debug(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
        public static void Information(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
        public static void Warning(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
        public static void Critical(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
