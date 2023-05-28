using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common.ViewModels;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 13
{
	// Enumeration member type
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumEnumerationType // ModelInterfaces.tt Line: 17
	{
		[Description("Int")]
		INTEGER_VALUE = 0,
		[Description("Short")]
		SHORT_VALUE = 11,
		[Description("Byte")]
		BYTE_VALUE = 21,
		[Description("String")]
		STRING_VALUE = 31,
	}
	// https://github.com/bchavez/Bogus
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumAddressDataType // ModelInterfaces.tt Line: 17
	{
		A_NONE = 0,
		A_COUNTRY = 1,
		A_COUNTRY_CODE = 2,
		A_COUNTY = 3,
		A_STATE = 4,
		A_STATE_ABBR = 5,
		A_ZIP_CODE = 6,
		A_CITY_PREFIX = 7,
		A_CITY = 8,
		A_CITY_SUFFIX = 9,
		A_STREET_ADDRESS = 10,
		A_STREET_NAME = 11,
		A_STREET_SUFFIX = 12,
		A_BUILDING_NUMBER = 13,
		A_SECONDARY_ADDRESS = 14,
		A_FULL_ADDRESS = 15,
		A_LATITUDE = 16,
		A_LONGITUDE = 17,
		A_DIRECTION = 18,
		A_CARDINAL_DIRECTION = 19,
		A_ORDINAL_DIRECTION = 20,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCatalogCodeUniqueScope // ModelInterfaces.tt Line: 17
	{
		[Description("Unique in whole catalog")]
		code_unique_in_whole_catalog = 0,
		[Description("Unique in each folder")]
		code_uniqueness_by_folder_settings = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCatalogDetailAccess // ModelInterfaces.tt Line: 17
	{
		[Description("By Parent")]
		C_BY_PARENT = 0,
		[Description("Hide")]
		C_HIDE = 11,
		[Description("View")]
		C_VIEW = 21,
		[Description("Edit Items")]
		C_EDIT_ITEMS = 31,
		[Description("Edit Folders")]
		C_EDIT_FOLDERS = 41,
		[Description("Del")]
		C_MARK_DEL = 51,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCatalogTreeIcon // ModelInterfaces.tt Line: 17
	{
		None = 0,
		Item = 11,
		Folder = 21,
		Custom = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCodeType // ModelInterfaces.tt Line: 17
	{
		[Description("Number")]
		Number = 0,
		[Description("Text")]
		Text = 1,
		[Description("Auto Number")]
		AutoNumber = 2,
		[Description("Auto Text")]
		AutoText = 3,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCommerceDataType // ModelInterfaces.tt Line: 17
	{
		C_NONE = 0,
		C_DEPARTMENT = 1,
		C_CATEGORIES = 2,
		C_PRODUCT_NAME = 3,
		C_PRODUCT = 4,
		C_PRODUCT_ADJECTIVE = 5,
		C_PRODUCT_MATERIAL = 6,
		C_COLOR = 7,
		C_PRICE = 8,
		C_EAN8 = 9,
		C_EAN13 = 10,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCompanyDataType // ModelInterfaces.tt Line: 17
	{
		P_NONE = 0,
		P_COMPANY_NAME = 1,
		P_COMPANY_SUFFIX = 2,
		P_CATCH_PHRASE = 3,
		P_BS = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumConstantAccess // ModelInterfaces.tt Line: 17
	{
		[Description("By Parent")]
		CN_BY_PARENT = 0,
		[Description("Hide")]
		CN_HIDE = 11,
		// 
		// with history
		[Description("View")]
		CN_VIEW = 21,
		// 
		// with history
		[Description("Edit")]
		CN_EDIT = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDataType // ModelInterfaces.tt Line: 17
	{
		[Description("Char")]
		CHAR = 0,
		[Description("String")]
		STRING = 11,
		[Description("Numerical")]
		NUMERICAL = 21,
		[Description("Boolean")]
		BOOL = 31,
		[Description("Time")]
		TIME = 41,
		[Description("Date")]
		DATE = 51,
		[Description("DateTime Local")]
		DATETIMELOCAL = 61,
		[Description("DateTime UTC")]
		DATETIMEUTC = 71,
		[Description("Enumeration")]
		ENUMERATION = 81,
		[Description("Catalog")]
		CATALOG = 91,
		[Description("Catalogs")]
		CATALOGS = 101,
		[Description("Document")]
		DOCUMENT = 111,
		[Description("Documents")]
		DOCUMENTS = 121,
		[Description("Any")]
		ANY = 131,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDatabaseDataType // ModelInterfaces.tt Line: 17
	{
		DB_NONE = 0,
		DB_COLUMN = 1,
		DB_TYPE = 2,
		DB_COLLATION = 3,
		DB_ENGINE = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDateDataType // ModelInterfaces.tt Line: 17
	{
		D_NONE = 0,
		D_PAST_OFFSET = 1,
		D_RECENT_OFFSET = 2,
		D_BETWEEN_OFFSET = 3,
		D_SOON_OFFSET = 4,
		D_FUTURE_OFFSET = 5,
		D_PAST = 6,
		D_RECENT = 7,
		D_BETWEEN = 8,
		D_SOON = 9,
		D_FUTURE = 10,
		D_TIMESPAN = 11,
		D_MONTH = 12,
		D_WEEKDAY = 13,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDocumentAccess // ModelInterfaces.tt Line: 17
	{
		[Description("By Parent")]
		D_BY_PARENT = 0,
		[Description("Hide")]
		D_HIDE = 11,
		[Description("View")]
		D_VIEW = 21,
		[Description("Edit")]
		D_EDIT = 31,
		[Description("Post")]
		D_POST = 41,
		[Description("Unpost")]
		D_UNPOST = 51,
		[Description("Del")]
		D_MARK_DEL = 61,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDocumentCodeUniqueScope // ModelInterfaces.tt Line: 17
	{
		[Description("Allways")]
		DOC_UNIQUE_FOREVER = 0,
		[Description("Year")]
		DOC_UNIQUE_YEAR = 11,
		[Description("Quater")]
		DOC_UNIQUE_QUATER = 21,
		[Description("Month")]
		DOC_UNIQUE_MONTH = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumFinanceDataType // ModelInterfaces.tt Line: 17
	{
		F_NONE = 0,
		F_ACCOUNT = 1,
		F_ACCOUNT_NAME = 2,
		F_TRANSACTION_TYPE = 3,
		F_CURRENCY = 4,
		F_CREDIT_CARD_NUMBER = 5,
		F_CREDIT_CARD_CVV = 6,
		F_BITCOIN_ADDRESS = 7,
		F_ETHEREUM_ADDRESS = 8,
		F_ROUTING_NUMBER = 9,
		F_BIC = 10,
		F_IBAN = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumHackerDataType // ModelInterfaces.tt Line: 17
	{
		H_NONE = 0,
		H_ABBREVIATION = 1,
		H_ADJECTIVE = 2,
		H_NOUN = 3,
		H_VERB = 4,
		H_INGVERB = 5,
		H_PHRASE = 6,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumHiddenType // ModelInterfaces.tt Line: 17
	{
		[Description("Never Hide")]
		NeverHide = 0,
		[Description("Hide on Extra small screen")]
		Xs = 11,
		[Description("Hide on Small screen and smaller")]
		SmAndDown = 21,
		[Description("Hide on Medium screen and smaller")]
		MdAndDown = 31,
		[Description("Hide on Large screen and smaller")]
		LgAndDown = 41,
		[Description("Hide on Extra Large screen and smaller")]
		XlAndDown = 51,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumImageDataType // ModelInterfaces.tt Line: 17
	{
		I_NONE = 0,
		I_DATAURI = 1,
		I_PICSUM = 2,
		I_PLACEHOLDER = 3,
		I_LOREMFLICKR = 4,
		I_LOREMPIXEL_ABSTRACT = 5,
		I_LOREMPIXEL_ANIMALS = 6,
		I_LOREMPIXEL_BUSINESS = 7,
		I_LOREMPIXEL_CATS = 8,
		I_LOREMPIXEL_CITY = 9,
		I_LOREMPIXEL_FOOD = 10,
		I_LOREMPIXEL_NIGHTLIFE = 11,
		I_LOREMPIXEL_FASHION = 12,
		I_LOREMPIXEL_PEOPLE = 13,
		I_LOREMPIXEL_NATURE = 14,
		I_LOREMPIXEL_SPORTS = 15,
		I_LOREMPIXEL_TECHNICS = 16,
		I_LOREMPIXEL_TRANSPORT = 17,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumInternetDataType // ModelInterfaces.tt Line: 17
	{
		N_NONE = 0,
		N_AVATAR = 1,
		N_EMAIL = 2,
		N_EMAIL_EXAMPLE = 3,
		N_USER_NAME = 4,
		N_USER_NAME_UNICODE = 5,
		N_DOMAIN_NAME = 6,
		N_DOMAIN_WORD = 7,
		N_DOMAIN_SUFFIX = 8,
		N_IP = 9,
		N_PORT = 10,
		N_IP_ADDRESS = 11,
		N_IP_END_POINT = 12,
		N_IPV6 = 13,
		N_IPV6_ADDRESS = 14,
		N_IPV6_END_POINT = 15,
		N_USER_AGENT = 16,
		N_MAC = 17,
		N_PASSWORD = 18,
		N_COLOR = 19,
		N_PROTOCOL = 20,
		N_URL = 21,
		N_URL_WITH_PATH = 22,
		N_URL_ROOTED_PATH = 23,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumLoremDataType // ModelInterfaces.tt Line: 17
	{
		L_NONE = 0,
		L_WORD = 1,
		L_WORDS = 2,
		L_LETTER = 3,
		L_SENTENCE = 4,
		L_SENTENCES = 5,
		L_PARAGRAPH = 6,
		L_PARAGRAPHS = 7,
		L_TEXT = 8,
		L_LINES = 9,
		L_SLUG = 10,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumNameDataType // ModelInterfaces.tt Line: 17
	{
		M_NONE = 0,
		M_FIRST_NAME = 1,
		M_LAST_NAME = 2,
		M_FULL_NAME = 3,
		M_PREFIX = 4,
		M_SUFFIX = 5,
		M_FIND_NAME = 6,
		M_JOB_TITLE = 7,
		M_JOB_DESCRIPTOR = 8,
		M_JOB_AREA = 9,
		M_JOB_TYPE = 10,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPhoneDataType // ModelInterfaces.tt Line: 17
	{
		PH_NONE = 0,
		PH_PHONE_NUMBER = 1,
		PH_PHONE_NUMBER_FORMAT = 2,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrimaryKeyType // ModelInterfaces.tt Line: 17
	{
		[Description("Int")]
		INT = 0,
		[Description("Long")]
		LONG = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrintAccess // ModelInterfaces.tt Line: 17
	{
		[Description("By Parent")]
		PR_BY_PARENT = 0,
		[Description("No print")]
		PR_NO_PRINT = 11,
		[Description("Print")]
		PR_PRINT = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPropertyAccess // ModelInterfaces.tt Line: 17
	{
		[Description("By Parent")]
		P_BY_PARENT = 0,
		[Description("Hide")]
		P_HIDE = 11,
		// 
		// with history
		[Description("View")]
		P_VIEW = 21,
		// 
		// with history
		[Description("Edit")]
		P_EDIT = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPropertyDataType // ModelInterfaces.tt Line: 17
	{
		PT_NONE = 0,
		PT_ADDRESS = 1,
		PT_COMMERCE = 2,
		PT_COMPANY = 3,
		PT_DATE = 4,
		PT_DATABASE = 5,
		PT_FINANCE = 6,
		PT_HACKER = 7,
		PT_IMAGE = 8,
		PT_INTERNET = 9,
		PT_LOREM = 10,
		PT_NAME = 11,
		PT_PHONE = 12,
		PT_RANT = 13,
		PT_SYSTEM = 14,
		PT_VEHICLE = 15,
		PT_RANDOM = 16,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRandomDataType // ModelInterfaces.tt Line: 17
	{
		RN_NONE = 0,
		RN_NUMBER = 1,
		RN_STRING = 2,
		RN_STRING2 = 3,
		RN_HASH = 4,
		RN_ALPHANUMERIC = 5,
		RN_HEXADECIMAL = 6,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRantDataType // ModelInterfaces.tt Line: 17
	{
		R_NONE = 0,
		R_REVIEW = 1,
		R_REVIEWS = 2,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumSystemDataType // ModelInterfaces.tt Line: 17
	{
		S_NONE = 0,
		S_FILE_NAME = 1,
		S_DIRECTORY_PATH = 2,
		S_FILE_PATH = 3,
		S_COMMON_FILE_NAME = 4,
		S_MIME_TYPE = 5,
		S_COMMON_FILE_TYPE = 6,
		S_COMMON_FILE_EXT = 7,
		S_FILE_TYPE = 8,
		S_FILE_EXT = 9,
		S_SEMVER = 10,
		S_VERSION = 11,
		S_EXCEPTION = 12,
		S_ANDROID_ID = 13,
		S_APPLE_PUSH_TOKEN = 14,
		S_BLACKBERRY_PIN = 15,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumTimeAccuracyType // ModelInterfaces.tt Line: 17
	{
		[Description("Second")]
		SECOND = 0,
		[Description("Minute")]
		MINUTE = 1,
		[Description("Hour")]
		HOUR = 2,
		[Description("Millisecond")]
		MS = 3,
		[Description("100 nanoseconds")]
		MAX = 5,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumUseType // ModelInterfaces.tt Line: 17
	{
		Default = 0,
		Yes = 11,
		No = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVehicleDataType // ModelInterfaces.tt Line: 17
	{
		V_NONE = 0,
		V_VIN = 1,
		V_MANUFACTURER = 2,
		V_MODEL = 3,
		V_TYPE = 4,
		V_FUEL = 5,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVersionFieldType // ModelInterfaces.tt Line: 17
	{
		[Description("Byte")]
		VER_BYTE = 0,
		[Description("Short")]
		VER_SHORT = 11,
		[Description("Int")]
		VER_INT = 21,
		[Description("Long")]
		VER_LONG = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum FormOrientation // ModelInterfaces.tt Line: 17
	{
		Vertical = 0,
		Horizontal = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum FormType // ModelInterfaces.tt Line: 17
	{
		[Description("Not selected")]
		FormTypeNotSelected = 0,
		[Description("Wide list view form")]
		ListWide = 11,
		[Description("Item edit form")]
		ItemEditForm = 21,
		[Description("Folder edit form")]
		FolderEditForm = 31,
		[Description("Narrow list view form")]
		ListNarrow = 41,
	}
    
    public partial interface IUserSettings // ModelInterfaces.tt Line: 33
    {
    	IReadOnlyList<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IUserSettingsOpenedConfig // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } // ModelInterfaces.tt Line: 55
    	string ConfigPath { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGroupListPlugins // ModelInterfaces.tt Line: 33
    {
    	IReadOnlyList<IPlugin> ListPlugins { get; } // ModelInterfaces.tt Line: 48
    	IPlugin this[int index] { get; }
    	int Count();
    }
    
    public partial interface IPlugin // ModelInterfaces.tt Line: 33
    {
    	string Version { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGenerator> ListGenerators { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IPluginGenerator // ModelInterfaces.tt Line: 33
    {
    	string Description { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface ISettingsConfig // ModelInterfaces.tt Line: 33
    {
    	string Name { get; } // ModelInterfaces.tt Line: 55
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// current migration version, increased by one on each deployment
    	int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 55
    	// min version supported by current version for migration
    	int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IConfigShortHistory // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	string Name { get; } // ModelInterfaces.tt Line: 55
    	IConfig CurrentConfig { get; } // ModelInterfaces.tt Line: 59
    	IConfig PrevStableConfig { get; } // ModelInterfaces.tt Line: 59
    }
    
    public partial interface IGroupListBaseConfigLinks // ModelInterfaces.tt Line: 33
    {
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IBaseConfigLink> ListBaseConfigLinks { get; } // ModelInterfaces.tt Line: 48
    	IBaseConfigLink this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IBaseConfigLink // ModelInterfaces.tt Line: 33
    {
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Configuration config
    
    public partial interface IConfig : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	int Version { get; } // ModelInterfaces.tt Line: 55
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 55
    	bool IsNeedCurrentUpdate { get; } // ModelInterfaces.tt Line: 55
    	IGroupListBaseConfigLinks GroupConfigLinks { get; } // ModelInterfaces.tt Line: 59
    	IModel Model { get; } // ModelInterfaces.tt Line: 59
    	IGroupListPlugins GroupPlugins { get; } // ModelInterfaces.tt Line: 59
    	IGroupListAppSolutions GroupAppSolutions { get; } // ModelInterfaces.tt Line: 59
    }
    
    public partial interface IAppDbSettings // ModelInterfaces.tt Line: 33
    {
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 55
    	string PluginName { get; } // ModelInterfaces.tt Line: 55
    	string Version { get; } // ModelInterfaces.tt Line: 55
    	string PluginGenGuid { get; } // ModelInterfaces.tt Line: 55
    	string PluginGenName { get; } // ModelInterfaces.tt Line: 55
    	string ConnGuid { get; } // ModelInterfaces.tt Line: 55
    	string ConnName { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IPluginGeneratorSolutionSettings // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	// string app_generator_guid = 2;
    	string Settings { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IPluginGeneratorProjectSettings // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	// string app_generator_guid = 2;
    	string Settings { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGroupListAppSolutions // ModelInterfaces.tt Line: 33
    {
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// List NET solutions
    	IReadOnlyList<IAppSolution> ListAppSolutions { get; } // ModelInterfaces.tt Line: 48
    	IAppSolution this[int index] { get; }
    	int Count();
    }
    
    public partial interface IAppSolution // ModelInterfaces.tt Line: 33
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string ShortIdForCacheKey { get; } // ModelInterfaces.tt Line: 55
    	string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 48
    	// 
    	// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	IReadOnlyList<IPluginGeneratorSolutionSettings> ListGeneratorsSolutionSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IAppProject // ModelInterfaces.tt Line: 33
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 48
    	// 
    	// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	IReadOnlyList<IPluginGeneratorProjectSettings> ListGeneratorsProjectSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IPluginGeneratorNodeSettings // ModelInterfaces.tt Line: 33
    {
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 55
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	// string node_settings_vm_guid = 6;
    	string Settings { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IPluginGeneratorSettings // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	string Name { get; } // ModelInterfaces.tt Line: 55
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } // ModelInterfaces.tt Line: 55
    	string Settings { get; } // ModelInterfaces.tt Line: 55
    }
    // Application project generator
    
    public partial interface IAppProjectGenerator // ModelInterfaces.tt Line: 33
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string PluginGuid { get; } // ModelInterfaces.tt Line: 55
    	string DescriptionPlugin { get; } // ModelInterfaces.tt Line: 55
    	string PluginGeneratorGuid { get; } // ModelInterfaces.tt Line: 55
    	string DescriptionGenerator { get; } // ModelInterfaces.tt Line: 55
    	// Relative folder path to project file
    	string RelativePathToGenFolder { get; } // ModelInterfaces.tt Line: 55
    	// Generator output file name
    	string GenFileName { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	string GeneratorSettings { get; } // ModelInterfaces.tt Line: 55
    	IPluginGeneratorSettings GeneratorSettingsVm { get; } // ModelInterfaces.tt Line: 59
    	string ConnStr { get; } // ModelInterfaces.tt Line: 55
    	string ConnStrToPrevStable { get; } // ModelInterfaces.tt Line: 55
    	bool IsGenerateSqlSqriptToUpdatePrevStable { get; } // ModelInterfaces.tt Line: 55
    	// Generator output file name
    	string GenScriptFileName { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings // ModelInterfaces.tt Line: 33
    {
    	// Guid of solution-project-generator node
    	string NodeSettingsVmGuid { get; } // ModelInterfaces.tt Line: 55
    	string Settings { get; } // ModelInterfaces.tt Line: 55
    }
    // Configuration model
    
    public partial interface IModel : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	int Version { get; } // ModelInterfaces.tt Line: 55
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	uint CompositeNameMaxLength { get; } // ModelInterfaces.tt Line: 55
    	bool IsUseCompositeNames { get; } // ModelInterfaces.tt Line: 55
    	bool IsUseGroupPrefix { get; } // ModelInterfaces.tt Line: 55
    	string PKeyGuid { get; } // ModelInterfaces.tt Line: 55
    	string PKeyName { get; } // ModelInterfaces.tt Line: 55
    	EnumPrimaryKeyType PKeyType { get; } // ModelInterfaces.tt Line: 55
    	string RecordVersionFieldGuid { get; } // ModelInterfaces.tt Line: 55
    	string RecordVersionFieldName { get; } // ModelInterfaces.tt Line: 55
    	EnumVersionFieldType RecordVersionFieldType { get; } // ModelInterfaces.tt Line: 55
    	string PropertyCodeName { get; } // ModelInterfaces.tt Line: 55
    	bool UseCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	string PropertyNameName { get; } // ModelInterfaces.tt Line: 55
    	bool UseNameProperty { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDescriptionName { get; } // ModelInterfaces.tt Line: 55
    	bool UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIsFolderName { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDocDateName { get; } // ModelInterfaces.tt Line: 55
    	bool UseDocDateProperty { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDocCodeName { get; } // ModelInterfaces.tt Line: 55
    	bool UseDocCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	int LastConstantGroupShortId { get; } // ModelInterfaces.tt Line: 55
    	int LastCatalogShortId { get; } // ModelInterfaces.tt Line: 55
    	int LastDocumentShortId { get; } // ModelInterfaces.tt Line: 55
    	int LastDetailShortId { get; } // ModelInterfaces.tt Line: 55
    	bool IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	bool IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	bool IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IGroupListCommon GroupCommon { get; } // ModelInterfaces.tt Line: 59
    	IGroupConstantGroups GroupConstantGroups { get; } // ModelInterfaces.tt Line: 59
    	IGroupListEnumerations GroupEnumerations { get; } // ModelInterfaces.tt Line: 59
    	IGroupListCatalogs GroupCatalogs { get; } // ModelInterfaces.tt Line: 59
    	IGroupDocuments GroupDocuments { get; } // ModelInterfaces.tt Line: 59
    	IGroupListJournals GroupJournals { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IDataType // ModelInterfaces.tt Line: 33
    {
    	EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 55
    	uint Length { get; } // ModelInterfaces.tt Line: 55
    	bool IsPositive { get; } // ModelInterfaces.tt Line: 55
    	uint Accuracy { get; } // ModelInterfaces.tt Line: 55
    	// <summary>
    	// / Guid of complex type. It can be Guid of Enumeration, Catalog, Document. 
    	// / Numerical, string, bool, date and similar are simple types. For simple types this property is empty.
    	// / If Guid of group types is assigned, then any type of such group of types is acceptable as type
    	// / If Guid is empty, but EnumDataType is Any, then any complex type is acceptable as type
    	// / </summary>
    	string ObjectGuid { get; } // ModelInterfaces.tt Line: 55
    	// <summary>
    	// / Guids of selected complex types, that are acceptable as types
    	// / </summary>
    	IReadOnlyList<string> ListObjectGuids { get; } // ModelInterfaces.tt Line: 46
    	bool IsPKey { get; } // ModelInterfaces.tt Line: 55
    	bool IsRefParent { get; } // ModelInterfaces.tt Line: 55
    }
    // Common parameters section
    
    public partial interface IGroupListCommon : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IGroupListRoles GroupRoles { get; } // ModelInterfaces.tt Line: 59
    	IGroupListMainViewForms GroupViewForms { get; } // ModelInterfaces.tt Line: 59
    	IGroupListSequences GroupListSequences { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface ICodeSequence : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	EnumCodeType SequenceType { get; } // ModelInterfaces.tt Line: 55
    	uint MaxSequenceLength { get; } // ModelInterfaces.tt Line: 55
    	string Prefix { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IGroupListSequences : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<ICodeSequence> ListSequences { get; } // ModelInterfaces.tt Line: 48
    	ICodeSequence this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IRole : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess DefaultConstantPrintAccessSettings { get; } // ModelInterfaces.tt Line: 55
    	EnumConstantAccess DefaultConstantEditAccessSettings { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess DefaultCatalogPrintAccessSettings { get; } // ModelInterfaces.tt Line: 55
    	EnumCatalogDetailAccess DefaultCatalogEditAccessSettings { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess DefaultDocumentPrintAccessSettings { get; } // ModelInterfaces.tt Line: 55
    	EnumDocumentAccess DefaultDocumentEditAccessSettings { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IRoleConstantAccess // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	EnumConstantAccess EditAccess { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IRolePropertyAccess // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	EnumPropertyAccess EditAccess { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IRoleCatalogAccess // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	EnumCatalogDetailAccess EditAccess { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IRoleDetailAccess // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	EnumCatalogDetailAccess EditAccess { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IRoleDocumentAccess // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	EnumDocumentAccess EditAccess { get; } // ModelInterfaces.tt Line: 55
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGroupListRoles : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IRole> ListRoles { get; } // ModelInterfaces.tt Line: 48
    	IRole this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IMainViewForm : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IGroupListMainViewForms GroupListViewForms { get; } // ModelInterfaces.tt Line: 59
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // main view forms hierarchy node with children
    
    public partial interface IGroupListMainViewForms : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IMainViewForm> ListMainViewForms { get; } // ModelInterfaces.tt Line: 48
    	IMainViewForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // P R O P E R T Y
    // @exclude
    // ####################################### P R O P E R T Y ##########################################
    
    public partial interface IGroupListProperties : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IProperty> ListProperties { get; } // ModelInterfaces.tt Line: 48
    	IProperty this[int index] { get; }
    	int Count();
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IProperty : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 59
    	bool IsNullable { get; } // ModelInterfaces.tt Line: 55
    	string DefaultValue { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	string RangeValuesRequirementStr { get; } // ModelInterfaces.tt Line: 55
    	string MinLengthRequirement { get; } // ModelInterfaces.tt Line: 55
    	string MaxLengthRequirement { get; } // ModelInterfaces.tt Line: 55
    	EnumTimeAccuracyType AccuracyForTime { get; } // ModelInterfaces.tt Line: 55
    	bool IsTryAttach { get; } // ModelInterfaces.tt Line: 55
    	int LinesOnScreen { get; } // ModelInterfaces.tt Line: 55
    	bool IsStartNewRow { get; } // ModelInterfaces.tt Line: 55
    	string TabName { get; } // ModelInterfaces.tt Line: 55
    	bool IsStartNewTabControl { get; } // ModelInterfaces.tt Line: 55
    	bool IsStopTabControl { get; } // ModelInterfaces.tt Line: 55
    	IPropertyDataGenerator DataGenerator { get; } // ModelInterfaces.tt Line: 59
    	// 
    	// // @attr [PropertyOrderAttribute(28)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("Start Grid")]
    	// // @attr [Description("Start new container of 12 columns grid system")]
    	// bool is_start_12_col_grid_system = 28;
    	// // @attr [PropertyOrderAttribute(29)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("Stop Grid")]
    	// // @attr [Description("Stop current container of 12 columns grid system")]
    	// bool is_stop_12_col_grid_system = 29;
    	// // @attr [PropertyOrderAttribute(30)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("Start Column")]
    	// // @attr [Description("Start new column of 12 columns grid system")]
    	// bool is_start_new_column_12_col_grid_system = 30;
    	// // @attr [PropertyOrderAttribute(32)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("Start Row")]
    	// // @attr [Description("Start new row of 12 columns grid system")]
    	// bool is_start_new_row_12_col_grid_system = 31;
    	// // @attr [PropertyOrderAttribute(31)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("Column Name")]
    	// // @attr [Description("Column Name of 12 columns grid system")]
    	// string column_name_12_col_grid_system = 32;
    	// // @attr [PropertyOrderAttribute(33)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("When Hide")]
    	// // @attr [Description("Condition of hiding base on screen size")]
    	// proto_enum_hidden_type hide_type = 33;
    	// // @attr [PropertyOrderAttribute(34)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("XS")]
    	// // @attr [Description("Extra small. Small to large phone. Range: < 600px")]
    	// google.protobuf.UInt32Value width_xs = 34;
    	// // @attr [PropertyOrderAttribute(35)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("SM")]
    	// // @attr [Description("Small. Small to medium tablet. Range: 600px > < 960px")]
    	// google.protobuf.UInt32Value width_sm = 35;
    	// // @attr [PropertyOrderAttribute(36)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("MD")]
    	// // @attr [Description("Medium. Large tablet to laptop. Range: 960px > < 1280px")]
    	// google.protobuf.UInt32Value width_md = 36;
    	// // @attr [PropertyOrderAttribute(37)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("LG")]
    	// // @attr [Description("Large. Desktop. Range: 1280px > < 1920px")]
    	// google.protobuf.UInt32Value width_lg = 37;
    	// // @attr [PropertyOrderAttribute(38)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("XL")]
    	// // @attr [Description("Extra Large. HD and 4k. Range: 1920px > < 2560px")]
    	// google.protobuf.UInt32Value width_xl = 38;
    	// // @attr [PropertyOrderAttribute(39)]
    	// // @attr [Category("12 Column Grid System")]
    	// // @attr [DisplayName("XX")]
    	// // @attr [Description("Extra Extra Large. 4k+ and ultra-wide. Range: >= 2560px")]
    	// google.protobuf.UInt32Value width_xx = 39;
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	// Protobuf field position
    	// Reserved positions: 1 - primary key
    	uint Position { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // D E T A I L S
    // @exclude
    // ####################################### D E T A I L S ########################################
    
    public partial interface IGroupListDetails : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IDetail> ListDetails { get; } // ModelInterfaces.tt Line: 48
    	IDetail this[int index] { get; }
    	int Count();
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IDetail : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// Create Index for foreign key navigation property
    	bool IsIndexFk { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 59
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 59
    	// Protobuf field position
    	// Reserved positions: 1 - primary key
    	uint Position { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 55
    	int ShortId { get; } // ModelInterfaces.tt Line: 55
    	bool IsTryAttach { get; } // ModelInterfaces.tt Line: 55
    	bool IsStartNewRow { get; } // ModelInterfaces.tt Line: 55
    	bool IsStartNewTab { get; } // ModelInterfaces.tt Line: 55
    	string TabName { get; } // ModelInterfaces.tt Line: 55
    	bool IsStartNewTabControl { get; } // ModelInterfaces.tt Line: 55
    	bool IsStopTabControl { get; } // ModelInterfaces.tt Line: 55
    	string ViewListWideGuid { get; } // ModelInterfaces.tt Line: 55
    	string ViewListNarrowGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyRefParentGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IPropertyDataGenerator // ModelInterfaces.tt Line: 33
    {
    	EnumPropertyDataType DataType { get; } // ModelInterfaces.tt Line: 55
    	EnumAddressDataType Address { get; } // ModelInterfaces.tt Line: 55
    	EnumCommerceDataType Commerce { get; } // ModelInterfaces.tt Line: 55
    	EnumCompanyDataType Company { get; } // ModelInterfaces.tt Line: 55
    	EnumDateDataType Date { get; } // ModelInterfaces.tt Line: 55
    	EnumDatabaseDataType Database { get; } // ModelInterfaces.tt Line: 55
    	EnumFinanceDataType Finance { get; } // ModelInterfaces.tt Line: 55
    	EnumHackerDataType Hacker { get; } // ModelInterfaces.tt Line: 55
    	EnumImageDataType Image { get; } // ModelInterfaces.tt Line: 55
    	EnumInternetDataType Internet { get; } // ModelInterfaces.tt Line: 55
    	EnumLoremDataType Lorem { get; } // ModelInterfaces.tt Line: 55
    	EnumNameDataType Name { get; } // ModelInterfaces.tt Line: 55
    	EnumPhoneDataType Phone { get; } // ModelInterfaces.tt Line: 55
    	EnumRantDataType Rant { get; } // ModelInterfaces.tt Line: 55
    	EnumSystemDataType System { get; } // ModelInterfaces.tt Line: 55
    	EnumVehicleDataType Vehicle { get; } // ModelInterfaces.tt Line: 55
    	EnumRandomDataType Random { get; } // ModelInterfaces.tt Line: 55
    }
    // C O N S T A N T
    // @exclude
    // ####################################### C O N S T A N T ##########################################
    
    public partial interface IGroupConstantGroups : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IGroupListConstants> ListConstantGroups { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IGroupListConstants : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IConstant> ListConstants { get; } // ModelInterfaces.tt Line: 48
    	IConstant this[int index] { get; }
    	int Count();
    	string ShortIdTypeForCacheKey { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	int ShortId { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Constant application wise value
    
    public partial interface IConstant : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IDataType DataType { get; } // ModelInterfaces.tt Line: 59
    	bool IsNullable { get; } // ModelInterfaces.tt Line: 55
    	string DefaultValue { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	int ShortId { get; } // ModelInterfaces.tt Line: 55
    	string RangeValuesRequirementStr { get; } // ModelInterfaces.tt Line: 55
    	string MinLengthRequirement { get; } // ModelInterfaces.tt Line: 55
    	string MaxLengthRequirement { get; } // ModelInterfaces.tt Line: 55
    	EnumTimeAccuracyType AccuracyForTime { get; } // ModelInterfaces.tt Line: 55
    	bool IsTryAttach { get; } // ModelInterfaces.tt Line: 55
    	int LinesOnScreen { get; } // ModelInterfaces.tt Line: 55
    	bool IsStartNewRow { get; } // ModelInterfaces.tt Line: 55
    	string TabName { get; } // ModelInterfaces.tt Line: 55
    	bool IsStartNewTabControl { get; } // ModelInterfaces.tt Line: 55
    	bool IsStopTabControl { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // E N U M E R A T I O N
    // @exclude
    // ####################################### E N U M E R A T I O N ##########################################
    
    public partial interface IGroupListEnumerations : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IEnumeration> ListEnumerations { get; } // ModelInterfaces.tt Line: 48
    	IEnumeration this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IEnumeration : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// Enumeration element type
    	EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 55
    	// Length of string if 'STRING' is selected as enumeration element type
    	int DataTypeLength { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IEnumerationPair> ListEnumerationPairs { get; } // ModelInterfaces.tt Line: 48
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IEnumerationPair : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string Value { get; } // ModelInterfaces.tt Line: 55
    	bool IsDefault { get; } // ModelInterfaces.tt Line: 55
    	int NumericValue { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // C A T A L O G
    // @exclude
    // ####################################### C A T A L O G ##########################################
    
    public partial interface ICatalogFolder : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	ICatalogCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 59
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 55
    	uint MaxNameLength { get; } // ModelInterfaces.tt Line: 55
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 55
    	uint MaxDescriptionLength { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 55
    	string ViewListWideGuid { get; } // ModelInterfaces.tt Line: 55
    	string ViewListNarrowGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIsFolderGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyRefSelfGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 59
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 59
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 59
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface ICatalogCodePropertySettings // ModelInterfaces.tt Line: 33
    {
    	EnumCodeType SequenceType { get; } // ModelInterfaces.tt Line: 55
    	uint MaxSequenceLength { get; } // ModelInterfaces.tt Line: 55
    	string Prefix { get; } // ModelInterfaces.tt Line: 55
    	EnumCatalogCodeUniqueScope UniqueScope { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface ICatalog : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	int ShortId { get; } // ModelInterfaces.tt Line: 55
    	bool UseTree { get; } // ModelInterfaces.tt Line: 55
    	bool UseSeparateTreeForFolders { get; } // ModelInterfaces.tt Line: 55
    	uint MaxTreeLevels { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	ICatalogCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 59
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 55
    	uint MaxNameLength { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 55
    	uint MaxDescriptionLength { get; } // ModelInterfaces.tt Line: 55
    	EnumCatalogTreeIcon ItemIconType { get; } // ModelInterfaces.tt Line: 55
    	EnumCatalogTreeIcon GroupIconType { get; } // ModelInterfaces.tt Line: 55
    	string ViewListWideGuid { get; } // ModelInterfaces.tt Line: 55
    	string ViewListNarrowGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIsFolderGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyRefSelfGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyRefFolderGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	ICatalogFolder Folder { get; } // ModelInterfaces.tt Line: 59
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 59
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 59
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 59
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 55
    	string ShortIdTypeForCacheKey { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<ICatalog> ListCatalogs { get; } // ModelInterfaces.tt Line: 48
    	ICatalog this[int index] { get; }
    	int Count();
    	EnumUseType UseCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseNameProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDescriptionProperty { get; } // ModelInterfaces.tt Line: 55
    	bool UseCodePropertyInSeparateTree { get; } // ModelInterfaces.tt Line: 55
    	bool UseNamePropertyInSeparateTree { get; } // ModelInterfaces.tt Line: 55
    	bool UseDescriptionPropertyInSeparateTree { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IGroupDocuments : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string PrefixForDbTables { get; } // ModelInterfaces.tt Line: 55
    	IGroupListProperties GroupSharedProperties { get; } // ModelInterfaces.tt Line: 59
    	IGroupListDocuments GroupListDocuments { get; } // ModelInterfaces.tt Line: 59
    	EnumUseType UseDocCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDocDateProperty { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IDocumentCodePropertySettings // ModelInterfaces.tt Line: 33
    {
    	EnumCodeType SequenceType { get; } // ModelInterfaces.tt Line: 55
    	uint MaxSequenceLength { get; } // ModelInterfaces.tt Line: 55
    	string Prefix { get; } // ModelInterfaces.tt Line: 55
    	string SequenceGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumDocumentCodeUniqueScope ScopeOfUnique { get; } // ModelInterfaces.tt Line: 55
    	Google.Protobuf.WellKnownTypes.Timestamp ScopePeriodStart { get; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IDocument : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 59
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 59
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 59
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 59
    	int ShortId { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType UseDocCodeProperty { get; } // ModelInterfaces.tt Line: 55
    	IDocumentCodePropertySettings CodePropertySettings { get; } // ModelInterfaces.tt Line: 59
    	EnumUseType UseDocDateProperty { get; } // ModelInterfaces.tt Line: 55
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDocCodeGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyDocDateGuid { get; } // ModelInterfaces.tt Line: 55
    	string PropertyVersionGuid { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	string ShortIdTypeForCacheKey { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 48
    	IDocument this[int index] { get; }
    	int Count();
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // J O U R N A L
    // @exclude
    // ####################################### J O U R N A L ##########################################
    
    public partial interface IGroupListJournals : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IJournal> ListJournals { get; } // ModelInterfaces.tt Line: 48
    	IJournal this[int index] { get; }
    	int Count();
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IJournal : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// repeated proto_group_properties list_properties = 6;
    	IReadOnlyList<IDocument> ListDocuments { get; } // ModelInterfaces.tt Line: 48
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IDocInJournal> ListSelectedDocsWithProperties { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IDocInJournal // ModelInterfaces.tt Line: 33
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<string> ListPropertyGuids { get; } // ModelInterfaces.tt Line: 46
    }
    // F O R M S
    // @exclude
    // ####################################### F O R M S ##########################################
    
    public partial interface IGroupListForms : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IForm> ListForms { get; } // ModelInterfaces.tt Line: 48
    	IForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Children collection can contain:
    //   - Children of Grid System
    
    public partial interface IForm : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsUseCode { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsUseName { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsUseDesc { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsUseFolderCode { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsUseFolderName { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsUseFolderDesc { get; } // ModelInterfaces.tt Line: 55
    	EnumUseType IsUseDocDate { get; } // ModelInterfaces.tt Line: 55
    	bool IsDummy { get; } // ModelInterfaces.tt Line: 55
    	FormType EnumFormType { get; } // ModelInterfaces.tt Line: 55
    	IFormGridSystem GridSystem { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<string> ListGuidViewProperties { get; } // ModelInterfaces.tt Line: 46
    	IReadOnlyList<string> ListGuidViewFolderProperties { get; } // ModelInterfaces.tt Line: 46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Children are collection of Grid System Rows 
    
    public partial interface IFormGridSystem : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IFormGridSystemRow> ListRows { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Children are collection of Grid System Columns 
    
    public partial interface IFormGridSystemRow : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IFormGridSystemColumn> ListColumns { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormGridSystemColumn : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	EnumHiddenType HideType { get; } // ModelInterfaces.tt Line: 55
    	uint? WidthXs { get; } // ModelInterfaces.tt Line: 55
    	uint? WidthSm { get; } // ModelInterfaces.tt Line: 55
    	uint? WidthMd { get; } // ModelInterfaces.tt Line: 55
    	uint? WidthLg { get; } // ModelInterfaces.tt Line: 55
    	uint? WidthXl { get; } // ModelInterfaces.tt Line: 55
    	uint? WidthXx { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IFormAutoLayoutBlock FormBlock { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Children collection can contain:
    //   - Fields
    //   - Data grids
    //   - Grid Systems
    //   - Tab Controls
    //   - Auto Layout Blocks
    
    public partial interface IFormAutoLayoutBlock : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IFormAutoLayoutSubBlock> ListFormAutoLayoutSubBlock { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // https://learn.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0
    
    public partial interface IFormAutoLayoutSubBlock : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IFormTabControl TabControl { get; } // ModelInterfaces.tt Line: 59
    	IFormDataGrid DataGridControl { get; } // ModelInterfaces.tt Line: 59
    	IFormAutoLayoutBlock AutoLayoutBlockControl { get; } // ModelInterfaces.tt Line: 59
    	IFormField FieldControl { get; } // ModelInterfaces.tt Line: 59
    	IFormGridSystem GridSystemControl { get; } // ModelInterfaces.tt Line: 59
    	IFormTree TreeControl { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IFormField : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormTabControlTab : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 46
    	IFormAutoLayoutBlock FormBlock { get; } // ModelInterfaces.tt Line: 59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // Children are collection of Tab Control Tabs
    
    public partial interface IFormTabControl : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IFormTabControlTab> ListTabs { get; } // ModelInterfaces.tt Line: 48
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // No Children
    
    public partial interface IFormDataGrid : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // No Children
    
    public partial interface IFormTree : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<string> ListGuidProperties { get; } // ModelInterfaces.tt Line: 46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    // R E P O R T S
    // @exclude
    // ####################################### R E P O R T S ##########################################
    
    public partial interface IGroupListReports : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IReport> ListReports { get; } // ModelInterfaces.tt Line: 48
    	IReport this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IReport : IGuid, IName // ModelInterfaces.tt Line: 33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // ModelInterfaces.tt Line: 36
    	string NameUi { get; } // ModelInterfaces.tt Line: 55
    	string Description { get; } // ModelInterfaces.tt Line: 55
    	bool IsNew { get; } // ModelInterfaces.tt Line: 55
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 55
    	// repeated proto_group_properties list_properties = 6;
    	// repeated proto_document list_documents = 7;
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 48
    }
    
    public partial interface IModelRow // ModelInterfaces.tt Line: 33
    {
    	string GroupName { get; } // ModelInterfaces.tt Line: 55
    	string Name { get; } // ModelInterfaces.tt Line: 55
    	string Guid { get; } // ModelInterfaces.tt Line: 55
    	bool IsIncluded { get; } // ModelInterfaces.tt Line: 55
    }
}