using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IConstant : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName, INodeWithPositionProperties
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
        IRoleConstantsSettings GetRoleSettings(IRole role);
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
