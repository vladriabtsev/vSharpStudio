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
    public partial class GroupListCatalogs : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings
    {
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
            this.IsEditable = false;
            if (this.Parent is Catalog)
            {
                this.NameUi = "Sub Catalogs";
            }
            this.ListCatalogs.OnAddedAction = (t) =>
            {
                t.AddAllAppGenSettingsVmsToNode();
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

        [DisplayName("Generators")]
        [Description("Expandable Attached Node Settings for App Project Generators")]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [PropertyOrderAttribute(int.MaxValue)]
        public object GeneratorNodeSettings
        {
            get
            {
                if (!(this is INodeGenSettings))
                    return null;
                var res = SettingsTypeBuilder.CreateNewObject(this.ListNodeGeneratorsSettings);
                return res;
            }
        }
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
        #endregion Tree operations
    }
}
