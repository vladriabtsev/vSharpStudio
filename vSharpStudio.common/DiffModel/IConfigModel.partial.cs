using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IConfigModel
    {
        DictionaryExt<string, IvPluginGeneratorNodeSettings> DicGenNodeSettings { get; }
        IDataType GetIdDataType();
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);
        //string GetVersionFieldGuid();
        string GetIdFieldName(IvPluginDbGenerator dbGen);
        string GetIdFieldGuid();
    }
}
