﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

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
        public static readonly string DefaultName = "Generator";
        [ExpandableObjectAttribute()]
        public object Settings
        {
            get
            {
                return this._Settings;
            }
            set
            {
                if (this._Settings != value)
                {
                    this._Settings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _Settings;
        public void CreateGenSettings()
        {
            OnPluginGeneratorGuidChanged();
        }
        partial void OnPluginGuidChanged()
        {
            this.PluginGeneratorGuid = "";
            Config cnfg = (Config)this.GetConfig();
            Plugin plg = (Plugin)cnfg.DicNodes[this.PluginGuid];
            EditorPluginSelection.ListGenerators.Clear();
            EditorPluginSelection.ListGenerators.AddRange(plg.ListGenerators);
            this.DescriptionPlugin = plg.Description;
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
            {
                this.GeneratorSettings = string.Empty;
                return;
            }
            Config cnfg = (Config)this.GetConfig();
            PluginGenerator gen = (PluginGenerator)cnfg.DicNodes[this.PluginGeneratorGuid];
            try
            {
                this.Settings = gen?.Generator?.GetGenerationSettingsMvvmFromJson(this.GeneratorSettings);
            }
            catch(Exception ex)
            {
                throw;
            }
            this.DescriptionGenerator = gen.Description;
        }
    }
}