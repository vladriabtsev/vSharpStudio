using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using static System.Net.Mime.MediaTypeNames;

namespace vSharpStudio.common
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
            Debug.WriteLine(LogExt.GetDebugText(message, file, member, line, (int)stackDeepness).ToString());
        }
        [Conditional("DEBUG")]
        // Use after DebugExt.WriteLineWithStack later in a same procedure
        public static void WriteLine(string? message = null,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            Debug.WriteLine(LogExt.GetDebugText(message, file, member, line, -1).ToString());
        }
        [Conditional("DEBUG")]
        public static void Write(string message, bool isWithPosition = false,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            if (isWithPosition)
                Debug.Write(LogExt.GetDebugText(message, file, member, line, -1).ToString());
            else
                Debug.Write(message);
        }
        public static string FilePos(this string text,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            return LogExt.FilePos(text, file, member, line);
        }
    }
    public static class LogExt
    {
        public static string Member([CallerMemberName] string member = "")
        {
            return member;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CallerInfo(this string message, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string sourceFilePath = "",
                  [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            var methodName = memberName;
            var line = sourceLineNumber;

            return $"{fileName}.cs {line} [{methodName}] {message}";
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        //public static void Debug(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
        //    [CallerMemberName] string memberName = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
        //    var methodName = memberName;
        //    var line = sourceLineNumber;
        //    var msg = $"{fileName} {line} [{methodName}] {message}";
        //    logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, null, msg);
        //}
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
        internal static StringBuilder GetDebugText(string? text, string file, string member, int line, int maxStackDeep = -1)
        {
            int stackLength = 3;
            int memberLength = 30;
            int lineLength = 4; // 4
            var ext = "...";
            var lineStr = line.ToString();
            Debug.Assert(lineLength == 0 || lineStr.Length <= lineLength);
            //var indent = Environment.StackTrace.Split('\n').Length - 5;
            //Debug.Assert(indent >= 0);
            //string indStr = new String(' ', indent);
            StringBuilder sb = new StringBuilder();
            if (maxStackDeep > 0)
            {
                sb.AppendLine(" ");
                var lstStack = Environment.StackTrace.Split("\r\n");
                var lst = new List<string>();
                int i = 0;
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
                        lst.Add(t);
                        i++;
                        if (i >= maxStackDeep)
                            break;
                        continue;
                    }
                    isDebugExt = true;
                }
                for (int j = lst.Count - 1; j >= 0; --j)
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
            var indent = Environment.StackTrace.Split('\n').Length;
            sb.Append(indent.ToString().PadLeft(stackLength));
            sb.Append(", ");
            if (maxStackDeep == 0)
            {
                if (lineLength > 0)
                {
                    if (lineLength > lineStr.Length)
                        sb.Append(new String(' ', lineLength - lineStr.Length));
                    sb.Append(lineStr);
                    sb.Append(' ');
                }
            }
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
            sb.Append(" --- File: ");
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
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static string Line([CallerLineNumber] int line = 0)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append(line);
//            return sb.ToString();
//        }
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static string CallerInfo(this string message, [CallerMemberName] string memberName = "",
//                  [CallerFilePath] string sourceFilePath = "",
//                  [CallerLineNumber] int sourceLineNumber = 0)
//        {
//            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
//            var methodName = memberName;
//            var line = sourceLineNumber;

//            return $"{fileName}.cs {line} [{methodName}] {message}";
//        }
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static void Trace(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
//        public static void Debug(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
//        public static void Information(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
//        public static void Warning(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
//        public static void LoggerError(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
//        public static void Critical(this Microsoft.Extensions.Logging.ILogger logger, string message = "",
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
