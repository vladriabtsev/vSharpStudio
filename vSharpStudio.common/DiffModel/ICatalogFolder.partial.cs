﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface ICatalogFolder : ITreeConfigNode , IGetNodeSetting, IDbTable
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen);
        IReadOnlyList<IPropertiesTab> GetIncludedPropertiesTabs(string guidAppPrjGen);
    }
}
