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
        IDataType GetDataTypeFromMaxValue(System.Numerics.BigInteger maxValue, bool isPositive, bool isNullable = true);
        // Any
        IDataType GetDataType(int enumDataType, uint length, bool isPositive, bool isNullable);
        IDataType GetDataType(EnumDataType enumDataType, uint length, bool isPositive, bool isNullable);
        // numerical
        IDataType GetDataType(uint length, uint accuracy, bool isNullable = true);
        // numerical
        IDataType GetDataType(uint length, bool isPositive, bool isNullable = true);
        // string
        IDataType GetDataType(uint length, bool isNullable = true);
        IDataType GetDataType(ICatalog obj, bool isNullable = true);
        IDataType GetDataType(IDocument obj, bool isNullable = true);
        IDataType GetDataTypeBool(bool isNullable = true);
        IDataType GetDataTypeDate(bool isNullable = true);
        IDataType GetDataTypeDateTime(bool isNullable = true);
        IDataType GetDataTypeDateTimeZ(bool isNullable = true);
        IDataType GetDataTypeTime(bool isNullable = true);
        IDataType GetDataTypeTimeZ(bool isNullable = true);
        IDataType GetIdDataType();
        IDataType GetIdRefDataType();
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);

        IProperty GetPropertyId(string guid);
        IProperty GetPropertyBool(string guid, string name, bool isNullable);
        IProperty GetPropertyInt(string guid, uint length, string name);
        IProperty GetPropertyString(string guid, uint length, string name);
        IProperty GetPropertyRefParent(string guid, string name);
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
    }
}
