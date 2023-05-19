using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModelBase
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datagrid?view=netcore-3.1
    public class DataGridViewModelBase<T> : VmBindable
      where T : VmBindable
    {
        //    public ObservableCollectionExt<E> RowListLoaded { get; set; }

        public ObservableCollectionExt<T> Collection
        {
            get { return _collection; }
            set
            {
                if (SetProperty<ObservableCollectionExt<T>>(ref _collection, value))
                {
                    if (_collection != null && _collection.Count > 0)
                    {
                        this.Selected = _collection[0];
                    }
                }
            }
        }
        private ObservableCollectionExt<T> _collection = new ObservableCollectionExt<T>();

        public T? Selected
        {
            get { return _selected; }
            set { if (SetProperty<T?>(ref _selected, value)) { this.SelectedRowChanged(); } }
        }
        private T? _selected;
        protected virtual void SelectedRowChanged() { }
    }
}
