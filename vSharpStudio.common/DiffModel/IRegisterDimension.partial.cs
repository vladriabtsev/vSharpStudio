using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IRegisterDimension : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue
    {
        IGroupListRegisterDimensions ParentGroupListRegisterDimensionsI { get; }
    }
}
