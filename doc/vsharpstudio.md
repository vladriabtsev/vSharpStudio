# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [vsharpstudio.proto](#vsharpstudio.proto)
    - [bool_nullable](#proto_config.bool_nullable)
    - [db_settings](#proto_config.db_settings)
    - [proto_app_db_settings](#proto_config.proto_app_db_settings)
    - [proto_app_project](#proto_config.proto_app_project)
    - [proto_app_solution](#proto_config.proto_app_solution)
    - [proto_base_config](#proto_config.proto_base_config)
    - [proto_catalog](#proto_config.proto_catalog)
    - [proto_config](#proto_config.proto_config)
    - [proto_config_short_history](#proto_config.proto_config_short_history)
    - [proto_constant](#proto_config.proto_constant)
    - [proto_data_type](#proto_config.proto_data_type)
    - [proto_document](#proto_config.proto_document)
    - [proto_enumeration](#proto_config.proto_enumeration)
    - [proto_enumeration_pair](#proto_config.proto_enumeration_pair)
    - [proto_form](#proto_config.proto_form)
    - [proto_group_documents](#proto_config.proto_group_documents)
    - [proto_group_list_app_solutions](#proto_config.proto_group_list_app_solutions)
    - [proto_group_list_base_configs](#proto_config.proto_group_list_base_configs)
    - [proto_group_list_catalogs](#proto_config.proto_group_list_catalogs)
    - [proto_group_list_common](#proto_config.proto_group_list_common)
    - [proto_group_list_constants](#proto_config.proto_group_list_constants)
    - [proto_group_list_documents](#proto_config.proto_group_list_documents)
    - [proto_group_list_enumerations](#proto_config.proto_group_list_enumerations)
    - [proto_group_list_forms](#proto_config.proto_group_list_forms)
    - [proto_group_list_journals](#proto_config.proto_group_list_journals)
    - [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms)
    - [proto_group_list_plugins](#proto_config.proto_group_list_plugins)
    - [proto_group_list_properties](#proto_config.proto_group_list_properties)
    - [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs)
    - [proto_group_list_reports](#proto_config.proto_group_list_reports)
    - [proto_group_list_roles](#proto_config.proto_group_list_roles)
    - [proto_journal](#proto_config.proto_journal)
    - [proto_main_view_form](#proto_config.proto_main_view_form)
    - [proto_plugin](#proto_config.proto_plugin)
    - [proto_plugin_generator](#proto_config.proto_plugin_generator)
    - [proto_plugin_generator_settings](#proto_config.proto_plugin_generator_settings)
    - [proto_properties_tab](#proto_config.proto_properties_tab)
    - [proto_property](#proto_config.proto_property)
    - [proto_report](#proto_config.proto_report)
    - [proto_role](#proto_config.proto_role)
    - [proto_settings_config](#proto_config.proto_settings_config)
    - [string_nullable](#proto_config.string_nullable)
  
    - [db_id_generator_method](#proto_config.db_id_generator_method)
    - [enum_enumeration_type](#proto_config.enum_enumeration_type)
    - [proto_enum_data_type](#proto_config.proto_enum_data_type)
    - [proto_enum_primary_key_type](#proto_config.proto_enum_primary_key_type)
  
  
  

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






<a name="proto_config.db_settings"></a>

### db_settings
General DB settings
@base : ViewModelValidatableWithSeverity&lt;DbSettings, DbSettings.DbSettingsValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| db_schema | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [Description(&#34;DB schema name for all object in this configuration&#34;)] |
| id_generator | [db_id_generator_method](#proto_config.db_id_generator_method) |  | @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Primary key generation method&#34;)] |
| key_type | [string](#string) |  | @attr [PropertyOrderAttribute(3)] @attr [Description(&#34;Primary key field type&#34;)] |
| key_name | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [Description(&#34;Primary key field name&#34;)] |
| timestamp | [string](#string) |  | @attr [PropertyOrderAttribute(5)] @attr [Description(&#34;Record data version/timestamp field name&#34;)] |
| is_db_from_connection_string | [bool](#bool) |  | if yes: Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name. if no: 1. Find DB type from 2. Create connection string from db_server, db_database_name, db_user |
| connection_string_name | [string](#string) |  |  |
| path_to_project_with_connection_string | [string](#string) |  | path to project with config file containing connection string. Usefull for UNIT tests. it will override previous settings @attr [PropertyOrderAttribute(4)] @attr [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))] @attr [Description(&#34;File path to store connection string settings in private place.&#34;)] |






<a name="proto_config.proto_app_db_settings"></a>

### proto_app_db_settings
@base : ViewModelValidatableWithSeverity&lt;AppDbSettings, AppDbSettings.AppDbSettingsValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| plugin_guid | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [Editor(typeof(EditorDbPluginSelection), typeof(EditorDbPluginSelection))] @attr [Description(&#34;Default DB Plugin&#34;)] |
| plugin_name | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [Editable(false)] |
| version | [string](#string) |  | @attr [PropertyOrderAttribute(3)] @attr [Editable(false)] |
| plugin_gen_guid | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [Editor(typeof(EditorDbPluginGenSelection), typeof(EditorDbPluginGenSelection))] @attr [Description(&#34;Default DB Plugin generator&#34;)] |
| plugin_gen_name | [string](#string) |  | @attr [PropertyOrderAttribute(5)] @attr [Editable(false)] |
| conn_guid | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(EditorDbConnSelection), typeof(EditorDbConnSelection))] @attr [Description(&#34;Default DB connection string&#34;)] |
| conn_name | [string](#string) |  | @attr [PropertyOrderAttribute(7)] @attr [Editable(false)] |






<a name="proto_config.proto_app_project"></a>

### proto_app_project



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| description | [string](#string) |  | string name_ui = 4; @attr [PropertyOrderAttribute(5)] |
| relative_app_project_path | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))] |
| default_db | [proto_app_db_settings](#proto_config.proto_app_db_settings) |  | @attr [PropertyOrderAttribute(8)] @attr [ExpandableObjectAttribute()] @attr [DisplayName(&#34;Default DB&#34;)] @attr [Description(&#34;Database connection. If empty, solution settings are used&#34;)] |






<a name="proto_config.proto_app_solution"></a>

### proto_app_solution



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| description | [string](#string) |  | string name_ui = 4; @attr [PropertyOrderAttribute(5)] |
| relative_app_solution_path | [string](#string) |  | List NET projects @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))] |
| default_db | [proto_app_db_settings](#proto_config.proto_app_db_settings) |  | @attr [PropertyOrderAttribute(8)] @attr [ExpandableObjectAttribute()] @attr [DisplayName(&#34;Default DB&#34;)] @attr [Description(&#34;Database connection. If empty, all solutions settings are used&#34;)] |
| list_app_projects | [proto_app_project](#proto_config.proto_app_project) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_base_config"></a>

### proto_base_config



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| description | [string](#string) |  | string name_ui = 4; @attr [PropertyOrderAttribute(5)] |
| config_node | [proto_config](#proto_config.proto_config) |  | @attr [BrowsableAttribute(false)] |
| relative_config_file_path | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(FilePickerEditor), typeof(ITypeEditor))] |






<a name="proto_config.proto_catalog"></a>

### proto_catalog



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [BrowsableAttribute(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_config"></a>

### proto_config
Configuration config


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | Unique Guid for configuration (for comparison) |
| version | [int32](#int32) |  | @attr [PropertyOrderAttribute(4)] @attr [Editable(false)] |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  |  |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(5)] |
| last_updated | [google.protobuf.Timestamp](#google.protobuf.Timestamp) |  | @attr [PropertyOrderAttribute(6)] |
| primary_key_type | [proto_enum_primary_key_type](#proto_config.proto_enum_primary_key_type) |  | @attr [PropertyOrderAttribute(7)] |
| db_settings | [db_settings](#proto_config.db_settings) |  | GENERAL DB SETTINGS @attr [PropertyOrderAttribute(11)] @attr [ExpandableObjectAttribute()] |
| group_app_solutions | [proto_group_list_app_solutions](#proto_config.proto_group_list_app_solutions) |  | @attr [BrowsableAttribute(false)] |
| group_plugins | [proto_group_list_plugins](#proto_config.proto_group_list_plugins) |  | @attr [BrowsableAttribute(false)] |
| group_configs | [proto_group_list_base_configs](#proto_config.proto_group_list_base_configs) |  | @attr [BrowsableAttribute(false)] |
| group_common | [proto_group_list_common](#proto_config.proto_group_list_common) |  | @attr [BrowsableAttribute(false)] |
| group_constants | [proto_group_list_constants](#proto_config.proto_group_list_constants) |  | @attr [BrowsableAttribute(false)] |
| group_enumerations | [proto_group_list_enumerations](#proto_config.proto_group_list_enumerations) |  | @attr [BrowsableAttribute(false)] |
| group_catalogs | [proto_group_list_catalogs](#proto_config.proto_group_list_catalogs) |  | @attr [BrowsableAttribute(false)] |
| group_documents | [proto_group_documents](#proto_config.proto_group_documents) |  | @attr [BrowsableAttribute(false)] |
| group_journals | [proto_group_list_journals](#proto_config.proto_group_list_journals) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_config_short_history"></a>

### proto_config_short_history



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| current_config | [proto_config](#proto_config.proto_config) |  |  |
| prev_stable_config | [proto_config](#proto_config.proto_config) |  |  |
| old_stable_config | [proto_config](#proto_config.proto_config) |  |  |






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
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [PropertyOrderAttribute(4)] @attr [ExpandableObjectAttribute()][DisplayName(&#34;Type&#34;)] |






<a name="proto_config.proto_data_type"></a>

### proto_data_type
@base : ViewModelValidatableWithSeverity&lt;DataType, DataType.DataTypeValidator&gt;


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data_type_enum | [proto_enum_data_type](#proto_config.proto_enum_data_type) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Type&#34;)] |
| length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(5)] |
| accuracy | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(7)] |
| is_positive | [bool](#bool) |  | @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Is positive&#34;)] |
| object_guid | [string](#string) |  | @attr [PropertyOrderAttribute(3)] @attr [Editor(typeof(DataTypeObjectNameEditor), typeof(DataTypeObjectNameEditor))] |
| is_nullable | [bool](#bool) |  | @attr [PropertyOrderAttribute(2)] |
| list_object_guids | [string](#string) | repeated | @attr [PropertyOrderAttribute(4)] |
| is_index_fk | [bool](#bool) |  | @attr [PropertyOrderAttribute(8)] @attr [DisplayName(&#34;FK Index&#34;)] @attr [Description(&#34;Create Index if this property is using foreign key (for Catalog or Document type)&#34;)] |






<a name="proto_config.proto_document"></a>

### proto_document



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [BrowsableAttribute(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_enumeration"></a>

### proto_enumeration



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type_enum | [enum_enumeration_type](#proto_config.enum_enumeration_type) |  | Enumeration element type @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Type&#34;)] |
| data_type_length | [int32](#int32) |  | Length of string if &#39;STRING&#39; is selected as enumeration element type @attr [PropertyOrderAttribute(5)] @attr [DisplayName(&#34;Length&#34;)] |
| list_enumeration_pairs | [proto_enumeration_pair](#proto_config.proto_enumeration_pair) | repeated | @attr [DisplayName(&#34;Elements&#34;)] @attr [NewItemTypes(typeof(EnumerationPair))] |






<a name="proto_config.proto_enumeration_pair"></a>

### proto_enumeration_pair



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| value | [string](#string) |  | TODO struct for different types, at least INTEGER |






<a name="proto_config.proto_form"></a>

### proto_form



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)]

repeated proto_group_properties list_properties = 6; repeated proto_document list_forms = 7; |






<a name="proto_config.proto_group_documents"></a>

### proto_group_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_shared_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_list_documents | [proto_group_list_documents](#proto_config.proto_group_list_documents) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_app_solutions"></a>

### proto_group_list_app_solutions



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Editable(false)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| description | [string](#string) |  | string name_ui = 4; @attr [PropertyOrderAttribute(2)] |
| default_db | [proto_app_db_settings](#proto_config.proto_app_db_settings) |  | @attr [PropertyOrderAttribute(3)] @attr [ExpandableObjectAttribute()] @attr [DisplayName(&#34;Default DB&#34;)] |
| list_app_solutions | [proto_app_solution](#proto_config.proto_app_solution) | repeated | List NET solutions @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_base_configs"></a>

### proto_group_list_base_configs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| description | [string](#string) |  | string name_ui = 4; |
| list_base_configs | [proto_base_config](#proto_config.proto_base_config) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_catalogs"></a>

### proto_group_list_catalogs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_catalogs | [proto_catalog](#proto_config.proto_catalog) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_common"></a>

### proto_group_list_common
Common parameters section


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_roles | [proto_group_list_roles](#proto_config.proto_group_list_roles) |  | @attr [BrowsableAttribute(false)] |
| group_view_forms | [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_constants"></a>

### proto_group_list_constants



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_constants | [proto_constant](#proto_config.proto_constant) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_documents"></a>

### proto_group_list_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_documents | [proto_document](#proto_config.proto_document) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_enumerations"></a>

### proto_group_list_enumerations



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_enumerations | [proto_enumeration](#proto_config.proto_enumeration) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_forms"></a>

### proto_group_list_forms



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_forms | [proto_form](#proto_config.proto_form) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_journals"></a>

### proto_group_list_journals



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_journals | [proto_journal](#proto_config.proto_journal) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_main_view_forms"></a>

### proto_group_list_main_view_forms
main view forms hierarchy node with children


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_main_view_forms | [proto_main_view_form](#proto_config.proto_main_view_form) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_plugins"></a>

### proto_group_list_plugins



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| list_plugins | [proto_plugin](#proto_config.proto_plugin) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_properties"></a>

### proto_group_list_properties



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_properties | [proto_property](#proto_config.proto_property) | repeated | @attr [BrowsableAttribute(false)] |
| last_gen_position | [uint32](#uint32) |  | Last generated Protobuf field position @attr [Editable(false)] |






<a name="proto_config.proto_group_list_properties_tabs"></a>

### proto_group_list_properties_tabs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_properties_tabs | [proto_properties_tab](#proto_config.proto_properties_tab) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_reports"></a>

### proto_group_list_reports



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_reports | [proto_report](#proto_config.proto_report) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_roles"></a>

### proto_group_list_roles



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_roles | [proto_role](#proto_config.proto_role) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_journal"></a>

### proto_journal



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_documents | [proto_document](#proto_config.proto_document) | repeated | repeated proto_group_properties list_properties = 6; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_main_view_form"></a>

### proto_main_view_form
main view forms hierarchy parent


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_list_view_forms | [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_plugin"></a>

### proto_plugin



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Editable(false)] |
| version | [string](#string) |  | @attr [Editable(false)] |
| name | [string](#string) |  | @attr [Editable(false)] |
| description | [string](#string) |  | @attr [Editable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| list_generators | [proto_plugin_generator](#proto_config.proto_plugin_generator) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_plugin_generator"></a>

### proto_plugin_generator



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Editable(false)] |
| name | [string](#string) |  | @attr [Editable(false)] |
| description | [string](#string) |  | @attr [Editable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| list_settings | [proto_plugin_generator_settings](#proto_config.proto_plugin_generator_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_plugin_generator_settings"></a>

### proto_plugin_generator_settings



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | This Guid is taken from Plugin Generator @attr [BrowsableAttribute(false)] |
| name | [string](#string) |  | This Name is taken from Plugin Generator @attr [PropertyOrderAttribute(1)] |
| description | [string](#string) |  | This Description is taken from Plugin Generator @attr [PropertyOrderAttribute(2)] @attr [Editable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| generator_settings | [string](#string) |  | @attr [BrowsableAttribute(false)] |
| is_private | [bool](#bool) |  | @attr [PropertyOrderAttribute(3)] @attr [Description(&#34;If false, connection string settings will be stored in configuration file. If true, will be stored in separate file.&#34;)] |
| file_path | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [Editor(typeof(FilePickerEditor), typeof(ITypeEditor))] @attr [Description(&#34;File path to store connection string settings in private place.&#34;)] |






<a name="proto_config.proto_properties_tab"></a>

### proto_properties_tab



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| is_index_fk | [bool](#bool) |  | Create Index for foreign key navigation property @attr [PropertyOrderAttribute(4)] |






<a name="proto_config.proto_property"></a>

### proto_property



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [PropertyOrderAttribute(4)] @attr [ExpandableObjectAttribute()] @attr [DisplayName(&#34;Type&#34;)] |
| position | [uint32](#uint32) |  | Protobuf field position Reserved positions: 1 - primary key @attr [Editable(false)] |






<a name="proto_config.proto_report"></a>

### proto_report



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)]

repeated proto_group_properties list_properties = 6; repeated proto_document list_documents = 7; |






<a name="proto_config.proto_role"></a>

### proto_role
User&#39;s role


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| name | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  |  |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |






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






<a name="proto_config.string_nullable"></a>

### string_nullable



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| has_value | [bool](#bool) |  |  |
| value | [string](#string) |  |  |





 


<a name="proto_config.db_id_generator_method"></a>

### db_id_generator_method


| Name | Number | Description |
| ---- | ------ | ----------- |
| Identity | 0 |  |
| HiLo | 1 |  |



<a name="proto_config.enum_enumeration_type"></a>

### enum_enumeration_type


| Name | Number | Description |
| ---- | ------ | ----------- |
| STRING_VALUE | 0 |  |
| BYTE_VALUE | 1 |  |
| SHORT_VALUE | 2 |  |
| INTEGER_VALUE | 3 |  |



<a name="proto_config.proto_enum_data_type"></a>

### proto_enum_data_type


| Name | Number | Description |
| ---- | ------ | ----------- |
| STRING | 0 |  |
| NUMERICAL | 1 |  |
| BOOL | 2 |  |
| TIME | 3 |  |
| DATE | 4 |  |
| DATETIME | 5 |  |
| ENUMERATION | 8 | CONSTANT = 7; |
| CATALOG | 9 |  |
| CATALOGS | 10 |  |
| DOCUMENT | 11 |  |
| DOCUMENTS | 12 |  |
| ANY | 15 |  |



<a name="proto_config.proto_enum_primary_key_type"></a>

### proto_enum_primary_key_type


| Name | Number | Description |
| ---- | ------ | ----------- |
| INT | 0 |  |
| LONG | 1 |  |


 

 

 



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

