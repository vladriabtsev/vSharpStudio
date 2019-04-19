using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq}")]
    public partial class GroupProperties : IListProperties
    {
        partial void OnInit()
        {
            this.Name = "Properties";
        }

        #region ITreeNode
        public new string NodeText { get { return this.Name + " " + this.ListProperties.Count; } }
        protected override bool OnNodeCanAddNew()
        {
            return false;
        }
        protected override bool OnNodeCanAddNewSubNode()
        {
            return true;
        }
        protected override ITreeConfigNode OnNodeAddNewSubNode()
        {
            var res = new Property();
            res.Parent = this.Parent;
            this.ListProperties.Add(res);
            GetUniqueName(Property.DefaultName, res, this.ListProperties);
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = res;
            return res;
        }
        protected override bool OnNodeCanMoveDown()
        {
            return false;
        }
        protected override bool OnNodeCanMoveUp()
        {
            return false;
        }
        protected override bool OnNodeCanAddClone()
        {
            return false;
        }
        protected override bool OnNodeCanRemove()
        {
            return false;
        }

        #endregion ITreeNode
    }
}
