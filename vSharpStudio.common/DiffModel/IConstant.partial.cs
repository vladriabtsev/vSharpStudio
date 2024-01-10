using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IConstant : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IGroupListConstants ParentGroupListConstantsI { get; }
        //string DefaultValue { get; }
        /// <summary>
        /// Mark property as computed if it will be not stored in DB
        /// </summary>
        bool IsComputed { get; set; }
        bool IsComplexRefId { get; }
        bool IsComplexRefGuid { get; }
        bool IsComplexDesc { get; }
        object? Tag { get; set; }
        //static IConfig Config { get; set; }
        EnumConstantAccess GetRoleConstantAccess(IRole role);
        EnumPrintAccess GetRoleConstantPrint(IRole role);
        IReadOnlyList<string> GetRolesByAccess(EnumConstantAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        string FullName { get; } // name with config name
        string? ComplexObjectName { get; set; }
        string PropValueValue { get; }
        IPropertyRangeValuesRequirements? RangeValuesRequirementsI { get; }
        string ComplexObjectNameWithDot();

        #region Plugin group model
        IConstant CreateConstantFromJson(string settings, string subName, IDataType dt);
        string ConvertToJson();
        IConstant? ParentConstant { get; set; }
        string NameWithExtention { get; }
        IConstant AddExtensionConstantRefId(string subName, string guid);
        IConstant AddExtensionConstantGd(string subName, string guid);
        IConstant AddExtensionConstantDesc(string subName, string guid);
        #endregion Plugin group model
    }
}
