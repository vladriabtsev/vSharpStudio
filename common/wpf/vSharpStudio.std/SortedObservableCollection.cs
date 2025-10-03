using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Diagnostics;

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
        /// <summary>
        /// Set sorting value field without changing IsChanged
        /// </summary>
        /// <param name="sortValue"></param>
        void SetSortingValueField(ulong sortValue);
        ulong SortingWeight { get; set; }
        string NameToCompare { get; }
    }
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
        /// <summary>
        /// Current sorting direction: 0 - explicitly by user; 1 - ascending order; 2 - descending order;
        /// </summary>
        public int SortingDirection { get; set; }
        private readonly object _lock = new object();
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
        //public SortedObservableCollection(Action<T> onAddingAction, Action<T> onRemovingAction, Action? onClearingAction = null)
        //{
        //    this.OnAddingAction = onAddingAction;
        //    this.OnRemovingAction = onRemovingAction;
        //    this.OnClearingAction = onClearingAction;
        //}
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
            if (this.SortingDirection != 0)
                return false;
            T p = (T)current;
            if (this.IndexOf(p) > 0)
                return true;
            return false;
        }
        public bool CanDown(object current)
        {
            if (this.SortingDirection != 0)
                return false;
            T p = (T)current;
            if (this.IndexOf(p) < this.Count - 1)
                return true;
            return false;
        }
        public object MoveUp(object current)
        {
            Debug.Assert(this.SortingDirection == 0);
            T p = (T)current;
            int i = this.IndexOf(p);
            while (i > 0)
            {
                var prev = this[i - 1].SortingValue;
                this[i - 1].SortingValue = this[i].SortingValue;
                this[i].SortingValue = prev;
                i--;
            }
            return current;
        }

        public object MoveDown(object current)
        {
            Debug.Assert(this.SortingDirection == 0);
            T p = (T)current;
            int i = this.IndexOf(p);
            while (i < this.Count - 1)
            {
                var next = this[i + 1].SortingValue;
                this[i + 1].SortingValue = this[i].SortingValue;
                this[i].SortingValue = next;
                i++;
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
                Guard.IsLessThan(sortingWeight, VmBindable.MaxSortingWeight);
                item.SortingWeight = sortingWeight << (64 - VmBindable.MaxSortingWeightShift);
                //item.SortingValue = item._SortingNameValue + item.SortingWeight;
                item.SetSortingValueField(item._SortingNameValue + item.SortingWeight);
            }
            base.Add(item);
            //#if DEBUG
            //            if (item is not ValidationMessage)
            //            {
            //            }
            //#endif
            InternalSort();
        }
        public new bool Remove(T item)
        {
            var res = base.Remove(item);
            // InternalSort(); no need for resorting
            return res;
        }
        public new void RemoveAt(int indx)
        {
            base.RemoveAt(indx);
            // InternalSort(); no need for resorting
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
        public void Sort(int? sortType = null)
        {
            if (sortType != null)
            {
                this.SortingDirection = sortType.Value;
            }
            InternalSort();
        }
        private void InternalSort()
        {
            if (Items.Count > 1)
            {
                switch (this.SortingDirection)
                {
                    case 0: // SortType.ExplicitlyByUser
                        InternalSort(Items.OrderBy(t => t.SortingValue, Comparer<ulong>.Create((k1, k2) => k1.CompareTo(k2))));
                        break;
                    case 1: // SortType.Ascending:
                        InternalSort(Items.OrderBy(t => t.NameToCompare, Comparer<string>.Create((k1, k2) => k1.CompareTo(k2))));
                        break;
                    case 2: // SortType.Descending:
                        InternalSort(Items.OrderByDescending(t => t.NameToCompare, Comparer<string>.Create((k1, k2) => k1.CompareTo(k2))));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
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
                        if (this[j].SortingValue == item.SortingValue)
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
