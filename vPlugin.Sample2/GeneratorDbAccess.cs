using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Proto.Plugin;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;

namespace vPlugin.Sample2
{
    public class GeneratorDbAccess : IvPluginGenerator
    {
        public ITreeConfigNode? Parent { get; set; }
        public IvPluginGenerator CreateNew(IAppProjectGenerator appProjectGenerator) { return new GeneratorDbAccess(appProjectGenerator); }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GeneratorDbAccess() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GeneratorDbAccess(ITreeConfigNode parent) : this() { this.Parent = parent; }
        public string Guid => "8876C5ED-8375-403B-A792-D05019BD89E0";
        //public string GroupGeneratorsGuid => SamplePlugin.GroupAccessGuidStatic;
        public string SolutionParametersGuid => string.Empty;
        public string ProjectParametersGuid => string.Empty;
        public string Name => "DbAccess2";
        public string NameUi => "Db Access2 Layer";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Db Access Layer";
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbAccess;
        public void Init() { }
        public IvPluginGeneratorSettings? GetAppGenerationSettingsVmFromJson(IAppProjectGenerator parent, string? settings)
        {
            var vm = new GeneratorDbAccessSettings2(parent);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                proto_generator_db_access_settings2 proto = proto_generator_db_access_settings2.Parser.WithDiscardUnknownFields(true).ParseJson(settings);
                vm = GeneratorDbAccessSettings2.ConvertToVM(proto, vm);
            }
            return vm;
        }
        public IvPluginGeneratorNodeSettings? GetGenerationNodeSettingsVmFromJson(ITreeConfigNode parent, string? settings)
        {
            return null;
        }
        public List<PreRenameData> GetListPreRename(IConfig annotatedConfig, List<string> lstGuidsRenamedNodes)
        {
            List<PreRenameData> res = new List<PreRenameData>();
            string nname, cname;
            PreRenameData prd = null!;
            var dic = annotatedConfig.DicNodes;
            foreach (var t in lstGuidsRenamedNodes)
            {
                var node = dic[t];
                if (node is ICatalog c)
                {
                    if (c.IsRenamed(false))
                    {
                        var pn = c.PrevStableVersion();
                        if (pn != null && pn is ICatalog cp)
                        {
                            cname = cp.Name;
                            nname = "MyNamespace";
                            prd = new PreRenameData(nname, cname, c.Name);
                            foreach (var tt in c.GroupProperties.ListProperties)
                            {
                                if (tt.IsRenamed(false))
                                {
                                    var prev = tt.PrevStableVersion();
                                    if (prev != null && prev is IProperty pp)
                                    {
                                        var m = new RenamePropertyData(pp.Name, tt.Name);
                                        prd.ListRenamedProperties.Add(m);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }
        public IvPluginGroupSettings? GetPluginGroupSolutionSettingsVmFromJson(IAppSolution parent, string? settings)
        {
            return null;
        }
        public IvPluginGroupSettings? GetPluginGroupProjectSettingsVmFromJson(IAppProject parent, string? settings)
        {
            return null;
        }
    }
}
