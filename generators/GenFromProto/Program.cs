using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Diagnostics.Trace.WriteLine("##### GetLatestAttr: " + Directory.GetCurrentDirectory());
            try
            {
                if (args.Count() != 2)
                {
                    if (args.Count() == 0)
                    {
                        args = new string[2];
                        args[0] = @"..\..\..\..\vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs";
                        args[1] = @"vSharpStudio.vm.ViewModels";
                    }
                    else
                        throw new ArgumentException("Usage: GenFromProto outFile namespace");
                }
                //if (!File.Exists(args[0]))
                //{
                //    string s = "Couldn't find file '" + args[0] + "'";
                //    Trace.WriteLine(s);
                //    Trace.WriteLine("Full path: " + Path.GetFullPath(args[0]));
                //    throw new ArgumentException(s);
                //}
                var ext = Path.GetExtension(args[0]);
                if (ext.ToLower() != ".cs")
                {
                    string s = "Destination file extention is not 'cs'";
                    Trace.WriteLine(s);
                    throw new ArgumentException(s);
                }
                var dir = Path.GetDirectoryName(args[0]);
                if (!Directory.Exists(dir))
                {
                    string s = "Couldn't find folder '" + dir + "'";
                    Trace.WriteLine(s);
                    Trace.WriteLine("Full path: " + Path.GetFullPath(dir));
                    throw new ArgumentException(s);
                }
                NameSpace ns = new NameSpace(Proto.Config.VsharpstudioReflection.Descriptor);
                string res = ns.TransformText();
                if (!File.Exists(args[0]))
                {
                    File.CreateText(args[0]);
                }
                using (var fs = File.Open(args[0], FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write, FileShare.Write))
                {
                    var bytes = Encoding.UTF8.GetBytes(res);
                    fs.Write(bytes, 0, bytes.Count());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("##### GenFromProto: Exception:" + ex.Message);
                throw;
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
        public static string ToTypeCs(this Google.Protobuf.Reflection.FieldDescriptor from)
        {
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
                    default:
                        return from.MessageType.Name.ToNameCs();
                }
            }
            else if (from.FieldType == Google.Protobuf.Reflection.FieldType.Enum)
            {
                return from.EnumType.Name;
            }

            return from.FieldType.ToTypeCs();
        }
        public static string ToTypeCs(this Google.Protobuf.Reflection.FieldType from)
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
