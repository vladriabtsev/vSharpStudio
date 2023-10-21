using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common.ViewModels;

namespace vSharpStudio.common //   7, ""  --- File: ModelInterfaces.tt Line: 14
{
	// Enumeration member type
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumEnumerationType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumAddressDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumCatalogCodeUniqueScope //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("Whole Catalog")]
		code_unique_in_whole_catalog = 0,
		[Description("Catalog Folder")]
		code_uniqueness_by_folder_settings = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCatalogDetailAccess //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumCatalogTreeIcon //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		None = 0,
		Item = 11,
		Folder = 21,
		Custom = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCodeType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("Number")]
		Number = 0,
		[Description("Text")]
		Text = 1,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCommerceDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumCompanyDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		P_NONE = 0,
		P_COMPANY_NAME = 1,
		P_COMPANY_SUFFIX = 2,
		P_CATCH_PHRASE = 3,
		P_BS = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumConstantAccess //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumDatabaseDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		DB_NONE = 0,
		DB_COLUMN = 1,
		DB_TYPE = 2,
		DB_COLLATION = 3,
		DB_ENGINE = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDateDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumDocNumberUniqueScope //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("Allways")]
		DOC_UNIQUE_FOREVER = 0,
		[Description("Year")]
		DOC_UNIQUE_YEAR = 11,
		[Description("Quater")]
		DOC_UNIQUE_QUATER = 21,
		[Description("Month")]
		DOC_UNIQUE_MONTH = 31,
		[Description("Week")]
		DOC_UNIQUE_WEEK = 34,
		[Description("Day")]
		DOC_UNIQUE_DAY = 37,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDocumentAccess //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumFinanceDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumHackerDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumHiddenType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumImageDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumInternetDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumLoremDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumMonths //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("Not Selected")]
		MONTH_NOT_SELECTED = 0,
		[Description("January")]
		MONTH_JANUARY = 1,
		[Description("February")]
		MONTH_FEBRUARY = 2,
		[Description("March")]
		MONTH_MARCH = 3,
		[Description("April")]
		MONTH_APRIL = 4,
		[Description("May")]
		MONTH_MAY = 5,
		[Description("June")]
		MONTH_JUNE = 6,
		[Description("July")]
		MONTH_JULY = 7,
		[Description("August")]
		MONTH_AUGUST = 8,
		[Description("September")]
		MONTH_SEPTEMBER = 9,
		[Description("October")]
		MONTH_OCTOBER = 10,
		[Description("November")]
		MONTH_NOVEMBER = 11,
		[Description("December")]
		MONTH_DECEMBER = 12,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumNameDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumPhoneDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		PH_NONE = 0,
		PH_PHONE_NUMBER = 1,
		PH_PHONE_NUMBER_FORMAT = 2,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrimaryKeyType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("Int")]
		INT = 0,
		[Description("Long")]
		LONG = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrintAccess //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("By Parent")]
		PR_BY_PARENT = 0,
		[Description("No print")]
		PR_NO_PRINT = 11,
		[Description("Print")]
		PR_PRINT = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPropertyAccess //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumPropertyDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumRandomDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumRantDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		R_NONE = 0,
		R_REVIEW = 1,
		R_REVIEWS = 2,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRelationType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("One to Many")]
		ONE_TO_MANY = 0,
		[Description("One to One")]
		ONE_TO_ONE = 1,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumSystemDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumTimeAccuracyType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum EnumUseType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("Inherited")]
		Default = 0,
		[Description("Yes")]
		Yes = 11,
		[Description("No")]
		No = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVehicleDataType //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		V_NONE = 0,
		V_VIN = 1,
		V_MANUFACTURER = 2,
		V_MODEL = 3,
		V_TYPE = 4,
		V_FUEL = 5,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVersionFieldType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
	public enum FormOrientation //   7, ""  --- File: ModelInterfaces.tt Line: 18
	{
		[Description("Vertical")]
		Vertical = 0,
		[Description("Horizontal")]
		Horizontal = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum FormType //   7, ""  --- File: ModelInterfaces.tt Line: 18
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
    
    public partial interface IUserSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	IReadOnlyList<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IUserSettingsOpenedConfig //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ConfigPath { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGroupListPlugins //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	IReadOnlyList<IPlugin> ListPlugins { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IPlugin this[int index] { get; }
    	int Count();
    }
    
    public partial interface IPlugin //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Version { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGenerator> ListGenerators { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginGenerator //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface ISettingsConfig //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Name { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// current migration version, increased by one on each deployment
    	int VersionMigrationCurrent { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// min version supported by current version for migration
    	int VersionMigrationSupportFromMin { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IConfigShortHistory //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Name { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IConfig CurrentConfig { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IConfig PrevStableConfig { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    }
    
    public partial interface IGroupListBaseConfigLinks //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IBaseConfigLink> ListBaseConfigLinks { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IBaseConfigLink this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IBaseConfigLink //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string RelativeConfigFilePath { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Configuration config
    
    public partial interface IConfig : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	int Version { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// / <summary>
    	// / True if configuration was changed since last code generation.
    	// / Set by SetIsNeedCurrentUpdate(bool val) function.
    	// / </summary>
    	bool IsNeedCurrentUpdate { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListBaseConfigLinks GroupConfigLinks { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IModel Model { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListPlugins GroupPlugins { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListAppSolutions GroupAppSolutions { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    }
    
    public partial interface IAppDbSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string PluginGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PluginName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Version { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PluginGenGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PluginGenName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ConnGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ConnName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginGeneratorSolutionSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// string app_generator_guid = 2;
    	string Settings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginGeneratorProjectSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// string app_generator_guid = 2;
    	string Settings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGroupListAppSolutions //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// List NET solutions
    	IReadOnlyList<IAppSolution> ListAppSolutions { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IAppSolution this[int index] { get; }
    	int Count();
    }
    
    public partial interface IAppSolution //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ShortIdForCacheKey { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string RelativeAppSolutionPath { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IAppProject> ListAppProjects { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	// 
    	// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	IReadOnlyList<IPluginGeneratorSolutionSettings> ListGeneratorsSolutionSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IAppProject //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string RelativeAppProjectPath { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	// 
    	// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	IReadOnlyList<IPluginGeneratorProjectSettings> ListGeneratorsProjectSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IPluginGeneratorNodeSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// string node_settings_vm_guid = 6;
    	string Settings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginGroupModelExtentions //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	// plugin group Guid
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// model extentions of plugin group
    	string Settings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginGeneratorSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Name { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Settings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    // Application project generator
    
    public partial interface IAppProjectGenerator //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PluginGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string DescriptionPlugin { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PluginGeneratorGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string DescriptionGenerator { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Relative folder path to project file
    	string RelativePathToGenFolder { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Generator output file name
    	string GenFileName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string GeneratorSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IPluginGeneratorSettings GeneratorSettingsVm { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	string ConnStr { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ConnStrToPrevStable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsGenerateSqlSqriptToUpdatePrevStable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Generator output file name
    	string GenScriptFileName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	// Guid of solution-project-generator node
    	string NodeSettingsVmGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Settings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    // Configuration model
    
    public partial interface IModel : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	int Version { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint CompositeNameMaxLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsUseCompositeNames { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsUseGroupPrefix { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PKeyGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PKeyName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrimaryKeyType PKeyType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string RecordVersionFieldGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string RecordVersionFieldName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumVersionFieldType RecordVersionFieldType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyCodeName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseCodeProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyNameName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseNameProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDescriptionName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseDescriptionProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyIsFolderName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDocDateName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseDocDateProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDocNumberName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseDocNumberProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int LastConstantGroupShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int LastCatalogShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int LastDocumentShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int LastDetailShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int LastRegisterShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListCommon GroupCommon { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupConstantGroups GroupConstantGroups { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListEnumerations GroupEnumerations { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListCatalogs GroupCatalogs { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupDocuments GroupDocuments { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListRegisters GroupListRegisters { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListJournals GroupJournals { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	// Plugin group Guid and string to store extentions
    	IReadOnlyList<IPluginGroupModelExtentions> ListPluginGroupsModelExtentions { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IDataType //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	EnumDataType DataTypeEnum { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint Length { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsPositive { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint Accuracy { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// <summary>
    	// / Guid of complex type. It can be Guid of Enumeration, Catalog, Document. 
    	// / Numerical, string, bool, date and similar are simple types. For simple types this property is empty.
    	// / If Guid of group types is assigned, then any type of such group of types is acceptable as type
    	// / If Guid is empty, but EnumDataType is Any, then any complex type is acceptable as type
    	// / </summary>
    	string ObjectGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumTimeAccuracyType AccuracyForTime { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// <summary>
    	// / Guids of selected complex types for data type CATALOGS or DOCUMENTS
    	// / </summary>
    	IReadOnlyList<string> ListObjectGuids { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	bool IsNullable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumRelationType RelationType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsPKey { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsRefParent { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    // Common parameters section
    
    public partial interface IGroupListCommon : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListRoles GroupRoles { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListMainViewForms GroupViewForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListEnumeratorSequences GroupListSequences { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IEnumeratorSequence : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCodeType SequenceType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxSequenceLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Prefix { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListEnumeratorSequences : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IEnumeratorSequence> ListEnumeratorSequences { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IEnumeratorSequence this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IRole : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess DefaultConstantPrintAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumConstantAccess DefaultConstantEditAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess DefaultCatalogPrintAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCatalogDetailAccess DefaultCatalogEditAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess DefaultDocumentPrintAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumDocumentAccess DefaultDocumentEditAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IRoleConstantAccess //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumConstantAccess EditAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess PrintAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IRolePropertyAccess //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPropertyAccess EditAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess PrintAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IRoleCatalogAccess //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCatalogDetailAccess EditAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess PrintAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IRoleDetailAccess //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCatalogDetailAccess EditAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess PrintAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IRoleDocumentAccess //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumDocumentAccess EditAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPrintAccess PrintAccess { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGroupListRoles : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRole> ListRoles { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IRole this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IMainViewForm : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListMainViewForms GroupListViewForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // main view forms hierarchy node with children
    
    public partial interface IGroupListMainViewForms : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IMainViewForm> ListMainViewForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IMainViewForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // P R O P E R T Y
    // @exclude
    // ####################################### P R O P E R T Y ##########################################
    
    public partial interface IGroupListProperties : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IProperty> ListProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IProperty this[int index] { get; }
    	int Count();
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IProperty : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IDataType DataType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	string DefaultValue { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string RangeValuesRequirementStr { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string MinLengthRequirement { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string MaxLengthRequirement { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsTryAttach { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int LinesOnScreen { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStartNewRow { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string TabName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStartNewTabControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStopTabControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IPropertyDataGenerator DataGenerator { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
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
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Protobuf field position
    	// Reserved positions: 1 - primary key
    	uint Position { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // D E T A I L S
    // @exclude
    // ####################################### D E T A I L S ########################################
    
    public partial interface IGroupListDetails : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IDetail> ListDetails { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IDetail this[int index] { get; }
    	int Count();
    	EnumUseType UseCodeProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseNameProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseDescriptionProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IDetail : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Create Index for foreign key navigation property
    	bool IsIndexFk { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListProperties GroupProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListDetails GroupDetails { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	// Protobuf field position
    	// Reserved positions: 1 - primary key
    	uint Position { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseCodeProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseNameProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseDescriptionProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int ShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsTryAttach { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStartNewRow { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStartNewTab { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string TabName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStartNewTabControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStopTabControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ViewListWideGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ViewListNarrowGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyIdGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyNameGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDescriptionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyRefParentGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyVersionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListForms GroupForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IPropertyDataGenerator //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	EnumPropertyDataType DataType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumAddressDataType Address { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCommerceDataType Commerce { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCompanyDataType Company { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumDateDataType Date { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumDatabaseDataType Database { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumFinanceDataType Finance { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumHackerDataType Hacker { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumImageDataType Image { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumInternetDataType Internet { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumLoremDataType Lorem { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumNameDataType Name { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumPhoneDataType Phone { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumRantDataType Rant { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumSystemDataType System { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumVehicleDataType Vehicle { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumRandomDataType Random { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    // C O N S T A N T
    // @exclude
    // ####################################### C O N S T A N T ##########################################
    
    public partial interface IGroupConstantGroups : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PrefixForDbTables { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IGroupListConstants> ListConstantGroups { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListConstants : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IConstant> ListConstants { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IConstant this[int index] { get; }
    	int Count();
    	string ShortIdTypeForCacheKey { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int ShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Constant application wise value
    
    public partial interface IConstant : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IDataType DataType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	bool IsNullable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string DefaultValue { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int ShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string RangeValuesRequirementStr { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string MinLengthRequirement { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string MaxLengthRequirement { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumTimeAccuracyType AccuracyForTime { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsTryAttach { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int LinesOnScreen { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStartNewRow { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string TabName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStartNewTabControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsStopTabControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // E N U M E R A T I O N
    // @exclude
    // ####################################### E N U M E R A T I O N ##########################################
    
    public partial interface IGroupListEnumerations : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IEnumeration> ListEnumerations { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IEnumeration this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IEnumeration : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Enumeration element type
    	EnumEnumerationType DataTypeEnum { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Length of string if 'STRING' is selected as enumeration element type
    	int DataTypeLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IEnumerationPair> ListEnumerationPairs { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IEnumerationPair : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Value { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsDefault { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int NumericValue { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // C A T A L O G
    // @exclude
    // ####################################### C A T A L O G ##########################################
    
    public partial interface ICatalogFolder : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyIdGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseCodeProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	ICatalogCodePropertySettings CodePropertySettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	string PropertyCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyVersionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexUniqueCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexRefTreeParentCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexNotUniqueCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseNameProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxNameLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyNameGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseDescriptionProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxDescriptionLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDescriptionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ViewListWideGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ViewListNarrowGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyIsFolderGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyRefSelfGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListProperties GroupProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListDetails GroupDetails { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListForms GroupForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListReports GroupReports { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface ICatalogCodePropertySettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	EnumCodeType SequenceType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxSequenceLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Prefix { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCatalogCodeUniqueScope UniqueScope { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface ICatalog : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int ShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseTree { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseSeparateTreeForFolders { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxTreeLevels { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseCodeProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	ICatalogCodePropertySettings CodePropertySettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	EnumUseType UseNameProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxNameLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseDescriptionProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxDescriptionLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexUniqueCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexRefFolderCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexRefTreeParentCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexNotUniqueCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCatalogTreeIcon ItemIconType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumCatalogTreeIcon GroupIconType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ViewListWideGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ViewListNarrowGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyIdGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyCodeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyNameGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDescriptionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyIsFolderGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyRefSelfGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyRefFolderGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyVersionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	ICatalogFolder Folder { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListProperties GroupProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListDetails GroupDetails { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListForms GroupForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListReports GroupReports { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PrefixForDbTables { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ShortIdTypeForCacheKey { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<ICatalog> ListCatalogs { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	ICatalog this[int index] { get; }
    	int Count();
    	EnumUseType UseCodeProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseNameProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseDescriptionProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseCodePropertyInSeparateTree { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseNamePropertyInSeparateTree { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseDescriptionPropertyInSeparateTree { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListRegisters : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRegister> ListRegisters { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IRegister this[int index] { get; }
    	int Count();
    	string PrefixForDbTables { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ShortIdTypeForCacheKey { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for document guid property. Auto generated.
    	string PropertyRegGuidGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for document date guid property. Auto generated.
    	string PropertyDocDateGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for document date secuence guid property. Auto generated.
    	string PropertyDocDateSequenceGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyVersionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IRegisterDimension : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// <summary>
    	// / Guid of Catalog type.
    	// / </summary>
    	string CatalogGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for dimension property. Auto generated.
    	string PropertyDimensionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Protobuf field position
    	// Reserved positions: 1-15
    	uint Position { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListDimensions : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRegisterDimension> ListDimensions { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IRegisterDimension this[int index] { get; }
    	int Count();
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IRegister : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	int ShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseQtyAccumulator { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyQtyAccumulatorName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint PropertyQtyAccumulatorLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint PropertyQtyAccumulatorAccuracy { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyQtyAccumulatorGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool UseMoneyAccumulator { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyMoneyAccumulatorName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint PropertyMoneyAccumulatorLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint PropertyMoneyAccumulatorAccuracy { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// 
    	// / <summary>
    	// / Guids of selected CATALOGS types
    	// / </summary>
    	string PropertyMoneyAccumulatorGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListDimensions GroupRegisterDimensions { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListProperties GroupAttachedProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	string PropertyDocRefGuidName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDocRefName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// <summary>
    	// / Guids of selected types of DOCUMENTS which can POST or UNPOST for this register
    	// / </summary>
    	IReadOnlyList<string> ListDocGuids { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	// Guid for document reference property. Auto generated.
    	string PropertyDocRefGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for document guid property. Auto generated.
    	string PropertyDocGuidGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for document date guid property. Auto generated.
    	string PropertyDocDateGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for index of document date, dimensions. Auto generated.
    	string IndexDocDateDimensionsGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for document number guid property. Auto generated.
    	string PropertyDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Guid for index of document Id and type. Auto generated.
    	string IndexDocIdTypeGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyVersionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Mapping register properties to document properties
    	IReadOnlyList<IRegisterDocToReg> ListDocMappings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IRegisterDocToReg //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Document GUID
    	string DocGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Mappings
    	IReadOnlyList<IRegisterRegPropToDocProp> ListMapings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IRegisterRegPropToDocProp //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Register property GUID
    	string RegPropGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// Document property GUID
    	string DocPropGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGroupDocuments : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PrefixForDbTables { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListProperties GroupSharedProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListDocuments GroupListDocuments { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	EnumUseType UseDocCodeProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	Google.Protobuf.WellKnownTypes.Timestamp MondayBeforeFirstDocDate { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseDocDateProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IDocumentNumberPropertySettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	EnumCodeType SequenceType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint MaxSequenceLength { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Prefix { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string SequenceGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumDocNumberUniqueScope ScopeOfUnique { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumMonths ScopePeriodStartMonth { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint ScopePeriodStartMonthDay { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IDocument : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IGroupListProperties GroupProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListDetails GroupDetails { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListForms GroupForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IGroupListReports GroupReports { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	int ShortId { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType UseDocNumberProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IDocumentNumberPropertySettings DocNumberPropertySettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	EnumUseType UseDocDateProperty { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// unique index of special field YEAR and DocNumber
    	// YEAR = DocDate.Year
    	string IndexYearDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// unique index of special field QUATER and DocNumber
    	// QUATER = DocDate.Year * 4 + DocDate.Month / 3
    	string IndexQuaterDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// unique index of special field MONTH and DocNumber
    	// MONTH = DocDate.Year * 12 + DocDate.Month
    	string IndexMonthDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// unique index of special field WEEK and DocNumber
    	// WEEK = DocDate.Year * (365???) + DocDate.Month / 3
    	string IndexWeekDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// unique index of special field Day and DocNumber
    	// DAY = DocDate.Year * 365?? + DocDate.Day
    	string IndexDayDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexUniqueDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string IndexNotUniqueDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyIdGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDocNumberGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyDocDateGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string PropertyVersionGuid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IGroupListDocuments : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string ShortIdTypeForCacheKey { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IDocument> ListDocuments { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IDocument this[int index] { get; }
    	int Count();
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // J O U R N A L
    // @exclude
    // ####################################### J O U R N A L ##########################################
    
    public partial interface IGroupListJournals : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IJournal> ListJournals { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IJournal this[int index] { get; }
    	int Count();
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IJournal : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// repeated proto_group_properties list_properties = 6;
    	IReadOnlyList<IDocument> ListDocuments { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridSortableCustom { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsGridFilterable { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IDocInJournal> ListSelectedDocsWithProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IDocInJournal //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<string> ListPropertyGuids { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    }
    // F O R M S
    // @exclude
    // ####################################### F O R M S ##########################################
    
    public partial interface IGroupListForms : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IForm> ListForms { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Children collection can contain:
    //   - Children of Grid System
    
    public partial interface IForm : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsUseCode { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsUseName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsUseDesc { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsUseFolderCode { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsUseFolderName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsUseFolderDesc { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumUseType IsUseDocDate { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsDummy { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	FormType EnumFormType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IFormGridSystem GridSystem { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<string> ListGuidViewProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	IReadOnlyList<string> ListGuidViewFolderProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Children are collection of Grid System Rows 
    
    public partial interface IFormGridSystem : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IFormGridSystemRow> ListRows { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<string> ListGuidProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Children are collection of Grid System Columns 
    
    public partial interface IFormGridSystemRow : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IFormGridSystemColumn> ListColumns { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormGridSystemColumn : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	EnumHiddenType HideType { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint? WidthXs { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint? WidthSm { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint? WidthMd { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint? WidthLg { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint? WidthXl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	uint? WidthXx { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IFormAutoLayoutBlock FormBlock { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Children collection can contain:
    //   - Fields
    //   - Data grids
    //   - Grid Systems
    //   - Tab Controls
    //   - Auto Layout Blocks
    
    public partial interface IFormAutoLayoutBlock : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IFormAutoLayoutSubBlock> ListFormAutoLayoutSubBlock { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // https://learn.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0
    
    public partial interface IFormAutoLayoutSubBlock : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IFormTabControl TabControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IFormDataGrid DataGridControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IFormAutoLayoutBlock AutoLayoutBlockControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IFormField FieldControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IFormGridSystem GridSystemControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IFormTree TreeControl { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IFormField : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormTabControlTab : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<string> ListGuidProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	IFormAutoLayoutBlock FormBlock { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // Children are collection of Tab Control Tabs
    
    public partial interface IFormTabControl : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<IFormTabControlTab> ListTabs { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReadOnlyList<string> ListGuidProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // No Children
    
    public partial interface IFormDataGrid : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<string> ListGuidProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // No Children
    
    public partial interface IFormTree : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IReadOnlyList<string> ListGuidProperties { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 47
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    // R E P O R T S
    // @exclude
    // ####################################### R E P O R T S ##########################################
    
    public partial interface IGroupListReports : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IReport> ListReports { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    	IReport this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IReport : IGuid, IName //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); //   7, ""  --- File: ModelInterfaces.tt Line: 37
    	string NameUi { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Description { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsNew { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsMarkedForDeletion { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	// repeated proto_group_properties list_properties = 6;
    	// repeated proto_document list_documents = 7;
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 49
    }
    
    public partial interface IModelRow //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string GroupName { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Name { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string Guid { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsIncluded { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
}