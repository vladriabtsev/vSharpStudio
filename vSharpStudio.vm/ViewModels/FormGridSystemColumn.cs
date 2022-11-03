using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Grouping:{Name,nq} props:{GroupProperties.ListProperties.Count,nq}")]
    public partial class FormGridSystemColumn : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public FormGridSystemRow ParentFormGridSystemRow { get { return (FormGridSystemRow)this.Parent; } }
        [BrowsableAttribute(false)]
        public IFormGridSystemRow ParentFormGridSystemRowI { get { return (IFormGridSystemRow)this.Parent; } }

        #region ITree

        public override IEnumerable<ITreeConfigNodeSortable> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNodeSortable> GetListSiblings()
        {
            return this.ParentFormGridSystemRow.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        public ConfigNodesCollection<ITreeConfigNodeSortable> Children { get; private set; }
        #region Tree operations
        #endregion Tree operations

        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._Name = "Column";
            this._Description = "Grid System Column";
            this.IsIncludableInModels = true;

            this.Children = new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.RefillChildren();
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            this.RefillChildren();
        }
        void RefillChildren()
        {
            VmBindable.IsNotifyingStatic = false;
            this.Children.Clear();
            VmBindable.IsNotifyingStatic = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }

        public FormGridSystemColumn(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }
        [ExpandableObjectAttribute()]
        public dynamic Setting { get; set; }

        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            //lst.Add(this.GetPropertyName(() => this.Parent));
            //lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
        public IFormAutoLayoutBlock AddAutoLayoutBlock(string name = "")
        {
            var node = new FormAutoLayoutBlock(this) { Name = name };
            this.FormBlock = node;
            return node;
        }
    }
}
