using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginGeneratorNodeSettings
    {
        public IvPluginNodeSettings SettingsVm { get; set; }

        //public PluginGeneratorNodeSettings(ITreeConfigNode parent, IvPluginNodeSettings t) : this(parent)
        //{
        //    this.Name=p.n
        //        this.NodeSettingsVmGuid
        //        this.AppProjectGeneratorGuid
        //        this.SettingsVm= t.GetAppGenerationNodeSettingsVm(this.Settings);
        //}
    }
}
