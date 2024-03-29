﻿using System;
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
        public void Run(IModel model, Action<ModelVisitorBase, ITreeConfigNode>? act = null)
        {
            this._act = act;
            this.currModel = model;
            this.currCfg = model.ParentConfigI;
            this.BeginVisit(this.currModel);

            //TODO change visiting to visit object with references to other objects after visiting referenced objects


            #region Common
            this.BeginVisit(this.currModel.GroupCommon);
            this._act?.Invoke(this, this.currModel.GroupCommon);
            this.BeginVisit(currModel.GroupCommon.GroupRoles);
            this._act?.Invoke(this, this.currModel.GroupCommon.GroupRoles);
            foreach (var tt in currModel.GroupCommon.GroupRoles.ListRoles)
            {
                this.BeginVisit(tt);
                this._act?.Invoke(this, tt);
                this.EndVisit(tt);
            }
            this.EndVisit(currModel.GroupCommon.GroupRoles);
            this.BeginVisit(currModel.GroupCommon.GroupViewForms);
            this._act?.Invoke(this, this.currModel.GroupCommon.GroupViewForms);
            foreach (var tt in currModel.GroupCommon.GroupViewForms.ListMainViewForms)
            {
                this.BeginVisit(tt);
                this._act?.Invoke(this, tt);
                this.EndVisit(tt);
            }
            this.EndVisit(currModel.GroupCommon.GroupViewForms);
            this.EndVisit(currModel.GroupCommon);
            #endregion Common

            #region Constants
            this.BeginVisit(currModel.GroupConstantGroups);
            this._act?.Invoke(this, this.currModel.GroupConstantGroups);
            foreach (var t in currModel.GroupConstantGroups.ListConstantGroups)
            {
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                foreach (var tt in t.ListConstants)
                {
                    this.BeginVisit(tt);
                    this._act?.Invoke(this, tt);
                    this.EndVisit(tt);
                }
                this.EndVisit(t);
            }
            this.EndVisit(currModel.GroupConstantGroups);
            #endregion Constants

            #region Enumerations
            this.BeginVisit(currModel.GroupEnumerations);
            this._act?.Invoke(this, this.currModel.GroupEnumerations);
            this.BeginVisit(currModel.GroupEnumerations.ListEnumerations);
            foreach (var tt in currModel.GroupEnumerations.ListEnumerations)
            {
                this.currEnum = tt;
                this.BeginVisit(tt);
                this._act?.Invoke(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                foreach (var ttt in tt.ListEnumerationPairs)
                {
                    this.BeginVisit(ttt);
                    this._act?.Invoke(this, ttt);
                    this.EndVisit(ttt);
                }
                this.EndVisit(tt);
                this.currEnum = null;
            }
            this.EndVisit(currModel.GroupEnumerations.ListEnumerations);
            this.EndVisit(currModel.GroupEnumerations);
            #endregion Enumerations

            #region Catalogs
            this.BeginVisit(currModel.GroupCatalogs);
            this._act?.Invoke(this, this.currModel.GroupCatalogs);
            this.BeginVisit(currModel.GroupCatalogs.ListCatalogs);
            foreach (var tt in currModel.GroupCatalogs.ListCatalogs)
            {
                this.BeginVisit(tt);
                this.currCat = tt;
                this._act?.Invoke(this, tt);
                this.BeginVisit(tt.Folder);
                this._act?.Invoke(this, tt.Folder);
                this.VisitProperties(tt.Folder.GroupProperties, tt.Folder.GroupProperties.ListProperties);
                this.VisitDetails(tt.Folder.GroupDetails, tt.Folder.GroupDetails.ListDetails);
                this.EndVisit(tt.Folder);
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                this.VisitDetails(tt.GroupDetails, tt.GroupDetails.ListDetails);
                this.VisitForms(tt.GroupForms, tt.GroupForms.ListForms);
                this.VisitReports(tt.GroupReports, tt.GroupReports.ListReports);
                this.EndVisit(tt);
                this.currCat = null;
            }
            this.EndVisit(currModel.GroupCatalogs.ListCatalogs);
            this.EndVisit(currModel.GroupCatalogs);
            #endregion Catalogs

            #region Documents
            this.BeginVisit(currModel.GroupDocuments);
            this._act?.Invoke(this, this.currModel.GroupDocuments);
            this.VisitProperties(currModel.GroupDocuments.GroupSharedProperties, currModel.GroupDocuments.GroupSharedProperties.ListProperties);
            this.BeginVisit(currModel.GroupDocuments.GroupListDocuments);
            this._act?.Invoke(this, this.currModel.GroupDocuments.GroupListDocuments);
            this.BeginVisit(currModel.GroupDocuments.GroupListDocuments.ListDocuments);
            foreach (var tt in currModel.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                this.BeginVisit(tt);
                this.currDoc = tt;
                this._act?.Invoke(this, tt);
                //if (tt.IsDeleted())
                //    continue;
                this.VisitProperties(tt.GroupProperties, tt.GroupProperties.ListProperties);
                this.VisitDetails(tt.GroupDetails, tt.GroupDetails.ListDetails);
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
            this.EndVisit(currModel.GroupJournals);
            #endregion Journals

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
        public void Run(IConfig curr, IAppSolution? sln, IAppProject? prj, IAppProjectGenerator? prjGen, Action<ModelVisitorBase, ITreeConfigNode>? act = null)
        {
            this._act = act;
            this.currCfg = curr;
            this.currModel = curr.Model;

            this.BeginVisit(this.currCfg);
            this._act?.Invoke(this, this.currCfg);

            #region Apps
            this.BeginVisit(this.currCfg.GroupAppSolutions);
            this._act?.Invoke(this, this.currCfg.GroupAppSolutions);
            this.BeginVisit(this.currCfg.GroupAppSolutions.ListAppSolutions);
            foreach (var t in this.currCfg.GroupAppSolutions.ListAppSolutions)
            {
                this.BeginVisit(t);
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
                    this._act?.Invoke(this, tt);
                    this.BeginVisit(tt.ListAppProjectGenerators);
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        this.BeginVisit(ttt);
                        this._act?.Invoke(this, ttt);
                        this.EndVisit(ttt);
                    }
                    this.EndVisit(tt);
                }
                this.EndVisit(t);
            }
            this.EndVisit(this.currCfg.GroupAppSolutions);
            #endregion Apps

            #region GroupConfigLinks
            this.BeginVisit(this.currCfg.GroupConfigLinks);
            this._act?.Invoke(this, this.currCfg.GroupConfigLinks);
            this.BeginVisit(this.currCfg.GroupConfigLinks.ListBaseConfigLinks);
            foreach (var t in this.currCfg.GroupConfigLinks.ListBaseConfigLinks)
            {
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
            }
            this.EndVisit(this.currCfg.GroupConfigLinks.ListBaseConfigLinks);
            this.EndVisit(this.currCfg.GroupConfigLinks);
            #endregion GroupConfigLinks

            #region Plugins
            this.BeginVisit(this.currCfg.GroupPlugins);
            this._act?.Invoke(this, this.currCfg.GroupPlugins);
            this.BeginVisit(this.currCfg.GroupPlugins.ListPlugins);
            foreach (var t in this.currCfg.GroupPlugins.ListPlugins)
            {
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                foreach (var tt in t.ListGenerators)
                {
                    this.BeginVisit(tt);
                    this._act?.Invoke(this, tt);
                    this.EndVisit(tt);
                }
                this.EndVisit(t);
            }
            this.EndVisit(this.currCfg.GroupPlugins.ListPlugins);
            this.EndVisit(this.currCfg.GroupPlugins);
            #endregion Plugins

            this.Run(this.currCfg.Model, act);

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
        protected IDocument? currDoc = null;
        protected vSharpStudio.common.IProperty? currProp = null;
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
        private void VisitProperties(IGroupListProperties parent, IEnumerable<IProperty> lst)
        {
            this.BeginVisit(parent);
            this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.currProp = t;
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
                this.currProp = null;
            }
            this.EndVisit(parent);
        }
        private void VisitDetails(IGroupListDetails parent, IEnumerable<IDetail> lst)
        {
            this.BeginVisit(parent);
            this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.BeginVisit(t);
                //if (t.IsDeleted())
                //    continue;
                this.currPropTabStack.Push(t);
                this._act?.Invoke(this, t);
                this.VisitProperties(t.GroupProperties, t.GroupProperties.ListProperties);
                this.VisitDetails(t.GroupDetails, t.GroupDetails.ListDetails);
                this.currPropTabStack.Pop();
                this.EndVisit(t);
            }
            this.EndVisit(parent);
        }
        private void VisitForms(IGroupListForms parent, IEnumerable<IForm> lst)
        {
            this.BeginVisit(parent);
            this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.currForm = t;
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
                this.currForm = null;
            }
            this.EndVisit(parent);
        }
        private void VisitReports(IGroupListReports parent, IEnumerable<IReport> lst)
        {
            this.BeginVisit(parent);
            this._act?.Invoke(this, parent);
            foreach (var t in lst)
            {
                this.currRep = t;
                this.BeginVisit(t);
                this._act?.Invoke(this, t);
                this.EndVisit(t);
                this.currRep = null;
            }
            this.EndVisit(parent);
        }
        #endregion Private Visits

        protected virtual void BeginVisit(IConfig cfg) { }
        protected virtual void EndVisit(IConfig cfg) { }

        #region Model Visits
        //protected virtual void BeginVisit(IEnumerable<IConstant> lst) { }
        //protected virtual void EndVisit(IEnumerable<IConstant> lst) { }
        protected virtual void BeginVisit(IEnumerable<IEnumeration> lst) { }
        protected virtual void EndVisit(IEnumerable<IEnumeration> lst) { }
        protected virtual void BeginVisit(IEnumerable<ICatalog> lst) { }
        protected virtual void EndVisit(IEnumerable<ICatalog> lst) { }
        protected virtual void BeginVisit(IEnumerable<IDocument> lst) { }
        protected virtual void EndVisit(IEnumerable<IDocument> lst) { }
        protected virtual void BeginVisit(IGroupListRoles cn) { }
        protected virtual void EndVisit(IGroupListRoles cn) { }
        protected virtual void BeginVisit(IEnumerable<IRole> lst) { }
        protected virtual void EndVisit(IEnumerable<IRole> lst) { }
        protected virtual void BeginVisit(IRole p) { }
        protected virtual void EndVisit(IRole p) { }
        protected virtual void BeginVisit(IGroupListMainViewForms cn) { }
        protected virtual void EndVisit(IGroupListMainViewForms cn) { }
        protected virtual void BeginVisit(IEnumerable<IMainViewForm> lst) { }
        protected virtual void EndVisit(IEnumerable<IMainViewForm> lst) { }
        protected virtual void BeginVisit(IMainViewForm p) { }
        protected virtual void EndVisit(IMainViewForm p) { }
        protected virtual void BeginVisit(IModel m) { }
        protected virtual void EndVisit(IModel m) { }
        protected virtual void BeginVisit(IGroupListCommon cn) { }
        protected virtual void EndVisit(IGroupListCommon cn) { }
        protected virtual void BeginVisit(IGroupConstantGroups cn) { }
        protected virtual void EndVisit(IGroupConstantGroups cn) { }
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
        protected virtual void BeginVisit(ICatalogFolder ct) { }
        protected virtual void EndVisit(ICatalogFolder ct) { }
        protected virtual void BeginVisit(IGroupDocuments cn) { }
        protected virtual void EndVisit(IGroupDocuments cn) { }
        protected virtual void BeginVisit(IGroupListDocuments cn) { }
        protected virtual void EndVisit(IGroupListDocuments cn) { }
        protected virtual void BeginVisit(IDocument d) { }
        protected virtual void EndVisit(IDocument d) { }
        protected virtual void BeginVisit(IGroupListProperties cn) { }
        protected virtual void EndVisit(IGroupListProperties cn) { }
        protected virtual void BeginVisit(IProperty p) { }
        protected virtual void EndVisit(IProperty p) { }
        protected virtual void BeginVisit(IGroupListDetails cn) { }
        protected virtual void EndVisit(IGroupListDetails cn) { }
        protected virtual void BeginVisit(IDetail t) { }
        protected virtual void EndVisit(IDetail t) { }
        protected virtual void BeginVisit(IGroupListForms cn) { }
        protected virtual void EndVisit(IGroupListForms cn) { }
        protected virtual void BeginVisit(IForm p) { }
        protected virtual void EndVisit(IForm p) { }
        protected virtual void BeginVisit(IGroupListJournals cn) { }
        protected virtual void EndVisit(IGroupListJournals cn) { }
        protected virtual void BeginVisit(IJournal cn) { }
        protected virtual void EndVisit(IJournal cn) { }
        protected virtual void BeginVisit(IGroupListReports cn) { }
        protected virtual void EndVisit(IGroupListReports cn) { }
        protected virtual void BeginVisit(IReport p) { }
        protected virtual void EndVisit(IReport p) { }
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
