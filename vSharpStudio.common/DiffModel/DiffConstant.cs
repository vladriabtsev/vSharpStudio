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
}
