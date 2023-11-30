using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class ManyToManyGroupRelations : ITreeModel, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Cats:{this.GroupListCatalogsRelations.ListCatalogsRelations.Count} Docs:{this.GroupListDocumentsRelations.ListDocumentsRelations.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentModel.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            if (this.Children.Count > 0)
                return;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupListCatalogsRelations, 2);
            children.Add(this.GroupListDocumentsRelations, 3);

            //this.ListRoles.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListRoles.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListRoles.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListRoles.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
            this._Name = Defaults.GroupMtmRelationsName;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }

        //#region Roles
        //public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        //{
        //    var pa = role.DefaultDocumentEditAccessSettings;
        //    switch (pa)
        //    {
        //        case EnumDocumentAccess.D_HIDE:
        //            return EnumPropertyAccess.P_HIDE;
        //        case EnumDocumentAccess.D_VIEW:
        //            return EnumPropertyAccess.P_VIEW;
        //        case EnumDocumentAccess.D_EDIT:
        //        case EnumDocumentAccess.D_MARK_DEL:
        //        case EnumDocumentAccess.D_POST:
        //        case EnumDocumentAccess.D_UNPOST:
        //            return EnumPropertyAccess.P_EDIT;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}
        //public EnumPrintAccess GetRolePropertyPrint(IRole role)
        //{
        //    var pa = role.DefaultDocumentPrintAccessSettings;
        //    if (pa == EnumPrintAccess.PR_BY_PARENT)
        //        return EnumPrintAccess.PR_PRINT;
        //    return pa;
        //}
        //public EnumDocumentAccess GetRoleDocumentAccess(IRole role)
        //{
        //    return role.DefaultDocumentEditAccessSettings;
        //}
        //public EnumPrintAccess GetRoleDocumentPrint(IRole role)
        //{
        //    return role.DefaultDocumentPrintAccessSettings;
        //}
        //#endregion Roles
    }
}
