using System;
using System.Collections.Generic;
using System.Text;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public class GeneratorDbAccess : IvPluginGenerator
    {
        public GeneratorDbAccess()
        {
            this.DicPathTypes = new Dictionary<string, List<string>>();
            var lst = new List<string>();
            this.DicPathTypes["Property"] = lst;
            lst.Add(typeof(GeneratorDbAccessNodePropertySettings).FullName);
            lst = new List<string>();
            this.DicPathTypes["Catalog.*.Form"] = lst;
            lst.Add(typeof(GeneratorDbAccessNodeCatalogFormSettings).FullName);
        }
        public string Guid => "7C2902AF-DF34-46FC-8911-A48EE7F9B2B0";
        public string Name => "DbAccess";
        public string NameUi => "Db Access Layer";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Db Access Layer";
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbAccess;
        private GeneratorDbAccessSettings gen_settings = null;
        public IvPluginGeneratorSettingsVM GetAppGenerationSettingsVmFromJson(string settings)
        {
            if (gen_settings != null)
                return gen_settings;
            if (string.IsNullOrWhiteSpace(settings))
                gen_settings = new GeneratorDbAccessSettings();
            else
            {
                proto_generator_db_access_settings proto = proto_generator_db_access_settings.Parser.ParseJson(settings);
                gen_settings = GeneratorDbAccessSettings.ConvertToVM(proto, new GeneratorDbAccessSettings());
            }
            return gen_settings;
        }
        public Dictionary<string, List<string>> DicPathTypes { get; private set; }
        public IvPluginGeneratorSettingsVM GetNodeGenerationSettingsVmFromJson(string fullTypeName, string settings)
        {
            if (fullTypeName == typeof(GeneratorDbAccessNodePropertySettings).FullName)
            {
                if (string.IsNullOrWhiteSpace(settings))
                    return new GeneratorDbAccessNodePropertySettings(null);
                proto_generator_db_access_node_property_settings proto = proto_generator_db_access_node_property_settings.Parser.ParseJson(settings);
                return GeneratorDbAccessNodePropertySettings.ConvertToVM(proto, new GeneratorDbAccessNodePropertySettings((ITreeConfigNode)null));
            }
            else if (fullTypeName == typeof(GeneratorDbAccessNodeCatalogFormSettings).FullName)
            {
                if (string.IsNullOrWhiteSpace(settings))
                    return new GeneratorDbAccessNodeCatalogFormSettings(null);
                proto_generator_db_access_node_catalog_form_settings proto = proto_generator_db_access_node_catalog_form_settings.Parser.ParseJson(settings);
                return GeneratorDbAccessNodeCatalogFormSettings.ConvertToVM(proto, new GeneratorDbAccessNodeCatalogFormSettings((ITreeConfigNode)null));
            }
            throw new ArgumentException("Unsupported type: " + fullTypeName);
        }
    }
}
