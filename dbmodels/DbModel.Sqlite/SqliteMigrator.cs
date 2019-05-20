using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Sqlite.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
namespace DbModel.Sqlite
{
    //[Export(typeof(IDbMigrator))]
    //[ExportMetadata("Name", "Sqlite")]
    public class SqliteMigrator : IDbMigrator
    {
        static DiagnosticSource SqlITEMigratorDiagnostic = new DiagnosticListener("DbModel.Sqlite.SqliteMigrator");
        public ILogger Logger;
        string IDbMigrator.DbTypeName => "Sqlite";
        ILoggerFactory IDbMigrator.LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<SqliteMigrator>();
            }
        }
        private ILoggerFactory _LoggerFactory;
        string IDbMigrator.ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
        private string _ConnectionString;
        bool IDbMigrator.CreateDb()
        {
            return false;
        }
        DatabaseModel IDbMigrator.GetDbModel(List<string> schemas, List<string> tables)
        {
            DatabaseModel m = null;
            return m;
        }
        int IDbMigrator.GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        void IDbMigrator.UpdateToModel(IModel model)
        {
            if (_LoggerFactory == null)
                throw new Exception();
            if (_ConnectionString == null)
                throw new Exception();
            var dbModelFactory = new SqliteDatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                                    _LoggerFactory,
                                    new LoggingOptions(),
                                    SqlITEMigratorDiagnostic
                ),
                new SqliteTypeMappingSource(
                    new TypeMappingSourceDependencies(
                        new ValueConverterSelector(new ValueConverterSelectorDependencies()),
                        Array.Empty<ITypeMappingSourcePlugin>()
                    ),
                    new RelationalTypeMappingSourceDependencies(
                        Array.Empty<IRelationalTypeMappingSourcePlugin>()
                    )
                ));
            var dbModel = dbModelFactory.Create(_ConnectionString, new List<string>(), new List<string>());
        }
    }
}
