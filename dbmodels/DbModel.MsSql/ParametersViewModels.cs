// Auto generated on UTC 05/24/2019 19:26:39
using System;
using System.Linq;
using ViewModelBase;
using FluentValidation;
using Proto.Config.Connection;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.ComponentModel;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017
	public partial class ConnMsSql : ViewModelValidatableWithSeverity<ConnMsSql, ConnMsSql.ConnMsSqlValidator>
	{
		public partial class ConnMsSqlValidator : ValidatorBase<ConnMsSql, ConnMsSqlValidator> { }
		#region CTOR
		public ConnMsSql() : base(ConnMsSqlValidator.Validator)
		{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static ConnMsSql Clone(ConnMsSql from, bool isDeep = true)
		{
		    ConnMsSql vm = new ConnMsSql();
		    vm.Name = from.Name;
		    vm.Guid = from.Guid;
		    vm.MaxPoolSize = from.MaxPoolSize;
		    vm.ConnectRetryCount = from.ConnectRetryCount;
		    vm.ConnectRetryInterval = from.ConnectRetryInterval;
		    vm.MinPoolSize = from.MinPoolSize;
		    vm.MultipleActiveResultSets = from.MultipleActiveResultSets;
		    vm.MultiSubnetFailover = from.MultiSubnetFailover;
		    vm.TransparentNetworkIPResolution = from.TransparentNetworkIPResolution;
		    vm.NetworkLibrary = from.NetworkLibrary;
		    vm.PacketSize = from.PacketSize;
		    vm.PersistSecurityInfo = from.PersistSecurityInfo;
		    vm.LoadBalanceTimeout = from.LoadBalanceTimeout;
		    vm.Pooling = from.Pooling;
		    vm.Replication = from.Replication;
		    vm.TransactionBinding = from.TransactionBinding;
		    vm.TypeSystemVersion = from.TypeSystemVersion;
		    vm.UserID = from.UserID;
		    vm.UserInstance = from.UserInstance;
		    vm.WorkstationID = from.WorkstationID;
		    vm.Password = from.Password;
		    vm.Authentication = from.Authentication;
		    vm.InitialCatalog = from.InitialCatalog;
		    vm.ApplicationIntentValue = from.ApplicationIntentValue;
		    vm.ApplicationName = from.ApplicationName;
		    vm.AsynchronousProcessing = from.AsynchronousProcessing;
		    vm.IntegratedSecurity = from.IntegratedSecurity;
		    vm.ContextConnection = from.ContextConnection;
		    vm.ConnectTimeout = from.ConnectTimeout;
		    vm.AttachDBFilename = from.AttachDBFilename;
		    vm.DataSource = from.DataSource;
		    vm.Encrypt = from.Encrypt;
		    vm.ColumnEncryptionSetting = from.ColumnEncryptionSetting;
		    vm.TrustServerCertificate = from.TrustServerCertificate;
		    vm.Enlist = from.Enlist;
		    vm.FailoverPartner = from.FailoverPartner;
		    vm.CurrentLanguage = from.CurrentLanguage;
		    return vm;
		}
		public static void Update(ConnMsSql to, ConnMsSql from, bool isDeep = true)
		{
		    to.Name = from.Name;
		    to.Guid = from.Guid;
		    to.MaxPoolSize = from.MaxPoolSize;
		    to.ConnectRetryCount = from.ConnectRetryCount;
		    to.ConnectRetryInterval = from.ConnectRetryInterval;
		    to.MinPoolSize = from.MinPoolSize;
		    to.MultipleActiveResultSets = from.MultipleActiveResultSets;
		    to.MultiSubnetFailover = from.MultiSubnetFailover;
		    to.TransparentNetworkIPResolution = from.TransparentNetworkIPResolution;
		    to.NetworkLibrary = from.NetworkLibrary;
		    to.PacketSize = from.PacketSize;
		    to.PersistSecurityInfo = from.PersistSecurityInfo;
		    to.LoadBalanceTimeout = from.LoadBalanceTimeout;
		    to.Pooling = from.Pooling;
		    to.Replication = from.Replication;
		    to.TransactionBinding = from.TransactionBinding;
		    to.TypeSystemVersion = from.TypeSystemVersion;
		    to.UserID = from.UserID;
		    to.UserInstance = from.UserInstance;
		    to.WorkstationID = from.WorkstationID;
		    to.Password = from.Password;
		    to.Authentication = from.Authentication;
		    to.InitialCatalog = from.InitialCatalog;
		    to.ApplicationIntentValue = from.ApplicationIntentValue;
		    to.ApplicationName = from.ApplicationName;
		    to.AsynchronousProcessing = from.AsynchronousProcessing;
		    to.IntegratedSecurity = from.IntegratedSecurity;
		    to.ContextConnection = from.ContextConnection;
		    to.ConnectTimeout = from.ConnectTimeout;
		    to.AttachDBFilename = from.AttachDBFilename;
		    to.DataSource = from.DataSource;
		    to.Encrypt = from.Encrypt;
		    to.ColumnEncryptionSetting = from.ColumnEncryptionSetting;
		    to.TrustServerCertificate = from.TrustServerCertificate;
		    to.Enlist = from.Enlist;
		    to.FailoverPartner = from.FailoverPartner;
		    to.CurrentLanguage = from.CurrentLanguage;
		}
		#region IEditable
		public override ConnMsSql Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return ConnMsSql.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(ConnMsSql from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    ConnMsSql.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_conn_ms_sql' to 'ConnMsSql'
		public static ConnMsSql ConvertToVM(proto_conn_ms_sql m, ConnMsSql vm = null)
		{
		    if (vm == null)
		        vm = new ConnMsSql();
		    vm.Name = m.Name;
		    vm.Guid = m.Guid;
		    vm.MaxPoolSize = m.MaxPoolSize;
		    vm.ConnectRetryCount = m.ConnectRetryCount;
		    vm.ConnectRetryInterval = m.ConnectRetryInterval;
		    vm.MinPoolSize = m.MinPoolSize;
		    vm.MultipleActiveResultSets = m.MultipleActiveResultSets;
		    vm.MultiSubnetFailover = m.MultiSubnetFailover;
		    vm.TransparentNetworkIPResolution = m.TransparentNetworkIPResolution;
		    vm.NetworkLibrary = m.NetworkLibrary;
		    vm.PacketSize = m.PacketSize;
		    vm.PersistSecurityInfo = m.PersistSecurityInfo;
		    vm.LoadBalanceTimeout = m.LoadBalanceTimeout;
		    vm.Pooling = m.Pooling;
		    vm.Replication = m.Replication;
		    vm.TransactionBinding = m.TransactionBinding;
		    vm.TypeSystemVersion = m.TypeSystemVersion;
		    vm.UserID = m.UserID;
		    vm.UserInstance = m.UserInstance;
		    vm.WorkstationID = m.WorkstationID;
		    vm.Password = m.Password;
		    vm.Authentication = m.Authentication;
		    vm.InitialCatalog = m.InitialCatalog;
		    vm.ApplicationIntentValue = m.ApplicationIntentValue;
		    vm.ApplicationName = m.ApplicationName;
		    vm.AsynchronousProcessing = m.AsynchronousProcessing;
		    vm.IntegratedSecurity = m.IntegratedSecurity;
		    vm.ContextConnection = m.ContextConnection;
		    vm.ConnectTimeout = m.ConnectTimeout;
		    vm.AttachDBFilename = m.AttachDBFilename;
		    vm.DataSource = m.DataSource;
		    vm.Encrypt = m.Encrypt;
		    vm.ColumnEncryptionSetting = m.ColumnEncryptionSetting;
		    vm.TrustServerCertificate = m.TrustServerCertificate;
		    vm.Enlist = m.Enlist;
		    vm.FailoverPartner = m.FailoverPartner;
		    vm.CurrentLanguage = m.CurrentLanguage;
		    return vm;
		}
		// Conversion from 'ConnMsSql' to 'proto_conn_ms_sql'
		public static proto_conn_ms_sql ConvertToProto(ConnMsSql vm)
		{
		    proto_conn_ms_sql m = new proto_conn_ms_sql();
		    m.Name = vm.Name;
		    m.Guid = vm.Guid;
		    m.MaxPoolSize = vm.MaxPoolSize;
		    m.ConnectRetryCount = vm.ConnectRetryCount;
		    m.ConnectRetryInterval = vm.ConnectRetryInterval;
		    m.MinPoolSize = vm.MinPoolSize;
		    m.MultipleActiveResultSets = vm.MultipleActiveResultSets;
		    m.MultiSubnetFailover = vm.MultiSubnetFailover;
		    m.TransparentNetworkIPResolution = vm.TransparentNetworkIPResolution;
		    m.NetworkLibrary = vm.NetworkLibrary;
		    m.PacketSize = vm.PacketSize;
		    m.PersistSecurityInfo = vm.PersistSecurityInfo;
		    m.LoadBalanceTimeout = vm.LoadBalanceTimeout;
		    m.Pooling = vm.Pooling;
		    m.Replication = vm.Replication;
		    m.TransactionBinding = vm.TransactionBinding;
		    m.TypeSystemVersion = vm.TypeSystemVersion;
		    m.UserID = vm.UserID;
		    m.UserInstance = vm.UserInstance;
		    m.WorkstationID = vm.WorkstationID;
		    m.Password = vm.Password;
		    m.Authentication = vm.Authentication;
		    m.InitialCatalog = vm.InitialCatalog;
		    m.ApplicationIntentValue = vm.ApplicationIntentValue;
		    m.ApplicationName = vm.ApplicationName;
		    m.AsynchronousProcessing = vm.AsynchronousProcessing;
		    m.IntegratedSecurity = vm.IntegratedSecurity;
		    m.ContextConnection = vm.ContextConnection;
		    m.ConnectTimeout = vm.ConnectTimeout;
		    m.AttachDBFilename = vm.AttachDBFilename;
		    m.DataSource = vm.DataSource;
		    m.Encrypt = vm.Encrypt;
		    m.ColumnEncryptionSetting = vm.ColumnEncryptionSetting;
		    m.TrustServerCertificate = vm.TrustServerCertificate;
		    m.Enlist = vm.Enlist;
		    m.FailoverPartner = vm.FailoverPartner;
		    m.CurrentLanguage = vm.CurrentLanguage;
		    return m;
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
		public string Guid
		{ 
			set
			{
				if (_Guid != value)
				{
					OnGuidChanging();
					_Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Guid; }
		}
		private string _Guid = "";
		partial void OnGuidChanging();
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
		[Category("Pooling")]
		public int MaxPoolSize
		{ 
			set
			{
				if (_MaxPoolSize != value)
				{
					OnMaxPoolSizeChanging();
					_MaxPoolSize = value;
					OnMaxPoolSizeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MaxPoolSize; }
		}
		private int _MaxPoolSize;
		partial void OnMaxPoolSizeChanging();
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
		[Category("ConnectionResilency")]
		public int ConnectRetryCount
		{ 
			set
			{
				if (_ConnectRetryCount != value)
				{
					OnConnectRetryCountChanging();
					_ConnectRetryCount = value;
					OnConnectRetryCountChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectRetryCount; }
		}
		private int _ConnectRetryCount;
		partial void OnConnectRetryCountChanging();
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
		[Category("ConnectionResilency")]
		public int ConnectRetryInterval
		{ 
			set
			{
				if (_ConnectRetryInterval != value)
				{
					OnConnectRetryIntervalChanging();
					_ConnectRetryInterval = value;
					OnConnectRetryIntervalChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectRetryInterval; }
		}
		private int _ConnectRetryInterval;
		partial void OnConnectRetryIntervalChanging();
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
		[Category("Pooling")]
		public int MinPoolSize
		{ 
			set
			{
				if (_MinPoolSize != value)
				{
					OnMinPoolSizeChanging();
					_MinPoolSize = value;
					OnMinPoolSizeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MinPoolSize; }
		}
		private int _MinPoolSize;
		partial void OnMinPoolSizeChanging();
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
		[CategoryAttribute("Advanced")]
		public bool MultipleActiveResultSets
		{ 
			set
			{
				if (_MultipleActiveResultSets != value)
				{
					OnMultipleActiveResultSetsChanging();
					_MultipleActiveResultSets = value;
					OnMultipleActiveResultSetsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MultipleActiveResultSets; }
		}
		private bool _MultipleActiveResultSets;
		partial void OnMultipleActiveResultSetsChanging();
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
		[Category("Source")]
		public bool MultiSubnetFailover
		{ 
			set
			{
				if (_MultiSubnetFailover != value)
				{
					OnMultiSubnetFailoverChanging();
					_MultiSubnetFailover = value;
					OnMultiSubnetFailoverChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _MultiSubnetFailover; }
		}
		private bool _MultiSubnetFailover;
		partial void OnMultiSubnetFailoverChanging();
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
		[Category("Source")]
		public bool TransparentNetworkIPResolution
		{ 
			set
			{
				if (_TransparentNetworkIPResolution != value)
				{
					OnTransparentNetworkIPResolutionChanging();
					_TransparentNetworkIPResolution = value;
					OnTransparentNetworkIPResolutionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TransparentNetworkIPResolution; }
		}
		private bool _TransparentNetworkIPResolution;
		partial void OnTransparentNetworkIPResolutionChanging();
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
		public string NetworkLibrary
		{ 
			set
			{
				if (_NetworkLibrary != value)
				{
					OnNetworkLibraryChanging();
					_NetworkLibrary = value;
					OnNetworkLibraryChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _NetworkLibrary; }
		}
		private string _NetworkLibrary = "";
		partial void OnNetworkLibraryChanging();
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
		[Category("Advanced")]
		public int PacketSize
		{ 
			set
			{
				if (_PacketSize != value)
				{
					OnPacketSizeChanging();
					_PacketSize = value;
					OnPacketSizeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PacketSize; }
		}
		private int _PacketSize;
		partial void OnPacketSizeChanging();
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
		[Category("Security")]
		public bool PersistSecurityInfo
		{ 
			set
			{
				if (_PersistSecurityInfo != value)
				{
					OnPersistSecurityInfoChanging();
					_PersistSecurityInfo = value;
					OnPersistSecurityInfoChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PersistSecurityInfo; }
		}
		private bool _PersistSecurityInfo;
		partial void OnPersistSecurityInfoChanging();
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
		[Category("Pooling")]
		public int LoadBalanceTimeout
		{ 
			set
			{
				if (_LoadBalanceTimeout != value)
				{
					OnLoadBalanceTimeoutChanging();
					_LoadBalanceTimeout = value;
					OnLoadBalanceTimeoutChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _LoadBalanceTimeout; }
		}
		private int _LoadBalanceTimeout;
		partial void OnLoadBalanceTimeoutChanging();
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
		[Category("Pooling")]
		public bool Pooling
		{ 
			set
			{
				if (_Pooling != value)
				{
					OnPoolingChanging();
					_Pooling = value;
					OnPoolingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Pooling; }
		}
		private bool _Pooling;
		partial void OnPoolingChanging();
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
		[Category("Replication")]
		public bool Replication
		{ 
			set
			{
				if (_Replication != value)
				{
					OnReplicationChanging();
					_Replication = value;
					OnReplicationChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Replication; }
		}
		private bool _Replication;
		partial void OnReplicationChanging();
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
		[Category("Advanced")]
		public string TransactionBinding
		{ 
			set
			{
				if (_TransactionBinding != value)
				{
					OnTransactionBindingChanging();
					_TransactionBinding = value;
					OnTransactionBindingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TransactionBinding; }
		}
		private string _TransactionBinding = "";
		partial void OnTransactionBindingChanging();
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
		public string TypeSystemVersion
		{ 
			set
			{
				if (_TypeSystemVersion != value)
				{
					OnTypeSystemVersionChanging();
					_TypeSystemVersion = value;
					OnTypeSystemVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TypeSystemVersion; }
		}
		private string _TypeSystemVersion = "";
		partial void OnTypeSystemVersionChanging();
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
		public string UserID
		{ 
			set
			{
				if (_UserID != value)
				{
					OnUserIDChanging();
					_UserID = value;
					OnUserIDChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _UserID; }
		}
		private string _UserID = "";
		partial void OnUserIDChanging();
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
		public bool UserInstance
		{ 
			set
			{
				if (_UserInstance != value)
				{
					OnUserInstanceChanging();
					_UserInstance = value;
					OnUserInstanceChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _UserInstance; }
		}
		private bool _UserInstance;
		partial void OnUserInstanceChanging();
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
		public string WorkstationID
		{ 
			set
			{
				if (_WorkstationID != value)
				{
					OnWorkstationIDChanging();
					_WorkstationID = value;
					OnWorkstationIDChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _WorkstationID; }
		}
		private string _WorkstationID = "";
		partial void OnWorkstationIDChanging();
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
		public string Password
		{ 
			set
			{
				if (_Password != value)
				{
					OnPasswordChanging();
					_Password = value;
					OnPasswordChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Password; }
		}
		private string _Password = "";
		partial void OnPasswordChanging();
		partial void OnPasswordChanged();
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets the authentication of the connection string.
		/// 
		/// Returns:
		///     The authentication of the connection string.
		///////////////////////////////////////////////////
		[Category("Security")]
		public proto_conn_ms_sql.Types.SqlAuthenticationMethod Authentication
		{ 
			set
			{
				if (_Authentication != value)
				{
					OnAuthenticationChanging();
					_Authentication = value;
					OnAuthenticationChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Authentication; }
		}
		private proto_conn_ms_sql.Types.SqlAuthenticationMethod _Authentication;
		partial void OnAuthenticationChanging();
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
		[Category("Source")]
		public string InitialCatalog
		{ 
			set
			{
				if (_InitialCatalog != value)
				{
					OnInitialCatalogChanging();
					_InitialCatalog = value;
					OnInitialCatalogChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _InitialCatalog; }
		}
		private string _InitialCatalog = "";
		partial void OnInitialCatalogChanging();
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
		[Category("Initialization")]
		public proto_conn_ms_sql.Types.ApplicationIntent ApplicationIntentValue
		{ 
			set
			{
				if (_ApplicationIntentValue != value)
				{
					OnApplicationIntentValueChanging();
					_ApplicationIntentValue = value;
					OnApplicationIntentValueChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ApplicationIntentValue; }
		}
		private proto_conn_ms_sql.Types.ApplicationIntent _ApplicationIntentValue;
		partial void OnApplicationIntentValueChanging();
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
		[Category("Context")]
		public string ApplicationName
		{ 
			set
			{
				if (_ApplicationName != value)
				{
					OnApplicationNameChanging();
					_ApplicationName = value;
					OnApplicationNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ApplicationName; }
		}
		private string _ApplicationName = "";
		partial void OnApplicationNameChanging();
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
		[Category("Initialization")]
		public bool AsynchronousProcessing
		{ 
			set
			{
				if (_AsynchronousProcessing != value)
				{
					OnAsynchronousProcessingChanging();
					_AsynchronousProcessing = value;
					OnAsynchronousProcessingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _AsynchronousProcessing; }
		}
		private bool _AsynchronousProcessing;
		partial void OnAsynchronousProcessingChanging();
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
		[Category("Security")]
		public bool IntegratedSecurity
		{ 
			set
			{
				if (_IntegratedSecurity != value)
				{
					OnIntegratedSecurityChanging();
					_IntegratedSecurity = value;
					OnIntegratedSecurityChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IntegratedSecurity; }
		}
		private bool _IntegratedSecurity;
		partial void OnIntegratedSecurityChanging();
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
		[Category("Source")]
		public bool ContextConnection
		{ 
			set
			{
				if (_ContextConnection != value)
				{
					OnContextConnectionChanging();
					_ContextConnection = value;
					OnContextConnectionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ContextConnection; }
		}
		private bool _ContextConnection;
		partial void OnContextConnectionChanging();
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
		[Category("Initialization")]
		public int ConnectTimeout
		{ 
			set
			{
				if (_ConnectTimeout != value)
				{
					OnConnectTimeoutChanging();
					_ConnectTimeout = value;
					OnConnectTimeoutChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectTimeout; }
		}
		private int _ConnectTimeout;
		partial void OnConnectTimeoutChanging();
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
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Category("Source")]
		public string AttachDBFilename
		{ 
			set
			{
				if (_AttachDBFilename != value)
				{
					OnAttachDBFilenameChanging();
					_AttachDBFilename = value;
					OnAttachDBFilenameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _AttachDBFilename; }
		}
		private string _AttachDBFilename = "";
		partial void OnAttachDBFilenameChanging();
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
		[Category("Source")]
		public string DataSource
		{ 
			set
			{
				if (_DataSource != value)
				{
					OnDataSourceChanging();
					_DataSource = value;
					OnDataSourceChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataSource; }
		}
		private string _DataSource = "";
		partial void OnDataSourceChanging();
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
		[Category("Security")]
		public bool Encrypt
		{ 
			set
			{
				if (_Encrypt != value)
				{
					OnEncryptChanging();
					_Encrypt = value;
					OnEncryptChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Encrypt; }
		}
		private bool _Encrypt;
		partial void OnEncryptChanging();
		partial void OnEncryptChanged();
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets and sets the column encryption settings for the connection string builder.
		/// 
		/// Returns:
		///     The column encryption settings for the connection string builder.
		///////////////////////////////////////////////////
		[Category("Security")]
		public proto_conn_ms_sql.Types.SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting
		{ 
			set
			{
				if (_ColumnEncryptionSetting != value)
				{
					OnColumnEncryptionSettingChanging();
					_ColumnEncryptionSetting = value;
					OnColumnEncryptionSettingChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ColumnEncryptionSetting; }
		}
		private proto_conn_ms_sql.Types.SqlConnectionColumnEncryptionSetting _ColumnEncryptionSetting;
		partial void OnColumnEncryptionSettingChanging();
		partial void OnColumnEncryptionSettingChanged();
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a value that indicates whether the channel will be encrypted while
		///     bypassing walking the certificate chain to validate trust.
		/// 
		/// Returns:
		///     A Boolean. Recognized values are true, false, yes, and no.
		///////////////////////////////////////////////////
		[Category("Security")]
		public bool TrustServerCertificate
		{ 
			set
			{
				if (_TrustServerCertificate != value)
				{
					OnTrustServerCertificateChanging();
					_TrustServerCertificate = value;
					OnTrustServerCertificateChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _TrustServerCertificate; }
		}
		private bool _TrustServerCertificate;
		partial void OnTrustServerCertificateChanging();
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
		[Category("Pooling")]
		public bool Enlist
		{ 
			set
			{
				if (_Enlist != value)
				{
					OnEnlistChanging();
					_Enlist = value;
					OnEnlistChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Enlist; }
		}
		private bool _Enlist;
		partial void OnEnlistChanging();
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
		[Category("Source")]
		public string FailoverPartner
		{ 
			set
			{
				if (_FailoverPartner != value)
				{
					OnFailoverPartnerChanging();
					_FailoverPartner = value;
					OnFailoverPartnerChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _FailoverPartner; }
		}
		private string _FailoverPartner = "";
		partial void OnFailoverPartnerChanging();
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
		[Category("Initialization")]
		public string CurrentLanguage
		{ 
			set
			{
				if (_CurrentLanguage != value)
				{
					OnCurrentLanguageChanging();
					_CurrentLanguage = value;
					OnCurrentLanguageChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _CurrentLanguage; }
		}
		private string _CurrentLanguage = "";
		partial void OnCurrentLanguageChanging();
		partial void OnCurrentLanguageChanged();
		#endregion Properties
	}
	
	public interface IVisitorConfigNode
	{
	    CancellationToken Token { get; }
	}
	
	public interface IVisitorProto
	{
		void Visit(proto_conn_ms_sql p);
	}
	
	public partial class ValidationVisitor : IVisitorConfigNode
	{
	    CancellationToken IVisitorConfigNode.Token => _cancellationToken;
	    private CancellationToken _cancellationToken;
		public void Visit(ConnMsSql p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ConnMsSql p)
	    {
	        OnVisitEnd(p);
	    }
	}
	
	public partial class ConfigVisitor : IVisitorConfigNode
	{
	    CancellationToken IVisitorConfigNode.Token => _cancellationToken;
	    private CancellationToken _cancellationToken;
	
		public void Visit(ConnMsSql p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ConnMsSql p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(ConnMsSql p) {}
	    protected virtual void OnVisitEnd(ConnMsSql p) {}
	}
}