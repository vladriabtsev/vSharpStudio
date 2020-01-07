// The MIT License (MIT)
//
// Copyright (c) 2016 Microsoft. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ViewModelBase
{

    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
    /// </summary>
    public class VmBindable : INotifyPropertyChanged
    {
        public bool IsNotNotifying { get; set; }

        #region Dispatcher Methods

        /// <summary>
        /// Gets access to the dispatcher for this view or application.
        /// </summary>
        public IDispatcher Dispatcher
        {
            get
            {
                //if (this.View != null)
                //    return this.View.Dispatcher;

                //var coreWindow1 = CoreWindow.GetForCurrentThread();
                //if (coreWindow1 != null)
                //    return coreWindow1.Dispatcher;

                //var coreWindow2 = CoreApplication.MainView.CoreWindow;
                //if (coreWindow2 != null)
                //    return coreWindow2.Dispatcher;
                if (_Dispatcher == null)
                    throw new InvalidOperationException("Dispatcher is not initialized!");
                return _Dispatcher;
            }
        }
        private IDispatcher _Dispatcher;

        /// <summary>
        /// Runs a function on the currently executing platform's UI thread.
        /// </summary>
        /// <param name="action">Code to be executed on the UI thread</param>
        /// <param name="priority">Priority to indicate to the system when to prioritize the execution of the code</param>
        /// <returns>Task representing the code to be executing</returns>
        //protected void InvokeOnUIThread(Action action, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        //{
        //    if (this.Dispatcher == null || this.Dispatcher.HasThreadAccess)
        //    {
        //        action();
        //    }
        //    else
        //    {
        //        // Execute asynchronously on the thread the Dispatcher is associated with.
        //        var task = this.Dispatcher.RunAsync(priority, () => action());
        //    }
        //}
        protected void InvokeOnUIThread(Action action)
        {
            if (Dispatcher.CheckAccess())
                action();
            else
                Dispatcher.BeginInvoke(() => action());
        }

        #endregion

        //#region MessageBox Methods

        ///// <summary>
        ///// Displays a message box dialog.
        ///// </summary>
        ///// <param name="message">Message to display.</param>
        ///// <param name="ct">Cancelation token</param>
        ///// <returns>Awaitable call which returns the index of the button clicked.</returns>
        //protected internal Task<int> ShowMessageBoxAsync(string message, CancellationToken ct)
        //{
        //    return this.ShowMessageBoxAsync(message, Strings.Resources.ApplicationName, null, 0, ct);
        //}

        ///// <summary>
        ///// Displays a message box dialog.
        ///// </summary>
        ///// <param name="message">Message to display.</param>
        ///// <param name="buttonNames">List of buttons to display.</param>
        ///// <param name="defaultIndex">Index of the default button of the dialog box.</param>
        ///// <param name="ct">Cancelation token</param>
        ///// <returns>Awaitable call which returns the index of the button clicked.</returns>
        //protected internal Task<int> ShowMessageBoxAsync(string message, IList<string> buttonNames = null, int defaultIndex = 0, CancellationToken? ct = null)
        //{
        //    return this.ShowMessageBoxAsync(message, Strings.Resources.ApplicationName, buttonNames, defaultIndex, ct);
        //}

        ///// <summary>
        ///// Displays a message box dialog.
        ///// </summary>
        ///// <param name="message">Message to display.</param>
        ///// <param name="title">Title of the message box.</param>
        ///// <param name="buttonNames">List of buttons to display.</param>
        ///// <param name="defaultIndex">Index of the default button of the dialog box.</param>
        ///// <param name="ct">Cancelation token</param>
        ///// <returns>Awaitable call which returns the index of the button clicked.</returns>
        //protected internal async Task<int> ShowMessageBoxAsync(string message, string title, IList<string> buttonNames = null, int defaultIndex = 0, CancellationToken? ct = null)
        //{
        //    if (string.IsNullOrEmpty(message))
        //        throw new ArgumentException("The specified message cannot be null or empty.", "message");

        //    // Set a default title if no title was specified.
        //    if (string.IsNullOrWhiteSpace(title))
        //        title = Strings.Resources.ApplicationName;

        //    int result = defaultIndex;
        //    MessageDialog dialog = new MessageDialog(message, title);

        //    // Show all the button names specified or just an OK label if no names were specified.
        //    if (buttonNames != null && buttonNames.Count > 0)
        //        foreach (string button in buttonNames)
        //            dialog.Commands.Add(new UICommand(button, new UICommandInvokedHandler((o) => result = buttonNames.IndexOf(button))));
        //    else
        //        dialog.Commands.Add(new UICommand(Strings.Resources.TextOk, new UICommandInvokedHandler((o) => result = 0)));

        //    // Set the default button of the dialog
        //    dialog.DefaultCommandIndex = (uint)defaultIndex;

        //    // Show on the appropriate thread
        //    if (this.Dispatcher == null || this.Dispatcher.HasThreadAccess)
        //    {
        //        await dialog.ShowAsync().AsTask(ct.HasValue ? ct.Value : CancellationToken.None);
        //        return result;
        //    }
        //    else
        //    {
        //        var tcs = new TaskCompletionSource<int>();

        //        // Execute asynchronously on the thread the Dispatcher is associated with.
        //        await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
        //        {
        //            await dialog.ShowAsync().AsTask(ct.HasValue ? ct.Value : CancellationToken.None);
        //            tcs.TrySetResult(result);
        //        });
        //        return tcs.Task.Result;
        //    }
        //}

        //#endregion

        //public virtual int CompareToById(VmBindable other) { throw new NotImplementedException("Please override CompareToById method"); }
        //public int CompareTo(VmBindable obj)
        //{
        //    int res = obj.GetType().Name.CompareTo(this.GetType().Name);
        //    if (res != 0) return res;
        //    return CompareToById(obj);
        //}

        #region Events

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            else
            {
                storage = value;
                this.NotifyPropertyChanged(propertyName);
                return true;
            }
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Optional name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (IsNotNotifying)
                return;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <typeparam name="T">Type of the property in the expression.</typeparam>
        /// <param name="property">Expression to retrieve the property. Example: () => this.FirstName</param>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> property)
        {
            var propertyName = this.GetPropertyName<T>(property);
            this.NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Gets the string name of a property expression.
        /// </summary>
        /// <typeparam name="T">Type of the property in the expression.</typeparam>
        /// <param name="property">Expression to retrieve the property. Example: () => this.FirstName</param>
        /// <returns>String value representing the property name.</returns>
        protected string GetPropertyName<T>(Expression<Func<T>> property)
        {
            MemberExpression memberExpression = GetMememberExpression<T>(property);
            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Gets the MemberExpression from a property expression.
        /// </summary>
        /// <typeparam name="T">Type of the property in the expression.</typeparam>
        /// <param name="property">Expression to retrieve the property. Example: () => this.FirstName</param>
        /// <returns>MemberExpression instance presenting the property expression.</returns>
        private MemberExpression GetMememberExpression<T>(Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
                memberExpression = (MemberExpression)lambda.Body;

            return memberExpression;
        }

        /// <summary>
        /// Retrieves a PropertyInfo object representing the property in the specified expression.
        /// </summary>
        /// <typeparam name="T">Type of the property in the expression.</typeparam>
        /// <param name="property">Expression to retrieve the property. Example: () => this.FirstName</param>
        /// <returns>PropertyInfo object of the expression property else null if not found.</returns>
        protected internal PropertyInfo GetPropertyInfo<T>(Expression<Func<T>> property)
        {
            if (property != null && property.Body is MemberExpression)
            {
                var mex = (MemberExpression)property.Body;
                if (mex != null && mex.Member is PropertyInfo)
                    return mex.Member as PropertyInfo;
            }
            return null;
        }

        /// <summary>
        /// Sets a PropertyInfo object with passed in value.
        /// </summary>
        /// <param name="pi">PropertyInfo instance representing the property that needs to be set.</param>
        /// <param name="value">Value to set to the specified PropertyInfo instance.</param>
        protected internal void SetPropertyValue(PropertyInfo pi, object value)
        {
            if (pi != null && pi.CanWrite)
                pi.SetValue(this, value, null);
        }

        #endregion
    }
}
