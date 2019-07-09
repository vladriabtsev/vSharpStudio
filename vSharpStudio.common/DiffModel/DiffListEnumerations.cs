using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffListEnumerations : DiffLists<IEnumeration>
    {
        public DiffListEnumerations(IEnumerable<IEnumeration> oldest, IEnumerable<IEnumeration> prev, IEnumerable<IEnumeration> current)
            : base(oldest, prev, current)
        {
            foreach (var t in this.ListAll)
            {
                IEnumeration tt = (IEnumeration)t;
                if (tt.IsDeleted())
                    continue;
                if (tt.IsNew())
                    continue;
                IEnumeration oldest2 = (IEnumeration)dic_oldest[t.Guid];
                IEnumeration prev2 = (IEnumeration)dic_prev[t.Guid];
                IEnumeration current2 = (IEnumeration)dic_curr[t.Guid];
                DiffListEnumerationPairs diff_elements = new DiffListEnumerationPairs(
                    oldest2 == null ? null : oldest2.ListEnumerationPairsI,
                    prev2 == null ? null : prev2.ListEnumerationPairsI,
                    current2.ListEnumerationPairsI);
                t[DiffEnumHistoryAnnotation.DiffListEnumerationPairs.ToString()] = diff_elements;
                if (tt.IsDeprecated())
                    continue;
                if (prev2.DataTypeEnum != current2.DataTypeEnum)
                {
                    if (current2.DataTypeEnum == EnumEnumerationType.BYTE_VALUE ||
                        current2.DataTypeEnum == EnumEnumerationType.SHORT_VALUE && prev2.DataTypeEnum == EnumEnumerationType.INTEGER_VALUE ||
                        current2.DataTypeEnum == EnumEnumerationType.SHORT_VALUE && prev2.DataTypeEnum == EnumEnumerationType.STRING_VALUE ||
                        current2.DataTypeEnum == EnumEnumerationType.INTEGER_VALUE && prev2.DataTypeEnum == EnumEnumerationType.STRING_VALUE
                        )
                        t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;
                    var diff_data_type = new DiffEnumerationType(prev2, current2);
                    t[DiffEnumHistoryAnnotation.DiffEnumerationType.ToString()] = diff_data_type;
                }
                else if (current2.DataTypeEnum == EnumEnumerationType.STRING_VALUE)
                {
                    if (current2.DataTypeLength != prev2.DataTypeLength)
                    {
                        if (current2.DataTypeLength < prev2.DataTypeLength)
                            t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;
                        var diff_data_type = new DiffEnumerationType(prev2, current2);
                        t[DiffEnumHistoryAnnotation.DiffEnumerationType.ToString()] = diff_data_type;
                    }
                }

                DiffEnumeration diff2 = new DiffEnumeration(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffEnumeration.ToString()] = diff2;
            }
            this.ClearDics();
        }
    }
}
