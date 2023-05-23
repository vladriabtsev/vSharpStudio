using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        public static async Task<TResult> ExecuteFuncAsync(Func<TResult> func)
        {
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func();
            });
            return tsk;
        }
        public static async Task<TResult> ExecuteFuncAsync(IBusy model, Func<TResult> func)
        {
            model.IsBusy = true;
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func();
            });
            model.IsBusy = false;
            return tsk;
        }
        public static async Task<TResult> ExecuteFuncAsync(Func<TResult> func, IBusy model)
        {
            model.IsBusy = true;
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func();
            });
            model.IsBusy = false;
            return tsk;
        }
        public static async Task<TResult> ExecuteFuncAsync(ProgressVM progress, Func<ProgressVM, Action, TResult> func)
        {
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func(prgrs, () => { progress.From(prgrs); });
            });
            return tsk;
        }
        public static async Task<TResult> ExecuteFuncAsync(CancellationToken token, ProgressVM progress, Func<CancellationToken, ProgressVM, Action, TResult> func)
        {
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
            {
                return func(token, prgrs, () => { progress.From(prgrs); });
            });
            return tsk;
        }
        public static vCommandAsync<TResult> Create(Func<TResult> command, Predicate<object?> canExecute)
        {
            var asyncCommand = new vCommandAsync<TResult>(async (cmd) =>
            {
                var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
                {
                    return command();
                });
                return tsk;
            }, canExecute);
            return asyncCommand;
        }
        public static vCommandAsync<TResult> Create(IBusy model, Func<TResult> command, Predicate<object?> canExecute)
        {
            var asyncCommand = new vCommandAsync<TResult>(async (cmd) =>
            {
                model.IsBusy = true;
                var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
                {
                    return command();
                });
                model.IsBusy = false;
                return tsk;
            }, canExecute);
            return asyncCommand;
        }
        public static vCommandAsync<TResult> Create(Func<CancellationToken, TResult> command, Predicate<object?> canExecute, CancellationTokenSource? cts = null)
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            var asyncCommand = new vCommandAsync<TResult>(async (cmd) =>
            {
                var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
                {
                    return command(cts.Token);
                });
                return tsk;
            }, canExecute, cts);
            return asyncCommand;
        }
        public static vCommandAsync<TResult> Create(ProgressVM progress, Func<CancellationToken, ProgressVM, Action, TResult> command, Predicate<object?> canExecute, CancellationTokenSource? cts = null)
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            var prgrs = new ProgressVM();
            prgrs.From(progress);
            var asyncCommand = new vCommandAsync<TResult>(async (cmd) =>
            {
                var tsk = await System.Threading.Tasks.Task<TResult>.Factory.StartNew(() =>
                {
                    return command(cts.Token, prgrs, () => { progress.From(prgrs); });
                });
                return tsk;
            }, canExecute, cts);
            return asyncCommand;
        }
        private readonly Func<CancellationToken, Task<TResult>> _command;
        public vCommandAsync(Func<CancellationToken, Task<TResult>> command, Predicate<object?> canExecute, CancellationTokenSource? cancellationTokenSource = null)
        {
            _command = command;
            _canExecute = canExecute;
            _cts = cancellationTokenSource;
            _cancelCommand = new CancelAsyncCommand(_cts);
        }
        public override async Task ExecuteAsync(object? parameter, bool isCatchException = false)
        {
            Debug.Assert(_cancelCommand != null);
            _cancelCommand.NotifyCommandStarting();
            Debug.Assert(_cancelCommand.Token != null);
            Execution = new NotifyTaskCompletion<TResult>(_command(_cancelCommand.Token.Value));
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
        public new NotifyTaskCompletion<TResult>? Execution { get; private set; }
        public new bool IsCanceled
        {
            get
            {
                Debug.Assert(this.Execution != null);
                Debug.Assert(_cancelCommand != null);
                Debug.Assert(_cancelCommand.Token != null);
                return _cancelCommand.Token.Value.IsCancellationRequested || this.Execution.IsCanceled;
            }
        }
        public new bool IsFaulted
        {
            get
            {
                if (this.Execution==null)
                    return false;
                return this.Execution.IsFaulted;
            }
        }
        public new AggregateException? Exception
        {
            get
            {
                return this.Execution?.Exception;
            }
        }
        public new Exception? InnerException
        {
            get
            {
                return this.Execution?.InnerException;
            }
        }
        public new string? ErrorMessage
        {
            get
            {
                return this.Execution?.ErrorMessage;
            }
        }
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
        public AggregateException? Exception { get { return TaskCompletion.Exception; } }
        public Exception? InnerException
        {
            get
            {
                return (Exception == null) ? null : Exception.InnerException;
            }
        }
        public string? ErrorMessage
        {
            get
            {
                return (InnerException == null) ? null : InnerException.Message;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public TResult? Result
        {
            get
            {
                return (TaskCompletion.Status == TaskStatus.RanToCompletion) ? TaskCompletion.Result : default(TResult);
            }
        }
    }
}
