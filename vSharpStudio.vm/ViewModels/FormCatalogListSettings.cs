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
        partial void OnCreated()
        {
            this.IsUseCode = true;
            this.IsUseName = true;
            this.IsUseFolderCode = true;
            this.IsUseFolderName = true;
            HideProperties();
        }
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
            var c = (Catalog)this.Parent.Parent.Parent;
            var lst = new List<string>();
            if (c.UseSeparateTreeForFolders)
            {
                if (!c.GetUseCodeProperty())
                {
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                }
                if (!c.GetUseNameProperty())
                {
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
                }
                if (!c.GetUseDescriptionProperty())
                {
                    lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                }
            }
            else
            {
                lst.Add(this.GetPropertyName(() => this.IsUseFolderCode));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderDesc));
                lst.Add(this.GetPropertyName(() => this.IsUseFolderName));
            }
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
            {
                lst.Add(this.GetPropertyName(() => this.IsUseCode));
            }
            if (!c.GetUseNameProperty())
            {
                lst.Add(this.GetPropertyName(() => this.IsUseName));
            }
            if (!c.GetUseDescriptionProperty())
            {
                lst.Add(this.GetPropertyName(() => this.IsUseDesc));
            }
            if (lst.Count == 0)
            {
                this.AutoGenerateProperties = true;
            }
            else
            {
                this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
            }
        }
    }
}
