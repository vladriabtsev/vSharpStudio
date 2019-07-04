using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

namespace vSharpStudio.common
{
	public enum SqlAuthenticationMethod {
		NotSpecified = 0,
		SqlPassword = 1,
		ActiveDirectoryPassword = 2,
		ActiveDirectoryIntegrated = 3,
	}
	public enum ApplicationIntent {
		ReadWrite = 0,
		ReadOnly = 1,
	}
	public enum SqlConnectionColumnEncryptionSetting {
		Disabled = 0,
		Enabled = 1,
	}
	
	public partial interface IMsSqlDesignGeneratorSettings{
		string Name { get; }
		string Guid { get; }
		bool IsUseForeingkey { get; }
		bool IsUseForeingkeyIndex { get; }
		bool IsUseStorageProcedures { get; }
		bool IsUseViews { get; }
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		bool IsPrimaryKeyClustered { get; }
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		bool IsMemoryOptimized { get; }
	}
	
	public partial interface IMsSqlConnectionSettings{
		string Name { get; }
		string Guid { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the maximum number of connections allowed in the connection pool
		///     for this specific connection string.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MaxPoolSize
		///     property, or 100 if none has been supplied.
		///////////////////////////////////////////////////
		int MaxPoolSize { get; }
		
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
		int ConnectRetryCount { get; }
		
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
		int ConnectRetryInterval { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the minimum number of connections allowed in the connection pool
		///     for this specific connection string.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MinPoolSize
		///     property, or 0 if none has been supplied.
		///////////////////////////////////////////////////
		int MinPoolSize { get; }
		
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
		bool MultipleActiveResultSets { get; }
		
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
		bool MultiSubnetFailover { get; }
		
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
		bool TransparentNetworkIPResolution { get; }
		
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
		string NetworkLibrary { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the size in bytes of the network packets used to communicate with
		///     an instance of SQL Server.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.PacketSize
		///     property, or 8000 if none has been supplied.
		///////////////////////////////////////////////////
		int PacketSize { get; }
		
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
		bool PersistSecurityInfo { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the minimum time, in seconds, for the connection to live in the
		///     connection pool before being destroyed.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.LoadBalanceTimeout
		///     property, or 0 if none has been supplied.
		///////////////////////////////////////////////////
		int LoadBalanceTimeout { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether the connection will be pooled
		///     or explicitly opened every time that the connection is requested.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Pooling property,
		///     or true if none has been supplied.
		///////////////////////////////////////////////////
		bool Pooling { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a Boolean value that indicates whether replication is supported
		///     using the connection.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Replication
		///     property, or false if none has been supplied.
		///////////////////////////////////////////////////
		bool Replication { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a string value that indicates how the connection maintains its association
		///     with an enlisted System.Transactions transaction.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.TransactionBinding
		///     property, or String.Empty if none has been supplied.
		///////////////////////////////////////////////////
		string TransactionBinding { get; }
		
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
		string TypeSystemVersion { get; }
		
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
		string UserID { get; }
		
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
		bool UserInstance { get; }
		
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
		string WorkstationID { get; }
		
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
		string Password { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets the authentication of the connection string.
		/// 
		/// Returns:
		///     The authentication of the connection string.
		///////////////////////////////////////////////////
		SqlAuthenticationMethod Authentication { get; }
		
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
		string InitialCatalog { get; }
		
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
		ApplicationIntent ApplicationIntentValue { get; }
		
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
		string ApplicationName { get; }
		
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
		bool AsynchronousProcessing { get; }
		
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
		bool IntegratedSecurity { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a value that indicates whether a client/server or in-process connection
		///     to SQL Server should be made.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ContextConnection
		///     property, or False if none has been supplied.
		///////////////////////////////////////////////////
		bool ContextConnection { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets the length of time (in seconds) to wait for a connection to the
		///     server before terminating the attempt and generating an error.
		/// 
		/// Returns:
		///     The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ConnectTimeout
		///     property, or 15 seconds if no value has been supplied.
		///////////////////////////////////////////////////
		int ConnectTimeout { get; }
		
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
		string AttachDBFilename { get; }
		
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
		string DataSource { get; }
		
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
		bool Encrypt { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets and sets the column encryption settings for the connection string builder.
		/// 
		/// Returns:
		///     The column encryption settings for the connection string builder.
		///////////////////////////////////////////////////
		SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting { get; }
		
		///////////////////////////////////////////////////
		/// Summary:
		///     Gets or sets a value that indicates whether the channel will be encrypted while
		///     bypassing walking the certificate chain to validate trust.
		/// 
		/// Returns:
		///     A Boolean. Recognized values are true, false, yes, and no.
		///////////////////////////////////////////////////
		bool TrustServerCertificate { get; }
		
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
		bool Enlist { get; }
		
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
		string FailoverPartner { get; }
		
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
		string CurrentLanguage { get; }
	}
}