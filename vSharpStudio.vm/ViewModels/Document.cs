using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Document : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "Document";

        [BrowsableAttribute(false)]
        public IGroupListDocuments IParent { get { return (IGroupListDocuments)this.Parent; } }
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconDiagnosticesFile"; } }
        //protected override string GetNodeIconName() { return "iconDiagnosticesFile"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupProperties.Parent = this;
            this.GroupPropertiesTabs.Parent = this;
            this.GroupForms.Parent = this;
            this.GroupReports.Parent = this;
            this.RefillChildren();
        }
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            this.RefillChildren();
        }
        void RefillChildren()
        {
            this.Children.Clear();
            this.Children.Add(this.GroupProperties, 6);
            this.Children.Add(this.GroupPropertiesTabs, 7);
            this.Children.Add(this.GroupForms, 8);
            this.Children.Add(this.GroupReports, 9);
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupPropertiesTabs.AddAllAppGenSettingsVmsToNode();
            this.GroupForms.AddAllAppGenSettingsVmsToNode();
            this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }
        public PropertiesTab AddPropertiesTab(string name)
        {
            var node = new PropertiesTab(this.GroupPropertiesTabs) { Name = name };
            this.GroupPropertiesTabs.NodeAddNewSubNode(node);
            return node;
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

        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListDocuments).Remove(this);
            this.Parent = null;
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            if (this.IsMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsNewChanged()
        {
            if (this.IsNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
        }
        //partial void OnIsHasMarkedForDeletionChanged()
        //{
        //    if (this.IsHasMarkedForDeletion)
        //    {
        //        (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
        //    }
        //    else
        //    {
        //        var p = (this.Parent as INewAndDeleteion);
        //        p.GetIsHasMarkedForDeletion();
        //    }
        //}
        //partial void OnIsHasNewChanged()
        //{
        //    if (this.IsHasNew)
        //    {
        //        (this.Parent as INewAndDeleteion).IsHasNew = true;
        //    }
        //    else
        //    {
        //        var p = (this.Parent as INewAndDeleteion);
        //        p.GetIsHasNew();
        //    }
        //}
        public bool GetIsHasMarkedForDeletion()
        {
            if (this.GroupForms.IsMarkedForDeletion || this.GroupForms.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            if (this.GroupProperties.IsMarkedForDeletion || this.GroupProperties.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            if (this.GroupPropertiesTabs.IsMarkedForDeletion || this.GroupPropertiesTabs.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            if (this.GroupReports.IsMarkedForDeletion || this.GroupReports.GetIsHasMarkedForDeletion())
            {
                this.IsHasMarkedForDeletion = true;
                return true;
            }
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            if (this.GroupForms.IsHasNew || this.GroupForms.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            if (this.GroupProperties.IsHasNew || this.GroupProperties.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            if (this.GroupPropertiesTabs.IsHasNew || this.GroupPropertiesTabs.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            if (this.GroupReports.IsHasNew || this.GroupReports.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            this.IsHasNew = false;
            return false;
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
        [PropertyOrder(1)]
        [ReadOnly(true)]
        [DisplayName("Composite")]
        [Description("Composite name based on IsCompositeNames and IsUseGroupPrefix model parameters")]
        public string CompositeName
        {
            get
            {
                return GetCompositeName();
            }
        }
    }
}
