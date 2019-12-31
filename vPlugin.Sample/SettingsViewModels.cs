// Auto generated on UTC 12/30/2019 13:18:00
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
    public partial class GeneratorDbSchemaSettings : ViewModelValidatableWithSeverity<GeneratorDbSchemaSettings, GeneratorDbSchemaSettings.GeneratorDbSchemaSettingsValidator>, IGeneratorDbSchemaSettings // Class.tt Line: 6
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
            vm.IsSchemaParam1 = from.IsSchemaParam1; // Clone.tt Line: 62
            vm.IsSchemaParam2 = from.IsSchemaParam2.HasValue ? from.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 55
            vm.SchemaParam3 = from.SchemaParam3; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(GeneratorDbSchemaSettings to, GeneratorDbSchemaSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.IsSchemaParam1 = from.IsSchemaParam1; // Clone.tt Line: 134
            to.IsSchemaParam2 = from.IsSchemaParam2.HasValue ? from.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 129
            to.SchemaParam3 = from.SchemaParam3; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static GeneratorDbSchemaSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_settings m, GeneratorDbSchemaSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsSchemaParam1 = m.IsSchemaParam1; // Clone.tt Line: 211
            vm.IsSchemaParam2 = m.IsSchemaParam2.HasValue ? (bool?)m.IsSchemaParam2.Value : (bool?)null; // Clone.tt Line: 211
            vm.SchemaParam3 = m.SchemaParam3; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaSettings' to 'proto_generator_db_schema_settings'
        public static Proto.Plugin.proto_generator_db_schema_settings ConvertToProto(GeneratorDbSchemaSettings vm) // Clone.tt Line: 222
        {
            Proto.Plugin.proto_generator_db_schema_settings m = new Proto.Plugin.proto_generator_db_schema_settings(); // Clone.tt Line: 224
            m.IsSchemaParam1 = vm.IsSchemaParam1; // Clone.tt Line: 261
            m.IsSchemaParam2 = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 238
            m.IsSchemaParam2.HasValue = vm.IsSchemaParam2.HasValue;
            if (vm.IsSchemaParam2.HasValue)
                m.IsSchemaParam2.Value = vm.IsSchemaParam2.Value;
            m.SchemaParam3 = vm.SchemaParam3; // Clone.tt Line: 261
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsSchemaParam1 // Property.tt Line: 132
        { 
            get 
            { 
                return this._IsSchemaParam1; 
            }
            set
            {
                if (this._IsSchemaParam1 != value)
                {
                    this.OnIsSchemaParam1Changing(this._IsSchemaParam1, value);
                    this._IsSchemaParam1 = value;
                    this.OnIsSchemaParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsSchemaParam1;
        partial void OnIsSchemaParam1Changing(bool from, bool to); // Property.tt Line: 153
        partial void OnIsSchemaParam1Changed();
        bool IGeneratorDbSchemaSettings.IsSchemaParam1 { get { return this._IsSchemaParam1; } }
        
        public bool? IsSchemaParam2 // Property.tt Line: 132
        { 
            get 
            { 
                return this._IsSchemaParam2; 
            }
            set
            {
                if (this._IsSchemaParam2 != value)
                {
                    this.OnIsSchemaParam2Changing(this._IsSchemaParam2, value);
                    this._IsSchemaParam2 = value;
                    this.OnIsSchemaParam2Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool? _IsSchemaParam2;
        partial void OnIsSchemaParam2Changing(bool? from, bool? to); // Property.tt Line: 153
        partial void OnIsSchemaParam2Changed();
        bool? IGeneratorDbSchemaSettings.IsSchemaParam2 { get { return this._IsSchemaParam2; } }
        
        public string SchemaParam3 // Property.tt Line: 132
        { 
            get 
            { 
                return this._SchemaParam3; 
            }
            set
            {
                if (this._SchemaParam3 != value)
                {
                    this.OnSchemaParam3Changing(this._SchemaParam3, value);
                    this._SchemaParam3 = value;
                    this.OnSchemaParam3Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _SchemaParam3 = string.Empty;
        partial void OnSchemaParam3Changing(string from, string to); // Property.tt Line: 153
        partial void OnSchemaParam3Changed();
        string IGeneratorDbSchemaSettings.SchemaParam3 { get { return this._SchemaParam3; } }
    
        #endregion Properties
    }
    public partial class GeneratorDbAccessSettings : ViewModelValidatableWithSeverity<GeneratorDbAccessSettings, GeneratorDbAccessSettings.GeneratorDbAccessSettingsValidator>, IGeneratorDbAccessSettings // Class.tt Line: 6
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
            vm.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 62
            vm.IsAccessParam2 = from.IsAccessParam2.HasValue ? from.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 55
            vm.AccessParam3 = from.AccessParam3; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings to, GeneratorDbAccessSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 134
            to.IsAccessParam2 = from.IsAccessParam2.HasValue ? from.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 129
            to.AccessParam3 = from.AccessParam3; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static GeneratorDbAccessSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_settings m, GeneratorDbAccessSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsAccessParam1 = m.IsAccessParam1; // Clone.tt Line: 211
            vm.IsAccessParam2 = m.IsAccessParam2.HasValue ? (bool?)m.IsAccessParam2.Value : (bool?)null; // Clone.tt Line: 211
            vm.AccessParam3 = m.AccessParam3; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings' to 'proto_generator_db_access_settings'
        public static Proto.Plugin.proto_generator_db_access_settings ConvertToProto(GeneratorDbAccessSettings vm) // Clone.tt Line: 222
        {
            Proto.Plugin.proto_generator_db_access_settings m = new Proto.Plugin.proto_generator_db_access_settings(); // Clone.tt Line: 224
            m.IsAccessParam1 = vm.IsAccessParam1; // Clone.tt Line: 261
            m.IsAccessParam2 = new Proto.Plugin.bool_nullable(); // Clone.tt Line: 238
            m.IsAccessParam2.HasValue = vm.IsAccessParam2.HasValue;
            if (vm.IsAccessParam2.HasValue)
                m.IsAccessParam2.Value = vm.IsAccessParam2.Value;
            m.AccessParam3 = vm.AccessParam3; // Clone.tt Line: 261
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsAccessParam1 // Property.tt Line: 132
        { 
            get 
            { 
                return this._IsAccessParam1; 
            }
            set
            {
                if (this._IsAccessParam1 != value)
                {
                    this.OnIsAccessParam1Changing(this._IsAccessParam1, value);
                    this._IsAccessParam1 = value;
                    this.OnIsAccessParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsAccessParam1;
        partial void OnIsAccessParam1Changing(bool from, bool to); // Property.tt Line: 153
        partial void OnIsAccessParam1Changed();
        bool IGeneratorDbAccessSettings.IsAccessParam1 { get { return this._IsAccessParam1; } }
        
        public bool? IsAccessParam2 // Property.tt Line: 132
        { 
            get 
            { 
                return this._IsAccessParam2; 
            }
            set
            {
                if (this._IsAccessParam2 != value)
                {
                    this.OnIsAccessParam2Changing(this._IsAccessParam2, value);
                    this._IsAccessParam2 = value;
                    this.OnIsAccessParam2Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool? _IsAccessParam2;
        partial void OnIsAccessParam2Changing(bool? from, bool? to); // Property.tt Line: 153
        partial void OnIsAccessParam2Changed();
        bool? IGeneratorDbAccessSettings.IsAccessParam2 { get { return this._IsAccessParam2; } }
        
        public string AccessParam3 // Property.tt Line: 132
        { 
            get 
            { 
                return this._AccessParam3; 
            }
            set
            {
                if (this._AccessParam3 != value)
                {
                    this.OnAccessParam3Changing(this._AccessParam3, value);
                    this._AccessParam3 = value;
                    this.OnAccessParam3Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _AccessParam3 = string.Empty;
        partial void OnAccessParam3Changing(string from, string to); // Property.tt Line: 153
        partial void OnAccessParam3Changed();
        string IGeneratorDbAccessSettings.AccessParam3 { get { return this._AccessParam3; } }
    
        #endregion Properties
    }
    public partial class GeneratorDbAccessNodePropertySettings : ViewModelValidatableWithSeverity<GeneratorDbAccessNodePropertySettings, GeneratorDbAccessNodePropertySettings.GeneratorDbAccessNodePropertySettingsValidator>, IGeneratorDbAccessNodePropertySettings // Class.tt Line: 6
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
            vm.IsPropertyParam1 = from.IsPropertyParam1; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(GeneratorDbAccessNodePropertySettings to, GeneratorDbAccessNodePropertySettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.IsPropertyParam1 = from.IsPropertyParam1; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static GeneratorDbAccessNodePropertySettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_property_settings m, GeneratorDbAccessNodePropertySettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsPropertyParam1 = m.IsPropertyParam1; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodePropertySettings' to 'proto_generator_db_access_node_property_settings'
        public static Proto.Plugin.proto_generator_db_access_node_property_settings ConvertToProto(GeneratorDbAccessNodePropertySettings vm) // Clone.tt Line: 222
        {
            Proto.Plugin.proto_generator_db_access_node_property_settings m = new Proto.Plugin.proto_generator_db_access_node_property_settings(); // Clone.tt Line: 224
            m.IsPropertyParam1 = vm.IsPropertyParam1; // Clone.tt Line: 261
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsPropertyParam1 // Property.tt Line: 132
        { 
            get 
            { 
                return this._IsPropertyParam1; 
            }
            set
            {
                if (this._IsPropertyParam1 != value)
                {
                    this.OnIsPropertyParam1Changing(this._IsPropertyParam1, value);
                    this._IsPropertyParam1 = value;
                    this.OnIsPropertyParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsPropertyParam1;
        partial void OnIsPropertyParam1Changing(bool from, bool to); // Property.tt Line: 153
        partial void OnIsPropertyParam1Changed();
        bool IGeneratorDbAccessNodePropertySettings.IsPropertyParam1 { get { return this._IsPropertyParam1; } }
    
        #endregion Properties
    }
    public partial class GeneratorDbAccessNodeCatalogFormSettings : ViewModelValidatableWithSeverity<GeneratorDbAccessNodeCatalogFormSettings, GeneratorDbAccessNodeCatalogFormSettings.GeneratorDbAccessNodeCatalogFormSettingsValidator>, IGeneratorDbAccessNodeCatalogFormSettings // Class.tt Line: 6
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
            vm.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(GeneratorDbAccessNodeCatalogFormSettings to, GeneratorDbAccessNodeCatalogFormSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.IsCatalogFormParam1 = from.IsCatalogFormParam1; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static GeneratorDbAccessNodeCatalogFormSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_catalog_form_settings m, GeneratorDbAccessNodeCatalogFormSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsCatalogFormParam1 = m.IsCatalogFormParam1; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodeCatalogFormSettings' to 'proto_generator_db_access_node_catalog_form_settings'
        public static Proto.Plugin.proto_generator_db_access_node_catalog_form_settings ConvertToProto(GeneratorDbAccessNodeCatalogFormSettings vm) // Clone.tt Line: 222
        {
            Proto.Plugin.proto_generator_db_access_node_catalog_form_settings m = new Proto.Plugin.proto_generator_db_access_node_catalog_form_settings(); // Clone.tt Line: 224
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // Clone.tt Line: 261
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public bool IsCatalogFormParam1 // Property.tt Line: 132
        { 
            get 
            { 
                return this._IsCatalogFormParam1; 
            }
            set
            {
                if (this._IsCatalogFormParam1 != value)
                {
                    this.OnIsCatalogFormParam1Changing(this._IsCatalogFormParam1, value);
                    this._IsCatalogFormParam1 = value;
                    this.OnIsCatalogFormParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsCatalogFormParam1;
        partial void OnIsCatalogFormParam1Changing(bool from, bool to); // Property.tt Line: 153
        partial void OnIsCatalogFormParam1Changed();
        bool IGeneratorDbAccessNodeCatalogFormSettings.IsCatalogFormParam1 { get { return this._IsCatalogFormParam1; } }
    
        #endregion Properties
    }
    
    public interface IVisitorProto // IVisitorProto.tt Line: 7
    {
        void Visit(Proto.Plugin.proto_generator_db_schema_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_settings p);
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