using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public class DiffListCatalogs : DiffLists<ICatalog>
    {
        public DiffListCatalogs(IEnumerable<ICatalog> oldest, IEnumerable<ICatalog> prev, IEnumerable<ICatalog> current)
            : base(oldest, prev, current)
        {
            foreach (var t in this.ListAll)
            {
                ICatalog tt = (ICatalog)t;
                if (tt.IsDeleted())
                    continue;
                if (tt.IsNew())
                    continue;
                if (tt.IsDeprecated())
                    continue;
                ICatalog oldest2 = (ICatalog)dic_oldest[t.Guid];
                ICatalog prev2 = (ICatalog)dic_prev[t.Guid];
                ICatalog current2 = (ICatalog)dic_curr[t.Guid];
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
                DiffCatalog diff = new DiffCatalog(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffCatalog.ToString()] = diff;
            }
            this.ClearDics();
        }
    }
}
