using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Document : ICanGoLeft, ICanGoRight, ICanAddNode
    {
        public static readonly string DefaultName = "Document";

        [BrowsableAttribute(false)]
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupProperties.Parent = this;
            this.Children.Add(this.GroupProperties, 6);
            this.GroupPropertiesTabs.Parent = this;
            this.Children.Add(this.GroupPropertiesTabs, 7);
            this.GroupForms.Parent = this;
            this.Children.Add(this.GroupForms, 8);
            this.GroupReports.Parent = this;
            this.Children.Add(this.GroupReports, 9);
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListDocuments).ListDocuments.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Document)(this.Parent as GroupListDocuments).ListDocuments.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListDocuments).ListDocuments.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListDocuments).ListDocuments.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Document)(this.Parent as GroupListDocuments).ListDocuments.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListDocuments).ListDocuments.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove()
        {
            (this.Parent as GroupListDocuments).Remove(this);
            this.Parent = null;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = Document.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListDocuments).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Document(this.Parent);
            (this.Parent as GroupListDocuments).Add(node);
            this.GetUniqueName(Document.DefaultName, node, (this.Parent as GroupListDocuments).ListDocuments);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
