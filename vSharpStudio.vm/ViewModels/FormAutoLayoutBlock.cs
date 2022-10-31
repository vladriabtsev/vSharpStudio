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
    public partial class FormAutoLayoutBlock : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public Form? ParentForm { get { return this.Parent as Form; } }
        [BrowsableAttribute(false)]
        public IForm? ParentFormI { get { return this.Parent as IForm; } }
        [BrowsableAttribute(false)]
        public FormAutoLayoutBlock? ParentFormAutoLayoutBlock { get { return this.Parent as FormAutoLayoutBlock; } }
        [BrowsableAttribute(false)]
        public IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get { return this.Parent as IFormAutoLayoutBlock; } }
        [BrowsableAttribute(false)]
        public FormTabControlTab? ParentFormTabControlTab { get { return this.Parent as FormTabControlTab; } }
        [BrowsableAttribute(false)]
        public IFormTabControlTab? ParentFormTabControlTabI { get { return this.Parent as IFormTabControlTab; } }
        [BrowsableAttribute(false)]
        public FormGridSystemColumn? ParentFormGridSystemColumn { get { return this.Parent as FormGridSystemColumn; } }
        [BrowsableAttribute(false)]
        public IFormGridSystemColumn? ParentFormGridSystemColumnI { get { return this.Parent as IFormGridSystemColumn; } }
        public static readonly string DefaultName = "Auto Layout Block";

        #region ITree

        public override IEnumerable<ITreeConfigNodeSortable> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNodeSortable> GetListSiblings()
        {
            if (this.ParentFormAutoLayoutBlock != null)
                return this.ParentFormAutoLayoutBlock.Children;
            //if (this.ParentFormTabControlTab != null)
            //    return this.ParentFormTabControlTab.Children;
            if (this.ParentFormGridSystemColumn != null)
                return this.ParentFormGridSystemColumn.Children;
            throw new NotImplementedException();
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
            this._Name = "AutoLayoutBlock";
            this._Description = "Auto Layout Block";
            this.IsIncludableInModels = true;

            this.Children = new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.RefillChildren();
        }
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
        public dynamic Setting { get; set; }

        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            //lst.Add(this.GetPropertyName(() => this.Parent));
            //lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
