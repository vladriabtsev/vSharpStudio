using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 11
{
    public enum EnumPrimaryKeyType // ModelInterfaces.tt Line: 15
    {
        INT = 0,
        LONG = 1,
    }
    public enum DbIdGeneratorMethod // ModelInterfaces.tt Line: 15
    {
        Identity = 0,
        HiLo = 1,
    }
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
        INTEGER_VALUE = 0,
        SHORT_VALUE = 1,
        BYTE_VALUE = 2,
        STRING_VALUE = 3,
    }
    public enum EnumCatalogTreeIcon // ModelInterfaces.tt Line: 15
    {
        None = 0,
        Item = 1,
        Folder = 2,
        Custom = 3,
    }
    public enum EnumCatalogCodeUniqueScope // ModelInterfaces.tt Line: 15
    {
        NoScope = 0,
        Group = 1,
        Catalog = 2,
    }
    public enum EnumCatalogCodeType // ModelInterfaces.tt Line: 15
    {
        Number = 0,
        Text = 1,
        AutoNumber = 2,
        AutoText = 3,
    }
    public enum FormOrientation // ModelInterfaces.tt Line: 15
    {
        Vertical = 0,
        Horizontal = 1,
    }
    public enum FormView // ModelInterfaces.tt Line: 15
    {
        Selection = 0,
        Editing = 1,
    }
    
    public partial interface IUserSettings // ModelInterfaces.tt Line: 29
    {
    	IReadOnlyList<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IUserSettingsOpenedConfig // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } // ModelInterfaces.tt Line: 51
    	string ConfigPath { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IGroupListPlugins // ModelInterfaces.tt Line: 29
    {
    	IReadOnlyList<IPlugin> ListPlugins { get; } // ModelInterfaces.tt Line: 44
    	IPlugin this[int index] { get; }
    	int Count();
    }
    
    public partial interface IPlugin // ModelInterfaces.tt Line: 29
    {
    	string Version { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGenerator> ListGenerators { get; } // ModelInterfaces.tt Line: 44
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IPluginGenerator // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface ISettingsConfig // ModelInterfaces.tt Line: 29
    {
    	string Name { get; } // ModelInterfaces.tt Line: 51
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// current migration version, increased by one on each deployment
    	///////////////////////////////////////////////////
    	int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// min version supported by current version for migration
    	///////////////////////////////////////////////////
    	int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IConfigShortHistory // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	string Name { get; } // ModelInterfaces.tt Line: 51
    	IConfig CurrentConfig { get; } // ModelInterfaces.tt Line: 55
    	IConfig PrevStableConfig { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGroupListBaseConfigLinks // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IBaseConfigLink> ListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 44
    	IBaseConfigLink this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IBaseConfigLink // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IConfig ConfigBase { get; } // ModelInterfaces.tt Line: 55
    	string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    
    public partial interface IConfig : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	int Version { get; } // ModelInterfaces.tt Line: 51
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 51
    	bool IsNeedCurrentUpdate { get; } // ModelInterfaces.tt Line: 51
    	IGroupListBaseConfigLinks GroupConfigLinks { get; } // ModelInterfaces.tt Line: 55
    	IModel Model { get; } // ModelInterfaces.tt Line: 55
    	IGroupListPlugins GroupPlugins { get; } // ModelInterfaces.tt Line: 55
    	IGroupListAppSolutions GroupAppSolutions { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IAppDbSettings // ModelInterfaces.tt Line: 29
    {
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 51
    	string PluginName { get; } // ModelInterfaces.tt Line: 51
    	string Version { get; } // ModelInterfaces.tt Line: 51
    	string PluginGenGuid { get; } // ModelInterfaces.tt Line: 51
    	string PluginGenName { get; } // ModelInterfaces.tt Line: 51
    	string ConnGuid { get; } // ModelInterfaces.tt Line: 51
    	string ConnName { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IPluginGroupGeneratorsDefaultSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Guid of group generators
    	///////////////////////////////////////////////////
    	string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 51
    	string Settings { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IGroupListAppSolutions // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// List NET solutions
    	///////////////////////////////////////////////////
    	IReadOnlyList<IAppSolution> ListAppSolutions { get; } // ModelInterfaces.tt Line: 44
    	IAppSolution this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGroupGeneratorsDefaultSettings> ListGroupGeneratorsDefultSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPluginGroupGeneratorsSettings // ModelInterfaces.tt Line: 29
    {
    	string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 51
    	string Settings { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IAppSolution // ModelInterfaces.tt Line: 29
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// List NET projects
    	/// App solution relative path to configuration file path
    	///////////////////////////////////////////////////
    	string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IPluginGroupGeneratorsSettings> ListGroupGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IAppProject // ModelInterfaces.tt Line: 29
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// App project relative path to .net solution file path
    	///////////////////////////////////////////////////
    	string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	string Namespace { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPluginGeneratorNodeSettings // ModelInterfaces.tt Line: 29
    {
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 51
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// string node_settings_vm_guid = 6;
    	///////////////////////////////////////////////////
    	string Settings { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IPluginGeneratorSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	string Name { get; } // ModelInterfaces.tt Line: 51
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 51
    	string Settings { get; } // ModelInterfaces.tt Line: 51
    }
    
    ///////////////////////////////////////////////////
    /// Application project generator
    ///////////////////////////////////////////////////
    
    public partial interface IAppProjectGenerator // ModelInterfaces.tt Line: 29
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 51
    	string DescriptionPlugin { get; } // ModelInterfaces.tt Line: 51
    	string PluginGeneratorGuid { get; } // ModelInterfaces.tt Line: 51
    	string DescriptionGenerator { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Relative folder path to project file
    	///////////////////////////////////////////////////
    	string RelativePathToGenFolder { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Generator output file name
    	///////////////////////////////////////////////////
    	string GenFileName { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	string GeneratorSettings { get; } // ModelInterfaces.tt Line: 51
    	IPluginGeneratorSettings GeneratorSettingsVm { get; } // ModelInterfaces.tt Line: 55
    	string ConnStr { get; } // ModelInterfaces.tt Line: 51
    	string PluginGroupSettingsGuid { get; } // ModelInterfaces.tt Line: 51
    	string ConnStrToPrevStable { get; } // ModelInterfaces.tt Line: 51
    	bool IsGenerateSqlSqriptToUpdatePrevStable { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Generator output file name
    	///////////////////////////////////////////////////
    	string GenScriptFileName { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings // ModelInterfaces.tt Line: 29
    {
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 51
    	string Settings { get; } // ModelInterfaces.tt Line: 51
    }
    
    ///////////////////////////////////////////////////
    /// General DB settings
    ///////////////////////////////////////////////////
    
    public partial interface IDbSettings // ModelInterfaces.tt Line: 29
    {
    	string DbSchema { get; } // ModelInterfaces.tt Line: 51
    	DbIdGeneratorMethod IdGenerator { get; } // ModelInterfaces.tt Line: 51
    	EnumPrimaryKeyType PKeyType { get; } // ModelInterfaces.tt Line: 51
    	string PKeyName { get; } // ModelInterfaces.tt Line: 51
    	string PKeyGuid { get; } // ModelInterfaces.tt Line: 51
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    
    public partial interface IModel : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	int Version { get; } // ModelInterfaces.tt Line: 51
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	uint CompositeNameMaxLength { get; } // ModelInterfaces.tt Line: 51
    	bool IsUseCompositeNames { get; } // ModelInterfaces.tt Line: 51
    	bool IsUseGroupPrefix { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// GENERAL DB SETTINGS
    	///////////////////////////////////////////////////
    	IDbSettings DbSettings { get; } // ModelInterfaces.tt Line: 55
    	IGroupListCommon GroupCommon { get; } // ModelInterfaces.tt Line: 55
    	IGroupListConstants GroupConstants { get; } // ModelInterfaces.tt Line: 55
    	IGroupListEnumerations GroupEnumerations { get; } // ModelInterfaces.tt Line: 55
    	IGroupListCatalogs GroupCatalogs { get; } // ModelInterfaces.tt Line: 55
    	IGroupDocuments GroupDocuments { get; } // ModelInterfaces.tt Line: 55
    	IGroupListJournals GroupJournals { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IDataType // ModelInterfaces.tt Line: 29
    {
    	EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 51
    	uint Length { get; } // ModelInterfaces.tt Line: 51
    	uint Accuracy { get; } // ModelInterfaces.tt Line: 51
    	string ObjectGuid { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<string> ListObjectGuids { get; } // ModelInterfaces.tt Line: 42
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 51
    	bool IsPositive { get; } // ModelInterfaces.tt Line: 51
    	bool IsNullable { get; } // ModelInterfaces.tt Line: 51
    	bool IsPKey { get; } // ModelInterfaces.tt Line: 51
    	bool IsRefParent { get; } // ModelInterfaces.tt Line: 51
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListCommon : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IGroupListRoles GroupRoles { get; } // ModelInterfaces.tt Line: 55
    	IGroupListMainViewForms GroupViewForms { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    
    public partial interface IRole : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListRoles : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IRole> ListRoles { get; } // ModelInterfaces.tt Line: 44
    	IRole this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    
    public partial interface IMainViewForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IGroupListMainViewForms GroupListViewForms { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListMainViewForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IMainViewForm> ListMainViewForms { get; } // ModelInterfaces.tt Line: 44
    	IMainViewForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListPropertiesTabs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPropertiesTab> ListPropertiesTabs { get; } // ModelInterfaces.tt Line: 44
    	IPropertiesTab this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPropertiesTab : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 55
    	
    	///////////////////////////////////////////////////
    	/// Create Index for foreign key navigation property
    	///////////////////////////////////////////////////
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListProperties : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IProperty> ListProperties { get; } // ModelInterfaces.tt Line: 44
    	IProperty this[int index] { get; }
    	int Count();
    	
    	///////////////////////////////////////////////////
    	/// Last generated Protobuf field position
    	///////////////////////////////////////////////////
    	uint LastGenPosition { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IProperty : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Protobuf field position
    	/// Reserved positions: 1 - primary key
    	///////////////////////////////////////////////////
    	uint Position { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListConstants : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IConstant> ListConstants { get; } // ModelInterfaces.tt Line: 44
    	IConstant this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListEnumerations : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IEnumeration> ListEnumerations { get; } // ModelInterfaces.tt Line: 44
    	IEnumeration this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IEnumeration : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Enumeration element type
    	///////////////////////////////////////////////////
    	EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Length of string if 'STRING' is selected as enumeration element type
    	///////////////////////////////////////////////////
    	int DataTypeLength { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IEnumerationPair> ListEnumerationPairs { get; } // ModelInterfaces.tt Line: 44
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IEnumerationPair : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string Value { get; } // ModelInterfaces.tt Line: 51
    	bool IsDefault { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface ICatalogFolder : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	bool? UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	ICatalogCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 55
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 51
    	bool? UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxNameLength { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 51
    	bool? UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxDescriptionLength { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyParentGuid { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface ICatalogCodePropertySettings // ModelInterfaces.tt Line: 29
    {
    	EnumCatalogCodeType Type { get; } // ModelInterfaces.tt Line: 51
    	uint Length { get; } // ModelInterfaces.tt Line: 51
    	string SequenceGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumCatalogCodeUniqueScope UniqueScope { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface ICatalog : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	ICatalogFolder Folder { get; } // ModelInterfaces.tt Line: 55
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 55
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 55
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 55
    	EnumCatalogTreeIcon ItemIconType { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	bool UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	ICatalogCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 55
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 51
    	bool UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxNameLength { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 51
    	bool UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxDescriptionLength { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 51
    	bool UseFolderTypeExplicitly { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsFolderGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsOpenGuid { get; } // ModelInterfaces.tt Line: 51
    	bool UseTree { get; } // ModelInterfaces.tt Line: 51
    	EnumCatalogTreeIcon GroupIconType { get; } // ModelInterfaces.tt Line: 51
    	uint MaxTreeLevels { get; } // ModelInterfaces.tt Line: 51
    	bool UseSeparatePropertiesForGroups { get; } // ModelInterfaces.tt Line: 51
    	string PropertyParentGuid { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 44
    	ICatalog this[int index] { get; }
    	int Count();
    	string PropertyCodeName { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameName { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionName { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsFolderName { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsOpenName { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 51
    	IGroupListProperties GroupSharedProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListDocuments GroupListDocuments { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IDocument : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 55
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 55
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 44
    	IDocument this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListJournals : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IJournal> ListJournals { get; } // ModelInterfaces.tt Line: 44
    	IJournal this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IJournal : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 44
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IForm> ListForms { get; } // ModelInterfaces.tt Line: 44
    	IForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IFormMarging // ModelInterfaces.tt Line: 29
    {
    	int Left { get; } // ModelInterfaces.tt Line: 51
    	int Up { get; } // ModelInterfaces.tt Line: 51
    	int Right { get; } // ModelInterfaces.tt Line: 51
    	int Bottom { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IFormPadding // ModelInterfaces.tt Line: 29
    {
    	int Left { get; } // ModelInterfaces.tt Line: 51
    	int Up { get; } // ModelInterfaces.tt Line: 51
    	int Right { get; } // ModelInterfaces.tt Line: 51
    	int Bottom { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IFormStackpanel // ModelInterfaces.tt Line: 29
    {
    	FormOrientation Orientation { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IFormGrid // ModelInterfaces.tt Line: 29
    {
    	IFormMarging Marging { get; } // ModelInterfaces.tt Line: 55
    	IFormPadding Padding { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IFormDatagrid // ModelInterfaces.tt Line: 29
    {
    	IFormMarging Marging { get; } // ModelInterfaces.tt Line: 55
    	IFormPadding Padding { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// 
    	/// repeated proto_group_properties list_properties = 6;
    	/// repeated proto_document list_forms = 7;
    	///////////////////////////////////////////////////
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	FormView EnumFormType { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 42
    	IReadOnlyList<string> ListGuidTreeProperties { get; } // ModelInterfaces.tt Line: 42
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListReports : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IReport> ListReports { get; } // ModelInterfaces.tt Line: 44
    	IReport this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IReport : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	/// repeated proto_document list_documents = 7;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 29
    {
    	string GroupName { get; } // ModelInterfaces.tt Line: 51
    	string Name { get; } // ModelInterfaces.tt Line: 51
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	bool IsIncluded { get; } // ModelInterfaces.tt Line: 51
    }
}