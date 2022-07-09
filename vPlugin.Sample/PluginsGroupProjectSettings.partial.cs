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
    public partial class PluginsGroupProjectSettings : IvPluginGroupProjectSettings
    {
        partial void OnInit()
        {
            this.Name = "PrjGrSet";
            this.Description = "vSharpStudio plugins group settings for projects";
            this.Version = "0.1";
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

        public IvPluginGroupProjectSettings GetPluginGroupProjectSettingsVm(string settings)
        {
            if (string.IsNullOrWhiteSpace(settings))
                return new PluginsGroupProjectSettings();
            var proto = proto_plugins_group_project_settings.Parser.ParseJson(settings);
            return PluginsGroupProjectSettings.ConvertToVM(proto, new PluginsGroupProjectSettings());
        }
        public ValidationResult ValidateSettings()
        {
            this.Validate();
            return this.ValidationResult;
        }
        public Task<ValidationResult> ValidateSettingsAsync()
        {
            return Task.FromResult(this.ValidationResult);
        }
    }
}
