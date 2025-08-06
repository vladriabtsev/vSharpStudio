using System.Threading.Tasks;
using FluentValidation.Results;

namespace vSharpStudio.common
{
    public interface IvPluginGeneratorValidatableSettings
    {
        ValidationResult? ValidateSettings();
        Task<ValidationResult?> ValidateSettingsAsync();
    }
}
