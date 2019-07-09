using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffListPropertiesTabs : DiffLists<IPropertiesTab>
    {
        public DiffListPropertiesTabs(IEnumerable<IPropertiesTab> oldest, IEnumerable<IPropertiesTab> prev, IEnumerable<IPropertiesTab> current)
            : base(oldest, prev, current)
        {
            foreach (var t in this.ListAll)
            {
                IPropertiesTab tt = (IPropertiesTab)t;
                if (tt.IsDeleted())
                    continue;
                if (tt.IsNew())
                    continue;
                if (tt.IsDeprecated())
                    continue;
                IPropertiesTab oldest2 = (IPropertiesTab)dic_oldest[t.Guid];
                IPropertiesTab prev2 = (IPropertiesTab)dic_prev[t.Guid];
                IPropertiesTab current2 = (IPropertiesTab)dic_curr[t.Guid];
                DiffListProperties diff_properties = new DiffListProperties(
                    oldest2 == null ? null : oldest2.GroupPropertiesI.ListPropertiesI,
                    prev2 == null ? null : prev2.GroupPropertiesI.ListPropertiesI,
                    current2.GroupPropertiesI.ListPropertiesI);
                t[DiffEnumHistoryAnnotation.DiffListProperties.ToString()] = diff_properties;
                DiffListPropertiesTabs diff_properties_tabs = new DiffListPropertiesTabs(
                    oldest2 == null ? null : oldest2.GroupPropertiesTabsI.ListPropertiesTabsI,
                    prev2 == null ? null : prev2.GroupPropertiesTabsI.ListPropertiesTabsI,
                    current2.GroupPropertiesTabsI.ListPropertiesTabsI);
                t[DiffEnumHistoryAnnotation.DiffListPropertiesTabs.ToString()] = diff_properties_tabs;

                DiffPropertiesTab diff = new DiffPropertiesTab(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffPropertiesTab.ToString()] = diff;
            }
            this.ClearDics();
        }
    }
}
