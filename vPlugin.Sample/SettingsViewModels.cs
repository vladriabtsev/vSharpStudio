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
using System.Diagnostics.Contracts;
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
    public partial class DbConnectionStringSettingsValidator : ValidatorBase<DbConnectionStringSettings, DbConnectionStringSettingsValidator> { } // Class.tt Line: 6
    public partial class DbConnectionStringSettings : VmValidatableWithSeverity<DbConnectionStringSettings, DbConnectionStringSettingsValidator>, IDbConnectionStringSettings // Class.tt Line: 7
    {
        #region CTOR
        public DbConnectionStringSettings() 
            : base(DbConnectionStringSettingsValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static DbConnectionStringSettings Clone(IDbConnectionStringSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            DbConnectionStringSettings vm = new DbConnectionStringSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.StringSettings = from.StringSettings; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(DbConnectionStringSettings to, IDbConnectionStringSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.StringSettings = from.StringSettings; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override DbConnectionStringSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return DbConnectionStringSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(DbConnectionStringSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            DbConnectionStringSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_db_connection_string_settings' to 'DbConnectionStringSettings'
        public static DbConnectionStringSettings ConvertToVM(Proto.Plugin.proto_db_connection_string_settings m, DbConnectionStringSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.StringSettings = m.StringSettings; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'DbConnectionStringSettings' to 'proto_db_connection_string_settings'
        public static Proto.Plugin.proto_db_connection_string_settings ConvertToProto(DbConnectionStringSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Plugin.proto_db_connection_string_settings m = new Proto.Plugin.proto_db_connection_string_settings(); // Clone.tt Line: 239
            m.StringSettings = vm.StringSettings; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public string StringSettings // Property.tt Line: 58
        { 
            get { return this._StringSettings; }
            set
            {
                if (this._StringSettings != value)
                {
                    this.OnStringSettingsChanging(ref value);
                    this._StringSettings = value;
                    this.OnStringSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _StringSettings = string.Empty;
        partial void OnStringSettingsChanging(ref string to); // Property.tt Line: 82
        partial void OnStringSettingsChanged();
        #endregion Properties
    }
    public partial class PluginsGroupSettingsValidator : ValidatorBase<PluginsGroupSettings, PluginsGroupSettingsValidator> { } // Class.tt Line: 6
    public partial class PluginsGroupSettings : VmValidatableWithSeverity<PluginsGroupSettings, PluginsGroupSettingsValidator>, IPluginsGroupSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginsGroupSettings() 
            : base(PluginsGroupSettingsValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static PluginsGroupSettings Clone(IPluginsGroupSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginsGroupSettings vm = new PluginsGroupSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsGroupParam1 = from.IsGroupParam1; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginsGroupSettings to, IPluginsGroupSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.IsGroupParam1 = from.IsGroupParam1; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginsGroupSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginsGroupSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginsGroupSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginsGroupSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugins_group_settings' to 'PluginsGroupSettings'
        public static PluginsGroupSettings ConvertToVM(Proto.Plugin.proto_plugins_group_settings m, PluginsGroupSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsGroupParam1 = m.IsGroupParam1; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginsGroupSettings' to 'proto_plugins_group_settings'
        public static Proto.Plugin.proto_plugins_group_settings ConvertToProto(PluginsGroupSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Plugin.proto_plugins_group_settings m = new Proto.Plugin.proto_plugins_group_settings(); // Clone.tt Line: 239
            m.IsGroupParam1 = vm.IsGroupParam1; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public bool IsGroupParam1 // Property.tt Line: 58
        { 
            get { return this._IsGroupParam1; }
            set
            {
                if (this._IsGroupParam1 != value)
                {
                    this.OnIsGroupParam1Changing(ref value);
                    this._IsGroupParam1 = value;
                    this.OnIsGroupParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsGroupParam1;
        partial void OnIsGroupParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsGroupParam1Changed();
        #endregion Properties
    }
    public partial class GeneratorDbSchemaSettingsValidator : ValidatorBase<GeneratorDbSchemaSettings, GeneratorDbSchemaSettingsValidator> { } // Class.tt Line: 6
    public partial class GeneratorDbSchemaSettings : VmValidatableWithSeverity<GeneratorDbSchemaSettings, GeneratorDbSchemaSettingsValidator>, IGeneratorDbSchemaSettings // Class.tt Line: 7
    {
        #region CTOR
        public GeneratorDbSchemaSettings() 
            : base(GeneratorDbSchemaSettingsValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbSchemaSettings Clone(IGeneratorDbSchemaSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GeneratorDbSchemaSettings vm = new GeneratorDbSchemaSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsSchemaParam1 = from.IsSchemaParam1; // Clone.tt Line: 65
            vm.IsSchemaParam2 = from.IsSchemaParam2.HasValue ? from.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 58
            vm.SchemaParam3 = from.SchemaParam3; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GeneratorDbSchemaSettings to, IGeneratorDbSchemaSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.IsSchemaParam1 = from.IsSchemaParam1; // Clone.tt Line: 141
            to.IsSchemaParam2 = from.IsSchemaParam2.HasValue ? from.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 136
            to.SchemaParam3 = from.SchemaParam3; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
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
        public static GeneratorDbSchemaSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_settings m, GeneratorDbSchemaSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsSchemaParam1 = m.IsSchemaParam1; // Clone.tt Line: 221
            vm.IsSchemaParam2 = m.IsSchemaParam2.HasValue ? (bool?)m.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 221
            vm.SchemaParam3 = m.SchemaParam3; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaSettings' to 'proto_generator_db_schema_settings'
        public static Proto.Plugin.proto_generator_db_schema_settings ConvertToProto(GeneratorDbSchemaSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Plugin.proto_generator_db_schema_settings m = new Proto.Plugin.proto_generator_db_schema_settings(); // Clone.tt Line: 239
            m.IsSchemaParam1 = vm.IsSchemaParam1; // Clone.tt Line: 276
            m.IsSchemaParam2 = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 253
            m.IsSchemaParam2.HasValue = vm.IsSchemaParam2.HasValue;
            if (vm.IsSchemaParam2.HasValue)
                m.IsSchemaParam2.Value = vm.IsSchemaParam2.Value;
            m.SchemaParam3 = vm.SchemaParam3; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public bool IsSchemaParam1 // Property.tt Line: 58
        { 
            get { return this._IsSchemaParam1; }
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
        partial void OnIsSchemaParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsSchemaParam1Changed();
        
        public bool? IsSchemaParam2 // Property.tt Line: 58
        { 
            get { return this._IsSchemaParam2; }
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
        partial void OnIsSchemaParam2Changing(ref bool? to); // Property.tt Line: 82
        partial void OnIsSchemaParam2Changed();
        //Ibool? IGeneratorDbSchemaSettings.IsSchemaParam2 { get { return this._IsSchemaParam2; } }
        
        public string SchemaParam3 // Property.tt Line: 58
        { 
            get { return this._SchemaParam3; }
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
        partial void OnSchemaParam3Changing(ref string to); // Property.tt Line: 82
        partial void OnSchemaParam3Changed();
        #endregion Properties
    }
    public partial class GeneratorDbSchemaNodeSettingsValidator : ValidatorBase<GeneratorDbSchemaNodeSettings, GeneratorDbSchemaNodeSettingsValidator> { } // Class.tt Line: 6
    public partial class GeneratorDbSchemaNodeSettings : VmValidatableWithSeverity<GeneratorDbSchemaNodeSettings, GeneratorDbSchemaNodeSettingsValidator>, IGeneratorDbSchemaNodeSettings // Class.tt Line: 7
    {
        #region CTOR
        public GeneratorDbSchemaNodeSettings() 
            : base(GeneratorDbSchemaNodeSettingsValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbSchemaNodeSettings Clone(IGeneratorDbSchemaNodeSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GeneratorDbSchemaNodeSettings vm = new GeneratorDbSchemaNodeSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsParam1 = from.IsParam1; // Clone.tt Line: 65
            vm.IsIncluded = from.IsIncluded.HasValue ? from.IsIncluded.Value : (bool?)null; // Clone.tt Line: 58
            vm.IsConstantParam1 = from.IsConstantParam1; // Clone.tt Line: 65
            vm.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GeneratorDbSchemaNodeSettings to, IGeneratorDbSchemaNodeSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.IsParam1 = from.IsParam1; // Clone.tt Line: 141
            to.IsIncluded = from.IsIncluded.HasValue ? from.IsIncluded.Value : (bool?)null; // Clone.tt Line: 136
            to.IsConstantParam1 = from.IsConstantParam1; // Clone.tt Line: 141
            to.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GeneratorDbSchemaNodeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GeneratorDbSchemaNodeSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbSchemaNodeSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbSchemaNodeSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_schema_node_settings' to 'GeneratorDbSchemaNodeSettings'
        public static GeneratorDbSchemaNodeSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_node_settings m, GeneratorDbSchemaNodeSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsParam1 = m.IsParam1; // Clone.tt Line: 221
            vm.IsIncluded = m.IsIncluded.HasValue ? (bool?)m.IsIncluded.Value : (bool?)null; // Clone.tt Line: 221
            vm.IsConstantParam1 = m.IsConstantParam1; // Clone.tt Line: 221
            vm.IsCatalogFormParam1 = m.IsCatalogFormParam1; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaNodeSettings' to 'proto_generator_db_schema_node_settings'
        public static Proto.Plugin.proto_generator_db_schema_node_settings ConvertToProto(GeneratorDbSchemaNodeSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Plugin.proto_generator_db_schema_node_settings m = new Proto.Plugin.proto_generator_db_schema_node_settings(); // Clone.tt Line: 239
            m.IsParam1 = vm.IsParam1; // Clone.tt Line: 276
            m.IsIncluded = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 253
            m.IsIncluded.HasValue = vm.IsIncluded.HasValue;
            if (vm.IsIncluded.HasValue)
                m.IsIncluded.Value = vm.IsIncluded.Value;
            m.IsConstantParam1 = vm.IsConstantParam1; // Clone.tt Line: 276
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public bool IsParam1 // Property.tt Line: 58
        { 
            get { return this._IsParam1; }
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
        partial void OnIsParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsParam1Changed();
        
        public bool? IsIncluded // Property.tt Line: 58
        { 
            get { return this._IsIncluded; }
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
        partial void OnIsIncludedChanging(ref bool? to); // Property.tt Line: 82
        partial void OnIsIncludedChanged();
        //Ibool? IGeneratorDbSchemaNodeSettings.IsIncluded { get { return this._IsIncluded; } }
        
        public bool IsConstantParam1 // Property.tt Line: 58
        { 
            get { return this._IsConstantParam1; }
            set
            {
                if (this._IsConstantParam1 != value)
                {
                    this.OnIsConstantParam1Changing(ref value);
                    this._IsConstantParam1 = value;
                    this.OnIsConstantParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsConstantParam1;
        partial void OnIsConstantParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsConstantParam1Changed();
        
        public bool IsCatalogFormParam1 // Property.tt Line: 58
        { 
            get { return this._IsCatalogFormParam1; }
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
        partial void OnIsCatalogFormParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsCatalogFormParam1Changed();
        #endregion Properties
    }
    public partial class GeneratorDbAccessSettingsValidator : ValidatorBase<GeneratorDbAccessSettings, GeneratorDbAccessSettingsValidator> { } // Class.tt Line: 6
    public partial class GeneratorDbAccessSettings : VmValidatableWithSeverity<GeneratorDbAccessSettings, GeneratorDbAccessSettingsValidator>, IGeneratorDbAccessSettings // Class.tt Line: 7
    {
        #region CTOR
        public GeneratorDbAccessSettings() 
            : base(GeneratorDbAccessSettingsValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessSettings Clone(IGeneratorDbAccessSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GeneratorDbAccessSettings vm = new GeneratorDbAccessSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 65
            vm.IsAccessParam2 = from.IsAccessParam2.HasValue ? from.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 58
            vm.AccessParam3 = from.AccessParam3; // Clone.tt Line: 65
            vm.AccessParam4 = from.AccessParam4; // Clone.tt Line: 56
            vm.IsGenerateNotValidCode = from.IsGenerateNotValidCode; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings to, IGeneratorDbAccessSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 141
            to.IsAccessParam2 = from.IsAccessParam2.HasValue ? from.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 136
            to.AccessParam3 = from.AccessParam3; // Clone.tt Line: 141
            to.AccessParam4 = from.AccessParam4; // Clone.tt Line: 134
            to.IsGenerateNotValidCode = from.IsGenerateNotValidCode; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
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
        public static GeneratorDbAccessSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_settings m, GeneratorDbAccessSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsAccessParam1 = m.IsAccessParam1; // Clone.tt Line: 221
            vm.IsAccessParam2 = m.IsAccessParam2.HasValue ? (bool?)m.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 221
            vm.AccessParam3 = m.AccessParam3; // Clone.tt Line: 221
            vm.AccessParam4 = m.AccessParam4.HasValue ? (string)m.AccessParam4.Value : (string)null; // Clone.tt Line: 221
            vm.IsGenerateNotValidCode = m.IsGenerateNotValidCode; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings' to 'proto_generator_db_access_settings'
        public static Proto.Plugin.proto_generator_db_access_settings ConvertToProto(GeneratorDbAccessSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Plugin.proto_generator_db_access_settings m = new Proto.Plugin.proto_generator_db_access_settings(); // Clone.tt Line: 239
            m.IsAccessParam1 = vm.IsAccessParam1; // Clone.tt Line: 276
            m.IsAccessParam2 = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 253
            m.IsAccessParam2.HasValue = vm.IsAccessParam2.HasValue;
            if (vm.IsAccessParam2.HasValue)
                m.IsAccessParam2.Value = vm.IsAccessParam2.Value;
            m.AccessParam3 = vm.AccessParam3; // Clone.tt Line: 276
            m.AccessParam4 = new Proto.Plugin.string_nullable(); // Clone.tt Line: 249
            m.AccessParam4.Value = string.IsNullOrEmpty(vm.AccessParam4) ? "" : vm.AccessParam4;
            m.AccessParam4.HasValue = !string.IsNullOrEmpty(vm.AccessParam4);
            m.IsGenerateNotValidCode = vm.IsGenerateNotValidCode; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public bool IsAccessParam1 // Property.tt Line: 58
        { 
            get { return this._IsAccessParam1; }
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
        partial void OnIsAccessParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsAccessParam1Changed();
        
        public bool? IsAccessParam2 // Property.tt Line: 58
        { 
            get { return this._IsAccessParam2; }
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
        partial void OnIsAccessParam2Changing(ref bool? to); // Property.tt Line: 82
        partial void OnIsAccessParam2Changed();
        //Ibool? IGeneratorDbAccessSettings.IsAccessParam2 { get { return this._IsAccessParam2; } }
        
        public string AccessParam3 // Property.tt Line: 58
        { 
            get { return this._AccessParam3; }
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
        partial void OnAccessParam3Changing(ref string to); // Property.tt Line: 82
        partial void OnAccessParam3Changed();
        
        public string AccessParam4 // Property.tt Line: 58
        { 
            get { return this._AccessParam4; }
            set
            {
                if (this._AccessParam4 != value)
                {
                    this.OnAccessParam4Changing(ref value);
                    this._AccessParam4 = value;
                    this.OnAccessParam4Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _AccessParam4;
        partial void OnAccessParam4Changing(ref string to); // Property.tt Line: 82
        partial void OnAccessParam4Changed();
        //Istring IGeneratorDbAccessSettings.AccessParam4 { get { return this._AccessParam4; } }
        
        public bool IsGenerateNotValidCode // Property.tt Line: 58
        { 
            get { return this._IsGenerateNotValidCode; }
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
        partial void OnIsGenerateNotValidCodeChanging(ref bool to); // Property.tt Line: 82
        partial void OnIsGenerateNotValidCodeChanged();
        #endregion Properties
    }
    public partial class GeneratorDbAccessNodeSettingsValidator : ValidatorBase<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettingsValidator> { } // Class.tt Line: 6
    public partial class GeneratorDbAccessNodeSettings : VmValidatableWithSeverity<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettingsValidator>, IGeneratorDbAccessNodeSettings // Class.tt Line: 7
    {
        #region CTOR
        public GeneratorDbAccessNodeSettings() 
            : base(GeneratorDbAccessNodeSettingsValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessNodeSettings Clone(IGeneratorDbAccessNodeSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GeneratorDbAccessNodeSettings vm = new GeneratorDbAccessNodeSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsParam1 = from.IsParam1; // Clone.tt Line: 65
            vm.IsIncluded = from.IsIncluded.HasValue ? from.IsIncluded.Value : (bool?)null; // Clone.tt Line: 58
            vm.IsPropertyParam1 = from.IsPropertyParam1; // Clone.tt Line: 65
            vm.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GeneratorDbAccessNodeSettings to, IGeneratorDbAccessNodeSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.IsParam1 = from.IsParam1; // Clone.tt Line: 141
            to.IsIncluded = from.IsIncluded.HasValue ? from.IsIncluded.Value : (bool?)null; // Clone.tt Line: 136
            to.IsPropertyParam1 = from.IsPropertyParam1; // Clone.tt Line: 141
            to.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
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
        public static GeneratorDbAccessNodeSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_settings m, GeneratorDbAccessNodeSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.IsParam1 = m.IsParam1; // Clone.tt Line: 221
            vm.IsIncluded = m.IsIncluded.HasValue ? (bool?)m.IsIncluded.Value : (bool?)null; // Clone.tt Line: 221
            vm.IsPropertyParam1 = m.IsPropertyParam1; // Clone.tt Line: 221
            vm.IsCatalogFormParam1 = m.IsCatalogFormParam1; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodeSettings' to 'proto_generator_db_access_node_settings'
        public static Proto.Plugin.proto_generator_db_access_node_settings ConvertToProto(GeneratorDbAccessNodeSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Plugin.proto_generator_db_access_node_settings m = new Proto.Plugin.proto_generator_db_access_node_settings(); // Clone.tt Line: 239
            m.IsParam1 = vm.IsParam1; // Clone.tt Line: 276
            m.IsIncluded = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 253
            m.IsIncluded.HasValue = vm.IsIncluded.HasValue;
            if (vm.IsIncluded.HasValue)
                m.IsIncluded.Value = vm.IsIncluded.Value;
            m.IsPropertyParam1 = vm.IsPropertyParam1; // Clone.tt Line: 276
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public bool IsParam1 // Property.tt Line: 58
        { 
            get { return this._IsParam1; }
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
        partial void OnIsParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsParam1Changed();
        
        public bool? IsIncluded // Property.tt Line: 58
        { 
            get { return this._IsIncluded; }
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
        partial void OnIsIncludedChanging(ref bool? to); // Property.tt Line: 82
        partial void OnIsIncludedChanged();
        //Ibool? IGeneratorDbAccessNodeSettings.IsIncluded { get { return this._IsIncluded; } }
        
        public bool IsPropertyParam1 // Property.tt Line: 58
        { 
            get { return this._IsPropertyParam1; }
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
        partial void OnIsPropertyParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsPropertyParam1Changed();
        
        public bool IsCatalogFormParam1 // Property.tt Line: 58
        { 
            get { return this._IsCatalogFormParam1; }
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
        partial void OnIsCatalogFormParam1Changing(ref bool to); // Property.tt Line: 82
        partial void OnIsCatalogFormParam1Changed();
        #endregion Properties
    }
    
    public interface IVisitorProto // IVisitorProto.tt Line: 7
    {
        void Visit(Proto.Plugin.proto_db_connection_string_settings p);
        void Visit(Proto.Plugin.proto_plugins_group_settings p);
        void Visit(Proto.Plugin.proto_generator_db_schema_settings p);
        void Visit(Proto.Plugin.proto_generator_db_schema_node_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_node_settings p);
    }
    
    public partial class ValidationPluginSampleVisitor : PluginSampleVisitor // ValidationVisitor.tt Line: 7
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(DbConnectionStringSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(DbConnectionStringSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginsGroupSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginsGroupSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbSchemaSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbSchemaSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbSchemaNodeSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbSchemaNodeSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbAccessSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbAccessSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GeneratorDbAccessNodeSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorDbAccessNodeSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
    }
    
    public partial class PluginSampleVisitor : IVisitorPluginSampleNode // NodeVisitor.tt Line: 7
    {
        public CancellationToken Token { get { return _cancellationToken; } }
        protected CancellationToken _cancellationToken;
    
        public void Visit(DbConnectionStringSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(DbConnectionStringSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(DbConnectionStringSettings p) { }
        protected virtual void OnVisitEnd(DbConnectionStringSettings p) { }
        public void Visit(PluginsGroupSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginsGroupSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginsGroupSettings p) { }
        protected virtual void OnVisitEnd(PluginsGroupSettings p) { }
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
        public void Visit(GeneratorDbSchemaNodeSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorDbSchemaNodeSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorDbSchemaNodeSettings p) { }
        protected virtual void OnVisitEnd(GeneratorDbSchemaNodeSettings p) { }
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
    }
    
    public interface IVisitorPluginSampleNode // IVisitorConfigNode.tt Line: 7
    {
        System.Threading.CancellationToken Token { get; }
    }
}