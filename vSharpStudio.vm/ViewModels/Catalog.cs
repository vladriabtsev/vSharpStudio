using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Catalog:{Name,nq} props:{listProperties.Count,nq}")]
    public partial class Catalog : IListGroupNodes
    {
        public static readonly string DefaultName = "Catalog";
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.ListNodes = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            ListNodes.Add(this.GroupProperties, 7);
            ListNodes.Add(this.GroupSubCatalogs, 8);
        }
        public Catalog(string name) : this()
        {
            (this as ITreeConfigNode).Name = name;
        }
        public Catalog(string name, List<Property> listProperties) : this()
        {
            (this as ITreeConfigNode).Name = name;
            foreach (var t in listProperties)
            {
                this.GroupProperties.ListProperties.Add(t);
            }
        }

        #region ITreeConfigNode

        protected override bool OnNodeCanMoveUp()
        {
            return (this.Parent as GroupCatalogs).ListCatalogs.IndexOf(this) > 0;
        }
        protected override void OnNodeMoveUp()
        {
            var p = this.Parent as GroupCatalogs;
            var i = p.ListCatalogs.IndexOf(this);
            if (i > 0)
            {
                this.SortingValue = p.ListCatalogs[i - 1].SortingValue - 1;
            }
        }
        protected override bool OnNodeCanMoveDown()
        {
            return (this.Parent as GroupCatalogs).ListCatalogs.IndexOf(this) < ((this.Parent as GroupCatalogs).ListCatalogs.Count - 1);
        }
        protected override void OnNodeMoveDown()
        {
            var p = this.Parent as GroupCatalogs;
            var i = p.ListCatalogs.IndexOf(this);
            if (i < p.ListCatalogs.Count - 1)
            {
                this.SortingValue = p.ListCatalogs[i + 1].SortingValue + 1;
            }
        }
        protected override void OnNodeRemove()
        {
            (this.Parent as GroupCatalogs).ListCatalogs.Remove(this);
        }
        protected override ITreeConfigNode OnNodeAddNew()
        {
            var res = new Catalog();
            res.Parent = this.Parent;
            (this.Parent as GroupCatalogs).ListCatalogs.Add(res);
            GetUniqueName(Catalog.DefaultName, res, (this.Parent as GroupCatalogs).ListCatalogs);
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }
        protected override ITreeConfigNode OnNodeAddClone()
        {
            var res = Catalog.Clone(this.Parent, this, true, true);
            res.Parent = this.Parent;
            (this.Parent as GroupCatalogs).ListCatalogs.Add(res);
            this.Name = this.Name + "2";
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }
        //protected override ITreeConfigNode OnNodeAddNewSubNode()
        //{
        //    var res = new Property();
        //    res.Parent = this.Parent;
        //    this.ListProperties.Add(res);
        //    GetUniqueName(Property.DefaultName, res, this.ListProperties);
        //    (this.Parent.Parent as Config).SelectedNode = res;
        //    return res;
        //}

        #endregion ITreeConfigNode

        #region ITreeNode

        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            Catalog t = new Catalog();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.DicPropAttrs res = new Proto.Attr.DicPropAttrs();
            t.PropertyNameAction(p => p.NameUi, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(2).ToString();
            });
            t.PropertyNameAction(p => p.Description, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(3).ToString();
            });
            return res;
        }
    }
}
