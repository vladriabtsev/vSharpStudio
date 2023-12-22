# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [plugin_sample2.proto](#plugin_sample2-proto)
    - [proto_db_connection_string_settings2](#proto_plugin_sample-proto_db_connection_string_settings2)
    - [proto_generator_db_access_settings2](#proto_plugin_sample-proto_generator_db_access_settings2)
  
- [Scalar Value Types](#scalar-value-types)



<a name="plugin_sample2-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## plugin_sample2.proto



<a name="proto_plugin_sample-proto_db_connection_string_settings2"></a>

### proto_db_connection_string_settings2
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| string_settings | [string](#string) |  |  |






<a name="proto_plugin_sample-proto_generator_db_access_settings2"></a>

### proto_generator_db_access_settings2
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_access_param1 | [bool](#bool) |  |  |
| is_access_param2 | [google.protobuf.BoolValue](#google-protobuf-BoolValue) |  |  |
| access_param3 | [string](#string) |  |  |
| access_param4 | [google.protobuf.StringValue](#google-protobuf-StringValue) |  |  |
| is_generate_not_valid_code | [bool](#bool) |  |  |





 

 

 

 



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

