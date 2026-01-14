namespace vSharpStudio.common
{
    public interface IConcurrentAccessControlInt
    {
        int ConcurrentAccessControlInt { get; }
    }

    public interface IvDbDesignSettings : IConcurrentAccessControlInt
    {
        bool IsUseAsyncMethods { get; }
        bool IsUseSyncMethods { get; }
        bool IsUseRetryDbAccessPolicy { get; }
        bool IsUseStoredProcedures { get; }
        bool IsNotUseCache { get; }
        //bool IsUseWal { get; }

        uint DbTableNameMaxLength { get; }
        uint DbFieldNameMaxLength { get; }
        uint DbIndexNameMaxLength { get; }
        uint DbFkNameMaxLength { get; }
        string DbSchema { get; }
    }
}
