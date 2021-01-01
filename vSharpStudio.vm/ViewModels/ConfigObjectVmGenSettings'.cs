using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            //this.HidePropertiesForXceedPropertyGrid();
            this.DicVmExclProps = new Dictionary<string, Dictionary<string, string>>();
        }
        [BrowsableAttribute(false)]
        public Dictionary<string, Dictionary<string, string>> DicVmExclProps { get; private set; }
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
                            this.AddNodeAppGenSettings(guid);
                        }
                    }
                }
            }
        }

        #region Node App Generator Settings


        //private void SearchPathAndAdd(AppProjectGenerator appProjectGenerator, INodeGenSettings ngs, IvPluginGenerator gen)
        //{
        //    var cfg = (Config)this.GetConfig();
        //    var lst = gen.GetListNodeGenerationSettings();
        //    foreach (var t in lst)
        //    {
        //        if (DicGenNodeSettings.ContainsKey(t.Guid))
        //            continue;
        //        string modelPath = this.ModelPath;
        //        var searchPattern = t.SearchPathInModel;

        //        bool is_found = SearchInModelPathByPattern(modelPath, searchPattern);

        //        if (is_found)
        //        {
        //            PluginGeneratorNodeSettings gs = null;
        //            foreach (var ttt in ngs.ListNodeGeneratorsSettings)
        //            {
        //                if (ttt.NodeSettingsVmGuid == t.Guid)
        //                {
        //                    gs = ttt;
        //                    break;
        //                }
        //            }
        //            if (gs == null)
        //            {
        //                gs = new PluginGeneratorNodeSettings(this);
        //                gs.Name = appProjectGenerator.Name;
        //                gs.NodeSettingsVmGuid = t.Guid;
        //                gs.AppProjectGeneratorGuid = appProjectGenerator.Guid;
        //                _logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appProjectGenerator.Name);
        //                ngs.ListNodeGeneratorsSettings.Add(gs);
        //            }
        //            gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings, this is ConfigModel);
        //            if (!this.DicGenNodeSettings.ContainsKey(appProjectGenerator.Guid))
        //            {
        //                this.DicGenNodeSettings[appProjectGenerator.Guid] = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
        //            }
        //            var dicS = this.DicGenNodeSettings[appProjectGenerator.Guid];
        //            dicS[gs.NodeSettingsVmGuid] = gs.SettingsVm;
        //            //// Model default settings
        //            //gs = null;
        //            //foreach (var ttt in cfg.Model.ListNodeGeneratorsSettings)
        //            //{
        //            //    if (ttt.NodeSettingsVmGuid == t.Guid)
        //            //    {
        //            //        gs = ttt;
        //            //        break;
        //            //    }
        //            //}
        //            //if (gs == null)
        //            //{
        //            //    gs = new PluginGeneratorNodeSettings(this);
        //            //    gs.Name = appProjectGenerator.Name;
        //            //    gs.NodeSettingsVmGuid = t.Guid;
        //            //    gs.AppProjectGeneratorGuid = appProjectGenerator.Guid;
        //            //    _logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appProjectGenerator.Name);
        //            //    cfg.Model.ListNodeGeneratorsSettings.Add(gs);
        //            //}
        //            //gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings);
        //            //if (!cfg.Model.DicGenNodeSettings.ContainsKey(appProjectGenerator.Guid))
        //            //{
        //            //    cfg.Model.DicGenNodeSettings[appProjectGenerator.Guid] = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
        //            //}
        //            //dicS = cfg.Model.DicGenNodeSettings[appProjectGenerator.Guid];
        //            //dicS[gs.NodeSettingsVmGuid] = t.GetAppGenerationNodeSettingsVm(gs.Settings, true);
        //        }
        //    }
        //}
        //        public static bool SearchInModelPathByPattern(string modelPath, string searchPattern)
        //        {
        //            Contract.Requires(searchPattern != null);
        //            var subPatterns = searchPattern.Split(';');
        //            if (subPatterns.Count() == 1)
        //            {
        //                if (string.IsNullOrWhiteSpace(subPatterns[0]) || subPatterns[0] == "*")
        //                    return true;
        //            }
        //            bool is_found = true;
        //            bool is_negative = false;
        //            foreach (var t in subPatterns)
        //            {
        //                string tt = t;
        //                if (t[0] == '!')
        //                {
        //                    is_negative = true;
        //                    tt = t.Substring(1);
        //                }
        //#if NET48
        //                        var sp = Split(t, ".*.");
        //#else
        //                var sp = tt.Split(".*.");
        //#endif
        //                is_found = true;
        //                int indx = 0;
        //                foreach (var s in sp)
        //                {
        //                    var sd = "." + s;
        //                    if (modelPath.Contains(s))
        //                    {
        //                        indx = modelPath.IndexOf(sd) + sd.Length;
        //                        modelPath = modelPath.Substring(indx);
        //                    }
        //                    else
        //                    {
        //                        is_found = false;
        //                        break;
        //                    }
        //                }
        //                if (modelPath.Length > 0)
        //                    is_found = false;
        //                if (is_found)
        //                    break;
        //            }
        //            if (is_negative)
        //                return !is_found;
        //            return is_found;
        //        }
        public void AddNodeAppGenSettings(string appProjectGeneratorGuid)
        {
            _logger.Trace();
            Debug.Assert(!this.DicGenNodeSettings.ContainsKey(appProjectGeneratorGuid));
            var ngs = (INodeGenSettings)this;
            var cfg = (Config)this.GetConfig();
            var appgen = (AppProjectGenerator)cfg.DicNodes[appProjectGeneratorGuid];
            var gen = cfg.DicActiveAppProjectGenerators[appProjectGeneratorGuid];
            PluginGeneratorNodeSettings gs = null;
            foreach (var ts in ngs.ListNodeGeneratorsSettings)
            {
                if (ts.Guid == appProjectGeneratorGuid)
                {
                    gs = ts;
                }
            }
            if (gs == null)
            {
                var t = gen.GetGenerationNodeSettingsVmFromJson(null, this);
                this.DicVmExclProps[t.GetType().Name] = t.DicNodeExcludedProperties;
                this.DicGenNodeSettings[appProjectGeneratorGuid] = t;
                gs = new PluginGeneratorNodeSettings(this);
                gs.Name = appgen.Name;
                gs.AppProjectGeneratorGuid = appgen.Guid;
                gs.SettingsVm = t;
                //_logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appProjectGenerator.Name);
                ngs.ListNodeGeneratorsSettings.Add(gs);
            }
            else
            {
                var t = gen.GetGenerationNodeSettingsVmFromJson(gs.Settings, this);
                this.DicVmExclProps[t.GetType().Name] = t.DicNodeExcludedProperties;
                gs.SettingsVm = t;
                this.DicGenNodeSettings[appProjectGeneratorGuid] = t;
            }
        }
        public void RestoreNodeAppGenSettingsVm()
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            var cfg = (Config)this.GetConfig();
            foreach (var tt in ngs.ListNodeGeneratorsSettings)
            {
                var gen = cfg.DicActiveAppProjectGenerators[tt.AppProjectGeneratorGuid];
                tt.SettingsVm = gen.GetGenerationNodeSettingsVmFromJson(tt.Settings, this);
                this.DicVmExclProps[tt.SettingsVm.GetType().Name] = tt.SettingsVm.DicNodeExcludedProperties;
                ngs.DicGenNodeSettings[tt.AppProjectGeneratorGuid] = tt.SettingsVm;
            }
            //foreach (var t in cfg.DicActiveAppProjectGenerators)
            //{
            //    //this.AddNodeAppGenSettings(t.Key);
            //}
        }
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
                    t.Settings = string.Empty;
                //    ngs.ListNodeGeneratorsSettings.Remove(t);
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
                    ngs.ListNodeGeneratorsSettings.RemoveAt(i);
                    break;
                }
            }
            this.DicGenNodeSettings.Remove(appGenGuid);
        }

        #endregion Node App Generator Settings

        [BrowsableAttribute(false)]
        // AppProjectGenerator guid, Settings quid, IvPluginGeneratorNodeSettings
        public DictionaryExt<string, IvPluginGeneratorNodeSettings> DicGenNodeSettings { get { return dicGenNodeSettings; } }
        private DictionaryExt<string, IvPluginGeneratorNodeSettings> dicGenNodeSettings =
            new DictionaryExt<string, IvPluginGeneratorNodeSettings>(20, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
        /// <summary>
        /// Getting VM of generator settings for node
        /// </summary>
        /// <param name="guidAppPrjGen">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public bool IsIncluded(string guidAppPrjGen, bool isFromPrevStable = false)
        {
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();

            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenSettings;
                if (p is ConfigModel)
                {
                    var m = p as ConfigModel;
                    if (m.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var settings = (IvPluginGeneratorNodeIncludable)(m.DicGenNodeSettings[guidAppPrjGen]);
                        if (!settings.IsIncluded.HasValue || settings.IsIncluded.Value)
                        {
                            return true;
                        }
                        if (!isFromPrevStable)
                            return this.IsIncludedInStable(guidAppPrjGen);
                        return false;
                    }
                    return true;
                }
                else if (ngs != null)
                {
                    if (ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        //if (!ngs.DicGenNodeSettings.ContainsKey(guid))
                        //    return true;
                        var settings = (IvPluginGeneratorNodeIncludable)(ngs.DicGenNodeSettings[guidAppPrjGen]);
                        if (settings.IsIncluded.HasValue)
                        {
                            if (settings.IsIncluded.Value)
                            {
                                return true;
                            }
                            if (!isFromPrevStable)
                                return this.IsIncludedInStable(guidAppPrjGen);
                            return false;
                        }
                    }
                }
                else
                    throw new Exception();
                //if (p.Parent == null)
                //    return true;
                p = p.Parent;
            }
            return true;
        }
        private bool IsIncludedInStable(string guidAppPrjGen)
        {
            var cfg = (Config)this.GetConfig();
            var prev = cfg.PrevStableConfig;
            if (prev != null && prev.DicNodes.ContainsKey(this.Guid))
            {
                var prevNode = (IGetNodeSetting)prev.DicNodes[this.Guid];
                return prevNode.IsIncluded(guidAppPrjGen, true);
            }
            return false;
        }
        public bool GetBoolSetting(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, bool?> func, bool isFromPrevStable = false)
        {
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();

            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenSettings;
                if (p is ConfigModel)
                {
                    var m = p as ConfigModel;
                    if (m.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var setting = func(m.DicGenNodeSettings[guidAppPrjGen]);
                        if (!setting.HasValue || setting.Value)
                        {
                            return true;
                        }
                        if (!isFromPrevStable)
                            return this.GetBoolSettingInStable(guidAppPrjGen, func);
                        return false;
                    }
                    return true;
                }
                else if (ngs != null)
                {
                    if (ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var setting = func(ngs.DicGenNodeSettings[guidAppPrjGen]);
                        if (setting.HasValue)
                        {
                            if (setting.Value)
                            {
                                return true;
                            }
                            if (!isFromPrevStable)
                                return this.GetBoolSettingInStable(guidAppPrjGen, func);
                            return false;
                        }
                    }
                }
                else
                    throw new Exception();
                p = p.Parent;
            }
            return true;
        }
        private bool GetBoolSettingInStable(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, bool?> func)
        {
            var cfg = (Config)this.GetConfig();
            var prev = cfg.PrevStableConfig;
            if (prev != null && prev.DicNodes.ContainsKey(this.Guid))
            {
                var prevNode = (IGetNodeSetting)prev.DicNodes[this.Guid];
                return prevNode.GetBoolSetting(guidAppPrjGen, func, true);
            }
            return false;
        }
        public string GetStringSetting(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, string> func, bool isFromPrevStable = false)
        {
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();

            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenSettings;
                if (p is ConfigModel)
                {
                    var m = p as ConfigModel;
                    if (m.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var setting = func(m.DicGenNodeSettings[guidAppPrjGen]);
                        if (!string.IsNullOrWhiteSpace(setting))
                        {
                            return setting;
                        }
                        if (!isFromPrevStable)
                            return this.GetStringSettingInStable(guidAppPrjGen, func);
                    }
                    return "";
                }
                else if (ngs != null)
                {
                    if (ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var setting = func(ngs.DicGenNodeSettings[guidAppPrjGen]);
                        if (!string.IsNullOrWhiteSpace(setting))
                        {
                            return setting;
                        }
                        if (!isFromPrevStable)
                            return this.GetStringSettingInStable(guidAppPrjGen, func);
                    }
                    return "";
                }
                else
                    throw new Exception();
                p = p.Parent;
            }
            return "";
        }
        private string GetStringSettingInStable(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, string> func)
        {
            var cfg = (Config)this.GetConfig();
            var prev = cfg.PrevStableConfig;
            if (prev != null && prev.DicNodes.ContainsKey(this.Guid))
            {
                var prevNode = (IGetNodeSetting)prev.DicNodes[this.Guid];
                return prevNode.GetStringSetting(guidAppPrjGen, func, true);
            }
            return "";
        }
        /// <summary>
        /// Getting VM of generator settings for node
        /// </summary>
        /// <param name="guidAppPrjGen">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            var res = DicGenNodeSettings[guidAppPrjGen];
            return res;
        }
        public T GetSettings<T>(string guidAppPrjGen, Func<ITreeConfigNode, T, bool> found)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                {
                    var res = (T)ngs.DicGenNodeSettings[guidAppPrjGen];
                    if (found(p, res))
                        return res;
                }
                p = p.Parent;
            }
            return default(T);
        }
        public TValue GetSettingsValue<T, TValue>(string guidAppPrjGen, Action<ITreeConfigNode, T, Result<TValue>> found)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            ITreeConfigNode p = this;
            Result<TValue> res = new Result<TValue>();
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                {
                    var st = (T)ngs.DicGenNodeSettings[guidAppPrjGen];
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
        public void GetSettings(string guidAppPrjGen, Func<ITreeConfigNode, IvPluginGeneratorNodeSettings, bool> toParents)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            ITreeConfigNode p = this;
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                {
                    var res = ngs.DicGenNodeSettings[guidAppPrjGen];
                    if (!toParents(p, res))
                        break;
                }
                p = p.Parent;
            }
        }
        public bool ContainsSettings(string guidAppPrjGen)
        {
            if (DicGenNodeSettings.ContainsKey(guidAppPrjGen))
            {
                var res = DicGenNodeSettings[guidAppPrjGen];
                return true;
            }
            return false;
        }
        public bool TrySetSettings(string guidAppPrjGen, IvPluginGeneratorNodeSettings setting)
        {
            bool res = false;
            DictionaryExt<string, IvPluginGeneratorNodeSettings> dic = null;
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
            {
                this.DicGenNodeSettings[guidAppPrjGen] = setting;
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
