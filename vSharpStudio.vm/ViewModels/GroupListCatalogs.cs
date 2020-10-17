using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
            this.ListCatalogs.OnRemovedAction = (t) => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
            this.ListCatalogs.OnClearedAction = () => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
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
        public bool IsNew { get { return false; } set { } }
        public bool IsMarkedForDeletion { get { return false; } set { } }
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
            foreach (var t in this.ListCatalogs)
            {
                if (t.IsMarkedForDeletion || t.GetIsHasMarkedForDeletion())
                {
                    this.IsHasMarkedForDeletion = true;
                    return true;
                }
            }
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            foreach (var t in this.ListCatalogs)
            {
                if (t.IsNew || t.GetIsHasNew())
                {
                    this.IsHasNew = true;
                    return true;
                }
            }
            this.IsHasNew = false;
            return false;
        }
        #endregion Tree operations
        public void RemoveMarkedForDeletionIfNewObjects()
        {
            var tlst = this.ListCatalogs.ToList();
            foreach (var t in tlst)
            {
                if (t.IsMarkedForDeletion && t.IsNew)
                {
                    this.ListCatalogs.Remove(t);
                    continue;
                }
                t.RemoveMarkedForDeletionIfNewObjects();
            }
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            foreach (var t in this.ListCatalogs)
            {
                t.RemoveMarkedForDeletionAndNewFlags();
                t.IsNew = false;
                t.IsMarkedForDeletion = false;
            }
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
    }
}
