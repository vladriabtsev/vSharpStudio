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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListReports.Count,nq}")]
    public partial class GroupListReports : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public override IEnumerable<object> GetChildren(object parent) { return this.ListReports; }
        public override bool HasChildren(object parent) { return this.ListReports.Count > 0; }
        partial void OnInit()
        {
            this.Name = "Reports";
            this.IsEditable = false;
        }

        #region Tree operations
        public void AddReport(Report node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Report node = null;
            if (node_impl == null)
                node = new Report(this);
            else
                node = (Report)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Report.DefaultName, node, this.ListReports);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations

        [BrowsableAttribute(false)]
        public List<IReport> ListAnnotated
        {
            get
            {
                var cfg = (Config)this.GetConfig();
                var p = this.Parent;
                while (p.IsIncludableInModels == false)
                    p = p.Parent;
                string par = p.GetType().Name;
                ConfigNodesCollection<Report> curr;
                ConfigNodesCollection<Report> prev;
                ConfigNodesCollection<Report> old;
                switch (par)
                {
                    case "Document":
                        var d = (Document)cfg.DicNodes[p.Guid];
                        curr = d.GroupReports.ListReports;
                        d = (Document)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = d?.GroupReports.ListReports;
                        d = (Document)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = d?.GroupReports.ListReports;
                        break;
                    case "Catalog":
                        var c = (Catalog)cfg.DicNodes[p.Guid];
                        curr = c.GroupReports.ListReports;
                        c = (Catalog)cfg.PrevStableConfig?.DicNodes[p.Guid];
                        prev = c?.GroupReports.ListReports;
                        c = (Catalog)cfg.OldStableConfig?.DicNodes[p.Guid];
                        old = c?.GroupReports.ListReports;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                var diff = new DiffLists<IReport>(old, prev, curr);
                return diff.ListAll;
            }
        }
    }
}
