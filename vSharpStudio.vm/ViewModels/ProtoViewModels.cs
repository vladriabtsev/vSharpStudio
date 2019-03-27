// Auto generated on UTC 03/27/2019 15:40:07
using System;
using ViewModelBase;
using FluentValidation;
using Proto.Config;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace vSharpStudio.vm.ViewModels
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017
	
	public partial class Config
	{
	
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConfigValidator.Validator, validationCollection)
		{
			this.Constants = new Constants(validationCollection);
			this.Enumerators = new Enumerations(validationCollection);
			this.Catalogs = new Catalogs(validationCollection);
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Config Clone(Config from)
		{
			Config res = new Config();
			res.Guid = from.Guid;
			res.Version = from.Version;
			res.Name = from.Name;
			res.IsDbFromConnectionString = from.IsDbFromConnectionString;
			res.ConnectionStringName = from.ConnectionStringName;
			res.DbTypeEnum = from.DbTypeEnum;
			res.DbServer = from.DbServer;
			res.DbDatabaseName = from.DbDatabaseName;
			res.IsDbWindowsAuthentication = from.IsDbWindowsAuthentication;
			res.DbUser = from.DbUser;
			res.DbPassword = from.DbPassword;
			res.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
			res.DbSchema = from.DbSchema;
			res.PrimaryKeyName = from.PrimaryKeyName;
			res.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered;
			res.IsMemoryOptimized = from.IsMemoryOptimized;
			res.IsSequenceHiLo = from.IsSequenceHiLo;
			res.HiLoSequenceName = from.HiLoSequenceName;
			res.HiLoSchema = from.HiLoSchema;
			res.Constants = vSharpStudio.vm.ViewModels.Constants.Clone(from.Constants);
			res.Enumerators = vSharpStudio.vm.ViewModels.Enumerations.Clone(from.Enumerators);
			res.Catalogs = vSharpStudio.vm.ViewModels.Catalogs.Clone(from.Catalogs);
			return res;
		}
		public void UpdateFrom(Config from)
		{
			this.Guid = from.Guid;
			this.Version = from.Version;
			this.Name = from.Name;
			this.IsDbFromConnectionString = from.IsDbFromConnectionString;
			this.ConnectionStringName = from.ConnectionStringName;
			this.DbTypeEnum = from.DbTypeEnum;
			this.DbServer = from.DbServer;
			this.DbDatabaseName = from.DbDatabaseName;
			this.IsDbWindowsAuthentication = from.IsDbWindowsAuthentication;
			this.DbUser = from.DbUser;
			this.DbPassword = from.DbPassword;
			this.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
			this.DbSchema = from.DbSchema;
			this.PrimaryKeyName = from.PrimaryKeyName;
			this.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered;
			this.IsMemoryOptimized = from.IsMemoryOptimized;
			this.IsSequenceHiLo = from.IsSequenceHiLo;
			this.HiLoSequenceName = from.HiLoSequenceName;
			this.HiLoSchema = from.HiLoSchema;
			this.Constants = from.Constants;
			this.Enumerators = from.Enumerators;
			this.Catalogs = from.Catalogs;
		}
		#region IEditable
		public override Config Backup()
		{
			return vSharpStudio.vm.ViewModels.Config.Clone(this);
		}
		public override void Restore(Config from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			visitor.Visit(this.Constants);
			visitor.Visit(this.Enumerators);
			visitor.Visit(this.Catalogs);
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
		private string _Version;
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
		private string _Name;
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
		private string _ConnectionStringName;
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
		private string _DbServer;
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
		private string _DbDatabaseName;
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
		private string _DbUser;
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
		private string _DbPassword;
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
		private string _PathToProjectWithConnectionString;
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
		private string _DbSchema;
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
		private string _PrimaryKeyName;
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
		private string _HiLoSequenceName;
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
		private string _HiLoSchema;
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
	
	public partial class Property
	{
	
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { }
		#region CTOR
		public Property(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(PropertyValidator.Validator, validationCollection)
		{
			this.DataType = new DataType(validationCollection);
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Property Clone(Property from)
		{
			Property res = new Property();
			res.Guid = from.Guid;
			res.Name = from.Name;
			res.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(from.DataType);
			return res;
		}
		public void UpdateFrom(Property from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
			this.DataType = from.DataType;
		}
		#region IEditable
		public override Property Backup()
		{
			return vSharpStudio.vm.ViewModels.Property.Clone(this);
		}
		public override void Restore(Property from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			visitor.Visit(this.DataType);
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
		private string _Name;
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
	
	public partial class DataType
	{
	
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { }
		#region CTOR
		public DataType(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DataTypeValidator.Validator, validationCollection)
		{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DataType Clone(DataType from)
		{
			DataType res = new DataType();
			res.DataTypeEnum = from.DataTypeEnum;
			res.Length = from.Length;
			res.Accuracy = from.Accuracy;
			res.IsPositive = from.IsPositive;
			res.TypeGuid = from.TypeGuid;
			res.MinValueString = from.MinValueString;
			res.MaxValueString = from.MaxValueString;
			res.ObjectName = from.ObjectName;
			return res;
		}
		public void UpdateFrom(DataType from)
		{
			this.DataTypeEnum = from.DataTypeEnum;
			this.Length = from.Length;
			this.Accuracy = from.Accuracy;
			this.IsPositive = from.IsPositive;
			this.TypeGuid = from.TypeGuid;
			this.MinValueString = from.MinValueString;
			this.MaxValueString = from.MaxValueString;
			this.ObjectName = from.ObjectName;
		}
		#region IEditable
		public override DataType Backup()
		{
			return vSharpStudio.vm.ViewModels.DataType.Clone(this);
		}
		public override void Restore(DataType from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
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
		private string _TypeGuid;
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
		private string _MinValueString;
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
		private string _MaxValueString;
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
		private string _ObjectName;
		partial void OnObjectNameChanging();
		partial void OnObjectNameChanged();
		#endregion Properties
	}
	
	public partial class Properties
	{
	
		public partial class PropertiesValidator : ValidatorBase<Properties, PropertiesValidator> { }
		#region CTOR
		public Properties(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(PropertiesValidator.Validator, validationCollection)
		{
			this.ListProperties = new ObservableCollection<Property>();
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
			OnInit();
		}
		private void ListProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "Property";
				    int i = 0;
	    			foreach (var tt in this.ListProperties)
		    		{
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
		public static Properties Clone(Properties from)
		{
			Properties res = new Properties();
			res.Name = from.Name;
			res.ListProperties = new ObservableCollection<Property>();
			foreach (var t in from.ListProperties)
			{
				res.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(t));
			}
			return res;
		}
		public void UpdateFrom(Properties from)
		{
			this.Name = from.Name;
		}
		#region IEditable
		public override Properties Backup()
		{
			return vSharpStudio.vm.ViewModels.Properties.Clone(this);
		}
		public override void Restore(Properties from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			foreach(var t in this.ListProperties)
				visitor.Visit(t);
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
		private string _Name;
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Property> ListProperties { get; set; }
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
		#endregion Properties
	}
	
	public partial class Constant
	{
	
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { }
		#region CTOR
		public Constant(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConstantValidator.Validator, validationCollection)
		{
			this.ConstantType = new Property(validationCollection);
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Constant Clone(Constant from)
		{
			Constant res = new Constant();
			res.Guid = from.Guid;
			res.Name = from.Name;
			res.ConstantType = vSharpStudio.vm.ViewModels.Property.Clone(from.ConstantType);
			return res;
		}
		public void UpdateFrom(Constant from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
			this.ConstantType = from.ConstantType;
		}
		#region IEditable
		public override Constant Backup()
		{
			return vSharpStudio.vm.ViewModels.Constant.Clone(this);
		}
		public override void Restore(Constant from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			visitor.Visit(this.ConstantType);
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
		private string _Name;
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
	
	public partial class Constants
	{
	
		public partial class ConstantsValidator : ValidatorBase<Constants, ConstantsValidator> { }
		#region CTOR
		public Constants(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConstantsValidator.Validator, validationCollection)
		{
			this.ListConstants = new ObservableCollection<Constant>();
			this.ListConstants.CollectionChanged += ListConstants_CollectionChanged;
			OnInit();
		}
		private void ListConstants_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "Constant";
				    int i = 0;
	    			foreach (var tt in this.ListConstants)
		    		{
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
		public static Constants Clone(Constants from)
		{
			Constants res = new Constants();
			res.Name = from.Name;
			res.ListConstants = new ObservableCollection<Constant>();
			foreach (var t in from.ListConstants)
			{
				res.ListConstants.Add(vSharpStudio.vm.ViewModels.Constant.Clone(t));
			}
			return res;
		}
		public void UpdateFrom(Constants from)
		{
			this.Name = from.Name;
		}
		#region IEditable
		public override Constants Backup()
		{
			return vSharpStudio.vm.ViewModels.Constants.Clone(this);
		}
		public override void Restore(Constants from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			foreach(var t in this.ListConstants)
				visitor.Visit(t);
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
		private string _Name;
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Constant> ListConstants { get; set; }
		partial void OnListConstantsChanging();
		partial void OnListConstantsChanged();
		#endregion Properties
	}
	
	public partial class EnumerationPair
	{
	
		public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { }
		#region CTOR
		public EnumerationPair(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(EnumerationPairValidator.Validator, validationCollection)
		{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static EnumerationPair Clone(EnumerationPair from)
		{
			EnumerationPair res = new EnumerationPair();
			res.Name = from.Name;
			res.Value = from.Value;
			return res;
		}
		public void UpdateFrom(EnumerationPair from)
		{
			this.Name = from.Name;
			this.Value = from.Value;
		}
		#region IEditable
		public override EnumerationPair Backup()
		{
			return vSharpStudio.vm.ViewModels.EnumerationPair.Clone(this);
		}
		public override void Restore(EnumerationPair from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
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
		private string _Name;
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
		private string _Value;
		partial void OnValueChanging();
		partial void OnValueChanged();
		#endregion Properties
	}
	
	public partial class Enumeration
	{
	
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { }
		#region CTOR
		public Enumeration(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(EnumerationValidator.Validator, validationCollection)
		{
			this.ListValues = new ObservableCollection<EnumerationPair>();
			this.ListValues.CollectionChanged += ListValues_CollectionChanged;
			OnInit();
		}
		private void ListValues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "EnumerationPair";
				    int i = 0;
	    			foreach (var tt in this.ListValues)
		    		{
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
		public static Enumeration Clone(Enumeration from)
		{
			Enumeration res = new Enumeration();
			res.Guid = from.Guid;
			res.Name = from.Name;
			res.DataTypeEnum = from.DataTypeEnum;
			res.ListValues = new ObservableCollection<EnumerationPair>();
			foreach (var t in from.ListValues)
			{
				res.ListValues.Add(vSharpStudio.vm.ViewModels.EnumerationPair.Clone(t));
			}
			return res;
		}
		public void UpdateFrom(Enumeration from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
			this.DataTypeEnum = from.DataTypeEnum;
		}
		#region IEditable
		public override Enumeration Backup()
		{
			return vSharpStudio.vm.ViewModels.Enumeration.Clone(this);
		}
		public override void Restore(Enumeration from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			foreach(var t in this.ListValues)
				visitor.Visit(t);
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
		private string _Name;
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
	
	public partial class Enumerations
	{
	
		public partial class EnumerationsValidator : ValidatorBase<Enumerations, EnumerationsValidator> { }
		#region CTOR
		public Enumerations(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(EnumerationsValidator.Validator, validationCollection)
		{
			this.ListEnumerations = new ObservableCollection<Enumeration>();
			this.ListEnumerations.CollectionChanged += ListEnumerations_CollectionChanged;
			OnInit();
		}
		private void ListEnumerations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "Enumeration";
				    int i = 0;
	    			foreach (var tt in this.ListEnumerations)
		    		{
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
		public static Enumerations Clone(Enumerations from)
		{
			Enumerations res = new Enumerations();
			res.Name = from.Name;
			res.ListEnumerations = new ObservableCollection<Enumeration>();
			foreach (var t in from.ListEnumerations)
			{
				res.ListEnumerations.Add(vSharpStudio.vm.ViewModels.Enumeration.Clone(t));
			}
			return res;
		}
		public void UpdateFrom(Enumerations from)
		{
			this.Name = from.Name;
		}
		#region IEditable
		public override Enumerations Backup()
		{
			return vSharpStudio.vm.ViewModels.Enumerations.Clone(this);
		}
		public override void Restore(Enumerations from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			foreach(var t in this.ListEnumerations)
				visitor.Visit(t);
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
		private string _Name;
		partial void OnNameChanging();
		partial void OnNameChanged();
		
		public ObservableCollection<Enumeration> ListEnumerations { get; set; }
		partial void OnListEnumerationsChanging();
		partial void OnListEnumerationsChanged();
		#endregion Properties
	}
	
	public partial class Catalog
	{
	
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(CatalogValidator.Validator, validationCollection)
		{
			this.Properties = new Properties(validationCollection);
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Catalog Clone(Catalog from)
		{
			Catalog res = new Catalog();
			res.Guid = from.Guid;
			res.Name = from.Name;
			res.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered;
			res.IsMemoryOptimized = from.IsMemoryOptimized;
			res.IsSequenceHiLo = from.IsSequenceHiLo;
			res.HiLoSequenceName = from.HiLoSequenceName;
			res.HiLoSchema = from.HiLoSchema;
			res.Properties = vSharpStudio.vm.ViewModels.Properties.Clone(from.Properties);
			return res;
		}
		public void UpdateFrom(Catalog from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
			this.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered;
			this.IsMemoryOptimized = from.IsMemoryOptimized;
			this.IsSequenceHiLo = from.IsSequenceHiLo;
			this.HiLoSequenceName = from.HiLoSequenceName;
			this.HiLoSchema = from.HiLoSchema;
			this.Properties = from.Properties;
		}
		#region IEditable
		public override Catalog Backup()
		{
			return vSharpStudio.vm.ViewModels.Catalog.Clone(this);
		}
		public override void Restore(Catalog from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			visitor.Visit(this.Properties);
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
		private string _Name;
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
		private string _HiLoSequenceName;
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
		private string _HiLoSchema;
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
	
	public partial class Catalogs
	{
	
		public partial class CatalogsValidator : ValidatorBase<Catalogs, CatalogsValidator> { }
		#region CTOR
		public Catalogs(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(CatalogsValidator.Validator, validationCollection)
		{
			this.ListSharedProperties = new ObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListCatalogs = new ObservableCollection<Catalog>();
			this.ListCatalogs.CollectionChanged += ListCatalogs_CollectionChanged;
			OnInit();
		}
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "Property";
				    int i = 0;
	    			foreach (var tt in this.ListSharedProperties)
		    		{
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
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "Catalog";
				    int i = 0;
	    			foreach (var tt in this.ListCatalogs)
		    		{
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
		public static Catalogs Clone(Catalogs from)
		{
			Catalogs res = new Catalogs();
			res.Name = from.Name;
			res.ListSharedProperties = new ObservableCollection<Property>();
			foreach (var t in from.ListSharedProperties)
			{
				res.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(t));
			}
			res.ListCatalogs = new ObservableCollection<Catalog>();
			foreach (var t in from.ListCatalogs)
			{
				res.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.Clone(t));
			}
			return res;
		}
		public void UpdateFrom(Catalogs from)
		{
			this.Name = from.Name;
		}
		#region IEditable
		public override Catalogs Backup()
		{
			return vSharpStudio.vm.ViewModels.Catalogs.Clone(this);
		}
		public override void Restore(Catalogs from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			foreach(var t in this.ListSharedProperties)
				visitor.Visit(t);
			foreach(var t in this.ListCatalogs)
				visitor.Visit(t);
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
		private string _Name;
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
	
	public partial class Document
	{
	
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DocumentValidator.Validator, validationCollection)
		{
			this.Properties = new Properties(validationCollection);
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static Document Clone(Document from)
		{
			Document res = new Document();
			res.Guid = from.Guid;
			res.Name = from.Name;
			res.Properties = vSharpStudio.vm.ViewModels.Properties.Clone(from.Properties);
			return res;
		}
		public void UpdateFrom(Document from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
			this.Properties = from.Properties;
		}
		#region IEditable
		public override Document Backup()
		{
			return vSharpStudio.vm.ViewModels.Document.Clone(this);
		}
		public override void Restore(Document from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			visitor.Visit(this.Properties);
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
		private string _Name;
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
	
	public partial class Documents
	{
	
		public partial class DocumentsValidator : ValidatorBase<Documents, DocumentsValidator> { }
		#region CTOR
		public Documents(SortedObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DocumentsValidator.Validator, validationCollection)
		{
			this.ListSharedProperties = new ObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListDocuments = new ObservableCollection<Document>();
			this.ListDocuments.CollectionChanged += ListDocuments_CollectionChanged;
			OnInit();
		}
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
	        switch(e.Action)
	        {
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "Property";
				    int i = 0;
	    			foreach (var tt in this.ListSharedProperties)
		    		{
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
	            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	                #region Default Name
			    	string bname = "Document";
				    int i = 0;
	    			foreach (var tt in this.ListDocuments)
		    		{
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
		public static Documents Clone(Documents from)
		{
			Documents res = new Documents();
			res.Name = from.Name;
			res.ListSharedProperties = new ObservableCollection<Property>();
			foreach (var t in from.ListSharedProperties)
			{
				res.ListSharedProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(t));
			}
			res.ListDocuments = new ObservableCollection<Document>();
			foreach (var t in from.ListDocuments)
			{
				res.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.Clone(t));
			}
			return res;
		}
		public void UpdateFrom(Documents from)
		{
			this.Name = from.Name;
		}
		#region IEditable
		public override Documents Backup()
		{
			return vSharpStudio.vm.ViewModels.Documents.Clone(this);
		}
		public override void Restore(Documents from)
		{
		    this.UpdateFrom(from);
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			foreach(var t in this.ListSharedProperties)
				visitor.Visit(t);
			foreach(var t in this.ListDocuments)
				visitor.Visit(t);
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
		private string _Name;
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
		void Visit(Config m);
		void Visit(Property m);
		void Visit(DataType m);
		void Visit(Properties m);
		void Visit(Constant m);
		void Visit(Constants m);
		void Visit(EnumerationPair m);
		void Visit(Enumeration m);
		void Visit(Enumerations m);
		void Visit(Catalog m);
		void Visit(Catalogs m);
		void Visit(Document m);
		void Visit(Documents m);
	}
	
	public interface IVisitorProto
	{
		void Visit(proto_config m);
		void Visit(proto_property m);
		void Visit(proto_data_type m);
		void Visit(proto_properties m);
		void Visit(proto_constant m);
		void Visit(proto_constants m);
		void Visit(proto_enumeration_pair m);
		void Visit(proto_enumeration m);
		void Visit(proto_enumerations m);
		void Visit(proto_catalog m);
		void Visit(proto_catalogs m);
		void Visit(proto_document m);
		void Visit(proto_documents m);
	}
	
	public static class ProtoToVM
	{
	    // Conversion from "promo_config" to "Config"
		public static Config ConvertToVM(proto_config m, SortedObservableCollection<ValidationMessage> validationCollection = null, Config vm = null)
	    {
	        if (vm == null)
	            vm = new Config(validationCollection);
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
		    vm.Constants = ConvertToVM(m.Constants);
		    vm.Enumerators = ConvertToVM(m.Enumerators);
		    vm.Catalogs = ConvertToVM(m.Catalogs);
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Property ConvertToVM(proto_property m, SortedObservableCollection<ValidationMessage> validationCollection = null, Property vm = null)
	    {
	        if (vm == null)
	            vm = new Property(validationCollection);
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.DataType = ConvertToVM(m.DataType);
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static DataType ConvertToVM(proto_data_type m, SortedObservableCollection<ValidationMessage> validationCollection = null, DataType vm = null)
	    {
	        if (vm == null)
	            vm = new DataType(validationCollection);
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
		public static Properties ConvertToVM(proto_properties m, SortedObservableCollection<ValidationMessage> validationCollection = null, Properties vm = null)
	    {
	        if (vm == null)
	            vm = new Properties(validationCollection);
		    vm.Name = m.Name;
	        vm.ListProperties = new ObservableCollection<Property>();
		    foreach(var t in m.ListProperties)
	            vm.ListProperties.Add(ConvertToVM(t));
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Constant ConvertToVM(proto_constant m, SortedObservableCollection<ValidationMessage> validationCollection = null, Constant vm = null)
	    {
	        if (vm == null)
	            vm = new Constant(validationCollection);
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.ConstantType = ConvertToVM(m.ConstantType);
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Constants ConvertToVM(proto_constants m, SortedObservableCollection<ValidationMessage> validationCollection = null, Constants vm = null)
	    {
	        if (vm == null)
	            vm = new Constants(validationCollection);
		    vm.Name = m.Name;
	        vm.ListConstants = new ObservableCollection<Constant>();
		    foreach(var t in m.ListConstants)
	            vm.ListConstants.Add(ConvertToVM(t));
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static EnumerationPair ConvertToVM(proto_enumeration_pair m, SortedObservableCollection<ValidationMessage> validationCollection = null, EnumerationPair vm = null)
	    {
	        if (vm == null)
	            vm = new EnumerationPair(validationCollection);
		    vm.Name = m.Name;
		    vm.Value = m.Value;
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Enumeration ConvertToVM(proto_enumeration m, SortedObservableCollection<ValidationMessage> validationCollection = null, Enumeration vm = null)
	    {
	        if (vm == null)
	            vm = new Enumeration(validationCollection);
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.DataTypeEnum = m.DataTypeEnum;
	        vm.ListValues = new ObservableCollection<EnumerationPair>();
		    foreach(var t in m.ListValues)
	            vm.ListValues.Add(ConvertToVM(t));
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Enumerations ConvertToVM(proto_enumerations m, SortedObservableCollection<ValidationMessage> validationCollection = null, Enumerations vm = null)
	    {
	        if (vm == null)
	            vm = new Enumerations(validationCollection);
		    vm.Name = m.Name;
	        vm.ListEnumerations = new ObservableCollection<Enumeration>();
		    foreach(var t in m.ListEnumerations)
	            vm.ListEnumerations.Add(ConvertToVM(t));
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Catalog ConvertToVM(proto_catalog m, SortedObservableCollection<ValidationMessage> validationCollection = null, Catalog vm = null)
	    {
	        if (vm == null)
	            vm = new Catalog(validationCollection);
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.IsPrimaryKeyClustered = m.IsPrimaryKeyClustered.HasValue ? m.IsPrimaryKeyClustered.Value : (bool?)null;
		    vm.IsMemoryOptimized = m.IsMemoryOptimized.HasValue ? m.IsMemoryOptimized.Value : (bool?)null;
		    vm.IsSequenceHiLo = m.IsSequenceHiLo.HasValue ? m.IsSequenceHiLo.Value : (bool?)null;
		    vm.HiLoSequenceName = m.HiLoSequenceName;
		    vm.HiLoSchema = m.HiLoSchema;
		    vm.Properties = ConvertToVM(m.Properties);
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Catalogs ConvertToVM(proto_catalogs m, SortedObservableCollection<ValidationMessage> validationCollection = null, Catalogs vm = null)
	    {
	        if (vm == null)
	            vm = new Catalogs(validationCollection);
		    vm.Name = m.Name;
	        vm.ListSharedProperties = new ObservableCollection<Property>();
		    foreach(var t in m.ListSharedProperties)
	            vm.ListSharedProperties.Add(ConvertToVM(t));
	        vm.ListCatalogs = new ObservableCollection<Catalog>();
		    foreach(var t in m.ListCatalogs)
	            vm.ListCatalogs.Add(ConvertToVM(t));
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Document ConvertToVM(proto_document m, SortedObservableCollection<ValidationMessage> validationCollection = null, Document vm = null)
	    {
	        if (vm == null)
	            vm = new Document(validationCollection);
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.Properties = ConvertToVM(m.Properties);
	        vm.OnInitFromDto();
	        return vm;
	    }
		public static Documents ConvertToVM(proto_documents m, SortedObservableCollection<ValidationMessage> validationCollection = null, Documents vm = null)
	    {
	        if (vm == null)
	            vm = new Documents(validationCollection);
		    vm.Name = m.Name;
	        vm.ListSharedProperties = new ObservableCollection<Property>();
		    foreach(var t in m.ListSharedProperties)
	            vm.ListSharedProperties.Add(ConvertToVM(t));
	        vm.ListDocuments = new ObservableCollection<Document>();
		    foreach(var t in m.ListDocuments)
	            vm.ListDocuments.Add(ConvertToVM(t));
	        vm.OnInitFromDto();
	        return vm;
	    }
	    // Conversion from "Config" to "promo_config"
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
		    m.Constants = ConvertToProto(vm.Constants);
		    m.Enumerators = ConvertToProto(vm.Enumerators);
		    m.Catalogs = ConvertToProto(vm.Catalogs);
	        return m;
	    }
		public static proto_property ConvertToProto(Property vm)
	    {
	        proto_property m = new proto_property();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.DataType = ConvertToProto(vm.DataType);
	        return m;
	    }
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
		public static proto_properties ConvertToProto(Properties vm)
	    {
	        proto_properties m = new proto_properties();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListProperties)
	            m.ListProperties.Add(ConvertToProto(t));
	        return m;
	    }
		public static proto_constant ConvertToProto(Constant vm)
	    {
	        proto_constant m = new proto_constant();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.ConstantType = ConvertToProto(vm.ConstantType);
	        return m;
	    }
		public static proto_constants ConvertToProto(Constants vm)
	    {
	        proto_constants m = new proto_constants();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListConstants)
	            m.ListConstants.Add(ConvertToProto(t));
	        return m;
	    }
		public static proto_enumeration_pair ConvertToProto(EnumerationPair vm)
	    {
	        proto_enumeration_pair m = new proto_enumeration_pair();
		    m.Name = vm.Name;
		    m.Value = vm.Value;
	        return m;
	    }
		public static proto_enumeration ConvertToProto(Enumeration vm)
	    {
	        proto_enumeration m = new proto_enumeration();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.DataTypeEnum = vm.DataTypeEnum;
		    foreach(var t in vm.ListValues)
	            m.ListValues.Add(ConvertToProto(t));
	        return m;
	    }
		public static proto_enumerations ConvertToProto(Enumerations vm)
	    {
	        proto_enumerations m = new proto_enumerations();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListEnumerations)
	            m.ListEnumerations.Add(ConvertToProto(t));
	        return m;
	    }
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
		    m.Properties = ConvertToProto(vm.Properties);
	        return m;
	    }
		public static proto_catalogs ConvertToProto(Catalogs vm)
	    {
	        proto_catalogs m = new proto_catalogs();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListSharedProperties)
	            m.ListSharedProperties.Add(ConvertToProto(t));
		    foreach(var t in vm.ListCatalogs)
	            m.ListCatalogs.Add(ConvertToProto(t));
	        return m;
	    }
		public static proto_document ConvertToProto(Document vm)
	    {
	        proto_document m = new proto_document();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.Properties = ConvertToProto(vm.Properties);
	        return m;
	    }
		public static proto_documents ConvertToProto(Documents vm)
	    {
	        proto_documents m = new proto_documents();
		    m.Name = vm.Name;
		    foreach(var t in vm.ListSharedProperties)
	            m.ListSharedProperties.Add(ConvertToProto(t));
		    foreach(var t in vm.ListDocuments)
	            m.ListDocuments.Add(ConvertToProto(t));
	        return m;
	    }
	}
}