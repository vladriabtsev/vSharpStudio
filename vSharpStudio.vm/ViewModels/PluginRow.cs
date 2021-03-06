﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class PluginRow
    {
        public vPluginLayerTypeEnum GeneratorType { get; set; }

        public Plugin Plugin { get; set; }

        public PluginGenerator PluginGenerator { get; set; }
    }

    public class PluginComparer : IEqualityComparer<PluginRow>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(PluginRow x, PluginRow y)
        {
            Contract.Requires(x != null);
            Contract.Requires(y != null);
            if (x.Plugin.Guid == y.Plugin.Guid && x.Plugin.Version == y.Plugin.Version)
            {
                return true;
            }

            // Check whether the compared objects reference the same data.
            // if (Object.ReferenceEquals(x.Plugin, y.Plugin)) return true;

            // Check whether any of the compared objects is null.
            // if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            //    return false;

            // Check whether the products' properties are equal.
            // return x.Code == y.Code && x.Name == y.Name;

            return false;
        }

        public int GetHashCode(PluginRow obj)
        {
            Contract.Requires(obj != null);
            int c1 = obj.Plugin.Guid.GetHashCode();
            int c2 = obj.Plugin.Version.GetHashCode();
            return c1 ^ c2;
        }
    }
}
