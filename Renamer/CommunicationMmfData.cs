using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Proto.Renamer;
#if VSHARPSTUDIO
using ViewModelBase;

namespace vSharpStudio.ViewModels
{
    public sealed class RenamerApp : IDisposable
#else
namespace Renamer
{
    public sealed class RenamerApp
#endif
    {
        private static int DELAY_RECHECK = 100; // ms
        private static int REQUEST_MAX_SIZE = 1000;
        private static int RESPONSE_MAX_SIZE = 5000;
        public const string MMF_NAME = "vsharpstudio.mmf";
        private static string MUTEX_NAME = "vsharpstudiomutex";
        private MemoryMappedFile mmf;
        public CancellationToken CancellationToken { get; private set; }
        private Mutex mutex = null;
        //private byte[] IntToByteArray(int number, int bytes)
        //{
        //    if (bytes > 4 || bytes < 0)
        //    {
        //        throw new ArgumentOutOfRangeException("bytes");
        //    }
        //    byte[] result = new byte[bytes];
        //    for (int i = bytes - 1; i >= 0; i--)
        //    {
        //        result[i] = (number >> (8 * i)) & 0xFF;
        //    }
        //    return result;
        //}
        private byte[] IntToByteArray(int number)
        {
            int bytes = 4;
            byte[] result = new byte[bytes];
            for (int i = bytes - 1; i >= 0; i--)
            {
                result[i] = (byte)((number >> (8 * i)) & 0xFF);
            }
            return result;
        }
#if VSHARPSTUDIO
        Process process = null;
        bool isProcessFinished = false;
        public RenamerApp()
        {
            this.mmf = MemoryMappedFile.CreateNew(MMF_NAME, REQUEST_MAX_SIZE + RESPONSE_MAX_SIZE);
            //bool mutexCreated;
            //this.mutex = new Mutex(true, MUTEX_NAME, out mutexCreated);
            this.mutex = new Mutex(false, MUTEX_NAME);
            var rqst = new proto_request() { RequestId = -1 };
            this.WriteRequest(rqst).Wait();
            process = new Process
            {
                StartInfo =
                {
#if DEBUG
                    FileName = Directory.GetCurrentDirectory()+@"\..\Renamer\Renamer.exe", Arguments = "-l Trace",
#else
                    FileName = Directory.GetCurrentDirectory()+@"\..\Renamer\Renamer.exe", Arguments = "",
#endif
                    UseShellExecute = false, CreateNoWindow = true,
                    RedirectStandardOutput = true, RedirectStandardError = true
                },
                EnableRaisingEvents = true
            };
            //process.Exited += (s, ea) =>
            //{
            //    isProcessFinished = true;
            //    if (process.ExitCode != 0)
            //    {
            //        var errorMessage = process.StandardError.ReadToEnd();
            //        throw new Exception("Unexpected exception in Renamer.exe: " + errorMessage);
            //    }
            //};
            bool started = process.Start();
            if (!started)
            {
                //you may allow for the process to be re-used (started = false) 
                //but I'm not sure about the guarantees of the Exited event in such a case
                throw new InvalidOperationException("Could not start process: " + process);
            }

            //process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        async private Task WriteRequest(proto_request request)
        {
            mutex.WaitOne();
            using (MemoryMappedViewStream stream = this.mmf.CreateViewStream(0, REQUEST_MAX_SIZE))
            {
                var data = request.ToByteArray();
                int len = data.Count();
                if (REQUEST_MAX_SIZE < len)
                    throw new Exception("Request length is greater than 'REQUEST_MAX_SIZE'");
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(len);
                await stream.WriteAsync(data, 0, data.Count());
            }
            using (MemoryMappedViewStream stream = this.mmf.CreateViewStream(REQUEST_MAX_SIZE, RESPONSE_MAX_SIZE))
            {
                // Clean response
                proto_response response = new proto_response() { ResponseId = -1 };
                var data = response.ToByteArray();
                int len = data.Count();
                if (RESPONSE_MAX_SIZE < len)
                    throw new Exception("Response length is greater than 'RESPONSE_MAX_SIZE'");
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(len);
                await stream.WriteAsync(data, 0, data.Count());
            }
            mutex.ReleaseMutex();
        }
        private bool ReadResponse()
        {
            proto_response res;
            byte[] data;
            mutex.WaitOne();
            using (MemoryMappedViewStream stream = this.mmf.CreateViewStream(REQUEST_MAX_SIZE, RESPONSE_MAX_SIZE))
            {
                int len;
                BinaryReader reader = new BinaryReader(stream);
                len = reader.ReadInt32();
                data = new byte[len];
                stream.Read(data, 0, len);
            }
            mutex.ReleaseMutex();
            res = proto_response.Parser.ParseFrom(data);
            if (res.ResponseId == this.requestId)
            {
                if (res.IsSuccess)
                    return true;
                if (res.IsFailure)
                    throw new Exception(res.Exception.ExceptionTypeName + ": " + res.Exception.Message + "\n" + res.Exception.StackTrace);
                if (res.IsCancelled)
                    throw new CancellationException();
                throw new Exception();
            }
            return false;
        }
        //async public Task CompileAndRename(string mmfName, CancellationToken cancellationToken, Func<Task> action)
        //{
        //    this.CancellationToken = cancellationToken;
        //    if (this.mutex == null)
        //    {
        //        bool mutexCreated;
        //        this.mutex = new Mutex(false, "MmfMutex", out mutexCreated);
        //        if (!mutexCreated)
        //            throw new Exception("Can't create mutex");
        //    }

        //    // https://stackoverflow.com/questions/528652/what-is-the-simplest-method-of-inter-process-communication-between-2-c-sharp-pro
        //    // https://docs.microsoft.com/en-us/dotnet/standard/io/memory-mapped-files?redirectedfrom=MSDN
        //    // https://csharpvault.com/memory-mapped-files/
        //    // https://stackoverflow.com/questions/528652/what-is-the-simplest-method-of-inter-process-communication-between-2-c-sharp-pro
        //    // https://gist.github.com/VPKSoft/5d78f1c06ec51ebad34817b491fe6ac6
        //    // https://docs.microsoft.com/en-us/dotnet/api/system.runtime.remoting.channels.ipc.ipcchannel?redirectedfrom=MSDN&view=netframework-4.7.2
        //    var request = new Request();

        //    using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", REQUEST_MAX_SIZE + RESPONSE_MAX_SIZE))
        //    {
        //        using (MemoryMappedViewStream stream = mmf.CreateViewStream())
        //        {
        //            BinaryWriter writer = new BinaryWriter(stream);
        //            writer.Write(1);
        //        }
        //        mutex.ReleaseMutex();

        //        Console.WriteLine("Start Process B and press ENTER to continue.");
        //        Console.ReadLine();

        //        Console.WriteLine("Start Process C and press ENTER to continue.");
        //        Console.ReadLine();

        //        mutex.WaitOne();
        //        using (MemoryMappedViewStream stream = mmf.CreateViewStream())
        //        {
        //            BinaryReader reader = new BinaryReader(stream);
        //            Console.WriteLine("Process A says: {0}", reader.ReadBoolean());
        //            Console.WriteLine("Process B says: {0}", reader.ReadBoolean());
        //            Console.WriteLine("Process C says: {0}", reader.ReadBoolean());
        //        }
        //        mutex.ReleaseMutex();
        //    }




        //    using (var mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateOrOpen(mmfName, System.Runtime.InteropServices.Marshal.SizeOf(request)))
        //    {
        //        using (var accessor = mmf.CreateViewAccessor())
        //        {
        //            action().Wait();
        //            while (true)
        //            {
        //                mutex.WaitOne();
        //                accessor.Write(0, ref request);
        //                mutex.ReleaseMutex();


        //                Thread.Sleep(100);
        //            }
        //        }
        //    }
        //}
        private int requestId = 0;
        async public Task Compile(string solutionPath, CancellationToken cancellationToken)
        {
            this.requestId++;
            var rqst = new proto_request() { RequestId = this.requestId, IsCompile = true, SolutionPath = solutionPath };
            this.WriteRequest(rqst).Wait();
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    rqst = new proto_request() { RequestId = this.requestId, IsCancel = true };
                    this.WriteRequest(rqst).Wait();
                }
                await Task.Delay(DELAY_RECHECK);
                if (this.ReadResponse())
                    break;
            }
        }
        async public Task Rename(proto_request request, CancellationToken cancellationToken)
        {
            this.requestId++;
            this.WriteRequest(request).Wait();
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    var rqst = new proto_request() { RequestId = this.requestId, IsCancel = true };
                    this.WriteRequest(rqst).Wait();
                }
                await Task.Delay(DELAY_RECHECK);
                if (this.ReadResponse())
                    break;
            }
        }
        //async public Task Exit()
        //{
        //    this.requestId++;
        //    var rqst = new proto_request() { RequestId = this.requestId, IsExit = true };
        //    this.WriteRequest(rqst);
        //    await Task.Delay(DELAY_RECHECK); // ???
        //}
        public void Dispose()
        {
            this.requestId++;
            var rqst = new proto_request() { RequestId = this.requestId, IsExit = true };
            this.WriteRequest(rqst).Wait();
            mmf.Dispose();
            //if (process != null && !isProcessFinished)
            //    process.Kill();
        }
