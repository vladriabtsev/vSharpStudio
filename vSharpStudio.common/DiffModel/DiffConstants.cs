using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffConstant
    {
        public DiffConstant(IConstant previous, IConstant current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IConstant Current { get; private set; }
        public IConstant Previous { get; private set; }
    }
    public class DiffConstants : DiffDics<IConstant>
    {
        public static DiffConstants DiffDics;
        public DiffConstants(IEnumerable<IConstant> oldest, IEnumerable<IConstant> prev, IEnumerable<IConstant> current)
            : base(oldest, prev, current)
        {
            //DiffConstants.DicTypeChanged = new Dictionary<string, DiffConstant>();
            DiffConstants.DiffDics = this;
        }
        //public static Dictionary<string, DiffConstant> DicTypeChanged { get; private set; }
    }
    public static partial class ExtConstant
    {
        public static bool IsNew(this IConstant obj)
        {
            if (DiffConstants.DiffDics.DicNew.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        public static bool IsDeprecated(this IConstant obj)
        {
            if (DiffConstants.DiffDics.DicDeprecated.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        public static bool IsDeleted(this IConstant obj)
        {
            if (DiffConstants.DiffDics.DicDeleted.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        public static bool IsRenamed(this IConstant obj)
        {
            if (DiffConstants.DiffDics.DicRenamed.ContainsKey(obj.FullName))
                return true;
            return false;
        }
        //public static DiffConstant GetChanges(this IConstant obj)
        //{
        //    if (DiffConstants.DicTypeChanged.ContainsKey(obj.FullName))
        //    {
        //        return DiffConstants.DicTypeChanged[obj.FullName];
        //    }
        //    return null;
        //}
    }
}
