using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
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
}
