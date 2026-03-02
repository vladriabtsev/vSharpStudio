using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class GroupDocuments : ITreeModel, ICanGoRight, ICanGoLeft, IEditableNodeGroup /*INodeGenSettings,*/
    {
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(" Shared:");
            sb.Append(this.DocumentTimeline.ListProperties.Count);
            sb.Append(" Docs:");
            sb.Append(this.GroupListDocuments.ListDocuments.Count);
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentModel.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this._Name = Defaults.DocumentsGroupName;
            if (this.Children.Count > 0)
                return;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.DocumentTimeline, 1);
            children.Add(this.GroupListSequences, 2);
            children.Add(this.GroupListDocuments, 3);
            children.Add(this.GroupRegisters, 4);
            children.Add(this.GroupJournals, 5);

            //this.ListRoles.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListRoles.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListRoles.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListRoles.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        public Document AddDocument(string name, string? guid = null)
        {
            var node = new Document(this.GroupListDocuments) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.GroupListDocuments.NodeAddNewSubNode(node);
            return node;
        }
        [Browsable(false)]
        public string DocumentTimelineName
        {
            get
            {
                if (this._DocumentTimelineName == null)
                {
                    this._DocumentTimelineName = this.DocumentTimeline.Name;
                }
                Debug.Assert(this._DocumentTimelineName != null);
                return this._DocumentTimelineName;
            }
        }
        private string? _DocumentTimelineName = null;
        [Browsable(false)]
        public string TimeLineDocDateTimePropertyName
        {
            get
            {
                if (this._TimeLineDocDateTimePropertyName == null)
                {
                    this._TimeLineDocDateTimePropertyName = this.DocumentTimeline.TimeLineDocDateTimePropertyName;
                }
                Debug.Assert(this._TimeLineDocDateTimePropertyName != null);
                return this._TimeLineDocDateTimePropertyName;
            }
        }
        private string? _TimeLineDocDateTimePropertyName = null;
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children),
                nameof(this.DynamicNodesSettings)
            };
            return [.. lst];
        }
    }
}
