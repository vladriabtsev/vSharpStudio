using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Google.Protobuf;
using Proto.Plugin;
using vPlugin.Shared;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class PluginsGroupProjectSettings : IvPluginGroupSettings
    {
        //private PluginsGroupProjectSettings() : base(null, PluginsGroupProjectSettingsValidator.Validator) { }
        public const string GuidStatic = "89F0CCC4-492A-47BE-8075-28E4E71831F4";
        partial void OnCreated()
        {
            this.Name = "PrjGrSet";
            this.Description = "vSharpStudio plugins group settings for projects";
            this.Version = "0.1";
            this.IsGroupProjectParam1 = false;
        }
        [BrowsableAttribute(false)]
        public string Guid { get { return GuidStatic; } }
        [BrowsableAttribute(false)]
        public string Name { get; private set; } = string.Empty;
        [BrowsableAttribute(false)]
        public string Version { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = PluginsGroupProjectSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public IvPluginGroupSettings GetPluginGroupSettingsVm(IAppSolution parent, string settings)
        {
            this.Parent = parent;
            if (string.IsNullOrWhiteSpace(settings))
                return new PluginsGroupProjectSettings(parent);
            var proto = CommonUtils.ParseJson<proto_plugins_group_project_settings>(settings, true);
            return PluginsGroupProjectSettings.ConvertToVM(proto, new PluginsGroupProjectSettings(parent));
        }
        public ValidationResult ValidateSettings()
        {
            this.Validate();
            return this.ValidationResult;
        }
        public async Task<ValidationResult> ValidateSettingsAsync()
        {
            await this.ValidateAsync();
            return this.ValidationResult;
        }
    }
}
