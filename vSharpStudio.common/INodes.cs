﻿using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public interface INewAndDeleteion
    {
        bool IsNew { get; set; }
        bool IsMarkedForDeletion { get; set; }
        bool IsHasNew { get; set; }
        bool IsHasMarkedForDeletion { get; set; }
        bool GetIsHasMarkedForDeletion();
        bool GetIsHasNew();
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
