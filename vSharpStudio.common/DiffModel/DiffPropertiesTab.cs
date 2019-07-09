using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffPropertiesTab
    {
        public DiffPropertiesTab(IPropertiesTab previous, IPropertiesTab current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IPropertiesTab Current { get; private set; }
        public IPropertiesTab Previous { get; private set; }
    }
}
