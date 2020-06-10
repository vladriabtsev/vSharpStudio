using System;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbSchemaSettings : IvPluginGeneratorSettings
    {
        public string SettingsAsJson => throw new NotImplementedException();
        public string GenerateCode(IConfig model)
        {
            throw new NotImplementedException();
        }
    }
}
