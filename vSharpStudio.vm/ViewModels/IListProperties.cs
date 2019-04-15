using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    interface IListProperties
    {
        SortedObservableCollection<Property> ListProperties { get; }
    }
}
