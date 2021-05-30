using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class DbConnectionStringSettings : IvPluginGeneratorSettings
    {
        public DbConnectionStringSettings(string connectionString) : this()
        {
            this.StringSettings = connectionString;
        }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = DbConnectionStringSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        public string GenerateCode(IConfig cfg)
        {
            return this.StringSettings;
        }
        public IvPluginGenerator Generator { get; set; }
        public IAppProjectGenerator Parent { get; set; }
    }
}
