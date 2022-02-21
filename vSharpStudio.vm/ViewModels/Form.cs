﻿using System;
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
        public static readonly string DefaultName = "Form";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListForms;
            return p.Children;
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
        partial void OnInit()
        {
            this.ListGuidViewProperties = new ObservableCollection<string>();
            this.ListGuidViewFolderProperties = new ObservableCollection<string>();
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.IsIncludableInModels = true;
            this.CatalogListSettings.Parent = this;
            this.CatalogEditSettings.Parent = this;
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
                if ((this.Parent as GroupListForms).ListForms.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Form)(this.Parent as GroupListForms).ListForms.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListForms).ListForms.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListForms).ListForms.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Form)(this.Parent as GroupListForms).ListForms.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListForms).ListForms.MoveDown(this);
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
            (this.Parent as GroupListForms).Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Form(this.Parent);
            (this.Parent as GroupListForms).Add(node);
            this.GetUniqueName(Form.DefaultName, node, (this.Parent as GroupListForms).ListForms);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListForms;
            p.ListForms.Remove(this);
        }
        #endregion Tree operations

        #region Visibility
        private void HideUnusedProperties()
        {
            var lst = new List<string>();
            var lst2 = new List<string>();
            if (this.Parent.Parent is ICatalog)
            {
                lst.Add(this.GetPropertyName(() => this.EnumDocumentFormType));
                switch (this.EnumCatalogFormType)
                {
                    case FormCatalogViewType.CatListForm:
                        var c = (ICatalog)this.Parent.Parent;
                        if (!c.UseTree || !(c.UseTree && c.UseSeparatePropertiesForGroups))
                        {
                            lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderCode));
                            lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderName));
                            lst2.Add(this.GetPropertyName(() => this.CatalogListSettings.IsUseFolderDesc));
                            this.CatalogListSettings.HidePropertiesForXceedPropertyGrid(lst2.ToArray());
                        }
                        lst.Add(this.GetPropertyName(() => this.CatalogEditSettings));
                        break;
                    case FormCatalogViewType.CatItemForm:
                    case FormCatalogViewType.CatFolderForm:
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
                lst.Add(this.GetPropertyName(() => this.EnumCatalogFormType));
                lst.Add(this.GetPropertyName(() => this.CatalogListSettings));
                lst.Add(this.GetPropertyName(() => this.CatalogEditSettings));
            }
            this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
        }
        partial void OnEnumCatalogFormTypeChanged()
        {
            HideUnusedProperties();
        }
        partial void OnEnumDocumentFormTypeChanged()
        {
            HideUnusedProperties();
        }
        #endregion Visibility
    }
}
