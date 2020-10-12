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
            this.GroupSharedProperties.ListProperties.OnAddingAction = (t) => {
                t.IsNew = true;
            };
            this.GroupSharedProperties.ListProperties.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.GroupListDocuments.ListDocuments.OnAddingAction = (t) => {
                t.IsNew = true;
            };
            this.GroupListDocuments.ListDocuments.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
        }
        //public override void MarkForDeletion()
        //{
        //    this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        //}
        //partial void OnIsMarkedForDeletionChanged()
        //{
        //    if (this.IsMarkedForDeletion)
        //    {
        //        (this.Parent as INewAndDeleteion).IsMarkedForDeletion = true;
        //    }
        //    else
        //    {
        //        var p = (this.Parent as GroupListForms);
        //        bool isMarked = false;
        //        foreach (var t in p.ListForms)
        //        {
        //            if (t.IsMarkedForDeletion)
        //            {
        //                isMarked = true;
        //                break;
        //            }
        //        }
        //        p.IsMarkedForDeletion = isMarked;
        //    }
        //}
    }
}
