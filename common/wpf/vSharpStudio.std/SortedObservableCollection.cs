using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModelBase;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Collections;
using System.Windows.Threading;

namespace ViewModelBase
{
    public interface ISetParent
    {
        void SetParent(object parent);
    }
    public interface ISortingValue
    {
        ulong _SortingNameValue { get; }
        ulong SortingValue { get; set; }
        ulong SortingWeight { get; set; }
    }
    public enum SortDirection { Ascending, Descending }
    public interface IMoveUpDown
    {
        bool CanUp(object current);
        bool CanDown(object current);
        object MoveUp(object current);
        object MoveDown(object current);
        object? GetPrev(object current);
        object? GetNext(object current);
    }
    public interface IObservableCollectionWithActions<T>
    {
        void Clear();
        void Add(T item);
        bool Remove(T item);
        void RemoveAt(int indx);
        void AddRange(IEnumerable<T> collection);
        Action? OnClearingAction { get; set; }
        Action? OnClearedAction { get; set; }
        Action<T>? OnRemovedAction { get; set; }
        Action<T>? OnAddedAction { get; set; }
        Action<T>? OnRemovingAction { get; set; }
        Action<T>? OnAddingAction { get; set; }
    }
    public class ObservableCollectionWithActions<T> : ObservableCollection<T>
    {
        private readonly object _lock = new object();
        public ObservableCollectionWithActions()
        {
        }
        public ObservableCollectionWithActions(IEnumerable<T> lst)
        {
            this.AddRange(lst);
        }
        public new void Clear()
        {
            if (OnClearingAction != null)
                OnClearingAction();
            UIDispatcher.Invoke(() =>
            {
                base.Clear();
            });
            if (OnClearedAction != null)
                OnClearedAction();
        }
        public new void Add(T item)
        {
            lock (_lock)
            {
                if (OnAddingAction != null)
                    OnAddingAction(item);
                UIDispatcher.Invoke(() =>
                {
                    base.Add(item);
                });
                if (OnAddedAction != null)
                    OnAddedAction(item);
            }
        }
        public new bool Remove(T item)
        {
            lock (_lock)
            {
                if (OnRemovingAction != null)
                    OnRemovingAction(item);
                bool res = false;
                UIDispatcher.Invoke(() =>
                {
                    res = base.Remove(item);
                });
                if (OnRemovedAction != null)
                    OnRemovedAction(item);
                return res;
            }
        }
        public new void RemoveAt(int indx)
        {
            lock (_lock)
            {
                var item = this[indx];
                if (OnRemovingAction != null)
                    OnRemovingAction(item);
                UIDispatcher.Invoke(() =>
                {
                    base.RemoveAt(indx);
                });
                if (OnRemovedAction != null)
                    OnRemovedAction(item);
            }
        }
        public void AddRange(IEnumerable<T> collection)
        {
            lock (_lock)
            {
                foreach (T itm in collection)
                {
                    if (OnAddingAction != null)
                        OnAddingAction(itm);
                    this.Add(itm);
                    if (OnAddedAction != null)
                        OnAddedAction(itm);
                }
            }
        }
        public Action? OnClearingAction { get; set; }
        public Action? OnClearedAction { get; set; }
        public Action<T>? OnRemovedAction { get; set; }
        public Action<T>? OnAddedAction { get; set; }
        public Action<T>? OnRemovingAction { get; set; }
        public Action<T>? OnAddingAction { get; set; }
    }
    public interface ISortedObservableCollection<T> : IObservableCollectionWithActions<T>, IMoveUpDown
    {

    }
    public class SortedObservableCollection<T> : ObservableCollectionWithActions<T>, ISortedObservableCollection<T>
      where T : ISortingValue //, IComparable<T> //IEquatable<T>
    {
        private readonly object _lock = new object();
        public SortDirection SortDirection = SortDirection.Ascending;
        //Action<NotifyCollectionChangedEventArgs> onCollectionChanged = null;
        //bool isSort;
        public SortedObservableCollection()
        {
            //this.CollectionChanged += SortedObservableCollection_CollectionChanged;
        }
        public SortedObservableCollection(IEnumerable<T> lst)
        {
            this.AddRange(lst);
        }
        //public SortedObservableCollection(object parent, bool isSort = true) : this()
        //{
        //    this.Parent = parent;
        //}
        //public SortedObservableCollection(Action<NotifyCollectionChangedEventArgs> onCollectionChanged, bool isSort = true) : this()
        //{
        //    this.isSort = isSort;
        //    this.onCollectionChanged = onCollectionChanged;
        //}
        //private void SortedObservableCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (this.onCollectionChanged != null)
        //        this.onCollectionChanged(e);
        //    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        //        foreach (var t in e.NewItems)
        //        {
        //            if (t is ISetParent)
        //                (t as ISetParent).SetParent(this.Parent);
        //        }
        //}
        //public object Parent { get; set; }

        #region IMoveUpDown

        public bool CanUp(object current)
        {
            T p = (T)current;
            if (this.IndexOf(p) > 0)
                return true;
            return false;
        }
        public bool CanDown(object current)
        {
            T p = (T)current;
            if (this.IndexOf(p) < this.Count - 1)
                return true;
            return false;
        }
        public object MoveUp(object current)
        {
            T p = (T)current;
            int i = this.IndexOf(p);
            if (i > 0)
            {
                p.SortingValue = this[i - 1].SortingValue - 1;
            }
            return current;
        }

        public object MoveDown(object current)
        {
            T p = (T)current;
            int i = this.IndexOf(p);
            if (i < this.Count - 1)
            {
                p.SortingValue = this[i + 1].SortingValue + 1;
            }
            return current;
        }

        public object? GetPrev(object current)
        {
            T p = (T)current;
            int i = this.IndexOf(p);
            if (i == 0)
                return null;
            return this[i - 1];
        }

        public object? GetNext(object current)
        {
            T p = (T)current;
            int i = this.IndexOf(p);
            if (i < this.Count - 1)
                return this[i + 1];
            return null;
        }

        #endregion IMove

        public new void Clear()
        {
            if (OnClearingAction != null)
                OnClearingAction();
            base.Clear();
            if (OnClearedAction != null)
                OnClearedAction();
        }
        public new void Add(T item)
        {
            this.Add(item, 0);
        }
        public void Add(T item, ulong sortingWeight)
        {
            if (sortingWeight > 0)
            {
                if (sortingWeight > VmBindable.MaxSortingWeight)
                    throw new ArgumentException("sortingWeight is too big. Expected less then " + VmBindable.MaxSortingWeight);
                item.SortingWeight = sortingWeight << (64 - VmBindable.MaxSortingWeightShift);
                item.SortingValue = item._SortingNameValue + item.SortingWeight;
            }
            base.Add(item);
            InternalSort();
        }
        public new bool Remove(T item)
        {
            var res = base.Remove(item);
            InternalSort();
            return res;
        }
        public new void RemoveAt(int indx)
        {
            base.RemoveAt(indx);
            InternalSort();
        }
        public void AddRange(IEnumerable<T> collection, ulong sortingWeight = 0)
        {
            foreach (T itm in collection)
            {
                this.Add(itm, sortingWeight);
            }
            InternalSort();
        }
        public Action<int, int>? OnSortMovedAction { get; set; }
        #region Sort
        /// <summary>
        /// Sorts the items of the collection in descending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        public void SortDescending<TKey>(Func<T, TKey> keySelector)
        {
            InternalSort(Items.OrderByDescending(keySelector));
        }
        public void SortDescending()
        {
            var comparer = Comparer<ulong>.Create((k1, k2) => k2.CompareTo(k1));
            InternalSort(Items.OrderBy(t => t.SortingValue, comparer));
        }

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        public void Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            InternalSort(Items.OrderBy(keySelector, comparer));
        }
        public void Sort()
        {
            InternalSort();
        }
        private void InternalSort()
        {
            var comparer = Comparer<ulong>.Create((k1, k2) => k1.CompareTo(k2));
            if (SortDirection == SortDirection.Ascending)
                InternalSort(Items.OrderBy(t => t.SortingValue, comparer));
            else
                InternalSort(Items.OrderByDescending(t => t.SortingValue, comparer));
        }
        /// <summary>
        /// Moves the items of the collection so that their orders are the same as those of the items provided.
        /// </summary>
        /// <param name="sortedItems">An <see cref="IEnumerable{T}"/> to provide item orders.</param>
        private void InternalSort(IEnumerable<T> sortedItems)
        {
            lock (_lock)
            {
                var sortedItemsList = sortedItems.ToList();
                if (sortedItemsList.Count == 1)
                    return;
                for (int i = 0; i < sortedItemsList.Count; i++)
                {
                    var item = sortedItemsList[i];
                    var ifrom = -1;
                    for (int j = 0; j < this.Count; j++)
                    {
                        if (this[j].Equals(item))
                        {
                            ifrom = j;
                            break;
                        }
                    }
                    Debug.Assert(ifrom != -1);
                    var ito = i;
                    Debug.Assert(ito != -1);
                    UIDispatcher.Invoke(() =>
                    {
                        base.MoveItem(ifrom, ito);
                    });
                    if (OnSortMovedAction != null)
                        OnSortMovedAction(ifrom, ito);
                }
            }
            UIDispatcher.Invoke(() =>
            {
                OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            });
        }

        #endregion Sort
    }
}
