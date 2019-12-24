using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListProperties.Count,nq}")]
    public partial class GroupListProperties : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListProperties;
        }

        public override bool HasChildren(object parent)
        {
            return this.ListProperties.Count > 0;
        }

        partial void OnInit()
        {
            if (this.Parent is GroupDocuments)
            {
                this.Name = "Shared";
            }
            else
            {
                this.Name = "Properties";
            }

            this.IsEditable = false;
            this.AddAllAppGenSettingsVmsToNewNode();
            this.ListProperties.CollectionChanged += ListProperties_CollectionChanged;
        }
        private void ListProperties_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnAddRemoveNode(e);
        }

        protected override void OnParentChanged()
        {
            if (this.Parent is GroupDocuments)
            {
                this.Name = "Shared";
            }
            else
            {
                this.Name = "Properties";
            }
        }

        protected override void OnInitFromDto()
        {
            if (this.Parent is GroupDocuments)
            {
                this.Name = "Shared";
            }
            else
            {
                this.Name = "Properties";
            }
        }

        #region Tree operations
        public Property AddProperty(string name)
        {
            var node = new Property(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public Property AddProperty(string name, DataType type)
        {
            var node = new Property(this) { Name = name, DataType = type };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this) { Name = name, DataType = new DataType() { DataTypeEnum = type, Length = length, Accuracy = accuracy } };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Property node = null;
            if (node_impl == null)
            {
                node = new Property(this);
            }
            else
            {
                node = (Property)node_impl;
            }

            this.Add(node);
            // TODO can be more economical?
            if (this.LastGenPosition == 0)
            {
                this.LastGenPosition = 1;
            }

            this.LastGenPosition++;
            node.Position = this.LastGenPosition;
            if (node_impl == null)
            {
                this.GetUniqueName(Property.DefaultName, node, this.ListProperties);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        [BrowsableAttribute(false)]
        public List<IProperty> ListAnnotated
        {
            get
            {
                var cfg = (Config)this.GetConfig();
                var p = this.Parent;
                while (p != null && p.IsIncludableInModels == false)
                {
                    p = p.Parent;
                }

                if (p == null)
                    return new List<IProperty>();
                string par = p.GetType().Name;
                ConfigNodesCollection<Property> curr;
                ConfigNodesCollection<Property> prev;
                ConfigNodesCollection<Property> old;
                switch (par)
                {
                    case "Document":
                        var d = (Document)cfg.DicNodes[p.Guid];
                        curr = d.GroupProperties.ListProperties;
                        d = (Document)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = d?.GroupProperties.ListProperties;
                        d = (Document)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = d?.GroupProperties.ListProperties;
                        break;
                    case "PropertiesTab":
                        var t = (PropertiesTab)cfg.DicNodes[p.Guid];
                        curr = t.GroupProperties.ListProperties;
                        t = (PropertiesTab)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = t?.GroupProperties.ListProperties;
                        t = (PropertiesTab)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = t?.GroupProperties.ListProperties;
                        break;
                    case "Catalog":
                        var c = (Catalog)cfg.DicNodes[p.Guid];
                        curr = c.GroupProperties.ListProperties;
                        c = (Catalog)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = c?.GroupProperties.ListProperties;
                        c = (Catalog)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = c?.GroupProperties.ListProperties;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                var diff = new DiffLists<IProperty>(old, prev, curr);
                return diff.ListAll;
            }
        }
    }
}
