using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModelBase
{
    public class RelayCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _execute;
        //
        // Summary:
        //     Initializes a new instance of the DelegateCommand class.
        //
        // Parameters:
        //   execute:
        //     The execute action.
        public RelayCommand(Action<object> execute) : this(null, execute) { }
        //
        // Summary:
        //     Initializes a new instance of the DelegateCommand class.
        //
        // Parameters:
        //   execute:
        //     The execute action.
        //
        //   canExecute:
        //     The can execute predicate.
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        //
        // Summary:
        //     Occurs when changes occur that affect whether the command should execute.
        public event EventHandler CanExecuteChanged;
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}
        //
        // Summary:
        //     Defines the method that determines whether the command can execute in its current
        //     state.
        //
        // Parameters:
        //   parameter:
        //     Data used by the command. If the command does not require data to be passed,
        //     this object can be set to null.
        //
        // Returns:
        //     True if this command can be executed, otherwise - false.
        public bool CanExecute(object parameter) { return _canExecute(parameter); }
        //
        // Summary:
        //     Defines the method to be called when the command is invoked.
        //
        // Parameters:
        //   parameter:
        //     Data used by the command. If the command does not require data to be passed,
        //     this object can be set to null.
        public void Execute(object parameter) { _execute(parameter); }
        //
        // Summary:
        //     Raises the CanExecuteChanged event.
        public void InvalidateCanExecute() { CanExecuteChanged?.Invoke(this, EventArgs.Empty); }
    }
}
