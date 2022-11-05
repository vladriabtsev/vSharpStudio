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
using FluentValidation.Results;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("AppSolution:{Name,nq} prj:{ListAppProjects.Count,nq}")]
    public partial class AppSolution : ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode, ICanRemoveNode, IEditableNode, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public GroupListAppSolutions ParentGroupListAppSolutions { get { Debug.Assert(this.Parent != null); return (GroupListAppSolutions)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListAppSolutions ParentGroupListAppSolutionsI { get { Debug.Assert(this.Parent != null); return (IGroupListAppSolutions)this.Parent; } }
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.ListAppProjects;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListAppSolutions.ListAppSolutions;
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

        partial void OnCreated()
        {
            this.ListAppProjects.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListAppProjects.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListAppProjects.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListAppProjects.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListAppProjects.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            this.ListAppProjects.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListAppProjects.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }

        public AppSolution(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }

        public AppSolution(ITreeConfigNode parent, string name, List<AppProject> listProjects)
            : this(parent)
        {
            Debug.Assert(listProjects != null);
            this.Name = name;
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
            if (!this.IsNotifying || to == null)
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativeAppSolutionPathChanged()
        {
            if (!this.IsNotifying)
                return;
            if (this._RelativeAppSolutionPath != null &&
                this._RelativeAppSolutionPath.Length > 1 &&
                this._RelativeAppSolutionPath[1] == ':')
            {
                this._RelativeAppSolutionPath = this.GetRelativeToConfigDiskPath(this._RelativeAppSolutionPath);
                this.Name = Path.GetFileName(this._RelativeAppSolutionPath);
            }
        }
        public string GetSolutionPath()
        {
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            if (string.IsNullOrEmpty(this.RelativeAppSolutionPath))
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
        #region Group Generator Solution Settings
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Groups Settings")]
        [Description("Solution groups generators settings. Group generators are working together")]
        public object? DynamicPluginGroupSettings
        {
            get
            {
                if (_DynamicPluginGroupSettings == null)
                {
                    var nd = new NodeSettings();
                    _DynamicPluginGroupSettings = nd.Run(this);
                }
                return _DynamicPluginGroupSettings;
            }
            set
            {
                _DynamicPluginGroupSettings = value;
                this.NotifyPropertyChanged();
            }
        }
        private object? _DynamicPluginGroupSettings;
        // GroupGeneratorsSettings guid, settings
        private DictionaryExt<string, IvPluginGroupSettings> dicPluginsGroupSettings = null;
        [BrowsableAttribute(false)]
        public DictionaryExt<string, IvPluginGroupSettings> DicPluginsGroupSettings
        {
            get
            {
                if (dicPluginsGroupSettings == null)
                {
                    dicPluginsGroupSettings = new DictionaryExt<string, IvPluginGroupSettings>(5, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
                }
                return dicPluginsGroupSettings;
            }
        }
        public IvPluginGroupSettings GetGroupSettings(string groupSettingsGuid)
        {
            return this.DicPluginsGroupSettings[groupSettingsGuid];
        }
        public void SaveGroupSettings()
        {
            this.ListGroupGeneratorsSettings.Clear();
            foreach (var t in this.DicPluginsGroupSettings)
            {
                var set = new PluginGroupGeneratorsSettings(this);
                set.AppGroupGeneratorsGuid = t.Key;
                set.Settings = t.Value.SettingsAsJson;
                this.ListGroupGeneratorsSettings.Add(set);
            }
        }
        public void RestoreGroupSettings(IvPluginGenerator? gen = null)
        {
            var cfg = (Config)this.GetConfig();
            if (gen == null)
            {
                this.DicPluginsGroupSettings.Clear();
                foreach (var t in this.ListGroupGeneratorsSettings)
                {
                    if (!cfg.DicGroupSettingGenerators.ContainsKey(t.AppGroupGeneratorsGuid))
                        throw new Exception();
                    var set = cfg.DicGroupSettingGenerators[t.AppGroupGeneratorsGuid].GetPluginGroupSolutionSettingsVmFromJson(this, t.Settings);
                    set.Parent = this;
                    this.DicPluginsGroupSettings[t.AppGroupGeneratorsGuid] = set;
                }
            }
            else
            {
                if (!cfg.DicGroupSettingGenerators.ContainsKey(gen.Guid))
                    cfg.DicGroupSettingGenerators[gen.Guid] = gen;
                var set = gen.GetPluginGroupSolutionSettingsVmFromJson(this, null);
                set.Parent = this;
                this.DicPluginsGroupSettings[gen.GroupGeneratorsGuid] = set;
            }
        }
        protected override void OnValidated(ValidationResult res)
        {
            //Severity? curSeverity = null;
            foreach (var t in this.DicPluginsGroupSettings)
            {
                var res2 = t.Value.ValidateSettings();
                //curSeverity=CheckMaxSeverity(res2, curSeverity);
                foreach (var tt in res2.Errors)
                {
                    tt.PropertyName = nameof(this.DynamicPluginGroupSettings);
                }
                res.Errors.AddRange(res2.Errors);
            }
        }
        #endregion Group Generator Solution Settings

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListAppSolutions.ListAppSolutions.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (AppSolution)this.ParentGroupListAppSolutions.ListAppSolutions.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListAppSolutions.ListAppSolutions.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListAppSolutions.ListAppSolutions.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (AppSolution)this.ParentGroupListAppSolutions.ListAppSolutions.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListAppSolutions.ListAppSolutions.MoveDown(this);
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
            this.ParentGroupListAppSolutions.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppSolution(this.Parent);
            this.ParentGroupListAppSolutions.Add(node);
            this.GetUniqueName(Defaults.AppSolutionName, node, this.ParentGroupListAppSolutions.ListAppSolutions);
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            AppProject node = null!;
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
                this.GetUniqueName(Defaults.AppProjectName, node, this.ListAppProjects);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        public void Remove()
        {
            this.ParentGroupListAppSolutions.ListAppSolutions.Remove(this);
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
