using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

// https://andrewlock.net/defining-custom-logging-messages-with-loggermessage-define-in-asp-net-core/
// https://www.c-sharpcorner.com/article/speed-up-logging-in-net/
// https://dotnettips.wordpress.com/spargine/
// https://github.com/RealDotNetDave/dotNetTips.Spargine
namespace ApplicationLogging
{
    public static class DebugExt
    {
        [Conditional("DEBUG")]
        // Use at a beginning of procedure
        public static void WriteLineWithStack(string? message = null, uint stackDeepness = 3,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            Debug.WriteLine(LoggerExt.GetDebugText(message, file, member, line, (int)stackDeepness).ToString());
        }
        [Conditional("DEBUG")]
        // Use after DebugExt.WriteLineWithStack later in a same procedure
        public static void WriteLine(string? message = null,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            Debug.WriteLine(LoggerExt.GetDebugText(message, file, member, line, -1).ToString());
        }
        [Conditional("DEBUG")]
        public static void Write(string message, bool isWithPosition = false,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            if (isWithPosition)
                Debug.Write(LoggerExt.GetDebugText(message, file, member, line, -1).ToString());
            else
                Debug.Write(message);
        }
        public static string FilePos(this string text,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            return LoggerExt.FilePos(text, file, member, line);
        }
    }
    public static class LoggerExt
    {
        public static string Member([CallerMemberName] string member = "")
        {
            return member;
        }
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Line([CallerLineNumber] int line = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(line);
            return sb.ToString();
        }
        public static string FilePos(string? text = null,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            var sb = GetDebugText(text, file, member, line);
            return sb.ToString();
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CallerInfo(this string message, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string sourceFilePath = "",
                  [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;

            return $"{fileName}.cs {line} [{methodName}] {message}";
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        //public static Action<ILogger, T1, Exception> Define<T1>(
        //LogLevel logLevel, EventId eventId, string formatString)
        //{
        //    var formatter = CreateLogValuesFormatter(
        //        formatString, expectedNamedParameterCount: 1);
        //    return (logger, arg1, exception) =>
        //    {
        //        if (logger.IsEnabled(logLevel))
        //        {
        //            logger.Log(logLevel, eventId,
        //           new LogValues<T1>(formatter, arg1),
        //           exception, LogValues<T1>.Callback);
        //        }
        //    };
        //}
        private static string GetMsg(string message, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            var fileName = Path.GetFileName(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;
            int n = Environment.StackTrace.Split(Environment.NewLine).Count();
            if (n < Logger.IndentShift)
                Logger.IndentShift = n;
            //System.Diagnostics.Debug.Assert(n >= Logger.IndentShift);
            var indent = new String(' ', n - Logger.IndentShift);
            string msg = "";
            if (message != null)
                msg = $"{indent}{message} [{methodName}] {fileName} {line}";
            else
                msg = $"{indent}[{methodName}] {fileName} {line}";
            return msg;
        }
        public class Dummy { }

        #region Trace
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void TraceMethodStart<T0, T1, T2, T3, T4, T5, T6>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        //    LoggerExt.Dummy? dummy = null,
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    if (logger == null || !logger.IsEnabled(LogLevel.Trace))
        //        return;
        //    var sb = new StringBuilder();
        //    var method = new StackTrace().GetFrame(2).GetMethod();
        //    //sb.Append("Method is starting. Class = ");
        //    //sb.Append(method.DeclaringType.FullName);
        //    //sb.Append("Method = ");
        //    sb.Append("Method is starting. ");
        //    sb.Append(method.Name);
        //    sb.Append("(");
        //    var sep = "";
        //    for (int i = 0; i < method.GetParameters().Length; i++)
        //    {
        //        sb.Append(sep);
        //        sb.Append(method.GetParameters().GetValue(i));
        //        sep = ", ";
        //    }
        //    sb.Append(")");
        //    var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, null, msg);
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace(this Microsoft.Extensions.Logging.ILogger? logger, string? message = null,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg);
        }
        public static void Trace(this Microsoft.Extensions.Logging.ILogger? logger, string? message, Action<StringBuilder> messageBuilder,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var sb = new StringBuilder();
            sb.AppendLine(message);
            messageBuilder(sb);
            var msg = GetMsg(sb.ToString(), memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg);
        }
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void Trace(this Microsoft.Extensions.Logging.ILogger? logger, string message, object?[] prms,
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    if (logger == null || !logger.IsEnabled(LogLevel.Trace))
        //        return;
        //    var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, prms);
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace<T0>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, arg0);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace<T0, T1>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, arg0, arg1);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace<T0, T1, T2>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, arg0, arg1, arg2);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace<T0, T1, T2, T3>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, arg0, arg1, arg2, arg3);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace<T0, T1, T2, T3, T4>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, arg0, arg1, arg2, arg3, arg4);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace<T0, T1, T2, T3, T4, T5>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Trace<T0, T1, T2, T3, T4, T5, T6>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Trace))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, msg, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        #endregion Trace

        #region Debug

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug(this Microsoft.Extensions.Logging.ILogger? logger, string? message = null,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg);
        }
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void Debug(this Microsoft.Extensions.Logging.ILogger? logger, string message, object?[] prms,
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    if (logger == null || !logger.IsEnabled(LogLevel.Debug))
        //        return;
        //    var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, prms);
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug<T0>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, arg0);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug<T0, T1>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, arg0, arg1);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug<T0, T1, T2>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, arg0, arg1, arg2);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug<T0, T1, T2, T3>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, arg0, arg1, arg2, arg3);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug<T0, T1, T2, T3, T4>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, arg0, arg1, arg2, arg3, arg4);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug<T0, T1, T2, T3, T4, T5>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Debug<T0, T1, T2, T3, T4, T5, T6>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Debug))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, msg, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        #endregion Debug

        #region Information
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information(this Microsoft.Extensions.Logging.ILogger? logger, string? message = null,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, null, msg);
        }
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void Information(this Microsoft.Extensions.Logging.ILogger? logger, string message, object?[] prms,
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    if (logger == null || !logger.IsEnabled(LogLevel.Information))
        //        return;
        //    var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, prms);
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information<T0>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, arg0);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information<T0, T1>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, arg0, arg1);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information<T0, T1, T2>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, arg0, arg1, arg2);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information<T0, T1, T2, T3>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, arg0, arg1, arg2, arg3);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information<T0, T1, T2, T3, T4>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, arg0, arg1, arg2, arg3, arg4);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information<T0, T1, T2, T3, T4, T5>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Information<T0, T1, T2, T3, T4, T5, T6>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Information))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, msg, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        #endregion Information

        #region Warning
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning(this Microsoft.Extensions.Logging.ILogger? logger, string? message = null,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, null, msg);
        }
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void Warning(this Microsoft.Extensions.Logging.ILogger? logger, string message, object?[] prms,
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    if (logger == null || !logger.IsEnabled(LogLevel.Warning))
        //        return;
        //    var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, prms);
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning<T0>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, arg0);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning<T0, T1>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, arg0, arg1);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning<T0, T1, T2>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, arg0, arg1, arg2);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning<T0, T1, T2, T3>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, arg0, arg1, arg2, arg3);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning<T0, T1, T2, T3, T4>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, arg0, arg1, arg2, arg3, arg4);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning<T0, T1, T2, T3, T4, T5>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Warning<T0, T1, T2, T3, T4, T5, T6>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Warning))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, msg, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        #endregion Warning

        #region Error
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error(this Microsoft.Extensions.Logging.ILogger? logger, string? message = null,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, null, msg);
        }
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void Error(this Microsoft.Extensions.Logging.ILogger? logger, string message, object?[] prms,
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    if (logger == null || !logger.IsEnabled(LogLevel.Error))
        //        return;
        //    var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, prms);
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error<T0>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, arg0);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error<T0, T1>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, arg0, arg1);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error<T0, T1, T2>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, arg0, arg1, arg2);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error<T0, T1, T2, T3>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, arg0, arg1, arg2, arg3);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error<T0, T1, T2, T3, T4>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, arg0, arg1, arg2, arg3, arg4);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error<T0, T1, T2, T3, T4, T5>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Error<T0, T1, T2, T3, T4, T5, T6>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Error))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, msg, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        #endregion Error

        #region Critical
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical(this Microsoft.Extensions.Logging.ILogger? logger, Exception ex, string? message = null,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, ex, msg);
        }
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static void Critical(this Microsoft.Extensions.Logging.ILogger? logger, Exception ex, string message, object?[] prms,
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    if (logger == null || !logger.IsEnabled(LogLevel.Critical))
        //        return;
        //    var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, ex, msg, prms);
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical<T0>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, msg, arg0);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical<T0, T1>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, msg, arg0, arg1);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical<T0, T1, T2>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, msg, arg0, arg1, arg2);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical<T0, T1, T2, T3>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, msg, arg0, arg1, arg2, arg3);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical<T0, T1, T2, T3, T4>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, msg, arg0, arg1, arg2, arg3, arg4);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical<T0, T1, T2, T3, T4, T5>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, msg, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Critical<T0, T1, T2, T3, T4, T5, T6>(this Microsoft.Extensions.Logging.ILogger? logger, string message, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            LoggerExt.Dummy? dummy = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (logger == null || !logger.IsEnabled(LogLevel.Critical))
                return;
            var msg = GetMsg(message, memberName, sourceFilePath, sourceLineNumber);
            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, msg, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        #endregion Critical

        internal static StringBuilder GetDebugText(string? text, string file, string member, int line, int maxStackDeep = -1)
        {
            //int stackLength = 3;
            int memberLength = 30;
            int lineLength = 4; // 4
            var ext = "...";
            var lineStr = line.ToString();
            System.Diagnostics.Debug.Assert(lineLength == 0 || lineStr.Length <= lineLength);
            //var indent = Environment.StackTrace.Split('\n').Length - 5;
            //Debug.Assert(indent >= 0);
            //string indStr = new String(' ', indent);
            StringBuilder sb = new StringBuilder();
            if (maxStackDeep > 0)
            {
                sb.AppendLine(" ");
                var lstStack = Environment.StackTrace.Split("\r\n");
                var lst = new List<string>();
                //int i = 0;
                int i = -1; // to keep stack deepness when avoiding repeating 'at' information
                bool isDebugExt = false;
                foreach (var t in lstStack)
                {
                    if (!isDebugExt)
                    {
                        if (!t.Contains("vSharpStudio.common.DebugExt"))
                            continue;
                    }
                    if (isDebugExt)
                    {
                        if (!t.Contains(":line"))
                            break;
                        lst.Add(t.Replace("at ", ""));
                        i++;
                        if (i >= maxStackDeep)
                            break;
                        continue;
                    }
                    isDebugExt = true;
                }
                //for (int j = lst.Count - 1; j >= 0; --j)
                for (int j = lst.Count - 1; j > 0; --j) // to avoid repeating 'at' information
                {
                    sb.AppendLine(lst[j]);
                }
            }
            if (maxStackDeep == 0)
            {
                if (member.Length > memberLength)
                {
                    var lenBeg = (memberLength - ext.Length) / 2;
                    sb.Append(member.Substring(0, lenBeg));
                    sb.Append(ext);
                    sb.Append(member.Substring(member.Length - (memberLength - lenBeg - ext.Length)));
                }
                else
                {
                    sb.Append(member);
                    if (member.Length < memberLength)
                        sb.Append(new String(' ', memberLength - member.Length));
                }
                sb.Append(' ');
            }

            //var indent = Environment.StackTrace.Split('\n').Length;
            //sb.Append("Stack Level:");
            //sb.Append(indent.ToString().PadLeft(stackLength / 10 + 1));
            //sb.Append(", ");
            //if (maxStackDeep == 0)
            //{
            //    if (lineLength > 0)
            //    {
            //        if (lineLength > lineStr.Length)
            //            sb.Append(new String(' ', lineLength - lineStr.Length));
            //        sb.Append(lineStr);
            //        sb.Append(' ');
            //    }
            //}

            // same position for all calls
            if (text != null)
            {
                //sb.Append(" Message: \"");
                sb.Append("\"");
                sb.Append(text);
                sb.Append("\" ");
            }
            else
            {
                sb.Append("\"\" ");
            }
            sb.Append(" Member: ");
            sb.Append(member);
            sb.Append(" File: ");
            sb.Append(Path.GetFileName(file));
            //sb.Append(file);
            sb.Append(" Line: ");
            sb.Append(line);
            return sb;
        }
    }
}
//namespace Microsoft.Extensions.Logging
//{
//    public static class LogExt
//    {
//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static string Line([CallerLineNumber] int line = 0)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append(line);
//            return sb.ToString();
//        }
//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static string FilePos([CallerFilePath] string file = "",
//                                        [CallerMemberName] string member = "",
//                                        [CallerLineNumber] int line = 0)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append(Path.GetFileName(file));
//            sb.Append(" Line: ");
//            sb.Append(line);
//            return sb.ToString();
//        }
//        public static string FilePos(this string text,
//                                        [CallerFilePath] string file = "",
//                                        [CallerMemberName] string member = "",
//                                        [CallerLineNumber] int line = 0)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append(text);
//            sb.Append(" ");
//            sb.Append(Path.GetFileName(file));
//            sb.Append(" Line: ");
//            sb.Append(line);
//            return sb.ToString();
//        }
//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static string CallerInfo(this string message, [CallerMemberName] string memberName = "",
//                  [CallerFilePath] string sourceFilePath = "",
//                  [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;

