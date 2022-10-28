﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupDocuments : ITreeConfigNode
    {
        IModel ParentModelI { get; }
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
        bool GetIsGridSortable();
        bool GetIsGridFilterable();
        bool GetIsGridSortableCustom();
    }
}
