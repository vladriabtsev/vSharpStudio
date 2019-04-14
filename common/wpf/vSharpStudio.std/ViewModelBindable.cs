using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBase
{
    // https://www.codeproject.com/Articles/1278754/Modernize-Your-Csharp-Code-Part-I-Properties
    // https://www.codeproject.com/Articles/1094079/An-advanced-introduction-to-Csharp-Lecture-Notes-P
    // https://www.codeproject.com/Articles/1094829/Professional-techniques-for-Csharp-Lecture-Notes-P#effective-c-sharp

    // https://msdn.microsoft.com/en-us/magazine/mt149362?author=Stephen+Cleary
    // https://msdn.microsoft.com/magazine/jj991977   Async/Await - Best Practices in Asynchronous Programming
    // https://msdn.microsoft.com/magazine/dn605875   Async Programming : Patterns for Asynchronous MVVM Applications: Data Binding
    // https://msdn.microsoft.com/en-us/magazine/dn630647.aspx  Async Programming : Patterns for Asynchronous MVVM Applications: Commands
    // https://msdn.microsoft.com/en-us/magazine/dn630646.aspx  MVVM : Multithreading and Dispatching in MVVM Applications

    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/data-binding-overview
    // https://blogs.windows.com/buildingapps/2015/10/07/optimizing-your-xaml-app-for-performance-10-by-10/
    // https://eprystupa.wordpress.com/2008/07/28/running-wpf-application-with-multiple-ui-threads/
    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/threading-model
    // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/best-practices-for-implementing-the-event-based-asynchronous-pattern
    // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
    public class ViewModelBindable<T> : ViewModelBindable
      where T : ViewModelBindable<T>
    {
        //private SortableObservableCollection<ISortingValue> _validationCollection=null;
        //public SortableObservableCollection<ISortingValue> ApplicatonValidationCollection { get { return _validationCollection; } }
        //public void SetApplicatonValidationCollection(SortableObservableCollection<ISortingValue> collection)
        //{
        //    if (_validationCollection != null)
        //        throw new InvalidOperationException("Application level collection for validation results is already selected.");
        //    _validationCollection = collection;
        //}
#if DEBUG
        public ViewModelBindable()
        {
            //if (this.GetType().Name != typeof(T).Name)
            //    throw new Exception("Unexpected class as a generic type: " + typeof(T).Name + " Expected:" + this.GetType().Name);
        }
#endif
        protected virtual void NotifyPropertyChanged<TResult>
            (Expression<Func<T, TResult>> property)
        {
            string propertyName = ((MemberExpression)property.Body).Member.Name;
            RaisePropertyChanged(propertyName);
        }
    }
    public class DispatcherDummy : IDispatcher
    {
        public void BeginInvoke(Action action)
        {
            throw new NotImplementedException();
        }

        public bool CheckAccess()
        {
            return true;
        }
    }
    public class ViewModelBindable : INotifyPropertyChanged
    {
        public static bool isUnitTests;
        public static ushort MaxSortingWeightShift = 3;
        public static ushort MaxSortingWeight = (ushort)(ulong.MaxValue - (ulong.MaxValue << MaxSortingWeightShift));
        public static ulong SortingWeightBase = ((ulong)1) << (64 - MaxSortingWeightShift);
        public ViewModelBindable()
        {
            if (ViewModelBindable._AppDispatcher == null && isUnitTests)
                ViewModelBindable.AppDispatcher = new DispatcherDummy();
            //if (this.Dispatcher == null)
            //    throw new InvalidOperationException("'ViewModelBindable.AppDispatcher' is not initialized. Use 'ViewModelBindable.AppDispatcher = UIDispatcher.Current;' before first usage ViewModel");
        }
        public bool IsChanged { get { return _IsChanged; } set { SetValue<bool>(ref _IsChanged, value); } }
        private bool _IsChanged;

        #region Messages
        protected readonly MessageBusCore CurrentMessageBus;

        // Proxy for communication with the MessageBus
        private readonly MessageBusProxy _messageBusHelper = new MessageBusProxy();

        /// <summary>
        /// Register callback using a string token, which is usually defined as a constant.
        /// </summary>
        /// <para>
        /// There is no need to unregister because receivers are allowed to be garbage collected.
        /// </para>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void RegisterToReceiveMessages(string token, EventHandler<NotificationEventArgs> callback)
        {
            // Register callback with MessageBusHelper and MessageBus
            _messageBusHelper.Register(token, callback);
            CurrentMessageBus.Register(token, _messageBusHelper);
        }

        /// <summary>
        /// Unregister callback using string token and notification with TOutgoing data
        /// and the subscriber's callback with TIncoming data.
        /// </summary>
        /// <para>
        /// This is optional because registered receivers are allowed to be garbage collected.
        /// </para>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void UnregisterToReceiveMessages<TOutgoing, TIncoming>(string token,
            EventHandler<NotificationEventArgs<TOutgoing, TIncoming>> callback)
        {
            // Unregister callback with MessageBusHelper and MessageBus
            _messageBusHelper.Unregister(token, callback);
            CurrentMessageBus.Unregister(token, _messageBusHelper);
        }

        /// <summary>
        /// Notify registered subscribers.
        /// Call is transparently marshalled to UI thread.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void SendMessage(string token, NotificationEventArgs e)
        {
            // Send notification through the MessageBus
            CurrentMessageBus.Notify(token, this, e);
        }

        /// <summary>
        /// Notify registered subscribers.
        /// Call is transparently marshalled to UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void SendMessage<TOutgoing>(string token,
            NotificationEventArgs<TOutgoing> e)
        {
            // Send notification through the MessageBus
            CurrentMessageBus.Notify(token, this, e);
        }

        /// <summary>
        /// Notify registered subscribers.
        /// Call is transparently marshalled to UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void SendMessage<TOutgoing, TIncoming>(string token,
            NotificationEventArgs<TOutgoing, TIncoming> e)
        {
            // Send notification through the MessageBus
            CurrentMessageBus.Notify(token, this, e);
        }

        #endregion Messages

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        //public void Set(object propField, object newValue, [System.Runtime.CompilerServices.CallerMemberName] string propName = null)
        //{
        //	if (propField != newValue)
        //	{
        //		propField = newValue;
        //		VMHelpers.InternalNotifyPropertyChanged(propName, this, this.PropertyChanged, this.Dispatcher);
        //	}
        //}
        //public bool SetValue<Tprop>(ref Tprop propField, Tprop newValue, [System.Runtime.CompilerServices.CallerMemberName] string propName = null)
        //{
        //  if (!EqualityComparer<Tprop>.Default.Equals(propField, newValue))
        //  {
        //    propField = newValue;
        //    VMHelpers.InternalNotifyPropertyChanged(propName, this, this.PropertyChanged, this.Dispatcher);
        //    IsChanged = true;
        //    return true;
        //  }
        //  return false;
        //}
        public bool SetValue<Tprop>(ref Tprop propField, Tprop newValue, Action onChanged = null, [System.Runtime.CompilerServices.CallerMemberName] string propName = null)
        {
            if (!EqualityComparer<Tprop>.Default.Equals(propField, newValue))
            {
                propField = newValue;
                VMHelpers.InternalNotifyPropertyChanged(propName, this, this.PropertyChanged, this.Dispatcher);
                onChanged?.Invoke();
                IsChanged = true;
                OnValidateProperty(propName);
                return true;
            }
            return false;
        }
        protected virtual void OnValidateProperty(string propertyName) { }

        public void RaisePropertyChanged(string propertyName)
        {
            VMHelpers.InternalNotifyPropertyChanged(propertyName, this, this.PropertyChanged, this.Dispatcher);
        }
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            VMHelpers.InternalNotifyPropertyChanged(propertyName, this, this.PropertyChanged, this.Dispatcher);
        }
        protected IDispatcher Dispatcher { get { return ViewModelBindable.AppDispatcher; } }
        public static IDispatcher AppDispatcher
        {
            get { return ViewModelBindable._AppDispatcher; }
            set
            {
                if (ViewModelBindable._AppDispatcher != null)
                    throw new InvalidOperationException("'ViewModelBindable.AppDispatcher' is already initialized");
                ViewModelBindable._AppDispatcher = value;
            }
        }
        private static IDispatcher _AppDispatcher = null;
        #endregion INotifyPropertyChanged

        protected void ExecuteOnUIThread(Action action)
        {
            if (Dispatcher.CheckAccess())
                action();
            else
                Dispatcher.BeginInvoke(() => action());
        }
    }
}
