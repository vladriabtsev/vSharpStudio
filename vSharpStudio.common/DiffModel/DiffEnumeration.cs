using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffEnumeration
    {
        public DiffEnumeration(IEnumeration previous, IEnumeration current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IEnumeration Current { get; private set; }
        public IEnumeration Previous { get; private set; }
    }
}
