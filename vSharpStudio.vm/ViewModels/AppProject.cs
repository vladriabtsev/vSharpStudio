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
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class AppProject : ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode, ICanRemoveNode, IEditableNode, IEditableNodeGroup, INodeDeletable
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Gens:{ListAppProjectGenerators.Count} RelPath:{RelativeAppProjectPath}";
        }
        [Browsable(false)]
        public AppSolution ParentAppSolution { get { Debug.Assert(this.Parent != null); return (AppSolution)this.Parent; } }
        [Browsable(false)]
        public IAppSolution ParentAppSolutionI { get { Debug.Assert(this.Parent != null); return (IAppSolution)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.ListAppProjectGenerators;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentAppSolution.ListAppProjects;
        }
        public override bool HasChildren()
        {
            return this.ListAppProjectGenerators.Count > 0;
        }
        #endregion ITree

        [Browsable(false)]
        public new ConfigNodesCollection<AppProjectGenerator> Children { get { return this.ListAppProjectGenerators; } }

        [Browsable(false)]
        public new string IconName { get { return "iconApplication"; } }

        //protected override string GetNodeIconName() { return "iconApplication"; }
        partial void OnCreated()
        {
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this._ListAppProjectGenerators.OnAddingAction = (t) =>
            {
                t.IsNew = true;
                var cfg = this.ParentAppSolution.ParentGroupListAppSolutions.ParentConfig;
                //var nv = new ModelVisitorNodeGenSettings();
                //nv.NodeGenSettingsApplyAction(cfg, (p) =>
                //{
                //    p.RestoreNodeAppGenSettingsVm(t.Guid);
                //});
            };
            //this.ListAppProjectGenerators.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            this._ListAppProjectGenerators.OnRemovedAction = (t) =>
            {
                var cfg = this.ParentAppSolution.ParentGroupListAppSolutions.ParentConfig;
                var nv = new ModelVisitorNodeGenSettings();
                nv.NodeGenSettingsApplyAction(cfg, (p) =>
                {
                    p.RemoveNodeAppGenSettings(t.Guid);
                });
                t.PluginGuid = string.Empty;
                if (cfg._DicActiveAppProjectGenerators.ContainsKey(t.Guid))
                    cfg._DicActiveAppProjectGenerators.Remove(t.Guid);
                this.OnRemoveChild();
            };
            this._ListAppProjectGenerators.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        protected override void OnConfigInitializedVirtual()
        {
            // All computed properties have to implement init logic in their getters
            // On...Changed procedure chaging fields and notifying properties changes to let binding start computing
            // It will help work with computed properties after loading configuration
            //Debug.Assert(cfg != null);
            //Debug.Assert(cfg.DicPlugins != null);
            //if (this.PluginGuid == string.Empty)
            //{
            //}
            //else
            //{
            //    this.plugin = cfg.DicPlugins[this.PluginGuid];
            //    if (this.PluginGeneratorGuid == string.Empty)
            //    {
            //    }
            //    else
            //    {
            //        UpdateListGenerators();
            //        Debug.Assert(this.ListGenerators != null);
            //        foreach (var t in this.ListGenerators)
            //        {
            //            if (t.Guid == this.PluginGeneratorGuid)
            //            {
            //                Debug.Assert(t.Generator != null);
            //                cfg._DicActiveAppProjectGenerators[this.Guid] = t.Generator;
            //                this.PluginGenerator = t.Generator;
            //                break;
            //            }
            //        }
            //    }
            //}
        }
        public AppProject(ITreeConfigNode parent, string name, string projectPath)
                        : this(parent)
        {
            Debug.Assert(parent != null);
            this._Name = name;
            this.ParentAppSolution.ListAppProjects.Add(this);
            this.RelativeAppProjectPath = projectPath;
        }
        public string GetProjectPath()
        {
            var cfg = this.ParentAppSolution.ParentGroupListAppSolutions.ParentConfig;
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            var path = Path.Combine(this.ParentAppSolution.GetSolutionFolderPath(), this.RelativeAppProjectPath);
            return path;
        }
        public string GetProjectFolderPath()
        {
            var path = this.GetProjectPath();
            path = path[..(path.LastIndexOf(@"\") + 1)];
            return path;
        }
        partial void OnRelativeAppProjectPathChanged()
        {
            if (string.IsNullOrWhiteSpace(this._RelativeAppProjectPath))
            {
            }
            else
            {
                if (Path.IsPathRooted(this._RelativeAppProjectPath))
                {
                    this._RelativeAppProjectPath = this.GetRelativeToConfigDiskPath(this._RelativeAppProjectPath);
                }
            }
        }
        public AppProjectGenerator AddGenerator(string name, string pluginGuid, string generatorGuid, string outFile, string? generationPath = null)
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
                if (this.ParentAppSolution.ListAppProjects.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (AppProject?)this.ParentAppSolution.ListAppProjects.GetPrev(this);
            if (prev != null)
                this.SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            this.ParentAppSolution.ListAppProjects.MoveUp(this);
            this.SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentAppSolution.ListAppProjects.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (AppProject?)this.ParentAppSolution.ListAppProjects.GetNext(this);
            if (next != null)
                this.SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            this.ParentAppSolution.ListAppProjects.MoveDown(this);
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
        //    this.Parent.ListAppProjects.Remove(this);
        //    this.Parent = null;
        //}
        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppProject.Clone(this.ParentAppSolution, this, true, true);
            node.Parent = this.Parent;
            this.ParentAppSolution.ListAppProjects.Add(node);
            this._Name += "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppProject(this.ParentAppSolution);
            this.ParentAppSolution.ListAppProjects.Add(node);
            this.GetUniqueName(Defaults.AppProjectName, node, this.ParentAppSolution.ListAppProjects);
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            AppProjectGenerator node;
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
                this.GetUniqueName(Defaults.AppPrjGeneratorName, node, this.ListAppProjectGenerators);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        public void Remove()
        {
            this.ParentAppSolution.ListAppProjects.Remove(this);
        }
        #region Group Generator Project Settings
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Groups Settings")]
        [Description("Project groups generators settings. Group generators are working together")]
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
                SetProperty(ref this._DynamicPluginGroupSettings, value);
            }
        }
        private object? _DynamicPluginGroupSettings;
        //// GroupGeneratorsSettings guid, settings
        private DictionaryExt<string, IvPluginGroupSettings?>? dicPluginsGroupSettings = null;
        [Browsable(false)]
        public DictionaryExt<string, IvPluginGroupSettings?> DicPluginsGroupSettings
        {
            get
            {
                dicPluginsGroupSettings ??= new DictionaryExt<string, IvPluginGroupSettings?>(5, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
                return dicPluginsGroupSettings;
            }
        }
        public IvPluginGroupSettings? GetGroupSettings(string groupSettingsGuid)
        {
            return this.DicPluginsGroupSettings[groupSettingsGuid];
        }
        public void SaveGroupSettings()
        {
            this.ListGeneratorsProjectSettings.Clear();
            foreach (var t in this.DicPluginsGroupSettings)
            {
                var set = new PluginGeneratorProjectSettings(this)
                {
                    Guid = t.Key
                };
                if (t.Value == null)
                    set.Settings = String.Empty;
                else
                    set.Settings = t.Value.SettingsAsJson;
                this.ListGeneratorsProjectSettings.Add(set);
            }
        }
        public void RestoreGroupSettings(IvPluginGenerator? gen = null)
        {
            if (gen == null)
            {
                this.DicPluginsGroupSettings.Clear();
                foreach (var t in this.ListGeneratorsProjectSettings)
                {
                    Debug.Assert(!this.DicPluginsGroupSettings.ContainsKey(t.Guid));
                    foreach (var tt in this.ListAppProjectGenerators)
                    {
                        IvPluginGenerator? gn = tt.PluginDbGenerator;
                        gn ??= tt.PluginGenerator;
                        if (gn == null)
                            continue;
                        if (gn.ProjectParametersGuid == t.Guid)
                        {
                            var set = gn.GetPluginGroupProjectSettingsVmFromJson(this, t.Settings);
                            if (set != null)
                            {
                                this.DicPluginsGroupSettings[set.Guid] = set;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                var set = gen.GetPluginGroupProjectSettingsVmFromJson(this, null);
                if (set != null && !this.DicPluginsGroupSettings.ContainsKey(set.Guid))
                {
                    set.Parent = this;
                    this.DicPluginsGroupSettings[set.Guid] = set;
                }
            }
        }
        protected override void OnValidated(ValidationResult res)
        {
            foreach (var t in this.DicPluginsGroupSettings)
            {
                if (t.Value == null)
                    continue;
                var res2 = t.Value.ValidateSettings();
                if (res2 != null)
                {
                    foreach (var tt in res2.Errors)
                    {
                        tt.PropertyName = nameof(this.DynamicPluginGroupSettings);
                    }
                    res.Errors.AddRange(res2.Errors);
                }
            }
        }
        public override List<IEditableObjectExt> GetEditableNodeSettings()
        {
            var lst = new List<IEditableObjectExt>();
            foreach (var t in this.DicPluginsGroupSettings.Values)
            {
                Debug.Assert(t is IEditableObjectExt);
                lst.Add((IEditableObjectExt)t);
            }
            return lst;
        }
        #endregion Group Generator Project Settings
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent)
            };
            //lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
        public void Delete()
        {
            if (this.Children.Count > 0)
            {
                var res = MessageBox.Show("Project contains generators. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                if (res == System.Windows.MessageBoxResult.OK)
                    this.Remove();
            }
            else
                this.Remove();
        }
    }
}
