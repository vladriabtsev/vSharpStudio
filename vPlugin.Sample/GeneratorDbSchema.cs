using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Proto.Plugin;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;

namespace vPlugin.Sample
{
    public class GeneratorDbSchema : IvPluginDbGenerator
    {
        public GeneratorDbSchema()
        {
            this.DicPathTypes = new Dictionary<string, List<string>>();
        }
        public ILoggerFactory LoggerFactory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsStableDbConnection { get; set; }
        public string ProviderName { get; set; }
        public string Guid => "08744482-BE03-464B-81AB-DD482AB66103";
        public string Name => "AbstractDbSchema";
        public string NameUi => "Abstract Db Provider Name";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Abstract Db Schema";
        public string DbSchema => "";
        public string PKeyName => "";
        public string VersionFieldName => "";
        public ITreeConfigNode Parent { get; set; }
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbDesign;
        public void EnsureDbDeletedAndCreated(string connectionString)
        {
            throw new NotImplementedException();
        }
        public IvPluginGeneratorSettings GetConnectionStringMvvm(string connectionString)
        {
            var res = new DbConnectionStringSettings(connectionString);
            return res;
        }
        public object GetDbModel(string connectionString, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }
        public object GetDbModel(object context, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }
        public IvPluginGeneratorSettings GetAppGenerationSettingsVmFromJson(string settings)
        {
            var vm = new GeneratorDbSchemaSettings();
            if (!string.IsNullOrWhiteSpace(settings))
            {
                proto_generator_db_schema_settings proto = proto_generator_db_schema_settings.Parser.ParseJson(settings);
                vm = GeneratorDbSchemaSettings.ConvertToVM(proto, vm);
            }
            return vm;
        }
        public IvPluginGeneratorNodeSettings GetGenerationNodeSettingsVmFromJson(string settings, ITreeConfigNode node)
        {
            var vm = new GeneratorDbSchemaNodeSettings(node);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                proto_generator_db_schema_node_settings proto = proto_generator_db_schema_node_settings.Parser.ParseJson(settings);
                vm = GeneratorDbSchemaNodeSettings.ConvertToVM(proto, vm);
            }
            return vm;
        }
        public Dictionary<string, List<string>> DicPathTypes { get; private set; }

        public string PKeyTypeStr => throw new NotImplementedException();

        public string VersionFieldTypeStr => throw new NotImplementedException();

        public string VersionFieldStoreTypeStr => throw new NotImplementedException();

        public string PKeyStoreTypeStr => throw new NotImplementedException();

        public IvPluginGeneratorSettings GetNodeGenerationSettingsVmFromJson(string fullTypeName, string settings)
        {
            return null;
        }
        public int GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        public string UpdateToModel(string connectionString, IConfig config, string guidAppPrjGen, EnumDbUpdateLevels dbUpdateLevels, bool isGenerateUpdateScript, Func<bool> onNeedDbCreate = null, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }

        public List<PreRenameData> GetListPreRename(IConfig annotatedConfig, List<string> lstGuidsRenamedNodes)
        {
            throw new NotImplementedException();
        }

        public void EnsureDbDeleted(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void EnsureDbCreated(string connectionString)
        {
            throw new NotImplementedException();
        }

        public List<ValidationPluginMessage> ValidateDbModel(string connectionString, IConfig diffConfig, string guidAppPrjGen)
        {
            return new List<ValidationPluginMessage>();
        }
        public List<ValidationPluginMessage> ValidateNode(ITreeConfigNode node, string guidAppPrjGen)
        {
            var res = new List<ValidationPluginMessage>();
            return res;
        }
        public List<ValidationPluginMessage> ValidateOnSelection(IAppSolution sln)
        {
            return new List<ValidationPluginMessage>();
        }
    }
}
