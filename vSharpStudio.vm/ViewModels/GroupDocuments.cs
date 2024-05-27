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
    public partial class GroupDocuments : ITreeModel, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Shared:{this.DocumentTimeline.ListProperties.Count} Docs:{this.GroupListDocuments.ListDocuments.Count}";
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
            this._MondayBeforeFirstDocDate = Timestamp.FromDateTime(new DateTime(1000, 1, 6, 0, 0, 0, DateTimeKind.Utc));

            this._UseDocNumberProperty = true;

            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            if (string.IsNullOrWhiteSpace(this._PrefixForCompositionNames)) this._PrefixForCompositionNames = "Doc";
            if (string.IsNullOrWhiteSpace(this._PropertyDocNumberName)) this._PropertyDocNumberName = "DocNumber";
            if (string.IsNullOrWhiteSpace(this._DocShortTypeIdPropertyName)) this._DocShortTypeIdPropertyName = "DocShortTypeId";
            if (this.Children.Count > 0)
                return;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.DocumentTimeline, 2);
            children.Add(this.GroupListSequences, 3);
            children.Add(this.GroupListDocuments, 4);
            children.Add(this.GroupRegisters, 5);
            children.Add(this.GroupJournals, 6);

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
            this._Name = Defaults.DocumentsGroupName;
        }
        public Document AddDocument(string name, string? guid = null)
        {
            var node = new Document(this.GroupListDocuments) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.GroupListDocuments.NodeAddNewSubNode(node);
            return node;
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
        public bool IsGridSortableGet()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridSortable;
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridFilterable;
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridSortableCustom;
        }

        #region Roles
        public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        {
            var pa = role.DefaultDocumentEditAccessSettings;
            switch (pa)
            {
                case EnumDocumentAccess.D_HIDE:
                    return EnumPropertyAccess.P_HIDE;
                case EnumDocumentAccess.D_VIEW:
                    return EnumPropertyAccess.P_VIEW;
                case EnumDocumentAccess.D_EDIT:
                case EnumDocumentAccess.D_MARK_DEL:
                case EnumDocumentAccess.D_POST:
                case EnumDocumentAccess.D_UNPOST:
                    return EnumPropertyAccess.P_EDIT;
                default:
                    throw new NotImplementedException();
            }
        }
        public EnumPrintAccess GetRolePropertyPrint(IRole role)
        {
            var pa = role.DefaultDocumentPrintAccessSettings;
            if (pa == EnumPrintAccess.PR_BY_PARENT)
                return EnumPrintAccess.PR_PRINT;
            return pa;
        }
        public EnumDocumentAccess GetRoleDocumentAccess(IRole role)
        {
            return role.DefaultDocumentEditAccessSettings;
        }
        public EnumPrintAccess GetRoleDocumentPrint(IRole role)
        {
            return role.DefaultDocumentPrintAccessSettings;
        }
        #endregion Roles

        [Browsable(false)]
        public string DocumentTimelineName
        {
            get
            {
                if (this._DocumentTimelineName == null)
                {
                    this._DocumentTimelineName = this.DocumentTimeline.Name;
                }
                Debug.Assert(this._DocumentTimelineName != null);
                return this._DocumentTimelineName;
            }
        }
        private string? _DocumentTimelineName = null;
        [Browsable(false)]
        public string DocumentDocDateTimePropertyName
        {
            get
            {
                if (this._DocumentDocDateTimePropertyName == null)
                {
                    this._DocumentDocDateTimePropertyName = this.DocumentTimeline.TimeLineDocDateTimePropertyName;
                }
                Debug.Assert(this._DocumentDocDateTimePropertyName != null);
                return this._DocumentDocDateTimePropertyName;
            }
        }
        private string? _DocumentDocDateTimePropertyName = null;
    }
}
