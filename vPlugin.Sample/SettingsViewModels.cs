// Auto generated on UTC 06/10/2020 08:20:34
using System;
using System.Linq;
using ViewModelBase;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vSharpStudio.common;
using Google.Protobuf;

namespace vPlugin.Sample // NameSpace.tt Line: 22
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IPluginSampleAcceptVisitor // NameSpace.tt Line: 28
    {
        void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor);
    }
    public partial class GeneratorDbSchemaSettings : VmValidatableWithSeverity<GeneratorDbSchemaSettings, GeneratorDbSchemaSettings.GeneratorDbSchemaSettingsValidator>, IGeneratorDbSchemaSettings // Class.tt Line: 6
    {
        public partial class GeneratorDbSchemaSettingsValidator : ValidatorBase<GeneratorDbSchemaSettings, GeneratorDbSchemaSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GeneratorDbSchemaSettings() 
            : base(GeneratorDbSchemaSettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbSchemaSettings Clone(GeneratorDbSchemaSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            GeneratorDbSchemaSettings vm = new GeneratorDbSchemaSettings();
            vm.IsNotNotifying = true;
            vm.IsSchemaParam1 = from.IsSchemaParam1; // Clone.tt Line: 63
            vm.IsSchemaParam2 = from.IsSchemaParam2.HasValue ? from.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 56
            vm.SchemaParam3 = from.SchemaParam3; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GeneratorDbSchemaSettings to, GeneratorDbSchemaSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.IsSchemaParam1 = from.IsSchemaParam1; // Clone.tt Line: 136
            to.IsSchemaParam2 = from.IsSchemaParam2.HasValue ? from.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 131
            to.SchemaParam3 = from.SchemaParam3; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
        #region IEditable
        public override GeneratorDbSchemaSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GeneratorDbSchemaSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbSchemaSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbSchemaSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_schema_settings' to 'GeneratorDbSchemaSettings'
        public static GeneratorDbSchemaSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_settings m, GeneratorDbSchemaSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsSchemaParam1 = m.IsSchemaParam1; // Clone.tt Line: 214
            vm.IsSchemaParam2 = m.IsSchemaParam2.HasValue ? (bool?)m.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 214
            vm.SchemaParam3 = m.SchemaParam3; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaSettings' to 'proto_generator_db_schema_settings'
        public static Proto.Plugin.proto_generator_db_schema_settings ConvertToProto(GeneratorDbSchemaSettings vm) // Clone.tt Line: 228
        {
            Proto.Plugin.proto_generator_db_schema_settings m = new Proto.Plugin.proto_generator_db_schema_settings(); // Clone.tt Line: 230
            m.IsSchemaParam1 = vm.IsSchemaParam1; // Clone.tt Line: 267
            m.IsSchemaParam2 = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 244
            m.IsSchemaParam2.HasValue = vm.IsSchemaParam2.HasValue;
            if (vm.IsSchemaParam2.HasValue)
                m.IsSchemaParam2.Value = vm.IsSchemaParam2.Value;
            m.SchemaParam3 = vm.SchemaParam3; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsSchemaParam1 // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsSchemaParam1; 
            }
            set
            {
                if (this._IsSchemaParam1 != value)
                {
                    this.OnIsSchemaParam1Changing(ref value);
                    this._IsSchemaParam1 = value;
                    this.OnIsSchemaParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsSchemaParam1;
        partial void OnIsSchemaParam1Changing(ref bool to); // Property.tt Line: 157
        partial void OnIsSchemaParam1Changed();
        bool IGeneratorDbSchemaSettings.IsSchemaParam1 { get { return this._IsSchemaParam1; } }
        
        public bool? IsSchemaParam2 // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsSchemaParam2; 
            }
            set
            {
                if (this._IsSchemaParam2 != value)
                {
                    this.OnIsSchemaParam2Changing(ref value);
                    this._IsSchemaParam2 = value;
                    this.OnIsSchemaParam2Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool? _IsSchemaParam2;
        partial void OnIsSchemaParam2Changing(ref bool? to); // Property.tt Line: 157
        partial void OnIsSchemaParam2Changed();
        bool? IGeneratorDbSchemaSettings.IsSchemaParam2 { get { return this._IsSchemaParam2; } }
        
        public string SchemaParam3 // Property.tt Line: 135
        { 
            get 
            { 
                return this._SchemaParam3; 
            }
            set
            {
                if (this._SchemaParam3 != value)
                {
                    this.OnSchemaParam3Changing(ref value);
                    this._SchemaParam3 = value;
                    this.OnSchemaParam3Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _SchemaParam3 = string.Empty;
        partial void OnSchemaParam3Changing(ref string to); // Property.tt Line: 157
        partial void OnSchemaParam3Changed();
        string IGeneratorDbSchemaSettings.SchemaParam3 { get { return this._SchemaParam3; } }
    
        #endregion Properties
    }
    public partial class GeneratorDbAccessSettings : VmValidatableWithSeverity<GeneratorDbAccessSettings, GeneratorDbAccessSettings.GeneratorDbAccessSettingsValidator>, IGeneratorDbAccessSettings // Class.tt Line: 6
    {
        public partial class GeneratorDbAccessSettingsValidator : ValidatorBase<GeneratorDbAccessSettings, GeneratorDbAccessSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GeneratorDbAccessSettings() 
            : base(GeneratorDbAccessSettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessSettings Clone(GeneratorDbAccessSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            GeneratorDbAccessSettings vm = new GeneratorDbAccessSettings();
            vm.IsNotNotifying = true;
            vm.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 63
            vm.IsAccessParam2 = from.IsAccessParam2.HasValue ? from.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 56
            vm.AccessParam3 = from.AccessParam3; // Clone.tt Line: 63
            vm.IsGenerateNotValidCode = from.IsGenerateNotValidCode; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings to, GeneratorDbAccessSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 136
            to.IsAccessParam2 = from.IsAccessParam2.HasValue ? from.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 131
            to.AccessParam3 = from.AccessParam3; // Clone.tt Line: 136
            to.IsGenerateNotValidCode = from.IsGenerateNotValidCode; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
        #region IEditable
        public override GeneratorDbAccessSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GeneratorDbAccessSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbAccessSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbAccessSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_access_settings' to 'GeneratorDbAccessSettings'
        public static GeneratorDbAccessSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_settings m, GeneratorDbAccessSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsAccessParam1 = m.IsAccessParam1; // Clone.tt Line: 214
            vm.IsAccessParam2 = m.IsAccessParam2.HasValue ? (bool?)m.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 214
            vm.AccessParam3 = m.AccessParam3; // Clone.tt Line: 214
            vm.IsGenerateNotValidCode = m.IsGenerateNotValidCode; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings' to 'proto_generator_db_access_settings'
        public static Proto.Plugin.proto_generator_db_access_settings ConvertToProto(GeneratorDbAccessSettings vm) // Clone.tt Line: 228
        {
            Proto.Plugin.proto_generator_db_access_settings m = new Proto.Plugin.proto_generator_db_access_settings(); // Clone.tt Line: 230
            m.IsAccessParam1 = vm.IsAccessParam1; // Clone.tt Line: 267
            m.IsAccessParam2 = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 244
            m.IsAccessParam2.HasValue = vm.IsAccessParam2.HasValue;
            if (vm.IsAccessParam2.HasValue)
                m.IsAccessParam2.Value = vm.IsAccessParam2.Value;
            m.AccessParam3 = vm.AccessParam3; // Clone.tt Line: 267
            m.IsGenerateNotValidCode = vm.IsGenerateNotValidCode; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsAccessParam1 // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsAccessParam1; 
            }
            set
            {
                if (this._IsAccessParam1 != value)
                {
                    this.OnIsAccessParam1Changing(ref value);
                    this._IsAccessParam1 = value;
                    this.OnIsAccessParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsAccessParam1;
        partial void OnIsAccessParam1Changing(ref bool to); // Property.tt Line: 157
        partial void OnIsAccessParam1Changed();
        bool IGeneratorDbAccessSettings.IsAccessParam1 { get { return this._IsAccessParam1; } }
        
        public bool? IsAccessParam2 // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsAccessParam2; 
            }
            set
            {
                if (this._IsAccessParam2 != value)
                {
                    this.OnIsAccessParam2Changing(ref value);
                    this._IsAccessParam2 = value;
                    this.OnIsAccessParam2Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool? _IsAccessParam2;
        partial void OnIsAccessParam2Changing(ref bool? to); // Property.tt Line: 157
        partial void OnIsAccessParam2Changed();
        bool? IGeneratorDbAccessSettings.IsAccessParam2 { get { return this._IsAccessParam2; } }
        
        public string AccessParam3 // Property.tt Line: 135
        { 
            get 
            { 
                return this._AccessParam3; 
            }
            set
            {
                if (this._AccessParam3 != value)
                {
                    this.OnAccessParam3Changing(ref value);
                    this._AccessParam3 = value;
                    this.OnAccessParam3Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _AccessParam3 = string.Empty;
        partial void OnAccessParam3Changing(ref string to); // Property.tt Line: 157
        partial void OnAccessParam3Changed();
        string IGeneratorDbAccessSettings.AccessParam3 { get { return this._AccessParam3; } }
        
        public bool IsGenerateNotValidCode // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsGenerateNotValidCode; 
            }
            set
            {
                if (this._IsGenerateNotValidCode != value)
                {
                    this.OnIsGenerateNotValidCodeChanging(ref value);
                    this._IsGenerateNotValidCode = value;
                    this.OnIsGenerateNotValidCodeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsGenerateNotValidCode;
        partial void OnIsGenerateNotValidCodeChanging(ref bool to); // Property.tt Line: 157
        partial void OnIsGenerateNotValidCodeChanged();
        bool IGeneratorDbAccessSettings.IsGenerateNotValidCode { get { return this._IsGenerateNotValidCode; } }
    
        #endregion Properties
    }
    public partial class GeneratorDbAccessNodeSettings : VmValidatableWithSeverity<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettings.GeneratorDbAccessNodeSettingsValidator>, IGeneratorDbAccessNodeSettings // Class.tt Line: 6
    {
        public partial class GeneratorDbAccessNodeSettingsValidator : ValidatorBase<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GeneratorDbAccessNodeSettings() 
            : base(GeneratorDbAccessNodeSettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessNodeSettings Clone(GeneratorDbAccessNodeSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            GeneratorDbAccessNodeSettings vm = new GeneratorDbAccessNodeSettings();
            vm.IsNotNotifying = true;
            vm.IsParam1 = from.IsParam1; // Clone.tt Line: 63
            vm.IsIncluded = from.IsIncluded.HasValue ? from.IsIncluded.Value : (bool?)null; // Clone.tt Line: 56
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GeneratorDbAccessNodeSettings to, GeneratorDbAccessNodeSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.IsParam1 = from.IsParam1; // Clone.tt Line: 136
            to.IsIncluded = from.IsIncluded.HasValue ? from.IsIncluded.Value : (bool?)null; // Clone.tt Line: 131
        }
        // Clone.tt Line: 142
        #region IEditable
        public override GeneratorDbAccessNodeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GeneratorDbAccessNodeSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbAccessNodeSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbAccessNodeSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_access_node_settings' to 'GeneratorDbAccessNodeSettings'
        public static GeneratorDbAccessNodeSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_settings m, GeneratorDbAccessNodeSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsParam1 = m.IsParam1; // Clone.tt Line: 214
            vm.IsIncluded = m.IsIncluded.HasValue ? (bool?)m.IsIncluded.Value : (bool?)null; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodeSettings' to 'proto_generator_db_access_node_settings'
        public static Proto.Plugin.proto_generator_db_access_node_settings ConvertToProto(GeneratorDbAccessNodeSettings vm) // Clone.tt Line: 228
        {
            Proto.Plugin.proto_generator_db_access_node_settings m = new Proto.Plugin.proto_generator_db_access_node_settings(); // Clone.tt Line: 230
            m.IsParam1 = vm.IsParam1; // Clone.tt Line: 267
            m.IsIncluded = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 244
            m.IsIncluded.HasValue = vm.IsIncluded.HasValue;
            if (vm.IsIncluded.HasValue)
                m.IsIncluded.Value = vm.IsIncluded.Value;
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsParam1 // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsParam1; 
            }
            set
            {
                if (this._IsParam1 != value)
                {
                    this.OnIsParam1Changing(ref value);
                    this._IsParam1 = value;
                    this.OnIsParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsParam1;
        partial void OnIsParam1Changing(ref bool to); // Property.tt Line: 157
        partial void OnIsParam1Changed();
        bool IGeneratorDbAccessNodeSettings.IsParam1 { get { return this._IsParam1; } }
        
        public bool? IsIncluded // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsIncluded; 
            }
            set
            {
                if (this._IsIncluded != value)
                {
                    this.OnIsIncludedChanging(ref value);
                    this._IsIncluded = value;
                    this.OnIsIncludedChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool? _IsIncluded;
        partial void OnIsIncludedChanging(ref bool? to); // Property.tt Line: 157
        partial void OnIsIncludedChanged();
        bool? IGeneratorDbAccessNodeSettings.IsIncluded { get { return this._IsIncluded; } }
    
        #endregion Properties
    }
    public partial class GeneratorDbAccessNodePropertySettings : VmValidatableWithSeverity<GeneratorDbAccessNodePropertySettings, GeneratorDbAccessNodePropertySettings.GeneratorDbAccessNodePropertySettingsValidator>, IGeneratorDbAccessNodePropertySettings // Class.tt Line: 6
    {
        public partial class GeneratorDbAccessNodePropertySettingsValidator : ValidatorBase<GeneratorDbAccessNodePropertySettings, GeneratorDbAccessNodePropertySettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GeneratorDbAccessNodePropertySettings() 
            : base(GeneratorDbAccessNodePropertySettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessNodePropertySettings Clone(GeneratorDbAccessNodePropertySettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            GeneratorDbAccessNodePropertySettings vm = new GeneratorDbAccessNodePropertySettings();
            vm.IsNotNotifying = true;
            vm.IsPropertyParam1 = from.IsPropertyParam1; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GeneratorDbAccessNodePropertySettings to, GeneratorDbAccessNodePropertySettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.IsPropertyParam1 = from.IsPropertyParam1; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
        #region IEditable
        public override GeneratorDbAccessNodePropertySettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GeneratorDbAccessNodePropertySettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbAccessNodePropertySettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbAccessNodePropertySettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_access_node_property_settings' to 'GeneratorDbAccessNodePropertySettings'
        public static GeneratorDbAccessNodePropertySettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_property_settings m, GeneratorDbAccessNodePropertySettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsPropertyParam1 = m.IsPropertyParam1; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodePropertySettings' to 'proto_generator_db_access_node_property_settings'
        public static Proto.Plugin.proto_generator_db_access_node_property_settings ConvertToProto(GeneratorDbAccessNodePropertySettings vm) // Clone.tt Line: 228
        {
            Proto.Plugin.proto_generator_db_access_node_property_settings m = new Proto.Plugin.proto_generator_db_access_node_property_settings(); // Clone.tt Line: 230
            m.IsPropertyParam1 = vm.IsPropertyParam1; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsPropertyParam1 // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsPropertyParam1; 
            }
            set
            {
                if (this._IsPropertyParam1 != value)
                {
                    this.OnIsPropertyParam1Changing(ref value);
                    this._IsPropertyParam1 = value;
                    this.OnIsPropertyParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsPropertyParam1;
        partial void OnIsPropertyParam1Changing(ref bool to); // Property.tt Line: 157
        partial void OnIsPropertyParam1Changed();
        bool IGeneratorDbAccessNodePropertySettings.IsPropertyParam1 { get { return this._IsPropertyParam1; } }
    
        #endregion Properties
    }
    public partial class GeneratorDbAccessNodeCatalogFormSettings : VmValidatableWithSeverity<GeneratorDbAccessNodeCatalogFormSettings, GeneratorDbAccessNodeCatalogFormSettings.GeneratorDbAccessNodeCatalogFormSettingsValidator>, IGeneratorDbAccessNodeCatalogFormSettings // Class.tt Line: 6
    {
        public partial class GeneratorDbAccessNodeCatalogFormSettingsValidator : ValidatorBase<GeneratorDbAccessNodeCatalogFormSettings, GeneratorDbAccessNodeCatalogFormSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GeneratorDbAccessNodeCatalogFormSettings() 
            : base(GeneratorDbAccessNodeCatalogFormSettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessNodeCatalogFormSettings Clone(GeneratorDbAccessNodeCatalogFormSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            GeneratorDbAccessNodeCatalogFormSettings vm = new GeneratorDbAccessNodeCatalogFormSettings();
            vm.IsNotNotifying = true;
            vm.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GeneratorDbAccessNodeCatalogFormSettings to, GeneratorDbAccessNodeCatalogFormSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
        #region IEditable
        public override GeneratorDbAccessNodeCatalogFormSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GeneratorDbAccessNodeCatalogFormSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbAccessNodeCatalogFormSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbAccessNodeCatalogFormSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_access_node_catalog_form_settings' to 'GeneratorDbAccessNodeCatalogFormSettings'
        public static GeneratorDbAccessNodeCatalogFormSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_catalog_form_settings m, GeneratorDbAccessNodeCatalogFormSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsCatalogFormParam1 = m.IsCatalogFormParam1; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodeCatalogFormSettings' to 'proto_generator_db_access_node_catalog_form_settings'
        public static Proto.Plugin.proto_generator_db_access_node_catalog_form_settings ConvertToProto(GeneratorDbAccessNodeCatalogFormSettings vm) // Clone.tt Line: 228
        {
            Proto.Plugin.proto_generator_db_access_node_catalog_form_settings m = new Proto.Plugin.proto_generator_db_access_node_catalog_form_settings(); // Clone.tt Line: 230
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsCatalogFormParam1 // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsCatalogFormParam1; 
            }
            set
            {
                if (this._IsCatalogFormParam1 != value)
                {
                    this.OnIsCatalogFormParam1Changing(ref value);
                    this._IsCatalogFormParam1 = value;
                    this.OnIsCatalogFormParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsCatalogFormParam1;
        partial void OnIsCatalogFormParam1Changing(ref bool to); // Property.tt Line: 157
        partial void OnIsCatalogFormParam1Changed();
        bool IGeneratorDbAccessNodeCatalogFormSettings.IsCatalogFormParam1 { get { return this._IsCatalogFormParam1; } }
    
        #endregion Properties
    }
    
    public interface IVisitorProto // IVisitorProto.tt Line: 7
    {
        void Visit(Proto.Plugin.proto_generator_db_schema_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_node_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_node_property_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_node_catalog_form_settings p);
    }
    
    public partial class ValidationPluginSampleVisitor : PluginSampleVisitor // ValidationVisitor.tt Line: 7
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(GeneratorDbSchemaSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbSchemaSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbAccessSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbAccessSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbAccessNodeSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbAccessNodeSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbAccessNodePropertySettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbAccessNodePropertySettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbAccessNodeCatalogFormSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbAccessNodeCatalogFormSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
    }
    
    public partial class PluginSampleVisitor : IVisitorPluginSampleNode // NodeVisitor.tt Line: 7
    {
        public CancellationToken Token { get { return _cancellationToken; } }
        protected CancellationToken _cancellationToken;
    
        public void Visit(GeneratorDbSchemaSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorDbSchemaSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorDbSchemaSettings p) { }
        protected virtual void OnVisitEnd(GeneratorDbSchemaSettings p) { }
        public void Visit(GeneratorDbAccessSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorDbAccessSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorDbAccessSettings p) { }
        protected virtual void OnVisitEnd(GeneratorDbAccessSettings p) { }
        public void Visit(GeneratorDbAccessNodeSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorDbAccessNodeSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorDbAccessNodeSettings p) { }
        protected virtual void OnVisitEnd(GeneratorDbAccessNodeSettings p) { }
        public void Visit(GeneratorDbAccessNodePropertySettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorDbAccessNodePropertySettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorDbAccessNodePropertySettings p) { }
        protected virtual void OnVisitEnd(GeneratorDbAccessNodePropertySettings p) { }
        public void Visit(GeneratorDbAccessNodeCatalogFormSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorDbAccessNodeCatalogFormSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorDbAccessNodeCatalogFormSettings p) { }
        protected virtual void OnVisitEnd(GeneratorDbAccessNodeCatalogFormSettings p) { }
    }
    
    public interface IVisitorPluginSampleNode // IVisitorConfigNode.tt Line: 7
    {
        System.Threading.CancellationToken Token { get; }
    }
}