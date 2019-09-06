using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.DbModel.MsSql
{
    public partial class MsSqlDesignGeneratorSettings : IvPluginGeneratorSettingsVM
    {
        partial void OnInit()
        {
        }
        public string NameUi { get; protected set; }
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

        public string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
