﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public static class LoggerExt
    {
        internal static string FilePos(this string text,
                                        [CallerFilePath] string file = "",
                                        [CallerMemberName] string member = "",
                                        [CallerLineNumber] int line = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(text);
            sb.Append(" ");
            sb.Append(Path.GetFileName(file));
            sb.Append(" Line: ");
            sb.Append(line);
            return sb.ToString();
        }
    }
}
