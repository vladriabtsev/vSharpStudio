using System;
using System.Collections.Generic;
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
            if (args.Count() != 3)
            {
                if (args.Count() == 0)
                {
                    args = new string[3];
                    args[0] = @"..\..\..\..\vSharpStudio.proto\bin\Debug\netstandard2.0\vSharpStudio.proto.dll";
                    args[2] = @"..\..\..\..\vSharpStudio.vm\ViewModels\ProtoViewModels.cs";
                    args[1] = @"vSharpStudio.vm.ViewModels";
                }
                else
                    throw new ArgumentException("Usage: GenProtoToMvvm dllPath namespace outFile");
            }
            if (!File.Exists(args[0]))
                throw new ArgumentException("Couldn't find file '" + args[0] + "'");
            var ext = Path.GetExtension(args[2]);
            if (ext.ToLower() != ".cs")
                throw new ArgumentException("Destination file extention is not 'cs'");
            var dir = Path.GetDirectoryName(args[2]);
            if (!Directory.Exists(dir))
                throw new ArgumentException("Couldn't find folder '" + dir + "'");

            //var lst = GetProtoClassDescs(args[0], args[1]);
            //string res = GenMvvm(lst);
            NameSpace ns = new NameSpace(Proto.Config.VsharpstudioReflection.Descriptor);
            string res = ns.TransformText();
            using (var fs = File.Open(args[2], FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write, FileShare.Write))
            {
                var bytes = Encoding.UTF8.GetBytes(res);
                fs.Write(bytes, 0, bytes.Count());
            }
        }
    }
    public static class Utils
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
