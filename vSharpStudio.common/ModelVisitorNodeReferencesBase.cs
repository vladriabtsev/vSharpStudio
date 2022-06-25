using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Serilog.Events;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace vSharpStudio.common
{
    public class ModelVisitorNodeReferencesBase : ModelVisitorBase
    {
        /// <summary>
        /// Model object references
        /// </summary>
        public class ModelNode
        {
            public ModelNode()
            {
                this.DicReferenceToNodes = new Dictionary<string, ReferenceTo>();
                this.DicReferecedFromNodes = new Dictionary<string, ReferenceFrom>();
            }
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid NodeObject { get; set; }
            /// <summary>
            /// References from this model objects to others
            /// </summary>
            public Dictionary<string, ReferenceTo> DicReferenceToNodes { get; set; }
            /// <summary>
            /// References from other model objects to this model object
            /// </summary>
            public Dictionary<string, ReferenceFrom> DicReferecedFromNodes { get; set; }
        }
        /// <summary>
        /// Reference to another model object
        /// </summary>
        public class ReferenceTo
        {
            public ReferenceTo() { this.DicByFields = new Dictionary<string, IGuid>(); }
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid ToObject { get; set; }
            /// <summary>
            /// Model object field which is referencing 
            /// </summary>
            public Dictionary<string, IGuid> DicByFields { get; set; }
        }
        /// <summary>
        /// Referenced from another model object
        /// </summary>
        public class ReferenceFrom
        {
            public ReferenceFrom() { this.DicFromFields = new Dictionary<string, IGuid>(); }
            /// <summary>
            /// Model object
            /// </summary>
            public IGuid FromObject { get; set; }
            /// <summary>
            /// Model object field which is referencing 
            /// </summary>
            public Dictionary<string, IGuid> DicFromFields { get; set; }
        }
        protected Dictionary<string, ModelNode> DicNodesWithReferences = new Dictionary<string, ModelNode>();
        //private List<IGuid> GrapfToSequenceForDb()
        private void ScanForDicNodesWithReferences()
        {
            foreach (var t in currModel.GroupEnumerations.ListEnumerations)
            {
                var md = new ModelNode() { NodeObject = t };
                this.DicNodesWithReferences[t.Guid] = md;
            }
            foreach (var t in currModel.GroupConstantGroups.ListConstantGroups)
            {
                foreach (var tt in t.ListConstants)
                {
                    var md = new ModelNode() { NodeObject = tt };
                    this.DicNodesWithReferences[tt.Guid] = md;
                    if (!string.IsNullOrWhiteSpace(tt.DataType.ObjectGuid))
                    {
                        AddReferenceToNode(md, tt, tt.DataType);
                    }
                }
            }
            foreach (var t in currModel.GroupCatalogs.ListCatalogs)
            {
                var md = new ModelNode() { NodeObject = t };
                this.DicNodesWithReferences[t.Guid] = md;
                ScanProperties(md, t.GroupProperties.ListProperties);
                ScanDetails(md, t.GroupDetails);
            }
            foreach (var t in currModel.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                var md = new ModelNode() { NodeObject = t };
                this.DicNodesWithReferences[t.Guid] = md;
                ScanProperties(md, currModel.GroupDocuments.GroupSharedProperties.ListProperties);
                ScanDetails(md, t.GroupDetails);
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
        private void ScanDetails(ModelNode md, IGroupListDetails t)
        {
            foreach (var tt in t.ListDetails)
            {
                ScanProperties(md, tt.GroupProperties.ListProperties);
                ScanDetails(md, tt.GroupDetails);
            }
        }
        private void ScanProperties(ModelNode md, IEnumerable<IProperty> lst)
        {
            foreach (var t in lst)
            {
                if (!string.IsNullOrWhiteSpace(t.DataType.ObjectGuid))
                {
                    AddReferenceToNode(md, t, t.DataType);
                }
            }
        }
        private void AddReferenceToNode(ModelNode md, IConstant constant, IDataType d)
        {
            if (!md.DicReferenceToNodes.ContainsKey(d.ObjectGuid))
            {
                md.DicReferenceToNodes[d.ObjectGuid] = new ReferenceTo();
            }
            var tn = md.DicReferenceToNodes[d.ObjectGuid];
            tn.DicByFields[constant.Guid] = constant;
            tn.ToObject = this.currCfg.DicNodes[d.ObjectGuid];
        }
        private void AddReferenceToNode(ModelNode md, IProperty property, IDataType d)
        {
            if (!md.DicReferenceToNodes.ContainsKey(d.ObjectGuid))
            {
                md.DicReferenceToNodes[d.ObjectGuid] = new ReferenceTo();
            }
            var tn = md.DicReferenceToNodes[d.ObjectGuid];
            tn.DicByFields[property.Guid] = property;
            tn.ToObject = this.currCfg.DicNodes[d.ObjectGuid];
        }
        private static void AddReferenceFromNode(ModelNode md, IGuid property, IGuid from)
        {
            if (!md.DicReferecedFromNodes.ContainsKey(from.Guid))
            {
                md.DicReferecedFromNodes[from.Guid] = new ReferenceFrom();
            }
            var tn = md.DicReferecedFromNodes[from.Guid];
            tn.DicFromFields[property.Guid] = property;
            tn.FromObject = from;
        }
        public new void Run(IModel model, Action<ModelVisitorBase, ITreeConfigNode> act = null)
        {
            this._act = act;
            this.currModel = model;
            this.ScanForDicNodesWithReferences();

            base.Run(model, act);
        }
        /// <summary>
        /// Visit and annotate config nodes.
        /// Create extended config model with deleted nodes.
        /// </summary>
        /// <param name="curr">Current config or clone</param>
        /// <param name="act"></param>
        /// <returns></returns>
        public new void Run(IConfig curr, IAppSolution sln, IAppProject prj, Action<ModelVisitorBase, ITreeConfigNode> act = null)
        {
            this._act = act;
            this.currCfg = curr;
            this.currSln = sln;
            this.currPrj = prj;

            this.BeginVisit(this.currCfg, this.currSln, this.currPrj);

            #region Apps
            this.BeginVisit(this.currCfg.GroupAppSolutions);
            this.BeginVisit(this.currCfg.GroupAppSolutions.ListAppSolutions);
            foreach (var t in this.currCfg.GroupAppSolutions.ListAppSolutions)
            {
                this.BeginVisit(t);
                if (_act != null)
                    _act(this, t);
                this.BeginVisit(t.ListAppProjects);
                foreach (var tt in t.ListAppProjects)
                {
                    this.BeginVisit(tt);
                    if (_act != null)
                        _act(this, tt);
                    this.BeginVisit(tt.ListAppProjectGenerators);
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        this.BeginVisit(ttt);
                        if (_act != null)
                            _act(this, ttt);
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
                if (_act != null)
                    _act(this, t);
                this.EndVisit(t);
            }
            this.EndVisit(this.currCfg.GroupConfigLinks.ListBaseConfigLinks);
            this.EndVisit(this.currCfg.GroupConfigLinks);
            #endregion GroupConfigLinks

            this.Run(this.currCfg.Model, act);

            this.EndVisit(this.currCfg);
        }
    }
}
