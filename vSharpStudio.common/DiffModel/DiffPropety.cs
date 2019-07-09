using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffProperty
    {
        public DiffProperty(IProperty previous, IProperty current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IProperty Current { get; private set; }
        public IProperty Previous { get; private set; }
    }
}
