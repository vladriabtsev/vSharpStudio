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
        public ITreeConfigNode Parent { get; set; }
        public IvPluginGenerator CreateNew(IAppProjectGenerator appProjectGenerator) { return new GeneratorDbAccess(appProjectGenerator); }
        public GeneratorDbAccess(ITreeConfigNode parent) : this() { this.Parent = parent; }
        public GeneratorDbAccess() { }
        public string Guid => "7C2902AF-DF34-46FC-8911-A48EE7F9B2B0";
        public string GroupGeneratorsGuid => SamplePlugin.GroupAccessGuidStatic;
        public string Name => "DbAccess";
        public string NameUi => "Db Access Layer";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Db Access Layer";
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbAccess;
        public void Init() { }
        public IvPluginGeneratorSettings GetAppGenerationSettingsVmFromJson(IAppProjectGenerator parent, string settings)
        {
            var vm = new GeneratorDbAccessSettings();
            if (!string.IsNullOrWhiteSpace(settings))
            {
                proto_generator_db_access_settings proto = proto_generator_db_access_settings.Parser.WithDiscardUnknownFields(true).ParseJson(settings);
                vm = GeneratorDbAccessSettings.ConvertToVM(proto, vm);
            }
            vm.Parent = parent;
            return vm;
        }
        public IvPluginGeneratorNodeSettings GetGenerationNodeSettingsVmFromJson(ITreeConfigNode parent, string settings)
        {
            if (parent is IModel || parent is IConstant || parent is IForm ||
                parent is IGroupListEnumerations || parent is IEnumeration ||
                parent is IModel || parent is ICatalog || parent is IProperty ||
                parent is IDocument)
            {
                var vm = new GeneratorDbAccessNodeSettings(parent);
                if (!string.IsNullOrWhiteSpace(settings))
                {
                    proto_generator_db_access_node_settings proto = proto_generator_db_access_node_settings.Parser.WithDiscardUnknownFields(true).ParseJson(settings);
                    vm = GeneratorDbAccessNodeSettings.ConvertToVM(proto, vm);
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
            PreRenameData prd = null;
            var dic = annotatedConfig.DicNodes;
            foreach (var t in lstGuidsRenamedNodes)
            {
                var node = dic[t];
                var typeName = node.GetType().Name;
                switch (typeName)
                {
                    case "ICatalog":
                        var nc = node as ICatalog;

                        if (nc.IsRenamed(false))
                        {
                            cname = (nc.PrevStableVersion() as ICatalog).Name;
                            nname = "MyNamespace";
                            prd = new PreRenameData(nname, cname, nc.Name);
                            foreach (var tt in nc.GroupProperties.ListProperties)
                            {
                                if (tt.IsRenamed(false))
                                {
                                    var m = new RenamePropertyData((tt.PrevStableVersion() as IProperty).Name, tt.Name);
                                    prd.ListRenamedProperties.Add(m);
                                }
                            }
                        }
                        break;
                }
            }
            return res;
        }
        public IvPluginGroupSettings GetPluginGroupSolutionSettingsVmFromJson(IAppSolution parent, string settings)
        {
            var res = new PluginsGroupSolutionSettings();
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = proto_plugins_group_solution_settings.Parser.WithDiscardUnknownFields(true).ParseJson(settings);
                res = PluginsGroupSolutionSettings.ConvertToVM(proto, res);
            }
            res.Parent = parent;
            return res;
        }
        public IvPluginGroupSettings GetPluginGroupProjectSettingsVmFromJson(IAppProject parent, string settings)
        {
            var res = new PluginsGroupProjectSettings();
            if (!string.IsNullOrWhiteSpace(settings))
            {
                var proto = proto_plugins_group_project_settings.Parser.WithDiscardUnknownFields(true).ParseJson(settings);
                res = PluginsGroupProjectSettings.ConvertToVM(proto, res);
            }
            res.Parent = parent;
            return res;
        }
    }
}
