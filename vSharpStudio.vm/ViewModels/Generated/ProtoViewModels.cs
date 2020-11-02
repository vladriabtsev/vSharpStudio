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
            : base(UserSettingsValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Class.tt Line: 52
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static UserSettings Clone(UserSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            UserSettings vm = new UserSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Clone.tt Line: 47
            foreach (var t in from.ListOpenConfigHistory) // Clone.tt Line: 48
                vm.ListOpenConfigHistory.Add(UserSettingsOpenedConfig.Clone((UserSettingsOpenedConfig)t, isDeep));
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(UserSettings to, UserSettings from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Clone.tt Line: 190
            foreach (var t in m.ListOpenConfigHistory) // Clone.tt Line: 191
            {
                var tvm = UserSettingsOpenedConfig.ConvertToVM(t, new UserSettingsOpenedConfig()); // Clone.tt Line: 196
                vm.ListOpenConfigHistory.Add(tvm);
            }
            vm.IsNotNotifying = false;
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
        #endregion Procedures
        #region Properties
        
        [BrowsableAttribute(false)]
        public ObservableCollection<UserSettingsOpenedConfig> ListOpenConfigHistory // Property.tt Line: 9
        { 
            get 
            { 
                return this._ListOpenConfigHistory; 
            }
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
        partial void OnListOpenConfigHistoryChanging(ObservableCollection<UserSettingsOpenedConfig> to); // Property.tt Line: 30
        partial void OnListOpenConfigHistoryChanged();
        IEnumerable<IUserSettingsOpenedConfig> IUserSettings.ListOpenConfigHistory { get { return this._ListOpenConfigHistory; } }
        #endregion Properties
    }
    public partial class UserSettingsOpenedConfigValidator : ValidatorBase<UserSettingsOpenedConfig, UserSettingsOpenedConfigValidator> { } // Class.tt Line: 6
    public partial class UserSettingsOpenedConfig : VmValidatableWithSeverity<UserSettingsOpenedConfig, UserSettingsOpenedConfigValidator>, IUserSettingsOpenedConfig // Class.tt Line: 7
    {
        #region CTOR
        public UserSettingsOpenedConfig() 
            : base(UserSettingsOpenedConfigValidator.Validator) // Class.tt Line: 43
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
        public static UserSettingsOpenedConfig Clone(UserSettingsOpenedConfig from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            UserSettingsOpenedConfig vm = new UserSettingsOpenedConfig();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.OpenedLastTimeOn = from.OpenedLastTimeOn; // Clone.tt Line: 65
            vm.ConfigPath = from.ConfigPath; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(UserSettingsOpenedConfig to, UserSettingsOpenedConfig from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.OpenedLastTimeOn = m.OpenedLastTimeOn; // Clone.tt Line: 221
            vm.ConfigPath = m.ConfigPath; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'UserSettingsOpenedConfig' to 'proto_user_settings_opened_config'
        public static Proto.Config.proto_user_settings_opened_config ConvertToProto(UserSettingsOpenedConfig vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_user_settings_opened_config m = new Proto.Config.proto_user_settings_opened_config(); // Clone.tt Line: 239
            m.OpenedLastTimeOn = vm.OpenedLastTimeOn; // Clone.tt Line: 276
            m.ConfigPath = vm.ConfigPath; // Clone.tt Line: 276
            return m;
        }
        #endregion Procedures
        #region Properties
        
        [BrowsableAttribute(false)]
        public Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn // Property.tt Line: 138
        { 
            get 
            { 
                return this._OpenedLastTimeOn; 
            }
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
        partial void OnOpenedLastTimeOnChanging(ref Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 160
        partial void OnOpenedLastTimeOnChanged();
        Google.Protobuf.WellKnownTypes.Timestamp IUserSettingsOpenedConfig.OpenedLastTimeOn { get { return this._OpenedLastTimeOn; } } 
        
        [BrowsableAttribute(false)]
        public string ConfigPath // Property.tt Line: 138
        { 
            get 
            { 
                return this._ConfigPath; 
            }
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
        partial void OnConfigPathChanging(ref string to); // Property.tt Line: 160
        partial void OnConfigPathChanged();
        string IUserSettingsOpenedConfig.ConfigPath { get { return this._ConfigPath; } } 
        #endregion Properties
    }
    public partial class GroupListPluginsValidator : ValidatorBase<GroupListPlugins, GroupListPluginsValidator> { } // Class.tt Line: 6
    public partial class GroupListPlugins : ConfigObjectVmBase<GroupListPlugins, GroupListPluginsValidator>, IComparable<GroupListPlugins>, IConfigAcceptVisitor, IGroupListPlugins // Class.tt Line: 7
    {
        #region CTOR
        public GroupListPlugins() : this((ITreeConfigNode)null)
        {
        }
        public GroupListPlugins(ITreeConfigNode parent) 
            : base(parent, GroupListPluginsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListPlugins = new ConfigNodesCollection<Plugin>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListPlugins Clone(ITreeConfigNode parent, GroupListPlugins from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListPlugins vm = new GroupListPlugins(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListPlugins) // Clone.tt Line: 52
                vm.ListPlugins.Add(Plugin.Clone(vm, (Plugin)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListPlugins to, GroupListPlugins from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListPlugins.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListPlugins)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListPlugins) // Clone.tt Line: 201
            {
                var tvm = Plugin.ConvertToVM(t, new Plugin(vm)); // Clone.tt Line: 204
                vm.ListPlugins.Add(tvm);
            }
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'GroupListPlugins' to 'proto_group_list_plugins'
        public static Proto.Config.proto_group_list_plugins ConvertToProto(GroupListPlugins vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_group_list_plugins m = new Proto.Config.proto_group_list_plugins(); // Clone.tt Line: 239
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            foreach (var t in vm.ListPlugins) // Clone.tt Line: 242
                m.ListPlugins.Add(Plugin.ConvertToProto((Plugin)t)); // Clone.tt Line: 246
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Plugin> ListPlugins // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListPlugins; 
            }
            private set
            {
                if (this._ListPlugins != value)
                {
                    this.OnListPluginsChanging(value);
                    this._ListPlugins = value;
                    this.OnListPluginsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Plugin> _ListPlugins;
        partial void OnListPluginsChanging(SortedObservableCollection<Plugin> to); // Property.tt Line: 79
        partial void OnListPluginsChanged();
        IEnumerable<IPlugin> IGroupListPlugins.ListPlugins { get { return this._ListPlugins; } }
        public Plugin this[int index] { get { return (Plugin)this.ListPlugins[index]; } }
        public void Add(Plugin item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListPlugins.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Plugin> items) 
        { 
            Contract.Requires(items != null);
            this.ListPlugins.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListPlugins.Count; 
        }
        public void Remove(Plugin item) 
        {
            Contract.Requires(item != null);
            this.ListPlugins.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListPlugins.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListPlugins.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListPlugins.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListPlugins.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListPlugins.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginValidator : ValidatorBase<Plugin, PluginValidator> { } // Class.tt Line: 6
    public partial class Plugin : ConfigObjectVmBase<Plugin, PluginValidator>, IComparable<Plugin>, IConfigAcceptVisitor, IPlugin // Class.tt Line: 7
    {
        #region CTOR
        public Plugin() : this((ITreeConfigNode)null)
        {
        }
        public Plugin(ITreeConfigNode parent) 
            : base(parent, PluginValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListGenerators = new ConfigNodesCollection<PluginGenerator>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Plugin Clone(ITreeConfigNode parent, Plugin from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Plugin vm = new Plugin(parent);
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Plugin to, Plugin from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        public string Version // Property.tt Line: 138
        { 
            get 
            { 
                return this._Version; 
            }
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
        partial void OnVersionChanging(ref string to); // Property.tt Line: 160
        partial void OnVersionChanged();
        string IPlugin.Version { get { return this._Version; } } 
        
        [ReadOnly(true)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IPlugin.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGenerator> ListGenerators // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListGenerators; 
            }
            private set
            {
                if (this._ListGenerators != value)
                {
                    this.OnListGeneratorsChanging(value);
                    this._ListGenerators = value;
                    this.OnListGeneratorsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGenerator> _ListGenerators;
        partial void OnListGeneratorsChanging(SortedObservableCollection<PluginGenerator> to); // Property.tt Line: 79
        partial void OnListGeneratorsChanged();
        IEnumerable<IPluginGenerator> IPlugin.ListGenerators { get { return this._ListGenerators; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IPlugin.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IPlugin.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IPlugin.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IPlugin.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IPlugin.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorValidator : ValidatorBase<PluginGenerator, PluginGeneratorValidator> { } // Class.tt Line: 6
    public partial class PluginGenerator : ConfigObjectVmBase<PluginGenerator, PluginGeneratorValidator>, IComparable<PluginGenerator>, IConfigAcceptVisitor, IPluginGenerator // Class.tt Line: 7
    {
        #region CTOR
        public PluginGenerator() : this((ITreeConfigNode)null)
        {
        }
        public PluginGenerator(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorValidator.Validator) // Class.tt Line: 15
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
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGenerator Clone(ITreeConfigNode parent, PluginGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGenerator vm = new PluginGenerator(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGenerator to, PluginGenerator from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IPluginGenerator.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IPluginGenerator.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IPluginGenerator.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IPluginGenerator.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IPluginGenerator.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IPluginGenerator.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { } // Class.tt Line: 6
    public partial class SettingsConfig : VmValidatableWithSeverity<SettingsConfig, SettingsConfigValidator>, ISettingsConfig // Class.tt Line: 7
    {
        #region CTOR
        public SettingsConfig() 
            : base(SettingsConfigValidator.Validator) // Class.tt Line: 43
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
        public static SettingsConfig Clone(SettingsConfig from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            SettingsConfig vm = new SettingsConfig();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 65
            vm.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(SettingsConfig to, SettingsConfig from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.VersionMigrationCurrent = m.VersionMigrationCurrent; // Clone.tt Line: 221
            vm.VersionMigrationSupportFromMin = m.VersionMigrationSupportFromMin; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'SettingsConfig' to 'proto_settings_config'
        public static Proto.Config.proto_settings_config ConvertToProto(SettingsConfig vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_settings_config m = new Proto.Config.proto_settings_config(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.VersionMigrationCurrent = vm.VersionMigrationCurrent; // Clone.tt Line: 276
            m.VersionMigrationSupportFromMin = vm.VersionMigrationSupportFromMin; // Clone.tt Line: 276
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public string Guid // Property.tt Line: 138
        { 
            get 
            { 
                return this._Guid; 
            }
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
        partial void OnGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnGuidChanged();
        string ISettingsConfig.Guid { get { return this._Guid; } } 
        
        public string Name // Property.tt Line: 138
        { 
            get 
            { 
                return this._Name; 
            }
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
        partial void OnNameChanging(ref string to); // Property.tt Line: 160
        partial void OnNameChanged();
        string ISettingsConfig.Name { get { return this._Name; } } 
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 138
        { 
            get 
            { 
                return this._NameUi; 
            }
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
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 160
        partial void OnNameUiChanged();
        string ISettingsConfig.NameUi { get { return this._NameUi; } } 
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string ISettingsConfig.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// current migration version, increased by one on each deployment
        ///////////////////////////////////////////////////
        public int VersionMigrationCurrent // Property.tt Line: 138
        { 
            get 
            { 
                return this._VersionMigrationCurrent; 
            }
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
        partial void OnVersionMigrationCurrentChanging(ref int to); // Property.tt Line: 160
        partial void OnVersionMigrationCurrentChanged();
        int ISettingsConfig.VersionMigrationCurrent { get { return this._VersionMigrationCurrent; } } 
        
        
        ///////////////////////////////////////////////////
        /// min version supported by current version for migration
        ///////////////////////////////////////////////////
        public int VersionMigrationSupportFromMin // Property.tt Line: 138
        { 
            get 
            { 
                return this._VersionMigrationSupportFromMin; 
            }
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
        partial void OnVersionMigrationSupportFromMinChanging(ref int to); // Property.tt Line: 160
        partial void OnVersionMigrationSupportFromMinChanged();
        int ISettingsConfig.VersionMigrationSupportFromMin { get { return this._VersionMigrationSupportFromMin; } } 
        #endregion Properties
    }
    public partial class ConfigShortHistoryValidator : ValidatorBase<ConfigShortHistory, ConfigShortHistoryValidator> { } // Class.tt Line: 6
    public partial class ConfigShortHistory : ConfigObjectVmGenSettings<ConfigShortHistory, ConfigShortHistoryValidator>, IComparable<ConfigShortHistory>, IConfigAcceptVisitor, IConfigShortHistory // Class.tt Line: 7
    {
        #region CTOR
        public ConfigShortHistory() : this((ITreeConfigNode)null)
        {
        }
        public ConfigShortHistory(ITreeConfigNode parent) 
            : base(parent, ConfigShortHistoryValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.CurrentConfig = new Config(this); // Class.tt Line: 32
            this.PrevStableConfig = new Config(this); // Class.tt Line: 32
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static ConfigShortHistory Clone(ITreeConfigNode parent, ConfigShortHistory from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            ConfigShortHistory vm = new ConfigShortHistory(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            if (isDeep) // Clone.tt Line: 62
                vm.CurrentConfig = Config.Clone(vm, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.PrevStableConfig = Config.Clone(vm, from.PrevStableConfig, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(ConfigShortHistory to, ConfigShortHistory from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            if (isDeep) // Clone.tt Line: 138
                Config.Update(to.CurrentConfig, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 138
                Config.Update(to.PrevStableConfig, from.PrevStableConfig, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override ConfigShortHistory Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return ConfigShortHistory.Clone(this.Parent, this);
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            if (vm.CurrentConfig == null) // Clone.tt Line: 213
                vm.CurrentConfig = new Config(vm); // Clone.tt Line: 215
            Config.ConvertToVM(m.CurrentConfig, vm.CurrentConfig); // Clone.tt Line: 219
            if (vm.PrevStableConfig == null) // Clone.tt Line: 213
                vm.PrevStableConfig = new Config(vm); // Clone.tt Line: 215
            Config.ConvertToVM(m.PrevStableConfig, vm.PrevStableConfig); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'ConfigShortHistory' to 'proto_config_short_history'
        public static Proto.Config.proto_config_short_history ConvertToProto(ConfigShortHistory vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_config_short_history m = new Proto.Config.proto_config_short_history(); // Clone.tt Line: 239
            m.CurrentConfig = Config.ConvertToProto(vm.CurrentConfig); // Clone.tt Line: 270
            m.PrevStableConfig = Config.ConvertToProto(vm.PrevStableConfig); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        public Config CurrentConfig // Property.tt Line: 113
        { 
            get 
            { 
                return this._CurrentConfig; 
            }
            set
            {
                if (this._CurrentConfig != value)
                {
                    this.OnCurrentConfigChanging(ref value);
                    this._CurrentConfig = value;
                    this.OnCurrentConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _CurrentConfig;
        partial void OnCurrentConfigChanging(ref Config to); // Property.tt Line: 134
        partial void OnCurrentConfigChanged();
        IConfig IConfigShortHistory.CurrentConfig { get { return this._CurrentConfig; } }
        
        public Config PrevStableConfig // Property.tt Line: 113
        { 
            get 
            { 
                return this._PrevStableConfig; 
            }
            set
            {
                if (this._PrevStableConfig != value)
                {
                    this.OnPrevStableConfigChanging(ref value);
                    this._PrevStableConfig = value;
                    this.OnPrevStableConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _PrevStableConfig;
        partial void OnPrevStableConfigChanging(ref Config to); // Property.tt Line: 134
        partial void OnPrevStableConfigChanged();
        IConfig IConfigShortHistory.PrevStableConfig { get { return this._PrevStableConfig; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IConfigShortHistory.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IConfigShortHistory.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IConfigShortHistory.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IConfigShortHistory.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IConfigShortHistory.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListBaseConfigLinksValidator : ValidatorBase<GroupListBaseConfigLinks, GroupListBaseConfigLinksValidator> { } // Class.tt Line: 6
    public partial class GroupListBaseConfigLinks : ConfigObjectVmGenSettings<GroupListBaseConfigLinks, GroupListBaseConfigLinksValidator>, IComparable<GroupListBaseConfigLinks>, IConfigAcceptVisitor, IGroupListBaseConfigLinks // Class.tt Line: 7
    {
        #region CTOR
        public GroupListBaseConfigLinks() : this((ITreeConfigNode)null)
        {
        }
        public GroupListBaseConfigLinks(ITreeConfigNode parent) 
            : base(parent, GroupListBaseConfigLinksValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListBaseConfigLinks Clone(ITreeConfigNode parent, GroupListBaseConfigLinks from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListBaseConfigLinks vm = new GroupListBaseConfigLinks(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListBaseConfigLinks) // Clone.tt Line: 52
                vm.ListBaseConfigLinks.Add(BaseConfigLink.Clone(vm, (BaseConfigLink)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListBaseConfigLinks to, GroupListBaseConfigLinks from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListBaseConfigLinks.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<BaseConfigLink> ListBaseConfigLinks // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListBaseConfigLinks; 
            }
            private set
            {
                if (this._ListBaseConfigLinks != value)
                {
                    this.OnListBaseConfigLinksChanging(value);
                    this._ListBaseConfigLinks = value;
                    this.OnListBaseConfigLinksChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<BaseConfigLink> _ListBaseConfigLinks;
        partial void OnListBaseConfigLinksChanging(SortedObservableCollection<BaseConfigLink> to); // Property.tt Line: 79
        partial void OnListBaseConfigLinksChanged();
        IEnumerable<IBaseConfigLink> IGroupListBaseConfigLinks.ListBaseConfigLinks { get { return this._ListBaseConfigLinks; } }
        public BaseConfigLink this[int index] { get { return (BaseConfigLink)this.ListBaseConfigLinks[index]; } }
        public void Add(BaseConfigLink item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListBaseConfigLinks.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<BaseConfigLink> items) 
        { 
            Contract.Requires(items != null);
            this.ListBaseConfigLinks.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListBaseConfigLinks.Count; 
        }
        public void Remove(BaseConfigLink item) 
        {
            Contract.Requires(item != null);
            this.ListBaseConfigLinks.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListBaseConfigLinks.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListBaseConfigLinks.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListBaseConfigLinks.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListBaseConfigLinks.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListBaseConfigLinks.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListBaseConfigLinks.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class BaseConfigLinkValidator : ValidatorBase<BaseConfigLink, BaseConfigLinkValidator> { } // Class.tt Line: 6
    public partial class BaseConfigLink : ConfigObjectVmGenSettings<BaseConfigLink, BaseConfigLinkValidator>, IComparable<BaseConfigLink>, IConfigAcceptVisitor, IBaseConfigLink // Class.tt Line: 7
    {
        #region CTOR
        public BaseConfigLink() : this((ITreeConfigNode)null)
        {
        }
        public BaseConfigLink(ITreeConfigNode parent) 
            : base(parent, BaseConfigLinkValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.Config = new Config(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static BaseConfigLink Clone(ITreeConfigNode parent, BaseConfigLink from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            BaseConfigLink vm = new BaseConfigLink(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.Config = Config.Clone(vm, from.Config, isDeep);
            vm.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(BaseConfigLink to, BaseConfigLink from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                Config.Update(to.Config, from.Config, isDeep);
            to.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.Config == null) // Clone.tt Line: 213
                vm.Config = new Config(vm); // Clone.tt Line: 215
            Config.ConvertToVM(m.Config, vm.Config); // Clone.tt Line: 219
            vm.RelativeConfigFilePath = m.RelativeConfigFilePath; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.Config = Config.ConvertToProto(vm.Config); // Clone.tt Line: 270
            m.RelativeConfigFilePath = vm.RelativeConfigFilePath; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
            this.Config.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IBaseConfigLink.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public Config Config // Property.tt Line: 113
        { 
            get 
            { 
                return this._Config; 
            }
            set
            {
                if (this._Config != value)
                {
                    this.OnConfigChanging(ref value);
                    this._Config = value;
                    this.OnConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _Config;
        partial void OnConfigChanging(ref Config to); // Property.tt Line: 134
        partial void OnConfigChanged();
        IConfig IBaseConfigLink.Config { get { return this._Config; } }
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorFilePicker), typeof(ITypeEditor))]
        public string RelativeConfigFilePath // Property.tt Line: 138
        { 
            get 
            { 
                return this._RelativeConfigFilePath; 
            }
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
        partial void OnRelativeConfigFilePathChanging(ref string to); // Property.tt Line: 160
        partial void OnRelativeConfigFilePathChanged();
        string IBaseConfigLink.RelativeConfigFilePath { get { return this._RelativeConfigFilePath; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IBaseConfigLink.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IBaseConfigLink.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IBaseConfigLink.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IBaseConfigLink.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IBaseConfigLink.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IBaseConfigLink.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    public partial class Config : ConfigObjectVmGenSettings<Config, ConfigValidator>, IComparable<Config>, IConfigAcceptVisitor, IConfig // Class.tt Line: 7
    {
        #region CTOR
        public Config() : this((ITreeConfigNode)null)
        {
        }
        public Config(ITreeConfigNode parent) 
            : base(parent, ConfigValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupConfigLinks = new GroupListBaseConfigLinks(this); // Class.tt Line: 32
            this.Model = new ConfigModel(this); // Class.tt Line: 32
            this.GroupPlugins = new GroupListPlugins(this); // Class.tt Line: 32
            this.GroupAppSolutions = new GroupListAppSolutions(this); // Class.tt Line: 32
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static Config Clone(ITreeConfigNode parent, Config from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Config vm = new Config(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Version = from.Version; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.LastUpdated = from.LastUpdated; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupConfigLinks = GroupListBaseConfigLinks.Clone(vm, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.Model = ConfigModel.Clone(vm, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPlugins = GroupListPlugins.Clone(vm, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupAppSolutions = GroupListAppSolutions.Clone(vm, from.GroupAppSolutions, isDeep);
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Config to, Config from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Version = from.Version; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.LastUpdated = from.LastUpdated; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                GroupListBaseConfigLinks.Update(to.GroupConfigLinks, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 138
                ConfigModel.Update(to.Model, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListPlugins.Update(to.GroupPlugins, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListAppSolutions.Update(to.GroupAppSolutions, from.GroupAppSolutions, isDeep);
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Version = m.Version; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.LastUpdated = m.LastUpdated; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            if (vm.GroupConfigLinks == null) // Clone.tt Line: 213
                vm.GroupConfigLinks = new GroupListBaseConfigLinks(vm); // Clone.tt Line: 215
            GroupListBaseConfigLinks.ConvertToVM(m.GroupConfigLinks, vm.GroupConfigLinks); // Clone.tt Line: 219
            if (vm.Model == null) // Clone.tt Line: 213
                vm.Model = new ConfigModel(vm); // Clone.tt Line: 215
            ConfigModel.ConvertToVM(m.Model, vm.Model); // Clone.tt Line: 219
            if (vm.GroupPlugins == null) // Clone.tt Line: 213
                vm.GroupPlugins = new GroupListPlugins(vm); // Clone.tt Line: 215
            GroupListPlugins.ConvertToVM(m.GroupPlugins, vm.GroupPlugins); // Clone.tt Line: 219
            if (vm.GroupAppSolutions == null) // Clone.tt Line: 213
                vm.GroupAppSolutions = new GroupListAppSolutions(vm); // Clone.tt Line: 215
            GroupListAppSolutions.ConvertToVM(m.GroupAppSolutions, vm.GroupAppSolutions); // Clone.tt Line: 219
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'Config' to 'proto_config'
        public static Proto.Config.proto_config ConvertToProto(Config vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_config m = new Proto.Config.proto_config(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Version = vm.Version; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.LastUpdated = vm.LastUpdated; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
            m.GroupConfigLinks = GroupListBaseConfigLinks.ConvertToProto(vm.GroupConfigLinks); // Clone.tt Line: 270
            m.Model = ConfigModel.ConvertToProto(vm.Model); // Clone.tt Line: 270
            m.GroupPlugins = GroupListPlugins.ConvertToProto(vm.GroupPlugins); // Clone.tt Line: 270
            m.GroupAppSolutions = GroupListAppSolutions.ConvertToProto(vm.GroupAppSolutions); // Clone.tt Line: 270
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
        
        [PropertyOrderAttribute(4)]
        [ReadOnly(true)]
        public int Version // Property.tt Line: 138
        { 
            get 
            { 
                return this._Version; 
            }
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
        partial void OnVersionChanging(ref int to); // Property.tt Line: 160
        partial void OnVersionChanged();
        int IConfig.Version { get { return this._Version; } } 
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IConfig.Description { get { return this._Description; } } 
        
        [PropertyOrderAttribute(6)]
        public Google.Protobuf.WellKnownTypes.Timestamp LastUpdated // Property.tt Line: 138
        { 
            get 
            { 
                return this._LastUpdated; 
            }
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
        partial void OnLastUpdatedChanging(ref Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 160
        partial void OnLastUpdatedChanged();
        Google.Protobuf.WellKnownTypes.Timestamp IConfig.LastUpdated { get { return this._LastUpdated; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IConfig.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IConfig.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IConfig.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IConfig.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IConfig.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public GroupListBaseConfigLinks GroupConfigLinks // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupConfigLinks; 
            }
            set
            {
                if (this._GroupConfigLinks != value)
                {
                    this.OnGroupConfigLinksChanging(ref value);
                    this._GroupConfigLinks = value;
                    this.OnGroupConfigLinksChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListBaseConfigLinks _GroupConfigLinks;
        partial void OnGroupConfigLinksChanging(ref GroupListBaseConfigLinks to); // Property.tt Line: 134
        partial void OnGroupConfigLinksChanged();
        IGroupListBaseConfigLinks IConfig.GroupConfigLinks { get { return this._GroupConfigLinks; } }
        
        [BrowsableAttribute(false)]
        public ConfigModel Model // Property.tt Line: 113
        { 
            get 
            { 
                return this._Model; 
            }
            set
            {
                if (this._Model != value)
                {
                    this.OnModelChanging(ref value);
                    this._Model = value;
                    this.OnModelChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigModel _Model;
        partial void OnModelChanging(ref ConfigModel to); // Property.tt Line: 134
        partial void OnModelChanged();
        IConfigModel IConfig.Model { get { return this._Model; } }
        
        [BrowsableAttribute(false)]
        public GroupListPlugins GroupPlugins // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupPlugins; 
            }
            set
            {
                if (this._GroupPlugins != value)
                {
                    this.OnGroupPluginsChanging(ref value);
                    this._GroupPlugins = value;
                    this.OnGroupPluginsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPlugins _GroupPlugins;
        partial void OnGroupPluginsChanging(ref GroupListPlugins to); // Property.tt Line: 134
        partial void OnGroupPluginsChanged();
        IGroupListPlugins IConfig.GroupPlugins { get { return this._GroupPlugins; } }
        
        [BrowsableAttribute(false)]
        public GroupListAppSolutions GroupAppSolutions // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupAppSolutions; 
            }
            set
            {
                if (this._GroupAppSolutions != value)
                {
                    this.OnGroupAppSolutionsChanging(ref value);
                    this._GroupAppSolutions = value;
                    this.OnGroupAppSolutionsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListAppSolutions _GroupAppSolutions;
        partial void OnGroupAppSolutionsChanging(ref GroupListAppSolutions to); // Property.tt Line: 134
        partial void OnGroupAppSolutionsChanged();
        IGroupListAppSolutions IConfig.GroupAppSolutions { get { return this._GroupAppSolutions; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class AppDbSettingsValidator : ValidatorBase<AppDbSettings, AppDbSettingsValidator> { } // Class.tt Line: 6
    public partial class AppDbSettings : VmValidatableWithSeverity<AppDbSettings, AppDbSettingsValidator>, IAppDbSettings // Class.tt Line: 7
    {
        #region CTOR
        public AppDbSettings() 
            : base(AppDbSettingsValidator.Validator) // Class.tt Line: 43
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
        public static AppDbSettings Clone(AppDbSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppDbSettings vm = new AppDbSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 65
            vm.PluginName = from.PluginName; // Clone.tt Line: 65
            vm.Version = from.Version; // Clone.tt Line: 65
            vm.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 65
            vm.PluginGenName = from.PluginGenName; // Clone.tt Line: 65
            vm.ConnGuid = from.ConnGuid; // Clone.tt Line: 65
            vm.ConnName = from.ConnName; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppDbSettings to, AppDbSettings from, bool isDeep = true) // Clone.tt Line: 77
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 221
            vm.PluginName = m.PluginName; // Clone.tt Line: 221
            vm.Version = m.Version; // Clone.tt Line: 221
            vm.PluginGenGuid = m.PluginGenGuid; // Clone.tt Line: 221
            vm.PluginGenName = m.PluginGenName; // Clone.tt Line: 221
            vm.ConnGuid = m.ConnGuid; // Clone.tt Line: 221
            vm.ConnName = m.ConnName; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
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
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(1)]
        [Editor(typeof(EditorDbPluginSelection), typeof(EditorDbPluginSelection))]
        [Description("Default DB Plugin")]
        public string PluginGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._PluginGuid; 
            }
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
        partial void OnPluginGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnPluginGuidChanged();
        string IAppDbSettings.PluginGuid { get { return this._PluginGuid; } } 
        
        [PropertyOrderAttribute(2)]
        [ReadOnly(true)]
        public string PluginName // Property.tt Line: 138
        { 
            get 
            { 
                return this._PluginName; 
            }
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
        partial void OnPluginNameChanging(ref string to); // Property.tt Line: 160
        partial void OnPluginNameChanged();
        string IAppDbSettings.PluginName { get { return this._PluginName; } } 
        
        [PropertyOrderAttribute(3)]
        [ReadOnly(true)]
        public string Version // Property.tt Line: 138
        { 
            get 
            { 
                return this._Version; 
            }
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
        partial void OnVersionChanging(ref string to); // Property.tt Line: 160
        partial void OnVersionChanged();
        string IAppDbSettings.Version { get { return this._Version; } } 
        
        [PropertyOrderAttribute(4)]
        [Editor(typeof(EditorDbPluginGenSelection), typeof(EditorDbPluginGenSelection))]
        [Description("Default DB Plugin generator")]
        public string PluginGenGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._PluginGenGuid; 
            }
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
        partial void OnPluginGenGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnPluginGenGuidChanged();
        string IAppDbSettings.PluginGenGuid { get { return this._PluginGenGuid; } } 
        
        [PropertyOrderAttribute(5)]
        [ReadOnly(true)]
        public string PluginGenName // Property.tt Line: 138
        { 
            get 
            { 
                return this._PluginGenName; 
            }
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
        partial void OnPluginGenNameChanging(ref string to); // Property.tt Line: 160
        partial void OnPluginGenNameChanged();
        string IAppDbSettings.PluginGenName { get { return this._PluginGenName; } } 
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorDbConnSelection), typeof(EditorDbConnSelection))]
        [Description("Default DB connection string")]
        public string ConnGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._ConnGuid; 
            }
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
        partial void OnConnGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnConnGuidChanged();
        string IAppDbSettings.ConnGuid { get { return this._ConnGuid; } } 
        
        [PropertyOrderAttribute(7)]
        [ReadOnly(true)]
        public string ConnName // Property.tt Line: 138
        { 
            get 
            { 
                return this._ConnName; 
            }
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
        partial void OnConnNameChanging(ref string to); // Property.tt Line: 160
        partial void OnConnNameChanged();
        string IAppDbSettings.ConnName { get { return this._ConnName; } } 
        #endregion Properties
    }
    public partial class PluginGroupGeneratorsDefaultSettingsValidator : ValidatorBase<PluginGroupGeneratorsDefaultSettings, PluginGroupGeneratorsDefaultSettingsValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Stored in App node. All nullable setting has to have value
    ///////////////////////////////////////////////////
    public partial class PluginGroupGeneratorsDefaultSettings : ConfigObjectVmGenSettings<PluginGroupGeneratorsDefaultSettings, PluginGroupGeneratorsDefaultSettingsValidator>, IComparable<PluginGroupGeneratorsDefaultSettings>, IConfigAcceptVisitor, IPluginGroupGeneratorsDefaultSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGroupGeneratorsDefaultSettings() : this((ITreeConfigNode)null)
        {
        }
        public PluginGroupGeneratorsDefaultSettings(ITreeConfigNode parent) 
            : base(parent, PluginGroupGeneratorsDefaultSettingsValidator.Validator) // Class.tt Line: 15
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
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGroupGeneratorsDefaultSettings Clone(ITreeConfigNode parent, PluginGroupGeneratorsDefaultSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGroupGeneratorsDefaultSettings vm = new PluginGroupGeneratorsDefaultSettings(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGroupGeneratorsDefaultSettings to, PluginGroupGeneratorsDefaultSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGroupGeneratorsDefaultSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGroupGeneratorsDefaultSettings.Clone(this.Parent, this);
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppGroupGeneratorsGuid = m.AppGroupGeneratorsGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGroupGeneratorsDefaultSettings' to 'proto_plugin_group_generators_default_settings'
        public static Proto.Config.proto_plugin_group_generators_default_settings ConvertToProto(PluginGroupGeneratorsDefaultSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_group_generators_default_settings m = new Proto.Config.proto_plugin_group_generators_default_settings(); // Clone.tt Line: 239
            m.AppGroupGeneratorsGuid = vm.AppGroupGeneratorsGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        /// Guid of group generators
        ///////////////////////////////////////////////////
        public string AppGroupGeneratorsGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._AppGroupGeneratorsGuid; 
            }
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
        partial void OnAppGroupGeneratorsGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnAppGroupGeneratorsGuidChanged();
        string IPluginGroupGeneratorsDefaultSettings.AppGroupGeneratorsGuid { get { return this._AppGroupGeneratorsGuid; } } 
        
        public string Settings // Property.tt Line: 138
        { 
            get 
            { 
                return this._Settings; 
            }
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
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 160
        partial void OnSettingsChanged();
        string IPluginGroupGeneratorsDefaultSettings.Settings { get { return this._Settings; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IPluginGroupGeneratorsDefaultSettings.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IPluginGroupGeneratorsDefaultSettings.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IPluginGroupGeneratorsDefaultSettings.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IPluginGroupGeneratorsDefaultSettings.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IPluginGroupGeneratorsDefaultSettings.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListAppSolutionsValidator : ValidatorBase<GroupListAppSolutions, GroupListAppSolutionsValidator> { } // Class.tt Line: 6
    public partial class GroupListAppSolutions : ConfigObjectVmBase<GroupListAppSolutions, GroupListAppSolutionsValidator>, IComparable<GroupListAppSolutions>, IConfigAcceptVisitor, IGroupListAppSolutions // Class.tt Line: 7
    {
        #region CTOR
        public GroupListAppSolutions() : this((ITreeConfigNode)null)
        {
        }
        public GroupListAppSolutions(ITreeConfigNode parent) 
            : base(parent, GroupListAppSolutionsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListAppSolutions = new ConfigNodesCollection<AppSolution>(this); // Class.tt Line: 26
            this.ListGroupGeneratorsDefultSettings = new ConfigNodesCollection<PluginGroupGeneratorsDefaultSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
            if (type == typeof(PluginGroupGeneratorsDefaultSettings)) // Clone.tt Line: 15
            {
                this.ListGroupGeneratorsDefultSettings.Sort();
            }
        }
        public static GroupListAppSolutions Clone(ITreeConfigNode parent, GroupListAppSolutions from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListAppSolutions vm = new GroupListAppSolutions(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListAppSolutions) // Clone.tt Line: 52
                vm.ListAppSolutions.Add(AppSolution.Clone(vm, (AppSolution)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListGroupGeneratorsDefultSettings = new ConfigNodesCollection<PluginGroupGeneratorsDefaultSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListGroupGeneratorsDefultSettings) // Clone.tt Line: 52
                vm.ListGroupGeneratorsDefultSettings.Add(PluginGroupGeneratorsDefaultSettings.Clone(vm, (PluginGroupGeneratorsDefaultSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListAppSolutions to, GroupListAppSolutions from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListGroupGeneratorsDefultSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGroupGeneratorsDefultSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGroupGeneratorsDefaultSettings(to); // Clone.tt Line: 117
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListGroupGeneratorsDefultSettings = new ConfigNodesCollection<PluginGroupGeneratorsDefaultSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListGroupGeneratorsDefultSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGroupGeneratorsDefaultSettings.ConvertToVM(t, new PluginGroupGeneratorsDefaultSettings(vm)); // Clone.tt Line: 204
                vm.ListGroupGeneratorsDefultSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(2)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListAppSolutions.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// List NET solutions
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppSolution> ListAppSolutions // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListAppSolutions; 
            }
            private set
            {
                if (this._ListAppSolutions != value)
                {
                    this.OnListAppSolutionsChanging(value);
                    this._ListAppSolutions = value;
                    this.OnListAppSolutionsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppSolution> _ListAppSolutions;
        partial void OnListAppSolutionsChanging(SortedObservableCollection<AppSolution> to); // Property.tt Line: 79
        partial void OnListAppSolutionsChanged();
        IEnumerable<IAppSolution> IGroupListAppSolutions.ListAppSolutions { get { return this._ListAppSolutions; } }
        public AppSolution this[int index] { get { return (AppSolution)this.ListAppSolutions[index]; } }
        public void Add(AppSolution item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListAppSolutions.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<AppSolution> items) 
        { 
            Contract.Requires(items != null);
            this.ListAppSolutions.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListAppSolutions.Count; 
        }
        public void Remove(AppSolution item) 
        {
            Contract.Requires(item != null);
            this.ListAppSolutions.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListAppSolutions.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListAppSolutions.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListAppSolutions.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListAppSolutions.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListAppSolutions.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGroupGeneratorsDefaultSettings> ListGroupGeneratorsDefultSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListGroupGeneratorsDefultSettings; 
            }
            private set
            {
                if (this._ListGroupGeneratorsDefultSettings != value)
                {
                    this.OnListGroupGeneratorsDefultSettingsChanging(value);
                    this._ListGroupGeneratorsDefultSettings = value;
                    this.OnListGroupGeneratorsDefultSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGroupGeneratorsDefaultSettings> _ListGroupGeneratorsDefultSettings;
        partial void OnListGroupGeneratorsDefultSettingsChanging(SortedObservableCollection<PluginGroupGeneratorsDefaultSettings> to); // Property.tt Line: 79
        partial void OnListGroupGeneratorsDefultSettingsChanged();
        IEnumerable<IPluginGroupGeneratorsDefaultSettings> IGroupListAppSolutions.ListGroupGeneratorsDefultSettings { get { return this._ListGroupGeneratorsDefultSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGroupGeneratorsSettingsValidator : ValidatorBase<PluginGroupGeneratorsSettings, PluginGroupGeneratorsSettingsValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Stored in AppSolution node. All null setting will use parent value
    ///////////////////////////////////////////////////
    public partial class PluginGroupGeneratorsSettings : ConfigObjectVmGenSettings<PluginGroupGeneratorsSettings, PluginGroupGeneratorsSettingsValidator>, IComparable<PluginGroupGeneratorsSettings>, IConfigAcceptVisitor, IPluginGroupGeneratorsSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGroupGeneratorsSettings() : this((ITreeConfigNode)null)
        {
        }
        public PluginGroupGeneratorsSettings(ITreeConfigNode parent) 
            : base(parent, PluginGroupGeneratorsSettingsValidator.Validator) // Class.tt Line: 15
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
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGroupGeneratorsSettings Clone(ITreeConfigNode parent, PluginGroupGeneratorsSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGroupGeneratorsSettings vm = new PluginGroupGeneratorsSettings(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGroupGeneratorsSettings to, PluginGroupGeneratorsSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.AppGroupGeneratorsGuid = from.AppGroupGeneratorsGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppGroupGeneratorsGuid = m.AppGroupGeneratorsGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGroupGeneratorsSettings' to 'proto_plugin_group_generators_settings'
        public static Proto.Config.proto_plugin_group_generators_settings ConvertToProto(PluginGroupGeneratorsSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_group_generators_settings m = new Proto.Config.proto_plugin_group_generators_settings(); // Clone.tt Line: 239
            m.AppGroupGeneratorsGuid = vm.AppGroupGeneratorsGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        /// Guid of group generators
        ///////////////////////////////////////////////////
        public string AppGroupGeneratorsGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._AppGroupGeneratorsGuid; 
            }
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
        partial void OnAppGroupGeneratorsGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnAppGroupGeneratorsGuidChanged();
        string IPluginGroupGeneratorsSettings.AppGroupGeneratorsGuid { get { return this._AppGroupGeneratorsGuid; } } 
        
        public string Settings // Property.tt Line: 138
        { 
            get 
            { 
                return this._Settings; 
            }
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
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 160
        partial void OnSettingsChanged();
        string IPluginGroupGeneratorsSettings.Settings { get { return this._Settings; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IPluginGroupGeneratorsSettings.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IPluginGroupGeneratorsSettings.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IPluginGroupGeneratorsSettings.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IPluginGroupGeneratorsSettings.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IPluginGroupGeneratorsSettings.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class AppSolutionValidator : ValidatorBase<AppSolution, AppSolutionValidator> { } // Class.tt Line: 6
    public partial class AppSolution : ConfigObjectVmBase<AppSolution, AppSolutionValidator>, IComparable<AppSolution>, IConfigAcceptVisitor, IAppSolution // Class.tt Line: 7
    {
        #region CTOR
        public AppSolution() : this((ITreeConfigNode)null)
        {
        }
        public AppSolution(ITreeConfigNode parent) 
            : base(parent, AppSolutionValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListAppProjects = new ConfigNodesCollection<AppProject>(this); // Class.tt Line: 26
            this.ListGroupGeneratorsSettings = new ConfigNodesCollection<PluginGroupGeneratorsSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static AppSolution Clone(ITreeConfigNode parent, AppSolution from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppSolution vm = new AppSolution(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListAppProjects) // Clone.tt Line: 52
                vm.ListAppProjects.Add(AppProject.Clone(vm, (AppProject)t, isDeep));
            vm.ListGroupGeneratorsSettings = new ConfigNodesCollection<PluginGroupGeneratorsSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListGroupGeneratorsSettings) // Clone.tt Line: 52
                vm.ListGroupGeneratorsSettings.Add(PluginGroupGeneratorsSettings.Clone(vm, (PluginGroupGeneratorsSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppSolution to, AppSolution from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListAppProjects.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListAppProjects)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.RelativeAppSolutionPath = m.RelativeAppSolutionPath; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
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
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.RelativeAppSolutionPath = vm.RelativeAppSolutionPath; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IAppSolution.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// List NET projects
        /// App solution relative path to configuration file path
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(6)]
        [DisplayName("Path")]
        [Editor(typeof(EditorSolutionPicker), typeof(ITypeEditor))]
        [Description(".NET solution file path relative to configuration file path")]
        public string RelativeAppSolutionPath // Property.tt Line: 138
        { 
            get 
            { 
                return this._RelativeAppSolutionPath; 
            }
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
        partial void OnRelativeAppSolutionPathChanging(ref string to); // Property.tt Line: 160
        partial void OnRelativeAppSolutionPathChanged();
        string IAppSolution.RelativeAppSolutionPath { get { return this._RelativeAppSolutionPath; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IAppSolution.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IAppSolution.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IAppSolution.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IAppSolution.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IAppSolution.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProject> ListAppProjects // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListAppProjects; 
            }
            private set
            {
                if (this._ListAppProjects != value)
                {
                    this.OnListAppProjectsChanging(value);
                    this._ListAppProjects = value;
                    this.OnListAppProjectsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppProject> _ListAppProjects;
        partial void OnListAppProjectsChanging(SortedObservableCollection<AppProject> to); // Property.tt Line: 79
        partial void OnListAppProjectsChanged();
        IEnumerable<IAppProject> IAppSolution.ListAppProjects { get { return this._ListAppProjects; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGroupGeneratorsSettings> ListGroupGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListGroupGeneratorsSettings; 
            }
            private set
            {
                if (this._ListGroupGeneratorsSettings != value)
                {
                    this.OnListGroupGeneratorsSettingsChanging(value);
                    this._ListGroupGeneratorsSettings = value;
                    this.OnListGroupGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGroupGeneratorsSettings> _ListGroupGeneratorsSettings;
        partial void OnListGroupGeneratorsSettingsChanging(SortedObservableCollection<PluginGroupGeneratorsSettings> to); // Property.tt Line: 79
        partial void OnListGroupGeneratorsSettingsChanged();
        IEnumerable<IPluginGroupGeneratorsSettings> IAppSolution.ListGroupGeneratorsSettings { get { return this._ListGroupGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class AppProjectValidator : ValidatorBase<AppProject, AppProjectValidator> { } // Class.tt Line: 6
    public partial class AppProject : ConfigObjectVmGenSettings<AppProject, AppProjectValidator>, IComparable<AppProject>, IConfigAcceptVisitor, IAppProject // Class.tt Line: 7
    {
        #region CTOR
        public AppProject() : this((ITreeConfigNode)null)
        {
        }
        public AppProject(ITreeConfigNode parent) 
            : base(parent, AppProjectValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static AppProject Clone(ITreeConfigNode parent, AppProject from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppProject vm = new AppProject(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.Namespace = from.Namespace; // Clone.tt Line: 65
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListAppProjectGenerators) // Clone.tt Line: 52
                vm.ListAppProjectGenerators.Add(AppProjectGenerator.Clone(vm, (AppProjectGenerator)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppProject to, AppProject from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            to.Namespace = from.Namespace; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListAppProjectGenerators.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListAppProjectGenerators)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.RelativeAppProjectPath = m.RelativeAppProjectPath; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.Namespace = m.Namespace; // Clone.tt Line: 221
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListAppProjectGenerators) // Clone.tt Line: 201
            {
                var tvm = AppProjectGenerator.ConvertToVM(t, new AppProjectGenerator(vm)); // Clone.tt Line: 204
                vm.ListAppProjectGenerators.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.RelativeAppProjectPath = vm.RelativeAppProjectPath; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IAppProject.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// App project relative path to .net solution file path
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorProjectPicker), typeof(ITypeEditor))]
        [Description(".NET project file path relative to solution file path")]
        public string RelativeAppProjectPath // Property.tt Line: 138
        { 
            get 
            { 
                return this._RelativeAppProjectPath; 
            }
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
        partial void OnRelativeAppProjectPathChanging(ref string to); // Property.tt Line: 160
        partial void OnRelativeAppProjectPathChanged();
        string IAppProject.RelativeAppProjectPath { get { return this._RelativeAppProjectPath; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IAppProject.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IAppProject.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IAppProject.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IAppProject.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IAppProject.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [PropertyOrderAttribute(9)]
        [DisplayName("Namespace")]
        [Description("Project namespace name")]
        public string Namespace // Property.tt Line: 138
        { 
            get 
            { 
                return this._Namespace; 
            }
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
        partial void OnNamespaceChanging(ref string to); // Property.tt Line: 160
        partial void OnNamespaceChanged();
        string IAppProject.Namespace { get { return this._Namespace; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProjectGenerator> ListAppProjectGenerators // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListAppProjectGenerators; 
            }
            private set
            {
                if (this._ListAppProjectGenerators != value)
                {
                    this.OnListAppProjectGeneratorsChanging(value);
                    this._ListAppProjectGenerators = value;
                    this.OnListAppProjectGeneratorsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppProjectGenerator> _ListAppProjectGenerators;
        partial void OnListAppProjectGeneratorsChanging(SortedObservableCollection<AppProjectGenerator> to); // Property.tt Line: 79
        partial void OnListAppProjectGeneratorsChanged();
        IEnumerable<IAppProjectGenerator> IAppProject.ListAppProjectGenerators { get { return this._ListAppProjectGenerators; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorNodeSettingsValidator : ValidatorBase<PluginGeneratorNodeSettings, PluginGeneratorNodeSettingsValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Stored in each node in ConfigModel branch
    ///////////////////////////////////////////////////
    public partial class PluginGeneratorNodeSettings : ConfigObjectVmGenSettings<PluginGeneratorNodeSettings, PluginGeneratorNodeSettingsValidator>, IComparable<PluginGeneratorNodeSettings>, IConfigAcceptVisitor, IPluginGeneratorNodeSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGeneratorNodeSettings() : this((ITreeConfigNode)null)
        {
        }
        public PluginGeneratorNodeSettings(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorNodeSettingsValidator.Validator) // Class.tt Line: 15
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
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGeneratorNodeSettings Clone(ITreeConfigNode parent, PluginGeneratorNodeSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGeneratorNodeSettings vm = new PluginGeneratorNodeSettings(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 65
            vm.NodeSettingsVmGuid = from.NodeSettingsVmGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGeneratorNodeSettings to, PluginGeneratorNodeSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 141
            to.NodeSettingsVmGuid = from.NodeSettingsVmGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppProjectGeneratorGuid = m.AppProjectGeneratorGuid; // Clone.tt Line: 221
            vm.NodeSettingsVmGuid = m.NodeSettingsVmGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGeneratorNodeSettings' to 'proto_plugin_generator_node_settings'
        public static Proto.Config.proto_plugin_generator_node_settings ConvertToProto(PluginGeneratorNodeSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_generator_node_settings m = new Proto.Config.proto_plugin_generator_node_settings(); // Clone.tt Line: 239
            m.AppProjectGeneratorGuid = vm.AppProjectGeneratorGuid; // Clone.tt Line: 276
            m.NodeSettingsVmGuid = vm.NodeSettingsVmGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        public string AppProjectGeneratorGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._AppProjectGeneratorGuid; 
            }
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
        partial void OnAppProjectGeneratorGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnAppProjectGeneratorGuidChanged();
        string IPluginGeneratorNodeSettings.AppProjectGeneratorGuid { get { return this._AppProjectGeneratorGuid; } } 
        
        
        ///////////////////////////////////////////////////
        /// Name of solution-project-generator node
        /// string name = 2;
        ///////////////////////////////////////////////////
        public string NodeSettingsVmGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._NodeSettingsVmGuid; 
            }
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
        partial void OnNodeSettingsVmGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnNodeSettingsVmGuidChanged();
        string IPluginGeneratorNodeSettings.NodeSettingsVmGuid { get { return this._NodeSettingsVmGuid; } } 
        
        public string Settings // Property.tt Line: 138
        { 
            get 
            { 
                return this._Settings; 
            }
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
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 160
        partial void OnSettingsChanged();
        string IPluginGeneratorNodeSettings.Settings { get { return this._Settings; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IPluginGeneratorNodeSettings.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorMainSettingsValidator : ValidatorBase<PluginGeneratorMainSettings, PluginGeneratorMainSettingsValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Stored in AppProjectGenerator node
    ///////////////////////////////////////////////////
    public partial class PluginGeneratorMainSettings : ConfigObjectVmGenSettings<PluginGeneratorMainSettings, PluginGeneratorMainSettingsValidator>, IComparable<PluginGeneratorMainSettings>, IConfigAcceptVisitor, IPluginGeneratorMainSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGeneratorMainSettings() : this((ITreeConfigNode)null)
        {
        }
        public PluginGeneratorMainSettings(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorMainSettingsValidator.Validator) // Class.tt Line: 15
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
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGeneratorMainSettings Clone(ITreeConfigNode parent, PluginGeneratorMainSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGeneratorMainSettings vm = new PluginGeneratorMainSettings(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGeneratorMainSettings to, PluginGeneratorMainSettings from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 141
            to.Settings = from.Settings; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
        }
        // Clone.tt Line: 147
        #region IEditable
        public override PluginGeneratorMainSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGeneratorMainSettings.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(PluginGeneratorMainSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            PluginGeneratorMainSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_plugin_generator_main_settings' to 'PluginGeneratorMainSettings'
        public static PluginGeneratorMainSettings ConvertToVM(Proto.Config.proto_plugin_generator_main_settings m, PluginGeneratorMainSettings vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.AppProjectGeneratorGuid = m.AppProjectGeneratorGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'PluginGeneratorMainSettings' to 'proto_plugin_generator_main_settings'
        public static Proto.Config.proto_plugin_generator_main_settings ConvertToProto(PluginGeneratorMainSettings vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_plugin_generator_main_settings m = new Proto.Config.proto_plugin_generator_main_settings(); // Clone.tt Line: 239
            m.AppProjectGeneratorGuid = vm.AppProjectGeneratorGuid; // Clone.tt Line: 276
            m.Settings = vm.Settings; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        public string AppProjectGeneratorGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._AppProjectGeneratorGuid; 
            }
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
        partial void OnAppProjectGeneratorGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnAppProjectGeneratorGuidChanged();
        string IPluginGeneratorMainSettings.AppProjectGeneratorGuid { get { return this._AppProjectGeneratorGuid; } } 
        
        public string Settings // Property.tt Line: 138
        { 
            get 
            { 
                return this._Settings; 
            }
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
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 160
        partial void OnSettingsChanged();
        string IPluginGeneratorMainSettings.Settings { get { return this._Settings; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IPluginGeneratorMainSettings.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class AppProjectGeneratorValidator : ValidatorBase<AppProjectGenerator, AppProjectGeneratorValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Application project generator
    ///////////////////////////////////////////////////
    public partial class AppProjectGenerator : ConfigObjectVmGenSettings<AppProjectGenerator, AppProjectGeneratorValidator>, IComparable<AppProjectGenerator>, IConfigAcceptVisitor, IAppProjectGenerator // Class.tt Line: 7
    {
        #region CTOR
        public AppProjectGenerator() : this((ITreeConfigNode)null)
        {
        }
        public AppProjectGenerator(ITreeConfigNode parent) 
            : base(parent, AppProjectGeneratorValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.MainSettings = new PluginGeneratorMainSettings(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static AppProjectGenerator Clone(ITreeConfigNode parent, AppProjectGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            AppProjectGenerator vm = new AppProjectGenerator(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 65
            vm.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 65
            vm.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 65
            vm.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 65
            vm.RelativePathToGenFolder = from.RelativePathToGenFolder; // Clone.tt Line: 65
            vm.GenFileName = from.GenFileName; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.MainSettings = PluginGeneratorMainSettings.Clone(vm, from.MainSettings, isDeep);
            vm.ConnStr = from.ConnStr; // Clone.tt Line: 65
            vm.IsGenerateSqlSqriptToUpdatePrevStable = from.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 65
            vm.ConnStrToPrevStable = from.ConnStrToPrevStable; // Clone.tt Line: 65
            vm.GenScriptFileName = from.GenScriptFileName; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(AppProjectGenerator to, AppProjectGenerator from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.PluginGuid = from.PluginGuid; // Clone.tt Line: 141
            to.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 141
            to.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 141
            to.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 141
            to.RelativePathToGenFolder = from.RelativePathToGenFolder; // Clone.tt Line: 141
            to.GenFileName = from.GenFileName; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            to.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                PluginGeneratorMainSettings.Update(to.MainSettings, from.MainSettings, isDeep);
            to.ConnStr = from.ConnStr; // Clone.tt Line: 141
            to.IsGenerateSqlSqriptToUpdatePrevStable = from.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 141
            to.ConnStrToPrevStable = from.ConnStrToPrevStable; // Clone.tt Line: 141
            to.GenScriptFileName = from.GenScriptFileName; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 221
            vm.DescriptionPlugin = m.DescriptionPlugin; // Clone.tt Line: 221
            vm.PluginGeneratorGuid = m.PluginGeneratorGuid; // Clone.tt Line: 221
            vm.DescriptionGenerator = m.DescriptionGenerator; // Clone.tt Line: 221
            vm.RelativePathToGenFolder = m.RelativePathToGenFolder; // Clone.tt Line: 221
            vm.GenFileName = m.GenFileName; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 221
            if (vm.MainSettings == null) // Clone.tt Line: 213
                vm.MainSettings = new PluginGeneratorMainSettings(vm); // Clone.tt Line: 215
            PluginGeneratorMainSettings.ConvertToVM(m.MainSettings, vm.MainSettings); // Clone.tt Line: 219
            vm.ConnStr = m.ConnStr; // Clone.tt Line: 221
            vm.IsGenerateSqlSqriptToUpdatePrevStable = m.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 221
            vm.ConnStrToPrevStable = m.ConnStrToPrevStable; // Clone.tt Line: 221
            vm.GenScriptFileName = m.GenScriptFileName; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 276
            m.DescriptionPlugin = vm.DescriptionPlugin; // Clone.tt Line: 276
            m.PluginGeneratorGuid = vm.PluginGeneratorGuid; // Clone.tt Line: 276
            m.DescriptionGenerator = vm.DescriptionGenerator; // Clone.tt Line: 276
            m.RelativePathToGenFolder = vm.RelativePathToGenFolder; // Clone.tt Line: 276
            m.GenFileName = vm.GenFileName; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
            m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 276
            m.MainSettings = PluginGeneratorMainSettings.ConvertToProto(vm.MainSettings); // Clone.tt Line: 270
            m.ConnStr = vm.ConnStr; // Clone.tt Line: 276
            m.IsGenerateSqlSqriptToUpdatePrevStable = vm.IsGenerateSqlSqriptToUpdatePrevStable; // Clone.tt Line: 276
            m.ConnStrToPrevStable = vm.ConnStrToPrevStable; // Clone.tt Line: 276
            m.GenScriptFileName = vm.GenScriptFileName; // Clone.tt Line: 276
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
            this.MainSettings.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IAppProjectGenerator.Description { get { return this._Description; } } 
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Plugin")]
        [Description("Plugins with generators")]
        [Editor(typeof(EditorPluginSelection), typeof(ITypeEditor))]
        public string PluginGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._PluginGuid; 
            }
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
        partial void OnPluginGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnPluginGuidChanged();
        string IAppProjectGenerator.PluginGuid { get { return this._PluginGuid; } } 
        
        [PropertyOrderAttribute(5)]
        [DisplayName("Description")]
        [ReadOnly(true)]
        public string DescriptionPlugin // Property.tt Line: 138
        { 
            get 
            { 
                return this._DescriptionPlugin; 
            }
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
        partial void OnDescriptionPluginChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionPluginChanged();
        string IAppProjectGenerator.DescriptionPlugin { get { return this._DescriptionPlugin; } } 
        
        [PropertyOrderAttribute(6)]
        [DisplayName("Generator")]
        [Description("Plugin generator")]
        [Editor(typeof(EditorPluginGeneratorSelection), typeof(ITypeEditor))]
        public string PluginGeneratorGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._PluginGeneratorGuid; 
            }
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
        partial void OnPluginGeneratorGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnPluginGeneratorGuidChanged();
        string IAppProjectGenerator.PluginGeneratorGuid { get { return this._PluginGeneratorGuid; } } 
        
        [PropertyOrderAttribute(7)]
        [DisplayName("Description")]
        [ReadOnly(true)]
        public string DescriptionGenerator // Property.tt Line: 138
        { 
            get 
            { 
                return this._DescriptionGenerator; 
            }
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
        partial void OnDescriptionGeneratorChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionGeneratorChanged();
        string IAppProjectGenerator.DescriptionGenerator { get { return this._DescriptionGenerator; } } 
        
        
        ///////////////////////////////////////////////////
        /// Relative folder path to project file
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(8)]
        [DisplayName("Output Folder")]
        [Editor(typeof(EditorFolderPicker), typeof(ITypeEditor))]
        [Description("Get is returning relative folder path to project file")]
        public string RelativePathToGenFolder // Property.tt Line: 138
        { 
            get 
            { 
                return this._RelativePathToGenFolder; 
            }
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
        partial void OnRelativePathToGenFolderChanging(ref string to); // Property.tt Line: 160
        partial void OnRelativePathToGenFolderChanged();
        string IAppProjectGenerator.RelativePathToGenFolder { get { return this._RelativePathToGenFolder; } } 
        
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        [DisplayName("Output File")]
        [PropertyOrderAttribute(9)]
        [Description("Generator output file name")]
        public string GenFileName // Property.tt Line: 138
        { 
            get 
            { 
                return this._GenFileName; 
            }
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
        partial void OnGenFileNameChanging(ref string to); // Property.tt Line: 160
        partial void OnGenFileNameChanged();
        string IAppProjectGenerator.GenFileName { get { return this._GenFileName; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IAppProjectGenerator.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IAppProjectGenerator.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IAppProjectGenerator.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IAppProjectGenerator.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IAppProjectGenerator.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public string GeneratorSettings // Property.tt Line: 138
        { 
            get 
            { 
                return this._GeneratorSettings; 
            }
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
        partial void OnGeneratorSettingsChanging(ref string to); // Property.tt Line: 160
        partial void OnGeneratorSettingsChanged();
        string IAppProjectGenerator.GeneratorSettings { get { return this._GeneratorSettings; } } 
        
        [PropertyOrderAttribute(29)]
        [BrowsableAttribute(false)]
        public PluginGeneratorMainSettings MainSettings // Property.tt Line: 113
        { 
            get 
            { 
                return this._MainSettings; 
            }
            set
            {
                if (this._MainSettings != value)
                {
                    this.OnMainSettingsChanging(ref value);
                    this._MainSettings = value;
                    this.OnMainSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private PluginGeneratorMainSettings _MainSettings;
        partial void OnMainSettingsChanging(ref PluginGeneratorMainSettings to); // Property.tt Line: 134
        partial void OnMainSettingsChanged();
        IPluginGeneratorMainSettings IAppProjectGenerator.MainSettings { get { return this._MainSettings; } }
        
        [PropertyOrderAttribute(9)]
        [Description("Db connection string. Directly editable or generated based on settings")]
        public string ConnStr // Property.tt Line: 138
        { 
            get 
            { 
                return this._ConnStr; 
            }
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
        partial void OnConnStrChanging(ref string to); // Property.tt Line: 160
        partial void OnConnStrChanged();
        string IAppProjectGenerator.ConnStr { get { return this._ConnStr; } } 
        
        [PropertyOrderAttribute(10)]
        [DisplayName("Sql script")]
        [Description("Generate Sql script to update previous stable version")]
        public bool IsGenerateSqlSqriptToUpdatePrevStable // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsGenerateSqlSqriptToUpdatePrevStable; 
            }
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
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanged();
        bool IAppProjectGenerator.IsGenerateSqlSqriptToUpdatePrevStable { get { return this._IsGenerateSqlSqriptToUpdatePrevStable; } } 
        
        [PropertyOrderAttribute(11)]
        [DisplayName("Stable DB")]
        [Description("Db connection string to previous stable version")]
        public string ConnStrToPrevStable // Property.tt Line: 138
        { 
            get 
            { 
                return this._ConnStrToPrevStable; 
            }
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
        partial void OnConnStrToPrevStableChanging(ref string to); // Property.tt Line: 160
        partial void OnConnStrToPrevStableChanged();
        string IAppProjectGenerator.ConnStrToPrevStable { get { return this._ConnStrToPrevStable; } } 
        
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        [DisplayName("SQL script")]
        [PropertyOrderAttribute(12)]
        [Description("SQL script output file name")]
        public string GenScriptFileName // Property.tt Line: 138
        { 
            get 
            { 
                return this._GenScriptFileName; 
            }
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
        partial void OnGenScriptFileNameChanging(ref string to); // Property.tt Line: 160
        partial void OnGenScriptFileNameChanged();
        string IAppProjectGenerator.GenScriptFileName { get { return this._GenScriptFileName; } } 
        
        [PropertyOrderAttribute(30)]
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IAppProjectGenerator.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PluginGeneratorNodeDefaultSettingsValidator : ValidatorBase<PluginGeneratorNodeDefaultSettings, PluginGeneratorNodeDefaultSettingsValidator> { } // Class.tt Line: 6
    public partial class PluginGeneratorNodeDefaultSettings : VmValidatableWithSeverity<PluginGeneratorNodeDefaultSettings, PluginGeneratorNodeDefaultSettingsValidator>, IPluginGeneratorNodeDefaultSettings // Class.tt Line: 7
    {
        #region CTOR
        public PluginGeneratorNodeDefaultSettings() 
            : base(PluginGeneratorNodeDefaultSettingsValidator.Validator) // Class.tt Line: 43
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
        public static PluginGeneratorNodeDefaultSettings Clone(PluginGeneratorNodeDefaultSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PluginGeneratorNodeDefaultSettings vm = new PluginGeneratorNodeDefaultSettings();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.NodeSettingsVmGuid = from.NodeSettingsVmGuid; // Clone.tt Line: 65
            vm.Settings = from.Settings; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PluginGeneratorNodeDefaultSettings to, PluginGeneratorNodeDefaultSettings from, bool isDeep = true) // Clone.tt Line: 77
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.NodeSettingsVmGuid = m.NodeSettingsVmGuid; // Clone.tt Line: 221
            vm.Settings = m.Settings; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
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
        #endregion Procedures
        #region Properties
        
        
        ///////////////////////////////////////////////////
        /// Guid of solution-project-generator node
        ///////////////////////////////////////////////////
        public string NodeSettingsVmGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._NodeSettingsVmGuid; 
            }
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
        partial void OnNodeSettingsVmGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnNodeSettingsVmGuidChanged();
        string IPluginGeneratorNodeDefaultSettings.NodeSettingsVmGuid { get { return this._NodeSettingsVmGuid; } } 
        
        public string Settings // Property.tt Line: 138
        { 
            get 
            { 
                return this._Settings; 
            }
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
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 160
        partial void OnSettingsChanged();
        string IPluginGeneratorNodeDefaultSettings.Settings { get { return this._Settings; } } 
        #endregion Properties
    }
    public partial class ConfigModelValidator : ValidatorBase<ConfigModel, ConfigModelValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    [CategoryOrder("Db Names Generation", 5)]
    public partial class ConfigModel : ConfigObjectVmGenSettings<ConfigModel, ConfigModelValidator>, IComparable<ConfigModel>, IConfigAcceptVisitor, IConfigModel // Class.tt Line: 7
    {
        #region CTOR
        public ConfigModel() : this((ITreeConfigNode)null)
        {
        }
        public ConfigModel(ITreeConfigNode parent) 
            : base(parent, ConfigModelValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupCommon = new GroupListCommon(this); // Class.tt Line: 32
            this.GroupConstants = new GroupListConstants(this); // Class.tt Line: 32
            this.GroupEnumerations = new GroupListEnumerations(this); // Class.tt Line: 32
            this.GroupCatalogs = new GroupListCatalogs(this); // Class.tt Line: 32
            this.GroupDocuments = new GroupDocuments(this); // Class.tt Line: 32
            this.GroupJournals = new GroupListJournals(this); // Class.tt Line: 32
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static ConfigModel Clone(ITreeConfigNode parent, ConfigModel from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            ConfigModel vm = new ConfigModel(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Version = from.Version; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.CompositeNameMaxLength = from.CompositeNameMaxLength; // Clone.tt Line: 65
            vm.IsCompositeNames = from.IsCompositeNames; // Clone.tt Line: 65
            vm.IsUseGroupPrefix = from.IsUseGroupPrefix; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupCommon = GroupListCommon.Clone(vm, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupConstants = GroupListConstants.Clone(vm, from.GroupConstants, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupEnumerations = GroupListEnumerations.Clone(vm, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupCatalogs = GroupListCatalogs.Clone(vm, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupDocuments = GroupDocuments.Clone(vm, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupJournals = GroupListJournals.Clone(vm, from.GroupJournals, isDeep);
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(ConfigModel to, ConfigModel from, bool isDeep = true) // Clone.tt Line: 77
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
            to.IsCompositeNames = from.IsCompositeNames; // Clone.tt Line: 141
            to.IsUseGroupPrefix = from.IsUseGroupPrefix; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                GroupListCommon.Update(to.GroupCommon, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListConstants.Update(to.GroupConstants, from.GroupConstants, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListEnumerations.Update(to.GroupEnumerations, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListCatalogs.Update(to.GroupCatalogs, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupDocuments.Update(to.GroupDocuments, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListJournals.Update(to.GroupJournals, from.GroupJournals, isDeep);
        }
        // Clone.tt Line: 147
        #region IEditable
        public override ConfigModel Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return ConfigModel.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(ConfigModel from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            ConfigModel.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_config_model' to 'ConfigModel'
        public static ConfigModel ConvertToVM(Proto.Config.proto_config_model m, ConfigModel vm) // Clone.tt Line: 170
        {
            Contract.Requires(vm != null);
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Version = m.Version; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.CompositeNameMaxLength = m.CompositeNameMaxLength; // Clone.tt Line: 221
            vm.IsCompositeNames = m.IsCompositeNames; // Clone.tt Line: 221
            vm.IsUseGroupPrefix = m.IsUseGroupPrefix; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            if (vm.GroupCommon == null) // Clone.tt Line: 213
                vm.GroupCommon = new GroupListCommon(vm); // Clone.tt Line: 215
            GroupListCommon.ConvertToVM(m.GroupCommon, vm.GroupCommon); // Clone.tt Line: 219
            if (vm.GroupConstants == null) // Clone.tt Line: 213
                vm.GroupConstants = new GroupListConstants(vm); // Clone.tt Line: 215
            GroupListConstants.ConvertToVM(m.GroupConstants, vm.GroupConstants); // Clone.tt Line: 219
            if (vm.GroupEnumerations == null) // Clone.tt Line: 213
                vm.GroupEnumerations = new GroupListEnumerations(vm); // Clone.tt Line: 215
            GroupListEnumerations.ConvertToVM(m.GroupEnumerations, vm.GroupEnumerations); // Clone.tt Line: 219
            if (vm.GroupCatalogs == null) // Clone.tt Line: 213
                vm.GroupCatalogs = new GroupListCatalogs(vm); // Clone.tt Line: 215
            GroupListCatalogs.ConvertToVM(m.GroupCatalogs, vm.GroupCatalogs); // Clone.tt Line: 219
            if (vm.GroupDocuments == null) // Clone.tt Line: 213
                vm.GroupDocuments = new GroupDocuments(vm); // Clone.tt Line: 215
            GroupDocuments.ConvertToVM(m.GroupDocuments, vm.GroupDocuments); // Clone.tt Line: 219
            if (vm.GroupJournals == null) // Clone.tt Line: 213
                vm.GroupJournals = new GroupListJournals(vm); // Clone.tt Line: 215
            GroupListJournals.ConvertToVM(m.GroupJournals, vm.GroupJournals); // Clone.tt Line: 219
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        // Conversion from 'ConfigModel' to 'proto_config_model'
        public static Proto.Config.proto_config_model ConvertToProto(ConfigModel vm) // Clone.tt Line: 236
        {
            Contract.Requires(vm != null);
            Proto.Config.proto_config_model m = new Proto.Config.proto_config_model(); // Clone.tt Line: 239
            m.Guid = vm.Guid; // Clone.tt Line: 276
            m.Version = vm.Version; // Clone.tt Line: 276
            m.Name = vm.Name; // Clone.tt Line: 276
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 276
            m.NameUi = vm.NameUi; // Clone.tt Line: 276
            m.Description = vm.Description; // Clone.tt Line: 276
            m.CompositeNameMaxLength = vm.CompositeNameMaxLength; // Clone.tt Line: 276
            m.IsCompositeNames = vm.IsCompositeNames; // Clone.tt Line: 276
            m.IsUseGroupPrefix = vm.IsUseGroupPrefix; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
            m.GroupCommon = GroupListCommon.ConvertToProto(vm.GroupCommon); // Clone.tt Line: 270
            m.GroupConstants = GroupListConstants.ConvertToProto(vm.GroupConstants); // Clone.tt Line: 270
            m.GroupEnumerations = GroupListEnumerations.ConvertToProto(vm.GroupEnumerations); // Clone.tt Line: 270
            m.GroupCatalogs = GroupListCatalogs.ConvertToProto(vm.GroupCatalogs); // Clone.tt Line: 270
            m.GroupDocuments = GroupDocuments.ConvertToProto(vm.GroupDocuments); // Clone.tt Line: 270
            m.GroupJournals = GroupListJournals.ConvertToProto(vm.GroupJournals); // Clone.tt Line: 270
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
            this.GroupCommon.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupConstants.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupEnumerations.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupCatalogs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupDocuments.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            this.GroupJournals.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 30
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(2)]
        [ReadOnly(true)]
        public int Version // Property.tt Line: 138
        { 
            get 
            { 
                return this._Version; 
            }
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
        partial void OnVersionChanging(ref int to); // Property.tt Line: 160
        partial void OnVersionChanged();
        int IConfigModel.Version { get { return this._Version; } } 
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IConfigModel.Description { get { return this._Description; } } 
        
        [PropertyOrderAttribute(8)]
        [Category("Composite Names Generation")]
        public uint CompositeNameMaxLength // Property.tt Line: 138
        { 
            get 
            { 
                return this._CompositeNameMaxLength; 
            }
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
        partial void OnCompositeNameMaxLengthChanging(ref uint to); // Property.tt Line: 160
        partial void OnCompositeNameMaxLengthChanged();
        uint IConfigModel.CompositeNameMaxLength { get { return this._CompositeNameMaxLength; } } 
        
        [PropertyOrderAttribute(9)]
        [Description("Use parent-child composite names.")]
        [Category("Composite Names Generation")]
        public bool IsCompositeNames // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsCompositeNames; 
            }
            set
            {
                if (this._IsCompositeNames != value)
                {
                    this.OnIsCompositeNamesChanging(ref value);
                    this._IsCompositeNames = value;
                    this.OnIsCompositeNamesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsCompositeNames;
        partial void OnIsCompositeNamesChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsCompositeNamesChanged();
        bool IConfigModel.IsCompositeNames { get { return this._IsCompositeNames; } } 
        
        [PropertyOrderAttribute(10)]
        [Description("Composite names use their parent name as prefix. In a case of simple names all object's name will have only group name as a prefix.")]
        [Category("Composite Names Generation")]
        public bool IsUseGroupPrefix // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsUseGroupPrefix; 
            }
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
        partial void OnIsUseGroupPrefixChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsUseGroupPrefixChanged();
        bool IConfigModel.IsUseGroupPrefix { get { return this._IsUseGroupPrefix; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IConfigModel.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IConfigModel.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IConfigModel.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IConfigModel.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IConfigModel.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public GroupListCommon GroupCommon // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupCommon; 
            }
            set
            {
                if (this._GroupCommon != value)
                {
                    this.OnGroupCommonChanging(ref value);
                    this._GroupCommon = value;
                    this.OnGroupCommonChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListCommon _GroupCommon;
        partial void OnGroupCommonChanging(ref GroupListCommon to); // Property.tt Line: 134
        partial void OnGroupCommonChanged();
        IGroupListCommon IConfigModel.GroupCommon { get { return this._GroupCommon; } }
        
        [BrowsableAttribute(false)]
        public GroupListConstants GroupConstants // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupConstants; 
            }
            set
            {
                if (this._GroupConstants != value)
                {
                    this.OnGroupConstantsChanging(ref value);
                    this._GroupConstants = value;
                    this.OnGroupConstantsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListConstants _GroupConstants;
        partial void OnGroupConstantsChanging(ref GroupListConstants to); // Property.tt Line: 134
        partial void OnGroupConstantsChanged();
        IGroupListConstants IConfigModel.GroupConstants { get { return this._GroupConstants; } }
        
        [BrowsableAttribute(false)]
        public GroupListEnumerations GroupEnumerations // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupEnumerations; 
            }
            set
            {
                if (this._GroupEnumerations != value)
                {
                    this.OnGroupEnumerationsChanging(ref value);
                    this._GroupEnumerations = value;
                    this.OnGroupEnumerationsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListEnumerations _GroupEnumerations;
        partial void OnGroupEnumerationsChanging(ref GroupListEnumerations to); // Property.tt Line: 134
        partial void OnGroupEnumerationsChanged();
        IGroupListEnumerations IConfigModel.GroupEnumerations { get { return this._GroupEnumerations; } }
        
        [BrowsableAttribute(false)]
        public GroupListCatalogs GroupCatalogs // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupCatalogs; 
            }
            set
            {
                if (this._GroupCatalogs != value)
                {
                    this.OnGroupCatalogsChanging(ref value);
                    this._GroupCatalogs = value;
                    this.OnGroupCatalogsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListCatalogs _GroupCatalogs;
        partial void OnGroupCatalogsChanging(ref GroupListCatalogs to); // Property.tt Line: 134
        partial void OnGroupCatalogsChanged();
        IGroupListCatalogs IConfigModel.GroupCatalogs { get { return this._GroupCatalogs; } }
        
        [BrowsableAttribute(false)]
        public GroupDocuments GroupDocuments // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupDocuments; 
            }
            set
            {
                if (this._GroupDocuments != value)
                {
                    this.OnGroupDocumentsChanging(ref value);
                    this._GroupDocuments = value;
                    this.OnGroupDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupDocuments _GroupDocuments;
        partial void OnGroupDocumentsChanging(ref GroupDocuments to); // Property.tt Line: 134
        partial void OnGroupDocumentsChanged();
        IGroupDocuments IConfigModel.GroupDocuments { get { return this._GroupDocuments; } }
        
        [BrowsableAttribute(false)]
        public GroupListJournals GroupJournals // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupJournals; 
            }
            set
            {
                if (this._GroupJournals != value)
                {
                    this.OnGroupJournalsChanging(ref value);
                    this._GroupJournals = value;
                    this.OnGroupJournalsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListJournals _GroupJournals;
        partial void OnGroupJournalsChanging(ref GroupListJournals to); // Property.tt Line: 134
        partial void OnGroupJournalsChanged();
        IGroupListJournals IConfigModel.GroupJournals { get { return this._GroupJournals; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { } // Class.tt Line: 6
    public partial class DataType : VmValidatableWithSeverity<DataType, DataTypeValidator>, IDataType // Class.tt Line: 7
    {
        #region CTOR
        public DataType() 
            : base(DataTypeValidator.Validator) // Class.tt Line: 43
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListObjectGuids = new ObservableCollection<string>(); // Class.tt Line: 52
            this.OnInit();
            this.IsValidate = true;
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static DataType Clone(DataType from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            DataType vm = new DataType();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 65
            vm.Length = from.Length; // Clone.tt Line: 65
            vm.Accuracy = from.Accuracy; // Clone.tt Line: 65
            vm.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 65
            foreach (var t in from.ListObjectGuids) // Clone.tt Line: 44
                vm.ListObjectGuids.Add(t);
            vm.EnumerationType = from.EnumerationType; // Clone.tt Line: 65
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 65
            vm.IsPositive = from.IsPositive; // Clone.tt Line: 65
            vm.IsNullable = from.IsNullable; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(DataType to, DataType from, bool isDeep = true) // Clone.tt Line: 77
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
            to.EnumerationType = from.EnumerationType; // Clone.tt Line: 141
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 141
            to.IsPositive = from.IsPositive; // Clone.tt Line: 141
            to.IsNullable = from.IsNullable; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
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
            vm.EnumerationType = (EnumEnumerationType)m.EnumerationType; // Clone.tt Line: 221
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 221
            vm.IsPositive = m.IsPositive; // Clone.tt Line: 221
            vm.IsNullable = m.IsNullable; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
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
            m.EnumerationType = (Proto.Config.enum_enumeration_type)vm.EnumerationType; // Clone.tt Line: 274
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 276
            m.IsPositive = vm.IsPositive; // Clone.tt Line: 276
            m.IsNullable = vm.IsNullable; // Clone.tt Line: 276
            return m;
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(1)]
        [DisplayName("Type")]
        public EnumDataType DataTypeEnum // Property.tt Line: 138
        { 
            get 
            { 
                return this._DataTypeEnum; 
            }
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
        partial void OnDataTypeEnumChanging(ref EnumDataType to); // Property.tt Line: 160
        partial void OnDataTypeEnumChanged();
        EnumDataType IDataType.DataTypeEnum { get { return this._DataTypeEnum; } } 
        
        [PropertyOrderAttribute(2)]
        [DisplayName("Length")]
        [Description("Maximum length of data (characters in string, or decimal digits for numeric data)")]
        public uint Length // Property.tt Line: 138
        { 
            get 
            { 
                return this._Length; 
            }
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
        partial void OnLengthChanging(ref uint to); // Property.tt Line: 160
        partial void OnLengthChanged();
        uint IDataType.Length { get { return this._Length; } } 
        
        [PropertyOrderAttribute(3)]
        [DisplayName("Accuracy")]
        [Description("Number of decimal places for numeric data)")]
        public uint Accuracy // Property.tt Line: 138
        { 
            get 
            { 
                return this._Accuracy; 
            }
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
        partial void OnAccuracyChanging(ref uint to); // Property.tt Line: 160
        partial void OnAccuracyChanged();
        uint IDataType.Accuracy { get { return this._Accuracy; } } 
        
        [PropertyOrderAttribute(4)]
        [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        public string ObjectGuid // Property.tt Line: 138
        { 
            get 
            { 
                return this._ObjectGuid; 
            }
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
        partial void OnObjectGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnObjectGuidChanged();
        string IDataType.ObjectGuid { get { return this._ObjectGuid; } } 
        
        [PropertyOrderAttribute(5)]
        public ObservableCollection<string> ListObjectGuids // Property.tt Line: 9
        { 
            get 
            { 
                return this._ListObjectGuids; 
            }
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
        partial void OnListObjectGuidsChanging(ObservableCollection<string> to); // Property.tt Line: 30
        partial void OnListObjectGuidsChanged();
        IEnumerable<string> IDataType.ListObjectGuids { get { return this._ListObjectGuids; } }
        
        public EnumEnumerationType EnumerationType // Property.tt Line: 138
        { 
            get 
            { 
                return this._EnumerationType; 
            }
            set
            {
                if (this._EnumerationType != value)
                {
                    this.OnEnumerationTypeChanging(ref value);
                    this._EnumerationType = value;
                    this.OnEnumerationTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private EnumEnumerationType _EnumerationType;
        partial void OnEnumerationTypeChanging(ref EnumEnumerationType to); // Property.tt Line: 160
        partial void OnEnumerationTypeChanged();
        EnumEnumerationType IDataType.EnumerationType { get { return this._EnumerationType; } } 
        
        [PropertyOrderAttribute(9)]
        [DisplayName("FK Index")]
        [Description("Create Index if this property is using foreign key (for Catalog or Document type)")]
        public bool IsIndexFk // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsIndexFk; 
            }
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
        partial void OnIsIndexFkChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsIndexFkChanged();
        bool IDataType.IsIndexFk { get { return this._IsIndexFk; } } 
        
        [PropertyOrderAttribute(11)]
        [DisplayName("Positive")]
        [Description("Expected always >= 0")]
        public bool IsPositive // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsPositive; 
            }
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
        partial void OnIsPositiveChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsPositiveChanged();
        bool IDataType.IsPositive { get { return this._IsPositive; } } 
        
        
        ///////////////////////////////////////////////////
        /// bool is_nullable = 12;
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(12)]
        [DisplayName("Can be NULL")]
        [Description("If unchecked always expected data")]
        public bool IsNullable // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNullable; 
            }
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
        partial void OnIsNullableChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNullableChanged();
        bool IDataType.IsNullable { get { return this._IsNullable; } } 
        #endregion Properties
    }
    public partial class GroupListCommonValidator : ValidatorBase<GroupListCommon, GroupListCommonValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    public partial class GroupListCommon : ConfigObjectVmGenSettings<GroupListCommon, GroupListCommonValidator>, IComparable<GroupListCommon>, IConfigAcceptVisitor, IGroupListCommon // Class.tt Line: 7
    {
        #region CTOR
        public GroupListCommon() : this((ITreeConfigNode)null)
        {
        }
        public GroupListCommon(ITreeConfigNode parent) 
            : base(parent, GroupListCommonValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupRoles = new GroupListRoles(this); // Class.tt Line: 32
            this.GroupViewForms = new GroupListMainViewForms(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListCommon Clone(ITreeConfigNode parent, GroupListCommon from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListCommon vm = new GroupListCommon(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupRoles = GroupListRoles.Clone(vm, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupViewForms = GroupListMainViewForms.Clone(vm, from.GroupViewForms, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListCommon to, GroupListCommon from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                GroupListRoles.Update(to.GroupRoles, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListMainViewForms.Update(to.GroupViewForms, from.GroupViewForms, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.GroupRoles == null) // Clone.tt Line: 213
                vm.GroupRoles = new GroupListRoles(vm); // Clone.tt Line: 215
            GroupListRoles.ConvertToVM(m.GroupRoles, vm.GroupRoles); // Clone.tt Line: 219
            if (vm.GroupViewForms == null) // Clone.tt Line: 213
                vm.GroupViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 215
            GroupListMainViewForms.ConvertToVM(m.GroupViewForms, vm.GroupViewForms); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.GroupRoles = GroupListRoles.ConvertToProto(vm.GroupRoles); // Clone.tt Line: 270
            m.GroupViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupViewForms); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListCommon.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public GroupListRoles GroupRoles // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupRoles; 
            }
            set
            {
                if (this._GroupRoles != value)
                {
                    this.OnGroupRolesChanging(ref value);
                    this._GroupRoles = value;
                    this.OnGroupRolesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListRoles _GroupRoles;
        partial void OnGroupRolesChanging(ref GroupListRoles to); // Property.tt Line: 134
        partial void OnGroupRolesChanged();
        IGroupListRoles IGroupListCommon.GroupRoles { get { return this._GroupRoles; } }
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupViewForms // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupViewForms; 
            }
            set
            {
                if (this._GroupViewForms != value)
                {
                    this.OnGroupViewFormsChanging(ref value);
                    this._GroupViewForms = value;
                    this.OnGroupViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListMainViewForms _GroupViewForms;
        partial void OnGroupViewFormsChanging(ref GroupListMainViewForms to); // Property.tt Line: 134
        partial void OnGroupViewFormsChanged();
        IGroupListMainViewForms IGroupListCommon.GroupViewForms { get { return this._GroupViewForms; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListCommon.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListCommon.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListCommon.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListCommon.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListCommon.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListCommon.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class RoleValidator : ValidatorBase<Role, RoleValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    public partial class Role : ConfigObjectVmGenSettings<Role, RoleValidator>, IComparable<Role>, IConfigAcceptVisitor, IRole // Class.tt Line: 7
    {
        #region CTOR
        public Role() : this((ITreeConfigNode)null)
        {
        }
        public Role(ITreeConfigNode parent) 
            : base(parent, RoleValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Role Clone(ITreeConfigNode parent, Role from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Role vm = new Role(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Role to, Role from, bool isDeep = true) // Clone.tt Line: 77
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IRole.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IRole.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IRole.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IRole.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IRole.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IRole.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IRole.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListRolesValidator : ValidatorBase<GroupListRoles, GroupListRolesValidator> { } // Class.tt Line: 6
    public partial class GroupListRoles : ConfigObjectVmGenSettings<GroupListRoles, GroupListRolesValidator>, IComparable<GroupListRoles>, IConfigAcceptVisitor, IGroupListRoles // Class.tt Line: 7
    {
        #region CTOR
        public GroupListRoles() : this((ITreeConfigNode)null)
        {
        }
        public GroupListRoles(ITreeConfigNode parent) 
            : base(parent, GroupListRolesValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListRoles = new ConfigNodesCollection<Role>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListRoles Clone(ITreeConfigNode parent, GroupListRoles from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListRoles vm = new GroupListRoles(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListRoles) // Clone.tt Line: 52
                vm.ListRoles.Add(Role.Clone(vm, (Role)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListRoles to, GroupListRoles from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListRoles.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Role> ListRoles // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListRoles; 
            }
            private set
            {
                if (this._ListRoles != value)
                {
                    this.OnListRolesChanging(value);
                    this._ListRoles = value;
                    this.OnListRolesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Role> _ListRoles;
        partial void OnListRolesChanging(SortedObservableCollection<Role> to); // Property.tt Line: 79
        partial void OnListRolesChanged();
        IEnumerable<IRole> IGroupListRoles.ListRoles { get { return this._ListRoles; } }
        public Role this[int index] { get { return (Role)this.ListRoles[index]; } }
        public void Add(Role item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListRoles.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Role> items) 
        { 
            Contract.Requires(items != null);
            this.ListRoles.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListRoles.Count; 
        }
        public void Remove(Role item) 
        {
            Contract.Requires(item != null);
            this.ListRoles.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListRoles.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListRoles.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListRoles.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListRoles.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListRoles.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListRoles.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class MainViewFormValidator : ValidatorBase<MainViewForm, MainViewFormValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    public partial class MainViewForm : ConfigObjectVmGenSettings<MainViewForm, MainViewFormValidator>, IComparable<MainViewForm>, IConfigAcceptVisitor, IMainViewForm // Class.tt Line: 7
    {
        #region CTOR
        public MainViewForm() : this((ITreeConfigNode)null)
        {
        }
        public MainViewForm(ITreeConfigNode parent) 
            : base(parent, MainViewFormValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupListViewForms = new GroupListMainViewForms(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static MainViewForm Clone(ITreeConfigNode parent, MainViewForm from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            MainViewForm vm = new MainViewForm(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupListViewForms = GroupListMainViewForms.Clone(vm, from.GroupListViewForms, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(MainViewForm to, MainViewForm from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                GroupListMainViewForms.Update(to.GroupListViewForms, from.GroupListViewForms, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.GroupListViewForms == null) // Clone.tt Line: 213
                vm.GroupListViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 215
            GroupListMainViewForms.ConvertToVM(m.GroupListViewForms, vm.GroupListViewForms); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.GroupListViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupListViewForms); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IMainViewForm.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupListViewForms // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupListViewForms; 
            }
            set
            {
                if (this._GroupListViewForms != value)
                {
                    this.OnGroupListViewFormsChanging(ref value);
                    this._GroupListViewForms = value;
                    this.OnGroupListViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListMainViewForms _GroupListViewForms;
        partial void OnGroupListViewFormsChanging(ref GroupListMainViewForms to); // Property.tt Line: 134
        partial void OnGroupListViewFormsChanged();
        IGroupListMainViewForms IMainViewForm.GroupListViewForms { get { return this._GroupListViewForms; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IMainViewForm.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IMainViewForm.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IMainViewForm.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IMainViewForm.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IMainViewForm.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IMainViewForm.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListMainViewFormsValidator : ValidatorBase<GroupListMainViewForms, GroupListMainViewFormsValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    public partial class GroupListMainViewForms : ConfigObjectVmGenSettings<GroupListMainViewForms, GroupListMainViewFormsValidator>, IComparable<GroupListMainViewForms>, IConfigAcceptVisitor, IGroupListMainViewForms // Class.tt Line: 7
    {
        #region CTOR
        public GroupListMainViewForms() : this((ITreeConfigNode)null)
        {
        }
        public GroupListMainViewForms(ITreeConfigNode parent) 
            : base(parent, GroupListMainViewFormsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListMainViewForms Clone(ITreeConfigNode parent, GroupListMainViewForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListMainViewForms vm = new GroupListMainViewForms(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListMainViewForms) // Clone.tt Line: 52
                vm.ListMainViewForms.Add(MainViewForm.Clone(vm, (MainViewForm)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListMainViewForms to, GroupListMainViewForms from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListMainViewForms.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<MainViewForm> ListMainViewForms // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListMainViewForms; 
            }
            private set
            {
                if (this._ListMainViewForms != value)
                {
                    this.OnListMainViewFormsChanging(value);
                    this._ListMainViewForms = value;
                    this.OnListMainViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<MainViewForm> _ListMainViewForms;
        partial void OnListMainViewFormsChanging(SortedObservableCollection<MainViewForm> to); // Property.tt Line: 79
        partial void OnListMainViewFormsChanged();
        IEnumerable<IMainViewForm> IGroupListMainViewForms.ListMainViewForms { get { return this._ListMainViewForms; } }
        public MainViewForm this[int index] { get { return (MainViewForm)this.ListMainViewForms[index]; } }
        public void Add(MainViewForm item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListMainViewForms.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<MainViewForm> items) 
        { 
            Contract.Requires(items != null);
            this.ListMainViewForms.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListMainViewForms.Count; 
        }
        public void Remove(MainViewForm item) 
        {
            Contract.Requires(item != null);
            this.ListMainViewForms.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListMainViewForms.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListMainViewForms.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListMainViewForms.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListMainViewForms.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListMainViewForms.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListMainViewForms.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { } // Class.tt Line: 6
    public partial class GroupListPropertiesTabs : ConfigObjectVmGenSettings<GroupListPropertiesTabs, GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IConfigAcceptVisitor, IGroupListPropertiesTabs // Class.tt Line: 7
    {
        #region CTOR
        public GroupListPropertiesTabs() : this((ITreeConfigNode)null)
        {
        }
        public GroupListPropertiesTabs(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesTabsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListPropertiesTabs Clone(ITreeConfigNode parent, GroupListPropertiesTabs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListPropertiesTabs vm = new GroupListPropertiesTabs(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListPropertiesTabs) // Clone.tt Line: 52
                vm.ListPropertiesTabs.Add(PropertiesTab.Clone(vm, (PropertiesTab)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListPropertiesTabs to, GroupListPropertiesTabs from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListPropertiesTabs.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PropertiesTab> ListPropertiesTabs // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListPropertiesTabs; 
            }
            private set
            {
                if (this._ListPropertiesTabs != value)
                {
                    this.OnListPropertiesTabsChanging(value);
                    this._ListPropertiesTabs = value;
                    this.OnListPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PropertiesTab> _ListPropertiesTabs;
        partial void OnListPropertiesTabsChanging(SortedObservableCollection<PropertiesTab> to); // Property.tt Line: 79
        partial void OnListPropertiesTabsChanged();
        IEnumerable<IPropertiesTab> IGroupListPropertiesTabs.ListPropertiesTabs { get { return this._ListPropertiesTabs; } }
        public PropertiesTab this[int index] { get { return (PropertiesTab)this.ListPropertiesTabs[index]; } }
        public void Add(PropertiesTab item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListPropertiesTabs.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<PropertiesTab> items) 
        { 
            Contract.Requires(items != null);
            this.ListPropertiesTabs.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListPropertiesTabs.Count; 
        }
        public void Remove(PropertiesTab item) 
        {
            Contract.Requires(item != null);
            this.ListPropertiesTabs.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListPropertiesTabs.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListPropertiesTabs.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListPropertiesTabs.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListPropertiesTabs.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListPropertiesTabs.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListPropertiesTabs.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PropertiesTabValidator : ValidatorBase<PropertiesTab, PropertiesTabValidator> { } // Class.tt Line: 6
    public partial class PropertiesTab : ConfigObjectVmGenSettings<PropertiesTab, PropertiesTabValidator>, IComparable<PropertiesTab>, IConfigAcceptVisitor, IPropertiesTab // Class.tt Line: 7
    {
        #region CTOR
        public PropertiesTab() : this((ITreeConfigNode)null)
        {
        }
        public PropertiesTab(ITreeConfigNode parent) 
            : base(parent, PropertiesTabValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 32
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static PropertiesTab Clone(ITreeConfigNode parent, PropertiesTab from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            PropertiesTab vm = new PropertiesTab(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(PropertiesTab to, PropertiesTab from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.GroupProperties == null) // Clone.tt Line: 213
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 219
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 213
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 215
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 219
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 270
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 270
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 276
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IPropertiesTab.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupProperties; 
            }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(ref value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupProperties;
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 134
        partial void OnGroupPropertiesChanged();
        IGroupListProperties IPropertiesTab.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupPropertiesTabs; 
            }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(ref value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 134
        partial void OnGroupPropertiesTabsChanged();
        IGroupListPropertiesTabs IPropertiesTab.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        
        ///////////////////////////////////////////////////
        /// Create Index for foreign key navigation property
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        public bool IsIndexFk // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsIndexFk; 
            }
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
        partial void OnIsIndexFkChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsIndexFkChanged();
        bool IPropertiesTab.IsIndexFk { get { return this._IsIndexFk; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IPropertiesTab.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IPropertiesTab.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IPropertiesTab.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IPropertiesTab.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IPropertiesTab.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IPropertiesTab.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { } // Class.tt Line: 6
    public partial class GroupListProperties : ConfigObjectVmGenSettings<GroupListProperties, GroupListPropertiesValidator>, IComparable<GroupListProperties>, IConfigAcceptVisitor, IGroupListProperties // Class.tt Line: 7
    {
        #region CTOR
        public GroupListProperties() : this((ITreeConfigNode)null)
        {
        }
        public GroupListProperties(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListProperties = new ConfigNodesCollection<Property>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListProperties Clone(ITreeConfigNode parent, GroupListProperties from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListProperties vm = new GroupListProperties(parent);
            vm.IsNotNotifying = true;
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
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListProperties to, GroupListProperties from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListProperties.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Property> ListProperties // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListProperties; 
            }
            private set
            {
                if (this._ListProperties != value)
                {
                    this.OnListPropertiesChanging(value);
                    this._ListProperties = value;
                    this.OnListPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Property> _ListProperties;
        partial void OnListPropertiesChanging(SortedObservableCollection<Property> to); // Property.tt Line: 79
        partial void OnListPropertiesChanged();
        IEnumerable<IProperty> IGroupListProperties.ListProperties { get { return this._ListProperties; } }
        public Property this[int index] { get { return (Property)this.ListProperties[index]; } }
        public void Add(Property item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListProperties.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Property> items) 
        { 
            Contract.Requires(items != null);
            this.ListProperties.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListProperties.Count; 
        }
        public void Remove(Property item) 
        {
            Contract.Requires(item != null);
            this.ListProperties.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        
        ///////////////////////////////////////////////////
        /// Last generated Protobuf field position
        ///////////////////////////////////////////////////
        [ReadOnly(true)]
        public uint LastGenPosition // Property.tt Line: 138
        { 
            get 
            { 
                return this._LastGenPosition; 
            }
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
        partial void OnLastGenPositionChanging(ref uint to); // Property.tt Line: 160
        partial void OnLastGenPositionChanged();
        uint IGroupListProperties.LastGenPosition { get { return this._LastGenPosition; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListProperties.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListProperties.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListProperties.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListProperties.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListProperties.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListProperties.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { } // Class.tt Line: 6
    public partial class Property : ConfigObjectVmGenSettings<Property, PropertyValidator>, IComparable<Property>, IConfigAcceptVisitor, IProperty // Class.tt Line: 7
    {
        #region CTOR
        public Property() : this((ITreeConfigNode)null)
        {
        }
        public Property(ITreeConfigNode parent) 
            : base(parent, PropertyValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 30
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Property Clone(ITreeConfigNode parent, Property from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Property vm = new Property(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.DataType = DataType.Clone(from.DataType, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.Position = from.Position; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Property to, Property from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                DataType.Update(to.DataType, from.DataType, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            to.Position = from.Position; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.DataType == null) // Clone.tt Line: 213
                vm.DataType = new DataType(); // Clone.tt Line: 217
            DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
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
            vm.IsNotNotifying = false;
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
            m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IProperty.Description { get { return this._Description; } } 
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 113
        { 
            get 
            { 
                return this._DataType; 
            }
            set
            {
                if (this._DataType != value)
                {
                    this.OnDataTypeChanging(ref value);
                    this._DataType = value;
                    this.OnDataTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private DataType _DataType;
        partial void OnDataTypeChanging(ref DataType to); // Property.tt Line: 134
        partial void OnDataTypeChanged();
        IDataType IProperty.DataType { get { return this._DataType; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IProperty.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IProperty.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IProperty.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IProperty.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IProperty.IsHasChanged { get { return this._IsHasChanged; } } 
        
        
        ///////////////////////////////////////////////////
        /// Protobuf field position
        /// Reserved positions: 1 - primary key
        ///////////////////////////////////////////////////
        [ReadOnly(true)]
        public uint Position // Property.tt Line: 138
        { 
            get 
            { 
                return this._Position; 
            }
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
        partial void OnPositionChanging(ref uint to); // Property.tt Line: 160
        partial void OnPositionChanged();
        uint IProperty.Position { get { return this._Position; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IProperty.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { } // Class.tt Line: 6
    public partial class GroupListConstants : ConfigObjectVmGenSettings<GroupListConstants, GroupListConstantsValidator>, IComparable<GroupListConstants>, IConfigAcceptVisitor, IGroupListConstants // Class.tt Line: 7
    {
        #region CTOR
        public GroupListConstants() : this((ITreeConfigNode)null)
        {
        }
        public GroupListConstants(ITreeConfigNode parent) 
            : base(parent, GroupListConstantsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListConstants = new ConfigNodesCollection<Constant>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListConstants Clone(ITreeConfigNode parent, GroupListConstants from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListConstants vm = new GroupListConstants(parent);
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListConstants to, GroupListConstants from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListConstants.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Constant> ListConstants // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListConstants; 
            }
            private set
            {
                if (this._ListConstants != value)
                {
                    this.OnListConstantsChanging(value);
                    this._ListConstants = value;
                    this.OnListConstantsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Constant> _ListConstants;
        partial void OnListConstantsChanging(SortedObservableCollection<Constant> to); // Property.tt Line: 79
        partial void OnListConstantsChanged();
        IEnumerable<IConstant> IGroupListConstants.ListConstants { get { return this._ListConstants; } }
        public Constant this[int index] { get { return (Constant)this.ListConstants[index]; } }
        public void Add(Constant item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListConstants.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Constant> items) 
        { 
            Contract.Requires(items != null);
            this.ListConstants.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListConstants.Count; 
        }
        public void Remove(Constant item) 
        {
            Contract.Requires(item != null);
            this.ListConstants.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListConstants.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListConstants.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListConstants.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListConstants.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListConstants.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListConstants.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { } // Class.tt Line: 6
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    public partial class Constant : ConfigObjectVmGenSettings<Constant, ConstantValidator>, IComparable<Constant>, IConfigAcceptVisitor, IConstant // Class.tt Line: 7
    {
        #region CTOR
        public Constant() : this((ITreeConfigNode)null)
        {
        }
        public Constant(ITreeConfigNode parent) 
            : base(parent, ConstantValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 30
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Constant Clone(ITreeConfigNode parent, Constant from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Constant vm = new Constant(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.DataType = DataType.Clone(from.DataType, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Constant to, Constant from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                DataType.Update(to.DataType, from.DataType, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            if (vm.DataType == null) // Clone.tt Line: 213
                vm.DataType = new DataType(); // Clone.tt Line: 217
            DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IConstant.Description { get { return this._Description; } } 
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 113
        { 
            get 
            { 
                return this._DataType; 
            }
            set
            {
                if (this._DataType != value)
                {
                    this.OnDataTypeChanging(ref value);
                    this._DataType = value;
                    this.OnDataTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private DataType _DataType;
        partial void OnDataTypeChanging(ref DataType to); // Property.tt Line: 134
        partial void OnDataTypeChanged();
        IDataType IConstant.DataType { get { return this._DataType; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IConstant.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IConstant.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IConstant.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IConstant.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IConstant.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IConstant.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { } // Class.tt Line: 6
    public partial class GroupListEnumerations : ConfigObjectVmGenSettings<GroupListEnumerations, GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IConfigAcceptVisitor, IGroupListEnumerations // Class.tt Line: 7
    {
        #region CTOR
        public GroupListEnumerations() : this((ITreeConfigNode)null)
        {
        }
        public GroupListEnumerations(ITreeConfigNode parent) 
            : base(parent, GroupListEnumerationsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListEnumerations = new ConfigNodesCollection<Enumeration>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListEnumerations Clone(ITreeConfigNode parent, GroupListEnumerations from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListEnumerations vm = new GroupListEnumerations(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListEnumerations) // Clone.tt Line: 52
                vm.ListEnumerations.Add(Enumeration.Clone(vm, (Enumeration)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListEnumerations to, GroupListEnumerations from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListEnumerations.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Enumeration> ListEnumerations // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListEnumerations; 
            }
            private set
            {
                if (this._ListEnumerations != value)
                {
                    this.OnListEnumerationsChanging(value);
                    this._ListEnumerations = value;
                    this.OnListEnumerationsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Enumeration> _ListEnumerations;
        partial void OnListEnumerationsChanging(SortedObservableCollection<Enumeration> to); // Property.tt Line: 79
        partial void OnListEnumerationsChanged();
        IEnumerable<IEnumeration> IGroupListEnumerations.ListEnumerations { get { return this._ListEnumerations; } }
        public Enumeration this[int index] { get { return (Enumeration)this.ListEnumerations[index]; } }
        public void Add(Enumeration item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListEnumerations.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Enumeration> items) 
        { 
            Contract.Requires(items != null);
            this.ListEnumerations.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListEnumerations.Count; 
        }
        public void Remove(Enumeration item) 
        {
            Contract.Requires(item != null);
            this.ListEnumerations.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListEnumerations.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListEnumerations.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListEnumerations.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListEnumerations.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListEnumerations.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListEnumerations.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { } // Class.tt Line: 6
    public partial class Enumeration : ConfigObjectVmGenSettings<Enumeration, EnumerationValidator>, IComparable<Enumeration>, IConfigAcceptVisitor, IEnumeration // Class.tt Line: 7
    {
        #region CTOR
        public Enumeration() : this((ITreeConfigNode)null)
        {
        }
        public Enumeration(ITreeConfigNode parent) 
            : base(parent, EnumerationValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Enumeration Clone(ITreeConfigNode parent, Enumeration from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Enumeration vm = new Enumeration(parent);
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Enumeration to, Enumeration from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IEnumeration.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// Enumeration element type
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        [DisplayName("Type")]
        public EnumEnumerationType DataTypeEnum // Property.tt Line: 138
        { 
            get 
            { 
                return this._DataTypeEnum; 
            }
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
        partial void OnDataTypeEnumChanging(ref EnumEnumerationType to); // Property.tt Line: 160
        partial void OnDataTypeEnumChanged();
        EnumEnumerationType IEnumeration.DataTypeEnum { get { return this._DataTypeEnum; } } 
        
        
        ///////////////////////////////////////////////////
        /// Length of string if 'STRING' is selected as enumeration element type
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(5)]
        [DisplayName("Length")]
        public int DataTypeLength // Property.tt Line: 138
        { 
            get 
            { 
                return this._DataTypeLength; 
            }
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
        partial void OnDataTypeLengthChanging(ref int to); // Property.tt Line: 160
        partial void OnDataTypeLengthChanged();
        int IEnumeration.DataTypeLength { get { return this._DataTypeLength; } } 
        
        [DisplayName("Elements")]
        [NewItemTypes(typeof(EnumerationPair))]
        public ConfigNodesCollection<EnumerationPair> ListEnumerationPairs // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListEnumerationPairs; 
            }
            private set
            {
                if (this._ListEnumerationPairs != value)
                {
                    this.OnListEnumerationPairsChanging(value);
                    this._ListEnumerationPairs = value;
                    this.OnListEnumerationPairsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<EnumerationPair> _ListEnumerationPairs;
        partial void OnListEnumerationPairsChanging(SortedObservableCollection<EnumerationPair> to); // Property.tt Line: 79
        partial void OnListEnumerationPairsChanged();
        IEnumerable<IEnumerationPair> IEnumeration.ListEnumerationPairs { get { return this._ListEnumerationPairs; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IEnumeration.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IEnumeration.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IEnumeration.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IEnumeration.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IEnumeration.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IEnumeration.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { } // Class.tt Line: 6
    public partial class EnumerationPair : ConfigObjectVmGenSettings<EnumerationPair, EnumerationPairValidator>, IComparable<EnumerationPair>, IConfigAcceptVisitor, IEnumerationPair // Class.tt Line: 7
    {
        #region CTOR
        public EnumerationPair() : this((ITreeConfigNode)null)
        {
        }
        public EnumerationPair(ITreeConfigNode parent) 
            : base(parent, EnumerationPairValidator.Validator) // Class.tt Line: 15
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
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static EnumerationPair Clone(ITreeConfigNode parent, EnumerationPair from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            EnumerationPair vm = new EnumerationPair(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.Value = from.Value; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(EnumerationPair to, EnumerationPair from, bool isDeep = true) // Clone.tt Line: 77
        {
            Contract.Requires(to != null);
            Contract.Requires(from != null);
            to.Guid = from.Guid; // Clone.tt Line: 141
            to.Name = from.Name; // Clone.tt Line: 141
            to.SortingValue = from.SortingValue; // Clone.tt Line: 141
            to.NameUi = from.NameUi; // Clone.tt Line: 141
            to.Description = from.Description; // Clone.tt Line: 141
            to.Value = from.Value; // Clone.tt Line: 141
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.Value = m.Value; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IEnumerationPair.Description { get { return this._Description; } } 
        
        public string Value // Property.tt Line: 138
        { 
            get 
            { 
                return this._Value; 
            }
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
        partial void OnValueChanging(ref string to); // Property.tt Line: 160
        partial void OnValueChanged();
        string IEnumerationPair.Value { get { return this._Value; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IEnumerationPair.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IEnumerationPair.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IEnumerationPair.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IEnumerationPair.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IEnumerationPair.IsHasChanged { get { return this._IsHasChanged; } } 
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { } // Class.tt Line: 6
    public partial class Catalog : ConfigObjectVmGenSettings<Catalog, CatalogValidator>, IComparable<Catalog>, IConfigAcceptVisitor, ICatalog // Class.tt Line: 7
    {
        #region CTOR
        public Catalog() : this((ITreeConfigNode)null)
        {
        }
        public Catalog(ITreeConfigNode parent) 
            : base(parent, CatalogValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 32
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 32
            this.GroupForms = new GroupListForms(this); // Class.tt Line: 32
            this.GroupReports = new GroupListReports(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Catalog Clone(ITreeConfigNode parent, Catalog from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Catalog vm = new Catalog(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupForms = GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupReports = GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Catalog to, Catalog from, bool isDeep = true) // Clone.tt Line: 77
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            if (vm.GroupProperties == null) // Clone.tt Line: 213
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 219
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 213
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 215
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 219
            if (vm.GroupForms == null) // Clone.tt Line: 213
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 215
            GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 219
            if (vm.GroupReports == null) // Clone.tt Line: 213
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 215
            GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 219
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 270
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 270
            m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 270
            m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 270
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string ICatalog.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(true)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool ICatalog.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool ICatalog.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(true)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool ICatalog.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(true)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool ICatalog.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool ICatalog.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupProperties; 
            }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(ref value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupProperties;
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 134
        partial void OnGroupPropertiesChanged();
        IGroupListProperties ICatalog.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupPropertiesTabs; 
            }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(ref value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 134
        partial void OnGroupPropertiesTabsChanged();
        IGroupListPropertiesTabs ICatalog.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupForms; 
            }
            set
            {
                if (this._GroupForms != value)
                {
                    this.OnGroupFormsChanging(ref value);
                    this._GroupForms = value;
                    this.OnGroupFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListForms _GroupForms;
        partial void OnGroupFormsChanging(ref GroupListForms to); // Property.tt Line: 134
        partial void OnGroupFormsChanged();
        IGroupListForms ICatalog.GroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupReports; 
            }
            set
            {
                if (this._GroupReports != value)
                {
                    this.OnGroupReportsChanging(ref value);
                    this._GroupReports = value;
                    this.OnGroupReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListReports _GroupReports;
        partial void OnGroupReportsChanging(ref GroupListReports to); // Property.tt Line: 134
        partial void OnGroupReportsChanged();
        IGroupListReports ICatalog.GroupReports { get { return this._GroupReports; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> ICatalog.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { } // Class.tt Line: 6
    public partial class GroupListCatalogs : ConfigObjectVmGenSettings<GroupListCatalogs, GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IConfigAcceptVisitor, IGroupListCatalogs // Class.tt Line: 7
    {
        #region CTOR
        public GroupListCatalogs() : this((ITreeConfigNode)null)
        {
        }
        public GroupListCatalogs(ITreeConfigNode parent) 
            : base(parent, GroupListCatalogsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListCatalogs = new ConfigNodesCollection<Catalog>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListCatalogs Clone(ITreeConfigNode parent, GroupListCatalogs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListCatalogs vm = new GroupListCatalogs(parent);
            vm.IsNotNotifying = true;
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
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListCatalogs to, GroupListCatalogs from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListCatalogs.Description { get { return this._Description; } } 
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Db prefix")]
        [Description("Prefix for catalog db table names. Used if set to use in config model")]
        public string PrefixForDbTables // Property.tt Line: 138
        { 
            get 
            { 
                return this._PrefixForDbTables; 
            }
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
        partial void OnPrefixForDbTablesChanging(ref string to); // Property.tt Line: 160
        partial void OnPrefixForDbTablesChanged();
        string IGroupListCatalogs.PrefixForDbTables { get { return this._PrefixForDbTables; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Catalog> ListCatalogs // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListCatalogs; 
            }
            private set
            {
                if (this._ListCatalogs != value)
                {
                    this.OnListCatalogsChanging(value);
                    this._ListCatalogs = value;
                    this.OnListCatalogsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Catalog> _ListCatalogs;
        partial void OnListCatalogsChanging(SortedObservableCollection<Catalog> to); // Property.tt Line: 79
        partial void OnListCatalogsChanged();
        IEnumerable<ICatalog> IGroupListCatalogs.ListCatalogs { get { return this._ListCatalogs; } }
        public Catalog this[int index] { get { return (Catalog)this.ListCatalogs[index]; } }
        public void Add(Catalog item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListCatalogs.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Catalog> items) 
        { 
            Contract.Requires(items != null);
            this.ListCatalogs.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListCatalogs.Count; 
        }
        public void Remove(Catalog item) 
        {
            Contract.Requires(item != null);
            this.ListCatalogs.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListCatalogs.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListCatalogs.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListCatalogs.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListCatalogs.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListCatalogs.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListCatalogs.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { } // Class.tt Line: 6
    public partial class GroupDocuments : ConfigObjectVmGenSettings<GroupDocuments, GroupDocumentsValidator>, IComparable<GroupDocuments>, IConfigAcceptVisitor, IGroupDocuments // Class.tt Line: 7
    {
        #region CTOR
        public GroupDocuments() : this((ITreeConfigNode)null)
        {
        }
        public GroupDocuments(ITreeConfigNode parent) 
            : base(parent, GroupDocumentsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupSharedProperties = new GroupListProperties(this); // Class.tt Line: 32
            this.GroupListDocuments = new GroupListDocuments(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupDocuments Clone(ITreeConfigNode parent, GroupDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupDocuments vm = new GroupDocuments(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.PrefixForDbTables = from.PrefixForDbTables; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupSharedProperties = GroupListProperties.Clone(vm, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupListDocuments = GroupListDocuments.Clone(vm, from.GroupListDocuments, isDeep);
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupDocuments to, GroupDocuments from, bool isDeep = true) // Clone.tt Line: 77
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
                GroupListProperties.Update(to.GroupSharedProperties, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListDocuments.Update(to.GroupListDocuments, from.GroupListDocuments, isDeep);
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.PrefixForDbTables = m.PrefixForDbTables; // Clone.tt Line: 221
            if (vm.GroupSharedProperties == null) // Clone.tt Line: 213
                vm.GroupSharedProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            GroupListProperties.ConvertToVM(m.GroupSharedProperties, vm.GroupSharedProperties); // Clone.tt Line: 219
            if (vm.GroupListDocuments == null) // Clone.tt Line: 213
                vm.GroupListDocuments = new GroupListDocuments(vm); // Clone.tt Line: 215
            GroupListDocuments.ConvertToVM(m.GroupListDocuments, vm.GroupListDocuments); // Clone.tt Line: 219
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.GroupSharedProperties = GroupListProperties.ConvertToProto(vm.GroupSharedProperties); // Clone.tt Line: 270
            m.GroupListDocuments = GroupListDocuments.ConvertToProto(vm.GroupListDocuments); // Clone.tt Line: 270
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupDocuments.Description { get { return this._Description; } } 
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Db prefix")]
        [Description("Prefix for document db table names. Used if set to use in config model")]
        public string PrefixForDbTables // Property.tt Line: 138
        { 
            get 
            { 
                return this._PrefixForDbTables; 
            }
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
        partial void OnPrefixForDbTablesChanging(ref string to); // Property.tt Line: 160
        partial void OnPrefixForDbTablesChanged();
        string IGroupDocuments.PrefixForDbTables { get { return this._PrefixForDbTables; } } 
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupSharedProperties // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupSharedProperties; 
            }
            set
            {
                if (this._GroupSharedProperties != value)
                {
                    this.OnGroupSharedPropertiesChanging(ref value);
                    this._GroupSharedProperties = value;
                    this.OnGroupSharedPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupSharedProperties;
        partial void OnGroupSharedPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 134
        partial void OnGroupSharedPropertiesChanged();
        IGroupListProperties IGroupDocuments.GroupSharedProperties { get { return this._GroupSharedProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListDocuments GroupListDocuments // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupListDocuments; 
            }
            set
            {
                if (this._GroupListDocuments != value)
                {
                    this.OnGroupListDocumentsChanging(ref value);
                    this._GroupListDocuments = value;
                    this.OnGroupListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListDocuments _GroupListDocuments;
        partial void OnGroupListDocumentsChanging(ref GroupListDocuments to); // Property.tt Line: 134
        partial void OnGroupListDocumentsChanged();
        IGroupListDocuments IGroupDocuments.GroupListDocuments { get { return this._GroupListDocuments; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupDocuments.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupDocuments.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupDocuments.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupDocuments.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupDocuments.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupDocuments.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { } // Class.tt Line: 6
    public partial class Document : ConfigObjectVmGenSettings<Document, DocumentValidator>, IComparable<Document>, IConfigAcceptVisitor, IDocument // Class.tt Line: 7
    {
        #region CTOR
        public Document() : this((ITreeConfigNode)null)
        {
        }
        public Document(ITreeConfigNode parent) 
            : base(parent, DocumentValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 32
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 32
            this.GroupForms = new GroupListForms(this); // Class.tt Line: 32
            this.GroupReports = new GroupListReports(this); // Class.tt Line: 32
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Document Clone(ITreeConfigNode parent, Document from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Document vm = new Document(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            if (isDeep) // Clone.tt Line: 62
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupForms = GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 62
                vm.GroupReports = GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Document to, Document from, bool isDeep = true) // Clone.tt Line: 77
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 138
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 138
                GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            if (vm.GroupProperties == null) // Clone.tt Line: 213
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 215
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 219
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 213
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 215
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 219
            if (vm.GroupForms == null) // Clone.tt Line: 213
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 215
            GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 219
            if (vm.GroupReports == null) // Clone.tt Line: 213
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 215
            GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 219
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 270
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 270
            m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 270
            m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 270
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IDocument.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IDocument.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IDocument.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IDocument.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IDocument.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IDocument.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupProperties; 
            }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(ref value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupProperties;
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 134
        partial void OnGroupPropertiesChanged();
        IGroupListProperties IDocument.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupPropertiesTabs; 
            }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(ref value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 134
        partial void OnGroupPropertiesTabsChanged();
        IGroupListPropertiesTabs IDocument.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupForms; 
            }
            set
            {
                if (this._GroupForms != value)
                {
                    this.OnGroupFormsChanging(ref value);
                    this._GroupForms = value;
                    this.OnGroupFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListForms _GroupForms;
        partial void OnGroupFormsChanging(ref GroupListForms to); // Property.tt Line: 134
        partial void OnGroupFormsChanged();
        IGroupListForms IDocument.GroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 113
        { 
            get 
            { 
                return this._GroupReports; 
            }
            set
            {
                if (this._GroupReports != value)
                {
                    this.OnGroupReportsChanging(ref value);
                    this._GroupReports = value;
                    this.OnGroupReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListReports _GroupReports;
        partial void OnGroupReportsChanging(ref GroupListReports to); // Property.tt Line: 134
        partial void OnGroupReportsChanged();
        IGroupListReports IDocument.GroupReports { get { return this._GroupReports; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IDocument.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { } // Class.tt Line: 6
    public partial class GroupListDocuments : ConfigObjectVmGenSettings<GroupListDocuments, GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IConfigAcceptVisitor, IGroupListDocuments // Class.tt Line: 7
    {
        #region CTOR
        public GroupListDocuments() : this((ITreeConfigNode)null)
        {
        }
        public GroupListDocuments(ITreeConfigNode parent) 
            : base(parent, GroupListDocumentsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListDocuments Clone(ITreeConfigNode parent, GroupListDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListDocuments vm = new GroupListDocuments(parent);
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListDocuments to, GroupListDocuments from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListDocuments.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Document> ListDocuments // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListDocuments; 
            }
            private set
            {
                if (this._ListDocuments != value)
                {
                    this.OnListDocumentsChanging(value);
                    this._ListDocuments = value;
                    this.OnListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Document> _ListDocuments;
        partial void OnListDocumentsChanging(SortedObservableCollection<Document> to); // Property.tt Line: 79
        partial void OnListDocumentsChanged();
        IEnumerable<IDocument> IGroupListDocuments.ListDocuments { get { return this._ListDocuments; } }
        public Document this[int index] { get { return (Document)this.ListDocuments[index]; } }
        public void Add(Document item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListDocuments.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Document> items) 
        { 
            Contract.Requires(items != null);
            this.ListDocuments.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListDocuments.Count; 
        }
        public void Remove(Document item) 
        {
            Contract.Requires(item != null);
            this.ListDocuments.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListDocuments.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListDocuments.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListDocuments.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListDocuments.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListDocuments.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListDocuments.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { } // Class.tt Line: 6
    public partial class GroupListJournals : ConfigObjectVmGenSettings<GroupListJournals, GroupListJournalsValidator>, IComparable<GroupListJournals>, IConfigAcceptVisitor, IGroupListJournals // Class.tt Line: 7
    {
        #region CTOR
        public GroupListJournals() : this((ITreeConfigNode)null)
        {
        }
        public GroupListJournals(ITreeConfigNode parent) 
            : base(parent, GroupListJournalsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListJournals = new ConfigNodesCollection<Journal>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListJournals Clone(ITreeConfigNode parent, GroupListJournals from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListJournals vm = new GroupListJournals(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListJournals) // Clone.tt Line: 52
                vm.ListJournals.Add(Journal.Clone(vm, (Journal)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListJournals to, GroupListJournals from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListJournals.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Journal> ListJournals // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListJournals; 
            }
            private set
            {
                if (this._ListJournals != value)
                {
                    this.OnListJournalsChanging(value);
                    this._ListJournals = value;
                    this.OnListJournalsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Journal> _ListJournals;
        partial void OnListJournalsChanging(SortedObservableCollection<Journal> to); // Property.tt Line: 79
        partial void OnListJournalsChanged();
        IEnumerable<IJournal> IGroupListJournals.ListJournals { get { return this._ListJournals; } }
        public Journal this[int index] { get { return (Journal)this.ListJournals[index]; } }
        public void Add(Journal item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListJournals.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Journal> items) 
        { 
            Contract.Requires(items != null);
            this.ListJournals.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListJournals.Count; 
        }
        public void Remove(Journal item) 
        {
            Contract.Requires(item != null);
            this.ListJournals.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListJournals.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListJournals.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListJournals.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListJournals.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListJournals.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListJournals.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { } // Class.tt Line: 6
    public partial class Journal : ConfigObjectVmGenSettings<Journal, JournalValidator>, IComparable<Journal>, IConfigAcceptVisitor, IJournal // Class.tt Line: 7
    {
        #region CTOR
        public Journal() : this((ITreeConfigNode)null)
        {
        }
        public Journal(ITreeConfigNode parent) 
            : base(parent, JournalValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Journal Clone(ITreeConfigNode parent, Journal from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Journal vm = new Journal(parent);
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Journal to, Journal from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IJournal.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Document> ListDocuments // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListDocuments; 
            }
            private set
            {
                if (this._ListDocuments != value)
                {
                    this.OnListDocumentsChanging(value);
                    this._ListDocuments = value;
                    this.OnListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Document> _ListDocuments;
        partial void OnListDocumentsChanging(SortedObservableCollection<Document> to); // Property.tt Line: 79
        partial void OnListDocumentsChanged();
        IEnumerable<IDocument> IJournal.ListDocuments { get { return this._ListDocuments; } }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IJournal.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IJournal.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IJournal.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IJournal.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IJournal.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IJournal.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { } // Class.tt Line: 6
    public partial class GroupListForms : ConfigObjectVmGenSettings<GroupListForms, GroupListFormsValidator>, IComparable<GroupListForms>, IConfigAcceptVisitor, IGroupListForms // Class.tt Line: 7
    {
        #region CTOR
        public GroupListForms() : this((ITreeConfigNode)null)
        {
        }
        public GroupListForms(ITreeConfigNode parent) 
            : base(parent, GroupListFormsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListForms = new ConfigNodesCollection<Form>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListForms Clone(ITreeConfigNode parent, GroupListForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListForms vm = new GroupListForms(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListForms) // Clone.tt Line: 52
                vm.ListForms.Add(Form.Clone(vm, (Form)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListForms to, GroupListForms from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListForms.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Form> ListForms // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListForms; 
            }
            private set
            {
                if (this._ListForms != value)
                {
                    this.OnListFormsChanging(value);
                    this._ListForms = value;
                    this.OnListFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Form> _ListForms;
        partial void OnListFormsChanging(SortedObservableCollection<Form> to); // Property.tt Line: 79
        partial void OnListFormsChanged();
        IEnumerable<IForm> IGroupListForms.ListForms { get { return this._ListForms; } }
        public Form this[int index] { get { return (Form)this.ListForms[index]; } }
        public void Add(Form item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListForms.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Form> items) 
        { 
            Contract.Requires(items != null);
            this.ListForms.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListForms.Count; 
        }
        public void Remove(Form item) 
        {
            Contract.Requires(item != null);
            this.ListForms.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListForms.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListForms.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListForms.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListForms.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListForms.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListForms.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class FormValidator : ValidatorBase<Form, FormValidator> { } // Class.tt Line: 6
    public partial class Form : ConfigObjectVmGenSettings<Form, FormValidator>, IComparable<Form>, IConfigAcceptVisitor, IForm // Class.tt Line: 7
    {
        #region CTOR
        public Form() : this((ITreeConfigNode)null)
        {
        }
        public Form(ITreeConfigNode parent) 
            : base(parent, FormValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Form Clone(ITreeConfigNode parent, Form from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Form vm = new Form(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Form to, Form from, bool isDeep = true) // Clone.tt Line: 77
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IForm.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IForm.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IForm.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IForm.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IForm.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IForm.IsHasChanged { get { return this._IsHasChanged; } } 
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_forms = 7;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IForm.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { } // Class.tt Line: 6
    public partial class GroupListReports : ConfigObjectVmGenSettings<GroupListReports, GroupListReportsValidator>, IComparable<GroupListReports>, IConfigAcceptVisitor, IGroupListReports // Class.tt Line: 7
    {
        #region CTOR
        public GroupListReports() : this((ITreeConfigNode)null)
        {
        }
        public GroupListReports(ITreeConfigNode parent) 
            : base(parent, GroupListReportsValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListReports = new ConfigNodesCollection<Report>(this); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static GroupListReports Clone(ITreeConfigNode parent, GroupListReports from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            GroupListReports vm = new GroupListReports(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListReports) // Clone.tt Line: 52
                vm.ListReports.Add(Report.Clone(vm, (Report)t, isDeep));
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(GroupListReports to, GroupListReports from, bool isDeep = true) // Clone.tt Line: 77
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
                        if (t == tt)
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
                        if (t == tt)
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
            to.IsNew = from.IsNew; // Clone.tt Line: 141
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
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
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsNew = vm.IsNew; // Clone.tt Line: 276
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsMarkedForDeletion = vm.IsMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IGroupListReports.Description { get { return this._Description; } } 
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Report> ListReports // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListReports; 
            }
            private set
            {
                if (this._ListReports != value)
                {
                    this.OnListReportsChanging(value);
                    this._ListReports = value;
                    this.OnListReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Report> _ListReports;
        partial void OnListReportsChanging(SortedObservableCollection<Report> to); // Property.tt Line: 79
        partial void OnListReportsChanged();
        IEnumerable<IReport> IGroupListReports.ListReports { get { return this._ListReports; } }
        public Report this[int index] { get { return (Report)this.ListReports[index]; } }
        public void Add(Report item) // Property.tt Line: 85
        { 
            Contract.Requires(item != null);
            this.ListReports.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Report> items) 
        { 
            Contract.Requires(items != null);
            this.ListReports.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
            this.IsChanged = true;
        }
        public int Count() 
        { 
            return this.ListReports.Count; 
        }
        public void Remove(Report item) 
        {
            Contract.Requires(item != null);
            this.ListReports.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IGroupListReports.IsNew { get { return this._IsNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IGroupListReports.IsHasNew { get { return this._IsHasNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IGroupListReports.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IGroupListReports.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IGroupListReports.IsHasChanged { get { return this._IsHasChanged; } } 
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IGroupListReports.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { } // Class.tt Line: 6
    public partial class Report : ConfigObjectVmGenSettings<Report, ReportValidator>, IComparable<Report>, IConfigAcceptVisitor, IReport // Class.tt Line: 7
    {
        #region CTOR
        public Report() : this((ITreeConfigNode)null)
        {
        }
        public Report(ITreeConfigNode parent) 
            : base(parent, ReportValidator.Validator) // Class.tt Line: 15
        {
            this.IsValidate = false;
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 26
            this.OnInit();
            this.IsValidate = true;
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
        public static Report Clone(ITreeConfigNode parent, Report from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            Report vm = new Report(parent);
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 65
            vm.NameUi = from.NameUi; // Clone.tt Line: 65
            vm.Description = from.Description; // Clone.tt Line: 65
            vm.IsNew = from.IsNew; // Clone.tt Line: 65
            vm.IsMarkedForDeletion = from.IsMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasNew = from.IsHasNew; // Clone.tt Line: 65
            vm.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 65
            vm.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 65
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 51
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 52
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 70
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(Report to, Report from, bool isDeep = true) // Clone.tt Line: 77
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
            to.IsHasNew = from.IsHasNew; // Clone.tt Line: 141
            to.IsHasMarkedForDeletion = from.IsHasMarkedForDeletion; // Clone.tt Line: 141
            to.IsHasChanged = from.IsHasChanged; // Clone.tt Line: 141
            if (isDeep) // Clone.tt Line: 86
            {
                foreach (var t in to.ListNodeGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListNodeGeneratorsSettings)
                    {
                        if (t == tt)
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
                        if (t == tt)
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 221
            vm.NameUi = m.NameUi; // Clone.tt Line: 221
            vm.Description = m.Description; // Clone.tt Line: 221
            vm.IsNew = m.IsNew; // Clone.tt Line: 221
            vm.IsMarkedForDeletion = m.IsMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasNew = m.IsHasNew; // Clone.tt Line: 221
            vm.IsHasMarkedForDeletion = m.IsHasMarkedForDeletion; // Clone.tt Line: 221
            vm.IsHasChanged = m.IsHasChanged; // Clone.tt Line: 221
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 200
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 201
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 204
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 227
            vm.IsChanged = false;
            vm.IsHasChanged = false;
            vm.IsNotNotifying = false;
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
            m.IsHasNew = vm.IsHasNew; // Clone.tt Line: 276
            m.IsHasMarkedForDeletion = vm.IsHasMarkedForDeletion; // Clone.tt Line: 276
            m.IsHasChanged = vm.IsHasChanged; // Clone.tt Line: 276
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
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 138
        { 
            get 
            { 
                return this._Description; 
            }
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 160
        partial void OnDescriptionChanged();
        string IReport.Description { get { return this._Description; } } 
        
        [BrowsableAttribute(false)]
        public bool IsNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsNew; 
            }
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
        partial void OnIsNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsNewChanged();
        bool IReport.IsNew { get { return this._IsNew; } } 
        
        [DisplayName("For deletion")]
        [Description("Mark for deletion. Will be deleted if new object, or will be trated as deprecated if exists in previous version")]
        public bool IsMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsMarkedForDeletion; 
            }
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
        partial void OnIsMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsMarkedForDeletionChanged();
        bool IReport.IsMarkedForDeletion { get { return this._IsMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasNew // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasNew; 
            }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasNewChanged();
        bool IReport.IsHasNew { get { return this._IsHasNew; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasMarkedForDeletion; 
            }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasMarkedForDeletionChanged();
        bool IReport.IsHasMarkedForDeletion { get { return this._IsHasMarkedForDeletion; } } 
        
        [BrowsableAttribute(false)]
        public bool IsHasChanged // Property.tt Line: 138
        { 
            get 
            { 
                return this._IsHasChanged; 
            }
            set
            {
                if (this._IsHasChanged != value)
                {
                    this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        partial void OnIsHasChangedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsHasChangedChanged();
        bool IReport.IsHasChanged { get { return this._IsHasChanged; } } 
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_documents = 7;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            private set
            {
                if (this._ListNodeGeneratorsSettings != value)
                {
                    this.OnListNodeGeneratorsSettingsChanging(value);
                    this._ListNodeGeneratorsSettings = value;
                    this.OnListNodeGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorNodeSettings> _ListNodeGeneratorsSettings;
        partial void OnListNodeGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorNodeSettings> to); // Property.tt Line: 79
        partial void OnListNodeGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorNodeSettings> IReport.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
        [BrowsableAttribute(false)]
        override public bool IsChanged 
        { 
            get 
            { 
                return this._IsChanged; 
            }
            set
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
        partial void OnIsChangedChanging(ref bool v);
        partial void OnIsChangedChanged();
        partial void OnIsNewChanged() { OnNodeIsNewChanged(); }
        partial void OnIsHasNewChanged() { OnNodeIsHasNewChanged(); }
        partial void OnIsChangedChanged() { OnNodeIsChangedChanged(); }
        partial void OnIsHasChangedChanged() { OnNodeIsHasChangedChanged(); }
        partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }
        partial void OnIsHasMarkedForDeletionChanged() { OnNodeIsHasMarkedForDeletionChanged(); }
        #endregion Properties
    }
    public partial class ModelRowValidator : ValidatorBase<ModelRow, ModelRowValidator> { } // Class.tt Line: 6
    public partial class ModelRow : VmBindable, IModelRow // Class.tt Line: 7
    {
        #region CTOR
        public ModelRow() // Class.tt Line: 43
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
        public static ModelRow Clone(ModelRow from, bool isDeep = true) // Clone.tt Line: 27
        {
            Contract.Requires(from != null);
            ModelRow vm = new ModelRow();
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.GroupName = from.GroupName; // Clone.tt Line: 65
            vm.Name = from.Name; // Clone.tt Line: 65
            vm.Guid = from.Guid; // Clone.tt Line: 65
            vm.IsIncluded = from.IsIncluded; // Clone.tt Line: 65
            vm.IsNotNotifying = false;
            vm.IsValidate = true;
            return vm;
        }
        public static void Update(ModelRow to, ModelRow from, bool isDeep = true) // Clone.tt Line: 77
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
            vm.IsNotNotifying = true;
            vm.IsValidate = false;
            vm.GroupName = m.GroupName; // Clone.tt Line: 221
            vm.Name = m.Name; // Clone.tt Line: 221
            vm.Guid = m.Guid; // Clone.tt Line: 221
            vm.IsIncluded = m.IsIncluded; // Clone.tt Line: 221
            vm.IsNotNotifying = false;
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
        
        public string GroupName // Property.tt Line: 138
        { 
            get 
            { 
                return this._GroupName; 
            }
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
        partial void OnGroupNameChanging(ref string to); // Property.tt Line: 160
        partial void OnGroupNameChanged();
        string IModelRow.GroupName { get { return this._GroupName; } } 
        
        public string Name // Property.tt Line: 138
        { 
            get 
            { 
                return this._Name; 
            }
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
        partial void OnNameChanging(ref string to); // Property.tt Line: 160
        partial void OnNameChanged();
        string IModelRow.Name { get { return this._Name; } } 
        
        public string Guid // Property.tt Line: 138
        { 
            get 
            { 
                return this._Guid; 
            }
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
        partial void OnGuidChanging(ref string to); // Property.tt Line: 160
        partial void OnGuidChanged();
        string IModelRow.Guid { get { return this._Guid; } } 
        
        public bool IsIncluded // Property.tt Line: 138
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
                }
            }
        }
        private bool _IsIncluded;
        partial void OnIsIncludedChanging(ref bool to); // Property.tt Line: 160
        partial void OnIsIncludedChanged();
        bool IModelRow.IsIncluded { get { return this._IsIncluded; } } 
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
        void Visit(Proto.Config.proto_plugin_generator_main_settings p);
        void Visit(Proto.Config.proto_app_project_generator p);
        void Visit(Proto.Config.proto_plugin_generator_node_default_settings p);
        void Visit(Proto.Config.proto_config_model p);
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
        void Visit(Proto.Config.proto_group_list_constants p);
        void Visit(Proto.Config.proto_constant p);
        void Visit(Proto.Config.proto_group_list_enumerations p);
        void Visit(Proto.Config.proto_enumeration p);
        void Visit(Proto.Config.proto_enumeration_pair p);
        void Visit(Proto.Config.proto_catalog p);
        void Visit(Proto.Config.proto_group_list_catalogs p);
        void Visit(Proto.Config.proto_group_documents p);
        void Visit(Proto.Config.proto_document p);
        void Visit(Proto.Config.proto_group_list_documents p);
        void Visit(Proto.Config.proto_group_list_journals p);
        void Visit(Proto.Config.proto_journal p);
        void Visit(Proto.Config.proto_group_list_forms p);
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
        protected override void OnVisit(PluginGeneratorMainSettings p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGeneratorMainSettings p) // ValidationVisitor.tt Line: 48
        {
            Contract.Requires(p != null);
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppProjectGenerator p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
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
        protected override void OnVisit(ConfigModel p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(ConfigModel p) // ValidationVisitor.tt Line: 48
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
        protected override void OnVisit(Catalog p) // ValidationVisitor.tt Line: 15
        {
            Contract.Requires(p != null);
            this.OnVisit(p as IValidatableWithSeverity);
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
        public void Visit(PluginGeneratorMainSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(PluginGeneratorMainSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(PluginGeneratorMainSettings p) { }
        protected virtual void OnVisitEnd(PluginGeneratorMainSettings p) { }
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
        public void Visit(ConfigModel p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(ConfigModel p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(ConfigModel p) { }
        protected virtual void OnVisitEnd(ConfigModel p) { }
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
        void Visit(GroupListPlugins p);
        void VisitEnd(GroupListPlugins p);
        void Visit(Plugin p);
        void VisitEnd(Plugin p);
        void Visit(PluginGenerator p);
        void VisitEnd(PluginGenerator p);
        void Visit(ConfigShortHistory p);
        void VisitEnd(ConfigShortHistory p);
        void Visit(GroupListBaseConfigLinks p);
        void VisitEnd(GroupListBaseConfigLinks p);
        void Visit(BaseConfigLink p);
        void VisitEnd(BaseConfigLink p);
        void Visit(Config p);
        void VisitEnd(Config p);
        void Visit(PluginGroupGeneratorsDefaultSettings p);
        void VisitEnd(PluginGroupGeneratorsDefaultSettings p);
        void Visit(GroupListAppSolutions p);
        void VisitEnd(GroupListAppSolutions p);
        void Visit(PluginGroupGeneratorsSettings p);
        void VisitEnd(PluginGroupGeneratorsSettings p);
        void Visit(AppSolution p);
        void VisitEnd(AppSolution p);
        void Visit(AppProject p);
        void VisitEnd(AppProject p);
        void Visit(PluginGeneratorNodeSettings p);
        void VisitEnd(PluginGeneratorNodeSettings p);
        void Visit(PluginGeneratorMainSettings p);
        void VisitEnd(PluginGeneratorMainSettings p);
        void Visit(AppProjectGenerator p);
        void VisitEnd(AppProjectGenerator p);
        void Visit(ConfigModel p);
        void VisitEnd(ConfigModel p);
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