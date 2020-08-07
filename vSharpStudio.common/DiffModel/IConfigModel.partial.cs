﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IConfigModel : IObjectAnnotatable
    {
        DictionaryExt<string, DictionaryExt<string, IvPluginGeneratorNodeSettings>> DicGenNodeSettings { get; }
    }
}
