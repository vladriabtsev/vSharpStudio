using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.MemoryMappedFiles;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Serilog;
using System.Runtime.CompilerServices;
using Grpc.Core;
using Proto.Renamer;

namespace Renamer
{
    class Program
    {
        // https://github.com/natemcmaster/CommandLineUtils
        // https://github.com/commandlineparser/commandline
        // https://github.com/dotnet/command-line-api
        // https://github.com/fclp/fluent-command-line-parser
        // https://github.com/jlevy/the-art-of-command-line


        [Option(Description = "Port number for communication. Default: 7171")]
        public int Port { get; set; }
        [Option(Description = "Folder for log files")]
        public string FolderLog { get; set; }
        [Option(Description = "Log level: Trace, Debug, Information, Warning, Error, Critical. Default: Error")]
        public string LevelLog { get; set; }
        [Option(Description = "Memory mapped file name for data exchange. Default: " + RenamerApp.MMF_NAME)]
        public string MmfName { get; set; }
        // https://natemcmaster.github.io/CommandLineUtils/
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);
        private ILoggerProvider loggerProvider = null;
        public void OnExecute()
        {
            if (Port==0)
                Port = 7171;
            if (string.IsNullOrEmpty(this.FolderLog))
                this.FolderLog = Directory.GetCurrentDirectory();
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            var conf = new LoggerConfiguration();
            if (LevelLog != null)
            {
                switch (LevelLog)
                {
                    case "Trace":
                        conf.MinimumLevel.Verbose();
                        break;
                    case "Debug":
                        conf.MinimumLevel.Debug();
                        break;
                    case "Information":
                        conf.MinimumLevel.Information();
                        break;
                    case "Warning":
                        conf.MinimumLevel.Warning();
                        break;
                    case "Error":
                        conf.MinimumLevel.Error();
                        break;
                    case "Critical":
                        conf.MinimumLevel.Fatal();
                        break;
                    default:
                        throw new ArithmeticException("Unexpected log level: " + LevelLog);
                }
            }
            else
            {
                conf.MinimumLevel.Error();
            }
            Log.Logger = conf.WriteTo.File(this.FolderLog + @"\log.txt", rollingInterval: Serilog.RollingInterval.Day)
                        // .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                        .CreateLogger();
            loggerProvider = new Serilog.Extensions.Logging.SerilogLoggerProvider(Serilog.Log.Logger);

            Microsoft.Extensions.Logging.ILogger _logger = loggerProvider.CreateLogger(this.GetType().Name);
            Server server = null;
            try
            {
                _logger.LogTrace("Renamer started with parameters. FolderLog={0}, LevelLog={1}, MmfName={2}".FilePos(),
                    FolderLog, LevelLog, MmfName);
#if GRPC
                //var features = RouteGuideUtil.ParseFeatures(RouteGuideUtil.DefaultFeaturesFile);
                server = new Server
                {
                    //Services = { RenamerService.BindService(new RenamerServiceImpl(features)) },
                    Services = { RenamerService.BindService(new RenamerServiceImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };
                server.Start();
                _logger.LogTrace(("RouteGuide server listening on port " + Port).FilePos());

                while (!RenamerServiceImpl.IsStopServer)
                {
                    Task.Delay(1000);
                }

#else
                var cr = new RenamerApp(loggerProvider);
                _logger.LogTrace("cr.WaitAndExecuteCommands()".FilePos());
                cr.WaitAndExecuteCommands();
#endif
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "".FilePos());
            }
#if GRPC
            server?.ShutdownAsync().Wait();
#endif
            _logger.LogTrace("D O N E".FilePos());
        }
    }
}






//static void Main(string[] args)
//{
//    var data = new SharedData
//    {
//        Id = 1,
//        Value = 0
//    };

//    // II. Build all solutions. Exception if not compilible (no need for UNDO)
//    #region
//    progress.SubName = "Check current code compilation";
//    onProgress(progress);

