//#define TEST
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.ComponentModel;

namespace ViewModelBase
{
  public class ObservableCollectionExt<T> : ObservableCollection<T>
  {

    public new event NotifyCollectionChangedEventHandler CollectionChanged;
    public new event PropertyChangedEventHandler PropertyChanged;
//    public event NotifyCollectionChangedEventHandler CollectionChangedNoThrottling;

    Action onCountChange = null;

    public ObservableCollectionExt()
    {
      this.Dispatcher = ViewModelBindable.AppDispatcher;
      _timer = new Timer(new TimerCallback(TimerCallback), null, Timeout.Infinite, Timeout.Infinite);
      base.PropertyChanged += ObservableCollectionExt_PropertyChanged;
      base.CollectionChanged += ObservableCollectionExt_CollectionChanged;
    }

    public ObservableCollectionExt(Action onCountChange)
      : this()
    {
      this.onCountChange = onCountChange;
    }

    public ObservableCollectionExt(IEnumerable<T> collection, Action onCountChange = null)
      : this()
    {
      this.onCountChange = onCountChange;
      this.AddRange(collection);
      if (this.onCountChange != null)
        this.onCountChange();
    }

    public ObservableCollectionExt(List<T> collection, Action onCountChange = null)
      : this()
    {
      this.onCountChange = onCountChange;
      this.AddRange(collection);
      if (this.onCountChange != null)
        this.onCountChange();
    }

    // implement auto throttling
    private bool throttleNotification = false;
    //public bool TrotlteNotification
    //{
    //  get { return trottlteNotification; }
    //  set
    //  {
    //    trottlteNotification = value;
    //    if (!trottlteNotification)
    //    {
    //      base.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
    //    }
    //  }
    //}

    void ObservableCollectionExt_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      lock (_lock)
      {
        if (PropertyChanged != null)
        {
          if (Dispatcher.CheckAccess())
          {
            PropertyChanged(sender, e);
          }
          else
          {
            Dispatcher.BeginInvoke(() => PropertyChanged(sender, e));
          }
        }
      }
    }

