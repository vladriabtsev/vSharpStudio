using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectSubBase<T, TValidator> : ConfigObjectBase<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectSubBase<T, TValidator>//, IComparable<T>, ISortingValue 
    {
        public ConfigObjectSubBase(ITreeConfigNode parent, TValidator validator)
            : base(parent, validator)
        {
        }
        protected void OnAddRemoveNode(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var cfg = this.GetConfig();
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    cfg.DicNodes[this.Guid] = this;
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    cfg.DicNodes.Remove(this.Guid);
                    break;
            }
        }

        #region Node App Generator Settings

        // App project generator Guid, 
        public void AddAllAppGenSettingsVmsToNewNode()
        {
            var ngs = (INodeGenSettings)this;
#if DEBUG
            if (ngs.ListGeneratorsSettings.Count > 0)
                throw new Exception();
#endif
            var cfg = (Config)this.GetConfig();
            if (cfg == null)
                return;
            _logger.LogTrace("Try Add Node Settings. {Count}".CallerInfo(), cfg.DicAppGenerators.Count);
            foreach (var dg in cfg.DicAppGenerators)
            {
                var gen = dg.Value;
                foreach (var t in gen.DicPathTypes)
                {
                    string ss = this.ModelPath;
                    var sp = t.Key.Split(".*.");
                    bool is_found = true;
                    int indx = 0;
                    foreach (var s in sp)
                    {
                        var sd = "." + s;
                        if (ss.Contains(s))
                        {
                            indx = ss.IndexOf(sd) + sd.Length;
                            ss = ss.Substring(indx);
                        }
                        else
                        {
                            is_found = false;
                            break;
                        }
                    }

                    if (is_found)
                    {
                        _logger.LogTrace("Adding Node Settings. {Path}".CallerInfo(), t.Key);
                        GeneratorSettings gs = new GeneratorSettings(this);
                        ngs.ListGeneratorsSettings.Add(gs);
                        gs.AppGeneratorGuid = dg.Key;
                        foreach (var tt in t.Value)
                        {
                            TypeSettings ts = new TypeSettings(this);
                            gs.ListTypeSettings.Add(ts);
                            ts.FullTypeName = tt;
                            ts.SettingsVm = gen.GetNodeGenerationSettingsVmFromJson(ts.FullTypeName, ts.Settings);
                        }
                        break;
                    }
                }
            }
        }
        public void RestoreNodeAppGenSettingsVm()
        {
            _logger.LogTrace();
            var ngs = (INodeGenSettings)this;
            var cfg = (Config)this.GetConfig();
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                if (cfg.DicAppGenerators.ContainsKey(t.AppGeneratorGuid))
                {
                    var gen = cfg.DicAppGenerators[t.AppGeneratorGuid];
                    foreach (var tt in t.ListTypeSettings)
                    {
                        tt.SettingsVm = gen.GetNodeGenerationSettingsVmFromJson(tt.FullTypeName, tt.Settings);
                    }
                }
            }
        }
        public void SaveNodeAppGenSettings()
        {
            _logger.LogTrace();
            var ngs = (INodeGenSettings)this;
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                foreach (var tt in t.ListTypeSettings)
                {
                    tt.Settings = tt.SettingsVm.SettingsAsJson;
                }
            }
        }
        public void RemoveNodeAppGenSettings(string appGenGuid)
        {
            _logger.LogTrace();
            var ngs = (INodeGenSettings)this;
            for (int i = ngs.ListGeneratorsSettings.Count - 1; i > 0; i--)
            {
                var t = ngs.ListGeneratorsSettings[i];
                if (t.AppGeneratorGuid == appGenGuid)
                {
                    ngs.ListGeneratorsSettings.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddNodeAppGenSettings(string appGenGuid)
        {
            _logger.LogTrace();
            //var cnfg = this.GetConfig();
            var ngs = (INodeGenSettings)this;
#if DEBUG
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                if (t.AppGeneratorGuid == appGenGuid)
                    throw new Exception();
            }
#endif
            var cfg = (Config)this.GetConfig();
            var gen = cfg.DicAppGenerators[appGenGuid];
            foreach (var t in gen.DicPathTypes)
            {
                if (this.FullName.Contains(t.Key))
                {
                    GeneratorSettings gs = new GeneratorSettings(this);
                    ngs.ListGeneratorsSettings.Add(gs);
                    gs.AppGeneratorGuid = appGenGuid;
                    foreach (var tt in t.Value)
                    {
                        TypeSettings ts = new TypeSettings(this);
                        gs.ListTypeSettings.Add(ts);
                        ts.FullTypeName = tt;
                        ts.SettingsVm = gen.GetNodeGenerationSettingsVmFromJson(ts.FullTypeName, ts.Settings);
                    }
                }
            }
        }

        #endregion Node App Generator Settings
    }
}
