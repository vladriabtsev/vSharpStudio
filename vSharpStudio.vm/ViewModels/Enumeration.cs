using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Enumeration:{Name,nq} Type:{Enumeration.GetTypeDesc(this),nq}")]
    public partial class Enumeration : IListNodes<EnumerationPair>
    {
        public static readonly string DefaultName = "Enumeration";
        [BrowsableAttribute(false)]
        public SortedObservableCollection<EnumerationPair> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.ListNodes = this.ListValues;
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
        //public string NodeText { get { return this.Name; } }
        #endregion ITreeNode
    }
}
