using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodePropertySettings : IvPluginGeneratorNodeSettings
    {
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string Guid { get { return "AAF6D5CD-7199-4D93-BC29-EA662E296B20"; } }
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string Name { get { return "Properties"; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodePropertySettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        [BrowsableAttribute(false)]
        public string SettingsAsJsonDefault
        {
            get
            {
                if (settingsAsJsonDefault==null)
                {
                    var proto = GeneratorDbAccessNodePropertySettings.ConvertToProto(new GeneratorDbAccessNodePropertySettings());
                    settingsAsJsonDefault = JsonFormatter.Default.Format(proto);
                }
                return settingsAsJsonDefault;
            }
        }
        private static string settingsAsJsonDefault = null;
        public IvPluginGeneratorNodeSettings GetAppGenerationNodeSettingsVm(string settings, bool isRootModelNode = false)
        {
            if (string.IsNullOrWhiteSpace(settings))
            {
                var res = new GeneratorDbAccessNodePropertySettings();
                if (isRootModelNode)
                {
                    res.IsPropertyParam1 = true;
                }
                return res;
            }
            proto_generator_db_access_node_property_settings proto = proto_generator_db_access_node_property_settings.Parser.ParseJson(settings);
            return GeneratorDbAccessNodePropertySettings.ConvertToVM(proto, new GeneratorDbAccessNodePropertySettings());
        }
        public static string SearchPath = "Property";
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string SearchPathInModel { get { return SearchPath; } }
    }
}
