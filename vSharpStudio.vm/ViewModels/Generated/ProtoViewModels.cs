// Auto generated on UTC 06/01/2019 21:06:03
using System;
using System.Linq;
using ViewModelBase;
using FluentValidation;
using Proto.Config;
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
	public partial class GroupListPlugins : ConfigObjectBase<GroupListPlugins, GroupListPlugins.GroupListPluginsValidator>, IComparable<GroupListPlugins>, IAccept
	{
		public partial class GroupListPluginsValidator : ValidatorBase<GroupListPlugins, GroupListPluginsValidator> { }
		#region CTOR
		public GroupListPlugins() : base(GroupListPluginsValidator.Validator)
		{
			this.ListPlugins = new SortedObservableCollection<Plugin>();
			OnInit();
		}
		public GroupListPlugins(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListPlugins.DefaultName, this, this.SubNodes);
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
		public static GroupListPlugins ConvertToVM(proto_group_list_plugins m, GroupListPlugins vm = null)
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
		public static proto_group_list_plugins ConvertToProto(GroupListPlugins vm)
		{
		    proto_group_list_plugins m = new proto_group_list_plugins();
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
		partial void OnListPluginsChanging();
		partial void OnListPluginsChanged();
		#endregion Properties
	}
	public partial class Plugin : ConfigObjectBase<Plugin, Plugin.PluginValidator>, IComparable<Plugin>, IAccept
	{
		public partial class PluginValidator : ValidatorBase<Plugin, PluginValidator> { }
		#region CTOR
		public Plugin() : base(PluginValidator.Validator)
		{
			this.ListPluginGenerators = new SortedObservableCollection<PluginGenerator>();
			OnInit();
		}
		public Plugin(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Plugin.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(PluginGenerator))
		    {
		        this.ListPluginGenerators.Sort();
		    }
		}
		public static Plugin Clone(Plugin from, bool isDeep = true, bool isNewGuid = false)
		{
		    Plugin vm = new Plugin();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.Description = from.Description;
		    vm.SortingValue = from.SortingValue;
		    vm.ListPluginGenerators = new SortedObservableCollection<PluginGenerator>();
		    foreach(var t in from.ListPluginGenerators)
		        vm.ListPluginGenerators.Add(vSharpStudio.vm.ViewModels.PluginGenerator.Clone((PluginGenerator)t, isDeep));
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
		        foreach(var t in to.ListPluginGenerators.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPluginGenerators)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.PluginGenerator.Update((PluginGenerator)t, (PluginGenerator)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListPluginGenerators.Remove(t);
		        }
		        foreach(var tt in from.ListPluginGenerators)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListPluginGenerators.ToList())
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
		                to.ListPluginGenerators.Add(p);
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
		public static Plugin ConvertToVM(proto_plugin m, Plugin vm = null)
		{
		    if (vm == null)
		        vm = new Plugin();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.Description = m.Description;
		    vm.SortingValue = m.SortingValue;
		    vm.ListPluginGenerators = new SortedObservableCollection<PluginGenerator>();
		    foreach(var t in m.ListPluginGenerators)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.PluginGenerator.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListPluginGenerators.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Plugin' to 'proto_plugin'
		public static proto_plugin ConvertToProto(Plugin vm)
		{
		    proto_plugin m = new proto_plugin();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.Description = vm.Description;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListPluginGenerators)
		        m.ListPluginGenerators.Add(vSharpStudio.vm.ViewModels.PluginGenerator.ConvertToProto((PluginGenerator)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPluginGenerators)
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
		public SortedObservableCollection<PluginGenerator> ListPluginGenerators 
		{ 
			set
			{
				if (_ListPluginGenerators != value)
				{
					OnListPluginGeneratorsChanging();
					_ListPluginGenerators = value;
					OnListPluginGeneratorsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPluginGenerators; }
		}
		private SortedObservableCollection<PluginGenerator> _ListPluginGenerators;
		partial void OnListPluginGeneratorsChanging();
		partial void OnListPluginGeneratorsChanged();
		#endregion Properties
	}
	public partial class PluginGenerator : ConfigObjectBase<PluginGenerator, PluginGenerator.PluginGeneratorValidator>, IComparable<PluginGenerator>, IAccept
	{
		public partial class PluginGeneratorValidator : ValidatorBase<PluginGenerator, PluginGeneratorValidator> { }
		#region CTOR
		public PluginGenerator() : base(PluginGeneratorValidator.Validator)
		{
			this.ListPluginGeneratorSettings = new SortedObservableCollection<PluginGeneratorSettings>();
			OnInit();
		}
		public PluginGenerator(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(PluginGenerator.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(PluginGeneratorSettings))
		    {
		        this.ListPluginGeneratorSettings.Sort();
		    }
		}
		public static PluginGenerator Clone(PluginGenerator from, bool isDeep = true, bool isNewGuid = false)
		{
		    PluginGenerator vm = new PluginGenerator();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.Description = from.Description;
		    vm.SortingValue = from.SortingValue;
		    vm.ListPluginGeneratorSettings = new SortedObservableCollection<PluginGeneratorSettings>();
		    foreach(var t in from.ListPluginGeneratorSettings)
		        vm.ListPluginGeneratorSettings.Add(vSharpStudio.vm.ViewModels.PluginGeneratorSettings.Clone((PluginGeneratorSettings)t, isDeep));
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
		        foreach(var t in to.ListPluginGeneratorSettings.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.ListPluginGeneratorSettings)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.PluginGeneratorSettings.Update((PluginGeneratorSettings)t, (PluginGeneratorSettings)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.ListPluginGeneratorSettings.Remove(t);
		        }
		        foreach(var tt in from.ListPluginGeneratorSettings)
		        {
		            bool isfound = false;
		            foreach(var t in to.ListPluginGeneratorSettings.ToList())
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
		                to.ListPluginGeneratorSettings.Add(p);
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
		public static PluginGenerator ConvertToVM(proto_plugin_generator m, PluginGenerator vm = null)
		{
		    if (vm == null)
		        vm = new PluginGenerator();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.Description = m.Description;
		    vm.SortingValue = m.SortingValue;
		    vm.ListPluginGeneratorSettings = new SortedObservableCollection<PluginGeneratorSettings>();
		    foreach(var t in m.ListPluginGeneratorSettings)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.PluginGeneratorSettings.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.ListPluginGeneratorSettings.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'PluginGenerator' to 'proto_plugin_generator'
		public static proto_plugin_generator ConvertToProto(PluginGenerator vm)
		{
		    proto_plugin_generator m = new proto_plugin_generator();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.Description = vm.Description;
		    m.SortingValue = vm.SortingValue;
		    foreach(var t in vm.ListPluginGeneratorSettings)
		        m.ListPluginGeneratorSettings.Add(vSharpStudio.vm.ViewModels.PluginGeneratorSettings.ConvertToProto((PluginGeneratorSettings)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.ListPluginGeneratorSettings)
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
		public SortedObservableCollection<PluginGeneratorSettings> ListPluginGeneratorSettings 
		{ 
			set
			{
				if (_ListPluginGeneratorSettings != value)
				{
					OnListPluginGeneratorSettingsChanging();
					_ListPluginGeneratorSettings = value;
					OnListPluginGeneratorSettingsChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _ListPluginGeneratorSettings; }
		}
		private SortedObservableCollection<PluginGeneratorSettings> _ListPluginGeneratorSettings;
		partial void OnListPluginGeneratorSettingsChanging();
		partial void OnListPluginGeneratorSettingsChanged();
		#endregion Properties
	}
	public partial class PluginGeneratorSettings : ConfigObjectBase<PluginGeneratorSettings, PluginGeneratorSettings.PluginGeneratorSettingsValidator>, IComparable<PluginGeneratorSettings>, IAccept
	{
		public partial class PluginGeneratorSettingsValidator : ValidatorBase<PluginGeneratorSettings, PluginGeneratorSettingsValidator> { }
		#region CTOR
		public PluginGeneratorSettings() : base(PluginGeneratorSettingsValidator.Validator)
		{
			OnInit();
		}
		public PluginGeneratorSettings(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(PluginGeneratorSettings.DefaultName, this, this.SubNodes);
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
		public static PluginGeneratorSettings ConvertToVM(proto_plugin_generator_settings m, PluginGeneratorSettings vm = null)
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
		public static proto_plugin_generator_settings ConvertToProto(PluginGeneratorSettings vm)
		{
		    proto_plugin_generator_settings m = new proto_plugin_generator_settings();
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
	public partial class SettingsConfig : ConfigObjectBase<SettingsConfig, SettingsConfig.SettingsConfigValidator>, IComparable<SettingsConfig>, IAccept
	{
		public partial class SettingsConfigValidator : ValidatorBase<SettingsConfig, SettingsConfigValidator> { }
		#region CTOR
		public SettingsConfig() : base(SettingsConfigValidator.Validator)
		{
			OnInit();
		}
		public SettingsConfig(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(SettingsConfig.DefaultName, this, this.SubNodes);
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
		public static SettingsConfig ConvertToVM(proto_settings_config m, SettingsConfig vm = null)
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
		public static proto_settings_config ConvertToProto(SettingsConfig vm)
		{
		    proto_settings_config m = new proto_settings_config();
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
	/// Primary key generation strategy
	///////////////////////////////////////////////////
	public partial class DbIdGenerator : ViewModelValidatableWithSeverity<DbIdGenerator, DbIdGenerator.DbIdGeneratorValidator>
	{
		public partial class DbIdGeneratorValidator : ValidatorBase<DbIdGenerator, DbIdGeneratorValidator> { }
		#region CTOR
		public DbIdGenerator() : base(DbIdGeneratorValidator.Validator)
		{
			OnInit();
		}
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public static DbIdGenerator Clone(DbIdGenerator from, bool isDeep = true)
		{
		    DbIdGenerator vm = new DbIdGenerator();
		    vm.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered.HasValue ? from.IsPrimaryKeyClustered.Value : (bool?)null;
		    vm.IsMemoryOptimized = from.IsMemoryOptimized.HasValue ? from.IsMemoryOptimized.Value : (bool?)null;
		    vm.IsSequenceHiLo = from.IsSequenceHiLo.HasValue ? from.IsSequenceHiLo.Value : (bool?)null;
		    vm.HiLoSequenceName = from.HiLoSequenceName;
		    vm.HiLoSchema = from.HiLoSchema;
		    return vm;
		}
		public static void Update(DbIdGenerator to, DbIdGenerator from, bool isDeep = true)
		{
		    to.IsPrimaryKeyClustered = from.IsPrimaryKeyClustered.HasValue ? from.IsPrimaryKeyClustered.Value : (bool?)null;
		    to.IsMemoryOptimized = from.IsMemoryOptimized.HasValue ? from.IsMemoryOptimized.Value : (bool?)null;
		    to.IsSequenceHiLo = from.IsSequenceHiLo.HasValue ? from.IsSequenceHiLo.Value : (bool?)null;
		    to.HiLoSequenceName = from.HiLoSequenceName;
		    to.HiLoSchema = from.HiLoSchema;
		}
		#region IEditable
		public override DbIdGenerator Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return DbIdGenerator.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(DbIdGenerator from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    DbIdGenerator.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'db_id_generator' to 'DbIdGenerator'
		public static DbIdGenerator ConvertToVM(db_id_generator m, DbIdGenerator vm = null)
		{
		    if (vm == null)
		        vm = new DbIdGenerator();
		    vm.IsPrimaryKeyClustered = m.IsPrimaryKeyClustered.HasValue ? m.IsPrimaryKeyClustered.Value : (bool?)null;
		    vm.IsMemoryOptimized = m.IsMemoryOptimized.HasValue ? m.IsMemoryOptimized.Value : (bool?)null;
		    vm.IsSequenceHiLo = m.IsSequenceHiLo.HasValue ? m.IsSequenceHiLo.Value : (bool?)null;
		    vm.HiLoSequenceName = m.HiLoSequenceName;
		    vm.HiLoSchema = m.HiLoSchema;
		    return vm;
		}
		// Conversion from 'DbIdGenerator' to 'db_id_generator'
		public static db_id_generator ConvertToProto(DbIdGenerator vm)
		{
		    db_id_generator m = new db_id_generator();
		    m.IsPrimaryKeyClustered = new bool_nullable();
		    m.IsPrimaryKeyClustered.HasValue = vm.IsPrimaryKeyClustered.HasValue;
		    if (vm.IsPrimaryKeyClustered.HasValue)
		        m.IsPrimaryKeyClustered.Value = vm.IsPrimaryKeyClustered.Value;
		    m.IsMemoryOptimized = new bool_nullable();
		    m.IsMemoryOptimized.HasValue = vm.IsMemoryOptimized.HasValue;
		    if (vm.IsMemoryOptimized.HasValue)
		        m.IsMemoryOptimized.Value = vm.IsMemoryOptimized.Value;
		    m.IsSequenceHiLo = new bool_nullable();
		    m.IsSequenceHiLo.HasValue = vm.IsSequenceHiLo.HasValue;
		    if (vm.IsSequenceHiLo.HasValue)
		        m.IsSequenceHiLo.Value = vm.IsSequenceHiLo.Value;
		    m.HiLoSequenceName = vm.HiLoSequenceName;
		    m.HiLoSchema = vm.HiLoSchema;
		    return m;
		}
		#endregion Procedures
		#region Properties
		
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		public bool? IsPrimaryKeyClustered
		{ 
			set
			{
				if (_IsPrimaryKeyClustered != value)
				{
					OnIsPrimaryKeyClusteredChanging();
		            _IsPrimaryKeyClustered = value;
					OnIsPrimaryKeyClusteredChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsPrimaryKeyClustered; }
		}
		private bool? _IsPrimaryKeyClustered;
		partial void OnIsPrimaryKeyClusteredChanging();
		partial void OnIsPrimaryKeyClusteredChanged();
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		public bool? IsMemoryOptimized
		{ 
			set
			{
				if (_IsMemoryOptimized != value)
				{
					OnIsMemoryOptimizedChanging();
		            _IsMemoryOptimized = value;
					OnIsMemoryOptimizedChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsMemoryOptimized; }
		}
		private bool? _IsMemoryOptimized;
		partial void OnIsMemoryOptimizedChanging();
		partial void OnIsMemoryOptimizedChanged();
		
		///////////////////////////////////////////////////
		/// SequenceHiLo or IdentityColumn. MsSql
		///////////////////////////////////////////////////
		public bool? IsSequenceHiLo
		{ 
			set
			{
				if (_IsSequenceHiLo != value)
				{
					OnIsSequenceHiLoChanging();
		            _IsSequenceHiLo = value;
					OnIsSequenceHiLoChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsSequenceHiLo; }
		}
		private bool? _IsSequenceHiLo;
		partial void OnIsSequenceHiLoChanging();
		partial void OnIsSequenceHiLoChanged();
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		public string HiLoSequenceName
		{ 
			set
			{
				if (_HiLoSequenceName != value)
				{
					OnHiLoSequenceNameChanging();
					_HiLoSequenceName = value;
					OnHiLoSequenceNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _HiLoSequenceName; }
		}
		private string _HiLoSequenceName = "";
		partial void OnHiLoSequenceNameChanging();
		partial void OnHiLoSequenceNameChanged();
		
		///////////////////////////////////////////////////
		/// MsSql
		///////////////////////////////////////////////////
		public string HiLoSchema
		{ 
			set
			{
				if (_HiLoSchema != value)
				{
					OnHiLoSchemaChanging();
					_HiLoSchema = value;
					OnHiLoSchemaChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _HiLoSchema; }
		}
		private string _HiLoSchema = "";
		partial void OnHiLoSchemaChanging();
		partial void OnHiLoSchemaChanged();
		#endregion Properties
	}
	public partial class GroupConfigs : ConfigObjectBase<GroupConfigs, GroupConfigs.GroupConfigsValidator>, IComparable<GroupConfigs>, IAccept
	{
		public partial class GroupConfigsValidator : ValidatorBase<GroupConfigs, GroupConfigsValidator> { }
		#region CTOR
		public GroupConfigs() : base(GroupConfigsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupConfigs(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupConfigs.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(ConfigTree))
		    {
		        this.Children.Sort();
		    }
		}
		public static GroupConfigs Clone(GroupConfigs from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupConfigs vm = new GroupConfigs();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.Description = from.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.ConfigTree.Clone((ConfigTree)t, isDeep));
		    vm.RelativeConfigPath = from.RelativeConfigPath;
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(GroupConfigs to, GroupConfigs from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.Description = from.Description;
		    if (isDeep)
		    {
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.ConfigTree.Update((ConfigTree)t, (ConfigTree)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new ConfigTree();
		                vSharpStudio.vm.ViewModels.ConfigTree.Update(p, (ConfigTree)tt, isDeep);
		                to.Children.Add(p);
		            }
		        }
		    }
		    to.RelativeConfigPath = from.RelativeConfigPath;
		}
		#region IEditable
		public override GroupConfigs Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupConfigs.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupConfigs from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupConfigs.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_configs' to 'GroupConfigs'
		public static GroupConfigs ConvertToVM(proto_group_configs m, GroupConfigs vm = null)
		{
		    if (vm == null)
		        vm = new GroupConfigs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.ConfigTree.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.RelativeConfigPath = m.RelativeConfigPath;
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupConfigs' to 'proto_group_configs'
		public static proto_group_configs ConvertToProto(GroupConfigs vm)
		{
		    proto_group_configs m = new proto_group_configs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.ConfigTree.ConvertToProto((ConfigTree)t));
		    m.RelativeConfigPath = vm.RelativeConfigPath;
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
				(t as ConfigTree).AcceptConfigNode(visitor);
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public ConfigTree this[int index] { get { return (ConfigTree)this.Children[index]; } }
		public void Add(ConfigTree item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<ConfigTree> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(ConfigTree item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		public string RelativeConfigPath
		{ 
			set
			{
				if (_RelativeConfigPath != value)
				{
					OnRelativeConfigPathChanging();
					_RelativeConfigPath = value;
					OnRelativeConfigPathChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _RelativeConfigPath; }
		}
		private string _RelativeConfigPath = "";
		partial void OnRelativeConfigPathChanging();
		partial void OnRelativeConfigPathChanged();
		#endregion Properties
	}
	public partial class ConfigTree : ConfigObjectBase<ConfigTree, ConfigTree.ConfigTreeValidator>, IComparable<ConfigTree>, IAccept
	{
		public partial class ConfigTreeValidator : ValidatorBase<ConfigTree, ConfigTreeValidator> { }
		#region CTOR
		public ConfigTree() : base(ConfigTreeValidator.Validator)
		{
			this.ConfigNode = new Config(this);
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public ConfigTree(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(ConfigTree.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(ConfigTree))
		    {
		        this.Children.Sort();
		    }
		}
		public static ConfigTree Clone(ConfigTree from, bool isDeep = true, bool isNewGuid = false)
		{
		    ConfigTree vm = new ConfigTree();
		    vm.Guid = from.Guid;
		    vm.Name = from.Name;
		    vm.SortingValue = from.SortingValue;
		    vm.Description = from.Description;
		    if (isDeep)
		        vm.ConfigNode = vSharpStudio.vm.ViewModels.Config.Clone(from.ConfigNode, isDeep);
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.ConfigTree.Clone((ConfigTree)t, isDeep));
		    if (isNewGuid)
		        vm.SetNewGuid();
		    return vm;
		}
		public static void Update(ConfigTree to, ConfigTree from, bool isDeep = true)
		{
		    to.Guid = from.Guid;
		    to.Name = from.Name;
		    to.SortingValue = from.SortingValue;
		    to.Description = from.Description;
		    if (isDeep)
		        Config.Update(to.ConfigNode, from.ConfigNode, isDeep);
		    if (isDeep)
		    {
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.ConfigTree.Update((ConfigTree)t, (ConfigTree)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new ConfigTree();
		                vSharpStudio.vm.ViewModels.ConfigTree.Update(p, (ConfigTree)tt, isDeep);
		                to.Children.Add(p);
		            }
		        }
		    }
		}
		#region IEditable
		public override ConfigTree Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return ConfigTree.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(ConfigTree from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    ConfigTree.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_config_tree' to 'ConfigTree'
		public static ConfigTree ConvertToVM(proto_config_tree m, ConfigTree vm = null)
		{
		    if (vm == null)
		        vm = new ConfigTree();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.Config.ConvertToVM(m.ConfigNode, vm.ConfigNode);
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.ConfigTree.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'ConfigTree' to 'proto_config_tree'
		public static proto_config_tree ConvertToProto(ConfigTree vm)
		{
		    proto_config_tree m = new proto_config_tree();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.Description = vm.Description;
		    m.ConfigNode = vSharpStudio.vm.ViewModels.Config.ConvertToProto(vm.ConfigNode);
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.ConfigTree.ConvertToProto((ConfigTree)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			this.ConfigNode.AcceptConfigNode(visitor);
			foreach(var t in this.Children)
				(t as ConfigTree).AcceptConfigNode(visitor);
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
		partial void OnConfigNodeChanging();
		partial void OnConfigNodeChanged();
		[BrowsableAttribute(false)]
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public ConfigTree this[int index] { get { return (ConfigTree)this.Children[index]; } }
		public void Add(ConfigTree item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<ConfigTree> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(ConfigTree item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Configuration config
	///////////////////////////////////////////////////
	public partial class Config : ConfigObjectBase<Config, Config.ConfigValidator>, IComparable<Config>, IAccept
	{
		public partial class ConfigValidator : ValidatorBase<Config, ConfigValidator> { }
		#region CTOR
		public Config() : base(ConfigValidator.Validator)
		{
			this.DbIdGenerator = new DbIdGenerator();
			this.GroupPlugins = new GroupListPlugins(this);
			this.GroupConfigs = new GroupConfigs(this);
			this.GroupConstants = new GroupListConstants(this);
			this.GroupEnumerations = new GroupListEnumerations(this);
			this.GroupCatalogs = new GroupListCatalogs(this);
			this.GroupDocuments = new GroupDocuments(this);
			this.GroupJournals = new GroupListJournals(this);
			OnInit();
		}
		public Config(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Config.DefaultName, this, this.SubNodes);
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
		    vm.IsDbFromConnectionString = from.IsDbFromConnectionString;
		    vm.ConnectionStringName = from.ConnectionStringName;
		    vm.DbServer = from.DbServer;
		    vm.DbDatabaseName = from.DbDatabaseName;
		    vm.IsDbWindowsAuthentication = from.IsDbWindowsAuthentication;
		    vm.DbUser = from.DbUser;
		    vm.DbPassword = from.DbPassword;
		    vm.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
		    vm.DbSchema = from.DbSchema;
		    vm.PrimaryKeyName = from.PrimaryKeyName;
		    if (isDeep)
		        vm.DbIdGenerator = vSharpStudio.vm.ViewModels.DbIdGenerator.Clone(from.DbIdGenerator, isDeep);
		    if (isDeep)
		        vm.GroupPlugins = vSharpStudio.vm.ViewModels.GroupListPlugins.Clone(from.GroupPlugins, isDeep);
		    if (isDeep)
		        vm.GroupConfigs = vSharpStudio.vm.ViewModels.GroupConfigs.Clone(from.GroupConfigs, isDeep);
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
		    to.IsDbFromConnectionString = from.IsDbFromConnectionString;
		    to.ConnectionStringName = from.ConnectionStringName;
		    to.DbServer = from.DbServer;
		    to.DbDatabaseName = from.DbDatabaseName;
		    to.IsDbWindowsAuthentication = from.IsDbWindowsAuthentication;
		    to.DbUser = from.DbUser;
		    to.DbPassword = from.DbPassword;
		    to.PathToProjectWithConnectionString = from.PathToProjectWithConnectionString;
		    to.DbSchema = from.DbSchema;
		    to.PrimaryKeyName = from.PrimaryKeyName;
		    if (isDeep)
		        DbIdGenerator.Update(to.DbIdGenerator, from.DbIdGenerator, isDeep);
		    if (isDeep)
		        GroupListPlugins.Update(to.GroupPlugins, from.GroupPlugins, isDeep);
		    if (isDeep)
		        GroupConfigs.Update(to.GroupConfigs, from.GroupConfigs, isDeep);
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
		public static Config ConvertToVM(proto_config m, Config vm = null)
		{
		    if (vm == null)
		        vm = new Config();
		    vm.Guid = m.Guid;
		    vm.Version = m.Version;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.IsDbFromConnectionString = m.IsDbFromConnectionString;
		    vm.ConnectionStringName = m.ConnectionStringName;
		    vm.DbServer = m.DbServer;
		    vm.DbDatabaseName = m.DbDatabaseName;
		    vm.IsDbWindowsAuthentication = m.IsDbWindowsAuthentication;
		    vm.DbUser = m.DbUser;
		    vm.DbPassword = m.DbPassword;
		    vm.PathToProjectWithConnectionString = m.PathToProjectWithConnectionString;
		    vm.DbSchema = m.DbSchema;
		    vm.PrimaryKeyName = m.PrimaryKeyName;
		    vSharpStudio.vm.ViewModels.DbIdGenerator.ConvertToVM(m.DbIdGenerator, vm.DbIdGenerator);
		    vSharpStudio.vm.ViewModels.GroupListPlugins.ConvertToVM(m.GroupPlugins, vm.GroupPlugins);
		    vSharpStudio.vm.ViewModels.GroupConfigs.ConvertToVM(m.GroupConfigs, vm.GroupConfigs);
		    vSharpStudio.vm.ViewModels.GroupListConstants.ConvertToVM(m.GroupConstants, vm.GroupConstants);
		    vSharpStudio.vm.ViewModels.GroupListEnumerations.ConvertToVM(m.GroupEnumerations, vm.GroupEnumerations);
		    vSharpStudio.vm.ViewModels.GroupListCatalogs.ConvertToVM(m.GroupCatalogs, vm.GroupCatalogs);
		    vSharpStudio.vm.ViewModels.GroupDocuments.ConvertToVM(m.GroupDocuments, vm.GroupDocuments);
		    vSharpStudio.vm.ViewModels.GroupListJournals.ConvertToVM(m.GroupJournals, vm.GroupJournals);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Config' to 'proto_config'
		public static proto_config ConvertToProto(Config vm)
		{
		    proto_config m = new proto_config();
		    m.Guid = vm.Guid;
		    m.Version = vm.Version;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.IsDbFromConnectionString = vm.IsDbFromConnectionString;
		    m.ConnectionStringName = vm.ConnectionStringName;
		    m.DbServer = vm.DbServer;
		    m.DbDatabaseName = vm.DbDatabaseName;
		    m.IsDbWindowsAuthentication = vm.IsDbWindowsAuthentication;
		    m.DbUser = vm.DbUser;
		    m.DbPassword = vm.DbPassword;
		    m.PathToProjectWithConnectionString = vm.PathToProjectWithConnectionString;
		    m.DbSchema = vm.DbSchema;
		    m.PrimaryKeyName = vm.PrimaryKeyName;
		    m.DbIdGenerator = vSharpStudio.vm.ViewModels.DbIdGenerator.ConvertToProto(vm.DbIdGenerator);
		    m.GroupPlugins = vSharpStudio.vm.ViewModels.GroupListPlugins.ConvertToProto(vm.GroupPlugins);
		    m.GroupConfigs = vSharpStudio.vm.ViewModels.GroupConfigs.ConvertToProto(vm.GroupConfigs);
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
		public string DbServer
		{ 
			set
			{
				if (_DbServer != value)
				{
					OnDbServerChanging();
					_DbServer = value;
					OnDbServerChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbServer; }
		}
		private string _DbServer = "";
		partial void OnDbServerChanging();
		partial void OnDbServerChanged();
		public string DbDatabaseName
		{ 
			set
			{
				if (_DbDatabaseName != value)
				{
					OnDbDatabaseNameChanging();
					_DbDatabaseName = value;
					OnDbDatabaseNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbDatabaseName; }
		}
		private string _DbDatabaseName = "";
		partial void OnDbDatabaseNameChanging();
		partial void OnDbDatabaseNameChanged();
		public bool IsDbWindowsAuthentication
		{ 
			set
			{
				if (_IsDbWindowsAuthentication != value)
				{
					OnIsDbWindowsAuthenticationChanging();
					_IsDbWindowsAuthentication = value;
					OnIsDbWindowsAuthenticationChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _IsDbWindowsAuthentication; }
		}
		private bool _IsDbWindowsAuthentication;
		partial void OnIsDbWindowsAuthenticationChanging();
		partial void OnIsDbWindowsAuthenticationChanged();
		public string DbUser
		{ 
			set
			{
				if (_DbUser != value)
				{
					OnDbUserChanging();
					_DbUser = value;
					OnDbUserChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbUser; }
		}
		private string _DbUser = "";
		partial void OnDbUserChanging();
		partial void OnDbUserChanged();
		public string DbPassword
		{ 
			set
			{
				if (_DbPassword != value)
				{
					OnDbPasswordChanging();
					_DbPassword = value;
					OnDbPasswordChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbPassword; }
		}
		private string _DbPassword = "";
		partial void OnDbPasswordChanging();
		partial void OnDbPasswordChanged();
		
		///////////////////////////////////////////////////
		/// path to project with config file containing connection string. Usefull for UNIT tests.
		/// it will override previous settings
		///////////////////////////////////////////////////
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
		public string PrimaryKeyName
		{ 
			set
			{
				if (_PrimaryKeyName != value)
				{
					OnPrimaryKeyNameChanging();
					_PrimaryKeyName = value;
					OnPrimaryKeyNameChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _PrimaryKeyName; }
		}
		private string _PrimaryKeyName = "";
		partial void OnPrimaryKeyNameChanging();
		partial void OnPrimaryKeyNameChanged();
		[ExpandableObjectAttribute()]
		public DbIdGenerator DbIdGenerator
		{ 
			set
			{
				if (_DbIdGenerator != value)
				{
					OnDbIdGeneratorChanging();
		            _DbIdGenerator = value;
					OnDbIdGeneratorChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbIdGenerator; }
		}
		private DbIdGenerator _DbIdGenerator;
		partial void OnDbIdGeneratorChanging();
		partial void OnDbIdGeneratorChanged();
		
		///////////////////////////////////////////////////
		/// CONFIG OBJECTS
		///////////////////////////////////////////////////
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
		partial void OnGroupPluginsChanging();
		partial void OnGroupPluginsChanged();
		public GroupConfigs GroupConfigs
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
		private GroupConfigs _GroupConfigs;
		partial void OnGroupConfigsChanging();
		partial void OnGroupConfigsChanged();
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
		partial void OnGroupConstantsChanging();
		partial void OnGroupConstantsChanged();
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
		partial void OnGroupEnumerationsChanging();
		partial void OnGroupEnumerationsChanged();
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
		partial void OnGroupCatalogsChanging();
		partial void OnGroupCatalogsChanged();
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
		partial void OnGroupDocumentsChanging();
		partial void OnGroupDocumentsChanged();
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
		partial void OnGroupJournalsChanging();
		partial void OnGroupJournalsChanged();
		#endregion Properties
	}
	public partial class DataType : ViewModelValidatableWithSeverity<DataType, DataType.DataTypeValidator>
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
		public static DataType ConvertToVM(proto_data_type m, DataType vm = null)
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
		public static proto_data_type ConvertToProto(DataType vm)
		{
		    proto_data_type m = new proto_data_type();
		    m.DataTypeEnum = (proto_enum_data_type)vm.DataTypeEnum;
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
	public partial class GroupListPropertiesTabs : ConfigObjectBase<GroupListPropertiesTabs, GroupListPropertiesTabs.GroupListPropertiesTabsValidator>, IComparable<GroupListPropertiesTabs>, IAccept
	{
		public partial class GroupListPropertiesTabsValidator : ValidatorBase<GroupListPropertiesTabs, GroupListPropertiesTabsValidator> { }
		#region CTOR
		public GroupListPropertiesTabs() : base(GroupListPropertiesTabsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListPropertiesTabs(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListPropertiesTabs.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(GroupPropertiesTab))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTab.Clone((GroupPropertiesTab)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.GroupPropertiesTab.Update((GroupPropertiesTab)t, (GroupPropertiesTab)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    break;
		                }
		            }
		            if (!isfound)
		            {
		                var p = new GroupPropertiesTab();
		                vSharpStudio.vm.ViewModels.GroupPropertiesTab.Update(p, (GroupPropertiesTab)tt, isDeep);
		                to.Children.Add(p);
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
		public static GroupListPropertiesTabs ConvertToVM(proto_group_list_properties_tabs m, GroupListPropertiesTabs vm = null)
		{
		    if (vm == null)
		        vm = new GroupListPropertiesTabs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.GroupPropertiesTab.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListPropertiesTabs' to 'proto_group_list_properties_tabs'
		public static proto_group_list_properties_tabs ConvertToProto(GroupListPropertiesTabs vm)
		{
		    proto_group_list_properties_tabs m = new proto_group_list_properties_tabs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.GroupPropertiesTab.ConvertToProto((GroupPropertiesTab)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
				(t as GroupPropertiesTab).AcceptConfigNode(visitor);
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public GroupPropertiesTab this[int index] { get { return (GroupPropertiesTab)this.Children[index]; } }
		public void Add(GroupPropertiesTab item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<GroupPropertiesTab> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(GroupPropertiesTab item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class GroupPropertiesTab : ConfigObjectBase<GroupPropertiesTab, GroupPropertiesTab.GroupPropertiesTabValidator>, IComparable<GroupPropertiesTab>, IAccept
	{
		public partial class GroupPropertiesTabValidator : ValidatorBase<GroupPropertiesTab, GroupPropertiesTabValidator> { }
		#region CTOR
		public GroupPropertiesTab() : base(GroupPropertiesTabValidator.Validator)
		{
			this.GroupProperties = new GroupListProperties(this);
			this.GroupPropertiesSubtabs = new GroupListPropertiesTabs(this);
			OnInit();
		}
		public GroupPropertiesTab(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupPropertiesTab.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    //throw new Exception();
		}
		public static GroupPropertiesTab Clone(GroupPropertiesTab from, bool isDeep = true, bool isNewGuid = false)
		{
		    GroupPropertiesTab vm = new GroupPropertiesTab();
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
		public static void Update(GroupPropertiesTab to, GroupPropertiesTab from, bool isDeep = true)
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
		public override GroupPropertiesTab Backup()
		{
		    bool isDeep = true;
		    OnBackupObjectStarting(ref isDeep);
			return GroupPropertiesTab.Clone(this);
		}
		partial void OnBackupObjectStarting(ref bool isDeep);
		public override void Restore(GroupPropertiesTab from)
		{
		    bool isDeep = true;
		    OnRestoreObjectStarting(ref isDeep);
		    GroupPropertiesTab.Update(this, from, isDeep);
		}
		partial void OnRestoreObjectStarting(ref bool isDeep);
		#endregion IEditable
		// Conversion from 'proto_group_properties_tab' to 'GroupPropertiesTab'
		public static GroupPropertiesTab ConvertToVM(proto_group_properties_tab m, GroupPropertiesTab vm = null)
		{
		    if (vm == null)
		        vm = new GroupPropertiesTab();
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
		// Conversion from 'GroupPropertiesTab' to 'proto_group_properties_tab'
		public static proto_group_properties_tab ConvertToProto(GroupPropertiesTab vm)
		{
		    proto_group_properties_tab m = new proto_group_properties_tab();
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
		partial void OnGroupPropertiesSubtabsChanging();
		partial void OnGroupPropertiesSubtabsChanged();
		#endregion Properties
	}
	public partial class GroupListProperties : ConfigObjectBase<GroupListProperties, GroupListProperties.GroupListPropertiesValidator>, IComparable<GroupListProperties>, IAccept
	{
		public partial class GroupListPropertiesValidator : ValidatorBase<GroupListProperties, GroupListPropertiesValidator> { }
		#region CTOR
		public GroupListProperties() : base(GroupListPropertiesValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListProperties(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListProperties.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Property))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Property.Clone((Property)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Property.Update((Property)t, (Property)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListProperties ConvertToVM(proto_group_list_properties m, GroupListProperties vm = null)
		{
		    if (vm == null)
		        vm = new GroupListProperties();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Property.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListProperties' to 'proto_group_list_properties'
		public static proto_group_list_properties ConvertToProto(GroupListProperties vm)
		{
		    proto_group_list_properties m = new proto_group_list_properties();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Property.ConvertToProto((Property)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Property this[int index] { get { return (Property)this.Children[index]; } }
		public void Add(Property item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Property> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Property item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class Property : ConfigObjectBase<Property, Property.PropertyValidator>, IComparable<Property>, IAccept
	{
		public partial class PropertyValidator : ValidatorBase<Property, PropertyValidator> { }
		#region CTOR
		public Property() : base(PropertyValidator.Validator)
		{
			this.DataType = new DataType();
			OnInit();
		}
		public Property(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Property.DefaultName, this, this.SubNodes);
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
		public static Property ConvertToVM(proto_property m, Property vm = null)
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
		public static proto_property ConvertToProto(Property vm)
		{
		    proto_property m = new proto_property();
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
		partial void OnDataTypeChanging();
		partial void OnDataTypeChanged();
		#endregion Properties
	}
	public partial class GroupListConstants : ConfigObjectBase<GroupListConstants, GroupListConstants.GroupListConstantsValidator>, IComparable<GroupListConstants>, IAccept
	{
		public partial class GroupListConstantsValidator : ValidatorBase<GroupListConstants, GroupListConstantsValidator> { }
		#region CTOR
		public GroupListConstants() : base(GroupListConstantsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListConstants(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListConstants.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Constant))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Constant.Clone((Constant)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Constant.Update((Constant)t, (Constant)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListConstants ConvertToVM(proto_group_list_constants m, GroupListConstants vm = null)
		{
		    if (vm == null)
		        vm = new GroupListConstants();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Constant.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListConstants' to 'proto_group_list_constants'
		public static proto_group_list_constants ConvertToProto(GroupListConstants vm)
		{
		    proto_group_list_constants m = new proto_group_list_constants();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Constant.ConvertToProto((Constant)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Constant this[int index] { get { return (Constant)this.Children[index]; } }
		public void Add(Constant item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Constant> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Constant item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	
	///////////////////////////////////////////////////
	/// Constant application wise value
	///////////////////////////////////////////////////
	public partial class Constant : ConfigObjectBase<Constant, Constant.ConstantValidator>, IComparable<Constant>, IAccept
	{
		public partial class ConstantValidator : ValidatorBase<Constant, ConstantValidator> { }
		#region CTOR
		public Constant() : base(ConstantValidator.Validator)
		{
			this.DataType = new DataType();
			OnInit();
		}
		public Constant(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Constant.DefaultName, this, this.SubNodes);
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
		public static Constant ConvertToVM(proto_constant m, Constant vm = null)
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
		public static proto_constant ConvertToProto(Constant vm)
		{
		    proto_constant m = new proto_constant();
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
		partial void OnDataTypeChanging();
		partial void OnDataTypeChanged();
		#endregion Properties
	}
	public partial class GroupListEnumerations : ConfigObjectBase<GroupListEnumerations, GroupListEnumerations.GroupListEnumerationsValidator>, IComparable<GroupListEnumerations>, IAccept
	{
		public partial class GroupListEnumerationsValidator : ValidatorBase<GroupListEnumerations, GroupListEnumerationsValidator> { }
		#region CTOR
		public GroupListEnumerations() : base(GroupListEnumerationsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListEnumerations(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListEnumerations.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Enumeration))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Enumeration.Clone((Enumeration)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Enumeration.Update((Enumeration)t, (Enumeration)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListEnumerations ConvertToVM(proto_group_list_enumerations m, GroupListEnumerations vm = null)
		{
		    if (vm == null)
		        vm = new GroupListEnumerations();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Enumeration.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListEnumerations' to 'proto_group_list_enumerations'
		public static proto_group_list_enumerations ConvertToProto(GroupListEnumerations vm)
		{
		    proto_group_list_enumerations m = new proto_group_list_enumerations();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Enumeration.ConvertToProto((Enumeration)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Enumeration this[int index] { get { return (Enumeration)this.Children[index]; } }
		public void Add(Enumeration item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Enumeration> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Enumeration item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class Enumeration : ConfigObjectBase<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>, IAccept
	{
		public partial class EnumerationValidator : ValidatorBase<Enumeration, EnumerationValidator> { }
		#region CTOR
		public Enumeration() : base(EnumerationValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public Enumeration(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Enumeration.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(EnumerationPair))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.EnumerationPair.Clone((EnumerationPair)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.EnumerationPair.Update((EnumerationPair)t, (EnumerationPair)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static Enumeration ConvertToVM(proto_enumeration m, Enumeration vm = null)
		{
		    if (vm == null)
		        vm = new Enumeration();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.DataTypeEnum = (EnumEnumerationType)m.DataTypeEnum;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.EnumerationPair.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Enumeration' to 'proto_enumeration'
		public static proto_enumeration ConvertToProto(Enumeration vm)
		{
		    proto_enumeration m = new proto_enumeration();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DataTypeEnum = (enum_enumeration_type)vm.DataTypeEnum;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.EnumerationPair.ConvertToProto((EnumerationPair)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public EnumerationPair this[int index] { get { return (EnumerationPair)this.Children[index]; } }
		public void Add(EnumerationPair item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<EnumerationPair> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(EnumerationPair item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class EnumerationPair : ConfigObjectBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>, IAccept
	{
		public partial class EnumerationPairValidator : ValidatorBase<EnumerationPair, EnumerationPairValidator> { }
		#region CTOR
		public EnumerationPair() : base(EnumerationPairValidator.Validator)
		{
			OnInit();
		}
		public EnumerationPair(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(EnumerationPair.DefaultName, this, this.SubNodes);
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
		public static EnumerationPair ConvertToVM(proto_enumeration_pair m, EnumerationPair vm = null)
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
		public static proto_enumeration_pair ConvertToProto(EnumerationPair vm)
		{
		    proto_enumeration_pair m = new proto_enumeration_pair();
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
	public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>, IComparable<Catalog>, IAccept
	{
		public partial class CatalogValidator : ValidatorBase<Catalog, CatalogValidator> { }
		#region CTOR
		public Catalog() : base(CatalogValidator.Validator)
		{
			this.DbIdGenerator = new DbIdGenerator();
			this.GroupProperties = new GroupListProperties(this);
			this.GroupForms = new GroupListForms(this);
			this.GroupReports = new GroupListReports(this);
			OnInit();
		}
		public Catalog(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Catalog.DefaultName, this, this.SubNodes);
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
		        vm.DbIdGenerator = vSharpStudio.vm.ViewModels.DbIdGenerator.Clone(from.DbIdGenerator, isDeep);
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
		        DbIdGenerator.Update(to.DbIdGenerator, from.DbIdGenerator, isDeep);
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
		public static Catalog ConvertToVM(proto_catalog m, Catalog vm = null)
		{
		    if (vm == null)
		        vm = new Catalog();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vSharpStudio.vm.ViewModels.DbIdGenerator.ConvertToVM(m.DbIdGenerator, vm.DbIdGenerator);
		    vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToVM(m.GroupProperties, vm.GroupProperties);
		    vSharpStudio.vm.ViewModels.GroupListForms.ConvertToVM(m.GroupForms, vm.GroupForms);
		    vSharpStudio.vm.ViewModels.GroupListReports.ConvertToVM(m.GroupReports, vm.GroupReports);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Catalog' to 'proto_catalog'
		public static proto_catalog ConvertToProto(Catalog vm)
		{
		    proto_catalog m = new proto_catalog();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.DbIdGenerator = vSharpStudio.vm.ViewModels.DbIdGenerator.ConvertToProto(vm.DbIdGenerator);
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
		[ExpandableObjectAttribute()]
		public DbIdGenerator DbIdGenerator
		{ 
			set
			{
				if (_DbIdGenerator != value)
				{
					OnDbIdGeneratorChanging();
		            _DbIdGenerator = value;
					OnDbIdGeneratorChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbIdGenerator; }
		}
		private DbIdGenerator _DbIdGenerator;
		partial void OnDbIdGeneratorChanging();
		partial void OnDbIdGeneratorChanged();
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
		partial void OnGroupReportsChanging();
		partial void OnGroupReportsChanged();
		#endregion Properties
	}
	public partial class GroupListCatalogs : ConfigObjectBase<GroupListCatalogs, GroupListCatalogs.GroupListCatalogsValidator>, IComparable<GroupListCatalogs>, IAccept
	{
		public partial class GroupListCatalogsValidator : ValidatorBase<GroupListCatalogs, GroupListCatalogsValidator> { }
		#region CTOR
		public GroupListCatalogs() : base(GroupListCatalogsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListCatalogs(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListCatalogs.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Catalog))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Catalog.Clone((Catalog)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Catalog.Update((Catalog)t, (Catalog)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListCatalogs ConvertToVM(proto_group_list_catalogs m, GroupListCatalogs vm = null)
		{
		    if (vm == null)
		        vm = new GroupListCatalogs();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Catalog.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListCatalogs' to 'proto_group_list_catalogs'
		public static proto_group_list_catalogs ConvertToProto(GroupListCatalogs vm)
		{
		    proto_group_list_catalogs m = new proto_group_list_catalogs();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Catalog.ConvertToProto((Catalog)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Catalog this[int index] { get { return (Catalog)this.Children[index]; } }
		public void Add(Catalog item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Catalog> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Catalog item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class GroupDocuments : ConfigObjectBase<GroupDocuments, GroupDocuments.GroupDocumentsValidator>, IComparable<GroupDocuments>, IAccept
	{
		public partial class GroupDocumentsValidator : ValidatorBase<GroupDocuments, GroupDocumentsValidator> { }
		#region CTOR
		public GroupDocuments() : base(GroupDocumentsValidator.Validator)
		{
			this.GroupSharedProperties = new GroupListProperties(this);
			this.GroupListDocuments = new GroupListDocuments(this);
			OnInit();
		}
		public GroupDocuments(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupDocuments.DefaultName, this, this.SubNodes);
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
		public static GroupDocuments ConvertToVM(proto_group_documents m, GroupDocuments vm = null)
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
		public static proto_group_documents ConvertToProto(GroupDocuments vm)
		{
		    proto_group_documents m = new proto_group_documents();
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
		partial void OnGroupListDocumentsChanging();
		partial void OnGroupListDocumentsChanged();
		#endregion Properties
	}
	public partial class Document : ConfigObjectBase<Document, Document.DocumentValidator>, IComparable<Document>, IAccept
	{
		public partial class DocumentValidator : ValidatorBase<Document, DocumentValidator> { }
		#region CTOR
		public Document() : base(DocumentValidator.Validator)
		{
			this.GroupProperties = new GroupListProperties(this);
			this.GroupPropertiesTabs = new GroupListPropertiesTabs(this);
			this.GroupForms = new GroupListForms(this);
			this.GroupReports = new GroupListReports(this);
			this.DbIdGenerator = new DbIdGenerator();
			OnInit();
		}
		public Document(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Document.DefaultName, this, this.SubNodes);
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
		    if (isDeep)
		        vm.DbIdGenerator = vSharpStudio.vm.ViewModels.DbIdGenerator.Clone(from.DbIdGenerator, isDeep);
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
		    if (isDeep)
		        DbIdGenerator.Update(to.DbIdGenerator, from.DbIdGenerator, isDeep);
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
		public static Document ConvertToVM(proto_document m, Document vm = null)
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
		    vSharpStudio.vm.ViewModels.DbIdGenerator.ConvertToVM(m.DbIdGenerator, vm.DbIdGenerator);
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Document' to 'proto_document'
		public static proto_document ConvertToProto(Document vm)
		{
		    proto_document m = new proto_document();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    m.GroupProperties = vSharpStudio.vm.ViewModels.GroupListProperties.ConvertToProto(vm.GroupProperties);
		    m.GroupPropertiesTabs = vSharpStudio.vm.ViewModels.GroupListPropertiesTabs.ConvertToProto(vm.GroupPropertiesTabs);
		    m.GroupForms = vSharpStudio.vm.ViewModels.GroupListForms.ConvertToProto(vm.GroupForms);
		    m.GroupReports = vSharpStudio.vm.ViewModels.GroupListReports.ConvertToProto(vm.GroupReports);
		    m.DbIdGenerator = vSharpStudio.vm.ViewModels.DbIdGenerator.ConvertToProto(vm.DbIdGenerator);
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
		partial void OnGroupReportsChanging();
		partial void OnGroupReportsChanged();
		[ExpandableObjectAttribute()]
		public DbIdGenerator DbIdGenerator
		{ 
			set
			{
				if (_DbIdGenerator != value)
				{
					OnDbIdGeneratorChanging();
		            _DbIdGenerator = value;
					OnDbIdGeneratorChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _DbIdGenerator; }
		}
		private DbIdGenerator _DbIdGenerator;
		partial void OnDbIdGeneratorChanging();
		partial void OnDbIdGeneratorChanged();
		#endregion Properties
	}
	public partial class GroupListDocuments : ConfigObjectBase<GroupListDocuments, GroupListDocuments.GroupListDocumentsValidator>, IComparable<GroupListDocuments>, IAccept
	{
		public partial class GroupListDocumentsValidator : ValidatorBase<GroupListDocuments, GroupListDocumentsValidator> { }
		#region CTOR
		public GroupListDocuments() : base(GroupListDocumentsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListDocuments(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListDocuments.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Document))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Document.Clone((Document)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Document.Update((Document)t, (Document)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListDocuments ConvertToVM(proto_group_list_documents m, GroupListDocuments vm = null)
		{
		    if (vm == null)
		        vm = new GroupListDocuments();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Document.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListDocuments' to 'proto_group_list_documents'
		public static proto_group_list_documents ConvertToProto(GroupListDocuments vm)
		{
		    proto_group_list_documents m = new proto_group_list_documents();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Document.ConvertToProto((Document)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Document this[int index] { get { return (Document)this.Children[index]; } }
		public void Add(Document item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Document> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Document item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class GroupListJournals : ConfigObjectBase<GroupListJournals, GroupListJournals.GroupListJournalsValidator>, IComparable<GroupListJournals>, IAccept
	{
		public partial class GroupListJournalsValidator : ValidatorBase<GroupListJournals, GroupListJournalsValidator> { }
		#region CTOR
		public GroupListJournals() : base(GroupListJournalsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListJournals(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListJournals.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Journal))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Journal.Clone((Journal)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Journal.Update((Journal)t, (Journal)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListJournals ConvertToVM(proto_group_list_journals m, GroupListJournals vm = null)
		{
		    if (vm == null)
		        vm = new GroupListJournals();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Journal.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListJournals' to 'proto_group_list_journals'
		public static proto_group_list_journals ConvertToProto(GroupListJournals vm)
		{
		    proto_group_list_journals m = new proto_group_list_journals();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Journal.ConvertToProto((Journal)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Journal this[int index] { get { return (Journal)this.Children[index]; } }
		public void Add(Journal item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Journal> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Journal item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class Journal : ConfigObjectBase<Journal, Journal.JournalValidator>, IComparable<Journal>, IAccept
	{
		public partial class JournalValidator : ValidatorBase<Journal, JournalValidator> { }
		#region CTOR
		public Journal() : base(JournalValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public Journal(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Journal.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Document))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Document.Clone((Document)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Document.Update((Document)t, (Document)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static Journal ConvertToVM(proto_journal m, Journal vm = null)
		{
		    if (vm == null)
		        vm = new Journal();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Document.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'Journal' to 'proto_journal'
		public static proto_journal ConvertToProto(Journal vm)
		{
		    proto_journal m = new proto_journal();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Document.ConvertToProto((Document)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Document this[int index] { get { return (Document)this.Children[index]; } }
		public void Add(Document item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Document> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Document item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class GroupListForms : ConfigObjectBase<GroupListForms, GroupListForms.GroupListFormsValidator>, IComparable<GroupListForms>, IAccept
	{
		public partial class GroupListFormsValidator : ValidatorBase<GroupListForms, GroupListFormsValidator> { }
		#region CTOR
		public GroupListForms() : base(GroupListFormsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListForms(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListForms.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Form))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Form.Clone((Form)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Form.Update((Form)t, (Form)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListForms ConvertToVM(proto_group_list_forms m, GroupListForms vm = null)
		{
		    if (vm == null)
		        vm = new GroupListForms();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Form.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListForms' to 'proto_group_list_forms'
		public static proto_group_list_forms ConvertToProto(GroupListForms vm)
		{
		    proto_group_list_forms m = new proto_group_list_forms();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Form.ConvertToProto((Form)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Form this[int index] { get { return (Form)this.Children[index]; } }
		public void Add(Form item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Form> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Form item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class Form : ConfigObjectBase<Form, Form.FormValidator>, IComparable<Form>, IAccept
	{
		public partial class FormValidator : ValidatorBase<Form, FormValidator> { }
		#region CTOR
		public Form() : base(FormValidator.Validator)
		{
			OnInit();
		}
		public Form(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Form.DefaultName, this, this.SubNodes);
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
		public static Form ConvertToVM(proto_form m, Form vm = null)
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
		public static proto_form ConvertToProto(Form vm)
		{
		    proto_form m = new proto_form();
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
	public partial class GroupListReports : ConfigObjectBase<GroupListReports, GroupListReports.GroupListReportsValidator>, IComparable<GroupListReports>, IAccept
	{
		public partial class GroupListReportsValidator : ValidatorBase<GroupListReports, GroupListReportsValidator> { }
		#region CTOR
		public GroupListReports() : base(GroupListReportsValidator.Validator)
		{
			this.Children = new SortedObservableCollection<ITreeConfigNode>();
			OnInit();
		}
		public GroupListReports(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(GroupListReports.DefaultName, this, this.SubNodes);
	    }
		partial void OnInit();
		#endregion CTOR
		#region Procedures
		public override void Sort(Type type)
		{
		    if (type == typeof(Report))
		    {
		        this.Children.Sort();
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
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in from.Children)
		        vm.Children.Add(vSharpStudio.vm.ViewModels.Report.Clone((Report)t, isDeep));
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
		        foreach(var t in to.Children.ToList())
		        {
		            bool isfound = false;
		            foreach(var tt in from.Children)
		            {
		                if (t == tt)
		                {
		                    isfound = true;
		                    vSharpStudio.vm.ViewModels.Report.Update((Report)t, (Report)tt, isDeep);
		                    break;
		                }
		            }
		            if (!isfound)
		                to.Children.Remove(t);
		        }
		        foreach(var tt in from.Children)
		        {
		            bool isfound = false;
		            foreach(var t in to.Children.ToList())
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
		                to.Children.Add(p);
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
		public static GroupListReports ConvertToVM(proto_group_list_reports m, GroupListReports vm = null)
		{
		    if (vm == null)
		        vm = new GroupListReports();
		    vm.Guid = m.Guid;
		    vm.Name = m.Name;
		    vm.SortingValue = m.SortingValue;
		    vm.NameUi = m.NameUi;
		    vm.Description = m.Description;
		    vm.Children = new SortedObservableCollection<ITreeConfigNode>();
		    foreach(var t in m.Children)
		    {
		        var tvm = vSharpStudio.vm.ViewModels.Report.ConvertToVM(t);
		        tvm.Parent = vm;
		        vm.Children.Add(tvm);
		    }
		    vm.OnInitFromDto();
		    return vm;
		}
		// Conversion from 'GroupListReports' to 'proto_group_list_reports'
		public static proto_group_list_reports ConvertToProto(GroupListReports vm)
		{
		    proto_group_list_reports m = new proto_group_list_reports();
		    m.Guid = vm.Guid;
		    m.Name = vm.Name;
		    m.SortingValue = vm.SortingValue;
		    m.NameUi = vm.NameUi;
		    m.Description = vm.Description;
		    foreach(var t in vm.Children)
		        m.Children.Add(vSharpStudio.vm.ViewModels.Report.ConvertToProto((Report)t));
		    return m;
		}
		public void AcceptConfigNode(IVisitorConfigNode visitor) 
		{
		    if (visitor.Token.IsCancellationRequested)
		        return;
			visitor.Visit(this);
			foreach(var t in this.Children)
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
		public SortedObservableCollection<ITreeConfigNode> Children
		{ 
			set
			{
				if (_Children != value)
				{
					OnChildrenChanging();
					_Children = value;
					OnChildrenChanged();
					NotifyPropertyChanged();
					ValidateProperty();
				}
			}
			get { return _Children; }
		}
		private SortedObservableCollection<ITreeConfigNode> _Children;
		public Report this[int index] { get { return (Report)this.Children[index]; } }
		public void Add(Report item) 
		{ 
		    this.Children.Add(item); 
		    item.Parent = this;
		}
		public void AddRange(IEnumerable<Report> items) 
		{ 
		    this.Children.AddRange(items); 
		    foreach(var t in items)
		        t.Parent = this;
		}
		public int Count() 
		{ 
		    return this.Children.Count; 
		}
		public void Remove(Report item) 
		{
		    this.Children.Remove(item); 
		    item.Parent = null;
		}
		partial void OnChildrenChanging();
		partial void OnChildrenChanged();
		#endregion Properties
	}
	public partial class Report : ConfigObjectBase<Report, Report.ReportValidator>, IComparable<Report>, IAccept
	{
		public partial class ReportValidator : ValidatorBase<Report, ReportValidator> { }
		#region CTOR
		public Report() : base(ReportValidator.Validator)
		{
			OnInit();
		}
		public Report(ITreeConfigNode parent) : this()
	    {
	        this.Parent = parent;
	        //GetUniqueName(Report.DefaultName, this, this.SubNodes);
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
		public static Report ConvertToVM(proto_report m, Report vm = null)
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
		public static proto_report ConvertToProto(Report vm)
		{
		    proto_report m = new proto_report();
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
		void Visit(GroupConfigs p);
		void VisitEnd(GroupConfigs p);
		void Visit(ConfigTree p);
		void VisitEnd(ConfigTree p);
		void Visit(Config p);
		void VisitEnd(Config p);
		void Visit(GroupListPropertiesTabs p);
		void VisitEnd(GroupListPropertiesTabs p);
		void Visit(GroupPropertiesTab p);
		void VisitEnd(GroupPropertiesTab p);
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
		void Visit(proto_group_list_plugins p);
		void Visit(proto_plugin p);
		void Visit(proto_plugin_generator p);
		void Visit(proto_plugin_generator_settings p);
		void Visit(proto_settings_config p);
		void Visit(db_id_generator p);
		void Visit(proto_group_configs p);
		void Visit(proto_config_tree p);
		void Visit(proto_config p);
		void Visit(proto_data_type p);
		void Visit(proto_group_list_properties_tabs p);
		void Visit(proto_group_properties_tab p);
		void Visit(proto_group_list_properties p);
		void Visit(proto_property p);
		void Visit(proto_group_list_constants p);
		void Visit(proto_constant p);
		void Visit(proto_group_list_enumerations p);
		void Visit(proto_enumeration p);
		void Visit(proto_enumeration_pair p);
		void Visit(proto_catalog p);
		void Visit(proto_group_list_catalogs p);
		void Visit(proto_group_documents p);
		void Visit(proto_document p);
		void Visit(proto_group_list_documents p);
		void Visit(proto_group_list_journals p);
		void Visit(proto_journal p);
		void Visit(proto_group_list_forms p);
		void Visit(proto_form p);
		void Visit(proto_group_list_reports p);
		void Visit(proto_report p);
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
		public void Visit(DbIdGenerator p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(DbIdGenerator p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(GroupConfigs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupConfigs p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(ConfigTree p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ConfigTree p)
	    {
	        OnVisitEnd(p);
	    }
		public void Visit(Config p)
	    {
	        OnVisit(p);
	        ValidateSubAndCollectErrors(p, p.DbIdGenerator);
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
		public void Visit(GroupPropertiesTab p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupPropertiesTab p)
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
	        ValidateSubAndCollectErrors(p, p.DbIdGenerator);
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
	        ValidateSubAndCollectErrors(p, p.DbIdGenerator);
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
		public void Visit(DbIdGenerator p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(DbIdGenerator p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(DbIdGenerator p) {}
	    protected virtual void OnVisitEnd(DbIdGenerator p) {}
		public void Visit(GroupConfigs p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupConfigs p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupConfigs p) {}
	    protected virtual void OnVisitEnd(GroupConfigs p) {}
		public void Visit(ConfigTree p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(ConfigTree p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(ConfigTree p) {}
	    protected virtual void OnVisitEnd(ConfigTree p) {}
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
		public void Visit(GroupPropertiesTab p)
	    {
	        OnVisit(p);
	    }
		public void VisitEnd(GroupPropertiesTab p)
	    {
	        OnVisitEnd(p);
	    }
	    protected virtual void OnVisit(GroupPropertiesTab p) {}
	    protected virtual void OnVisitEnd(GroupPropertiesTab p) {}
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