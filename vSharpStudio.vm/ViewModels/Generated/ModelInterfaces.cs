using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

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
	
	public partial interface IGroupListPlugins
	{
		ulong SortingValue { get; }
		IEnumerable<IPlugin> ListPluginsI { get; }
	}
	
	public partial interface IPlugin
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		string Description { get; }
		ulong SortingValue { get; }
		IEnumerable<IPluginGenerator> ListGeneratorsI { get; }
	}
	
	public partial interface IPluginGenerator
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		string Description { get; }
		ulong SortingValue { get; }
		IEnumerable<IPluginGeneratorSettings> ListSettingsI { get; }
	}
	
	public partial interface IPluginGeneratorSettings
	{
		
		///////////////////////////////////////////////////
		/// This Guid is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Guid { get; }
		
		///////////////////////////////////////////////////
		/// This Name is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Name { get; }
		string FullName { get; }
		
		///////////////////////////////////////////////////
		/// This Description is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Description { get; }
		ulong SortingValue { get; }
		string GeneratorSettings { get; }
		bool IsPrivate { get; }
		string FilePath { get; }
	}
	
	public partial interface ISettingsConfig
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
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
	
	public partial interface IDbSettings
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
	
	public partial interface IConfigLastReleases
	{
		IConfig CurrentConfigI { get; }
		IConfig PrevStableConfigI { get; }
		IConfig OldStableConfigI { get; }
	}
	
	public partial interface IGroupListBaseConfigs
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; }
		IEnumerable<IBaseConfig> ListBaseConfigsI { get; }
	}
	
	public partial interface IBaseConfig
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
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
	
	public partial interface IConfig
	{
		
		///////////////////////////////////////////////////
		/// Unique Guid for configuration (for comparison)
		///////////////////////////////////////////////////
		string Guid { get; }
		string Version { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; }
		
		///////////////////////////////////////////////////
		/// GENERAL DB SETTINGS
		///////////////////////////////////////////////////
		IDbSettings DbSettingsI { get; }
		IGroupListPlugins GroupPluginsI { get; }
		IGroupListBaseConfigs GroupConfigsI { get; }
		IGroupListCommon GroupCommonI { get; }
		IGroupListConstants GroupConstantsI { get; }
		IGroupListEnumerations GroupEnumerationsI { get; }
		IGroupListCatalogs GroupCatalogsI { get; }
		IGroupDocuments GroupDocumentsI { get; }
		IGroupListJournals GroupJournalsI { get; }
	}
	
	public partial interface IDataType
	{
		EnumDataType DataTypeEnum { get; }
		uint Length { get; }
		uint Accuracy { get; }
		bool IsPositive { get; }
		string ObjectGuid { get; }
	}
	
	///////////////////////////////////////////////////
	/// Common parameters section
	///////////////////////////////////////////////////
	
	public partial interface IGroupListCommon
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListRoles GroupRolesI { get; }
		IGroupListMainViewForms GroupViewFormsI { get; }
	}
	
	///////////////////////////////////////////////////
	/// User's role
	///////////////////////////////////////////////////
	
	public partial interface IRole
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
	}
	
	public partial interface IGroupListRoles
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IRole> ListRolesI { get; }
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy parent
	///////////////////////////////////////////////////
	
	public partial interface IMainViewForm
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListMainViewForms GroupListViewFormsI { get; }
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy node with children
	///////////////////////////////////////////////////
	
	public partial interface IGroupListMainViewForms
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IMainViewForm> ListMainViewFormsI { get; }
	}
	
	public partial interface IGroupListPropertiesTabs
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IPropertiesTab> ListPropertiesTabsI { get; }
	}
	
	public partial interface IPropertiesTab
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListPropertiesTabs GroupPropertiesSubtabsI { get; }
	}
	
	public partial interface IGroupListProperties
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IProperty> ListPropertiesI { get; }
		
		///////////////////////////////////////////////////
		/// Last generated Protobuf field position
		///////////////////////////////////////////////////
		uint LastGenPosition { get; }
	}
	
	public partial interface IProperty
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDataType DataTypeI { get; }
		
		///////////////////////////////////////////////////
		/// Protobuf field position
		/// Reserved positions: 1 - primary key
		///////////////////////////////////////////////////
		uint Position { get; }
	}
	
	public partial interface IGroupListConstants
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IConstant> ListConstantsI { get; }
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	
	public partial interface IConstant
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDataType DataTypeI { get; }
	}
	
	public partial interface IGroupListEnumerations
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IEnumeration> ListEnumerationsI { get; }
	}
	
	public partial interface IEnumeration
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		EnumEnumerationType DataTypeEnum { get; }
		IEnumerable<IEnumerationPair> ListEnumerationPairsI { get; }
	}
	
	public partial interface IEnumerationPair
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		string Value { get; }
	}
	
	public partial interface ICatalog
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListForms GroupFormsI { get; }
		IGroupListReports GroupReportsI { get; }
	}
	
	public partial interface IGroupListCatalogs
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<ICatalog> ListCatalogsI { get; }
	}
	
	public partial interface IGroupDocuments
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupSharedPropertiesI { get; }
		IGroupListDocuments GroupListDocumentsI { get; }
	}
	
	public partial interface IDocument
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; }
		IGroupListForms GroupFormsI { get; }
		IGroupListReports GroupReportsI { get; }
	}
	
	public partial interface IGroupListDocuments
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IDocument> ListDocumentsI { get; }
	}
	
	public partial interface IGroupListJournals
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IJournal> ListJournalsI { get; }
	}
	
	public partial interface IJournal
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IDocument> ListDocumentsI { get; }
	}
	
	public partial interface IGroupListForms
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IForm> ListFormsI { get; }
	}
	
	public partial interface IForm
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_forms = 7;
		///////////////////////////////////////////////////
		string Description { get; }
	}
	
	public partial interface IGroupListReports
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IReport> ListReportsI { get; }
	}
	
	public partial interface IReport
	{
		string Guid { get; }
		string Name { get; }
		string FullName { get; }
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