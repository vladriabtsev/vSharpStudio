using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : INewAndDeleteion
    {
        public static readonly string DefaultName = "Element";

        [Browsable(false)]
        new public string IconName { get { return "iconEnumItem"; } }
        //protected override string GetNodeIconName() { return "iconEnumItem"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            if (this.IsMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as Enumeration);
                bool isMarked = false;
                foreach (var t in p.ListEnumerationPairs)
                {
                    if (t.IsMarkedForDeletion)
                    {
                        isMarked = true;
                        break;
                    }
                }
                p.IsMarkedForDeletion = isMarked;
            }
        }
        partial void OnIsNewChanged()
        {
            if (this.IsNew)
            {
                (this.Parent as INewAndDeleteion).IsNew = true;
            }
            else
            {
                var p = (this.Parent as Enumeration);
                bool isNew = false;
                foreach (var t in p.ListEnumerationPairs)
                {
                    if (t.IsNew)
                    {
                        isNew = true;
                        break;
                    }
                }
                p.IsNew = isNew;
            }
        }
    }
}
