using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public interface ILayoutParameters
    {
        bool IsStartNewRow { get; }
        string TabName { get; }
        bool IsStartNewTabControl { get; }
        bool IsStopTabControl { get; }
    }
    public interface ILayoutFieldParameters : ILayoutParameters
    {
        bool IsTryAttach { get; }

        bool IsPKey { get; }
        bool IsRefParent { get; }
        bool IsNullable { get; }
        //IDataType DataType { get; }
        string NameUi { get; }
        string Description { get; }
        int LinesOnScreen { get; }
    }
}
