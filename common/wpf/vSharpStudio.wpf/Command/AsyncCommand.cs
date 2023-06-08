using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.wpf.Command
{
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/march/async-programming-patterns-for-asynchronous-mvvm-applications-data-binding
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<Task> _command;
        private readonly Predicate<object> _canExecute;
        public AsyncCommand(Func<Task> command)
        {
            _command = command;
        }
        public AsyncCommand(Func<Task> command, Predicate<object> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }
        //
        // Summary:
        //     Raises the CanExecuteChanged event.
        public void InvalidateCanExecute()
        {
            this.RaiseCanExecuteChanged();
        }
    }
}
//<Grid>
//  <TextBox Text = "{Binding Url}" />
//  < Button Command="{Binding CountUrlBytesCommand}" 
//      Content="Go" />
//  <TextBlock Text = "{Binding ByteCount}" />
//</ Grid>
//public sealed class MainWindowViewModel : INotifyPropertyChanged
//{
//    public MainWindowViewModel()
//    {
//        Url = "http://www.example.com/";
//        CountUrlBytesCommand = new AsyncCommand(async () =>
//        {
//            ByteCount = await MyService.DownloadAndCountBytesAsync(Url);
//        });
//    }
//    public string Url { get; set; } // Raises PropertyChanged
//    public IAsyncCommand CountUrlBytesCommand { get; private set; }
//    public int ByteCount { get; private set; } // Raises PropertyChanged
//}
