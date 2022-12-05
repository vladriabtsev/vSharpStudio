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
    public enum EnumVersionFieldType // ModelInterfaces.tt Line: 15
    {
        VER_BYTE = 0,
        VER_SHORT = 1,
        VER_INT = 2,
        VER_LONG = 3,
    }
    public enum EnumDataType // ModelInterfaces.tt Line: 15
    {
        CHAR = 0,
        STRING = 1,
        NUMERICAL = 2,
        BOOL = 3,
        TIME = 4,
        DATE = 5,
        DATETIMELOCAL = 6,
        DATETIMEUTC = 7,
        ENUMERATION = 10,
        CATALOG = 11,
        CATALOGS = 12,
        DOCUMENT = 13,
        DOCUMENTS = 14,
        ANY = 15,
    }
    public enum EnumTimeAccuracyType // ModelInterfaces.tt Line: 15
    {
        SECOND = 0,
        MINUTE = 1,
        HOUR = 2,
        MS = 3,
        MAX = 5,
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
    public enum EnumCodeType // ModelInterfaces.tt Line: 15
    {
        Number = 0,
        Text = 1,
        AutoNumber = 2,
        AutoText = 3,
    }
    public enum EnumUseType // ModelInterfaces.tt Line: 15
    {
        Default = 0,
        Yes = 1,
        No = 2,
    }
    public enum EnumDocumentCodeUniqueScope // ModelInterfaces.tt Line: 15
    {
        Forever = 0,
        Year = 1,
        Quater = 2,
        Month = 3,
    }
    public enum FormOrientation // ModelInterfaces.tt Line: 15
    {
        Vertical = 0,
        Horizontal = 1,
    }
    public enum FormType // ModelInterfaces.tt Line: 15
    {
        ViewListWide = 0,
        ViewListNarrow = 1,
        ItemEditForm = 2,
        FolderEditForm = 3,
    }
    public enum EnumHiddenType // ModelInterfaces.tt Line: 15
    {
        NeverHide = 0,
        Xs = 1,
        SmAndDown = 2,
        MdAndDown = 3,
        LgAndDown = 4,
        XlAndDown = 5,
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
    }
    
    public partial interface IPluginGenerator // ModelInterfaces.tt Line: 29
    {
    	string Description { get; } // ModelInterfaces.tt Line: 51
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
    
    public partial interface IGroupListAppSolutions : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
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
    
    public partial interface IAppSolution : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string ShortIdForCacheKey { get; } // ModelInterfaces.tt Line: 51
    	string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IPluginGroupGeneratorsSettings> ListGroupGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IAppProject : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IPluginGroupGeneratorsSettings> ListGroupGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
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
    
    public partial interface IAppProjectGenerator : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
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
    	string PluginGeneratorGroupGuid { get; } // ModelInterfaces.tt Line: 51
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
    	string PKeyGuid { get; } // ModelInterfaces.tt Line: 51
    	string PKeyName { get; } // ModelInterfaces.tt Line: 51
    	EnumPrimaryKeyType PKeyType { get; } // ModelInterfaces.tt Line: 51
    	string RecordVersionFieldGuid { get; } // ModelInterfaces.tt Line: 51
    	string RecordVersionFieldName { get; } // ModelInterfaces.tt Line: 51
    	EnumVersionFieldType RecordVersionFieldType { get; } // ModelInterfaces.tt Line: 51
    	string PropertyCodeName { get; } // ModelInterfaces.tt Line: 51
    	bool UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameName { get; } // ModelInterfaces.tt Line: 51
    	bool UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionName { get; } // ModelInterfaces.tt Line: 51
    	bool UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsFolderName { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsOpenName { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDocDateName { get; } // ModelInterfaces.tt Line: 51
    	bool UseDocDateProperty { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDocCodeName { get; } // ModelInterfaces.tt Line: 51
    	bool UseDocCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	int LastConstantGroupShortId { get; } // ModelInterfaces.tt Line: 51
    	int LastCatalogShortId { get; } // ModelInterfaces.tt Line: 51
    	int LastDocumentShortId { get; } // ModelInterfaces.tt Line: 51
    	int LastDetailShortId { get; } // ModelInterfaces.tt Line: 51
    	bool IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	bool IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	bool IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IGroupListCommon GroupCommon { get; } // ModelInterfaces.tt Line: 55
    	IGroupConstantGroups GroupConstantGroups { get; } // ModelInterfaces.tt Line: 55
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
    	bool IsPositive { get; } // ModelInterfaces.tt Line: 51
    	uint Accuracy { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// <summary>
    	/// / Guid of complex type. It can be Guid of Enumeration, Catalog, Document. 
    	/// / Numerical, string, bool, date and similar are simple types. For simple types this property is empty.
    	/// / If Guid of group types is assigned, then any type of such group of types is acceptable as type
    	/// / If Guid is empty, but EnumDataType is Any, then any complex type is acceptable as type
    	/// / </summary>
    	///////////////////////////////////////////////////
    	string ObjectGuid { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// <summary>
    	/// / Guids of selected complex types, that are acceptable as types
    	/// / </summary>
    	///////////////////////////////////////////////////
    	IReadOnlyList<string> ListObjectGuids { get; } // ModelInterfaces.tt Line: 42
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
    
    public partial interface IGroupListDetails : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IDetail> ListDetails { get; } // ModelInterfaces.tt Line: 44
    	IDetail this[int index] { get; }
    	int Count();
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IDetail : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Create Index for foreign key navigation property
    	///////////////////////////////////////////////////
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 55
    	
    	///////////////////////////////////////////////////
    	/// Protobuf field position
    	/// Reserved positions: 1 - primary key
    	///////////////////////////////////////////////////
    	uint Position { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	int ShortId { get; } // ModelInterfaces.tt Line: 51
    	bool IsTryAttach { get; } // ModelInterfaces.tt Line: 51
    	bool IsStartNewRow { get; } // ModelInterfaces.tt Line: 51
    	bool IsStartNewTab { get; } // ModelInterfaces.tt Line: 51
    	string TabName { get; } // ModelInterfaces.tt Line: 51
    	bool IsStartNewTabControl { get; } // ModelInterfaces.tt Line: 51
    	bool IsStopTabControl { get; } // ModelInterfaces.tt Line: 51
    	string ViewListWideGuid { get; } // ModelInterfaces.tt Line: 51
    	string ViewListNarrowGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyRefParentGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<string> ListGuidViewProperties { get; } // ModelInterfaces.tt Line: 42
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
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IProperty : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 55
    	bool IsNullable { get; } // ModelInterfaces.tt Line: 51
    	string DefaultValue { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	string RangeValuesRequirementStr { get; } // ModelInterfaces.tt Line: 51
    	string MinLengthRequirement { get; } // ModelInterfaces.tt Line: 51
    	string MaxLengthRequirement { get; } // ModelInterfaces.tt Line: 51
    	EnumTimeAccuracyType AccuracyForTime { get; } // ModelInterfaces.tt Line: 51
    	bool IsTryAttach { get; } // ModelInterfaces.tt Line: 51
    	int LinesOnScreen { get; } // ModelInterfaces.tt Line: 51
    	bool IsStartNewRow { get; } // ModelInterfaces.tt Line: 51
    	string TabName { get; } // ModelInterfaces.tt Line: 51
    	bool IsStartNewTabControl { get; } // ModelInterfaces.tt Line: 51
    	bool IsStopTabControl { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// Protobuf field position
    	/// Reserved positions: 1 - primary key
    	///////////////////////////////////////////////////
    	uint Position { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupConstantGroups : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IGroupListConstants> ListConstantGroups { get; } // ModelInterfaces.tt Line: 44
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
    	string ShortIdTypeForCacheKey { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	int ShortId { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    /// 
    ///////////////////////////////////////////////////
    
    public partial interface IConstant : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 55
    	bool IsNullable { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	int ShortId { get; } // ModelInterfaces.tt Line: 51
    	bool IsTryAttach { get; } // ModelInterfaces.tt Line: 51
    	int LinesOnScreen { get; } // ModelInterfaces.tt Line: 51
    	bool IsStartNewRow { get; } // ModelInterfaces.tt Line: 51
    	string TabName { get; } // ModelInterfaces.tt Line: 51
    	bool IsStartNewTabControl { get; } // ModelInterfaces.tt Line: 51
    	bool IsStopTabControl { get; } // ModelInterfaces.tt Line: 51
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
    	int NumericValue { get; } // ModelInterfaces.tt Line: 51
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
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	ICatalogCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 55
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxNameLength { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxDescriptionLength { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 51
    	string ViewListWideGuid { get; } // ModelInterfaces.tt Line: 51
    	string ViewListNarrowGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyRefSelfGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface ICatalogCodePropertySettings // ModelInterfaces.tt Line: 29
    {
    	EnumCodeType Type { get; } // ModelInterfaces.tt Line: 51
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
    	int ShortId { get; } // ModelInterfaces.tt Line: 51
    	bool UseTree { get; } // ModelInterfaces.tt Line: 51
    	bool UseSeparateTreeForFolders { get; } // ModelInterfaces.tt Line: 51
    	uint MaxTreeLevels { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	ICatalogCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxNameLength { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	uint MaxDescriptionLength { get; } // ModelInterfaces.tt Line: 51
    	bool UseFolderTypeExplicitly { get; } // ModelInterfaces.tt Line: 51
    	EnumCatalogTreeIcon ItemIconType { get; } // ModelInterfaces.tt Line: 51
    	EnumCatalogTreeIcon GroupIconType { get; } // ModelInterfaces.tt Line: 51
    	string ViewListWideGuid { get; } // ModelInterfaces.tt Line: 51
    	string ViewListNarrowGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsFolderGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsOpenGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyRefSelfGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyRefFolderGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	ICatalogFolder Folder { get; } // ModelInterfaces.tt Line: 55
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 55
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 55
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 51
    	string ShortIdTypeForCacheKey { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 44
    	ICatalog this[int index] { get; }
    	int Count();
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 51
    	bool UseCodePropertyInSeparateTree { get; } // ModelInterfaces.tt Line: 51
    	bool UseNamePropertyInSeparateTree { get; } // ModelInterfaces.tt Line: 51
    	bool UseDescriptionPropertyInSeparateTree { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
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
    	EnumUseType UseDocCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseDocDateProperty { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IDocumentCodePropertySettings // ModelInterfaces.tt Line: 29
    {
    	EnumCodeType Type { get; } // ModelInterfaces.tt Line: 51
    	uint Length { get; } // ModelInterfaces.tt Line: 51
    	string SequenceGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumDocumentCodeUniqueScope UniqueScope { get; } // ModelInterfaces.tt Line: 51
    	string ScopePeriodStart { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IDocument : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 55
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 55
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 55
    	int ShortId { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType UseDocCodeProperty { get; } // ModelInterfaces.tt Line: 51
    	IDocumentCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDocDateProperty { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDocCodeGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDocDateGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string ShortIdTypeForCacheKey { get; } // ModelInterfaces.tt Line: 51
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
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
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
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
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
    
    ///////////////////////////////////////////////////
    /// Children collection can contain:
    ///   - Children of Grid System
    ///////////////////////////////////////////////////
    
    public partial interface IForm : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsUseCode { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsUseName { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsUseDesc { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsUseFolderCode { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsUseFolderName { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsUseFolderDesc { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsUseDocDate { get; } // ModelInterfaces.tt Line: 51
    	bool IsDummy { get; } // ModelInterfaces.tt Line: 51
    	FormType EnumFormType { get; } // ModelInterfaces.tt Line: 51
    	IFormGridSystem GridSystem { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<string> ListGuidViewProperties { get; } // ModelInterfaces.tt Line: 42
    	IReadOnlyList<string> ListGuidViewFolderProperties { get; } // ModelInterfaces.tt Line: 42
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Children are collection of Grid System Rows 
    ///////////////////////////////////////////////////
    
    public partial interface IFormGridSystem : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IFormGridSystemRow> ListRows { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 42
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Children are collection of Grid System Columns 
    ///////////////////////////////////////////////////
    
    public partial interface IFormGridSystemRow : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IFormGridSystemColumn> ListColumns { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Children are collection of Auto Layout Block children
    ///////////////////////////////////////////////////
    
    public partial interface IFormGridSystemColumn : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	EnumHiddenType HideType { get; } // ModelInterfaces.tt Line: 51
    	uint? WidthXs { get; } // ModelInterfaces.tt Line: 51
    	uint? WidthSm { get; } // ModelInterfaces.tt Line: 51
    	uint? WidthMd { get; } // ModelInterfaces.tt Line: 51
    	uint? WidthLg { get; } // ModelInterfaces.tt Line: 51
    	uint? WidthXl { get; } // ModelInterfaces.tt Line: 51
    	uint? WidthXx { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IFormAutoLayoutBlock FormBlock { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Children collection can contain:
    ///   - Fields
    ///   - Data grids
    ///   - Grid Systems
    ///   - Tab Controls
    ///   - Auto Layout Blocks
    ///////////////////////////////////////////////////
    
    public partial interface IFormAutoLayoutBlock : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IFormAutoLayoutSubBlock> ListFormAutoLayoutSubBlock { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// https://learn.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0
    ///////////////////////////////////////////////////
    
    public partial interface IFormAutoLayoutSubBlock : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IFormTabControl TabControl { get; } // ModelInterfaces.tt Line: 55
    	IFormDataGrid DataGridControl { get; } // ModelInterfaces.tt Line: 55
    	IFormAutoLayoutBlock AutoLayoutBlockControl { get; } // ModelInterfaces.tt Line: 55
    	IFormField FieldControl { get; } // ModelInterfaces.tt Line: 55
    	IFormGridSystem GridSystemControl { get; } // ModelInterfaces.tt Line: 55
    	IFormTree TreeControl { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IFormField : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Children are collection of Auto Layout Block children
    ///////////////////////////////////////////////////
    
    public partial interface IFormTabControlTab : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 42
    	IFormAutoLayoutBlock FormBlock { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// Children are collection of Tab Control Tabs
    ///////////////////////////////////////////////////
    
    public partial interface IFormTabControl : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IFormTabControlTab> ListTabs { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 42
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// No Children
    ///////////////////////////////////////////////////
    
    public partial interface IFormDataGrid : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 42
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    ///////////////////////////////////////////////////
    /// No Children
    ///////////////////////////////////////////////////
    
    public partial interface IFormTree : IGuid, IName // ModelInterfaces.tt Line: 29
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 32
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 42
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