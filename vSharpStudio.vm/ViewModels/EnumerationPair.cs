using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("EnumerationPair:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class EnumerationPair : IEditableNode, ICanAddNode, INodeGenSettings
    {
        [Browsable(false)]
        public Enumeration ParentEnumeration { get { Debug.Assert(this.Parent != null); return (Enumeration) this.Parent; } }
        [Browsable(false)]
        public IEnumeration ParentEnumerationI { get { Debug.Assert(this.Parent != null); return (IEnumeration)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentEnumeration.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree


        [Browsable(false)]
        public new string IconName { get { return "iconEnumItem"; } }
        //protected override string GetNodeIconName() { return "iconEnumItem"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
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
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        public void Remove()
        {
            this.ParentEnumeration.ListEnumerationPairs.Remove(this);
        }
        partial void OnIsDefaultChanged()
        {
            if (this.IsDefault)
            {
                var p = this.ParentEnumeration;
                foreach (var t in p.ListEnumerationPairs)
                {
                    if ((this.Guid != t.Guid) && t.IsDefault)
                    {
                        t.IsDefault = false;
                    }
                }
            }
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Parent)
            };
            return lst.ToArray();
        }
    }
}
