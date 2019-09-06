using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;
using System.ComponentModel;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppDbSettings : IParent
    {
        object lockobj = new object();
        private bool isNeedInit = true;
        private void Init()
        {
            isNeedInit = false;
            lock (lockobj)
            {
                if (AppDbSettings.ListPlugins == null)
                {
                    AppDbSettings.ListPlugins = new ObservableCollection<Plugin>();
                    IParent p = (IParent)this;
                    while (p.Parent != null)
                        p = p.Parent;
                    Config cfg = (Config)p;
                    if (cfg.DicPlugins.ContainsKey(vPluginLayerTypeEnum.DbConnection))
                    {
                        var lst = cfg.DicPlugins[vPluginLayerTypeEnum.DbConnection];
                        foreach (var t in lst)
                        {
                            if (AppDbSettings.ListPlugins.Contains(t.Plugin))
                                continue;
                            AppDbSettings.ListPlugins.Add(t.Plugin);
                        }
                    }
                }
            }
            if (this.PluginGuid.Length == 0)
            {
                this.ListPluginGens.Clear();
                this.ListDbConns.Clear();
            }
            else
            {
                Plugin plugin = null;
                foreach (var t in AppDbSettings.ListPlugins)
                {
                    if (this.PluginGuid == t.Guid)
                    {
                        plugin = t;
                        break;
                    }
                }
                if (plugin == null)
                    return;
                this.ListPluginGens.Clear();
                foreach (var t in plugin.ListGenerators)
                {
                    if (t.Generator.PluginGeneratorType == vPluginLayerTypeEnum.DbConnection)
                        this.ListPluginGens.Add(t);
                }
                if (this.PluginGenGuid.Length == 0)
                {
                    this.ListDbConns.Clear();
                }
                else
                {
                    PluginGenerator gen = null;
                    foreach (var t in this.ListPluginGens)
                    {
                        if (this.PluginGenGuid == t.Guid)
                        {
                            gen = t;
                            break;
                        }
                    }
                    if (gen == null)
                        return;
                    this.ListDbConns.Clear();
                    foreach (var t in gen.ListSettings)
                    {
                        this.ListDbConns.Add(t);
                    }
                }
            }
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
        public static ObservableCollection<Plugin> ListPlugins { get; private set; }
        [BrowsableAttribute(false)]
        public ObservableCollection<Plugin> ListPluginsProp
        {
            get
            {
                if (isNeedInit)
                    Init();
                return AppDbSettings.ListPlugins;
            }
        }
        partial void OnPluginGuidChanged()
        {
            Plugin plugin = null;
            foreach (var t in AppDbSettings.ListPlugins)
            {
                if (this.PluginGuid == t.Guid)
                {
                    plugin = t;
                    break;
                }
            }
            if (plugin == null)
                return;
            this.PluginName = plugin.Name;
            this.Version = plugin.Version;

            this.ListPluginGens.Clear();
            foreach (var t in plugin.ListGenerators)
            {
                if (t.Generator.PluginGeneratorType == vPluginLayerTypeEnum.DbConnection)
                    this.ListPluginGens.Add(t);
            }
            if (this.ListPluginGens.Count == 1)
            {
                this.PluginGenGuid = this.ListPluginGens[0].Guid;
                this.PluginGenName = this.ListPluginGens[0].Name;
            }
        }
        [BrowsableAttribute(false)]
        public ObservableCollection<PluginGenerator> ListPluginGens
        {
            get
            {
                if (isNeedInit)
                    Init();
                return _ListPluginGens;
            }
        }
        partial void OnPluginGenGuidChanged()
        {
            PluginGenerator gen = null;
            foreach (var t in this.ListPluginGens)
            {
                if (this.PluginGenGuid == t.Guid)
                {
                    gen = t;
                    break;
                }
            }
            if (gen == null)
                return;
            this.ListDbConns.Clear();
            foreach (var t in gen.ListSettings)
            {
                this.ListDbConns.Add(t);
            }
            if (this.ListDbConns.Count == 1)
            {
                this.ConnGuid = this.ListDbConns[0].Guid;
                this.ConnName = this.ListDbConns[0].Name;
            }
        }
        private ObservableCollection<PluginGenerator> _ListPluginGens = new ObservableCollection<PluginGenerator>();
        [BrowsableAttribute(false)]
        public ObservableCollection<PluginGeneratorSettings> ListDbConns
        {
            get
            {
                if (isNeedInit)
                    Init();
                return _ListDbConns;
            }
        }
        private ObservableCollection<PluginGeneratorSettings> _ListDbConns = new ObservableCollection<PluginGeneratorSettings>();
    }
}
