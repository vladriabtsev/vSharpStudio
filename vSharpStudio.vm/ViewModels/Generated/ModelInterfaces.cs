
namespace vSharpStudio.common
{
	public enum EnumDataType {
		STRING = 0,
		NUMERICAL = 1,
		BOOL = 5,
		ENUMERATION = 8,
		CATALOG = 9,
		CATALOGS = 10,
		DOCUMENT = 11,
		DOCUMENTS = 12,
		ANY = 15,
	}
	public enum EnumEnumerationType {
		STRING_VALUE = 0,
		BYTE_VALUE = 1,
		SHORT_VALUE = 2,
		INTEGER_VALUE = 3,
	}
	
	public interface IGroupListPlugins
	{
		ulong SortingValue { get; }
		IPlugin ListPlugins { get; }
	}
	
	public interface IPlugin
	{
		string Guid { get; }
		string Name { get; }
		string Description { get; }
		ulong SortingValue { get; }
		IPluginGenerator ListPluginGenerators { get; }
	}
	
	public interface IPluginGenerator
	{
		string Guid { get; }
		string Name { get; }
		string Description { get; }
		ulong SortingValue { get; }
		IPluginGeneratorSettings ListPluginGeneratorSettings { get; }
	}
	
	public interface IPluginGeneratorSettings
	{
		
		///////////////////////////////////////////////////
		/// This Guid is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Guid { get; }
		
		///////////////////////////////////////////////////
		/// This Name is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Name { get; }
		
		///////////////////////////////////////////////////
		/// This Description is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Description { get; }
		ulong SortingValue { get; }
		string GeneratorSettings { get; }
		bool IsPrivate { get; }
		string FilePath { get; }
	}
	
	public interface ISettingsConfig
	{
		string Guid { get; }
		string Name { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// current migration version, increased by one on each deployment
		///////////////////////////////////////////////////
		int VersionMigrationCurrent { get; }
		
		///////////////////////////////////////////////////
		/// min version supported by current version for migration
		///////////////////////////////////////////////////
		int VersionMigrationSupportFromMin { get; }
	}
	
	///////////////////////////////////////////////////
	/// Primary key generation strategy
	///////////////////////////////////////////////////
	
	public interface IDbIdGenerator
	{
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		bool? IsPrimaryKeyClustered { get; }
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		bool? IsMemoryOptimized { get; }
		
		///////////////////////////////////////////////////
		/// SequenceHiLo or IdentityColumn. MsSql
		///////////////////////////////////////////////////
		bool? IsSequenceHiLo { get; }
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		string HiLoSequenceName { get; }
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		string HiLoSchema { get; }
	}
	
	public interface IGroupConfigs
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; }
		IConfigTree Children { get; }
		string RelativeConfigPath { get; }
	}
	
	public interface IConfigTree
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; }
		IConfig ConfigNode { get; }
		IConfigTree Children { get; }
	}
	
	///////////////////////////////////////////////////
	/// Configuration config
	///////////////////////////////////////////////////
	
	public interface IConfig
	{
		
		///////////////////////////////////////////////////
		/// Unique Guid for configuration (for comparison)
		///////////////////////////////////////////////////
		string Guid { get; }
		string Version { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// if yes: 
		///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
		/// if no:
		///    1. Find DB type from 
		///    2. Create connection string from db_server, db_database_name, db_user
		///////////////////////////////////////////////////
		bool IsDbFromConnectionString { get; }
		string ConnectionStringName { get; }
		string DbServer { get; }
		string DbDatabaseName { get; }
		bool IsDbWindowsAuthentication { get; }
		string DbUser { get; }
		string DbPassword { get; }
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
		string PathToProjectWithConnectionString { get; }
		string DbSchema { get; }
		string PrimaryKeyName { get; }
		IDbIdGenerator DbIdGenerator { get; }
		
		///////////////////////////////////////////////////
		/// CONFIG OBJECTS
		///////////////////////////////////////////////////
		IGroupListPlugins GroupPlugins { get; }
		IGroupConfigs GroupConfigs { get; }
		IGroupListConstants GroupConstants { get; }
		IGroupListEnumerations GroupEnumerations { get; }
		IGroupListCatalogs GroupCatalogs { get; }
		IGroupDocuments GroupDocuments { get; }
		IGroupListJournals GroupJournals { get; }
	}
	
	public interface IDataType
	{
		EnumDataType DataTypeEnum { get; }
		uint Length { get; }
		uint Accuracy { get; }
		bool IsPositive { get; }
		string ObjectGuid { get; }
	}
	
	public interface IGroupListPropertiesTabs
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupPropertiesTab Children { get; }
	}
	
	public interface IGroupPropertiesTab
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupProperties { get; }
		IGroupListPropertiesTabs GroupPropertiesSubtabs { get; }
	}
	
	public interface IGroupListProperties
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IProperty Children { get; }
	}
	
	public interface IProperty
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDataType DataType { get; }
	}
	
	public interface IGroupListConstants
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IConstant Children { get; }
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	
	public interface IConstant
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDataType DataType { get; }
	}
	
	public interface IGroupListEnumerations
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumeration Children { get; }
	}
	
	public interface IEnumeration
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		EnumEnumerationType DataTypeEnum { get; }
		IEnumerationPair Children { get; }
	}
	
	public interface IEnumerationPair
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		string Value { get; }
	}
	
	public interface ICatalog
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDbIdGenerator DbIdGenerator { get; }
		IGroupListProperties GroupProperties { get; }
		IGroupListForms GroupForms { get; }
		IGroupListReports GroupReports { get; }
	}
	
	public interface IGroupListCatalogs
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		ICatalog Children { get; }
	}
	
	public interface IGroupDocuments
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupSharedProperties { get; }
		IGroupListDocuments GroupListDocuments { get; }
	}
	
	public interface IDocument
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupProperties { get; }
		IGroupListPropertiesTabs GroupPropertiesTabs { get; }
		IGroupListForms GroupForms { get; }
		IGroupListReports GroupReports { get; }
		IDbIdGenerator DbIdGenerator { get; }
	}
	
	public interface IGroupListDocuments
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDocument Children { get; }
	}
	
	public interface IGroupListJournals
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IJournal Children { get; }
	}
	
	public interface IJournal
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		IDocument Children { get; }
	}
	
	public interface IGroupListForms
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IForm Children { get; }
	}
	
	public interface IForm
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_forms = 7;
		///////////////////////////////////////////////////
		string Description { get; }
	}
	
	public interface IGroupListReports
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IReport Children { get; }
	}
	
	public interface IReport
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_documents = 7;
		///////////////////////////////////////////////////
		string Description { get; }
	}
}