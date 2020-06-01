using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("NodeSettings:{Name,nq}")]
    public partial class PluginGeneratorNodeSettings
    {
        public IvPluginNodeSettings SettingsVm { get; set; }

        public PluginGeneratorNodeSettings(ITreeConfigNode parent, string appProjectGeneratorGuid, IvPluginNodeSettings t) : this(parent)
        {
            this.Name = t.Name;
            this.NodeSettingsVmGuid = t.Guid;
            this.AppProjectGeneratorGuid = appProjectGeneratorGuid;
            this.SettingsVm = t.GetAppGenerationNodeSettingsVm(this.Settings);
            //this.SettingsVm = gen?.Generator?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
        }
    }
}
