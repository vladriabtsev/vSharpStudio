using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Catalog:{Name,nq} props:{GroupProperties.ListProperties.Count,nq}")]
    public partial class Catalog : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "Catalog";
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
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
            this.Children.Add(this.GroupProperties, 3);
            this.Children.Add(this.GroupPropertiesTabs, 4);
            this.Children.Add(this.GroupForms, 5);
            this.Children.Add(this.GroupReports, 6);
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupPropertiesTabs.AddAllAppGenSettingsVmsToNode();
            this.GroupForms.AddAllAppGenSettingsVmsToNode();
            this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        public Catalog(ITreeConfigNode parent, string name)
            : this(parent)
        {
            (this as ITreeConfigNode).Name = name;
        }

        public Catalog(ITreeConfigNode parent, string name, List<Property> listProperties)
            : this(parent)
        {
            Contract.Requires(listProperties != null);
            (this as ITreeConfigNode).Name = name;
            foreach (var t in listProperties)
            {
                this.GroupProperties.ListProperties.Add(t);
            }
        }
        public PropertiesTab AddPropertiesTab(string name)
        {
            var node = new PropertiesTab(this.GroupPropertiesTabs) { Name = name };
            this.GroupPropertiesTabs.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name)
        {
            var node = new Property(this.GroupProperties) { Name = name };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, DataType type)
        {
            var node = new Property(this.GroupProperties) { Name = name, DataType = type };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this.GroupProperties) { Name = name, DataType = new DataType() { DataTypeEnum = type, Length = length, Accuracy = accuracy } };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = length };
            var node = new Property(this.GroupProperties) { Name = name, DataType = dt };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            var node = new Property(this.GroupProperties) { Name = name, DataType = dt };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListCatalogs).ListCatalogs.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Catalog)(this.Parent as GroupListCatalogs).ListCatalogs.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListCatalogs).ListCatalogs.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListCatalogs).ListCatalogs.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Catalog)(this.Parent as GroupListCatalogs).ListCatalogs.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListCatalogs).ListCatalogs.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListCatalogs).Remove(this);
            this.Parent = null;
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            if (this.IsNotNotifying)
                return;
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
            if (this.IsNotNotifying)
                return;
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
            if (this.GroupProperties.IsMarkedForDeletion || this.GroupProperties.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            if (this.GroupPropertiesTabs.IsMarkedForDeletion || this.GroupPropertiesTabs.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            if (this.GroupReports.IsMarkedForDeletion || this.GroupReports.GetIsHasNew())
            {
                this.IsHasNew = true;
                return true;
            }
            this.IsHasNew = false;
            return false;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = Catalog.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListCatalogs).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Catalog(this.Parent);
            (this.Parent as GroupListCatalogs).Add(node);
            this.GetUniqueName(Catalog.DefaultName, node, (this.Parent as GroupListCatalogs).ListCatalogs);
            this.SetSelected(node);
            return node;
        }
        public void RemoveMarkedForDeletionIfNewObjects()
        {
            this.GroupForms.RemoveMarkedForDeletionIfNewObjects();
            this.GroupProperties.RemoveMarkedForDeletionIfNewObjects();
            this.GroupPropertiesTabs.RemoveMarkedForDeletionIfNewObjects();
            this.GroupReports.RemoveMarkedForDeletionIfNewObjects();
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            this.GroupForms.RemoveMarkedForDeletionAndNewFlags();
            this.GroupProperties.RemoveMarkedForDeletionAndNewFlags();
            this.GroupPropertiesTabs.RemoveMarkedForDeletionAndNewFlags();
            this.GroupReports.RemoveMarkedForDeletionAndNewFlags();
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
        #endregion Tree operations

        [ExpandableObjectAttribute()]
        public dynamic Setting { get; set; }

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
