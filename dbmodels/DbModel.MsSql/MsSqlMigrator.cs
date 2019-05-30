using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proto.Config.Connection;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;

// https://docs.microsoft.com/en-us/ef/core/providers/
// https://medium.com/volosoft/asp-net-core-dependency-injection-58bc78c5d369
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
namespace vPlugin.DbModel.MsSql
{
    //[Export(typeof(IDbMigrator))]
    //[ExportMetadata("Name", "MsSQL")]
    public class MsSqlPlugin : IvPlugin
    {
        public MsSqlPlugin()
        {
            this.Guid = new Guid("C94175E4-E8F4-4A84-871B-6994199A2076");
            this.Name = "MsSql";
            this.Description = "vSharpStudio plugin. Database design for MS SQL support";
            this.Author = "Vladimir Riabtsev";
            this.Version = "0.1";
            this.Url = "https://www.vladnet.ca";
            this.Licence = "";
            this.ListGenerators = new List<IvPluginCodeGenerator>();
            this.ListGenerators.Add(new MsSqlMigrator());
        }
        public Guid Guid { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Author { get; protected set; }
        public string Version { get; protected set; }
        public string Url { get; protected set; }
        public string Licence { get; protected set; }
        public List<IvPluginCodeGenerator> ListGenerators { get; protected set; }
    }
    public class MsSqlMigrator : IDbMigrator
    {
        public MsSqlMigrator()
        {
            this.Guid = new Guid("0D85CF93-9134-45C6-B3B9-6BFAF4A12183");
            this.Name = "Connections";
            this.DefaultSettingsName = "Connection";
            this.Description = "Connection strings";
            this.PluginType = vPluginTypeEnum.DbDesign;
        }
        static DiagnosticSource MsSqlMigratorDiagnostic = new DiagnosticListener("vPlugin.MsSqlMigrator");
        public ILogger Logger;
        public Guid Guid { get; protected set; }
        public string Name { get; protected set; }
        public string DefaultSettingsName { get; protected set; }
        public string Description { get; protected set; }
        public vPluginTypeEnum PluginType { get; }
        public IvPluginSettingsVM GetSettingsMvvm(string settings)
        {
            if (settings == null)
                return new ConnMsSql();
            proto_conn_ms_sql proto = proto_conn_ms_sql.Parser.ParseJson(settings);
            ConnMsSql res = ConnMsSql.ConvertToVM(proto);
            return res;
        }
        public ILoggerFactory LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<MsSqlMigrator>();
            }
        }
        private ILoggerFactory _LoggerFactory;

        public string ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }

        //public Any Settings => throw new NotImplementedException();

        //public List<IvPluginCodeGenerator> ListGenerators => throw new NotImplementedException();

        private string _ConnectionString;
        public bool CreateDb()
        {
            return false;
        }
        public void SetServices(ServiceProvider serviceProvider)
        {
            this.LoggerFactory = serviceProvider.GetService<ILoggerFactory>();
            reporter = serviceProvider.GetService<IOperationReporter>();
            candidateNamingService = serviceProvider.GetService<ICandidateNamingService>();
            pluralizer = serviceProvider.GetService<IPluralizer>();
            cSharpUtilities = serviceProvider.GetService<ICSharpUtilities>();
            scaffoldingTypeMapper = serviceProvider.GetService<IScaffoldingTypeMapper>();
            changeDetector = serviceProvider.GetService<IChangeDetector>();
        }

        IOperationReporter reporter;
        ICandidateNamingService candidateNamingService;
        IPluralizer pluralizer;
        ICSharpUtilities cSharpUtilities;
        IScaffoldingTypeMapper scaffoldingTypeMapper;
        IChangeDetector changeDetector;

        public int GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        public DatabaseModel GetDbModel(List<string> schemas, List<string> tables)
        {
            var databaseModelFactory = new SqlServerDatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                    _LoggerFactory,
                    new LoggingOptions(),
                    MsSqlMigratorDiagnostic));

            var databaseModel = databaseModelFactory.Create(this.ConnectionString, tables, schemas);
            return databaseModel;
        }
        public void UpdateToModel(IModel modelTarget)
        {
            if (_LoggerFactory == null)
                throw new Exception();
            if (_ConnectionString == null)
                throw new Exception();
            var dbModelFactory = new SqlServerDatabaseModelFactory(
                                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                                    _LoggerFactory,
                                    new LoggingOptions(),
                                    MsSqlMigratorDiagnostic));

            //var typeMappingSource = new SqlServerTypeMappingSource(
            //    new TypeMappingSourceDependencies(
            //        new ValueConverterSelector(new ValueConverterSelectorDependencies()),
            //        Array.Empty<ITypeMappingSourcePlugin>()
            //    ),
            //    new RelationalTypeMappingSourceDependencies(Array.Empty<IRelationalTypeMappingSourcePlugin>())
            //);

            var dbModel = dbModelFactory.Create(_ConnectionString, new List<string>(), new List<string>());
            RelationalScaffoldingModelFactory relFactory = new RelationalScaffoldingModelFactory(
                reporter,
                candidateNamingService,
                pluralizer,
                cSharpUtilities,
                scaffoldingTypeMapper);
            var modelSource = relFactory.Create(dbModel, true);

            //var ctx = TestHelpers.CreateContext(
            //    TestHelpers.AddProviderOptions(new DbContextOptionsBuilder())
            //        .UseModel(model).EnableSensitiveDataLogging().Options);

            //var differ = new MigrationsModelDiffer(
            //    new SqlServerTypeMappingSource(
            //            new TypeMappingSourceDependencies(
            //                new ValueConverterSelector(new ValueConverterSelectorDependencies()),
            //                Array.Empty<ITypeMappingSourcePlugin>()
            //            ),
            //            new RelationalTypeMappingSourceDependencies(Array.Empty<IRelationalTypeMappingSourcePlugin>())),
            //    new SqlServerMigrationsAnnotationProvider(
            //        new MigrationsAnnotationProviderDependencies()),
            //    changeDetector,
            //    ctx.GetService<StateManagerDependencies>(),
            //    ctx.GetService<CommandBatchPreparerDependencies>());

        }

    }
}
