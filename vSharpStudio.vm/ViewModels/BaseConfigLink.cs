﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class BaseConfigLink : IEditableNode  // INodeGenSettings, 
    {
        public static readonly string DefaultName = "BaseConfig";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListBaseConfigLinks;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "icon3DScene"; } }
        //protected override string GetNodeIconName() { return "icon3DScene"; }
        partial void OnInit()
        {
        }
        public void OnAdded()
        {
         //   this.AddAllAppGenSettingsVmsToNode();
        }
        public IEnumerable<ITreeConfigNode> GetParentList()
        {
            var p = this.Parent as GroupListBaseConfigLinks;
            return p.ListBaseConfigLinks;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListBaseConfigLinks;
            p.ListBaseConfigLinks.Remove(this);
        }
    }
}
