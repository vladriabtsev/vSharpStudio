using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Proto.Plugin;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;

namespace vPlugin.Sample
{
    public class GeneratorDbSchema : IvPluginDbGenerator
    {
        public IvPluginGenerator CreateNew(IAppProjectGenerator appProjectGenerator) { return new GeneratorDbSchema(appProjectGenerator); }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GeneratorDbSchema()
        {
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            this.DicPathTypes = new Dictionary<string, List<string>>();
        }
        public GeneratorDbSchema(ITreeConfigNode parent) : this()
        {
            this.Parent = parent;
        }
        public ILoggerFactory LoggerFactory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsDbDataStructureChanged { get; private set; }
        public bool IsStableDbConnection { get; set; }
        public string ProviderName { get; set; } = String.Empty;
        public string Guid => "08744482-BE03-464B-81AB-DD482AB66103";
        public string SolutionParametersGuid => PluginsGroupSolutionSettings.GuidStatic;
        public string ProjectParametersGuid => PluginsGroupProjectSettings.GuidStatic;
        public string Name => "AbstractDbSchema";
        public string NameUi => "Abstract Db Provider Name";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Abstract Db Schema";
        public string DbSchema => "";
        public string PKeyName => "";
        public string VersionFieldName => "";
        public ITreeConfigNode? Parent { get; set; }
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbDesign;
        public void Init() { }
        public void EnsureDbDeletedAndCreated(string connectionString)
        {
            throw new NotImplementedException();
        }
        public IvPluginGeneratorSettings? GetConnectionStringMvvm(IAppProjectGenerator parent, string connectionString)
        {
            var res = new DbConnectionStringSettings(parent, connectionString);
            return res;
        }
        public object GetDbModel(string connectionString)
        {
            throw new NotImplementedException();
        }
        public object GetDbModel(object context)
        {
            throw new NotImplementedException();
        }
        public IvPluginGeneratorSettings? GetAppGenerationSettingsVmFromJson(IAppProjectGenerator parent, string? settings)
        {
            var vm = new GeneratorDbSchemaSettings(parent);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = CommonUtils.ParseJson<proto_generator_db_schema_settings>(settings, true);
                vm = GeneratorDbSchemaSettings.ConvertToVM(proto, vm);
            }
            vm.Parent = parent;
            return vm;
        }
        public IvPluginGeneratorNodeSettings? GetGenerationNodeSettingsVmFromJson(ITreeConfigNode parent, string? settings)
        {
            if (parent is IModel || parent is ICatalog)
            {
                var vm = new GeneratorDbSchemaNodeSettings(parent);
                if (!string.IsNullOrWhiteSpace(settings))
                {
                    var proto = CommonUtils.ParseJson<proto_generator_db_schema_node_settings>(settings, true);
                    vm = GeneratorDbSchemaNodeSettings.ConvertToVM(proto, vm);
                    vm.Parent = parent;
                }
                else
                {
                    // default settings on Model level
                    if (parent is IModel)
                    {
                        vm.IsCatalogFormParam1 = true;
                    }
                }
                return vm;
            }
            return null;
        }
        public Dictionary<string, List<string>> DicPathTypes { get; private set; }

        public string PKeyTypeStr => throw new NotImplementedException();

        public string VersionFieldTypeStr => throw new NotImplementedException();

        public string VersionFieldStoreTypeStr => throw new NotImplementedException();

        public string PKeyStoreTypeStr => throw new NotImplementedException();

        public IvPluginGeneratorSettings? GetNodeGenerationSettingsVmFromJson(string fullTypeName, string? settings)
        {
            return null;
        }
        public int GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        public string UpdateToModel(string connectionString, IConfig config, IAppSolution sln, IAppProject prj, string guidAppPrjGen, EnumDbUpdateLevels dbUpdateLevels, bool isGenerateUpdateScript, Func<bool>? onNeedDbCreate = null)
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
        public IvPluginGroupSettings? GetPluginGroupSolutionSettingsVmFromJson(IAppSolution parent, string? settings)
        {
            var res = new PluginsGroupSolutionSettings(parent);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = CommonUtils.ParseJson<proto_plugins_group_solution_settings>(settings, true);
                res = PluginsGroupSolutionSettings.ConvertToVM(proto, res);
            }
            res.Parent = parent;
            return res;
        }
        public IvPluginGroupSettings? GetPluginGroupProjectSettingsVmFromJson(IAppProject parent, string? settings)
        {
            var res = new PluginsGroupProjectSettings(parent);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = CommonUtils.ParseJson<proto_plugins_group_project_settings>(settings, true);
                res = PluginsGroupProjectSettings.ConvertToVM(proto, res);
            }
            res.Parent = parent;
            return res;
        }
    }
}
