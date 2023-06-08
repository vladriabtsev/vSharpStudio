using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace vSharpStudio.wpf.Command
{
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/march/async-programming-patterns-for-asynchronous-mvvm-applications-data-binding
    public class AsyncCommand<TResult> : AsyncCommandBase, INotifyPropertyChanged
    {
        private readonly Func<Task<TResult>> _command;
        private readonly Predicate<object> _canExecute;
        private readonly CancelAsyncCommand _cancelCommand;
        private readonly NotifyTaskCompletion<TResult> _execution;

        //var propertyChanged = PropertyChanged;
        //    if (propertyChanged == null)
        //        return;
        //    propertyChanged(this, new PropertyChangedEventArgs("Status"));
        public event PropertyChangedEventHandler PropertyChanged;

        public AsyncCommand(Func<Task<TResult>> command)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
        }
        public AsyncCommand(Func<Task<TResult>> command, Predicate<object> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
            _cancelCommand = new CancelAsyncCommand();
        }
        public override bool CanExecute(object parameter)
        {
            //return true; // multi commands
            return Execution == null || Execution.IsCompleted; // only one command
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion<TResult>(_command());
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }
        //
        // Summary:
        //     Raises the CanExecuteChanged event.
        public void InvalidateCanExecute()
        {
            this.RaiseCanExecuteChanged();
        }
        // Raises PropertyChanged
        public NotifyTaskCompletion<TResult> Execution { get; private set; }

        #region Cancellation
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
            void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
        #endregion Cancellation
    }
}
//public sealed class MainWindowViewModel : INotifyPropertyChanged
//{
//    public MainWindowViewModel()
//    {
//        Url = "http://www.example.com/";
//        CountUrlBytesCommand = new AsyncCommand<int>(() =>
//          MyService.DownloadAndCountBytesAsync(Url));
//    }
//    // Raises PropertyChanged
//    public string Url { get; set; }
//    public IAsyncCommand CountUrlBytesCommand { get; private set; }
//}
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
//<Grid>
//  <TextBox Text = "{Binding Url}" />
//  < Button Command="{Binding CountUrlBytesCommand}" Content="Go" />
//  <Button Command = "{Binding CountUrlBytesCommand.CancelCommand}" Content="Cancel" />
//  <Grid Visibility = "{Binding CountUrlBytesCommand.Execution,
//    Converter={StaticResource NullToVisibilityConverter}}">
//    <!--Busy indicator-->
//    <Label Content = "Loading..."
//      Visibility="{Binding CountUrlBytesCommand.Execution.IsNotCompleted,
//      Converter={StaticResource BooleanToVisibilityConverter}}" />
//    <!--Results-->
//    <Label Content = "{Binding CountUrlBytesCommand.Execution.Result}"
//      Visibility="{Binding CountUrlBytesCommand.Execution.IsSuccessfullyCompleted,
//      Converter={StaticResource BooleanToVisibilityConverter}}" />
//    <!--Error details-->
//    <Label Content = "{Binding CountUrlBytesCommand.Execution.ErrorMessage}"
//      Visibility="{Binding CountUrlBytesCommand.Execution.IsFaulted,
//      Converter={StaticResource BooleanToVisibilityConverter}}" Foreground="Red" />
//    <!--Canceled-->
//    <Label Content = "Canceled"
//      Visibility="{Binding CountUrlBytesCommand.Execution.IsCanceled,
//      Converter={StaticResource BooleanToVisibilityConverter}}" Foreground="Blue" />
//  </Grid>
//</Grid>
