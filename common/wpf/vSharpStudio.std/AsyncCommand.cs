using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public interface IAsyncCommand : System.Windows.Input.ICommand
    {
        Task ExecuteAsync(object parameter);
    }
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        public abstract bool CanExecute(object parameter);
        public abstract Task ExecuteAsync(object parameter);
        public async void Execute(object parameter)
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
    }
    public class AsyncCommand<TResult> : AsyncCommandBase, INotifyPropertyChanged
    {
        private readonly Func<CancellationToken, Task<TResult>> _command;
        private readonly CancelAsyncCommand _cancelCommand;
        private readonly Predicate<object> _canExecute;
        // Raises PropertyChanged
        public AsyncCommand(Func<CancellationToken, Task<TResult>> command, Predicate<object> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
            _cancelCommand = new CancelAsyncCommand();
        }
        public override bool CanExecute(object parameter)
        {
            return (Execution == null || Execution.IsCompleted) && _canExecute(parameter);
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion<TResult>(_command(_cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }
        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }
        private sealed class CancelAsyncCommand : ICommand
        {
            private CancellationTokenSource _cts = new CancellationTokenSource();
            private bool _commandExecuting;
            public CancellationToken Token { get { return _cts.Token; } }
            public void NotifyCommandStarting()
            {
                _commandExecuting = true;
                if (!_cts.IsCancellationRequested)
                    return;
                _cts = new CancellationTokenSource();
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
        public NotifyTaskCompletion<TResult> Execution { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task<TResult> task)
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
            var propertyChanged = PropertyChanged;
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
                propertyChanged(this,
                  new PropertyChangedEventArgs(nameof(this.InnerException)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.ErrorMessage)));
            }
            else
            {
                propertyChanged(this,
                  new PropertyChangedEventArgs(nameof(this.IsSuccessfullyCompleted)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.Result)));
            }
        }
        public Task<TResult> TaskCompletion { get; private set; }
        public TResult Result
        {
            get
            {
                return (TaskCompletion.Status == TaskStatus.RanToCompletion) ? TaskCompletion.Result : default(TResult);
            }
        }
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
