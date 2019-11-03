using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public class DiffListConfigs : DiffLists<IConfig>
    {
        public DiffListConfigs(IEnumerable<IConfig> oldest, IEnumerable<IConfig> prev, IEnumerable<IConfig> current)
            : base(oldest, prev, current)
        {
            foreach (var t in this.ListAll)
            {
                IConfig tt = (IConfig)t;
                IConfig oldest2 = dic_oldest.ContainsKey(t.Guid) ? dic_oldest[t.Guid] : null;
                IConfig prev2 = dic_prev.ContainsKey(t.Guid) ? dic_prev[t.Guid] : null;
                IConfig current2 = dic_curr.ContainsKey(t.Guid) ? dic_curr[t.Guid] : null;
                DiffConfig diff_config = null;
                diff_config = new DiffConfig(oldest2, prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffConfig.ToString()] = diff_config;
            }
            this.ClearDics();
        }
        public IConfig Config { get { return this.ListAll[0]; } }
        public List<IConfig> ListSubConfigs
        {
            get
            {
                List<IConfig> lst = new List<IConfig>();
                for (int i = 1; i < this.ListAll.Count; i++)
                {
                    lst.Add(this.ListAll[i]);
                }
                return lst;
            }
        }
    }
}
