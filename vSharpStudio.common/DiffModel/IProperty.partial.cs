using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IProperty : IParent, ITreeConfigNodeSortable, IGetNodeSetting
    {
        //#region Standard Property Positions
        //#region DB record
        //static uint PropertyIdPosition { get; } = 5;
        //static uint PropertyVersionPosition { get; } = 6;
        //static uint PropertyVersionPrevPosition { get; } = 7;
        //static uint PropertyShortTypeIdPosition { get; } = 8;
        //#endregion DB record

        //#region Catalog or Folder
        //static uint PropertyCodePosition { get; } = 9;
        //static uint PropertyNamePosition { get; } = 10;
        //static uint PropertyDescriptionPosition { get; } = 11;
        //static uint PropertyIsFolderPosition { get; } = 12;
        //static uint PropertyIsOpenPosition { get; } = 13;
        //static uint PropertyRefSelfParentPosition { get; } = 14;
        //#endregion Catalog or Folder

        //#region Document
        //static uint PropertyDocumentNumberPosition { get; } = 9;
        //static uint PropertyDocumentDatePosition { get; } = 10;
        //static uint PropertyIsPostedPosition { get; } = 11;
        //#endregion Document

        //#region Register
        //static uint PropertyMoneyAccumulatorPosition { get; } = 9;
        //static uint PropertyQtyAccumulatorPosition { get; } = 10;
        //#endregion Register

        //#region Detail, or Catalog, or Document, or Register
        //static uint PropertyRefParentPosition { get; } = 14;
        //#endregion Detail, or Catalog, or Document, or Register

        //// reserved positions: 1-4

        //// reserved positions: 5-20 special properties

        //static uint PropertyStartingPosition { get; } = 21; // configured properties: starting from 21
        ////static uint PropertyPositionGapFoComplex { get; } = 3; // configured properties: starting from 21

        //#endregion Standard Property Positions

        IGroupListProperties ParentGroupListPropertiesI { get; }
        //string DefaultValue { get; }
        /// <summary>
        /// Mark property as computed if it will be not stored in DB
        /// </summary>
        bool IsComplex { get; }
        bool IsPKey { get; set; }
        bool IsSimple { get; set; }
        bool IsViewDefault { get; set; }
        bool IsDocShared { get; set; }
        bool IsRecordVersion { get; }
        bool IsRefTimeline { get; }
        bool IsComplexRefId { get; }
        bool IsComplexRefGuid { get; }
        bool IsComplexDesc { get; }

        /// <summary>
        /// Can be nullable in code, but not nullable in DB. Samples: catalog code, document number, document date
        /// </summary>
        bool IsCsNullable { get; set; }
        bool IsNullable { get; }
        bool IsReadonly { get; }
        bool IsUnicode { get; }

        /// <summary>
        /// Is hidden on UI (special properties)
        /// </summary>
        bool IsHidden { get; set; }
        string? ComplexObjectName { get; set; }
        string PropValueValue { get; }
        IPropertyRangeValuesRequirements? RangeValuesRequirementsI { get; }
        string ComplexObjectNameWithDot();
        object? Tag { get; set; }
        string? TagInList { get; set; }
        void SetPosition(uint position);
        //static IConfig Config { get; set; }
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        IReadOnlyList<string> GetRolesByAccess(EnumPropertyAccess access);

        #region Plugin group model
        IProperty CreatePropertyFromJson(string settings, string subName, IDataType dt);
        string ConvertToJson();
        IProperty? ParentProperty { get; set; }
        string NameWithExtention { get; }
        //List<IProperty> ListExtensionProperties { get; }
        IProperty AddExtensionPropertyRefId(string subName, IProperty t, IComplexRef tt);
        IProperty AddExtensionPropertyGd(string subName, bool isNullable, bool isCsNullable, uint position);
        IProperty AddExtensionPropertyDesc(string subName, bool isNullable, bool isCsNullable, uint position);
        IProperty AddExtensionPropertyString(string subName, uint length, string guid);
        IProperty AddExtensionPropertyNumerical(string subName, uint length, uint accuracy, string guid);
        #endregion Plugin group model

        string GetShortDescription(StringBuilder sb);

        static virtual IStandartPropertyGuidPosition GetGuidPosition(ITreeConfigNode node, EnumSpecialPropertyType enumDataType) { throw new NotImplementedException(); }
        static virtual string GetPropertyGuid(ITreeConfigNode node, EnumSpecialPropertyType enumDataType) { throw new NotImplementedException(); }
        static string SpecialRefParentName { get; } = "Not implemented";
        static string SpecialRefTreeParentName { get; } = "Not implemented";
        static string SpecialPropertyNameRefTimeline { get; } = "Not implemented";
        static string SpecialPropertyHistoryDataTimeUtc { get; } = "Not implemented";
        static string GetPropertyCodeGuid(ITreeConfigNode node) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyCodeStr(ITreeConfigNode node, bool isNullable, uint length) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyCodeInt(ITreeConfigNode node, bool isNullable, uint length) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyName(ITreeConfigNode node, bool isNullable, uint length) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyDocumentDate(ITreeConfigNode node) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyDocNumberString(ITreeConfigNode node, uint length) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyDocNumberInt(ITreeConfigNode node, uint length) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyDescription(ITreeConfigNode node, bool isNullable, uint length) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyIsFolder(ITreeConfigNode node, bool isNullable) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyIsPosted(ITreeConfigNode node, bool isNullable) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyDocShortTypeId(ITreeConfigNode node, bool isNullable) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyBalanceOnDateInt(ITreeConfigNode node, bool isPKey) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyDateTimeUtc(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable, EnumTimeAccuracyType enumTimeAccuracyType = EnumTimeAccuracyType.MKS_TIME_ACC) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyVersion(ITreeConfigNode node) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyVersionPrev(ITreeConfigNode node) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyNumber(ITreeConfigNode node, EnumSpecialPropertyType enumDataType, uint length, uint accuracy, bool isNullable) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyRefDimension(IRegisterDimension node, bool isNullable = false) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertyRef(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable = false, bool is_pkey = false) { throw new NotImplementedException(); }
        static virtual IProperty GetPropertySpecial(ITreeConfigNode node, EnumSpecialPropertyType propertyType, bool? isNullable = null, ITreeConfigNode? toNode = null) { throw new NotImplementedException(); }

        static virtual IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, uint accuracy, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeFromMaxValue(ITreeConfigNode? parent, System.Numerics.BigInteger maxValue, bool isPositive, bool isNullable, bool isPKey = false) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, bool isPositive, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeInt(ITreeConfigNode? parent, bool isPositive, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeString(ITreeConfigNode? parent, uint length, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeDateTimeUtc(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeStringFixed(ITreeConfigNode? parent, uint length, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeStringGuid(ITreeConfigNode? parent, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetDataTypeBool(ITreeConfigNode? parent, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetIdDataType(ITreeConfigNode parent, bool isNullable) { throw new NotImplementedException(); }
        static virtual IDataType GetIdRefDataType(ITreeConfigNode? parent, bool isNullable) { throw new NotImplementedException(); }
    }
    public interface IPropertyRangeValuesRequirements
    {
        bool IsHasRequirements { get; }
        IReadOnlyList<IValidationBoundary> ListBoundaries { get; }
        IReadOnlyList<string> ListValues { get; }
        bool IsHasErrors { get; }
        IReadOnlyList<string> ListErrors { get; }
    }
    public interface IValidationBoundary
    {
        public string? BoundaryMin { get; }
        public string? BoundaryMax { get; }
    }
}
