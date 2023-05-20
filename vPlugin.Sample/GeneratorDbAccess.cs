using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Proto.Plugin;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;

namespace vPlugin.Sample
{
    public class GeneratorDbAccess : IvPluginGenerator
    {
        public ITreeConfigNode? Parent { get; set; }
        public IvPluginGenerator CreateNew(IAppProjectGenerator appProjectGenerator) { return new GeneratorDbAccess(appProjectGenerator); }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GeneratorDbAccess() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GeneratorDbAccess(ITreeConfigNode parent) : this() { this.Parent = parent; }
        public string Guid => "7C2902AF-DF34-46FC-8911-A48EE7F9B2B0";
        //public string GroupGeneratorsGuid => SamplePlugin.GroupAccessGuidStatic;
        public string SolutionParametersGuid => PluginsGroupSolutionSettings.GuidStatic;
        public string ProjectParametersGuid => PluginsGroupProjectSettings.GuidStatic;
        public string Name => "DbAccess";
        public string NameUi => "Db Access Layer";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Db Access Layer";
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbAccess;
        public void Init() { }
        public IvPluginGeneratorSettings? GetAppGenerationSettingsVmFromJson(IAppProjectGenerator parent, string? settings)
        {
            var vm = new GeneratorDbAccessSettings(parent);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = CommonUtils.ParseJson<proto_generator_db_access_settings>(settings, true);
                vm = GeneratorDbAccessSettings.ConvertToVM(proto, vm);
            }
            return vm;
        }
        public IvPluginGeneratorNodeSettings? GetGenerationNodeSettingsVmFromJson(ITreeConfigNode parent, string? settings)
        {
            if (parent is IModel || parent is IConstant || parent is IGroupConstantGroups || parent is IForm ||
                parent is IGroupListEnumerations || parent is IEnumeration ||
                parent is ICatalog || parent is IProperty || parent is IDocument)
            {
                var vm = new GeneratorDbAccessNodeSettings(parent);
                if (!string.IsNullOrWhiteSpace(settings))
                {
                    var proto = CommonUtils.ParseJson<proto_generator_db_access_node_settings>(settings, true);
                    vm = GeneratorDbAccessNodeSettings.ConvertToVM(proto, vm);
                    vm.Parent = parent;
                }
                else
                {
                    // default settings on Model level
                    if (parent is IModel)
                    {
                        vm.IsCatalogFormParam1 = true; ;
                    }
                }
                return vm;
            }
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
        public IvPluginGroupSettings GetPluginGroupSolutionSettingsVmFromJson(IAppSolution parent, string? settings)
        {
            var res = new PluginsGroupSolutionSettings(parent);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = CommonUtils.ParseJson<proto_plugins_group_solution_settings>(settings, true);
                res = PluginsGroupSolutionSettings.ConvertToVM(proto, res);
            }
            return res;
        }
        public IvPluginGroupSettings GetPluginGroupProjectSettingsVmFromJson(IAppProject parent, string? settings)
        {
            var res = new PluginsGroupProjectSettings(parent);
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = CommonUtils.ParseJson<proto_plugins_group_project_settings>(settings, true);
                res = PluginsGroupProjectSettings.ConvertToVM(proto, res);
            }
            return res;
        }
    }
}
