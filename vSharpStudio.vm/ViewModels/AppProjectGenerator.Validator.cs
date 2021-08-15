﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;
using FluentValidation.Results;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProjectGeneratorValidator
    {
        public AppProjectGeneratorValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //this.RuleFor(x => x.FilePathPrivateConnStr).Must((o, file) => { return o.IsPrivateConnStr && string.IsNullOrWhiteSpace(o.FilePathPrivateConnStr); })
            //    .WithMessage("File path is not selected");
            //this.RuleFor(x => x.FilePathPrivateConnStr)
            //    .Custom((file, cntx) =>
            //    {
            //        var pg = (AppProjectGenerator)cntx.InstanceToValidate;
            //        var dir = Path.GetDirectoryName(Path.GetFullPath(file));
            //        if (!string.IsNullOrEmpty(file) && !Directory.Exists(dir))
            //        {
            //            cntx.AddFailure("Folder doesn't exists: " + dir);
            //        }
            //    });
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
                                    if (ttt.PluginDbGenerator != null)
                                        continue;
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
            this.RuleFor(x => x.PluginGeneratorGuid)
                .Custom((guid, cntx) =>
                {
                    if (string.IsNullOrWhiteSpace(guid))
                        return;
                    var pg = (AppProjectGenerator)cntx.InstanceToValidate;
                    var lst = pg.PluginGenerator.ValidateOnSelection(pg.AppProject);
                    AddValidationResults(cntx, lst);
                });
            this.RuleFor(x => x.ConnStr)
                .Custom((connStr, cntx) =>
                {
                    if (!string.IsNullOrEmpty(connStr))
                    {
                        var pg = (AppProjectGenerator)cntx.InstanceToValidate;
                        if (pg.PluginDbGenerator == null)
                            return;
                        var cfg = (Config)pg.GetConfig();
                        var lst = pg.PluginDbGenerator.ValidateDbModel(connStr, cfg, pg.Guid);
                        AddValidationResults(cntx, lst);
                    }
                });
            this.RuleFor(x => x.DynamicGeneratorSettings)
                .Custom((settings, cntx) =>
                {
                    if (settings != null && settings is IValidatableWithSeverity)
                    {
                        var m = (IValidatableWithSeverity)settings;
                        m.Validate();
                        foreach (var t in m.ValidationCollection)
                        {
                            var r = new ValidationFailure(cntx.PropertyName, t.Message);
                            switch (t.Severity)
                            {
                                case Severity.Error:
                                    r.Severity = Severity.Error;
                                    break;
                                case Severity.Warning:
                                    r.Severity = Severity.Warning;
                                    break;
                                case Severity.Info:
                                    r.Severity = Severity.Info;
                                    break;
                                default:
                                    throw new Exception();
                            }
                            cntx.AddFailure(r);
                        }
                    }
                });
        }
        public static void AddValidationResults(ValidationContext<AppProjectGenerator> cntx, List<ValidationPluginMessage> lst)
        {
            foreach (var t in lst)
            {
                var r = new ValidationFailure(cntx.PropertyName, t.Message);
                switch (t.Level)
                {
                    case ValidationPluginMessage.EnumValidationMessage.Error:
                        r.Severity = Severity.Error;
                        break;
                    case ValidationPluginMessage.EnumValidationMessage.Warning:
                        r.Severity = Severity.Warning;
                        break;
                    case ValidationPluginMessage.EnumValidationMessage.Info:
                        r.Severity = Severity.Info;
                        break;
                    default:
                        throw new Exception();
                }
                cntx.AddFailure(r);
            }
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

            var p = val.AppProject;
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
