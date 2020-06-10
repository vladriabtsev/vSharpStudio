using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginGeneratorMainSettings
    {
        public IvPluginGeneratorSettings SettingsVm { get; set; }
        //[ReadOnly(true)]
        //public new string Name
        //{
        //    get
        //    {
        //        var apg = (AppProjectGenerator)this.Parent;
        //        return apg.Name;
        //    }
        //}
        //[ReadOnly(true)]
        //public new string NameUi
        //{
        //    get
        //    {
        //        var apg = (AppProjectGenerator)this.Parent;
        //        return apg.NameUi;
        //    }
        //}
        //[ReadOnly(true)]
        //public string Description
        //{
        //    get
        //    {
        //        var apg = (AppProjectGenerator)this.Parent;
        //        return apg.DescriptionGenerator;
        //    }
        //}
    }
}
