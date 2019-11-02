using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vSharpStudio.common
{
    public enum DiffEnumHistoryAnnotation
    {
        New, Deprecated, Deleted, Renamed, DiffListEnumerationPairs, DiffEnumerationType, DiffPropertyDataType,
        CanLooseData, DiffConfig, DiffConstant, DiffEnumeration, DiffProperty, DiffPropertiesTab, DiffCatalog, DiffDocument, DiffListProperties, DiffListPropertiesTabs, DiffEnumerationPair
    }
    public class DiffLists<T>
            where T : IGuid, IName, IMutableAnnotatable
    {
        protected Dictionary<string, T> dic_oldest;
        protected Dictionary<string, T> dic_prev;
        protected Dictionary<string, T> dic_curr;
        public DiffLists(IEnumerable<T> oldest, IEnumerable<T> prev, IEnumerable<T> current)
        {
            this.ListAll = new List<T>();

            dic_oldest = new Dictionary<string, T>();
            if (oldest != null)
            {
                foreach (var t in oldest)
                {
                    if (dic_oldest.ContainsKey(t.Guid))
                        throw new Exception();
                    dic_oldest[t.Guid] = t;
                }
            }
            dic_prev = new Dictionary<string, T>();
            if (prev != null)
            {
                foreach (var t in prev)
                {
                    if (dic_prev.ContainsKey(t.Guid))
                        throw new Exception();
                    dic_prev[t.Guid] = t;
                }
            }
            dic_curr = new Dictionary<string, T>();
            if (current != null)
            {
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
        }
        // string is a current namespace plus name
        //public static Dictionary<string, DiffConstant> DicTypeChanged { get; private set; }
        public List<T> ListAll { get; private set; }
        protected void ClearDics()
        {
            dic_oldest.Clear();
            dic_oldest = null;
            dic_prev.Clear();
            dic_prev = null;
            dic_curr.Clear();
            dic_curr = null;
        }
        protected bool? IsCanLooseData(IDataType prev, IDataType cur)
        {
            if (cur.DataTypeEnum != prev.DataTypeEnum)
                return true;
            switch (cur.DataTypeEnum)
            {
                case EnumDataType.ANY:
                case EnumDataType.BOOL:
                case EnumDataType.DATE:
                case EnumDataType.DATETIME:
                case EnumDataType.TIME:
                    return false;
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
                case EnumDataType.ENUMERATION:
                case EnumDataType.DOCUMENT:
                case EnumDataType.CATALOG:
                    if (cur.ObjectGuid != prev.ObjectGuid)
                        return true;
                    return false;
                case EnumDataType.DOCUMENTS:
                case EnumDataType.CATALOGS:
                    foreach (var t in prev.IListObjectGuids)
                    {
                        bool is_found = false;
                        foreach (var tt in cur.IListObjectGuids)
                        {
                            if (t == tt)
                            {
                                is_found = true;
                                break;
                            }
                        }
                        if (!is_found)
                            return true;
                    }
                    return false;
                default:
                    throw new NotImplementedException();
            }
            return false;
        }
    }
}
