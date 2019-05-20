using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.common
{
    // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
    // https://docs.microsoft.com/en-us/dotnet/framework/mef/
    [InheritedExport(typeof(IDbMigrator))] // metadata was not exported
    public interface IDbMigrator
    {
        ILoggerFactory LoggerFactory { set; get; }
        string DbTypeName { get; }
        string ConnectionString { set; get; }
        bool CreateDb();
        int GetMigrationVersion();
        DatabaseModel GetDbModel(List<string> schemas, List<string> tables);
        void UpdateToModel(IModel model);
    }
}
