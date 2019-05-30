using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Plugin
    {
        public partial class PluginValidator
        {
            public PluginValidator()
            {
                RuleFor(x => x.VPlugin).NotEmpty().WithMessage(Config.ValidationMessages.PLUGIN_WAS_NOT_FOUND);
                RuleFor(x => x.Guid).NotEmpty().WithMessage(Config.ValidationMessages.GUID_IS_EMPTY);
            }
        }
    }
}
