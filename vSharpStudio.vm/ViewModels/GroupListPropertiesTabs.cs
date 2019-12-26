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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListPropertiesTabs.Count,nq}")]
    public partial class GroupListPropertiesTabs : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings
    {
        [DisplayName("Generators")]
        [Description("Expandable Attached Node Settings for App Project Generators")]
        [ExpandableObjectAttribute()]
        public object GenSettings { get; set; }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListPropertiesTabs;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListPropertiesTabs.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = "Tabs";
            this.IsEditable = false;
            this.AddAllAppGenSettingsVmsToNewNode();
            this.ListPropertiesTabs.CollectionChanged += ListPropertiesTabs_CollectionChanged;
        }
        private void ListPropertiesTabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnAddRemoveNode(e);
        }

        #region Tree operations
        public PropertiesTab AddPropertiesTab(string name)
        {
            var node = new PropertiesTab(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            PropertiesTab node = null;
            if (node_impl == null)
            {
                node = new PropertiesTab(this);
            }
            else
            {
                node = (PropertiesTab)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(PropertiesTab.DefaultName, node, this.ListPropertiesTabs);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        [BrowsableAttribute(false)]
        public List<IPropertiesTab> ListAnnotated
        {
            get
            {
                var cfg = (Config)this.GetConfig();
                var p = this.Parent;
                while (p.IsIncludableInModels == false)
                {
                    p = p.Parent;
                }

                string par = p.GetType().Name;
                ConfigNodesCollection<PropertiesTab> curr;
                ConfigNodesCollection<PropertiesTab> prev;
                ConfigNodesCollection<PropertiesTab> old;
                switch (par)
                {
                    case "Document":
                        var d = (Document)cfg.DicNodes[p.Guid];
                        curr = d.GroupPropertiesTabs.ListPropertiesTabs;
                        d = (Document)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = d?.GroupPropertiesTabs.ListPropertiesTabs;
                        d = (Document)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = d?.GroupPropertiesTabs.ListPropertiesTabs;
                        break;
                    case "PropertiesTab":
                        var t = (PropertiesTab)cfg.DicNodes[p.Guid];
                        curr = t.GroupPropertiesTabs.ListPropertiesTabs;
                        t = (PropertiesTab)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = t?.GroupPropertiesTabs.ListPropertiesTabs;
                        t = (PropertiesTab)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = t?.GroupPropertiesTabs.ListPropertiesTabs;
                        break;
                    case "Catalog":
                        var c = (Catalog)cfg.DicNodes[p.Guid];
                        curr = c.GroupPropertiesTabs.ListPropertiesTabs;
                        c = (Catalog)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = c?.GroupPropertiesTabs.ListPropertiesTabs;
                        c = (Catalog)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = c?.GroupPropertiesTabs.ListPropertiesTabs;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                var diff = new DiffLists<IPropertiesTab>(old, prev, curr);
                return diff.ListAll;
            }
        }
    }
}
