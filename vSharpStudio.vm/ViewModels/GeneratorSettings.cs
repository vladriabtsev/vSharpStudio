using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GeneratorSettings
    {
        public IvPluginNodeSettings SettingsVm { get; set; }
        [ReadOnly(true)]
        public new string Name
        {
            get
            {
                var cfg = (Config)this.GetConfig();
                return cfg.DicNodes[this.AppGeneratorGuid].Name;
            }
        }
        [ReadOnly(true)]
        public new string NameUi
        {
            get
            {
                var cfg = (Config)this.GetConfig();
                return (cfg.DicNodes[this.AppGeneratorGuid] as AppProjectGenerator).NameUi;
            }
        }
        [ReadOnly(true)]
        public string Description
        {
            get
            {
                var cfg = (Config)this.GetConfig();
                return (cfg.DicNodes[this.AppGeneratorGuid] as AppProjectGenerator).DescriptionGenerator;
            }
        }
    }
}
