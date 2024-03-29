﻿using System;
using System.Collections.Generic;
using Proto.Plugin;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
// https://medium.com/volosoft/asp-net-core-dependency-injection-58bc78c5d369
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
namespace vPlugin.Sample2
{
    public class SamplePlugin : IvPlugin
    {
        public const string GuidStatic = "D535B939-6A33-4B96-B64C-F8FB78FD9F94";
        public const string GroupAccessGuidStatic = "007CFD6E-E879-42EE-8F98-8AE8F9ED65D8";

        public SamplePlugin()
        {
            this.Name = "Sample2";
            this.NameUI = "Sample2 plugin";
            this.Description = "vSharpStudio plugin with one generators";
            this.Author = "Vladimir Riabtsev";
            this.Version = "0.1";
            this.Url = "https://www.vladnet.ca";
            this.Licence = string.Empty;
            this.ListGenerators = new List<IvPluginGenerator>();
            this.ListGenerators.Add(new GeneratorDbAccess());
        }
        public string Guid { get { return GuidStatic; } }
        public string Name { get; protected set; }
        public string NameUI { get; protected set; }
        public string Description { get; protected set; }
        public string Author { get; protected set; }
        public string Version { get; protected set; }
        public string Url { get; protected set; }
        public string Licence { get; protected set; }

        public List<IvPluginGenerator> ListGenerators { get; }
    }
}
