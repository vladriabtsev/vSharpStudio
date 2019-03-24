using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModelBase;

namespace ViewModelBase
{
    public interface ISortingValue
    {
        int SortingValue { get; }
    }
    public class SortableObservableCollection<T> : ObservableCollection<T>
      where T : ISortingValue
    {

    }
}
