using System;
using System.Collections.Generic;
using Proto.Plugin;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/ef/core/providers/
// https://medium.com/volosoft/asp-net-core-dependency-injection-58bc78c5d369
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
namespace vPlugin.Sample
{
    public class SamplePlugin : IvPlugin
    {
        public const string GuidStatic = "ED93228B-8D8E-456C-9688-37EEB1B2D835";
        public SamplePlugin()
        {
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
        public string Guid { get { return GuidStatic; } }
        public string Name { get; protected set; }
        public string NameUI { get; protected set; }
        public string Description { get; protected set; }
        public string Author { get; protected set; }
        public string Version { get; protected set; }
        public string Url { get; protected set; }
        public string Licence { get; protected set; }
        public IvPluginGroupSolutionSettings GetPluginGroupSolutionSettingsVmFromJson(string settings)
        {
            var res = new PluginsGroupSettings();
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = proto_plugins_group_settings.Parser.ParseJson(settings);
                res = PluginsGroupSettings.ConvertToVM(proto, res);
            }
            return res;
        }

        public List<ValidationPluginMessage> ValidateOnSelection(IAppSolution sln)
        {
            return new List<ValidationPluginMessage>();
        }

        public List<IvPluginGenerator> ListGenerators { get; }
    }
}
