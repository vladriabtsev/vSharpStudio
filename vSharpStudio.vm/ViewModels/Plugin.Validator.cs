using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginValidator
    {
        public PluginValidator()
        {
            this.RuleFor(x => x.VPlugin).NotEmpty().WithMessage(Config.ValidationMessages.PLUGIN_WAS_NOT_FOUND);
            this.RuleFor(x => x.Guid).NotEmpty().WithMessage(Config.ValidationMessages.GUID_IS_EMPTY);
            this.RuleFor(x => x.Guid).Custom((guid, cntx) =>
            {
                var pg = (Plugin)cntx.InstanceToValidate;
                Debug.Assert(pg.Parent != null); 
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
            //this.RuleFor(x => x.Guid)
            //    .Custom((file, cntx) =>
            //    {
            //        if (!string.IsNullOrEmpty(file))
            //        {
            //            var pg = (AppProjectGenerator)cntx.InstanceToValidate;
            //            var path = pg.GetGenerationFilePath();
            //            var gs = (GroupListAppSolutions)pg.Parent.Parent.Parent;
            //            StringBuilder sb = new StringBuilder();
            //            sb.Append("Files override each other . Generators: ");
            //            int count = 0;
            //            string sep = "";
            //            foreach (var t in gs.ListAppSolutions)
            //            {
            //                foreach (var tt in t.ListAppProjects)
            //                {
            //                    foreach (var ttt in tt.ListAppProjectGenerators)
            //                    {
            //                        if (pg != ttt && ttt.GetGenerationFilePath() == path)
            //                        {
            //                            sb.Append(sep);
            //                            sb.Append(t.Name);
            //                            sb.Append(@"\");
            //                            sb.Append(tt.Name);
            //                            sb.Append(@"\");
            //                            sb.Append(ttt.Name);
            //                            sep = ", ";
            //                            count++;
            //                        }
            //                    }
            //                }
            //            }
            //            if (count > 1)
            //            {
            //                sb.Append(". Change generation file path");
            //                cntx.AddFailure(sb.ToString());
            //            }
            //        }
            //    });
            // unique guid for generator

            // unique guid for generator node settings
        }
    }
}
