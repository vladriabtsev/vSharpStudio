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
    }
}
