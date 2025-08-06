using System.Threading.Tasks;

namespace ViewModelBase
{
    public interface IValidatable
    {
        bool Validate();
        Task<bool> ValidateAsync();
    }
}