//    var lstBuilds = Microsoft.Build.Locator.MSBuildLocator.QueryVisualStudioInstances().ToList();
//    var build = lstBuilds[0];
//    Microsoft.Build.Locator.MSBuildLocator.RegisterInstance(build);
//    //Microsoft.Build.Locator.VisualStudioInstanceQueryOptions.Default = new Microsoft.Build.Locator.VisualStudioInstanceQueryOptions() { DiscoveryType = Microsoft.Build.Locator.DiscoveryType.DotNetSdk };
//    //Microsoft.Build.Locator.MSBuildLocator.RegisterDefaults();
//    var properties = new Dictionary<string, string>
//                {
//                    // Use the latest language version to force the full set of available analyzers to run on the project.
//                    { "LangVersion", "latest" },
//                };
//    using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create(properties))
//    //https://gist.github.com/DustinCampbell/32cd69d04ea1c08a16ae5c4cd21dd3a3
//    //using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
//    {
//        int i = 0;
//        foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
//        {
//            if (cancellationToken.IsCancellationRequested)
//                throw new CancellationException();
//            Microsoft.CodeAnalysis.Solution solution = await workspace.OpenSolutionAsync(ts.GetCombinedPath(ts.RelativeAppSolutionPath));
//            if (workspace.Diagnostics.Count > 0)
//            {
//                var en = workspace.Diagnostics.GetEnumerator();
//                en.MoveNext();
//                if (en.Current.Kind == Microsoft.CodeAnalysis.WorkspaceDiagnosticKind.Failure)
//                    throw new Exception(en.Current.Message);
//            }
//            foreach (var project in solution.Projects)
//            {
//                var compilation = await project.GetCompilationAsync();
//                var diag = compilation.GetDiagnostics();
//                var lst = from p in diag where p.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error select p;
//                if (lst.Count() > 0)
//                    throw new Exception("Compilation errors are found.\nSolution: " + ts.RelativeAppSolutionPath + "\nProject: " + project.FilePath);
//            }
//            i++;
//            progress.SubProgress = 100 * i / this.Config.GroupAppSolutions.ListAppSolutions.Count;
//            onProgress(progress);
//        }
//    }
//    if (tst != null && tst.IsThrowExceptionOnBuildValidated)
//        throw new Exception();
//    #endregion

//    // III. Rename objects and properties by solution (code can be not compilible after that) (need UNDO from zip code backup)
//    #region
//    var mvr = new ModelVisitorForRenamer();
//    mvr.RunThroughConfig(this.Config, this.Config.PrevCurrentConfig, this.Config.OldStableConfig);
//    using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
//    {
//        foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
//        {
//            if (cancellationToken.IsCancellationRequested)
//                throw new CancellationException();
//            // Open the solution within the workspace.
//            Microsoft.CodeAnalysis.Solution solution = await workspace.OpenSolutionAsync(ts.GetCombinedPath(ts.RelativeAppSolutionPath));
//            foreach (var tp in ts.ListAppProjects)
//            {
//                bool isProjectFound = false;
//                foreach (Microsoft.CodeAnalysis.ProjectId projectId in solution.ProjectIds)
//                {
//                    if (cancellationToken.IsCancellationRequested)
//                        throw new CancellationException();
//                    // Look up the snapshot for the original project in the latest forked solution.
//                    Microsoft.CodeAnalysis.Project project = solution.GetProject(projectId);
//                    if (project.FilePath == tp.GetCombinedPath(tp.RelativeAppProjectPath))
//                    {
//                        isProjectFound = true;
//                        foreach (var tg in tp.ListAppProjectGenerators)
//                        {
//                            var generator = this._Config.DicGenerators[tg.PluginGeneratorGuid];
//                            List<common.DiffModel.PreRenameData> lstRenames = generator.GetListPreRename(mvr.DiffAnnotatedConfig, mvr.ListGuidsRenamedObjects);
//                            foreach (Microsoft.CodeAnalysis.DocumentId documentId in project.DocumentIds)
//                            {
//                                // Look up the snapshot for the original document in the latest forked solution.
//                                Microsoft.CodeAnalysis.Document document = solution.GetDocument(documentId);
//                                //tg.RelativePathToGeneratedFile
//                                if (Path.GetDirectoryName(document.FilePath).EndsWith("ViewModels"))
//                                {
//                                    if (Path.GetExtension(document.FilePath) == "cs")
//                                    {
//                                        await CodeAnalysisCSharp.Rename(solution, document, lstRenames, cancellationToken);
//                                    }
//                                    else if (Path.GetExtension(document.FilePath) == "vb")
//                                    {
//                                        CodeAnalysisVisualBasic.Rename(solution, document, lstRenames, cancellationToken);
//                                    }
//                                    else
//                                        throw new NotSupportedException();
//                                }
//                            }
//                        }
//                    }
//                }
//                if (!isProjectFound)
//                    throw new Exception("Project not found");
//            }
//        }
//        if (tst != null && tst.IsThrowExceptionOnRenamed)
//            throw new Exception();
//    }
//    #endregion
//}
