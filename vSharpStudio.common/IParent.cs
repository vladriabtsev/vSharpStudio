using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public interface IParentObject // : ITreeModel
    {
        IEditableObjectExt Parent { get; set; }
    }
    public interface IParent // : ITreeModel
    {
        ITreeConfigNode? Parent { get; set; }
    }
    public interface IChildrenCollection : ICollection, IEnumerable //IMoveUpDown, 
    {
        void Clear();
        void Add(object item);
        //void Add(ITreeConfigNode item);
        //bool Remove(ITreeConfigNode item);
        //void RemoveAt(int indx);
        //void AddRange(IEnumerable<ITreeConfigNode> collection);
        //Action OnClearingAction { get; set; }
        //Action OnClearedAction { get; set; }
        //Action<ITreeConfigNode> OnRemovedAction { get; set; }
        //Action<ITreeConfigNode> OnAddedAction { get; set; }
        //Action<ITreeConfigNode> OnRemovingAction { get; set; }
        //Action<ITreeConfigNode> OnAddingAction { get; set; }
    }
    public interface ITree : IParent
    {
        //IChildrenCollection Children { get; }
        IChildrenCollection GetListSiblings();
        IChildrenCollection GetListChildren();
        bool HasChildren();
    }
}
