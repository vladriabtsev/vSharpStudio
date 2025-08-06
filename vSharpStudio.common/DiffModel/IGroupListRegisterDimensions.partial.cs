namespace vSharpStudio.common
{
    public partial interface IGroupListRegisterDimensions : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IRegister ParentRegisterI { get; }
        int IndexOf(IRegisterDimension reg);

        //IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
        //string FullName { get; } // name with config name
    }
}
