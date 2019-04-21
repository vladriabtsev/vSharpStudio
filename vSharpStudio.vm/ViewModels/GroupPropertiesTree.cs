using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq} sub:{ListSubPropertiesGroups.Count,nq}")]
    public partial class GroupPropertiesTree
    {
        #region ITreeConfigNode

        //protected override bool OnNodeCanMoveUp()
        //{
        //    if (this.Parent is Document)
        //        return (this.Parent as Document).ListPropertiesTreeGroups.IndexOf(this) > 0;
        //    else
        //        return (this.Parent as GroupPropertiesTree).ListSubPropertiesGroups.IndexOf(this) > 0;
        //}
        //protected override void OnNodeMoveUp()
        //{
        //    if (this.Parent is Document)
        //    {
        //        var p = this.Parent as Document;
        //        var i = p.ListPropertiesTreeGroups.IndexOf(this);
        //        if (i > 0)
        //        {
        //            this.SortingValue = p.ListPropertiesTreeGroups[i - 1].SortingValue - 1;
        //        }
        //    }
        //    else
        //    {
        //        var p = this.Parent as GroupPropertiesTree;
        //        var i = p.ListSubPropertiesGroups.IndexOf(this);
        //        if (i > 0)
        //        {
        //            this.SortingValue = p.ListSubPropertiesGroups[i - 1].SortingValue - 1;
        //        }
        //    }
        //}
        //protected override bool OnNodeCanMoveDown()
        //{
        //    if (this.Parent is Document)
        //        return (this.Parent as Document).ListPropertiesTreeGroups.IndexOf(this) < ((this.Parent as Document).ListPropertiesTreeGroups.Count - 1);
        //    else
        //        return (this.Parent as GroupPropertiesTree).ListSubPropertiesGroups.IndexOf(this) < ((this.Parent as GroupPropertiesTree).ListSubPropertiesGroups.Count - 1);
        //}
        //protected override void OnNodeMoveDown()
        //{
        //    if (this.Parent is Document)
        //    {
        //        var p = this.Parent as Document;
        //        var i = p.ListPropertiesTreeGroups.IndexOf(this);
        //        if (i < p.ListPropertiesTreeGroups.Count - 1)
        //        {
        //            this.SortingValue = p.ListPropertiesTreeGroups[i + 1].SortingValue + 1;
        //        }
        //    }
        //    else
        //    {
        //        var p = this.Parent as GroupPropertiesTree;
        //        var i = p.ListSubPropertiesGroups.IndexOf(this);
        //        if (i < p.ListSubPropertiesGroups.Count - 1)
        //        {
        //            this.SortingValue = p.ListSubPropertiesGroups[i + 1].SortingValue + 1;
        //        }
        //    }
        //}
        //protected override void OnNodeRemove()
        //{
        //    if (this.Parent is Document)
        //        (this.Parent as Document).ListPropertiesTreeGroups.Remove(this);
        //    else
        //        (this.Parent as GroupPropertiesTree).ListSubPropertiesGroups.Remove(this);
        //}
        //protected override ITreeConfigNode OnNodeAddNew()
        //{
        //    var res = new Catalog();
        //    res.Parent = this.Parent;
        //    (this.Parent as GroupCatalogs).ListCatalogs.Add(res);
        //    GetUniqueName(Catalog.DefaultName, res, (this.Parent as GroupCatalogs).ListCatalogs);
        //    (this.Parent.Parent as Config).SelectedNode = res;
        //    return res;
        //}
        //protected override ITreeConfigNode OnNodeAddClone()
        //{
        //    var res = Catalog.Clone(this.Parent, this, true, true);
        //    res.Parent = this.Parent;
        //    (this.Parent as GroupCatalogs).ListCatalogs.Add(res);
        //    this.Name = this.Name + "2";
        //    (this.Parent.Parent as Config).SelectedNode = res;
        //    return res;
        //}
        //protected override bool OnNodeCanAddNewSubNode()
        //{
        //    return true;
        //}
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

        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            GroupPropertiesTree t = new GroupPropertiesTree();
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
