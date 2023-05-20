using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using FluentValidation;
using Proto.Doc;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Form:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Form : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [Browsable(false)]
        public GroupListForms ParentGroupListForms { get { Debug.Assert(this.Parent != null); return (GroupListForms)this.Parent; } }
        [Browsable(false)]
        public IGroupListForms ParentGroupListFormsI { get { Debug.Assert(this.Parent != null); return (IGroupListForms)this.Parent; } }

        public Form(ITreeConfigNode? parent, FormType ftype, List<IProperty> lst) : this(parent)
        {
            this._ListProperties = lst;
            switch(ftype)
            {
                case FormType.ListWide:
                case FormType.ListNarrow:
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
                    this.EnumFormType = t;
                    break;
                }
            }
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
                case FormType.ListNarrow:
                case FormType.ListWide:
                    lst.Add(this.GetPropertyName(() => this.IsDummy));
                    break;
                case FormType.ItemEditForm:
                case FormType.FolderEditForm:
                    break;
                default:
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
        #endregion Visibility

        [Browsable(false)]
        public IReadOnlyList<IProperty> ListProperties
        {
            get
            {
                if (this._ListProperties == null)
                {
                    this._ListProperties = new List<IProperty>();
                    if (this.ParentGroupListForms.Parent is IDetail dt)
                    {
                        dt.GetSpecialProperties(this._ListProperties, false);
                    }
                    else if (this.ParentGroupListForms.Parent is ICatalog c)
                    {
                        c.GetSpecialProperties(this._ListProperties, false);
                    }
                    else if (this.ParentGroupListForms.Parent is IDocument d)
                    {
                        d.GetSpecialProperties(this._ListProperties, false);
                    }
                    else if (this.ParentGroupListForms.Parent is ICatalogFolder cf)
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

        [Browsable(false)]
        public SortedObservableCollection<IProperty> ListSeparateTreeSelectedNotSpecialProperties
        {
            get
            {
                if (this.listSeparateTreeSelectedNotSpecialProperties == null)
                {
                    this.listSeparateTreeSelectedNotSpecialProperties = new SortedObservableCollection<IProperty>();
                    if (this.ParentGroupListForms.Parent is Catalog c)
                    {
                        if (c.UseTree && c.UseSeparateTreeForFolders)
                        {
                            var res = new List<IProperty>();
                            c.Folder.GetNormalProperties(res);
                            foreach (var t in res)
                            {
                                foreach (var tt in this.ListGuidViewFolderProperties)
                                {
                                    if (tt == t.Guid)
                                    {
                                        this.listSeparateTreeSelectedNotSpecialProperties.Add(t);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return this.listSeparateTreeSelectedNotSpecialProperties;
            }
        }
        private SortedObservableCollection<IProperty>? listSeparateTreeSelectedNotSpecialProperties;
        [Browsable(false)]
        public ObservableCollection<IProperty> ListSeparateTreeAllNotSpecialProperties
        {
            get
            {
                if (this.listSeparateTreeAllNotSpecialProperties == null)
                {
                    this.listSeparateTreeAllNotSpecialProperties = new ObservableCollection<IProperty>();
                    if (this.ParentGroupListForms.Parent is Catalog c)
                    {
                        if (c.UseTree && c.UseSeparateTreeForFolders)
                        {
                            var res = new List<IProperty>();
                            c.Folder.GetNormalProperties(res);
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
                                    this.listSeparateTreeAllNotSpecialProperties.Add(t);
                            }
                            this.listSeparateTreeAllNotSpecialProperties.CollectionChanged += ResFolder_CollectionChanged;
                        }
                    }
                }
                return this.listSeparateTreeAllNotSpecialProperties;
            }
        }
        private ObservableCollection<IProperty>? listSeparateTreeAllNotSpecialProperties = null;
        private void ResFolder_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.Assert(this.listSeparateTreeSelectedNotSpecialProperties != null);
            this.ListGuidViewFolderProperties.Clear();
            foreach (var t in this.listSeparateTreeSelectedNotSpecialProperties)
            {
                this.ListGuidViewFolderProperties.Add(t.Guid);
            }
        }
        [Browsable(false)]
        public ObservableCollection<IProperty> ListAllNotSpecialProperties
        {
            get
            {
                if (this.listAllNotSpecialProperties == null)
                {
                    this.listAllNotSpecialProperties = new ObservableCollection<IProperty>();
                    if (this.ParentGroupListForms.Parent is Catalog c)
                    {
                        var res = new List<IProperty>();
                        c.GetNormalProperties(res);
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
                                this.listAllNotSpecialProperties.Add(t);
                        }
                    }
                }
                return this.listAllNotSpecialProperties;
            }
        }
        private ObservableCollection<IProperty> listAllNotSpecialProperties;
        [Browsable(false)]
        public SortedObservableCollection<IProperty> ListSelectedNotSpecialProperties
        {
            get
            {
                if (this.listSelectedNotSpecialProperties == null)
                {
                    this.listSelectedNotSpecialProperties = new SortedObservableCollection<IProperty>();
                    if (this.ParentGroupListForms.Parent is Catalog c)
                    {
                        var res = new List<IProperty>();
                        c.GetNormalProperties(res);
                        var lst = new List<IProperty>();
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
                        this.listSelectedNotSpecialProperties.AddRange(lst);
                        this.listSelectedNotSpecialProperties.CollectionChanged += Res_CollectionChanged;
                    }
                }
                return this.listSelectedNotSpecialProperties;
            }
        }
        private SortedObservableCollection<IProperty>? listSelectedNotSpecialProperties;
        private void Res_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.Assert(this.listSelectedNotSpecialProperties != null);
            this.ListGuidViewProperties.Clear();
            foreach (var t in this.listSelectedNotSpecialProperties)
            {
                this.ListGuidViewProperties.Add(t.Guid);
            }
        }

        partial void OnEnumFormTypeChanging(ref FormType to)
        {
            if (to == FormType.FormTypeNotSelected)
                return;
            var ft = to;
            var f = (from p in this.ParentGroupListForms.ListForms
                     where p.EnumFormType == ft
                     select p).SingleOrDefault();
            if (f != null)
            {
                MessageBox.Show($"List forms already contains '{Enum.GetName<FormType>(ft)}' form type", "Warning", System.Windows.MessageBoxButton.OK);
                to = FormType.FormTypeNotSelected;
            }
        }
        partial void OnEnumFormTypeChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
            this.NotifyPropertyChanged(() => this.IsListForm);
        }
        [Browsable(false)]
        public bool IsListForm
        {
            get
            {
                return this.EnumFormType == FormType.ListWide || this.EnumFormType == FormType.ListNarrow;
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
        //public bool UseCodePropertySeparateFolder
        //{
        //    get
        //    {
        //        if (this.ParentGroupListForms.Parent is Catalog c)
        //        {
        //            return c.GetUseCodePropertySeparateFolder();
        //        }
        //        return false;
        //    }
        //}
        //public bool UseDescriptionPropertySeparateFolder
        //{
        //    get
        //    {
        //        if (this.ParentGroupListForms.Parent is Catalog c)
        //        {
        //            return c.GetUseDescriptionPropertSeparateFoldery();
        //        }
        //        return false;
        //    }
        //}
        //public string CodePropertyName { get { return this.Cfg.Model.PropertyCodeName; } }
        //public bool UseCodeProperty
        //{
        //    get
        //    {
        //        if (this.ParentGroupListForms.Parent is Catalog c)
        //        {
        //            return c.GetUseCodeProperty();
        //        }
        //        return false;
        //    }
        //}
        //public string DescriptionPropertyName { get { return this.Cfg.Model.PropertyDescriptionName; } }
        //public bool UseDescriptionProperty
        //{
        //    get
        //    {
        //        if (this.ParentGroupListForms.Parent is Catalog c)
        //        {
        //            return c.GetUseDescriptionProperty();
        //        }
        //        return false;
        //    }
        //}
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
