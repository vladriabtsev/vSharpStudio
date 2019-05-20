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
using Npgsql.EntityFrameworkCore.PostgreSQL.Scaffolding.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
namespace DbModel.Npgsql
{
    //[Export(typeof(IDbMigrator))]
    //[ExportMetadata("Name", "NpgSQL")]
    public class NpgsqlMigrator : IDbMigrator
    {
        static DiagnosticSource SqlITEMigratorDiagnostic = new DiagnosticListener("DbModel.Npgsql.NpgsqlMigrator");
        public ILogger Logger;
        string IDbMigrator.DbTypeName => "NpgSQL";
        ILoggerFactory IDbMigrator.LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<NpgsqlMigrator>();
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
            var dbModelFactory = new NpgsqlDatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                                    _LoggerFactory,
                                    new LoggingOptions(),
                                    SqlITEMigratorDiagnostic
                ));
            var dbModel = dbModelFactory.Create(_ConnectionString, new List<string>(), new List<string>());
        }
    }
}
