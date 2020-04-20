using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using System.Threading;
using System.Threading.Tasks;
using Proto.Renamer;
using Microsoft.Extensions.Logging;

namespace Renamer
{
    public class CodeAnalysisCSharp
    {
        public async static Task Rename(ILogger _logger, Solution solution, Document document, proto_request request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("List renames:".FilePos());
            foreach (var tr in request.ListRenames)
            {
                _logger.LogInformation("   Class: {0}".FilePos(), tr.ClassName);
                foreach (var tp in tr.ListRenamedProperties)
                {
                    _logger.LogInformation("      Property: {0} -> {1}".FilePos(), tp.PropName, tp.PropNameNew);
                }
            }
#if DEBUG
            foreach (var tr in request.ListRenames)
            {
                if (string.IsNullOrWhiteSpace(tr.Namespace))
                    throw new NotSupportedException("Namespace is empty");
                if (string.IsNullOrWhiteSpace(tr.ClassName))
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
                        foreach (var tr in request.ListRenames)
                        {
                            //if (tr.Namespace == ns.Externs)
                            //{
                            if (tr.ClassName == c.Identifier.Text)
                            {
                                //type.GetMembers().OfType<IPropertySymbol>()
                                // rename properties
                                foreach (var tp in tr.ListRenamedProperties)
                                {
                                    if (tp.PropName == p.Identifier.Text)
                                    {
                                        _logger.LogInformation("Rename Property: {0} -> {1} Class: {2}".FilePos(), tp.PropName, tp.PropNameNew, tr.ClassName);
                                        var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
                                        var propSymbolOpt = semanticModel.GetDeclaredSymbol(c) as IPropertySymbol;
                                        await Microsoft.CodeAnalysis.Rename.Renamer.RenameSymbolAsync(solution, propSymbolOpt, tp.PropNameNew, solution.Options, cancellationToken);
                                    }
                                }
                                // rename classes
                                if (tr.ClassName != tr.ClassNameNew)
                                {
                                    _logger.LogInformation("Rename Class: {0} -> {1}".FilePos(), tr.ClassName, tr.ClassNameNew);
                                    var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
                                    var propSymbolOpt = semanticModel.GetDeclaredSymbol(c) as INamedTypeSymbol;
                                    await Microsoft.CodeAnalysis.Rename.Renamer.RenameSymbolAsync(solution, propSymbolOpt, tr.ClassNameNew, solution.Options, cancellationToken);
                                }
                            }
                            //}
                        }
                    }
                    //foreach (var tr in request.ListRenames)
                    //{
                    //    if (!string.IsNullOrWhiteSpace(tr.ClassNameBeforeRename))
                    //    {
                    //        //if (tr.Namespace == ns.Externs)
                    //        //{
                    //        if (tr.ClassNameBeforeRename == c.Identifier.Text)
                    //        {
                    //            var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
                    //            var classSymbolOpt = semanticModel.GetDeclaredSymbol(c) as INamedTypeSymbol;
                    //            await Microsoft.CodeAnalysis.Rename.Renamer.RenameSymbolAsync(solution, classSymbolOpt, tr.ClassNameBeforeRename, solution.Options, cancellationToken);
                    //        }
                    //        //}
                    //    }
                    //}
                }
            }
        }
    }
}
