using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common.ViewModels;

namespace vSharpStudio.common 
{
	// Enumeration member type
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumEnumerationType 
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
	public enum EnumAddressDataType 
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
	public enum EnumCatalogCodeUniqueScope 
	{
		[Description("Whole Catalog")]
		code_unique_in_whole_catalog = 0,
		[Description("Catalog Folder")]
		code_uniqueness_in_catalog_folder = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCatalogDetailAccess 
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
	public enum EnumCatalogTreeIcon 
	{
		None = 0,
		Item = 11,
		Folder = 21,
		Custom = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCodeType 
	{
		[Description("Number")]
		Number = 0,
		[Description("Text")]
		Text = 1,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCommerceDataType 
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
	public enum EnumCompanyDataType 
	{
		P_NONE = 0,
		P_COMPANY_NAME = 1,
		P_COMPANY_SUFFIX = 2,
		P_CATCH_PHRASE = 3,
		P_BS = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumConstantAccess 
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
	public enum EnumDataType 
	{
		[Description("Char")]
		[Browsable(false)]
		CHAR = 0,
		[Description("String")]
		STRING = 11,
		// not sure how work in c# with this typ
		[Browsable(false)]
		[Description("String Fixed")]
		STRING_FIXED = 13,
		// https://github.com/Cysharp/Ulid
		[Description("ULID")]
		ULID = 15,
		[Description("Numerical")]
		NUMERICAL = 21,
		[Description("Boolean")]
		BOOL = 31,
		// not supported in PG, not storing ZONE
		[Browsable(false)]
		[Description("DateTimeOffset")]
		DATETIMEOFFSET = 35,
		[Description("TimeSpan with Time only")]
		TIMESPAN_TIME_ONLY = 37,
		// which DB type to use for whole TimeSpan ???
		[Browsable(false)]
		[Description("TimeSpan")]
		TIMESPAN = 39,
		[Description("Time")]
		TIME = 41,
		[Browsable(false)]
		[Description("Time with Time Zone")]
		TIMEZ = 45,
		[Description("Date")]
		DATE = 51,
		[Description("DateTime Local")]
		DATETIMELOCAL = 61,
		[Description("DateTime UTC")]
		DATETIMEUTC = 71,
		[Description("DateTime with Time Zone")]
		DATETIMEZ = 75,
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
		[Browsable(false)]
		[Description("Any Doc or Catalog")]
		ANY = 131,
		[Browsable(false)]
		REF_DETAIL_TO_PARENT_DETAIL = 141,
		[Browsable(false)]
		REF_DETAIL_TO_PARENT_CATALOG = 142,
		[Browsable(false)]
		REF_DETAIL_TO_PARENT_CATALOG_FOLDER = 143,
		[Browsable(false)]
		REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER = 144,
		[Browsable(false)]
		REF_TO_SELF_TREE_CATALOG_PARENT = 145,
		[Browsable(false)]
		REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT = 146,
		[Browsable(false)]
		REF_DETAIL_TO_PARENT_DOCUMENT = 147,
		[Browsable(false)]
		REF_TIMELINE = 148,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDatabaseDataType 
	{
		DB_NONE = 0,
		DB_COLUMN = 1,
		DB_TYPE = 2,
		DB_COLLATION = 3,
		DB_ENGINE = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDateDataType 
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
	public enum EnumDateTimeAccuracyType 
	{
		[Description("Max accuracy (may be limited by DB)")]
		MAX_DT_ACC = 0,
		[Description("Year")]
		YEAR_DT_ACC = 10,
		[Description("Month")]
		MOUNTH_DT_ACC = 20,
		[Description("Week")]
		WEEK_DT_ACC = 30,
		[Description("Day")]
		DAY_DT_ACC = 40,
		[Description("Hour")]
		HOUR_DT_ACC = 50,
		[Description("Minute")]
		MINUTE_DT_ACC = 60,
		[Description("Second")]
		SECOND_DT_ACC = 70,
		[Description("Millisecond")]
		MS_DT_ACC = 80,
		[Description("Microsecond")]
		MKS_DT_ACC = 90,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDocNumberUniqueScope 
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
	public enum EnumDocumentAccess 
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
		[Description("Post data")]
		D_VIEW_POST_DATA = 45,
		[Description("Unpost")]
		D_UNPOST = 51,
		[Description("Del")]
		D_MARK_DEL = 61,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumFinanceDataType 
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
	public enum EnumHackerDataType 
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
	public enum EnumHiddenType 
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
	public enum EnumImageDataType 
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
	public enum EnumInternetDataType 
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
	public enum EnumLoremDataType 
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
	public enum EnumMonths 
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
	public enum EnumNameDataType 
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
	public enum EnumOneToOneRefType 
	{
		[Description("Ref in both directions")]
		ONE_TO_ONE_REF_BOTH_DIRECTIONS = 0,
		[Description("From first to second only")]
		ONE_TO_ONE_REF_FROM_FIRST_TO_SECOND_ONLY = 1,
		[Description("From second to first only")]
		ONE_TO_ONE_REF_FROM_SECOND_TO_FIRST_ONLY = 2,
		[Browsable(false)]
		[Description("Same ID for both tables")]
		ONE_TO_ONE_BY_SAME_ID = 5,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPhoneDataType 
	{
		PH_NONE = 0,
		PH_PHONE_NUMBER = 1,
		PH_PHONE_NUMBER_FORMAT = 2,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrimaryKeyType 
	{
		[Description("Int")]
		INT = 0,
		[Description("Long")]
		LONG = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrintAccess 
	{
		[Description("By Parent")]
		PR_BY_PARENT = 0,
		[Description("No print")]
		PR_NO_PRINT = 11,
		[Description("Print")]
		PR_PRINT = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPropertyAccess 
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
	public enum EnumPropertyDataType 
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
	public enum EnumRandomDataType 
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
	public enum EnumRantDataType 
	{
		R_NONE = 0,
		R_REVIEW = 1,
		R_REVIEWS = 2,
	}
	public enum EnumRefType 
	{
		REF_TYPE_NOT_SELECTED = 0,
		REF_TYPE_CONSTANT = 1,
		REF_TYPE_CONSTANT_GROUP = 2,
		REF_TYPE_CATALOG = 3,
		REF_TYPE_CATALOG_DETAIL = 4,
		REF_TYPE_DOCUMENT = 5,
		REF_TYPE_DOCUMENT_DETAIL = 6,
		REF_TYPE_CATALOG_FOLDER = 7,
		REF_TYPE_CATALOG_FOLDER_DETAIL = 8,
		REF_TYPE_MANY_TO_MANY_CATALOGS = 9,
		REF_TYPE_MANY_TO_MANY_DOCUMENTS = 10,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRegisterBalancePeriodicity 
	{
		[Description("Year")]
		REGISTER_PERIOD_YEAR = 0,
		[Description("Quarter")]
		REGISTER_PERIOD_QUARTER = 11,
		[Description("Month")]
		REGISTER_PERIOD_MONTH = 21,
		[Description("Week")]
		REGISTER_PERIOD_WEEK = 31,
		[Description("Day")]
		REGISTER_PERIOD_DAY = 41,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRegisterType 
	{
		///<summary>
		/// Balance data for register dimensions. Balance always contains latest accumulated data. One record per unique combination of DIMENTSIONS.
		/// </summary>
		[Description("Balance")]
		BALANCE = 0,
		///<summary>
		/// Only turnovers data for register dimensions.
		/// </summary>
		[Description("Turnover")]
		TURNOVER = 11,
		///<summary>
		/// Balance and turnovers data for selected register periodicity and dimensions. 
		/// Combination of Balance and Turnover functionality. Balance is calculated and stored for beggining of each period.
		/// </summary>
		[Description("Balance and Turnover")]
		BALANCE_AND_TURNOVER = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRelationConfigType 
	{
		[Description("Catalog")]
		RelConfigTypeCatalogs = 0,
		[Description("Document")]
		[Browsable(false)]
		RelConfigTypeDocuments = 1,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumSortingType 
	{
		[Description("Explicitly by user")]
		EXPLICIT = 0,
		[Description("Ascending order")]
		ASCENDING = 1,
		[Description("Descending order")]
		DESCENDING = 2,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumSystemDataType 
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
	public enum EnumTimeAccuracyType 
	{
		[Description("Max accuracy (may be limited by DB)")]
		MAX_TIME_ACC = 0,
		[Description("Hour")]
		HOUR_TIME_ACC = 50,
		[Description("Minute")]
		MINUTE_TIME_ACC = 60,
		[Description("Second")]
		SECOND_TIME_ACC = 70,
		[Description("Ten Milliseconds")]
		TEN_MS_TIME_ACC = 75,
		[Description("Millisecond")]
		MS_TIME_ACC = 80,
		[Description("Microsecond")]
		MKS_TIME_ACC = 90,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumTimespanBoundaryType 
	{
		[Description("Not Selected")]
		NOT_SELECTED_BNDR_ACC = 0,
		[Description("Millennium")]
		MILLENNIUM_BNDR_ACC = 10,
		[Description("Century")]
		CENTURY_BNDR_ACC = 20,
		[Description("Year")]
		YEAR_BNDR_ACC = 30,
		[Description("Month")]
		MOUNTH_BNDR_ACC = 40,
		[Description("Week")]
		WEEK_BNDR_ACC = 50,
		[Description("Day")]
		DAY_BNDR_ACC = 60,
		[Description("Hour")]
		HOUR_BNDR_ACC = 70,
		[Description("Minute")]
		MINUTE_BNDR_ACC = 80,
		[Description("Second")]
		SECOND_BNDR_ACC = 90,
		[Description("Millisecond")]
		MS_BNDR_ACC = 100,
		[Description("Microsecond")]
		MKS_BNDR_ACC = 110,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumUseType 
	{
		[Description("Inherited")]
		Default = 0,
		[Description("Yes")]
		Yes = 11,
		[Description("No")]
		No = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVehicleDataType 
	{
		V_NONE = 0,
		V_VIN = 1,
		V_MANUFACTURER = 2,
		V_MODEL = 3,
		V_TYPE = 4,
		V_FUEL = 5,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVersionFieldType 
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
	public enum EnumWeekDays 
	{
		[Description("Sunday")]
		WEEK_SUNDAY = 0,
		[Description("Monday")]
		WEEK_MONDAY = 1,
		[Description("Tuesday")]
		WEEK_TUESDAY = 2,
		[Description("Wednesday")]
		WEEK_WEDNESDAY = 3,
		[Description("Thursday")]
		WEEK_THURSDAY = 4,
		[Description("Friday")]
		WEEK_FRIDAY = 5,
		[Description("Saturday")]
		WEEK_SATURDAY = 6,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum FormOrientation 
	{
		[Description("Vertical")]
		Vertical = 0,
		[Description("Horizontal")]
		Horizontal = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum FormType 
	{
		[Description("Not selected")]
		FormTypeNotSelected = 0,
		[Description("List view form for data grid")]
		ListDataGrid = 11,
		[Description("Item edit form")]
		ItemEditForm = 21,
		[Description("Folder edit form")]
		FolderEditForm = 31,
		[Description("List view form for combo box")]
		ListComboBox = 41,
	}
    
    public partial interface IRectOnScreen 
    {
    	double X { get; } 
    	double Y { get; } 
    	double Width { get; } 
    	double Height { get; } 
    }
    
    public partial interface IUserSettings 
    {
    	IReadOnlyList<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } 
    	IRectOnScreen LastAppMainWindowRectOnVirtualScreen { get; } 
    	double LastVirtualScreenWidth { get; } 
    	double LastVirtualScreenHeight { get; } 
    }
    
    public partial interface IUserSettingsOpenedConfig 
    {
    	string Guid { get; } 
    	Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } 
    	string ConfigPath { get; } 
    }
    
    public partial interface IGroupListPlugins 
    {
    	IReadOnlyList<IPlugin> ListPlugins { get; } 
    	IPlugin this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    }
    
    public partial interface IPlugin 
    {
    	string Version { get; } 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IPluginGenerator> ListGenerators { get; } 
    	bool IsNew { get; } 
    }
    
    public partial interface IPluginGenerator 
    {
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    }
    
    public partial interface ISettingsConfig 
    {
    	string Name { get; } 
    	string NameUi { get; } 
    	string Description { get; } 
    	// current migration version, increased by one on each deployment
    	int VersionMigrationCurrent { get; } 
    	// min version supported by current version for migration
    	int VersionMigrationSupportFromMin { get; } 
    }
    
    public partial interface IConfigShortHistory 
    {
    	string Guid { get; } 
    	string Name { get; } 
    	IConfig CurrentConfig { get; } 
    	IConfig PrevStableConfig { get; } 
    }
    
    public partial interface IGroupListBaseConfigLinks 
    {
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IBaseConfigLink> ListBaseConfigLinks { get; } 
    	IBaseConfigLink this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IBaseConfigLink 
    {
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	string RelativeConfigFilePath { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Configuration config
    
    public partial interface IConfig : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int Version { get; } 
    	string Description { get; } 
    	Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } 
    	int ExplicitSortingPosition { get; } 
    	// / <summary>
    	// / True if configuration was changed since last code generation.
    	// / Set by SetIsNeedCurrentUpdate(bool val) function.
    	// / </summary>
    	bool IsNeedCurrentUpdate { get; } 
    	IGroupListBaseConfigLinks GroupConfigLinks { get; } 
    	IModel Model { get; } 
    	IGroupListPlugins GroupPlugins { get; } 
    	IGroupListAppSolutions GroupAppSolutions { get; } 
    }
    
    public partial interface IAppDbSettings 
    {
    	string PluginGuid { get; } 
    	string PluginName { get; } 
    	string Version { get; } 
    	string PluginGenGuid { get; } 
    	string PluginGenName { get; } 
    	string ConnGuid { get; } 
    	string ConnName { get; } 
    }
    
    public partial interface IPluginGeneratorSolutionSettings 
    {
    	string Guid { get; } 
    	// string app_generator_guid = 2;
    	string Settings { get; } 
    }
    
    public partial interface IPluginGeneratorProjectSettings 
    {
    	string Guid { get; } 
    	// string app_generator_guid = 2;
    	string Settings { get; } 
    }
    
    public partial interface IGroupListAppSolutions 
    {
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	int ExplicitSortingPosition { get; } 
    	// List NET solutions
    	IReadOnlyList<IAppSolution> ListAppSolutions { get; } 
    	IAppSolution this[int index] { get; }
    	int Count();
    }
    
    public partial interface IAppSolution 
    {
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	string ShortIdForCacheKey { get; } 
    	string RelativeAppSolutionPath { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IAppProject> ListAppProjects { get; } 
    	IReadOnlyList<IPluginGeneratorSolutionSettings> ListGeneratorsSolutionSettings { get; } 
    }
    
    public partial interface IAppProject 
    {
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	string RelativeAppProjectPath { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } 
    	// 
    	// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	IReadOnlyList<IPluginGeneratorProjectSettings> ListGeneratorsProjectSettings { get; } 
    }
    
    public partial interface IPluginGeneratorNodeSettings 
    {
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } 
    	int ExplicitSortingPosition { get; } 
    	// string node_settings_vm_guid = 6;
    	string Settings { get; } 
    }
    
    public partial interface IPluginGroupModelExtensions 
    {
    	// plugin group Guid
    	string Guid { get; } 
    	// model extensions of plugin group
    	string Settings { get; } 
    }
    
    public partial interface IPluginGeneratorSettings 
    {
    	string Guid { get; } 
    	string Name { get; } 
    	string NameUi { get; } 
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } 
    	string Settings { get; } 
    }
    // Application project generator
    
    public partial interface IAppProjectGenerator 
    {
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	string PluginGuid { get; } 
    	string DescriptionPlugin { get; } 
    	string PluginGeneratorGuid { get; } 
    	string DescriptionGenerator { get; } 
    	// Relative folder path to project file
    	string RelativePathToGenFolder { get; } 
    	// Generator output file name
    	string GenFileName { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	string GeneratorSettings { get; } 
    	IPluginGeneratorSettings GeneratorSettingsVm { get; } 
    	string ConnStr { get; } 
    	string ConnStrToPrevStable { get; } 
    	bool IsGenerateSqlSqriptToUpdatePrevStable { get; } 
    	// Generator output file name
    	string GenScriptFileName { get; } 
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings 
    {
    	// Guid of solution-project-generator node
    	string NodeSettingsVmGuid { get; } 
    	string Settings { get; } 
    }
    // Configuration model
    
    public partial interface IModel : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int Version { get; } 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	uint CompositeNameMaxLength { get; } 
    	bool IsUseNameComposition { get; } 
    	string PKeyName { get; } 
    	EnumPrimaryKeyType PKeyType { get; } 
    	string RecordVersionFieldName { get; } 
    	EnumVersionFieldType RecordVersionFieldType { get; } 
    	uint ComplexPropertyRefDescrLength { get; } 
    	uint LastTypeShortRefId { get; } 
    	string PropertyIdGuid { get; } 
    	string PropertyVersionGuid { get; } 
    	string PropertyCtlgCodeGuid { get; } 
    	string PropertyCtlgNameGuid { get; } 
    	string PropertyCtlgDescriptionGuid { get; } 
    	string PropertyCtlgIsFolderGuid { get; } 
    	string PropertyDocNumberGuid { get; } 
    	string PropertyDocDateGuid { get; } 
    	string PropertyDocShortTypeIdGuid { get; } 
    	string PropertyDocIsPostedGuid { get; } 
    	bool IsGridSortable { get; } 
    	bool IsGridSortableCustom { get; } 
    	bool IsGridFilterable { get; } 
    	IGroupListCommon GroupCommon { get; } 
    	IGroupConstantGroups GroupConstantGroups { get; } 
    	IGroupListEnumerations GroupEnumerations { get; } 
    	IGroupListCatalogs GroupCatalogs { get; } 
    	IGroupDocuments GroupDocuments { get; } 
    	IRelationsGroup GroupRelations { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    	// Plugin group Guid and string to store extensions
    	IReadOnlyList<IPluginGroupModelExtensions> ListPluginGroupsModelExtensions { get; } 
    }
    
    public partial interface IComplexRef 
    {
    	// Complex property guid. Empty for register doc ???
    	string Guid { get; } 
    	string Name { get; } 
    	int ExplicitSortingPosition { get; } 
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } 
    	string ForeignObjectGuid { get; } 
    	// extended property guid for foreign object id
    	string RefComplexObjectIdPropertyGuid { get; } 
    }
    
    public partial interface IDataType 
    {
    	EnumDataType DataTypeEnum { get; } 
    	bool IsUnicode { get; } 
    	uint Length { get; } 
    	bool IsPositive { get; } 
    	uint Accuracy { get; } 
    	EnumTimeAccuracyType AccuracyForTime { get; } 
    	///<summary>
    	/// Guids of selected complex types for data type CATALOGS or DOCUMENTS
    	/// </summary>
    	IReadOnlyList<IComplexRef> ListObjectRefs { get; } 
    	bool IsNullable { get; } 
    	EnumTimespanBoundaryType TimespanAccuracy { get; } 
    	EnumTimespanBoundaryType TimespanMaxValue { get; } 
    	bool IsUseHistory { get; } 
    	bool IsPKey { get; } 
    	bool IsRefParent { get; } 
    }
    // Common parameters section
    
    public partial interface IGroupListCommon : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	IGroupListRoles GroupRoles { get; } 
    	IGroupListMainViewForms GroupViewForms { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRole : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	EnumPrintAccess DefaultConstantPrintAccessSettings { get; } 
    	EnumConstantAccess DefaultConstantEditAccessSettings { get; } 
    	EnumPrintAccess DefaultCatalogPrintAccessSettings { get; } 
    	EnumCatalogDetailAccess DefaultCatalogEditAccessSettings { get; } 
    	EnumPrintAccess DefaultDocumentPrintAccessSettings { get; } 
    	EnumDocumentAccess DefaultDocumentEditAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRoleConstantAccess 
    {
    	string Guid { get; } 
    	EnumConstantAccess EditAccess { get; } 
    	EnumPrintAccess PrintAccess { get; } 
    }
    
    public partial interface IRolePropertyAccess 
    {
    	string Guid { get; } 
    	EnumPropertyAccess EditAccess { get; } 
    	EnumPrintAccess PrintAccess { get; } 
    }
    
    public partial interface IRoleCatalogAccess 
    {
    	string Guid { get; } 
    	EnumCatalogDetailAccess EditAccess { get; } 
    	EnumPrintAccess PrintAccess { get; } 
    }
    
    public partial interface IRoleDetailAccess 
    {
    	string Guid { get; } 
    	EnumCatalogDetailAccess EditAccess { get; } 
    	EnumPrintAccess PrintAccess { get; } 
    }
    
    public partial interface IRoleDocumentAccess 
    {
    	string Guid { get; } 
    	EnumDocumentAccess EditAccess { get; } 
    	EnumPrintAccess PrintAccess { get; } 
    }
    
    public partial interface IGroupListRoles : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IRole> ListRoles { get; } 
    	IRole this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IMainViewForm : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	IGroupListMainViewForms GroupListViewForms { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // main view forms hierarchy node with children
    
    public partial interface IGroupListMainViewForms : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IMainViewForm> ListMainViewForms { get; } 
    	IMainViewForm this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // P R O P E R T Y
    // @exclude
    // ####################################### P R O P E R T Y ##########################################
    
    public partial interface IGroupListProperties : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IProperty> ListProperties { get; } 
    	IProperty this[int index] { get; }
    	int Count();
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } 
    	int ExplicitSortingPosition { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IProperty : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	IDataType DataType { get; } 
    	string DefaultValue { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	string RangeValuesRequirementStr { get; } 
    	string MinLengthRequirement { get; } 
    	string MaxLengthRequirement { get; } 
    	bool IsTryAttach { get; } 
    	int LinesOnScreen { get; } 
    	bool IsStartNewRow { get; } 
    	string TabName { get; } 
    	bool IsStartNewTabControl { get; } 
    	bool IsStopTabControl { get; } 
    	IPropertyDataGenerator DataGenerator { get; } 
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
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	// extended property guid for reference complex object type guid
    	string RefComplexObjectGdPropertyGuid { get; } 
    	// extended property guid for reference complex object short description guid
    	string RefComplexObjectDescrPropertyGuid { get; } 
    	// Position of complex type GUID for CATALOGS, or DOCUMENTS, or ANY
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfGd { get; } 
    	// Position of short description property for complex types
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfDescr { get; } 
    	// Position of property
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // D E T A I L S
    // @exclude
    // ####################################### D E T A I L S ########################################
    
    public partial interface IGroupListDetails : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IDetail> ListDetails { get; } 
    	IDetail this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IDetail : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	// Create Index for foreign key navigation property
    	bool IsIndexFk { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IGroupListProperties GroupProperties { get; } 
    	IGroupListDetails GroupDetails { get; } 
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } 
    	// special RefTreeParent property    
    	IProperty PropertyRefParent { get; } 
    	// Sequential unique number in configuration
    	uint ShortId { get; } 
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } 
    	bool IsTryAttach { get; } 
    	bool IsStartNewRow { get; } 
    	bool IsStartNewTab { get; } 
    	string TabName { get; } 
    	bool IsStartNewTabControl { get; } 
    	bool IsStopTabControl { get; } 
    	string ViewListDatagridGuid { get; } 
    	string ViewListComboBoxGuid { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IGroupListForms GroupForms { get; } 
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // C O N S T A N T
    // @exclude
    // ####################################### C O N S T A N T ##########################################
    
    public partial interface IGroupConstantGroups : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	string PrefixForCompositionNames { get; } 
    	IReadOnlyList<IGroupListConstants> ListConstantGroups { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IGroupListConstants : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IConstant> ListConstants { get; } 
    	IConstant this[int index] { get; }
    	int Count();
    	string ShortIdTypeForCacheKey { get; } 
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	uint ShortId { get; } 
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Constant application wise value
    
    public partial interface IConstant : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	IDataType DataType { get; } 
    	bool IsNullable { get; } 
    	string DefaultValue { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	// Sequential unique number in configuration
    	uint ShortId { get; } 
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } 
    	string RangeValuesRequirementStr { get; } 
    	string MinLengthRequirement { get; } 
    	string MaxLengthRequirement { get; } 
    	EnumTimeAccuracyType AccuracyForTime { get; } 
    	bool IsTryAttach { get; } 
    	int LinesOnScreen { get; } 
    	bool IsStartNewRow { get; } 
    	string TabName { get; } 
    	bool IsStartNewTabControl { get; } 
    	bool IsStopTabControl { get; } 
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } 
    	// extended property guid for reference complex object type guid
    	string RefComplexObjectGdPropertyGuid { get; } 
    	// extended property guid for reference complex object short description guid
    	string RefComplexObjectDescrPropertyGuid { get; } 
    	// Position of complex type GUID for CATALOGS, or DOCUMENTS, or ANY
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfGd { get; } 
    	// Position of short description property for complex types
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfDescr { get; } 
    	// Position of property
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // E N U M E R A T I O N
    // @exclude
    // ####################################### E N U M E R A T I O N ##########################################
    
    public partial interface IGroupListEnumerations : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IEnumeration> ListEnumerations { get; } 
    	IEnumeration this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IEnumeration : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	// Enumeration element type
    	EnumEnumerationType DataTypeEnum { get; } 
    	// Length of string if 'STRING' is selected as enumeration element type
    	int DataTypeLength { get; } 
    	IReadOnlyList<IEnumerationPair> ListEnumerationPairs { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IEnumerationPair : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	string Value { get; } 
    	bool IsDefault { get; } 
    	int NumericValue { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface ICatalogFolder : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	// special RefTreeParent property    
    	IProperty PropertyRefSelf { get; } 
    	EnumUseType UseCodeProperty { get; } 
    	ICatalogCodePropertySettings CodePropertySettings { get; } 
    	string IndexUniqueCodeGuid { get; } 
    	string IndexRefTreeParentCodeGuid { get; } 
    	string IndexNotUniqueCodeGuid { get; } 
    	EnumUseType UseNameProperty { get; } 
    	uint MaxNameLength { get; } 
    	EnumUseType UseDescriptionProperty { get; } 
    	uint MaxDescriptionLength { get; } 
    	string ViewListDatagridGuid { get; } 
    	string ViewListComboBoxGuid { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IGroupListProperties GroupProperties { get; } 
    	IGroupListDetails GroupDetails { get; } 
    	IGroupListForms GroupForms { get; } 
    	IGroupListReports GroupReports { get; } 
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface ICatalogCodePropertySettings 
    {
    	EnumCodeType SequenceType { get; } 
    	bool IsUnicode { get; } 
    	uint MaxSequenceLength { get; } 
    	string Prefix { get; } 
    	EnumCatalogCodeUniqueScope UniqueScope { get; } 
    	string? PropertyCodeName { get; } 
    }
    
    public partial interface ICatalog : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	// special RefParent property    
    	IProperty PropertyRefSelf { get; } 
    	// special RefTreeParent property    
    	IProperty PropertyRefFolder { get; } 
    	// Sequential unique number in configuration
    	uint ShortId { get; } 
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } 
    	bool UseTree { get; } 
    	uint MaxTreeLevels { get; } 
    	bool UseSeparateTreeForFolders { get; } 
    	bool UseItemsAtRoot { get; } 
    	EnumUseType UseCodeProperty { get; } 
    	ICatalogCodePropertySettings CodePropertySettings { get; } 
    	EnumUseType UseNameProperty { get; } 
    	bool IsUnicodeName { get; } 
    	uint MaxNameLength { get; } 
    	EnumUseType UseDescriptionProperty { get; } 
    	bool IsUnicodeDescription { get; } 
    	uint MaxDescriptionLength { get; } 
    	string IndexUniqueCodeGuid { get; } 
    	string IndexRefFolderCodeGuid { get; } 
    	string IndexRefTreeParentCodeGuid { get; } 
    	string IndexNotUniqueCodeGuid { get; } 
    	EnumCatalogTreeIcon ItemIconType { get; } 
    	EnumCatalogTreeIcon GroupIconType { get; } 
    	string ViewListDatagridGuid { get; } 
    	string ViewListComboBoxGuid { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	ICatalogFolder Folder { get; } 
    	IGroupListProperties GroupProperties { get; } 
    	IGroupListDetails GroupDetails { get; } 
    	IGroupListForms GroupForms { get; } 
    	IGroupListReports GroupReports { get; } 
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	string PrefixForCompositionNames { get; } 
    	string ShortIdTypeForCacheKey { get; } 
    	IReadOnlyList<ICatalog> ListCatalogs { get; } 
    	ICatalog this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	bool UseCodeProperty { get; } 
    	string PropertyCodeName { get; } 
    	bool UseNameProperty { get; } 
    	string PropertyNameName { get; } 
    	bool UseDescriptionProperty { get; } 
    	string PropertyDescriptionName { get; } 
    	bool UseCodePropertyInSeparateTree { get; } 
    	bool UseNamePropertyInSeparateTree { get; } 
    	bool UseDescriptionPropertyInSeparateTree { get; } 
    	string PropertyIsFolderName { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IGroupListRegisters : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IRegister> ListRegisters { get; } 
    	IRegister this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	string PrefixForCompositionNames { get; } 
    	string ShortIdTypeForCacheKey { get; } 
    	// Guid for document guid property. Auto generated.
    	string PropertyRegGuidGuid { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRegisterDimension : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	///<summary>
    	/// Guid of Catalog type.
    	/// </summary>
    	string DimensionCatalogGuid { get; } 
    	// Dimension property. Auto generated.
    	IProperty PropertyRefDimensionCatalog { get; } 
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IGroupListRegisterDimensions : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IRegisterDimension> ListDimensions { get; } 
    	int ExplicitSortingPosition { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRegister : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	EnumRegisterType RegisterType { get; } 
    	EnumRegisterBalancePeriodicity RegisterBalancePeriodicity { get; } 
    	EnumWeekDays RegisterBalanceWeeklyStartDay { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	uint ShortId { get; } 
    	bool UseQtyAccumulator { get; } 
    	string PropertyQtyAccumulatorName { get; } 
    	uint PropertyQtyAccumulatorLength { get; } 
    	uint PropertyQtyAccumulatorAccuracy { get; } 
    	string PropertyQtyAccumulatorGuid { get; } 
    	bool UseMoneyAccumulator { get; } 
    	string PropertyMoneyAccumulatorName { get; } 
    	uint PropertyMoneyAccumulatorLength { get; } 
    	uint PropertyMoneyAccumulatorAccuracy { get; } 
    	string PropertyMoneyAccumulatorGuid { get; } 
    	IGroupListRegisterDimensions GroupRegisterDimensions { get; } 
    	IGroupListProperties GroupProperties { get; } 
    	string PropertyDocRefGuidName { get; } 
    	string PropertyDocRefName { get; } 
    	///<summary>
    	/// Guids of selected types of DOCUMENTS which can POST or UNPOST for this register
    	/// </summary>
    	IReadOnlyList<IComplexRef> ListObjectDocRefs { get; } 
    	// Guid for index of document date, dimensions. Auto generated.
    	string IndexDocDateGuid { get; } 
    	// Guid for index of document Id and type. Auto generated.
    	string IndexDocIdTypeGuid { get; } 
    	// special RefTimeline property    
    	IProperty PropertyRefTimeline { get; } 
    	string TableTurnoverPropertyIdGuid { get; } 
    	string TableTurnoverPropertyVersionGuid { get; } 
    	string TableTurnoverGuid { get; } 
    	string TableTurnoverPropertyPostDateGuid { get; } 
    	string TableBalancePropertyIdGuid { get; } 
    	string TableBalancePropertyVersionGuid { get; } 
    	string TableBalanceGuid { get; } 
    	string TableBalancePropertyDateGuid { get; } 
    	// Mapping register properties to document properties
    	IReadOnlyList<IRegisterDocToReg> ListDocMappings { get; } 
    	IGroupListReports GroupReports { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRegisterDocToReg 
    {
    	string Guid { get; } 
    	// Document GUID
    	string DocGuid { get; } 
    	// Mappings
    	IReadOnlyList<IRegisterRegPropToDocProp> ListMappings { get; } 
    	// Use manually written code for POST and UNPOST data for document
    	bool IsManualPostCode { get; } 
    }
    
    public partial interface IRegisterRegPropToDocProp 
    {
    	string Guid { get; } 
    	// Register property GUID
    	string RegPropGuid { get; } 
    	// Document property GUID
    	string DocPropGuid { get; } 
    	// Register GUID
    	string RegGuid { get; } 
    	// Document GUID
    	string DocGuid { get; } 
    }
    
    public partial interface IDocumentEnumeratorSequence : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	EnumCodeType SequenceType { get; } 
    	uint MaxSequenceLength { get; } 
    	string Prefix { get; } 
    	EnumDocNumberUniqueScope ScopeOfUnique { get; } 
    	EnumMonths ScopePeriodStartMonth { get; } 
    	uint ScopePeriodStartMonthDay { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IGroupListEnumeratorSequences : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	IReadOnlyList<IDocumentEnumeratorSequence> ListEnumeratorSequences { get; } 
    	IDocumentEnumeratorSequence this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IGroupDocuments : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	string PrefixForCompositionNames { get; } 
    	IDocumentTimeline DocumentTimeline { get; } 
    	IGroupListDocuments GroupListDocuments { get; } 
    	string DocShortTypeIdPropertyName { get; } 
    	IGroupListRegisters GroupRegisters { get; } 
    	IGroupListJournals GroupJournals { get; } 
    	IGroupListEnumeratorSequences GroupListSequences { get; } 
    	Google.Protobuf.WellKnownTypes.Timestamp MondayBeforeFirstDocDate { get; } 
    	string PropertyDocNumberName { get; } 
    	bool UseDocNumberProperty { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IDocumentTimeline : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	IReadOnlyList<IProperty> ListProperties { get; } 
    	int ExplicitSortingPosition { get; } 
    	EnumTimeAccuracyType TimeLineTimeAccuracy { get; } 
    	string TimeLineDocDateTimePropertyName { get; } 
    	string PropertyTimelineDocDateTimeGuid { get; } 
    	string ShortIdTypeForCacheKey { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } 
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IDocument : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	string SequenceGuid { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IGroupListProperties GroupProperties { get; } 
    	IGroupListDetails GroupDetails { get; } 
    	IGroupListForms GroupForms { get; } 
    	IGroupListReports GroupReports { get; } 
    	// Sequential unique number in configuration
    	uint ShortId { get; } 
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IGroupListDocuments : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	string ShortIdTypeForCacheKey { get; } 
    	IReadOnlyList<IDocument> ListDocuments { get; } 
    	IDocument this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRelationsGroup : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	IRelationsManyToManyGroup GroupListManyToManyRelations { get; } 
    	IRelationsOneToOneGroup GroupListOneToOneRelations { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRelationNode 
    {
    	EnumRelationConfigType RefObjType { get; } 
    	string? GuidObj { get; } 
    }
    
    public partial interface IRelationManyToMany : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	EnumRelationConfigType RefObj1Type { get; } 
    	string? GuidObj1 { get; } 
    	EnumRelationConfigType RefObj2Type { get; } 
    	string? GuidObj2 { get; } 
    	bool IsUseHistory { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	// Sequential unique number in configuration
    	uint ShortId { get; } 
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } 
    	IProperty PropertyRefObj1 { get; } 
    	IProperty PropertyRefObj2 { get; } 
    	string PropertyDataTimeGuid { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRelationsManyToManyGroup : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	string PrefixForCompositionNames { get; } 
    	string ShortIdTypeForCacheKey { get; } 
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IRelationManyToMany> ListRelations { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRelationOneToOne : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	EnumRelationConfigType RefObj1Type { get; } 
    	string? GuidObj1 { get; } 
    	bool IsRelationReferenceNullable { get; } 
    	EnumOneToOneRefType RefType { get; } 
    	EnumRelationConfigType RefObj2Type { get; } 
    	string? GuidObj2 { get; } 
    	bool IsUseHistory { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	// Sequential unique number in configuration
    	uint ShortId { get; } 
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } 
    	IProperty PropertyRefObj1 { get; } 
    	IProperty PropertyRefObj2 { get; } 
    	string PropertyDataTimeGuid { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IRelationsOneToOneGroup : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	string PrefixForCompositionNames { get; } 
    	string ShortIdTypeForCacheKey { get; } 
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IRelationOneToOne> ListRelations { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // J O U R N A L
    // @exclude
    // ####################################### J O U R N A L ##########################################
    
    public partial interface IGroupListJournals : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IJournal> ListJournals { get; } 
    	IJournal this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IJournal : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	EnumUseType IsGridSortable { get; } 
    	EnumUseType IsGridSortableCustom { get; } 
    	EnumUseType IsGridFilterable { get; } 
    	IReadOnlyList<IDocInJournal> ListSelectedDocsWithProperties { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IDocInJournal 
    {
    	// doc guid
    	string Guid { get; } 
    	IReadOnlyList<string> ListPropertyGuids { get; } 
    }
    // F O R M S
    // @exclude
    // ####################################### F O R M S ##########################################
    
    public partial interface IGroupListForms : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IForm> ListForms { get; } 
    	IForm this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Children collection can contain:
    //   - Children of Grid System
    
    public partial interface IForm : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	EnumUseType IsUseCode { get; } 
    	EnumUseType IsUseName { get; } 
    	EnumUseType IsUseDesc { get; } 
    	EnumUseType IsUseFolderCode { get; } 
    	EnumUseType IsUseFolderName { get; } 
    	EnumUseType IsUseFolderDesc { get; } 
    	EnumUseType IsUseDocDate { get; } 
    	bool IsDummy { get; } 
    	FormType EnumFormType { get; } 
    	IFormGridSystem GridSystem { get; } 
    	IReadOnlyList<string> ListGuidViewProperties { get; } 
    	IReadOnlyList<string> ListGuidViewFolderProperties { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Children are collection of Grid System Rows 
    
    public partial interface IFormGridSystem : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IFormGridSystemRow> ListRows { get; } 
    	IReadOnlyList<string> ListGuidProperties { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Children are collection of Grid System Columns 
    
    public partial interface IFormGridSystemRow : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IFormGridSystemColumn> ListColumns { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormGridSystemColumn : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumHiddenType HideType { get; } 
    	uint? WidthXs { get; } 
    	uint? WidthSm { get; } 
    	uint? WidthMd { get; } 
    	uint? WidthLg { get; } 
    	uint? WidthXl { get; } 
    	uint? WidthXx { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IFormAutoLayoutBlock FormBlock { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Children collection can contain:
    //   - Fields
    //   - Data grids
    //   - Grid Systems
    //   - Tab Controls
    //   - Auto Layout Blocks
    
    public partial interface IFormAutoLayoutBlock : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IFormAutoLayoutSubBlock> ListFormAutoLayoutSubBlock { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // https://learn.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0
    
    public partial interface IFormAutoLayoutSubBlock : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IFormTabControl TabControl { get; } 
    	IFormDataGrid DataGridControl { get; } 
    	IFormAutoLayoutBlock AutoLayoutBlockControl { get; } 
    	IFormField FieldControl { get; } 
    	IFormGridSystem GridSystemControl { get; } 
    	IFormTree TreeControl { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IFormField : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormTabControlTab : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<string> ListGuidProperties { get; } 
    	IFormAutoLayoutBlock FormBlock { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // Children are collection of Tab Control Tabs
    
    public partial interface IFormTabControl : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<IFormTabControlTab> ListTabs { get; } 
    	IReadOnlyList<string> ListGuidProperties { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // No Children
    
    public partial interface IFormDataGrid : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<string> ListGuidProperties { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // No Children
    
    public partial interface IFormTree : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	int ExplicitSortingPosition { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	IReadOnlyList<string> ListGuidProperties { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    // R E P O R T S
    // @exclude
    // ####################################### R E P O R T S ##########################################
    
    public partial interface IGroupListReports : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	string Description { get; } 
    	EnumSortingType SortType { get; } 
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IReport> ListReports { get; } 
    	IReport this[int index] { get; }
    	int Count();
    	int ExplicitSortingPosition { get; } 
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IReport : IGuid, IName 
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); 
    	int ExplicitSortingPosition { get; } 
    	string Description { get; } 
    	bool IsNew { get; } 
    	bool IsMarkedForDeletion { get; } 
    	// repeated proto_group_properties list_properties = 6;
    	// repeated proto_document list_documents = 7;
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } 
    }
    
    public partial interface IModelRow 
    {
    	string GroupName { get; } 
    	string Name { get; } 
    	string Guid { get; } 
    	bool IsIncluded { get; } 
    }
    
    public partial interface IPropertyDataGenerator 
    {
    	EnumPropertyDataType DataType { get; } 
    	EnumAddressDataType Address { get; } 
    	EnumCommerceDataType Commerce { get; } 
    	EnumCompanyDataType Company { get; } 
    	EnumDateDataType Date { get; } 
    	EnumDatabaseDataType Database { get; } 
    	EnumFinanceDataType Finance { get; } 
    	EnumHackerDataType Hacker { get; } 
    	EnumImageDataType Image { get; } 
    	EnumInternetDataType Internet { get; } 
    	EnumLoremDataType Lorem { get; } 
    	EnumNameDataType Name { get; } 
    	EnumPhoneDataType Phone { get; } 
    	EnumRantDataType Rant { get; } 
    	EnumSystemDataType System { get; } 
    	EnumVehicleDataType Vehicle { get; } 
    	EnumRandomDataType Random { get; } 
    }
}