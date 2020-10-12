using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ModelVisitorForAnnotation : DiffModelVisitorBase
    {
        public List<string> ListGuidsRenamedObjects { get; private set; }
        public Config DiffAnnotatedConfig { get; set; }
        public Config GetDiffAnnotatedConfig(Config curr, IConfig prev, IConfig old)
        {
            this.DiffAnnotatedConfig = Config.Clone(null, curr);
            foreach(var t in curr.DicActiveAppProjectGenerators)
            {
                this.DiffAnnotatedConfig.DicActiveAppProjectGenerators[t.Key] = t.Value;
            }
            foreach (var t in curr.DicGenNodeSettings)
            {
                this.DiffAnnotatedConfig.DicGenNodeSettings[t.Key] = t.Value;
            }
            foreach (var t in curr.DicNodes)
            {
                this.DiffAnnotatedConfig.DicNodes[t.Key] = t.Value;
            }
            this.DiffAnnotatedConfig.DicGenerators = curr.DicGenerators;
            this.DiffAnnotatedConfig.DicPluginLists = curr.DicPluginLists;
            this.DiffAnnotatedConfig.DicPlugins = curr.DicPlugins;
            this.DiffAnnotatedConfig.AddAllAppGenSettingsVmsToNode();


            this.ListGuidsRenamedObjects = new List<string>();
            base.RunThroughConfig(curr, prev, old, (visitor, obj) =>
            {
                if (obj is IGuid)
                {
                    IGuid curr2 = (IGuid)obj;
                    if (obj.IsRenamed())
                        ListGuidsRenamedObjects.Add(curr2.Guid);
                }
            });
            foreach (var t in curr.Model.DicGenNodeSettings)
                this.DiffAnnotatedConfig.Model.DicGenNodeSettings[t.Key] = t.Value;

            return this.DiffAnnotatedConfig;
        }
        protected override void Visit(IEnumeration parent, List<IEnumerationPair> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            Contract.Requires(parent != null);
            List<EnumerationPair> lst = new List<EnumerationPair>();
            foreach (var t in diff_lst)
                lst.Add((EnumerationPair)t);
            var grp = (Enumeration)parent;
            grp.ListEnumerationPairs.Clear();
            grp.ListEnumerationPairs.AddRange(lst);
        }
        protected override void Visit(List<IEnumeration> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            List<Enumeration> lst = new List<Enumeration>();
            foreach (var t in diff_lst)
                lst.Add((Enumeration)t);
            DiffAnnotatedConfig.Model.GroupEnumerations.ListEnumerations.Clear();
            DiffAnnotatedConfig.Model.GroupEnumerations.ListEnumerations.AddRange(lst);
        }
        protected override void Visit(List<IConstant> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            List<Constant> lst = new List<Constant>();
            foreach (var t in diff_lst)
                lst.Add((Constant)t);
            DiffAnnotatedConfig.Model.GroupConstants.ListConstants.Clear();
            DiffAnnotatedConfig.Model.GroupConstants.ListConstants.AddRange(lst);
        }
        protected override void Visit(IGroupListProperties parent, List<IProperty> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            Contract.Requires(parent != null);
            List<Property> lst = new List<Property>();
            foreach (var t in diff_lst)
                lst.Add((Property)t);
            var grp = (GroupListProperties)parent;
            grp.ListProperties.Clear();
            grp.ListProperties.AddRange(lst);
        }
        protected override void Visit(IGroupListPropertiesTabs parent, List<IPropertiesTab> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            Contract.Requires(parent != null);
            List<PropertiesTab> lst = new List<PropertiesTab>();
            foreach (var t in diff_lst)
                lst.Add((PropertiesTab)t);
            var grp = (GroupListPropertiesTabs)parent;
            grp.ListPropertiesTabs.Clear();
            grp.ListPropertiesTabs.AddRange(lst);
        }
        protected override void Visit(List<ICatalog> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            List<Catalog> lst = new List<Catalog>();
            foreach (var t in diff_lst)
                lst.Add((Catalog)t);
            DiffAnnotatedConfig.Model.GroupCatalogs.ListCatalogs.Clear();
            DiffAnnotatedConfig.Model.GroupCatalogs.ListCatalogs.AddRange(lst);
        }
        protected override void Visit(List<IDocument> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            List<Document> lst = new List<Document>();
            foreach (var t in diff_lst)
                lst.Add((Document)t);
            DiffAnnotatedConfig.Model.GroupDocuments.GroupListDocuments.ListDocuments.Clear();
            DiffAnnotatedConfig.Model.GroupDocuments.GroupListDocuments.ListDocuments.AddRange(lst);
        }
        protected override void Visit(IGroupListForms parent, List<IForm> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            Contract.Requires(parent != null);
            List<Form> lst = new List<Form>();
            foreach (var t in diff_lst)
                lst.Add((Form)t);
            var grp = (GroupListForms)parent;
            grp.ListForms.Clear();
            grp.ListForms.AddRange(lst);
        }
        protected override void Visit(IGroupListReports parent, List<IReport> diff_lst)
        {
            Contract.Requires(diff_lst != null);
            Contract.Requires(parent != null);
            List<Report> lst = new List<Report>();
            foreach (var t in diff_lst)
                lst.Add((Report)t);
            var grp = (GroupListReports)parent;
            grp.ListReports.Clear();
            grp.ListReports.AddRange(lst);
        }
    }
}
