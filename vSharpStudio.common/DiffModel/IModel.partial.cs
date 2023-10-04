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
        IDataType GetDataTypeFromMaxValue(ITreeConfigNode? parent, System.Numerics.BigInteger maxValue, bool isPositive, bool isNullable, bool isPKey = false);
        // Any
        IDataType GetDataType(ITreeConfigNode? parent, int enumDataType, uint length, uint accuracy, bool isPositive, string objectGuid, bool isNullable);
        IDataType GetDataType(ITreeConfigNode? parent, EnumDataType enumDataType, uint length, bool isPositive, bool isNullable);
        // numerical
        IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, uint accuracy, bool isNullable);
        // numerical
        IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, bool isPositive, bool isNullable);
        // string
        IDataType GetDataTypeString(ITreeConfigNode? parent, uint length, bool isNullable);
        IDataType GetDataTypeStringGuid(ITreeConfigNode? parent, bool isNullable);
        IDataType GetDataTypeAny(ITreeConfigNode? parent, bool isNullable);
        IDataType GetDataType(ITreeConfigNode? parent, ICatalog obj, bool isNullable);
        IDataType GetDataType(ITreeConfigNode? parent, IDocument obj, bool isNullable);
        IDataType GetDataTypeBool(ITreeConfigNode? parent, bool isNullable);
        IDataType GetDataTypeDate(ITreeConfigNode? parent, bool isNullable);
        //IDataType GetDataTypeDateTime();
        //IDataType GetDataTypeDateTimeZ();
        IDataType GetDataTypeDateTimeLocal(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeDateTimeUtc(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeTime(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        //IDataType GetDataTypeTimeZ();
        IDataType GetIdDataType(ITreeConfigNode? parent, bool isNullable);
        IDataType GetIdRefDataType(ITreeConfigNode? parent, bool isNullable);
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);

        IProperty GetPropertyGuid(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyDate(ITreeConfigNode parent, string guid, string name, bool isNullable, EnumTimeAccuracyType enumTimeAccuracyType = EnumTimeAccuracyType.MAX);
        IProperty GetPropertyPkId(ITreeConfigNode parent, string guid);
        IProperty GetPropertyId(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyRefParent(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyRefDimention(IRegister parent, string guid, string name, bool isNullable);
        IProperty GetPropertyCatalogCode(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogCodeInt(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogName(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogDescription(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyIsFolder(ITreeConfigNode parent, string guid, bool isNullable);
        IProperty GetPropertyVersion(ITreeConfigNode parent, string guid);
        IProperty GetPropertyDocumentDate(ITreeConfigNode parent, string guid);
        IProperty GetPropertyDocNumberString(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyDocNumberInt(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyDocNumberUniqueScopeHelper(ITreeConfigNode parent, string guid);

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
        void VisitTabs(string appGenGuig, bool isOptimistic, EnumVisitType typeOp, ITreeConfigNode p, Action<IReadOnlyList<TableInfo>> action);
        //EnumConstantAccess GetRoleConstantAccess(string roleGuid);
        //EnumCatalogDetailAccess GetRoleCatalogAccess(string roleGuid);
        //EnumDocumentAccess GetRoleDocumentAccess(string roleGuid);
        Dictionary<string, Tuple<string, string>> GetRefTypeNames(IDataType dt);
        string GetRefTypeNamesString(IDataType dt);
    }
}
