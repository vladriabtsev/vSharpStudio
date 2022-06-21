using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class PluginsGroupProjectSettings : IvPluginGroupProjectSettings
    {
        public const string GuidStatic = "5FFB5F5F-7B06-4AC2-899A-21E1E059472B";
        partial void OnInit()
        {
            this.Name = "PrjGrSet";
            this.Description = "vSharpStudio plugins group settings for projects";
            this.Version = "0.1";
        }
        [BrowsableAttribute(false)]
        public string Guid { get { return GuidStatic; } }
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
    }
}
