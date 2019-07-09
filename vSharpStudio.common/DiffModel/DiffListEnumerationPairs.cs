using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffListEnumerationPairs : DiffLists<IEnumerationPair>
    {
        public DiffListEnumerationPairs(IEnumerable<IEnumerationPair> oldest, IEnumerable<IEnumerationPair> prev, IEnumerable<IEnumerationPair> current)
            : base(oldest, prev, current)
        {
            foreach (var t in this.ListAll)
            {
                IEnumerationPair tt = (IEnumerationPair)t;
                if (tt.IsDeleted())
                    continue;
                if (tt.IsNew())
                    continue;
                IEnumerationPair prev2 = dic_prev[t.Guid];
                IEnumerationPair current2 = dic_curr[t.Guid];
                DiffEnumerationPair diff = new DiffEnumerationPair(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffEnumerationPair.ToString()] = diff;

                DiffEnumerationPair diff2 = new DiffEnumerationPair(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffEnumerationPair.ToString()] = diff2;
            }
            this.ClearDics();
        }
    }
}
