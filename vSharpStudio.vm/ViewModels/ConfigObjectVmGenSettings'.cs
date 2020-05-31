using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectVmGenSettings<T, TValidator> : ConfigObjectVmBase<T, TValidator>, IGetNodeSetting
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectVmGenSettings<T, TValidator>//, IComparable<T>, ISortingValue 
    {
        public ConfigObjectVmGenSettings(ITreeConfigNode parent, TValidator validator)
            : base(parent, validator)
        {
        }
        //[BrowsableAttribute(false)]
        //public ConfigNodesCollection<PluginGeneratorMainSettings> ListGeneratorsSettings
        //{
        //    get
        //    {
        //        if (this._ListGeneratorsSettings == null)
        //            this._ListGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorMainSettings>(this);
        //        return this._ListGeneratorsSettings;
        //    }
        //    set
        //    {
        //        if (this._ListGeneratorsSettings != value)
        //        {
        //            this._ListGeneratorsSettings = value;
        //            this.NotifyPropertyChanged();
        //            //this.ValidateProperty();
        //        }
        //    }
        //}
        //private ConfigNodesCollection<PluginGeneratorMainSettings> _ListGeneratorsSettings;

        // Settings workflow:
        // 1. When Config is loaded: init all generators settings VMs on all model nodes
        // 2. When model node is added: init all generators settings VMs on this node
        // 3. When new generator is selected: old generator has to be removed from all model nodes, 
        //     and new generator settings has to be added for all model nodes
        // 4. When saving Config: convert all model nodes generators settings to string representations

        // 2. When model node is added: init all generators settings VMs on this node
        public void AddAllAppGenSettingsVmsToNode()
        {
            if (!(this is INodeGenSettings))
                return;
            var cfg = (Config)this.GetConfig();
            if (cfg == null || cfg.GroupAppSolutions == null)
                return;
            _logger.LogTrace("Try Add Node Settings. {Count}".CallerInfo(), cfg.DicActiveAppProjectGenerators.Count);
            foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (cfg.DicActiveAppProjectGenerators.ContainsKey(ttt.Guid))
                        {
                            this.AddNodeAppGenSettings(ttt.Guid);
                        }
                    }
                }
            }
        }

        #region Node App Generator Settings

        private void SearchPathAndAdd(AppProjectGenerator appgen, INodeGenSettings ngs, IvPluginGenerator gen)
        {
            var lst = gen.GetListNodeGenerationSettings();
            foreach (var t in lst)
            {
                string modelPath = this.ModelPath;
                var searchPattern = t.SearchPathInModel;
                var is_found = SearchInModelPathByPattern(modelPath, searchPattern);

                if (is_found)
                {
                    PluginGeneratorNodeSettings gs = new PluginGeneratorNodeSettings(this);
                    gs.Name = appgen.Name;
                    gs.NodeSettingsVmGuid = t.Guid;
                    gs.AppProjectGeneratorGuid = appgen.Guid;
                    _logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appgen.Name);
#if DEBUG
                    foreach (var ttt in ngs.ListNodeGeneratorsSettings)
                    {
                        if (ttt.NodeSettingsVmGuid == gs.NodeSettingsVmGuid)
                            throw new Exception();
                    }
#endif
                    ngs.ListNodeGeneratorsSettings.Add(gs);
                    gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings);
                    dicGenNodeSettings[gs.NodeSettingsVmGuid] = gs.SettingsVm;
                }
            }
        }
        public static bool SearchInModelPathByPattern(string modelPath, string searchPattern)
        {
            var subPatterns = searchPattern.Split(';');
            if (subPatterns.Count() == 1)
            {
                if (string.IsNullOrWhiteSpace(subPatterns[0]) || subPatterns[0] == "*")
                    return true;
            }
            bool is_found = true;
            foreach (var t in subPatterns)
            {
#if NET48
                        var sp = Split(t, ".*.");
#else
                var sp = t.Split(".*.");
#endif
                is_found = true;
                int indx = 0;
                foreach (var s in sp)
                {
                    var sd = "." + s;
                    if (modelPath.Contains(s))
                    {
                        indx = modelPath.IndexOf(sd) + sd.Length;
                        modelPath = modelPath.Substring(indx);
                    }
                    else
                    {
                        is_found = false;
                        break;
                    }
                }
                if (modelPath.Length > 0)
                    is_found = false;
                if (is_found)
                    break;
            }
            return is_found;
        }
        public void RestoreNodeAppGenSettingsVm()
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            var cfg = (Config)this.GetConfig();
            foreach (var t in cfg.DicActiveAppProjectGenerators)
            {
                foreach (var tt in t.Value.GetListNodeGenerationSettings())
                {
                    bool is_found = false;
                    foreach (var ttt in ngs.ListNodeGeneratorsSettings)
                    {
                        if (ttt.Guid == ttt.NodeSettingsVmGuid)
                        {
                            ttt.SettingsVm = tt.GetAppGenerationNodeSettingsVm(ttt.Settings);
                            dicGenNodeSettings[ttt.NodeSettingsVmGuid] = ttt.SettingsVm;
                            is_found = true;
                            break;
                        }
                    }
                    if (!is_found)
                    {
                        var p = new PluginGeneratorNodeSettings(this);
                        p.SettingsVm = tt.GetAppGenerationNodeSettingsVm(tt.SettingsAsJsonDefault);
                        p.NodeSettingsVmGuid = tt.Guid;
                        dicGenNodeSettings[p.NodeSettingsVmGuid] = p.SettingsVm;
                        ngs.ListNodeGeneratorsSettings.Add(p);
                    }
                }
            }
            //foreach (var t in ngs.ListNodeGeneratorsSettings)
            //{
            //    if (cfg.DicActiveAppProjectGenerators.ContainsKey(t.AppProjectGeneratorGuid))
            //    {
            //        var gen = cfg.DicActiveAppProjectGenerators[t.AppProjectGeneratorGuid];
            //        var appgen = (AppProjectGenerator)(cfg.DicNodes[t.AppProjectGeneratorGuid]);
            //        bool is_found = false;
            //        foreach (var tt in gen.GetListNodeGenerationSettings())
            //        {
            //            if (tt.Guid == t.NodeSettingsVmGuid)
            //            {
            //                t.SettingsVm = tt.GetAppGenerationNodeSettingsVm(t.Settings);
            //                is_found = true;
            //                break;
            //            }
            //        }
            //        if (!is_found)
            //            throw new Exception();
            //    }
            //    else
            //        throw new Exception();
            //}
        }
        public void SaveNodeAppGenSettings()
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            foreach (var t in ngs.ListNodeGeneratorsSettings.ToList())
            {
                t.Settings = t.SettingsVm.SettingsAsJson;
                if (t.Settings == t.SettingsVm.SettingsAsJsonDefault)
                    ngs.ListNodeGeneratorsSettings.Remove(t);
            }
        }
        public void RemoveNodeAppGenSettings(string appGenGuid)
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            for (int i = ngs.ListNodeGeneratorsSettings.Count - 1; i > -1; i--)
            {
                var t = ngs.ListNodeGeneratorsSettings[i];
                if (t.AppProjectGeneratorGuid == appGenGuid)
                {
                    dicGenNodeSettings.Remove(t.NodeSettingsVmGuid);
                    ngs.ListNodeGeneratorsSettings.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddNodeAppGenSettings(string appGenGuid)
        {
            _logger.Trace();
            //var cnfg = this.GetConfig();
            var ngs = (INodeGenSettings)this;
#if DEBUG
            if (ngs == null)
                throw new Exception();
            foreach (var t in ngs.ListNodeGeneratorsSettings)
            {
                if (t.AppProjectGeneratorGuid == appGenGuid)
                    throw new Exception();
            }
#endif
            var cfg = (Config)this.GetConfig();
            //var gen = cfg.DicActiveAppProjectGenerators[appGenGuid];
            //var set = new PluginGeneratorMainSettings(this);
            //set.AppGeneratorGuid = appGenGuid;
            //set.SettingsVm = gen.GetAppGenerationSettingsVmFromJson("");
            //ngs.ListNodeGeneratorsSettings.Add(set);
            var appgen = (AppProjectGenerator)cfg.DicNodes[appGenGuid];
            var gen = cfg.DicActiveAppProjectGenerators[appGenGuid];
            SearchPathAndAdd(appgen, ngs, gen);
        }

        #endregion Node App Generator Settings

        protected Dictionary<string, IvPluginNodeSettings> dicGenNodeSettings = new Dictionary<string, IvPluginNodeSettings>();
        /// <summary>
        /// Getting VM of generator settings for node
        /// </summary>
        /// <param name="guid">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public IvPluginNodeSettings GetSettings(string guid)
        {
            //if (!dicGenNodeSettings.ContainsKey(guid))
            //{
            //    var tt = this as INodeGenSettings;
            //    foreach (var t in tt.ListNodeGeneratorsSettings)
            //    {
            //        if (!dicGenNodeSettings.ContainsKey(t.NodeSettingsVmGuid))
            //            dicGenNodeSettings[t.NodeSettingsVmGuid] = t.SettingsVm;
            //    }
            //}
            if (!dicGenNodeSettings.ContainsKey(guid))
                throw new Exception();
            return dicGenNodeSettings[guid];
        }
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Settings")]
        [Description("Generators node settings")]
        // Dynamic class object of for node generators representation:
        // Expandable node generator name
        //    Expandable generator parameters object
        public object DynamicNodesSettings
        {
            get
            {
                //Config cfg = (Config)this.GetConfig();
                //var gen = cfg.DicActiveAppProjectGenerators[this.Guid];
                //var lst = gen?.GetListNodeGenerationSettings();
                //var lstNodesSettings = new SortedObservableCollection<PluginGeneratorNodeSettings>();
                //foreach (var t in lst)
                //{
                //    var p = new PluginGeneratorNodeSettings(this, this.Guid, t);
                //    lstNodesSettings.Add(p);
                //}
                //var res = SettingsTypeBuilder.CreateNodesSettingObject(lstNodesSettings);
                var nd = new NodeSettings();
                var res = nd.Run(this);
                return res;
            }
        }
    }
}
