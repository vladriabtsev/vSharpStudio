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
    public partial class EnumerationPair : INewAndDeleteion, IEditableNode
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
        public bool GetIsHasMarkedForDeletion()
        {
            //foreach (var t in this.ListDocuments)
            //{
            //    if (t.IsMarkedForDeletion || t.GetIsHasMarkedForDeletion())
            //    {
            //        this.IsHasMarkedForDeletion = true;
            //        return true;
            //    }
            //}
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            //foreach (var t in this.ListDocuments)
            //{
            //    if (t.IsHasNew || t.GetIsHasNew())
            //    {
            //        this.IsHasNew = true;
            //        return true;
            //    }
            //}
            this.IsHasNew = false;
            return false;
        }
        public IEnumerable<ITreeConfigNode> GetParentList()
        {
            var p = this.Parent as Enumeration;
            return p.ListEnumerationPairs;
        }
        public void Remove()
        {
            var p = this.Parent as Enumeration;
            p.ListEnumerationPairs.Remove(this);
        }
    }
}
