// Auto generated on UTC 12/25/2019 20:52:17
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
    public partial class GroupListPlugins : ConfigObjectSubBase<GroupListPlugins, GroupListPlugins.GroupListPluginsValidator>, IComparable<GroupListPlugins>, IConfigAcceptVisitor, IGroupListPlugins // Class.tt Line: 6
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
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListPlugins) // Clone.tt Line: 49
                vm.ListPlugins.Add(Plugin.Clone(vm, (Plugin)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListPlugins to, GroupListPlugins from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Plugin(to); // Clone.tt Line: 110
                        Plugin.Update(p, (Plugin)tt, isDeep);
                        to.ListPlugins.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListPlugins ConvertToVM(Proto.Config.proto_group_list_plugins m, GroupListPlugins vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListPlugins) // Clone.tt Line: 191
            {
                var tvm = Plugin.ConvertToVM(t, new Plugin(vm)); // Clone.tt Line: 194
                vm.ListPlugins.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListPlugins' to 'proto_group_list_plugins'
        public static Proto.Config.proto_group_list_plugins ConvertToProto(GroupListPlugins vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_plugins m = new Proto.Config.proto_group_list_plugins(); // Clone.tt Line: 224
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            foreach (var t in vm.ListPlugins) // Clone.tt Line: 227
                m.ListPlugins.Add(Plugin.ConvertToProto((Plugin)t)); // Clone.tt Line: 231
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
        public ConfigNodesCollection<Plugin> ListPlugins // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListPlugins; 
            }
            set
            {
                if (this._ListPlugins != value)
                {
                    this.OnListPluginsChanging(this._ListPlugins, value);
                    this._ListPlugins = value;
                    this.OnListPluginsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Plugin> _ListPlugins;
        partial void OnListPluginsChanging(SortedObservableCollection<Plugin> from, SortedObservableCollection<Plugin> to); // Property.tt Line: 80
        partial void OnListPluginsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IPlugin> IListPlugins { get { foreach (var t in this._ListPlugins) yield return t; } }
        public Plugin this[int index] { get { return (Plugin)this.ListPlugins[index]; } }
        public void Add(Plugin item) // Property.tt Line: 87
        { 
            this.ListPlugins.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Plugin> items) 
        { 
            this.ListPlugins.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListPlugins.Count; 
        }
        public void Remove(Plugin item) 
        {
            this.ListPlugins.Remove(item); 
            item.Parent = null;
        }
    
        #endregion Properties
    }
    public partial class Plugin : ConfigObjectSubBase<Plugin, Plugin.PluginValidator>, IComparable<Plugin>, IConfigAcceptVisitor, IPlugin // Class.tt Line: 6
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Version = from.Version; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.ListGenerators = new ConfigNodesCollection<PluginGenerator>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGenerators) // Clone.tt Line: 49
                vm.ListGenerators.Add(PluginGenerator.Clone(vm, (PluginGenerator)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Plugin to, Plugin from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Version = from.Version; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new PluginGenerator(to); // Clone.tt Line: 110
                        PluginGenerator.Update(p, (PluginGenerator)tt, isDeep);
                        to.ListGenerators.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Plugin ConvertToVM(Proto.Config.proto_plugin m, Plugin vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Version = m.Version; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.ListGenerators = new ConfigNodesCollection<PluginGenerator>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGenerators) // Clone.tt Line: 191
            {
                var tvm = PluginGenerator.ConvertToVM(t, new PluginGenerator(vm)); // Clone.tt Line: 194
                vm.ListGenerators.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Plugin' to 'proto_plugin'
        public static Proto.Config.proto_plugin ConvertToProto(Plugin vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_plugin m = new Proto.Config.proto_plugin(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Version = vm.Version; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            foreach (var t in vm.ListGenerators) // Clone.tt Line: 227
                m.ListGenerators.Add(PluginGenerator.ConvertToProto((PluginGenerator)t)); // Clone.tt Line: 231
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
                    this.OnVersionChanging(this._Version, value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Version = string.Empty;
        partial void OnVersionChanging(string from, string to); // Property.tt Line: 156
        partial void OnVersionChanged();
        
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
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
        public ConfigNodesCollection<PluginGenerator> ListGenerators // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGenerators; 
            }
            set
            {
                if (this._ListGenerators != value)
                {
                    this.OnListGeneratorsChanging(this._ListGenerators, value);
                    this._ListGenerators = value;
                    this.OnListGeneratorsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGenerator> _ListGenerators;
        partial void OnListGeneratorsChanging(SortedObservableCollection<PluginGenerator> from, SortedObservableCollection<PluginGenerator> to); // Property.tt Line: 80
        partial void OnListGeneratorsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IPluginGenerator> IListGenerators { get { foreach (var t in this._ListGenerators) yield return t; } }
    
        #endregion Properties
    }
    public partial class PluginGenerator : ConfigObjectSubBase<PluginGenerator, PluginGenerator.PluginGeneratorValidator>, IComparable<PluginGenerator>, IConfigAcceptVisitor, IPluginGenerator // Class.tt Line: 6
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.ListSettings = new ConfigNodesCollection<PluginGeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListSettings) // Clone.tt Line: 49
                vm.ListSettings.Add(PluginGeneratorSettings.Clone(vm, (PluginGeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(PluginGenerator to, PluginGenerator from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new PluginGeneratorSettings(to); // Clone.tt Line: 110
                        PluginGeneratorSettings.Update(p, (PluginGeneratorSettings)tt, isDeep);
                        to.ListSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static PluginGenerator ConvertToVM(Proto.Config.proto_plugin_generator m, PluginGenerator vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.ListSettings = new ConfigNodesCollection<PluginGeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListSettings) // Clone.tt Line: 191
            {
                var tvm = PluginGeneratorSettings.ConvertToVM(t, new PluginGeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'PluginGenerator' to 'proto_plugin_generator'
        public static Proto.Config.proto_plugin_generator ConvertToProto(PluginGenerator vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_plugin_generator m = new Proto.Config.proto_plugin_generator(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            foreach (var t in vm.ListSettings) // Clone.tt Line: 227
                m.ListSettings.Add(PluginGeneratorSettings.ConvertToProto((PluginGeneratorSettings)t)); // Clone.tt Line: 231
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PluginGeneratorSettings> ListSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListSettings; 
            }
            set
            {
                if (this._ListSettings != value)
                {
                    this.OnListSettingsChanging(this._ListSettings, value);
                    this._ListSettings = value;
                    this.OnListSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PluginGeneratorSettings> _ListSettings;
        partial void OnListSettingsChanging(SortedObservableCollection<PluginGeneratorSettings> from, SortedObservableCollection<PluginGeneratorSettings> to); // Property.tt Line: 80
        partial void OnListSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IPluginGeneratorSettings> IListSettings { get { foreach (var t in this._ListSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class PluginGeneratorSettings : ConfigObjectSubBase<PluginGeneratorSettings, PluginGeneratorSettings.PluginGeneratorSettingsValidator>, IComparable<PluginGeneratorSettings>, IConfigAcceptVisitor, IPluginGeneratorSettings // Class.tt Line: 6
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 62
            vm.IsPrivate = from.IsPrivate; // Clone.tt Line: 62
            vm.FilePath = from.FilePath; // Clone.tt Line: 62
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(PluginGeneratorSettings to, PluginGeneratorSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 134
            to.IsPrivate = from.IsPrivate; // Clone.tt Line: 134
            to.FilePath = from.FilePath; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static PluginGeneratorSettings ConvertToVM(Proto.Config.proto_plugin_generator_settings m, PluginGeneratorSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 211
            vm.IsPrivate = m.IsPrivate; // Clone.tt Line: 211
            vm.FilePath = m.FilePath; // Clone.tt Line: 211
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'PluginGeneratorSettings' to 'proto_plugin_generator_settings'
        public static Proto.Config.proto_plugin_generator_settings ConvertToProto(PluginGeneratorSettings vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_plugin_generator_settings m = new Proto.Config.proto_plugin_generator_settings(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 261
            m.IsPrivate = vm.IsPrivate; // Clone.tt Line: 261
            m.FilePath = vm.FilePath; // Clone.tt Line: 261
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
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
                    this.OnGeneratorSettingsChanging(this._GeneratorSettings, value);
                    this._GeneratorSettings = value;
                    this.OnGeneratorSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _GeneratorSettings = string.Empty;
        partial void OnGeneratorSettingsChanging(string from, string to); // Property.tt Line: 156
        partial void OnGeneratorSettingsChanged();
        
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
                    this.OnIsPrivateChanging(this._IsPrivate, value);
                    this._IsPrivate = value;
                    this.OnIsPrivateChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsPrivate;
        partial void OnIsPrivateChanging(bool from, bool to); // Property.tt Line: 156
        partial void OnIsPrivateChanged();
        
        [PropertyOrderAttribute(4)]
        [Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
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
                    this.OnFilePathChanging(this._FilePath, value);
                    this._FilePath = value;
                    this.OnFilePathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _FilePath = string.Empty;
        partial void OnFilePathChanging(string from, string to); // Property.tt Line: 156
        partial void OnFilePathChanged();
    
        #endregion Properties
    }
    public partial class SettingsConfig : ConfigObjectSubBase<SettingsConfig, SettingsConfig.SettingsConfigValidator>, IComparable<SettingsConfig>, IConfigAcceptVisitor, ISettingsConfig // Class.tt Line: 6
    {
        public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { } // Class.tt Line: 8
        #region CTOR
        public SettingsConfig(ITreeConfigNode parent) 
            : base(parent, SettingsConfigValidator.Validator) // Class.tt Line: 12
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
        public static SettingsConfig Clone(ITreeConfigNode parent, SettingsConfig from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            SettingsConfig vm = new SettingsConfig(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 62
            vm.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 62
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(SettingsConfig to, SettingsConfig from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 134
            to.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
        #region IEditable
        public override SettingsConfig Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return SettingsConfig.Clone(this.Parent, this);
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
        public static SettingsConfig ConvertToVM(Proto.Config.proto_settings_config m, SettingsConfig vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.VersionMigrationCurrent = m.VersionMigrationCurrent; // Clone.tt Line: 211
            vm.VersionMigrationSupportFromMin = m.VersionMigrationSupportFromMin; // Clone.tt Line: 211
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'SettingsConfig' to 'proto_settings_config'
        public static Proto.Config.proto_settings_config ConvertToProto(SettingsConfig vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_settings_config m = new Proto.Config.proto_settings_config(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.VersionMigrationCurrent = vm.VersionMigrationCurrent; // Clone.tt Line: 261
            m.VersionMigrationSupportFromMin = vm.VersionMigrationSupportFromMin; // Clone.tt Line: 261
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
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
                    this.OnVersionMigrationCurrentChanging(this._VersionMigrationCurrent, value);
                    this._VersionMigrationCurrent = value;
                    this.OnVersionMigrationCurrentChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private int _VersionMigrationCurrent;
        partial void OnVersionMigrationCurrentChanging(int from, int to); // Property.tt Line: 156
        partial void OnVersionMigrationCurrentChanged();
        
        
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
                    this.OnVersionMigrationSupportFromMinChanging(this._VersionMigrationSupportFromMin, value);
                    this._VersionMigrationSupportFromMin = value;
                    this.OnVersionMigrationSupportFromMinChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private int _VersionMigrationSupportFromMin;
        partial void OnVersionMigrationSupportFromMinChanging(int from, int to); // Property.tt Line: 156
        partial void OnVersionMigrationSupportFromMinChanged();
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// General DB settings
    ///////////////////////////////////////////////////
    public partial class DbSettings : ViewModelValidatableWithSeverity<DbSettings, DbSettings.DbSettingsValidator>, IDbSettings // Class.tt Line: 6
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
            vm.DbSchema = from.DbSchema; // Clone.tt Line: 62
            vm.IdGenerator = from.IdGenerator; // Clone.tt Line: 62
            vm.PKeyType = from.PKeyType; // Clone.tt Line: 62
            vm.KeyName = from.KeyName; // Clone.tt Line: 62
            vm.Timestamp = from.Timestamp; // Clone.tt Line: 62
            vm.IsDbFromConnectionString = from.IsDbFromConnectionString; // Clone.tt Line: 62
            vm.ConnectionStringName = from.ConnectionStringName; // Clone.tt Line: 62
            vm.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(DbSettings to, DbSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.DbSchema = from.DbSchema; // Clone.tt Line: 134
            to.IdGenerator = from.IdGenerator; // Clone.tt Line: 134
            to.PKeyType = from.PKeyType; // Clone.tt Line: 134
            to.KeyName = from.KeyName; // Clone.tt Line: 134
            to.Timestamp = from.Timestamp; // Clone.tt Line: 134
            to.IsDbFromConnectionString = from.IsDbFromConnectionString; // Clone.tt Line: 134
            to.ConnectionStringName = from.ConnectionStringName; // Clone.tt Line: 134
            to.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static DbSettings ConvertToVM(Proto.Config.db_settings m, DbSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.DbSchema = m.DbSchema; // Clone.tt Line: 211
            vm.IdGenerator = (DbIdGeneratorMethod)m.IdGenerator; // Clone.tt Line: 211
            vm.PKeyType = (EnumPrimaryKeyType)m.PKeyType; // Clone.tt Line: 211
            vm.KeyName = m.KeyName; // Clone.tt Line: 211
            vm.Timestamp = m.Timestamp; // Clone.tt Line: 211
            vm.IsDbFromConnectionString = m.IsDbFromConnectionString; // Clone.tt Line: 211
            vm.ConnectionStringName = m.ConnectionStringName; // Clone.tt Line: 211
            vm.PathToProjectWithConnectionString = m.PathToProjectWithConnectionString; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'DbSettings' to 'db_settings'
        public static Proto.Config.db_settings ConvertToProto(DbSettings vm) // Clone.tt Line: 222
        {
            Proto.Config.db_settings m = new Proto.Config.db_settings(); // Clone.tt Line: 224
            m.DbSchema = vm.DbSchema; // Clone.tt Line: 261
            m.IdGenerator = (Proto.Config.db_id_generator_method)vm.IdGenerator; // Clone.tt Line: 259
            m.PKeyType = (Proto.Config.proto_enum_primary_key_type)vm.PKeyType; // Clone.tt Line: 259
            m.KeyName = vm.KeyName; // Clone.tt Line: 261
            m.Timestamp = vm.Timestamp; // Clone.tt Line: 261
            m.IsDbFromConnectionString = vm.IsDbFromConnectionString; // Clone.tt Line: 261
            m.ConnectionStringName = vm.ConnectionStringName; // Clone.tt Line: 261
            m.PathToProjectWithConnectionString = vm.PathToProjectWithConnectionString; // Clone.tt Line: 261
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
                    this.OnDbSchemaChanging(this._DbSchema, value);
                    this._DbSchema = value;
                    this.OnDbSchemaChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _DbSchema = string.Empty;
        partial void OnDbSchemaChanging(string from, string to); // Property.tt Line: 156
        partial void OnDbSchemaChanged();
        
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
                    this.OnIdGeneratorChanging(this._IdGenerator, value);
                    this._IdGenerator = value;
                    this.OnIdGeneratorChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private DbIdGeneratorMethod _IdGenerator;
        partial void OnIdGeneratorChanging(DbIdGeneratorMethod from, DbIdGeneratorMethod to); // Property.tt Line: 156
        partial void OnIdGeneratorChanged();
        
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
                    this.OnPKeyTypeChanging(this._PKeyType, value);
                    this._PKeyType = value;
                    this.OnPKeyTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private EnumPrimaryKeyType _PKeyType;
        partial void OnPKeyTypeChanging(EnumPrimaryKeyType from, EnumPrimaryKeyType to); // Property.tt Line: 156
        partial void OnPKeyTypeChanged();
        
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
                    this.OnKeyNameChanging(this._KeyName, value);
                    this._KeyName = value;
                    this.OnKeyNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _KeyName = string.Empty;
        partial void OnKeyNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnKeyNameChanged();
        
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
                    this.OnTimestampChanging(this._Timestamp, value);
                    this._Timestamp = value;
                    this.OnTimestampChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Timestamp = string.Empty;
        partial void OnTimestampChanging(string from, string to); // Property.tt Line: 156
        partial void OnTimestampChanged();
        
        
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
                    this.OnIsDbFromConnectionStringChanging(this._IsDbFromConnectionString, value);
                    this._IsDbFromConnectionString = value;
                    this.OnIsDbFromConnectionStringChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsDbFromConnectionString;
        partial void OnIsDbFromConnectionStringChanging(bool from, bool to); // Property.tt Line: 156
        partial void OnIsDbFromConnectionStringChanged();
        
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
                    this.OnConnectionStringNameChanging(this._ConnectionStringName, value);
                    this._ConnectionStringName = value;
                    this.OnConnectionStringNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _ConnectionStringName = string.Empty;
        partial void OnConnectionStringNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnConnectionStringNameChanged();
        
        
        ///////////////////////////////////////////////////
        /// path to project with config file containing connection string. Usefull for UNIT tests.
        /// it will override previous settings
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(4)]
        [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
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
                    this.OnPathToProjectWithConnectionStringChanging(this._PathToProjectWithConnectionString, value);
                    this._PathToProjectWithConnectionString = value;
                    this.OnPathToProjectWithConnectionStringChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _PathToProjectWithConnectionString = string.Empty;
        partial void OnPathToProjectWithConnectionStringChanging(string from, string to); // Property.tt Line: 156
        partial void OnPathToProjectWithConnectionStringChanged();
    
        #endregion Properties
    }
    public partial class ConfigShortHistory : ConfigObjectSubBase<ConfigShortHistory, ConfigShortHistory.ConfigShortHistoryValidator>, IComparable<ConfigShortHistory>, IConfigAcceptVisitor, IConfigShortHistory // Class.tt Line: 6
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
            if (isDeep) // Clone.tt Line: 59
                vm.CurrentConfig = Config.Clone(vm, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.PrevStableConfig = Config.Clone(vm, from.PrevStableConfig, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.OldStableConfig = Config.Clone(vm, from.OldStableConfig, isDeep);
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(ConfigShortHistory to, ConfigShortHistory from, bool isDeep = true) // Clone.tt Line: 72
        {
            if (isDeep) // Clone.tt Line: 131
                Config.Update(to.CurrentConfig, from.CurrentConfig, isDeep);
            if (isDeep) // Clone.tt Line: 131
                Config.Update(to.PrevStableConfig, from.PrevStableConfig, isDeep);
            if (isDeep) // Clone.tt Line: 131
                Config.Update(to.OldStableConfig, from.OldStableConfig, isDeep);
        }
        // Clone.tt Line: 140
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
        public static ConfigShortHistory ConvertToVM(Proto.Config.proto_config_short_history m, ConfigShortHistory vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            if (vm.CurrentConfig == null) // Clone.tt Line: 203
                vm.CurrentConfig = new Config(vm); // Clone.tt Line: 205
            Config.ConvertToVM(m.CurrentConfig, vm.CurrentConfig); // Clone.tt Line: 209
            if (vm.PrevStableConfig == null) // Clone.tt Line: 203
                vm.PrevStableConfig = new Config(vm); // Clone.tt Line: 205
            Config.ConvertToVM(m.PrevStableConfig, vm.PrevStableConfig); // Clone.tt Line: 209
            if (vm.OldStableConfig == null) // Clone.tt Line: 203
                vm.OldStableConfig = new Config(vm); // Clone.tt Line: 205
            Config.ConvertToVM(m.OldStableConfig, vm.OldStableConfig); // Clone.tt Line: 209
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'ConfigShortHistory' to 'proto_config_short_history'
        public static Proto.Config.proto_config_short_history ConvertToProto(ConfigShortHistory vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_config_short_history m = new Proto.Config.proto_config_short_history(); // Clone.tt Line: 224
            m.CurrentConfig = Config.ConvertToProto(vm.CurrentConfig); // Clone.tt Line: 255
            m.PrevStableConfig = Config.ConvertToProto(vm.PrevStableConfig); // Clone.tt Line: 255
            m.OldStableConfig = Config.ConvertToProto(vm.OldStableConfig); // Clone.tt Line: 255
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
        
        public Config CurrentConfig // Property.tt Line: 109
        { 
            get 
            { 
                return this._CurrentConfig; 
            }
            set
            {
                if (this._CurrentConfig != value)
                {
                    this.OnCurrentConfigChanging(this._CurrentConfig, value);
                    this._CurrentConfig = value;
                    this.OnCurrentConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _CurrentConfig;
        partial void OnCurrentConfigChanging(Config from, Config to); // Property.tt Line: 130
        partial void OnCurrentConfigChanged();
        [BrowsableAttribute(false)]
        public IConfig ICurrentConfig { get { return this._CurrentConfig; } }
        
        public Config PrevStableConfig // Property.tt Line: 109
        { 
            get 
            { 
                return this._PrevStableConfig; 
            }
            set
            {
                if (this._PrevStableConfig != value)
                {
                    this.OnPrevStableConfigChanging(this._PrevStableConfig, value);
                    this._PrevStableConfig = value;
                    this.OnPrevStableConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _PrevStableConfig;
        partial void OnPrevStableConfigChanging(Config from, Config to); // Property.tt Line: 130
        partial void OnPrevStableConfigChanged();
        [BrowsableAttribute(false)]
        public IConfig IPrevStableConfig { get { return this._PrevStableConfig; } }
        
        public Config OldStableConfig // Property.tt Line: 109
        { 
            get 
            { 
                return this._OldStableConfig; 
            }
            set
            {
                if (this._OldStableConfig != value)
                {
                    this.OnOldStableConfigChanging(this._OldStableConfig, value);
                    this._OldStableConfig = value;
                    this.OnOldStableConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _OldStableConfig;
        partial void OnOldStableConfigChanging(Config from, Config to); // Property.tt Line: 130
        partial void OnOldStableConfigChanged();
        [BrowsableAttribute(false)]
        public IConfig IOldStableConfig { get { return this._OldStableConfig; } }
    
        #endregion Properties
    }
    public partial class GroupListBaseConfigLinks : ConfigObjectSubBase<GroupListBaseConfigLinks, GroupListBaseConfigLinks.GroupListBaseConfigLinksValidator>, IComparable<GroupListBaseConfigLinks>, IConfigAcceptVisitor, IGroupListBaseConfigLinks // Class.tt Line: 6
    {
        public partial class GroupListBaseConfigLinksValidator : ValidatorBase<GroupListBaseConfigLinks, GroupListBaseConfigLinksValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListBaseConfigLinks(ITreeConfigNode parent) 
            : base(parent, GroupListBaseConfigLinksValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(this); // Class.tt Line: 22
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
        }
        public static GroupListBaseConfigLinks Clone(ITreeConfigNode parent, GroupListBaseConfigLinks from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListBaseConfigLinks vm = new GroupListBaseConfigLinks(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListBaseConfigLinks) // Clone.tt Line: 49
                vm.ListBaseConfigLinks.Add(BaseConfigLink.Clone(vm, (BaseConfigLink)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListBaseConfigLinks to, GroupListBaseConfigLinks from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new BaseConfigLink(to); // Clone.tt Line: 110
                        BaseConfigLink.Update(p, (BaseConfigLink)tt, isDeep);
                        to.ListBaseConfigLinks.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListBaseConfigLinks ConvertToVM(Proto.Config.proto_group_list_base_config_links m, GroupListBaseConfigLinks vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListBaseConfigLinks) // Clone.tt Line: 191
            {
                var tvm = BaseConfigLink.ConvertToVM(t, new BaseConfigLink(vm)); // Clone.tt Line: 194
                vm.ListBaseConfigLinks.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListBaseConfigLinks' to 'proto_group_list_base_config_links'
        public static Proto.Config.proto_group_list_base_config_links ConvertToProto(GroupListBaseConfigLinks vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_base_config_links m = new Proto.Config.proto_group_list_base_config_links(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListBaseConfigLinks) // Clone.tt Line: 227
                m.ListBaseConfigLinks.Add(BaseConfigLink.ConvertToProto((BaseConfigLink)t)); // Clone.tt Line: 231
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<BaseConfigLink> ListBaseConfigLinks // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListBaseConfigLinks; 
            }
            set
            {
                if (this._ListBaseConfigLinks != value)
                {
                    this.OnListBaseConfigLinksChanging(this._ListBaseConfigLinks, value);
                    this._ListBaseConfigLinks = value;
                    this.OnListBaseConfigLinksChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<BaseConfigLink> _ListBaseConfigLinks;
        partial void OnListBaseConfigLinksChanging(SortedObservableCollection<BaseConfigLink> from, SortedObservableCollection<BaseConfigLink> to); // Property.tt Line: 80
        partial void OnListBaseConfigLinksChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IBaseConfigLink> IListBaseConfigLinks { get { foreach (var t in this._ListBaseConfigLinks) yield return t; } }
        public BaseConfigLink this[int index] { get { return (BaseConfigLink)this.ListBaseConfigLinks[index]; } }
        public void Add(BaseConfigLink item) // Property.tt Line: 87
        { 
            this.ListBaseConfigLinks.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<BaseConfigLink> items) 
        { 
            this.ListBaseConfigLinks.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListBaseConfigLinks.Count; 
        }
        public void Remove(BaseConfigLink item) 
        {
            this.ListBaseConfigLinks.Remove(item); 
            item.Parent = null;
        }
    
        #endregion Properties
    }
    public partial class BaseConfigLink : ConfigObjectSubBase<BaseConfigLink, BaseConfigLink.BaseConfigLinkValidator>, IComparable<BaseConfigLink>, IConfigAcceptVisitor, IBaseConfigLink // Class.tt Line: 6
    {
        public partial class BaseConfigLinkValidator : ValidatorBase<BaseConfigLink, BaseConfigLinkValidator> { } // Class.tt Line: 8
        #region CTOR
        public BaseConfigLink(ITreeConfigNode parent) 
            : base(parent, BaseConfigLinkValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.Config = new Config(this); // Class.tt Line: 28
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
        public static BaseConfigLink Clone(ITreeConfigNode parent, BaseConfigLink from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            BaseConfigLink vm = new BaseConfigLink(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.Config = Config.Clone(vm, from.Config, isDeep);
            vm.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 62
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(BaseConfigLink to, BaseConfigLink from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                Config.Update(to.Config, from.Config, isDeep);
            to.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static BaseConfigLink ConvertToVM(Proto.Config.proto_base_config_link m, BaseConfigLink vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.Config == null) // Clone.tt Line: 203
                vm.Config = new Config(vm); // Clone.tt Line: 205
            Config.ConvertToVM(m.Config, vm.Config); // Clone.tt Line: 209
            vm.RelativeConfigFilePath = m.RelativeConfigFilePath; // Clone.tt Line: 211
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'BaseConfigLink' to 'proto_base_config_link'
        public static Proto.Config.proto_base_config_link ConvertToProto(BaseConfigLink vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_base_config_link m = new Proto.Config.proto_base_config_link(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.Config = Config.ConvertToProto(vm.Config); // Clone.tt Line: 255
            m.RelativeConfigFilePath = vm.RelativeConfigFilePath; // Clone.tt Line: 261
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public Config Config // Property.tt Line: 109
        { 
            get 
            { 
                return this._Config; 
            }
            set
            {
                if (this._Config != value)
                {
                    this.OnConfigChanging(this._Config, value);
                    this._Config = value;
                    this.OnConfigChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Config _Config;
        partial void OnConfigChanging(Config from, Config to); // Property.tt Line: 130
        partial void OnConfigChanged();
        [BrowsableAttribute(false)]
        public IConfig IConfig { get { return this._Config; } }
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
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
                    this.OnRelativeConfigFilePathChanging(this._RelativeConfigFilePath, value);
                    this._RelativeConfigFilePath = value;
                    this.OnRelativeConfigFilePathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _RelativeConfigFilePath = string.Empty;
        partial void OnRelativeConfigFilePathChanging(string from, string to); // Property.tt Line: 156
        partial void OnRelativeConfigFilePathChanged();
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Configuration config
    ///////////////////////////////////////////////////
    public partial class Config : ConfigObjectSubBase<Config, Config.ConfigValidator>, IComparable<Config>, IConfigAcceptVisitor, IConfig // Class.tt Line: 6
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Version = from.Version; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.LastUpdated = from.LastUpdated; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.DbSettings = DbSettings.Clone(from.DbSettings, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupConfigLinks = GroupListBaseConfigLinks.Clone(vm, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.Model = ConfigModel.Clone(vm, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupPlugins = GroupListPlugins.Clone(vm, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupAppSolutions = GroupListAppSolutions.Clone(vm, from.GroupAppSolutions, isDeep);
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Config to, Config from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Version = from.Version; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.LastUpdated = from.LastUpdated; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                DbSettings.Update(to.DbSettings, from.DbSettings, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListBaseConfigLinks.Update(to.GroupConfigLinks, from.GroupConfigLinks, isDeep);
            if (isDeep) // Clone.tt Line: 131
                ConfigModel.Update(to.Model, from.Model, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListPlugins.Update(to.GroupPlugins, from.GroupPlugins, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListAppSolutions.Update(to.GroupAppSolutions, from.GroupAppSolutions, isDeep);
        }
        // Clone.tt Line: 140
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
        public static Config ConvertToVM(Proto.Config.proto_config m, Config vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Version = m.Version; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.LastUpdated = m.LastUpdated; // Clone.tt Line: 211
            if (vm.DbSettings == null) // Clone.tt Line: 203
                vm.DbSettings = new DbSettings(); // Clone.tt Line: 207
            DbSettings.ConvertToVM(m.DbSettings, vm.DbSettings); // Clone.tt Line: 209
            if (vm.GroupConfigLinks == null) // Clone.tt Line: 203
                vm.GroupConfigLinks = new GroupListBaseConfigLinks(vm); // Clone.tt Line: 205
            GroupListBaseConfigLinks.ConvertToVM(m.GroupConfigLinks, vm.GroupConfigLinks); // Clone.tt Line: 209
            if (vm.Model == null) // Clone.tt Line: 203
                vm.Model = new ConfigModel(vm); // Clone.tt Line: 205
            ConfigModel.ConvertToVM(m.Model, vm.Model); // Clone.tt Line: 209
            if (vm.GroupPlugins == null) // Clone.tt Line: 203
                vm.GroupPlugins = new GroupListPlugins(vm); // Clone.tt Line: 205
            GroupListPlugins.ConvertToVM(m.GroupPlugins, vm.GroupPlugins); // Clone.tt Line: 209
            if (vm.GroupAppSolutions == null) // Clone.tt Line: 203
                vm.GroupAppSolutions = new GroupListAppSolutions(vm); // Clone.tt Line: 205
            GroupListAppSolutions.ConvertToVM(m.GroupAppSolutions, vm.GroupAppSolutions); // Clone.tt Line: 209
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Config' to 'proto_config'
        public static Proto.Config.proto_config ConvertToProto(Config vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_config m = new Proto.Config.proto_config(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Version = vm.Version; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.LastUpdated = vm.LastUpdated; // Clone.tt Line: 261
            m.DbSettings = DbSettings.ConvertToProto(vm.DbSettings); // Clone.tt Line: 255
            m.GroupConfigLinks = GroupListBaseConfigLinks.ConvertToProto(vm.GroupConfigLinks); // Clone.tt Line: 255
            m.Model = ConfigModel.ConvertToProto(vm.Model); // Clone.tt Line: 255
            m.GroupPlugins = GroupListPlugins.ConvertToProto(vm.GroupPlugins); // Clone.tt Line: 255
            m.GroupAppSolutions = GroupListAppSolutions.ConvertToProto(vm.GroupAppSolutions); // Clone.tt Line: 255
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
                    this.OnVersionChanging(this._Version, value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private int _Version;
        partial void OnVersionChanging(int from, int to); // Property.tt Line: 156
        partial void OnVersionChanged();
        
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
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
                    this.OnLastUpdatedChanging(this._LastUpdated, value);
                    this._LastUpdated = value;
                    this.OnLastUpdatedChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private Google.Protobuf.WellKnownTypes.Timestamp _LastUpdated;
        partial void OnLastUpdatedChanging(Google.Protobuf.WellKnownTypes.Timestamp from, Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 156
        partial void OnLastUpdatedChanged();
        
        
        ///////////////////////////////////////////////////
        /// GENERAL DB SETTINGS
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        public DbSettings DbSettings // Property.tt Line: 109
        { 
            get 
            { 
                return this._DbSettings; 
            }
            set
            {
                if (this._DbSettings != value)
                {
                    this.OnDbSettingsChanging(this._DbSettings, value);
                    this._DbSettings = value;
                    this.OnDbSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private DbSettings _DbSettings;
        partial void OnDbSettingsChanging(DbSettings from, DbSettings to); // Property.tt Line: 130
        partial void OnDbSettingsChanged();
        [BrowsableAttribute(false)]
        public IDbSettings IDbSettings { get { return this._DbSettings; } }
        
        [BrowsableAttribute(false)]
        public GroupListBaseConfigLinks GroupConfigLinks // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupConfigLinks; 
            }
            set
            {
                if (this._GroupConfigLinks != value)
                {
                    this.OnGroupConfigLinksChanging(this._GroupConfigLinks, value);
                    this._GroupConfigLinks = value;
                    this.OnGroupConfigLinksChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListBaseConfigLinks _GroupConfigLinks;
        partial void OnGroupConfigLinksChanging(GroupListBaseConfigLinks from, GroupListBaseConfigLinks to); // Property.tt Line: 130
        partial void OnGroupConfigLinksChanged();
        [BrowsableAttribute(false)]
        public IGroupListBaseConfigLinks IGroupConfigLinks { get { return this._GroupConfigLinks; } }
        
        [BrowsableAttribute(false)]
        public ConfigModel Model // Property.tt Line: 109
        { 
            get 
            { 
                return this._Model; 
            }
            set
            {
                if (this._Model != value)
                {
                    this.OnModelChanging(this._Model, value);
                    this._Model = value;
                    this.OnModelChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigModel _Model;
        partial void OnModelChanging(ConfigModel from, ConfigModel to); // Property.tt Line: 130
        partial void OnModelChanged();
        [BrowsableAttribute(false)]
        public IConfigModel IModel { get { return this._Model; } }
        
        [BrowsableAttribute(false)]
        public GroupListPlugins GroupPlugins // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupPlugins; 
            }
            set
            {
                if (this._GroupPlugins != value)
                {
                    this.OnGroupPluginsChanging(this._GroupPlugins, value);
                    this._GroupPlugins = value;
                    this.OnGroupPluginsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPlugins _GroupPlugins;
        partial void OnGroupPluginsChanging(GroupListPlugins from, GroupListPlugins to); // Property.tt Line: 130
        partial void OnGroupPluginsChanged();
        [BrowsableAttribute(false)]
        public IGroupListPlugins IGroupPlugins { get { return this._GroupPlugins; } }
        
        [BrowsableAttribute(false)]
        public GroupListAppSolutions GroupAppSolutions // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupAppSolutions; 
            }
            set
            {
                if (this._GroupAppSolutions != value)
                {
                    this.OnGroupAppSolutionsChanging(this._GroupAppSolutions, value);
                    this._GroupAppSolutions = value;
                    this.OnGroupAppSolutionsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListAppSolutions _GroupAppSolutions;
        partial void OnGroupAppSolutionsChanging(GroupListAppSolutions from, GroupListAppSolutions to); // Property.tt Line: 130
        partial void OnGroupAppSolutionsChanged();
        [BrowsableAttribute(false)]
        public IGroupListAppSolutions IGroupAppSolutions { get { return this._GroupAppSolutions; } }
    
        #endregion Properties
    }
    public partial class AppDbSettings : ViewModelValidatableWithSeverity<AppDbSettings, AppDbSettings.AppDbSettingsValidator>, IAppDbSettings // Class.tt Line: 6
    {
        public partial class AppDbSettingsValidator : ValidatorBase<AppDbSettings, AppDbSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppDbSettings() 
            : base(AppDbSettingsValidator.Validator) // Class.tt Line: 38
        {
            this.OnInitBegin();
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        public static AppDbSettings Clone(AppDbSettings from, bool isDeep = true) // Clone.tt Line: 27
        {
            AppDbSettings vm = new AppDbSettings();
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 62
            vm.PluginName = from.PluginName; // Clone.tt Line: 62
            vm.Version = from.Version; // Clone.tt Line: 62
            vm.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 62
            vm.PluginGenName = from.PluginGenName; // Clone.tt Line: 62
            vm.ConnGuid = from.ConnGuid; // Clone.tt Line: 62
            vm.ConnName = from.ConnName; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(AppDbSettings to, AppDbSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.PluginGuid = from.PluginGuid; // Clone.tt Line: 134
            to.PluginName = from.PluginName; // Clone.tt Line: 134
            to.Version = from.Version; // Clone.tt Line: 134
            to.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 134
            to.PluginGenName = from.PluginGenName; // Clone.tt Line: 134
            to.ConnGuid = from.ConnGuid; // Clone.tt Line: 134
            to.ConnName = from.ConnName; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static AppDbSettings ConvertToVM(Proto.Config.proto_app_db_settings m, AppDbSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 211
            vm.PluginName = m.PluginName; // Clone.tt Line: 211
            vm.Version = m.Version; // Clone.tt Line: 211
            vm.PluginGenGuid = m.PluginGenGuid; // Clone.tt Line: 211
            vm.PluginGenName = m.PluginGenName; // Clone.tt Line: 211
            vm.ConnGuid = m.ConnGuid; // Clone.tt Line: 211
            vm.ConnName = m.ConnName; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'AppDbSettings' to 'proto_app_db_settings'
        public static Proto.Config.proto_app_db_settings ConvertToProto(AppDbSettings vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_app_db_settings m = new Proto.Config.proto_app_db_settings(); // Clone.tt Line: 224
            m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 261
            m.PluginName = vm.PluginName; // Clone.tt Line: 261
            m.Version = vm.Version; // Clone.tt Line: 261
            m.PluginGenGuid = vm.PluginGenGuid; // Clone.tt Line: 261
            m.PluginGenName = vm.PluginGenName; // Clone.tt Line: 261
            m.ConnGuid = vm.ConnGuid; // Clone.tt Line: 261
            m.ConnName = vm.ConnName; // Clone.tt Line: 261
            return m;
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
                    this.OnPluginGuidChanging(this._PluginGuid, value);
                    this._PluginGuid = value;
                    this.OnPluginGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _PluginGuid = string.Empty;
        partial void OnPluginGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnPluginGuidChanged();
        
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
                    this.OnPluginNameChanging(this._PluginName, value);
                    this._PluginName = value;
                    this.OnPluginNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _PluginName = string.Empty;
        partial void OnPluginNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnPluginNameChanged();
        
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
                    this.OnVersionChanging(this._Version, value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Version = string.Empty;
        partial void OnVersionChanging(string from, string to); // Property.tt Line: 156
        partial void OnVersionChanged();
        
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
                    this.OnPluginGenGuidChanging(this._PluginGenGuid, value);
                    this._PluginGenGuid = value;
                    this.OnPluginGenGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _PluginGenGuid = string.Empty;
        partial void OnPluginGenGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnPluginGenGuidChanged();
        
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
                    this.OnPluginGenNameChanging(this._PluginGenName, value);
                    this._PluginGenName = value;
                    this.OnPluginGenNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _PluginGenName = string.Empty;
        partial void OnPluginGenNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnPluginGenNameChanged();
        
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
                    this.OnConnGuidChanging(this._ConnGuid, value);
                    this._ConnGuid = value;
                    this.OnConnGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _ConnGuid = string.Empty;
        partial void OnConnGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnConnGuidChanged();
        
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
                    this.OnConnNameChanging(this._ConnName, value);
                    this._ConnName = value;
                    this.OnConnNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _ConnName = string.Empty;
        partial void OnConnNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnConnNameChanged();
    
        #endregion Properties
    }
    public partial class GroupListAppSolutions : ConfigObjectSubBase<GroupListAppSolutions, GroupListAppSolutions.GroupListAppSolutionsValidator>, IComparable<GroupListAppSolutions>, IConfigAcceptVisitor, IGroupListAppSolutions // Class.tt Line: 6
    {
        public partial class GroupListAppSolutionsValidator : ValidatorBase<GroupListAppSolutions, GroupListAppSolutionsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListAppSolutions(ITreeConfigNode parent) 
            : base(parent, GroupListAppSolutionsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DefaultDb = new AppDbSettings(); // Class.tt Line: 26
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.DefaultDb = AppDbSettings.Clone(from.DefaultDb, isDeep);
            vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListAppSolutions) // Clone.tt Line: 49
                vm.ListAppSolutions.Add(AppSolution.Clone(vm, (AppSolution)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListAppSolutions to, GroupListAppSolutions from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new AppSolution(to); // Clone.tt Line: 110
                        AppSolution.Update(p, (AppSolution)tt, isDeep);
                        to.ListAppSolutions.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListAppSolutions ConvertToVM(Proto.Config.proto_group_list_app_solutions m, GroupListAppSolutions vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.DefaultDb == null) // Clone.tt Line: 203
                vm.DefaultDb = new AppDbSettings(); // Clone.tt Line: 207
            AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 209
            vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListAppSolutions) // Clone.tt Line: 191
            {
                var tvm = AppSolution.ConvertToVM(t, new AppSolution(vm)); // Clone.tt Line: 194
                vm.ListAppSolutions.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListAppSolutions' to 'proto_group_list_app_solutions'
        public static Proto.Config.proto_group_list_app_solutions ConvertToProto(GroupListAppSolutions vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_app_solutions m = new Proto.Config.proto_group_list_app_solutions(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 255
            foreach (var t in vm.ListAppSolutions) // Clone.tt Line: 227
                m.ListAppSolutions.Add(AppSolution.ConvertToProto((AppSolution)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(3)]
        [ExpandableObjectAttribute()]
        [DisplayName("Default DB")]
        public AppDbSettings DefaultDb // Property.tt Line: 109
        { 
            get 
            { 
                return this._DefaultDb; 
            }
            set
            {
                if (this._DefaultDb != value)
                {
                    this.OnDefaultDbChanging(this._DefaultDb, value);
                    this._DefaultDb = value;
                    this.OnDefaultDbChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private AppDbSettings _DefaultDb;
        partial void OnDefaultDbChanging(AppDbSettings from, AppDbSettings to); // Property.tt Line: 130
        partial void OnDefaultDbChanged();
        [BrowsableAttribute(false)]
        public IAppDbSettings IDefaultDb { get { return this._DefaultDb; } }
        
        
        ///////////////////////////////////////////////////
        /// List NET solutions
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppSolution> ListAppSolutions // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListAppSolutions; 
            }
            set
            {
                if (this._ListAppSolutions != value)
                {
                    this.OnListAppSolutionsChanging(this._ListAppSolutions, value);
                    this._ListAppSolutions = value;
                    this.OnListAppSolutionsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppSolution> _ListAppSolutions;
        partial void OnListAppSolutionsChanging(SortedObservableCollection<AppSolution> from, SortedObservableCollection<AppSolution> to); // Property.tt Line: 80
        partial void OnListAppSolutionsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IAppSolution> IListAppSolutions { get { foreach (var t in this._ListAppSolutions) yield return t; } }
        public AppSolution this[int index] { get { return (AppSolution)this.ListAppSolutions[index]; } }
        public void Add(AppSolution item) // Property.tt Line: 87
        { 
            this.ListAppSolutions.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<AppSolution> items) 
        { 
            this.ListAppSolutions.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListAppSolutions.Count; 
        }
        public void Remove(AppSolution item) 
        {
            this.ListAppSolutions.Remove(item); 
            item.Parent = null;
        }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// message proto_plugins_group {
    /// // @attr [PropertyOrderAttribute(1)]
    /// // @attr [DisplayName("Group")]
    /// // @attr [Description("Group compatible plugins of one vendor")]
    /// // @attr [Editor(typeof(EditorPluginsGroupSelection), typeof(EditorPluginsGroupSelection))]
    /// string plugins_group_guid = 1;
    /// // @attr [PropertyOrderAttribute(2)]
    /// // @attr [DisplayName("Info")]
    /// // @attr [Description("Plugin short information")]
    /// // @attr [ReadOnly(true)]
    /// string plugins_group_info = 2;
    /// // @attr [PropertyOrderAttribute(3)]
    /// // @attr [DisplayName("Settings")]
    /// // @attr [Description("Global settings for plugin generators")]
    /// // @attr [ExpandableObjectAttribute()]
    /// string plugins_global_settins = 3;
    /// }
    ///////////////////////////////////////////////////
    public partial class AppSolution : ConfigObjectSubBase<AppSolution, AppSolution.AppSolutionValidator>, IComparable<AppSolution>, IConfigAcceptVisitor, IAppSolution // Class.tt Line: 6
    {
        public partial class AppSolutionValidator : ValidatorBase<AppSolution, AppSolutionValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppSolution(ITreeConfigNode parent) 
            : base(parent, AppSolutionValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DefaultDb = new AppDbSettings(); // Class.tt Line: 26
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.DefaultDb = AppDbSettings.Clone(from.DefaultDb, isDeep);
            vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListAppProjects) // Clone.tt Line: 49
                vm.ListAppProjects.Add(AppProject.Clone(vm, (AppProject)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(AppSolution to, AppSolution from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new AppProject(to); // Clone.tt Line: 110
                        AppProject.Update(p, (AppProject)tt, isDeep);
                        to.ListAppProjects.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static AppSolution ConvertToVM(Proto.Config.proto_app_solution m, AppSolution vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.RelativeAppSolutionPath = m.RelativeAppSolutionPath; // Clone.tt Line: 211
            if (vm.DefaultDb == null) // Clone.tt Line: 203
                vm.DefaultDb = new AppDbSettings(); // Clone.tt Line: 207
            AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 209
            vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListAppProjects) // Clone.tt Line: 191
            {
                var tvm = AppProject.ConvertToVM(t, new AppProject(vm)); // Clone.tt Line: 194
                vm.ListAppProjects.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'AppSolution' to 'proto_app_solution'
        public static Proto.Config.proto_app_solution ConvertToProto(AppSolution vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_app_solution m = new Proto.Config.proto_app_solution(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.RelativeAppSolutionPath = vm.RelativeAppSolutionPath; // Clone.tt Line: 261
            m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 255
            foreach (var t in vm.ListAppProjects) // Clone.tt Line: 227
                m.ListAppProjects.Add(AppProject.ConvertToProto((AppProject)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// List NET projects
        ///////////////////////////////////////////////////
        [PropertyOrderAttribute(6)]
        [DisplayName("Path")]
        [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
        [Description("Relative path to solution folder")]
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
                    this.OnRelativeAppSolutionPathChanging(this._RelativeAppSolutionPath, value);
                    this._RelativeAppSolutionPath = value;
                    this.OnRelativeAppSolutionPathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _RelativeAppSolutionPath = string.Empty;
        partial void OnRelativeAppSolutionPathChanging(string from, string to); // Property.tt Line: 156
        partial void OnRelativeAppSolutionPathChanged();
        
        [PropertyOrderAttribute(8)]
        [ExpandableObjectAttribute()]
        [DisplayName("Default DB")]
        [Description("Database connection. If empty, all solutions settings are used")]
        public AppDbSettings DefaultDb // Property.tt Line: 109
        { 
            get 
            { 
                return this._DefaultDb; 
            }
            set
            {
                if (this._DefaultDb != value)
                {
                    this.OnDefaultDbChanging(this._DefaultDb, value);
                    this._DefaultDb = value;
                    this.OnDefaultDbChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private AppDbSettings _DefaultDb;
        partial void OnDefaultDbChanging(AppDbSettings from, AppDbSettings to); // Property.tt Line: 130
        partial void OnDefaultDbChanged();
        [BrowsableAttribute(false)]
        public IAppDbSettings IDefaultDb { get { return this._DefaultDb; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProject> ListAppProjects // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListAppProjects; 
            }
            set
            {
                if (this._ListAppProjects != value)
                {
                    this.OnListAppProjectsChanging(this._ListAppProjects, value);
                    this._ListAppProjects = value;
                    this.OnListAppProjectsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppProject> _ListAppProjects;
        partial void OnListAppProjectsChanging(SortedObservableCollection<AppProject> from, SortedObservableCollection<AppProject> to); // Property.tt Line: 80
        partial void OnListAppProjectsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IAppProject> IListAppProjects { get { foreach (var t in this._ListAppProjects) yield return t; } }
    
        #endregion Properties
    }
    public partial class AppProject : ConfigObjectSubBase<AppProject, AppProject.AppProjectValidator>, IComparable<AppProject>, IConfigAcceptVisitor, IAppProject // Class.tt Line: 6
    {
        public partial class AppProjectValidator : ValidatorBase<AppProject, AppProjectValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppProject(ITreeConfigNode parent) 
            : base(parent, AppProjectValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DefaultDb = new AppDbSettings(); // Class.tt Line: 26
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.DefaultDb = AppDbSettings.Clone(from.DefaultDb, isDeep);
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListAppProjectGenerators) // Clone.tt Line: 49
                vm.ListAppProjectGenerators.Add(AppProjectGenerator.Clone(vm, (AppProjectGenerator)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(AppProject to, AppProject from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new AppProjectGenerator(to); // Clone.tt Line: 110
                        AppProjectGenerator.Update(p, (AppProjectGenerator)tt, isDeep);
                        to.ListAppProjectGenerators.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static AppProject ConvertToVM(Proto.Config.proto_app_project m, AppProject vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.RelativeAppProjectPath = m.RelativeAppProjectPath; // Clone.tt Line: 211
            if (vm.DefaultDb == null) // Clone.tt Line: 203
                vm.DefaultDb = new AppDbSettings(); // Clone.tt Line: 207
            AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 209
            vm.ListAppProjectGenerators = new ConfigNodesCollection<AppProjectGenerator>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListAppProjectGenerators) // Clone.tt Line: 191
            {
                var tvm = AppProjectGenerator.ConvertToVM(t, new AppProjectGenerator(vm)); // Clone.tt Line: 194
                vm.ListAppProjectGenerators.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'AppProject' to 'proto_app_project'
        public static Proto.Config.proto_app_project ConvertToProto(AppProject vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_app_project m = new Proto.Config.proto_app_project(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.RelativeAppProjectPath = vm.RelativeAppProjectPath; // Clone.tt Line: 261
            m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 255
            foreach (var t in vm.ListAppProjectGenerators) // Clone.tt Line: 227
                m.ListAppProjectGenerators.Add(AppProjectGenerator.ConvertToProto((AppProjectGenerator)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(6)]
        [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
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
                    this.OnRelativeAppProjectPathChanging(this._RelativeAppProjectPath, value);
                    this._RelativeAppProjectPath = value;
                    this.OnRelativeAppProjectPathChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _RelativeAppProjectPath = string.Empty;
        partial void OnRelativeAppProjectPathChanging(string from, string to); // Property.tt Line: 156
        partial void OnRelativeAppProjectPathChanged();
        
        [PropertyOrderAttribute(8)]
        [ExpandableObjectAttribute()]
        [DisplayName("Default DB")]
        [Description("Database connection. If empty, solution settings are used")]
        public AppDbSettings DefaultDb // Property.tt Line: 109
        { 
            get 
            { 
                return this._DefaultDb; 
            }
            set
            {
                if (this._DefaultDb != value)
                {
                    this.OnDefaultDbChanging(this._DefaultDb, value);
                    this._DefaultDb = value;
                    this.OnDefaultDbChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private AppDbSettings _DefaultDb;
        partial void OnDefaultDbChanging(AppDbSettings from, AppDbSettings to); // Property.tt Line: 130
        partial void OnDefaultDbChanged();
        [BrowsableAttribute(false)]
        public IAppDbSettings IDefaultDb { get { return this._DefaultDb; } }
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<AppProjectGenerator> ListAppProjectGenerators // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListAppProjectGenerators; 
            }
            set
            {
                if (this._ListAppProjectGenerators != value)
                {
                    this.OnListAppProjectGeneratorsChanging(this._ListAppProjectGenerators, value);
                    this._ListAppProjectGenerators = value;
                    this.OnListAppProjectGeneratorsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<AppProjectGenerator> _ListAppProjectGenerators;
        partial void OnListAppProjectGeneratorsChanging(SortedObservableCollection<AppProjectGenerator> from, SortedObservableCollection<AppProjectGenerator> to); // Property.tt Line: 80
        partial void OnListAppProjectGeneratorsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IAppProjectGenerator> IListAppProjectGenerators { get { foreach (var t in this._ListAppProjectGenerators) yield return t; } }
    
        #endregion Properties
    }
    public partial class AppProjectGenerator : ConfigObjectSubBase<AppProjectGenerator, AppProjectGenerator.AppProjectGeneratorValidator>, IComparable<AppProjectGenerator>, IConfigAcceptVisitor, IAppProjectGenerator // Class.tt Line: 6
    {
        public partial class AppProjectGeneratorValidator : ValidatorBase<AppProjectGenerator, AppProjectGeneratorValidator> { } // Class.tt Line: 8
        #region CTOR
        public AppProjectGenerator(ITreeConfigNode parent) 
            : base(parent, AppProjectGeneratorValidator.Validator) // Class.tt Line: 12
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
        public static AppProjectGenerator Clone(ITreeConfigNode parent, AppProjectGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            AppProjectGenerator vm = new AppProjectGenerator(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 62
            vm.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 62
            vm.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 62
            vm.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 62
            vm.RelativePathToGeneratedFile = from.RelativePathToGeneratedFile; // Clone.tt Line: 62
            vm.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 62
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(AppProjectGenerator to, AppProjectGenerator from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.PluginGuid = from.PluginGuid; // Clone.tt Line: 134
            to.DescriptionPlugin = from.DescriptionPlugin; // Clone.tt Line: 134
            to.PluginGeneratorGuid = from.PluginGeneratorGuid; // Clone.tt Line: 134
            to.DescriptionGenerator = from.DescriptionGenerator; // Clone.tt Line: 134
            to.RelativePathToGeneratedFile = from.RelativePathToGeneratedFile; // Clone.tt Line: 134
            to.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static AppProjectGenerator ConvertToVM(Proto.Config.proto_app_project_generator m, AppProjectGenerator vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 211
            vm.DescriptionPlugin = m.DescriptionPlugin; // Clone.tt Line: 211
            vm.PluginGeneratorGuid = m.PluginGeneratorGuid; // Clone.tt Line: 211
            vm.DescriptionGenerator = m.DescriptionGenerator; // Clone.tt Line: 211
            vm.RelativePathToGeneratedFile = m.RelativePathToGeneratedFile; // Clone.tt Line: 211
            vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 211
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'AppProjectGenerator' to 'proto_app_project_generator'
        public static Proto.Config.proto_app_project_generator ConvertToProto(AppProjectGenerator vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_app_project_generator m = new Proto.Config.proto_app_project_generator(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 261
            m.DescriptionPlugin = vm.DescriptionPlugin; // Clone.tt Line: 261
            m.PluginGeneratorGuid = vm.PluginGeneratorGuid; // Clone.tt Line: 261
            m.DescriptionGenerator = vm.DescriptionGenerator; // Clone.tt Line: 261
            m.RelativePathToGeneratedFile = vm.RelativePathToGeneratedFile; // Clone.tt Line: 261
            m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 261
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [DisplayName("Plugin")]
        [Description("Plugins with generators")]
        [Editor(typeof(EditorPluginSelection), typeof(EditorPluginSelection))]
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
                    this.OnPluginGuidChanging(this._PluginGuid, value);
                    this._PluginGuid = value;
                    this.OnPluginGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _PluginGuid = string.Empty;
        partial void OnPluginGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnPluginGuidChanged();
        
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
                    this.OnDescriptionPluginChanging(this._DescriptionPlugin, value);
                    this._DescriptionPlugin = value;
                    this.OnDescriptionPluginChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _DescriptionPlugin = string.Empty;
        partial void OnDescriptionPluginChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionPluginChanged();
        
        [PropertyOrderAttribute(6)]
        [DisplayName("Generator")]
        [Description("Plugin generator")]
        [Editor(typeof(EditorPluginGeneratorSelection), typeof(EditorPluginGeneratorSelection))]
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
                    this.OnPluginGeneratorGuidChanging(this._PluginGeneratorGuid, value);
                    this._PluginGeneratorGuid = value;
                    this.OnPluginGeneratorGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _PluginGeneratorGuid = string.Empty;
        partial void OnPluginGeneratorGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnPluginGeneratorGuidChanged();
        
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
                    this.OnDescriptionGeneratorChanging(this._DescriptionGenerator, value);
                    this._DescriptionGenerator = value;
                    this.OnDescriptionGeneratorChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _DescriptionGenerator = string.Empty;
        partial void OnDescriptionGeneratorChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionGeneratorChanged();
        
        [PropertyOrderAttribute(8)]
        [DisplayName("Output")]
        [Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
        [Description("File path to store connection string settings in private place.")]
        public string RelativePathToGeneratedFile // Property.tt Line: 135
        { 
            get 
            { 
                return this._RelativePathToGeneratedFile; 
            }
            set
            {
                if (this._RelativePathToGeneratedFile != value)
                {
                    this.OnRelativePathToGeneratedFileChanging(this._RelativePathToGeneratedFile, value);
                    this._RelativePathToGeneratedFile = value;
                    this.OnRelativePathToGeneratedFileChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _RelativePathToGeneratedFile = string.Empty;
        partial void OnRelativePathToGeneratedFileChanging(string from, string to); // Property.tt Line: 156
        partial void OnRelativePathToGeneratedFileChanged();
        
        
        ///////////////////////////////////////////////////
        /// 
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        [ReadOnly(true)]
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
                    this.OnGeneratorSettingsChanging(this._GeneratorSettings, value);
                    this._GeneratorSettings = value;
                    this.OnGeneratorSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _GeneratorSettings = string.Empty;
        partial void OnGeneratorSettingsChanging(string from, string to); // Property.tt Line: 156
        partial void OnGeneratorSettingsChanged();
    
        #endregion Properties
    }
    public partial class GeneratorSettings : ConfigObjectSubBase<GeneratorSettings, GeneratorSettings.GeneratorSettingsValidator>, IComparable<GeneratorSettings>, IConfigAcceptVisitor, IGeneratorSettings // Class.tt Line: 6
    {
        public partial class GeneratorSettingsValidator : ValidatorBase<GeneratorSettings, GeneratorSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GeneratorSettings(ITreeConfigNode parent) 
            : base(parent, GeneratorSettingsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListTypeSettings = new ConfigNodesCollection<TypeSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(TypeSettings)) // Clone.tt Line: 15
            {
                this.ListTypeSettings.Sort();
            }
        }
        public static GeneratorSettings Clone(ITreeConfigNode parent, GeneratorSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GeneratorSettings vm = new GeneratorSettings(parent);
            vm.AppGeneratorGuid = from.AppGeneratorGuid; // Clone.tt Line: 62
            vm.ListTypeSettings = new ConfigNodesCollection<TypeSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListTypeSettings) // Clone.tt Line: 49
                vm.ListTypeSettings.Add(TypeSettings.Clone(vm, (TypeSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GeneratorSettings to, GeneratorSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.AppGeneratorGuid = from.AppGeneratorGuid; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListTypeSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListTypeSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            TypeSettings.Update((TypeSettings)t, (TypeSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListTypeSettings.Remove(t);
                }
                foreach (var tt in from.ListTypeSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListTypeSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new TypeSettings(to); // Clone.tt Line: 110
                        TypeSettings.Update(p, (TypeSettings)tt, isDeep);
                        to.ListTypeSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
        #region IEditable
        public override GeneratorSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return GeneratorSettings.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(GeneratorSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            GeneratorSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_generator_settings' to 'GeneratorSettings'
        public static GeneratorSettings ConvertToVM(Proto.Config.proto_generator_settings m, GeneratorSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.AppGeneratorGuid = m.AppGeneratorGuid; // Clone.tt Line: 211
            vm.ListTypeSettings = new ConfigNodesCollection<TypeSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListTypeSettings) // Clone.tt Line: 191
            {
                var tvm = TypeSettings.ConvertToVM(t, new TypeSettings(vm)); // Clone.tt Line: 194
                vm.ListTypeSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GeneratorSettings' to 'proto_generator_settings'
        public static Proto.Config.proto_generator_settings ConvertToProto(GeneratorSettings vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_generator_settings m = new Proto.Config.proto_generator_settings(); // Clone.tt Line: 224
            m.AppGeneratorGuid = vm.AppGeneratorGuid; // Clone.tt Line: 261
            foreach (var t in vm.ListTypeSettings) // Clone.tt Line: 227
                m.ListTypeSettings.Add(TypeSettings.ConvertToProto((TypeSettings)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListTypeSettings)
            {
                t.AcceptConfigNodeVisitor(visitor);
            }
            visitor.VisitEnd(this);
        }
        #endregion Procedures
        #region Properties
        
        public string AppGeneratorGuid // Property.tt Line: 135
        { 
            get 
            { 
                return this._AppGeneratorGuid; 
            }
            set
            {
                if (this._AppGeneratorGuid != value)
                {
                    this.OnAppGeneratorGuidChanging(this._AppGeneratorGuid, value);
                    this._AppGeneratorGuid = value;
                    this.OnAppGeneratorGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _AppGeneratorGuid = string.Empty;
        partial void OnAppGeneratorGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnAppGeneratorGuidChanged();
        
        public ConfigNodesCollection<TypeSettings> ListTypeSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListTypeSettings; 
            }
            set
            {
                if (this._ListTypeSettings != value)
                {
                    this.OnListTypeSettingsChanging(this._ListTypeSettings, value);
                    this._ListTypeSettings = value;
                    this.OnListTypeSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<TypeSettings> _ListTypeSettings;
        partial void OnListTypeSettingsChanging(SortedObservableCollection<TypeSettings> from, SortedObservableCollection<TypeSettings> to); // Property.tt Line: 80
        partial void OnListTypeSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<ITypeSettings> IListTypeSettings { get { foreach (var t in this._ListTypeSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class TypeSettings : ConfigObjectSubBase<TypeSettings, TypeSettings.TypeSettingsValidator>, IComparable<TypeSettings>, IConfigAcceptVisitor, ITypeSettings // Class.tt Line: 6
    {
        public partial class TypeSettingsValidator : ValidatorBase<TypeSettings, TypeSettingsValidator> { } // Class.tt Line: 8
        #region CTOR
        public TypeSettings(ITreeConfigNode parent) 
            : base(parent, TypeSettingsValidator.Validator) // Class.tt Line: 12
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
        public static TypeSettings Clone(ITreeConfigNode parent, TypeSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            TypeSettings vm = new TypeSettings(parent);
            vm.FullTypeName = from.FullTypeName; // Clone.tt Line: 62
            vm.Settings = from.Settings; // Clone.tt Line: 62
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(TypeSettings to, TypeSettings from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.FullTypeName = from.FullTypeName; // Clone.tt Line: 134
            to.Settings = from.Settings; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
        #region IEditable
        public override TypeSettings Backup()
        {
            bool isDeep = true;
            this.OnBackupObjectStarting(ref isDeep);
            return TypeSettings.Clone(this.Parent, this);
        }
        partial void OnBackupObjectStarting(ref bool isDeep);
        public override void Restore(TypeSettings from)
        {
            bool isDeep = true;
            this.OnRestoreObjectStarting(ref isDeep);
            TypeSettings.Update(this, from, isDeep);
        }
        partial void OnRestoreObjectStarting(ref bool isDeep);
        #endregion IEditable
        // Conversion from 'proto_type_settings' to 'TypeSettings'
        public static TypeSettings ConvertToVM(Proto.Config.proto_type_settings m, TypeSettings vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.FullTypeName = m.FullTypeName; // Clone.tt Line: 211
            vm.Settings = m.Settings; // Clone.tt Line: 211
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'TypeSettings' to 'proto_type_settings'
        public static Proto.Config.proto_type_settings ConvertToProto(TypeSettings vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_type_settings m = new Proto.Config.proto_type_settings(); // Clone.tt Line: 224
            m.FullTypeName = vm.FullTypeName; // Clone.tt Line: 261
            m.Settings = vm.Settings; // Clone.tt Line: 261
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
        
        public string FullTypeName // Property.tt Line: 135
        { 
            get 
            { 
                return this._FullTypeName; 
            }
            set
            {
                if (this._FullTypeName != value)
                {
                    this.OnFullTypeNameChanging(this._FullTypeName, value);
                    this._FullTypeName = value;
                    this.OnFullTypeNameChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _FullTypeName = string.Empty;
        partial void OnFullTypeNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnFullTypeNameChanged();
        
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
                    this.OnSettingsChanging(this._Settings, value);
                    this._Settings = value;
                    this.OnSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Settings = string.Empty;
        partial void OnSettingsChanging(string from, string to); // Property.tt Line: 156
        partial void OnSettingsChanged();
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Configuration model
    ///////////////////////////////////////////////////
    public partial class ConfigModel : ConfigObjectSubBase<ConfigModel, ConfigModel.ConfigModelValidator>, IComparable<ConfigModel>, IConfigAcceptVisitor, IConfigModel // Class.tt Line: 6
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
        public static ConfigModel Clone(ITreeConfigNode parent, ConfigModel from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            ConfigModel vm = new ConfigModel(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Version = from.Version; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.GroupCommon = GroupListCommon.Clone(vm, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupConstants = GroupListConstants.Clone(vm, from.GroupConstants, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupEnumerations = GroupListEnumerations.Clone(vm, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupCatalogs = GroupListCatalogs.Clone(vm, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupDocuments = GroupDocuments.Clone(vm, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupJournals = GroupListJournals.Clone(vm, from.GroupJournals, isDeep);
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(ConfigModel to, ConfigModel from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Version = from.Version; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                GroupListCommon.Update(to.GroupCommon, from.GroupCommon, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListConstants.Update(to.GroupConstants, from.GroupConstants, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListEnumerations.Update(to.GroupEnumerations, from.GroupEnumerations, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListCatalogs.Update(to.GroupCatalogs, from.GroupCatalogs, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupDocuments.Update(to.GroupDocuments, from.GroupDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListJournals.Update(to.GroupJournals, from.GroupJournals, isDeep);
        }
        // Clone.tt Line: 140
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
        public static ConfigModel ConvertToVM(Proto.Config.proto_config_model m, ConfigModel vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Version = m.Version; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.GroupCommon == null) // Clone.tt Line: 203
                vm.GroupCommon = new GroupListCommon(vm); // Clone.tt Line: 205
            GroupListCommon.ConvertToVM(m.GroupCommon, vm.GroupCommon); // Clone.tt Line: 209
            if (vm.GroupConstants == null) // Clone.tt Line: 203
                vm.GroupConstants = new GroupListConstants(vm); // Clone.tt Line: 205
            GroupListConstants.ConvertToVM(m.GroupConstants, vm.GroupConstants); // Clone.tt Line: 209
            if (vm.GroupEnumerations == null) // Clone.tt Line: 203
                vm.GroupEnumerations = new GroupListEnumerations(vm); // Clone.tt Line: 205
            GroupListEnumerations.ConvertToVM(m.GroupEnumerations, vm.GroupEnumerations); // Clone.tt Line: 209
            if (vm.GroupCatalogs == null) // Clone.tt Line: 203
                vm.GroupCatalogs = new GroupListCatalogs(vm); // Clone.tt Line: 205
            GroupListCatalogs.ConvertToVM(m.GroupCatalogs, vm.GroupCatalogs); // Clone.tt Line: 209
            if (vm.GroupDocuments == null) // Clone.tt Line: 203
                vm.GroupDocuments = new GroupDocuments(vm); // Clone.tt Line: 205
            GroupDocuments.ConvertToVM(m.GroupDocuments, vm.GroupDocuments); // Clone.tt Line: 209
            if (vm.GroupJournals == null) // Clone.tt Line: 203
                vm.GroupJournals = new GroupListJournals(vm); // Clone.tt Line: 205
            GroupListJournals.ConvertToVM(m.GroupJournals, vm.GroupJournals); // Clone.tt Line: 209
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'ConfigModel' to 'proto_config_model'
        public static Proto.Config.proto_config_model ConvertToProto(ConfigModel vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_config_model m = new Proto.Config.proto_config_model(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Version = vm.Version; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.GroupCommon = GroupListCommon.ConvertToProto(vm.GroupCommon); // Clone.tt Line: 255
            m.GroupConstants = GroupListConstants.ConvertToProto(vm.GroupConstants); // Clone.tt Line: 255
            m.GroupEnumerations = GroupListEnumerations.ConvertToProto(vm.GroupEnumerations); // Clone.tt Line: 255
            m.GroupCatalogs = GroupListCatalogs.ConvertToProto(vm.GroupCatalogs); // Clone.tt Line: 255
            m.GroupDocuments = GroupDocuments.ConvertToProto(vm.GroupDocuments); // Clone.tt Line: 255
            m.GroupJournals = GroupListJournals.ConvertToProto(vm.GroupJournals); // Clone.tt Line: 255
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
                    this.OnVersionChanging(this._Version, value);
                    this._Version = value;
                    this.OnVersionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private int _Version;
        partial void OnVersionChanging(int from, int to); // Property.tt Line: 156
        partial void OnVersionChanged();
        
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListCommon GroupCommon // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupCommon; 
            }
            set
            {
                if (this._GroupCommon != value)
                {
                    this.OnGroupCommonChanging(this._GroupCommon, value);
                    this._GroupCommon = value;
                    this.OnGroupCommonChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListCommon _GroupCommon;
        partial void OnGroupCommonChanging(GroupListCommon from, GroupListCommon to); // Property.tt Line: 130
        partial void OnGroupCommonChanged();
        [BrowsableAttribute(false)]
        public IGroupListCommon IGroupCommon { get { return this._GroupCommon; } }
        
        [BrowsableAttribute(false)]
        public GroupListConstants GroupConstants // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupConstants; 
            }
            set
            {
                if (this._GroupConstants != value)
                {
                    this.OnGroupConstantsChanging(this._GroupConstants, value);
                    this._GroupConstants = value;
                    this.OnGroupConstantsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListConstants _GroupConstants;
        partial void OnGroupConstantsChanging(GroupListConstants from, GroupListConstants to); // Property.tt Line: 130
        partial void OnGroupConstantsChanged();
        [BrowsableAttribute(false)]
        public IGroupListConstants IGroupConstants { get { return this._GroupConstants; } }
        
        [BrowsableAttribute(false)]
        public GroupListEnumerations GroupEnumerations // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupEnumerations; 
            }
            set
            {
                if (this._GroupEnumerations != value)
                {
                    this.OnGroupEnumerationsChanging(this._GroupEnumerations, value);
                    this._GroupEnumerations = value;
                    this.OnGroupEnumerationsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListEnumerations _GroupEnumerations;
        partial void OnGroupEnumerationsChanging(GroupListEnumerations from, GroupListEnumerations to); // Property.tt Line: 130
        partial void OnGroupEnumerationsChanged();
        [BrowsableAttribute(false)]
        public IGroupListEnumerations IGroupEnumerations { get { return this._GroupEnumerations; } }
        
        [BrowsableAttribute(false)]
        public GroupListCatalogs GroupCatalogs // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupCatalogs; 
            }
            set
            {
                if (this._GroupCatalogs != value)
                {
                    this.OnGroupCatalogsChanging(this._GroupCatalogs, value);
                    this._GroupCatalogs = value;
                    this.OnGroupCatalogsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListCatalogs _GroupCatalogs;
        partial void OnGroupCatalogsChanging(GroupListCatalogs from, GroupListCatalogs to); // Property.tt Line: 130
        partial void OnGroupCatalogsChanged();
        [BrowsableAttribute(false)]
        public IGroupListCatalogs IGroupCatalogs { get { return this._GroupCatalogs; } }
        
        [BrowsableAttribute(false)]
        public GroupDocuments GroupDocuments // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupDocuments; 
            }
            set
            {
                if (this._GroupDocuments != value)
                {
                    this.OnGroupDocumentsChanging(this._GroupDocuments, value);
                    this._GroupDocuments = value;
                    this.OnGroupDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupDocuments _GroupDocuments;
        partial void OnGroupDocumentsChanging(GroupDocuments from, GroupDocuments to); // Property.tt Line: 130
        partial void OnGroupDocumentsChanged();
        [BrowsableAttribute(false)]
        public IGroupDocuments IGroupDocuments { get { return this._GroupDocuments; } }
        
        [BrowsableAttribute(false)]
        public GroupListJournals GroupJournals // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupJournals; 
            }
            set
            {
                if (this._GroupJournals != value)
                {
                    this.OnGroupJournalsChanging(this._GroupJournals, value);
                    this._GroupJournals = value;
                    this.OnGroupJournalsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListJournals _GroupJournals;
        partial void OnGroupJournalsChanging(GroupListJournals from, GroupListJournals to); // Property.tt Line: 130
        partial void OnGroupJournalsChanged();
        [BrowsableAttribute(false)]
        public IGroupListJournals IGroupJournals { get { return this._GroupJournals; } }
    
        #endregion Properties
    }
    public partial class DataType : ViewModelValidatableWithSeverity<DataType, DataType.DataTypeValidator>, IDataType // Class.tt Line: 6
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
            vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 62
            vm.Length = from.Length; // Clone.tt Line: 62
            vm.Accuracy = from.Accuracy; // Clone.tt Line: 62
            vm.IsPositive = from.IsPositive; // Clone.tt Line: 62
            vm.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 62
            vm.IsNullable = from.IsNullable; // Clone.tt Line: 62
            foreach (var t in from.ListObjectGuids) // Clone.tt Line: 41
                vm.ListObjectGuids.Add(t);
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(DataType to, DataType from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 134
            to.Length = from.Length; // Clone.tt Line: 134
            to.Accuracy = from.Accuracy; // Clone.tt Line: 134
            to.IsPositive = from.IsPositive; // Clone.tt Line: 134
            to.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 134
            to.IsNullable = from.IsNullable; // Clone.tt Line: 134
                to.ListObjectGuids.Clear(); // Clone.tt Line: 120
                foreach (var tt in from.ListObjectGuids)
                {
                    to.ListObjectGuids.Add(tt);
                }
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static DataType ConvertToVM(Proto.Config.proto_data_type m, DataType vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.DataTypeEnum = (EnumDataType)m.DataTypeEnum; // Clone.tt Line: 211
            vm.Length = m.Length; // Clone.tt Line: 211
            vm.Accuracy = m.Accuracy; // Clone.tt Line: 211
            vm.IsPositive = m.IsPositive; // Clone.tt Line: 211
            vm.ObjectGuid = m.ObjectGuid; // Clone.tt Line: 211
            vm.IsNullable = m.IsNullable; // Clone.tt Line: 211
            vm.ListObjectGuids = new ObservableCollection<string>(); // Clone.tt Line: 174
            foreach (var t in m.ListObjectGuids) // Clone.tt Line: 175
            {
                vm.ListObjectGuids.Add(t);
            }
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'DataType' to 'proto_data_type'
        public static Proto.Config.proto_data_type ConvertToProto(DataType vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_data_type m = new Proto.Config.proto_data_type(); // Clone.tt Line: 224
            m.DataTypeEnum = (Proto.Config.proto_enum_data_type)vm.DataTypeEnum; // Clone.tt Line: 259
            m.Length = vm.Length; // Clone.tt Line: 261
            m.Accuracy = vm.Accuracy; // Clone.tt Line: 261
            m.IsPositive = vm.IsPositive; // Clone.tt Line: 261
            m.ObjectGuid = vm.ObjectGuid; // Clone.tt Line: 261
            m.IsNullable = vm.IsNullable; // Clone.tt Line: 261
            foreach (var t in vm.ListObjectGuids) // Clone.tt Line: 227
                m.ListObjectGuids.Add(t); // Clone.tt Line: 229
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 261
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
                    this.OnDataTypeEnumChanging(this._DataTypeEnum, value);
                    this._DataTypeEnum = value;
                    this.OnDataTypeEnumChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private EnumDataType _DataTypeEnum;
        partial void OnDataTypeEnumChanging(EnumDataType from, EnumDataType to); // Property.tt Line: 156
        partial void OnDataTypeEnumChanged();
        
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
                    this.OnLengthChanging(this._Length, value);
                    this._Length = value;
                    this.OnLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private uint _Length;
        partial void OnLengthChanging(uint from, uint to); // Property.tt Line: 156
        partial void OnLengthChanged();
        
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
                    this.OnAccuracyChanging(this._Accuracy, value);
                    this._Accuracy = value;
                    this.OnAccuracyChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private uint _Accuracy;
        partial void OnAccuracyChanging(uint from, uint to); // Property.tt Line: 156
        partial void OnAccuracyChanged();
        
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
                    this.OnIsPositiveChanging(this._IsPositive, value);
                    this._IsPositive = value;
                    this.OnIsPositiveChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsPositive;
        partial void OnIsPositiveChanging(bool from, bool to); // Property.tt Line: 156
        partial void OnIsPositiveChanged();
        
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
                    this.OnObjectGuidChanging(this._ObjectGuid, value);
                    this._ObjectGuid = value;
                    this.OnObjectGuidChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _ObjectGuid = string.Empty;
        partial void OnObjectGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnObjectGuidChanged();
        
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
                    this.OnIsNullableChanging(this._IsNullable, value);
                    this._IsNullable = value;
                    this.OnIsNullableChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsNullable;
        partial void OnIsNullableChanging(bool from, bool to); // Property.tt Line: 156
        partial void OnIsNullableChanged();
        
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
                    this.OnListObjectGuidsChanging(this._ListObjectGuids, value);
                    _ListObjectGuids = value;
                    this.OnListObjectGuidsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ObservableCollection<string> _ListObjectGuids;
        partial void OnListObjectGuidsChanging(ObservableCollection<string> from, ObservableCollection<string> to); // Property.tt Line: 30
        partial void OnListObjectGuidsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<string> IListObjectGuids { get { foreach (var t in _ListObjectGuids) yield return t; } }
        
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
                    this.OnIsIndexFkChanging(this._IsIndexFk, value);
                    this._IsIndexFk = value;
                    this.OnIsIndexFkChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsIndexFk;
        partial void OnIsIndexFkChanging(bool from, bool to); // Property.tt Line: 156
        partial void OnIsIndexFkChanged();
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Common parameters section
    ///////////////////////////////////////////////////
    public partial class GroupListCommon : ConfigObjectSubBase<GroupListCommon, GroupListCommon.GroupListCommonValidator>, IComparable<GroupListCommon>, IConfigAcceptVisitor, IGroupListCommon // Class.tt Line: 6
    {
        public partial class GroupListCommonValidator : ValidatorBase<GroupListCommon, GroupListCommonValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListCommon(ITreeConfigNode parent) 
            : base(parent, GroupListCommonValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupRoles = new GroupListRoles(this); // Class.tt Line: 28
            this.GroupViewForms = new GroupListMainViewForms(this); // Class.tt Line: 28
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
        public static GroupListCommon Clone(ITreeConfigNode parent, GroupListCommon from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListCommon vm = new GroupListCommon(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.GroupRoles = GroupListRoles.Clone(vm, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupViewForms = GroupListMainViewForms.Clone(vm, from.GroupViewForms, isDeep);
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListCommon to, GroupListCommon from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                GroupListRoles.Update(to.GroupRoles, from.GroupRoles, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListMainViewForms.Update(to.GroupViewForms, from.GroupViewForms, isDeep);
        }
        // Clone.tt Line: 140
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
        public static GroupListCommon ConvertToVM(Proto.Config.proto_group_list_common m, GroupListCommon vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.GroupRoles == null) // Clone.tt Line: 203
                vm.GroupRoles = new GroupListRoles(vm); // Clone.tt Line: 205
            GroupListRoles.ConvertToVM(m.GroupRoles, vm.GroupRoles); // Clone.tt Line: 209
            if (vm.GroupViewForms == null) // Clone.tt Line: 203
                vm.GroupViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 205
            GroupListMainViewForms.ConvertToVM(m.GroupViewForms, vm.GroupViewForms); // Clone.tt Line: 209
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListCommon' to 'proto_group_list_common'
        public static Proto.Config.proto_group_list_common ConvertToProto(GroupListCommon vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_common m = new Proto.Config.proto_group_list_common(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.GroupRoles = GroupListRoles.ConvertToProto(vm.GroupRoles); // Clone.tt Line: 255
            m.GroupViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupViewForms); // Clone.tt Line: 255
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListRoles GroupRoles // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupRoles; 
            }
            set
            {
                if (this._GroupRoles != value)
                {
                    this.OnGroupRolesChanging(this._GroupRoles, value);
                    this._GroupRoles = value;
                    this.OnGroupRolesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListRoles _GroupRoles;
        partial void OnGroupRolesChanging(GroupListRoles from, GroupListRoles to); // Property.tt Line: 130
        partial void OnGroupRolesChanged();
        [BrowsableAttribute(false)]
        public IGroupListRoles IGroupRoles { get { return this._GroupRoles; } }
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupViewForms // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupViewForms; 
            }
            set
            {
                if (this._GroupViewForms != value)
                {
                    this.OnGroupViewFormsChanging(this._GroupViewForms, value);
                    this._GroupViewForms = value;
                    this.OnGroupViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListMainViewForms _GroupViewForms;
        partial void OnGroupViewFormsChanging(GroupListMainViewForms from, GroupListMainViewForms to); // Property.tt Line: 130
        partial void OnGroupViewFormsChanged();
        [BrowsableAttribute(false)]
        public IGroupListMainViewForms IGroupViewForms { get { return this._GroupViewForms; } }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// User's role
    ///////////////////////////////////////////////////
    public partial class Role : ConfigObjectSubBase<Role, Role.RoleValidator>, IComparable<Role>, IConfigAcceptVisitor, IRole // Class.tt Line: 6
    {
        public partial class RoleValidator : ValidatorBase<Role, RoleValidator> { } // Class.tt Line: 8
        #region CTOR
        public Role(ITreeConfigNode parent) 
            : base(parent, RoleValidator.Validator) // Class.tt Line: 12
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
        public static Role Clone(ITreeConfigNode parent, Role from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Role vm = new Role(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Role to, Role from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static Role ConvertToVM(Proto.Config.proto_role m, Role vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Role' to 'proto_role'
        public static Proto.Config.proto_role ConvertToProto(Role vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_role m = new Proto.Config.proto_role(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
    
        #endregion Properties
    }
    public partial class GroupListRoles : ConfigObjectSubBase<GroupListRoles, GroupListRoles.GroupListRolesValidator>, IComparable<GroupListRoles>, IConfigAcceptVisitor, IGroupListRoles // Class.tt Line: 6
    {
        public partial class GroupListRolesValidator : ValidatorBase<GroupListRoles, GroupListRolesValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListRoles(ITreeConfigNode parent) 
            : base(parent, GroupListRolesValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListRoles = new ConfigNodesCollection<Role>(this); // Class.tt Line: 22
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
        }
        public static GroupListRoles Clone(ITreeConfigNode parent, GroupListRoles from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListRoles vm = new GroupListRoles(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListRoles) // Clone.tt Line: 49
                vm.ListRoles.Add(Role.Clone(vm, (Role)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListRoles to, GroupListRoles from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Role(to); // Clone.tt Line: 110
                        Role.Update(p, (Role)tt, isDeep);
                        to.ListRoles.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListRoles ConvertToVM(Proto.Config.proto_group_list_roles m, GroupListRoles vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListRoles) // Clone.tt Line: 191
            {
                var tvm = Role.ConvertToVM(t, new Role(vm)); // Clone.tt Line: 194
                vm.ListRoles.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListRoles' to 'proto_group_list_roles'
        public static Proto.Config.proto_group_list_roles ConvertToProto(GroupListRoles vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_roles m = new Proto.Config.proto_group_list_roles(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListRoles) // Clone.tt Line: 227
                m.ListRoles.Add(Role.ConvertToProto((Role)t)); // Clone.tt Line: 231
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Role> ListRoles // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListRoles; 
            }
            set
            {
                if (this._ListRoles != value)
                {
                    this.OnListRolesChanging(this._ListRoles, value);
                    this._ListRoles = value;
                    this.OnListRolesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Role> _ListRoles;
        partial void OnListRolesChanging(SortedObservableCollection<Role> from, SortedObservableCollection<Role> to); // Property.tt Line: 80
        partial void OnListRolesChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IRole> IListRoles { get { foreach (var t in this._ListRoles) yield return t; } }
        public Role this[int index] { get { return (Role)this.ListRoles[index]; } }
        public void Add(Role item) // Property.tt Line: 87
        { 
            this.ListRoles.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Role> items) 
        { 
            this.ListRoles.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListRoles.Count; 
        }
        public void Remove(Role item) 
        {
            this.ListRoles.Remove(item); 
            item.Parent = null;
        }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy parent
    ///////////////////////////////////////////////////
    public partial class MainViewForm : ConfigObjectSubBase<MainViewForm, MainViewForm.MainViewFormValidator>, IComparable<MainViewForm>, IConfigAcceptVisitor, IMainViewForm // Class.tt Line: 6
    {
        public partial class MainViewFormValidator : ValidatorBase<MainViewForm, MainViewFormValidator> { } // Class.tt Line: 8
        #region CTOR
        public MainViewForm(ITreeConfigNode parent) 
            : base(parent, MainViewFormValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupListViewForms = new GroupListMainViewForms(this); // Class.tt Line: 28
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
        public static MainViewForm Clone(ITreeConfigNode parent, MainViewForm from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            MainViewForm vm = new MainViewForm(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.GroupListViewForms = GroupListMainViewForms.Clone(vm, from.GroupListViewForms, isDeep);
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(MainViewForm to, MainViewForm from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                GroupListMainViewForms.Update(to.GroupListViewForms, from.GroupListViewForms, isDeep);
        }
        // Clone.tt Line: 140
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
        public static MainViewForm ConvertToVM(Proto.Config.proto_main_view_form m, MainViewForm vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.GroupListViewForms == null) // Clone.tt Line: 203
                vm.GroupListViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 205
            GroupListMainViewForms.ConvertToVM(m.GroupListViewForms, vm.GroupListViewForms); // Clone.tt Line: 209
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'MainViewForm' to 'proto_main_view_form'
        public static Proto.Config.proto_main_view_form ConvertToProto(MainViewForm vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_main_view_form m = new Proto.Config.proto_main_view_form(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.GroupListViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupListViewForms); // Clone.tt Line: 255
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListMainViewForms GroupListViewForms // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupListViewForms; 
            }
            set
            {
                if (this._GroupListViewForms != value)
                {
                    this.OnGroupListViewFormsChanging(this._GroupListViewForms, value);
                    this._GroupListViewForms = value;
                    this.OnGroupListViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListMainViewForms _GroupListViewForms;
        partial void OnGroupListViewFormsChanging(GroupListMainViewForms from, GroupListMainViewForms to); // Property.tt Line: 130
        partial void OnGroupListViewFormsChanged();
        [BrowsableAttribute(false)]
        public IGroupListMainViewForms IGroupListViewForms { get { return this._GroupListViewForms; } }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// main view forms hierarchy node with children
    ///////////////////////////////////////////////////
    public partial class GroupListMainViewForms : ConfigObjectSubBase<GroupListMainViewForms, GroupListMainViewForms.GroupListMainViewFormsValidator>, IComparable<GroupListMainViewForms>, IConfigAcceptVisitor, IGroupListMainViewForms // Class.tt Line: 6
    {
        public partial class GroupListMainViewFormsValidator : ValidatorBase<GroupListMainViewForms, GroupListMainViewFormsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListMainViewForms(ITreeConfigNode parent) 
            : base(parent, GroupListMainViewFormsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(this); // Class.tt Line: 22
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
        }
        public static GroupListMainViewForms Clone(ITreeConfigNode parent, GroupListMainViewForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListMainViewForms vm = new GroupListMainViewForms(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListMainViewForms) // Clone.tt Line: 49
                vm.ListMainViewForms.Add(MainViewForm.Clone(vm, (MainViewForm)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListMainViewForms to, GroupListMainViewForms from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new MainViewForm(to); // Clone.tt Line: 110
                        MainViewForm.Update(p, (MainViewForm)tt, isDeep);
                        to.ListMainViewForms.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListMainViewForms ConvertToVM(Proto.Config.proto_group_list_main_view_forms m, GroupListMainViewForms vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListMainViewForms) // Clone.tt Line: 191
            {
                var tvm = MainViewForm.ConvertToVM(t, new MainViewForm(vm)); // Clone.tt Line: 194
                vm.ListMainViewForms.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListMainViewForms' to 'proto_group_list_main_view_forms'
        public static Proto.Config.proto_group_list_main_view_forms ConvertToProto(GroupListMainViewForms vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_main_view_forms m = new Proto.Config.proto_group_list_main_view_forms(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListMainViewForms) // Clone.tt Line: 227
                m.ListMainViewForms.Add(MainViewForm.ConvertToProto((MainViewForm)t)); // Clone.tt Line: 231
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<MainViewForm> ListMainViewForms // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListMainViewForms; 
            }
            set
            {
                if (this._ListMainViewForms != value)
                {
                    this.OnListMainViewFormsChanging(this._ListMainViewForms, value);
                    this._ListMainViewForms = value;
                    this.OnListMainViewFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<MainViewForm> _ListMainViewForms;
        partial void OnListMainViewFormsChanging(SortedObservableCollection<MainViewForm> from, SortedObservableCollection<MainViewForm> to); // Property.tt Line: 80
        partial void OnListMainViewFormsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IMainViewForm> IListMainViewForms { get { foreach (var t in this._ListMainViewForms) yield return t; } }
        public MainViewForm this[int index] { get { return (MainViewForm)this.ListMainViewForms[index]; } }
        public void Add(MainViewForm item) // Property.tt Line: 87
        { 
            this.ListMainViewForms.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<MainViewForm> items) 
        { 
            this.ListMainViewForms.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListMainViewForms.Count; 
        }
        public void Remove(MainViewForm item) 
        {
            this.ListMainViewForms.Remove(item); 
            item.Parent = null;
        }
    
        #endregion Properties
    }
    public partial class GroupListPropertiesTabs : ConfigObjectSubBase<GroupListPropertiesTabs, GroupListPropertiesTabs.GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IConfigAcceptVisitor, IGroupListPropertiesTabs // Class.tt Line: 6
    {
        public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListPropertiesTabs(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesTabsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListPropertiesTabs Clone(ITreeConfigNode parent, GroupListPropertiesTabs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListPropertiesTabs vm = new GroupListPropertiesTabs(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListPropertiesTabs) // Clone.tt Line: 49
                vm.ListPropertiesTabs.Add(PropertiesTab.Clone(vm, (PropertiesTab)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListPropertiesTabs to, GroupListPropertiesTabs from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new PropertiesTab(to); // Clone.tt Line: 110
                        PropertiesTab.Update(p, (PropertiesTab)tt, isDeep);
                        to.ListPropertiesTabs.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListPropertiesTabs ConvertToVM(Proto.Config.proto_group_list_properties_tabs m, GroupListPropertiesTabs vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListPropertiesTabs) // Clone.tt Line: 191
            {
                var tvm = PropertiesTab.ConvertToVM(t, new PropertiesTab(vm)); // Clone.tt Line: 194
                vm.ListPropertiesTabs.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListPropertiesTabs' to 'proto_group_list_properties_tabs'
        public static Proto.Config.proto_group_list_properties_tabs ConvertToProto(GroupListPropertiesTabs vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_properties_tabs m = new Proto.Config.proto_group_list_properties_tabs(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListPropertiesTabs) // Clone.tt Line: 227
                m.ListPropertiesTabs.Add(PropertiesTab.ConvertToProto((PropertiesTab)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<PropertiesTab> ListPropertiesTabs // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListPropertiesTabs; 
            }
            set
            {
                if (this._ListPropertiesTabs != value)
                {
                    this.OnListPropertiesTabsChanging(this._ListPropertiesTabs, value);
                    this._ListPropertiesTabs = value;
                    this.OnListPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<PropertiesTab> _ListPropertiesTabs;
        partial void OnListPropertiesTabsChanging(SortedObservableCollection<PropertiesTab> from, SortedObservableCollection<PropertiesTab> to); // Property.tt Line: 80
        partial void OnListPropertiesTabsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IPropertiesTab> IListPropertiesTabs { get { foreach (var t in this._ListPropertiesTabs) yield return t; } }
        public PropertiesTab this[int index] { get { return (PropertiesTab)this.ListPropertiesTabs[index]; } }
        public void Add(PropertiesTab item) // Property.tt Line: 87
        { 
            this.ListPropertiesTabs.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<PropertiesTab> items) 
        { 
            this.ListPropertiesTabs.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListPropertiesTabs.Count; 
        }
        public void Remove(PropertiesTab item) 
        {
            this.ListPropertiesTabs.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class PropertiesTab : ConfigObjectSubBase<PropertiesTab, PropertiesTab.PropertiesTabValidator>, IComparable<PropertiesTab>, IConfigAcceptVisitor, IPropertiesTab // Class.tt Line: 6
    {
        public partial class PropertiesTabValidator : ValidatorBase<PropertiesTab, PropertiesTabValidator> { } // Class.tt Line: 8
        #region CTOR
        public PropertiesTab(ITreeConfigNode parent) 
            : base(parent, PropertiesTabValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 28
            this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 28
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static PropertiesTab Clone(ITreeConfigNode parent, PropertiesTab from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            PropertiesTab vm = new PropertiesTab(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 62
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(PropertiesTab to, PropertiesTab from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static PropertiesTab ConvertToVM(Proto.Config.proto_properties_tab m, PropertiesTab vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.GroupProperties == null) // Clone.tt Line: 203
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 205
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 209
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 203
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 205
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 209
            vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 211
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'PropertiesTab' to 'proto_properties_tab'
        public static Proto.Config.proto_properties_tab ConvertToProto(PropertiesTab vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_properties_tab m = new Proto.Config.proto_properties_tab(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 255
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 255
            m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 261
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
        
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupProperties; 
            }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(this._GroupProperties, value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupProperties;
        partial void OnGroupPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 130
        partial void OnGroupPropertiesChanged();
        [BrowsableAttribute(false)]
        public IGroupListProperties IGroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupPropertiesTabs; 
            }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(this._GroupPropertiesTabs, value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        partial void OnGroupPropertiesTabsChanging(GroupListPropertiesTabs from, GroupListPropertiesTabs to); // Property.tt Line: 130
        partial void OnGroupPropertiesTabsChanged();
        [BrowsableAttribute(false)]
        public IGroupListPropertiesTabs IGroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        
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
                    this.OnIsIndexFkChanging(this._IsIndexFk, value);
                    this._IsIndexFk = value;
                    this.OnIsIndexFkChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsIndexFk;
        partial void OnIsIndexFkChanging(bool from, bool to); // Property.tt Line: 156
        partial void OnIsIndexFkChanged();
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListProperties : ConfigObjectSubBase<GroupListProperties, GroupListProperties.GroupListPropertiesValidator>, IComparable<GroupListProperties>, IConfigAcceptVisitor, IGroupListProperties // Class.tt Line: 6
    {
        public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListProperties(ITreeConfigNode parent) 
            : base(parent, GroupListPropertiesValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListProperties = new ConfigNodesCollection<Property>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListProperties Clone(ITreeConfigNode parent, GroupListProperties from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListProperties vm = new GroupListProperties(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListProperties = new ConfigNodesCollection<Property>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListProperties) // Clone.tt Line: 49
                vm.ListProperties.Add(Property.Clone(vm, (Property)t, isDeep));
            vm.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 62
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListProperties to, GroupListProperties from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Property(to); // Clone.tt Line: 110
                        Property.Update(p, (Property)tt, isDeep);
                        to.ListProperties.Add(p);
                    }
                }
            }
            to.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListProperties ConvertToVM(Proto.Config.proto_group_list_properties m, GroupListProperties vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListProperties = new ConfigNodesCollection<Property>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListProperties) // Clone.tt Line: 191
            {
                var tvm = Property.ConvertToVM(t, new Property(vm)); // Clone.tt Line: 194
                vm.ListProperties.Add(tvm);
            }
            vm.LastGenPosition = m.LastGenPosition; // Clone.tt Line: 211
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListProperties' to 'proto_group_list_properties'
        public static Proto.Config.proto_group_list_properties ConvertToProto(GroupListProperties vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_properties m = new Proto.Config.proto_group_list_properties(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListProperties) // Clone.tt Line: 227
                m.ListProperties.Add(Property.ConvertToProto((Property)t)); // Clone.tt Line: 231
            m.LastGenPosition = vm.LastGenPosition; // Clone.tt Line: 261
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Property> ListProperties // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListProperties; 
            }
            set
            {
                if (this._ListProperties != value)
                {
                    this.OnListPropertiesChanging(this._ListProperties, value);
                    this._ListProperties = value;
                    this.OnListPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Property> _ListProperties;
        partial void OnListPropertiesChanging(SortedObservableCollection<Property> from, SortedObservableCollection<Property> to); // Property.tt Line: 80
        partial void OnListPropertiesChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IProperty> IListProperties { get { foreach (var t in this._ListProperties) yield return t; } }
        public Property this[int index] { get { return (Property)this.ListProperties[index]; } }
        public void Add(Property item) // Property.tt Line: 87
        { 
            this.ListProperties.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Property> items) 
        { 
            this.ListProperties.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListProperties.Count; 
        }
        public void Remove(Property item) 
        {
            this.ListProperties.Remove(item); 
            item.Parent = null;
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
                    this.OnLastGenPositionChanging(this._LastGenPosition, value);
                    this._LastGenPosition = value;
                    this.OnLastGenPositionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private uint _LastGenPosition;
        partial void OnLastGenPositionChanging(uint from, uint to); // Property.tt Line: 156
        partial void OnLastGenPositionChanged();
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class Property : ConfigObjectSubBase<Property, Property.PropertyValidator>, IComparable<Property>, IConfigAcceptVisitor, IProperty // Class.tt Line: 6
    {
        public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { } // Class.tt Line: 8
        #region CTOR
        public Property(ITreeConfigNode parent) 
            : base(parent, PropertyValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 26
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Property Clone(ITreeConfigNode parent, Property from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Property vm = new Property(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.DataType = DataType.Clone(from.DataType, isDeep);
            vm.Position = from.Position; // Clone.tt Line: 62
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Property to, Property from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                DataType.Update(to.DataType, from.DataType, isDeep);
            to.Position = from.Position; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Property ConvertToVM(Proto.Config.proto_property m, Property vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.DataType == null) // Clone.tt Line: 203
                vm.DataType = new DataType(); // Clone.tt Line: 207
            DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 209
            vm.Position = m.Position; // Clone.tt Line: 211
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Property' to 'proto_property'
        public static Proto.Config.proto_property ConvertToProto(Property vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_property m = new Proto.Config.proto_property(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 255
            m.Position = vm.Position; // Clone.tt Line: 261
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 109
        { 
            get 
            { 
                return this._DataType; 
            }
            set
            {
                if (this._DataType != value)
                {
                    this.OnDataTypeChanging(this._DataType, value);
                    this._DataType = value;
                    this.OnDataTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private DataType _DataType;
        partial void OnDataTypeChanging(DataType from, DataType to); // Property.tt Line: 130
        partial void OnDataTypeChanged();
        [BrowsableAttribute(false)]
        public IDataType IDataType { get { return this._DataType; } }
        
        
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
                    this.OnPositionChanging(this._Position, value);
                    this._Position = value;
                    this.OnPositionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private uint _Position;
        partial void OnPositionChanging(uint from, uint to); // Property.tt Line: 156
        partial void OnPositionChanged();
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListConstants : ConfigObjectSubBase<GroupListConstants, GroupListConstants.GroupListConstantsValidator>, IComparable<GroupListConstants>, IConfigAcceptVisitor, IGroupListConstants // Class.tt Line: 6
    {
        public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListConstants(ITreeConfigNode parent) 
            : base(parent, GroupListConstantsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListConstants = new ConfigNodesCollection<Constant>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListConstants Clone(ITreeConfigNode parent, GroupListConstants from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListConstants vm = new GroupListConstants(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListConstants = new ConfigNodesCollection<Constant>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListConstants) // Clone.tt Line: 49
                vm.ListConstants.Add(Constant.Clone(vm, (Constant)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListConstants to, GroupListConstants from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Constant(to); // Clone.tt Line: 110
                        Constant.Update(p, (Constant)tt, isDeep);
                        to.ListConstants.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListConstants ConvertToVM(Proto.Config.proto_group_list_constants m, GroupListConstants vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListConstants = new ConfigNodesCollection<Constant>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListConstants) // Clone.tt Line: 191
            {
                var tvm = Constant.ConvertToVM(t, new Constant(vm)); // Clone.tt Line: 194
                vm.ListConstants.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListConstants' to 'proto_group_list_constants'
        public static Proto.Config.proto_group_list_constants ConvertToProto(GroupListConstants vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_constants m = new Proto.Config.proto_group_list_constants(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListConstants) // Clone.tt Line: 227
                m.ListConstants.Add(Constant.ConvertToProto((Constant)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Constant> ListConstants // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListConstants; 
            }
            set
            {
                if (this._ListConstants != value)
                {
                    this.OnListConstantsChanging(this._ListConstants, value);
                    this._ListConstants = value;
                    this.OnListConstantsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Constant> _ListConstants;
        partial void OnListConstantsChanging(SortedObservableCollection<Constant> from, SortedObservableCollection<Constant> to); // Property.tt Line: 80
        partial void OnListConstantsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IConstant> IListConstants { get { foreach (var t in this._ListConstants) yield return t; } }
        public Constant this[int index] { get { return (Constant)this.ListConstants[index]; } }
        public void Add(Constant item) // Property.tt Line: 87
        { 
            this.ListConstants.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Constant> items) 
        { 
            this.ListConstants.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListConstants.Count; 
        }
        public void Remove(Constant item) 
        {
            this.ListConstants.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    
    ///////////////////////////////////////////////////
    /// Constant application wise value
    ///////////////////////////////////////////////////
    public partial class Constant : ConfigObjectSubBase<Constant, Constant.ConstantValidator>, IComparable<Constant>, IConfigAcceptVisitor, IConstant // Class.tt Line: 6
    {
        public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { } // Class.tt Line: 8
        #region CTOR
        public Constant(ITreeConfigNode parent) 
            : base(parent, ConstantValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.DataType = new DataType(); // Class.tt Line: 26
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Constant Clone(ITreeConfigNode parent, Constant from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Constant vm = new Constant(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.DataType = DataType.Clone(from.DataType, isDeep);
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Constant to, Constant from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                DataType.Update(to.DataType, from.DataType, isDeep);
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Constant ConvertToVM(Proto.Config.proto_constant m, Constant vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.DataType == null) // Clone.tt Line: 203
                vm.DataType = new DataType(); // Clone.tt Line: 207
            DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 209
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Constant' to 'proto_constant'
        public static Proto.Config.proto_constant ConvertToProto(Constant vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_constant m = new Proto.Config.proto_constant(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 255
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [PropertyOrderAttribute(4)]
        [ExpandableObjectAttribute()]
        [DisplayName("Type")]
        public DataType DataType // Property.tt Line: 109
        { 
            get 
            { 
                return this._DataType; 
            }
            set
            {
                if (this._DataType != value)
                {
                    this.OnDataTypeChanging(this._DataType, value);
                    this._DataType = value;
                    this.OnDataTypeChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private DataType _DataType;
        partial void OnDataTypeChanging(DataType from, DataType to); // Property.tt Line: 130
        partial void OnDataTypeChanged();
        [BrowsableAttribute(false)]
        public IDataType IDataType { get { return this._DataType; } }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListEnumerations : ConfigObjectSubBase<GroupListEnumerations, GroupListEnumerations.GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IConfigAcceptVisitor, IGroupListEnumerations // Class.tt Line: 6
    {
        public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListEnumerations(ITreeConfigNode parent) 
            : base(parent, GroupListEnumerationsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListEnumerations = new ConfigNodesCollection<Enumeration>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListEnumerations Clone(ITreeConfigNode parent, GroupListEnumerations from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListEnumerations vm = new GroupListEnumerations(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListEnumerations) // Clone.tt Line: 49
                vm.ListEnumerations.Add(Enumeration.Clone(vm, (Enumeration)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListEnumerations to, GroupListEnumerations from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Enumeration(to); // Clone.tt Line: 110
                        Enumeration.Update(p, (Enumeration)tt, isDeep);
                        to.ListEnumerations.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListEnumerations ConvertToVM(Proto.Config.proto_group_list_enumerations m, GroupListEnumerations vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListEnumerations) // Clone.tt Line: 191
            {
                var tvm = Enumeration.ConvertToVM(t, new Enumeration(vm)); // Clone.tt Line: 194
                vm.ListEnumerations.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListEnumerations' to 'proto_group_list_enumerations'
        public static Proto.Config.proto_group_list_enumerations ConvertToProto(GroupListEnumerations vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_enumerations m = new Proto.Config.proto_group_list_enumerations(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListEnumerations) // Clone.tt Line: 227
                m.ListEnumerations.Add(Enumeration.ConvertToProto((Enumeration)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Enumeration> ListEnumerations // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListEnumerations; 
            }
            set
            {
                if (this._ListEnumerations != value)
                {
                    this.OnListEnumerationsChanging(this._ListEnumerations, value);
                    this._ListEnumerations = value;
                    this.OnListEnumerationsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Enumeration> _ListEnumerations;
        partial void OnListEnumerationsChanging(SortedObservableCollection<Enumeration> from, SortedObservableCollection<Enumeration> to); // Property.tt Line: 80
        partial void OnListEnumerationsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IEnumeration> IListEnumerations { get { foreach (var t in this._ListEnumerations) yield return t; } }
        public Enumeration this[int index] { get { return (Enumeration)this.ListEnumerations[index]; } }
        public void Add(Enumeration item) // Property.tt Line: 87
        { 
            this.ListEnumerations.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Enumeration> items) 
        { 
            this.ListEnumerations.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListEnumerations.Count; 
        }
        public void Remove(Enumeration item) 
        {
            this.ListEnumerations.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class Enumeration : ConfigObjectSubBase<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>, IConfigAcceptVisitor, IEnumeration // Class.tt Line: 6
    {
        public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { } // Class.tt Line: 8
        #region CTOR
        public Enumeration(ITreeConfigNode parent) 
            : base(parent, EnumerationValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Enumeration Clone(ITreeConfigNode parent, Enumeration from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Enumeration vm = new Enumeration(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 62
            vm.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 62
            vm.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListEnumerationPairs) // Clone.tt Line: 49
                vm.ListEnumerationPairs.Add(EnumerationPair.Clone(vm, (EnumerationPair)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Enumeration to, Enumeration from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 134
            to.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new EnumerationPair(to); // Clone.tt Line: 110
                        EnumerationPair.Update(p, (EnumerationPair)tt, isDeep);
                        to.ListEnumerationPairs.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Enumeration ConvertToVM(Proto.Config.proto_enumeration m, Enumeration vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.DataTypeEnum = (EnumEnumerationType)m.DataTypeEnum; // Clone.tt Line: 211
            vm.DataTypeLength = m.DataTypeLength; // Clone.tt Line: 211
            vm.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListEnumerationPairs) // Clone.tt Line: 191
            {
                var tvm = EnumerationPair.ConvertToVM(t, new EnumerationPair(vm)); // Clone.tt Line: 194
                vm.ListEnumerationPairs.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Enumeration' to 'proto_enumeration'
        public static Proto.Config.proto_enumeration ConvertToProto(Enumeration vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_enumeration m = new Proto.Config.proto_enumeration(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.DataTypeEnum = (Proto.Config.enum_enumeration_type)vm.DataTypeEnum; // Clone.tt Line: 259
            m.DataTypeLength = vm.DataTypeLength; // Clone.tt Line: 261
            foreach (var t in vm.ListEnumerationPairs) // Clone.tt Line: 227
                m.ListEnumerationPairs.Add(EnumerationPair.ConvertToProto((EnumerationPair)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
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
                    this.OnDataTypeEnumChanging(this._DataTypeEnum, value);
                    this._DataTypeEnum = value;
                    this.OnDataTypeEnumChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private EnumEnumerationType _DataTypeEnum;
        partial void OnDataTypeEnumChanging(EnumEnumerationType from, EnumEnumerationType to); // Property.tt Line: 156
        partial void OnDataTypeEnumChanged();
        
        
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
                    this.OnDataTypeLengthChanging(this._DataTypeLength, value);
                    this._DataTypeLength = value;
                    this.OnDataTypeLengthChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private int _DataTypeLength;
        partial void OnDataTypeLengthChanging(int from, int to); // Property.tt Line: 156
        partial void OnDataTypeLengthChanged();
        
        [DisplayName("Elements")]
        [NewItemTypes(typeof(EnumerationPair))]
        public ConfigNodesCollection<EnumerationPair> ListEnumerationPairs // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListEnumerationPairs; 
            }
            set
            {
                if (this._ListEnumerationPairs != value)
                {
                    this.OnListEnumerationPairsChanging(this._ListEnumerationPairs, value);
                    this._ListEnumerationPairs = value;
                    this.OnListEnumerationPairsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<EnumerationPair> _ListEnumerationPairs;
        partial void OnListEnumerationPairsChanging(SortedObservableCollection<EnumerationPair> from, SortedObservableCollection<EnumerationPair> to); // Property.tt Line: 80
        partial void OnListEnumerationPairsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IEnumerationPair> IListEnumerationPairs { get { foreach (var t in this._ListEnumerationPairs) yield return t; } }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class EnumerationPair : ConfigObjectSubBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>, IConfigAcceptVisitor, IEnumerationPair // Class.tt Line: 6
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
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.Value = from.Value; // Clone.tt Line: 62
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(EnumerationPair to, EnumerationPair from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            to.Value = from.Value; // Clone.tt Line: 134
        }
        // Clone.tt Line: 140
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
        public static EnumerationPair ConvertToVM(Proto.Config.proto_enumeration_pair m, EnumerationPair vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.Value = m.Value; // Clone.tt Line: 211
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
        public static Proto.Config.proto_enumeration_pair ConvertToProto(EnumerationPair vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_enumeration_pair m = new Proto.Config.proto_enumeration_pair(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.Value = vm.Value; // Clone.tt Line: 261
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// TODO struct for different types, at least INTEGER
        ///////////////////////////////////////////////////
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
                    this.OnValueChanging(this._Value, value);
                    this._Value = value;
                    this.OnValueChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Value = string.Empty;
        partial void OnValueChanging(string from, string to); // Property.tt Line: 156
        partial void OnValueChanged();
    
        #endregion Properties
    }
    public partial class Catalog : ConfigObjectSubBase<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IConfigAcceptVisitor, ICatalog // Class.tt Line: 6
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
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Catalog Clone(ITreeConfigNode parent, Catalog from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Catalog vm = new Catalog(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupForms = GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupReports = GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Catalog to, Catalog from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Catalog ConvertToVM(Proto.Config.proto_catalog m, Catalog vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.GroupProperties == null) // Clone.tt Line: 203
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 205
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 209
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 203
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 205
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 209
            if (vm.GroupForms == null) // Clone.tt Line: 203
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 205
            GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 209
            if (vm.GroupReports == null) // Clone.tt Line: 203
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 205
            GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 209
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Catalog' to 'proto_catalog'
        public static Proto.Config.proto_catalog ConvertToProto(Catalog vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_catalog m = new Proto.Config.proto_catalog(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 255
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 255
            m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 255
            m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 255
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
        
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupProperties; 
            }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(this._GroupProperties, value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupProperties;
        partial void OnGroupPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 130
        partial void OnGroupPropertiesChanged();
        [BrowsableAttribute(false)]
        public IGroupListProperties IGroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupPropertiesTabs; 
            }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(this._GroupPropertiesTabs, value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        partial void OnGroupPropertiesTabsChanging(GroupListPropertiesTabs from, GroupListPropertiesTabs to); // Property.tt Line: 130
        partial void OnGroupPropertiesTabsChanged();
        [BrowsableAttribute(false)]
        public IGroupListPropertiesTabs IGroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupForms; 
            }
            set
            {
                if (this._GroupForms != value)
                {
                    this.OnGroupFormsChanging(this._GroupForms, value);
                    this._GroupForms = value;
                    this.OnGroupFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListForms _GroupForms;
        partial void OnGroupFormsChanging(GroupListForms from, GroupListForms to); // Property.tt Line: 130
        partial void OnGroupFormsChanged();
        [BrowsableAttribute(false)]
        public IGroupListForms IGroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupReports; 
            }
            set
            {
                if (this._GroupReports != value)
                {
                    this.OnGroupReportsChanging(this._GroupReports, value);
                    this._GroupReports = value;
                    this.OnGroupReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListReports _GroupReports;
        partial void OnGroupReportsChanging(GroupListReports from, GroupListReports to); // Property.tt Line: 130
        partial void OnGroupReportsChanged();
        [BrowsableAttribute(false)]
        public IGroupListReports IGroupReports { get { return this._GroupReports; } }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListCatalogs : ConfigObjectSubBase<GroupListCatalogs, GroupListCatalogs.GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IConfigAcceptVisitor, IGroupListCatalogs // Class.tt Line: 6
    {
        public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListCatalogs(ITreeConfigNode parent) 
            : base(parent, GroupListCatalogsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListCatalogs = new ConfigNodesCollection<Catalog>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListCatalogs Clone(ITreeConfigNode parent, GroupListCatalogs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListCatalogs vm = new GroupListCatalogs(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListCatalogs = new ConfigNodesCollection<Catalog>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListCatalogs) // Clone.tt Line: 49
                vm.ListCatalogs.Add(Catalog.Clone(vm, (Catalog)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListCatalogs to, GroupListCatalogs from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Catalog(to); // Clone.tt Line: 110
                        Catalog.Update(p, (Catalog)tt, isDeep);
                        to.ListCatalogs.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListCatalogs ConvertToVM(Proto.Config.proto_group_list_catalogs m, GroupListCatalogs vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListCatalogs = new ConfigNodesCollection<Catalog>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListCatalogs) // Clone.tt Line: 191
            {
                var tvm = Catalog.ConvertToVM(t, new Catalog(vm)); // Clone.tt Line: 194
                vm.ListCatalogs.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
        public static Proto.Config.proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_catalogs m = new Proto.Config.proto_group_list_catalogs(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListCatalogs) // Clone.tt Line: 227
                m.ListCatalogs.Add(Catalog.ConvertToProto((Catalog)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Catalog> ListCatalogs // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListCatalogs; 
            }
            set
            {
                if (this._ListCatalogs != value)
                {
                    this.OnListCatalogsChanging(this._ListCatalogs, value);
                    this._ListCatalogs = value;
                    this.OnListCatalogsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Catalog> _ListCatalogs;
        partial void OnListCatalogsChanging(SortedObservableCollection<Catalog> from, SortedObservableCollection<Catalog> to); // Property.tt Line: 80
        partial void OnListCatalogsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<ICatalog> IListCatalogs { get { foreach (var t in this._ListCatalogs) yield return t; } }
        public Catalog this[int index] { get { return (Catalog)this.ListCatalogs[index]; } }
        public void Add(Catalog item) // Property.tt Line: 87
        { 
            this.ListCatalogs.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Catalog> items) 
        { 
            this.ListCatalogs.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListCatalogs.Count; 
        }
        public void Remove(Catalog item) 
        {
            this.ListCatalogs.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupDocuments : ConfigObjectSubBase<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IConfigAcceptVisitor, IGroupDocuments // Class.tt Line: 6
    {
        public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupDocuments(ITreeConfigNode parent) 
            : base(parent, GroupDocumentsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.GroupSharedProperties = new GroupListProperties(this); // Class.tt Line: 28
            this.GroupListDocuments = new GroupListDocuments(this); // Class.tt Line: 28
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupDocuments Clone(ITreeConfigNode parent, GroupDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupDocuments vm = new GroupDocuments(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.GroupSharedProperties = GroupListProperties.Clone(vm, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupListDocuments = GroupListDocuments.Clone(vm, from.GroupListDocuments, isDeep);
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupDocuments to, GroupDocuments from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                GroupListProperties.Update(to.GroupSharedProperties, from.GroupSharedProperties, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListDocuments.Update(to.GroupListDocuments, from.GroupListDocuments, isDeep);
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupDocuments ConvertToVM(Proto.Config.proto_group_documents m, GroupDocuments vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.GroupSharedProperties == null) // Clone.tt Line: 203
                vm.GroupSharedProperties = new GroupListProperties(vm); // Clone.tt Line: 205
            GroupListProperties.ConvertToVM(m.GroupSharedProperties, vm.GroupSharedProperties); // Clone.tt Line: 209
            if (vm.GroupListDocuments == null) // Clone.tt Line: 203
                vm.GroupListDocuments = new GroupListDocuments(vm); // Clone.tt Line: 205
            GroupListDocuments.ConvertToVM(m.GroupListDocuments, vm.GroupListDocuments); // Clone.tt Line: 209
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupDocuments' to 'proto_group_documents'
        public static Proto.Config.proto_group_documents ConvertToProto(GroupDocuments vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_documents m = new Proto.Config.proto_group_documents(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.GroupSharedProperties = GroupListProperties.ConvertToProto(vm.GroupSharedProperties); // Clone.tt Line: 255
            m.GroupListDocuments = GroupListDocuments.ConvertToProto(vm.GroupListDocuments); // Clone.tt Line: 255
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
        
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupSharedProperties // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupSharedProperties; 
            }
            set
            {
                if (this._GroupSharedProperties != value)
                {
                    this.OnGroupSharedPropertiesChanging(this._GroupSharedProperties, value);
                    this._GroupSharedProperties = value;
                    this.OnGroupSharedPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupSharedProperties;
        partial void OnGroupSharedPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 130
        partial void OnGroupSharedPropertiesChanged();
        [BrowsableAttribute(false)]
        public IGroupListProperties IGroupSharedProperties { get { return this._GroupSharedProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListDocuments GroupListDocuments // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupListDocuments; 
            }
            set
            {
                if (this._GroupListDocuments != value)
                {
                    this.OnGroupListDocumentsChanging(this._GroupListDocuments, value);
                    this._GroupListDocuments = value;
                    this.OnGroupListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListDocuments _GroupListDocuments;
        partial void OnGroupListDocumentsChanging(GroupListDocuments from, GroupListDocuments to); // Property.tt Line: 130
        partial void OnGroupListDocumentsChanged();
        [BrowsableAttribute(false)]
        public IGroupListDocuments IGroupListDocuments { get { return this._GroupListDocuments; } }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class Document : ConfigObjectSubBase<Document, Document.DocumentValidator>, IComparable<Document>, IConfigAcceptVisitor, IDocument // Class.tt Line: 6
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
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Document Clone(ITreeConfigNode parent, Document from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Document vm = new Document(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            if (isDeep) // Clone.tt Line: 59
                vm.GroupProperties = GroupListProperties.Clone(vm, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(vm, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupForms = GroupListForms.Clone(vm, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 59
                vm.GroupReports = GroupListReports.Clone(vm, from.GroupReports, isDeep);
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Document to, Document from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 131
                GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
            if (isDeep) // Clone.tt Line: 131
                GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Document ConvertToVM(Proto.Config.proto_document m, Document vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            if (vm.GroupProperties == null) // Clone.tt Line: 203
                vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 205
            GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 209
            if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 203
                vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 205
            GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 209
            if (vm.GroupForms == null) // Clone.tt Line: 203
                vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 205
            GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 209
            if (vm.GroupReports == null) // Clone.tt Line: 203
                vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 205
            GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 209
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Document' to 'proto_document'
        public static Proto.Config.proto_document ConvertToProto(Document vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_document m = new Proto.Config.proto_document(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 255
            m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 255
            m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 255
            m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 255
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
        
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public GroupListProperties GroupProperties // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupProperties; 
            }
            set
            {
                if (this._GroupProperties != value)
                {
                    this.OnGroupPropertiesChanging(this._GroupProperties, value);
                    this._GroupProperties = value;
                    this.OnGroupPropertiesChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListProperties _GroupProperties;
        partial void OnGroupPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 130
        partial void OnGroupPropertiesChanged();
        [BrowsableAttribute(false)]
        public IGroupListProperties IGroupProperties { get { return this._GroupProperties; } }
        
        [BrowsableAttribute(false)]
        public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupPropertiesTabs; 
            }
            set
            {
                if (this._GroupPropertiesTabs != value)
                {
                    this.OnGroupPropertiesTabsChanging(this._GroupPropertiesTabs, value);
                    this._GroupPropertiesTabs = value;
                    this.OnGroupPropertiesTabsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListPropertiesTabs _GroupPropertiesTabs;
        partial void OnGroupPropertiesTabsChanging(GroupListPropertiesTabs from, GroupListPropertiesTabs to); // Property.tt Line: 130
        partial void OnGroupPropertiesTabsChanged();
        [BrowsableAttribute(false)]
        public IGroupListPropertiesTabs IGroupPropertiesTabs { get { return this._GroupPropertiesTabs; } }
        
        [BrowsableAttribute(false)]
        public GroupListForms GroupForms // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupForms; 
            }
            set
            {
                if (this._GroupForms != value)
                {
                    this.OnGroupFormsChanging(this._GroupForms, value);
                    this._GroupForms = value;
                    this.OnGroupFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListForms _GroupForms;
        partial void OnGroupFormsChanging(GroupListForms from, GroupListForms to); // Property.tt Line: 130
        partial void OnGroupFormsChanged();
        [BrowsableAttribute(false)]
        public IGroupListForms IGroupForms { get { return this._GroupForms; } }
        
        [BrowsableAttribute(false)]
        public GroupListReports GroupReports // Property.tt Line: 109
        { 
            get 
            { 
                return this._GroupReports; 
            }
            set
            {
                if (this._GroupReports != value)
                {
                    this.OnGroupReportsChanging(this._GroupReports, value);
                    this._GroupReports = value;
                    this.OnGroupReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private GroupListReports _GroupReports;
        partial void OnGroupReportsChanging(GroupListReports from, GroupListReports to); // Property.tt Line: 130
        partial void OnGroupReportsChanged();
        [BrowsableAttribute(false)]
        public IGroupListReports IGroupReports { get { return this._GroupReports; } }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListDocuments : ConfigObjectSubBase<GroupListDocuments, GroupListDocuments.GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IConfigAcceptVisitor, IGroupListDocuments // Class.tt Line: 6
    {
        public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListDocuments(ITreeConfigNode parent) 
            : base(parent, GroupListDocumentsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListDocuments Clone(ITreeConfigNode parent, GroupListDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListDocuments vm = new GroupListDocuments(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListDocuments) // Clone.tt Line: 49
                vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListDocuments to, GroupListDocuments from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Document(to); // Clone.tt Line: 110
                        Document.Update(p, (Document)tt, isDeep);
                        to.ListDocuments.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListDocuments ConvertToVM(Proto.Config.proto_group_list_documents m, GroupListDocuments vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListDocuments) // Clone.tt Line: 191
            {
                var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 194
                vm.ListDocuments.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
        public static Proto.Config.proto_group_list_documents ConvertToProto(GroupListDocuments vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_documents m = new Proto.Config.proto_group_list_documents(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListDocuments) // Clone.tt Line: 227
                m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Document> ListDocuments // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListDocuments; 
            }
            set
            {
                if (this._ListDocuments != value)
                {
                    this.OnListDocumentsChanging(this._ListDocuments, value);
                    this._ListDocuments = value;
                    this.OnListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Document> _ListDocuments;
        partial void OnListDocumentsChanging(SortedObservableCollection<Document> from, SortedObservableCollection<Document> to); // Property.tt Line: 80
        partial void OnListDocumentsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IDocument> IListDocuments { get { foreach (var t in this._ListDocuments) yield return t; } }
        public Document this[int index] { get { return (Document)this.ListDocuments[index]; } }
        public void Add(Document item) // Property.tt Line: 87
        { 
            this.ListDocuments.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Document> items) 
        { 
            this.ListDocuments.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListDocuments.Count; 
        }
        public void Remove(Document item) 
        {
            this.ListDocuments.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListJournals : ConfigObjectSubBase<GroupListJournals, GroupListJournals.GroupListJournalsValidator>, IComparable<GroupListJournals>, IConfigAcceptVisitor, IGroupListJournals // Class.tt Line: 6
    {
        public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListJournals(ITreeConfigNode parent) 
            : base(parent, GroupListJournalsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListJournals = new ConfigNodesCollection<Journal>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListJournals Clone(ITreeConfigNode parent, GroupListJournals from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListJournals vm = new GroupListJournals(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListJournals) // Clone.tt Line: 49
                vm.ListJournals.Add(Journal.Clone(vm, (Journal)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListJournals to, GroupListJournals from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Journal(to); // Clone.tt Line: 110
                        Journal.Update(p, (Journal)tt, isDeep);
                        to.ListJournals.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListJournals ConvertToVM(Proto.Config.proto_group_list_journals m, GroupListJournals vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListJournals) // Clone.tt Line: 191
            {
                var tvm = Journal.ConvertToVM(t, new Journal(vm)); // Clone.tt Line: 194
                vm.ListJournals.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListJournals' to 'proto_group_list_journals'
        public static Proto.Config.proto_group_list_journals ConvertToProto(GroupListJournals vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_journals m = new Proto.Config.proto_group_list_journals(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListJournals) // Clone.tt Line: 227
                m.ListJournals.Add(Journal.ConvertToProto((Journal)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Journal> ListJournals // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListJournals; 
            }
            set
            {
                if (this._ListJournals != value)
                {
                    this.OnListJournalsChanging(this._ListJournals, value);
                    this._ListJournals = value;
                    this.OnListJournalsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Journal> _ListJournals;
        partial void OnListJournalsChanging(SortedObservableCollection<Journal> from, SortedObservableCollection<Journal> to); // Property.tt Line: 80
        partial void OnListJournalsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IJournal> IListJournals { get { foreach (var t in this._ListJournals) yield return t; } }
        public Journal this[int index] { get { return (Journal)this.ListJournals[index]; } }
        public void Add(Journal item) // Property.tt Line: 87
        { 
            this.ListJournals.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Journal> items) 
        { 
            this.ListJournals.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListJournals.Count; 
        }
        public void Remove(Journal item) 
        {
            this.ListJournals.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class Journal : ConfigObjectSubBase<Journal, Journal.JournalValidator>, IComparable<Journal>, IConfigAcceptVisitor, IJournal // Class.tt Line: 6
    {
        public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { } // Class.tt Line: 8
        #region CTOR
        public Journal(ITreeConfigNode parent) 
            : base(parent, JournalValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Journal Clone(ITreeConfigNode parent, Journal from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Journal vm = new Journal(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListDocuments) // Clone.tt Line: 49
                vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Journal to, Journal from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Document(to); // Clone.tt Line: 110
                        Document.Update(p, (Document)tt, isDeep);
                        to.ListDocuments.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Journal ConvertToVM(Proto.Config.proto_journal m, Journal vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListDocuments) // Clone.tt Line: 191
            {
                var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 194
                vm.ListDocuments.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Journal' to 'proto_journal'
        public static Proto.Config.proto_journal ConvertToProto(Journal vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_journal m = new Proto.Config.proto_journal(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListDocuments) // Clone.tt Line: 227
                m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Document> ListDocuments // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListDocuments; 
            }
            set
            {
                if (this._ListDocuments != value)
                {
                    this.OnListDocumentsChanging(this._ListDocuments, value);
                    this._ListDocuments = value;
                    this.OnListDocumentsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Document> _ListDocuments;
        partial void OnListDocumentsChanging(SortedObservableCollection<Document> from, SortedObservableCollection<Document> to); // Property.tt Line: 80
        partial void OnListDocumentsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IDocument> IListDocuments { get { foreach (var t in this._ListDocuments) yield return t; } }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListForms : ConfigObjectSubBase<GroupListForms, GroupListForms.GroupListFormsValidator>, IComparable<GroupListForms>, IConfigAcceptVisitor, IGroupListForms // Class.tt Line: 6
    {
        public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListForms(ITreeConfigNode parent) 
            : base(parent, GroupListFormsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListForms = new ConfigNodesCollection<Form>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListForms Clone(ITreeConfigNode parent, GroupListForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListForms vm = new GroupListForms(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListForms) // Clone.tt Line: 49
                vm.ListForms.Add(Form.Clone(vm, (Form)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListForms to, GroupListForms from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Form(to); // Clone.tt Line: 110
                        Form.Update(p, (Form)tt, isDeep);
                        to.ListForms.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListForms ConvertToVM(Proto.Config.proto_group_list_forms m, GroupListForms vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListForms) // Clone.tt Line: 191
            {
                var tvm = Form.ConvertToVM(t, new Form(vm)); // Clone.tt Line: 194
                vm.ListForms.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListForms' to 'proto_group_list_forms'
        public static Proto.Config.proto_group_list_forms ConvertToProto(GroupListForms vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_forms m = new Proto.Config.proto_group_list_forms(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListForms) // Clone.tt Line: 227
                m.ListForms.Add(Form.ConvertToProto((Form)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Form> ListForms // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListForms; 
            }
            set
            {
                if (this._ListForms != value)
                {
                    this.OnListFormsChanging(this._ListForms, value);
                    this._ListForms = value;
                    this.OnListFormsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Form> _ListForms;
        partial void OnListFormsChanging(SortedObservableCollection<Form> from, SortedObservableCollection<Form> to); // Property.tt Line: 80
        partial void OnListFormsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IForm> IListForms { get { foreach (var t in this._ListForms) yield return t; } }
        public Form this[int index] { get { return (Form)this.ListForms[index]; } }
        public void Add(Form item) // Property.tt Line: 87
        { 
            this.ListForms.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Form> items) 
        { 
            this.ListForms.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListForms.Count; 
        }
        public void Remove(Form item) 
        {
            this.ListForms.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class Form : ConfigObjectSubBase<Form, Form.FormValidator>, IComparable<Form>, IConfigAcceptVisitor, IForm // Class.tt Line: 6
    {
        public partial class FormValidator : ValidatorBase<Form, FormValidator> { } // Class.tt Line: 8
        #region CTOR
        public Form(ITreeConfigNode parent) 
            : base(parent, FormValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Form Clone(ITreeConfigNode parent, Form from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Form vm = new Form(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Form to, Form from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Form ConvertToVM(Proto.Config.proto_form m, Form vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Form' to 'proto_form'
        public static Proto.Config.proto_form ConvertToProto(Form vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_form m = new Proto.Config.proto_form(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_forms = 7;
        ///////////////////////////////////////////////////
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class GroupListReports : ConfigObjectSubBase<GroupListReports, GroupListReports.GroupListReportsValidator>, IComparable<GroupListReports>, IConfigAcceptVisitor, IGroupListReports // Class.tt Line: 6
    {
        public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { } // Class.tt Line: 8
        #region CTOR
        public GroupListReports(ITreeConfigNode parent) 
            : base(parent, GroupListReportsValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListReports = new ConfigNodesCollection<Report>(this); // Class.tt Line: 22
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
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
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static GroupListReports Clone(ITreeConfigNode parent, GroupListReports from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            GroupListReports vm = new GroupListReports(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListReports) // Clone.tt Line: 49
                vm.ListReports.Add(Report.Clone(vm, (Report)t, isDeep));
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(GroupListReports to, GroupListReports from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
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
                        var p = new Report(to); // Clone.tt Line: 110
                        Report.Update(p, (Report)tt, isDeep);
                        to.ListReports.Add(p);
                    }
                }
            }
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static GroupListReports ConvertToVM(Proto.Config.proto_group_list_reports m, GroupListReports vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListReports) // Clone.tt Line: 191
            {
                var tvm = Report.ConvertToVM(t, new Report(vm)); // Clone.tt Line: 194
                vm.ListReports.Add(tvm);
            }
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'GroupListReports' to 'proto_group_list_reports'
        public static Proto.Config.proto_group_list_reports ConvertToProto(GroupListReports vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_group_list_reports m = new Proto.Config.proto_group_list_reports(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListReports) // Clone.tt Line: 227
                m.ListReports.Add(Report.ConvertToProto((Report)t)); // Clone.tt Line: 231
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
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
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_property list_shared_properties = 6;
        ///////////////////////////////////////////////////
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<Report> ListReports // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListReports; 
            }
            set
            {
                if (this._ListReports != value)
                {
                    this.OnListReportsChanging(this._ListReports, value);
                    this._ListReports = value;
                    this.OnListReportsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<Report> _ListReports;
        partial void OnListReportsChanging(SortedObservableCollection<Report> from, SortedObservableCollection<Report> to); // Property.tt Line: 80
        partial void OnListReportsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IReport> IListReports { get { foreach (var t in this._ListReports) yield return t; } }
        public Report this[int index] { get { return (Report)this.ListReports[index]; } }
        public void Add(Report item) // Property.tt Line: 87
        { 
            this.ListReports.Add(item); 
            item.Parent = this;
        }
        public void AddRange(IEnumerable<Report> items) 
        { 
            this.ListReports.AddRange(items); 
            foreach (var t in items)
                t.Parent = this;
        }
        public int Count() 
        { 
            return this.ListReports.Count; 
        }
        public void Remove(Report item) 
        {
            this.ListReports.Remove(item); 
            item.Parent = null;
        }
        
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class Report : ConfigObjectSubBase<Report, Report.ReportValidator>, IComparable<Report>, IConfigAcceptVisitor, IReport // Class.tt Line: 6
    {
        public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { } // Class.tt Line: 8
        #region CTOR
        public Report(ITreeConfigNode parent) 
            : base(parent, ReportValidator.Validator) // Class.tt Line: 12
        {
            this.OnInitBegin();
            this.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(this); // Class.tt Line: 22
            this.OnInit();
        }
        partial void OnInitBegin();
        partial void OnInit();
        #endregion CTOR
        #region Procedures
        
        public override void Sort(Type type) // Clone.tt Line: 8
        {
            if (type == typeof(GeneratorSettings)) // Clone.tt Line: 15
            {
                this.ListGeneratorsSettings.Sort();
            }
        }
        public static Report Clone(ITreeConfigNode parent, Report from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            Report vm = new Report(parent);
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
            vm.NameUi = from.NameUi; // Clone.tt Line: 62
            vm.Description = from.Description; // Clone.tt Line: 62
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 48
            foreach (var t in from.ListGeneratorsSettings) // Clone.tt Line: 49
                vm.ListGeneratorsSettings.Add(GeneratorSettings.Clone(vm, (GeneratorSettings)t, isDeep));
            if (isNewGuid) // Clone.tt Line: 67
                vm.SetNewGuid();
            return vm;
        }
        public static void Update(Report to, Report from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.SortingValue = from.SortingValue; // Clone.tt Line: 134
            to.NameUi = from.NameUi; // Clone.tt Line: 134
            to.Description = from.Description; // Clone.tt Line: 134
            if (isDeep) // Clone.tt Line: 79
            {
                foreach (var t in to.ListGeneratorsSettings.ToList())
                {
                    bool isfound = false;
                    foreach (var tt in from.ListGeneratorsSettings)
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            GeneratorSettings.Update((GeneratorSettings)t, (GeneratorSettings)tt, isDeep);
                            break;
                        }
                    }
                    if (!isfound)
                        to.ListGeneratorsSettings.Remove(t);
                }
                foreach (var tt in from.ListGeneratorsSettings)
                {
                    bool isfound = false;
                    foreach (var t in to.ListGeneratorsSettings.ToList())
                    {
                        if (t == tt)
                        {
                            isfound = true;
                            break;
                        }
                    }
                    if (!isfound)
                    {
                        var p = new GeneratorSettings(to); // Clone.tt Line: 110
                        GeneratorSettings.Update(p, (GeneratorSettings)tt, isDeep);
                        to.ListGeneratorsSettings.Add(p);
                    }
                }
            }
        }
        // Clone.tt Line: 140
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
        public static Report ConvertToVM(Proto.Config.proto_report m, Report vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.SortingValue = m.SortingValue; // Clone.tt Line: 211
            vm.NameUi = m.NameUi; // Clone.tt Line: 211
            vm.Description = m.Description; // Clone.tt Line: 211
            vm.ListGeneratorsSettings = new ConfigNodesCollection<GeneratorSettings>(vm); // Clone.tt Line: 190
            foreach (var t in m.ListGeneratorsSettings) // Clone.tt Line: 191
            {
                var tvm = GeneratorSettings.ConvertToVM(t, new GeneratorSettings(vm)); // Clone.tt Line: 194
                vm.ListGeneratorsSettings.Add(tvm);
            }
            vm.OnInitFromDto(); // Clone.tt Line: 217
            return vm;
        }
        // Conversion from 'Report' to 'proto_report'
        public static Proto.Config.proto_report ConvertToProto(Report vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_report m = new Proto.Config.proto_report(); // Clone.tt Line: 224
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.SortingValue = vm.SortingValue; // Clone.tt Line: 261
            m.NameUi = vm.NameUi; // Clone.tt Line: 261
            m.Description = vm.Description; // Clone.tt Line: 261
            foreach (var t in vm.ListGeneratorsSettings) // Clone.tt Line: 227
                m.ListGeneratorsSettings.Add(GeneratorSettings.ConvertToProto((GeneratorSettings)t)); // Clone.tt Line: 231
            return m;
        }
        
        public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
        {
            if (visitor.Token.IsCancellationRequested)
            {
                return;
            }
            visitor.Visit(this);
            foreach (var t in this.ListGeneratorsSettings)
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
                    this.OnDescriptionChanging(this._Description, value);
                    this._Description = value;
                    this.OnDescriptionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string _Description = string.Empty;
        partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 156
        partial void OnDescriptionChanged();
        
        
        ///////////////////////////////////////////////////
        /// repeated proto_group_properties list_properties = 6;
        /// repeated proto_document list_documents = 7;
        ///////////////////////////////////////////////////
        public ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings // Property.tt Line: 59
        { 
            get 
            { 
                return this._ListGeneratorsSettings; 
            }
            set
            {
                if (this._ListGeneratorsSettings != value)
                {
                    this.OnListGeneratorsSettingsChanging(this._ListGeneratorsSettings, value);
                    this._ListGeneratorsSettings = value;
                    this.OnListGeneratorsSettingsChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private ConfigNodesCollection<GeneratorSettings> _ListGeneratorsSettings;
        partial void OnListGeneratorsSettingsChanging(SortedObservableCollection<GeneratorSettings> from, SortedObservableCollection<GeneratorSettings> to); // Property.tt Line: 80
        partial void OnListGeneratorsSettingsChanged();
        [BrowsableAttribute(false)]
        public IEnumerable<IGeneratorSettings> IListGeneratorsSettings { get { foreach (var t in this._ListGeneratorsSettings) yield return t; } }
    
        #endregion Properties
    }
    public partial class ModelRow : ViewModelBindable<ModelRow>, IModelRow // Class.tt Line: 6
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
            vm.GroupName = from.GroupName; // Clone.tt Line: 62
            vm.Name = from.Name; // Clone.tt Line: 62
            vm.Guid = from.Guid; // Clone.tt Line: 62
            vm.IsIncluded = from.IsIncluded; // Clone.tt Line: 62
            return vm;
        }
        public static void Update(ModelRow to, ModelRow from, bool isDeep = true) // Clone.tt Line: 72
        {
            to.GroupName = from.GroupName; // Clone.tt Line: 134
            to.Name = from.Name; // Clone.tt Line: 134
            to.Guid = from.Guid; // Clone.tt Line: 134
            to.IsIncluded = from.IsIncluded; // Clone.tt Line: 134
        }
        // Conversion from 'proto_model_row' to 'ModelRow'
        public static ModelRow ConvertToVM(Proto.Config.proto_model_row m, ModelRow vm) // Clone.tt Line: 163
        {
            if (m == null)
            {
                return vm;
            }
            vm.GroupName = m.GroupName; // Clone.tt Line: 211
            vm.Name = m.Name; // Clone.tt Line: 211
            vm.Guid = m.Guid; // Clone.tt Line: 211
            vm.IsIncluded = m.IsIncluded; // Clone.tt Line: 211
            return vm;
        }
        // Conversion from 'ModelRow' to 'proto_model_row'
        public static Proto.Config.proto_model_row ConvertToProto(ModelRow vm) // Clone.tt Line: 222
        {
            Proto.Config.proto_model_row m = new Proto.Config.proto_model_row(); // Clone.tt Line: 224
            m.GroupName = vm.GroupName; // Clone.tt Line: 261
            m.Name = vm.Name; // Clone.tt Line: 261
            m.Guid = vm.Guid; // Clone.tt Line: 261
            m.IsIncluded = vm.IsIncluded; // Clone.tt Line: 261
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
                    this.OnGroupNameChanging(this._GroupName, value);
                    this._GroupName = value;
                    this.OnGroupNameChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private string _GroupName = string.Empty;
        partial void OnGroupNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnGroupNameChanged();
        
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
                    this.OnNameChanging(this._Name, value);
                    this._Name = value;
                    this.OnNameChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private string _Name = string.Empty;
        partial void OnNameChanging(string from, string to); // Property.tt Line: 156
        partial void OnNameChanged();
        
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
                    this.OnGuidChanging(this._Guid, value);
                    this._Guid = value;
                    this.OnGuidChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private string _Guid = string.Empty;
        partial void OnGuidChanging(string from, string to); // Property.tt Line: 156
        partial void OnGuidChanged();
        
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
                    this.OnIsIncludedChanging(this._IsIncluded, value);
                    this._IsIncluded = value;
                    this.OnIsIncludedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsIncluded;
        partial void OnIsIncludedChanging(bool from, bool to); // Property.tt Line: 156
        partial void OnIsIncludedChanged();
    
        #endregion Properties
    }
    
    public interface IVisitorProto // IVisitorProto.tt Line: 7
    {
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
        void Visit(Proto.Config.proto_app_project_generator p);
        void Visit(Proto.Config.proto_generator_settings p);
        void Visit(Proto.Config.proto_type_settings p);
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
            ValidateSubAndCollectErrors(p, p.DefaultDb); // ValidationVisitor.tt Line: 30
        }
        protected override void OnVisitEnd(GroupListAppSolutions p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppSolution p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DefaultDb); // ValidationVisitor.tt Line: 30
        }
        protected override void OnVisitEnd(AppSolution p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(AppProject p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
            ValidateSubAndCollectErrors(p, p.DefaultDb); // ValidationVisitor.tt Line: 30
        }
        protected override void OnVisitEnd(AppProject p) // ValidationVisitor.tt Line: 47
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
        protected override void OnVisit(GeneratorSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(GeneratorSettings p) // ValidationVisitor.tt Line: 47
        {
            this.OnVisitEnd(p as IValidatableWithSeverity);
        }
        protected override void OnVisit(TypeSettings p) // ValidationVisitor.tt Line: 15
        {
            this.OnVisit(p as IValidatableWithSeverity);
        }
        protected override void OnVisitEnd(TypeSettings p) // ValidationVisitor.tt Line: 47
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
        public void Visit(GeneratorSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(GeneratorSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(GeneratorSettings p) { }
        protected virtual void OnVisitEnd(GeneratorSettings p) { }
        public void Visit(TypeSettings p)
        {
            this.OnVisit(p);
        }
        public void VisitEnd(TypeSettings p)
        {
            this.OnVisitEnd(p);
        }
        protected virtual void OnVisit(TypeSettings p) { }
        protected virtual void OnVisitEnd(TypeSettings p) { }
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
        void Visit(SettingsConfig p);
        void VisitEnd(SettingsConfig p);
        void Visit(ConfigShortHistory p);
        void VisitEnd(ConfigShortHistory p);
        void Visit(GroupListBaseConfigLinks p);
        void VisitEnd(GroupListBaseConfigLinks p);
        void Visit(BaseConfigLink p);
        void VisitEnd(BaseConfigLink p);
        void Visit(Config p);
        void VisitEnd(Config p);
        void Visit(GroupListAppSolutions p);
        void VisitEnd(GroupListAppSolutions p);
        void Visit(AppSolution p);
        void VisitEnd(AppSolution p);
        void Visit(AppProject p);
        void VisitEnd(AppProject p);
        void Visit(AppProjectGenerator p);
        void VisitEnd(AppProjectGenerator p);
        void Visit(GeneratorSettings p);
        void VisitEnd(GeneratorSettings p);
        void Visit(TypeSettings p);
        void VisitEnd(TypeSettings p);
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