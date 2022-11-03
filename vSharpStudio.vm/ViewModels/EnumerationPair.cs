using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : IEditableNode, INodeGenSettings
    {
        [BrowsableAttribute(false)]
        public Enumeration ParentEnumeration { get { return (Enumeration) this.Parent; } }
        [BrowsableAttribute(false)]
        public IEnumeration ParentEnumerationI { get { return (IEnumeration)this.Parent; } }

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentEnumeration.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree


        [Browsable(false)]
        new public string IconName { get { return "iconEnumItem"; } }
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
                var p = (Enumeration)this.Parent;
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
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            return lst.ToArray();
        }
    }
}
