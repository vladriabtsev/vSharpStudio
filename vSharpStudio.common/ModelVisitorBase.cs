using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Serilog.Events;
using System.Net.WebSockets;

namespace vSharpStudio.common
{
    public abstract class ModelVisitorBase
    {
        public static string DB_CONFIG_MODEL_TABLE = "_config_model";
        public static string DB_CONFIG_MODEL_TABLE_FK = "_config_model_fk";
        protected virtual void BeginVisit(IEnumerable<IConstant> lst) { }
        protected virtual void EndVisit(IEnumerable<IConstant> lst) { }
        protected virtual void BeginVisit(IEnumerable<IEnumeration> lst) { }
        protected virtual void EndVisit(IEnumerable<IEnumeration> lst) { }
        protected virtual void BeginVisit(IEnumeration parent, IEnumerable<IEnumerationPair> lst) { }
        protected virtual void EndVisit(IEnumeration parent, IEnumerable<IEnumerationPair> lst) { }
        protected virtual void BeginVisit(IEnumerable<ICatalog> lst) { }
        protected virtual void EndVisit(IEnumerable<ICatalog> lst) { }
        protected virtual void BeginVisit(IEnumerable<IDocument> lst) { }
        protected virtual void EndVisit(IEnumerable<IDocument> lst) { }
        protected virtual void BeginVisit(IConfig c) { }
        protected virtual void EndVisit(IConfig c) { }
        protected virtual void BeginVisit(IConfigModel m) { }
        protected virtual void EndVisit(IConfigModel m) { }
        protected virtual void BeginVisit(IGroupListCommon cn) { }
        protected virtual void EndVisit(IGroupListCommon cn) { }
        protected virtual void BeginVisit(IGroupListConstants cn) { }
        protected virtual void EndVisit(IGroupListConstants cn) { }
        protected virtual void BeginVisit(IConstant cn) { }
        protected virtual void EndVisit(IConstant cn) { }
        protected virtual void BeginVisit(IGroupListEnumerations cn) { }
        protected virtual void EndVisit(IGroupListEnumerations cn) { }
        protected virtual void BeginVisit(IEnumeration en) { }
        protected virtual void EndVisit(IEnumeration en) { }
        protected virtual void BeginVisit(IEnumerationPair p) { }
        protected virtual void EndVisit(IEnumerationPair p) { }
        protected virtual void BeginVisit(IGroupListCatalogs cn) { }
        protected virtual void EndVisit(IGroupListCatalogs cn) { }
        protected virtual void BeginVisit(ICatalog ct) { }
        protected virtual void EndVisit(ICatalog ct) { }
        protected virtual void BeginVisit(IGroupDocuments cn) { }
        protected virtual void EndVisit(IGroupDocuments cn) { }
        protected virtual void BeginVisit(IGroupListDocuments cn) { }
        protected virtual void EndVisit(IGroupListDocuments cn) { }
        protected virtual void BeginVisit(IDocument d) { }
        protected virtual void EndVisit(IDocument d) { }
        protected virtual void BeginVisit(IGroupListProperties cn) { }
        protected virtual void EndVisit(IGroupListProperties cn) { }
        protected virtual void BeginVisit(IGroupListProperties parent, IEnumerable<IProperty> lst) { }
        protected virtual void EndVisit(IGroupListProperties parent, IEnumerable<IProperty> lst) { }
        protected virtual void BeginVisit(IProperty p) { }
        protected virtual void EndVisit(IProperty p) { }
        protected virtual void BeginVisit(IGroupListPropertiesTabs cn) { }
        protected virtual void EndVisit(IGroupListPropertiesTabs cn) { }
        protected virtual void BeginVisit(IGroupListPropertiesTabs parent, IEnumerable<IPropertiesTab> lst) { }
        protected virtual void EndVisit(IGroupListPropertiesTabs parent, IEnumerable<IPropertiesTab> lst) { }
        protected virtual void BeginVisit(IPropertiesTab t) { }
        protected virtual void EndVisit(IPropertiesTab t) { }
        protected virtual void BeginVisit(IGroupListForms cn) { }
        protected virtual void EndVisit(IGroupListForms cn) { }
        protected virtual void BeginVisit(IGroupListForms parent, IEnumerable<IForm> diff_lst) { }
        protected virtual void EndVisit(IGroupListForms parent, IEnumerable<IForm> diff_lst) { }
        protected virtual void BeginVisit(IForm p) { }
        protected virtual void EndVisit(IForm p) { }
        protected virtual void BeginVisit(IGroupListJournals cn) { }
        protected virtual void EndVisit(IGroupListJournals cn) { }
        protected virtual void BeginVisit(IGroupListJournals parent, IEnumerable<IJournal> diff_lst) { }
        protected virtual void EndVisit(IGroupListJournals parent, IEnumerable<IJournal> diff_lst) { }
        protected virtual void BeginVisit(IJournal cn) { }
        protected virtual void EndVisit(IJournal cn) { }
        protected virtual void BeginVisit(IGroupListReports cn) { }
        protected virtual void EndVisit(IGroupListReports cn) { }
        protected virtual void BeginVisit(IGroupListReports parent, IEnumerable<IReport> diff_lst) { }
        protected virtual void EndVisit(IGroupListReports parent, IEnumerable<IReport> diff_lst) { }
        protected virtual void BeginVisit(IReport p) { }
        protected virtual void EndVisit(IReport p) { }
        private void VisitProperties(IGroupListProperties parent, IEnumerable<IProperty> lst)
        {
            this.BeginVisit(parent);
            this.BeginVisit(parent, lst);
            foreach (var t in lst)
            {
                this.currProp = t;
                this.BeginVisit(t);
                if (_act != null)
                    _act(this, t);
                this.EndVisit(t);
                this.currProp = null;
            }
            this.EndVisit(parent, lst);
            this.EndVisit(parent);
        }

