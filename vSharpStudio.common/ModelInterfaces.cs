using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 11
{
    public enum DbIdGeneratorMethod // ModelInterfaces.tt Line: 15
    {
        Identity = 0,
        HiLo = 1,
    }
    public enum EnumPrimaryKeyType // ModelInterfaces.tt Line: 15
    {
        INT = 0,
        LONG = 1,
    }
    public enum EnumDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumEnumerationType // ModelInterfaces.tt Line: 15
    {
        STRING_VALUE = 0,
        BYTE_VALUE = 1,
        SHORT_VALUE = 2,
        INTEGER_VALUE = 3,
    }
    
    public partial interface IGroupListPlugins : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPlugin> ListPlugins { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPlugin : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Version { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// 
        /// attr [ReadOnly(true)]
        /// string group_guid = 7;
        /// attr [ReadOnly(true)]
        /// string group_version = 8;
        /// attr [ReadOnly(true)]
        /// string group_info = 9;
        ///////////////////////////////////////////////////
        IEnumerable<IPluginGenerator> ListGenerators { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPluginGenerator : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorSettings> ListSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPluginGeneratorSettings : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// This Description is taken from Plugin Generator
        ///////////////////////////////////////////////////
        string Description { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string GeneratorSettings { get; } // ModelInterfaces.tt Line: 47
        bool IsPrivate { get; } // ModelInterfaces.tt Line: 47
        string FilePath { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface ISettingsConfig : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// current migration version, increased by one on each deployment
        ///////////////////////////////////////////////////
        int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// min version supported by current version for migration
        ///////////////////////////////////////////////////
        int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// General DB settings
    ///////////////////////////////////////////////////
    
    public partial interface IDbSettings // ModelInterfaces.tt Line: 29
    {
        string DbSchema { get; } // ModelInterfaces.tt Line: 47
        DbIdGeneratorMethod IdGenerator { get; } // ModelInterfaces.tt Line: 47
        EnumPrimaryKeyType PKeyType { get; } // ModelInterfaces.tt Line: 47
        string KeyName { get; } // ModelInterfaces.tt Line: 47
        string Timestamp { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// if yes: 
        ///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
        /// if no:
        ///    1. Find DB type from 
        ///    2. Create connection string from db_server, db_database_name, db_user
        ///////////////////////////////////////////////////
        bool IsDbFromConnectionString { get; } // ModelInterfaces.tt Line: 47
        string ConnectionStringName { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// path to project with config file containing connection string. Usefull for UNIT tests.
        /// it will override previous settings
        ///////////////////////////////////////////////////
        string PathToProjectWithConnectionString { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IConfigShortHistory : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        IConfig CurrentConfig { get; } // ModelInterfaces.tt Line: 51
        IConfig PrevStableConfig { get; } // ModelInterfaces.tt Line: 51
        IConfig OldStableConfig { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IGroupListBaseConfigLinks : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IBaseConfigLink> ListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IBaseConfigLink : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IConfig Config { get; } // ModelInterfaces.tt Line: 51
        string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    
    public partial interface IConfig : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        int Version { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// GENERAL DB SETTINGS
        ///////////////////////////////////////////////////
        IDbSettings DbSettings { get; } // ModelInterfaces.tt Line: 51
        IGroupListBaseConfigLinks GroupConfigLinks { get; } // ModelInterfaces.tt Line: 51
        IConfigModel Model { get; } // ModelInterfaces.tt Line: 51
        IGroupListPlugins GroupPlugins { get; } // ModelInterfaces.tt Line: 51
        IGroupListAppSolutions GroupAppSolutions { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IAppDbSettings // ModelInterfaces.tt Line: 29
    {
        string PluginGuid { get; } // ModelInterfaces.tt Line: 47
        string PluginName { get; } // ModelInterfaces.tt Line: 47
        string Version { get; } // ModelInterfaces.tt Line: 47
        string PluginGenGuid { get; } // ModelInterfaces.tt Line: 47
        string PluginGenName { get; } // ModelInterfaces.tt Line: 47
        string ConnGuid { get; } // ModelInterfaces.tt Line: 47
        string ConnName { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGroupListAppSolutions : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IAppDbSettings DefaultDb { get; } // ModelInterfaces.tt Line: 51
        
        ///////////////////////////////////////////////////
        /// List NET solutions
        ///////////////////////////////////////////////////
        IEnumerable<IAppSolution> ListAppSolutions { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// message proto_plugins_group {
    /// // @attr [PropertyOrderAttribute(1)]
    /// // @attr [DisplayName("Group")]
    /// // @attr [Description("Group compatible plugins of one vendor")]
    /// // @attr [Editor(typeof(EditorPluginsGroupSelection), typeof(EditorPluginsGroupSelection))]
    /// string plugins_group_guid = 1;
    /// // @attr [PropertyOrderAttribute(2)]
    /// // @attr [DisplayName("Info")]
    /// // @attr [Description("Plugin short information")]
    /// // @attr [ReadOnly(true)]
    /// string plugins_group_info = 2;
    /// // @attr [PropertyOrderAttribute(3)]
    /// // @attr [DisplayName("Settings")]
    /// // @attr [Description("Global settings for plugin generators")]
    /// // @attr [ExpandableObjectAttribute()]
    /// string plugins_global_settins = 3;
    /// }
    ///////////////////////////////////////////////////
    
    public partial interface IAppSolution : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// List NET projects
        ///////////////////////////////////////////////////
        string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 47
        IAppDbSettings DefaultDb { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IAppProject : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 47
        IAppDbSettings DefaultDb { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IAppProjectGenerator : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        string PluginGuid { get; } // ModelInterfaces.tt Line: 47
        string DescriptionPlugin { get; } // ModelInterfaces.tt Line: 47
        string PluginGeneratorGuid { get; } // ModelInterfaces.tt Line: 47
        string DescriptionGenerator { get; } // ModelInterfaces.tt Line: 47
        string RelativePathToGeneratedFile { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// 
        ///////////////////////////////////////////////////
        string GeneratorSettings { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGeneratorSettings : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid is plugin guid
        /// AppGeneratorGuid is Guid for file generator
        ///////////////////////////////////////////////////
        string AppGeneratorGuid { get; } // ModelInterfaces.tt Line: 47
        string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 47
        string Settings { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    
    public partial interface IConfigModel : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        int Version { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListCommon GroupCommon { get; } // ModelInterfaces.tt Line: 51
        IGroupListConstants GroupConstants { get; } // ModelInterfaces.tt Line: 51
        IGroupListEnumerations GroupEnumerations { get; } // ModelInterfaces.tt Line: 51
        IGroupListCatalogs GroupCatalogs { get; } // ModelInterfaces.tt Line: 51
        IGroupDocuments GroupDocuments { get; } // ModelInterfaces.tt Line: 51
        IGroupListJournals GroupJournals { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IDataType // ModelInterfaces.tt Line: 29
    {
        EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 47
        uint Length { get; } // ModelInterfaces.tt Line: 47
        uint Accuracy { get; } // ModelInterfaces.tt Line: 47
        bool IsPositive { get; } // ModelInterfaces.tt Line: 47
        string ObjectGuid { get; } // ModelInterfaces.tt Line: 47
        bool IsNullable { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<string> ListObjectGuids { get; } // ModelInterfaces.tt Line: 42
        bool IsIndexFk { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListCommon : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListRoles GroupRoles { get; } // ModelInterfaces.tt Line: 51
        IGroupListMainViewForms GroupViewForms { get; } // ModelInterfaces.tt Line: 51
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    
    public partial interface IRole : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGroupListRoles : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IRole> ListRoles { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    
    public partial interface IMainViewForm : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListMainViewForms GroupListViewForms { get; } // ModelInterfaces.tt Line: 51
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListMainViewForms : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IMainViewForm> ListMainViewForms { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListPropertiesTabs : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPropertiesTab> ListPropertiesTabs { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPropertiesTab : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 51
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 51
        
        ///////////////////////////////////////////////////
        /// Create Index for foreign key navigation property
        ///////////////////////////////////////////////////
        bool IsIndexFk { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListProperties : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IProperty> ListProperties { get; } // ModelInterfaces.tt Line: 44
        
        ///////////////////////////////////////////////////
        /// Last generated Protobuf field position
        ///////////////////////////////////////////////////
        uint LastGenPosition { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IProperty : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IDataType DataType { get; } // ModelInterfaces.tt Line: 51
        
        ///////////////////////////////////////////////////
        /// Protobuf field position
        /// Reserved positions: 1 - primary key
        ///////////////////////////////////////////////////
        uint Position { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListConstants : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IConstant> ListConstants { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IDataType DataType { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListEnumerations : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IEnumeration> ListEnumerations { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IEnumeration : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// Enumeration element type
        ///////////////////////////////////////////////////
        EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// Length of string if 'STRING' is selected as enumeration element type
        ///////////////////////////////////////////////////
        int DataTypeLength { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IEnumerationPair> ListEnumerationPairs { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IEnumerationPair : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// TODO struct for different types, at least INTEGER
        ///////////////////////////////////////////////////
        string Value { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface ICatalog : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 51
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 51
        IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 51
        IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListCatalogs : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupDocuments : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListProperties GroupSharedProperties { get; } // ModelInterfaces.tt Line: 51
        IGroupListDocuments GroupListDocuments { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IDocument : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 51
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 51
        IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 51
        IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListDocuments : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListJournals : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IJournal> ListJournals { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IJournal : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListForms : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IForm> ListForms { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IForm : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_forms = 7;
        ///////////////////////////////////////////////////
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListReports : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IReport> ListReports { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IReport : IValidatableWithSeverity, IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_documents = 7;
        ///////////////////////////////////////////////////
        IEnumerable<IGeneratorSettings> ListGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 29
    {
        string GroupName { get; } // ModelInterfaces.tt Line: 47
        string Name { get; } // ModelInterfaces.tt Line: 47
        string Guid { get; } // ModelInterfaces.tt Line: 47
        bool IsIncluded { get; } // ModelInterfaces.tt Line: 47
    }
}