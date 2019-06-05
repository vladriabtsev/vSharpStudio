using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class MsSqlDesignGeneratorSettings : IvPluginGeneratorSettingsVM
    {
        public string Settings => throw new NotImplementedException();

        public ITreeConfigNode Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;

        public string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
