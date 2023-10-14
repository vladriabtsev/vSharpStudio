using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListDocuments.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListDocuments : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleAccess
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupDocuments ParentGroupDocuments { get { Debug.Assert(this.Parent != null); return (GroupDocuments)this.Parent; } }
        [Browsable(false)]
        public IGroupDocuments ParentGroupDocumentsI { get { Debug.Assert(this.Parent != null); return (IGroupDocuments)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupDocuments.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<Document> Children { get { return this.ListDocuments; } }
        public IGroupDocuments IParent { get { return this.ParentGroupDocuments; } }

        partial void OnCreated()
        {
            this._Name = "Documents";
            this.IsEditable = false;
            this._ShortIdTypeForCacheKey = "d";
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListDocuments.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDocuments.OnAddedAction = (t) =>
            {
                t.OnAdded();
                t.InitRoles();
            };
            this.ListDocuments.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListDocuments.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public Document AddDocument(string name)
        {
            var node = new Document(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public int IndexOf(IDocument doc)
        {
            return this.ListDocuments.IndexOf((doc as Document)!);
        }
        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Document node = null!;
            if (node_impl == null)
            {
                node = new Document(this);
            }
            else
            {
                node = (Document)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.DocumentName, node, this.ListDocuments);
            }
            var cfg = (Config)this.Cfg;
            node.ShortId = cfg.Model.LastDocumentShortId + 1;
            cfg.Model.LastDocumentShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Description),
                this.GetPropertyName(() => this.Guid),
                this.GetPropertyName(() => this.NameUi),
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicDocumentAccess.ContainsKey(role.Guid))
            {
                var rca = new RoleDocumentAccess() { Guid = role.Guid };
                this.ListRoleDocumentAccessSettings.Add(rca);
                this.dicDocumentAccess[role.Guid] = rca;
            }
            return dicDocumentAccess[role.Guid];
        }
        public void SetRoleAccess(IRole role, EnumDocumentAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicDocumentAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                dicDocumentAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                dicDocumentAccess[role.Guid].PrintAccess = print.Value;
        }
        internal Dictionary<string, RoleDocumentAccess> dicDocumentAccess = new();
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleDocumentAccessSettings)
            {
                this.dicDocumentAccess[tt.Guid] = tt;
            }
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (!this.dicDocumentAccess.ContainsKey(t.Guid))
                {
                    var rca = new RoleDocumentAccess() { Guid = t.Guid };
                    this.dicDocumentAccess[t.Guid] = rca;
                }
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
        public EnumDocumentAccess GetRoleDocumentAccess(IRole role)
        {
            if (this.dicDocumentAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumDocumentAccess.D_BY_PARENT)
                return r.EditAccess;
            return this.ParentGroupDocuments.GetRoleDocumentAccess(role);
        }
        public EnumPrintAccess GetRoleDocumentPrint(IRole role)
        {
            if (this.dicDocumentAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentGroupDocuments.GetRoleDocumentPrint(role);
        }
        #endregion Roles
    }
}
