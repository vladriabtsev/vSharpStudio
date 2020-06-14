using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProjectGenerator
    {
        public partial class AppProjectGeneratorValidator
        {
            public AppProjectGeneratorValidator()
            {
                this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                this.RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                this.RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
                //this.RuleFor(x => x.RelativePathToGenFolder)
                //    .NotEmpty()
                //    .WithMessage("Output generation folder is not selected");
                this.RuleFor(x => x.RelativePathToGenFolder)
                    .Custom((path, cntx) =>
                    {
                        var pg = (AppProjectGenerator)cntx.InstanceToValidate;
                        if (!string.IsNullOrEmpty(path) && !Directory.Exists(pg.GetGenerationFolderPath()))
                        {
                            cntx.AddFailure("Output generation folder was not found:" + pg.GetGenerationFolderPath());
                        }
                    });
                this.RuleFor(x => x.GenFileName)
                    .NotEmpty()
                    .WithMessage("Output file name is empty");
                this.RuleFor(x => x.GenFileName)
                    .Custom((file, cntx) =>
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            var pg = (AppProjectGenerator)cntx.InstanceToValidate;
                            var path = pg.GetGenerationFilePath();
                            var gs = (GroupListAppSolutions)pg.Parent.Parent.Parent;
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Files override each other . Generators: ");
                            int count = 0;
                            string sep = "";
                            foreach (var t in gs.ListAppSolutions)
                            {
                                foreach (var tt in t.ListAppProjects)
                                {
                                    foreach (var ttt in tt.ListAppProjectGenerators)
                                    {
                                        if (pg != ttt && ttt.GetGenerationFilePath() == path)
                                        {
                                            sb.Append(sep);
                                            sb.Append(t.Name);
                                            sb.Append(@"\");
                                            sb.Append(tt.Name);
                                            sb.Append(@"\");
                                            sb.Append(ttt.Name);
                                            sep = ", ";
                                            count++;
                                        }
                                    }
                                }
                            }
                            if (count > 1)
                            {
                                sb.Append(". Change generation file path");
                                cntx.AddFailure(sb.ToString());
                            }
                        }
                    });
                this.RuleFor(x => x.PluginGuid)
                    .NotEmpty()
                    .WithMessage("Plugin is not selected");
                this.RuleFor(x => x.PluginGeneratorGuid)
                    .NotEmpty()
                    .WithMessage("Generator is not selected");
            }

            private bool IsUnique(AppProjectGenerator val)
            {
                if (val.Parent == null)
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                {
                    return true;
                }

                AppProject p = (AppProject)val.Parent;
                foreach (var t in p.ListAppProjectGenerators)
                {
                    if ((val.Guid != t.Guid) && (val.Name == t.Name))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
