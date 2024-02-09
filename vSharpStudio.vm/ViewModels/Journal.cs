using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Journal : ICanAddNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [Browsable(false)]
        public GroupListJournals ParentGroupListJournals { get { Debug.Assert(this.Parent != null); return (GroupListJournals)this.Parent; } }
        [Browsable(false)]
        public IGroupListJournals ParentGroupListJournalsI { get { Debug.Assert(this.Parent != null); return (IGroupListJournals)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListJournals.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            //    Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListMainViewForms.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListMainViewForms.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListMainViewForms.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListMainViewForms.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            //this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            //this.GroupForms.AddAllAppGenSettingsVmsToNode();
            //this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListJournals.ListJournals.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Journal?)this.ParentGroupListJournals.ListJournals.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            this.ParentGroupListJournals.ListJournals.MoveUp(this);
            this.SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListJournals.ListJournals.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Journal?)this.ParentGroupListJournals.ListJournals.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            this.ParentGroupListJournals.ListJournals.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Journal.Clone(this.ParentGroupListJournals, this, true, true);
            this.ParentGroupListJournals.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Journal(this.ParentGroupListJournals);
            this.ParentGroupListJournals.Add(node);
            this.GetUniqueName(Defaults.JournalName, node, this.ParentGroupListJournals.ListJournals);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListJournals.ListJournals.Remove(this);
        }
        #endregion Tree operations

        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
        public bool GetIsGridSortable()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentGroupListJournals.GetIsGridSortable();
        }
        public bool GetIsGridFilterable()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListJournals.GetIsGridFilterable();
        }
        public bool GetIsGridSortableCustom()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListJournals.GetIsGridSortableCustom();
        }

        #region Editor

        #region Documents

        [Browsable(false)]
        public ObservableCollection<ISortingValue> ListNotIncludedDocuments
        {
            get { return this.listNotIncludedDocuments; }
            set
            {
                SetProperty(ref this.listNotIncludedDocuments, value);
            }
        }
        private ObservableCollection<ISortingValue> listNotIncludedDocuments = new ObservableCollection<ISortingValue>();
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListIncludedDocuments
        {
            get { return this.listIncludedDocuments; }
            set
            {
                SetProperty(ref this.listIncludedDocuments, value);
            }
        }
        private SortedObservableCollection<ISortingValue> listIncludedDocuments = new SortedObservableCollection<ISortingValue>();
        [Browsable(false)]
        public IDocument? SelectedIncludedDocument
        {
            get { return selectedIncludedDocument; }
            set
            {
                if (value == null)
                {
                    if (selectedIncludedDocument != null)
                    {
                        foreach (var t in this.listIncludedDocuments)
                        {
                            if (((IDocument)t).Guid == selectedIncludedDocument.Guid)
                            {
                                return;
                            }
                        }
                    }
                }
                SetProperty(ref this.selectedIncludedDocument, value);
                this.GetNotIncludedProperties();
                this.GetIncludedProperties();
                if (value == null)
                {
                    this.SelectedDocumentTitle = "Included document is not selected...";
                }
                else
                {
                    this.SelectedDocumentTitle = $"'{value.Name}' properties to include:";
                }
            }
        }
        private IDocument? selectedIncludedDocument;
        [Browsable(false)]
        public string? SelectedDocumentTitle
        {
            get { return selectedDocumentTitle; }
            set
            {
                SetProperty(ref this.selectedDocumentTitle, value);
            }
        }
        private string? selectedDocumentTitle;

        #endregion Documents

        #region Properties

        [Browsable(false)]
        public ObservableCollection<ISortingValue> ListNotIncludedProperties
        {
            get { return this.listNotIncludedProperties; }
            set
            {
                SetProperty(ref this.listNotIncludedProperties, value);
            }
        }
        private ObservableCollection<ISortingValue> listNotIncludedProperties = new ObservableCollection<ISortingValue>();
        private void GetNotIncludedProperties()
        {
            var listNotIncludedProperties = new ObservableCollection<ISortingValue>();
            if (this.SelectedIncludedDocument != null)
            {
                var hashProps = new HashSet<string>();
                foreach (var t in this.ListSelectedDocsWithProperties)
                {
                    if (t.Guid == this.SelectedIncludedDocument.Guid)
                    {
                        foreach (var tt in t.ListPropertyGuids)
                        {
                            hashProps.Add(tt);
                        }
                        break;
                    }
                }
                foreach (var tt in ((IDocument)this.Cfg.DicNodes[this.SelectedIncludedDocument.Guid]).GetPropertiesWithShared(false, true))
                {
                    if (hashProps.Contains(tt.Guid))
                        continue;
                    listNotIncludedProperties.Add(tt);
                }
            }
            this.ListNotIncludedProperties = listNotIncludedProperties;
        }
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListIncludedProperties
        {
            get { return this.listIncludedProperties; }
            set
            {
                SetProperty(ref this.listIncludedProperties, value);
            }
        }
        private SortedObservableCollection<ISortingValue> listIncludedProperties = new SortedObservableCollection<ISortingValue>();
        private void GetIncludedProperties()
        {
            var listIncludedProperties = new SortedObservableCollection<ISortingValue>();
            if (this.SelectedIncludedDocument != null)
            {
                foreach (var t in this.ListSelectedDocsWithProperties)
                {
                    if (t.Guid == this.SelectedIncludedDocument.Guid)
                    {
                        var hashProps = new HashSet<string>();
                        foreach (var tt in t.ListPropertyGuids)
                        {
                            hashProps.Add(tt);
                        }
                        foreach (var tt in ((Document)this.Cfg.DicNodes[t.Guid]).GetPropertiesWithShared(false, true))
                        {
                            if (!hashProps.Contains(tt.Guid))
                                continue;
                            listIncludedProperties.Add(tt);
                        }
                        break;
                    }
                }
            }
            this.ListIncludedProperties = listIncludedProperties;
        }
        [Browsable(false)]
        public IProperty? SelectedNotIncludedProperty
        {
            get { return selectedNotIncludedProperty; }
            set
            {
                if (SetProperty(ref this.selectedNotIncludedProperty, value))
                {
                    this.SelectedIncludedDocument = null;
                }
            }
        }
        private IProperty? selectedNotIncludedProperty;

        #endregion Properties

        #endregion Editor
    }
}
