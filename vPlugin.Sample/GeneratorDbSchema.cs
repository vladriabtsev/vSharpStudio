﻿using System;
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
        public string ProviderName => "AbstractDbProviderName";
        public string Guid => "08744482-BE03-464B-81AB-DD482AB66103";
        public string Name => "AbstractDbSchema";
        public string NameUi => "Abstract Db Provider Name";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Abstract Db Schema";
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbDesign;
        public void EnsureDbDeletedAndCreated(string connectionString)
        {
            throw new NotImplementedException();
        }
        public IvPluginGeneratorSettingsVM GetConnectionStringMvvm(string provider, string connectionString)
        {
            throw new NotImplementedException();
        }
        public object GetDbModel(string connectionString, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }
        public object GetDbModel(object context, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }
        private GeneratorDbSchemaSettings gen_settings = null;
        public IvPluginGeneratorSettingsVM GetAppGenerationSettingsVmFromJson(string settings)
        {
            if (gen_settings != null)
                return gen_settings;
            if (string.IsNullOrWhiteSpace(settings))
                gen_settings = new GeneratorDbSchemaSettings();
            else
            {
                proto_generator_db_schema_settings proto = proto_generator_db_schema_settings.Parser.ParseJson(settings);
                gen_settings = GeneratorDbSchemaSettings.ConvertToVM(proto, null);
            }
            return gen_settings;
        }
        public List<IvPluginNodeSettings> GetListNodeGenerationSettings()
        {
            List<IvPluginNodeSettings> res = new List<IvPluginNodeSettings>();
            res.Add(new GeneratorDbAccessNodePropertySettings());
            res.Add(new GeneratorDbAccessNodeCatalogFormSettings());
            return res;
        }
        public Dictionary<string, List<string>> DicPathTypes { get; private set; }
        public IvPluginGeneratorSettingsVM GetNodeGenerationSettingsVmFromJson(string fullTypeName, string settings)
        {
            return null;        }
        public int GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        public object UpdateToModel(string connectionString, IConfig config, Func<bool> onNeedDbCreate = null, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }

        public List<PreRenameData> GetListPreRename(IConfig annotatedConfig, List<string> lstGuidsRenamedNodes)
        {
            throw new NotImplementedException();
        }
    }
}
