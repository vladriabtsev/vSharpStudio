using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public class DiffCatalogs : DiffDics<ICatalog>
    {
        public static DiffCatalogs DiffDics;
        public DiffCatalogs(IEnumerable<ICatalog> oldest, IEnumerable<ICatalog> prev, IEnumerable<ICatalog> current)
            : base(oldest, prev, current)
        {
            //DiffConstants.DicTypeChanged = new Dictionary<string, DiffConstant>();
            DiffCatalogs.DiffDics = this;
            foreach(var t in this.ListAll)
            {

            }
        }
        //public static Dictionary<string, DiffConstant> DicTypeChanged { get; private set; }
    }
    public static partial class Ext
    {
        public static bool IsNew(this ICatalog obj)
        {
            if (DiffCatalogs.DiffDics.DicNew.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        public static bool IsDeprecated(this ICatalog obj)
        {
            if (DiffCatalogs.DiffDics.DicDeprecated.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        public static bool IsDeleted(this ICatalog obj)
        {
            if (DiffCatalogs.DiffDics.DicDeleted.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        public static bool IsRenamed(this ICatalog obj)
        {
            if (DiffCatalogs.DiffDics.DicRenamed.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        //public static DiffCatalog GetChanges(this ICatalog obj)
        //{
        //    if (DiffCatalogs.DicChanged.ContainsKey(obj.FullName))
        //    {
        //        return DiffCatalogs.DicChanged[obj.FullName];
        //    }
        //    return null;
        //}
    }
}
