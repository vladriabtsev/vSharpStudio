// Auto generated on UTC 11/02/2019 21:37:07
using System;
using System.Linq;
using ViewModelBase;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vSharpStudio.common;
using Google.Protobuf;

namespace vPlugin.DbModel.MsSql // NameSpace.tt Line: 22
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IMsSqlDesignGeneratorSettingsAcceptVisitor // NameSpace.tt Line: 28
    {
        void AcceptMsSqlDesignGeneratorSettingsNodeVisitor(MsSqlDesignGeneratorSettingsVisitor visitor);
    }
	public partial class MsSqlDesignGeneratorSettings : ViewModelValidatableWithSeverity<MsSqlDesignGeneratorSettings, MsSqlDesignGeneratorSettings.MsSqlDesignGeneratorSettingsValidator>, IMsSqlDesignGeneratorSettings // Class.tt Line: 6
	{
		public partial class MsSqlDesignGeneratorSettingsValidator : ValidatorBase<MsSqlDesignGeneratorSettings, MsSqlDesignGeneratorSettingsValidator> { } // Class.tt Line: 8
		#region CTOR
		public MsSqlDesignGeneratorSettings() : base(MsSqlDesignGeneratorSettingsValidator.Validator) // Class.tt Line: 38
		{
			OnInitBegin();
			OnInit();
		}
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static MsSqlDesignGeneratorSettings Clone(ITreeConfigNode parent, MsSqlDesignGeneratorSettings from, bool isDeep = true) // Clone.tt Line: 27
		{
		    MsSqlDesignGeneratorSettings vm = new MsSqlDesignGeneratorSettings();
		    vm.Name = from.Name; // Clone.tt Line: 62
		    vm.Guid = from.Guid; // Clone.tt Line: 62
		    vm.NameUi = from.NameUi; // Clone.tt Line: 62
		    vm.IsUseForeingkey = from.IsUseForeingkey; // Clone.tt Line: 62
		    vm.IsUseForeingkeyIndex = from.IsUseForeingkeyIndex; // Clone.tt Line: 62
		    vm.IsUseStorageProcedures = from.IsUseStorageProcedures; // Clone.tt Line: 62
		    vm.IsUseViews = from.IsUseViews; // Clone.tt Line: 62
		    vm.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered; // Clone.tt Line: 62
		    vm.IsMemoryOptimized = from.IsMemoryOptimized; // Clone.tt Line: 62
		    return vm;
		}
		public static void Update(MsSqlDesignGeneratorSettings to, MsSqlDesignGeneratorSettings from, bool isDeep = true) // Clone.tt Line: 72
		{
		    to.Name = from.Name; // Clone.tt Line: 134
		    to.Guid = from.Guid; // Clone.tt Line: 134
		    to.NameUi = from.NameUi; // Clone.tt Line: 134
		    to.IsUseForeingkey = from.IsUseForeingkey; // Clone.tt Line: 134
		    to.IsUseForeingkeyIndex = from.IsUseForeingkeyIndex; // Clone.tt Line: 134
		    to.IsUseStorageProcedures = from.IsUseStorageProcedures; // Clone.tt Line: 134
		    to.IsUseViews = from.IsUseViews; // Clone.tt Line: 134
		    to.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered; // Clone.tt Line: 134
		    to.IsMemoryOptimized = from.IsMemoryOptimized; // Clone.tt Line: 134
		}
		// Clone.tt Line: 140
		#region IEditable
		public override MsSqlDesignGeneratorSettings Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return MsSqlDesignGeneratorSettings.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(MsSqlDesignGeneratorSettings from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    MsSqlDesignGeneratorSettings.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_ms_sql_design_generator_settings' to 'MsSqlDesignGeneratorSettings'
		public static MsSqlDesignGeneratorSettings ConvertToVM(Proto.Config.Connection.proto_ms_sql_design_generator_settings m, MsSqlDesignGeneratorSettings vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.IsUseForeingkey = m.IsUseForeingkey; // Clone.tt Line: 216
		    vm.IsUseForeingkeyIndex = m.IsUseForeingkeyIndex; // Clone.tt Line: 216
		    vm.IsUseStorageProcedures = m.IsUseStorageProcedures; // Clone.tt Line: 216
		    vm.IsUseViews = m.IsUseViews; // Clone.tt Line: 216
		    vm.IsPrimaryKeyClustered = m.IsPrimaryKeyClustered; // Clone.tt Line: 216
		    vm.IsMemoryOptimized = m.IsMemoryOptimized; // Clone.tt Line: 216
		    return vm;
		}
		// Conversion from 'MsSqlDesignGeneratorSettings' to 'proto_ms_sql_design_generator_settings'
		public static Proto.Config.Connection.proto_ms_sql_design_generator_settings ConvertToProto(MsSqlDesignGeneratorSettings vm) // Clone.tt Line: 226
		{
		    Proto.Config.Connection.proto_ms_sql_design_generator_settings m = new Proto.Config.Connection.proto_ms_sql_design_generator_settings(); // Clone.tt Line: 228
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.IsUseForeingkey = vm.IsUseForeingkey; // Clone.tt Line: 252
		    m.IsUseForeingkeyIndex = vm.IsUseForeingkeyIndex; // Clone.tt Line: 252
		    m.IsUseStorageProcedures = vm.IsUseStorageProcedures; // Clone.tt Line: 252
		    m.IsUseViews = vm.IsUseViews; // Clone.tt Line: 252
		    m.IsPrimaryKeyClustered = vm.IsPrimaryKeyClustered; // Clone.tt Line: 252
		    m.IsMemoryOptimized = vm.IsMemoryOptimized; // Clone.tt Line: 252
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		public string Name // Property.tt Line: 123
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging(_Name, value);
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnNameChanged();
		
		public string Guid // Property.tt Line: 123
		{ 
			set
			{
				if (_Guid != value)
				{
					OnGuidChanging(_Guid, value);
					_Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Guid; }
		}
		private string _Guid = "";
		partial void OnGuidChanging(string from, string to); // Property.tt Line: 141
		partial void OnGuidChanged();
		
		[Editable(false)]
		[DisplayName("UI name")]
		public string NameUi // Property.tt Line: 123
		{ 
			set
			{
				if (_NameUi != value)
				{
					OnNameUiChanging(_NameUi, value);
					_NameUi = value;
					OnNameUiChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NameUi; }
		}
		private string _NameUi = "";
		partial void OnNameUiChanging(string from, string to); // Property.tt Line: 141
		partial void OnNameUiChanged();
		
		public bool IsUseForeingkey // Property.tt Line: 123
		{ 
			set
			{
				if (_IsUseForeingkey != value)
				{
					OnIsUseForeingkeyChanging(_IsUseForeingkey, value);
					_IsUseForeingkey = value;
					OnIsUseForeingkeyChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsUseForeingkey; }
		}
		private bool _IsUseForeingkey;
		partial void OnIsUseForeingkeyChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsUseForeingkeyChanged();
		
		public bool IsUseForeingkeyIndex // Property.tt Line: 123
		{ 
			set
			{
				if (_IsUseForeingkeyIndex != value)
				{
					OnIsUseForeingkeyIndexChanging(_IsUseForeingkeyIndex, value);
					_IsUseForeingkeyIndex = value;
					OnIsUseForeingkeyIndexChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsUseForeingkeyIndex; }
		}
		private bool _IsUseForeingkeyIndex;
		partial void OnIsUseForeingkeyIndexChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsUseForeingkeyIndexChanged();
		
		public bool IsUseStorageProcedures // Property.tt Line: 123
		{ 
			set
			{
				if (_IsUseStorageProcedures != value)
				{
					OnIsUseStorageProceduresChanging(_IsUseStorageProcedures, value);
					_IsUseStorageProcedures = value;
					OnIsUseStorageProceduresChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsUseStorageProcedures; }
		}
		private bool _IsUseStorageProcedures;
		partial void OnIsUseStorageProceduresChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsUseStorageProceduresChanged();
		
		public bool IsUseViews // Property.tt Line: 123
		{ 
			set
			{
				if (_IsUseViews != value)
				{
					OnIsUseViewsChanging(_IsUseViews, value);
					_IsUseViews = value;
					OnIsUseViewsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsUseViews; }
		}
		private bool _IsUseViews;
		partial void OnIsUseViewsChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsUseViewsChanged();
		
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		public bool IsPrimaryKeyClustered // Property.tt Line: 123
		{ 
			set
			{
				if (_IsPrimaryKeyClustered != value)
				{
					OnIsPrimaryKeyClusteredChanging(_IsPrimaryKeyClustered, value);
					_IsPrimaryKeyClustered = value;
					OnIsPrimaryKeyClusteredChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPrimaryKeyClustered; }
		}
		private bool _IsPrimaryKeyClustered;
		partial void OnIsPrimaryKeyClusteredChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsPrimaryKeyClusteredChanged();
		
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		public bool IsMemoryOptimized // Property.tt Line: 123
		{ 
			set
			{
				if (_IsMemoryOptimized != value)
				{
					OnIsMemoryOptimizedChanging(_IsMemoryOptimized, value);
					_IsMemoryOptimized = value;
					OnIsMemoryOptimizedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsMemoryOptimized; }
		}
		private bool _IsMemoryOptimized;
		partial void OnIsMemoryOptimizedChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsMemoryOptimizedChanged();
	
		#endregion Properties
	}
	[CategoryOrder("Source", 1)]
	[CategoryOrder("Security", 2)]
	[CategoryOrder("Pooling", 3)]
	[CategoryOrder("Initialization", 4)]
	[CategoryOrder("ConnectionResilency", 5)]
	[CategoryOrder("Advanced", 6)]
	public partial class MsSqlConnectionSettings : ViewModelValidatableWithSeverity<MsSqlConnectionSettings, MsSqlConnectionSettings.MsSqlConnectionSettingsValidator>, IMsSqlConnectionSettings // Class.tt Line: 6
	{
		public partial class MsSqlConnectionSettingsValidator : ValidatorBase<MsSqlConnectionSettings, MsSqlConnectionSettingsValidator> { } // Class.tt Line: 8
		#region CTOR
		public MsSqlConnectionSettings() : base(MsSqlConnectionSettingsValidator.Validator) // Class.tt Line: 38
		{
			OnInitBegin();
			OnInit();
		}
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static MsSqlConnectionSettings Clone(ITreeConfigNode parent, MsSqlConnectionSettings from, bool isDeep = true) // Clone.tt Line: 27
		{
		    MsSqlConnectionSettings vm = new MsSqlConnectionSettings();
		    vm.Name = from.Name; // Clone.tt Line: 62
		    vm.Guid = from.Guid; // Clone.tt Line: 62
		    vm.MaxPoolSize = from.MaxPoolSize; // Clone.tt Line: 62
		    vm.ConnectRetryCount = from.ConnectRetryCount; // Clone.tt Line: 62
		    vm.ConnectRetryInterval = from.ConnectRetryInterval; // Clone.tt Line: 62
		    vm.MinPoolSize = from.MinPoolSize; // Clone.tt Line: 62
		    vm.MultipleActiveResultSets = from.MultipleActiveResultSets; // Clone.tt Line: 62
		    vm.MultiSubnetFailover = from.MultiSubnetFailover; // Clone.tt Line: 62
		    vm.TransparentNetworkIPResolution = from.TransparentNetworkIPResolution; // Clone.tt Line: 62
		    vm.NetworkLibrary = from.NetworkLibrary; // Clone.tt Line: 62
		    vm.PacketSize = from.PacketSize; // Clone.tt Line: 62
		    vm.PersistSecurityInfo = from.PersistSecurityInfo; // Clone.tt Line: 62
		    vm.LoadBalanceTimeout = from.LoadBalanceTimeout; // Clone.tt Line: 62
		    vm.Pooling = from.Pooling; // Clone.tt Line: 62
		    vm.Replication = from.Replication; // Clone.tt Line: 62
		    vm.TransactionBinding = from.TransactionBinding; // Clone.tt Line: 62
		    vm.TypeSystemVersion = from.TypeSystemVersion; // Clone.tt Line: 62
		    vm.UserID = from.UserID; // Clone.tt Line: 62
		    vm.UserInstance = from.UserInstance; // Clone.tt Line: 62
		    vm.WorkstationID = from.WorkstationID; // Clone.tt Line: 62
		    vm.Password = from.Password; // Clone.tt Line: 62
		    vm.Authentication = from.Authentication; // Clone.tt Line: 62
		    vm.InitialCatalog = from.InitialCatalog; // Clone.tt Line: 62
		    vm.ApplicationIntentValue = from.ApplicationIntentValue; // Clone.tt Line: 62
		    vm.ApplicationName = from.ApplicationName; // Clone.tt Line: 62
		    vm.AsynchronousProcessing = from.AsynchronousProcessing; // Clone.tt Line: 62
		    vm.IntegratedSecurity = from.IntegratedSecurity; // Clone.tt Line: 62
		    vm.ContextConnection = from.ContextConnection; // Clone.tt Line: 62
		    vm.ConnectTimeout = from.ConnectTimeout; // Clone.tt Line: 62
		    vm.AttachDBFilename = from.AttachDBFilename; // Clone.tt Line: 62
		    vm.DataSource = from.DataSource; // Clone.tt Line: 62
		    vm.Encrypt = from.Encrypt; // Clone.tt Line: 62
		    vm.ColumnEncryptionSetting = from.ColumnEncryptionSetting; // Clone.tt Line: 62
		    vm.TrustServerCertificate = from.TrustServerCertificate; // Clone.tt Line: 62
		    vm.Enlist = from.Enlist; // Clone.tt Line: 62
		    vm.FailoverPartner = from.FailoverPartner; // Clone.tt Line: 62
		    vm.CurrentLanguage = from.CurrentLanguage; // Clone.tt Line: 62
		    return vm;
		}
		public static void Update(MsSqlConnectionSettings to, MsSqlConnectionSettings from, bool isDeep = true) // Clone.tt Line: 72
		{
		    to.Name = from.Name; // Clone.tt Line: 134
		    to.Guid = from.Guid; // Clone.tt Line: 134
		    to.MaxPoolSize = from.MaxPoolSize; // Clone.tt Line: 134
		    to.ConnectRetryCount = from.ConnectRetryCount; // Clone.tt Line: 134
		    to.ConnectRetryInterval = from.ConnectRetryInterval; // Clone.tt Line: 134
		    to.MinPoolSize = from.MinPoolSize; // Clone.tt Line: 134
		    to.MultipleActiveResultSets = from.MultipleActiveResultSets; // Clone.tt Line: 134
		    to.MultiSubnetFailover = from.MultiSubnetFailover; // Clone.tt Line: 134
		    to.TransparentNetworkIPResolution = from.TransparentNetworkIPResolution; // Clone.tt Line: 134
		    to.NetworkLibrary = from.NetworkLibrary; // Clone.tt Line: 134
		    to.PacketSize = from.PacketSize; // Clone.tt Line: 134
		    to.PersistSecurityInfo = from.PersistSecurityInfo; // Clone.tt Line: 134
		    to.LoadBalanceTimeout = from.LoadBalanceTimeout; // Clone.tt Line: 134
		    to.Pooling = from.Pooling; // Clone.tt Line: 134
		    to.Replication = from.Replication; // Clone.tt Line: 134
		    to.TransactionBinding = from.TransactionBinding; // Clone.tt Line: 134
		    to.TypeSystemVersion = from.TypeSystemVersion; // Clone.tt Line: 134
		    to.UserID = from.UserID; // Clone.tt Line: 134
		    to.UserInstance = from.UserInstance; // Clone.tt Line: 134
		    to.WorkstationID = from.WorkstationID; // Clone.tt Line: 134
		    to.Password = from.Password; // Clone.tt Line: 134
		    to.Authentication = from.Authentication; // Clone.tt Line: 134
		    to.InitialCatalog = from.InitialCatalog; // Clone.tt Line: 134
		    to.ApplicationIntentValue = from.ApplicationIntentValue; // Clone.tt Line: 134
		    to.ApplicationName = from.ApplicationName; // Clone.tt Line: 134
		    to.AsynchronousProcessing = from.AsynchronousProcessing; // Clone.tt Line: 134
		    to.IntegratedSecurity = from.IntegratedSecurity; // Clone.tt Line: 134
		    to.ContextConnection = from.ContextConnection; // Clone.tt Line: 134
		    to.ConnectTimeout = from.ConnectTimeout; // Clone.tt Line: 134
		    to.AttachDBFilename = from.AttachDBFilename; // Clone.tt Line: 134
		    to.DataSource = from.DataSource; // Clone.tt Line: 134
		    to.Encrypt = from.Encrypt; // Clone.tt Line: 134
		    to.ColumnEncryptionSetting = from.ColumnEncryptionSetting; // Clone.tt Line: 134
		    to.TrustServerCertificate = from.TrustServerCertificate; // Clone.tt Line: 134
		    to.Enlist = from.Enlist; // Clone.tt Line: 134
		    to.FailoverPartner = from.FailoverPartner; // Clone.tt Line: 134
		    to.CurrentLanguage = from.CurrentLanguage; // Clone.tt Line: 134
		}
		// Clone.tt Line: 140
		#region IEditable
		public override MsSqlConnectionSettings Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return MsSqlConnectionSettings.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(MsSqlConnectionSettings from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    MsSqlConnectionSettings.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_ms_sql_connection_settings' to 'MsSqlConnectionSettings'
		public static MsSqlConnectionSettings ConvertToVM(Proto.Config.Connection.proto_ms_sql_connection_settings m, MsSqlConnectionSettings vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.MaxPoolSize = m.MaxPoolSize; // Clone.tt Line: 216
		    vm.ConnectRetryCount = m.ConnectRetryCount; // Clone.tt Line: 216
		    vm.ConnectRetryInterval = m.ConnectRetryInterval; // Clone.tt Line: 216
		    vm.MinPoolSize = m.MinPoolSize; // Clone.tt Line: 216
		    vm.MultipleActiveResultSets = m.MultipleActiveResultSets; // Clone.tt Line: 216
		    vm.MultiSubnetFailover = m.MultiSubnetFailover; // Clone.tt Line: 216
		    vm.TransparentNetworkIPResolution = m.TransparentNetworkIPResolution; // Clone.tt Line: 216
		    vm.NetworkLibrary = m.NetworkLibrary; // Clone.tt Line: 216
		    vm.PacketSize = m.PacketSize; // Clone.tt Line: 216
		    vm.PersistSecurityInfo = m.PersistSecurityInfo; // Clone.tt Line: 216
		    vm.LoadBalanceTimeout = m.LoadBalanceTimeout; // Clone.tt Line: 216
		    vm.Pooling = m.Pooling; // Clone.tt Line: 216
		    vm.Replication = m.Replication; // Clone.tt Line: 216
		    vm.TransactionBinding = m.TransactionBinding; // Clone.tt Line: 216
		    vm.TypeSystemVersion = m.TypeSystemVersion; // Clone.tt Line: 216
		    vm.UserID = m.UserID; // Clone.tt Line: 216
		    vm.UserInstance = m.UserInstance; // Clone.tt Line: 216
		    vm.WorkstationID = m.WorkstationID; // Clone.tt Line: 216
		    vm.Password = m.Password; // Clone.tt Line: 216
		    vm.Authentication = (SqlAuthenticationMethod)m.Authentication; // Clone.tt Line: 214
		    vm.InitialCatalog = m.InitialCatalog; // Clone.tt Line: 216
		    vm.ApplicationIntentValue = (ApplicationIntent)m.ApplicationIntentValue; // Clone.tt Line: 214
		    vm.ApplicationName = m.ApplicationName; // Clone.tt Line: 216
		    vm.AsynchronousProcessing = m.AsynchronousProcessing; // Clone.tt Line: 216
		    vm.IntegratedSecurity = m.IntegratedSecurity; // Clone.tt Line: 216
		    vm.ContextConnection = m.ContextConnection; // Clone.tt Line: 216
		    vm.ConnectTimeout = m.ConnectTimeout; // Clone.tt Line: 216
		    vm.AttachDBFilename = m.AttachDBFilename; // Clone.tt Line: 216
		    vm.DataSource = m.DataSource; // Clone.tt Line: 216
		    vm.Encrypt = m.Encrypt; // Clone.tt Line: 216
		    vm.ColumnEncryptionSetting = (SqlConnectionColumnEncryptionSetting)m.ColumnEncryptionSetting; // Clone.tt Line: 214
		    vm.TrustServerCertificate = m.TrustServerCertificate; // Clone.tt Line: 216
		    vm.Enlist = m.Enlist; // Clone.tt Line: 216
		    vm.FailoverPartner = m.FailoverPartner; // Clone.tt Line: 216
		    vm.CurrentLanguage = m.CurrentLanguage; // Clone.tt Line: 216
		    return vm;
		}
		// Conversion from 'MsSqlConnectionSettings' to 'proto_ms_sql_connection_settings'
		public static Proto.Config.Connection.proto_ms_sql_connection_settings ConvertToProto(MsSqlConnectionSettings vm) // Clone.tt Line: 226
		{
		    Proto.Config.Connection.proto_ms_sql_connection_settings m = new Proto.Config.Connection.proto_ms_sql_connection_settings(); // Clone.tt Line: 228
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.MaxPoolSize = vm.MaxPoolSize; // Clone.tt Line: 252
		    m.ConnectRetryCount = vm.ConnectRetryCount; // Clone.tt Line: 252
		    m.ConnectRetryInterval = vm.ConnectRetryInterval; // Clone.tt Line: 252
		    m.MinPoolSize = vm.MinPoolSize; // Clone.tt Line: 252
		    m.MultipleActiveResultSets = vm.MultipleActiveResultSets; // Clone.tt Line: 252
		    m.MultiSubnetFailover = vm.MultiSubnetFailover; // Clone.tt Line: 252
		    m.TransparentNetworkIPResolution = vm.TransparentNetworkIPResolution; // Clone.tt Line: 252
		    m.NetworkLibrary = vm.NetworkLibrary; // Clone.tt Line: 252
		    m.PacketSize = vm.PacketSize; // Clone.tt Line: 252
		    m.PersistSecurityInfo = vm.PersistSecurityInfo; // Clone.tt Line: 252
		    m.LoadBalanceTimeout = vm.LoadBalanceTimeout; // Clone.tt Line: 252
		    m.Pooling = vm.Pooling; // Clone.tt Line: 252
		    m.Replication = vm.Replication; // Clone.tt Line: 252
		    m.TransactionBinding = vm.TransactionBinding; // Clone.tt Line: 252
		    m.TypeSystemVersion = vm.TypeSystemVersion; // Clone.tt Line: 252
		    m.UserID = vm.UserID; // Clone.tt Line: 252
		    m.UserInstance = vm.UserInstance; // Clone.tt Line: 252
		    m.WorkstationID = vm.WorkstationID; // Clone.tt Line: 252
		    m.Password = vm.Password; // Clone.tt Line: 252
		    m.Authentication = (Proto.Config.Connection.SqlAuthenticationMethod)vm.Authentication; // Clone.tt Line: 250
		    m.InitialCatalog = vm.InitialCatalog; // Clone.tt Line: 252
		    m.ApplicationIntentValue = (Proto.Config.Connection.ApplicationIntent)vm.ApplicationIntentValue; // Clone.tt Line: 250
		    m.ApplicationName = vm.ApplicationName; // Clone.tt Line: 252
		    m.AsynchronousProcessing = vm.AsynchronousProcessing; // Clone.tt Line: 252
		    m.IntegratedSecurity = vm.IntegratedSecurity; // Clone.tt Line: 252
		    m.ContextConnection = vm.ContextConnection; // Clone.tt Line: 252
		    m.ConnectTimeout = vm.ConnectTimeout; // Clone.tt Line: 252
		    m.AttachDBFilename = vm.AttachDBFilename; // Clone.tt Line: 252
		    m.DataSource = vm.DataSource; // Clone.tt Line: 252
		    m.Encrypt = vm.Encrypt; // Clone.tt Line: 252
		    m.ColumnEncryptionSetting = (Proto.Config.Connection.SqlConnectionColumnEncryptionSetting)vm.ColumnEncryptionSetting; // Clone.tt Line: 250
		    m.TrustServerCertificate = vm.TrustServerCertificate; // Clone.tt Line: 252
		    m.Enlist = vm.Enlist; // Clone.tt Line: 252
		    m.FailoverPartner = vm.FailoverPartner; // Clone.tt Line: 252
		    m.CurrentLanguage = vm.CurrentLanguage; // Clone.tt Line: 252
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		public string Name // Property.tt Line: 123
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging(_Name, value);
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnNameChanged();
		
		public string Guid // Property.tt Line: 123
		{ 
			set
			{
				if (_Guid != value)
				{
					OnGuidChanging(_Guid, value);
					_Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Guid; }
		}
		private string _Guid = "";
		partial void OnGuidChanging(string from, string to); // Property.tt Line: 141
		partial void OnGuidChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the maximum number of connections allowed in the connection pool
		///     for this specific connection string.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MaxPoolSize
		///     property, or 100 if none has been supplied.
		///////////////////////////////////////////////////
		[Description("The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MaxPoolSize property, or 100 if none has been supplied.")]
		[Category("Pooling")]
		public int MaxPoolSize // Property.tt Line: 123
		{ 
			set
			{
				if (_MaxPoolSize != value)
				{
					OnMaxPoolSizeChanging(_MaxPoolSize, value);
					_MaxPoolSize = value;
					OnMaxPoolSizeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MaxPoolSize; }
		}
		private int _MaxPoolSize;
		partial void OnMaxPoolSizeChanging(int from, int to); // Property.tt Line: 141
		partial void OnMaxPoolSizeChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     [Supported in the .NET Framework 4.5.1 and later versions] The number of reconnections
		///     attempted after identifying that there was an idle connection failure. This must
		///     be an integer between 0 and 255. Default is 1. Set to 0 to disable reconnecting
		///     on idle connection failures. An System.ArgumentException will be thrown if set
		///     to a value outside of the allowed range.
		/// 
		/// Returns:
		///     The number of reconnections attempted after identifying that there was an idle
		///     connection failure.
		///////////////////////////////////////////////////
		[Description(".NET Framework 4.5.1 and later versions. The number of reconnections attempted after identifying that there was an idle connection failure. This must be an integer between 0 and 255. Default is 1. Set to 0 to disable reconnecting on idle connection failures.")]
		[Category("ConnectionResilency")]
		public int ConnectRetryCount // Property.tt Line: 123
		{ 
			set
			{
				if (_ConnectRetryCount != value)
				{
					OnConnectRetryCountChanging(_ConnectRetryCount, value);
					_ConnectRetryCount = value;
					OnConnectRetryCountChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectRetryCount; }
		}
		private int _ConnectRetryCount;
		partial void OnConnectRetryCountChanging(int from, int to); // Property.tt Line: 141
		partial void OnConnectRetryCountChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     [Supported in the .NET Framework 4.5.1 and later versions] Amount of time (in
		///     seconds) between each reconnection attempt after identifying that there was an
		///     idle connection failure. This must be an integer between 1 and 60. The default
		///     is 10 seconds. An System.ArgumentException will be thrown if set to a value outside
		///     of the allowed range.
		/// 
		/// Returns:
		///     Amount of time (in seconds) between each reconnection attempt after identifying
		///     that there was an idle connection failure.
		///////////////////////////////////////////////////
		[Description(".NET Framework 4.5.1 and later versions. Amount of time (in seconds) between each reconnection attempt after identifying that there was an idle connection failure. This must be an integer between 1 and 60. The default is 10 seconds.")]
		[Category("ConnectionResilency")]
		public int ConnectRetryInterval // Property.tt Line: 123
		{ 
			set
			{
				if (_ConnectRetryInterval != value)
				{
					OnConnectRetryIntervalChanging(_ConnectRetryInterval, value);
					_ConnectRetryInterval = value;
					OnConnectRetryIntervalChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectRetryInterval; }
		}
		private int _ConnectRetryInterval;
		partial void OnConnectRetryIntervalChanging(int from, int to); // Property.tt Line: 141
		partial void OnConnectRetryIntervalChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the minimum number of connections allowed in the connection pool
		///     for this specific connection string.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MinPoolSize
		///     property, or 0 if none has been supplied.
		///////////////////////////////////////////////////
		[Description("The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MinPoolSize property, or 0 if none has been supplied.")]
		[Category("Pooling")]
		public int MinPoolSize // Property.tt Line: 123
		{ 
			set
			{
				if (_MinPoolSize != value)
				{
					OnMinPoolSizeChanging(_MinPoolSize, value);
					_MinPoolSize = value;
					OnMinPoolSizeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MinPoolSize; }
		}
		private int _MinPoolSize;
		partial void OnMinPoolSizeChanging(int from, int to); // Property.tt Line: 141
		partial void OnMinPoolSizeChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     When true, an application can maintain multiple active result sets (MARS). When
		///     false, an application must process or cancel all result sets from one batch before
		///     it can execute any other batch on that connection.For more information, see Multiple
		///     Active Result Sets (MARS).
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MultipleActiveResultSets
		///     property, or false if none has been supplied.
		///////////////////////////////////////////////////
		[Description("When true, an application can maintain multiple active result sets (MARS). When false, an application must process or cancel all result sets from one batch before it can execute any other batch on that connection.")]
		[CategoryAttribute("Advanced")]
		public bool MultipleActiveResultSets // Property.tt Line: 123
		{ 
			set
			{
				if (_MultipleActiveResultSets != value)
				{
					OnMultipleActiveResultSetsChanging(_MultipleActiveResultSets, value);
					_MultipleActiveResultSets = value;
					OnMultipleActiveResultSetsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MultipleActiveResultSets; }
		}
		private bool _MultipleActiveResultSets;
		partial void OnMultipleActiveResultSetsChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnMultipleActiveResultSetsChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     If your application is connecting to an AlwaysOn availability group (AG) on different
		///     subnets, setting MultiSubnetFailover=true provides faster detection of and connection
		///     to the (currently) active server. For more information about SqlClient support
		///     for Always On Availability Groups, see SqlClient Support for High Availability,
		///     Disaster Recovery.
		/// 
		/// Returns:
		///     Returns System.Boolean indicating the current value of the property.
		///////////////////////////////////////////////////
		[Description("If your application is connecting to an AlwaysOn availability group (AG) on different subnets, setting MultiSubnetFailover=true provides faster detection of and connection to the (currently) active server. For more information about SqlClient support for Always On Availability Groups, see SqlClient Support for High Availability, Disaster Recovery.")]
		[Category("Source")]
		public bool MultiSubnetFailover // Property.tt Line: 123
		{ 
			set
			{
				if (_MultiSubnetFailover != value)
				{
					OnMultiSubnetFailoverChanging(_MultiSubnetFailover, value);
					_MultiSubnetFailover = value;
					OnMultiSubnetFailoverChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MultiSubnetFailover; }
		}
		private bool _MultiSubnetFailover;
		partial void OnMultiSubnetFailoverChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnMultiSubnetFailoverChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     When the value of this key is set to true, the application is required to retrieve
		///     all IP addresses for a particular DNS entry and attempt to connect with the first
		///     one in the list. If the connection is not established within 0.5 seconds, the
		///     application will try to connect to all others in parallel. When the first answers,
		///     the application will establish the connection with the respondent IP address.
		/// 
		/// Returns:
		///     A boolean value.
		///////////////////////////////////////////////////
		[Description("When the value of this key is set to true, the application is required to retrieve all IP addresses for a particular DNS entry and attempt to connect with the first one in the list. If the connection is not established within 0.5 seconds, the application will try to connect to all others in parallel. When the first answers, the application will establish the connection with the respondent IP address.")]
		[Category("Source")]
		public bool TransparentNetworkIPResolution // Property.tt Line: 123
		{ 
			set
			{
				if (_TransparentNetworkIPResolution != value)
				{
					OnTransparentNetworkIPResolutionChanging(_TransparentNetworkIPResolution, value);
					_TransparentNetworkIPResolution = value;
					OnTransparentNetworkIPResolutionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TransparentNetworkIPResolution; }
		}
		private bool _TransparentNetworkIPResolution;
		partial void OnTransparentNetworkIPResolutionChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnTransparentNetworkIPResolutionChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a string that contains the name of the network library used to establish
		///     a connection to the SQL Server.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.NetworkLibrary
		///     property, or String.Empty if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Category("Advanced")]
		public string NetworkLibrary // Property.tt Line: 123
		{ 
			set
			{
				if (_NetworkLibrary != value)
				{
					OnNetworkLibraryChanging(_NetworkLibrary, value);
					_NetworkLibrary = value;
					OnNetworkLibraryChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NetworkLibrary; }
		}
		private string _NetworkLibrary = "";
		partial void OnNetworkLibraryChanging(string from, string to); // Property.tt Line: 141
		partial void OnNetworkLibraryChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the size in bytes of the network packets used to communicate with
		///     an instance of SQL Server.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.PacketSize
		///     property, or 8000 if none has been supplied.
		///////////////////////////////////////////////////
		[Description("The value of the System.Data.SqlClient.SqlConnectionStringBuilder.PacketSize property, or 8000 if none has been supplied.")]
		[Category("Advanced")]
		public int PacketSize // Property.tt Line: 123
		{ 
			set
			{
				if (_PacketSize != value)
				{
					OnPacketSizeChanging(_PacketSize, value);
					_PacketSize = value;
					OnPacketSizeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PacketSize; }
		}
		private int _PacketSize;
		partial void OnPacketSizeChanging(int from, int to); // Property.tt Line: 141
		partial void OnPacketSizeChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates if security-sensitive information,
		///     such as the password, is not returned as part of the connection if the connection
		///     is open or has ever been in an open state.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.PersistSecurityInfo
		///     property, or false if none has been supplied.
		///////////////////////////////////////////////////
		[Description("Gets or sets a Boolean value that indicates if security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.")]
		[Category("Security")]
		public bool PersistSecurityInfo // Property.tt Line: 123
		{ 
			set
			{
				if (_PersistSecurityInfo != value)
				{
					OnPersistSecurityInfoChanging(_PersistSecurityInfo, value);
					_PersistSecurityInfo = value;
					OnPersistSecurityInfoChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PersistSecurityInfo; }
		}
		private bool _PersistSecurityInfo;
		partial void OnPersistSecurityInfoChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnPersistSecurityInfoChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the minimum time, in seconds, for the connection to live in the
		///     connection pool before being destroyed.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.LoadBalanceTimeout
		///     property, or 0 if none has been supplied.
		///////////////////////////////////////////////////
		[Description("Gets or sets the minimum time, in seconds, for the connection to live in the connection pool before being destroyed.")]
		[Category("Pooling")]
		public int LoadBalanceTimeout // Property.tt Line: 123
		{ 
			set
			{
				if (_LoadBalanceTimeout != value)
				{
					OnLoadBalanceTimeoutChanging(_LoadBalanceTimeout, value);
					_LoadBalanceTimeout = value;
					OnLoadBalanceTimeoutChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _LoadBalanceTimeout; }
		}
		private int _LoadBalanceTimeout;
		partial void OnLoadBalanceTimeoutChanging(int from, int to); // Property.tt Line: 141
		partial void OnLoadBalanceTimeoutChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether the connection will be pooled
		///     or explicitly opened every time that the connection is requested.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Pooling property,
		///     or true if none has been supplied.
		///////////////////////////////////////////////////
		[Description("Gets or sets a Boolean value that indicates whether the connection will be pooled or explicitly opened every time that the connection is requested.")]
		[Category("Pooling")]
		public bool Pooling // Property.tt Line: 123
		{ 
			set
			{
				if (_Pooling != value)
				{
					OnPoolingChanging(_Pooling, value);
					_Pooling = value;
					OnPoolingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Pooling; }
		}
		private bool _Pooling;
		partial void OnPoolingChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnPoolingChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether replication is supported
		///     using the connection.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Replication
		///     property, or false if none has been supplied.
		///////////////////////////////////////////////////
		[Description("System.Data.SqlClient.SqlConnectionStringBuilder.Replication")]
		[Category("Replication")]
		public bool Replication // Property.tt Line: 123
		{ 
			set
			{
				if (_Replication != value)
				{
					OnReplicationChanging(_Replication, value);
					_Replication = value;
					OnReplicationChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Replication; }
		}
		private bool _Replication;
		partial void OnReplicationChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnReplicationChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a string value that indicates how the connection maintains its association
		///     with an enlisted System.Transactions transaction.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.TransactionBinding
		///     property, or String.Empty if none has been supplied.
		///////////////////////////////////////////////////
		[Description("System.Data.SqlClient.SqlConnectionStringBuilder.TransactionBinding")]
		[Category("Advanced")]
		public string TransactionBinding // Property.tt Line: 123
		{ 
			set
			{
				if (_TransactionBinding != value)
				{
					OnTransactionBindingChanging(_TransactionBinding, value);
					_TransactionBinding = value;
					OnTransactionBindingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TransactionBinding; }
		}
		private string _TransactionBinding = "";
		partial void OnTransactionBindingChanging(string from, string to); // Property.tt Line: 141
		partial void OnTransactionBindingChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a string value that indicates the type system the application expects.
		/// 
		/// Returns:
		///     The following table shows the possible values for the System.Data.SqlClient.SqlConnectionStringBuilder.TypeSystemVersion
		///     property:ValueDescription SQL Server 2005Uses the SQL Server 2005 type system.
		///     No conversions are made for the current version of ADO.NET.SQL Server 2008Uses
		///     the SQL Server 2008 type system.LatestUse the latest version than this client-server
		///     pair can handle. This will automatically move forward as the client and server
		///     components are upgraded.
		///////////////////////////////////////////////////
		[Category("Advanced")]
		public string TypeSystemVersion // Property.tt Line: 123
		{ 
			set
			{
				if (_TypeSystemVersion != value)
				{
					OnTypeSystemVersionChanging(_TypeSystemVersion, value);
					_TypeSystemVersion = value;
					OnTypeSystemVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TypeSystemVersion; }
		}
		private string _TypeSystemVersion = "";
		partial void OnTypeSystemVersionChanging(string from, string to); // Property.tt Line: 141
		partial void OnTypeSystemVersionChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the user ID to be used when connecting to SQL Server.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.UserID property,
		///     or String.Empty if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Category("Security")]
		public string UserID // Property.tt Line: 123
		{ 
			set
			{
				if (_UserID != value)
				{
					OnUserIDChanging(_UserID, value);
					_UserID = value;
					OnUserIDChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _UserID; }
		}
		private string _UserID = "";
		partial void OnUserIDChanging(string from, string to); // Property.tt Line: 141
		partial void OnUserIDChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a value that indicates whether to redirect the connection from the
		///     default SQL Server Express instance to a runtime-initiated instance running under
		///     the account of the caller.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.UserInstance
		///     property, or False if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Category("Source")]
		public bool UserInstance // Property.tt Line: 123
		{ 
			set
			{
				if (_UserInstance != value)
				{
					OnUserInstanceChanging(_UserInstance, value);
					_UserInstance = value;
					OnUserInstanceChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _UserInstance; }
		}
		private bool _UserInstance;
		partial void OnUserInstanceChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnUserInstanceChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the name of the workstation connecting to SQL Server.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.WorkstationID
		///     property, or String.Empty if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Category("Context")]
		public string WorkstationID // Property.tt Line: 123
		{ 
			set
			{
				if (_WorkstationID != value)
				{
					OnWorkstationIDChanging(_WorkstationID, value);
					_WorkstationID = value;
					OnWorkstationIDChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _WorkstationID; }
		}
		private string _WorkstationID = "";
		partial void OnWorkstationIDChanging(string from, string to); // Property.tt Line: 141
		partial void OnWorkstationIDChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the password for the SQL Server account.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Password property,
		///     or String.Empty if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     The password was incorrectly set to null. See code sample below.
		///////////////////////////////////////////////////
		[Category("Security")]
		public string Password // Property.tt Line: 123
		{ 
			set
			{
				if (_Password != value)
				{
					OnPasswordChanging(_Password, value);
					_Password = value;
					OnPasswordChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Password; }
		}
		private string _Password = "";
		partial void OnPasswordChanging(string from, string to); // Property.tt Line: 141
		partial void OnPasswordChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets the authentication of the connection string.
		/// 
		/// Returns:
		///     The authentication of the connection string.
		///////////////////////////////////////////////////
		[Category("Security")]
		public SqlAuthenticationMethod Authentication // Property.tt Line: 123
		{ 
			set
			{
				if (_Authentication != value)
				{
					OnAuthenticationChanging(_Authentication, value);
					_Authentication = value;
					OnAuthenticationChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Authentication; }
		}
		private SqlAuthenticationMethod _Authentication;
		partial void OnAuthenticationChanging(SqlAuthenticationMethod from, SqlAuthenticationMethod to); // Property.tt Line: 141
		partial void OnAuthenticationChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the name of the database associated with the connection.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.InitialCatalog
		///     property, or String.Empty if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Description("Gets or sets the name of the database associated with the connection.")]
		[Category("Source")]
		public string InitialCatalog // Property.tt Line: 123
		{ 
			set
			{
				if (_InitialCatalog != value)
				{
					OnInitialCatalogChanging(_InitialCatalog, value);
					_InitialCatalog = value;
					OnInitialCatalogChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _InitialCatalog; }
		}
		private string _InitialCatalog = "";
		partial void OnInitialCatalogChanging(string from, string to); // Property.tt Line: 141
		partial void OnInitialCatalogChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Declares the application workload type when connecting to a database in an SQL
		///     Server Availability Group. You can set the value of this property with System.Data.SqlClient.ApplicationIntent.
		///     For more information about SqlClient support for Always On Availability Groups,
		///     see SqlClient Support for High Availability, Disaster Recovery.
		/// 
		/// Returns:
		///     Returns the current value of the property (a value of type System.Data.SqlClient.ApplicationIntent).
		///////////////////////////////////////////////////
		[Description("Declares the application workload type when connecting to a database in an SQL Server Availability Group. You can set the value of this property with System.Data.SqlClient.ApplicationIntent.")]
		[Category("Initialization")]
		public ApplicationIntent ApplicationIntentValue // Property.tt Line: 123
		{ 
			set
			{
				if (_ApplicationIntentValue != value)
				{
					OnApplicationIntentValueChanging(_ApplicationIntentValue, value);
					_ApplicationIntentValue = value;
					OnApplicationIntentValueChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ApplicationIntentValue; }
		}
		private ApplicationIntent _ApplicationIntentValue;
		partial void OnApplicationIntentValueChanging(ApplicationIntent from, ApplicationIntent to); // Property.tt Line: 141
		partial void OnApplicationIntentValueChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the name of the application associated with the connection string.
		/// 
		/// Returns:
		///     The name of the application, or ".NET SqlClient Data Provider" if no name has
		///     been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Description("The name of the application, or \".NET SqlClient Data Provider\" if no name has been supplied.")]
		[Category("Context")]
		public string ApplicationName // Property.tt Line: 123
		{ 
			set
			{
				if (_ApplicationName != value)
				{
					OnApplicationNameChanging(_ApplicationName, value);
					_ApplicationName = value;
					OnApplicationNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ApplicationName; }
		}
		private string _ApplicationName = "";
		partial void OnApplicationNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnApplicationNameChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether asynchronous processing is
		///     allowed by the connection created by using this connection string.
		/// 
		/// Returns:
		///     This property is ignored beginning in .NET Framework 4.5. For more information
		///     about SqlClient support for asynchronous programming, see Asynchronous Programming.The
		///     value of the System.Data.SqlClient.SqlConnectionStringBuilder.AsynchronousProcessing
		///     property, or false if no value has been supplied.
		///////////////////////////////////////////////////
		[Description("This property is ignored beginning in .NET Framework 4.5.")]
		[Category("Initialization")]
		public bool AsynchronousProcessing // Property.tt Line: 123
		{ 
			set
			{
				if (_AsynchronousProcessing != value)
				{
					OnAsynchronousProcessingChanging(_AsynchronousProcessing, value);
					_AsynchronousProcessing = value;
					OnAsynchronousProcessingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _AsynchronousProcessing; }
		}
		private bool _AsynchronousProcessing;
		partial void OnAsynchronousProcessingChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnAsynchronousProcessingChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether User ID and Password are
		///     specified in the connection (when false) or whether the current Windows account
		///     credentials are used for authentication (when true).
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.IntegratedSecurity
		///     property, or false if none has been supplied.
		///////////////////////////////////////////////////
		[Description("Gets or sets a Boolean value that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true).")]
		[Category("Security")]
		public bool IntegratedSecurity // Property.tt Line: 123
		{ 
			set
			{
				if (_IntegratedSecurity != value)
				{
					OnIntegratedSecurityChanging(_IntegratedSecurity, value);
					_IntegratedSecurity = value;
					OnIntegratedSecurityChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IntegratedSecurity; }
		}
		private bool _IntegratedSecurity;
		partial void OnIntegratedSecurityChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIntegratedSecurityChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a value that indicates whether a client/server or in-process connection
		///     to SQL Server should be made.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ContextConnection
		///     property, or False if none has been supplied.
		///////////////////////////////////////////////////
		[Description("System.Data.SqlClient.SqlConnectionStringBuilder.ContextConnection")]
		[Category("Source")]
		public bool ContextConnection // Property.tt Line: 123
		{ 
			set
			{
				if (_ContextConnection != value)
				{
					OnContextConnectionChanging(_ContextConnection, value);
					_ContextConnection = value;
					OnContextConnectionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ContextConnection; }
		}
		private bool _ContextConnection;
		partial void OnContextConnectionChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnContextConnectionChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the length of time (in seconds) to wait for a connection to the
		///     server before terminating the attempt and generating an error.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ConnectTimeout
		///     property, or 15 seconds if no value has been supplied.
		///////////////////////////////////////////////////
		[Description("The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ConnectTimeout property, or 15 seconds if no value has been supplied.")]
		[Category("Initialization")]
		public int ConnectTimeout // Property.tt Line: 123
		{ 
			set
			{
				if (_ConnectTimeout != value)
				{
					OnConnectTimeoutChanging(_ConnectTimeout, value);
					_ConnectTimeout = value;
					OnConnectTimeoutChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectTimeout; }
		}
		private int _ConnectTimeout;
		partial void OnConnectTimeoutChanging(int from, int to); // Property.tt Line: 141
		partial void OnConnectTimeoutChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a string that contains the name of the primary data file. This includes
		///     the full path name of an attachable database.
		/// 
		/// Returns:
		///     The value of the AttachDBFilename property, or String.Empty if no value has been
		///     supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Description("Gets or sets a string that contains the name of the primary data file. This includes the full path name of an attachable database.")]
		[Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
		[Category("Source")]
		public string AttachDBFilename // Property.tt Line: 123
		{ 
			set
			{
				if (_AttachDBFilename != value)
				{
					OnAttachDBFilenameChanging(_AttachDBFilename, value);
					_AttachDBFilename = value;
					OnAttachDBFilenameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _AttachDBFilename; }
		}
		private string _AttachDBFilename = "";
		partial void OnAttachDBFilenameChanging(string from, string to); // Property.tt Line: 141
		partial void OnAttachDBFilenameChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the name or network address of the instance of SQL Server to connect
		///     to.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.DataSource
		///     property, or String.Empty if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Description("Gets or sets the name or network address of the instance of SQL Server to connect to.")]
		[Category("Source")]
		public string DataSource // Property.tt Line: 123
		{ 
			set
			{
				if (_DataSource != value)
				{
					OnDataSourceChanging(_DataSource, value);
					_DataSource = value;
					OnDataSourceChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataSource; }
		}
		private string _DataSource = "";
		partial void OnDataSourceChanging(string from, string to); // Property.tt Line: 141
		partial void OnDataSourceChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether SQL Server uses SSL encryption
		///     for all data sent between the client and server if the server has a certificate
		///     installed.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Encrypt property,
		///     or false if none has been supplied.
		///////////////////////////////////////////////////
		[Description("Gets or sets a Boolean value that indicates whether SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.")]
		[Category("Security")]
		public bool Encrypt // Property.tt Line: 123
		{ 
			set
			{
				if (_Encrypt != value)
				{
					OnEncryptChanging(_Encrypt, value);
					_Encrypt = value;
					OnEncryptChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Encrypt; }
		}
		private bool _Encrypt;
		partial void OnEncryptChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnEncryptChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets and sets the column encryption settings for the connection string builder.
		/// 
		/// Returns:
		///     The column encryption settings for the connection string builder.
		///////////////////////////////////////////////////
		[Description("Gets and sets the column encryption settings for the connection string builder.")]
		[Category("Security")]
		public SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting // Property.tt Line: 123
		{ 
			set
			{
				if (_ColumnEncryptionSetting != value)
				{
					OnColumnEncryptionSettingChanging(_ColumnEncryptionSetting, value);
					_ColumnEncryptionSetting = value;
					OnColumnEncryptionSettingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ColumnEncryptionSetting; }
		}
		private SqlConnectionColumnEncryptionSetting _ColumnEncryptionSetting;
		partial void OnColumnEncryptionSettingChanging(SqlConnectionColumnEncryptionSetting from, SqlConnectionColumnEncryptionSetting to); // Property.tt Line: 141
		partial void OnColumnEncryptionSettingChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a value that indicates whether the channel will be encrypted while
		///     bypassing walking the certificate chain to validate trust.
		/// 
		/// Returns:
		///     A Boolean. Recognized values are true, false, yes, and no.
		///////////////////////////////////////////////////
		[Description("Gets or sets a value that indicates whether the channel will be encrypted while bypassing walking the certificate chain to validate trust.")]
		[Category("Security")]
		public bool TrustServerCertificate // Property.tt Line: 123
		{ 
			set
			{
				if (_TrustServerCertificate != value)
				{
					OnTrustServerCertificateChanging(_TrustServerCertificate, value);
					_TrustServerCertificate = value;
					OnTrustServerCertificateChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TrustServerCertificate; }
		}
		private bool _TrustServerCertificate;
		partial void OnTrustServerCertificateChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnTrustServerCertificateChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether the SQL Server connection
		///     pooler automatically enlists the connection in the creation thread's current
		///     transaction context.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Enlist property,
		///     or true if none has been supplied.
		///////////////////////////////////////////////////
		[Description("Gets or sets a Boolean value that indicates whether the SQL Server connection pooler automatically enlists the connection in the creation thread's current transaction context.")]
		[Category("Pooling")]
		public bool Enlist // Property.tt Line: 123
		{ 
			set
			{
				if (_Enlist != value)
				{
					OnEnlistChanging(_Enlist, value);
					_Enlist = value;
					OnEnlistChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Enlist; }
		}
		private bool _Enlist;
		partial void OnEnlistChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnEnlistChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the name or address of the partner server to connect to if the primary
		///     server is down.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.FailoverPartner
		///     property, or String.Empty if none has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Description("Gets or sets the name or address of the partner server to connect to if the primary server is down.")]
		[Category("Source")]
		public string FailoverPartner // Property.tt Line: 123
		{ 
			set
			{
				if (_FailoverPartner != value)
				{
					OnFailoverPartnerChanging(_FailoverPartner, value);
					_FailoverPartner = value;
					OnFailoverPartnerChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _FailoverPartner; }
		}
		private string _FailoverPartner = "";
		partial void OnFailoverPartnerChanging(string from, string to); // Property.tt Line: 141
		partial void OnFailoverPartnerChanged();
		
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the SQL Server Language record name.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.CurrentLanguage
		///     property, or String.Empty if no value has been supplied.
		/// 
		/// Exceptions:
		///   T:System.ArgumentNullException:
		///     To set the value to null, use System.DBNull.Value.
		///////////////////////////////////////////////////
		[Description("Gets or sets the SQL Server Language record name.")]
		[Category("Initialization")]
		public string CurrentLanguage // Property.tt Line: 123
		{ 
			set
			{
				if (_CurrentLanguage != value)
				{
					OnCurrentLanguageChanging(_CurrentLanguage, value);
					_CurrentLanguage = value;
					OnCurrentLanguageChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _CurrentLanguage; }
		}
		private string _CurrentLanguage = "";
		partial void OnCurrentLanguageChanging(string from, string to); // Property.tt Line: 141
		partial void OnCurrentLanguageChanged();
	
		#endregion Properties
	}
	
	public interface IVisitorProto // IVisitorProto.tt Line: 7
	{
		void Visit(Proto.Config.Connection.proto_ms_sql_design_generator_settings p);
		void Visit(Proto.Config.Connection.proto_ms_sql_connection_settings p);
	}
	
	public partial class ValidationMsSqlDesignGeneratorSettingsVisitor : MsSqlDesignGeneratorSettingsVisitor // ValidationVisitor.tt Line: 7
	{
	    partial void OnVisit(IValidatableWithSeverity p);
	    partial void OnVisitEnd(IValidatableWithSeverity p);
		protected override void OnVisit(MsSqlDesignGeneratorSettings p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(MsSqlDesignGeneratorSettings p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(MsSqlConnectionSettings p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(MsSqlConnectionSettings p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
	}
	
	public partial class MsSqlDesignGeneratorSettingsVisitor : IVisitorMsSqlDesignGeneratorSettingsNode // NodeVisitor.tt Line: 7
	{
	    public CancellationToken Token { get { return _cancellationToken; } }
	    protected CancellationToken _cancellationToken;
	
		public void Visit(MsSqlDesignGeneratorSettings p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(MsSqlDesignGeneratorSettings p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(MsSqlDesignGeneratorSettings p) {}
	    protected virtual void OnVisitEnd(MsSqlDesignGeneratorSettings p) {}
		public void Visit(MsSqlConnectionSettings p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(MsSqlConnectionSettings p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(MsSqlConnectionSettings p) {}
	    protected virtual void OnVisitEnd(MsSqlConnectionSettings p) {}
	}

public interface IVisitorMsSqlDesignGeneratorSettingsNode // IVisitorConfigNode.tt Line: 7
{
    System.Threading.CancellationToken Token { get; }
}
}