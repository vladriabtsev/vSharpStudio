using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IDetail : ITreeConfigNodeSortable, IGetNodeSetting, 
        ICompositeName, IPKey, INodeWithStandartProperties
    {
        string GetDebuggerDisplay(bool isOptimistic);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        IGroupListDetails ParentGroupListDetailsI { get; }

        IRoleDetailsSettings GetRoleSettings(IRole role);
        string FullName { get; } // name with config name
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);

        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
    }
}
