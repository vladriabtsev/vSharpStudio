using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace vSharpStudio.common
{
    public interface IvPluginGeneratorValidatableSettings
    {
        ValidationResult ValidateSettings();
        Task<ValidationResult> ValidateSettingsAsync();
    }
}
