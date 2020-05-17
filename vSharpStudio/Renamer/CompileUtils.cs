using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Renamer;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.ViewModels
{
    public class CompileUtils
    {
        public static void Compile(ILogger _logger, string solutionPath, CancellationToken cancellationToken)
        {
            //var lstBuilds = Microsoft.Build.Locator.MSBuildLocator.QueryVisualStudioInstances().ToList();
            //var build = lstBuilds[0];
            //Microsoft.Build.Locator.MSBuildLocator.RegisterInstance(build);
            //Microsoft.Build.Locator.VisualStudioInstanceQueryOptions.Default = new Microsoft.Build.Locator.VisualStudioInstanceQueryOptions() { DiscoveryType = Microsoft.Build.Locator.DiscoveryType.DotNetSdk };
            //Microsoft.Build.Locator.MSBuildLocator.RegisterDefaults();
            var properties = new Dictionary<string, string>
                {
                    //{ "Configuration", "Release" },
                    // Use the latest language version to force the full set of available analyzers to run on the project.
                    { "LangVersion", "latest" },
                };
            using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create(properties))
            //https://gist.github.com/DustinCampbell/32cd69d04ea1c08a16ae5c4cd21dd3a3
            //using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
            {
                _logger.LogInformation("Compiling solution {0}".FilePos(), solutionPath);
                Microsoft.CodeAnalysis.Solution solution = workspace.OpenSolutionAsync(solutionPath).Result;
                if (workspace.Diagnostics.Count > 0)
                {
                    var en = workspace.Diagnostics.GetEnumerator();
                    en.MoveNext();
                    if (en.Current.Kind == Microsoft.CodeAnalysis.WorkspaceDiagnosticKind.Failure)
                        throw new Exception(en.Current.Message);
                }
                foreach (var project in solution.Projects)
                {
                    var compilation = project.GetCompilationAsync().Result;
                    var diag = compilation.GetDiagnostics();
                    var lst = from p in diag where p.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error select p;
                    if (lst.Count() > 0)
                        throw new Exception("Compilation errors are found.\nSolution: " + solutionPath + "\nProject: " + project.FilePath);
                }
            }
        }
        public static void Rename(ILogger _logger, string solutionPath, string projectPath, List<PreRenameData> lstRenames, CancellationToken cancellationToken)
        {
            using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
            {
                // Open the solution within the workspace.
                Microsoft.CodeAnalysis.Solution solution = workspace.OpenSolutionAsync(solutionPath).Result;
                bool isProjectFound = false;
                foreach (Microsoft.CodeAnalysis.ProjectId projectId in solution.ProjectIds)
                {
                    // Look up the snapshot for the original project in the latest forked solution.
                    Microsoft.CodeAnalysis.Project project = solution.GetProject(projectId);
                    if (project.FilePath == projectPath)
                    {
                        isProjectFound = true;
                        foreach (Microsoft.CodeAnalysis.DocumentId documentId in project.DocumentIds)
                        {
                            // Look up the snapshot for the original document in the latest forked solution.
                            Microsoft.CodeAnalysis.Document document = solution.GetDocument(documentId);
                            //tg.RelativePathToGeneratedFile

                            // only in 'main' declaration files
                            //TODO implement based on knowledge of generated file ???
                            if (Path.GetDirectoryName(document.FilePath).EndsWith("ViewModels"))
                            {
                                if (Path.GetExtension(document.FilePath) == "cs")
                                {
                                    CodeAnalysisCSharp.Rename(_logger, solution, document, lstRenames, cancellationToken).Wait();
                                }
                                else if (Path.GetExtension(document.FilePath) == "vb")
                                {
                                    CodeAnalysisVisualBasic.Rename(solution, document, lstRenames, cancellationToken).Wait();
                                }
                                else
                                    throw new NotSupportedException();
                            }
                        }
                    }
                }
                if (!isProjectFound)
                    throw new Exception("Project not found");
            }
        }
        //        public async static Task Rename(ILogger _logger, Solution solution, Document document, proto_request request, CancellationToken cancellationToken)
        //        {
        //            _logger.LogInformation("List renames:".FilePos());
        //            foreach (var tr in request.ListRenames)
        //            {
        //                _logger.LogInformation("   Class: {0}".FilePos(), tr.ClassName);
        //                foreach (var tp in tr.ListRenamedProperties)
        //                {
        //                    _logger.LogInformation("      Property: {0} -> {1}".FilePos(), tp.PropName, tp.PropNameNew);
        //                }
        //            }
        //#if DEBUG
        //            foreach (var tr in request.ListRenames)
        //            {
        //                if (string.IsNullOrWhiteSpace(tr.Namespace))
        //                    throw new NotSupportedException("Namespace is empty");
        //                if (string.IsNullOrWhiteSpace(tr.ClassName))
        //                    throw new NotSupportedException("ClassName is empty");
        //                foreach (var tp in tr.ListRenamedProperties)
        //                {
        //                    if (string.IsNullOrWhiteSpace(tp.PropName))
        //                        throw new NotSupportedException("PropName is empty");
        //                    if (string.IsNullOrWhiteSpace(tp.PropNameNew))
        //                        throw new NotSupportedException("PropNameNew is empty");
        //                    if (tp.PropName == tp.PropNameNew)
        //                        throw new NotSupportedException("PropNameNew is equal PropName");
        //                }
        //            }
        //#endif
        //            var root = (CompilationUnitSyntax)document.GetSyntaxRootAsync().Result;
        //            var diag = root.GetDiagnostics().ToList();
        //            if (diag.Count > 0)
        //            {
        //                StringBuilder sb = new StringBuilder();
        //                foreach (var t in diag)
        //                {
        //                    if (t.WarningLevel == 0 && !t.IsWarningAsError)
        //                        sb.AppendLine(t.ToString());
        //                }
        //                if (sb.Length > 0)
        //                    throw new Exception(sb.ToString());
        //            }
        //            //var editor = new SyntaxEditor(root, EmptyWorkspace);
        //            foreach (var nmsp in root.Members)
        //            {
        //                if (!(nmsp is NamespaceDeclarationSyntax))
        //                    continue;
        //                NamespaceDeclarationSyntax ns = (NamespaceDeclarationSyntax)nmsp;
        //                foreach (var t in ((NamespaceDeclarationSyntax)nmsp).Members)
        //                {
        //                    if (!(t is ClassDeclarationSyntax))
        //                        continue;
        //                    var c = (ClassDeclarationSyntax)t;
        //                    foreach (var tt in c.Members)
        //                    {
        //                        if (!(tt is PropertyDeclarationSyntax))
        //                            continue;
        //                        var p = (PropertyDeclarationSyntax)tt;
        //                        foreach (var tr in request.ListRenames)
        //                        {
        //                            //if (tr.Namespace == ns.Externs)
        //                            //{
        //                            if (tr.ClassName == c.Identifier.Text)
        //                            {
        //                                //type.GetMembers().OfType<IPropertySymbol>()
        //                                // rename properties
        //                                foreach (var tp in tr.ListRenamedProperties)
        //                                {
        //                                    if (tp.PropName == p.Identifier.Text)
        //                                    {
        //                                        _logger.LogInformation("Rename Property: {0} -> {1} Class: {2}".FilePos(), tp.PropName, tp.PropNameNew, tr.ClassName);
        //                                        var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
        //                                        var propSymbolOpt = semanticModel.GetDeclaredSymbol(c) as IPropertySymbol;
        //                                        await Microsoft.CodeAnalysis.Rename.Renamer.RenameSymbolAsync(solution, propSymbolOpt, tp.PropNameNew, solution.Options, cancellationToken);
        //                                    }
        //                                }
        //                                // rename classes
        //                                if (tr.ClassName != tr.ClassNameNew)
        //                                {
        //                                    _logger.LogInformation("Rename Class: {0} -> {1}".FilePos(), tr.ClassName, tr.ClassNameNew);
        //                                    var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
        //                                    var propSymbolOpt = semanticModel.GetDeclaredSymbol(c) as INamedTypeSymbol;
        //                                    await Microsoft.CodeAnalysis.Rename.Renamer.RenameSymbolAsync(solution, propSymbolOpt, tr.ClassNameNew, solution.Options, cancellationToken);
        //                                }
        //                            }
        //                            //}
        //                        }
        //                    }
        //                    //foreach (var tr in request.ListRenames)
        //                    //{
        //                    //    if (!string.IsNullOrWhiteSpace(tr.ClassNameBeforeRename))
        //                    //    {
        //                    //        //if (tr.Namespace == ns.Externs)
        //                    //        //{
        //                    //        if (tr.ClassNameBeforeRename == c.Identifier.Text)
        //                    //        {
        //                    //            var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
        //                    //            var classSymbolOpt = semanticModel.GetDeclaredSymbol(c) as INamedTypeSymbol;
        //                    //            await Microsoft.CodeAnalysis.Rename.Renamer.RenameSymbolAsync(solution, classSymbolOpt, tr.ClassNameBeforeRename, solution.Options, cancellationToken);
        //                    //        }
        //                    //        //}
        //                    //    }
        //                    //}
        //                }
        //            }
        //        }
    }
}
