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
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);
        //string GetVersionFieldGuid();
        string GetIdFieldName(IvPluginDbGenerator dbGen);
        string GetIdFieldGuid();
        IProperty GetRefParentProperty(IvPluginDbGenerator dbGen, ICompositeName parent);
        IReadOnlyList<IProperty> GetListDocSharedProperties(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetListProperties(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IEnumeration> GetListEnumerations(string guidAppPrjGen);
        IReadOnlyList<IEnumerationPair> GetListEnumerationPairs(IEnumeration node, string guidAppPrjGen);
        IReadOnlyList<IConstant> GetListConstants(string guidAppPrjGen);
        IReadOnlyList<ICatalog> GetListCatalogs(string guidAppPrjGen);
        IReadOnlyList<IPropertiesTab> GetListTabs(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IDocument> GetListDocuments(string guidAppPrjGen);
    }
}
