using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Linq;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Diagnostics.Contracts;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    // [DebuggerDisplay("AppProject:{Name,nq} props:{listProperties.Count,nq}")]
    [DebuggerDisplay("AppProject:{Name,nq}")]
    public partial class AppProject : ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode, ICanRemoveNode, IEditableNode, IEditableNodeGroup
    {
        public static readonly string DefaultName = "Project";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.ListAppProjectGenerators;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
                var p = this.Parent as AppSolution;
                return p.ListAppProjects;
        }
        public override bool HasChildren()
        {
            return this.ListAppProjectGenerators.Count > 0;
        }
        #endregion ITree

        new public ConfigNodesCollection<AppProjectGenerator> Children { get { return this.ListAppProjectGenerators; } }

        [Browsable(false)]
        new public string IconName { get { return "iconApplication"; } }

        //protected override string GetNodeIconName() { return "iconApplication"; }
        partial void OnInit()
        {
            //this.DefaultDb.Parent = this;
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            //this.ListAppProjectGenerators.OnRemovedAction = (item) => {
            //    this.RemoveNodeAppGenSettings(item.Guid);
            //    var cfg = (Config)this.GetConfig();
            //    cfg.DicAppGenerators.Remove(item.Guid);
            //    _logger.LogTrace("{DicAppGenerators}", cfg.DicAppGenerators);
            //};

            //this.RefillChildren();
        }
        protected override void OnInitFromDto()
        {
            //_logger.Trace();
            //base.OnInitFromDto();
            //this.RefillChildren();
        }
        void RefillChildren()
        {
            //this.Children.Clear();
            //this.Children.Add(this.GroupConfigLinks, 0);
            //this.Children.Add(this.Model, 1);
            //this.Children.Add(this.GroupPlugins, 9);
            //this.Children.Add(this.GroupAppSolutions, 10);
        }
        public AppProject(ITreeConfigNode parent, string name, string projectPath)
            : this(parent)
        {
            Contract.Requires(parent != null);
            (this as ITreeConfigNode).Name = name;
#pragma warning disable CA1062 // Validate arguments of public methods
            (parent as AppSolution).ListAppProjects.Add(this);
#pragma warning restore CA1062 // Validate arguments of public methods
            this.RelativeAppProjectPath = projectPath;
        }
        public string GetProjectPath()
        {
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            AppSolution sln = this.Parent as AppSolution;
            var path = Path.Combine(sln.GetSolutionFolderPath(), this.RelativeAppProjectPath);
            return path;
        }
        public string GetProjectFolderPath()
        {
            var path = this.GetProjectPath();
            path = path.Substring(0, path.LastIndexOf(@"\") + 1);
            return path;
        }
        partial void OnRelativeAppProjectPathChanging(ref string to)
        {
            if (this.IsNotNotifying || to == null)
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativeAppProjectPathChanged()
        {
            if (this.IsNotNotifying)
                return;
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                throw new Exception("Config is not saved yet");
            var sln = this.Parent as AppSolution;
            if (sln.RelativeAppSolutionPath == null)
                throw new Exception("Solution path is not selected yet");
            if (this._RelativeAppProjectPath != null)
            {
                var path = sln.GetSolutionFolderPath();
#if NET48
                this._RelativeAppProjectPath = vSharpStudio.common.Utils..GetRelativePath(path, this._RelativeAppProjectPath);
#else
                this._RelativeAppProjectPath = Path.GetRelativePath(path, this._RelativeAppProjectPath);
#endif
            }
        }
        public AppProjectGenerator AddGenerator(string name, string pluginGuid, string generatorGuid, string outFile, string generationPath = null)
        {
            AppProjectGenerator node = new AppProjectGenerator(this);
            this.ListAppProjectGenerators.Add(node);
            if (generationPath != null)
                node.RelativePathToGenFolder = generationPath;
            node.Name = name;
            node.PluginGuid = pluginGuid;
            node.PluginGeneratorGuid = generatorGuid;
            if (!string.IsNullOrWhiteSpace(outFile))
                node.GenFileName = outFile;
            return node;
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
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
        public override void NodeRemove(bool ask = true)
        {
            if (ask)
            {
                var res = MessageBox.Show("You are deleting generators for Project. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                if (res != System.Windows.MessageBoxResult.OK)
                    return;
            }
            foreach (var t in this.ListAppProjectGenerators)
            {
                t.NodeRemove(false);
            }
            (this.Parent as AppSolution).ListAppProjects.Remove(this);
            this.Parent = null;
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
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
        public void Remove()
        {
            var p = this.Parent as AppSolution;
            p.ListAppProjects.Remove(this);
        }
    }
}
