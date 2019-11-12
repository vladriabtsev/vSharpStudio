using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.ViewModels
{
    public static class Utils
    {
        public static void TryCall(
            Action action,
            string onErrorMessage,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(onErrorMessage);
                sb.Append("Dir: ");
                sb.AppendLine(Path.GetDirectoryName(sourceFilePath));
                sb.Append("File: ");
                sb.Append(Path.GetFileName(sourceFilePath));
                sb.Append(" Method: ");
                sb.Append(memberName);
                sb.Append(" Line: ");
                sb.AppendLine(sourceLineNumber.ToString());
                sb.AppendLine(ex.ToString());
                MessageBox.Show(sb.ToString(), "Error");
            }
        }
    }
}
