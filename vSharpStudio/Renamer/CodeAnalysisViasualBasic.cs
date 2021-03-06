﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Threading;
using System.Threading.Tasks;
using Proto.Renamer;
using vSharpStudio.common.DiffModel;

namespace Renamer
{
    public class CodeAnalysisVisualBasic
    {
        public async static Task Rename(Solution solution, Document document, List<PreRenameData> lstRenames, CancellationToken cancellationToken)
        {
            var root = (CompilationUnitSyntax)document.GetSyntaxRootAsync().Result;
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
