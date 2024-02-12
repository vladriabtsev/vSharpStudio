using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Form : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [Browsable(false)]
        public GroupListForms ParentGroupListForms { get { Debug.Assert(this.Parent != null); return (GroupListForms)this.Parent; } }
        [Browsable(false)]
        public IGroupListForms ParentGroupListFormsI { get { Debug.Assert(this.Parent != null); return (IGroupListForms)this.Parent; } }

        public Form(ITreeConfigNode? parent, FormType ftype, List<IProperty> lst) : this(parent)
        {
            this._ListProperties = lst;
            switch (ftype)
            {
                case FormType.ListDataGrid:
                case FormType.ListComboBox:
                    this._Name = $"View{Enum.GetName(typeof(FormType), ftype)}";
                    break;
                default:
                    throw new NotImplementedException();
            }
            this._EnumFormType = ftype;
        }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListForms.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        //partial void OnCreate()
        //{
        //}
        partial void OnCreated()
        {
            foreach (var t in Enum.GetValues<FormType>())
            {
                var f = (from p in this.ParentGroupListForms.ListForms
                         where p.EnumFormType == t
                         select p).SingleOrDefault();
                if (f == null)
                {
                    if (t == FormType.FormTypeNotSelected)
                        continue;
                    this._EnumFormType = t;
                    break;
                }
            }
            this.IsIncludableInModels = true;
            Init();
            //this.PropertyChanging += Form_PropertyChanging;
            this.PropertyChanged += Form_PropertyChanged;
            this._ListSeparateTreeAllNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
            this._ListSeparateTreeSelectedNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
            this._ListSeparateTreeSelectedNotSpecialProperties.CollectionChanged += _ListSeparateTreeSelectedNotSpecialProperties_CollectionChanged;
            this._ListAllNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
            this._ListSelectedNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
            this._ListSelectedNotSpecialProperties.CollectionChanged += _ListSelectedNotSpecialProperties_CollectionChanged;
        }

        //private void Form_PropertyChanging(object? sender, PropertyChangingEventArgs e)
        //{
        //    switch(e.PropertyName)
        //    {
        //        default:
        //            break;
        //    }
        //}
        private void Form_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.EnumFormType):
                    if (this.EnumFormType == FormType.FormTypeNotSelected)
                        return;
                    var cnt = (from p in this.ParentGroupListForms.ListForms
                               where p.EnumFormType == this.EnumFormType
                               select p).Count();
                    if (cnt > 1)
                        MessageBox.Show($"List forms already contains '{Enum.GetName<FormType>(this.EnumFormType)}' form type", "Warning", System.Windows.MessageBoxButton.OK);
                    break;
                default:
                    break;
            }
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
            this.GridSystem.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListForms.ListForms.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Form?)this.ParentGroupListForms.ListForms.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListForms.ListForms.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListForms.ListForms.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Form?)this.ParentGroupListForms.ListForms.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListForms.ListForms.MoveDown(this);
            this.SetSelected(this);
        }

        //partial void OnIsHasMarkedForDeletionChanged()
        //{
        //    if (this.IsHasMarkedForDeletion)
        //    {
        //        (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
        //    }
        //    else
        //    {
        //        var p = (this.Parent as INewAndDeleteion);
        //        p.GetIsHasMarkedForDeletion();
        //    }
        //}
        //partial void OnIsHasNewChanged()
        //{
        //    if (this.IsHasNew)
        //    {
        //        (this.Parent as INewAndDeleteion).IsHasNew = true;
        //    }
        //    else
        //    {
        //        var p = (this.Parent as INewAndDeleteion);
        //        p.GetIsHasNew();
        //    }
        //}

        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Form.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListForms.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            Debug.Assert(this.Parent != null);
            var node = new Form(this.Parent);
            this.ParentGroupListForms.Add(node);
            this.GetUniqueName(Defaults.FormName, node, this.ParentGroupListForms.ListForms);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListForms.ListForms.Remove(this);
        }
        #endregion Tree operations

        #region Visibility
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            Debug.Assert(this.Parent != null);
            if (this.Parent.Parent is ICatalog)
            {
                var c = (Catalog)this.Parent.Parent;
                if (!c.GetUseCodeProperty())
                    lst.Add(nameof(this.IsUseCode));
                if (!c.GetUseNameProperty())
                    lst.Add(nameof(this.IsUseName));
                if (!c.GetUseDescriptionProperty())
                    lst.Add(nameof(this.IsUseDesc));
                if (!c.UseTree || !(c.UseTree && c.UseSeparateTreeForFolders))
                {
                    lst.Add(nameof(this.IsUseFolderCode));
                    lst.Add(nameof(this.IsUseFolderName));
                    lst.Add(nameof(this.IsUseFolderDesc));
                }
                else
                {
                    if (!c.Folder.GetUseCodeProperty())
                        lst.Add(nameof(this.IsUseFolderCode));
                    if (!c.Folder.GetUseNameProperty())
                        lst.Add(nameof(this.IsUseFolderName));
                    if (!c.Folder.GetUseDescriptionProperty())
                        lst.Add(nameof(this.IsUseFolderDesc));
                }
                lst.Add(nameof(this.IsUseDocDate));
            }
            else if (this.Parent.Parent is IDetail)
            {
                var c = (IDetail)this.Parent.Parent;
                lst.Add(nameof(this.IsUseCode));
                lst.Add(nameof(this.IsUseName));
                lst.Add(nameof(this.IsUseDesc));
                lst.Add(nameof(this.IsUseFolderCode));
                lst.Add(nameof(this.IsUseFolderName));
                lst.Add(nameof(this.IsUseFolderDesc));
                lst.Add(nameof(this.IsUseDocDate));
            }
            else if (this.Parent.Parent is IDocument)
            {
                lst.Add(nameof(this.EnumFormType));
                lst.Add(nameof(this.IsUseName));
                lst.Add(nameof(this.IsUseDesc));
                lst.Add(nameof(this.IsUseFolderCode));
                lst.Add(nameof(this.IsUseFolderName));
                lst.Add(nameof(this.IsUseFolderDesc));
            }
            else if (this.Parent.Parent is IRegister)
            {
                var c = (IRegister)this.Parent.Parent;
                lst.Add(nameof(this.IsUseCode));
                lst.Add(nameof(this.IsUseName));
                lst.Add(nameof(this.IsUseDesc));
                lst.Add(nameof(this.IsUseFolderCode));
                lst.Add(nameof(this.IsUseFolderName));
                lst.Add(nameof(this.IsUseFolderDesc));
                lst.Add(nameof(this.IsUseDocDate));
            }
            else
            {
                Debug.Assert(false);
            }
            switch (this.EnumFormType)
            {
                case FormType.ListComboBox:
                case FormType.ListDataGrid:
                    lst.Add(nameof(this.IsDummy));
                    break;
                case FormType.ItemEditForm:
                case FormType.FolderEditForm:
                    break;
                default:
                    lst.Add(nameof(this.IsUseCode));
                    lst.Add(nameof(this.IsUseName));
                    lst.Add(nameof(this.IsUseDesc));
                    lst.Add(nameof(this.IsUseFolderCode));
                    lst.Add(nameof(this.IsUseFolderName));
                    lst.Add(nameof(this.IsUseFolderDesc));
                    break;
            }
            return lst.ToArray();
        }
        #endregion Visibility

        [Browsable(false)]
        public IReadOnlyList<IProperty> ListProperties
        {
            get
            {
                if (this._ListProperties == null)
                {
                    this._ListProperties = new List<IProperty>();
                    if (this.ParentGroupListForms.Parent is Detail dt)
                    {
                        dt.GetSpecialProperties(this._ListProperties, false);
                    }
                    else if (this.ParentGroupListForms.Parent is Catalog c)
                    {
                        c.GetSpecialProperties(this._ListProperties, false);
                    }
                    else if (this.ParentGroupListForms.Parent is Document d)
                    {
                        d.GetSpecialProperties(this._ListProperties, false);
                    }
                    else if (this.ParentGroupListForms.Parent is CatalogFolder cf)
                    {
                        cf.GetSpecialProperties(this._ListProperties, false);
                    }
                    else
                        throw new NotImplementedException();
                }
                return this._ListProperties;
            }
        }
        private List<IProperty>? _ListProperties = null;

        #region Editor

        private bool isOnOpeningEditor = false;
        public override void OnOpeningEditor()
        {
            this.isOnOpeningEditor = true;

            #region ListSeparateTreeAllNotSpecialProperties
            if (this.ParentGroupListForms.Parent is Catalog c2)
            {
                if (c2.UseTree && c2.UseSeparateTreeForFolders)
                {
                    this._ListSeparateTreeAllNotSpecialProperties.Clear();
                    var res = new List<IProperty>();
                    c2.Folder.GetNormalProperties(res);
                    foreach (var t in res)
                    {
                        bool notFound = true;
                        foreach (var tt in this.ListGuidViewFolderProperties)
                        {
                            if (tt == t.Guid)
                            {
                                notFound = false;
                                break;
                            }
                        }
                        if (notFound)
                            this._ListSeparateTreeAllNotSpecialProperties.Add(t);
                    }
                }
            }
            else if (this.ParentGroupListForms.Parent is Document d2)
            {
            }
            else if (this.ParentGroupListForms.Parent is Detail dt2)
            {
            }
            else if (this.ParentGroupListForms.Parent is Register r2)
            {
            }
            else
            {
                Debug.Assert(false, "Not implemented");
            }

            #endregion ListSeparateTreeAllNotSpecialProperties

            #region ListSeparateTreeSelectedNotSpecialProperties
            this._ListSeparateTreeSelectedNotSpecialProperties.Clear();
            if (this.ParentGroupListForms.Parent is Catalog c1)
            {
                if (c1.UseTree && c1.UseSeparateTreeForFolders)
                {
                    this._ListSeparateTreeSelectedNotSpecialProperties.Clear();
                    var res = new List<IProperty>();
                    c1.Folder.GetNormalProperties(res);
                    foreach (var t in res)
                    {
                        foreach (var tt in this.ListGuidViewFolderProperties)
                        {
                            if (tt == t.Guid)
                            {
                                this._ListSeparateTreeSelectedNotSpecialProperties.Add(t);
                                break;
                            }
                        }
                    }
                }
            }
            else if (this.ParentGroupListForms.Parent is Document d1)
            {
            }
            else if (this.ParentGroupListForms.Parent is Detail dt1)
            {
            }
            else if (this.ParentGroupListForms.Parent is Register r1)
            {
            }
            else
            {
                Debug.Assert(false, "Not implemented");
            }
            #endregion ListSeparateTreeSelectedNotSpecialProperties

            #region ListAllNotSpecialProperties
            if (this.ParentGroupListForms.Parent is Catalog c4)
            {
                this._ListAllNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                c4.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    var found = false;
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        lst.Add(t);
                }
                this._ListAllNotSpecialProperties.AddRange(lst);
            }
            else if (this.ParentGroupListForms.Parent is Document d4)
            {
                this._ListAllNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                d4.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    var found = false;
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        lst.Add(t);
                }
                this._ListAllNotSpecialProperties.AddRange(lst);
            }
            else if (this.ParentGroupListForms.Parent is Detail dt4)
            {
                this._ListAllNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                dt4.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    var found = false;
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        lst.Add(t);
                }
                this._ListAllNotSpecialProperties.AddRange(lst);
            }
            else if (this.ParentGroupListForms.Parent is Register r4)
            {
                this._ListAllNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                r4.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    var found = false;
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        lst.Add(t);
                }
                this._ListAllNotSpecialProperties.AddRange(lst);
            }
            else
            {
                Debug.Assert(false, "Not implemented");
            }
            #endregion ListAllNotSpecialProperties

            #region ListSelectedNotSpecialProperties
            if (this.ParentGroupListForms.Parent is Catalog c5)
            {
                this._ListSelectedNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                c5.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            lst.Add(t);
                            break;
                        }
                    }
                }
                this._ListSelectedNotSpecialProperties.AddRange(lst);
            }
            else if (this.ParentGroupListForms.Parent is Document d5)
            {
                this._ListSelectedNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                d5.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            lst.Add(t);
                            break;
                        }
                    }
                }
                this._ListSelectedNotSpecialProperties.AddRange(lst);
            }
            else if (this.ParentGroupListForms.Parent is Detail dt5)
            {
                this._ListSelectedNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                dt5.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            lst.Add(t);
                            break;
                        }
                    }
                }
                this._ListSelectedNotSpecialProperties.AddRange(lst);
            }
            else if (this.ParentGroupListForms.Parent is Register r5)
            {
                this._ListSelectedNotSpecialProperties.Clear();
                var res = new List<IProperty>();
                r5.GetNormalProperties(res);
                var lst = new List<ISortingValue>();
                foreach (var t in res)
                {
                    foreach (var tt in this.ListGuidViewProperties)
                    {
                        if (tt == t.Guid)
                        {
                            lst.Add(t);
                            break;
                        }
                    }
                }
                this._ListSelectedNotSpecialProperties.AddRange(lst);
            }
            else
            {
                Debug.Assert(false, "Not implemented");
            }
            #endregion ListSelectedNotSpecialProperties

            this.isOnOpeningEditor = false;
        }

        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListSeparateTreeAllNotSpecialProperties
        {
            get => _ListSeparateTreeAllNotSpecialProperties;
            set => SetProperty(ref _ListSeparateTreeAllNotSpecialProperties, value);
        }
        private SortedObservableCollection<ISortingValue> _ListSeparateTreeAllNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListSeparateTreeSelectedNotSpecialProperties
        {
            get => _ListSeparateTreeSelectedNotSpecialProperties;
            set => SetProperty(ref _ListSeparateTreeSelectedNotSpecialProperties, value);
        }
        private void _ListSeparateTreeSelectedNotSpecialProperties_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.isOnOpeningEditor)
                return;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (var t in e.NewItems)
                        {
                            this.ListGuidViewFolderProperties.Add(((IProperty)t).Guid);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var t in e.OldItems)
                        {
                            this.ListGuidViewFolderProperties.Remove(((IProperty)t).Guid);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private SortedObservableCollection<ISortingValue> _ListSeparateTreeSelectedNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListAllNotSpecialProperties
        {
            get => _ListAllNotSpecialProperties;
            set => SetProperty(ref _ListAllNotSpecialProperties, value);
        }
        private SortedObservableCollection<ISortingValue> _ListAllNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListSelectedNotSpecialProperties
        {
            get => _ListSelectedNotSpecialProperties;
            set => SetProperty(ref _ListSelectedNotSpecialProperties, value);
        }
        private void _ListSelectedNotSpecialProperties_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.isOnOpeningEditor)
                return;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
