using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodePropertySettings : IvPluginNodeSettings
    {
        [ReadOnly(true)]
        public string Guid { get { return "AAF6D5CD-7199-4D93-BC29-EA662E296B20"; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodePropertySettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        public IvPluginNodeSettings GetAppGenerationNodeSettingsVm(string settings)
        {
            if (string.IsNullOrWhiteSpace(settings))
                return new GeneratorDbAccessNodePropertySettings();
            proto_generator_db_access_node_property_settings proto = proto_generator_db_access_node_property_settings.Parser.ParseJson(settings);
            return GeneratorDbAccessNodePropertySettings.ConvertToVM(proto, new GeneratorDbAccessNodePropertySettings());
        }
        public static string SearchPath = "Property";
        [ReadOnly(true)]
        public string SearchPathInModel { get { return SearchPath; } }
    }
}
