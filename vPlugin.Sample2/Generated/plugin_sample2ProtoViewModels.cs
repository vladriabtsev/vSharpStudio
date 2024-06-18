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

namespace vPlugin.Sample2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\NameSpace.tt Line:24
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IPluginSampleAcceptVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\NameSpace.tt Line:30
    {
        void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor);
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class DbConnectionStringSettings2Validator : ValidatorBase<DbConnectionStringSettings2, DbConnectionStringSettings2Validator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:16
    public partial class DbConnectionStringSettings2 : BaseSettings<DbConnectionStringSettings2, DbConnectionStringSettings2Validator>, IDbConnectionStringSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:17
    {
        public override string ToDebugString()
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes);
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR
        public DbConnectionStringSettings2(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:50
            : base(parent, DbConnectionStringSettings2Validator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static DbConnectionStringSettings2 Clone(ITreeConfigNode? parent, IDbConnectionStringSettings2 from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            DbConnectionStringSettings2 vm = new DbConnectionStringSettings2(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:36
            vm._StringSettings = from.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:69
            return vm;
        }
        public static void Update(DbConnectionStringSettings2 to, IDbConnectionStringSettings2 from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:79
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._StringSettings = from.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:143
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:149
        #region IEditable
        public override DbConnectionStringSettings2 Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return DbConnectionStringSettings2.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:157
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
        public static DbConnectionStringSettings2 ConvertToVM(Proto.Plugin.proto_db_connection_string_settings2 m, DbConnectionStringSettings2 vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:173
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._StringSettings = m.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:219
            return vm;
        }
        // Conversion from 'DbConnectionStringSettings2' to 'proto_db_connection_string_settings2'
        public static Proto.Plugin.proto_db_connection_string_settings2 ConvertToProto(DbConnectionStringSettings2 vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:232
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_db_connection_string_settings2 m = new Proto.Plugin.proto_db_connection_string_settings2(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:235
            m.StringSettings = vm.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:272
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public string StringSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:8
        { 
            get { return this._StringSettings; }
            set
            {
                // Use 'OnStringSettingsChanging' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._StringSettings, value, (t) => { bool isCancel = false; this.OnStringSettingsChanging(ref value, ref isCancel); if (isCancel) return; this._StringSettings = value; this.OnStringSettingsChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:15
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:18
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:21
                }
            }
        }
        private string _StringSettings = string.Empty; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:43
        partial void OnStringSettingsChanging(ref string to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:45
        partial void OnStringSettingsChanged();
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbAccessSettings2Validator : ValidatorBase<GeneratorDbAccessSettings2, GeneratorDbAccessSettings2Validator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:16
    public partial class GeneratorDbAccessSettings2 : BaseSettings<GeneratorDbAccessSettings2, GeneratorDbAccessSettings2Validator>, IGeneratorDbAccessSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:17
    {
        public override string ToDebugString()
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes);
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR
        public GeneratorDbAccessSettings2(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:50
            : base(parent, GeneratorDbAccessSettings2Validator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessSettings2 Clone(ITreeConfigNode? parent, IGeneratorDbAccessSettings2 from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            GeneratorDbAccessSettings2 vm = new GeneratorDbAccessSettings2(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:36
            vm._IsAccessParam1 = from.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:69
            vm._IsAccessParam2 = from.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:69
            vm._AccessParam3 = from.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:69
            vm._AccessParam4 = from.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:69
            vm._IsGenerateNotValidCode = from.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:69
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings2 to, IGeneratorDbAccessSettings2 from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:79
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsAccessParam1 = from.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:143
            to._IsAccessParam2 = from.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:143
            to._AccessParam3 = from.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:143
            to._AccessParam4 = from.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:143
            to._IsGenerateNotValidCode = from.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:143
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:149
        #region IEditable
        public override GeneratorDbAccessSettings2 Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbAccessSettings2.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:157
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
        public static GeneratorDbAccessSettings2 ConvertToVM(Proto.Plugin.proto_generator_db_access_settings2 m, GeneratorDbAccessSettings2 vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:173
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsAccessParam1 = m.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:219
            vm._IsAccessParam2 = m.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:219
            vm._AccessParam3 = m.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:219
            vm._AccessParam4 = m.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:219
            vm._IsGenerateNotValidCode = m.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:219
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings2' to 'proto_generator_db_access_settings2'
        public static Proto.Plugin.proto_generator_db_access_settings2 ConvertToProto(GeneratorDbAccessSettings2 vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:232
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_access_settings2 m = new Proto.Plugin.proto_generator_db_access_settings2(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:235
            m.IsAccessParam1 = vm.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:272
            m.IsAccessParam2 = vm.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:272
            m.AccessParam3 = vm.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:272
            m.AccessParam4 = vm.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:272
            m.IsGenerateNotValidCode = vm.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:272
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool IsAccessParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:8
        { 
            get { return this._IsAccessParam1; }
            set
            {
                // Use 'OnIsAccessParam1Changing' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._IsAccessParam1, value, (t) => { bool isCancel = false; this.OnIsAccessParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsAccessParam1 = value; this.OnIsAccessParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:15
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:18
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:21
                }
            }
        }
        private bool _IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:43
        partial void OnIsAccessParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:45
        partial void OnIsAccessParam1Changed();
        
        public bool? IsAccessParam2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:8
        { 
            get { return this._IsAccessParam2; }
            set
            {
                // Use 'OnIsAccessParam2Changing' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._IsAccessParam2, value, (t) => { bool isCancel = false; this.OnIsAccessParam2Changing(ref value, ref isCancel); if (isCancel) return; this._IsAccessParam2 = value; this.OnIsAccessParam2Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:15
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:18
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:21
                }
            }
        }
        private bool? _IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:43
        partial void OnIsAccessParam2Changing(ref bool? to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:45
        partial void OnIsAccessParam2Changed();
        
        public string AccessParam3 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:8
        { 
            get { return this._AccessParam3; }
            set
            {
                // Use 'OnAccessParam3Changing' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._AccessParam3, value, (t) => { bool isCancel = false; this.OnAccessParam3Changing(ref value, ref isCancel); if (isCancel) return; this._AccessParam3 = value; this.OnAccessParam3Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:15
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:18
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:21
                }
            }
        }
        private string _AccessParam3 = string.Empty; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:43
        partial void OnAccessParam3Changing(ref string to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:45
        partial void OnAccessParam3Changed();
        
        public string? AccessParam4 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:8
        { 
            get { return this._AccessParam4; }
            set
            {
                // Use 'OnAccessParam4Changing' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._AccessParam4, value, (t) => { bool isCancel = false; this.OnAccessParam4Changing(ref value, ref isCancel); if (isCancel) return; this._AccessParam4 = value; this.OnAccessParam4Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:15
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:18
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:21
                }
            }
        }
        private string? _AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:43
        partial void OnAccessParam4Changing(ref string? to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:45
        partial void OnAccessParam4Changed();
        
        public bool IsGenerateNotValidCode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:8
        { 
            get { return this._IsGenerateNotValidCode; }
            set
            {
                // Use 'OnIsGenerateNotValidCodeChanging' to change 'value' before setting property. It is a patial method and expected will be implemented not often.
                if (SetProperty(this._IsGenerateNotValidCode, value, (t) => { bool isCancel = false; this.OnIsGenerateNotValidCodeChanging(ref value, ref isCancel); if (isCancel) return; this._IsGenerateNotValidCode = value; this.OnIsGenerateNotValidCodeChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:15
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:18
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:21
                }
            }
        }
        private bool _IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:43
        partial void OnIsGenerateNotValidCodeChanging(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:45
        partial void OnIsGenerateNotValidCodeChanged();
        #endregion Properties
    }
    
    public interface IVisitorProto // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\IVisitorProto.tt Line:8
    {
        void Visit(Proto.Plugin.proto_db_connection_string_settings2 p);
        void Visit(Proto.Plugin.proto_generator_db_access_settings2 p);
    }
    
    public partial class ValidationPluginSampleVisitor : PluginSampleVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:8
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(DbConnectionStringSettings2 p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(DbConnectionStringSettings2 p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbAccessSettings2 p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbAccessSettings2 p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
    }
    
    public partial class PluginSampleVisitor : IVisitorPluginSampleNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\NodeVisitor.tt Line:8
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
    
    public interface IVisitorPluginSampleNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\IVisitorConfigNode.tt Line:8
    {
        System.Threading.CancellationToken Token { get; }
    }
}