#if DEBUG
                        // Chack new item is not added yet
                        foreach (var t in e.NewItems)
                        {
                            var guid = ((IGuid)t).Guid;
                            var j = -1;
                            for (int i = 0; i < this.ListGuidViewProperties.Count; i++)
                            {
                                if (this.ListGuidViewProperties[i] == guid)
                                {
                                    j = i;
                                    break;
                                }
                            }
                            Debug.Assert(j == -1);
                        }
#endif
                        foreach (var t in e.NewItems)
                        {
                            this.ListGuidViewProperties.Add(((IGuid)t).Guid);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var t in e.OldItems)
                        {
                            var guid = ((IGuid)t).Guid;
                            var j = -1;
                            for (int i = 0; i < this.ListGuidViewProperties.Count; i++)
                            {
                                if (this.ListGuidViewProperties[i] == guid)
                                {
                                    j = i;
                                    break;
                                }
                            }
                            Debug.Assert(j >= 0);
                            this.ListGuidViewProperties.RemoveAt(j);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private SortedObservableCollection<ISortingValue> _ListSelectedNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
        partial void OnEnumFormTypeChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
            this.OnPropertyChanged(nameof(this.IsListForm));
        }
        [Browsable(false)]
        public bool IsListForm
        {
            get
            {
                return this.EnumFormType == FormType.ListDataGrid || this.EnumFormType == FormType.ListComboBox;
            }
        }
        [Browsable(false)]
        public bool UseSeparateTreeForFolders
        {
            get
            {
                if (this.ParentGroupListForms.Parent is Catalog c)
                {
                    return c.UseTree && c.UseSeparateTreeForFolders;
                }
                return false;
            }
        }
        [Browsable(false)]
        public bool UseSelfTreeForFolders
        {
            get
            {
                if (this.ParentGroupListForms.Parent is Catalog c)
                {
                    return c.UseTree && !c.UseSeparateTreeForFolders;
                }
                return false;
            }
        }
        [Browsable(false)]
        public string IsFolderPropertyName { get { return this.Cfg.Model.PropertyIsFolderName; } }
        [Browsable(false)]
        public bool UseFolderTypeExplicitly
        {
            get
            {
                if (this.ParentGroupListForms.Parent is Catalog c)
                {
                    return c.UseTree && !c.UseSeparateTreeForFolders;
                }
                return false;
            }
        }

        #endregion Editor
    }
}
