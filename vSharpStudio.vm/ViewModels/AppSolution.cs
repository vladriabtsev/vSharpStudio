using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("AppSolution:{Name,nq} prj:{listProjects.Count,nq}")]
    public partial class AppSolution : ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode
    {
        public static readonly string DefaultName = "Solution";
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
        }
        public AppSolution(string name) : this()
        {
            (this as ITreeConfigNode).Name = name;
        }
        public AppSolution(string name, List<AppProject> listProjects) : this()
        {
            (this as ITreeConfigNode).Name = name;
            foreach (var t in listProjects)
            {
                this.ListAppProjects.Add(t);
            }
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListAppSolutions).ListAppSolutions.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (AppSolution)(this.Parent as GroupListAppSolutions).ListAppSolutions.GetPrev(this);
            if (prev == null)
                return;
            SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListAppSolutions).ListAppSolutions.MoveUp(this);
            SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListAppSolutions).ListAppSolutions.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (AppSolution)(this.Parent as GroupListAppSolutions).ListAppSolutions.GetNext(this);
            if (next == null)
                return;
            SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListAppSolutions).ListAppSolutions.MoveDown(this);
            SetSelected(this);
        }
        public override void NodeRemove()
        {
            (this.Parent as GroupListAppSolutions).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppSolution.Clone(this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListAppSolutions).Add(node);
            this.Name = this.Name + "2";
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppSolution();
            (this.Parent as GroupListAppSolutions).Add(node);
            GetUniqueName(AppSolution.DefaultName, node, (this.Parent as GroupListAppSolutions).ListAppSolutions);
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            AppProject node = null;
            if (node_impl == null)
                node = new AppProject();
            else
                node = (AppProject)node_impl;
            node.Parent = this;
            this.ListAppProjects.Add(node);
            if (node_impl == null)
                GetUniqueName(AppProject.DefaultName, node, this.ListAppProjects);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
