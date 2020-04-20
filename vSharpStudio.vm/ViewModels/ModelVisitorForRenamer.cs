using System;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ModelVisitorForRenamer : DiffModelVisitorBase
    {
        public List<string> ListGuidsRenamedObjects { get; set; }
        public Config DiffAnnotatedConfig { get; set; }
        public Config RunThroughConfig(Config curr, IConfig prev, IConfig old)
        {
            this.DiffAnnotatedConfig = Config.Clone(null, curr);
            this.ListGuidsRenamedObjects = new List<string>();
            base.RunThroughConfig(curr, prev, old, (visitor, obj) =>
            {
                if (obj is IGuid && obj.IsRenamed())
                {
                    IGuid curr2 = (IGuid)obj;
                    ListGuidsRenamedObjects.Add(curr2.Guid);
                }
            });
            return this.DiffAnnotatedConfig;
        }
        protected override void Visit(IEnumeration parent, List<IEnumerationPair> diff_lst)
        {
            List<EnumerationPair> lst = new List<EnumerationPair>();
            foreach (var t in diff_lst)
                lst.Add((EnumerationPair)t);
            var grp = (Enumeration)DiffAnnotatedConfig.DicNodes[parent.Guid];
            grp.ListEnumerationPairs.Clear();
            grp.ListEnumerationPairs.AddRange(lst);
        }
        protected override void Visit(List<IEnumeration> diff_lst)
        {
            List<Enumeration> lst = new List<Enumeration>();
            foreach (var t in diff_lst)
                lst.Add((Enumeration)t);
            DiffAnnotatedConfig.Model.GroupEnumerations.ListEnumerations.Clear();
            DiffAnnotatedConfig.Model.GroupEnumerations.ListEnumerations.AddRange(lst);
        }
        protected override void Visit(List<IConstant> diff_lst)
        {
            List<Constant> lst = new List<Constant>();
            foreach (var t in diff_lst)
                lst.Add((Constant)t);
            DiffAnnotatedConfig.Model.GroupConstants.ListConstants.Clear();
            DiffAnnotatedConfig.Model.GroupConstants.ListConstants.AddRange(lst);
        }
        protected override void Visit(IGroupListProperties parent, List<IProperty> diff_lst)
        {
            List<Property> lst = new List<Property>();
            foreach (var t in diff_lst)
                lst.Add((Property)t);
            var grp = (GroupListProperties)DiffAnnotatedConfig.DicNodes[parent.Guid];
            grp.ListProperties.Clear();
            grp.ListProperties.AddRange(lst);
        }
        protected override void Visit(IGroupListPropertiesTabs parent, List<IPropertiesTab> diff_lst)
        {
            List<PropertiesTab> lst = new List<PropertiesTab>();
            foreach (var t in diff_lst)
                lst.Add((PropertiesTab)t);
            var grp = (GroupListPropertiesTabs)DiffAnnotatedConfig.DicNodes[parent.Guid];
            grp.ListPropertiesTabs.Clear();
            grp.ListPropertiesTabs.AddRange(lst);
        }
        protected override void Visit(List<ICatalog> diff_lst)
        {
            List<Catalog> lst = new List<Catalog>();
            foreach (var t in diff_lst)
                lst.Add((Catalog)t);
            DiffAnnotatedConfig.Model.GroupCatalogs.ListCatalogs.Clear();
            DiffAnnotatedConfig.Model.GroupCatalogs.ListCatalogs.AddRange(lst);
        }
        protected override void Visit(List<IDocument> diff_lst)
        {
            List<Document> lst = new List<Document>();
            foreach (var t in diff_lst)
                lst.Add((Document)t);
            DiffAnnotatedConfig.Model.GroupDocuments.GroupListDocuments.ListDocuments.Clear();
            DiffAnnotatedConfig.Model.GroupDocuments.GroupListDocuments.ListDocuments.AddRange(lst);
        }
        protected override void Visit(IGroupListForms parent, List<IForm> diff_lst)
        {
            List<Form> lst = new List<Form>();
            foreach (var t in diff_lst)
                lst.Add((Form)t);
            var grp = (GroupListForms)DiffAnnotatedConfig.DicNodes[parent.Guid];
            grp.ListForms.Clear();
            grp.ListForms.AddRange(lst);
        }
        protected override void Visit(IGroupListReports parent, List<IReport> diff_lst)
        {
            List<Report> lst = new List<Report>();
            foreach (var t in diff_lst)
                lst.Add((Report)t);
            var grp = (GroupListReports)DiffAnnotatedConfig.DicNodes[parent.Guid];
            grp.ListReports.Clear();
            grp.ListReports.AddRange(lst);
        }
    }
}
