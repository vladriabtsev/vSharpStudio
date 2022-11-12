using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Documents;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Form:{Name,nq}")]
    public partial class Form : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public GroupListForms ParentGroupListForms { get { Debug.Assert(this.Parent != null); return (GroupListForms)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListForms ParentGroupListFormsI { get { Debug.Assert(this.Parent != null); return (IGroupListForms)this.Parent; } }

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
        new public string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        //partial void OnCreate()
        //{
        //}
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            Init();
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
            var prev = (Form)this.ParentGroupListForms.ListForms.GetPrev(this);
            if (prev == null)
            {
                return;
            }

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
            var next = (Form)this.ParentGroupListForms.ListForms.GetNext(this);
            if (next == null)
            {
                return;
            }

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
        //public bool GetUseCodeProperty()
        //{
        //    bool res = false;
        //    if (this.ParentGroupListForms.Parent
        //    if (this.UseCodeProperty.HasValue && this.UseCodeProperty.Value)
        //        res = true;
        //    else
        //        res = this.ParentGroupListCatalogs.UseCodeProperty;
        //    return res;
        //}
        //public bool GetUseNameProperty()
        //{
        //    bool res = false;
        //    if (this.UseNameProperty.HasValue && this.UseNameProperty.Value)
        //        res = true;
        //    else
        //        res = this.ParentGroupListCatalogs.UseNameProperty;
        //    return res;
        //}
        //public bool GetUseDescriptionProperty()
        //{
        //    bool res = false;
        //    if (this.UseDescriptionProperty.HasValue && this.UseDescriptionProperty.Value)
        //        res = true;
        //    else
        //        res = this.ParentGroupListCatalogs.UseDescriptionProperty;
        //    return res;
        //}
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            Debug.Assert(this.Parent != null);
            if (this.Parent.Parent is ICatalog)
            {
                var c = (Catalog)this.Parent.Parent;
                if (!c.GetUseCodeProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseCode));
                if (!c.GetUseNameProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseName));
                if (!c.GetUseDescriptionProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseDesc));
                if (!c.UseTree || !(c.UseTree && c.UseSeparateTreeForFolders))
                {
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                }
                else
                {
                    if (!c.GetUseCodePropertySeparateFolder())
                        lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                    if (!c.GetUseNamePropertySeparateFolder())
                        lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                    if (!c.GetUseDescriptionPropertSeparateFoldery())
                        lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                }
                lst.Add(this.GetPropertyName(() => this.IsUseDocDate));
            }
            else if (this.Parent.Parent is IDetail)
            {
                var c = (IDetail)this.Parent.Parent;
                lst.Add(this.GetPropertyName(() => this.IsUseCode));
                lst.Add(this.GetPropertyName(() => this.IsUseName));
                lst.Add(this.GetPropertyName(() => this.IsUseDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseDocDate));
            }
            else if (this.Parent.Parent is IDocument)
            {
                lst.Add(this.GetPropertyName(() => this.EnumFormType));
                lst.Add(this.GetPropertyName(() => this.IsUseName));
                lst.Add(this.GetPropertyName(() => this.IsUseDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
            }
            else
            {
                Debug.Assert(false);
            }
            switch (this.EnumFormType)
            {
                case FormType.ViewListNarrow:
                case FormType.ViewListWide:
                    lst.Add(this.GetPropertyName(() => this.IsDummy));
                    break;
                case FormType.ItemEditForm:
                case FormType.FolderEditForm:
                    break;
                default:
                    Debug.Assert(false);
                    lst.Add(this.GetPropertyName(() => this.IsUseCode));
                    lst.Add(this.GetPropertyName(() => this.IsUseName));
                    lst.Add(this.GetPropertyName(() => this.IsUseDesc));
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                    break;
            }
            return lst.ToArray();
        }
        partial void OnEnumFormTypeChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        }
        #endregion Visibility

        #region Editor

        [BrowsableAttribute(false)]
        public SortedObservableCollection<IProperty> ListViewFolderNotSpecialProperties
        {
            get
            {
                listViewFolderNotSpecialProperties = new SortedObservableCollection<IProperty>();
                if (this.ParentGroupListForms.Parent is Catalog c)
                {
                    if (c.UseTree && c.UseSeparateTreeForFolders)
                    {
                        foreach (var t in c.Folder.GroupProperties.ListProperties)
                        {
                            foreach (var tt in this.ListGuidViewProperties)
                            {
                                if (tt == t.Guid)
                                {
                                    listViewFolderNotSpecialProperties.Add(t);
                                    break;
                                }
                            }
                        }
                    }
                }
                return listViewFolderNotSpecialProperties;
            }
        }
        private SortedObservableCollection<IProperty>? listViewFolderNotSpecialProperties;
        [BrowsableAttribute(false)]
        public ObservableCollection<IProperty> ListAllFolderNotSpecialProperties
        {
            get
            {
                listAllFolderNotSpecialProperties = new ObservableCollection<IProperty>();
                if (this.ParentGroupListForms.Parent is Catalog c)
                {
                    if (c.UseTree && c.UseSeparateTreeForFolders)
                    {
                        var res = new List<IProperty>();
                        c.GetSpecialProperties(res, true, false, false);
                        foreach (var t in c.Folder.GroupProperties.ListProperties)
                        {
                            res.Add(t);
                        }
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
                                listAllFolderNotSpecialProperties.Add(t);
                        }
                        listAllFolderNotSpecialProperties.CollectionChanged += ResFolder_CollectionChanged;
                    }
                }
                return listAllFolderNotSpecialProperties;
            }
        }
        private ObservableCollection<IProperty>? listAllFolderNotSpecialProperties = null;
        private void ResFolder_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.Assert(this.listViewFolderNotSpecialProperties != null);
            this.ListGuidViewFolderProperties.Clear();
            foreach (var t in this.listViewFolderNotSpecialProperties)
            {
                this.ListGuidViewFolderProperties.Add(t.Guid);
            }
        }
        [BrowsableAttribute(false)]
        public ObservableCollection<IProperty> ListAllNotSpecialProperties
        {
            get
            {
                var listAllNotSpecialProperties = new ObservableCollection<IProperty>();
                if (this.ParentGroupListForms.Parent is Catalog c)
                {
                    var res = new List<IProperty>();
                    c.GetSpecialProperties(res, true, false, false);
                    foreach (var t in c.GroupProperties.ListProperties)
                    {
                        res.Add(t);
                    }
                    foreach (var t in res)
                    {
                        bool notFound = true;
                        foreach (var tt in this.ListGuidViewProperties)
                        {
                            if (tt == t.Guid)
                            {
                                notFound = false;
                                break;
                            }
                        }
                        if (notFound)
                            listAllNotSpecialProperties.Add(t);
                    }
                }
                return listAllNotSpecialProperties;
            }
        }
        [BrowsableAttribute(false)]
        public SortedObservableCollection<IProperty> ListViewNotSpecialProperties
        {
            get
            {
                listViewNotSpecialProperties = new SortedObservableCollection<IProperty>();
                if (this.ParentGroupListForms.Parent is Catalog c)
                {
                    var lst = new List<IProperty>();
                    foreach (var t in c.GroupProperties.ListProperties)
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
                    listViewNotSpecialProperties.AddRange(lst);
                    listViewNotSpecialProperties.CollectionChanged += Res_CollectionChanged;
                }
                return listViewNotSpecialProperties;
            }
        }
        private SortedObservableCollection<IProperty>? listViewNotSpecialProperties;
        private void Res_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.Assert(this.listViewFolderNotSpecialProperties != null);
            this.ListGuidViewProperties.Clear();
            foreach (var t in this.listViewNotSpecialProperties)
            {
                this.ListGuidViewProperties.Add(t.Guid);
            }
        }

        #endregion Editor
    }
}
