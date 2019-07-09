using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffListProperties : DiffLists<IProperty>
    {
        public DiffListProperties(IEnumerable<IProperty> oldest, IEnumerable<IProperty> prev, IEnumerable<IProperty> current)
            : base(oldest, prev, current)
        {
            foreach (var t in this.ListAll)
            {
                IProperty tt = (IProperty)t;
                if (tt.IsDeleted())
                    continue;
                if (tt.IsNew())
                    continue;
                if (tt.IsDeprecated())
                    continue;
                IProperty prev2 = (IProperty)dic_prev[t.Guid];
                IProperty current2 = (IProperty)dic_curr[t.Guid];
                bool isCanLoose = false;
                bool isTypeChanged = false;
                if (current2.DataTypeI.DataTypeEnum != prev2.DataTypeI.DataTypeEnum)
                {
                    isCanLoose = true;
                }
                else if (current2.DataTypeI.DataTypeEnum == EnumDataType.STRING)
                {
                    if (current2.DataTypeI.Length < prev2.DataTypeI.Length)
                        isCanLoose = true;
                }
                else if (current2.DataTypeI.DataTypeEnum == EnumDataType.NUMERICAL)
                {
                    if (current2.DataTypeI.Length < prev2.DataTypeI.Length)
                        isCanLoose = true;
                    if (current2.DataTypeI.Accuracy < prev2.DataTypeI.Accuracy)
                        isCanLoose = true;
                }
                if (isCanLoose)
                    t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;

                if (current2.DataTypeI.DataTypeEnum != prev2.DataTypeI.DataTypeEnum ||
                    current2.DataTypeI.Length != prev2.DataTypeI.Length ||
                    current2.DataTypeI.Accuracy != prev2.DataTypeI.Accuracy
                    )
                {
                    isTypeChanged = true;
                }

                if (isTypeChanged)
                {
                    DiffDataType diff_data_type = new DiffDataType(prev2.DataTypeI, current2.DataTypeI);
                    t[DiffEnumHistoryAnnotation.DiffPropertyDataType.ToString()] = diff_data_type;
                }

                DiffProperty diff = new DiffProperty(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffProperty.ToString()] = diff;
            }
            this.ClearDics();
        }
    }
}
