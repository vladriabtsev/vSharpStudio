﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodeSettings : IvPluginNodeSettings
    {
        [ReadOnly(true)]
        public string Guid { get { return "2511D2D7-020E-4481-BB38-08D4B1ECF083"; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodeSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        public IvPluginNodeSettings GetAppGenerationNodeSettingsVm(string settings)
        {
            if (string.IsNullOrWhiteSpace(settings))
                return new GeneratorDbAccessNodeSettings();
            proto_generator_db_access_node_settings proto = proto_generator_db_access_node_settings.Parser.ParseJson(settings);
            return GeneratorDbAccessNodeSettings.ConvertToVM(proto, new GeneratorDbAccessNodeSettings());
        }
        public static string SearchPath = "";
        [ReadOnly(true)]
        public string SearchPathInModel { get { return SearchPath; } }
    }
}
