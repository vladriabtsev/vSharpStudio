// Auto generated on UTC 04/21/2019 16:38:20
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
	
	public partial class GroupConfigs : ConfigObjectBase<GroupConfigs, GroupConfigs.GroupConfigsValidator>, IComparable<GroupConfigs>, IAccept
	{
	
		public partial class GroupConfigsValidator : ValidatorBase<GroupConfigs, GroupConfigsValidator> { }
		#region CTOR
		public GroupConfigs() : base(GroupConfigsValidator.Validator)
		{
			this.ListConfigs = new SortedObservableCollection<ConfigTree>();
			this.ListConfigs.CollectionChanged += ListConfigs_CollectionChanged;
			OnInit();
		}
		public GroupConfigs(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupConfigs.DefaultName, this, this.SubNodes);
	    }
		private void ListConfigs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
		    			(t as ConfigTree).Parent = this;
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
		    if (type == typeof(ConfigTree))
		    {
		        this.ListConfigs.Sort();
		    }
		}
		public static GroupConfigs Clone(ITreeConfigNode parent, GroupConfigs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupConfigs vm = new GroupConfigs();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.Description = from.Description;
		    vm.ListConfigs = new SortedObservableCollection<ConfigTree>();
		    foreach(var t in from.ListConfigs)
		        vm.ListConfigs.Add(vSharpStudio.vm.ViewModels.ConfigTree.Clone(vm, t, isDeep));
		    vm.RelativeConfigPath = from.RelativeConfigPath;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupConfigs to, GroupConfigs from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListConfigs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListConfigs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.ConfigTree.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListConfigs.Remove(t);
		        }
		        foreach(var tt in from.ListConfigs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListConfigs.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new ConfigTree();
		                vSharpStudio.vm.ViewModels.ConfigTree.Update(p, tt, isDeep);
		                to.ListConfigs.Add(p);
		            }
		        }
		    }
		    to.RelativeConfigPath = from.RelativeConfigPath;
		}
		#region IEditable
		public override GroupConfigs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupConfigs.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupConfigs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupConfigs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_configs' to 'GroupConfigs'
		public static GroupConfigs ConvertToVM(proto_group_configs m, GroupConfigs vm = null)
		{
		    if (vm == null)
		        vm = new GroupConfigs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.Description = m.Description;
		    vm.ListConfigs = new SortedObservableCollection<ConfigTree>();
		    foreach(var t in m.ListConfigs)
		        vm.ListConfigs.Add(vSharpStudio.vm.ViewModels.ConfigTree.ConvertToVM(t));
		    vm.RelativeConfigPath = m.RelativeConfigPath;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupConfigs' to 'proto_group_configs'
		public static proto_group_configs ConvertToProto(GroupConfigs vm)
		{
		    proto_group_configs m = new proto_group_configs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListConfigs)
		        m.ListConfigs.Add(vSharpStudio.vm.ViewModels.ConfigTree.ConvertToProto(t));
		    m.RelativeConfigPath = vm.RelativeConfigPath;
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListConfigs)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
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
		
		
		public SortedObservableCollection<ConfigTree> ListConfigs { get; set; }
		partial void OnListConfigsChanging();
		partial void OnListConfigsChanged();
		
		
		public string RelativeConfigPath
		{ 
			set
			{
				if (_RelativeConfigPath != value)
				{
					OnRelativeConfigPathChanging();
					_RelativeConfigPath = value;
					OnRelativeConfigPathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeConfigPath; }
		}
		private string _RelativeConfigPath = "";
		partial void OnRelativeConfigPathChanging();
		partial void OnRelativeConfigPathChanged();
		#endregion Properties
	}
	
	public partial class Config : ConfigObjectBase<Config, Config.ConfigValidator>, IComparable<Config>, IAccept
	{
	
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config() : base(ConfigValidator.Validator)
		{
			this.GroupConfigs = new GroupConfigs(this);
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
		        vm.GroupConfigs = vSharpStudio.vm.ViewModels.GroupConfigs.Clone(vm, from.GroupConfigs, isDeep);
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
		        GroupConfigs.Update(to.GroupConfigs, from.GroupConfigs, isDeep);
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
		    vm.GroupConfigs = vSharpStudio.vm.ViewModels.GroupConfigs.ConvertToVM(m.GroupConfigs);
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
		    m.GroupConfigs = vSharpStudio.vm.ViewModels.GroupConfigs.ConvertToProto(vm.GroupConfigs);
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
			this.GroupConfigs.Accept(visitor);
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
		
		
		public GroupConfigs GroupConfigs
		{ 
			set
			{
				if (_GroupConfigs != value)
				{
					OnGroupConfigsChanging();
		            _GroupConfigs = value;
					OnGroupConfigsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupConfigs; }
		}
		private GroupConfigs _GroupConfigs;
		partial void OnGroupConfigsChanging();
		partial void OnGroupConfigsChanged();
		
		
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
	
	public partial class ConfigTree : ConfigObjectBase<ConfigTree, ConfigTree.ConfigTreeValidator>, IComparable<ConfigTree>, IAccept
	{
	
		public partial class ConfigTreeValidator : ValidatorBase<ConfigTree, ConfigTreeValidator> { }
		#region CTOR
		public ConfigTree() : base(ConfigTreeValidator.Validator)
		{
			this.ConfigNode = new Config(this);
			this.ListConfigs = new SortedObservableCollection<ConfigTree>();
			this.ListConfigs.CollectionChanged += ListConfigs_CollectionChanged;
			OnInit();
		}
		public ConfigTree(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(ConfigTree.DefaultName, this, this.SubNodes);
	    }
		private void ListConfigs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
		    			(t as ConfigTree).Parent = this;
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
		    if (type == typeof(ConfigTree))
		    {
		        this.ListConfigs.Sort();
		    }
		}
		public static ConfigTree Clone(ITreeConfigNode parent, ConfigTree from, bool isDeep = true, bool isNewGuid = false)
		{
		    ConfigTree vm = new ConfigTree();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.ConfigNode = vSharpStudio.vm.ViewModels.Config.Clone(vm, from.ConfigNode, isDeep);
		    vm.ListConfigs = new SortedObservableCollection<ConfigTree>();
		    foreach(var t in from.ListConfigs)
		        vm.ListConfigs.Add(vSharpStudio.vm.ViewModels.ConfigTree.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(ConfigTree to, ConfigTree from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.Description = from.Description;
		    if (isDeep)
		        Config.Update(to.ConfigNode, from.ConfigNode, isDeep);
		    if (isDeep)
		    {
		        foreach(var t in to.ListConfigs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListConfigs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.ConfigTree.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListConfigs.Remove(t);
		        }
		        foreach(var tt in from.ListConfigs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListConfigs.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new ConfigTree();
		                vSharpStudio.vm.ViewModels.ConfigTree.Update(p, tt, isDeep);
		                to.ListConfigs.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override ConfigTree Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return ConfigTree.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(ConfigTree from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    ConfigTree.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_config_tree' to 'ConfigTree'
		public static ConfigTree ConvertToVM(proto_config_tree m, ConfigTree vm = null)
		{
		    if (vm == null)
		        vm = new ConfigTree();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.Description = m.Description;
		    vm.ConfigNode = vSharpStudio.vm.ViewModels.Config.ConvertToVM(m.ConfigNode);
		    vm.ListConfigs = new SortedObservableCollection<ConfigTree>();
		    foreach(var t in m.ListConfigs)
		        vm.ListConfigs.Add(vSharpStudio.vm.ViewModels.ConfigTree.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'ConfigTree' to 'proto_config_tree'
		public static proto_config_tree ConvertToProto(ConfigTree vm)
		{
		    proto_config_tree m = new proto_config_tree();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.Description = vm.Description;
		    m.ConfigNode = vSharpStudio.vm.ViewModels.Config.ConvertToProto(vm.ConfigNode);
		    foreach(var t in vm.ListConfigs)
		        m.ListConfigs.Add(vSharpStudio.vm.ViewModels.ConfigTree.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.ConfigNode.Accept(visitor);
			foreach(var t in this.ListConfigs)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
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
		
		
		public Config ConfigNode
		{ 
			set
			{
				if (_ConfigNode != value)
				{
					OnConfigNodeChanging();
		            _ConfigNode = value;
					OnConfigNodeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConfigNode; }
		}
		private Config _ConfigNode;
		partial void OnConfigNodeChanging();
		partial void OnConfigNodeChanged();
		
		
		public SortedObservableCollection<ConfigTree> ListConfigs { get; set; }
		partial void OnListConfigsChanging();
		partial void OnListConfigsChanged();
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
	
	public partial class ObjectSharedProps : ConfigObjectBase<ObjectSharedProps, ObjectSharedProps.ObjectSharedPropsValidator>, IComparable<ObjectSharedProps>, IAccept
	{
	
		public partial class ObjectSharedPropsValidator : ValidatorBase<ObjectSharedProps, ObjectSharedPropsValidator> { }
		#region CTOR
		public ObjectSharedProps() : base(ObjectSharedPropsValidator.Validator)
		{
			OnInit();
		}
		public ObjectSharedProps(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(ObjectSharedProps.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static ObjectSharedProps Clone(ITreeConfigNode parent, ObjectSharedProps from, bool isDeep = true, bool isNewGuid = false)
		{
		    ObjectSharedProps vm = new ObjectSharedProps();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(ObjectSharedProps to, ObjectSharedProps from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		}
		#region IEditable
		public override ObjectSharedProps Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return ObjectSharedProps.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(ObjectSharedProps from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    ObjectSharedProps.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_object_shared_props' to 'ObjectSharedProps'
		public static ObjectSharedProps ConvertToVM(proto_object_shared_props m, ObjectSharedProps vm = null)
		{
		    if (vm == null)
		        vm = new ObjectSharedProps();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'ObjectSharedProps' to 'proto_object_shared_props'
		public static proto_object_shared_props ConvertToProto(ObjectSharedProps vm)
		{
		    proto_object_shared_props m = new proto_object_shared_props();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		#endregion Properties
	}
	
	public partial class GroupPropertyTab : ConfigObjectBase<GroupPropertyTab, GroupPropertyTab.GroupPropertyTabValidator>, IComparable<GroupPropertyTab>, IAccept
	{
	
		public partial class GroupPropertyTabValidator : ValidatorBase<GroupPropertyTab, GroupPropertyTabValidator> { }
		#region CTOR
		public GroupPropertyTab() : base(GroupPropertyTabValidator.Validator)
		{
			this.Shared = new ObjectSharedProps(this);
			this.ListProperties = new SortedObservableCollection<Property>();
			this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
			OnInit();
		}
		public GroupPropertyTab(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupPropertyTab.DefaultName, this, this.SubNodes);
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
		public static GroupPropertyTab Clone(ITreeConfigNode parent, GroupPropertyTab from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupPropertyTab vm = new GroupPropertyTab();
		    if (isDeep)
		        vm.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.Clone(vm, from.Shared, isDeep);
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupPropertyTab to, GroupPropertyTab from, bool isDeep = true)
		{
		    if (isDeep)
		        ObjectSharedProps.Update(to.Shared, from.Shared, isDeep);
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
		public override GroupPropertyTab Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupPropertyTab.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupPropertyTab from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupPropertyTab.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_property_tab' to 'GroupPropertyTab'
		public static GroupPropertyTab ConvertToVM(proto_group_property_tab m, GroupPropertyTab vm = null)
		{
		    if (vm == null)
		        vm = new GroupPropertyTab();
		    vm.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.ConvertToVM(m.Shared);
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupPropertyTab' to 'proto_group_property_tab'
		public static proto_group_property_tab ConvertToProto(GroupPropertyTab vm)
		{
		    proto_group_property_tab m = new proto_group_property_tab();
		    m.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.ConvertToProto(vm.Shared);
		    foreach(var t in vm.ListProperties)
		        m.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Shared.Accept(visitor);
			foreach(var t in this.ListProperties)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public ObjectSharedProps Shared
		{ 
			set
			{
				if (_Shared != value)
				{
					OnSharedChanging();
		            _Shared = value;
					OnSharedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Shared; }
		}
		private ObjectSharedProps _Shared;
		partial void OnSharedChanging();
		partial void OnSharedChanged();
		
		
		public SortedObservableCollection<Property> ListProperties { get; set; }
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
		#endregion Properties
	}
	
	public partial class GroupPropertyTabs : ConfigObjectBase<GroupPropertyTabs, GroupPropertyTabs.GroupPropertyTabsValidator>, IComparable<GroupPropertyTabs>, IAccept
	{
	
		public partial class GroupPropertyTabsValidator : ValidatorBase<GroupPropertyTabs, GroupPropertyTabsValidator> { }
		#region CTOR
		public GroupPropertyTabs() : base(GroupPropertyTabsValidator.Validator)
		{
			this.Shared = new ObjectSharedProps(this);
			this.ListPropertiesTabs = new SortedObservableCollection<GroupPropertyTabsTree>();
			this.ListPropertiesTabs.CollectionChanged += ListPropertiesTabs_CollectionChanged;
			OnInit();
		}
		public GroupPropertyTabs(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupPropertyTabs.DefaultName, this, this.SubNodes);
	    }
		private void ListPropertiesTabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
		    			(t as GroupPropertyTabsTree).Parent = this;
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
		    if (type == typeof(GroupPropertyTabsTree))
		    {
		        this.ListPropertiesTabs.Sort();
		    }
		}
		public static GroupPropertyTabs Clone(ITreeConfigNode parent, GroupPropertyTabs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupPropertyTabs vm = new GroupPropertyTabs();
		    if (isDeep)
		        vm.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.Clone(vm, from.Shared, isDeep);
		    vm.ListPropertiesTabs = new SortedObservableCollection<GroupPropertyTabsTree>();
		    foreach(var t in from.ListPropertiesTabs)
		        vm.ListPropertiesTabs.Add(vSharpStudio.vm.ViewModels.GroupPropertyTabsTree.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupPropertyTabs to, GroupPropertyTabs from, bool isDeep = true)
		{
		    if (isDeep)
		        ObjectSharedProps.Update(to.Shared, from.Shared, isDeep);
		    if (isDeep)
		    {
		        foreach(var t in to.ListPropertiesTabs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPropertiesTabs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.GroupPropertyTabsTree.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListPropertiesTabs.Remove(t);
		        }
		        foreach(var tt in from.ListPropertiesTabs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListPropertiesTabs.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new GroupPropertyTabsTree();
		                vSharpStudio.vm.ViewModels.GroupPropertyTabsTree.Update(p, tt, isDeep);
		                to.ListPropertiesTabs.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupPropertyTabs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupPropertyTabs.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupPropertyTabs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupPropertyTabs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_property_tabs' to 'GroupPropertyTabs'
		public static GroupPropertyTabs ConvertToVM(proto_group_property_tabs m, GroupPropertyTabs vm = null)
		{
		    if (vm == null)
		        vm = new GroupPropertyTabs();
		    vm.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.ConvertToVM(m.Shared);
		    vm.ListPropertiesTabs = new SortedObservableCollection<GroupPropertyTabsTree>();
		    foreach(var t in m.ListPropertiesTabs)
		        vm.ListPropertiesTabs.Add(vSharpStudio.vm.ViewModels.GroupPropertyTabsTree.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupPropertyTabs' to 'proto_group_property_tabs'
		public static proto_group_property_tabs ConvertToProto(GroupPropertyTabs vm)
		{
		    proto_group_property_tabs m = new proto_group_property_tabs();
		    m.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.ConvertToProto(vm.Shared);
		    foreach(var t in vm.ListPropertiesTabs)
		        m.ListPropertiesTabs.Add(vSharpStudio.vm.ViewModels.GroupPropertyTabsTree.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Shared.Accept(visitor);
			foreach(var t in this.ListPropertiesTabs)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public ObjectSharedProps Shared
		{ 
			set
			{
				if (_Shared != value)
				{
					OnSharedChanging();
		            _Shared = value;
					OnSharedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Shared; }
		}
		private ObjectSharedProps _Shared;
		partial void OnSharedChanging();
		partial void OnSharedChanged();
		
		
		public SortedObservableCollection<GroupPropertyTabsTree> ListPropertiesTabs { get; set; }
		partial void OnListPropertiesTabsChanging();
		partial void OnListPropertiesTabsChanged();
		#endregion Properties
	}
	
	public partial class GroupPropertyTabsTree : ConfigObjectBase<GroupPropertyTabsTree, GroupPropertyTabsTree.GroupPropertyTabsTreeValidator>, IComparable<GroupPropertyTabsTree>, IAccept
	{
	
		public partial class GroupPropertyTabsTreeValidator : ValidatorBase<GroupPropertyTabsTree, GroupPropertyTabsTreeValidator> { }
		#region CTOR
		public GroupPropertyTabsTree() : base(GroupPropertyTabsTreeValidator.Validator)
		{
			this.Shared = new ObjectSharedProps(this);
			this.ListPropertiesTab = new GroupPropertyTab(this);
			this.ListPropertiesSubTabs = new GroupPropertyTabs(this);
			OnInit();
		}
		public GroupPropertyTabsTree(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupPropertyTabsTree.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static GroupPropertyTabsTree Clone(ITreeConfigNode parent, GroupPropertyTabsTree from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupPropertyTabsTree vm = new GroupPropertyTabsTree();
		    if (isDeep)
		        vm.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.Clone(vm, from.Shared, isDeep);
		    if (isDeep)
		        vm.ListPropertiesTab = vSharpStudio.vm.ViewModels.GroupPropertyTab.Clone(vm, from.ListPropertiesTab, isDeep);
		    if (isDeep)
		        vm.ListPropertiesSubTabs = vSharpStudio.vm.ViewModels.GroupPropertyTabs.Clone(vm, from.ListPropertiesSubTabs, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupPropertyTabsTree to, GroupPropertyTabsTree from, bool isDeep = true)
		{
		    if (isDeep)
		        ObjectSharedProps.Update(to.Shared, from.Shared, isDeep);
		    if (isDeep)
		        GroupPropertyTab.Update(to.ListPropertiesTab, from.ListPropertiesTab, isDeep);
		    if (isDeep)
		        GroupPropertyTabs.Update(to.ListPropertiesSubTabs, from.ListPropertiesSubTabs, isDeep);
		}
		#region IEditable
		public override GroupPropertyTabsTree Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupPropertyTabsTree.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupPropertyTabsTree from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupPropertyTabsTree.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_property_tabs_tree' to 'GroupPropertyTabsTree'
		public static GroupPropertyTabsTree ConvertToVM(proto_group_property_tabs_tree m, GroupPropertyTabsTree vm = null)
		{
		    if (vm == null)
		        vm = new GroupPropertyTabsTree();
		    vm.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.ConvertToVM(m.Shared);
		    vm.ListPropertiesTab = vSharpStudio.vm.ViewModels.GroupPropertyTab.ConvertToVM(m.ListPropertiesTab);
		    vm.ListPropertiesSubTabs = vSharpStudio.vm.ViewModels.GroupPropertyTabs.ConvertToVM(m.ListPropertiesSubTabs);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupPropertyTabsTree' to 'proto_group_property_tabs_tree'
		public static proto_group_property_tabs_tree ConvertToProto(GroupPropertyTabsTree vm)
		{
		    proto_group_property_tabs_tree m = new proto_group_property_tabs_tree();
		    m.Shared = vSharpStudio.vm.ViewModels.ObjectSharedProps.ConvertToProto(vm.Shared);
		    m.ListPropertiesTab = vSharpStudio.vm.ViewModels.GroupPropertyTab.ConvertToProto(vm.ListPropertiesTab);
		    m.ListPropertiesSubTabs = vSharpStudio.vm.ViewModels.GroupPropertyTabs.ConvertToProto(vm.ListPropertiesSubTabs);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Shared.Accept(visitor);
			this.ListPropertiesTab.Accept(visitor);
			this.ListPropertiesSubTabs.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		public ObjectSharedProps Shared
		{ 
			set
			{
				if (_Shared != value)
				{
					OnSharedChanging();
		            _Shared = value;
					OnSharedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Shared; }
		}
		private ObjectSharedProps _Shared;
		partial void OnSharedChanging();
		partial void OnSharedChanged();
		
		
		public GroupPropertyTab ListPropertiesTab
		{ 
			set
			{
				if (_ListPropertiesTab != value)
				{
					OnListPropertiesTabChanging();
		            _ListPropertiesTab = value;
					OnListPropertiesTabChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPropertiesTab; }
		}
		private GroupPropertyTab _ListPropertiesTab;
		partial void OnListPropertiesTabChanging();
		partial void OnListPropertiesTabChanged();
		
		
		public GroupPropertyTabs ListPropertiesSubTabs
		{ 
			set
			{
				if (_ListPropertiesSubTabs != value)
				{
					OnListPropertiesSubTabsChanging();
		            _ListPropertiesSubTabs = value;
					OnListPropertiesSubTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPropertiesSubTabs; }
		}
		private GroupPropertyTabs _ListPropertiesSubTabs;
		partial void OnListPropertiesSubTabsChanging();
		partial void OnListPropertiesSubTabsChanged();
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		
		
		public SortedObservableCollection<Property> ListProperties { get; set; }
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(vm, from.DataType, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Property to, Property from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType);
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto(vm.DataType);
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
		#endregion Properties
	}
	
	public partial class GroupPropertiesTree : ConfigObjectBase<GroupPropertiesTree, GroupPropertiesTree.GroupPropertiesTreeValidator>, IComparable<GroupPropertiesTree>, IAccept
	{
	
		public partial class GroupPropertiesTreeValidator : ValidatorBase<GroupPropertiesTree, GroupPropertiesTreeValidator> { }
		#region CTOR
		public GroupPropertiesTree() : base(GroupPropertiesTreeValidator.Validator)
		{
			this.ListPropertiesTreeGroups = new SortedObservableCollection<GroupPropertiesTree>();
			this.ListPropertiesTreeGroups.CollectionChanged += ListPropertiesTreeGroups_CollectionChanged;
			this.GroupProperties = new GroupProperties(this);
			OnInit();
		}
		public GroupPropertiesTree(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupPropertiesTree.DefaultName, this, this.SubNodes);
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
		public static GroupPropertiesTree Clone(ITreeConfigNode parent, GroupPropertiesTree from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupPropertiesTree vm = new GroupPropertiesTree();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListPropertiesTreeGroups = new SortedObservableCollection<GroupPropertiesTree>();
		    foreach(var t in from.ListPropertiesTreeGroups)
		        vm.ListPropertiesTreeGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.Clone(vm, t, isDeep));
		    if (isDeep)
		        vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupProperties.Clone(vm, from.GroupProperties, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupPropertiesTree to, GroupPropertiesTree from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    if (isDeep)
		        GroupProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListPropertiesTreeGroups = new SortedObservableCollection<GroupPropertiesTree>();
		    foreach(var t in m.ListPropertiesTreeGroups)
		        vm.ListPropertiesTreeGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToVM(t));
		    vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupProperties.ConvertToVM(m.GroupProperties);
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListPropertiesTreeGroups)
		        m.ListPropertiesTreeGroups.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToProto(t));
		    m.GroupProperties = vSharpStudio.vm.ViewModels.GroupProperties.ConvertToProto(vm.GroupProperties);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPropertiesTreeGroups)
				t.Accept(visitor);
			this.GroupProperties.Accept(visitor);
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
		
		
		public SortedObservableCollection<GroupPropertiesTree> ListPropertiesTreeGroups { get; set; }
		partial void OnListPropertiesTreeGroupsChanging();
		partial void OnListPropertiesTreeGroupsChanged();
		
		
		public GroupProperties GroupProperties
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging();
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupProperties _GroupProperties;
		partial void OnGroupPropertiesChanging();
		partial void OnGroupPropertiesChanged();
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		
		
		public SortedObservableCollection<Constant> ListConstants { get; set; }
		partial void OnListConstantsChanging();
		partial void OnListConstantsChanged();
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(vm, from.DataType, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Constant to, Constant from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        DataType.Update(to.DataType, from.DataType, isDeep);
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType);
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto(vm.DataType);
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		
		
		public SortedObservableCollection<Enumeration> ListEnumerations { get; set; }
		partial void OnListEnumerationsChanging();
		partial void OnListEnumerationsChanged();
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.DataTypeEnum = from.DataTypeEnum;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.DataTypeEnum = m.DataTypeEnum;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DataTypeEnum = vm.DataTypeEnum;
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
		
		
		public SortedObservableCollection<EnumerationPair> ListValues { get; set; }
		partial void OnListValuesChanging();
		partial void OnListValuesChanged();
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		
		
		public SortedObservableCollection<Catalog> ListCatalogs { get; set; }
		partial void OnListCatalogsChanging();
		partial void OnListCatalogsChanged();
		#endregion Properties
	}
	
	public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IAccept
	{
	
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog() : base(CatalogValidator.Validator)
		{
			this.GroupProperties = new GroupProperties(this);
			this.GroupSubCatalogs = new GroupListCatalogs(this);
			this.GroupForms = new GroupForms(this);
			this.GroupReports = new GroupReports(this);
			OnInit();
		}
		public Catalog(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Catalog.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
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
		    if (isDeep)
		        vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupProperties.Clone(vm, from.GroupProperties, isDeep);
		    if (isDeep)
		        vm.GroupSubCatalogs = vSharpStudio.vm.ViewModels.GroupListCatalogs.Clone(vm, from.GroupSubCatalogs, isDeep);
		    if (isDeep)
		        vm.GroupForms = vSharpStudio.vm.ViewModels.GroupForms.Clone(vm, from.GroupForms, isDeep);
		    if (isDeep)
		        vm.GroupReports = vSharpStudio.vm.ViewModels.GroupReports.Clone(vm, from.GroupReports, isDeep);
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
		        GroupProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
		    if (isDeep)
		        GroupListCatalogs.Update(to.GroupSubCatalogs, from.GroupSubCatalogs, isDeep);
		    if (isDeep)
		        GroupForms.Update(to.GroupForms, from.GroupForms, isDeep);
		    if (isDeep)
		        GroupReports.Update(to.GroupReports, from.GroupReports, isDeep);
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
		    vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupProperties.ConvertToVM(m.GroupProperties);
		    vm.GroupSubCatalogs = vSharpStudio.vm.ViewModels.GroupListCatalogs.ConvertToVM(m.GroupSubCatalogs);
		    vm.GroupForms = vSharpStudio.vm.ViewModels.GroupForms.ConvertToVM(m.GroupForms);
		    vm.GroupReports = vSharpStudio.vm.ViewModels.GroupReports.ConvertToVM(m.GroupReports);
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
		    m.GroupProperties = vSharpStudio.vm.ViewModels.GroupProperties.ConvertToProto(vm.GroupProperties);
		    m.GroupSubCatalogs = vSharpStudio.vm.ViewModels.GroupListCatalogs.ConvertToProto(vm.GroupSubCatalogs);
		    m.GroupForms = vSharpStudio.vm.ViewModels.GroupForms.ConvertToProto(vm.GroupForms);
		    m.GroupReports = vSharpStudio.vm.ViewModels.GroupReports.ConvertToProto(vm.GroupReports);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupProperties.Accept(visitor);
			this.GroupSubCatalogs.Accept(visitor);
			this.GroupForms.Accept(visitor);
			this.GroupReports.Accept(visitor);
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
		
		
		public GroupProperties GroupProperties
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging();
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupProperties _GroupProperties;
		partial void OnGroupPropertiesChanging();
		partial void OnGroupPropertiesChanged();
		
		
		public GroupListCatalogs GroupSubCatalogs
		{ 
			set
			{
				if (_GroupSubCatalogs != value)
				{
					OnGroupSubCatalogsChanging();
		            _GroupSubCatalogs = value;
					OnGroupSubCatalogsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupSubCatalogs; }
		}
		private GroupListCatalogs _GroupSubCatalogs;
		partial void OnGroupSubCatalogsChanging();
		partial void OnGroupSubCatalogsChanged();
		
		
		public GroupForms GroupForms
		{ 
			set
			{
				if (_GroupForms != value)
				{
					OnGroupFormsChanging();
		            _GroupForms = value;
					OnGroupFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupForms; }
		}
		private GroupForms _GroupForms;
		partial void OnGroupFormsChanging();
		partial void OnGroupFormsChanged();
		
		
		public GroupReports GroupReports
		{ 
			set
			{
				if (_GroupReports != value)
				{
					OnGroupReportsChanging();
		            _GroupReports = value;
					OnGroupReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupReports; }
		}
		private GroupReports _GroupReports;
		partial void OnGroupReportsChanging();
		partial void OnGroupReportsChanged();
		#endregion Properties
	}
	
	public partial class GroupListCatalogs : ConfigObjectBase<GroupListCatalogs, GroupListCatalogs.GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IAccept
	{
	
		public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { }
		#region CTOR
		public GroupListCatalogs() : base(GroupListCatalogsValidator.Validator)
		{
			this.ListCatalogs = new SortedObservableCollection<Catalog>();
			this.ListCatalogs.CollectionChanged += ListCatalogs_CollectionChanged;
			OnInit();
		}
		public GroupListCatalogs(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListCatalogs.DefaultName, this, this.SubNodes);
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
		public static GroupListCatalogs Clone(ITreeConfigNode parent, GroupListCatalogs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListCatalogs vm = new GroupListCatalogs();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in from.ListCatalogs)
		        vm.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListCatalogs to, GroupListCatalogs from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		public override GroupListCatalogs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListCatalogs.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListCatalogs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListCatalogs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_catalogs' to 'GroupListCatalogs'
		public static GroupListCatalogs ConvertToVM(proto_group_list_catalogs m, GroupListCatalogs vm = null)
		{
		    if (vm == null)
		        vm = new GroupListCatalogs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in m.ListCatalogs)
		        vm.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
		public static proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm)
		{
		    proto_group_list_catalogs m = new proto_group_list_catalogs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		
		
		public SortedObservableCollection<Catalog> ListCatalogs { get; set; }
		partial void OnListCatalogsChanging();
		partial void OnListCatalogsChanged();
		#endregion Properties
	}
	
	public partial class GroupDocuments : ConfigObjectBase<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IAccept
	{
	
		public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { }
		#region CTOR
		public GroupDocuments() : base(GroupDocumentsValidator.Validator)
		{
			this.GroupSharedProperties = new GroupProperties(this);
			this.GroupListDocuments = new GroupListDocuments(this);
			OnInit();
		}
		public GroupDocuments(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupDocuments.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static GroupDocuments Clone(ITreeConfigNode parent, GroupDocuments from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupDocuments vm = new GroupDocuments();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.GroupSharedProperties = vSharpStudio.vm.ViewModels.GroupProperties.Clone(vm, from.GroupSharedProperties, isDeep);
		    if (isDeep)
		        vm.GroupListDocuments = vSharpStudio.vm.ViewModels.GroupListDocuments.Clone(vm, from.GroupListDocuments, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupDocuments to, GroupDocuments from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        GroupProperties.Update(to.GroupSharedProperties, from.GroupSharedProperties, isDeep);
		    if (isDeep)
		        GroupListDocuments.Update(to.GroupListDocuments, from.GroupListDocuments, isDeep);
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.GroupSharedProperties = vSharpStudio.vm.ViewModels.GroupProperties.ConvertToVM(m.GroupSharedProperties);
		    vm.GroupListDocuments = vSharpStudio.vm.ViewModels.GroupListDocuments.ConvertToVM(m.GroupListDocuments);
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.GroupSharedProperties = vSharpStudio.vm.ViewModels.GroupProperties.ConvertToProto(vm.GroupSharedProperties);
		    m.GroupListDocuments = vSharpStudio.vm.ViewModels.GroupListDocuments.ConvertToProto(vm.GroupListDocuments);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupSharedProperties.Accept(visitor);
			this.GroupListDocuments.Accept(visitor);
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
		
		
		public GroupProperties GroupSharedProperties
		{ 
			set
			{
				if (_GroupSharedProperties != value)
				{
					OnGroupSharedPropertiesChanging();
		            _GroupSharedProperties = value;
					OnGroupSharedPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupSharedProperties; }
		}
		private GroupProperties _GroupSharedProperties;
		partial void OnGroupSharedPropertiesChanging();
		partial void OnGroupSharedPropertiesChanged();
		
		
		public GroupListDocuments GroupListDocuments
		{ 
			set
			{
				if (_GroupListDocuments != value)
				{
					OnGroupListDocumentsChanging();
		            _GroupListDocuments = value;
					OnGroupListDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupListDocuments; }
		}
		private GroupListDocuments _GroupListDocuments;
		partial void OnGroupListDocumentsChanging();
		partial void OnGroupListDocumentsChanged();
		#endregion Properties
	}
	
	public partial class Document : ConfigObjectBase<Document, Document.DocumentValidator>, IComparable<Document>, IAccept
	{
	
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document() : base(DocumentValidator.Validator)
		{
			this.GroupPropertiesTree = new GroupPropertiesTree(this);
			this.GroupForms = new GroupForms(this);
			this.GroupReports = new GroupReports(this);
			OnInit();
		}
		public Document(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Document.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Document Clone(ITreeConfigNode parent, Document from, bool isDeep = true, bool isNewGuid = false)
		{
		    Document vm = new Document();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.GroupPropertiesTree = vSharpStudio.vm.ViewModels.GroupPropertiesTree.Clone(vm, from.GroupPropertiesTree, isDeep);
		    if (isDeep)
		        vm.GroupForms = vSharpStudio.vm.ViewModels.GroupForms.Clone(vm, from.GroupForms, isDeep);
		    if (isDeep)
		        vm.GroupReports = vSharpStudio.vm.ViewModels.GroupReports.Clone(vm, from.GroupReports, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Document to, Document from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        GroupPropertiesTree.Update(to.GroupPropertiesTree, from.GroupPropertiesTree, isDeep);
		    if (isDeep)
		        GroupForms.Update(to.GroupForms, from.GroupForms, isDeep);
		    if (isDeep)
		        GroupReports.Update(to.GroupReports, from.GroupReports, isDeep);
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.GroupPropertiesTree = vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToVM(m.GroupPropertiesTree);
		    vm.GroupForms = vSharpStudio.vm.ViewModels.GroupForms.ConvertToVM(m.GroupForms);
		    vm.GroupReports = vSharpStudio.vm.ViewModels.GroupReports.ConvertToVM(m.GroupReports);
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.GroupPropertiesTree = vSharpStudio.vm.ViewModels.GroupPropertiesTree.ConvertToProto(vm.GroupPropertiesTree);
		    m.GroupForms = vSharpStudio.vm.ViewModels.GroupForms.ConvertToProto(vm.GroupForms);
		    m.GroupReports = vSharpStudio.vm.ViewModels.GroupReports.ConvertToProto(vm.GroupReports);
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupPropertiesTree.Accept(visitor);
			this.GroupForms.Accept(visitor);
			this.GroupReports.Accept(visitor);
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
		
		
		public GroupPropertiesTree GroupPropertiesTree
		{ 
			set
			{
				if (_GroupPropertiesTree != value)
				{
					OnGroupPropertiesTreeChanging();
		            _GroupPropertiesTree = value;
					OnGroupPropertiesTreeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesTree; }
		}
		private GroupPropertiesTree _GroupPropertiesTree;
		partial void OnGroupPropertiesTreeChanging();
		partial void OnGroupPropertiesTreeChanged();
		
		
		public GroupForms GroupForms
		{ 
			set
			{
				if (_GroupForms != value)
				{
					OnGroupFormsChanging();
		            _GroupForms = value;
					OnGroupFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupForms; }
		}
		private GroupForms _GroupForms;
		partial void OnGroupFormsChanging();
		partial void OnGroupFormsChanged();
		
		
		public GroupReports GroupReports
		{ 
			set
			{
				if (_GroupReports != value)
				{
					OnGroupReportsChanging();
		            _GroupReports = value;
					OnGroupReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupReports; }
		}
		private GroupReports _GroupReports;
		partial void OnGroupReportsChanging();
		partial void OnGroupReportsChanged();
		#endregion Properties
	}
	
	public partial class GroupListDocuments : ConfigObjectBase<GroupListDocuments, GroupListDocuments.GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IAccept
	{
	
		public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { }
		#region CTOR
		public GroupListDocuments() : base(GroupListDocumentsValidator.Validator)
		{
			this.ListDocuments = new SortedObservableCollection<Document>();
			this.ListDocuments.CollectionChanged += ListDocuments_CollectionChanged;
			OnInit();
		}
		public GroupListDocuments(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListDocuments.DefaultName, this, this.SubNodes);
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
		public static GroupListDocuments Clone(ITreeConfigNode parent, GroupListDocuments from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListDocuments vm = new GroupListDocuments();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in from.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListDocuments to, GroupListDocuments from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		public override GroupListDocuments Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListDocuments.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListDocuments from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListDocuments.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_documents' to 'GroupListDocuments'
		public static GroupListDocuments ConvertToVM(proto_group_list_documents m, GroupListDocuments vm = null)
		{
		    if (vm == null)
		        vm = new GroupListDocuments();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in m.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
		public static proto_group_list_documents ConvertToProto(GroupListDocuments vm)
		{
		    proto_group_list_documents m = new proto_group_list_documents();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
			this.ListJournals = new SortedObservableCollection<Journal>();
			this.ListJournals.CollectionChanged += ListJournals_CollectionChanged;
			OnInit();
		}
		public GroupJournals(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupJournals.DefaultName, this, this.SubNodes);
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListJournals)
		        m.ListJournals.Add(vSharpStudio.vm.ViewModels.Journal.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListJournals)
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
		
		
		public SortedObservableCollection<Journal> ListJournals { get; set; }
		partial void OnListJournalsChanging();
		partial void OnListJournalsChanged();
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
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
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
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
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
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
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
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		
		
		public SortedObservableCollection<Document> ListDocuments { get; set; }
		partial void OnListDocumentsChanging();
		partial void OnListDocumentsChanged();
		#endregion Properties
	}
	
	public partial class GroupForms : ConfigObjectBase<GroupForms, GroupForms.GroupFormsValidator>, IComparable<GroupForms>, IAccept
	{
	
		public partial class GroupFormsValidator : ValidatorBase<GroupForms, GroupFormsValidator> { }
		#region CTOR
		public GroupForms() : base(GroupFormsValidator.Validator)
		{
			this.ListForms = new SortedObservableCollection<Form>();
			this.ListForms.CollectionChanged += ListForms_CollectionChanged;
			OnInit();
		}
		public GroupForms(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupForms.DefaultName, this, this.SubNodes);
	    }
		private void ListForms_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
		    			(t as Form).Parent = this;
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
		    if (type == typeof(Form))
		    {
		        this.ListForms.Sort();
		    }
		}
		public static GroupForms Clone(ITreeConfigNode parent, GroupForms from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupForms vm = new GroupForms();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListForms = new SortedObservableCollection<Form>();
		    foreach(var t in from.ListForms)
		        vm.ListForms.Add(vSharpStudio.vm.ViewModels.Form.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupForms to, GroupForms from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListForms.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListForms)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Form.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListForms.Remove(t);
		        }
		        foreach(var tt in from.ListForms)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListForms.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Form();
		                vSharpStudio.vm.ViewModels.Form.Update(p, tt, isDeep);
		                to.ListForms.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupForms Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupForms.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupForms from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupForms.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_forms' to 'GroupForms'
		public static GroupForms ConvertToVM(proto_group_forms m, GroupForms vm = null)
		{
		    if (vm == null)
		        vm = new GroupForms();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListForms = new SortedObservableCollection<Form>();
		    foreach(var t in m.ListForms)
		        vm.ListForms.Add(vSharpStudio.vm.ViewModels.Form.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupForms' to 'proto_group_forms'
		public static proto_group_forms ConvertToProto(GroupForms vm)
		{
		    proto_group_forms m = new proto_group_forms();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListForms)
		        m.ListForms.Add(vSharpStudio.vm.ViewModels.Form.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListForms)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
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
		
		
		public SortedObservableCollection<Form> ListForms { get; set; }
		partial void OnListFormsChanging();
		partial void OnListFormsChanged();
		#endregion Properties
	}
	
	public partial class Form : ConfigObjectBase<Form, Form.FormValidator>, IComparable<Form>, IAccept
	{
	
		public partial class FormValidator : ValidatorBase<Form, FormValidator> { }
		#region CTOR
		public Form() : base(FormValidator.Validator)
		{
			OnInit();
		}
		public Form(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Form.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Form Clone(ITreeConfigNode parent, Form from, bool isDeep = true, bool isNewGuid = false)
		{
		    Form vm = new Form();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Form to, Form from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		}
		#region IEditable
		public override Form Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Form.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Form from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Form.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_form' to 'Form'
		public static Form ConvertToVM(proto_form m, Form vm = null)
		{
		    if (vm == null)
		        vm = new Form();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Form' to 'proto_form'
		public static proto_form ConvertToProto(Form vm)
		{
		    proto_form m = new proto_form();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		#endregion Properties
	}
	
	public partial class GroupReports : ConfigObjectBase<GroupReports, GroupReports.GroupReportsValidator>, IComparable<GroupReports>, IAccept
	{
	
		public partial class GroupReportsValidator : ValidatorBase<GroupReports, GroupReportsValidator> { }
		#region CTOR
		public GroupReports() : base(GroupReportsValidator.Validator)
		{
			this.ListReports = new SortedObservableCollection<Report>();
			this.ListReports.CollectionChanged += ListReports_CollectionChanged;
			OnInit();
		}
		public GroupReports(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupReports.DefaultName, this, this.SubNodes);
	    }
		private void ListReports_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
		    			(t as Report).Parent = this;
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
		    if (type == typeof(Report))
		    {
		        this.ListReports.Sort();
		    }
		}
		public static GroupReports Clone(ITreeConfigNode parent, GroupReports from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupReports vm = new GroupReports();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListReports = new SortedObservableCollection<Report>();
		    foreach(var t in from.ListReports)
		        vm.ListReports.Add(vSharpStudio.vm.ViewModels.Report.Clone(vm, t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupReports to, GroupReports from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListReports.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListReports)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Report.Update(t, tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListReports.Remove(t);
		        }
		        foreach(var tt in from.ListReports)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListReports.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new Report();
		                vSharpStudio.vm.ViewModels.Report.Update(p, tt, isDeep);
		                to.ListReports.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override GroupReports Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupReports.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupReports from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupReports.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_reports' to 'GroupReports'
		public static GroupReports ConvertToVM(proto_group_reports m, GroupReports vm = null)
		{
		    if (vm == null)
		        vm = new GroupReports();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListReports = new SortedObservableCollection<Report>();
		    foreach(var t in m.ListReports)
		        vm.ListReports.Add(vSharpStudio.vm.ViewModels.Report.ConvertToVM(t));
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupReports' to 'proto_group_reports'
		public static proto_group_reports ConvertToProto(GroupReports vm)
		{
		    proto_group_reports m = new proto_group_reports();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListReports)
		        m.ListReports.Add(vSharpStudio.vm.ViewModels.Report.ConvertToProto(t));
		    return m;
		}
		public void Accept(IVisitorConfig visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListReports)
				t.Accept(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
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
		
		
		public SortedObservableCollection<Report> ListReports { get; set; }
		partial void OnListReportsChanging();
		partial void OnListReportsChanged();
		#endregion Properties
	}
	
	public partial class Report : ConfigObjectBase<Report, Report.ReportValidator>, IComparable<Report>, IAccept
	{
	
		public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { }
		#region CTOR
		public Report() : base(ReportValidator.Validator)
		{
			OnInit();
		}
		public Report(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Report.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Report Clone(ITreeConfigNode parent, Report from, bool isDeep = true, bool isNewGuid = false)
		{
		    Report vm = new Report();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Report to, Report from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		}
		#region IEditable
		public override Report Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Report.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Report from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Report.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_report' to 'Report'
		public static Report ConvertToVM(proto_report m, Report vm = null)
		{
		    if (vm == null)
		        vm = new Report();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Report' to 'proto_report'
		public static proto_report ConvertToProto(Report vm)
		{
		    proto_report m = new proto_report();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
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
		#endregion Properties
	}
	
	public interface IVisitorConfig
	{
	    CancellationToken Token { get; }
		void Visit(GroupConfigs p);
		void VisitEnd(GroupConfigs p);
		void Visit(Config p);
		void VisitEnd(Config p);
		void Visit(ConfigTree p);
		void VisitEnd(ConfigTree p);
		void Visit(DataType p);
		void VisitEnd(DataType p);
		void Visit(ObjectSharedProps p);
		void VisitEnd(ObjectSharedProps p);
		void Visit(GroupPropertyTab p);
		void VisitEnd(GroupPropertyTab p);
		void Visit(GroupPropertyTabs p);
		void VisitEnd(GroupPropertyTabs p);
		void Visit(GroupPropertyTabsTree p);
		void VisitEnd(GroupPropertyTabsTree p);
		void Visit(GroupProperties p);
		void VisitEnd(GroupProperties p);
		void Visit(Property p);
		void VisitEnd(Property p);
		void Visit(GroupPropertiesTree p);
		void VisitEnd(GroupPropertiesTree p);
		void Visit(GroupConstants p);
		void VisitEnd(GroupConstants p);
		void Visit(Constant p);
		void VisitEnd(Constant p);
		void Visit(GroupEnumerations p);
		void VisitEnd(GroupEnumerations p);
		void Visit(Enumeration p);
		void VisitEnd(Enumeration p);
		void Visit(EnumerationPair p);
		void VisitEnd(EnumerationPair p);
		void Visit(GroupCatalogs p);
		void VisitEnd(GroupCatalogs p);
		void Visit(Catalog p);
		void VisitEnd(Catalog p);
		void Visit(GroupListCatalogs p);
		void VisitEnd(GroupListCatalogs p);
		void Visit(GroupDocuments p);
		void VisitEnd(GroupDocuments p);
		void Visit(Document p);
		void VisitEnd(Document p);
		void Visit(GroupListDocuments p);
		void VisitEnd(GroupListDocuments p);
		void Visit(GroupJournals p);
		void VisitEnd(GroupJournals p);
		void Visit(Journal p);
		void VisitEnd(Journal p);
		void Visit(GroupForms p);
		void VisitEnd(GroupForms p);
		void Visit(Form p);
		void VisitEnd(Form p);
		void Visit(GroupReports p);
		void VisitEnd(GroupReports p);
		void Visit(Report p);
		void VisitEnd(Report p);
	}
	
	public interface IVisitorProto
	{
		void Visit(proto_group_configs p);
		void Visit(proto_config p);
		void Visit(proto_config_tree p);
		void Visit(proto_data_type p);
		void Visit(proto_object_shared_props p);
		void Visit(proto_group_property_tab p);
		void Visit(proto_group_property_tabs p);
		void Visit(proto_group_property_tabs_tree p);
		void Visit(proto_group_properties p);
		void Visit(proto_property p);
		void Visit(proto_group_properties_tree p);
		void Visit(proto_group_constants p);
		void Visit(proto_constant p);
		void Visit(proto_group_enumerations p);
		void Visit(proto_enumeration p);
		void Visit(proto_enumeration_pair p);
		void Visit(proto_group_catalogs p);
		void Visit(proto_catalog p);
		void Visit(proto_group_list_catalogs p);
		void Visit(proto_group_documents p);
		void Visit(proto_document p);
		void Visit(proto_group_list_documents p);
		void Visit(proto_group_journals p);
		void Visit(proto_journal p);
		void Visit(proto_group_forms p);
		void Visit(proto_form p);
		void Visit(proto_group_reports p);
		void Visit(proto_report p);
	}
}