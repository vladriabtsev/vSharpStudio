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
                IProperty prev2 = dic_prev.ContainsKey(t.Guid) ? dic_prev[t.Guid] : null;
                IProperty current2 = (IProperty)dic_curr[t.Guid];
                bool isCanLoose = false;
                bool isTypeChanged = false;
                if (prev2 != null && current2!=null)
                {
                    if (current2.IDataType.DataTypeEnum != prev2.IDataType.DataTypeEnum)
                    {
                        isCanLoose = true;
                    }
                    else if (current2.IDataType.DataTypeEnum == EnumDataType.STRING)
                    {
                        if (current2.IDataType.Length < prev2.IDataType.Length)
                            isCanLoose = true;
                    }
                    else if (current2.IDataType.DataTypeEnum == EnumDataType.NUMERICAL)
                    {
                        if (current2.IDataType.Length < prev2.IDataType.Length)
                            isCanLoose = true;
                        if (current2.IDataType.Accuracy < prev2.IDataType.Accuracy)
                            isCanLoose = true;
                    }
                    if (isCanLoose)
                        t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;

                    if (current2.IDataType.DataTypeEnum != prev2.IDataType.DataTypeEnum ||
                        current2.IDataType.Length != prev2.IDataType.Length ||
                        current2.IDataType.Accuracy != prev2.IDataType.Accuracy
                        )
                    {
                        isTypeChanged = true;
                    }

                    if (isTypeChanged)
                    {
                        DiffDataType diff_data_type = new DiffDataType(prev2.IDataType, current2.IDataType);
                        if (!diff_data_type.IsSame())
                            t[DiffEnumHistoryAnnotation.DiffPropertyDataType.ToString()] = diff_data_type;
                    }

                    DiffProperty diff = new DiffProperty(prev2, current2);
                    t[DiffEnumHistoryAnnotation.DiffProperty.ToString()] = diff;
                }
            }
            this.ClearDics();
        }
    }
}
