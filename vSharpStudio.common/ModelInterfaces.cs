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
    
    public partial interface IUserSettings // ModelInterfaces.tt Line: 29
    {
    	IReadOnlyList<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IUserSettingsOpenedConfig // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } // ModelInterfaces.tt Line: 50
    	string ConfigPath { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IGroupListPlugins // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPlugin> ListPlugins { get; } // ModelInterfaces.tt Line: 43
    	IPlugin this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IPlugin // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Version { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGenerator> ListGenerators { get; } // ModelInterfaces.tt Line: 43
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IPluginGenerator // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface ISettingsConfig // ModelInterfaces.tt Line: 29
    {
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// current migration version, increased by one on each deployment
    	///////////////////////////////////////////////////
    	int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// min version supported by current version for migration
    	///////////////////////////////////////////////////
    	int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IConfigShortHistory // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	IConfig CurrentConfig { get; } // ModelInterfaces.tt Line: 54
    	IConfig PrevStableConfig { get; } // ModelInterfaces.tt Line: 54
    }
    
    public partial interface IGroupListBaseConfigLinks // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IBaseConfigLink> ListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 43
    	IBaseConfigLink this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IBaseConfigLink // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IConfig ConfigBase { get; } // ModelInterfaces.tt Line: 54
    	string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    
    public partial interface IConfig : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	int Version { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	bool IsNeedCurrentUpdate { get; } // ModelInterfaces.tt Line: 50
    	IGroupListBaseConfigLinks GroupConfigLinks { get; } // ModelInterfaces.tt Line: 54
    	IConfigModel Model { get; } // ModelInterfaces.tt Line: 54
    	IGroupListPlugins GroupPlugins { get; } // ModelInterfaces.tt Line: 54
    	IGroupListAppSolutions GroupAppSolutions { get; } // ModelInterfaces.tt Line: 54
    }
    
    public partial interface IAppDbSettings // ModelInterfaces.tt Line: 29
    {
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 50
    	string PluginName { get; } // ModelInterfaces.tt Line: 50
    	string Version { get; } // ModelInterfaces.tt Line: 50
    	string PluginGenGuid { get; } // ModelInterfaces.tt Line: 50
    	string PluginGenName { get; } // ModelInterfaces.tt Line: 50
    	string ConnGuid { get; } // ModelInterfaces.tt Line: 50
    	string ConnName { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IPluginGroupGeneratorsDefaultSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Guid of group generators
    	///////////////////////////////////////////////////
    	string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 50
    	string Settings { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IGroupListAppSolutions // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// List NET solutions
    	///////////////////////////////////////////////////
    	IReadOnlyList<IAppSolution> ListAppSolutions { get; } // ModelInterfaces.tt Line: 43
    	IAppSolution this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGroupGeneratorsDefaultSettings> ListGroupGeneratorsDefultSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IPluginGroupGeneratorsSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string AppGroupGeneratorsGuid { get; } // ModelInterfaces.tt Line: 50
    	string Settings { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IAppSolution // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// List NET projects
    	/// App solution relative path to configuration file path
    	///////////////////////////////////////////////////
    	string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 43
    	IReadOnlyList<IPluginGroupGeneratorsSettings> ListGroupGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IAppProject // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// App project relative path to .net solution file path
    	///////////////////////////////////////////////////
    	string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	string Namespace { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IPluginGeneratorNodeSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Name of solution-project-generator node
    	///////////////////////////////////////////////////
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingWeight { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// string node_settings_vm_guid = 6;
    	///////////////////////////////////////////////////
    	string Settings { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IPluginGeneratorSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 50
    	string Settings { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    }
    
    ///////////////////////////////////////////////////
    /// Application project generator
    ///////////////////////////////////////////////////
    
    public partial interface IAppProjectGenerator // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 50
    	string DescriptionPlugin { get; } // ModelInterfaces.tt Line: 50
    	string PluginGeneratorGuid { get; } // ModelInterfaces.tt Line: 50
    	string DescriptionGenerator { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Relative folder path to project file
    	///////////////////////////////////////////////////
    	string RelativePathToGenFolder { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Generator output file name
    	///////////////////////////////////////////////////
    	string GenFileName { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	string GeneratorSettings { get; } // ModelInterfaces.tt Line: 50
    	IPluginGeneratorSettings GeneratorSettingsVm { get; } // ModelInterfaces.tt Line: 54
    	string ConnStr { get; } // ModelInterfaces.tt Line: 50
    	string PluginGroupSettingsGuid { get; } // ModelInterfaces.tt Line: 50
    	string ConnStrToPrevStable { get; } // ModelInterfaces.tt Line: 50
    	bool IsGenerateSqlSqriptToUpdatePrevStable { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Generator output file name
    	///////////////////////////////////////////////////
    	string GenScriptFileName { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings // ModelInterfaces.tt Line: 29
    {
    	
    	///////////////////////////////////////////////////
    	/// Guid of solution-project-generator node
    	///////////////////////////////////////////////////
    	string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 50
    	string Settings { get; } // ModelInterfaces.tt Line: 50
    }
    
    ///////////////////////////////////////////////////
    /// General DB settings
    ///////////////////////////////////////////////////
    
    public partial interface IDbSettings // ModelInterfaces.tt Line: 29
    {
    	string DbSchema { get; } // ModelInterfaces.tt Line: 50
    	DbIdGeneratorMethod IdGenerator { get; } // ModelInterfaces.tt Line: 50
    	EnumPrimaryKeyType PKeyType { get; } // ModelInterfaces.tt Line: 50
    	string PKeyName { get; } // ModelInterfaces.tt Line: 50
    	string PKeyFieldGuid { get; } // ModelInterfaces.tt Line: 50
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    
    public partial interface IConfigModel : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	int Version { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	uint CompositeNameMaxLength { get; } // ModelInterfaces.tt Line: 50
    	bool IsUseCompositeNames { get; } // ModelInterfaces.tt Line: 50
    	bool IsUseGroupPrefix { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// GENERAL DB SETTINGS
    	///////////////////////////////////////////////////
    	IDbSettings DbSettings { get; } // ModelInterfaces.tt Line: 54
    	IGroupListCommon GroupCommon { get; } // ModelInterfaces.tt Line: 54
    	IGroupListConstants GroupConstants { get; } // ModelInterfaces.tt Line: 54
    	IGroupListEnumerations GroupEnumerations { get; } // ModelInterfaces.tt Line: 54
    	IGroupListCatalogs GroupCatalogs { get; } // ModelInterfaces.tt Line: 54
    	IGroupDocuments GroupDocuments { get; } // ModelInterfaces.tt Line: 54
    	IGroupListJournals GroupJournals { get; } // ModelInterfaces.tt Line: 54
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IDataType // ModelInterfaces.tt Line: 29
    {
    	EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 50
    	uint Length { get; } // ModelInterfaces.tt Line: 50
    	uint Accuracy { get; } // ModelInterfaces.tt Line: 50
    	string ObjectGuid { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<string> ListObjectGuids { get; } // ModelInterfaces.tt Line: 41
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 50
    	bool IsPositive { get; } // ModelInterfaces.tt Line: 50
    	bool IsNullable { get; } // ModelInterfaces.tt Line: 50
    	bool IsPKey { get; } // ModelInterfaces.tt Line: 50
    	bool IsRefParent { get; } // ModelInterfaces.tt Line: 50
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListCommon : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IGroupListRoles GroupRoles { get; } // ModelInterfaces.tt Line: 54
    	IGroupListMainViewForms GroupViewForms { get; } // ModelInterfaces.tt Line: 54
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    
    public partial interface IRole : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListRoles : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IRole> ListRoles { get; } // ModelInterfaces.tt Line: 43
    	IRole this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    
    public partial interface IMainViewForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IGroupListMainViewForms GroupListViewForms { get; } // ModelInterfaces.tt Line: 54
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    
    public partial interface IGroupListMainViewForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IMainViewForm> ListMainViewForms { get; } // ModelInterfaces.tt Line: 43
    	IMainViewForm this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListPropertiesTabs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPropertiesTab> ListPropertiesTabs { get; } // ModelInterfaces.tt Line: 43
    	IPropertiesTab this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IPropertiesTab : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 54
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 54
    	
    	///////////////////////////////////////////////////
    	/// Create Index for foreign key navigation property
    	///////////////////////////////////////////////////
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListProperties : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IProperty> ListProperties { get; } // ModelInterfaces.tt Line: 43
    	IProperty this[int index] { get; }
    	int Count();
    	
    	///////////////////////////////////////////////////
    	/// Last generated Protobuf field position
    	///////////////////////////////////////////////////
    	uint LastGenPosition { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IProperty : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 54
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Protobuf field position
    	/// Reserved positions: 1 - primary key
    	///////////////////////////////////////////////////
    	uint Position { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListConstants : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IConstant> ListConstants { get; } // ModelInterfaces.tt Line: 43
    	IConstant this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 54
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListEnumerations : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IEnumeration> ListEnumerations { get; } // ModelInterfaces.tt Line: 43
    	IEnumeration this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IEnumeration : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Enumeration element type
    	///////////////////////////////////////////////////
    	EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// Length of string if 'STRING' is selected as enumeration element type
    	///////////////////////////////////////////////////
    	int DataTypeLength { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IEnumerationPair> ListEnumerationPairs { get; } // ModelInterfaces.tt Line: 43
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IEnumerationPair : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	string Value { get; } // ModelInterfaces.tt Line: 50
    	bool IsDefault { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface ICatalogItemsGroup : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 54
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 54
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface ICatalogSettings // ModelInterfaces.tt Line: 29
    {
    	int MaxCatalogItemNameLength { get; } // ModelInterfaces.tt Line: 50
    	int MaxCatalogItemDescriptionLength { get; } // ModelInterfaces.tt Line: 50
    	int MaxCatalogItemTreeLevels { get; } // ModelInterfaces.tt Line: 50
    	bool SeparatePropertiesForGroups { get; } // ModelInterfaces.tt Line: 50
    }
    
    public partial interface ICatalog : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	ICatalogSettings CatalogSettings { get; } // ModelInterfaces.tt Line: 54
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	ICatalogItemsGroup GroupItems { get; } // ModelInterfaces.tt Line: 54
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 54
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 54
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 54
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 54
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 43
    	ICatalog this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 50
    	IGroupListProperties GroupSharedProperties { get; } // ModelInterfaces.tt Line: 54
    	IGroupListDocuments GroupListDocuments { get; } // ModelInterfaces.tt Line: 54
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IDocument : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 54
    	IGroupListPropertiesTabs GroupPropertiesTabs { get; } // ModelInterfaces.tt Line: 54
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 54
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 54
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 43
    	IDocument this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListJournals : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IJournal> ListJournals { get; } // ModelInterfaces.tt Line: 43
    	IJournal this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IJournal : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 43
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListForms : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IForm> ListForms { get; } // ModelInterfaces.tt Line: 43
    	IForm this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	/// repeated proto_document list_forms = 7;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IGroupListReports : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_property list_shared_properties = 6;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IReport> ListReports { get; } // ModelInterfaces.tt Line: 43
    	IReport this[int index] { get; }
    	int Count();
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IReport : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	ulong SortingValue { get; } // ModelInterfaces.tt Line: 50
    	string NameUi { get; } // ModelInterfaces.tt Line: 50
    	string Description { get; } // ModelInterfaces.tt Line: 50
    	bool IsNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasNew { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasMarkedForDeletion { get; } // ModelInterfaces.tt Line: 50
    	bool IsHasChanged { get; } // ModelInterfaces.tt Line: 50
    	
    	///////////////////////////////////////////////////
    	/// repeated proto_group_properties list_properties = 6;
    	/// repeated proto_document list_documents = 7;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 43
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 29
    {
    	string GroupName { get; } // ModelInterfaces.tt Line: 50
    	string Name { get; } // ModelInterfaces.tt Line: 50
    	string Guid { get; } // ModelInterfaces.tt Line: 50
    	bool IsIncluded { get; } // ModelInterfaces.tt Line: 50
    }
}