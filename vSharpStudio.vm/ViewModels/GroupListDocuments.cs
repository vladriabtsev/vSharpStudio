using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListDocuments.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListDocuments : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
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

        new public ConfigNodesCollection<Document> Children { get { return this.ListDocuments; } }
        public IGroupDocuments IParent { get { return this.ParentGroupDocuments; } }

        partial void OnCreated()
        {
            this._Name = "Documents";
            this.IsEditable = false;
            this.ShortIdTypeForCacheKey = "d";
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
            };
            this.ListDocuments.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListDocuments.OnClearedAction = () => {
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
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }

        #region Roles
        internal Dictionary<string, EnumDocumentAccess> dicDocumentAccess = new Dictionary<string, EnumDocumentAccess>();
        public void InitRoles()
        {
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                bool found = false;
                foreach (var tt in this.ListRoleDocumentAccessSettings)
                {
                    if (tt.Guid == t.Guid)
                    {
                        this.dicDocumentAccess[t.Guid] = tt.EditAccess;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    this.dicDocumentAccess[t.Guid] = EnumDocumentAccess.D_BY_PARENT;
                }
            }
        }
        public void InitRoleAdd(Role role)
        {
            var rca = new RoleDocumentAccess() { Guid = role.Guid };
            this.ListRoleDocumentAccessSettings.Add(rca);
            this.dicDocumentAccess[rca.Guid] = rca.EditAccess;
        }
        public void InitRoleRemove(Role role)
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
            if (this.dicDocumentAccess.TryGetValue(roleGuid, out var r) && r != EnumDocumentAccess.D_BY_PARENT)
                return r;
            return this.ParentGroupDocuments.GetRoleDocumentAccess(roleGuid);
        }
        #endregion Roles
    }
}
