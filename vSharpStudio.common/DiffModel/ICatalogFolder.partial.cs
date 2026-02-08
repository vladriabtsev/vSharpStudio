using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface ICatalogFolder : ITreeConfigNodeSortable, IGetNodeSetting, 
        ICompositeName, IPKey, INodeWithStandartProperties
    {
        ICatalog ParentCatalogI { get; }
        string GetDebuggerDisplay(bool isOptimistic);

        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();

        bool GetUseCodeProperty();
        bool GetUseNameProperty();
        bool GetUseDescriptionProperty();
        //string GetDebuggerDisplay(bool isOptimistic);
        IProperty? GetCodeProperty(List<IProperty> lst);
        IProperty? GetCodeProperty();
        IProperty GetNameProperty(List<IProperty> lst);
        IProperty? GetDescriptionProperty(List<IProperty> lst);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
    }
}
