using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListPlugins.Count,nq}")]
    public partial class GroupListPlugins : ITreeModel, ICanGoRight, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public bool IsNew { get { return false; } }
        [BrowsableAttribute(false)]
        public Config ParentConfig { get { return (Config)this.Parent; } }
        [BrowsableAttribute(false)]
        public IConfig ParentConfigI { get { return (IConfig)this.Parent; } }
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentConfig.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Plugin> Children { get { return this.ListPlugins; } }

        partial void OnCreated()
        {
            this._Name = "Plugins";
            this.IsEditable = false;
            //this.ListPlugins.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListPlugins.OnAddedAction = (t) =>
            //{
            //};
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        public bool GetIsHasMarkedForDeletion()
        {
            return false;
        }
        public bool GetIsHasNew()
        {
            return false;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            //lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            //lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
