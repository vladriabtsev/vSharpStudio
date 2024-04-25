using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ComplexRef
    {
        /// <summary>
        /// Complex reference
        /// </summary>
        /// <param name="propGuid">Reference property GUID. One exception, for register it is empty</param>
        /// <param name="foreignObjectGuid">Referenced complex object. Catalog, ANY, ...</param>
        public ComplexRef(string propGuid, string foreignObjectGuid) : this()
        {
            this._ForeignObjectGuid = foreignObjectGuid;
            this._Guid = propGuid;
        }
        partial void OnCreated()
        {
            this._RefComplexObjectIdPropertyGuid = System.Guid.NewGuid().ToString();
        }
    }
}
