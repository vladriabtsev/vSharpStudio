using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupPropertiesTree
    {
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            DataType t = new DataType();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.DicPropAttrs res = new Proto.Attr.DicPropAttrs();
            //t.PropertyNameAction(p => p.DataTypeEnum, (m) =>
            //{
            //    res[m] = sb.Clear().Category("kuku").ToString();
            //});
            return res;
        }
    }
}
