using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Threading;
using System.Threading.Tasks;
//using Proto.Renamer;
using vSharpStudio.common.DiffModel;
using System.Diagnostics;

namespace Renamer
{
    public class CodeAnalysisVisualBasic
    {
        public async static Task RenameAsync(Solution solution, Document document, List<PreRenameData> lstRenames, CancellationToken cancellationToken)
        {
            var syntaxNode = await document.GetSyntaxRootAsync();
            Debug.Assert(syntaxNode != null);
            var root = (CompilationUnitSyntax)syntaxNode;
            var diag = root.GetDiagnostics().ToList();
            if (diag.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var t in diag)
                {
                    if (t.WarningLevel == 0 && !t.IsWarningAsError)
                        sb.AppendLine(t.ToString());
                }
                if (sb.Length > 0)
                    throw new Exception(sb.ToString());
            }
            //var editor = new SyntaxEditor(root, EmptyWorkspace);
            throw new NotImplementedException();
            //foreach (var nmsp in root.Members)
            //{
            //    if (!(nmsp is NamespaceDeclarationSyntax))
            //        continue;
            //    foreach (var t in ((NamespaceDeclarationSyntax)nmsp).Members)
            //    {
            //        if (!(t is ClassDeclarationSyntax))
            //            continue;
            //        var c = (ClassDeclarationSyntax)t;
            //        foreach (var tt in c.Members)
            //        {
            //            if (!(tt is PropertyDeclarationSyntax))
            //                continue;
            //            var p = (PropertyDeclarationSyntax)tt;
            //        }
            //    }
            //}
        }
    }
}
