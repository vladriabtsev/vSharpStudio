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
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    // [DebuggerDisplay("AppProject:{Name,nq} props:{listProperties.Count,nq}")]
    [DebuggerDisplay("AppProject:{Name,nq} RelPath:{RelativeAppProjectPath,nq}")]
    public partial class AppProject : ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode, ICanRemoveNode, IEditableNode, IEditableNodeGroup
    {
        public static readonly string DefaultName = "Project";
        [BrowsableAttribute(false)]
        public IAppSolution AppSolution { get { return (IAppSolution)this.Parent; } }

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

        public ConfigNodesCollection<AppProjectGenerator> Children { get { return this.ListAppProjectGenerators; } }

        [Browsable(false)]
        new public string IconName { get { return "iconApplication"; } }

        //protected override string GetNodeIconName() { return "iconApplication"; }
        partial void OnCreated()
        {
            //this.DefaultDb.Parent = this;
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.ListAppProjectGenerators.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListAppProjectGenerators.OnRemovedAction = (t) =>
            {
                var cfg = (Config)this.GetConfig();
                cfg.RemoveNodeAppGenSettings(t.Guid);
                t.PluginGuid = string.Empty;
                if (cfg._DicActiveAppProjectGenerators.ContainsKey(t.Guid))
                    cfg._DicActiveAppProjectGenerators.Remove(t.Guid);
                this.OnRemoveChild();
            };
            this.ListAppProjectGenerators.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
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
            this.Name = name;
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
            if (!this.IsNotifying || to == null)
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativeAppProjectPathChanged()
        {
            if (!this.IsNotifying)
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
                this.Name = Path.GetFileName(this._RelativeAppProjectPath);
            }
        }
        public AppProjectGenerator AddGenerator(string name, string pluginGuid, string generatorGuid, string outFile, string generationPath = null)
        {
            AppProjectGenerator node = new AppProjectGenerator(this);
            this.ListAppProjectGenerators.Add(node);
            if (generationPath != null)
                node.RelativePathToGenFolder = generationPath;
            node.PluginGuid = pluginGuid;
            node.PluginGeneratorGuid = generatorGuid;
            if (!string.IsNullOrWhiteSpace(outFile))
                node.GenFileName = outFile;
            node.Name = name;
            return node;
        }

        //public object GetGroupSettings(string groupSettingsGuid)
        //{
        //    throw new NotImplementedException();
        //    return this.DicPluginsGroupSettings[groupSettingsGuid];
        //}
        //public void SaveGroupSettings()
        //{
        //    throw new NotImplementedException();
        //    this.ListGroupGeneratorsSettings.Clear();
        //    foreach (var t in this.DicPluginsGroupSettings)
        //    {
        //        var set = new PluginGroupGeneratorsSettings(this);
        //        set.AppGroupGeneratorsGuid = t.Key;
        //        set.Settings = t.Value.SettingsAsJson;
        //        this.ListGroupGeneratorsSettings.Add(set);
        //    }
        //}
        //public void RestoreGroupSettings()
        //{
        //    throw new NotImplementedException();
        //    var cfg = (Config)this.GetConfig();
        //    this.DicPluginsGroupSettings.Clear();
        //    foreach (var t in this.ListGroupGeneratorsSettings)
        //    {
        //        if (!cfg.DicGroupSettings.ContainsKey(t.AppGroupGeneratorsGuid))
        //            throw new Exception();
        //        this.DicPluginsGroupSettings[t.AppGroupGeneratorsGuid] = cfg.DicGroupSettings[t.AppGroupGeneratorsGuid].GetPluginGroupSolutionSettingsVmFromJson(t.Settings);
        //    }
        //}

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
        //public override void NodeRemove(bool ask = true)
        //{
        //    if (ask)
        //    {
        //        var res = MessageBox.Show("You are deleting generators for Project. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
        //        if (res != System.Windows.MessageBoxResult.OK)
        //            return;
        //    }
        //    foreach (var t in this.ListAppProjectGenerators)
        //    {
        //        t.NodeRemove(false);
        //    }
        //    (this.Parent as AppSolution).ListAppProjects.Remove(this);
        //    this.Parent = null;
        //}
        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppProject.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as AppSolution).ListAppProjects.Add(node);
            this._Name = this._Name + "2";
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
        #region Group Generator Project Settings
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Groups Settings")]
        [Description("Project groups generators settings. Group generators are working together")]
        public object DynamicPluginGroupSettings
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
        private object _DynamicPluginGroupSettings;
        //// GroupGeneratorsSettings guid, settings
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
        public void RestoreGroupSettings(IvPluginGenerator gen = null)
        {
            var cfg = (Config)this.GetConfig();
            if (gen == null)
            {
                this.DicPluginsGroupSettings.Clear();
                foreach (var t in this.ListGroupGeneratorsSettings)
                {
                    if (!cfg.DicGroupSettingGenerators.ContainsKey(t.AppGroupGeneratorsGuid))
                        throw new Exception();
                    var set = cfg.DicGroupSettingGenerators[t.AppGroupGeneratorsGuid].GetPluginGroupProjectSettingsVmFromJson(this, t.Settings);
                    set.Parent = this;
                    this.DicPluginsGroupSettings[t.AppGroupGeneratorsGuid] = set;
                }
            }
            else
            {
                if (!cfg.DicGroupSettingGenerators.ContainsKey(gen.Guid))
                    cfg.DicGroupSettingGenerators[gen.Guid] = gen;
                var set = gen.GetPluginGroupProjectSettingsVmFromJson(this, null);
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
        #endregion Group Generator Project Settings
    }
}
