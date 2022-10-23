using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Plugin:{Name,nq} Count:{ListGenerators.Count,nq}")]
    public partial class Plugin : ICanGoLeft, ICanGoRight
    {
        [BrowsableAttribute(false)]
        public GroupListPlugins ParentGroupListPlugins { get { return (GroupListPlugins)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListPlugins ParentGroupListPluginsI { get { return (IGroupListPlugins)this.Parent; } }
        public ConfigNodesCollection<PluginGenerator> Children { get { return this.ListGenerators; } }
        public Plugin(ITreeConfigNode parent, IvPlugin plugin)
            : this(parent)
        {
            Debug.Assert(plugin != null);
            this.Guid = plugin.Guid.ToString();
            this._Name = plugin.Name;
            this.Description = plugin.Description;
            this.VPlugin = plugin;
            this.IsEditable = false;
        }

        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
        }

        [BrowsableAttribute(false)]
        public IvPlugin VPlugin { get; private set; }

        public void SetVPlugin(IvPlugin plugin)
        {
            this.VPlugin = plugin;
        }

        #region IConfigObject
        // public void Create()
        // {
        //    GroupListConstants vm = (GroupListConstants)this.Parent;
        //    int icurr = vm.Children.IndexOf(this);
        //    vm.Children.Add(new Constant() { Parent = this.Parent });
        // }
        #endregion IConfigObject
    }
}
