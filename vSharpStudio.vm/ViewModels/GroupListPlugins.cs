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
    public partial class GroupListPlugins : ITreeModel, ICanGoRight
    {
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
            this.ListPlugins.CollectionChanged += ListPlugins_CollectionChanged;
        }

        private void ListPlugins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnAddRemoveNode(e);
        }
    }
}
