using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public class DiffListDocuments : DiffLists<IDocument>
    {
        public DiffListDocuments(IGroupDocuments oldest, IGroupDocuments prev, IGroupDocuments current)
            : base(oldest.GroupListDocumentsI.ListDocumentsI, prev.GroupListDocumentsI.ListDocumentsI, current.GroupListDocumentsI.ListDocumentsI)
        {
            foreach (var t in this.ListAll)
            {
                IDocument tt = (IDocument)t;
                if (tt.IsDeleted())
                    continue;
                if (tt.IsNew())
                    continue;
                if (tt.IsDeprecated())
                    continue;
                IDocument oldest2 = (IDocument)dic_oldest[t.Guid];
                IDocument prev2 = (IDocument)dic_prev[t.Guid];
                IDocument current2 = (IDocument)dic_curr[t.Guid];
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

                DiffDocument diff = new DiffDocument(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffDocument.ToString()] = diff;
            }
            this.ClearDics();
        }
    }
}
