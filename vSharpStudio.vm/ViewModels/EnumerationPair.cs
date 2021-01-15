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
    public partial class EnumerationPair : IEditableNode, INodeGenSettings
    {
        public static readonly string DefaultName = "Element";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as Enumeration;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree


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
        public void Remove()
        {
            var p = this.Parent as Enumeration;
            p.ListEnumerationPairs.Remove(this);
        }
        partial void OnIsDefaultChanged()
        {
            if (this.IsDefault)
            {
                var p = (Enumeration)this.Parent;
                foreach (var t in p.ListEnumerationPairs)
                {
                    if ((this.Guid != t.Guid) && t.IsDefault)
                    {
                        t.IsDefault = false;
                    }
                }
            }
        }
    }
}
