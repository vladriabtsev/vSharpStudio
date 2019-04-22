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
    internal interface IListNodes
    {
        IMoveUpDown ListNodes { get; }
    }
    internal interface IListGroupNodes
    {
        SortedObservableCollection<ITreeConfigNode> ListNodes { get; }
    }
    internal interface IListNodes<T> where T : ISortingValue
    {
        SortedObservableCollection<T> ListNodes { get; }
    }
    interface IListProperties
    {
        SortedObservableCollection<Property> ListProperties { get; }
    }
}
