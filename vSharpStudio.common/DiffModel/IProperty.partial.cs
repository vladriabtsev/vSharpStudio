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
        bool IsComputed { get; set; }
        bool IsPKey { get; }
        bool IsDocShared { get; set; }
        bool IsRecordVersion { get; }

        /// <summary>
        /// Is hidden on UI (special properties)
        /// </summary>
        bool IsHidden { get; set; }
        string? ComplexObjectName { get; set; }
        string PropValueValue { get; }
        IPropertyRangeValuesRequirements? RangeValuesRequirementsI { get; }
        string ComplexObjectNameWithDot();
        object? Tag { get; set; }
        //static IConfig Config { get; set; }
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        IReadOnlyList<string> GetRolesByAccess(EnumPropertyAccess access);
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
