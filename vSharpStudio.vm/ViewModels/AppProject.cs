﻿using System;
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
    // [DebuggerDisplay("AppProject:{Name,nq} props:{listProperties.Count,nq}")]
    [DebuggerDisplay("AppProject:{Name,nq}")]
    public partial class AppProject : ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode
    {
        public static readonly string DefaultName = "Project";

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        partial void OnInit()
        {
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.DefaultDb.Parent = this;
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
        }

        public AppProject(ITreeConfigNode parent, string name, string relativeToSolutionProjectPath)
            : this(parent)
        {
            (this as ITreeConfigNode).Name = name;
            this.RelativeAppProjectPath = relativeToSolutionProjectPath;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppSolution).ListAppProjects.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (AppProject)(this.Parent as AppSolution).ListAppProjects.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as AppSolution).ListAppProjects.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppSolution).ListAppProjects.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (AppProject)(this.Parent as AppSolution).ListAppProjects.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as AppSolution).ListAppProjects.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove()
        {
            (this.Parent as AppSolution).ListAppProjects.Remove(this);
            this.Parent = null;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppProject.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as AppSolution).ListAppProjects.Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppProject(this.Parent);
            (this.Parent as AppSolution).ListAppProjects.Add(node);
            this.GetUniqueName(AppProject.DefaultName, node, (this.Parent as AppSolution).ListAppProjects);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
