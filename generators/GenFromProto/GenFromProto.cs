using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Google.Protobuf.Reflection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace GenFromProto
{
    // https://docs.microsoft.com/en-us/visualstudio/msbuild/how-to-extend-the-visual-studio-build-process?view=vs-2019
    // https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-task-reference?view=vs-2019
    public class GenFromProto : AppDomainIsolatedTask
    {
        public override bool Execute()
        {
            TaskLoggingHelper loggingHelper = new TaskLoggingHelper(this);
            try
            {
                loggingHelper.LogMessageFromText("*** Start View Model generation from Proto Reflection", MessageImportance.High);
                if (SourceFiles.Count() != DestinationFiles.Count())
                {
                    loggingHelper.LogError("Expected SourceFiles.Count == DestinationFiles.Count");
                    return false;
                }
                int count = SourceFiles.Count();
                if (count == 0)
                {
                    loggingHelper.LogError("Expected SourceFiles.Count > 0");
                    return false;
                }
                for (int i = 0; i < count; i++)
                {
                    Generate(SourceFiles[i], DestinationFiles[i], loggingHelper);
                }
            }
            catch (Exception ex)
            {
                loggingHelper.LogErrorFromException(ex);
                return false;
            }
            return true;
        }
        private const string ExeName = "GenFromProto.exe";
        [Required]
        public ITaskItem[] SourceFiles { get; set; }
        [Required]
        public ITaskItem[] DestinationFiles { get; set; }

        private void Generate(ITaskItem source, ITaskItem dest, TaskLoggingHelper logger)
        {
            string filename = source.GetMetadata("Filename");
            if (string.IsNullOrWhiteSpace(filename))
            {
                logger.LogError("Can't get 'Filename' metadata");
                throw new ArgumentException();
            }
            string reflectionClass = filename.ToNameCs() + "Reflection";

            Type reflection = typeof(Proto.Config.VsharpstudioReflection).Assembly.GetType("Proto.Config.Connection." + reflectionClass);
            var descr = reflection.GetProperty("Descriptor", BindingFlags.Public | BindingFlags.Static);
            object value = descr.GetValue(null, null);
            FileDescriptor typedValue = (FileDescriptor)value;

            NameSpace ns = new NameSpace(typedValue);
            string res = ns.TransformText();

            string filedest = dest.GetMetadata("FullPath");
            logger.LogMessage("Output file: "+ filedest);
            if (!File.Exists(filedest))
            {
                File.CreateText(filedest);
            }
            using (var fs = File.Open(filedest, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write, FileShare.Write))
            {
                var bytes = Encoding.UTF8.GetBytes(res);
                fs.Write(bytes, 0, bytes.Count());
            }
        }
    }
}
