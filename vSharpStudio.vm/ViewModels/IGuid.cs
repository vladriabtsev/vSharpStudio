﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface IGuid : IComparable
    {
        string Guid { get; }
    }
}
