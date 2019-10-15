using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListCatalogs.Count,nq}")]
    public partial class GroupListCatalogs : ITreeModel, ICanAddSubNode, ICanGoRight
    {
        public override IEnumerable<object> GetChildren(object parent) { return this.ListCatalogs; }
        public override bool HasChildren(object parent) { return this.ListCatalogs.Count > 0; }

        partial void OnInit()
        {
            this.Name = Defaults.CatalogsGroupName;
            this.IsEditable = false;
            if (this.Parent is Catalog)
                this.NameUi = "Sub Catalogs";
        }
        protected override void OnInitFromDto()
        {
            if (this.Parent is Catalog)
                this.NameUi = "Sub Catalogs";
        }
        #region Tree operations
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
                node = new Catalog(this);
            else
                node = (Catalog)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Catalog.DefaultName, node, this.ListCatalogs);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
