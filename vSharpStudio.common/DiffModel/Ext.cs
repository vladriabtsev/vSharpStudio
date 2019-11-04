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
        public static DiffListConstants GetDiffListConstants(this IConfig obj)
        {
            Annotation annotation = obj.FindAnnotation(DiffEnumHistoryAnnotation.DiffConfig.ToString());
            return (DiffListConstants)annotation?.Value;
        }



        #region IConfig
        public static IEnumerable<IEnumeration> GetEnumerations(this IConfig obj)
        {
            return obj.IModel.IGroupEnumerations.IListEnumerations;
        }
        public static List<IEnumeration> GetDiffEnumerations(this IConfig obj)
        {
            return obj.IModel.IGroupEnumerations.ListAnnotated;
        }
        public static IEnumerable<IConstant> GetConstants(this IConfig obj)
        {
            return obj.IModel.IGroupConstants.IListConstants;
        }
        public static List<IConstant> GetDiffConstants(this IConfig obj)
        {
            return obj.IModel.IGroupConstants.ListAnnotated;
        }
        public static IEnumerable<ICatalog> GetCatalogs(this IConfig obj)
        {
            return obj.IModel.IGroupCatalogs.IListCatalogs;
        }
        public static List<ICatalog> GetDiffCatalogs(this IConfig obj)
        {
            return obj.IModel.IGroupCatalogs.ListAnnotated;
        }
        public static IEnumerable<IDocument> GetDocuments(this IConfig obj)
        {
            return obj.IModel.IGroupDocuments.IGroupListDocuments.IListDocuments;
        }
        public static List<IDocument> GetDiffDocuments(this IConfig obj)
        {
            return obj.IModel.IGroupDocuments.IGroupListDocuments.ListAnnotated;
        }
        public static IEnumerable<IProperty> GetDocShared(this IConfig obj)
        {
            return obj.IModel.IGroupDocuments.IGroupSharedProperties.IListProperties;
        }
        public static List<IProperty> GetDiffDocShared(this IConfig obj)
        {
            return obj.IModel.IGroupDocuments.IGroupSharedProperties.ListAnnotated;
        }
        public static IEnumerable<IJournal> GetJournals(this IConfig obj)
        {
            return obj.IModel.IGroupJournals.IListJournals;
        }
        public static List<IJournal> GetDiffJournals(this IConfig obj)
        {
            return obj.IModel.IGroupJournals.ListAnnotated;
        }
        #endregion IConfig

        #region IEnumeration
        public static IEnumerable<IEnumerationPair> GetEnumerationPairs(this IEnumeration obj)
        {
            return obj.IListEnumerationPairs;
        }
        public static List<IEnumerationPair> GetDiffEnumerationPairs(this IEnumeration obj)
        {
            return obj.ListAnnotated;
        }
        #endregion IEnumeration

        #region IPropertiesTab
        public static IEnumerable<IProperty> GetProperties(this IPropertiesTab obj)
        {
            return obj.IGroupProperties.IListProperties;
        }
        public static List<IProperty> GetDiffProperties(this IPropertiesTab obj)
        {
            return obj.IGroupProperties.ListAnnotated;
        }
        #endregion IPropertiesTab

        #region ICatalog
        public static IEnumerable<IProperty> GetProperties(this ICatalog obj)
        {
            return obj.IGroupProperties.IListProperties;
        }
        public static List<IProperty> GetDiffProperties(this ICatalog obj)
        {
            return obj.IGroupProperties.ListAnnotated;
        }
        public static IEnumerable<IPropertiesTab> GetPropertiesTabs(this ICatalog obj)
        {
            return obj.IGroupPropertiesTabs.IListPropertiesTabs;
        }
        public static List<IPropertiesTab> GetDiffPropertiesTabs(this ICatalog obj)
        {
            return obj.IGroupPropertiesTabs.ListAnnotated;
        }
        public static IEnumerable<IForm> GetForms(this ICatalog obj)
        {
            return obj.IGroupForms.IListForms;
        }
        public static List<IForm> GetDiffForms(this ICatalog obj)
        {
            return obj.IGroupForms.ListAnnotated;
        }
        public static IEnumerable<IReport> GetReports(this ICatalog obj)
        {
            return obj.IGroupReports.IListReports;
        }
        public static List<IReport> GetDiffReports(this ICatalog obj)
        {
            return obj.IGroupReports.ListAnnotated;
        }
        #endregion ICatalog

        #region IDocument
        public static IEnumerable<IProperty> GetProperties(this IDocument obj)
        {
            return obj.IGroupProperties.IListProperties;
        }
        public static List<IProperty> GetDiffProperties(this IDocument obj)
        {
            return obj.IGroupProperties.ListAnnotated;
        }
        public static IEnumerable<IPropertiesTab> GetPropertiesTabs(this IDocument obj)
        {
            return obj.IGroupPropertiesTabs.IListPropertiesTabs;
        }
        public static List<IPropertiesTab> GetDiffPropertiesTabs(this IDocument obj)
        {
            return obj.IGroupPropertiesTabs.ListAnnotated;
        }
        public static IEnumerable<IForm> GetForms(this IDocument obj)
        {
            return obj.IGroupForms.IListForms;
        }
        public static List<IForm> GetDiffForms(this IDocument obj)
        {
            return obj.IGroupForms.ListAnnotated;
        }
        public static IEnumerable<IReport> GetReports(this IDocument obj)
        {
            return obj.IGroupReports.IListReports;
        }
        public static List<IReport> GetDiffReports(this IDocument obj)
        {
            return obj.IGroupReports.ListAnnotated;
        }
        #endregion IDocument
    }
}
