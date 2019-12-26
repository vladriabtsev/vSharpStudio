using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Catalog:{Name,nq} props:{GroupProperties.ListProperties.Count,nq}")]
    public partial class Catalog : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings
    {
        public static readonly string DefaultName = "Catalog";
        [DisplayName("Generators")]
        [Description("Expandable Attached Node Settings for App Project Generators")]
        [ExpandableObjectAttribute()]
        public object GenSettings { get; set; }
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupProperties.Parent = this;
            this.Children.Add(this.GroupProperties, 3);
            this.GroupForms.Parent = this;
            this.Children.Add(this.GroupForms, 4);
            this.GroupReports.Parent = this;
            this.Children.Add(this.GroupReports, 5);
            this.AddAllAppGenSettingsVmsToNewNode();
        }

        public Catalog(ITreeConfigNode parent, string name)
            : this(parent)
        {
            (this as ITreeConfigNode).Name = name;
        }

        public Catalog(ITreeConfigNode parent, string name, List<Property> listProperties)
            : this(parent)
        {
            (this as ITreeConfigNode).Name = name;
            foreach (var t in listProperties)
            {
                this.GroupProperties.ListProperties.Add(t);
            }
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

        public override void NodeRemove()
        {
            (this.Parent as GroupListCatalogs).Remove(this);
            this.Parent = null;
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
        #endregion Tree operations
        [ExpandableObjectAttribute()]
        public dynamic Setting { get; set; }
    }
}
