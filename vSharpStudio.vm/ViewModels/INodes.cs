using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    internal interface ICanLeft
    {
    }
    internal interface ICanRight
    {
    }
    internal interface ICanAddNode
    {
    }
    internal interface ICanAddSubNode
    {
    }
    internal interface IGroupListSubNodes
    {
        int Count { get; }
        int IndexOf(ITreeConfigNode obj);
        ITreeConfigNode GetNode(int index);
//        void AddNew();
    }
    internal interface IListGroupNodes
    {
        SortedObservableCollection<ITreeConfigNode> ListNodes { get; }
    }
    internal interface IListNodes<T> where T : ISortingValue
    {
        SortedObservableCollection<T> ListNodes { get; }
    }
}
