// Auto generated on UTC 11/03/2019 19:55:58
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
	public partial class GroupListPlugins : ConfigObjectBase<GroupListPlugins, GroupListPlugins.GroupListPluginsValidator>, IComparable<GroupListPlugins>, IConfigAcceptVisitor, IGroupListPlugins // Class.tt Line: 6
	{
		public partial class GroupListPluginsValidator : ValidatorBase<GroupListPlugins, GroupListPluginsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListPlugins(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListPlugins() : base(GroupListPluginsValidator.Validator)
		public GroupListPlugins(ITreeConfigNode parent) : base(parent, GroupListPluginsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListPlugins = new ConfigNodesCollection<Plugin>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListPlugins) // Clone.tt Line: 49
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
		        foreach(var t in to.ListPlugins.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPlugins)
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
		        foreach(var tt in from.ListPlugins)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListPlugins.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return GroupListPlugins.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListPlugins from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListPlugins.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_plugins' to 'GroupListPlugins'
		public static GroupListPlugins ConvertToVM(Proto.Config.proto_group_list_plugins m, GroupListPlugins vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.ListPlugins = new ConfigNodesCollection<Plugin>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListPlugins) // Clone.tt Line: 189
		    {
		        var tvm = Plugin.ConvertToVM(t, new Plugin(vm)); // Clone.tt Line: 192
		        vm.ListPlugins.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListPlugins' to 'proto_group_list_plugins'
		public static Proto.Config.proto_group_list_plugins ConvertToProto(GroupListPlugins vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_plugins m = new Proto.Config.proto_group_list_plugins(); // Clone.tt Line: 228
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    foreach(var t in vm.ListPlugins) // Clone.tt Line: 231
		        m.ListPlugins.Add(Plugin.ConvertToProto((Plugin)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPlugins)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Plugin> ListPlugins  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListPlugins != value)
				{
					OnListPluginsChanging(_ListPlugins, value);
					_ListPlugins = value;
					OnListPluginsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPlugins; }
		}
		private ConfigNodesCollection<Plugin> _ListPlugins;
		partial void OnListPluginsChanging(SortedObservableCollection<Plugin> from, SortedObservableCollection<Plugin> to); // Property.tt Line: 71
		partial void OnListPluginsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IPlugin> IListPlugins { get { foreach (var t in _ListPlugins) yield return t; } }
		public Plugin this[int index] { get { return (Plugin)this.ListPlugins[index]; } }
		public void Add(Plugin item)  // Property.tt Line: 78
		{ 
		    this.ListPlugins.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Plugin> items) 
		{ 
		    this.ListPlugins.AddRange(items); 
		    foreach(var t in items)
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
	public partial class Plugin : ConfigObjectBase<Plugin, Plugin.PluginValidator>, IComparable<Plugin>, IConfigAcceptVisitor, IPlugin // Class.tt Line: 6
	{
		public partial class PluginValidator : ValidatorBase<Plugin, PluginValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Plugin(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Plugin() : base(PluginValidator.Validator)
		public Plugin(ITreeConfigNode parent) : base(parent, PluginValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListGenerators = new ConfigNodesCollection<PluginGenerator>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListGenerators) // Clone.tt Line: 49
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
		        foreach(var t in to.ListGenerators.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListGenerators)
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
		        foreach(var tt in from.ListGenerators)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListGenerators.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return Plugin.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Plugin from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Plugin.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_plugin' to 'Plugin'
		public static Plugin ConvertToVM(Proto.Config.proto_plugin m, Plugin vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Version = m.Version; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.ListGenerators = new ConfigNodesCollection<PluginGenerator>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListGenerators) // Clone.tt Line: 189
		    {
		        var tvm = PluginGenerator.ConvertToVM(t, new PluginGenerator(vm)); // Clone.tt Line: 192
		        vm.ListGenerators.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Plugin' to 'proto_plugin'
		public static Proto.Config.proto_plugin ConvertToProto(Plugin vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_plugin m = new Proto.Config.proto_plugin(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Version = vm.Version; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    foreach(var t in vm.ListGenerators) // Clone.tt Line: 231
		        m.ListGenerators.Add(PluginGenerator.ConvertToProto((PluginGenerator)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListGenerators)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[Editable(false)]
		public string Version // Property.tt Line: 123
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging(_Version, value);
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private string _Version = "";
		partial void OnVersionChanging(string from, string to); // Property.tt Line: 141
		partial void OnVersionChanged();
		
		[Editable(false)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<PluginGenerator> ListGenerators  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListGenerators != value)
				{
					OnListGeneratorsChanging(_ListGenerators, value);
					_ListGenerators = value;
					OnListGeneratorsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListGenerators; }
		}
		private ConfigNodesCollection<PluginGenerator> _ListGenerators;
		partial void OnListGeneratorsChanging(SortedObservableCollection<PluginGenerator> from, SortedObservableCollection<PluginGenerator> to); // Property.tt Line: 71
		partial void OnListGeneratorsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IPluginGenerator> IListGenerators { get { foreach (var t in _ListGenerators) yield return t; } }
	
		#endregion Properties
	}
	public partial class PluginGenerator : ConfigObjectBase<PluginGenerator, PluginGenerator.PluginGeneratorValidator>, IComparable<PluginGenerator>, IConfigAcceptVisitor, IPluginGenerator // Class.tt Line: 6
	{
		public partial class PluginGeneratorValidator : ValidatorBase<PluginGenerator, PluginGeneratorValidator> { } // Class.tt Line: 8
		#region CTOR
		//public PluginGenerator(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public PluginGenerator() : base(PluginGeneratorValidator.Validator)
		public PluginGenerator(ITreeConfigNode parent) : base(parent, PluginGeneratorValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListSettings = new ConfigNodesCollection<PluginGeneratorSettings>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListSettings) // Clone.tt Line: 49
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
		        foreach(var t in to.ListSettings.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListSettings)
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
		        foreach(var tt in from.ListSettings)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListSettings.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return PluginGenerator.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(PluginGenerator from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    PluginGenerator.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_plugin_generator' to 'PluginGenerator'
		public static PluginGenerator ConvertToVM(Proto.Config.proto_plugin_generator m, PluginGenerator vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.ListSettings = new ConfigNodesCollection<PluginGeneratorSettings>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListSettings) // Clone.tt Line: 189
		    {
		        var tvm = PluginGeneratorSettings.ConvertToVM(t, new PluginGeneratorSettings(vm)); // Clone.tt Line: 192
		        vm.ListSettings.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'PluginGenerator' to 'proto_plugin_generator'
		public static Proto.Config.proto_plugin_generator ConvertToProto(PluginGenerator vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_plugin_generator m = new Proto.Config.proto_plugin_generator(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    foreach(var t in vm.ListSettings) // Clone.tt Line: 231
		        m.ListSettings.Add(PluginGeneratorSettings.ConvertToProto((PluginGeneratorSettings)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListSettings)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[Editable(false)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<PluginGeneratorSettings> ListSettings  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListSettings != value)
				{
					OnListSettingsChanging(_ListSettings, value);
					_ListSettings = value;
					OnListSettingsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListSettings; }
		}
		private ConfigNodesCollection<PluginGeneratorSettings> _ListSettings;
		partial void OnListSettingsChanging(SortedObservableCollection<PluginGeneratorSettings> from, SortedObservableCollection<PluginGeneratorSettings> to); // Property.tt Line: 71
		partial void OnListSettingsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IPluginGeneratorSettings> IListSettings { get { foreach (var t in _ListSettings) yield return t; } }
	
		#endregion Properties
	}
	public partial class PluginGeneratorSettings : ConfigObjectBase<PluginGeneratorSettings, PluginGeneratorSettings.PluginGeneratorSettingsValidator>, IComparable<PluginGeneratorSettings>, IConfigAcceptVisitor, IPluginGeneratorSettings // Class.tt Line: 6
	{
		public partial class PluginGeneratorSettingsValidator : ValidatorBase<PluginGeneratorSettings, PluginGeneratorSettingsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public PluginGeneratorSettings(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public PluginGeneratorSettings() : base(PluginGeneratorSettingsValidator.Validator)
		public PluginGeneratorSettings(ITreeConfigNode parent) : base(parent, PluginGeneratorSettingsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return PluginGeneratorSettings.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(PluginGeneratorSettings from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    PluginGeneratorSettings.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_plugin_generator_settings' to 'PluginGeneratorSettings'
		public static PluginGeneratorSettings ConvertToVM(Proto.Config.proto_plugin_generator_settings m, PluginGeneratorSettings vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 216
		    vm.IsPrivate = m.IsPrivate; // Clone.tt Line: 216
		    vm.FilePath = m.FilePath; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'PluginGeneratorSettings' to 'proto_plugin_generator_settings'
		public static Proto.Config.proto_plugin_generator_settings ConvertToProto(PluginGeneratorSettings vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_plugin_generator_settings m = new Proto.Config.proto_plugin_generator_settings(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 252
		    m.IsPrivate = vm.IsPrivate; // Clone.tt Line: 252
		    m.FilePath = vm.FilePath; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		///////////////////////////////////////////////////
		/// This Description is taken from Plugin Generator
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(2)]
		[Editable(false)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public string GeneratorSettings // Property.tt Line: 123
		{ 
			set
			{
				if (_GeneratorSettings != value)
				{
					OnGeneratorSettingsChanging(_GeneratorSettings, value);
					_GeneratorSettings = value;
					OnGeneratorSettingsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GeneratorSettings; }
		}
		private string _GeneratorSettings = "";
		partial void OnGeneratorSettingsChanging(string from, string to); // Property.tt Line: 141
		partial void OnGeneratorSettingsChanged();
		
		[PropertyOrderAttribute(3)]
		[Description("If false, connection string settings will be stored in configuration file. If true, will be stored in separate file.")]
		public bool IsPrivate // Property.tt Line: 123
		{ 
			set
			{
				if (_IsPrivate != value)
				{
					OnIsPrivateChanging(_IsPrivate, value);
					_IsPrivate = value;
					OnIsPrivateChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPrivate; }
		}
		private bool _IsPrivate;
		partial void OnIsPrivateChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsPrivateChanged();
		
		[PropertyOrderAttribute(4)]
		[Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
		[Description("File path to store connection string settings in private place.")]
		public string FilePath // Property.tt Line: 123
		{ 
			set
			{
				if (_FilePath != value)
				{
					OnFilePathChanging(_FilePath, value);
					_FilePath = value;
					OnFilePathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _FilePath; }
		}
		private string _FilePath = "";
		partial void OnFilePathChanging(string from, string to); // Property.tt Line: 141
		partial void OnFilePathChanged();
	
		#endregion Properties
	}
	public partial class SettingsConfig : ConfigObjectBase<SettingsConfig, SettingsConfig.SettingsConfigValidator>, IComparable<SettingsConfig>, IConfigAcceptVisitor, ISettingsConfig // Class.tt Line: 6
	{
		public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { } // Class.tt Line: 8
		#region CTOR
		//public SettingsConfig(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public SettingsConfig() : base(SettingsConfigValidator.Validator)
		public SettingsConfig(ITreeConfigNode parent) : base(parent, SettingsConfigValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return SettingsConfig.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(SettingsConfig from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    SettingsConfig.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_settings_config' to 'SettingsConfig'
		public static SettingsConfig ConvertToVM(Proto.Config.proto_settings_config m, SettingsConfig vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.VersionMigrationCurrent = m.VersionMigrationCurrent; // Clone.tt Line: 216
		    vm.VersionMigrationSupportFromMin = m.VersionMigrationSupportFromMin; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'SettingsConfig' to 'proto_settings_config'
		public static Proto.Config.proto_settings_config ConvertToProto(SettingsConfig vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_settings_config m = new Proto.Config.proto_settings_config(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.VersionMigrationCurrent = vm.VersionMigrationCurrent; // Clone.tt Line: 252
		    m.VersionMigrationSupportFromMin = vm.VersionMigrationSupportFromMin; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// current migration version, increased by one on each deployment
		///////////////////////////////////////////////////
		public int VersionMigrationCurrent // Property.tt Line: 123
		{ 
			set
			{
				if (_VersionMigrationCurrent != value)
				{
					OnVersionMigrationCurrentChanging(_VersionMigrationCurrent, value);
					_VersionMigrationCurrent = value;
					OnVersionMigrationCurrentChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _VersionMigrationCurrent; }
		}
		private int _VersionMigrationCurrent;
		partial void OnVersionMigrationCurrentChanging(int from, int to); // Property.tt Line: 141
		partial void OnVersionMigrationCurrentChanged();
		
		
		///////////////////////////////////////////////////
		/// min version supported by current version for migration
		///////////////////////////////////////////////////
		public int VersionMigrationSupportFromMin // Property.tt Line: 123
		{ 
			set
			{
				if (_VersionMigrationSupportFromMin != value)
				{
					OnVersionMigrationSupportFromMinChanging(_VersionMigrationSupportFromMin, value);
					_VersionMigrationSupportFromMin = value;
					OnVersionMigrationSupportFromMinChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _VersionMigrationSupportFromMin; }
		}
		private int _VersionMigrationSupportFromMin;
		partial void OnVersionMigrationSupportFromMinChanging(int from, int to); // Property.tt Line: 141
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
		public DbSettings() : base(DbSettingsValidator.Validator) // Class.tt Line: 38
		{
			OnInitBegin();
			OnInit();
		}
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DbSettings Clone(ITreeConfigNode parent, DbSettings from, bool isDeep = true) // Clone.tt Line: 27
		{
		    DbSettings vm = new DbSettings();
		    vm.DbSchema = from.DbSchema; // Clone.tt Line: 62
		    vm.IdGenerator = from.IdGenerator; // Clone.tt Line: 62
		    vm.KeyType = from.KeyType; // Clone.tt Line: 62
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
		    to.KeyType = from.KeyType; // Clone.tt Line: 134
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
		    OnBackupObjectStarting(ref isDeep);
			return DbSettings.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(DbSettings from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    DbSettings.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'db_settings' to 'DbSettings'
		public static DbSettings ConvertToVM(Proto.Config.db_settings m, DbSettings vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.DbSchema = m.DbSchema; // Clone.tt Line: 216
		    vm.IdGenerator = (DbIdGeneratorMethod)m.IdGenerator; // Clone.tt Line: 214
		    vm.KeyType = m.KeyType; // Clone.tt Line: 216
		    vm.KeyName = m.KeyName; // Clone.tt Line: 216
		    vm.Timestamp = m.Timestamp; // Clone.tt Line: 216
		    vm.IsDbFromConnectionString = m.IsDbFromConnectionString; // Clone.tt Line: 216
		    vm.ConnectionStringName = m.ConnectionStringName; // Clone.tt Line: 216
		    vm.PathToProjectWithConnectionString = m.PathToProjectWithConnectionString; // Clone.tt Line: 216
		    return vm;
		}
		// Conversion from 'DbSettings' to 'db_settings'
		public static Proto.Config.db_settings ConvertToProto(DbSettings vm) // Clone.tt Line: 226
		{
		    Proto.Config.db_settings m = new Proto.Config.db_settings(); // Clone.tt Line: 228
		    m.DbSchema = vm.DbSchema; // Clone.tt Line: 252
		    m.IdGenerator = (Proto.Config.db_id_generator_method)vm.IdGenerator; // Clone.tt Line: 250
		    m.KeyType = vm.KeyType; // Clone.tt Line: 252
		    m.KeyName = vm.KeyName; // Clone.tt Line: 252
		    m.Timestamp = vm.Timestamp; // Clone.tt Line: 252
		    m.IsDbFromConnectionString = vm.IsDbFromConnectionString; // Clone.tt Line: 252
		    m.ConnectionStringName = vm.ConnectionStringName; // Clone.tt Line: 252
		    m.PathToProjectWithConnectionString = vm.PathToProjectWithConnectionString; // Clone.tt Line: 252
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[Description("DB schema name for all object in this configuration")]
		public string DbSchema // Property.tt Line: 123
		{ 
			set
			{
				if (_DbSchema != value)
				{
					OnDbSchemaChanging(_DbSchema, value);
					_DbSchema = value;
					OnDbSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbSchema; }
		}
		private string _DbSchema = "";
		partial void OnDbSchemaChanging(string from, string to); // Property.tt Line: 141
		partial void OnDbSchemaChanged();
		
		[PropertyOrderAttribute(2)]
		[Description("Primary key generation method")]
		public DbIdGeneratorMethod IdGenerator // Property.tt Line: 123
		{ 
			set
			{
				if (_IdGenerator != value)
				{
					OnIdGeneratorChanging(_IdGenerator, value);
					_IdGenerator = value;
					OnIdGeneratorChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IdGenerator; }
		}
		private DbIdGeneratorMethod _IdGenerator;
		partial void OnIdGeneratorChanging(DbIdGeneratorMethod from, DbIdGeneratorMethod to); // Property.tt Line: 141
		partial void OnIdGeneratorChanged();
		
		[PropertyOrderAttribute(3)]
		[Description("Primary key field type")]
		public string KeyType // Property.tt Line: 123
		{ 
			set
			{
				if (_KeyType != value)
				{
					OnKeyTypeChanging(_KeyType, value);
					_KeyType = value;
					OnKeyTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _KeyType; }
		}
		private string _KeyType = "";
		partial void OnKeyTypeChanging(string from, string to); // Property.tt Line: 141
		partial void OnKeyTypeChanged();
		
		[PropertyOrderAttribute(4)]
		[Description("Primary key field name")]
		public string KeyName // Property.tt Line: 123
		{ 
			set
			{
				if (_KeyName != value)
				{
					OnKeyNameChanging(_KeyName, value);
					_KeyName = value;
					OnKeyNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _KeyName; }
		}
		private string _KeyName = "";
		partial void OnKeyNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnKeyNameChanged();
		
		[PropertyOrderAttribute(5)]
		[Description("Record data version/timestamp field name")]
		public string Timestamp // Property.tt Line: 123
		{ 
			set
			{
				if (_Timestamp != value)
				{
					OnTimestampChanging(_Timestamp, value);
					_Timestamp = value;
					OnTimestampChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Timestamp; }
		}
		private string _Timestamp = "";
		partial void OnTimestampChanging(string from, string to); // Property.tt Line: 141
		partial void OnTimestampChanged();
		
		
		///////////////////////////////////////////////////
		/// if yes: 
		///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
		/// if no:
		///    1. Find DB type from 
		///    2. Create connection string from db_server, db_database_name, db_user
		///////////////////////////////////////////////////
		public bool IsDbFromConnectionString // Property.tt Line: 123
		{ 
			set
			{
				if (_IsDbFromConnectionString != value)
				{
					OnIsDbFromConnectionStringChanging(_IsDbFromConnectionString, value);
					_IsDbFromConnectionString = value;
					OnIsDbFromConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsDbFromConnectionString; }
		}
		private bool _IsDbFromConnectionString;
		partial void OnIsDbFromConnectionStringChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsDbFromConnectionStringChanged();
		
		public string ConnectionStringName // Property.tt Line: 123
		{ 
			set
			{
				if (_ConnectionStringName != value)
				{
					OnConnectionStringNameChanging(_ConnectionStringName, value);
					_ConnectionStringName = value;
					OnConnectionStringNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectionStringName; }
		}
		private string _ConnectionStringName = "";
		partial void OnConnectionStringNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnConnectionStringNameChanged();
		
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(4)]
		[Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
		[Description("File path to store connection string settings in private place.")]
		public string PathToProjectWithConnectionString // Property.tt Line: 123
		{ 
			set
			{
				if (_PathToProjectWithConnectionString != value)
				{
					OnPathToProjectWithConnectionStringChanging(_PathToProjectWithConnectionString, value);
					_PathToProjectWithConnectionString = value;
					OnPathToProjectWithConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PathToProjectWithConnectionString; }
		}
		private string _PathToProjectWithConnectionString = "";
		partial void OnPathToProjectWithConnectionStringChanging(string from, string to); // Property.tt Line: 141
		partial void OnPathToProjectWithConnectionStringChanged();
	
		#endregion Properties
	}
	public partial class ConfigShortHistory : ConfigObjectBase<ConfigShortHistory, ConfigShortHistory.ConfigShortHistoryValidator>, IComparable<ConfigShortHistory>, IConfigAcceptVisitor, IConfigShortHistory // Class.tt Line: 6
	{
		public partial class ConfigShortHistoryValidator : ValidatorBase<ConfigShortHistory, ConfigShortHistoryValidator> { } // Class.tt Line: 8
		#region CTOR
		//public ConfigShortHistory(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public ConfigShortHistory() : base(ConfigShortHistoryValidator.Validator)
		public ConfigShortHistory(ITreeConfigNode parent) : base(parent, ConfigShortHistoryValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.CurrentConfig = new Config(this); // Class.tt Line: 29
			this.PrevStableConfig = new Config(this); // Class.tt Line: 29
			this.OldStableConfig = new Config(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return ConfigShortHistory.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(ConfigShortHistory from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    ConfigShortHistory.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_config_short_history' to 'ConfigShortHistory'
		public static ConfigShortHistory ConvertToVM(Proto.Config.proto_config_short_history m, ConfigShortHistory vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    if (vm.CurrentConfig == null) // Clone.tt Line: 204
		        vm.CurrentConfig = new Config(vm); // Clone.tt Line: 206
		    Config.ConvertToVM(m.CurrentConfig, vm.CurrentConfig);
		    if (vm.PrevStableConfig == null) // Clone.tt Line: 204
		        vm.PrevStableConfig = new Config(vm); // Clone.tt Line: 206
		    Config.ConvertToVM(m.PrevStableConfig, vm.PrevStableConfig);
		    if (vm.OldStableConfig == null) // Clone.tt Line: 204
		        vm.OldStableConfig = new Config(vm); // Clone.tt Line: 206
		    Config.ConvertToVM(m.OldStableConfig, vm.OldStableConfig);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'ConfigShortHistory' to 'proto_config_short_history'
		public static Proto.Config.proto_config_short_history ConvertToProto(ConfigShortHistory vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_config_short_history m = new Proto.Config.proto_config_short_history(); // Clone.tt Line: 228
		    m.CurrentConfig = Config.ConvertToProto(vm.CurrentConfig); // Clone.tt Line: 246
		    m.PrevStableConfig = Config.ConvertToProto(vm.PrevStableConfig); // Clone.tt Line: 246
		    m.OldStableConfig = Config.ConvertToProto(vm.OldStableConfig); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.CurrentConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.PrevStableConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.OldStableConfig.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public Config CurrentConfig // Property.tt Line: 100
		{ 
			set
			{
				if (_CurrentConfig != value)
				{
					OnCurrentConfigChanging(_CurrentConfig, value);
		            _CurrentConfig = value;
					OnCurrentConfigChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _CurrentConfig; }
		}
		private Config _CurrentConfig;
		partial void OnCurrentConfigChanging(Config from, Config to); // Property.tt Line: 118
		partial void OnCurrentConfigChanged();
		[BrowsableAttribute(false)]
		public IConfig ICurrentConfig { get { return _CurrentConfig; }}
		
		public Config PrevStableConfig // Property.tt Line: 100
		{ 
			set
			{
				if (_PrevStableConfig != value)
				{
					OnPrevStableConfigChanging(_PrevStableConfig, value);
		            _PrevStableConfig = value;
					OnPrevStableConfigChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PrevStableConfig; }
		}
		private Config _PrevStableConfig;
		partial void OnPrevStableConfigChanging(Config from, Config to); // Property.tt Line: 118
		partial void OnPrevStableConfigChanged();
		[BrowsableAttribute(false)]
		public IConfig IPrevStableConfig { get { return _PrevStableConfig; }}
		
		public Config OldStableConfig // Property.tt Line: 100
		{ 
			set
			{
				if (_OldStableConfig != value)
				{
					OnOldStableConfigChanging(_OldStableConfig, value);
		            _OldStableConfig = value;
					OnOldStableConfigChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _OldStableConfig; }
		}
		private Config _OldStableConfig;
		partial void OnOldStableConfigChanging(Config from, Config to); // Property.tt Line: 118
		partial void OnOldStableConfigChanged();
		[BrowsableAttribute(false)]
		public IConfig IOldStableConfig { get { return _OldStableConfig; }}
	
		#endregion Properties
	}
	public partial class GroupListBaseConfigLinks : ConfigObjectBase<GroupListBaseConfigLinks, GroupListBaseConfigLinks.GroupListBaseConfigLinksValidator>, IComparable<GroupListBaseConfigLinks>, IConfigAcceptVisitor, IGroupListBaseConfigLinks // Class.tt Line: 6
	{
		public partial class GroupListBaseConfigLinksValidator : ValidatorBase<GroupListBaseConfigLinks, GroupListBaseConfigLinksValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListBaseConfigLinks(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListBaseConfigLinks() : base(GroupListBaseConfigLinksValidator.Validator)
		public GroupListBaseConfigLinks(ITreeConfigNode parent) : base(parent, GroupListBaseConfigLinksValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListBaseConfigLinks) // Clone.tt Line: 49
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
		        foreach(var t in to.ListBaseConfigLinks.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListBaseConfigLinks)
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
		        foreach(var tt in from.ListBaseConfigLinks)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListBaseConfigLinks.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return GroupListBaseConfigLinks.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListBaseConfigLinks from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListBaseConfigLinks.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_base_config_links' to 'GroupListBaseConfigLinks'
		public static GroupListBaseConfigLinks ConvertToVM(Proto.Config.proto_group_list_base_config_links m, GroupListBaseConfigLinks vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListBaseConfigLinks = new ConfigNodesCollection<BaseConfigLink>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListBaseConfigLinks) // Clone.tt Line: 189
		    {
		        var tvm = BaseConfigLink.ConvertToVM(t, new BaseConfigLink(vm)); // Clone.tt Line: 192
		        vm.ListBaseConfigLinks.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListBaseConfigLinks' to 'proto_group_list_base_config_links'
		public static Proto.Config.proto_group_list_base_config_links ConvertToProto(GroupListBaseConfigLinks vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_base_config_links m = new Proto.Config.proto_group_list_base_config_links(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListBaseConfigLinks) // Clone.tt Line: 231
		        m.ListBaseConfigLinks.Add(BaseConfigLink.ConvertToProto((BaseConfigLink)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListBaseConfigLinks)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<BaseConfigLink> ListBaseConfigLinks  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListBaseConfigLinks != value)
				{
					OnListBaseConfigLinksChanging(_ListBaseConfigLinks, value);
					_ListBaseConfigLinks = value;
					OnListBaseConfigLinksChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListBaseConfigLinks; }
		}
		private ConfigNodesCollection<BaseConfigLink> _ListBaseConfigLinks;
		partial void OnListBaseConfigLinksChanging(SortedObservableCollection<BaseConfigLink> from, SortedObservableCollection<BaseConfigLink> to); // Property.tt Line: 71
		partial void OnListBaseConfigLinksChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IBaseConfigLink> IListBaseConfigLinks { get { foreach (var t in _ListBaseConfigLinks) yield return t; } }
		public BaseConfigLink this[int index] { get { return (BaseConfigLink)this.ListBaseConfigLinks[index]; } }
		public void Add(BaseConfigLink item)  // Property.tt Line: 78
		{ 
		    this.ListBaseConfigLinks.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<BaseConfigLink> items) 
		{ 
		    this.ListBaseConfigLinks.AddRange(items); 
		    foreach(var t in items)
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
	public partial class BaseConfigLink : ConfigObjectBase<BaseConfigLink, BaseConfigLink.BaseConfigLinkValidator>, IComparable<BaseConfigLink>, IConfigAcceptVisitor, IBaseConfigLink // Class.tt Line: 6
	{
		public partial class BaseConfigLinkValidator : ValidatorBase<BaseConfigLink, BaseConfigLinkValidator> { } // Class.tt Line: 8
		#region CTOR
		//public BaseConfigLink(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public BaseConfigLink() : base(BaseConfigLinkValidator.Validator)
		public BaseConfigLink(ITreeConfigNode parent) : base(parent, BaseConfigLinkValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.Config = new Config(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return BaseConfigLink.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(BaseConfigLink from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    BaseConfigLink.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_base_config_link' to 'BaseConfigLink'
		public static BaseConfigLink ConvertToVM(Proto.Config.proto_base_config_link m, BaseConfigLink vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.Config == null) // Clone.tt Line: 204
		        vm.Config = new Config(vm); // Clone.tt Line: 206
		    Config.ConvertToVM(m.Config, vm.Config);
		    vm.RelativeConfigFilePath = m.RelativeConfigFilePath; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'BaseConfigLink' to 'proto_base_config_link'
		public static Proto.Config.proto_base_config_link ConvertToProto(BaseConfigLink vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_base_config_link m = new Proto.Config.proto_base_config_link(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.Config = Config.ConvertToProto(vm.Config); // Clone.tt Line: 246
		    m.RelativeConfigFilePath = vm.RelativeConfigFilePath; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Config.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public Config Config // Property.tt Line: 100
		{ 
			set
			{
				if (_Config != value)
				{
					OnConfigChanging(_Config, value);
		            _Config = value;
					OnConfigChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Config; }
		}
		private Config _Config;
		partial void OnConfigChanging(Config from, Config to); // Property.tt Line: 118
		partial void OnConfigChanged();
		[BrowsableAttribute(false)]
		public IConfig IConfig { get { return _Config; }}
		
		[PropertyOrderAttribute(6)]
		[Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
		public string RelativeConfigFilePath // Property.tt Line: 123
		{ 
			set
			{
				if (_RelativeConfigFilePath != value)
				{
					OnRelativeConfigFilePathChanging(_RelativeConfigFilePath, value);
					_RelativeConfigFilePath = value;
					OnRelativeConfigFilePathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeConfigFilePath; }
		}
		private string _RelativeConfigFilePath = "";
		partial void OnRelativeConfigFilePathChanging(string from, string to); // Property.tt Line: 141
		partial void OnRelativeConfigFilePathChanged();
	
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Configuration config
	///////////////////////////////////////////////////
	public partial class Config : ConfigObjectBase<Config, Config.ConfigValidator>, IComparable<Config>, IConfigAcceptVisitor, IConfig // Class.tt Line: 6
	{
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Config(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Config() : base(ConfigValidator.Validator)
		public Config(ITreeConfigNode parent) : base(parent, ConfigValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.DbSettings = new DbSettings(); // Class.tt Line: 27
			this.GroupConfigLinks = new GroupListBaseConfigLinks(this); // Class.tt Line: 29
			this.Model = new ConfigModel(this); // Class.tt Line: 29
			this.GroupPlugins = new GroupListPlugins(this); // Class.tt Line: 29
			this.GroupAppSolutions = new GroupListAppSolutions(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    vm.PrimaryKeyType = from.PrimaryKeyType; // Clone.tt Line: 62
		    if (isDeep) // Clone.tt Line: 59
		        vm.DbSettings = DbSettings.Clone(vm, from.DbSettings, isDeep);
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
		    to.PrimaryKeyType = from.PrimaryKeyType; // Clone.tt Line: 134
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
		    OnBackupObjectStarting(ref isDeep);
			return Config.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Config from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Config.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_config' to 'Config'
		public static Config ConvertToVM(Proto.Config.proto_config m, Config vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Version = m.Version; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.LastUpdated = m.LastUpdated; // Clone.tt Line: 216
		    vm.PrimaryKeyType = (EnumPrimaryKeyType)m.PrimaryKeyType; // Clone.tt Line: 214
		    if (vm.DbSettings == null) // Clone.tt Line: 204
		        vm.DbSettings = new DbSettings(); // Clone.tt Line: 208
		    DbSettings.ConvertToVM(m.DbSettings, vm.DbSettings);
		    if (vm.GroupConfigLinks == null) // Clone.tt Line: 204
		        vm.GroupConfigLinks = new GroupListBaseConfigLinks(vm); // Clone.tt Line: 206
		    GroupListBaseConfigLinks.ConvertToVM(m.GroupConfigLinks, vm.GroupConfigLinks);
		    if (vm.Model == null) // Clone.tt Line: 204
		        vm.Model = new ConfigModel(vm); // Clone.tt Line: 206
		    ConfigModel.ConvertToVM(m.Model, vm.Model);
		    if (vm.GroupPlugins == null) // Clone.tt Line: 204
		        vm.GroupPlugins = new GroupListPlugins(vm); // Clone.tt Line: 206
		    GroupListPlugins.ConvertToVM(m.GroupPlugins, vm.GroupPlugins);
		    if (vm.GroupAppSolutions == null) // Clone.tt Line: 204
		        vm.GroupAppSolutions = new GroupListAppSolutions(vm); // Clone.tt Line: 206
		    GroupListAppSolutions.ConvertToVM(m.GroupAppSolutions, vm.GroupAppSolutions);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Config' to 'proto_config'
		public static Proto.Config.proto_config ConvertToProto(Config vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_config m = new Proto.Config.proto_config(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Version = vm.Version; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.LastUpdated = vm.LastUpdated; // Clone.tt Line: 252
		    m.PrimaryKeyType = (Proto.Config.proto_enum_primary_key_type)vm.PrimaryKeyType; // Clone.tt Line: 250
		    m.DbSettings = DbSettings.ConvertToProto(vm.DbSettings); // Clone.tt Line: 246
		    m.GroupConfigLinks = GroupListBaseConfigLinks.ConvertToProto(vm.GroupConfigLinks); // Clone.tt Line: 246
		    m.Model = ConfigModel.ConvertToProto(vm.Model); // Clone.tt Line: 246
		    m.GroupPlugins = GroupListPlugins.ConvertToProto(vm.GroupPlugins); // Clone.tt Line: 246
		    m.GroupAppSolutions = GroupListAppSolutions.ConvertToProto(vm.GroupAppSolutions); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Model.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupAppSolutions.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(4)]
		[Editable(false)]
		public int Version // Property.tt Line: 123
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging(_Version, value);
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private int _Version;
		partial void OnVersionChanging(int from, int to); // Property.tt Line: 141
		partial void OnVersionChanged();
		
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[PropertyOrderAttribute(6)]
		public Google.Protobuf.WellKnownTypes.Timestamp LastUpdated // Property.tt Line: 123
		{ 
			set
			{
				if (_LastUpdated != value)
				{
					OnLastUpdatedChanging(_LastUpdated, value);
					_LastUpdated = value;
					OnLastUpdatedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _LastUpdated; }
		}
		private Google.Protobuf.WellKnownTypes.Timestamp _LastUpdated;
		partial void OnLastUpdatedChanging(Google.Protobuf.WellKnownTypes.Timestamp from, Google.Protobuf.WellKnownTypes.Timestamp to); // Property.tt Line: 141
		partial void OnLastUpdatedChanged();
		
		[PropertyOrderAttribute(7)]
		public EnumPrimaryKeyType PrimaryKeyType // Property.tt Line: 123
		{ 
			set
			{
				if (_PrimaryKeyType != value)
				{
					OnPrimaryKeyTypeChanging(_PrimaryKeyType, value);
					_PrimaryKeyType = value;
					OnPrimaryKeyTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PrimaryKeyType; }
		}
		private EnumPrimaryKeyType _PrimaryKeyType;
		partial void OnPrimaryKeyTypeChanging(EnumPrimaryKeyType from, EnumPrimaryKeyType to); // Property.tt Line: 141
		partial void OnPrimaryKeyTypeChanged();
		
		
		///////////////////////////////////////////////////
		/// GENERAL DB SETTINGS
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(11)]
		[ExpandableObjectAttribute()]
		public DbSettings DbSettings // Property.tt Line: 100
		{ 
			set
			{
				if (_DbSettings != value)
				{
					OnDbSettingsChanging(_DbSettings, value);
		            _DbSettings = value;
					OnDbSettingsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbSettings; }
		}
		private DbSettings _DbSettings;
		partial void OnDbSettingsChanging(DbSettings from, DbSettings to); // Property.tt Line: 118
		partial void OnDbSettingsChanged();
		[BrowsableAttribute(false)]
		public IDbSettings IDbSettings { get { return _DbSettings; }}
		
		[BrowsableAttribute(false)]
		public GroupListBaseConfigLinks GroupConfigLinks // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupConfigLinks != value)
				{
					OnGroupConfigLinksChanging(_GroupConfigLinks, value);
		            _GroupConfigLinks = value;
					OnGroupConfigLinksChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupConfigLinks; }
		}
		private GroupListBaseConfigLinks _GroupConfigLinks;
		partial void OnGroupConfigLinksChanging(GroupListBaseConfigLinks from, GroupListBaseConfigLinks to); // Property.tt Line: 118
		partial void OnGroupConfigLinksChanged();
		[BrowsableAttribute(false)]
		public IGroupListBaseConfigLinks IGroupConfigLinks { get { return _GroupConfigLinks; }}
		
		[BrowsableAttribute(false)]
		public ConfigModel Model // Property.tt Line: 100
		{ 
			set
			{
				if (_Model != value)
				{
					OnModelChanging(_Model, value);
		            _Model = value;
					OnModelChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Model; }
		}
		private ConfigModel _Model;
		partial void OnModelChanging(ConfigModel from, ConfigModel to); // Property.tt Line: 118
		partial void OnModelChanged();
		[BrowsableAttribute(false)]
		public IConfigModel IModel { get { return _Model; }}
		
		[BrowsableAttribute(false)]
		public GroupListPlugins GroupPlugins // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupPlugins != value)
				{
					OnGroupPluginsChanging(_GroupPlugins, value);
		            _GroupPlugins = value;
					OnGroupPluginsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPlugins; }
		}
		private GroupListPlugins _GroupPlugins;
		partial void OnGroupPluginsChanging(GroupListPlugins from, GroupListPlugins to); // Property.tt Line: 118
		partial void OnGroupPluginsChanged();
		[BrowsableAttribute(false)]
		public IGroupListPlugins IGroupPlugins { get { return _GroupPlugins; }}
		
		[BrowsableAttribute(false)]
		public GroupListAppSolutions GroupAppSolutions // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupAppSolutions != value)
				{
					OnGroupAppSolutionsChanging(_GroupAppSolutions, value);
		            _GroupAppSolutions = value;
					OnGroupAppSolutionsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupAppSolutions; }
		}
		private GroupListAppSolutions _GroupAppSolutions;
		partial void OnGroupAppSolutionsChanging(GroupListAppSolutions from, GroupListAppSolutions to); // Property.tt Line: 118
		partial void OnGroupAppSolutionsChanged();
		[BrowsableAttribute(false)]
		public IGroupListAppSolutions IGroupAppSolutions { get { return _GroupAppSolutions; }}
	
		#endregion Properties
	}
	public partial class AppDbSettings : ViewModelValidatableWithSeverity<AppDbSettings, AppDbSettings.AppDbSettingsValidator>, IAppDbSettings // Class.tt Line: 6
	{
		public partial class AppDbSettingsValidator : ValidatorBase<AppDbSettings, AppDbSettingsValidator> { } // Class.tt Line: 8
		#region CTOR
		public AppDbSettings() : base(AppDbSettingsValidator.Validator) // Class.tt Line: 38
		{
			OnInitBegin();
			OnInit();
		}
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static AppDbSettings Clone(ITreeConfigNode parent, AppDbSettings from, bool isDeep = true) // Clone.tt Line: 27
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
		    OnBackupObjectStarting(ref isDeep);
			return AppDbSettings.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(AppDbSettings from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    AppDbSettings.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_app_db_settings' to 'AppDbSettings'
		public static AppDbSettings ConvertToVM(Proto.Config.proto_app_db_settings m, AppDbSettings vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 216
		    vm.PluginName = m.PluginName; // Clone.tt Line: 216
		    vm.Version = m.Version; // Clone.tt Line: 216
		    vm.PluginGenGuid = m.PluginGenGuid; // Clone.tt Line: 216
		    vm.PluginGenName = m.PluginGenName; // Clone.tt Line: 216
		    vm.ConnGuid = m.ConnGuid; // Clone.tt Line: 216
		    vm.ConnName = m.ConnName; // Clone.tt Line: 216
		    return vm;
		}
		// Conversion from 'AppDbSettings' to 'proto_app_db_settings'
		public static Proto.Config.proto_app_db_settings ConvertToProto(AppDbSettings vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_app_db_settings m = new Proto.Config.proto_app_db_settings(); // Clone.tt Line: 228
		    m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 252
		    m.PluginName = vm.PluginName; // Clone.tt Line: 252
		    m.Version = vm.Version; // Clone.tt Line: 252
		    m.PluginGenGuid = vm.PluginGenGuid; // Clone.tt Line: 252
		    m.PluginGenName = vm.PluginGenName; // Clone.tt Line: 252
		    m.ConnGuid = vm.ConnGuid; // Clone.tt Line: 252
		    m.ConnName = vm.ConnName; // Clone.tt Line: 252
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[Editor(typeof(EditorDbPluginSelection), typeof(EditorDbPluginSelection))]
		[Description("Default DB Plugin")]
		public string PluginGuid // Property.tt Line: 123
		{ 
			set
			{
				if (_PluginGuid != value)
				{
					OnPluginGuidChanging(_PluginGuid, value);
					_PluginGuid = value;
					OnPluginGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginGuid; }
		}
		private string _PluginGuid = "";
		partial void OnPluginGuidChanging(string from, string to); // Property.tt Line: 141
		partial void OnPluginGuidChanged();
		
		[PropertyOrderAttribute(2)]
		[Editable(false)]
		public string PluginName // Property.tt Line: 123
		{ 
			set
			{
				if (_PluginName != value)
				{
					OnPluginNameChanging(_PluginName, value);
					_PluginName = value;
					OnPluginNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginName; }
		}
		private string _PluginName = "";
		partial void OnPluginNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnPluginNameChanged();
		
		[PropertyOrderAttribute(3)]
		[Editable(false)]
		public string Version // Property.tt Line: 123
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging(_Version, value);
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private string _Version = "";
		partial void OnVersionChanging(string from, string to); // Property.tt Line: 141
		partial void OnVersionChanged();
		
		[PropertyOrderAttribute(4)]
		[Editor(typeof(EditorDbPluginGenSelection), typeof(EditorDbPluginGenSelection))]
		[Description("Default DB Plugin generator")]
		public string PluginGenGuid // Property.tt Line: 123
		{ 
			set
			{
				if (_PluginGenGuid != value)
				{
					OnPluginGenGuidChanging(_PluginGenGuid, value);
					_PluginGenGuid = value;
					OnPluginGenGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginGenGuid; }
		}
		private string _PluginGenGuid = "";
		partial void OnPluginGenGuidChanging(string from, string to); // Property.tt Line: 141
		partial void OnPluginGenGuidChanged();
		
		[PropertyOrderAttribute(5)]
		[Editable(false)]
		public string PluginGenName // Property.tt Line: 123
		{ 
			set
			{
				if (_PluginGenName != value)
				{
					OnPluginGenNameChanging(_PluginGenName, value);
					_PluginGenName = value;
					OnPluginGenNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginGenName; }
		}
		private string _PluginGenName = "";
		partial void OnPluginGenNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnPluginGenNameChanged();
		
		[PropertyOrderAttribute(6)]
		[Editor(typeof(EditorDbConnSelection), typeof(EditorDbConnSelection))]
		[Description("Default DB connection string")]
		public string ConnGuid // Property.tt Line: 123
		{ 
			set
			{
				if (_ConnGuid != value)
				{
					OnConnGuidChanging(_ConnGuid, value);
					_ConnGuid = value;
					OnConnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnGuid; }
		}
		private string _ConnGuid = "";
		partial void OnConnGuidChanging(string from, string to); // Property.tt Line: 141
		partial void OnConnGuidChanged();
		
		[PropertyOrderAttribute(7)]
		[Editable(false)]
		public string ConnName // Property.tt Line: 123
		{ 
			set
			{
				if (_ConnName != value)
				{
					OnConnNameChanging(_ConnName, value);
					_ConnName = value;
					OnConnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnName; }
		}
		private string _ConnName = "";
		partial void OnConnNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnConnNameChanged();
	
		#endregion Properties
	}
	public partial class GroupListAppSolutions : ConfigObjectBase<GroupListAppSolutions, GroupListAppSolutions.GroupListAppSolutionsValidator>, IComparable<GroupListAppSolutions>, IConfigAcceptVisitor, IGroupListAppSolutions // Class.tt Line: 6
	{
		public partial class GroupListAppSolutionsValidator : ValidatorBase<GroupListAppSolutions, GroupListAppSolutionsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListAppSolutions(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListAppSolutions() : base(GroupListAppSolutionsValidator.Validator)
		public GroupListAppSolutions(ITreeConfigNode parent) : base(parent, GroupListAppSolutionsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 27
			this.ListAppSolutions = new ConfigNodesCollection<AppSolution>(this); // Class.tt Line: 23
			OnInit();
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
		        vm.DefaultDb = AppDbSettings.Clone(vm, from.DefaultDb, isDeep);
		    vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 48
		    foreach(var t in from.ListAppSolutions) // Clone.tt Line: 49
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
		        foreach(var t in to.ListAppSolutions.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListAppSolutions)
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
		        foreach(var tt in from.ListAppSolutions)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListAppSolutions.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return GroupListAppSolutions.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListAppSolutions from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListAppSolutions.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_app_solutions' to 'GroupListAppSolutions'
		public static GroupListAppSolutions ConvertToVM(Proto.Config.proto_group_list_app_solutions m, GroupListAppSolutions vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.DefaultDb == null) // Clone.tt Line: 204
		        vm.DefaultDb = new AppDbSettings(); // Clone.tt Line: 208
		    AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb);
		    vm.ListAppSolutions = new ConfigNodesCollection<AppSolution>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListAppSolutions) // Clone.tt Line: 189
		    {
		        var tvm = AppSolution.ConvertToVM(t, new AppSolution(vm)); // Clone.tt Line: 192
		        vm.ListAppSolutions.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListAppSolutions' to 'proto_group_list_app_solutions'
		public static Proto.Config.proto_group_list_app_solutions ConvertToProto(GroupListAppSolutions vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_app_solutions m = new Proto.Config.proto_group_list_app_solutions(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 246
		    foreach(var t in vm.ListAppSolutions) // Clone.tt Line: 231
		        m.ListAppSolutions.Add(AppSolution.ConvertToProto((AppSolution)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListAppSolutions)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(2)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[PropertyOrderAttribute(3)]
		[ExpandableObjectAttribute()]
		[DisplayName("Default DB")]
		public AppDbSettings DefaultDb // Property.tt Line: 100
		{ 
			set
			{
				if (_DefaultDb != value)
				{
					OnDefaultDbChanging(_DefaultDb, value);
		            _DefaultDb = value;
					OnDefaultDbChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DefaultDb; }
		}
		private AppDbSettings _DefaultDb;
		partial void OnDefaultDbChanging(AppDbSettings from, AppDbSettings to); // Property.tt Line: 118
		partial void OnDefaultDbChanged();
		[BrowsableAttribute(false)]
		public IAppDbSettings IDefaultDb { get { return _DefaultDb; }}
		
		
		///////////////////////////////////////////////////
		/// List NET solutions
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<AppSolution> ListAppSolutions  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListAppSolutions != value)
				{
					OnListAppSolutionsChanging(_ListAppSolutions, value);
					_ListAppSolutions = value;
					OnListAppSolutionsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListAppSolutions; }
		}
		private ConfigNodesCollection<AppSolution> _ListAppSolutions;
		partial void OnListAppSolutionsChanging(SortedObservableCollection<AppSolution> from, SortedObservableCollection<AppSolution> to); // Property.tt Line: 71
		partial void OnListAppSolutionsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IAppSolution> IListAppSolutions { get { foreach (var t in _ListAppSolutions) yield return t; } }
		public AppSolution this[int index] { get { return (AppSolution)this.ListAppSolutions[index]; } }
		public void Add(AppSolution item)  // Property.tt Line: 78
		{ 
		    this.ListAppSolutions.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<AppSolution> items) 
		{ 
		    this.ListAppSolutions.AddRange(items); 
		    foreach(var t in items)
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
	public partial class AppSolution : ConfigObjectBase<AppSolution, AppSolution.AppSolutionValidator>, IComparable<AppSolution>, IConfigAcceptVisitor, IAppSolution // Class.tt Line: 6
	{
		public partial class AppSolutionValidator : ValidatorBase<AppSolution, AppSolutionValidator> { } // Class.tt Line: 8
		#region CTOR
		//public AppSolution(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public AppSolution() : base(AppSolutionValidator.Validator)
		public AppSolution(ITreeConfigNode parent) : base(parent, AppSolutionValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 27
			this.ListAppProjects = new ConfigNodesCollection<AppProject>(this); // Class.tt Line: 23
			OnInit();
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
		        vm.DefaultDb = AppDbSettings.Clone(vm, from.DefaultDb, isDeep);
		    vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 48
		    foreach(var t in from.ListAppProjects) // Clone.tt Line: 49
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
		        foreach(var t in to.ListAppProjects.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListAppProjects)
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
		        foreach(var tt in from.ListAppProjects)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListAppProjects.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return AppSolution.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(AppSolution from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    AppSolution.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_app_solution' to 'AppSolution'
		public static AppSolution ConvertToVM(Proto.Config.proto_app_solution m, AppSolution vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.RelativeAppSolutionPath = m.RelativeAppSolutionPath; // Clone.tt Line: 216
		    if (vm.DefaultDb == null) // Clone.tt Line: 204
		        vm.DefaultDb = new AppDbSettings(); // Clone.tt Line: 208
		    AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb);
		    vm.ListAppProjects = new ConfigNodesCollection<AppProject>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListAppProjects) // Clone.tt Line: 189
		    {
		        var tvm = AppProject.ConvertToVM(t, new AppProject(vm)); // Clone.tt Line: 192
		        vm.ListAppProjects.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'AppSolution' to 'proto_app_solution'
		public static Proto.Config.proto_app_solution ConvertToProto(AppSolution vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_app_solution m = new Proto.Config.proto_app_solution(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.RelativeAppSolutionPath = vm.RelativeAppSolutionPath; // Clone.tt Line: 252
		    m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 246
		    foreach(var t in vm.ListAppProjects) // Clone.tt Line: 231
		        m.ListAppProjects.Add(AppProject.ConvertToProto((AppProject)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListAppProjects)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// List NET projects
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(6)]
		[Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
		public string RelativeAppSolutionPath // Property.tt Line: 123
		{ 
			set
			{
				if (_RelativeAppSolutionPath != value)
				{
					OnRelativeAppSolutionPathChanging(_RelativeAppSolutionPath, value);
					_RelativeAppSolutionPath = value;
					OnRelativeAppSolutionPathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeAppSolutionPath; }
		}
		private string _RelativeAppSolutionPath = "";
		partial void OnRelativeAppSolutionPathChanging(string from, string to); // Property.tt Line: 141
		partial void OnRelativeAppSolutionPathChanged();
		
		[PropertyOrderAttribute(8)]
		[ExpandableObjectAttribute()]
		[DisplayName("Default DB")]
		[Description("Database connection. If empty, all solutions settings are used")]
		public AppDbSettings DefaultDb // Property.tt Line: 100
		{ 
			set
			{
				if (_DefaultDb != value)
				{
					OnDefaultDbChanging(_DefaultDb, value);
		            _DefaultDb = value;
					OnDefaultDbChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DefaultDb; }
		}
		private AppDbSettings _DefaultDb;
		partial void OnDefaultDbChanging(AppDbSettings from, AppDbSettings to); // Property.tt Line: 118
		partial void OnDefaultDbChanged();
		[BrowsableAttribute(false)]
		public IAppDbSettings IDefaultDb { get { return _DefaultDb; }}
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<AppProject> ListAppProjects  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListAppProjects != value)
				{
					OnListAppProjectsChanging(_ListAppProjects, value);
					_ListAppProjects = value;
					OnListAppProjectsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListAppProjects; }
		}
		private ConfigNodesCollection<AppProject> _ListAppProjects;
		partial void OnListAppProjectsChanging(SortedObservableCollection<AppProject> from, SortedObservableCollection<AppProject> to); // Property.tt Line: 71
		partial void OnListAppProjectsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IAppProject> IListAppProjects { get { foreach (var t in _ListAppProjects) yield return t; } }
	
		#endregion Properties
	}
	public partial class AppProject : ConfigObjectBase<AppProject, AppProject.AppProjectValidator>, IComparable<AppProject>, IConfigAcceptVisitor, IAppProject // Class.tt Line: 6
	{
		public partial class AppProjectValidator : ValidatorBase<AppProject, AppProjectValidator> { } // Class.tt Line: 8
		#region CTOR
		//public AppProject(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public AppProject() : base(AppProjectValidator.Validator)
		public AppProject(ITreeConfigNode parent) : base(parent, AppProjectValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 27
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		        vm.DefaultDb = AppDbSettings.Clone(vm, from.DefaultDb, isDeep);
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override AppProject Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return AppProject.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(AppProject from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    AppProject.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_app_project' to 'AppProject'
		public static AppProject ConvertToVM(Proto.Config.proto_app_project m, AppProject vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.RelativeAppProjectPath = m.RelativeAppProjectPath; // Clone.tt Line: 216
		    if (vm.DefaultDb == null) // Clone.tt Line: 204
		        vm.DefaultDb = new AppDbSettings(); // Clone.tt Line: 208
		    AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'AppProject' to 'proto_app_project'
		public static Proto.Config.proto_app_project ConvertToProto(AppProject vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_app_project m = new Proto.Config.proto_app_project(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.RelativeAppProjectPath = vm.RelativeAppProjectPath; // Clone.tt Line: 252
		    m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[PropertyOrderAttribute(6)]
		[Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
		public string RelativeAppProjectPath // Property.tt Line: 123
		{ 
			set
			{
				if (_RelativeAppProjectPath != value)
				{
					OnRelativeAppProjectPathChanging(_RelativeAppProjectPath, value);
					_RelativeAppProjectPath = value;
					OnRelativeAppProjectPathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeAppProjectPath; }
		}
		private string _RelativeAppProjectPath = "";
		partial void OnRelativeAppProjectPathChanging(string from, string to); // Property.tt Line: 141
		partial void OnRelativeAppProjectPathChanged();
		
		[PropertyOrderAttribute(8)]
		[ExpandableObjectAttribute()]
		[DisplayName("Default DB")]
		[Description("Database connection. If empty, solution settings are used")]
		public AppDbSettings DefaultDb // Property.tt Line: 100
		{ 
			set
			{
				if (_DefaultDb != value)
				{
					OnDefaultDbChanging(_DefaultDb, value);
		            _DefaultDb = value;
					OnDefaultDbChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DefaultDb; }
		}
		private AppDbSettings _DefaultDb;
		partial void OnDefaultDbChanging(AppDbSettings from, AppDbSettings to); // Property.tt Line: 118
		partial void OnDefaultDbChanged();
		[BrowsableAttribute(false)]
		public IAppDbSettings IDefaultDb { get { return _DefaultDb; }}
	
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Configuration model
	///////////////////////////////////////////////////
	public partial class ConfigModel : ConfigObjectBase<ConfigModel, ConfigModel.ConfigModelValidator>, IComparable<ConfigModel>, IConfigAcceptVisitor, IConfigModel // Class.tt Line: 6
	{
		public partial class ConfigModelValidator : ValidatorBase<ConfigModel, ConfigModelValidator> { } // Class.tt Line: 8
		#region CTOR
		//public ConfigModel(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public ConfigModel() : base(ConfigModelValidator.Validator)
		public ConfigModel(ITreeConfigNode parent) : base(parent, ConfigModelValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.GroupCommon = new GroupListCommon(this); // Class.tt Line: 29
			this.GroupConstants = new GroupListConstants(this); // Class.tt Line: 29
			this.GroupEnumerations = new GroupListEnumerations(this); // Class.tt Line: 29
			this.GroupCatalogs = new GroupListCatalogs(this); // Class.tt Line: 29
			this.GroupDocuments = new GroupDocuments(this); // Class.tt Line: 29
			this.GroupJournals = new GroupListJournals(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return ConfigModel.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(ConfigModel from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    ConfigModel.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_config_model' to 'ConfigModel'
		public static ConfigModel ConvertToVM(Proto.Config.proto_config_model m, ConfigModel vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Version = m.Version; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.GroupCommon == null) // Clone.tt Line: 204
		        vm.GroupCommon = new GroupListCommon(vm); // Clone.tt Line: 206
		    GroupListCommon.ConvertToVM(m.GroupCommon, vm.GroupCommon);
		    if (vm.GroupConstants == null) // Clone.tt Line: 204
		        vm.GroupConstants = new GroupListConstants(vm); // Clone.tt Line: 206
		    GroupListConstants.ConvertToVM(m.GroupConstants, vm.GroupConstants);
		    if (vm.GroupEnumerations == null) // Clone.tt Line: 204
		        vm.GroupEnumerations = new GroupListEnumerations(vm); // Clone.tt Line: 206
		    GroupListEnumerations.ConvertToVM(m.GroupEnumerations, vm.GroupEnumerations);
		    if (vm.GroupCatalogs == null) // Clone.tt Line: 204
		        vm.GroupCatalogs = new GroupListCatalogs(vm); // Clone.tt Line: 206
		    GroupListCatalogs.ConvertToVM(m.GroupCatalogs, vm.GroupCatalogs);
		    if (vm.GroupDocuments == null) // Clone.tt Line: 204
		        vm.GroupDocuments = new GroupDocuments(vm); // Clone.tt Line: 206
		    GroupDocuments.ConvertToVM(m.GroupDocuments, vm.GroupDocuments);
		    if (vm.GroupJournals == null) // Clone.tt Line: 204
		        vm.GroupJournals = new GroupListJournals(vm); // Clone.tt Line: 206
		    GroupListJournals.ConvertToVM(m.GroupJournals, vm.GroupJournals);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'ConfigModel' to 'proto_config_model'
		public static Proto.Config.proto_config_model ConvertToProto(ConfigModel vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_config_model m = new Proto.Config.proto_config_model(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Version = vm.Version; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.GroupCommon = GroupListCommon.ConvertToProto(vm.GroupCommon); // Clone.tt Line: 246
		    m.GroupConstants = GroupListConstants.ConvertToProto(vm.GroupConstants); // Clone.tt Line: 246
		    m.GroupEnumerations = GroupListEnumerations.ConvertToProto(vm.GroupEnumerations); // Clone.tt Line: 246
		    m.GroupCatalogs = GroupListCatalogs.ConvertToProto(vm.GroupCatalogs); // Clone.tt Line: 246
		    m.GroupDocuments = GroupDocuments.ConvertToProto(vm.GroupDocuments); // Clone.tt Line: 246
		    m.GroupJournals = GroupListJournals.ConvertToProto(vm.GroupJournals); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupCommon.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupConstants.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupEnumerations.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupCatalogs.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupDocuments.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupJournals.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(4)]
		[Editable(false)]
		public int Version // Property.tt Line: 123
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging(_Version, value);
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private int _Version;
		partial void OnVersionChanging(int from, int to); // Property.tt Line: 141
		partial void OnVersionChanged();
		
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public GroupListCommon GroupCommon // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupCommon != value)
				{
					OnGroupCommonChanging(_GroupCommon, value);
		            _GroupCommon = value;
					OnGroupCommonChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupCommon; }
		}
		private GroupListCommon _GroupCommon;
		partial void OnGroupCommonChanging(GroupListCommon from, GroupListCommon to); // Property.tt Line: 118
		partial void OnGroupCommonChanged();
		[BrowsableAttribute(false)]
		public IGroupListCommon IGroupCommon { get { return _GroupCommon; }}
		
		[BrowsableAttribute(false)]
		public GroupListConstants GroupConstants // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupConstants != value)
				{
					OnGroupConstantsChanging(_GroupConstants, value);
		            _GroupConstants = value;
					OnGroupConstantsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupConstants; }
		}
		private GroupListConstants _GroupConstants;
		partial void OnGroupConstantsChanging(GroupListConstants from, GroupListConstants to); // Property.tt Line: 118
		partial void OnGroupConstantsChanged();
		[BrowsableAttribute(false)]
		public IGroupListConstants IGroupConstants { get { return _GroupConstants; }}
		
		[BrowsableAttribute(false)]
		public GroupListEnumerations GroupEnumerations // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupEnumerations != value)
				{
					OnGroupEnumerationsChanging(_GroupEnumerations, value);
		            _GroupEnumerations = value;
					OnGroupEnumerationsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupEnumerations; }
		}
		private GroupListEnumerations _GroupEnumerations;
		partial void OnGroupEnumerationsChanging(GroupListEnumerations from, GroupListEnumerations to); // Property.tt Line: 118
		partial void OnGroupEnumerationsChanged();
		[BrowsableAttribute(false)]
		public IGroupListEnumerations IGroupEnumerations { get { return _GroupEnumerations; }}
		
		[BrowsableAttribute(false)]
		public GroupListCatalogs GroupCatalogs // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupCatalogs != value)
				{
					OnGroupCatalogsChanging(_GroupCatalogs, value);
		            _GroupCatalogs = value;
					OnGroupCatalogsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupCatalogs; }
		}
		private GroupListCatalogs _GroupCatalogs;
		partial void OnGroupCatalogsChanging(GroupListCatalogs from, GroupListCatalogs to); // Property.tt Line: 118
		partial void OnGroupCatalogsChanged();
		[BrowsableAttribute(false)]
		public IGroupListCatalogs IGroupCatalogs { get { return _GroupCatalogs; }}
		
		[BrowsableAttribute(false)]
		public GroupDocuments GroupDocuments // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupDocuments != value)
				{
					OnGroupDocumentsChanging(_GroupDocuments, value);
		            _GroupDocuments = value;
					OnGroupDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupDocuments; }
		}
		private GroupDocuments _GroupDocuments;
		partial void OnGroupDocumentsChanging(GroupDocuments from, GroupDocuments to); // Property.tt Line: 118
		partial void OnGroupDocumentsChanged();
		[BrowsableAttribute(false)]
		public IGroupDocuments IGroupDocuments { get { return _GroupDocuments; }}
		
		[BrowsableAttribute(false)]
		public GroupListJournals GroupJournals // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupJournals != value)
				{
					OnGroupJournalsChanging(_GroupJournals, value);
		            _GroupJournals = value;
					OnGroupJournalsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupJournals; }
		}
		private GroupListJournals _GroupJournals;
		partial void OnGroupJournalsChanging(GroupListJournals from, GroupListJournals to); // Property.tt Line: 118
		partial void OnGroupJournalsChanged();
		[BrowsableAttribute(false)]
		public IGroupListJournals IGroupJournals { get { return _GroupJournals; }}
	
		#endregion Properties
	}
	public partial class DataType : ViewModelValidatableWithSeverity<DataType, DataType.DataTypeValidator>, IDataType // Class.tt Line: 6
	{
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { } // Class.tt Line: 8
		#region CTOR
		public DataType() : base(DataTypeValidator.Validator) // Class.tt Line: 38
		{
			OnInitBegin();
			this.ListObjectGuids = new ObservableCollection<string>(); // Class.tt Line: 46
			OnInit();
		}
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DataType Clone(ITreeConfigNode parent, DataType from, bool isDeep = true) // Clone.tt Line: 27
		{
		    DataType vm = new DataType();
		    vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 62
		    vm.Length = from.Length; // Clone.tt Line: 62
		    vm.Accuracy = from.Accuracy; // Clone.tt Line: 62
		    vm.IsPositive = from.IsPositive; // Clone.tt Line: 62
		    vm.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 62
		    vm.IsNullable = from.IsNullable; // Clone.tt Line: 62
		    foreach(var t in from.ListObjectGuids) // Clone.tt Line: 41
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
		        foreach(var tt in from.ListObjectGuids)
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
		    OnBackupObjectStarting(ref isDeep);
			return DataType.Clone(null, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(DataType from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    DataType.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_data_type' to 'DataType'
		public static DataType ConvertToVM(Proto.Config.proto_data_type m, DataType vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.DataTypeEnum = (EnumDataType)m.DataTypeEnum; // Clone.tt Line: 214
		    vm.Length = m.Length; // Clone.tt Line: 216
		    vm.Accuracy = m.Accuracy; // Clone.tt Line: 216
		    vm.IsPositive = m.IsPositive; // Clone.tt Line: 216
		    vm.ObjectGuid = m.ObjectGuid; // Clone.tt Line: 216
		    vm.IsNullable = m.IsNullable; // Clone.tt Line: 216
		    vm.ListObjectGuids = new ObservableCollection<string>(); // Clone.tt Line: 172
		    foreach(var t in m.ListObjectGuids) // Clone.tt Line: 173
		    {
		        vm.ListObjectGuids.Add(t);
		    }
		    vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 216
		    return vm;
		}
		// Conversion from 'DataType' to 'proto_data_type'
		public static Proto.Config.proto_data_type ConvertToProto(DataType vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_data_type m = new Proto.Config.proto_data_type(); // Clone.tt Line: 228
		    m.DataTypeEnum = (Proto.Config.proto_enum_data_type)vm.DataTypeEnum; // Clone.tt Line: 250
		    m.Length = vm.Length; // Clone.tt Line: 252
		    m.Accuracy = vm.Accuracy; // Clone.tt Line: 252
		    m.IsPositive = vm.IsPositive; // Clone.tt Line: 252
		    m.ObjectGuid = vm.ObjectGuid; // Clone.tt Line: 252
		    m.IsNullable = vm.IsNullable; // Clone.tt Line: 252
		    foreach(var t in vm.ListObjectGuids) // Clone.tt Line: 231
		        m.ListObjectGuids.Add(t); // Clone.tt Line: 233
		    m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 252
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[DisplayName("Type")]
		public EnumDataType DataTypeEnum // Property.tt Line: 123
		{ 
			set
			{
				if (_DataTypeEnum != value)
				{
					OnDataTypeEnumChanging(_DataTypeEnum, value);
					_DataTypeEnum = value;
					OnDataTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeEnum; }
		}
		private EnumDataType _DataTypeEnum;
		partial void OnDataTypeEnumChanging(EnumDataType from, EnumDataType to); // Property.tt Line: 141
		partial void OnDataTypeEnumChanged();
		
		[PropertyOrderAttribute(5)]
		public uint Length // Property.tt Line: 123
		{ 
			set
			{
				if (_Length != value)
				{
					OnLengthChanging(_Length, value);
					_Length = value;
					OnLengthChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Length; }
		}
		private uint _Length;
		partial void OnLengthChanging(uint from, uint to); // Property.tt Line: 141
		partial void OnLengthChanged();
		
		[PropertyOrderAttribute(7)]
		public uint Accuracy // Property.tt Line: 123
		{ 
			set
			{
				if (_Accuracy != value)
				{
					OnAccuracyChanging(_Accuracy, value);
					_Accuracy = value;
					OnAccuracyChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Accuracy; }
		}
		private uint _Accuracy;
		partial void OnAccuracyChanging(uint from, uint to); // Property.tt Line: 141
		partial void OnAccuracyChanged();
		
		[PropertyOrderAttribute(6)]
		[DisplayName("Is positive")]
		public bool IsPositive // Property.tt Line: 123
		{ 
			set
			{
				if (_IsPositive != value)
				{
					OnIsPositiveChanging(_IsPositive, value);
					_IsPositive = value;
					OnIsPositiveChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPositive; }
		}
		private bool _IsPositive;
		partial void OnIsPositiveChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsPositiveChanged();
		
		[PropertyOrderAttribute(3)]
		[Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
		public string ObjectGuid // Property.tt Line: 123
		{ 
			set
			{
				if (_ObjectGuid != value)
				{
					OnObjectGuidChanging(_ObjectGuid, value);
					_ObjectGuid = value;
					OnObjectGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ObjectGuid; }
		}
		private string _ObjectGuid = "";
		partial void OnObjectGuidChanging(string from, string to); // Property.tt Line: 141
		partial void OnObjectGuidChanged();
		
		[PropertyOrderAttribute(2)]
		public bool IsNullable // Property.tt Line: 123
		{ 
			set
			{
				if (_IsNullable != value)
				{
					OnIsNullableChanging(_IsNullable, value);
					_IsNullable = value;
					OnIsNullableChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsNullable; }
		}
		private bool _IsNullable;
		partial void OnIsNullableChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsNullableChanged();
		
		[PropertyOrderAttribute(4)]
		public ObservableCollection<string> ListObjectGuids // Property.tt Line: 9
		{ 
			set
			{
				if (_ListObjectGuids != value)
				{
					OnListObjectGuidsChanging(_ListObjectGuids, value);
					_ListObjectGuids = value;
					OnListObjectGuidsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListObjectGuids; }
		}
		private ObservableCollection<string> _ListObjectGuids;
		partial void OnListObjectGuidsChanging(ObservableCollection<string> from, ObservableCollection<string> to); // Property.tt Line: 27
		partial void OnListObjectGuidsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<string> IListObjectGuids { get { foreach (var t in _ListObjectGuids) yield return t; } }
		
		[PropertyOrderAttribute(8)]
		[DisplayName("FK Index")]
		[Description("Create Index if this property is using foreign key (for Catalog or Document type)")]
		public bool IsIndexFk // Property.tt Line: 123
		{ 
			set
			{
				if (_IsIndexFk != value)
				{
					OnIsIndexFkChanging(_IsIndexFk, value);
					_IsIndexFk = value;
					OnIsIndexFkChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsIndexFk; }
		}
		private bool _IsIndexFk;
		partial void OnIsIndexFkChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsIndexFkChanged();
	
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Common parameters section
	///////////////////////////////////////////////////
	public partial class GroupListCommon : ConfigObjectBase<GroupListCommon, GroupListCommon.GroupListCommonValidator>, IComparable<GroupListCommon>, IConfigAcceptVisitor, IGroupListCommon // Class.tt Line: 6
	{
		public partial class GroupListCommonValidator : ValidatorBase<GroupListCommon, GroupListCommonValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListCommon(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListCommon() : base(GroupListCommonValidator.Validator)
		public GroupListCommon(ITreeConfigNode parent) : base(parent, GroupListCommonValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.GroupRoles = new GroupListRoles(this); // Class.tt Line: 29
			this.GroupViewForms = new GroupListMainViewForms(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return GroupListCommon.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListCommon from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListCommon.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_common' to 'GroupListCommon'
		public static GroupListCommon ConvertToVM(Proto.Config.proto_group_list_common m, GroupListCommon vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.GroupRoles == null) // Clone.tt Line: 204
		        vm.GroupRoles = new GroupListRoles(vm); // Clone.tt Line: 206
		    GroupListRoles.ConvertToVM(m.GroupRoles, vm.GroupRoles);
		    if (vm.GroupViewForms == null) // Clone.tt Line: 204
		        vm.GroupViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 206
		    GroupListMainViewForms.ConvertToVM(m.GroupViewForms, vm.GroupViewForms);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListCommon' to 'proto_group_list_common'
		public static Proto.Config.proto_group_list_common ConvertToProto(GroupListCommon vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_common m = new Proto.Config.proto_group_list_common(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.GroupRoles = GroupListRoles.ConvertToProto(vm.GroupRoles); // Clone.tt Line: 246
		    m.GroupViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupViewForms); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupRoles.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupViewForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public GroupListRoles GroupRoles // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupRoles != value)
				{
					OnGroupRolesChanging(_GroupRoles, value);
		            _GroupRoles = value;
					OnGroupRolesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupRoles; }
		}
		private GroupListRoles _GroupRoles;
		partial void OnGroupRolesChanging(GroupListRoles from, GroupListRoles to); // Property.tt Line: 118
		partial void OnGroupRolesChanged();
		[BrowsableAttribute(false)]
		public IGroupListRoles IGroupRoles { get { return _GroupRoles; }}
		
		[BrowsableAttribute(false)]
		public GroupListMainViewForms GroupViewForms // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupViewForms != value)
				{
					OnGroupViewFormsChanging(_GroupViewForms, value);
		            _GroupViewForms = value;
					OnGroupViewFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupViewForms; }
		}
		private GroupListMainViewForms _GroupViewForms;
		partial void OnGroupViewFormsChanging(GroupListMainViewForms from, GroupListMainViewForms to); // Property.tt Line: 118
		partial void OnGroupViewFormsChanged();
		[BrowsableAttribute(false)]
		public IGroupListMainViewForms IGroupViewForms { get { return _GroupViewForms; }}
	
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// User's role
	///////////////////////////////////////////////////
	public partial class Role : ConfigObjectBase<Role, Role.RoleValidator>, IComparable<Role>, IConfigAcceptVisitor, IRole // Class.tt Line: 6
	{
		public partial class RoleValidator : ValidatorBase<Role, RoleValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Role(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Role() : base(RoleValidator.Validator)
		public Role(ITreeConfigNode parent) : base(parent, RoleValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return Role.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Role from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Role.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_role' to 'Role'
		public static Role ConvertToVM(Proto.Config.proto_role m, Role vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Role' to 'proto_role'
		public static Proto.Config.proto_role ConvertToProto(Role vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_role m = new Proto.Config.proto_role(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
	
		#endregion Properties
	}
	public partial class GroupListRoles : ConfigObjectBase<GroupListRoles, GroupListRoles.GroupListRolesValidator>, IComparable<GroupListRoles>, IConfigAcceptVisitor, IGroupListRoles // Class.tt Line: 6
	{
		public partial class GroupListRolesValidator : ValidatorBase<GroupListRoles, GroupListRolesValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListRoles(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListRoles() : base(GroupListRolesValidator.Validator)
		public GroupListRoles(ITreeConfigNode parent) : base(parent, GroupListRolesValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListRoles = new ConfigNodesCollection<Role>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListRoles) // Clone.tt Line: 49
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
		        foreach(var t in to.ListRoles.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListRoles)
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
		        foreach(var tt in from.ListRoles)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListRoles.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return GroupListRoles.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListRoles from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListRoles.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_roles' to 'GroupListRoles'
		public static GroupListRoles ConvertToVM(Proto.Config.proto_group_list_roles m, GroupListRoles vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListRoles = new ConfigNodesCollection<Role>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListRoles) // Clone.tt Line: 189
		    {
		        var tvm = Role.ConvertToVM(t, new Role(vm)); // Clone.tt Line: 192
		        vm.ListRoles.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListRoles' to 'proto_group_list_roles'
		public static Proto.Config.proto_group_list_roles ConvertToProto(GroupListRoles vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_roles m = new Proto.Config.proto_group_list_roles(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListRoles) // Clone.tt Line: 231
		        m.ListRoles.Add(Role.ConvertToProto((Role)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListRoles)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Role> ListRoles  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListRoles != value)
				{
					OnListRolesChanging(_ListRoles, value);
					_ListRoles = value;
					OnListRolesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListRoles; }
		}
		private ConfigNodesCollection<Role> _ListRoles;
		partial void OnListRolesChanging(SortedObservableCollection<Role> from, SortedObservableCollection<Role> to); // Property.tt Line: 71
		partial void OnListRolesChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IRole> IListRoles { get { foreach (var t in _ListRoles) yield return t; } }
		public Role this[int index] { get { return (Role)this.ListRoles[index]; } }
		public void Add(Role item)  // Property.tt Line: 78
		{ 
		    this.ListRoles.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Role> items) 
		{ 
		    this.ListRoles.AddRange(items); 
		    foreach(var t in items)
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
	public partial class MainViewForm : ConfigObjectBase<MainViewForm, MainViewForm.MainViewFormValidator>, IComparable<MainViewForm>, IConfigAcceptVisitor, IMainViewForm // Class.tt Line: 6
	{
		public partial class MainViewFormValidator : ValidatorBase<MainViewForm, MainViewFormValidator> { } // Class.tt Line: 8
		#region CTOR
		//public MainViewForm(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public MainViewForm() : base(MainViewFormValidator.Validator)
		public MainViewForm(ITreeConfigNode parent) : base(parent, MainViewFormValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.GroupListViewForms = new GroupListMainViewForms(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return MainViewForm.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(MainViewForm from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    MainViewForm.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_main_view_form' to 'MainViewForm'
		public static MainViewForm ConvertToVM(Proto.Config.proto_main_view_form m, MainViewForm vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.GroupListViewForms == null) // Clone.tt Line: 204
		        vm.GroupListViewForms = new GroupListMainViewForms(vm); // Clone.tt Line: 206
		    GroupListMainViewForms.ConvertToVM(m.GroupListViewForms, vm.GroupListViewForms);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'MainViewForm' to 'proto_main_view_form'
		public static Proto.Config.proto_main_view_form ConvertToProto(MainViewForm vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_main_view_form m = new Proto.Config.proto_main_view_form(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.GroupListViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupListViewForms); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupListViewForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public GroupListMainViewForms GroupListViewForms // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupListViewForms != value)
				{
					OnGroupListViewFormsChanging(_GroupListViewForms, value);
		            _GroupListViewForms = value;
					OnGroupListViewFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupListViewForms; }
		}
		private GroupListMainViewForms _GroupListViewForms;
		partial void OnGroupListViewFormsChanging(GroupListMainViewForms from, GroupListMainViewForms to); // Property.tt Line: 118
		partial void OnGroupListViewFormsChanged();
		[BrowsableAttribute(false)]
		public IGroupListMainViewForms IGroupListViewForms { get { return _GroupListViewForms; }}
	
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy node with children
	///////////////////////////////////////////////////
	public partial class GroupListMainViewForms : ConfigObjectBase<GroupListMainViewForms, GroupListMainViewForms.GroupListMainViewFormsValidator>, IComparable<GroupListMainViewForms>, IConfigAcceptVisitor, IGroupListMainViewForms // Class.tt Line: 6
	{
		public partial class GroupListMainViewFormsValidator : ValidatorBase<GroupListMainViewForms, GroupListMainViewFormsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListMainViewForms(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListMainViewForms() : base(GroupListMainViewFormsValidator.Validator)
		public GroupListMainViewForms(ITreeConfigNode parent) : base(parent, GroupListMainViewFormsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListMainViewForms) // Clone.tt Line: 49
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
		        foreach(var t in to.ListMainViewForms.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListMainViewForms)
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
		        foreach(var tt in from.ListMainViewForms)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListMainViewForms.ToList())
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
		    OnBackupObjectStarting(ref isDeep);
			return GroupListMainViewForms.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListMainViewForms from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListMainViewForms.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_main_view_forms' to 'GroupListMainViewForms'
		public static GroupListMainViewForms ConvertToVM(Proto.Config.proto_group_list_main_view_forms m, GroupListMainViewForms vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListMainViewForms = new ConfigNodesCollection<MainViewForm>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListMainViewForms) // Clone.tt Line: 189
		    {
		        var tvm = MainViewForm.ConvertToVM(t, new MainViewForm(vm)); // Clone.tt Line: 192
		        vm.ListMainViewForms.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListMainViewForms' to 'proto_group_list_main_view_forms'
		public static Proto.Config.proto_group_list_main_view_forms ConvertToProto(GroupListMainViewForms vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_main_view_forms m = new Proto.Config.proto_group_list_main_view_forms(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListMainViewForms) // Clone.tt Line: 231
		        m.ListMainViewForms.Add(MainViewForm.ConvertToProto((MainViewForm)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListMainViewForms)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<MainViewForm> ListMainViewForms  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListMainViewForms != value)
				{
					OnListMainViewFormsChanging(_ListMainViewForms, value);
					_ListMainViewForms = value;
					OnListMainViewFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListMainViewForms; }
		}
		private ConfigNodesCollection<MainViewForm> _ListMainViewForms;
		partial void OnListMainViewFormsChanging(SortedObservableCollection<MainViewForm> from, SortedObservableCollection<MainViewForm> to); // Property.tt Line: 71
		partial void OnListMainViewFormsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IMainViewForm> IListMainViewForms { get { foreach (var t in _ListMainViewForms) yield return t; } }
		public MainViewForm this[int index] { get { return (MainViewForm)this.ListMainViewForms[index]; } }
		public void Add(MainViewForm item)  // Property.tt Line: 78
		{ 
		    this.ListMainViewForms.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<MainViewForm> items) 
		{ 
		    this.ListMainViewForms.AddRange(items); 
		    foreach(var t in items)
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
	public partial class GroupListPropertiesTabs : ConfigObjectBase<GroupListPropertiesTabs, GroupListPropertiesTabs.GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IConfigAcceptVisitor, IGroupListPropertiesTabs // Class.tt Line: 6
	{
		public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListPropertiesTabs(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListPropertiesTabs() : base(GroupListPropertiesTabsValidator.Validator)
		public GroupListPropertiesTabs(ITreeConfigNode parent) : base(parent, GroupListPropertiesTabsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListPropertiesTabs) // Clone.tt Line: 49
		        vm.ListPropertiesTabs.Add(PropertiesTab.Clone(vm, (PropertiesTab)t, isDeep));
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
		        foreach(var t in to.ListPropertiesTabs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPropertiesTabs)
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
		        foreach(var tt in from.ListPropertiesTabs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListPropertiesTabs.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListPropertiesTabs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListPropertiesTabs.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListPropertiesTabs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListPropertiesTabs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_properties_tabs' to 'GroupListPropertiesTabs'
		public static GroupListPropertiesTabs ConvertToVM(Proto.Config.proto_group_list_properties_tabs m, GroupListPropertiesTabs vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListPropertiesTabs = new ConfigNodesCollection<PropertiesTab>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListPropertiesTabs) // Clone.tt Line: 189
		    {
		        var tvm = PropertiesTab.ConvertToVM(t, new PropertiesTab(vm)); // Clone.tt Line: 192
		        vm.ListPropertiesTabs.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListPropertiesTabs' to 'proto_group_list_properties_tabs'
		public static Proto.Config.proto_group_list_properties_tabs ConvertToProto(GroupListPropertiesTabs vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_properties_tabs m = new Proto.Config.proto_group_list_properties_tabs(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListPropertiesTabs) // Clone.tt Line: 231
		        m.ListPropertiesTabs.Add(PropertiesTab.ConvertToProto((PropertiesTab)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPropertiesTabs)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<PropertiesTab> ListPropertiesTabs  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListPropertiesTabs != value)
				{
					OnListPropertiesTabsChanging(_ListPropertiesTabs, value);
					_ListPropertiesTabs = value;
					OnListPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPropertiesTabs; }
		}
		private ConfigNodesCollection<PropertiesTab> _ListPropertiesTabs;
		partial void OnListPropertiesTabsChanging(SortedObservableCollection<PropertiesTab> from, SortedObservableCollection<PropertiesTab> to); // Property.tt Line: 71
		partial void OnListPropertiesTabsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IPropertiesTab> IListPropertiesTabs { get { foreach (var t in _ListPropertiesTabs) yield return t; } }
		public PropertiesTab this[int index] { get { return (PropertiesTab)this.ListPropertiesTabs[index]; } }
		public void Add(PropertiesTab item)  // Property.tt Line: 78
		{ 
		    this.ListPropertiesTabs.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<PropertiesTab> items) 
		{ 
		    this.ListPropertiesTabs.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	public partial class PropertiesTab : ConfigObjectBase<PropertiesTab, PropertiesTab.PropertiesTabValidator>, IComparable<PropertiesTab>, IConfigAcceptVisitor, IPropertiesTab // Class.tt Line: 6
	{
		public partial class PropertiesTabValidator : ValidatorBase<PropertiesTab, PropertiesTabValidator> { } // Class.tt Line: 8
		#region CTOR
		//public PropertiesTab(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public PropertiesTab() : base(PropertiesTabValidator.Validator)
		public PropertiesTab(ITreeConfigNode parent) : base(parent, PropertiesTabValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 29
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override PropertiesTab Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return PropertiesTab.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(PropertiesTab from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    PropertiesTab.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_properties_tab' to 'PropertiesTab'
		public static PropertiesTab ConvertToVM(Proto.Config.proto_properties_tab m, PropertiesTab vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.GroupProperties == null) // Clone.tt Line: 204
		        vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 206
		    GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties);
		    if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 204
		        vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 206
		    GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs);
		    vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'PropertiesTab' to 'proto_properties_tab'
		public static Proto.Config.proto_properties_tab ConvertToProto(PropertiesTab vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_properties_tab m = new Proto.Config.proto_properties_tab(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 246
		    m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 246
		    m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupProperties.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging(_GroupProperties, value);
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupListProperties _GroupProperties;
		partial void OnGroupPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 118
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public IGroupListProperties IGroupProperties { get { return _GroupProperties; }}
		
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupPropertiesTabs != value)
				{
					OnGroupPropertiesTabsChanging(_GroupPropertiesTabs, value);
		            _GroupPropertiesTabs = value;
					OnGroupPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesTabs; }
		}
		private GroupListPropertiesTabs _GroupPropertiesTabs;
		partial void OnGroupPropertiesTabsChanging(GroupListPropertiesTabs from, GroupListPropertiesTabs to); // Property.tt Line: 118
		partial void OnGroupPropertiesTabsChanged();
		[BrowsableAttribute(false)]
		public IGroupListPropertiesTabs IGroupPropertiesTabs { get { return _GroupPropertiesTabs; }}
		
		
		///////////////////////////////////////////////////
		/// Create Index for foreign key navigation property
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(4)]
		public bool IsIndexFk // Property.tt Line: 123
		{ 
			set
			{
				if (_IsIndexFk != value)
				{
					OnIsIndexFkChanging(_IsIndexFk, value);
					_IsIndexFk = value;
					OnIsIndexFkChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsIndexFk; }
		}
		private bool _IsIndexFk;
		partial void OnIsIndexFkChanging(bool from, bool to); // Property.tt Line: 141
		partial void OnIsIndexFkChanged();
	
		#endregion Properties
	}
	public partial class GroupListProperties : ConfigObjectBase<GroupListProperties, GroupListProperties.GroupListPropertiesValidator>, IComparable<GroupListProperties>, IConfigAcceptVisitor, IGroupListProperties // Class.tt Line: 6
	{
		public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListProperties(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListProperties() : base(GroupListPropertiesValidator.Validator)
		public GroupListProperties(ITreeConfigNode parent) : base(parent, GroupListPropertiesValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListProperties = new ConfigNodesCollection<Property>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListProperties) // Clone.tt Line: 49
		        vm.ListProperties.Add(Property.Clone(vm, (Property)t, isDeep));
		    vm.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 62
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
		        foreach(var t in to.ListProperties.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListProperties)
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
		        foreach(var tt in from.ListProperties)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListProperties.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListProperties Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListProperties.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListProperties from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListProperties.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_properties' to 'GroupListProperties'
		public static GroupListProperties ConvertToVM(Proto.Config.proto_group_list_properties m, GroupListProperties vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListProperties = new ConfigNodesCollection<Property>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListProperties) // Clone.tt Line: 189
		    {
		        var tvm = Property.ConvertToVM(t, new Property(vm)); // Clone.tt Line: 192
		        vm.ListProperties.Add(tvm);
		    }
		    vm.LastGenPosition = m.LastGenPosition; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListProperties' to 'proto_group_list_properties'
		public static Proto.Config.proto_group_list_properties ConvertToProto(GroupListProperties vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_properties m = new Proto.Config.proto_group_list_properties(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListProperties) // Clone.tt Line: 231
		        m.ListProperties.Add(Property.ConvertToProto((Property)t)); // Clone.tt Line: 235
		    m.LastGenPosition = vm.LastGenPosition; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListProperties)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Property> ListProperties  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListProperties != value)
				{
					OnListPropertiesChanging(_ListProperties, value);
					_ListProperties = value;
					OnListPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListProperties; }
		}
		private ConfigNodesCollection<Property> _ListProperties;
		partial void OnListPropertiesChanging(SortedObservableCollection<Property> from, SortedObservableCollection<Property> to); // Property.tt Line: 71
		partial void OnListPropertiesChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IProperty> IListProperties { get { foreach (var t in _ListProperties) yield return t; } }
		public Property this[int index] { get { return (Property)this.ListProperties[index]; } }
		public void Add(Property item)  // Property.tt Line: 78
		{ 
		    this.ListProperties.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Property> items) 
		{ 
		    this.ListProperties.AddRange(items); 
		    foreach(var t in items)
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
		[Editable(false)]
		public uint LastGenPosition // Property.tt Line: 123
		{ 
			set
			{
				if (_LastGenPosition != value)
				{
					OnLastGenPositionChanging(_LastGenPosition, value);
					_LastGenPosition = value;
					OnLastGenPositionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _LastGenPosition; }
		}
		private uint _LastGenPosition;
		partial void OnLastGenPositionChanging(uint from, uint to); // Property.tt Line: 141
		partial void OnLastGenPositionChanged();
	
		#endregion Properties
	}
	public partial class Property : ConfigObjectBase<Property, Property.PropertyValidator>, IComparable<Property>, IConfigAcceptVisitor, IProperty // Class.tt Line: 6
	{
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Property(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Property() : base(PropertyValidator.Validator)
		public Property(ITreeConfigNode parent) : base(parent, PropertyValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.DataType = new DataType(); // Class.tt Line: 27
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		        vm.DataType = DataType.Clone(vm, from.DataType, isDeep);
		    vm.Position = from.Position; // Clone.tt Line: 62
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Property Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Property.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Property from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Property.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_property' to 'Property'
		public static Property ConvertToVM(Proto.Config.proto_property m, Property vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.DataType == null) // Clone.tt Line: 204
		        vm.DataType = new DataType(); // Clone.tt Line: 208
		    DataType.ConvertToVM(m.DataType, vm.DataType);
		    vm.Position = m.Position; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Property' to 'proto_property'
		public static Proto.Config.proto_property ConvertToProto(Property vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_property m = new Proto.Config.proto_property(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 246
		    m.Position = vm.Position; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[PropertyOrderAttribute(4)]
		[ExpandableObjectAttribute()]
		[DisplayName("Type")]
		public DataType DataType // Property.tt Line: 100
		{ 
			set
			{
				if (_DataType != value)
				{
					OnDataTypeChanging(_DataType, value);
		            _DataType = value;
					OnDataTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataType; }
		}
		private DataType _DataType;
		partial void OnDataTypeChanging(DataType from, DataType to); // Property.tt Line: 118
		partial void OnDataTypeChanged();
		[BrowsableAttribute(false)]
		public IDataType IDataType { get { return _DataType; }}
		
		
		///////////////////////////////////////////////////
		/// Protobuf field position
		/// Reserved positions: 1 - primary key
		///////////////////////////////////////////////////
		[Editable(false)]
		public uint Position // Property.tt Line: 123
		{ 
			set
			{
				if (_Position != value)
				{
					OnPositionChanging(_Position, value);
					_Position = value;
					OnPositionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Position; }
		}
		private uint _Position;
		partial void OnPositionChanging(uint from, uint to); // Property.tt Line: 141
		partial void OnPositionChanged();
	
		#endregion Properties
	}
	public partial class GroupListConstants : ConfigObjectBase<GroupListConstants, GroupListConstants.GroupListConstantsValidator>, IComparable<GroupListConstants>, IConfigAcceptVisitor, IGroupListConstants // Class.tt Line: 6
	{
		public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListConstants(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListConstants() : base(GroupListConstantsValidator.Validator)
		public GroupListConstants(ITreeConfigNode parent) : base(parent, GroupListConstantsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListConstants = new ConfigNodesCollection<Constant>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListConstants) // Clone.tt Line: 49
		        vm.ListConstants.Add(Constant.Clone(vm, (Constant)t, isDeep));
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
		        foreach(var t in to.ListConstants.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListConstants)
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
		        foreach(var tt in from.ListConstants)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListConstants.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListConstants Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListConstants.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListConstants from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListConstants.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_constants' to 'GroupListConstants'
		public static GroupListConstants ConvertToVM(Proto.Config.proto_group_list_constants m, GroupListConstants vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListConstants = new ConfigNodesCollection<Constant>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListConstants) // Clone.tt Line: 189
		    {
		        var tvm = Constant.ConvertToVM(t, new Constant(vm)); // Clone.tt Line: 192
		        vm.ListConstants.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListConstants' to 'proto_group_list_constants'
		public static Proto.Config.proto_group_list_constants ConvertToProto(GroupListConstants vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_constants m = new Proto.Config.proto_group_list_constants(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListConstants) // Clone.tt Line: 231
		        m.ListConstants.Add(Constant.ConvertToProto((Constant)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListConstants)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Constant> ListConstants  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListConstants != value)
				{
					OnListConstantsChanging(_ListConstants, value);
					_ListConstants = value;
					OnListConstantsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListConstants; }
		}
		private ConfigNodesCollection<Constant> _ListConstants;
		partial void OnListConstantsChanging(SortedObservableCollection<Constant> from, SortedObservableCollection<Constant> to); // Property.tt Line: 71
		partial void OnListConstantsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IConstant> IListConstants { get { foreach (var t in _ListConstants) yield return t; } }
		public Constant this[int index] { get { return (Constant)this.ListConstants[index]; } }
		public void Add(Constant item)  // Property.tt Line: 78
		{ 
		    this.ListConstants.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Constant> items) 
		{ 
		    this.ListConstants.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	public partial class Constant : ConfigObjectBase<Constant, Constant.ConstantValidator>, IComparable<Constant>, IConfigAcceptVisitor, IConstant // Class.tt Line: 6
	{
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Constant(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Constant() : base(ConstantValidator.Validator)
		public Constant(ITreeConfigNode parent) : base(parent, ConstantValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.DataType = new DataType(); // Class.tt Line: 27
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		        vm.DataType = DataType.Clone(vm, from.DataType, isDeep);
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Constant Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Constant.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Constant from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Constant.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_constant' to 'Constant'
		public static Constant ConvertToVM(Proto.Config.proto_constant m, Constant vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.DataType == null) // Clone.tt Line: 204
		        vm.DataType = new DataType(); // Clone.tt Line: 208
		    DataType.ConvertToVM(m.DataType, vm.DataType);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Constant' to 'proto_constant'
		public static Proto.Config.proto_constant ConvertToProto(Constant vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_constant m = new Proto.Config.proto_constant(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[PropertyOrderAttribute(4)]
		[ExpandableObjectAttribute()]
		[DisplayName("Type")]
		public DataType DataType // Property.tt Line: 100
		{ 
			set
			{
				if (_DataType != value)
				{
					OnDataTypeChanging(_DataType, value);
		            _DataType = value;
					OnDataTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataType; }
		}
		private DataType _DataType;
		partial void OnDataTypeChanging(DataType from, DataType to); // Property.tt Line: 118
		partial void OnDataTypeChanged();
		[BrowsableAttribute(false)]
		public IDataType IDataType { get { return _DataType; }}
	
		#endregion Properties
	}
	public partial class GroupListEnumerations : ConfigObjectBase<GroupListEnumerations, GroupListEnumerations.GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IConfigAcceptVisitor, IGroupListEnumerations // Class.tt Line: 6
	{
		public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListEnumerations(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListEnumerations() : base(GroupListEnumerationsValidator.Validator)
		public GroupListEnumerations(ITreeConfigNode parent) : base(parent, GroupListEnumerationsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListEnumerations = new ConfigNodesCollection<Enumeration>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListEnumerations) // Clone.tt Line: 49
		        vm.ListEnumerations.Add(Enumeration.Clone(vm, (Enumeration)t, isDeep));
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
		        foreach(var t in to.ListEnumerations.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListEnumerations)
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
		        foreach(var tt in from.ListEnumerations)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListEnumerations.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListEnumerations Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListEnumerations.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListEnumerations from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListEnumerations.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_enumerations' to 'GroupListEnumerations'
		public static GroupListEnumerations ConvertToVM(Proto.Config.proto_group_list_enumerations m, GroupListEnumerations vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListEnumerations = new ConfigNodesCollection<Enumeration>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListEnumerations) // Clone.tt Line: 189
		    {
		        var tvm = Enumeration.ConvertToVM(t, new Enumeration(vm)); // Clone.tt Line: 192
		        vm.ListEnumerations.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListEnumerations' to 'proto_group_list_enumerations'
		public static Proto.Config.proto_group_list_enumerations ConvertToProto(GroupListEnumerations vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_enumerations m = new Proto.Config.proto_group_list_enumerations(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListEnumerations) // Clone.tt Line: 231
		        m.ListEnumerations.Add(Enumeration.ConvertToProto((Enumeration)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListEnumerations)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Enumeration> ListEnumerations  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListEnumerations != value)
				{
					OnListEnumerationsChanging(_ListEnumerations, value);
					_ListEnumerations = value;
					OnListEnumerationsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListEnumerations; }
		}
		private ConfigNodesCollection<Enumeration> _ListEnumerations;
		partial void OnListEnumerationsChanging(SortedObservableCollection<Enumeration> from, SortedObservableCollection<Enumeration> to); // Property.tt Line: 71
		partial void OnListEnumerationsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IEnumeration> IListEnumerations { get { foreach (var t in _ListEnumerations) yield return t; } }
		public Enumeration this[int index] { get { return (Enumeration)this.ListEnumerations[index]; } }
		public void Add(Enumeration item)  // Property.tt Line: 78
		{ 
		    this.ListEnumerations.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Enumeration> items) 
		{ 
		    this.ListEnumerations.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	public partial class Enumeration : ConfigObjectBase<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>, IConfigAcceptVisitor, IEnumeration // Class.tt Line: 6
	{
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Enumeration(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Enumeration() : base(EnumerationValidator.Validator)
		public Enumeration(ITreeConfigNode parent) : base(parent, EnumerationValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListEnumerationPairs) // Clone.tt Line: 49
		        vm.ListEnumerationPairs.Add(EnumerationPair.Clone(vm, (EnumerationPair)t, isDeep));
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
		        foreach(var t in to.ListEnumerationPairs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListEnumerationPairs)
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
		        foreach(var tt in from.ListEnumerationPairs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListEnumerationPairs.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Enumeration Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Enumeration.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Enumeration from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Enumeration.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_enumeration' to 'Enumeration'
		public static Enumeration ConvertToVM(Proto.Config.proto_enumeration m, Enumeration vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.DataTypeEnum = (EnumEnumerationType)m.DataTypeEnum; // Clone.tt Line: 214
		    vm.DataTypeLength = m.DataTypeLength; // Clone.tt Line: 216
		    vm.ListEnumerationPairs = new ConfigNodesCollection<EnumerationPair>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListEnumerationPairs) // Clone.tt Line: 189
		    {
		        var tvm = EnumerationPair.ConvertToVM(t, new EnumerationPair(vm)); // Clone.tt Line: 192
		        vm.ListEnumerationPairs.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Enumeration' to 'proto_enumeration'
		public static Proto.Config.proto_enumeration ConvertToProto(Enumeration vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_enumeration m = new Proto.Config.proto_enumeration(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.DataTypeEnum = (Proto.Config.enum_enumeration_type)vm.DataTypeEnum; // Clone.tt Line: 250
		    m.DataTypeLength = vm.DataTypeLength; // Clone.tt Line: 252
		    foreach(var t in vm.ListEnumerationPairs) // Clone.tt Line: 231
		        m.ListEnumerationPairs.Add(EnumerationPair.ConvertToProto((EnumerationPair)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListEnumerationPairs)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// Enumeration element type
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(4)]
		[DisplayName("Type")]
		public EnumEnumerationType DataTypeEnum // Property.tt Line: 123
		{ 
			set
			{
				if (_DataTypeEnum != value)
				{
					OnDataTypeEnumChanging(_DataTypeEnum, value);
					_DataTypeEnum = value;
					OnDataTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeEnum; }
		}
		private EnumEnumerationType _DataTypeEnum;
		partial void OnDataTypeEnumChanging(EnumEnumerationType from, EnumEnumerationType to); // Property.tt Line: 141
		partial void OnDataTypeEnumChanged();
		
		
		///////////////////////////////////////////////////
		/// Length of string if 'STRING' is selected as enumeration element type
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(5)]
		[DisplayName("Length")]
		public int DataTypeLength // Property.tt Line: 123
		{ 
			set
			{
				if (_DataTypeLength != value)
				{
					OnDataTypeLengthChanging(_DataTypeLength, value);
					_DataTypeLength = value;
					OnDataTypeLengthChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeLength; }
		}
		private int _DataTypeLength;
		partial void OnDataTypeLengthChanging(int from, int to); // Property.tt Line: 141
		partial void OnDataTypeLengthChanged();
		
		[DisplayName("Elements")]
		[NewItemTypes(typeof(EnumerationPair))]
		public ConfigNodesCollection<EnumerationPair> ListEnumerationPairs  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListEnumerationPairs != value)
				{
					OnListEnumerationPairsChanging(_ListEnumerationPairs, value);
					_ListEnumerationPairs = value;
					OnListEnumerationPairsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListEnumerationPairs; }
		}
		private ConfigNodesCollection<EnumerationPair> _ListEnumerationPairs;
		partial void OnListEnumerationPairsChanging(SortedObservableCollection<EnumerationPair> from, SortedObservableCollection<EnumerationPair> to); // Property.tt Line: 71
		partial void OnListEnumerationPairsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IEnumerationPair> IListEnumerationPairs { get { foreach (var t in _ListEnumerationPairs) yield return t; } }
	
		#endregion Properties
	}
	public partial class EnumerationPair : ConfigObjectBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>, IConfigAcceptVisitor, IEnumerationPair // Class.tt Line: 6
	{
		public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { } // Class.tt Line: 8
		#region CTOR
		//public EnumerationPair(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public EnumerationPair() : base(EnumerationPairValidator.Validator)
		public EnumerationPair(ITreeConfigNode parent) : base(parent, EnumerationPairValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		    OnBackupObjectStarting(ref isDeep);
			return EnumerationPair.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(EnumerationPair from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    EnumerationPair.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_enumeration_pair' to 'EnumerationPair'
		public static EnumerationPair ConvertToVM(Proto.Config.proto_enumeration_pair m, EnumerationPair vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.Value = m.Value; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
		public static Proto.Config.proto_enumeration_pair ConvertToProto(EnumerationPair vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_enumeration_pair m = new Proto.Config.proto_enumeration_pair(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.Value = vm.Value; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		public string Value // Property.tt Line: 123
		{ 
			set
			{
				if (_Value != value)
				{
					OnValueChanging(_Value, value);
					_Value = value;
					OnValueChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Value; }
		}
		private string _Value = "";
		partial void OnValueChanging(string from, string to); // Property.tt Line: 141
		partial void OnValueChanged();
	
		#endregion Properties
	}
	public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IConfigAcceptVisitor, ICatalog // Class.tt Line: 6
	{
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Catalog(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Catalog() : base(CatalogValidator.Validator)
		public Catalog(ITreeConfigNode parent) : base(parent, CatalogValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 29
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 29
			this.GroupForms = new GroupListForms(this); // Class.tt Line: 29
			this.GroupReports = new GroupListReports(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Catalog Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Catalog.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Catalog from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Catalog.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_catalog' to 'Catalog'
		public static Catalog ConvertToVM(Proto.Config.proto_catalog m, Catalog vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.GroupProperties == null) // Clone.tt Line: 204
		        vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 206
		    GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties);
		    if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 204
		        vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 206
		    GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs);
		    if (vm.GroupForms == null) // Clone.tt Line: 204
		        vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 206
		    GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms);
		    if (vm.GroupReports == null) // Clone.tt Line: 204
		        vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 206
		    GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Catalog' to 'proto_catalog'
		public static Proto.Config.proto_catalog ConvertToProto(Catalog vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_catalog m = new Proto.Config.proto_catalog(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 246
		    m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 246
		    m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 246
		    m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupReports.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging(_GroupProperties, value);
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupListProperties _GroupProperties;
		partial void OnGroupPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 118
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public IGroupListProperties IGroupProperties { get { return _GroupProperties; }}
		
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupPropertiesTabs != value)
				{
					OnGroupPropertiesTabsChanging(_GroupPropertiesTabs, value);
		            _GroupPropertiesTabs = value;
					OnGroupPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesTabs; }
		}
		private GroupListPropertiesTabs _GroupPropertiesTabs;
		partial void OnGroupPropertiesTabsChanging(GroupListPropertiesTabs from, GroupListPropertiesTabs to); // Property.tt Line: 118
		partial void OnGroupPropertiesTabsChanged();
		[BrowsableAttribute(false)]
		public IGroupListPropertiesTabs IGroupPropertiesTabs { get { return _GroupPropertiesTabs; }}
		
		[BrowsableAttribute(false)]
		public GroupListForms GroupForms // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupForms != value)
				{
					OnGroupFormsChanging(_GroupForms, value);
		            _GroupForms = value;
					OnGroupFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupForms; }
		}
		private GroupListForms _GroupForms;
		partial void OnGroupFormsChanging(GroupListForms from, GroupListForms to); // Property.tt Line: 118
		partial void OnGroupFormsChanged();
		[BrowsableAttribute(false)]
		public IGroupListForms IGroupForms { get { return _GroupForms; }}
		
		[BrowsableAttribute(false)]
		public GroupListReports GroupReports // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupReports != value)
				{
					OnGroupReportsChanging(_GroupReports, value);
		            _GroupReports = value;
					OnGroupReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupReports; }
		}
		private GroupListReports _GroupReports;
		partial void OnGroupReportsChanging(GroupListReports from, GroupListReports to); // Property.tt Line: 118
		partial void OnGroupReportsChanged();
		[BrowsableAttribute(false)]
		public IGroupListReports IGroupReports { get { return _GroupReports; }}
	
		#endregion Properties
	}
	public partial class GroupListCatalogs : ConfigObjectBase<GroupListCatalogs, GroupListCatalogs.GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IConfigAcceptVisitor, IGroupListCatalogs // Class.tt Line: 6
	{
		public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListCatalogs(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListCatalogs() : base(GroupListCatalogsValidator.Validator)
		public GroupListCatalogs(ITreeConfigNode parent) : base(parent, GroupListCatalogsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListCatalogs = new ConfigNodesCollection<Catalog>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListCatalogs) // Clone.tt Line: 49
		        vm.ListCatalogs.Add(Catalog.Clone(vm, (Catalog)t, isDeep));
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
		        foreach(var t in to.ListCatalogs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListCatalogs)
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
		        foreach(var tt in from.ListCatalogs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListCatalogs.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListCatalogs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListCatalogs.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListCatalogs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListCatalogs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_catalogs' to 'GroupListCatalogs'
		public static GroupListCatalogs ConvertToVM(Proto.Config.proto_group_list_catalogs m, GroupListCatalogs vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListCatalogs = new ConfigNodesCollection<Catalog>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListCatalogs) // Clone.tt Line: 189
		    {
		        var tvm = Catalog.ConvertToVM(t, new Catalog(vm)); // Clone.tt Line: 192
		        vm.ListCatalogs.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
		public static Proto.Config.proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_catalogs m = new Proto.Config.proto_group_list_catalogs(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListCatalogs) // Clone.tt Line: 231
		        m.ListCatalogs.Add(Catalog.ConvertToProto((Catalog)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListCatalogs)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Catalog> ListCatalogs  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListCatalogs != value)
				{
					OnListCatalogsChanging(_ListCatalogs, value);
					_ListCatalogs = value;
					OnListCatalogsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListCatalogs; }
		}
		private ConfigNodesCollection<Catalog> _ListCatalogs;
		partial void OnListCatalogsChanging(SortedObservableCollection<Catalog> from, SortedObservableCollection<Catalog> to); // Property.tt Line: 71
		partial void OnListCatalogsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<ICatalog> IListCatalogs { get { foreach (var t in _ListCatalogs) yield return t; } }
		public Catalog this[int index] { get { return (Catalog)this.ListCatalogs[index]; } }
		public void Add(Catalog item)  // Property.tt Line: 78
		{ 
		    this.ListCatalogs.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Catalog> items) 
		{ 
		    this.ListCatalogs.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	public partial class GroupDocuments : ConfigObjectBase<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IConfigAcceptVisitor, IGroupDocuments // Class.tt Line: 6
	{
		public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupDocuments(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupDocuments() : base(GroupDocumentsValidator.Validator)
		public GroupDocuments(ITreeConfigNode parent) : base(parent, GroupDocumentsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.GroupSharedProperties = new GroupListProperties(this); // Class.tt Line: 29
			this.GroupListDocuments = new GroupListDocuments(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupDocuments Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupDocuments.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupDocuments from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupDocuments.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_documents' to 'GroupDocuments'
		public static GroupDocuments ConvertToVM(Proto.Config.proto_group_documents m, GroupDocuments vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.GroupSharedProperties == null) // Clone.tt Line: 204
		        vm.GroupSharedProperties = new GroupListProperties(vm); // Clone.tt Line: 206
		    GroupListProperties.ConvertToVM(m.GroupSharedProperties, vm.GroupSharedProperties);
		    if (vm.GroupListDocuments == null) // Clone.tt Line: 204
		        vm.GroupListDocuments = new GroupListDocuments(vm); // Clone.tt Line: 206
		    GroupListDocuments.ConvertToVM(m.GroupListDocuments, vm.GroupListDocuments);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupDocuments' to 'proto_group_documents'
		public static Proto.Config.proto_group_documents ConvertToProto(GroupDocuments vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_documents m = new Proto.Config.proto_group_documents(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.GroupSharedProperties = GroupListProperties.ConvertToProto(vm.GroupSharedProperties); // Clone.tt Line: 246
		    m.GroupListDocuments = GroupListDocuments.ConvertToProto(vm.GroupListDocuments); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupListDocuments.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public GroupListProperties GroupSharedProperties // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupSharedProperties != value)
				{
					OnGroupSharedPropertiesChanging(_GroupSharedProperties, value);
		            _GroupSharedProperties = value;
					OnGroupSharedPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupSharedProperties; }
		}
		private GroupListProperties _GroupSharedProperties;
		partial void OnGroupSharedPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 118
		partial void OnGroupSharedPropertiesChanged();
		[BrowsableAttribute(false)]
		public IGroupListProperties IGroupSharedProperties { get { return _GroupSharedProperties; }}
		
		[BrowsableAttribute(false)]
		public GroupListDocuments GroupListDocuments // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupListDocuments != value)
				{
					OnGroupListDocumentsChanging(_GroupListDocuments, value);
		            _GroupListDocuments = value;
					OnGroupListDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupListDocuments; }
		}
		private GroupListDocuments _GroupListDocuments;
		partial void OnGroupListDocumentsChanging(GroupListDocuments from, GroupListDocuments to); // Property.tt Line: 118
		partial void OnGroupListDocumentsChanged();
		[BrowsableAttribute(false)]
		public IGroupListDocuments IGroupListDocuments { get { return _GroupListDocuments; }}
	
		#endregion Properties
	}
	public partial class Document : ConfigObjectBase<Document, Document.DocumentValidator>, IComparable<Document>, IConfigAcceptVisitor, IDocument // Class.tt Line: 6
	{
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Document(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Document() : base(DocumentValidator.Validator)
		public Document(ITreeConfigNode parent) : base(parent, DocumentValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 29
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 29
			this.GroupForms = new GroupListForms(this); // Class.tt Line: 29
			this.GroupReports = new GroupListReports(this); // Class.tt Line: 29
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Document Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Document.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Document from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Document.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_document' to 'Document'
		public static Document ConvertToVM(Proto.Config.proto_document m, Document vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    if (vm.GroupProperties == null) // Clone.tt Line: 204
		        vm.GroupProperties = new GroupListProperties(vm); // Clone.tt Line: 206
		    GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties);
		    if (vm.GroupPropertiesTabs == null) // Clone.tt Line: 204
		        vm.GroupPropertiesTabs = new GroupListPropertiesTabs(vm); // Clone.tt Line: 206
		    GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs);
		    if (vm.GroupForms == null) // Clone.tt Line: 204
		        vm.GroupForms = new GroupListForms(vm); // Clone.tt Line: 206
		    GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms);
		    if (vm.GroupReports == null) // Clone.tt Line: 204
		        vm.GroupReports = new GroupListReports(vm); // Clone.tt Line: 206
		    GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports);
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Document' to 'proto_document'
		public static Proto.Config.proto_document ConvertToProto(Document vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_document m = new Proto.Config.proto_document(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 246
		    m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 246
		    m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 246
		    m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 246
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupForms.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupReports.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging(_GroupProperties, value);
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupListProperties _GroupProperties;
		partial void OnGroupPropertiesChanging(GroupListProperties from, GroupListProperties to); // Property.tt Line: 118
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public IGroupListProperties IGroupProperties { get { return _GroupProperties; }}
		
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupPropertiesTabs != value)
				{
					OnGroupPropertiesTabsChanging(_GroupPropertiesTabs, value);
		            _GroupPropertiesTabs = value;
					OnGroupPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesTabs; }
		}
		private GroupListPropertiesTabs _GroupPropertiesTabs;
		partial void OnGroupPropertiesTabsChanging(GroupListPropertiesTabs from, GroupListPropertiesTabs to); // Property.tt Line: 118
		partial void OnGroupPropertiesTabsChanged();
		[BrowsableAttribute(false)]
		public IGroupListPropertiesTabs IGroupPropertiesTabs { get { return _GroupPropertiesTabs; }}
		
		[BrowsableAttribute(false)]
		public GroupListForms GroupForms // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupForms != value)
				{
					OnGroupFormsChanging(_GroupForms, value);
		            _GroupForms = value;
					OnGroupFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupForms; }
		}
		private GroupListForms _GroupForms;
		partial void OnGroupFormsChanging(GroupListForms from, GroupListForms to); // Property.tt Line: 118
		partial void OnGroupFormsChanged();
		[BrowsableAttribute(false)]
		public IGroupListForms IGroupForms { get { return _GroupForms; }}
		
		[BrowsableAttribute(false)]
		public GroupListReports GroupReports // Property.tt Line: 100
		{ 
			set
			{
				if (_GroupReports != value)
				{
					OnGroupReportsChanging(_GroupReports, value);
		            _GroupReports = value;
					OnGroupReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupReports; }
		}
		private GroupListReports _GroupReports;
		partial void OnGroupReportsChanging(GroupListReports from, GroupListReports to); // Property.tt Line: 118
		partial void OnGroupReportsChanged();
		[BrowsableAttribute(false)]
		public IGroupListReports IGroupReports { get { return _GroupReports; }}
	
		#endregion Properties
	}
	public partial class GroupListDocuments : ConfigObjectBase<GroupListDocuments, GroupListDocuments.GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IConfigAcceptVisitor, IGroupListDocuments // Class.tt Line: 6
	{
		public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListDocuments(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListDocuments() : base(GroupListDocumentsValidator.Validator)
		public GroupListDocuments(ITreeConfigNode parent) : base(parent, GroupListDocumentsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListDocuments) // Clone.tt Line: 49
		        vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
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
		        foreach(var t in to.ListDocuments.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListDocuments)
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
		        foreach(var tt in from.ListDocuments)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListDocuments.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListDocuments Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListDocuments.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListDocuments from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListDocuments.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_documents' to 'GroupListDocuments'
		public static GroupListDocuments ConvertToVM(Proto.Config.proto_group_list_documents m, GroupListDocuments vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListDocuments) // Clone.tt Line: 189
		    {
		        var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 192
		        vm.ListDocuments.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
		public static Proto.Config.proto_group_list_documents ConvertToProto(GroupListDocuments vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_documents m = new Proto.Config.proto_group_list_documents(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListDocuments) // Clone.tt Line: 231
		        m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListDocuments)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Document> ListDocuments  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListDocuments != value)
				{
					OnListDocumentsChanging(_ListDocuments, value);
					_ListDocuments = value;
					OnListDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListDocuments; }
		}
		private ConfigNodesCollection<Document> _ListDocuments;
		partial void OnListDocumentsChanging(SortedObservableCollection<Document> from, SortedObservableCollection<Document> to); // Property.tt Line: 71
		partial void OnListDocumentsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IDocument> IListDocuments { get { foreach (var t in _ListDocuments) yield return t; } }
		public Document this[int index] { get { return (Document)this.ListDocuments[index]; } }
		public void Add(Document item)  // Property.tt Line: 78
		{ 
		    this.ListDocuments.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Document> items) 
		{ 
		    this.ListDocuments.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	public partial class GroupListJournals : ConfigObjectBase<GroupListJournals, GroupListJournals.GroupListJournalsValidator>, IComparable<GroupListJournals>, IConfigAcceptVisitor, IGroupListJournals // Class.tt Line: 6
	{
		public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListJournals(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListJournals() : base(GroupListJournalsValidator.Validator)
		public GroupListJournals(ITreeConfigNode parent) : base(parent, GroupListJournalsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListJournals = new ConfigNodesCollection<Journal>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListJournals) // Clone.tt Line: 49
		        vm.ListJournals.Add(Journal.Clone(vm, (Journal)t, isDeep));
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
		        foreach(var t in to.ListJournals.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListJournals)
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
		        foreach(var tt in from.ListJournals)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListJournals.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListJournals Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListJournals.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListJournals from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListJournals.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_journals' to 'GroupListJournals'
		public static GroupListJournals ConvertToVM(Proto.Config.proto_group_list_journals m, GroupListJournals vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListJournals = new ConfigNodesCollection<Journal>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListJournals) // Clone.tt Line: 189
		    {
		        var tvm = Journal.ConvertToVM(t, new Journal(vm)); // Clone.tt Line: 192
		        vm.ListJournals.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListJournals' to 'proto_group_list_journals'
		public static Proto.Config.proto_group_list_journals ConvertToProto(GroupListJournals vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_journals m = new Proto.Config.proto_group_list_journals(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListJournals) // Clone.tt Line: 231
		        m.ListJournals.Add(Journal.ConvertToProto((Journal)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListJournals)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Journal> ListJournals  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListJournals != value)
				{
					OnListJournalsChanging(_ListJournals, value);
					_ListJournals = value;
					OnListJournalsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListJournals; }
		}
		private ConfigNodesCollection<Journal> _ListJournals;
		partial void OnListJournalsChanging(SortedObservableCollection<Journal> from, SortedObservableCollection<Journal> to); // Property.tt Line: 71
		partial void OnListJournalsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IJournal> IListJournals { get { foreach (var t in _ListJournals) yield return t; } }
		public Journal this[int index] { get { return (Journal)this.ListJournals[index]; } }
		public void Add(Journal item)  // Property.tt Line: 78
		{ 
		    this.ListJournals.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Journal> items) 
		{ 
		    this.ListJournals.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	public partial class Journal : ConfigObjectBase<Journal, Journal.JournalValidator>, IComparable<Journal>, IConfigAcceptVisitor, IJournal // Class.tt Line: 6
	{
		public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Journal(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Journal() : base(JournalValidator.Validator)
		public Journal(ITreeConfigNode parent) : base(parent, JournalValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListDocuments = new ConfigNodesCollection<Document>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListDocuments) // Clone.tt Line: 49
		        vm.ListDocuments.Add(Document.Clone(vm, (Document)t, isDeep));
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
		        foreach(var t in to.ListDocuments.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListDocuments)
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
		        foreach(var tt in from.ListDocuments)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListDocuments.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Journal Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Journal.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Journal from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Journal.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_journal' to 'Journal'
		public static Journal ConvertToVM(Proto.Config.proto_journal m, Journal vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListDocuments = new ConfigNodesCollection<Document>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListDocuments) // Clone.tt Line: 189
		    {
		        var tvm = Document.ConvertToVM(t, new Document(vm)); // Clone.tt Line: 192
		        vm.ListDocuments.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Journal' to 'proto_journal'
		public static Proto.Config.proto_journal ConvertToProto(Journal vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_journal m = new Proto.Config.proto_journal(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListDocuments) // Clone.tt Line: 231
		        m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListDocuments)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Document> ListDocuments  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListDocuments != value)
				{
					OnListDocumentsChanging(_ListDocuments, value);
					_ListDocuments = value;
					OnListDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListDocuments; }
		}
		private ConfigNodesCollection<Document> _ListDocuments;
		partial void OnListDocumentsChanging(SortedObservableCollection<Document> from, SortedObservableCollection<Document> to); // Property.tt Line: 71
		partial void OnListDocumentsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IDocument> IListDocuments { get { foreach (var t in _ListDocuments) yield return t; } }
	
		#endregion Properties
	}
	public partial class GroupListForms : ConfigObjectBase<GroupListForms, GroupListForms.GroupListFormsValidator>, IComparable<GroupListForms>, IConfigAcceptVisitor, IGroupListForms // Class.tt Line: 6
	{
		public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListForms(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListForms() : base(GroupListFormsValidator.Validator)
		public GroupListForms(ITreeConfigNode parent) : base(parent, GroupListFormsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListForms = new ConfigNodesCollection<Form>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListForms) // Clone.tt Line: 49
		        vm.ListForms.Add(Form.Clone(vm, (Form)t, isDeep));
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
		        foreach(var t in to.ListForms.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListForms)
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
		        foreach(var tt in from.ListForms)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListForms.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListForms Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListForms.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListForms from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListForms.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_forms' to 'GroupListForms'
		public static GroupListForms ConvertToVM(Proto.Config.proto_group_list_forms m, GroupListForms vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListForms = new ConfigNodesCollection<Form>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListForms) // Clone.tt Line: 189
		    {
		        var tvm = Form.ConvertToVM(t, new Form(vm)); // Clone.tt Line: 192
		        vm.ListForms.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListForms' to 'proto_group_list_forms'
		public static Proto.Config.proto_group_list_forms ConvertToProto(GroupListForms vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_forms m = new Proto.Config.proto_group_list_forms(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListForms) // Clone.tt Line: 231
		        m.ListForms.Add(Form.ConvertToProto((Form)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListForms)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Form> ListForms  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListForms != value)
				{
					OnListFormsChanging(_ListForms, value);
					_ListForms = value;
					OnListFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListForms; }
		}
		private ConfigNodesCollection<Form> _ListForms;
		partial void OnListFormsChanging(SortedObservableCollection<Form> from, SortedObservableCollection<Form> to); // Property.tt Line: 71
		partial void OnListFormsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IForm> IListForms { get { foreach (var t in _ListForms) yield return t; } }
		public Form this[int index] { get { return (Form)this.ListForms[index]; } }
		public void Add(Form item)  // Property.tt Line: 78
		{ 
		    this.ListForms.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Form> items) 
		{ 
		    this.ListForms.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	public partial class Form : ConfigObjectBase<Form, Form.FormValidator>, IComparable<Form>, IConfigAcceptVisitor, IForm // Class.tt Line: 6
	{
		public partial class FormValidator : ValidatorBase<Form, FormValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Form(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Form() : base(FormValidator.Validator)
		public Form(ITreeConfigNode parent) : base(parent, FormValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Form Clone(ITreeConfigNode parent, Form from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Form vm = new Form(parent);
		    vm.Guid = from.Guid; // Clone.tt Line: 62
		    vm.Name = from.Name; // Clone.tt Line: 62
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
		    vm.NameUi = from.NameUi; // Clone.tt Line: 62
		    vm.Description = from.Description; // Clone.tt Line: 62
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Form Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Form.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Form from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Form.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_form' to 'Form'
		public static Form ConvertToVM(Proto.Config.proto_form m, Form vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Form' to 'proto_form'
		public static Proto.Config.proto_form ConvertToProto(Form vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_form m = new Proto.Config.proto_form(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_forms = 7;
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
	
		#endregion Properties
	}
	public partial class GroupListReports : ConfigObjectBase<GroupListReports, GroupListReports.GroupListReportsValidator>, IComparable<GroupListReports>, IConfigAcceptVisitor, IGroupListReports // Class.tt Line: 6
	{
		public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { } // Class.tt Line: 8
		#region CTOR
		//public GroupListReports(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public GroupListReports() : base(GroupListReportsValidator.Validator)
		public GroupListReports(ITreeConfigNode parent) : base(parent, GroupListReportsValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			this.ListReports = new ConfigNodesCollection<Report>(this); // Class.tt Line: 23
			OnInit();
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
		    foreach(var t in from.ListReports) // Clone.tt Line: 49
		        vm.ListReports.Add(Report.Clone(vm, (Report)t, isDeep));
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
		        foreach(var t in to.ListReports.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListReports)
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
		        foreach(var tt in from.ListReports)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListReports.ToList())
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override GroupListReports Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListReports.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListReports from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListReports.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_reports' to 'GroupListReports'
		public static GroupListReports ConvertToVM(Proto.Config.proto_group_list_reports m, GroupListReports vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.ListReports = new ConfigNodesCollection<Report>(vm); // Clone.tt Line: 188
		    foreach(var t in m.ListReports) // Clone.tt Line: 189
		    {
		        var tvm = Report.ConvertToVM(t, new Report(vm)); // Clone.tt Line: 192
		        vm.ListReports.Add(tvm);
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'GroupListReports' to 'proto_group_list_reports'
		public static Proto.Config.proto_group_list_reports ConvertToProto(GroupListReports vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_group_list_reports m = new Proto.Config.proto_group_list_reports(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    foreach(var t in vm.ListReports) // Clone.tt Line: 231
		        m.ListReports.Add(Report.ConvertToProto((Report)t)); // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListReports)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
		
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public ConfigNodesCollection<Report> ListReports  // Property.tt Line: 53
		{ 
			set
			{
				if (_ListReports != value)
				{
					OnListReportsChanging(_ListReports, value);
					_ListReports = value;
					OnListReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListReports; }
		}
		private ConfigNodesCollection<Report> _ListReports;
		partial void OnListReportsChanging(SortedObservableCollection<Report> from, SortedObservableCollection<Report> to); // Property.tt Line: 71
		partial void OnListReportsChanged();
		[BrowsableAttribute(false)]
		public IEnumerable<IReport> IListReports { get { foreach (var t in _ListReports) yield return t; } }
		public Report this[int index] { get { return (Report)this.ListReports[index]; } }
		public void Add(Report item)  // Property.tt Line: 78
		{ 
		    this.ListReports.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Report> items) 
		{ 
		    this.ListReports.AddRange(items); 
		    foreach(var t in items)
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
	
		#endregion Properties
	}
	public partial class Report : ConfigObjectBase<Report, Report.ReportValidator>, IComparable<Report>, IConfigAcceptVisitor, IReport // Class.tt Line: 6
	{
		public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { } // Class.tt Line: 8
		#region CTOR
		//public Report(ITreeConfigNode parent) : this() { this.Parent = parent; } // Class.tt Line: 11
		//public Report() : base(ReportValidator.Validator)
		public Report(ITreeConfigNode parent) : base(parent, ReportValidator.Validator) // Class.tt Line: 13
	    {
			OnInitBegin();
			OnInit();
	    }
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Report Clone(ITreeConfigNode parent, Report from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Report vm = new Report(parent);
		    vm.Guid = from.Guid; // Clone.tt Line: 62
		    vm.Name = from.Name; // Clone.tt Line: 62
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 62
		    vm.NameUi = from.NameUi; // Clone.tt Line: 62
		    vm.Description = from.Description; // Clone.tt Line: 62
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
		}
		// Clone.tt Line: 140
		#region IEditable
		public override Report Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Report.Clone(this.Parent, this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(Report from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    Report.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_report' to 'Report'
		public static Report ConvertToVM(Proto.Config.proto_report m, Report vm) // Clone.tt Line: 163
		{
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 216
		    vm.NameUi = m.NameUi; // Clone.tt Line: 216
		    vm.Description = m.Description; // Clone.tt Line: 216
		    vm.OnInitFromDto(); // Clone.tt Line: 221
		    return vm;
		}
		// Conversion from 'Report' to 'proto_report'
		public static Proto.Config.proto_report ConvertToProto(Report vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_report m = new Proto.Config.proto_report(); // Clone.tt Line: 228
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 252
		    m.NameUi = vm.NameUi; // Clone.tt Line: 252
		    m.Description = vm.Description; // Clone.tt Line: 252
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		///////////////////////////////////////////////////
		/// 
		/// repeated proto_group_properties list_properties = 6;
		/// repeated proto_document list_documents = 7;
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(3)]
		public string Description // Property.tt Line: 123
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging(_Description, value);
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(string from, string to); // Property.tt Line: 141
		partial void OnDescriptionChanged();
	
		#endregion Properties
	}
	public partial class ModelRow : ViewModelBindable<ModelRow>, IModelRow // Class.tt Line: 6
	{
		public partial class ModelRowValidator : ValidatorBase<ModelRow, ModelRowValidator> { } // Class.tt Line: 8
		#region CTOR
		public ModelRow() // Class.tt Line: 38
		{
			OnInitBegin();
			OnInit();
		}
		partial void OnInitBegin();
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static ModelRow Clone(ITreeConfigNode parent, ModelRow from, bool isDeep = true) // Clone.tt Line: 27
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
		        return vm;
		    vm.GroupName = m.GroupName; // Clone.tt Line: 216
		    vm.Name = m.Name; // Clone.tt Line: 216
		    vm.Guid = m.Guid; // Clone.tt Line: 216
		    vm.IsIncluded = m.IsIncluded; // Clone.tt Line: 216
		    return vm;
		}
		// Conversion from 'ModelRow' to 'proto_model_row'
		public static Proto.Config.proto_model_row ConvertToProto(ModelRow vm) // Clone.tt Line: 226
		{
		    Proto.Config.proto_model_row m = new Proto.Config.proto_model_row(); // Clone.tt Line: 228
		    m.GroupName = vm.GroupName; // Clone.tt Line: 252
		    m.Name = vm.Name; // Clone.tt Line: 252
		    m.Guid = vm.Guid; // Clone.tt Line: 252
		    m.IsIncluded = vm.IsIncluded; // Clone.tt Line: 252
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		public string GroupName // Property.tt Line: 123
		{ 
			set
			{
				if (_GroupName != value)
				{
					OnGroupNameChanging(_GroupName, value);
					_GroupName = value;
					OnGroupNameChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _GroupName; }
		}
		private string _GroupName = "";
		partial void OnGroupNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnGroupNameChanged();
		
		public string Name // Property.tt Line: 123
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging(_Name, value);
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging(string from, string to); // Property.tt Line: 141
		partial void OnNameChanged();
		
		public string Guid // Property.tt Line: 123
		{ 
			set
			{
				if (_Guid != value)
				{
					OnGuidChanging(_Guid, value);
					_Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _Guid; }
		}
		private string _Guid = "";
		partial void OnGuidChanging(string from, string to); // Property.tt Line: 141
		partial void OnGuidChanged();
		
		public bool IsIncluded // Property.tt Line: 123
		{ 
			set
			{
				if (_IsIncluded != value)
				{
					OnIsIncludedChanging(_IsIncluded, value);
					_IsIncluded = value;
					OnIsIncludedChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _IsIncluded; }
		}
		private bool _IsIncluded;
		partial void OnIsIncludedChanging(bool from, bool to); // Property.tt Line: 141
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
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListPlugins p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Plugin p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Plugin p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(PluginGenerator p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(PluginGenerator p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(PluginGeneratorSettings p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(PluginGeneratorSettings p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(SettingsConfig p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(SettingsConfig p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(DbSettings p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(DbSettings p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(ConfigShortHistory p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(ConfigShortHistory p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListBaseConfigLinks p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListBaseConfigLinks p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(BaseConfigLink p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(BaseConfigLink p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Config p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	        ValidateSubAndCollectErrors(p, p.DbSettings); // ValidationVisitor.tt Line: 29
	    }
		protected override void OnVisitEnd(Config p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(AppDbSettings p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(AppDbSettings p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListAppSolutions p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	        ValidateSubAndCollectErrors(p, p.DefaultDb); // ValidationVisitor.tt Line: 29
	    }
		protected override void OnVisitEnd(GroupListAppSolutions p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(AppSolution p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	        ValidateSubAndCollectErrors(p, p.DefaultDb); // ValidationVisitor.tt Line: 29
	    }
		protected override void OnVisitEnd(AppSolution p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(AppProject p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	        ValidateSubAndCollectErrors(p, p.DefaultDb); // ValidationVisitor.tt Line: 29
	    }
		protected override void OnVisitEnd(AppProject p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(ConfigModel p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(ConfigModel p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(DataType p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(DataType p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListCommon p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListCommon p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Role p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Role p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListRoles p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListRoles p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(MainViewForm p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(MainViewForm p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListMainViewForms p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListMainViewForms p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListPropertiesTabs p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListPropertiesTabs p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(PropertiesTab p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(PropertiesTab p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListProperties p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListProperties p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Property p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	        ValidateSubAndCollectErrors(p, p.DataType); // ValidationVisitor.tt Line: 29
	    }
		protected override void OnVisitEnd(Property p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListConstants p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListConstants p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Constant p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	        ValidateSubAndCollectErrors(p, p.DataType); // ValidationVisitor.tt Line: 29
	    }
		protected override void OnVisitEnd(Constant p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListEnumerations p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListEnumerations p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Enumeration p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Enumeration p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(EnumerationPair p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(EnumerationPair p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Catalog p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Catalog p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListCatalogs p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListCatalogs p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupDocuments p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupDocuments p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Document p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Document p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListDocuments p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListDocuments p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListJournals p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListJournals p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Journal p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Journal p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListForms p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListForms p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Form p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Form p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListReports p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListReports p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(Report p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(Report p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(ModelRow p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(ModelRow p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
	}
	
	public partial class ConfigVisitor : IVisitorConfigNode // NodeVisitor.tt Line: 7
	{
	    public CancellationToken Token { get { return _cancellationToken; } }
	    protected CancellationToken _cancellationToken;
	
		public void Visit(GroupListPlugins p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListPlugins p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListPlugins p) {}
	    protected virtual void OnVisitEnd(GroupListPlugins p) {}
		public void Visit(Plugin p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Plugin p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Plugin p) {}
	    protected virtual void OnVisitEnd(Plugin p) {}
		public void Visit(PluginGenerator p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(PluginGenerator p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(PluginGenerator p) {}
	    protected virtual void OnVisitEnd(PluginGenerator p) {}
		public void Visit(PluginGeneratorSettings p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(PluginGeneratorSettings p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(PluginGeneratorSettings p) {}
	    protected virtual void OnVisitEnd(PluginGeneratorSettings p) {}
		public void Visit(SettingsConfig p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(SettingsConfig p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(SettingsConfig p) {}
	    protected virtual void OnVisitEnd(SettingsConfig p) {}
		public void Visit(DbSettings p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(DbSettings p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(DbSettings p) {}
	    protected virtual void OnVisitEnd(DbSettings p) {}
		public void Visit(ConfigShortHistory p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ConfigShortHistory p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(ConfigShortHistory p) {}
	    protected virtual void OnVisitEnd(ConfigShortHistory p) {}
		public void Visit(GroupListBaseConfigLinks p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListBaseConfigLinks p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListBaseConfigLinks p) {}
	    protected virtual void OnVisitEnd(GroupListBaseConfigLinks p) {}
		public void Visit(BaseConfigLink p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(BaseConfigLink p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(BaseConfigLink p) {}
	    protected virtual void OnVisitEnd(BaseConfigLink p) {}
		public void Visit(Config p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Config p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Config p) {}
	    protected virtual void OnVisitEnd(Config p) {}
		public void Visit(AppDbSettings p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(AppDbSettings p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(AppDbSettings p) {}
	    protected virtual void OnVisitEnd(AppDbSettings p) {}
		public void Visit(GroupListAppSolutions p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListAppSolutions p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListAppSolutions p) {}
	    protected virtual void OnVisitEnd(GroupListAppSolutions p) {}
		public void Visit(AppSolution p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(AppSolution p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(AppSolution p) {}
	    protected virtual void OnVisitEnd(AppSolution p) {}
		public void Visit(AppProject p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(AppProject p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(AppProject p) {}
	    protected virtual void OnVisitEnd(AppProject p) {}
		public void Visit(ConfigModel p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ConfigModel p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(ConfigModel p) {}
	    protected virtual void OnVisitEnd(ConfigModel p) {}
		public void Visit(DataType p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(DataType p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(DataType p) {}
	    protected virtual void OnVisitEnd(DataType p) {}
		public void Visit(GroupListCommon p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListCommon p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListCommon p) {}
	    protected virtual void OnVisitEnd(GroupListCommon p) {}
		public void Visit(Role p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Role p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Role p) {}
	    protected virtual void OnVisitEnd(Role p) {}
		public void Visit(GroupListRoles p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListRoles p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListRoles p) {}
	    protected virtual void OnVisitEnd(GroupListRoles p) {}
		public void Visit(MainViewForm p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(MainViewForm p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(MainViewForm p) {}
	    protected virtual void OnVisitEnd(MainViewForm p) {}
		public void Visit(GroupListMainViewForms p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListMainViewForms p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListMainViewForms p) {}
	    protected virtual void OnVisitEnd(GroupListMainViewForms p) {}
		public void Visit(GroupListPropertiesTabs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListPropertiesTabs p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListPropertiesTabs p) {}
	    protected virtual void OnVisitEnd(GroupListPropertiesTabs p) {}
		public void Visit(PropertiesTab p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(PropertiesTab p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(PropertiesTab p) {}
	    protected virtual void OnVisitEnd(PropertiesTab p) {}
		public void Visit(GroupListProperties p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListProperties p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListProperties p) {}
	    protected virtual void OnVisitEnd(GroupListProperties p) {}
		public void Visit(Property p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Property p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Property p) {}
	    protected virtual void OnVisitEnd(Property p) {}
		public void Visit(GroupListConstants p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListConstants p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListConstants p) {}
	    protected virtual void OnVisitEnd(GroupListConstants p) {}
		public void Visit(Constant p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Constant p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Constant p) {}
	    protected virtual void OnVisitEnd(Constant p) {}
		public void Visit(GroupListEnumerations p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListEnumerations p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListEnumerations p) {}
	    protected virtual void OnVisitEnd(GroupListEnumerations p) {}
		public void Visit(Enumeration p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Enumeration p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Enumeration p) {}
	    protected virtual void OnVisitEnd(Enumeration p) {}
		public void Visit(EnumerationPair p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(EnumerationPair p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(EnumerationPair p) {}
	    protected virtual void OnVisitEnd(EnumerationPair p) {}
		public void Visit(Catalog p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Catalog p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Catalog p) {}
	    protected virtual void OnVisitEnd(Catalog p) {}
		public void Visit(GroupListCatalogs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListCatalogs p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListCatalogs p) {}
	    protected virtual void OnVisitEnd(GroupListCatalogs p) {}
		public void Visit(GroupDocuments p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupDocuments p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupDocuments p) {}
	    protected virtual void OnVisitEnd(GroupDocuments p) {}
		public void Visit(Document p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Document p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Document p) {}
	    protected virtual void OnVisitEnd(Document p) {}
		public void Visit(GroupListDocuments p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListDocuments p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListDocuments p) {}
	    protected virtual void OnVisitEnd(GroupListDocuments p) {}
		public void Visit(GroupListJournals p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListJournals p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListJournals p) {}
	    protected virtual void OnVisitEnd(GroupListJournals p) {}
		public void Visit(Journal p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Journal p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Journal p) {}
	    protected virtual void OnVisitEnd(Journal p) {}
		public void Visit(GroupListForms p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListForms p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListForms p) {}
	    protected virtual void OnVisitEnd(GroupListForms p) {}
		public void Visit(Form p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Form p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Form p) {}
	    protected virtual void OnVisitEnd(Form p) {}
		public void Visit(GroupListReports p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListReports p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListReports p) {}
	    protected virtual void OnVisitEnd(GroupListReports p) {}
		public void Visit(Report p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Report p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(Report p) {}
	    protected virtual void OnVisitEnd(Report p) {}
		public void Visit(ModelRow p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ModelRow p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(ModelRow p) {}
	    protected virtual void OnVisitEnd(ModelRow p) {}
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