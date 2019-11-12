using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.common
{
    // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
    // https://docs.microsoft.com/en-us/dotnet/framework/mef/
    /// <summary>
    /// Interface for DbDesign type of plugins
    /// </summary>
    public interface IvPluginDbGenerator : IvPluginGenerator
    {
        ILoggerFactory LoggerFactory { get; set; }

        string ProviderName { get; }

        int GetMigrationVersion();
        // DatabaseModel GetDbModel(List<string> schemas, List<string> tables);
        // void UpdateToModel(IModel model);

        /// <summary>
        /// Return last proto model which was used last time for migration
        /// </summary>
        /// <returns>json version of IConfig</returns>
        string GetLastModel();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="operations">To do what model differ will not capable to do. Probably renaming tables and fields propery</param>
        /// <param name="target_model"></param>
        /// <param name="onNeedDbCreate"></param>
        /// <param name="onError"></param>
        void UpdateToModel(string connectionString, MigrationOperation[] operations, IConfig config, Func<bool> onNeedDbCreate, Action<Exception> onError);
        // void Backup(string filePath);
        // void Restore(string filePath);
        // void Export(string filePath);
        // void Import(string filePath);
    }
}
