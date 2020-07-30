# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [plugin_sample.proto](#plugin_sample.proto)
    - [bool_nullable](#proto_plugin_sample.bool_nullable)
    - [proto_generator_db_access_node_catalog_form_settings](#proto_plugin_sample.proto_generator_db_access_node_catalog_form_settings)
    - [proto_generator_db_access_node_property_settings](#proto_plugin_sample.proto_generator_db_access_node_property_settings)
    - [proto_generator_db_access_node_settings](#proto_plugin_sample.proto_generator_db_access_node_settings)
    - [proto_generator_db_access_settings](#proto_plugin_sample.proto_generator_db_access_settings)
    - [proto_generator_db_schema_settings](#proto_plugin_sample.proto_generator_db_schema_settings)
    - [proto_plugins_group_settings](#proto_plugin_sample.proto_plugins_group_settings)
    - [string_nullable](#proto_plugin_sample.string_nullable)
  
  
  
  

- [Scalar Value Types](#scalar-value-types)



<a name="plugin_sample.proto"></a>
<p align="right"><a href="#top">Top</a></p>

## plugin_sample.proto



<a name="proto_plugin_sample.bool_nullable"></a>

### bool_nullable
all simple nullable (generator check suffics &#39;_nullable&#39;)


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| has_value | [bool](#bool) |  |  |
| value | [bool](#bool) |  |  |






<a name="proto_plugin_sample.proto_generator_db_access_node_catalog_form_settings"></a>

### proto_generator_db_access_node_catalog_form_settings
@base ViewModelValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_catalog_form_param1 | [bool](#bool) |  |  |






<a name="proto_plugin_sample.proto_generator_db_access_node_property_settings"></a>

### proto_generator_db_access_node_property_settings
@base ViewModelValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_property_param1 | [bool](#bool) |  |  |






<a name="proto_plugin_sample.proto_generator_db_access_node_settings"></a>

### proto_generator_db_access_node_settings
@base ViewModelValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_param1 | [bool](#bool) |  |  |
| is_included | [bool_nullable](#proto_plugin_sample.bool_nullable) |  |  |






<a name="proto_plugin_sample.proto_generator_db_access_settings"></a>

### proto_generator_db_access_settings
@base ViewModelValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_access_param1 | [bool](#bool) |  |  |
| is_access_param2 | [bool_nullable](#proto_plugin_sample.bool_nullable) |  |  |
| access_param3 | [string](#string) |  |  |
| access_param4 | [string_nullable](#proto_plugin_sample.string_nullable) |  |  |
| is_generate_not_valid_code | [bool](#bool) |  |  |






<a name="proto_plugin_sample.proto_generator_db_schema_settings"></a>

### proto_generator_db_schema_settings
@base ViewModelValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_schema_param1 | [bool](#bool) |  |  |
| is_schema_param2 | [bool_nullable](#proto_plugin_sample.bool_nullable) |  |  |
| schema_param3 | [string](#string) |  |  |






<a name="proto_plugin_sample.proto_plugins_group_settings"></a>

### proto_plugins_group_settings
@base ViewModelValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_group_param1 | [bool](#bool) |  |  |






<a name="proto_plugin_sample.string_nullable"></a>

### string_nullable



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| has_value | [bool](#bool) |  |  |
| value | [string](#string) |  |  |





 

 

 

 



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

