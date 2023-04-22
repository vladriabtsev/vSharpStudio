﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace vSharpStudio.wpf.Controls
{
    public interface ITreeModel
	{
		/// <summary>
		/// Get list of children of the specified parent
		/// </summary>
		IEnumerable GetChildren(object parent);

        /// <summary>
        /// returns wheather specified parent has any children or not.
        /// </summary>
        bool HasChildren(object parent);
	}
}
