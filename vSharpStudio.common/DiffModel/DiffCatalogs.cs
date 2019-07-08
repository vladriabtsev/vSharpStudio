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
}
