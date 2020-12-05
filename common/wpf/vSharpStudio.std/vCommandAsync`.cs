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
    public class vCommandAsync<TResult> : vCommandAsync
    {
        async static public Task<TResult> ExecuteFuncAsync(Func<TResult> func)
        {
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func();
            });
            return tsk;
        }
        async static public Task<TResult> ExecuteFuncAsync(ProgressVM progress, Func<ProgressVM, Action, TResult> func)
        {
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func(prgrs, () => { progress.From(prgrs); });
            });
            return tsk;
        }
        async static public Task<TResult> ExecuteFuncAsync(CancellationToken token, ProgressVM progress, Func<CancellationToken, ProgressVM, Action, TResult> func)
        {
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func(token, prgrs, () => { progress.From(prgrs); });
            });
            return tsk;
        }
        static public vCommandAsync<TResult> Create(Func<CancellationToken, TResult> command, Predicate<object> canExecute, CancellationTokenSource cts = null)
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            var asyncCommand = new vCommandAsync<TResult>((cmd) =>
            {
                var tsk = System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
                {
                    return command(cts.Token);
                });
                return tsk;
            }, canExecute, cts);
            return asyncCommand;
        }
        static public vCommandAsync<TResult> Create(ProgressVM progress, Func<CancellationToken, ProgressVM, Action, TResult> command, Predicate<object> canExecute, CancellationTokenSource cts = null)
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            var asyncCommand = new vCommandAsync<TResult>((cmd) =>
            {
                var tsk = System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
                {
                    return command(cts.Token, prgrs, () => { progress.From(prgrs); });
                });
                return tsk;
            }, canExecute, cts);
            return asyncCommand;
        }
        private readonly Func<CancellationToken, Task<TResult>> _command;
        public vCommandAsync(Func<CancellationToken, Task<TResult>> command, Predicate<object> canExecute, CancellationTokenSource cancellationTokenSource = null)
        {
            _command = command;
            _canExecute = canExecute;
            _cts = cancellationTokenSource;
            _cancelCommand = new CancelAsyncCommand(_cts);
        }
        override async public Task ExecuteAsync(object parameter, bool isCatchException = false)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion<TResult>(_command(_cancelCommand.Token));
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
        new public NotifyTaskCompletion<TResult> Execution { get; private set; }
        new public bool IsCanceled
        {
            get
            {
                return _cancelCommand.Token.IsCancellationRequested ||
                    this.Execution.IsCanceled;
            }
        }
        new public bool IsFaulted { get { return this.Execution.IsFaulted; } }
        new public AggregateException Exception { get { return this.Execution.Exception; } }
        new public Exception InnerException { get { return this.Execution.InnerException; } }
        new public string ErrorMessage { get { return this.Execution.ErrorMessage; } }
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
                propertyChanged(this, new PropertyChangedEventArgs(nameof(this.Result)));
            }
        }
        public Task<TResult> TaskCompletion { get; private set; }
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
        public TResult Result
        {
            get
            {
                return (TaskCompletion.Status == TaskStatus.RanToCompletion) ? TaskCompletion.Result : default(TResult);
            }
        }
    }
}
