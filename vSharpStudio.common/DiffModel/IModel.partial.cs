using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        IDataType GetDataTypeInt(ITreeConfigNode? parent, bool isPositive, bool isNullable);
        // string
        IDataType GetDataTypeString(ITreeConfigNode? parent, uint length, bool isNullable);
        IDataType GetDataTypeStringGuid(ITreeConfigNode? parent, bool isNullable);

        IDataType GetDataTypeCatalog(ITreeConfigNode? parent, string catGuid, bool isNullable);
        IDataType GetDataTypeDocument(ITreeConfigNode? parent, string docGuid, bool isNullable);
        IDataType GetDataTypeAny(ITreeConfigNode? parent, bool isNullable);

        IDataType GetDataType(ITreeConfigNode? parent, ICatalog obj, bool isNullable);
        IDataType GetDataType(ITreeConfigNode? parent, IDocument obj, bool isNullable);
        IDataType GetDataTypeBool(ITreeConfigNode? parent, bool isNullable);
        IDataType GetDataTypeDate(ITreeConfigNode? parent, bool isNullable);
        //IDataType GetDataTypeDateTime();
        //IDataType GetDataTypeDateTimeZ();
        IDataType GetDataTypeDateTimeLocal(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeDateTimeUtc(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable, bool isPKey = false);
        IDataType GetDataTypeTime(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeDateTimeZ(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeDateTimeOffset(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetIdDataType(ITreeConfigNode? parent, bool isNullable);
        IDataType GetIdRefDataType(ITreeConfigNode? parent, bool isNullable);
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);

        IProperty GetPropertyGuid(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyDateTimeUtc(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable, EnumTimeAccuracyType enumTimeAccuracyType = EnumTimeAccuracyType.MKS_TIME_ACC);
        IProperty GetPropertyPkId(ITreeConfigNode parent, string guid);
        IProperty GetPropertyId(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyBool(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyNumber(ITreeConfigNode parent, string guid, string name, uint length, uint accuracy, bool isNullable);
        IProperty GetPropertyInt(ITreeConfigNode parent, string guid, string name, bool isPositive, bool isNullable);
        //IProperty GetPropertyRefParent(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyRefDimension(IRegister parent, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRefCatalog(ITreeConfigNode parent, string guid, ICatalog c, uint position, bool isNullable);
        IProperty GetPropertyCatalogCode(IGroupListProperties parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogCodeInt(IGroupListProperties parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogName(IGroupListProperties parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogDescription(IGroupListProperties parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyIsFolder(IGroupListProperties parent, string guid, bool isNullable);
        IProperty GetPropertyVersion(ITreeConfigNode parent, string guid);
        IProperty GetPropertyRefDocument(IGroupListProperties parent, string guid, IDocument d, uint position, bool isNullable);
        IProperty GetPropertyDocumentDate(IGroupListProperties parent, string guid, bool isPKey = false);
        IProperty GetPropertyDocNumberString(IGroupListProperties parent, string guid, uint length);
        IProperty GetPropertyDocNumberInt(IGroupListProperties parent, string guid, uint length);
        IProperty GetPropertyDocNumberUniqueScopeHelper(IGroupListProperties parent, string guid);

        IProperty GetPropertyCatalog(ITreeConfigNode parent, string guid, string name, string catGuid, uint position, bool isNullable);
        IProperty GetPropertyDocument(ITreeConfigNode parent, string guid, string name, string docGuid, uint position, bool isNullable);
        IProperty GetPropertyAny(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable);

        IProperty GetPropertyRef(IDetail fromObject, IDetail toObject, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRef(IDetail fromObject, ICatalog toObject, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRef(IDetail fromObject, ICatalogFolder toObject, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRef(IDetail fromObject, IDocument toObject, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRef(ICatalog fromObject, ICatalog toObject, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRef(ICatalog fromObject, ICatalogFolder toObject, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRef(ICatalogFolder fromObject, ICatalogFolder toObject, string guid, string name, uint position, bool isNullable);

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
        IReadOnlyList<IRegister> GetListRegisters(string guidAppPrjGen);
        Dictionary<string, Tuple<string, string>> GetRefTypeNames(IDataType dt);
        string GetRefTypeNamesString(IDataType dt);

        string GetUniqueStringShortID(ITreeConfigNode node);
        string GetUniquePropertyFullShortID(IProperty p);
        string GetUniquePropertyShortID(IProperty p);
        EnumRefType RefTypeForNode(ITreeConfigNode n);

        IForm CreateForm(IGroupListForms groupForms, FormType formType, List<IProperty> lst);
    }
}
