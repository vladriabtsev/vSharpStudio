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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Oracle.EntityFrameworkCore.Scaffolding.Internal;
using Oracle.EntityFrameworkCore.Storage.Internal;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
namespace DbModel.Oracle
{
    //[Export(typeof(IDbMigrator))]
    //[ExportMetadata("Name", "Oracle")]
    public class OracleMigrator : IDbMigrator
    {
        static DiagnosticSource OracleMigratorDiagnostic = new DiagnosticListener("DbModel.Oracle.OracleMigrator");
        public ILogger Logger;
        string IDbMigrator.DbTypeName => "Oracle";
        ILoggerFactory IDbMigrator.LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<OracleMigrator>();
            }
        }
        private ILoggerFactory _LoggerFactory;
        string IDbMigrator.ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
        private string _ConnectionString;
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
            var dbModelFactory = new OracleDatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                                    _LoggerFactory,
                                    new LoggingOptions(),
                                    OracleMigratorDiagnostic
                ));
            //new OracleTypeMappingSource(
            //    new TypeMappingSourceDependencies(
            //        new ValueConverterSelector(new ValueConverterSelectorDependencies()),
            //        Array.Empty<ITypeMappingSourcePlugin>()
            //    ),
            //    new RelationalTypeMappingSourceDependencies(
            //        Array.Empty<IRelationalTypeMappingSourcePlugin>()
            //    )
            //));
            var dbModel = dbModelFactory.Create(_ConnectionString, new List<string>(), new List<string>());
        }
    }
}
