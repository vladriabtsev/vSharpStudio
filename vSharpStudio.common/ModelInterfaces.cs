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
    	IReadOnlyList<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IUserSettingsOpenedConfig // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 60
    	Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } // ModelInterfaces.tt Line: 60
    	string ConfigPath { get; } // ModelInterfaces.tt Line: 60
    }
    
    public partial interface IGroupListPlugins // ModelInterfaces.tt Line: 29
    {
    	IReadOnlyList<IPlugin> ListPlugins { get; } // ModelInterfaces.tt Line: 53
    	IPlugin this[int index] { get; }
    	int Count();
    }
    
    public partial interface IPlugin // ModelInterfaces.tt Line: 29
    {
    	string Version { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGenerator> ListGenerators { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IPluginGenerator // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 60
    }
    
    public partial interface ISettingsConfig // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 60
    	string Name { get; } // ModelInterfaces.tt Line: 60
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// current migration version, increased by one on each deployment
    	///////////////////////////////////////////////////
    	int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// min version supported by current version for migration
    	///////////////////////////////////////////////////
    	int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 60
    }
    
    public partial interface IConfigShortHistory // ModelInterfaces.tt Line: 29
    {
    	IConfig CurrentConfig { get; } // ModelInterfaces.tt Line: 64
    	IConfig PrevStableConfig { get; } // ModelInterfaces.tt Line: 64
    }
    
    public partial interface IGroupListBaseConfigLinks // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IBaseConfigLink> ListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 53
    	IBaseConfigLink this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IBaseConfigLink // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IConfig Config { get; } // ModelInterfaces.tt Line: 64
    	string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    
    public partial interface IConfig : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	int Version { get; } // ModelInterfaces.tt Line: 60
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 60
    	bool IsNeedCurrentUpdate { get; } // ModelInterfaces.tt Line: 60
    	IGroupListBaseConfigLinks GroupConfigLinks { get; } // ModelInterfaces.tt Line: 64
    	IConfigModel Model { get; } // ModelInterfaces.tt Line: 64
    	IGroupListPlugins GroupPlugins { get; } // ModelInterfaces.tt Line: 64
    	IGroupListAppSolutions GroupAppSolutions { get; } // ModelInterfaces.tt Line: 64
    }
    
    public partial interface IAppDbSettings // ModelInterfaces.tt Line: 29
    {
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 60
    	string PluginName { get; } // ModelInterfaces.tt Line: 60
    	string Version { get; } // ModelInterfaces.tt Line: 60
    	string PluginGenGuid { get; } // ModelInterfaces.tt Line: 60
    	string PluginGenName { get; } // ModelInterfaces.tt Line: 60
    	string ConnGuid { get; } // ModelInterfaces.tt Line: 60
    	string ConnName { get; } // ModelInterfaces.tt Line: 60
    }
    
    ///////////////////////////////////////////////////
    /// Stored in App node. All nullable setting has to have value
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGroupGeneratorsDefaultSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	
    	///////////////////////////////////////////////////
    	/// Guid of group generators
    	///////////////////////////////////////////////////
    	string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 60
    	string Settings { get; } // ModelInterfaces.tt Line: 60
    }
    
    public partial interface IGroupListAppSolutions // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// List NET solutions
    	///////////////////////////////////////////////////
    	IReadOnlyList<IAppSolution> ListAppSolutions { get; } // ModelInterfaces.tt Line: 53
    	IAppSolution this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGroupGeneratorsDefaultSettings> ListGroupGeneratorsDefultSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Stored in AppSolution node. All null setting will use parent value
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGroupGeneratorsSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	
    	///////////////////////////////////////////////////
    	/// Guid of group generators
    	///////////////////////////////////////////////////
    	string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 60
    	string Settings { get; } // ModelInterfaces.tt Line: 60
    }
    
    public partial interface IAppSolution // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// List NET projects
    	/// App solution relative path to configuration file path
    	///////////////////////////////////////////////////
    	string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 53
    	IReadOnlyList<IPluginGroupGeneratorsSettings> ListGroupGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IAppProject // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// App project relative path to .net solution file path
    	///////////////////////////////////////////////////
    	string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 60
    	string Namespace { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Stored in each node in ConfigModel branch
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGeneratorNodeSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// Name of solution-project-generator node
    	/// string name = 2;
    	///////////////////////////////////////////////////
    	string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 60
    	string Settings { get; } // ModelInterfaces.tt Line: 60
    }
    
    ///////////////////////////////////////////////////
    /// Stored in AppProjectGenerator node
    ///////////////////////////////////////////////////
    
    public partial interface IPluginGeneratorMainSettings : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 60
    	string Settings { get; } // ModelInterfaces.tt Line: 60
    }
    
    ///////////////////////////////////////////////////
    /// Application project generator
    ///////////////////////////////////////////////////
    
    public partial interface IAppProjectGenerator : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 60
    	string DescriptionPlugin { get; } // ModelInterfaces.tt Line: 60
    	string PluginGeneratorGuid { get; } // ModelInterfaces.tt Line: 60
    	string DescriptionGenerator { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// Relative folder path to project file
    	///////////////////////////////////////////////////
    	string RelativePathToGenFolder { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// Generator output file name
    	///////////////////////////////////////////////////
    	string GenFileName { get; } // ModelInterfaces.tt Line: 60
    	string GeneratorSettings { get; } // ModelInterfaces.tt Line: 60
    	IPluginGeneratorMainSettings MainSettings { get; } // ModelInterfaces.tt Line: 64
    	string ConnStr { get; } // ModelInterfaces.tt Line: 60
    	bool IsGenerateSqlSqriptToUpdatePrevStable { get; } // ModelInterfaces.tt Line: 60
    	string ConnStrToPrevStable { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// Generator output file name
    	///////////////////////////////////////////////////
    	string GenScriptFileName { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings // ModelInterfaces.tt Line: 29
    {
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 60
    	string Settings { get; } // ModelInterfaces.tt Line: 60
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    
    public partial interface IConfigModel : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	int Version { get; } // ModelInterfaces.tt Line: 60
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	uint CompositeNameMaxLength { get; } // ModelInterfaces.tt Line: 60
    	bool IsCompositeNames { get; } // ModelInterfaces.tt Line: 60
    	bool IsUseGroupPrefix { get; } // ModelInterfaces.tt Line: 60
    	IGroupListCommon GroupCommon { get; } // ModelInterfaces.tt Line: 64
    	IGroupListConstants GroupConstants { get; } // ModelInterfaces.tt Line: 64
    	IGroupListEnumerations GroupEnumerations { get; } // ModelInterfaces.tt Line: 64
    	IGroupListCatalogs GroupCatalogs { get; } // ModelInterfaces.tt Line: 64
    	IGroupDocuments GroupDocuments { get; } // ModelInterfaces.tt Line: 64
    	IGroupListJournals GroupJournals { get; } // ModelInterfaces.tt Line: 64
    }
    
    public partial interface IDataType // ModelInterfaces.tt Line: 29
    {
    	EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 60
    	uint Length { get; } // ModelInterfaces.tt Line: 60
    	uint Accuracy { get; } // ModelInterfaces.tt Line: 60
    	string ObjectGuid { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<string> ListObjectGuids { get; } // ModelInterfaces.tt Line: 51
    	EnumEnumerationType EnumerationType { get; } // ModelInterfaces.tt Line: 60
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 60
    	bool IsPositive { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// bool is_nullable = 12;
    	///////////////////////////////////////////////////
    	bool IsNullable { get; } // ModelInterfaces.tt Line: 60
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListCommon : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IGroupListRoles GroupRoles { get; } // ModelInterfaces.tt Line: 64
    	IGroupListMainViewForms GroupViewForms { get; } // ModelInterfaces.tt Line: 64
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    
    public partial interface IRole : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListRoles : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IRole> ListRoles { get; } // ModelInterfaces.tt Line: 53
    	IRole this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    
    public partial interface IMainViewForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IGroupListMainViewForms GroupListViewForms { get; } // ModelInterfaces.tt Line: 64
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListMainViewForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IMainViewForm> ListMainViewForms { get; } // ModelInterfaces.tt Line: 53
    	IMainViewForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListPropertiesTabs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPropertiesTab> ListPropertiesTabs { get; } // ModelInterfaces.tt Line: 53
    	IPropertiesTab this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IPropertiesTab : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 64
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 64
    	
    	///////////////////////////////////////////////////
    	/// Create Index for foreign key navigation property
    	///////////////////////////////////////////////////
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListProperties : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IProperty> ListProperties { get; } // ModelInterfaces.tt Line: 53
    	IProperty this[int index] { get; }
    	int Count();
    	
    	///////////////////////////////////////////////////
    	/// Last generated Protobuf field position
    	///////////////////////////////////////////////////
    	uint LastGenPosition { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IProperty : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 64
    	
    	///////////////////////////////////////////////////
    	/// Protobuf field position
    	/// Reserved positions: 1 - primary key
    	///////////////////////////////////////////////////
    	uint Position { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListConstants : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IConstant> ListConstants { get; } // ModelInterfaces.tt Line: 53
    	IConstant this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 64
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListEnumerations : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IEnumeration> ListEnumerations { get; } // ModelInterfaces.tt Line: 53
    	IEnumeration this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IEnumeration : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// Enumeration element type
    	///////////////////////////////////////////////////
    	EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// Length of string if 'STRING' is selected as enumeration element type
    	///////////////////////////////////////////////////
    	int DataTypeLength { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IEnumerationPair> ListEnumerationPairs { get; } // ModelInterfaces.tt Line: 53
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IEnumerationPair : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	string Value { get; } // ModelInterfaces.tt Line: 60
    }
    
    public partial interface ICatalog : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 64
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 64
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 64
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 64
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 53
    	ICatalog this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 60
    	IGroupListProperties GroupSharedProperties { get; } // ModelInterfaces.tt Line: 64
    	IGroupListDocuments GroupListDocuments { get; } // ModelInterfaces.tt Line: 64
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IDocument : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 64
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 64
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 64
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 64
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 53
    	IDocument this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListJournals : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IJournal> ListJournals { get; } // ModelInterfaces.tt Line: 53
    	IJournal this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IJournal : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 53
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IForm> ListForms { get; } // ModelInterfaces.tt Line: 53
    	IForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	/// repeated proto_document list_forms = 7;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGroupListReports : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IReport> ListReports { get; } // ModelInterfaces.tt Line: 53
    	IReport this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IReport : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 60
    	string Description { get; } // ModelInterfaces.tt Line: 60
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	/// repeated proto_document list_documents = 7;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 29
    {
    	string GroupName { get; } // ModelInterfaces.tt Line: 60
    	string Name { get; } // ModelInterfaces.tt Line: 60
    	string Guid { get; } // ModelInterfaces.tt Line: 60
    	bool IsIncluded { get; } // ModelInterfaces.tt Line: 60
    }
}