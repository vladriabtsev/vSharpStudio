using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
namespace DbModel.DB2
{
    [System.ComponentModel.Composition.ExportMetadata("Name", "Db2")]
    public class Db2Migrator : IDbMigrator
    {
        static DiagnosticSource Db2MigratorDiagnostic = new DiagnosticListener("DbModel.DB2.Db2Migrator");
        public ILogger Logger;
        ILoggerFactory IDbMigrator.LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<Db2Migrator>();
            }
        }
        private ILoggerFactory _LoggerFactory;
        DatabaseModel IDbMigrator.GetDbModel(string connectionString, List<string> tables, List<string> schemas)
        {
            if (_LoggerFactory == null)
                throw new Exception();
            var dbModelFactory = new Db2DatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                                    _LoggerFactory,
                                    new LoggingOptions(),
                                    Db2MigratorDiagnostic
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
