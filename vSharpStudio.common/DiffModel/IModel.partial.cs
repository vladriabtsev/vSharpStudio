using System;
using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IModel : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IConfig ParentConfigI { get; }
        string PKeyTypeStr { get; }
        IReadOnlyDictionary<string, IvPluginGeneratorNodeSettings?> DicGenNodeSettings { get; }
        uint LastTypeShortRefIdForNode(ITreeConfigNode n, uint shortId);

        #region Properties
        string GetRecordVersionFieldType();
        IStandartPropertyGuidPosition GetGuidPosition(ITreeConfigNode node, EnumSpecialPropertyType enumDataType);
        string GetPropertyGuid(ITreeConfigNode node, EnumSpecialPropertyType enumDataType);
        string GetPropertyCodeGuid(ITreeConfigNode node);
        IProperty GetPropertyCodeStr(ITreeConfigNode node, bool isNullable, uint length);
        IProperty GetPropertyCodeInt(ITreeConfigNode node, bool isNullable, uint length);
        IProperty GetPropertyName(ITreeConfigNode node, bool isNullable, uint length);
        IProperty GetPropertyDocumentDate(ITreeConfigNode node);
        IProperty GetPropertyDocNumberString(ITreeConfigNode node, uint length);
        IProperty GetPropertyDocNumberInt(ITreeConfigNode node, uint length);
        IProperty GetPropertyDescription(ITreeConfigNode node, bool isNullable, uint length);
        IProperty GetPropertyIsFolder(ITreeConfigNode node, bool isNullable);
        IProperty GetPropertyTimelineIsPosted(ITreeConfigNode node, bool isNullable);
        IProperty GetPropertyTimelineShortTypeId(ITreeConfigNode node, bool isNullable);
        IProperty GetPropertyDocShortTypeId(ITreeConfigNode node, bool isNullable);
        IProperty GetPropertyBalanceOnDateInt(ITreeConfigNode node, bool isPKey);
        IProperty GetPropertyDateTimeUtc(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable, EnumTimeAccuracyType enumTimeAccuracyType = EnumTimeAccuracyType.MKS_TIME_ACC);
        IProperty GetPropertyVersion(ITreeConfigNode node);
        IProperty GetPropertyVersionPrev(ITreeConfigNode node);
        IProperty GetPropertyNumber(ITreeConfigNode node, EnumSpecialPropertyType enumDataType, uint length, uint accuracy, bool isNullable);
        IProperty GetPropertyRefDimension(IRegisterDimension node, bool isNullable = false);
        IProperty GetPropertyRef(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable = false, bool is_pkey = false);
        IProperty GetPropertyRefCatalog(ITreeConfigNode parent, string guid, ICatalog c, uint position, bool isNullable);
        IProperty GetPropertyRefDocument(IGroupListProperties parent, string guid, IDocument d, uint position, bool isNullable);
        IProperty GetPropertySpecial(ITreeConfigNode node, EnumSpecialPropertyType propertyType, bool? isNullable = null, ITreeConfigNode? toNode = null);
        IProperty GetPropertyComplexDescr(ITreeConfigNode node, IProperty prop, IComplexRef? complexRef, string nameSuffix, uint length);
        IProperty GetPropertyComplexGd(ITreeConfigNode node, IProperty prop, IComplexRef? complexRef, string nameSuffix);
        IProperty GetPropertyComplexRefId(ITreeConfigNode node, IProperty prop, IComplexRef? complexRef, string nameSuffix);
        IProperty GetPropertySpecial(ITreeConfigNode node, IProperty prop, IComplexRef? complexRef, EnumSpecialPropertyType propertyType, string nameSuffix, uint length = 0);

        IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, uint accuracy, bool isNullable);
        IDataType GetDataTypeFromMaxValue(ITreeConfigNode? parent, System.Numerics.BigInteger maxValue, bool isPositive, bool isNullable, bool isPKey = false);
        IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, bool isPositive, bool isNullable);
        IDataType GetDataTypeInt(ITreeConfigNode? parent, bool isPositive, bool isNullable);
        IDataType GetDataTypeString(ITreeConfigNode? parent, uint length, bool isNullable);
        IDataType GetDataTypeDateTimeUtc(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeStringFixed(ITreeConfigNode? parent, uint length, bool isNullable);
        IDataType GetDataTypeStringGuid(ITreeConfigNode? parent, bool isNullable);
        IDataType GetDataTypeBool(ITreeConfigNode? parent, bool isNullable);
        IDataType GetDataTypePkId(ITreeConfigNode parent, bool isNullable);

        #endregion Properties

        // Any
        IDataType GetDataType(ITreeConfigNode? parent, int enumDataType, uint length, uint accuracy, bool isPositive, string objectGuid, bool isNullable);
        IDataType GetDataType(ITreeConfigNode? parent, EnumDataType enumDataType, uint length, bool isPositive, bool isNullable);

        IDataType GetDataTypeCatalog(ITreeConfigNode? parent, string catGuid, bool isNullable);
        IDataType GetDataTypeDocument(ITreeConfigNode? parent, string docGuid, bool isNullable);
        IDataType GetDataTypeAny(ITreeConfigNode? parent, bool isNullable);

        IDataType GetDataType(ITreeConfigNode? parent, ICatalog obj, bool isNullable);
        IDataType GetDataType(ITreeConfigNode? parent, IDocument obj, bool isNullable);
        IDataType GetDataTypeDate(ITreeConfigNode? parent, bool isNullable);
        //IDataType GetDataTypeDateTime();
        //IDataType GetDataTypeDateTimeZ();
        IDataType GetDataTypeDateTimeLocal(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeTime(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeDateTimeZ(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);
        IDataType GetDataTypeDateTimeOffset(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable);

        //IProperty GetPropertyRefParent(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyCatalog(ITreeConfigNode parent, string guid, string name, string catGuid, uint position, bool isNullable);
        IProperty GetPropertyDocument(ITreeConfigNode parent, string guid, string name, string docGuid, uint position, bool isNullable);
        IProperty GetPropertyAny(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable);

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
