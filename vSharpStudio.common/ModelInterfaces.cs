using System.Collections.Generic;

namespace vSharpStudio.common
{
	public enum DbIdGeneratorMethod {
		Identity = 0,
		HiLo = 1,
	}
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
		IEnumerable<IPlugin> ListPluginsI { get; }
	}
	
	public interface IPlugin
	{
		string Guid { get; }
		string Name { get; }
		string Description { get; }
		ulong SortingValue { get; }
		IEnumerable<IPluginGenerator> ListGeneratorsI { get; }
	}
	
	public interface IPluginGenerator
	{
		string Guid { get; }
		string Name { get; }
		string Description { get; }
		ulong SortingValue { get; }
		IEnumerable<IPluginGeneratorSettings> ListSettingsI { get; }
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
	/// General DB settings
	///////////////////////////////////////////////////
	
	public interface IDbSettings
	{
		string DbSchema { get; }
		DbIdGeneratorMethod IdGenerator { get; }
		string KeyType { get; }
		string KeyName { get; }
		string Timestamp { get; }
		
		///////////////////////////////////////////////////
		/// if yes: 
		///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
		/// if no:
		///    1. Find DB type from 
		///    2. Create connection string from db_server, db_database_name, db_user
		///////////////////////////////////////////////////
		bool IsDbFromConnectionString { get; }
		string ConnectionStringName { get; }
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
		string PathToProjectWithConnectionString { get; }
	}
	
	public interface IGroupListBaseConfigs
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; }
		IEnumerable<IBaseConfig> ListBaseConfigsI { get; }
	}
	
	public interface IBaseConfig
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; }
		IConfig ConfigNodeI { get; }
		string RelativeConfigFilePath { get; }
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
		/// GENERAL DB SETTINGS
		///////////////////////////////////////////////////
		IDbSettings DbSettingsI { get; }
		IGroupListPlugins GroupPluginsI { get; }
		IGroupListBaseConfigs GroupConfigsI { get; }
		IGroupListConstants GroupConstantsI { get; }
		IGroupListEnumerations GroupEnumerationsI { get; }
		IGroupListCatalogs GroupCatalogsI { get; }
		IGroupDocuments GroupDocumentsI { get; }
		IGroupListJournals GroupJournalsI { get; }
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
		IEnumerable<IPropertiesTab> ListPropertiesTabsI { get; }
	}
	
	public interface IPropertiesTab
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListPropertiesTabs GroupPropertiesSubtabsI { get; }
	}
	
	public interface IGroupListProperties
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IProperty> ListPropertiesI { get; }
	}
	
	public interface IProperty
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDataType DataTypeI { get; }
	}
	
	public interface IGroupListConstants
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IConstant> ListConstantsI { get; }
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
		IDataType DataTypeI { get; }
	}
	
	public interface IGroupListEnumerations
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IEnumeration> ListEnumerationsI { get; }
	}
	
	public interface IEnumeration
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		EnumEnumerationType DataTypeEnum { get; }
		IEnumerable<IEnumerationPair> ListEnumerationPairsI { get; }
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
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListForms GroupFormsI { get; }
		IGroupListReports GroupReportsI { get; }
	}
	
	public interface IGroupListCatalogs
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<ICatalog> ListCatalogsI { get; }
	}
	
	public interface IGroupDocuments
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupSharedPropertiesI { get; }
		IGroupListDocuments GroupListDocumentsI { get; }
	}
	
	public interface IDocument
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; }
		IGroupListForms GroupFormsI { get; }
		IGroupListReports GroupReportsI { get; }
	}
	
	public interface IGroupListDocuments
	{
		string Guid { get; }
		string Name { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IDocument> ListDocumentsI { get; }
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
		IEnumerable<IJournal> ListJournalsI { get; }
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
		IEnumerable<IDocument> ListDocumentsI { get; }
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
		IEnumerable<IForm> ListFormsI { get; }
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
		IEnumerable<IReport> ListReportsI { get; }
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