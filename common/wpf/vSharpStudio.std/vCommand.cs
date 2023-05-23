using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

// https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
// https://github.com/brminnick/AsyncAwaitBestPractices
// https://johnthiriet.com/mvvm-going-async-with-async-command/
namespace ViewModelBase
{
    public class vCommand : VmBindable, ICommand
    {
        public static vCommand Create(Action<object?> execute, Predicate<object?> canExecute)
        {
            return new vCommand((o) => { execute(o); }, (o) => { return canExecute(o); });
        }
        public static vCommand CreateAsync(Action<object?> execute, Predicate<object?> canExecute)
        {
            return new vCommand((o) => { Task.Run(() => { execute(o); }); }, (o) => { return canExecute(o); });
        }
        private bool _isexecuted = false;
        //public Visibility Visibility
        //{
        //    set
        //    {
        //        if (_Visibility != value)
        //        {
        //            _Visibility = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //    get { return _Visibility; }
        //}
        //private Visibility _Visibility = Visibility.Visible;

        private readonly Predicate<object?>? _canExecute;
        private readonly Action<object?> _execute;
        //private readonly Func<object, Task> _executeAsync;
        public vCommand(Action<object?> execute)
           : this(execute, null)
        {
            _execute = execute;
        }
        public vCommand(Action<object?> execute, Predicate<object?>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        //public vCommand(Func<object, Task> executeAsync, Predicate<object> canExecute)
        //{
        //    _executeAsync = executeAsync;
        //    _canExecute = canExecute;
        //}
        public bool CanExecute(object? parameter)
        {
            //if (this.Dispatcher == null) // to made visible all controls binded to visibility
            //    return true;
            if (_isexecuted)
                return false;
            if (_canExecute == null)
                return true;
            if (_canExecute(parameter))
            {
                //this.IsEnabled = true;
                //                this.Visibility = Visibility.Visible;
                return true;
            }
            //          this.Visibility = Visibility.Collapsed;
            //this.IsEnabled = false;
            return false;
        }
        public void Execute(object? parameter)
        {
            _isexecuted = true;
            if (this.CanExecuteChangedInternal != null)
                this.CanExecuteChangedInternal.Raise(this);
            try
            {
                //if (_execute != null)
                //{
                _execute(parameter);
                //}
                //else
                //{
                //    await _executeAsync(parameter);
                //}
            }
            finally
            {
                _isexecuted = false;
                if (this.CanExecuteChangedInternal != null)
                    this.CanExecuteChangedInternal.Raise(this);
            }
        }
        public async Task ExecuteAsync(object? parameter)
        {
            _isexecuted = true;
            if (this.CanExecuteChangedInternal != null)
                this.CanExecuteChangedInternal.Raise(this);
            try
            {
                await Task.Run(() =>
                {
                    _execute(parameter);
                });
                //await _executeAsync(parameter);
            }
            finally
            {
                _isexecuted = false;
                if (this.CanExecuteChangedInternal != null)
                    this.CanExecuteChangedInternal.Raise(this);
            }
        }
        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked 
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                //CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                //CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }
        private event EventHandler? CanExecuteChangedInternal;
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChangedInternal != null) // to exclude errors in Design mode
                CanExecuteChangedInternal.Raise(this);
        }
        //public bool IsEnabled
        //{
        //    get { return this._IsEnabled; }
        //    set
        //    {
        //        if (SetProperty<bool>(ref this._IsEnabled, value))
        //        {
        //            if (this._IsEnabled) this.Visibility = Visibility.Visible; else this.Visibility = Visibility.Hidden;
        //        }
        //    }
        //}
        //private bool _IsEnabled;
        //public Visibility Visibility
        //{
        //    get { return this._Visibility; }
        //    set { SetProperty<Visibility>(ref this._Visibility, value); }
        //}
        //private Visibility _Visibility;
        public string? ToolTipText
        {
            get { return this._ToolTipText; }
            set { SetProperty<string?>(ref this._ToolTipText, value); }
        }
        private string? _ToolTipText;
    }
}