//            return $"{fileName}.cs {line} [{methodName}] {message}";
//        }
//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static string StackInfo(this string message,
//            [CallerMemberName] string memberName = "",
//            [CallerFilePath] string sourceFilePath = "",
//            [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;
//            return $"{fileName} {line} [{methodName}] {message}\n   StackTrace:\n{Environment.StackTrace}";
//        }
//        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static void Trace(this Microsoft.Extensions.Logging.ILogger? logger, string message = "",
//            [CallerMemberName] string memberName = "",
//            [CallerFilePath] string sourceFilePath = "",
//            [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;
//            var msg = $"{fileName} {line} [{methodName}] {message}";
//            logger.Log(Microsoft.Extensions.Logging.LogLevel.Trace, null, msg);
//        }
//        public static void Debug(this Microsoft.Extensions.Logging.ILogger? logger, string message = "",
//            [CallerMemberName] string memberName = "",
//            [CallerFilePath] string sourceFilePath = "",
//            [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;
//            var msg = $"{fileName} {line} [{methodName}] {message}";
//            logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, null, msg);
//        }
//        public static void Information(this Microsoft.Extensions.Logging.ILogger? logger, string message = "",
//            [CallerMemberName] string memberName = "",
//            [CallerFilePath] string sourceFilePath = "",
//            [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;
//            var msg = $"{fileName} {line} [{methodName}] {message}";
//            logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, null, msg);
//        }
//        public static void Warning(this Microsoft.Extensions.Logging.ILogger? logger, string message = "",
//            [CallerMemberName] string memberName = "",
//            [CallerFilePath] string sourceFilePath = "",
//            [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;
//            var msg = $"{fileName} {line} [{methodName}] {message}";
//            logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, null, msg);
//        }
//        public static void LoggerError(this Microsoft.Extensions.Logging.ILogger? logger, string message = "",
//            [CallerMemberName] string memberName = "",
//            [CallerFilePath] string sourceFilePath = "",
//            [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;
//            var msg = $"{fileName} {line} [{methodName}] {message}";
//            logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, null, msg);
//        }
//        public static void Critical(this Microsoft.Extensions.Logging.ILogger? logger, string message = "",
//            [CallerMemberName] string memberName = "",
//            [CallerFilePath] string sourceFilePath = "",
//            [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;
//            var msg = $"{fileName} {line} [{methodName}] {message}";
//            logger.Log(Microsoft.Extensions.Logging.LogLevel.Critical, null, msg);
//        }
//    }
//}
