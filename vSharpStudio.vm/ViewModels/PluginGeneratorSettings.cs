using System.ComponentModel;
using System.Diagnostics;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class PluginGeneratorSettings : IParent
    {
        public IvPluginGeneratorSettings? SettingsVm { get; set; }
        [Browsable(false)]
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
