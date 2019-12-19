using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessSettings : IvPluginGeneratorSettingsVM
    {
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public ITreeConfigNode Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
