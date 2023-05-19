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
    [DebuggerDisplay("Grouping:{Name,nq} props:{GroupProperties.ListProperties.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class FormAutoLayoutBlock : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        [Browsable(false)]
        public Form? ParentForm { get { Debug.Assert(this.Parent != null); return this.Parent as Form; } }
        [Browsable(false)]
        public IForm? ParentFormI { get { Debug.Assert(this.Parent != null); return this.Parent as IForm; } }
        [Browsable(false)]
        public FormAutoLayoutBlock? ParentFormAutoLayoutBlock { get { Debug.Assert(this.Parent != null); return this.Parent as FormAutoLayoutBlock; } }
        [Browsable(false)]
        public IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get { Debug.Assert(this.Parent != null); return this.Parent as IFormAutoLayoutBlock; } }
        [Browsable(false)]
        public FormTabControlTab? ParentFormTabControlTab { get { Debug.Assert(this.Parent != null); return this.Parent as FormTabControlTab; } }
        [Browsable(false)]
        public IFormTabControlTab? ParentFormTabControlTabI { get { Debug.Assert(this.Parent != null); return this.Parent as IFormTabControlTab; } }
        [Browsable(false)]
        public FormGridSystemColumn? ParentFormGridSystemColumn { get { Debug.Assert(this.Parent != null); return this.Parent as FormGridSystemColumn; } }
        [Browsable(false)]
        public IFormGridSystemColumn? ParentFormGridSystemColumnI { get { Debug.Assert(this.Parent != null); return this.Parent as IFormGridSystemColumn; } }

        #region ITree

        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            if (this.ParentFormAutoLayoutBlock != null)
                return this.ParentFormAutoLayoutBlock.Children;
            //if (this.ParentFormTabControlTab != null)
            //    return this.ParentFormTabControlTab.Children;
            if (this.ParentFormGridSystemColumn != null)
                return this.ParentFormGridSystemColumn.Children;
            throw new NotImplementedException();
        }
        #region Tree operations
        #endregion Tree operations

        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._Name = "AutoLayoutBlock";
            this._Description = "Auto Layout Block";
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
            VmBindable.IsNotifyingStatic = false;
            VmBindable.IsNotifyingStatic = true;
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


        [ExpandableObjectAttribute()]
        public dynamic? Setting { get; set; }

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
            this.Children.Add(node);
            return node;
        }
        public IFormDataGrid AddDataGrid(string name = "")
        {
            var node = new FormDataGrid(this) { Name = name };
            this.Children.Add(node);
            return node;
        }
        public IFormField AddField(string name = "")
        {
            var node = new FormField(this) { Name = name };
            this.Children.Add(node);
            return node;
        }
        public IFormGridSystem AddGridSystem(string name = "")
        {
            var node = new FormGridSystem(this) { Name = name };
            this.Children.Add(node);
            return node;
        }
        public IFormTabControl AddTabControl(string name = "")
        {
            var node = new FormTabControl(this) { Name = name };
            this.Children.Add(node);
            return node;
        }
        public IFormTree AddTree(string name = "")
        {
            var node = new FormTree(this) { Name = name };
            this.Children.Add(node);
            return node;
        }
    }
}
