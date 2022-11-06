using System;
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
    public partial class PluginsGroupProjectSettings : IvPluginGroupSettings
    {
        //private PluginsGroupProjectSettings() : base(null, PluginsGroupProjectSettingsValidator.Validator) { }
        partial void OnCreated()
        {
            this.Name = "PrjGrSet";
            this.Description = "vSharpStudio plugins group settings for projects";
            this.Version = "0.1";
            this.IsGroupProjectParam1 = false;
        }
        [BrowsableAttribute(false)]
        public string Name { get; private set; }
        [BrowsableAttribute(false)]
        public string Version { get; private set; }
        public string Description { get; private set; }

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
            var proto = proto_plugins_group_project_settings.Parser.WithDiscardUnknownFields(true).ParseJson(settings);
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
