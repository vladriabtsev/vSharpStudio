using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginGeneratorValidator
    {
        public PluginGeneratorValidator()
        {
            this.RuleFor(x => x.Generator).Custom((plg, cntx) =>
            {
                if (plg == null)
                {
                    var pg = (PluginGenerator)cntx.InstanceToValidate;
                    Debug.Assert(pg.Parent != null);
                    var sb = new StringBuilder();
                    sb.Append("Generator '");
                    sb.Append(pg.Name);
                    sb.Append("' of plugin '");
                    sb.Append(pg.ParentPlugin.Name);
                    sb.Append("' is not found");
                    var flr = new ValidationFailure();
                    flr.ErrorMessage = sb.ToString();
                    flr.Severity = Severity.Info;
                    flr.PropertyName = cntx.PropertyPath;
                    cntx.AddFailure(flr);
                }
            });
            this.RuleFor(x => x.Guid).NotEmpty().WithMessage(Config.ValidationMessages.GUID_IS_EMPTY);
            this.RuleFor(x => x.Guid).Custom((guid, cntx) =>
            {
                PluginGenerator pg = (PluginGenerator)cntx.InstanceToValidate;
                if (pg.Parent == null)
                {
                    return;
                }

                Debug.Assert(pg.Parent != null);
                Debug.Assert(pg.Parent.Parent != null);
                GroupListPlugins lst = (GroupListPlugins)pg.Parent.Parent;
                StringBuilder sb = new StringBuilder();
                sb.Append("Not unique Generator Guid. Plugin/Generator: ");
                int count = 0;
                string sep = "";
                foreach (var tt in lst.ListPlugins)
                {
                    foreach (var t in tt.ListGenerators)
                    {
                        if (t.Guid == guid)
                        {
                            sb.Append(sep);
                            sb.Append(tt.Name);
                            sb.Append("/");
                            sb.Append(t.Name);
                            sep = ", ";
                            count++;
                        }
                    }
                }
                if (count > 1)
                {
                    sb.Append(". Remove extra plugings");
                    cntx.AddFailure(sb.ToString());
                }
            });
            this.RuleFor(x => x).Custom((name, cntx) =>
            {
                PluginGenerator pg = (PluginGenerator)cntx.InstanceToValidate;
                if (pg.Generator == null)
                {
                    return;
                }
                switch (pg.Generator.PluginGeneratorType)
                {
                    case common.vPluginLayerTypeEnum.DbDesign:
                        if (!(pg.Generator is IvPluginDbGenerator))
                        {
                            cntx.AddFailure("Interface 'IDbMigrator' is not implemented. Remove or fix Plugin: " + pg.Name);
                        }
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
