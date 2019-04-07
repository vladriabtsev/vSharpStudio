using System;
using System.Windows;
using ViewModelBase;
using WindowsDispatcher = System.Windows.Threading.Dispatcher;

namespace vSharpStudio.wpf
{
    /// <summary>
    /// Helper class for dispatching work across threads.
    /// WPF apps should call Initialize from the UI thread in App_Start.
    /// </summary>
    public sealed class UIDispatcher : IDispatcher
    {
        private static volatile IDispatcher _dispatcher;
        private static readonly object SyncRoot = new Object();
        private readonly WindowsDispatcher _windowsDispatcher;

        private UIDispatcher(WindowsDispatcher windowsDispatcher)
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
        /// Executes the specified delegate asynchronously with the specified array of arguments on the thread the Dispatcher is associated with.
        /// </summary>
        /// <param name="action">A delegate to a method that takes multiple arguments, which is pushed onto the Dispatcher event queue.</param>
        public void BeginInvoke(Action action)
        {
            _windowsDispatcher.BeginInvoke(action);
        }

#if !SILVERLIGHT
        /// <summary>
        /// Invoke from main UI thread.
        /// </summary>
        public static void Initialize()
        {
            WindowsDispatcher windowsDispatcher = WindowsDispatcher.CurrentDispatcher;
            _dispatcher = new UIDispatcher(windowsDispatcher);
        }

#endif
        /// <summary>
        /// Obtain the current dispatcher for cross-thread marshaling
        /// </summary>
        public static IDispatcher Current
        {
            get
            {
                if (_dispatcher == null)
                {
                    lock (SyncRoot)
                    {
#if SILVERLIGHT
						WindowsDispatcher windowsDispatcher = Deployment.Current.Dispatcher;
#else
                        WindowsDispatcher windowsDispatcher = WindowsDispatcher.CurrentDispatcher;
#endif
                        _dispatcher = new UIDispatcher(windowsDispatcher);
                    }
                }
                return _dispatcher;
            }
        }

        /// <summary>
        /// Execute an action on the UI thread.
        /// </summary>
        /// <param name="action"></param>
        public static void Execute(Action action)
        {
            if (_dispatcher.CheckAccess()) action();
            else _dispatcher.BeginInvoke(action);
        }
    }
}
