using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDbSchema
    {
        string DbSchemaStr { get; }
        string KeyNameStr { get; }
        string PKeyTypeStr { get; }
        string IdGeneratorStr { get; }
        string VersionFieldNameStr { get; }
        string LP { get; }
        string RP { get; }
    }
}
