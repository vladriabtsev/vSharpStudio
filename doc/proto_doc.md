# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [proto_doc.proto](#proto_doc-proto)
    - [enums](#proto_doc-enums)
    - [extension](#proto_doc-extension)
    - [field](#proto_doc-field)
    - [file_doc](#proto_doc-file_doc)
    - [json_doc](#proto_doc-json_doc)
    - [message](#proto_doc-message)
    - [scalarValueType](#proto_doc-scalarValueType)
    - [service](#proto_doc-service)
    - [value](#proto_doc-value)
  
- [Scalar Value Types](#scalar-value-types)



<a name="proto_doc-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## proto_doc.proto



<a name="proto_doc-enums"></a>

### enums



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| longName | [string](#string) |  |  |
| fullName | [string](#string) |  |  |
| description | [string](#string) |  |  |
| values | [value](#proto_doc-value) | repeated |  |






<a name="proto_doc-extension"></a>

### extension



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| longName | [string](#string) |  |  |
| fullName | [string](#string) |  |  |
| description | [string](#string) |  |  |






<a name="proto_doc-field"></a>

### field



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| description | [string](#string) |  |  |
| label | [string](#string) |  |  |
| type | [string](#string) |  |  |
| longType | [string](#string) |  |  |
| fullType | [string](#string) |  |  |
| ismap | [bool](#bool) |  |  |
| isoneof | [bool](#bool) |  |  |
| oneofdecl | [string](#string) |  |  |
| defaultValue | [string](#string) |  |  |






<a name="proto_doc-file_doc"></a>

### file_doc



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| description | [string](#string) |  |  |
| package | [string](#string) |  |  |
| hasEnums | [bool](#bool) |  |  |
| hasExtensions | [bool](#bool) |  |  |
| hasMessages | [bool](#bool) |  |  |
| hasServices | [bool](#bool) |  |  |
| enums | [enums](#proto_doc-enums) | repeated |  |
| extensions | [extension](#proto_doc-extension) | repeated |  |
| messages | [message](#proto_doc-message) | repeated |  |
| services | [service](#proto_doc-service) | repeated |  |






<a name="proto_doc-json_doc"></a>

### json_doc



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| files | [file_doc](#proto_doc-file_doc) | repeated |  |
| scalarValueTypes | [scalarValueType](#proto_doc-scalarValueType) | repeated |  |






<a name="proto_doc-message"></a>

### message



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| longName | [string](#string) |  |  |
| fullName | [string](#string) |  |  |
| description | [string](#string) |  |  |
| hasExtensions | [bool](#bool) |  |  |
| hasFields | [bool](#bool) |  |  |
| hasOneofs | [bool](#bool) |  |  |
| extensions | [extension](#proto_doc-extension) | repeated |  |
| fields | [field](#proto_doc-field) | repeated |  |






<a name="proto_doc-scalarValueType"></a>

### scalarValueType



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| protoType | [string](#string) |  |  |
| notes | [string](#string) |  |  |
| cppType | [string](#string) |  |  |
| csType | [string](#string) |  |  |
| goType | [string](#string) |  |  |
| javaType | [string](#string) |  |  |
| phpType | [string](#string) |  |  |
| pythonType | [string](#string) |  |  |
| rubyType | [string](#string) |  |  |






<a name="proto_doc-service"></a>

### service



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| longName | [string](#string) |  |  |
| fullName | [string](#string) |  |  |
| description | [string](#string) |  |  |






<a name="proto_doc-value"></a>

### value



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  |  |
| number | [string](#string) |  |  |
| description | [string](#string) |  |  |





 

 

 

 



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

