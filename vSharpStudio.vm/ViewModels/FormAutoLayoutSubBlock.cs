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
    [DebuggerDisplay("Grouping:{Name,nq} props:{GroupProperties.ListProperties.Count,nq} HasChanged:{IsHasChanged}")]
    public partial class FormAutoLayoutSubBlock : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public Form? ParentForm { get { Debug.Assert(this.Parent != null); return this.Parent as Form; } }
        [BrowsableAttribute(false)]
        public IForm? ParentFormI { get { Debug.Assert(this.Parent != null); return this.Parent as IForm; } }
        [BrowsableAttribute(false)]
        public FormAutoLayoutBlock? ParentFormAutoLayoutBlock { get { Debug.Assert(this.Parent != null); return this.Parent as FormAutoLayoutBlock; } }
        [BrowsableAttribute(false)]
        public IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get { Debug.Assert(this.Parent != null); return this.Parent as IFormAutoLayoutBlock; } }
        [BrowsableAttribute(false)]
        public FormTabControlTab? ParentFormTabControlTab { get { Debug.Assert(this.Parent != null); return this.Parent as FormTabControlTab; } }
        [BrowsableAttribute(false)]
        public IFormTabControlTab? ParentFormTabControlTabI { get { Debug.Assert(this.Parent != null); return this.Parent as IFormTabControlTab; } }
        [BrowsableAttribute(false)]
        public FormGridSystemColumn? ParentFormGridSystemColumn { get { Debug.Assert(this.Parent != null); return this.Parent as FormGridSystemColumn; } }
        [BrowsableAttribute(false)]
        public IFormGridSystemColumn? ParentFormGridSystemColumnI { get { Debug.Assert(this.Parent != null); return this.Parent as IFormGridSystemColumn; } }
        public static readonly string DefaultName = "Auto Layout Block";

        #region ITree

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
        new public string IconName { get { return "iconFolder"; } }
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
