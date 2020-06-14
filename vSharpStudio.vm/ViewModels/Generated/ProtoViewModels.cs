// Auto generated on UTC 06/14/2020 23:12:46
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

namespace vSharpStudio.vm.ViewModels // NameSpace.tt Line: 22
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017

    public interface IConfigAcceptVisitor // NameSpace.tt Line: 28
    {
        void AcceptConfigNodeVisitor(ConfigVisitor visitor);
    }
    public partial class UserSettings : VmValidatableWithSeverity<UserSettings, UserSettings.UserSettingsValidator>, IUserSettings // Class.tt Line: 6
    {
        public partial class UserSettingsValidator : ValidatorBase<UserSettings, UserSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public UserSettings() 
            : base(UserSettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Class.tt Line: 46
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static UserSettings Clone(UserSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            UserSettings vm = new UserSettings();
            vm.IsNotNotifying = true;
            vm.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Clone.tt Line: 45
            foreach (var t in from.ListOpenConfigHistory) // Clone.tt Line: 46
                vm.ListOpenConfigHistory.Add(UserSettingsOpenedConfig.Clone((UserSettingsOpenedConfig)t, isDeep));
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(UserSettings to, UserSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new UserSettingsOpenedConfig(); // Clone.tt Line: 114
                        UserSettingsOpenedConfig.Update(p, (UserSettingsOpenedConfig)tt, isDeep);
                        to.ListOpenConfigHistory.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static UserSettings ConvertToVM(Proto.Config.proto_user_settings m, UserSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.ListOpenConfigHistory = new ObservableCollection<UserSettingsOpenedConfig>(); // Clone.tt Line: 183
            foreach (var t in m.ListOpenConfigHistory) // Clone.tt Line: 184
            {
                var tvm = UserSettingsOpenedConfig.ConvertToVM(t, new UserSettingsOpenedConfig()); // Clone.tt Line: 189
                vm.ListOpenConfigHistory.Add(tvm);
            }
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'UserSettings' to 'proto_user_settings'
        public static Proto.Config.proto_user_settings ConvertToProto(UserSettings vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_user_settings m = new Proto.Config.proto_user_settings(); // Clone.tt Line: 230
            foreach (var t in vm.ListOpenConfigHistory) // Clone.tt Line: 233
                m.ListOpenConfigHistory.Add(UserSettingsOpenedConfig.ConvertToProto((UserSettingsOpenedConfig)t)); // Clone.tt Line: 237
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
    public partial class UserSettingsOpenedConfig : VmValidatableWithSeverity<UserSettingsOpenedConfig, UserSettingsOpenedConfig.UserSettingsOpenedConfigValidator>, IUserSettingsOpenedConfig // Class.tt Line: 6
    {
        public partial class UserSettingsOpenedConfigValidator : ValidatorBase<UserSettingsOpenedConfig, UserSettingsOpenedConfigValidator> { } // Class.tt Line: 8
        #region CTOR
        public UserSettingsOpenedConfig() 
            : base(UserSettingsOpenedConfigValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static UserSettingsOpenedConfig Clone(UserSettingsOpenedConfig from, bool isDeep = true) // Clone.tt Line: 27
        {
            UserSettingsOpenedConfig vm = new UserSettingsOpenedConfig();
            vm.IsNotNotifying = true;
            vm.OpenedLastTimeOn = from.OpenedLastTimeOn; // Clone.tt Line: 63
            vm.ConfigPath = from.ConfigPath; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(UserSettingsOpenedConfig to, UserSettingsOpenedConfig from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.OpenedLastTimeOn = from.OpenedLastTimeOn; // Clone.tt Line: 136
            to.ConfigPath = from.ConfigPath; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
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
        public static UserSettingsOpenedConfig ConvertToVM(Proto.Config.proto_user_settings_opened_config m, UserSettingsOpenedConfig vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.OpenedLastTimeOn = m.OpenedLastTimeOn; // Clone.tt Line: 214
            vm.ConfigPath = m.ConfigPath; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'UserSettingsOpenedConfig' to 'proto_user_settings_opened_config'
        public static Proto.Config.proto_user_settings_opened_config ConvertToProto(UserSettingsOpenedConfig vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_user_settings_opened_config m = new Proto.Config.proto_user_settings_opened_config(); // Clone.tt Line: 230
            m.OpenedLastTimeOn = vm.OpenedLastTimeOn; // Clone.tt Line: 267
            m.ConfigPath = vm.ConfigPath; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        [BrowsableAttribute(false)]
        public Google.Protobuf.WellKnownTypes.Timestamp OpenedLastTimeOn // Property.tt Line: 135
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
        partial void OnOpenedLastTimeOnChanging(ref Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 157
        partial void OnOpenedLastTimeOnChanged();
        Google.Protobuf.WellKnownTypes.Timestamp IUserSettingsOpenedConfig.OpenedLastTimeOn { get { return this._OpenedLastTimeOn; } }
        
        [BrowsableAttribute(false)]
        public string ConfigPath // Property.tt Line: 135
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
        partial void OnConfigPathChanging(ref string to); // Property.tt Line: 157
        partial void OnConfigPathChanged();
        string IUserSettingsOpenedConfig.ConfigPath { get { return this._ConfigPath; } }
    
        #endregion Properties
    }
    public partial class GroupListPlugins : ConfigObjectVmBase<GroupListPlugins, GroupListPlugins.GroupListPluginsValidator>, IComparable<GroupListPlugins>, IConfigAcceptVisitor, IGroupListPlugins // Class.tt Line: 6
    {
        public partial class GroupListPluginsValidator : ValidatorBase<GroupListPlugins, GroupListPluginsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListPlugins(ITreeConfigNode parent) 
            : base(parent, GroupListPluginsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListPlugins = new ConfigNodesCollection<Plugin>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListPlugins vm = new GroupListPlugins(parent);
            vm.IsNotNotifying = true;
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListPlugins) // Clone.tt Line: 50
                vm.ListPlugins.Add(Plugin.Clone(vm, (Plugin)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListPlugins to, GroupListPlugins from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Plugin(to); // Clone.tt Line: 112
                        Plugin.Update(p, (Plugin)tt, isDeep);
                        to.ListPlugins.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListPlugins ConvertToVM(Proto.Config.proto_group_list_plugins m, GroupListPlugins vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListPlugins) // Clone.tt Line: 194
            {
                var tvm = Plugin.ConvertToVM(t, new Plugin(vm)); // Clone.tt Line: 197
                vm.ListPlugins.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListPlugins' to 'proto_group_list_plugins'
        public static Proto.Config.proto_group_list_plugins ConvertToProto(GroupListPlugins vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_plugins m = new Proto.Config.proto_group_list_plugins(); // Clone.tt Line: 230
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            foreach (var t in vm.ListPlugins) // Clone.tt Line: 233
                m.ListPlugins.Add(Plugin.ConvertToProto((Plugin)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
            set
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
            this.ListPlugins.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Plugin> items) 
        { 
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
            this.ListPlugins.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
    
        #endregion Properties
    }
    public partial class Plugin : ConfigObjectVmBase<Plugin, Plugin.PluginValidator>, IComparable<Plugin>, IConfigAcceptVisitor, IPlugin // Class.tt Line: 6
    {
        public partial class PluginValidator : ValidatorBase<Plugin, PluginValidator> { } // Class.tt Line: 8
        #region CTOR
        public Plugin(ITreeConfigNode parent) 
            : base(parent, PluginValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListGenerators = new ConfigNodesCollection<PluginGenerator>(this); // Class.tt Line: 22
            this.OnInit();
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
            Plugin vm = new Plugin(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Version = from.Version; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.ListGenerators = new ConfigNodesCollection<PluginGenerator>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListGenerators) // Clone.tt Line: 50
                vm.ListGenerators.Add(PluginGenerator.Clone(vm, (PluginGenerator)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Plugin to, Plugin from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Version = from.Version; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGenerator(to); // Clone.tt Line: 112
                        PluginGenerator.Update(p, (PluginGenerator)tt, isDeep);
                        to.ListGenerators.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Plugin ConvertToVM(Proto.Config.proto_plugin m, Plugin vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Version = m.Version; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.ListGenerators = new ConfigNodesCollection<PluginGenerator>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListGenerators) // Clone.tt Line: 194
            {
                var tvm = PluginGenerator.ConvertToVM(t, new PluginGenerator(vm)); // Clone.tt Line: 197
                vm.ListGenerators.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Plugin' to 'proto_plugin'
        public static Proto.Config.proto_plugin ConvertToProto(Plugin vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_plugin m = new Proto.Config.proto_plugin(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Version = vm.Version; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            foreach (var t in vm.ListGenerators) // Clone.tt Line: 233
                m.ListGenerators.Add(PluginGenerator.ConvertToProto((PluginGenerator)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Version // Property.tt Line: 135
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
        partial void OnVersionChanging(ref string to); // Property.tt Line: 157
        partial void OnVersionChanged();
        string IPlugin.Version { get { return this._Version; } }
        
        [ReadOnly(true)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IPlugin.Description { get { return this._Description; } }
        
        
        ///////////////////////////////////////////////////
        /// 
        /// attr [ReadOnly(true)]
        /// string group_guid = 7;
        /// attr [ReadOnly(true)]
        /// string group_version = 8;
        /// attr [ReadOnly(true)]
        /// string group_info = 9;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGenerator> ListGenerators // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListGenerators; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class PluginGenerator : ConfigObjectVmBase<PluginGenerator, PluginGenerator.PluginGeneratorValidator>, IComparable<PluginGenerator>, IConfigAcceptVisitor, IPluginGenerator // Class.tt Line: 6
    {
        public partial class PluginGeneratorValidator : ValidatorBase<PluginGenerator, PluginGeneratorValidator> { } // Class.tt Line: 8
        #region CTOR
        public PluginGenerator(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListSettings = new ConfigNodesCollection<PluginGeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListSettings.Sort();
            }
        }
        public static PluginGenerator Clone(ITreeConfigNode parent, PluginGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            PluginGenerator vm = new PluginGenerator(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.ListSettings = new ConfigNodesCollection<PluginGeneratorSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListSettings) // Clone.tt Line: 50
                vm.ListSettings.Add(PluginGeneratorSettings.Clone(vm, (PluginGeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(PluginGenerator to, PluginGenerator from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
            {
                foreach (var t in to.ListSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            PluginGeneratorSettings.Update((PluginGeneratorSettings)t, (PluginGeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListSettings.Remove(t);
                }
                foreach (var tt in from.ListSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorSettings(to); // Clone.tt Line: 112
                        PluginGeneratorSettings.Update(p, (PluginGeneratorSettings)tt, isDeep);
                        to.ListSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static PluginGenerator ConvertToVM(Proto.Config.proto_plugin_generator m, PluginGenerator vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.ListSettings = new ConfigNodesCollection<PluginGeneratorSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorSettings.ConvertToVM(t, new PluginGeneratorSettings(vm)); // Clone.tt Line: 197
                vm.ListSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'PluginGenerator' to 'proto_plugin_generator'
        public static Proto.Config.proto_plugin_generator ConvertToProto(PluginGenerator vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_plugin_generator m = new Proto.Config.proto_plugin_generator(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            foreach (var t in vm.ListSettings) // Clone.tt Line: 233
                m.ListSettings.Add(PluginGeneratorSettings.ConvertToProto((PluginGeneratorSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [ReadOnly(true)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IPluginGenerator.Description { get { return this._Description; } }
        
        public ConfigNodesCollection<PluginGeneratorSettings> ListSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListSettings; 
            }
            set
            {
                if (this._ListSettings != value)
                {
                    this.OnListSettingsChanging(value);
                    this._ListSettings = value;
                    this.OnListSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorSettings> _ListSettings;
        partial void OnListSettingsChanging(SortedObservableCollection<PluginGeneratorSettings> to); // Property.tt Line: 79
        partial void OnListSettingsChanged();
        IEnumerable<IPluginGeneratorSettings> IPluginGenerator.ListSettings { get { return this._ListSettings; } }
    
        #endregion Properties
    }
    public partial class PluginGeneratorSettings : ConfigObjectVmBase<PluginGeneratorSettings, PluginGeneratorSettings.PluginGeneratorSettingsValidator>, IComparable<PluginGeneratorSettings>, IConfigAcceptVisitor, IPluginGeneratorSettings // Class.tt Line: 6
    {
        public partial class PluginGeneratorSettingsValidator : ValidatorBase<PluginGeneratorSettings, PluginGeneratorSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public PluginGeneratorSettings(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorSettingsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static PluginGeneratorSettings Clone(ITreeConfigNode parent, PluginGeneratorSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            PluginGeneratorSettings vm = new PluginGeneratorSettings(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 63
            vm.IsPrivate = from.IsPrivate; // Clone.tt Line: 63
            vm.FilePath = from.FilePath; // Clone.tt Line: 63
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(PluginGeneratorSettings to, PluginGeneratorSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 136
            to.IsPrivate = from.IsPrivate; // Clone.tt Line: 136
            to.FilePath = from.FilePath; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
        #region IEditable
        public override PluginGeneratorSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return PluginGeneratorSettings.Clone(this.Parent, this);
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
        public static PluginGeneratorSettings ConvertToVM(Proto.Config.proto_plugin_generator_settings m, PluginGeneratorSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 214
            vm.IsPrivate = m.IsPrivate; // Clone.tt Line: 214
            vm.FilePath = m.FilePath; // Clone.tt Line: 214
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'PluginGeneratorSettings' to 'proto_plugin_generator_settings'
        public static Proto.Config.proto_plugin_generator_settings ConvertToProto(PluginGeneratorSettings vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_plugin_generator_settings m = new Proto.Config.proto_plugin_generator_settings(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 267
            m.IsPrivate = vm.IsPrivate; // Clone.tt Line: 267
            m.FilePath = vm.FilePath; // Clone.tt Line: 267
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        /// This Description is taken from Plugin Generator
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(2)]
        [ReadOnly(true)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IPluginGeneratorSettings.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public string GeneratorSettings // Property.tt Line: 135
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
        partial void OnGeneratorSettingsChanging(ref string to); // Property.tt Line: 157
        partial void OnGeneratorSettingsChanged();
        string IPluginGeneratorSettings.GeneratorSettings { get { return this._GeneratorSettings; } }
        
        [PropertyOrderAttribute(3)]
        [Description("If false, connection string settings will be stored in configuration file. If true, will be stored in separate file.")]
        public bool IsPrivate // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsPrivate; 
            }
            set
            {
                if (this._IsPrivate != value)
                {
                    this.OnIsPrivateChanging(ref value);
                    this._IsPrivate = value;
                    this.OnIsPrivateChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsPrivate;
        partial void OnIsPrivateChanging(ref bool to); // Property.tt Line: 157
        partial void OnIsPrivateChanged();
        bool IPluginGeneratorSettings.IsPrivate { get { return this._IsPrivate; } }
        
        [PropertyOrderAttribute(4)]
        [Editor(typeof(EditorFilePicker), typeof(ITypeEditor))]
        [Description("File path to store connection string settings in private place.")]
        public string FilePath // Property.tt Line: 135
        { 
            get 
            { 
                return this._FilePath; 
            }
            set
            {
                if (this._FilePath != value)
                {
                    this.OnFilePathChanging(ref value);
                    this._FilePath = value;
                    this.OnFilePathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _FilePath = string.Empty;
        partial void OnFilePathChanging(ref string to); // Property.tt Line: 157
        partial void OnFilePathChanged();
        string IPluginGeneratorSettings.FilePath { get { return this._FilePath; } }
    
        #endregion Properties
    }
    public partial class SettingsConfig : VmValidatableWithSeverity<SettingsConfig, SettingsConfig.SettingsConfigValidator>, ISettingsConfig // Class.tt Line: 6
    {
        public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { } // Class.tt Line: 8
        #region CTOR
        public SettingsConfig() 
            : base(SettingsConfigValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static SettingsConfig Clone(SettingsConfig from, bool isDeep = true) // Clone.tt Line: 27
        {
            SettingsConfig vm = new SettingsConfig();
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 63
            vm.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(SettingsConfig to, SettingsConfig from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 136
            to.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
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
        public static SettingsConfig ConvertToVM(Proto.Config.proto_settings_config m, SettingsConfig vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.VersionMigrationCurrent = m.VersionMigrationCurrent; // Clone.tt Line: 214
            vm.VersionMigrationSupportFromMin = m.VersionMigrationSupportFromMin; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'SettingsConfig' to 'proto_settings_config'
        public static Proto.Config.proto_settings_config ConvertToProto(SettingsConfig vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_settings_config m = new Proto.Config.proto_settings_config(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.VersionMigrationCurrent = vm.VersionMigrationCurrent; // Clone.tt Line: 267
            m.VersionMigrationSupportFromMin = vm.VersionMigrationSupportFromMin; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public string Guid // Property.tt Line: 135
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
        partial void OnGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnGuidChanged();
        string ISettingsConfig.Guid { get { return this._Guid; } }
        
        public string Name // Property.tt Line: 135
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
        partial void OnNameChanging(ref string to); // Property.tt Line: 157
        partial void OnNameChanged();
        string ISettingsConfig.Name { get { return this._Name; } }
        
        [PropertyOrderAttribute(2)]
        [DisplayName("UI name")]
        public string NameUi // Property.tt Line: 135
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
        partial void OnNameUiChanging(ref string to); // Property.tt Line: 157
        partial void OnNameUiChanged();
        string ISettingsConfig.NameUi { get { return this._NameUi; } }
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string ISettingsConfig.Description { get { return this._Description; } }
        
        
        ///////////////////////////////////////////////////
        /// current migration version, increased by one on each deployment
        ///////////////////////////////////////////////////
        public int VersionMigrationCurrent // Property.tt Line: 135
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
        partial void OnVersionMigrationCurrentChanging(ref int to); // Property.tt Line: 157
        partial void OnVersionMigrationCurrentChanged();
        int ISettingsConfig.VersionMigrationCurrent { get { return this._VersionMigrationCurrent; } }
        
        
        ///////////////////////////////////////////////////
        /// min version supported by current version for migration
        ///////////////////////////////////////////////////
        public int VersionMigrationSupportFromMin // Property.tt Line: 135
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
        partial void OnVersionMigrationSupportFromMinChanging(ref int to); // Property.tt Line: 157
        partial void OnVersionMigrationSupportFromMinChanged();
        int ISettingsConfig.VersionMigrationSupportFromMin { get { return this._VersionMigrationSupportFromMin; } }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// General DB settings
    ///////////////////////////////////////////////////
    public partial class DbSettings : VmValidatableWithSeverity<DbSettings, DbSettings.DbSettingsValidator>, IDbSettings // Class.tt Line: 6
    {
        public partial class DbSettingsValidator : ValidatorBase<DbSettings, DbSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public DbSettings() 
            : base(DbSettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static DbSettings Clone(DbSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            DbSettings vm = new DbSettings();
            vm.IsNotNotifying = true;
            vm.DbSchema = from.DbSchema; // Clone.tt Line: 63
            vm.IdGenerator = from.IdGenerator; // Clone.tt Line: 63
            vm.PKeyType = from.PKeyType; // Clone.tt Line: 63
            vm.KeyName = from.KeyName; // Clone.tt Line: 63
            vm.Timestamp = from.Timestamp; // Clone.tt Line: 63
            vm.IsDbFromConnectionString = from.IsDbFromConnectionString; // Clone.tt Line: 63
            vm.ConnectionStringName = from.ConnectionStringName; // Clone.tt Line: 63
            vm.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(DbSettings to, DbSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.DbSchema = from.DbSchema; // Clone.tt Line: 136
            to.IdGenerator = from.IdGenerator; // Clone.tt Line: 136
            to.PKeyType = from.PKeyType; // Clone.tt Line: 136
            to.KeyName = from.KeyName; // Clone.tt Line: 136
            to.Timestamp = from.Timestamp; // Clone.tt Line: 136
            to.IsDbFromConnectionString = from.IsDbFromConnectionString; // Clone.tt Line: 136
            to.ConnectionStringName = from.ConnectionStringName; // Clone.tt Line: 136
            to.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
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
        public static DbSettings ConvertToVM(Proto.Config.db_settings m, DbSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.DbSchema = m.DbSchema; // Clone.tt Line: 214
            vm.IdGenerator = (DbIdGeneratorMethod)m.IdGenerator; // Clone.tt Line: 214
            vm.PKeyType = (EnumPrimaryKeyType)m.PKeyType; // Clone.tt Line: 214
            vm.KeyName = m.KeyName; // Clone.tt Line: 214
            vm.Timestamp = m.Timestamp; // Clone.tt Line: 214
            vm.IsDbFromConnectionString = m.IsDbFromConnectionString; // Clone.tt Line: 214
            vm.ConnectionStringName = m.ConnectionStringName; // Clone.tt Line: 214
            vm.PathToProjectWithConnectionString = m.PathToProjectWithConnectionString; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'DbSettings' to 'db_settings'
        public static Proto.Config.db_settings ConvertToProto(DbSettings vm) // Clone.tt Line: 228
        {
            Proto.Config.db_settings m = new Proto.Config.db_settings(); // Clone.tt Line: 230
            m.DbSchema = vm.DbSchema; // Clone.tt Line: 267
            m.IdGenerator = (Proto.Config.db_id_generator_method)vm.IdGenerator; // Clone.tt Line: 265
            m.PKeyType = (Proto.Config.proto_enum_primary_key_type)vm.PKeyType; // Clone.tt Line: 265
            m.KeyName = vm.KeyName; // Clone.tt Line: 267
            m.Timestamp = vm.Timestamp; // Clone.tt Line: 267
            m.IsDbFromConnectionString = vm.IsDbFromConnectionString; // Clone.tt Line: 267
            m.ConnectionStringName = vm.ConnectionStringName; // Clone.tt Line: 267
            m.PathToProjectWithConnectionString = vm.PathToProjectWithConnectionString; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(1)]
        [Description("DB schema name for all object in this configuration")]
        public string DbSchema // Property.tt Line: 135
        { 
            get 
            { 
                return this._DbSchema; 
            }
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
        partial void OnDbSchemaChanging(ref string to); // Property.tt Line: 157
        partial void OnDbSchemaChanged();
        string IDbSettings.DbSchema { get { return this._DbSchema; } }
        
        [PropertyOrderAttribute(2)]
        [Description("Primary key generation method")]
        public DbIdGeneratorMethod IdGenerator // Property.tt Line: 135
        { 
            get 
            { 
                return this._IdGenerator; 
            }
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
        partial void OnIdGeneratorChanging(ref DbIdGeneratorMethod to); // Property.tt Line: 157
        partial void OnIdGeneratorChanged();
        DbIdGeneratorMethod IDbSettings.IdGenerator { get { return this._IdGenerator; } }
        
        [PropertyOrderAttribute(3)]
        [Description("Primary key field type")]
        public EnumPrimaryKeyType PKeyType // Property.tt Line: 135
        { 
            get 
            { 
                return this._PKeyType; 
            }
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
        partial void OnPKeyTypeChanging(ref EnumPrimaryKeyType to); // Property.tt Line: 157
        partial void OnPKeyTypeChanged();
        EnumPrimaryKeyType IDbSettings.PKeyType { get { return this._PKeyType; } }
        
        [PropertyOrderAttribute(4)]
        [Description("Primary key field name")]
        public string KeyName // Property.tt Line: 135
        { 
            get 
            { 
                return this._KeyName; 
            }
            set
            {
                if (this._KeyName != value)
                {
                    this.OnKeyNameChanging(ref value);
                    this._KeyName = value;
                    this.OnKeyNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _KeyName = string.Empty;
        partial void OnKeyNameChanging(ref string to); // Property.tt Line: 157
        partial void OnKeyNameChanged();
        string IDbSettings.KeyName { get { return this._KeyName; } }
        
        [PropertyOrderAttribute(5)]
        [Description("Record data version/timestamp field name")]
        public string Timestamp // Property.tt Line: 135
        { 
            get 
            { 
                return this._Timestamp; 
            }
            set
            {
                if (this._Timestamp != value)
                {
                    this.OnTimestampChanging(ref value);
                    this._Timestamp = value;
                    this.OnTimestampChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _Timestamp = string.Empty;
        partial void OnTimestampChanging(ref string to); // Property.tt Line: 157
        partial void OnTimestampChanged();
        string IDbSettings.Timestamp { get { return this._Timestamp; } }
        
        
        ///////////////////////////////////////////////////
        /// if yes: 
        ///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
        /// if no:
        ///    1. Find DB type from 
        ///    2. Create connection string from db_server, db_database_name, db_user
        ///////////////////////////////////////////////////
        public bool IsDbFromConnectionString // Property.tt Line: 135
        { 
            get 
            { 
                return this._IsDbFromConnectionString; 
            }
            set
            {
                if (this._IsDbFromConnectionString != value)
                {
                    this.OnIsDbFromConnectionStringChanging(ref value);
                    this._IsDbFromConnectionString = value;
                    this.OnIsDbFromConnectionStringChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private bool _IsDbFromConnectionString;
        partial void OnIsDbFromConnectionStringChanging(ref bool to); // Property.tt Line: 157
        partial void OnIsDbFromConnectionStringChanged();
        bool IDbSettings.IsDbFromConnectionString { get { return this._IsDbFromConnectionString; } }
        
        public string ConnectionStringName // Property.tt Line: 135
        { 
            get 
            { 
                return this._ConnectionStringName; 
            }
            set
            {
                if (this._ConnectionStringName != value)
                {
                    this.OnConnectionStringNameChanging(ref value);
                    this._ConnectionStringName = value;
                    this.OnConnectionStringNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _ConnectionStringName = string.Empty;
        partial void OnConnectionStringNameChanging(ref string to); // Property.tt Line: 157
        partial void OnConnectionStringNameChanged();
        string IDbSettings.ConnectionStringName { get { return this._ConnectionStringName; } }
        
        
        ///////////////////////////////////////////////////
        /// path to project with config file containing connection string. Usefull for UNIT tests.
        /// it will override previous settings
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        [Editor(typeof(EditorFolderPicker), typeof(ITypeEditor))]
        [Description("File path to store connection string settings in private place.")]
        public string PathToProjectWithConnectionString // Property.tt Line: 135
        { 
            get 
            { 
                return this._PathToProjectWithConnectionString; 
            }
            set
            {
                if (this._PathToProjectWithConnectionString != value)
                {
                    this.OnPathToProjectWithConnectionStringChanging(ref value);
                    this._PathToProjectWithConnectionString = value;
                    this.OnPathToProjectWithConnectionStringChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                    this.IsChanged = true;
                }
            }
        }
        private string _PathToProjectWithConnectionString = string.Empty;
        partial void OnPathToProjectWithConnectionStringChanging(ref string to); // Property.tt Line: 157
        partial void OnPathToProjectWithConnectionStringChanged();
        string IDbSettings.PathToProjectWithConnectionString { get { return this._PathToProjectWithConnectionString; } }
    
        #endregion Properties
    }
    public partial class ConfigShortHistory : ConfigObjectVmBase<ConfigShortHistory, ConfigShortHistory.ConfigShortHistoryValidator>, IComparable<ConfigShortHistory>, IConfigAcceptVisitor, IConfigShortHistory // Class.tt Line: 6
    {
        public partial class ConfigShortHistoryValidator : ValidatorBase<ConfigShortHistory, ConfigShortHistoryValidator> { } // Class.tt Line: 8
        #region CTOR
        public ConfigShortHistory(ITreeConfigNode parent) 
            : base(parent, ConfigShortHistoryValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.CurrentConfig = new Config(this); // Class.tt Line: 28
            this.PrevStableConfig = new Config(this); // Class.tt Line: 28
            this.OldStableConfig = new Config(this); // Class.tt Line: 28
            this.OnInit();
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
            ConfigShortHistory vm = new ConfigShortHistory(parent);
            vm.IsNotNotifying = true;
            if (isDeep) // Clone.tt Line: 60
                vm.CurrentConfig = Config.Clone(vm, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.PrevStableConfig = Config.Clone(vm, from.PrevStableConfig, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.OldStableConfig = Config.Clone(vm, from.OldStableConfig, isDeep);
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(ConfigShortHistory to, ConfigShortHistory from, bool isDeep = true) // Clone.tt Line: 74
        {
            if (isDeep) // Clone.tt Line: 133
                Config.Update(to.CurrentConfig, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 133
                Config.Update(to.PrevStableConfig, from.PrevStableConfig, isDeep);
            if (isDeep) // Clone.tt Line: 133
                Config.Update(to.OldStableConfig, from.OldStableConfig, isDeep);
        }
        // Clone.tt Line: 142
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
        public static ConfigShortHistory ConvertToVM(Proto.Config.proto_config_short_history m, ConfigShortHistory vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            if (vm.CurrentConfig == null) // Clone.tt Line: 206
                vm.CurrentConfig = new Config(vm); // Clone.tt Line: 208
            Config.ConvertToVM(m.CurrentConfig, vm.CurrentConfig); // Clone.tt Line: 212
            if (vm.PrevStableConfig == null) // Clone.tt Line: 206
                vm.PrevStableConfig = new Config(vm); // Clone.tt Line: 208
            Config.ConvertToVM(m.PrevStableConfig, vm.PrevStableConfig); // Clone.tt Line: 212
            if (vm.OldStableConfig == null) // Clone.tt Line: 206
                vm.OldStableConfig = new Config(vm); // Clone.tt Line: 208
            Config.ConvertToVM(m.OldStableConfig, vm.OldStableConfig); // Clone.tt Line: 212
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'ConfigShortHistory' to 'proto_config_short_history'
        public static Proto.Config.proto_config_short_history ConvertToProto(ConfigShortHistory vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_config_short_history m = new Proto.Config.proto_config_short_history(); // Clone.tt Line: 230
            m.CurrentConfig = Config.ConvertToProto(vm.CurrentConfig); // Clone.tt Line: 261
            m.PrevStableConfig = Config.ConvertToProto(vm.PrevStableConfig); // Clone.tt Line: 261
            m.OldStableConfig = Config.ConvertToProto(vm.OldStableConfig); // Clone.tt Line: 261
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.CurrentConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.PrevStableConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.OldStableConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public Config CurrentConfig // Property.tt Line: 110
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
        partial void OnCurrentConfigChanging(ref Config to); // Property.tt Line: 131
        partial void OnCurrentConfigChanged();
        IConfig IConfigShortHistory.CurrentConfig { get { return this._CurrentConfig; } }
        
        public Config PrevStableConfig // Property.tt Line: 110
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
        partial void OnPrevStableConfigChanging(ref Config to); // Property.tt Line: 131
        partial void OnPrevStableConfigChanged();
        IConfig IConfigShortHistory.PrevStableConfig { get { return this._PrevStableConfig; } }
        
        public Config OldStableConfig // Property.tt Line: 110
        { 
            get 
            { 
                return this._OldStableConfig; 
            }
            set
            {
                if (this._OldStableConfig != value)
                {
                    this.OnOldStableConfigChanging(ref value);
                    this._OldStableConfig = value;
                    this.OnOldStableConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _OldStableConfig;
        partial void OnOldStableConfigChanging(ref Config to); // Property.tt Line: 131
        partial void OnOldStableConfigChanged();
        IConfig IConfigShortHistory.OldStableConfig { get { return this._OldStableConfig; } }
    
        #endregion Properties
    }
    public partial class GroupListBaseConfigLinks : ConfigObjectVmGenSettings<GroupListBaseConfigLinks, GroupListBaseConfigLinks.GroupListBaseConfigLinksValidator>, IComparable<GroupListBaseConfigLinks>, IConfigAcceptVisitor, IGroupListBaseConfigLinks // Class.tt Line: 6
    {
        public partial class GroupListBaseConfigLinksValidator : ValidatorBase<GroupListBaseConfigLinks, GroupListBaseConfigLinksValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListBaseConfigLinks(ITreeConfigNode parent) 
            : base(parent, GroupListBaseConfigLinksValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListBaseConfigLinks vm = new GroupListBaseConfigLinks(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListBaseConfigLinks) // Clone.tt Line: 50
                vm.ListBaseConfigLinks.Add(BaseConfigLink.Clone(vm, (BaseConfigLink)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListBaseConfigLinks to, GroupListBaseConfigLinks from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new BaseConfigLink(to); // Clone.tt Line: 112
                        BaseConfigLink.Update(p, (BaseConfigLink)tt, isDeep);
                        to.ListBaseConfigLinks.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListBaseConfigLinks ConvertToVM(Proto.Config.proto_group_list_base_config_links m, GroupListBaseConfigLinks vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListBaseConfigLinks) // Clone.tt Line: 194
            {
                var tvm = BaseConfigLink.ConvertToVM(t, new BaseConfigLink(vm)); // Clone.tt Line: 197
                vm.ListBaseConfigLinks.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListBaseConfigLinks' to 'proto_group_list_base_config_links'
        public static Proto.Config.proto_group_list_base_config_links ConvertToProto(GroupListBaseConfigLinks vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_base_config_links m = new Proto.Config.proto_group_list_base_config_links(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListBaseConfigLinks) // Clone.tt Line: 233
                m.ListBaseConfigLinks.Add(BaseConfigLink.ConvertToProto((BaseConfigLink)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListBaseConfigLinks.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<BaseConfigLink> ListBaseConfigLinks // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListBaseConfigLinks; 
            }
            set
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
            this.ListBaseConfigLinks.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<BaseConfigLink> items) 
        { 
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
            this.ListBaseConfigLinks.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class BaseConfigLink : ConfigObjectVmGenSettings<BaseConfigLink, BaseConfigLink.BaseConfigLinkValidator>, IComparable<BaseConfigLink>, IConfigAcceptVisitor, IBaseConfigLink // Class.tt Line: 6
    {
        public partial class BaseConfigLinkValidator : ValidatorBase<BaseConfigLink, BaseConfigLinkValidator> { } // Class.tt Line: 8
        #region CTOR
        public BaseConfigLink(ITreeConfigNode parent) 
            : base(parent, BaseConfigLinkValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.Config = new Config(this); // Class.tt Line: 28
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            BaseConfigLink vm = new BaseConfigLink(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.Config = Config.Clone(vm, from.Config, isDeep);
            vm.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 63
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(BaseConfigLink to, BaseConfigLink from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                Config.Update(to.Config, from.Config, isDeep);
            to.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static BaseConfigLink ConvertToVM(Proto.Config.proto_base_config_link m, BaseConfigLink vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.Config == null) // Clone.tt Line: 206
                vm.Config = new Config(vm); // Clone.tt Line: 208
            Config.ConvertToVM(m.Config, vm.Config); // Clone.tt Line: 212
            vm.RelativeConfigFilePath = m.RelativeConfigFilePath; // Clone.tt Line: 214
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'BaseConfigLink' to 'proto_base_config_link'
        public static Proto.Config.proto_base_config_link ConvertToProto(BaseConfigLink vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_base_config_link m = new Proto.Config.proto_base_config_link(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.Config = Config.ConvertToProto(vm.Config); // Clone.tt Line: 261
            m.RelativeConfigFilePath = vm.RelativeConfigFilePath; // Clone.tt Line: 267
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.Config.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IBaseConfigLink.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public Config Config // Property.tt Line: 110
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
        partial void OnConfigChanging(ref Config to); // Property.tt Line: 131
        partial void OnConfigChanged();
        IConfig IBaseConfigLink.Config { get { return this._Config; } }
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorFilePicker), typeof(ITypeEditor))]
        public string RelativeConfigFilePath // Property.tt Line: 135
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
        partial void OnRelativeConfigFilePathChanging(ref string to); // Property.tt Line: 157
        partial void OnRelativeConfigFilePathChanged();
        string IBaseConfigLink.RelativeConfigFilePath { get { return this._RelativeConfigFilePath; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    public partial class Config : ConfigObjectVmGenSettings<Config, Config.ConfigValidator>, IComparable<Config>, IConfigAcceptVisitor, IConfig // Class.tt Line: 6
    {
        public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { } // Class.tt Line: 8
        #region CTOR
        public Config(ITreeConfigNode parent) 
            : base(parent, ConfigValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DbSettings = new DbSettings(); // Class.tt Line: 26
            this.GroupConfigLinks = new GroupListBaseConfigLinks(this); // Class.tt Line: 28
            this.Model = new ConfigModel(this); // Class.tt Line: 28
            this.GroupPlugins = new GroupListPlugins(this); // Class.tt Line: 28
            this.GroupAppSolutions = new GroupListAppSolutions(this); // Class.tt Line: 28
            this.OnInit();
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
            Config vm = new Config(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Version = from.Version; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.LastUpdated = from.LastUpdated; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.DbSettings = DbSettings.Clone(from.DbSettings, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupConfigLinks = GroupListBaseConfigLinks.Clone(vm, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.Model = ConfigModel.Clone(vm, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupPlugins = GroupListPlugins.Clone(vm, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupAppSolutions = GroupListAppSolutions.Clone(vm, from.GroupAppSolutions, isDeep);
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Config to, Config from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Version = from.Version; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.LastUpdated = from.LastUpdated; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                DbSettings.Update(to.DbSettings, from.DbSettings, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListBaseConfigLinks.Update(to.GroupConfigLinks, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 133
                ConfigModel.Update(to.Model, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListPlugins.Update(to.GroupPlugins, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListAppSolutions.Update(to.GroupAppSolutions, from.GroupAppSolutions, isDeep);
        }
        // Clone.tt Line: 142
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
        public static Config ConvertToVM(Proto.Config.proto_config m, Config vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Version = m.Version; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.LastUpdated = m.LastUpdated; // Clone.tt Line: 214
            if (vm.DbSettings == null) // Clone.tt Line: 206
                vm.DbSettings = new DbSettings(); // Clone.tt Line: 210
            DbSettings.ConvertToVM(m.DbSettings, vm.DbSettings); // Clone.tt Line: 212
            if (vm.GroupConfigLinks == null) // Clone.tt Line: 206
                vm.GroupConfigLinks = new GroupListBaseConfigLinks(vm); // Clone.tt Line: 208
            GroupListBaseConfigLinks.ConvertToVM(m.GroupConfigLinks, vm.GroupConfigLinks); // Clone.tt Line: 212
            if (vm.Model == null) // Clone.tt Line: 206
                vm.Model = new ConfigModel(vm); // Clone.tt Line: 208
            ConfigModel.ConvertToVM(m.Model, vm.Model); // Clone.tt Line: 212
            if (vm.GroupPlugins == null) // Clone.tt Line: 206
                vm.GroupPlugins = new GroupListPlugins(vm); // Clone.tt Line: 208
            GroupListPlugins.ConvertToVM(m.GroupPlugins, vm.GroupPlugins); // Clone.tt Line: 212
            if (vm.GroupAppSolutions == null) // Clone.tt Line: 206
                vm.GroupAppSolutions = new GroupListAppSolutions(vm); // Clone.tt Line: 208
            GroupListAppSolutions.ConvertToVM(m.GroupAppSolutions, vm.GroupAppSolutions); // Clone.tt Line: 212
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Config' to 'proto_config'
        public static Proto.Config.proto_config ConvertToProto(Config vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_config m = new Proto.Config.proto_config(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Version = vm.Version; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.LastUpdated = vm.LastUpdated; // Clone.tt Line: 267
            m.DbSettings = DbSettings.ConvertToProto(vm.DbSettings); // Clone.tt Line: 261
            m.GroupConfigLinks = GroupListBaseConfigLinks.ConvertToProto(vm.GroupConfigLinks); // Clone.tt Line: 261
            m.Model = ConfigModel.ConvertToProto(vm.Model); // Clone.tt Line: 261
            m.GroupPlugins = GroupListPlugins.ConvertToProto(vm.GroupPlugins); // Clone.tt Line: 261
            m.GroupAppSolutions = GroupListAppSolutions.ConvertToProto(vm.GroupAppSolutions); // Clone.tt Line: 261
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupConfigLinks.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.Model.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupPlugins.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupAppSolutions.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(4)]
        [ReadOnly(true)]
        public int Version // Property.tt Line: 135
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
        partial void OnVersionChanging(ref int to); // Property.tt Line: 157
        partial void OnVersionChanged();
        int IConfig.Version { get { return this._Version; } }
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IConfig.Description { get { return this._Description; } }
        
        [PropertyOrderAttribute(6)]
        public Google.Protobuf.WellKnownTypes.Timestamp LastUpdated // Property.tt Line: 135
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
        partial void OnLastUpdatedChanging(ref Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 157
        partial void OnLastUpdatedChanged();
        Google.Protobuf.WellKnownTypes.Timestamp IConfig.LastUpdated { get { return this._LastUpdated; } }
        
        
        ///////////////////////////////////////////////////
        /// GENERAL DB SETTINGS
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        public DbSettings DbSettings // Property.tt Line: 110
        { 
            get 
            { 
                return this._DbSettings; 
            }
            set
            {
                if (this._DbSettings != value)
                {
                    this.OnDbSettingsChanging(ref value);
                    this._DbSettings = value;
                    this.OnDbSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private DbSettings _DbSettings;
        partial void OnDbSettingsChanging(ref DbSettings to); // Property.tt Line: 131
        partial void OnDbSettingsChanged();
        IDbSettings IConfig.DbSettings { get { return this._DbSettings; } }
        
        [BrowsableAttribute(false)]
        public GroupListBaseConfigLinks GroupConfigLinks // Property.tt Line: 110
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
        partial void OnGroupConfigLinksChanging(ref GroupListBaseConfigLinks to); // Property.tt Line: 131
        partial void OnGroupConfigLinksChanged();
        IGroupListBaseConfigLinks IConfig.GroupConfigLinks { get { return this._GroupConfigLinks; } }
        
        [BrowsableAttribute(false)]
        public ConfigModel Model // Property.tt Line: 110
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
        partial void OnModelChanging(ref ConfigModel to); // Property.tt Line: 131
        partial void OnModelChanged();
        IConfigModel IConfig.Model { get { return this._Model; } }
        
        [BrowsableAttribute(false)]
        public GroupListPlugins GroupPlugins // Property.tt Line: 110
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
        partial void OnGroupPluginsChanging(ref GroupListPlugins to); // Property.tt Line: 131
        partial void OnGroupPluginsChanged();
        IGroupListPlugins IConfig.GroupPlugins { get { return this._GroupPlugins; } }
        
        [BrowsableAttribute(false)]
        public GroupListAppSolutions GroupAppSolutions // Property.tt Line: 110
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
        partial void OnGroupAppSolutionsChanging(ref GroupListAppSolutions to); // Property.tt Line: 131
        partial void OnGroupAppSolutionsChanged();
        IGroupListAppSolutions IConfig.GroupAppSolutions { get { return this._GroupAppSolutions; } }
    
        #endregion Properties
    }
    public partial class AppDbSettings : ConfigObjectVmBase<AppDbSettings, AppDbSettings.AppDbSettingsValidator>, IComparable<AppDbSettings>, IConfigAcceptVisitor, IAppDbSettings // Class.tt Line: 6
    {
        public partial class AppDbSettingsValidator : ValidatorBase<AppDbSettings, AppDbSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppDbSettings(ITreeConfigNode parent) 
            : base(parent, AppDbSettingsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static AppDbSettings Clone(ITreeConfigNode parent, AppDbSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            AppDbSettings vm = new AppDbSettings(parent);
            vm.IsNotNotifying = true;
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 63
            vm.PluginName = from.PluginName; // Clone.tt Line: 63
            vm.Version = from.Version; // Clone.tt Line: 63
            vm.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 63
            vm.PluginGenName = from.PluginGenName; // Clone.tt Line: 63
            vm.ConnGuid = from.ConnGuid; // Clone.tt Line: 63
            vm.ConnName = from.ConnName; // Clone.tt Line: 63
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(AppDbSettings to, AppDbSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.PluginGuid = from.PluginGuid; // Clone.tt Line: 136
            to.PluginName = from.PluginName; // Clone.tt Line: 136
            to.Version = from.Version; // Clone.tt Line: 136
            to.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 136
            to.PluginGenName = from.PluginGenName; // Clone.tt Line: 136
            to.ConnGuid = from.ConnGuid; // Clone.tt Line: 136
            to.ConnName = from.ConnName; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
        #region IEditable
        public override AppDbSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return AppDbSettings.Clone(this.Parent, this);
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
        public static AppDbSettings ConvertToVM(Proto.Config.proto_app_db_settings m, AppDbSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 214
            vm.PluginName = m.PluginName; // Clone.tt Line: 214
            vm.Version = m.Version; // Clone.tt Line: 214
            vm.PluginGenGuid = m.PluginGenGuid; // Clone.tt Line: 214
            vm.PluginGenName = m.PluginGenName; // Clone.tt Line: 214
            vm.ConnGuid = m.ConnGuid; // Clone.tt Line: 214
            vm.ConnName = m.ConnName; // Clone.tt Line: 214
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'AppDbSettings' to 'proto_app_db_settings'
        public static Proto.Config.proto_app_db_settings ConvertToProto(AppDbSettings vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_app_db_settings m = new Proto.Config.proto_app_db_settings(); // Clone.tt Line: 230
            m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 267
            m.PluginName = vm.PluginName; // Clone.tt Line: 267
            m.Version = vm.Version; // Clone.tt Line: 267
            m.PluginGenGuid = vm.PluginGenGuid; // Clone.tt Line: 267
            m.PluginGenName = vm.PluginGenName; // Clone.tt Line: 267
            m.ConnGuid = vm.ConnGuid; // Clone.tt Line: 267
            m.ConnName = vm.ConnName; // Clone.tt Line: 267
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string PluginGuid // Property.tt Line: 135
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
        partial void OnPluginGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnPluginGuidChanged();
        string IAppDbSettings.PluginGuid { get { return this._PluginGuid; } }
        
        [PropertyOrderAttribute(2)]
        [ReadOnly(true)]
        public string PluginName // Property.tt Line: 135
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
        partial void OnPluginNameChanging(ref string to); // Property.tt Line: 157
        partial void OnPluginNameChanged();
        string IAppDbSettings.PluginName { get { return this._PluginName; } }
        
        [PropertyOrderAttribute(3)]
        [ReadOnly(true)]
        public string Version // Property.tt Line: 135
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
        partial void OnVersionChanging(ref string to); // Property.tt Line: 157
        partial void OnVersionChanged();
        string IAppDbSettings.Version { get { return this._Version; } }
        
        [PropertyOrderAttribute(4)]
        [Editor(typeof(EditorDbPluginGenSelection), typeof(EditorDbPluginGenSelection))]
        [Description("Default DB Plugin generator")]
        public string PluginGenGuid // Property.tt Line: 135
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
        partial void OnPluginGenGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnPluginGenGuidChanged();
        string IAppDbSettings.PluginGenGuid { get { return this._PluginGenGuid; } }
        
        [PropertyOrderAttribute(5)]
        [ReadOnly(true)]
        public string PluginGenName // Property.tt Line: 135
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
        partial void OnPluginGenNameChanging(ref string to); // Property.tt Line: 157
        partial void OnPluginGenNameChanged();
        string IAppDbSettings.PluginGenName { get { return this._PluginGenName; } }
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorDbConnSelection), typeof(EditorDbConnSelection))]
        [Description("Default DB connection string")]
        public string ConnGuid // Property.tt Line: 135
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
        partial void OnConnGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnConnGuidChanged();
        string IAppDbSettings.ConnGuid { get { return this._ConnGuid; } }
        
        [PropertyOrderAttribute(7)]
        [ReadOnly(true)]
        public string ConnName // Property.tt Line: 135
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
        partial void OnConnNameChanging(ref string to); // Property.tt Line: 157
        partial void OnConnNameChanged();
        string IAppDbSettings.ConnName { get { return this._ConnName; } }
    
        #endregion Properties
    }
    public partial class GroupListAppSolutions : ConfigObjectVmBase<GroupListAppSolutions, GroupListAppSolutions.GroupListAppSolutionsValidator>, IComparable<GroupListAppSolutions>, IConfigAcceptVisitor, IGroupListAppSolutions // Class.tt Line: 6
    {
        public partial class GroupListAppSolutionsValidator : ValidatorBase<GroupListAppSolutions, GroupListAppSolutionsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListAppSolutions(ITreeConfigNode parent) 
            : base(parent, GroupListAppSolutionsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DefaultDb = new AppDbSettings(this); // Class.tt Line: 28
            this.ListAppSolutions = new ConfigNodesCollection<AppSolution>(this); // Class.tt Line: 22
            this.OnInit();
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
        public static GroupListAppSolutions Clone(ITreeConfigNode parent, GroupListAppSolutions from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListAppSolutions vm = new GroupListAppSolutions(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.DefaultDb = AppDbSettings.Clone(vm, from.DefaultDb, isDeep);
            vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListAppSolutions) // Clone.tt Line: 50
                vm.ListAppSolutions.Add(AppSolution.Clone(vm, (AppSolution)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListAppSolutions to, GroupListAppSolutions from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new AppSolution(to); // Clone.tt Line: 112
                        AppSolution.Update(p, (AppSolution)tt, isDeep);
                        to.ListAppSolutions.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListAppSolutions ConvertToVM(Proto.Config.proto_group_list_app_solutions m, GroupListAppSolutions vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.DefaultDb == null) // Clone.tt Line: 206
                vm.DefaultDb = new AppDbSettings(vm); // Clone.tt Line: 208
            AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 212
            vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListAppSolutions) // Clone.tt Line: 194
            {
                var tvm = AppSolution.ConvertToVM(t, new AppSolution(vm)); // Clone.tt Line: 197
                vm.ListAppSolutions.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListAppSolutions' to 'proto_group_list_app_solutions'
        public static Proto.Config.proto_group_list_app_solutions ConvertToProto(GroupListAppSolutions vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_app_solutions m = new Proto.Config.proto_group_list_app_solutions(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 261
            foreach (var t in vm.ListAppSolutions) // Clone.tt Line: 233
                m.ListAppSolutions.Add(AppSolution.ConvertToProto((AppSolution)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.DefaultDb.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListAppSolutions)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(2)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListAppSolutions.Description { get { return this._Description; } }
        
        [PropertyOrderAttribute(3)]
        [ExpandableObjectAttribute()]
        [DisplayName("Default DB")]
        public AppDbSettings DefaultDb // Property.tt Line: 110
        { 
            get 
            { 
                return this._DefaultDb; 
            }
            set
            {
                if (this._DefaultDb != value)
                {
                    this.OnDefaultDbChanging(ref value);
                    this._DefaultDb = value;
                    this.OnDefaultDbChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private AppDbSettings _DefaultDb;
        partial void OnDefaultDbChanging(ref AppDbSettings to); // Property.tt Line: 131
        partial void OnDefaultDbChanged();
        IAppDbSettings IGroupListAppSolutions.DefaultDb { get { return this._DefaultDb; } }
        
        
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
            set
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
            this.ListAppSolutions.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<AppSolution> items) 
        { 
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
            this.ListAppSolutions.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
    
        #endregion Properties
    }
    public partial class AppSolution : ConfigObjectVmBase<AppSolution, AppSolution.AppSolutionValidator>, IComparable<AppSolution>, IConfigAcceptVisitor, IAppSolution // Class.tt Line: 6
    {
        public partial class AppSolutionValidator : ValidatorBase<AppSolution, AppSolutionValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppSolution(ITreeConfigNode parent) 
            : base(parent, AppSolutionValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DefaultDb = new AppDbSettings(this); // Class.tt Line: 28
            this.ListAppProjects = new ConfigNodesCollection<AppProject>(this); // Class.tt Line: 22
            this.OnInit();
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
        }
        public static AppSolution Clone(ITreeConfigNode parent, AppSolution from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            AppSolution vm = new AppSolution(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.DefaultDb = AppDbSettings.Clone(vm, from.DefaultDb, isDeep);
            vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListAppProjects) // Clone.tt Line: 50
                vm.ListAppProjects.Add(AppProject.Clone(vm, (AppProject)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(AppSolution to, AppSolution from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new AppProject(to); // Clone.tt Line: 112
                        AppProject.Update(p, (AppProject)tt, isDeep);
                        to.ListAppProjects.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static AppSolution ConvertToVM(Proto.Config.proto_app_solution m, AppSolution vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.RelativeAppSolutionPath = m.RelativeAppSolutionPath; // Clone.tt Line: 214
            if (vm.DefaultDb == null) // Clone.tt Line: 206
                vm.DefaultDb = new AppDbSettings(vm); // Clone.tt Line: 208
            AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 212
            vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListAppProjects) // Clone.tt Line: 194
            {
                var tvm = AppProject.ConvertToVM(t, new AppProject(vm)); // Clone.tt Line: 197
                vm.ListAppProjects.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'AppSolution' to 'proto_app_solution'
        public static Proto.Config.proto_app_solution ConvertToProto(AppSolution vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_app_solution m = new Proto.Config.proto_app_solution(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.RelativeAppSolutionPath = vm.RelativeAppSolutionPath; // Clone.tt Line: 267
            m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 261
            foreach (var t in vm.ListAppProjects) // Clone.tt Line: 233
                m.ListAppProjects.Add(AppProject.ConvertToProto((AppProject)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.DefaultDb.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListAppProjects)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
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
        public string RelativeAppSolutionPath // Property.tt Line: 135
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
        partial void OnRelativeAppSolutionPathChanging(ref string to); // Property.tt Line: 157
        partial void OnRelativeAppSolutionPathChanged();
        string IAppSolution.RelativeAppSolutionPath { get { return this._RelativeAppSolutionPath; } }
        
        [PropertyOrderAttribute(8)]
        [ExpandableObjectAttribute()]
        [DisplayName("Default DB")]
        [Description("Database connection. If empty, all solutions settings are used")]
        public AppDbSettings DefaultDb // Property.tt Line: 110
        { 
            get 
            { 
                return this._DefaultDb; 
            }
            set
            {
                if (this._DefaultDb != value)
                {
                    this.OnDefaultDbChanging(ref value);
                    this._DefaultDb = value;
                    this.OnDefaultDbChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private AppDbSettings _DefaultDb;
        partial void OnDefaultDbChanging(ref AppDbSettings to); // Property.tt Line: 131
        partial void OnDefaultDbChanged();
        IAppDbSettings IAppSolution.DefaultDb { get { return this._DefaultDb; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProject> ListAppProjects // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListAppProjects; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class AppProject : ConfigObjectVmBase<AppProject, AppProject.AppProjectValidator>, IComparable<AppProject>, IConfigAcceptVisitor, IAppProject // Class.tt Line: 6
    {
        public partial class AppProjectValidator : ValidatorBase<AppProject, AppProjectValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppProject(ITreeConfigNode parent) 
            : base(parent, AppProjectValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DefaultDb = new AppDbSettings(this); // Class.tt Line: 28
            this.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(this); // Class.tt Line: 22
            this.OnInit();
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
            AppProject vm = new AppProject(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.DefaultDb = AppDbSettings.Clone(vm, from.DefaultDb, isDeep);
            vm.Namespace = from.Namespace; // Clone.tt Line: 63
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListAppProjectGenerators) // Clone.tt Line: 50
                vm.ListAppProjectGenerators.Add(AppProjectGenerator.Clone(vm, (AppProjectGenerator)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(AppProject to, AppProject from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
            to.Namespace = from.Namespace; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new AppProjectGenerator(to); // Clone.tt Line: 112
                        AppProjectGenerator.Update(p, (AppProjectGenerator)tt, isDeep);
                        to.ListAppProjectGenerators.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static AppProject ConvertToVM(Proto.Config.proto_app_project m, AppProject vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.RelativeAppProjectPath = m.RelativeAppProjectPath; // Clone.tt Line: 214
            if (vm.DefaultDb == null) // Clone.tt Line: 206
                vm.DefaultDb = new AppDbSettings(vm); // Clone.tt Line: 208
            AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 212
            vm.Namespace = m.Namespace; // Clone.tt Line: 214
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListAppProjectGenerators) // Clone.tt Line: 194
            {
                var tvm = AppProjectGenerator.ConvertToVM(t, new AppProjectGenerator(vm)); // Clone.tt Line: 197
                vm.ListAppProjectGenerators.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'AppProject' to 'proto_app_project'
        public static Proto.Config.proto_app_project ConvertToProto(AppProject vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_app_project m = new Proto.Config.proto_app_project(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.RelativeAppProjectPath = vm.RelativeAppProjectPath; // Clone.tt Line: 267
            m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 261
            m.Namespace = vm.Namespace; // Clone.tt Line: 267
            foreach (var t in vm.ListAppProjectGenerators) // Clone.tt Line: 233
                m.ListAppProjectGenerators.Add(AppProjectGenerator.ConvertToProto((AppProjectGenerator)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.DefaultDb.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListAppProjectGenerators)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IAppProject.Description { get { return this._Description; } }
        
        
        ///////////////////////////////////////////////////
        /// App project relative path to .net solution file path
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorProjectPicker), typeof(ITypeEditor))]
        [Description(".NET project file path relative to solution file path")]
        public string RelativeAppProjectPath // Property.tt Line: 135
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
        partial void OnRelativeAppProjectPathChanging(ref string to); // Property.tt Line: 157
        partial void OnRelativeAppProjectPathChanged();
        string IAppProject.RelativeAppProjectPath { get { return this._RelativeAppProjectPath; } }
        
        [PropertyOrderAttribute(8)]
        [ExpandableObjectAttribute()]
        [DisplayName("Default DB")]
        [Description("Database connection. If empty, solution settings are used")]
        public AppDbSettings DefaultDb // Property.tt Line: 110
        { 
            get 
            { 
                return this._DefaultDb; 
            }
            set
            {
                if (this._DefaultDb != value)
                {
                    this.OnDefaultDbChanging(ref value);
                    this._DefaultDb = value;
                    this.OnDefaultDbChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private AppDbSettings _DefaultDb;
        partial void OnDefaultDbChanging(ref AppDbSettings to); // Property.tt Line: 131
        partial void OnDefaultDbChanged();
        IAppDbSettings IAppProject.DefaultDb { get { return this._DefaultDb; } }
        
        [PropertyOrderAttribute(9)]
        [DisplayName("Namespace")]
        [Description("Project namespace name")]
        public string Namespace // Property.tt Line: 135
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
        partial void OnNamespaceChanging(ref string to); // Property.tt Line: 157
        partial void OnNamespaceChanged();
        string IAppProject.Namespace { get { return this._Namespace; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProjectGenerator> ListAppProjectGenerators // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListAppProjectGenerators; 
            }
            set
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
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Stored in each node in ConfigModel branch
    ///////////////////////////////////////////////////
    public partial class PluginGeneratorNodeSettings : ConfigObjectVmBase<PluginGeneratorNodeSettings, PluginGeneratorNodeSettings.PluginGeneratorNodeSettingsValidator>, IComparable<PluginGeneratorNodeSettings>, IConfigAcceptVisitor, IPluginGeneratorNodeSettings // Class.tt Line: 6
    {
        public partial class PluginGeneratorNodeSettingsValidator : ValidatorBase<PluginGeneratorNodeSettings, PluginGeneratorNodeSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public PluginGeneratorNodeSettings(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorNodeSettingsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.OnInit();
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
            PluginGeneratorNodeSettings vm = new PluginGeneratorNodeSettings(parent);
            vm.IsNotNotifying = true;
            vm.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 63
            vm.NodeSettingsVmGuid = from.NodeSettingsVmGuid; // Clone.tt Line: 63
            vm.Settings = from.Settings; // Clone.tt Line: 63
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(PluginGeneratorNodeSettings to, PluginGeneratorNodeSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 136
            to.NodeSettingsVmGuid = from.NodeSettingsVmGuid; // Clone.tt Line: 136
            to.Settings = from.Settings; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
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
        public static PluginGeneratorNodeSettings ConvertToVM(Proto.Config.proto_plugin_generator_node_settings m, PluginGeneratorNodeSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.AppProjectGeneratorGuid = m.AppProjectGeneratorGuid; // Clone.tt Line: 214
            vm.NodeSettingsVmGuid = m.NodeSettingsVmGuid; // Clone.tt Line: 214
            vm.Settings = m.Settings; // Clone.tt Line: 214
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'PluginGeneratorNodeSettings' to 'proto_plugin_generator_node_settings'
        public static Proto.Config.proto_plugin_generator_node_settings ConvertToProto(PluginGeneratorNodeSettings vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_plugin_generator_node_settings m = new Proto.Config.proto_plugin_generator_node_settings(); // Clone.tt Line: 230
            m.AppProjectGeneratorGuid = vm.AppProjectGeneratorGuid; // Clone.tt Line: 267
            m.NodeSettingsVmGuid = vm.NodeSettingsVmGuid; // Clone.tt Line: 267
            m.Settings = vm.Settings; // Clone.tt Line: 267
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string AppProjectGeneratorGuid // Property.tt Line: 135
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
        partial void OnAppProjectGeneratorGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnAppProjectGeneratorGuidChanged();
        string IPluginGeneratorNodeSettings.AppProjectGeneratorGuid { get { return this._AppProjectGeneratorGuid; } }
        
        
        ///////////////////////////////////////////////////
        /// Name of solution-project-generator node
        /// string name = 2;
        ///////////////////////////////////////////////////
        public string NodeSettingsVmGuid // Property.tt Line: 135
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
        partial void OnNodeSettingsVmGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnNodeSettingsVmGuidChanged();
        string IPluginGeneratorNodeSettings.NodeSettingsVmGuid { get { return this._NodeSettingsVmGuid; } }
        
        public string Settings // Property.tt Line: 135
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
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 157
        partial void OnSettingsChanged();
        string IPluginGeneratorNodeSettings.Settings { get { return this._Settings; } }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Stored in AppProjectGenerator node
    ///////////////////////////////////////////////////
    public partial class PluginGeneratorMainSettings : ConfigObjectVmBase<PluginGeneratorMainSettings, PluginGeneratorMainSettings.PluginGeneratorMainSettingsValidator>, IComparable<PluginGeneratorMainSettings>, IConfigAcceptVisitor, IPluginGeneratorMainSettings // Class.tt Line: 6
    {
        public partial class PluginGeneratorMainSettingsValidator : ValidatorBase<PluginGeneratorMainSettings, PluginGeneratorMainSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public PluginGeneratorMainSettings(ITreeConfigNode parent) 
            : base(parent, PluginGeneratorMainSettingsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.OnInit();
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
            PluginGeneratorMainSettings vm = new PluginGeneratorMainSettings(parent);
            vm.IsNotNotifying = true;
            vm.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 63
            vm.Settings = from.Settings; // Clone.tt Line: 63
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(PluginGeneratorMainSettings to, PluginGeneratorMainSettings from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.AppProjectGeneratorGuid = from.AppProjectGeneratorGuid; // Clone.tt Line: 136
            to.Settings = from.Settings; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
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
        public static PluginGeneratorMainSettings ConvertToVM(Proto.Config.proto_plugin_generator_main_settings m, PluginGeneratorMainSettings vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.AppProjectGeneratorGuid = m.AppProjectGeneratorGuid; // Clone.tt Line: 214
            vm.Settings = m.Settings; // Clone.tt Line: 214
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'PluginGeneratorMainSettings' to 'proto_plugin_generator_main_settings'
        public static Proto.Config.proto_plugin_generator_main_settings ConvertToProto(PluginGeneratorMainSettings vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_plugin_generator_main_settings m = new Proto.Config.proto_plugin_generator_main_settings(); // Clone.tt Line: 230
            m.AppProjectGeneratorGuid = vm.AppProjectGeneratorGuid; // Clone.tt Line: 267
            m.Settings = vm.Settings; // Clone.tt Line: 267
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string AppProjectGeneratorGuid // Property.tt Line: 135
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
        partial void OnAppProjectGeneratorGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnAppProjectGeneratorGuidChanged();
        string IPluginGeneratorMainSettings.AppProjectGeneratorGuid { get { return this._AppProjectGeneratorGuid; } }
        
        public string Settings // Property.tt Line: 135
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
        partial void OnSettingsChanging(ref string to); // Property.tt Line: 157
        partial void OnSettingsChanged();
        string IPluginGeneratorMainSettings.Settings { get { return this._Settings; } }
    
        #endregion Properties
    }
    public partial class AppProjectGenerator : ConfigObjectVmBase<AppProjectGenerator, AppProjectGenerator.AppProjectGeneratorValidator>, IComparable<AppProjectGenerator>, IConfigAcceptVisitor, IAppProjectGenerator // Class.tt Line: 6
    {
        public partial class AppProjectGeneratorValidator : ValidatorBase<AppProjectGenerator, AppProjectGeneratorValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppProjectGenerator(ITreeConfigNode parent) 
            : base(parent, AppProjectGeneratorValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.MainSettings = new PluginGeneratorMainSettings(this); // Class.tt Line: 28
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            // throw new Exception();
        }
        public static AppProjectGenerator Clone(ITreeConfigNode parent, AppProjectGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            AppProjectGenerator vm = new AppProjectGenerator(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 63
            vm.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 63
            vm.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 63
            vm.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 63
            vm.RelativePathToGenFolder = from.RelativePathToGenFolder; // Clone.tt Line: 63
            vm.GenFileName = from.GenFileName; // Clone.tt Line: 63
            vm.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.MainSettings = PluginGeneratorMainSettings.Clone(vm, from.MainSettings, isDeep);
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(AppProjectGenerator to, AppProjectGenerator from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.PluginGuid = from.PluginGuid; // Clone.tt Line: 136
            to.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 136
            to.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 136
            to.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 136
            to.RelativePathToGenFolder = from.RelativePathToGenFolder; // Clone.tt Line: 136
            to.GenFileName = from.GenFileName; // Clone.tt Line: 136
            to.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                PluginGeneratorMainSettings.Update(to.MainSettings, from.MainSettings, isDeep);
        }
        // Clone.tt Line: 142
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
        public static AppProjectGenerator ConvertToVM(Proto.Config.proto_app_project_generator m, AppProjectGenerator vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 214
            vm.DescriptionPlugin = m.DescriptionPlugin; // Clone.tt Line: 214
            vm.PluginGeneratorGuid = m.PluginGeneratorGuid; // Clone.tt Line: 214
            vm.DescriptionGenerator = m.DescriptionGenerator; // Clone.tt Line: 214
            vm.RelativePathToGenFolder = m.RelativePathToGenFolder; // Clone.tt Line: 214
            vm.GenFileName = m.GenFileName; // Clone.tt Line: 214
            vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 214
            if (vm.MainSettings == null) // Clone.tt Line: 206
                vm.MainSettings = new PluginGeneratorMainSettings(vm); // Clone.tt Line: 208
            PluginGeneratorMainSettings.ConvertToVM(m.MainSettings, vm.MainSettings); // Clone.tt Line: 212
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'AppProjectGenerator' to 'proto_app_project_generator'
        public static Proto.Config.proto_app_project_generator ConvertToProto(AppProjectGenerator vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_app_project_generator m = new Proto.Config.proto_app_project_generator(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 267
            m.DescriptionPlugin = vm.DescriptionPlugin; // Clone.tt Line: 267
            m.PluginGeneratorGuid = vm.PluginGeneratorGuid; // Clone.tt Line: 267
            m.DescriptionGenerator = vm.DescriptionGenerator; // Clone.tt Line: 267
            m.RelativePathToGenFolder = vm.RelativePathToGenFolder; // Clone.tt Line: 267
            m.GenFileName = vm.GenFileName; // Clone.tt Line: 267
            m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 267
            m.MainSettings = PluginGeneratorMainSettings.ConvertToProto(vm.MainSettings); // Clone.tt Line: 261
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.MainSettings.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IAppProjectGenerator.Description { get { return this._Description; } }
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Plugin")]
        [Description("Plugins with generators")]
        [Editor(typeof(EditorPluginSelection), typeof(ITypeEditor))]
        public string PluginGuid // Property.tt Line: 135
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
        partial void OnPluginGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnPluginGuidChanged();
        string IAppProjectGenerator.PluginGuid { get { return this._PluginGuid; } }
        
        [PropertyOrderAttribute(5)]
        [DisplayName("Description")]
        [ReadOnly(true)]
        public string DescriptionPlugin // Property.tt Line: 135
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
        partial void OnDescriptionPluginChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionPluginChanged();
        string IAppProjectGenerator.DescriptionPlugin { get { return this._DescriptionPlugin; } }
        
        [PropertyOrderAttribute(6)]
        [DisplayName("Generator")]
        [Description("Plugin generator")]
        [Editor(typeof(EditorPluginGeneratorSelection), typeof(ITypeEditor))]
        public string PluginGeneratorGuid // Property.tt Line: 135
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
        partial void OnPluginGeneratorGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnPluginGeneratorGuidChanged();
        string IAppProjectGenerator.PluginGeneratorGuid { get { return this._PluginGeneratorGuid; } }
        
        [PropertyOrderAttribute(7)]
        [DisplayName("Description")]
        [ReadOnly(true)]
        public string DescriptionGenerator // Property.tt Line: 135
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
        partial void OnDescriptionGeneratorChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionGeneratorChanged();
        string IAppProjectGenerator.DescriptionGenerator { get { return this._DescriptionGenerator; } }
        
        
        ///////////////////////////////////////////////////
        /// Relative folder path to project file
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(8)]
        [DisplayName("Output Folder")]
        [Editor(typeof(EditorFolderPicker), typeof(ITypeEditor))]
        [Description("Get is returning relative folder path to project file")]
        public string RelativePathToGenFolder // Property.tt Line: 135
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
        partial void OnRelativePathToGenFolderChanging(ref string to); // Property.tt Line: 157
        partial void OnRelativePathToGenFolderChanged();
        string IAppProjectGenerator.RelativePathToGenFolder { get { return this._RelativePathToGenFolder; } }
        
        
        ///////////////////////////////////////////////////
        /// Generator output file name
        ///////////////////////////////////////////////////
        [DisplayName("Output File")]
        [PropertyOrderAttribute(9)]
        [Description("Generator output file name")]
        public string GenFileName // Property.tt Line: 135
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
        partial void OnGenFileNameChanging(ref string to); // Property.tt Line: 157
        partial void OnGenFileNameChanged();
        string IAppProjectGenerator.GenFileName { get { return this._GenFileName; } }
        
        [BrowsableAttribute(false)]
        public string GeneratorSettings // Property.tt Line: 135
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
        partial void OnGeneratorSettingsChanging(ref string to); // Property.tt Line: 157
        partial void OnGeneratorSettingsChanged();
        string IAppProjectGenerator.GeneratorSettings { get { return this._GeneratorSettings; } }
        
        
        ///////////////////////////////////////////////////
        /// 
        /// proto_plugin_generator_node_settings node_settings = 17;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public PluginGeneratorMainSettings MainSettings // Property.tt Line: 110
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
        partial void OnMainSettingsChanging(ref PluginGeneratorMainSettings to); // Property.tt Line: 131
        partial void OnMainSettingsChanged();
        IPluginGeneratorMainSettings IAppProjectGenerator.MainSettings { get { return this._MainSettings; } }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    public partial class ConfigModel : ConfigObjectVmGenSettings<ConfigModel, ConfigModel.ConfigModelValidator>, IComparable<ConfigModel>, IConfigAcceptVisitor, IConfigModel // Class.tt Line: 6
    {
        public partial class ConfigModelValidator : ValidatorBase<ConfigModel, ConfigModelValidator> { } // Class.tt Line: 8
        #region CTOR
        public ConfigModel(ITreeConfigNode parent) 
            : base(parent, ConfigModelValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupCommon = new GroupListCommon(this); // Class.tt Line: 28
            this.GroupConstants = new GroupListConstants(this); // Class.tt Line: 28
            this.GroupEnumerations = new GroupListEnumerations(this); // Class.tt Line: 28
            this.GroupCatalogs = new GroupListCatalogs(this); // Class.tt Line: 28
            this.GroupDocuments = new GroupDocuments(this); // Class.tt Line: 28
            this.GroupJournals = new GroupListJournals(this); // Class.tt Line: 28
            this.ListMainGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorMainSettings>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(PluginGeneratorMainSettings)) // Clone.tt Line: 15
            {
                this.ListMainGeneratorsSettings.Sort();
            }
            if (type == typeof(PluginGeneratorNodeSettings)) // Clone.tt Line: 15
            {
                this.ListNodeGeneratorsSettings.Sort();
            }
        }
        public static ConfigModel Clone(ITreeConfigNode parent, ConfigModel from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            ConfigModel vm = new ConfigModel(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Version = from.Version; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.GroupCommon = GroupListCommon.Clone(vm, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupConstants = GroupListConstants.Clone(vm, from.GroupConstants, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupEnumerations = GroupListEnumerations.Clone(vm, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupCatalogs = GroupListCatalogs.Clone(vm, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupDocuments = GroupDocuments.Clone(vm, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupJournals = GroupListJournals.Clone(vm, from.GroupJournals, isDeep);
            vm.ListMainGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorMainSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListMainGeneratorsSettings) // Clone.tt Line: 50
                vm.ListMainGeneratorsSettings.Add(PluginGeneratorMainSettings.Clone(vm, (PluginGeneratorMainSettings)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(ConfigModel to, ConfigModel from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Version = from.Version; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                GroupListCommon.Update(to.GroupCommon, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListConstants.Update(to.GroupConstants, from.GroupConstants, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListEnumerations.Update(to.GroupEnumerations, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListCatalogs.Update(to.GroupCatalogs, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupDocuments.Update(to.GroupDocuments, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListJournals.Update(to.GroupJournals, from.GroupJournals, isDeep);
            if (isDeep) // Clone.tt Line: 81
            {
                foreach (var t in to.ListMainGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListMainGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            PluginGeneratorMainSettings.Update((PluginGeneratorMainSettings)t, (PluginGeneratorMainSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListMainGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListMainGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListMainGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new PluginGeneratorMainSettings(to); // Clone.tt Line: 112
                        PluginGeneratorMainSettings.Update(p, (PluginGeneratorMainSettings)tt, isDeep);
                        to.ListMainGeneratorsSettings.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static ConfigModel ConvertToVM(Proto.Config.proto_config_model m, ConfigModel vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Version = m.Version; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.GroupCommon == null) // Clone.tt Line: 206
                vm.GroupCommon = new GroupListCommon(vm); // Clone.tt Line: 208
            GroupListCommon.ConvertToVM(m.GroupCommon, vm.GroupCommon); // Clone.tt Line: 212
            if (vm.GroupConstants == null) // Clone.tt Line: 206
                vm.GroupConstants = new GroupListConstants(vm); // Clone.tt Line: 208
            GroupListConstants.ConvertToVM(m.GroupConstants, vm.GroupConstants); // Clone.tt Line: 212
            if (vm.GroupEnumerations == null) // Clone.tt Line: 206
                vm.GroupEnumerations = new GroupListEnumerations(vm); // Clone.tt Line: 208
            GroupListEnumerations.ConvertToVM(m.GroupEnumerations, vm.GroupEnumerations); // Clone.tt Line: 212
            if (vm.GroupCatalogs == null) // Clone.tt Line: 206
                vm.GroupCatalogs = new GroupListCatalogs(vm); // Clone.tt Line: 208
            GroupListCatalogs.ConvertToVM(m.GroupCatalogs, vm.GroupCatalogs); // Clone.tt Line: 212
            if (vm.GroupDocuments == null) // Clone.tt Line: 206
                vm.GroupDocuments = new GroupDocuments(vm); // Clone.tt Line: 208
            GroupDocuments.ConvertToVM(m.GroupDocuments, vm.GroupDocuments); // Clone.tt Line: 212
            if (vm.GroupJournals == null) // Clone.tt Line: 206
                vm.GroupJournals = new GroupListJournals(vm); // Clone.tt Line: 208
            GroupListJournals.ConvertToVM(m.GroupJournals, vm.GroupJournals); // Clone.tt Line: 212
            vm.ListMainGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorMainSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListMainGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorMainSettings.ConvertToVM(t, new PluginGeneratorMainSettings(vm)); // Clone.tt Line: 197
                vm.ListMainGeneratorsSettings.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'ConfigModel' to 'proto_config_model'
        public static Proto.Config.proto_config_model ConvertToProto(ConfigModel vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_config_model m = new Proto.Config.proto_config_model(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Version = vm.Version; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.GroupCommon = GroupListCommon.ConvertToProto(vm.GroupCommon); // Clone.tt Line: 261
            m.GroupConstants = GroupListConstants.ConvertToProto(vm.GroupConstants); // Clone.tt Line: 261
            m.GroupEnumerations = GroupListEnumerations.ConvertToProto(vm.GroupEnumerations); // Clone.tt Line: 261
            m.GroupCatalogs = GroupListCatalogs.ConvertToProto(vm.GroupCatalogs); // Clone.tt Line: 261
            m.GroupDocuments = GroupDocuments.ConvertToProto(vm.GroupDocuments); // Clone.tt Line: 261
            m.GroupJournals = GroupListJournals.ConvertToProto(vm.GroupJournals); // Clone.tt Line: 261
            foreach (var t in vm.ListMainGeneratorsSettings) // Clone.tt Line: 233
                m.ListMainGeneratorsSettings.Add(PluginGeneratorMainSettings.ConvertToProto((PluginGeneratorMainSettings)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupCommon.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupConstants.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupEnumerations.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupCatalogs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupDocuments.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupJournals.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListMainGeneratorsSettings)
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
        
        [PropertyOrderAttribute(4)]
        [ReadOnly(true)]
        public int Version // Property.tt Line: 135
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
        partial void OnVersionChanging(ref int to); // Property.tt Line: 157
        partial void OnVersionChanged();
        int IConfigModel.Version { get { return this._Version; } }
        
        [PropertyOrderAttribute(5)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IConfigModel.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public GroupListCommon GroupCommon // Property.tt Line: 110
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
        partial void OnGroupCommonChanging(ref GroupListCommon to); // Property.tt Line: 131
        partial void OnGroupCommonChanged();
        IGroupListCommon IConfigModel.GroupCommon { get { return this._GroupCommon; } }
        
        [BrowsableAttribute(false)]
        public GroupListConstants GroupConstants // Property.tt Line: 110
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
        partial void OnGroupConstantsChanging(ref GroupListConstants to); // Property.tt Line: 131
        partial void OnGroupConstantsChanged();
        IGroupListConstants IConfigModel.GroupConstants { get { return this._GroupConstants; } }
        
        [BrowsableAttribute(false)]
        public GroupListEnumerations GroupEnumerations // Property.tt Line: 110
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
        partial void OnGroupEnumerationsChanging(ref GroupListEnumerations to); // Property.tt Line: 131
        partial void OnGroupEnumerationsChanged();
        IGroupListEnumerations IConfigModel.GroupEnumerations { get { return this._GroupEnumerations; } }
        
        [BrowsableAttribute(false)]
        public GroupListCatalogs GroupCatalogs // Property.tt Line: 110
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
        partial void OnGroupCatalogsChanging(ref GroupListCatalogs to); // Property.tt Line: 131
        partial void OnGroupCatalogsChanged();
        IGroupListCatalogs IConfigModel.GroupCatalogs { get { return this._GroupCatalogs; } }
        
        [BrowsableAttribute(false)]
        public GroupDocuments GroupDocuments // Property.tt Line: 110
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
        partial void OnGroupDocumentsChanging(ref GroupDocuments to); // Property.tt Line: 131
        partial void OnGroupDocumentsChanged();
        IGroupDocuments IConfigModel.GroupDocuments { get { return this._GroupDocuments; } }
        
        [BrowsableAttribute(false)]
        public GroupListJournals GroupJournals // Property.tt Line: 110
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
        partial void OnGroupJournalsChanging(ref GroupListJournals to); // Property.tt Line: 131
        partial void OnGroupJournalsChanged();
        IGroupListJournals IConfigModel.GroupJournals { get { return this._GroupJournals; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorMainSettings> ListMainGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListMainGeneratorsSettings; 
            }
            set
            {
                if (this._ListMainGeneratorsSettings != value)
                {
                    this.OnListMainGeneratorsSettingsChanging(value);
                    this._ListMainGeneratorsSettings = value;
                    this.OnListMainGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorMainSettings> _ListMainGeneratorsSettings;
        partial void OnListMainGeneratorsSettingsChanging(SortedObservableCollection<PluginGeneratorMainSettings> to); // Property.tt Line: 79
        partial void OnListMainGeneratorsSettingsChanged();
        IEnumerable<IPluginGeneratorMainSettings> IConfigModel.ListMainGeneratorsSettings { get { return this._ListMainGeneratorsSettings; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
        IEnumerable<IPluginGeneratorNodeSettings> IConfigModel.ListNodeGeneratorsSettings { get { return this._ListNodeGeneratorsSettings; } }
    
        #endregion Properties
    }
    public partial class DataType : VmValidatableWithSeverity<DataType, DataType.DataTypeValidator>, IDataType // Class.tt Line: 6
    {
        public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { } // Class.tt Line: 8
        #region CTOR
        public DataType() 
            : base(DataTypeValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.ListObjectGuids = new ObservableCollection<string>(); // Class.tt Line: 46
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static DataType Clone(DataType from, bool isDeep = true) // Clone.tt Line: 27
        {
            DataType vm = new DataType();
            vm.IsNotNotifying = true;
            vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 63
            vm.Length = from.Length; // Clone.tt Line: 63
            vm.Accuracy = from.Accuracy; // Clone.tt Line: 63
            vm.IsPositive = from.IsPositive; // Clone.tt Line: 63
            vm.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 63
            vm.IsNullable = from.IsNullable; // Clone.tt Line: 63
            foreach (var t in from.ListObjectGuids) // Clone.tt Line: 42
                vm.ListObjectGuids.Add(t);
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(DataType to, DataType from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 136
            to.Length = from.Length; // Clone.tt Line: 136
            to.Accuracy = from.Accuracy; // Clone.tt Line: 136
            to.IsPositive = from.IsPositive; // Clone.tt Line: 136
            to.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 136
            to.IsNullable = from.IsNullable; // Clone.tt Line: 136
                to.ListObjectGuids.Clear(); // Clone.tt Line: 122
                foreach (var tt in from.ListObjectGuids)
                {
                    to.ListObjectGuids.Add(tt);
                }
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
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
        public static DataType ConvertToVM(Proto.Config.proto_data_type m, DataType vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.DataTypeEnum = (EnumDataType)m.DataTypeEnum; // Clone.tt Line: 214
            vm.Length = m.Length; // Clone.tt Line: 214
            vm.Accuracy = m.Accuracy; // Clone.tt Line: 214
            vm.IsPositive = m.IsPositive; // Clone.tt Line: 214
            vm.ObjectGuid = m.ObjectGuid; // Clone.tt Line: 214
            vm.IsNullable = m.IsNullable; // Clone.tt Line: 214
            vm.ListObjectGuids = new ObservableCollection<string>(); // Clone.tt Line: 177
            foreach (var t in m.ListObjectGuids) // Clone.tt Line: 178
            {
                vm.ListObjectGuids.Add(t);
            }
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'DataType' to 'proto_data_type'
        public static Proto.Config.proto_data_type ConvertToProto(DataType vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_data_type m = new Proto.Config.proto_data_type(); // Clone.tt Line: 230
            m.DataTypeEnum = (Proto.Config.proto_enum_data_type)vm.DataTypeEnum; // Clone.tt Line: 265
            m.Length = vm.Length; // Clone.tt Line: 267
            m.Accuracy = vm.Accuracy; // Clone.tt Line: 267
            m.IsPositive = vm.IsPositive; // Clone.tt Line: 267
            m.ObjectGuid = vm.ObjectGuid; // Clone.tt Line: 267
            m.IsNullable = vm.IsNullable; // Clone.tt Line: 267
            foreach (var t in vm.ListObjectGuids) // Clone.tt Line: 233
                m.ListObjectGuids.Add(t); // Clone.tt Line: 235
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(1)]
        [DisplayName("Type")]
        public EnumDataType DataTypeEnum // Property.tt Line: 135
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
        partial void OnDataTypeEnumChanging(ref EnumDataType to); // Property.tt Line: 157
        partial void OnDataTypeEnumChanged();
        EnumDataType IDataType.DataTypeEnum { get { return this._DataTypeEnum; } }
        
        [PropertyOrderAttribute(5)]
        public uint Length // Property.tt Line: 135
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
        partial void OnLengthChanging(ref uint to); // Property.tt Line: 157
        partial void OnLengthChanged();
        uint IDataType.Length { get { return this._Length; } }
        
        [PropertyOrderAttribute(7)]
        public uint Accuracy // Property.tt Line: 135
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
        partial void OnAccuracyChanging(ref uint to); // Property.tt Line: 157
        partial void OnAccuracyChanged();
        uint IDataType.Accuracy { get { return this._Accuracy; } }
        
        [PropertyOrderAttribute(6)]
        [DisplayName("Is positive")]
        public bool IsPositive // Property.tt Line: 135
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
        partial void OnIsPositiveChanging(ref bool to); // Property.tt Line: 157
        partial void OnIsPositiveChanged();
        bool IDataType.IsPositive { get { return this._IsPositive; } }
        
        [PropertyOrderAttribute(3)]
        [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        public string ObjectGuid // Property.tt Line: 135
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
        partial void OnObjectGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnObjectGuidChanged();
        string IDataType.ObjectGuid { get { return this._ObjectGuid; } }
        
        [PropertyOrderAttribute(2)]
        public bool IsNullable // Property.tt Line: 135
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
        partial void OnIsNullableChanging(ref bool to); // Property.tt Line: 157
        partial void OnIsNullableChanged();
        bool IDataType.IsNullable { get { return this._IsNullable; } }
        
        [PropertyOrderAttribute(4)]
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
        
        [PropertyOrderAttribute(8)]
        [DisplayName("FK Index")]
        [Description("Create Index if this property is using foreign key (for Catalog or Document type)")]
        public bool IsIndexFk // Property.tt Line: 135
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
        partial void OnIsIndexFkChanging(ref bool to); // Property.tt Line: 157
        partial void OnIsIndexFkChanged();
        bool IDataType.IsIndexFk { get { return this._IsIndexFk; } }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    public partial class GroupListCommon : ConfigObjectVmGenSettings<GroupListCommon, GroupListCommon.GroupListCommonValidator>, IComparable<GroupListCommon>, IConfigAcceptVisitor, IGroupListCommon // Class.tt Line: 6
    {
        public partial class GroupListCommonValidator : ValidatorBase<GroupListCommon, GroupListCommonValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListCommon(ITreeConfigNode parent) 
            : base(parent, GroupListCommonValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupRoles = new GroupListRoles(this); // Class.tt Line: 28
            this.GroupViewForms = new GroupListMainViewForms(this); // Class.tt Line: 28
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListCommon vm = new GroupListCommon(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.GroupRoles = GroupListRoles.Clone(vm, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupViewForms = GroupListMainViewForms.Clone(vm, from.GroupViewForms, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListCommon to, GroupListCommon from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                GroupListRoles.Update(to.GroupRoles, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListMainViewForms.Update(to.GroupViewForms, from.GroupViewForms, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListCommon ConvertToVM(Proto.Config.proto_group_list_common m, GroupListCommon vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.GroupRoles == null) // Clone.tt Line: 206
                vm.GroupRoles = new GroupListRoles(vm); // Clone.tt Line: 208
            GroupListRoles.ConvertToVM(m.GroupRoles, vm.GroupRoles); // Clone.tt Line: 212
            if (vm.GroupViewForms == null) // Clone.tt Line: 206
                vm.GroupViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 208
            GroupListMainViewForms.ConvertToVM(m.GroupViewForms, vm.GroupViewForms); // Clone.tt Line: 212
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListCommon' to 'proto_group_list_common'
        public static Proto.Config.proto_group_list_common ConvertToProto(GroupListCommon vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_common m = new Proto.Config.proto_group_list_common(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.GroupRoles = GroupListRoles.ConvertToProto(vm.GroupRoles); // Clone.tt Line: 261
            m.GroupViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupViewForms); // Clone.tt Line: 261
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupRoles.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupViewForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListCommon.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public GroupListRoles GroupRoles // Property.tt Line: 110
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
        partial void OnGroupRolesChanging(ref GroupListRoles to); // Property.tt Line: 131
        partial void OnGroupRolesChanged();
        IGroupListRoles IGroupListCommon.GroupRoles { get { return this._GroupRoles; } }
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupViewForms // Property.tt Line: 110
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
        partial void OnGroupViewFormsChanging(ref GroupListMainViewForms to); // Property.tt Line: 131
        partial void OnGroupViewFormsChanged();
        IGroupListMainViewForms IGroupListCommon.GroupViewForms { get { return this._GroupViewForms; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    public partial class Role : ConfigObjectVmGenSettings<Role, Role.RoleValidator>, IComparable<Role>, IConfigAcceptVisitor, IRole // Class.tt Line: 6
    {
        public partial class RoleValidator : ValidatorBase<Role, RoleValidator> { } // Class.tt Line: 8
        #region CTOR
        public Role(ITreeConfigNode parent) 
            : base(parent, RoleValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Role vm = new Role(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Role to, Role from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Role ConvertToVM(Proto.Config.proto_role m, Role vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Role' to 'proto_role'
        public static Proto.Config.proto_role ConvertToProto(Role vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_role m = new Proto.Config.proto_role(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IRole.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListRoles : ConfigObjectVmGenSettings<GroupListRoles, GroupListRoles.GroupListRolesValidator>, IComparable<GroupListRoles>, IConfigAcceptVisitor, IGroupListRoles // Class.tt Line: 6
    {
        public partial class GroupListRolesValidator : ValidatorBase<GroupListRoles, GroupListRolesValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListRoles(ITreeConfigNode parent) 
            : base(parent, GroupListRolesValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListRoles = new ConfigNodesCollection<Role>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListRoles vm = new GroupListRoles(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListRoles) // Clone.tt Line: 50
                vm.ListRoles.Add(Role.Clone(vm, (Role)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListRoles to, GroupListRoles from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Role(to); // Clone.tt Line: 112
                        Role.Update(p, (Role)tt, isDeep);
                        to.ListRoles.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListRoles ConvertToVM(Proto.Config.proto_group_list_roles m, GroupListRoles vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListRoles) // Clone.tt Line: 194
            {
                var tvm = Role.ConvertToVM(t, new Role(vm)); // Clone.tt Line: 197
                vm.ListRoles.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListRoles' to 'proto_group_list_roles'
        public static Proto.Config.proto_group_list_roles ConvertToProto(GroupListRoles vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_roles m = new Proto.Config.proto_group_list_roles(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListRoles) // Clone.tt Line: 233
                m.ListRoles.Add(Role.ConvertToProto((Role)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListRoles.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Role> ListRoles // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListRoles; 
            }
            set
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
            this.ListRoles.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Role> items) 
        { 
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
            this.ListRoles.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    public partial class MainViewForm : ConfigObjectVmGenSettings<MainViewForm, MainViewForm.MainViewFormValidator>, IComparable<MainViewForm>, IConfigAcceptVisitor, IMainViewForm // Class.tt Line: 6
    {
        public partial class MainViewFormValidator : ValidatorBase<MainViewForm, MainViewFormValidator> { } // Class.tt Line: 8
        #region CTOR
        public MainViewForm(ITreeConfigNode parent) 
            : base(parent, MainViewFormValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupListViewForms = new GroupListMainViewForms(this); // Class.tt Line: 28
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            MainViewForm vm = new MainViewForm(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.GroupListViewForms = GroupListMainViewForms.Clone(vm, from.GroupListViewForms, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(MainViewForm to, MainViewForm from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                GroupListMainViewForms.Update(to.GroupListViewForms, from.GroupListViewForms, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static MainViewForm ConvertToVM(Proto.Config.proto_main_view_form m, MainViewForm vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.GroupListViewForms == null) // Clone.tt Line: 206
                vm.GroupListViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 208
            GroupListMainViewForms.ConvertToVM(m.GroupListViewForms, vm.GroupListViewForms); // Clone.tt Line: 212
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'MainViewForm' to 'proto_main_view_form'
        public static Proto.Config.proto_main_view_form ConvertToProto(MainViewForm vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_main_view_form m = new Proto.Config.proto_main_view_form(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.GroupListViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupListViewForms); // Clone.tt Line: 261
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupListViewForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IMainViewForm.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupListViewForms // Property.tt Line: 110
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
        partial void OnGroupListViewFormsChanging(ref GroupListMainViewForms to); // Property.tt Line: 131
        partial void OnGroupListViewFormsChanged();
        IGroupListMainViewForms IMainViewForm.GroupListViewForms { get { return this._GroupListViewForms; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    public partial class GroupListMainViewForms : ConfigObjectVmGenSettings<GroupListMainViewForms, GroupListMainViewForms.GroupListMainViewFormsValidator>, IComparable<GroupListMainViewForms>, IConfigAcceptVisitor, IGroupListMainViewForms // Class.tt Line: 6
    {
        public partial class GroupListMainViewFormsValidator : ValidatorBase<GroupListMainViewForms, GroupListMainViewFormsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListMainViewForms(ITreeConfigNode parent) 
            : base(parent, GroupListMainViewFormsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListMainViewForms vm = new GroupListMainViewForms(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListMainViewForms) // Clone.tt Line: 50
                vm.ListMainViewForms.Add(MainViewForm.Clone(vm, (MainViewForm)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListMainViewForms to, GroupListMainViewForms from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new MainViewForm(to); // Clone.tt Line: 112
                        MainViewForm.Update(p, (MainViewForm)tt, isDeep);
                        to.ListMainViewForms.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListMainViewForms ConvertToVM(Proto.Config.proto_group_list_main_view_forms m, GroupListMainViewForms vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListMainViewForms) // Clone.tt Line: 194
            {
                var tvm = MainViewForm.ConvertToVM(t, new MainViewForm(vm)); // Clone.tt Line: 197
                vm.ListMainViewForms.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListMainViewForms' to 'proto_group_list_main_view_forms'
        public static Proto.Config.proto_group_list_main_view_forms ConvertToProto(GroupListMainViewForms vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_main_view_forms m = new Proto.Config.proto_group_list_main_view_forms(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListMainViewForms) // Clone.tt Line: 233
                m.ListMainViewForms.Add(MainViewForm.ConvertToProto((MainViewForm)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListMainViewForms.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<MainViewForm> ListMainViewForms // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListMainViewForms; 
            }
            set
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
            this.ListMainViewForms.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<MainViewForm> items) 
        { 
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
            this.ListMainViewForms.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListPropertiesTabs : ConfigObjectVmGenSettings<GroupListPropertiesTabs, GroupListPropertiesTabs.GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IConfigAcceptVisitor, IGroupListPropertiesTabs // Class.tt Line: 6
    {
        public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListPropertiesTabs(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesTabsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListPropertiesTabs vm = new GroupListPropertiesTabs(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListPropertiesTabs) // Clone.tt Line: 50
                vm.ListPropertiesTabs.Add(PropertiesTab.Clone(vm, (PropertiesTab)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListPropertiesTabs to, GroupListPropertiesTabs from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PropertiesTab(to); // Clone.tt Line: 112
                        PropertiesTab.Update(p, (PropertiesTab)tt, isDeep);
                        to.ListPropertiesTabs.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListPropertiesTabs ConvertToVM(Proto.Config.proto_group_list_properties_tabs m, GroupListPropertiesTabs vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListPropertiesTabs) // Clone.tt Line: 194
            {
                var tvm = PropertiesTab.ConvertToVM(t, new PropertiesTab(vm)); // Clone.tt Line: 197
                vm.ListPropertiesTabs.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListPropertiesTabs' to 'proto_group_list_properties_tabs'
        public static Proto.Config.proto_group_list_properties_tabs ConvertToProto(GroupListPropertiesTabs vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_properties_tabs m = new Proto.Config.proto_group_list_properties_tabs(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListPropertiesTabs) // Clone.tt Line: 233
                m.ListPropertiesTabs.Add(PropertiesTab.ConvertToProto((PropertiesTab)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListPropertiesTabs.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PropertiesTab> ListPropertiesTabs // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListPropertiesTabs; 
            }
            set
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
            this.ListPropertiesTabs.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<PropertiesTab> items) 
        { 
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
            this.ListPropertiesTabs.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class PropertiesTab : ConfigObjectVmGenSettings<PropertiesTab, PropertiesTab.PropertiesTabValidator>, IComparable<PropertiesTab>, IConfigAcceptVisitor, IPropertiesTab // Class.tt Line: 6
    {
        public partial class PropertiesTabValidator : ValidatorBase<PropertiesTab, PropertiesTabValidator> { } // Class.tt Line: 8
        #region CTOR
        public PropertiesTab(ITreeConfigNode parent) 
            : base(parent, PropertiesTabValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 28
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 28
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            PropertiesTab vm = new PropertiesTab(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 63
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(PropertiesTab to, PropertiesTab from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static PropertiesTab ConvertToVM(Proto.Config.proto_properties_tab m, PropertiesTab vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.GroupProperties == null) // Clone.tt Line: 206
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 208
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 212
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 206
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 208
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 212
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 214
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'PropertiesTab' to 'proto_properties_tab'
        public static Proto.Config.proto_properties_tab ConvertToProto(PropertiesTab vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_properties_tab m = new Proto.Config.proto_properties_tab(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 261
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 261
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 267
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupPropertiesTabs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IPropertiesTab.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 110
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
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 131
        partial void OnGroupPropertiesChanged();
        IGroupListProperties IPropertiesTab.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 110
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
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 131
        partial void OnGroupPropertiesTabsChanged();
        IGroupListPropertiesTabs IPropertiesTab.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        
        ///////////////////////////////////////////////////
        /// Create Index for foreign key navigation property
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        public bool IsIndexFk // Property.tt Line: 135
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
        partial void OnIsIndexFkChanging(ref bool to); // Property.tt Line: 157
        partial void OnIsIndexFkChanged();
        bool IPropertiesTab.IsIndexFk { get { return this._IsIndexFk; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListProperties : ConfigObjectVmGenSettings<GroupListProperties, GroupListProperties.GroupListPropertiesValidator>, IComparable<GroupListProperties>, IConfigAcceptVisitor, IGroupListProperties // Class.tt Line: 6
    {
        public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListProperties(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListProperties = new ConfigNodesCollection<Property>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListProperties vm = new GroupListProperties(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListProperties = new ConfigNodesCollection<Property>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListProperties) // Clone.tt Line: 50
                vm.ListProperties.Add(Property.Clone(vm, (Property)t, isDeep));
            vm.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 63
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListProperties to, GroupListProperties from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Property(to); // Clone.tt Line: 112
                        Property.Update(p, (Property)tt, isDeep);
                        to.ListProperties.Add(p);
                    }
                }
            }
            to.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListProperties ConvertToVM(Proto.Config.proto_group_list_properties m, GroupListProperties vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListProperties = new ConfigNodesCollection<Property>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListProperties) // Clone.tt Line: 194
            {
                var tvm = Property.ConvertToVM(t, new Property(vm)); // Clone.tt Line: 197
                vm.ListProperties.Add(tvm);
            }
            vm.LastGenPosition = m.LastGenPosition; // Clone.tt Line: 214
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListProperties' to 'proto_group_list_properties'
        public static Proto.Config.proto_group_list_properties ConvertToProto(GroupListProperties vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_properties m = new Proto.Config.proto_group_list_properties(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListProperties) // Clone.tt Line: 233
                m.ListProperties.Add(Property.ConvertToProto((Property)t)); // Clone.tt Line: 237
            m.LastGenPosition = vm.LastGenPosition; // Clone.tt Line: 267
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListProperties.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Property> ListProperties // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListProperties; 
            }
            set
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
            this.ListProperties.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Property> items) 
        { 
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
            this.ListProperties.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        
        ///////////////////////////////////////////////////
        /// Last generated Protobuf field position
        ///////////////////////////////////////////////////
        [ReadOnly(true)]
        public uint LastGenPosition // Property.tt Line: 135
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
        partial void OnLastGenPositionChanging(ref uint to); // Property.tt Line: 157
        partial void OnLastGenPositionChanged();
        uint IGroupListProperties.LastGenPosition { get { return this._LastGenPosition; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class Property : ConfigObjectVmGenSettings<Property, Property.PropertyValidator>, IComparable<Property>, IConfigAcceptVisitor, IProperty // Class.tt Line: 6
    {
        public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { } // Class.tt Line: 8
        #region CTOR
        public Property(ITreeConfigNode parent) 
            : base(parent, PropertyValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Property vm = new Property(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.DataType = DataType.Clone(from.DataType, isDeep);
            vm.Position = from.Position; // Clone.tt Line: 63
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Property to, Property from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                DataType.Update(to.DataType, from.DataType, isDeep);
            to.Position = from.Position; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Property ConvertToVM(Proto.Config.proto_property m, Property vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.DataType == null) // Clone.tt Line: 206
                vm.DataType = new DataType(); // Clone.tt Line: 210
            DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 212
            vm.Position = m.Position; // Clone.tt Line: 214
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Property' to 'proto_property'
        public static Proto.Config.proto_property ConvertToProto(Property vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_property m = new Proto.Config.proto_property(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 261
            m.Position = vm.Position; // Clone.tt Line: 267
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IProperty.Description { get { return this._Description; } }
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 110
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
        partial void OnDataTypeChanging(ref DataType to); // Property.tt Line: 131
        partial void OnDataTypeChanged();
        IDataType IProperty.DataType { get { return this._DataType; } }
        
        
        ///////////////////////////////////////////////////
        /// Protobuf field position
        /// Reserved positions: 1 - primary key
        ///////////////////////////////////////////////////
        [ReadOnly(true)]
        public uint Position // Property.tt Line: 135
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
        partial void OnPositionChanging(ref uint to); // Property.tt Line: 157
        partial void OnPositionChanged();
        uint IProperty.Position { get { return this._Position; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListConstants : ConfigObjectVmGenSettings<GroupListConstants, GroupListConstants.GroupListConstantsValidator>, IComparable<GroupListConstants>, IConfigAcceptVisitor, IGroupListConstants // Class.tt Line: 6
    {
        public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListConstants(ITreeConfigNode parent) 
            : base(parent, GroupListConstantsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListConstants = new ConfigNodesCollection<Constant>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListConstants vm = new GroupListConstants(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListConstants = new ConfigNodesCollection<Constant>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListConstants) // Clone.tt Line: 50
                vm.ListConstants.Add(Constant.Clone(vm, (Constant)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListConstants to, GroupListConstants from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Constant(to); // Clone.tt Line: 112
                        Constant.Update(p, (Constant)tt, isDeep);
                        to.ListConstants.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListConstants ConvertToVM(Proto.Config.proto_group_list_constants m, GroupListConstants vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListConstants = new ConfigNodesCollection<Constant>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListConstants) // Clone.tt Line: 194
            {
                var tvm = Constant.ConvertToVM(t, new Constant(vm)); // Clone.tt Line: 197
                vm.ListConstants.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListConstants' to 'proto_group_list_constants'
        public static Proto.Config.proto_group_list_constants ConvertToProto(GroupListConstants vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_constants m = new Proto.Config.proto_group_list_constants(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListConstants) // Clone.tt Line: 233
                m.ListConstants.Add(Constant.ConvertToProto((Constant)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListConstants.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Constant> ListConstants // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListConstants; 
            }
            set
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
            this.ListConstants.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Constant> items) 
        { 
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
            this.ListConstants.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    public partial class Constant : ConfigObjectVmGenSettings<Constant, Constant.ConstantValidator>, IComparable<Constant>, IConfigAcceptVisitor, IConstant // Class.tt Line: 6
    {
        public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { } // Class.tt Line: 8
        #region CTOR
        public Constant(ITreeConfigNode parent) 
            : base(parent, ConstantValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 26
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Constant vm = new Constant(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.DataType = DataType.Clone(from.DataType, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Constant to, Constant from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                DataType.Update(to.DataType, from.DataType, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Constant ConvertToVM(Proto.Config.proto_constant m, Constant vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.DataType == null) // Clone.tt Line: 206
                vm.DataType = new DataType(); // Clone.tt Line: 210
            DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 212
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Constant' to 'proto_constant'
        public static Proto.Config.proto_constant ConvertToProto(Constant vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_constant m = new Proto.Config.proto_constant(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 261
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IConstant.Description { get { return this._Description; } }
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 110
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
        partial void OnDataTypeChanging(ref DataType to); // Property.tt Line: 131
        partial void OnDataTypeChanged();
        IDataType IConstant.DataType { get { return this._DataType; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListEnumerations : ConfigObjectVmGenSettings<GroupListEnumerations, GroupListEnumerations.GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IConfigAcceptVisitor, IGroupListEnumerations // Class.tt Line: 6
    {
        public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListEnumerations(ITreeConfigNode parent) 
            : base(parent, GroupListEnumerationsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListEnumerations = new ConfigNodesCollection<Enumeration>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListEnumerations vm = new GroupListEnumerations(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListEnumerations) // Clone.tt Line: 50
                vm.ListEnumerations.Add(Enumeration.Clone(vm, (Enumeration)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListEnumerations to, GroupListEnumerations from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Enumeration(to); // Clone.tt Line: 112
                        Enumeration.Update(p, (Enumeration)tt, isDeep);
                        to.ListEnumerations.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListEnumerations ConvertToVM(Proto.Config.proto_group_list_enumerations m, GroupListEnumerations vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListEnumerations) // Clone.tt Line: 194
            {
                var tvm = Enumeration.ConvertToVM(t, new Enumeration(vm)); // Clone.tt Line: 197
                vm.ListEnumerations.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListEnumerations' to 'proto_group_list_enumerations'
        public static Proto.Config.proto_group_list_enumerations ConvertToProto(GroupListEnumerations vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_enumerations m = new Proto.Config.proto_group_list_enumerations(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListEnumerations) // Clone.tt Line: 233
                m.ListEnumerations.Add(Enumeration.ConvertToProto((Enumeration)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListEnumerations.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Enumeration> ListEnumerations // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListEnumerations; 
            }
            set
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
            this.ListEnumerations.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Enumeration> items) 
        { 
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
            this.ListEnumerations.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class Enumeration : ConfigObjectVmGenSettings<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>, IConfigAcceptVisitor, IEnumeration // Class.tt Line: 6
    {
        public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { } // Class.tt Line: 8
        #region CTOR
        public Enumeration(ITreeConfigNode parent) 
            : base(parent, EnumerationValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Enumeration vm = new Enumeration(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 63
            vm.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 63
            vm.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListEnumerationPairs) // Clone.tt Line: 50
                vm.ListEnumerationPairs.Add(EnumerationPair.Clone(vm, (EnumerationPair)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Enumeration to, Enumeration from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 136
            to.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new EnumerationPair(to); // Clone.tt Line: 112
                        EnumerationPair.Update(p, (EnumerationPair)tt, isDeep);
                        to.ListEnumerationPairs.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Enumeration ConvertToVM(Proto.Config.proto_enumeration m, Enumeration vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.DataTypeEnum = (EnumEnumerationType)m.DataTypeEnum; // Clone.tt Line: 214
            vm.DataTypeLength = m.DataTypeLength; // Clone.tt Line: 214
            vm.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListEnumerationPairs) // Clone.tt Line: 194
            {
                var tvm = EnumerationPair.ConvertToVM(t, new EnumerationPair(vm)); // Clone.tt Line: 197
                vm.ListEnumerationPairs.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Enumeration' to 'proto_enumeration'
        public static Proto.Config.proto_enumeration ConvertToProto(Enumeration vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_enumeration m = new Proto.Config.proto_enumeration(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.DataTypeEnum = (Proto.Config.enum_enumeration_type)vm.DataTypeEnum; // Clone.tt Line: 265
            m.DataTypeLength = vm.DataTypeLength; // Clone.tt Line: 267
            foreach (var t in vm.ListEnumerationPairs) // Clone.tt Line: 233
                m.ListEnumerationPairs.Add(EnumerationPair.ConvertToProto((EnumerationPair)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IEnumeration.Description { get { return this._Description; } }
        
        
        ///////////////////////////////////////////////////
        /// Enumeration element type
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        [DisplayName("Type")]
        public EnumEnumerationType DataTypeEnum // Property.tt Line: 135
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
        partial void OnDataTypeEnumChanging(ref EnumEnumerationType to); // Property.tt Line: 157
        partial void OnDataTypeEnumChanged();
        EnumEnumerationType IEnumeration.DataTypeEnum { get { return this._DataTypeEnum; } }
        
        
        ///////////////////////////////////////////////////
        /// Length of string if 'STRING' is selected as enumeration element type
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(5)]
        [DisplayName("Length")]
        public int DataTypeLength // Property.tt Line: 135
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
        partial void OnDataTypeLengthChanging(ref int to); // Property.tt Line: 157
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
            set
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
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class EnumerationPair : ConfigObjectVmGenSettings<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>, IConfigAcceptVisitor, IEnumerationPair // Class.tt Line: 6
    {
        public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { } // Class.tt Line: 8
        #region CTOR
        public EnumerationPair(ITreeConfigNode parent) 
            : base(parent, EnumerationPairValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.OnInit();
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
            EnumerationPair vm = new EnumerationPair(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.Value = from.Value; // Clone.tt Line: 63
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(EnumerationPair to, EnumerationPair from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            to.Value = from.Value; // Clone.tt Line: 136
        }
        // Clone.tt Line: 142
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
        public static EnumerationPair ConvertToVM(Proto.Config.proto_enumeration_pair m, EnumerationPair vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.Value = m.Value; // Clone.tt Line: 214
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
        public static Proto.Config.proto_enumeration_pair ConvertToProto(EnumerationPair vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_enumeration_pair m = new Proto.Config.proto_enumeration_pair(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.Value = vm.Value; // Clone.tt Line: 267
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IEnumerationPair.Description { get { return this._Description; } }
        
        public string Value // Property.tt Line: 135
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
        partial void OnValueChanging(ref string to); // Property.tt Line: 157
        partial void OnValueChanged();
        string IEnumerationPair.Value { get { return this._Value; } }
    
        #endregion Properties
    }
    public partial class Catalog : ConfigObjectVmGenSettings<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IConfigAcceptVisitor, ICatalog // Class.tt Line: 6
    {
        public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { } // Class.tt Line: 8
        #region CTOR
        public Catalog(ITreeConfigNode parent) 
            : base(parent, CatalogValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 28
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 28
            this.GroupForms = new GroupListForms(this); // Class.tt Line: 28
            this.GroupReports = new GroupListReports(this); // Class.tt Line: 28
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Catalog vm = new Catalog(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupForms = GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupReports = GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Catalog to, Catalog from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Catalog ConvertToVM(Proto.Config.proto_catalog m, Catalog vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.GroupProperties == null) // Clone.tt Line: 206
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 208
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 212
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 206
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 208
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 212
            if (vm.GroupForms == null) // Clone.tt Line: 206
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 208
            GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 212
            if (vm.GroupReports == null) // Clone.tt Line: 206
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 208
            GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 212
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Catalog' to 'proto_catalog'
        public static Proto.Config.proto_catalog ConvertToProto(Catalog vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_catalog m = new Proto.Config.proto_catalog(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 261
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 261
            m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 261
            m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 261
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupPropertiesTabs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupReports.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string ICatalog.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 110
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
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 131
        partial void OnGroupPropertiesChanged();
        IGroupListProperties ICatalog.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 110
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
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 131
        partial void OnGroupPropertiesTabsChanged();
        IGroupListPropertiesTabs ICatalog.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 110
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
        partial void OnGroupFormsChanging(ref GroupListForms to); // Property.tt Line: 131
        partial void OnGroupFormsChanged();
        IGroupListForms ICatalog.GroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 110
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
        partial void OnGroupReportsChanging(ref GroupListReports to); // Property.tt Line: 131
        partial void OnGroupReportsChanged();
        IGroupListReports ICatalog.GroupReports { get { return this._GroupReports; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListCatalogs : ConfigObjectVmGenSettings<GroupListCatalogs, GroupListCatalogs.GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IConfigAcceptVisitor, IGroupListCatalogs // Class.tt Line: 6
    {
        public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListCatalogs(ITreeConfigNode parent) 
            : base(parent, GroupListCatalogsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListCatalogs = new ConfigNodesCollection<Catalog>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListCatalogs vm = new GroupListCatalogs(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListCatalogs = new ConfigNodesCollection<Catalog>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListCatalogs) // Clone.tt Line: 50
                vm.ListCatalogs.Add(Catalog.Clone(vm, (Catalog)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListCatalogs to, GroupListCatalogs from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Catalog(to); // Clone.tt Line: 112
                        Catalog.Update(p, (Catalog)tt, isDeep);
                        to.ListCatalogs.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListCatalogs ConvertToVM(Proto.Config.proto_group_list_catalogs m, GroupListCatalogs vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListCatalogs = new ConfigNodesCollection<Catalog>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListCatalogs) // Clone.tt Line: 194
            {
                var tvm = Catalog.ConvertToVM(t, new Catalog(vm)); // Clone.tt Line: 197
                vm.ListCatalogs.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
        public static Proto.Config.proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_catalogs m = new Proto.Config.proto_group_list_catalogs(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListCatalogs) // Clone.tt Line: 233
                m.ListCatalogs.Add(Catalog.ConvertToProto((Catalog)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListCatalogs.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Catalog> ListCatalogs // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListCatalogs; 
            }
            set
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
            this.ListCatalogs.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Catalog> items) 
        { 
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
            this.ListCatalogs.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupDocuments : ConfigObjectVmGenSettings<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IConfigAcceptVisitor, IGroupDocuments // Class.tt Line: 6
    {
        public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupDocuments(ITreeConfigNode parent) 
            : base(parent, GroupDocumentsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupSharedProperties = new GroupListProperties(this); // Class.tt Line: 28
            this.GroupListDocuments = new GroupListDocuments(this); // Class.tt Line: 28
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupDocuments vm = new GroupDocuments(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.GroupSharedProperties = GroupListProperties.Clone(vm, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupListDocuments = GroupListDocuments.Clone(vm, from.GroupListDocuments, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupDocuments to, GroupDocuments from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                GroupListProperties.Update(to.GroupSharedProperties, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListDocuments.Update(to.GroupListDocuments, from.GroupListDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupDocuments ConvertToVM(Proto.Config.proto_group_documents m, GroupDocuments vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.GroupSharedProperties == null) // Clone.tt Line: 206
                vm.GroupSharedProperties = new GroupListProperties(vm); // Clone.tt Line: 208
            GroupListProperties.ConvertToVM(m.GroupSharedProperties, vm.GroupSharedProperties); // Clone.tt Line: 212
            if (vm.GroupListDocuments == null) // Clone.tt Line: 206
                vm.GroupListDocuments = new GroupListDocuments(vm); // Clone.tt Line: 208
            GroupListDocuments.ConvertToVM(m.GroupListDocuments, vm.GroupListDocuments); // Clone.tt Line: 212
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupDocuments' to 'proto_group_documents'
        public static Proto.Config.proto_group_documents ConvertToProto(GroupDocuments vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_documents m = new Proto.Config.proto_group_documents(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.GroupSharedProperties = GroupListProperties.ConvertToProto(vm.GroupSharedProperties); // Clone.tt Line: 261
            m.GroupListDocuments = GroupListDocuments.ConvertToProto(vm.GroupListDocuments); // Clone.tt Line: 261
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupSharedProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupListDocuments.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupDocuments.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupSharedProperties // Property.tt Line: 110
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
        partial void OnGroupSharedPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 131
        partial void OnGroupSharedPropertiesChanged();
        IGroupListProperties IGroupDocuments.GroupSharedProperties { get { return this._GroupSharedProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListDocuments GroupListDocuments // Property.tt Line: 110
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
        partial void OnGroupListDocumentsChanging(ref GroupListDocuments to); // Property.tt Line: 131
        partial void OnGroupListDocumentsChanged();
        IGroupListDocuments IGroupDocuments.GroupListDocuments { get { return this._GroupListDocuments; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class Document : ConfigObjectVmGenSettings<Document, Document.DocumentValidator>, IComparable<Document>, IConfigAcceptVisitor, IDocument // Class.tt Line: 6
    {
        public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { } // Class.tt Line: 8
        #region CTOR
        public Document(ITreeConfigNode parent) 
            : base(parent, DocumentValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 28
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 28
            this.GroupForms = new GroupListForms(this); // Class.tt Line: 28
            this.GroupReports = new GroupListReports(this); // Class.tt Line: 28
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Document vm = new Document(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            if (isDeep) // Clone.tt Line: 60
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupForms = GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 60
                vm.GroupReports = GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Document to, Document from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 133
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 133
                GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Document ConvertToVM(Proto.Config.proto_document m, Document vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            if (vm.GroupProperties == null) // Clone.tt Line: 206
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 208
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 212
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 206
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 208
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 212
            if (vm.GroupForms == null) // Clone.tt Line: 206
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 208
            GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 212
            if (vm.GroupReports == null) // Clone.tt Line: 206
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 208
            GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 212
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Document' to 'proto_document'
        public static Proto.Config.proto_document ConvertToProto(Document vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_document m = new Proto.Config.proto_document(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 261
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 261
            m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 261
            m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 261
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupPropertiesTabs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            this.GroupReports.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 29
        
            foreach (var t in this.ListNodeGeneratorsSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        [PropertyOrderAttribute(3)]
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IDocument.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 110
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
        partial void OnGroupPropertiesChanging(ref GroupListProperties to); // Property.tt Line: 131
        partial void OnGroupPropertiesChanged();
        IGroupListProperties IDocument.GroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 110
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
        partial void OnGroupPropertiesTabsChanging(ref GroupListPropertiesTabs to); // Property.tt Line: 131
        partial void OnGroupPropertiesTabsChanged();
        IGroupListPropertiesTabs IDocument.GroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 110
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
        partial void OnGroupFormsChanging(ref GroupListForms to); // Property.tt Line: 131
        partial void OnGroupFormsChanged();
        IGroupListForms IDocument.GroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 110
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
        partial void OnGroupReportsChanging(ref GroupListReports to); // Property.tt Line: 131
        partial void OnGroupReportsChanged();
        IGroupListReports IDocument.GroupReports { get { return this._GroupReports; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListDocuments : ConfigObjectVmGenSettings<GroupListDocuments, GroupListDocuments.GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IConfigAcceptVisitor, IGroupListDocuments // Class.tt Line: 6
    {
        public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListDocuments(ITreeConfigNode parent) 
            : base(parent, GroupListDocumentsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListDocuments vm = new GroupListDocuments(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListDocuments) // Clone.tt Line: 50
                vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListDocuments to, GroupListDocuments from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Document(to); // Clone.tt Line: 112
                        Document.Update(p, (Document)tt, isDeep);
                        to.ListDocuments.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListDocuments ConvertToVM(Proto.Config.proto_group_list_documents m, GroupListDocuments vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListDocuments) // Clone.tt Line: 194
            {
                var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 197
                vm.ListDocuments.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
        public static Proto.Config.proto_group_list_documents ConvertToProto(GroupListDocuments vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_documents m = new Proto.Config.proto_group_list_documents(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListDocuments) // Clone.tt Line: 233
                m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IGroupListDocuments.Description { get { return this._Description; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Document> ListDocuments // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListDocuments; 
            }
            set
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
            this.ListDocuments.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Document> items) 
        { 
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
            this.ListDocuments.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListJournals : ConfigObjectVmGenSettings<GroupListJournals, GroupListJournals.GroupListJournalsValidator>, IComparable<GroupListJournals>, IConfigAcceptVisitor, IGroupListJournals // Class.tt Line: 6
    {
        public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListJournals(ITreeConfigNode parent) 
            : base(parent, GroupListJournalsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListJournals = new ConfigNodesCollection<Journal>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListJournals vm = new GroupListJournals(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListJournals) // Clone.tt Line: 50
                vm.ListJournals.Add(Journal.Clone(vm, (Journal)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListJournals to, GroupListJournals from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Journal(to); // Clone.tt Line: 112
                        Journal.Update(p, (Journal)tt, isDeep);
                        to.ListJournals.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListJournals ConvertToVM(Proto.Config.proto_group_list_journals m, GroupListJournals vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListJournals) // Clone.tt Line: 194
            {
                var tvm = Journal.ConvertToVM(t, new Journal(vm)); // Clone.tt Line: 197
                vm.ListJournals.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListJournals' to 'proto_group_list_journals'
        public static Proto.Config.proto_group_list_journals ConvertToProto(GroupListJournals vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_journals m = new Proto.Config.proto_group_list_journals(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListJournals) // Clone.tt Line: 233
                m.ListJournals.Add(Journal.ConvertToProto((Journal)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
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
            set
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
            this.ListJournals.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Journal> items) 
        { 
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
            this.ListJournals.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class Journal : ConfigObjectVmGenSettings<Journal, Journal.JournalValidator>, IComparable<Journal>, IConfigAcceptVisitor, IJournal // Class.tt Line: 6
    {
        public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { } // Class.tt Line: 8
        #region CTOR
        public Journal(ITreeConfigNode parent) 
            : base(parent, JournalValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Journal vm = new Journal(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListDocuments) // Clone.tt Line: 50
                vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Journal to, Journal from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Document(to); // Clone.tt Line: 112
                        Document.Update(p, (Document)tt, isDeep);
                        to.ListDocuments.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Journal ConvertToVM(Proto.Config.proto_journal m, Journal vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListDocuments) // Clone.tt Line: 194
            {
                var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 197
                vm.ListDocuments.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Journal' to 'proto_journal'
        public static Proto.Config.proto_journal ConvertToProto(Journal vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_journal m = new Proto.Config.proto_journal(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListDocuments) // Clone.tt Line: 233
                m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
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
            set
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
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class GroupListForms : ConfigObjectVmGenSettings<GroupListForms, GroupListForms.GroupListFormsValidator>, IComparable<GroupListForms>, IConfigAcceptVisitor, IGroupListForms // Class.tt Line: 6
    {
        public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListForms(ITreeConfigNode parent) 
            : base(parent, GroupListFormsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListForms = new ConfigNodesCollection<Form>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListForms vm = new GroupListForms(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListForms) // Clone.tt Line: 50
                vm.ListForms.Add(Form.Clone(vm, (Form)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListForms to, GroupListForms from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Form(to); // Clone.tt Line: 112
                        Form.Update(p, (Form)tt, isDeep);
                        to.ListForms.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListForms ConvertToVM(Proto.Config.proto_group_list_forms m, GroupListForms vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListForms) // Clone.tt Line: 194
            {
                var tvm = Form.ConvertToVM(t, new Form(vm)); // Clone.tt Line: 197
                vm.ListForms.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListForms' to 'proto_group_list_forms'
        public static Proto.Config.proto_group_list_forms ConvertToProto(GroupListForms vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_forms m = new Proto.Config.proto_group_list_forms(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListForms) // Clone.tt Line: 233
                m.ListForms.Add(Form.ConvertToProto((Form)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
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
            set
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
            this.ListForms.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Form> items) 
        { 
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
            this.ListForms.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class Form : ConfigObjectVmGenSettings<Form, Form.FormValidator>, IComparable<Form>, IConfigAcceptVisitor, IForm // Class.tt Line: 6
    {
        public partial class FormValidator : ValidatorBase<Form, FormValidator> { } // Class.tt Line: 8
        #region CTOR
        public Form(ITreeConfigNode parent) 
            : base(parent, FormValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Form vm = new Form(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Form to, Form from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Form ConvertToVM(Proto.Config.proto_form m, Form vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Form' to 'proto_form'
        public static Proto.Config.proto_form ConvertToProto(Form vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_form m = new Proto.Config.proto_form(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IForm.Description { get { return this._Description; } }
        
        
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
            set
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
    
        #endregion Properties
    }
    public partial class GroupListReports : ConfigObjectVmGenSettings<GroupListReports, GroupListReports.GroupListReportsValidator>, IComparable<GroupListReports>, IConfigAcceptVisitor, IGroupListReports // Class.tt Line: 6
    {
        public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListReports(ITreeConfigNode parent) 
            : base(parent, GroupListReportsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListReports = new ConfigNodesCollection<Report>(this); // Class.tt Line: 22
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            GroupListReports vm = new GroupListReports(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListReports) // Clone.tt Line: 50
                vm.ListReports.Add(Report.Clone(vm, (Report)t, isDeep));
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(GroupListReports to, GroupListReports from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new Report(to); // Clone.tt Line: 112
                        Report.Update(p, (Report)tt, isDeep);
                        to.ListReports.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static GroupListReports ConvertToVM(Proto.Config.proto_group_list_reports m, GroupListReports vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListReports) // Clone.tt Line: 194
            {
                var tvm = Report.ConvertToVM(t, new Report(vm)); // Clone.tt Line: 197
                vm.ListReports.Add(tvm);
            }
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'GroupListReports' to 'proto_group_list_reports'
        public static Proto.Config.proto_group_list_reports ConvertToProto(GroupListReports vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_group_list_reports m = new Proto.Config.proto_group_list_reports(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListReports) // Clone.tt Line: 233
                m.ListReports.Add(Report.ConvertToProto((Report)t)); // Clone.tt Line: 237
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
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
            set
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
            this.ListReports.Add(item); 
            item.Parent = this;
            this.IsChanged = true;
        }
        public void AddRange(IEnumerable<Report> items) 
        { 
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
            this.ListReports.Remove(item); 
            item.Parent = null;
            this.IsChanged = true;
        }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings // Property.tt Line: 58
        { 
            get 
            { 
                return this._ListNodeGeneratorsSettings; 
            }
            set
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
    
        #endregion Properties
    }
    public partial class Report : ConfigObjectVmGenSettings<Report, Report.ReportValidator>, IComparable<Report>, IConfigAcceptVisitor, IReport // Class.tt Line: 6
    {
        public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { } // Class.tt Line: 8
        #region CTOR
        public Report(ITreeConfigNode parent) 
            : base(parent, ReportValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(this); // Class.tt Line: 22
            this.OnInit();
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
            Report vm = new Report(parent);
            vm.IsNotNotifying = true;
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 63
            vm.NameUi = from.NameUi; // Clone.tt Line: 63
            vm.Description = from.Description; // Clone.tt Line: 63
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 49
            foreach (var t in from.ListNodeGeneratorsSettings) // Clone.tt Line: 50
                vm.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.Clone(vm, (PluginGeneratorNodeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 68
                vm.SetNewGuid();
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(Report to, Report from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.SortingValue = from.SortingValue; // Clone.tt Line: 136
            to.NameUi = from.NameUi; // Clone.tt Line: 136
            to.Description = from.Description; // Clone.tt Line: 136
            if (isDeep) // Clone.tt Line: 81
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
                        var p = new PluginGeneratorNodeSettings(to); // Clone.tt Line: 112
                        PluginGeneratorNodeSettings.Update(p, (PluginGeneratorNodeSettings)tt, isDeep);
                        to.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 142
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
        public static Report ConvertToVM(Proto.Config.proto_report m, Report vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 214
            vm.NameUi = m.NameUi; // Clone.tt Line: 214
            vm.Description = m.Description; // Clone.tt Line: 214
            vm.ListNodeGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorNodeSettings>(vm); // Clone.tt Line: 193
            foreach (var t in m.ListNodeGeneratorsSettings) // Clone.tt Line: 194
            {
                var tvm = PluginGeneratorNodeSettings.ConvertToVM(t, new PluginGeneratorNodeSettings(vm)); // Clone.tt Line: 197
                vm.ListNodeGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 220
            vm.IsSubTreeChanged = false;
            vm.IsChanged = false;
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'Report' to 'proto_report'
        public static Proto.Config.proto_report ConvertToProto(Report vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_report m = new Proto.Config.proto_report(); // Clone.tt Line: 230
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 267
            m.NameUi = vm.NameUi; // Clone.tt Line: 267
            m.Description = vm.Description; // Clone.tt Line: 267
            foreach (var t in vm.ListNodeGeneratorsSettings) // Clone.tt Line: 233
                m.ListNodeGeneratorsSettings.Add(PluginGeneratorNodeSettings.ConvertToProto((PluginGeneratorNodeSettings)t)); // Clone.tt Line: 237
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
        public string Description // Property.tt Line: 135
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
        partial void OnDescriptionChanging(ref string to); // Property.tt Line: 157
        partial void OnDescriptionChanged();
        string IReport.Description { get { return this._Description; } }
        
        
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
            set
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
    
        #endregion Properties
    }
    public partial class ModelRow : VmBindable, IModelRow // Class.tt Line: 6
    {
        public partial class ModelRowValidator : ValidatorBase<ModelRow, ModelRowValidator> { } // Class.tt Line: 8
        #region CTOR
        public ModelRow() // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static ModelRow Clone(ModelRow from, bool isDeep = true) // Clone.tt Line: 27
        {
            ModelRow vm = new ModelRow();
            vm.IsNotNotifying = true;
            vm.GroupName = from.GroupName; // Clone.tt Line: 63
            vm.Name = from.Name; // Clone.tt Line: 63
            vm.Guid = from.Guid; // Clone.tt Line: 63
            vm.IsIncluded = from.IsIncluded; // Clone.tt Line: 63
            vm.IsNotNotifying = false;
            return vm;
        }
        public static void Update(ModelRow to, ModelRow from, bool isDeep = true) // Clone.tt Line: 74
        {
            to.GroupName = from.GroupName; // Clone.tt Line: 136
            to.Name = from.Name; // Clone.tt Line: 136
            to.Guid = from.Guid; // Clone.tt Line: 136
            to.IsIncluded = from.IsIncluded; // Clone.tt Line: 136
        }
        // Conversion from 'proto_model_row' to 'ModelRow'
        public static ModelRow ConvertToVM(Proto.Config.proto_model_row m, ModelRow vm) // Clone.tt Line: 165
        {
            if (m == null)
            {
                return vm;
            }
            vm.IsNotNotifying = true;
            vm.GroupName = m.GroupName; // Clone.tt Line: 214
            vm.Name = m.Name; // Clone.tt Line: 214
            vm.Guid = m.Guid; // Clone.tt Line: 214
            vm.IsIncluded = m.IsIncluded; // Clone.tt Line: 214
            vm.IsNotNotifying = false;
            return vm;
        }
        // Conversion from 'ModelRow' to 'proto_model_row'
        public static Proto.Config.proto_model_row ConvertToProto(ModelRow vm) // Clone.tt Line: 228
        {
            Proto.Config.proto_model_row m = new Proto.Config.proto_model_row(); // Clone.tt Line: 230
            m.GroupName = vm.GroupName; // Clone.tt Line: 267
            m.Name = vm.Name; // Clone.tt Line: 267
            m.Guid = vm.Guid; // Clone.tt Line: 267
            m.IsIncluded = vm.IsIncluded; // Clone.tt Line: 267
            return m;
        }
        #endregion Procedures
        #region Properties
        
        public string GroupName // Property.tt Line: 135
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
        partial void OnGroupNameChanging(ref string to); // Property.tt Line: 157
        partial void OnGroupNameChanged();
        string IModelRow.GroupName { get { return this._GroupName; } }
        
        public string Name // Property.tt Line: 135
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
        partial void OnNameChanging(ref string to); // Property.tt Line: 157
        partial void OnNameChanged();
        string IModelRow.Name { get { return this._Name; } }
        
        public string Guid // Property.tt Line: 135
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
        partial void OnGuidChanging(ref string to); // Property.tt Line: 157
        partial void OnGuidChanged();
        string IModelRow.Guid { get { return this._Guid; } }
        
        public bool IsIncluded // Property.tt Line: 135
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
        partial void OnIsIncludedChanging(ref bool to); // Property.tt Line: 157
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
        void Visit(Proto.Config.proto_plugin_generator_settings p);
        void Visit(Proto.Config.proto_settings_config p);
        void Visit(Proto.Config.db_settings p);
        void Visit(Proto.Config.proto_config_short_history p);
        void Visit(Proto.Config.proto_group_list_base_config_links p);
        void Visit(Proto.Config.proto_base_config_link p);
        void Visit(Proto.Config.proto_config p);
        void Visit(Proto.Config.proto_app_db_settings p);
        void Visit(Proto.Config.proto_group_list_app_solutions p);
        void Visit(Proto.Config.proto_app_solution p);
        void Visit(Proto.Config.proto_app_project p);
        void Visit(Proto.Config.proto_plugin_generator_node_settings p);
        void Visit(Proto.Config.proto_plugin_generator_main_settings p);
        void Visit(Proto.Config.proto_app_project_generator p);
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
            this.OnVisit(p as IValidatableWithSeverity);
            foreach (var t in p.ListOpenConfigHistory) // ValidationVisitor.tt Line: 37
                p.ValidateSubAndCollectErrors(t);
        }
        protected override void OnVisitEnd(UserSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(UserSettingsOpenedConfig p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(UserSettingsOpenedConfig p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListPlugins p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListPlugins p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Plugin p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Plugin p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGenerator p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGenerator p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGeneratorSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGeneratorSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(SettingsConfig p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(SettingsConfig p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(DbSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(DbSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(ConfigShortHistory p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(ConfigShortHistory p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListBaseConfigLinks p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListBaseConfigLinks p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(BaseConfigLink p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(BaseConfigLink p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Config p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DbSettings); // ValidationVisitor.tt Line: 30
        }
        protected override void OnVisitEnd(Config p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppDbSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(AppDbSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListAppSolutions p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListAppSolutions p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppSolution p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(AppSolution p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppProject p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(AppProject p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGeneratorNodeSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGeneratorNodeSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PluginGeneratorMainSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PluginGeneratorMainSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppProjectGenerator p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(AppProjectGenerator p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(ConfigModel p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(ConfigModel p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(DataType p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(DataType p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListCommon p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListCommon p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Role p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Role p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListRoles p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListRoles p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(MainViewForm p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(MainViewForm p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListMainViewForms p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListMainViewForms p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListPropertiesTabs p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListPropertiesTabs p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(PropertiesTab p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(PropertiesTab p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListProperties p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListProperties p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Property p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DataType); // ValidationVisitor.tt Line: 30
        }
        protected override void OnVisitEnd(Property p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListConstants p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListConstants p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Constant p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DataType); // ValidationVisitor.tt Line: 30
        }
        protected override void OnVisitEnd(Constant p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListEnumerations p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListEnumerations p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Enumeration p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Enumeration p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(EnumerationPair p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(EnumerationPair p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Catalog p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Catalog p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListCatalogs p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListCatalogs p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupDocuments p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupDocuments p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Document p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Document p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListDocuments p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListDocuments p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListJournals p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListJournals p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Journal p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Journal p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListForms p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListForms p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Form p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Form p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(GroupListReports p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GroupListReports p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(Report p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(Report p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(ModelRow p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(ModelRow p) // ValidationVisitor.tt Line: 47
        {
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
        void Visit(PluginGeneratorSettings p);
        void VisitEnd(PluginGeneratorSettings p);
        void Visit(ConfigShortHistory p);
        void VisitEnd(ConfigShortHistory p);
        void Visit(GroupListBaseConfigLinks p);
        void VisitEnd(GroupListBaseConfigLinks p);
        void Visit(BaseConfigLink p);
        void VisitEnd(BaseConfigLink p);
        void Visit(Config p);
        void VisitEnd(Config p);
        void Visit(AppDbSettings p);
        void VisitEnd(AppDbSettings p);
        void Visit(GroupListAppSolutions p);
        void VisitEnd(GroupListAppSolutions p);
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