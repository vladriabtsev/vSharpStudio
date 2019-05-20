using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
// https://medium.com/volosoft/asp-net-core-dependency-injection-58bc78c5d369
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
namespace DbModel.MsSql
{
    //[Export(typeof(IDbMigrator))]
    //[ExportMetadata("Name", "MsSQL")]
    public class MsSqlMigrator : IDbMigrator
    {
        static DiagnosticSource MsSqlMigratorDiagnostic = new DiagnosticListener("DbModel.MsSql.MsSqlMigrator");
        public ILogger Logger;
        string IDbMigrator.DbTypeName => "MsSQL";
        ILoggerFactory IDbMigrator.LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<MsSqlMigrator>();
            }
        }
        private ILoggerFactory _LoggerFactory;
        string IDbMigrator.ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
        private string _ConnectionString;
        bool IDbMigrator.CreateDb()
        {
            return false;
        }
        public void SetServices(ServiceProvider serviceProvider)
        {
            (this as IDbMigrator).LoggerFactory = serviceProvider.GetService<ILoggerFactory>();
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

        int IDbMigrator.GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        DatabaseModel IDbMigrator.GetDbModel(List<string> schemas, List<string> tables)
        {
            var databaseModelFactory = new SqlServerDatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                    _LoggerFactory,
                    new LoggingOptions(),
                    MsSqlMigratorDiagnostic));

            var databaseModel = databaseModelFactory.Create((this as IDbMigrator).ConnectionString, tables, schemas);
            return databaseModel;
        }
        void IDbMigrator.UpdateToModel(IModel modelTarget)
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
