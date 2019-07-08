using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vSharpStudio.common
{
    public enum DiffEnumHistoryAnnotation { New, Deprecated, Deleted, Renamed, DiffDataType, CanLooseData, DiffProperties }
    public class DiffDics<T>
            where T : IGuid, IName, IMutableAnnotatable
    {
        public DiffDics(IEnumerable<T> oldest, IEnumerable<T> prev, IEnumerable<T> current)
        {
            this.ListAll = new List<T>();

            Dictionary<string, T> dic_oldest = new Dictionary<string, T>();
            if (oldest != null)
            {
                foreach (var t in oldest)
                {
                    if (dic_oldest.ContainsKey(t.Guid))
                        throw new Exception();
                    dic_oldest[t.Guid] = t;
                }
            }
            Dictionary<string, T> dic_prev = new Dictionary<string, T>();
            if (prev != null)
            {
                foreach (var t in prev)
                {
                    if (dic_prev.ContainsKey(t.Guid))
                        throw new Exception();
                    dic_prev[t.Guid] = t;
                }
            }
            Dictionary<string, T> dic_curr = new Dictionary<string, T>();
            foreach (var t in current)
            {
                foreach (var tt in t.GetAnnotations().ToList())
                {
                    t.RemoveAnnotation(tt.Name);
                }
                if (dic_curr.ContainsKey(t.Guid))
                    throw new Exception();
                dic_curr[t.Guid] = t;
            }
            // new, renamed
            foreach (var t in current)
            {
                if (!dic_prev.ContainsKey(t.Guid))
                {
                    t[DiffEnumHistoryAnnotation.New.ToString()] = DiffEnumHistoryAnnotation.New;
                }
                else if (t.Name != dic_prev[t.Guid].Name)
                {
                    t[DiffEnumHistoryAnnotation.Renamed.ToString()] = DiffEnumHistoryAnnotation.Renamed;
                }
                else
                {

                }
                this.ListAll.Add(t);
            }
            // deprecated
            if (prev != null)
            {
                foreach (var t in prev)
                {
                    if (!dic_curr.ContainsKey(t.Guid))
                    {
                        foreach (var tt in t.GetAnnotations().ToList())
                        {
                            t.RemoveAnnotation(tt.Name);
                        }
                        t[DiffEnumHistoryAnnotation.Deprecated.ToString()] = DiffEnumHistoryAnnotation.Deprecated;
                        this.ListAll.Add(t);
                    }
                }
            }
            // deleted
            if (oldest != null)
            {
                foreach (var t in oldest)
                {
                    if (!dic_prev.ContainsKey(t.Guid))
                    {
                        foreach (var tt in t.GetAnnotations().ToList())
                        {
                            t.RemoveAnnotation(tt.Name);
                        }
                        t[DiffEnumHistoryAnnotation.Deleted.ToString()] = DiffEnumHistoryAnnotation.Deleted;
                        this.ListAll.Add(t);
                    }
                }
            }
            if (typeof(T).Name == typeof(IConstant).Name)
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
                    var res = IsCanLoseData(prev2, current2);
                    if (res == null)
                        continue;
                    if (res ?? false)
                        t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;
                    var diff_data_type = new DiffDataType(prev2, current2);
                    t[DiffEnumHistoryAnnotation.DiffDataType.ToString()] = diff_data_type;
                }
            }
            else if (typeof(T).Name == typeof(IEnumeration).Name)
            {
                foreach (var t in this.ListAll)
                {
                    IEnumeration tt = (IEnumeration)t;
                    if (tt.IsDeleted())
                        continue;
                    if (tt.IsDeprecated())
                        continue;
                    if (tt.IsNew())
                        continue;
                    IEnumeration prev2 = (IEnumeration)dic_prev[t.Guid];
                    IEnumeration current2 = (IEnumeration)dic_curr[t.Guid];
                    if (prev2.DataTypeEnum!=current2.DataTypeEnum)
                    {
                        if (current2.DataTypeEnum== EnumEnumerationType.BYTE_VALUE)
                            t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;
                        var diff_data_type = new DiffDataType(prev2, current2);
                        t[DiffEnumHistoryAnnotation.DiffDataType.ToString()] = diff_data_type;
                    }
                    else if (current2.DataTypeEnum== EnumEnumerationType.STRING_VALUE)
                    {
                        if (current2.DataTypeLength < prev2.DataTypeLength)
                            t[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = DiffEnumHistoryAnnotation.CanLooseData;
                        var diff_data_type = new DiffDataType(prev2, current2);
                        t[DiffEnumHistoryAnnotation.DiffDataType.ToString()] = diff_data_type;
                    }
                }
            }
            else if (typeof(T).Name == typeof(ICatalog).Name)
            {
                foreach (var t in this.ListAll)
                {
                    ICatalog tt = (ICatalog)t;
                    IEnumerable<IProperty> oldest2 = dic_oldest.ContainsKey(t.Guid) ? ((ICatalog)dic_oldest[t.Guid]).GroupPropertiesI.ListPropertiesI : new List<IProperty>();
                    IEnumerable<IProperty> prev2 = dic_prev.ContainsKey(t.Guid) ? ((ICatalog)dic_prev[t.Guid]).GroupPropertiesI.ListPropertiesI : new List<IProperty>();
                    IEnumerable<IProperty> current2 = dic_curr.ContainsKey(t.Guid) ? ((ICatalog)dic_curr[t.Guid]).GroupPropertiesI.ListPropertiesI : new List<IProperty>();
                    var diff_properties = new DiffProperties(oldest2, prev2, current2);
                    t[DiffEnumHistoryAnnotation.DiffProperties.ToString()] = diff_properties;
                }
            }
            dic_oldest.Clear();
            dic_prev.Clear();
            dic_curr.Clear();
        }
        // string is a current namespace plus name
        //public static Dictionary<string, DiffConstant> DicTypeChanged { get; private set; }
        public List<T> ListAll { get; private set; }

        private bool? IsCanLoseData(IDataType prev, IDataType cur)
        {
            if (cur.DataTypeEnum != prev.DataTypeEnum)
                return true;
            switch (cur.DataTypeEnum)
            {
                case EnumDataType.STRING:
                    if (cur.Length != prev.Length)
                    {
                        if (cur.Length < prev.Length || prev.Length == 0)
                            return true;
                        return false;
                    }
                    break;
                case EnumDataType.NUMERICAL:
                    if (cur.IsPositive != prev.IsPositive)
                    {
                        if (cur.IsPositive)
                            return true;
                        return false;
                    }
                    if (cur.Length != prev.Length)
                    {
                        if (cur.Length < prev.Length)
                            return true;
                        return false;
                    }
                    if (cur.Accuracy != prev.Accuracy)
                    {
                        if (cur.Accuracy < prev.Accuracy)
                            return true;
                        return false;
                    }
                    break;
            }
            return null;
        }
    }
}
