# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [conn_mssql.proto](#conn_mssql.proto)
    - [proto_ms_sql_connection_settings](#proto_ms_sql_design_generator_settings.proto_ms_sql_connection_settings)
    - [proto_ms_sql_design_generator_settings](#proto_ms_sql_design_generator_settings.proto_ms_sql_design_generator_settings)
  
    - [ApplicationIntent](#proto_ms_sql_design_generator_settings.ApplicationIntent)
    - [SqlAuthenticationMethod](#proto_ms_sql_design_generator_settings.SqlAuthenticationMethod)
    - [SqlConnectionColumnEncryptionSetting](#proto_ms_sql_design_generator_settings.SqlConnectionColumnEncryptionSetting)
  
  
  

- [Scalar Value Types](#scalar-value-types)



<a name="conn_mssql.proto"></a>
<p align="right"><a href="#top">Top</a></p>

## conn_mssql.proto



<a name="proto_ms_sql_design_generator_settings.proto_ms_sql_connection_settings"></a>

### proto_ms_sql_connection_settings
@attr [CategoryOrder(&#34;Source&#34;, 1)]
@attr [CategoryOrder(&#34;Security&#34;, 2)]
@attr [CategoryOrder(&#34;Pooling&#34;, 3)]
@attr [CategoryOrder(&#34;Initialization&#34;, 4)]
@attr [CategoryOrder(&#34;ConnectionResilency&#34;, 5)]
@attr [CategoryOrder(&#34;Advanced&#34;, 6)]
@base : ViewModelValidatableWithSeverity&lt;MsSqlConnectionSettings, MsSqlConnectionSettings.MsSqlConnectionSettingsValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| guid | [string](#string) |  |  |
| max_pool_size | [int32](#int32) |  | Summary: Gets or sets the maximum number of connections allowed in the connection pool for this specific connection string.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MaxPoolSize property, or 100 if none has been supplied. @attr [Description(&#34;The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MaxPoolSize property, or 100 if none has been supplied.&#34;)] @attr [Category(&#34;Pooling&#34;)] |
| connect_retry_count | [int32](#int32) |  | Summary: [Supported in the .NET Framework 4.5.1 and later versions] The number of reconnections attempted after identifying that there was an idle connection failure. This must be an integer between 0 and 255. Default is 1. Set to 0 to disable reconnecting on idle connection failures. An System.ArgumentException will be thrown if set to a value outside of the allowed range.

Returns: The number of reconnections attempted after identifying that there was an idle connection failure. @attr [Description(&#34;.NET Framework 4.5.1 and later versions. The number of reconnections attempted after identifying that there was an idle connection failure. This must be an integer between 0 and 255. Default is 1. Set to 0 to disable reconnecting on idle connection failures.&#34;)] @attr [Category(&#34;ConnectionResilency&#34;)] |
| connect_retry_interval | [int32](#int32) |  | Summary: [Supported in the .NET Framework 4.5.1 and later versions] Amount of time (in seconds) between each reconnection attempt after identifying that there was an idle connection failure. This must be an integer between 1 and 60. The default is 10 seconds. An System.ArgumentException will be thrown if set to a value outside of the allowed range.

Returns: Amount of time (in seconds) between each reconnection attempt after identifying that there was an idle connection failure. @attr [Description(&#34;.NET Framework 4.5.1 and later versions. Amount of time (in seconds) between each reconnection attempt after identifying that there was an idle connection failure. This must be an integer between 1 and 60. The default is 10 seconds.&#34;)] @attr [Category(&#34;ConnectionResilency&#34;)] |
| min_pool_size | [int32](#int32) |  | Summary: Gets or sets the minimum number of connections allowed in the connection pool for this specific connection string.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MinPoolSize property, or 0 if none has been supplied. @attr [Description(&#34;The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MinPoolSize property, or 0 if none has been supplied.&#34;)] @attr [Category(&#34;Pooling&#34;)] |
| multiple_active_result_sets | [bool](#bool) |  | Summary: When true, an application can maintain multiple active result sets (MARS). When false, an application must process or cancel all result sets from one batch before it can execute any other batch on that connection.For more information, see Multiple Active Result Sets (MARS).

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.MultipleActiveResultSets property, or false if none has been supplied. @attr [Description(&#34;When true, an application can maintain multiple active result sets (MARS). When false, an application must process or cancel all result sets from one batch before it can execute any other batch on that connection.&#34;)] @attr [CategoryAttribute(&#34;Advanced&#34;)] |
| multi_subnet_failover | [bool](#bool) |  | Summary: If your application is connecting to an AlwaysOn availability group (AG) on different subnets, setting MultiSubnetFailover=true provides faster detection of and connection to the (currently) active server. For more information about SqlClient support for Always On Availability Groups, see SqlClient Support for High Availability, Disaster Recovery.

Returns: Returns System.Boolean indicating the current value of the property. @attr [Description(&#34;If your application is connecting to an AlwaysOn availability group (AG) on different subnets, setting MultiSubnetFailover=true provides faster detection of and connection to the (currently) active server. For more information about SqlClient support for Always On Availability Groups, see SqlClient Support for High Availability, Disaster Recovery.&#34;)] @attr [Category(&#34;Source&#34;)] |
| transparent_network_i_p_resolution | [bool](#bool) |  | Summary: When the value of this key is set to true, the application is required to retrieve all IP addresses for a particular DNS entry and attempt to connect with the first one in the list. If the connection is not established within 0.5 seconds, the application will try to connect to all others in parallel. When the first answers, the application will establish the connection with the respondent IP address.

Returns: A boolean value. @attr [Description(&#34;When the value of this key is set to true, the application is required to retrieve all IP addresses for a particular DNS entry and attempt to connect with the first one in the list. If the connection is not established within 0.5 seconds, the application will try to connect to all others in parallel. When the first answers, the application will establish the connection with the respondent IP address.&#34;)] @attr [Category(&#34;Source&#34;)] |
| network_library | [string](#string) |  | Summary: Gets or sets a string that contains the name of the network library used to establish a connection to the SQL Server.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.NetworkLibrary property, or String.Empty if none has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Category(&#34;Advanced&#34;)] |
| PacketSize | [int32](#int32) |  | Summary: Gets or sets the size in bytes of the network packets used to communicate with an instance of SQL Server.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.PacketSize property, or 8000 if none has been supplied. @attr [Description(&#34;The value of the System.Data.SqlClient.SqlConnectionStringBuilder.PacketSize property, or 8000 if none has been supplied.&#34;)] @attr [Category(&#34;Advanced&#34;)] |
| persist_security_info | [bool](#bool) |  | Summary: Gets or sets a Boolean value that indicates if security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.PersistSecurityInfo property, or false if none has been supplied. @attr [Description(&#34;Gets or sets a Boolean value that indicates if security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.&#34;)] @attr [Category(&#34;Security&#34;)] |
| load_balance_timeout | [int32](#int32) |  | Summary: Gets or sets the minimum time, in seconds, for the connection to live in the connection pool before being destroyed.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.LoadBalanceTimeout property, or 0 if none has been supplied. @attr [Description(&#34;Gets or sets the minimum time, in seconds, for the connection to live in the connection pool before being destroyed.&#34;)] @attr [Category(&#34;Pooling&#34;)] |
| pooling | [bool](#bool) |  | Summary: Gets or sets a Boolean value that indicates whether the connection will be pooled or explicitly opened every time that the connection is requested.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Pooling property, or true if none has been supplied. @attr [Description(&#34;Gets or sets a Boolean value that indicates whether the connection will be pooled or explicitly opened every time that the connection is requested.&#34;)] @attr [Category(&#34;Pooling&#34;)] |
| replication | [bool](#bool) |  | Summary: Gets or sets a Boolean value that indicates whether replication is supported using the connection.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Replication property, or false if none has been supplied. @attr [Description(&#34;System.Data.SqlClient.SqlConnectionStringBuilder.Replication&#34;)] @attr [Category(&#34;Replication&#34;)] |
| transaction_binding | [string](#string) |  | Summary: Gets or sets a string value that indicates how the connection maintains its association with an enlisted System.Transactions transaction.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.TransactionBinding property, or String.Empty if none has been supplied. @attr [Description(&#34;System.Data.SqlClient.SqlConnectionStringBuilder.TransactionBinding&#34;)] @attr [Category(&#34;Advanced&#34;)] |
| type_system_version | [string](#string) |  | Summary: Gets or sets a string value that indicates the type system the application expects.

Returns: The following table shows the possible values for the System.Data.SqlClient.SqlConnectionStringBuilder.TypeSystemVersion property:ValueDescription SQL Server 2005Uses the SQL Server 2005 type system. No conversions are made for the current version of ADO.NET.SQL Server 2008Uses the SQL Server 2008 type system.LatestUse the latest version than this client-server pair can handle. This will automatically move forward as the client and server components are upgraded. @attr [Category(&#34;Advanced&#34;)] |
| user_i_d | [string](#string) |  | Summary: Gets or sets the user ID to be used when connecting to SQL Server.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.UserID property, or String.Empty if none has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Category(&#34;Security&#34;)] |
| user_instance | [bool](#bool) |  | Summary: Gets or sets a value that indicates whether to redirect the connection from the default SQL Server Express instance to a runtime-initiated instance running under the account of the caller.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.UserInstance property, or False if none has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Category(&#34;Source&#34;)] |
| workstation_i_d | [string](#string) |  | Summary: Gets or sets the name of the workstation connecting to SQL Server.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.WorkstationID property, or String.Empty if none has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Category(&#34;Context&#34;)] |
| password | [string](#string) |  | Summary: Gets or sets the password for the SQL Server account.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Password property, or String.Empty if none has been supplied.

Exceptions: T:System.ArgumentNullException: The password was incorrectly set to null. See code sample below. @attr [Category(&#34;Security&#34;)] |
| authentication | [SqlAuthenticationMethod](#proto_ms_sql_design_generator_settings.SqlAuthenticationMethod) |  | Summary: Gets the authentication of the connection string.

Returns: The authentication of the connection string. @attr [Category(&#34;Security&#34;)] |
| initial_catalog | [string](#string) |  | Summary: Gets or sets the name of the database associated with the connection.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.InitialCatalog property, or String.Empty if none has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Description(&#34;Gets or sets the name of the database associated with the connection.&#34;)] @attr [Category(&#34;Source&#34;)] |
| ApplicationIntentValue | [ApplicationIntent](#proto_ms_sql_design_generator_settings.ApplicationIntent) |  | Summary: Declares the application workload type when connecting to a database in an SQL Server Availability Group. You can set the value of this property with System.Data.SqlClient.ApplicationIntent. For more information about SqlClient support for Always On Availability Groups, see SqlClient Support for High Availability, Disaster Recovery.

Returns: Returns the current value of the property (a value of type System.Data.SqlClient.ApplicationIntent). @attr [Description(&#34;Declares the application workload type when connecting to a database in an SQL Server Availability Group. You can set the value of this property with System.Data.SqlClient.ApplicationIntent.&#34;)] @attr [Category(&#34;Initialization&#34;)] |
| ApplicationName | [string](#string) |  | Summary: Gets or sets the name of the application associated with the connection string.

Returns: The name of the application, or &#34;.NET SqlClient Data Provider&#34; if no name has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Description(&#34;The name of the application, or \&#34;.NET SqlClient Data Provider\&#34; if no name has been supplied.&#34;)] @attr [Category(&#34;Context&#34;)] |
| AsynchronousProcessing | [bool](#bool) |  | Summary: Gets or sets a Boolean value that indicates whether asynchronous processing is allowed by the connection created by using this connection string.

Returns: This property is ignored beginning in .NET Framework 4.5. For more information about SqlClient support for asynchronous programming, see Asynchronous Programming.The value of the System.Data.SqlClient.SqlConnectionStringBuilder.AsynchronousProcessing property, or false if no value has been supplied. @attr [Description(&#34;This property is ignored beginning in .NET Framework 4.5.&#34;)] @attr [Category(&#34;Initialization&#34;)] |
| IntegratedSecurity | [bool](#bool) |  | Summary: Gets or sets a Boolean value that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true).

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.IntegratedSecurity property, or false if none has been supplied. @attr [Description(&#34;Gets or sets a Boolean value that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true).&#34;)] @attr [Category(&#34;Security&#34;)] |
| ContextConnection | [bool](#bool) |  | Summary: Gets or sets a value that indicates whether a client/server or in-process connection to SQL Server should be made.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ContextConnection property, or False if none has been supplied. @attr [Description(&#34;System.Data.SqlClient.SqlConnectionStringBuilder.ContextConnection&#34;)] @attr [Category(&#34;Source&#34;)] |
| ConnectTimeout | [int32](#int32) |  | Summary: Gets or sets the length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ConnectTimeout property, or 15 seconds if no value has been supplied. @attr [Description(&#34;The value of the System.Data.SqlClient.SqlConnectionStringBuilder.ConnectTimeout property, or 15 seconds if no value has been supplied.&#34;)] @attr [Category(&#34;Initialization&#34;)] |
| attach_d_b_filename | [string](#string) |  | Summary: Gets or sets a string that contains the name of the primary data file. This includes the full path name of an attachable database.

Returns: The value of the AttachDBFilename property, or String.Empty if no value has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Description(&#34;Gets or sets a string that contains the name of the primary data file. This includes the full path name of an attachable database.&#34;)] @attr [Editor(typeof(FilePickerEditor), typeof(ITypeEditor))] @attr [Category(&#34;Source&#34;)] |
| data_source | [string](#string) |  | Summary: Gets or sets the name or network address of the instance of SQL Server to connect to.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.DataSource property, or String.Empty if none has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Description(&#34;Gets or sets the name or network address of the instance of SQL Server to connect to.&#34;)] @attr [Category(&#34;Source&#34;)] |
| encrypt | [bool](#bool) |  | Summary: Gets or sets a Boolean value that indicates whether SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Encrypt property, or false if none has been supplied. @attr [Description(&#34;Gets or sets a Boolean value that indicates whether SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.&#34;)] @attr [Category(&#34;Security&#34;)] |
| ColumnEncryptionSetting | [SqlConnectionColumnEncryptionSetting](#proto_ms_sql_design_generator_settings.SqlConnectionColumnEncryptionSetting) |  | Summary: Gets and sets the column encryption settings for the connection string builder.

Returns: The column encryption settings for the connection string builder. @attr [Description(&#34;Gets and sets the column encryption settings for the connection string builder.&#34;)] @attr [Category(&#34;Security&#34;)] |
| trust_server_certificate | [bool](#bool) |  | Summary: Gets or sets a value that indicates whether the channel will be encrypted while bypassing walking the certificate chain to validate trust.

Returns: A Boolean. Recognized values are true, false, yes, and no. @attr [Description(&#34;Gets or sets a value that indicates whether the channel will be encrypted while bypassing walking the certificate chain to validate trust.&#34;)] @attr [Category(&#34;Security&#34;)] |
| enlist | [bool](#bool) |  | Summary: Gets or sets a Boolean value that indicates whether the SQL Server connection pooler automatically enlists the connection in the creation thread&#39;s current transaction context.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.Enlist property, or true if none has been supplied. @attr [Description(&#34;Gets or sets a Boolean value that indicates whether the SQL Server connection pooler automatically enlists the connection in the creation thread&#39;s current transaction context.&#34;)] @attr [Category(&#34;Pooling&#34;)] |
| failover_partner | [string](#string) |  | Summary: Gets or sets the name or address of the partner server to connect to if the primary server is down.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.FailoverPartner property, or String.Empty if none has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Description(&#34;Gets or sets the name or address of the partner server to connect to if the primary server is down.&#34;)] @attr [Category(&#34;Source&#34;)] |
| CurrentLanguage | [string](#string) |  | Summary: Gets or sets the SQL Server Language record name.

Returns: The value of the System.Data.SqlClient.SqlConnectionStringBuilder.CurrentLanguage property, or String.Empty if no value has been supplied.

Exceptions: T:System.ArgumentNullException: To set the value to null, use System.DBNull.Value. @attr [Description(&#34;Gets or sets the SQL Server Language record name.&#34;)] @attr [Category(&#34;Initialization&#34;)] |






<a name="proto_ms_sql_design_generator_settings.proto_ms_sql_design_generator_settings"></a>

### proto_ms_sql_design_generator_settings
@base : ViewModelValidatableWithSeverity&lt;MsSqlDesignGeneratorSettings, MsSqlDesignGeneratorSettings.MsSqlDesignGeneratorSettingsValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| guid | [string](#string) |  |  |
| is_use_foreingkey | [bool](#bool) |  |  |
| is_use_foreingkey_index | [bool](#bool) |  |  |
| is_use_storage_procedures | [bool](#bool) |  |  |
| is_use_views | [bool](#bool) |  |  |
| is_primary_key_clustered | [bool](#bool) |  | MsSql |
| is_memory_optimized | [bool](#bool) |  | MsSql |





 


<a name="proto_ms_sql_design_generator_settings.ApplicationIntent"></a>

### ApplicationIntent
Summary:
    Specifies a value for System.Data.SqlClient.SqlConnectionStringBuilder.ApplicationIntent.
    Possible values are ReadWrite and ReadOnly.

| Name | Number | Description |
| ---- | ------ | ----------- |
| ReadWrite | 0 | Summary: The application workload type when connecting to a server is read write. |
| ReadOnly | 1 | Summary: The application workload type when connecting to a server is read only. |



<a name="proto_ms_sql_design_generator_settings.SqlAuthenticationMethod"></a>

### SqlAuthenticationMethod
Summary:
    Describes the different SQL authentication methods that can be used by a client
    connecting to Azure SQL Database. For details, see Connecting to SQL Database

    By Using Azure Active Directory Authentication.

| Name | Number | Description |
| ---- | ------ | ----------- |
| NotSpecified | 0 | Summary: The authentication method is not specified. |
| SqlPassword | 1 | Summary: The authentication method is Sql Password. |
| ActiveDirectoryPassword | 2 | Summary: The authentication method uses Active Directory Password. Use Active Directory Password to connect to a SQL Database using an Azure AD principal name and password. |
| ActiveDirectoryIntegrated | 3 | Summary: The authentication method uses Active Directory Integrated. Use Active Directory Integrated to connect to a SQL Database using integrated Windows authentication. |



<a name="proto_ms_sql_design_generator_settings.SqlConnectionColumnEncryptionSetting"></a>

### SqlConnectionColumnEncryptionSetting
Summary:
    Specifies that Always Encrypted functionality is enabled in a connection. Note
    that these settings cannot be used to bypass encryption and gain access to plaintext
    data. For details, see Always Encrypted (Database Engine).

| Name | Number | Description |
| ---- | ------ | ----------- |
| Disabled | 0 | Summary: Specifies the connection does not use Always Encrypted. Should be used if no queries sent over the connection access encrypted columns. |
| Enabled | 1 | Summary: Enables Always Encrypted functionality for the connection. Query parameters that correspond to encrypted columns will be transparently encrypted and query results from encrypted columns will be transparently decrypted. |


 

 

 



## Scalar Value Types

| .proto Type | Notes | C++ Type | Java Type | Python Type |
| ----------- | ----- | -------- | --------- | ----------- |
| <a name="double" /> double |  | double | double | float |
| <a name="float" /> float |  | float | float | float |
| <a name="int32" /> int32 | Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint32 instead. | int32 | int | int |
| <a name="int64" /> int64 | Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint64 instead. | int64 | long | int/long |
| <a name="uint32" /> uint32 | Uses variable-length encoding. | uint32 | int | int/long |
| <a name="uint64" /> uint64 | Uses variable-length encoding. | uint64 | long | int/long |
| <a name="sint32" /> sint32 | Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int32s. | int32 | int | int |
| <a name="sint64" /> sint64 | Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int64s. | int64 | long | int/long |
| <a name="fixed32" /> fixed32 | Always four bytes. More efficient than uint32 if values are often greater than 2^28. | uint32 | int | int |
| <a name="fixed64" /> fixed64 | Always eight bytes. More efficient than uint64 if values are often greater than 2^56. | uint64 | long | int/long |
| <a name="sfixed32" /> sfixed32 | Always four bytes. | int32 | int | int |
| <a name="sfixed64" /> sfixed64 | Always eight bytes. | int64 | long | int/long |
| <a name="bool" /> bool |  | bool | boolean | boolean |
| <a name="string" /> string | A string must always contain UTF-8 encoded or 7-bit ASCII text. | string | String | str/unicode |
| <a name="bytes" /> bytes | May contain any arbitrary sequence of bytes. | string | ByteString | str |

