using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{GroupProperties.ListProperties.Count,nq} tabs:{GroupDetails.ListDetails.Count,nq}")]
    public partial class Detail : ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNode, IEditableNodeGroup, IDbTable, INodeWithProperties
    {
        public static readonly string DefaultName = "Tab";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListDetails;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        [BrowsableAttribute(false)]
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.IsIndexFk = true;
            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyRefParentGuid = System.Guid.NewGuid().ToString();
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.GroupProperties.Parent = this;
            this.GroupProperties.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.GroupProperties.ListProperties.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };

            VmBindable.IsNotifyingStatic = false;
            this.Children.Add(this.GroupProperties, 7);
            VmBindable.IsNotifyingStatic = true;

            this.GroupDetails.Parent = this;
            this.GroupProperties.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.GroupDetails.ListDetails.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.Children.Add(this.GroupDetails, 9);
            var glp = (this.Parent.Parent as INodeWithProperties);
            this.Position = glp.GroupProperties.GetNextPosition();
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListDetails).ListDetails.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Detail)(this.Parent as GroupListDetails).ListDetails.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListDetails).ListDetails.MoveUp(this);
            this.SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListDetails).ListDetails.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Detail)(this.Parent as GroupListDetails).ListDetails.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListDetails).ListDetails.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Detail.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListDetails).Add(node);
            var glp = (this.Parent.Parent as INodeWithProperties);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Detail(this.Parent);
            (this.Parent as GroupListDetails).Add(node);
            var glp = (this.Parent.Parent as INodeWithProperties);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.GetUniqueName(Detail.DefaultName, node, (this.Parent as GroupListDetails).ListDetails);
            this.SetSelected(node);
            return node;
        }
        public Detail AddPropertiesTab(string name)
        {
            var node = new Detail(this.GroupDetails) { Name = name };
            this.GroupDetails.NodeAddNewSubNode(node);
            var glp = (this.Parent.Parent as INodeWithProperties);
            node.Position = glp.GroupProperties.GetNextPosition();
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
        public IEnumerable<ITreeConfigNode> GetParentList()
        {
            var p = this.Parent as GroupListDetails;
            return p.ListDetails;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListDetails;
            p.ListDetails.Remove(this);
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
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            var cfg = this.GetConfig();
            var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            res.Add(prp);
            var parent = this.Parent.Parent as ICompositeName;
            prp = cfg.Model.GetPropertyRefParent(this.PropertyRefParentGuid, "Ref" + parent.CompositeName);
            res.Add(prp);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedViewProperties(string guidAppPrjDbGen)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, true);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjDbGen))
                {
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            res.Add(t);
                            break;
                        }
                    }
                }
            }
            return res;
        }
        private void GetSpecialProperties(List<IProperty> res, bool isAll)
        {
            var cfg = this.GetConfig();
            var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            if (isAll)
            {
                res.Add(prp);
            }
            //if (this.GetUseCodeProperty())
            //{
            //    switch (this.CodePropertySettings.Type)
            //    {
            //        case EnumCodeType.AutoNumber:
            //            throw new NotImplementedException();
            //        case EnumCodeType.AutoText:
            //            throw new NotImplementedException();
            //        case EnumCodeType.Number:
            //            prp = cfg.Model.GetPropertyCatalogCodeInt(this.PropertyCodeGuid, this.CodePropertySettings.Length);
            //            break;
            //        case EnumCodeType.Text:
            //            prp = cfg.Model.GetPropertyCatalogCode(this.PropertyCodeGuid, this.CodePropertySettings.Length);
            //            break;
            //    }
            //    res.Add(prp);
            //}
            //if (this.GetUseNameProperty())
            //{
            //    prp = cfg.Model.GetPropertyCatalogName(this.PropertyNameGuid, this.MaxNameLength);
            //    res.Add(prp);
            //}
            //if (this.GetUseDescriptionProperty())
            //{
            //    prp = cfg.Model.GetPropertyCatalogDescription(this.PropertyDescriptionGuid, this.MaxDescriptionLength);
            //    res.Add(prp);
            //}
        }
        public IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen)
        {
            var res = new List<IDetail>();
            foreach (var t in this.GroupDetails.ListDetails)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
    }
}
