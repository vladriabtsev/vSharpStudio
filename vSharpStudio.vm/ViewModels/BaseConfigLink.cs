using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class BaseConfigLink : IEditableNode  // INodeGenSettings, 
    {
        [BrowsableAttribute(false)]
        public GroupListBaseConfigLinks ParentGroupListBaseConfigLinks { get { Debug.Assert(this.Parent != null); return (GroupListBaseConfigLinks)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListBaseConfigLinks ParentGroupListBaseConfigLinksI { get { Debug.Assert(this.Parent != null); return (IGroupListBaseConfigLinks)this.Parent; } }

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListBaseConfigLinks.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "icon3DScene"; } }
        //protected override string GetNodeIconName() { return "icon3DScene"; }
        partial void OnCreated()
        {
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
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
        //public void OnAdded()
        //{
        //    this.AddAllAppGenSettingsVmsToNode();
        //}
        public IEnumerable<ITreeConfigNode> GetParentList()
        {
            return this.ParentGroupListBaseConfigLinks.ListBaseConfigLinks;
        }
        public void Remove()
        {
            this.ParentGroupListBaseConfigLinks.ListBaseConfigLinks.Remove(this);
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            return lst.ToArray();
        }
    }
}
