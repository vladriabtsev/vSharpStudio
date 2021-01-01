using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDbSchema
    {
        string DbSchemaStr { get; }
        string PKeyName { get; }
        string PKeyTypeStr { get; }
        string PKeyStoreTypeStr { get; }
        string IdGeneratorStr { get; }
        //string VersionFieldName { get; }
        string LP { get; }
        string RP { get; }
    }
}
