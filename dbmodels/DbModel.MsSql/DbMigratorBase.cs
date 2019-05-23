using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;
using vSharpStudio.common;

namespace vPlugin.DbModel
{
    public class DbMigratorBase
    {
        public vPluginTypeEnum PluginType { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Author { get; protected set; }
        public string Version { get; protected set; }
        public string Url { get; protected set; }
        public string Licence { get; protected set; }
    }
}
