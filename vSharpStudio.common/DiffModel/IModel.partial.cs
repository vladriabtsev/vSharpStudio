using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IModel : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IConfig ParentConfigI { get; }
        string PKeyTypeStr { get; }
        IReadOnlyDictionary<string, IvPluginGeneratorNodeSettings?> DicGenNodeSettings { get; }
        // numerical
        IDataType GetDataTypeFromMaxValue(ITreeConfigNode? parent, System.Numerics.BigInteger maxValue, bool isPositive, bool isPKey = false);
        // Any
        IDataType GetDataType(ITreeConfigNode? parent, int enumDataType, uint length, uint accuracy, bool isPositive, string objectGuid);
        IDataType GetDataType(ITreeConfigNode? parent, EnumDataType enumDataType, uint length, bool isPositive);
        // numerical
        IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, uint accuracy);
        // numerical
        IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, bool isPositive);
        // string
        IDataType GetDataTypeString(ITreeConfigNode? parent, uint length);
        IDataType GetDataType(ITreeConfigNode? parent, ICatalog obj);
        IDataType GetDataType(ITreeConfigNode? parent, IDocument obj);
        IDataType GetDataTypeBool(ITreeConfigNode? parent);
        IDataType GetDataTypeDate(ITreeConfigNode? parent);
        //IDataType GetDataTypeDateTime();
        //IDataType GetDataTypeDateTimeZ();
        IDataType GetDataTypeDateTimeUtc(ITreeConfigNode? parent);
        IDataType GetDataTypeTime(ITreeConfigNode? parent);
        //IDataType GetDataTypeTimeZ();
        IDataType GetIdDataType(ITreeConfigNode? parent);
        IDataType GetIdRefDataType(ITreeConfigNode? parent);
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);

        IProperty GetPropertyId(ITreeConfigNode parent, string guid);
        IProperty GetPropertyRefParent(ITreeConfigNode parent, string guid, string name, bool isNullable = false);
        IProperty GetPropertyCatalogCode(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyCatalogCodeInt(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyCatalogName(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyCatalogDescription(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyIsFolder(ITreeConfigNode parent, string guid);
        IProperty GetPropertyVersion(ITreeConfigNode parent, string guid);
        IProperty GetPropertyDocumentDate(ITreeConfigNode parent, string guid);
        IProperty GetPropertyDocumentCodeString(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyDocumentCodeInt(ITreeConfigNode parent, string guid, uint length);

        //IProperty GetPropertyBool(string guid, string name, bool isNullable);
        //IProperty GetPropertyInt(string guid, uint length, string name);
        //IProperty GetPropertyString(string guid, uint length, string name);

        //IProperty GetPropertyRefParent(ICompositeName parent);

        //string GetIdFieldName(IvPluginDbGenerator dbGen);
        //string GetIdFieldGuid();

        //IReadOnlyList<IDetail> GetListDetails(ITreeConfigNode node, string guidAppPrjGen);
        //IReadOnlyList<IProperty> GetGroupProperties(IGroupListProperties g, string guidAppPrjGen);
        //IReadOnlyList<IProperty> GetListDocSharedProperties(string guidAppPrjGen);
        //IReadOnlyList<IProperty> GetListProperties(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IEnumeration> GetListEnumerations(string guidAppPrjGen);
        IReadOnlyList<IEnumerationPair> GetListEnumerationPairs(IEnumeration node, string guidAppPrjGen);
        IReadOnlyList<IGroupListConstants> GetListConstantGroups(string guidAppPrjGen);
        IReadOnlyList<IConstant> GetListConstants(IGroupListConstants group, string guidAppPrjGen);
        IReadOnlyList<ICatalog> GetListCatalogs(string guidAppPrjGen);
        //IReadOnlyList<IDetail> GetListTabs(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IDocument> GetListDocuments(string guidAppPrjGen);
        void VisitTabs(string appGenGuig, bool isSupportVersion, EnumVisitType typeOp, ITreeConfigNode p, Action<IReadOnlyList<TableInfo>> action);
        EnumConstantAccess GetRoleConstantAccess(string roleGuid);
        EnumCatalogDetailAccess GetRoleCatalogAccess(string roleGuid);
        EnumDocumentAccess GetRoleDocumentAccess(string roleGuid);
    }
}
