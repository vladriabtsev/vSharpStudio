using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    /// <summary>
    /// Diff model is using two previous release models and current model
    /// 1. Objects which exist only in current model are new objects
    /// 2. Objects which exist in previous model. but not in current model are deprecated objects
    /// 3. Objects which exist in oldest model. but not in previous and current model are objects for deletion
    /// Same aproach for properties of objects
    /// </summary>
    public partial class DiffModel
    {
        public DiffModel(IConfig oldest_config, IConfig prev_config, IConfig current_config)
        {
            var oldests = GetListConfigs(oldest_config);
            var prevs = GetListConfigs(prev_config);
            var currents = GetListConfigs(current_config);
            this.Configs = new DiffListConfigs(oldests, prevs, currents);
        }

        private static List<IConfig> GetListConfigs(IConfig cfg)
        {
            var lst = new List<IConfig>();
            if (cfg == null)
                return lst;
            var dic = new Dictionary<string, IConfig>();
            dic[cfg.Guid] = cfg;
            GetSubConfigs(cfg, dic);
            foreach(var t in dic)
            {
                lst.Add(t.Value);
            }
            dic.Clear();
            return lst;
        }
        private static void GetSubConfigs(IConfig cfg, Dictionary<string, IConfig> dic)
        {
            foreach (var t in cfg.GroupConfigsI.ListBaseConfigsI)
            {
                dic[t.Config.Guid] = t.Config;
                GetSubConfigs(t.Config, dic);
            }
        }

        public DiffListConfigs Configs;
    }
}
