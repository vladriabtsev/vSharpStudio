using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProjectGenerator //: ICanAddSubNode
    {
        public static readonly string DefaultName = "Generator";
        private Config cfg;
        private IvPluginGenerator gen
        {
            get { return _gen; }
            set
            {
                _gen = value;
                if (this.IsConnectString() ?? false)
                {
                    (_gen as IvPluginDbConnStringGenerator).OnConnectionStringChanged =
                        (connStr) =>
                        {
                            this._ConnStr = connStr;
                            this.NotifyPropertyChanged(() => this.ConnStr);
                        };
                }
            }
        }
        private IvPluginGenerator _gen = null;
        partial void OnConnStrChanged()
        {
            if (this.IsConnectString() ?? false)
            {
                (_gen as IvPluginDbConnStringGenerator).ConnectionStringToVM(this.ConnStr);
            }
        }
        partial void OnInit()
        {
            this._RelativePathToGenFolder = @"Generated\";
            this.ListGenerators = new SortedObservableCollection<PluginGenerator>();
            cfg = (Config)this.GetConfig();
        }
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            UpdateListGenerators();
            cfg = (Config)this.GetConfig();
        }
        public bool? IsConnectString()
        {
            if (gen == null)
                return null;
            return gen is IvPluginDbConnStringGenerator;
        }
        [Browsable(false)]
        public string IconName
        {
            get
            {
                if (gen == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                    gen = cfg.DicActiveAppProjectGenerators[this.Guid];
                if (gen == null)
                {
                    return null;
                }
                else if (gen is IvPluginDbConnStringGenerator)
                {
                    return "iconAddBehavior";
                }
                else
                {
                    return "iconGenerateFile";
                }
            }
        }
        public SortedObservableCollection<PluginGenerator> ListGenerators { get; private set; }
        [PropertyOrderAttribute(10)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Settings")]
        [Description("General generator settings. Config model nodes can contain additional settings if generator supports node settings")]
        // Generator parameters object
        public object DynamicMainSettings
        {
            get
            {
                return this._DynamicMainSettings;
            }
            set
            {
                if (this._DynamicMainSettings != value)
                {
                    this._DynamicMainSettings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _DynamicMainSettings;
        //[PropertyOrderAttribute(11)]
        //[ExpandableObjectAttribute()]
        //[ReadOnly(true)]
        //[DisplayName("Nodes")]
        //[Description("Nodes default generators settings")]
        //// Dynamic class object of for node generators representation:
        //// Expandable node generator name
        ////    Expandable generator parameters object
        //public object DynamicNodesSettings
        //{
        //    get
        //    {
        //        //Config cfg = (Config)this.GetConfig();
        //        //var gen = cfg.DicActiveAppProjectGenerators[this.Guid];
        //        //var lst = gen?.GetListNodeGenerationSettings();
        //        var lstNodesSettings = new SortedObservableCollection<PluginGeneratorNodeSettings>();
        //        //foreach (var t in lst)
        //        //{
        //        //    var p = new PluginGeneratorNodeSettings(this, this.Guid, t);
        //        //    lstNodesSettings.Add(p);
        //        //}
        //        var res = SettingsTypeBuilder.CreateNodesSettingObject(lstNodesSettings);
        //        return res;
        //    }
        //}
        public void CreateGenSettings()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                {
                    this.GeneratorSettings = string.Empty;
                    return;
                }
                if (gen == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                    gen = cfg.DicActiveAppProjectGenerators[this.Guid];
                ChangeSettingsObject();
                //this.NodesSettings = gen?.Generator?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                HideProperties(gen);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ChangeSettingsObject()
        {
            if (gen is IvPluginDbConnStringGenerator)
            {
                if (gen != null)
                    this.DynamicMainSettings = (gen as IvPluginDbConnStringGenerator).ConnectionStringToVM(this.GeneratorSettings);
            }
            else
            {
                if (gen != null)
                    this.DynamicMainSettings = gen.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
            }
        }

        private string prevRelativePathToGenFolder = string.Empty;
        private string prevGenFileName = string.Empty;
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
            //this.NotifyPropertyChanged(() => this.DynamicNodesSettings);
            if (!string.IsNullOrWhiteSpace(this.GenFileName))
                prevGenFileName = this.GenFileName;
            this.GenFileName = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.RelativePathToGenFolder))
                prevRelativePathToGenFolder = this.RelativePathToGenFolder;
            this.RelativePathToGenFolder = string.Empty;
        }

        private void UpdateListGenerators()
        {
            Plugin plg = (Plugin)cfg.DicNodes[this.PluginGuid];
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
            //if (cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
            //    cfg.DicActiveAppProjectGenerators.Remove(this.Guid);
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.RemoveNodeAppGenSettings(this.Guid);
            });
            this.GeneratorSettings = string.Empty;
            this.DescriptionGenerator = string.Empty;
            this.gen = null;
        }
        partial void OnPluginGuidChanging(ref string to)
        {
            if (cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                cfg.DicActiveAppProjectGenerators.Remove(this.Guid);
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            if (this.IsNotNotifying)
                return;
            var nv = new ModelVisitorNodeGenSettings();
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
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.AddNodeAppGenSettings(this.Guid);
            });
            gen = cfg.DicActiveAppProjectGenerators[this.Guid];
            ChangeSettingsObject();
            this.DescriptionGenerator = gen.Description;
            if (gen is IvPluginDbConnStringGenerator)
            {
                this._GenFileName = "app-settings.json";
                this._RelativePathToGenFolder = string.Empty;
                //(gen as IvPluginDbConnStringGenerator).ConnectionString = "";
            }
            //else if (gen is IvPluginDbConnStringGenerator)
            //{
            //}
            else
            {
                this._GenFileName = prevGenFileName;
                this._RelativePathToGenFolder = prevRelativePathToGenFolder;
            }
            HideProperties(gen);
            this.NotifyPropertyChanged(this.IconName);
        }

        private void HideProperties(IvPluginGenerator gen)
        {
            if (gen == null)
            {
                this.AutoGenerateProperties = true;
            }
            else if (gen is IvPluginDbConnStringGenerator)
            {
                this.AutoGenerateProperties = false;
                this.SetPropertyDefinitions(new string[] {
                    this.GetPropertyName(() => this.RelativePathToGenFolder),
                    this.GetPropertyName(() => this.GenFileName),
                    this.GetPropertyName(() => this.ListGenerators),
                    this.GetPropertyName(() => this.ListInModels),
                });
            }
            //else if (gen is IvPluginDbConnStringGenerator)
            //{
            //}
            else
            {
                this.AutoGenerateProperties = false;
                this.SetPropertyDefinitions(new string[] {
                    this.GetPropertyName(() => this.ConnStr),
                    this.GetPropertyName(() => this.IsPrivateConnStr),
                });
            }
        }

        public string GetGenerationFilePath()
        {
            var path = this.GetGenerationFolderPath();
            path = Path.Combine(path, this.GenFileName);
            return path;
        }
        public string GetGenerationFolderPath()
        {
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            AppProject prj = this.Parent as AppProject;
            var path = prj.GetProjectFolderPath();
            if (string.IsNullOrWhiteSpace(this.RelativePathToGenFolder))
                path = Path.Combine(path, this.RelativePathToGenFolder);
            return path;
        }
        partial void OnRelativePathToGenFolderChanging(ref string to)
        {
            if (this.IsNotNotifying || string.IsNullOrWhiteSpace(to))
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativePathToGenFolderChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                throw new Exception("Config is not saved yet");
            var sln = this.Parent.Parent as AppSolution;
            if (sln.RelativeAppSolutionPath == null)
                throw new Exception("Solution path is not selected yet");
            var prj = this.Parent as AppProject;
            if (prj.RelativeAppProjectPath == null)
                throw new Exception("Project path is not selected yet");
            if (!string.IsNullOrWhiteSpace(this._RelativePathToGenFolder))
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
        //public bool CanAddSubNode()
        //{
        //    if (this.PluginGeneratorGuid == null)
        //        return false;
        //    Config cfg = (Config)this.GetConfig();
        //    var gen = cfg.DicActiveAppProjectGenerators[this.Guid];
        //    if (gen.PluginGeneratorType == vPluginLayerTypeEnum.DbDesign)
        //    {
        //        var plugin = cfg.DicPlugins[this.PluginGuid];
        //        foreach (var t in plugin.ListGenerators)
        //        {
        //            if (t.PluginGeneratorType == vPluginLayerTypeEnum.DbConnection)
        //                return true;
        //        }
        //        //var pluginLst = cfg.DicPluginLists[vPluginLayerTypeEnum.DbDesign];
        //        //(gen as IvPluginDbGenerator).
        //    }
        //    return false;
        //}
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
            this.PluginGeneratorGuid = "";
            //var nv = new ModelVisitorNodeGenSettings();
            //nv.NodeGenSettingsApplyAction(cfg, (p) =>
            //{
            //    p.RemoveNodeAppGenSettings(this.Guid);
            //});
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
