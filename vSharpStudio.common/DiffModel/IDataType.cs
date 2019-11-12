using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vSharpStudio.common
{
    public partial interface IDataType
    {
        string ClrTypeName { get; }

        Type ClrType { get; }
    }
}
