using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Dummy;
using FluentValidation;
using ViewModelBase;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    [DebuggerDisplay("IdDbGenerator:{DataType.GetTypeDesc(this),nq}")]
    public partial class IdDbGenerator
    {
        partial void OnInit()
        {
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }

        #region ITreeNode
        //        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode

        public static Proto.Attr.ClassData GetDicPropertyAttributes()
        {
            DataType t = new DataType();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.ClassData res = new Proto.Attr.ClassData();
            res.BaseClass= "ViewModelValidatableWithSeverity<IdDbGenerator, IdDbGenerator.DataTypeValidator>";
            //t.PropertyNameAction(p => p.DataTypeEnum, (m) =>
            //{
            //    res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(2).ToString();
            //});
            //t.PropertyNameAction(p => p.Length, (m) =>
            //{
            //    res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(3).ToString();
            //});
            //t.PropertyNameAction(p => p.Accuracy, (m) =>
            //{
            //    res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(4).ToString();
            //});
            //t.PropertyNameAction(p => p.IsPositive, (m) =>
            //{
            //    res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(5).ToString();
            //});
            //t.PropertyNameAction(p => p.ObjectName, (m) =>
            //{
            //    res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(6).ToString();
            //});
            return res;
        }
    }
}
