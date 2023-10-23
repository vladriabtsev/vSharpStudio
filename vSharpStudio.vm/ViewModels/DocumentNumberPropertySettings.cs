using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;
using vSharpStudio.common.ViewModels;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class DocumentNumberPropertySettings : IParent
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
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                switch (this.SequenceType)
                {
                    case EnumCodeType.Number:
                        return $"Number{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    case EnumCodeType.Text:
                        return $"Text{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    default:
                        throw new NotImplementedException();
                }
            }
            return $"{this.ParentDocument.Cfg.DicNodes[this.SequenceGuid].Name}-{unique}";
        }
        [Browsable(false)]
        public Document ParentDocument { get { Debug.Assert(this.Parent != null); return (Document)this.Parent; } }
        partial void OnCreated()
        {
            this._ScopePeriodStartMonth = EnumMonths.MONTH_JANUARY;
            this._ScopePeriodStartMonthDay = 1;
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) =>
        //    {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () =>
        //    {
        //        this.OnRemoveChild();
        //    };
        //}
        //protected override void OnIsChangedChanged()
        //{
        //    if (this.Parent != null && this.IsChanged)
        //        this.Parent.IsChanged = true;
        //}
        partial void OnMaxSequenceLengthChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnPrefixChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceTypeChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnScopeOfUniqueChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnScopePeriodStartMonthChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnScopePeriodStartMonthDayChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceGuidChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (!string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                lst.Add(nameof(this.SequenceType));
                lst.Add(nameof(this.MaxSequenceLength));
                lst.Add(nameof(this.Prefix));
            }
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
