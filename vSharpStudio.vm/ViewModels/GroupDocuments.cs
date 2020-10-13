using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq}")]
    public partial class GroupDocuments : ITreeModel, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.Children;
        }
        public override bool HasChildren(object parent)
        {
            return this.Children.Count > 0;
        }
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnInit()
        {
            this.Name = Defaults.DocumentsGroupName;
            this.PrefixForDbTables = "Doc";
            this.IsEditable = false;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.GroupSharedProperties.Parent = this;
            this.Children.Add(this.GroupSharedProperties, 7);
            this.GroupListDocuments.Parent = this;
            this.Children.Add(this.GroupListDocuments, 8);
            this.GroupSharedProperties.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.GroupSharedProperties.ListProperties.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.GroupListDocuments.ListDocuments.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.GroupListDocuments.ListDocuments.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
        }
        public bool IsNew { get { return false; } set { } }
        public bool IsMarkedForDeletion { get { return false; } set { } }
        partial void OnIsHasMarkedForDeletionChanged()
        {
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
            if (this.GroupSharedProperties.IsMarkedForDeletion || this.GroupSharedProperties.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            if (this.GroupListDocuments.IsMarkedForDeletion || this.GroupListDocuments.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            if (this.GroupSharedProperties.IsHasNew || this.GroupSharedProperties.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            if (this.GroupListDocuments.IsHasNew || this.GroupListDocuments.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            this.IsHasNew = false;
            return false;
        }
    }
}
