using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.TextFormatting;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Diagnostics;
using System.Diagnostics;
using System.Windows.Media;

namespace ViewModelBase
{
    public partial class vButtonVM : vButtonVmBase
    {
        private Action execute;
        private Func<bool> canExecute;
        private Action executeInternal => () =>
        {
            this.IsEnabled = false;
            this.execute();
            this.IsEnabled = this.canExecute();
        };
        private Func<bool> canExecuteInternal => () =>
        {
            this.IsEnabled = this.canExecute();
            return this.IsEnabled;
        };
        public vButtonVM(Action execute, Func<bool> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            ArgumentNullException.ThrowIfNull(canExecute);
            this.execute = execute;
            this.canExecute = canExecute;
            this.Command = new RelayCommand(executeInternal, canExecuteInternal);
        }
        //public vButtonVM(string iconExecute, string iconCantExecute, Action execute, Func<bool> canExecute)
        //    : base(iconExecute, iconCantExecute)
        //{
        //    ArgumentNullException.ThrowIfNull(execute);
        //    ArgumentNullException.ThrowIfNull(canExecute);
        //    this.execute = execute;
        //    this.canExecute = canExecute;
        //    this.Command = new RelayCommand(executeInternal, canExecuteInternal);
        //}
        public void Execute()
        {
            this.Command.Execute(null);
        }
        public bool CanExecute()
        {
            return this.canExecute.Invoke();
        }
        public RelayCommand Command { get; private set; }
    }
    public partial class vButtonVM<T> : vButtonVmBase
    {
        private Action<T?> execute;
        private Predicate<T?> canExecute;
        private Action<T?> executeInternal => (o) =>
        {
            this.IsEnabled = false;
            this.execute(o);
            this.IsEnabled = this.canExecute(o);
        };
        private Predicate<T?> canExecuteInternal => (o) =>
        {
            this.IsEnabled = this.canExecute(o);
            return this.IsEnabled;
        };
        public vButtonVM(Action<T?> execute, Predicate<T?> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            ArgumentNullException.ThrowIfNull(canExecute);
            this.execute = execute;
            this.canExecute = canExecute;
            this.Command = new RelayCommand<T?>(executeInternal, canExecuteInternal);
        }
        //public vButtonVM(string iconExecute, string iconCantExecute, Action<T?> execute, Predicate<T?> canExecute)
        //    : base(iconExecute, iconCantExecute)
        //{
        //    ArgumentNullException.ThrowIfNull(execute);
        //    ArgumentNullException.ThrowIfNull(canExecute);
        //    this.execute = execute;
        //    this.canExecute = canExecute;
        //    this.Command = new RelayCommand<T?>(executeInternal, canExecuteInternal);
        //}
        public void Execute(T? o = default(T))
        {
            this.Command.Execute(o);
        }
        public bool CanExecute(T? o = default(T))
        {
            return this.canExecute.Invoke(o);
        }
        public RelayCommand<T?> Command { get; private set; }
    }
    public partial class vButtonVmAsync<T> : vButtonVmBase
    {
        private Func<T?, Task> execute;
        private Predicate<T?> canExecute;
        private Func<T?, Task> executeInternal => async (o) =>
        {
            this.IsEnabled = false;
            await this.execute(o);
            this.IsEnabled = this.canExecute(o);
        };
        private Predicate<T> canExecuteInternal => (o) =>
        {
            this.IsEnabled = this.canExecute(o);
            return this.IsEnabled;
        };
        public vButtonVmAsync(Func<T?, Task> execute, Predicate<T?> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            ArgumentNullException.ThrowIfNull(canExecute);
            this.execute = execute;
            this.canExecute = canExecute;
            this.Command = new AsyncRelayCommand<T?>(executeInternal, canExecuteInternal);
        }
        //public vButtonVmAsync(string iconExecute, string iconCantExecute, Func<T?, Task> execute, Predicate<T?> canExecute)
        //    : base(iconExecute, iconCantExecute)
        //{
        //    ArgumentNullException.ThrowIfNull(execute);
        //    ArgumentNullException.ThrowIfNull(canExecute);
        //    this.execute = execute;
        //    this.canExecute = canExecute;
        //    this.Command = new AsyncRelayCommand<T?>(executeInternal, canExecuteInternal);
        //}
        public async Task ExecuteAsync(T? o = default(T))
        {
            await this.Command.ExecuteAsync(o);
        }
        public bool CanExecute(T? o = default(T))
        {
            return this.canExecute.Invoke(o);
        }
        public AsyncRelayCommand<T?> Command { get; private set; }
    }
    public partial class vButtonVmBase : ObservableObject
    {
        private SolidColorBrush backGround = new SolidColorBrush(Color.FromArgb(0xFF, 0x22, 0x22, 0x22));
        [ObservableProperty]
        private SolidColorBrush? backGroundDisabled;
        //private string iconExecute = "iconNewFile";
        //private string iconCantExecute;
        //public vButtonVmBase(string iconExecute, string iconCantExecute)
        //{
        //    this.iconExecute = iconExecute;
        //    this.iconCantExecute = iconCantExecute;
        //    this.IconControlTemplate = this.GetIconControlTemplate(iconExecute);
        //}
        [ObservableProperty]
        private string text;
        [ObservableProperty]
        private string isBusy;
        [ObservableProperty]
        private string toolTipText;
        [ObservableProperty]
        private ControlTemplate iconControlTemplate;
        public bool IsEnabled
        {
            get => isEnabled;
            internal set
            {
                SetProperty(ref isEnabled, value);
                //if (IsEnabled)
                //{
                //    this.BackGroundDisabled = null;
                //}
                //else
                //{
                //    this.BackGroundDisabled = backGround;
                //}
                //if (IsEnabled)
                //{
                //    this.IconControlTemplate = this.GetIconControlTemplate(this.iconExecute);
                //}
                //else
                //{
                //    this.IconControlTemplate = this.GetIconControlTemplate(this.iconCantExecute);
                //}
            }
        }
        private bool isEnabled;
        private ControlTemplate GetIconControlTemplate(string iconResourceKey)
        {
#if DEBUG
            if (Application.Current==null)
                return null;
#endif
            Guard.IsNotNullOrWhiteSpace(iconResourceKey);
            ControlTemplate? res = null;
            var obj = Application.Current.TryFindResource(iconResourceKey);
            if (obj == null)
                ThrowHelper.ThrowArgumentException($"Resource '{iconResourceKey}' is not found", nameof(iconResourceKey));
            res = obj as ControlTemplate;
            if (res == null)
                ThrowHelper.ThrowArgumentException($"DataTemplate resource type is expected for resource '{iconResourceKey}'", nameof(iconResourceKey));
            return res;
        }
    }
}
