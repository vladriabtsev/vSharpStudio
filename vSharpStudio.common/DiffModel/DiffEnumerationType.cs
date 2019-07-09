using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffEnumerationType
    {
        public DiffEnumerationType(IEnumeration previous, IEnumeration current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IEnumeration Current { get; private set; }
        public IEnumeration Previous { get; private set; }
    }
}
