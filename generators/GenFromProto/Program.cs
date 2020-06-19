﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Google.Protobuf.Reflection;
using Microsoft.Extensions.Logging;

namespace GenFromProto
{
    partial class Program
    {
        // https://github.com/j-maly/CommandLineParser
        // https://github.com/fclp/fluent-command-line-parser
        // https://github.com/natemcmaster/CommandLineUtils
        // https://github.com/commandlineparser/commandline !!!!
        public class Options
        {
            [Option('m', "model", SetName = "model", Required = false, HelpText = "Model generation")]
            public bool IsModel { get; set; }
            [Option('i', "interface", SetName = "model", Required = false, HelpText = "Model interface generation")]
            public bool IsInterface { get; set; }
            [Option('r', "readonly", Required = false, HelpText = "Readonly model interface")]
            public bool IsReadonly { get; set; }
            [Option('o', "output", Required = true, HelpText = "Output file path for generated code")]
            public string OutputFile { get; set; }
            [Option('p', "proto", Required = true, HelpText = "Proto file name")]
            public string ProtoFileName { get; set; }
            [Option('n', "namespace", Required = true, HelpText = "Namespace for generated code")]
            public string Namespace { get; set; }
            [Option('b', "baseclass", Required = false, HelpText = "Default base class for models (can be overriden by specifying base class in proto file)")]
            public string BaseclassDefault { get; set; }
            [Option('d', "docfolder", Required = true, HelpText = "Json doc folder")]
            public string JsonDocFolder { get; set; }
        }
        public static Options RunOptions { get; private set; }

