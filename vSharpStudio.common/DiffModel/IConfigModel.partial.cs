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
        DictionaryExt<string, DictionaryExt<string, IvPluginGeneratorNodeSettings>> DicGenNodeSettings { get; }
        IProperty GetIdProperty(IvPluginDbGenerator dbGen);
        IProperty GetVersionProperty(IvPluginDbGenerator dbGen);
        IProperty GetRefProperty(IvPluginDbGenerator dbGen, ICompositeName parent);
        IDataType GetIdDataType();
    }
}
