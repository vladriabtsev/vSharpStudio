﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IMainViewForm : ITreeConfigNode
    {
        IGroupListMainViewForms ParentGroupListMainViewFormsI { get; }
    }
}
