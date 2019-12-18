﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public interface ITreeConfigNode : ITree, IValidatableWithSeverity, ISortingValue
    {
        string Guid { get; }
        string Name { get; set; }
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
        // ITreeConfigNode Parent { get; set; }
        void Sort(Type type);
        bool NodeCanMoveUp();
        void NodeMoveUp();
        bool NodeCanMoveDown();
        void NodeMoveDown();
        bool NodeCanAddNew();
        ITreeConfigNode NodeAddNew();
        bool NodeCanAddNewSubNode();
        ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node = null);
        // Clone selected. Name is same + suffix 'New'
        bool NodeCanAddClone();
        ITreeConfigNode NodeAddClone();
        bool NodeCanRemove();
        void NodeRemove();
        bool NodeCanLeft();
        void NodeLeft();
        bool NodeCanRight();
        void NodeRight();
        bool NodeCanUp();
        void NodeUp();
        bool NodeCanDown();
        void NodeDown();
        IConfig GetConfig();
       // if true, object can be included in submodel
        // if false, this is group of objects. Can't be included in submodel, but show inclusion of objects in the group
        bool IsIncludableInModels { get; }
        List<IModelRow> ListInModels { get; }
    }
}
