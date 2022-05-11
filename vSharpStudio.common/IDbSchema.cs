using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDbSchema
    {
        string DbSchemaStr { get; }
        string IdGeneratorStr { get; }
        //string VersionFieldName { get; }
        string LP { get; }
        string RP { get; }
    }
}
