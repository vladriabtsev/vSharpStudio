using System;
using System.Collections.Generic;

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
    /// <summary>
    /// A component which translates a CLR name (e.g. SomeClass) into a database name (e.g. some_class)
    /// according to some scheme.
    /// Used for mapping enum and composite types.
    /// Idea from Npgsql.INpgsqlNameTranslator.
    /// </summary>
    public interface IDbNameTranslator
    {
        static void TranslateNameCacheClear() { throw new NotImplementedException(); }
        /// <summary>
        /// Given a CLR type name (e.g class, struct, enum), translates its name to a database type name.
        /// </summary>
        static string TranslateTypeNameStatic(string clrName) { throw new NotImplementedException(); }
        string TranslateTypeName(string clrName);

        /// <summary>
        /// Given a CLR member name (property or field), translates its name to a database type name.
        /// </summary>
        static string TranslateMemberNameStatic(string clrName) { throw new NotImplementedException(); }
        string TranslateMemberName(string clrName);
    }
    // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
    // https://docs.microsoft.com/en-us/dotnet/framework/mef/
    /// <summary>
    /// Interface for DbDesign type of plugins
    /// </summary>
    public interface IvPluginDbGenerator : IvPluginGenerator, IDbNameTranslator
    {
        //ILoggerFactory LoggerFactory { get; set; }
        bool IsStableDbConnection { get; set; }
        // Provider name as from connection string
        string ProviderName { get; set; }
        string DbSchema { get; }
        string PKeyName { get; }
        // csharp type name, int or long or etc
        string PKeyClrTypeStr { get; }
        string PKeyStoreTypeStr { get; }
        //string VersionFieldName { get; }
        //// csharp type name, int or long or etc
        //string VersionFieldTypeStr { get; }
        //string VersionFieldStoreTypeStr { get; }

        IvPluginGeneratorSettings? GetConnectionStringMvvm(IAppProjectGenerator parent, string connectionString);

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
        string? UpdateToModel(string connectionString, IConfig config, IAppSolution sln, IAppProject prj, string guidAppPrjGen, EnumDbUpdateLevels dbUpdateLevels, bool isGenerateUpdateScript, Func<bool>? onNeedDbCreate = null);
        /// <summary>
        /// True if DB data structure is changed after last UpdateToModel call.
        /// Data structure changes can include: 
        ///   - DB schema changed;
        ///   - table deleted;
        ///   - table created;
        ///   - table renamed;
        ///   - field data type changed;
        ///   - field deleted;
        ///   - field created;
        ///   - field renamed;
        /// </summary>
        bool IsDbDataStructureChanged { get; }
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
