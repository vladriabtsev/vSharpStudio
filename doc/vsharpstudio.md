# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [vsharpstudio.proto](#vsharpstudio.proto)
    - [bool_nullable](#proto_config.bool_nullable)
    - [db_id_generator](#proto_config.db_id_generator)
    - [proto_catalog](#proto_config.proto_catalog)
    - [proto_config](#proto_config.proto_config)
    - [proto_config_tree](#proto_config.proto_config_tree)
    - [proto_constant](#proto_config.proto_constant)
    - [proto_data_type](#proto_config.proto_data_type)
    - [proto_document](#proto_config.proto_document)
    - [proto_enumeration](#proto_config.proto_enumeration)
    - [proto_enumeration_pair](#proto_config.proto_enumeration_pair)
    - [proto_form](#proto_config.proto_form)
    - [proto_group_configs](#proto_config.proto_group_configs)
    - [proto_group_documents](#proto_config.proto_group_documents)
    - [proto_group_list_catalogs](#proto_config.proto_group_list_catalogs)
    - [proto_group_list_constants](#proto_config.proto_group_list_constants)
    - [proto_group_list_documents](#proto_config.proto_group_list_documents)
    - [proto_group_list_enumerations](#proto_config.proto_group_list_enumerations)
    - [proto_group_list_forms](#proto_config.proto_group_list_forms)
    - [proto_group_list_journals](#proto_config.proto_group_list_journals)
    - [proto_group_list_properties](#proto_config.proto_group_list_properties)
    - [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs)
    - [proto_group_list_reports](#proto_config.proto_group_list_reports)
    - [proto_group_properties_tab](#proto_config.proto_group_properties_tab)
    - [proto_group_settings](#proto_config.proto_group_settings)
    - [proto_journal](#proto_config.proto_journal)
    - [proto_property](#proto_config.proto_property)
    - [proto_report](#proto_config.proto_report)
    - [proto_settings_config](#proto_config.proto_settings_config)
    - [proto_settings_db](#proto_config.proto_settings_db)
    - [proto_settings_for_db_plugin](#proto_config.proto_settings_for_db_plugin)
    - [string_nullable](#proto_config.string_nullable)
  
    - [proto_config.EnumDbType](#proto_config.proto_config.EnumDbType)
    - [proto_data_type.EnumDataType](#proto_config.proto_data_type.EnumDataType)
    - [proto_enumeration.EnumEnumerationType](#proto_config.proto_enumeration.EnumEnumerationType)
  
  
  

- [Scalar Value Types](#scalar-value-types)



<a name="vsharpstudio.proto"></a>
<p align="right"><a href="#top">Top</a></p>

## vsharpstudio.proto



<a name="proto_config.bool_nullable"></a>

### bool_nullable
all simple nullable (generator check suffics &#39;_nullable&#39;)


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| has_value | [bool](#bool) |  |  |
| value | [bool](#bool) |  |  |






<a name="proto_config.db_id_generator"></a>

### db_id_generator
Primary key generation strategy
@base : ViewModelValidatableWithSeverity&lt;DbIdGenerator, DbIdGenerator.DbIdGeneratorValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| is_primary_key_clustered | [bool_nullable](#proto_config.bool_nullable) |  | MsSql |
| is_memory_optimized | [bool_nullable](#proto_config.bool_nullable) |  | MsSql |
| is_sequence_hi_lo | [bool_nullable](#proto_config.bool_nullable) |  | SequenceHiLo or IdentityColumn. MsSql |
| hi_lo_sequence_name | [string](#string) |  | MsSql |
| hi_lo_schema | [string](#string) |  | MsSql |






<a name="proto_config.proto_catalog"></a>

### proto_catalog



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| db_id_generator | [db_id_generator](#proto_config.db_id_generator) |  | @attr [ExpandableObjectAttribute()] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [BrowsableAttribute(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_config"></a>

### proto_config
Configuration config


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | Unique Guid for configuration (for comparison) |
| version | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| is_db_from_connection_string | [bool](#bool) |  | if yes: Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name. if no: 1. Find DB type from 2. Create connection string from db_server, db_database_name, db_user |
| connection_string_name | [string](#string) |  |  |
| db_type_enum | [proto_config.EnumDbType](#proto_config.proto_config.EnumDbType) |  |  |
| db_server | [string](#string) |  |  |
| db_database_name | [string](#string) |  |  |
| is_db_windows_authentication | [bool](#bool) |  |  |
| db_user | [string](#string) |  |  |
| db_password | [string](#string) |  |  |
| path_to_project_with_connection_string | [string](#string) |  | path to project with config file containing connection string. Usefull for UNIT tests. it will override previous settings |
| db_schema | [string](#string) |  |  |
| primary_key_name | [string](#string) |  |  |
| db_id_generator | [db_id_generator](#proto_config.db_id_generator) |  | @attr [ExpandableObjectAttribute()] |
| group_settings | [proto_group_settings](#proto_config.proto_group_settings) |  | CONFIG OBJECTS |
| group_configs | [proto_group_configs](#proto_config.proto_group_configs) |  |  |
| group_constants | [proto_group_list_constants](#proto_config.proto_group_list_constants) |  |  |
| group_enumerations | [proto_group_list_enumerations](#proto_config.proto_group_list_enumerations) |  |  |
| group_catalogs | [proto_group_list_catalogs](#proto_config.proto_group_list_catalogs) |  |  |
| group_documents | [proto_group_documents](#proto_config.proto_group_documents) |  |  |
| group_journals | [proto_group_list_journals](#proto_config.proto_group_list_journals) |  |  |






<a name="proto_config.proto_config_tree"></a>

### proto_config_tree



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| description | [string](#string) |  | string name_ui = 4; |
| config_node | [proto_config](#proto_config.proto_config) |  |  |
| children | [proto_config_tree](#proto_config.proto_config_tree) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_constant"></a>

### proto_constant
Constant application wise value


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [PropertyOrderAttribute(4)][ExpandableObjectAttribute()][DisplayName(&#34;Type&#34;)] |






<a name="proto_config.proto_data_type"></a>

### proto_data_type
@base : ViewModelValidatableWithSeverity&lt;DataType, DataType.DataTypeValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data_type_enum | [proto_data_type.EnumDataType](#proto_config.proto_data_type.EnumDataType) |  | @attr [PropertyOrderAttribute(1)][DisplayName(&#34;Type&#34;)] |
| length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(3)] |
| accuracy | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(5)] |
| is_positive | [bool](#bool) |  | @attr [PropertyOrderAttribute(4)][DisplayName(&#34;Is positive&#34;)] |
| object_guid | [string](#string) |  | @attr [PropertyOrderAttribute(2)][Editor(typeof(DataTypeObjectNameEditor), typeof(DataTypeObjectNameEditor))] |






<a name="proto_config.proto_document"></a>

### proto_document



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [BrowsableAttribute(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [BrowsableAttribute(false)] |
| db_id_generator | [db_id_generator](#proto_config.db_id_generator) |  | @attr [ExpandableObjectAttribute()] |






<a name="proto_config.proto_enumeration"></a>

### proto_enumeration



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type_enum | [proto_enumeration.EnumEnumerationType](#proto_config.proto_enumeration.EnumEnumerationType) |  | @attr [PropertyOrderAttribute(4)][DisplayName(&#34;Type&#34;)] |
| children | [proto_enumeration_pair](#proto_config.proto_enumeration_pair) | repeated | @attr [DisplayName(&#34;Elements&#34;)][NewItemTypes(typeof(EnumerationPair))] |






<a name="proto_config.proto_enumeration_pair"></a>

### proto_enumeration_pair



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| value | [string](#string) |  | TODO struct for different types, at least INTEGER |






<a name="proto_config.proto_form"></a>

### proto_form



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)]

repeated proto_group_properties list_properties = 6; repeated proto_document list_forms = 7; |






<a name="proto_config.proto_group_configs"></a>

### proto_group_configs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| description | [string](#string) |  | string name_ui = 4; |
| children | [proto_config_tree](#proto_config.proto_config_tree) | repeated | @attr [BrowsableAttribute(false)] |
| relative_config_path | [string](#string) |  |  |






<a name="proto_config.proto_group_documents"></a>

### proto_group_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_shared_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_list_documents | [proto_group_list_documents](#proto_config.proto_group_list_documents) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_catalogs"></a>

### proto_group_list_catalogs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_catalog](#proto_config.proto_catalog) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_constants"></a>

### proto_group_list_constants



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_constant](#proto_config.proto_constant) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_documents"></a>

### proto_group_list_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_document](#proto_config.proto_document) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_enumerations"></a>

### proto_group_list_enumerations



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_enumeration](#proto_config.proto_enumeration) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_forms"></a>

### proto_group_list_forms



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_form](#proto_config.proto_form) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_journals"></a>

### proto_group_list_journals



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_journal](#proto_config.proto_journal) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_properties"></a>

### proto_group_list_properties



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_property](#proto_config.proto_property) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_properties_tabs"></a>

### proto_group_list_properties_tabs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_group_properties_tab](#proto_config.proto_group_properties_tab) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_reports"></a>

### proto_group_list_reports



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_report](#proto_config.proto_report) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_properties_tab"></a>

### proto_group_properties_tab



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_subtabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_settings"></a>

### proto_group_settings



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [BrowsableAttribute(false)] |
| sorting_value | [uint64](#uint64) |  |  |
| config | [proto_settings_config](#proto_config.proto_settings_config) |  | @attr [PropertyOrderAttribute(4)][ExpandableObjectAttribute()] |
| db_type | [proto_settings_db](#proto_config.proto_settings_db) |  | @attr [PropertyOrderAttribute(5)][ExpandableObjectAttribute()] |






<a name="proto_config.proto_journal"></a>

### proto_journal



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| children | [proto_document](#proto_config.proto_document) | repeated | repeated proto_group_properties list_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_property"></a>

### proto_property



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [PropertyOrderAttribute(4)][ExpandableObjectAttribute()][DisplayName(&#34;Type&#34;)] |






<a name="proto_config.proto_report"></a>

### proto_report



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)]

repeated proto_group_properties list_properties = 6; repeated proto_document list_documents = 7; |






<a name="proto_config.proto_settings_config"></a>

### proto_settings_config



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| version_migration_current | [int32](#int32) |  | current migration version, increased by one on each deployment |
| version_migration_support_from_min | [int32](#int32) |  | min version supported by current version for migration |






<a name="proto_config.proto_settings_db"></a>

### proto_settings_db
@bas : ViewModelValidatableWithSeverity&lt;SettingsDb, SettingsDb.SettingsDbValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| selected_db_design_plugin_guid | [string](#string) |  |  |
| selected_db_connection_guid | [string](#string) |  |  |
| list_plugin_connections | [proto_settings_for_db_plugin](#proto_config.proto_settings_for_db_plugin) | repeated |  |






<a name="proto_config.proto_settings_for_db_plugin"></a>

### proto_settings_for_db_plugin
@bas : ViewModelValidatableWithSeverity&lt;SettingsForDbPlugin, SettingsForDbPlugin.SettingsForDbPluginValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| db_design_plugin_guid | [string](#string) |  |  |
| list_db_connections | [google.protobuf.Any](#google.protobuf.Any) | repeated |  |






<a name="proto_config.string_nullable"></a>

### string_nullable



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| has_value | [bool](#bool) |  |  |
| value | [string](#string) |  |  |





 


<a name="proto_config.proto_config.EnumDbType"></a>

### proto_config.EnumDbType


| Name | Number | Description |
| ---- | ------ | ----------- |
| MS_SQL_SERVER | 0 |  |
| MS_SQL_FILE | 1 |  |



<a name="proto_config.proto_data_type.EnumDataType"></a>

### proto_data_type.EnumDataType


| Name | Number | Description |
| ---- | ------ | ----------- |
| STRING | 0 |  |
| NUMERICAL | 1 |  |
| BOOL | 5 |  |
| ENUMERATION | 8 | CONSTANT = 7; |
| CATALOG | 9 |  |
| CATALOGS | 10 |  |
| DOCUMENT | 11 |  |
| DOCUMENTS | 12 |  |
| Any | 15 |  |



<a name="proto_config.proto_enumeration.EnumEnumerationType"></a>

### proto_enumeration.EnumEnumerationType


| Name | Number | Description |
| ---- | ------ | ----------- |
| STRING | 0 |  |
| BYTE | 1 |  |
| SHORT | 2 |  |
| INTEGER | 3 |  |


 

 

 



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

