using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public class DebugList<T> : List<T>
    {
        public new void Add(T item)
        {
            base.Add(item);
        }
        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
        }
    }
}
