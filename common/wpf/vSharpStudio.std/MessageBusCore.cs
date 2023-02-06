using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace ViewModelBase
{
  /// <summary>
  /// Facilitates communication among view-models.
  /// To prevent memory leaks weak references are used.
  /// </summary>
  public class MessageBusCore
  {
    // Provide thread-safe access to subscriptions
    private readonly object _sharedLock = new object();

    // Each token can have multiple subscriptions
    private readonly Dictionary<string, List<WeakReference>> _subscriptions =
        new Dictionary<string, List<WeakReference>>();

    // Dispatcher for cross-thread operations
    protected readonly Dispatcher Dispatcher;

    protected MessageBusCore(Dispatcher dispatcher)
    {
      Dispatcher = dispatcher;
    }

    /// <summary>
    /// Register subscriber using a string token, which is usually defined as a constant.
    /// Subscriber performs internal notifications.
    /// </summary>
    /// <param name="token">String identifying a message token</param>
    /// <param name="subscriber">Subscriber requesting notifications</param>
    public void Register(string token, INotifyable subscriber)
    {
      lock (_sharedLock)
      {
        // Add token if not present
        if (!_subscriptions.ContainsKey(token))
        {
          _subscriptions.Add(token, new List<WeakReference> { new WeakReference(subscriber) });
          return;
        }

        // Get subscribers for this token
        List<WeakReference> weakSubscribers;
        if (_subscriptions.TryGetValue(token, out weakSubscribers))
        {
          // See if subcriber is already present
          var existing = (from w in weakSubscribers
                          where w != null && w.IsAlive &&
                              ReferenceEquals(w.Target, subscriber)
                          select w).SingleOrDefault();

          // Add if subcriber is already present
          if (existing == null)
            _subscriptions[token].Add(new WeakReference(subscriber));
        }
      }
    }

    /// <summary>
    /// Remove subscriber from the invocation list
    /// </summary>
    /// <param name="token">String identifying a message token</param>
    /// <param name="subscriber">Subscriber to remove from notifications</param>
    public void Unregister(string token, INotifyable subscriber)
    {
      lock (_sharedLock)
      {
        List<WeakReference> weakSubscribers;
        if (_subscriptions.TryGetValue(token, out weakSubscribers))
        {
          // Find subscriber
          WeakReference weakSubscriber = weakSubscribers
              .Where(w => w.IsAlive && ReferenceEquals
                  (w.Target, subscriber)).SingleOrDefault();

          // Remove subscriber
          if (weakSubscriber != null)
            weakSubscribers.Remove(weakSubscriber);

          // Remove dictionary entry if no subscibers left
          if (_subscriptions[token].Count == 0) _subscriptions.Remove(token);
        }
      }
    }

    /// <summary>
    /// Notify registered subscribers.
    /// Call is transparently marshalled to UI thread.
    /// </summary>
    /// <param name="token">String identifying a message token</param>
    /// <param name="sender">Sender of notification</param>
    /// <param name="e">Event args carrying message</param>
    public void Notify(string token, object sender, NotificationEventArgs e)
    {
      // Notify subscriber on UI thread
      InternalNotify(token, sender, e, true);
    }

    /// <summary>
    /// Notify registered subscribers.
    /// Call is transparently marshalled to UI thread.
    /// </summary>
    /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
    /// <param name="token">String identifying a message token</param>
    /// <param name="sender">Sender of notification</param>
    /// <param name="e">Event args carrying message</param>
    public void Notify<TOutgoing>(string token, object sender,
        NotificationEventArgs<TOutgoing> e)
    {
      // Notify subscriber on UI thread
      InternalNotify(token, sender, e, true);
    }

    /// <summary>
    /// Notify registered subscribers.
    /// Call is transparently marshalled to UI thread.
    /// </summary>
    /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
    /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
    /// <param name="token">String identifying a message token</param>
    /// <param name="sender">Sender of notification</param>
    /// <param name="e">Event args carrying message</param>
    public void Notify<TOutgoing, TIncoming>(string token, object sender,
        NotificationEventArgs<TOutgoing, TIncoming> e)
    {
      // Notify subscriber on UI thread
      InternalNotify(token, sender, e, true);
    }

    /// <summary>
    /// Notify registered subscribers asynchronously.
    /// Call is not marshalled to UI thread.
    /// </summary>
    /// <param name="token">String identifying a message token</param>
    /// <param name="sender">Sender of notification</param>
    /// <param name="e">Event args carrying message</param>
    public void BeginNotify(string token, object sender, NotificationEventArgs e)
    {
      // Notify subscriber on ThreadPool thread
      InternalNotify(token, sender, e, false);
    }

    /// <summary>
    /// Notify registered subscribers asynchronously.
    /// Call is not marshalled to UI thread.
    /// </summary>
    /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
    /// <param name="token">String identifying a message token</param>
    /// <param name="sender">Sender of notification</param>
    /// <param name="e">Event args carrying message</param>
    public void BeginNotify<TOutgoing>(string token, object sender,
        NotificationEventArgs<TOutgoing> e)
    {
      // Notify subscriber on ThreadPool thread
      InternalNotify(token, sender, e, false);
    }

    /// <summary>
    /// Notify registered subscribers asynchronously.
    /// Call is not marshalled to UI thread.
    /// </summary>
    /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
    /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
    /// <param name="token">String identifying a message token</param>
    /// <param name="sender">Sender of notification</param>
    /// <param name="e">Event args carrying message</param>
    public void BeginNotify<TOutgoing, TIncoming>(string token, object sender,
        NotificationEventArgs<TOutgoing, TIncoming> e)
    {
      // Notify subscriber on ThreadPool thread
      InternalNotify(token, sender, e, false);
    }

    private void InternalNotify(string token, object sender,
        NotificationEventArgs e, bool post)
    {
      // Get weak subscribers
      List<WeakReference> weakSubscribers;
      lock (_sharedLock)
      {
        if (!_subscriptions.TryGetValue(token, out weakSubscribers)) return;

        // Make a copy while locked
        weakSubscribers = weakSubscribers.ToList();
      }

      // Get compatible living subscribers
      var subscribers = from w in weakSubscribers
                        let s = w.Target as INotifyable
                        where w != null && w.IsAlive && s != null
                        select s;

      // Invoke each callback associated with token
      foreach (var subscriber in subscribers)
      {
        INotifyable subscriber1 = subscriber;
        SafeNotify(() => subscriber1.Notify(token, sender, e), post);
      }

      lock (_sharedLock)
      {
        if (_subscriptions.ContainsKey(token))
        {
          // Remove subscribers who are no longer alive
          var deadSubscribers = weakSubscribers
              .Where(w => w == null || !w.IsAlive);
          foreach (var s in deadSubscribers)
          {
            _subscriptions[token].Remove(s);
          }

          // Remove dictionary entry if no subscibers left
          if (_subscriptions[token].Count == 0) _subscriptions.Remove(token);
        }
      }
    }

    private void SafeNotify(Action method, bool post)
    {
      try
      {
        // Fire the event on the UI thread
        if (post)
        {
          if (Dispatcher.CheckAccess())
          {
            method();
          }
          else
          {
            Dispatcher.BeginInvoke(method);
          }
        }
        // Fire event on a ThreadPool thread
        else
        {
          ThreadPool.QueueUserWorkItem(o => method(), null);
        }

      }
      catch (Exception ex)
      {
        // If there's an exception write it to the Output window
        Debug.WriteLine(ex.ToString());
      }
    }
  }
}
