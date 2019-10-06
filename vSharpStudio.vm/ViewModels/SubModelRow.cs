using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class SubModelRow
    {
        public SubModelRow(SubModel subModel, string groupName, string name, string guid, bool isIncluded)
        {
            this.SubModel = subModel;
            this._GroupName = groupName;
            this._Name = name;
            this._Guid = guid;
            this._IsIncluded = isIncluded;
        }
        public SubModelRow(SubModel subModel, string groupName, List<SubModelRow> lst, bool isIncluded)
        {
            this.SubModel = subModel;
            this._GroupName = groupName;
            this.ListSubModelRow = lst;
            this._IsIncluded = isIncluded;
        }
        public SubModel SubModel { get; set; }
        public List<SubModelRow> ListSubModelRow;
        partial void OnIsIncludedChanged()
        {
            if (_IsIncluded)
            {
                if (string.IsNullOrEmpty(this.Guid))
                {
                    foreach (var t in ListSubModelRow)
                    {
                        this.SubModel.DicGuids[t.Guid] = null;
                    }
                }
                else
                {
                    this.SubModel.DicGuids[this.Guid] = null;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.Guid))
                {
                    foreach (var t in ListSubModelRow)
                    {
                        if (this.SubModel.DicGuids.ContainsKey(t.Guid))
                            this.SubModel.DicGuids.Remove(t.Guid);
                    }
                }
                else
                {
                    if (this.SubModel.DicGuids.ContainsKey(this.Guid))
                        this.SubModel.DicGuids.Remove(this.Guid);
                }
            }
        }
    }
}
