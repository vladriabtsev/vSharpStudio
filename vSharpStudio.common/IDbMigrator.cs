using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.common
{
    // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
    // https://docs.microsoft.com/en-us/dotnet/framework/mef/
    [System.ComponentModel.Composition.InheritedExport(typeof(IDbMigrator))]
    public interface IDbMigrator
    {
        ILoggerFactory LoggerFactory { set; get; }
        DatabaseModel GetDbModel(string connectionString, List<string> tables, List<string> schemas);
        int GetMigrationVersion();
    }
}
