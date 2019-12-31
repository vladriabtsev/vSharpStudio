using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using System.Threading;
using System.Threading.Tasks;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.ViewModels
{
    public class CodeAnalysisCSharp
    {
        public async static Task Rename(Solution solution, Document document, List<PreRenameData> lstRenames, CancellationToken cancellationToken)
        {
#if DEBUG
            foreach (var tr in lstRenames)
            {
                if (string.IsNullOrWhiteSpace(tr.Namespace))
                    throw new NotSupportedException("Namespace is empty");
                if (string.IsNullOrWhiteSpace(tr.ClassNameBeforeRename))
                    throw new NotSupportedException("ClassName is empty");
                foreach (var tp in tr.ListRenamedProperties)
                {
                    if (string.IsNullOrWhiteSpace(tp.PropName))
                        throw new NotSupportedException("PropName is empty");
                    if (string.IsNullOrWhiteSpace(tp.PropNameNew))
                        throw new NotSupportedException("PropNameNew is empty");
                    if (tp.PropName == tp.PropNameNew)
                        throw new NotSupportedException("PropNameNew is equal PropName");
                }
            }
#endif
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
            foreach (var nmsp in root.Members)
            {
                if (!(nmsp is NamespaceDeclarationSyntax))
                    continue;
                NamespaceDeclarationSyntax ns = (NamespaceDeclarationSyntax)nmsp;
                foreach (var t in ((NamespaceDeclarationSyntax)nmsp).Members)
                {
                    if (!(t is ClassDeclarationSyntax))
                        continue;
                    var c = (ClassDeclarationSyntax)t;
                    foreach (var tt in c.Members)
                    {
                        if (!(tt is PropertyDeclarationSyntax))
                            continue;
                        var p = (PropertyDeclarationSyntax)tt;
                        foreach (var tr in lstRenames)
                        {
                            //if (tr.Namespace == ns.Externs)
                            //{
                            if (tr.ClassNameBeforeRename == c.Identifier.Text)
                            {
                                foreach (var tp in tr.ListRenamedProperties)
                                {
                                    if (tp.PropName == p.Identifier.Text)
                                    {
                                        var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
                                        var propSymbolOpt = semanticModel.GetDeclaredSymbol(c) as IPropertySymbol;
                                        await Renamer.RenameSymbolAsync(solution, propSymbolOpt, tp.PropNameNew, solution.Options, cancellationToken);
                                    }
                                }
                            }
                            //}
                        }
                    }
                    foreach (var tr in lstRenames)
                    {
                        if (!string.IsNullOrWhiteSpace(tr.ClassNameBeforeRename))
                        {
                            //if (tr.Namespace == ns.Externs)
                            //{
                            if (tr.ClassNameBeforeRename == c.Identifier.Text)
                            {
                                var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
                                var classSymbolOpt = semanticModel.GetDeclaredSymbol(c) as INamedTypeSymbol;
                                await Renamer.RenameSymbolAsync(solution, classSymbolOpt, tr.ClassNameBeforeRename, solution.Options, cancellationToken);
                            }
                            //}
                        }
                    }
                }
            }
        }
    }
}
