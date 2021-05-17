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
    public enum EnumVisitType { Load, Remove }
    public class TableInfo
    {
        public string ClassName { get; set; }
        public string TableName { get; set; }
        public string TableParent { get; set; }
        public ITreeConfigNode Node { get; set; }
        public IReadOnlyList<IProperty> List { get; set; }
    }
}
