using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffCatalog
    {
        public DiffCatalog(ICatalog previous, ICatalog current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public ICatalog Current { get; private set; }
        public ICatalog Previous { get; private set; }
    }
}
