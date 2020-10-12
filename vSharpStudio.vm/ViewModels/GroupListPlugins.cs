using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListPlugins.Count,nq}")]
    public partial class GroupListPlugins : ITreeModel, ICanGoRight, INewAndDeleteion
    {
        public ConfigNodesCollection<Plugin> Children { get { return this.ListPlugins; } }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListPlugins;
        }

        public override bool HasChildren(object parent)
        {
            return this.ListPlugins.Count > 0;
        }

        partial void OnInit()
        {
            this.Name = "Plugins";
            this.IsEditable = false;
            this.ListPlugins.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListPlugins.OnAddedAction = (t) =>
            //{
            //};
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            (this.Parent as INewAndDeleteion).IsMarkedForDeletion = this.IsMarkedForDeletion;
        }
        partial void OnIsNewChanged()
        {
            (this.Parent as INewAndDeleteion).IsNew = this.IsNew;
        }
    }
}
