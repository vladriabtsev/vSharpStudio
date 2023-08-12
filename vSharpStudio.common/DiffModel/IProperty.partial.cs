﻿using System;
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
        /// Can be nullable in code, but not nullable in DB. Samples: catalog code, document number, document date
        /// </summary>
        bool IsCsNullable { get; }

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

        #region Plugin group model
        IProperty CreatePropertyFromJson(string settings, string subName, IDataType dt);
        string ConvertToJson();
        IProperty AddExtensionPropertyRefId(string subName, string guid);
        IProperty AddExtensionPropertyGuid(string subName, string guid);
        IProperty AddExtensionPropertyString(string subName, uint length, string guid);
        IProperty AddExtensionPropertyNumerical(string subName, uint length, uint accuracy, string guid);
        #endregion Plugin group model
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
