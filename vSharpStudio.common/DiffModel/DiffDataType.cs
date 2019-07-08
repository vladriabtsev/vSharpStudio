using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffDataType
    {
        public DiffDataType(IDataType previous, IDataType current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IDataType Current { get; private set; }
        public IDataType Previous { get; private set; }
    }
}
