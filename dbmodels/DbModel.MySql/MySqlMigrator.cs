using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
namespace DbModel.MySql
{
    // https://github.com/mysql/mysql-connector-net
    // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
    //[Export(typeof(IDbMigrator))]
    //[ExportMetadata("Name", "MySQL")]
    public class MySqlMigrator : IDbMigrator
    {
        public ILogger Logger;
        string IDbMigrator.DbTypeName => "MySQL";
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
        string IDbMigrator.ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
        private string _ConnectionString;
        int IDbMigrator.GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        DatabaseModel IDbMigrator.GetDbModel(List<string> schemas, List<string> tables)
        {
            DatabaseModel m = null;
            return m;
        }
        void IDbMigrator.UpdateToModel(IModel model)
        {
            if (_LoggerFactory == null)
                throw new Exception();
            if (_ConnectionString == null)
                throw new Exception();
            var dbModelFactory = new MySqlDatabaseModelFactory(_LoggerFactory);
            var dbModel = dbModelFactory.Create(_ConnectionString, new List<string>(), new List<string>());
        }
    }
}
