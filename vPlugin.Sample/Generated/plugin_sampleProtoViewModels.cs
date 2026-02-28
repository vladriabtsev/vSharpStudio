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

namespace vPlugin.Sample // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\NameSpace.tt Line:26
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IPluginSampleAcceptVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\NameSpace.tt Line:32
    {
        void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor);
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class DbConnectionStringSettingsValidator : ValidatorBase<DbConnectionStringSettings, DbConnectionStringSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
            this.RuleFor(x => x.StringSettings).Custom((str, cntx) => // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:24
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
    ///<summary>
    ///     Represents the caching modes that can be used when creating a new <see cref="SqliteConnection" />.
    /// </summary>
    /// <seealso href="http://sqlite.org/sharedcache.html">SQLite Shared-Cache Mode</seealso>
    public partial class DbConnectionStringSettings : BaseSettings<DbConnectionStringSettings, DbConnectionStringSettingsValidator>, IDbConnectionStringSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public DbConnectionStringSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, DbConnectionStringSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static DbConnectionStringSettings Clone(ITreeConfigNode? parent, IDbConnectionStringSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new DbConnectionStringSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._StringSettings = from.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            return vm;
        }
        public static void Update(DbConnectionStringSettings to, IDbConnectionStringSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._StringSettings = from.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override DbConnectionStringSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return DbConnectionStringSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(DbConnectionStringSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            DbConnectionStringSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_db_connection_string_settings' to 'DbConnectionStringSettings'
        public static DbConnectionStringSettings ConvertToVM(Proto.Plugin.proto_db_connection_string_settings m, DbConnectionStringSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._StringSettings = m.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            return vm;
        }
        // Conversion from 'DbConnectionStringSettings' to 'proto_db_connection_string_settings'
        public static Proto.Plugin.proto_db_connection_string_settings ConvertToProto(DbConnectionStringSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_db_connection_string_settings m = new Proto.Plugin.proto_db_connection_string_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            try // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:298
            { 
                m.StringSettings = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(vm.StringSettings)); 
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error while converting to PROTO and encoding from Default to UTF8. For 'plugin_sample.proto' message 'proto_db_connection_string_settings' field 'string_settings'", ex); 
            }
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        // Represents the caching modes that can be used when creating a new <see cref="SqliteConnection" />.
        public string StringSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._StringSettings; }
            set
            {
                // Use 'OnStringSettingsChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._StringSettings, value, (t) => { bool isCancel = false; this.OnStringSettingsChanging(ref value, ref isCancel); if (isCancel) return; this._StringSettings = value; this.OnStringSettingsChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private string _StringSettings = string.Empty; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnStringSettingsChanging(ref string to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnStringSettingsChanged();
        #endregion Properties
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class PluginsGroupSolutionSubSettingsValidator : ValidatorBase<PluginsGroupSolutionSubSettings, PluginsGroupSolutionSubSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
        }
    }
    public partial class PluginsGroupSolutionSubSettings : BaseSubSettings<PluginsGroupSolutionSubSettings, PluginsGroupSolutionSubSettingsValidator>, IPluginsGroupSolutionSubSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public PluginsGroupSolutionSubSettings(IEditableObjectExt? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, PluginsGroupSolutionSubSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static PluginsGroupSolutionSubSettings Clone(IEditableObjectExt? parent, IPluginsGroupSolutionSubSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new PluginsGroupSolutionSubSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._IsSubParam1 = from.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsSubParam2 = from.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            return vm;
        }
        public static void Update(PluginsGroupSolutionSubSettings to, IPluginsGroupSolutionSubSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsSubParam1 = from.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsSubParam2 = from.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override PluginsGroupSolutionSubSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return PluginsGroupSolutionSubSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginsGroupSolutionSubSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginsGroupSolutionSubSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugins_group_solution_sub_settings' to 'PluginsGroupSolutionSubSettings'
        public static PluginsGroupSolutionSubSettings ConvertToVM(Proto.Plugin.proto_plugins_group_solution_sub_settings m, PluginsGroupSolutionSubSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsSubParam1 = m.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsSubParam2 = m.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            return vm;
        }
        // Conversion from 'PluginsGroupSolutionSubSettings' to 'proto_plugins_group_solution_sub_settings'
        public static Proto.Plugin.proto_plugins_group_solution_sub_settings ConvertToProto(PluginsGroupSolutionSubSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_plugins_group_solution_sub_settings m = new Proto.Plugin.proto_plugins_group_solution_sub_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            m.IsSubParam1 = vm.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsSubParam2 = vm.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        public bool IsSubParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsSubParam1; }
            set
            {
                // Use 'OnIsSubParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsSubParam1, value, (t) => { bool isCancel = false; this.OnIsSubParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsSubParam1 = value; this.OnIsSubParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsSubParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsSubParam1Changed();
        
        public bool IsSubParam2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsSubParam2; }
            set
            {
                // Use 'OnIsSubParam2Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsSubParam2, value, (t) => { bool isCancel = false; this.OnIsSubParam2Changing(ref value, ref isCancel); if (isCancel) return; this._IsSubParam2 = value; this.OnIsSubParam2Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsSubParam2Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsSubParam2Changed();
        #endregion Properties
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class PluginsGroupSolutionSettingsValidator : ValidatorBase<PluginsGroupSolutionSettings, PluginsGroupSolutionSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
        }
    }
    public partial class PluginsGroupSolutionSettings : BaseSettings<PluginsGroupSolutionSettings, PluginsGroupSolutionSettingsValidator>, IPluginsGroupSolutionSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public PluginsGroupSolutionSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, PluginsGroupSolutionSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this._SubSettings = new PluginsGroupSolutionSubSettings(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:82
            this._TestMap = new Dictionary<string, uint>(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:74
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static PluginsGroupSolutionSettings Clone(ITreeConfigNode? parent, IPluginsGroupSolutionSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new PluginsGroupSolutionSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._IsGroupParam1 = from.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            if (isDeep) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:68 IsDefaultBase=False
                vm._SubSettings = vPlugin.Sample.PluginsGroupSolutionSubSettings.Clone(vm, from.SubSettings, isDeep);
            vm._TestMap = new Dictionary<string, uint>(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:55
            foreach (var t in from.TestMap) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:56
                vm._TestMap[t.Key] = t.Value;
            return vm;
        }
        public static void Update(PluginsGroupSolutionSettings to, IPluginsGroupSolutionSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsGroupParam1 = from.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            if (isDeep) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:153
                vPlugin.Sample.PluginsGroupSolutionSubSettings.Update((PluginsGroupSolutionSubSettings)to.SubSettings, from.SubSettings, isDeep);
            to.TestMap.Clear(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:94
            foreach (var tt in from.TestMap)
            {
                to.TestMap.Add(tt.Key, tt.Value);
            }
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override PluginsGroupSolutionSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return PluginsGroupSolutionSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginsGroupSolutionSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginsGroupSolutionSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugins_group_solution_settings' to 'PluginsGroupSolutionSettings'
        public static PluginsGroupSolutionSettings ConvertToVM(Proto.Plugin.proto_plugins_group_solution_settings m, PluginsGroupSolutionSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsGroupParam1 = m.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm.SubSettings ??= new PluginsGroupSolutionSubSettings(vm); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:233
            vPlugin.Sample.PluginsGroupSolutionSubSettings.ConvertToVM(m.SubSettings, (PluginsGroupSolutionSubSettings)vm.SubSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:237
            vm._TestMap = new Dictionary<string, uint>(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:198
            foreach (var t in m.TestMap) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:199
                vm._TestMap[t.Key] = t.Value; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:203
            return vm;
        }
        // Conversion from 'PluginsGroupSolutionSettings' to 'proto_plugins_group_solution_settings'
        public static Proto.Plugin.proto_plugins_group_solution_settings ConvertToProto(PluginsGroupSolutionSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_plugins_group_solution_settings m = new Proto.Plugin.proto_plugins_group_solution_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            m.IsGroupParam1 = vm.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.SubSettings = vPlugin.Sample.PluginsGroupSolutionSubSettings.ConvertToProto((PluginsGroupSolutionSubSettings)vm.SubSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:292
            foreach (var t in vm.TestMap) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:258
                m.TestMap.Add(t.Key, t.Value); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:265
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.SubSettings.AcceptPluginSampleNodeVisitor(visitor); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:33
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        [PropertyOrderAttribute(1)]
        [DisplayName("Param1")]
        [Description("Sample of Param1")]
        public bool IsGroupParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsGroupParam1; }
            set
            {
                // Use 'OnIsGroupParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsGroupParam1, value, (t) => { bool isCancel = false; this.OnIsGroupParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsGroupParam1 = value; this.OnIsGroupParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsGroupParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsGroupParam1Changed();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("Sub Settings")]
        [Description("Sample of Sub Settings")]
        [ExpandableObjectAttribute()]
        public PluginsGroupSolutionSubSettings SubSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._SubSettings; }
            set
            {
                // Use 'OnSubSettingsChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._SubSettings, value, (t) => { bool isCancel = false; this.OnSubSettingsChanging(ref value, ref isCancel); if (isCancel) return; this._SubSettings = value; this.OnSubSettingsChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                }
            }
        }
        private PluginsGroupSolutionSubSettings _SubSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnSubSettingsChanging(ref PluginsGroupSolutionSubSettings to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnSubSettingsChanged();
        IPluginsGroupSolutionSubSettings IPluginsGroupSolutionSettings.SubSettings { get { return (this as PluginsGroupSolutionSettings).SubSettings; } } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:78
        
        public Dictionary<string, uint> TestMap // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._TestMap; }
            set
            {
                // Use 'OnTestMapChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._TestMap, value, (t) => { bool isCancel = false; this.OnTestMapChanging(ref value, ref isCancel); if (isCancel) return; this._TestMap = value; this.OnTestMapChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                }
            }
        }
        private Dictionary<string, uint> _TestMap; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnTestMapChanging(ref Dictionary<string, uint> to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnTestMapChanged();
        IDictionary<string, uint> IPluginsGroupSolutionSettings.TestMap { get { return (this as PluginsGroupSolutionSettings).TestMap; } } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:78
        #endregion Properties
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class PluginsGroupProjectSettingsValidator : ValidatorBase<PluginsGroupProjectSettings, PluginsGroupProjectSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
        }
    }
    public partial class PluginsGroupProjectSettings : BaseSettings<PluginsGroupProjectSettings, PluginsGroupProjectSettingsValidator>, IPluginsGroupProjectSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public PluginsGroupProjectSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, PluginsGroupProjectSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static PluginsGroupProjectSettings Clone(ITreeConfigNode? parent, IPluginsGroupProjectSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new PluginsGroupProjectSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._IsGroupProjectParam1 = from.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            return vm;
        }
        public static void Update(PluginsGroupProjectSettings to, IPluginsGroupProjectSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsGroupProjectParam1 = from.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override PluginsGroupProjectSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return PluginsGroupProjectSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginsGroupProjectSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginsGroupProjectSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugins_group_project_settings' to 'PluginsGroupProjectSettings'
        public static PluginsGroupProjectSettings ConvertToVM(Proto.Plugin.proto_plugins_group_project_settings m, PluginsGroupProjectSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsGroupProjectParam1 = m.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            return vm;
        }
        // Conversion from 'PluginsGroupProjectSettings' to 'proto_plugins_group_project_settings'
        public static Proto.Plugin.proto_plugins_group_project_settings ConvertToProto(PluginsGroupProjectSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_plugins_group_project_settings m = new Proto.Plugin.proto_plugins_group_project_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            m.IsGroupProjectParam1 = vm.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        public bool IsGroupProjectParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsGroupProjectParam1; }
            set
            {
                // Use 'OnIsGroupProjectParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsGroupProjectParam1, value, (t) => { bool isCancel = false; this.OnIsGroupProjectParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsGroupProjectParam1 = value; this.OnIsGroupProjectParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsGroupProjectParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsGroupProjectParam1Changed();
        #endregion Properties
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbSchemaSettingsValidator : ValidatorBase<GeneratorDbSchemaSettings, GeneratorDbSchemaSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
            this.RuleFor(x => x.SchemaParam3).Custom((str, cntx) => // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:24
            {
                try
                {
                    System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(str));
                }
                catch(Exception ex)
                {
                    cntx.AddFailure(new ValidationFailure("SchemaParam3", $"Can't convert to UTF8. Error: {ex.Message}") { Severity = Severity.Error });
                }
            });
        }
    }
    public partial class GeneratorDbSchemaSettings : BaseSettings<GeneratorDbSchemaSettings, GeneratorDbSchemaSettingsValidator>, IGeneratorDbSchemaSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public GeneratorDbSchemaSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, GeneratorDbSchemaSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static GeneratorDbSchemaSettings Clone(ITreeConfigNode? parent, IGeneratorDbSchemaSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new GeneratorDbSchemaSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._IsSchemaParam1 = from.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsSchemaParam2 = from.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._SchemaParam3 = from.SchemaParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            return vm;
        }
        public static void Update(GeneratorDbSchemaSettings to, IGeneratorDbSchemaSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsSchemaParam1 = from.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsSchemaParam2 = from.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._SchemaParam3 = from.SchemaParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override GeneratorDbSchemaSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbSchemaSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbSchemaSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbSchemaSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_schema_settings' to 'GeneratorDbSchemaSettings'
        public static GeneratorDbSchemaSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_settings m, GeneratorDbSchemaSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsSchemaParam1 = m.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsSchemaParam2 = m.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._SchemaParam3 = m.SchemaParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaSettings' to 'proto_generator_db_schema_settings'
        public static Proto.Plugin.proto_generator_db_schema_settings ConvertToProto(GeneratorDbSchemaSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_schema_settings m = new Proto.Plugin.proto_generator_db_schema_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            m.IsSchemaParam1 = vm.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsSchemaParam2 = vm.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            try // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:298
            { 
                m.SchemaParam3 = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(vm.SchemaParam3)); 
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error while converting to PROTO and encoding from Default to UTF8. For 'plugin_sample.proto' message 'proto_generator_db_schema_settings' field 'schema_param3'", ex); 
            }
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        public bool IsSchemaParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsSchemaParam1; }
            set
            {
                // Use 'OnIsSchemaParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsSchemaParam1, value, (t) => { bool isCancel = false; this.OnIsSchemaParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsSchemaParam1 = value; this.OnIsSchemaParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsSchemaParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsSchemaParam1Changed();
        
        public bool? IsSchemaParam2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsSchemaParam2; }
            set
            {
                // Use 'OnIsSchemaParam2Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsSchemaParam2, value, (t) => { bool isCancel = false; this.OnIsSchemaParam2Changing(ref value, ref isCancel); if (isCancel) return; this._IsSchemaParam2 = value; this.OnIsSchemaParam2Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool? _IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsSchemaParam2Changing(ref bool? to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsSchemaParam2Changed();
        
        public string SchemaParam3 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._SchemaParam3; }
            set
            {
                // Use 'OnSchemaParam3Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._SchemaParam3, value, (t) => { bool isCancel = false; this.OnSchemaParam3Changing(ref value, ref isCancel); if (isCancel) return; this._SchemaParam3 = value; this.OnSchemaParam3Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private string _SchemaParam3 = string.Empty; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnSchemaParam3Changing(ref string to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnSchemaParam3Changed();
        #endregion Properties
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbSchemaNodeSettingsValidator : ValidatorBase<GeneratorDbSchemaNodeSettings, GeneratorDbSchemaNodeSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
        }
    }
    public partial class GeneratorDbSchemaNodeSettings : BaseSettings<GeneratorDbSchemaNodeSettings, GeneratorDbSchemaNodeSettingsValidator>, IGeneratorDbSchemaNodeSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public GeneratorDbSchemaNodeSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, GeneratorDbSchemaNodeSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static GeneratorDbSchemaNodeSettings Clone(ITreeConfigNode? parent, IGeneratorDbSchemaNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new GeneratorDbSchemaNodeSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsConstantParam1 = from.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            return vm;
        }
        public static void Update(GeneratorDbSchemaNodeSettings to, IGeneratorDbSchemaNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsConstantParam1 = from.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override GeneratorDbSchemaNodeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbSchemaNodeSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbSchemaNodeSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbSchemaNodeSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_schema_node_settings' to 'GeneratorDbSchemaNodeSettings'
        public static GeneratorDbSchemaNodeSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_node_settings m, GeneratorDbSchemaNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsParam1 = m.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsIncluded = m.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsConstantParam1 = m.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsCatalogFormParam1 = m.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaNodeSettings' to 'proto_generator_db_schema_node_settings'
        public static Proto.Plugin.proto_generator_db_schema_node_settings ConvertToProto(GeneratorDbSchemaNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_schema_node_settings m = new Proto.Plugin.proto_generator_db_schema_node_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            m.IsParam1 = vm.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsIncluded = vm.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsConstantParam1 = vm.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        public bool IsParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsParam1; }
            set
            {
                // Use 'OnIsParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsParam1, value, (t) => { bool isCancel = false; this.OnIsParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsParam1 = value; this.OnIsParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsParam1Changed();
        
        public bool? IsIncluded // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsIncluded; }
            set
            {
                // Use 'OnIsIncludedChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsIncluded, value, (t) => { bool isCancel = false; this.OnIsIncludedChanging(ref value, ref isCancel); if (isCancel) return; this._IsIncluded = value; this.OnIsIncludedChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool? _IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsIncludedChanging(ref bool? to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsIncludedChanged();
        
        public bool IsConstantParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsConstantParam1; }
            set
            {
                // Use 'OnIsConstantParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsConstantParam1, value, (t) => { bool isCancel = false; this.OnIsConstantParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsConstantParam1 = value; this.OnIsConstantParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsConstantParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsConstantParam1Changed();
        
        public bool IsCatalogFormParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsCatalogFormParam1; }
            set
            {
                // Use 'OnIsCatalogFormParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsCatalogFormParam1, value, (t) => { bool isCancel = false; this.OnIsCatalogFormParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsCatalogFormParam1 = value; this.OnIsCatalogFormParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsCatalogFormParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsCatalogFormParam1Changed();
        #endregion Properties
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbAccessSettingsValidator : ValidatorBase<GeneratorDbAccessSettings, GeneratorDbAccessSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
            this.RuleFor(x => x.AccessParam3).Custom((str, cntx) => // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:24
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
    public partial class GeneratorDbAccessSettings : BaseSettings<GeneratorDbAccessSettings, GeneratorDbAccessSettingsValidator>, IGeneratorDbAccessSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public GeneratorDbAccessSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, GeneratorDbAccessSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static GeneratorDbAccessSettings Clone(ITreeConfigNode? parent, IGeneratorDbAccessSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new GeneratorDbAccessSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._IsAccessParam1 = from.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsAccessParam2 = from.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._AccessParam3 = from.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._AccessParam4 = from.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsGenerateNotValidCode = from.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings to, IGeneratorDbAccessSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsAccessParam1 = from.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsAccessParam2 = from.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._AccessParam3 = from.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._AccessParam4 = from.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsGenerateNotValidCode = from.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override GeneratorDbAccessSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbAccessSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbAccessSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbAccessSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_access_settings' to 'GeneratorDbAccessSettings'
        public static GeneratorDbAccessSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_settings m, GeneratorDbAccessSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsAccessParam1 = m.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsAccessParam2 = m.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._AccessParam3 = m.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._AccessParam4 = m.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsGenerateNotValidCode = m.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings' to 'proto_generator_db_access_settings'
        public static Proto.Plugin.proto_generator_db_access_settings ConvertToProto(GeneratorDbAccessSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_access_settings m = new Proto.Plugin.proto_generator_db_access_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            m.IsAccessParam1 = vm.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsAccessParam2 = vm.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            try // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:298
            { 
                m.AccessParam3 = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(vm.AccessParam3)); 
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error while converting to PROTO and encoding from Default to UTF8. For 'plugin_sample.proto' message 'proto_generator_db_access_settings' field 'access_param3'", ex); 
            }
            m.AccessParam4 = vm.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsGenerateNotValidCode = vm.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        public bool IsAccessParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsAccessParam1; }
            set
            {
                // Use 'OnIsAccessParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsAccessParam1, value, (t) => { bool isCancel = false; this.OnIsAccessParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsAccessParam1 = value; this.OnIsAccessParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsAccessParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsAccessParam1Changed();
        
        public bool? IsAccessParam2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsAccessParam2; }
            set
            {
                // Use 'OnIsAccessParam2Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsAccessParam2, value, (t) => { bool isCancel = false; this.OnIsAccessParam2Changing(ref value, ref isCancel); if (isCancel) return; this._IsAccessParam2 = value; this.OnIsAccessParam2Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool? _IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsAccessParam2Changing(ref bool? to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsAccessParam2Changed();
        
        public string AccessParam3 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._AccessParam3; }
            set
            {
                // Use 'OnAccessParam3Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._AccessParam3, value, (t) => { bool isCancel = false; this.OnAccessParam3Changing(ref value, ref isCancel); if (isCancel) return; this._AccessParam3 = value; this.OnAccessParam3Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private string _AccessParam3 = string.Empty; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnAccessParam3Changing(ref string to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnAccessParam3Changed();
        
        public string? AccessParam4 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._AccessParam4; }
            set
            {
                // Use 'OnAccessParam4Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._AccessParam4, value, (t) => { bool isCancel = false; this.OnAccessParam4Changing(ref value, ref isCancel); if (isCancel) return; this._AccessParam4 = value; this.OnAccessParam4Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private string? _AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnAccessParam4Changing(ref string? to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnAccessParam4Changed();
        
        public bool IsGenerateNotValidCode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsGenerateNotValidCode; }
            set
            {
                // Use 'OnIsGenerateNotValidCodeChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsGenerateNotValidCode, value, (t) => { bool isCancel = false; this.OnIsGenerateNotValidCodeChanging(ref value, ref isCancel); if (isCancel) return; this._IsGenerateNotValidCode = value; this.OnIsGenerateNotValidCodeChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsGenerateNotValidCodeChanging(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsGenerateNotValidCodeChanged();
        #endregion Properties
    }
    
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:9
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbAccessNodeSettingsValidator : ValidatorBase<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettingsValidator>  // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:18
    {
        private void GeneralRules() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:20
        {
        }
    }
    public partial class GeneratorDbAccessNodeSettings : BaseSettings<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettingsValidator>, IGeneratorDbAccessNodeSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:39
    {
        public override string ToDebugString() // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:42
        {
            var t = this.GetType();
            var mes = t.Name + ":";
            var p = t.GetProperty("Name");
            if (p != null)
                mes = mes + (string?)p.GetValue(this) + ":";
            OnDebugStringExtend(ref mes); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:59
            return mes + base.ToDebugString();
        }
        partial void OnDebugStringExtend(ref string mes);
        #region CTOR // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:64
        public GeneratorDbAccessNodeSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:66
            : base(parent, GeneratorDbAccessNodeSettingsValidator.Validator)
        {
            //Debug.Assert(/*!VmBindable.isUnitTests*/ this is IDataType || this is IConfig || parent != null);
            this.OnCreating();
            this.OnCreated();
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:125
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        
        public static GeneratorDbAccessNodeSettings Clone(ITreeConfigNode? parent, IGeneratorDbAccessNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:30
        {
            Debug.Assert(from != null);
            var vm = new GeneratorDbAccessNodeSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:37
            vm._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsPropertyParam1 = from.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            vm._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:74
            return vm;
        }
        public static void Update(GeneratorDbAccessNodeSettings to, IGeneratorDbAccessNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:84
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsPropertyParam1 = from.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
            to._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:156
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:162
        #region IEditable
        public override GeneratorDbAccessNodeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbAccessNodeSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:170
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorDbAccessNodeSettings from) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:176
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorDbAccessNodeSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_db_access_node_settings' to 'GeneratorDbAccessNodeSettings'
        public static GeneratorDbAccessNodeSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_settings m, GeneratorDbAccessNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:186
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm._IsParam1 = m.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsIncluded = m.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsPropertyParam1 = m.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            vm._IsCatalogFormParam1 = m.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:239
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodeSettings' to 'proto_generator_db_access_node_settings'
        public static Proto.Plugin.proto_generator_db_access_node_settings ConvertToProto(GeneratorDbAccessNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:252
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_access_node_settings m = new Proto.Plugin.proto_generator_db_access_node_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:255
            m.IsParam1 = vm.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsIncluded = vm.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsPropertyParam1 = vm.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Clone.tt Line:307
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:10
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\AcceptNodeVisitor.tt Line:38
        }
        #endregion Procedures
        #region Properties // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Class.tt Line:145
        
        public bool IsParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsParam1; }
            set
            {
                // Use 'OnIsParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsParam1, value, (t) => { bool isCancel = false; this.OnIsParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsParam1 = value; this.OnIsParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsParam1Changed();
        
        public bool? IsIncluded // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsIncluded; }
            set
            {
                // Use 'OnIsIncludedChanging' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsIncluded, value, (t) => { bool isCancel = false; this.OnIsIncludedChanging(ref value, ref isCancel); if (isCancel) return; this._IsIncluded = value; this.OnIsIncludedChanged(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool? _IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsIncludedChanging(ref bool? to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsIncludedChanged();
        
        public bool IsPropertyParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsPropertyParam1; }
            set
            {
                // Use 'OnIsPropertyParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsPropertyParam1, value, (t) => { bool isCancel = false; this.OnIsPropertyParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsPropertyParam1 = value; this.OnIsPropertyParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsPropertyParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsPropertyParam1Changed();
        
        public bool IsCatalogFormParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:9
        { 
            get { return this._IsCatalogFormParam1; }
            set
            {
                // Use 'OnIsCatalogFormParam1Changing' to change 'value' before setting property. It is a partial method and expected will be implemented not often.
                if (SetProperty(this._IsCatalogFormParam1, value, (t) => { bool isCancel = false; this.OnIsCatalogFormParam1Changing(ref value, ref isCancel); if (isCancel) return; this._IsCatalogFormParam1 = value; this.OnIsCatalogFormParam1Changed(); })) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:16
                {
                    this.ValidateProperty(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:19
                    this.IsChanged = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:22
                }
            }
        }
        private bool _IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:44
        partial void OnIsCatalogFormParam1Changing(ref bool to, ref bool isCancel); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\Property.tt Line:46
        partial void OnIsCatalogFormParam1Changed();
        #endregion Properties
    }
    
    public interface IVisitorProto // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\IVisitorProto.tt Line:9
    {
        void Visit(Proto.Plugin.proto_db_connection_string_settings p);
        void Visit(Proto.Plugin.proto_plugins_group_solution_sub_settings p);
        void Visit(Proto.Plugin.proto_plugins_group_solution_settings p);
        void Visit(Proto.Plugin.proto_plugins_group_project_settings p);
        void Visit(Proto.Plugin.proto_generator_db_schema_settings p);
        void Visit(Proto.Plugin.proto_generator_db_schema_node_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_settings p);
        void Visit(Proto.Plugin.proto_generator_db_access_node_settings p);
    }
    
    public partial class ValidationPluginSampleVisitor : PluginSampleVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:9
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(DbConnectionStringSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(DbConnectionStringSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(PluginsGroupSolutionSubSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(PluginsGroupSolutionSubSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(PluginsGroupSolutionSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
            p.ValidateSubAndCollectErrors(p.SubSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:47
        }
        protected override void OnVisitEnd(PluginsGroupSolutionSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(PluginsGroupProjectSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(PluginsGroupProjectSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbSchemaSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbSchemaSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbSchemaNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbSchemaNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbAccessSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbAccessSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbAccessNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:17
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbAccessNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ValidationVisitor.tt Line:55
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
    }
    
    public partial class PluginSampleVisitor : IVisitorPluginSampleNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\NodeVisitor.tt Line:9
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
        public void Visit(PluginsGroupSolutionSubSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginsGroupSolutionSubSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginsGroupSolutionSubSettings p) { }
        protected virtual void OnVisitEnd(PluginsGroupSolutionSubSettings p) { }
        public void Visit(PluginsGroupSolutionSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginsGroupSolutionSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginsGroupSolutionSettings p) { }
        protected virtual void OnVisitEnd(PluginsGroupSolutionSettings p) { }
        public void Visit(PluginsGroupProjectSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginsGroupProjectSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginsGroupProjectSettings p) { }
        protected virtual void OnVisitEnd(PluginsGroupProjectSettings p) { }
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
    
    public interface IVisitorPluginSampleNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\IVisitorConfigNode.tt Line:9
    {
        System.Threading.CancellationToken Token { get; }
    }
}