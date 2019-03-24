// Auto generated on UTC 03/24/2019 00:41:59
using System;
using ViewModelBase;
using FluentValidation;
using Proto.Config;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace vSharpStudio.vm.ViewModels
{
	
	public partial class Config
	{
	
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConfigValidator.Validator, validationCollection)
		{
			this._dto = new proto_config();
			this.Constants = new Constants();
			this.Enumerators = new Enumerations();
			this.Catalogs = new Catalogs();
			OnInit();
		}
		public Config(proto_config dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConfigValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.Constants = new Constants(_dto.Constants);
			this.Enumerators = new Enumerations(_dto.Enumerators);
			this.Catalogs = new Catalogs(_dto.Catalogs);
		}
		private proto_config _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Config Clone()
		{
			Config res = new Config();
			res.Guid = this.Guid;
			res.Version = this.Version;
			res.Name = this.Name;
			res.IsDbFromConnectionString = this.IsDbFromConnectionString;
			res.ConnectionStringName = this.ConnectionStringName;
			res.DbTypeEnum = this.DbTypeEnum;
			res.DbServer = this.DbServer;
			res.DbDatabaseName = this.DbDatabaseName;
			res.IsDbWindowsAuthentication = this.IsDbWindowsAuthentication;
			res.DbUser = this.DbUser;
			res.DbPasswork = this.DbPasswork;
			res.PathToProjectWithConnectionString = this.PathToProjectWithConnectionString;
			res.DbSchema = this.DbSchema;
			res.PrimaryKeyName = this.PrimaryKeyName;
			res.IsPrimaryKeyClustered = this.IsPrimaryKeyClustered;
			res.IsMemoryOptimized = this.IsMemoryOptimized;
			res.IsSequenceHiLo = this.IsSequenceHiLo;
			res.HiLoSequenceName = this.HiLoSequenceName;
			res.HiLoSchema = this.HiLoSchema;
			res.Constants = this.Constants.Clone();
			res.Enumerators = this.Enumerators.Clone();
			res.Catalogs = this.Catalogs.Clone();
			return res;
		}
		#region IEditable
		protected override Config Backup()
		{
			Config res = new Config();
			res.Guid = this.Guid;
			res.Version = this.Version;
			res.Name = this.Name;
			res.IsDbFromConnectionString = this.IsDbFromConnectionString;
			res.ConnectionStringName = this.ConnectionStringName;
			res.DbTypeEnum = this.DbTypeEnum;
			res.DbServer = this.DbServer;
			res.DbDatabaseName = this.DbDatabaseName;
			res.IsDbWindowsAuthentication = this.IsDbWindowsAuthentication;
			res.DbUser = this.DbUser;
			res.DbPasswork = this.DbPasswork;
			res.PathToProjectWithConnectionString = this.PathToProjectWithConnectionString;
			res.DbSchema = this.DbSchema;
			res.PrimaryKeyName = this.PrimaryKeyName;
			res.IsPrimaryKeyClustered = this.IsPrimaryKeyClustered;
			res.IsMemoryOptimized = this.IsMemoryOptimized;
			res.IsSequenceHiLo = this.IsSequenceHiLo;
			res.HiLoSequenceName = this.HiLoSequenceName;
			res.HiLoSchema = this.HiLoSchema;
			return res;
		}
		protected override void Restore(Config from)
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
			this.DbPasswork = from.DbPasswork;
			this.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
			this.DbSchema = from.DbSchema;
			this.PrimaryKeyName = from.PrimaryKeyName;
			this.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered;
			this.IsMemoryOptimized = from.IsMemoryOptimized;
			this.IsSequenceHiLo = from.IsSequenceHiLo;
			this.HiLoSequenceName = from.HiLoSequenceName;
			this.HiLoSchema = from.HiLoSchema;
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
				if (_dto.Version != value)
				{
					_dto.Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Version; }
		}
		partial void OnVersionChanged();
		
		
		
		public string Name
		{ 
			set
			{
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public bool IsDbFromConnectionString
		{ 
			set
			{
				if (_dto.IsDbFromConnectionString != value)
				{
					_dto.IsDbFromConnectionString = value;
					OnIsDbFromConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsDbFromConnectionString; }
		}
		partial void OnIsDbFromConnectionStringChanged();
		
		
		
		public string ConnectionStringName
		{ 
			set
			{
				if (_dto.ConnectionStringName != value)
				{
					_dto.ConnectionStringName = value;
					OnConnectionStringNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.ConnectionStringName; }
		}
		partial void OnConnectionStringNameChanged();
		
		
		
		public proto_config.Types.EnumDbType DbTypeEnum
		{ 
			set
			{
				if (_dto.DbTypeEnum != value)
				{
					_dto.DbTypeEnum = value;
					OnDbTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.DbTypeEnum; }
		}
		partial void OnDbTypeEnumChanged();
		
		
		
		public string DbServer
		{ 
			set
			{
				if (_dto.DbServer != value)
				{
					_dto.DbServer = value;
					OnDbServerChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.DbServer; }
		}
		partial void OnDbServerChanged();
		
		
		
		public string DbDatabaseName
		{ 
			set
			{
				if (_dto.DbDatabaseName != value)
				{
					_dto.DbDatabaseName = value;
					OnDbDatabaseNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.DbDatabaseName; }
		}
		partial void OnDbDatabaseNameChanged();
		
		
		
		public bool IsDbWindowsAuthentication
		{ 
			set
			{
				if (_dto.IsDbWindowsAuthentication != value)
				{
					_dto.IsDbWindowsAuthentication = value;
					OnIsDbWindowsAuthenticationChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsDbWindowsAuthentication; }
		}
		partial void OnIsDbWindowsAuthenticationChanged();
		
		
		
		public string DbUser
		{ 
			set
			{
				if (_dto.DbUser != value)
				{
					_dto.DbUser = value;
					OnDbUserChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.DbUser; }
		}
		partial void OnDbUserChanged();
		
		
		
		public string DbPasswork
		{ 
			set
			{
				if (_dto.DbPasswork != value)
				{
					_dto.DbPasswork = value;
					OnDbPassworkChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.DbPasswork; }
		}
		partial void OnDbPassworkChanged();
		
		
		
		public string PathToProjectWithConnectionString
		{ 
			set
			{
				if (_dto.PathToProjectWithConnectionString != value)
				{
					_dto.PathToProjectWithConnectionString = value;
					OnPathToProjectWithConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.PathToProjectWithConnectionString; }
		}
		partial void OnPathToProjectWithConnectionStringChanged();
		
		
		
		public string DbSchema
		{ 
			set
			{
				if (_dto.DbSchema != value)
				{
					_dto.DbSchema = value;
					OnDbSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.DbSchema; }
		}
		partial void OnDbSchemaChanged();
		
		
		
		public string PrimaryKeyName
		{ 
			set
			{
				if (_dto.PrimaryKeyName != value)
				{
					_dto.PrimaryKeyName = value;
					OnPrimaryKeyNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.PrimaryKeyName; }
		}
		partial void OnPrimaryKeyNameChanged();
		
		
		
		public bool IsPrimaryKeyClustered
		{ 
			set
			{
				if (_dto.IsPrimaryKeyClustered != value)
				{
					_dto.IsPrimaryKeyClustered = value;
					OnIsPrimaryKeyClusteredChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsPrimaryKeyClustered; }
		}
		partial void OnIsPrimaryKeyClusteredChanged();
		
		
		
		public bool IsMemoryOptimized
		{ 
			set
			{
				if (_dto.IsMemoryOptimized != value)
				{
					_dto.IsMemoryOptimized = value;
					OnIsMemoryOptimizedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsMemoryOptimized; }
		}
		partial void OnIsMemoryOptimizedChanged();
		
		
		
		public bool IsSequenceHiLo
		{ 
			set
			{
				if (_dto.IsSequenceHiLo != value)
				{
					_dto.IsSequenceHiLo = value;
					OnIsSequenceHiLoChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsSequenceHiLo; }
		}
		partial void OnIsSequenceHiLoChanged();
		
		
		
		public string HiLoSequenceName
		{ 
			set
			{
				if (_dto.HiLoSequenceName != value)
				{
					_dto.HiLoSequenceName = value;
					OnHiLoSequenceNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.HiLoSequenceName; }
		}
		partial void OnHiLoSequenceNameChanged();
		
		
		
		public string HiLoSchema
		{ 
			set
			{
				if (_dto.HiLoSchema != value)
				{
					_dto.HiLoSchema = value;
					OnHiLoSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.HiLoSchema; }
		}
		partial void OnHiLoSchemaChanged();
		
		
		
		public Constants Constants { set; get; }
		
		
		
		public Enumerations Enumerators { set; get; }
		
		
		
		public Catalogs Catalogs { set; get; }
		
		
		#endregion Properties
	}
	
	public partial class Property
	{
	
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { }
		#region CTOR
		public Property(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(PropertyValidator.Validator, validationCollection)
		{
			this._dto = new proto_property();
			this.DataType = new DataType();
			OnInit();
		}
		public Property(proto_property dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(PropertyValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.DataType = new DataType(_dto.DataType);
		}
		private proto_property _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Property Clone()
		{
			Property res = new Property();
			res.Guid = this.Guid;
			res.Name = this.Name;
			res.DataType = this.DataType.Clone();
			return res;
		}
		#region IEditable
		protected override Property Backup()
		{
			Property res = new Property();
			res.Guid = this.Guid;
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Property from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public DataType DataType { set; get; }
		
		
		#endregion Properties
	}
	
	public partial class DataType
	{
	
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { }
		#region CTOR
		public DataType(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DataTypeValidator.Validator, validationCollection)
		{
			this._dto = new proto_data_type();
			OnInit();
		}
		public DataType(proto_data_type dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DataTypeValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
		}
		private proto_data_type _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public DataType Clone()
		{
			DataType res = new DataType();
			res.DataTypeEnum = this.DataTypeEnum;
			res.Length = this.Length;
			res.Accuracy = this.Accuracy;
			res.IsPositive = this.IsPositive;
			res.TypeGuid = this.TypeGuid;
			res.MinValueString = this.MinValueString;
			res.MaxValueString = this.MaxValueString;
			res.ObjectName = this.ObjectName;
			return res;
		}
		#region IEditable
		protected override DataType Backup()
		{
			DataType res = new DataType();
			res.DataTypeEnum = this.DataTypeEnum;
			res.Length = this.Length;
			res.Accuracy = this.Accuracy;
			res.IsPositive = this.IsPositive;
			res.TypeGuid = this.TypeGuid;
			res.MinValueString = this.MinValueString;
			res.MaxValueString = this.MaxValueString;
			res.ObjectName = this.ObjectName;
			return res;
		}
		protected override void Restore(DataType from)
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
				if (_dto.DataTypeEnum != value)
				{
					_dto.DataTypeEnum = value;
					OnDataTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.DataTypeEnum; }
		}
		partial void OnDataTypeEnumChanged();
		
		
		
		public uint Length
		{ 
			set
			{
				if (_dto.Length != value)
				{
					_dto.Length = value;
					OnLengthChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Length; }
		}
		partial void OnLengthChanged();
		
		
		
		public uint Accuracy
		{ 
			set
			{
				if (_dto.Accuracy != value)
				{
					_dto.Accuracy = value;
					OnAccuracyChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Accuracy; }
		}
		partial void OnAccuracyChanged();
		
		
		
		public bool IsPositive
		{ 
			set
			{
				if (_dto.IsPositive != value)
				{
					_dto.IsPositive = value;
					OnIsPositiveChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsPositive; }
		}
		partial void OnIsPositiveChanged();
		
		
		
		public string TypeGuid
		{ 
			set
			{
				if (_dto.TypeGuid != value)
				{
					_dto.TypeGuid = value;
					OnTypeGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.TypeGuid; }
		}
		partial void OnTypeGuidChanged();
		
		
		
		public string MinValueString
		{ 
			set
			{
				if (_dto.MinValueString != value)
				{
					_dto.MinValueString = value;
					OnMinValueStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.MinValueString; }
		}
		partial void OnMinValueStringChanged();
		
		
		
		public string MaxValueString
		{ 
			set
			{
				if (_dto.MaxValueString != value)
				{
					_dto.MaxValueString = value;
					OnMaxValueStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.MaxValueString; }
		}
		partial void OnMaxValueStringChanged();
		
		
		
		public string ObjectName
		{ 
			set
			{
				if (_dto.ObjectName != value)
				{
					_dto.ObjectName = value;
					OnObjectNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.ObjectName; }
		}
		partial void OnObjectNameChanged();
		
		
		#endregion Properties
	}
	
	public partial class Properties
	{
	
		public partial class PropertiesValidator : ValidatorBase<Properties, PropertiesValidator> { }
		#region CTOR
		public Properties(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(PropertiesValidator.Validator, validationCollection)
		{
			this._dto = new proto_properties();
			this.ListProperties = new ObservableCollection<Property>();
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
			OnInit();
		}
		private void ListProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
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
					i++;
					(t as Property).Name = bname + i;
				}
			}
		}
		public Properties(proto_properties dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(PropertiesValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.ListProperties = new ObservableCollection<Property>();
			foreach (var t in _dto.ListProperties)
			{
				this.ListProperties.Add(new Property(t));
			}
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
		}
		private proto_properties _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Properties Clone()
		{
			Properties res = new Properties();
			res.Name = this.Name;
			res.ListProperties = new ObservableCollection<Property>();
			foreach (var t in this.ListProperties)
			{
				res.ListProperties.Add(t.Clone());
			}
			return res;
		}
		#region IEditable
		protected override Properties Backup()
		{
			Properties res = new Properties();
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Properties from)
		{
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public ObservableCollection<Property> ListProperties { get; set; }
		
		
		#endregion Properties
	}
	
	public partial class Constant
	{
	
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { }
		#region CTOR
		public Constant(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConstantValidator.Validator, validationCollection)
		{
			this._dto = new proto_constant();
			this.ConstantType = new Property();
			OnInit();
		}
		public Constant(proto_constant dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConstantValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.ConstantType = new Property(_dto.ConstantType);
		}
		private proto_constant _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Constant Clone()
		{
			Constant res = new Constant();
			res.Guid = this.Guid;
			res.Name = this.Name;
			res.ConstantType = this.ConstantType.Clone();
			return res;
		}
		#region IEditable
		protected override Constant Backup()
		{
			Constant res = new Constant();
			res.Guid = this.Guid;
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Constant from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public Property ConstantType { set; get; }
		
		
		#endregion Properties
	}
	
	public partial class Constants
	{
	
		public partial class ConstantsValidator : ValidatorBase<Constants, ConstantsValidator> { }
		#region CTOR
		public Constants(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConstantsValidator.Validator, validationCollection)
		{
			this._dto = new proto_constants();
			this.ListConstants = new ObservableCollection<Constant>();
			this.ListConstants.CollectionChanged += ListConstants_CollectionChanged;
			OnInit();
		}
		private void ListConstants_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
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
					i++;
					(t as Constant).Name = bname + i;
				}
			}
		}
		public Constants(proto_constants dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(ConstantsValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.ListConstants = new ObservableCollection<Constant>();
			foreach (var t in _dto.ListConstants)
			{
				this.ListConstants.Add(new Constant(t));
			}
			this.ListConstants.CollectionChanged += ListConstants_CollectionChanged;
		}
		private proto_constants _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Constants Clone()
		{
			Constants res = new Constants();
			res.Name = this.Name;
			res.ListConstants = new ObservableCollection<Constant>();
			foreach (var t in this.ListConstants)
			{
				res.ListConstants.Add(t.Clone());
			}
			return res;
		}
		#region IEditable
		protected override Constants Backup()
		{
			Constants res = new Constants();
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Constants from)
		{
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public ObservableCollection<Constant> ListConstants { get; set; }
		
		
		#endregion Properties
	}
	
	public partial class Enumeration
	{
	
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { }
		#region CTOR
		public Enumeration(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(EnumerationValidator.Validator, validationCollection)
		{
			this._dto = new proto_enumeration();
			OnInit();
		}
		public Enumeration(proto_enumeration dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(EnumerationValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
		}
		private proto_enumeration _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Enumeration Clone()
		{
			Enumeration res = new Enumeration();
			res.Guid = this.Guid;
			res.Name = this.Name;
			return res;
		}
		#region IEditable
		protected override Enumeration Backup()
		{
			Enumeration res = new Enumeration();
			res.Guid = this.Guid;
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Enumeration from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		#endregion Properties
	}
	
	public partial class Enumerations
	{
	
		public partial class EnumerationsValidator : ValidatorBase<Enumerations, EnumerationsValidator> { }
		#region CTOR
		public Enumerations(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(EnumerationsValidator.Validator, validationCollection)
		{
			this._dto = new proto_enumerations();
			this.ListEnumerations = new ObservableCollection<Enumeration>();
			this.ListEnumerations.CollectionChanged += ListEnumerations_CollectionChanged;
			OnInit();
		}
		private void ListEnumerations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
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
					i++;
					(t as Enumeration).Name = bname + i;
				}
			}
		}
		public Enumerations(proto_enumerations dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(EnumerationsValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.ListEnumerations = new ObservableCollection<Enumeration>();
			foreach (var t in _dto.ListEnumerations)
			{
				this.ListEnumerations.Add(new Enumeration(t));
			}
			this.ListEnumerations.CollectionChanged += ListEnumerations_CollectionChanged;
		}
		private proto_enumerations _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Enumerations Clone()
		{
			Enumerations res = new Enumerations();
			res.Name = this.Name;
			res.ListEnumerations = new ObservableCollection<Enumeration>();
			foreach (var t in this.ListEnumerations)
			{
				res.ListEnumerations.Add(t.Clone());
			}
			return res;
		}
		#region IEditable
		protected override Enumerations Backup()
		{
			Enumerations res = new Enumerations();
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Enumerations from)
		{
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public ObservableCollection<Enumeration> ListEnumerations { get; set; }
		
		
		#endregion Properties
	}
	
	public partial class Catalog
	{
	
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(CatalogValidator.Validator, validationCollection)
		{
			this._dto = new proto_catalog();
			this.Properties = new Properties();
			OnInit();
		}
		public Catalog(proto_catalog dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(CatalogValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.IsPrimaryKeyClustered = _dto.IsPrimaryKeyClustered.HasValue ? _dto.IsPrimaryKeyClustered.Value : (bool?)null;
			this.IsMemoryOptimized = _dto.IsMemoryOptimized.HasValue ? _dto.IsMemoryOptimized.Value : (bool?)null;
			this.IsSequenceHiLo = _dto.IsSequenceHiLo.HasValue ? _dto.IsSequenceHiLo.Value : (bool?)null;
			this.Properties = new Properties(_dto.Properties);
		}
		private proto_catalog _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Catalog Clone()
		{
			Catalog res = new Catalog();
			res.Guid = this.Guid;
			res.Name = this.Name;
			res.IsPrimaryKeyClustered = this.IsPrimaryKeyClustered;
			res.IsMemoryOptimized = this.IsMemoryOptimized;
			res.IsSequenceHiLo = this.IsSequenceHiLo;
			res.HiLoSequenceName = this.HiLoSequenceName;
			res.HiLoSchema = this.HiLoSchema;
			res.Properties = this.Properties.Clone();
			return res;
		}
		#region IEditable
		protected override Catalog Backup()
		{
			Catalog res = new Catalog();
			res.Guid = this.Guid;
			res.Name = this.Name;
			res.HiLoSequenceName = this.HiLoSequenceName;
			res.HiLoSchema = this.HiLoSchema;
			return res;
		}
		protected override void Restore(Catalog from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
			this.HiLoSequenceName = from.HiLoSequenceName;
			this.HiLoSchema = from.HiLoSchema;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public bool? IsPrimaryKeyClustered
		{ 
			set
			{
				if (_dto.IsPrimaryKeyClustered.HasValue != value.HasValue || (value.HasValue && _dto.IsPrimaryKeyClustered.Value != value.Value))
				{
					_dto.IsPrimaryKeyClustered.HasValue = value.HasValue;
					_dto.IsPrimaryKeyClustered.Value = value.Value;
					OnIsPrimaryKeyClusteredChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsPrimaryKeyClustered.HasValue ? _dto.IsPrimaryKeyClustered.Value : (bool?)null; }
		}
		partial void OnIsPrimaryKeyClusteredChanged();
		
		
		
		public bool? IsMemoryOptimized
		{ 
			set
			{
				if (_dto.IsMemoryOptimized.HasValue != value.HasValue || (value.HasValue && _dto.IsMemoryOptimized.Value != value.Value))
				{
					_dto.IsMemoryOptimized.HasValue = value.HasValue;
					_dto.IsMemoryOptimized.Value = value.Value;
					OnIsMemoryOptimizedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsMemoryOptimized.HasValue ? _dto.IsMemoryOptimized.Value : (bool?)null; }
		}
		partial void OnIsMemoryOptimizedChanged();
		
		
		
		public bool? IsSequenceHiLo
		{ 
			set
			{
				if (_dto.IsSequenceHiLo.HasValue != value.HasValue || (value.HasValue && _dto.IsSequenceHiLo.Value != value.Value))
				{
					_dto.IsSequenceHiLo.HasValue = value.HasValue;
					_dto.IsSequenceHiLo.Value = value.Value;
					OnIsSequenceHiLoChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.IsSequenceHiLo.HasValue ? _dto.IsSequenceHiLo.Value : (bool?)null; }
		}
		partial void OnIsSequenceHiLoChanged();
		
		
		
		public string HiLoSequenceName
		{ 
			set
			{
				if (_dto.HiLoSequenceName != value)
				{
					_dto.HiLoSequenceName = value;
					OnHiLoSequenceNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.HiLoSequenceName; }
		}
		partial void OnHiLoSequenceNameChanged();
		
		
		
		public string HiLoSchema
		{ 
			set
			{
				if (_dto.HiLoSchema != value)
				{
					_dto.HiLoSchema = value;
					OnHiLoSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.HiLoSchema; }
		}
		partial void OnHiLoSchemaChanged();
		
		
		
		public Properties Properties { set; get; }
		
		
		#endregion Properties
	}
	
	public partial class Catalogs
	{
	
		public partial class CatalogsValidator : ValidatorBase<Catalogs, CatalogsValidator> { }
		#region CTOR
		public Catalogs(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(CatalogsValidator.Validator, validationCollection)
		{
			this._dto = new proto_catalogs();
			this.ListSharedProperties = new ObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListCatalogs = new ObservableCollection<Catalog>();
			this.ListCatalogs.CollectionChanged += ListCatalogs_CollectionChanged;
			OnInit();
		}
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
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
					i++;
					(t as Property).Name = bname + i;
				}
			}
		}
		private void ListCatalogs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
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
					i++;
					(t as Catalog).Name = bname + i;
				}
			}
		}
		public Catalogs(proto_catalogs dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(CatalogsValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.ListSharedProperties = new ObservableCollection<Property>();
			foreach (var t in _dto.ListSharedProperties)
			{
				this.ListSharedProperties.Add(new Property(t));
			}
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListCatalogs = new ObservableCollection<Catalog>();
			foreach (var t in _dto.ListCatalogs)
			{
				this.ListCatalogs.Add(new Catalog(t));
			}
			this.ListCatalogs.CollectionChanged += ListCatalogs_CollectionChanged;
		}
		private proto_catalogs _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Catalogs Clone()
		{
			Catalogs res = new Catalogs();
			res.Name = this.Name;
			res.ListSharedProperties = new ObservableCollection<Property>();
			foreach (var t in this.ListSharedProperties)
			{
				res.ListSharedProperties.Add(t.Clone());
			}
			res.ListCatalogs = new ObservableCollection<Catalog>();
			foreach (var t in this.ListCatalogs)
			{
				res.ListCatalogs.Add(t.Clone());
			}
			return res;
		}
		#region IEditable
		protected override Catalogs Backup()
		{
			Catalogs res = new Catalogs();
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Catalogs from)
		{
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public ObservableCollection<Property> ListSharedProperties { get; set; }
		
		
		
		public ObservableCollection<Catalog> ListCatalogs { get; set; }
		
		
		#endregion Properties
	}
	
	public partial class Document
	{
	
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DocumentValidator.Validator, validationCollection)
		{
			this._dto = new proto_document();
			this.Properties = new ObservableCollection<Properties>();
			this.Properties.CollectionChanged += Properties_CollectionChanged;
			OnInit();
		}
		private void Properties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
				string bname = "Properties";
				int i = 0;
				foreach (var tt in this.Properties)
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
					i++;
					(t as Properties).Name = bname + i;
				}
			}
		}
		public Document(proto_document dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DocumentValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.Properties = new ObservableCollection<Properties>();
			foreach (var t in _dto.Properties)
			{
				this.Properties.Add(new Properties(t));
			}
			this.Properties.CollectionChanged += Properties_CollectionChanged;
		}
		private proto_document _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Document Clone()
		{
			Document res = new Document();
			res.Guid = this.Guid;
			res.Name = this.Name;
			res.Properties = new ObservableCollection<Properties>();
			foreach (var t in this.Properties)
			{
				res.Properties.Add(t.Clone());
			}
			return res;
		}
		#region IEditable
		protected override Document Backup()
		{
			Document res = new Document();
			res.Guid = this.Guid;
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Document from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			foreach(var t in this.Properties)
				visitor.Visit(t);
		}
		#endregion Procedures
		#region Properties
		
		public string Name
		{ 
			set
			{
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public ObservableCollection<Properties> Properties { get; set; }
		
		
		#endregion Properties
	}
	
	public partial class Documents
	{
	
		public partial class DocumentsValidator : ValidatorBase<Documents, DocumentsValidator> { }
		#region CTOR
		public Documents(SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DocumentsValidator.Validator, validationCollection)
		{
			this._dto = new proto_documents();
			this.ListSharedProperties = new ObservableCollection<Property>();
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListDocuments = new ObservableCollection<Document>();
			this.ListDocuments.CollectionChanged += ListDocuments_CollectionChanged;
			OnInit();
		}
		private void ListSharedProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
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
					i++;
					(t as Property).Name = bname + i;
				}
			}
		}
		private void ListDocuments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems.Count > 0)
			{
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
					i++;
					(t as Document).Name = bname + i;
				}
			}
		}
		public Documents(proto_documents dto, SortableObservableCollection<ValidationMessage> validationCollection = null) 
	        : base(DocumentsValidator.Validator, validationCollection)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
			this.ListSharedProperties = new ObservableCollection<Property>();
			foreach (var t in _dto.ListSharedProperties)
			{
				this.ListSharedProperties.Add(new Property(t));
			}
			this.ListSharedProperties.CollectionChanged += ListSharedProperties_CollectionChanged;
			this.ListDocuments = new ObservableCollection<Document>();
			foreach (var t in _dto.ListDocuments)
			{
				this.ListDocuments.Add(new Document(t));
			}
			this.ListDocuments.CollectionChanged += ListDocuments_CollectionChanged;
		}
		private proto_documents _dto;
		partial void OnInit();
		partial void OnInitFromDto();
		#endregion CTOR
		#region Procedures
		public Documents Clone()
		{
			Documents res = new Documents();
			res.Name = this.Name;
			res.ListSharedProperties = new ObservableCollection<Property>();
			foreach (var t in this.ListSharedProperties)
			{
				res.ListSharedProperties.Add(t.Clone());
			}
			res.ListDocuments = new ObservableCollection<Document>();
			foreach (var t in this.ListDocuments)
			{
				res.ListDocuments.Add(t.Clone());
			}
			return res;
		}
		#region IEditable
		protected override Documents Backup()
		{
			Documents res = new Documents();
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Documents from)
		{
			this.Name = from.Name;
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
				if (_dto.Name != value)
				{
					_dto.Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Name; }
		}
		partial void OnNameChanged();
		
		
		
		public ObservableCollection<Property> ListSharedProperties { get; set; }
		
		
		
		public ObservableCollection<Document> ListDocuments { get; set; }
		
		
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
		void Visit(Enumeration m);
		void Visit(Enumerations m);
		void Visit(Catalog m);
		void Visit(Catalogs m);
		void Visit(Document m);
		void Visit(Documents m);
	}
}