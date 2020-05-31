using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodeCatalogFormSettings : IvPluginNodeSettings
    {
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string Guid { get { return "91014E62-2F0E-451A-AB64-E642FD5577BD"; } }
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string Name { get { return "CatalogsForms"; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodeCatalogFormSettings.ConvertToProto(this);
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
                    var proto = GeneratorDbAccessNodeCatalogFormSettings.ConvertToProto(new GeneratorDbAccessNodeCatalogFormSettings());
                    settingsAsJsonDefault = JsonFormatter.Default.Format(proto);
                }
                return settingsAsJsonDefault;
            }
        }
        private static string settingsAsJsonDefault = null;
        public IvPluginNodeSettings GetAppGenerationNodeSettingsVm(string settings)
        {
            if (string.IsNullOrWhiteSpace(settings))
                return new GeneratorDbAccessNodeCatalogFormSettings();
            proto_generator_db_access_node_catalog_form_settings proto = proto_generator_db_access_node_catalog_form_settings.Parser.ParseJson(settings);
            return GeneratorDbAccessNodeCatalogFormSettings.ConvertToVM(proto, new GeneratorDbAccessNodeCatalogFormSettings());
        }
        public static string SearchPath = "Catalog.*.Form";
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
        public string SearchPathInModel { get { return SearchPath; } }
    }
}
