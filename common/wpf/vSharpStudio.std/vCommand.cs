using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelBase
{
    public class vCommand : VmBindable, ICommand
    {
        public static vCommand Create(Action<object> execute, Predicate<object> canExecute)
        {
            return new vCommand(
                (o) =>
                {
                    execute(o);
                },
                (o) =>
                {
                    return canExecute(o);
                });
        }
        public static vCommand CreateAsync(Func<object, Task> execute, Predicate<object> canExecute)
        {
            return new vCommand(
                (o) =>
                {
                    return execute(o);
                },
                (o) =>
                {
                    return canExecute(o);
                });
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

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        private readonly Func<object, Task> _executeAsync;
        public vCommand(Action<object> execute)
           : this(execute, null)
        {
            _execute = execute;
        }
        public vCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }
        public vCommand(Func<object, Task> executeAsync, Predicate<object> canExecute)
        {
            if (executeAsync == null)
            {
                throw new ArgumentNullException("execute");
            }
            _executeAsync = executeAsync;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (this.Dispatcher == null) // to made visible all controls binded to visibility
                return true;
            if (_isexecuted)
                return false;
            if (_canExecute == null)
                return true;
            if (_canExecute(parameter))
            {
                //                this.Visibility = Visibility.Visible;
                return true;
            }
            //          this.Visibility = Visibility.Collapsed;
            return false;
        }
        public void Execute(object parameter)
        {
            _isexecuted = true;
            CanExecuteChangedInternal.Raise(this);
            try
            {
                _execute(parameter);
            }
            finally
            {
                _isexecuted = false;
                CanExecuteChangedInternal.Raise(this);
            }
        }
        async public Task ExecuteAsync(object parameter)
        {
            _isexecuted = true;
            CanExecuteChangedInternal.Raise(this);
            try
            {
                await _executeAsync(parameter);
            }
            finally
            {
                _isexecuted = false;
                CanExecuteChangedInternal.Raise(this);
            }
        }
        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked 
        public event EventHandler CanExecuteChanged
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
        private event EventHandler CanExecuteChangedInternal;
        public void RaiseCanExecuteChanged()
        {
            if (this.Dispatcher != null) // to exclude errors in Design mode
                CanExecuteChangedInternal.Raise(this);
        }
    }
}
