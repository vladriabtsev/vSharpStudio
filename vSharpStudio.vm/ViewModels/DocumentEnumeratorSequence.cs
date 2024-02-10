﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.ViewModels;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class DocumentEnumeratorSequence : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" {this.ToString()}";
        }
        public override string ToString()
        {
            string unique = "";
            var conv = new EnumDescriptionTypeConverter(typeof(EnumMonths));
            switch (this.ScopeOfUnique)
            {
                case EnumDocNumberUniqueScope.DOC_UNIQUE_FOREVER:
                    unique = "Unique";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_YEAR:
                    unique = $"Year: {conv.ConvertTo(null, null, this.ScopePeriodStartMonth, typeof(string))} {this.ScopePeriodStartMonthDay}";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_QUATER:
                    unique = $"Quater: {conv.ConvertTo(null, null, this.ScopePeriodStartMonth, typeof(string))} {this.ScopePeriodStartMonthDay}";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_MONTH:
                    unique = "Month";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_WEEK:
                    unique = "Week";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_DAY:
                    unique = "Day";
                    break;
                default:
                    throw new NotImplementedException();
            }
            return $"{this.Name}-{unique}";
        }
        public string Text { get { return this.Name; } }
        public string Value { get { return this.Guid; } }
        [Browsable(false)]
        public GroupListEnumeratorSequences ParentGroupListSequences { get { Debug.Assert(this.Parent != null); return (GroupListEnumeratorSequences)this.Parent; } }
        [Browsable(false)]
        public IGroupListEnumeratorSequences ParentGroupListSequencesI { get { Debug.Assert(this.Parent != null); return (IGroupListEnumeratorSequences)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListSequences.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconLifeline"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._ScopePeriodStartMonth = EnumMonths.MONTH_JANUARY;
            this._ScopePeriodStartMonthDay = 1;
            this._SequenceType = EnumCodeType.Text;
            this._MaxSequenceLength = 9;
            this._Prefix = "";
            this._ScopeOfUnique = common.EnumDocNumberUniqueScope.DOC_UNIQUE_YEAR;
            this._ScopePeriodStartMonth = EnumMonths.MONTH_JANUARY;
            this._ScopePeriodStartMonthDay = 1;

            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            //this.ListMainViewForms.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListMainViewForms.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListMainViewForms.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListMainViewForms.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            //this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            //this.GroupForms.AddAllAppGenSettingsVmsToNode();
            //this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListSequences.ListEnumeratorSequences.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (DocumentEnumeratorSequence?)this.ParentGroupListSequences.ListEnumeratorSequences.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListSequences.ListEnumeratorSequences.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListSequences.ListEnumeratorSequences.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (DocumentEnumeratorSequence?)this.ParentGroupListSequences.ListEnumeratorSequences.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListSequences.ListEnumeratorSequences.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = DocumentEnumeratorSequence.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListSequences.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new DocumentEnumeratorSequence(this.Parent);
            this.ParentGroupListSequences.Add(node);
            this.GetUniqueName(Defaults.SequenceName, node, this.ParentGroupListSequences.ListEnumeratorSequences);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListSequences.ListEnumeratorSequences.Remove(this);
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            switch (this.ScopeOfUnique)
            {
                case EnumDocNumberUniqueScope.DOC_UNIQUE_QUATER:
                case EnumDocNumberUniqueScope.DOC_UNIQUE_YEAR:
                    break;
                default:
                    lst.Add(nameof(this.ScopePeriodStartMonth));
                    lst.Add(nameof(this.ScopePeriodStartMonthDay));
                    break;
            }
            return lst.ToArray();
        }
    }
}