using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Form:{Name,nq}")]
    public partial class Form : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public GroupListForms ParentGroupListForms { get { return (GroupListForms)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListForms ParentGroupListFormsI { get { return (IGroupListForms)this.Parent; } }
        public static readonly string DefaultName = "Form";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListForms.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        [Browsable(false)]
        new public string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        //partial void OnCreate()
        //{
        //}
        partial void OnCreated()
        {
            this.ListGuidViewProperties = new ObservableCollection<string>();
            this.ListGuidViewFolderProperties = new ObservableCollection<string>();
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.IsIncludableInModels = true;
            HideUnusedProperties();
        }
        protected override void OnInitFromDto()
        {
            HideUnusedProperties();
        }

        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            //this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            //this.GroupForms.AddAllAppGenSettingsVmsToNode();
            //this.GroupReports.AddAllAppGenSettingsVmsToNode();
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
            var node = Form.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListForms.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Form(this.Parent);
            this.ParentGroupListForms.Add(node);
            this.GetUniqueName(Form.DefaultName, node, this.ParentGroupListForms.ListForms);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListForms.ListForms.Remove(this);
        }
        #endregion Tree operations

        #region Visibility
        private void HideUnusedProperties()
        {
            var lst = new List<string>();
            var lst2 = new List<string>();
            if (this.Parent.Parent is ICatalog)
            {
                var c = (ICatalog)this.Parent.Parent;
                //lst.Add(this.GetPropertyName(() => this.EnumDocumentFormType));
                switch (this.EnumFormType)
                {
                    case FormType.ViewListNarrow:
                    case FormType.ViewListWide:
                        if (!c.UseTree || !(c.UseTree && c.UseSeparateTreeForFolders))
                        {
                            lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderCode));
                            lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderName));
                            lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderDesc));
                            this.CatalogListSettings.HidePropertiesForXceedPropertyGrid(lst2.ToArray());
                        }
                        lst.Add(this.GetPropertyName(() => this.CatalogEditSettings));
                        break;
                    case FormType.ItemEditForm:
                    case FormType.FolderEditForm:
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogCode));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogName));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogDesc));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogFolderCode));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogFolderName));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogFolderDesc));
                        //this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
                        lst.Add(this.GetPropertyName(() => this.CatalogListSettings));
                        break;
                    default:
                        Debug.Assert(false);
                        lst.Add(this.GetPropertyName(() => this.CatalogListSettings));
                        lst.Add(this.GetPropertyName(() => this.CatalogEditSettings));
                        break;
                }
            }
            else if (this.Parent.Parent is IDetail)
            {
                var c = (IDetail)this.Parent.Parent;
                //lst.Add(this.GetPropertyName(() => this.EnumDocumentFormType));
                switch (this.EnumFormType)
                {
                    case FormType.ViewListNarrow:
                    case FormType.ViewListWide:
                        lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderCode));
                        lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderName));
                        lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderDesc));
                        this.CatalogListSettings.HidePropertiesForXceedPropertyGrid(lst2.ToArray());

                        lst.Add(this.GetPropertyName(() => this.CatalogEditSettings));
                        break;
                    case FormType.ItemEditForm:
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogCode));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogName));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogDesc));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogFolderCode));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogFolderName));
                        //lst.Add(this.GetPropertyName(() => this.IsUseCatalogFolderDesc));
                        //this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
                        lst.Add(this.GetPropertyName(() => this.CatalogListSettings));
                        break;
                    default:
                        Debug.Assert(false);
                        lst.Add(this.GetPropertyName(() => this.CatalogListSettings));
                        lst.Add(this.GetPropertyName(() => this.CatalogEditSettings));
                        break;
                }

            }
            else if (this.Parent.Parent is IDocument)
            {
                lst.Add(this.GetPropertyName(() => this.EnumFormType));
                lst.Add(this.GetPropertyName(() => this.CatalogListSettings));
                lst.Add(this.GetPropertyName(() => this.CatalogEditSettings));
            }
            this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
        }
        partial void OnEnumFormTypeChanged()
        {
            HideUnusedProperties();
        }
        #endregion Visibility
    }
}
