using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text;
using System.Windows;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    public partial class FormCatalogListSettings
    {
        [BrowsableAttribute(false)]
        public Form ParentForm { get { return (Form)this.Parent; } }
        [BrowsableAttribute(false)]
        public IForm ParentFormI { get { return (IForm)this.Parent; } }
        partial void OnCreated()
        {
            this.IsUseCode = true;
            this.IsUseName = true;
            this.IsUseFolderCode = true;
            this.IsUseFolderName = true;
            HideProperties();
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
        //protected override void OnInitFromDto()
        //{
        //    HideProperties();
        //}
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
        private void HideProperties()
        {
            var lst = new List<string>();
            string cmplx = ""; //"CatalogListSettings.";
            if (this.ParentForm.ParentGroupListForms.Parent is Catalog c)
            {
                //if (!c.UseTree)
                //{
                //    lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                //    lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                //    lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                //}
                //else
                //{
                //    if (c.UseSeparateTreeForFolders)
                //    {
                //        lst.Add(this.GetPropertyName(() => this.UseFolderTypeExplicitly));
                //    }
                //}
                if (!c.GetUseCodeProperty())
                    lst.Add(cmplx + this.GetPropertyName(() => this.IsUseCode));
                if (!c.GetUseNameProperty())
                    lst.Add(cmplx + this.GetPropertyName(() => this.IsUseName));
                if (!c.GetUseDescriptionProperty())
                    lst.Add(cmplx + this.GetPropertyName(() => this.IsUseDesc));
                lst.Add(cmplx + this.GetPropertyName(() => this.IsUseFolderCode));
                lst.Add(cmplx + this.GetPropertyName(() => this.IsUseFolderDesc));
                lst.Add(cmplx + this.GetPropertyName(() => this.IsUseFolderName));
                lst.Add(cmplx + this.GetPropertyName(() => this.IsUseDocDate));
            }
            else if (this.ParentForm.ParentGroupListForms.Parent is Document d)
            {
                if (!d.GetUseCodeProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseCode));
                if (!d.GetUseDateProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseDocDate));
                //if (!d.GetUseNameProperty())
                lst.Add(this.GetPropertyName(() => this.IsUseName));
                //if (!d.GetUseDescriptionProperty())
                lst.Add(this.GetPropertyName(() => this.IsUseDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
            }
            else if (this.ParentForm.ParentGroupListForms.Parent is Detail dt)
            {
                lst.Add(this.GetPropertyName(() => this.IsUseCode));
                lst.Add(this.GetPropertyName(() => this.IsUseName));
                lst.Add(this.GetPropertyName(() => this.IsUseDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseDocDate));
            }
            else if (this.ParentForm.ParentGroupListForms.Parent is CatalogFolder cf)
            {
                lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                if (!cf.GetUseCodeProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                if (!cf.GetUseNameProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                if (!cf.GetUseDescriptionProperty())
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseDocDate));
            }
            if (lst.Count == 0)
                this.AutoGenerateProperties = true;
            else
                this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
        }
    }
}
