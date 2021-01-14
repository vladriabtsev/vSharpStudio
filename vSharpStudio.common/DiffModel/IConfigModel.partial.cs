using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IConfigModel : IGetNodeSetting
    {
        IReadOnlyDictionary<string, IvPluginGeneratorNodeSettings> DicGenNodeSettings { get; }
        IDataType GetIdDataType();
        //string GetVersionFieldName(IvPluginDbGenerator dbGen);
        //string GetVersionFieldGuid();
        string GetIdFieldName(IvPluginDbGenerator dbGen);
        string GetIdFieldGuid();
        IProperty GetRefParentProperty(IvPluginDbGenerator dbGen, ICompositeName parent);
        IReadOnlyList<IProperty> GetListDocSharedProperties(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetListProperties(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IEnumeration> GetListEnumerations(string guidAppPrjGen);
        IReadOnlyList<IEnumerationPair> GetListEnumerationPairs(IEnumeration node, string guidAppPrjGen);
        IReadOnlyList<IConstant> GetListConstants(string guidAppPrjGen);
        IReadOnlyList<ICatalog> GetListCatalogs(string guidAppPrjGen);
        IReadOnlyList<IPropertiesTab> GetListTabs(ITreeConfigNode node, string guidAppPrjGen);
        IReadOnlyList<IDocument> GetListDocuments(string guidAppPrjGen);
    }
}