    void ObservableCollectionExt_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      lock (_lock)
      {
//        if (CollectionChangedNoThrottling != null)
//        {
//          if (Dispatcher.CheckAccess())
//          {
//#if TEST
//            logger.Debug("direct notification");
//#endif
//            CollectionChangedNoThrottling(sender, e);
//          }
//          else
//          {
//#if TEST
//            logger.Debug("dispatcher notification");
//#endif
//            Dispatcher.BeginInvoke(() => CollectionChangedNoThrottling(sender, e));
//          }
//        }

        if (CollectionChanged != null)
        {
          if (Dispatcher.CheckAccess())
          {
#if TEST
            logger.Debug("direct notification");
#endif
            CollectionChanged(sender, e);
          }
          else
          {
#if TEST
            logger.Debug("dispatcher notification");
#endif
            Dispatcher.BeginInvoke(() => CollectionChanged(sender, e));
          }
        }
      }
    }

    protected readonly IDispatcher Dispatcher;

    private Timer _timer;
    //    private NotifyCollectionChangedEventArgs eLast;
    // this is connected to the DispatherTimer
    private void TimerCallback(object state)
    {
      lock (_lock)
      {
//        trottlteNotification = false;
        // Fire the event on the UI thread
        if (Dispatcher.CheckAccess())
        {
#if TEST
          logger.Debug("direct notification");
#endif
          base.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
        }
        else
        {
#if TEST
          logger.Debug("dispatcher notification");
#endif
          // base.
          Dispatcher.BeginInvoke(() => base.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset)));
        }
        if (this.onCountChange != null)
          this.onCountChange();
      }
    }


    //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    //{
    //  // Be nice - use BlockReentrancy like MSDN said
    //  using (BlockReentrancy())
    //  {
    //    var eventHandler = CollectionChanged;
    //    if (eventHandler != null)
    //    {
    //      Delegate[] delegates = eventHandler.GetInvocationList();
    //      // Walk thru invocation list
    //      foreach (NotifyCollectionChangedEventHandler handler in delegates)
    //      {
    //        var dispatcherObject = handler.Target as DispatcherObject;
    //        // If the subscriber is a DispatcherObject and different thread
    //        if (dispatcherObject != null && dispatcherObject.CheckAccess() == false)
    //          // Invoke handler in the target dispatcher's thread
    //          dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind,
    //                        handler, this, e);
    //        else // Execute handler as is
    //          handler(this, e);
    //      }
    //    }
    //  }
    //}

    private object _lock = new object();

    public new void Add(T item)
    {
      lock (_lock)
      {
        base.Add(item);
      }
    }

    public new bool Remove(T item)
    {
      lock (_lock)
      {
        return base.Remove(item);
      }
    }

    public new void RemoveAt(int index)
    {
      lock (_lock)
      {
        base.RemoveAt(index);
      }
    }

    private int _trottleInterval = 500;

    private bool _firstNotification = true;
    private DateTime _lastChanges;
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      lock (_lock)
      {
        //if (_firstNotification)
        //{
        //  _firstNotification = false;
        //  _lastChanges = DateTime.Now;
        //}
        //else
        //{
        //  DateTime lastChanges = DateTime.Now;
        //  if ((lastChanges - _lastChanges).Milliseconds < _trottleInterval)
        //  {
        //    trottlteNotification = true;
        //  }
        //  _lastChanges = lastChanges;
        //}
        if (throttleNotification)
        {
#if TEST
//          logger.Debug("reset timer");
#endif
          _timer.Change(_trottleInterval, Timeout.Infinite); // collect all notification for 500 mc
        }
        else
        {
#if TEST
          logger.Debug("direct call");
#endif
          base.OnCollectionChanged(e);
          if (this.onCountChange != null)
            this.onCountChange();
        }
      }
    }

    //    bool deferNotification = false;
    public void AddRange(IEnumerable<T> collection)
    {
      //      deferNotification = true;
      int index = this.Count;
      try
      {
        foreach (T itm in collection)
        {
          this.Add(itm);
        }
      }
      catch (Exception ex)
      {

      }
      //      deferNotification = false;
      //      base.OnCollectionChanged(e);
    }

    public void RemoveRange(IEnumerable<T> collection)
    {
      //      deferNotification = true;
      foreach (T itm in collection)
      {
        this.Remove(itm);
      }
      //      deferNotification = false;
      //      base.OnCollectionChanged(e);
    }

    //protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    //{
    //  if (!deferNotification)
    //  {
    //      base.OnCollectionChanged(e);
    //  }
    //}

    #region Sorting

    /// <summary>
    /// Sorts the items of the collection in ascending order according to a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="keySelector">A function to extract a key from an item.</param>
    public void Sort<TKey>(Func<T, TKey> keySelector)
    {
      InternalSort(Items.OrderBy(keySelector));
    }

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

    /// <summary>
    /// Moves the items of the collection so that their orders are the same as those of the items provided.
    /// </summary>
    /// <param name="sortedItems">An <see cref="IEnumerable{T}"/> to provide item orders.</param>
    private void InternalSort(IEnumerable<T> sortedItems)
    {
      var sortedItemsList = sortedItems.ToList();

      foreach (var item in sortedItemsList)
      {
        MoveItem(IndexOf(item), sortedItemsList.IndexOf(item), false);
      }
      OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
    }

    protected virtual void MoveItem(int oldIndex, int newIndex, bool notify = true)
    {
      //      deferNotification = true;
      //      this.CheckReentrancy();
      T t = base[oldIndex];
      base.RemoveItem(oldIndex);
      base.InsertItem(newIndex, t);
      //this.OnPropertyChanged("Item[]");
      //this.OnCollectionChanged(NotifyCollectionChangedAction.Move, t, newIndex, oldIndex);
      //      deferNotification = false;
      if (notify)
        OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
    }

    #endregion // Sorting

  }
}
