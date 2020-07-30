using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class PluginsGroupSettings : IvPluginGroupSolutionSettings
    {
        partial void OnInit()
        {
            this.Guid = "BE281D79-3CBC-4211-B9AD-E580F8CEB731";
            this.Name = "GrSet";
            this.Description = "vSharpStudio plugins group settings";
            this.Version = "0.1";
        }
        [BrowsableAttribute(false)]
        public string Guid { get; private set; }
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
                var proto = PluginsGroupSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public IvPluginGroupSolutionSettings GetPluginGroupSolutionSettingsVm(string settings)
        {
            if (string.IsNullOrWhiteSpace(settings))
                return new PluginsGroupSettings();
            var proto = proto_plugins_group_settings.Parser.ParseJson(settings);
            return PluginsGroupSettings.ConvertToVM(proto, new PluginsGroupSettings());
        }
    }
}
