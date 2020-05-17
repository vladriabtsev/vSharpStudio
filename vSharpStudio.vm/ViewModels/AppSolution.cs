using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        partial void OnInit()
        {
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.DefaultDb.Parent = this;
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            //    this.RefillChildren();
        }
        //protected override void OnInitFromDto()
        //{
        //    _logger.Trace();
        //    base.OnInitFromDto();
        //    this.RefillChildren();
        //}
        //void RefillChildren()
        //{
        //    this.Children.Clear();
        //    this.Children.Add(this.GroupConfigLinks, 0);
        //    this.Children.Add(this.Model, 1);
        //    this.Children.Add(this.GroupPlugins, 9);
        //    this.Children.Add(this.GroupAppSolutions, 10);
        //}

        public AppSolution(ITreeConfigNode parent, string name)
            : this(parent)
        {
            (this as ITreeConfigNode).Name = name;
        }

        public AppSolution(ITreeConfigNode parent, string name, List<AppProject> listProjects)
            : this(parent)
        {
            (this as ITreeConfigNode).Name = name;
            foreach (var t in listProjects)
            {
                this.ListAppProjects.Add(t);
            }
        }
        partial void OnRelativeAppSolutionPathChanging(ref string to)
        {
            //#if DEBUG
            //            if (to == null || to.Length < 2 || to[1] != ':')
            //                throw new Exception("Full path is expected");
            //#endif
            if (this.IsNotNotifying || to == null)
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativeAppSolutionPathChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this._RelativeAppSolutionPath != null &&
                this._RelativeAppSolutionPath.Length > 1 &&
                this._RelativeAppSolutionPath[1] == ':')
                this._RelativeAppSolutionPath = this.GetRelativeToConfigDiskPath(this._RelativeAppSolutionPath);
        }
        //partial void OnRelativeAppSolutionPathChanged()
        //{
        //    if (this.IsNotNotifying)
        //        return;
        //    if (!string.IsNullOrWhiteSpace(this._RelativeAppSolutionPath))
        //        this._RelativeAppSolutionPath = this.GetRelativeToConfigDiskPath(this._RelativeAppSolutionPath);
        //}
        public AppProject AddProject(string name, string projectPath)
        {
            AppProject node = new AppProject(this, name, projectPath);
            return node;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListAppSolutions).ListAppSolutions.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (AppSolution)(this.Parent as GroupListAppSolutions).ListAppSolutions.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListAppSolutions).ListAppSolutions.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListAppSolutions).ListAppSolutions.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (AppSolution)(this.Parent as GroupListAppSolutions).ListAppSolutions.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListAppSolutions).ListAppSolutions.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove()
        {
            (this.Parent as GroupListAppSolutions).Remove(this);
            this.Parent = null;
            var nv = new ModelVisitorNodeGenSettings();
            foreach (var t in this.ListAppProjects)
            {
                foreach (var tt in t.ListAppProjectGenerators)
                {
                    nv.NodeGenSettingsApplyAction(this.GetConfig(), (p) =>
                    {
                        p.RemoveNodeAppGenSettings(tt.PluginGeneratorGuid);
                    });
                }
            }
            //this.RefillDicGenerators();
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppSolution.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListAppSolutions).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppSolution(this.Parent);
            (this.Parent as GroupListAppSolutions).Add(node);
            this.GetUniqueName(AppSolution.DefaultName, node, (this.Parent as GroupListAppSolutions).ListAppSolutions);
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            AppProject node = null;
            if (node_impl == null)
            {
                node = new AppProject(this);
            }
            else
            {
                node = (AppProject)node_impl;
            }

            node.Parent = this;
            this.ListAppProjects.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(AppProject.DefaultName, node, this.ListAppProjects);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
