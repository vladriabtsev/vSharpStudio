// Auto generated on UTC 03/31/2019 20:05:04
using System;
using System.Linq;
using ViewModelBase;
using FluentValidation;
using Proto.Config;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;

namespace vSharpStudio.vm.ViewModels
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017
	
	public partial class Config : IAccept
	{
	
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config() 
	        : base(ConfigValidator.Validator)
		{
			this.Constants = new Constants(this);
			this.Enumerators = new Enumerations(this);
			this.Catalogs = new Catalogs(this);
			OnInit();
		}
		public Config(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Config Clone(ITreeNode parent, Config from, bool isDeep = true)
		{
		    Config vm = new Config();
		    vm.Guid = from.Guid;
		    vm.Version = from.Version;
		    vm.Name = from.Name;
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
		        vm.Constants = Constants.Clone(vm, from.Constants, isDeep);
		    if (isDeep)
		        vm.Enumerators = Enumerations.Clone(vm, from.Enumerators, isDeep);
		    if (isDeep)
		        vm.Catalogs = Catalogs.Clone(vm, from.Catalogs, isDeep);
		    return vm;
		}
		public static void Update(Config to, Config from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Version = from.Version;
		    to.Name = from.Name;
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
		        Constants.Update(to.Constants, from.Constants, isDeep);
		    if (isDeep)
		        Enumerations.Update(to.Enumerators, from.Enumerators, isDeep);
		    if (isDeep)
		        Catalogs.Update(to.Catalogs, from.Catalogs, isDeep);
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
		    vm.Constants = Constants.ConvertToVM(m.Constants);
		    vm.Enumerators = Enumerations.ConvertToVM(m.Enumerators);
		    vm.Catalogs = Catalogs.ConvertToVM(m.Catalogs);
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
		    m.Constants = Constants.ConvertToProto(vm.Constants);
		    m.Enumerators = Enumerations.ConvertToProto(vm.Enumerators);
		    m.Catalogs = Catalogs.ConvertToProto(vm.Catalogs);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Constants.Accept(visitor);
			this.Enumerators.Accept(visitor);
			this.Catalogs.Accept(visitor);
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
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
		
		public Constants Constants
		{ 
			set
			{
				if (_Constants != value)
				{
					OnConstantsChanging();
		            _Constants = value;
					OnConstantsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Constants; }
		}
		private Constants _Constants;
		partial void OnConstantsChanging();
		partial void OnConstantsChanged();
		
		public Enumerations Enumerators
		{ 
			set
			{
				if (_Enumerators != value)
				{
					OnEnumeratorsChanging();
		            _Enumerators = value;
					OnEnumeratorsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Enumerators; }
		}
		private Enumerations _Enumerators;
		partial void OnEnumeratorsChanging();
		partial void OnEnumeratorsChanged();
		
		public Catalogs Catalogs
		{ 
			set
			{
				if (_Catalogs != value)
				{
					OnCatalogsChanging();
		            _Catalogs = value;
					OnCatalogsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Catalogs; }
		}
		private Catalogs _Catalogs;
		partial void OnCatalogsChanging();
		partial void OnCatalogsChanged();
		#endregion Properties
	}
	
	public partial class Property : IAccept
	{
	
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { }
		#region CTOR
		public Property() 
	        : base(PropertyValidator.Validator)
		{
			this.DataType = new DataType(this);
			OnInit();
		}
		public Property(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Property Clone(ITreeNode parent, Property from, bool isDeep = true)
		{
		    Property vm = new Property();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    if (isDeep)
		        vm.DataType = DataType.Clone(vm, from.DataType, isDeep);
		    return vm;
		}
		public static void Update(Property to, Property from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    if (isDeep)
		        DataType.Update(to.DataType, from.DataType, isDeep);
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
		    vm.DataType = DataType.ConvertToVM(m.DataType);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Property' to 'proto_property'
		public static proto_property ConvertToProto(Property vm)
		{
		    proto_property m = new proto_property();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.DataType = DataType.ConvertToProto(vm.DataType);
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
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
		#endregion Properties
	}
	
	public partial class DataType : IAccept
	{
	
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { }
		#region CTOR
		public DataType() 
	        : base(DataTypeValidator.Validator)
		{
			OnInit();
		}
		public DataType(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DataType Clone(ITreeNode parent, DataType from, bool isDeep = true)
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
	
	public partial class Properties : IAccept
	{
	
		public partial class PropertiesValidator : ValidatorBase<Properties, PropertiesValidator> { }
		#region CTOR
		public Properties() 
	        : base(PropertiesValidator.Validator)
		{
			this.ListProperties = new ObservableCollection<Property>();
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
			OnInit();
		}
		public Properties(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		private void ListProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                #region Default Name
			    	string bname = "Property";
				    int i = 0;
	    			foreach (var tt in this.ListProperties)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as Property).Name))
	                        continue;
	    				i++;
		    			(t as Property).Name = bname + i;
			    	}
	                #endregion Default Name
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Properties Clone(ITreeNode parent, Properties from, bool isDeep = true)
		{
		    Properties vm = new Properties();
		    vm.Name = from.Name;
		    vm.ListProperties = new ObservableCollection<Property>();
		    foreach(var t in from.ListProperties)
		        vm.ListProperties.Add(Property.Clone(vm, t, isDeep));
		    return vm;
		}
		public static void Update(Properties to, Properties from, bool isDeep = true)
		{
		    to.Name = from.Name;
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
		                    Property.Update(t, tt, isDeep);
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
		                Property.Update(p, tt, isDeep);
		                to.ListProperties.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Properties Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Properties.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Properties from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Properties.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_properties' to 'Properties'
		public static Properties ConvertToVM(proto_properties m, Properties vm = null)
		{
		    if (vm == null)
		        vm = new Properties();
		    vm.Name = m.Name;
		    vm.ListProperties = new ObservableCollection<Property>();
		    foreach(var t in m.ListProperties)
		        vm.ListProperties.Add(Property.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Properties' to 'proto_properties'
		public static proto_properties ConvertToProto(Properties vm)
		{
		    proto_properties m = new proto_properties();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListProperties)
		        m.ListProperties.Add(Property.ConvertToProto(t));
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Property> ListProperties { get; set; }
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
		#endregion Properties
	}
	
	public partial class Constant : IAccept
	{
	
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { }
		#region CTOR
		public Constant() 
	        : base(ConstantValidator.Validator)
		{
			this.ConstantType = new Property(this);
			OnInit();
		}
		public Constant(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Constant Clone(ITreeNode parent, Constant from, bool isDeep = true)
		{
		    Constant vm = new Constant();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    if (isDeep)
		        vm.ConstantType = Property.Clone(vm, from.ConstantType, isDeep);
		    return vm;
		}
		public static void Update(Constant to, Constant from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    if (isDeep)
		        Property.Update(to.ConstantType, from.ConstantType, isDeep);
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
		    vm.ConstantType = Property.ConvertToVM(m.ConstantType);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Constant' to 'proto_constant'
		public static proto_constant ConvertToProto(Constant vm)
		{
		    proto_constant m = new proto_constant();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.ConstantType = Property.ConvertToProto(vm.ConstantType);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.ConstantType.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public Property ConstantType
		{ 
			set
			{
				if (_ConstantType != value)
				{
					OnConstantTypeChanging();
		            _ConstantType = value;
					OnConstantTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConstantType; }
		}
		private Property _ConstantType;
		partial void OnConstantTypeChanging();
		partial void OnConstantTypeChanged();
		#endregion Properties
	}
	
	public partial class Constants : IAccept
	{
	
		public partial class ConstantsValidator : ValidatorBase<Constants, ConstantsValidator> { }
		#region CTOR
		public Constants() 
	        : base(ConstantsValidator.Validator)
		{
			this.ListConstants = new ObservableCollection<Constant>();
			this.ListConstants.CollectionChanged += ListConstants_CollectionChanged;
			OnInit();
		}
		public Constants(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		private void ListConstants_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Constant).Parent = this;
	                #region Default Name
			    	string bname = "Constant";
				    int i = 0;
	    			foreach (var tt in this.ListConstants)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as Constant).Name))
	                        continue;
	    				i++;
		    			(t as Constant).Name = bname + i;
			    	}
	                #endregion Default Name
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Constants Clone(ITreeNode parent, Constants from, bool isDeep = true)
		{
		    Constants vm = new Constants();
		    vm.Name = from.Name;
		    vm.ListConstants = new ObservableCollection<Constant>();
		    foreach(var t in from.ListConstants)
		        vm.ListConstants.Add(Constant.Clone(vm, t, isDeep));
		    return vm;
		}
		public static void Update(Constants to, Constants from, bool isDeep = true)
		{
		    to.Name = from.Name;
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
		                    Constant.Update(t, tt, isDeep);
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
		                Constant.Update(p, tt, isDeep);
		                to.ListConstants.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Constants Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Constants.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Constants from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Constants.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_constants' to 'Constants'
		public static Constants ConvertToVM(proto_constants m, Constants vm = null)
		{
		    if (vm == null)
		        vm = new Constants();
		    vm.Name = m.Name;
		    vm.ListConstants = new ObservableCollection<Constant>();
		    foreach(var t in m.ListConstants)
		        vm.ListConstants.Add(Constant.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Constants' to 'proto_constants'
		public static proto_constants ConvertToProto(Constants vm)
		{
		    proto_constants m = new proto_constants();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListConstants)
		        m.ListConstants.Add(Constant.ConvertToProto(t));
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Constant> ListConstants { get; set; }
		partial void OnListConstantsChanging();
		partial void OnListConstantsChanged();
		#endregion Properties
	}
	
	public partial class EnumerationPair : IAccept
	{
	
		public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { }
		#region CTOR
		public EnumerationPair() 
	        : base(EnumerationPairValidator.Validator)
		{
			OnInit();
		}
		public EnumerationPair(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static EnumerationPair Clone(ITreeNode parent, EnumerationPair from, bool isDeep = true)
		{
		    EnumerationPair vm = new EnumerationPair();
		    vm.Name = from.Name;
		    vm.Value = from.Value;
		    return vm;
		}
		public static void Update(EnumerationPair to, EnumerationPair from, bool isDeep = true)
		{
		    to.Name = from.Name;
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
		    vm.Name = m.Name;
		    vm.Value = m.Value;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
		public static proto_enumeration_pair ConvertToProto(EnumerationPair vm)
		{
		    proto_enumeration_pair m = new proto_enumeration_pair();
		    m.Name = vm.Name;
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
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
	
	public partial class Enumeration : IAccept
	{
	
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { }
		#region CTOR
		public Enumeration() 
	        : base(EnumerationValidator.Validator)
		{
			this.ListValues = new ObservableCollection<EnumerationPair>();
			this.ListValues.CollectionChanged += ListValues_CollectionChanged;
			OnInit();
		}
		public Enumeration(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		private void ListValues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as EnumerationPair).Parent = this;
	                #region Default Name
			    	string bname = "EnumerationPair";
				    int i = 0;
	    			foreach (var tt in this.ListValues)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as EnumerationPair).Name))
	                        continue;
	    				i++;
		    			(t as EnumerationPair).Name = bname + i;
			    	}
	                #endregion Default Name
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Enumeration Clone(ITreeNode parent, Enumeration from, bool isDeep = true)
		{
		    Enumeration vm = new Enumeration();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.DataTypeEnum = from.DataTypeEnum;
		    vm.ListValues = new ObservableCollection<EnumerationPair>();
		    foreach(var t in from.ListValues)
		        vm.ListValues.Add(EnumerationPair.Clone(vm, t, isDeep));
		    return vm;
		}
		public static void Update(Enumeration to, Enumeration from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.DataTypeEnum = from.DataTypeEnum;
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
		                    EnumerationPair.Update(t, tt, isDeep);
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
		                EnumerationPair.Update(p, tt, isDeep);
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
		    vm.DataTypeEnum = m.DataTypeEnum;
		    vm.ListValues = new ObservableCollection<EnumerationPair>();
		    foreach(var t in m.ListValues)
		        vm.ListValues.Add(EnumerationPair.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Enumeration' to 'proto_enumeration'
		public static proto_enumeration ConvertToProto(Enumeration vm)
		{
		    proto_enumeration m = new proto_enumeration();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.DataTypeEnum = vm.DataTypeEnum;
		    foreach(var t in vm.ListValues)
		        m.ListValues.Add(EnumerationPair.ConvertToProto(t));
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
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
		
		public ObservableCollection<EnumerationPair> ListValues { get; set; }
		partial void OnListValuesChanging();
		partial void OnListValuesChanged();
		#endregion Properties
	}
	
	public partial class Enumerations : IAccept
	{
	
		public partial class EnumerationsValidator : ValidatorBase<Enumerations, EnumerationsValidator> { }
		#region CTOR
		public Enumerations() 
	        : base(EnumerationsValidator.Validator)
		{
			this.ListEnumerations = new ObservableCollection<Enumeration>();
			this.ListEnumerations.CollectionChanged += ListEnumerations_CollectionChanged;
			OnInit();
		}
		public Enumerations(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		private void ListEnumerations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Enumeration).Parent = this;
	                #region Default Name
			    	string bname = "Enumeration";
				    int i = 0;
	    			foreach (var tt in this.ListEnumerations)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as Enumeration).Name))
	                        continue;
	    				i++;
		    			(t as Enumeration).Name = bname + i;
			    	}
	                #endregion Default Name
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Enumerations Clone(ITreeNode parent, Enumerations from, bool isDeep = true)
		{
		    Enumerations vm = new Enumerations();
		    vm.Name = from.Name;
		    vm.ListEnumerations = new ObservableCollection<Enumeration>();
		    foreach(var t in from.ListEnumerations)
		        vm.ListEnumerations.Add(Enumeration.Clone(vm, t, isDeep));
		    return vm;
		}
		public static void Update(Enumerations to, Enumerations from, bool isDeep = true)
		{
		    to.Name = from.Name;
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
		                    Enumeration.Update(t, tt, isDeep);
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
		                Enumeration.Update(p, tt, isDeep);
		                to.ListEnumerations.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Enumerations Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Enumerations.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Enumerations from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Enumerations.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_enumerations' to 'Enumerations'
		public static Enumerations ConvertToVM(proto_enumerations m, Enumerations vm = null)
		{
		    if (vm == null)
		        vm = new Enumerations();
		    vm.Name = m.Name;
		    vm.ListEnumerations = new ObservableCollection<Enumeration>();
		    foreach(var t in m.ListEnumerations)
		        vm.ListEnumerations.Add(Enumeration.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Enumerations' to 'proto_enumerations'
		public static proto_enumerations ConvertToProto(Enumerations vm)
		{
		    proto_enumerations m = new proto_enumerations();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListEnumerations)
		        m.ListEnumerations.Add(Enumeration.ConvertToProto(t));
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Enumeration> ListEnumerations { get; set; }
		partial void OnListEnumerationsChanging();
		partial void OnListEnumerationsChanged();
		#endregion Properties
	}
	
	public partial class Catalog : IAccept
	{
	
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog() 
	        : base(CatalogValidator.Validator)
		{
			this.Properties = new Properties(this);
			OnInit();
		}
		public Catalog(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Catalog Clone(ITreeNode parent, Catalog from, bool isDeep = true)
		{
		    Catalog vm = new Catalog();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered.HasValue ? from.IsPrimaryKeyClustered.Value : (bool?)null;
		    vm.IsMemoryOptimized = from.IsMemoryOptimized.HasValue ? from.IsMemoryOptimized.Value : (bool?)null;
		    vm.IsSequenceHiLo = from.IsSequenceHiLo.HasValue ? from.IsSequenceHiLo.Value : (bool?)null;
		    vm.HiLoSequenceName = from.HiLoSequenceName;
		    vm.HiLoSchema = from.HiLoSchema;
		    if (isDeep)
		        vm.Properties = Properties.Clone(vm, from.Properties, isDeep);
		    return vm;
		}
		public static void Update(Catalog to, Catalog from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered.HasValue ? from.IsPrimaryKeyClustered.Value : (bool?)null;
		    to.IsMemoryOptimized = from.IsMemoryOptimized.HasValue ? from.IsMemoryOptimized.Value : (bool?)null;
		    to.IsSequenceHiLo = from.IsSequenceHiLo.HasValue ? from.IsSequenceHiLo.Value : (bool?)null;
		    to.HiLoSequenceName = from.HiLoSequenceName;
		    to.HiLoSchema = from.HiLoSchema;
		    if (isDeep)
		        Properties.Update(to.Properties, from.Properties, isDeep);
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
		    vm.IsPrimaryKeyClustered = m.IsPrimaryKeyClustered.HasValue ? m.IsPrimaryKeyClustered.Value : (bool?)null;
		    vm.IsMemoryOptimized = m.IsMemoryOptimized.HasValue ? m.IsMemoryOptimized.Value : (bool?)null;
		    vm.IsSequenceHiLo = m.IsSequenceHiLo.HasValue ? m.IsSequenceHiLo.Value : (bool?)null;
		    vm.HiLoSequenceName = m.HiLoSequenceName;
		    vm.HiLoSchema = m.HiLoSchema;
		    vm.Properties = Properties.ConvertToVM(m.Properties);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Catalog' to 'proto_catalog'
		public static proto_catalog ConvertToProto(Catalog vm)
		{
		    proto_catalog m = new proto_catalog();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.IsPrimaryKeyClustered.Value = vm.IsPrimaryKeyClustered.Value;
		    m.IsPrimaryKeyClustered.HasValue = vm.IsPrimaryKeyClustered.HasValue;
		    m.IsMemoryOptimized.Value = vm.IsMemoryOptimized.Value;
		    m.IsMemoryOptimized.HasValue = vm.IsMemoryOptimized.HasValue;
		    m.IsSequenceHiLo.Value = vm.IsSequenceHiLo.Value;
		    m.IsSequenceHiLo.HasValue = vm.IsSequenceHiLo.HasValue;
		    m.HiLoSequenceName = vm.HiLoSequenceName;
		    m.HiLoSchema = vm.HiLoSchema;
		    m.Properties = Properties.ConvertToProto(vm.Properties);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Properties.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
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
		
		public Properties Properties
		{ 
			set
			{
				if (_Properties != value)
				{
					OnPropertiesChanging();
		            _Properties = value;
					OnPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Properties; }
		}
		private Properties _Properties;
		partial void OnPropertiesChanging();
		partial void OnPropertiesChanged();
		#endregion Properties
	}
	
	public partial class Catalogs : IAccept
	{
	
		public partial class CatalogsValidator : ValidatorBase<Catalogs, CatalogsValidator> { }
		#region CTOR
		public Catalogs() 
	        : base(CatalogsValidator.Validator)
		{
			this.ListSharedProperties = new ObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListCatalogs = new ObservableCollection<Catalog>();
			this.ListCatalogs.CollectionChanged += ListCatalogs_CollectionChanged;
			OnInit();
		}
		public Catalogs(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                #region Default Name
			    	string bname = "Property";
				    int i = 0;
	    			foreach (var tt in this.ListSharedProperties)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as Property).Name))
	                        continue;
	    				i++;
		    			(t as Property).Name = bname + i;
			    	}
	                #endregion Default Name
	                break;
	            default:
	                throw new Exception();
			}
		}
		private void ListCatalogs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Catalog).Parent = this;
	                #region Default Name
			    	string bname = "Catalog";
				    int i = 0;
	    			foreach (var tt in this.ListCatalogs)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as Catalog).Name))
	                        continue;
	    				i++;
		    			(t as Catalog).Name = bname + i;
			    	}
	                #endregion Default Name
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Catalogs Clone(ITreeNode parent, Catalogs from, bool isDeep = true)
		{
		    Catalogs vm = new Catalogs();
		    vm.Name = from.Name;
		    vm.ListSharedProperties = new ObservableCollection<Property>();
		    foreach(var t in from.ListSharedProperties)
		        vm.ListSharedProperties.Add(Property.Clone(vm, t, isDeep));
		    vm.ListCatalogs = new ObservableCollection<Catalog>();
		    foreach(var t in from.ListCatalogs)
		        vm.ListCatalogs.Add(Catalog.Clone(vm, t, isDeep));
		    return vm;
		}
		public static void Update(Catalogs to, Catalogs from, bool isDeep = true)
		{
		    to.Name = from.Name;
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
		                    Property.Update(t, tt, isDeep);
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
		                Property.Update(p, tt, isDeep);
		                to.ListSharedProperties.Add(p);
		            }
		        }
		    }
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
		                    Catalog.Update(t, tt, isDeep);
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
		                Catalog.Update(p, tt, isDeep);
		                to.ListCatalogs.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Catalogs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Catalogs.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Catalogs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Catalogs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_catalogs' to 'Catalogs'
		public static Catalogs ConvertToVM(proto_catalogs m, Catalogs vm = null)
		{
		    if (vm == null)
		        vm = new Catalogs();
		    vm.Name = m.Name;
		    vm.ListSharedProperties = new ObservableCollection<Property>();
		    foreach(var t in m.ListSharedProperties)
		        vm.ListSharedProperties.Add(Property.ConvertToVM(t));
		    vm.ListCatalogs = new ObservableCollection<Catalog>();
		    foreach(var t in m.ListCatalogs)
		        vm.ListCatalogs.Add(Catalog.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Catalogs' to 'proto_catalogs'
		public static proto_catalogs ConvertToProto(Catalogs vm)
		{
		    proto_catalogs m = new proto_catalogs();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListSharedProperties)
		        m.ListSharedProperties.Add(Property.ConvertToProto(t));
		    foreach(var t in vm.ListCatalogs)
		        m.ListCatalogs.Add(Catalog.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListSharedProperties)
				t.Accept(visitor);
			foreach(var t in this.ListCatalogs)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Property> ListSharedProperties { get; set; }
		partial void OnListSharedPropertiesChanging();
		partial void OnListSharedPropertiesChanged();
		
		public ObservableCollection<Catalog> ListCatalogs { get; set; }
		partial void OnListCatalogsChanging();
		partial void OnListCatalogsChanged();
		#endregion Properties
	}
	
	public partial class Document : IAccept
	{
	
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document() 
	        : base(DocumentValidator.Validator)
		{
			this.Properties = new Properties(this);
			OnInit();
		}
		public Document(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Document Clone(ITreeNode parent, Document from, bool isDeep = true)
		{
		    Document vm = new Document();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    if (isDeep)
		        vm.Properties = Properties.Clone(vm, from.Properties, isDeep);
		    return vm;
		}
		public static void Update(Document to, Document from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    if (isDeep)
		        Properties.Update(to.Properties, from.Properties, isDeep);
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
		    vm.Properties = Properties.ConvertToVM(m.Properties);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Document' to 'proto_document'
		public static proto_document ConvertToProto(Document vm)
		{
		    proto_document m = new proto_document();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.Properties = Properties.ConvertToProto(vm.Properties);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Properties.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public Properties Properties
		{ 
			set
			{
				if (_Properties != value)
				{
					OnPropertiesChanging();
		            _Properties = value;
					OnPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Properties; }
		}
		private Properties _Properties;
		partial void OnPropertiesChanging();
		partial void OnPropertiesChanged();
		#endregion Properties
	}
	
	public partial class Documents : IAccept
	{
	
		public partial class DocumentsValidator : ValidatorBase<Documents, DocumentsValidator> { }
		#region CTOR
		public Documents() 
	        : base(DocumentsValidator.Validator)
		{
			this.ListSharedProperties = new ObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListDocuments = new ObservableCollection<Document>();
			this.ListDocuments.CollectionChanged += ListDocuments_CollectionChanged;
			OnInit();
		}
		public Documents(ITreeNode parent) : this()
	    {
	        this.Parent = parent;
	    }
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
	                break;
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Property).Parent = this;
	                #region Default Name
			    	string bname = "Property";
				    int i = 0;
	    			foreach (var tt in this.ListSharedProperties)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as Property).Name))
	                        continue;
	    				i++;
		    			(t as Property).Name = bname + i;
			    	}
	                #endregion Default Name
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
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		    		foreach (var t in e.NewItems)
		    			(t as Document).Parent = this;
	                #region Default Name
			    	string bname = "Document";
				    int i = 0;
	    			foreach (var tt in this.ListDocuments)
		    		{
	                    bool isfound = false;
	                    foreach (var t in e.NewItems)
	                    {
	                        if (t == tt)
	                        {
	                            isfound = true;
	                            break;
	                        }
	                    }
	                    if (isfound)
	                        continue;
			    		if (tt.Name.StartsWith(bname))
				    	{
						    string s = tt.Name.Remove(0, bname.Length);
					    	int ii;
	    					if (int.TryParse(s, out ii))
		    				{
			    				if (ii > i) i = ii;
				    		}
					    }
	    			}
		    		foreach (var t in e.NewItems)
			    	{
	                    if (!string.IsNullOrWhiteSpace((t as Document).Name))
	                        continue;
	    				i++;
		    			(t as Document).Name = bname + i;
			    	}
	                #endregion Default Name
	                break;
	            default:
	                throw new Exception();
			}
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Documents Clone(ITreeNode parent, Documents from, bool isDeep = true)
		{
		    Documents vm = new Documents();
		    vm.Name = from.Name;
		    vm.ListSharedProperties = new ObservableCollection<Property>();
		    foreach(var t in from.ListSharedProperties)
		        vm.ListSharedProperties.Add(Property.Clone(vm, t, isDeep));
		    vm.ListDocuments = new ObservableCollection<Document>();
		    foreach(var t in from.ListDocuments)
		        vm.ListDocuments.Add(Document.Clone(vm, t, isDeep));
		    return vm;
		}
		public static void Update(Documents to, Documents from, bool isDeep = true)
		{
		    to.Name = from.Name;
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
		                    Property.Update(t, tt, isDeep);
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
		                Property.Update(p, tt, isDeep);
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
		                    Document.Update(t, tt, isDeep);
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
		                Document.Update(p, tt, isDeep);
		                to.ListDocuments.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override Documents Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Documents.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Documents from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Documents.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_documents' to 'Documents'
		public static Documents ConvertToVM(proto_documents m, Documents vm = null)
		{
		    if (vm == null)
		        vm = new Documents();
		    vm.Name = m.Name;
		    vm.ListSharedProperties = new ObservableCollection<Property>();
		    foreach(var t in m.ListSharedProperties)
		        vm.ListSharedProperties.Add(Property.ConvertToVM(t));
		    vm.ListDocuments = new ObservableCollection<Document>();
		    foreach(var t in m.ListDocuments)
		        vm.ListDocuments.Add(Document.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Documents' to 'proto_documents'
		public static proto_documents ConvertToProto(Documents vm)
		{
		    proto_documents m = new proto_documents();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListSharedProperties)
		        m.ListSharedProperties.Add(Property.ConvertToProto(t));
		    foreach(var t in vm.ListDocuments)
		        m.ListDocuments.Add(Document.ConvertToProto(t));
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
		
		public string Name
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Property> ListSharedProperties { get; set; }
		partial void OnListSharedPropertiesChanging();
		partial void OnListSharedPropertiesChanged();
		
		public ObservableCollection<Document> ListDocuments { get; set; }
		partial void OnListDocumentsChanging();
		partial void OnListDocumentsChanged();
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
		void Visit(Properties p);
		void VisitEnd(Properties p);
		void Visit(Constant p);
		void VisitEnd(Constant p);
		void Visit(Constants p);
		void VisitEnd(Constants p);
		void Visit(EnumerationPair p);
		void VisitEnd(EnumerationPair p);
		void Visit(Enumeration p);
		void VisitEnd(Enumeration p);
		void Visit(Enumerations p);
		void VisitEnd(Enumerations p);
		void Visit(Catalog p);
		void VisitEnd(Catalog p);
		void Visit(Catalogs p);
		void VisitEnd(Catalogs p);
		void Visit(Document p);
		void VisitEnd(Document p);
		void Visit(Documents p);
		void VisitEnd(Documents p);
	}
	
	public interface IVisitorProto
	{
		void Visit(proto_config p);
		void Visit(proto_property p);
		void Visit(proto_data_type p);
		void Visit(proto_properties p);
		void Visit(proto_constant p);
		void Visit(proto_constants p);
		void Visit(proto_enumeration_pair p);
		void Visit(proto_enumeration p);
		void Visit(proto_enumerations p);
		void Visit(proto_catalog p);
		void Visit(proto_catalogs p);
		void Visit(proto_document p);
		void Visit(proto_documents p);
	}
}