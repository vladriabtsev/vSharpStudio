using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace GenTask
{
    // https://docs.microsoft.com/en-us/visualstudio/msbuild/how-to-extend-the-visual-studio-build-process?view=vs-2019
    // https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-task-reference?view=vs-2019
    public class GenTask : ToolTask
    {
        private const string ExeName = "GenFromProto.exe";
        [Required]
        public ITaskItem GenToolPath { get; set; }
        [Required]
        public ITaskItem ProtoFile { get; set; }
        [Required]
        public ITaskItem DestinationFile { get; set; }
        [Required]
        public string NameSpace { get; set; }

        protected override bool SkipTaskExecution()
        {
            //Debugger.Launch();
            var ct = DestinationFile.GetMetadata("ModifiedTime");
            if (string.IsNullOrEmpty(ct))
                return false;
            DateTime ctt = DateTime.Parse(ct);
            var pt = ProtoFile.GetMetadata("ModifiedTime");
            DateTime ptt = DateTime.Parse(pt);
            if (ctt > ptt)
                return true;
            return false;
        }
        protected override string ToolName => ExeName;
        protected override string GenerateFullPathToTool()
        {
            return this.GenToolPath.GetMetadata("FullPath");
        }
        protected override bool ValidateParameters()
        {
            base.Log.LogMessageFromText("Validating arguments", MessageImportance.Low);
            //base.Log.LogMessageFromText("*** Start View Model generation from Proto Reflection", MessageImportance.High);
            if (!File.Exists(GenerateFullPathToTool()))
            {
                string message = string.Format("Missing Generator: {0}", GenToolPath);
                base.Log.LogError(message, null);
                return false;
            }
            return base.ValidateParameters();
        }
        protected override string GenerateCommandLineCommands()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GenToolPath);
            sb.Append(" ");
            sb.Append(this.ProtoFile.GetMetadata("Filename"));
            sb.Append(" ");
            sb.Append(this.DestinationFile.GetMetadata("FullPath"));
            sb.Append(" ");
            sb.Append(this.NameSpace);

            string exec = sb.ToString();
            base.Log.LogMessageFromText(exec, MessageImportance.Low);
            return exec;
        }
        //public override bool Execute()
        //{
        //    TaskLoggingHelper loggingHelper = new TaskLoggingHelper(this);
        //    try
        //    {
        //        int count = ProtoFile.Count();
        //        for (int i = 0; i < count; i++)
        //        {
        //            Generate(ProtoFile[i], DestinationFile[i], loggingHelper);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loggingHelper.LogErrorFromException(ex);
        //        return false;
        //    }
        //    return true;
        //}

        //private void Generate(ITaskItem source, ITaskItem dest, TaskLoggingHelper logger)
        //{
        //    string filename = source.GetMetadata("Filename");
        //    if (string.IsNullOrWhiteSpace(filename))
        //    {
        //        logger.LogError("Can't get 'Filename' metadata");
        //        throw new ArgumentException();
        //    }
        //    string reflectionClass = filename.ToNameCs() + "Reflection";

        //    Type reflection = typeof(Proto.Config.VsharpstudioReflection).Assembly.GetType("Proto.Config.Connection." + reflectionClass);
        //    var descr = reflection.GetProperty("Descriptor", BindingFlags.Public | BindingFlags.Static);
        //    object value = descr.GetValue(null, null);
        //    FileDescriptor typedValue = (FileDescriptor)value;

        //    NameSpace ns = new NameSpace(typedValue);
        //    string res = ns.TransformText();

        //    string filedest = dest.GetMetadata("FullPath");
        //    logger.LogMessage("Output file: " + filedest);
        //    if (!File.Exists(filedest))
        //    {
        //        File.CreateText(filedest);
        //    }
        //    using (var fs = File.Open(filedest, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write, FileShare.Write))
        //    {
        //        var bytes = Encoding.UTF8.GetBytes(res);
        //        fs.Write(bytes, 0, bytes.Count());
        //    }
        //}
    }
}
