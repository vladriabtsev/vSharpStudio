using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Reflection;

namespace GenFromProto
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int ii = 0;
                if (args.Count() > 3)
                    ii = 1;
                //Debugger.Launch();
                string protofilename = args[0 + ii]; // args[0] proto file name (without extention)
                string destfile = args[1 + ii]; // args[1] destination file
                string destNS = args[2 + ii]; // args[2] destination namespace

                var ncs = protofilename.ToNameCs();
                string reflectionClass = ncs + "Reflection";
                Type reflection = typeof(Proto.Config.VsharpstudioReflection).Assembly.GetType(destNS + "." + reflectionClass);
                var descr = reflection.GetProperty("Descriptor", BindingFlags.Public | BindingFlags.Static);
                object value = descr.GetValue(null, null);
                FileDescriptor typedValue = (FileDescriptor)value;

                NameSpace ns = new NameSpace(typedValue, protofilename, destNS);
                string res = ns.TransformText();

                string filedest = destfile;
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
                            return true;
                        default:
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
                    default:
                        return from.MessageType.Name.ToNameCs();
                }
            }
            else if (from.FieldType == Google.Protobuf.Reflection.FieldType.Enum)
            {
                return from.EnumType.Name;
            }
            return FieldTypeSimpleToTypeCs(from.FieldType);
        }
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
