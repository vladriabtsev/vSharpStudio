// Auto generated on UTC 06/05/2019 21:34:53
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

namespace vSharpStudio.vm.ViewModels
{
    // TODO investigate  https://docs.microsoft.com/en-us/visualstudio/debugger/using-debuggertypeproxy-attribute?view=vs-2017
    // TODO create debugger display for Property, ... https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2017
    // TODO create visualizers for Property, Catalog, Document, Constants https://docs.microsoft.com/en-us/visualstudio/debugger/create-custom-visualizers-of-data?view=vs-2017
	public partial class GroupListPlugins : ConfigObjectBase<GroupListPlugins, GroupListPlugins.GroupListPluginsValidator>, IComparable<GroupListPlugins>, IAccept, IGroupListPlugins
	{
		public partial class GroupListPluginsValidator : ValidatorBase<GroupListPlugins, GroupListPluginsValidator> { }
		#region CTOR
		public GroupListPlugins() : base(GroupListPluginsValidator.Validator)
		{
			this.ListPlugins = new SortedObservableCollection<Plugin>();
			OnInit();
		}
		public GroupListPlugins(ITreeConfigNode parent) : base(GroupListPluginsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListPlugins = new SortedObservableCollection<Plugin>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Plugin))
		    {
		        this.ListPlugins.Sort();
		    }
		}
		public static GroupListPlugins Clone(GroupListPlugins from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListPlugins vm = new GroupListPlugins();
		    vm.SortingValue = from.SortingValue;
		    vm.ListPlugins = new SortedObservableCollection<Plugin>();
		    foreach(var t in from.ListPlugins)
		        vm.ListPlugins.Add(vSharpStudio.vm.ViewModels.Plugin.Clone((Plugin)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListPlugins to, GroupListPlugins from, bool isDeep = true)
		{
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListPlugins.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPlugins)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Plugin.Update((Plugin)t, (Plugin)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Plugin.Update(p, (Plugin)tt, isDeep);
		                to.ListPlugins.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListPlugins ConvertToVM(Proto.Config.proto_group_list_plugins m, GroupListPlugins vm = null)
		{
		    if (vm == null)
		        vm = new GroupListPlugins();
		    vm.SortingValue = m.SortingValue;
		    vm.ListPlugins = new SortedObservableCollection<Plugin>();
		    foreach(var t in m.ListPlugins)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Plugin.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListPlugins.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListPlugins' to 'proto_group_list_plugins'
		public static Proto.Config.proto_group_list_plugins ConvertToProto(GroupListPlugins vm)
		{
		    Proto.Config.proto_group_list_plugins m = new Proto.Config.proto_group_list_plugins();
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListPlugins)
		        m.ListPlugins.Add(vSharpStudio.vm.ViewModels.Plugin.ConvertToProto((Plugin)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPlugins)
				(t as Plugin).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Plugin> ListPlugins 
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
		public void Add(Plugin item) 
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
		partial void OnListPluginsChanging();
		partial void OnListPluginsChanged();
		#endregion Properties
	}
	public partial class Plugin : ConfigObjectBase<Plugin, Plugin.PluginValidator>, IComparable<Plugin>, IAccept, IPlugin
	{
		public partial class PluginValidator : ValidatorBase<Plugin, PluginValidator> { }
		#region CTOR
		public Plugin() : base(PluginValidator.Validator)
		{
			this.ListGenerators = new SortedObservableCollection<PluginGenerator>();
			OnInit();
		}
		public Plugin(ITreeConfigNode parent) : base(PluginValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListGenerators = new SortedObservableCollection<PluginGenerator>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(PluginGenerator))
		    {
		        this.ListGenerators.Sort();
		    }
		}
		public static Plugin Clone(Plugin from, bool isDeep = true, bool isNewGuid = false)
		{
		    Plugin vm = new Plugin();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.Description = from.Description;
		    vm.SortingValue = from.SortingValue;
		    vm.ListGenerators = new SortedObservableCollection<PluginGenerator>();
		    foreach(var t in from.ListGenerators)
		        vm.ListGenerators.Add(vSharpStudio.vm.ViewModels.PluginGenerator.Clone((PluginGenerator)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Plugin to, Plugin from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.Description = from.Description;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListGenerators.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListGenerators)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.PluginGenerator.Update((PluginGenerator)t, (PluginGenerator)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.PluginGenerator.Update(p, (PluginGenerator)tt, isDeep);
		                to.ListGenerators.Add(p);
		            }
		        }
		    }
		}
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
		public static Plugin ConvertToVM(Proto.Config.proto_plugin m, Plugin vm = null)
		{
		    if (vm == null)
		        vm = new Plugin();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.Description = m.Description;
		    vm.SortingValue = m.SortingValue;
		    vm.ListGenerators = new SortedObservableCollection<PluginGenerator>();
		    foreach(var t in m.ListGenerators)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.PluginGenerator.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListGenerators.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Plugin' to 'proto_plugin'
		public static Proto.Config.proto_plugin ConvertToProto(Plugin vm)
		{
		    Proto.Config.proto_plugin m = new Proto.Config.proto_plugin();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.Description = vm.Description;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListGenerators)
		        m.ListGenerators.Add(vSharpStudio.vm.ViewModels.PluginGenerator.ConvertToProto((PluginGenerator)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListGenerators)
				(t as PluginGenerator).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[Editable(false)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<PluginGenerator> ListGenerators 
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
		partial void OnListGeneratorsChanging();
		partial void OnListGeneratorsChanged();
		#endregion Properties
	}
	public partial class PluginGenerator : ConfigObjectBase<PluginGenerator, PluginGenerator.PluginGeneratorValidator>, IComparable<PluginGenerator>, IAccept, IPluginGenerator
	{
		public partial class PluginGeneratorValidator : ValidatorBase<PluginGenerator, PluginGeneratorValidator> { }
		#region CTOR
		public PluginGenerator() : base(PluginGeneratorValidator.Validator)
		{
			this.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>();
			OnInit();
		}
		public PluginGenerator(ITreeConfigNode parent) : base(PluginGeneratorValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(PluginGeneratorSettings))
		    {
		        this.ListSettings.Sort();
		    }
		}
		public static PluginGenerator Clone(PluginGenerator from, bool isDeep = true, bool isNewGuid = false)
		{
		    PluginGenerator vm = new PluginGenerator();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.Description = from.Description;
		    vm.SortingValue = from.SortingValue;
		    vm.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>();
		    foreach(var t in from.ListSettings)
		        vm.ListSettings.Add(vSharpStudio.vm.ViewModels.PluginGeneratorSettings.Clone((PluginGeneratorSettings)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(PluginGenerator to, PluginGenerator from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.Description = from.Description;
		    to.SortingValue = from.SortingValue;
		    if (isDeep)
		    {
		        foreach(var t in to.ListSettings.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListSettings)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.PluginGeneratorSettings.Update((PluginGeneratorSettings)t, (PluginGeneratorSettings)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.PluginGeneratorSettings.Update(p, (PluginGeneratorSettings)tt, isDeep);
		                to.ListSettings.Add(p);
		            }
		        }
		    }
		}
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
		public static PluginGenerator ConvertToVM(Proto.Config.proto_plugin_generator m, PluginGenerator vm = null)
		{
		    if (vm == null)
		        vm = new PluginGenerator();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.Description = m.Description;
		    vm.SortingValue = m.SortingValue;
		    vm.ListSettings = new SortedObservableCollection<PluginGeneratorSettings>();
		    foreach(var t in m.ListSettings)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.PluginGeneratorSettings.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListSettings.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'PluginGenerator' to 'proto_plugin_generator'
		public static Proto.Config.proto_plugin_generator ConvertToProto(PluginGenerator vm)
		{
		    Proto.Config.proto_plugin_generator m = new Proto.Config.proto_plugin_generator();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.Description = vm.Description;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListSettings)
		        m.ListSettings.Add(vSharpStudio.vm.ViewModels.PluginGeneratorSettings.ConvertToProto((PluginGeneratorSettings)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListSettings)
				(t as PluginGeneratorSettings).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[Editable(false)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<PluginGeneratorSettings> ListSettings 
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
		partial void OnListSettingsChanging();
		partial void OnListSettingsChanged();
		#endregion Properties
	}
	public partial class PluginGeneratorSettings : ConfigObjectBase<PluginGeneratorSettings, PluginGeneratorSettings.PluginGeneratorSettingsValidator>, IComparable<PluginGeneratorSettings>, IAccept, IPluginGeneratorSettings
	{
		public partial class PluginGeneratorSettingsValidator : ValidatorBase<PluginGeneratorSettings, PluginGeneratorSettingsValidator> { }
		#region CTOR
		public PluginGeneratorSettings() : base(PluginGeneratorSettingsValidator.Validator)
		{
			OnInit();
		}
		public PluginGeneratorSettings(ITreeConfigNode parent) : base(PluginGeneratorSettingsValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static PluginGeneratorSettings Clone(PluginGeneratorSettings from, bool isDeep = true, bool isNewGuid = false)
		{
		    PluginGeneratorSettings vm = new PluginGeneratorSettings();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.Description = from.Description;
		    vm.SortingValue = from.SortingValue;
		    vm.GeneratorSettings = from.GeneratorSettings;
		    vm.IsPrivate = from.IsPrivate;
		    vm.FilePath = from.FilePath;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(PluginGeneratorSettings to, PluginGeneratorSettings from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.Description = from.Description;
		    to.SortingValue = from.SortingValue;
		    to.GeneratorSettings = from.GeneratorSettings;
		    to.IsPrivate = from.IsPrivate;
		    to.FilePath = from.FilePath;
		}
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
		public static PluginGeneratorSettings ConvertToVM(Proto.Config.proto_plugin_generator_settings m, PluginGeneratorSettings vm = null)
		{
		    if (vm == null)
		        vm = new PluginGeneratorSettings();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.Description = m.Description;
		    vm.SortingValue = m.SortingValue;
		    vm.GeneratorSettings = m.GeneratorSettings;
		    vm.IsPrivate = m.IsPrivate;
		    vm.FilePath = m.FilePath;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'PluginGeneratorSettings' to 'proto_plugin_generator_settings'
		public static Proto.Config.proto_plugin_generator_settings ConvertToProto(PluginGeneratorSettings vm)
		{
		    Proto.Config.proto_plugin_generator_settings m = new Proto.Config.proto_plugin_generator_settings();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.Description = vm.Description;
		    m.SortingValue = vm.SortingValue;
		    m.GeneratorSettings = vm.GeneratorSettings;
		    m.IsPrivate = vm.IsPrivate;
		    m.FilePath = vm.FilePath;
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
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
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public string GeneratorSettings
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
		partial void OnGeneratorSettingsChanging();
		partial void OnGeneratorSettingsChanged();
		[PropertyOrderAttribute(3)]
		[Description("If false, connection string settings will be stored in configuration file. If true, will be stored in separate file.")]
		public bool IsPrivate
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
		partial void OnIsPrivateChanging();
		partial void OnIsPrivateChanged();
		[PropertyOrderAttribute(4)]
		[Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
		[Description("File path to store connection string settings in private place.")]
		public string FilePath
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
		partial void OnFilePathChanging();
		partial void OnFilePathChanged();
		#endregion Properties
	}
	public partial class SettingsConfig : ConfigObjectBase<SettingsConfig, SettingsConfig.SettingsConfigValidator>, IComparable<SettingsConfig>, IAccept, ISettingsConfig
	{
		public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { }
		#region CTOR
		public SettingsConfig() : base(SettingsConfigValidator.Validator)
		{
			OnInit();
		}
		public SettingsConfig(ITreeConfigNode parent) : base(SettingsConfigValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static SettingsConfig Clone(SettingsConfig from, bool isDeep = true, bool isNewGuid = false)
		{
		    SettingsConfig vm = new SettingsConfig();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.VersionMigrationCurrent = from.VersionMigrationCurrent;
		    vm.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(SettingsConfig to, SettingsConfig from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    to.VersionMigrationCurrent = from.VersionMigrationCurrent;
		    to.VersionMigrationSupportFromMin = from.VersionMigrationSupportFromMin;
		}
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
		public static SettingsConfig ConvertToVM(Proto.Config.proto_settings_config m, SettingsConfig vm = null)
		{
		    if (vm == null)
		        vm = new SettingsConfig();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.VersionMigrationCurrent = m.VersionMigrationCurrent;
		    vm.VersionMigrationSupportFromMin = m.VersionMigrationSupportFromMin;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'SettingsConfig' to 'proto_settings_config'
		public static Proto.Config.proto_settings_config ConvertToProto(SettingsConfig vm)
		{
		    Proto.Config.proto_settings_config m = new Proto.Config.proto_settings_config();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.VersionMigrationCurrent = vm.VersionMigrationCurrent;
		    m.VersionMigrationSupportFromMin = vm.VersionMigrationSupportFromMin;
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// current migration version, increased by one on each deployment
		///////////////////////////////////////////////////
		public int VersionMigrationCurrent
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
		partial void OnVersionMigrationCurrentChanging();
		partial void OnVersionMigrationCurrentChanged();
		
		///////////////////////////////////////////////////
		/// min version supported by current version for migration
		///////////////////////////////////////////////////
		public int VersionMigrationSupportFromMin
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
		partial void OnVersionMigrationSupportFromMinChanging();
		partial void OnVersionMigrationSupportFromMinChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// General DB settings
	///////////////////////////////////////////////////
	public partial class DbSettings : ViewModelValidatableWithSeverity<DbSettings, DbSettings.DbSettingsValidator>, IDbSettings
	{
		public partial class DbSettingsValidator : ValidatorBase<DbSettings, DbSettingsValidator> { }
		#region CTOR
		public DbSettings() : base(DbSettingsValidator.Validator)
		{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DbSettings Clone(DbSettings from, bool isDeep = true)
		{
		    DbSettings vm = new DbSettings();
		    vm.DbSchema = from.DbSchema;
		    vm.IdGenerator = from.IdGenerator;
		    vm.KeyType = from.KeyType;
		    vm.KeyName = from.KeyName;
		    vm.Timestamp = from.Timestamp;
		    vm.IsDbFromConnectionString = from.IsDbFromConnectionString;
		    vm.ConnectionStringName = from.ConnectionStringName;
		    vm.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
		    return vm;
		}
		public static void Update(DbSettings to, DbSettings from, bool isDeep = true)
		{
		    to.DbSchema = from.DbSchema;
		    to.IdGenerator = from.IdGenerator;
		    to.KeyType = from.KeyType;
		    to.KeyName = from.KeyName;
		    to.Timestamp = from.Timestamp;
		    to.IsDbFromConnectionString = from.IsDbFromConnectionString;
		    to.ConnectionStringName = from.ConnectionStringName;
		    to.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
		}
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
		public static DbSettings ConvertToVM(Proto.Config.db_settings m, DbSettings vm = null)
		{
		    if (vm == null)
		        vm = new DbSettings();
		    vm.DbSchema = m.DbSchema;
		    vm.IdGenerator = (DbIdGeneratorMethod)m.IdGenerator;
		    vm.KeyType = m.KeyType;
		    vm.KeyName = m.KeyName;
		    vm.Timestamp = m.Timestamp;
		    vm.IsDbFromConnectionString = m.IsDbFromConnectionString;
		    vm.ConnectionStringName = m.ConnectionStringName;
		    vm.PathToProjectWithConnectionString = m.PathToProjectWithConnectionString;
		    return vm;
		}
		// Conversion from 'DbSettings' to 'db_settings'
		public static Proto.Config.db_settings ConvertToProto(DbSettings vm)
		{
		    Proto.Config.db_settings m = new Proto.Config.db_settings();
		    m.DbSchema = vm.DbSchema;
		    m.IdGenerator = (Proto.Config.db_id_generator_method)vm.IdGenerator;
		    m.KeyType = vm.KeyType;
		    m.KeyName = vm.KeyName;
		    m.Timestamp = vm.Timestamp;
		    m.IsDbFromConnectionString = vm.IsDbFromConnectionString;
		    m.ConnectionStringName = vm.ConnectionStringName;
		    m.PathToProjectWithConnectionString = vm.PathToProjectWithConnectionString;
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[Description("DB schema name for all object in this configuration")]
		public string DbSchema
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
		partial void OnDbSchemaChanging();
		partial void OnDbSchemaChanged();
		[PropertyOrderAttribute(2)]
		[Description("Primary key generation method")]
		public DbIdGeneratorMethod IdGenerator
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
		partial void OnIdGeneratorChanging();
		partial void OnIdGeneratorChanged();
		[PropertyOrderAttribute(3)]
		[Description("Primary key field type")]
		public string KeyType
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
		partial void OnKeyTypeChanging();
		partial void OnKeyTypeChanged();
		[PropertyOrderAttribute(4)]
		[Description("Primary key field name")]
		public string KeyName
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
		partial void OnKeyNameChanging();
		partial void OnKeyNameChanged();
		[PropertyOrderAttribute(5)]
		[Description("Record data version/timestamp field name")]
		public string Timestamp
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
		partial void OnTimestampChanging();
		partial void OnTimestampChanged();
		
		///////////////////////////////////////////////////
		/// if yes: 
		///    Try to find one connecion string in config file. If more than one connection string found we use use connection_string_name.
		/// if no:
		///    1. Find DB type from 
		///    2. Create connection string from db_server, db_database_name, db_user
		///////////////////////////////////////////////////
		public bool IsDbFromConnectionString
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
		partial void OnIsDbFromConnectionStringChanging();
		partial void OnIsDbFromConnectionStringChanged();
		public string ConnectionStringName
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
		partial void OnConnectionStringNameChanging();
		partial void OnConnectionStringNameChanged();
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(4)]
		[Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
		[Description("File path to store connection string settings in private place.")]
		public string PathToProjectWithConnectionString
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
		partial void OnPathToProjectWithConnectionStringChanging();
		partial void OnPathToProjectWithConnectionStringChanged();
		#endregion Properties
	}
	public partial class GroupListBaseConfigs : ConfigObjectBase<GroupListBaseConfigs, GroupListBaseConfigs.GroupListBaseConfigsValidator>, IComparable<GroupListBaseConfigs>, IAccept, IGroupListBaseConfigs
	{
		public partial class GroupListBaseConfigsValidator : ValidatorBase<GroupListBaseConfigs, GroupListBaseConfigsValidator> { }
		#region CTOR
		public GroupListBaseConfigs() : base(GroupListBaseConfigsValidator.Validator)
		{
			this.ListBaseConfigs = new SortedObservableCollection<BaseConfig>();
			OnInit();
		}
		public GroupListBaseConfigs(ITreeConfigNode parent) : base(GroupListBaseConfigsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListBaseConfigs = new SortedObservableCollection<BaseConfig>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(BaseConfig))
		    {
		        this.ListBaseConfigs.Sort();
		    }
		}
		public static GroupListBaseConfigs Clone(GroupListBaseConfigs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListBaseConfigs vm = new GroupListBaseConfigs();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.Description = from.Description;
		    vm.ListBaseConfigs = new SortedObservableCollection<BaseConfig>();
		    foreach(var t in from.ListBaseConfigs)
		        vm.ListBaseConfigs.Add(vSharpStudio.vm.ViewModels.BaseConfig.Clone((BaseConfig)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListBaseConfigs to, GroupListBaseConfigs from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListBaseConfigs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListBaseConfigs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.BaseConfig.Update((BaseConfig)t, (BaseConfig)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.BaseConfig.Update(p, (BaseConfig)tt, isDeep);
		                to.ListBaseConfigs.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListBaseConfigs ConvertToVM(Proto.Config.proto_group_list_base_configs m, GroupListBaseConfigs vm = null)
		{
		    if (vm == null)
		        vm = new GroupListBaseConfigs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.Description = m.Description;
		    vm.ListBaseConfigs = new SortedObservableCollection<BaseConfig>();
		    foreach(var t in m.ListBaseConfigs)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.BaseConfig.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListBaseConfigs.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListBaseConfigs' to 'proto_group_list_base_configs'
		public static Proto.Config.proto_group_list_base_configs ConvertToProto(GroupListBaseConfigs vm)
		{
		    Proto.Config.proto_group_list_base_configs m = new Proto.Config.proto_group_list_base_configs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListBaseConfigs)
		        m.ListBaseConfigs.Add(vSharpStudio.vm.ViewModels.BaseConfig.ConvertToProto((BaseConfig)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListBaseConfigs)
				(t as BaseConfig).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<BaseConfig> ListBaseConfigs 
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
		public void Add(BaseConfig item) 
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
		partial void OnListBaseConfigsChanging();
		partial void OnListBaseConfigsChanged();
		#endregion Properties
	}
	public partial class BaseConfig : ConfigObjectBase<BaseConfig, BaseConfig.BaseConfigValidator>, IComparable<BaseConfig>, IAccept, IBaseConfig
	{
		public partial class BaseConfigValidator : ValidatorBase<BaseConfig, BaseConfigValidator> { }
		#region CTOR
		public BaseConfig() : base(BaseConfigValidator.Validator)
		{
			this.ConfigNode = new Config(this);
			OnInit();
		}
		public BaseConfig(ITreeConfigNode parent) : base(BaseConfigValidator.Validator)
	    {
	        this.Parent = parent;
			this.ConfigNode = new Config(this);
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static BaseConfig Clone(BaseConfig from, bool isDeep = true, bool isNewGuid = false)
		{
		    BaseConfig vm = new BaseConfig();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.ConfigNode = vSharpStudio.vm.ViewModels.Config.Clone(from.ConfigNode, isDeep);
		    vm.RelativeConfigFilePath = from.RelativeConfigFilePath;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(BaseConfig to, BaseConfig from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.Description = from.Description;
		    if (isDeep)
		        Config.Update(to.ConfigNode, from.ConfigNode, isDeep);
		    to.RelativeConfigFilePath = from.RelativeConfigFilePath;
		}
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
		public static BaseConfig ConvertToVM(Proto.Config.proto_base_config m, BaseConfig vm = null)
		{
		    if (vm == null)
		        vm = new BaseConfig();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.Config.ConvertToVM(m.ConfigNode, vm.ConfigNode);
		    vm.RelativeConfigFilePath = m.RelativeConfigFilePath;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'BaseConfig' to 'proto_base_config'
		public static Proto.Config.proto_base_config ConvertToProto(BaseConfig vm)
		{
		    Proto.Config.proto_base_config m = new Proto.Config.proto_base_config();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.Description = vm.Description;
		    m.ConfigNode = vSharpStudio.vm.ViewModels.Config.ConvertToProto(vm.ConfigNode);
		    m.RelativeConfigFilePath = vm.RelativeConfigFilePath;
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.ConfigNode.AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		
		///////////////////////////////////////////////////
		/// string name_ui = 4;
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(5)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public Config ConfigNode
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
		partial void OnConfigNodeChanging();
		partial void OnConfigNodeChanged();
		[PropertyOrderAttribute(6)]
		[Editor(typeof(FilePickerEditor), typeof(ITypeEditor))]
		public string RelativeConfigFilePath
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
		partial void OnRelativeConfigFilePathChanging();
		partial void OnRelativeConfigFilePathChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Configuration config
	///////////////////////////////////////////////////
	public partial class Config : ConfigObjectBase<Config, Config.ConfigValidator>, IComparable<Config>, IAccept, IConfig
	{
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config() : base(ConfigValidator.Validator)
		{
			this.DbSettings = new DbSettings();
			this.GroupPlugins = new GroupListPlugins(this);
			this.GroupConfigs = new GroupListBaseConfigs(this);
			this.GroupConstants = new GroupListConstants(this);
			this.GroupEnumerations = new GroupListEnumerations(this);
			this.GroupCatalogs = new GroupListCatalogs(this);
			this.GroupDocuments = new GroupDocuments(this);
			this.GroupJournals = new GroupListJournals(this);
			OnInit();
		}
		public Config(ITreeConfigNode parent) : base(ConfigValidator.Validator)
	    {
	        this.Parent = parent;
			this.DbSettings = new DbSettings();
			this.GroupPlugins = new GroupListPlugins(this);
			this.GroupConfigs = new GroupListBaseConfigs(this);
			this.GroupConstants = new GroupListConstants(this);
			this.GroupEnumerations = new GroupListEnumerations(this);
			this.GroupCatalogs = new GroupListCatalogs(this);
			this.GroupDocuments = new GroupDocuments(this);
			this.GroupJournals = new GroupListJournals(this);
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Config Clone(Config from, bool isDeep = true, bool isNewGuid = false)
		{
		    Config vm = new Config();
		    vm.Guid = from.Guid;
		    vm.Version = from.Version;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.DbSettings = vSharpStudio.vm.ViewModels.DbSettings.Clone(from.DbSettings, isDeep);
		    if (isDeep)
		        vm.GroupPlugins = vSharpStudio.vm.ViewModels.GroupListPlugins.Clone(from.GroupPlugins, isDeep);
		    if (isDeep)
		        vm.GroupConfigs = vSharpStudio.vm.ViewModels.GroupListBaseConfigs.Clone(from.GroupConfigs, isDeep);
		    if (isDeep)
		        vm.GroupConstants = vSharpStudio.vm.ViewModels.GroupListConstants.Clone(from.GroupConstants, isDeep);
		    if (isDeep)
		        vm.GroupEnumerations = vSharpStudio.vm.ViewModels.GroupListEnumerations.Clone(from.GroupEnumerations, isDeep);
		    if (isDeep)
		        vm.GroupCatalogs = vSharpStudio.vm.ViewModels.GroupListCatalogs.Clone(from.GroupCatalogs, isDeep);
		    if (isDeep)
		        vm.GroupDocuments = vSharpStudio.vm.ViewModels.GroupDocuments.Clone(from.GroupDocuments, isDeep);
		    if (isDeep)
		        vm.GroupJournals = vSharpStudio.vm.ViewModels.GroupListJournals.Clone(from.GroupJournals, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Config to, Config from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Version = from.Version;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        DbSettings.Update(to.DbSettings, from.DbSettings, isDeep);
		    if (isDeep)
		        GroupListPlugins.Update(to.GroupPlugins, from.GroupPlugins, isDeep);
		    if (isDeep)
		        GroupListBaseConfigs.Update(to.GroupConfigs, from.GroupConfigs, isDeep);
		    if (isDeep)
		        GroupListConstants.Update(to.GroupConstants, from.GroupConstants, isDeep);
		    if (isDeep)
		        GroupListEnumerations.Update(to.GroupEnumerations, from.GroupEnumerations, isDeep);
		    if (isDeep)
		        GroupListCatalogs.Update(to.GroupCatalogs, from.GroupCatalogs, isDeep);
		    if (isDeep)
		        GroupDocuments.Update(to.GroupDocuments, from.GroupDocuments, isDeep);
		    if (isDeep)
		        GroupListJournals.Update(to.GroupJournals, from.GroupJournals, isDeep);
		}
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
		public static Config ConvertToVM(Proto.Config.proto_config m, Config vm = null)
		{
		    if (vm == null)
		        vm = new Config();
		    vm.Guid = m.Guid;
		    vm.Version = m.Version;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.DbSettings.ConvertToVM(m.DbSettings, vm.DbSettings);
		    vSharpStudio.vm.ViewModels.GroupListPlugins.ConvertToVM(m.GroupPlugins, vm.GroupPlugins);
		    vSharpStudio.vm.ViewModels.GroupListBaseConfigs.ConvertToVM(m.GroupConfigs, vm.GroupConfigs);
		    vSharpStudio.vm.ViewModels.GroupListConstants.ConvertToVM(m.GroupConstants, vm.GroupConstants);
		    vSharpStudio.vm.ViewModels.GroupListEnumerations.ConvertToVM(m.GroupEnumerations, vm.GroupEnumerations);
		    vSharpStudio.vm.ViewModels.GroupListCatalogs.ConvertToVM(m.GroupCatalogs, vm.GroupCatalogs);
		    vSharpStudio.vm.ViewModels.GroupDocuments.ConvertToVM(m.GroupDocuments, vm.GroupDocuments);
		    vSharpStudio.vm.ViewModels.GroupListJournals.ConvertToVM(m.GroupJournals, vm.GroupJournals);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Config' to 'proto_config'
		public static Proto.Config.proto_config ConvertToProto(Config vm)
		{
		    Proto.Config.proto_config m = new Proto.Config.proto_config();
		    m.Guid = vm.Guid;
		    m.Version = vm.Version;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DbSettings = vSharpStudio.vm.ViewModels.DbSettings.ConvertToProto(vm.DbSettings);
		    m.GroupPlugins = vSharpStudio.vm.ViewModels.GroupListPlugins.ConvertToProto(vm.GroupPlugins);
		    m.GroupConfigs = vSharpStudio.vm.ViewModels.GroupListBaseConfigs.ConvertToProto(vm.GroupConfigs);
		    m.GroupConstants = vSharpStudio.vm.ViewModels.GroupListConstants.ConvertToProto(vm.GroupConstants);
		    m.GroupEnumerations = vSharpStudio.vm.ViewModels.GroupListEnumerations.ConvertToProto(vm.GroupEnumerations);
		    m.GroupCatalogs = vSharpStudio.vm.ViewModels.GroupListCatalogs.ConvertToProto(vm.GroupCatalogs);
		    m.GroupDocuments = vSharpStudio.vm.ViewModels.GroupDocuments.ConvertToProto(vm.GroupDocuments);
		    m.GroupJournals = vSharpStudio.vm.ViewModels.GroupListJournals.ConvertToProto(vm.GroupJournals);
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupConstants.AcceptConfigNode(visitor);
			this.GroupEnumerations.AcceptConfigNode(visitor);
			this.GroupCatalogs.AcceptConfigNode(visitor);
			this.GroupDocuments.AcceptConfigNode(visitor);
			this.GroupJournals.AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(4)]
		public string Version
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
		partial void OnVersionChanging();
		partial void OnVersionChanged();
		[PropertyOrderAttribute(5)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// GENERAL DB SETTINGS
		///////////////////////////////////////////////////
		[PropertyOrderAttribute(11)]
		[ExpandableObjectAttribute()]
		public DbSettings DbSettings
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
		partial void OnDbSettingsChanging();
		partial void OnDbSettingsChanged();
		[BrowsableAttribute(false)]
		public GroupListPlugins GroupPlugins
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
		partial void OnGroupPluginsChanging();
		partial void OnGroupPluginsChanged();
		[BrowsableAttribute(false)]
		public GroupListBaseConfigs GroupConfigs
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
		partial void OnGroupConfigsChanging();
		partial void OnGroupConfigsChanged();
		[BrowsableAttribute(false)]
		public GroupListConstants GroupConstants
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
		partial void OnGroupConstantsChanging();
		partial void OnGroupConstantsChanged();
		[BrowsableAttribute(false)]
		public GroupListEnumerations GroupEnumerations
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
		partial void OnGroupEnumerationsChanging();
		partial void OnGroupEnumerationsChanged();
		[BrowsableAttribute(false)]
		public GroupListCatalogs GroupCatalogs
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
		partial void OnGroupCatalogsChanging();
		partial void OnGroupCatalogsChanged();
		[BrowsableAttribute(false)]
		public GroupDocuments GroupDocuments
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
		partial void OnGroupDocumentsChanging();
		partial void OnGroupDocumentsChanged();
		[BrowsableAttribute(false)]
		public GroupListJournals GroupJournals
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
		partial void OnGroupJournalsChanging();
		partial void OnGroupJournalsChanged();
		#endregion Properties
	}
	public partial class DataType : ViewModelValidatableWithSeverity<DataType, DataType.DataTypeValidator>, IDataType
	{
		public partial class DataTypeValidator : ValidatorBase<DataType, DataTypeValidator> { }
		#region CTOR
		public DataType() : base(DataTypeValidator.Validator)
		{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DataType Clone(DataType from, bool isDeep = true)
		{
		    DataType vm = new DataType();
		    vm.DataTypeEnum = from.DataTypeEnum;
		    vm.Length = from.Length;
		    vm.Accuracy = from.Accuracy;
		    vm.IsPositive = from.IsPositive;
		    vm.ObjectGuid = from.ObjectGuid;
		    return vm;
		}
		public static void Update(DataType to, DataType from, bool isDeep = true)
		{
		    to.DataTypeEnum = from.DataTypeEnum;
		    to.Length = from.Length;
		    to.Accuracy = from.Accuracy;
		    to.IsPositive = from.IsPositive;
		    to.ObjectGuid = from.ObjectGuid;
		}
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
		public static DataType ConvertToVM(Proto.Config.proto_data_type m, DataType vm = null)
		{
		    if (vm == null)
		        vm = new DataType();
		    vm.DataTypeEnum = (EnumDataType)m.DataTypeEnum;
		    vm.Length = m.Length;
		    vm.Accuracy = m.Accuracy;
		    vm.IsPositive = m.IsPositive;
		    vm.ObjectGuid = m.ObjectGuid;
		    return vm;
		}
		// Conversion from 'DataType' to 'proto_data_type'
		public static Proto.Config.proto_data_type ConvertToProto(DataType vm)
		{
		    Proto.Config.proto_data_type m = new Proto.Config.proto_data_type();
		    m.DataTypeEnum = (Proto.Config.proto_enum_data_type)vm.DataTypeEnum;
		    m.Length = vm.Length;
		    m.Accuracy = vm.Accuracy;
		    m.IsPositive = vm.IsPositive;
		    m.ObjectGuid = vm.ObjectGuid;
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(1)]
		[DisplayName("Type")]
		public EnumDataType DataTypeEnum
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
		partial void OnDataTypeEnumChanging();
		partial void OnDataTypeEnumChanged();
		[PropertyOrderAttribute(3)]
		public uint Length
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
		partial void OnLengthChanging();
		partial void OnLengthChanged();
		[PropertyOrderAttribute(5)]
		public uint Accuracy
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
		partial void OnAccuracyChanging();
		partial void OnAccuracyChanged();
		[PropertyOrderAttribute(4)]
		[DisplayName("Is positive")]
		public bool IsPositive
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
		partial void OnIsPositiveChanging();
		partial void OnIsPositiveChanged();
		[PropertyOrderAttribute(2)]
		[Editor(typeof(DataTypeObjectNameEditor), typeof(DataTypeObjectNameEditor))]
		public string ObjectGuid
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
		partial void OnObjectGuidChanging();
		partial void OnObjectGuidChanged();
		#endregion Properties
	}
	public partial class GroupListPropertiesTabs : ConfigObjectBase<GroupListPropertiesTabs, GroupListPropertiesTabs.GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IAccept, IGroupListPropertiesTabs
	{
		public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { }
		#region CTOR
		public GroupListPropertiesTabs() : base(GroupListPropertiesTabsValidator.Validator)
		{
			this.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>();
			OnInit();
		}
		public GroupListPropertiesTabs(ITreeConfigNode parent) : base(GroupListPropertiesTabsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(PropertiesTab))
		    {
		        this.ListPropertiesTabs.Sort();
		    }
		}
		public static GroupListPropertiesTabs Clone(GroupListPropertiesTabs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListPropertiesTabs vm = new GroupListPropertiesTabs();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>();
		    foreach(var t in from.ListPropertiesTabs)
		        vm.ListPropertiesTabs.Add(vSharpStudio.vm.ViewModels.PropertiesTab.Clone((PropertiesTab)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListPropertiesTabs to, GroupListPropertiesTabs from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListPropertiesTabs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPropertiesTabs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.PropertiesTab.Update((PropertiesTab)t, (PropertiesTab)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.PropertiesTab.Update(p, (PropertiesTab)tt, isDeep);
		                to.ListPropertiesTabs.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListPropertiesTabs ConvertToVM(Proto.Config.proto_group_list_properties_tabs m, GroupListPropertiesTabs vm = null)
		{
		    if (vm == null)
		        vm = new GroupListPropertiesTabs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListPropertiesTabs = new SortedObservableCollection<PropertiesTab>();
		    foreach(var t in m.ListPropertiesTabs)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.PropertiesTab.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListPropertiesTabs.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListPropertiesTabs' to 'proto_group_list_properties_tabs'
		public static Proto.Config.proto_group_list_properties_tabs ConvertToProto(GroupListPropertiesTabs vm)
		{
		    Proto.Config.proto_group_list_properties_tabs m = new Proto.Config.proto_group_list_properties_tabs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListPropertiesTabs)
		        m.ListPropertiesTabs.Add(vSharpStudio.vm.ViewModels.PropertiesTab.ConvertToProto((PropertiesTab)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPropertiesTabs)
				(t as PropertiesTab).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<PropertiesTab> ListPropertiesTabs 
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
		public void Add(PropertiesTab item) 
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
		partial void OnListPropertiesTabsChanging();
		partial void OnListPropertiesTabsChanged();
		#endregion Properties
	}
	public partial class PropertiesTab : ConfigObjectBase<PropertiesTab, PropertiesTab.PropertiesTabValidator>, IComparable<PropertiesTab>, IAccept, IPropertiesTab
	{
		public partial class PropertiesTabValidator : ValidatorBase<PropertiesTab, PropertiesTabValidator> { }
		#region CTOR
		public PropertiesTab() : base(PropertiesTabValidator.Validator)
		{
			this.GroupProperties = new GroupListProperties(this);
			this.GroupPropertiesSubtabs = new GroupListPropertiesTabs(this);
			OnInit();
		}
		public PropertiesTab(ITreeConfigNode parent) : base(PropertiesTabValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupProperties = new GroupListProperties(this);
			this.GroupPropertiesSubtabs = new GroupListPropertiesTabs(this);
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static PropertiesTab Clone(PropertiesTab from, bool isDeep = true, bool isNewGuid = false)
		{
		    PropertiesTab vm = new PropertiesTab();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(from.GroupProperties, isDeep);
		    if (isDeep)
		        vm.GroupPropertiesSubtabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Clone(from.GroupPropertiesSubtabs, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(PropertiesTab to, PropertiesTab from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
		    if (isDeep)
		        GroupListPropertiesTabs.Update(to.GroupPropertiesSubtabs, from.GroupPropertiesSubtabs, isDeep);
		}
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
		public static PropertiesTab ConvertToVM(Proto.Config.proto_properties_tab m, PropertiesTab vm = null)
		{
		    if (vm == null)
		        vm = new PropertiesTab();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties);
		    vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesSubtabs, vm.GroupPropertiesSubtabs);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'PropertiesTab' to 'proto_properties_tab'
		public static Proto.Config.proto_properties_tab ConvertToProto(PropertiesTab vm)
		{
		    Proto.Config.proto_properties_tab m = new Proto.Config.proto_properties_tab();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto(vm.GroupProperties);
		    m.GroupPropertiesSubtabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesSubtabs);
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupProperties.AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties
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
		partial void OnGroupPropertiesChanging();
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesSubtabs
		{ 
			set
			{
				if (_GroupPropertiesSubtabs != value)
				{
					OnGroupPropertiesSubtabsChanging();
		            _GroupPropertiesSubtabs = value;
					OnGroupPropertiesSubtabsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _GroupPropertiesSubtabs; }
		}
		private GroupListPropertiesTabs _GroupPropertiesSubtabs;
		[BrowsableAttribute(false)]
		public IGroupListPropertiesTabs GroupPropertiesSubtabsI { get { return _GroupPropertiesSubtabs; }}
		partial void OnGroupPropertiesSubtabsChanging();
		partial void OnGroupPropertiesSubtabsChanged();
		#endregion Properties
	}
	public partial class GroupListProperties : ConfigObjectBase<GroupListProperties, GroupListProperties.GroupListPropertiesValidator>, IComparable<GroupListProperties>, IAccept, IGroupListProperties
	{
		public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { }
		#region CTOR
		public GroupListProperties() : base(GroupListPropertiesValidator.Validator)
		{
			this.ListProperties = new SortedObservableCollection<Property>();
			OnInit();
		}
		public GroupListProperties(ITreeConfigNode parent) : base(GroupListPropertiesValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListProperties = new SortedObservableCollection<Property>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Property))
		    {
		        this.ListProperties.Sort();
		    }
		}
		public static GroupListProperties Clone(GroupListProperties from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListProperties vm = new GroupListProperties();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in from.ListProperties)
		        vm.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.Clone((Property)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListProperties to, GroupListProperties from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListProperties.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListProperties)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Property.Update((Property)t, (Property)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Property.Update(p, (Property)tt, isDeep);
		                to.ListProperties.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListProperties ConvertToVM(Proto.Config.proto_group_list_properties m, GroupListProperties vm = null)
		{
		    if (vm == null)
		        vm = new GroupListProperties();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListProperties = new SortedObservableCollection<Property>();
		    foreach(var t in m.ListProperties)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Property.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListProperties.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListProperties' to 'proto_group_list_properties'
		public static Proto.Config.proto_group_list_properties ConvertToProto(GroupListProperties vm)
		{
		    Proto.Config.proto_group_list_properties m = new Proto.Config.proto_group_list_properties();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListProperties)
		        m.ListProperties.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto((Property)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListProperties)
				(t as Property).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Property> ListProperties 
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
		public void Add(Property item) 
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
		partial void OnListPropertiesChanging();
		partial void OnListPropertiesChanged();
		#endregion Properties
	}
	public partial class Property : ConfigObjectBase<Property, Property.PropertyValidator>, IComparable<Property>, IAccept, IProperty
	{
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { }
		#region CTOR
		public Property() : base(PropertyValidator.Validator)
		{
			this.DataType = new DataType();
			OnInit();
		}
		public Property(ITreeConfigNode parent) : base(PropertyValidator.Validator)
	    {
	        this.Parent = parent;
			this.DataType = new DataType();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Property Clone(Property from, bool isDeep = true, bool isNewGuid = false)
		{
		    Property vm = new Property();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(from.DataType, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Property to, Property from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        DataType.Update(to.DataType, from.DataType, isDeep);
		}
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
		public static Property ConvertToVM(Proto.Config.proto_property m, Property vm = null)
		{
		    if (vm == null)
		        vm = new Property();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType, vm.DataType);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Property' to 'proto_property'
		public static Proto.Config.proto_property ConvertToProto(Property vm)
		{
		    Proto.Config.proto_property m = new Proto.Config.proto_property();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto(vm.DataType);
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(4)]
		[ExpandableObjectAttribute()]
		[DisplayName("Type")]
		public DataType DataType
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
		partial void OnDataTypeChanging();
		partial void OnDataTypeChanged();
		#endregion Properties
	}
	public partial class GroupListConstants : ConfigObjectBase<GroupListConstants, GroupListConstants.GroupListConstantsValidator>, IComparable<GroupListConstants>, IAccept, IGroupListConstants
	{
		public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { }
		#region CTOR
		public GroupListConstants() : base(GroupListConstantsValidator.Validator)
		{
			this.ListConstants = new SortedObservableCollection<Constant>();
			OnInit();
		}
		public GroupListConstants(ITreeConfigNode parent) : base(GroupListConstantsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListConstants = new SortedObservableCollection<Constant>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Constant))
		    {
		        this.ListConstants.Sort();
		    }
		}
		public static GroupListConstants Clone(GroupListConstants from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListConstants vm = new GroupListConstants();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListConstants = new SortedObservableCollection<Constant>();
		    foreach(var t in from.ListConstants)
		        vm.ListConstants.Add(vSharpStudio.vm.ViewModels.Constant.Clone((Constant)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListConstants to, GroupListConstants from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListConstants.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListConstants)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Constant.Update((Constant)t, (Constant)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Constant.Update(p, (Constant)tt, isDeep);
		                to.ListConstants.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListConstants ConvertToVM(Proto.Config.proto_group_list_constants m, GroupListConstants vm = null)
		{
		    if (vm == null)
		        vm = new GroupListConstants();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListConstants = new SortedObservableCollection<Constant>();
		    foreach(var t in m.ListConstants)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Constant.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListConstants.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListConstants' to 'proto_group_list_constants'
		public static Proto.Config.proto_group_list_constants ConvertToProto(GroupListConstants vm)
		{
		    Proto.Config.proto_group_list_constants m = new Proto.Config.proto_group_list_constants();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListConstants)
		        m.ListConstants.Add(vSharpStudio.vm.ViewModels.Constant.ConvertToProto((Constant)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListConstants)
				(t as Constant).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Constant> ListConstants 
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
		public void Add(Constant item) 
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
		partial void OnListConstantsChanging();
		partial void OnListConstantsChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	public partial class Constant : ConfigObjectBase<Constant, Constant.ConstantValidator>, IComparable<Constant>, IAccept, IConstant
	{
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { }
		#region CTOR
		public Constant() : base(ConstantValidator.Validator)
		{
			this.DataType = new DataType();
			OnInit();
		}
		public Constant(ITreeConfigNode parent) : base(ConstantValidator.Validator)
	    {
	        this.Parent = parent;
			this.DataType = new DataType();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Constant Clone(Constant from, bool isDeep = true, bool isNewGuid = false)
		{
		    Constant vm = new Constant();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.DataType = vSharpStudio.vm.ViewModels.DataType.Clone(from.DataType, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Constant to, Constant from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        DataType.Update(to.DataType, from.DataType, isDeep);
		}
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
		public static Constant ConvertToVM(Proto.Config.proto_constant m, Constant vm = null)
		{
		    if (vm == null)
		        vm = new Constant();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.DataType.ConvertToVM(m.DataType, vm.DataType);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Constant' to 'proto_constant'
		public static Proto.Config.proto_constant ConvertToProto(Constant vm)
		{
		    Proto.Config.proto_constant m = new Proto.Config.proto_constant();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DataType = vSharpStudio.vm.ViewModels.DataType.ConvertToProto(vm.DataType);
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(4)]
		[ExpandableObjectAttribute()]
		[DisplayName("Type")]
		public DataType DataType
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
		partial void OnDataTypeChanging();
		partial void OnDataTypeChanged();
		#endregion Properties
	}
	public partial class GroupListEnumerations : ConfigObjectBase<GroupListEnumerations, GroupListEnumerations.GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IAccept, IGroupListEnumerations
	{
		public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { }
		#region CTOR
		public GroupListEnumerations() : base(GroupListEnumerationsValidator.Validator)
		{
			this.ListEnumerations = new SortedObservableCollection<Enumeration>();
			OnInit();
		}
		public GroupListEnumerations(ITreeConfigNode parent) : base(GroupListEnumerationsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListEnumerations = new SortedObservableCollection<Enumeration>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Enumeration))
		    {
		        this.ListEnumerations.Sort();
		    }
		}
		public static GroupListEnumerations Clone(GroupListEnumerations from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListEnumerations vm = new GroupListEnumerations();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListEnumerations = new SortedObservableCollection<Enumeration>();
		    foreach(var t in from.ListEnumerations)
		        vm.ListEnumerations.Add(vSharpStudio.vm.ViewModels.Enumeration.Clone((Enumeration)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListEnumerations to, GroupListEnumerations from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListEnumerations.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListEnumerations)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Enumeration.Update((Enumeration)t, (Enumeration)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Enumeration.Update(p, (Enumeration)tt, isDeep);
		                to.ListEnumerations.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListEnumerations ConvertToVM(Proto.Config.proto_group_list_enumerations m, GroupListEnumerations vm = null)
		{
		    if (vm == null)
		        vm = new GroupListEnumerations();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListEnumerations = new SortedObservableCollection<Enumeration>();
		    foreach(var t in m.ListEnumerations)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Enumeration.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListEnumerations.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListEnumerations' to 'proto_group_list_enumerations'
		public static Proto.Config.proto_group_list_enumerations ConvertToProto(GroupListEnumerations vm)
		{
		    Proto.Config.proto_group_list_enumerations m = new Proto.Config.proto_group_list_enumerations();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListEnumerations)
		        m.ListEnumerations.Add(vSharpStudio.vm.ViewModels.Enumeration.ConvertToProto((Enumeration)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListEnumerations)
				(t as Enumeration).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Enumeration> ListEnumerations 
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
		public void Add(Enumeration item) 
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
		partial void OnListEnumerationsChanging();
		partial void OnListEnumerationsChanged();
		#endregion Properties
	}
	public partial class Enumeration : ConfigObjectBase<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>, IAccept, IEnumeration
	{
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { }
		#region CTOR
		public Enumeration() : base(EnumerationValidator.Validator)
		{
			this.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>();
			OnInit();
		}
		public Enumeration(ITreeConfigNode parent) : base(EnumerationValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(EnumerationPair))
		    {
		        this.ListEnumerationPairs.Sort();
		    }
		}
		public static Enumeration Clone(Enumeration from, bool isDeep = true, bool isNewGuid = false)
		{
		    Enumeration vm = new Enumeration();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.DataTypeEnum = from.DataTypeEnum;
		    vm.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>();
		    foreach(var t in from.ListEnumerationPairs)
		        vm.ListEnumerationPairs.Add(vSharpStudio.vm.ViewModels.EnumerationPair.Clone((EnumerationPair)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Enumeration to, Enumeration from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    to.DataTypeEnum = from.DataTypeEnum;
		    if (isDeep)
		    {
		        foreach(var t in to.ListEnumerationPairs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListEnumerationPairs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.EnumerationPair.Update((EnumerationPair)t, (EnumerationPair)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.EnumerationPair.Update(p, (EnumerationPair)tt, isDeep);
		                to.ListEnumerationPairs.Add(p);
		            }
		        }
		    }
		}
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
		public static Enumeration ConvertToVM(Proto.Config.proto_enumeration m, Enumeration vm = null)
		{
		    if (vm == null)
		        vm = new Enumeration();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.DataTypeEnum = (EnumEnumerationType)m.DataTypeEnum;
		    vm.ListEnumerationPairs = new SortedObservableCollection<EnumerationPair>();
		    foreach(var t in m.ListEnumerationPairs)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.EnumerationPair.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListEnumerationPairs.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Enumeration' to 'proto_enumeration'
		public static Proto.Config.proto_enumeration ConvertToProto(Enumeration vm)
		{
		    Proto.Config.proto_enumeration m = new Proto.Config.proto_enumeration();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DataTypeEnum = (Proto.Config.enum_enumeration_type)vm.DataTypeEnum;
		    foreach(var t in vm.ListEnumerationPairs)
		        m.ListEnumerationPairs.Add(vSharpStudio.vm.ViewModels.EnumerationPair.ConvertToProto((EnumerationPair)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListEnumerationPairs)
				(t as EnumerationPair).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[PropertyOrderAttribute(4)]
		[DisplayName("Type")]
		public EnumEnumerationType DataTypeEnum
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
		partial void OnDataTypeEnumChanging();
		partial void OnDataTypeEnumChanged();
		[DisplayName("Elements")]
		[NewItemTypes(typeof(EnumerationPair))]
		public SortedObservableCollection<EnumerationPair> ListEnumerationPairs 
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
		partial void OnListEnumerationPairsChanging();
		partial void OnListEnumerationPairsChanged();
		#endregion Properties
	}
	public partial class EnumerationPair : ConfigObjectBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>, IAccept, IEnumerationPair
	{
		public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { }
		#region CTOR
		public EnumerationPair() : base(EnumerationPairValidator.Validator)
		{
			OnInit();
		}
		public EnumerationPair(ITreeConfigNode parent) : base(EnumerationPairValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static EnumerationPair Clone(EnumerationPair from, bool isDeep = true, bool isNewGuid = false)
		{
		    EnumerationPair vm = new EnumerationPair();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.Value = from.Value;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(EnumerationPair to, EnumerationPair from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    to.Value = from.Value;
		}
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
		public static EnumerationPair ConvertToVM(Proto.Config.proto_enumeration_pair m, EnumerationPair vm = null)
		{
		    if (vm == null)
		        vm = new EnumerationPair();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Value = m.Value;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'EnumerationPair' to 'proto_enumeration_pair'
		public static Proto.Config.proto_enumeration_pair ConvertToProto(EnumerationPair vm)
		{
		    Proto.Config.proto_enumeration_pair m = new Proto.Config.proto_enumeration_pair();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.Value = vm.Value;
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// TODO struct for different types, at least INTEGER
		///////////////////////////////////////////////////
		public string Value
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
		partial void OnValueChanging();
		partial void OnValueChanged();
		#endregion Properties
	}
	public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IAccept, ICatalog
	{
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog() : base(CatalogValidator.Validator)
		{
			this.GroupProperties = new GroupListProperties(this);
			this.GroupForms = new GroupListForms(this);
			this.GroupReports = new GroupListReports(this);
			OnInit();
		}
		public Catalog(ITreeConfigNode parent) : base(CatalogValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupProperties = new GroupListProperties(this);
			this.GroupForms = new GroupListForms(this);
			this.GroupReports = new GroupListReports(this);
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Catalog Clone(Catalog from, bool isDeep = true, bool isNewGuid = false)
		{
		    Catalog vm = new Catalog();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(from.GroupProperties, isDeep);
		    if (isDeep)
		        vm.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.Clone(from.GroupForms, isDeep);
		    if (isDeep)
		        vm.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.Clone(from.GroupReports, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Catalog to, Catalog from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
		    if (isDeep)
		        GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
		    if (isDeep)
		        GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
		}
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
		public static Catalog ConvertToVM(Proto.Config.proto_catalog m, Catalog vm = null)
		{
		    if (vm == null)
		        vm = new Catalog();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties);
		    vSharpStudio.vm.ViewModels.GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms);
		    vSharpStudio.vm.ViewModels.GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Catalog' to 'proto_catalog'
		public static Proto.Config.proto_catalog ConvertToProto(Catalog vm)
		{
		    Proto.Config.proto_catalog m = new Proto.Config.proto_catalog();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto(vm.GroupProperties);
		    m.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.ConvertToProto(vm.GroupForms);
		    m.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.ConvertToProto(vm.GroupReports);
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupForms.AcceptConfigNode(visitor);
			this.GroupReports.AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties
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
		partial void OnGroupPropertiesChanging();
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListForms GroupForms
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
		partial void OnGroupFormsChanging();
		partial void OnGroupFormsChanged();
		[BrowsableAttribute(false)]
		public GroupListReports GroupReports
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
		partial void OnGroupReportsChanging();
		partial void OnGroupReportsChanged();
		#endregion Properties
	}
	public partial class GroupListCatalogs : ConfigObjectBase<GroupListCatalogs, GroupListCatalogs.GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IAccept, IGroupListCatalogs
	{
		public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { }
		#region CTOR
		public GroupListCatalogs() : base(GroupListCatalogsValidator.Validator)
		{
			this.ListCatalogs = new SortedObservableCollection<Catalog>();
			OnInit();
		}
		public GroupListCatalogs(ITreeConfigNode parent) : base(GroupListCatalogsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListCatalogs = new SortedObservableCollection<Catalog>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Catalog))
		    {
		        this.ListCatalogs.Sort();
		    }
		}
		public static GroupListCatalogs Clone(GroupListCatalogs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListCatalogs vm = new GroupListCatalogs();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in from.ListCatalogs)
		        vm.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.Clone((Catalog)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListCatalogs to, GroupListCatalogs from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListCatalogs.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListCatalogs)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Catalog.Update((Catalog)t, (Catalog)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Catalog.Update(p, (Catalog)tt, isDeep);
		                to.ListCatalogs.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListCatalogs ConvertToVM(Proto.Config.proto_group_list_catalogs m, GroupListCatalogs vm = null)
		{
		    if (vm == null)
		        vm = new GroupListCatalogs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListCatalogs = new SortedObservableCollection<Catalog>();
		    foreach(var t in m.ListCatalogs)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Catalog.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListCatalogs.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
		public static Proto.Config.proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm)
		{
		    Proto.Config.proto_group_list_catalogs m = new Proto.Config.proto_group_list_catalogs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListCatalogs)
		        m.ListCatalogs.Add(vSharpStudio.vm.ViewModels.Catalog.ConvertToProto((Catalog)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListCatalogs)
				(t as Catalog).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Catalog> ListCatalogs 
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
		public void Add(Catalog item) 
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
		partial void OnListCatalogsChanging();
		partial void OnListCatalogsChanged();
		#endregion Properties
	}
	public partial class GroupDocuments : ConfigObjectBase<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IAccept, IGroupDocuments
	{
		public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { }
		#region CTOR
		public GroupDocuments() : base(GroupDocumentsValidator.Validator)
		{
			this.GroupSharedProperties = new GroupListProperties(this);
			this.GroupListDocuments = new GroupListDocuments(this);
			OnInit();
		}
		public GroupDocuments(ITreeConfigNode parent) : base(GroupDocumentsValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupSharedProperties = new GroupListProperties(this);
			this.GroupListDocuments = new GroupListDocuments(this);
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static GroupDocuments Clone(GroupDocuments from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupDocuments vm = new GroupDocuments();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.GroupSharedProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(from.GroupSharedProperties, isDeep);
		    if (isDeep)
		        vm.GroupListDocuments = vSharpStudio.vm.ViewModels.GroupListDocuments.Clone(from.GroupListDocuments, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupDocuments to, GroupDocuments from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        GroupListProperties.Update(to.GroupSharedProperties, from.GroupSharedProperties, isDeep);
		    if (isDeep)
		        GroupListDocuments.Update(to.GroupListDocuments, from.GroupListDocuments, isDeep);
		}
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
		public static GroupDocuments ConvertToVM(Proto.Config.proto_group_documents m, GroupDocuments vm = null)
		{
		    if (vm == null)
		        vm = new GroupDocuments();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupSharedProperties, vm.GroupSharedProperties);
		    vSharpStudio.vm.ViewModels.GroupListDocuments.ConvertToVM(m.GroupListDocuments, vm.GroupListDocuments);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupDocuments' to 'proto_group_documents'
		public static Proto.Config.proto_group_documents ConvertToProto(GroupDocuments vm)
		{
		    Proto.Config.proto_group_documents m = new Proto.Config.proto_group_documents();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.GroupSharedProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto(vm.GroupSharedProperties);
		    m.GroupListDocuments = vSharpStudio.vm.ViewModels.GroupListDocuments.ConvertToProto(vm.GroupListDocuments);
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupListDocuments.AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupSharedProperties
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
		partial void OnGroupSharedPropertiesChanging();
		partial void OnGroupSharedPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListDocuments GroupListDocuments
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
		partial void OnGroupListDocumentsChanging();
		partial void OnGroupListDocumentsChanged();
		#endregion Properties
	}
	public partial class Document : ConfigObjectBase<Document, Document.DocumentValidator>, IComparable<Document>, IAccept, IDocument
	{
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document() : base(DocumentValidator.Validator)
		{
			this.GroupProperties = new GroupListProperties(this);
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this);
			this.GroupForms = new GroupListForms(this);
			this.GroupReports = new GroupListReports(this);
			OnInit();
		}
		public Document(ITreeConfigNode parent) : base(DocumentValidator.Validator)
	    {
	        this.Parent = parent;
			this.GroupProperties = new GroupListProperties(this);
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this);
			this.GroupForms = new GroupListForms(this);
			this.GroupReports = new GroupListReports(this);
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Document Clone(Document from, bool isDeep = true, bool isNewGuid = false)
		{
		    Document vm = new Document();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.Clone(from.GroupProperties, isDeep);
		    if (isDeep)
		        vm.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.Clone(from.GroupPropertiesTabs, isDeep);
		    if (isDeep)
		        vm.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.Clone(from.GroupForms, isDeep);
		    if (isDeep)
		        vm.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.Clone(from.GroupReports, isDeep);
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Document to, Document from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		        GroupListProperties.Update(to.GroupProperties, from.GroupProperties, isDeep);
		    if (isDeep)
		        GroupListPropertiesTabs.Update(to.GroupPropertiesTabs, from.GroupPropertiesTabs, isDeep);
		    if (isDeep)
		        GroupListForms.Update(to.GroupForms, from.GroupForms, isDeep);
		    if (isDeep)
		        GroupListReports.Update(to.GroupReports, from.GroupReports, isDeep);
		}
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
		public static Document ConvertToVM(Proto.Config.proto_document m, Document vm = null)
		{
		    if (vm == null)
		        vm = new Document();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties);
		    vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToVM(m.GroupPropertiesTabs, vm.GroupPropertiesTabs);
		    vSharpStudio.vm.ViewModels.GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms);
		    vSharpStudio.vm.ViewModels.GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Document' to 'proto_document'
		public static Proto.Config.proto_document ConvertToProto(Document vm)
		{
		    Proto.Config.proto_document m = new Proto.Config.proto_document();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto(vm.GroupProperties);
		    m.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs);
		    m.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.ConvertToProto(vm.GroupForms);
		    m.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.ConvertToProto(vm.GroupReports);
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.GroupForms.AcceptConfigNode(visitor);
			this.GroupReports.AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public GroupListProperties GroupProperties
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
		partial void OnGroupPropertiesChanging();
		partial void OnGroupPropertiesChanged();
		[BrowsableAttribute(false)]
		public GroupListPropertiesTabs GroupPropertiesTabs
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
		partial void OnGroupPropertiesTabsChanging();
		partial void OnGroupPropertiesTabsChanged();
		[BrowsableAttribute(false)]
		public GroupListForms GroupForms
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
		partial void OnGroupFormsChanging();
		partial void OnGroupFormsChanged();
		[BrowsableAttribute(false)]
		public GroupListReports GroupReports
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
		partial void OnGroupReportsChanging();
		partial void OnGroupReportsChanged();
		#endregion Properties
	}
	public partial class GroupListDocuments : ConfigObjectBase<GroupListDocuments, GroupListDocuments.GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IAccept, IGroupListDocuments
	{
		public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { }
		#region CTOR
		public GroupListDocuments() : base(GroupListDocumentsValidator.Validator)
		{
			this.ListDocuments = new SortedObservableCollection<Document>();
			OnInit();
		}
		public GroupListDocuments(ITreeConfigNode parent) : base(GroupListDocumentsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListDocuments = new SortedObservableCollection<Document>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Document))
		    {
		        this.ListDocuments.Sort();
		    }
		}
		public static GroupListDocuments Clone(GroupListDocuments from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListDocuments vm = new GroupListDocuments();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in from.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.Clone((Document)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListDocuments to, GroupListDocuments from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListDocuments.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListDocuments)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Document.Update((Document)t, (Document)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Document.Update(p, (Document)tt, isDeep);
		                to.ListDocuments.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListDocuments ConvertToVM(Proto.Config.proto_group_list_documents m, GroupListDocuments vm = null)
		{
		    if (vm == null)
		        vm = new GroupListDocuments();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in m.ListDocuments)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Document.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListDocuments.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
		public static Proto.Config.proto_group_list_documents ConvertToProto(GroupListDocuments vm)
		{
		    Proto.Config.proto_group_list_documents m = new Proto.Config.proto_group_list_documents();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListDocuments)
		        m.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.ConvertToProto((Document)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListDocuments)
				(t as Document).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Document> ListDocuments 
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
		public void Add(Document item) 
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
		partial void OnListDocumentsChanging();
		partial void OnListDocumentsChanged();
		#endregion Properties
	}
	public partial class GroupListJournals : ConfigObjectBase<GroupListJournals, GroupListJournals.GroupListJournalsValidator>, IComparable<GroupListJournals>, IAccept, IGroupListJournals
	{
		public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { }
		#region CTOR
		public GroupListJournals() : base(GroupListJournalsValidator.Validator)
		{
			this.ListJournals = new SortedObservableCollection<Journal>();
			OnInit();
		}
		public GroupListJournals(ITreeConfigNode parent) : base(GroupListJournalsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListJournals = new SortedObservableCollection<Journal>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Journal))
		    {
		        this.ListJournals.Sort();
		    }
		}
		public static GroupListJournals Clone(GroupListJournals from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListJournals vm = new GroupListJournals();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListJournals = new SortedObservableCollection<Journal>();
		    foreach(var t in from.ListJournals)
		        vm.ListJournals.Add(vSharpStudio.vm.ViewModels.Journal.Clone((Journal)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListJournals to, GroupListJournals from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListJournals.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListJournals)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Journal.Update((Journal)t, (Journal)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Journal.Update(p, (Journal)tt, isDeep);
		                to.ListJournals.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListJournals ConvertToVM(Proto.Config.proto_group_list_journals m, GroupListJournals vm = null)
		{
		    if (vm == null)
		        vm = new GroupListJournals();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListJournals = new SortedObservableCollection<Journal>();
		    foreach(var t in m.ListJournals)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Journal.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListJournals.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListJournals' to 'proto_group_list_journals'
		public static Proto.Config.proto_group_list_journals ConvertToProto(GroupListJournals vm)
		{
		    Proto.Config.proto_group_list_journals m = new Proto.Config.proto_group_list_journals();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListJournals)
		        m.ListJournals.Add(vSharpStudio.vm.ViewModels.Journal.ConvertToProto((Journal)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListJournals)
				(t as Journal).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Journal> ListJournals 
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
		public void Add(Journal item) 
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
		partial void OnListJournalsChanging();
		partial void OnListJournalsChanged();
		#endregion Properties
	}
	public partial class Journal : ConfigObjectBase<Journal, Journal.JournalValidator>, IComparable<Journal>, IAccept, IJournal
	{
		public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { }
		#region CTOR
		public Journal() : base(JournalValidator.Validator)
		{
			this.ListDocuments = new SortedObservableCollection<Document>();
			OnInit();
		}
		public Journal(ITreeConfigNode parent) : base(JournalValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListDocuments = new SortedObservableCollection<Document>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Document))
		    {
		        this.ListDocuments.Sort();
		    }
		}
		public static Journal Clone(Journal from, bool isDeep = true, bool isNewGuid = false)
		{
		    Journal vm = new Journal();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in from.ListDocuments)
		        vm.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.Clone((Document)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Journal to, Journal from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListDocuments.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListDocuments)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Document.Update((Document)t, (Document)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Document.Update(p, (Document)tt, isDeep);
		                to.ListDocuments.Add(p);
		            }
		        }
		    }
		}
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
		public static Journal ConvertToVM(Proto.Config.proto_journal m, Journal vm = null)
		{
		    if (vm == null)
		        vm = new Journal();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListDocuments = new SortedObservableCollection<Document>();
		    foreach(var t in m.ListDocuments)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Document.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListDocuments.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Journal' to 'proto_journal'
		public static Proto.Config.proto_journal ConvertToProto(Journal vm)
		{
		    Proto.Config.proto_journal m = new Proto.Config.proto_journal();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListDocuments)
		        m.ListDocuments.Add(vSharpStudio.vm.ViewModels.Document.ConvertToProto((Document)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListDocuments)
				(t as Document).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_group_properties list_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Document> ListDocuments 
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
		partial void OnListDocumentsChanging();
		partial void OnListDocumentsChanged();
		#endregion Properties
	}
	public partial class GroupListForms : ConfigObjectBase<GroupListForms, GroupListForms.GroupListFormsValidator>, IComparable<GroupListForms>, IAccept, IGroupListForms
	{
		public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { }
		#region CTOR
		public GroupListForms() : base(GroupListFormsValidator.Validator)
		{
			this.ListForms = new SortedObservableCollection<Form>();
			OnInit();
		}
		public GroupListForms(ITreeConfigNode parent) : base(GroupListFormsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListForms = new SortedObservableCollection<Form>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Form))
		    {
		        this.ListForms.Sort();
		    }
		}
		public static GroupListForms Clone(GroupListForms from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListForms vm = new GroupListForms();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListForms = new SortedObservableCollection<Form>();
		    foreach(var t in from.ListForms)
		        vm.ListForms.Add(vSharpStudio.vm.ViewModels.Form.Clone((Form)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListForms to, GroupListForms from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListForms.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListForms)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Form.Update((Form)t, (Form)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Form.Update(p, (Form)tt, isDeep);
		                to.ListForms.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListForms ConvertToVM(Proto.Config.proto_group_list_forms m, GroupListForms vm = null)
		{
		    if (vm == null)
		        vm = new GroupListForms();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListForms = new SortedObservableCollection<Form>();
		    foreach(var t in m.ListForms)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Form.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListForms.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListForms' to 'proto_group_list_forms'
		public static Proto.Config.proto_group_list_forms ConvertToProto(GroupListForms vm)
		{
		    Proto.Config.proto_group_list_forms m = new Proto.Config.proto_group_list_forms();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListForms)
		        m.ListForms.Add(vSharpStudio.vm.ViewModels.Form.ConvertToProto((Form)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListForms)
				(t as Form).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Form> ListForms 
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
		public void Add(Form item) 
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
		partial void OnListFormsChanging();
		partial void OnListFormsChanged();
		#endregion Properties
	}
	public partial class Form : ConfigObjectBase<Form, Form.FormValidator>, IComparable<Form>, IAccept, IForm
	{
		public partial class FormValidator : ValidatorBase<Form, FormValidator> { }
		#region CTOR
		public Form() : base(FormValidator.Validator)
		{
			OnInit();
		}
		public Form(ITreeConfigNode parent) : base(FormValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Form Clone(Form from, bool isDeep = true, bool isNewGuid = false)
		{
		    Form vm = new Form();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Form to, Form from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		}
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
		public static Form ConvertToVM(Proto.Config.proto_form m, Form vm = null)
		{
		    if (vm == null)
		        vm = new Form();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Form' to 'proto_form'
		public static Proto.Config.proto_form ConvertToProto(Form vm)
		{
		    Proto.Config.proto_form m = new Proto.Config.proto_form();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
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
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		#endregion Properties
	}
	public partial class GroupListReports : ConfigObjectBase<GroupListReports, GroupListReports.GroupListReportsValidator>, IComparable<GroupListReports>, IAccept, IGroupListReports
	{
		public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { }
		#region CTOR
		public GroupListReports() : base(GroupListReportsValidator.Validator)
		{
			this.ListReports = new SortedObservableCollection<Report>();
			OnInit();
		}
		public GroupListReports(ITreeConfigNode parent) : base(GroupListReportsValidator.Validator)
	    {
	        this.Parent = parent;
			this.ListReports = new SortedObservableCollection<Report>();
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Report))
		    {
		        this.ListReports.Sort();
		    }
		}
		public static GroupListReports Clone(GroupListReports from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupListReports vm = new GroupListReports();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    vm.ListReports = new SortedObservableCollection<Report>();
		    foreach(var t in from.ListReports)
		        vm.ListReports.Add(vSharpStudio.vm.ViewModels.Report.Clone((Report)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupListReports to, GroupListReports from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.ListReports.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListReports)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Report.Update((Report)t, (Report)tt, isDeep);
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
		                vSharpStudio.vm.ViewModels.Report.Update(p, (Report)tt, isDeep);
		                to.ListReports.Add(p);
		            }
		        }
		    }
		}
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
		public static GroupListReports ConvertToVM(Proto.Config.proto_group_list_reports m, GroupListReports vm = null)
		{
		    if (vm == null)
		        vm = new GroupListReports();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.ListReports = new SortedObservableCollection<Report>();
		    foreach(var t in m.ListReports)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Report.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListReports.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListReports' to 'proto_group_list_reports'
		public static Proto.Config.proto_group_list_reports ConvertToProto(GroupListReports vm)
		{
		    Proto.Config.proto_group_list_reports m = new Proto.Config.proto_group_list_reports();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.ListReports)
		        m.ListReports.Add(vSharpStudio.vm.ViewModels.Report.ConvertToProto((Report)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListReports)
				(t as Report).AcceptConfigNode(visitor);
			visitor.VisitEnd(this);
		}
		#endregion Procedures
		#region Properties
		
		[PropertyOrderAttribute(3)]
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		
		///////////////////////////////////////////////////
		/// repeated proto_property list_shared_properties = 6;
		///////////////////////////////////////////////////
		[BrowsableAttribute(false)]
		public SortedObservableCollection<Report> ListReports 
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
		public void Add(Report item) 
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
		partial void OnListReportsChanging();
		partial void OnListReportsChanged();
		#endregion Properties
	}
	public partial class Report : ConfigObjectBase<Report, Report.ReportValidator>, IComparable<Report>, IAccept, IReport
	{
		public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { }
		#region CTOR
		public Report() : base(ReportValidator.Validator)
		{
			OnInit();
		}
		public Report(ITreeConfigNode parent) : base(ReportValidator.Validator)
	    {
	        this.Parent = parent;
			OnInit();
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static Report Clone(Report from, bool isDeep = true, bool isNewGuid = false)
		{
		    Report vm = new Report();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.NameUi = from.NameUi;
		    vm.Description = from.Description;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(Report to, Report from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.NameUi = from.NameUi;
		    to.Description = from.Description;
		}
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
		public static Report ConvertToVM(Proto.Config.proto_report m, Report vm = null)
		{
		    if (vm == null)
		        vm = new Report();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Report' to 'proto_report'
		public static Proto.Config.proto_report ConvertToProto(Report vm)
		{
		    Proto.Config.proto_report m = new Proto.Config.proto_report();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
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
		public string Description
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
		partial void OnDescriptionChanging();
		partial void OnDescriptionChanged();
		#endregion Properties
	}
	
	public interface IVisitorConfigNode
	{
	    CancellationToken Token { get; }
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
		void Visit(GroupListBaseConfigs p);
		void VisitEnd(GroupListBaseConfigs p);
		void Visit(BaseConfig p);
		void VisitEnd(BaseConfig p);
		void Visit(Config p);
		void VisitEnd(Config p);
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
	
	public interface IVisitorProto
	{
		void Visit(Proto.Config.proto_group_list_plugins p);
		void Visit(Proto.Config.proto_plugin p);
		void Visit(Proto.Config.proto_plugin_generator p);
		void Visit(Proto.Config.proto_plugin_generator_settings p);
		void Visit(Proto.Config.proto_settings_config p);
		void Visit(Proto.Config.db_settings p);
		void Visit(Proto.Config.proto_group_list_base_configs p);
		void Visit(Proto.Config.proto_base_config p);
		void Visit(Proto.Config.proto_config p);
		void Visit(Proto.Config.proto_data_type p);
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
	}
	
	public partial class ValidationVisitor : IVisitorConfigNode
	{
	    CancellationToken IVisitorConfigNode.Token => _cancellationToken;
	    private CancellationToken _cancellationToken;
		public void Visit(GroupListPlugins p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListPlugins p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Plugin p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Plugin p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(PluginGenerator p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(PluginGenerator p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(PluginGeneratorSettings p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(PluginGeneratorSettings p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(SettingsConfig p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(SettingsConfig p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(DbSettings p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(DbSettings p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListBaseConfigs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListBaseConfigs p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(BaseConfig p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(BaseConfig p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Config p)
	    {
	        OnVisit(p);
	        ValidateSubAndCollectErrors(p, p.DbSettings);
	    }
		public void VisitEnd(Config p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(DataType p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(DataType p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListPropertiesTabs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListPropertiesTabs p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(PropertiesTab p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(PropertiesTab p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListProperties p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListProperties p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Property p)
	    {
	        OnVisit(p);
	        ValidateSubAndCollectErrors(p, p.DataType);
	    }
		public void VisitEnd(Property p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListConstants p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListConstants p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Constant p)
	    {
	        OnVisit(p);
	        ValidateSubAndCollectErrors(p, p.DataType);
	    }
		public void VisitEnd(Constant p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListEnumerations p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListEnumerations p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Enumeration p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Enumeration p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(EnumerationPair p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(EnumerationPair p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Catalog p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Catalog p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListCatalogs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListCatalogs p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupDocuments p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupDocuments p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Document p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Document p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListDocuments p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListDocuments p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListJournals p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListJournals p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Journal p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Journal p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListForms p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListForms p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Form p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Form p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupListReports p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupListReports p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Report p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(Report p)
	    {
	        OnVisitEnd(p);
	    }
	}
	
	public partial class ConfigVisitor : IVisitorConfigNode
	{
	    CancellationToken IVisitorConfigNode.Token => _cancellationToken;
	    private CancellationToken _cancellationToken;
	
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
	}
}