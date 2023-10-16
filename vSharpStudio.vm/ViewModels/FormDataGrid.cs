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
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class FormDataGrid : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        [Browsable(false)]
        public FormAutoLayoutBlock ParentFormAutoLayoutBlock { get { Debug.Assert(this.Parent != null); return (FormAutoLayoutBlock)this.Parent; } }
        [Browsable(false)]
        public IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get { Debug.Assert(this.Parent != null); return (IFormAutoLayoutBlock)this.Parent; } }

        #region ITree

        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentFormAutoLayoutBlock.Children;
        }
        #region Tree operations
        #endregion Tree operations

        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._Name = "DataGrid";
            this._Description = "Data Grid";
            this.IsIncludableInModels = true;
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
            if (this.Children.Count > 0)
                return;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }

        //public FormGridSystemColumn(ITreeConfigNode parent, string name)
        //    : this(parent)
        //{
        //    this.Name = name;
        //}

        //public FormGridSystemColumn(ITreeConfigNode parent, string name, List<FormGridSystemColumn> listColumns)
        //    : this(parent)
        //{
        //    Debug.Assert(listColumns != null);
        //    this.Name = name;
        //    this.lis.ListColumns.AddRange(listColumns);
        //}
        //public FormGridSystemColumn AddGridColumn(string name)
        //{
        //    var node = new FormGridSystemColumn(this) { Name = name };
        //    this.ListColumns.Add(node);
        //    return node;
        //}


        [ExpandableObjectAttribute()]
        public dynamic? Setting { get; set; }

        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            //lst.Add(this.GetPropertyName(() => this.Parent));
            //lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
