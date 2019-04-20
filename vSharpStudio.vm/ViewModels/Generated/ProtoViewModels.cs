// Auto generated on UTC 04/20/2019 20:04:36
using System;
using System.Linq;
using ViewModelBase;
using FluentValidation;
using Proto.Config;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017
	
	public partial class Config : ConfigObjectBase<Config, Config.ConfigValidator>, IComparable<Config>, IAccept
	{
	
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config() : base(ConfigValidator.Validator)
		{
			this.GroupConstants = new GroupConstants(this);
			this.GroupEnumerations = new GroupEnumerations(this);
			this.GroupCatalogs = new GroupCatalogs(this);
			this.GroupDocuments = new GroupDocuments(this);
			this.GroupJournals = new GroupJournals(this);
			OnInit();
		}
		public Config(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Config.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Config Clone(ITreeConfigNode parent, Config from, bool isDeep = true, bool isNewGuid = false)
		{
		    Config vm = new Config();
		    vm.Guid = from.Guid;
		    vm.Version = from.Version;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.IsDbFromConnectionString = from.IsDbFromConnectionString;
		    vm.ConnectionStringName = from.ConnectionStringName;
		    vm.DbTypeEnum = from.DbTypeEnum;
		    vm.DbServer = from.DbServer;
		    vm.DbDatabaseName = from.DbDatabaseName;
		    vm.IsDbWindowsAuthentication = from.IsDbWindowsAuthentication;
		    vm.DbUser = from.DbUser;
		    vm.DbPassword = from.DbPassword;
		    vm.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
		    vm.DbSchema = from.DbSchema;
		    vm.PrimaryKeyName = from.PrimaryKeyName;
		    vm.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered;
		    vm.IsMemoryOptimized = from.IsMemoryOptimized;
		    vm.IsSequenceHiLo = from.IsSequenceHiLo;
		    vm.HiLoSequenceName = from.HiLoSequenceName;
		    vm.HiLoSchema = from.HiLoSchema;
		    if (isDeep)
		        vm.GroupConstants = vSharpStudio.vm.ViewModels.GroupConstants.Clone(vm, from.GroupConstants, isDeep);
		    if (isDeep)
		        vm.GroupEnumerations = vSharpStudio.vm.ViewModels.GroupEnumerations.Clone(vm, from.GroupEnumerations, isDeep);
		    if (isDeep)
		        vm.GroupCatalogs = vSharpStudio.vm.ViewModels.GroupCatalogs.Clone(vm, from.GroupCatalogs, isDeep);
		    if (isDeep)
		        vm.GroupDocuments = vSharpStudio.vm.ViewModels.GroupDocuments.Clone(vm, from.GroupDocuments, isDeep);
		    if (isDeep)
		        vm.GroupJournals = vSharpStudio.vm.ViewModels.GroupJournals.Clone(vm, from.GroupJournals, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Config to, Config from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Version = from.Version;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    to.IsDbFromConnectionString = from.IsDbFromConnectionString;
		    to.ConnectionStringName = from.ConnectionStringName;
		    to.DbTypeEnum = from.DbTypeEnum;
		    to.DbServer = from.DbServer;
		    to.DbDatabaseName = from.DbDatabaseName;
		    to.IsDbWindowsAuthentication = from.IsDbWindowsAuthentication;
		    to.DbUser = from.DbUser;
		    to.DbPassword = from.DbPassword;
		    to.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
		    to.DbSchema = from.DbSchema;
		    to.PrimaryKeyName = from.PrimaryKeyName;
		    to.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered;
		    to.IsMemoryOptimized = from.IsMemoryOptimized;
		    to.IsSequenceHiLo = from.IsSequenceHiLo;
		    to.HiLoSequenceName = from.HiLoSequenceName;
		    to.HiLoSchema = from.HiLoSchema;
		    if (isDeep)
		        GroupConstants.Update(to.GroupConstants, from.GroupConstants, isDeep);
		    if (isDeep)
		        GroupEnumerations.Update(to.GroupEnumerations, from.GroupEnumerations, isDeep);
		    if (isDeep)
		        GroupCatalogs.Update(to.GroupCatalogs, from.GroupCatalogs, isDeep);
		    if (isDeep)
		        GroupDocuments.Update(to.GroupDocuments, from.GroupDocuments, isDeep);
		    if (isDeep)
		        GroupJournals.Update(to.GroupJournals, from.GroupJournals, isDeep);
		}
		#region IEditable
		public override Config Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Config.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Config from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Config.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_config' to 'Config'
		public static Config ConvertToVM(proto_config m, Config vm = null)
		{
		    if (vm == null)
		        vm = new Config();
		    vm.Guid = m.Guid;
		    vm.Version = m.Version;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.IsDbFromConnectionString = m.IsDbFromConnectionString;
		    vm.ConnectionStringName = m.ConnectionStringName;
		    vm.DbTypeEnum = m.DbTypeEnum;
		    vm.DbServer = m.DbServer;
		    vm.DbDatabaseName = m.DbDatabaseName;
		    vm.IsDbWindowsAuthentication = m.IsDbWindowsAuthentication;
		    vm.DbUser = m.DbUser;
		    vm.DbPassword = m.DbPassword;
		    vm.PathToProjectWithConnectionString = m.PathToProjectWithConnectionString;
		    vm.DbSchema = m.DbSchema;
		    vm.PrimaryKeyName = m.PrimaryKeyName;
		    vm.IsPrimaryKeyClustered = m.IsPrimaryKeyClustered;
		    vm.IsMemoryOptimized = m.IsMemoryOptimized;
		    vm.IsSequenceHiLo = m.IsSequenceHiLo;
		    vm.HiLoSequenceName = m.HiLoSequenceName;
		    vm.HiLoSchema = m.HiLoSchema;
		    vm.GroupConstants = vSharpStudio.vm.ViewModels.GroupConstants.ConvertToVM(m.GroupConstants);
		    vm.GroupEnumerations = vSharpStudio.vm.ViewModels.GroupEnumerations.ConvertToVM(m.GroupEnumerations);
		    vm.GroupCatalogs = vSharpStudio.vm.ViewModels.GroupCatalogs.ConvertToVM(m.GroupCatalogs);
		    vm.GroupDocuments = vSharpStudio.vm.ViewModels.GroupDocuments.ConvertToVM(m.GroupDocuments);
		    vm.GroupJournals = vSharpStudio.vm.ViewModels.GroupJournals.ConvertToVM(m.GroupJournals);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Config' to 'proto_config'
		public static proto_config ConvertToProto(Config vm)
		{
		    proto_config m = new proto_config();
		    m.Guid = vm.Guid;
		    m.Version = vm.Version;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.IsDbFromConnectionString = vm.IsDbFromConnectionString;
		    m.ConnectionStringName = vm.ConnectionStringName;
		    m.DbTypeEnum = vm.DbTypeEnum;
		    m.DbServer = vm.DbServer;
		    m.DbDatabaseName = vm.DbDatabaseName;
		    m.IsDbWindowsAuthentication = vm.IsDbWindowsAuthentication;
		    m.DbUser = vm.DbUser;
		    m.DbPassword = vm.DbPassword;
		    m.PathToProjectWithConnectionString = vm.PathToProjectWithConnectionString;
		    m.DbSchema = vm.DbSchema;
		    m.PrimaryKeyName = vm.PrimaryKeyName;
		    m.IsPrimaryKeyClustered = vm.IsPrimaryKeyClustered;
		    m.IsMemoryOptimized = vm.IsMemoryOptimized;
		    m.IsSequenceHiLo = vm.IsSequenceHiLo;
		    m.HiLoSequenceName = vm.HiLoSequenceName;
		    m.HiLoSchema = vm.HiLoSchema;
		    m.GroupConstants = vSharpStudio.vm.ViewModels.GroupConstants.ConvertToProto(vm.GroupConstants);
		    m.GroupEnumerations = vSharpStudio.vm.ViewModels.GroupEnumerations.ConvertToProto(vm.GroupEnumerations);
		    m.GroupCatalogs = vSharpStudio.vm.ViewModels.GroupCatalogs.ConvertToProto(vm.GroupCatalogs);
		    m.GroupDocuments = vSharpStudio.vm.ViewModels.GroupDocuments.ConvertToProto(vm.GroupDocuments);
		    m.GroupJournals = vSharpStudio.vm.ViewModels.GroupJournals.ConvertToProto(vm.GroupJournals);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupConstants.Accept(visitor);
			this.GroupEnumerations.Accept(visitor);
			this.GroupCatalogs.Accept(visitor);
			this.GroupDocuments.Accept(visitor);
			this.GroupJournals.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public string Version
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging();
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private string _Version = "";
		partial void OnVersionChanging();
		partial void OnVersionChanged();
		
		
		public string NameUi
		{ 
			set
			{
				if (_NameUi != value)
				{
					OnNameUiChanging();
					_NameUi = value;
					OnNameUiChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NameUi; }
		}
		private string _NameUi = "";
		partial void OnNameUiChanging();
		partial void OnNameUiChanged();
		
		
		public string Description
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		
		public bool IsDbFromConnectionString
		{ 
			set
			{
				if (_IsDbFromConnectionString != value)
				{
					OnIsDbFromConnectionStringChanging();
					_IsDbFromConnectionString = value;
					OnIsDbFromConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsDbFromConnectionString; }
		}
		private bool _IsDbFromConnectionString;
		partial void OnIsDbFromConnectionStringChanging();
		partial void OnIsDbFromConnectionStringChanged();
		
		
		public string ConnectionStringName
		{ 
			set
			{
				if (_ConnectionStringName != value)
				{
					OnConnectionStringNameChanging();
					_ConnectionStringName = value;
					OnConnectionStringNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectionStringName; }
		}
		private string _ConnectionStringName = "";
		partial void OnConnectionStringNameChanging();
		partial void OnConnectionStringNameChanged();
		
		
		public proto_config.Types.EnumDbType DbTypeEnum
		{ 
			set
			{
				if (_DbTypeEnum != value)
				{
					OnDbTypeEnumChanging();
					_DbTypeEnum = value;
					OnDbTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbTypeEnum; }
		}
		private proto_config.Types.EnumDbType _DbTypeEnum;
		partial void OnDbTypeEnumChanging();
		partial void OnDbTypeEnumChanged();
		
		
		public string DbServer
		{ 
			set
			{
				if (_DbServer != value)
				{
					OnDbServerChanging();
					_DbServer = value;
					OnDbServerChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbServer; }
		}
		private string _DbServer = "";
		partial void OnDbServerChanging();
		partial void OnDbServerChanged();
		
		
		public string DbDatabaseName
		{ 
			set
			{
				if (_DbDatabaseName != value)
				{
					OnDbDatabaseNameChanging();
					_DbDatabaseName = value;
					OnDbDatabaseNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbDatabaseName; }
		}
		private string _DbDatabaseName = "";
		partial void OnDbDatabaseNameChanging();
		partial void OnDbDatabaseNameChanged();
		
		
		public bool IsDbWindowsAuthentication
		{ 
			set
			{
				if (_IsDbWindowsAuthentication != value)
				{
					OnIsDbWindowsAuthenticationChanging();
					_IsDbWindowsAuthentication = value;
					OnIsDbWindowsAuthenticationChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsDbWindowsAuthentication; }
		}
		private bool _IsDbWindowsAuthentication;
		partial void OnIsDbWindowsAuthenticationChanging();
		partial void OnIsDbWindowsAuthenticationChanged();
		
		
		public string DbUser
		{ 
			set
			{
				if (_DbUser != value)
				{
					OnDbUserChanging();
					_DbUser = value;
					OnDbUserChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbUser; }
		}
		private string _DbUser = "";
		partial void OnDbUserChanging();
		partial void OnDbUserChanged();
		
		
		public string DbPassword
		{ 
			set
			{
				if (_DbPassword != value)
				{
					OnDbPasswordChanging();
					_DbPassword = value;
					OnDbPasswordChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbPassword; }
		}
		private string _DbPassword = "";
		partial void OnDbPasswordChanging();
		partial void OnDbPasswordChanged();
		
		
		public string PathToProjectWithConnectionString
		{ 
			set
			{
				if (_PathToProjectWithConnectionString != value)
				{
					OnPathToProjectWithConnectionStringChanging();
					_PathToProjectWithConnectionString = value;
					OnPathToProjectWithConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PathToProjectWithConnectionString; }
		}
		private string _PathToProjectWithConnectionString = "";
		partial void OnPathToProjectWithConnectionStringChanging();
		partial void OnPathToProjectWithConnectionStringChanged();
		
		
		public string DbSchema
		{ 
			set
			{
				if (_DbSchema != value)
				{
					OnDbSchemaChanging();
					_DbSchema = value;
					OnDbSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbSchema; }
		}
		private string _DbSchema = "";
		partial void OnDbSchemaChanging();
		partial void OnDbSchemaChanged();
		
		
		public string PrimaryKeyName
		{ 
			set
			{
				if (_PrimaryKeyName != value)
				{
					OnPrimaryKeyNameChanging();
					_PrimaryKeyName = value;
					OnPrimaryKeyNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PrimaryKeyName; }
		}
		private string _PrimaryKeyName = "";
		partial void OnPrimaryKeyNameChanging();
		partial void OnPrimaryKeyNameChanged();
		
		
		public bool IsPrimaryKeyClustered
		{ 
			set
			{
				if (_IsPrimaryKeyClustered != value)
				{
					OnIsPrimaryKeyClusteredChanging();
					_IsPrimaryKeyClustered = value;
					OnIsPrimaryKeyClusteredChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPrimaryKeyClustered; }
		}
		private bool _IsPrimaryKeyClustered;
		partial void OnIsPrimaryKeyClusteredChanging();
		partial void OnIsPrimaryKeyClusteredChanged();
		
		
		public bool IsMemoryOptimized
		{ 
			set
			{
				if (_IsMemoryOptimized != value)
				{
					OnIsMemoryOptimizedChanging();
					_IsMemoryOptimized = value;
					OnIsMemoryOptimizedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsMemoryOptimized; }
		}
		private bool _IsMemoryOptimized;
		partial void OnIsMemoryOptimizedChanging();
		partial void OnIsMemoryOptimizedChanged();
		
		
		public bool IsSequenceHiLo
		{ 
			set
			{
				if (_IsSequenceHiLo != value)
				{
					OnIsSequenceHiLoChanging();
					_IsSequenceHiLo = value;
					OnIsSequenceHiLoChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsSequenceHiLo; }
		}
		private bool _IsSequenceHiLo;
		partial void OnIsSequenceHiLoChanging();
		partial void OnIsSequenceHiLoChanged();
		
		
		public string HiLoSequenceName
		{ 
			set
			{
				if (_HiLoSequenceName != value)
				{
					OnHiLoSequenceNameChanging();
					_HiLoSequenceName = value;
					OnHiLoSequenceNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _HiLoSequenceName; }
		}
		private string _HiLoSequenceName = "";
		partial void OnHiLoSequenceNameChanging();
		partial void OnHiLoSequenceNameChanged();
		
		
		public string HiLoSchema
		{ 
			set
			{
				if (_HiLoSchema != value)
				{
					OnHiLoSchemaChanging();
					_HiLoSchema = value;
					OnHiLoSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _HiLoSchema; }
		}
		private string _HiLoSchema = "";
		partial void OnHiLoSchemaChanging();
		partial void OnHiLoSchemaChanged();
		
		
		public GroupConstants GroupConstants
		{ 
			set
			{
				if (_GroupConstants != value)
				{
					OnGroupConstantsChanging();
		            _GroupConstants = value;
					OnGroupConstantsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupConstants; }
		}
		private GroupConstants _GroupConstants;
		partial void OnGroupConstantsChanging();
		partial void OnGroupConstantsChanged();
		
		
		public GroupEnumerations GroupEnumerations
		{ 
			set
			{
				if (_GroupEnumerations != value)
				{
					OnGroupEnumerationsChanging();
		            _GroupEnumerations = value;
					OnGroupEnumerationsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupEnumerations; }
		}
		private GroupEnumerations _GroupEnumerations;
		partial void OnGroupEnumerationsChanging();
		partial void OnGroupEnumerationsChanged();
		
		
		public GroupCatalogs GroupCatalogs
		{ 
			set
			{
				if (_GroupCatalogs != value)
				{
					OnGroupCatalogsChanging();
		            _GroupCatalogs = value;
					OnGroupCatalogsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupCatalogs; }
		}
		private GroupCatalogs _GroupCatalogs;
		partial void OnGroupCatalogsChanging();
		partial void OnGroupCatalogsChanged();
		
		
		public GroupDocuments GroupDocuments
		{ 
			set
			{
				if (_GroupDocuments != value)
				{
					OnGroupDocumentsChanging();
		            _GroupDocuments = value;
					OnGroupDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupDocuments; }
		}
		private GroupDocuments _GroupDocuments;
		partial void OnGroupDocumentsChanging();
		partial void OnGroupDocumentsChanged();
		
		
		public GroupJournals GroupJournals
		{ 
			set
			{
				if (_GroupJournals != value)
				{
					OnGroupJournalsChanging();
		            _GroupJournals = value;
					OnGroupJournalsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupJournals; }
		}
		private GroupJournals _GroupJournals;
		partial void OnGroupJournalsChanging();
		partial void OnGroupJournalsChanged();
		#endregion Properties
	}
	
	public partial class Property : ConfigObjectBase<Property, Property.PropertyValidator>, IComparable<Property>, IAccept
	{
	
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { }
		#region CTOR
		public Property() : base(PropertyValidator.Validator)
		{
			this.DataType = new DataType(this);
			OnInit();
		}
		public Property(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Property.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Property Clone(ITreeConfigNode parent, Property from, bool isDeep = true, bool isNewGuid = false)
		{
		    Property vm = new Property();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    if (isDeep)
		        vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(vm, from.DataType, isDeep);
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Property to, Property from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		        DataType.Update(to.DataType, from.DataType, isDeep);
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		}
		#region IEditable
		public override Property Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Property.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Property from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Property.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_property' to 'Property'
		public static Property ConvertToVM(proto_property m, Property vm = null)
		{
		    if (vm == null)
		        vm = new Property();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType);
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Property' to 'proto_property'
		public static proto_property ConvertToProto(Property vm)
		{
		    proto_property m = new proto_property();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto(vm.DataType);
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.DataType.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[ExpandableObject()]
		public DataType DataType
		{ 
			set
			{
				if (_DataType != value)
				{
					OnDataTypeChanging();
		            _DataType = value;
					OnDataTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataType; }
		}
		private DataType _DataType;
		partial void OnDataTypeChanging();
		partial void OnDataTypeChanged();
		
		[PropertyOrder(2)]
		public string NameUi
		{ 
			set
			{
				if (_NameUi != value)
				{
					OnNameUiChanging();
					_NameUi = value;
					OnNameUiChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NameUi; }
		}
		private string _NameUi = "";
		partial void OnNameUiChanging();
		partial void OnNameUiChanged();
		
		[PropertyOrder(3)]
		public string Description
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		#endregion Properties
	}
	
	public partial class DataType : ConfigObjectBase<DataType, DataType.DataTypeValidator>, IComparable<DataType>, IAccept
	{
	
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { }
		#region CTOR
		public DataType() : base(DataTypeValidator.Validator)
		{
			OnInit();
		}
		public DataType(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(DataType.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static DataType Clone(ITreeConfigNode parent, DataType from, bool isDeep = true, bool isNewGuid = false)
		{
		    DataType vm = new DataType();
		    vm.DataTypeEnum = from.DataTypeEnum;
		    vm.Length = from.Length;
		    vm.Accuracy = from.Accuracy;
		    vm.IsPositive = from.IsPositive;
		    vm.TypeGuid = from.TypeGuid;
		    vm.MinValueString = from.MinValueString;
		    vm.MaxValueString = from.MaxValueString;
		    vm.ObjectName = from.ObjectName;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(DataType to, DataType from, bool isDeep = true)
		{
		    to.DataTypeEnum = from.DataTypeEnum;
		    to.Length = from.Length;
		    to.Accuracy = from.Accuracy;
		    to.IsPositive = from.IsPositive;
		    to.TypeGuid = from.TypeGuid;
		    to.MinValueString = from.MinValueString;
		    to.MaxValueString = from.MaxValueString;
		    to.ObjectName = from.ObjectName;
		}
		#region IEditable
		public override DataType Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return DataType.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(DataType from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    DataType.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_data_type' to 'DataType'
		public static DataType ConvertToVM(proto_data_type m, DataType vm = null)
		{
		    if (vm == null)
		        vm = new DataType();
		    vm.DataTypeEnum = m.DataTypeEnum;
		    vm.Length = m.Length;
		    vm.Accuracy = m.Accuracy;
		    vm.IsPositive = m.IsPositive;
		    vm.TypeGuid = m.TypeGuid;
		    vm.MinValueString = m.MinValueString;
		    vm.MaxValueString = m.MaxValueString;
		    vm.ObjectName = m.ObjectName;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'DataType' to 'proto_data_type'
		public static proto_data_type ConvertToProto(DataType vm)
		{
		    proto_data_type m = new proto_data_type();
		    m.DataTypeEnum = vm.DataTypeEnum;
		    m.Length = vm.Length;
		    m.Accuracy = vm.Accuracy;
		    m.IsPositive = vm.IsPositive;
		    m.TypeGuid = vm.TypeGuid;
		    m.MinValueString = vm.MinValueString;
		    m.MaxValueString = vm.MaxValueString;
		    m.ObjectName = vm.ObjectName;
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrder(2)]
		public proto_data_type.Types.EnumDataType DataTypeEnum
		{ 
			set
			{
				if (_DataTypeEnum != value)
				{
					OnDataTypeEnumChanging();
					_DataTypeEnum = value;
					OnDataTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeEnum; }
		}
		private proto_data_type.Types.EnumDataType _DataTypeEnum;
		partial void OnDataTypeEnumChanging();
		partial void OnDataTypeEnumChanged();
		
		[PropertyOrder(3)]
		public uint Length
		{ 
			set
			{
				if (_Length != value)
				{
					OnLengthChanging();
					_Length = value;
					OnLengthChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Length; }
		}
		private uint _Length;
		partial void OnLengthChanging();
		partial void OnLengthChanged();
		
		[PropertyOrder(4)]
		public uint Accuracy
		{ 
			set
			{
				if (_Accuracy != value)
				{
					OnAccuracyChanging();
					_Accuracy = value;
					OnAccuracyChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Accuracy; }
		}
		private uint _Accuracy;
		partial void OnAccuracyChanging();
		partial void OnAccuracyChanged();
		
		[PropertyOrder(5)]
		public bool IsPositive
		{ 
			set
			{
				if (_IsPositive != value)
				{
					OnIsPositiveChanging();
					_IsPositive = value;
					OnIsPositiveChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPositive; }
		}
		private bool _IsPositive;
		partial void OnIsPositiveChanging();
		partial void OnIsPositiveChanged();
		
		
		public string TypeGuid
		{ 
			set
			{
				if (_TypeGuid != value)
				{
					OnTypeGuidChanging();
					_TypeGuid = value;
					OnTypeGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TypeGuid; }
		}
		private string _TypeGuid = "";
		partial void OnTypeGuidChanging();
		partial void OnTypeGuidChanged();
		
		
		public string MinValueString
		{ 
			set
			{
				if (_MinValueString != value)
				{
					OnMinValueStringChanging();
					_MinValueString = value;
					OnMinValueStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MinValueString; }
		}
		private string _MinValueString = "";
		partial void OnMinValueStringChanging();
		partial void OnMinValueStringChanged();
		
		
		public string MaxValueString
		{ 
			set
			{
				if (_MaxValueString != value)
				{
					OnMaxValueStringChanging();
					_MaxValueString = value;
					OnMaxValueStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MaxValueString; }
		}
		private string _MaxValueString = "";
		partial void OnMaxValueStringChanging();
		partial void OnMaxValueStringChanged();
		
		[PropertyOrder(6)]
		public string ObjectName
		{ 
			set
			{
				if (_ObjectName != value)
				{
					OnObjectNameChanging();
					_ObjectName = value;
					OnObjectNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ObjectName; }
		}
		private string _ObjectName = "";
		partial void OnObjectNameChanging();
		partial void OnObjectNameChanged();
		#endregion Properties
	}
	
	public partial class Constant : ConfigObjectBase<Constant, Constant.ConstantValidator>, IComparable<Constant>, IAccept
	{
	
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { }
		#region CTOR
		public Constant() : base(ConstantValidator.Validator)
		{
			this.DataType = new DataType(this);
			OnInit();
		}
		public Constant(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Constant.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Constant Clone(ITreeConfigNode parent, Constant from, bool isDeep = true, bool isNewGuid = false)
		{
		    Constant vm = new Constant();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    if (isDeep)
		        vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(vm, from.DataType, isDeep);
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Constant to, Constant from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		        DataType.Update(to.DataType, from.DataType, isDeep);
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		}
		#region IEditable
		public override Constant Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Constant.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Constant from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Constant.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_constant' to 'Constant'
		public static Constant ConvertToVM(proto_constant m, Constant vm = null)
		{
		    if (vm == null)
		        vm = new Constant();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType);
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Constant' to 'proto_constant'
		public static proto_constant ConvertToProto(Constant vm)
		{
		    proto_constant m = new proto_constant();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto(vm.DataType);
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.DataType.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[ExpandableObject()]
		public DataType DataType
		{ 
			set
			{
				if (_DataType != value)
				{
					OnDataTypeChanging();
		            _DataType = value;
					OnDataTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataType; }
		}
		private DataType _DataType;
		partial void OnDataTypeChanging();
		partial void OnDataTypeChanged();
		
		[PropertyOrder(2)]
		public string NameUi
		{ 
			set
			{
				if (_NameUi != value)
				{
					OnNameUiChanging();
					_NameUi = value;
					OnNameUiChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NameUi; }
		}
		private string _NameUi = "";
		partial void OnNameUiChanging();
		partial void OnNameUiChanged();
		
		[PropertyOrder(3)]
		public string Description
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		#endregion Properties
	}
	
	public partial class GroupConstants : ConfigObjectBase<GroupConstants, GroupConstants.GroupConstantsValidator>, IComparable<GroupConstants>, IAccept
	{
	
		public partial class GroupConstantsValidator : ValidatorBase<GroupConstants, GroupConstantsValidator> { }
		#region CTOR
		public GroupConstants() : base(GroupConstantsValidator.Validator)
		{
			this.ListConstants = new SortedObservableCollection<Constant>();
			this.ListConstants.CollectionChanged += ListConstants_CollectionChanged;
			OnInit();
		}
		public GroupConstants(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupConstants.DefaultName, this, this.SubNodes);
	    }
		private void ListConstants_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Constant).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Constant))
		    {
		        this.ListConstants.Sort();
		    }
		}
		public static GroupConstants Clone(ITreeConfigNode parent, GroupConstants from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupConstants vm = new GroupConstants();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListConstants = new SortedObservableCollection<Constant>();
		    foreach(var t in from.ListConstants)
		        vm.ListConstants.Add(vSharpStudio.vm.ViewModels.Constant.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupConstants to, GroupConstants from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListConstants.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListConstants)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Constant.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListConstants.Remove(t);
		        }
		        foreach(var tt in from.ListConstants)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListConstants.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Constant();
		                vSharpStudio.vm.ViewModels.Constant.Update(p, tt, isDeep);
		                to.ListConstants.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupConstants Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupConstants.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupConstants from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupConstants.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_constants' to 'GroupConstants'
		public static GroupConstants ConvertToVM(proto_group_constants m, GroupConstants vm = null)
		{
		    if (vm == null)
		        vm = new GroupConstants();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListConstants = new SortedObservableCollection<Constant>();
		    foreach(var t in m.ListConstants)
		        vm.ListConstants.Add(vSharpStudio.vm.ViewModels.Constant.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupConstants' to 'proto_group_constants'
		public static proto_group_constants ConvertToProto(GroupConstants vm)
		{
		    proto_group_constants m = new proto_group_constants();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListConstants)
		        m.ListConstants.Add(vSharpStudio.vm.ViewModels.Constant.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListConstants)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Constant> ListConstants { get; set; }
		partial void OnListConstantsChanging();
		partial void OnListConstantsChanged();
		#endregion Properties
	}
	
	public partial class EnumerationPair : ConfigObjectBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>, IAccept
	{
	
		public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { }
		#region CTOR
		public EnumerationPair() : base(EnumerationPairValidator.Validator)
		{
			OnInit();
		}
		public EnumerationPair(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(EnumerationPair.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static EnumerationPair Clone(ITreeConfigNode parent, EnumerationPair from, bool isDeep = true, bool isNewGuid = false)
		{
		    EnumerationPair vm = new EnumerationPair();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.Value = from.Value;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(EnumerationPair to, EnumerationPair from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.Value = from.Value;
		}
		#region IEditable
		public override EnumerationPair Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return EnumerationPair.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(EnumerationPair from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    EnumerationPair.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_enumeration_pair' to 'EnumerationPair'
		public static EnumerationPair ConvertToVM(proto_enumeration_pair m, EnumerationPair vm = null)
		{
		    if (vm == null)
		        vm = new EnumerationPair();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.Value = m.Value;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
		public static proto_enumeration_pair ConvertToProto(EnumerationPair vm)
		{
		    proto_enumeration_pair m = new proto_enumeration_pair();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.Value = vm.Value;
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public string Value
		{ 
			set
			{
				if (_Value != value)
				{
					OnValueChanging();
					_Value = value;
					OnValueChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Value; }
		}
		private string _Value = "";
		partial void OnValueChanging();
		partial void OnValueChanged();
		#endregion Properties
	}
	
	public partial class Enumeration : ConfigObjectBase<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>, IAccept
	{
	
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { }
		#region CTOR
		public Enumeration() : base(EnumerationValidator.Validator)
		{
			this.ListValues = new SortedObservableCollection<EnumerationPair>();
			this.ListValues.CollectionChanged += ListValues_CollectionChanged;
			OnInit();
		}
		public Enumeration(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Enumeration.DefaultName, this, this.SubNodes);
	    }
		private void ListValues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as EnumerationPair).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(EnumerationPair))
		    {
		        this.ListValues.Sort();
		    }
		}
		public static Enumeration Clone(ITreeConfigNode parent, Enumeration from, bool isDeep = true, bool isNewGuid = false)
		{
		    Enumeration vm = new Enumeration();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.DataTypeEnum = from.DataTypeEnum;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListValues = new SortedObservableCollection<EnumerationPair>();
		    foreach(var t in from.ListValues)
		        vm.ListValues.Add(vSharpStudio.vm.ViewModels.EnumerationPair.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Enumeration to, Enumeration from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.DataTypeEnum = from.DataTypeEnum;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListValues.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListValues)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.EnumerationPair.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListValues.Remove(t);
		        }
		        foreach(var tt in from.ListValues)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListValues.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new EnumerationPair();
		                vSharpStudio.vm.ViewModels.EnumerationPair.Update(p, tt, isDeep);
		                to.ListValues.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Enumeration Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Enumeration.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Enumeration from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Enumeration.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_enumeration' to 'Enumeration'
		public static Enumeration ConvertToVM(proto_enumeration m, Enumeration vm = null)
		{
		    if (vm == null)
		        vm = new Enumeration();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.DataTypeEnum = m.DataTypeEnum;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListValues = new SortedObservableCollection<EnumerationPair>();
		    foreach(var t in m.ListValues)
		        vm.ListValues.Add(vSharpStudio.vm.ViewModels.EnumerationPair.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Enumeration' to 'proto_enumeration'
		public static proto_enumeration ConvertToProto(Enumeration vm)
		{
		    proto_enumeration m = new proto_enumeration();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.DataTypeEnum = vm.DataTypeEnum;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListValues)
		        m.ListValues.Add(vSharpStudio.vm.ViewModels.EnumerationPair.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListValues)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrder(4)]
		public proto_enumeration.Types.EnumEnumerationType DataTypeEnum
		{ 
			set
			{
				if (_DataTypeEnum != value)
				{
					OnDataTypeEnumChanging();
					_DataTypeEnum = value;
					OnDataTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeEnum; }
		}
		private proto_enumeration.Types.EnumEnumerationType _DataTypeEnum;
		partial void OnDataTypeEnumChanging();
		partial void OnDataTypeEnumChanged();
		
		[PropertyOrder(2)]
		public string NameUi
		{ 
			set
			{
				if (_NameUi != value)
				{
					OnNameUiChanging();
					_NameUi = value;
					OnNameUiChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NameUi; }
		}
		private string _NameUi = "";
		partial void OnNameUiChanging();
		partial void OnNameUiChanged();
		
		[PropertyOrder(3)]
		public string Description
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		
		public SortedObservableCollection<EnumerationPair> ListValues { get; set; }
		partial void OnListValuesChanging();
		partial void OnListValuesChanged();
		#endregion Properties
	}
	
	public partial class GroupEnumerations : ConfigObjectBase<GroupEnumerations, GroupEnumerations.GroupEnumerationsValidator>, IComparable<GroupEnumerations>, IAccept
	{
	
		public partial class GroupEnumerationsValidator : ValidatorBase<GroupEnumerations, GroupEnumerationsValidator> { }
		#region CTOR
		public GroupEnumerations() : base(GroupEnumerationsValidator.Validator)
		{
			this.ListEnumerations = new SortedObservableCollection<Enumeration>();
			this.ListEnumerations.CollectionChanged += ListEnumerations_CollectionChanged;
			OnInit();
		}
		public GroupEnumerations(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupEnumerations.DefaultName, this, this.SubNodes);
	    }
		private void ListEnumerations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Enumeration).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Enumeration))
		    {
		        this.ListEnumerations.Sort();
		    }
		}
		public static GroupEnumerations Clone(ITreeConfigNode parent, GroupEnumerations from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupEnumerations vm = new GroupEnumerations();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListEnumerations = new SortedObservableCollection<Enumeration>();
		    foreach(var t in from.ListEnumerations)
		        vm.ListEnumerations.Add(vSharpStudio.vm.ViewModels.Enumeration.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupEnumerations to, GroupEnumerations from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListEnumerations.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListEnumerations)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Enumeration.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListEnumerations.Remove(t);
		        }
		        foreach(var tt in from.ListEnumerations)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListEnumerations.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Enumeration();
		                vSharpStudio.vm.ViewModels.Enumeration.Update(p, tt, isDeep);
		                to.ListEnumerations.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupEnumerations Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupEnumerations.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupEnumerations from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupEnumerations.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_enumerations' to 'GroupEnumerations'
		public static GroupEnumerations ConvertToVM(proto_group_enumerations m, GroupEnumerations vm = null)
		{
		    if (vm == null)
		        vm = new GroupEnumerations();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListEnumerations = new SortedObservableCollection<Enumeration>();
		    foreach(var t in m.ListEnumerations)
		        vm.ListEnumerations.Add(vSharpStudio.vm.ViewModels.Enumeration.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupEnumerations' to 'proto_group_enumerations'
		public static proto_group_enumerations ConvertToProto(GroupEnumerations vm)
		{
		    proto_group_enumerations m = new proto_group_enumerations();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListEnumerations)
		        m.ListEnumerations.Add(vSharpStudio.vm.ViewModels.Enumeration.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListEnumerations)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Enumeration> ListEnumerations { get; set; }
		partial void OnListEnumerationsChanging();
		partial void OnListEnumerationsChanged();
		#endregion Properties
	}
	
	public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IAccept
	{
	
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog() : base(CatalogValidator.Validator)
		{
			this.ListProperties = new SortedObservableCollection<Property>();
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
			OnInit();
		}
		public Catalog(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Catalog.DefaultName, this, this.SubNodes);
	    }
		private void ListProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Property))
		    {
		        this.ListProperties.Sort();
		    }
		}
		public static Catalog Clone(ITreeConfigNode parent, Catalog from, bool isDeep = true, bool isNewGuid = false)
		{
		    Catalog vm = new Catalog();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered.HasValue ? from.IsPrimaryKeyClustered.Value : (bool?)null;
		    vm.IsMemoryOptimized = from.IsMemoryOptimized.HasValue ? from.IsMemoryOptimized.Value : (bool?)null;
		    vm.IsSequenceHiLo = from.IsSequenceHiLo.HasValue ? from.IsSequenceHiLo.Value : (bool?)null;
		    vm.HiLoSequenceName = from.HiLoSequenceName;
		    vm.HiLoSchema = from.HiLoSchema;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Catalog to, Catalog from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    to.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered.HasValue ? from.IsPrimaryKeyClustered.Value : (bool?)null;
		    to.IsMemoryOptimized = from.IsMemoryOptimized.HasValue ? from.IsMemoryOptimized.Value : (bool?)null;
		    to.IsSequenceHiLo = from.IsSequenceHiLo.HasValue ? from.IsSequenceHiLo.Value : (bool?)null;
		    to.HiLoSequenceName = from.HiLoSequenceName;
		    to.HiLoSchema = from.HiLoSchema;
		    if (isDeep)
		    {
		        foreach(var t in to.ListProperties.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListProperties)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Property.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListProperties.Remove(t);
		        }
		        foreach(var tt in from.ListProperties)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListProperties.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Property();
		                vSharpStudio.vm.ViewModels.Property.Update(p, tt, isDeep);
		                to.ListProperties.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Catalog Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Catalog.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Catalog from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Catalog.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_catalog' to 'Catalog'
		public static Catalog ConvertToVM(proto_catalog m, Catalog vm = null)
		{
		    if (vm == null)
		        vm = new Catalog();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.IsPrimaryKeyClustered = m.IsPrimaryKeyClustered.HasValue ? m.IsPrimaryKeyClustered.Value : (bool?)null;
		    vm.IsMemoryOptimized = m.IsMemoryOptimized.HasValue ? m.IsMemoryOptimized.Value : (bool?)null;
		    vm.IsSequenceHiLo = m.IsSequenceHiLo.HasValue ? m.IsSequenceHiLo.Value : (bool?)null;
		    vm.HiLoSequenceName = m.HiLoSequenceName;
		    vm.HiLoSchema = m.HiLoSchema;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Catalog' to 'proto_catalog'
		public static proto_catalog ConvertToProto(Catalog vm)
		{
		    proto_catalog m = new proto_catalog();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.IsPrimaryKeyClustered.Value = vm.IsPrimaryKeyClustered.Value;
		    m.IsPrimaryKeyClustered.HasValue = vm.IsPrimaryKeyClustered.HasValue;
		    m.IsMemoryOptimized.Value = vm.IsMemoryOptimized.Value;
		    m.IsMemoryOptimized.HasValue = vm.IsMemoryOptimized.HasValue;
		    m.IsSequenceHiLo.Value = vm.IsSequenceHiLo.Value;
		    m.IsSequenceHiLo.HasValue = vm.IsSequenceHiLo.HasValue;
		    m.HiLoSequenceName = vm.HiLoSequenceName;
		    m.HiLoSchema = vm.HiLoSchema;
		    foreach(var t in vm.ListProperties)
		        m.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListProperties)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrder(2)]
		public string NameUi
		{ 
			set
			{
				if (_NameUi != value)
				{
					OnNameUiChanging();
					_NameUi = value;
					OnNameUiChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NameUi; }
		}
		private string _NameUi = "";
		partial void OnNameUiChanging();
		partial void OnNameUiChanged();
		
		[PropertyOrder(3)]
		public string Description
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		
		public bool? IsPrimaryKeyClustered
		{ 
			set
			{
				if (_IsPrimaryKeyClustered != value)
				{
					OnIsPrimaryKeyClusteredChanging();
		            _IsPrimaryKeyClustered = value;
					OnIsPrimaryKeyClusteredChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPrimaryKeyClustered; }
		}
		private bool? _IsPrimaryKeyClustered;
		partial void OnIsPrimaryKeyClusteredChanging();
		partial void OnIsPrimaryKeyClusteredChanged();
		
		
		public bool? IsMemoryOptimized
		{ 
			set
			{
				if (_IsMemoryOptimized != value)
				{
					OnIsMemoryOptimizedChanging();
		            _IsMemoryOptimized = value;
					OnIsMemoryOptimizedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsMemoryOptimized; }
		}
		private bool? _IsMemoryOptimized;
		partial void OnIsMemoryOptimizedChanging();
		partial void OnIsMemoryOptimizedChanged();
		
		
		public bool? IsSequenceHiLo
		{ 
			set
			{
				if (_IsSequenceHiLo != value)
				{
					OnIsSequenceHiLoChanging();
		            _IsSequenceHiLo = value;
					OnIsSequenceHiLoChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsSequenceHiLo; }
		}
		private bool? _IsSequenceHiLo;
		partial void OnIsSequenceHiLoChanging();
		partial void OnIsSequenceHiLoChanged();
		
		
		public string HiLoSequenceName
		{ 
			set
			{
				if (_HiLoSequenceName != value)
				{
					OnHiLoSequenceNameChanging();
					_HiLoSequenceName = value;
					OnHiLoSequenceNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _HiLoSequenceName; }
		}
		private string _HiLoSequenceName = "";
		partial void OnHiLoSequenceNameChanging();
		partial void OnHiLoSequenceNameChanged();
		
		
		public string HiLoSchema
		{ 
			set
			{
				if (_HiLoSchema != value)
				{
					OnHiLoSchemaChanging();
					_HiLoSchema = value;
					OnHiLoSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _HiLoSchema; }
		}
		private string _HiLoSchema = "";
		partial void OnHiLoSchemaChanging();
		partial void OnHiLoSchemaChanged();
		
		
		public SortedObservableCollection<Property> ListProperties { get; set; }
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
		#endregion Properties
	}
	
	public partial class GroupProperties : ConfigObjectBase<GroupProperties, GroupProperties.GroupPropertiesValidator>, IComparable<GroupProperties>, IAccept
	{
	
		public partial class GroupPropertiesValidator : ValidatorBase<GroupProperties, GroupPropertiesValidator> { }
		#region CTOR
		public GroupProperties() : base(GroupPropertiesValidator.Validator)
		{
			this.ListProperties = new SortedObservableCollection<Property>();
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
			OnInit();
		}
		public GroupProperties(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupProperties.DefaultName, this, this.SubNodes);
	    }
		private void ListProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Property))
		    {
		        this.ListProperties.Sort();
		    }
		}
		public static GroupProperties Clone(ITreeConfigNode parent, GroupProperties from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupProperties vm = new GroupProperties();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupProperties to, GroupProperties from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListProperties.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListProperties)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Property.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListProperties.Remove(t);
		        }
		        foreach(var tt in from.ListProperties)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListProperties.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Property();
		                vSharpStudio.vm.ViewModels.Property.Update(p, tt, isDeep);
		                to.ListProperties.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupProperties Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupProperties.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupProperties from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupProperties.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_properties' to 'GroupProperties'
		public static GroupProperties ConvertToVM(proto_group_properties m, GroupProperties vm = null)
		{
		    if (vm == null)
		        vm = new GroupProperties();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupProperties' to 'proto_group_properties'
		public static proto_group_properties ConvertToProto(GroupProperties vm)
		{
		    proto_group_properties m = new proto_group_properties();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListProperties)
		        m.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListProperties)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Property> ListProperties { get; set; }
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
		#endregion Properties
	}
	
	public partial class GroupPropertiesTree : ConfigObjectBase<GroupPropertiesTree, GroupPropertiesTree.GroupPropertiesTreeValidator>, IComparable<GroupPropertiesTree>, IAccept
	{
	
		public partial class GroupPropertiesTreeValidator : ValidatorBase<GroupPropertiesTree, GroupPropertiesTreeValidator> { }
		#region CTOR
		public GroupPropertiesTree() : base(GroupPropertiesTreeValidator.Validator)
		{
			this.ListProperties = new SortedObservableCollection<Property>();
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
			this.ListSubPropertiesGroups = new SortedObservableCollection<GroupPropertiesTree>();
			this.ListSubPropertiesGroups.CollectionChanged += ListSubPropertiesGroups_CollectionChanged;
			OnInit();
		}
		public GroupPropertiesTree(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupPropertiesTree.DefaultName, this, this.SubNodes);
	    }
		private void ListProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		private void ListSubPropertiesGroups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as GroupPropertiesTree).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Property))
		    {
		        this.ListProperties.Sort();
		    }
		    if (type == typeof(GroupPropertiesTree))
		    {
		        this.ListSubPropertiesGroups.Sort();
		    }
		}
		public static GroupPropertiesTree Clone(ITreeConfigNode parent, GroupPropertiesTree from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupPropertiesTree vm = new GroupPropertiesTree();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(vm, t, isDeep));
		    vm.ListSubPropertiesGroups = new SortedObservableCollection<GroupPropertiesTree>();
		    foreach(var t in from.ListSubPropertiesGroups)
		        vm.ListSubPropertiesGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupPropertiesTree to, GroupPropertiesTree from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListProperties.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListProperties)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Property.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListProperties.Remove(t);
		        }
		        foreach(var tt in from.ListProperties)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListProperties.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Property();
		                vSharpStudio.vm.ViewModels.Property.Update(p, tt, isDeep);
		                to.ListProperties.Add(p);
		            }
		        }
		    }
		    if (isDeep)
		    {
		        foreach(var t in to.ListSubPropertiesGroups.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListSubPropertiesGroups)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.GroupPropertiesTree.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListSubPropertiesGroups.Remove(t);
		        }
		        foreach(var tt in from.ListSubPropertiesGroups)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListSubPropertiesGroups.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new GroupPropertiesTree();
		                vSharpStudio.vm.ViewModels.GroupPropertiesTree.Update(p, tt, isDeep);
		                to.ListSubPropertiesGroups.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupPropertiesTree Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupPropertiesTree.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupPropertiesTree from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupPropertiesTree.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_properties_tree' to 'GroupPropertiesTree'
		public static GroupPropertiesTree ConvertToVM(proto_group_properties_tree m, GroupPropertiesTree vm = null)
		{
		    if (vm == null)
		        vm = new GroupPropertiesTree();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToVM(t));
		    vm.ListSubPropertiesGroups = new SortedObservableCollection<GroupPropertiesTree>();
		    foreach(var t in m.ListSubPropertiesGroups)
		        vm.ListSubPropertiesGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupPropertiesTree' to 'proto_group_properties_tree'
		public static proto_group_properties_tree ConvertToProto(GroupPropertiesTree vm)
		{
		    proto_group_properties_tree m = new proto_group_properties_tree();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListProperties)
		        m.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto(t));
		    foreach(var t in vm.ListSubPropertiesGroups)
		        m.ListSubPropertiesGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListProperties)
				t.Accept(visitor);
			foreach(var t in this.ListSubPropertiesGroups)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Property> ListProperties { get; set; }
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
		
		
		public SortedObservableCollection<GroupPropertiesTree> ListSubPropertiesGroups { get; set; }
		partial void OnListSubPropertiesGroupsChanging();
		partial void OnListSubPropertiesGroupsChanged();
		#endregion Properties
	}
	
	public partial class GroupCatalogs : ConfigObjectBase<GroupCatalogs, GroupCatalogs.GroupCatalogsValidator>, IComparable<GroupCatalogs>, IAccept
	{
	
		public partial class GroupCatalogsValidator : ValidatorBase<GroupCatalogs, GroupCatalogsValidator> { }
		#region CTOR
		public GroupCatalogs() : base(GroupCatalogsValidator.Validator)
		{
			this.ListCatalogs = new SortedObservableCollection<Catalog>();
			this.ListCatalogs.CollectionChanged += ListCatalogs_CollectionChanged;
			OnInit();
		}
		public GroupCatalogs(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupCatalogs.DefaultName, this, this.SubNodes);
	    }
		private void ListCatalogs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Catalog).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Catalog))
		    {
		        this.ListCatalogs.Sort();
		    }
		}
		public static GroupCatalogs Clone(ITreeConfigNode parent, GroupCatalogs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupCatalogs vm = new GroupCatalogs();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in from.ListCatalogs)
		        vm.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupCatalogs to, GroupCatalogs from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListCatalogs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListCatalogs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Catalog.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListCatalogs.Remove(t);
		        }
		        foreach(var tt in from.ListCatalogs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListCatalogs.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Catalog();
		                vSharpStudio.vm.ViewModels.Catalog.Update(p, tt, isDeep);
		                to.ListCatalogs.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupCatalogs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupCatalogs.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupCatalogs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupCatalogs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_catalogs' to 'GroupCatalogs'
		public static GroupCatalogs ConvertToVM(proto_group_catalogs m, GroupCatalogs vm = null)
		{
		    if (vm == null)
		        vm = new GroupCatalogs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in m.ListCatalogs)
		        vm.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupCatalogs' to 'proto_group_catalogs'
		public static proto_group_catalogs ConvertToProto(GroupCatalogs vm)
		{
		    proto_group_catalogs m = new proto_group_catalogs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListCatalogs)
		        m.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListCatalogs)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Catalog> ListCatalogs { get; set; }
		partial void OnListCatalogsChanging();
		partial void OnListCatalogsChanged();
		#endregion Properties
	}
	
	public partial class Document : ConfigObjectBase<Document, Document.DocumentValidator>, IComparable<Document>, IAccept
	{
	
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document() : base(DocumentValidator.Validator)
		{
			this.ListPropertiesTreeGroups = new SortedObservableCollection<GroupPropertiesTree>();
			this.ListPropertiesTreeGroups.CollectionChanged += ListPropertiesTreeGroups_CollectionChanged;
			OnInit();
		}
		public Document(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Document.DefaultName, this, this.SubNodes);
	    }
		private void ListPropertiesTreeGroups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as GroupPropertiesTree).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(GroupPropertiesTree))
		    {
		        this.ListPropertiesTreeGroups.Sort();
		    }
		}
		public static Document Clone(ITreeConfigNode parent, Document from, bool isDeep = true, bool isNewGuid = false)
		{
		    Document vm = new Document();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListPropertiesTreeGroups = new SortedObservableCollection<GroupPropertiesTree>();
		    foreach(var t in from.ListPropertiesTreeGroups)
		        vm.ListPropertiesTreeGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Document to, Document from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListPropertiesTreeGroups.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPropertiesTreeGroups)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.GroupPropertiesTree.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListPropertiesTreeGroups.Remove(t);
		        }
		        foreach(var tt in from.ListPropertiesTreeGroups)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListPropertiesTreeGroups.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new GroupPropertiesTree();
		                vSharpStudio.vm.ViewModels.GroupPropertiesTree.Update(p, tt, isDeep);
		                to.ListPropertiesTreeGroups.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Document Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Document.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Document from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Document.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_document' to 'Document'
		public static Document ConvertToVM(proto_document m, Document vm = null)
		{
		    if (vm == null)
		        vm = new Document();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListPropertiesTreeGroups = new SortedObservableCollection<GroupPropertiesTree>();
		    foreach(var t in m.ListPropertiesTreeGroups)
		        vm.ListPropertiesTreeGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Document' to 'proto_document'
		public static proto_document ConvertToProto(Document vm)
		{
		    proto_document m = new proto_document();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListPropertiesTreeGroups)
		        m.ListPropertiesTreeGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPropertiesTreeGroups)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<GroupPropertiesTree> ListPropertiesTreeGroups { get; set; }
		partial void OnListPropertiesTreeGroupsChanging();
		partial void OnListPropertiesTreeGroupsChanged();
		#endregion Properties
	}
	
	public partial class GroupDocuments : ConfigObjectBase<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IAccept
	{
	
		public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { }
		#region CTOR
		public GroupDocuments() : base(GroupDocumentsValidator.Validator)
		{
			this.ListSharedProperties = new SortedObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListDocuments = new SortedObservableCollection<Document>();
			this.ListDocuments.CollectionChanged += ListDocuments_CollectionChanged;
			OnInit();
		}
		public GroupDocuments(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupDocuments.DefaultName, this, this.SubNodes);
	    }
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		private void ListDocuments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Document).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Property))
		    {
		        this.ListSharedProperties.Sort();
		    }
		    if (type == typeof(Document))
		    {
		        this.ListDocuments.Sort();
		    }
		}
		public static GroupDocuments Clone(ITreeConfigNode parent, GroupDocuments from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupDocuments vm = new GroupDocuments();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListSharedProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListSharedProperties)
		        vm.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(vm, t, isDeep));
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in from.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupDocuments to, GroupDocuments from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListSharedProperties.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListSharedProperties)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Property.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListSharedProperties.Remove(t);
		        }
		        foreach(var tt in from.ListSharedProperties)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListSharedProperties.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Property();
		                vSharpStudio.vm.ViewModels.Property.Update(p, tt, isDeep);
		                to.ListSharedProperties.Add(p);
		            }
		        }
		    }
		    if (isDeep)
		    {
		        foreach(var t in to.ListDocuments.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListDocuments)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Document.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListDocuments.Remove(t);
		        }
		        foreach(var tt in from.ListDocuments)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListDocuments.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Document();
		                vSharpStudio.vm.ViewModels.Document.Update(p, tt, isDeep);
		                to.ListDocuments.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupDocuments Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupDocuments.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupDocuments from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupDocuments.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_documents' to 'GroupDocuments'
		public static GroupDocuments ConvertToVM(proto_group_documents m, GroupDocuments vm = null)
		{
		    if (vm == null)
		        vm = new GroupDocuments();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListSharedProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListSharedProperties)
		        vm.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToVM(t));
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in m.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupDocuments' to 'proto_group_documents'
		public static proto_group_documents ConvertToProto(GroupDocuments vm)
		{
		    proto_group_documents m = new proto_group_documents();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListSharedProperties)
		        m.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto(t));
		    foreach(var t in vm.ListDocuments)
		        m.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListSharedProperties)
				t.Accept(visitor);
			foreach(var t in this.ListDocuments)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Property> ListSharedProperties { get; set; }
		partial void OnListSharedPropertiesChanging();
		partial void OnListSharedPropertiesChanged();
		
		
		public SortedObservableCollection<Document> ListDocuments { get; set; }
		partial void OnListDocumentsChanging();
		partial void OnListDocumentsChanged();
		#endregion Properties
	}
	
	public partial class Journal : ConfigObjectBase<Journal, Journal.JournalValidator>, IComparable<Journal>, IAccept
	{
	
		public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { }
		#region CTOR
		public Journal() : base(JournalValidator.Validator)
		{
			this.ListDocuments = new SortedObservableCollection<Document>();
			this.ListDocuments.CollectionChanged += ListDocuments_CollectionChanged;
			OnInit();
		}
		public Journal(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Journal.DefaultName, this, this.SubNodes);
	    }
		private void ListDocuments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Document).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Document))
		    {
		        this.ListDocuments.Sort();
		    }
		}
		public static Journal Clone(ITreeConfigNode parent, Journal from, bool isDeep = true, bool isNewGuid = false)
		{
		    Journal vm = new Journal();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in from.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Journal to, Journal from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListDocuments.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListDocuments)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Document.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListDocuments.Remove(t);
		        }
		        foreach(var tt in from.ListDocuments)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListDocuments.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Document();
		                vSharpStudio.vm.ViewModels.Document.Update(p, tt, isDeep);
		                to.ListDocuments.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Journal Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Journal.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Journal from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Journal.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_journal' to 'Journal'
		public static Journal ConvertToVM(proto_journal m, Journal vm = null)
		{
		    if (vm == null)
		        vm = new Journal();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in m.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Journal' to 'proto_journal'
		public static proto_journal ConvertToProto(Journal vm)
		{
		    proto_journal m = new proto_journal();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListDocuments)
		        m.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListDocuments)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Document> ListDocuments { get; set; }
		partial void OnListDocumentsChanging();
		partial void OnListDocumentsChanged();
		#endregion Properties
	}
	
	public partial class GroupJournals : ConfigObjectBase<GroupJournals, GroupJournals.GroupJournalsValidator>, IComparable<GroupJournals>, IAccept
	{
	
		public partial class GroupJournalsValidator : ValidatorBase<GroupJournals, GroupJournalsValidator> { }
		#region CTOR
		public GroupJournals() : base(GroupJournalsValidator.Validator)
		{
			this.ListSharedProperties = new SortedObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListJournals = new SortedObservableCollection<Journal>();
			this.ListJournals.CollectionChanged += ListJournals_CollectionChanged;
			OnInit();
		}
		public GroupJournals(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupJournals.DefaultName, this, this.SubNodes);
	    }
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		private void ListJournals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Journal).Parent = this;
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Property))
		    {
		        this.ListSharedProperties.Sort();
		    }
		    if (type == typeof(Journal))
		    {
		        this.ListJournals.Sort();
		    }
		}
		public static GroupJournals Clone(ITreeConfigNode parent, GroupJournals from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupJournals vm = new GroupJournals();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.ListSharedProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListSharedProperties)
		        vm.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(vm, t, isDeep));
		    vm.ListJournals = new SortedObservableCollection<Journal>();
		    foreach(var t in from.ListJournals)
		        vm.ListJournals.Add(vSharpStudio.vm.ViewModels.Journal.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupJournals to, GroupJournals from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListSharedProperties.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListSharedProperties)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Property.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListSharedProperties.Remove(t);
		        }
		        foreach(var tt in from.ListSharedProperties)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListSharedProperties.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Property();
		                vSharpStudio.vm.ViewModels.Property.Update(p, tt, isDeep);
		                to.ListSharedProperties.Add(p);
		            }
		        }
		    }
		    if (isDeep)
		    {
		        foreach(var t in to.ListJournals.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListJournals)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Journal.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListJournals.Remove(t);
		        }
		        foreach(var tt in from.ListJournals)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListJournals.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Journal();
		                vSharpStudio.vm.ViewModels.Journal.Update(p, tt, isDeep);
		                to.ListJournals.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupJournals Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupJournals.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupJournals from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupJournals.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_journals' to 'GroupJournals'
		public static GroupJournals ConvertToVM(proto_group_journals m, GroupJournals vm = null)
		{
		    if (vm == null)
		        vm = new GroupJournals();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.ListSharedProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListSharedProperties)
		        vm.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToVM(t));
		    vm.ListJournals = new SortedObservableCollection<Journal>();
		    foreach(var t in m.ListJournals)
		        vm.ListJournals.Add(vSharpStudio.vm.ViewModels.Journal.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupJournals' to 'proto_group_journals'
		public static proto_group_journals ConvertToProto(GroupJournals vm)
		{
		    proto_group_journals m = new proto_group_journals();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListSharedProperties)
		        m.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto(t));
		    foreach(var t in vm.ListJournals)
		        m.ListJournals.Add(vSharpStudio.vm.ViewModels.Journal.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListSharedProperties)
				t.Accept(visitor);
			foreach(var t in this.ListJournals)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public SortedObservableCollection<Property> ListSharedProperties { get; set; }
		partial void OnListSharedPropertiesChanging();
		partial void OnListSharedPropertiesChanged();
		
		
		public SortedObservableCollection<Journal> ListJournals { get; set; }
		partial void OnListJournalsChanging();
		partial void OnListJournalsChanged();
		#endregion Properties
	}
	
	public interface IVisitorConfig
	{
	    CancellationToken Token { get; }
		void Visit(Config p);
		void VisitEnd(Config p);
		void Visit(Property p);
		void VisitEnd(Property p);
		void Visit(DataType p);
		void VisitEnd(DataType p);
		void Visit(Constant p);
		void VisitEnd(Constant p);
		void Visit(GroupConstants p);
		void VisitEnd(GroupConstants p);
		void Visit(EnumerationPair p);
		void VisitEnd(EnumerationPair p);
		void Visit(Enumeration p);
		void VisitEnd(Enumeration p);
		void Visit(GroupEnumerations p);
		void VisitEnd(GroupEnumerations p);
		void Visit(Catalog p);
		void VisitEnd(Catalog p);
		void Visit(GroupProperties p);
		void VisitEnd(GroupProperties p);
		void Visit(GroupPropertiesTree p);
		void VisitEnd(GroupPropertiesTree p);
		void Visit(GroupCatalogs p);
		void VisitEnd(GroupCatalogs p);
		void Visit(Document p);
		void VisitEnd(Document p);
		void Visit(GroupDocuments p);
		void VisitEnd(GroupDocuments p);
		void Visit(Journal p);
		void VisitEnd(Journal p);
		void Visit(GroupJournals p);
		void VisitEnd(GroupJournals p);
	}
	
	public interface IVisitorProto
	{
		void Visit(proto_config p);
		void Visit(proto_property p);
		void Visit(proto_data_type p);
		void Visit(proto_constant p);
		void Visit(proto_group_constants p);
		void Visit(proto_enumeration_pair p);
		void Visit(proto_enumeration p);
		void Visit(proto_group_enumerations p);
		void Visit(proto_catalog p);
		void Visit(proto_group_properties p);
		void Visit(proto_group_properties_tree p);
		void Visit(proto_group_catalogs p);
		void Visit(proto_document p);
		void Visit(proto_group_documents p);
		void Visit(proto_journal p);
		void Visit(proto_group_journals p);
	}
}