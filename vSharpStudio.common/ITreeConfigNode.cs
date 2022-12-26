using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public interface ISettings
    {
        bool IsChanged { get; set; }
    }
    public interface ITreeConfigNodeSortable : ITreeConfigNode, ISortingValue
    {
    }
    public interface ITreeConfigNode : ITree, IValidatableWithSeverity, IGuid, IName, ISettings
    {
        bool AutoGenerateProperties { get; }
        public Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection PropertyDefinitions { get; }
        string ModelPath { get; }
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
        // ITreeConfigNode Parent { get; set; }
        //bool IsHasChanged { get; set; }
        //bool IsNotNotifying { get; set; }
        //bool IsValidate { get; set; }
        bool IsNewNode();
        bool IsDeleted();
        bool IsDeprecated();
        bool IsRenamed(bool isStable);
        //bool IsCanLooseData(bool isStable);
        ITreeConfigNode? PrevStableVersion();
        ITreeConfigNode? PrevCurrentVersion();
        void Sort(Type type);
        bool NodeCanMoveUp();
        void NodeMoveUp();
        bool NodeCanMoveDown();
        void NodeMoveDown();
        bool NodeCanAddNew();
        ITreeConfigNode NodeAddNew();
        bool NodeCanAddNewSubNode();
        ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node = null);
        // Clone selected. Name is same + suffix 'New'
        bool NodeCanAddClone();
        ITreeConfigNode NodeAddClone();
        bool NodeCanMarkForDeletion();
        void NodeMarkForDeletion();
        bool NodeCanLeft();
        void NodeLeft();
        bool NodeCanRight();
        void NodeRight();
        bool NodeCanUp();
        void NodeUp();
        bool NodeCanDown();
        void NodeDown();
        // Get path relative to config file path
        string GetRelativeToConfigDiskPath(string path);
        // Get combined config and relative path
        string GetCombinedPath(string relative_path);
        // if true, object can be included in submodel
        // if false, this is group of objects. Can't be included in submodel, but show inclusion of objects in the group
        bool IsIncludableInModels { get; }
        List<IModelRow> ListInModels { get; }
        IConfig Cfg { get; }
    }
}
