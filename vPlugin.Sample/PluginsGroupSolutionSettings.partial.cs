using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class PluginsGroupSolutionSettings : IvPluginGroupSolutionSettings
    {
        public const string GuidStatic = "BE281D79-3CBC-4211-B9AD-E580F8CEB731";
        partial void OnInit()
        {
            this.Name = "SolGrSet";
            this.Description = "vSharpStudio plugins group settings for solutions";
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
                var proto = PluginsGroupSolutionSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public IvPluginGroupSolutionSettings GetPluginGroupSolutionSettingsVm(string settings)
        {
            if (string.IsNullOrWhiteSpace(settings))
                return new PluginsGroupSolutionSettings();
            var proto = proto_plugins_group_solution_settings.Parser.ParseJson(settings);
            return PluginsGroupSolutionSettings.ConvertToVM(proto, new PluginsGroupSolutionSettings());
        }
    }
}
