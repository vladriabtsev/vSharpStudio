using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using ViewModelBase;
using System.ComponentModel;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProjectGenerator
    {
        public partial class AppProjectGeneratorValidator
        {
            public AppProjectGeneratorValidator()
            {
                this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                this.RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                this.RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
                this.RuleFor(x => x.RelativePathToGenFolder)
                    .NotEmpty()
                    .WithMessage("Output generation folder is not selected");
                this.RuleFor(x => x.RelativePathToGenFolder)
                    .Custom((path, cntx) =>
                    {
                        var pg = (AppProjectGenerator)cntx.InstanceToValidate;
                        if (!string.IsNullOrEmpty(path) && !Directory.Exists(pg.GetGenerationFolderPath()))
                        {
                            cntx.AddFailure("Output generation folder was not found:" + pg.GetGenerationFolderPath());
                        }
                    });
                this.RuleFor(x => x.GenFileName)
                    .NotEmpty()
                    .WithMessage("Output file name is empty");
                this.RuleFor(x => x.GenFileName)
                    .Custom((file, cntx) =>
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            var pg = (AppProjectGenerator)cntx.InstanceToValidate;
                            var path = pg.GetGenerationFilePath();
                            var gs = (GroupListAppSolutions)pg.Parent.Parent.Parent;
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Files override each other . Generators: ");
                            int count = 0;
                            string sep = "";
                            foreach (var t in gs.ListAppSolutions)
                            {
                                foreach (var tt in t.ListAppProjects)
                                {
                                    foreach (var ttt in tt.ListAppProjectGenerators)
                                    {
                                        if (pg != ttt && ttt.GetGenerationFilePath() == path)
                                        {
                                            sb.Append(sep);
                                            sb.Append(t.Name);
                                            sb.Append(@"\");
                                            sb.Append(tt.Name);
                                            sb.Append(@"\");
                                            sb.Append(ttt.Name);
                                            sep = ", ";
                                            count++;
                                        }
                                    }
                                }
                            }
                            if (count > 1)
                            {
                                sb.Append(". Change generation file path");
                                cntx.AddFailure(sb.ToString());
                            }
                        }
                    });
                this.RuleFor(x => x.PluginGuid)
                    .NotEmpty()
                    .WithMessage("Plugin is not selected");
                this.RuleFor(x => x.PluginGeneratorGuid)
                    .NotEmpty()
                    .WithMessage("Generator is not selected");
            }

            private bool IsUnique(AppProjectGenerator val)
            {
                if (val.Parent == null)
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                {
                    return true;
                }

                AppProject p = (AppProject)val.Parent;
                foreach (var t in p.ListAppProjectGenerators)
                {
                    if ((val.Guid != t.Guid) && (val.Name == t.Name))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public static readonly string DefaultName = "Generator";

        partial void OnInit()
        {
            this._RelativePathToGenFolder = @"Generated\";
            this.ListGenerators = new SortedObservableCollection<PluginGenerator>();
        }
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            UpdateListGenerators();
        }
        public SortedObservableCollection<PluginGenerator> ListGenerators { get; private set; }
        [PropertyOrderAttribute(10)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("General")]
        [Description("General generator settings")]
        public object GeneralSettings
        {
            get
            {
                return this._GeneralSettings;
            }
            set
            {
                if (this._GeneralSettings != value)
                {
                    this._GeneralSettings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _GeneralSettings;
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Nodes")]
        [Description("Nodes default generators settings")]
        public object NodesSettings
        {
            get { return this._NodesSettings; }
            set
            {
                if (this._NodesSettings != value)
                {
                    this._NodesSettings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _NodesSettings;
        public void CreateGenSettings()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                {
                    this.GeneratorSettings = string.Empty;
                    return;
                }
                Config cnfg = (Config)this.GetConfig();
                PluginGenerator gen = (PluginGenerator)cnfg.DicNodes[this.PluginGeneratorGuid];
                this.GeneralSettings = gen?.Generator?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                //this.NodesSettings = gen?.Generator?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        partial void OnPluginGuidChanged()
        {
            if (this.IsNotNotifying)
                return;
            this.PluginGeneratorGuid = "";
            UpdateListGenerators();
            if (!Config.IsLoading)
            {
                var nv = new ModelVisitorNodeGenSettings();
                nv.NodeGenSettingsApplyAction(this.GetConfig(), (p) =>
                {
                    p.RemoveNodeAppGenSettings(this.Guid);
                });
            }
        }

        private void UpdateListGenerators()
        {
            Config cnfg = (Config)this.GetConfig();
            Plugin plg = (Plugin)cnfg.DicNodes[this.PluginGuid];
            this.ListGenerators.Clear();
            this.ListGenerators.AddRange(plg.ListGenerators);
            //EditorPluginSelection.ListGenerators.Clear();
            //EditorPluginSelection.ListGenerators.AddRange(plg.ListGenerators);
            this.DescriptionPlugin = plg.Description;
        }
        partial void OnPluginGeneratorGuidChanging(ref string to)
        {
            if (this.IsNotNotifying)
                return;
            Config cfg = (Config)this.GetConfig();
            if (cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                cfg.DicActiveAppProjectGenerators.Remove(this.Guid);
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.RemoveNodeAppGenSettings(this.Guid);
            });
            this.GeneratorSettings = string.Empty;
            this.DescriptionGenerator = string.Empty;
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            if (this.IsNotNotifying)
                return;
            var nv = new ModelVisitorNodeGenSettings();
            Config cfg = (Config)this.GetConfig();
            if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                return;
            if (!cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
            {
                foreach (var t in cfg.GroupPlugins.ListPlugins)
                {
                    foreach (var tt in t.ListGenerators)
                    {
                        if (tt.Generator.Guid == this.PluginGeneratorGuid)
                        {
                            cfg.DicActiveAppProjectGenerators[this.Guid] = tt.Generator;
                        }
                    }
                }
            }
            var gen = cfg.DicActiveAppProjectGenerators[this.Guid];
            this.GeneralSettings = gen?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
            var lst = gen?.GetListNodeGenerationSettings();
            var lstNodesSettings = new SortedObservableCollection<PluginGeneratorNodeSettings>();
            //foreach (var t in lst)
            //{
            //    var p = new PluginGeneratorNodeSettings(this, t);
            //    lstNodesSettings.Add(p);
            //}
            this.NodesSettings = SettingsTypeBuilder.CreateNodesSettingObject(lstNodesSettings);
            this.DescriptionGenerator = gen.Description;
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.AddNodeAppGenSettings(this.Guid);
            });
        }
        public string GetGenerationFilePath()
        {
            var path = this.GetGenerationFolderPath();
            path = Path.Combine(path, this.GenFileName);
            return path;
        }
        public string GetGenerationFolderPath()
        {
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            AppProject prj = this.Parent as AppProject;
            var path = prj.GetProjectFolderPath();
            path = Path.Combine(path, this.RelativePathToGenFolder);
            return path;
        }
        partial void OnRelativePathToGenFolderChanging(ref string to)
        {
            if (this.IsNotNotifying || to == null)
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativePathToGenFolderChanged()
        {
            if (this.IsNotNotifying)
                return;
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                throw new Exception("Config is not saved yet");
            var sln = this.Parent.Parent as AppSolution;
            if (sln.RelativeAppSolutionPath == null)
                throw new Exception("Solution path is not selected yet");
            var prj = this.Parent as AppProject;
            if (prj.RelativeAppProjectPath == null)
                throw new Exception("Project path is not selected yet");
            if (this._RelativePathToGenFolder != null)
            {
                var path = prj.GetProjectFolderPath();
#if NET48
                this._RelativePathToGenFolder = vSharpStudio.common.Utils..GetRelativePath(path, this._RelativePathToGenFolder);
#else
                this._RelativePathToGenFolder = Path.GetRelativePath(path, this._RelativePathToGenFolder);
#endif
            }
        }
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppProject).ListAppProjectGenerators.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (AppProject)(this.Parent as AppProject).ListAppProjectGenerators.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as AppProject).ListAppProjectGenerators.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppProject).ListAppProjectGenerators.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (AppProject)(this.Parent as AppProject).ListAppProjectGenerators.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as AppProject).ListAppProjectGenerators.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove()
        {
            var cfg = (Config)this.GetConfig();
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.RemoveNodeAppGenSettings(this.Guid);
            });
            (this.Parent as AppProject).ListAppProjectGenerators.Remove(this);
            this.Parent = null;
            cfg.DicActiveAppProjectGenerators.Remove(this.Guid);
            //this.RefillDicGenerators();
            //    this.RemoveNodeAppGenSettings(item.Guid);
            //    var cfg = (Config)this.GetConfig();
            //    cfg.DicAppGenerators.Remove(item.Guid);
            //    _logger.LogTrace("{DicAppGenerators}", cfg.DicAppGenerators);
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppProjectGenerator.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as AppProject).ListAppProjectGenerators.Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppProjectGenerator(this.Parent);
            (this.Parent as AppProject).ListAppProjectGenerators.Add(node);
            this.GetUniqueName(AppProjectGenerator.DefaultName, node, (this.Parent as AppProject).ListAppProjectGenerators);
            this.SetSelected(node);
            return node;
        }

        #endregion Tree operations
    }
}
