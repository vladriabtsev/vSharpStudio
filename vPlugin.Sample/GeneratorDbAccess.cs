using System;
using System.Collections.Generic;
using System.Text;
using Proto.Plugin;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;

namespace vPlugin.Sample
{
    public class GeneratorDbAccess : IvPluginGenerator
    {
        public GeneratorDbAccess()
        {
        }
        public string Guid => "7C2902AF-DF34-46FC-8911-A48EE7F9B2B0";
        public string Name => "DbAccess";
        public string NameUi => "Db Access Layer";
        public string DefaultSettingsName => throw new NotImplementedException();
        public string Description => "Description Db Access Layer";
        public ITreeConfigNode Parent { get; set; }
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbAccess;
        public IvPluginGeneratorSettings GetAppGenerationSettingsVmFromJson(string settings)
        {
            var vm = new GeneratorDbAccessSettings();
            if (!string.IsNullOrWhiteSpace(settings))
            {
                proto_generator_db_access_settings proto = proto_generator_db_access_settings.Parser.ParseJson(settings);
                vm = GeneratorDbAccessSettings.ConvertToVM(proto, vm);
            }
            return vm;
        }

        public List<IvPluginGeneratorNodeSettings> GetListNodeGenerationSettings()
        {
            List<IvPluginGeneratorNodeSettings> res = new List<IvPluginGeneratorNodeSettings>();
            res.Add(new GeneratorDbAccessNodePropertySettings());
            res.Add(new GeneratorDbAccessNodeCatalogFormSettings());
            res.Add(new GeneratorDbAccessNodeSettings());
            return res;
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
    }
}
