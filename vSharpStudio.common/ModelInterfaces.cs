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
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IPlugin> ListPluginsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IPlugin : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		string Version { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IPluginGenerator> ListGeneratorsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IPluginGenerator : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		string Description { get; } // ModelInterfaces.tt Line: 43
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IPluginGeneratorSettings> ListSettingsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IPluginGeneratorSettings : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		
		///////////////////////////////////////////////////
		/// This Description is taken from Plugin Generator
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 43
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string GeneratorSettings { get; } // ModelInterfaces.tt Line: 43
		bool IsPrivate { get; } // ModelInterfaces.tt Line: 43
		string FilePath { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface ISettingsConfig : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// current migration version, increased by one on each deployment
		///////////////////////////////////////////////////
		int VersionMigrationCurrent { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// min version supported by current version for migration
		///////////////////////////////////////////////////
		int VersionMigrationSupportFromMin { get; } // ModelInterfaces.tt Line: 43
	}
	
	///////////////////////////////////////////////////
	/// General DB settings
	///////////////////////////////////////////////////
	
	public partial interface IDbSettings  // ModelInterfaces.tt Line: 26
	{
		string DbSchema { get; } // ModelInterfaces.tt Line: 43
		DbIdGeneratorMethod IdGenerator { get; } // ModelInterfaces.tt Line: 43
		string KeyType { get; } // ModelInterfaces.tt Line: 43
		string KeyName { get; } // ModelInterfaces.tt Line: 43
		string Timestamp { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// if yes: 
		///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
		/// if no:
		///    1. Find DB type from 
		///    2. Create connection string from db_server, db_database_name, db_user
		///////////////////////////////////////////////////
		bool IsDbFromConnectionString { get; } // ModelInterfaces.tt Line: 43
		string ConnectionStringName { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
		string PathToProjectWithConnectionString { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IAppDbSettings  // ModelInterfaces.tt Line: 26
	{
		string PluginGuid { get; } // ModelInterfaces.tt Line: 43
		string PluginName { get; } // ModelInterfaces.tt Line: 43
		string Version { get; } // ModelInterfaces.tt Line: 43
		string PluginGenGuid { get; } // ModelInterfaces.tt Line: 43
		string PluginGenName { get; } // ModelInterfaces.tt Line: 43
		string ConnGuid { get; } // ModelInterfaces.tt Line: 43
		string ConnName { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IGroupListAppSolutions : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IAppDbSettings DefaultDbI { get; } // ModelInterfaces.tt Line: 47
		
		///////////////////////////////////////////////////
		/// List NET solutions
		///////////////////////////////////////////////////
		IEnumerable<IAppSolution> ListAppSolutionsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IAppSolution : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// List NET projects
		///////////////////////////////////////////////////
		string RelativeAppSolutionPath { get; } // ModelInterfaces.tt Line: 43
		IAppDbSettings DefaultDbI { get; } // ModelInterfaces.tt Line: 47
		IEnumerable<IAppProject> ListAppProjectsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IAppProject : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		string RelativeAppProjectPath { get; } // ModelInterfaces.tt Line: 43
		IAppDbSettings DefaultDbI { get; } // ModelInterfaces.tt Line: 47
	}
	
	public partial interface IConfigShortHistory : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		IConfig CurrentConfigI { get; } // ModelInterfaces.tt Line: 47
		IConfig PrevStableConfigI { get; } // ModelInterfaces.tt Line: 47
		IConfig OldStableConfigI { get; } // ModelInterfaces.tt Line: 47
	}
	
	public partial interface IGroupListBaseConfigLinks : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IBaseConfigLink> ListBaseConfigLinksI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IBaseConfigLink : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IConfig ConfigNodeI { get; } // ModelInterfaces.tt Line: 47
		string RelativeConfigFilePath { get; } // ModelInterfaces.tt Line: 43
	}
	
	///////////////////////////////////////////////////
	/// Configuration config
	///////////////////////////////////////////////////
	
	public partial interface IConfig : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		int Version { get; } // ModelInterfaces.tt Line: 43
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		Google.Protobuf.WellKnownTypes.Timestamp LastUpdated { get; } // ModelInterfaces.tt Line: 43
		EnumPrimaryKeyType PrimaryKeyType { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// GENERAL DB SETTINGS
		///////////////////////////////////////////////////
		IDbSettings DbSettingsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListBaseConfigLinks GroupConfigLinksI { get; } // ModelInterfaces.tt Line: 47
		IConfigModel ModelI { get; } // ModelInterfaces.tt Line: 47
		IGroupListPlugins GroupPluginsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListAppSolutions GroupAppSolutionsI { get; } // ModelInterfaces.tt Line: 47
	}
	
	///////////////////////////////////////////////////
	/// Configuration model
	///////////////////////////////////////////////////
	
	public partial interface IConfigModel : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		int Version { get; } // ModelInterfaces.tt Line: 43
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IGroupListCommon GroupCommonI { get; } // ModelInterfaces.tt Line: 47
		IGroupListConstants GroupConstantsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListEnumerations GroupEnumerationsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListCatalogs GroupCatalogsI { get; } // ModelInterfaces.tt Line: 47
		IGroupDocuments GroupDocumentsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListJournals GroupJournalsI { get; } // ModelInterfaces.tt Line: 47
	}
	
	public partial interface IDataType  // ModelInterfaces.tt Line: 26
	{
		EnumDataType DataTypeEnum { get; } // ModelInterfaces.tt Line: 43
		uint Length { get; } // ModelInterfaces.tt Line: 43
		uint Accuracy { get; } // ModelInterfaces.tt Line: 43
		bool IsPositive { get; } // ModelInterfaces.tt Line: 43
		string ObjectGuid { get; } // ModelInterfaces.tt Line: 43
		bool IsNullable { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<string> ListObjectGuidsI { get; } // ModelInterfaces.tt Line: 38
		bool IsIndexFk { get; } // ModelInterfaces.tt Line: 43
	}
	
	///////////////////////////////////////////////////
	/// Common parameters section
	///////////////////////////////////////////////////
	
	public partial interface IGroupListCommon : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IGroupListRoles GroupRolesI { get; } // ModelInterfaces.tt Line: 47
		IGroupListMainViewForms GroupViewFormsI { get; } // ModelInterfaces.tt Line: 47
	}
	
	///////////////////////////////////////////////////
	/// User's role
	///////////////////////////////////////////////////
	
	public partial interface IRole : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IGroupListRoles : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IRole> ListRolesI { get; } // ModelInterfaces.tt Line: 40
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy parent
	///////////////////////////////////////////////////
	
	public partial interface IMainViewForm : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IGroupListMainViewForms GroupListViewFormsI { get; } // ModelInterfaces.tt Line: 47
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy node with children
	///////////////////////////////////////////////////
	
	public partial interface IGroupListMainViewForms : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IMainViewForm> ListMainViewFormsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IGroupListPropertiesTabs : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IPropertiesTab> ListPropertiesTabsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IPropertiesTab : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IGroupListProperties GroupPropertiesI { get; } // ModelInterfaces.tt Line: 47
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; } // ModelInterfaces.tt Line: 47
		
		///////////////////////////////////////////////////
		/// Create Index for foreign key navigation property
		///////////////////////////////////////////////////
		bool IsIndexFk { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IGroupListProperties : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IProperty> ListPropertiesI { get; } // ModelInterfaces.tt Line: 40
		
		///////////////////////////////////////////////////
		/// Last generated Protobuf field position
		///////////////////////////////////////////////////
		uint LastGenPosition { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IProperty : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IDataType DataTypeI { get; } // ModelInterfaces.tt Line: 47
		
		///////////////////////////////////////////////////
		/// Protobuf field position
		/// Reserved positions: 1 - primary key
		///////////////////////////////////////////////////
		uint Position { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IGroupListConstants : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IConstant> ListConstantsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	
	public partial interface IConstant : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IDataType DataTypeI { get; } // ModelInterfaces.tt Line: 47
	}
	
	public partial interface IGroupListEnumerations : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IEnumeration> ListEnumerationsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IEnumeration : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// Enumeration element type
		///////////////////////////////////////////////////
		EnumEnumerationType DataTypeEnum { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// Length of string if 'STRING' is selected as enumeration element type
		///////////////////////////////////////////////////
		int DataTypeLength { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IEnumerationPair> ListEnumerationPairsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IEnumerationPair : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		string Value { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface ICatalog : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IGroupListProperties GroupPropertiesI { get; } // ModelInterfaces.tt Line: 47
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListForms GroupFormsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListReports GroupReportsI { get; } // ModelInterfaces.tt Line: 47
	}
	
	public partial interface IGroupListCatalogs : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<ICatalog> ListCatalogsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IGroupDocuments : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IGroupListProperties GroupSharedPropertiesI { get; } // ModelInterfaces.tt Line: 47
		IGroupListDocuments GroupListDocumentsI { get; } // ModelInterfaces.tt Line: 47
	}
	
	public partial interface IDocument : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IGroupListProperties GroupPropertiesI { get; } // ModelInterfaces.tt Line: 47
		IGroupListPropertiesTabs GroupPropertiesTabsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListForms GroupFormsI { get; } // ModelInterfaces.tt Line: 47
		IGroupListReports GroupReportsI { get; } // ModelInterfaces.tt Line: 47
	}
	
	public partial interface IGroupListDocuments : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		IEnumerable<IDocument> ListDocumentsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IGroupListJournals : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IJournal> ListJournalsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IJournal : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IDocument> ListDocumentsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IGroupListForms : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IForm> ListFormsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IForm : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_forms = 7;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IGroupListReports : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		string Description { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		IEnumerable<IReport> ListReportsI { get; } // ModelInterfaces.tt Line: 40
	}
	
	public partial interface IReport : IValidatableWithSeverity, IGuid, IName  // ModelInterfaces.tt Line: 26
	{
		ulong SortingValue { get; } // ModelInterfaces.tt Line: 43
		string NameUi { get; } // ModelInterfaces.tt Line: 43
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_documents = 7;
		///////////////////////////////////////////////////
		string Description { get; } // ModelInterfaces.tt Line: 43
	}
	
	public partial interface IModelRow  // ModelInterfaces.tt Line: 26
	{
		string GroupName { get; } // ModelInterfaces.tt Line: 43
		string Name { get; } // ModelInterfaces.tt Line: 43
		string Guid { get; } // ModelInterfaces.tt Line: 43
		bool IsIncluded { get; } // ModelInterfaces.tt Line: 43
	}
}