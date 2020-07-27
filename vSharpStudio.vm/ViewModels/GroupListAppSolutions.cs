using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListAppSolutions.Count,nq}")]
    public partial class GroupListAppSolutions : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListAppSolutions;
        }

        public override bool HasChildren(object parent)
        {
            return this.ListAppSolutions.Count > 0;
        }

        partial void OnInit()
        {
            this.Name = "Apps";
            this.IsEditable = false;
            //this.DefaultDb.Parent = this;
            //this.ListAppSolutions.OnAddedAction = (t) =>
            //{
            //    t.AddAllAppGenSettingsVmsToNode();
            //};
        }

        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Settings")]
        [Description("Default group generators settings. Group generators are working together")]
        public object DynamicMainSettings
        {
            get
            {
                return this._DynamicMainSettings;
            }
            set
            {
                if (this._DynamicMainSettings != value)
                {
                    this._DynamicMainSettings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _DynamicMainSettings;

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public void AddAppSolution(AppSolution node)
        {
            this.NodeAddNewSubNode(node);
        }
        public AppSolution AddAppSolution(string name, string appSolutionPath)
        {
            appSolutionPath = Path.GetFullPath(appSolutionPath);
            AppSolution node = new AppSolution(this, name)
            {
                //RelativeAppSolutionPath = this.GetRelativeToConfigDiskPath(appSolutionPath)
                RelativeAppSolutionPath = appSolutionPath
            };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            AppSolution node = null;
            if (node_impl == null)
            {
                node = new AppSolution(this);
            }
            else
            {
                node = (AppSolution)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(AppSolution.DefaultName, node, this.ListAppSolutions);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
