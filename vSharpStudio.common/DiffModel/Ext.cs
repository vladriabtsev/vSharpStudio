using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public static class Ext
    {
        public static Dictionary<string, object> ToDicSql(this string param, object obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic[param] = obj;
            return dic;
        }
        public static Dictionary<string, object> ToDicSql(this Dictionary<string, object> dic, string param, object obj)
        {
            dic[param] = obj;
            return dic;
        }
        public static Type ToType(this EnumPrimaryKeyType obj)
        {
            switch(obj)
            {
                case EnumPrimaryKeyType.INT:
                    return typeof(int);
                case EnumPrimaryKeyType.LONG:
                    return typeof(long);
                default:
                    throw new NotSupportedException();
            }
        }
        public static bool IsNew(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.New.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsDeprecated(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.Deprecated.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsDeleted(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.Deleted.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsRenamed(this IMutableAnnotatable obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.Renamed.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsCanLooseData(this IConstant obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.CanLooseData.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsCanLooseData(this IEnumeration obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.CanLooseData.ToString()) == null)
                return false;
            return true;
        }
        public static bool IsCanLooseData(this IProperty obj)
        {
            if (obj.FindAnnotation(DiffEnumHistoryAnnotation.CanLooseData.ToString()) == null)
                return false;
            return true;
        }
        public static DiffEnumerationPair GetDiffEnumerationPair(this IEnumerationPair obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffEnumerationPair.ToString());
            return (DiffEnumerationPair)annotation?.Value;
        }
        public static DiffListEnumerationPairs GetDiffListEnumElements(this IEnumeration obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffListEnumerationPairs.ToString());
            return (DiffListEnumerationPairs)annotation?.Value;
        }
        public static DiffEnumerationType GetDiffEnumerationType(this IEnumeration obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffEnumerationType.ToString());
            return (DiffEnumerationType)annotation?.Value;
        }
        public static DiffDataType GetDiffDataType(this IConstant obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffPropertyDataType.ToString());
            return (DiffDataType)annotation?.Value;
        }
        public static DiffDataType GetDiffDataType(this IProperty obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffPropertyDataType.ToString());
            return (DiffDataType)annotation?.Value;
        }
        public static DiffProperty GetDiffProperty(this IProperty obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffProperty.ToString());
            return (DiffProperty)annotation?.Value;
        }
        public static DiffListProperties GetDiffListProperties(this ICatalog obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffListProperties.ToString());
            return (DiffListProperties)annotation?.Value;
        }
        public static DiffDocument GetDiffDocument(this IDocument obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffDocument.ToString());
            return (DiffDocument)annotation?.Value;
        }
        public static DiffListProperties GetDiffListProperties(this IDocument obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffListProperties.ToString());
            return (DiffListProperties)annotation?.Value;
        }
        public static DiffPropertiesTab GetDiffPropertiesTab(this IPropertiesTab obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffPropertiesTab.ToString());
            return (DiffPropertiesTab)annotation?.Value;
        }
        public static DiffListProperties GetDiffListProperties(this IPropertiesTab obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffListProperties.ToString());
            return (DiffListProperties)annotation?.Value;
        }
        public static DiffListPropertiesTabs GetDiffListPropertiesTabs(this ICatalog obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffListPropertiesTabs.ToString());
            return (DiffListPropertiesTabs)annotation?.Value;
        }
        public static DiffCatalog GetDiffCatalog(this ICatalog obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffCatalog.ToString());
            return (DiffCatalog)annotation?.Value;
        }
        public static DiffListPropertiesTabs GetDiffListPropertiesTabs(this IDocument obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffListPropertiesTabs.ToString());
            return (DiffListPropertiesTabs)annotation?.Value;
        }
        public static DiffListPropertiesTabs GetDiffListPropertiesTabs(this IPropertiesTab obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffListPropertiesTabs.ToString());
            return (DiffListPropertiesTabs)annotation?.Value;
        }
        public static DiffConfig GetDiffConfig(this IConfig obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffConfig.ToString());
            return (DiffConfig)annotation?.Value;
        }
    }
}
