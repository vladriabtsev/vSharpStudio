using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol.contenttemplateselector?view=netcore-3.1
    public class NodeIconTemplateSelector : DataTemplateSelector
    {
        //public override DataTemplate SelectTemplate(object item, DependencyObject container)
        //{
        //    DataTemplate dt = null;
        //    // Null value can be passed by IDE designer
        //    if (item == null) return null;
        //    var type = item.GetType().Name;
        //    switch (type)
        //    {
        //        case "Property":
        //            return this.fin
        //            break;
        //        default:
        //            break;
        //    }
        //    return dt;
        //}
    }
}
