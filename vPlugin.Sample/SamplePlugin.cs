using System;
using System.Collections.Generic;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
// https://medium.com/volosoft/asp-net-core-dependency-injection-58bc78c5d369
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
namespace vPlugin.Sample
{
    public class SamplePlugin : IvPlugin
    {
        public SamplePlugin()
        {
            this.Guid = "ED93228B-8D8E-456C-9688-37EEB1B2D835";
            this.Name = "Sample";
            this.NameUI = "Sample plugin";
            this.Description = "vSharpStudio plugin with several generators";
            this.Author = "Vladimir Riabtsev";
            this.Version = "0.1";
            this.Url = "https://www.vladnet.ca";
            this.Licence = string.Empty;
            this.ListGenerators = new List<IvPluginGenerator>();
            this.ListGenerators.Add(new GeneratorDbSchema());
            this.ListGenerators.Add(new GeneratorDbAccess());
        }
        public string Guid { get; protected set; }
        public string Name { get; protected set; }
        public string NameUI { get; protected set; }
        public string Description { get; protected set; }
        public string Author { get; protected set; }
        public string Version { get; protected set; }
        public string Url { get; protected set; }
        public string Licence { get; protected set; }
        public List<IvPluginGenerator> ListGenerators { get; protected set; }

        public string GroupGuid => throw new NotImplementedException();

        public string GroupVersion => throw new NotImplementedException();

        public string GroupInfo => throw new NotImplementedException();
    }
}
