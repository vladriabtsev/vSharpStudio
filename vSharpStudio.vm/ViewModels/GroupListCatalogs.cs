using System;
using System.Collections.Generic;
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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListCatalogs.Count,nq}")]
    public partial class GroupListCatalogs : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<Catalog> Children { get { return this.ListCatalogs; } }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListCatalogs;
        }

        public override bool HasChildren(object parent)
        {
            return this.ListCatalogs.Count > 0;
        }

        partial void OnInit()
        {
            this.Name = Defaults.CatalogsGroupName;
            this.PrefixForDbTables = "Ctlg";
            this.IsEditable = false;
            if (this.Parent is Catalog)
            {
                this.NameUi = "Sub Catalogs";
            }
            this.ListCatalogs.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListCatalogs.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListCatalogs.OnRemovedAction = (t) =>
            {
                var cfg = this.GetConfig();
                cfg.DicDeletedNodesInCurrentSession[t.Guid] = t;
            };
        }
        protected override void OnInitFromDto()
        {
            if (this.Parent is Catalog)
            {
                this.NameUi = "Sub Catalogs";
            }
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Catalog AddCatalog()
        {
            var node = new Catalog(this);
            this.NodeAddNewSubNode(node);
            return node;
        }

        public Catalog AddCatalog(string name)
        {
            var node = new Catalog(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Catalog node = null;
            if (node_impl == null)
            {
                node = new Catalog(this);
            }
            else
            {
                node = (Catalog)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Catalog.DefaultName, node, this.ListCatalogs);
            }

            this.SetSelected(node);
            return node;
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            (this.Parent as INewAndDeleteion).IsMarkedForDeletion = this.IsMarkedForDeletion;
        }
        partial void OnIsNewChanged()
        {
            (this.Parent as INewAndDeleteion).IsNew = this.IsNew;
        }
        #endregion Tree operations
    }
}
