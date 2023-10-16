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
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Plugin : ICanGoLeft, ICanGoRight, ICanAddNode
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListGenerators.Count}";
        }
        [Browsable(false)]
        public GroupListPlugins ParentGroupListPlugins { get { Debug.Assert(this.Parent != null); return (GroupListPlugins)this.Parent; } }
        [Browsable(false)]
        public IGroupListPlugins ParentGroupListPluginsI { get { Debug.Assert(this.Parent != null); return (IGroupListPlugins)this.Parent; } }
        public new ConfigNodesCollection<PluginGenerator> Children { get { return this.ListGenerators; } }
        public Plugin(ITreeConfigNode parent, IvPlugin plugin)
            : this(parent)
        {
            Debug.Assert(plugin != null);
            this._Guid = plugin.Guid.ToString();
            this._Name = plugin.Name;
            this._Description = plugin.Description;
            this._Version = plugin.Version;
            this.VPlugin = plugin;
            this.IsEditable = false;
        }

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            //    Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListMainViewForms.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListMainViewForms.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListMainViewForms.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListMainViewForms.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}

        [Browsable(false)]
        public IvPlugin? VPlugin { get; private set; }

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
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }
    }
}
