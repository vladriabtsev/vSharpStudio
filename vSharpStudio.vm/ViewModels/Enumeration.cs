﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Enumeration:{Name,nq} Type:{Enumeration.GetTypeDesc(this),nq}")]
    public partial class Enumeration : ConfigObjectBase<Enumeration, Enumeration.EnumerationValidator>, IComparable<Enumeration>
    {
        partial void OnInit()
        {
            this.SubNodes.AddRange(this.ListValues);
        }
        public void OnInitFromDto()
        {
        }
        public static string GetTypeDesc(Enumeration p)
        {
            string res = Enum.GetName(typeof(Proto.Config.proto_data_type.Types.EnumDataType), (int)p.DataTypeEnum);
            //switch (p.DataTypeEnum)
            //{
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.Integer:
            //        break;
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.String:
            //        break;
            //}
            return res;
        }

        #region ITreeNode
        #region status icon
        public string StatusIcon
        {
            get
            {
                string iconName = null;
                if (this.IsExpanded)
                {
                    if (this.CountErrors > 0)
                        iconName = "iconFolderOpenError";
                    else
                    {
                        if (this.CountWarnings > 0)
                            iconName = "iconFolderOpenWarning";
                        else
                        {
                            if (this.CountInfos > 0)
                                iconName = "iconFolderOpenInformation";
                            else
                                iconName = "iconFolderOpen";
                        }
                    }
                }
                else
                {
                    if (this.CountErrors > 0)
                        iconName = "iconFolderError";
                    else
                    {
                        if (this.CountWarnings > 0)
                            iconName = "iconFolderWarning";
                        else
                        {
                            if (this.CountInfos > 0)
                                iconName = "iconFolderInformation";
                            else
                                iconName = "iconFolder";
                        }
                    }
                }
                return iconName;
            }
        }
        protected override void OnCountErrorsChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        protected override void OnCountWarningsChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        protected override void OnCountInfosChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        #endregion status icon
        //public string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanMoveUp()
        {
            return (this.Parent as Enumerations).ListEnumerations.IndexOf(this) > 0;
        }
        protected override void OnNodeMoveUp()
        {
            var p = this.Parent as Enumerations;
            var i = p.ListEnumerations.IndexOf(this);
            if (i > 0)
            {
                this.SortingValue = p.ListEnumerations[i - 1].SortingValue - 1;
            }
        }
        protected override bool OnNodeCanMoveDown()
        {
            return (this.Parent as Enumerations).ListEnumerations.IndexOf(this) < ((this.Parent as Enumerations).ListEnumerations.Count - 1);
        }
        protected override void OnNodeMoveDown()
        {
            var p = this.Parent as Enumerations;
            var i = p.ListEnumerations.IndexOf(this);
            if (i < p.ListEnumerations.Count - 1)
            {
                this.SortingValue = p.ListEnumerations[i + 1].SortingValue + 1;
            }
        }
        protected override void OnNodeRemove()
        {
            (this.Parent as Enumerations).ListEnumerations.Remove(this);
        }
        protected override ITreeConfigNode OnNodeAddNew()
        {
            var res = new Enumeration();
            (this.Parent as Enumerations).ListEnumerations.Add(res);
            GetUniqueName("Enumeration", res, (this.Parent as Enumerations).ListEnumerations);
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }
        protected override ITreeConfigNode OnNodeAddClone()
        {
            var res = Enumeration.Clone(this.Parent, this, true, true);
            (this.Parent as Enumerations).ListEnumerations.Add(res);
            this.Name = this.Name + "2";
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }

        #endregion ITreeNode
    }
}
