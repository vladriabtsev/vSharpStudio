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
        int SortingValue { get; }
    }
    public class SortedObservableCollection<T> : ObservableCollection<T>
      where T : ISortingValue , IComparable<int>
    {
        private object _lock = new object();
        public new void Add(T item)
        {
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
        public void AddRange(IEnumerable<T> collection)
        {
            lock (_lock)
            {
                foreach (T itm in collection)
                {
                    this.Add(itm);
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
        private void InternalSort()
        {
            var comparer = Comparer<int>.Create((k1, k2) => k2.CompareTo(k1));
            InternalSort(Items.OrderBy(t=>t.SortingValue, comparer));
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
