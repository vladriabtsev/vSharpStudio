namespace vSharpStudio.common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum DiffEnumHistoryAnnotation
    {
        New,
        Deprecated,
        Deleted,
        Renamed,
        CanLooseData,
    }

    public class DiffLists<T>
            where T : IGuid, IName, IObjectAnnotatable
    {
        protected Dictionary<string, T> dic_oldest;
        protected Dictionary<string, T> dic_prev;
        protected Dictionary<string, T> dic_curr;

        // public DiffLists(IConfig oldest, IConfig prev, IConfig current, ITreeConfigNode parent)
        // {

        // }
        public DiffLists(IEnumerable<T> oldest, IEnumerable<T> prev, IEnumerable<T> current)
        {
            this.ListAll = new List<T>();

            this.dic_oldest = new Dictionary<string, T>();
            if (oldest != null)
            {
                foreach (var t in oldest)
                {
                    if (this.dic_oldest.ContainsKey(t.Guid))
                    {
                        throw new Exception();
                    }

                    this.dic_oldest[t.Guid] = t;
                }
            }
            this.dic_prev = new Dictionary<string, T>();
            if (prev != null)
            {
                foreach (var t in prev)
                {
                    if (this.dic_prev.ContainsKey(t.Guid))
                    {
                        throw new Exception();
                    }

                    this.dic_prev[t.Guid] = t;
                }
            }
            this.dic_curr = new Dictionary<string, T>();
            if (current != null)
            {
                foreach (var t in current)
                {
                    foreach (var tt in t.GetAnnotations().ToList())
                    {
                        t.RemoveAnnotation(tt.Name);
                    }
                    if (this.dic_curr.ContainsKey(t.Guid))
                    {
                        throw new Exception();
                    }

                    this.dic_curr[t.Guid] = t;
                }
                // New, Renamed
                foreach (var t in current)
                {
                    if (!this.dic_prev.ContainsKey(t.Guid))
                    {
                        t[DiffEnumHistoryAnnotation.New.ToString()] = DiffEnumHistoryAnnotation.New;
                    }
                    else if (t.Name != this.dic_prev[t.Guid].Name)
                    {
                        t[DiffEnumHistoryAnnotation.Renamed.ToString()] = DiffEnumHistoryAnnotation.Renamed;
                    }
                    //else
                    //{

                    //}
                    this.ListAll.Add(t);
                }
            }
            // Deprecated, CanLooseData
            if (prev != null)
            {
                foreach (var t in prev)
                {
                    if (!this.dic_curr.ContainsKey(t.Guid))
                    {
                        t[DiffEnumHistoryAnnotation.Deprecated.ToString()] = DiffEnumHistoryAnnotation.Deprecated;
                        this.ListAll.Add(t);
                    }
                    else
                    {
                        var tt = this.dic_curr[t.Guid];
                        if (typeof(T).Name == typeof(IProperty).Name)
                        {
                            var tp = (t as IProperty).IDataType;
                            var tc = (tt as IProperty).IDataType;
                            if (tp.DataTypeEnum != tc.DataTypeEnum)
                            {
                                tt[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = "Property data type was changed from '" + tp.DataTypeEnum + "' to '" + tc.DataTypeEnum + "'.";
                            }
                            else if (tp.Length > tc.Length)
                            {
                                tt[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = "Property data type length was changed from " + tp.Length + " to " + tc.Length + ". Date type:'" + tp.DataTypeEnum + "'.";
                            }
                        }
                        else if (typeof(T).Name == typeof(IConstant).Name)
                        {
                            var tp = (t as IConstant).IDataType;
                            var tc = (tt as IConstant).IDataType;
                            if (tp.DataTypeEnum != tc.DataTypeEnum)
                            {
                                tt[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = "Constant data type was changed from '" + tp.DataTypeEnum + "' to '" + tc.DataTypeEnum + "'.";
                            }
                            else if (tp.Length > tc.Length)
                            {
                                tt[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = "Constant data type length was changed from " + tp.Length + " to " + tc.Length + ". Date type:'" + tp.DataTypeEnum + "'.";
                            }
                        }
                        else if (typeof(T).Name == typeof(IEnumeration).Name)
                        {
                            var tp = t as IEnumeration;
                            var tc = tt as IEnumeration;
                            if (tp.DataTypeEnum > tc.DataTypeEnum)
                            {
                                tt[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = "Enumeration data type was changed from '" + tp.DataTypeEnum + "' to '" + tc.DataTypeEnum + "'.";
                            }
                            else if (tp.DataTypeLength > tc.DataTypeLength)
                            {
                                tt[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = "Enumeration data type length was changed from " + tp.DataTypeLength + " to " + tc.DataTypeLength + ". Date type:'" + tp.DataTypeEnum + "'.";
                            }
                        }
                        else if (typeof(T).Name == typeof(IEnumerationPair).Name)
                        {
                            var tp = t as IEnumerationPair;
                            var tc = tt as IEnumerationPair;
                            if (tp.Value != tc.Value)
                            {
                                tt[DiffEnumHistoryAnnotation.CanLooseData.ToString()] = "EnumerationPair value was changed from '" + tp.Value + "' to '" + tc.Value + "'.";
                            }
                        }
                    }
                }
            }
            // deleted
            if (oldest != null)
            {
                foreach (var t in oldest)
                {
                    if (!this.dic_prev.ContainsKey(t.Guid))
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
        // public static Dictionary<string, DiffConstant> DicTypeChanged { get; private set; }
        public List<T> ListAll { get; private set; }

        protected void ClearDics()
        {
            this.dic_oldest.Clear();
            this.dic_oldest = null;
            this.dic_prev.Clear();
            this.dic_prev = null;
            this.dic_curr.Clear();
            this.dic_curr = null;
        }

        protected bool? IsCanLooseData(IDataType prev, IDataType cur)
        {
            if (cur.DataTypeEnum != prev.DataTypeEnum)
            {
                return true;
            }

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
                        {
                            return true;
                        }

                        return false;
                    }
                    break;
                case EnumDataType.NUMERICAL:
                    if (cur.IsPositive != prev.IsPositive)
                    {
                        if (cur.IsPositive)
                        {
                            return true;
                        }

                        return false;
                    }
                    if (cur.Length != prev.Length)
                    {
                        if (cur.Length < prev.Length)
                        {
                            return true;
                        }

                        return false;
                    }
                    if (cur.Accuracy != prev.Accuracy)
                    {
                        if (cur.Accuracy < prev.Accuracy)
                        {
                            return true;
                        }

                        return false;
                    }
                    break;
                case EnumDataType.ENUMERATION:
                case EnumDataType.DOCUMENT:
                case EnumDataType.CATALOG:
                    if (cur.ObjectGuid != prev.ObjectGuid)
                    {
                        return true;
                    }

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
                        {
                            return true;
                        }
                    }
                    return false;
                default:
                    throw new NotImplementedException();
            }
            return false;
        }
    }
}
