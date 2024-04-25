using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IProperty : IParent, ITreeConfigNodeSortable, IGetNodeSetting
    {
        #region Standard Property Positions
        #region DB record
        static uint PropertyIdPosition { get; } = 5;
        static uint PropertyVersionPosition { get; } = 6;
        static uint PropertyShortTypeIdPosition { get; } = 7;
        #endregion DB record

        #region Catalog or Folder
        static uint PropertyCodePosition { get; } = 8;
        static uint PropertyNamePosition { get; } = 9;
        static uint PropertyDescriptionPosition { get; } = 10;
        static uint PropertyIsFolderPosition { get; } = 11;
        static uint PropertyIsOpenPosition { get; } = 12;
        static uint PropertyRefSelfParentPosition { get; } = 13;
        #endregion Catalog or Folder

        #region Document
        static uint PropertyDocumentDatePosition { get; } = 8;
        static uint PropertyDocumentNumberPosition { get; } = 9;
        static uint PropertyIsPostedPosition { get; } = 11;
        #endregion Document

        #region Register
        static uint PropertyMoneyAccumulatorPosition { get; } = 8;
        static uint PropertyQtyAccumulatorPosition { get; } = 9;
        #endregion Register

        #region Detail, or Catalog, or Document, or Register
        static uint PropertyRefParentPosition { get; } = 14;
        #endregion Detail, or Catalog, or Document, or Register

        // reserved positions: 1-4
        
        // reserved positions: 5-20 special properties

        // configured properties: starting from 21

        #endregion Standard Property Positions

        IGroupListProperties ParentGroupListPropertiesI { get; }
        //string DefaultValue { get; }
        /// <summary>
        /// Mark property as computed if it will be not stored in DB
        /// </summary>
        bool IsComplex { get; }
        bool IsPKey { get; }
        bool IsDocShared { get; set; }
        bool IsRecordVersion { get; }
        bool IsComplexRefId { get; }
        bool IsComplexRefGuid { get; }
        bool IsComplexDesc { get; }

        /// <summary>
        /// Can be nullable in code, but not nullable in DB. Samples: catalog code, document number, document date
        /// </summary>
        bool IsCsNullable { get; set; }
        bool IsNullable { get; }
        bool IsReadonly { get; }

        /// <summary>
        /// Is hidden on UI (special properties)
        /// </summary>
        bool IsHidden { get; set; }
        string? ComplexObjectName { get; set; }
        string RefIdName();
        string PropValueValue { get; }
        IPropertyRangeValuesRequirements? RangeValuesRequirementsI { get; }
        string ComplexObjectNameWithDot();
        object? Tag { get; set; }
        string? TagInList { get; set; }
        int PositionInConfigObject { get; set; }
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
        IProperty AddExtensionPropertyRefId(string subName, IComplexRef complexRef, bool isNullable, bool isCsNullable, int positionInConfigObject, uint position);
        IProperty AddExtensionPropertyGd(string subName, bool isNullable, bool isCsNullable, uint position);
        IProperty AddExtensionPropertyDesc(string subName, bool isNullable, bool isCsNullable, uint position);
        IProperty AddExtensionPropertyString(string subName, uint length, string guid);
        IProperty AddExtensionPropertyNumerical(string subName, uint length, uint accuracy, string guid);
        #endregion Plugin group model

        string GetShortDescription(StringBuilder sb);
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
