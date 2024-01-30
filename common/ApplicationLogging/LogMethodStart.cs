using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogging
{
    internal class LogMethodStart
    {
        public static string GetMethodStartString()
        {
            var sb = new StringBuilder();
            var method = new StackTrace().GetFrame(2).GetMethod();
            //sb.Append("Method is starting. Class = ");
            //sb.Append(method.DeclaringType.FullName);
            //sb.Append("Method = ");
            sb.Append("Method is starting. ");
            sb.Append(method.Name);
            sb.Append("(");
            var sep = "";
            for (int i = 0; i < method.GetParameters().Length; i++)
            {
                sb.Append(sep);
                sb.Append(method.GetParameters().GetValue(i));
                sep = ", ";
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
