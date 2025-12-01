using System;
using System.Linq;
using ViewModelBase;
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
using FluentValidation;
using FluentValidation.Results;

namespace vPlugin.Sample2 
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IPluginSampleAcceptVisitor 
    {
        void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor);
    }
    
    
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class DbConnectionStringSettings2Validator : ValidatorBase<DbConnectionStringSettings2, DbConnectionStringSettings2Validator>  
    {
        private void GeneralRules()
        {
            this.RuleFor(x => x.StringSettings).Custom((str, cntx) =>
            {
                try
                {
                    System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(str));
                }
                catch(Exception ex)
                {
                    cntx.AddFailure(new ValidationFailure("StringSettings", $"Can't convert to UTF8. Error: {ex.Message}") { Severity = Severity.Error });
                }
            });
        }
    }
    public partial class DbConnectionStringSettings2 : BaseSettings<DbConnectionStringSettings2, DbConnectionStringSettings2Validator>, IDbConnectionStringSettings2 
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
        public DbConnectionStringSettings2(ITreeConfigNode? parent) 
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
        
        public static DbConnectionStringSettings2 Clone(ITreeConfigNode? parent, IDbConnectionStringSettings2 from, bool isDeep = true) 
        {
            Debug.Assert(from != null);
            var vm = new DbConnectionStringSettings2(parent); 
            vm._StringSettings = from.StringSettings; 
            return vm;
        }
        public static void Update(DbConnectionStringSettings2 to, IDbConnectionStringSettings2 from, bool isDeep = true) 
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._StringSettings = from.StringSettings; 
        }
        
        #region IEditable
        public override DbConnectionStringSettings2 Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return DbConnectionStringSettings2.Clone(this.Parent, this); 
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
        public static DbConnectionStringSettings2 ConvertToVM(Proto.Plugin.proto_db_connection_string_settings2 m, DbConnectionStringSettings2 vm) 
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._StringSettings = m.StringSettings; 
            return vm;
        }
        // Conversion from 'DbConnectionStringSettings2' to 'proto_db_connection_string_settings2'
        public static Proto.Plugin.proto_db_connection_string_settings2 ConvertToProto(DbConnectionStringSettings2 vm) 
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_db_connection_string_settings2 m = new Proto.Plugin.proto_db_connection_string_settings2(); 
            try 
            { 
                m.StringSettings = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(vm.StringSettings)); 
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error while converting to PROTO and encoding from Default to UTF8. For 'plugin_sample2.proto' message 'proto_db_connection_string_settings2' field 'string_settings'", ex); 
            }
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) 
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
        
        public string StringSettings 
        { 
            get { return this._StringSettings; }
            set
            {
                // Use 'OnStringSettingsChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._StringSettings, value, (t) => { bool isCancel = false; this.OnStringSettingsChanging(ref value, ref isCancel); if (isCancel) return; this._StringSettings = value; this.OnStringSettingsChanged(); })) 
                {
                    this.ValidateProperty(); 
                    this.IsChanged = true; 
                }
            }
        }
        private string _StringSettings = string.Empty; 
        partial void OnStringSettingsChanging(ref string to, ref bool isCancel); 
        partial void OnStringSettingsChanged();
        #endregion Properties
    }
    
    
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbAccessSettings2Validator : ValidatorBase<GeneratorDbAccessSettings2, GeneratorDbAccessSettings2Validator>  
    {
        private void GeneralRules()
        {
            this.RuleFor(x => x.AccessParam3).Custom((str, cntx) =>
            {
                try
                {
                    System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(str));
                }
                catch(Exception ex)
                {
                    cntx.AddFailure(new ValidationFailure("AccessParam3", $"Can't convert to UTF8. Error: {ex.Message}") { Severity = Severity.Error });
                }
            });
        }
    }
    public partial class GeneratorDbAccessSettings2 : BaseSettings<GeneratorDbAccessSettings2, GeneratorDbAccessSettings2Validator>, IGeneratorDbAccessSettings2 
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
        public GeneratorDbAccessSettings2(ITreeConfigNode? parent) 
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
        
        public static GeneratorDbAccessSettings2 Clone(ITreeConfigNode? parent, IGeneratorDbAccessSettings2 from, bool isDeep = true) 
        {
            Debug.Assert(from != null);
            var vm = new GeneratorDbAccessSettings2(parent); 
            vm._IsAccessParam1 = from.IsAccessParam1; 
            vm._IsAccessParam2 = from.IsAccessParam2; 
            vm._AccessParam3 = from.AccessParam3; 
            vm._AccessParam4 = from.AccessParam4; 
            vm._IsGenerateNotValidCode = from.IsGenerateNotValidCode; 
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings2 to, IGeneratorDbAccessSettings2 from, bool isDeep = true) 
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsAccessParam1 = from.IsAccessParam1; 
            to._IsAccessParam2 = from.IsAccessParam2; 
            to._AccessParam3 = from.AccessParam3; 
            to._AccessParam4 = from.AccessParam4; 
            to._IsGenerateNotValidCode = from.IsGenerateNotValidCode; 
        }
        
        #region IEditable
        public override GeneratorDbAccessSettings2 Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbAccessSettings2.Clone(this.Parent, this); 
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
        public static GeneratorDbAccessSettings2 ConvertToVM(Proto.Plugin.proto_generator_db_access_settings2 m, GeneratorDbAccessSettings2 vm) 
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsAccessParam1 = m.IsAccessParam1; 
            vm._IsAccessParam2 = m.IsAccessParam2; 
            vm._AccessParam3 = m.AccessParam3; 
            vm._AccessParam4 = m.AccessParam4; 
            vm._IsGenerateNotValidCode = m.IsGenerateNotValidCode; 
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings2' to 'proto_generator_db_access_settings2'
        public static Proto.Plugin.proto_generator_db_access_settings2 ConvertToProto(GeneratorDbAccessSettings2 vm) 
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_access_settings2 m = new Proto.Plugin.proto_generator_db_access_settings2(); 
            m.IsAccessParam1 = vm.IsAccessParam1; 
            m.IsAccessParam2 = vm.IsAccessParam2; 
            try 
            { 
                m.AccessParam3 = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(vm.AccessParam3)); 
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error while converting to PROTO and encoding from Default to UTF8. For 'plugin_sample2.proto' message 'proto_generator_db_access_settings2' field 'access_param3'", ex); 
            }
            m.AccessParam4 = vm.AccessParam4; 
            m.IsGenerateNotValidCode = vm.IsGenerateNotValidCode; 
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) 
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
        
        public bool IsAccessParam1 
        { 
            get { return this._IsAccessParam1; }
            set
            {
                // Use 'OnIsAccessParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsAccessParam1, value, (t) => { bool isCancel = false; this.OnIsAccessParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsAccessParam1 = value; this.OnIsAccessParam1Changed(); })) 
                {
                    this.ValidateProperty(); 
                    this.IsChanged = true; 
                }
            }
        }
        private bool _IsAccessParam1; 
        partial void OnIsAccessParam1Changing(ref bool to, ref bool isCancel); 
        partial void OnIsAccessParam1Changed();
        
        public bool? IsAccessParam2 
        { 
            get { return this._IsAccessParam2; }
            set
            {
                // Use 'OnIsAccessParam2Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsAccessParam2, value, (t) => { bool isCancel = false; this.OnIsAccessParam2Changing(ref value, ref isCancel); if (isCancel) return; this._IsAccessParam2 = value; this.OnIsAccessParam2Changed(); })) 
                {
                    this.ValidateProperty(); 
                    this.IsChanged = true; 
                }
            }
        }
        private bool? _IsAccessParam2; 
        partial void OnIsAccessParam2Changing(ref bool? to, ref bool isCancel); 
        partial void OnIsAccessParam2Changed();
        
        public string AccessParam3 
        { 
            get { return this._AccessParam3; }
            set
            {
                // Use 'OnAccessParam3Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._AccessParam3, value, (t) => { bool isCancel = false; this.OnAccessParam3Changing(ref value, ref isCancel); if (isCancel) return; this._AccessParam3 = value; this.OnAccessParam3Changed(); })) 
                {
                    this.ValidateProperty(); 
                    this.IsChanged = true; 
                }
            }
        }
        private string _AccessParam3 = string.Empty; 
        partial void OnAccessParam3Changing(ref string to, ref bool isCancel); 
        partial void OnAccessParam3Changed();
        
        public string? AccessParam4 
        { 
            get { return this._AccessParam4; }
            set
            {
                // Use 'OnAccessParam4Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._AccessParam4, value, (t) => { bool isCancel = false; this.OnAccessParam4Changing(ref value, ref isCancel); if (isCancel) return; this._AccessParam4 = value; this.OnAccessParam4Changed(); })) 
                {
                    this.ValidateProperty(); 
                    this.IsChanged = true; 
                }
            }
        }
        private string? _AccessParam4; 
        partial void OnAccessParam4Changing(ref string? to, ref bool isCancel); 
        partial void OnAccessParam4Changed();
        
        public bool IsGenerateNotValidCode 
        { 
            get { return this._IsGenerateNotValidCode; }
            set
            {
                // Use 'OnIsGenerateNotValidCodeChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsGenerateNotValidCode, value, (t) => { bool isCancel = false; this.OnIsGenerateNotValidCodeChanging(ref value, ref isCancel); if (isCancel) return; this._IsGenerateNotValidCode = value; this.OnIsGenerateNotValidCodeChanged(); })) 
                {
                    this.ValidateProperty(); 
                    this.IsChanged = true; 
                }
            }
        }
        private bool _IsGenerateNotValidCode; 
        partial void OnIsGenerateNotValidCodeChanging(ref bool to, ref bool isCancel); 
        partial void OnIsGenerateNotValidCodeChanged();
        #endregion Properties
    }
    
    public interface IVisitorProto 
    {
        void Visit(Proto.Plugin.proto_db_connection_string_settings2 p);
        void Visit(Proto.Plugin.proto_generator_db_access_settings2 p);
    }
    
    public partial class ValidationPluginSampleVisitor : PluginSampleVisitor 
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(DbConnectionStringSettings2 p) 
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(DbConnectionStringSettings2 p) 
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbAccessSettings2 p) 
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbAccessSettings2 p) 
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
    }
    
    public partial class PluginSampleVisitor : IVisitorPluginSampleNode 
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
    
    public interface IVisitorPluginSampleNode 
    {
        System.Threading.CancellationToken Token { get; }
    }
}