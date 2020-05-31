using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodeSettings : IvPluginNodeSettings
    {
        public static string GuidStatic = "2511D2D7-020E-4481-BB38-08D4B1ECF083";
        [BrowsableAttribute(false)]
        public string Guid { get { return GeneratorDbAccessNodeSettings.GuidStatic; } }
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string Name { get { return "All"; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodeSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        [BrowsableAttribute(false)]
        public string SettingsAsJsonDefault
        {
            get
            {
                if (settingsAsJsonDefault == null)
                {
                    var proto = GeneratorDbAccessNodeSettings.ConvertToProto(new GeneratorDbAccessNodeSettings());
                    settingsAsJsonDefault = JsonFormatter.Default.Format(proto);
                }
                return settingsAsJsonDefault;
            }
        }
        private static string settingsAsJsonDefault = null;
        public IvPluginNodeSettings GetAppGenerationNodeSettingsVm(string settings)
        {
            if (string.IsNullOrWhiteSpace(settings))
                return new GeneratorDbAccessNodeSettings();
            proto_generator_db_access_node_settings proto = proto_generator_db_access_node_settings.Parser.ParseJson(settings);
            return GeneratorDbAccessNodeSettings.ConvertToVM(proto, new GeneratorDbAccessNodeSettings());
        }
        public static string SearchPath = "";
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string SearchPathInModel { get { return SearchPath; } }
    }
}
