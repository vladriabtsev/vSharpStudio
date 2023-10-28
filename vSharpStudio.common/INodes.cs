using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
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
        /// <summary>
        /// Object is new from moment it added to configuration and until next stable/production version is created
        /// </summary>
        bool IsNew { get; set; }
    }
    public interface IEditableNode
    {
        bool IsChanged { get; set; }
        /// <summary>
        /// Marked for deletion object are deleted when
        ///  - during code generation if object is new
        ///  - if object exist in stable/production version, then it will survive as deprecated object for next stable/production version
        ///  - if object already exist in stable/production version as deprecated, then it will deleted when next stable/production version will be created
        /// </summary>
        bool IsMarkedForDeletion { get; set; }
        void Remove();
    }
    public interface IEditableNodeGroup
    {
        bool IsHasChanged { get; set; }
        bool IsNewOrHasNew { get; }
        bool IsHasNew { get; set; }
        //void SetIsHasNew(bool isHasNew);
        bool IsHasMarkedForDeletion { get; set; }
        void CheckChildrenIsOrHasChanged();
        void CheckChildrenIsOrHasNew();
        void CheckChildrenIsOrHasMarkedForDeletion();
        void RestoreIsHas();
        static bool IsChangedNotPropagate { get; set; }
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
