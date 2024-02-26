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
        IGroupListProperties ParentGroupListPropertiesI { get; }
        //string DefaultValue { get; }
        /// <summary>
        /// Mark property as computed if it will be not stored in DB
        /// </summary>
        bool IsComplex { get; }
        bool IsComputed { get; set; }
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
        IProperty AddExtensionPropertyRefId(string subName, string guid, bool isNullable, bool isCsNullable, int positionInObject, string foreignObjectGuid);
        IProperty AddExtensionPropertyGd(string subName, string guid, bool isNullable, bool isCsNullable);
        IProperty AddExtensionPropertyDesc(string subName, string guid, bool isNullable, bool isCsNullable);
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
