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

namespace vPlugin.Sample // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\NameSpace.tt Line:24
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IPluginSampleAcceptVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\NameSpace.tt Line:30
    {
        void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor);
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class DbConnectionStringSettingsValidator : ValidatorBase<DbConnectionStringSettings, DbConnectionStringSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class DbConnectionStringSettings : BaseSettings<DbConnectionStringSettings, DbConnectionStringSettingsValidator>, IDbConnectionStringSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public DbConnectionStringSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, DbConnectionStringSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static DbConnectionStringSettings Clone(ITreeConfigNode? parent, IDbConnectionStringSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            DbConnectionStringSettings vm = new DbConnectionStringSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._StringSettings = from.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(DbConnectionStringSettings to, IDbConnectionStringSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._StringSettings = from.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override DbConnectionStringSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return DbConnectionStringSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
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
        public static DbConnectionStringSettings ConvertToVM(Proto.Plugin.proto_db_connection_string_settings m, DbConnectionStringSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._StringSettings = m.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'DbConnectionStringSettings' to 'proto_db_connection_string_settings'
        public static Proto.Plugin.proto_db_connection_string_settings ConvertToProto(DbConnectionStringSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_db_connection_string_settings m = new Proto.Plugin.proto_db_connection_string_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.StringSettings = vm.StringSettings; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public string StringSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnStringSettingsChanging(ref string to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnStringSettingsChanged();
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class PluginsGroupSolutionSubSettingsValidator : ValidatorBase<PluginsGroupSolutionSubSettings, PluginsGroupSolutionSubSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class PluginsGroupSolutionSubSettings : BaseSubSettings<PluginsGroupSolutionSubSettings, PluginsGroupSolutionSubSettingsValidator>, IPluginsGroupSolutionSubSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public PluginsGroupSolutionSubSettings(IEditableObjectExt? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, PluginsGroupSolutionSubSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static PluginsGroupSolutionSubSettings Clone(IEditableObjectExt? parent, IPluginsGroupSolutionSubSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            PluginsGroupSolutionSubSettings vm = new PluginsGroupSolutionSubSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._IsSubParam1 = from.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsSubParam2 = from.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(PluginsGroupSolutionSubSettings to, IPluginsGroupSolutionSubSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsSubParam1 = from.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsSubParam2 = from.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override PluginsGroupSolutionSubSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return PluginsGroupSolutionSubSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginsGroupSolutionSubSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginsGroupSolutionSubSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugins_group_solution_sub_settings' to 'PluginsGroupSolutionSubSettings'
        public static PluginsGroupSolutionSubSettings ConvertToVM(Proto.Plugin.proto_plugins_group_solution_sub_settings m, PluginsGroupSolutionSubSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._IsSubParam1 = m.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsSubParam2 = m.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'PluginsGroupSolutionSubSettings' to 'proto_plugins_group_solution_sub_settings'
        public static Proto.Plugin.proto_plugins_group_solution_sub_settings ConvertToProto(PluginsGroupSolutionSubSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_plugins_group_solution_sub_settings m = new Proto.Plugin.proto_plugins_group_solution_sub_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.IsSubParam1 = vm.IsSubParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsSubParam2 = vm.IsSubParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool IsSubParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
        { 
            get { return this._IsSubParam1; }
            set
            {
                if (this._IsSubParam1 != value)
                {
                    this.OnIsSubParam1Changing(ref value);
                    this._IsSubParam1 = value;
                    this.OnIsSubParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsSubParam1;
        partial void OnIsSubParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsSubParam1Changed();
        
        public bool IsSubParam2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
        { 
            get { return this._IsSubParam2; }
            set
            {
                if (this._IsSubParam2 != value)
                {
                    this.OnIsSubParam2Changing(ref value);
                    this._IsSubParam2 = value;
                    this.OnIsSubParam2Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsSubParam2;
        partial void OnIsSubParam2Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsSubParam2Changed();
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class PluginsGroupSolutionSettingsValidator : ValidatorBase<PluginsGroupSolutionSettings, PluginsGroupSolutionSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class PluginsGroupSolutionSettings : BaseSettings<PluginsGroupSolutionSettings, PluginsGroupSolutionSettingsValidator>, IPluginsGroupSolutionSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public PluginsGroupSolutionSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, PluginsGroupSolutionSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this._SubSettings = new PluginsGroupSolutionSubSettings(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:41
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static PluginsGroupSolutionSettings Clone(ITreeConfigNode? parent, IPluginsGroupSolutionSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            PluginsGroupSolutionSettings vm = new PluginsGroupSolutionSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._IsGroupParam1 = from.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            if (isDeep) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:69 IsDefaultBase=False
                vm.SubSettings = vPlugin.Sample.PluginsGroupSolutionSubSettings.Clone(vm, from.SubSettings, isDeep);
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(PluginsGroupSolutionSettings to, IPluginsGroupSolutionSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsGroupParam1 = from.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            if (isDeep) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:149
                vPlugin.Sample.PluginsGroupSolutionSubSettings.Update((PluginsGroupSolutionSubSettings)to.SubSettings, from.SubSettings, isDeep);
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override PluginsGroupSolutionSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return PluginsGroupSolutionSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginsGroupSolutionSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginsGroupSolutionSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugins_group_solution_settings' to 'PluginsGroupSolutionSettings'
        public static PluginsGroupSolutionSettings ConvertToVM(Proto.Plugin.proto_plugins_group_solution_settings m, PluginsGroupSolutionSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._IsGroupParam1 = m.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            if (vm.SubSettings == null) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:226
                vm.SubSettings = new PluginsGroupSolutionSubSettings(vm); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:228
            vPlugin.Sample.PluginsGroupSolutionSubSettings.ConvertToVM(m.SubSettings, (PluginsGroupSolutionSubSettings)vm.SubSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:232
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'PluginsGroupSolutionSettings' to 'proto_plugins_group_solution_settings'
        public static Proto.Plugin.proto_plugins_group_solution_settings ConvertToProto(PluginsGroupSolutionSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_plugins_group_solution_settings m = new Proto.Plugin.proto_plugins_group_solution_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.IsGroupParam1 = vm.IsGroupParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.SubSettings = vPlugin.Sample.PluginsGroupSolutionSubSettings.ConvertToProto((PluginsGroupSolutionSubSettings)vm.SubSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:287
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.SubSettings.AcceptPluginSampleNodeVisitor(visitor); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:31
        
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(1)]
        [DisplayName("Param1")]
        [Description("Sample of Param1")]
        public bool IsGroupParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsGroupParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsGroupParam1Changed();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("Sub Settings")]
        [Description("Sample of Sub Settings")]
        [ExpandableObjectAttribute()]
        public PluginsGroupSolutionSubSettings SubSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
        { 
            get { return this._SubSettings; }
            set
            {
                if (this._SubSettings != value)
                {
                    this.OnSubSettingsChanging(ref value);
                    this._SubSettings = value;
                    this.OnSubSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private PluginsGroupSolutionSubSettings _SubSettings;
        IPluginsGroupSolutionSubSettings IPluginsGroupSolutionSettings.SubSettings { get { return (this as PluginsGroupSolutionSettings).SubSettings; } } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:82
        partial void OnSubSettingsChanging(ref PluginsGroupSolutionSubSettings to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnSubSettingsChanged();
        //IPluginsGroupSolutionSubSettings IPluginsGroupSolutionSettings.SubSettings { get { return this._SubSettings; } }
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class PluginsGroupProjectSettingsValidator : ValidatorBase<PluginsGroupProjectSettings, PluginsGroupProjectSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class PluginsGroupProjectSettings : BaseSettings<PluginsGroupProjectSettings, PluginsGroupProjectSettingsValidator>, IPluginsGroupProjectSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public PluginsGroupProjectSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, PluginsGroupProjectSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static PluginsGroupProjectSettings Clone(ITreeConfigNode? parent, IPluginsGroupProjectSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            PluginsGroupProjectSettings vm = new PluginsGroupProjectSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._IsGroupProjectParam1 = from.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(PluginsGroupProjectSettings to, IPluginsGroupProjectSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsGroupProjectParam1 = from.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override PluginsGroupProjectSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return PluginsGroupProjectSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginsGroupProjectSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginsGroupProjectSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugins_group_project_settings' to 'PluginsGroupProjectSettings'
        public static PluginsGroupProjectSettings ConvertToVM(Proto.Plugin.proto_plugins_group_project_settings m, PluginsGroupProjectSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._IsGroupProjectParam1 = m.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'PluginsGroupProjectSettings' to 'proto_plugins_group_project_settings'
        public static Proto.Plugin.proto_plugins_group_project_settings ConvertToProto(PluginsGroupProjectSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_plugins_group_project_settings m = new Proto.Plugin.proto_plugins_group_project_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.IsGroupProjectParam1 = vm.IsGroupProjectParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool IsGroupProjectParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
        { 
            get { return this._IsGroupProjectParam1; }
            set
            {
                if (this._IsGroupProjectParam1 != value)
                {
                    this.OnIsGroupProjectParam1Changing(ref value);
                    this._IsGroupProjectParam1 = value;
                    this.OnIsGroupProjectParam1Changed();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsGroupProjectParam1;
        partial void OnIsGroupProjectParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsGroupProjectParam1Changed();
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbSchemaSettingsValidator : ValidatorBase<GeneratorDbSchemaSettings, GeneratorDbSchemaSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class GeneratorDbSchemaSettings : BaseSettings<GeneratorDbSchemaSettings, GeneratorDbSchemaSettingsValidator>, IGeneratorDbSchemaSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public GeneratorDbSchemaSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, GeneratorDbSchemaSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbSchemaSettings Clone(ITreeConfigNode? parent, IGeneratorDbSchemaSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            GeneratorDbSchemaSettings vm = new GeneratorDbSchemaSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._IsSchemaParam1 = from.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsSchemaParam2 = from.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._SchemaParam3 = from.SchemaParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(GeneratorDbSchemaSettings to, IGeneratorDbSchemaSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsSchemaParam1 = from.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsSchemaParam2 = from.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._SchemaParam3 = from.SchemaParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override GeneratorDbSchemaSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbSchemaSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
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
        public static GeneratorDbSchemaSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_settings m, GeneratorDbSchemaSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._IsSchemaParam1 = m.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsSchemaParam2 = m.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._SchemaParam3 = m.SchemaParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaSettings' to 'proto_generator_db_schema_settings'
        public static Proto.Plugin.proto_generator_db_schema_settings ConvertToProto(GeneratorDbSchemaSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_schema_settings m = new Proto.Plugin.proto_generator_db_schema_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.IsSchemaParam1 = vm.IsSchemaParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsSchemaParam2 = vm.IsSchemaParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.SchemaParam3 = vm.SchemaParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool IsSchemaParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsSchemaParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsSchemaParam1Changed();
        
        public bool? IsSchemaParam2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsSchemaParam2Changing(ref bool? to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsSchemaParam2Changed();
        
        public string SchemaParam3 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnSchemaParam3Changing(ref string to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnSchemaParam3Changed();
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbSchemaNodeSettingsValidator : ValidatorBase<GeneratorDbSchemaNodeSettings, GeneratorDbSchemaNodeSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class GeneratorDbSchemaNodeSettings : BaseSettings<GeneratorDbSchemaNodeSettings, GeneratorDbSchemaNodeSettingsValidator>, IGeneratorDbSchemaNodeSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public GeneratorDbSchemaNodeSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, GeneratorDbSchemaNodeSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbSchemaNodeSettings Clone(ITreeConfigNode? parent, IGeneratorDbSchemaNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            GeneratorDbSchemaNodeSettings vm = new GeneratorDbSchemaNodeSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsConstantParam1 = from.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(GeneratorDbSchemaNodeSettings to, IGeneratorDbSchemaNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsConstantParam1 = from.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override GeneratorDbSchemaNodeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbSchemaNodeSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
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
        public static GeneratorDbSchemaNodeSettings ConvertToVM(Proto.Plugin.proto_generator_db_schema_node_settings m, GeneratorDbSchemaNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._IsParam1 = m.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsIncluded = m.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsConstantParam1 = m.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsCatalogFormParam1 = m.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'GeneratorDbSchemaNodeSettings' to 'proto_generator_db_schema_node_settings'
        public static Proto.Plugin.proto_generator_db_schema_node_settings ConvertToProto(GeneratorDbSchemaNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_schema_node_settings m = new Proto.Plugin.proto_generator_db_schema_node_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.IsParam1 = vm.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsIncluded = vm.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsConstantParam1 = vm.IsConstantParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool IsParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsParam1Changed();
        
        public bool? IsIncluded // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsIncludedChanging(ref bool? to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsIncludedChanged();
        
        public bool IsConstantParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsConstantParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsConstantParam1Changed();
        
        public bool IsCatalogFormParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsCatalogFormParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsCatalogFormParam1Changed();
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbAccessSettingsValidator : ValidatorBase<GeneratorDbAccessSettings, GeneratorDbAccessSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class GeneratorDbAccessSettings : BaseSettings<GeneratorDbAccessSettings, GeneratorDbAccessSettingsValidator>, IGeneratorDbAccessSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public GeneratorDbAccessSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, GeneratorDbAccessSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessSettings Clone(ITreeConfigNode? parent, IGeneratorDbAccessSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            GeneratorDbAccessSettings vm = new GeneratorDbAccessSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._IsAccessParam1 = from.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsAccessParam2 = from.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._AccessParam3 = from.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._AccessParam4 = from.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsGenerateNotValidCode = from.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(GeneratorDbAccessSettings to, IGeneratorDbAccessSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsAccessParam1 = from.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsAccessParam2 = from.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._AccessParam3 = from.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._AccessParam4 = from.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsGenerateNotValidCode = from.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override GeneratorDbAccessSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbAccessSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
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
        public static GeneratorDbAccessSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_settings m, GeneratorDbAccessSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._IsAccessParam1 = m.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsAccessParam2 = m.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._AccessParam3 = m.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._AccessParam4 = m.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsGenerateNotValidCode = m.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'GeneratorDbAccessSettings' to 'proto_generator_db_access_settings'
        public static Proto.Plugin.proto_generator_db_access_settings ConvertToProto(GeneratorDbAccessSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_access_settings m = new Proto.Plugin.proto_generator_db_access_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.IsAccessParam1 = vm.IsAccessParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsAccessParam2 = vm.IsAccessParam2; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.AccessParam3 = vm.AccessParam3; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.AccessParam4 = vm.AccessParam4; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsGenerateNotValidCode = vm.IsGenerateNotValidCode; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool IsAccessParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsAccessParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsAccessParam1Changed();
        
        public bool? IsAccessParam2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsAccessParam2Changing(ref bool? to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsAccessParam2Changed();
        
        public string AccessParam3 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnAccessParam3Changing(ref string to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnAccessParam3Changed();
        
        public string? AccessParam4 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        private string? _AccessParam4;
        partial void OnAccessParam4Changing(ref string? to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnAccessParam4Changed();
        
        public bool IsGenerateNotValidCode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsGenerateNotValidCodeChanging(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsGenerateNotValidCodeChanged();
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:7
    //       IsWithParent: True 
    //      IsDefaultBase: False 
    // IsConfigObjectBase: False 
    //      IsGenSettings: False 
    //     IsBindableBase: True 
    //     IsEditableBase: True 
    //  IsValidatableBase: True 
    //    IsISortingValue: False 
    public partial class GeneratorDbAccessNodeSettingsValidator : ValidatorBase<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettingsValidator> { } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:16
    public partial class GeneratorDbAccessNodeSettings : BaseSettings<GeneratorDbAccessNodeSettings, GeneratorDbAccessNodeSettingsValidator>, IGeneratorDbAccessNodeSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:17
    {
        #region CTOR
        public GeneratorDbAccessNodeSettings(ITreeConfigNode? parent) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:27
            : base(parent, GeneratorDbAccessNodeSettingsValidator.Validator)
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnCreating();
            this.OnCreated();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreating();
        // Use fields to set properties of this class during creation to avoid property change notification
        partial void OnCreated();
        #endregion CTOR
        #region Procedures
        public static GeneratorDbAccessNodeSettings Clone(ITreeConfigNode? parent, IGeneratorDbAccessNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:29
        {
            Debug.Assert(from != null);
            GeneratorDbAccessNodeSettings vm = new GeneratorDbAccessNodeSettings(parent); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:36
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:41
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:44
            vm._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsPropertyParam1 = from.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:72
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:81
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:84
            return vm;
        }
        public static void Update(GeneratorDbAccessNodeSettings to, IGeneratorDbAccessNodeSettings from, bool isDeep = true) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:88
        {
            Debug.Assert(to != null);
            Debug.Assert(from != null);
            to._IsParam1 = from.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsIncluded = from.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsPropertyParam1 = from.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
            to._IsCatalogFormParam1 = from.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:152
        }
        // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:158
        #region IEditable
        public override GeneratorDbAccessNodeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            Debug.Assert(this is IConfig || this.Parent != null);
            return GeneratorDbAccessNodeSettings.Clone(this.Parent, this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:166
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
        public static GeneratorDbAccessNodeSettings ConvertToVM(Proto.Plugin.proto_generator_db_access_node_settings m, GeneratorDbAccessNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:182
        {
            Debug.Assert(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:190
            vm.IsValidate = false; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:193
            vm._IsParam1 = m.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsIncluded = m.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsPropertyParam1 = m.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm._IsCatalogFormParam1 = m.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:234
            vm.IsNotifying = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:245
            vm.IsValidate = true; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:248
            return vm;
        }
        // Conversion from 'GeneratorDbAccessNodeSettings' to 'proto_generator_db_access_node_settings'
        public static Proto.Plugin.proto_generator_db_access_node_settings ConvertToProto(GeneratorDbAccessNodeSettings vm) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:253
        {
            Debug.Assert(vm != null);
            Proto.Plugin.proto_generator_db_access_node_settings m = new Proto.Plugin.proto_generator_db_access_node_settings(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:256
            m.IsParam1 = vm.IsParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsIncluded = vm.IsIncluded; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsPropertyParam1 = vm.IsPropertyParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            m.IsCatalogFormParam1 = vm.IsCatalogFormParam1; // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Clone.tt Line:293
            return m;
        }
        
        public void AcceptPluginSampleNodeVisitor(PluginSampleVisitor visitor) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:9
        {
            Debug.Assert(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            visitor.VisitEnd(this); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\AcceptNodeVisitor.tt Line:36
        }
        #endregion Procedures
        #region Properties
        
        public bool IsParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsParam1Changed();
        
        public bool? IsIncluded // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsIncludedChanging(ref bool? to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsIncludedChanged();
        
        public bool IsPropertyParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsPropertyParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsPropertyParam1Changed();
        
        public bool IsCatalogFormParam1 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:58
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
        partial void OnIsCatalogFormParam1Changing(ref bool to); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:84
        partial void OnIsCatalogFormParam1Changed();
    /*
        [Browsable(false)]
        public override bool IsChanged // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:117
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
        partial void OnIsChangedChanging(ref bool v); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:134
        */
        //partial void OnIsChangedChanged(); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Class.tt Line:139
        #endregion Properties
    }
    
    public interface IVisitorProto // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\IVisitorProto.tt Line:8
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
    
    public partial class ValidationPluginSampleVisitor : PluginSampleVisitor // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:8
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(DbConnectionStringSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(DbConnectionStringSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(PluginsGroupSolutionSubSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(PluginsGroupSolutionSubSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(PluginsGroupSolutionSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
            p.ValidateSubAndCollectErrors(p.SubSettings); // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:43
        }
        protected override void OnVisitEnd(PluginsGroupSolutionSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(PluginsGroupProjectSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(PluginsGroupProjectSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbSchemaSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbSchemaSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbSchemaNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbSchemaNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbAccessSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbAccessSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
        protected override void OnVisit(GeneratorDbAccessNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:16
        {
            this.OnVisit((IValidatableWithSeverity)p);
        }
        protected override void OnVisitEnd(GeneratorDbAccessNodeSettings p) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ValidationVisitor.tt Line:50
        {
            this.OnVisitEnd((IValidatableWithSeverity)p);
        }
    }
    
    public partial class PluginSampleVisitor : IVisitorPluginSampleNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\NodeVisitor.tt Line:8
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
    
    public interface IVisitorPluginSampleNode // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\IVisitorConfigNode.tt Line:8
    {
        System.Threading.CancellationToken Token { get; }
    }
}