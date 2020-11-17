using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 11
{
    public enum EnumDataType // ModelInterfaces.tt Line: 15
    {
        STRING = 0,
        NUMERICAL = 1,
        BOOL = 2,
        TIME = 3,
        TIMEZ = 4,
        DATE = 5,
        DATETIME = 6,
        DATETIMEZ = 7,
        ENUMERATION = 10,
        CATALOG = 11,
        CATALOGS = 12,
        DOCUMENT = 13,
        DOCUMENTS = 14,
        ANY = 15,
    }
    public enum EnumEnumerationType // ModelInterfaces.tt Line: 15
    {
        STRING_VALUE = 0,
        BYTE_VALUE = 1,
        SHORT_VALUE = 2,
        INTEGER_VALUE = 3,
    }
    
    public partial interface IUserSettings // ModelInterfaces.tt Line: 29
    {
        IEnumerable<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IUserSettingsOpenedConfig // ModelInterfaces.tt Line: 29
    {
        Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } // ModelInterfaces.tt Line: 53
        string ConfigPath { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListPlugins : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        IEnumerable<IPlugin> ListPlugins { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IPlugin : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Version { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPluginGenerator> ListGenerators { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IPluginGenerator : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface ISettingsConfig // ModelInterfaces.tt Line: 29
    {
        string Guid { get; } // ModelInterfaces.tt Line: 53
        string Name { get; } // ModelInterfaces.tt Line: 53
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// current migration version, increased by one on each deployment
        ///////////////////////////////////////////////////
        int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// min version supported by current version for migration
        ///////////////////////////////////////////////////
        int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IConfigShortHistory : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        IConfig CurrentConfig { get; } // ModelInterfaces.tt Line: 58
        IConfig PrevStableConfig { get; } // ModelInterfaces.tt Line: 58
    }
    
    public partial interface IGroupListBaseConfigLinks : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IBaseConfigLink> ListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IBaseConfigLink : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 53
        IConfig Config { get; } // ModelInterfaces.tt Line: 58
        string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    
    public partial interface IConfig : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        int Version { get; } // ModelInterfaces.tt Line: 53
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 53
        bool IsNeedCurrentUpdate { get; } // ModelInterfaces.tt Line: 53
        IGroupListBaseConfigLinks GroupConfigLinks { get; } // ModelInterfaces.tt Line: 58
        IConfigModel Model { get; } // ModelInterfaces.tt Line: 58
        IGroupListPlugins GroupPlugins { get; } // ModelInterfaces.tt Line: 58
        IGroupListAppSolutions GroupAppSolutions { get; } // ModelInterfaces.tt Line: 58
    }
    
    public partial interface IAppDbSettings // ModelInterfaces.tt Line: 29
    {
        string PluginGuid { get; } // ModelInterfaces.tt Line: 53
        string PluginName { get; } // ModelInterfaces.tt Line: 53
        string Version { get; } // ModelInterfaces.tt Line: 53
        string PluginGenGuid { get; } // ModelInterfaces.tt Line: 53
        string PluginGenName { get; } // ModelInterfaces.tt Line: 53
        string ConnGuid { get; } // ModelInterfaces.tt Line: 53
        string ConnName { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Stored in App node. All nullable setting has to have value
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGroupGeneratorsDefaultSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of group generators
        ///////////////////////////////////////////////////
        string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 53
        string Settings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListAppSolutions : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// List NET solutions
        ///////////////////////////////////////////////////
        IEnumerable<IAppSolution> ListAppSolutions { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGroupGeneratorsDefaultSettings> ListGroupGeneratorsDefultSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    ///////////////////////////////////////////////////
    /// Stored in AppSolution node. All null setting will use parent value
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGroupGeneratorsSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of group generators
        ///////////////////////////////////////////////////
        string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 53
        string Settings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IAppSolution : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// List NET projects
        /// App solution relative path to configuration file path
        ///////////////////////////////////////////////////
        string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGroupGeneratorsSettings> ListGroupGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IAppProject : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// App project relative path to .net solution file path
        ///////////////////////////////////////////////////
        string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 53
        string Namespace { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 49
    }
    
    ///////////////////////////////////////////////////
    /// Stored in each node in ConfigModel branch
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGeneratorNodeSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// Name of solution-project-generator node
        /// string name = 2;
        ///////////////////////////////////////////////////
        string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 53
        string Settings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Stored in AppProjectGenerator node
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGeneratorMainSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 53
        string Settings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Application project generator
    ///////////////////////////////////////////////////
    
    public partial interface IAppProjectGenerator : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 53
        string PluginGuid { get; } // ModelInterfaces.tt Line: 53
        string DescriptionPlugin { get; } // ModelInterfaces.tt Line: 53
        string PluginGeneratorGuid { get; } // ModelInterfaces.tt Line: 53
        string DescriptionGenerator { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// Relative folder path to project file
        ///////////////////////////////////////////////////
        string RelativePathToGenFolder { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        string GenFileName { get; } // ModelInterfaces.tt Line: 53
        string GeneratorSettings { get; } // ModelInterfaces.tt Line: 53
        IPluginGeneratorMainSettings MainSettings { get; } // ModelInterfaces.tt Line: 58
        string ConnStr { get; } // ModelInterfaces.tt Line: 53
        bool IsGenerateSqlSqriptToUpdatePrevStable { get; } // ModelInterfaces.tt Line: 53
        string ConnStrToPrevStable { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        string GenScriptFileName { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 53
        string Settings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    
    public partial interface IConfigModel : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        int Version { get; } // ModelInterfaces.tt Line: 53
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        uint CompositeNameMaxLength { get; } // ModelInterfaces.tt Line: 53
        bool IsCompositeNames { get; } // ModelInterfaces.tt Line: 53
        bool IsUseGroupPrefix { get; } // ModelInterfaces.tt Line: 53
        IGroupListCommon GroupCommon { get; } // ModelInterfaces.tt Line: 58
        IGroupListConstants GroupConstants { get; } // ModelInterfaces.tt Line: 58
        IGroupListEnumerations GroupEnumerations { get; } // ModelInterfaces.tt Line: 58
        IGroupListCatalogs GroupCatalogs { get; } // ModelInterfaces.tt Line: 58
        IGroupDocuments GroupDocuments { get; } // ModelInterfaces.tt Line: 58
        IGroupListJournals GroupJournals { get; } // ModelInterfaces.tt Line: 58
    }
    
    public partial interface IDataType // ModelInterfaces.tt Line: 29
    {
        EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 53
        uint Length { get; } // ModelInterfaces.tt Line: 53
        uint Accuracy { get; } // ModelInterfaces.tt Line: 53
        string ObjectGuid { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<string> ListObjectGuids { get; } // ModelInterfaces.tt Line: 47
        EnumEnumerationType EnumerationType { get; } // ModelInterfaces.tt Line: 53
        bool IsIndexFk { get; } // ModelInterfaces.tt Line: 53
        bool IsPositive { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// bool is_nullable = 12;
        ///////////////////////////////////////////////////
        bool IsNullable { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListCommon : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IGroupListRoles GroupRoles { get; } // ModelInterfaces.tt Line: 58
        IGroupListMainViewForms GroupViewForms { get; } // ModelInterfaces.tt Line: 58
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    
    public partial interface IRole : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListRoles : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IRole> ListRoles { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    
    public partial interface IMainViewForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IGroupListMainViewForms GroupListViewForms { get; } // ModelInterfaces.tt Line: 58
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListMainViewForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IMainViewForm> ListMainViewForms { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListPropertiesTabs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPropertiesTab> ListPropertiesTabs { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IPropertiesTab : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 58
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 58
        
        ///////////////////////////////////////////////////
        /// Create Index for foreign key navigation property
        ///////////////////////////////////////////////////
        bool IsIndexFk { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListProperties : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IProperty> ListProperties { get; } // ModelInterfaces.tt Line: 49
        
        ///////////////////////////////////////////////////
        /// Last generated Protobuf field position
        ///////////////////////////////////////////////////
        uint LastGenPosition { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IProperty : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IDataType DataType { get; } // ModelInterfaces.tt Line: 58
        
        ///////////////////////////////////////////////////
        /// Protobuf field position
        /// Reserved positions: 1 - primary key
        ///////////////////////////////////////////////////
        uint Position { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListConstants : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IConstant> ListConstants { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IDataType DataType { get; } // ModelInterfaces.tt Line: 58
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListEnumerations : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IEnumeration> ListEnumerations { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IEnumeration : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// Enumeration element type
        ///////////////////////////////////////////////////
        EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// Length of string if 'STRING' is selected as enumeration element type
        ///////////////////////////////////////////////////
        int DataTypeLength { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IEnumerationPair> ListEnumerationPairs { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IEnumerationPair : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        string Value { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface ICatalog : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 58
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 58
        IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 58
        IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 58
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 53
        IGroupListProperties GroupSharedProperties { get; } // ModelInterfaces.tt Line: 58
        IGroupListDocuments GroupListDocuments { get; } // ModelInterfaces.tt Line: 58
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IDocument : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 58
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 58
        IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 58
        IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 58
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        IEnumerable<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListJournals : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IJournal> ListJournals { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IJournal : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IForm> ListForms { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_forms = 7;
        ///////////////////////////////////////////////////
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListReports : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IReport> ListReports { get; } // ModelInterfaces.tt Line: 49
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IReport : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string NameUi { get; } // ModelInterfaces.tt Line: 53
        string Description { get; } // ModelInterfaces.tt Line: 53
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_documents = 7;
        ///////////////////////////////////////////////////
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 49
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 29
    {
        string GroupName { get; } // ModelInterfaces.tt Line: 53
        string Name { get; } // ModelInterfaces.tt Line: 53
        string Guid { get; } // ModelInterfaces.tt Line: 53
        bool IsIncluded { get; } // ModelInterfaces.tt Line: 53
    }
}