using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListListCommon.Count,nq}")]
    public partial class GroupListCommon : ITreeModel, ICanGoRight, INodeGenSettings, INewAndDeleteion, IEditableNodeGroup
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as ConfigModel;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        partial void OnInit()
        {
            this.Name = "Common";
            this.IsEditable = false;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);

            // this.GroupPlugins.Parent = this;
            // this.Children.Add(this.GroupPlugins, 0);
        }

        public bool GetIsHasMarkedForDeletion()
        {
            if (this.GroupRoles.IsHasMarkedForDeletion || this.GroupRoles.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            if (this.GroupViewForms.IsHasMarkedForDeletion || this.GroupViewForms.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            this.IsHasMarkedForDeletion = false;
            return false;
        }

        public bool GetIsHasNew()
        {
            if (this.GroupRoles.IsHasNew || this.GroupRoles.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            if (this.GroupViewForms.IsHasNew || this.GroupViewForms.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            this.IsHasNew = false;
            return false;
        }
    }
}
