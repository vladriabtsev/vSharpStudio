using System.Collections.Generic;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface ICatalog : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue, 
        ICompositeName, IPKey, INodeWithStandartProperties
    {
        string GetDebuggerDisplay(bool isOptimistic);
        IGroupListCatalogs ParentGroupListCatalogsI { get; }
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();

        bool GetUseCodeProperty();
        bool GetUseNameProperty();
        bool GetUseDescriptionProperty();
        IRoleCatalogsSettings GetRoleSettings(IRole role);
        string FullName { get; } // name with config name
        //string GetDebuggerDisplay(bool isOptimistic);
        IProperty? GetCodeProperty(List<IProperty> lst);
        IProperty? GetCodeProperty();
        IProperty GetNameProperty(List<IProperty> lst);
        IProperty? GetDescriptionProperty(List<IProperty> lst);

        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjDbGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
    }
}
