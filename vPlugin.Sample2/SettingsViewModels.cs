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
using vSharpStudio.common.ViewModels;
using Google.Protobuf;
using System.Diagnostics;

namespace vPlugin.Sample2 // NameSpace.tt Line: 23
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IPluginSampleAcceptVisitor // NameSpace.tt Line: 29
    {
        void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor);
    }
    // Class.tt Line: 6
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class DbConnectionStringSettings2Validator : ValidatorBase<DbConnectionStringSettings2, DbConnectionStringSettings2Validator> { } // Class.tt Line: 15
    public partial class DbConnectionStringSettings2 : BaseSettings<DbConnectionStringSettings2, DbConnectionStringSettings2Validator>, IDbConnectionStringSettings2 // Class.tt Line: 16
    {
        #region CTOR
        public DbConnectionStringSettings2(ITreeConfigNode? parent) // Class.tt Line: 26
            : base(parent, DbConnectionStringSettings2Validator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnCreating();
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static DbConnectionStringSettings2 Clone(ITreeConfigNode? parent, IDbConnectionStringSettings2 from, bool isDeep = true) // Clone.tt Line: 28
        {
            Debug.Assert(from != null);
            DbConnectionStringSettings2 vm = new DbConnectionStringSettings2(parent); // Clone.tt Line: 35
            vm.IsNotifying = false; // Clone.tt Line: 39
            vm.IsValidate = false;
            vm.StringSettings = from.StringSettings; // Clone.tt Line: 67
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(DbConnectionStringSettings2 to, IDbConnectionStringSettings2 from, bool isDeep = true) // Clone.tt Line: 79
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to.StringSettings = from.StringSettings; // Clone.tt Line: 143
        }
        // Clone.tt Line: 149
        #region IEditable
        public override DbConnectionStringSettings2 Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return DbConnectionStringSettings2.Clone(this.Parent, this); // Clone.tt Line: 157
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(DbConnectionStringSettings2 from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            DbConnectionStringSettings2.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_db_connection_string_settings2' to 'DbConnectionStringSettings2'
        public static DbConnectionStringSettings2 ConvertToVM(Proto.Plugin.proto_db_connection_string_settings2 m, DbConnectionStringSettings2 vm) // Clone.tt Line: 173
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.StringSettings = m.StringSettings; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'DbConnectionStringSettings2' to 'proto_db_connection_string_settings2'
        public static Proto.Plugin.proto_db_connection_string_settings2 ConvertToProto(DbConnectionStringSettings2 vm) // Clone.tt Line: 236
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_db_connection_string_settings2 m = new Proto.Plugin.proto_db_connection_string_settings2(); // Clone.tt Line: 239
            m.StringSettings = vm.StringSettings; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public string StringSettings // Property.tt Line: 55
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
        partial void OnStringSettingsChanging(ref string to); // Property.tt Line: 79
        partial void OnStringSettingsChanged();
        [BrowsableAttribute(false)]
        public override bool IsChanged // Class.tt Line: 109
        { 
            get { return this._IsChanged; }
            set
            {
                if (VmBindable.IsNotifyingStatic && this.IsNotifying)
                {
                    if (this._IsChanged != value)
                    {
                        this.OnIsChangedChanging(ref value);
                        this._IsChanged = value;
                        this.OnIsChangedChanged();
                        this.NotifyPropertyChanged();
                    }
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 126
        //partial void OnIsChangedChanged(); // Class.tt Line: 130
        #endregion Properties
    }
    // Class.tt Line: 6
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbAccessSettings2Validator : ValidatorBase<GeneratorDbAccessSettings2, GeneratorDbAccessSettings2Validator> { } // Class.tt Line: 15
    public partial class GeneratorDbAccessSettings2 : BaseSettings<GeneratorDbAccessSettings2, GeneratorDbAccessSettings2Validator>, IGeneratorDbAccessSettings2 // Class.tt Line: 16
    {
        #region CTOR
        public GeneratorDbAccessSettings2(ITreeConfigNode? parent) // Class.tt Line: 26
            : base(parent, GeneratorDbAccessSettings2Validator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnCreating();
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessSettings2 Clone(ITreeConfigNode? parent, IGeneratorDbAccessSettings2 from, bool isDeep = true) // Clone.tt Line: 28
        {
            Debug.Assert(from != null);
            GeneratorDbAccessSettings2 vm = new GeneratorDbAccessSettings2(parent); // Clone.tt Line: 35
            vm.IsNotifying = false; // Clone.tt Line: 39
            vm.IsValidate = false;
            vm.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 67
            vm.IsAccessParam2 = from.IsAccessParam2; // Clone.tt Line: 67
            vm.AccessParam3 = from.AccessParam3; // Clone.tt Line: 67
            vm.AccessParam4 = from.AccessParam4; // Clone.tt Line: 67
            vm.IsGenerateNotValidCode = from.IsGenerateNotValidCode; // Clone.tt Line: 67
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings2 to, IGeneratorDbAccessSettings2 from, bool isDeep = true) // Clone.tt Line: 79
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to.IsAccessParam1 = from.IsAccessParam1; // Clone.tt Line: 143
            to.IsAccessParam2 = from.IsAccessParam2; // Clone.tt Line: 143
            to.AccessParam3 = from.AccessParam3; // Clone.tt Line: 143
            to.AccessParam4 = from.AccessParam4; // Clone.tt Line: 143
            to.IsGenerateNotValidCode = from.IsGenerateNotValidCode; // Clone.tt Line: 143
        }
        // Clone.tt Line: 149
        #region IEditable
        public override GeneratorDbAccessSettings2 Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbAccessSettings2.Clone(this.Parent, this); // Clone.tt Line: 157
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbAccessSettings2 from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbAccessSettings2.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_access_settings2' to 'GeneratorDbAccessSettings2'
        public static GeneratorDbAccessSettings2 ConvertToVM(Proto.Plugin.proto_generator_db_access_settings2 m, GeneratorDbAccessSettings2 vm) // Clone.tt Line: 173
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.IsAccessParam1 = m.IsAccessParam1; // Clone.tt Line: 221
            vm.IsAccessParam2 = m.IsAccessParam2; // Clone.tt Line: 221
            vm.AccessParam3 = m.AccessParam3; // Clone.tt Line: 221
            vm.AccessParam4 = m.AccessParam4; // Clone.tt Line: 221
            vm.IsGenerateNotValidCode = m.IsGenerateNotValidCode; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings2' to 'proto_generator_db_access_settings2'
        public static Proto.Plugin.proto_generator_db_access_settings2 ConvertToProto(GeneratorDbAccessSettings2 vm) // Clone.tt Line: 236
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_access_settings2 m = new Proto.Plugin.proto_generator_db_access_settings2(); // Clone.tt Line: 239
            m.IsAccessParam1 = vm.IsAccessParam1; // Clone.tt Line: 276
            m.IsAccessParam2 = vm.IsAccessParam2; // Clone.tt Line: 276
            m.AccessParam3 = vm.AccessParam3; // Clone.tt Line: 276
            m.AccessParam4 = vm.AccessParam4; // Clone.tt Line: 276
            m.IsGenerateNotValidCode = vm.IsGenerateNotValidCode; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public bool IsAccessParam1 // Property.tt Line: 55
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
        partial void OnIsAccessParam1Changing(ref bool to); // Property.tt Line: 79
        partial void OnIsAccessParam1Changed();
        
        public bool? IsAccessParam2 // Property.tt Line: 55
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
        partial void OnIsAccessParam2Changing(ref bool? to); // Property.tt Line: 79
        partial void OnIsAccessParam2Changed();
        
        public string AccessParam3 // Property.tt Line: 55
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
        partial void OnAccessParam3Changing(ref string to); // Property.tt Line: 79
        partial void OnAccessParam3Changed();
        
        public string AccessParam4 // Property.tt Line: 55
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
        partial void OnAccessParam4Changing(ref string to); // Property.tt Line: 79
        partial void OnAccessParam4Changed();
        
        public bool IsGenerateNotValidCode // Property.tt Line: 55
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
        partial void OnIsGenerateNotValidCodeChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsGenerateNotValidCodeChanged();
        [BrowsableAttribute(false)]
        public override bool IsChanged // Class.tt Line: 109
        { 
            get { return this._IsChanged; }
            set
            {
                if (VmBindable.IsNotifyingStatic && this.IsNotifying)
                {
                    if (this._IsChanged != value)
                    {
                        this.OnIsChangedChanging(ref value);
                        this._IsChanged = value;
                        this.OnIsChangedChanged();
                        this.NotifyPropertyChanged();
                    }
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 126
        //partial void OnIsChangedChanged(); // Class.tt Line: 130
        #endregion Properties
    }
    
    public interface IVisitorProto // IVisitorProto.tt Line: 7
    {
        void Visit(Proto.Plugin.proto_db_connection_string_settings2 p);
        void Visit(Proto.Plugin.proto_generator_db_access_settings2 p);
    }
    
    public partial class ValidationPluginSampleVisitor : PluginSampleVisitor // ValidationVisitor.tt Line: 7
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(DbConnectionStringSettings2 p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(DbConnectionStringSettings2 p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbAccessSettings2 p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbAccessSettings2 p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
    }
    
    public partial class PluginSampleVisitor : IVisitorPluginSampleNode // NodeVisitor.tt Line: 7
    {
        public CancellationToken Token { get { return _cancellationToken; } }
        protected CancellationToken _cancellationToken;
    
        public void Visit(DbConnectionStringSettings2 p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(DbConnectionStringSettings2 p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(DbConnectionStringSettings2 p) { }
        protected virtual void OnVisitEnd(DbConnectionStringSettings2 p) { }
        public void Visit(GeneratorDbAccessSettings2 p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorDbAccessSettings2 p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorDbAccessSettings2 p) { }
        protected virtual void OnVisitEnd(GeneratorDbAccessSettings2 p) { }
    }
    
    public interface IVisitorPluginSampleNode // IVisitorConfigNode.tt Line: 7
    {
        System.Threading.CancellationToken Token { get; }
    }
}