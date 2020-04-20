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
        public vPluginLayerTypeEnum PluginGeneratorType => vPluginLayerTypeEnum.DbAccess;
        private GeneratorDbAccessSettings gen_settings = null;
        public IvPluginGeneratorSettingsVM GetAppGenerationSettingsVmFromJson(string settings)
        {
            if (gen_settings != null)
                return gen_settings;
            if (string.IsNullOrWhiteSpace(settings))
                gen_settings = new GeneratorDbAccessSettings();
            else
            {
                proto_generator_db_access_settings proto = proto_generator_db_access_settings.Parser.ParseJson(settings);
                gen_settings = GeneratorDbAccessSettings.ConvertToVM(proto, new GeneratorDbAccessSettings());
            }
            return gen_settings;
        }

        public List<IvPluginNodeSettings> GetListNodeGenerationSettings()
        {
            List<IvPluginNodeSettings> res = new List<IvPluginNodeSettings>();
            res.Add(new GeneratorDbAccessNodePropertySettings());
            res.Add(new GeneratorDbAccessNodeCatalogFormSettings());
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

                        if (nc.IsRenamed())
                        {
                            cname = (nc.PrevVersion() as ICatalog).Name;
                            nname = "MyNamespace";
                            prd = new PreRenameData(nname, cname, nc.Name);
                            foreach (var tt in nc.GroupProperties.ListProperties)
                            {
                                if (tt.IsRenamed())
                                {
                                    var m = new RenamePropertyData((tt.PrevVersion() as IProperty).Name, tt.Name);
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
