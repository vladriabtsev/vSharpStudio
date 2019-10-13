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
    public partial class ModelRow
    {
        public ModelRow(Model subModel, string groupName, string name, string guid, bool isIncluded)
        {
            this.SubModel = subModel;
            this._GroupName = groupName;
            this._Name = name;
            this._Guid = guid;
            this._IsIncluded = isIncluded;
        }
        public ModelRow(Model subModel, string groupName, List<ModelRow> lst, bool isIncluded)
        {
            this.SubModel = subModel;
            this._GroupName = groupName;
            this.ListSubModelRow = lst;
            this._IsIncluded = isIncluded;
        }
        public Model SubModel { get; set; }
        public List<ModelRow> ListSubModelRow;
        partial void OnIsIncludedChanged()
        {
            if (_IsIncluded)
            {
                if (string.IsNullOrEmpty(this.Guid))
                {
                    foreach (var t in ListSubModelRow)
                    {
                        this.SubModel.DicInclusionRecordObjectGuids[t.Guid] = null;
                    }
                }
                else
                {
                    this.SubModel.DicInclusionRecordObjectGuids[this.Guid] = null;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.Guid))
                {
                    foreach (var t in ListSubModelRow)
                    {
                        if (this.SubModel.DicInclusionRecordObjectGuids.ContainsKey(t.Guid))
                            this.SubModel.DicInclusionRecordObjectGuids.Remove(t.Guid);
                    }
                }
                else
                {
                    if (this.SubModel.DicInclusionRecordObjectGuids.ContainsKey(this.Guid))
                        this.SubModel.DicInclusionRecordObjectGuids.Remove(this.Guid);
                }
            }
        }
    }
}
