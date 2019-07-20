using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

namespace vSharpStudio.common
{
	public enum DbIdGeneratorMethod {
		Identity = 0,
		HiLo = 1,
	}
	public enum EnumPrimaryKeyType {
		INT = 0,
		LONG = 1,
	}
	public enum EnumDataType {
		STRING = 0,
		NUMERICAL = 1,
		BOOL = 2,
		TIME = 3,
		DATE = 4,
		DATETIME = 5,
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
	
	public partial interface IGroupListPlugins : IGuid, IName 
	{
		ulong SortingValue { get; }
		IEnumerable<IPlugin> ListPluginsI { get; }
	}
	
	public partial interface IPlugin : IGuid, IName 
	{
		string Description { get; }
		ulong SortingValue { get; }
		IEnumerable<IPluginGenerator> ListGeneratorsI { get; }
	}
	
	public partial interface IPluginGenerator : IGuid, IName 
	{
		string Description { get; }
		ulong SortingValue { get; }
		IEnumerable<IPluginGeneratorSettings> ListSettingsI { get; }
	}
	
	public partial interface IPluginGeneratorSettings : IGuid, IName 
	{
		
		///////////////////////////////////////////////////
		/// This Description is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Description { get; }
		ulong SortingValue { get; }
		string GeneratorSettings { get; }
		bool IsPrivate { get; }
		string FilePath { get; }
	}
	
	public partial interface ISettingsConfig : IGuid, IName 
	{
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
	
	public partial interface IConfigShortHistory : IGuid, IName 
	{
		IConfig CurrentConfigI { get; }
		IConfig PrevStableConfigI { get; }
		IConfig OldStableConfigI { get; }
	}
	
	public partial interface IGroupListBaseConfigs : IGuid, IName 
	{
		ulong SortingValue { get; }
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; }
		IEnumerable<IBaseConfig> ListBaseConfigsI { get; }
	}
	
	public partial interface IBaseConfig : IGuid, IName 
	{
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
	
	public partial interface IConfig : IGuid, IName 
	{
		int Version { get; }
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; }
		EnumPrimaryKeyType PrimaryKeyType { get; }
		
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
		bool IsNullable { get; }
		IEnumerable<string> ListObjectGuidsI { get; }
	}
	
	///////////////////////////////////////////////////
	/// Common parameters section
	///////////////////////////////////////////////////
	
	public partial interface IGroupListCommon : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListRoles GroupRolesI { get; }
		IGroupListMainViewForms GroupViewFormsI { get; }
	}
	
	///////////////////////////////////////////////////
	/// User's role
	///////////////////////////////////////////////////
	
	public partial interface IRole : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
	}
	
	public partial interface IGroupListRoles : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IRole> ListRolesI { get; }
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy parent
	///////////////////////////////////////////////////
	
	public partial interface IMainViewForm : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListMainViewForms GroupListViewFormsI { get; }
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy node with children
	///////////////////////////////////////////////////
	
	public partial interface IGroupListMainViewForms : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IMainViewForm> ListMainViewFormsI { get; }
	}
	
	public partial interface IGroupListPropertiesTabs : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IPropertiesTab> ListPropertiesTabsI { get; }
	}
	
	public partial interface IPropertiesTab : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; }
		bool IsIndexFk { get; }
	}
	
	public partial interface IGroupListProperties : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IProperty> ListPropertiesI { get; }
		
		///////////////////////////////////////////////////
		/// Last generated Protobuf field position
		///////////////////////////////////////////////////
		uint LastGenPosition { get; }
	}
	
	public partial interface IProperty : IGuid, IName 
	{
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
	
	public partial interface IGroupListConstants : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IConstant> ListConstantsI { get; }
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	
	public partial interface IConstant : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IDataType DataTypeI { get; }
	}
	
	public partial interface IGroupListEnumerations : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IEnumeration> ListEnumerationsI { get; }
	}
	
	public partial interface IEnumeration : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// Enumeration element type
		///////////////////////////////////////////////////
		EnumEnumerationType DataTypeEnum { get; }
		
		///////////////////////////////////////////////////
		/// Length of string if 'STRING' is selected as enumeration element type
		///////////////////////////////////////////////////
		int DataTypeLength { get; }
		IEnumerable<IEnumerationPair> ListEnumerationPairsI { get; }
	}
	
	public partial interface IEnumerationPair : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		string Value { get; }
	}
	
	public partial interface ICatalog : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		bool IsIndexFk { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; }
		IGroupListForms GroupFormsI { get; }
		IGroupListReports GroupReportsI { get; }
	}
	
	public partial interface IGroupListCatalogs : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<ICatalog> ListCatalogsI { get; }
	}
	
	public partial interface IGroupDocuments : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupSharedPropertiesI { get; }
		IGroupListDocuments GroupListDocumentsI { get; }
	}
	
	public partial interface IDocument : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IGroupListProperties GroupPropertiesI { get; }
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; }
		IGroupListForms GroupFormsI { get; }
		IGroupListReports GroupReportsI { get; }
	}
	
	public partial interface IGroupListDocuments : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		IEnumerable<IDocument> ListDocumentsI { get; }
	}
	
	public partial interface IGroupListJournals : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IJournal> ListJournalsI { get; }
	}
	
	public partial interface IJournal : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IDocument> ListDocumentsI { get; }
	}
	
	public partial interface IGroupListForms : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IForm> ListFormsI { get; }
	}
	
	public partial interface IForm : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_forms = 7;
		///////////////////////////////////////////////////
		string Description { get; }
	}
	
	public partial interface IGroupListReports : IGuid, IName 
	{
		ulong SortingValue { get; }
		string NameUi { get; }
		string Description { get; }
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IReport> ListReportsI { get; }
	}
	
	public partial interface IReport : IGuid, IName 
	{
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