﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessSettings : IvPluginGeneratorSettings
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
        public string GenerateCode(IConfig model)
        {
            if (this.IsAccessParam1)
            {
                // do something 
            }
            var visitor = new MyModelVisitor(this);
            visitor.Run(model.Model);
            return visitor.Result;
        }
    }
    public class MyModelVisitor : ModelVisitorBase
    {

        GeneratorDbAccessSettings pluginSettings;
        public MyModelVisitor(GeneratorDbAccessSettings pluginSettings)
        {
            this.pluginSettings = pluginSettings;
        }
        StringBuilder sb = new StringBuilder();
        protected override void BeginVisit(ICatalog ct)
        {
            base.BeginVisit(ct);

            //if (ct.NodeAllSettings().IsParam1)
            //    sb.AppendLine("Node IsParam1==true");
            //if (this.pluginSettings.IsAccessParam1)
            //    sb.AppendLine("IsAccessParam1==true");
            //sb.AppendLine("generated code");
        }
        public string Result { get { return sb.ToString(); } }
    }
}
