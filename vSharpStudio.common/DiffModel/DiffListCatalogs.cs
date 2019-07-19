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
                ICatalog oldest2 = dic_oldest.ContainsKey(t.Guid) ? dic_oldest[t.Guid] : null;
                ICatalog prev2 = dic_prev.ContainsKey(t.Guid) ? dic_prev[t.Guid] : null;
                ICatalog current2 = dic_curr.ContainsKey(t.Guid) ? dic_curr[t.Guid] : null;
                DiffListProperties diff_properties = new DiffListProperties(
                    oldest2?.GroupPropertiesI.ListPropertiesI,
                    prev2?.GroupPropertiesI.ListPropertiesI,
                    current2?.GroupPropertiesI.ListPropertiesI);
                t[DiffEnumHistoryAnnotation.DiffListProperties.ToString()] = diff_properties;
                DiffListPropertiesTabs diff_properties_tabs = new DiffListPropertiesTabs(
                    oldest2?.GroupPropertiesTabsI.ListPropertiesTabsI,
                    prev2?.GroupPropertiesTabsI.ListPropertiesTabsI,
                    current2?.GroupPropertiesTabsI.ListPropertiesTabsI);
                t[DiffEnumHistoryAnnotation.DiffListPropertiesTabs.ToString()] = diff_properties_tabs;
                DiffCatalog diff = new DiffCatalog(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffCatalog.ToString()] = diff;
            }
            this.ClearDics();
        }
    }
}
