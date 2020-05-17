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
                this.RuleFor(x => x.Guid).Custom((guid, cntx) =>
                {
                    Plugin pg = (Plugin)cntx.InstanceToValidate;
                    GroupListPlugins lst = (GroupListPlugins)pg.Parent;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Not unique Plugin Guid. Plugins: ");
                    int count = 0;
                    string sep = "";
                    foreach (var t in lst.ListPlugins)
                    {
                            if (t.Guid == guid)
                            {
                                sb.Append(sep);
                                sb.Append(t.Name);
                                sep = ", ";
                                count++;
                            }
                    }
                    if (count > 1)
                    {
                        sb.Append(". Remove extra plugings");
                        cntx.AddFailure(sb.ToString());
                    }
                });
            }
        }
    }
}
