using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FkComplexRef
    {
        public FkComplexRef(string configObjectGuid) : this()
        {
            this._ConfigObjectGuid = configObjectGuid;
        }
        partial void OnCreated()
        {
            this._Guid = System.Guid.NewGuid().ToString();
            this._FkIndexTableGuid = System.Guid.NewGuid().ToString();
        }
    }
}
