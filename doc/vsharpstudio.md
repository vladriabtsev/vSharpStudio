# Protocol Documentation
<a name="top"></a>

## Table of Contents

- [vsharpstudio.proto](#vsharpstudio.proto)
    - [proto_app_db_settings](#proto_config.proto_app_db_settings)
    - [proto_app_project](#proto_config.proto_app_project)
    - [proto_app_project_generator](#proto_config.proto_app_project_generator)
    - [proto_app_solution](#proto_config.proto_app_solution)
    - [proto_base_config_link](#proto_config.proto_base_config_link)
    - [proto_catalog](#proto_config.proto_catalog)
    - [proto_catalog_code_property_settings](#proto_config.proto_catalog_code_property_settings)
    - [proto_catalog_folder](#proto_config.proto_catalog_folder)
    - [proto_code_sequence](#proto_config.proto_code_sequence)
    - [proto_config](#proto_config.proto_config)
    - [proto_config_short_history](#proto_config.proto_config_short_history)
    - [proto_constant](#proto_config.proto_constant)
    - [proto_data_type](#proto_config.proto_data_type)
    - [proto_detail](#proto_config.proto_detail)
    - [proto_doc_in_journal](#proto_config.proto_doc_in_journal)
    - [proto_document](#proto_config.proto_document)
    - [proto_document_number_property_settings](#proto_config.proto_document_number_property_settings)
    - [proto_enumeration](#proto_config.proto_enumeration)
    - [proto_enumeration_pair](#proto_config.proto_enumeration_pair)
    - [proto_form](#proto_config.proto_form)
    - [proto_form_auto_layout_block](#proto_config.proto_form_auto_layout_block)
    - [proto_form_auto_layout_sub_block](#proto_config.proto_form_auto_layout_sub_block)
    - [proto_form_data_grid](#proto_config.proto_form_data_grid)
    - [proto_form_field](#proto_config.proto_form_field)
    - [proto_form_grid_system](#proto_config.proto_form_grid_system)
    - [proto_form_grid_system_column](#proto_config.proto_form_grid_system_column)
    - [proto_form_grid_system_row](#proto_config.proto_form_grid_system_row)
    - [proto_form_tab_control](#proto_config.proto_form_tab_control)
    - [proto_form_tab_control_tab](#proto_config.proto_form_tab_control_tab)
    - [proto_form_tree](#proto_config.proto_form_tree)
    - [proto_group_constant_groups](#proto_config.proto_group_constant_groups)
    - [proto_group_documents](#proto_config.proto_group_documents)
    - [proto_group_list_app_solutions](#proto_config.proto_group_list_app_solutions)
    - [proto_group_list_base_config_links](#proto_config.proto_group_list_base_config_links)
    - [proto_group_list_catalogs](#proto_config.proto_group_list_catalogs)
    - [proto_group_list_common](#proto_config.proto_group_list_common)
    - [proto_group_list_constants](#proto_config.proto_group_list_constants)
    - [proto_group_list_details](#proto_config.proto_group_list_details)
    - [proto_group_list_documents](#proto_config.proto_group_list_documents)
    - [proto_group_list_enumerations](#proto_config.proto_group_list_enumerations)
    - [proto_group_list_forms](#proto_config.proto_group_list_forms)
    - [proto_group_list_journals](#proto_config.proto_group_list_journals)
    - [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms)
    - [proto_group_list_plugins](#proto_config.proto_group_list_plugins)
    - [proto_group_list_properties](#proto_config.proto_group_list_properties)
    - [proto_group_list_reports](#proto_config.proto_group_list_reports)
    - [proto_group_list_roles](#proto_config.proto_group_list_roles)
    - [proto_group_list_sequences](#proto_config.proto_group_list_sequences)
    - [proto_journal](#proto_config.proto_journal)
    - [proto_main_view_form](#proto_config.proto_main_view_form)
    - [proto_model](#proto_config.proto_model)
    - [proto_model_row](#proto_config.proto_model_row)
    - [proto_plugin](#proto_config.proto_plugin)
    - [proto_plugin_generator](#proto_config.proto_plugin_generator)
    - [proto_plugin_generator_node_default_settings](#proto_config.proto_plugin_generator_node_default_settings)
    - [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings)
    - [proto_plugin_generator_project_settings](#proto_config.proto_plugin_generator_project_settings)
    - [proto_plugin_generator_settings](#proto_config.proto_plugin_generator_settings)
    - [proto_plugin_generator_solution_settings](#proto_config.proto_plugin_generator_solution_settings)
    - [proto_property](#proto_config.proto_property)
    - [proto_property_data_generator](#proto_config.proto_property_data_generator)
    - [proto_report](#proto_config.proto_report)
    - [proto_role](#proto_config.proto_role)
    - [proto_role_catalog_access](#proto_config.proto_role_catalog_access)
    - [proto_role_constant_access](#proto_config.proto_role_constant_access)
    - [proto_role_detail_access](#proto_config.proto_role_detail_access)
    - [proto_role_document_access](#proto_config.proto_role_document_access)
    - [proto_role_property_access](#proto_config.proto_role_property_access)
    - [proto_settings_config](#proto_config.proto_settings_config)
    - [proto_user_settings](#proto_config.proto_user_settings)
    - [proto_user_settings_opened_config](#proto_config.proto_user_settings_opened_config)
  
    - [enum_enumeration_type](#proto_config.enum_enumeration_type)
    - [proto_enum_address_data_type](#proto_config.proto_enum_address_data_type)
    - [proto_enum_catalog_code_unique_scope](#proto_config.proto_enum_catalog_code_unique_scope)
    - [proto_enum_catalog_detail_access](#proto_config.proto_enum_catalog_detail_access)
    - [proto_enum_catalog_tree_icon](#proto_config.proto_enum_catalog_tree_icon)
    - [proto_enum_code_type](#proto_config.proto_enum_code_type)
    - [proto_enum_commerce_data_type](#proto_config.proto_enum_commerce_data_type)
    - [proto_enum_company_data_type](#proto_config.proto_enum_company_data_type)
    - [proto_enum_constant_access](#proto_config.proto_enum_constant_access)
    - [proto_enum_data_type](#proto_config.proto_enum_data_type)
    - [proto_enum_database_data_type](#proto_config.proto_enum_database_data_type)
    - [proto_enum_date_data_type](#proto_config.proto_enum_date_data_type)
    - [proto_enum_doc_number_unique_scope](#proto_config.proto_enum_doc_number_unique_scope)
    - [proto_enum_document_access](#proto_config.proto_enum_document_access)
    - [proto_enum_finance_data_type](#proto_config.proto_enum_finance_data_type)
    - [proto_enum_hacker_data_type](#proto_config.proto_enum_hacker_data_type)
    - [proto_enum_hidden_type](#proto_config.proto_enum_hidden_type)
    - [proto_enum_image_data_type](#proto_config.proto_enum_image_data_type)
    - [proto_enum_internet_data_type](#proto_config.proto_enum_internet_data_type)
    - [proto_enum_lorem_data_type](#proto_config.proto_enum_lorem_data_type)
    - [proto_enum_months](#proto_config.proto_enum_months)
    - [proto_enum_name_data_type](#proto_config.proto_enum_name_data_type)
    - [proto_enum_phone_data_type](#proto_config.proto_enum_phone_data_type)
    - [proto_enum_primary_key_type](#proto_config.proto_enum_primary_key_type)
    - [proto_enum_print_access](#proto_config.proto_enum_print_access)
    - [proto_enum_property_access](#proto_config.proto_enum_property_access)
    - [proto_enum_property_data_type](#proto_config.proto_enum_property_data_type)
    - [proto_enum_random_data_type](#proto_config.proto_enum_random_data_type)
    - [proto_enum_rant_data_type](#proto_config.proto_enum_rant_data_type)
    - [proto_enum_system_data_type](#proto_config.proto_enum_system_data_type)
    - [proto_enum_time_accuracy_type](#proto_config.proto_enum_time_accuracy_type)
    - [proto_enum_use_type](#proto_config.proto_enum_use_type)
    - [proto_enum_vehicle_data_type](#proto_config.proto_enum_vehicle_data_type)
    - [proto_enum_version_field_type](#proto_config.proto_enum_version_field_type)
    - [proto_form_orientation](#proto_config.proto_form_orientation)
    - [proto_form_type](#proto_config.proto_form_type)
  
  
  

- [Scalar Value Types](#scalar-value-types)



<a name="vsharpstudio.proto"></a>
<p align="right"><a href="#top">Top</a></p>

## vsharpstudio.proto



<a name="proto_config.proto_app_db_settings"></a>

### proto_app_db_settings
@base VmValidatable


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
@interface ICanAddNode
@interface ISortingValue
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| relative_app_project_path | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Path&#34;)] @attr [Editor(typeof(EditorProjectPicker), typeof(ITypeEditor))] @attr [Description(&#34;.NET project file path relative to solution file path&#34;)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_app_project_generators | [proto_app_project_generator](#proto_config.proto_app_project_generator) | repeated | @attr [Browsable(false)] |
| list_generators_project_settings | [proto_plugin_generator_project_settings](#proto_config.proto_plugin_generator_project_settings) | repeated | @attr [Browsable(false)]

repeated proto_plugin_group_generators_settings list_group_generators_settings = 18; |






<a name="proto_config.proto_app_project_generator"></a>

### proto_app_project_generator
Application project generator
@interface ICanAddNode
@interface ISortingValue
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| plugin_guid | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Plugin&#34;)] @attr [Description(&#34;Plugins with generators&#34;)] @attr [Editor(typeof(EditorPluginSelection), typeof(ITypeEditor))] |
| description_plugin | [string](#string) |  | @attr [PropertyOrderAttribute(5)] @attr [DisplayName(&#34;Description&#34;)] @attr [ReadOnly(true)] |
| plugin_generator_guid | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Generator&#34;)] @attr [Description(&#34;Plugin generator&#34;)] @attr [Editor(typeof(EditorPluginGeneratorSelection), typeof(ITypeEditor))] |
| description_generator | [string](#string) |  | @attr [PropertyOrderAttribute(7)] @attr [DisplayName(&#34;Description&#34;)] @attr [ReadOnly(true)] |
| relative_path_to_gen_folder | [string](#string) |  | @attr [PropertyOrderAttribute(8)] @attr [DisplayName(&#34;Output Folder&#34;)] @attr [Editor(typeof(EditorFolderPicker), typeof(ITypeEditor))] @attr [Description(&#34;Project subfolder for generated file/files&#34;)] Relative folder path to project file |
| gen_file_name | [string](#string) |  | @attr [DisplayName(&#34;Output File&#34;)] @attr [PropertyOrderAttribute(9)] @attr [Description(&#34;Generator output file name&#34;)] Generator output file name |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| generator_settings | [string](#string) |  | @attr [Browsable(false)] |
| generator_settings_vm | [proto_plugin_generator_settings](#proto_config.proto_plugin_generator_settings) |  | @attr [PropertyOrderAttribute(29)] @attr [Browsable(false)] |
| conn_str | [string](#string) |  | @attr [PropertyOrderAttribute(9)] @attr [Description(&#34;Db connection string. Directly editable or generated based on settings&#34;)] |
| conn_str_to_prev_stable | [string](#string) |  | @attr [PropertyOrderAttribute(13)] @attr [DisplayName(&#34;Stable DB&#34;)] @attr [Description(&#34;Db connection string to previous stable version&#34;)] |
| is_generate_sql_sqript_to_update_prev_stable | [bool](#bool) |  | @attr [PropertyOrderAttribute(14)] @attr [DisplayName(&#34;Migrate DB&#34;)] @attr [Description(&#34;Generate Sql script to update stable DB version to current state&#34;)] |
| gen_script_file_name | [string](#string) |  | @attr [DisplayName(&#34;SQL file&#34;)] @attr [PropertyOrderAttribute(15)] @attr [Description(&#34;SQL script output file name&#34;)] Generator output file name |






<a name="proto_config.proto_app_solution"></a>

### proto_app_solution
@interface ICanAddNode
@interface ISortingValue
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| short_id_for_cache_key | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Short ID&#34;)] @attr [Description(&#34;Short solution ID for cache key generator. Need if projects from different solutions will use same cache storage instance in deployment&#34;)] |
| relative_app_solution_path | [string](#string) |  | @attr [PropertyOrderAttribute(7)] @attr [DisplayName(&#34;Path&#34;)] @attr [Editor(typeof(EditorSolutionPicker), typeof(ITypeEditor))] @attr [Description(&#34;.NET solution file path relative to configuration file path&#34;)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_app_projects | [proto_app_project](#proto_config.proto_app_project) | repeated | @attr [Browsable(false)] |
| list_generators_solution_settings | [proto_plugin_generator_solution_settings](#proto_config.proto_plugin_generator_solution_settings) | repeated | @attr [Browsable(false)]

repeated proto_plugin_group_generators_settings list_group_generators_settings = 18; |






<a name="proto_config.proto_base_config_link"></a>

### proto_base_config_link
@interface ISortingValue
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [ReadOnly(true)] |
| relative_config_file_path | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(EditorBaseConfigFilePicker), typeof(ITypeEditor))] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] @attr [DisplayName(&#34;For deletion&#34;)] @attr [Description(&#34;Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_catalog"></a>

### proto_catalog
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| use_tree | [bool](#bool) |  | @attr [PropertyOrderAttribute(20)] @attr [DisplayName(&#34;Use Tree&#34;)] @attr [Description(&#34;Use tree catalog structure&#34;)] |
| use_separate_tree_for_folders | [bool](#bool) |  | @attr [PropertyOrderAttribute(21)] @attr [DisplayName(&#34;Separate Tree&#34;)] @attr [Description(&#34;Separate tree object for folders&#34;)] |
| max_tree_levels | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Max Tree Levels&#34;)] @attr [Description(&#34;Maximum amount levels in catalog item groups. If zero, than unlimited&#34;)] |
| use_code_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(25)] @attr [DisplayName(&#34;Use Code&#34;)] @attr [Description(&#34;Use Code property for catalog item&#34;)] |
| code_property_settings | [proto_catalog_code_property_settings](#proto_config.proto_catalog_code_property_settings) |  | @attr [PropertyOrderAttribute(26)] @attr [DisplayName(&#34;Code&#34;)] @attr [Description(&#34;Code property settings for catalog item&#34;)] @attr [Editor(typeof(EditorPropertyGridDialog), typeof(EditorPropertyGridDialog))] |
| use_name_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(27)] @attr [DisplayName(&#34;Use Name&#34;)] @attr [Description(&#34;Use Name property for catalog item&#34;)] |
| max_name_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(28)] @attr [DisplayName(&#34;Name Length&#34;)] @attr [Description(&#34;Maximum catalog item name length. If zero, than unlimited length&#34;)] |
| use_description_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(29)] @attr [DisplayName(&#34;Use Description&#34;)] @attr [Description(&#34;Use Description property for catalog item&#34;)] |
| max_description_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(30)] @attr [DisplayName(&#34;Desc Length&#34;)] @attr [Description(&#34;Maximum catalog item description length. If zero, than unlimited length&#34;)] |
| index_unique_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| index_ref_folder_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| index_ref_tree_parent_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| index_not_unique_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| item_icon_type | [proto_enum_catalog_tree_icon](#proto_config.proto_enum_catalog_tree_icon) |  | @attr [PropertyOrderAttribute(41)] @attr [DisplayName(&#34;Item Icon&#34;)] @attr [Description(&#34;Catalog item icon type&#34;)] |
| group_icon_type | [proto_enum_catalog_tree_icon](#proto_config.proto_enum_catalog_tree_icon) |  | @attr [PropertyOrderAttribute(42)] @attr [DisplayName(&#34;Group Icon&#34;)] @attr [Description(&#34;Catalog group icon type&#34;)] |
| view_list_wide_guid | [string](#string) |  | @attr [Browsable(false)] |
| view_list_narrow_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_id_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_name_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_description_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_is_folder_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_ref_self_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_ref_folder_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_version_guid | [string](#string) |  | @attr [Browsable(false)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| folder | [proto_catalog_folder](#proto_config.proto_catalog_folder) |  | @attr [Browsable(false)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [Browsable(false)] |
| group_details | [proto_group_list_details](#proto_config.proto_group_list_details) |  | @attr [Browsable(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [Browsable(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [Browsable(false)] |
| list_role_catalog_access_settings | [proto_role_catalog_access](#proto_config.proto_role_catalog_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_catalog_code_property_settings"></a>

### proto_catalog_code_property_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| sequence_type | [proto_enum_code_type](#proto_config.proto_enum_code_type) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Sequence type&#34;)] @attr [Description(&#34;Sequence type general settings&#34;)] |
| max_sequence_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;Max Sequence&#34;)] @attr [Description(&#34;Maximum number of character places for generated sequence numbers. Valid sequence numbers from 1 to 999, where number &#39;9&#39; is limited by this parameter&#34;)] |
| prefix | [string](#string) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Prefix&#34;)] @attr [Description(&#34;Prefix for text code sequence&#34;)] |
| sequence_guid | [string](#string) |  | @attr [PropertyOrderAttribute(0)] @attr [DisplayName(&#34;Code Sequence&#34;)] @attr [Description(&#34;Code Sequence for auto code generation. Same sequence can be used for different catalogs&#34;)] @attr [Editor(typeof(EditorCodeSequenceSelection), typeof(EditorCodeSequenceSelection))] |
| unique_scope | [proto_enum_catalog_code_unique_scope](#proto_config.proto_enum_catalog_code_unique_scope) |  | @attr [PropertyOrderAttribute(7)] @attr [DisplayName(&#34;Unique Scope&#34;)] @attr [Description(&#34;Code has to be unique in selected scope&#34;)] |






<a name="proto_config.proto_catalog_folder"></a>

### proto_catalog_folder
C A T A L O G
@exclude
####################################### C A T A L O G ##########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] @attr [DisplayName(&#34;For deletion&#34;)] @attr [Description(&#34;Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version&#34;)] |
| property_id_guid | [string](#string) |  | @attr [Browsable(false)] |
| use_code_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(21)] @attr [DisplayName(&#34;Use Code&#34;)] @attr [Description(&#34;Use Code property for catalog item&#34;)] |
| code_property_settings | [proto_catalog_code_property_settings](#proto_config.proto_catalog_code_property_settings) |  | @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Code&#34;)] @attr [Description(&#34;Code property settings for catalog folder&#34;)] @attr [Editor(typeof(EditorPropertyGridDialog), typeof(EditorPropertyGridDialog))] |
| property_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_version_guid | [string](#string) |  | @attr [Browsable(false)] |
| index_unique_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| index_ref_tree_parent_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| index_not_unique_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| use_name_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(41)] @attr [DisplayName(&#34;Use Name&#34;)] @attr [Description(&#34;Use Name property for catalog item&#34;)] |
| max_name_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(42)] @attr [DisplayName(&#34;Max Length&#34;)] @attr [Description(&#34;Maximum catalog item name length. If zero, than unlimited length&#34;)] |
| property_name_guid | [string](#string) |  | @attr [Browsable(false)] |
| use_description_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(51)] @attr [DisplayName(&#34;Use Description&#34;)] @attr [Description(&#34;Use Description property for catalog item&#34;)] |
| max_description_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(52)] @attr [DisplayName(&#34;Max Length&#34;)] @attr [Description(&#34;Maximum catalog item description length. If zero, than unlimited length&#34;)] |
| property_description_guid | [string](#string) |  | @attr [Browsable(false)] |
| view_list_wide_guid | [string](#string) |  | @attr [Browsable(false)] |
| view_list_narrow_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_is_folder_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_ref_self_guid | [string](#string) |  | @attr [Browsable(false)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [Browsable(false)] |
| group_details | [proto_group_list_details](#proto_config.proto_group_list_details) |  | @attr [Browsable(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [Browsable(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [Browsable(false)] |
| list_role_catalog_access_settings | [proto_role_catalog_access](#proto_config.proto_role_catalog_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_code_sequence"></a>

### proto_code_sequence
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| sequence_type | [proto_enum_code_type](#proto_config.proto_enum_code_type) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(11)] @attr [DisplayName(&#34;Sequence type&#34;)] @attr [Description(&#34;Sequence type for code&#34;)] |
| max_sequence_length | [uint32](#uint32) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(12)] @attr [DisplayName(&#34;Max Sequence&#34;)] @attr [Description(&#34;Maximum number of character places for generated sequence numbers&#34;)] |
| prefix | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(13)] @attr [DisplayName(&#34;Prefix&#34;)] @attr [Description(&#34;Prefix for text code sequence&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_config"></a>

### proto_config
Configuration config


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| version | [int32](#int32) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [ReadOnly(true)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [ReadOnly(true)] |
| last_updated | [google.protobuf.Timestamp](#google.protobuf.Timestamp) |  | @attr [PropertyOrderAttribute(6)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_need_current_update | [bool](#bool) |  | @attr [Browsable(false)] |
| group_config_links | [proto_group_list_base_config_links](#proto_config.proto_group_list_base_config_links) |  | @attr [Browsable(false)] |
| model | [proto_model](#proto_config.proto_model) |  | @attr [Browsable(false)] |
| group_plugins | [proto_group_list_plugins](#proto_config.proto_group_list_plugins) |  | @attr [Browsable(false)] |
| group_app_solutions | [proto_group_list_app_solutions](#proto_config.proto_group_list_app_solutions) |  | @attr [Browsable(false)] |






<a name="proto_config.proto_config_short_history"></a>

### proto_config_short_history
@base VmEditable


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| current_config | [proto_config](#proto_config.proto_config) |  |  |
| prev_stable_config | [proto_config](#proto_config.proto_config) |  |  |






<a name="proto_config.proto_constant"></a>

### proto_constant
Constant application wise value
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [Description(&#34;Constant name&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)][DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [Description(&#34;Description of constant&#34;)] |
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [Browsable(false)] |
| is_nullable | [bool](#bool) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(20)] @attr [DisplayName(&#34;Can be NULL&#34;)] @attr [Description(&#34;If unchecked always expected data&#34;)] |
| default_value | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(8)] @attr [DisplayName(&#34;Default&#34;)] @attr [Description(&#34;Chunk of code to calculate Default value (can be inserted in generated code by generator if supported)&#34;)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| range_values_requirement_str | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(32)] @attr [DisplayName(&#34;Expected&#34;)] @attr [Description(&#34;Expected values or ranges of values. Use &#39;#&#39; to create range, and &#39;;&#39; to separate values or ranges&#34;)] |
| min_length_requirement | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(34)] @attr [DisplayName(&#34;Min Length&#34;)] @attr [Description(&#34;Minimum length of string&#34;)] |
| max_length_requirement | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(35)] @attr [DisplayName(&#34;Max Length&#34;)] @attr [Description(&#34;Maximum length of string&#34;)] |
| accuracy_for_time | [proto_enum_time_accuracy_type](#proto_config.proto_enum_time_accuracy_type) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(36)] @attr [DisplayName(&#34;Time accuracy&#34;)] @attr [Description(&#34;Time accuracy for TimeOnly type. Business model is expecting selected accuracy&#34;)] |
| is_try_attach | [bool](#bool) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;UI attach&#34;)] @attr [Description(&#34;UI engine will try put this field on same line as previous field&#34;)] |
| lines_on_screen | [int32](#int32) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;UI lines&#34;)] @attr [Description(&#34;Lines on screen for edit box&#34;)] |
| is_start_new_row | [bool](#bool) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;New UI row&#34;)] @attr [Description(&#34;Start new UI row for this property&#34;)] |
| tab_name | [string](#string) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Tab Name&#34;)] @attr [Description(&#34;If not empty, then start new tab in tab control. If empty, then continue adding fields in current control&#34;)] |
| is_start_new_tab_control | [bool](#bool) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;New Tab Control&#34;)] @attr [Description(&#34;Start new tab control as current control&#34;)] |
| is_stop_tab_control | [bool](#bool) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Stop Tab Control&#34;)] @attr [Description(&#34;Stop using tab control for layout&#34;)] |
| list_role_constant_access_settings | [proto_role_constant_access](#proto_config.proto_role_constant_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_data_type"></a>

### proto_data_type
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data_type_enum | [proto_enum_data_type](#proto_config.proto_enum_data_type) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Type&#34;)] |
| length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Length&#34;)] @attr [Description(&#34;Maximum length of data (characters in string, or decimal digits for numeric data)&#34;)] |
| is_positive | [bool](#bool) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Positive&#34;)] @attr [Description(&#34;Expected numerical value always &gt;= 0&#34;)] |
| accuracy | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(5)] @attr [DisplayName(&#34;Accuracy&#34;)] @attr [Description(&#34;Number of decimal places in fractional part for numeric data)&#34;)] |
| object_guid | [string](#string) |  | &lt;summary&gt; / Guid of complex type. It can be Guid of Enumeration, Catalog, Document. / Numerical, string, bool, date and similar are simple types. For simple types this property is empty. / If Guid of group types is assigned, then any type of such group of types is acceptable as type / If Guid is empty, but EnumDataType is Any, then any complex type is acceptable as type / &lt;/summary&gt; @attr [PropertyOrderAttribute(6)] @attr [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))] |
| list_object_guids | [string](#string) | repeated | &lt;summary&gt; / Guids of selected complex types, that are acceptable as types / &lt;/summary&gt; @attr [PropertyOrderAttribute(8)] |
| is_p_key | [bool](#bool) |  | @attr [Browsable(false)] |
| is_ref_parent | [bool](#bool) |  | @attr [Browsable(false)] |






<a name="proto_config.proto_detail"></a>

### proto_detail
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_index_fk | [bool](#bool) |  | Create Index for foreign key navigation property @attr [PropertyOrderAttribute(5)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [Browsable(false)] |
| group_details | [proto_group_list_details](#proto_config.proto_group_list_details) |  | @attr [Browsable(false)] |
| position | [uint32](#uint32) |  | Protobuf field position Reserved positions: 1 - primary key @attr [ReadOnly(true)] |
| use_code_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Use Code&#34;)] @attr [Description(&#34;Use Code property for detail item&#34;)] |
| use_name_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(24)] @attr [DisplayName(&#34;Use Name&#34;)] @attr [Description(&#34;Use Name property for detail item&#34;)] |
| use_description_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(26)] @attr [DisplayName(&#34;Use Description&#34;)] @attr [Description(&#34;Use Description property for detail item&#34;)] |
| short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| is_try_attach | [bool](#bool) |  | @attr [PropertyOrderAttribute(22)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;UI attach&#34;)] @attr [Description(&#34;UI engine will try put this detail block on same line as previous detail block or block of header fields&#34;)] |
| is_start_new_row | [bool](#bool) |  | @attr [PropertyOrderAttribute(23)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Start UI row&#34;)] @attr [Description(&#34;Start new UI row for this detaail block&#34;)] |
| is_start_new_tab | [bool](#bool) |  | @attr [PropertyOrderAttribute(24)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Start New Tab&#34;)] @attr [Description(&#34;Start tab in current tab control (create new tab control if current not created yet or start new tab control is specified). Tab name is taken from details name if not specified explicitly&#34;)] |
| tab_name | [string](#string) |  | @attr [PropertyOrderAttribute(25)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Tab Name&#34;)] @attr [Description(&#34;If not empty, try to find tab in previous tab controls. If not found, then start new tab in tab control. If empty, then continue adding data grids in current tab control&#34;)] |
| is_start_new_tab_control | [bool](#bool) |  | @attr [PropertyOrderAttribute(26)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Start Tab Control&#34;)] @attr [Description(&#34;Start new tab control as current control&#34;)] |
| is_stop_tab_control | [bool](#bool) |  | @attr [PropertyOrderAttribute(27)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Stop Tab Control&#34;)] @attr [Description(&#34;Stop using tab control for layout&#34;)] |
| view_list_wide_guid | [string](#string) |  | @attr [Browsable(false)] |
| view_list_narrow_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_id_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_code_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_name_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_description_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_ref_parent_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_version_guid | [string](#string) |  | @attr [Browsable(false)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [Browsable(false)] |
| list_role_detail_access_settings | [proto_role_detail_access](#proto_config.proto_role_detail_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_doc_in_journal"></a>

### proto_doc_in_journal
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  |  |
| list_property_guids | [string](#string) | repeated |  |






<a name="proto_config.proto_document"></a>

### proto_document
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| group_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [Browsable(false)] |
| group_details | [proto_group_list_details](#proto_config.proto_group_list_details) |  | @attr [Browsable(false)] |
| group_forms | [proto_group_list_forms](#proto_config.proto_group_list_forms) |  | @attr [Browsable(false)] |
| group_reports | [proto_group_list_reports](#proto_config.proto_group_list_reports) |  | @attr [Browsable(false)] |
| short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| use_doc_number_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(21)] @attr [DisplayName(&#34;Use Number&#34;)] @attr [Description(&#34;Use DocNumber property for document&#34;)] |
| doc_number_property_settings | [proto_document_number_property_settings](#proto_config.proto_document_number_property_settings) |  | @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Code&#34;)] @attr [Description(&#34;Code property settings for Document&#34;)] @attr [Editor(typeof(EditorPropertyGridDialog), typeof(EditorPropertyGridDialog))] |
| use_doc_date_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(24)] @attr [DisplayName(&#34;Use Date&#34;)] @attr [Description(&#34;Use Date property for document&#34;)] |
| index_year_doc_number_guid | [string](#string) |  | unique index of special field YEAR and DocNumber YEAR = DocDate.Year @attr [Browsable(false)] |
| index_quater_doc_number_guid | [string](#string) |  | unique index of special field QUATER and DocNumber QUATER = DocDate.Year * 4 &#43; DocDate.Month / 3 @attr [Browsable(false)] |
| index_month_doc_number_guid | [string](#string) |  | unique index of special field MONTH and DocNumber MONTH = DocDate.Year * 12 &#43; DocDate.Month @attr [Browsable(false)] |
| index_week_doc_number_guid | [string](#string) |  | unique index of special field WEEK and DocNumber WEEK = DocDate.Year * (365???) &#43; DocDate.Month / 3 @attr [Browsable(false)] |
| index_day_doc_number_guid | [string](#string) |  | unique index of special field Day and DocNumber DAY = DocDate.Year * 365?? &#43; DocDate.Day @attr [Browsable(false)] |
| index_unique_doc_number_guid | [string](#string) |  | @attr [Browsable(false)] |
| index_not_unique_doc_number_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_id_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_doc_number_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_doc_date_guid | [string](#string) |  | @attr [Browsable(false)] |
| property_version_guid | [string](#string) |  | @attr [Browsable(false)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| list_role_document_access_settings | [proto_role_document_access](#proto_config.proto_role_document_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_document_number_property_settings"></a>

### proto_document_number_property_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| sequence_type | [proto_enum_code_type](#proto_config.proto_enum_code_type) |  | @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Sequence type&#34;)] @attr [Description(&#34;Sequence type general settings&#34;)] |
| max_sequence_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;Max Sequence&#34;)] @attr [Description(&#34;Maximum number of character places for generated sequence numbers. Valid sequence numbers from 1 to 999, where number &#39;9&#39; is limited by this parameter&#34;)] |
| prefix | [string](#string) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Prefix&#34;)] @attr [Description(&#34;Prefix for text code sequence&#34;)] |
| sequence_guid | [string](#string) |  | @attr [PropertyOrderAttribute(0)] @attr [DisplayName(&#34;Code Sequence&#34;)] @attr [Description(&#34;Code Sequence for auto code generation. Same sequence can be used for different documents&#34;)] @attr [Editor(typeof(EditorCodeSequenceSelection), typeof(EditorCodeSequenceSelection))] |
| scope_of_unique | [proto_enum_doc_number_unique_scope](#proto_config.proto_enum_doc_number_unique_scope) |  | @attr [PropertyOrderAttribute(7)] @attr [DisplayName(&#34;Unique Scope&#34;)] @attr [Description(&#34;Code has to be unique in selected scope&#34;)] |
| scope_period_start_month | [proto_enum_months](#proto_config.proto_enum_months) |  | @attr [PropertyOrderAttribute(9)] @attr [DisplayName(&#34;Start Month&#34;)] @attr [Description(&#34;Start month of scope period&#34;)] |
| scope_period_start_month_day | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(10)] @attr [DisplayName(&#34;Start Day&#34;)] @attr [Description(&#34;Start month day of scope period&#34;)] |






<a name="proto_config.proto_enumeration"></a>

### proto_enumeration
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [Description(&#34;Enumeration name&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Description&#34;)] @attr [Description(&#34;Description of enumeration&#34;)] |
| data_type_enum | [enum_enumeration_type](#proto_config.enum_enumeration_type) |  | Enumeration element type @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Type&#34;)] |
| data_type_length | [int32](#int32) |  | Length of string if &#39;STRING&#39; is selected as enumeration element type @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(5)] @attr [DisplayName(&#34;Length&#34;)] |
| list_enumeration_pairs | [proto_enumeration_pair](#proto_config.proto_enumeration_pair) | repeated | @attr [DisplayName(&#34;Elements&#34;)] @attr [NewItemTypes(typeof(EnumerationPair))] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_enumeration_pair"></a>

### proto_enumeration_pair
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [Description(&#34;Enumeration element name&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(5)] @attr [Description(&#34;Description of enumeration element&#34;)] |
| value | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Value&#34;)] @attr [Description(&#34;Enumeration element value&#34;)] |
| is_default | [bool](#bool) |  | @attr [PropertyOrderAttribute(9)] @attr [DisplayName(&#34;Is default&#34;)] @attr [Description(&#34;Used as default value for enumeration&#34;)] |
| numeric_value | [int32](#int32) |  | @attr [PropertyOrderAttribute(8)] @attr [DisplayName(&#34;Numeric Value&#34;)] @attr [Description(&#34;Enumeration element numeric value&#34;)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form"></a>

### proto_form
Children collection can contain:
  - Children of Grid System
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| is_use_code | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;List form&#34;)] @attr [PropertyOrderAttribute(1)] @attr [DisplayName(&#34;Code&#34;)] @attr [Description(&#34;Use catalog item code for list view&#34;)] |
| is_use_name | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;List form&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;Name&#34;)] @attr [Description(&#34;Use catalog item name for list view&#34;)] |
| is_use_desc | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;List form&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Description&#34;)] @attr [Description(&#34;Use catalog item description for list view&#34;)] |
| is_use_folder_code | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;List form&#34;)] @attr [PropertyOrderAttribute(11)] @attr [DisplayName(&#34;Folder Code&#34;)] @attr [Description(&#34;Use catalog folder code for list view&#34;)] |
| is_use_folder_name | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;List form&#34;)] @attr [PropertyOrderAttribute(12)] @attr [DisplayName(&#34;Folder Name&#34;)] @attr [Description(&#34;Use catalog folder name for list view&#34;)] |
| is_use_folder_desc | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;List form&#34;)] @attr [PropertyOrderAttribute(13)] @attr [DisplayName(&#34;Folder Desc&#34;)] @attr [Description(&#34;Use catalog folder description for list view&#34;)] |
| is_use_doc_date | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;List form&#34;)] @attr [PropertyOrderAttribute(15)] @attr [DisplayName(&#34;Doc Date&#34;)] @attr [Description(&#34;Use document date for list view&#34;)] |
| is_dummy | [bool](#bool) |  | @attr [Category(&#34;Edit form&#34;)] @attr [PropertyOrderAttribute(15)] @attr [DisplayName(&#34;???&#34;)] @attr [Description(&#34;Not implemented&#34;)] |
| enum_form_type | [proto_form_type](#proto_config.proto_form_type) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Form type&#34;)] @attr [Description(&#34;Form type&#34;)] |
| grid_system | [proto_form_grid_system](#proto_config.proto_form_grid_system) |  | @attr [Browsable(false)] |
| list_guid_view_properties | [string](#string) | repeated | @attr [Browsable(false)] |
| list_guid_view_folder_properties | [string](#string) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_auto_layout_block"></a>

### proto_form_auto_layout_block
Children collection can contain:
  - Fields
  - Data grids
  - Grid Systems
  - Tab Controls
  - Auto Layout Blocks
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_form_auto_layout_sub_block | [proto_form_auto_layout_sub_block](#proto_config.proto_form_auto_layout_sub_block) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_auto_layout_sub_block"></a>

### proto_form_auto_layout_sub_block
https://learn.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| tab_control | [proto_form_tab_control](#proto_config.proto_form_tab_control) |  |  |
| data_grid_control | [proto_form_data_grid](#proto_config.proto_form_data_grid) |  |  |
| auto_layout_block_control | [proto_form_auto_layout_block](#proto_config.proto_form_auto_layout_block) |  |  |
| field_control | [proto_form_field](#proto_config.proto_form_field) |  |  |
| grid_system_control | [proto_form_grid_system](#proto_config.proto_form_grid_system) |  |  |
| tree_control | [proto_form_tree](#proto_config.proto_form_tree) |  |  |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_data_grid"></a>

### proto_form_data_grid
No Children
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_guid_properties | [string](#string) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_field"></a>

### proto_form_field
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_grid_system"></a>

### proto_form_grid_system
Children are collection of Grid System Rows 
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_rows | [proto_form_grid_system_row](#proto_config.proto_form_grid_system_row) | repeated | @attr [Browsable(false)] |
| list_guid_properties | [string](#string) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_grid_system_column"></a>

### proto_form_grid_system_column
Children are collection of Auto Layout Block children
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| hide_type | [proto_enum_hidden_type](#proto_config.proto_enum_hidden_type) |  | @attr [DisplayName(&#34;When Hide&#34;)] @attr [Description(&#34;Condition of hiding base on screen size&#34;)] |
| width_xs | [google.protobuf.UInt32Value](#google.protobuf.UInt32Value) |  | @attr [DisplayName(&#34;XS&#34;)] @attr [Description(&#34;Extra small. Small to large phone. Range: &lt; 600px&#34;)] |
| width_sm | [google.protobuf.UInt32Value](#google.protobuf.UInt32Value) |  | @attr [DisplayName(&#34;SM&#34;)] @attr [Description(&#34;Small. Small to medium tablet. Range: 600px &gt; &lt; 960px&#34;)] |
| width_md | [google.protobuf.UInt32Value](#google.protobuf.UInt32Value) |  | @attr [DisplayName(&#34;MD&#34;)] @attr [Description(&#34;Medium. Large tablet to laptop. Range: 960px &gt; &lt; 1280px&#34;)] |
| width_lg | [google.protobuf.UInt32Value](#google.protobuf.UInt32Value) |  | @attr [DisplayName(&#34;LG&#34;)] @attr [Description(&#34;Large. Desktop. Range: 1280px &gt; &lt; 1920px&#34;)] |
| width_xl | [google.protobuf.UInt32Value](#google.protobuf.UInt32Value) |  | @attr [DisplayName(&#34;XL&#34;)] @attr [Description(&#34;Extra Large. HD and 4k. Range: 1920px &gt; &lt; 2560px&#34;)] |
| width_xx | [google.protobuf.UInt32Value](#google.protobuf.UInt32Value) |  | @attr [DisplayName(&#34;XX&#34;)] @attr [Description(&#34;Extra Extra Large. 4k&#43; and ultra-wide. Range: &gt;= 2560px&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| form_block | [proto_form_auto_layout_block](#proto_config.proto_form_auto_layout_block) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_grid_system_row"></a>

### proto_form_grid_system_row
Children are collection of Grid System Columns 
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_columns | [proto_form_grid_system_column](#proto_config.proto_form_grid_system_column) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_tab_control"></a>

### proto_form_tab_control
Children are collection of Tab Control Tabs
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_tabs | [proto_form_tab_control_tab](#proto_config.proto_form_tab_control_tab) | repeated | @attr [Browsable(false)] |
| list_guid_properties | [string](#string) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_tab_control_tab"></a>

### proto_form_tab_control_tab
Children are collection of Auto Layout Block children
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_guid_properties | [string](#string) | repeated | @attr [Browsable(false)] |
| form_block | [proto_form_auto_layout_block](#proto_config.proto_form_auto_layout_block) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_form_tree"></a>

### proto_form_tree
No Children
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Tab control name&#34;)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Desc&#34;)] @attr [Description(&#34;Tab control description&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_guid_properties | [string](#string) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_constant_groups"></a>

### proto_group_constant_groups
C O N S T A N T
@exclude
####################################### C O N S T A N T ##########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [Description(&#34;Constants group name&#34;)] @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] @attr [ReadOnly(true)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [Description(&#34;Description constants group name&#34;)] |
| prefix_for_db_tables | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Db prefix&#34;)] @attr [Description(&#34;Prefix for constants db table names. Used if set to use in config model&#34;)] |
| list_constant_groups | [proto_group_list_constants](#proto_config.proto_group_list_constants) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_documents"></a>

### proto_group_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| prefix_for_db_tables | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Db prefix&#34;)] @attr [Description(&#34;Prefix for document db table names. Used if set to use in config model&#34;)] |
| group_shared_properties | [proto_group_list_properties](#proto_config.proto_group_list_properties) |  | @attr [Browsable(false)] @attr [Description(&#34;Properties for all documents&#34;)] |
| group_list_documents | [proto_group_list_documents](#proto_config.proto_group_list_documents) |  | @attr [Browsable(false)] |
| use_doc_code_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Use Code&#34;)] @attr [Description(&#34;Use Code property for document by default&#34;)] |
| monday_before_first_doc_date | [google.protobuf.Timestamp](#google.protobuf.Timestamp) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(23)] @attr [DisplayName(&#34;Monday Date&#34;)] @attr [Description(&#34;Initial date of Monday for calculation of relative current day, week, month, year. Can be used for DocNumber uniqueness restrictions &#34;)] @attr [ReadOnly(true)] |
| use_doc_date_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(24)] @attr [DisplayName(&#34;Use Date&#34;)] @attr [Description(&#34;Use Date property for document by default&#34;)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_app_solutions"></a>

### proto_group_list_app_solutions
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_app_solutions | [proto_app_solution](#proto_config.proto_app_solution) | repeated | List NET solutions @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_base_config_links"></a>

### proto_group_list_base_config_links
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_base_config_links | [proto_base_config_link](#proto_config.proto_base_config_link) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_catalogs"></a>

### proto_group_list_catalogs



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| prefix_for_db_tables | [string](#string) |  | @attr [PropertyOrderAttribute(4)] @attr [DisplayName(&#34;Db prefix&#34;)] @attr [Description(&#34;Prefix for catalog db table names. Used if set to use in config model&#34;)] |
| short_id_type_for_cache_key | [string](#string) |  | @attr [PropertyOrderAttribute(7)] @attr [DisplayName(&#34;Short ID&#34;)] @attr [Description(&#34;Short catalog type ID for cache key generator&#34;)] |
| list_catalogs | [proto_catalog](#proto_config.proto_catalog) | repeated | @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| use_code_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Use Code&#34;)] @attr [Description(&#34;Use Code property for catalog item by default&#34;)] |
| use_name_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(24)] @attr [DisplayName(&#34;Use Name&#34;)] @attr [Description(&#34;Use Name property for catalog item by default&#34;)] |
| use_description_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(26)] @attr [DisplayName(&#34;Use Description&#34;)] @attr [Description(&#34;Use Description property for catalog item by default&#34;)] |
| use_code_property_in_separate_tree | [bool](#bool) |  | @attr [PropertyOrderAttribute(32)] @attr [DisplayName(&#34;Tree Use Code&#34;)] @attr [Description(&#34;Use Code property in separate tree for catalog folder by default&#34;)] |
| use_name_property_in_separate_tree | [bool](#bool) |  | @attr [PropertyOrderAttribute(34)] @attr [DisplayName(&#34;Tree Use Name&#34;)] @attr [Description(&#34;Use Name property in separate tree for catalog item by default&#34;)] |
| use_description_property_in_separate_tree | [bool](#bool) |  | @attr [PropertyOrderAttribute(36)] @attr [DisplayName(&#34;Tree Use Description&#34;)] @attr [Description(&#34;Use Description property in separate tree for catalog item by default&#34;)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_common"></a>

### proto_group_list_common
Common parameters section


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| group_roles | [proto_group_list_roles](#proto_config.proto_group_list_roles) |  | @attr [Browsable(false)] |
| group_view_forms | [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms) |  | @attr [Browsable(false)] |
| group_list_sequences | [proto_group_list_sequences](#proto_config.proto_group_list_sequences) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_constants"></a>

### proto_group_list_constants
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [Description(&#34;Group name of constants&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_constants | [proto_constant](#proto_config.proto_constant) | repeated | @attr [Browsable(false)] |
| short_id_type_for_cache_key | [string](#string) |  | @attr [PropertyOrderAttribute(7)] @attr [DisplayName(&#34;Short ID&#34;)] @attr [Description(&#34;Short constant type ID for cache key generator&#34;)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| list_role_constant_access_settings | [proto_role_constant_access](#proto_config.proto_role_constant_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_details"></a>

### proto_group_list_details
D E T A I L S
@exclude
####################################### D E T A I L S ########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_details | [proto_detail](#proto_config.proto_detail) | repeated | @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| use_code_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Use Code&#34;)] @attr [Description(&#34;Use Code property for detail item&#34;)] |
| use_name_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(24)] @attr [DisplayName(&#34;Use Name&#34;)] @attr [Description(&#34;Use Name property for detail item&#34;)] |
| use_description_property | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [PropertyOrderAttribute(26)] @attr [DisplayName(&#34;Use Description&#34;)] @attr [Description(&#34;Use Description property for detail item&#34;)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| list_role_detail_access_settings | [proto_role_detail_access](#proto_config.proto_role_detail_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_documents"></a>

### proto_group_list_documents



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| short_id_type_for_cache_key | [string](#string) |  | @attr [PropertyOrderAttribute(6)] @attr [DisplayName(&#34;Short ID&#34;)] @attr [Description(&#34;Short document type ID for cache key generator&#34;)] |
| list_documents | [proto_document](#proto_config.proto_document) | repeated | @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_role_document_access_settings | [proto_role_document_access](#proto_config.proto_role_document_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_enumerations"></a>

### proto_group_list_enumerations
E N U M E R A T I O N
@exclude
####################################### E N U M E R A T I O N ##########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_enumerations | [proto_enumeration](#proto_config.proto_enumeration) | repeated | @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_forms"></a>

### proto_group_list_forms
F O R M S
@exclude
####################################### F O R M S ##########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_forms | [proto_form](#proto_config.proto_form) | repeated | repeated proto_property list_shared_properties = 6; @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_journals"></a>

### proto_group_list_journals
J O U R N A L
@exclude
####################################### J O U R N A L ##########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_journals | [proto_journal](#proto_config.proto_journal) | repeated | repeated proto_property list_shared_properties = 6; @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_main_view_forms"></a>

### proto_group_list_main_view_forms
main view forms hierarchy node with children


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_main_view_forms | [proto_main_view_form](#proto_config.proto_main_view_form) | repeated | @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_plugins"></a>

### proto_group_list_plugins
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] @attr [DisplayName(&#34;Plugins&#34;)] @attr [Description(&#34;Contains all registered plugins as children&#34;)] |
| list_plugins | [proto_plugin](#proto_config.proto_plugin) | repeated | @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_properties"></a>

### proto_group_list_properties
P R O P E R T Y
@exclude
####################################### P R O P E R T Y ##########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_properties | [proto_property](#proto_config.proto_property) | repeated | @attr [Browsable(false)] |
| last_gen_position | [uint32](#uint32) |  | Last generated Protobuf field position @attr [ReadOnly(true)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| list_role_property_access_settings | [proto_role_property_access](#proto_config.proto_role_property_access) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_reports"></a>

### proto_group_list_reports
R E P O R T S
@exclude
####################################### R E P O R T S ##########################################


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_reports | [proto_report](#proto_config.proto_report) | repeated | repeated proto_property list_shared_properties = 6; @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_roles"></a>

### proto_group_list_roles



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_roles | [proto_role](#proto_config.proto_role) | repeated | @attr [Browsable(false)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_group_list_sequences"></a>

### proto_group_list_sequences



| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_sequences | [proto_code_sequence](#proto_config.proto_code_sequence) | repeated | @attr [Browsable(false)] @attr [Description(&#34;Document number sequences&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_journal"></a>

### proto_journal
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| list_documents | [proto_document](#proto_config.proto_document) | repeated | repeated proto_group_properties list_properties = 6; @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| list_selected_docs_with_properties | [proto_doc_in_journal](#proto_config.proto_doc_in_journal) | repeated | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_main_view_form"></a>

### proto_main_view_form
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| group_list_view_forms | [proto_group_list_main_view_forms](#proto_config.proto_group_list_main_view_forms) |  | @attr [Browsable(false)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_model"></a>

### proto_model
Configuration model
@attr [CategoryOrder(&#34;Db Names Generation&#34;, 5)]


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| version | [int32](#int32) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(4)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| composite_name_max_length | [uint32](#uint32) |  | @attr [PropertyOrderAttribute(8)] @attr [DisplayName(&#34;Max length&#34;)] @attr [Category(&#34;Composite Names Generation&#34;)] |
| is_use_composite_names | [bool](#bool) |  | @attr [PropertyOrderAttribute(9)] @attr [DisplayName(&#34;Use Composite&#34;)] @attr [Description(&#34;Use parent-child composite names.&#34;)] @attr [Category(&#34;Composite Names Generation&#34;)] |
| is_use_group_prefix | [bool](#bool) |  | @attr [PropertyOrderAttribute(10)] @attr [DisplayName(&#34;Use Prefix&#34;)] @attr [Description(&#34;Composite names use their parent name as prefix. In a case of simple names all object&#39;s name will have only group name as a prefix.&#34;)] @attr [Category(&#34;Composite Names Generation&#34;)] |
| p_key_guid | [string](#string) |  | @attr [Browsable(false)] |
| p_key_name | [string](#string) |  | @attr [PropertyOrderAttribute(14)] @attr [DisplayName(&#34;Id name&#34;)] @attr [Description(&#34;Primary key field name&#34;)] @attr [Category(&#34;Property settings&#34;)] |
| p_key_type | [proto_enum_primary_key_type](#proto_config.proto_enum_primary_key_type) |  | @attr [PropertyOrderAttribute(15)] @attr [DisplayName(&#34;Id type&#34;)] @attr [Description(&#34;Primary key field type&#34;)] @attr [Category(&#34;Property settings&#34;)] |
| record_version_field_guid | [string](#string) |  | @attr [Browsable(false)] |
| record_version_field_name | [string](#string) |  | @attr [PropertyOrderAttribute(18)] @attr [DisplayName(&#34;Version field&#34;)] @attr [Description(&#34;Record version field name&#34;)] @attr [Category(&#34;Property settings&#34;)] |
| record_version_field_type | [proto_enum_version_field_type](#proto_config.proto_enum_version_field_type) |  | @attr [PropertyOrderAttribute(19)] @attr [DisplayName(&#34;Version type&#34;)] @attr [Description(&#34;Record version field type&#34;)] @attr [Category(&#34;Property settings&#34;)] |
| property_code_name | [string](#string) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(21)] @attr [DisplayName(&#34;Code property&#34;)] @attr [Description(&#34;Name of code auto generated property if it is used in catalog&#34;)] |
| use_code_property | [bool](#bool) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(22)] @attr [DisplayName(&#34;Use Code&#34;)] @attr [Description(&#34;Use Code property for catalog item by default&#34;)] |
| property_name_name | [string](#string) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(23)] @attr [DisplayName(&#34;Name property&#34;)] @attr [Description(&#34;Name of name auto generated property if it is used in catalog&#34;)] |
| use_name_property | [bool](#bool) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(24)] @attr [DisplayName(&#34;Use Name&#34;)] @attr [Description(&#34;Use Name property for catalog item by default&#34;)] |
| property_description_name | [string](#string) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(25)] @attr [DisplayName(&#34;Description property&#34;)] @attr [Description(&#34;Name of description auto generated property if it is used in catalog&#34;)] |
| use_description_property | [bool](#bool) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(26)] @attr [DisplayName(&#34;Use Description&#34;)] @attr [Description(&#34;Use Description property for catalog item by default&#34;)] |
| property_is_folder_name | [string](#string) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(27)] @attr [DisplayName(&#34;IsFolder property&#34;)] @attr [Description(&#34;Name of is folder auto generated property if it is used in catalog&#34;)] |
| property_doc_date_name | [string](#string) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(31)] @attr [DisplayName(&#34;Date property&#34;)] @attr [Description(&#34;Name of date auto generated property if it is used in documents&#34;)] |
| use_doc_date_property | [bool](#bool) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(32)] @attr [DisplayName(&#34;Use Doc Date&#34;)] @attr [Description(&#34;Use Date property for documents&#34;)] |
| property_doc_code_name | [string](#string) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(29)] @attr [DisplayName(&#34;Doc Code property&#34;)] @attr [Description(&#34;Name of document code auto generated property&#34;)] |
| use_doc_code_property | [bool](#bool) |  | @attr [Category(&#34;Property settings&#34;)] @attr [PropertyOrderAttribute(30)] @attr [DisplayName(&#34;Use Doc Code&#34;)] @attr [Description(&#34;Use Code property for documents&#34;)] |
| last_constant_group_short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| last_catalog_short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| last_document_short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| last_detail_short_id | [int32](#int32) |  | @attr [Browsable(false)] |
| is_grid_sortable | [bool](#bool) |  | @attr [Category(&#34;DataGrid settings&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [bool](#bool) |  | @attr [Category(&#34;DataGrid settings&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [bool](#bool) |  | @attr [Category(&#34;DataGrid settings&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| group_common | [proto_group_list_common](#proto_config.proto_group_list_common) |  | @attr [Browsable(false)] |
| group_constant_groups | [proto_group_constant_groups](#proto_config.proto_group_constant_groups) |  | @attr [Browsable(false)] |
| group_enumerations | [proto_group_list_enumerations](#proto_config.proto_group_list_enumerations) |  | @attr [Browsable(false)] |
| group_catalogs | [proto_group_list_catalogs](#proto_config.proto_group_list_catalogs) |  | @attr [Browsable(false)] |
| group_documents | [proto_group_documents](#proto_config.proto_group_documents) |  | @attr [Browsable(false)] |
| group_journals | [proto_group_list_journals](#proto_config.proto_group_list_journals) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






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
@interface ISortingValue
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| version | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] @attr [Description(&#34;Name of plugin&#34;)] |
| description | [string](#string) |  | @attr [ReadOnly(true)] @attr [Description(&#34;Description of plugin&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| list_generators | [proto_plugin_generator](#proto_config.proto_plugin_generator) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_plugin_generator"></a>

### proto_plugin_generator
@interface ISortingValue
@base ConfigObjectVmBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] @attr [ReadOnly(true)] @attr [Description(&#34;Name of plugin&#34;)] |
| description | [string](#string) |  | @attr [ReadOnly(true)] @attr [Description(&#34;Description of plugin&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |






<a name="proto_config.proto_plugin_generator_node_default_settings"></a>

### proto_plugin_generator_node_default_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| node_settings_vm_guid | [string](#string) |  | Guid of solution-project-generator node |
| settings | [string](#string) |  |  |






<a name="proto_config.proto_plugin_generator_node_settings"></a>

### proto_plugin_generator_node_settings
@interface ISortingValue
@base ConfigObjectCommonBase


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| app_project_generator_guid | [string](#string) |  | Guid of solution-project-generator node |
| name | [string](#string) |  | Name of solution-project-generator node |
| name_ui | [string](#string) |  |  |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| settings | [string](#string) |  | string node_settings_vm_guid = 6; |






<a name="proto_config.proto_plugin_generator_project_settings"></a>

### proto_plugin_generator_project_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| settings | [string](#string) |  | string app_generator_guid = 2; |






<a name="proto_config.proto_plugin_generator_settings"></a>

### proto_plugin_generator_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [PropertyOrderAttribute(1)] |
| name_ui | [string](#string) |  | @attr [PropertyOrderAttribute(2)] |
| app_project_generator_guid | [string](#string) |  | Guid of solution-project-generator node |
| settings | [string](#string) |  |  |






<a name="proto_config.proto_plugin_generator_solution_settings"></a>

### proto_plugin_generator_solution_settings
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| settings | [string](#string) |  | string app_generator_guid = 2; |






<a name="proto_config.proto_property"></a>

### proto_property
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] @attr [Description(&#34;Property name&#34;)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Typically used as UI field label&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Description&#34;)] @attr [Description(&#34;Description of property&#34;)] |
| data_type | [proto_data_type](#proto_config.proto_data_type) |  | @attr [Browsable(false)] |
| is_nullable | [bool](#bool) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(20)] @attr [DisplayName(&#34;Can be NULL&#34;)] @attr [Description(&#34;If unchecked always expected data&#34;)] |
| default_value | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(8)] @attr [DisplayName(&#34;Default&#34;)] @attr [Description(&#34;Chunk of code to calculate Default value (can be inserted in generated code by generator if supported)&#34;)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| range_values_requirement_str | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(32)] @attr [DisplayName(&#34;Expected&#34;)] @attr [Description(&#34;Expected values or ranges of values. Use &#39;#&#39; to create range, and &#39;;&#39; to separate values or ranges&#34;)] |
| min_length_requirement | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(34)] @attr [DisplayName(&#34;Min Length&#34;)] @attr [Description(&#34;Minimum length of string&#34;)] |
| max_length_requirement | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(35)] @attr [DisplayName(&#34;Max Length&#34;)] @attr [Description(&#34;Maximum length of string&#34;)] |
| accuracy_for_time | [proto_enum_time_accuracy_type](#proto_config.proto_enum_time_accuracy_type) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(36)] @attr [DisplayName(&#34;Time accuracy&#34;)] @attr [Description(&#34;Time accuracy for TimeOnly type. Business model is expecting selected accuracy&#34;)] |
| is_try_attach | [bool](#bool) |  | @attr [PropertyOrderAttribute(23)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;UI attach&#34;)] @attr [Description(&#34;UI engine will try put this field on same line as previous field&#34;)] |
| lines_on_screen | [int32](#int32) |  | @attr [PropertyOrderAttribute(22)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;UI lines&#34;)] @attr [Description(&#34;Lines on screen for edit box&#34;)] |
| is_start_new_row | [bool](#bool) |  | @attr [PropertyOrderAttribute(24)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Start UI row&#34;)] @attr [Description(&#34;Start new UI row for this property&#34;)] |
| tab_name | [string](#string) |  | @attr [PropertyOrderAttribute(26)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Tab Name&#34;)] @attr [Description(&#34;If not empty, then start new tab in tab control. If empty, then continue adding fields in current control&#34;)] |
| is_start_new_tab_control | [bool](#bool) |  | @attr [PropertyOrderAttribute(25)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Start Tab Control&#34;)] @attr [Description(&#34;Start new tab control as current control&#34;)] |
| is_stop_tab_control | [bool](#bool) |  | @attr [PropertyOrderAttribute(27)] @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Stop Tab Control&#34;)] @attr [Description(&#34;Stop using tab control for layout&#34;)] |
| data_generator | [proto_property_data_generator](#proto_config.proto_property_data_generator) |  | @attr [PropertyOrderAttribute(31)] @attr [ExpandableObjectAttribute()] @attr [DisplayName(&#34;Data Generator&#34;)] @attr [Browsable(false)] |
| list_role_property_access_settings | [proto_role_property_access](#proto_config.proto_role_property_access) | repeated | @attr [Browsable(false)]

// @attr [PropertyOrderAttribute(28)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;Start Grid&#34;)] // @attr [Description(&#34;Start new container of 12 columns grid system&#34;)] bool is_start_12_col_grid_system = 28; // @attr [PropertyOrderAttribute(29)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;Stop Grid&#34;)] // @attr [Description(&#34;Stop current container of 12 columns grid system&#34;)] bool is_stop_12_col_grid_system = 29; // @attr [PropertyOrderAttribute(30)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;Start Column&#34;)] // @attr [Description(&#34;Start new column of 12 columns grid system&#34;)] bool is_start_new_column_12_col_grid_system = 30; // @attr [PropertyOrderAttribute(32)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;Start Row&#34;)] // @attr [Description(&#34;Start new row of 12 columns grid system&#34;)] bool is_start_new_row_12_col_grid_system = 31; // @attr [PropertyOrderAttribute(31)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;Column Name&#34;)] // @attr [Description(&#34;Column Name of 12 columns grid system&#34;)] string column_name_12_col_grid_system = 32; // @attr [PropertyOrderAttribute(33)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;When Hide&#34;)] // @attr [Description(&#34;Condition of hiding base on screen size&#34;)] proto_enum_hidden_type hide_type = 33; // @attr [PropertyOrderAttribute(34)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;XS&#34;)] // @attr [Description(&#34;Extra small. Small to large phone. Range: &lt; 600px&#34;)] google.protobuf.UInt32Value width_xs = 34; // @attr [PropertyOrderAttribute(35)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;SM&#34;)] // @attr [Description(&#34;Small. Small to medium tablet. Range: 600px &gt; &lt; 960px&#34;)] google.protobuf.UInt32Value width_sm = 35; // @attr [PropertyOrderAttribute(36)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;MD&#34;)] // @attr [Description(&#34;Medium. Large tablet to laptop. Range: 960px &gt; &lt; 1280px&#34;)] google.protobuf.UInt32Value width_md = 36; // @attr [PropertyOrderAttribute(37)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;LG&#34;)] // @attr [Description(&#34;Large. Desktop. Range: 1280px &gt; &lt; 1920px&#34;)] google.protobuf.UInt32Value width_lg = 37; // @attr [PropertyOrderAttribute(38)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;XL&#34;)] // @attr [Description(&#34;Extra Large. HD and 4k. Range: 1920px &gt; &lt; 2560px&#34;)] google.protobuf.UInt32Value width_xl = 38; // @attr [PropertyOrderAttribute(39)] // @attr [Category(&#34;12 Column Grid System&#34;)] // @attr [DisplayName(&#34;XX&#34;)] // @attr [Description(&#34;Extra Extra Large. 4k&#43; and ultra-wide. Range: &gt;= 2560px&#34;)] google.protobuf.UInt32Value width_xx = 39; |
| is_grid_sortable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Sortable&#34;)] @attr [Description(&#34;Sortable in data grid&#34;)] |
| is_grid_sortable_custom | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Custom Sortable&#34;)] @attr [Description(&#34;Custom sortable in data grid by using custom function&#34;)] |
| is_grid_filterable | [proto_enum_use_type](#proto_config.proto_enum_use_type) |  | @attr [Category(&#34;Auto Layout&#34;)] @attr [DisplayName(&#34;Filterable&#34;)] @attr [Description(&#34;Filterable in data grid&#34;)] |
| position | [uint32](#uint32) |  | Protobuf field position Reserved positions: 1 - primary key @attr [ReadOnly(true)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_property_data_generator"></a>

### proto_property_data_generator
@base BaseSettings


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data_type | [proto_enum_property_data_type](#proto_config.proto_enum_property_data_type) |  | @attr [PropertyOrderAttribute(3)] @attr [DisplayName(&#34;Group&#34;)] @attr [Description(&#34;Data generator type&#34;)] |
| address | [proto_enum_address_data_type](#proto_config.proto_enum_address_data_type) |  |  |
| commerce | [proto_enum_commerce_data_type](#proto_config.proto_enum_commerce_data_type) |  |  |
| company | [proto_enum_company_data_type](#proto_config.proto_enum_company_data_type) |  |  |
| date | [proto_enum_date_data_type](#proto_config.proto_enum_date_data_type) |  |  |
| database | [proto_enum_database_data_type](#proto_config.proto_enum_database_data_type) |  |  |
| finance | [proto_enum_finance_data_type](#proto_config.proto_enum_finance_data_type) |  |  |
| hacker | [proto_enum_hacker_data_type](#proto_config.proto_enum_hacker_data_type) |  |  |
| image | [proto_enum_image_data_type](#proto_config.proto_enum_image_data_type) |  |  |
| internet | [proto_enum_internet_data_type](#proto_config.proto_enum_internet_data_type) |  |  |
| lorem | [proto_enum_lorem_data_type](#proto_config.proto_enum_lorem_data_type) |  |  |
| name | [proto_enum_name_data_type](#proto_config.proto_enum_name_data_type) |  |  |
| phone | [proto_enum_phone_data_type](#proto_config.proto_enum_phone_data_type) |  |  |
| rant | [proto_enum_rant_data_type](#proto_config.proto_enum_rant_data_type) |  |  |
| system | [proto_enum_system_data_type](#proto_config.proto_enum_system_data_type) |  |  |
| vehicle | [proto_enum_vehicle_data_type](#proto_config.proto_enum_vehicle_data_type) |  |  |
| random | [proto_enum_random_data_type](#proto_config.proto_enum_random_data_type) |  |  |






<a name="proto_config.proto_report"></a>

### proto_report
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | repeated proto_group_properties list_properties = 6; repeated proto_document list_documents = 7; @attr [Browsable(false)] |






<a name="proto_config.proto_role"></a>

### proto_role
@interface ICanAddNode
@interface ISortingValue


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(-2)] @attr [ReadOnly(true)] |
| name | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(1)] |
| sorting_value | [uint64](#uint64) |  | @attr [Browsable(false)] |
| name_ui | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(2)] @attr [DisplayName(&#34;UI name&#34;)] @attr [Description(&#34;Used as label/name for UI&#34;)] |
| description | [string](#string) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(3)] |
| is_new | [bool](#bool) |  | @attr [Browsable(false)] |
| is_marked_for_deletion | [bool](#bool) |  | @attr [Browsable(false)] |
| default_constant_print_access_settings | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(11)] @attr [DisplayName(&#34;Print Constants&#34;)] @attr [Description(&#34;Default constant-print access for role&#34;)] |
| default_constant_edit_access_settings | [proto_enum_constant_access](#proto_config.proto_enum_constant_access) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(12)] @attr [DisplayName(&#34;Edit Constants&#34;)] @attr [Description(&#34;Default constant-edit access for role&#34;)] |
| default_catalog_print_access_settings | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(13)] @attr [DisplayName(&#34;Print Catalog&#34;)] @attr [Description(&#34;Default catalog-print access for role&#34;)] |
| default_catalog_edit_access_settings | [proto_enum_catalog_detail_access](#proto_config.proto_enum_catalog_detail_access) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(14)] @attr [DisplayName(&#34;Edit Catalog&#34;)] @attr [Description(&#34;Default catalog-edit access for role&#34;)] |
| default_document_print_access_settings | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(15)] @attr [DisplayName(&#34;Print Document&#34;)] @attr [Description(&#34;Default document-print access for role&#34;)] |
| default_document_edit_access_settings | [proto_enum_document_access](#proto_config.proto_enum_document_access) |  | @attr [Category(&#34;&#34;)] @attr [PropertyOrderAttribute(16)] @attr [DisplayName(&#34;Edit Document&#34;)] @attr [Description(&#34;Default document-edit access for role&#34;)] |
| list_node_generators_settings | [proto_plugin_generator_node_settings](#proto_config.proto_plugin_generator_node_settings) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_role_catalog_access"></a>

### proto_role_catalog_access
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Browsable(false)] |
| edit_access | [proto_enum_catalog_detail_access](#proto_config.proto_enum_catalog_detail_access) |  |  |
| print_access | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  |  |






<a name="proto_config.proto_role_constant_access"></a>

### proto_role_constant_access
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Browsable(false)] |
| edit_access | [proto_enum_constant_access](#proto_config.proto_enum_constant_access) |  |  |
| print_access | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  |  |






<a name="proto_config.proto_role_detail_access"></a>

### proto_role_detail_access
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Browsable(false)] |
| edit_access | [proto_enum_catalog_detail_access](#proto_config.proto_enum_catalog_detail_access) |  |  |
| print_access | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  |  |






<a name="proto_config.proto_role_document_access"></a>

### proto_role_document_access
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Browsable(false)] |
| edit_access | [proto_enum_document_access](#proto_config.proto_enum_document_access) |  |  |
| print_access | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  |  |






<a name="proto_config.proto_role_property_access"></a>

### proto_role_property_access
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [Browsable(false)] |
| edit_access | [proto_enum_property_access](#proto_config.proto_enum_property_access) |  |  |
| print_access | [proto_enum_print_access](#proto_config.proto_enum_print_access) |  |  |






<a name="proto_config.proto_settings_config"></a>

### proto_settings_config
@base VmEditable


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
| list_open_config_history | [proto_user_settings_opened_config](#proto_config.proto_user_settings_opened_config) | repeated | @attr [Browsable(false)] |






<a name="proto_config.proto_user_settings_opened_config"></a>

### proto_user_settings_opened_config
@base VmValidatableWithSeverity


| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| guid | [string](#string) |  | @attr [ReadOnly(true)] |
| opened_last_time_on | [google.protobuf.Timestamp](#google.protobuf.Timestamp) |  | @attr [Browsable(false)] |
| config_path | [string](#string) |  | @attr [Browsable(false)] |





 


<a name="proto_config.enum_enumeration_type"></a>

### enum_enumeration_type
Enumeration member type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| INTEGER_VALUE | 0 | @attr [Description(&#34;Int&#34;)] |
| SHORT_VALUE | 11 | @attr [Description(&#34;Short&#34;)] |
| BYTE_VALUE | 21 | @attr [Description(&#34;Byte&#34;)] |
| STRING_VALUE | 31 | @attr [Description(&#34;String&#34;)] |



<a name="proto_config.proto_enum_address_data_type"></a>

### proto_enum_address_data_type
https://github.com/bchavez/Bogus
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| A_NONE | 0 |  |
| A_COUNTRY | 1 |  |
| A_COUNTRY_CODE | 2 |  |
| A_COUNTY | 3 |  |
| A_STATE | 4 |  |
| A_STATE_ABBR | 5 |  |
| A_ZIP_CODE | 6 |  |
| A_CITY_PREFIX | 7 |  |
| A_CITY | 8 |  |
| A_CITY_SUFFIX | 9 |  |
| A_STREET_ADDRESS | 10 |  |
| A_STREET_NAME | 11 |  |
| A_STREET_SUFFIX | 12 |  |
| A_BUILDING_NUMBER | 13 |  |
| A_SECONDARY_ADDRESS | 14 |  |
| A_FULL_ADDRESS | 15 |  |
| A_LATITUDE | 16 |  |
| A_LONGITUDE | 17 |  |
| A_DIRECTION | 18 |  |
| A_CARDINAL_DIRECTION | 19 |  |
| A_ORDINAL_DIRECTION | 20 |  |



<a name="proto_config.proto_enum_catalog_code_unique_scope"></a>

### proto_enum_catalog_code_unique_scope
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| code_unique_in_whole_catalog | 0 | @attr [Description(&#34;Whole Catalog&#34;)] |
| code_uniqueness_by_folder_settings | 11 | @attr [Description(&#34;Catalog Folder&#34;)] |
| code_not_unique_settings | 41 | @attr [Description(&#34;Not Unique&#34;)] |



<a name="proto_config.proto_enum_catalog_detail_access"></a>

### proto_enum_catalog_detail_access
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| C_BY_PARENT | 0 | @attr [Description(&#34;By Parent&#34;)] |
| C_HIDE | 11 | @attr [Description(&#34;Hide&#34;)] |
| C_VIEW | 21 | @attr [Description(&#34;View&#34;)] |
| C_EDIT_ITEMS | 31 | @attr [Description(&#34;Edit Items&#34;)] |
| C_EDIT_FOLDERS | 41 | @attr [Description(&#34;Edit Folders&#34;)] |
| C_MARK_DEL | 51 | @attr [Description(&#34;Del&#34;)] |



<a name="proto_config.proto_enum_catalog_tree_icon"></a>

### proto_enum_catalog_tree_icon
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| None | 0 |  |
| Item | 11 |  |
| Folder | 21 |  |
| Custom | 31 |  |



<a name="proto_config.proto_enum_code_type"></a>

### proto_enum_code_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| Number | 0 | @attr [Description(&#34;Number&#34;)] |
| Text | 1 | @attr [Description(&#34;Text&#34;)] |
| AutoNumber | 2 | @attr [Description(&#34;Auto Number&#34;)] |
| AutoText | 3 | @attr [Description(&#34;Auto Text&#34;)] |



<a name="proto_config.proto_enum_commerce_data_type"></a>

### proto_enum_commerce_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| C_NONE | 0 |  |
| C_DEPARTMENT | 1 |  |
| C_CATEGORIES | 2 |  |
| C_PRODUCT_NAME | 3 |  |
| C_PRODUCT | 4 |  |
| C_PRODUCT_ADJECTIVE | 5 |  |
| C_PRODUCT_MATERIAL | 6 |  |
| C_COLOR | 7 |  |
| C_PRICE | 8 |  |
| C_EAN8 | 9 |  |
| C_EAN13 | 10 |  |



<a name="proto_config.proto_enum_company_data_type"></a>

### proto_enum_company_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| P_NONE | 0 |  |
| P_COMPANY_NAME | 1 |  |
| P_COMPANY_SUFFIX | 2 |  |
| P_CATCH_PHRASE | 3 |  |
| P_BS | 4 |  |



<a name="proto_config.proto_enum_constant_access"></a>

### proto_enum_constant_access
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| CN_BY_PARENT | 0 | @attr [Description(&#34;By Parent&#34;)] |
| CN_HIDE | 11 | @attr [Description(&#34;Hide&#34;)] |
| CN_VIEW | 21 | @attr [Description(&#34;View&#34;)]

with history |
| CN_EDIT | 31 | @attr [Description(&#34;Edit&#34;)]

with history |



<a name="proto_config.proto_enum_data_type"></a>

### proto_enum_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| CHAR | 0 | @attr [Description(&#34;Char&#34;)] |
| STRING | 11 | @attr [Description(&#34;String&#34;)] |
| NUMERICAL | 21 | @attr [Description(&#34;Numerical&#34;)] |
| BOOL | 31 | @attr [Description(&#34;Boolean&#34;)] |
| TIME | 41 | @attr [Description(&#34;Time&#34;)] |
| DATE | 51 | @attr [Description(&#34;Date&#34;)] |
| DATETIMELOCAL | 61 | @attr [Description(&#34;DateTime Local&#34;)] |
| DATETIMEUTC | 71 | @attr [Description(&#34;DateTime UTC&#34;)] |
| ENUMERATION | 81 | @attr [Description(&#34;Enumeration&#34;)] |
| CATALOG | 91 | @attr [Description(&#34;Catalog&#34;)] |
| CATALOGS | 101 | @attr [Description(&#34;Catalogs&#34;)] |
| DOCUMENT | 111 | @attr [Description(&#34;Document&#34;)] |
| DOCUMENTS | 121 | @attr [Description(&#34;Documents&#34;)] |
| ANY | 131 | @attr [Description(&#34;Any&#34;)] |



<a name="proto_config.proto_enum_database_data_type"></a>

### proto_enum_database_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| DB_NONE | 0 |  |
| DB_COLUMN | 1 |  |
| DB_TYPE | 2 |  |
| DB_COLLATION | 3 |  |
| DB_ENGINE | 4 |  |



<a name="proto_config.proto_enum_date_data_type"></a>

### proto_enum_date_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| D_NONE | 0 |  |
| D_PAST_OFFSET | 1 |  |
| D_RECENT_OFFSET | 2 |  |
| D_BETWEEN_OFFSET | 3 |  |
| D_SOON_OFFSET | 4 |  |
| D_FUTURE_OFFSET | 5 |  |
| D_PAST | 6 |  |
| D_RECENT | 7 |  |
| D_BETWEEN | 8 |  |
| D_SOON | 9 |  |
| D_FUTURE | 10 |  |
| D_TIMESPAN | 11 |  |
| D_MONTH | 12 |  |
| D_WEEKDAY | 13 |  |



<a name="proto_config.proto_enum_doc_number_unique_scope"></a>

### proto_enum_doc_number_unique_scope
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| DOC_UNIQUE_FOREVER | 0 | @attr [Description(&#34;Allways&#34;)] |
| DOC_UNIQUE_YEAR | 11 | @attr [Description(&#34;Year&#34;)] |
| DOC_UNIQUE_QUATER | 21 | @attr [Description(&#34;Quater&#34;)] |
| DOC_UNIQUE_MONTH | 31 | @attr [Description(&#34;Month&#34;)] |
| DOC_UNIQUE_WEEK | 34 | @attr [Description(&#34;Week&#34;)] |
| DOC_UNIQUE_DAY | 37 | @attr [Description(&#34;Day&#34;)] |
| DOC_UNIQUE_NOT_REQUIRED | 41 | @attr [Description(&#34;Not Required&#34;)] |



<a name="proto_config.proto_enum_document_access"></a>

### proto_enum_document_access
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| D_BY_PARENT | 0 | @attr [Description(&#34;By Parent&#34;)] |
| D_HIDE | 11 | @attr [Description(&#34;Hide&#34;)] |
| D_VIEW | 21 | @attr [Description(&#34;View&#34;)] |
| D_EDIT | 31 | @attr [Description(&#34;Edit&#34;)] |
| D_POST | 41 | @attr [Description(&#34;Post&#34;)] |
| D_UNPOST | 51 | @attr [Description(&#34;Unpost&#34;)] |
| D_MARK_DEL | 61 | @attr [Description(&#34;Del&#34;)] |



<a name="proto_config.proto_enum_finance_data_type"></a>

### proto_enum_finance_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| F_NONE | 0 |  |
| F_ACCOUNT | 1 |  |
| F_ACCOUNT_NAME | 2 |  |
| F_TRANSACTION_TYPE | 3 |  |
| F_CURRENCY | 4 |  |
| F_CREDIT_CARD_NUMBER | 5 |  |
| F_CREDIT_CARD_CVV | 6 |  |
| F_BITCOIN_ADDRESS | 7 |  |
| F_ETHEREUM_ADDRESS | 8 |  |
| F_ROUTING_NUMBER | 9 |  |
| F_BIC | 10 |  |
| F_IBAN | 11 |  |



<a name="proto_config.proto_enum_hacker_data_type"></a>

### proto_enum_hacker_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| H_NONE | 0 |  |
| H_ABBREVIATION | 1 |  |
| H_ADJECTIVE | 2 |  |
| H_NOUN | 3 |  |
| H_VERB | 4 |  |
| H_INGVERB | 5 |  |
| H_PHRASE | 6 |  |



<a name="proto_config.proto_enum_hidden_type"></a>

### proto_enum_hidden_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| NeverHide | 0 | @attr [Description(&#34;Never Hide&#34;)] |
| Xs | 11 | @attr [Description(&#34;Hide on Extra small screen&#34;)] |
| SmAndDown | 21 | @attr [Description(&#34;Hide on Small screen and smaller&#34;)] |
| MdAndDown | 31 | @attr [Description(&#34;Hide on Medium screen and smaller&#34;)] |
| LgAndDown | 41 | @attr [Description(&#34;Hide on Large screen and smaller&#34;)] |
| XlAndDown | 51 | @attr [Description(&#34;Hide on Extra Large screen and smaller&#34;)] |



<a name="proto_config.proto_enum_image_data_type"></a>

### proto_enum_image_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| I_NONE | 0 |  |
| I_DATAURI | 1 |  |
| I_PICSUM | 2 |  |
| I_PLACEHOLDER | 3 |  |
| I_LOREMFLICKR | 4 |  |
| I_LOREMPIXEL_ABSTRACT | 5 |  |
| I_LOREMPIXEL_ANIMALS | 6 |  |
| I_LOREMPIXEL_BUSINESS | 7 |  |
| I_LOREMPIXEL_CATS | 8 |  |
| I_LOREMPIXEL_CITY | 9 |  |
| I_LOREMPIXEL_FOOD | 10 |  |
| I_LOREMPIXEL_NIGHTLIFE | 11 |  |
| I_LOREMPIXEL_FASHION | 12 |  |
| I_LOREMPIXEL_PEOPLE | 13 |  |
| I_LOREMPIXEL_NATURE | 14 |  |
| I_LOREMPIXEL_SPORTS | 15 |  |
| I_LOREMPIXEL_TECHNICS | 16 |  |
| I_LOREMPIXEL_TRANSPORT | 17 |  |



<a name="proto_config.proto_enum_internet_data_type"></a>

### proto_enum_internet_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| N_NONE | 0 |  |
| N_AVATAR | 1 |  |
| N_EMAIL | 2 |  |
| N_EMAIL_EXAMPLE | 3 |  |
| N_USER_NAME | 4 |  |
| N_USER_NAME_UNICODE | 5 |  |
| N_DOMAIN_NAME | 6 |  |
| N_DOMAIN_WORD | 7 |  |
| N_DOMAIN_SUFFIX | 8 |  |
| N_IP | 9 |  |
| N_PORT | 10 |  |
| N_IP_ADDRESS | 11 |  |
| N_IP_END_POINT | 12 |  |
| N_IPV6 | 13 |  |
| N_IPV6_ADDRESS | 14 |  |
| N_IPV6_END_POINT | 15 |  |
| N_USER_AGENT | 16 |  |
| N_MAC | 17 |  |
| N_PASSWORD | 18 |  |
| N_COLOR | 19 |  |
| N_PROTOCOL | 20 |  |
| N_URL | 21 |  |
| N_URL_WITH_PATH | 22 |  |
| N_URL_ROOTED_PATH | 23 |  |



<a name="proto_config.proto_enum_lorem_data_type"></a>

### proto_enum_lorem_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| L_NONE | 0 |  |
| L_WORD | 1 |  |
| L_WORDS | 2 |  |
| L_LETTER | 3 |  |
| L_SENTENCE | 4 |  |
| L_SENTENCES | 5 |  |
| L_PARAGRAPH | 6 |  |
| L_PARAGRAPHS | 7 |  |
| L_TEXT | 8 |  |
| L_LINES | 9 |  |
| L_SLUG | 10 |  |



<a name="proto_config.proto_enum_months"></a>

### proto_enum_months
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| MONTH_NOT_SELECTED | 0 | @attr [Description(&#34;Not Selected&#34;)] |
| MONTH_JANUARY | 1 | @attr [Description(&#34;January&#34;)] |
| MONTH_FEBRUARY | 2 | @attr [Description(&#34;February&#34;)] |
| MONTH_MARCH | 3 | @attr [Description(&#34;March&#34;)] |
| MONTH_APRIL | 4 | @attr [Description(&#34;April&#34;)] |
| MONTH_MAY | 5 | @attr [Description(&#34;May&#34;)] |
| MONTH_JUNE | 6 | @attr [Description(&#34;June&#34;)] |
| MONTH_JULY | 7 | @attr [Description(&#34;July&#34;)] |
| MONTH_AUGUST | 8 | @attr [Description(&#34;August&#34;)] |
| MONTH_SEPTEMBER | 9 | @attr [Description(&#34;September&#34;)] |
| MONTH_OCTOBER | 10 | @attr [Description(&#34;October&#34;)] |
| MONTH_NOVEMBER | 11 | @attr [Description(&#34;November&#34;)] |
| MONTH_DECEMBER | 12 | @attr [Description(&#34;December&#34;)] |



<a name="proto_config.proto_enum_name_data_type"></a>

### proto_enum_name_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| M_NONE | 0 |  |
| M_FIRST_NAME | 1 |  |
| M_LAST_NAME | 2 |  |
| M_FULL_NAME | 3 |  |
| M_PREFIX | 4 |  |
| M_SUFFIX | 5 |  |
| M_FIND_NAME | 6 |  |
| M_JOB_TITLE | 7 |  |
| M_JOB_DESCRIPTOR | 8 |  |
| M_JOB_AREA | 9 |  |
| M_JOB_TYPE | 10 |  |



<a name="proto_config.proto_enum_phone_data_type"></a>

### proto_enum_phone_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| PH_NONE | 0 |  |
| PH_PHONE_NUMBER | 1 |  |
| PH_PHONE_NUMBER_FORMAT | 2 |  |



<a name="proto_config.proto_enum_primary_key_type"></a>

### proto_enum_primary_key_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| INT | 0 | @attr [Description(&#34;Int&#34;)] |
| LONG | 11 | @attr [Description(&#34;Long&#34;)] |



<a name="proto_config.proto_enum_print_access"></a>

### proto_enum_print_access
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| PR_BY_PARENT | 0 | @attr [Description(&#34;By Parent&#34;)] |
| PR_NO_PRINT | 11 | @attr [Description(&#34;No print&#34;)] |
| PR_PRINT | 21 | @attr [Description(&#34;Print&#34;)] |



<a name="proto_config.proto_enum_property_access"></a>

### proto_enum_property_access
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| P_BY_PARENT | 0 | @attr [Description(&#34;By Parent&#34;)] |
| P_HIDE | 11 | @attr [Description(&#34;Hide&#34;)] |
| P_VIEW | 21 | @attr [Description(&#34;View&#34;)]

with history |
| P_EDIT | 31 | @attr [Description(&#34;Edit&#34;)]

with history |



<a name="proto_config.proto_enum_property_data_type"></a>

### proto_enum_property_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| PT_NONE | 0 |  |
| PT_ADDRESS | 1 |  |
| PT_COMMERCE | 2 |  |
| PT_COMPANY | 3 |  |
| PT_DATE | 4 |  |
| PT_DATABASE | 5 |  |
| PT_FINANCE | 6 |  |
| PT_HACKER | 7 |  |
| PT_IMAGE | 8 |  |
| PT_INTERNET | 9 |  |
| PT_LOREM | 10 |  |
| PT_NAME | 11 |  |
| PT_PHONE | 12 |  |
| PT_RANT | 13 |  |
| PT_SYSTEM | 14 |  |
| PT_VEHICLE | 15 |  |
| PT_RANDOM | 16 |  |



<a name="proto_config.proto_enum_random_data_type"></a>

### proto_enum_random_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| RN_NONE | 0 |  |
| RN_NUMBER | 1 |  |
| RN_STRING | 2 |  |
| RN_STRING2 | 3 |  |
| RN_HASH | 4 |  |
| RN_ALPHANUMERIC | 5 |  |
| RN_HEXADECIMAL | 6 |  |



<a name="proto_config.proto_enum_rant_data_type"></a>

### proto_enum_rant_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| R_NONE | 0 |  |
| R_REVIEW | 1 |  |
| R_REVIEWS | 2 |  |



<a name="proto_config.proto_enum_system_data_type"></a>

### proto_enum_system_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| S_NONE | 0 |  |
| S_FILE_NAME | 1 |  |
| S_DIRECTORY_PATH | 2 |  |
| S_FILE_PATH | 3 |  |
| S_COMMON_FILE_NAME | 4 |  |
| S_MIME_TYPE | 5 |  |
| S_COMMON_FILE_TYPE | 6 |  |
| S_COMMON_FILE_EXT | 7 |  |
| S_FILE_TYPE | 8 |  |
| S_FILE_EXT | 9 |  |
| S_SEMVER | 10 |  |
| S_VERSION | 11 |  |
| S_EXCEPTION | 12 |  |
| S_ANDROID_ID | 13 |  |
| S_APPLE_PUSH_TOKEN | 14 |  |
| S_BLACKBERRY_PIN | 15 |  |



<a name="proto_config.proto_enum_time_accuracy_type"></a>

### proto_enum_time_accuracy_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| SECOND | 0 | @attr [Description(&#34;Second&#34;)] |
| MINUTE | 1 | @attr [Description(&#34;Minute&#34;)] |
| HOUR | 2 | @attr [Description(&#34;Hour&#34;)] |
| MS | 3 | @attr [Description(&#34;Millisecond&#34;)] |
| MAX | 5 | @attr [Description(&#34;100 nanoseconds&#34;)] |



<a name="proto_config.proto_enum_use_type"></a>

### proto_enum_use_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| Default | 0 |  |
| Yes | 11 |  |
| No | 21 |  |



<a name="proto_config.proto_enum_vehicle_data_type"></a>

### proto_enum_vehicle_data_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| V_NONE | 0 |  |
| V_VIN | 1 |  |
| V_MANUFACTURER | 2 |  |
| V_MODEL | 3 |  |
| V_TYPE | 4 |  |
| V_FUEL | 5 |  |



<a name="proto_config.proto_enum_version_field_type"></a>

### proto_enum_version_field_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| VER_BYTE | 0 | @attr [Description(&#34;Byte&#34;)] |
| VER_SHORT | 11 | @attr [Description(&#34;Short&#34;)] |
| VER_INT | 21 | @attr [Description(&#34;Int&#34;)] |
| VER_LONG | 31 | @attr [Description(&#34;Long&#34;)] |



<a name="proto_config.proto_form_orientation"></a>

### proto_form_orientation
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| Vertical | 0 |  |
| Horizontal | 11 |  |



<a name="proto_config.proto_form_type"></a>

### proto_form_type
@attr [TypeConverter(typeof(EnumDescriptionTypeConverter))]

| Name | Number | Description |
| ---- | ------ | ----------- |
| FormTypeNotSelected | 0 | @attr [Description(&#34;Not selected&#34;)] |
| ListWide | 11 | @attr [Description(&#34;Wide list view form&#34;)] |
| ItemEditForm | 21 | @attr [Description(&#34;Item edit form&#34;)] |
| FolderEditForm | 31 | @attr [Description(&#34;Folder edit form&#34;)] |
| ListNarrow | 41 | @attr [Description(&#34;Narrow list view form&#34;)] |


 

 

 



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

