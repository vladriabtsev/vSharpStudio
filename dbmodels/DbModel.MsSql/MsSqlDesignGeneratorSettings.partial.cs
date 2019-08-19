using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class MsSqlDesignGeneratorSettings : IvPluginGeneratorSettingsVM
    {
        partial void OnInit()
        {
        }
        [BrowsableAttribute(false)]
        public string Settings
        {
            get
            {
                var proto = MsSqlDesignGeneratorSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public ITreeConfigNode Parent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
