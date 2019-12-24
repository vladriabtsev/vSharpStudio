using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public abstract class ModelVisitorBase
    {
        protected virtual void Visit(IConfig c) { }
        protected virtual void Visit(IConfigModel m) { }
        protected virtual void Visit(IEnumerable<IConstant> diff_lst) { }
        protected virtual void Visit(IConstant cn) { }
        protected virtual void Visit(IEnumerable<IEnumeration> diff_lst) { }
        protected virtual void Visit(IEnumeration en) { }
        protected virtual void Visit(IEnumerationPair p) { }
        protected virtual void Visit(IEnumerable<ICatalog> diff_lst) { }
        protected virtual void Visit(ICatalog ct) { }
        protected virtual void Visit(IEnumerable<IDocument> diff_lst) { }
        protected virtual void Visit(IDocument d) { }
        protected virtual void Visit(IEnumerable<vSharpStudio.common.IProperty> diff_lst) { }
        protected virtual void Visit(vSharpStudio.common.IProperty p) { }
        protected virtual void Visit(IEnumerable<IPropertiesTab> diff_lst) { }
        protected virtual void Visit(IPropertiesTab t) { }
        protected virtual void Visit(IEnumerable<IForm> diff_lst) { }
        protected virtual void Visit(IForm p) { }
        protected virtual void Visit(IEnumerable<IReport> diff_lst) { }
        protected virtual void Visit(IReport p) { }
        private void VisitProperties(IEnumerable<vSharpStudio.common.IProperty> lst)
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
        protected vSharpStudio.common.IProperty currProp = null;
        protected Stack<IPropertiesTab> currPropTabStack = new Stack<IPropertiesTab>();

        protected IPropertiesTab currPropTab => this.currPropTabStack.Peek();

        protected void RunThroughConfig(IConfig t)
        {
            this.currCfg = t;
            this.Visit(t);
            var tc = t.GetDiffConstants();
            this.Visit(tc);
            foreach (var tt in tc)
            {
                this.Visit(tt);
            }
            var te = t.GetDiffEnumerations();
            this.Visit(te);
            foreach (var tt in te)
            {
                this.Visit(tt);
                this.currEnum = tt;
                var ttp = tt.GetDiffEnumerationPairs();
                foreach (var ttt in ttp)
                {
                    this.Visit(ttt);
                }
                this.currEnum = null;
            }
            var tdc = t.GetDiffCatalogs();
            this.Visit(tdc);
            foreach (var tt in tdc)
            {
                this.Visit(tt);
                this.currCat = tt;
                this.VisitProperties(tt.GetDiffProperties());
                this.VisitPropertiesTabs(tt.GetDiffPropertiesTabs());
                this.VisitForms(tt.GetDiffForms());
                this.VisitReports(tt.GetDiffReports());
                this.currCat = null;
            }
            var tdd = t.GetDiffDocuments();
            this.Visit(tdd);
            foreach (var tt in tdd)
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
