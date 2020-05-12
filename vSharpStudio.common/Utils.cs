using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace vSharpStudio.common
{
    public class Utils
    {
#if NET48
        public static string GetRelativePath(string relativeTo, string path)
        {
            var fullPath = Path.GetFullPath(path);
            string rel = fullPath.Replace(relativeTo, "");
            return rel;
        }
#endif
    }
}
