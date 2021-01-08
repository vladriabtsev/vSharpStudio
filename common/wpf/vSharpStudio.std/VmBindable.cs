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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ViewModelBase
{
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

    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
    /// </summary>
    public class VmBindable : INotifyPropertyChanged
    {
        public VmBindable()
        {
            if (VmBindable._AppDispatcher == null && isUnitTests)
                VmBindable.AppDispatcher = new DispatcherDummy();
        }
        public static bool isUnitTests;
#if DEBUG
        public static bool isNotValidateForUnitTests;
#endif
        public static ushort MaxSortingWeightShift = 4;
        public static ushort MaxSortingWeight = (ushort)(ulong.MaxValue - (ulong.MaxValue << MaxSortingWeightShift));
        public static ulong SortingWeightBase = ((ulong)1) << (64 - MaxSortingWeightShift);
        [BrowsableAttribute(false)]
        public bool IsNotNotifying { get; set; }
        [BrowsableAttribute(false)]
        public bool IsValidate { get; set; }
        public static bool IsValidateAll = true;

        [BrowsableAttribute(false)]
        public bool IsBusy
        {
            get { return _isBusy; }
            set { if (SetProperty<bool>(ref _isBusy, value)) { this.IsNotBusy = !this.IsBusy; this.IsBusyChanged(); } }
        }
        private bool _isBusy;
        [BrowsableAttribute(false)]
        public bool IsNotBusy
        {
            get { return _isNotBusy; }
            set { if (SetProperty<bool>(ref _isNotBusy, value)) { this.IsBusyChanged(); } }
        }
        private bool _isNotBusy = true;
        protected virtual void IsBusyChanged() { }

        #region Dispatcher Methods

        /// <summary>
        /// Gets access to the dispatcher for this view or application.
        /// </summary>
        //public IDispatcher Dispatcher
        //{
        //    get
        //    {
        //        //if (this.View != null)
        //        //    return this.View.Dispatcher;

        //        //var coreWindow1 = CoreWindow.GetForCurrentThread();
        //        //if (coreWindow1 != null)
        //        //    return coreWindow1.Dispatcher;

        //        //var coreWindow2 = CoreApplication.MainView.CoreWindow;
        //        //if (coreWindow2 != null)
        //        //    return coreWindow2.Dispatcher;
        //        if (_Dispatcher == null)
        //            throw new InvalidOperationException("Dispatcher is not initialized!");
        //        return _Dispatcher;
        //    }
        //}
        //private IDispatcher _Dispatcher;
        protected IDispatcher Dispatcher { get { return VmBindable.AppDispatcher; } }
        public static IDispatcher AppDispatcher
        {
            get { return VmBindable._AppDispatcher; }
            set
            {
                if (VmBindable._AppDispatcher != null)
                    throw new InvalidOperationException("'VmBindable.AppDispatcher' is already initialized");
                VmBindable._AppDispatcher = value;
            }
        }
        private static IDispatcher _AppDispatcher = null;

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

        public void CopyDiffToObject(object destination, string[] exclProps = null, Type interfaceSourceType = null)
        {
            PropertyInfo[] pdlst;
            pdlst = destination.GetType().GetProperties();
            Dictionary<string, PropertyInfo> ddic = new Dictionary<string, PropertyInfo>();
            foreach (PropertyInfo p in pdlst)
                ddic[p.Name] = p;

            PropertyInfo[] plst;
            if (interfaceSourceType == null)
            {
                plst = this.GetType().GetProperties();
            }
            else
            {
                plst = interfaceSourceType.GetProperties();
            }
            foreach (PropertyInfo p in plst)
            {
                if (exclProps != null)
                {
                    bool is_found = false;
                    foreach (var t in exclProps)
                    {
                        if (t == p.Name)
                        {
                            is_found = true;
                            break;
                        }
                    }
                    if (is_found)
                        continue;
                }
                if (!ddic.ContainsKey(p.Name))
                    throw new Exception("Destination object doesn't have property with name: " + p.Name);
                var dp = ddic[p.Name];
                object to = dp.GetValue(destination, null);
                var toType = dp.PropertyType;
                object from = p.GetValue(this, null);
                var fromType = p.PropertyType;
                switch (fromType.Name)
                {
                    case "String":
                        if ((string)from != (string)to)
                            dp.SetValue(destination, from);
                        break;
                    case "Int32":
                        if ((int)from != (int)to)
                            dp.SetValue(destination, from);
                        break;
                    case "UInt32":
                        if ((uint)from != (uint)to)
                            dp.SetValue(destination, from);
                        break;
                    case "Boolean":
                        if ((bool)from != (bool)to)
                            dp.SetValue(destination, from);
                        break;
                    case "Nullable`1":
                        if (fromType.UnderlyingSystemType.GenericTypeArguments.Count() == 1)
                        {
                            var type = fromType.UnderlyingSystemType.GenericTypeArguments[0];
                            if (from == null && to == null)
                                break;
                            switch (fromType.Name)
                            {
                                case "Int32":
                                    if ((int?)from != (int?)to)
                                        dp.SetValue(destination, from);
                                    break;
                                case "Boolean":
                                    if ((bool?)from != (bool?)to)
                                        dp.SetValue(destination, from);
                                    break;
                                default:
                                    throw new Exception("Not suported type: " + fromType.Name);
                            }
                        }
                        else
                        {
                            throw new Exception("Not suported type: " + fromType.Name);
                        }
                        break;
                    default:
                        if (fromType.BaseType?.Name == "Enum")
                        {
                            if (toType.BaseType?.Name != "Enum")
                                throw new Exception("Destination object property with name '" + p.Name + "' is not Enum");
                            if ((int)from != (int)to)
                            {
                                foreach (var t in Enum.GetValues(toType))
                                {
                                    if ((int)from == (int)to)
                                    {
                                        dp.SetValue(destination, t);
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Not suported type: " + fromType.Name);
                        }
                        break;
                }
            }
        }
        // Copy all properties except entity Id
        //public virtual void Clone(RowSelectAbstract from)
        //{
        //    Type et = this.GetType();
        //    bool found = false;
        //    Type etFrom = from.GetType();
        //    while (etFrom != null)
        //    {
        //        if (et.Name != etFrom.Name || et.Namespace != etFrom.Namespace)
        //        {
        //            etFrom = etFrom.BaseType;
        //        }
        //        else
        //        {
        //            found = true;
        //            break;
        //        }
        //    }
        //    if (!found)
        //    {
        //        etFrom = from.GetType();
        //        while (et != null)
        //        {
        //            if (et.Name != etFrom.Name || et.Namespace != etFrom.Namespace)
        //            {
        //                et = et.BaseType;
        //            }
        //            else
        //            {
        //                found = true;
        //                break;
        //            }
        //        }
        //    }
        //    if (!found)
        //    {
        //        throw new Exception("Types for copying have to be same or from class has to be subclass of this class ");
        //    }
        //    this.CopyMemberProperties(from, et);
        //}
        //private void CopyMemberProperties(RowSelectAbstract from, Type et)
        //{
        //    PropertyInfo[] plst = et.GetProperties();
        //    foreach (PropertyInfo p in plst)
        //    {
        //        if (p.Name == "Id" || p.Name == "IdInt")
        //            continue;
        //        try
        //        {
        //            object memfrom = p.GetValue(from, null);
        //            if (memfrom is IMember)
        //            {
        //                object memthis = p.GetValue(this, null);
        //                if (!(memthis is MemberTimeStamp))
        //                    ((IMember)memthis).Clone((IMember)memfrom);
        //            }
        //        }
        //        catch (Exception)
        //        { }
        //    }
        //}
        //public virtual void Clone(RowSelectAbstract from, Type mutualBase)
        //{
        //    bool found = false;
        //    Type etFrom = from.GetType();
        //    while (etFrom != null)
        //    {
        //        if (mutualBase.Name != etFrom.Name || mutualBase.Namespace != etFrom.Namespace)
        //        {
        //            etFrom = etFrom.BaseType;
        //        }
        //        else
        //        {
        //            found = true;
        //            break;
        //        }
        //    }
        //    if (!found)
        //    {
        //        throw new Exception("From type " + from.GetType().Name + " doesn't have base class of type " + mutualBase.Name);
        //    }
        //    found = false;
        //    Type et = this.GetType();
        //    while (et != null)
        //    {
        //        if (et.Name != mutualBase.Name || et.Namespace != mutualBase.Namespace)
        //        {
        //            et = et.BaseType;
        //        }
        //        else
        //        {
        //            found = true;
        //            break;
        //        }
        //    }
        //    if (!found)
        //    {
        //        throw new Exception("To type " + et.GetType().Name + " doesn't have base class of type " + mutualBase.Name);
        //    }
        //    this.CopyMemberProperties(from, mutualBase);
        //}
    }
}
