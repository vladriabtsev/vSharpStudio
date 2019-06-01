using System;
using System.Collections.Generic;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
// https://medium.com/volosoft/asp-net-core-dependency-injection-58bc78c5d369
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
namespace vPlugin.DbModel.MsSql
{
    //[Export(typeof(IDbMigrator))]
    //[ExportMetadata("Name", "MsSQL")]
    public class MsSqlPlugin : IvPlugin
    {
        public MsSqlPlugin()
        {
            this.Guid = new Guid("C94175E4-E8F4-4A84-871B-6994199A2076");
            this.Name = "MsSql";
            this.Description = "vSharpStudio plugin. Database design for MS SQL support";
            this.Author = "Vladimir Riabtsev";
            this.Version = "0.1";
            this.Url = "https://www.vladnet.ca";
            this.Licence = "";
            this.ListGenerators = new List<IvPluginCodeGenerator>();
            this.ListGenerators.Add(new ConnectionGenerator());
            this.ListGenerators.Add(new MsSqlDesignGenerator());
        }
        public Guid Guid { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Author { get; protected set; }
        public string Version { get; protected set; }
        public string Url { get; protected set; }
        public string Licence { get; protected set; }
        public List<IvPluginCodeGenerator> ListGenerators { get; protected set; }
    }
}
