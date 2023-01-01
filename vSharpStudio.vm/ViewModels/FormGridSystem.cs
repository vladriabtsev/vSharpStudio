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
    public partial class FormGridSystem : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, ITree
    {
        [BrowsableAttribute(false)]
        public Form? ParentForm { get { Debug.Assert(this.Parent != null); return this.Parent as Form; } }
        [BrowsableAttribute(false)]
        public IForm? ParentFormI { get { Debug.Assert(this.Parent != null); return this.Parent as IForm; } }
        [BrowsableAttribute(false)]
        public FormAutoLayoutBlock? ParentFormAutoLayoutBlock { get { Debug.Assert(this.Parent != null); return this.Parent as FormAutoLayoutBlock; } }
        [BrowsableAttribute(false)]
        public IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get { Debug.Assert(this.Parent != null); return this.Parent as IFormAutoLayoutBlock; } }

        #region ITree

        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            Debug.Assert(this.Parent != null);
            return this.Parent.GetListChildren();
        }
        #region Tree operations
        #endregion Tree operations

        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._Name = "Grid";
            this._Description = "Grid System";
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

        public FormGridSystem(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }

        public FormGridSystem(ITreeConfigNode parent, string name, List<FormGridSystemRow> listRows)
            : this(parent)
        {
            Debug.Assert(listRows != null);
            this.Name = name;
            this.ListRows.AddRange(listRows);
        }
        [ExpandableObjectAttribute()]
        public dynamic? Setting { get; set; }

        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            //lst.Add(this.GetPropertyName(() => this.Parent));
            //lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
        public IFormGridSystemRow AddGridRow(string name = "")
        {
            var node = new FormGridSystemRow(this) { Name = name };
            this.ListRows.Add(node);
            return node;
        }
    }
}
