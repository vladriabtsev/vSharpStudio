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
    public enum EnumPrintAccess // ModelInterfaces.tt Line: 15
    {
        PR_BY_PARENT = 0,
        PR_NO_PRINT = 1,
        PR_PRINT = 2,
    }
    public enum EnumConstantAccess // ModelInterfaces.tt Line: 15
    {
        CN_BY_PARENT = 0,
        CN_HIDE = 1,
        CN_VIEW = 2,
        CN_EDIT = 3,
    }
    public enum EnumPropertyAccess // ModelInterfaces.tt Line: 15
    {
        P_BY_PARENT = 0,
        P_HIDE = 1,
        P_VIEW = 2,
        P_EDIT = 3,
    }
    public enum EnumCatalogDetailAccess // ModelInterfaces.tt Line: 15
    {
        C_BY_PARENT = 0,
        C_HIDE = 1,
        C_VIEW = 2,
        C_EDIT = 3,
        C_MARK_DEL = 4,
    }
    public enum EnumDocumentAccess // ModelInterfaces.tt Line: 15
    {
        D_BY_PARENT = 0,
        D_HIDE = 1,
        D_VIEW = 2,
        D_EDIT = 3,
        D_POST = 4,
        D_UNPOST = 5,
        D_MARK_DEL = 6,
    }
    public enum EnumPropertyDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumAddressDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumCommerceDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumCompanyDataType // ModelInterfaces.tt Line: 15
    {
        P_NONE = 0,
        P_COMPANY_NAME = 1,
        P_COMPANY_SUFFIX = 2,
        P_CATCH_PHRASE = 3,
        P_BS = 4,
    }
    public enum EnumDateDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumDatabaseDataType // ModelInterfaces.tt Line: 15
    {
        DB_NONE = 0,
        DB_COLUMN = 1,
        DB_TYPE = 2,
        DB_COLLATION = 3,
        DB_ENGINE = 4,
    }
    public enum EnumFinanceDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumHackerDataType // ModelInterfaces.tt Line: 15
    {
        H_NONE = 0,
        H_ABBREVIATION = 1,
        H_ADJECTIVE = 2,
        H_NOUN = 3,
        H_VERB = 4,
        H_INGVERB = 5,
        H_PHRASE = 6,
    }
    public enum EnumImageDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumInternetDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumLoremDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumNameDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumPhoneDataType // ModelInterfaces.tt Line: 15
    {
        PH_NONE = 0,
        PH_PHONE_NUMBER = 1,
        PH_PHONE_NUMBER_FORMAT = 2,
    }
    public enum EnumRantDataType // ModelInterfaces.tt Line: 15
    {
        R_NONE = 0,
        R_REVIEW = 1,
        R_REVIEWS = 2,
    }
    public enum EnumSystemDataType // ModelInterfaces.tt Line: 15
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
    public enum EnumVehicleDataType // ModelInterfaces.tt Line: 15
    {
        V_NONE = 0,
        V_VIN = 1,
        V_MANUFACTURER = 2,
        V_MODEL = 3,
        V_TYPE = 4,
        V_FUEL = 5,
    }
    public enum EnumRandomDataType // ModelInterfaces.tt Line: 15
    {
        RN_NONE = 0,
        RN_NUMBER = 1,
        RN_STRING = 2,
        RN_STRING2 = 3,
        RN_HASH = 4,
        RN_ALPHANUMERIC = 5,
        RN_HEXADECIMAL = 6,
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
        FormTypeNotSelected = 0,
        ListWide = 1,
        ItemEditForm = 2,
        FolderEditForm = 3,
        ListNarrow = 4,
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
    
    public partial interface IPluginGeneratorSolutionSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// string app_generator_guid = 2;
    	///////////////////////////////////////////////////
    	string Settings { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IPluginGeneratorProjectSettings // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	
    	///////////////////////////////////////////////////
    	/// string app_generator_guid = 2;
    	///////////////////////////////////////////////////
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
    }
    
    public partial interface IAppSolution // ModelInterfaces.tt Line: 29
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string ShortIdForCacheKey { get; } // ModelInterfaces.tt Line: 51
    	string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IAppProject> ListAppProjects { get; } // ModelInterfaces.tt Line: 44
    	
    	///////////////////////////////////////////////////
    	/// 
    	/// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IPluginGeneratorSolutionSettings> ListGeneratorsSolutionSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IAppProject // ModelInterfaces.tt Line: 29
    {
    	string NameUi { get; } // ModelInterfaces.tt Line: 51
    	string Description { get; } // ModelInterfaces.tt Line: 51
    	string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 51
    	bool IsNew { get; } // ModelInterfaces.tt Line: 51
    	bool IsMarkedForDeletion { get; } // ModelInterfaces.tt Line: 51
    	IReadOnlyList<IAppProjectGenerator> ListAppProjectGenerators { get; } // ModelInterfaces.tt Line: 44
    	
    	///////////////////////////////////////////////////
    	/// 
    	/// repeated proto_plugin_group_generators_settings list_group_generators_settings = 18;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IPluginGeneratorProjectSettings> ListGeneratorsProjectSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    
    public partial interface IRoleConstantAccess // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	EnumConstantAccess EditAccess { get; } // ModelInterfaces.tt Line: 51
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IRolePropertyAccess // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	EnumPropertyAccess EditAccess { get; } // ModelInterfaces.tt Line: 51
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IRoleCatalogAccess // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	EnumCatalogDetailAccess EditAccess { get; } // ModelInterfaces.tt Line: 51
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IRoleDetailAccess // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	EnumCatalogDetailAccess EditAccess { get; } // ModelInterfaces.tt Line: 51
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IRoleDocumentAccess // ModelInterfaces.tt Line: 29
    {
    	string Guid { get; } // ModelInterfaces.tt Line: 51
    	EnumDocumentAccess EditAccess { get; } // ModelInterfaces.tt Line: 51
    	EnumPrintAccess PrintAccess { get; } // ModelInterfaces.tt Line: 51
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
    	IReadOnlyList<IRoleDetailAccess> ListRoleDetailAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // ModelInterfaces.tt Line: 44
    	IReadOnlyList<IPluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; } // ModelInterfaces.tt Line: 44
    }
    
    public partial interface IPropertyDataGenerator // ModelInterfaces.tt Line: 29
    {
    	EnumPropertyDataType DataType { get; } // ModelInterfaces.tt Line: 51
    	EnumAddressDataType Address { get; } // ModelInterfaces.tt Line: 51
    	EnumCommerceDataType Commerce { get; } // ModelInterfaces.tt Line: 51
    	EnumCompanyDataType Company { get; } // ModelInterfaces.tt Line: 51
    	EnumDateDataType Date { get; } // ModelInterfaces.tt Line: 51
    	EnumDatabaseDataType Database { get; } // ModelInterfaces.tt Line: 51
    	EnumFinanceDataType Finance { get; } // ModelInterfaces.tt Line: 51
    	EnumHackerDataType Hacker { get; } // ModelInterfaces.tt Line: 51
    	EnumImageDataType Image { get; } // ModelInterfaces.tt Line: 51
    	EnumInternetDataType Internet { get; } // ModelInterfaces.tt Line: 51
    	EnumLoremDataType Lorem { get; } // ModelInterfaces.tt Line: 51
    	EnumNameDataType Name { get; } // ModelInterfaces.tt Line: 51
    	EnumPhoneDataType Phone { get; } // ModelInterfaces.tt Line: 51
    	EnumRantDataType Rant { get; } // ModelInterfaces.tt Line: 51
    	EnumSystemDataType System { get; } // ModelInterfaces.tt Line: 51
    	EnumVehicleDataType Vehicle { get; } // ModelInterfaces.tt Line: 51
    	EnumRandomDataType Random { get; } // ModelInterfaces.tt Line: 51
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
    	IPropertyDataGenerator DataGenerator { get; } // ModelInterfaces.tt Line: 55
    	
    	///////////////////////////////////////////////////
    	/// 
    	/// // @attr [PropertyOrderAttribute(28)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("Start Grid")]
    	/// // @attr [Description("Start new container of 12 columns grid system")]
    	/// bool is_start_12_col_grid_system = 28;
    	/// // @attr [PropertyOrderAttribute(29)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("Stop Grid")]
    	/// // @attr [Description("Stop current container of 12 columns grid system")]
    	/// bool is_stop_12_col_grid_system = 29;
    	/// // @attr [PropertyOrderAttribute(30)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("Start Column")]
    	/// // @attr [Description("Start new column of 12 columns grid system")]
    	/// bool is_start_new_column_12_col_grid_system = 30;
    	/// // @attr [PropertyOrderAttribute(32)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("Start Row")]
    	/// // @attr [Description("Start new row of 12 columns grid system")]
    	/// bool is_start_new_row_12_col_grid_system = 31;
    	/// // @attr [PropertyOrderAttribute(31)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("Column Name")]
    	/// // @attr [Description("Column Name of 12 columns grid system")]
    	/// string column_name_12_col_grid_system = 32;
    	/// // @attr [PropertyOrderAttribute(33)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("When Hide")]
    	/// // @attr [Description("Condition of hiding base on screen size")]
    	/// proto_enum_hidden_type hide_type = 33;
    	/// // @attr [PropertyOrderAttribute(34)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("XS")]
    	/// // @attr [Description("Extra small. Small to large phone. Range: < 600px")]
    	/// google.protobuf.UInt32Value width_xs = 34;
    	/// // @attr [PropertyOrderAttribute(35)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("SM")]
    	/// // @attr [Description("Small. Small to medium tablet. Range: 600px > < 960px")]
    	/// google.protobuf.UInt32Value width_sm = 35;
    	/// // @attr [PropertyOrderAttribute(36)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("MD")]
    	/// // @attr [Description("Medium. Large tablet to laptop. Range: 960px > < 1280px")]
    	/// google.protobuf.UInt32Value width_md = 36;
    	/// // @attr [PropertyOrderAttribute(37)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("LG")]
    	/// // @attr [Description("Large. Desktop. Range: 1280px > < 1920px")]
    	/// google.protobuf.UInt32Value width_lg = 37;
    	/// // @attr [PropertyOrderAttribute(38)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("XL")]
    	/// // @attr [Description("Extra Large. HD and 4k. Range: 1920px > < 2560px")]
    	/// google.protobuf.UInt32Value width_xl = 38;
    	/// // @attr [PropertyOrderAttribute(39)]
    	/// // @attr [Category("12 Column Grid System")]
    	/// // @attr [DisplayName("XX")]
    	/// // @attr [Description("Extra Extra Large. 4k+ and ultra-wide. Range: >= 2560px")]
    	/// google.protobuf.UInt32Value width_xx = 39;
    	///////////////////////////////////////////////////
    	IReadOnlyList<IRolePropertyAccess> ListRolePropertyAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleConstantAccess> ListRoleConstantAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	string PropertyIsFolderGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyRefSelfGuid { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortable { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridSortableCustom { get; } // ModelInterfaces.tt Line: 51
    	EnumUseType IsGridFilterable { get; } // ModelInterfaces.tt Line: 51
    	IGroupListProperties GroupProperties { get; } // ModelInterfaces.tt Line: 55
    	IGroupListDetails GroupDetails { get; } // ModelInterfaces.tt Line: 55
    	IGroupListForms GroupForms { get; } // ModelInterfaces.tt Line: 55
    	IGroupListReports GroupReports { get; } // ModelInterfaces.tt Line: 55
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	EnumCatalogTreeIcon ItemIconType { get; } // ModelInterfaces.tt Line: 51
    	EnumCatalogTreeIcon GroupIconType { get; } // ModelInterfaces.tt Line: 51
    	string ViewListWideGuid { get; } // ModelInterfaces.tt Line: 51
    	string ViewListNarrowGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIdGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyCodeGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyNameGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyDescriptionGuid { get; } // ModelInterfaces.tt Line: 51
    	string PropertyIsFolderGuid { get; } // ModelInterfaces.tt Line: 51
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
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleCatalogAccess> ListRoleCatalogAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // ModelInterfaces.tt Line: 44
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
    	IReadOnlyList<IRoleDocumentAccess> ListRoleDocumentAccessSettings { get; } // ModelInterfaces.tt Line: 44
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