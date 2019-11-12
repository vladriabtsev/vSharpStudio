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
                this.RuleFor(x => x.Generator).NotEmpty().WithMessage(Config.ValidationMessages.PLUGIN_GENERATOR_WAS_NOT_FOUND);
                this.RuleFor(x => x.Guid).NotEmpty().WithMessage(Config.ValidationMessages.GUID_IS_NOT_UNIQUE);
                this.RuleFor(x => x.Guid).Must((x, guid) => {
                    if (x.Parent == null)
                    {
                        return true;
                    }

                    GroupListPlugins lst = (GroupListPlugins)x.Parent.Parent;
                    int count = 0;
                    foreach (var tt in lst.ListPlugins)
                    {
                        foreach (var t in tt.ListGenerators)
                        {
                            if (t.Guid == guid)
                            {
                                count++;
                            }
                        }
                    }
                    if (count > 1)
                    {
                        return false;
                    }

                    return true;
                }).WithMessage(Config.ValidationMessages.GUID_IS_EMPTY);
                string message = "wrong validation rule";
                this.RuleFor(x => x).Must((o, name) => {
                    if (o.Generator == null)
                    {
                        return true;
                    }

                    switch (o.Generator.PluginGeneratorType)
                    {
                        case common.vPluginLayerTypeEnum.DbDesign:
                            if (!(o.Generator is IvPluginDbGenerator))
                            {
                                message = "Interface 'IDbMigrator' is not implemented";
                                return false;
                            }
                            break;
                        default:
                            break;
                    }
                    return true;
                }).WithMessage(message);
            }
        }
    }
}
