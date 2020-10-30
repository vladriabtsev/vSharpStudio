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
    
    public partial interface IUserSettings // ModelInterfaces.tt Line: 29
    {
        IEnumerable<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IUserSettingsOpenedConfig // ModelInterfaces.tt Line: 29
    {
        Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } // ModelInterfaces.tt Line: 47
        string ConfigPath { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGroupListPlugins : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPlugin> ListPlugins { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IPlugin : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Version { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGenerator> ListGenerators { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IPluginGenerator : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        string Description { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface ISettingsConfig // ModelInterfaces.tt Line: 29
    {
        string Guid { get; } // ModelInterfaces.tt Line: 47
        string Name { get; } // ModelInterfaces.tt Line: 47
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
    
    public partial interface IConfigShortHistory : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        IConfig CurrentConfig { get; } // ModelInterfaces.tt Line: 51
        IConfig PrevStableConfig { get; } // ModelInterfaces.tt Line: 51
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGroupListBaseConfigLinks : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IBaseConfigLink> ListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IBaseConfigLink : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IConfig Config { get; } // ModelInterfaces.tt Line: 51
        string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    
    public partial interface IConfig : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        int Version { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
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
    
    ///////////////////////////////////////////////////
    /// Stored in App node. All nullable setting has to have value
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGroupGeneratorsDefaultSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of group generators
        ///////////////////////////////////////////////////
        string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 47
        string Settings { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGroupListAppSolutions : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// List NET solutions
        ///////////////////////////////////////////////////
        IEnumerable<IAppSolution> ListAppSolutions { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGroupGeneratorsDefaultSettings> ListGroupGeneratorsDefultSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Stored in AppSolution node. All null setting will use parent value
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGroupGeneratorsSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of group generators
        ///////////////////////////////////////////////////
        string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 47
        string Settings { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IAppSolution : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// List NET projects
        /// App solution relative path to configuration file path
        ///////////////////////////////////////////////////
        string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 44
        IEnumerable<IPluginGroupGeneratorsSettings> ListGroupGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IAppProject : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// App project relative path to .net solution file path
        ///////////////////////////////////////////////////
        string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        string Namespace { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Stored in each node in ConfigModel branch
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGeneratorNodeSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// Name of solution-project-generator node
        /// string name = 2;
        ///////////////////////////////////////////////////
        string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 47
        string Settings { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// Stored in AppProjectGenerator node
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGeneratorMainSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 47
        string Settings { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// Application project generator
    ///////////////////////////////////////////////////
    
    public partial interface IAppProjectGenerator : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        string PluginGuid { get; } // ModelInterfaces.tt Line: 47
        string DescriptionPlugin { get; } // ModelInterfaces.tt Line: 47
        string PluginGeneratorGuid { get; } // ModelInterfaces.tt Line: 47
        string DescriptionGenerator { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// Relative folder path to project file
        ///////////////////////////////////////////////////
        string RelativePathToGenFolder { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        string GenFileName { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        string GeneratorSettings { get; } // ModelInterfaces.tt Line: 47
        IPluginGeneratorMainSettings MainSettings { get; } // ModelInterfaces.tt Line: 51
        string ConnStr { get; } // ModelInterfaces.tt Line: 47
        bool IsGenerateSqlSqriptToUpdatePrevStable { get; } // ModelInterfaces.tt Line: 47
        string ConnStrToPrevStable { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        string GenScriptFileName { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings // ModelInterfaces.tt Line: 29
    {
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 47
        string Settings { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    
    public partial interface IConfigModel : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        int Version { get; } // ModelInterfaces.tt Line: 47
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        uint CompositeNameMaxLength { get; } // ModelInterfaces.tt Line: 47
        bool IsCompositeNames { get; } // ModelInterfaces.tt Line: 47
        bool IsUseGroupPrefix { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
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
        EnumEnumerationType EnumerationType { get; } // ModelInterfaces.tt Line: 47
        bool IsIndexFk { get; } // ModelInterfaces.tt Line: 47
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListCommon : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListRoles GroupRoles { get; } // ModelInterfaces.tt Line: 51
        IGroupListMainViewForms GroupViewForms { get; } // ModelInterfaces.tt Line: 51
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    
    public partial interface IRole : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListRoles : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IRole> ListRoles { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    
    public partial interface IMainViewForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IGroupListMainViewForms GroupListViewForms { get; } // ModelInterfaces.tt Line: 51
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListMainViewForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IMainViewForm> ListMainViewForms { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListPropertiesTabs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPropertiesTab> ListPropertiesTabs { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPropertiesTab : IGuid, IName // ModelInterfaces.tt Line: 29
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
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListProperties : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IProperty> ListProperties { get; } // ModelInterfaces.tt Line: 44
        
        ///////////////////////////////////////////////////
        /// Last generated Protobuf field position
        ///////////////////////////////////////////////////
        uint LastGenPosition { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IProperty : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IDataType DataType { get; } // ModelInterfaces.tt Line: 51
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// Protobuf field position
        /// Reserved positions: 1 - primary key
        ///////////////////////////////////////////////////
        uint Position { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListConstants : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IConstant> ListConstants { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IDataType DataType { get; } // ModelInterfaces.tt Line: 51
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListEnumerations : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IEnumeration> ListEnumerations { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IEnumeration : IGuid, IName // ModelInterfaces.tt Line: 29
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
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IEnumerationPair : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        string Value { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface ICatalog : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 51
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 51
        IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 51
        IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 47
        IGroupListProperties GroupSharedProperties { get; } // ModelInterfaces.tt Line: 51
        IGroupListDocuments GroupListDocuments { get; } // ModelInterfaces.tt Line: 51
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IDocument : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 51
        IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 51
        IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 51
        IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 51
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListJournals : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IJournal> ListJournals { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IJournal : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IForm> ListForms { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_forms = 7;
        ///////////////////////////////////////////////////
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListReports : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        IEnumerable<IReport> ListReports { get; } // ModelInterfaces.tt Line: 44
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IReport : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        ulong SortingValue { get; } // ModelInterfaces.tt Line: 47
        string NameUi { get; } // ModelInterfaces.tt Line: 47
        string Description { get; } // ModelInterfaces.tt Line: 47
        bool IsNew { get; } // ModelInterfaces.tt Line: 47
        bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasNew { get; } // ModelInterfaces.tt Line: 47
        bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 47
        bool IsHasChanged { get; } // ModelInterfaces.tt Line: 47
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_documents = 7;
        ///////////////////////////////////////////////////
        IEnumerable<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 29
    {
        string GroupName { get; } // ModelInterfaces.tt Line: 47
        string Name { get; } // ModelInterfaces.tt Line: 47
        string Guid { get; } // ModelInterfaces.tt Line: 47
        bool IsIncluded { get; } // ModelInterfaces.tt Line: 47
    }
}