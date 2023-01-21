using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Numerics;
using Xceed.Wpf.Toolkit;

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
        public static string GetProtoTypeForNumeric(IProperty p)
        {
            return GetProtoTypeForNumeric(p.IsNullable, p.DataType.MaxNumericalValue, p.DataType.IsPositive, p.DataType.Accuracy, p.DataType.Length);
        }
        public static string GetProtoTypeForNumeric(bool isNullable, BigInteger? max_value, bool is_positive, uint accuracy, uint length)
        {
            // https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/protobuf-data-types
            if (accuracy == 0)
            {
                if (is_positive)
                {
                    if (max_value <= uint.MaxValue)
                    {
                        if (isNullable)
                            return "google.protobuf.UInt32Value";
                        else
                            return "uint32";
                    }
                    if (max_value <= long.MaxValue) // long, not ulong
                    {
                        if (isNullable)
                            return "google.protobuf.UInt64Value";
                        else
                            return "uint64";
                    }
                    if (length <= 28)
                    {
                        if (isNullable)
                            return "customTypes.DecimalValueNullable";
                        else
                            return "customTypes.DecimalValue";
                    }
                    return "bytes"; // need conversions
                }
                else
                {
                    if (max_value <= int.MaxValue)
                    {
                        if (isNullable)
                            return "google.protobuf.Int32Value";
                        else
                            return "int32";
                    }
                    if (max_value <= long.MaxValue)
                    {
                        if (isNullable)
                            return "google.protobuf.Int64Value";
                        else
                            return "int64";
                    }
                    if (length <= 28)
                    {
                        if (isNullable)
                            return "customTypes.DecimalValueNullable";
                        else
                            return "customTypes.DecimalValue";
                    }
                    return "bytes"; // need conversions
                }
            }
            else
            {
                // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                if (length <= 6)
                {
                    if (isNullable)
                        return "google.protobuf.FloatValue";
                    else
                        return "float";
                }
                if (length <= 15)
                {
                    if (isNullable)
                        return "google.protobuf.DoubleValue";
                    else
                        return "double";
                }
                if (length <= 28)
                {
                    if (isNullable)
                        return "customTypes.DecimalValueNullable";
                    else
                        return "customTypes.DecimalValue";
                }
                return "bytes"; // need conversions
            }
        }
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
        public static string ToGrpcName(this string s)
        {
            var sb = new StringBuilder();
            var c = s[0];
            if (char.IsLower(c))
            {
                var cc = char.ToUpper(c);
                sb.Append(cc);
            }
            else
                sb.Append(c);
            for (int i = 1; i < s.Length; i++)
            {
                c = s[i];
                if (c == '_')
                {
                    while (s[i + 1] == '_')
                        i++;
                    i++;
                    c = s[i];
                    var cc = char.ToUpper(c);
                    sb.Append(cc);
                }
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }
        public static void WriteToFileIfNotExist(string code, string path, string fileRelativePath, string fileName)
        {
            string outFolder = Path.Combine(path, fileRelativePath);
            string outFile;
            if (Path.EndsInDirectorySeparator(outFolder))
                outFile = $"{outFolder}{fileName}";
            else
                outFile = $"{outFolder}\\{fileName}";
            if (!File.Exists(outFile))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(code);
                File.WriteAllBytes(outFile, bytes);
            }
        }
        public static void WriteToFile(string code, string path, string fileRelativePath, string fileName)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(code);
            string outFolder = Path.Combine(path, fileRelativePath);
            string outFile;
            if (Path.EndsInDirectorySeparator(outFolder))
                outFile = $"{outFolder}{fileName}";
            else
                outFile = $"{outFolder}\\{fileName}";
            File.WriteAllBytes(outFile, bytes);
        }
        public static void WriteToFile(byte[] bytes, string path, string fileRelativePath, string fileName)
        {
            string outFolder = Path.Combine(path, fileRelativePath);
            string outFile;
            if (Path.EndsInDirectorySeparator(outFolder))
                outFile = $"{outFolder}{fileName}";
            else
                outFile = $"{outFolder}\\{fileName}";
            File.WriteAllBytes(outFile, bytes);
        }
        public static string GetOuputFilePath(string currentCfgFolderPath, IAppSolution ts, IAppProject tp, IAppProjectGenerator tpg, string fileName)
        {
            return GetOuputFilePath(currentCfgFolderPath, ts, tp, tpg.RelativePathToGenFolder, fileName);
        }
        public static string GetOuputFilePath(string currentCfgFolderPath, IAppSolution ts, IAppProject tp, string relativePathToGenFolder, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(currentCfgFolderPath);
            if (!currentCfgFolderPath.EndsWith('\\'))
            {
                sb.Append('\\');
            }
            var folder = Path.GetDirectoryName(ts.RelativeAppSolutionPath);
            if (folder?.Length > 0)
            {
                sb.Append(folder);
                sb.Append('\\');
            }
            folder = Path.GetDirectoryName(tp.RelativeAppProjectPath);
            if (folder?.Length > 0)
            {
                sb.Append(folder);
                sb.Append('\\');
            }
            if (!string.IsNullOrWhiteSpace(relativePathToGenFolder))
            {
                sb.Append(relativePathToGenFolder);
                if (relativePathToGenFolder[relativePathToGenFolder.Length - 1] != '\\')
                    sb.Append('\\');
            }
            sb.Append(fileName);
            return sb.ToString();
        }
        // https://docs.microsoft.com/en-us/dotnet/csharp/codedoc
        private static string commentBegSummary = "/// <summary>";
        private static string commentEndSummary = "/// </summary>";
        private static string comment = "/// ";
        public static string Comment(IGroupListConstants t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string Comment(IEnumeration t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string Comment(IEnumerationPair t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string Comment(IConstant t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string Comment(ICatalog t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string Comment(IDocument t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string Comment(IDetail t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string Comment(IProperty t, string indent = "")
        {
            var sb = new StringBuilder();
            if (t.NameUi != t.Name || !string.IsNullOrWhiteSpace(t.Description))
            {
                sb.Append(indent);
                sb.Append(commentBegSummary);
                sb.AppendLine();

                if (t.NameUi != t.Name)
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    sb.Append("UI name: ");
                    sb.Append(t.NameUi);
                    sb.AppendLine();
                }

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
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
    public enum EnumVisitType { Load, Remove }
    public class TableInfo
    {
        public TableInfo(string className, string tableName, string tableParent, ITreeConfigNode node, IReadOnlyList<IProperty> lst)
        {
            this.ClassName = className;
            this.TableName = tableName;
            this.TableParent = tableParent;
            this.Node = node;
            this.List = lst;
        }
        public string ClassName { get; set; }
        public string TableName { get; set; }
        public string TableParent { get; set; }
        public ITreeConfigNode Node { get; set; }
        public IReadOnlyList<IProperty> List { get; set; }
    }
}
