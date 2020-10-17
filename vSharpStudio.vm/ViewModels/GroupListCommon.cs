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
    public partial class GroupListCommon : ITreeModel, ICanGoRight, INodeGenSettings, INewAndDeleteion
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.Children;
        }

        public override bool HasChildren(object parent)
        {
            return this.Children.Count > 0;
        }

        partial void OnInit()
        {
            this.Name = "Common";
            this.IsEditable = false;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);

            // this.GroupPlugins.Parent = this;
            // this.Children.Add(this.GroupPlugins, 0);
        }

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        public bool IsNew { get { return false; } set { } }
        public bool IsMarkedForDeletion { get { return false; } set { } }
        partial void OnIsHasMarkedForDeletionChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsHasMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsHasNewChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsHasNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
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
        public void RemoveMarkedForDeletionIfNewObjects()
        {
            this.GroupRoles.RemoveMarkedForDeletionIfNewObjects();
            this.GroupViewForms.RemoveMarkedForDeletionIfNewObjects();
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            this.GroupRoles.RemoveMarkedForDeletionAndNewFlags();
            this.GroupViewForms.RemoveMarkedForDeletionAndNewFlags();
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
    }
}
