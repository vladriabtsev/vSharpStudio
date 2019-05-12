using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
namespace DbModel.MySql
{
    // https://github.com/mysql/mysql-connector-net
    // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
    [System.ComponentModel.Composition.ExportMetadata("Name", "My SQL")]
    public class MySqlMigrator : IDbMigrator
    {
        public ILogger Logger;
        ILoggerFactory IDbMigrator.LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<MySqlMigrator>();
            }
        }
        private ILoggerFactory _LoggerFactory;
        DatabaseModel IDbMigrator.GetDbModel(string connectionString, List<string> tables, List<string> schemas)
        {
            if (_LoggerFactory == null)
                throw new Exception();
            var dbModelFactory = new MySqlDatabaseModelFactory(_LoggerFactory);

            var dbModel = dbModelFactory.Create(connectionString, tables, schemas);
            return dbModel;
        }

        int IDbMigrator.GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
    }
}
