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
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as Config;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Plugin> Children { get { return this.ListPlugins; } }

        partial void OnInit()
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
        }
        public bool GetIsHasMarkedForDeletion()
        {
            return false;
        }
        public bool GetIsHasNew()
        {
            return false;
        }
    }
}
