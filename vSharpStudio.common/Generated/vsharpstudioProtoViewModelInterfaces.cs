using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common.ViewModels;

namespace vSharpStudio.common // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:13
{
	// Enumeration member type
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumEnumerationType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumAddressDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumCatalogCodeUniqueScope // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Whole Catalog")]
		code_unique_in_whole_catalog = 0,
		[Description("Catalog Folder")]
		code_uniqueness_in_catalog_folder = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCatalogDetailAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumCatalogTreeIcon // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		None = 0,
		Item = 11,
		Folder = 21,
		Custom = 31,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCodeType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Number")]
		Number = 0,
		[Description("Text")]
		Text = 1,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumCommerceDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumCompanyDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		P_NONE = 0,
		P_COMPANY_NAME = 1,
		P_COMPANY_SUFFIX = 2,
		P_CATCH_PHRASE = 3,
		P_BS = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumConstantAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Char")]
		CHAR = 0,
		[Description("String")]
		STRING = 11,
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
		[Description("Any")]
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
	public enum EnumDatabaseDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		DB_NONE = 0,
		DB_COLUMN = 1,
		DB_TYPE = 2,
		DB_COLLATION = 3,
		DB_ENGINE = 4,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumDateDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumDateTimeAccuracyType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumDocNumberUniqueScope // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumDocumentAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumFinanceDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumHackerDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumHiddenType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumImageDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumInternetDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumLoremDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumMonths // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumNameDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumOneToOneRefType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumPhoneDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		PH_NONE = 0,
		PH_PHONE_NUMBER = 1,
		PH_PHONE_NUMBER_FORMAT = 2,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrimaryKeyType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Int")]
		INT = 0,
		[Description("Long")]
		LONG = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPrintAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("By Parent")]
		PR_BY_PARENT = 0,
		[Description("No print")]
		PR_NO_PRINT = 11,
		[Description("Print")]
		PR_PRINT = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumPropertyAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumPropertyDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumRandomDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumRantDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		R_NONE = 0,
		R_REVIEW = 1,
		R_REVIEWS = 2,
	}
	public enum EnumRefType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumRegisterPeriodicity // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Year")]
		REGISTER_PERIOD_YEAR = 0,
		[Description("Quater")]
		REGISTER_PERIOD_QUATER = 11,
		[Description("Month")]
		REGISTER_PERIOD_MONTH = 21,
		[Description("Week")]
		REGISTER_PERIOD_WEEK = 31,
		[Description("Day")]
		REGISTER_PERIOD_DAY = 41,
		[Description("Hour")]
		REGISTER_PERIOD_HOUR = 51,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRegisterType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		///<summary>
		/// Balance data for selected register dimentions. Balance always contains latest accumulated data. One record per period.
		/// </summary>
		[Description("Balance")]
		BALANCE = 0,
		///<summary>
		/// Only turnovers data for selected register periodicity and dimentions. Many records per period.
		/// </summary>
		[Description("Turnover")]
		TURNOVER = 11,
		///<summary>
		/// Balance and turnovers data for selected register periodicity and dimentions. 
		/// Combination of Balance and Turnover functionality. Balance is calculated and stored for beggining of each period.
		/// </summary>
		[Description("Balance and Turnover")]
		BALANCE_AND_TURNOVER = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumRelationConfigType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Catalog")]
		RelConfigTypeCatalogs = 0,
		[Description("Document")]
		[Browsable(false)]
		RelConfigTypeDocuments = 1,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumSystemDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumTimeAccuracyType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumTimespanBoundaryType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum EnumUseType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Inherited")]
		Default = 0,
		[Description("Yes")]
		Yes = 11,
		[Description("No")]
		No = 21,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVehicleDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		V_NONE = 0,
		V_VIN = 1,
		V_MANUFACTURER = 2,
		V_MODEL = 3,
		V_TYPE = 4,
		V_FUEL = 5,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EnumVersionFieldType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
	public enum FormOrientation // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
	{
		[Description("Vertical")]
		Vertical = 0,
		[Description("Horizontal")]
		Horizontal = 11,
	}
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum FormType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:17
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
    
    public partial interface IUserSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	IReadOnlyList<IUserSettingsOpenedConfig> ListOpenConfigHistory { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IUserSettingsOpenedConfig // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ConfigPath { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGroupListPlugins // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	IReadOnlyList<IPlugin> ListPlugins { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IPlugin this[int index] { get; }
    	int Count();
    }
    
    public partial interface IPlugin // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Version { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGenerator> ListGenerators { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginGenerator // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface ISettingsConfig // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Name { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// current migration version, increased by one on each deployment
    	int VersionMigrationCurrent { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// min version supported by current version for migration
    	int VersionMigrationSupportFromMin { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IConfigShortHistory // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Name { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IConfig CurrentConfig { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IConfig PrevStableConfig { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    }
    
    public partial interface IGroupListBaseConfigLinks // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IBaseConfigLink> ListBaseConfigLinks { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IBaseConfigLink this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IBaseConfigLink // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string RelativeConfigFilePath { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Configuration config
    
    public partial interface IConfig : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	int Version { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// / <summary>
    	// / True if configuration was changed since last code generation.
    	// / Set by SetIsNeedCurrentUpdate(bool val) function.
    	// / </summary>
    	bool IsNeedCurrentUpdate { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListBaseConfigLinks GroupConfigLinks { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IModel Model { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListPlugins GroupPlugins { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListAppSolutions GroupAppSolutions { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    }
    
    public partial interface IAppDbSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string PluginGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PluginName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Version { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PluginGenGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PluginGenName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ConnGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ConnName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginGeneratorSolutionSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// string app_generator_guid = 2;
    	string Settings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginGeneratorProjectSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// string app_generator_guid = 2;
    	string Settings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGroupListAppSolutions // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// List NET solutions
    	IReadOnlyList<IAppSolution> ListAppSolutions { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IAppSolution this[int index] { get; }
    	int Count();
    }
    
    public partial interface IAppSolution // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ShortIdForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string RelativeAppSolutionPath { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IAppProject> ListAppProjects { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorSolutionSettings> ListGeneratorsSolutionSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IAppProject // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string RelativeAppProjectPath { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	// 
    	// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	IReadOnlyList<IPluginGeneratorProjectSettings> ListGeneratorsProjectSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IPluginGeneratorNodeSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// string node_settings_vm_guid = 6;
    	string Settings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginGroupModelExtensions // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	// plugin group Guid
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// model extensions of plugin group
    	string Settings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginGeneratorSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Name { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Guid of solution-project-generator node
    	string AppProjectGeneratorGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Settings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    // Application project generator
    
    public partial interface IAppProjectGenerator // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PluginGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string DescriptionPlugin { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PluginGeneratorGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string DescriptionGenerator { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Relative folder path to project file
    	string RelativePathToGenFolder { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Generator output file name
    	string GenFileName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string GeneratorSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IPluginGeneratorSettings GeneratorSettingsVm { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string ConnStr { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ConnStrToPrevStable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsGenerateSqlSqriptToUpdatePrevStable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Generator output file name
    	string GenScriptFileName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginGeneratorNodeDefaultSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	// Guid of solution-project-generator node
    	string NodeSettingsVmGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Settings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    // Configuration model
    
    public partial interface IModel : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	int Version { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint CompositeNameMaxLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsUseNameComposition { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PKeyName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrimaryKeyType PKeyType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string RecordVersionFieldName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumVersionFieldType RecordVersionFieldType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint ComplexPropertyRefDescrLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint LastTypeShortRefId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyIdGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyVersionGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyCtlgCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyCtlgNameGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyCtlgDescriptionGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyCtlgIsFolderGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyDocNumberGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyDocDateGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyDocShortTypeIdGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyDocIsPostedGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListCommon GroupCommon { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupConstantGroups GroupConstantGroups { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListEnumerations GroupEnumerations { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListCatalogs GroupCatalogs { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupDocuments GroupDocuments { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IRelationsGroup GroupRelations { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	// Plugin group Guid and string to store extensions
    	IReadOnlyList<IPluginGroupModelExtensions> ListPluginGroupsModelExtensions { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IComplexRef // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	// Complex property guid. Empty for register doc ???
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Name { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	ulong SortingValue { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ForeignObjectGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// extended property guid for foreign object id
    	string RefComplexObjectIdPropertyGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IDataType // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	EnumDataType DataTypeEnum { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint Length { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsPositive { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint Accuracy { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumTimeAccuracyType AccuracyForTime { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	///<summary>
    	/// Guids of selected complex types for data type CATALOGS or DOCUMENTS
    	/// </summary>
    	IReadOnlyList<IComplexRef> ListObjectRefs { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	bool IsNullable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumTimespanBoundaryType TimespanAccuracy { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumTimespanBoundaryType TimespanMaxValue { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsUseHistory { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsPKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsRefParent { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    // Common parameters section
    
    public partial interface IGroupListCommon : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListRoles GroupRoles { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListMainViewForms GroupViewForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRole : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess DefaultConstantPrintAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumConstantAccess DefaultConstantEditAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess DefaultCatalogPrintAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCatalogDetailAccess DefaultCatalogEditAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess DefaultDocumentPrintAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumDocumentAccess DefaultDocumentEditAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRoleConstantAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumConstantAccess EditAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess PrintAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IRolePropertyAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPropertyAccess EditAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess PrintAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IRoleCatalogAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCatalogDetailAccess EditAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess PrintAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IRoleDetailAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCatalogDetailAccess EditAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess PrintAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IRoleDocumentAccess // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumDocumentAccess EditAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPrintAccess PrintAccess { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGroupListRoles : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRole> ListRoles { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IRole this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IMainViewForm : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListMainViewForms GroupListViewForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // main view forms hierarchy node with children
    
    public partial interface IGroupListMainViewForms : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IMainViewForm> ListMainViewForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IMainViewForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // P R O P E R T Y
    // @exclude
    // ####################################### P R O P E R T Y ##########################################
    
    public partial interface IGroupListProperties : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IProperty> ListProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IProperty this[int index] { get; }
    	int Count();
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IProperty : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IDataType DataType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string DefaultValue { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string RangeValuesRequirementStr { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string MinLengthRequirement { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string MaxLengthRequirement { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsTryAttach { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	int LinesOnScreen { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStartNewRow { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TabName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStartNewTabControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStopTabControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IPropertyDataGenerator DataGenerator { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
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
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// extended property guid for reference complex object type guid
    	string RefComplexObjectGdPropertyGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// extended property guid for reference complex object short description guid
    	string RefComplexObjectDescrPropertyGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Position of complex type GUID for CATALOGS, or DOCUMENTS, or ANY
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfGd { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Position of short description property for complex types
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfDescr { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Position of property
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // D E T A I L S
    // @exclude
    // ####################################### D E T A I L S ########################################
    
    public partial interface IGroupListDetails : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IDetail> ListDetails { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IDetail this[int index] { get; }
    	int Count();
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IDetail : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Create Index for foreign key navigation property
    	bool IsIndexFk { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListProperties GroupProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListDetails GroupDetails { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// special RefTreeParent property    
    	IProperty PropertyRefParent { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	// Sequential unique number in configuration
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsTryAttach { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStartNewRow { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStartNewTab { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TabName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStartNewTabControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStopTabControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ViewListDatagridGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ViewListComboBoxGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListForms GroupForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // C O N S T A N T
    // @exclude
    // ####################################### C O N S T A N T ##########################################
    
    public partial interface IGroupConstantGroups : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PrefixForCompositionNames { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IGroupListConstants> ListConstantGroups { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IGroupListConstants : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IConstant> ListConstants { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IConstant this[int index] { get; }
    	int Count();
    	string ShortIdTypeForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Constant application wise value
    
    public partial interface IConstant : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IDataType DataType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	bool IsNullable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string DefaultValue { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Sequential unique number in configuration
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string RangeValuesRequirementStr { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string MinLengthRequirement { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string MaxLengthRequirement { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumTimeAccuracyType AccuracyForTime { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsTryAttach { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	int LinesOnScreen { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStartNewRow { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TabName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStartNewTabControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsStopTabControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	// extended property guid for reference complex object type guid
    	string RefComplexObjectGdPropertyGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// extended property guid for reference complex object short description guid
    	string RefComplexObjectDescrPropertyGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Position of complex type GUID for CATALOGS, or DOCUMENTS, or ANY
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfGd { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Position of short description property for complex types
    	// unique in object (can be used as Protobuf field position)
    	uint PositionOfDescr { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Position of property
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // E N U M E R A T I O N
    // @exclude
    // ####################################### E N U M E R A T I O N ##########################################
    
    public partial interface IGroupListEnumerations : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IEnumeration> ListEnumerations { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IEnumeration this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IEnumeration : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Enumeration element type
    	EnumEnumerationType DataTypeEnum { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Length of string if 'STRING' is selected as enumeration element type
    	int DataTypeLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IEnumerationPair> ListEnumerationPairs { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IEnumerationPair : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Value { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsDefault { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	int NumericValue { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface ICatalogFolder : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// special RefTreeParent property    
    	IProperty PropertyRefSelf { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	EnumUseType UseCodeProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	ICatalogCodePropertySettings CodePropertySettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string IndexUniqueCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string IndexRefTreeParentCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string IndexNotUniqueCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType UseNameProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint MaxNameLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType UseDescriptionProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint MaxDescriptionLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ViewListDatagridGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ViewListComboBoxGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListProperties GroupProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListDetails GroupDetails { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListForms GroupForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListReports GroupReports { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface ICatalogCodePropertySettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	EnumCodeType SequenceType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint MaxSequenceLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Prefix { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCatalogCodeUniqueScope UniqueScope { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? PropertyCodeName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface ICatalog : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// special RefParent property    
    	IProperty PropertyRefSelf { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	// special RefTreeParent property    
    	IProperty PropertyRefFolder { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	// Sequential unique number in configuration
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseTree { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseSeparateTreeForFolders { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint MaxTreeLevels { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType UseCodeProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	ICatalogCodePropertySettings CodePropertySettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	EnumUseType UseNameProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint MaxNameLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType UseDescriptionProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint MaxDescriptionLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string IndexUniqueCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string IndexRefFolderCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string IndexRefTreeParentCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string IndexNotUniqueCodeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCatalogTreeIcon ItemIconType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCatalogTreeIcon GroupIconType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ViewListDatagridGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ViewListComboBoxGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	ICatalogFolder Folder { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListProperties GroupProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListDetails GroupDetails { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListForms GroupForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListReports GroupReports { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IGroupListCatalogs : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PrefixForCompositionNames { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ShortIdTypeForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<ICatalog> ListCatalogs { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	ICatalog this[int index] { get; }
    	int Count();
    	bool UseCodeProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyCodeName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseNameProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyNameName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseDescriptionProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyDescriptionName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseCodePropertyInSeparateTree { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseNamePropertyInSeparateTree { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseDescriptionPropertyInSeparateTree { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyIsFolderName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IGroupListRegisters : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRegister> ListRegisters { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IRegister this[int index] { get; }
    	int Count();
    	string PrefixForCompositionNames { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ShortIdTypeForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Guid for document guid property. Auto generated.
    	string PropertyRegGuidGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRegisterDimension : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	///<summary>
    	/// Guid of Catalog type.
    	/// </summary>
    	string DimensionCatalogGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Dimension property. Auto generated.
    	IProperty PropertyRefDimensionCatalog { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	// unique in object (can be used as Protobuf field position)
    	uint Position { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IGroupListRegisterDimensions : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRegisterDimension> ListDimensions { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRegister : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRegisterType RegisterType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRegisterPeriodicity RegisterPeriodicity { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseQtyAccumulator { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverPropertyQtyAccumulatorName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint TableTurnoverPropertyQtyAccumulatorLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint TableTurnoverPropertyQtyAccumulatorAccuracy { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverPropertyQtyAccumulatorGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseMoneyAccumulator { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverPropertyMoneyAccumulatorName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint TableTurnoverPropertyMoneyAccumulatorLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint TableTurnoverPropertyMoneyAccumulatorAccuracy { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverPropertyMoneyAccumulatorGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverPropertyIsStartingBalanceGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListRegisterDimensions GroupRegisterDimensions { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListProperties GroupProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string PropertyDocRefGuidName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyDocRefName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	///<summary>
    	/// Guids of selected types of DOCUMENTS which can POST or UNPOST for this register
    	/// </summary>
    	IReadOnlyList<IComplexRef> ListObjectDocRefs { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	// Guid for index of document date, dimensions. Auto generated.
    	string IndexDocDateGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Guid for index of document Id and type. Auto generated.
    	string IndexDocIdTypeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// special RefTimeline property    
    	IProperty PropertyRefTimeline { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string TableTurnoverPropertyIdGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverPropertyVersionGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableTurnoverPropertyPostDateGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableBalancePropertyIdGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableBalancePropertyVersionGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TableBalanceGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Mapping register properties to document properties
    	IReadOnlyList<IRegisterDocToReg> ListDocMappings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IGroupListForms GroupForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListReports GroupReports { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRegisterDocToReg // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Document GUID
    	string DocGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Mappings
    	IReadOnlyList<IRegisterRegPropToDocProp> ListMappings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRegisterRegPropToDocProp // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Register property GUID
    	string RegPropGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Document property GUID
    	string DocPropGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IDocumentEnumeratorSequence : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCodeType SequenceType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint MaxSequenceLength { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Prefix { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumDocNumberUniqueScope ScopeOfUnique { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumMonths ScopePeriodStartMonth { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint ScopePeriodStartMonthDay { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IGroupListEnumeratorSequences : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IDocumentEnumeratorSequence> ListEnumeratorSequences { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IDocumentEnumeratorSequence this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IGroupDocuments : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PrefixForCompositionNames { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IDocumentTimeline DocumentTimeline { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListDocuments GroupListDocuments { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string DocShortTypeIdPropertyName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListRegisters GroupRegisters { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListJournals GroupJournals { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListEnumeratorSequences GroupListSequences { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	Google.Protobuf.WellKnownTypes.Timestamp MondayBeforeFirstDocDate { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyDocNumberName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool UseDocNumberProperty { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IDocumentTimeline : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IProperty> ListProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	EnumTimeAccuracyType TimeLineTimeAccuracy { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string TimeLineDocDateTimePropertyName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PropertyTimelineDocDateTimeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ShortIdTypeForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Last generated Protobuf field position
    	uint LastGenPosition { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IDocument : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string SequenceGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IGroupListProperties GroupProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListDetails GroupDetails { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListForms GroupForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IGroupListReports GroupReports { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	// Sequential unique number in configuration
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IGroupListDocuments : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ShortIdTypeForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IDocument> ListDocuments { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IDocument this[int index] { get; }
    	int Count();
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRelationsGroup : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IRelationsManyToManyGroup GroupListManyToManyRelations { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IRelationsOneToOneGroup GroupListOneToOneRelations { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRelationNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	EnumRelationConfigType RefObjType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? GuidObj { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IRelationManyToMany : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRelationConfigType RefObj1Type { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? GuidObj1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRelationConfigType RefObj2Type { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? GuidObj2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsUseHistory { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Sequential unique number in configuration
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IProperty PropertyRefObj1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IProperty PropertyRefObj2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string PropertyDataTimeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRelationsManyToManyGroup : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PrefixForCompositionNames { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ShortIdTypeForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRelationManyToMany> ListRelations { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRelationOneToOne : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRelationConfigType RefObj1Type { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? GuidObj1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsRelationReferenceNullable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumOneToOneRefType RefType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRelationConfigType RefObj2Type { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? GuidObj2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsUseHistory { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Sequential unique number in configuration
    	uint ShortId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// Combination of short_id and type group in higher bits
    	uint ShortRefId { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IProperty PropertyRefObj1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IProperty PropertyRefObj2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	string PropertyDataTimeGuid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IRelationsOneToOneGroup : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string PrefixForCompositionNames { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string ShortIdTypeForCacheKey { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IRelationOneToOne> ListRelations { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // J O U R N A L
    // @exclude
    // ####################################### J O U R N A L ##########################################
    
    public partial interface IGroupListJournals : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IJournal> ListJournals { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IJournal this[int index] { get; }
    	int Count();
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IJournal : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridSortableCustom { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsGridFilterable { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IDocInJournal> ListSelectedDocsWithProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IDocInJournal // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	// doc guid
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<string> ListPropertyGuids { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    }
    // F O R M S
    // @exclude
    // ####################################### F O R M S ##########################################
    
    public partial interface IGroupListForms : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IForm> ListForms { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IForm this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Children collection can contain:
    //   - Children of Grid System
    
    public partial interface IForm : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsUseCode { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsUseName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsUseDesc { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsUseFolderCode { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsUseFolderName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsUseFolderDesc { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumUseType IsUseDocDate { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsDummy { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	FormType EnumFormType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IFormGridSystem GridSystem { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<string> ListGuidViewProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    	IReadOnlyList<string> ListGuidViewFolderProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Children are collection of Grid System Rows 
    
    public partial interface IFormGridSystem : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IFormGridSystemRow> ListRows { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<string> ListGuidProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Children are collection of Grid System Columns 
    
    public partial interface IFormGridSystemRow : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IFormGridSystemColumn> ListColumns { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormGridSystemColumn : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumHiddenType HideType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint? WidthXs { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint? WidthSm { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint? WidthMd { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint? WidthLg { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint? WidthXl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	uint? WidthXx { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IFormAutoLayoutBlock FormBlock { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Children collection can contain:
    //   - Fields
    //   - Data grids
    //   - Grid Systems
    //   - Tab Controls
    //   - Auto Layout Blocks
    
    public partial interface IFormAutoLayoutBlock : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IFormAutoLayoutSubBlock> ListFormAutoLayoutSubBlock { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // https://learn.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0
    
    public partial interface IFormAutoLayoutSubBlock : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IFormTabControl TabControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IFormDataGrid DataGridControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IFormAutoLayoutBlock AutoLayoutBlockControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IFormField FieldControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IFormGridSystem GridSystemControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IFormTree TreeControl { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IFormField : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Children are collection of Auto Layout Block children
    
    public partial interface IFormTabControlTab : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<string> ListGuidProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    	IFormAutoLayoutBlock FormBlock { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // Children are collection of Tab Control Tabs
    
    public partial interface IFormTabControl : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<IFormTabControlTab> ListTabs { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReadOnlyList<string> ListGuidProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // No Children
    
    public partial interface IFormDataGrid : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<string> ListGuidProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // No Children
    
    public partial interface IFormTree : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IReadOnlyList<string> ListGuidProperties { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:46
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    // R E P O R T S
    // @exclude
    // ####################################### R E P O R T S ##########################################
    
    public partial interface IGroupListReports : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// repeated proto_property list_shared_properties = 6;
    	IReadOnlyList<IReport> ListReports { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    	IReport this[int index] { get; }
    	int Count();
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IReport : IGuid, IName // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
        //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    	string NameUi { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Description { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsNew { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsMarkedForDeletion { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	// repeated proto_group_properties list_properties = 6;
    	// repeated proto_document list_documents = 7;
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:48
    }
    
    public partial interface IModelRow // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string GroupName { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Name { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string Guid { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsIncluded { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPropertyDataGenerator // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	EnumPropertyDataType DataType { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumAddressDataType Address { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCommerceDataType Commerce { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumCompanyDataType Company { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumDateDataType Date { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumDatabaseDataType Database { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumFinanceDataType Finance { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumHackerDataType Hacker { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumImageDataType Image { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumInternetDataType Internet { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumLoremDataType Lorem { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumNameDataType Name { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumPhoneDataType Phone { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRantDataType Rant { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumSystemDataType System { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumVehicleDataType Vehicle { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	EnumRandomDataType Random { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
}