using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //public static Type ToType(this EnumPrimaryKeyType obj)
        //{
        //    switch (obj)
        //    {
        //        case EnumPrimaryKeyType.INT:
        //            return typeof(int);
        //        case EnumPrimaryKeyType.LONG:
        //            return typeof(long);
        //        default:
        //            throw new NotSupportedException();
        //    }
        //}

        #region IConfig
        // get Constant from Config for Guid from provided constant
        public static IEnumerable<IEnumeration> GetEnumerations(this IConfig obj)
        {
            return obj.Model.GroupEnumerations.ListEnumerations;
        }

        public static IEnumerable<IConstant> GetConstants(this IConfig obj)
        {
            return obj.Model.GroupConstants.ListConstants;
        }

        public static IEnumerable<ICatalog> GetCatalogs(this IConfig obj)
        {
            return obj.Model.GroupCatalogs.ListCatalogs;
        }

        public static IEnumerable<IDocument> GetDocuments(this IConfig obj)
        {
            return obj.Model.GroupDocuments.GroupListDocuments.ListDocuments;
        }

        public static IEnumerable<IProperty> GetDocShared(this IConfig obj)
        {
            return obj.Model.GroupDocuments.GroupSharedProperties.ListProperties;
        }

        public static IEnumerable<IJournal> GetJournals(this IConfig obj)
        {
            return obj.Model.GroupJournals.ListJournals;
        }

        #endregion IConfig

        #region IEnumeration

        public static IEnumerable<IEnumerationPair> GetEnumerationPairs(this IEnumeration obj)
        {
            return obj.ListEnumerationPairs;
        }

        #endregion IEnumeration

        #region IPropertiesTab
        public static IEnumerable<IProperty> GetProperties(this IPropertiesTab obj)
        {
            return obj.GroupProperties.ListProperties;
        }

        public static IEnumerable<IPropertiesTab> GetPropertiesTabs(this IPropertiesTab obj)
        {
            return obj.GroupPropertiesTabs.ListPropertiesTabs;
        }

        #endregion IPropertiesTab

        #region ICatalog
        public static IEnumerable<IProperty> GetProperties(this ICatalog obj)
        {
            return obj.GroupProperties.ListProperties;
        }

        public static IEnumerable<IPropertiesTab> GetPropertiesTabs(this ICatalog obj)
        {
            return obj.GroupPropertiesTabs.ListPropertiesTabs;
        }

        public static IEnumerable<IForm> GetForms(this ICatalog obj)
        {
            return obj.GroupForms.ListForms;
        }

        public static IEnumerable<IReport> GetReports(this ICatalog obj)
        {
            return obj.GroupReports.ListReports;
        }

        #endregion ICatalog

        #region IDocument
        public static IEnumerable<IProperty> GetProperties(this IDocument obj)
        {
            return obj.GroupProperties.ListProperties;
        }

        public static IEnumerable<IPropertiesTab> GetPropertiesTabs(this IDocument obj)
        {
            return obj.GroupPropertiesTabs.ListPropertiesTabs;
        }

        public static IEnumerable<IForm> GetForms(this IDocument obj)
        {
            return obj.GroupForms.ListForms;
        }

        public static IEnumerable<IReport> GetReports(this IDocument obj)
        {
            return obj.GroupReports.ListReports;
        }
        #endregion IDocument
    }
}
