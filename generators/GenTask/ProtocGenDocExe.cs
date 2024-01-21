using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;
using Microsoft.Win32;

namespace GenTask
{
    public class ProtocGenDocExe : ToolTask
    {
        private string ExeName = "protoc.exe";
        public ProtocGenDocExe()
        {
        }
        [Required]
        public string ProtocFullPath { get; set; }
        [Required]
        public string Protobuf_StandardImportsPath { get; set; }
        [Required]
        public string ProtocGenDocFullPath { get; set; }
        [Required]
        public ITaskItem ProtoFile { get; set; }
        [Required]
        public string ProtocGenDocOutputDir { get; set; }
        [Required]
        public string GenDocType { get; set; }
        protected override bool ValidateParameters()
        {
            this.EchoOff = false;
            this.UseCommandProcessor = true;
            //System.Diagnostics.Debugger.Launch();
            base.Log.LogMessageFromText("Validating arguments", MessageImportance.Low);
            if (!File.Exists(ProtocFullPath))
            {
                string message = string.Format("Missing ProtocFullPath: {0}", ProtocFullPath);
                base.Log.LogError(message, null);
                return false;
            }
            this.ExeName = ProtocFullPath;
            if (!Directory.Exists(Protobuf_StandardImportsPath))
            {
                string message = string.Format("Missing Protobuf_StandardImportsPath: {0}", Protobuf_StandardImportsPath);
                base.Log.LogError(message, null);
                return false;
            }
            if (!File.Exists(ProtocGenDocFullPath))
            {
                string message = string.Format("Missing ProtocGenDocFullPath: {0}", ProtocGenDocFullPath);
                base.Log.LogError(message, null);
                return false;
            }
            if (!Directory.Exists(ProtocGenDocOutputDir))
            {
                string message = string.Format("Missing ProtocGenDocOutputDir: {0}", ProtocGenDocOutputDir);
                base.Log.LogError(message, null);
                return false;
            }
            switch (GenDocType)
            {
                case "json":
                case "html":
                case "md":
                    break;
                default:
                    string message = string.Format("Wrong GenDocType: {0}. Supported types: json, md, html.", GenDocType);
                    base.Log.LogError(message, null);
                    return false;
            }
            if (!File.Exists(ProtoFile.GetMetadata("FullPath")))
            {
                string message = string.Format("Missing ProtoFile: {0}", ProtoFile);
                base.Log.LogError(message, null);
                return false;
            }
            return base.ValidateParameters();
        }
        protected override string GenerateFullPathToTool()
        {
            //string path = ToolPath;
            //// If ToolPath was not provided by the MSBuild script try to find it.
            //if (string.IsNullOrEmpty(path))
            //{
            //    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(
            //    @"SOFTWARE\Microsoft\VisualStudio\10.0\Setup\VS"))
            //    {
            //        if (key != null)
            //        {
            //            string keyValue =
            //            key.GetValue("EnvironmentDirectory", null).ToString();
            //            path = keyValue;
            //        }
            //    }
            //}
            //if (string.IsNullOrEmpty(path))
            //{
            //    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(
            //    @"SOFTWARE\Microsoft\VisualStudio\9.0\Setup\VS"))
            //    {
            //        if (key != null)
            //        {
            //            string keyValue =
            //            key.GetValue("EnvironmentDirectory", null).ToString();
            //            path = keyValue;
            //        }
            //    }
            //}
            //if (string.IsNullOrEmpty(path))
            //{
            //    using (RegistryKey key = Registry.LocalMachine.OpenSubKey
            //    (@"SOFTWARE\Microsoft\VisualStudio\8.0\Setup\VS"))
            //    {
            //        if (key != null)
            //        {
            //            string keyValue =
            //            key.GetValue("EnvironmentDirectory", null).ToString();
            //            path = keyValue;
            //        }
            //    }
            //}
            //if (string.IsNullOrEmpty(path))
            //{
            //    Log.LogError("VisualStudio install directory not found",
            //    null);
            //    return string.Empty;
            //}
            //string fullpath = Path.Combine(path, ToolName);
            return ExeName;
        }
        protected override string GenerateCommandLineCommands()
        {
            var protoFileName = ProtoFile.GetMetadata(ProtoFile.GetMetadata("Filename"));
            var protoFileDir = Path.GetDirectoryName(ProtoFile.GetMetadata("FullPath"));
            this.command = $" --plugin=protoc-gen-doc={ProtocGenDocFullPath} --doc_out={ProtocGenDocOutputDir} --doc_opt={GenDocType},{protoFileName}.{GenDocType} -I {protoFileDir};{Protobuf_StandardImportsPath} {protoFileName}.proto";
            //System.Diagnostics.Debugger.Launch();
            //base.Log.LogError(command);
            //Log.LogMessageFromResources(MessageImportance.Normal, "Exec.CommandFailedNoErrorCode", command, ExitCode);
            return command;
        }
        private string command;
        protected override void LogToolCommand(string message)
        {
            // Dont print the command line if Echo is Off.
            if (!EchoOff)
            {
                base.LogToolCommand(this.command);
            }
        }
        protected override string ToolName
        {
            get { return ExeName; }
        }
    }
}
