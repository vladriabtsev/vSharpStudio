using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace vSharpStudio.common
{
    public class ModelVisitorNodeReferencesBase(string appDbGenGuid) : ModelVisitorBase
    {
        private readonly string appDbGenGuid = appDbGenGuid;
        /// <summary>
        /// Model object references
        /// </summary>
        public class ModelNode(IGuid nodeObject)
        {
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid NodeObject { get; set; } = nodeObject;
            /// <summary>
            /// References from this model objects to others
            /// </summary>
            public Dictionary<string, ReferenceTo> DicReferenceToNodes { get; set; } = [];
            /// <summary>
            /// References from other model objects to this model object
            /// </summary>
            public Dictionary<string, ReferenceFrom> DicReferecedFromNodes { get; set; } = [];
        }
        /// <summary>
        /// Reference to another model object
        /// </summary>
        public class ReferenceTo(IGuid toObject)
        {
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid ToObject { get; set; } = toObject;
            /// <summary>
            /// Model object field which is referencing 
            /// </summary>
            public Dictionary<string, IGuid> DicByFields { get; set; } = [];
        }
        /// <summary>
        /// Referenced from another model object
        /// </summary>
        public class ReferenceFrom(IGuid fromObject)
        {
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid FromObject { get; set; } = fromObject;
            /// <summary>
            /// Model object field which is referencing 
            /// </summary>
            public Dictionary<string, IGuid> DicFromFields { get; set; } = [];
        }
        protected Dictionary<string, ModelNode> DicNodesWithReferences = [];
        //private List<IGuid> GrapfToSequenceForDb()
        private void ScanForDicNodesWithReferences()
        {
            Debug.Assert(this.currModel != null);
            foreach (var t in currModel.GroupEnumerations.ListEnumerations)
            {
                var md = new ModelNode(t);
                this.DicNodesWithReferences[t.Guid] = md;
            }
            foreach (var t in currModel.GroupConstantGroups.ListConstantGroups)
            {
                foreach (var tt in t.ListConstants)
                {
                    var md = new ModelNode(tt);
                    AddReferenceToNode(md, tt, tt.DataType);
                }
            }
            foreach (var t in currModel.GroupCatalogs.GroupListCatalogs.ListCatalogs)
            {
                var md = new ModelNode(t);
                this.DicNodesWithReferences[t.Guid] = md;
                ScanProperties(md, t.GetIncludedProperties(this.appDbGenGuid, false, false));
                ScanDetails(md, t.GetIncludedDetails(this.appDbGenGuid));
            }
            foreach (var t in currModel.GroupCatalogs.GroupRelations.GroupListOneToOneRelations.ListRelations)
            {
                var md = new ModelNode(t);
                this.DicNodesWithReferences[t.Guid] = md;
                ScanProperties(md, t.GetIncludedProperties(this.appDbGenGuid, false, false));
            }
            foreach (var t in currModel.GroupCatalogs.GroupRelations.GroupListManyToManyRelations.ListRelations)
            {
                var md = new ModelNode(t);
                this.DicNodesWithReferences[t.Guid] = md;
                ScanProperties(md, t.GetIncludedProperties(this.appDbGenGuid, false, false));
            }
            foreach (var t in currModel.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                var md = new ModelNode(t);
                this.DicNodesWithReferences[t.Guid] = md;
                ScanProperties(md, t.GetIncludedProperties(this.appDbGenGuid, false, true));
                ScanDetails(md, t.GetIncludedDetails(this.appDbGenGuid));
            }
            foreach (var t in currModel.GroupDocuments.GroupRegisters.ListRegisters)
            {
                var md = new ModelNode(t);
                this.DicNodesWithReferences[t.Guid] = md;
                //ScanProperties(md, t.GetIncludedProperties(this.appDbGenGuid, false, true));
                switch (t.RegisterType)
                {
                    case EnumRegisterType.BALANCE:
                        ScanProperties(md, t.GetIncludedBalanceProperties(this.appDbGenGuid, false, true));
                        break;
                    case EnumRegisterType.BALANCE_AND_TURNOVER:
                        ScanProperties(md, t.GetIncludedBalanceProperties(this.appDbGenGuid, false, true));
                        ScanProperties(md, t.GetIncludedTurnoverProperties(this.appDbGenGuid, false, true));
                        break;
                    case EnumRegisterType.TURNOVER:
                        ScanProperties(md, t.GetIncludedTurnoverProperties(this.appDbGenGuid, false, true));
                        break;
                }
            }
            foreach (var t in this.DicNodesWithReferences)
            {
                var md = t.Value;
                foreach (var tt in t.Value.DicReferenceToNodes)
                {
                    var toObject = this.DicNodesWithReferences[tt.Value.ToObject.Guid];
                    foreach (var ttt in tt.Value.DicByFields)
                    {
                        AddReferenceFromNode(toObject, ttt.Value, t.Value.NodeObject);
                    }
                }
            }
        }
        private void ScanDetails(ModelNode md, IEnumerable<IDetail> lst)
        {
            foreach (var tt in lst)
            {
                ScanProperties(md, tt.GroupProperties.ListProperties);
                ScanDetails(md, tt.GetIncludedDetails(this.appDbGenGuid));
            }
        }
        private void ScanProperties(ModelNode md, IEnumerable<IProperty> lst)
        {
            foreach (var t in lst)
            {
                AddReferenceToNode(md, t, t.DataType);
            }
        }
        private void AddReferenceToNode(ModelNode md, IGuid obj, IDataType d)
        {
            Debug.Assert(this.currCfg != null);
            if (!string.IsNullOrWhiteSpace(d.ObjectRef?.ForeignObjectGuid))
            {
                Debug.Assert(!md.DicReferenceToNodes.ContainsKey(d.ObjectRef.ForeignObjectGuid));
                md.DicReferenceToNodes[d.ObjectRef.ForeignObjectGuid] = new ReferenceTo(this.currCfg.DicNodes[d.ObjectRef.ForeignObjectGuid]);
                Debug.Assert(d.ListObjectRefs.Count == 0);
                var tn = md.DicReferenceToNodes[d.ObjectRef.ForeignObjectGuid];
                tn.DicByFields[obj.Guid] = obj;
            }
            foreach (var ttt in d.ListObjectRefs)
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(ttt.ForeignObjectGuid));
                Debug.Assert(!md.DicReferenceToNodes.ContainsKey(ttt.ForeignObjectGuid));
                md.DicReferenceToNodes[ttt.ForeignObjectGuid] = new ReferenceTo(this.currCfg.DicNodes[ttt.ForeignObjectGuid]);
                var tn = md.DicReferenceToNodes[ttt.ForeignObjectGuid];
                tn.DicByFields[obj.Guid] = obj;
            }
        }
        private static void AddReferenceFromNode(ModelNode md, IGuid property, IGuid from)
        {
            if (!md.DicReferecedFromNodes.TryGetValue(from.Guid, out var tn))
            {
                tn = new ReferenceFrom(from);
                md.DicReferecedFromNodes[from.Guid] = tn;
            }

            tn.DicFromFields[property.Guid] = property;
        }
        public new void Run(IModel model, bool isActFromRootToBottom = true, Action<ModelVisitorBase, ITreeConfigNode>? act = null)
        {
            this._act = act;
            this.currModel = model;
            this.ScanForDicNodesWithReferences();

            base.Run(model, isActFromRootToBottom, act);
        }
        /// <summary>
        /// Visit and annotate config nodes.
        /// Create extended config model with deleted nodes.
        /// </summary>
        /// <param name="curr">Current config or clone</param>
        /// <param name="act"></param>
        /// <returns></returns>
        public void Run(IConfig curr, IAppSolution? sln, IAppProject? prj, bool isActFromRootToBottom = true, Action<ModelVisitorBase, ITreeConfigNode>? act = null)
        {
            this._act = act;
            this.currCfg = curr;
            this.currSln = sln;
            this.currPrj = prj;

            #region Apps
            this.BeginVisit(this.currCfg.GroupAppSolutions);
            this.BeginVisit(this.currCfg.GroupAppSolutions.ListAppSolutions);
            foreach (var t in this.currCfg.GroupAppSolutions.ListAppSolutions)
            {
                this.BeginVisit(t);
                _act?.Invoke(this, t);
                this.BeginVisit(t.ListAppProjects);
                foreach (var tt in t.ListAppProjects)
                {
                    this.BeginVisit(tt);
                    _act?.Invoke(this, tt);
                    this.BeginVisit(tt.ListAppProjectGenerators);
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        this.BeginVisit(ttt);
                        _act?.Invoke(this, ttt);
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
            this.BeginVisit(this.currCfg.GroupConfigLinks.ListBaseConfigLinks);
            foreach (var t in this.currCfg.GroupConfigLinks.ListBaseConfigLinks)
            {
                this.BeginVisit(t);
                _act?.Invoke(this, t);
                this.EndVisit(t);
            }
            this.EndVisit(this.currCfg.GroupConfigLinks.ListBaseConfigLinks);
            this.EndVisit(this.currCfg.GroupConfigLinks);
            #endregion GroupConfigLinks

            this.Run(this.currCfg.Model, isActFromRootToBottom, act);

            this.EndVisit(this.currCfg);
        }
    }
}