        private void VisitPropertiesTabs(IGroupListPropertiesTabs parent, IEnumerable<IPropertiesTab> lst)
        {
            this.BeginVisit(parent);
            this.BeginVisit(parent, lst);
            foreach (var t in lst)
            {
                this.BeginVisit(t);
                //if (t.IsDeleted())
                //    continue;
                this.currPropTabStack.Push(t);
                if (_act != null)
                    _act(this, t);
                this.VisitProperties(t.GroupProperties, t.GroupProperties.ListProperties);
                this.VisitPropertiesTabs(t.GroupPropertiesTabs, t.GroupPropertiesTabs.ListPropertiesTabs);
                this.currPropTabStack.Pop();
                this.EndVisit(t);
            }
            this.EndVisit(parent, lst);
            this.EndVisit(parent);
        }

        private void VisitForms(IGroupListForms parent, IEnumerable<IForm> lst)
        {
            this.BeginVisit(parent);
            this.BeginVisit(parent, lst);
            foreach (var t in lst)
            {
                this.currForm = t;
                this.BeginVisit(t);
                if (_act != null)
                    _act(this, t);
                this.EndVisit(t);
                this.currForm = null;
            }
            this.EndVisit(parent, lst);
            this.EndVisit(parent);
        }

        private void VisitReports(IGroupListReports parent, IEnumerable<IReport> lst)
        {
            this.BeginVisit(parent);
            this.BeginVisit(parent, lst);
            foreach (var t in lst)
            {
                this.currRep = t;
                this.BeginVisit(t);
                if (_act != null)
                    _act(this, t);
                this.EndVisit(t);
                this.currRep = null;
            }
            this.EndVisit(parent, lst);
            this.EndVisit(parent);
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

        /// <summary>
        /// Model object references
        /// </summary>
        public class ModelNode
        {
            public ModelNode()
            {
                this.DicReferenceToNodes = new Dictionary<string, ReferenceToNode>();
                this.DicReferecedFromNodes = new Dictionary<string, IGuid>();
            }
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid NodeObject { get; set; }
            /// <summary>
            /// References from this model objects to others
            /// </summary>
            public Dictionary<string, ReferenceToNode> DicReferenceToNodes { get; set; }
            /// <summary>
            /// References from other model objects to this model object
            /// </summary>
            public Dictionary<string, IGuid> DicReferecedFromNodes { get; set; }
        }
        /// <summary>
        /// Reference to another model object
        /// </summary>
        public class ReferenceToNode
        {
            public ReferenceToNode() { this.DicReferencedByField = new Dictionary<string, IGuid>(); }
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid ReferencedObject { get; set; }
            /// <summary>
            /// Model object field which is referencing 
            /// </summary>
            public Dictionary<string, IGuid> DicReferencedByField { get; set; }
        }
        protected Dictionary<string, ModelNode> DicNodesWithReferences = new Dictionary<string, ModelNode>();
        //private List<IGuid> GrapfToSequenceForDb()
        private void ScanForDicNodesWithReferences()
        {
            foreach (var t in currModel.GroupConstants.ListConstants)
            {
                var md = new ModelNode() { NodeObject = t };
                DicNodesWithReferences[t.Guid] = md;
                if (!string.IsNullOrWhiteSpace(t.DataType.ObjectGuid))
                {
                    AddReferenceToNode(md, t, t.DataType);
                }
            }
            foreach (var t in currModel.GroupCatalogs.ListCatalogs)
            {
                var md = new ModelNode() { NodeObject = t };
                DicNodesWithReferences[t.Guid] = md;
                ScanPropertiesTabs(md, t.GroupPropertiesTabs);
            }
            foreach (var t in currModel.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                var md = new ModelNode() { NodeObject = t };
                DicNodesWithReferences[t.Guid] = md;
                ScanProperties(md, currModel.GroupDocuments.GroupSharedProperties.ListProperties);
                ScanPropertiesTabs(md, t.GroupPropertiesTabs);
            }

            foreach (var t in DicNodesWithReferences)
            {
                foreach (var tt in t.Value.DicReferenceToNodes)
                {
                    tt.Value.ReferencedObject = DicNodesWithReferences[tt.Key].NodeObject;
                    DicNodesWithReferences[tt.Key].DicReferecedFromNodes[t.Key] = t.Value.NodeObject;
                }
            }
            //var res = new List<IGuid>();
            //var dic = new Dictionary<string, IGuid>();
            //int prev_cnt = -1;
            //while (dicNodesWithReferences.Count > 0)
            //{
            //    if (dicNodesWithReferences.Count == prev_cnt)
            //        throw new Exception("Can't create sequence for graph");
            //    prev_cnt = dicNodesWithReferences.Count;
            //    var lst = dicNodesWithReferences.ToList();
            //    foreach (var t in lst)
            //    {
            //        bool skip_t = false;
            //        foreach(var tt in t.Value.DicReferenceToNodes)
            //        {
            //            if (!dic.ContainsKey(tt.Key))
            //            {
            //                skip_t = true;
            //                break;
            //            }
            //        }
            //        if (skip_t)
            //            continue;
            //        res.Add(t.Value.NodeObject);
            //        dicNodesWithReferences.Remove(t.Key);
            //    }
            //}
            //return res;
        }

        private static void ScanPropertiesTabs(ModelNode md, IGroupListPropertiesTabs t)
        {
            foreach (var tt in t.ListPropertiesTabs)
            {
                ScanProperties(md, tt.GroupProperties.ListProperties);
                ScanPropertiesTabs(md, tt.GroupPropertiesTabs);
            }
        }

        private static void ScanProperties(ModelNode md, IEnumerable<IProperty> lst)
        {
            foreach (var t in lst)
            {
                if (!string.IsNullOrWhiteSpace(t.DataType.ObjectGuid))
                {
                    AddReferenceToNode(md, t, t.DataType);
                }
            }
        }

        private static void AddReferenceToNode(ModelNode md, IGuid t, IDataType d)
        {
            if (!md.DicReferenceToNodes.ContainsKey(d.ObjectGuid))
            {
                md.DicReferenceToNodes[d.ObjectGuid] = new ReferenceToNode();
            }
            var tn = md.DicReferenceToNodes[d.ObjectGuid];
            tn.DicReferencedByField[t.Guid] = t;
        }
        public void RunThroughConfig(IConfigModel model, Action<ModelVisitorBase, IObjectAnnotatable> act = null)
        {
            this._act = act;
            this.currModel = model;
            this.ScanForDicNodesWithReferences();

            this.BeginVisit(this.currModel);

            //TODO change visiting to visit object with references to other objects after visiting referenced objects

            #region Common
            this.BeginVisit(currModel.GroupCommon);
            //this.Visit(currModel.GroupCommon.li.ListConstants);
            //foreach (var tt in currModel.GroupConstants.ListConstants)
            //{
            //    this.Visit(tt);
            //    if (_act != null)
            //        _act(this, tt);
            //}
            this.EndVisit(currModel.GroupCommon);
            #endregion Common

            #region Constants
            this.BeginVisit(currModel.GroupConstants);
            this.BeginVisit(currModel.GroupConstants.ListConstants);
            foreach (var tt in currModel.GroupConstants.ListConstants)
            {
                this.BeginVisit(tt);
                if (_act != null)
                    _act(this, tt);
                this.EndVisit(tt);
            }
            this.EndVisit(currModel.GroupConstants.ListConstants);
            this.EndVisit(currModel.GroupConstants);
            #endregion Constants

            #region Enumerations
            this.BeginVisit(currModel.GroupEnumerations);
            this.BeginVisit(currModel.GroupEnumerations.ListEnumerations);
            foreach (var tt in currModel.GroupEnumerations.ListEnumerations)
            {
                this.BeginVisit(tt);
                this.currEnum = tt;
                if (_act != null)
                    _act(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                this.BeginVisit(tt, tt.ListEnumerationPairs);
                foreach (var ttt in tt.ListEnumerationPairs)
                {
                    this.BeginVisit(ttt);
                    if (_act != null)
                        _act(this, ttt);
                    this.EndVisit(ttt);
                }
                this.EndVisit(tt, tt.ListEnumerationPairs);
                this.EndVisit(tt);
                this.currEnum = null;
            }
            this.EndVisit(currModel.GroupEnumerations.ListEnumerations);
            this.EndVisit(currModel.GroupEnumerations);
            #endregion Enumerations

            #region Catalogs
            this.BeginVisit(currModel.GroupCatalogs);
            this.BeginVisit(currModel.GroupCatalogs.ListCatalogs);
            foreach (var tt in currModel.GroupCatalogs.ListCatalogs)
            {
                this.BeginVisit(tt);
                this.currCat = tt;
                if (_act != null)
                    _act(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                this.VisitPropertiesTabs(tt.GroupPropertiesTabs, tt.GroupPropertiesTabs.ListPropertiesTabs);
                this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                this.EndVisit(tt);
                this.currCat = null;
            }
            this.EndVisit(currModel.GroupCatalogs.ListCatalogs);
            this.EndVisit(currModel.GroupCatalogs);
            #endregion Catalogs

            #region Documents
            var sharedProps = currModel.GroupDocuments.GroupSharedProperties.ListProperties;
            this.BeginVisit(currModel.GroupDocuments);
            this.BeginVisit(currModel.GroupDocuments.GroupSharedProperties);
            this.BeginVisit(currModel.GroupDocuments.GroupListDocuments);
            this.BeginVisit(currModel.GroupDocuments.GroupListDocuments.ListDocuments);
            foreach (var tt in currModel.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                this.BeginVisit(tt);
                this.currDoc = tt;
                if (_act != null)
                    _act(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                this.VisitProperties(currModel.GroupDocuments.GroupSharedProperties, sharedProps);
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                this.VisitPropertiesTabs(tt.GroupPropertiesTabs, tt.GroupPropertiesTabs.ListPropertiesTabs);
                this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                this.EndVisit(tt);
                this.currDoc = null;
            }
            this.EndVisit(currModel.GroupDocuments.GroupListDocuments.ListDocuments);
            this.EndVisit(currModel.GroupDocuments.GroupListDocuments);
            this.EndVisit(currModel.GroupDocuments.GroupSharedProperties);
            this.EndVisit(currModel.GroupDocuments);
            #endregion Documents

            #region Journals
            this.BeginVisit(currModel.GroupJournals);
            foreach (var tt in currModel.GroupJournals.ListJournals)
            {
                this.BeginVisit(tt);
                if (_act != null)
                    _act(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                //this.VisitProperties(currModel.GroupDocuments.GroupSharedProperties, sharedProps);
                //this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                //this.VisitPropertiesTabs(tt.GroupPropertiesTabs, tt.GroupPropertiesTabs.ListPropertiesTabs);
                //this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                //this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                //this.currDoc = null;
                this.EndVisit(tt);
            }
            this.EndVisit(currModel.GroupJournals);
            #endregion Journals

            this.EndVisit(this.currModel);

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

            this.BeginVisit(this.currCfg);

            this.RunThroughConfig(this.currCfg.Model, act);

            this.EndVisit(this.currCfg);

            this.currCfg = null;
        }
    }
}
