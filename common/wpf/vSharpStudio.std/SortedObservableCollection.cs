using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModelBase;

namespace ViewModelBase
{
    public interface ISortingValue
    {
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
        object GetPrev(object current);
        object GetNext(object current);
    }
    public class SortedObservableCollection<T> : ObservableCollection<T>, IMoveUpDown
      where T : ISortingValue
    {
        private object _lock = new object();
        public SortDirection SortDirection = SortDirection.Ascending;

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

        public object GetPrev(object current)
        {
            T p = (T)current;
            int i = this.IndexOf(p);
            if (i == 0)
                return null;
            return this[i - 1];
        }

        public object GetNext(object current)
        {
            T p = (T)current;
            int i = this.IndexOf(p);
            if (i < this.Count - 1)
                return this[i + 1];
            return null;
        }

        #endregion IMove

        public new void Add(T item)
        {
            this.Add(item, 0);
        }
        public void Add(T item, ulong sortingWeight)
        {
            if (sortingWeight > 0)
            {
                if (sortingWeight > ViewModelBindable.MaxSortingWeight)
                    throw new ArgumentException("sortingWeight is too big. Expected less then " + ViewModelBindable.MaxSortingWeight);
                item.SortingWeight = sortingWeight << (64 - ViewModelBindable.MaxSortingWeightShift);
                item.SortingValue = item.SortingValue + item.SortingWeight;
            }
            lock (_lock)
            {
                base.Add(item);
                InternalSort();
            }
        }
        public new bool Remove(T item)
        {
            lock (_lock)
            {
                var res = base.Remove(item);
                InternalSort();
                return res;
            }
        }
        public void AddRange(IEnumerable<T> collection, ulong sortingWeight = 0)
        {
            lock (_lock)
            {
                foreach (T itm in collection)
                {
                    this.Add(itm, sortingWeight);
                }
                InternalSort();
            }
        }
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
            lock (_lock)
            {
                InternalSort(Items.OrderBy(keySelector, comparer));
            }
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
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList)
            {
                base.MoveItem(IndexOf(item), sortedItemsList.IndexOf(item));
            }
            OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
        }

        #endregion Sort
    }
}