#else
        CancellationTokenSource cancellationTokenSource;
        ILogger _logger;
        public RenamerApp(ILoggerProvider loggerProvider)
        {
            ILogger _logger = loggerProvider.CreateLogger(this.GetType().Name);
            this.cancellationTokenSource = new CancellationTokenSource();
            CancellationToken = this.cancellationTokenSource.Token;
            this.mmf = MemoryMappedFile.OpenExisting(MMF_NAME);
            mutex = Mutex.OpenExisting(MUTEX_NAME);
        }
        private proto_request ReadRequest()
        {
            proto_request res;
            byte[] data;
            mutex.WaitOne();
            using (MemoryMappedViewStream stream = this.mmf.CreateViewStream(0, REQUEST_MAX_SIZE))
            {
                int len;
                BinaryReader reader = new BinaryReader(stream);
                len = reader.ReadInt32();
                data = new byte[len];
                stream.Read(data, 0, len);
            }
            mutex.ReleaseMutex();
            res = proto_request.Parser.ParseFrom(data);
            return res;
        }
        private void WriteResponse(proto_response response)
        {
            mutex.WaitOne();
            using (MemoryMappedViewStream stream = this.mmf.CreateViewStream(REQUEST_MAX_SIZE, RESPONSE_MAX_SIZE))
            {
                var data = response.ToByteArray();
                int len = data.Count();
                if (RESPONSE_MAX_SIZE < len)
                    throw new Exception("Response length is greater than 'RESPONSE_MAX_SIZE'");
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(len);
                stream.Write(data, 0, data.Count());
            }
            mutex.ReleaseMutex();
        }
        private int requestId;
        public void WaitAndExecuteCommands()
        {
            _logger.LogTrace("WaitAndExecuteCommands".FilePos());
            while (true)
            {
                try
                {
                    var rqwst = this.ReadRequest();
                    if (rqwst.RequestId > 0 && rqwst.RequestId != this.requestId)
                    {
                        this.requestId = rqwst.RequestId;
                        // run in background to have capability cancel
                        if (rqwst.IsCompile)
                        {
                            _logger.LogTrace("Compile request. Id={0}".FilePos(), rqwst.RequestId);
                            Task.Run(() => { Compile(rqwst); });
                        }
                        else if (rqwst.IsRename)
                        {
                            _logger.LogTrace("Rename request. Id={0}".FilePos(), rqwst.RequestId);
                            Task.Run(() => { Rename(rqwst); });
                        }
                        else if (rqwst.IsExit)
                        {
                            _logger.LogTrace("Exit request. Id={0}".FilePos(), rqwst.RequestId);
                            return;
                        }
                        else
                        {
                            throw new Exception();
                        }
                        var res = new proto_response()
                        {
                            ResponseId = this.requestId,
                            IsSuccess = true,
                        };
                        _logger.LogTrace("Success responce. Id={0}".FilePos(), rqwst.RequestId);
                        this.WriteResponse(res);
                    }
                    if (rqwst.IsCancel)
                    {
                        _logger.LogTrace("Cancel request. Id={0}".FilePos(), rqwst.RequestId);
                        this.cancellationTokenSource.Cancel();
                        Task.Delay(5000).Wait();
                        _logger.LogTrace("E X I T".FilePos());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    var res = new proto_response()
                    {
                        ResponseId = this.requestId,
                        IsFailure = true,
                        Exception = new proto_exception()
                        {
                            ExceptionTypeName = ex.GetType().Name,
                            Message = ex.Message,
                            StackTrace = ex.StackTrace
                        }
                    };
                    _logger.LogCritical(ex, "Error. Id={0}".FilePos(), this.requestId);
                    this.WriteResponse(res);
                    return;
                }

                Task.Delay(DELAY_RECHECK).Wait();
            }
        }
        //public void CompileAndRename(string mmfName)
        //{
        //    var request = new Request();
        //    var mutex = new Mutex(false, "MmfMutex");
        //    // https://docs.microsoft.com/en-us/dotnet/standard/io/memory-mapped-files?redirectedfrom=MSDN
        //    using (var mmf = MemoryMappedFile.CreateOrOpen(mmfName, System.Runtime.InteropServices.Marshal.SizeOf(data)))
        //    {
        //        using (var accessor = mmf.CreateViewAccessor())
        //        {
        //            var lstBuilds = Microsoft.Build.Locator.MSBuildLocator.QueryVisualStudioInstances().ToList();
        //            var build = lstBuilds[0];
        //            Microsoft.Build.Locator.MSBuildLocator.RegisterInstance(build);
        //            //Microsoft.Build.Locator.VisualStudioInstanceQueryOptions.Default = new Microsoft.Build.Locator.VisualStudioInstanceQueryOptions() { DiscoveryType = Microsoft.Build.Locator.DiscoveryType.DotNetSdk };
        //            //Microsoft.Build.Locator.MSBuildLocator.RegisterDefaults();
        //            var properties = new Dictionary<string, string>
        //                {
        //                    // Use the latest language version to force the full set of available analyzers to run on the project.
        //                    { "LangVersion", "latest" },
        //                };
        //            using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create(properties))
        //            //https://gist.github.com/DustinCampbell/32cd69d04ea1c08a16ae5c4cd21dd3a3
        //            //using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
        //            {
        //                while (true)
        //                {
        //                    mutex.WaitOne();
        //                    accessor.Read(0, ref request);
        //                    mutex.ReleaseMutex();
        //                    if (request.IsCancel)
        //                        this.cancellationTokenSource.Cancel();

        //                    var res = new Response();

        //                    BinaryWriter writer = new BinaryWriter(stream);

        //                    Thread.Sleep(1000);
        //                }
        //            }
        //        }
        //    }
        //}
        public void Compile(proto_request executeRequest)
        {
            var lstBuilds = Microsoft.Build.Locator.MSBuildLocator.QueryVisualStudioInstances().ToList();
            var build = lstBuilds[0];
            Microsoft.Build.Locator.MSBuildLocator.RegisterInstance(build);
            //Microsoft.Build.Locator.VisualStudioInstanceQueryOptions.Default = new Microsoft.Build.Locator.VisualStudioInstanceQueryOptions() { DiscoveryType = Microsoft.Build.Locator.DiscoveryType.DotNetSdk };
            //Microsoft.Build.Locator.MSBuildLocator.RegisterDefaults();
            var properties = new Dictionary<string, string>
                            {
                            // Use the latest language version to force the full set of available analyzers to run on the project.
                            { "LangVersion", "latest" },
                            };
            using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create(properties))
            //https://gist.github.com/DustinCampbell/32cd69d04ea1c08a16ae5c4cd21dd3a3
            //using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
            {
                _logger.LogInformation("Compiling solution {0}".FilePos(), executeRequest.SolutionPath);
                Microsoft.CodeAnalysis.Solution solution = workspace.OpenSolutionAsync(executeRequest.SolutionPath).Result;
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
                        throw new Exception("Compilation errors are found.\nSolution: " + executeRequest.SolutionPath + "\nProject: " + project.FilePath);
                }
            }
        }
        public void Rename(proto_request executeRequest)
        {
            using (Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create())
            {
                // Open the solution within the workspace.
                Microsoft.CodeAnalysis.Solution solution = workspace.OpenSolutionAsync(executeRequest.SolutionPath).Result;
                bool isProjectFound = false;
                foreach (Microsoft.CodeAnalysis.ProjectId projectId in solution.ProjectIds)
                {
                    // Look up the snapshot for the original project in the latest forked solution.
                    Microsoft.CodeAnalysis.Project project = solution.GetProject(projectId);
                    if (project.FilePath == executeRequest.ProjectPath)
                    {
                        isProjectFound = true;
                        foreach (Microsoft.CodeAnalysis.DocumentId documentId in project.DocumentIds)
                        {
                            // Look up the snapshot for the original document in the latest forked solution.
                            Microsoft.CodeAnalysis.Document document = solution.GetDocument(documentId);
                            //tg.RelativePathToGeneratedFile
                            if (Path.GetDirectoryName(document.FilePath).EndsWith("ViewModels"))
                            {
                                if (Path.GetExtension(document.FilePath) == "cs")
                                {
                                    CodeAnalysisCSharp.Rename(_logger, solution, document, executeRequest, this.CancellationToken).Wait();
                                }
                                else if (Path.GetExtension(document.FilePath) == "vb")
                                {
                                    CodeAnalysisVisualBasic.Rename(solution, document, executeRequest, this.CancellationToken); //.Wait();
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
#endif
                }
}
