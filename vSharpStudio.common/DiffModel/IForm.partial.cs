using System.Collections.Generic;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IForm : ITreeConfigNodeSortable
    {
        IGroupListForms ParentGroupListFormsI { get; }
        IReadOnlyList<IProperty> ListProperties { get; }
        SortedObservableCollection<ISortingValue> ListAllNotSpecialProperties { get; }
    }
}
