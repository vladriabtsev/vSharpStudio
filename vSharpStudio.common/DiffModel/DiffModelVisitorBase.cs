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

        protected abstract void Visit(IConfig c);
        protected abstract void Visit(IConfigModel m);
        protected abstract void Visit(IEnumerable<IConstant> diff_lst);
        protected abstract void Visit(IConstant cn);
        protected abstract void Visit(IEnumerable<IEnumeration> diff_lst);
        protected abstract void Visit(IEnumeration en);
        protected abstract void Visit(IEnumerationPair p);
        protected abstract void Visit(IEnumerable<ICatalog> diff_lst);
        protected abstract void Visit(ICatalog ct);
        protected abstract void Visit(IEnumerable<IDocument> diff_lst);
        protected abstract void Visit(IDocument d);
        protected abstract void Visit(IEnumerable<IProperty> diff_lst);
        protected abstract void Visit(IProperty p);
        protected abstract void Visit(IEnumerable<IPropertiesTab> diff_lst);
        protected abstract void Visit(IPropertiesTab t);
        protected abstract void Visit(IEnumerable<IForm> diff_lst);
        protected abstract void Visit(IForm p);
        protected abstract void Visit(IEnumerable<IReport> diff_lst);
        protected abstract void Visit(IReport p);

        public void Visit(IConfig cfg, ModelBuilder modelBuilder, bool isCreateGeneralTable = false)
        {
            this.cfg = cfg;
            this.modelBuilder = modelBuilder;
            this.isCreateGeneralTable = isCreateGeneralTable;

            // RunThroughConfig(cfg);
            foreach (var t in cfg.ListAnnotated)
            {
                this.RunThroughConfig(t);
            }
            this.Model = this.modelBuilder.Model;
        }
        private void VisitProperties(IEnumerable<IProperty> lst)
        {
            this.Visit(lst);
            foreach (var t in lst)
            {
                this.currProp = t;
                this.Visit(t);
                this.currProp = null;
            }
        }

        private void VisitPropertiesTabs(IEnumerable<IPropertiesTab> lst)
        {
            this.Visit(lst);
            foreach (var t in lst)
            {
                this.Visit(t);
                this.currPropTabStack.Push(t);
                this.VisitProperties(t.GetDiffProperties());
                this.VisitPropertiesTabs(t.GetDiffPropertiesTabs());
                this.currPropTabStack.Pop();
            }
        }

        private void VisitForms(IEnumerable<IForm> lst)
        {
            this.Visit(lst);
            foreach (var t in lst)
            {
                this.currForm = t;
                this.Visit(t);
                this.currForm = null;
            }
        }

        private void VisitReports(IEnumerable<IReport> lst)
        {
            this.Visit(lst);
            foreach (var t in lst)
            {
                this.currRep = t;
                this.Visit(t);
                this.currRep = null;
            }
        }

        protected IConfig currCfg = null;
        protected IEnumeration currEnum = null;
        protected IForm currForm = null;
        protected IReport currRep = null;
        protected ICatalog currCat = null;
        protected IDocument currDoc = null;
        protected IProperty currProp = null;
        protected Stack<IPropertiesTab> currPropTabStack = new Stack<IPropertiesTab>();

        protected IPropertiesTab currPropTab => this.currPropTabStack.Peek();

        private void RunThroughConfig(IConfig t)
        {
            this.currCfg = t;
            this.Visit(t);
            this.Visit(t.GetDiffConstants());
            foreach (var tt in t.GetDiffConstants())
            {
                this.Visit(tt);
            }
            this.Visit(t.GetDiffEnumerations());
            foreach (var tt in t.GetDiffEnumerations())
            {
                this.Visit(tt);
                this.currEnum = tt;
                foreach (var ttt in tt.GetDiffEnumerationPairs())
                {
                    this.Visit(ttt);
                }
                this.currEnum = null;
            }
            this.Visit(t.GetDiffCatalogs());
            foreach (var tt in t.GetDiffCatalogs())
            {
                this.Visit(tt);
                this.currCat = tt;
                this.VisitProperties(tt.GetDiffProperties());
                this.VisitPropertiesTabs(tt.GetDiffPropertiesTabs());
                this.VisitForms(tt.GetDiffForms());
                this.VisitReports(tt.GetDiffReports());
                this.currCat = null;
            }
            this.Visit(t.GetDiffDocuments());
            foreach (var tt in t.GetDiffDocuments())
            {
                this.Visit(tt);
                this.currDoc = tt;
                this.VisitProperties(this.currCfg.GetDiffDocShared());
                this.VisitProperties(tt.GetDiffProperties());
                this.VisitPropertiesTabs(tt.GetDiffPropertiesTabs());
                this.VisitForms(tt.GetDiffForms());
                this.VisitReports(tt.GetDiffReports());
                this.currDoc = null;
            }
            this.currCfg = null;
        }
    }
}
