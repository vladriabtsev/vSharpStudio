﻿using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Dummy
{
    //
    // Summary:
    //     Specifies the display state of an element.
    public enum Visibility : byte
    {
        //
        // Summary:
        //     Display the element.
        Visible = 0,
        //
        // Summary:
        //     Do not display the element, but reserve space for the element in layout.
        Hidden = 1,
        //
        // Summary:
        //     Do not display the element, and do not reserve space for it in layout.
        Collapsed = 2
    }
}
