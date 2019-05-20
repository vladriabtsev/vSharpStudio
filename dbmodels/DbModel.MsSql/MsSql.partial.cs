using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class MsSql : IParent
    {
        public ITreeConfigNode Parent { get; set; }
    }
}
