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
