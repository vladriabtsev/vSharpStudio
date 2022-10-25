using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListAppSolutions.Count,nq}")]
    public partial class GroupListAppSolutions : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public Config ParentConfig { get { return (Config)this.Parent; } }
        [BrowsableAttribute(false)]
        public IConfig ParentConfigI { get { return (IConfig)this.Parent; } }
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentConfig.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<AppSolution> Children { get { return this.ListAppSolutions; } }

        partial void OnCreated()
        {
            this._Name = "Apps";
            this.IsEditable = false;
            //this.DefaultDb.Parent = this;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }

        private void Init()
        {
            this.ListAppSolutions.OnAddingAction = (t) => {
                t.IsNew = true;
            };
            this.ListAppSolutions.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListAppSolutions.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }
        //[PropertyOrderAttribute(11)]
        //[ExpandableObjectAttribute()]
        //[ReadOnly(true)]
        //[DisplayName("Settings")]
        //[Description("Default group generators settings. Group generators are working together")]
        //public object DynamicMainSettings
        //{
        //    get
        //    {
        //        return this._DynamicMainSettings;
        //    }
        //    set
        //    {
        //        if (this._DynamicMainSettings != value)
        //        {
        //            this._DynamicMainSettings = value;
        //            this.NotifyPropertyChanged();
        //            this.ValidateProperty();
        //        }
        //    }
        //}
        //private object _DynamicMainSettings;

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
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
