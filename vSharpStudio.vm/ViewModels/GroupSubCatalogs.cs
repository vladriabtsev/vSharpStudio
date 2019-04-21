﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} sub_catalogs:{ListCatalogs.Count,nq}")]
    public partial class GroupSubCatalogs : IListNodes<Catalog>
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Catalog> ListNodes { get; private set; }

        partial void OnInit()
        {
            this.Name = "SubCatalogs";
            this.ListNodes = this.ListCatalogs;
        }

        #region ITreeNode

        public new string NodeText { get { return this.Name; } }
        //protected override bool OnNodeCanLeft()
        //{
        //    return false;
        //}
        //protected override ITreeConfigNode OnNodeAddNewSubNode()
        //{
        //    var res = new Constant();
        //    res.Parent = this.Parent;
        //    this.ListConstants.Add(res);
        //    GetUniqueName(Constant.DefaultName, res, this.ListConstants);
        //    (this.Parent as Config).SelectedNode = res;
        //    return res;
        //}
        //protected override bool OnNodeCanMoveDown()
        //{
        //    return false;
        //}
        //protected override bool OnNodeCanMoveUp()
        //{
        //    return false;
        //}
        //protected override bool OnNodeCanAddClone()
        //{
        //    return false;
        //}
        //protected override bool OnNodeCanRemove()
        //{
        //    return false;
        //}

        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            GroupSubCatalogs t = new GroupSubCatalogs();
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
