using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

//<Grid>
//  <TextBox Text = "{Binding Url}" />
//  < Button Command="{Binding CountUrlBytesCommand}" Content="Go" />
//  <Grid Visibility = "{Binding CountUrlBytesCommand.Execution,
//    Converter={StaticResource NullToVisibilityConverter}}">
//    <!--Busy indicator-->
//    <Label Visibility = "{Binding CountUrlBytesCommand.Execution.IsNotCompleted,
//      Converter={StaticResource BooleanToVisibilityConverter}}"
//      Content="Loading..." />
//    <!--Results-->
//    <Label Content = "{Binding CountUrlBytesCommand.Execution.Result}"
//      Visibility="{Binding CountUrlBytesCommand.Execution.IsSuccessfullyCompleted,
//      Converter={StaticResource BooleanToVisibilityConverter}}" />
//    <!--Error details-->
//    <Label Content = "{Binding CountUrlBytesCommand.Execution.ErrorMessage}"
//      Visibility="{Binding CountUrlBytesCommand.Execution.IsFaulted,
//      Converter={StaticResource BooleanToVisibilityConverter}}" Foreground="Red" />
//  </Grid>
//</Grid>

// https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/march/async-programming-patterns-for-asynchronous-mvvm-applications-data-binding
// https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
// https://github.com/StephenCleary
// https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming
namespace ViewModelBase
{
    public interface IBusy
    {
        bool IsBusy { get; set; }
    }
    public interface IAsyncCommand : System.Windows.Input.ICommand
    {
        Task ExecuteAsync(object parameter, bool isCatchException = false);
    }
    abstract public class AsyncCommandBase : IAsyncCommand
    {
        abstract public bool CanExecute(object parameter);
        abstract public Task ExecuteAsync(object parameter, bool isCatchException = false);
        async public void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        public void InvalidateCanExecute()
        {
            this.RaiseCanExecuteChanged();
        }
    }
    public class vCommandAsync : AsyncCommandBase
    {
        async static public Task ExecuteActionAsync(Action action)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                action();
            });
        }
        async static public Task ExecuteActionAsync(IBusy model, Action action)
        {
            model.IsBusy = true;
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                action();
            });
            model.IsBusy = false;
        }
        async static public Task ExecuteActionAsync(CancellationToken token, ProgressVM progress, Action<CancellationToken, ProgressVM, Action> action)
        {
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                action(token, prgrs, () => { progress.From(prgrs); });
            });
        }
        async static public Task ExecuteActionAsync(ProgressVM progress, Action<ProgressVM, Action> action)
        {
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                action(prgrs, () => { progress.From(prgrs); });
            });
        }
        static public vCommandAsync Create(Action command, Predicate<object> canExecute)
        {
            var asyncCommand = new vCommandAsync((cmd) =>
            {
                var tsk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    command();
                });
                return tsk;
            }, canExecute);
            return asyncCommand;
        }
        static public vCommandAsync Create(IBusy model, Action command, Predicate<object> canExecute)
        {
            var asyncCommand = new vCommandAsync((cmd) =>
            {
                model.IsBusy = true;
                var tsk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    command();
                    model.IsBusy = false;
                });
                return tsk;
            }, canExecute);
            return asyncCommand;
        }
        static public vCommandAsync Create(Action<CancellationToken> command, Predicate<object> canExecute, CancellationTokenSource cts = null)
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            var asyncCommand = new vCommandAsync((cmd) =>
            {
                var tsk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    command(cts.Token);
                });
                return tsk;
            }, canExecute, cts);
            return asyncCommand;
        }
        static public vCommandAsync Create(ProgressVM progress, Action<CancellationToken, ProgressVM, Action> command, Predicate<object> canExecute, CancellationTokenSource cts = null)
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            var asyncCommand = new vCommandAsync((cmd) =>
            {
                var tsk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    command(cts.Token, prgrs, () => { progress.From(prgrs); });
                });
                return tsk;
            }, canExecute, cts);
            return asyncCommand;
        }
        protected CancellationTokenSource _cts;
        public CancellationTokenSource CancellationTokenSource { get { return _cts; } }
        private readonly Func<CancellationToken, Task> _command;
        protected CancelAsyncCommand _cancelCommand;
        protected Predicate<object> _canExecute;
        // Raises PropertyChanged
        protected vCommandAsync() { }
        public vCommandAsync(Func<CancellationToken, Task> command, Predicate<object> canExecute, CancellationTokenSource cts = null)
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            _command = command;
            _canExecute = canExecute;
            _cts = cts;
            _cancelCommand = new CancelAsyncCommand(_cts);
        }
        override public bool CanExecute(object parameter)
        {
            return (Execution == null || Execution.IsCompleted) && _canExecute(parameter);
        }
        override async public Task ExecuteAsync(object parameter, bool isCatchException = false)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_command(_cancelCommand.Token));
            RaiseCanExecuteChanged();
            if (isCatchException)
            {
                try
                {
                    await Execution.TaskCompletion;
                }
                catch { }
            }
            else
            {
                await Execution.TaskCompletion;
            }
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }
        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }
        public sealed class CancelAsyncCommand : ICommand
        {
            public CancelAsyncCommand(CancellationTokenSource cts)
            {
                _cts = cts;
            }
            private CancellationTokenSource _cts;
            private bool _commandExecuting;
            public CancellationToken Token { get { return _cts.Token; } }
            public void NotifyCommandStarting()
            {
                _commandExecuting = true;
                if (!_cts.IsCancellationRequested)
                    return;
                //_cts = new CancellationTokenSource();
                RaiseCanExecuteChanged();
            }
            public void NotifyCommandFinished()
            {
                _commandExecuting = false;
                RaiseCanExecuteChanged();
            }
            bool ICommand.CanExecute(object parameter)
            {
                return _commandExecuting && !_cts.IsCancellationRequested;
            }
            void ICommand.Execute(object parameter)
            {
                _cts.Cancel();
                RaiseCanExecuteChanged();
            }
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
            private void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public NotifyTaskCompletion Execution { get; private set; }
        public bool IsCanceled
        {
            get
            {
                return _cancelCommand.Token.IsCancellationRequested || 
                    this.Execution.IsCanceled;
            }
        }
        public bool IsFaulted { get { return this.Execution.IsFaulted; } }
        public AggregateException Exception { get { return this.Execution.Exception; } }
        public Exception InnerException { get { return this.Execution.InnerException; } }
        public string ErrorMessage { get { return this.Execution.ErrorMessage; } }
    }
    public sealed class NotifyTaskCompletion : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task task)
        {
            TaskCompletion = task;
            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }
        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch
            {
            }
            var propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged(this, new PropertyChangedEventArgs(nameof(this.Status)));
            propertyChanged(this, new PropertyChangedEventArgs(nameof(this.IsCompleted)));
            propertyChanged(this, new PropertyChangedEventArgs(nameof(this.IsNotCompleted)));
            if (task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.IsCanceled)));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.IsFaulted)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.Exception)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.InnerException)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.ErrorMessage)));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.IsSuccessfullyCompleted)));
            }
        }
        public Task TaskCompletion { get; private set; }
        public TaskStatus Status { get { return TaskCompletion.Status; } }
        public bool IsCompleted { get { return TaskCompletion.IsCompleted; } }
        public bool IsNotCompleted { get { return !TaskCompletion.IsCompleted; } }
        public bool IsSuccessfullyCompleted
        {
            get
            {
                return TaskCompletion.Status == TaskStatus.RanToCompletion;
            }
        }
        public bool IsCanceled { get { return TaskCompletion.IsCanceled; } }
        public bool IsFaulted { get { return TaskCompletion.IsFaulted; } }
        public AggregateException Exception { get { return TaskCompletion.Exception; } }
        public Exception InnerException
        {
            get
            {
                return (Exception == null) ? null : Exception.InnerException;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return (InnerException == null) ? null : InnerException.Message;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
