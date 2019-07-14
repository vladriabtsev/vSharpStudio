﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffListConstants : DiffLists<IConstant>
    {
        public DiffListConstants(IEnumerable<IConstant> oldest, IEnumerable<IConstant> prev, IEnumerable<IConstant> current)
            : base(oldest, prev, current)
        {
            foreach (var t in this.ListAll)
            {
                IConstant tt = (IConstant)t;
                if (tt.IsDeleted())
                    continue;
                if (tt.IsDeprecated())
                    continue;
                if (tt.IsNew())
                    continue;
                IDataType prev2 = dic_prev.ContainsKey(t.Guid) ? ((IConstant)dic_prev[t.Guid]).DataTypeI : null;
                IDataType current2 = dic_curr.ContainsKey(t.Guid) ? ((IConstant)dic_curr[t.Guid]).DataTypeI : null;
                var res = IsCanLooseData(prev2, current2);
                if (res == null)
                    continue;
                if (res ?? false)
                    t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;
                var diff_data_type = new DiffDataType(prev2, current2);
                t[DiffEnumHistoryAnnotation.DiffPropertyDataType.ToString()] = diff_data_type;

                IConstant prev3 = dic_prev.ContainsKey(t.Guid) ? (IConstant)dic_prev[t.Guid] : null;
                IConstant current3 = dic_curr.ContainsKey(t.Guid) ? (IConstant)dic_curr[t.Guid] : null;
                DiffConstant diff = new DiffConstant(prev3, current3);
                t[DiffEnumHistoryAnnotation.DiffConstant.ToString()] = diff;
            }
            this.ClearDics();
        }
    }
}