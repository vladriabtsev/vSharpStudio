using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Serilog.Events;

namespace vSharpStudio.common
{
    public abstract class ModelVisitorBase
    {
        protected virtual void Visit(IEnumerable<IConstant> lst) { }
        protected virtual void Visit(IEnumerable<IEnumeration> lst) { }
        protected virtual void Visit(IEnumeration parent, IEnumerable<IEnumerationPair> lst) { }
        protected virtual void Visit(IEnumerable<ICatalog> lst) { }
        protected virtual void Visit(IEnumerable<IDocument> lst) { }
        protected virtual void Visit(IConfig c) { }
        protected virtual void Visit(IConfigModel m) { }
        protected virtual void Visit(IGroupListCommon cn) { }
        protected virtual void Visit(IGroupListConstants cn) { }
        protected virtual void Visit(IConstant cn) { }
        protected virtual void Visit(IGroupListEnumerations cn) { }
        protected virtual void Visit(IEnumeration en) { }
        protected virtual void Visit(IEnumerationPair p) { }
        protected virtual void Visit(IGroupListCatalogs cn) { }
        protected virtual void Visit(ICatalog ct) { }
        protected virtual void Visit(IGroupDocuments cn) { }
        protected virtual void Visit(IGroupListDocuments cn) { }
        protected virtual void Visit(IDocument d) { }
        protected virtual void Visit(IGroupListProperties cn) { }
        protected virtual void Visit(IGroupListProperties parent, IEnumerable<IProperty> lst) { }
        protected virtual void Visit(IProperty p) { }
        protected virtual void Visit(IGroupListPropertiesTabs cn) { }
        protected virtual void Visit(IGroupListPropertiesTabs parent, IEnumerable<IPropertiesTab> lst) { }
        protected virtual void Visit(IPropertiesTab t) { }
        protected virtual void Visit(IGroupListForms cn) { }
        protected virtual void Visit(IGroupListForms parent, IEnumerable<IForm> diff_lst) { }
        protected virtual void Visit(IForm p) { }
        protected virtual void Visit(IGroupListJournals cn) { }
        protected virtual void Visit(IGroupListJournals parent, IEnumerable<IJournal> diff_lst) { }
        protected virtual void Visit(IJournal cn) { }
        protected virtual void Visit(IGroupListReports cn) { }
        protected virtual void Visit(IGroupListReports parent, IEnumerable<IReport> diff_lst) { }
        protected virtual void Visit(IReport p) { }
        private void VisitProperties(IGroupListProperties parent, IEnumerable<IProperty> lst)
        {
            this.Visit(parent);
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

        private void VisitPropertiesTabs(IGroupListPropertiesTabs parent, IEnumerable<IPropertiesTab> lst)
        {
            this.Visit(parent);
            this.Visit(parent, lst);
            foreach (var t in lst)
            {
                this.Visit(t);
                if (t.IsDeleted())
                    continue;
                this.currPropTabStack.Push(t);
                if (_act != null)
                    _act(this, t);
                this.VisitProperties(t.GroupProperties, t.GroupProperties.ListProperties);
                this.VisitPropertiesTabs(t.GroupPropertiesTabs, t.GroupPropertiesTabs.ListPropertiesTabs);
                this.currPropTabStack.Pop();
            }
        }

        private void VisitForms(IGroupListForms parent, IEnumerable<IForm> lst)
        {
            this.Visit(parent);
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

        private void VisitReports(IGroupListReports parent, IEnumerable<IReport> lst)
        {
            this.Visit(parent);
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
        protected IConfigModel currModel = null;
        protected IEnumeration currEnum = null;
        protected IForm currForm = null;
        protected IReport currRep = null;
        protected ICatalog currCat = null;
        protected IDocument currDoc = null;
        protected vSharpStudio.common.IProperty currProp = null;
        protected Stack<IPropertiesTab> currPropTabStack = new Stack<IPropertiesTab>();
        private Action<ModelVisitorBase, IObjectAnnotatable> _act = null;
        // 0 - previous, 1 - previous of previous
        protected IPropertiesTab GetPropertiesTabFromStack(int level)
        {
            if (this.currPropTabStack.Count < level)
                throw new Exception();
            //return this.currPropTabStack.ToArray()[level];
            return this.currPropTabStack.ToArray()[this.currPropTabStack.Count - level - 1];
        }

        protected IPropertiesTab currPropTab => this.currPropTabStack.Peek();

        public void RunThroughConfig(IConfigModel model, Action<ModelVisitorBase, IObjectAnnotatable> act = null)
        {
            this._act = act;
            this.currModel = model;

            this.Visit(this.currModel);

            #region Common
            this.Visit(currModel.GroupCommon);
            //this.Visit(currModel.GroupCommon.li.ListConstants);
            //foreach (var tt in currModel.GroupConstants.ListConstants)
            //{
            //    this.Visit(tt);
            //    if (_act != null)
            //        _act(this, tt);
            //}
            #endregion Common

            #region Constants
            this.Visit(currModel.GroupConstants);
            this.Visit(currModel.GroupConstants.ListConstants);
            foreach (var tt in currModel.GroupConstants.ListConstants)
            {
                this.Visit(tt);
                if (_act != null)
                    _act(this, tt);
            }
            #endregion Constants

            #region Enumerations
            this.Visit(currModel.GroupEnumerations);
            this.Visit(currModel.GroupEnumerations.ListEnumerations);
            foreach (var tt in currModel.GroupEnumerations.ListEnumerations)
            {
                this.Visit(tt);
                this.currEnum = tt;
                if (_act != null)
                    _act(this, tt);
                if (tt.IsDeleted())
                    continue;
                this.Visit(tt, tt.ListEnumerationPairs);
                foreach (var ttt in tt.ListEnumerationPairs)
                {
                    this.Visit(ttt);
                    if (_act != null)
                        _act(this, ttt);
                }
                this.currEnum = null;
            }
            #endregion Enumerations

            #region Catalogs
            this.Visit(currModel.GroupCatalogs);
            this.Visit(currModel.GroupCatalogs.ListCatalogs);
            foreach (var tt in currModel.GroupCatalogs.ListCatalogs)
            {
                this.Visit(tt);
                this.currCat = tt;
                if (_act != null)
                    _act(this, tt);
                if (tt.IsDeleted())
                    continue;
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                this.VisitPropertiesTabs(tt.GroupPropertiesTabs, tt.GroupPropertiesTabs.ListPropertiesTabs);
                this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                this.currCat = null;
            }
            #endregion Catalogs

            #region Documents
            var sharedProps = currModel.GroupDocuments.GroupSharedProperties.ListProperties;
            this.Visit(currModel.GroupDocuments);
            this.Visit(currModel.GroupDocuments.GroupSharedProperties);
            this.Visit(currModel.GroupDocuments.GroupListDocuments);
            this.Visit(currModel.GroupDocuments.GroupListDocuments.ListDocuments);
            foreach (var tt in currModel.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                this.Visit(tt);
                this.currDoc = tt;
                if (_act != null)
                    _act(this, tt);
                if (tt.IsDeleted())
                    continue;
                this.VisitProperties(currModel.GroupDocuments.GroupSharedProperties, sharedProps);
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                this.VisitPropertiesTabs(tt.GroupPropertiesTabs, tt.GroupPropertiesTabs.ListPropertiesTabs);
                this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                this.currDoc = null;
            }
            #endregion Documents

            #region Journals
            this.Visit(currModel.GroupJournals);
            foreach (var tt in currModel.GroupJournals.ListJournals)
            {
                this.Visit(tt);
                if (_act != null)
                    _act(this, tt);
                if (tt.IsDeleted())
                    continue;
                //this.VisitProperties(currModel.GroupDocuments.GroupSharedProperties, sharedProps);
                //this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                //this.VisitPropertiesTabs(tt.GroupPropertiesTabs, tt.GroupPropertiesTabs.ListPropertiesTabs);
                //this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                //this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                //this.currDoc = null;
            }
            #endregion Journals

            this.currCfg = null;
        }
        /// <summary>
        /// Visit and annotate config nodes.
        /// Create extended config model with deleted nodes.
        /// </summary>
        /// <param name="curr">Current config or clone</param>
        /// <param name="prev">Previous version of config</param>
        /// <param name="old">Oldest version of config</param>
        /// <param name="act"></param>
        /// <returns></returns>
        protected void RunThroughConfig(IConfig curr, Action<ModelVisitorBase, IObjectAnnotatable> act = null)
        {
            this._act = act;
            this.currCfg = curr;

            this.Visit(this.currCfg);

            this.RunThroughConfig(this.currCfg.Model, act);

            this.currCfg = null;
        }
    }
}
