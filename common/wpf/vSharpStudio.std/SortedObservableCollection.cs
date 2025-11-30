using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using CommunityToolkit.Diagnostics;

namespace ViewModelBase
{
    public interface ISetParent
    {
        void SetParent(object parent);
    }
    public interface ISortingValue
    {
        int ExplicitSortingPosition { get; set; }
        /// <summary>
        /// Set ExplicitSortingPosition field without changing IsChanged
        /// </summary>
        /// <param name="sortPosition"></param>
        void SetExplicitSortingPosition(int sortPosition);
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
        private readonly Lock _lock = new();
        public ObservableCollectionWithActions()
        {
        }
        public ObservableCollectionWithActions(IEnumerable<T> lst)
        {
            this.AddRange(lst);
        }
        public new void Clear()
        {
            OnClearingAction?.Invoke();
            UIDispatcher.Invoke(() =>
            {
                base.Clear();
            });
            OnClearedAction?.Invoke();
        }
        public void AddClone(T item)
        {
            base.Add(item);
        }
        public new void Add(T item)
        {
            lock (_lock)
            {
                OnAddingAction?.Invoke(item);
                UIDispatcher.Invoke(() =>
                {
                    base.Add(item);
                });
                OnAddedAction?.Invoke(item);
            }
        }
        public new bool Remove(T item)
        {
            lock (_lock)
            {
                OnRemovingAction?.Invoke(item);
                bool res = false;
                UIDispatcher.Invoke(() =>
                {
                    res = base.Remove(item);
                });
                OnRemovedAction?.Invoke(item);
                return res;
            }
        }
        public new void RemoveAt(int indx)
        {
            lock (_lock)
            {
                var item = this[indx];
                OnRemovingAction?.Invoke(item);
                UIDispatcher.Invoke(() =>
                {
                    base.RemoveAt(indx);
                });
                OnRemovedAction?.Invoke(item);
            }
        }
        public void AddRange(IEnumerable<T> collection)
        {
            lock (_lock)
            {
                foreach (T itm in collection)
                {
                    OnAddingAction?.Invoke(itm);
                    this.Add(itm);
                    OnAddedAction?.Invoke(itm);
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
    public interface ISortedObservableCollection<T> : IObservableCollectionWithActions<T>
    {
        bool CanUp(T current);
        bool CanDown(T current);
        T MoveUp(T current);
        T MoveDown(T current);
        T? GetPrev(T current);
        T? GetNext(T current);
    }
    public class SortedObservableCollection<T> : ObservableCollectionWithActions<T>, ISortedObservableCollection<T>
      where T : ISortingValue //, IComparable<T> //IEquatable<T>
    {
        public enum SortingDirection { INCREASE, DECREASE }
        public SortingDirection Direction { get; set; } = SortingDirection.INCREASE;
        private readonly Lock _lock = new();
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

        #region IMoveUpDown
        public virtual bool CanUp(T current)
        {
            if (this.IndexOf(current) > 0)
                return true;
            return false;
        }
        public virtual bool CanDown(T current)
        {
            if (this.IndexOf(current) < this.Count - 1)
                return true;
            return false;
        }
        public T MoveUp(T current)
        {
            int i = this.Items.IndexOf(current);
            if (i > 0)
            {
                var prev = this.Items[i - 1].ExplicitSortingPosition;
                this.Items[i - 1].SetExplicitSortingPosition(this.Items[i].ExplicitSortingPosition);
                this.Items[i].SetExplicitSortingPosition(prev);
                this.InternalSort();
            }
            return current;
        }
        public T MoveDown(T current)
        {
            int i = this.Items.IndexOf(current);
            if (i < this.Count - 1)
            {
                var next = this.Items[i + 1].ExplicitSortingPosition;
                this.Items[i + 1].SetExplicitSortingPosition(this.Items[i].ExplicitSortingPosition);
                this.Items[i].SetExplicitSortingPosition(next);
                this.InternalSort();
            }
            return current;
        }
        public T? GetPrev(T current)
        {
            int i = this.IndexOf(current);
            if (i == 0)
                return default;
            return this[i - 1];
        }
        public T? GetNext(T current)
        {
            int i = this.IndexOf(current);
            if (i < this.Count - 1)
                return this[i + 1];
            return default;
        }
        #endregion IMoveUpDown

        public new void Clear()
        {
            OnClearingAction?.Invoke();
            base.Clear();
            OnClearedAction?.Invoke();
        }
        /// <summary>
        /// Add T item after selected
        /// </summary>
        /// <param name="item"></param>
        /// <param name="selected"></param>
        public void Add(T item, T? selected)
        {
            int explicitPosition = 0;
            if (selected == null) // add at end of list
            {
                if (this.Count > 0)
                {
                    for (int i = 0; i < this.Count; ++i)
                    {
                        explicitPosition = Math.Max(explicitPosition, this[i].ExplicitSortingPosition);
                    }
                }
            }
            else
            {
                var indx = this.IndexOf(selected);
                explicitPosition = selected.ExplicitSortingPosition;
                for (int i = this.Count - 1; i > indx; ++i)
                {
                    this[i].SetExplicitSortingPosition(this[i].ExplicitSortingPosition + 1);
                }
            }
            this.Add(item, ++explicitPosition);
        }
        public void Add(T item, int explicitPosition)
        {
            item.SetExplicitSortingPosition(explicitPosition);
            //item.SetExplicitSortingPosition(explicitPosition);
            base.Add(item);
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
        public new void AddRange(IEnumerable<T> collection)
        {
            foreach (T itm in collection)
            {
                this.Add(itm);
            }
            InternalSort();
        }
        public Action<int, int>? OnSortMovedAction { get; set; }
        #region Sort
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
        /// <summary>
        /// Moves the items of the collection so that their orders are the same as those of the items provided.
        /// </summary>
        /// <param name="sortedItems">An <see cref="IEnumerable{T}"/> to provide item orders.</param>
        protected void InternalSort(IEnumerable<T> sortedItems)
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
                        if (this[j].ExplicitSortingPosition == item.ExplicitSortingPosition)
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
                    OnSortMovedAction?.Invoke(ifrom, ito);
                }
            }
            UIDispatcher.Invoke(() =>
            {
                OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            });
        }
        protected virtual void InternalSort()
        {
            if (Items.Count > 1)
            {
                if (Direction == SortingDirection.INCREASE)
                {
                    InternalSort(Items.OrderBy(t => t.ExplicitSortingPosition, Comparer<int>.Create((k1, k2) => k1.CompareTo(k2))));
                }
                else if (Direction == SortingDirection.DECREASE)
                {
                    InternalSort(Items.OrderByDescending(t => t.ExplicitSortingPosition, Comparer<int>.Create((k1, k2) => k1.CompareTo(k2))));
                }
                else
                    throw new NotImplementedException();
            }
        }
        ///// <summary>
        ///// Sorts the items of the collection in descending order according to a key.
        ///// </summary>
        ///// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        ///// <param name="keySelector">A function to extract a key from an item.</param>
        //public void SortDescending<TKey>(Func<T, TKey> keySelector)
        //{
        //    InternalSort(Items.OrderByDescending(keySelector));
        //}
        //public void SortDescending()
        //{
        //    var comparer = Comparer<int>.Create((k1, k2) => k2.CompareTo(k1));
        //    InternalSort(Items.OrderBy(t => t.ExplicitSortingPosition, comparer));
        //}
        #endregion Sort
    }
}
