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

namespace vSharpStudio.vm.ViewModels // NameSpace.tt Line: 22
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IConfigAcceptVisitor // NameSpace.tt Line: 28
    {
        void AcceptConfigNodeVisitor(ConfigVisitor visitor);
    }
    public partial class UserSettingsValidator : ValidatorBase<UserSettings, UserSettingsValidator> { } // Class.tt Line: 6
    public partial class UserSettings : VmValidatableWithSeverity<UserSettings, UserSettingsValidator>, IUserSettings // Class.tt Line: 7
    {
        #region CTOR
        public UserSettings() 
            : base(UserSettingsValidator.Validator) // Class.tt Line: 45
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Class.tt Line: 54
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static UserSettings Clone(IUserSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            UserSettings vm = new UserSettings();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Clone.tt Line: 47
            foreach (var t in from.ListOpenConfigHistory) // Clone.tt Line: 48
                vm.ListOpenConfigHistory.Add(UserSettingsOpenedConfig.Clone((UserSettingsOpenedConfig)t, isDeep));
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(UserSettings to, IUserSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListOpenConfigHistory.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListOpenConfigHistory)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            UserSettingsOpenedConfig.Update((UserSettingsOpenedConfig)t, (UserSettingsOpenedConfig)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListOpenConfigHistory.Remove(t);
                }
                foreach (var tt in from.ListOpenConfigHistory)
                {
                    bool isfound = false;
                    foreach (var t in to.ListOpenConfigHistory.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new UserSettingsOpenedConfig(); // Clone.tt Line: 119
                        UserSettingsOpenedConfig.Update(p, (UserSettingsOpenedConfig)tt, isDeep);
                        to.ListOpenConfigHistory.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override UserSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return UserSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(UserSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            UserSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_user_settings' to 'UserSettings'
        public static UserSettings ConvertToVM(Proto.Config.proto_user_settings m, UserSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Clone.tt Line: 190
            foreach (var t in m.ListOpenConfigHistory) // Clone.tt Line: 191
            {
                var tvm = UserSettingsOpenedConfig.ConvertToVM(t, new UserSettingsOpenedConfig()); // Clone.tt Line: 196
                vm.ListOpenConfigHistory.Add(tvm);
            }
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'UserSettings' to 'proto_user_settings'
        public static Proto.Config.proto_user_settings ConvertToProto(UserSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_user_settings m = new Proto.Config.proto_user_settings(); // Clone.tt Line: 239
            foreach (var t in vm.ListOpenConfigHistory) // Clone.tt Line: 242
                m.ListOpenConfigHistory.Add(UserSettingsOpenedConfig.ConvertToProto((UserSettingsOpenedConfig)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListOpenConfigHistory)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [BrowsableAttribute(false)]
        public ObservableCollection<UserSettingsOpenedConfig> ListOpenConfigHistory // Property.tt Line: 8
        { 
            get { return this._ListOpenConfigHistory; }
            set
            {
                if (this._ListOpenConfigHistory != value)
                {
                    this.OnListOpenConfigHistoryChanging(value);
                    _ListOpenConfigHistory = value;
                    this.OnListOpenConfigHistoryChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ObservableCollection<UserSettingsOpenedConfig> _ListOpenConfigHistory;
        IReadOnlyList<IUserSettingsOpenedConfig> IUserSettings.ListOpenConfigHistory { get { return (this as UserSettings).ListOpenConfigHistory; } } // Property.tt Line: 26
        partial void OnListOpenConfigHistoryChanging(ObservableCollection<UserSettingsOpenedConfig> to); // Property.tt Line: 27
        partial void OnListOpenConfigHistoryChanged();
        #endregion Properties
    }
    public partial class UserSettingsOpenedConfigValidator : ValidatorBase<UserSettingsOpenedConfig, UserSettingsOpenedConfigValidator> { } // Class.tt Line: 6
    public partial class UserSettingsOpenedConfig : VmValidatableWithSeverity<UserSettingsOpenedConfig, UserSettingsOpenedConfigValidator>, IUserSettingsOpenedConfig // Class.tt Line: 7
    {
        #region CTOR
        public UserSettingsOpenedConfig() 
            : base(UserSettingsOpenedConfigValidator.Validator) // Class.tt Line: 45
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
        public static UserSettingsOpenedConfig Clone(IUserSettingsOpenedConfig from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            UserSettingsOpenedConfig vm = new UserSettingsOpenedConfig();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.OpenedLastTimeOn = from.OpenedLastTimeOn; // Clone.tt Line: 65
            vm.ConfigPath = from.ConfigPath; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(UserSettingsOpenedConfig to, IUserSettingsOpenedConfig from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.OpenedLastTimeOn = from.OpenedLastTimeOn; // Clone.tt Line: 141
            to.ConfigPath = from.ConfigPath; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override UserSettingsOpenedConfig Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return UserSettingsOpenedConfig.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(UserSettingsOpenedConfig from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            UserSettingsOpenedConfig.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_user_settings_opened_config' to 'UserSettingsOpenedConfig'
        public static UserSettingsOpenedConfig ConvertToVM(Proto.Config.proto_user_settings_opened_config m, UserSettingsOpenedConfig vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.OpenedLastTimeOn = m.OpenedLastTimeOn; // Clone.tt Line: 221
            vm.ConfigPath = m.ConfigPath; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'UserSettingsOpenedConfig' to 'proto_user_settings_opened_config'
        public static Proto.Config.proto_user_settings_opened_config ConvertToProto(UserSettingsOpenedConfig vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_user_settings_opened_config m = new Proto.Config.proto_user_settings_opened_config(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.OpenedLastTimeOn = vm.OpenedLastTimeOn; // Clone.tt Line: 276
            m.ConfigPath = vm.ConfigPath; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Guid = string.Empty;
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [BrowsableAttribute(false)]
        public Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn // Property.tt Line: 55
        { 
            get { return this._OpenedLastTimeOn; }
            set
            {
                if (this._OpenedLastTimeOn != value)
                {
                    this.OnOpenedLastTimeOnChanging(ref value);
                    this._OpenedLastTimeOn = value;
                    this.OnOpenedLastTimeOnChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private Google.Protobuf.WellKnownTypes.Timestamp _OpenedLastTimeOn;
        partial void OnOpenedLastTimeOnChanging(ref Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 79
        partial void OnOpenedLastTimeOnChanged();
        //IGoogle.Protobuf.WellKnownTypes.Timestamp IUserSettingsOpenedConfig.OpenedLastTimeOn { get { return this._OpenedLastTimeOn; } }
        
        [BrowsableAttribute(false)]
        public string ConfigPath // Property.tt Line: 55
        { 
            get { return this._ConfigPath; }
            set
            {
                if (this._ConfigPath != value)
                {
                    this.OnConfigPathChanging(ref value);
                    this._ConfigPath = value;
                    this.OnConfigPathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _ConfigPath = string.Empty;
        partial void OnConfigPathChanging(ref string to); // Property.tt Line: 79
        partial void OnConfigPathChanged();
        #endregion Properties
    }
    public partial class GroupListPluginsValidator : ValidatorBase<GroupListPlugins, GroupListPluginsValidator> { } // Class.tt Line: 6
    public partial class GroupListPlugins : ConfigObjectCommonBase<GroupListPlugins, GroupListPluginsValidator>, IComparable<GroupListPlugins>, IConfigAcceptVisitor, IGroupListPlugins // Class.tt Line: 7
    {
        #region CTOR
        public GroupListPlugins() : this(default(ITreeConfigNode))
        {
        }
        public GroupListPlugins(ITreeConfigNode parent) 
            : base(parent, GroupListPluginsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListPlugins = new ConfigNodesCollection<Plugin>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Plugin)) // Clone.tt Line: 15
            {
                this.ListPlugins.Sort();
            }
        }
        public static GroupListPlugins Clone(ITreeConfigNode parent, IGroupListPlugins from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListPlugins vm = new GroupListPlugins(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListPlugins) // Clone.tt Line: 52
                vm.ListPlugins.Add(Plugin.Clone(vm, (Plugin)t, isDeep));
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListPlugins to, IGroupListPlugins from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListPlugins.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListPlugins)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Plugin.Update((Plugin)t, (Plugin)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListPlugins.Remove(t);
                }
                foreach (var tt in from.ListPlugins)
                {
                    bool isfound = false;
                    foreach (var t in to.ListPlugins.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Plugin(to); // Clone.tt Line: 117
                        Plugin.Update(p, (Plugin)tt, isDeep);
                        to.ListPlugins.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListPlugins Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListPlugins.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListPlugins from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListPlugins.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_plugins' to 'GroupListPlugins'
        public static GroupListPlugins ConvertToVM(Proto.Config.proto_group_list_plugins m, GroupListPlugins vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListPlugins) // Clone.tt Line: 201
            {
                var tvm = Plugin.ConvertToVM(t, new Plugin(vm)); // Clone.tt Line: 204
                vm.ListPlugins.Add(tvm);
            }
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListPlugins' to 'proto_group_list_plugins'
        public static Proto.Config.proto_group_list_plugins ConvertToProto(GroupListPlugins vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_plugins m = new Proto.Config.proto_group_list_plugins(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            foreach (var t in vm.ListPlugins) // Clone.tt Line: 242
                m.ListPlugins.Add(Plugin.ConvertToProto((Plugin)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListPlugins)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Plugin> ListPlugins // Property.tt Line: 8
        { 
            get { return this._ListPlugins; }
            set
            {
                if (this._ListPlugins != value)
                {
                    this.OnListPluginsChanging(value);
                    _ListPlugins = value;
                    this.OnListPluginsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Plugin> _ListPlugins;
        IReadOnlyList<IPlugin> IGroupListPlugins.ListPlugins { get { return (this as GroupListPlugins).ListPlugins; } } // Property.tt Line: 26
        partial void OnListPluginsChanging(ObservableCollection<Plugin> to); // Property.tt Line: 27
        partial void OnListPluginsChanged();
        public Plugin this[int index] { get { return (Plugin)this.ListPlugins[index]; } }
        IPlugin IGroupListPlugins.this[int index] { get { return (Plugin)this.ListPlugins[index]; } }
        public void Add(Plugin item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListPlugins.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Plugin> items) 
        { 
            Contract.Requires(items != null);
            this.ListPlugins.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListPlugins.Count; }
        int IGroupListPlugins.Count() { return this.Count(); }
        public void Remove(Plugin item) 
        {
            Contract.Requires(item != null);
            this.ListPlugins.Remove(item); 
            item.Parent = null;
        }
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class PluginValidator : ValidatorBase<Plugin, PluginValidator> { } // Class.tt Line: 6
    public partial class Plugin : ConfigObjectCommonBase<Plugin, PluginValidator>, IComparable<Plugin>, IConfigAcceptVisitor, IPlugin // Class.tt Line: 7
    {
        #region CTOR
        public Plugin() : this(default(ITreeConfigNode))
        {
        }
        public Plugin(ITreeConfigNode parent) 
            : base(parent, PluginValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListGenerators = new ConfigNodesCollection<PluginGenerator>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGenerator)) // Clone.tt Line: 15
            {
                this.ListGenerators.Sort();
            }
        }
        public static Plugin Clone(ITreeConfigNode parent, IPlugin from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Plugin vm = new Plugin(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Version = from.Version; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.ListGenerators = new ConfigNodesCollection<PluginGenerator>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListGenerators) // Clone.tt Line: 52
                vm.ListGenerators.Add(PluginGenerator.Clone(vm, (PluginGenerator)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Plugin to, IPlugin from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Version = from.Version; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListGenerators.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGenerators)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGenerator.Update((PluginGenerator)t, (PluginGenerator)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGenerators.Remove(t);
                }
                foreach (var tt in from.ListGenerators)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGenerators.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGenerator(to); // Clone.tt Line: 117
                        PluginGenerator.Update(p, (PluginGenerator)tt, isDeep);
                        to.ListGenerators.Add(p);
                    }
                }
            }
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Plugin Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Plugin.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Plugin from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Plugin.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin' to 'Plugin'
        public static Plugin ConvertToVM(Proto.Config.proto_plugin m, Plugin vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Version = m.Version; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.ListGenerators = new ConfigNodesCollection<PluginGenerator>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListGenerators) // Clone.tt Line: 201
            {
                var tvm = PluginGenerator.ConvertToVM(t, new PluginGenerator(vm)); // Clone.tt Line: 204
                vm.ListGenerators.Add(tvm);
            }
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Plugin' to 'proto_plugin'
        public static Proto.Config.proto_plugin ConvertToProto(Plugin vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin m = new Proto.Config.proto_plugin(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Version = vm.Version; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            foreach (var t in vm.ListGenerators) // Clone.tt Line: 242
                m.ListGenerators.Add(PluginGenerator.ConvertToProto((PluginGenerator)t)); // Clone.tt Line: 246
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListGenerators)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [ReadOnly(true)]
        public string Version // Property.tt Line: 55
        { 
            get { return this._Version; }
            set
            {
                if (this._Version != value)
                {
                    this.OnVersionChanging(ref value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Version = string.Empty;
        partial void OnVersionChanging(ref string to); // Property.tt Line: 79
        partial void OnVersionChanged();
        
        [PropertyOrderAttribute(1)]
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [ReadOnly(true)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGenerator> ListGenerators // Property.tt Line: 8
        { 
            get { return this._ListGenerators; }
            set
            {
                if (this._ListGenerators != value)
                {
                    this.OnListGeneratorsChanging(value);
                    _ListGenerators = value;
                    this.OnListGeneratorsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGenerator> _ListGenerators;
        IReadOnlyList<IPluginGenerator> IPlugin.ListGenerators { get { return (this as Plugin).ListGenerators; } } // Property.tt Line: 26
        partial void OnListGeneratorsChanging(ObservableCollection<PluginGenerator> to); // Property.tt Line: 27
        partial void OnListGeneratorsChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorValidator : ValidatorBase<PluginGenerator, PluginGeneratorValidator> { } // Class.tt Line: 6
    public partial class PluginGenerator : ConfigObjectCommonBase<PluginGenerator, PluginGeneratorValidator>, IComparable<PluginGenerator>, IConfigAcceptVisitor, IPluginGenerator // Class.tt Line: 7
    {
        #region CTOR
        public PluginGenerator() : this(default(ITreeConfigNode))
        {
        }
        public PluginGenerator(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGenerator Clone(ITreeConfigNode parent, IPluginGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGenerator vm = new PluginGenerator(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGenerator to, IPluginGenerator from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGenerator Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGenerator.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginGenerator from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginGenerator.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin_generator' to 'PluginGenerator'
        public static PluginGenerator ConvertToVM(Proto.Config.proto_plugin_generator m, PluginGenerator vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGenerator' to 'proto_plugin_generator'
        public static Proto.Config.proto_plugin_generator ConvertToProto(PluginGenerator vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_generator m = new Proto.Config.proto_plugin_generator(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [ReadOnly(true)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { } // Class.tt Line: 6
    public partial class SettingsConfig : VmValidatableWithSeverity<SettingsConfig, SettingsConfigValidator>, ISettingsConfig // Class.tt Line: 7
    {
        #region CTOR
        public SettingsConfig() 
            : base(SettingsConfigValidator.Validator) // Class.tt Line: 45
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
        public static SettingsConfig Clone(ISettingsConfig from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            SettingsConfig vm = new SettingsConfig();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 65
            vm.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(SettingsConfig to, ISettingsConfig from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Name = from.Name; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 141
            to.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override SettingsConfig Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return SettingsConfig.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(SettingsConfig from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            SettingsConfig.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_settings_config' to 'SettingsConfig'
        public static SettingsConfig ConvertToVM(Proto.Config.proto_settings_config m, SettingsConfig vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.VersionMigrationCurrent = m.VersionMigrationCurrent; // Clone.tt Line: 221
            vm.VersionMigrationSupportFromMin = m.VersionMigrationSupportFromMin; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'SettingsConfig' to 'proto_settings_config'
        public static Proto.Config.proto_settings_config ConvertToProto(SettingsConfig vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_settings_config m = new Proto.Config.proto_settings_config(); // Clone.tt Line: 239
            m.Name = vm.Name; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.VersionMigrationCurrent = vm.VersionMigrationCurrent; // Clone.tt Line: 276
            m.VersionMigrationSupportFromMin = vm.VersionMigrationSupportFromMin; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [PropertyOrderAttribute(1)]
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Name = string.Empty;
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _NameUi = string.Empty;
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// current migration version, increased by one on each deployment
        ///////////////////////////////////////////////////
        public int VersionMigrationCurrent // Property.tt Line: 55
        { 
            get { return this._VersionMigrationCurrent; }
            set
            {
                if (this._VersionMigrationCurrent != value)
                {
                    this.OnVersionMigrationCurrentChanging(ref value);
                    this._VersionMigrationCurrent = value;
                    this.OnVersionMigrationCurrentChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _VersionMigrationCurrent;
        partial void OnVersionMigrationCurrentChanging(ref int to); // Property.tt Line: 79
        partial void OnVersionMigrationCurrentChanged();
        
        
        ///////////////////////////////////////////////////
        /// min version supported by current version for migration
        ///////////////////////////////////////////////////
        public int VersionMigrationSupportFromMin // Property.tt Line: 55
        { 
            get { return this._VersionMigrationSupportFromMin; }
            set
            {
                if (this._VersionMigrationSupportFromMin != value)
                {
                    this.OnVersionMigrationSupportFromMinChanging(ref value);
                    this._VersionMigrationSupportFromMin = value;
                    this.OnVersionMigrationSupportFromMinChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _VersionMigrationSupportFromMin;
        partial void OnVersionMigrationSupportFromMinChanging(ref int to); // Property.tt Line: 79
        partial void OnVersionMigrationSupportFromMinChanged();
        #endregion Properties
    }
    public partial class ConfigShortHistoryValidator : ValidatorBase<ConfigShortHistory, ConfigShortHistoryValidator> { } // Class.tt Line: 6
    public partial class ConfigShortHistory : VmValidatableWithSeverity<ConfigShortHistory, ConfigShortHistoryValidator>, IConfigShortHistory // Class.tt Line: 7
    {
        #region CTOR
        public ConfigShortHistory() 
            : base(ConfigShortHistoryValidator.Validator) // Class.tt Line: 45
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.CurrentConfig = new Config(this); // Class.tt Line: 63
            this.PrevStableConfig = new Config(this); // Class.tt Line: 63
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static ConfigShortHistory Clone(IConfigShortHistory from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            ConfigShortHistory vm = new ConfigShortHistory();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.CurrentConfig = vSharpStudio.vm.ViewModels.Config.Clone(vm, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.PrevStableConfig = vSharpStudio.vm.ViewModels.Config.Clone(vm, from.PrevStableConfig, isDeep);
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(ConfigShortHistory to, IConfigShortHistory from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.Config.Update((Config)to.CurrentConfig, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.Config.Update((Config)to.PrevStableConfig, from.PrevStableConfig, isDeep);
        }
        // Clone.tt Line: 147
        #region IEditable
        public override ConfigShortHistory Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return ConfigShortHistory.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(ConfigShortHistory from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            ConfigShortHistory.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_config_short_history' to 'ConfigShortHistory'
        public static ConfigShortHistory ConvertToVM(Proto.Config.proto_config_short_history m, ConfigShortHistory vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            if (vm.CurrentConfig == null) // Clone.tt Line: 213
                vm.CurrentConfig = new Config(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.Config.ConvertToVM(m.CurrentConfig, (Config)vm.CurrentConfig); // Clone.tt Line: 219
            if (vm.PrevStableConfig == null) // Clone.tt Line: 213
                vm.PrevStableConfig = new Config(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.Config.ConvertToVM(m.PrevStableConfig, (Config)vm.PrevStableConfig); // Clone.tt Line: 219
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'ConfigShortHistory' to 'proto_config_short_history'
        public static Proto.Config.proto_config_short_history ConvertToProto(ConfigShortHistory vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_config_short_history m = new Proto.Config.proto_config_short_history(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.CurrentConfig = vSharpStudio.vm.ViewModels.Config.ConvertToProto((Config)vm.CurrentConfig); // Clone.tt Line: 270
            m.PrevStableConfig = vSharpStudio.vm.ViewModels.Config.ConvertToProto((Config)vm.PrevStableConfig); // Clone.tt Line: 270
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.CurrentConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.PrevStableConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Guid = string.Empty;
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Name = string.Empty;
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        public Config CurrentConfig // Property.tt Line: 55
        { 
            get { return this._CurrentConfig; }
            set
            {
                if (this._CurrentConfig != value)
                {
                    this.OnCurrentConfigChanging(ref value);
                    this._CurrentConfig = value;
                    this.OnCurrentConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private Config _CurrentConfig;
        IConfig IConfigShortHistory.CurrentConfig { get { return (this as ConfigShortHistory).CurrentConfig; } } // Property.tt Line: 77
        partial void OnCurrentConfigChanging(ref Config to); // Property.tt Line: 79
        partial void OnCurrentConfigChanged();
        //IConfig IConfigShortHistory.CurrentConfig { get { return this._CurrentConfig; } }
        
        public Config PrevStableConfig // Property.tt Line: 55
        { 
            get { return this._PrevStableConfig; }
            set
            {
                if (this._PrevStableConfig != value)
                {
                    this.OnPrevStableConfigChanging(ref value);
                    this._PrevStableConfig = value;
                    this.OnPrevStableConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private Config _PrevStableConfig;
        IConfig IConfigShortHistory.PrevStableConfig { get { return (this as ConfigShortHistory).PrevStableConfig; } } // Property.tt Line: 77
        partial void OnPrevStableConfigChanging(ref Config to); // Property.tt Line: 79
        partial void OnPrevStableConfigChanged();
        //IConfig IConfigShortHistory.PrevStableConfig { get { return this._PrevStableConfig; } }
        #endregion Properties
    }
    public partial class GroupListBaseConfigLinksValidator : ValidatorBase<GroupListBaseConfigLinks, GroupListBaseConfigLinksValidator> { } // Class.tt Line: 6
    public partial class GroupListBaseConfigLinks : ConfigObjectCommonBase<GroupListBaseConfigLinks, GroupListBaseConfigLinksValidator>, IComparable<GroupListBaseConfigLinks>, IConfigAcceptVisitor, IGroupListBaseConfigLinks // Class.tt Line: 7
    {
        #region CTOR
        public GroupListBaseConfigLinks() : this(default(ITreeConfigNode))
        {
        }
        public GroupListBaseConfigLinks(ITreeConfigNode parent) 
            : base(parent, GroupListBaseConfigLinksValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(BaseConfigLink)) // Clone.tt Line: 15
            {
                this.ListBaseConfigLinks.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListBaseConfigLinks Clone(ITreeConfigNode parent, IGroupListBaseConfigLinks from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListBaseConfigLinks vm = new GroupListBaseConfigLinks(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListBaseConfigLinks) // Clone.tt Line: 52
                vm.ListBaseConfigLinks.Add(BaseConfigLink.Clone(vm, (BaseConfigLink)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListBaseConfigLinks to, IGroupListBaseConfigLinks from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListBaseConfigLinks.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListBaseConfigLinks)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            BaseConfigLink.Update((BaseConfigLink)t, (BaseConfigLink)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListBaseConfigLinks.Remove(t);
                }
                foreach (var tt in from.ListBaseConfigLinks)
                {
                    bool isfound = false;
                    foreach (var t in to.ListBaseConfigLinks.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new BaseConfigLink(to); // Clone.tt Line: 117
                        BaseConfigLink.Update(p, (BaseConfigLink)tt, isDeep);
                        to.ListBaseConfigLinks.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListBaseConfigLinks Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListBaseConfigLinks.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListBaseConfigLinks from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListBaseConfigLinks.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_base_config_links' to 'GroupListBaseConfigLinks'
        public static GroupListBaseConfigLinks ConvertToVM(Proto.Config.proto_group_list_base_config_links m, GroupListBaseConfigLinks vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListBaseConfigLinks) // Clone.tt Line: 201
            {
                var tvm = BaseConfigLink.ConvertToVM(t, new BaseConfigLink(vm)); // Clone.tt Line: 204
                vm.ListBaseConfigLinks.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListBaseConfigLinks' to 'proto_group_list_base_config_links'
        public static Proto.Config.proto_group_list_base_config_links ConvertToProto(GroupListBaseConfigLinks vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_base_config_links m = new Proto.Config.proto_group_list_base_config_links(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListBaseConfigLinks) // Clone.tt Line: 242
                m.ListBaseConfigLinks.Add(BaseConfigLink.ConvertToProto((BaseConfigLink)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListBaseConfigLinks)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<BaseConfigLink> ListBaseConfigLinks // Property.tt Line: 8
        { 
            get { return this._ListBaseConfigLinks; }
            set
            {
                if (this._ListBaseConfigLinks != value)
                {
                    this.OnListBaseConfigLinksChanging(value);
                    _ListBaseConfigLinks = value;
                    this.OnListBaseConfigLinksChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<BaseConfigLink> _ListBaseConfigLinks;
        IReadOnlyList<IBaseConfigLink> IGroupListBaseConfigLinks.ListBaseConfigLinks { get { return (this as GroupListBaseConfigLinks).ListBaseConfigLinks; } } // Property.tt Line: 26
        partial void OnListBaseConfigLinksChanging(ObservableCollection<BaseConfigLink> to); // Property.tt Line: 27
        partial void OnListBaseConfigLinksChanged();
        public BaseConfigLink this[int index] { get { return (BaseConfigLink)this.ListBaseConfigLinks[index]; } }
        IBaseConfigLink IGroupListBaseConfigLinks.this[int index] { get { return (BaseConfigLink)this.ListBaseConfigLinks[index]; } }
        public void Add(BaseConfigLink item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListBaseConfigLinks.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<BaseConfigLink> items) 
        { 
            Contract.Requires(items != null);
            this.ListBaseConfigLinks.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListBaseConfigLinks.Count; }
        int IGroupListBaseConfigLinks.Count() { return this.Count(); }
        public void Remove(BaseConfigLink item) 
        {
            Contract.Requires(item != null);
            this.ListBaseConfigLinks.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListBaseConfigLinks.ListNodeGeneratorsSettings { get { return (this as GroupListBaseConfigLinks).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class BaseConfigLinkValidator : ValidatorBase<BaseConfigLink, BaseConfigLinkValidator> { } // Class.tt Line: 6
    public partial class BaseConfigLink : ConfigObjectCommonBase<BaseConfigLink, BaseConfigLinkValidator>, IComparable<BaseConfigLink>, IConfigAcceptVisitor, IBaseConfigLink // Class.tt Line: 7
    {
        #region CTOR
        public BaseConfigLink() : this(default(ITreeConfigNode))
        {
        }
        public BaseConfigLink(ITreeConfigNode parent) 
            : base(parent, BaseConfigLinkValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ConfigBase = new Config(this); // Class.tt Line: 33
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static BaseConfigLink Clone(ITreeConfigNode parent, IBaseConfigLink from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            BaseConfigLink vm = new BaseConfigLink(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.ConfigBase = vSharpStudio.vm.ViewModels.Config.Clone(vm, from.ConfigBase, isDeep);
            vm.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(BaseConfigLink to, IBaseConfigLink from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.Config.Update((Config)to.ConfigBase, from.ConfigBase, isDeep);
            to.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override BaseConfigLink Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return BaseConfigLink.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(BaseConfigLink from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            BaseConfigLink.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_base_config_link' to 'BaseConfigLink'
        public static BaseConfigLink ConvertToVM(Proto.Config.proto_base_config_link m, BaseConfigLink vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.ConfigBase == null) // Clone.tt Line: 213
                vm.ConfigBase = new Config(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.Config.ConvertToVM(m.ConfigBase, (Config)vm.ConfigBase); // Clone.tt Line: 219
            vm.RelativeConfigFilePath = m.RelativeConfigFilePath; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'BaseConfigLink' to 'proto_base_config_link'
        public static Proto.Config.proto_base_config_link ConvertToProto(BaseConfigLink vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_base_config_link m = new Proto.Config.proto_base_config_link(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.ConfigBase = vSharpStudio.vm.ViewModels.Config.ConvertToProto((Config)vm.ConfigBase); // Clone.tt Line: 270
            m.RelativeConfigFilePath = vm.RelativeConfigFilePath; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.ConfigBase.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public Config ConfigBase // Property.tt Line: 55
        { 
            get { return this._ConfigBase; }
            set
            {
                if (this._ConfigBase != value)
                {
                    this.OnConfigBaseChanging(ref value);
                    this._ConfigBase = value;
                    this.OnConfigBaseChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private Config _ConfigBase;
        IConfig IBaseConfigLink.ConfigBase { get { return (this as BaseConfigLink).ConfigBase; } } // Property.tt Line: 77
        partial void OnConfigBaseChanging(ref Config to); // Property.tt Line: 79
        partial void OnConfigBaseChanged();
        //IConfig IBaseConfigLink.ConfigBase { get { return this._ConfigBase; } }
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorFilePicker), typeof(ITypeEditor))]
        public string RelativeConfigFilePath // Property.tt Line: 55
        { 
            get { return this._RelativeConfigFilePath; }
            set
            {
                if (this._RelativeConfigFilePath != value)
                {
                    this.OnRelativeConfigFilePathChanging(ref value);
                    this._RelativeConfigFilePath = value;
                    this.OnRelativeConfigFilePathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _RelativeConfigFilePath = string.Empty;
        partial void OnRelativeConfigFilePathChanging(ref string to); // Property.tt Line: 79
        partial void OnRelativeConfigFilePathChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IBaseConfigLink.ListNodeGeneratorsSettings { get { return (this as BaseConfigLink).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    public partial class Config : ConfigObjectVmGenSettings<Config, ConfigValidator>, IComparable<Config>, IConfigAcceptVisitor, IConfig // Class.tt Line: 7
    {
        #region CTOR
        public Config() : this(default(ITreeConfigNode))
        {
        }
        public Config(ITreeConfigNode parent) 
            : base(parent, ConfigValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupConfigLinks = new GroupListBaseConfigLinks(); // Class.tt Line: 31
            this.Model = new Model(this); // Class.tt Line: 33
            this.GroupPlugins = new GroupListPlugins(); // Class.tt Line: 31
            this.GroupAppSolutions = new GroupListAppSolutions(); // Class.tt Line: 31
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static Config Clone(ITreeConfigNode parent, IConfig from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Config vm = new Config(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.Version = from.Version; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.LastUpdated = from.LastUpdated; // Clone.tt Line: 65
            vm.IsNeedCurrentUpdate = from.IsNeedCurrentUpdate; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupConfigLinks = vSharpStudio.vm.ViewModels.GroupListBaseConfigLinks.Clone(vm, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.Model = vSharpStudio.vm.ViewModels.Model.Clone(vm, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPlugins = vSharpStudio.vm.ViewModels.GroupListPlugins.Clone(vm, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupAppSolutions = vSharpStudio.vm.ViewModels.GroupListAppSolutions.Clone(vm, from.GroupAppSolutions, isDeep);
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Config to, IConfig from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.Version = from.Version; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.LastUpdated = from.LastUpdated; // Clone.tt Line: 141
            to.IsNeedCurrentUpdate = from.IsNeedCurrentUpdate; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListBaseConfigLinks.Update((GroupListBaseConfigLinks)to.GroupConfigLinks, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.Model.Update((Model)to.Model, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListPlugins.Update((GroupListPlugins)to.GroupPlugins, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListAppSolutions.Update((GroupListAppSolutions)to.GroupAppSolutions, from.GroupAppSolutions, isDeep);
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Config Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Config.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Config from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Config.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_config' to 'Config'
        public static Config ConvertToVM(Proto.Config.proto_config m, Config vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.Version = m.Version; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.LastUpdated = m.LastUpdated; // Clone.tt Line: 221
            vm.IsNeedCurrentUpdate = m.IsNeedCurrentUpdate; // Clone.tt Line: 221
            if (vm.GroupConfigLinks == null) // Clone.tt Line: 213
                vm.GroupConfigLinks = new GroupListBaseConfigLinks(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListBaseConfigLinks.ConvertToVM(m.GroupConfigLinks, (GroupListBaseConfigLinks)vm.GroupConfigLinks); // Clone.tt Line: 219
            if (vm.Model == null) // Clone.tt Line: 213
                vm.Model = new Model(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.Model.ConvertToVM(m.Model, (Model)vm.Model); // Clone.tt Line: 219
            if (vm.GroupPlugins == null) // Clone.tt Line: 213
                vm.GroupPlugins = new GroupListPlugins(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListPlugins.ConvertToVM(m.GroupPlugins, (GroupListPlugins)vm.GroupPlugins); // Clone.tt Line: 219
            if (vm.GroupAppSolutions == null) // Clone.tt Line: 213
                vm.GroupAppSolutions = new GroupListAppSolutions(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListAppSolutions.ConvertToVM(m.GroupAppSolutions, (GroupListAppSolutions)vm.GroupAppSolutions); // Clone.tt Line: 219
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Config' to 'proto_config'
        public static Proto.Config.proto_config ConvertToProto(Config vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_config m = new Proto.Config.proto_config(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.Version = vm.Version; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.LastUpdated = vm.LastUpdated; // Clone.tt Line: 276
            m.IsNeedCurrentUpdate = vm.IsNeedCurrentUpdate; // Clone.tt Line: 276
            m.GroupConfigLinks = vSharpStudio.vm.ViewModels.GroupListBaseConfigLinks.ConvertToProto((GroupListBaseConfigLinks)vm.GroupConfigLinks); // Clone.tt Line: 270
            m.Model = vSharpStudio.vm.ViewModels.Model.ConvertToProto((Model)vm.Model); // Clone.tt Line: 270
            m.GroupPlugins = vSharpStudio.vm.ViewModels.GroupListPlugins.ConvertToProto((GroupListPlugins)vm.GroupPlugins); // Clone.tt Line: 270
            m.GroupAppSolutions = vSharpStudio.vm.ViewModels.GroupListAppSolutions.ConvertToProto((GroupListAppSolutions)vm.GroupAppSolutions); // Clone.tt Line: 270
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupConfigLinks.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.Model.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupPlugins.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupAppSolutions.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [PropertyOrderAttribute(4)]
        [ReadOnly(true)]
        public int Version // Property.tt Line: 55
        { 
            get { return this._Version; }
            set
            {
                if (this._Version != value)
                {
                    this.OnVersionChanging(ref value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Version;
        partial void OnVersionChanging(ref int to); // Property.tt Line: 79
        partial void OnVersionChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(6)]
        public Google.Protobuf.WellKnownTypes.Timestamp LastUpdated // Property.tt Line: 55
        { 
            get { return this._LastUpdated; }
            set
            {
                if (this._LastUpdated != value)
                {
                    this.OnLastUpdatedChanging(ref value);
                    this._LastUpdated = value;
                    this.OnLastUpdatedChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private Google.Protobuf.WellKnownTypes.Timestamp _LastUpdated;
        partial void OnLastUpdatedChanging(ref Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 79
        partial void OnLastUpdatedChanged();
        //IGoogle.Protobuf.WellKnownTypes.Timestamp IConfig.LastUpdated { get { return this._LastUpdated; } }
        
        [BrowsableAttribute(false)]
        public bool IsNeedCurrentUpdate // Property.tt Line: 55
        { 
            get { return this._IsNeedCurrentUpdate; }
            set
            {
                if (this._IsNeedCurrentUpdate != value)
                {
                    this.OnIsNeedCurrentUpdateChanging(ref value);
                    this._IsNeedCurrentUpdate = value;
                    this.OnIsNeedCurrentUpdateChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNeedCurrentUpdate;
        partial void OnIsNeedCurrentUpdateChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNeedCurrentUpdateChanged();
        
        [BrowsableAttribute(false)]
        public GroupListBaseConfigLinks GroupConfigLinks // Property.tt Line: 55
        { 
            get { return this._GroupConfigLinks; }
            set
            {
                if (this._GroupConfigLinks != value)
                {
                    this.OnGroupConfigLinksChanging(ref value);
                    this._GroupConfigLinks = value;
                    this.OnGroupConfigLinksChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListBaseConfigLinks _GroupConfigLinks;
        IGroupListBaseConfigLinks IConfig.GroupConfigLinks { get { return (this as Config).GroupConfigLinks; } } // Property.tt Line: 77
        partial void OnGroupConfigLinksChanging(ref GroupListBaseConfigLinks to); // Property.tt Line: 79
        partial void OnGroupConfigLinksChanged();
        //IGroupListBaseConfigLinks IConfig.GroupConfigLinks { get { return this._GroupConfigLinks; } }
        
        [BrowsableAttribute(false)]
        public Model Model // Property.tt Line: 55
        { 
            get { return this._Model; }
            set
            {
                if (this._Model != value)
                {
                    this.OnModelChanging(ref value);
                    this._Model = value;
                    this.OnModelChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private Model _Model;
        IModel IConfig.Model { get { return (this as Config).Model; } } // Property.tt Line: 77
        partial void OnModelChanging(ref Model to); // Property.tt Line: 79
        partial void OnModelChanged();
        //IModel IConfig.Model { get { return this._Model; } }
        
        [BrowsableAttribute(false)]
        public GroupListPlugins GroupPlugins // Property.tt Line: 55
        { 
            get { return this._GroupPlugins; }
            set
            {
                if (this._GroupPlugins != value)
                {
                    this.OnGroupPluginsChanging(ref value);
                    this._GroupPlugins = value;
                    this.OnGroupPluginsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListPlugins _GroupPlugins;
        IGroupListPlugins IConfig.GroupPlugins { get { return (this as Config).GroupPlugins; } } // Property.tt Line: 77
        partial void OnGroupPluginsChanging(ref GroupListPlugins to); // Property.tt Line: 79
        partial void OnGroupPluginsChanged();
        //IGroupListPlugins IConfig.GroupPlugins { get { return this._GroupPlugins; } }
        
        [BrowsableAttribute(false)]
        public GroupListAppSolutions GroupAppSolutions // Property.tt Line: 55
        { 
            get { return this._GroupAppSolutions; }
            set
            {
                if (this._GroupAppSolutions != value)
                {
                    this.OnGroupAppSolutionsChanging(ref value);
                    this._GroupAppSolutions = value;
                    this.OnGroupAppSolutionsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListAppSolutions _GroupAppSolutions;
        IGroupListAppSolutions IConfig.GroupAppSolutions { get { return (this as Config).GroupAppSolutions; } } // Property.tt Line: 77
        partial void OnGroupAppSolutionsChanging(ref GroupListAppSolutions to); // Property.tt Line: 79
        partial void OnGroupAppSolutionsChanged();
        //IGroupListAppSolutions IConfig.GroupAppSolutions { get { return this._GroupAppSolutions; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class AppDbSettingsValidator : ValidatorBase<AppDbSettings, AppDbSettingsValidator> { } // Class.tt Line: 6
    public partial class AppDbSettings : VmValidatableWithSeverity<AppDbSettings, AppDbSettingsValidator>, IAppDbSettings // Class.tt Line: 7
    {
        #region CTOR
        public AppDbSettings() 
            : base(AppDbSettingsValidator.Validator) // Class.tt Line: 45
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
        public static AppDbSettings Clone(IAppDbSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppDbSettings vm = new AppDbSettings();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 65
            vm.PluginName = from.PluginName; // Clone.tt Line: 65
            vm.Version = from.Version; // Clone.tt Line: 65
            vm.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 65
            vm.PluginGenName = from.PluginGenName; // Clone.tt Line: 65
            vm.ConnGuid = from.ConnGuid; // Clone.tt Line: 65
            vm.ConnName = from.ConnName; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppDbSettings to, IAppDbSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.PluginGuid = from.PluginGuid; // Clone.tt Line: 141
            to.PluginName = from.PluginName; // Clone.tt Line: 141
            to.Version = from.Version; // Clone.tt Line: 141
            to.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 141
            to.PluginGenName = from.PluginGenName; // Clone.tt Line: 141
            to.ConnGuid = from.ConnGuid; // Clone.tt Line: 141
            to.ConnName = from.ConnName; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override AppDbSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return AppDbSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(AppDbSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            AppDbSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_app_db_settings' to 'AppDbSettings'
        public static AppDbSettings ConvertToVM(Proto.Config.proto_app_db_settings m, AppDbSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 221
            vm.PluginName = m.PluginName; // Clone.tt Line: 221
            vm.Version = m.Version; // Clone.tt Line: 221
            vm.PluginGenGuid = m.PluginGenGuid; // Clone.tt Line: 221
            vm.PluginGenName = m.PluginGenName; // Clone.tt Line: 221
            vm.ConnGuid = m.ConnGuid; // Clone.tt Line: 221
            vm.ConnName = m.ConnName; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'AppDbSettings' to 'proto_app_db_settings'
        public static Proto.Config.proto_app_db_settings ConvertToProto(AppDbSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_app_db_settings m = new Proto.Config.proto_app_db_settings(); // Clone.tt Line: 239
            m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 276
            m.PluginName = vm.PluginName; // Clone.tt Line: 276
            m.Version = vm.Version; // Clone.tt Line: 276
            m.PluginGenGuid = vm.PluginGenGuid; // Clone.tt Line: 276
            m.PluginGenName = vm.PluginGenName; // Clone.tt Line: 276
            m.ConnGuid = vm.ConnGuid; // Clone.tt Line: 276
            m.ConnName = vm.ConnName; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [PropertyOrderAttribute(1)]
        [Editor(typeof(EditorDbPluginSelection), typeof(EditorDbPluginSelection))]
        [Description("Default DB Plugin")]
        public string PluginGuid // Property.tt Line: 55
        { 
            get { return this._PluginGuid; }
            set
            {
                if (this._PluginGuid != value)
                {
                    this.OnPluginGuidChanging(ref value);
                    this._PluginGuid = value;
                    this.OnPluginGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PluginGuid = string.Empty;
        partial void OnPluginGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPluginGuidChanged();
        
        [PropertyOrderAttribute(2)]
        [ReadOnly(true)]
        public string PluginName // Property.tt Line: 55
        { 
            get { return this._PluginName; }
            set
            {
                if (this._PluginName != value)
                {
                    this.OnPluginNameChanging(ref value);
                    this._PluginName = value;
                    this.OnPluginNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PluginName = string.Empty;
        partial void OnPluginNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPluginNameChanged();
        
        [PropertyOrderAttribute(3)]
        [ReadOnly(true)]
        public string Version // Property.tt Line: 55
        { 
            get { return this._Version; }
            set
            {
                if (this._Version != value)
                {
                    this.OnVersionChanging(ref value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Version = string.Empty;
        partial void OnVersionChanging(ref string to); // Property.tt Line: 79
        partial void OnVersionChanged();
        
        [PropertyOrderAttribute(4)]
        [Editor(typeof(EditorDbPluginGenSelection), typeof(EditorDbPluginGenSelection))]
        [Description("Default DB Plugin generator")]
        public string PluginGenGuid // Property.tt Line: 55
        { 
            get { return this._PluginGenGuid; }
            set
            {
                if (this._PluginGenGuid != value)
                {
                    this.OnPluginGenGuidChanging(ref value);
                    this._PluginGenGuid = value;
                    this.OnPluginGenGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PluginGenGuid = string.Empty;
        partial void OnPluginGenGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPluginGenGuidChanged();
        
        [PropertyOrderAttribute(5)]
        [ReadOnly(true)]
        public string PluginGenName // Property.tt Line: 55
        { 
            get { return this._PluginGenName; }
            set
            {
                if (this._PluginGenName != value)
                {
                    this.OnPluginGenNameChanging(ref value);
                    this._PluginGenName = value;
                    this.OnPluginGenNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PluginGenName = string.Empty;
        partial void OnPluginGenNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPluginGenNameChanged();
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorDbConnSelection), typeof(EditorDbConnSelection))]
        [Description("Default DB connection string")]
        public string ConnGuid // Property.tt Line: 55
        { 
            get { return this._ConnGuid; }
            set
            {
                if (this._ConnGuid != value)
                {
                    this.OnConnGuidChanging(ref value);
                    this._ConnGuid = value;
                    this.OnConnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _ConnGuid = string.Empty;
        partial void OnConnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnConnGuidChanged();
        
        [PropertyOrderAttribute(7)]
        [ReadOnly(true)]
        public string ConnName // Property.tt Line: 55
        { 
            get { return this._ConnName; }
            set
            {
                if (this._ConnName != value)
                {
                    this.OnConnNameChanging(ref value);
                    this._ConnName = value;
                    this.OnConnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _ConnName = string.Empty;
        partial void OnConnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnConnNameChanged();
        #endregion Properties
    }
    public partial class PluginGroupGeneratorsDefaultSettingsValidator : ValidatorBase<PluginGroupGeneratorsDefaultSettings, PluginGroupGeneratorsDefaultSettingsValidator> { } // Class.tt Line: 6
    public partial class PluginGroupGeneratorsDefaultSettings : VmValidatableWithSeverity<PluginGroupGeneratorsDefaultSettings, PluginGroupGeneratorsDefaultSettingsValidator>, IPluginGroupGeneratorsDefaultSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGroupGeneratorsDefaultSettings() 
            : base(PluginGroupGeneratorsDefaultSettingsValidator.Validator) // Class.tt Line: 45
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
        public static PluginGroupGeneratorsDefaultSettings Clone(IPluginGroupGeneratorsDefaultSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGroupGeneratorsDefaultSettings vm = new PluginGroupGeneratorsDefaultSettings();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGroupGeneratorsDefaultSettings to, IPluginGroupGeneratorsDefaultSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGroupGeneratorsDefaultSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGroupGeneratorsDefaultSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginGroupGeneratorsDefaultSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginGroupGeneratorsDefaultSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin_group_generators_default_settings' to 'PluginGroupGeneratorsDefaultSettings'
        public static PluginGroupGeneratorsDefaultSettings ConvertToVM(Proto.Config.proto_plugin_group_generators_default_settings m, PluginGroupGeneratorsDefaultSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.AppGroupGeneratorsGuid = m.AppGroupGeneratorsGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGroupGeneratorsDefaultSettings' to 'proto_plugin_group_generators_default_settings'
        public static Proto.Config.proto_plugin_group_generators_default_settings ConvertToProto(PluginGroupGeneratorsDefaultSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_group_generators_default_settings m = new Proto.Config.proto_plugin_group_generators_default_settings(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.AppGroupGeneratorsGuid = vm.AppGroupGeneratorsGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Guid = string.Empty;
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        
        ///////////////////////////////////////////////////
        /// Guid of group generators
        ///////////////////////////////////////////////////
        public string AppGroupGeneratorsGuid // Property.tt Line: 55
        { 
            get { return this._AppGroupGeneratorsGuid; }
            set
            {
                if (this._AppGroupGeneratorsGuid != value)
                {
                    this.OnAppGroupGeneratorsGuidChanging(ref value);
                    this._AppGroupGeneratorsGuid = value;
                    this.OnAppGroupGeneratorsGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _AppGroupGeneratorsGuid = string.Empty;
        partial void OnAppGroupGeneratorsGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnAppGroupGeneratorsGuidChanged();
        
        public string Settings // Property.tt Line: 55
        { 
            get { return this._Settings; }
            set
            {
                if (this._Settings != value)
                {
                    this.OnSettingsChanging(ref value);
                    this._Settings = value;
                    this.OnSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Settings = string.Empty;
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 79
        partial void OnSettingsChanged();
        #endregion Properties
    }
    public partial class GroupListAppSolutionsValidator : ValidatorBase<GroupListAppSolutions, GroupListAppSolutionsValidator> { } // Class.tt Line: 6
    public partial class GroupListAppSolutions : ConfigObjectCommonBase<GroupListAppSolutions, GroupListAppSolutionsValidator>, IComparable<GroupListAppSolutions>, IConfigAcceptVisitor, IGroupListAppSolutions // Class.tt Line: 7
    {
        #region CTOR
        public GroupListAppSolutions() : this(default(ITreeConfigNode))
        {
        }
        public GroupListAppSolutions(ITreeConfigNode parent) 
            : base(parent, GroupListAppSolutionsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListAppSolutions = new ConfigNodesCollection<AppSolution>(this); // Class.tt Line: 27
            this.ListGroupGeneratorsDefultSettings = new ObservableCollection<PluginGroupGeneratorsDefaultSettings>(); // Class.tt Line: 25
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(AppSolution)) // Clone.tt Line: 15
            {
                this.ListAppSolutions.Sort();
            }
        }
        public static GroupListAppSolutions Clone(ITreeConfigNode parent, IGroupListAppSolutions from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListAppSolutions vm = new GroupListAppSolutions(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListAppSolutions) // Clone.tt Line: 52
                vm.ListAppSolutions.Add(AppSolution.Clone(vm, (AppSolution)t, isDeep));
            vm.ListGroupGeneratorsDefultSettings = new ObservableCollection<PluginGroupGeneratorsDefaultSettings>(); // Clone.tt Line: 47
            foreach (var t in from.ListGroupGeneratorsDefultSettings) // Clone.tt Line: 48
                vm.ListGroupGeneratorsDefultSettings.Add(PluginGroupGeneratorsDefaultSettings.Clone((PluginGroupGeneratorsDefaultSettings)t, isDeep));
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListAppSolutions to, IGroupListAppSolutions from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListAppSolutions.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListAppSolutions)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            AppSolution.Update((AppSolution)t, (AppSolution)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListAppSolutions.Remove(t);
                }
                foreach (var tt in from.ListAppSolutions)
                {
                    bool isfound = false;
                    foreach (var t in to.ListAppSolutions.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new AppSolution(to); // Clone.tt Line: 117
                        AppSolution.Update(p, (AppSolution)tt, isDeep);
                        to.ListAppSolutions.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListGroupGeneratorsDefultSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGroupGeneratorsDefultSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGroupGeneratorsDefaultSettings.Update((PluginGroupGeneratorsDefaultSettings)t, (PluginGroupGeneratorsDefaultSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGroupGeneratorsDefultSettings.Remove(t);
                }
                foreach (var tt in from.ListGroupGeneratorsDefultSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGroupGeneratorsDefultSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGroupGeneratorsDefaultSettings(); // Clone.tt Line: 119
                        PluginGroupGeneratorsDefaultSettings.Update(p, (PluginGroupGeneratorsDefaultSettings)tt, isDeep);
                        to.ListGroupGeneratorsDefultSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListAppSolutions Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListAppSolutions.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListAppSolutions from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListAppSolutions.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_app_solutions' to 'GroupListAppSolutions'
        public static GroupListAppSolutions ConvertToVM(Proto.Config.proto_group_list_app_solutions m, GroupListAppSolutions vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListAppSolutions) // Clone.tt Line: 201
            {
                var tvm = AppSolution.ConvertToVM(t, new AppSolution(vm)); // Clone.tt Line: 204
                vm.ListAppSolutions.Add(tvm);
            }
            vm.ListGroupGeneratorsDefultSettings = new ObservableCollection<PluginGroupGeneratorsDefaultSettings>(); // Clone.tt Line: 190
            foreach (var t in m.ListGroupGeneratorsDefultSettings) // Clone.tt Line: 191
            {
                var tvm = PluginGroupGeneratorsDefaultSettings.ConvertToVM(t, new PluginGroupGeneratorsDefaultSettings()); // Clone.tt Line: 196
                vm.ListGroupGeneratorsDefultSettings.Add(tvm);
            }
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListAppSolutions' to 'proto_group_list_app_solutions'
        public static Proto.Config.proto_group_list_app_solutions ConvertToProto(GroupListAppSolutions vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_app_solutions m = new Proto.Config.proto_group_list_app_solutions(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListAppSolutions) // Clone.tt Line: 242
                m.ListAppSolutions.Add(AppSolution.ConvertToProto((AppSolution)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListGroupGeneratorsDefultSettings) // Clone.tt Line: 242
                m.ListGroupGeneratorsDefultSettings.Add(PluginGroupGeneratorsDefaultSettings.ConvertToProto((PluginGroupGeneratorsDefaultSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListAppSolutions)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListGroupGeneratorsDefultSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// List NET solutions
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppSolution> ListAppSolutions // Property.tt Line: 8
        { 
            get { return this._ListAppSolutions; }
            set
            {
                if (this._ListAppSolutions != value)
                {
                    this.OnListAppSolutionsChanging(value);
                    _ListAppSolutions = value;
                    this.OnListAppSolutionsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppSolution> _ListAppSolutions;
        IReadOnlyList<IAppSolution> IGroupListAppSolutions.ListAppSolutions { get { return (this as GroupListAppSolutions).ListAppSolutions; } } // Property.tt Line: 26
        partial void OnListAppSolutionsChanging(ObservableCollection<AppSolution> to); // Property.tt Line: 27
        partial void OnListAppSolutionsChanged();
        public AppSolution this[int index] { get { return (AppSolution)this.ListAppSolutions[index]; } }
        IAppSolution IGroupListAppSolutions.this[int index] { get { return (AppSolution)this.ListAppSolutions[index]; } }
        public void Add(AppSolution item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListAppSolutions.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<AppSolution> items) 
        { 
            Contract.Requires(items != null);
            this.ListAppSolutions.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListAppSolutions.Count; }
        int IGroupListAppSolutions.Count() { return this.Count(); }
        public void Remove(AppSolution item) 
        {
            Contract.Requires(item != null);
            this.ListAppSolutions.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ObservableCollection<PluginGroupGeneratorsDefaultSettings> ListGroupGeneratorsDefultSettings // Property.tt Line: 8
        { 
            get { return this._ListGroupGeneratorsDefultSettings; }
            set
            {
                if (this._ListGroupGeneratorsDefultSettings != value)
                {
                    this.OnListGroupGeneratorsDefultSettingsChanging(value);
                    _ListGroupGeneratorsDefultSettings = value;
                    this.OnListGroupGeneratorsDefultSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ObservableCollection<PluginGroupGeneratorsDefaultSettings> _ListGroupGeneratorsDefultSettings;
        IReadOnlyList<IPluginGroupGeneratorsDefaultSettings> IGroupListAppSolutions.ListGroupGeneratorsDefultSettings { get { return (this as GroupListAppSolutions).ListGroupGeneratorsDefultSettings; } } // Property.tt Line: 26
        partial void OnListGroupGeneratorsDefultSettingsChanging(ObservableCollection<PluginGroupGeneratorsDefaultSettings> to); // Property.tt Line: 27
        partial void OnListGroupGeneratorsDefultSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class PluginGroupGeneratorsSettingsValidator : ValidatorBase<PluginGroupGeneratorsSettings, PluginGroupGeneratorsSettingsValidator> { } // Class.tt Line: 6
    public partial class PluginGroupGeneratorsSettings : ConfigObjectCommonBase<PluginGroupGeneratorsSettings, PluginGroupGeneratorsSettingsValidator>, IComparable<PluginGroupGeneratorsSettings>, IConfigAcceptVisitor, IPluginGroupGeneratorsSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGroupGeneratorsSettings() : this(default(ITreeConfigNode))
        {
        }
        public PluginGroupGeneratorsSettings(ITreeConfigNode parent) 
            : base(parent, PluginGroupGeneratorsSettingsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGroupGeneratorsSettings Clone(ITreeConfigNode parent, IPluginGroupGeneratorsSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGroupGeneratorsSettings vm = new PluginGroupGeneratorsSettings(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGroupGeneratorsSettings to, IPluginGroupGeneratorsSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGroupGeneratorsSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGroupGeneratorsSettings.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginGroupGeneratorsSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginGroupGeneratorsSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin_group_generators_settings' to 'PluginGroupGeneratorsSettings'
        public static PluginGroupGeneratorsSettings ConvertToVM(Proto.Config.proto_plugin_group_generators_settings m, PluginGroupGeneratorsSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.AppGroupGeneratorsGuid = m.AppGroupGeneratorsGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGroupGeneratorsSettings' to 'proto_plugin_group_generators_settings'
        public static Proto.Config.proto_plugin_group_generators_settings ConvertToProto(PluginGroupGeneratorsSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_group_generators_settings m = new Proto.Config.proto_plugin_group_generators_settings(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.AppGroupGeneratorsGuid = vm.AppGroupGeneratorsGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [ReadOnly(true)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        public string AppGroupGeneratorsGuid // Property.tt Line: 55
        { 
            get { return this._AppGroupGeneratorsGuid; }
            set
            {
                if (this._AppGroupGeneratorsGuid != value)
                {
                    this.OnAppGroupGeneratorsGuidChanging(ref value);
                    this._AppGroupGeneratorsGuid = value;
                    this.OnAppGroupGeneratorsGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _AppGroupGeneratorsGuid = string.Empty;
        partial void OnAppGroupGeneratorsGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnAppGroupGeneratorsGuidChanged();
        
        public string Settings // Property.tt Line: 55
        { 
            get { return this._Settings; }
            set
            {
                if (this._Settings != value)
                {
                    this.OnSettingsChanging(ref value);
                    this._Settings = value;
                    this.OnSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Settings = string.Empty;
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 79
        partial void OnSettingsChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class AppSolutionValidator : ValidatorBase<AppSolution, AppSolutionValidator> { } // Class.tt Line: 6
    public partial class AppSolution : ConfigObjectCommonBase<AppSolution, AppSolutionValidator>, IComparable<AppSolution>, IConfigAcceptVisitor, IAppSolution // Class.tt Line: 7
    {
        #region CTOR
        public AppSolution() : this(default(ITreeConfigNode))
        {
        }
        public AppSolution(ITreeConfigNode parent) 
            : base(parent, AppSolutionValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListAppProjects = new ConfigNodesCollection<AppProject>(this); // Class.tt Line: 27
            this.ListGroupGeneratorsSettings = new ConfigNodesCollection<PluginGroupGeneratorsSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(AppProject)) // Clone.tt Line: 15
            {
                this.ListAppProjects.Sort();
            }
            if (type == typeof(PluginGroupGeneratorsSettings)) // Clone.tt Line: 15
            {
                this.ListGroupGeneratorsSettings.Sort();
            }
        }
        public static AppSolution Clone(ITreeConfigNode parent, IAppSolution from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppSolution vm = new AppSolution(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListAppProjects) // Clone.tt Line: 52
                vm.ListAppProjects.Add(AppProject.Clone(vm, (AppProject)t, isDeep));
            vm.ListGroupGeneratorsSettings = new ConfigNodesCollection<PluginGroupGeneratorsSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListGroupGeneratorsSettings) // Clone.tt Line: 52
                vm.ListGroupGeneratorsSettings.Add(PluginGroupGeneratorsSettings.Clone(vm, (PluginGroupGeneratorsSettings)t, isDeep));
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppSolution to, IAppSolution from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListAppProjects.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListAppProjects)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            AppProject.Update((AppProject)t, (AppProject)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListAppProjects.Remove(t);
                }
                foreach (var tt in from.ListAppProjects)
                {
                    bool isfound = false;
                    foreach (var t in to.ListAppProjects.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new AppProject(to); // Clone.tt Line: 117
                        AppProject.Update(p, (AppProject)tt, isDeep);
                        to.ListAppProjects.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListGroupGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGroupGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGroupGeneratorsSettings.Update((PluginGroupGeneratorsSettings)t, (PluginGroupGeneratorsSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGroupGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGroupGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGroupGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGroupGeneratorsSettings(to); // Clone.tt Line: 117
                        PluginGroupGeneratorsSettings.Update(p, (PluginGroupGeneratorsSettings)tt, isDeep);
                        to.ListGroupGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override AppSolution Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return AppSolution.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(AppSolution from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            AppSolution.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_app_solution' to 'AppSolution'
        public static AppSolution ConvertToVM(Proto.Config.proto_app_solution m, AppSolution vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.RelativeAppSolutionPath = m.RelativeAppSolutionPath; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListAppProjects) // Clone.tt Line: 201
            {
                var tvm = AppProject.ConvertToVM(t, new AppProject(vm)); // Clone.tt Line: 204
                vm.ListAppProjects.Add(tvm);
            }
            vm.ListGroupGeneratorsSettings = new ConfigNodesCollection<PluginGroupGeneratorsSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListGroupGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGroupGeneratorsSettings.ConvertToVM(t, new PluginGroupGeneratorsSettings(vm)); // Clone.tt Line: 204
                vm.ListGroupGeneratorsSettings.Add(tvm);
            }
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'AppSolution' to 'proto_app_solution'
        public static Proto.Config.proto_app_solution ConvertToProto(AppSolution vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_app_solution m = new Proto.Config.proto_app_solution(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.RelativeAppSolutionPath = vm.RelativeAppSolutionPath; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListAppProjects) // Clone.tt Line: 242
                m.ListAppProjects.Add(AppProject.ConvertToProto((AppProject)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListGroupGeneratorsSettings) // Clone.tt Line: 242
                m.ListGroupGeneratorsSettings.Add(PluginGroupGeneratorsSettings.ConvertToProto((PluginGroupGeneratorsSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListAppProjects)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListGroupGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// List NET projects
        /// App solution relative path to configuration file path
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(6)]
        [DisplayName("Path")]
        [Editor(typeof(EditorSolutionPicker), typeof(ITypeEditor))]
        [Description(".NET solution file path relative to configuration file path")]
        public string RelativeAppSolutionPath // Property.tt Line: 55
        { 
            get { return this._RelativeAppSolutionPath; }
            set
            {
                if (this._RelativeAppSolutionPath != value)
                {
                    this.OnRelativeAppSolutionPathChanging(ref value);
                    this._RelativeAppSolutionPath = value;
                    this.OnRelativeAppSolutionPathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _RelativeAppSolutionPath = string.Empty;
        partial void OnRelativeAppSolutionPathChanging(ref string to); // Property.tt Line: 79
        partial void OnRelativeAppSolutionPathChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProject> ListAppProjects // Property.tt Line: 8
        { 
            get { return this._ListAppProjects; }
            set
            {
                if (this._ListAppProjects != value)
                {
                    this.OnListAppProjectsChanging(value);
                    _ListAppProjects = value;
                    this.OnListAppProjectsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppProject> _ListAppProjects;
        IReadOnlyList<IAppProject> IAppSolution.ListAppProjects { get { return (this as AppSolution).ListAppProjects; } } // Property.tt Line: 26
        partial void OnListAppProjectsChanging(ObservableCollection<AppProject> to); // Property.tt Line: 27
        partial void OnListAppProjectsChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGroupGeneratorsSettings> ListGroupGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListGroupGeneratorsSettings; }
            set
            {
                if (this._ListGroupGeneratorsSettings != value)
                {
                    this.OnListGroupGeneratorsSettingsChanging(value);
                    _ListGroupGeneratorsSettings = value;
                    this.OnListGroupGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGroupGeneratorsSettings> _ListGroupGeneratorsSettings;
        IReadOnlyList<IPluginGroupGeneratorsSettings> IAppSolution.ListGroupGeneratorsSettings { get { return (this as AppSolution).ListGroupGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListGroupGeneratorsSettingsChanging(ObservableCollection<PluginGroupGeneratorsSettings> to); // Property.tt Line: 27
        partial void OnListGroupGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class AppProjectValidator : ValidatorBase<AppProject, AppProjectValidator> { } // Class.tt Line: 6
    public partial class AppProject : ConfigObjectCommonBase<AppProject, AppProjectValidator>, IComparable<AppProject>, IConfigAcceptVisitor, IAppProject // Class.tt Line: 7
    {
        #region CTOR
        public AppProject() : this(default(ITreeConfigNode))
        {
        }
        public AppProject(ITreeConfigNode parent) 
            : base(parent, AppProjectValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(AppProjectGenerator)) // Clone.tt Line: 15
            {
                this.ListAppProjectGenerators.Sort();
            }
        }
        public static AppProject Clone(ITreeConfigNode parent, IAppProject from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppProject vm = new AppProject(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.Namespace = from.Namespace; // Clone.tt Line: 65
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListAppProjectGenerators) // Clone.tt Line: 52
                vm.ListAppProjectGenerators.Add(AppProjectGenerator.Clone(vm, (AppProjectGenerator)t, isDeep));
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppProject to, IAppProject from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.Namespace = from.Namespace; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListAppProjectGenerators.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListAppProjectGenerators)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            AppProjectGenerator.Update((AppProjectGenerator)t, (AppProjectGenerator)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListAppProjectGenerators.Remove(t);
                }
                foreach (var tt in from.ListAppProjectGenerators)
                {
                    bool isfound = false;
                    foreach (var t in to.ListAppProjectGenerators.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new AppProjectGenerator(to); // Clone.tt Line: 117
                        AppProjectGenerator.Update(p, (AppProjectGenerator)tt, isDeep);
                        to.ListAppProjectGenerators.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override AppProject Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return AppProject.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(AppProject from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            AppProject.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_app_project' to 'AppProject'
        public static AppProject ConvertToVM(Proto.Config.proto_app_project m, AppProject vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.RelativeAppProjectPath = m.RelativeAppProjectPath; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.Namespace = m.Namespace; // Clone.tt Line: 221
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListAppProjectGenerators) // Clone.tt Line: 201
            {
                var tvm = AppProjectGenerator.ConvertToVM(t, new AppProjectGenerator(vm)); // Clone.tt Line: 204
                vm.ListAppProjectGenerators.Add(tvm);
            }
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'AppProject' to 'proto_app_project'
        public static Proto.Config.proto_app_project ConvertToProto(AppProject vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_app_project m = new Proto.Config.proto_app_project(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.RelativeAppProjectPath = vm.RelativeAppProjectPath; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.Namespace = vm.Namespace; // Clone.tt Line: 276
            foreach (var t in vm.ListAppProjectGenerators) // Clone.tt Line: 242
                m.ListAppProjectGenerators.Add(AppProjectGenerator.ConvertToProto((AppProjectGenerator)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListAppProjectGenerators)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// App project relative path to .net solution file path
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorProjectPicker), typeof(ITypeEditor))]
        [Description(".NET project file path relative to solution file path")]
        public string RelativeAppProjectPath // Property.tt Line: 55
        { 
            get { return this._RelativeAppProjectPath; }
            set
            {
                if (this._RelativeAppProjectPath != value)
                {
                    this.OnRelativeAppProjectPathChanging(ref value);
                    this._RelativeAppProjectPath = value;
                    this.OnRelativeAppProjectPathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _RelativeAppProjectPath = string.Empty;
        partial void OnRelativeAppProjectPathChanging(ref string to); // Property.tt Line: 79
        partial void OnRelativeAppProjectPathChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [PropertyOrderAttribute(9)]
        [DisplayName("Namespace")]
        [Description("Project namespace name")]
        public string Namespace // Property.tt Line: 55
        { 
            get { return this._Namespace; }
            set
            {
                if (this._Namespace != value)
                {
                    this.OnNamespaceChanging(ref value);
                    this._Namespace = value;
                    this.OnNamespaceChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Namespace = string.Empty;
        partial void OnNamespaceChanging(ref string to); // Property.tt Line: 79
        partial void OnNamespaceChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProjectGenerator> ListAppProjectGenerators // Property.tt Line: 8
        { 
            get { return this._ListAppProjectGenerators; }
            set
            {
                if (this._ListAppProjectGenerators != value)
                {
                    this.OnListAppProjectGeneratorsChanging(value);
                    _ListAppProjectGenerators = value;
                    this.OnListAppProjectGeneratorsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppProjectGenerator> _ListAppProjectGenerators;
        IReadOnlyList<IAppProjectGenerator> IAppProject.ListAppProjectGenerators { get { return (this as AppProject).ListAppProjectGenerators; } } // Property.tt Line: 26
        partial void OnListAppProjectGeneratorsChanging(ObservableCollection<AppProjectGenerator> to); // Property.tt Line: 27
        partial void OnListAppProjectGeneratorsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorNodeSettingsValidator : ValidatorBase<PluginGeneratorNodeSettings, PluginGeneratorNodeSettingsValidator> { } // Class.tt Line: 6
    public partial class PluginGeneratorNodeSettings : ConfigObjectCommonBase<PluginGeneratorNodeSettings, PluginGeneratorNodeSettingsValidator>, IComparable<PluginGeneratorNodeSettings>, IConfigAcceptVisitor, IPluginGeneratorNodeSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGeneratorNodeSettings() : this(default(ITreeConfigNode))
        {
        }
        public PluginGeneratorNodeSettings(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorNodeSettingsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGeneratorNodeSettings Clone(ITreeConfigNode parent, IPluginGeneratorNodeSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGeneratorNodeSettings vm = new PluginGeneratorNodeSettings(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGeneratorNodeSettings to, IPluginGeneratorNodeSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGeneratorNodeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGeneratorNodeSettings.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginGeneratorNodeSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginGeneratorNodeSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin_generator_node_settings' to 'PluginGeneratorNodeSettings'
        public static PluginGeneratorNodeSettings ConvertToVM(Proto.Config.proto_plugin_generator_node_settings m, PluginGeneratorNodeSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.AppProjectGeneratorGuid = m.AppProjectGeneratorGuid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGeneratorNodeSettings' to 'proto_plugin_generator_node_settings'
        public static Proto.Config.proto_plugin_generator_node_settings ConvertToProto(PluginGeneratorNodeSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_generator_node_settings m = new Proto.Config.proto_plugin_generator_node_settings(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.AppProjectGeneratorGuid = vm.AppProjectGeneratorGuid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        public string AppProjectGeneratorGuid // Property.tt Line: 55
        { 
            get { return this._AppProjectGeneratorGuid; }
            set
            {
                if (this._AppProjectGeneratorGuid != value)
                {
                    this.OnAppProjectGeneratorGuidChanging(ref value);
                    this._AppProjectGeneratorGuid = value;
                    this.OnAppProjectGeneratorGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _AppProjectGeneratorGuid = string.Empty;
        partial void OnAppProjectGeneratorGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnAppProjectGeneratorGuidChanged();
        
        
        ///////////////////////////////////////////////////
        /// Name of solution-project-generator node
        ///////////////////////////////////////////////////
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        
        ///////////////////////////////////////////////////
        /// string node_settings_vm_guid = 6;
        ///////////////////////////////////////////////////
        public string Settings // Property.tt Line: 55
        { 
            get { return this._Settings; }
            set
            {
                if (this._Settings != value)
                {
                    this.OnSettingsChanging(ref value);
                    this._Settings = value;
                    this.OnSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Settings = string.Empty;
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 79
        partial void OnSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorSettingsValidator : ValidatorBase<PluginGeneratorSettings, PluginGeneratorSettingsValidator> { } // Class.tt Line: 6
    public partial class PluginGeneratorSettings : VmValidatableWithSeverity<PluginGeneratorSettings, PluginGeneratorSettingsValidator>, IPluginGeneratorSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGeneratorSettings() 
            : base(PluginGeneratorSettingsValidator.Validator) // Class.tt Line: 45
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
        public static PluginGeneratorSettings Clone(IPluginGeneratorSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGeneratorSettings vm = new PluginGeneratorSettings();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGeneratorSettings to, IPluginGeneratorSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGeneratorSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGeneratorSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginGeneratorSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginGeneratorSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin_generator_settings' to 'PluginGeneratorSettings'
        public static PluginGeneratorSettings ConvertToVM(Proto.Config.proto_plugin_generator_settings m, PluginGeneratorSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.AppProjectGeneratorGuid = m.AppProjectGeneratorGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGeneratorSettings' to 'proto_plugin_generator_settings'
        public static Proto.Config.proto_plugin_generator_settings ConvertToProto(PluginGeneratorSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_generator_settings m = new Proto.Config.proto_plugin_generator_settings(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.AppProjectGeneratorGuid = vm.AppProjectGeneratorGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Guid = string.Empty;
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Name = string.Empty;
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [PropertyOrderAttribute(2)]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _NameUi = string.Empty;
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        public string AppProjectGeneratorGuid // Property.tt Line: 55
        { 
            get { return this._AppProjectGeneratorGuid; }
            set
            {
                if (this._AppProjectGeneratorGuid != value)
                {
                    this.OnAppProjectGeneratorGuidChanging(ref value);
                    this._AppProjectGeneratorGuid = value;
                    this.OnAppProjectGeneratorGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _AppProjectGeneratorGuid = string.Empty;
        partial void OnAppProjectGeneratorGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnAppProjectGeneratorGuidChanged();
        
        public string Settings // Property.tt Line: 55
        { 
            get { return this._Settings; }
            set
            {
                if (this._Settings != value)
                {
                    this.OnSettingsChanging(ref value);
                    this._Settings = value;
                    this.OnSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Settings = string.Empty;
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 79
        partial void OnSettingsChanged();
        #endregion Properties
    }
    public partial class AppProjectGeneratorValidator : ValidatorBase<AppProjectGenerator, AppProjectGeneratorValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Application project generator
    ///////////////////////////////////////////////////
    public partial class AppProjectGenerator : ConfigObjectCommonBase<AppProjectGenerator, AppProjectGeneratorValidator>, IComparable<AppProjectGenerator>, IConfigAcceptVisitor, IAppProjectGenerator // Class.tt Line: 7
    {
        #region CTOR
        public AppProjectGenerator() : this(default(ITreeConfigNode))
        {
        }
        public AppProjectGenerator(ITreeConfigNode parent) 
            : base(parent, AppProjectGeneratorValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GeneratorSettingsVm = new PluginGeneratorSettings(); // Class.tt Line: 31
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static AppProjectGenerator Clone(ITreeConfigNode parent, IAppProjectGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppProjectGenerator vm = new AppProjectGenerator(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 65
            vm.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 65
            vm.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 65
            vm.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 65
            vm.RelativePathToGenFolder = from.RelativePathToGenFolder; // Clone.tt Line: 65
            vm.GenFileName = from.GenFileName; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GeneratorSettingsVm = vSharpStudio.vm.ViewModels.PluginGeneratorSettings.Clone(from.GeneratorSettingsVm, isDeep);
            vm.ConnStr = from.ConnStr; // Clone.tt Line: 65
            vm.PluginGroupSettingsGuid = from.PluginGroupSettingsGuid; // Clone.tt Line: 65
            vm.ConnStrToPrevStable = from.ConnStrToPrevStable; // Clone.tt Line: 65
            vm.IsGenerateSqlSqriptToUpdatePrevStable = from.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 65
            vm.GenScriptFileName = from.GenScriptFileName; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppProjectGenerator to, IAppProjectGenerator from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.PluginGuid = from.PluginGuid; // Clone.tt Line: 141
            to.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 141
            to.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 141
            to.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 141
            to.RelativePathToGenFolder = from.RelativePathToGenFolder; // Clone.tt Line: 141
            to.GenFileName = from.GenFileName; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.PluginGeneratorSettings.Update((PluginGeneratorSettings)to.GeneratorSettingsVm, from.GeneratorSettingsVm, isDeep);
            to.ConnStr = from.ConnStr; // Clone.tt Line: 141
            to.PluginGroupSettingsGuid = from.PluginGroupSettingsGuid; // Clone.tt Line: 141
            to.ConnStrToPrevStable = from.ConnStrToPrevStable; // Clone.tt Line: 141
            to.IsGenerateSqlSqriptToUpdatePrevStable = from.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 141
            to.GenScriptFileName = from.GenScriptFileName; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override AppProjectGenerator Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return AppProjectGenerator.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(AppProjectGenerator from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            AppProjectGenerator.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_app_project_generator' to 'AppProjectGenerator'
        public static AppProjectGenerator ConvertToVM(Proto.Config.proto_app_project_generator m, AppProjectGenerator vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 221
            vm.DescriptionPlugin = m.DescriptionPlugin; // Clone.tt Line: 221
            vm.PluginGeneratorGuid = m.PluginGeneratorGuid; // Clone.tt Line: 221
            vm.DescriptionGenerator = m.DescriptionGenerator; // Clone.tt Line: 221
            vm.RelativePathToGenFolder = m.RelativePathToGenFolder; // Clone.tt Line: 221
            vm.GenFileName = m.GenFileName; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 221
            if (vm.GeneratorSettingsVm == null) // Clone.tt Line: 213
                vm.GeneratorSettingsVm = new PluginGeneratorSettings(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.PluginGeneratorSettings.ConvertToVM(m.GeneratorSettingsVm, (PluginGeneratorSettings)vm.GeneratorSettingsVm); // Clone.tt Line: 219
            vm.ConnStr = m.ConnStr; // Clone.tt Line: 221
            vm.PluginGroupSettingsGuid = m.PluginGroupSettingsGuid; // Clone.tt Line: 221
            vm.ConnStrToPrevStable = m.ConnStrToPrevStable; // Clone.tt Line: 221
            vm.IsGenerateSqlSqriptToUpdatePrevStable = m.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 221
            vm.GenScriptFileName = m.GenScriptFileName; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'AppProjectGenerator' to 'proto_app_project_generator'
        public static Proto.Config.proto_app_project_generator ConvertToProto(AppProjectGenerator vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_app_project_generator m = new Proto.Config.proto_app_project_generator(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 276
            m.DescriptionPlugin = vm.DescriptionPlugin; // Clone.tt Line: 276
            m.PluginGeneratorGuid = vm.PluginGeneratorGuid; // Clone.tt Line: 276
            m.DescriptionGenerator = vm.DescriptionGenerator; // Clone.tt Line: 276
            m.RelativePathToGenFolder = vm.RelativePathToGenFolder; // Clone.tt Line: 276
            m.GenFileName = vm.GenFileName; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 276
            m.GeneratorSettingsVm = vSharpStudio.vm.ViewModels.PluginGeneratorSettings.ConvertToProto((PluginGeneratorSettings)vm.GeneratorSettingsVm); // Clone.tt Line: 270
            m.ConnStr = vm.ConnStr; // Clone.tt Line: 276
            m.PluginGroupSettingsGuid = vm.PluginGroupSettingsGuid; // Clone.tt Line: 276
            m.ConnStrToPrevStable = vm.ConnStrToPrevStable; // Clone.tt Line: 276
            m.IsGenerateSqlSqriptToUpdatePrevStable = vm.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 276
            m.GenScriptFileName = vm.GenScriptFileName; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GeneratorSettingsVm.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [Description("Connection string name for DB connection generator")]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Plugin")]
        [Description("Plugins with generators")]
        [Editor(typeof(EditorPluginSelection), typeof(ITypeEditor))]
        public string PluginGuid // Property.tt Line: 55
        { 
            get { return this._PluginGuid; }
            set
            {
                if (this._PluginGuid != value)
                {
                    this.OnPluginGuidChanging(ref value);
                    this._PluginGuid = value;
                    this.OnPluginGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PluginGuid = string.Empty;
        partial void OnPluginGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPluginGuidChanged();
        
        [PropertyOrderAttribute(5)]
        [DisplayName("Description")]
        [ReadOnly(true)]
        public string DescriptionPlugin // Property.tt Line: 55
        { 
            get { return this._DescriptionPlugin; }
            set
            {
                if (this._DescriptionPlugin != value)
                {
                    this.OnDescriptionPluginChanging(ref value);
                    this._DescriptionPlugin = value;
                    this.OnDescriptionPluginChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _DescriptionPlugin = string.Empty;
        partial void OnDescriptionPluginChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionPluginChanged();
        
        [PropertyOrderAttribute(6)]
        [DisplayName("Generator")]
        [Description("Plugin generator")]
        [Editor(typeof(EditorPluginGeneratorSelection), typeof(ITypeEditor))]
        public string PluginGeneratorGuid // Property.tt Line: 55
        { 
            get { return this._PluginGeneratorGuid; }
            set
            {
                if (this._PluginGeneratorGuid != value)
                {
                    this.OnPluginGeneratorGuidChanging(ref value);
                    this._PluginGeneratorGuid = value;
                    this.OnPluginGeneratorGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PluginGeneratorGuid = string.Empty;
        partial void OnPluginGeneratorGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPluginGeneratorGuidChanged();
        
        [PropertyOrderAttribute(7)]
        [DisplayName("Description")]
        [ReadOnly(true)]
        public string DescriptionGenerator // Property.tt Line: 55
        { 
            get { return this._DescriptionGenerator; }
            set
            {
                if (this._DescriptionGenerator != value)
                {
                    this.OnDescriptionGeneratorChanging(ref value);
                    this._DescriptionGenerator = value;
                    this.OnDescriptionGeneratorChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _DescriptionGenerator = string.Empty;
        partial void OnDescriptionGeneratorChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionGeneratorChanged();
        
        
        ///////////////////////////////////////////////////
        /// Relative folder path to project file
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(8)]
        [DisplayName("Output Folder")]
        [Editor(typeof(EditorFolderPicker), typeof(ITypeEditor))]
        [Description("Get is returning relative folder path to project file")]
        public string RelativePathToGenFolder // Property.tt Line: 55
        { 
            get { return this._RelativePathToGenFolder; }
            set
            {
                if (this._RelativePathToGenFolder != value)
                {
                    this.OnRelativePathToGenFolderChanging(ref value);
                    this._RelativePathToGenFolder = value;
                    this.OnRelativePathToGenFolderChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _RelativePathToGenFolder = string.Empty;
        partial void OnRelativePathToGenFolderChanging(ref string to); // Property.tt Line: 79
        partial void OnRelativePathToGenFolderChanged();
        
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        [DisplayName("Output File")]
        [PropertyOrderAttribute(9)]
        [Description("Generator output file name")]
        public string GenFileName // Property.tt Line: 55
        { 
            get { return this._GenFileName; }
            set
            {
                if (this._GenFileName != value)
                {
                    this.OnGenFileNameChanging(ref value);
                    this._GenFileName = value;
                    this.OnGenFileNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _GenFileName = string.Empty;
        partial void OnGenFileNameChanging(ref string to); // Property.tt Line: 79
        partial void OnGenFileNameChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public string GeneratorSettings // Property.tt Line: 55
        { 
            get { return this._GeneratorSettings; }
            set
            {
                if (this._GeneratorSettings != value)
                {
                    this.OnGeneratorSettingsChanging(ref value);
                    this._GeneratorSettings = value;
                    this.OnGeneratorSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _GeneratorSettings = string.Empty;
        partial void OnGeneratorSettingsChanging(ref string to); // Property.tt Line: 79
        partial void OnGeneratorSettingsChanged();
        
        [PropertyOrderAttribute(29)]
        [BrowsableAttribute(false)]
        public PluginGeneratorSettings GeneratorSettingsVm // Property.tt Line: 55
        { 
            get { return this._GeneratorSettingsVm; }
            set
            {
                if (this._GeneratorSettingsVm != value)
                {
                    this.OnGeneratorSettingsVmChanging(ref value);
                    this._GeneratorSettingsVm = value;
                    this.OnGeneratorSettingsVmChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private PluginGeneratorSettings _GeneratorSettingsVm;
        IPluginGeneratorSettings IAppProjectGenerator.GeneratorSettingsVm { get { return (this as AppProjectGenerator).GeneratorSettingsVm; } } // Property.tt Line: 77
        partial void OnGeneratorSettingsVmChanging(ref PluginGeneratorSettings to); // Property.tt Line: 79
        partial void OnGeneratorSettingsVmChanged();
        //IPluginGeneratorSettings IAppProjectGenerator.GeneratorSettingsVm { get { return this._GeneratorSettingsVm; } }
        
        [PropertyOrderAttribute(9)]
        [Description("Db connection string. Directly editable or generated based on settings")]
        public string ConnStr // Property.tt Line: 55
        { 
            get { return this._ConnStr; }
            set
            {
                if (this._ConnStr != value)
                {
                    this.OnConnStrChanging(ref value);
                    this._ConnStr = value;
                    this.OnConnStrChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _ConnStr = string.Empty;
        partial void OnConnStrChanging(ref string to); // Property.tt Line: 79
        partial void OnConnStrChanged();
        
        [BrowsableAttribute(false)]
        public string PluginGroupSettingsGuid // Property.tt Line: 55
        { 
            get { return this._PluginGroupSettingsGuid; }
            set
            {
                if (this._PluginGroupSettingsGuid != value)
                {
                    this.OnPluginGroupSettingsGuidChanging(ref value);
                    this._PluginGroupSettingsGuid = value;
                    this.OnPluginGroupSettingsGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PluginGroupSettingsGuid = string.Empty;
        partial void OnPluginGroupSettingsGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPluginGroupSettingsGuidChanged();
        
        [PropertyOrderAttribute(13)]
        [DisplayName("Stable DB")]
        [Description("Db connection string to previous stable version")]
        public string ConnStrToPrevStable // Property.tt Line: 55
        { 
            get { return this._ConnStrToPrevStable; }
            set
            {
                if (this._ConnStrToPrevStable != value)
                {
                    this.OnConnStrToPrevStableChanging(ref value);
                    this._ConnStrToPrevStable = value;
                    this.OnConnStrToPrevStableChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _ConnStrToPrevStable = string.Empty;
        partial void OnConnStrToPrevStableChanging(ref string to); // Property.tt Line: 79
        partial void OnConnStrToPrevStableChanged();
        
        [PropertyOrderAttribute(14)]
        [DisplayName("Migrate DB")]
        [Description("Generate Sql script to update stable DB version to current state")]
        public bool IsGenerateSqlSqriptToUpdatePrevStable // Property.tt Line: 55
        { 
            get { return this._IsGenerateSqlSqriptToUpdatePrevStable; }
            set
            {
                if (this._IsGenerateSqlSqriptToUpdatePrevStable != value)
                {
                    this.OnIsGenerateSqlSqriptToUpdatePrevStableChanging(ref value);
                    this._IsGenerateSqlSqriptToUpdatePrevStable = value;
                    this.OnIsGenerateSqlSqriptToUpdatePrevStableChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsGenerateSqlSqriptToUpdatePrevStable;
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanged();
        
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        [DisplayName("SQL file")]
        [PropertyOrderAttribute(15)]
        [Description("SQL script output file name")]
        public string GenScriptFileName // Property.tt Line: 55
        { 
            get { return this._GenScriptFileName; }
            set
            {
                if (this._GenScriptFileName != value)
                {
                    this.OnGenScriptFileNameChanging(ref value);
                    this._GenScriptFileName = value;
                    this.OnGenScriptFileNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _GenScriptFileName = string.Empty;
        partial void OnGenScriptFileNameChanging(ref string to); // Property.tt Line: 79
        partial void OnGenScriptFileNameChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorNodeDefaultSettingsValidator : ValidatorBase<PluginGeneratorNodeDefaultSettings, PluginGeneratorNodeDefaultSettingsValidator> { } // Class.tt Line: 6
    public partial class PluginGeneratorNodeDefaultSettings : VmValidatableWithSeverity<PluginGeneratorNodeDefaultSettings, PluginGeneratorNodeDefaultSettingsValidator>, IPluginGeneratorNodeDefaultSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGeneratorNodeDefaultSettings() 
            : base(PluginGeneratorNodeDefaultSettingsValidator.Validator) // Class.tt Line: 45
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
        public static PluginGeneratorNodeDefaultSettings Clone(IPluginGeneratorNodeDefaultSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGeneratorNodeDefaultSettings vm = new PluginGeneratorNodeDefaultSettings();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.NodeSettingsVmGuid = from.NodeSettingsVmGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGeneratorNodeDefaultSettings to, IPluginGeneratorNodeDefaultSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.NodeSettingsVmGuid = from.NodeSettingsVmGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGeneratorNodeDefaultSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGeneratorNodeDefaultSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginGeneratorNodeDefaultSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginGeneratorNodeDefaultSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin_generator_node_default_settings' to 'PluginGeneratorNodeDefaultSettings'
        public static PluginGeneratorNodeDefaultSettings ConvertToVM(Proto.Config.proto_plugin_generator_node_default_settings m, PluginGeneratorNodeDefaultSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.NodeSettingsVmGuid = m.NodeSettingsVmGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGeneratorNodeDefaultSettings' to 'proto_plugin_generator_node_default_settings'
        public static Proto.Config.proto_plugin_generator_node_default_settings ConvertToProto(PluginGeneratorNodeDefaultSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_generator_node_default_settings m = new Proto.Config.proto_plugin_generator_node_default_settings(); // Clone.tt Line: 239
            m.NodeSettingsVmGuid = vm.NodeSettingsVmGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        public string NodeSettingsVmGuid // Property.tt Line: 55
        { 
            get { return this._NodeSettingsVmGuid; }
            set
            {
                if (this._NodeSettingsVmGuid != value)
                {
                    this.OnNodeSettingsVmGuidChanging(ref value);
                    this._NodeSettingsVmGuid = value;
                    this.OnNodeSettingsVmGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _NodeSettingsVmGuid = string.Empty;
        partial void OnNodeSettingsVmGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnNodeSettingsVmGuidChanged();
        
        public string Settings // Property.tt Line: 55
        { 
            get { return this._Settings; }
            set
            {
                if (this._Settings != value)
                {
                    this.OnSettingsChanging(ref value);
                    this._Settings = value;
                    this.OnSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Settings = string.Empty;
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 79
        partial void OnSettingsChanged();
        #endregion Properties
    }
    public partial class DbSettingsValidator : ValidatorBase<DbSettings, DbSettingsValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// General DB settings
    ///////////////////////////////////////////////////
    public partial class DbSettings : VmValidatableWithSeverity<DbSettings, DbSettingsValidator>, IDbSettings // Class.tt Line: 7
    {
        #region CTOR
        public DbSettings() 
            : base(DbSettingsValidator.Validator) // Class.tt Line: 45
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
        public static DbSettings Clone(IDbSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            DbSettings vm = new DbSettings();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.DbSchema = from.DbSchema; // Clone.tt Line: 65
            vm.IdGenerator = from.IdGenerator; // Clone.tt Line: 65
            vm.PKeyType = from.PKeyType; // Clone.tt Line: 65
            vm.PKeyName = from.PKeyName; // Clone.tt Line: 65
            vm.PKeyGuid = from.PKeyGuid; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(DbSettings to, IDbSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.DbSchema = from.DbSchema; // Clone.tt Line: 141
            to.IdGenerator = from.IdGenerator; // Clone.tt Line: 141
            to.PKeyType = from.PKeyType; // Clone.tt Line: 141
            to.PKeyName = from.PKeyName; // Clone.tt Line: 141
            to.PKeyGuid = from.PKeyGuid; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override DbSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return DbSettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(DbSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            DbSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'db_settings' to 'DbSettings'
        public static DbSettings ConvertToVM(Proto.Config.db_settings m, DbSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.DbSchema = m.DbSchema; // Clone.tt Line: 221
            vm.IdGenerator = (DbIdGeneratorMethod)m.IdGenerator; // Clone.tt Line: 221
            vm.PKeyType = (EnumPrimaryKeyType)m.PKeyType; // Clone.tt Line: 221
            vm.PKeyName = m.PKeyName; // Clone.tt Line: 221
            vm.PKeyGuid = m.PKeyGuid; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'DbSettings' to 'db_settings'
        public static Proto.Config.db_settings ConvertToProto(DbSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.db_settings m = new Proto.Config.db_settings(); // Clone.tt Line: 239
            m.DbSchema = vm.DbSchema; // Clone.tt Line: 276
            m.IdGenerator = (Proto.Config.db_id_generator_method)vm.IdGenerator; // Clone.tt Line: 274
            m.PKeyType = (Proto.Config.proto_enum_primary_key_type)vm.PKeyType; // Clone.tt Line: 274
            m.PKeyName = vm.PKeyName; // Clone.tt Line: 276
            m.PKeyGuid = vm.PKeyGuid; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [PropertyOrderAttribute(1)]
        [DisplayName("Schema")]
        [Description("DB schema name for all object in this configuration")]
        public string DbSchema // Property.tt Line: 55
        { 
            get { return this._DbSchema; }
            set
            {
                if (this._DbSchema != value)
                {
                    this.OnDbSchemaChanging(ref value);
                    this._DbSchema = value;
                    this.OnDbSchemaChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _DbSchema = string.Empty;
        partial void OnDbSchemaChanging(ref string to); // Property.tt Line: 79
        partial void OnDbSchemaChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("Id method")]
        [Description("Primary key generation method")]
        public DbIdGeneratorMethod IdGenerator // Property.tt Line: 55
        { 
            get { return this._IdGenerator; }
            set
            {
                if (this._IdGenerator != value)
                {
                    this.OnIdGeneratorChanging(ref value);
                    this._IdGenerator = value;
                    this.OnIdGeneratorChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private DbIdGeneratorMethod _IdGenerator;
        partial void OnIdGeneratorChanging(ref DbIdGeneratorMethod to); // Property.tt Line: 79
        partial void OnIdGeneratorChanged();
        
        [PropertyOrderAttribute(3)]
        [DisplayName("Id type")]
        [Description("Primary key field type")]
        public EnumPrimaryKeyType PKeyType // Property.tt Line: 55
        { 
            get { return this._PKeyType; }
            set
            {
                if (this._PKeyType != value)
                {
                    this.OnPKeyTypeChanging(ref value);
                    this._PKeyType = value;
                    this.OnPKeyTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumPrimaryKeyType _PKeyType;
        partial void OnPKeyTypeChanging(ref EnumPrimaryKeyType to); // Property.tt Line: 79
        partial void OnPKeyTypeChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Id name")]
        [Description("Primary key field name")]
        public string PKeyName // Property.tt Line: 55
        { 
            get { return this._PKeyName; }
            set
            {
                if (this._PKeyName != value)
                {
                    this.OnPKeyNameChanging(ref value);
                    this._PKeyName = value;
                    this.OnPKeyNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PKeyName = string.Empty;
        partial void OnPKeyNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPKeyNameChanged();
        
        [BrowsableAttribute(false)]
        public string PKeyGuid // Property.tt Line: 55
        { 
            get { return this._PKeyGuid; }
            set
            {
                if (this._PKeyGuid != value)
                {
                    this.OnPKeyGuidChanging(ref value);
                    this._PKeyGuid = value;
                    this.OnPKeyGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PKeyGuid = string.Empty;
        partial void OnPKeyGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPKeyGuidChanged();
        #endregion Properties
    }
    public partial class ModelValidator : ValidatorBase<Model, ModelValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    [CategoryOrder("Db Names Generation", 5)]
    public partial class Model : ConfigObjectVmGenSettings<Model, ModelValidator>, IComparable<Model>, IConfigAcceptVisitor, IModel // Class.tt Line: 7
    {
        #region CTOR
        public Model() : this(default(ITreeConfigNode))
        {
        }
        public Model(ITreeConfigNode parent) 
            : base(parent, ModelValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.DbSettings = new DbSettings(); // Class.tt Line: 31
            this.GroupCommon = new GroupListCommon(this); // Class.tt Line: 33
            this.GroupConstantGroups = new GroupConstantGroups(this); // Class.tt Line: 33
            this.GroupEnumerations = new GroupListEnumerations(this); // Class.tt Line: 33
            this.GroupCatalogs = new GroupListCatalogs(this); // Class.tt Line: 33
            this.GroupDocuments = new GroupDocuments(this); // Class.tt Line: 33
            this.GroupJournals = new GroupListJournals(this); // Class.tt Line: 33
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Model Clone(ITreeConfigNode parent, IModel from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Model vm = new Model(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Version = from.Version; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.CompositeNameMaxLength = from.CompositeNameMaxLength; // Clone.tt Line: 65
            vm.IsUseCompositeNames = from.IsUseCompositeNames; // Clone.tt Line: 65
            vm.IsUseGroupPrefix = from.IsUseGroupPrefix; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.DbSettings = vSharpStudio.vm.ViewModels.DbSettings.Clone(from.DbSettings, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupCommon = vSharpStudio.vm.ViewModels.GroupListCommon.Clone(vm, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupConstantGroups = vSharpStudio.vm.ViewModels.GroupConstantGroups.Clone(vm, from.GroupConstantGroups, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupEnumerations = vSharpStudio.vm.ViewModels.GroupListEnumerations.Clone(vm, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupCatalogs = vSharpStudio.vm.ViewModels.GroupListCatalogs.Clone(vm, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupDocuments = vSharpStudio.vm.ViewModels.GroupDocuments.Clone(vm, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupJournals = vSharpStudio.vm.ViewModels.GroupListJournals.Clone(vm, from.GroupJournals, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Model to, IModel from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Version = from.Version; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.CompositeNameMaxLength = from.CompositeNameMaxLength; // Clone.tt Line: 141
            to.IsUseCompositeNames = from.IsUseCompositeNames; // Clone.tt Line: 141
            to.IsUseGroupPrefix = from.IsUseGroupPrefix; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.DbSettings.Update((DbSettings)to.DbSettings, from.DbSettings, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListCommon.Update((GroupListCommon)to.GroupCommon, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupConstantGroups.Update((GroupConstantGroups)to.GroupConstantGroups, from.GroupConstantGroups, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListEnumerations.Update((GroupListEnumerations)to.GroupEnumerations, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListCatalogs.Update((GroupListCatalogs)to.GroupCatalogs, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupDocuments.Update((GroupDocuments)to.GroupDocuments, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListJournals.Update((GroupListJournals)to.GroupJournals, from.GroupJournals, isDeep);
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Model Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Model.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Model from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Model.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_model' to 'Model'
        public static Model ConvertToVM(Proto.Config.proto_model m, Model vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Version = m.Version; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.CompositeNameMaxLength = m.CompositeNameMaxLength; // Clone.tt Line: 221
            vm.IsUseCompositeNames = m.IsUseCompositeNames; // Clone.tt Line: 221
            vm.IsUseGroupPrefix = m.IsUseGroupPrefix; // Clone.tt Line: 221
            if (vm.DbSettings == null) // Clone.tt Line: 213
                vm.DbSettings = new DbSettings(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.DbSettings.ConvertToVM(m.DbSettings, (DbSettings)vm.DbSettings); // Clone.tt Line: 219
            if (vm.GroupCommon == null) // Clone.tt Line: 213
                vm.GroupCommon = new GroupListCommon(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListCommon.ConvertToVM(m.GroupCommon, (GroupListCommon)vm.GroupCommon); // Clone.tt Line: 219
            if (vm.GroupConstantGroups == null) // Clone.tt Line: 213
                vm.GroupConstantGroups = new GroupConstantGroups(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupConstantGroups.ConvertToVM(m.GroupConstantGroups, (GroupConstantGroups)vm.GroupConstantGroups); // Clone.tt Line: 219
            if (vm.GroupEnumerations == null) // Clone.tt Line: 213
                vm.GroupEnumerations = new GroupListEnumerations(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListEnumerations.ConvertToVM(m.GroupEnumerations, (GroupListEnumerations)vm.GroupEnumerations); // Clone.tt Line: 219
            if (vm.GroupCatalogs == null) // Clone.tt Line: 213
                vm.GroupCatalogs = new GroupListCatalogs(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListCatalogs.ConvertToVM(m.GroupCatalogs, (GroupListCatalogs)vm.GroupCatalogs); // Clone.tt Line: 219
            if (vm.GroupDocuments == null) // Clone.tt Line: 213
                vm.GroupDocuments = new GroupDocuments(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupDocuments.ConvertToVM(m.GroupDocuments, (GroupDocuments)vm.GroupDocuments); // Clone.tt Line: 219
            if (vm.GroupJournals == null) // Clone.tt Line: 213
                vm.GroupJournals = new GroupListJournals(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListJournals.ConvertToVM(m.GroupJournals, (GroupListJournals)vm.GroupJournals); // Clone.tt Line: 219
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Model' to 'proto_model'
        public static Proto.Config.proto_model ConvertToProto(Model vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_model m = new Proto.Config.proto_model(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Version = vm.Version; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.CompositeNameMaxLength = vm.CompositeNameMaxLength; // Clone.tt Line: 276
            m.IsUseCompositeNames = vm.IsUseCompositeNames; // Clone.tt Line: 276
            m.IsUseGroupPrefix = vm.IsUseGroupPrefix; // Clone.tt Line: 276
            m.DbSettings = vSharpStudio.vm.ViewModels.DbSettings.ConvertToProto((DbSettings)vm.DbSettings); // Clone.tt Line: 270
            m.GroupCommon = vSharpStudio.vm.ViewModels.GroupListCommon.ConvertToProto((GroupListCommon)vm.GroupCommon); // Clone.tt Line: 270
            m.GroupConstantGroups = vSharpStudio.vm.ViewModels.GroupConstantGroups.ConvertToProto((GroupConstantGroups)vm.GroupConstantGroups); // Clone.tt Line: 270
            m.GroupEnumerations = vSharpStudio.vm.ViewModels.GroupListEnumerations.ConvertToProto((GroupListEnumerations)vm.GroupEnumerations); // Clone.tt Line: 270
            m.GroupCatalogs = vSharpStudio.vm.ViewModels.GroupListCatalogs.ConvertToProto((GroupListCatalogs)vm.GroupCatalogs); // Clone.tt Line: 270
            m.GroupDocuments = vSharpStudio.vm.ViewModels.GroupDocuments.ConvertToProto((GroupDocuments)vm.GroupDocuments); // Clone.tt Line: 270
            m.GroupJournals = vSharpStudio.vm.ViewModels.GroupListJournals.ConvertToProto((GroupListJournals)vm.GroupJournals); // Clone.tt Line: 270
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.DbSettings.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupCommon.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupConstantGroups.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupEnumerations.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupCatalogs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupDocuments.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupJournals.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(2)]
        [ReadOnly(true)]
        public int Version // Property.tt Line: 55
        { 
            get { return this._Version; }
            set
            {
                if (this._Version != value)
                {
                    this.OnVersionChanging(ref value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Version;
        partial void OnVersionChanging(ref int to); // Property.tt Line: 79
        partial void OnVersionChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(8)]
        [Category("Composite Names Generation")]
        [DisplayName("Max length")]
        public uint CompositeNameMaxLength // Property.tt Line: 55
        { 
            get { return this._CompositeNameMaxLength; }
            set
            {
                if (this._CompositeNameMaxLength != value)
                {
                    this.OnCompositeNameMaxLengthChanging(ref value);
                    this._CompositeNameMaxLength = value;
                    this.OnCompositeNameMaxLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _CompositeNameMaxLength;
        partial void OnCompositeNameMaxLengthChanging(ref uint to); // Property.tt Line: 79
        partial void OnCompositeNameMaxLengthChanged();
        
        [PropertyOrderAttribute(9)]
        [Description("Use parent-child composite names.")]
        [Category("Composite Names Generation")]
        [DisplayName("Use Composite")]
        public bool IsUseCompositeNames // Property.tt Line: 55
        { 
            get { return this._IsUseCompositeNames; }
            set
            {
                if (this._IsUseCompositeNames != value)
                {
                    this.OnIsUseCompositeNamesChanging(ref value);
                    this._IsUseCompositeNames = value;
                    this.OnIsUseCompositeNamesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsUseCompositeNames;
        partial void OnIsUseCompositeNamesChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsUseCompositeNamesChanged();
        
        [PropertyOrderAttribute(10)]
        [Description("Composite names use their parent name as prefix. In a case of simple names all object's name will have only group name as a prefix.")]
        [Category("Composite Names Generation")]
        [DisplayName("Use Prefix")]
        public bool IsUseGroupPrefix // Property.tt Line: 55
        { 
            get { return this._IsUseGroupPrefix; }
            set
            {
                if (this._IsUseGroupPrefix != value)
                {
                    this.OnIsUseGroupPrefixChanging(ref value);
                    this._IsUseGroupPrefix = value;
                    this.OnIsUseGroupPrefixChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsUseGroupPrefix;
        partial void OnIsUseGroupPrefixChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsUseGroupPrefixChanged();
        
        
        ///////////////////////////////////////////////////
        /// GENERAL DB SETTINGS
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [Description("General DB generator settings")]
        [DisplayName("DB settings")]
        public DbSettings DbSettings // Property.tt Line: 55
        { 
            get { return this._DbSettings; }
            set
            {
                if (this._DbSettings != value)
                {
                    this.OnDbSettingsChanging(ref value);
                    this._DbSettings = value;
                    this.OnDbSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private DbSettings _DbSettings;
        IDbSettings IModel.DbSettings { get { return (this as Model).DbSettings; } } // Property.tt Line: 77
        partial void OnDbSettingsChanging(ref DbSettings to); // Property.tt Line: 79
        partial void OnDbSettingsChanged();
        //IDbSettings IModel.DbSettings { get { return this._DbSettings; } }
        
        [BrowsableAttribute(false)]
        public GroupListCommon GroupCommon // Property.tt Line: 55
        { 
            get { return this._GroupCommon; }
            set
            {
                if (this._GroupCommon != value)
                {
                    this.OnGroupCommonChanging(ref value);
                    this._GroupCommon = value;
                    this.OnGroupCommonChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListCommon _GroupCommon;
        IGroupListCommon IModel.GroupCommon { get { return (this as Model).GroupCommon; } } // Property.tt Line: 77
        partial void OnGroupCommonChanging(ref GroupListCommon to); // Property.tt Line: 79
        partial void OnGroupCommonChanged();
        //IGroupListCommon IModel.GroupCommon { get { return this._GroupCommon; } }
        
        [BrowsableAttribute(false)]
        public GroupConstantGroups GroupConstantGroups // Property.tt Line: 55
        { 
            get { return this._GroupConstantGroups; }
            set
            {
                if (this._GroupConstantGroups != value)
                {
                    this.OnGroupConstantGroupsChanging(ref value);
                    this._GroupConstantGroups = value;
                    this.OnGroupConstantGroupsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupConstantGroups _GroupConstantGroups;
        IGroupConstantGroups IModel.GroupConstantGroups { get { return (this as Model).GroupConstantGroups; } } // Property.tt Line: 77
        partial void OnGroupConstantGroupsChanging(ref GroupConstantGroups to); // Property.tt Line: 79
        partial void OnGroupConstantGroupsChanged();
        //IGroupConstantGroups IModel.GroupConstantGroups { get { return this._GroupConstantGroups; } }
        
        [BrowsableAttribute(false)]
        public GroupListEnumerations GroupEnumerations // Property.tt Line: 55
        { 
            get { return this._GroupEnumerations; }
            set
            {
                if (this._GroupEnumerations != value)
                {
                    this.OnGroupEnumerationsChanging(ref value);
                    this._GroupEnumerations = value;
                    this.OnGroupEnumerationsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListEnumerations _GroupEnumerations;
        IGroupListEnumerations IModel.GroupEnumerations { get { return (this as Model).GroupEnumerations; } } // Property.tt Line: 77
        partial void OnGroupEnumerationsChanging(ref GroupListEnumerations to); // Property.tt Line: 79
        partial void OnGroupEnumerationsChanged();
        //IGroupListEnumerations IModel.GroupEnumerations { get { return this._GroupEnumerations; } }
        
        [BrowsableAttribute(false)]
        public GroupListCatalogs GroupCatalogs // Property.tt Line: 55
        { 
            get { return this._GroupCatalogs; }
            set
            {
                if (this._GroupCatalogs != value)
                {
                    this.OnGroupCatalogsChanging(ref value);
                    this._GroupCatalogs = value;
                    this.OnGroupCatalogsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListCatalogs _GroupCatalogs;
        IGroupListCatalogs IModel.GroupCatalogs { get { return (this as Model).GroupCatalogs; } } // Property.tt Line: 77
        partial void OnGroupCatalogsChanging(ref GroupListCatalogs to); // Property.tt Line: 79
        partial void OnGroupCatalogsChanged();
        //IGroupListCatalogs IModel.GroupCatalogs { get { return this._GroupCatalogs; } }
        
        [BrowsableAttribute(false)]
        public GroupDocuments GroupDocuments // Property.tt Line: 55
        { 
            get { return this._GroupDocuments; }
            set
            {
                if (this._GroupDocuments != value)
                {
                    this.OnGroupDocumentsChanging(ref value);
                    this._GroupDocuments = value;
                    this.OnGroupDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupDocuments _GroupDocuments;
        IGroupDocuments IModel.GroupDocuments { get { return (this as Model).GroupDocuments; } } // Property.tt Line: 77
        partial void OnGroupDocumentsChanging(ref GroupDocuments to); // Property.tt Line: 79
        partial void OnGroupDocumentsChanged();
        //IGroupDocuments IModel.GroupDocuments { get { return this._GroupDocuments; } }
        
        [BrowsableAttribute(false)]
        public GroupListJournals GroupJournals // Property.tt Line: 55
        { 
            get { return this._GroupJournals; }
            set
            {
                if (this._GroupJournals != value)
                {
                    this.OnGroupJournalsChanging(ref value);
                    this._GroupJournals = value;
                    this.OnGroupJournalsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListJournals _GroupJournals;
        IGroupListJournals IModel.GroupJournals { get { return (this as Model).GroupJournals; } } // Property.tt Line: 77
        partial void OnGroupJournalsChanging(ref GroupListJournals to); // Property.tt Line: 79
        partial void OnGroupJournalsChanged();
        //IGroupListJournals IModel.GroupJournals { get { return this._GroupJournals; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IModel.ListNodeGeneratorsSettings { get { return (this as Model).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { } // Class.tt Line: 6
    public partial class DataType : VmValidatableWithSeverity<DataType, DataTypeValidator>, IDataType // Class.tt Line: 7
    {
        #region CTOR
        public DataType() 
            : base(DataTypeValidator.Validator) // Class.tt Line: 45
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListObjectGuids = new ObservableCollection<string>(); // Class.tt Line: 54
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static DataType Clone(IDataType from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            DataType vm = new DataType();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 65
            vm.Length = from.Length; // Clone.tt Line: 65
            vm.Accuracy = from.Accuracy; // Clone.tt Line: 65
            vm.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 65
            foreach (var t in from.ListObjectGuids) // Clone.tt Line: 44
                vm.ListObjectGuids.Add(t);
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 65
            vm.IsPositive = from.IsPositive; // Clone.tt Line: 65
            vm.IsNullable = from.IsNullable; // Clone.tt Line: 65
            vm.IsPKey = from.IsPKey; // Clone.tt Line: 65
            vm.IsRefParent = from.IsRefParent; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(DataType to, IDataType from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 141
            to.Length = from.Length; // Clone.tt Line: 141
            to.Accuracy = from.Accuracy; // Clone.tt Line: 141
            to.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 141
                to.ListObjectGuids.Clear(); // Clone.tt Line: 127
                foreach (var tt in from.ListObjectGuids)
                {
                    to.ListObjectGuids.Add(tt);
                }
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 141
            to.IsPositive = from.IsPositive; // Clone.tt Line: 141
            to.IsNullable = from.IsNullable; // Clone.tt Line: 141
            to.IsPKey = from.IsPKey; // Clone.tt Line: 141
            to.IsRefParent = from.IsRefParent; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override DataType Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return DataType.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(DataType from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            DataType.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_data_type' to 'DataType'
        public static DataType ConvertToVM(Proto.Config.proto_data_type m, DataType vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.DataTypeEnum = (EnumDataType)m.DataTypeEnum; // Clone.tt Line: 221
            vm.Length = m.Length; // Clone.tt Line: 221
            vm.Accuracy = m.Accuracy; // Clone.tt Line: 221
            vm.ObjectGuid = m.ObjectGuid; // Clone.tt Line: 221
            vm.ListObjectGuids = new ObservableCollection<string>(); // Clone.tt Line: 184
            foreach (var t in m.ListObjectGuids) // Clone.tt Line: 185
            {
                vm.ListObjectGuids.Add(t);
            }
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 221
            vm.IsPositive = m.IsPositive; // Clone.tt Line: 221
            vm.IsNullable = m.IsNullable; // Clone.tt Line: 221
            vm.IsPKey = m.IsPKey; // Clone.tt Line: 221
            vm.IsRefParent = m.IsRefParent; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'DataType' to 'proto_data_type'
        public static Proto.Config.proto_data_type ConvertToProto(DataType vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_data_type m = new Proto.Config.proto_data_type(); // Clone.tt Line: 239
            m.DataTypeEnum = (Proto.Config.proto_enum_data_type)vm.DataTypeEnum; // Clone.tt Line: 274
            m.Length = vm.Length; // Clone.tt Line: 276
            m.Accuracy = vm.Accuracy; // Clone.tt Line: 276
            m.ObjectGuid = vm.ObjectGuid; // Clone.tt Line: 276
            foreach (var t in vm.ListObjectGuids) // Clone.tt Line: 242
                m.ListObjectGuids.Add(t); // Clone.tt Line: 244
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 276
            m.IsPositive = vm.IsPositive; // Clone.tt Line: 276
            m.IsNullable = vm.IsNullable; // Clone.tt Line: 276
            m.IsPKey = vm.IsPKey; // Clone.tt Line: 276
            m.IsRefParent = vm.IsRefParent; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [PropertyOrderAttribute(1)]
        [DisplayName("Type")]
        public EnumDataType DataTypeEnum // Property.tt Line: 55
        { 
            get { return this._DataTypeEnum; }
            set
            {
                if (this._DataTypeEnum != value)
                {
                    this.OnDataTypeEnumChanging(ref value);
                    this._DataTypeEnum = value;
                    this.OnDataTypeEnumChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumDataType _DataTypeEnum;
        partial void OnDataTypeEnumChanging(ref EnumDataType to); // Property.tt Line: 79
        partial void OnDataTypeEnumChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("Length")]
        [Description("Maximum length of data (characters in string, or decimal digits for numeric data)")]
        public uint Length // Property.tt Line: 55
        { 
            get { return this._Length; }
            set
            {
                if (this._Length != value)
                {
                    this.OnLengthChanging(ref value);
                    this._Length = value;
                    this.OnLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _Length;
        partial void OnLengthChanging(ref uint to); // Property.tt Line: 79
        partial void OnLengthChanged();
        
        [PropertyOrderAttribute(3)]
        [DisplayName("Accuracy")]
        [Description("Number of decimal places for numeric data)")]
        public uint Accuracy // Property.tt Line: 55
        { 
            get { return this._Accuracy; }
            set
            {
                if (this._Accuracy != value)
                {
                    this.OnAccuracyChanging(ref value);
                    this._Accuracy = value;
                    this.OnAccuracyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _Accuracy;
        partial void OnAccuracyChanging(ref uint to); // Property.tt Line: 79
        partial void OnAccuracyChanged();
        
        [PropertyOrderAttribute(4)]
        [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        public string ObjectGuid // Property.tt Line: 55
        { 
            get { return this._ObjectGuid; }
            set
            {
                if (this._ObjectGuid != value)
                {
                    this.OnObjectGuidChanging(ref value);
                    this._ObjectGuid = value;
                    this.OnObjectGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _ObjectGuid = string.Empty;
        partial void OnObjectGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnObjectGuidChanged();
        
        [PropertyOrderAttribute(5)]
        public ObservableCollection<string> ListObjectGuids // Property.tt Line: 8
        { 
            get { return this._ListObjectGuids; }
            set
            {
                if (this._ListObjectGuids != value)
                {
                    this.OnListObjectGuidsChanging(value);
                    _ListObjectGuids = value;
                    this.OnListObjectGuidsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ObservableCollection<string> _ListObjectGuids;
        IReadOnlyList<string> IDataType.ListObjectGuids { get { return (this as DataType).ListObjectGuids; } } // Property.tt Line: 26
        partial void OnListObjectGuidsChanging(ObservableCollection<string> to); // Property.tt Line: 27
        partial void OnListObjectGuidsChanged();
        
        [PropertyOrderAttribute(9)]
        [DisplayName("FK Index")]
        [Description("Create Index if this property is using foreign key (for Catalog or Document type)")]
        public bool IsIndexFk // Property.tt Line: 55
        { 
            get { return this._IsIndexFk; }
            set
            {
                if (this._IsIndexFk != value)
                {
                    this.OnIsIndexFkChanging(ref value);
                    this._IsIndexFk = value;
                    this.OnIsIndexFkChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsIndexFk;
        partial void OnIsIndexFkChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsIndexFkChanged();
        
        [PropertyOrderAttribute(11)]
        [DisplayName("Positive")]
        [Description("Expected always >= 0")]
        public bool IsPositive // Property.tt Line: 55
        { 
            get { return this._IsPositive; }
            set
            {
                if (this._IsPositive != value)
                {
                    this.OnIsPositiveChanging(ref value);
                    this._IsPositive = value;
                    this.OnIsPositiveChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsPositive;
        partial void OnIsPositiveChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsPositiveChanged();
        
        [PropertyOrderAttribute(12)]
        [DisplayName("Can be NULL")]
        [Description("If unchecked always expected data")]
        public bool IsNullable // Property.tt Line: 55
        { 
            get { return this._IsNullable; }
            set
            {
                if (this._IsNullable != value)
                {
                    this.OnIsNullableChanging(ref value);
                    this._IsNullable = value;
                    this.OnIsNullableChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNullable;
        partial void OnIsNullableChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNullableChanged();
        
        [BrowsableAttribute(false)]
        public bool IsPKey // Property.tt Line: 55
        { 
            get { return this._IsPKey; }
            set
            {
                if (this._IsPKey != value)
                {
                    this.OnIsPKeyChanging(ref value);
                    this._IsPKey = value;
                    this.OnIsPKeyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsPKey;
        partial void OnIsPKeyChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsPKeyChanged();
        
        [BrowsableAttribute(false)]
        public bool IsRefParent // Property.tt Line: 55
        { 
            get { return this._IsRefParent; }
            set
            {
                if (this._IsRefParent != value)
                {
                    this.OnIsRefParentChanging(ref value);
                    this._IsRefParent = value;
                    this.OnIsRefParentChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsRefParent;
        partial void OnIsRefParentChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsRefParentChanged();
        #endregion Properties
    }
    public partial class GroupListCommonValidator : ValidatorBase<GroupListCommon, GroupListCommonValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    public partial class GroupListCommon : ConfigObjectVmGenSettings<GroupListCommon, GroupListCommonValidator>, IComparable<GroupListCommon>, IConfigAcceptVisitor, IGroupListCommon // Class.tt Line: 7
    {
        #region CTOR
        public GroupListCommon() : this(default(ITreeConfigNode))
        {
        }
        public GroupListCommon(ITreeConfigNode parent) 
            : base(parent, GroupListCommonValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupRoles = new GroupListRoles(this); // Class.tt Line: 33
            this.GroupViewForms = new GroupListMainViewForms(this); // Class.tt Line: 33
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListCommon Clone(ITreeConfigNode parent, IGroupListCommon from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListCommon vm = new GroupListCommon(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupRoles = vSharpStudio.vm.ViewModels.GroupListRoles.Clone(vm, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupViewForms = vSharpStudio.vm.ViewModels.GroupListMainViewForms.Clone(vm, from.GroupViewForms, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListCommon to, IGroupListCommon from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListRoles.Update((GroupListRoles)to.GroupRoles, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListMainViewForms.Update((GroupListMainViewForms)to.GroupViewForms, from.GroupViewForms, isDeep);
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListCommon Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListCommon.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListCommon from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListCommon.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_common' to 'GroupListCommon'
        public static GroupListCommon ConvertToVM(Proto.Config.proto_group_list_common m, GroupListCommon vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.GroupRoles == null) // Clone.tt Line: 213
                vm.GroupRoles = new GroupListRoles(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListRoles.ConvertToVM(m.GroupRoles, (GroupListRoles)vm.GroupRoles); // Clone.tt Line: 219
            if (vm.GroupViewForms == null) // Clone.tt Line: 213
                vm.GroupViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListMainViewForms.ConvertToVM(m.GroupViewForms, (GroupListMainViewForms)vm.GroupViewForms); // Clone.tt Line: 219
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListCommon' to 'proto_group_list_common'
        public static Proto.Config.proto_group_list_common ConvertToProto(GroupListCommon vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_common m = new Proto.Config.proto_group_list_common(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.GroupRoles = vSharpStudio.vm.ViewModels.GroupListRoles.ConvertToProto((GroupListRoles)vm.GroupRoles); // Clone.tt Line: 270
            m.GroupViewForms = vSharpStudio.vm.ViewModels.GroupListMainViewForms.ConvertToProto((GroupListMainViewForms)vm.GroupViewForms); // Clone.tt Line: 270
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupRoles.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupViewForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListRoles GroupRoles // Property.tt Line: 55
        { 
            get { return this._GroupRoles; }
            set
            {
                if (this._GroupRoles != value)
                {
                    this.OnGroupRolesChanging(ref value);
                    this._GroupRoles = value;
                    this.OnGroupRolesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListRoles _GroupRoles;
        IGroupListRoles IGroupListCommon.GroupRoles { get { return (this as GroupListCommon).GroupRoles; } } // Property.tt Line: 77
        partial void OnGroupRolesChanging(ref GroupListRoles to); // Property.tt Line: 79
        partial void OnGroupRolesChanged();
        //IGroupListRoles IGroupListCommon.GroupRoles { get { return this._GroupRoles; } }
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupViewForms // Property.tt Line: 55
        { 
            get { return this._GroupViewForms; }
            set
            {
                if (this._GroupViewForms != value)
                {
                    this.OnGroupViewFormsChanging(ref value);
                    this._GroupViewForms = value;
                    this.OnGroupViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListMainViewForms _GroupViewForms;
        IGroupListMainViewForms IGroupListCommon.GroupViewForms { get { return (this as GroupListCommon).GroupViewForms; } } // Property.tt Line: 77
        partial void OnGroupViewFormsChanging(ref GroupListMainViewForms to); // Property.tt Line: 79
        partial void OnGroupViewFormsChanged();
        //IGroupListMainViewForms IGroupListCommon.GroupViewForms { get { return this._GroupViewForms; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListCommon.ListNodeGeneratorsSettings { get { return (this as GroupListCommon).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class RoleValidator : ValidatorBase<Role, RoleValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    public partial class Role : ConfigObjectVmGenSettings<Role, RoleValidator>, IComparable<Role>, IConfigAcceptVisitor, IRole // Class.tt Line: 7
    {
        #region CTOR
        public Role() : this(default(ITreeConfigNode))
        {
        }
        public Role(ITreeConfigNode parent) 
            : base(parent, RoleValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Role Clone(ITreeConfigNode parent, IRole from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Role vm = new Role(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Role to, IRole from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Role Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Role.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Role from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Role.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_role' to 'Role'
        public static Role ConvertToVM(Proto.Config.proto_role m, Role vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Role' to 'proto_role'
        public static Proto.Config.proto_role ConvertToProto(Role vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_role m = new Proto.Config.proto_role(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IRole.ListNodeGeneratorsSettings { get { return (this as Role).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListRolesValidator : ValidatorBase<GroupListRoles, GroupListRolesValidator> { } // Class.tt Line: 6
    public partial class GroupListRoles : ConfigObjectVmGenSettings<GroupListRoles, GroupListRolesValidator>, IComparable<GroupListRoles>, IConfigAcceptVisitor, IGroupListRoles // Class.tt Line: 7
    {
        #region CTOR
        public GroupListRoles() : this(default(ITreeConfigNode))
        {
        }
        public GroupListRoles(ITreeConfigNode parent) 
            : base(parent, GroupListRolesValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListRoles = new ConfigNodesCollection<Role>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Role)) // Clone.tt Line: 15
            {
                this.ListRoles.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListRoles Clone(ITreeConfigNode parent, IGroupListRoles from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListRoles vm = new GroupListRoles(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListRoles) // Clone.tt Line: 52
                vm.ListRoles.Add(Role.Clone(vm, (Role)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListRoles to, IGroupListRoles from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListRoles.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListRoles)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Role.Update((Role)t, (Role)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListRoles.Remove(t);
                }
                foreach (var tt in from.ListRoles)
                {
                    bool isfound = false;
                    foreach (var t in to.ListRoles.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Role(to); // Clone.tt Line: 117
                        Role.Update(p, (Role)tt, isDeep);
                        to.ListRoles.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListRoles Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListRoles.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListRoles from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListRoles.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_roles' to 'GroupListRoles'
        public static GroupListRoles ConvertToVM(Proto.Config.proto_group_list_roles m, GroupListRoles vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListRoles) // Clone.tt Line: 201
            {
                var tvm = Role.ConvertToVM(t, new Role(vm)); // Clone.tt Line: 204
                vm.ListRoles.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListRoles' to 'proto_group_list_roles'
        public static Proto.Config.proto_group_list_roles ConvertToProto(GroupListRoles vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_roles m = new Proto.Config.proto_group_list_roles(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListRoles) // Clone.tt Line: 242
                m.ListRoles.Add(Role.ConvertToProto((Role)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListRoles)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Role> ListRoles // Property.tt Line: 8
        { 
            get { return this._ListRoles; }
            set
            {
                if (this._ListRoles != value)
                {
                    this.OnListRolesChanging(value);
                    _ListRoles = value;
                    this.OnListRolesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Role> _ListRoles;
        IReadOnlyList<IRole> IGroupListRoles.ListRoles { get { return (this as GroupListRoles).ListRoles; } } // Property.tt Line: 26
        partial void OnListRolesChanging(ObservableCollection<Role> to); // Property.tt Line: 27
        partial void OnListRolesChanged();
        public Role this[int index] { get { return (Role)this.ListRoles[index]; } }
        IRole IGroupListRoles.this[int index] { get { return (Role)this.ListRoles[index]; } }
        public void Add(Role item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListRoles.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Role> items) 
        { 
            Contract.Requires(items != null);
            this.ListRoles.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListRoles.Count; }
        int IGroupListRoles.Count() { return this.Count(); }
        public void Remove(Role item) 
        {
            Contract.Requires(item != null);
            this.ListRoles.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListRoles.ListNodeGeneratorsSettings { get { return (this as GroupListRoles).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class MainViewFormValidator : ValidatorBase<MainViewForm, MainViewFormValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    public partial class MainViewForm : ConfigObjectVmGenSettings<MainViewForm, MainViewFormValidator>, IComparable<MainViewForm>, IConfigAcceptVisitor, IMainViewForm // Class.tt Line: 7
    {
        #region CTOR
        public MainViewForm() : this(default(ITreeConfigNode))
        {
        }
        public MainViewForm(ITreeConfigNode parent) 
            : base(parent, MainViewFormValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupListViewForms = new GroupListMainViewForms(this); // Class.tt Line: 33
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static MainViewForm Clone(ITreeConfigNode parent, IMainViewForm from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            MainViewForm vm = new MainViewForm(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupListViewForms = vSharpStudio.vm.ViewModels.GroupListMainViewForms.Clone(vm, from.GroupListViewForms, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(MainViewForm to, IMainViewForm from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListMainViewForms.Update((GroupListMainViewForms)to.GroupListViewForms, from.GroupListViewForms, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override MainViewForm Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return MainViewForm.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(MainViewForm from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            MainViewForm.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_main_view_form' to 'MainViewForm'
        public static MainViewForm ConvertToVM(Proto.Config.proto_main_view_form m, MainViewForm vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.GroupListViewForms == null) // Clone.tt Line: 213
                vm.GroupListViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListMainViewForms.ConvertToVM(m.GroupListViewForms, (GroupListMainViewForms)vm.GroupListViewForms); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'MainViewForm' to 'proto_main_view_form'
        public static Proto.Config.proto_main_view_form ConvertToProto(MainViewForm vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_main_view_form m = new Proto.Config.proto_main_view_form(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.GroupListViewForms = vSharpStudio.vm.ViewModels.GroupListMainViewForms.ConvertToProto((GroupListMainViewForms)vm.GroupListViewForms); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupListViewForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupListViewForms // Property.tt Line: 55
        { 
            get { return this._GroupListViewForms; }
            set
            {
                if (this._GroupListViewForms != value)
                {
                    this.OnGroupListViewFormsChanging(ref value);
                    this._GroupListViewForms = value;
                    this.OnGroupListViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListMainViewForms _GroupListViewForms;
        IGroupListMainViewForms IMainViewForm.GroupListViewForms { get { return (this as MainViewForm).GroupListViewForms; } } // Property.tt Line: 77
        partial void OnGroupListViewFormsChanging(ref GroupListMainViewForms to); // Property.tt Line: 79
        partial void OnGroupListViewFormsChanged();
        //IGroupListMainViewForms IMainViewForm.GroupListViewForms { get { return this._GroupListViewForms; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IMainViewForm.ListNodeGeneratorsSettings { get { return (this as MainViewForm).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListMainViewFormsValidator : ValidatorBase<GroupListMainViewForms, GroupListMainViewFormsValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    public partial class GroupListMainViewForms : ConfigObjectVmGenSettings<GroupListMainViewForms, GroupListMainViewFormsValidator>, IComparable<GroupListMainViewForms>, IConfigAcceptVisitor, IGroupListMainViewForms // Class.tt Line: 7
    {
        #region CTOR
        public GroupListMainViewForms() : this(default(ITreeConfigNode))
        {
        }
        public GroupListMainViewForms(ITreeConfigNode parent) 
            : base(parent, GroupListMainViewFormsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(MainViewForm)) // Clone.tt Line: 15
            {
                this.ListMainViewForms.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListMainViewForms Clone(ITreeConfigNode parent, IGroupListMainViewForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListMainViewForms vm = new GroupListMainViewForms(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListMainViewForms) // Clone.tt Line: 52
                vm.ListMainViewForms.Add(MainViewForm.Clone(vm, (MainViewForm)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListMainViewForms to, IGroupListMainViewForms from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListMainViewForms.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListMainViewForms)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            MainViewForm.Update((MainViewForm)t, (MainViewForm)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListMainViewForms.Remove(t);
                }
                foreach (var tt in from.ListMainViewForms)
                {
                    bool isfound = false;
                    foreach (var t in to.ListMainViewForms.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new MainViewForm(to); // Clone.tt Line: 117
                        MainViewForm.Update(p, (MainViewForm)tt, isDeep);
                        to.ListMainViewForms.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListMainViewForms Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListMainViewForms.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListMainViewForms from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListMainViewForms.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_main_view_forms' to 'GroupListMainViewForms'
        public static GroupListMainViewForms ConvertToVM(Proto.Config.proto_group_list_main_view_forms m, GroupListMainViewForms vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListMainViewForms) // Clone.tt Line: 201
            {
                var tvm = MainViewForm.ConvertToVM(t, new MainViewForm(vm)); // Clone.tt Line: 204
                vm.ListMainViewForms.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListMainViewForms' to 'proto_group_list_main_view_forms'
        public static Proto.Config.proto_group_list_main_view_forms ConvertToProto(GroupListMainViewForms vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_main_view_forms m = new Proto.Config.proto_group_list_main_view_forms(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListMainViewForms) // Clone.tt Line: 242
                m.ListMainViewForms.Add(MainViewForm.ConvertToProto((MainViewForm)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListMainViewForms)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<MainViewForm> ListMainViewForms // Property.tt Line: 8
        { 
            get { return this._ListMainViewForms; }
            set
            {
                if (this._ListMainViewForms != value)
                {
                    this.OnListMainViewFormsChanging(value);
                    _ListMainViewForms = value;
                    this.OnListMainViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<MainViewForm> _ListMainViewForms;
        IReadOnlyList<IMainViewForm> IGroupListMainViewForms.ListMainViewForms { get { return (this as GroupListMainViewForms).ListMainViewForms; } } // Property.tt Line: 26
        partial void OnListMainViewFormsChanging(ObservableCollection<MainViewForm> to); // Property.tt Line: 27
        partial void OnListMainViewFormsChanged();
        public MainViewForm this[int index] { get { return (MainViewForm)this.ListMainViewForms[index]; } }
        IMainViewForm IGroupListMainViewForms.this[int index] { get { return (MainViewForm)this.ListMainViewForms[index]; } }
        public void Add(MainViewForm item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListMainViewForms.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<MainViewForm> items) 
        { 
            Contract.Requires(items != null);
            this.ListMainViewForms.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListMainViewForms.Count; }
        int IGroupListMainViewForms.Count() { return this.Count(); }
        public void Remove(MainViewForm item) 
        {
            Contract.Requires(item != null);
            this.ListMainViewForms.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListMainViewForms.ListNodeGeneratorsSettings { get { return (this as GroupListMainViewForms).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { } // Class.tt Line: 6
    public partial class GroupListPropertiesTabs : ConfigObjectVmGenSettings<GroupListPropertiesTabs, GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IConfigAcceptVisitor, IGroupListPropertiesTabs // Class.tt Line: 7
    {
        #region CTOR
        public GroupListPropertiesTabs() : this(default(ITreeConfigNode))
        {
        }
        public GroupListPropertiesTabs(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesTabsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PropertiesTab)) // Clone.tt Line: 15
            {
                this.ListPropertiesTabs.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListPropertiesTabs Clone(ITreeConfigNode parent, IGroupListPropertiesTabs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListPropertiesTabs vm = new GroupListPropertiesTabs(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListPropertiesTabs) // Clone.tt Line: 52
                vm.ListPropertiesTabs.Add(PropertiesTab.Clone(vm, (PropertiesTab)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListPropertiesTabs to, IGroupListPropertiesTabs from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListPropertiesTabs.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListPropertiesTabs)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PropertiesTab.Update((PropertiesTab)t, (PropertiesTab)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListPropertiesTabs.Remove(t);
                }
                foreach (var tt in from.ListPropertiesTabs)
                {
                    bool isfound = false;
                    foreach (var t in to.ListPropertiesTabs.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PropertiesTab(to); // Clone.tt Line: 117
                        PropertiesTab.Update(p, (PropertiesTab)tt, isDeep);
                        to.ListPropertiesTabs.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListPropertiesTabs Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListPropertiesTabs.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListPropertiesTabs from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListPropertiesTabs.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_properties_tabs' to 'GroupListPropertiesTabs'
        public static GroupListPropertiesTabs ConvertToVM(Proto.Config.proto_group_list_properties_tabs m, GroupListPropertiesTabs vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListPropertiesTabs) // Clone.tt Line: 201
            {
                var tvm = PropertiesTab.ConvertToVM(t, new PropertiesTab(vm)); // Clone.tt Line: 204
                vm.ListPropertiesTabs.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListPropertiesTabs' to 'proto_group_list_properties_tabs'
        public static Proto.Config.proto_group_list_properties_tabs ConvertToProto(GroupListPropertiesTabs vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_properties_tabs m = new Proto.Config.proto_group_list_properties_tabs(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListPropertiesTabs) // Clone.tt Line: 242
                m.ListPropertiesTabs.Add(PropertiesTab.ConvertToProto((PropertiesTab)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListPropertiesTabs)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PropertiesTab> ListPropertiesTabs // Property.tt Line: 8
        { 
            get { return this._ListPropertiesTabs; }
            set
            {
                if (this._ListPropertiesTabs != value)
                {
                    this.OnListPropertiesTabsChanging(value);
                    _ListPropertiesTabs = value;
                    this.OnListPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PropertiesTab> _ListPropertiesTabs;
        IReadOnlyList<IPropertiesTab> IGroupListPropertiesTabs.ListPropertiesTabs { get { return (this as GroupListPropertiesTabs).ListPropertiesTabs; } } // Property.tt Line: 26
        partial void OnListPropertiesTabsChanging(ObservableCollection<PropertiesTab> to); // Property.tt Line: 27
        partial void OnListPropertiesTabsChanged();
        public PropertiesTab this[int index] { get { return (PropertiesTab)this.ListPropertiesTabs[index]; } }
        IPropertiesTab IGroupListPropertiesTabs.this[int index] { get { return (PropertiesTab)this.ListPropertiesTabs[index]; } }
        public void Add(PropertiesTab item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListPropertiesTabs.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<PropertiesTab> items) 
        { 
            Contract.Requires(items != null);
            this.ListPropertiesTabs.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListPropertiesTabs.Count; }
        int IGroupListPropertiesTabs.Count() { return this.Count(); }
        public void Remove(PropertiesTab item) 
        {
            Contract.Requires(item != null);
            this.ListPropertiesTabs.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListPropertiesTabs.ListNodeGeneratorsSettings { get { return (this as GroupListPropertiesTabs).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class PropertiesTabValidator : ValidatorBase<PropertiesTab, PropertiesTabValidator> { } // Class.tt Line: 6
    public partial class PropertiesTab : ConfigObjectVmGenSettings<PropertiesTab, PropertiesTabValidator>, IComparable<PropertiesTab>, IConfigAcceptVisitor, IPropertiesTab // Class.tt Line: 7
    {
        #region CTOR
        public PropertiesTab() : this(default(ITreeConfigNode))
        {
        }
        public PropertiesTab(ITreeConfigNode parent) 
            : base(parent, PropertiesTabValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 33
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 33
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static PropertiesTab Clone(ITreeConfigNode parent, IPropertiesTab from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PropertiesTab vm = new PropertiesTab(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 65
            vm.PropertyRefParentGuid = from.PropertyRefParentGuid; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PropertiesTab to, IPropertiesTab from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListProperties.Update((GroupListProperties)to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Update((GroupListPropertiesTabs)to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 141
            to.PropertyRefParentGuid = from.PropertyRefParentGuid; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PropertiesTab Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PropertiesTab.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PropertiesTab from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PropertiesTab.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_properties_tab' to 'PropertiesTab'
        public static PropertiesTab ConvertToVM(Proto.Config.proto_properties_tab m, PropertiesTab vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.GroupProperties == null) // Clone.tt Line: 213
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, (GroupListProperties)vm.GroupProperties); // Clone.tt Line: 219
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 213
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, (GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 219
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.PropertyIdGuid = m.PropertyIdGuid; // Clone.tt Line: 221
            vm.PropertyRefParentGuid = m.PropertyRefParentGuid; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PropertiesTab' to 'proto_properties_tab'
        public static Proto.Config.proto_properties_tab ConvertToProto(PropertiesTab vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_properties_tab m = new Proto.Config.proto_properties_tab(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto((GroupListProperties)vm.GroupProperties); // Clone.tt Line: 270
            m.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToProto((GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 270
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.PropertyIdGuid = vm.PropertyIdGuid; // Clone.tt Line: 276
            m.PropertyRefParentGuid = vm.PropertyRefParentGuid; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupPropertiesTabs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 55
        { 
            get { return this._GroupProperties; }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(ref value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListProperties _GroupProperties;
        IGroupListProperties IPropertiesTab.GroupProperties { get { return (this as PropertiesTab).GroupProperties; } } // Property.tt Line: 77
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 79
        partial void OnGroupPropertiesChanged();
        //IGroupListProperties IPropertiesTab.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 55
        { 
            get { return this._GroupPropertiesTabs; }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(ref value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        IGroupListPropertiesTabs IPropertiesTab.GroupPropertiesTabs { get { return (this as PropertiesTab).GroupPropertiesTabs; } } // Property.tt Line: 77
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 79
        partial void OnGroupPropertiesTabsChanged();
        //IGroupListPropertiesTabs IPropertiesTab.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        
        ///////////////////////////////////////////////////
        /// Create Index for foreign key navigation property
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        public bool IsIndexFk // Property.tt Line: 55
        { 
            get { return this._IsIndexFk; }
            set
            {
                if (this._IsIndexFk != value)
                {
                    this.OnIsIndexFkChanging(ref value);
                    this._IsIndexFk = value;
                    this.OnIsIndexFkChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsIndexFk;
        partial void OnIsIndexFkChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsIndexFkChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyIdGuid // Property.tt Line: 55
        { 
            get { return this._PropertyIdGuid; }
            set
            {
                if (this._PropertyIdGuid != value)
                {
                    this.OnPropertyIdGuidChanging(ref value);
                    this._PropertyIdGuid = value;
                    this.OnPropertyIdGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIdGuid = string.Empty;
        partial void OnPropertyIdGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIdGuidChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyRefParentGuid // Property.tt Line: 55
        { 
            get { return this._PropertyRefParentGuid; }
            set
            {
                if (this._PropertyRefParentGuid != value)
                {
                    this.OnPropertyRefParentGuidChanging(ref value);
                    this._PropertyRefParentGuid = value;
                    this.OnPropertyRefParentGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyRefParentGuid = string.Empty;
        partial void OnPropertyRefParentGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyRefParentGuidChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IPropertiesTab.ListNodeGeneratorsSettings { get { return (this as PropertiesTab).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { } // Class.tt Line: 6
    public partial class GroupListProperties : ConfigObjectVmGenSettings<GroupListProperties, GroupListPropertiesValidator>, IComparable<GroupListProperties>, IConfigAcceptVisitor, IGroupListProperties // Class.tt Line: 7
    {
        #region CTOR
        public GroupListProperties() : this(default(ITreeConfigNode))
        {
        }
        public GroupListProperties(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListProperties = new ConfigNodesCollection<Property>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Property)) // Clone.tt Line: 15
            {
                this.ListProperties.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListProperties Clone(ITreeConfigNode parent, IGroupListProperties from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListProperties vm = new GroupListProperties(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListProperties = new ConfigNodesCollection<Property>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListProperties) // Clone.tt Line: 52
                vm.ListProperties.Add(Property.Clone(vm, (Property)t, isDeep));
            vm.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListProperties to, IGroupListProperties from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListProperties.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListProperties)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Property.Update((Property)t, (Property)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListProperties.Remove(t);
                }
                foreach (var tt in from.ListProperties)
                {
                    bool isfound = false;
                    foreach (var t in to.ListProperties.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Property(to); // Clone.tt Line: 117
                        Property.Update(p, (Property)tt, isDeep);
                        to.ListProperties.Add(p);
                    }
                }
            }
            to.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListProperties Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListProperties.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListProperties from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListProperties.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_properties' to 'GroupListProperties'
        public static GroupListProperties ConvertToVM(Proto.Config.proto_group_list_properties m, GroupListProperties vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListProperties = new ConfigNodesCollection<Property>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListProperties) // Clone.tt Line: 201
            {
                var tvm = Property.ConvertToVM(t, new Property(vm)); // Clone.tt Line: 204
                vm.ListProperties.Add(tvm);
            }
            vm.LastGenPosition = m.LastGenPosition; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListProperties' to 'proto_group_list_properties'
        public static Proto.Config.proto_group_list_properties ConvertToProto(GroupListProperties vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_properties m = new Proto.Config.proto_group_list_properties(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListProperties) // Clone.tt Line: 242
                m.ListProperties.Add(Property.ConvertToProto((Property)t)); // Clone.tt Line: 246
            m.LastGenPosition = vm.LastGenPosition; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListProperties)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Property> ListProperties // Property.tt Line: 8
        { 
            get { return this._ListProperties; }
            set
            {
                if (this._ListProperties != value)
                {
                    this.OnListPropertiesChanging(value);
                    _ListProperties = value;
                    this.OnListPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Property> _ListProperties;
        IReadOnlyList<IProperty> IGroupListProperties.ListProperties { get { return (this as GroupListProperties).ListProperties; } } // Property.tt Line: 26
        partial void OnListPropertiesChanging(ObservableCollection<Property> to); // Property.tt Line: 27
        partial void OnListPropertiesChanged();
        public Property this[int index] { get { return (Property)this.ListProperties[index]; } }
        IProperty IGroupListProperties.this[int index] { get { return (Property)this.ListProperties[index]; } }
        public void Add(Property item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListProperties.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Property> items) 
        { 
            Contract.Requires(items != null);
            this.ListProperties.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListProperties.Count; }
        int IGroupListProperties.Count() { return this.Count(); }
        public void Remove(Property item) 
        {
            Contract.Requires(item != null);
            this.ListProperties.Remove(item); 
            item.Parent = null;
        }
        
        
        ///////////////////////////////////////////////////
        /// Last generated Protobuf field position
        ///////////////////////////////////////////////////
        [ReadOnly(true)]
        public uint LastGenPosition // Property.tt Line: 55
        { 
            get { return this._LastGenPosition; }
            set
            {
                if (this._LastGenPosition != value)
                {
                    this.OnLastGenPositionChanging(ref value);
                    this._LastGenPosition = value;
                    this.OnLastGenPositionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _LastGenPosition;
        partial void OnLastGenPositionChanging(ref uint to); // Property.tt Line: 79
        partial void OnLastGenPositionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListProperties.ListNodeGeneratorsSettings { get { return (this as GroupListProperties).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { } // Class.tt Line: 6
    public partial class Property : ConfigObjectVmGenSettings<Property, PropertyValidator>, IComparable<Property>, IConfigAcceptVisitor, IProperty // Class.tt Line: 7
    {
        #region CTOR
        public Property() : this(default(ITreeConfigNode))
        {
        }
        public Property(ITreeConfigNode parent) 
            : base(parent, PropertyValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 31
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Property Clone(ITreeConfigNode parent, IProperty from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Property vm = new Property(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(from.DataType, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.Position = from.Position; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Property to, IProperty from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.DataType.Update((DataType)to.DataType, from.DataType, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.Position = from.Position; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Property Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Property.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Property from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Property.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_property' to 'Property'
        public static Property ConvertToVM(Proto.Config.proto_property m, Property vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.DataType == null) // Clone.tt Line: 213
                vm.DataType = new DataType(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType, (DataType)vm.DataType); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.Position = m.Position; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Property' to 'proto_property'
        public static Proto.Config.proto_property ConvertToProto(Property vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_property m = new Proto.Config.proto_property(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto((DataType)vm.DataType); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.Position = vm.Position; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.DataType.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 55
        { 
            get { return this._DataType; }
            set
            {
                if (this._DataType != value)
                {
                    this.OnDataTypeChanging(ref value);
                    this._DataType = value;
                    this.OnDataTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private DataType _DataType;
        IDataType IProperty.DataType { get { return (this as Property).DataType; } } // Property.tt Line: 77
        partial void OnDataTypeChanging(ref DataType to); // Property.tt Line: 79
        partial void OnDataTypeChanged();
        //IDataType IProperty.DataType { get { return this._DataType; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        
        ///////////////////////////////////////////////////
        /// Protobuf field position
        /// Reserved positions: 1 - primary key
        ///////////////////////////////////////////////////
        [ReadOnly(true)]
        public uint Position // Property.tt Line: 55
        { 
            get { return this._Position; }
            set
            {
                if (this._Position != value)
                {
                    this.OnPositionChanging(ref value);
                    this._Position = value;
                    this.OnPositionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _Position;
        partial void OnPositionChanging(ref uint to); // Property.tt Line: 79
        partial void OnPositionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IProperty.ListNodeGeneratorsSettings { get { return (this as Property).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupConstantGroupsValidator : ValidatorBase<GroupConstantGroups, GroupConstantGroupsValidator> { } // Class.tt Line: 6
    public partial class GroupConstantGroups : ConfigObjectVmGenSettings<GroupConstantGroups, GroupConstantGroupsValidator>, IComparable<GroupConstantGroups>, IConfigAcceptVisitor, IGroupConstantGroups // Class.tt Line: 7
    {
        #region CTOR
        public GroupConstantGroups() : this(default(ITreeConfigNode))
        {
        }
        public GroupConstantGroups(ITreeConfigNode parent) 
            : base(parent, GroupConstantGroupsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListConstantGroups = new ConfigNodesCollection<GroupListConstants>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GroupListConstants)) // Clone.tt Line: 15
            {
                this.ListConstantGroups.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupConstantGroups Clone(ITreeConfigNode parent, IGroupConstantGroups from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupConstantGroups vm = new GroupConstantGroups(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.PrefixForDbTables = from.PrefixForDbTables; // Clone.tt Line: 65
            vm.ListConstantGroups = new ConfigNodesCollection<GroupListConstants>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListConstantGroups) // Clone.tt Line: 52
                vm.ListConstantGroups.Add(GroupListConstants.Clone(vm, (GroupListConstants)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupConstantGroups to, IGroupConstantGroups from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.PrefixForDbTables = from.PrefixForDbTables; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListConstantGroups.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListConstantGroups)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            GroupListConstants.Update((GroupListConstants)t, (GroupListConstants)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListConstantGroups.Remove(t);
                }
                foreach (var tt in from.ListConstantGroups)
                {
                    bool isfound = false;
                    foreach (var t in to.ListConstantGroups.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GroupListConstants(to); // Clone.tt Line: 117
                        GroupListConstants.Update(p, (GroupListConstants)tt, isDeep);
                        to.ListConstantGroups.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupConstantGroups Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupConstantGroups.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupConstantGroups from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupConstantGroups.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_constant_groups' to 'GroupConstantGroups'
        public static GroupConstantGroups ConvertToVM(Proto.Config.proto_group_constant_groups m, GroupConstantGroups vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.PrefixForDbTables = m.PrefixForDbTables; // Clone.tt Line: 221
            vm.ListConstantGroups = new ConfigNodesCollection<GroupListConstants>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListConstantGroups) // Clone.tt Line: 201
            {
                var tvm = GroupListConstants.ConvertToVM(t, new GroupListConstants(vm)); // Clone.tt Line: 204
                vm.ListConstantGroups.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupConstantGroups' to 'proto_group_constant_groups'
        public static Proto.Config.proto_group_constant_groups ConvertToProto(GroupConstantGroups vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_constant_groups m = new Proto.Config.proto_group_constant_groups(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.PrefixForDbTables = vm.PrefixForDbTables; // Clone.tt Line: 276
            foreach (var t in vm.ListConstantGroups) // Clone.tt Line: 242
                m.ListConstantGroups.Add(GroupListConstants.ConvertToProto((GroupListConstants)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListConstantGroups)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Db prefix")]
        [Description("Prefix for constants db table names. Used if set to use in config model")]
        public string PrefixForDbTables // Property.tt Line: 55
        { 
            get { return this._PrefixForDbTables; }
            set
            {
                if (this._PrefixForDbTables != value)
                {
                    this.OnPrefixForDbTablesChanging(ref value);
                    this._PrefixForDbTables = value;
                    this.OnPrefixForDbTablesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PrefixForDbTables = string.Empty;
        partial void OnPrefixForDbTablesChanging(ref string to); // Property.tt Line: 79
        partial void OnPrefixForDbTablesChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<GroupListConstants> ListConstantGroups // Property.tt Line: 8
        { 
            get { return this._ListConstantGroups; }
            set
            {
                if (this._ListConstantGroups != value)
                {
                    this.OnListConstantGroupsChanging(value);
                    _ListConstantGroups = value;
                    this.OnListConstantGroupsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GroupListConstants> _ListConstantGroups;
        IReadOnlyList<IGroupListConstants> IGroupConstantGroups.ListConstantGroups { get { return (this as GroupConstantGroups).ListConstantGroups; } } // Property.tt Line: 26
        partial void OnListConstantGroupsChanging(ObservableCollection<GroupListConstants> to); // Property.tt Line: 27
        partial void OnListConstantGroupsChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupConstantGroups.ListNodeGeneratorsSettings { get { return (this as GroupConstantGroups).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { } // Class.tt Line: 6
    public partial class GroupListConstants : ConfigObjectVmGenSettings<GroupListConstants, GroupListConstantsValidator>, IComparable<GroupListConstants>, IConfigAcceptVisitor, IGroupListConstants // Class.tt Line: 7
    {
        #region CTOR
        public GroupListConstants() : this(default(ITreeConfigNode))
        {
        }
        public GroupListConstants(ITreeConfigNode parent) 
            : base(parent, GroupListConstantsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListConstants = new ConfigNodesCollection<Constant>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Constant)) // Clone.tt Line: 15
            {
                this.ListConstants.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListConstants Clone(ITreeConfigNode parent, IGroupListConstants from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListConstants vm = new GroupListConstants(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListConstants = new ConfigNodesCollection<Constant>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListConstants) // Clone.tt Line: 52
                vm.ListConstants.Add(Constant.Clone(vm, (Constant)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListConstants to, IGroupListConstants from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListConstants.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListConstants)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Constant.Update((Constant)t, (Constant)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListConstants.Remove(t);
                }
                foreach (var tt in from.ListConstants)
                {
                    bool isfound = false;
                    foreach (var t in to.ListConstants.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Constant(to); // Clone.tt Line: 117
                        Constant.Update(p, (Constant)tt, isDeep);
                        to.ListConstants.Add(p);
                    }
                }
            }
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListConstants Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListConstants.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListConstants from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListConstants.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_constants' to 'GroupListConstants'
        public static GroupListConstants ConvertToVM(Proto.Config.proto_group_list_constants m, GroupListConstants vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListConstants = new ConfigNodesCollection<Constant>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListConstants) // Clone.tt Line: 201
            {
                var tvm = Constant.ConvertToVM(t, new Constant(vm)); // Clone.tt Line: 204
                vm.ListConstants.Add(tvm);
            }
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListConstants' to 'proto_group_list_constants'
        public static Proto.Config.proto_group_list_constants ConvertToProto(GroupListConstants vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_constants m = new Proto.Config.proto_group_list_constants(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListConstants) // Clone.tt Line: 242
                m.ListConstants.Add(Constant.ConvertToProto((Constant)t)); // Clone.tt Line: 246
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListConstants)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Constant> ListConstants // Property.tt Line: 8
        { 
            get { return this._ListConstants; }
            set
            {
                if (this._ListConstants != value)
                {
                    this.OnListConstantsChanging(value);
                    _ListConstants = value;
                    this.OnListConstantsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Constant> _ListConstants;
        IReadOnlyList<IConstant> IGroupListConstants.ListConstants { get { return (this as GroupListConstants).ListConstants; } } // Property.tt Line: 26
        partial void OnListConstantsChanging(ObservableCollection<Constant> to); // Property.tt Line: 27
        partial void OnListConstantsChanged();
        public Constant this[int index] { get { return (Constant)this.ListConstants[index]; } }
        IConstant IGroupListConstants.this[int index] { get { return (Constant)this.ListConstants[index]; } }
        public void Add(Constant item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListConstants.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Constant> items) 
        { 
            Contract.Requires(items != null);
            this.ListConstants.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListConstants.Count; }
        int IGroupListConstants.Count() { return this.Count(); }
        public void Remove(Constant item) 
        {
            Contract.Requires(item != null);
            this.ListConstants.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListConstants.ListNodeGeneratorsSettings { get { return (this as GroupListConstants).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    public partial class Constant : ConfigObjectVmGenSettings<Constant, ConstantValidator>, IComparable<Constant>, IConfigAcceptVisitor, IConstant // Class.tt Line: 7
    {
        #region CTOR
        public Constant() : this(default(ITreeConfigNode))
        {
        }
        public Constant(ITreeConfigNode parent) 
            : base(parent, ConstantValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 31
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Constant Clone(ITreeConfigNode parent, IConstant from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Constant vm = new Constant(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(from.DataType, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Constant to, IConstant from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.DataType.Update((DataType)to.DataType, from.DataType, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Constant Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Constant.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Constant from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Constant.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_constant' to 'Constant'
        public static Constant ConvertToVM(Proto.Config.proto_constant m, Constant vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.DataType == null) // Clone.tt Line: 213
                vm.DataType = new DataType(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType, (DataType)vm.DataType); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Constant' to 'proto_constant'
        public static Proto.Config.proto_constant ConvertToProto(Constant vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_constant m = new Proto.Config.proto_constant(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto((DataType)vm.DataType); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.DataType.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 55
        { 
            get { return this._DataType; }
            set
            {
                if (this._DataType != value)
                {
                    this.OnDataTypeChanging(ref value);
                    this._DataType = value;
                    this.OnDataTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private DataType _DataType;
        IDataType IConstant.DataType { get { return (this as Constant).DataType; } } // Property.tt Line: 77
        partial void OnDataTypeChanging(ref DataType to); // Property.tt Line: 79
        partial void OnDataTypeChanged();
        //IDataType IConstant.DataType { get { return this._DataType; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IConstant.ListNodeGeneratorsSettings { get { return (this as Constant).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { } // Class.tt Line: 6
    public partial class GroupListEnumerations : ConfigObjectVmGenSettings<GroupListEnumerations, GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IConfigAcceptVisitor, IGroupListEnumerations // Class.tt Line: 7
    {
        #region CTOR
        public GroupListEnumerations() : this(default(ITreeConfigNode))
        {
        }
        public GroupListEnumerations(ITreeConfigNode parent) 
            : base(parent, GroupListEnumerationsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListEnumerations = new ConfigNodesCollection<Enumeration>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Enumeration)) // Clone.tt Line: 15
            {
                this.ListEnumerations.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListEnumerations Clone(ITreeConfigNode parent, IGroupListEnumerations from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListEnumerations vm = new GroupListEnumerations(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListEnumerations) // Clone.tt Line: 52
                vm.ListEnumerations.Add(Enumeration.Clone(vm, (Enumeration)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListEnumerations to, IGroupListEnumerations from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListEnumerations.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListEnumerations)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Enumeration.Update((Enumeration)t, (Enumeration)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListEnumerations.Remove(t);
                }
                foreach (var tt in from.ListEnumerations)
                {
                    bool isfound = false;
                    foreach (var t in to.ListEnumerations.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Enumeration(to); // Clone.tt Line: 117
                        Enumeration.Update(p, (Enumeration)tt, isDeep);
                        to.ListEnumerations.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListEnumerations Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListEnumerations.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListEnumerations from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListEnumerations.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_enumerations' to 'GroupListEnumerations'
        public static GroupListEnumerations ConvertToVM(Proto.Config.proto_group_list_enumerations m, GroupListEnumerations vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListEnumerations) // Clone.tt Line: 201
            {
                var tvm = Enumeration.ConvertToVM(t, new Enumeration(vm)); // Clone.tt Line: 204
                vm.ListEnumerations.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListEnumerations' to 'proto_group_list_enumerations'
        public static Proto.Config.proto_group_list_enumerations ConvertToProto(GroupListEnumerations vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_enumerations m = new Proto.Config.proto_group_list_enumerations(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListEnumerations) // Clone.tt Line: 242
                m.ListEnumerations.Add(Enumeration.ConvertToProto((Enumeration)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListEnumerations)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Enumeration> ListEnumerations // Property.tt Line: 8
        { 
            get { return this._ListEnumerations; }
            set
            {
                if (this._ListEnumerations != value)
                {
                    this.OnListEnumerationsChanging(value);
                    _ListEnumerations = value;
                    this.OnListEnumerationsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Enumeration> _ListEnumerations;
        IReadOnlyList<IEnumeration> IGroupListEnumerations.ListEnumerations { get { return (this as GroupListEnumerations).ListEnumerations; } } // Property.tt Line: 26
        partial void OnListEnumerationsChanging(ObservableCollection<Enumeration> to); // Property.tt Line: 27
        partial void OnListEnumerationsChanged();
        public Enumeration this[int index] { get { return (Enumeration)this.ListEnumerations[index]; } }
        IEnumeration IGroupListEnumerations.this[int index] { get { return (Enumeration)this.ListEnumerations[index]; } }
        public void Add(Enumeration item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListEnumerations.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Enumeration> items) 
        { 
            Contract.Requires(items != null);
            this.ListEnumerations.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListEnumerations.Count; }
        int IGroupListEnumerations.Count() { return this.Count(); }
        public void Remove(Enumeration item) 
        {
            Contract.Requires(item != null);
            this.ListEnumerations.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListEnumerations.ListNodeGeneratorsSettings { get { return (this as GroupListEnumerations).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { } // Class.tt Line: 6
    public partial class Enumeration : ConfigObjectVmGenSettings<Enumeration, EnumerationValidator>, IComparable<Enumeration>, IConfigAcceptVisitor, IEnumeration // Class.tt Line: 7
    {
        #region CTOR
        public Enumeration() : this(default(ITreeConfigNode))
        {
        }
        public Enumeration(ITreeConfigNode parent) 
            : base(parent, EnumerationValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(EnumerationPair)) // Clone.tt Line: 15
            {
                this.ListEnumerationPairs.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Enumeration Clone(ITreeConfigNode parent, IEnumeration from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Enumeration vm = new Enumeration(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 65
            vm.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 65
            vm.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListEnumerationPairs) // Clone.tt Line: 52
                vm.ListEnumerationPairs.Add(EnumerationPair.Clone(vm, (EnumerationPair)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Enumeration to, IEnumeration from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 141
            to.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListEnumerationPairs.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListEnumerationPairs)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            EnumerationPair.Update((EnumerationPair)t, (EnumerationPair)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListEnumerationPairs.Remove(t);
                }
                foreach (var tt in from.ListEnumerationPairs)
                {
                    bool isfound = false;
                    foreach (var t in to.ListEnumerationPairs.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new EnumerationPair(to); // Clone.tt Line: 117
                        EnumerationPair.Update(p, (EnumerationPair)tt, isDeep);
                        to.ListEnumerationPairs.Add(p);
                    }
                }
            }
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Enumeration Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Enumeration.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Enumeration from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Enumeration.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_enumeration' to 'Enumeration'
        public static Enumeration ConvertToVM(Proto.Config.proto_enumeration m, Enumeration vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.DataTypeEnum = (EnumEnumerationType)m.DataTypeEnum; // Clone.tt Line: 221
            vm.DataTypeLength = m.DataTypeLength; // Clone.tt Line: 221
            vm.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListEnumerationPairs) // Clone.tt Line: 201
            {
                var tvm = EnumerationPair.ConvertToVM(t, new EnumerationPair(vm)); // Clone.tt Line: 204
                vm.ListEnumerationPairs.Add(tvm);
            }
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Enumeration' to 'proto_enumeration'
        public static Proto.Config.proto_enumeration ConvertToProto(Enumeration vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_enumeration m = new Proto.Config.proto_enumeration(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.DataTypeEnum = (Proto.Config.enum_enumeration_type)vm.DataTypeEnum; // Clone.tt Line: 274
            m.DataTypeLength = vm.DataTypeLength; // Clone.tt Line: 276
            foreach (var t in vm.ListEnumerationPairs) // Clone.tt Line: 242
                m.ListEnumerationPairs.Add(EnumerationPair.ConvertToProto((EnumerationPair)t)); // Clone.tt Line: 246
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListEnumerationPairs)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// Enumeration element type
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        [DisplayName("Type")]
        public EnumEnumerationType DataTypeEnum // Property.tt Line: 55
        { 
            get { return this._DataTypeEnum; }
            set
            {
                if (this._DataTypeEnum != value)
                {
                    this.OnDataTypeEnumChanging(ref value);
                    this._DataTypeEnum = value;
                    this.OnDataTypeEnumChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumEnumerationType _DataTypeEnum;
        partial void OnDataTypeEnumChanging(ref EnumEnumerationType to); // Property.tt Line: 79
        partial void OnDataTypeEnumChanged();
        
        
        ///////////////////////////////////////////////////
        /// Length of string if 'STRING' is selected as enumeration element type
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(5)]
        [DisplayName("Length")]
        public int DataTypeLength // Property.tt Line: 55
        { 
            get { return this._DataTypeLength; }
            set
            {
                if (this._DataTypeLength != value)
                {
                    this.OnDataTypeLengthChanging(ref value);
                    this._DataTypeLength = value;
                    this.OnDataTypeLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _DataTypeLength;
        partial void OnDataTypeLengthChanging(ref int to); // Property.tt Line: 79
        partial void OnDataTypeLengthChanged();
        
        [DisplayName("Elements")]
        [NewItemTypes(typeof(EnumerationPair))]
        public ConfigNodesCollection<EnumerationPair> ListEnumerationPairs // Property.tt Line: 8
        { 
            get { return this._ListEnumerationPairs; }
            set
            {
                if (this._ListEnumerationPairs != value)
                {
                    this.OnListEnumerationPairsChanging(value);
                    _ListEnumerationPairs = value;
                    this.OnListEnumerationPairsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<EnumerationPair> _ListEnumerationPairs;
        IReadOnlyList<IEnumerationPair> IEnumeration.ListEnumerationPairs { get { return (this as Enumeration).ListEnumerationPairs; } } // Property.tt Line: 26
        partial void OnListEnumerationPairsChanging(ObservableCollection<EnumerationPair> to); // Property.tt Line: 27
        partial void OnListEnumerationPairsChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IEnumeration.ListNodeGeneratorsSettings { get { return (this as Enumeration).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { } // Class.tt Line: 6
    public partial class EnumerationPair : ConfigObjectVmGenSettings<EnumerationPair, EnumerationPairValidator>, IComparable<EnumerationPair>, IConfigAcceptVisitor, IEnumerationPair // Class.tt Line: 7
    {
        #region CTOR
        public EnumerationPair() : this(default(ITreeConfigNode))
        {
        }
        public EnumerationPair(ITreeConfigNode parent) 
            : base(parent, EnumerationPairValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static EnumerationPair Clone(ITreeConfigNode parent, IEnumerationPair from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            EnumerationPair vm = new EnumerationPair(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.Value = from.Value; // Clone.tt Line: 65
            vm.IsDefault = from.IsDefault; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(EnumerationPair to, IEnumerationPair from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.Value = from.Value; // Clone.tt Line: 141
            to.IsDefault = from.IsDefault; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override EnumerationPair Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return EnumerationPair.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(EnumerationPair from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            EnumerationPair.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_enumeration_pair' to 'EnumerationPair'
        public static EnumerationPair ConvertToVM(Proto.Config.proto_enumeration_pair m, EnumerationPair vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.Value = m.Value; // Clone.tt Line: 221
            vm.IsDefault = m.IsDefault; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
        public static Proto.Config.proto_enumeration_pair ConvertToProto(EnumerationPair vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_enumeration_pair m = new Proto.Config.proto_enumeration_pair(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.Value = vm.Value; // Clone.tt Line: 276
            m.IsDefault = vm.IsDefault; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        [DisplayName("Name")]
        [Description("Enumeration element name")]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(5)]
        [Description("Description of enumeration element")]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(3)]
        [DisplayName("Value")]
        [Description("Enumeration element value")]
        public string Value // Property.tt Line: 55
        { 
            get { return this._Value; }
            set
            {
                if (this._Value != value)
                {
                    this.OnValueChanging(ref value);
                    this._Value = value;
                    this.OnValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Value = string.Empty;
        partial void OnValueChanging(ref string to); // Property.tt Line: 79
        partial void OnValueChanged();
        
        [PropertyOrderAttribute(3)]
        [DisplayName("Is default")]
        [Description("Used as default value for enumeration")]
        public bool IsDefault // Property.tt Line: 55
        { 
            get { return this._IsDefault; }
            set
            {
                if (this._IsDefault != value)
                {
                    this.OnIsDefaultChanging(ref value);
                    this._IsDefault = value;
                    this.OnIsDefaultChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsDefault;
        partial void OnIsDefaultChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsDefaultChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IEnumerationPair.ListNodeGeneratorsSettings { get { return (this as EnumerationPair).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class CatalogFolderValidator : ValidatorBase<CatalogFolder, CatalogFolderValidator> { } // Class.tt Line: 6
    public partial class CatalogFolder : ConfigObjectVmGenSettings<CatalogFolder, CatalogFolderValidator>, IComparable<CatalogFolder>, IConfigAcceptVisitor, ICatalogFolder // Class.tt Line: 7
    {
        #region CTOR
        public CatalogFolder() : this(default(ITreeConfigNode))
        {
        }
        public CatalogFolder(ITreeConfigNode parent) 
            : base(parent, CatalogFolderValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 33
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 33
            this.CodePropertySettings = new CatalogCodePropertySettings(); // Class.tt Line: 31
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static CatalogFolder Clone(ITreeConfigNode parent, ICatalogFolder from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            CatalogFolder vm = new CatalogFolder(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            vm.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 65
            vm.UseCodeProperty = from.UseCodeProperty.HasValue ? from.UseCodeProperty.Value : (bool?)null; // Clone.tt Line: 58
            if (isDeep) // Clone.tt Line: 62
                vm.CodePropertySettings = vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.Clone(from.CodePropertySettings, isDeep);
            vm.PropertyCodeGuid = from.PropertyCodeGuid; // Clone.tt Line: 65
            vm.UseNameProperty = from.UseNameProperty.HasValue ? from.UseNameProperty.Value : (bool?)null; // Clone.tt Line: 58
            vm.MaxNameLength = from.MaxNameLength; // Clone.tt Line: 65
            vm.PropertyNameGuid = from.PropertyNameGuid; // Clone.tt Line: 65
            vm.UseDescriptionProperty = from.UseDescriptionProperty.HasValue ? from.UseDescriptionProperty.Value : (bool?)null; // Clone.tt Line: 58
            vm.MaxDescriptionLength = from.MaxDescriptionLength; // Clone.tt Line: 65
            vm.PropertyDescriptionGuid = from.PropertyDescriptionGuid; // Clone.tt Line: 65
            vm.PropertyRefSelfGuid = from.PropertyRefSelfGuid; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(CatalogFolder to, ICatalogFolder from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListProperties.Update((GroupListProperties)to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Update((GroupListPropertiesTabs)to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            to.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 141
            to.UseCodeProperty = from.UseCodeProperty.HasValue ? from.UseCodeProperty.Value : (bool?)null; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.Update((CatalogCodePropertySettings)to.CodePropertySettings, from.CodePropertySettings, isDeep);
            to.PropertyCodeGuid = from.PropertyCodeGuid; // Clone.tt Line: 141
            to.UseNameProperty = from.UseNameProperty.HasValue ? from.UseNameProperty.Value : (bool?)null; // Clone.tt Line: 136
            to.MaxNameLength = from.MaxNameLength; // Clone.tt Line: 141
            to.PropertyNameGuid = from.PropertyNameGuid; // Clone.tt Line: 141
            to.UseDescriptionProperty = from.UseDescriptionProperty.HasValue ? from.UseDescriptionProperty.Value : (bool?)null; // Clone.tt Line: 136
            to.MaxDescriptionLength = from.MaxDescriptionLength; // Clone.tt Line: 141
            to.PropertyDescriptionGuid = from.PropertyDescriptionGuid; // Clone.tt Line: 141
            to.PropertyRefSelfGuid = from.PropertyRefSelfGuid; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override CatalogFolder Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return CatalogFolder.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(CatalogFolder from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            CatalogFolder.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_catalog_folder' to 'CatalogFolder'
        public static CatalogFolder ConvertToVM(Proto.Config.proto_catalog_folder m, CatalogFolder vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            if (vm.GroupProperties == null) // Clone.tt Line: 213
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, (GroupListProperties)vm.GroupProperties); // Clone.tt Line: 219
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 213
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, (GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 219
            vm.PropertyIdGuid = m.PropertyIdGuid; // Clone.tt Line: 221
            vm.UseCodeProperty = m.UseCodeProperty.HasValue ? (bool?)m.UseCodeProperty.Value : (bool?)null; // Clone.tt Line: 221
            if (vm.CodePropertySettings == null) // Clone.tt Line: 213
                vm.CodePropertySettings = new CatalogCodePropertySettings(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.ConvertToVM(m.CodePropertySettings, (CatalogCodePropertySettings)vm.CodePropertySettings); // Clone.tt Line: 219
            vm.PropertyCodeGuid = m.PropertyCodeGuid; // Clone.tt Line: 221
            vm.UseNameProperty = m.UseNameProperty.HasValue ? (bool?)m.UseNameProperty.Value : (bool?)null; // Clone.tt Line: 221
            vm.MaxNameLength = m.MaxNameLength; // Clone.tt Line: 221
            vm.PropertyNameGuid = m.PropertyNameGuid; // Clone.tt Line: 221
            vm.UseDescriptionProperty = m.UseDescriptionProperty.HasValue ? (bool?)m.UseDescriptionProperty.Value : (bool?)null; // Clone.tt Line: 221
            vm.MaxDescriptionLength = m.MaxDescriptionLength; // Clone.tt Line: 221
            vm.PropertyDescriptionGuid = m.PropertyDescriptionGuid; // Clone.tt Line: 221
            vm.PropertyRefSelfGuid = m.PropertyRefSelfGuid; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'CatalogFolder' to 'proto_catalog_folder'
        public static Proto.Config.proto_catalog_folder ConvertToProto(CatalogFolder vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_catalog_folder m = new Proto.Config.proto_catalog_folder(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto((GroupListProperties)vm.GroupProperties); // Clone.tt Line: 270
            m.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToProto((GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 270
            m.PropertyIdGuid = vm.PropertyIdGuid; // Clone.tt Line: 276
            m.UseCodeProperty = new Proto.Config.bool_nullable(); // Clone.tt Line: 253
            m.UseCodeProperty.HasValue = vm.UseCodeProperty.HasValue;
            if (vm.UseCodeProperty.HasValue)
                m.UseCodeProperty.Value = vm.UseCodeProperty.Value;
            m.CodePropertySettings = vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.ConvertToProto((CatalogCodePropertySettings)vm.CodePropertySettings); // Clone.tt Line: 270
            m.PropertyCodeGuid = vm.PropertyCodeGuid; // Clone.tt Line: 276
            m.UseNameProperty = new Proto.Config.bool_nullable(); // Clone.tt Line: 253
            m.UseNameProperty.HasValue = vm.UseNameProperty.HasValue;
            if (vm.UseNameProperty.HasValue)
                m.UseNameProperty.Value = vm.UseNameProperty.Value;
            m.MaxNameLength = vm.MaxNameLength; // Clone.tt Line: 276
            m.PropertyNameGuid = vm.PropertyNameGuid; // Clone.tt Line: 276
            m.UseDescriptionProperty = new Proto.Config.bool_nullable(); // Clone.tt Line: 253
            m.UseDescriptionProperty.HasValue = vm.UseDescriptionProperty.HasValue;
            if (vm.UseDescriptionProperty.HasValue)
                m.UseDescriptionProperty.Value = vm.UseDescriptionProperty.Value;
            m.MaxDescriptionLength = vm.MaxDescriptionLength; // Clone.tt Line: 276
            m.PropertyDescriptionGuid = vm.PropertyDescriptionGuid; // Clone.tt Line: 276
            m.PropertyRefSelfGuid = vm.PropertyRefSelfGuid; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupPropertiesTabs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.CodePropertySettings.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 55
        { 
            get { return this._GroupProperties; }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(ref value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListProperties _GroupProperties;
        IGroupListProperties ICatalogFolder.GroupProperties { get { return (this as CatalogFolder).GroupProperties; } } // Property.tt Line: 77
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 79
        partial void OnGroupPropertiesChanged();
        //IGroupListProperties ICatalogFolder.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 55
        { 
            get { return this._GroupPropertiesTabs; }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(ref value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        IGroupListPropertiesTabs ICatalogFolder.GroupPropertiesTabs { get { return (this as CatalogFolder).GroupPropertiesTabs; } } // Property.tt Line: 77
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 79
        partial void OnGroupPropertiesTabsChanged();
        //IGroupListPropertiesTabs ICatalogFolder.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public string PropertyIdGuid // Property.tt Line: 55
        { 
            get { return this._PropertyIdGuid; }
            set
            {
                if (this._PropertyIdGuid != value)
                {
                    this.OnPropertyIdGuidChanging(ref value);
                    this._PropertyIdGuid = value;
                    this.OnPropertyIdGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIdGuid = string.Empty;
        partial void OnPropertyIdGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIdGuidChanged();
        
        [PropertyOrderAttribute(21)]
        [DisplayName("Use Code")]
        [Description("Use Code property for catalog item")]
        public bool? UseCodeProperty // Property.tt Line: 55
        { 
            get { return this._UseCodeProperty; }
            set
            {
                if (this._UseCodeProperty != value)
                {
                    this.OnUseCodePropertyChanging(ref value);
                    this._UseCodeProperty = value;
                    this.OnUseCodePropertyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool? _UseCodeProperty;
        partial void OnUseCodePropertyChanging(ref bool? to); // Property.tt Line: 79
        partial void OnUseCodePropertyChanged();
        //Ibool? ICatalogFolder.UseCodeProperty { get { return this._UseCodeProperty; } }
        
        [PropertyOrderAttribute(22)]
        [ExpandableObjectAttribute()]
        [DisplayName("Code")]
        [Description("Code property settings for catalog item")]
        public CatalogCodePropertySettings CodePropertySettings // Property.tt Line: 55
        { 
            get { return this._CodePropertySettings; }
            set
            {
                if (this._CodePropertySettings != value)
                {
                    this.OnCodePropertySettingsChanging(ref value);
                    this._CodePropertySettings = value;
                    this.OnCodePropertySettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private CatalogCodePropertySettings _CodePropertySettings;
        ICatalogCodePropertySettings ICatalogFolder.CodePropertySettings { get { return (this as CatalogFolder).CodePropertySettings; } } // Property.tt Line: 77
        partial void OnCodePropertySettingsChanging(ref CatalogCodePropertySettings to); // Property.tt Line: 79
        partial void OnCodePropertySettingsChanged();
        //ICatalogCodePropertySettings ICatalogFolder.CodePropertySettings { get { return this._CodePropertySettings; } }
        
        [BrowsableAttribute(false)]
        public string PropertyCodeGuid // Property.tt Line: 55
        { 
            get { return this._PropertyCodeGuid; }
            set
            {
                if (this._PropertyCodeGuid != value)
                {
                    this.OnPropertyCodeGuidChanging(ref value);
                    this._PropertyCodeGuid = value;
                    this.OnPropertyCodeGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyCodeGuid = string.Empty;
        partial void OnPropertyCodeGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyCodeGuidChanged();
        
        [PropertyOrderAttribute(41)]
        [DisplayName("Use Name")]
        [Description("Use Name property for catalog item")]
        public bool? UseNameProperty // Property.tt Line: 55
        { 
            get { return this._UseNameProperty; }
            set
            {
                if (this._UseNameProperty != value)
                {
                    this.OnUseNamePropertyChanging(ref value);
                    this._UseNameProperty = value;
                    this.OnUseNamePropertyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool? _UseNameProperty;
        partial void OnUseNamePropertyChanging(ref bool? to); // Property.tt Line: 79
        partial void OnUseNamePropertyChanged();
        //Ibool? ICatalogFolder.UseNameProperty { get { return this._UseNameProperty; } }
        
        [PropertyOrderAttribute(42)]
        [DisplayName("Max Length")]
        [Description("Maximum catalog item name length. If zero, than unlimited length")]
        public uint MaxNameLength // Property.tt Line: 55
        { 
            get { return this._MaxNameLength; }
            set
            {
                if (this._MaxNameLength != value)
                {
                    this.OnMaxNameLengthChanging(ref value);
                    this._MaxNameLength = value;
                    this.OnMaxNameLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _MaxNameLength;
        partial void OnMaxNameLengthChanging(ref uint to); // Property.tt Line: 79
        partial void OnMaxNameLengthChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyNameGuid // Property.tt Line: 55
        { 
            get { return this._PropertyNameGuid; }
            set
            {
                if (this._PropertyNameGuid != value)
                {
                    this.OnPropertyNameGuidChanging(ref value);
                    this._PropertyNameGuid = value;
                    this.OnPropertyNameGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyNameGuid = string.Empty;
        partial void OnPropertyNameGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyNameGuidChanged();
        
        [PropertyOrderAttribute(51)]
        [DisplayName("Use Description")]
        [Description("Use Description property for catalog item")]
        public bool? UseDescriptionProperty // Property.tt Line: 55
        { 
            get { return this._UseDescriptionProperty; }
            set
            {
                if (this._UseDescriptionProperty != value)
                {
                    this.OnUseDescriptionPropertyChanging(ref value);
                    this._UseDescriptionProperty = value;
                    this.OnUseDescriptionPropertyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool? _UseDescriptionProperty;
        partial void OnUseDescriptionPropertyChanging(ref bool? to); // Property.tt Line: 79
        partial void OnUseDescriptionPropertyChanged();
        //Ibool? ICatalogFolder.UseDescriptionProperty { get { return this._UseDescriptionProperty; } }
        
        [PropertyOrderAttribute(52)]
        [DisplayName("Max Length")]
        [Description("Maximum catalog item description length. If zero, than unlimited length")]
        public uint MaxDescriptionLength // Property.tt Line: 55
        { 
            get { return this._MaxDescriptionLength; }
            set
            {
                if (this._MaxDescriptionLength != value)
                {
                    this.OnMaxDescriptionLengthChanging(ref value);
                    this._MaxDescriptionLength = value;
                    this.OnMaxDescriptionLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _MaxDescriptionLength;
        partial void OnMaxDescriptionLengthChanging(ref uint to); // Property.tt Line: 79
        partial void OnMaxDescriptionLengthChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyDescriptionGuid // Property.tt Line: 55
        { 
            get { return this._PropertyDescriptionGuid; }
            set
            {
                if (this._PropertyDescriptionGuid != value)
                {
                    this.OnPropertyDescriptionGuidChanging(ref value);
                    this._PropertyDescriptionGuid = value;
                    this.OnPropertyDescriptionGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyDescriptionGuid = string.Empty;
        partial void OnPropertyDescriptionGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyDescriptionGuidChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyRefSelfGuid // Property.tt Line: 55
        { 
            get { return this._PropertyRefSelfGuid; }
            set
            {
                if (this._PropertyRefSelfGuid != value)
                {
                    this.OnPropertyRefSelfGuidChanging(ref value);
                    this._PropertyRefSelfGuid = value;
                    this.OnPropertyRefSelfGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyRefSelfGuid = string.Empty;
        partial void OnPropertyRefSelfGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyRefSelfGuidChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> ICatalogFolder.ListNodeGeneratorsSettings { get { return (this as CatalogFolder).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class CatalogCodePropertySettingsValidator : ValidatorBase<CatalogCodePropertySettings, CatalogCodePropertySettingsValidator> { } // Class.tt Line: 6
    public partial class CatalogCodePropertySettings : VmValidatableWithSeverity<CatalogCodePropertySettings, CatalogCodePropertySettingsValidator>, ICatalogCodePropertySettings // Class.tt Line: 7
    {
        #region CTOR
        public CatalogCodePropertySettings() 
            : base(CatalogCodePropertySettingsValidator.Validator) // Class.tt Line: 45
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
        public static CatalogCodePropertySettings Clone(ICatalogCodePropertySettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            CatalogCodePropertySettings vm = new CatalogCodePropertySettings();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Type = from.Type; // Clone.tt Line: 65
            vm.Length = from.Length; // Clone.tt Line: 65
            vm.SequenceGuid = from.SequenceGuid; // Clone.tt Line: 65
            vm.UniqueScope = from.UniqueScope; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(CatalogCodePropertySettings to, ICatalogCodePropertySettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Type = from.Type; // Clone.tt Line: 141
            to.Length = from.Length; // Clone.tt Line: 141
            to.SequenceGuid = from.SequenceGuid; // Clone.tt Line: 141
            to.UniqueScope = from.UniqueScope; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override CatalogCodePropertySettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return CatalogCodePropertySettings.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(CatalogCodePropertySettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            CatalogCodePropertySettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_catalog_code_property_settings' to 'CatalogCodePropertySettings'
        public static CatalogCodePropertySettings ConvertToVM(Proto.Config.proto_catalog_code_property_settings m, CatalogCodePropertySettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Type = (EnumCatalogCodeType)m.Type; // Clone.tt Line: 221
            vm.Length = m.Length; // Clone.tt Line: 221
            vm.SequenceGuid = m.SequenceGuid; // Clone.tt Line: 221
            vm.UniqueScope = (EnumCatalogCodeUniqueScope)m.UniqueScope; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'CatalogCodePropertySettings' to 'proto_catalog_code_property_settings'
        public static Proto.Config.proto_catalog_code_property_settings ConvertToProto(CatalogCodePropertySettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_catalog_code_property_settings m = new Proto.Config.proto_catalog_code_property_settings(); // Clone.tt Line: 239
            m.Type = (Proto.Config.proto_enum_catalog_code_type)vm.Type; // Clone.tt Line: 274
            m.Length = vm.Length; // Clone.tt Line: 276
            m.SequenceGuid = vm.SequenceGuid; // Clone.tt Line: 276
            m.UniqueScope = (Proto.Config.proto_enum_catalog_code_unique_scope)vm.UniqueScope; // Clone.tt Line: 274
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        [PropertyOrderAttribute(3)]
        [DisplayName("Type")]
        [Description("Code type")]
        public EnumCatalogCodeType Type // Property.tt Line: 55
        { 
            get { return this._Type; }
            set
            {
                if (this._Type != value)
                {
                    this.OnTypeChanging(ref value);
                    this._Type = value;
                    this.OnTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumCatalogCodeType _Type;
        partial void OnTypeChanging(ref EnumCatalogCodeType to); // Property.tt Line: 79
        partial void OnTypeChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Length")]
        [Description("Length is number of decimal digits for numbers, string length for text")]
        public uint Length // Property.tt Line: 55
        { 
            get { return this._Length; }
            set
            {
                if (this._Length != value)
                {
                    this.OnLengthChanging(ref value);
                    this._Length = value;
                    this.OnLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _Length;
        partial void OnLengthChanging(ref uint to); // Property.tt Line: 79
        partial void OnLengthChanged();
        
        [PropertyOrderAttribute(5)]
        [DisplayName("Sequence")]
        [Description("Sequence for auto code generation")]
        public string SequenceGuid // Property.tt Line: 55
        { 
            get { return this._SequenceGuid; }
            set
            {
                if (this._SequenceGuid != value)
                {
                    this.OnSequenceGuidChanging(ref value);
                    this._SequenceGuid = value;
                    this.OnSequenceGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _SequenceGuid = string.Empty;
        partial void OnSequenceGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnSequenceGuidChanged();
        
        [PropertyOrderAttribute(7)]
        [DisplayName("Unique Scope")]
        [Description("Code has to be unique in selected scope")]
        public EnumCatalogCodeUniqueScope UniqueScope // Property.tt Line: 55
        { 
            get { return this._UniqueScope; }
            set
            {
                if (this._UniqueScope != value)
                {
                    this.OnUniqueScopeChanging(ref value);
                    this._UniqueScope = value;
                    this.OnUniqueScopeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumCatalogCodeUniqueScope _UniqueScope;
        partial void OnUniqueScopeChanging(ref EnumCatalogCodeUniqueScope to); // Property.tt Line: 79
        partial void OnUniqueScopeChanged();
        #endregion Properties
    }
    public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { } // Class.tt Line: 6
    public partial class Catalog : ConfigObjectVmGenSettings<Catalog, CatalogValidator>, IComparable<Catalog>, IConfigAcceptVisitor, ICatalog // Class.tt Line: 7
    {
        #region CTOR
        public Catalog() : this(default(ITreeConfigNode))
        {
        }
        public Catalog(ITreeConfigNode parent) 
            : base(parent, CatalogValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.Folder = new CatalogFolder(this); // Class.tt Line: 33
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 33
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 33
            this.GroupForms = new GroupListForms(this); // Class.tt Line: 33
            this.GroupReports = new GroupListReports(this); // Class.tt Line: 33
            this.CodePropertySettings = new CatalogCodePropertySettings(); // Class.tt Line: 31
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Catalog Clone(ITreeConfigNode parent, ICatalog from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Catalog vm = new Catalog(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.Folder = vSharpStudio.vm.ViewModels.CatalogFolder.Clone(vm, from.Folder, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.ItemIconType = from.ItemIconType; // Clone.tt Line: 65
            vm.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 65
            vm.UseCodeProperty = from.UseCodeProperty; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.CodePropertySettings = vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.Clone(from.CodePropertySettings, isDeep);
            vm.PropertyCodeGuid = from.PropertyCodeGuid; // Clone.tt Line: 65
            vm.UseNameProperty = from.UseNameProperty; // Clone.tt Line: 65
            vm.MaxNameLength = from.MaxNameLength; // Clone.tt Line: 65
            vm.PropertyNameGuid = from.PropertyNameGuid; // Clone.tt Line: 65
            vm.UseDescriptionProperty = from.UseDescriptionProperty; // Clone.tt Line: 65
            vm.MaxDescriptionLength = from.MaxDescriptionLength; // Clone.tt Line: 65
            vm.PropertyDescriptionGuid = from.PropertyDescriptionGuid; // Clone.tt Line: 65
            vm.UseFolderTypeExplicitly = from.UseFolderTypeExplicitly; // Clone.tt Line: 65
            vm.PropertyIsFolderGuid = from.PropertyIsFolderGuid; // Clone.tt Line: 65
            vm.PropertyIsOpenGuid = from.PropertyIsOpenGuid; // Clone.tt Line: 65
            vm.UseTree = from.UseTree; // Clone.tt Line: 65
            vm.GroupIconType = from.GroupIconType; // Clone.tt Line: 65
            vm.MaxTreeLevels = from.MaxTreeLevels; // Clone.tt Line: 65
            vm.UseSeparatePropertiesForGroups = from.UseSeparatePropertiesForGroups; // Clone.tt Line: 65
            vm.PropertyRefSelfGuid = from.PropertyRefSelfGuid; // Clone.tt Line: 65
            vm.PropertyRefFolderGuid = from.PropertyRefFolderGuid; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Catalog to, ICatalog from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.CatalogFolder.Update((CatalogFolder)to.Folder, from.Folder, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListProperties.Update((GroupListProperties)to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Update((GroupListPropertiesTabs)to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListForms.Update((GroupListForms)to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListReports.Update((GroupListReports)to.GroupReports, from.GroupReports, isDeep);
            to.ItemIconType = from.ItemIconType; // Clone.tt Line: 141
            to.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 141
            to.UseCodeProperty = from.UseCodeProperty; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.Update((CatalogCodePropertySettings)to.CodePropertySettings, from.CodePropertySettings, isDeep);
            to.PropertyCodeGuid = from.PropertyCodeGuid; // Clone.tt Line: 141
            to.UseNameProperty = from.UseNameProperty; // Clone.tt Line: 141
            to.MaxNameLength = from.MaxNameLength; // Clone.tt Line: 141
            to.PropertyNameGuid = from.PropertyNameGuid; // Clone.tt Line: 141
            to.UseDescriptionProperty = from.UseDescriptionProperty; // Clone.tt Line: 141
            to.MaxDescriptionLength = from.MaxDescriptionLength; // Clone.tt Line: 141
            to.PropertyDescriptionGuid = from.PropertyDescriptionGuid; // Clone.tt Line: 141
            to.UseFolderTypeExplicitly = from.UseFolderTypeExplicitly; // Clone.tt Line: 141
            to.PropertyIsFolderGuid = from.PropertyIsFolderGuid; // Clone.tt Line: 141
            to.PropertyIsOpenGuid = from.PropertyIsOpenGuid; // Clone.tt Line: 141
            to.UseTree = from.UseTree; // Clone.tt Line: 141
            to.GroupIconType = from.GroupIconType; // Clone.tt Line: 141
            to.MaxTreeLevels = from.MaxTreeLevels; // Clone.tt Line: 141
            to.UseSeparatePropertiesForGroups = from.UseSeparatePropertiesForGroups; // Clone.tt Line: 141
            to.PropertyRefSelfGuid = from.PropertyRefSelfGuid; // Clone.tt Line: 141
            to.PropertyRefFolderGuid = from.PropertyRefFolderGuid; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Catalog Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Catalog.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Catalog from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Catalog.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_catalog' to 'Catalog'
        public static Catalog ConvertToVM(Proto.Config.proto_catalog m, Catalog vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            if (vm.Folder == null) // Clone.tt Line: 213
                vm.Folder = new CatalogFolder(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.CatalogFolder.ConvertToVM(m.Folder, (CatalogFolder)vm.Folder); // Clone.tt Line: 219
            if (vm.GroupProperties == null) // Clone.tt Line: 213
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, (GroupListProperties)vm.GroupProperties); // Clone.tt Line: 219
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 213
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, (GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 219
            if (vm.GroupForms == null) // Clone.tt Line: 213
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListForms.ConvertToVM(m.GroupForms, (GroupListForms)vm.GroupForms); // Clone.tt Line: 219
            if (vm.GroupReports == null) // Clone.tt Line: 213
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListReports.ConvertToVM(m.GroupReports, (GroupListReports)vm.GroupReports); // Clone.tt Line: 219
            vm.ItemIconType = (EnumCatalogTreeIcon)m.ItemIconType; // Clone.tt Line: 221
            vm.PropertyIdGuid = m.PropertyIdGuid; // Clone.tt Line: 221
            vm.UseCodeProperty = m.UseCodeProperty; // Clone.tt Line: 221
            if (vm.CodePropertySettings == null) // Clone.tt Line: 213
                vm.CodePropertySettings = new CatalogCodePropertySettings(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.ConvertToVM(m.CodePropertySettings, (CatalogCodePropertySettings)vm.CodePropertySettings); // Clone.tt Line: 219
            vm.PropertyCodeGuid = m.PropertyCodeGuid; // Clone.tt Line: 221
            vm.UseNameProperty = m.UseNameProperty; // Clone.tt Line: 221
            vm.MaxNameLength = m.MaxNameLength; // Clone.tt Line: 221
            vm.PropertyNameGuid = m.PropertyNameGuid; // Clone.tt Line: 221
            vm.UseDescriptionProperty = m.UseDescriptionProperty; // Clone.tt Line: 221
            vm.MaxDescriptionLength = m.MaxDescriptionLength; // Clone.tt Line: 221
            vm.PropertyDescriptionGuid = m.PropertyDescriptionGuid; // Clone.tt Line: 221
            vm.UseFolderTypeExplicitly = m.UseFolderTypeExplicitly; // Clone.tt Line: 221
            vm.PropertyIsFolderGuid = m.PropertyIsFolderGuid; // Clone.tt Line: 221
            vm.PropertyIsOpenGuid = m.PropertyIsOpenGuid; // Clone.tt Line: 221
            vm.UseTree = m.UseTree; // Clone.tt Line: 221
            vm.GroupIconType = (EnumCatalogTreeIcon)m.GroupIconType; // Clone.tt Line: 221
            vm.MaxTreeLevels = m.MaxTreeLevels; // Clone.tt Line: 221
            vm.UseSeparatePropertiesForGroups = m.UseSeparatePropertiesForGroups; // Clone.tt Line: 221
            vm.PropertyRefSelfGuid = m.PropertyRefSelfGuid; // Clone.tt Line: 221
            vm.PropertyRefFolderGuid = m.PropertyRefFolderGuid; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Catalog' to 'proto_catalog'
        public static Proto.Config.proto_catalog ConvertToProto(Catalog vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_catalog m = new Proto.Config.proto_catalog(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.Folder = vSharpStudio.vm.ViewModels.CatalogFolder.ConvertToProto((CatalogFolder)vm.Folder); // Clone.tt Line: 270
            m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto((GroupListProperties)vm.GroupProperties); // Clone.tt Line: 270
            m.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToProto((GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 270
            m.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.ConvertToProto((GroupListForms)vm.GroupForms); // Clone.tt Line: 270
            m.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.ConvertToProto((GroupListReports)vm.GroupReports); // Clone.tt Line: 270
            m.ItemIconType = (Proto.Config.proto_enum_catalog_tree_icon)vm.ItemIconType; // Clone.tt Line: 274
            m.PropertyIdGuid = vm.PropertyIdGuid; // Clone.tt Line: 276
            m.UseCodeProperty = vm.UseCodeProperty; // Clone.tt Line: 276
            m.CodePropertySettings = vSharpStudio.vm.ViewModels.CatalogCodePropertySettings.ConvertToProto((CatalogCodePropertySettings)vm.CodePropertySettings); // Clone.tt Line: 270
            m.PropertyCodeGuid = vm.PropertyCodeGuid; // Clone.tt Line: 276
            m.UseNameProperty = vm.UseNameProperty; // Clone.tt Line: 276
            m.MaxNameLength = vm.MaxNameLength; // Clone.tt Line: 276
            m.PropertyNameGuid = vm.PropertyNameGuid; // Clone.tt Line: 276
            m.UseDescriptionProperty = vm.UseDescriptionProperty; // Clone.tt Line: 276
            m.MaxDescriptionLength = vm.MaxDescriptionLength; // Clone.tt Line: 276
            m.PropertyDescriptionGuid = vm.PropertyDescriptionGuid; // Clone.tt Line: 276
            m.UseFolderTypeExplicitly = vm.UseFolderTypeExplicitly; // Clone.tt Line: 276
            m.PropertyIsFolderGuid = vm.PropertyIsFolderGuid; // Clone.tt Line: 276
            m.PropertyIsOpenGuid = vm.PropertyIsOpenGuid; // Clone.tt Line: 276
            m.UseTree = vm.UseTree; // Clone.tt Line: 276
            m.GroupIconType = (Proto.Config.proto_enum_catalog_tree_icon)vm.GroupIconType; // Clone.tt Line: 274
            m.MaxTreeLevels = vm.MaxTreeLevels; // Clone.tt Line: 276
            m.UseSeparatePropertiesForGroups = vm.UseSeparatePropertiesForGroups; // Clone.tt Line: 276
            m.PropertyRefSelfGuid = vm.PropertyRefSelfGuid; // Clone.tt Line: 276
            m.PropertyRefFolderGuid = vm.PropertyRefFolderGuid; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.Folder.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupPropertiesTabs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupReports.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.CodePropertySettings.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public CatalogFolder Folder // Property.tt Line: 55
        { 
            get { return this._Folder; }
            set
            {
                if (this._Folder != value)
                {
                    this.OnFolderChanging(ref value);
                    this._Folder = value;
                    this.OnFolderChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private CatalogFolder _Folder;
        ICatalogFolder ICatalog.Folder { get { return (this as Catalog).Folder; } } // Property.tt Line: 77
        partial void OnFolderChanging(ref CatalogFolder to); // Property.tt Line: 79
        partial void OnFolderChanged();
        //ICatalogFolder ICatalog.Folder { get { return this._Folder; } }
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 55
        { 
            get { return this._GroupProperties; }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(ref value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListProperties _GroupProperties;
        IGroupListProperties ICatalog.GroupProperties { get { return (this as Catalog).GroupProperties; } } // Property.tt Line: 77
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 79
        partial void OnGroupPropertiesChanged();
        //IGroupListProperties ICatalog.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 55
        { 
            get { return this._GroupPropertiesTabs; }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(ref value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        IGroupListPropertiesTabs ICatalog.GroupPropertiesTabs { get { return (this as Catalog).GroupPropertiesTabs; } } // Property.tt Line: 77
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 79
        partial void OnGroupPropertiesTabsChanged();
        //IGroupListPropertiesTabs ICatalog.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 55
        { 
            get { return this._GroupForms; }
            set
            {
                if (this._GroupForms != value)
                {
                    this.OnGroupFormsChanging(ref value);
                    this._GroupForms = value;
                    this.OnGroupFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListForms _GroupForms;
        IGroupListForms ICatalog.GroupForms { get { return (this as Catalog).GroupForms; } } // Property.tt Line: 77
        partial void OnGroupFormsChanging(ref GroupListForms to); // Property.tt Line: 79
        partial void OnGroupFormsChanged();
        //IGroupListForms ICatalog.GroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 55
        { 
            get { return this._GroupReports; }
            set
            {
                if (this._GroupReports != value)
                {
                    this.OnGroupReportsChanging(ref value);
                    this._GroupReports = value;
                    this.OnGroupReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListReports _GroupReports;
        IGroupListReports ICatalog.GroupReports { get { return (this as Catalog).GroupReports; } } // Property.tt Line: 77
        partial void OnGroupReportsChanging(ref GroupListReports to); // Property.tt Line: 79
        partial void OnGroupReportsChanged();
        //IGroupListReports ICatalog.GroupReports { get { return this._GroupReports; } }
        
        [PropertyOrderAttribute(19)]
        [DisplayName("Item Icon")]
        [Description("Catalog item icon type")]
        public EnumCatalogTreeIcon ItemIconType // Property.tt Line: 55
        { 
            get { return this._ItemIconType; }
            set
            {
                if (this._ItemIconType != value)
                {
                    this.OnItemIconTypeChanging(ref value);
                    this._ItemIconType = value;
                    this.OnItemIconTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumCatalogTreeIcon _ItemIconType;
        partial void OnItemIconTypeChanging(ref EnumCatalogTreeIcon to); // Property.tt Line: 79
        partial void OnItemIconTypeChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyIdGuid // Property.tt Line: 55
        { 
            get { return this._PropertyIdGuid; }
            set
            {
                if (this._PropertyIdGuid != value)
                {
                    this.OnPropertyIdGuidChanging(ref value);
                    this._PropertyIdGuid = value;
                    this.OnPropertyIdGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIdGuid = string.Empty;
        partial void OnPropertyIdGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIdGuidChanged();
        
        [PropertyOrderAttribute(21)]
        [DisplayName("Use Code")]
        [Description("Use Code property for catalog item")]
        public bool UseCodeProperty // Property.tt Line: 55
        { 
            get { return this._UseCodeProperty; }
            set
            {
                if (this._UseCodeProperty != value)
                {
                    this.OnUseCodePropertyChanging(ref value);
                    this._UseCodeProperty = value;
                    this.OnUseCodePropertyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _UseCodeProperty;
        partial void OnUseCodePropertyChanging(ref bool to); // Property.tt Line: 79
        partial void OnUseCodePropertyChanged();
        
        [PropertyOrderAttribute(22)]
        [ExpandableObjectAttribute()]
        [DisplayName("Code")]
        [Description("Code property settings for catalog item")]
        public CatalogCodePropertySettings CodePropertySettings // Property.tt Line: 55
        { 
            get { return this._CodePropertySettings; }
            set
            {
                if (this._CodePropertySettings != value)
                {
                    this.OnCodePropertySettingsChanging(ref value);
                    this._CodePropertySettings = value;
                    this.OnCodePropertySettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private CatalogCodePropertySettings _CodePropertySettings;
        ICatalogCodePropertySettings ICatalog.CodePropertySettings { get { return (this as Catalog).CodePropertySettings; } } // Property.tt Line: 77
        partial void OnCodePropertySettingsChanging(ref CatalogCodePropertySettings to); // Property.tt Line: 79
        partial void OnCodePropertySettingsChanged();
        //ICatalogCodePropertySettings ICatalog.CodePropertySettings { get { return this._CodePropertySettings; } }
        
        [BrowsableAttribute(false)]
        public string PropertyCodeGuid // Property.tt Line: 55
        { 
            get { return this._PropertyCodeGuid; }
            set
            {
                if (this._PropertyCodeGuid != value)
                {
                    this.OnPropertyCodeGuidChanging(ref value);
                    this._PropertyCodeGuid = value;
                    this.OnPropertyCodeGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyCodeGuid = string.Empty;
        partial void OnPropertyCodeGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyCodeGuidChanged();
        
        [PropertyOrderAttribute(41)]
        [DisplayName("Use Name")]
        [Description("Use Name property for catalog item")]
        public bool UseNameProperty // Property.tt Line: 55
        { 
            get { return this._UseNameProperty; }
            set
            {
                if (this._UseNameProperty != value)
                {
                    this.OnUseNamePropertyChanging(ref value);
                    this._UseNameProperty = value;
                    this.OnUseNamePropertyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _UseNameProperty;
        partial void OnUseNamePropertyChanging(ref bool to); // Property.tt Line: 79
        partial void OnUseNamePropertyChanged();
        
        [PropertyOrderAttribute(42)]
        [DisplayName("Max Length")]
        [Description("Maximum catalog item name length. If zero, than unlimited length")]
        public uint MaxNameLength // Property.tt Line: 55
        { 
            get { return this._MaxNameLength; }
            set
            {
                if (this._MaxNameLength != value)
                {
                    this.OnMaxNameLengthChanging(ref value);
                    this._MaxNameLength = value;
                    this.OnMaxNameLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _MaxNameLength;
        partial void OnMaxNameLengthChanging(ref uint to); // Property.tt Line: 79
        partial void OnMaxNameLengthChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyNameGuid // Property.tt Line: 55
        { 
            get { return this._PropertyNameGuid; }
            set
            {
                if (this._PropertyNameGuid != value)
                {
                    this.OnPropertyNameGuidChanging(ref value);
                    this._PropertyNameGuid = value;
                    this.OnPropertyNameGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyNameGuid = string.Empty;
        partial void OnPropertyNameGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyNameGuidChanged();
        
        [PropertyOrderAttribute(51)]
        [DisplayName("Use Description")]
        [Description("Use Description property for catalog item")]
        public bool UseDescriptionProperty // Property.tt Line: 55
        { 
            get { return this._UseDescriptionProperty; }
            set
            {
                if (this._UseDescriptionProperty != value)
                {
                    this.OnUseDescriptionPropertyChanging(ref value);
                    this._UseDescriptionProperty = value;
                    this.OnUseDescriptionPropertyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _UseDescriptionProperty;
        partial void OnUseDescriptionPropertyChanging(ref bool to); // Property.tt Line: 79
        partial void OnUseDescriptionPropertyChanged();
        
        [PropertyOrderAttribute(52)]
        [DisplayName("Max Length")]
        [Description("Maximum catalog item description length. If zero, than unlimited length")]
        public uint MaxDescriptionLength // Property.tt Line: 55
        { 
            get { return this._MaxDescriptionLength; }
            set
            {
                if (this._MaxDescriptionLength != value)
                {
                    this.OnMaxDescriptionLengthChanging(ref value);
                    this._MaxDescriptionLength = value;
                    this.OnMaxDescriptionLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _MaxDescriptionLength;
        partial void OnMaxDescriptionLengthChanging(ref uint to); // Property.tt Line: 79
        partial void OnMaxDescriptionLengthChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyDescriptionGuid // Property.tt Line: 55
        { 
            get { return this._PropertyDescriptionGuid; }
            set
            {
                if (this._PropertyDescriptionGuid != value)
                {
                    this.OnPropertyDescriptionGuidChanging(ref value);
                    this._PropertyDescriptionGuid = value;
                    this.OnPropertyDescriptionGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyDescriptionGuid = string.Empty;
        partial void OnPropertyDescriptionGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyDescriptionGuidChanged();
        
        [PropertyOrderAttribute(54)]
        [DisplayName("Use Folders")]
        [Description("User has choose explicitly item or folder type for catalog element ")]
        public bool UseFolderTypeExplicitly // Property.tt Line: 55
        { 
            get { return this._UseFolderTypeExplicitly; }
            set
            {
                if (this._UseFolderTypeExplicitly != value)
                {
                    this.OnUseFolderTypeExplicitlyChanging(ref value);
                    this._UseFolderTypeExplicitly = value;
                    this.OnUseFolderTypeExplicitlyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _UseFolderTypeExplicitly;
        partial void OnUseFolderTypeExplicitlyChanging(ref bool to); // Property.tt Line: 79
        partial void OnUseFolderTypeExplicitlyChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyIsFolderGuid // Property.tt Line: 55
        { 
            get { return this._PropertyIsFolderGuid; }
            set
            {
                if (this._PropertyIsFolderGuid != value)
                {
                    this.OnPropertyIsFolderGuidChanging(ref value);
                    this._PropertyIsFolderGuid = value;
                    this.OnPropertyIsFolderGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIsFolderGuid = string.Empty;
        partial void OnPropertyIsFolderGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIsFolderGuidChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyIsOpenGuid // Property.tt Line: 55
        { 
            get { return this._PropertyIsOpenGuid; }
            set
            {
                if (this._PropertyIsOpenGuid != value)
                {
                    this.OnPropertyIsOpenGuidChanging(ref value);
                    this._PropertyIsOpenGuid = value;
                    this.OnPropertyIsOpenGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIsOpenGuid = string.Empty;
        partial void OnPropertyIsOpenGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIsOpenGuidChanged();
        
        [PropertyOrderAttribute(61)]
        [DisplayName("Use Tree")]
        [Description("Use tree catalog structure")]
        public bool UseTree // Property.tt Line: 55
        { 
            get { return this._UseTree; }
            set
            {
                if (this._UseTree != value)
                {
                    this.OnUseTreeChanging(ref value);
                    this._UseTree = value;
                    this.OnUseTreeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _UseTree;
        partial void OnUseTreeChanging(ref bool to); // Property.tt Line: 79
        partial void OnUseTreeChanged();
        
        [PropertyOrderAttribute(62)]
        [DisplayName("Group Icon")]
        [Description("Catalog group icon type")]
        public EnumCatalogTreeIcon GroupIconType // Property.tt Line: 55
        { 
            get { return this._GroupIconType; }
            set
            {
                if (this._GroupIconType != value)
                {
                    this.OnGroupIconTypeChanging(ref value);
                    this._GroupIconType = value;
                    this.OnGroupIconTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumCatalogTreeIcon _GroupIconType;
        partial void OnGroupIconTypeChanging(ref EnumCatalogTreeIcon to); // Property.tt Line: 79
        partial void OnGroupIconTypeChanged();
        
        [PropertyOrderAttribute(63)]
        [DisplayName("Levels")]
        [Description("Maximum amount levels in catalog item groups. If zero, than unlimited")]
        public uint MaxTreeLevels // Property.tt Line: 55
        { 
            get { return this._MaxTreeLevels; }
            set
            {
                if (this._MaxTreeLevels != value)
                {
                    this.OnMaxTreeLevelsChanging(ref value);
                    this._MaxTreeLevels = value;
                    this.OnMaxTreeLevelsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private uint _MaxTreeLevels;
        partial void OnMaxTreeLevelsChanging(ref uint to); // Property.tt Line: 79
        partial void OnMaxTreeLevelsChanged();
        
        [PropertyOrderAttribute(64)]
        [DisplayName("Group properties")]
        [Description("Separate set of properties for groups")]
        public bool UseSeparatePropertiesForGroups // Property.tt Line: 55
        { 
            get { return this._UseSeparatePropertiesForGroups; }
            set
            {
                if (this._UseSeparatePropertiesForGroups != value)
                {
                    this.OnUseSeparatePropertiesForGroupsChanging(ref value);
                    this._UseSeparatePropertiesForGroups = value;
                    this.OnUseSeparatePropertiesForGroupsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _UseSeparatePropertiesForGroups;
        partial void OnUseSeparatePropertiesForGroupsChanging(ref bool to); // Property.tt Line: 79
        partial void OnUseSeparatePropertiesForGroupsChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyRefSelfGuid // Property.tt Line: 55
        { 
            get { return this._PropertyRefSelfGuid; }
            set
            {
                if (this._PropertyRefSelfGuid != value)
                {
                    this.OnPropertyRefSelfGuidChanging(ref value);
                    this._PropertyRefSelfGuid = value;
                    this.OnPropertyRefSelfGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyRefSelfGuid = string.Empty;
        partial void OnPropertyRefSelfGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyRefSelfGuidChanged();
        
        [BrowsableAttribute(false)]
        public string PropertyRefFolderGuid // Property.tt Line: 55
        { 
            get { return this._PropertyRefFolderGuid; }
            set
            {
                if (this._PropertyRefFolderGuid != value)
                {
                    this.OnPropertyRefFolderGuidChanging(ref value);
                    this._PropertyRefFolderGuid = value;
                    this.OnPropertyRefFolderGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyRefFolderGuid = string.Empty;
        partial void OnPropertyRefFolderGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyRefFolderGuidChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> ICatalog.ListNodeGeneratorsSettings { get { return (this as Catalog).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { } // Class.tt Line: 6
    public partial class GroupListCatalogs : ConfigObjectVmGenSettings<GroupListCatalogs, GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IConfigAcceptVisitor, IGroupListCatalogs // Class.tt Line: 7
    {
        #region CTOR
        public GroupListCatalogs() : this(default(ITreeConfigNode))
        {
        }
        public GroupListCatalogs(ITreeConfigNode parent) 
            : base(parent, GroupListCatalogsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListCatalogs = new ConfigNodesCollection<Catalog>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Catalog)) // Clone.tt Line: 15
            {
                this.ListCatalogs.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListCatalogs Clone(ITreeConfigNode parent, IGroupListCatalogs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListCatalogs vm = new GroupListCatalogs(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.PrefixForDbTables = from.PrefixForDbTables; // Clone.tt Line: 65
            vm.ListCatalogs = new ConfigNodesCollection<Catalog>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListCatalogs) // Clone.tt Line: 52
                vm.ListCatalogs.Add(Catalog.Clone(vm, (Catalog)t, isDeep));
            vm.PropertyCodeName = from.PropertyCodeName; // Clone.tt Line: 65
            vm.PropertyNameName = from.PropertyNameName; // Clone.tt Line: 65
            vm.PropertyDescriptionName = from.PropertyDescriptionName; // Clone.tt Line: 65
            vm.PropertyIsFolderName = from.PropertyIsFolderName; // Clone.tt Line: 65
            vm.PropertyIsOpenName = from.PropertyIsOpenName; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListCatalogs to, IGroupListCatalogs from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.PrefixForDbTables = from.PrefixForDbTables; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListCatalogs.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListCatalogs)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Catalog.Update((Catalog)t, (Catalog)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListCatalogs.Remove(t);
                }
                foreach (var tt in from.ListCatalogs)
                {
                    bool isfound = false;
                    foreach (var t in to.ListCatalogs.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Catalog(to); // Clone.tt Line: 117
                        Catalog.Update(p, (Catalog)tt, isDeep);
                        to.ListCatalogs.Add(p);
                    }
                }
            }
            to.PropertyCodeName = from.PropertyCodeName; // Clone.tt Line: 141
            to.PropertyNameName = from.PropertyNameName; // Clone.tt Line: 141
            to.PropertyDescriptionName = from.PropertyDescriptionName; // Clone.tt Line: 141
            to.PropertyIsFolderName = from.PropertyIsFolderName; // Clone.tt Line: 141
            to.PropertyIsOpenName = from.PropertyIsOpenName; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListCatalogs Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListCatalogs.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListCatalogs from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListCatalogs.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_catalogs' to 'GroupListCatalogs'
        public static GroupListCatalogs ConvertToVM(Proto.Config.proto_group_list_catalogs m, GroupListCatalogs vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.PrefixForDbTables = m.PrefixForDbTables; // Clone.tt Line: 221
            vm.ListCatalogs = new ConfigNodesCollection<Catalog>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListCatalogs) // Clone.tt Line: 201
            {
                var tvm = Catalog.ConvertToVM(t, new Catalog(vm)); // Clone.tt Line: 204
                vm.ListCatalogs.Add(tvm);
            }
            vm.PropertyCodeName = m.PropertyCodeName; // Clone.tt Line: 221
            vm.PropertyNameName = m.PropertyNameName; // Clone.tt Line: 221
            vm.PropertyDescriptionName = m.PropertyDescriptionName; // Clone.tt Line: 221
            vm.PropertyIsFolderName = m.PropertyIsFolderName; // Clone.tt Line: 221
            vm.PropertyIsOpenName = m.PropertyIsOpenName; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
        public static Proto.Config.proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_catalogs m = new Proto.Config.proto_group_list_catalogs(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.PrefixForDbTables = vm.PrefixForDbTables; // Clone.tt Line: 276
            foreach (var t in vm.ListCatalogs) // Clone.tt Line: 242
                m.ListCatalogs.Add(Catalog.ConvertToProto((Catalog)t)); // Clone.tt Line: 246
            m.PropertyCodeName = vm.PropertyCodeName; // Clone.tt Line: 276
            m.PropertyNameName = vm.PropertyNameName; // Clone.tt Line: 276
            m.PropertyDescriptionName = vm.PropertyDescriptionName; // Clone.tt Line: 276
            m.PropertyIsFolderName = vm.PropertyIsFolderName; // Clone.tt Line: 276
            m.PropertyIsOpenName = vm.PropertyIsOpenName; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListCatalogs)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Db prefix")]
        [Description("Prefix for catalog db table names. Used if set to use in config model")]
        public string PrefixForDbTables // Property.tt Line: 55
        { 
            get { return this._PrefixForDbTables; }
            set
            {
                if (this._PrefixForDbTables != value)
                {
                    this.OnPrefixForDbTablesChanging(ref value);
                    this._PrefixForDbTables = value;
                    this.OnPrefixForDbTablesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PrefixForDbTables = string.Empty;
        partial void OnPrefixForDbTablesChanging(ref string to); // Property.tt Line: 79
        partial void OnPrefixForDbTablesChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Catalog> ListCatalogs // Property.tt Line: 8
        { 
            get { return this._ListCatalogs; }
            set
            {
                if (this._ListCatalogs != value)
                {
                    this.OnListCatalogsChanging(value);
                    _ListCatalogs = value;
                    this.OnListCatalogsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Catalog> _ListCatalogs;
        IReadOnlyList<ICatalog> IGroupListCatalogs.ListCatalogs { get { return (this as GroupListCatalogs).ListCatalogs; } } // Property.tt Line: 26
        partial void OnListCatalogsChanging(ObservableCollection<Catalog> to); // Property.tt Line: 27
        partial void OnListCatalogsChanged();
        public Catalog this[int index] { get { return (Catalog)this.ListCatalogs[index]; } }
        ICatalog IGroupListCatalogs.this[int index] { get { return (Catalog)this.ListCatalogs[index]; } }
        public void Add(Catalog item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListCatalogs.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Catalog> items) 
        { 
            Contract.Requires(items != null);
            this.ListCatalogs.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListCatalogs.Count; }
        int IGroupListCatalogs.Count() { return this.Count(); }
        public void Remove(Catalog item) 
        {
            Contract.Requires(item != null);
            this.ListCatalogs.Remove(item); 
            item.Parent = null;
        }
        
        [PropertyOrderAttribute(21)]
        [DisplayName("Code property")]
        [Description("Name of code auto generated property if it is used in catalog")]
        public string PropertyCodeName // Property.tt Line: 55
        { 
            get { return this._PropertyCodeName; }
            set
            {
                if (this._PropertyCodeName != value)
                {
                    this.OnPropertyCodeNameChanging(ref value);
                    this._PropertyCodeName = value;
                    this.OnPropertyCodeNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyCodeName = string.Empty;
        partial void OnPropertyCodeNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyCodeNameChanged();
        
        [PropertyOrderAttribute(22)]
        [DisplayName("Name property")]
        [Description("Name of name auto generated property if it is used in catalog")]
        public string PropertyNameName // Property.tt Line: 55
        { 
            get { return this._PropertyNameName; }
            set
            {
                if (this._PropertyNameName != value)
                {
                    this.OnPropertyNameNameChanging(ref value);
                    this._PropertyNameName = value;
                    this.OnPropertyNameNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyNameName = string.Empty;
        partial void OnPropertyNameNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyNameNameChanged();
        
        [PropertyOrderAttribute(23)]
        [DisplayName("Description property")]
        [Description("Name of description auto generated property if it is used in catalog")]
        public string PropertyDescriptionName // Property.tt Line: 55
        { 
            get { return this._PropertyDescriptionName; }
            set
            {
                if (this._PropertyDescriptionName != value)
                {
                    this.OnPropertyDescriptionNameChanging(ref value);
                    this._PropertyDescriptionName = value;
                    this.OnPropertyDescriptionNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyDescriptionName = string.Empty;
        partial void OnPropertyDescriptionNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyDescriptionNameChanged();
        
        [PropertyOrderAttribute(25)]
        [DisplayName("IsFolder property")]
        [Description("Name of is folder auto generated property if it is used in catalog")]
        public string PropertyIsFolderName // Property.tt Line: 55
        { 
            get { return this._PropertyIsFolderName; }
            set
            {
                if (this._PropertyIsFolderName != value)
                {
                    this.OnPropertyIsFolderNameChanging(ref value);
                    this._PropertyIsFolderName = value;
                    this.OnPropertyIsFolderNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIsFolderName = string.Empty;
        partial void OnPropertyIsFolderNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIsFolderNameChanged();
        
        [PropertyOrderAttribute(26)]
        [DisplayName("IsOpen property")]
        [Description("Name of is open auto generated property if folder is used in catalog")]
        public string PropertyIsOpenName // Property.tt Line: 55
        { 
            get { return this._PropertyIsOpenName; }
            set
            {
                if (this._PropertyIsOpenName != value)
                {
                    this.OnPropertyIsOpenNameChanging(ref value);
                    this._PropertyIsOpenName = value;
                    this.OnPropertyIsOpenNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIsOpenName = string.Empty;
        partial void OnPropertyIsOpenNameChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIsOpenNameChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListCatalogs.ListNodeGeneratorsSettings { get { return (this as GroupListCatalogs).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { } // Class.tt Line: 6
    public partial class GroupDocuments : ConfigObjectVmGenSettings<GroupDocuments, GroupDocumentsValidator>, IComparable<GroupDocuments>, IConfigAcceptVisitor, IGroupDocuments // Class.tt Line: 7
    {
        #region CTOR
        public GroupDocuments() : this(default(ITreeConfigNode))
        {
        }
        public GroupDocuments(ITreeConfigNode parent) 
            : base(parent, GroupDocumentsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupSharedProperties = new GroupListProperties(this); // Class.tt Line: 33
            this.GroupListDocuments = new GroupListDocuments(this); // Class.tt Line: 33
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupDocuments Clone(ITreeConfigNode parent, IGroupDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupDocuments vm = new GroupDocuments(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.PrefixForDbTables = from.PrefixForDbTables; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupSharedProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(vm, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupListDocuments = vSharpStudio.vm.ViewModels.GroupListDocuments.Clone(vm, from.GroupListDocuments, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupDocuments to, IGroupDocuments from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.PrefixForDbTables = from.PrefixForDbTables; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListProperties.Update((GroupListProperties)to.GroupSharedProperties, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListDocuments.Update((GroupListDocuments)to.GroupListDocuments, from.GroupListDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupDocuments Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupDocuments.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupDocuments from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupDocuments.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_documents' to 'GroupDocuments'
        public static GroupDocuments ConvertToVM(Proto.Config.proto_group_documents m, GroupDocuments vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.PrefixForDbTables = m.PrefixForDbTables; // Clone.tt Line: 221
            if (vm.GroupSharedProperties == null) // Clone.tt Line: 213
                vm.GroupSharedProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupSharedProperties, (GroupListProperties)vm.GroupSharedProperties); // Clone.tt Line: 219
            if (vm.GroupListDocuments == null) // Clone.tt Line: 213
                vm.GroupListDocuments = new GroupListDocuments(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListDocuments.ConvertToVM(m.GroupListDocuments, (GroupListDocuments)vm.GroupListDocuments); // Clone.tt Line: 219
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupDocuments' to 'proto_group_documents'
        public static Proto.Config.proto_group_documents ConvertToProto(GroupDocuments vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_documents m = new Proto.Config.proto_group_documents(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.PrefixForDbTables = vm.PrefixForDbTables; // Clone.tt Line: 276
            m.GroupSharedProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto((GroupListProperties)vm.GroupSharedProperties); // Clone.tt Line: 270
            m.GroupListDocuments = vSharpStudio.vm.ViewModels.GroupListDocuments.ConvertToProto((GroupListDocuments)vm.GroupListDocuments); // Clone.tt Line: 270
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupSharedProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupListDocuments.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Db prefix")]
        [Description("Prefix for document db table names. Used if set to use in config model")]
        public string PrefixForDbTables // Property.tt Line: 55
        { 
            get { return this._PrefixForDbTables; }
            set
            {
                if (this._PrefixForDbTables != value)
                {
                    this.OnPrefixForDbTablesChanging(ref value);
                    this._PrefixForDbTables = value;
                    this.OnPrefixForDbTablesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PrefixForDbTables = string.Empty;
        partial void OnPrefixForDbTablesChanging(ref string to); // Property.tt Line: 79
        partial void OnPrefixForDbTablesChanged();
        
        [BrowsableAttribute(false)]
        [Description("Properties for all documents")]
        public GroupListProperties GroupSharedProperties // Property.tt Line: 55
        { 
            get { return this._GroupSharedProperties; }
            set
            {
                if (this._GroupSharedProperties != value)
                {
                    this.OnGroupSharedPropertiesChanging(ref value);
                    this._GroupSharedProperties = value;
                    this.OnGroupSharedPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListProperties _GroupSharedProperties;
        IGroupListProperties IGroupDocuments.GroupSharedProperties { get { return (this as GroupDocuments).GroupSharedProperties; } } // Property.tt Line: 77
        partial void OnGroupSharedPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 79
        partial void OnGroupSharedPropertiesChanged();
        //IGroupListProperties IGroupDocuments.GroupSharedProperties { get { return this._GroupSharedProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListDocuments GroupListDocuments // Property.tt Line: 55
        { 
            get { return this._GroupListDocuments; }
            set
            {
                if (this._GroupListDocuments != value)
                {
                    this.OnGroupListDocumentsChanging(ref value);
                    this._GroupListDocuments = value;
                    this.OnGroupListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListDocuments _GroupListDocuments;
        IGroupListDocuments IGroupDocuments.GroupListDocuments { get { return (this as GroupDocuments).GroupListDocuments; } } // Property.tt Line: 77
        partial void OnGroupListDocumentsChanging(ref GroupListDocuments to); // Property.tt Line: 79
        partial void OnGroupListDocumentsChanged();
        //IGroupListDocuments IGroupDocuments.GroupListDocuments { get { return this._GroupListDocuments; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupDocuments.ListNodeGeneratorsSettings { get { return (this as GroupDocuments).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { } // Class.tt Line: 6
    public partial class Document : ConfigObjectVmGenSettings<Document, DocumentValidator>, IComparable<Document>, IConfigAcceptVisitor, IDocument // Class.tt Line: 7
    {
        #region CTOR
        public Document() : this(default(ITreeConfigNode))
        {
        }
        public Document(ITreeConfigNode parent) 
            : base(parent, DocumentValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 33
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 33
            this.GroupForms = new GroupListForms(this); // Class.tt Line: 33
            this.GroupReports = new GroupListReports(this); // Class.tt Line: 33
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Document Clone(ITreeConfigNode parent, IDocument from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Document vm = new Document(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Document to, IDocument from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListProperties.Update((GroupListProperties)to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Update((GroupListPropertiesTabs)to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListForms.Update((GroupListForms)to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.GroupListReports.Update((GroupListReports)to.GroupReports, from.GroupReports, isDeep);
            to.PropertyIdGuid = from.PropertyIdGuid; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Document Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Document.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Document from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Document.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_document' to 'Document'
        public static Document ConvertToVM(Proto.Config.proto_document m, Document vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            if (vm.GroupProperties == null) // Clone.tt Line: 213
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, (GroupListProperties)vm.GroupProperties); // Clone.tt Line: 219
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 213
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, (GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 219
            if (vm.GroupForms == null) // Clone.tt Line: 213
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListForms.ConvertToVM(m.GroupForms, (GroupListForms)vm.GroupForms); // Clone.tt Line: 219
            if (vm.GroupReports == null) // Clone.tt Line: 213
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 215
            vSharpStudio.vm.ViewModels.GroupListReports.ConvertToVM(m.GroupReports, (GroupListReports)vm.GroupReports); // Clone.tt Line: 219
            vm.PropertyIdGuid = m.PropertyIdGuid; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Document' to 'proto_document'
        public static Proto.Config.proto_document ConvertToProto(Document vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_document m = new Proto.Config.proto_document(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto((GroupListProperties)vm.GroupProperties); // Clone.tt Line: 270
            m.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToProto((GroupListPropertiesTabs)vm.GroupPropertiesTabs); // Clone.tt Line: 270
            m.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.ConvertToProto((GroupListForms)vm.GroupForms); // Clone.tt Line: 270
            m.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.ConvertToProto((GroupListReports)vm.GroupReports); // Clone.tt Line: 270
            m.PropertyIdGuid = vm.PropertyIdGuid; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupPropertiesTabs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupReports.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 55
        { 
            get { return this._GroupProperties; }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(ref value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListProperties _GroupProperties;
        IGroupListProperties IDocument.GroupProperties { get { return (this as Document).GroupProperties; } } // Property.tt Line: 77
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 79
        partial void OnGroupPropertiesChanged();
        //IGroupListProperties IDocument.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 55
        { 
            get { return this._GroupPropertiesTabs; }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(ref value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        IGroupListPropertiesTabs IDocument.GroupPropertiesTabs { get { return (this as Document).GroupPropertiesTabs; } } // Property.tt Line: 77
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 79
        partial void OnGroupPropertiesTabsChanged();
        //IGroupListPropertiesTabs IDocument.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 55
        { 
            get { return this._GroupForms; }
            set
            {
                if (this._GroupForms != value)
                {
                    this.OnGroupFormsChanging(ref value);
                    this._GroupForms = value;
                    this.OnGroupFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListForms _GroupForms;
        IGroupListForms IDocument.GroupForms { get { return (this as Document).GroupForms; } } // Property.tt Line: 77
        partial void OnGroupFormsChanging(ref GroupListForms to); // Property.tt Line: 79
        partial void OnGroupFormsChanged();
        //IGroupListForms IDocument.GroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 55
        { 
            get { return this._GroupReports; }
            set
            {
                if (this._GroupReports != value)
                {
                    this.OnGroupReportsChanging(ref value);
                    this._GroupReports = value;
                    this.OnGroupReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private GroupListReports _GroupReports;
        IGroupListReports IDocument.GroupReports { get { return (this as Document).GroupReports; } } // Property.tt Line: 77
        partial void OnGroupReportsChanging(ref GroupListReports to); // Property.tt Line: 79
        partial void OnGroupReportsChanged();
        //IGroupListReports IDocument.GroupReports { get { return this._GroupReports; } }
        
        [BrowsableAttribute(false)]
        public string PropertyIdGuid // Property.tt Line: 55
        { 
            get { return this._PropertyIdGuid; }
            set
            {
                if (this._PropertyIdGuid != value)
                {
                    this.OnPropertyIdGuidChanging(ref value);
                    this._PropertyIdGuid = value;
                    this.OnPropertyIdGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PropertyIdGuid = string.Empty;
        partial void OnPropertyIdGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnPropertyIdGuidChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IDocument.ListNodeGeneratorsSettings { get { return (this as Document).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { } // Class.tt Line: 6
    public partial class GroupListDocuments : ConfigObjectVmGenSettings<GroupListDocuments, GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IConfigAcceptVisitor, IGroupListDocuments // Class.tt Line: 7
    {
        #region CTOR
        public GroupListDocuments() : this(default(ITreeConfigNode))
        {
        }
        public GroupListDocuments(ITreeConfigNode parent) 
            : base(parent, GroupListDocumentsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Document)) // Clone.tt Line: 15
            {
                this.ListDocuments.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListDocuments Clone(ITreeConfigNode parent, IGroupListDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListDocuments vm = new GroupListDocuments(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListDocuments) // Clone.tt Line: 52
                vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListDocuments to, IGroupListDocuments from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListDocuments.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListDocuments)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Document.Update((Document)t, (Document)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListDocuments.Remove(t);
                }
                foreach (var tt in from.ListDocuments)
                {
                    bool isfound = false;
                    foreach (var t in to.ListDocuments.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Document(to); // Clone.tt Line: 117
                        Document.Update(p, (Document)tt, isDeep);
                        to.ListDocuments.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListDocuments Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListDocuments.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListDocuments from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListDocuments.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_documents' to 'GroupListDocuments'
        public static GroupListDocuments ConvertToVM(Proto.Config.proto_group_list_documents m, GroupListDocuments vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListDocuments) // Clone.tt Line: 201
            {
                var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 204
                vm.ListDocuments.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
        public static Proto.Config.proto_group_list_documents ConvertToProto(GroupListDocuments vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_documents m = new Proto.Config.proto_group_list_documents(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListDocuments) // Clone.tt Line: 242
                m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListDocuments)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Document> ListDocuments // Property.tt Line: 8
        { 
            get { return this._ListDocuments; }
            set
            {
                if (this._ListDocuments != value)
                {
                    this.OnListDocumentsChanging(value);
                    _ListDocuments = value;
                    this.OnListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Document> _ListDocuments;
        IReadOnlyList<IDocument> IGroupListDocuments.ListDocuments { get { return (this as GroupListDocuments).ListDocuments; } } // Property.tt Line: 26
        partial void OnListDocumentsChanging(ObservableCollection<Document> to); // Property.tt Line: 27
        partial void OnListDocumentsChanged();
        public Document this[int index] { get { return (Document)this.ListDocuments[index]; } }
        IDocument IGroupListDocuments.this[int index] { get { return (Document)this.ListDocuments[index]; } }
        public void Add(Document item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListDocuments.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Document> items) 
        { 
            Contract.Requires(items != null);
            this.ListDocuments.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListDocuments.Count; }
        int IGroupListDocuments.Count() { return this.Count(); }
        public void Remove(Document item) 
        {
            Contract.Requires(item != null);
            this.ListDocuments.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListDocuments.ListNodeGeneratorsSettings { get { return (this as GroupListDocuments).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { } // Class.tt Line: 6
    public partial class GroupListJournals : ConfigObjectVmGenSettings<GroupListJournals, GroupListJournalsValidator>, IComparable<GroupListJournals>, IConfigAcceptVisitor, IGroupListJournals // Class.tt Line: 7
    {
        #region CTOR
        public GroupListJournals() : this(default(ITreeConfigNode))
        {
        }
        public GroupListJournals(ITreeConfigNode parent) 
            : base(parent, GroupListJournalsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListJournals = new ConfigNodesCollection<Journal>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Journal)) // Clone.tt Line: 15
            {
                this.ListJournals.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListJournals Clone(ITreeConfigNode parent, IGroupListJournals from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListJournals vm = new GroupListJournals(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListJournals) // Clone.tt Line: 52
                vm.ListJournals.Add(Journal.Clone(vm, (Journal)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListJournals to, IGroupListJournals from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListJournals.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListJournals)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Journal.Update((Journal)t, (Journal)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListJournals.Remove(t);
                }
                foreach (var tt in from.ListJournals)
                {
                    bool isfound = false;
                    foreach (var t in to.ListJournals.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Journal(to); // Clone.tt Line: 117
                        Journal.Update(p, (Journal)tt, isDeep);
                        to.ListJournals.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListJournals Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListJournals.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListJournals from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListJournals.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_journals' to 'GroupListJournals'
        public static GroupListJournals ConvertToVM(Proto.Config.proto_group_list_journals m, GroupListJournals vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListJournals) // Clone.tt Line: 201
            {
                var tvm = Journal.ConvertToVM(t, new Journal(vm)); // Clone.tt Line: 204
                vm.ListJournals.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListJournals' to 'proto_group_list_journals'
        public static Proto.Config.proto_group_list_journals ConvertToProto(GroupListJournals vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_journals m = new Proto.Config.proto_group_list_journals(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListJournals) // Clone.tt Line: 242
                m.ListJournals.Add(Journal.ConvertToProto((Journal)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListJournals)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Journal> ListJournals // Property.tt Line: 8
        { 
            get { return this._ListJournals; }
            set
            {
                if (this._ListJournals != value)
                {
                    this.OnListJournalsChanging(value);
                    _ListJournals = value;
                    this.OnListJournalsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Journal> _ListJournals;
        IReadOnlyList<IJournal> IGroupListJournals.ListJournals { get { return (this as GroupListJournals).ListJournals; } } // Property.tt Line: 26
        partial void OnListJournalsChanging(ObservableCollection<Journal> to); // Property.tt Line: 27
        partial void OnListJournalsChanged();
        public Journal this[int index] { get { return (Journal)this.ListJournals[index]; } }
        IJournal IGroupListJournals.this[int index] { get { return (Journal)this.ListJournals[index]; } }
        public void Add(Journal item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListJournals.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Journal> items) 
        { 
            Contract.Requires(items != null);
            this.ListJournals.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListJournals.Count; }
        int IGroupListJournals.Count() { return this.Count(); }
        public void Remove(Journal item) 
        {
            Contract.Requires(item != null);
            this.ListJournals.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListJournals.ListNodeGeneratorsSettings { get { return (this as GroupListJournals).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { } // Class.tt Line: 6
    public partial class Journal : ConfigObjectVmGenSettings<Journal, JournalValidator>, IComparable<Journal>, IConfigAcceptVisitor, IJournal // Class.tt Line: 7
    {
        #region CTOR
        public Journal() : this(default(ITreeConfigNode))
        {
        }
        public Journal(ITreeConfigNode parent) 
            : base(parent, JournalValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Document)) // Clone.tt Line: 15
            {
                this.ListDocuments.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Journal Clone(ITreeConfigNode parent, IJournal from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Journal vm = new Journal(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListDocuments) // Clone.tt Line: 52
                vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Journal to, IJournal from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListDocuments.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListDocuments)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Document.Update((Document)t, (Document)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListDocuments.Remove(t);
                }
                foreach (var tt in from.ListDocuments)
                {
                    bool isfound = false;
                    foreach (var t in to.ListDocuments.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Document(to); // Clone.tt Line: 117
                        Document.Update(p, (Document)tt, isDeep);
                        to.ListDocuments.Add(p);
                    }
                }
            }
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Journal Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Journal.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Journal from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Journal.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_journal' to 'Journal'
        public static Journal ConvertToVM(Proto.Config.proto_journal m, Journal vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListDocuments) // Clone.tt Line: 201
            {
                var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 204
                vm.ListDocuments.Add(tvm);
            }
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Journal' to 'proto_journal'
        public static Proto.Config.proto_journal ConvertToProto(Journal vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_journal m = new Proto.Config.proto_journal(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListDocuments) // Clone.tt Line: 242
                m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 246
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListDocuments)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Document> ListDocuments // Property.tt Line: 8
        { 
            get { return this._ListDocuments; }
            set
            {
                if (this._ListDocuments != value)
                {
                    this.OnListDocumentsChanging(value);
                    _ListDocuments = value;
                    this.OnListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Document> _ListDocuments;
        IReadOnlyList<IDocument> IJournal.ListDocuments { get { return (this as Journal).ListDocuments; } } // Property.tt Line: 26
        partial void OnListDocumentsChanging(ObservableCollection<Document> to); // Property.tt Line: 27
        partial void OnListDocumentsChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IJournal.ListNodeGeneratorsSettings { get { return (this as Journal).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { } // Class.tt Line: 6
    public partial class GroupListForms : ConfigObjectVmGenSettings<GroupListForms, GroupListFormsValidator>, IComparable<GroupListForms>, IConfigAcceptVisitor, IGroupListForms // Class.tt Line: 7
    {
        #region CTOR
        public GroupListForms() : this(default(ITreeConfigNode))
        {
        }
        public GroupListForms(ITreeConfigNode parent) 
            : base(parent, GroupListFormsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListForms = new ConfigNodesCollection<Form>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Form)) // Clone.tt Line: 15
            {
                this.ListForms.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListForms Clone(ITreeConfigNode parent, IGroupListForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListForms vm = new GroupListForms(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListForms) // Clone.tt Line: 52
                vm.ListForms.Add(Form.Clone(vm, (Form)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListForms to, IGroupListForms from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListForms.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListForms)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Form.Update((Form)t, (Form)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListForms.Remove(t);
                }
                foreach (var tt in from.ListForms)
                {
                    bool isfound = false;
                    foreach (var t in to.ListForms.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Form(to); // Clone.tt Line: 117
                        Form.Update(p, (Form)tt, isDeep);
                        to.ListForms.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListForms Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListForms.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListForms from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListForms.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_forms' to 'GroupListForms'
        public static GroupListForms ConvertToVM(Proto.Config.proto_group_list_forms m, GroupListForms vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListForms) // Clone.tt Line: 201
            {
                var tvm = Form.ConvertToVM(t, new Form(vm)); // Clone.tt Line: 204
                vm.ListForms.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListForms' to 'proto_group_list_forms'
        public static Proto.Config.proto_group_list_forms ConvertToProto(GroupListForms vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_forms m = new Proto.Config.proto_group_list_forms(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListForms) // Clone.tt Line: 242
                m.ListForms.Add(Form.ConvertToProto((Form)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListForms)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Form> ListForms // Property.tt Line: 8
        { 
            get { return this._ListForms; }
            set
            {
                if (this._ListForms != value)
                {
                    this.OnListFormsChanging(value);
                    _ListForms = value;
                    this.OnListFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Form> _ListForms;
        IReadOnlyList<IForm> IGroupListForms.ListForms { get { return (this as GroupListForms).ListForms; } } // Property.tt Line: 26
        partial void OnListFormsChanging(ObservableCollection<Form> to); // Property.tt Line: 27
        partial void OnListFormsChanged();
        public Form this[int index] { get { return (Form)this.ListForms[index]; } }
        IForm IGroupListForms.this[int index] { get { return (Form)this.ListForms[index]; } }
        public void Add(Form item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListForms.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Form> items) 
        { 
            Contract.Requires(items != null);
            this.ListForms.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListForms.Count; }
        int IGroupListForms.Count() { return this.Count(); }
        public void Remove(Form item) 
        {
            Contract.Requires(item != null);
            this.ListForms.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListForms.ListNodeGeneratorsSettings { get { return (this as GroupListForms).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class FormMargingValidator : ValidatorBase<FormMarging, FormMargingValidator> { } // Class.tt Line: 6
    public partial class FormMarging : VmValidatableWithSeverity<FormMarging, FormMargingValidator>, IFormMarging // Class.tt Line: 7
    {
        #region CTOR
        public FormMarging() 
            : base(FormMargingValidator.Validator) // Class.tt Line: 45
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
        public static FormMarging Clone(IFormMarging from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            FormMarging vm = new FormMarging();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Left = from.Left; // Clone.tt Line: 65
            vm.Up = from.Up; // Clone.tt Line: 65
            vm.Right = from.Right; // Clone.tt Line: 65
            vm.Bottom = from.Bottom; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(FormMarging to, IFormMarging from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Left = from.Left; // Clone.tt Line: 141
            to.Up = from.Up; // Clone.tt Line: 141
            to.Right = from.Right; // Clone.tt Line: 141
            to.Bottom = from.Bottom; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override FormMarging Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return FormMarging.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(FormMarging from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            FormMarging.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_form_marging' to 'FormMarging'
        public static FormMarging ConvertToVM(Proto.Config.proto_form_marging m, FormMarging vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Left = m.Left; // Clone.tt Line: 221
            vm.Up = m.Up; // Clone.tt Line: 221
            vm.Right = m.Right; // Clone.tt Line: 221
            vm.Bottom = m.Bottom; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'FormMarging' to 'proto_form_marging'
        public static Proto.Config.proto_form_marging ConvertToProto(FormMarging vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_form_marging m = new Proto.Config.proto_form_marging(); // Clone.tt Line: 239
            m.Left = vm.Left; // Clone.tt Line: 276
            m.Up = vm.Up; // Clone.tt Line: 276
            m.Right = vm.Right; // Clone.tt Line: 276
            m.Bottom = vm.Bottom; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        public int Left // Property.tt Line: 55
        { 
            get { return this._Left; }
            set
            {
                if (this._Left != value)
                {
                    this.OnLeftChanging(ref value);
                    this._Left = value;
                    this.OnLeftChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Left;
        partial void OnLeftChanging(ref int to); // Property.tt Line: 79
        partial void OnLeftChanged();
        
        public int Up // Property.tt Line: 55
        { 
            get { return this._Up; }
            set
            {
                if (this._Up != value)
                {
                    this.OnUpChanging(ref value);
                    this._Up = value;
                    this.OnUpChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Up;
        partial void OnUpChanging(ref int to); // Property.tt Line: 79
        partial void OnUpChanged();
        
        public int Right // Property.tt Line: 55
        { 
            get { return this._Right; }
            set
            {
                if (this._Right != value)
                {
                    this.OnRightChanging(ref value);
                    this._Right = value;
                    this.OnRightChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Right;
        partial void OnRightChanging(ref int to); // Property.tt Line: 79
        partial void OnRightChanged();
        
        public int Bottom // Property.tt Line: 55
        { 
            get { return this._Bottom; }
            set
            {
                if (this._Bottom != value)
                {
                    this.OnBottomChanging(ref value);
                    this._Bottom = value;
                    this.OnBottomChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Bottom;
        partial void OnBottomChanging(ref int to); // Property.tt Line: 79
        partial void OnBottomChanged();
        #endregion Properties
    }
    public partial class FormPaddingValidator : ValidatorBase<FormPadding, FormPaddingValidator> { } // Class.tt Line: 6
    public partial class FormPadding : VmValidatableWithSeverity<FormPadding, FormPaddingValidator>, IFormPadding // Class.tt Line: 7
    {
        #region CTOR
        public FormPadding() 
            : base(FormPaddingValidator.Validator) // Class.tt Line: 45
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
        public static FormPadding Clone(IFormPadding from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            FormPadding vm = new FormPadding();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Left = from.Left; // Clone.tt Line: 65
            vm.Up = from.Up; // Clone.tt Line: 65
            vm.Right = from.Right; // Clone.tt Line: 65
            vm.Bottom = from.Bottom; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(FormPadding to, IFormPadding from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Left = from.Left; // Clone.tt Line: 141
            to.Up = from.Up; // Clone.tt Line: 141
            to.Right = from.Right; // Clone.tt Line: 141
            to.Bottom = from.Bottom; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override FormPadding Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return FormPadding.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(FormPadding from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            FormPadding.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_form_padding' to 'FormPadding'
        public static FormPadding ConvertToVM(Proto.Config.proto_form_padding m, FormPadding vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Left = m.Left; // Clone.tt Line: 221
            vm.Up = m.Up; // Clone.tt Line: 221
            vm.Right = m.Right; // Clone.tt Line: 221
            vm.Bottom = m.Bottom; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'FormPadding' to 'proto_form_padding'
        public static Proto.Config.proto_form_padding ConvertToProto(FormPadding vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_form_padding m = new Proto.Config.proto_form_padding(); // Clone.tt Line: 239
            m.Left = vm.Left; // Clone.tt Line: 276
            m.Up = vm.Up; // Clone.tt Line: 276
            m.Right = vm.Right; // Clone.tt Line: 276
            m.Bottom = vm.Bottom; // Clone.tt Line: 276
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        public int Left // Property.tt Line: 55
        { 
            get { return this._Left; }
            set
            {
                if (this._Left != value)
                {
                    this.OnLeftChanging(ref value);
                    this._Left = value;
                    this.OnLeftChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Left;
        partial void OnLeftChanging(ref int to); // Property.tt Line: 79
        partial void OnLeftChanged();
        
        public int Up // Property.tt Line: 55
        { 
            get { return this._Up; }
            set
            {
                if (this._Up != value)
                {
                    this.OnUpChanging(ref value);
                    this._Up = value;
                    this.OnUpChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Up;
        partial void OnUpChanging(ref int to); // Property.tt Line: 79
        partial void OnUpChanged();
        
        public int Right // Property.tt Line: 55
        { 
            get { return this._Right; }
            set
            {
                if (this._Right != value)
                {
                    this.OnRightChanging(ref value);
                    this._Right = value;
                    this.OnRightChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Right;
        partial void OnRightChanging(ref int to); // Property.tt Line: 79
        partial void OnRightChanged();
        
        public int Bottom // Property.tt Line: 55
        { 
            get { return this._Bottom; }
            set
            {
                if (this._Bottom != value)
                {
                    this.OnBottomChanging(ref value);
                    this._Bottom = value;
                    this.OnBottomChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private int _Bottom;
        partial void OnBottomChanging(ref int to); // Property.tt Line: 79
        partial void OnBottomChanged();
        #endregion Properties
    }
    public partial class FormStackpanelValidator : ValidatorBase<FormStackpanel, FormStackpanelValidator> { } // Class.tt Line: 6
    public partial class FormStackpanel : VmValidatableWithSeverity<FormStackpanel, FormStackpanelValidator>, IFormStackpanel // Class.tt Line: 7
    {
        #region CTOR
        public FormStackpanel() 
            : base(FormStackpanelValidator.Validator) // Class.tt Line: 45
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
        public static FormStackpanel Clone(IFormStackpanel from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            FormStackpanel vm = new FormStackpanel();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Orientation = from.Orientation; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(FormStackpanel to, IFormStackpanel from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Orientation = from.Orientation; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override FormStackpanel Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return FormStackpanel.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(FormStackpanel from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            FormStackpanel.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_form_stackpanel' to 'FormStackpanel'
        public static FormStackpanel ConvertToVM(Proto.Config.proto_form_stackpanel m, FormStackpanel vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Orientation = (FormOrientation)m.Orientation; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'FormStackpanel' to 'proto_form_stackpanel'
        public static Proto.Config.proto_form_stackpanel ConvertToProto(FormStackpanel vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_form_stackpanel m = new Proto.Config.proto_form_stackpanel(); // Clone.tt Line: 239
            m.Orientation = (Proto.Config.proto_form_orientation)vm.Orientation; // Clone.tt Line: 274
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
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
        
        public FormOrientation Orientation // Property.tt Line: 55
        { 
            get { return this._Orientation; }
            set
            {
                if (this._Orientation != value)
                {
                    this.OnOrientationChanging(ref value);
                    this._Orientation = value;
                    this.OnOrientationChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private FormOrientation _Orientation;
        partial void OnOrientationChanging(ref FormOrientation to); // Property.tt Line: 79
        partial void OnOrientationChanged();
        #endregion Properties
    }
    public partial class FormGridValidator : ValidatorBase<FormGrid, FormGridValidator> { } // Class.tt Line: 6
    public partial class FormGrid : VmValidatableWithSeverity<FormGrid, FormGridValidator>, IFormGrid // Class.tt Line: 7
    {
        #region CTOR
        public FormGrid() 
            : base(FormGridValidator.Validator) // Class.tt Line: 45
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.Marging = new FormMarging(); // Class.tt Line: 60
            this.Padding = new FormPadding(); // Class.tt Line: 60
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static FormGrid Clone(IFormGrid from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            FormGrid vm = new FormGrid();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            if (isDeep) // Clone.tt Line: 62
                vm.Marging = vSharpStudio.vm.ViewModels.FormMarging.Clone(from.Marging, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.Padding = vSharpStudio.vm.ViewModels.FormPadding.Clone(from.Padding, isDeep);
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(FormGrid to, IFormGrid from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.FormMarging.Update((FormMarging)to.Marging, from.Marging, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.FormPadding.Update((FormPadding)to.Padding, from.Padding, isDeep);
        }
        // Clone.tt Line: 147
        #region IEditable
        public override FormGrid Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return FormGrid.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(FormGrid from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            FormGrid.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_form_grid' to 'FormGrid'
        public static FormGrid ConvertToVM(Proto.Config.proto_form_grid m, FormGrid vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            if (vm.Marging == null) // Clone.tt Line: 213
                vm.Marging = new FormMarging(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.FormMarging.ConvertToVM(m.Marging, (FormMarging)vm.Marging); // Clone.tt Line: 219
            if (vm.Padding == null) // Clone.tt Line: 213
                vm.Padding = new FormPadding(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.FormPadding.ConvertToVM(m.Padding, (FormPadding)vm.Padding); // Clone.tt Line: 219
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'FormGrid' to 'proto_form_grid'
        public static Proto.Config.proto_form_grid ConvertToProto(FormGrid vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_form_grid m = new Proto.Config.proto_form_grid(); // Clone.tt Line: 239
            m.Marging = vSharpStudio.vm.ViewModels.FormMarging.ConvertToProto((FormMarging)vm.Marging); // Clone.tt Line: 270
            m.Padding = vSharpStudio.vm.ViewModels.FormPadding.ConvertToProto((FormPadding)vm.Padding); // Clone.tt Line: 270
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.Marging.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.Padding.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public FormMarging Marging // Property.tt Line: 55
        { 
            get { return this._Marging; }
            set
            {
                if (this._Marging != value)
                {
                    this.OnMargingChanging(ref value);
                    this._Marging = value;
                    this.OnMargingChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private FormMarging _Marging;
        IFormMarging IFormGrid.Marging { get { return (this as FormGrid).Marging; } } // Property.tt Line: 77
        partial void OnMargingChanging(ref FormMarging to); // Property.tt Line: 79
        partial void OnMargingChanged();
        //IFormMarging IFormGrid.Marging { get { return this._Marging; } }
        
        public FormPadding Padding // Property.tt Line: 55
        { 
            get { return this._Padding; }
            set
            {
                if (this._Padding != value)
                {
                    this.OnPaddingChanging(ref value);
                    this._Padding = value;
                    this.OnPaddingChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private FormPadding _Padding;
        IFormPadding IFormGrid.Padding { get { return (this as FormGrid).Padding; } } // Property.tt Line: 77
        partial void OnPaddingChanging(ref FormPadding to); // Property.tt Line: 79
        partial void OnPaddingChanged();
        //IFormPadding IFormGrid.Padding { get { return this._Padding; } }
        #endregion Properties
    }
    public partial class FormDatagridValidator : ValidatorBase<FormDatagrid, FormDatagridValidator> { } // Class.tt Line: 6
    public partial class FormDatagrid : VmValidatableWithSeverity<FormDatagrid, FormDatagridValidator>, IFormDatagrid // Class.tt Line: 7
    {
        #region CTOR
        public FormDatagrid() 
            : base(FormDatagridValidator.Validator) // Class.tt Line: 45
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.Marging = new FormMarging(); // Class.tt Line: 60
            this.Padding = new FormPadding(); // Class.tt Line: 60
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static FormDatagrid Clone(IFormDatagrid from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            FormDatagrid vm = new FormDatagrid();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            if (isDeep) // Clone.tt Line: 62
                vm.Marging = vSharpStudio.vm.ViewModels.FormMarging.Clone(from.Marging, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.Padding = vSharpStudio.vm.ViewModels.FormPadding.Clone(from.Padding, isDeep);
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(FormDatagrid to, IFormDatagrid from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.FormMarging.Update((FormMarging)to.Marging, from.Marging, isDeep);
            if (isDeep) // Clone.tt Line: 138
                vSharpStudio.vm.ViewModels.FormPadding.Update((FormPadding)to.Padding, from.Padding, isDeep);
        }
        // Clone.tt Line: 147
        #region IEditable
        public override FormDatagrid Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return FormDatagrid.Clone(this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(FormDatagrid from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            FormDatagrid.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_form_datagrid' to 'FormDatagrid'
        public static FormDatagrid ConvertToVM(Proto.Config.proto_form_datagrid m, FormDatagrid vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            if (vm.Marging == null) // Clone.tt Line: 213
                vm.Marging = new FormMarging(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.FormMarging.ConvertToVM(m.Marging, (FormMarging)vm.Marging); // Clone.tt Line: 219
            if (vm.Padding == null) // Clone.tt Line: 213
                vm.Padding = new FormPadding(); // Clone.tt Line: 217
            vSharpStudio.vm.ViewModels.FormPadding.ConvertToVM(m.Padding, (FormPadding)vm.Padding); // Clone.tt Line: 219
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'FormDatagrid' to 'proto_form_datagrid'
        public static Proto.Config.proto_form_datagrid ConvertToProto(FormDatagrid vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_form_datagrid m = new Proto.Config.proto_form_datagrid(); // Clone.tt Line: 239
            m.Marging = vSharpStudio.vm.ViewModels.FormMarging.ConvertToProto((FormMarging)vm.Marging); // Clone.tt Line: 270
            m.Padding = vSharpStudio.vm.ViewModels.FormPadding.ConvertToProto((FormPadding)vm.Padding); // Clone.tt Line: 270
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.Marging.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.Padding.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public FormMarging Marging // Property.tt Line: 55
        { 
            get { return this._Marging; }
            set
            {
                if (this._Marging != value)
                {
                    this.OnMargingChanging(ref value);
                    this._Marging = value;
                    this.OnMargingChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private FormMarging _Marging;
        IFormMarging IFormDatagrid.Marging { get { return (this as FormDatagrid).Marging; } } // Property.tt Line: 77
        partial void OnMargingChanging(ref FormMarging to); // Property.tt Line: 79
        partial void OnMargingChanged();
        //IFormMarging IFormDatagrid.Marging { get { return this._Marging; } }
        
        public FormPadding Padding // Property.tt Line: 55
        { 
            get { return this._Padding; }
            set
            {
                if (this._Padding != value)
                {
                    this.OnPaddingChanging(ref value);
                    this._Padding = value;
                    this.OnPaddingChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private FormPadding _Padding;
        IFormPadding IFormDatagrid.Padding { get { return (this as FormDatagrid).Padding; } } // Property.tt Line: 77
        partial void OnPaddingChanging(ref FormPadding to); // Property.tt Line: 79
        partial void OnPaddingChanged();
        //IFormPadding IFormDatagrid.Padding { get { return this._Padding; } }
        #endregion Properties
    }
    public partial class FormValidator : ValidatorBase<Form, FormValidator> { } // Class.tt Line: 6
    public partial class Form : ConfigObjectVmGenSettings<Form, FormValidator>, IComparable<Form>, IConfigAcceptVisitor, IForm // Class.tt Line: 7
    {
        #region CTOR
        public Form() : this(default(ITreeConfigNode))
        {
        }
        public Form(ITreeConfigNode parent) 
            : base(parent, FormValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Form Clone(ITreeConfigNode parent, IForm from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Form vm = new Form(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.EnumFormType = from.EnumFormType; // Clone.tt Line: 65
            foreach (var t in from.ListGuidProperties) // Clone.tt Line: 44
                vm.ListGuidProperties.Add(t);
            foreach (var t in from.ListGuidTreeProperties) // Clone.tt Line: 44
                vm.ListGuidTreeProperties.Add(t);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Form to, IForm from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.EnumFormType = from.EnumFormType; // Clone.tt Line: 141
                to.ListGuidProperties.Clear(); // Clone.tt Line: 127
                foreach (var tt in from.ListGuidProperties)
                {
                    to.ListGuidProperties.Add(tt);
                }
                to.ListGuidTreeProperties.Clear(); // Clone.tt Line: 127
                foreach (var tt in from.ListGuidTreeProperties)
                {
                    to.ListGuidTreeProperties.Add(tt);
                }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Form Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Form.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Form from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Form.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_form' to 'Form'
        public static Form ConvertToVM(Proto.Config.proto_form m, Form vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.EnumFormType = (FormView)m.EnumFormType; // Clone.tt Line: 221
            vm.ListGuidProperties = new ObservableCollection<string>(); // Clone.tt Line: 184
            foreach (var t in m.ListGuidProperties) // Clone.tt Line: 185
            {
                vm.ListGuidProperties.Add(t);
            }
            vm.ListGuidTreeProperties = new ObservableCollection<string>(); // Clone.tt Line: 184
            foreach (var t in m.ListGuidTreeProperties) // Clone.tt Line: 185
            {
                vm.ListGuidTreeProperties.Add(t);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Form' to 'proto_form'
        public static Proto.Config.proto_form ConvertToProto(Form vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_form m = new Proto.Config.proto_form(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.EnumFormType = (Proto.Config.proto_form_view)vm.EnumFormType; // Clone.tt Line: 274
            foreach (var t in vm.ListGuidProperties) // Clone.tt Line: 242
                m.ListGuidProperties.Add(t); // Clone.tt Line: 244
            foreach (var t in vm.ListGuidTreeProperties) // Clone.tt Line: 242
                m.ListGuidTreeProperties.Add(t); // Clone.tt Line: 244
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        
        ///////////////////////////////////////////////////
        /// 
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_forms = 7;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        [BrowsableAttribute(false)]
        public FormView EnumFormType // Property.tt Line: 55
        { 
            get { return this._EnumFormType; }
            set
            {
                if (this._EnumFormType != value)
                {
                    this.OnEnumFormTypeChanging(ref value);
                    this._EnumFormType = value;
                    this.OnEnumFormTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private FormView _EnumFormType;
        partial void OnEnumFormTypeChanging(ref FormView to); // Property.tt Line: 79
        partial void OnEnumFormTypeChanged();
        
        [BrowsableAttribute(false)]
        public ObservableCollection<string> ListGuidProperties // Property.tt Line: 8
        { 
            get { return this._ListGuidProperties; }
            set
            {
                if (this._ListGuidProperties != value)
                {
                    this.OnListGuidPropertiesChanging(value);
                    _ListGuidProperties = value;
                    this.OnListGuidPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ObservableCollection<string> _ListGuidProperties;
        IReadOnlyList<string> IForm.ListGuidProperties { get { return (this as Form).ListGuidProperties; } } // Property.tt Line: 26
        partial void OnListGuidPropertiesChanging(ObservableCollection<string> to); // Property.tt Line: 27
        partial void OnListGuidPropertiesChanged();
        
        [BrowsableAttribute(false)]
        public ObservableCollection<string> ListGuidTreeProperties // Property.tt Line: 8
        { 
            get { return this._ListGuidTreeProperties; }
            set
            {
                if (this._ListGuidTreeProperties != value)
                {
                    this.OnListGuidTreePropertiesChanging(value);
                    _ListGuidTreeProperties = value;
                    this.OnListGuidTreePropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ObservableCollection<string> _ListGuidTreeProperties;
        IReadOnlyList<string> IForm.ListGuidTreeProperties { get { return (this as Form).ListGuidTreeProperties; } } // Property.tt Line: 26
        partial void OnListGuidTreePropertiesChanging(ObservableCollection<string> to); // Property.tt Line: 27
        partial void OnListGuidTreePropertiesChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IForm.ListNodeGeneratorsSettings { get { return (this as Form).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { } // Class.tt Line: 6
    public partial class GroupListReports : ConfigObjectVmGenSettings<GroupListReports, GroupListReportsValidator>, IComparable<GroupListReports>, IConfigAcceptVisitor, IGroupListReports // Class.tt Line: 7
    {
        #region CTOR
        public GroupListReports() : this(default(ITreeConfigNode))
        {
        }
        public GroupListReports(ITreeConfigNode parent) 
            : base(parent, GroupListReportsValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListReports = new ConfigNodesCollection<Report>(this); // Class.tt Line: 27
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(Report)) // Clone.tt Line: 15
            {
                this.ListReports.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static GroupListReports Clone(ITreeConfigNode parent, IGroupListReports from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListReports vm = new GroupListReports(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListReports) // Clone.tt Line: 52
                vm.ListReports.Add(Report.Clone(vm, (Report)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListReports to, IGroupListReports from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListReports.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListReports)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            Report.Update((Report)t, (Report)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListReports.Remove(t);
                }
                foreach (var tt in from.ListReports)
                {
                    bool isfound = false;
                    foreach (var t in to.ListReports.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new Report(to); // Clone.tt Line: 117
                        Report.Update(p, (Report)tt, isDeep);
                        to.ListReports.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override GroupListReports Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GroupListReports.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GroupListReports from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GroupListReports.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_group_list_reports' to 'GroupListReports'
        public static GroupListReports ConvertToVM(Proto.Config.proto_group_list_reports m, GroupListReports vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListReports) // Clone.tt Line: 201
            {
                var tvm = Report.ConvertToVM(t, new Report(vm)); // Clone.tt Line: 204
                vm.ListReports.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListReports' to 'proto_group_list_reports'
        public static Proto.Config.proto_group_list_reports ConvertToProto(GroupListReports vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_reports m = new Proto.Config.proto_group_list_reports(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            foreach (var t in vm.ListReports) // Clone.tt Line: 242
                m.ListReports.Add(Report.ConvertToProto((Report)t)); // Clone.tt Line: 246
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListReports)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Report> ListReports // Property.tt Line: 8
        { 
            get { return this._ListReports; }
            set
            {
                if (this._ListReports != value)
                {
                    this.OnListReportsChanging(value);
                    _ListReports = value;
                    this.OnListReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Report> _ListReports;
        IReadOnlyList<IReport> IGroupListReports.ListReports { get { return (this as GroupListReports).ListReports; } } // Property.tt Line: 26
        partial void OnListReportsChanging(ObservableCollection<Report> to); // Property.tt Line: 27
        partial void OnListReportsChanged();
        public Report this[int index] { get { return (Report)this.ListReports[index]; } }
        IReport IGroupListReports.this[int index] { get { return (Report)this.ListReports[index]; } }
        public void Add(Report item) // Property.tt Line: 32
        { 
            Contract.Requires(item != null);
            this.ListReports.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Report> items) 
        { 
            Contract.Requires(items != null);
            this.ListReports.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() { return this.ListReports.Count; }
        int IGroupListReports.Count() { return this.Count(); }
        public void Remove(Report item) 
        {
            Contract.Requires(item != null);
            this.ListReports.Remove(item); 
            item.Parent = null;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IGroupListReports.ListNodeGeneratorsSettings { get { return (this as GroupListReports).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        #endregion Properties
    }
    public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { } // Class.tt Line: 6
    public partial class Report : ConfigObjectVmGenSettings<Report, ReportValidator>, IComparable<Report>, IConfigAcceptVisitor, IReport // Class.tt Line: 7
    {
        #region CTOR
        public Report() : this(default(ITreeConfigNode))
        {
        }
        public Report(ITreeConfigNode parent) 
            : base(parent, ReportValidator.Validator) // Class.tt Line: 15
        {
            this.IsNotifying = false;
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 27
            this.OnInit();
            this.IsValidate = true;
            this.IsNotifying = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static Report Clone(ITreeConfigNode parent, IReport from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Report vm = new Report(parent);
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Report to, IReport from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            PluginGeneratorNodeSettings.Update((PluginGeneratorNodeSettings)t, (PluginGeneratorNodeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListNodeGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListNodeGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                    {
                        if (t.Guid == tt.Guid)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 117
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 147
        #region IEditable
        public override Report Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return Report.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(Report from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            Report.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_report' to 'Report'
        public static Report ConvertToVM(Proto.Config.proto_report m, Report vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Report' to 'proto_report'
        public static Proto.Config.proto_report ConvertToProto(Report vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_report m = new Proto.Config.proto_report(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 242
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 246
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            Contract.Requires(visitor != null);
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        [PropertyOrderAttribute(1)]
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        [BrowsableAttribute(false)]
        public ulong SortingValue // Property.tt Line: 55
        { 
            get { return this._SortingValue; }
            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging(ref value);
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnSortingValueChanging(ref ulong to); // Property.tt Line: 79
        partial void OnSortingValueChanged();
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 55
        { 
            get { return this._NameUi; }
            set
            {
                if (this._NameUi != value)
                {
                    this.OnNameUiChanging(ref value);
                    this._NameUi = value;
                    this.OnNameUiChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 79
        partial void OnNameUiChanged();
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 55
        { 
            get { return this._Description; }
            set
            {
                if (this._Description != value)
                {
                    this.OnDescriptionChanging(ref value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 79
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 55
        { 
            get { return this._IsNew; }
            set
            {
                if (this._IsNew != value)
                {
                    this.OnIsNewChanging(ref value);
                    this._IsNew = value;
                    this.OnIsNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsNew;
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsNewChanged();
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 55
        { 
            get { return this._IsMarkedForDeletion; }
            set
            {
                if (this._IsMarkedForDeletion != value)
                {
                    this.OnIsMarkedForDeletionChanging(ref value);
                    this._IsMarkedForDeletion = value;
                    this.OnIsMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsMarkedForDeletion;
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsMarkedForDeletionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_documents = 7;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 8
        { 
            get { return this._ListNodeGeneratorsSettings; }
            set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    _ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        IReadOnlyList<IPluginGeneratorNodeSettings> IReport.ListNodeGeneratorsSettings { get { return (this as Report).ListNodeGeneratorsSettings; } } // Property.tt Line: 26
        partial void OnListNodeGeneratorsSettingsChanging(ObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 27
        partial void OnListNodeGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        override public bool IsChanged // Class.tt Line: 103
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
                    var cfg = (Config)this.GetConfig();
                    if (cfg != null && cfg.SelectedNode != null)
                        cfg.ValidateSubTreeFromNode(cfg.SelectedNode);
                }
            }
        }
        partial void OnIsChangedChanging(ref bool v); // Class.tt Line: 123
        protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class ModelRowValidator : ValidatorBase<ModelRow, ModelRowValidator> { } // Class.tt Line: 6
    public partial class ModelRow : VmBindable, IModelRow // Class.tt Line: 7
    {
        #region CTOR
        public ModelRow() // Class.tt Line: 45
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
        public static ModelRow Clone(IModelRow from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            ModelRow vm = new ModelRow();
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.GroupName = from.GroupName; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.IsIncluded = from.IsIncluded; // Clone.tt Line: 65
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(ModelRow to, IModelRow from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.GroupName = from.GroupName; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.IsIncluded = from.IsIncluded; // Clone.tt Line: 141
        }
        // Conversion from 'proto_model_row' to 'ModelRow'
        public static ModelRow ConvertToVM(Proto.Config.proto_model_row m, ModelRow vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotifying = false;
            vm.IsValidate = false;
            vm.GroupName = m.GroupName; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.IsIncluded = m.IsIncluded; // Clone.tt Line: 221
            vm.IsNotifying = true;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'ModelRow' to 'proto_model_row'
        public static Proto.Config.proto_model_row ConvertToProto(ModelRow vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_model_row m = new Proto.Config.proto_model_row(); // Clone.tt Line: 239
            m.GroupName = vm.GroupName; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.IsIncluded = vm.IsIncluded; // Clone.tt Line: 276
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public string GroupName // Property.tt Line: 55
        { 
            get { return this._GroupName; }
            set
            {
                if (this._GroupName != value)
                {
                    this.OnGroupNameChanging(ref value);
                    this._GroupName = value;
                    this.OnGroupNameChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private string _GroupName = string.Empty;
        partial void OnGroupNameChanging(ref string to); // Property.tt Line: 79
        partial void OnGroupNameChanged();
        
        public string Name // Property.tt Line: 55
        { 
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this.OnNameChanging(ref value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private string _Name = string.Empty;
        partial void OnNameChanging(ref string to); // Property.tt Line: 79
        partial void OnNameChanged();
        
        public string Guid // Property.tt Line: 55
        { 
            get { return this._Guid; }
            set
            {
                if (this._Guid != value)
                {
                    this.OnGuidChanging(ref value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private string _Guid = string.Empty;
        partial void OnGuidChanging(ref string to); // Property.tt Line: 79
        partial void OnGuidChanged();
        
        public bool IsIncluded // Property.tt Line: 55
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
                }
            }
        }
        private bool _IsIncluded;
        partial void OnIsIncludedChanging(ref bool to); // Property.tt Line: 79
        partial void OnIsIncludedChanged();
        #endregion Properties
    }
    
    public interface IVisitorProto // IVisitorProto.tt Line: 7
    {
        void Visit(Proto.Config.proto_user_settings p);
        void Visit(Proto.Config.proto_user_settings_opened_config p);
        void Visit(Proto.Config.proto_group_list_plugins p);
        void Visit(Proto.Config.proto_plugin p);
        void Visit(Proto.Config.proto_plugin_generator p);
        void Visit(Proto.Config.proto_settings_config p);
        void Visit(Proto.Config.proto_config_short_history p);
        void Visit(Proto.Config.proto_group_list_base_config_links p);
        void Visit(Proto.Config.proto_base_config_link p);
        void Visit(Proto.Config.proto_config p);
        void Visit(Proto.Config.proto_app_db_settings p);
        void Visit(Proto.Config.proto_plugin_group_generators_default_settings p);
        void Visit(Proto.Config.proto_group_list_app_solutions p);
        void Visit(Proto.Config.proto_plugin_group_generators_settings p);
        void Visit(Proto.Config.proto_app_solution p);
        void Visit(Proto.Config.proto_app_project p);
        void Visit(Proto.Config.proto_plugin_generator_node_settings p);
        void Visit(Proto.Config.proto_plugin_generator_settings p);
        void Visit(Proto.Config.proto_app_project_generator p);
        void Visit(Proto.Config.proto_plugin_generator_node_default_settings p);
        void Visit(Proto.Config.db_settings p);
        void Visit(Proto.Config.proto_model p);
        void Visit(Proto.Config.proto_data_type p);
        void Visit(Proto.Config.proto_group_list_common p);
        void Visit(Proto.Config.proto_role p);
        void Visit(Proto.Config.proto_group_list_roles p);
        void Visit(Proto.Config.proto_main_view_form p);
        void Visit(Proto.Config.proto_group_list_main_view_forms p);
        void Visit(Proto.Config.proto_group_list_properties_tabs p);
        void Visit(Proto.Config.proto_properties_tab p);
        void Visit(Proto.Config.proto_group_list_properties p);
        void Visit(Proto.Config.proto_property p);
        void Visit(Proto.Config.proto_group_constant_groups p);
        void Visit(Proto.Config.proto_group_list_constants p);
        void Visit(Proto.Config.proto_constant p);
        void Visit(Proto.Config.proto_group_list_enumerations p);
        void Visit(Proto.Config.proto_enumeration p);
        void Visit(Proto.Config.proto_enumeration_pair p);
        void Visit(Proto.Config.proto_catalog_folder p);
        void Visit(Proto.Config.proto_catalog_code_property_settings p);
        void Visit(Proto.Config.proto_catalog p);
        void Visit(Proto.Config.proto_group_list_catalogs p);
        void Visit(Proto.Config.proto_group_documents p);
        void Visit(Proto.Config.proto_document p);
        void Visit(Proto.Config.proto_group_list_documents p);
        void Visit(Proto.Config.proto_group_list_journals p);
        void Visit(Proto.Config.proto_journal p);
        void Visit(Proto.Config.proto_group_list_forms p);
        void Visit(Proto.Config.proto_form_marging p);
        void Visit(Proto.Config.proto_form_padding p);
        void Visit(Proto.Config.proto_form_stackpanel p);
        void Visit(Proto.Config.proto_form_grid p);
        void Visit(Proto.Config.proto_form_datagrid p);
        void Visit(Proto.Config.proto_form p);
        void Visit(Proto.Config.proto_group_list_reports p);
        void Visit(Proto.Config.proto_report p);
        void Visit(Proto.Config.proto_model_row p);
    }
    
    public partial class ValidationConfigVisitor : ConfigVisitor // ValidationVisitor.tt Line: 7
    {
        partial void OnVisit(IValidatableWithSeverity p);
        partial void OnVisitEnd(IValidatableWithSeverity p);
        protected override void OnVisit(UserSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            foreach (var t in p.ListOpenConfigHistory) // ValidationVisitor.tt Line: 38
                p.ValidateSubAndCollectErrors(t);
        }
        protected override void OnVisitEnd(UserSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(UserSettingsOpenedConfig p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(UserSettingsOpenedConfig p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListPlugins p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListPlugins p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Plugin p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Plugin p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGenerator p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGenerator p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(SettingsConfig p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(SettingsConfig p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(ConfigShortHistory p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(ConfigShortHistory p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListBaseConfigLinks p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListBaseConfigLinks p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(BaseConfigLink p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(BaseConfigLink p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Config p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Config p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppDbSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(AppDbSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGroupGeneratorsDefaultSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGroupGeneratorsDefaultSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListAppSolutions p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            foreach (var t in p.ListGroupGeneratorsDefultSettings) // ValidationVisitor.tt Line: 28
                ValidateSubAndCollectErrors(p, t);
        }
        protected override void OnVisitEnd(GroupListAppSolutions p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGroupGeneratorsSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGroupGeneratorsSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppSolution p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(AppSolution p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppProject p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(AppProject p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGeneratorNodeSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGeneratorNodeSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGeneratorSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGeneratorSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppProjectGenerator p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.GeneratorSettingsVm); // ValidationVisitor.tt Line: 31
        }
        protected override void OnVisitEnd(AppProjectGenerator p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGeneratorNodeDefaultSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGeneratorNodeDefaultSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(DbSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(DbSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Model p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DbSettings); // ValidationVisitor.tt Line: 31
        }
        protected override void OnVisitEnd(Model p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(DataType p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(DataType p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListCommon p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListCommon p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Role p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Role p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListRoles p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListRoles p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(MainViewForm p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(MainViewForm p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListMainViewForms p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListMainViewForms p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListPropertiesTabs p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListPropertiesTabs p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PropertiesTab p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PropertiesTab p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListProperties p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListProperties p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Property p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DataType); // ValidationVisitor.tt Line: 31
        }
        protected override void OnVisitEnd(Property p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupConstantGroups p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupConstantGroups p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListConstants p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListConstants p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Constant p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DataType); // ValidationVisitor.tt Line: 31
        }
        protected override void OnVisitEnd(Constant p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListEnumerations p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListEnumerations p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Enumeration p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Enumeration p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(EnumerationPair p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(EnumerationPair p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(CatalogFolder p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.CodePropertySettings); // ValidationVisitor.tt Line: 31
        }
        protected override void OnVisitEnd(CatalogFolder p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(CatalogCodePropertySettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(CatalogCodePropertySettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Catalog p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.CodePropertySettings); // ValidationVisitor.tt Line: 31
        }
        protected override void OnVisitEnd(Catalog p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListCatalogs p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListCatalogs p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupDocuments p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupDocuments p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Document p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Document p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListDocuments p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListDocuments p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListJournals p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListJournals p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Journal p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Journal p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListForms p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListForms p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(FormMarging p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(FormMarging p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(FormPadding p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(FormPadding p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(FormStackpanel p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(FormStackpanel p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(FormGrid p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            p.ValidateSubAndCollectErrors(p.Marging); // ValidationVisitor.tt Line: 41
            p.ValidateSubAndCollectErrors(p.Padding); // ValidationVisitor.tt Line: 41
        }
        protected override void OnVisitEnd(FormGrid p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(FormDatagrid p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
            p.ValidateSubAndCollectErrors(p.Marging); // ValidationVisitor.tt Line: 41
            p.ValidateSubAndCollectErrors(p.Padding); // ValidationVisitor.tt Line: 41
        }
        protected override void OnVisitEnd(FormDatagrid p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Form p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Form p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListReports p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListReports p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Report p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Report p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(ModelRow p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(ModelRow p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
    }
    
    public partial class ConfigVisitor : IVisitorConfigNode // NodeVisitor.tt Line: 7
    {
        public CancellationToken Token { get { return _cancellationToken; } }
        protected CancellationToken _cancellationToken;
    
        public void Visit(UserSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(UserSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(UserSettings p) { }
        protected virtual void OnVisitEnd(UserSettings p) { }
        public void Visit(UserSettingsOpenedConfig p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(UserSettingsOpenedConfig p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(UserSettingsOpenedConfig p) { }
        protected virtual void OnVisitEnd(UserSettingsOpenedConfig p) { }
        public void Visit(GroupListPlugins p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListPlugins p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListPlugins p) { }
        protected virtual void OnVisitEnd(GroupListPlugins p) { }
        public void Visit(Plugin p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Plugin p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Plugin p) { }
        protected virtual void OnVisitEnd(Plugin p) { }
        public void Visit(PluginGenerator p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginGenerator p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginGenerator p) { }
        protected virtual void OnVisitEnd(PluginGenerator p) { }
        public void Visit(SettingsConfig p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(SettingsConfig p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(SettingsConfig p) { }
        protected virtual void OnVisitEnd(SettingsConfig p) { }
        public void Visit(ConfigShortHistory p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(ConfigShortHistory p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(ConfigShortHistory p) { }
        protected virtual void OnVisitEnd(ConfigShortHistory p) { }
        public void Visit(GroupListBaseConfigLinks p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListBaseConfigLinks p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListBaseConfigLinks p) { }
        protected virtual void OnVisitEnd(GroupListBaseConfigLinks p) { }
        public void Visit(BaseConfigLink p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(BaseConfigLink p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(BaseConfigLink p) { }
        protected virtual void OnVisitEnd(BaseConfigLink p) { }
        public void Visit(Config p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Config p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Config p) { }
        protected virtual void OnVisitEnd(Config p) { }
        public void Visit(AppDbSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(AppDbSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(AppDbSettings p) { }
        protected virtual void OnVisitEnd(AppDbSettings p) { }
        public void Visit(PluginGroupGeneratorsDefaultSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginGroupGeneratorsDefaultSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginGroupGeneratorsDefaultSettings p) { }
        protected virtual void OnVisitEnd(PluginGroupGeneratorsDefaultSettings p) { }
        public void Visit(GroupListAppSolutions p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListAppSolutions p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListAppSolutions p) { }
        protected virtual void OnVisitEnd(GroupListAppSolutions p) { }
        public void Visit(PluginGroupGeneratorsSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginGroupGeneratorsSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginGroupGeneratorsSettings p) { }
        protected virtual void OnVisitEnd(PluginGroupGeneratorsSettings p) { }
        public void Visit(AppSolution p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(AppSolution p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(AppSolution p) { }
        protected virtual void OnVisitEnd(AppSolution p) { }
        public void Visit(AppProject p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(AppProject p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(AppProject p) { }
        protected virtual void OnVisitEnd(AppProject p) { }
        public void Visit(PluginGeneratorNodeSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginGeneratorNodeSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginGeneratorNodeSettings p) { }
        protected virtual void OnVisitEnd(PluginGeneratorNodeSettings p) { }
        public void Visit(PluginGeneratorSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginGeneratorSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginGeneratorSettings p) { }
        protected virtual void OnVisitEnd(PluginGeneratorSettings p) { }
        public void Visit(AppProjectGenerator p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(AppProjectGenerator p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(AppProjectGenerator p) { }
        protected virtual void OnVisitEnd(AppProjectGenerator p) { }
        public void Visit(PluginGeneratorNodeDefaultSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginGeneratorNodeDefaultSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginGeneratorNodeDefaultSettings p) { }
        protected virtual void OnVisitEnd(PluginGeneratorNodeDefaultSettings p) { }
        public void Visit(DbSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(DbSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(DbSettings p) { }
        protected virtual void OnVisitEnd(DbSettings p) { }
        public void Visit(Model p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Model p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Model p) { }
        protected virtual void OnVisitEnd(Model p) { }
        public void Visit(DataType p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(DataType p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(DataType p) { }
        protected virtual void OnVisitEnd(DataType p) { }
        public void Visit(GroupListCommon p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListCommon p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListCommon p) { }
        protected virtual void OnVisitEnd(GroupListCommon p) { }
        public void Visit(Role p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Role p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Role p) { }
        protected virtual void OnVisitEnd(Role p) { }
        public void Visit(GroupListRoles p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListRoles p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListRoles p) { }
        protected virtual void OnVisitEnd(GroupListRoles p) { }
        public void Visit(MainViewForm p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(MainViewForm p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(MainViewForm p) { }
        protected virtual void OnVisitEnd(MainViewForm p) { }
        public void Visit(GroupListMainViewForms p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListMainViewForms p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListMainViewForms p) { }
        protected virtual void OnVisitEnd(GroupListMainViewForms p) { }
        public void Visit(GroupListPropertiesTabs p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListPropertiesTabs p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListPropertiesTabs p) { }
        protected virtual void OnVisitEnd(GroupListPropertiesTabs p) { }
        public void Visit(PropertiesTab p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PropertiesTab p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PropertiesTab p) { }
        protected virtual void OnVisitEnd(PropertiesTab p) { }
        public void Visit(GroupListProperties p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListProperties p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListProperties p) { }
        protected virtual void OnVisitEnd(GroupListProperties p) { }
        public void Visit(Property p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Property p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Property p) { }
        protected virtual void OnVisitEnd(Property p) { }
        public void Visit(GroupConstantGroups p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupConstantGroups p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupConstantGroups p) { }
        protected virtual void OnVisitEnd(GroupConstantGroups p) { }
        public void Visit(GroupListConstants p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListConstants p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListConstants p) { }
        protected virtual void OnVisitEnd(GroupListConstants p) { }
        public void Visit(Constant p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Constant p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Constant p) { }
        protected virtual void OnVisitEnd(Constant p) { }
        public void Visit(GroupListEnumerations p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListEnumerations p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListEnumerations p) { }
        protected virtual void OnVisitEnd(GroupListEnumerations p) { }
        public void Visit(Enumeration p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Enumeration p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Enumeration p) { }
        protected virtual void OnVisitEnd(Enumeration p) { }
        public void Visit(EnumerationPair p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(EnumerationPair p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(EnumerationPair p) { }
        protected virtual void OnVisitEnd(EnumerationPair p) { }
        public void Visit(CatalogFolder p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(CatalogFolder p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(CatalogFolder p) { }
        protected virtual void OnVisitEnd(CatalogFolder p) { }
        public void Visit(CatalogCodePropertySettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(CatalogCodePropertySettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(CatalogCodePropertySettings p) { }
        protected virtual void OnVisitEnd(CatalogCodePropertySettings p) { }
        public void Visit(Catalog p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Catalog p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Catalog p) { }
        protected virtual void OnVisitEnd(Catalog p) { }
        public void Visit(GroupListCatalogs p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListCatalogs p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListCatalogs p) { }
        protected virtual void OnVisitEnd(GroupListCatalogs p) { }
        public void Visit(GroupDocuments p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupDocuments p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupDocuments p) { }
        protected virtual void OnVisitEnd(GroupDocuments p) { }
        public void Visit(Document p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Document p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Document p) { }
        protected virtual void OnVisitEnd(Document p) { }
        public void Visit(GroupListDocuments p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListDocuments p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListDocuments p) { }
        protected virtual void OnVisitEnd(GroupListDocuments p) { }
        public void Visit(GroupListJournals p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListJournals p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListJournals p) { }
        protected virtual void OnVisitEnd(GroupListJournals p) { }
        public void Visit(Journal p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Journal p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Journal p) { }
        protected virtual void OnVisitEnd(Journal p) { }
        public void Visit(GroupListForms p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListForms p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListForms p) { }
        protected virtual void OnVisitEnd(GroupListForms p) { }
        public void Visit(FormMarging p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(FormMarging p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(FormMarging p) { }
        protected virtual void OnVisitEnd(FormMarging p) { }
        public void Visit(FormPadding p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(FormPadding p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(FormPadding p) { }
        protected virtual void OnVisitEnd(FormPadding p) { }
        public void Visit(FormStackpanel p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(FormStackpanel p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(FormStackpanel p) { }
        protected virtual void OnVisitEnd(FormStackpanel p) { }
        public void Visit(FormGrid p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(FormGrid p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(FormGrid p) { }
        protected virtual void OnVisitEnd(FormGrid p) { }
        public void Visit(FormDatagrid p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(FormDatagrid p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(FormDatagrid p) { }
        protected virtual void OnVisitEnd(FormDatagrid p) { }
        public void Visit(Form p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Form p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Form p) { }
        protected virtual void OnVisitEnd(Form p) { }
        public void Visit(GroupListReports p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GroupListReports p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GroupListReports p) { }
        protected virtual void OnVisitEnd(GroupListReports p) { }
        public void Visit(Report p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(Report p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(Report p) { }
        protected virtual void OnVisitEnd(Report p) { }
        public void Visit(ModelRow p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(ModelRow p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(ModelRow p) { }
        protected virtual void OnVisitEnd(ModelRow p) { }
    }
    
    public interface IVisitorConfigNode // IVisitorConfigNode.tt Line: 7
    {
        System.Threading.CancellationToken Token { get; }
        void Visit(Config p);
        void VisitEnd(Config p);
        void Visit(Model p);
        void VisitEnd(Model p);
        void Visit(GroupListCommon p);
        void VisitEnd(GroupListCommon p);
        void Visit(Role p);
        void VisitEnd(Role p);
        void Visit(GroupListRoles p);
        void VisitEnd(GroupListRoles p);
        void Visit(MainViewForm p);
        void VisitEnd(MainViewForm p);
        void Visit(GroupListMainViewForms p);
        void VisitEnd(GroupListMainViewForms p);
        void Visit(GroupListPropertiesTabs p);
        void VisitEnd(GroupListPropertiesTabs p);
        void Visit(PropertiesTab p);
        void VisitEnd(PropertiesTab p);
        void Visit(GroupListProperties p);
        void VisitEnd(GroupListProperties p);
        void Visit(Property p);
        void VisitEnd(Property p);
        void Visit(GroupConstantGroups p);
        void VisitEnd(GroupConstantGroups p);
        void Visit(GroupListConstants p);
        void VisitEnd(GroupListConstants p);
        void Visit(Constant p);
        void VisitEnd(Constant p);
        void Visit(GroupListEnumerations p);
        void VisitEnd(GroupListEnumerations p);
        void Visit(Enumeration p);
        void VisitEnd(Enumeration p);
        void Visit(EnumerationPair p);
        void VisitEnd(EnumerationPair p);
        void Visit(CatalogFolder p);
        void VisitEnd(CatalogFolder p);
        void Visit(Catalog p);
        void VisitEnd(Catalog p);
        void Visit(GroupListCatalogs p);
        void VisitEnd(GroupListCatalogs p);
        void Visit(GroupDocuments p);
        void VisitEnd(GroupDocuments p);
        void Visit(Document p);
        void VisitEnd(Document p);
        void Visit(GroupListDocuments p);
        void VisitEnd(GroupListDocuments p);
        void Visit(GroupListJournals p);
        void VisitEnd(GroupListJournals p);
        void Visit(Journal p);
        void VisitEnd(Journal p);
        void Visit(GroupListForms p);
        void VisitEnd(GroupListForms p);
        void Visit(Form p);
        void VisitEnd(Form p);
        void Visit(GroupListReports p);
        void VisitEnd(GroupListReports p);
        void Visit(Report p);
        void VisitEnd(Report p);
    }
}