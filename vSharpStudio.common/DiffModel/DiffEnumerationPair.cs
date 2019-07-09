using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffEnumerationPair
    {
        public DiffEnumerationPair(IEnumerationPair previous, IEnumerationPair current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IEnumerationPair Current { get; private set; }
        public IEnumerationPair Previous { get; private set; }
    }
}
