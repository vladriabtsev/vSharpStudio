using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public interface IDbTable: IGuid, ICompositeName
    {
    }
    public interface ICanGoLeft
    {
    }
    public interface ICanGoRight
    {
    }
    public interface IOnAdded
    {
        void OnAdded();
    }
    public interface ICanAddNode //: IOnAdded, IOnRemoved
    {
        bool IsNew { get; set; }
    }
    public interface IEditableNode : ICanAddNode
    {
        bool IsChanged { get; set; }
        bool IsMarkedForDeletion { get; set; }
        void Remove();
    }
    public interface IEditableNodeGroup
    {
        bool IsHasChanged { get; set; }
        bool IsNewOrHasNew { get; }
        bool IsHasNew { get; set; }
        bool IsHasMarkedForDeletion { get; set; }
    }
    public interface IOnRemoved
    {
        void OnRemoved();
    }
    public interface ICanRemoveNode //: IOnRemoved
    {
    }
    public interface ICanAddSubNode
    {
        bool CanAddSubNode();
    }
}
