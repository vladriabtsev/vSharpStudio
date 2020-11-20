using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public interface IvPluginGeneratorNodeIncludable
    {
        bool? IsIncluded { get; set; }
    }
}
