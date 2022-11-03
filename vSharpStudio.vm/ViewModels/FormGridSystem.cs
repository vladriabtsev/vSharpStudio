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
    public partial class FormGridSystem : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public Form ParentForm { get { return (Form)this.Parent; } }
        [BrowsableAttribute(false)]
        public IForm ParentFormI { get { return (IForm)this.Parent; } }
        [BrowsableAttribute(false)]
        public FormAutoLayoutBlock ParentFormAutoLayoutBlock { get { return (FormAutoLayoutBlock)this.Parent; } }
        [BrowsableAttribute(false)]
        public IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get { return (IFormAutoLayoutBlock)this.Parent; } }

        #region ITree

        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentForm.Children;
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
            this._Name = "Grid";
            this._Description = "Grid System";
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
        public dynamic Setting { get; set; }

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
