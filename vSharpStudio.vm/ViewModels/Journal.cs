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
    [DebuggerDisplay("Journal:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Journal : ICanAddNode, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [Browsable(false)]
        public GroupListJournals ParentGroupListJournals { get { Debug.Assert(this.Parent != null); return (GroupListJournals)this.Parent; } }
        [Browsable(false)]
        public IGroupListJournals ParentGroupListJournalsI { get { Debug.Assert(this.Parent != null); return (IGroupListJournals)this.Parent; } }
        public static readonly string DefaultName = "Journal";

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
            this.GetUniqueName(Journal.DefaultName, node, this.ParentGroupListJournals.ListJournals);
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
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
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
            get
            {
                if (this.listNotIncludedDocuments == null)
                {
                    var hashDoc = new HashSet<string>();
                    foreach (var tt in this.ListSelectedDocsWithProperties)
                    {
                        hashDoc.Add(tt.Guid);
                    }
                    this.listNotIncludedDocuments = new ObservableCollection<ISortingValue>();
                    foreach (var t in this.ParentGroupListJournals.ParentModel.GroupDocuments.GroupListDocuments.ListDocuments)
                    {
                        if (hashDoc.Contains(t.Guid))
                            continue;
                        this.listNotIncludedDocuments.Add(t);
                    }
                }
                return this.listNotIncludedDocuments;
            }
        }
        private ObservableCollection<ISortingValue>? listNotIncludedDocuments;
        [Browsable(false)]
        public ObservableCollection<ISortingValue> ListIncludedDocuments
        {
            get
            {
                if (this.listIncludedDocuments == null)
                {
                    var hashDoc = new HashSet<string>();
                    foreach (var tt in this.ListSelectedDocsWithProperties)
                    {
                        hashDoc.Add(tt.Guid);
                    }
                    this.listIncludedDocuments = new ObservableCollection<ISortingValue>();
                    foreach (var t in this.ParentGroupListJournals.ParentModel.GroupDocuments.GroupListDocuments.ListDocuments)
                    {
                        if (!hashDoc.Contains(t.Guid))
                            continue;
                        this.listIncludedDocuments.Add(t);
                    }
                }
                return this.listIncludedDocuments;
            }
        }
        private ObservableCollection<ISortingValue>? listIncludedDocuments;
        [Browsable(false)]
        public IDocument? SelectedIncludedDocument
        {
            get { return selectedIncludedDocument; }
            set
            {
                selectedIncludedDocument = value;
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged(() => this.ListNotIncludedProperties);
                this.NotifyPropertyChanged(() => this.ListIncludedProperties);
            }
        }
        private IDocument? selectedIncludedDocument;

        #endregion Documents

        #region Properties

        [Browsable(false)]
        public ObservableCollection<ISortingValue> ListNotIncludedProperties
        {
            get
            {
                this.GetNotIncludedProperties();
                Debug.Assert(this.listNotIncludedProperties != null);
                return this.listNotIncludedProperties;
            }
        }
        private ObservableCollection<ISortingValue>? listNotIncludedProperties;
        private void GetNotIncludedProperties()
        {
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
                        this.listNotIncludedProperties = new ObservableCollection<ISortingValue>();
                        foreach (var tt in ((Document)this.Cfg.DicNodes[t.Guid]).GetPropertiesWithShared(false, true))
                        {
                            if (hashProps.Contains(tt.Guid))
                                continue;
                            this.listNotIncludedProperties.Add(tt);
                        }
                        break;
                    }
                }
            }
            else
                this.listNotIncludedProperties = new ObservableCollection<ISortingValue>();
        }
        [Browsable(false)]
        public ObservableCollection<ISortingValue> ListIncludedProperties
        {
            get
            {
                this.GetIncludedProperties();
                Debug.Assert(this.listIncludedProperties != null);
                return this.listIncludedProperties;
            }
        }
        private ObservableCollection<ISortingValue>? listIncludedProperties;
        private void GetIncludedProperties()
        {
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
                        this.listIncludedProperties = new ObservableCollection<ISortingValue>();
                        foreach (var tt in ((Document)this.Cfg.DicNodes[t.Guid]).GetPropertiesWithShared(false, true))
                        {
                            if (!hashProps.Contains(tt.Guid))
                                continue;
                            this.listIncludedProperties.Add(tt);
                        }
                        break;
                    }
                }
            }
            else
                this.listIncludedProperties = new ObservableCollection<ISortingValue>();
        }
        [Browsable(false)]
        public IProperty? SelectedNotIncludedProperty
        {
            get { return selectedNotIncludedProperty; }
            set
            {
                selectedNotIncludedProperty = value;
                this.NotifyPropertyChanged();
                this.SelectedIncludedDocument = null;
            }
        }
        private IProperty? selectedNotIncludedProperty;

        #endregion Properties

        #endregion Editor
    }
}
