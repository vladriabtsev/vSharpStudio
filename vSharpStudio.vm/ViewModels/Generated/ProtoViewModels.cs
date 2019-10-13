// Auto generated on UTC 10/13/2019 02:32:55
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
		public partial class GroupListPluginsValidator : ValidatorBase<GroupListPlugins, GroupListPluginsValidator> { } 
		#region CTOR
		public GroupListPlugins() : base(GroupListPluginsValidator.Validator)	{
			this.ListPlugins = new SortedObservableCollection<Plugin>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListPlugins(ITreeConfigNode parent) : base(GroupListPluginsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListPlugins = new SortedObservableCollection<Plugin>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListPlugins Clone(GroupListPlugins from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListPlugins vm = new GroupListPlugins();
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.ListPlugins = new SortedObservableCollection<Plugin>();
		    foreach(var t in from.ListPlugins) // Clone.tt Line: 45
		        vm.ListPlugins.Add(Plugin.Clone((Plugin)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListPlugins to, GroupListPlugins from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Plugin();
		                Plugin.Update(p, (Plugin)tt, isDeep);
		                to.ListPlugins.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListPlugins Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListPlugins.Clone(this);
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
		public static GroupListPlugins ConvertToVM(Proto.Config.proto_group_list_plugins m, GroupListPlugins vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListPlugins();
		    if (m == null)
		        return vm;
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.ListPlugins = new SortedObservableCollection<Plugin>();
		    foreach(var t in m.ListPlugins) // Clone.tt Line: 179
		    {
		        var tvm = Plugin.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListPlugins.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListPlugins' to 'proto_group_list_plugins'
		public static Proto.Config.proto_group_list_plugins ConvertToProto(GroupListPlugins vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_plugins m = new Proto.Config.proto_group_list_plugins(); // Clone.tt Line: 211
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    foreach(var t in vm.ListPlugins) // Clone.tt Line: 214
		        m.ListPlugins.Add(Plugin.ConvertToProto((Plugin)t)); // Clone.tt Line: 218
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
		public SortedObservableCollection<Plugin> ListPlugins  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListPlugins != value)
				{
					OnListPluginsChanging();
					_ListPlugins = value;
					OnListPluginsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPlugins; }
		}
		private SortedObservableCollection<Plugin> _ListPlugins;
		[BrowsableAttribute(false)]
		public IEnumerable<IPlugin> ListPluginsI { get { foreach (var t in _ListPlugins) yield return t; } }
		public Plugin this[int index] { get { return (Plugin)this.ListPlugins[index]; } }
		public void Add(Plugin item)  // Property.tt Line: 72
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
		partial void OnListPluginsChanging(); // Property.tt Line: 134
		partial void OnListPluginsChanged();
		#endregion Properties
	}
	public partial class Plugin : ConfigObjectBase<Plugin, Plugin.PluginValidator>, IComparable<Plugin>, IConfigAcceptVisitor, IPlugin // Class.tt Line: 6
	{
		public partial class PluginValidator : ValidatorBase<Plugin, PluginValidator> { } 
		#region CTOR
		public Plugin() : base(PluginValidator.Validator)	{
			this.ListGenerators = new SortedObservableCollection<PluginGenerator>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public Plugin(ITreeConfigNode parent) : base(PluginValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListGenerators = new SortedObservableCollection<PluginGenerator>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static Plugin Clone(Plugin from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Plugin vm = new Plugin();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Version = from.Version; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.ListGenerators = new SortedObservableCollection<PluginGenerator>();
		    foreach(var t in from.ListGenerators) // Clone.tt Line: 45
		        vm.ListGenerators.Add(PluginGenerator.Clone((PluginGenerator)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Plugin to, Plugin from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Version = from.Version; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new PluginGenerator();
		                PluginGenerator.Update(p, (PluginGenerator)tt, isDeep);
		                to.ListGenerators.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Plugin Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Plugin.Clone(this);
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
		public static Plugin ConvertToVM(Proto.Config.proto_plugin m, Plugin vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Plugin();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Version = m.Version; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.ListGenerators = new SortedObservableCollection<PluginGenerator>();
		    foreach(var t in m.ListGenerators) // Clone.tt Line: 179
		    {
		        var tvm = PluginGenerator.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListGenerators.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Plugin' to 'proto_plugin'
		public static Proto.Config.proto_plugin ConvertToProto(Plugin vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_plugin m = new Proto.Config.proto_plugin(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Version = vm.Version; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    foreach(var t in vm.ListGenerators) // Clone.tt Line: 214
		        m.ListGenerators.Add(PluginGenerator.ConvertToProto((PluginGenerator)t)); // Clone.tt Line: 218
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
		public string Version // Property.tt Line: 115
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging();
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private string _Version = "";
		partial void OnVersionChanging(); // Property.tt Line: 134
		partial void OnVersionChanged();
		[Editable(false)]
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<PluginGenerator> ListGenerators  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListGenerators != value)
				{
					OnListGeneratorsChanging();
					_ListGenerators = value;
					OnListGeneratorsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListGenerators; }
		}
		private SortedObservableCollection<PluginGenerator> _ListGenerators;
		[BrowsableAttribute(false)]
		public IEnumerable<IPluginGenerator> ListGeneratorsI { get { foreach (var t in _ListGenerators) yield return t; } }
		partial void OnListGeneratorsChanging(); // Property.tt Line: 134
		partial void OnListGeneratorsChanged();
		#endregion Properties
	}
	public partial class PluginGenerator : ConfigObjectBase<PluginGenerator, PluginGenerator.PluginGeneratorValidator>, IComparable<PluginGenerator>, IConfigAcceptVisitor, IPluginGenerator // Class.tt Line: 6
	{
		public partial class PluginGeneratorValidator : ValidatorBase<PluginGenerator, PluginGeneratorValidator> { } 
		#region CTOR
		public PluginGenerator() : base(PluginGeneratorValidator.Validator)	{
			this.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public PluginGenerator(ITreeConfigNode parent) : base(PluginGeneratorValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static PluginGenerator Clone(PluginGenerator from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    PluginGenerator vm = new PluginGenerator();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>();
		    foreach(var t in from.ListSettings) // Clone.tt Line: 45
		        vm.ListSettings.Add(PluginGeneratorSettings.Clone((PluginGeneratorSettings)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(PluginGenerator to, PluginGenerator from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new PluginGeneratorSettings();
		                PluginGeneratorSettings.Update(p, (PluginGeneratorSettings)tt, isDeep);
		                to.ListSettings.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override PluginGenerator Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return PluginGenerator.Clone(this);
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
		public static PluginGenerator ConvertToVM(Proto.Config.proto_plugin_generator m, PluginGenerator vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new PluginGenerator();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>();
		    foreach(var t in m.ListSettings) // Clone.tt Line: 179
		    {
		        var tvm = PluginGeneratorSettings.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListSettings.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'PluginGenerator' to 'proto_plugin_generator'
		public static Proto.Config.proto_plugin_generator ConvertToProto(PluginGenerator vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_plugin_generator m = new Proto.Config.proto_plugin_generator(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    foreach(var t in vm.ListSettings) // Clone.tt Line: 214
		        m.ListSettings.Add(PluginGeneratorSettings.ConvertToProto((PluginGeneratorSettings)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<PluginGeneratorSettings> ListSettings  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListSettings != value)
				{
					OnListSettingsChanging();
					_ListSettings = value;
					OnListSettingsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListSettings; }
		}
		private SortedObservableCollection<PluginGeneratorSettings> _ListSettings;
		[BrowsableAttribute(false)]
		public IEnumerable<IPluginGeneratorSettings> ListSettingsI { get { foreach (var t in _ListSettings) yield return t; } }
		partial void OnListSettingsChanging(); // Property.tt Line: 134
		partial void OnListSettingsChanged();
		#endregion Properties
	}
	public partial class PluginGeneratorSettings : ConfigObjectBase<PluginGeneratorSettings, PluginGeneratorSettings.PluginGeneratorSettingsValidator>, IComparable<PluginGeneratorSettings>, IConfigAcceptVisitor, IPluginGeneratorSettings // Class.tt Line: 6
	{
		public partial class PluginGeneratorSettingsValidator : ValidatorBase<PluginGeneratorSettings, PluginGeneratorSettingsValidator> { } 
		#region CTOR
		public PluginGeneratorSettings() : base(PluginGeneratorSettingsValidator.Validator)	{
			OnInit();
		}
	    // Class.tt Line: 34
		public PluginGeneratorSettings(ITreeConfigNode parent) : base(PluginGeneratorSettingsValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static PluginGeneratorSettings Clone(PluginGeneratorSettings from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    PluginGeneratorSettings vm = new PluginGeneratorSettings();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 58
		    vm.IsPrivate = from.IsPrivate; // Clone.tt Line: 58
		    vm.FilePath = from.FilePath; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(PluginGeneratorSettings to, PluginGeneratorSettings from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.GeneratorSettings = from.GeneratorSettings; // Clone.tt Line: 126
		    to.IsPrivate = from.IsPrivate; // Clone.tt Line: 126
		    to.FilePath = from.FilePath; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override PluginGeneratorSettings Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return PluginGeneratorSettings.Clone(this);
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
		public static PluginGeneratorSettings ConvertToVM(Proto.Config.proto_plugin_generator_settings m, PluginGeneratorSettings vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new PluginGeneratorSettings();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.GeneratorSettings = m.GeneratorSettings; // Clone.tt Line: 199
		    vm.IsPrivate = m.IsPrivate; // Clone.tt Line: 199
		    vm.FilePath = m.FilePath; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'PluginGeneratorSettings' to 'proto_plugin_generator_settings'
		public static Proto.Config.proto_plugin_generator_settings ConvertToProto(PluginGeneratorSettings vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_plugin_generator_settings m = new Proto.Config.proto_plugin_generator_settings(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.GeneratorSettings = vm.GeneratorSettings; // Clone.tt Line: 235
		    m.IsPrivate = vm.IsPrivate; // Clone.tt Line: 235
		    m.FilePath = vm.FilePath; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public string GeneratorSettings // Property.tt Line: 115
		{ 
			set
			{
				if (_GeneratorSettings != value)
				{
					OnGeneratorSettingsChanging();
					_GeneratorSettings = value;
					OnGeneratorSettingsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GeneratorSettings; }
		}
		private string _GeneratorSettings = "";
		partial void OnGeneratorSettingsChanging(); // Property.tt Line: 134
		partial void OnGeneratorSettingsChanged();
		[PropertyOrderAttribute(3)]
		[Description("If false, connection string settings will be stored in configuration file. If true, will be stored in separate file.")]
		public bool IsPrivate // Property.tt Line: 115
		{ 
			set
			{
				if (_IsPrivate != value)
				{
					OnIsPrivateChanging();
					_IsPrivate = value;
					OnIsPrivateChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPrivate; }
		}
		private bool _IsPrivate;
		partial void OnIsPrivateChanging(); // Property.tt Line: 134
		partial void OnIsPrivateChanged();
		[PropertyOrderAttribute(4)]
		[Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
		[Description("File path to store connection string settings in private place.")]
		public string FilePath // Property.tt Line: 115
		{ 
			set
			{
				if (_FilePath != value)
				{
					OnFilePathChanging();
					_FilePath = value;
					OnFilePathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _FilePath; }
		}
		private string _FilePath = "";
		partial void OnFilePathChanging(); // Property.tt Line: 134
		partial void OnFilePathChanged();
		#endregion Properties
	}
	public partial class SettingsConfig : ConfigObjectBase<SettingsConfig, SettingsConfig.SettingsConfigValidator>, IComparable<SettingsConfig>, IConfigAcceptVisitor, ISettingsConfig // Class.tt Line: 6
	{
		public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { } 
		#region CTOR
		public SettingsConfig() : base(SettingsConfigValidator.Validator)	{
			OnInit();
		}
	    // Class.tt Line: 34
		public SettingsConfig(ITreeConfigNode parent) : base(SettingsConfigValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static SettingsConfig Clone(SettingsConfig from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    SettingsConfig vm = new SettingsConfig();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 58
		    vm.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(SettingsConfig to, SettingsConfig from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.VersionMigrationCurrent = from.VersionMigrationCurrent; // Clone.tt Line: 126
		    to.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override SettingsConfig Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return SettingsConfig.Clone(this);
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
		public static SettingsConfig ConvertToVM(Proto.Config.proto_settings_config m, SettingsConfig vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new SettingsConfig();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.VersionMigrationCurrent = m.VersionMigrationCurrent; // Clone.tt Line: 199
		    vm.VersionMigrationSupportFromMin = m.VersionMigrationSupportFromMin; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'SettingsConfig' to 'proto_settings_config'
		public static Proto.Config.proto_settings_config ConvertToProto(SettingsConfig vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_settings_config m = new Proto.Config.proto_settings_config(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.VersionMigrationCurrent = vm.VersionMigrationCurrent; // Clone.tt Line: 235
		    m.VersionMigrationSupportFromMin = vm.VersionMigrationSupportFromMin; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// current migration version, increased by one on each deployment
		///////////////////////////////////////////////////
		public int VersionMigrationCurrent // Property.tt Line: 115
		{ 
			set
			{
				if (_VersionMigrationCurrent != value)
				{
					OnVersionMigrationCurrentChanging();
					_VersionMigrationCurrent = value;
					OnVersionMigrationCurrentChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _VersionMigrationCurrent; }
		}
		private int _VersionMigrationCurrent;
		partial void OnVersionMigrationCurrentChanging(); // Property.tt Line: 134
		partial void OnVersionMigrationCurrentChanged();
		
		///////////////////////////////////////////////////
		/// min version supported by current version for migration
		///////////////////////////////////////////////////
		public int VersionMigrationSupportFromMin // Property.tt Line: 115
		{ 
			set
			{
				if (_VersionMigrationSupportFromMin != value)
				{
					OnVersionMigrationSupportFromMinChanging();
					_VersionMigrationSupportFromMin = value;
					OnVersionMigrationSupportFromMinChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _VersionMigrationSupportFromMin; }
		}
		private int _VersionMigrationSupportFromMin;
		partial void OnVersionMigrationSupportFromMinChanging(); // Property.tt Line: 134
		partial void OnVersionMigrationSupportFromMinChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// General DB settings
	///////////////////////////////////////////////////
	public partial class DbSettings : ViewModelValidatableWithSeverity<DbSettings, DbSettings.DbSettingsValidator>, IDbSettings // Class.tt Line: 6
	{
		public partial class DbSettingsValidator : ValidatorBase<DbSettings, DbSettingsValidator> { } 
		#region CTOR
		public DbSettings() : base(DbSettingsValidator.Validator)	{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DbSettings Clone(DbSettings from, bool isDeep = true) // Clone.tt Line: 27
		{
		    DbSettings vm = new DbSettings();
		    vm.DbSchema = from.DbSchema; // Clone.tt Line: 58
		    vm.IdGenerator = from.IdGenerator; // Clone.tt Line: 58
		    vm.KeyType = from.KeyType; // Clone.tt Line: 58
		    vm.KeyName = from.KeyName; // Clone.tt Line: 58
		    vm.Timestamp = from.Timestamp; // Clone.tt Line: 58
		    vm.IsDbFromConnectionString = from.IsDbFromConnectionString; // Clone.tt Line: 58
		    vm.ConnectionStringName = from.ConnectionStringName; // Clone.tt Line: 58
		    vm.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString; // Clone.tt Line: 58
		    return vm;
		}
		public static void Update(DbSettings to, DbSettings from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.DbSchema = from.DbSchema; // Clone.tt Line: 126
		    to.IdGenerator = from.IdGenerator; // Clone.tt Line: 126
		    to.KeyType = from.KeyType; // Clone.tt Line: 126
		    to.KeyName = from.KeyName; // Clone.tt Line: 126
		    to.Timestamp = from.Timestamp; // Clone.tt Line: 126
		    to.IsDbFromConnectionString = from.IsDbFromConnectionString; // Clone.tt Line: 126
		    to.ConnectionStringName = from.ConnectionStringName; // Clone.tt Line: 126
		    to.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override DbSettings Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return DbSettings.Clone(this);
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
		public static DbSettings ConvertToVM(Proto.Config.db_settings m, DbSettings vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new DbSettings();
		    if (m == null)
		        return vm;
		    vm.DbSchema = m.DbSchema; // Clone.tt Line: 199
		    vm.IdGenerator = (DbIdGeneratorMethod)m.IdGenerator; // Clone.tt Line: 197
		    vm.KeyType = m.KeyType; // Clone.tt Line: 199
		    vm.KeyName = m.KeyName; // Clone.tt Line: 199
		    vm.Timestamp = m.Timestamp; // Clone.tt Line: 199
		    vm.IsDbFromConnectionString = m.IsDbFromConnectionString; // Clone.tt Line: 199
		    vm.ConnectionStringName = m.ConnectionStringName; // Clone.tt Line: 199
		    vm.PathToProjectWithConnectionString = m.PathToProjectWithConnectionString; // Clone.tt Line: 199
		    return vm;
		}
		// Conversion from 'DbSettings' to 'db_settings'
		public static Proto.Config.db_settings ConvertToProto(DbSettings vm) // Clone.tt Line: 209
		{
		    Proto.Config.db_settings m = new Proto.Config.db_settings(); // Clone.tt Line: 211
		    m.DbSchema = vm.DbSchema; // Clone.tt Line: 235
		    m.IdGenerator = (Proto.Config.db_id_generator_method)vm.IdGenerator; // Clone.tt Line: 233
		    m.KeyType = vm.KeyType; // Clone.tt Line: 235
		    m.KeyName = vm.KeyName; // Clone.tt Line: 235
		    m.Timestamp = vm.Timestamp; // Clone.tt Line: 235
		    m.IsDbFromConnectionString = vm.IsDbFromConnectionString; // Clone.tt Line: 235
		    m.ConnectionStringName = vm.ConnectionStringName; // Clone.tt Line: 235
		    m.PathToProjectWithConnectionString = vm.PathToProjectWithConnectionString; // Clone.tt Line: 235
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[Description("DB schema name for all object in this configuration")]
		public string DbSchema // Property.tt Line: 115
		{ 
			set
			{
				if (_DbSchema != value)
				{
					OnDbSchemaChanging();
					_DbSchema = value;
					OnDbSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbSchema; }
		}
		private string _DbSchema = "";
		partial void OnDbSchemaChanging(); // Property.tt Line: 134
		partial void OnDbSchemaChanged();
		[PropertyOrderAttribute(2)]
		[Description("Primary key generation method")]
		public DbIdGeneratorMethod IdGenerator // Property.tt Line: 115
		{ 
			set
			{
				if (_IdGenerator != value)
				{
					OnIdGeneratorChanging();
					_IdGenerator = value;
					OnIdGeneratorChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IdGenerator; }
		}
		private DbIdGeneratorMethod _IdGenerator;
		partial void OnIdGeneratorChanging(); // Property.tt Line: 134
		partial void OnIdGeneratorChanged();
		[PropertyOrderAttribute(3)]
		[Description("Primary key field type")]
		public string KeyType // Property.tt Line: 115
		{ 
			set
			{
				if (_KeyType != value)
				{
					OnKeyTypeChanging();
					_KeyType = value;
					OnKeyTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _KeyType; }
		}
		private string _KeyType = "";
		partial void OnKeyTypeChanging(); // Property.tt Line: 134
		partial void OnKeyTypeChanged();
		[PropertyOrderAttribute(4)]
		[Description("Primary key field name")]
		public string KeyName // Property.tt Line: 115
		{ 
			set
			{
				if (_KeyName != value)
				{
					OnKeyNameChanging();
					_KeyName = value;
					OnKeyNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _KeyName; }
		}
		private string _KeyName = "";
		partial void OnKeyNameChanging(); // Property.tt Line: 134
		partial void OnKeyNameChanged();
		[PropertyOrderAttribute(5)]
		[Description("Record data version/timestamp field name")]
		public string Timestamp // Property.tt Line: 115
		{ 
			set
			{
				if (_Timestamp != value)
				{
					OnTimestampChanging();
					_Timestamp = value;
					OnTimestampChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Timestamp; }
		}
		private string _Timestamp = "";
		partial void OnTimestampChanging(); // Property.tt Line: 134
		partial void OnTimestampChanged();
		
		///////////////////////////////////////////////////
		/// if yes: 
		///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
		/// if no:
		///    1. Find DB type from 
		///    2. Create connection string from db_server, db_database_name, db_user
		///////////////////////////////////////////////////
		public bool IsDbFromConnectionString // Property.tt Line: 115
		{ 
			set
			{
				if (_IsDbFromConnectionString != value)
				{
					OnIsDbFromConnectionStringChanging();
					_IsDbFromConnectionString = value;
					OnIsDbFromConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsDbFromConnectionString; }
		}
		private bool _IsDbFromConnectionString;
		partial void OnIsDbFromConnectionStringChanging(); // Property.tt Line: 134
		partial void OnIsDbFromConnectionStringChanged();
		public string ConnectionStringName // Property.tt Line: 115
		{ 
			set
			{
				if (_ConnectionStringName != value)
				{
					OnConnectionStringNameChanging();
					_ConnectionStringName = value;
					OnConnectionStringNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnectionStringName; }
		}
		private string _ConnectionStringName = "";
		partial void OnConnectionStringNameChanging(); // Property.tt Line: 134
		partial void OnConnectionStringNameChanged();
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(4)]
		[Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
		[Description("File path to store connection string settings in private place.")]
		public string PathToProjectWithConnectionString // Property.tt Line: 115
		{ 
			set
			{
				if (_PathToProjectWithConnectionString != value)
				{
					OnPathToProjectWithConnectionStringChanging();
					_PathToProjectWithConnectionString = value;
					OnPathToProjectWithConnectionStringChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PathToProjectWithConnectionString; }
		}
		private string _PathToProjectWithConnectionString = "";
		partial void OnPathToProjectWithConnectionStringChanging(); // Property.tt Line: 134
		partial void OnPathToProjectWithConnectionStringChanged();
		#endregion Properties
	}
	public partial class AppDbSettings : ViewModelValidatableWithSeverity<AppDbSettings, AppDbSettings.AppDbSettingsValidator>, IAppDbSettings // Class.tt Line: 6
	{
		public partial class AppDbSettingsValidator : ValidatorBase<AppDbSettings, AppDbSettingsValidator> { } 
		#region CTOR
		public AppDbSettings() : base(AppDbSettingsValidator.Validator)	{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static AppDbSettings Clone(AppDbSettings from, bool isDeep = true) // Clone.tt Line: 27
		{
		    AppDbSettings vm = new AppDbSettings();
		    vm.PluginGuid = from.PluginGuid; // Clone.tt Line: 58
		    vm.PluginName = from.PluginName; // Clone.tt Line: 58
		    vm.Version = from.Version; // Clone.tt Line: 58
		    vm.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 58
		    vm.PluginGenName = from.PluginGenName; // Clone.tt Line: 58
		    vm.ConnGuid = from.ConnGuid; // Clone.tt Line: 58
		    vm.ConnName = from.ConnName; // Clone.tt Line: 58
		    return vm;
		}
		public static void Update(AppDbSettings to, AppDbSettings from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.PluginGuid = from.PluginGuid; // Clone.tt Line: 126
		    to.PluginName = from.PluginName; // Clone.tt Line: 126
		    to.Version = from.Version; // Clone.tt Line: 126
		    to.PluginGenGuid = from.PluginGenGuid; // Clone.tt Line: 126
		    to.PluginGenName = from.PluginGenName; // Clone.tt Line: 126
		    to.ConnGuid = from.ConnGuid; // Clone.tt Line: 126
		    to.ConnName = from.ConnName; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override AppDbSettings Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return AppDbSettings.Clone(this);
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
		public static AppDbSettings ConvertToVM(Proto.Config.proto_app_db_settings m, AppDbSettings vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new AppDbSettings();
		    if (m == null)
		        return vm;
		    vm.PluginGuid = m.PluginGuid; // Clone.tt Line: 199
		    vm.PluginName = m.PluginName; // Clone.tt Line: 199
		    vm.Version = m.Version; // Clone.tt Line: 199
		    vm.PluginGenGuid = m.PluginGenGuid; // Clone.tt Line: 199
		    vm.PluginGenName = m.PluginGenName; // Clone.tt Line: 199
		    vm.ConnGuid = m.ConnGuid; // Clone.tt Line: 199
		    vm.ConnName = m.ConnName; // Clone.tt Line: 199
		    return vm;
		}
		// Conversion from 'AppDbSettings' to 'proto_app_db_settings'
		public static Proto.Config.proto_app_db_settings ConvertToProto(AppDbSettings vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_app_db_settings m = new Proto.Config.proto_app_db_settings(); // Clone.tt Line: 211
		    m.PluginGuid = vm.PluginGuid; // Clone.tt Line: 235
		    m.PluginName = vm.PluginName; // Clone.tt Line: 235
		    m.Version = vm.Version; // Clone.tt Line: 235
		    m.PluginGenGuid = vm.PluginGenGuid; // Clone.tt Line: 235
		    m.PluginGenName = vm.PluginGenName; // Clone.tt Line: 235
		    m.ConnGuid = vm.ConnGuid; // Clone.tt Line: 235
		    m.ConnName = vm.ConnName; // Clone.tt Line: 235
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[Editor(typeof(EditorDbPluginSelection), typeof(EditorDbPluginSelection))]
		[Description("Default DB Plugin")]
		public string PluginGuid // Property.tt Line: 115
		{ 
			set
			{
				if (_PluginGuid != value)
				{
					OnPluginGuidChanging();
					_PluginGuid = value;
					OnPluginGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginGuid; }
		}
		private string _PluginGuid = "";
		partial void OnPluginGuidChanging(); // Property.tt Line: 134
		partial void OnPluginGuidChanged();
		[PropertyOrderAttribute(2)]
		[Editable(false)]
		public string PluginName // Property.tt Line: 115
		{ 
			set
			{
				if (_PluginName != value)
				{
					OnPluginNameChanging();
					_PluginName = value;
					OnPluginNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginName; }
		}
		private string _PluginName = "";
		partial void OnPluginNameChanging(); // Property.tt Line: 134
		partial void OnPluginNameChanged();
		[PropertyOrderAttribute(3)]
		[Editable(false)]
		public string Version // Property.tt Line: 115
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging();
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private string _Version = "";
		partial void OnVersionChanging(); // Property.tt Line: 134
		partial void OnVersionChanged();
		[PropertyOrderAttribute(4)]
		[Editor(typeof(EditorDbPluginGenSelection), typeof(EditorDbPluginGenSelection))]
		[Description("Default DB Plugin generator")]
		public string PluginGenGuid // Property.tt Line: 115
		{ 
			set
			{
				if (_PluginGenGuid != value)
				{
					OnPluginGenGuidChanging();
					_PluginGenGuid = value;
					OnPluginGenGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginGenGuid; }
		}
		private string _PluginGenGuid = "";
		partial void OnPluginGenGuidChanging(); // Property.tt Line: 134
		partial void OnPluginGenGuidChanged();
		[PropertyOrderAttribute(5)]
		[Editable(false)]
		public string PluginGenName // Property.tt Line: 115
		{ 
			set
			{
				if (_PluginGenName != value)
				{
					OnPluginGenNameChanging();
					_PluginGenName = value;
					OnPluginGenNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PluginGenName; }
		}
		private string _PluginGenName = "";
		partial void OnPluginGenNameChanging(); // Property.tt Line: 134
		partial void OnPluginGenNameChanged();
		[PropertyOrderAttribute(6)]
		[Editor(typeof(EditorDbConnSelection), typeof(EditorDbConnSelection))]
		[Description("Default DB connection string")]
		public string ConnGuid // Property.tt Line: 115
		{ 
			set
			{
				if (_ConnGuid != value)
				{
					OnConnGuidChanging();
					_ConnGuid = value;
					OnConnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnGuid; }
		}
		private string _ConnGuid = "";
		partial void OnConnGuidChanging(); // Property.tt Line: 134
		partial void OnConnGuidChanged();
		[PropertyOrderAttribute(7)]
		[Editable(false)]
		public string ConnName // Property.tt Line: 115
		{ 
			set
			{
				if (_ConnName != value)
				{
					OnConnNameChanging();
					_ConnName = value;
					OnConnNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConnName; }
		}
		private string _ConnName = "";
		partial void OnConnNameChanging(); // Property.tt Line: 134
		partial void OnConnNameChanged();
		#endregion Properties
	}
	public partial class GroupListAppSolutions : ConfigObjectBase<GroupListAppSolutions, GroupListAppSolutions.GroupListAppSolutionsValidator>, IComparable<GroupListAppSolutions>, IConfigAcceptVisitor, IGroupListAppSolutions // Class.tt Line: 6
	{
		public partial class GroupListAppSolutionsValidator : ValidatorBase<GroupListAppSolutions, GroupListAppSolutionsValidator> { } 
		#region CTOR
		public GroupListAppSolutions() : base(GroupListAppSolutionsValidator.Validator)	{
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 23
			this.ListAppSolutions = new SortedObservableCollection<AppSolution>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListAppSolutions(ITreeConfigNode parent) : base(GroupListAppSolutionsValidator.Validator)
	    {
	        this.Parent = parent;
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 49
			this.ListAppSolutions = new SortedObservableCollection<AppSolution>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListAppSolutions Clone(GroupListAppSolutions from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListAppSolutions vm = new GroupListAppSolutions();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.DefaultDb = AppDbSettings.Clone(from.DefaultDb, isDeep);
		    vm.ListAppSolutions = new SortedObservableCollection<AppSolution>();
		    foreach(var t in from.ListAppSolutions) // Clone.tt Line: 45
		        vm.ListAppSolutions.Add(AppSolution.Clone((AppSolution)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListAppSolutions to, GroupListAppSolutions from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new AppSolution();
		                AppSolution.Update(p, (AppSolution)tt, isDeep);
		                to.ListAppSolutions.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListAppSolutions Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListAppSolutions.Clone(this);
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
		public static GroupListAppSolutions ConvertToVM(Proto.Config.proto_group_list_app_solutions m, GroupListAppSolutions vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListAppSolutions();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 193
		    vm.ListAppSolutions = new SortedObservableCollection<AppSolution>();
		    foreach(var t in m.ListAppSolutions) // Clone.tt Line: 179
		    {
		        var tvm = AppSolution.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListAppSolutions.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListAppSolutions' to 'proto_group_list_app_solutions'
		public static Proto.Config.proto_group_list_app_solutions ConvertToProto(GroupListAppSolutions vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_app_solutions m = new Proto.Config.proto_group_list_app_solutions(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 229
		    foreach(var t in vm.ListAppSolutions) // Clone.tt Line: 214
		        m.ListAppSolutions.Add(AppSolution.ConvertToProto((AppSolution)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(3)]
		[ExpandableObjectAttribute()]
		[DisplayName("Default DB")]
		public AppDbSettings DefaultDb // Property.tt Line: 94
		{ 
			set
			{
				if (_DefaultDb != value)
				{
					OnDefaultDbChanging();
		            _DefaultDb = value;
					OnDefaultDbChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DefaultDb; }
		}
		private AppDbSettings _DefaultDb;
		[BrowsableAttribute(false)]
		public IAppDbSettings DefaultDbI { get { return _DefaultDb; }}
		partial void OnDefaultDbChanging(); // Property.tt Line: 134
		partial void OnDefaultDbChanged();
		
		///////////////////////////////////////////////////
		/// List NET solutions
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<AppSolution> ListAppSolutions  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListAppSolutions != value)
				{
					OnListAppSolutionsChanging();
					_ListAppSolutions = value;
					OnListAppSolutionsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListAppSolutions; }
		}
		private SortedObservableCollection<AppSolution> _ListAppSolutions;
		[BrowsableAttribute(false)]
		public IEnumerable<IAppSolution> ListAppSolutionsI { get { foreach (var t in _ListAppSolutions) yield return t; } }
		public AppSolution this[int index] { get { return (AppSolution)this.ListAppSolutions[index]; } }
		public void Add(AppSolution item)  // Property.tt Line: 72
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
		partial void OnListAppSolutionsChanging(); // Property.tt Line: 134
		partial void OnListAppSolutionsChanged();
		#endregion Properties
	}
	public partial class AppSolution : ConfigObjectBase<AppSolution, AppSolution.AppSolutionValidator>, IComparable<AppSolution>, IConfigAcceptVisitor, IAppSolution // Class.tt Line: 6
	{
		public partial class AppSolutionValidator : ValidatorBase<AppSolution, AppSolutionValidator> { } 
		#region CTOR
		public AppSolution() : base(AppSolutionValidator.Validator)	{
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 23
			this.ListAppProjects = new SortedObservableCollection<AppProject>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public AppSolution(ITreeConfigNode parent) : base(AppSolutionValidator.Validator)
	    {
	        this.Parent = parent;
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 49
			this.ListAppProjects = new SortedObservableCollection<AppProject>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static AppSolution Clone(AppSolution from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    AppSolution vm = new AppSolution();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.DefaultDb = AppDbSettings.Clone(from.DefaultDb, isDeep);
		    vm.ListAppProjects = new SortedObservableCollection<AppProject>();
		    foreach(var t in from.ListAppProjects) // Clone.tt Line: 45
		        vm.ListAppProjects.Add(AppProject.Clone((AppProject)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(AppSolution to, AppSolution from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.RelativeAppSolutionPath = from.RelativeAppSolutionPath; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new AppProject();
		                AppProject.Update(p, (AppProject)tt, isDeep);
		                to.ListAppProjects.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override AppSolution Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return AppSolution.Clone(this);
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
		public static AppSolution ConvertToVM(Proto.Config.proto_app_solution m, AppSolution vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new AppSolution();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.RelativeAppSolutionPath = m.RelativeAppSolutionPath; // Clone.tt Line: 199
		    AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 193
		    vm.ListAppProjects = new SortedObservableCollection<AppProject>();
		    foreach(var t in m.ListAppProjects) // Clone.tt Line: 179
		    {
		        var tvm = AppProject.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListAppProjects.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'AppSolution' to 'proto_app_solution'
		public static Proto.Config.proto_app_solution ConvertToProto(AppSolution vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_app_solution m = new Proto.Config.proto_app_solution(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.RelativeAppSolutionPath = vm.RelativeAppSolutionPath; // Clone.tt Line: 235
		    m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 229
		    foreach(var t in vm.ListAppProjects) // Clone.tt Line: 214
		        m.ListAppProjects.Add(AppProject.ConvertToProto((AppProject)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// List NET projects
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(6)]
		[Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
		public string RelativeAppSolutionPath // Property.tt Line: 115
		{ 
			set
			{
				if (_RelativeAppSolutionPath != value)
				{
					OnRelativeAppSolutionPathChanging();
					_RelativeAppSolutionPath = value;
					OnRelativeAppSolutionPathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeAppSolutionPath; }
		}
		private string _RelativeAppSolutionPath = "";
		partial void OnRelativeAppSolutionPathChanging(); // Property.tt Line: 134
		partial void OnRelativeAppSolutionPathChanged();
		[PropertyOrderAttribute(8)]
		[ExpandableObjectAttribute()]
		[DisplayName("Default DB")]
		[Description("Database connection. If empty, all solutions settings are used")]
		public AppDbSettings DefaultDb // Property.tt Line: 94
		{ 
			set
			{
				if (_DefaultDb != value)
				{
					OnDefaultDbChanging();
		            _DefaultDb = value;
					OnDefaultDbChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DefaultDb; }
		}
		private AppDbSettings _DefaultDb;
		[BrowsableAttribute(false)]
		public IAppDbSettings DefaultDbI { get { return _DefaultDb; }}
		partial void OnDefaultDbChanging(); // Property.tt Line: 134
		partial void OnDefaultDbChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<AppProject> ListAppProjects  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListAppProjects != value)
				{
					OnListAppProjectsChanging();
					_ListAppProjects = value;
					OnListAppProjectsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListAppProjects; }
		}
		private SortedObservableCollection<AppProject> _ListAppProjects;
		[BrowsableAttribute(false)]
		public IEnumerable<IAppProject> ListAppProjectsI { get { foreach (var t in _ListAppProjects) yield return t; } }
		partial void OnListAppProjectsChanging(); // Property.tt Line: 134
		partial void OnListAppProjectsChanged();
		#endregion Properties
	}
	public partial class AppProject : ConfigObjectBase<AppProject, AppProject.AppProjectValidator>, IComparable<AppProject>, IConfigAcceptVisitor, IAppProject // Class.tt Line: 6
	{
		public partial class AppProjectValidator : ValidatorBase<AppProject, AppProjectValidator> { } 
		#region CTOR
		public AppProject() : base(AppProjectValidator.Validator)	{
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 23
			OnInit();
		}
	    // Class.tt Line: 34
		public AppProject(ITreeConfigNode parent) : base(AppProjectValidator.Validator)
	    {
	        this.Parent = parent;
			this.DefaultDb = new AppDbSettings(); // Class.tt Line: 49
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static AppProject Clone(AppProject from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    AppProject vm = new AppProject();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.DefaultDb = AppDbSettings.Clone(from.DefaultDb, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(AppProject to, AppProject from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.RelativeAppProjectPath = from.RelativeAppProjectPath; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        AppDbSettings.Update(to.DefaultDb, from.DefaultDb, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override AppProject Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return AppProject.Clone(this);
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
		public static AppProject ConvertToVM(Proto.Config.proto_app_project m, AppProject vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new AppProject();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.RelativeAppProjectPath = m.RelativeAppProjectPath; // Clone.tt Line: 199
		    AppDbSettings.ConvertToVM(m.DefaultDb, vm.DefaultDb); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'AppProject' to 'proto_app_project'
		public static Proto.Config.proto_app_project ConvertToProto(AppProject vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_app_project m = new Proto.Config.proto_app_project(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.RelativeAppProjectPath = vm.RelativeAppProjectPath; // Clone.tt Line: 235
		    m.DefaultDb = AppDbSettings.ConvertToProto(vm.DefaultDb); // Clone.tt Line: 229
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(6)]
		[Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
		public string RelativeAppProjectPath // Property.tt Line: 115
		{ 
			set
			{
				if (_RelativeAppProjectPath != value)
				{
					OnRelativeAppProjectPathChanging();
					_RelativeAppProjectPath = value;
					OnRelativeAppProjectPathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeAppProjectPath; }
		}
		private string _RelativeAppProjectPath = "";
		partial void OnRelativeAppProjectPathChanging(); // Property.tt Line: 134
		partial void OnRelativeAppProjectPathChanged();
		[PropertyOrderAttribute(8)]
		[ExpandableObjectAttribute()]
		[DisplayName("Default DB")]
		[Description("Database connection. If empty, solution settings are used")]
		public AppDbSettings DefaultDb // Property.tt Line: 94
		{ 
			set
			{
				if (_DefaultDb != value)
				{
					OnDefaultDbChanging();
		            _DefaultDb = value;
					OnDefaultDbChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DefaultDb; }
		}
		private AppDbSettings _DefaultDb;
		[BrowsableAttribute(false)]
		public IAppDbSettings DefaultDbI { get { return _DefaultDb; }}
		partial void OnDefaultDbChanging(); // Property.tt Line: 134
		partial void OnDefaultDbChanged();
		#endregion Properties
	}
	public partial class ConfigShortHistory : ConfigObjectBase<ConfigShortHistory, ConfigShortHistory.ConfigShortHistoryValidator>, IComparable<ConfigShortHistory>, IConfigAcceptVisitor, IConfigShortHistory // Class.tt Line: 6
	{
		public partial class ConfigShortHistoryValidator : ValidatorBase<ConfigShortHistory, ConfigShortHistoryValidator> { } 
		#region CTOR
		public ConfigShortHistory() : base(ConfigShortHistoryValidator.Validator)	{
			this.CurrentConfig = new Config(this); // Class.tt Line: 25
			this.PrevStableConfig = new Config(this); // Class.tt Line: 25
			this.OldStableConfig = new Config(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public ConfigShortHistory(ITreeConfigNode parent) : base(ConfigShortHistoryValidator.Validator)
	    {
	        this.Parent = parent;
			this.CurrentConfig = new Config(this); // Class.tt Line: 51
			this.PrevStableConfig = new Config(this); // Class.tt Line: 51
			this.OldStableConfig = new Config(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static ConfigShortHistory Clone(ConfigShortHistory from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    ConfigShortHistory vm = new ConfigShortHistory();
		    if (isDeep) // Clone.tt Line: 55
		        vm.CurrentConfig = Config.Clone(from.CurrentConfig, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.PrevStableConfig = Config.Clone(from.PrevStableConfig, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.OldStableConfig = Config.Clone(from.OldStableConfig, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(ConfigShortHistory to, ConfigShortHistory from, bool isDeep = true) // Clone.tt Line: 68
		{
		    if (isDeep) // Clone.tt Line: 123
		        Config.Update(to.CurrentConfig, from.CurrentConfig, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        Config.Update(to.PrevStableConfig, from.PrevStableConfig, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        Config.Update(to.OldStableConfig, from.OldStableConfig, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override ConfigShortHistory Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return ConfigShortHistory.Clone(this);
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
		public static ConfigShortHistory ConvertToVM(Proto.Config.proto_config_short_history m, ConfigShortHistory vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new ConfigShortHistory();
		    if (m == null)
		        return vm;
		    Config.ConvertToVM(m.CurrentConfig, vm.CurrentConfig); // Clone.tt Line: 193
		    Config.ConvertToVM(m.PrevStableConfig, vm.PrevStableConfig); // Clone.tt Line: 193
		    Config.ConvertToVM(m.OldStableConfig, vm.OldStableConfig); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'ConfigShortHistory' to 'proto_config_short_history'
		public static Proto.Config.proto_config_short_history ConvertToProto(ConfigShortHistory vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_config_short_history m = new Proto.Config.proto_config_short_history(); // Clone.tt Line: 211
		    m.CurrentConfig = Config.ConvertToProto(vm.CurrentConfig); // Clone.tt Line: 229
		    m.PrevStableConfig = Config.ConvertToProto(vm.PrevStableConfig); // Clone.tt Line: 229
		    m.OldStableConfig = Config.ConvertToProto(vm.OldStableConfig); // Clone.tt Line: 229
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
		
		public Config CurrentConfig // Property.tt Line: 94
		{ 
			set
			{
				if (_CurrentConfig != value)
				{
					OnCurrentConfigChanging();
		            _CurrentConfig = value;
					OnCurrentConfigChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _CurrentConfig; }
		}
		private Config _CurrentConfig;
		[BrowsableAttribute(false)]
		public IConfig CurrentConfigI { get { return _CurrentConfig; }}
		partial void OnCurrentConfigChanging(); // Property.tt Line: 134
		partial void OnCurrentConfigChanged();
		public Config PrevStableConfig // Property.tt Line: 94
		{ 
			set
			{
				if (_PrevStableConfig != value)
				{
					OnPrevStableConfigChanging();
		            _PrevStableConfig = value;
					OnPrevStableConfigChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PrevStableConfig; }
		}
		private Config _PrevStableConfig;
		[BrowsableAttribute(false)]
		public IConfig PrevStableConfigI { get { return _PrevStableConfig; }}
		partial void OnPrevStableConfigChanging(); // Property.tt Line: 134
		partial void OnPrevStableConfigChanged();
		public Config OldStableConfig // Property.tt Line: 94
		{ 
			set
			{
				if (_OldStableConfig != value)
				{
					OnOldStableConfigChanging();
		            _OldStableConfig = value;
					OnOldStableConfigChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _OldStableConfig; }
		}
		private Config _OldStableConfig;
		[BrowsableAttribute(false)]
		public IConfig OldStableConfigI { get { return _OldStableConfig; }}
		partial void OnOldStableConfigChanging(); // Property.tt Line: 134
		partial void OnOldStableConfigChanged();
		#endregion Properties
	}
	public partial class GroupListBaseConfigs : ConfigObjectBase<GroupListBaseConfigs, GroupListBaseConfigs.GroupListBaseConfigsValidator>, IComparable<GroupListBaseConfigs>, IConfigAcceptVisitor, IGroupListBaseConfigs // Class.tt Line: 6
	{
		public partial class GroupListBaseConfigsValidator : ValidatorBase<GroupListBaseConfigs, GroupListBaseConfigsValidator> { } 
		#region CTOR
		public GroupListBaseConfigs() : base(GroupListBaseConfigsValidator.Validator)	{
			this.ListBaseConfigs = new SortedObservableCollection<BaseConfig>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListBaseConfigs(ITreeConfigNode parent) : base(GroupListBaseConfigsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListBaseConfigs = new SortedObservableCollection<BaseConfig>(); // Class.tt Line: 45
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    if (type == typeof(BaseConfig)) // Clone.tt Line: 15
		    {
		        this.ListBaseConfigs.Sort();
		    }
		}
		public static GroupListBaseConfigs Clone(GroupListBaseConfigs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListBaseConfigs vm = new GroupListBaseConfigs();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListBaseConfigs = new SortedObservableCollection<BaseConfig>();
		    foreach(var t in from.ListBaseConfigs) // Clone.tt Line: 45
		        vm.ListBaseConfigs.Add(BaseConfig.Clone((BaseConfig)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListBaseConfigs to, GroupListBaseConfigs from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
		    {
		        foreach(var t in to.ListBaseConfigs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListBaseConfigs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    BaseConfig.Update((BaseConfig)t, (BaseConfig)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListBaseConfigs.Remove(t);
		        }
		        foreach(var tt in from.ListBaseConfigs)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListBaseConfigs.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new BaseConfig();
		                BaseConfig.Update(p, (BaseConfig)tt, isDeep);
		                to.ListBaseConfigs.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListBaseConfigs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListBaseConfigs.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListBaseConfigs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListBaseConfigs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_base_configs' to 'GroupListBaseConfigs'
		public static GroupListBaseConfigs ConvertToVM(Proto.Config.proto_group_list_base_configs m, GroupListBaseConfigs vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListBaseConfigs();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListBaseConfigs = new SortedObservableCollection<BaseConfig>();
		    foreach(var t in m.ListBaseConfigs) // Clone.tt Line: 179
		    {
		        var tvm = BaseConfig.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListBaseConfigs.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListBaseConfigs' to 'proto_group_list_base_configs'
		public static Proto.Config.proto_group_list_base_configs ConvertToProto(GroupListBaseConfigs vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_base_configs m = new Proto.Config.proto_group_list_base_configs(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListBaseConfigs) // Clone.tt Line: 214
		        m.ListBaseConfigs.Add(BaseConfig.ConvertToProto((BaseConfig)t)); // Clone.tt Line: 218
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListBaseConfigs)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<BaseConfig> ListBaseConfigs  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListBaseConfigs != value)
				{
					OnListBaseConfigsChanging();
					_ListBaseConfigs = value;
					OnListBaseConfigsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListBaseConfigs; }
		}
		private SortedObservableCollection<BaseConfig> _ListBaseConfigs;
		[BrowsableAttribute(false)]
		public IEnumerable<IBaseConfig> ListBaseConfigsI { get { foreach (var t in _ListBaseConfigs) yield return t; } }
		public BaseConfig this[int index] { get { return (BaseConfig)this.ListBaseConfigs[index]; } }
		public void Add(BaseConfig item)  // Property.tt Line: 72
		{ 
		    this.ListBaseConfigs.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<BaseConfig> items) 
		{ 
		    this.ListBaseConfigs.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.ListBaseConfigs.Count; 
		}
		public void Remove(BaseConfig item) 
		{
		    this.ListBaseConfigs.Remove(item); 
		    item.Parent = null;
		}
		partial void OnListBaseConfigsChanging(); // Property.tt Line: 134
		partial void OnListBaseConfigsChanged();
		#endregion Properties
	}
	public partial class BaseConfig : ConfigObjectBase<BaseConfig, BaseConfig.BaseConfigValidator>, IComparable<BaseConfig>, IConfigAcceptVisitor, IBaseConfig // Class.tt Line: 6
	{
		public partial class BaseConfigValidator : ValidatorBase<BaseConfig, BaseConfigValidator> { } 
		#region CTOR
		public BaseConfig() : base(BaseConfigValidator.Validator)	{
			this.ConfigNode = new Config(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public BaseConfig(ITreeConfigNode parent) : base(BaseConfigValidator.Validator)
	    {
	        this.Parent = parent;
			this.ConfigNode = new Config(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static BaseConfig Clone(BaseConfig from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    BaseConfig vm = new BaseConfig();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.ConfigNode = Config.Clone(from.ConfigNode, isDeep);
		    vm.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(BaseConfig to, BaseConfig from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        Config.Update(to.ConfigNode, from.ConfigNode, isDeep);
		    to.RelativeConfigFilePath = from.RelativeConfigFilePath; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override BaseConfig Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return BaseConfig.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(BaseConfig from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    BaseConfig.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_base_config' to 'BaseConfig'
		public static BaseConfig ConvertToVM(Proto.Config.proto_base_config m, BaseConfig vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new BaseConfig();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    Config.ConvertToVM(m.ConfigNode, vm.ConfigNode); // Clone.tt Line: 193
		    vm.RelativeConfigFilePath = m.RelativeConfigFilePath; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'BaseConfig' to 'proto_base_config'
		public static Proto.Config.proto_base_config ConvertToProto(BaseConfig vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_base_config m = new Proto.Config.proto_base_config(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.ConfigNode = Config.ConvertToProto(vm.ConfigNode); // Clone.tt Line: 229
		    m.RelativeConfigFilePath = vm.RelativeConfigFilePath; // Clone.tt Line: 235
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.ConfigNode.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public Config ConfigNode // Property.tt Line: 94
		{ 
			set
			{
				if (_ConfigNode != value)
				{
					OnConfigNodeChanging();
		            _ConfigNode = value;
					OnConfigNodeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ConfigNode; }
		}
		private Config _ConfigNode;
		[BrowsableAttribute(false)]
		public IConfig ConfigNodeI { get { return _ConfigNode; }}
		partial void OnConfigNodeChanging(); // Property.tt Line: 134
		partial void OnConfigNodeChanged();
		[PropertyOrderAttribute(6)]
		[Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
		public string RelativeConfigFilePath // Property.tt Line: 115
		{ 
			set
			{
				if (_RelativeConfigFilePath != value)
				{
					OnRelativeConfigFilePathChanging();
					_RelativeConfigFilePath = value;
					OnRelativeConfigFilePathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeConfigFilePath; }
		}
		private string _RelativeConfigFilePath = "";
		partial void OnRelativeConfigFilePathChanging(); // Property.tt Line: 134
		partial void OnRelativeConfigFilePathChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Configuration config
	///////////////////////////////////////////////////
	public partial class Config : ConfigObjectBase<Config, Config.ConfigValidator>, IComparable<Config>, IConfigAcceptVisitor, IConfig // Class.tt Line: 6
	{
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { } 
		#region CTOR
		public Config() : base(ConfigValidator.Validator)	{
			this.DbSettings = new DbSettings(); // Class.tt Line: 23
			this.GroupConfigs = new GroupListBaseConfigs(this); // Class.tt Line: 25
			this.Model = new ConfigModel(this); // Class.tt Line: 25
			this.GroupSubModels = new GroupListSubModels(this); // Class.tt Line: 25
			this.GroupPlugins = new GroupListPlugins(this); // Class.tt Line: 25
			this.GroupAppSolutions = new GroupListAppSolutions(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public Config(ITreeConfigNode parent) : base(ConfigValidator.Validator)
	    {
	        this.Parent = parent;
			this.DbSettings = new DbSettings(); // Class.tt Line: 49
			this.GroupConfigs = new GroupListBaseConfigs(this); // Class.tt Line: 51
			this.Model = new ConfigModel(this); // Class.tt Line: 51
			this.GroupSubModels = new GroupListSubModels(this); // Class.tt Line: 51
			this.GroupPlugins = new GroupListPlugins(this); // Class.tt Line: 51
			this.GroupAppSolutions = new GroupListAppSolutions(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Config Clone(Config from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Config vm = new Config();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Version = from.Version; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.LastUpdated = from.LastUpdated; // Clone.tt Line: 58
		    vm.PrimaryKeyType = from.PrimaryKeyType; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.DbSettings = DbSettings.Clone(from.DbSettings, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupConfigs = GroupListBaseConfigs.Clone(from.GroupConfigs, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.Model = ConfigModel.Clone(from.Model, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupSubModels = GroupListSubModels.Clone(from.GroupSubModels, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupPlugins = GroupListPlugins.Clone(from.GroupPlugins, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupAppSolutions = GroupListAppSolutions.Clone(from.GroupAppSolutions, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Config to, Config from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Version = from.Version; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.LastUpdated = from.LastUpdated; // Clone.tt Line: 126
		    to.PrimaryKeyType = from.PrimaryKeyType; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        DbSettings.Update(to.DbSettings, from.DbSettings, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListBaseConfigs.Update(to.GroupConfigs, from.GroupConfigs, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        ConfigModel.Update(to.Model, from.Model, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListSubModels.Update(to.GroupSubModels, from.GroupSubModels, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListPlugins.Update(to.GroupPlugins, from.GroupPlugins, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListAppSolutions.Update(to.GroupAppSolutions, from.GroupAppSolutions, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Config Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Config.Clone(this);
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
		public static Config ConvertToVM(Proto.Config.proto_config m, Config vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Config();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Version = m.Version; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.LastUpdated = m.LastUpdated; // Clone.tt Line: 199
		    vm.PrimaryKeyType = (EnumPrimaryKeyType)m.PrimaryKeyType; // Clone.tt Line: 197
		    DbSettings.ConvertToVM(m.DbSettings, vm.DbSettings); // Clone.tt Line: 193
		    GroupListBaseConfigs.ConvertToVM(m.GroupConfigs, vm.GroupConfigs); // Clone.tt Line: 193
		    ConfigModel.ConvertToVM(m.Model, vm.Model); // Clone.tt Line: 193
		    GroupListSubModels.ConvertToVM(m.GroupSubModels, vm.GroupSubModels); // Clone.tt Line: 193
		    GroupListPlugins.ConvertToVM(m.GroupPlugins, vm.GroupPlugins); // Clone.tt Line: 193
		    GroupListAppSolutions.ConvertToVM(m.GroupAppSolutions, vm.GroupAppSolutions); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Config' to 'proto_config'
		public static Proto.Config.proto_config ConvertToProto(Config vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_config m = new Proto.Config.proto_config(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Version = vm.Version; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.LastUpdated = vm.LastUpdated; // Clone.tt Line: 235
		    m.PrimaryKeyType = (Proto.Config.proto_enum_primary_key_type)vm.PrimaryKeyType; // Clone.tt Line: 233
		    m.DbSettings = DbSettings.ConvertToProto(vm.DbSettings); // Clone.tt Line: 229
		    m.GroupConfigs = GroupListBaseConfigs.ConvertToProto(vm.GroupConfigs); // Clone.tt Line: 229
		    m.Model = ConfigModel.ConvertToProto(vm.Model); // Clone.tt Line: 229
		    m.GroupSubModels = GroupListSubModels.ConvertToProto(vm.GroupSubModels); // Clone.tt Line: 229
		    m.GroupPlugins = GroupListPlugins.ConvertToProto(vm.GroupPlugins); // Clone.tt Line: 229
		    m.GroupAppSolutions = GroupListAppSolutions.ConvertToProto(vm.GroupAppSolutions); // Clone.tt Line: 229
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.Model.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			this.GroupSubModels.AcceptConfigNodeVisitor(visitor); // AcceptNodeVisitor.tt Line: 25
		
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(4)]
		[Editable(false)]
		public int Version // Property.tt Line: 115
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging();
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private int _Version;
		partial void OnVersionChanging(); // Property.tt Line: 134
		partial void OnVersionChanged();
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(6)]
		public Google.Protobuf.WellKnownTypes.Timestamp LastUpdated // Property.tt Line: 115
		{ 
			set
			{
				if (_LastUpdated != value)
				{
					OnLastUpdatedChanging();
					_LastUpdated = value;
					OnLastUpdatedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _LastUpdated; }
		}
		private Google.Protobuf.WellKnownTypes.Timestamp _LastUpdated;
		partial void OnLastUpdatedChanging(); // Property.tt Line: 134
		partial void OnLastUpdatedChanged();
		[PropertyOrderAttribute(7)]
		public EnumPrimaryKeyType PrimaryKeyType // Property.tt Line: 115
		{ 
			set
			{
				if (_PrimaryKeyType != value)
				{
					OnPrimaryKeyTypeChanging();
					_PrimaryKeyType = value;
					OnPrimaryKeyTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PrimaryKeyType; }
		}
		private EnumPrimaryKeyType _PrimaryKeyType;
		partial void OnPrimaryKeyTypeChanging(); // Property.tt Line: 134
		partial void OnPrimaryKeyTypeChanged();
		
		///////////////////////////////////////////////////
		/// GENERAL DB SETTINGS
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(11)]
		[ExpandableObjectAttribute()]
		public DbSettings DbSettings // Property.tt Line: 94
		{ 
			set
			{
				if (_DbSettings != value)
				{
					OnDbSettingsChanging();
		            _DbSettings = value;
					OnDbSettingsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbSettings; }
		}
		private DbSettings _DbSettings;
		[BrowsableAttribute(false)]
		public IDbSettings DbSettingsI { get { return _DbSettings; }}
		partial void OnDbSettingsChanging(); // Property.tt Line: 134
		partial void OnDbSettingsChanged();
		[BrowsableAttribute(false)]
		public GroupListBaseConfigs GroupConfigs // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupConfigs != value)
				{
					OnGroupConfigsChanging();
		            _GroupConfigs = value;
					OnGroupConfigsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupConfigs; }
		}
		private GroupListBaseConfigs _GroupConfigs;
		[BrowsableAttribute(false)]
		public IGroupListBaseConfigs GroupConfigsI { get { return _GroupConfigs; }}
		partial void OnGroupConfigsChanging(); // Property.tt Line: 134
		partial void OnGroupConfigsChanged();
		[BrowsableAttribute(false)]
		public ConfigModel Model // Property.tt Line: 94
		{ 
			set
			{
				if (_Model != value)
				{
					OnModelChanging();
		            _Model = value;
					OnModelChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Model; }
		}
		private ConfigModel _Model;
		[BrowsableAttribute(false)]
		public IConfigModel ModelI { get { return _Model; }}
		partial void OnModelChanging(); // Property.tt Line: 134
		partial void OnModelChanged();
		[BrowsableAttribute(false)]
		public GroupListSubModels GroupSubModels // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupSubModels != value)
				{
					OnGroupSubModelsChanging();
		            _GroupSubModels = value;
					OnGroupSubModelsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupSubModels; }
		}
		private GroupListSubModels _GroupSubModels;
		[BrowsableAttribute(false)]
		public IGroupListSubModels GroupSubModelsI { get { return _GroupSubModels; }}
		partial void OnGroupSubModelsChanging(); // Property.tt Line: 134
		partial void OnGroupSubModelsChanged();
		[BrowsableAttribute(false)]
		public GroupListPlugins GroupPlugins // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupPlugins != value)
				{
					OnGroupPluginsChanging();
		            _GroupPlugins = value;
					OnGroupPluginsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPlugins; }
		}
		private GroupListPlugins _GroupPlugins;
		[BrowsableAttribute(false)]
		public IGroupListPlugins GroupPluginsI { get { return _GroupPlugins; }}
		partial void OnGroupPluginsChanging(); // Property.tt Line: 134
		partial void OnGroupPluginsChanged();
		[BrowsableAttribute(false)]
		public GroupListAppSolutions GroupAppSolutions // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupAppSolutions != value)
				{
					OnGroupAppSolutionsChanging();
		            _GroupAppSolutions = value;
					OnGroupAppSolutionsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupAppSolutions; }
		}
		private GroupListAppSolutions _GroupAppSolutions;
		[BrowsableAttribute(false)]
		public IGroupListAppSolutions GroupAppSolutionsI { get { return _GroupAppSolutions; }}
		partial void OnGroupAppSolutionsChanging(); // Property.tt Line: 134
		partial void OnGroupAppSolutionsChanged();
		#endregion Properties
	}
	public partial class GroupListSubModels : ConfigObjectBase<GroupListSubModels, GroupListSubModels.GroupListSubModelsValidator>, IComparable<GroupListSubModels>, IConfigAcceptVisitor, IGroupListSubModels // Class.tt Line: 6
	{
		public partial class GroupListSubModelsValidator : ValidatorBase<GroupListSubModels, GroupListSubModelsValidator> { } 
		#region CTOR
		public GroupListSubModels() : base(GroupListSubModelsValidator.Validator)	{
			this.ListSubModels = new SortedObservableCollection<SubModel>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListSubModels(ITreeConfigNode parent) : base(GroupListSubModelsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListSubModels = new SortedObservableCollection<SubModel>(); // Class.tt Line: 45
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    if (type == typeof(SubModel)) // Clone.tt Line: 15
		    {
		        this.ListSubModels.Sort();
		    }
		}
		public static GroupListSubModels Clone(GroupListSubModels from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListSubModels vm = new GroupListSubModels();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.IsAutoInsertDependancies = from.IsAutoInsertDependancies; // Clone.tt Line: 58
		    vm.ListSubModels = new SortedObservableCollection<SubModel>();
		    foreach(var t in from.ListSubModels) // Clone.tt Line: 45
		        vm.ListSubModels.Add(SubModel.Clone((SubModel)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListSubModels to, GroupListSubModels from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.IsAutoInsertDependancies = from.IsAutoInsertDependancies; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
		    {
		        foreach(var t in to.ListSubModels.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListSubModels)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    SubModel.Update((SubModel)t, (SubModel)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListSubModels.Remove(t);
		        }
		        foreach(var tt in from.ListSubModels)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListSubModels.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new SubModel();
		                SubModel.Update(p, (SubModel)tt, isDeep);
		                to.ListSubModels.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListSubModels Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListSubModels.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupListSubModels from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupListSubModels.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_list_sub_models' to 'GroupListSubModels'
		public static GroupListSubModels ConvertToVM(Proto.Config.proto_group_list_sub_models m, GroupListSubModels vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListSubModels();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.IsAutoInsertDependancies = m.IsAutoInsertDependancies; // Clone.tt Line: 199
		    vm.ListSubModels = new SortedObservableCollection<SubModel>();
		    foreach(var t in m.ListSubModels) // Clone.tt Line: 179
		    {
		        var tvm = SubModel.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListSubModels.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListSubModels' to 'proto_group_list_sub_models'
		public static Proto.Config.proto_group_list_sub_models ConvertToProto(GroupListSubModels vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_sub_models m = new Proto.Config.proto_group_list_sub_models(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.IsAutoInsertDependancies = vm.IsAutoInsertDependancies; // Clone.tt Line: 235
		    foreach(var t in vm.ListSubModels) // Clone.tt Line: 214
		        m.ListSubModels.Add(SubModel.ConvertToProto((SubModel)t)); // Clone.tt Line: 218
		    return m;
		}
		
		public void AcceptConfigNodeVisitor(ConfigVisitor visitor) // AcceptNodeVisitor.tt Line: 8
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListSubModels)
				t.AcceptConfigNodeVisitor(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[DisplayName("Auto")]
		[Description("Auto insert dependancies")]
		public bool IsAutoInsertDependancies // Property.tt Line: 115
		{ 
			set
			{
				if (_IsAutoInsertDependancies != value)
				{
					OnIsAutoInsertDependanciesChanging();
					_IsAutoInsertDependancies = value;
					OnIsAutoInsertDependanciesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsAutoInsertDependancies; }
		}
		private bool _IsAutoInsertDependancies;
		partial void OnIsAutoInsertDependanciesChanging(); // Property.tt Line: 134
		partial void OnIsAutoInsertDependanciesChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<SubModel> ListSubModels  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListSubModels != value)
				{
					OnListSubModelsChanging();
					_ListSubModels = value;
					OnListSubModelsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListSubModels; }
		}
		private SortedObservableCollection<SubModel> _ListSubModels;
		[BrowsableAttribute(false)]
		public IEnumerable<ISubModel> ListSubModelsI { get { foreach (var t in _ListSubModels) yield return t; } }
		public SubModel this[int index] { get { return (SubModel)this.ListSubModels[index]; } }
		public void Add(SubModel item)  // Property.tt Line: 72
		{ 
		    this.ListSubModels.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<SubModel> items) 
		{ 
		    this.ListSubModels.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.ListSubModels.Count; 
		}
		public void Remove(SubModel item) 
		{
		    this.ListSubModels.Remove(item); 
		    item.Parent = null;
		}
		partial void OnListSubModelsChanging(); // Property.tt Line: 134
		partial void OnListSubModelsChanged();
		#endregion Properties
	}
	public partial class ObjectInclusionRecord : ViewModelValidatableWithSeverity<ObjectInclusionRecord, ObjectInclusionRecord.ObjectInclusionRecordValidator>, IObjectInclusionRecord // Class.tt Line: 6
	{
		public partial class ObjectInclusionRecordValidator : ValidatorBase<ObjectInclusionRecord, ObjectInclusionRecordValidator> { } 
		#region CTOR
		public ObjectInclusionRecord() : base(ObjectInclusionRecordValidator.Validator)	{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static ObjectInclusionRecord Clone(ObjectInclusionRecord from, bool isDeep = true) // Clone.tt Line: 27
		{
		    ObjectInclusionRecord vm = new ObjectInclusionRecord();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Inclusion = from.Inclusion.HasValue ? from.Inclusion.Value : (bool?)null; // Clone.tt Line: 51
		    return vm;
		}
		public static void Update(ObjectInclusionRecord to, ObjectInclusionRecord from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Inclusion = from.Inclusion.HasValue ? from.Inclusion.Value : (bool?)null; // Clone.tt Line: 121
		}
		// Clone.tt Line: 132
		#region IEditable
		public override ObjectInclusionRecord Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return ObjectInclusionRecord.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(ObjectInclusionRecord from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    ObjectInclusionRecord.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_object_inclusion_record' to 'ObjectInclusionRecord'
		public static ObjectInclusionRecord ConvertToVM(Proto.Config.proto_object_inclusion_record m, ObjectInclusionRecord vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new ObjectInclusionRecord();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Inclusion = m.Inclusion.HasValue ? m.Inclusion.Value : (bool?)null; // Clone.tt Line: 191
		    return vm;
		}
		// Conversion from 'ObjectInclusionRecord' to 'proto_object_inclusion_record'
		public static Proto.Config.proto_object_inclusion_record ConvertToProto(ObjectInclusionRecord vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_object_inclusion_record m = new Proto.Config.proto_object_inclusion_record(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Inclusion = new Proto.Config.bool_nullable(); // Clone.tt Line: 224
		    m.Inclusion.HasValue = vm.Inclusion.HasValue;
		    if (vm.Inclusion.HasValue)
		        m.Inclusion.Value = vm.Inclusion.Value;
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[BrowsableAttribute(false)]
		public string Guid // Property.tt Line: 115
		{ 
			set
			{
				if (_Guid != value)
				{
					OnGuidChanging();
					_Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Guid; }
		}
		private string _Guid = "";
		partial void OnGuidChanging(); // Property.tt Line: 134
		partial void OnGuidChanged();
		public bool? Inclusion // Property.tt Line: 115
		{ 
			set
			{
				if (_Inclusion != value)
				{
					OnInclusionChanging();
					_Inclusion = value;
					OnInclusionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Inclusion; }
		}
		private bool? _Inclusion;
		partial void OnInclusionChanging(); // Property.tt Line: 134
		partial void OnInclusionChanged();
		#endregion Properties
	}
	public partial class SubModel : ConfigObjectBase<SubModel, SubModel.SubModelValidator>, IComparable<SubModel>, IConfigAcceptVisitor, ISubModel // Class.tt Line: 6
	{
		public partial class SubModelValidator : ValidatorBase<SubModel, SubModelValidator> { } 
		#region CTOR
		public SubModel() : base(SubModelValidator.Validator)	{
			this.ListObjectInclusionRecords = new ObservableCollection<ObjectInclusionRecord>(); // Class.tt Line: 17
			this.ListGroupObjects = new ObservableCollection<ObjectInclusionRecord>(); // Class.tt Line: 17
			OnInit();
		}
	    // Class.tt Line: 34
		public SubModel(ITreeConfigNode parent) : base(SubModelValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListObjectInclusionRecords = new ObservableCollection<ObjectInclusionRecord>(); // Class.tt Line: 43
			this.ListGroupObjects = new ObservableCollection<ObjectInclusionRecord>(); // Class.tt Line: 43
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		}
		public static SubModel Clone(SubModel from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    SubModel vm = new SubModel();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.EnumDefaultInclusion = from.EnumDefaultInclusion; // Clone.tt Line: 58
		    vm.ListObjectInclusionRecords = new ObservableCollection<ObjectInclusionRecord>();
		    foreach(var t in from.ListObjectInclusionRecords) // Clone.tt Line: 41
		        vm.ListObjectInclusionRecords.Add(ObjectInclusionRecord.Clone((ObjectInclusionRecord)t, isDeep));
		    vm.ListGroupObjects = new ObservableCollection<ObjectInclusionRecord>();
		    foreach(var t in from.ListGroupObjects) // Clone.tt Line: 41
		        vm.ListGroupObjects.Add(ObjectInclusionRecord.Clone((ObjectInclusionRecord)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(SubModel to, SubModel from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.EnumDefaultInclusion = from.EnumDefaultInclusion; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
		    {
		        foreach(var t in to.ListObjectInclusionRecords.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListObjectInclusionRecords)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    ObjectInclusionRecord.Update((ObjectInclusionRecord)t, (ObjectInclusionRecord)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListObjectInclusionRecords.Remove(t);
		        }
		        foreach(var tt in from.ListObjectInclusionRecords)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListObjectInclusionRecords.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new ObjectInclusionRecord();
		                ObjectInclusionRecord.Update(p, (ObjectInclusionRecord)tt, isDeep);
		                to.ListObjectInclusionRecords.Add(p);
		            }
		        }
		    }
		    if (isDeep) // Clone.tt Line: 75
		    {
		        foreach(var t in to.ListGroupObjects.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListGroupObjects)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    ObjectInclusionRecord.Update((ObjectInclusionRecord)t, (ObjectInclusionRecord)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListGroupObjects.Remove(t);
		        }
		        foreach(var tt in from.ListGroupObjects)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListGroupObjects.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new ObjectInclusionRecord();
		                ObjectInclusionRecord.Update(p, (ObjectInclusionRecord)tt, isDeep);
		                to.ListGroupObjects.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override SubModel Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return SubModel.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(SubModel from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    SubModel.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_sub_model' to 'SubModel'
		public static SubModel ConvertToVM(Proto.Config.proto_sub_model m, SubModel vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new SubModel();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.EnumDefaultInclusion = (EnumIncludeDefaultPolicy)m.EnumDefaultInclusion; // Clone.tt Line: 197
		    vm.ListObjectInclusionRecords = new ObservableCollection<ObjectInclusionRecord>();
		    foreach(var t in m.ListObjectInclusionRecords) // Clone.tt Line: 169
		    {
		        var tvm = ObjectInclusionRecord.ConvertToVM(t);
		        vm.ListObjectInclusionRecords.Add(tvm); // Clone.tt Line: 175
		    }
		    vm.ListGroupObjects = new ObservableCollection<ObjectInclusionRecord>();
		    foreach(var t in m.ListGroupObjects) // Clone.tt Line: 169
		    {
		        var tvm = ObjectInclusionRecord.ConvertToVM(t);
		        vm.ListGroupObjects.Add(tvm); // Clone.tt Line: 175
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'SubModel' to 'proto_sub_model'
		public static Proto.Config.proto_sub_model ConvertToProto(SubModel vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_sub_model m = new Proto.Config.proto_sub_model(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.EnumDefaultInclusion = (Proto.Config.proto_enum_include_default_policy)vm.EnumDefaultInclusion; // Clone.tt Line: 233
		    foreach(var t in vm.ListObjectInclusionRecords) // Clone.tt Line: 214
		        m.ListObjectInclusionRecords.Add(ObjectInclusionRecord.ConvertToProto((ObjectInclusionRecord)t)); // Clone.tt Line: 218
		    foreach(var t in vm.ListGroupObjects) // Clone.tt Line: 214
		        m.ListGroupObjects.Add(ObjectInclusionRecord.ConvertToProto((ObjectInclusionRecord)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[DisplayName("Default Mode")]
		[Description("Default mode for model objects inclusion behavior")]
		public EnumIncludeDefaultPolicy EnumDefaultInclusion // Property.tt Line: 115
		{ 
			set
			{
				if (_EnumDefaultInclusion != value)
				{
					OnEnumDefaultInclusionChanging();
					_EnumDefaultInclusion = value;
					OnEnumDefaultInclusionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _EnumDefaultInclusion; }
		}
		private EnumIncludeDefaultPolicy _EnumDefaultInclusion;
		partial void OnEnumDefaultInclusionChanging(); // Property.tt Line: 134
		partial void OnEnumDefaultInclusionChanged();
		[BrowsableAttribute(false)]
		public ObservableCollection<ObjectInclusionRecord> ListObjectInclusionRecords // Property.tt Line: 9
		{ 
			set
			{
				if (_ListObjectInclusionRecords != value)
				{
					OnListObjectInclusionRecordsChanging();
					_ListObjectInclusionRecords = value;
					OnListObjectInclusionRecordsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListObjectInclusionRecords; }
		}
		private ObservableCollection<ObjectInclusionRecord> _ListObjectInclusionRecords;
		[BrowsableAttribute(false)]
		public IEnumerable<IObjectInclusionRecord> ListObjectInclusionRecordsI { get { foreach (var t in _ListObjectInclusionRecords) yield return t; } }
		partial void OnListObjectInclusionRecordsChanging(); // Property.tt Line: 134
		partial void OnListObjectInclusionRecordsChanged();
		
		///////////////////////////////////////////////////
		/// 
		/// proto_object_inclusion_record test =12;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		[ExpandableObjectAttribute()]
		public ObservableCollection<ObjectInclusionRecord> ListGroupObjects // Property.tt Line: 9
		{ 
			set
			{
				if (_ListGroupObjects != value)
				{
					OnListGroupObjectsChanging();
					_ListGroupObjects = value;
					OnListGroupObjectsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListGroupObjects; }
		}
		private ObservableCollection<ObjectInclusionRecord> _ListGroupObjects;
		[BrowsableAttribute(false)]
		public IEnumerable<IObjectInclusionRecord> ListGroupObjectsI { get { foreach (var t in _ListGroupObjects) yield return t; } }
		partial void OnListGroupObjectsChanging(); // Property.tt Line: 134
		partial void OnListGroupObjectsChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Configuration model
	///////////////////////////////////////////////////
	public partial class ConfigModel : ConfigObjectBase<ConfigModel, ConfigModel.ConfigModelValidator>, IComparable<ConfigModel>, IConfigAcceptVisitor, IConfigModel // Class.tt Line: 6
	{
		public partial class ConfigModelValidator : ValidatorBase<ConfigModel, ConfigModelValidator> { } 
		#region CTOR
		public ConfigModel() : base(ConfigModelValidator.Validator)	{
			this.GroupCommon = new GroupListCommon(this); // Class.tt Line: 25
			this.GroupConstants = new GroupListConstants(this); // Class.tt Line: 25
			this.GroupEnumerations = new GroupListEnumerations(this); // Class.tt Line: 25
			this.GroupCatalogs = new GroupListCatalogs(this); // Class.tt Line: 25
			this.GroupDocuments = new GroupDocuments(this); // Class.tt Line: 25
			this.GroupJournals = new GroupListJournals(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public ConfigModel(ITreeConfigNode parent) : base(ConfigModelValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupCommon = new GroupListCommon(this); // Class.tt Line: 51
			this.GroupConstants = new GroupListConstants(this); // Class.tt Line: 51
			this.GroupEnumerations = new GroupListEnumerations(this); // Class.tt Line: 51
			this.GroupCatalogs = new GroupListCatalogs(this); // Class.tt Line: 51
			this.GroupDocuments = new GroupDocuments(this); // Class.tt Line: 51
			this.GroupJournals = new GroupListJournals(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static ConfigModel Clone(ConfigModel from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    ConfigModel vm = new ConfigModel();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Version = from.Version; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupCommon = GroupListCommon.Clone(from.GroupCommon, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupConstants = GroupListConstants.Clone(from.GroupConstants, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupEnumerations = GroupListEnumerations.Clone(from.GroupEnumerations, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupCatalogs = GroupListCatalogs.Clone(from.GroupCatalogs, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupDocuments = GroupDocuments.Clone(from.GroupDocuments, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupJournals = GroupListJournals.Clone(from.GroupJournals, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(ConfigModel to, ConfigModel from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Version = from.Version; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        GroupListCommon.Update(to.GroupCommon, from.GroupCommon, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListConstants.Update(to.GroupConstants, from.GroupConstants, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListEnumerations.Update(to.GroupEnumerations, from.GroupEnumerations, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListCatalogs.Update(to.GroupCatalogs, from.GroupCatalogs, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupDocuments.Update(to.GroupDocuments, from.GroupDocuments, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListJournals.Update(to.GroupJournals, from.GroupJournals, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override ConfigModel Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return ConfigModel.Clone(this);
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
		public static ConfigModel ConvertToVM(Proto.Config.proto_config_model m, ConfigModel vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new ConfigModel();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Version = m.Version; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    GroupListCommon.ConvertToVM(m.GroupCommon, vm.GroupCommon); // Clone.tt Line: 193
		    GroupListConstants.ConvertToVM(m.GroupConstants, vm.GroupConstants); // Clone.tt Line: 193
		    GroupListEnumerations.ConvertToVM(m.GroupEnumerations, vm.GroupEnumerations); // Clone.tt Line: 193
		    GroupListCatalogs.ConvertToVM(m.GroupCatalogs, vm.GroupCatalogs); // Clone.tt Line: 193
		    GroupDocuments.ConvertToVM(m.GroupDocuments, vm.GroupDocuments); // Clone.tt Line: 193
		    GroupListJournals.ConvertToVM(m.GroupJournals, vm.GroupJournals); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'ConfigModel' to 'proto_config_model'
		public static Proto.Config.proto_config_model ConvertToProto(ConfigModel vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_config_model m = new Proto.Config.proto_config_model(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Version = vm.Version; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.GroupCommon = GroupListCommon.ConvertToProto(vm.GroupCommon); // Clone.tt Line: 229
		    m.GroupConstants = GroupListConstants.ConvertToProto(vm.GroupConstants); // Clone.tt Line: 229
		    m.GroupEnumerations = GroupListEnumerations.ConvertToProto(vm.GroupEnumerations); // Clone.tt Line: 229
		    m.GroupCatalogs = GroupListCatalogs.ConvertToProto(vm.GroupCatalogs); // Clone.tt Line: 229
		    m.GroupDocuments = GroupDocuments.ConvertToProto(vm.GroupDocuments); // Clone.tt Line: 229
		    m.GroupJournals = GroupListJournals.ConvertToProto(vm.GroupJournals); // Clone.tt Line: 229
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
		public int Version // Property.tt Line: 115
		{ 
			set
			{
				if (_Version != value)
				{
					OnVersionChanging();
					_Version = value;
					OnVersionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Version; }
		}
		private int _Version;
		partial void OnVersionChanging(); // Property.tt Line: 134
		partial void OnVersionChanged();
		[PropertyOrderAttribute(5)]
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListCommon GroupCommon // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupCommon != value)
				{
					OnGroupCommonChanging();
		            _GroupCommon = value;
					OnGroupCommonChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupCommon; }
		}
		private GroupListCommon _GroupCommon;
		[BrowsableAttribute(false)]
		public IGroupListCommon GroupCommonI { get { return _GroupCommon; }}
		partial void OnGroupCommonChanging(); // Property.tt Line: 134
		partial void OnGroupCommonChanged();
		[BrowsableAttribute(false)]
		public GroupListConstants GroupConstants // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupConstants != value)
				{
					OnGroupConstantsChanging();
		            _GroupConstants = value;
					OnGroupConstantsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupConstants; }
		}
		private GroupListConstants _GroupConstants;
		[BrowsableAttribute(false)]
		public IGroupListConstants GroupConstantsI { get { return _GroupConstants; }}
		partial void OnGroupConstantsChanging(); // Property.tt Line: 134
		partial void OnGroupConstantsChanged();
		[BrowsableAttribute(false)]
		public GroupListEnumerations GroupEnumerations // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupEnumerations != value)
				{
					OnGroupEnumerationsChanging();
		            _GroupEnumerations = value;
					OnGroupEnumerationsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupEnumerations; }
		}
		private GroupListEnumerations _GroupEnumerations;
		[BrowsableAttribute(false)]
		public IGroupListEnumerations GroupEnumerationsI { get { return _GroupEnumerations; }}
		partial void OnGroupEnumerationsChanging(); // Property.tt Line: 134
		partial void OnGroupEnumerationsChanged();
		[BrowsableAttribute(false)]
		public GroupListCatalogs GroupCatalogs // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupCatalogs != value)
				{
					OnGroupCatalogsChanging();
		            _GroupCatalogs = value;
					OnGroupCatalogsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupCatalogs; }
		}
		private GroupListCatalogs _GroupCatalogs;
		[BrowsableAttribute(false)]
		public IGroupListCatalogs GroupCatalogsI { get { return _GroupCatalogs; }}
		partial void OnGroupCatalogsChanging(); // Property.tt Line: 134
		partial void OnGroupCatalogsChanged();
		[BrowsableAttribute(false)]
		public GroupDocuments GroupDocuments // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupDocuments != value)
				{
					OnGroupDocumentsChanging();
		            _GroupDocuments = value;
					OnGroupDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupDocuments; }
		}
		private GroupDocuments _GroupDocuments;
		[BrowsableAttribute(false)]
		public IGroupDocuments GroupDocumentsI { get { return _GroupDocuments; }}
		partial void OnGroupDocumentsChanging(); // Property.tt Line: 134
		partial void OnGroupDocumentsChanged();
		[BrowsableAttribute(false)]
		public GroupListJournals GroupJournals // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupJournals != value)
				{
					OnGroupJournalsChanging();
		            _GroupJournals = value;
					OnGroupJournalsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupJournals; }
		}
		private GroupListJournals _GroupJournals;
		[BrowsableAttribute(false)]
		public IGroupListJournals GroupJournalsI { get { return _GroupJournals; }}
		partial void OnGroupJournalsChanging(); // Property.tt Line: 134
		partial void OnGroupJournalsChanged();
		#endregion Properties
	}
	public partial class DataType : ViewModelValidatableWithSeverity<DataType, DataType.DataTypeValidator>, IDataType // Class.tt Line: 6
	{
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { } 
		#region CTOR
		public DataType() : base(DataTypeValidator.Validator)	{
			this.ListObjectGuids = new ObservableCollection<string>(); // Class.tt Line: 17
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DataType Clone(DataType from, bool isDeep = true) // Clone.tt Line: 27
		{
		    DataType vm = new DataType();
		    vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 58
		    vm.Length = from.Length; // Clone.tt Line: 58
		    vm.Accuracy = from.Accuracy; // Clone.tt Line: 58
		    vm.IsPositive = from.IsPositive; // Clone.tt Line: 58
		    vm.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 58
		    vm.IsNullable = from.IsNullable; // Clone.tt Line: 58
		    foreach(var t in from.ListObjectGuids) // Clone.tt Line: 37
		        vm.ListObjectGuids.Add(t);
		    vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 58
		    return vm;
		}
		public static void Update(DataType to, DataType from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 126
		    to.Length = from.Length; // Clone.tt Line: 126
		    to.Accuracy = from.Accuracy; // Clone.tt Line: 126
		    to.IsPositive = from.IsPositive; // Clone.tt Line: 126
		    to.ObjectGuid = from.ObjectGuid; // Clone.tt Line: 126
		    to.IsNullable = from.IsNullable; // Clone.tt Line: 126
		        to.ListObjectGuids.Clear(); // Clone.tt Line: 112
		        foreach(var tt in from.ListObjectGuids)
		        {
		            to.ListObjectGuids.Add(tt);
		        }
		    to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override DataType Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return DataType.Clone(this);
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
		public static DataType ConvertToVM(Proto.Config.proto_data_type m, DataType vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new DataType();
		    if (m == null)
		        return vm;
		    vm.DataTypeEnum = (EnumDataType)m.DataTypeEnum; // Clone.tt Line: 197
		    vm.Length = m.Length; // Clone.tt Line: 199
		    vm.Accuracy = m.Accuracy; // Clone.tt Line: 199
		    vm.IsPositive = m.IsPositive; // Clone.tt Line: 199
		    vm.ObjectGuid = m.ObjectGuid; // Clone.tt Line: 199
		    vm.IsNullable = m.IsNullable; // Clone.tt Line: 199
		    vm.ListObjectGuids = new ObservableCollection<string>();
		    foreach(var t in m.ListObjectGuids) // Clone.tt Line: 163
		    {
		        vm.ListObjectGuids.Add(t);
		    }
		    vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 199
		    return vm;
		}
		// Conversion from 'DataType' to 'proto_data_type'
		public static Proto.Config.proto_data_type ConvertToProto(DataType vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_data_type m = new Proto.Config.proto_data_type(); // Clone.tt Line: 211
		    m.DataTypeEnum = (Proto.Config.proto_enum_data_type)vm.DataTypeEnum; // Clone.tt Line: 233
		    m.Length = vm.Length; // Clone.tt Line: 235
		    m.Accuracy = vm.Accuracy; // Clone.tt Line: 235
		    m.IsPositive = vm.IsPositive; // Clone.tt Line: 235
		    m.ObjectGuid = vm.ObjectGuid; // Clone.tt Line: 235
		    m.IsNullable = vm.IsNullable; // Clone.tt Line: 235
		    foreach(var t in vm.ListObjectGuids) // Clone.tt Line: 214
		        m.ListObjectGuids.Add(t); // Clone.tt Line: 216
		    m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 235
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[DisplayName("Type")]
		public EnumDataType DataTypeEnum // Property.tt Line: 115
		{ 
			set
			{
				if (_DataTypeEnum != value)
				{
					OnDataTypeEnumChanging();
					_DataTypeEnum = value;
					OnDataTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeEnum; }
		}
		private EnumDataType _DataTypeEnum;
		partial void OnDataTypeEnumChanging(); // Property.tt Line: 134
		partial void OnDataTypeEnumChanged();
		[PropertyOrderAttribute(5)]
		public uint Length // Property.tt Line: 115
		{ 
			set
			{
				if (_Length != value)
				{
					OnLengthChanging();
					_Length = value;
					OnLengthChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Length; }
		}
		private uint _Length;
		partial void OnLengthChanging(); // Property.tt Line: 134
		partial void OnLengthChanged();
		[PropertyOrderAttribute(7)]
		public uint Accuracy // Property.tt Line: 115
		{ 
			set
			{
				if (_Accuracy != value)
				{
					OnAccuracyChanging();
					_Accuracy = value;
					OnAccuracyChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Accuracy; }
		}
		private uint _Accuracy;
		partial void OnAccuracyChanging(); // Property.tt Line: 134
		partial void OnAccuracyChanged();
		[PropertyOrderAttribute(6)]
		[DisplayName("Is positive")]
		public bool IsPositive // Property.tt Line: 115
		{ 
			set
			{
				if (_IsPositive != value)
				{
					OnIsPositiveChanging();
					_IsPositive = value;
					OnIsPositiveChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPositive; }
		}
		private bool _IsPositive;
		partial void OnIsPositiveChanging(); // Property.tt Line: 134
		partial void OnIsPositiveChanged();
		[PropertyOrderAttribute(3)]
		[Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
		public string ObjectGuid // Property.tt Line: 115
		{ 
			set
			{
				if (_ObjectGuid != value)
				{
					OnObjectGuidChanging();
					_ObjectGuid = value;
					OnObjectGuidChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ObjectGuid; }
		}
		private string _ObjectGuid = "";
		partial void OnObjectGuidChanging(); // Property.tt Line: 134
		partial void OnObjectGuidChanged();
		[PropertyOrderAttribute(2)]
		public bool IsNullable // Property.tt Line: 115
		{ 
			set
			{
				if (_IsNullable != value)
				{
					OnIsNullableChanging();
					_IsNullable = value;
					OnIsNullableChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsNullable; }
		}
		private bool _IsNullable;
		partial void OnIsNullableChanging(); // Property.tt Line: 134
		partial void OnIsNullableChanged();
		[PropertyOrderAttribute(4)]
		public ObservableCollection<string> ListObjectGuids // Property.tt Line: 9
		{ 
			set
			{
				if (_ListObjectGuids != value)
				{
					OnListObjectGuidsChanging();
					_ListObjectGuids = value;
					OnListObjectGuidsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListObjectGuids; }
		}
		private ObservableCollection<string> _ListObjectGuids;
		[BrowsableAttribute(false)]
		public IEnumerable<string> ListObjectGuidsI { get { foreach (var t in _ListObjectGuids) yield return t; } }
		partial void OnListObjectGuidsChanging(); // Property.tt Line: 134
		partial void OnListObjectGuidsChanged();
		[PropertyOrderAttribute(8)]
		[DisplayName("FK Index")]
		[Description("Create Index if this property is using foreign key (for Catalog or Document type)")]
		public bool IsIndexFk // Property.tt Line: 115
		{ 
			set
			{
				if (_IsIndexFk != value)
				{
					OnIsIndexFkChanging();
					_IsIndexFk = value;
					OnIsIndexFkChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsIndexFk; }
		}
		private bool _IsIndexFk;
		partial void OnIsIndexFkChanging(); // Property.tt Line: 134
		partial void OnIsIndexFkChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Common parameters section
	///////////////////////////////////////////////////
	public partial class GroupListCommon : ConfigObjectBase<GroupListCommon, GroupListCommon.GroupListCommonValidator>, IComparable<GroupListCommon>, IConfigAcceptVisitor, IGroupListCommon // Class.tt Line: 6
	{
		public partial class GroupListCommonValidator : ValidatorBase<GroupListCommon, GroupListCommonValidator> { } 
		#region CTOR
		public GroupListCommon() : base(GroupListCommonValidator.Validator)	{
			this.GroupRoles = new GroupListRoles(this); // Class.tt Line: 25
			this.GroupViewForms = new GroupListMainViewForms(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListCommon(ITreeConfigNode parent) : base(GroupListCommonValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupRoles = new GroupListRoles(this); // Class.tt Line: 51
			this.GroupViewForms = new GroupListMainViewForms(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static GroupListCommon Clone(GroupListCommon from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListCommon vm = new GroupListCommon();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupRoles = GroupListRoles.Clone(from.GroupRoles, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupViewForms = GroupListMainViewForms.Clone(from.GroupViewForms, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListCommon to, GroupListCommon from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        GroupListRoles.Update(to.GroupRoles, from.GroupRoles, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListMainViewForms.Update(to.GroupViewForms, from.GroupViewForms, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListCommon Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListCommon.Clone(this);
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
		public static GroupListCommon ConvertToVM(Proto.Config.proto_group_list_common m, GroupListCommon vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListCommon();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    GroupListRoles.ConvertToVM(m.GroupRoles, vm.GroupRoles); // Clone.tt Line: 193
		    GroupListMainViewForms.ConvertToVM(m.GroupViewForms, vm.GroupViewForms); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListCommon' to 'proto_group_list_common'
		public static Proto.Config.proto_group_list_common ConvertToProto(GroupListCommon vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_common m = new Proto.Config.proto_group_list_common(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.GroupRoles = GroupListRoles.ConvertToProto(vm.GroupRoles); // Clone.tt Line: 229
		    m.GroupViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupViewForms); // Clone.tt Line: 229
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListRoles GroupRoles // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupRoles != value)
				{
					OnGroupRolesChanging();
		            _GroupRoles = value;
					OnGroupRolesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupRoles; }
		}
		private GroupListRoles _GroupRoles;
		[BrowsableAttribute(false)]
		public IGroupListRoles GroupRolesI { get { return _GroupRoles; }}
		partial void OnGroupRolesChanging(); // Property.tt Line: 134
		partial void OnGroupRolesChanged();
		[BrowsableAttribute(false)]
		public GroupListMainViewForms GroupViewForms // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupViewForms != value)
				{
					OnGroupViewFormsChanging();
		            _GroupViewForms = value;
					OnGroupViewFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupViewForms; }
		}
		private GroupListMainViewForms _GroupViewForms;
		[BrowsableAttribute(false)]
		public IGroupListMainViewForms GroupViewFormsI { get { return _GroupViewForms; }}
		partial void OnGroupViewFormsChanging(); // Property.tt Line: 134
		partial void OnGroupViewFormsChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// User's role
	///////////////////////////////////////////////////
	public partial class Role : ConfigObjectBase<Role, Role.RoleValidator>, IComparable<Role>, IConfigAcceptVisitor, IRole // Class.tt Line: 6
	{
		public partial class RoleValidator : ValidatorBase<Role, RoleValidator> { } 
		#region CTOR
		public Role() : base(RoleValidator.Validator)	{
			OnInit();
		}
	    // Class.tt Line: 34
		public Role(ITreeConfigNode parent) : base(RoleValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Role Clone(Role from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Role vm = new Role();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Role to, Role from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Role Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Role.Clone(this);
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
		public static Role ConvertToVM(Proto.Config.proto_role m, Role vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Role();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Role' to 'proto_role'
		public static Proto.Config.proto_role ConvertToProto(Role vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_role m = new Proto.Config.proto_role(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		#endregion Properties
	}
	public partial class GroupListRoles : ConfigObjectBase<GroupListRoles, GroupListRoles.GroupListRolesValidator>, IComparable<GroupListRoles>, IConfigAcceptVisitor, IGroupListRoles // Class.tt Line: 6
	{
		public partial class GroupListRolesValidator : ValidatorBase<GroupListRoles, GroupListRolesValidator> { } 
		#region CTOR
		public GroupListRoles() : base(GroupListRolesValidator.Validator)	{
			this.ListRoles = new SortedObservableCollection<Role>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListRoles(ITreeConfigNode parent) : base(GroupListRolesValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListRoles = new SortedObservableCollection<Role>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListRoles Clone(GroupListRoles from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListRoles vm = new GroupListRoles();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListRoles = new SortedObservableCollection<Role>();
		    foreach(var t in from.ListRoles) // Clone.tt Line: 45
		        vm.ListRoles.Add(Role.Clone((Role)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListRoles to, GroupListRoles from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Role();
		                Role.Update(p, (Role)tt, isDeep);
		                to.ListRoles.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListRoles Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListRoles.Clone(this);
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
		public static GroupListRoles ConvertToVM(Proto.Config.proto_group_list_roles m, GroupListRoles vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListRoles();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListRoles = new SortedObservableCollection<Role>();
		    foreach(var t in m.ListRoles) // Clone.tt Line: 179
		    {
		        var tvm = Role.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListRoles.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListRoles' to 'proto_group_list_roles'
		public static Proto.Config.proto_group_list_roles ConvertToProto(GroupListRoles vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_roles m = new Proto.Config.proto_group_list_roles(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListRoles) // Clone.tt Line: 214
		        m.ListRoles.Add(Role.ConvertToProto((Role)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Role> ListRoles  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListRoles != value)
				{
					OnListRolesChanging();
					_ListRoles = value;
					OnListRolesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListRoles; }
		}
		private SortedObservableCollection<Role> _ListRoles;
		[BrowsableAttribute(false)]
		public IEnumerable<IRole> ListRolesI { get { foreach (var t in _ListRoles) yield return t; } }
		public Role this[int index] { get { return (Role)this.ListRoles[index]; } }
		public void Add(Role item)  // Property.tt Line: 72
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
		partial void OnListRolesChanging(); // Property.tt Line: 134
		partial void OnListRolesChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy parent
	///////////////////////////////////////////////////
	public partial class MainViewForm : ConfigObjectBase<MainViewForm, MainViewForm.MainViewFormValidator>, IComparable<MainViewForm>, IConfigAcceptVisitor, IMainViewForm // Class.tt Line: 6
	{
		public partial class MainViewFormValidator : ValidatorBase<MainViewForm, MainViewFormValidator> { } 
		#region CTOR
		public MainViewForm() : base(MainViewFormValidator.Validator)	{
			this.GroupListViewForms = new GroupListMainViewForms(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public MainViewForm(ITreeConfigNode parent) : base(MainViewFormValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupListViewForms = new GroupListMainViewForms(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static MainViewForm Clone(MainViewForm from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    MainViewForm vm = new MainViewForm();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupListViewForms = GroupListMainViewForms.Clone(from.GroupListViewForms, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(MainViewForm to, MainViewForm from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        GroupListMainViewForms.Update(to.GroupListViewForms, from.GroupListViewForms, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override MainViewForm Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return MainViewForm.Clone(this);
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
		public static MainViewForm ConvertToVM(Proto.Config.proto_main_view_form m, MainViewForm vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new MainViewForm();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    GroupListMainViewForms.ConvertToVM(m.GroupListViewForms, vm.GroupListViewForms); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'MainViewForm' to 'proto_main_view_form'
		public static Proto.Config.proto_main_view_form ConvertToProto(MainViewForm vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_main_view_form m = new Proto.Config.proto_main_view_form(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.GroupListViewForms = GroupListMainViewForms.ConvertToProto(vm.GroupListViewForms); // Clone.tt Line: 229
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListMainViewForms GroupListViewForms // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupListViewForms != value)
				{
					OnGroupListViewFormsChanging();
		            _GroupListViewForms = value;
					OnGroupListViewFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupListViewForms; }
		}
		private GroupListMainViewForms _GroupListViewForms;
		[BrowsableAttribute(false)]
		public IGroupListMainViewForms GroupListViewFormsI { get { return _GroupListViewForms; }}
		partial void OnGroupListViewFormsChanging(); // Property.tt Line: 134
		partial void OnGroupListViewFormsChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// main view forms hierarchy node with children
	///////////////////////////////////////////////////
	public partial class GroupListMainViewForms : ConfigObjectBase<GroupListMainViewForms, GroupListMainViewForms.GroupListMainViewFormsValidator>, IComparable<GroupListMainViewForms>, IConfigAcceptVisitor, IGroupListMainViewForms // Class.tt Line: 6
	{
		public partial class GroupListMainViewFormsValidator : ValidatorBase<GroupListMainViewForms, GroupListMainViewFormsValidator> { } 
		#region CTOR
		public GroupListMainViewForms() : base(GroupListMainViewFormsValidator.Validator)	{
			this.ListMainViewForms = new SortedObservableCollection<MainViewForm>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListMainViewForms(ITreeConfigNode parent) : base(GroupListMainViewFormsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListMainViewForms = new SortedObservableCollection<MainViewForm>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListMainViewForms Clone(GroupListMainViewForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListMainViewForms vm = new GroupListMainViewForms();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListMainViewForms = new SortedObservableCollection<MainViewForm>();
		    foreach(var t in from.ListMainViewForms) // Clone.tt Line: 45
		        vm.ListMainViewForms.Add(MainViewForm.Clone((MainViewForm)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListMainViewForms to, GroupListMainViewForms from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new MainViewForm();
		                MainViewForm.Update(p, (MainViewForm)tt, isDeep);
		                to.ListMainViewForms.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListMainViewForms Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListMainViewForms.Clone(this);
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
		public static GroupListMainViewForms ConvertToVM(Proto.Config.proto_group_list_main_view_forms m, GroupListMainViewForms vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListMainViewForms();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListMainViewForms = new SortedObservableCollection<MainViewForm>();
		    foreach(var t in m.ListMainViewForms) // Clone.tt Line: 179
		    {
		        var tvm = MainViewForm.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListMainViewForms.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListMainViewForms' to 'proto_group_list_main_view_forms'
		public static Proto.Config.proto_group_list_main_view_forms ConvertToProto(GroupListMainViewForms vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_main_view_forms m = new Proto.Config.proto_group_list_main_view_forms(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListMainViewForms) // Clone.tt Line: 214
		        m.ListMainViewForms.Add(MainViewForm.ConvertToProto((MainViewForm)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<MainViewForm> ListMainViewForms  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListMainViewForms != value)
				{
					OnListMainViewFormsChanging();
					_ListMainViewForms = value;
					OnListMainViewFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListMainViewForms; }
		}
		private SortedObservableCollection<MainViewForm> _ListMainViewForms;
		[BrowsableAttribute(false)]
		public IEnumerable<IMainViewForm> ListMainViewFormsI { get { foreach (var t in _ListMainViewForms) yield return t; } }
		public MainViewForm this[int index] { get { return (MainViewForm)this.ListMainViewForms[index]; } }
		public void Add(MainViewForm item)  // Property.tt Line: 72
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
		partial void OnListMainViewFormsChanging(); // Property.tt Line: 134
		partial void OnListMainViewFormsChanged();
		#endregion Properties
	}
	public partial class GroupListPropertiesTabs : ConfigObjectBase<GroupListPropertiesTabs, GroupListPropertiesTabs.GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IConfigAcceptVisitor, IGroupListPropertiesTabs // Class.tt Line: 6
	{
		public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { } 
		#region CTOR
		public GroupListPropertiesTabs() : base(GroupListPropertiesTabsValidator.Validator)	{
			this.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListPropertiesTabs(ITreeConfigNode parent) : base(GroupListPropertiesTabsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListPropertiesTabs Clone(GroupListPropertiesTabs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListPropertiesTabs vm = new GroupListPropertiesTabs();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>();
		    foreach(var t in from.ListPropertiesTabs) // Clone.tt Line: 45
		        vm.ListPropertiesTabs.Add(PropertiesTab.Clone((PropertiesTab)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListPropertiesTabs to, GroupListPropertiesTabs from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new PropertiesTab();
		                PropertiesTab.Update(p, (PropertiesTab)tt, isDeep);
		                to.ListPropertiesTabs.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListPropertiesTabs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListPropertiesTabs.Clone(this);
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
		public static GroupListPropertiesTabs ConvertToVM(Proto.Config.proto_group_list_properties_tabs m, GroupListPropertiesTabs vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListPropertiesTabs();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>();
		    foreach(var t in m.ListPropertiesTabs) // Clone.tt Line: 179
		    {
		        var tvm = PropertiesTab.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListPropertiesTabs.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListPropertiesTabs' to 'proto_group_list_properties_tabs'
		public static Proto.Config.proto_group_list_properties_tabs ConvertToProto(GroupListPropertiesTabs vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_properties_tabs m = new Proto.Config.proto_group_list_properties_tabs(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListPropertiesTabs) // Clone.tt Line: 214
		        m.ListPropertiesTabs.Add(PropertiesTab.ConvertToProto((PropertiesTab)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<PropertiesTab> ListPropertiesTabs  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListPropertiesTabs != value)
				{
					OnListPropertiesTabsChanging();
					_ListPropertiesTabs = value;
					OnListPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPropertiesTabs; }
		}
		private SortedObservableCollection<PropertiesTab> _ListPropertiesTabs;
		[BrowsableAttribute(false)]
		public IEnumerable<IPropertiesTab> ListPropertiesTabsI { get { foreach (var t in _ListPropertiesTabs) yield return t; } }
		public PropertiesTab this[int index] { get { return (PropertiesTab)this.ListPropertiesTabs[index]; } }
		public void Add(PropertiesTab item)  // Property.tt Line: 72
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
		partial void OnListPropertiesTabsChanging(); // Property.tt Line: 134
		partial void OnListPropertiesTabsChanged();
		#endregion Properties
	}
	public partial class PropertiesTab : ConfigObjectBase<PropertiesTab, PropertiesTab.PropertiesTabValidator>, IComparable<PropertiesTab>, IConfigAcceptVisitor, IPropertiesTab // Class.tt Line: 6
	{
		public partial class PropertiesTabValidator : ValidatorBase<PropertiesTab, PropertiesTabValidator> { } 
		#region CTOR
		public PropertiesTab() : base(PropertiesTabValidator.Validator)	{
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 25
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public PropertiesTab(ITreeConfigNode parent) : base(PropertiesTabValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 51
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static PropertiesTab Clone(PropertiesTab from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    PropertiesTab vm = new PropertiesTab();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupProperties = GroupListProperties.Clone(from.GroupProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(from.GroupPropertiesTabs, isDeep);
		    vm.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(PropertiesTab to, PropertiesTab from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
		    to.IsIndexFk = from.IsIndexFk; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override PropertiesTab Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return PropertiesTab.Clone(this);
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
		public static PropertiesTab ConvertToVM(Proto.Config.proto_properties_tab m, PropertiesTab vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new PropertiesTab();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 193
		    GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 193
		    vm.IsIndexFk = m.IsIndexFk; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'PropertiesTab' to 'proto_properties_tab'
		public static Proto.Config.proto_properties_tab ConvertToProto(PropertiesTab vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_properties_tab m = new Proto.Config.proto_properties_tab(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 229
		    m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 229
		    m.IsIndexFk = vm.IsIndexFk; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging();
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupListProperties _GroupProperties;
		[BrowsableAttribute(false)]
		public IGroupListProperties GroupPropertiesI { get { return _GroupProperties; }}
		partial void OnGroupPropertiesChanging(); // Property.tt Line: 134
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupPropertiesTabs != value)
				{
					OnGroupPropertiesTabsChanging();
		            _GroupPropertiesTabs = value;
					OnGroupPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesTabs; }
		}
		private GroupListPropertiesTabs _GroupPropertiesTabs;
		[BrowsableAttribute(false)]
		public IGroupListPropertiesTabs GroupPropertiesTabsI { get { return _GroupPropertiesTabs; }}
		partial void OnGroupPropertiesTabsChanging(); // Property.tt Line: 134
		partial void OnGroupPropertiesTabsChanged();
		
		///////////////////////////////////////////////////
		/// Create Index for foreign key navigation property
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(4)]
		public bool IsIndexFk // Property.tt Line: 115
		{ 
			set
			{
				if (_IsIndexFk != value)
				{
					OnIsIndexFkChanging();
					_IsIndexFk = value;
					OnIsIndexFkChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsIndexFk; }
		}
		private bool _IsIndexFk;
		partial void OnIsIndexFkChanging(); // Property.tt Line: 134
		partial void OnIsIndexFkChanged();
		#endregion Properties
	}
	public partial class GroupListProperties : ConfigObjectBase<GroupListProperties, GroupListProperties.GroupListPropertiesValidator>, IComparable<GroupListProperties>, IConfigAcceptVisitor, IGroupListProperties // Class.tt Line: 6
	{
		public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { } 
		#region CTOR
		public GroupListProperties() : base(GroupListPropertiesValidator.Validator)	{
			this.ListProperties = new SortedObservableCollection<Property>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListProperties(ITreeConfigNode parent) : base(GroupListPropertiesValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListProperties = new SortedObservableCollection<Property>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListProperties Clone(GroupListProperties from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListProperties vm = new GroupListProperties();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListProperties) // Clone.tt Line: 45
		        vm.ListProperties.Add(Property.Clone((Property)t, isDeep));
		    vm.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListProperties to, GroupListProperties from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Property();
		                Property.Update(p, (Property)tt, isDeep);
		                to.ListProperties.Add(p);
		            }
		        }
		    }
		    to.LastGenPosition = from.LastGenPosition; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListProperties Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListProperties.Clone(this);
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
		public static GroupListProperties ConvertToVM(Proto.Config.proto_group_list_properties m, GroupListProperties vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListProperties();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListProperties) // Clone.tt Line: 179
		    {
		        var tvm = Property.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListProperties.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.LastGenPosition = m.LastGenPosition; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListProperties' to 'proto_group_list_properties'
		public static Proto.Config.proto_group_list_properties ConvertToProto(GroupListProperties vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_properties m = new Proto.Config.proto_group_list_properties(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListProperties) // Clone.tt Line: 214
		        m.ListProperties.Add(Property.ConvertToProto((Property)t)); // Clone.tt Line: 218
		    m.LastGenPosition = vm.LastGenPosition; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Property> ListProperties  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListProperties != value)
				{
					OnListPropertiesChanging();
					_ListProperties = value;
					OnListPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListProperties; }
		}
		private SortedObservableCollection<Property> _ListProperties;
		[BrowsableAttribute(false)]
		public IEnumerable<IProperty> ListPropertiesI { get { foreach (var t in _ListProperties) yield return t; } }
		public Property this[int index] { get { return (Property)this.ListProperties[index]; } }
		public void Add(Property item)  // Property.tt Line: 72
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
		partial void OnListPropertiesChanging(); // Property.tt Line: 134
		partial void OnListPropertiesChanged();
		
		///////////////////////////////////////////////////
		/// Last generated Protobuf field position
		///////////////////////////////////////////////////
		[Editable(false)]
		public uint LastGenPosition // Property.tt Line: 115
		{ 
			set
			{
				if (_LastGenPosition != value)
				{
					OnLastGenPositionChanging();
					_LastGenPosition = value;
					OnLastGenPositionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _LastGenPosition; }
		}
		private uint _LastGenPosition;
		partial void OnLastGenPositionChanging(); // Property.tt Line: 134
		partial void OnLastGenPositionChanged();
		#endregion Properties
	}
	public partial class Property : ConfigObjectBase<Property, Property.PropertyValidator>, IComparable<Property>, IConfigAcceptVisitor, IProperty // Class.tt Line: 6
	{
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { } 
		#region CTOR
		public Property() : base(PropertyValidator.Validator)	{
			this.DataType = new DataType(); // Class.tt Line: 23
			OnInit();
		}
	    // Class.tt Line: 34
		public Property(ITreeConfigNode parent) : base(PropertyValidator.Validator)
	    {
	        this.Parent = parent;
			this.DataType = new DataType(); // Class.tt Line: 49
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Property Clone(Property from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Property vm = new Property();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.DataType = DataType.Clone(from.DataType, isDeep);
		    vm.Position = from.Position; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Property to, Property from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        DataType.Update(to.DataType, from.DataType, isDeep);
		    to.Position = from.Position; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Property Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Property.Clone(this);
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
		public static Property ConvertToVM(Proto.Config.proto_property m, Property vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Property();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 193
		    vm.Position = m.Position; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Property' to 'proto_property'
		public static Proto.Config.proto_property ConvertToProto(Property vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_property m = new Proto.Config.proto_property(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 229
		    m.Position = vm.Position; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(4)]
		[ExpandableObjectAttribute()]
		[DisplayName("Type")]
		public DataType DataType // Property.tt Line: 94
		{ 
			set
			{
				if (_DataType != value)
				{
					OnDataTypeChanging();
		            _DataType = value;
					OnDataTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataType; }
		}
		private DataType _DataType;
		[BrowsableAttribute(false)]
		public IDataType DataTypeI { get { return _DataType; }}
		partial void OnDataTypeChanging(); // Property.tt Line: 134
		partial void OnDataTypeChanged();
		
		///////////////////////////////////////////////////
		/// Protobuf field position
		/// Reserved positions: 1 - primary key
		///////////////////////////////////////////////////
		[Editable(false)]
		public uint Position // Property.tt Line: 115
		{ 
			set
			{
				if (_Position != value)
				{
					OnPositionChanging();
					_Position = value;
					OnPositionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Position; }
		}
		private uint _Position;
		partial void OnPositionChanging(); // Property.tt Line: 134
		partial void OnPositionChanged();
		#endregion Properties
	}
	public partial class GroupListConstants : ConfigObjectBase<GroupListConstants, GroupListConstants.GroupListConstantsValidator>, IComparable<GroupListConstants>, IConfigAcceptVisitor, IGroupListConstants // Class.tt Line: 6
	{
		public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { } 
		#region CTOR
		public GroupListConstants() : base(GroupListConstantsValidator.Validator)	{
			this.ListConstants = new SortedObservableCollection<Constant>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListConstants(ITreeConfigNode parent) : base(GroupListConstantsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListConstants = new SortedObservableCollection<Constant>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListConstants Clone(GroupListConstants from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListConstants vm = new GroupListConstants();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListConstants = new SortedObservableCollection<Constant>();
		    foreach(var t in from.ListConstants) // Clone.tt Line: 45
		        vm.ListConstants.Add(Constant.Clone((Constant)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListConstants to, GroupListConstants from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Constant();
		                Constant.Update(p, (Constant)tt, isDeep);
		                to.ListConstants.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListConstants Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListConstants.Clone(this);
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
		public static GroupListConstants ConvertToVM(Proto.Config.proto_group_list_constants m, GroupListConstants vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListConstants();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListConstants = new SortedObservableCollection<Constant>();
		    foreach(var t in m.ListConstants) // Clone.tt Line: 179
		    {
		        var tvm = Constant.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListConstants.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListConstants' to 'proto_group_list_constants'
		public static Proto.Config.proto_group_list_constants ConvertToProto(GroupListConstants vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_constants m = new Proto.Config.proto_group_list_constants(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListConstants) // Clone.tt Line: 214
		        m.ListConstants.Add(Constant.ConvertToProto((Constant)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Constant> ListConstants  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListConstants != value)
				{
					OnListConstantsChanging();
					_ListConstants = value;
					OnListConstantsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListConstants; }
		}
		private SortedObservableCollection<Constant> _ListConstants;
		[BrowsableAttribute(false)]
		public IEnumerable<IConstant> ListConstantsI { get { foreach (var t in _ListConstants) yield return t; } }
		public Constant this[int index] { get { return (Constant)this.ListConstants[index]; } }
		public void Add(Constant item)  // Property.tt Line: 72
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
		partial void OnListConstantsChanging(); // Property.tt Line: 134
		partial void OnListConstantsChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	public partial class Constant : ConfigObjectBase<Constant, Constant.ConstantValidator>, IComparable<Constant>, IConfigAcceptVisitor, IConstant // Class.tt Line: 6
	{
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { } 
		#region CTOR
		public Constant() : base(ConstantValidator.Validator)	{
			this.DataType = new DataType(); // Class.tt Line: 23
			OnInit();
		}
	    // Class.tt Line: 34
		public Constant(ITreeConfigNode parent) : base(ConstantValidator.Validator)
	    {
	        this.Parent = parent;
			this.DataType = new DataType(); // Class.tt Line: 49
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Constant Clone(Constant from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Constant vm = new Constant();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.DataType = DataType.Clone(from.DataType, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Constant to, Constant from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        DataType.Update(to.DataType, from.DataType, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Constant Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Constant.Clone(this);
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
		public static Constant ConvertToVM(Proto.Config.proto_constant m, Constant vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Constant();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    DataType.ConvertToVM(m.DataType, vm.DataType); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Constant' to 'proto_constant'
		public static Proto.Config.proto_constant ConvertToProto(Constant vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_constant m = new Proto.Config.proto_constant(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.DataType = DataType.ConvertToProto(vm.DataType); // Clone.tt Line: 229
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(4)]
		[ExpandableObjectAttribute()]
		[DisplayName("Type")]
		public DataType DataType // Property.tt Line: 94
		{ 
			set
			{
				if (_DataType != value)
				{
					OnDataTypeChanging();
		            _DataType = value;
					OnDataTypeChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataType; }
		}
		private DataType _DataType;
		[BrowsableAttribute(false)]
		public IDataType DataTypeI { get { return _DataType; }}
		partial void OnDataTypeChanging(); // Property.tt Line: 134
		partial void OnDataTypeChanged();
		#endregion Properties
	}
	public partial class GroupListEnumerations : ConfigObjectBase<GroupListEnumerations, GroupListEnumerations.GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IConfigAcceptVisitor, IGroupListEnumerations // Class.tt Line: 6
	{
		public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { } 
		#region CTOR
		public GroupListEnumerations() : base(GroupListEnumerationsValidator.Validator)	{
			this.ListEnumerations = new SortedObservableCollection<Enumeration>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListEnumerations(ITreeConfigNode parent) : base(GroupListEnumerationsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListEnumerations = new SortedObservableCollection<Enumeration>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListEnumerations Clone(GroupListEnumerations from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListEnumerations vm = new GroupListEnumerations();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListEnumerations = new SortedObservableCollection<Enumeration>();
		    foreach(var t in from.ListEnumerations) // Clone.tt Line: 45
		        vm.ListEnumerations.Add(Enumeration.Clone((Enumeration)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListEnumerations to, GroupListEnumerations from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Enumeration();
		                Enumeration.Update(p, (Enumeration)tt, isDeep);
		                to.ListEnumerations.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListEnumerations Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListEnumerations.Clone(this);
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
		public static GroupListEnumerations ConvertToVM(Proto.Config.proto_group_list_enumerations m, GroupListEnumerations vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListEnumerations();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListEnumerations = new SortedObservableCollection<Enumeration>();
		    foreach(var t in m.ListEnumerations) // Clone.tt Line: 179
		    {
		        var tvm = Enumeration.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListEnumerations.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListEnumerations' to 'proto_group_list_enumerations'
		public static Proto.Config.proto_group_list_enumerations ConvertToProto(GroupListEnumerations vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_enumerations m = new Proto.Config.proto_group_list_enumerations(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListEnumerations) // Clone.tt Line: 214
		        m.ListEnumerations.Add(Enumeration.ConvertToProto((Enumeration)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Enumeration> ListEnumerations  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListEnumerations != value)
				{
					OnListEnumerationsChanging();
					_ListEnumerations = value;
					OnListEnumerationsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListEnumerations; }
		}
		private SortedObservableCollection<Enumeration> _ListEnumerations;
		[BrowsableAttribute(false)]
		public IEnumerable<IEnumeration> ListEnumerationsI { get { foreach (var t in _ListEnumerations) yield return t; } }
		public Enumeration this[int index] { get { return (Enumeration)this.ListEnumerations[index]; } }
		public void Add(Enumeration item)  // Property.tt Line: 72
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
		partial void OnListEnumerationsChanging(); // Property.tt Line: 134
		partial void OnListEnumerationsChanged();
		#endregion Properties
	}
	public partial class Enumeration : ConfigObjectBase<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>, IConfigAcceptVisitor, IEnumeration // Class.tt Line: 6
	{
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { } 
		#region CTOR
		public Enumeration() : base(EnumerationValidator.Validator)	{
			this.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public Enumeration(ITreeConfigNode parent) : base(EnumerationValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static Enumeration Clone(Enumeration from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Enumeration vm = new Enumeration();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 58
		    vm.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 58
		    vm.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>();
		    foreach(var t in from.ListEnumerationPairs) // Clone.tt Line: 45
		        vm.ListEnumerationPairs.Add(EnumerationPair.Clone((EnumerationPair)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Enumeration to, Enumeration from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.DataTypeEnum = from.DataTypeEnum; // Clone.tt Line: 126
		    to.DataTypeLength = from.DataTypeLength; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new EnumerationPair();
		                EnumerationPair.Update(p, (EnumerationPair)tt, isDeep);
		                to.ListEnumerationPairs.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Enumeration Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Enumeration.Clone(this);
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
		public static Enumeration ConvertToVM(Proto.Config.proto_enumeration m, Enumeration vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Enumeration();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.DataTypeEnum = (EnumEnumerationType)m.DataTypeEnum; // Clone.tt Line: 197
		    vm.DataTypeLength = m.DataTypeLength; // Clone.tt Line: 199
		    vm.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>();
		    foreach(var t in m.ListEnumerationPairs) // Clone.tt Line: 179
		    {
		        var tvm = EnumerationPair.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListEnumerationPairs.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Enumeration' to 'proto_enumeration'
		public static Proto.Config.proto_enumeration ConvertToProto(Enumeration vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_enumeration m = new Proto.Config.proto_enumeration(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.DataTypeEnum = (Proto.Config.enum_enumeration_type)vm.DataTypeEnum; // Clone.tt Line: 233
		    m.DataTypeLength = vm.DataTypeLength; // Clone.tt Line: 235
		    foreach(var t in vm.ListEnumerationPairs) // Clone.tt Line: 214
		        m.ListEnumerationPairs.Add(EnumerationPair.ConvertToProto((EnumerationPair)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// Enumeration element type
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(4)]
		[DisplayName("Type")]
		public EnumEnumerationType DataTypeEnum // Property.tt Line: 115
		{ 
			set
			{
				if (_DataTypeEnum != value)
				{
					OnDataTypeEnumChanging();
					_DataTypeEnum = value;
					OnDataTypeEnumChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeEnum; }
		}
		private EnumEnumerationType _DataTypeEnum;
		partial void OnDataTypeEnumChanging(); // Property.tt Line: 134
		partial void OnDataTypeEnumChanged();
		
		///////////////////////////////////////////////////
		/// Length of string if 'STRING' is selected as enumeration element type
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(5)]
		[DisplayName("Length")]
		public int DataTypeLength // Property.tt Line: 115
		{ 
			set
			{
				if (_DataTypeLength != value)
				{
					OnDataTypeLengthChanging();
					_DataTypeLength = value;
					OnDataTypeLengthChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DataTypeLength; }
		}
		private int _DataTypeLength;
		partial void OnDataTypeLengthChanging(); // Property.tt Line: 134
		partial void OnDataTypeLengthChanged();
		[DisplayName("Elements")]
		[NewItemTypes(typeof(EnumerationPair))]
		public SortedObservableCollection<EnumerationPair> ListEnumerationPairs  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListEnumerationPairs != value)
				{
					OnListEnumerationPairsChanging();
					_ListEnumerationPairs = value;
					OnListEnumerationPairsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListEnumerationPairs; }
		}
		private SortedObservableCollection<EnumerationPair> _ListEnumerationPairs;
		[BrowsableAttribute(false)]
		public IEnumerable<IEnumerationPair> ListEnumerationPairsI { get { foreach (var t in _ListEnumerationPairs) yield return t; } }
		partial void OnListEnumerationPairsChanging(); // Property.tt Line: 134
		partial void OnListEnumerationPairsChanged();
		#endregion Properties
	}
	public partial class EnumerationPair : ConfigObjectBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>, IConfigAcceptVisitor, IEnumerationPair // Class.tt Line: 6
	{
		public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { } 
		#region CTOR
		public EnumerationPair() : base(EnumerationPairValidator.Validator)	{
			OnInit();
		}
	    // Class.tt Line: 34
		public EnumerationPair(ITreeConfigNode parent) : base(EnumerationPairValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static EnumerationPair Clone(EnumerationPair from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    EnumerationPair vm = new EnumerationPair();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.Value = from.Value; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(EnumerationPair to, EnumerationPair from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    to.Value = from.Value; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override EnumerationPair Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return EnumerationPair.Clone(this);
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
		public static EnumerationPair ConvertToVM(Proto.Config.proto_enumeration_pair m, EnumerationPair vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new EnumerationPair();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.Value = m.Value; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
		public static Proto.Config.proto_enumeration_pair ConvertToProto(EnumerationPair vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_enumeration_pair m = new Proto.Config.proto_enumeration_pair(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.Value = vm.Value; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		public string Value // Property.tt Line: 115
		{ 
			set
			{
				if (_Value != value)
				{
					OnValueChanging();
					_Value = value;
					OnValueChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Value; }
		}
		private string _Value = "";
		partial void OnValueChanging(); // Property.tt Line: 134
		partial void OnValueChanged();
		#endregion Properties
	}
	public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IConfigAcceptVisitor, ICatalog // Class.tt Line: 6
	{
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { } 
		#region CTOR
		public Catalog() : base(CatalogValidator.Validator)	{
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 25
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 25
			this.GroupForms = new GroupListForms(this); // Class.tt Line: 25
			this.GroupReports = new GroupListReports(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public Catalog(ITreeConfigNode parent) : base(CatalogValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 51
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 51
			this.GroupForms = new GroupListForms(this); // Class.tt Line: 51
			this.GroupReports = new GroupListReports(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Catalog Clone(Catalog from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Catalog vm = new Catalog();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupProperties = GroupListProperties.Clone(from.GroupProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(from.GroupPropertiesTabs, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupForms = GroupListForms.Clone(from.GroupForms, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupReports = GroupListReports.Clone(from.GroupReports, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Catalog to, Catalog from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Catalog Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Catalog.Clone(this);
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
		public static Catalog ConvertToVM(Proto.Config.proto_catalog m, Catalog vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Catalog();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 193
		    GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 193
		    GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 193
		    GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Catalog' to 'proto_catalog'
		public static Proto.Config.proto_catalog ConvertToProto(Catalog vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_catalog m = new Proto.Config.proto_catalog(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 229
		    m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 229
		    m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 229
		    m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 229
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging();
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupListProperties _GroupProperties;
		[BrowsableAttribute(false)]
		public IGroupListProperties GroupPropertiesI { get { return _GroupProperties; }}
		partial void OnGroupPropertiesChanging(); // Property.tt Line: 134
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupPropertiesTabs != value)
				{
					OnGroupPropertiesTabsChanging();
		            _GroupPropertiesTabs = value;
					OnGroupPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesTabs; }
		}
		private GroupListPropertiesTabs _GroupPropertiesTabs;
		[BrowsableAttribute(false)]
		public IGroupListPropertiesTabs GroupPropertiesTabsI { get { return _GroupPropertiesTabs; }}
		partial void OnGroupPropertiesTabsChanging(); // Property.tt Line: 134
		partial void OnGroupPropertiesTabsChanged();
		[BrowsableAttribute(false)]
		public GroupListForms GroupForms // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupForms != value)
				{
					OnGroupFormsChanging();
		            _GroupForms = value;
					OnGroupFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupForms; }
		}
		private GroupListForms _GroupForms;
		[BrowsableAttribute(false)]
		public IGroupListForms GroupFormsI { get { return _GroupForms; }}
		partial void OnGroupFormsChanging(); // Property.tt Line: 134
		partial void OnGroupFormsChanged();
		[BrowsableAttribute(false)]
		public GroupListReports GroupReports // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupReports != value)
				{
					OnGroupReportsChanging();
		            _GroupReports = value;
					OnGroupReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupReports; }
		}
		private GroupListReports _GroupReports;
		[BrowsableAttribute(false)]
		public IGroupListReports GroupReportsI { get { return _GroupReports; }}
		partial void OnGroupReportsChanging(); // Property.tt Line: 134
		partial void OnGroupReportsChanged();
		#endregion Properties
	}
	public partial class GroupListCatalogs : ConfigObjectBase<GroupListCatalogs, GroupListCatalogs.GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IConfigAcceptVisitor, IGroupListCatalogs // Class.tt Line: 6
	{
		public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { } 
		#region CTOR
		public GroupListCatalogs() : base(GroupListCatalogsValidator.Validator)	{
			this.ListCatalogs = new SortedObservableCollection<Catalog>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListCatalogs(ITreeConfigNode parent) : base(GroupListCatalogsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListCatalogs = new SortedObservableCollection<Catalog>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListCatalogs Clone(GroupListCatalogs from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListCatalogs vm = new GroupListCatalogs();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in from.ListCatalogs) // Clone.tt Line: 45
		        vm.ListCatalogs.Add(Catalog.Clone((Catalog)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListCatalogs to, GroupListCatalogs from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Catalog();
		                Catalog.Update(p, (Catalog)tt, isDeep);
		                to.ListCatalogs.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListCatalogs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListCatalogs.Clone(this);
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
		public static GroupListCatalogs ConvertToVM(Proto.Config.proto_group_list_catalogs m, GroupListCatalogs vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListCatalogs();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in m.ListCatalogs) // Clone.tt Line: 179
		    {
		        var tvm = Catalog.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListCatalogs.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
		public static Proto.Config.proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_catalogs m = new Proto.Config.proto_group_list_catalogs(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListCatalogs) // Clone.tt Line: 214
		        m.ListCatalogs.Add(Catalog.ConvertToProto((Catalog)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Catalog> ListCatalogs  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListCatalogs != value)
				{
					OnListCatalogsChanging();
					_ListCatalogs = value;
					OnListCatalogsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListCatalogs; }
		}
		private SortedObservableCollection<Catalog> _ListCatalogs;
		[BrowsableAttribute(false)]
		public IEnumerable<ICatalog> ListCatalogsI { get { foreach (var t in _ListCatalogs) yield return t; } }
		public Catalog this[int index] { get { return (Catalog)this.ListCatalogs[index]; } }
		public void Add(Catalog item)  // Property.tt Line: 72
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
		partial void OnListCatalogsChanging(); // Property.tt Line: 134
		partial void OnListCatalogsChanged();
		#endregion Properties
	}
	public partial class GroupDocuments : ConfigObjectBase<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IConfigAcceptVisitor, IGroupDocuments // Class.tt Line: 6
	{
		public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { } 
		#region CTOR
		public GroupDocuments() : base(GroupDocumentsValidator.Validator)	{
			this.GroupSharedProperties = new GroupListProperties(this); // Class.tt Line: 25
			this.GroupListDocuments = new GroupListDocuments(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupDocuments(ITreeConfigNode parent) : base(GroupDocumentsValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupSharedProperties = new GroupListProperties(this); // Class.tt Line: 51
			this.GroupListDocuments = new GroupListDocuments(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static GroupDocuments Clone(GroupDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupDocuments vm = new GroupDocuments();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupSharedProperties = GroupListProperties.Clone(from.GroupSharedProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupListDocuments = GroupListDocuments.Clone(from.GroupListDocuments, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupDocuments to, GroupDocuments from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        GroupListProperties.Update(to.GroupSharedProperties, from.GroupSharedProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListDocuments.Update(to.GroupListDocuments, from.GroupListDocuments, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupDocuments Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupDocuments.Clone(this);
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
		public static GroupDocuments ConvertToVM(Proto.Config.proto_group_documents m, GroupDocuments vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupDocuments();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    GroupListProperties.ConvertToVM(m.GroupSharedProperties, vm.GroupSharedProperties); // Clone.tt Line: 193
		    GroupListDocuments.ConvertToVM(m.GroupListDocuments, vm.GroupListDocuments); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupDocuments' to 'proto_group_documents'
		public static Proto.Config.proto_group_documents ConvertToProto(GroupDocuments vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_documents m = new Proto.Config.proto_group_documents(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.GroupSharedProperties = GroupListProperties.ConvertToProto(vm.GroupSharedProperties); // Clone.tt Line: 229
		    m.GroupListDocuments = GroupListDocuments.ConvertToProto(vm.GroupListDocuments); // Clone.tt Line: 229
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupSharedProperties // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupSharedProperties != value)
				{
					OnGroupSharedPropertiesChanging();
		            _GroupSharedProperties = value;
					OnGroupSharedPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupSharedProperties; }
		}
		private GroupListProperties _GroupSharedProperties;
		[BrowsableAttribute(false)]
		public IGroupListProperties GroupSharedPropertiesI { get { return _GroupSharedProperties; }}
		partial void OnGroupSharedPropertiesChanging(); // Property.tt Line: 134
		partial void OnGroupSharedPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListDocuments GroupListDocuments // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupListDocuments != value)
				{
					OnGroupListDocumentsChanging();
		            _GroupListDocuments = value;
					OnGroupListDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupListDocuments; }
		}
		private GroupListDocuments _GroupListDocuments;
		[BrowsableAttribute(false)]
		public IGroupListDocuments GroupListDocumentsI { get { return _GroupListDocuments; }}
		partial void OnGroupListDocumentsChanging(); // Property.tt Line: 134
		partial void OnGroupListDocumentsChanged();
		#endregion Properties
	}
	public partial class Document : ConfigObjectBase<Document, Document.DocumentValidator>, IComparable<Document>, IConfigAcceptVisitor, IDocument // Class.tt Line: 6
	{
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { } 
		#region CTOR
		public Document() : base(DocumentValidator.Validator)	{
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 25
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 25
			this.GroupForms = new GroupListForms(this); // Class.tt Line: 25
			this.GroupReports = new GroupListReports(this); // Class.tt Line: 25
			OnInit();
		}
	    // Class.tt Line: 34
		public Document(ITreeConfigNode parent) : base(DocumentValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupProperties = new GroupListProperties(this); // Class.tt Line: 51
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this); // Class.tt Line: 51
			this.GroupForms = new GroupListForms(this); // Class.tt Line: 51
			this.GroupReports = new GroupListReports(this); // Class.tt Line: 51
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Document Clone(Document from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Document vm = new Document();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupProperties = GroupListProperties.Clone(from.GroupProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupPropertiesTabs = GroupListPropertiesTabs.Clone(from.GroupPropertiesTabs, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupForms = GroupListForms.Clone(from.GroupForms, isDeep);
		    if (isDeep) // Clone.tt Line: 55
		        vm.GroupReports = GroupListReports.Clone(from.GroupReports, isDeep);
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Document to, Document from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 123
		        GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
		    if (isDeep) // Clone.tt Line: 123
		        GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Document Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Document.Clone(this);
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
		public static Document ConvertToVM(Proto.Config.proto_document m, Document vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Document();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties); // Clone.tt Line: 193
		    GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs); // Clone.tt Line: 193
		    GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms); // Clone.tt Line: 193
		    GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports); // Clone.tt Line: 193
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Document' to 'proto_document'
		public static Proto.Config.proto_document ConvertToProto(Document vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_document m = new Proto.Config.proto_document(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    m.GroupProperties = GroupListProperties.ConvertToProto(vm.GroupProperties); // Clone.tt Line: 229
		    m.GroupPropertiesTabs = GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs); // Clone.tt Line: 229
		    m.GroupForms = GroupListForms.ConvertToProto(vm.GroupForms); // Clone.tt Line: 229
		    m.GroupReports = GroupListReports.ConvertToProto(vm.GroupReports); // Clone.tt Line: 229
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupProperties != value)
				{
					OnGroupPropertiesChanging();
		            _GroupProperties = value;
					OnGroupPropertiesChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupProperties; }
		}
		private GroupListProperties _GroupProperties;
		[BrowsableAttribute(false)]
		public IGroupListProperties GroupPropertiesI { get { return _GroupProperties; }}
		partial void OnGroupPropertiesChanging(); // Property.tt Line: 134
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesTabs // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupPropertiesTabs != value)
				{
					OnGroupPropertiesTabsChanging();
		            _GroupPropertiesTabs = value;
					OnGroupPropertiesTabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesTabs; }
		}
		private GroupListPropertiesTabs _GroupPropertiesTabs;
		[BrowsableAttribute(false)]
		public IGroupListPropertiesTabs GroupPropertiesTabsI { get { return _GroupPropertiesTabs; }}
		partial void OnGroupPropertiesTabsChanging(); // Property.tt Line: 134
		partial void OnGroupPropertiesTabsChanged();
		[BrowsableAttribute(false)]
		public GroupListForms GroupForms // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupForms != value)
				{
					OnGroupFormsChanging();
		            _GroupForms = value;
					OnGroupFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupForms; }
		}
		private GroupListForms _GroupForms;
		[BrowsableAttribute(false)]
		public IGroupListForms GroupFormsI { get { return _GroupForms; }}
		partial void OnGroupFormsChanging(); // Property.tt Line: 134
		partial void OnGroupFormsChanged();
		[BrowsableAttribute(false)]
		public GroupListReports GroupReports // Property.tt Line: 94
		{ 
			set
			{
				if (_GroupReports != value)
				{
					OnGroupReportsChanging();
		            _GroupReports = value;
					OnGroupReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupReports; }
		}
		private GroupListReports _GroupReports;
		[BrowsableAttribute(false)]
		public IGroupListReports GroupReportsI { get { return _GroupReports; }}
		partial void OnGroupReportsChanging(); // Property.tt Line: 134
		partial void OnGroupReportsChanged();
		#endregion Properties
	}
	public partial class GroupListDocuments : ConfigObjectBase<GroupListDocuments, GroupListDocuments.GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IConfigAcceptVisitor, IGroupListDocuments // Class.tt Line: 6
	{
		public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { } 
		#region CTOR
		public GroupListDocuments() : base(GroupListDocumentsValidator.Validator)	{
			this.ListDocuments = new SortedObservableCollection<Document>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListDocuments(ITreeConfigNode parent) : base(GroupListDocumentsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListDocuments = new SortedObservableCollection<Document>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListDocuments Clone(GroupListDocuments from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListDocuments vm = new GroupListDocuments();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in from.ListDocuments) // Clone.tt Line: 45
		        vm.ListDocuments.Add(Document.Clone((Document)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListDocuments to, GroupListDocuments from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Document();
		                Document.Update(p, (Document)tt, isDeep);
		                to.ListDocuments.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListDocuments Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListDocuments.Clone(this);
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
		public static GroupListDocuments ConvertToVM(Proto.Config.proto_group_list_documents m, GroupListDocuments vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListDocuments();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in m.ListDocuments) // Clone.tt Line: 179
		    {
		        var tvm = Document.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListDocuments.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
		public static Proto.Config.proto_group_list_documents ConvertToProto(GroupListDocuments vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_documents m = new Proto.Config.proto_group_list_documents(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListDocuments) // Clone.tt Line: 214
		        m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Document> ListDocuments  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListDocuments != value)
				{
					OnListDocumentsChanging();
					_ListDocuments = value;
					OnListDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListDocuments; }
		}
		private SortedObservableCollection<Document> _ListDocuments;
		[BrowsableAttribute(false)]
		public IEnumerable<IDocument> ListDocumentsI { get { foreach (var t in _ListDocuments) yield return t; } }
		public Document this[int index] { get { return (Document)this.ListDocuments[index]; } }
		public void Add(Document item)  // Property.tt Line: 72
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
		partial void OnListDocumentsChanging(); // Property.tt Line: 134
		partial void OnListDocumentsChanged();
		#endregion Properties
	}
	public partial class GroupListJournals : ConfigObjectBase<GroupListJournals, GroupListJournals.GroupListJournalsValidator>, IComparable<GroupListJournals>, IConfigAcceptVisitor, IGroupListJournals // Class.tt Line: 6
	{
		public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { } 
		#region CTOR
		public GroupListJournals() : base(GroupListJournalsValidator.Validator)	{
			this.ListJournals = new SortedObservableCollection<Journal>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListJournals(ITreeConfigNode parent) : base(GroupListJournalsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListJournals = new SortedObservableCollection<Journal>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListJournals Clone(GroupListJournals from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListJournals vm = new GroupListJournals();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListJournals = new SortedObservableCollection<Journal>();
		    foreach(var t in from.ListJournals) // Clone.tt Line: 45
		        vm.ListJournals.Add(Journal.Clone((Journal)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListJournals to, GroupListJournals from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Journal();
		                Journal.Update(p, (Journal)tt, isDeep);
		                to.ListJournals.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListJournals Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListJournals.Clone(this);
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
		public static GroupListJournals ConvertToVM(Proto.Config.proto_group_list_journals m, GroupListJournals vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListJournals();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListJournals = new SortedObservableCollection<Journal>();
		    foreach(var t in m.ListJournals) // Clone.tt Line: 179
		    {
		        var tvm = Journal.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListJournals.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListJournals' to 'proto_group_list_journals'
		public static Proto.Config.proto_group_list_journals ConvertToProto(GroupListJournals vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_journals m = new Proto.Config.proto_group_list_journals(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListJournals) // Clone.tt Line: 214
		        m.ListJournals.Add(Journal.ConvertToProto((Journal)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Journal> ListJournals  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListJournals != value)
				{
					OnListJournalsChanging();
					_ListJournals = value;
					OnListJournalsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListJournals; }
		}
		private SortedObservableCollection<Journal> _ListJournals;
		[BrowsableAttribute(false)]
		public IEnumerable<IJournal> ListJournalsI { get { foreach (var t in _ListJournals) yield return t; } }
		public Journal this[int index] { get { return (Journal)this.ListJournals[index]; } }
		public void Add(Journal item)  // Property.tt Line: 72
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
		partial void OnListJournalsChanging(); // Property.tt Line: 134
		partial void OnListJournalsChanged();
		#endregion Properties
	}
	public partial class Journal : ConfigObjectBase<Journal, Journal.JournalValidator>, IComparable<Journal>, IConfigAcceptVisitor, IJournal // Class.tt Line: 6
	{
		public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { } 
		#region CTOR
		public Journal() : base(JournalValidator.Validator)	{
			this.ListDocuments = new SortedObservableCollection<Document>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public Journal(ITreeConfigNode parent) : base(JournalValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListDocuments = new SortedObservableCollection<Document>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static Journal Clone(Journal from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Journal vm = new Journal();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in from.ListDocuments) // Clone.tt Line: 45
		        vm.ListDocuments.Add(Document.Clone((Document)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Journal to, Journal from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Document();
		                Document.Update(p, (Document)tt, isDeep);
		                to.ListDocuments.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Journal Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Journal.Clone(this);
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
		public static Journal ConvertToVM(Proto.Config.proto_journal m, Journal vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Journal();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in m.ListDocuments) // Clone.tt Line: 179
		    {
		        var tvm = Document.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListDocuments.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Journal' to 'proto_journal'
		public static Proto.Config.proto_journal ConvertToProto(Journal vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_journal m = new Proto.Config.proto_journal(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListDocuments) // Clone.tt Line: 214
		        m.ListDocuments.Add(Document.ConvertToProto((Document)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Document> ListDocuments  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListDocuments != value)
				{
					OnListDocumentsChanging();
					_ListDocuments = value;
					OnListDocumentsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListDocuments; }
		}
		private SortedObservableCollection<Document> _ListDocuments;
		[BrowsableAttribute(false)]
		public IEnumerable<IDocument> ListDocumentsI { get { foreach (var t in _ListDocuments) yield return t; } }
		partial void OnListDocumentsChanging(); // Property.tt Line: 134
		partial void OnListDocumentsChanged();
		#endregion Properties
	}
	public partial class GroupListForms : ConfigObjectBase<GroupListForms, GroupListForms.GroupListFormsValidator>, IComparable<GroupListForms>, IConfigAcceptVisitor, IGroupListForms // Class.tt Line: 6
	{
		public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { } 
		#region CTOR
		public GroupListForms() : base(GroupListFormsValidator.Validator)	{
			this.ListForms = new SortedObservableCollection<Form>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListForms(ITreeConfigNode parent) : base(GroupListFormsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListForms = new SortedObservableCollection<Form>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListForms Clone(GroupListForms from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListForms vm = new GroupListForms();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListForms = new SortedObservableCollection<Form>();
		    foreach(var t in from.ListForms) // Clone.tt Line: 45
		        vm.ListForms.Add(Form.Clone((Form)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListForms to, GroupListForms from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Form();
		                Form.Update(p, (Form)tt, isDeep);
		                to.ListForms.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListForms Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListForms.Clone(this);
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
		public static GroupListForms ConvertToVM(Proto.Config.proto_group_list_forms m, GroupListForms vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListForms();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListForms = new SortedObservableCollection<Form>();
		    foreach(var t in m.ListForms) // Clone.tt Line: 179
		    {
		        var tvm = Form.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListForms.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListForms' to 'proto_group_list_forms'
		public static Proto.Config.proto_group_list_forms ConvertToProto(GroupListForms vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_forms m = new Proto.Config.proto_group_list_forms(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListForms) // Clone.tt Line: 214
		        m.ListForms.Add(Form.ConvertToProto((Form)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Form> ListForms  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListForms != value)
				{
					OnListFormsChanging();
					_ListForms = value;
					OnListFormsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListForms; }
		}
		private SortedObservableCollection<Form> _ListForms;
		[BrowsableAttribute(false)]
		public IEnumerable<IForm> ListFormsI { get { foreach (var t in _ListForms) yield return t; } }
		public Form this[int index] { get { return (Form)this.ListForms[index]; } }
		public void Add(Form item)  // Property.tt Line: 72
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
		partial void OnListFormsChanging(); // Property.tt Line: 134
		partial void OnListFormsChanged();
		#endregion Properties
	}
	public partial class Form : ConfigObjectBase<Form, Form.FormValidator>, IComparable<Form>, IConfigAcceptVisitor, IForm // Class.tt Line: 6
	{
		public partial class FormValidator : ValidatorBase<Form, FormValidator> { } 
		#region CTOR
		public Form() : base(FormValidator.Validator)	{
			OnInit();
		}
	    // Class.tt Line: 34
		public Form(ITreeConfigNode parent) : base(FormValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Form Clone(Form from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Form vm = new Form();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Form to, Form from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Form Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Form.Clone(this);
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
		public static Form ConvertToVM(Proto.Config.proto_form m, Form vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Form();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Form' to 'proto_form'
		public static Proto.Config.proto_form ConvertToProto(Form vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_form m = new Proto.Config.proto_form(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		#endregion Properties
	}
	public partial class GroupListReports : ConfigObjectBase<GroupListReports, GroupListReports.GroupListReportsValidator>, IComparable<GroupListReports>, IConfigAcceptVisitor, IGroupListReports // Class.tt Line: 6
	{
		public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { } 
		#region CTOR
		public GroupListReports() : base(GroupListReportsValidator.Validator)	{
			this.ListReports = new SortedObservableCollection<Report>(); // Class.tt Line: 19
			OnInit();
		}
	    // Class.tt Line: 34
		public GroupListReports(ITreeConfigNode parent) : base(GroupListReportsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListReports = new SortedObservableCollection<Report>(); // Class.tt Line: 45
			OnInit();
	    }
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
		public static GroupListReports Clone(GroupListReports from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    GroupListReports vm = new GroupListReports();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    vm.ListReports = new SortedObservableCollection<Report>();
		    foreach(var t in from.ListReports) // Clone.tt Line: 45
		        vm.ListReports.Add(Report.Clone((Report)t, isDeep));
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListReports to, GroupListReports from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		    if (isDeep) // Clone.tt Line: 75
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
		                var p = new Report();
		                Report.Update(p, (Report)tt, isDeep);
		                to.ListReports.Add(p);
		            }
		        }
		    }
		}
		// Clone.tt Line: 132
		#region IEditable
		public override GroupListReports Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupListReports.Clone(this);
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
		public static GroupListReports ConvertToVM(Proto.Config.proto_group_list_reports m, GroupListReports vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new GroupListReports();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.ListReports = new SortedObservableCollection<Report>();
		    foreach(var t in m.ListReports) // Clone.tt Line: 179
		    {
		        var tvm = Report.ConvertToVM(t);
		        tvm.Parent = vm; // Clone.tt Line: 183
		        vm.ListReports.Add(tvm); // Clone.tt Line: 185
		    }
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'GroupListReports' to 'proto_group_list_reports'
		public static Proto.Config.proto_group_list_reports ConvertToProto(GroupListReports vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_group_list_reports m = new Proto.Config.proto_group_list_reports(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
		    foreach(var t in vm.ListReports) // Clone.tt Line: 214
		        m.ListReports.Add(Report.ConvertToProto((Report)t)); // Clone.tt Line: 218
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Report> ListReports  // Property.tt Line: 49
		{ 
			set
			{
				if (_ListReports != value)
				{
					OnListReportsChanging();
					_ListReports = value;
					OnListReportsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListReports; }
		}
		private SortedObservableCollection<Report> _ListReports;
		[BrowsableAttribute(false)]
		public IEnumerable<IReport> ListReportsI { get { foreach (var t in _ListReports) yield return t; } }
		public Report this[int index] { get { return (Report)this.ListReports[index]; } }
		public void Add(Report item)  // Property.tt Line: 72
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
		partial void OnListReportsChanging(); // Property.tt Line: 134
		partial void OnListReportsChanged();
		#endregion Properties
	}
	public partial class Report : ConfigObjectBase<Report, Report.ReportValidator>, IComparable<Report>, IConfigAcceptVisitor, IReport // Class.tt Line: 6
	{
		public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { } 
		#region CTOR
		public Report() : base(ReportValidator.Validator)	{
			OnInit();
		}
	    // Class.tt Line: 34
		public Report(ITreeConfigNode parent) : base(ReportValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		
		public override void Sort(Type type) // Clone.tt Line: 8
		{
		    //throw new Exception();
		}
		public static Report Clone(Report from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
		{
		    Report vm = new Report();
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.SortingValue = from.SortingValue; // Clone.tt Line: 58
		    vm.NameUi = from.NameUi; // Clone.tt Line: 58
		    vm.Description = from.Description; // Clone.tt Line: 58
		    if (isNewGuid) // Clone.tt Line: 63
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Report to, Report from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.SortingValue = from.SortingValue; // Clone.tt Line: 126
		    to.NameUi = from.NameUi; // Clone.tt Line: 126
		    to.Description = from.Description; // Clone.tt Line: 126
		}
		// Clone.tt Line: 132
		#region IEditable
		public override Report Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return Report.Clone(this);
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
		public static Report ConvertToVM(Proto.Config.proto_report m, Report vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new Report();
		    if (m == null)
		        return vm;
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.SortingValue = m.SortingValue; // Clone.tt Line: 199
		    vm.NameUi = m.NameUi; // Clone.tt Line: 199
		    vm.Description = m.Description; // Clone.tt Line: 199
		    vm.OnInitFromDto(); // Clone.tt Line: 204
		    return vm;
		}
		// Conversion from 'Report' to 'proto_report'
		public static Proto.Config.proto_report ConvertToProto(Report vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_report m = new Proto.Config.proto_report(); // Clone.tt Line: 211
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.SortingValue = vm.SortingValue; // Clone.tt Line: 235
		    m.NameUi = vm.NameUi; // Clone.tt Line: 235
		    m.Description = vm.Description; // Clone.tt Line: 235
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
		public string Description // Property.tt Line: 115
		{ 
			set
			{
				if (_Description != value)
				{
					OnDescriptionChanging();
					_Description = value;
					OnDescriptionChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Description; }
		}
		private string _Description = "";
		partial void OnDescriptionChanging(); // Property.tt Line: 134
		partial void OnDescriptionChanged();
		#endregion Properties
	}
	public partial class SubModelRow : ViewModelBindable<SubModelRow>, ISubModelRow // Class.tt Line: 6
	{
		public partial class SubModelRowValidator : ValidatorBase<SubModelRow, SubModelRowValidator> { } 
		#region CTOR
		public SubModelRow()	{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static SubModelRow Clone(SubModelRow from, bool isDeep = true) // Clone.tt Line: 27
		{
		    SubModelRow vm = new SubModelRow();
		    vm.GroupName = from.GroupName; // Clone.tt Line: 58
		    vm.Name = from.Name; // Clone.tt Line: 58
		    vm.Guid = from.Guid; // Clone.tt Line: 58
		    vm.IsIncluded = from.IsIncluded; // Clone.tt Line: 58
		    return vm;
		}
		public static void Update(SubModelRow to, SubModelRow from, bool isDeep = true) // Clone.tt Line: 68
		{
		    to.GroupName = from.GroupName; // Clone.tt Line: 126
		    to.Name = from.Name; // Clone.tt Line: 126
		    to.Guid = from.Guid; // Clone.tt Line: 126
		    to.IsIncluded = from.IsIncluded; // Clone.tt Line: 126
		}
		// Conversion from 'proto_sub_model_row' to 'SubModelRow'
		public static SubModelRow ConvertToVM(Proto.Config.proto_sub_model_row m, SubModelRow vm = null) // Clone.tt Line: 151
		{
		    if (vm == null)
		        vm = new SubModelRow();
		    if (m == null)
		        return vm;
		    vm.GroupName = m.GroupName; // Clone.tt Line: 199
		    vm.Name = m.Name; // Clone.tt Line: 199
		    vm.Guid = m.Guid; // Clone.tt Line: 199
		    vm.IsIncluded = m.IsIncluded; // Clone.tt Line: 199
		    return vm;
		}
		// Conversion from 'SubModelRow' to 'proto_sub_model_row'
		public static Proto.Config.proto_sub_model_row ConvertToProto(SubModelRow vm) // Clone.tt Line: 209
		{
		    Proto.Config.proto_sub_model_row m = new Proto.Config.proto_sub_model_row(); // Clone.tt Line: 211
		    m.GroupName = vm.GroupName; // Clone.tt Line: 235
		    m.Name = vm.Name; // Clone.tt Line: 235
		    m.Guid = vm.Guid; // Clone.tt Line: 235
		    m.IsIncluded = vm.IsIncluded; // Clone.tt Line: 235
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		public string GroupName // Property.tt Line: 115
		{ 
			set
			{
				if (_GroupName != value)
				{
					OnGroupNameChanging();
					_GroupName = value;
					OnGroupNameChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _GroupName; }
		}
		private string _GroupName = "";
		partial void OnGroupNameChanging(); // Property.tt Line: 134
		partial void OnGroupNameChanged();
		public string Name // Property.tt Line: 115
		{ 
			set
			{
				if (_Name != value)
				{
					OnNameChanging();
					_Name = value;
					OnNameChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _Name; }
		}
		private string _Name = "";
		partial void OnNameChanging(); // Property.tt Line: 134
		partial void OnNameChanged();
		public string Guid // Property.tt Line: 115
		{ 
			set
			{
				if (_Guid != value)
				{
					OnGuidChanging();
					_Guid = value;
					OnGuidChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _Guid; }
		}
		private string _Guid = "";
		partial void OnGuidChanging(); // Property.tt Line: 134
		partial void OnGuidChanged();
		public bool IsIncluded // Property.tt Line: 115
		{ 
			set
			{
				if (_IsIncluded != value)
				{
					OnIsIncludedChanging();
					_IsIncluded = value;
					OnIsIncludedChanged();
					NotifyPropertyChanged();
				}
			}
			get { return _IsIncluded; }
		}
		private bool _IsIncluded;
		partial void OnIsIncludedChanging(); // Property.tt Line: 134
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
		void Visit(Proto.Config.proto_app_db_settings p);
		void Visit(Proto.Config.proto_group_list_app_solutions p);
		void Visit(Proto.Config.proto_app_solution p);
		void Visit(Proto.Config.proto_app_project p);
		void Visit(Proto.Config.proto_config_short_history p);
		void Visit(Proto.Config.proto_group_list_base_configs p);
		void Visit(Proto.Config.proto_base_config p);
		void Visit(Proto.Config.proto_config p);
		void Visit(Proto.Config.proto_group_list_sub_models p);
		void Visit(Proto.Config.proto_object_inclusion_record p);
		void Visit(Proto.Config.proto_sub_model p);
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
		void Visit(Proto.Config.proto_sub_model_row p);
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
		protected override void OnVisit(ConfigShortHistory p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(ConfigShortHistory p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(GroupListBaseConfigs p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListBaseConfigs p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(BaseConfig p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(BaseConfig p) // ValidationVisitor.tt Line: 35
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
		protected override void OnVisit(GroupListSubModels p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(GroupListSubModels p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(ObjectInclusionRecord p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(ObjectInclusionRecord p) // ValidationVisitor.tt Line: 35
	    {
	        OnVisitEnd(p as IValidatableWithSeverity);
	    }
		protected override void OnVisit(SubModel p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	        foreach(var t in p.ListObjectInclusionRecords) // ValidationVisitor.tt Line: 26
	            ValidateSubAndCollectErrors(p, t);
	        foreach(var t in p.ListGroupObjects) // ValidationVisitor.tt Line: 26
	            ValidateSubAndCollectErrors(p, t);
	    }
		protected override void OnVisitEnd(SubModel p) // ValidationVisitor.tt Line: 35
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
		protected override void OnVisit(SubModelRow p) // ValidationVisitor.tt Line: 15
	    {
	        OnVisit(p as IValidatableWithSeverity);
	    }
		protected override void OnVisitEnd(SubModelRow p) // ValidationVisitor.tt Line: 35
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
		public void Visit(GroupListBaseConfigs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListBaseConfigs p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListBaseConfigs p) {}
	    protected virtual void OnVisitEnd(GroupListBaseConfigs p) {}
		public void Visit(BaseConfig p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(BaseConfig p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(BaseConfig p) {}
	    protected virtual void OnVisitEnd(BaseConfig p) {}
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
		public void Visit(GroupListSubModels p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListSubModels p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupListSubModels p) {}
	    protected virtual void OnVisitEnd(GroupListSubModels p) {}
		public void Visit(ObjectInclusionRecord p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ObjectInclusionRecord p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(ObjectInclusionRecord p) {}
	    protected virtual void OnVisitEnd(ObjectInclusionRecord p) {}
		public void Visit(SubModel p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(SubModel p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(SubModel p) {}
	    protected virtual void OnVisitEnd(SubModel p) {}
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
		public void Visit(SubModelRow p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(SubModelRow p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(SubModelRow p) {}
	    protected virtual void OnVisitEnd(SubModelRow p) {}
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
	void Visit(GroupListAppSolutions p);
	void VisitEnd(GroupListAppSolutions p);
	void Visit(AppSolution p);
	void VisitEnd(AppSolution p);
	void Visit(AppProject p);
	void VisitEnd(AppProject p);
	void Visit(ConfigShortHistory p);
	void VisitEnd(ConfigShortHistory p);
	void Visit(GroupListBaseConfigs p);
	void VisitEnd(GroupListBaseConfigs p);
	void Visit(BaseConfig p);
	void VisitEnd(BaseConfig p);
	void Visit(Config p);
	void VisitEnd(Config p);
	void Visit(GroupListSubModels p);
	void VisitEnd(GroupListSubModels p);
	void Visit(SubModel p);
	void VisitEnd(SubModel p);
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