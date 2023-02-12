using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace ViewModelBase
{
    /// <summary>
    /// Helper class for dispatching work across threads.
    /// WPF apps should call Initialize from the UI thread in App_Start.
    /// </summary>
    public sealed class UIDispatcher //: IDispatcher
    {
        private static volatile UIDispatcher _dispatcher;
        private static readonly object SyncRoot = new Object();
        private readonly Dispatcher _windowsDispatcher;

        private UIDispatcher(Dispatcher windowsDispatcher)
        {
            _windowsDispatcher = windowsDispatcher;
        }

        /// <summary>
        /// Determines whether the calling thread is the thread associated with this Dispatcher.
        /// </summary>
        /// <returns>
        /// true if the calling thread is the thread associated with this UIDispatcher; otherwise, false.
        /// </returns>
        public bool CheckAccess()
        {
            return _windowsDispatcher.CheckAccess();
        }

        /// <summary>
        /// Invoke from main UI thread.
        /// </summary>
        public static void Initialize()
        {
            var windowsDispatcher = Dispatcher.CurrentDispatcher;
            _dispatcher = new UIDispatcher(windowsDispatcher);
        }

        /// <summary>
        /// Obtain the current dispatcher for cross-thread marshaling
        /// </summary>
        public static UIDispatcher Current
        {
            get
            {
                if (_dispatcher == null)
                {
                    lock (SyncRoot)
                    {
                        var windowsDispatcher = Dispatcher.CurrentDispatcher;
                        _dispatcher = new UIDispatcher(windowsDispatcher);
                    }
                }
                return _dispatcher;
            }
        }

        /// <summary>
        /// Execute an action synchronously on the UI thread.
        /// </summary>
        /// <param name="action"></param>
        public static void Invoke(Action action)
        {
            Debug.Assert((VmBindable.isUnitTests && _dispatcher == null) || (!VmBindable.isUnitTests && _dispatcher != null));
            if (_dispatcher == null)
            {
                action();
                return;
            }
            if (_dispatcher.CheckAccess()) action();
            else _dispatcher._windowsDispatcher.Invoke(action);
        }
        /// <summary>
        /// Start execute an action asynchronously on the UI thread.
        /// </summary>
        /// <param name="action"></param>
        public static void BeginInvoke(Action action)
        {
            if (_dispatcher == null)
            {
                action();
                return;
            }
            if (_dispatcher.CheckAccess()) action();
            else _dispatcher._windowsDispatcher.BeginInvoke(action);
        }
    }
}
