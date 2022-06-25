using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.common
{
    [Flags]
    public enum EnumDbUpdateLevels
    {
        OnlyModel = 0b_0000_0000, // 0
        TryKeepIndexes = 0b_0000_0001, // 1
        TryKeepTables = 0b_0000_0010, // 2

        TryKeepAll = TryKeepIndexes | TryKeepTables
    }
    // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
    // https://docs.microsoft.com/en-us/dotnet/framework/mef/
    /// <summary>
    /// Interface for DbDesign type of plugins
    /// </summary>
    public interface IvPluginDbGenerator : IvPluginGenerator
    {
        //ILoggerFactory LoggerFactory { get; set; }
        bool IsStableDbConnection { get; set; }
        // Provider name as from connection string
        string ProviderName { get; set; }
        string DbSchema { get; }
        string PKeyName { get; }
        // csharp type name, int or long or etc
        //string PKeyTypeStr { get; }
        string PKeyStoreTypeStr { get; }
        //string VersionFieldName { get; }
        //// csharp type name, int or long or etc
        //string VersionFieldTypeStr { get; }
        //string VersionFieldStoreTypeStr { get; }

        IvPluginGeneratorSettings GetConnectionStringMvvm(string connectionString);

        int GetMigrationVersion();
        // DatabaseModel GetDbModel(List<string> schemas, List<string> tables);
        // void UpdateToModel(IModel model);

        List<ValidationPluginMessage> ValidateDbModel(string connectionString, IConfig diffConfig, string guidAppPrjGen);

        /// <summary>
        /// Generate DatabaseModel of a current DB
        /// </summary>
        /// <returns>Return DatabaseModel of a current DB</returns>
        object GetDbModel(string connectionString);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">DbContext type</param>
        /// <returns></returns>
        object GetDbModel(object context);
        /// <summary>
        /// Return IMutableModel
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="operations">To do what model differ will not capable to do. Probably renaming tables and fields propery</param>
        /// <param name="target_model"></param>
        /// <param name="onNeedDbCreate"></param>
        string UpdateToModel(string connectionString, IConfig config, IAppSolution sln, IAppProject prj, string guidAppPrjGen, EnumDbUpdateLevels dbUpdateLevels, bool isGenerateUpdateScript, Func<bool> onNeedDbCreate = null);
        // void UpdateToModel2(string connectionString, MigrationOperation[] operations, IConfig config, Func<bool> onNeedDbCreate, Action<Exception> onError);
        // void Backup(string filePath);
        // void Restore(string filePath);
        // void Export(string filePath);
        // void Import(string filePath);
        void EnsureDbDeleted(string connectionString);
        void EnsureDbCreated(string connectionString);
        void EnsureDbDeletedAndCreated(string connectionString);
    }
}
