using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public class DiffListDocuments : DiffLists<IDocument>
    {
        public DiffListProperties DiffSharedProps { get; private set; }
        public DiffListDocuments(IGroupDocuments oldest, IGroupDocuments prev, IGroupDocuments current)
            : base(oldest?.IGroupListDocuments.IListDocuments, prev?.IGroupListDocuments.IListDocuments, current?.IGroupListDocuments.IListDocuments)
        {
            DiffSharedProps = new DiffListProperties(
                oldest?.IGroupSharedProperties.IListProperties,
                prev?.IGroupSharedProperties.IListProperties,
                current?.IGroupSharedProperties.IListProperties
                );
            foreach (var t in this.ListAll)
            {
                IDocument tt = (IDocument)t;
                IDocument oldest2 = dic_oldest.ContainsKey(t.Guid) ? dic_oldest[t.Guid] : null;
                IDocument prev2 = dic_prev.ContainsKey(t.Guid) ? dic_prev[t.Guid] : null;
                IDocument current2 = dic_curr.ContainsKey(t.Guid) ? dic_curr[t.Guid] : null;
                DiffListProperties diff_properties = new DiffListProperties(
                    oldest2?.IGroupProperties.IListProperties,
                    prev2?.IGroupProperties.IListProperties,
                    current2?.IGroupProperties.IListProperties);
                t[DiffEnumHistoryAnnotation.DiffListProperties.ToString()] = diff_properties;
                DiffListPropertiesTabs diff_properties_tabs = new DiffListPropertiesTabs(
                    oldest2?.IGroupPropertiesTabs.IListPropertiesTabs,
                    prev2?.IGroupPropertiesTabs.IListPropertiesTabs,
                    current2?.IGroupPropertiesTabs.IListPropertiesTabs);
                t[DiffEnumHistoryAnnotation.DiffListPropertiesTabs.ToString()] = diff_properties_tabs;

                DiffDocument diff = new DiffDocument(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffDocument.ToString()] = diff;
            }
            this.ClearDics();
        }
    }
}