        static void Main(string[] args)
        {
            //    Console.WriteLine($"Hello {subject}!");
            LoggerInit.Init();
            var _logger = Logger.CreateLogger<Program>();

            Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {

                try
                {
                    RunOptions = o;
                    _logger.LogInformation("***  App Starting IsModel={1}".CallerInfo(), o.IsModel);
                    //_logger.LogInformation("***  App Starting {@Args}".CallerInfo(), args.Aggregate((s1, s2) =>
                    //{
                    //    return s1 + " " + s2;
                    //}));
                    var ncs = o.ProtoFileName.ToNameCs();
                    string reflectionClass = ncs + "Reflection";
                    //Type reflection = typeof(Proto.Config.Connection.ConnMssqlReflection).Assembly.GetType(destNS + "." + reflectionClass);
                    var types = typeof(Proto.Doc.ProtoDocReflection).Assembly.GetTypes();
                    Type reflection = null;
                    foreach (var t in types)
                    {
                        if (t.Name == reflectionClass)
                        {
                            reflection = t;
                            break;
                        }
                    }
                    var protoNS = reflection.FullName.Substring(0, reflection.FullName.LastIndexOf('.'));
                    //Type reflection = typeof(Proto.Config.Connection.ConnMssqlReflection).Assembly.GetType(reflectionClass);
                    var descr = reflection.GetProperty("Descriptor", BindingFlags.Public | BindingFlags.Static);
                    object value = descr.GetValue(null, null);
                    FileDescriptor typedValue = (FileDescriptor)value;

                    Dictionary<string, List<MessageDescriptor>> dicParents = new Dictionary<string, List<MessageDescriptor>>();
                    List<MessageDescriptor> messages = CollectMessages(typedValue, dicParents);

                    //string path = "../../../../doc/" + protofilename + ".json";
                    string path = o.JsonDocFolder + o.ProtoFileName + ".json";
                    ProtoDoc.CreateDoc(path);

                    string res = null;
                    if (o.IsModel)
                    {
                        NameSpace ns = new NameSpace(typedValue, messages, dicParents, o.Namespace, protoNS, o.BaseclassDefault);
                        res = ns.TransformText();
                    }
                    else if (o.IsInterface)
                    {
                        ModelInterfaces ns = new ModelInterfaces(typedValue, messages, dicParents, o.Namespace, protoNS);
                        res = ns.TransformText();
                    }
                    else
                        throw new ArgumentException("Expected 'model' or 'interface'");

                    string filedest = o.OutputFile;
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
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //System.Diagnostics.Trace.WriteLine("##### GenFromProto current dir: " + Directory.GetCurrentDirectory());
                }
            });
        }
    }
    public static partial class Utils
    {
        public static string ToNameCs(this string from)
        {
            string s = from;
            if (s.StartsWith("proto_"))
                s = s.Remove(0, 6);
            s = char.ToUpper(s[0]) + s.Remove(0, 1);
            int nxt = s.IndexOf('_');
            while (nxt > -1)
            {
                s = s.Remove(nxt) + char.ToUpper(s[nxt + 1]) + s.Remove(0, nxt + 2);
                nxt = s.IndexOf('_');
            }
            return s;
        }
        public static bool IsDefaultBase(this Google.Protobuf.Reflection.MessageDescriptor from)
        {
            if (from.Name.EndsWith("_nullable"))
                return false;
            if (from.Name.EndsWith("_nullable_enum"))
                return false;
            var doc = JsonDoc.Files[from.File.Name].Messages[from.Name];
            //Trace.TraceInformation("#############  Doc base class: " + doc.BaseClass);
            if (string.IsNullOrWhiteSpace(doc.BaseClass)
                || doc.BaseClass.Contains("ConfigObjectVmBase")
                || doc.BaseClass.Contains("ConfigObjectCommonBase")
                || doc.BaseClass.Contains("ConfigObjectVmGenSettings")
                )
                return true;
            return false;
        }
        public static bool IsDefaultBase(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            if (from.IsMessage())
            {
                if (from.MessageType.Name.EndsWith("_nullable"))
                    return false;
                if (from.MessageType.Name.EndsWith("_nullable_enum"))
                    return false;
            }
            var doc = JsonDoc.Files[from.File.Name].Messages[from.MessageType.Name];
            if (string.IsNullOrWhiteSpace(doc.BaseClass)
                || doc.BaseClass.Contains("ConfigObjectVmBase")
                || doc.BaseClass.Contains("ConfigObjectCommonBase")
                || doc.BaseClass.Contains("ConfigObjectVmGenSettings")
                )
                return true;
            return false;
        }
        public static bool IsBytes(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            switch (from.FieldType)
            {
                case Google.Protobuf.Reflection.FieldType.Bytes:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsMap(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            if (from.IsMap)
                return true;
            return false;
        }
        public static bool IsMessage(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            switch (from.FieldType)
            {
                case Google.Protobuf.Reflection.FieldType.Message:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsNullable(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            if (from.MessageType.Name.EndsWith("_nullable"))
                return true;
            if (from.MessageType.Name.EndsWith("_nullable_enum"))
                return true;
            return false;
        }
        public static bool IsAny(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            switch (from.FieldType)
            {
                case Google.Protobuf.Reflection.FieldType.Message:
                    switch (from.MessageType.FullName)
                    {
                        case "google.protobuf.Any":
                            return true;
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }
        public static bool IsCsSimple(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            switch (from.FieldType)
            {
                case Google.Protobuf.Reflection.FieldType.Bool:
                case Google.Protobuf.Reflection.FieldType.Bytes:
                case Google.Protobuf.Reflection.FieldType.Double:
                case Google.Protobuf.Reflection.FieldType.Enum:
                case Google.Protobuf.Reflection.FieldType.Fixed32:
                case Google.Protobuf.Reflection.FieldType.Fixed64:
                case Google.Protobuf.Reflection.FieldType.Float:
                case Google.Protobuf.Reflection.FieldType.Int32:
                case Google.Protobuf.Reflection.FieldType.Int64:
                case Google.Protobuf.Reflection.FieldType.SFixed32:
                case Google.Protobuf.Reflection.FieldType.SFixed64:
                case Google.Protobuf.Reflection.FieldType.SInt32:
                case Google.Protobuf.Reflection.FieldType.SInt64:
                case Google.Protobuf.Reflection.FieldType.String:
                case Google.Protobuf.Reflection.FieldType.UInt32:
                case Google.Protobuf.Reflection.FieldType.UInt64:
                    return true;
                case Google.Protobuf.Reflection.FieldType.Message:
                    switch (from.MessageType.Name)
                    {
                        case "bool_nullable":
                        case "double_nullable":
                        case "uint_nullable":
                        case "ulong_nullable":
                        case "float_nullable":
                        case "int_nullable":
                        case "long_nullable":
                        case "string_nullable":
                        case "Timestamp":
                        case "Duration":
                            return true;
                        default:
                            if (from.MessageType.Name.EndsWith("_nullable_enum"))
                                return true;
                            return false;
                    }
                default:
                    throw new NotSupportedException();
            }
        }
        public static string ToTypeCs(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
            // https://developers.google.com/protocol-buffers/docs/reference/google.protobuf
            if (from.FieldType == Google.Protobuf.Reflection.FieldType.Message)
            {
                switch (from.MessageType.Name)
                {
                    case "bool_nullable":
                        return "bool?";
                    case "double_nullable":
                        return "double?";
                    case "uint_nullable":
                        return "uint?";
                    case "ulong_nullable":
                        return "ulong?";
                    case "float_nullable":
                        return "float?";
                    case "int_nullable":
                        return "int?";
                    case "long_nullable":
                        return "long?";
                    case "string_nullable":
                        return "string";
                    case "Any":
                        return "Google.Protobuf.WellKnownTypes.Any";
                    case "Duration":
                        return "Google.Protobuf.WellKnownTypes.Duration";
                    case "Timestamp":
                        return "Google.Protobuf.WellKnownTypes.Timestamp";
                    default:
                        if (from.MessageType.Name.EndsWith("_nullable"))
                            return from.MessageType.Name.Replace("_nullable", "").ToNameCs() + "?";
                        if (from.MessageType.Name.EndsWith("_nullable_enum"))
                            return from.MessageType.Name.Replace("_nullable_enum", "").ToNameCs() + "?";
                        return from.MessageType.Name.ToNameCs();
                }
            }
            else if (from.FieldType == Google.Protobuf.Reflection.FieldType.Enum)
            {
                return from.EnumType.Name.ToNameCs();
            }
            return FieldTypeSimpleToTypeCs(from.FieldType);
        }
        public static string ConvertToVm(this Google.Protobuf.Reflection.FieldDescriptor field, string from_proto)
        {
            StringBuilder sb = new StringBuilder();
            // https://developers.google.com/protocol-buffers/docs/reference/google.protobuf
            if (field.FieldType == Google.Protobuf.Reflection.FieldType.Message)
            {
                if (field.IsNullable())
                {
                    sb.Append(from_proto);
                    sb.Append(".");
                    sb.Append(field.Name.ToNameCs());
                    sb.Append(".HasValue ? (");
                    sb.Append(field.ToTypeCs());
                    sb.Append(")");
                    sb.Append(from_proto);
                    sb.Append(".");
                    sb.Append(field.Name.ToNameCs());
                    switch (field.MessageType.Name)
                    {
                        //case "Any":
                        //    return "Google.Protobuf.WellKnownTypes.Any";
                        case "time_span_nullable":
                            sb.Append(".Value.ToTimeSpan() : (");
                            break;
                        case "date_time_nullable":
                            sb.Append(".Value.ToDateTime() : (");
                            break;
                        default:
                            sb.Append(".Value : (");
                            //sb.Append("/*");
                            //sb.Append(field.MessageType.Name);
                            //sb.Append("*/");
                            break;
                    }
                    sb.Append(field.ToTypeCs());
                    sb.Append(")null");
                }
                else
                {
                    switch (field.MessageType.Name)
                    {
                        case "Any":
                            sb.Append("Google.Protobuf.WellKnownTypes.Any");
                            break;
                        default:
                            sb.Append(from_proto);
                            sb.Append(".");
                            sb.Append(field.Name.ToNameCs());
                            break;
                    }
                }

            }
            else if (field.FieldType == Google.Protobuf.Reflection.FieldType.Enum)
            {
                sb.Append("(");
                sb.Append(field.EnumType.Name.ToNameCs());
                sb.Append(")");
                sb.Append(from_proto);
                sb.Append(".");
                sb.Append(field.Name.ToNameCs());
            }
            else
            {
                sb.Append(from_proto);
                sb.Append(".");
                sb.Append(field.Name.ToNameCs());
            }
            return sb.ToString();
        }
        //public static string ConvertToProto(this Google.Protobuf.Reflection.FieldDescriptor field, string from_proto)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    // https://developers.google.com/protocol-buffers/docs/reference/google.protobuf
        //    if (field.FieldType == Google.Protobuf.Reflection.FieldType.Message)
        //    {
        //        if (field.IsNullable())
        //        {
        //            //sb.Append(from_proto);
        //            //sb.Append(".");
        //            //sb.Append(field.Name.ToNameCs());
        //            //sb.Append(".HasValue ? (");
        //            //sb.Append(field.ToTypeCs());
        //            //sb.Append(")");
        //            //sb.Append(from_proto);
        //            //sb.Append(".");
        //            //sb.Append(field.Name.ToNameCs());
        //            switch (field.MessageType.Name)
        //            {
        //                //case "Any":
        //                //    return "Google.Protobuf.WellKnownTypes.Any";
        //                case "time_span_nullable":
        //                    sb.Append("Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(");
        //                    sb.Append(from_proto);
        //                    sb.Append(".");
        //                    sb.Append(field.Name.ToNameCs());
        //                    sb.Append(".Value)");
        //                    break;
        //                case "date_time_nullable":
        //                    sb.Append("Google.Protobuf.WellKnownTypes.Duration.FromDateTime(");
        //                    sb.Append(from_proto);
        //                    sb.Append(".");
        //                    sb.Append(field.Name.ToNameCs());
        //                    sb.Append(".Value)");
        //                    break;
        //                default:
        //                    sb.Append(".Value : (");
        //                    //sb.Append("/*");
        //                    //sb.Append(field.MessageType.Name);
        //                    //sb.Append("*/");
        //                    break;
        //            }
        //            //sb.Append(field.ToTypeCs());
        //            //sb.Append(")null");
        //        }
        //        else
        //        {
        //            switch (field.MessageType.Name)
        //            {
        //                case "Any":
        //                    sb.Append("Google.Protobuf.WellKnownTypes.Any");
        //                    break;
        //                default:
        //                    sb.Append(from_proto);
        //                    sb.Append(".");
        //                    sb.Append(field.Name.ToNameCs());
        //                    break;
        //            }
        //        }

        //    }
        //    else if (field.FieldType == Google.Protobuf.Reflection.FieldType.Enum)
        //    {
        //        sb.Append("(");
        //        sb.Append(field.EnumType.Name.ToNameCs());
        //        sb.Append(")");
        //        sb.Append(from_proto);
        //        sb.Append(".");
        //        sb.Append(field.Name.ToNameCs());
        //    }
        //    else
        //    {
        //        sb.Append(from_proto);
        //        sb.Append(".");
        //        sb.Append(field.Name.ToNameCs());
        //    }
        //    return sb.ToString();
        //}
        public static string FieldTypeSimpleToTypeCs(Google.Protobuf.Reflection.FieldType from)
        {
            switch (from)
            {
                case Google.Protobuf.Reflection.FieldType.Bool:
                    return "bool";
                case Google.Protobuf.Reflection.FieldType.Bytes:
                    return "byte[]";
                case Google.Protobuf.Reflection.FieldType.Double:
                    return "double";
                case Google.Protobuf.Reflection.FieldType.Enum:
                    return "enum";
                case Google.Protobuf.Reflection.FieldType.Fixed32:
                    return "uint";
                case Google.Protobuf.Reflection.FieldType.Fixed64:
                    return "ulong";
                case Google.Protobuf.Reflection.FieldType.Float:
                    return "float";
                case Google.Protobuf.Reflection.FieldType.Int32:
                    return "int";
                case Google.Protobuf.Reflection.FieldType.Int64:
                    return "long";
                case Google.Protobuf.Reflection.FieldType.SFixed32:
                    return "int";
                case Google.Protobuf.Reflection.FieldType.SFixed64:
                    return "long";
                case Google.Protobuf.Reflection.FieldType.SInt32:
                    return "int";
                case Google.Protobuf.Reflection.FieldType.SInt64:
                    return "long";
                case Google.Protobuf.Reflection.FieldType.String:
                    return "string";
                case Google.Protobuf.Reflection.FieldType.UInt32:
                    return "uint";
                case Google.Protobuf.Reflection.FieldType.UInt64:
                    return "ulong";
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
