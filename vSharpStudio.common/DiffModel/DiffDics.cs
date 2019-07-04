using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vSharpStudio.common
{
    public enum DiffEnumAnnotation { New, Deprecated, Deleted, Renamed, DiffProperties }
    public class DiffDics<T>
            where T : IGuid, IName, IMutableAnnotatable
    {
        public DiffDics(IEnumerable<T> oldest, IEnumerable<T> prev, IEnumerable<T> current)
        {
            this.DicNew = new Dictionary<string, T>();
            this.DicDeprecated = new Dictionary<string, T>();
            this.DicDeleted = new Dictionary<string, T>();
            this.DicRenamed = new Dictionary<string, T>();
            this.ListAll = new List<T>();

            Dictionary<string, T> dic_oldest = new Dictionary<string, T>();
            foreach (var t in oldest)
            {
                if (dic_oldest.ContainsKey(t.Guid))
                    throw new Exception();
                dic_oldest[t.Guid] = t;
            }
            Dictionary<string, T> dic_prev = new Dictionary<string, T>();
            foreach (var t in prev)
            {
                if (dic_prev.ContainsKey(t.Guid))
                    throw new Exception();
                dic_prev[t.Guid] = t;
            }
            Dictionary<string, T> dic_curr = new Dictionary<string, T>();
            foreach (var t in current)
            {
                if (dic_curr.ContainsKey(t.Guid))
                    throw new Exception();
                dic_curr[t.Guid] = t;
            }
            // new, renamed
            foreach (var t in current)
            {
                foreach (var tt in t.GetAnnotations())
                {
                    t.RemoveAnnotation(tt.Name);
                }
                if (!dic_prev.ContainsKey(t.Guid))
                {
                    t[DiffEnumAnnotation.New.ToString()] = DiffEnumAnnotation.New;
                    this.DicNew[t.FullName] = t;
                }
                else if (t.Name != dic_prev[t.Guid].Name)
                {
                    t[DiffEnumAnnotation.Renamed.ToString()] = DiffEnumAnnotation.Renamed;
                    this.DicRenamed[t.FullName] = t;
                }
                else
                {

                }
                this.ListAll.Add(t);
            }
            // deprecated
            foreach (var t in prev)
            {
                if (!dic_curr.ContainsKey(t.Guid))
                {
                    foreach (var tt in t.GetAnnotations())
                    {
                        t.RemoveAnnotation(tt.Name);
                    }
                    t[DiffEnumAnnotation.Deprecated.ToString()] = DiffEnumAnnotation.Deprecated;
                    this.DicDeprecated[t.FullName] = t;
                    this.ListAll.Add(t);
                }
            }
            // deleted
            foreach (var t in oldest)
            {
                if (!dic_prev.ContainsKey(t.Guid))
                {
                    this.DicDeleted[t.FullName] = t;
                }
            }
            if (typeof(T) is ICatalog)
            {
                foreach(var t in this.ListAll)
                {
                    ICatalog tt = (ICatalog)t;
                    IEnumerable<IProperty> oldest2 = dic_oldest.ContainsKey(t.Guid) ? ((ICatalog)dic_oldest[t.Guid]).GroupPropertiesI.ListPropertiesI : new List<IProperty>();
                    IEnumerable<IProperty> prev2 = dic_prev.ContainsKey(t.Guid) ? ((ICatalog)dic_prev[t.Guid]).GroupPropertiesI.ListPropertiesI : new List<IProperty>();
                    IEnumerable<IProperty> current2 = dic_curr.ContainsKey(t.Guid) ? ((ICatalog)dic_curr[t.Guid]).GroupPropertiesI.ListPropertiesI : new List<IProperty>();
                    var diff_props = new DiffProperties(oldest2, prev2, current2);
                    t[DiffEnumAnnotation.DiffProperties.ToString()] = diff_props;
                }
            }
        }
        // string is a current namespace plus name
        public Dictionary<string, T> DicNew { get; private set; }
        public Dictionary<string, T> DicDeprecated { get; private set; }
        public Dictionary<string, T> DicDeleted { get; private set; }
        public Dictionary<string, T> DicRenamed { get; private set; }
        //public static Dictionary<string, DiffConstant> DicTypeChanged { get; private set; }
        public List<T> ListAll { get; private set; }
    }
}
