using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.Versioning;
using System.Text;
using Google.Protobuf;

namespace vSharpStudio.common
{
    public static class CommonUtils
    {
        #region Model extensions

        public static List<IProperty> GetIncludedExtendedConstantsAsProperties(this IGroupListConstants dt, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = dt.GetIncludedConstantsAsProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(dt, lst, isSkipComplex, isSkipComplexDescr);
        }
        public static List<IProperty> GetIncludedExtendedProperties(this ICatalog c, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = c.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(c, lst, isSkipComplex, isSkipComplexDescr);
        }

        public static List<IProperty> GetIncludedExtendedProperties(this ICatalogFolder cf, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = cf.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(cf, lst, isSkipComplex, isSkipComplexDescr);
        }

        public static List<IProperty> GetIncludedExtendedProperties(this IDetail dt, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = dt.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(dt, lst, isSkipComplex, isSkipComplexDescr);
        }

        #region IDocument
        public static List<IProperty> GetIncludedExtendedPropertiesShared(this IDocument d, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr)
        {
            var lst = d.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, true, true);
            return ExtendComplexProperties(d, lst, isSkipComplex, isSkipComplexDescr);
        }
        public static List<IProperty> GetIncludedExtendedProperties(this IDocument d, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = d.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(d, lst, isSkipComplex, isSkipComplexDescr);
        }
        public static List<IProperty> GetIncludedExtendedPropertiesWithoutShared(this IDocument d, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = d.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial, false, true);
            return ExtendComplexProperties(d, lst, isSkipComplex, isSkipComplexDescr);
        }
        public static List<IProperty> GetIncludedExtendedProperties(this IDocumentTimeline tl, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = tl.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(tl, lst, isSkipComplex, isSkipComplexDescr);
        }
        #endregion IDocument

        public static List<IProperty> GetIncludedExtendedProperties(this IRelationManyToMany r, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = r.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(r, lst, isSkipComplex, isSkipComplexDescr);
        }
        public static List<IProperty> GetIncludedTurnoverExtendedProperties(this IRegister r, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = r.GetIncludedTurnoverProperties(isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(r, lst, isSkipComplex, isSkipComplexDescr);
        }
        public static List<IProperty> GetIncludedBalanceExtendedProperties(this IRegister r, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            var lst = r.GetIncludedBalanceProperties(isOptimistic, isExcludeSpecial);
            return ExtendComplexProperties(r, lst, isSkipComplex, isSkipComplexDescr);
        }
        //public static List<IProperty> GetIncludedExtendedProperties(this IRegister r, string guidAppPrjDbGen, bool isOptimistic, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        //{
        //    var lst = r.GetIncludedProperties(guidAppPrjDbGen, isOptimistic, isExcludeSpecial);
        //    return ExtendComplexProperties(r, lst, isSkipComplex, isSkipComplexDescr);
        //}
        public static List<IProperty> GetExtendedProperties(this IForm r, bool isSkipComplex, bool isSkipComplexDescr, bool isExcludeSpecial)
        {
            return ExtendComplexProperties(r, r.ListProperties, isSkipComplex, isSkipComplexDescr);
        }
        public static List<IProperty> ToSimplePropertiesList(this IReadOnlyList<IProperty> lstModelProperties)
        {
            var res = new List<IProperty>();
            foreach (var t in lstModelProperties)
            {
                if (t.IsComplex)
                    continue;
                res.Add(t);
            }
            return res;
        }
        public static List<IProperty> ExtendComplexProperties(ITreeConfigNode parentWithPositions, IReadOnlyList<IProperty> lstModelProperties, bool isSkipComplex, bool isSkipComplexDescr)
        {
            Debug.Assert(parentWithPositions is INodeWithPositionProperties);
#if DEBUG
            var hash = new HashSet<string>();
#endif
            var model = parentWithPositions.Cfg.Model;
            var lst = new List<IProperty>();
            foreach (var t in lstModelProperties)
            {
                if (t.IsViewDefault)
                {
                    lst.Add(t);
                    continue;
                }
                switch (t.DataType.DataTypeEnum)
                {
                    case EnumDataType.CATALOG:
                    case EnumDataType.DOCUMENT:
                        Debug.Assert(t.IsComplex);
                        var nameSuffix = "Ref" + ((ICompositeName)t.Cfg.DicNodes[t.DataType.ObjectRef.ForeignObjectGuid]).CompositeName;
                        t.DataType.ComplexRefSuffix = nameSuffix;
                        var p = model.GetPropertySpecial(parentWithPositions, t, null, EnumSpecialPropertyType.SUB_PROPERTY_REF_ID, nameSuffix + "Id");
                        lst.Add(p);
#if DEBUG
                        Debug.Assert(!hash.Contains(p.Guid));
                        hash.Add(p.Guid);
#endif
                        if (!isSkipComplexDescr)
                        {
                            p = model.GetPropertySpecial(parentWithPositions, t, null, EnumSpecialPropertyType.SUB_PROPERTY_DESCR, "Descr");
                            lst.Add(p);
#if DEBUG
                            Debug.Assert(!hash.Contains(p.Guid));
                            hash.Add(p.Guid);
#endif
                        }
                        if (isSkipComplex)
                            continue;
                        break;
                    case EnumDataType.CATALOGS:
                    case EnumDataType.DOCUMENTS:
                    case EnumDataType.ANY:
                        Debug.Assert(t.IsComplex);
                        foreach (var tt in t.DataType.ListObjectRefs)
                        {
                            nameSuffix = "Ref" + ((ICompositeName)t.Cfg.DicNodes[tt.ForeignObjectGuid]).CompositeName;
                            p = model.GetPropertySpecial(parentWithPositions, t, tt, EnumSpecialPropertyType.SUB_PROPERTY_REF_ID, nameSuffix + "Id");
                            lst.Add(p);
#if DEBUG
                            Debug.Assert(!hash.Contains(p.Guid));
                            hash.Add(p.Guid);
#endif
                        }
                        p = model.GetPropertySpecial(parentWithPositions, t, null, EnumSpecialPropertyType.SUB_PROPERTY_GD, "Gd");
                        lst.Add(p);
#if DEBUG
                        Debug.Assert(!hash.Contains(p.Guid));
                        hash.Add(p.Guid);
#endif
                        if (!isSkipComplexDescr)
                        {
                            p = model.GetPropertySpecial(parentWithPositions, t, null, EnumSpecialPropertyType.SUB_PROPERTY_DESCR, "Descr");
                            lst.Add(p);
#if DEBUG
                            Debug.Assert(!hash.Contains(p.Guid));
                            hash.Add(p.Guid);
#endif
                        }
                        if (isSkipComplex)
                            continue;
                        break;
                    default:
                        break;
                }
                lst.Add(t);
                //#if DEBUG
                //                Debug.Assert(!hash.Contains(t.Guid));
                //                hash.Add(t.Guid);
                //#endif
            }
            return lst;
        }

        #endregion Model extensions

        public static T ParseJson<T>(string json, bool discardUnknownFields = true) where T : IMessage<T>, new()
        {
            var jp = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(discardUnknownFields));
            T msg = jp.Parse<T>(json);
            return msg;
        }
        public static string ToMessage(this Exception ex)
        {
            return ToInnerMessage(ex);
        }
        private static string ToInnerMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ToInnerMessage(ex.InnerException);
            }
            return ex.Message;
        }
        //private static void ToInnerMessage(StringBuilder sb, Exception ex, string indent = "")
        //{
        //    sb.Append(indent);
        //    sb.AppendLine(ex.Message);
        //    if (ex.InnerException != null)
        //    {
        //        ToInnerMessage(sb, ex.InnerException, indent + "  ");
        //    }
        //}
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
            return GetProtoTypeForNumeric(p.DataType.IsNullable, p.DataType.MaxNumericalValue, p.DataType.IsPositive, p.DataType.Accuracy, p.DataType.Length);
        }
        public static string GetProtoTypeForCsNumeric(string csNumericType)
        {
            return csNumericType switch
            {
                "sbyte" => GetProtoTypeForNumeric(false, sbyte.MaxValue, false, 0, 28),
                "byte" => GetProtoTypeForNumeric(false, byte.MaxValue, false, 0, 28),
                "short" => GetProtoTypeForNumeric(false, short.MaxValue, false, 0, 28),
                "ushort" => GetProtoTypeForNumeric(false, ushort.MaxValue, false, 0, 28),
                "int" => GetProtoTypeForNumeric(false, int.MaxValue, false, 0, 28),
                "uint" => GetProtoTypeForNumeric(false, uint.MaxValue, false, 0, 28),
                "long" => GetProtoTypeForNumeric(false, long.MaxValue, false, 0, 28),
                "ulong" => GetProtoTypeForNumeric(false, ulong.MaxValue, false, 0, 28),
                _ => throw new ArgumentException($"Unsupported type: {csNumericType}"),
            };
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
                if (relativePathToGenFolder[^1] != '\\')
                    sb.Append('\\');
            }
            sb.Append(fileName);
            return sb.ToString();
        }
        // https://docs.microsoft.com/en-us/dotnet/csharp/codedoc
        private static readonly string commentBegSummary = "/// <summary>";
        private static readonly string commentEndSummary = "/// </summary>";
        private static readonly string comment = "/// ";
        public static string Comment(ITreeConfigNode t, string doc_comment, string indent = "")
        {
            if (t is IProperty p)
            {
                return Comment(p, doc_comment, indent);
            }
            else if (t is ICatalog c)
            {
                return Comment(c, doc_comment, indent);
            }
            else if (t is IDocument d)
            {
                return Comment(d, doc_comment, indent);
            }
            else if (t is IDetail dt)
            {
                return Comment(dt, doc_comment, indent);
            }
            else if (t is ICatalogFolder cf)
            {
                return Comment(cf, doc_comment, indent);
            }
            else if (t is IRegister r)
            {
                return Comment(r, doc_comment, indent);
            }
            else if (t is IRegisterDimension rd)
            {
                return Comment(rd, doc_comment, indent);
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IGroupListConstants t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IEnumeration t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IEnumerationPair t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IConstant t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(ICatalog t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(ICatalogFolder t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IDocument t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IGroupListRegisters t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IRegister t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IRegisterDimension t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IDetail t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Comments for code.
        /// Comments are created from 'doc_comment' parameter or from configuration doc description.
        /// Configuration doc description exists if node Name not equal NameUi or node Description is not empty.
        /// Configuration doc description has higher priority.
        /// </summary>
        /// <param name="t">Configuration node</param>
        /// <param name="doc_comment">Default comment for node</param>
        /// <param name="indent"></param>
        /// <returns>Comment text</returns>
        public static string Comment(IProperty t, string doc_comment, string indent = "")
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
                    sb.Append(t.NameUi);
                    sb.Append(": ");
                    if (!string.IsNullOrWhiteSpace(t.Description))
                        sb.Append(t.Description);
                    sb.AppendLine();
                }
                else if (!string.IsNullOrWhiteSpace(t.Description))
                {
                    sb.Append(indent);
                    sb.Append(comment);
                    //sb.Append("Description: ");
                    sb.Append(t.Description);
                    sb.AppendLine();
                }

                sb.Append(indent);
                sb.Append(commentEndSummary);
                //sb.AppendLine();
            }
            else if (!string.IsNullOrWhiteSpace(doc_comment))
            {
                sb.Append(indent);
                sb.Append(doc_comment);
            }
            return sb.ToString();
        }
    }
    public enum EnumVisitType { Load, Remove }
    public class TableInfo(string className, string tableName, string tableParent, ITreeConfigNode node, IReadOnlyList<IProperty> lst)
    {
        public string ClassName { get; set; } = className;
        public string TableName { get; set; } = tableName;
        public string TableParent { get; set; } = tableParent;
        public ITreeConfigNode Node { get; set; } = node;
        public IReadOnlyList<IProperty> List { get; set; } = lst;
    }
}
