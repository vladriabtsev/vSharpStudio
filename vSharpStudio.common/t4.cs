using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace vSharpStudio.common
{
    public class t4
    {
        public static string FilePos(string text = "",
                                [CallerFilePath] string file = "",
                                [CallerMemberName] string member = "",
                                [CallerLineNumber] int line = 0)
        {
            return text + Path.GetFileName(file) + " Line: " + line;
        }
    }
}
