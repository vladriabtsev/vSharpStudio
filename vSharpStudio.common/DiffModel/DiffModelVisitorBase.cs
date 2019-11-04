using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vSharpStudio.common
{
    public abstract class DiffModelVisitorBase
    {
        public IMutableModel Model;
        protected ModelBuilder modelBuilder;
        protected IConfig cfg;
        protected bool isCreateGeneralTable;
        protected abstract void Visit(IEnumeration m, DiffEnumerationType diff_type);
        protected abstract void Visit(IEnumerationPair m, DiffEnumerationPair diff_type);
        protected abstract void Visit(List<IConstant> diff_lst);
        protected abstract void Visit(IConstant m, DiffDataType diff_type);
        protected abstract void Visit(IConfig m, DiffConfig diff);
        protected abstract void Visit(IConfigModel m, DiffConfig diff);
        protected abstract void Visit(ICatalog m, DiffCatalog diff);
        protected abstract void Visit(IDocument m, DiffListProperties diff_shared_prop, DiffDocument diff);
        protected abstract void Visit(IPropertiesTab m, DiffPropertiesTab diff);
        protected abstract void Visit(IProperty m, DiffProperty diff, DiffDataType diff_type);

        private void VisitProperties(DiffListProperties diff)
        {
            foreach (var t in diff.ListAll)
            {
                var diff_property = t.GetDiffProperty();
                var diff_data = t.GetDiffDataType();
                this.Visit(t, diff_property, diff_data);
            }
        }
        private void VisitPropertiesTabs(DiffListPropertiesTabs diff)
        {
            foreach (var t in diff.ListAll)
            {
                var diff_tab = t.GetDiffPropertiesTab();
                this.Visit(t, diff_tab);

                var diff_properties = t.GetDiffListProperties();
                this.VisitProperties(diff_properties);

                var diff_catalog_tabs = t.GetDiffListPropertiesTabs();
                this.VisitPropertiesTabs(diff_catalog_tabs);
            }
        }
        public void Visit(IConfig cfg, ModelBuilder modelBuilder, bool isCreateGeneralTable = true)
        {
            this.cfg = cfg;
            this.modelBuilder = modelBuilder;
            this.isCreateGeneralTable = isCreateGeneralTable;

            //RunThroughConfig(cfg);

            foreach (var t in cfg.ListAnnotated)
            {
                //if (t.IsDeleted()) { }
                //if (t.IsDeprecated()) { }
                //if (t.IsNew()) { }
                //if (t.IsRenamed()) { }
                RunThroughConfig(t);
            }
            this.Model = this.modelBuilder.Model;
        }
        private void RunThroughConfig(IConfig t)
        {
            var diff_config = t.GetDiffConfig();
            this.Visit(t, diff_config);
            foreach (var tt in diff_config.DiffConfigModel.Enumerations.ListAll)
            {
                var diff_type = tt.GetDiffEnumerationType();
                this.Visit(tt, diff_type);

                var diff_elements = tt.GetDiffListEnumElements();
                foreach (var ttt in diff_elements.ListAll)
                {
                    var diff_pair = ttt.GetDiffEnumerationPair();
                    this.Visit(ttt, diff_pair);
                }
            }
            this.Visit(diff_config.DiffConfigModel.Constants.ListAll);
            foreach (var tt in diff_config.DiffConfigModel.Constants.ListAll)
            {
                var diff_type = tt.GetDiffDataType();
                this.Visit(tt, diff_type);
            }
            foreach (var tt in diff_config.DiffConfigModel.Catalogs.ListAll)
            {
                var diff_catalog = tt.GetDiffCatalog();
                this.Visit(tt, diff_catalog);

                var diff_properties = tt.GetDiffListProperties();
                this.VisitProperties(diff_properties);

                var diff_tabs = tt.GetDiffListPropertiesTabs();
                this.VisitPropertiesTabs(diff_tabs);
            }
            foreach (var tt in diff_config.DiffConfigModel.Documents.ListAll)
            {
                var diff_doc = tt.GetDiffDocument();
                this.Visit(tt, diff_config.DiffConfigModel.Documents.DiffSharedProps, diff_doc);

                this.VisitProperties(diff_config.DiffConfigModel.Documents.DiffSharedProps);

                var diff_properties = tt.GetDiffListProperties();
                this.VisitProperties(diff_properties);

                var diff_tabs = tt.GetDiffListPropertiesTabs();
                this.VisitPropertiesTabs(diff_tabs);
            }
        }
    }
}
