using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.SqlCe.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
namespace DbModel.SqlServerCompact4
{
    [System.ComponentModel.Composition.ExportMetadata("Name", "SqlCe4")]
    public class SqlCe4Migrator : IDbMigrator
    {
        static DiagnosticSource SqlServerCompact4MigratorDiagnostic = new DiagnosticListener("DbModel.SqlServerCompact4.SqlServerCompact4Migrator");
        public ILogger Logger;
        ILoggerFactory IDbMigrator.LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<SqlCe4Migrator>();
            }
        }
        private ILoggerFactory _LoggerFactory;
        DatabaseModel IDbMigrator.GetDbModel(string connectionString, List<string> tables, List<string> schemas)
        {
            if (_LoggerFactory == null)
                throw new Exception();
            var dbModelFactory = new SqlCeDatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                                    _LoggerFactory,
                                    new LoggingOptions(),
                                    SqlServerCompact4MigratorDiagnostic
                ));
                //new SqliteTypeMappingSource(
                //    new TypeMappingSourceDependencies(
                //        new ValueConverterSelector(new ValueConverterSelectorDependencies()),
                //        Array.Empty<ITypeMappingSourcePlugin>()
                //    ),
                //    new RelationalTypeMappingSourceDependencies(
                //        Array.Empty<IRelationalTypeMappingSourcePlugin>()
                //    )
                //));
            var dbModel = dbModelFactory.Create(connectionString, tables, schemas);
            return dbModel;
        }

        int IDbMigrator.GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
    }
}
