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
                this.RuleFor(x => x.VPlugin).NotEmpty().WithMessage(Config.ValidationMessages.PLUGIN_WAS_NOT_FOUND);
                this.RuleFor(x => x.Guid).NotEmpty().WithMessage(Config.ValidationMessages.GUID_IS_EMPTY);
            }
        }
    }
}
