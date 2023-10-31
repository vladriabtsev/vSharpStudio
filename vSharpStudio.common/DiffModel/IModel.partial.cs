﻿using System;
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

        IDataType GetDataTypeCatalog(ITreeConfigNode? parent, string catGuid, bool isNullable);
        IDataType GetDataTypeCatalogs(ITreeConfigNode? parent, IEnumerable<string> lstCatGuids, bool isNullable);
        IDataType GetDataTypeDocument(ITreeConfigNode? parent, string docGuid, bool isNullable);
        IDataType GetDataTypeDocuments(ITreeConfigNode? parent, IEnumerable<string> lstDocGuids, bool isNullable);
        IDataType GetDataTypeCatalogsDocuments(ITreeConfigNode? parent, IEnumerable<string> lstCatDocGuids, bool isNullable);
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
        //IDataType GetDataTypeTimeZ();
        IDataType GetIdDataType(ITreeConfigNode? parent, bool isNullable);
        IDataType GetIdRefDataType(ITreeConfigNode? parent, bool isNullable);
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);

        IProperty GetPropertyGuid(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyDateTimeUtc(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable, EnumTimeAccuracyType enumTimeAccuracyType = EnumTimeAccuracyType.MAX);
        IProperty GetPropertyPkId(ITreeConfigNode parent, string guid);
        IProperty GetPropertyId(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyBool(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyNumber(ITreeConfigNode parent, string guid, string name, uint length, uint accuracy, bool isNullable);
        IProperty GetPropertyRefParent(ITreeConfigNode parent, string guid, string name, bool isNullable);
        IProperty GetPropertyRefDimension(IRegister parent, string guid, string name, uint position, bool isNullable);
        IProperty GetPropertyRefCatalog(ITreeConfigNode parent, string guid, ICatalog c, uint position, bool isNullable);
        IProperty GetPropertyCatalogCode(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogCodeInt(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogName(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyCatalogDescription(ITreeConfigNode parent, string guid, uint length, bool isNullable);
        IProperty GetPropertyIsFolder(ITreeConfigNode parent, string guid, bool isNullable);
        IProperty GetPropertyVersion(ITreeConfigNode parent, string guid);
        IProperty GetPropertyRefDocument(ITreeConfigNode parent, string guid, IDocument d, uint position, bool isNullable);
        IProperty GetPropertyDocumentDate(ITreeConfigNode parent, string guid, bool isPKey = false);
        IProperty GetPropertyDocNumberString(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyDocNumberInt(ITreeConfigNode parent, string guid, uint length);
        IProperty GetPropertyDocNumberUniqueScopeHelper(ITreeConfigNode parent, string guid);

        IProperty GetPropertyCatalog(ITreeConfigNode parent, string guid, string name, string catGuid, uint position, bool isNullable);
        IProperty GetPropertyCatalogs(ITreeConfigNode parent, string guid, string name, IEnumerable<string> lstCatGuids, uint position, bool isNullable);
        IProperty GetPropertyDocument(ITreeConfigNode parent, string guid, string name, string docGuid, uint position, bool isNullable);
        IProperty GetPropertyDocuments(ITreeConfigNode parent, string guid, string name, IEnumerable<string> lstDocGuids, uint position, bool isNullable);
        IProperty GetPropertyCatalogsDocuments(ITreeConfigNode parent, string guid, string name, IEnumerable<string> lstCatOrDocGuids, uint position, bool isNullable);
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
        void VisitTabs(string appGenGuig, bool isOptimistic, EnumVisitType typeOp, ITreeConfigNode p, Action<IReadOnlyList<TableInfo>> action);
        //EnumConstantAccess GetRoleConstantAccess(string roleGuid);
        //EnumCatalogDetailAccess GetRoleCatalogAccess(string roleGuid);
        //EnumDocumentAccess GetRoleDocumentAccess(string roleGuid);
        Dictionary<string, Tuple<string, string>> GetRefTypeNames(IDataType dt);
        string GetRefTypeNamesString(IDataType dt);
    }
}
