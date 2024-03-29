﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class PluginsGroupSolutionSettings : IvPluginGroupSettings
    {
        public const string GuidStatic = "1A2B31CD-E50F-490D-A5AD-485B14CF5A01";
        partial void OnCreated()
        {
            this.Name = "SolGrSet";
            this.Description = "vSharpStudio plugins group settings for solutions";
            this.Version = "0.1";
        }
        [Browsable(false)]
        public string Guid { get { return GuidStatic; } }
        [Browsable(false)]
        public string Name { get; private set; } = string.Empty;
        [Browsable(false)]
        public string Version { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        [Browsable(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = PluginsGroupSolutionSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public IvPluginGroupSettings GetPluginGroupSettingsVm(IAppSolution parent, string settings)
        {
            this.Parent = parent;
            if (string.IsNullOrWhiteSpace(settings))
                return new PluginsGroupSolutionSettings(parent);
            var proto = CommonUtils.ParseJson<proto_plugins_group_solution_settings>(settings, true);
            return PluginsGroupSolutionSettings.ConvertToVM(proto, new PluginsGroupSolutionSettings(parent));
        }
        public ValidationResult? ValidateSettings()
        {
            this.Validate();
            return this.ValidationResult;
        }
        public async Task<ValidationResult?> ValidateSettingsAsync()
        {
            await this.ValidateAsync();
            return this.ValidationResult;
        }
    }
}
