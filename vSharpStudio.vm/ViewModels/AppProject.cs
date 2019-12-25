﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Microsoft.Extensions.Logging;

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
            //this.ListAppProjectGenerators.OnRemovedAction = (item) => {
            //    this.RemoveNodeAppGenSettings(item.Guid);
            //    var cfg = (Config)this.GetConfig();
            //    cfg.DicAppGenerators.Remove(item.Guid);
            //    _logger.LogTrace("{DicAppGenerators}", cfg.DicAppGenerators);
            //};
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
            var nv = new NodeGenSettingsModelVisitor();
            foreach (var t in this.ListAppProjectGenerators)
            {
                nv.NodeGenSettingsApplyAction(this.GetConfig(), (p) =>
                {
                    p.RemoveNodeAppGenSettings(t.PluginGeneratorGuid);
                });
            }
            //this.RefillDicGenerators();
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
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            AppProjectGenerator node = null;
            if (node_impl == null)
            {
                node = new AppProjectGenerator(this);
            }
            else
            {
                node = (AppProjectGenerator)node_impl;
            }

            node.Parent = this;
            this.ListAppProjectGenerators.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(AppProjectGenerator.DefaultName, node, this.ListAppProjectGenerators);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
