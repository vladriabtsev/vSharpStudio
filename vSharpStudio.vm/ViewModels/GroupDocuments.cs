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
            mes = mes + $" Shared:{this.GroupSharedProperties.ListProperties.Count} Docs:{this.GroupListDocuments.ListDocuments.Count}";
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
            this._PrefixForCompositionNames = "Doc";
            this._MondayBeforeFirstDocDate = Timestamp.FromDateTime(new DateTime(1000, 1, 6, 0, 0, 0, DateTimeKind.Utc));
            this._TimeLinePropertyName = "DocDateTime";
            this._DocShortTypeIdPropertyName = "DocShortTypeId";
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
            children.Add(this.GroupSharedProperties, 2);
            children.Add(this.GroupListSequences, 3);
            children.Add(this.GroupListDocuments, 4);
            children.Add(this.GroupJournals, 5);
            children.Add(this.GroupRegisters, 6);

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
            this._TimelineName = "JournalTimeline";
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
        public Property AddSharedProperty(string name, string? guid = null)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedPropertyCatalog(string name, string catGuid, bool isNullable = false, bool isCsNullable = true, string? guidProperty = null)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name, IsNullable = isNullable, IsCsNullable = isCsNullable };
#if DEBUG
            if (guidProperty != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guidProperty))
                    return node;
                node.Guid = guidProperty;
            }
#endif
            node.DataType = new DataType(node)
            {
                DataTypeEnum = EnumDataType.CATALOG,
                IsNullable = isNullable,
                ObjectRef = new FkComplexRef(catGuid)
            };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name, DataType type, string? guid = null)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name, DataType = type };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name, EnumDataType type, uint length, uint accuracy, string? guid = null)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = type, Length = length, Accuracy = accuracy };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedPropertyString(string name, uint length, string? guid = null)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedPropertyNumerical(string name, uint length, uint accuracy, string? guid = null)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        /// <summary>
        /// Only shared properties
        /// </summary>
        /// <param name="guidAppPrjGen"></param>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            foreach (var t in this.GroupSharedProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    t.IsDocShared = true;
                    res.Add(t);
                }
            }
            return res;
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

        public string GetTimelineCompositeName()
        {
            string name = this.TimelineName;
            if (this.ParentModel.IsUseNameComposition)
            {
                name = this.PrefixForCompositionNames + name;
            }
            return name;
        }
    }
}
