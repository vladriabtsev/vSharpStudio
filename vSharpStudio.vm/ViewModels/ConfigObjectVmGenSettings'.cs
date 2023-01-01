using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectVmGenSettings<T, TValidator> : ConfigObjectVmBase<T, TValidator>, IGetNodeSetting
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectVmGenSettings<T, TValidator>, ITreeConfigNodeSortable//, IComparable<T>, ISortingValue 
    {
        public ConfigObjectVmGenSettings(ITreeConfigNode? parent, TValidator validator)
            : base(parent, validator)
        {
            this.Children = new ConfigNodesCollection<ITreeConfigNodeSortable>((ITreeConfigNode)this);
            //this.HidePropertiesForXceedPropertyGrid();
            this.DicVmExclProps = new Dictionary<string, Dictionary<string, string?>?>();
        }
        [BrowsableAttribute(false)]
        public Dictionary<string, Dictionary<string, string?>?> DicVmExclProps { get; private set; }
        public void AddAllAppGenSettingsVmsToNode()
        {
            if (!(this is INodeGenSettings))
                return;
            if (this.Parent == null)
                return;
            var cfg = (Config)this.Cfg;
            //if (cfg == null || cfg.GroupAppSolutions == null)
            //    return;
            _logger?.LogTrace("Try Add Node Settings. {Count}".CallerInfo(), cfg.DicActiveAppProjectGenerators.Count);
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

        public void AddNodeAppGenSettings(string appProjectGeneratorGuid)
        {
            _logger?.Trace();
            Debug.Assert(!this._DicGenNodeSettings.ContainsKey(appProjectGeneratorGuid));
            var ngs = (INodeGenSettings)this;
            var appgen = (AppProjectGenerator)this.Cfg.DicNodes[appProjectGeneratorGuid];
            var gen = this.Cfg.DicActiveAppProjectGenerators[appProjectGeneratorGuid];
            PluginGeneratorNodeSettings? gs = null;
            foreach (var ts in ngs.ListNodeGeneratorsSettings)
            {
                if (ts.Guid == appProjectGeneratorGuid)
                {
                    gs = ts;
                }
            }
            if (gs == null)
            {
                var t = gen.GetGenerationNodeSettingsVmFromJson((ITreeConfigNode)this, null);
                if (t != null)
                {
                    this.DicVmExclProps[t.GetType().Name] = t.DicNodeExcludedProperties;
                    this._DicGenNodeSettings[appProjectGeneratorGuid] = t;
                    gs = new PluginGeneratorNodeSettings((ITreeConfigNode)this);
                    gs.Name = appgen.Name;
                    gs.AppProjectGeneratorGuid = appgen.Guid;
                    gs.SettingsVm = t;
                    //_logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appProjectGenerator.Name);
                    ngs.ListNodeGeneratorsSettings.Add(gs);
                }
            }
            else
            {
                var t = gen.GetGenerationNodeSettingsVmFromJson((ITreeConfigNode)this, gs.Settings);
                if (t != null)
                {
                    this.DicVmExclProps[t.GetType().Name] = t.DicNodeExcludedProperties;
                    gs.SettingsVm = t;
                    this._DicGenNodeSettings[appProjectGeneratorGuid] = t;
                }
            }
        }
        protected Severity? CheckMaxSeverity(ValidationResult vr, Severity? curSeverity = null)
        {
            if (curSeverity != Severity.Error)
            {
                foreach (var tt in vr.Errors)
                {
                    if (tt.Severity == Severity.Error)
                    {
                        return Severity.Error;
                    }
                    else if (tt.Severity == Severity.Warning)
                    {
                        curSeverity = Severity.Warning;
                    }
                    else if (tt.Severity == Severity.Info)
                    {
                        if (curSeverity == null)
                            curSeverity = Severity.Info;
                    }
                }
            }
            return curSeverity;
        }
        protected override void OnValidated(ValidationResult res)
        {
            _logger?.Trace();
            if (this is INodeGenSettings)
            {
                foreach (var t in this.DicGenNodeSettings)
                {
                    if (t.Value == null)
                        continue;
                    var vr = t.Value.ValidateSettings();
                    this.CheckMaxSeverity(vr);
                    res.Errors.AddRange(vr.Errors);
                }
            }
        }
        public void RestoreNodeAppGenSettingsVm()
        {
            _logger?.Trace();
            var ngs = (INodeGenSettings)this;
            foreach (var tt in ngs.ListNodeGeneratorsSettings)
            {
                if (!this.Cfg.DicActiveAppProjectGenerators.ContainsKey(tt.AppProjectGeneratorGuid))
                    continue;
                var gen = this.Cfg.DicActiveAppProjectGenerators[tt.AppProjectGeneratorGuid];
                tt.SettingsVm = gen.GetGenerationNodeSettingsVmFromJson((ITreeConfigNode)this, tt.Settings);
                if (tt.SettingsVm != null)
                    this.DicVmExclProps[tt.SettingsVm.GetType().Name] = tt.SettingsVm.DicNodeExcludedProperties;
                this._DicGenNodeSettings[tt.AppProjectGeneratorGuid] = tt.SettingsVm;
            }
            //foreach (var t in cfg.DicActiveAppProjectGenerators)
            //{
            //    //this.AddNodeAppGenSettings(t.Key);
            //}
        }
        public override List<IEditableObjectExt> GetEditableNodeSettings()
        {
            var lst = new List<IEditableObjectExt>();
            foreach (var t in this._DicGenNodeSettings.Values)
            {
                Debug.Assert(t is IEditableObjectExt);
                lst.Add((IEditableObjectExt)t);
            }
            return lst;
        }
        public void SaveNodeAppGenSettings()
        {
            //if (this is ConfigModel)
            //    return;
            _logger?.Trace();
            var ngs = (INodeGenSettings)this;
            foreach (var t in ngs.ListNodeGeneratorsSettings.ToList())
            {
                if (t.SettingsVm is IEditableObjectExt tt && tt.IsChanged)
                {
                    t.Settings = t.SettingsVm.SettingsAsJson;
                    if (t.Settings == t.SettingsVm.SettingsAsJsonDefault)
                        t.Settings = string.Empty;
                }
                //    ngs.ListNodeGeneratorsSettings.Remove(t);
            }
        }
        public void RemoveNodeAppGenSettings(string appGenGuid)
        {
            _logger?.Trace();
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
            this._DicGenNodeSettings.Remove(appGenGuid);
        }

        #endregion Node App Generator Settings

        [BrowsableAttribute(false)]
        // AppProjectGenerator guid, Settings quid, IvPluginGeneratorNodeSettings
        public IReadOnlyDictionary<string, IvPluginGeneratorNodeSettings?> DicGenNodeSettings { get { return _DicGenNodeSettings; } }
        internal DictionaryExt<string, IvPluginGeneratorNodeSettings?> _DicGenNodeSettings =
            new DictionaryExt<string, IvPluginGeneratorNodeSettings?>(20, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
        /// <summary>
        /// Check if this node is included for generation for selected generator
        /// </summary>
        /// <param name="guidAppPrjGen">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public bool IsIncluded(string guidAppPrjGen) //, bool isFromPrevStable = false)
        {
            //if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
            //    throw new Exception();

            var p = (ITreeConfigNode)this;
            while (p != null)
            {
                var ngs = p as INodeGenSettings;
                if (p is Model m)
                {
                    if (m.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var settings = (IvPluginGeneratorNodeIncludable?)(m.DicGenNodeSettings[guidAppPrjGen]);
                        if (settings != null)
                        {
                            if (!settings.IsIncluded.HasValue || settings.IsIncluded.Value)
                            {
                                return true;
                            }
                        }
                        //if (!isFromPrevStable)
                        //    return this.IsIncludedInStable(guidAppPrjGen);
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
                        var nodeSettings = (IvPluginGeneratorNodeIncludable?)ngs.DicGenNodeSettings[guidAppPrjGen];
                        if (nodeSettings != null && nodeSettings.IsIncluded.HasValue)
                        {
                            return nodeSettings.IsIncluded.Value;
                        }
                        //if (!isFromPrevStable)
                        //    return this.IsIncludedInStable(guidAppPrjGen);
                        //return false;
                    }
                }
                //else
                //    throw new Exception();
                //if (p.Parent == null)
                //    return true;
                p = p.Parent;
            }
            return true;
        }
        //private bool IsIncludedInStable(string guidAppPrjGen)
        //{
        //    var cfg = this.GetConfig();
        //    var prev = cfg.PrevStableConfig;
        //    if (prev != null && prev.DicNodes.ContainsKey(this._Guid))
        //    {
        //        var prevNode = (IGetNodeSetting)prev.DicNodes[this._Guid];
        //        return prevNode.IsIncluded(guidAppPrjGen, true);
        //    }
        //    return false;
        //}
        public bool GetBoolSetting(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, bool?> func, bool isFromPrevStable = false)
        {
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();

            var p = (ITreeConfigNode)this;
            while (p != null)
            {
                var ngs = p as INodeGenSettings;
                if (p is Model m)
                {
                    if (m.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var setting = m.DicGenNodeSettings[guidAppPrjGen];
                        if (setting != null)
                        {
                            var isIncluded = func(setting);
                            if (!isIncluded.HasValue || isIncluded.Value)
                            {
                                return true;
                            }
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
                        var setting = ngs.DicGenNodeSettings[guidAppPrjGen];
                        if (setting != null)
                        {
                            var isTrue = func(setting);
                            if (isTrue.HasValue)
                            {
                                if (isTrue.Value)
                                {
                                    return true;
                                }
                                if (!isFromPrevStable)
                                    return this.GetBoolSettingInStable(guidAppPrjGen, func);
                                return false;
                            }
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
            var prev = this.Cfg.PrevStableConfig;
            if (prev != null && prev.DicNodes.ContainsKey(this._Guid))
            {
                var prevNode = (IGetNodeSetting)prev.DicNodes[this._Guid];
                return prevNode.GetBoolSetting(guidAppPrjGen, func, true);
            }
            return false;
        }
        public string GetStringSetting(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, string> func, bool isFromPrevStable = false)
        {
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();

            var p = (ITreeConfigNode)this;
            while (p != null)
            {
                var ngs = p as INodeGenSettings;
                if (p is Model m)
                {
                    if (m.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                    {
                        var nodeSettings = m.DicGenNodeSettings[guidAppPrjGen];
                        if (nodeSettings != null)
                        {
                            var settingString = func(nodeSettings);
                            if (!string.IsNullOrWhiteSpace(settingString))
                            {
                                return settingString;
                            }
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
                        var settings = ngs.DicGenNodeSettings[guidAppPrjGen];
                        if (settings != null)
                        {
                            var resString = func(settings);
                            if (!string.IsNullOrWhiteSpace(resString))
                            {
                                return resString;
                            }
                        }
                        if (!isFromPrevStable)
                            return this.GetStringSettingInStable(guidAppPrjGen, func);
                    }
                    return "";
                }
                else
                    throw new Exception();
            }
            return "";
        }
        private string GetStringSettingInStable(string guidAppPrjGen, Func<IvPluginGeneratorNodeSettings, string> func)
        {
            var prev = this.Cfg.PrevStableConfig;
            if (prev != null && prev.DicNodes.ContainsKey(this._Guid))
            {
                var prevNode = (IGetNodeSetting)prev.DicNodes[this._Guid];
                return prevNode.GetStringSetting(guidAppPrjGen, func, true);
            }
            return "";
        }
        /// <summary>
        /// Getting VM of generator settings for node
        /// </summary>
        /// <param name="guidAppPrjGen">Guid of VM of generator node settings</param>
        /// <returns></returns>
        public IvPluginGeneratorNodeSettings? GetSettings(string guidAppPrjGen)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                return null;
            var res = DicGenNodeSettings[guidAppPrjGen];
            return res;
        }
        public T2? GetSettings<T2>(string guidAppPrjGen, Func<ITreeConfigNode, T2, bool> found)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            var p = (ITreeConfigNode)this;
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                {
                    var res = (T2?)ngs.DicGenNodeSettings[guidAppPrjGen];
                    if (res != null && found(p, res))
                        return res;
                }
                p = p.Parent;
            }
            return default(T2);
        }
        public TValue? GetSettingsValue<T2, TValue>(string guidAppPrjGen, Action<ITreeConfigNode, T2, Result<TValue>> found)
        {
            if (!DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                throw new Exception();
            var p = (ITreeConfigNode)this;
            Result<TValue> res = new Result<TValue>();
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                {
                    var st = (T2?)ngs.DicGenNodeSettings[guidAppPrjGen];
                    if (st != null)
                    {
                        found(p, st, res);
                        if (!res.IsContinue)
                            return res.Value;
                    }
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
            var p = (ITreeConfigNode)this;
            while (p != null)
            {
                var ngs = p as INodeGenDicSettings;
                if (ngs != null && ngs.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
                {
                    var res = ngs.DicGenNodeSettings[guidAppPrjGen];
                    if (res != null && !toParents(p, res))
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
            if (!this.DicGenNodeSettings.ContainsKey(guidAppPrjGen))
            {
                this._DicGenNodeSettings[guidAppPrjGen] = setting;
                res = true;
            }
            return res;
        }

        [Category("")]
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Node Settings")]
        [Description("Node settings by solutions, projects, generators")]
        // Dynamic class object of for node generators representation:
        // Expandable node generator name
        //    Expandable generator parameters object
        public object? DynamicNodesSettings
        {
            get
            {
                var nd = new NodeSettings();
                var res = nd.Run((ITreeConfigNode)this, true);
                return res;
            }
        }
    }
}
