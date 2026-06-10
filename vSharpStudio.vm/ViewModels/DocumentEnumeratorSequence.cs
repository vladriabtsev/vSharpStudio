using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.ViewModels;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class DocumentEnumeratorSequence : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(" ");
            sb.Append(this.ToString());
        }
        public override string ToString()
        {
            string unique = "";
            var conv = new EnumDescriptionTypeConverter(typeof(EnumMonths));
            var gd = this.ParentGroupListSequences.ParentGroupDocuments;
            switch (this.ScopeOfUnique)
            {
                case EnumDocNumberUniqueScope.DOC_UNIQUE_FOREVER:
                    unique = "Unique";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_CALENDAR_YEAR:
                    unique = $"Unique for every calendar year";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_FISCAL_YEAR:
                    switch (gd.FiscalYearStartMethod)
                    {
                        case EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_MONTH_DAY:
                            unique = $"Unique for fiscal year starting on {gd.FiscalYearStartMonthDay} {conv.ConvertTo(null, null, gd.FiscalYearStartMonth, typeof(string))}";
                            break;
                        case EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_WEEK_DAY_BEFORE_MONTH_DAY:
                            unique = $"Unique for fiscal year startin on '{gd.FiscalYearStartWeekDay.ToString()}' before starting month: {conv.ConvertTo(null, null, gd.FiscalYearStartMonth, typeof(string))} day: {gd.FiscalYearStartMonthDay}";
                            break;
                        case EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_NOT_SELECTED:
                            unique = $"Fiscal year method is not selected";
                            break;
                    }
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_QUATER:
                    unique = $"Unique for every calendar quater";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_MONTH:
                    unique = $"Unique for every calendar month";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_WEEK:
                    unique = $"Unique for every week. Starting week day: {conv.ConvertTo(null, null, this.ScopePeriodStartWeekDay, typeof(string))}";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_DAY:
                    unique = $"Unique for every day";
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
            this._SequenceType = EnumCodeType.Text;
            this._MaxSequenceLength = 9;
            this._Prefix = "";
            /*
            if (this.ParentGroupListSequences.ParentGroupDocuments.FiscalYearStartMethod == EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_MONTH_DAY)
            {

            }
            else if (this.ParentGroupListSequences.ParentGroupDocuments.FiscalYearStartMethod == EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_MONTH_DAY_WEEK_DAY)
            {

            }
            this._ScopeOfUnique = common.EnumDocNumberUniqueScope.DOC_UNIQUE_YEAR;
            */
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
        protected override ConfigNodesCollection<DocumentEnumeratorSequence>? GetParentCollection() { return this.ParentGroupListSequences.ListEnumeratorSequences; }
        public void OnAdded()
        {
            this.AddOrRestoreAllAppGenSettingsVmsToNode();
            //this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            //this.GroupForms.AddAllAppGenSettingsVmsToNode();
            //this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = DocumentEnumeratorSequence.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListSequences.ListEnumeratorSequences.Add(node, this);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new DocumentEnumeratorSequence(this.Parent);
            this.ParentGroupListSequences.ListEnumeratorSequences.Add(node, this);
            this.GetUniqueName(Defaults.SequenceName, node, this.ParentGroupListSequences.ListEnumeratorSequences);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListSequences.ListEnumeratorSequences.Remove(this);
        }
        #endregion Tree operations

        partial void OnScopeOfUniqueChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            switch (this.ScopeOfUnique)
            {
                case EnumDocNumberUniqueScope.DOC_UNIQUE_FOREVER:
                    lst.Add(nameof(this.ScopePeriodStartWeekDay));
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_CALENDAR_YEAR:
                    lst.Add(nameof(this.ScopePeriodStartWeekDay));
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_FISCAL_YEAR:
                    lst.Add(nameof(this.ScopePeriodStartWeekDay));
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_QUATER:
                    lst.Add(nameof(this.ScopePeriodStartWeekDay));
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_MONTH:
                    lst.Add(nameof(this.ScopePeriodStartWeekDay));
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_WEEK:
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_DAY:
                    lst.Add(nameof(this.ScopePeriodStartWeekDay));
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return [.. lst];
        }
    }
}
