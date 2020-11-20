﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public class DebugList<T> : List<T>
    {
        new public void Add(T item)
        {
            base.Add(item);
        }
        new public void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
        }
    }
}
