﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
