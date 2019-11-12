using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListForms.Count,nq}")]
    public partial class GroupListForms : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListForms;
        }

        public override bool HasChildren(object parent)
        {
            return this.ListForms.Count > 0;
        }

        partial void OnInit()
        {
            this.Name = "Forms";
            this.IsEditable = false;
        }

        #region Tree operations
        public void AddForm(Form node)
        {
            this.NodeAddNewSubNode(node);
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Form node = null;
            if (node_impl == null)
            {
                node = new Form(this);
            }
            else
            {
                node = (Form)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Form.DefaultName, node, this.ListForms);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        [BrowsableAttribute(false)]
        public List<IForm> ListAnnotated
        {
            get
            {
                var cfg = (Config)this.GetConfig();
                var p = this.Parent;
                while (p.IsIncludableInModels == false)
                {
                    p = p.Parent;
                }

                string par = p.GetType().Name;
                ConfigNodesCollection<Form> curr;
                ConfigNodesCollection<Form> prev;
                ConfigNodesCollection<Form> old;
                switch (par)
                {
                    case "Document":
                        var d = (Document)cfg.DicNodes[p.Guid];
                        curr = d.GroupForms.ListForms;
                        d = (Document)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = d?.GroupForms.ListForms;
                        d = (Document)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = d?.GroupForms.ListForms;
                        break;
                    case "Catalog":
                        var c = (Catalog)cfg.DicNodes[p.Guid];
                        curr = c.GroupForms.ListForms;
                        c = (Catalog)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = c?.GroupForms.ListForms;
                        c = (Catalog)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = c?.GroupForms.ListForms;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                var diff = new DiffLists<IForm>(old, prev, curr);
                return diff.ListAll;
            }
        }
    }
}
