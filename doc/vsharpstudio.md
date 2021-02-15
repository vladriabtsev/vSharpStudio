# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [vsharpstudio.proto](#vsharpstudio.proto)
    - [db_settings](#proto_config.db_settings)
    - [proto_app_db_settings](#proto_config.proto_app_db_settings)
    - [proto_app_project](#proto_config.proto_app_project)
    - [proto_app_project_generator](#proto_config.proto_app_project_generator)
    - [proto_app_solution](#proto_config.proto_app_solution)
    - [proto_base_config_link](#proto_config.proto_base_config_link)
    - [proto_catalog](#proto_config.proto_catalog)
    - [proto_catalog_items_group](#proto_config.proto_catalog_items_group)
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
    - [proto_group_list_base_config_links](#proto_config.proto_group_list_base_config_links)
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
    - [proto_model](#proto_config.proto_model)
    - [proto_model_row](#proto_config.proto_model_row)
    - [proto_plugin](#proto_config.proto_plugin)
    - [proto_plugin_generator](#proto_config.proto_plugin_generator)
    - [proto_plugin_generator_node_default_settings](#proto_config.proto_plugin_generator_node_default_settings)
    - [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings)
    - [proto_plugin_generator_settings](#proto_config.proto_plugin_generator_settings)
    - [proto_plugin_group_generators_default_settings](#proto_config.proto_plugin_group_generators_default_settings)
    - [proto_plugin_group_generators_settings](#proto_config.proto_plugin_group_generators_settings)
    - [proto_properties_tab](#proto_config.proto_properties_tab)
    - [proto_property](#proto_config.proto_property)
    - [proto_report](#proto_config.proto_report)
    - [proto_role](#proto_config.proto_role)
    - [proto_settings_config](#proto_config.proto_settings_config)
    - [proto_user_settings](#proto_config.proto_user_settings)
    - [proto_user_settings_opened_config](#proto_config.proto_user_settings_opened_config)
  
    - [db_id_generator_method](#proto_config.db_id_generator_method)
    - [enum_enumeration_type](#proto_config.enum_enumeration_type)
    - [proto_enum_catalog_tree_icon](#proto_config.proto_enum_catalog_tree_icon)
    - [proto_enum_data_type](#proto_config.proto_enum_data_type)
    - [proto_enum_primary_key_type](#proto_config.proto_enum_primary_key_type)
  
  
  

- [Scalar Value Types](#scalar-value-types)



<a name="vsharpstudio.proto"></a>
<p align="right"><a href="#top">Top</a></p>

## vsharpstudio.proto



<a name="proto_config.db_settings"></a>

### db_settings
General DB settings
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| db_schema | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Schema&#34;)] @attr [Description(&#34;DB schema name for all object in this configuration&#34;)] |
| id_generator | [db_id_generator_method](#proto_config.db_id_generator_method) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;Id method&#34;)] @attr [Description(&#34;Primary key generation method&#34;)] |
| p_key_type | [proto_enum_primary_key_type](#proto_config.proto_enum_primary_key_type) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Id type&#34;)] @attr [Description(&#34;Primary key field type&#34;)] |
| p_key_name | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Id name&#34;)] @attr [Description(&#34;Primary key field name&#34;)] |
| p_key_field_guid | [string](#string) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_app_db_settings"></a>

### proto_app_db_settings
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| plugin_guid | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [Editor(typeof(EditorDbPluginSelection), typeof(EditorDbPluginSelection))] @attr [Description(&#34;Default DB Plugin&#34;)] |
| plugin_name | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [ReadOnly(true)] |
| version | [string](#string) |  | @attr [PropertyOrderAttribute(3)] @attr [ReadOnly(true)] |
| plugin_gen_guid | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [Editor(typeof(EditorDbPluginGenSelection), typeof(EditorDbPluginGenSelection))] @attr [Description(&#34;Default DB Plugin generator&#34;)] |
| plugin_gen_name | [string](#string) |  | @attr [PropertyOrderAttribute(5)] @attr [ReadOnly(true)] |
| conn_guid | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(EditorDbConnSelection), typeof(EditorDbConnSelection))] @attr [Description(&#34;Default DB connection string&#34;)] |
| conn_name | [string](#string) |  | @attr [PropertyOrderAttribute(7)] @attr [ReadOnly(true)] |






<a name="proto_config.proto_app_project"></a>

### proto_app_project
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(5)] |
| relative_app_project_path | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(EditorProjectPicker), typeof(ITypeEditor))] @attr [Description(&#34;.NET project file path relative to solution file path&#34;)] App project relative path to .net solution file path |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| namespace | [string](#string) |  | @attr [PropertyOrderAttribute(9)] @attr [DisplayName(&#34;Namespace&#34;)] @attr [Description(&#34;Project namespace name&#34;)] |
| list_app_project_generators | [proto_app_project_generator](#proto_config.proto_app_project_generator) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_app_project_generator"></a>

### proto_app_project_generator
Application project generator
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Description(&#34;Connection string name for DB connection generator&#34;)] |
| name_ui | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| plugin_guid | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Plugin&#34;)] @attr [Description(&#34;Plugins with generators&#34;)] @attr [Editor(typeof(EditorPluginSelection), typeof(ITypeEditor))] |
| description_plugin | [string](#string) |  | @attr [PropertyOrderAttribute(5)] @attr [DisplayName(&#34;Description&#34;)] @attr [ReadOnly(true)] |
| plugin_generator_guid | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Generator&#34;)] @attr [Description(&#34;Plugin generator&#34;)] @attr [Editor(typeof(EditorPluginGeneratorSelection), typeof(ITypeEditor))] |
| description_generator | [string](#string) |  | @attr [PropertyOrderAttribute(7)] @attr [DisplayName(&#34;Description&#34;)] @attr [ReadOnly(true)] |
| relative_path_to_gen_folder | [string](#string) |  | @attr [PropertyOrderAttribute(8)] @attr [DisplayName(&#34;Output Folder&#34;)] @attr [Editor(typeof(EditorFolderPicker), typeof(ITypeEditor))] @attr [Description(&#34;Get is returning relative folder path to project file&#34;)] Relative folder path to project file |
| gen_file_name | [string](#string) |  | @attr [DisplayName(&#34;Output File&#34;)] @attr [PropertyOrderAttribute(9)] @attr [Description(&#34;Generator output file name&#34;)] Generator output file name |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| generator_settings | [string](#string) |  | @attr [BrowsableAttribute(false)] |
| generator_settings_vm | [proto_plugin_generator_settings](#proto_config.proto_plugin_generator_settings) |  | @attr [PropertyOrderAttribute(29)] @attr [BrowsableAttribute(false)] |
| conn_str | [string](#string) |  | @attr [PropertyOrderAttribute(9)] @attr [Description(&#34;Db connection string. Directly editable or generated based on settings&#34;)] |
| plugin_group_settings_guid | [string](#string) |  | @attr [BrowsableAttribute(false)] |
| conn_str_to_prev_stable | [string](#string) |  | @attr [PropertyOrderAttribute(13)] @attr [DisplayName(&#34;Stable DB&#34;)] @attr [Description(&#34;Db connection string to previous stable version&#34;)] |
| is_generate_sql_sqript_to_update_prev_stable | [bool](#bool) |  | @attr [PropertyOrderAttribute(14)] @attr [DisplayName(&#34;Migrate DB&#34;)] @attr [Description(&#34;Generate Sql script to update stable DB version to current state&#34;)] |
| gen_script_file_name | [string](#string) |  | @attr [DisplayName(&#34;SQL file&#34;)] @attr [PropertyOrderAttribute(15)] @attr [Description(&#34;SQL script output file name&#34;)] Generator output file name |






<a name="proto_config.proto_app_solution"></a>

### proto_app_solution
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(5)] |
| relative_app_solution_path | [string](#string) |  | List NET projects @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Path&#34;)] @attr [Editor(typeof(EditorSolutionPicker), typeof(ITypeEditor))] @attr [Description(&#34;.NET solution file path relative to configuration file path&#34;)] App solution relative path to configuration file path |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_app_projects | [proto_app_project](#proto_config.proto_app_project) | repeated | @attr [BrowsableAttribute(false)] |
| list_group_generators_settings | [proto_plugin_group_generators_settings](#proto_config.proto_plugin_group_generators_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_base_config_link"></a>

### proto_base_config_link
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(5)] |
| config_base | [proto_config](#proto_config.proto_config) |  | @attr [BrowsableAttribute(false)] |
| relative_config_file_path | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(EditorFilePicker), typeof(ITypeEditor))] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] @attr [DisplayName(&#34;For deletion&#34;)] @attr [Description(&#34;Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_catalog"></a>

### proto_catalog



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| group_items | [proto_catalog_items_group](#proto_config.proto_catalog_items_group) |  | @attr [BrowsableAttribute(false)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [BrowsableAttribute(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [BrowsableAttribute(false)] |
| item_icon_type | [proto_enum_catalog_tree_icon](#proto_config.proto_enum_catalog_tree_icon) |  | @attr [PropertyOrderAttribute(19)] @attr [DisplayName(&#34;Item Icon&#34;)] @attr [Description(&#34;Catalog item icon type&#34;)] |
| use_name_property | [bool](#bool) |  | @attr [PropertyOrderAttribute(21)] @attr [DisplayName(&#34;Use Name&#34;)] @attr [Description(&#34;Use Name property for catalog item&#34;)] |
| max_name_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Max Length&#34;)] @attr [Description(&#34;Maximum catalog item name length. If zero, than unlimited length&#34;)] |
| property_name_guid | [string](#string) |  | @attr [BrowsableAttribute(false)] |
| use_description_property | [bool](#bool) |  | @attr [PropertyOrderAttribute(31)] @attr [DisplayName(&#34;Use Description&#34;)] @attr [Description(&#34;Use Description property for catalog item&#34;)] |
| max_description_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(32)] @attr [DisplayName(&#34;Max Length&#34;)] @attr [Description(&#34;Maximum catalog item description length. If zero, than unlimited length&#34;)] |
| property_description_guid | [string](#string) |  | @attr [BrowsableAttribute(false)] |
| use_tree | [bool](#bool) |  | @attr [PropertyOrderAttribute(41)] @attr [DisplayName(&#34;Use Tree&#34;)] @attr [Description(&#34;Use tree catalog structure&#34;)] |
| group_icon_type | [proto_enum_catalog_tree_icon](#proto_config.proto_enum_catalog_tree_icon) |  | @attr [PropertyOrderAttribute(42)] @attr [DisplayName(&#34;Group Icon&#34;)] @attr [Description(&#34;Catalog group icon type&#34;)] |
| max_tree_levels | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(43)] @attr [DisplayName(&#34;Levels&#34;)] @attr [Description(&#34;Maximum amount levels in catalog item groups. If zero, than unlimited&#34;)] |
| separate_properties_for_groups | [bool](#bool) |  | @attr [PropertyOrderAttribute(44)] @attr [DisplayName(&#34;Group properties&#34;)] @attr [Description(&#34;Separate set of properties for groups&#34;)] |
| property_parent_guid | [string](#string) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_catalog_items_group"></a>

### proto_catalog_items_group



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] @attr [DisplayName(&#34;For deletion&#34;)] @attr [Description(&#34;Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version&#34;)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_config"></a>

### proto_config
Configuration config


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| version | [int32](#int32) |  | @attr [PropertyOrderAttribute(4)] @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  |  |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(5)] |
| last_updated | [google.protobuf.Timestamp](#google.protobuf.Timestamp) |  | @attr [PropertyOrderAttribute(6)] |
| is_need_current_update | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| group_config_links | [proto_group_list_base_config_links](#proto_config.proto_group_list_base_config_links) |  | @attr [BrowsableAttribute(false)] |
| model | [proto_model](#proto_config.proto_model) |  | @attr [BrowsableAttribute(false)] |
| group_plugins | [proto_group_list_plugins](#proto_config.proto_group_list_plugins) |  | @attr [BrowsableAttribute(false)] |
| group_app_solutions | [proto_group_list_app_solutions](#proto_config.proto_group_list_app_solutions) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_config_short_history"></a>

### proto_config_short_history
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| current_config | [proto_config](#proto_config.proto_config) |  |  |
| prev_stable_config | [proto_config](#proto_config.proto_config) |  |  |






<a name="proto_config.proto_constant"></a>

### proto_constant
Constant application wise value


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [PropertyOrderAttribute(4)] @attr [ExpandableObjectAttribute()][DisplayName(&#34;Type&#34;)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_data_type"></a>

### proto_data_type
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data_type_enum | [proto_enum_data_type](#proto_config.proto_enum_data_type) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Type&#34;)] |
| length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;Length&#34;)] @attr [Description(&#34;Maximum length of data (characters in string, or decimal digits for numeric data)&#34;)] |
| accuracy | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Accuracy&#34;)] @attr [Description(&#34;Number of decimal places for numeric data)&#34;)] |
| object_guid | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))] |
| list_object_guids | [string](#string) | repeated | @attr [PropertyOrderAttribute(5)] |
| is_index_fk | [bool](#bool) |  | @attr [PropertyOrderAttribute(9)] @attr [DisplayName(&#34;FK Index&#34;)] @attr [Description(&#34;Create Index if this property is using foreign key (for Catalog or Document type)&#34;)] |
| is_positive | [bool](#bool) |  | @attr [PropertyOrderAttribute(11)] @attr [DisplayName(&#34;Positive&#34;)] @attr [Description(&#34;Expected always &gt;= 0&#34;)] |
| is_nullable | [bool](#bool) |  | @attr [PropertyOrderAttribute(12)] @attr [DisplayName(&#34;Can be NULL&#34;)] @attr [Description(&#34;If unchecked always expected data&#34;)] |
| is_p_key | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_ref_parent | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_document"></a>

### proto_document



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [BrowsableAttribute(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_enumeration"></a>

### proto_enumeration



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type_enum | [enum_enumeration_type](#proto_config.enum_enumeration_type) |  | Enumeration element type @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Type&#34;)] |
| data_type_length | [int32](#int32) |  | Length of string if &#39;STRING&#39; is selected as enumeration element type @attr [PropertyOrderAttribute(5)] @attr [DisplayName(&#34;Length&#34;)] |
| list_enumeration_pairs | [proto_enumeration_pair](#proto_config.proto_enumeration_pair) | repeated | @attr [DisplayName(&#34;Elements&#34;)] @attr [NewItemTypes(typeof(EnumerationPair))] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_enumeration_pair"></a>

### proto_enumeration_pair



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Name&#34;)] @attr [Description(&#34;Enumeration element name&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(5)] @attr [Description(&#34;Description of enumeration element&#34;)] |
| value | [string](#string) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Value&#34;)] @attr [Description(&#34;Enumeration element value&#34;)] |
| is_default | [bool](#bool) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Is default&#34;)] @attr [Description(&#34;Used as default value for enumeration&#34;)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_form"></a>

### proto_form



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | repeated proto_group_properties list_properties = 6; repeated proto_document list_forms = 7; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_documents"></a>

### proto_group_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| prefix_for_db_tables | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Db prefix&#34;)] @attr [Description(&#34;Prefix for document db table names. Used if set to use in config model&#34;)] |
| group_shared_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] @attr [Description(&#34;Properties for all documents&#34;)] |
| group_list_documents | [proto_group_list_documents](#proto_config.proto_group_list_documents) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_app_solutions"></a>

### proto_group_list_app_solutions
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(2)] |
| list_app_solutions | [proto_app_solution](#proto_config.proto_app_solution) | repeated | List NET solutions @attr [BrowsableAttribute(false)] |
| list_group_generators_defult_settings | [proto_plugin_group_generators_default_settings](#proto_config.proto_plugin_group_generators_default_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_base_config_links"></a>

### proto_group_list_base_config_links
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| description | [string](#string) |  |  |
| list_base_config_links | [proto_base_config_link](#proto_config.proto_base_config_link) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_catalogs"></a>

### proto_group_list_catalogs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| prefix_for_db_tables | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Db prefix&#34;)] @attr [Description(&#34;Prefix for catalog db table names. Used if set to use in config model&#34;)] |
| list_catalogs | [proto_catalog](#proto_config.proto_catalog) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_common"></a>

### proto_group_list_common
Common parameters section


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_roles | [proto_group_list_roles](#proto_config.proto_group_list_roles) |  | @attr [BrowsableAttribute(false)] |
| group_view_forms | [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_constants"></a>

### proto_group_list_constants



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_constants | [proto_constant](#proto_config.proto_constant) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_documents"></a>

### proto_group_list_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_documents | [proto_document](#proto_config.proto_document) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_enumerations"></a>

### proto_group_list_enumerations



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_enumerations | [proto_enumeration](#proto_config.proto_enumeration) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_forms"></a>

### proto_group_list_forms



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_forms | [proto_form](#proto_config.proto_form) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_journals"></a>

### proto_group_list_journals



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_journals | [proto_journal](#proto_config.proto_journal) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_main_view_forms"></a>

### proto_group_list_main_view_forms
main view forms hierarchy node with children


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_main_view_forms | [proto_main_view_form](#proto_config.proto_main_view_form) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_plugins"></a>

### proto_group_list_plugins
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| list_plugins | [proto_plugin](#proto_config.proto_plugin) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_properties"></a>

### proto_group_list_properties



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_properties | [proto_property](#proto_config.proto_property) | repeated | @attr [BrowsableAttribute(false)] |
| last_gen_position | [uint32](#uint32) |  | Last generated Protobuf field position @attr [ReadOnly(true)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_properties_tabs"></a>

### proto_group_list_properties_tabs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_properties_tabs | [proto_properties_tab](#proto_config.proto_properties_tab) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_reports"></a>

### proto_group_list_reports



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_reports | [proto_report](#proto_config.proto_report) | repeated | repeated proto_property list_shared_properties = 6; @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_group_list_roles"></a>

### proto_group_list_roles



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_roles | [proto_role](#proto_config.proto_role) | repeated | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_journal"></a>

### proto_journal



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| list_documents | [proto_document](#proto_config.proto_document) | repeated | repeated proto_group_properties list_properties = 6; @attr [BrowsableAttribute(false)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_main_view_form"></a>

### proto_main_view_form
main view forms hierarchy parent


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_list_view_forms | [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms) |  | @attr [BrowsableAttribute(false)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_model"></a>

### proto_model
Configuration model
@attr [CategoryOrder(&#34;Db Names Generation&#34;, 5)]


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| version | [int32](#int32) |  | @attr [PropertyOrderAttribute(2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| composite_name_max_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(8)] @attr [Category(&#34;Composite Names Generation&#34;)] @attr [DisplayName(&#34;Max length&#34;)] |
| is_use_composite_names | [bool](#bool) |  | @attr [PropertyOrderAttribute(9)] @attr [Description(&#34;Use parent-child composite names.&#34;)] @attr [Category(&#34;Composite Names Generation&#34;)] @attr [DisplayName(&#34;Use Composite&#34;)] |
| is_use_group_prefix | [bool](#bool) |  | @attr [PropertyOrderAttribute(10)] @attr [Description(&#34;Composite names use their parent name as prefix. In a case of simple names all object&#39;s name will have only group name as a prefix.&#34;)] @attr [Category(&#34;Composite Names Generation&#34;)] @attr [DisplayName(&#34;Use Prefix&#34;)] |
| db_settings | [db_settings](#proto_config.db_settings) |  | GENERAL DB SETTINGS @attr [PropertyOrderAttribute(11)] @attr [ExpandableObjectAttribute()] @attr [Description(&#34;General DB generator settings&#34;)] @attr [DisplayName(&#34;DB settings&#34;)] |
| group_common | [proto_group_list_common](#proto_config.proto_group_list_common) |  | @attr [BrowsableAttribute(false)] |
| group_constants | [proto_group_list_constants](#proto_config.proto_group_list_constants) |  | @attr [BrowsableAttribute(false)] |
| group_enumerations | [proto_group_list_enumerations](#proto_config.proto_group_list_enumerations) |  | @attr [BrowsableAttribute(false)] |
| group_catalogs | [proto_group_list_catalogs](#proto_config.proto_group_list_catalogs) |  | @attr [BrowsableAttribute(false)] |
| group_documents | [proto_group_documents](#proto_config.proto_group_documents) |  | @attr [BrowsableAttribute(false)] |
| group_journals | [proto_group_list_journals](#proto_config.proto_group_list_journals) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_model_row"></a>

### proto_model_row
@base VmBindable


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| group_name | [string](#string) |  |  |
| name | [string](#string) |  |  |
| guid | [string](#string) |  |  |
| is_included | [bool](#bool) |  |  |






<a name="proto_config.proto_plugin"></a>

### proto_plugin
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| version | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| description | [string](#string) |  | @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| list_generators | [proto_plugin_generator](#proto_config.proto_plugin_generator) | repeated | @attr [BrowsableAttribute(false)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_plugin_generator"></a>

### proto_plugin_generator
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| description | [string](#string) |  | @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_plugin_generator_node_default_settings"></a>

### proto_plugin_generator_node_default_settings
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| node_settings_vm_guid | [string](#string) |  | Guid of solution-project-generator node |
| settings | [string](#string) |  |  |






<a name="proto_config.proto_plugin_generator_node_settings"></a>

### proto_plugin_generator_node_settings
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| app_project_generator_guid | [string](#string) |  | Guid of solution-project-generator node |
| name | [string](#string) |  | Name of solution-project-generator node |
| name_ui | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| settings | [string](#string) |  | string node_settings_vm_guid = 6; |






<a name="proto_config.proto_plugin_generator_settings"></a>

### proto_plugin_generator_settings
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] |
| app_project_generator_guid | [string](#string) |  | Guid of solution-project-generator node |
| settings | [string](#string) |  |  |






<a name="proto_config.proto_plugin_group_generators_default_settings"></a>

### proto_plugin_group_generators_default_settings
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| app_group_generators_guid | [string](#string) |  | Guid of group generators |
| settings | [string](#string) |  |  |






<a name="proto_config.proto_plugin_group_generators_settings"></a>

### proto_plugin_group_generators_settings
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [ReadOnly(true)] |
| app_group_generators_guid | [string](#string) |  |  |
| settings | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_properties_tab"></a>

### proto_properties_tab



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [BrowsableAttribute(false)] |
| group_properties_tabs | [proto_group_list_properties_tabs](#proto_config.proto_group_list_properties_tabs) |  | @attr [BrowsableAttribute(false)] |
| is_index_fk | [bool](#bool) |  | Create Index for foreign key navigation property @attr [PropertyOrderAttribute(4)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_property"></a>

### proto_property



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [PropertyOrderAttribute(4)] @attr [ExpandableObjectAttribute()] @attr [DisplayName(&#34;Type&#34;)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| position | [uint32](#uint32) |  | Protobuf field position Reserved positions: 1 - primary key @attr [ReadOnly(true)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_report"></a>

### proto_report



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | repeated proto_group_properties list_properties = 6; repeated proto_document list_documents = 7; @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_role"></a>

### proto_role
User&#39;s role


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [BrowsableAttribute(false)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [BrowsableAttribute(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_settings_config"></a>

### proto_settings_config
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [PropertyOrderAttribute(3)] |
| version_migration_current | [int32](#int32) |  | current migration version, increased by one on each deployment |
| version_migration_support_from_min | [int32](#int32) |  | min version supported by current version for migration |






<a name="proto_config.proto_user_settings"></a>

### proto_user_settings
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| list_open_config_history | [proto_user_settings_opened_config](#proto_config.proto_user_settings_opened_config) | repeated | @attr [BrowsableAttribute(false)] |






<a name="proto_config.proto_user_settings_opened_config"></a>

### proto_user_settings_opened_config
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| opened_last_time_on | [google.protobuf.Timestamp](#google.protobuf.Timestamp) |  | @attr [BrowsableAttribute(false)] |
| config_path | [string](#string) |  | @attr [BrowsableAttribute(false)] |





 


<a name="proto_config.db_id_generator_method"></a>

### db_id_generator_method


| Name | Number | Description |
| ---- | ------ | ----------- |
| Identity | 0 |  |
| HiLo | 1 |  |



<a name="proto_config.enum_enumeration_type"></a>

### enum_enumeration_type
Enumeration member value for numerical type is representing accuracy. Used to estimate potential data loss

| Name | Number | Description |
| ---- | ------ | ----------- |
| INTEGER_VALUE | 0 |  |
| SHORT_VALUE | 1 |  |
| BYTE_VALUE | 2 |  |
| STRING_VALUE | 3 |  |



<a name="proto_config.proto_enum_catalog_tree_icon"></a>

### proto_enum_catalog_tree_icon


| Name | Number | Description |
| ---- | ------ | ----------- |
| None | 0 |  |
| Item | 1 |  |
| Folder | 2 |  |
| Custom | 3 |  |



<a name="proto_config.proto_enum_data_type"></a>

### proto_enum_data_type


| Name | Number | Description |
| ---- | ------ | ----------- |
| STRING | 0 | @attr [DisplayName(&#34;String&#34;)] @attr [Description(&#34;String type. If length is zero, unlimited string length&#34;)] |
| NUMERICAL | 1 | @attr [DisplayName(&#34;Numeric&#34;)] @attr [Description(&#34;Numerical data type. Type depend on length and accuracy&#34;)] |
| BOOL | 2 | @attr [DisplayName(&#34;Boolean&#34;)] @attr [Description(&#34;Boolean type&#34;)] |
| TIME | 3 | @attr [DisplayName(&#34;Time&#34;)] @attr [Description(&#34;Time without time zone&#34;)] |
| TIMEZ | 4 | @attr [DisplayName(&#34;Time Z&#34;)] @attr [Description(&#34;Time with time zone&#34;)] |
| DATE | 5 | @attr [DisplayName(&#34;Date&#34;)] @attr [Description(&#34;Date without time zone&#34;)] |
| DATETIME | 6 | @attr [DisplayName(&#34;DateTime&#34;)] @attr [Description(&#34;Date and time without time zone&#34;)] |
| DATETIMEZ | 7 | @attr [DisplayName(&#34;DateTime Z&#34;)] @attr [Description(&#34;Date and time with time zone&#34;)] |
| ENUMERATION | 10 | @attr [DisplayName(&#34;Enumeration&#34;)] @attr [Description(&#34;Enumeration type&#34;)] |
| CATALOG | 11 | @attr [DisplayName(&#34;Catalog&#34;)] @attr [Description(&#34;Catalog type&#34;)] |
| CATALOGS | 12 | @attr [DisplayName(&#34;Catalogs&#34;)] @attr [Description(&#34;List of catalogs&#34;)] |
| DOCUMENT | 13 | @attr [DisplayName(&#34;Document&#34;)] @attr [Description(&#34;Document type&#34;)] |
| DOCUMENTS | 14 | @attr [DisplayName(&#34;Documents&#34;)] @attr [Description(&#34;List of documents&#34;)] |
| ANY | 15 | @attr [DisplayName(&#34;Any type&#34;)] @attr [Description(&#34;Any data type&#34;)] |



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

