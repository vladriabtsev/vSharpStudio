using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupDocuments : ITreeModel, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup, IRoleAccess
    {
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
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this._Name = Defaults.DocumentsGroupName;
            this.PrefixForDbTables = "Doc";
            this.IsEditable = false;

            Init();
            this.InitRoles();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            if (this.Children.Count > 0)
                return;
            VmBindable.IsNotifyingStatic = false;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupSharedProperties, 1);
            children.Add(this.GroupListDocuments, 2);
            VmBindable.IsNotifyingStatic = true;

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
        }
        public Document AddDocument(string name)
        {
            var node = new Document(this.GroupListDocuments) { Name = name };
            this.GroupListDocuments.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name, DataType type)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name, DataType = type };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = type, Length = length, Accuracy = accuracy };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedPropertyString(string name, uint length)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            this.GroupSharedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddSharedPropertyNumerical(string name, uint length, uint accuracy)
        {
            var node = new Property(this.GroupSharedProperties) { Name = name };
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
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
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
        public bool GetUseDocCodeProperty()
        {
            if (this.UseDocCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseDocCodeProperty == EnumUseType.No)
                return false;
            return this.ParentModel.UseDocCodeProperty;
        }
        public bool GetUseDocDateProperty()
        {
            if (this.UseDocDateProperty == EnumUseType.Yes)
                return true;
            if (this.UseDocDateProperty == EnumUseType.No)
                return false;
            return this.ParentModel.UseDocDateProperty;
        }

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicDocumentAccess.ContainsKey(role.Guid));
            return dicDocumentAccess[role.Guid];
        }
        internal Dictionary<string, RoleDocumentAccess> dicDocumentAccess = new Dictionary<string, RoleDocumentAccess>();
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleDocumentAccessSettings)
            {
                this.dicDocumentAccess[tt.Guid] = tt;
            }
        }
        public void InitRoleAdd(IRole role)
        {
            var rca = new RoleDocumentAccess() { Guid = role.Guid };
            this.ListRoleDocumentAccessSettings.Add(rca);
            this.dicDocumentAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(IRole role)
        {
            for (int i = 0; i < this.ListRoleDocumentAccessSettings.Count; i++)
            {
                if (this.ListRoleDocumentAccessSettings[i].Guid == role.Guid)
                {
                    this.ListRoleDocumentAccessSettings.RemoveAt(i);
                    break;
                }
            }
            this.dicDocumentAccess.Remove(role.Guid);
        }
        public EnumDocumentAccess GetRoleDocumentAccess(string roleGuid)
        {
            if (this.dicDocumentAccess.TryGetValue(roleGuid, out var r) && r.EditAccess != EnumDocumentAccess.D_BY_PARENT)
                return r.EditAccess;
            return this.ParentModel.GetRoleDocumentAccess(roleGuid);
        }
        public EnumPrintAccess GetRoleDocumentPrint(string roleGuid)
        {
            if (this.dicDocumentAccess.TryGetValue(roleGuid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentModel.GetRoleDocumentPrint(roleGuid);
        }
        #endregion Roles
    }
}
