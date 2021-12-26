using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IModel : ITreeConfigNode, IGetNodeSetting
    {
        IReadOnlyDictionary<string, IvPluginGeneratorNodeSettings> DicGenNodeSettings { get; }
        // numerical
        IDataType GetDataTypeFromMaxValue(System.Numerics.BigInteger maxValue, bool isPositive);
        // Any
        IDataType GetDataType(int enumDataType, uint length, uint accuracy, bool isPositive, string objectGuid);
        IDataType GetDataType(EnumDataType enumDataType, uint length, bool isPositive);
        // numerical
        IDataType GetDataTypeNumerical(uint length, uint accuracy);
        // numerical
        IDataType GetDataTypeNumerical(uint length, bool isPositive);
        // string
        IDataType GetDataTypeString(uint length);
        IDataType GetDataType(ICatalog obj);
        IDataType GetDataType(IDocument obj);
        IDataType GetDataTypeBool();
        IDataType GetDataTypeDate();
        //IDataType GetDataTypeDateTime();
        //IDataType GetDataTypeDateTimeZ();
        IDataType GetDataTypeDateTimeUtc();
        IDataType GetDataTypeTime();
        //IDataType GetDataTypeTimeZ();
        IDataType GetIdDataType();
        IDataType GetIdRefDataType();
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);

        IProperty GetPropertyId(string guid);
        IProperty GetPropertyRefParent(string guid, string name, bool isNullable = false);
        IProperty GetPropertyCatalogCode(string guid, uint length);
        IProperty GetPropertyCatalogCodeInt(string guid, uint length);
        IProperty GetPropertyCatalogName(string guid, uint length);
        IProperty GetPropertyCatalogDescription(string guid, uint length);
        IProperty GetPropertyIsFolder(string guid);
        IProperty GetPropertyIsOpen(string guid);
        IProperty GetPropertyDocumentDate(string guid);
        IProperty GetPropertyDocumentCodeString(string guid, uint length);
        IProperty GetPropertyDocumentCodeInt(string guid, uint length);

        //IProperty GetPropertyBool(string guid, string name, bool isNullable);
        //IProperty GetPropertyInt(string guid, uint length, string name);
        //IProperty GetPropertyString(string guid, uint length, string name);

        //IProperty GetPropertyRefParent(ICompositeName parent);

        //string GetIdFieldName(IvPluginDbGenerator dbGen);
        //string GetIdFieldGuid();

        //IReadOnlyList<IPropertiesTab> GetListPropertiesTabs(ITreeConfigNode node, string guidAppPrjGen);
        //IReadOnlyList<IProperty> GetGroupProperties(IGroupListProperties g, string guidAppPrjGen);
        //IReadOnlyList<IProperty> GetListDocSharedProperties(string guidAppPrjGen);
        //IReadOnlyList<IProperty> GetListProperties(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IEnumeration> GetListEnumerations(string guidAppPrjGen);
        IReadOnlyList<IEnumerationPair> GetListEnumerationPairs(IEnumeration node, string guidAppPrjGen);
        IReadOnlyList<IGroupListConstants> GetListConstantGroups(string guidAppPrjGen);
        IReadOnlyList<IConstant> GetListConstants(IGroupListConstants group, string guidAppPrjGen);
        IReadOnlyList<ICatalog> GetListCatalogs(string guidAppPrjGen);
        //IReadOnlyList<IPropertiesTab> GetListTabs(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IDocument> GetListDocuments(string guidAppPrjGen);
        void VisitTabs(string appGenGuig, EnumVisitType typeOp, ITreeConfigNode p, Action<IReadOnlyList<TableInfo>> action);
    }
}
