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
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            Enumeration t = new Enumeration();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.DicPropAttrs res = new Proto.Attr.DicPropAttrs();
            t.PropertyNameAction(p => p.NameUi, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(2).ToString();
            });
            t.PropertyNameAction(p => p.Description, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(3).ToString();
            });
            t.PropertyNameAction(p => p.DataTypeEnum, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(4).ToString();
            });
            return res;
        }
    }
}
