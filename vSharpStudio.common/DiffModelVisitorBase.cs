using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public abstract class DiffModelVisitorBase
    {
        protected virtual void Visit(List<IConfig> diff_lst) { }
        protected virtual void Visit(IConfig c) { }
        protected virtual void Visit(IConfigModel m) { }
        protected virtual void Visit(List<IConstant> diff_lst) { }
        protected virtual void Visit(IConstant cn) { }
        protected virtual void Visit(List<IEnumeration> diff_lst) { }
        protected virtual void Visit(IEnumeration en) { }
        protected virtual void Visit(IEnumeration parent, List<IEnumerationPair> diff_lst) { }
        protected virtual void Visit(IEnumerationPair p) { }
        protected virtual void Visit(List<ICatalog> diff_lst) { }
        protected virtual void Visit(ICatalog ct) { }
        protected virtual void Visit(List<IDocument> diff_lst) { }
        protected virtual void Visit(IDocument d) { }
        protected virtual void Visit(IGroupListProperties parent, List<IProperty> diff_lst) { }
        protected virtual void Visit(IProperty p) { }
        protected virtual void Visit(IGroupListPropertiesTabs parent, List<IPropertiesTab> diff_lst) { }
        protected virtual void Visit(IPropertiesTab t) { }
        protected virtual void Visit(IGroupListForms parent, List<IForm> diff_lst) { }
        protected virtual void Visit(IForm p) { }
        protected virtual void Visit(IGroupListReports parent, List<IReport> diff_lst) { }
        protected virtual void Visit(IReport p) { }
        private void VisitProperties(IGroupListProperties parent, List<vSharpStudio.common.IProperty> lst)
        {
            this.Visit(parent, lst);
            foreach (var t in lst)
            {
                this.currProp = t;
                this.Visit(t);
                if (_act != null)
                    _act(this, t);
                this.currProp = null;
            }
        }

        private void VisitPropertiesTabs(IGroupListPropertiesTabs parent, List<IPropertiesTab> lst)
        {
            this.Visit(parent, lst);
            foreach (var t in lst)
            {
                this.Visit(t);
                if (t.IsDeleted())
                    continue;
                this.currPropTabStack.Push(t);
                if (_act != null)
                    _act(this, t);
                this.VisitProperties(t.GroupProperties, new DiffLists<IProperty>(
                    t.GroupProperties.ListProperties,
                    ((IPropertiesTab)t[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupProperties.ListProperties,
                    ((IPropertiesTab)t[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupProperties.ListProperties
                    ).ListAll);
                this.VisitPropertiesTabs(t.GroupPropertiesTabs, new DiffLists<IPropertiesTab>(
                    t.GroupPropertiesTabs.ListPropertiesTabs,
                    ((IPropertiesTab)t[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupPropertiesTabs.ListPropertiesTabs,
                    ((IPropertiesTab)t[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupPropertiesTabs.ListPropertiesTabs
                    ).ListAll);
                this.currPropTabStack.Pop();
            }
        }

        private void VisitForms(IGroupListForms parent, List<IForm> lst)
        {
            this.Visit(parent, lst);
            foreach (var t in lst)
            {
                this.currForm = t;
                this.Visit(t);
                if (_act != null)
                    _act(this, t);
                this.currForm = null;
            }
        }

        private void VisitReports(IGroupListReports parent, List<IReport> lst)
        {
            this.Visit(parent, lst);
            foreach (var t in lst)
            {
                this.currRep = t;
                this.Visit(t);
                if (_act != null)
                    _act(this, t);
                this.currRep = null;
            }
        }

        protected IConfig currCfg = null;
        protected IConfig prevCfg = null;
        protected IConfig oldCfg = null;
        protected IEnumeration currEnum = null;
        protected IForm currForm = null;
        protected IReport currRep = null;
        protected ICatalog currCat = null;
        protected IDocument currDoc = null;
        protected vSharpStudio.common.IProperty currProp = null;
        protected Stack<IPropertiesTab> currPropTabStack = new Stack<IPropertiesTab>();
        private Action<DiffModelVisitorBase, IObjectAnnotatable> _act = null;

        protected IPropertiesTab currPropTab => this.currPropTabStack.Peek();

        /// <summary>
        /// Visit and annotate config nodes.
        /// Create extended config model with deleted nodes.
        /// </summary>
        /// <param name="curr">Current config or clone</param>
        /// <param name="prev">Previous version of config</param>
        /// <param name="old">Oldest version of config</param>
        /// <param name="act"></param>
        /// <returns></returns>
        protected IConfig RunThroughConfig(IConfig curr, IConfig prev, IConfig old, Action<DiffModelVisitorBase, IObjectAnnotatable> act = null)
        {
            this._act = act;

            var tcfg = new DiffLists<IConfig>(
                curr.GetListConfigs(),
                prev?.GetListConfigs(),
                old?.GetListConfigs()).ListAll;
            this.Visit(tcfg);

            foreach (var tconfig in tcfg)
            {
                this.currCfg = tconfig;
                this.prevCfg = (IConfig)tconfig[DiffEnumHistoryAnnotation.PrevVersion.ToString()];
                this.oldCfg = (IConfig)tconfig[DiffEnumHistoryAnnotation.OldVersion.ToString()];

                this.Visit(this.currCfg);
                this.Visit(this.currCfg.Model);

                #region Constants
                //this.Visit(t.IModel.IGroupConstants, prev.IModel.IGroupConstants);
                var tc = new DiffLists<IConstant>(
                    currCfg.Model.GroupConstants.ListConstants,
                    prevCfg?.Model.GroupConstants.ListConstants,
                    oldCfg?.Model.GroupConstants.ListConstants).ListAll;
                this.Visit(tc);
                foreach (var tt in tc)
                {
                    this.Visit(tt);
                    if (_act != null)
                        _act(this, tt);
                }
                #endregion Constants

                #region Enumerations
                //this.Visit(t.IModel.IGroupEnumerations, prev.IModel.IGroupEnumerations);
                var te = new DiffLists<IEnumeration>(
                    currCfg.Model.GroupEnumerations.ListEnumerations,
                    prevCfg?.Model.GroupEnumerations.ListEnumerations,
                    oldCfg?.Model.GroupEnumerations.ListEnumerations).ListAll;
                this.Visit(te);
                foreach (var tt in te)
                {
                    this.Visit(tt);
                    this.currEnum = tt;
                    if (_act != null)
                        _act(this, tt);
                    if (tt.IsDeleted())
                        continue;
                    var ttp = new DiffLists<IEnumerationPair>(
                        tt.ListEnumerationPairs,
                        ((IEnumeration)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.ListEnumerationPairs,
                        ((IEnumeration)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.ListEnumerationPairs)
                        .ListAll;
                    this.Visit(tt, ttp);
                    foreach (var ttt in ttp)
                    {
                        this.Visit(ttt);
                        if (_act != null)
                            _act(this, ttt);
                    }
                    this.currEnum = null;
                }
                #endregion Enumerations

                #region Catalogs
                var tdc = new DiffLists<ICatalog>(
                    currCfg.Model.GroupCatalogs.ListCatalogs,
                    prevCfg?.Model.GroupCatalogs.ListCatalogs,
                    oldCfg?.Model.GroupCatalogs.ListCatalogs).ListAll;
                this.Visit(tdc);
                foreach (var tt in tdc)
                {
                    this.Visit(tt);
                    this.currCat = tt;
                    if (_act != null)
                        _act(this, tt);
                    if (tt.IsDeleted())
                        continue;
                    this.VisitProperties(tt.GroupProperties, new DiffLists<IProperty>(
                        tt.GroupProperties.ListProperties,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupProperties.ListProperties,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupProperties.ListProperties
                        ).ListAll);
                    this.VisitPropertiesTabs(tt.GroupPropertiesTabs, new DiffLists<IPropertiesTab>(
                        tt.GroupPropertiesTabs.ListPropertiesTabs,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupPropertiesTabs.ListPropertiesTabs,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupPropertiesTabs.ListPropertiesTabs
                        ).ListAll);
                    this.VisitForms(tt.GroupForms, new DiffLists<IForm>(
                        tt.GroupForms.ListForms,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupForms.ListForms,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupForms.ListForms
                        ).ListAll);
                    this.VisitReports(tt.GroupReports, new DiffLists<IReport>(
                        tt.GroupReports.ListReports,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupReports.ListReports,
                        ((ICatalog)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupReports.ListReports
                        ).ListAll);
                    this.currCat = null;
                }
                #endregion Catalogs

                #region Documents
                var sharedProps = new DiffLists<IProperty>(
                    currCfg.Model.GroupDocuments.GroupSharedProperties.ListProperties,
                    prevCfg?.Model.GroupDocuments.GroupSharedProperties.ListProperties,
                    oldCfg?.Model.GroupDocuments.GroupSharedProperties.ListProperties).ListAll;
                var tdd = new DiffLists<IDocument>(
                    currCfg.Model.GroupDocuments.GroupListDocuments.ListDocuments,
                    prevCfg?.Model.GroupDocuments.GroupListDocuments.ListDocuments,
                    oldCfg?.Model.GroupDocuments.GroupListDocuments.ListDocuments).ListAll;
                this.Visit(tdd);
                foreach (var tt in tdd)
                {
                    this.Visit(tt);
                    this.currDoc = tt;
                    if (_act != null)
                        _act(this, tt);
                    if (tt.IsDeleted())
                        continue;
                    this.VisitProperties(currCfg.Model.GroupDocuments.GroupSharedProperties, sharedProps);
                    this.VisitProperties(tt.GroupProperties, new DiffLists<IProperty>(
                        tt.GroupProperties.ListProperties,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupProperties.ListProperties,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupProperties.ListProperties
                        ).ListAll);
                    this.VisitPropertiesTabs(tt.GroupPropertiesTabs, new DiffLists<IPropertiesTab>(
                        tt.GroupPropertiesTabs.ListPropertiesTabs,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupPropertiesTabs.ListPropertiesTabs,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupPropertiesTabs.ListPropertiesTabs
                        ).ListAll);
                    this.VisitForms(tt.GroupForms, new DiffLists<IForm>(
                        tt.GroupForms.ListForms,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupForms.ListForms,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupForms.ListForms
                        ).ListAll);
                    this.VisitReports(tt.GroupReports, new DiffLists<IReport>(
                        tt.GroupReports.ListReports,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.PrevVersion.ToString()])?.GroupReports.ListReports,
                        ((IDocument)tt[DiffEnumHistoryAnnotation.OldVersion.ToString()])?.GroupReports.ListReports
                        ).ListAll);
                    this.currDoc = null;
                }
                #endregion Documents

                this.currCfg = null;
            }
            return curr;
        }
    }
}
