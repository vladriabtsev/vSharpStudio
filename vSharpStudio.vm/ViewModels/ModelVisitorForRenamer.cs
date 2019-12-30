using System;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ModelVisitorForRenamer : DiffModelVisitorBase
    {
        Config clone;
        public IConfig RunThroughConfig(Config curr, IConfig prev, IConfig old, Action<DiffModelVisitorBase, IObjectAnnotatable> act = null)
        {
            clone = Config.Clone(null, curr);
            base.RunThroughConfig(curr, prev, old, act);
            return clone;
        }
        public List<RenameData> GetListRenameData(IConfig curr, IConfig prev, IConfig old)
        {
            List<RenameData> res = new List<RenameData>();
            this.RunThroughConfig(curr, prev, old, (visitor, obj) =>
            {
                if (obj.IsRenamed())
                {
                    IName curr = (IName)obj;
                    IName prev = (IName)obj[DiffEnumHistoryAnnotation.PrevVersion.ToString()];
                    var rd = new RenameData(prev.Name, curr.Name);
                    res.Add(rd);
                }
            });
            return res;
        }
        protected override void Visit(IEnumeration parent, List<IEnumerationPair> diff_lst)
        {
            List<EnumerationPair> lst = new List<EnumerationPair>();
            foreach (var t in diff_lst)
                lst.Add((EnumerationPair)t);
            var grp = (Enumeration)clone.DicNodes[parent.Guid];
            grp.ListEnumerationPairs.Clear();
            grp.ListEnumerationPairs.AddRange(lst);
        }
        protected override void Visit(List<IEnumeration> diff_lst)
        {
            List<Enumeration> lst = new List<Enumeration>();
            foreach (var t in diff_lst)
                lst.Add((Enumeration)t);
            clone.Model.GroupEnumerations.ListEnumerations.Clear();
            clone.Model.GroupEnumerations.ListEnumerations.AddRange(lst);
        }
        protected override void Visit(List<IConstant> diff_lst)
        {
            List<Constant> lst = new List<Constant>();
            foreach (var t in diff_lst)
                lst.Add((Constant)t);
            clone.Model.GroupConstants.ListConstants.Clear();
            clone.Model.GroupConstants.ListConstants.AddRange(lst);
        }
        protected override void Visit(IGroupListProperties parent, List<IProperty> diff_lst)
        {
            List<Property> lst = new List<Property>();
            foreach (var t in diff_lst)
                lst.Add((Property)t);
            var grp = (GroupListProperties)clone.DicNodes[parent.Guid];
            grp.ListProperties.Clear();
            grp.ListProperties.AddRange(lst);
        }
        protected override void Visit(IGroupListPropertiesTabs parent, List<IPropertiesTab> diff_lst)
        {
            List<PropertiesTab> lst = new List<PropertiesTab>();
            foreach (var t in diff_lst)
                lst.Add((PropertiesTab)t);
            var grp = (GroupListPropertiesTabs)clone.DicNodes[parent.Guid];
            grp.ListPropertiesTabs.Clear();
            grp.ListPropertiesTabs.AddRange(lst);
        }
        protected override void Visit(List<ICatalog> diff_lst)
        {
            List<Catalog> lst = new List<Catalog>();
            foreach (var t in diff_lst)
                lst.Add((Catalog)t);
            clone.Model.GroupCatalogs.ListCatalogs.Clear();
            clone.Model.GroupCatalogs.ListCatalogs.AddRange(lst);
        }
        protected override void Visit(List<IDocument> diff_lst)
        {
            List<Document> lst = new List<Document>();
            foreach (var t in diff_lst)
                lst.Add((Document)t);
            clone.Model.GroupDocuments.GroupListDocuments.ListDocuments.Clear();
            clone.Model.GroupDocuments.GroupListDocuments.ListDocuments.AddRange(lst);
        }
        protected override void Visit(IGroupListForms parent, List<IForm> diff_lst)
        {
            List<Form> lst = new List<Form>();
            foreach (var t in diff_lst)
                lst.Add((Form)t);
            var grp = (GroupListForms)clone.DicNodes[parent.Guid];
            grp.ListForms.Clear();
            grp.ListForms.AddRange(lst);
        }
        protected override void Visit(IGroupListReports parent, List<IReport> diff_lst)
        {
            List<Report> lst = new List<Report>();
            foreach (var t in diff_lst)
                lst.Add((Report)t);
            var grp = (GroupListReports)clone.DicNodes[parent.Guid];
            grp.ListReports.Clear();
            grp.ListReports.AddRange(lst);
        }
    }
    public class RenameData
    {
        public RenameData(string from, string to)
        {
            this.From = from;
            this.To = to;
        }
        public string From { get; private set; }
        public string To { get; private set; }
    }
}
