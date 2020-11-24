using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("AppSolution:{Name,nq} prj:{ListAppProjects.Count,nq}")]
    public partial class AppSolution : ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode, ICanRemoveNode, IEditableNode, IEditableNodeGroup
    {
        public static readonly string DefaultName = "Solution";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.ListAppProjects;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListAppSolutions;
            return p.ListAppSolutions;
        }
        public override bool HasChildren()
        {
            return this.ListAppProjects.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<AppProject> Children { get { return this.ListAppProjects; } }
        [Browsable(false)]
        new public string IconName { get { return "iconApplicationGroup"; } }
        //protected override string GetNodeIconName() { return "iconApplicationGroup"; }

        partial void OnInit()
        {
            //this.DefaultDb.Parent = this;
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            //    this.RefillChildren();
            this.ListAppProjects.OnAddingAction = (t) => {
                t.IsNew = true;
            };
            this.ListAppProjects.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListAppProjects.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }

        public AppSolution(ITreeConfigNode parent, string name)
            : this(parent)
        {
            (this as ITreeConfigNode).Name = name;
        }

        public AppSolution(ITreeConfigNode parent, string name, List<AppProject> listProjects)
            : this(parent)
        {
            Contract.Requires(listProjects != null);
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
        public string GetSolutionPath()
        {
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            var path = Path.Combine(cfg.CurrentCfgFolderPath, this.RelativeAppSolutionPath);
            return path;
        }
        public string GetSolutionFolderPath()
        {
            var path = this.GetSolutionPath();
            path = path.Substring(0, path.LastIndexOf(@"\") + 1);
            return path;
        }
        public AppProject AddProject(string name, string projectPath)
        {
            AppProject node = new AppProject(this, name, projectPath);
            return node;
        }
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Groups Settings")]
        [Description("Solution group generators settings. Group generators are working together")]
        public object DynamicMainSettings
        {
            get
            {
                var nd = new NodeSettings();
                var res = nd.Run(this);
                return res;
            }
        }
        // GroupGeneratorsSettings guid, settings
        private DictionaryExt<string, IvPluginGroupSolutionSettings> dicSolutionGroupGeneratorsSettings = null;
        [BrowsableAttribute(false)]
        public DictionaryExt<string, IvPluginGroupSolutionSettings> DicPluginsGroupSettings
        {
            get
            {
                if (dicSolutionGroupGeneratorsSettings == null)
                {
                    dicSolutionGroupGeneratorsSettings = new DictionaryExt<string, IvPluginGroupSolutionSettings>(5, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
                }
                return dicSolutionGroupGeneratorsSettings;
            }
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
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

        //public override void NodeRemove(bool ask = true)
        //{
        //    if (ask)
        //    {
        //        var res = MessageBox.Show("You are deleting generators for Solution. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
        //        if (res != System.Windows.MessageBoxResult.OK)
        //            return;
        //    }
        //    foreach (var t in this.ListAppProjects)
        //    {
        //        t.NodeRemove(false);
        //    }
        //    (this.Parent as GroupListAppSolutions).ListAppSolutions.Remove(this);
        //    this.Parent = null;
        //}
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
        public void Remove()
        {
            var p = this.Parent as GroupListAppSolutions;
            p.ListAppSolutions.Remove(this);
        }
    }
}
