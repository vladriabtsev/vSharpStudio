using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    public class DiffDocument
    {
        public DiffDocument(IDocument previous, IDocument current)
        {
            this.Previous = previous;
            this.Current = current;
        }
        public IDocument Current { get; private set; }
        public IDocument Previous { get; private set; }
    }
}
