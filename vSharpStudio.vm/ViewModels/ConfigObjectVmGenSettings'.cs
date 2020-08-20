using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
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
                            var guid = ttt.Guid;
                            var gen = cfg.DicActiveAppProjectGenerators[ttt.Guid];
                            if (gen is IvPluginDbConnStringGenerator)
                            {
                                guid = (gen as IvPluginDbConnStringGenerator).DbGenerator.Guid;
                            }
                            else
                                this.AddNodeAppGenSettings(guid);
                        }
                    }
                }
            }
        }

        #region Node App Generator Settings

        private void SearchPathAndAdd(AppProjectGenerator appProjectGenerator, INodeGenSettings ngs, IvPluginGenerator gen)
        {
            var cfg = (Config)this.GetConfig();
            List<IvPluginGeneratorNodeSettings> lst = null;
            if (gen is IvPluginDbConnStringGenerator)
            {
                var genDb = (gen as IvPluginDbConnStringGenerator).DbGenerator;
                lst = genDb.GetListNodeGenerationSettings();
            }
            else
            {
                lst = gen.GetListNodeGenerationSettings();
            }
            foreach (var t in lst)
            {
                if (DicGenNodeSettings.ContainsKey(t.Guid))
                    continue;
                string modelPath = this.ModelPath;
                var searchPattern = t.SearchPathInModel;

                bool is_found = SearchInModelPathByPattern(modelPath, searchPattern);

                if (is_found)
                {
                    PluginGeneratorNodeSettings gs = null;
                    foreach (var ttt in ngs.ListNodeGeneratorsSettings)
                    {
                        if (ttt.NodeSettingsVmGuid == t.Guid)
                        {
                            gs = ttt;
                            break;
                        }
                    }
                    if (gs == null)
                    {
                        gs = new PluginGeneratorNodeSettings(this);
                        gs.Name = appProjectGenerator.Name;
                        gs.NodeSettingsVmGuid = t.Guid;
                        gs.AppProjectGeneratorGuid = appProjectGenerator.Guid;
                        _logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appProjectGenerator.Name);
                        ngs.ListNodeGeneratorsSettings.Add(gs);
                    }
                    gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings, this is ConfigModel);
                    if (!this.DicGenNodeSettings.ContainsKey(appProjectGenerator.Guid))
                    {
                        this.DicGenNodeSettings[appProjectGenerator.Guid] = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
                    }
                    var dicS = this.DicGenNodeSettings[appProjectGenerator.Guid];
                    dicS[gs.NodeSettingsVmGuid] = gs.SettingsVm;
                    //// Model default settings
                    //gs = null;
                    //foreach (var ttt in cfg.Model.ListNodeGeneratorsSettings)
                    //{
                    //    if (ttt.NodeSettingsVmGuid == t.Guid)
                    //    {
                    //        gs = ttt;
                    //        break;
                    //    }
                    //}
                    //if (gs == null)
                    //{
                    //    gs = new PluginGeneratorNodeSettings(this);
                    //    gs.Name = appProjectGenerator.Name;
                    //    gs.NodeSettingsVmGuid = t.Guid;
                    //    gs.AppProjectGeneratorGuid = appProjectGenerator.Guid;
                    //    _logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appProjectGenerator.Name);
                    //    cfg.Model.ListNodeGeneratorsSettings.Add(gs);
                    //}
                    //gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings);
                    //if (!cfg.Model.DicGenNodeSettings.ContainsKey(appProjectGenerator.Guid))
                    //{
                    //    cfg.Model.DicGenNodeSettings[appProjectGenerator.Guid] = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
                    //}
                    //dicS = cfg.Model.DicGenNodeSettings[appProjectGenerator.Guid];
                    //dicS[gs.NodeSettingsVmGuid] = t.GetAppGenerationNodeSettingsVm(gs.Settings, true);
                }
            }
        }
        public static bool SearchInModelPathByPattern(string modelPath, string searchPattern)
        {
            Contract.Requires(searchPattern != null);
            var subPatterns = searchPattern.Split(';');
            if (subPatterns.Count() == 1)
            {
                if (string.IsNullOrWhiteSpace(subPatterns[0]) || subPatterns[0] == "*")
                    return true;
            }
            bool is_found = true;
            bool is_negative = false;
            foreach (var t in subPatterns)
            {
                string tt = t;
                if (t[0] == '!')
                {
                    is_negative = true;
                    tt = t.Substring(1);
                }
#if NET48
                        var sp = Split(t, ".*.");
#else
                var sp = tt.Split(".*.");
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
            if (is_negative)
                return !is_found;
            return is_found;
        }
        public void AddNodeAppGenSettings(string appProjectGeneratorGuid)
        {
            _logger.Trace();
            //var cnfg = this.GetConfig();
            var ngs = (INodeGenSettings)this;
            //#if DEBUG
            //            if (ngs == null)
            //                throw new Exception();
            //            foreach (var t in ngs.ListNodeGeneratorsSettings)
            //            {
            //                if (t.AppProjectGeneratorGuid == appProjectGeneratorGuid)
            //                    throw new Exception();
            //            }
            //#endif
            var cfg = (Config)this.GetConfig();
            //var gen = cfg.DicActiveAppProjectGenerators[appGenGuid];
            //var set = new PluginGeneratorMainSettings(this);
            //set.AppGeneratorGuid = appGenGuid;
            //set.SettingsVm = gen.GetAppGenerationSettingsVmFromJson("");
            //ngs.ListNodeGeneratorsSettings.Add(set);
            var appgen = (AppProjectGenerator)cfg.DicNodes[appProjectGeneratorGuid];
            var gen = cfg.DicActiveAppProjectGenerators[appProjectGeneratorGuid];
            SearchPathAndAdd(appgen, ngs, gen);
        }
        public void RestoreNodeAppGenSettingsVm()
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            var cfg = (Config)this.GetConfig();
            foreach (var t in cfg.DicActiveAppProjectGenerators)
            {
                this.AddNodeAppGenSettings(t.Key);
                //AddAppProjectGenerator(ngs, t.Value);
            }
        }

        //private void AddAppProjectGenerator(INodeGenSettings ngs, KeyValuePair<string, IvPluginGenerator> t)
        //private void AddAppProjectGenerator(INodeGenSettings ngs, IvPluginGenerator t)
        //{
        //    foreach (var tt in t.GetListNodeGenerationSettings())
        //    {
        //        bool is_found = false;
        //        foreach (var ttt in ngs.ListNodeGeneratorsSettings)
        //        {
        //            if (tt.Guid == ttt.NodeSettingsVmGuid)
        //            {
        //                ttt.SettingsVm = tt.GetAppGenerationNodeSettingsVm(ttt.Settings);
        //                dicGenNodeSettings[ttt.NodeSettingsVmGuid] = ttt.SettingsVm;
        //                is_found = true;
        //                break;
        //            }
        //        }
        //        if (!is_found)
        //        {
        //            //var appgen = (AppProjectGenerator)cfg.DicNodes[appProjectGeneratorGuid];
        //            //var gen = cfg.DicActiveAppProjectGenerators[appProjectGeneratorGuid];
        //            //SearchPathAndAdd(appgen, ngs, gen);
        //            var p = new PluginGeneratorNodeSettings(this);
        //            p.SettingsVm = tt.GetAppGenerationNodeSettingsVm(tt.SettingsAsJsonDefault);
        //            p.NodeSettingsVmGuid = tt.Guid;
        //            dicGenNodeSettings[p.NodeSettingsVmGuid] = p.SettingsVm;
        //            ngs.ListNodeGeneratorsSettings.Add(p);
        //        }
        //    }
        //}

        public void SaveNodeAppGenSettings()
        {
            //if (this is ConfigModel)
            //    return;
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
            if (!this.DicGenNodeSettings.ContainsKey(appGenGuid))
                return;
            var ngs = (INodeGenSettings)this;
            for (int i = ngs.ListNodeGeneratorsSettings.Count - 1; i > -1; i--)
            {
                var t = ngs.ListNodeGeneratorsSettings[i];
                if (t.AppProjectGeneratorGuid == appGenGuid)
                {
                    //DicGenNodeSettings.Remove(t.NodeSettingsVmGuid);
                    ngs.ListNodeGeneratorsSettings.RemoveAt(i);
                    break;
                }
            }
            this.DicGenNodeSettings.Remove(appGenGuid);
        }

        #endregion Node App Generator Settings

        [BrowsableAttribute(false)]
        // AppProjectGenerator guid, Settings quid, IvPluginGeneratorNodeSettings
        public DictionaryExt<string, DictionaryExt<string, IvPluginGeneratorNodeSettings>> DicGenNodeSettings { get { return dicGenNodeSettings; } }
        private DictionaryExt<string, DictionaryExt<string, IvPluginGeneratorNodeSettings>> dicGenNodeSettings =
            new DictionaryExt<string, DictionaryExt<string, IvPluginGeneratorNodeSettings>>(20, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
        /// <summary>
        /// Getting VM of generator settings for node
        /// </summary>
        /// <param name="guidAppPrjGen">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public bool IsIncluded(string guidAppPrjGen, string guidSettings)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();

            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenSettings;
                if (ngs != null)
                {
                    if (ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen) && ngs.DicGenNodeSettings[guidAppPrjGen].ContainsKey(guidSettings))
                    {
                        //if (!ngs.DicGenNodeSettings.ContainsKey(guid))
                        //    return true;
                        var settings = (IvPluginGeneratorNodeIncludable)(ngs.DicGenNodeSettings[guidAppPrjGen][guidSettings]);
                        if (settings.IsIncluded.HasValue)
                        {
                            return settings.IsIncluded ?? false;
                        }
                    }
                }
                else if (p is ConfigModel)
                {
                    var m = p as ConfigModel;
                    if (m.DicGenNodeSettings.ContainsKey(guidAppPrjGen) && m.DicGenNodeSettings[guidAppPrjGen].ContainsKey(guidSettings))
                    {
                        var settings = (IvPluginGeneratorNodeIncludable)(m.DicGenNodeSettings[guidAppPrjGen][guidSettings]);
                        if (!settings.IsIncluded.HasValue)
                        {
                            return true;
                        }
                        return settings.IsIncluded ?? false;
                    }
                    return true;
                }
                else
                    throw new Exception();
                //if (p.Parent == null)
                //    return true;
                p = p.Parent;
            }
            return true;
        }
        /// <summary>
        /// Getting VM of generator settings for node
        /// </summary>
        /// <param name="guidAppPrjGen">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guidSettings)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            var dic = DicGenNodeSettings[guidAppPrjGen];
            if (!dic.ContainsKey(guidSettings))
                throw new Exception();
            return dic[guidSettings];
        }
        public T GetSettings<T>(string guidAppPrjGen, string guidSettings, Func<ITreeConfigNode, T, bool> found)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen) && ngs.DicGenNodeSettings[guidAppPrjGen].ContainsKey(guidSettings))
                {
                    var res = (T)ngs.DicGenNodeSettings[guidAppPrjGen][guidSettings];
                    if (found(p, res))
                        return res;
                }
                p = p.Parent;
            }
            return default(T);
        }
        public TValue GetSettingsValue<T, TValue>(string guidAppPrjGen, string guidSettings, Action<ITreeConfigNode, T, Result<TValue>> found)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            ITreeConfigNode p = this;
            Result<TValue> res = new Result<TValue>();
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen) && ngs.DicGenNodeSettings[guidAppPrjGen].ContainsKey(guidSettings))
                {
                    var st = (T)ngs.DicGenNodeSettings[guidAppPrjGen][guidSettings];
                    found(p, st, res);
                    if (!res.IsContinue)
                        return res.Value;
                }
                p = p.Parent;
            }
            return default(TValue);
        }
        /// <summary>
        /// Getting VM of generator settings for node with capability analyze tree
        /// </summary>
        /// <param name="guidAppPrjGen">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public void GetSettings(string guidAppPrjGen, string guidSettings, Func<ITreeConfigNode, IvPluginGeneratorNodeSettings, bool> toParents)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen) && ngs.DicGenNodeSettings[guidAppPrjGen].ContainsKey(guidSettings))
                {
                    var res = ngs.DicGenNodeSettings[guidAppPrjGen][guidSettings];
                    if (!toParents(p, res))
                        break;
                }
                p = p.Parent;
            }
        }
        public bool ContainsSettings(string guidAppPrjGen, string guidSettings)
        {
            if (DicGenNodeSettings.ContainsKey(guidAppPrjGen))
            {
                var dic = DicGenNodeSettings[guidAppPrjGen];
                return dic.ContainsKey(guidSettings);
            }
            return false;
        }
        public bool TrySetSettings(string guidAppPrjGen, string guidSettings, IvPluginGeneratorNodeSettings setting)
        {
            bool res = false;
            DictionaryExt<string, IvPluginGeneratorNodeSettings> dic = null;
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
            {
                this.DicGenNodeSettings[guidAppPrjGen] = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
            }
            dic = this.DicGenNodeSettings[guidAppPrjGen];
            if (!dic.ContainsKey(guidSettings))
            {
                dic[guidSettings] = setting;
                res = true;
            }
            return res;
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
                var nd = new NodeSettings();
                var res = nd.Run(this, true);
                return res;
            }
        }
    }
}
