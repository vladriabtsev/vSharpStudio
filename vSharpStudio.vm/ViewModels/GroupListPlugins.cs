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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListPlugins.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListPlugins : ITreeModel, ICanGoRight, IEditableNodeGroup
    {
        [Browsable(false)]
        public Config ParentConfig { get { Debug.Assert(this.Parent != null); return (Config)this.Parent; } }
        [Browsable(false)]
        public IConfig ParentConfigI { get { Debug.Assert(this.Parent != null); return (IConfig)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentConfig.Children;
        }
        public new ConfigNodesCollection<Plugin> Children { get { return this.ListPlugins; } }
        #endregion ITree

        partial void OnCreated()
        {
            this._Name = "Plugins";
            this.IsEditable = false;
            this.ListPlugins.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
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
            var lst = new List<string>
            {
                //lst.Add(this.GetPropertyName(() => this.Description));
                this.GetPropertyName(() => this.Guid),
                //lst.Add(this.GetPropertyName(() => this.NameUi));
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }
    }
}
