using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Linq;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Diagnostics.Contracts;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    //[DebuggerDisplay("RegisterDocToReg: Doc:{RelativeAppProjectPath,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    //[DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class RegisterRegPropToDocProp
    {
        //public string ToDebugString()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append("Mapping Path:");
        //    sb.Append(BranchPath);
        //    sb.Append(" RegProp:");
        //    if (cfg.DicNodes.ContainsKey(RegPropGuid))
        //        if (cfg.DicNodes.ContainsKey(RegPropGuid))
        //        {
        //            var p = cfg.DicNodes[RegPropGuid];
        //            sb.Append(p.Name);
        //            sb.Append('(');
        //            var n = p.Parent;
        //            string g = "";
        //            while (n is not IRegister)
        //            {
        //                n = n.Parent;
        //                if (n == null)
        //                    break;
        //            }
        //            if (n != null)
        //            {
        //                g = n.Guid;
        //                var d = cfg.DicNodes[g];
        //                sb.Append(d.Name);
        //            }
        //            else
        //                sb.Append("<not found>");
        //            sb.Append(')');
        //        }
        //        else
        //            sb.Append("<not found>");
        //    sb.Append(" DocProp:");
        //    if (cfg.DicNodes.ContainsKey(DocPropGuid))
        //    {
        //        var p = cfg.DicNodes[DocPropGuid];
        //        sb.Append(p.Name);
        //        sb.Append('(');
        //        var g = p.Parent?.Parent?.Guid;
        //        if (g != null)
        //        {
        //            var d = cfg.DicNodes[g];
        //            sb.Append(d.Name);
        //        }
        //        else
        //            sb.Append("<not found>");
        //        sb.Append(')');
        //    }
        //    else
        //        sb.Append("<not found>");
        //    return sb.ToString();
        //}
        partial void OnCreated()
        {
            this._Guid = System.Guid.NewGuid().ToString();
        }
    }
}
