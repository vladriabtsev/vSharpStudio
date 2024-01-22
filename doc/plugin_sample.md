# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [plugin_sample.proto](#plugin_sample-proto)
    - [proto_db_connection_string_settings](#proto_plugin_sample-proto_db_connection_string_settings)
    - [proto_generator_db_access_node_settings](#proto_plugin_sample-proto_generator_db_access_node_settings)
    - [proto_generator_db_access_settings](#proto_plugin_sample-proto_generator_db_access_settings)
    - [proto_generator_db_schema_node_settings](#proto_plugin_sample-proto_generator_db_schema_node_settings)
    - [proto_generator_db_schema_settings](#proto_plugin_sample-proto_generator_db_schema_settings)
    - [proto_plugins_group_project_settings](#proto_plugin_sample-proto_plugins_group_project_settings)
    - [proto_plugins_group_solution_settings](#proto_plugin_sample-proto_plugins_group_solution_settings)
    - [proto_plugins_group_solution_sub_settings](#proto_plugin_sample-proto_plugins_group_solution_sub_settings)
  
- [Scalar Value Types](#scalar-value-types)



<a name="plugin_sample-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## plugin_sample.proto



<a name="proto_plugin_sample-proto_db_connection_string_settings"></a>

### proto_db_connection_string_settings
&lt;summary&gt;
/     Represents the caching modes that can be used when creating a new &lt;see cref=&#34;SqliteConnection&#34; /&gt;.
/ &lt;/summary&gt;
/ &lt;seealso href=&#34;http://sqlite.org/sharedcache.html&#34;&gt;SQLite Shared-Cache Mode&lt;/seealso&gt;
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| string_settings | [string](#string) |  | Represents the caching modes that can be used when creating a new &lt;see cref=&#34;SqliteConnection&#34; /&gt;. |






<a name="proto_plugin_sample-proto_generator_db_access_node_settings"></a>

### proto_generator_db_access_node_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_param1 | [bool](#bool) |  |  |
| is_included | [google.protobuf.BoolValue](#google-protobuf-BoolValue) |  |  |
| is_property_param1 | [bool](#bool) |  |  |
| is_catalog_form_param1 | [bool](#bool) |  |  |






<a name="proto_plugin_sample-proto_generator_db_access_settings"></a>

### proto_generator_db_access_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_access_param1 | [bool](#bool) |  |  |
| is_access_param2 | [google.protobuf.BoolValue](#google-protobuf-BoolValue) |  |  |
| access_param3 | [string](#string) |  |  |
| access_param4 | [google.protobuf.StringValue](#google-protobuf-StringValue) |  |  |
| is_generate_not_valid_code | [bool](#bool) |  |  |






<a name="proto_plugin_sample-proto_generator_db_schema_node_settings"></a>

### proto_generator_db_schema_node_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_param1 | [bool](#bool) |  |  |
| is_included | [google.protobuf.BoolValue](#google-protobuf-BoolValue) |  |  |
| is_constant_param1 | [bool](#bool) |  |  |
| is_catalog_form_param1 | [bool](#bool) |  |  |






<a name="proto_plugin_sample-proto_generator_db_schema_settings"></a>

### proto_generator_db_schema_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_schema_param1 | [bool](#bool) |  |  |
| is_schema_param2 | [google.protobuf.BoolValue](#google-protobuf-BoolValue) |  |  |
| schema_param3 | [string](#string) |  |  |






<a name="proto_plugin_sample-proto_plugins_group_project_settings"></a>

### proto_plugins_group_project_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_group_project_param1 | [bool](#bool) |  |  |






<a name="proto_plugin_sample-proto_plugins_group_solution_settings"></a>

### proto_plugins_group_solution_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_group_param1 | [bool](#bool) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Param1&#34;)] @attr [Description(&#34;Sample of Param1&#34;)] |
| sub_settings | [proto_plugins_group_solution_sub_settings](#proto_plugin_sample-proto_plugins_group_solution_sub_settings) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;Sub Settings&#34;)] @attr [Description(&#34;Sample of Sub Settings&#34;)] @attr [ExpandableObjectAttribute()] |






<a name="proto_plugin_sample-proto_plugins_group_solution_sub_settings"></a>

### proto_plugins_group_solution_sub_settings
@base BaseSubSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_sub_param1 | [bool](#bool) |  |  |
| is_sub_param2 | [bool](#bool) |  |  |





 

 

 

 



## Scalar Value Types

| .proto Type | Notes | C++ | Java | Python | Go | C# | PHP | Ruby |
| ----------- | ----- | --- | ---- | ------ | -- | -- | --- | ---- |
| <a name="double" /> double |  | double | double | float | float64 | double | float | Float |
| <a name="float" /> float |  | float | float | float | float32 | float | float | Float |
| <a name="int32" /> int32 | Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint32 instead. | int32 | int | int | int32 | int | integer | Bignum or Fixnum (as required) |
| <a name="int64" /> int64 | Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint64 instead. | int64 | long | int/long | int64 | long | integer/string | Bignum |
| <a name="uint32" /> uint32 | Uses variable-length encoding. | uint32 | int | int/long | uint32 | uint | integer | Bignum or Fixnum (as required) |
| <a name="uint64" /> uint64 | Uses variable-length encoding. | uint64 | long | int/long | uint64 | ulong | integer/string | Bignum or Fixnum (as required) |
| <a name="sint32" /> sint32 | Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int32s. | int32 | int | int | int32 | int | integer | Bignum or Fixnum (as required) |
| <a name="sint64" /> sint64 | Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int64s. | int64 | long | int/long | int64 | long | integer/string | Bignum |
| <a name="fixed32" /> fixed32 | Always four bytes. More efficient than uint32 if values are often greater than 2^28. | uint32 | int | int | uint32 | uint | integer | Bignum or Fixnum (as required) |
| <a name="fixed64" /> fixed64 | Always eight bytes. More efficient than uint64 if values are often greater than 2^56. | uint64 | long | int/long | uint64 | ulong | integer/string | Bignum |
| <a name="sfixed32" /> sfixed32 | Always four bytes. | int32 | int | int | int32 | int | integer | Bignum or Fixnum (as required) |
| <a name="sfixed64" /> sfixed64 | Always eight bytes. | int64 | long | int/long | int64 | long | integer/string | Bignum |
| <a name="bool" /> bool |  | bool | boolean | boolean | bool | bool | boolean | TrueClass/FalseClass |
| <a name="string" /> string | A string must always contain UTF-8 encoded or 7-bit ASCII text. | string | String | str/unicode | string | string | string | String (UTF-8) |
| <a name="bytes" /> bytes | May contain any arbitrary sequence of bytes. | string | ByteString | str | []byte | ByteString | string | String (ASCII-8BIT) |

