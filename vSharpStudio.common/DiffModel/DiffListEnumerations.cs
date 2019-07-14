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
                if (tt.IsDeprecated())
                    continue;
                IEnumeration oldest2 = dic_oldest.ContainsKey(t.Guid) ? dic_oldest[t.Guid] : null;
                IEnumeration prev2 = dic_prev.ContainsKey(t.Guid) ? dic_prev[t.Guid] : null;
                IEnumeration current2 = dic_curr.ContainsKey(t.Guid) ? dic_curr[t.Guid] : null;
                DiffListEnumerationPairs diff_elements = new DiffListEnumerationPairs(
                    oldest2?.ListEnumerationPairsI,
                    prev2?.ListEnumerationPairsI,
                    current2.ListEnumerationPairsI);
                t[DiffEnumHistoryAnnotation.DiffListEnumerationPairs.ToString()] = diff_elements;
                if (prev2 != null)
                {
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
            }
            this.ClearDics();
        }
    }
}
