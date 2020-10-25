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
    public partial class GroupDocuments : ITreeModel, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion, IEditableNodeGroup
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
            //this.GroupSharedProperties.ListProperties.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.GroupSharedProperties.ListProperties.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.GroupListDocuments.ListDocuments.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.GroupListDocuments.ListDocuments.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.GroupListDocuments.ListDocuments.OnRemovedAction = (t) => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
            //this.GroupSharedProperties.ListProperties.OnRemovedAction = (t) => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
            //this.GroupListDocuments.ListDocuments.OnClearedAction = () => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
            //this.GroupSharedProperties.ListProperties.OnClearedAction = () => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
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
        public Document AddDocument(string name)
        {
            var node = new Document(this.GroupListDocuments) { Name = name };
            this.GroupListDocuments.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name, DataType type)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name, DataType = type };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name, DataType = new DataType() { DataTypeEnum = type, Length = length, Accuracy = accuracy } };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedPropertyString(string name, uint length)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = length };
            var node = new Property(this.GroupSharedProperties) { Name = name, DataType = dt };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedPropertyNumerical(string name, uint length, uint accuracy)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            var node = new Property(this.GroupSharedProperties) { Name = name, DataType = dt };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
    }
}
