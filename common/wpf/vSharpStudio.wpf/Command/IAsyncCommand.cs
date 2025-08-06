using System.Threading.Tasks;
using System.Windows.Input;

namespace vSharpStudio.wpf.Command
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
