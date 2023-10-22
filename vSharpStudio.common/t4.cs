using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace vSharpStudio.common
{
    public static class t4
    {
        public static string FilePos(string? text = null,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumber] int line = 0)
        {
            var indx = file.IndexOf("vPlugin");
            if (indx > 0)
            {
                if (text != null)
                    return $"{text} ...{file.Substring(indx)} Line:{line}";
                return $"...{file.Substring(indx)} Line:{line}";
            }
            if (text != null)
                return $"{text} {file} Line:{line}";
            return $"{file} Line:{line}";
        }
        //public static string FilePos(this string text,
        //                        [CallerFilePath] string file = "",
        //                        [CallerMemberName] string member = "",
        //                        [CallerLineNumber] int line = 0)
        //{
        //    var indx = file.IndexOf("vPlugin");
        //    if (indx > 0)
        //    {
        //        if (text != null)
        //            return $"{text} ...{file.Substring(indx)} Line:{line}";
        //        return $"...{file.Substring(indx)} Line:{line}";
        //    }
        //    if (text != null)
        //        return $"{text} {file} Line:{line}";
        //    return $"{file} Line:{line}";
        //}
        //public static string Line([CallerLineNumber] int line = 0)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(line);
        //    return sb.ToString();
        //}
    }
}
