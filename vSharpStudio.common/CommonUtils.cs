using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace vSharpStudio.common
{
    public static class CommonUtils
    {
#if NET48
        public static string GetRelativePath(string relativeTo, string path)
        {
            var fullPath = Path.GetFullPath(path);
            string rel = fullPath.Replace(relativeTo, "");
            return rel;
        }
#endif
        public static string ToProtoName(this string s)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (char.IsUpper(c))
                {
                    if (i > 0)
                        sb.Append('_');
                    var cc = char.ToLower(c);
                    sb.Append(cc);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string GetOuputFilePath(string currentCfgFolderPath, IAppSolution ts, IAppProject tp, IAppProjectGenerator tpg, string fileName)
        {
            return GetOuputFilePath(currentCfgFolderPath, ts, tp, tpg.RelativePathToGenFolder, fileName);
        }
        public static string GetOuputFilePath(string currentCfgFolderPath, IAppSolution ts, IAppProject tp, string relativePathToGenFolder, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(currentCfgFolderPath);
            var folder = Path.GetDirectoryName(ts.RelativeAppSolutionPath);
            if (folder?.Length > 0)
            {
                sb.Append(folder);
                sb.Append("\\");
            }
            folder = Path.GetDirectoryName(tp.RelativeAppProjectPath);
            if (folder?.Length > 0)
            {
                sb.Append(folder);
                sb.Append("\\");
            }
            if (!string.IsNullOrWhiteSpace(relativePathToGenFolder))
            {
                sb.Append(relativePathToGenFolder);
                if (relativePathToGenFolder[relativePathToGenFolder.Length - 1] != '\\')
                    sb.Append("\\");
            }
            sb.Append(fileName);
            return sb.ToString();
        }
        // https://docs.microsoft.com/en-us/dotnet/csharp/codedoc
        private static string commentBegSummary = "/// <summary>";
        private static string commentEndSummary = "/// </summary>";
        private static string comment = "/// ";
        public static string Comment(IEnumeration t, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(commentBegSummary);
            sb.AppendLine();

            sb.Append(indent);
            sb.Append(comment);
            sb.Append("UI name: ");
            sb.Append(t.NameUi);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(comment);
                //sb.Append("Description: ");
                sb.Append(t.Description);
                sb.AppendLine();
            }

            sb.Append(indent);
            sb.Append(commentEndSummary);
            return sb.ToString();
        }
        public static string Comment(IEnumerationPair t, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(commentBegSummary);
            sb.AppendLine();

            sb.Append(indent);
            sb.Append(comment);
            sb.Append("UI name: ");
            sb.Append(t.NameUi);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(comment);
                //sb.Append("Description: ");
                sb.Append(t.Description);
                sb.AppendLine();
            }

            sb.Append(indent);
            sb.Append(commentEndSummary);
            return sb.ToString();
        }
        public static string Comment(IConstant t, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(commentBegSummary);
            sb.AppendLine();

            sb.Append(indent);
            sb.Append(comment);
            sb.Append("UI name: ");
            sb.Append(t.NameUi);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(comment);
                //sb.Append("Description: ");
                sb.Append(t.Description);
                sb.AppendLine();
            }

            sb.Append(indent);
            sb.Append(commentEndSummary);
            return sb.ToString();
        }
        public static string Comment(ICatalog t, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(commentBegSummary);
            sb.AppendLine();

            sb.Append(indent);
            sb.Append(comment);
            sb.Append("UI name: ");
            sb.Append(t.NameUi);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(comment);
                //sb.Append("Description: ");
                sb.Append(t.Description);
                sb.AppendLine();
            }

            sb.Append(indent);
            sb.Append(commentEndSummary);
            return sb.ToString();
        }
        public static string Comment(IDocument t, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(commentBegSummary);
            sb.AppendLine();

            sb.Append(indent);
            sb.Append(comment);
            sb.Append("UI name: ");
            sb.Append(t.NameUi);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(comment);
                //sb.Append("Description: ");
                sb.Append(t.Description);
                sb.AppendLine();
            }

            sb.Append(indent);
            sb.Append(commentEndSummary);
            return sb.ToString();
        }
        public static string Comment(IPropertiesTab t, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(commentBegSummary);
            sb.AppendLine();

            sb.Append(indent);
            sb.Append(comment);
            sb.Append("UI name: ");
            sb.Append(t.NameUi);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(comment);
                //sb.Append("Description: ");
                sb.Append(t.Description);
                sb.AppendLine();
            }

            sb.Append(indent);
            sb.Append(commentEndSummary);
            return sb.ToString();
        }
        public static string Comment(IProperty t, string indent = "")
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(commentBegSummary);
            sb.AppendLine();

            sb.Append(indent);
            sb.Append(comment);
            sb.Append("UI name: ");
            sb.Append(t.NameUi);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(comment);
                //sb.Append("Description: ");
                sb.Append(t.Description);
                sb.AppendLine();
            }

            sb.Append(indent);
            sb.Append(commentEndSummary);
            return sb.ToString();
        }
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
