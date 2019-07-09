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
                if (tt.IsDeleted())
                    continue;
                if (tt.IsNew())
                    continue;
                if (tt.IsDeprecated())
                    continue;
                IConfig oldest2 = (IConfig)dic_oldest[t.Guid];
                IConfig prev2 = (IConfig)dic_prev[t.Guid];
                IConfig current2 = (IConfig)dic_curr[t.Guid];
                DiffConfig diff_config = new DiffConfig(
                    oldest2 == null ? null : oldest2,
                    prev2 == null ? null : prev2,
                    current2);
                t[DiffEnumHistoryAnnotation.DiffConfig.ToString()] = diff_config;
            }
            this.ClearDics();
        }
    }
}
