// Auto generated on UTC 03/07/2019 23:32:57
using System;
using ViewModelBase;
using FluentValidation;
using Proto.Config;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace vSharpStudio.vm.ViewModels
{
	public partial class Config : ViewModelValidatable<Config, Config.ConfigValidator>
	{
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config() : base(ConfigValidator.Validator)
		{
			this._dto = new proto_config();
			this.Constants = new Constants();
			this.Enumerators = new Enumerations();
			this.Catalogs = new Catalogs();
			OnInit();
		}
		public Config(proto_config dto) : base(ConfigValidator.Validator)
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
			res.PathToProjectWithConnectionString = this.PathToProjectWithConnectionString;
			res.ConnectionStringName = this.ConnectionStringName;
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
			res.PathToProjectWithConnectionString = this.PathToProjectWithConnectionString;
			res.ConnectionStringName = this.ConnectionStringName;
			return res;
		}
		protected override void Restore(Config from)
		{
			this.Guid = from.Guid;
			this.Version = from.Version;
			this.Name = from.Name;
			this.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
			this.ConnectionStringName = from.ConnectionStringName;
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
		public string Guid
		{ 
			private set
			{
				if (_dto.Guid != value)
				{
					_dto.Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Guid; }
		}
		partial void OnGuidChanged();
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
		public Constants Constants { get; set; }
		public Enumerations Enumerators { get; set; }
		public Catalogs Catalogs { get; set; }
		#endregion Properties
	}
	public partial class Property : ViewModelValidatable<Property, Property.PropertyValidator>
	{
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { }
		#region CTOR
		public Property() : base(PropertyValidator.Validator)
		{
			this._dto = new proto_property();
			this.DataType = new DataType();
			OnInit();
		}
		public Property(proto_property dto) : base(PropertyValidator.Validator)
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
		public string Guid
		{ 
			private set
			{
				if (_dto.Guid != value)
				{
					_dto.Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Guid; }
		}
		partial void OnGuidChanged();
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
		public DataType DataType { get; set; }
		#endregion Properties
	}
	public partial class DataType : ViewModelValidatable<DataType, DataType.DataTypeValidator>
	{
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { }
		#region CTOR
		public DataType() : base(DataTypeValidator.Validator)
		{
			this._dto = new proto_data_type();
			OnInit();
		}
		public DataType(proto_data_type dto) : base(DataTypeValidator.Validator)
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
			res.EnumDataType = this.EnumDataType;
			res.Length = this.Length;
			res.Accuracy = this.Accuracy;
			res.IsPositive = this.IsPositive;
			return res;
		}
		#region IEditable
		protected override DataType Backup()
		{
			DataType res = new DataType();
			res.EnumDataType = this.EnumDataType;
			res.Length = this.Length;
			res.Accuracy = this.Accuracy;
			res.IsPositive = this.IsPositive;
			return res;
		}
		protected override void Restore(DataType from)
		{
			this.EnumDataType = from.EnumDataType;
			this.Length = from.Length;
			this.Accuracy = from.Accuracy;
			this.IsPositive = from.IsPositive;
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
		}
		#endregion Procedures
		#region Properties
		public proto_data_type.Types.EnumDataType EnumDataType
		{ 
			set
			{
				if (_dto.EnumDataType != value)
				{
					_dto.EnumDataType = value;
					OnEnumDataTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.EnumDataType; }
		}
		partial void OnEnumDataTypeChanged();
		public int Length
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
		public int Accuracy
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
		#endregion Properties
	}
	public partial class Properties : ViewModelValidatable<Properties, Properties.PropertiesValidator>
	{
		public partial class PropertiesValidator : ValidatorBase<Properties, PropertiesValidator> { }
		#region CTOR
		public Properties() : base(PropertiesValidator.Validator)
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
		public Properties(proto_properties dto) : base(PropertiesValidator.Validator)
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
	public partial class Constant : ViewModelValidatable<Constant, Constant.ConstantValidator>
	{
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { }
		#region CTOR
		public Constant() : base(ConstantValidator.Validator)
		{
			this._dto = new proto_constant();
			this.ConstantType = new Property();
			OnInit();
		}
		public Constant(proto_constant dto) : base(ConstantValidator.Validator)
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
		public string Guid
		{ 
			private set
			{
				if (_dto.Guid != value)
				{
					_dto.Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Guid; }
		}
		partial void OnGuidChanged();
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
		public Property ConstantType { get; set; }
		#endregion Properties
	}
	public partial class Constants : ViewModelValidatable<Constants, Constants.ConstantsValidator>
	{
		public partial class ConstantsValidator : ValidatorBase<Constants, ConstantsValidator> { }
		#region CTOR
		public Constants() : base(ConstantsValidator.Validator)
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
		public Constants(proto_constants dto) : base(ConstantsValidator.Validator)
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
			res.GroupName = this.GroupName;
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
			res.GroupName = this.GroupName;
			return res;
		}
		protected override void Restore(Constants from)
		{
			this.GroupName = from.GroupName;
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
		public string GroupName
		{ 
			set
			{
				if (_dto.GroupName != value)
				{
					_dto.GroupName = value;
					OnGroupNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.GroupName; }
		}
		partial void OnGroupNameChanged();
		public ObservableCollection<Constant> ListConstants { get; set; }
		#endregion Properties
	}
	public partial class Enumeration : ViewModelValidatable<Enumeration, Enumeration.EnumerationValidator>
	{
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { }
		#region CTOR
		public Enumeration() : base(EnumerationValidator.Validator)
		{
			this._dto = new proto_enumeration();
			OnInit();
		}
		public Enumeration(proto_enumeration dto) : base(EnumerationValidator.Validator)
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
		public string Guid
		{ 
			private set
			{
				if (_dto.Guid != value)
				{
					_dto.Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Guid; }
		}
		partial void OnGuidChanged();
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
	public partial class Enumerations : ViewModelValidatable<Enumerations, Enumerations.EnumerationsValidator>
	{
		public partial class EnumerationsValidator : ValidatorBase<Enumerations, EnumerationsValidator> { }
		#region CTOR
		public Enumerations() : base(EnumerationsValidator.Validator)
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
		public Enumerations(proto_enumerations dto) : base(EnumerationsValidator.Validator)
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
	public partial class Catalog : ViewModelValidatable<Catalog, Catalog.CatalogValidator>
	{
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog() : base(CatalogValidator.Validator)
		{
			this._dto = new proto_catalog();
			this.Properties = new Properties();
			OnInit();
		}
		public Catalog(proto_catalog dto) : base(CatalogValidator.Validator)
		{
			this._dto = dto;
			this.initFromDto();
			OnInitFromDto();
		}
		private void initFromDto()
		{
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
			res.Properties = this.Properties.Clone();
			return res;
		}
		#region IEditable
		protected override Catalog Backup()
		{
			Catalog res = new Catalog();
			res.Guid = this.Guid;
			res.Name = this.Name;
			return res;
		}
		protected override void Restore(Catalog from)
		{
			this.Guid = from.Guid;
			this.Name = from.Name;
		}
		#endregion IEditable
		public void Accept(IVisitorConfig visitor) 
		{ 
			visitor.Visit(this);
			visitor.Visit(this.Properties);
		}
		#endregion Procedures
		#region Properties
		public string Guid
		{ 
			private set
			{
				if (_dto.Guid != value)
				{
					_dto.Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Guid; }
		}
		partial void OnGuidChanged();
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
		public Properties Properties { get; set; }
		#endregion Properties
	}
	public partial class Catalogs : ViewModelValidatable<Catalogs, Catalogs.CatalogsValidator>
	{
		public partial class CatalogsValidator : ValidatorBase<Catalogs, CatalogsValidator> { }
		#region CTOR
		public Catalogs() : base(CatalogsValidator.Validator)
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
		public Catalogs(proto_catalogs dto) : base(CatalogsValidator.Validator)
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
	public partial class Document : ViewModelValidatable<Document, Document.DocumentValidator>
	{
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document() : base(DocumentValidator.Validator)
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
		public Document(proto_document dto) : base(DocumentValidator.Validator)
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
		public string Guid
		{ 
			private set
			{
				if (_dto.Guid != value)
				{
					_dto.Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.Guid; }
		}
		partial void OnGuidChanged();
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
	public partial class Documents : ViewModelValidatable<Documents, Documents.DocumentsValidator>
	{
		public partial class DocumentsValidator : ValidatorBase<Documents, DocumentsValidator> { }
		#region CTOR
		public Documents() : base(DocumentsValidator.Validator)
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
		public Documents(proto_documents dto) : base(DocumentsValidator.Validator)
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
			res.GroupName = this.GroupName;
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
			res.GroupName = this.GroupName;
			return res;
		}
		protected override void Restore(Documents from)
		{
			this.GroupName = from.GroupName;
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
		public string GroupName
		{ 
			set
			{
				if (_dto.GroupName != value)
				{
					_dto.GroupName = value;
					OnGroupNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _dto.GroupName; }
		}
		partial void OnGroupNameChanged();
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