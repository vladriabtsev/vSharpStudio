﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IMainViewForm : ITreeConfigNodeSortable
    {
        IGroupListMainViewForms ParentGroupListMainViewFormsI { get; }
    }
}
