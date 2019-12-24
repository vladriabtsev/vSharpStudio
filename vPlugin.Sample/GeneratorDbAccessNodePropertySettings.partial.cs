using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodePropertySettings : IvPluginGeneratorSettingsVM
    {
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodePropertySettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
