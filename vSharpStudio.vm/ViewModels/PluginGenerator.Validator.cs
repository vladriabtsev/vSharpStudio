using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginGenerator
    {
        public partial class PluginGeneratorValidator
        {
            public PluginGeneratorValidator()
            {
                RuleFor(x => x.Generator).NotEmpty().WithMessage(Config.ValidationMessages.PLUGIN_GENERATOR_WAS_NOT_FOUND);
                RuleFor(x => x.Guid).NotEmpty().WithMessage(Config.ValidationMessages.GUID_IS_EMPTY);
                string message = "wrong validation rule";
                RuleFor(x => x).Must((o, name) => {
                    if (o.Generator == null)
                        return true;
                    switch(o.Generator.PluginType)
                    {
                        case common.vPluginTypeEnum.DbDesign:
                            if (o.Generator is IDbMigrator)
                                return true;
                            message = "Interface 'IDbMigrator' is not implemented";
                            break;
                        default:
                            break;
                    }
                    return false;
                }).WithMessage(message);
            }
        }
    }
}
