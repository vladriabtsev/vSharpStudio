using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Serilog.Events;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace vSharpStudio.common
{
    /// <summary>
    /// Base class for visiting all config nodes
    /// </summary>
    public class ModelVisitorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="act"></param>
        public void Run(IModel model, bool isActFromRootToBottom = true, Action<ModelVisitorBase, ITreeConfigNode>? act = null)
        {
            this._act = act;
            this.currModel = model;
            this.currCfg = model.ParentConfigI;
            this.BeginVisit(this.currModel);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel);

            //TODO change visiting to visit object with references to other objects after visiting referenced objects

            #region Common
            this.BeginVisit(this.currModel.GroupCommon);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon);
            this.BeginVisit(currModel.GroupCommon.GroupRoles);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon.GroupRoles);
            foreach (var tt in currModel.GroupCommon.GroupRoles.ListRoles)
            {
                this.BeginVisit(tt);
                this._act?.Invoke(this, tt);
                this.EndVisit(tt);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon.GroupRoles);
            this.EndVisit(currModel.GroupCommon.GroupRoles);
            this.BeginVisit(currModel.GroupCommon.GroupViewForms);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon.GroupViewForms);
            foreach (var tt in currModel.GroupCommon.GroupViewForms.ListMainViewForms)
            {
                this.BeginVisit(tt);
                this._act?.Invoke(this, tt);
                this.EndVisit(tt);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon.GroupViewForms);
            this.EndVisit(currModel.GroupCommon.GroupViewForms);
            this.BeginVisit(currModel.GroupCommon.GroupListSequences);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon.GroupListSequences);
            foreach (var tt in currModel.GroupCommon.GroupListSequences.ListEnumeratorSequences)
            {
                this.BeginVisit(tt);
                this._act?.Invoke(this, tt);
                this.EndVisit(tt);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon.GroupListSequences);
            this.EndVisit(currModel.GroupCommon.GroupListSequences);
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCommon);
            this.EndVisit(currModel.GroupCommon);
            #endregion Common

            #region Constants
            this.BeginVisit(currModel.GroupConstantGroups);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupConstantGroups);
            foreach (var t in currModel.GroupConstantGroups.ListConstantGroups)
            {
                this.BeginVisit(t);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, t);
                foreach (var tt in t.ListConstants)
                {
                    this.BeginVisit(tt);
                    this._act?.Invoke(this, tt);
                    this.EndVisit(tt);
                }
                if (!isActFromRootToBottom)
                    this._act?.Invoke(this, t);
                this.EndVisit(t);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupConstantGroups);
            this.EndVisit(currModel.GroupConstantGroups);
            #endregion Constants

            #region Enumerations
            this.BeginVisit(currModel.GroupEnumerations);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupEnumerations);
            this.BeginVisit(currModel.GroupEnumerations.ListEnumerations);
            foreach (var tt in currModel.GroupEnumerations.ListEnumerations)
            {
                this.currEnum = tt;
                this.BeginVisit(tt);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                foreach (var ttt in tt.ListEnumerationPairs)
                {
                    this.BeginVisit(ttt);
                    this._act?.Invoke(this, ttt);
                    this.EndVisit(ttt);
                }
                if (!isActFromRootToBottom)
                    this._act?.Invoke(this, tt);
                this.EndVisit(tt);
                this.currEnum = null;
            }
            this.EndVisit(currModel.GroupEnumerations.ListEnumerations);
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupEnumerations);
            this.EndVisit(currModel.GroupEnumerations);
            #endregion Enumerations

            #region Catalogs
            this.BeginVisit(currModel.GroupCatalogs);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCatalogs);
            this.BeginVisit(currModel.GroupCatalogs.ListCatalogs);
            foreach (var tt in currModel.GroupCatalogs.ListCatalogs)
            {
                this.BeginVisit(tt);
                this.currCat = tt;
                this._act?.Invoke(this, tt);
                this.BeginVisit(tt.Folder);
                this._act?.Invoke(this, tt.Folder);
                this.VisitProperties(tt.Folder.GroupProperties, tt.Folder.GroupProperties.ListProperties, isActFromRootToBottom);
                this.VisitDetails(tt.Folder.GroupDetails, tt.Folder.GroupDetails.ListDetails, isActFromRootToBottom);
                this.EndVisit(tt.Folder);
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties, isActFromRootToBottom);
                this.VisitDetails(tt.GroupDetails, tt.GroupDetails.ListDetails, isActFromRootToBottom);
                this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms, isActFromRootToBottom);
                this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports, isActFromRootToBottom);
                this.EndVisit(tt);
                this.currCat = null;
            }
            this.EndVisit(currModel.GroupCatalogs.ListCatalogs);
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupCatalogs);
            this.EndVisit(currModel.GroupCatalogs);
            #endregion Catalogs

            #region ManyToMany
            this.BeginVisit(currModel.GroupRelations);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRelations);
            this.BeginVisit(currModel.GroupRelations.GroupListCatalogsRelations);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRelations.GroupListCatalogsRelations);
            foreach (var tr in currModel.GroupRelations.GroupListCatalogsRelations.ListCatalogsRelations)
            {
                this.currCatRelation = tr;
                this.BeginVisit(tr);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, tr);
                if (!isActFromRootToBottom)
                    this._act?.Invoke(this, tr);
                this.EndVisit(tr);
                this.currReg = null;
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRelations.GroupListCatalogsRelations);
            this.BeginVisit(currModel.GroupRelations.GroupListDocumentsRelations);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRelations.GroupListDocumentsRelations);
            foreach (var tr in currModel.GroupRelations.GroupListDocumentsRelations.ListDocumentsRelations)
            {
                this.currDocRelation = tr;
                this.BeginVisit(tr);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, tr);
                if (!isActFromRootToBottom)
                    this._act?.Invoke(this, tr);
                this.EndVisit(tr);
                this.currReg = null;
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRelations.GroupListDocumentsRelations);
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRelations);
            this.EndVisit(currModel.GroupRelations);
            #endregion ManyToMany

            #region Documents
            this.BeginVisit(currModel.GroupDocuments);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupDocuments);
            this.VisitProperties(currModel.GroupDocuments.GroupSharedProperties, currModel.GroupDocuments.GroupSharedProperties.ListProperties, isActFromRootToBottom);
            this.BeginVisit(currModel.GroupDocuments.GroupListDocuments);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupDocuments.GroupListDocuments);
            this.BeginVisit(currModel.GroupDocuments.GroupListDocuments.ListDocuments);
            foreach (var tt in currModel.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                this.BeginVisit(tt);
                this.currDoc = tt;
                this._act?.Invoke(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties, isActFromRootToBottom);
                this.VisitDetails(tt.GroupDetails, tt.GroupDetails.ListDetails, isActFromRootToBottom);
                this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms, isActFromRootToBottom);
                this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports, isActFromRootToBottom);
                this.EndVisit(tt);
                this.currDoc = null;
            }
            this.EndVisit(currModel.GroupDocuments.GroupListDocuments.ListDocuments);
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupDocuments.GroupListDocuments);
            this.EndVisit(currModel.GroupDocuments.GroupListDocuments);
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupDocuments);
            this.EndVisit(currModel.GroupDocuments);
            #endregion Documents

            #region Registers
            this.BeginVisit(currModel.GroupRegisters);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRegisters);
            foreach (var tr in currModel.GroupRegisters.ListRegisters)
            {
                this.currReg = tr;
                this.BeginVisit(tr);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, tr);
                this.BeginVisit(tr.GroupRegisterDimensions);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, tr.GroupRegisterDimensions);
                foreach (var td in tr.GroupRegisterDimensions.ListDimensions)
                {
                    this.BeginVisit(td);
                    this._act?.Invoke(this, td);
                    this.EndVisit(td);
                }
                if (!isActFromRootToBottom)
                    this._act?.Invoke(this, tr.GroupRegisterDimensions);
                this.EndVisit(tr.GroupRegisterDimensions);
                this.VisitProperties(tr.GroupProperties, tr.GroupProperties.ListProperties, isActFromRootToBottom);
                this.VisitForms(tr.GroupForms, tr.GroupForms.ListForms, isActFromRootToBottom);
                this.VisitReports(tr.GroupReports, tr.GroupReports.ListReports, isActFromRootToBottom);
                this.EndVisit(tr);
                this.currReg = null;
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupRegisters);
            this.EndVisit(currModel.GroupRegisters);
            #endregion Registers

            #region Journals
            this.BeginVisit(currModel.GroupJournals);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupJournals);
            foreach (var tt in currModel.GroupJournals.ListJournals)
            {
                this.BeginVisit(tt);
                this._act?.Invoke(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                //this.VisitProperties(currModel.GroupDocuments.GroupSharedProperties, sharedProps);
                //this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                //this.VisitDetails(tt.GroupDetails, tt.GroupDetails.ListDetails);
                //this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                //this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                //this.currDoc = null;
                this.EndVisit(tt);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel.GroupJournals);
            this.EndVisit(currModel.GroupJournals);
            #endregion Journals

            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currModel);
            this.EndVisit(this.currModel);
        }
        /// <summary>
        /// Visit all config nodes.
        /// </summary>
        /// <param name="curr">Config to visit</param>
        /// <param name="sln"></param>
        /// <param name="prj"></param>
        /// <param name="prjGen"></param>
        /// <param name="act">Action for each node</param>
        public void RunFromRoot(IConfig curr, IAppSolution? sln, IAppProject? prj, IAppProjectGenerator? prjGen, Action<ModelVisitorBase, ITreeConfigNode>? act)
        {
            this.Run(curr, sln, prj, prjGen, true, act);
        }
        /// <summary>
        /// Visit all config nodes.
        /// </summary>
        /// <param name="curr">Config to visit</param>
        /// <param name="sln"></param>
        /// <param name="prj"></param>
        /// <param name="prjGen"></param>
        /// <param name="act">Action for each node</param>
        public void RunToRoot(IConfig curr, IAppSolution? sln, IAppProject? prj, IAppProjectGenerator? prjGen, Action<ModelVisitorBase, ITreeConfigNode>? act)
        {
            this.Run(curr, sln, prj, prjGen, false, act);
        }
        /// <summary>
        /// Visit all config nodes.
        /// </summary>
        /// <param name="curr">Config to visit</param>
        /// <param name="sln"></param>
        /// <param name="prj"></param>
        /// <param name="prjGen"></param>
        /// <param name="act">Action for each node</param>
        protected void Run(IConfig curr, IAppSolution? sln, IAppProject? prj, IAppProjectGenerator? prjGen, bool isActFromRootToBottom = true, Action<ModelVisitorBase, ITreeConfigNode>? act = null)
        {
            this._act = act;
            this.currCfg = curr;
            this.currModel = curr.Model;

            this.BeginVisit(this.currCfg);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg);

            #region Apps
            this.BeginVisit(this.currCfg.GroupAppSolutions);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg.GroupAppSolutions);
            this.BeginVisit(this.currCfg.GroupAppSolutions.ListAppSolutions);
            foreach (var t in this.currCfg.GroupAppSolutions.ListAppSolutions)
            {
                this.BeginVisit(t);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, t);
                //foreach (var tt in t.ListGroupGeneratorsSettings)
                //{
                //    this.BeginVisit(tt);
                //        this._act?.Invoke(this, tt);
                //    this.EndVisit(tt);
                //}
                this.BeginVisit(t.ListAppProjects);
                foreach (var tt in t.ListAppProjects)
                {
                    this.BeginVisit(tt);
                    if (isActFromRootToBottom)
                        this._act?.Invoke(this, tt);
                    this.BeginVisit(tt.ListAppProjectGenerators);
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        this.BeginVisit(ttt);
                        this._act?.Invoke(this, ttt);
                        this.EndVisit(ttt);
                    }
                    if (!isActFromRootToBottom)
                        this._act?.Invoke(this, tt);
                    this.EndVisit(tt);
                }
                if (!isActFromRootToBottom)
                    this._act?.Invoke(this, t);
                this.EndVisit(t);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg.GroupAppSolutions);
            this.EndVisit(this.currCfg.GroupAppSolutions);
            #endregion Apps

            #region GroupConfigLinks
            this.BeginVisit(this.currCfg.GroupConfigLinks);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg.GroupConfigLinks);
            this.BeginVisit(this.currCfg.GroupConfigLinks.ListBaseConfigLinks);
            foreach (var t in this.currCfg.GroupConfigLinks.ListBaseConfigLinks)
            {
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg.GroupConfigLinks);
            this.EndVisit(this.currCfg.GroupConfigLinks.ListBaseConfigLinks);
            this.EndVisit(this.currCfg.GroupConfigLinks);
            #endregion GroupConfigLinks

            #region Plugins
            this.BeginVisit(this.currCfg.GroupPlugins);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg.GroupPlugins);
            this.BeginVisit(this.currCfg.GroupPlugins.ListPlugins);
            foreach (var t in this.currCfg.GroupPlugins.ListPlugins)
            {
                this.BeginVisit(t);
                if (isActFromRootToBottom)
                    this._act?.Invoke(this, t);
                foreach (var tt in t.ListGenerators)
                {
                    this.BeginVisit(tt);
                    this._act?.Invoke(this, tt);
                    this.EndVisit(tt);
                }
                if (!isActFromRootToBottom)
                    this._act?.Invoke(this, t);
                this.EndVisit(t);
            }
            this.EndVisit(this.currCfg.GroupPlugins.ListPlugins);
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg.GroupPlugins);
            this.EndVisit(this.currCfg.GroupPlugins);
            #endregion Plugins

            this.Run(this.currCfg.Model, isActFromRootToBottom, act);

            if (!isActFromRootToBottom)
                this._act?.Invoke(this, this.currCfg);
            this.EndVisit(this.currCfg);
        }

        protected IConfig? currCfg;
        protected IAppSolution? currSln = null;
        protected IAppProject? currPrj = null;
        protected IModel? currModel;
        protected IEnumeration? currEnum = null;
        protected IForm? currForm = null;
        protected IReport? currRep = null;
        protected ICatalog? currCat = null;
        protected IRegister? currReg = null;
        protected IDocument? currDoc = null;
        protected vSharpStudio.common.IProperty? currProp = null;
        protected IManyToManyCatalogsRelation? currCatRelation = null;
        protected IManyToManyDocumentsRelation? currDocRelation = null;


        protected Stack<IDetail> currPropTabStack = new Stack<IDetail>();
        protected Action<ModelVisitorBase, ITreeConfigNode>? _act = null;
        // 0 - previous, 1 - previous of previous
        protected IDetail GetPropertiesTabFromStack(int level)
        {
            if (this.currPropTabStack.Count < level)
                throw new Exception();
            //return this.currPropTabStack.ToArray()[level];
            return this.currPropTabStack.ToArray()[this.currPropTabStack.Count - level - 1];
        }
        protected IDetail currPropTab => this.currPropTabStack.Peek();

        #region Private Visits
        private void VisitProperties(IGroupListProperties parent, IEnumerable<IProperty> lst, bool isActFromRootToBottom)
        {
            this.BeginVisit(parent);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.currProp = t;
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
                this.currProp = null;
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            this.EndVisit(parent);
        }
        private void VisitDetails(IGroupListDetails parent, IEnumerable<IDetail> lst, bool isActFromRootToBottom)
        {
            this.BeginVisit(parent);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.BeginVisit(t);
                //if (t.IsDeleted())
                //    continue;
                this.currPropTabStack.Push(t);
                this._act?.Invoke(this, t);
                this.VisitProperties(t.GroupProperties, t.GroupProperties.ListProperties, isActFromRootToBottom);
                this.VisitForms(t.GroupForms, t.GroupForms.ListForms, isActFromRootToBottom);
                this.VisitDetails(t.GroupDetails, t.GroupDetails.ListDetails, isActFromRootToBottom);
                this.currPropTabStack.Pop();
                this.EndVisit(t);
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            this.EndVisit(parent);
        }
        private void VisitForms(IGroupListForms parent, IEnumerable<IForm> lst, bool isActFromRootToBottom)
        {
            this.BeginVisit(parent);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.currForm = t;
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
                this.currForm = null;
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            this.EndVisit(parent);
        }
        private void VisitReports(IGroupListReports parent, IEnumerable<IReport> lst, bool isActFromRootToBottom)
        {
            this.BeginVisit(parent);
            if (isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.currRep = t;
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
                this.currRep = null;
            }
            if (!isActFromRootToBottom)
                this._act?.Invoke(this, parent);
            this.EndVisit(parent);
        }
        #endregion Private Visits

        protected virtual void BeginVisit(IConfig cfg) { }
        protected virtual void EndVisit(IConfig cfg) { }

        #region Model Visits

        #region Enumeration
        protected virtual void BeginVisit(IGroupListEnumerations cn) { }
        protected virtual void EndVisit(IGroupListEnumerations cn) { }
        protected virtual void BeginVisit(IEnumeration en) { }
        protected virtual void EndVisit(IEnumeration en) { }
        protected virtual void BeginVisit(IEnumerationPair p) { }
        protected virtual void EndVisit(IEnumerationPair p) { }
        protected virtual void BeginVisit(IEnumerable<IEnumeration> lst) { }
        protected virtual void EndVisit(IEnumerable<IEnumeration> lst) { }
        protected virtual void BeginVisit(IGroupListEnumeratorSequences cn) { }
        protected virtual void EndVisit(IGroupListEnumeratorSequences cn) { }
        protected virtual void BeginVisit(IEnumeratorSequence p) { }
        protected virtual void EndVisit(IEnumeratorSequence p) { }
        #endregion Enumeration

        #region Constant
        protected virtual void BeginVisit(IGroupConstantGroups cn) { }
        protected virtual void EndVisit(IGroupConstantGroups cn) { }
        protected virtual void BeginVisit(IGroupListConstants cn) { }
        protected virtual void EndVisit(IGroupListConstants cn) { }
        protected virtual void BeginVisit(IConstant cn) { }
        protected virtual void EndVisit(IConstant cn) { }
        //protected virtual void BeginVisit(IEnumerable<IConstant> lst) { }
        //protected virtual void EndVisit(IEnumerable<IConstant> lst) { }
        #endregion Constant

        #region Catalog
        protected virtual void BeginVisit(IGroupListCatalogs cn) { }
        protected virtual void EndVisit(IGroupListCatalogs cn) { }
        protected virtual void BeginVisit(ICatalog ct) { }
        protected virtual void EndVisit(ICatalog ct) { }
        protected virtual void BeginVisit(ICatalogFolder ct) { }
        protected virtual void EndVisit(ICatalogFolder ct) { }
        protected virtual void BeginVisit(IEnumerable<ICatalog> lst) { }
        protected virtual void EndVisit(IEnumerable<ICatalog> lst) { }
        #endregion Catalog

        #region Detail
        protected virtual void BeginVisit(IGroupListDetails cn) { }
        protected virtual void EndVisit(IGroupListDetails cn) { }
        protected virtual void BeginVisit(IDetail t) { }
        protected virtual void EndVisit(IDetail t) { }
        #endregion Detail

        #region ManyToMany
        protected virtual void BeginVisit(IManyToManyGroupRelations d) { }
        protected virtual void EndVisit(IManyToManyGroupRelations d) { }
        protected virtual void BeginVisit(IManyToManyGroupCatalogsRelations d) { }
        protected virtual void EndVisit(IManyToManyGroupCatalogsRelations d) { }
        protected virtual void BeginVisit(IManyToManyCatalogsRelation d) { }
        protected virtual void EndVisit(IManyToManyCatalogsRelation d) { }
        protected virtual void BeginVisit(IManyToManyGroupDocumentsRelations d) { }
        protected virtual void EndVisit(IManyToManyGroupDocumentsRelations d) { }
        protected virtual void BeginVisit(IManyToManyDocumentsRelation d) { }
        protected virtual void EndVisit(IManyToManyDocumentsRelation d) { }
        #endregion ManyToMany

        #region Document
        protected virtual void BeginVisit(IGroupDocuments cn) { }
        protected virtual void EndVisit(IGroupDocuments cn) { }
        protected virtual void BeginVisit(IEnumerable<IDocument> lst) { }
        protected virtual void EndVisit(IEnumerable<IDocument> lst) { }
        protected virtual void BeginVisit(IGroupListDocuments cn) { }
        protected virtual void EndVisit(IGroupListDocuments cn) { }
        protected virtual void BeginVisit(IDocument d) { }
        protected virtual void EndVisit(IDocument d) { }
        #endregion Document

        #region Register
        protected virtual void BeginVisit(IGroupListRegisters cn) { }
        protected virtual void EndVisit(IGroupListRegisters cn) { }
        protected virtual void BeginVisit(IRegister d) { }
        protected virtual void EndVisit(IRegister d) { }
        protected virtual void BeginVisit(IRegisterDimension d) { }
        protected virtual void EndVisit(IRegisterDimension d) { }
        protected virtual void BeginVisit(IGroupListRegisterDimensions d) { }
        protected virtual void EndVisit(IGroupListRegisterDimensions d) { }
        protected virtual void BeginVisit(IGroupListDimensions d) { }
        protected virtual void EndVisit(IGroupListDimensions d) { }
        #endregion Register

        #region Journal
        protected virtual void BeginVisit(IGroupListJournals cn) { }
        protected virtual void EndVisit(IGroupListJournals cn) { }
        protected virtual void BeginVisit(IJournal cn) { }
        protected virtual void EndVisit(IJournal cn) { }
        #endregion Journal

        #region Form
        protected virtual void BeginVisit(IGroupListMainViewForms cn) { }
        protected virtual void EndVisit(IGroupListMainViewForms cn) { }
        protected virtual void BeginVisit(IEnumerable<IMainViewForm> lst) { }
        protected virtual void EndVisit(IEnumerable<IMainViewForm> lst) { }
        protected virtual void BeginVisit(IMainViewForm p) { }
        protected virtual void EndVisit(IMainViewForm p) { }
        protected virtual void BeginVisit(IGroupListForms cn) { }
        protected virtual void EndVisit(IGroupListForms cn) { }
        protected virtual void BeginVisit(IForm p) { }
        protected virtual void EndVisit(IForm p) { }
        #endregion Form

        #region Report
        protected virtual void BeginVisit(IGroupListReports cn) { }
        protected virtual void EndVisit(IGroupListReports cn) { }
        protected virtual void BeginVisit(IReport p) { }
        protected virtual void EndVisit(IReport p) { }
        #endregion Report

        #region Common
        protected virtual void BeginVisit(IGroupListCommon cn) { }
        protected virtual void EndVisit(IGroupListCommon cn) { }
        #endregion Common

        #region Property
        protected virtual void BeginVisit(IGroupListProperties cn) { }
        protected virtual void EndVisit(IGroupListProperties cn) { }
        protected virtual void BeginVisit(IProperty p) { }
        protected virtual void EndVisit(IProperty p) { }
        #endregion Property

        #region Role
        protected virtual void BeginVisit(IGroupListRoles cn) { }
        protected virtual void EndVisit(IGroupListRoles cn) { }
        protected virtual void BeginVisit(IEnumerable<IRole> lst) { }
        protected virtual void EndVisit(IEnumerable<IRole> lst) { }
        protected virtual void BeginVisit(IRole p) { }
        protected virtual void EndVisit(IRole p) { }
        //protected virtual void BeginVisit(IEnumerable<IRole> lst) { }
        //protected virtual void EndVisit(IEnumerable<IRole> lst) { }
        #endregion Role

        protected virtual void BeginVisit(IModel m) { }
        protected virtual void EndVisit(IModel m) { }
        #endregion Model Visits

        #region Links Visits
        protected virtual void EndVisit(IGroupListBaseConfigLinks groupConfigLinks) { }
        protected virtual void EndVisit(IEnumerable<IBaseConfigLink> listBaseConfigLinks) { }
        protected virtual void EndVisit(IBaseConfigLink t) { }
        protected virtual void BeginVisit(IBaseConfigLink t) { }
        protected virtual void BeginVisit(IEnumerable<IBaseConfigLink> listBaseConfigLinks) { }
        protected virtual void BeginVisit(IGroupListBaseConfigLinks groupConfigLinks) { }
        #endregion Links Visits

        #region App Visits
        protected virtual void EndVisit(IAppProjectGenerator ttt) { }
        protected virtual void BeginVisit(IAppProjectGenerator ttt) { }
        protected virtual void BeginVisit(IEnumerable<IAppProjectGenerator> listAppProjectGenerators) { }
        protected virtual void EndVisit(IAppProject tt) { }
        protected virtual void BeginVisit(IAppProject tt) { }
        protected virtual void BeginVisit(IEnumerable<IAppProject> listAppProjects) { }
        protected virtual void EndVisit(IAppSolution t) { }
        protected virtual void BeginVisit(IAppSolution t) { }
        protected virtual void BeginVisit(IEnumerable<IAppSolution> listAppSolutions) { }
        protected virtual void EndVisit(IGroupListAppSolutions groupAppSolutions) { }
        protected virtual void BeginVisit(IGroupListAppSolutions groupAppSolutions) { }
        #endregion App Visits

        #region Plugin Visits
        protected virtual void EndVisit(IPluginGroupGeneratorsSettings tt) { }
        protected virtual void BeginVisit(IPluginGroupGeneratorsSettings tt) { }
        protected virtual void EndVisit(IGroupListPlugins groupPlugins) { }
        protected virtual void EndVisit(IReadOnlyList<IPlugin> listPlugins) { }
        protected virtual void EndVisit(IPlugin t) { }
        protected virtual void EndVisit(IPluginGenerator tt) { }
        protected virtual void BeginVisit(IPlugin t) { }
        protected virtual void BeginVisit(IPluginGenerator tt) { }
        protected virtual void BeginVisit(IReadOnlyList<IPlugin> listPlugins) { }
        protected virtual void BeginVisit(IGroupListPlugins groupPlugins) { }
        #endregion Plugin Visits
    }
}
