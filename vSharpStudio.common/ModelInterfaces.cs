using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 10
{
	public enum DbIdGeneratorMethod { // ModelInterfaces.tt Line: 13
		Identity = 0,
		HiLo = 1,
	}
	public enum EnumPrimaryKeyType { // ModelInterfaces.tt Line: 13
		INT = 0,
		LONG = 1,
	}
	public enum EnumDataType { // ModelInterfaces.tt Line: 13
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
	public enum EnumEnumerationType { // ModelInterfaces.tt Line: 13
		STRING_VALUE = 0,
		BYTE_VALUE = 1,
		SHORT_VALUE = 2,
		INTEGER_VALUE = 3,
	}
	
	public partial interface IGroupListPlugins : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IPlugin> ListPluginsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IPlugin : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		string Description { get; } // ModelInterfaces.tt Line: 41
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IPluginGenerator> ListGeneratorsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IPluginGenerator : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		string Description { get; } // ModelInterfaces.tt Line: 41
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IPluginGeneratorSettings> ListSettingsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IPluginGeneratorSettings : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		
		///////////////////////////////////////////////////
		/// This Description is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string GeneratorSettings { get; } // ModelInterfaces.tt Line: 41
		bool IsPrivate { get; } // ModelInterfaces.tt Line: 41
		string FilePath { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface ISettingsConfig : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// current migration version, increased by one on each deployment
		///////////////////////////////////////////////////
		int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// min version supported by current version for migration
		///////////////////////////////////////////////////
		int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 41
	}
	
	///////////////////////////////////////////////////
	/// General DB settings
	///////////////////////////////////////////////////
	
	public partial interface IDbSettings  // ModelInterfaces.tt Line: 26
	{
		string DbSchema { get; } // ModelInterfaces.tt Line: 41
		DbIdGeneratorMethod IdGenerator { get; } // ModelInterfaces.tt Line: 41
		string KeyType { get; } // ModelInterfaces.tt Line: 41
		string KeyName { get; } // ModelInterfaces.tt Line: 41
		string Timestamp { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// if yes: 
		///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
		/// if no:
		///    1. Find DB type from 
		///    2. Create connection string from db_server, db_database_name, db_user
		///////////////////////////////////////////////////
		bool IsDbFromConnectionString { get; } // ModelInterfaces.tt Line: 41
		string ConnectionStringName { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
		string PathToProjectWithConnectionString { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IGroupListAppSolutions : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// List NET solutions
		///////////////////////////////////////////////////
		IEnumerable<IAppSolution> ListAppSolutionsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IAppSolution : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// List NET projects
		///////////////////////////////////////////////////
		string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 41
		string ConnectionStringName { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IAppProject> ListAppProjectsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IAppProject : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
		string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 41
		string ConnectionStringName { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IConfigShortHistory : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		IConfig CurrentConfigI { get; } // ModelInterfaces.tt Line: 45
		IConfig PrevStableConfigI { get; } // ModelInterfaces.tt Line: 45
		IConfig OldStableConfigI { get; } // ModelInterfaces.tt Line: 45
	}
	
	public partial interface IGroupListBaseConfigs : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IBaseConfig> ListBaseConfigsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IBaseConfig : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
		IConfig ConfigNodeI { get; } // ModelInterfaces.tt Line: 45
		string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 41
	}
	
	///////////////////////////////////////////////////
	/// Configuration config
	///////////////////////////////////////////////////
	
	public partial interface IConfig : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		int Version { get; } // ModelInterfaces.tt Line: 41
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 41
		EnumPrimaryKeyType PrimaryKeyType { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// GENERAL DB SETTINGS
		///////////////////////////////////////////////////
		IDbSettings DbSettingsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListAppSolutions GroupAppSolutionsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListPlugins GroupPluginsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListBaseConfigs GroupConfigsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListCommon GroupCommonI { get; } // ModelInterfaces.tt Line: 45
		IGroupListConstants GroupConstantsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListEnumerations GroupEnumerationsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListCatalogs GroupCatalogsI { get; } // ModelInterfaces.tt Line: 45
		IGroupDocuments GroupDocumentsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListJournals GroupJournalsI { get; } // ModelInterfaces.tt Line: 45
	}
	
	public partial interface IDataType  // ModelInterfaces.tt Line: 26
	{
		EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 41
		uint Length { get; } // ModelInterfaces.tt Line: 41
		uint Accuracy { get; } // ModelInterfaces.tt Line: 41
		bool IsPositive { get; } // ModelInterfaces.tt Line: 41
		string ObjectGuid { get; } // ModelInterfaces.tt Line: 41
		bool IsNullable { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<string> ListObjectGuidsI { get; } // ModelInterfaces.tt Line: 36
		bool IsIndexFk { get; } // ModelInterfaces.tt Line: 41
	}
	
	///////////////////////////////////////////////////
	/// Common parameters section
	///////////////////////////////////////////////////
	
	public partial interface IGroupListCommon : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IGroupListRoles GroupRolesI { get; } // ModelInterfaces.tt Line: 45
		IGroupListMainViewForms GroupViewFormsI { get; } // ModelInterfaces.tt Line: 45
	}
	
	///////////////////////////////////////////////////
	/// User's role
	///////////////////////////////////////////////////
	
	public partial interface IRole : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IGroupListRoles : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IRole> ListRolesI { get; } // ModelInterfaces.tt Line: 38
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy parent
	///////////////////////////////////////////////////
	
	public partial interface IMainViewForm : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IGroupListMainViewForms GroupListViewFormsI { get; } // ModelInterfaces.tt Line: 45
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy node with children
	///////////////////////////////////////////////////
	
	public partial interface IGroupListMainViewForms : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IMainViewForm> ListMainViewFormsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IGroupListPropertiesTabs : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IPropertiesTab> ListPropertiesTabsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IPropertiesTab : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IGroupListProperties GroupPropertiesI { get; } // ModelInterfaces.tt Line: 45
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; } // ModelInterfaces.tt Line: 45
		
		///////////////////////////////////////////////////
		/// Create Index for foreign key navigation property
		///////////////////////////////////////////////////
		bool IsIndexFk { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IGroupListProperties : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IProperty> ListPropertiesI { get; } // ModelInterfaces.tt Line: 38
		
		///////////////////////////////////////////////////
		/// Last generated Protobuf field position
		///////////////////////////////////////////////////
		uint LastGenPosition { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IProperty : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IDataType DataTypeI { get; } // ModelInterfaces.tt Line: 45
		
		///////////////////////////////////////////////////
		/// Protobuf field position
		/// Reserved positions: 1 - primary key
		///////////////////////////////////////////////////
		uint Position { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IGroupListConstants : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IConstant> ListConstantsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	
	public partial interface IConstant : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IDataType DataTypeI { get; } // ModelInterfaces.tt Line: 45
	}
	
	public partial interface IGroupListEnumerations : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IEnumeration> ListEnumerationsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IEnumeration : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// Enumeration element type
		///////////////////////////////////////////////////
		EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// Length of string if 'STRING' is selected as enumeration element type
		///////////////////////////////////////////////////
		int DataTypeLength { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IEnumerationPair> ListEnumerationPairsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IEnumerationPair : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		string Value { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface ICatalog : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IGroupListProperties GroupPropertiesI { get; } // ModelInterfaces.tt Line: 45
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListForms GroupFormsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListReports GroupReportsI { get; } // ModelInterfaces.tt Line: 45
	}
	
	public partial interface IGroupListCatalogs : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<ICatalog> ListCatalogsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IGroupDocuments : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IGroupListProperties GroupSharedPropertiesI { get; } // ModelInterfaces.tt Line: 45
		IGroupListDocuments GroupListDocumentsI { get; } // ModelInterfaces.tt Line: 45
	}
	
	public partial interface IDocument : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IGroupListProperties GroupPropertiesI { get; } // ModelInterfaces.tt Line: 45
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListForms GroupFormsI { get; } // ModelInterfaces.tt Line: 45
		IGroupListReports GroupReportsI { get; } // ModelInterfaces.tt Line: 45
	}
	
	public partial interface IGroupListDocuments : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		IEnumerable<IDocument> ListDocumentsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IGroupListJournals : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IJournal> ListJournalsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IJournal : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IDocument> ListDocumentsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IGroupListForms : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IForm> ListFormsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IForm : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_forms = 7;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IGroupListReports : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		string Description { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IReport> ListReportsI { get; } // ModelInterfaces.tt Line: 38
	}
	
	public partial interface IReport : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
		string NameUi { get; } // ModelInterfaces.tt Line: 41
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_documents = 7;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 41
	}
	
	public partial interface IItemNameValue : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		Google.Protobuf.WellKnownTypes.Any Value { get; } // ModelInterfaces.tt Line: 43
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 41
	}
}