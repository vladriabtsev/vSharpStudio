﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupListCommon : ITreeConfigNodeSortable
    {
        IModel ParentModelI { get; }
    }
}
