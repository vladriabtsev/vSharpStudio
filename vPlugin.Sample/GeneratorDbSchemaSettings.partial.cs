using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbSchemaSettings : IvPluginGeneratorSettings
    {
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbSchemaSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        public string GenerateCode(IConfig model)
        {
            string s = "";
            //if (this.IsAccessParam1)
            //{
            //    s = "kuku";
            //}
            //var visitor = new MyModelVisitor(this);
            //visitor.Run(model.Model);
            //return visitor.Result;
            return s;
        }
        public IvPluginGenerator Generator { get; set; }
        public IAppProjectGenerator Parent { get; set; }
    }
}
