using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 10
{
    public enum DbIdGeneratorMethod // ModelInterfaces.tt Line: 14
    {
        Identity = 0,
        HiLo = 1,
    }
    public enum EnumPrimaryKeyType // ModelInterfaces.tt Line: 14
    {
        INT = 0,
        LONG = 1,
    }
    public enum EnumDataType // ModelInterfaces.tt Line: 14
    {
        STRING = 0,
        NUMERICAL = 1,
        BOOL = 2,
        TIME = 3,
        DATE = 4,
        DATETIME = 5,
        ENUMERATION = 8,
        CATALOG = 9,
        CATALOGS = 10,
        DOCUMENT = 11,
        DOCUMENTS = 12,
        ANY = 15,
    }
    public enum EnumEnumerationType // ModelInterfaces.tt Line: 14
    {
        STRING_VALUE = 0,
        BYTE_VALUE = 1,
        SHORT_VALUE = 2,
        INTEGER_VALUE = 3,
    }
    
    public partial interface IGroupListPlugins : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IPlugin> IListPlugins { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IPlugin : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        string Version { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IPluginGenerator> IListGenerators { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IPluginGenerator : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        string Description { get; } // ModelInterfaces.tt Line: 46
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IPluginGeneratorSettings> IListSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IPluginGeneratorSettings : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        
        ///////////////////////////////////////////////////
        /// This Description is taken from Plugin Generator
        ///////////////////////////////////////////////////
        string Description { get; } // ModelInterfaces.tt Line: 46
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string GeneratorSettings { get; } // ModelInterfaces.tt Line: 46
        bool IsPrivate { get; } // ModelInterfaces.tt Line: 46
        string FilePath { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface ISettingsConfig : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// current migration version, increased by one on each deployment
        ///////////////////////////////////////////////////
        int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// min version supported by current version for migration
        ///////////////////////////////////////////////////
        int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 46
    }
    
    ///////////////////////////////////////////////////
    /// General DB settings
    ///////////////////////////////////////////////////
    
    public partial interface IDbSettings // ModelInterfaces.tt Line: 28
    {
        string DbSchema { get; } // ModelInterfaces.tt Line: 46
        DbIdGeneratorMethod IdGenerator { get; } // ModelInterfaces.tt Line: 46
        EnumPrimaryKeyType PKeyType { get; } // ModelInterfaces.tt Line: 46
        string KeyName { get; } // ModelInterfaces.tt Line: 46
        string Timestamp { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// if yes: 
        ///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
        /// if no:
        ///    1. Find DB type from 
        ///    2. Create connection string from db_server, db_database_name, db_user
        ///////////////////////////////////////////////////
        bool IsDbFromConnectionString { get; } // ModelInterfaces.tt Line: 46
        string ConnectionStringName { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// path to project with config file containing connection string. Usefull for UNIT tests.
        /// it will override previous settings
        ///////////////////////////////////////////////////
        string PathToProjectWithConnectionString { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IConfigShortHistory : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        IConfig ICurrentConfig { get; } // ModelInterfaces.tt Line: 50
        IConfig IPrevStableConfig { get; } // ModelInterfaces.tt Line: 50
        IConfig IOldStableConfig { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IGroupListBaseConfigLinks : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IBaseConfigLink> IListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IBaseConfigLink : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IConfig IConfig { get; } // ModelInterfaces.tt Line: 50
        string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 46
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    
    public partial interface IConfig : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        int Version { get; } // ModelInterfaces.tt Line: 46
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// GENERAL DB SETTINGS
        ///////////////////////////////////////////////////
        IDbSettings IDbSettings { get; } // ModelInterfaces.tt Line: 50
        IGroupListBaseConfigLinks IGroupConfigLinks { get; } // ModelInterfaces.tt Line: 50
        IConfigModel IModel { get; } // ModelInterfaces.tt Line: 50
        IGroupListPlugins IGroupPlugins { get; } // ModelInterfaces.tt Line: 50
        IGroupListAppSolutions IGroupAppSolutions { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IAppDbSettings // ModelInterfaces.tt Line: 28
    {
        string PluginGuid { get; } // ModelInterfaces.tt Line: 46
        string PluginName { get; } // ModelInterfaces.tt Line: 46
        string Version { get; } // ModelInterfaces.tt Line: 46
        string PluginGenGuid { get; } // ModelInterfaces.tt Line: 46
        string PluginGenName { get; } // ModelInterfaces.tt Line: 46
        string ConnGuid { get; } // ModelInterfaces.tt Line: 46
        string ConnName { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IGroupListAppSolutions : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IAppDbSettings IDefaultDb { get; } // ModelInterfaces.tt Line: 50
        
        ///////////////////////////////////////////////////
        /// List NET solutions
        ///////////////////////////////////////////////////
        IEnumerable<IAppSolution> IListAppSolutions { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IAppSolution : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// List NET projects
        ///////////////////////////////////////////////////
        string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 46
        IAppDbSettings IDefaultDb { get; } // ModelInterfaces.tt Line: 50
        IEnumerable<IAppProject> IListAppProjects { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IAppProject : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 46
        IAppDbSettings IDefaultDb { get; } // ModelInterfaces.tt Line: 50
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    
    public partial interface IConfigModel : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        int Version { get; } // ModelInterfaces.tt Line: 46
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IGroupListCommon IGroupCommon { get; } // ModelInterfaces.tt Line: 50
        IGroupListConstants IGroupConstants { get; } // ModelInterfaces.tt Line: 50
        IGroupListEnumerations IGroupEnumerations { get; } // ModelInterfaces.tt Line: 50
        IGroupListCatalogs IGroupCatalogs { get; } // ModelInterfaces.tt Line: 50
        IGroupDocuments IGroupDocuments { get; } // ModelInterfaces.tt Line: 50
        IGroupListJournals IGroupJournals { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IDataType // ModelInterfaces.tt Line: 28
    {
        EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 46
        uint Length { get; } // ModelInterfaces.tt Line: 46
        uint Accuracy { get; } // ModelInterfaces.tt Line: 46
        bool IsPositive { get; } // ModelInterfaces.tt Line: 46
        string ObjectGuid { get; } // ModelInterfaces.tt Line: 46
        bool IsNullable { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<string> IListObjectGuids { get; } // ModelInterfaces.tt Line: 41
        bool IsIndexFk { get; } // ModelInterfaces.tt Line: 46
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListCommon : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IGroupListRoles IGroupRoles { get; } // ModelInterfaces.tt Line: 50
        IGroupListMainViewForms IGroupViewForms { get; } // ModelInterfaces.tt Line: 50
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    
    public partial interface IRole : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IGroupListRoles : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IRole> IListRoles { get; } // ModelInterfaces.tt Line: 43
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    
    public partial interface IMainViewForm : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IGroupListMainViewForms IGroupListViewForms { get; } // ModelInterfaces.tt Line: 50
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListMainViewForms : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IMainViewForm> IListMainViewForms { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListPropertiesTabs : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IPropertiesTab> IListPropertiesTabs { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IPropertiesTab : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IGroupListProperties IGroupProperties { get; } // ModelInterfaces.tt Line: 50
        IGroupListPropertiesTabs IGroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 50
        
        ///////////////////////////////////////////////////
        /// Create Index for foreign key navigation property
        ///////////////////////////////////////////////////
        bool IsIndexFk { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IGroupListProperties : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IProperty> IListProperties { get; } // ModelInterfaces.tt Line: 43
        
        ///////////////////////////////////////////////////
        /// Last generated Protobuf field position
        ///////////////////////////////////////////////////
        uint LastGenPosition { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IProperty : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IDataType IDataType { get; } // ModelInterfaces.tt Line: 50
        
        ///////////////////////////////////////////////////
        /// Protobuf field position
        /// Reserved positions: 1 - primary key
        ///////////////////////////////////////////////////
        uint Position { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IGroupListConstants : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IConstant> IListConstants { get; } // ModelInterfaces.tt Line: 43
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IDataType IDataType { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IGroupListEnumerations : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IEnumeration> IListEnumerations { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IEnumeration : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// Enumeration element type
        ///////////////////////////////////////////////////
        EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// Length of string if 'STRING' is selected as enumeration element type
        ///////////////////////////////////////////////////
        int DataTypeLength { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IEnumerationPair> IListEnumerationPairs { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IEnumerationPair : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// TODO struct for different types, at least INTEGER
        ///////////////////////////////////////////////////
        string Value { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface ICatalog : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IGroupListProperties IGroupProperties { get; } // ModelInterfaces.tt Line: 50
        IGroupListPropertiesTabs IGroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 50
        IGroupListForms IGroupForms { get; } // ModelInterfaces.tt Line: 50
        IGroupListReports IGroupReports { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IGroupListCatalogs : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<ICatalog> IListCatalogs { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupDocuments : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IGroupListProperties IGroupSharedProperties { get; } // ModelInterfaces.tt Line: 50
        IGroupListDocuments IGroupListDocuments { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IDocument : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IGroupListProperties IGroupProperties { get; } // ModelInterfaces.tt Line: 50
        IGroupListPropertiesTabs IGroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 50
        IGroupListForms IGroupForms { get; } // ModelInterfaces.tt Line: 50
        IGroupListReports IGroupReports { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IGroupListDocuments : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        IEnumerable<IDocument> IListDocuments { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListJournals : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IJournal> IListJournals { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IJournal : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IDocument> IListDocuments { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListForms : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IForm> IListForms { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IForm : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// 
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_forms = 7;
        ///////////////////////////////////////////////////
        string Description { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IGroupListReports : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        string Description { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IReport> IListReports { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IReport : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 28
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 46
        string NameUi { get; } // ModelInterfaces.tt Line: 46
        
        ///////////////////////////////////////////////////
        /// 
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_documents = 7;
        ///////////////////////////////////////////////////
        string Description { get; } // ModelInterfaces.tt Line: 46
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 28
    {
        string GroupName { get; } // ModelInterfaces.tt Line: 46
        string Name { get; } // ModelInterfaces.tt Line: 46
        string Guid { get; } // ModelInterfaces.tt Line: 46
        bool IsIncluded { get; } // ModelInterfaces.tt Line: 46
    }
}