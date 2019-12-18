using System;
using System.Collections.Generic;
using System.Text;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public class GeneratorDbAccess : IvPluginGenerator
    {
        public string Guid => "7C2902AF-DF34-46FC-8911-A48EE7F9B2B0";
        public string Name => "DbAccess";
        public string NameUi => "Db Access Layer";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Db Access Layer";
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbAccess;
        private GeneratorDbAccessSettings gen_settings = null;
        public IvPluginGeneratorSettingsVM GetGenerationSettingsMvvmFromJson(string settings)
        {
            if (gen_settings != null)
                return gen_settings;
            if (settings == null)
                gen_settings = new GeneratorDbAccessSettings();
            else
            {
                proto_generator_db_access_settings proto = proto_generator_db_access_settings.Parser.ParseJson(settings);
                gen_settings = GeneratorDbAccessSettings.ConvertToVM(proto, null);
            }
            return gen_settings;
        }
        public IvPluginGeneratorSettingsVM GetGlobalAppGenerationSettingsMvvmFromJson(string settings)
        {
            throw new NotImplementedException();
        }
    }
}
