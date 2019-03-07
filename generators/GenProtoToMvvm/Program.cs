using Proto.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GenProtoToMvvm
{
  //TODO post build event for vSharpStudio.proto project. Need for auto generation config view models in vSharpStudio.vm project. Currently is not working.
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Count() != 3)
        throw new ArgumentException("Usage: GenProtoToMvvm dllPath namespace outFile");
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
      string res = GenMvvm();
      using (var fs = File.Open(args[2], FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write, FileShare.Write))
      {
        fs.Write(Encoding.UTF8.GetBytes(res));
      }
    }
    public static string GenMvvm()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(@"// Auto generated on UTC " + DateTime.Now.ToUniversalTime() + @"
using System;
using ViewModelBase;
using FluentValidation;
using Proto.Config;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace vSharpStudio.vm.ViewModels
{");
      foreach (var t in VsharpstudioReflection.Descriptor.MessageTypes)
      {
        #region class

        sb.Append("\tpublic partial class ");
        sb.Append(t.Name.ToNameCs());
        sb.Append(" : ViewModelValidatable<");
        sb.Append(t.Name.ToNameCs());
        sb.Append(", ");
        sb.Append(t.Name.ToNameCs());
        sb.Append(".");
        sb.Append(t.Name.ToNameCs());
        sb.AppendLine("Validator>");
        sb.AppendLine("\t{");

        #endregion class

        sb.Append("\t\tpublic partial class ");
        sb.Append(t.Name.ToNameCs());
        sb.Append("Validator : ValidatorBase<");
        sb.Append(t.Name.ToNameCs());
        sb.Append(", ");
        sb.Append(t.Name.ToNameCs());
        sb.AppendLine("Validator> { }");

        sb.Append("\t\tprivate ");
        sb.Append(t.Name);
        sb.AppendLine(" _dto;");

        #region CTOR

        sb.Append("\t\tpublic ");
        sb.Append(t.Name.ToNameCs());
        sb.Append("(");
        sb.Append(t.Name);
        sb.Append(" dto) : base(");
        sb.Append(t.Name.ToNameCs());
        sb.AppendLine("Validator.Validator)");
        sb.AppendLine("\t\t{");
        sb.AppendLine("\t\t\tthis._dto = dto;");
        sb.AppendLine("\t\t\tthis.initFromDto();");
        sb.AppendLine("\t\t\tOnInit();");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t\tprivate void initFromDto()");
        sb.AppendLine("\t\t{");
        foreach (var tt in t.Fields.InDeclarationOrder())
        {
          if (tt.IsRepeated)
          {
            sb.Append("\t\t\tthis.");
            sb.Append(tt.Name.ToNameCs());
            sb.Append(" = new ObservableCollection<");
            sb.Append(tt.MessageType.Name.ToNameCs());
            sb.AppendLine(">();");
            sb.Append("\t\t\tforeach(var t in _dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(")");
            sb.AppendLine("\t\t\t{");
            sb.Append("\t\t\t\tthis.");
            sb.Append(tt.Name.ToNameCs());
            sb.Append(".Add(new ");
            sb.Append(tt.MessageType.Name.ToNameCs());
            //            sb.Append(tt.GenericParameterTypeCs);
            sb.AppendLine("(t));");
            sb.AppendLine("\t\t\t}");
          }
          else if (tt.FieldType == Google.Protobuf.Reflection.FieldType.Message)
          {
            sb.Append("\t\t\tthis.");
            sb.Append(tt.Name.ToNameCs());
            sb.Append(" = new ");
            sb.Append(tt.MessageType.Name.ToNameCs());
            sb.Append("(_dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(");");
          }
          else if (tt.IsMap)
          {
            sb.Append("\t\t\tthis.");
            sb.Append(tt.Name);
            sb.Append(" = new ");
            sb.Append(tt.Name);
            sb.Append("(_dto.");
            sb.Append(tt.Name);
            sb.AppendLine(");");
          }
        }
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t\tpartial void OnInit();");

        #endregion CTOR

        //#region local enums
        //foreach (var tt in t.EnumTypes)
        //{
        //  sb.Append("\t\tenum ");
        //  sb.Append(tt.Name.ToNameCs());
        //  sb.AppendLine(" {");
        //  foreach (var ttt in tt.Values)
        //  {
        //    sb.Append("\t\t\t ");
        //    sb.Append(ttt.Name);
        //    sb.Append(" = ");
        //    sb.Append(ttt.Number);
        //    sb.AppendLine(",");
        //  }
        //  sb.AppendLine("\t\t}");
        //}
        //#endregion local enums

        foreach (var tt in t.Fields.InDeclarationOrder())
        {
          if (tt.IsRepeated)
          {
            sb.Append("\t\tpublic ObservableCollection<");
            sb.Append(tt.MessageType.Name.ToNameCs());
            sb.Append("> ");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(" { set; get; }");
          }
          else if (tt.FieldType == Google.Protobuf.Reflection.FieldType.Message)
          {
            sb.Append("\t\tpublic ");
            sb.Append(tt.MessageType.Name.ToNameCs());
            sb.Append(" ");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(" { set; get; }");
          }
          else if (tt.FieldType == Google.Protobuf.Reflection.FieldType.Enum)
          {
            sb.Append("\t\tpublic ");
            sb.Append(t.Name);
            sb.Append(".Types. ");
            sb.Append(tt.Name.ToNameCs());
            sb.Append(" ");
            sb.AppendLine(tt.Name.ToNameCs());
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tset");
            sb.AppendLine("\t\t\t{");
            sb.Append("\t\t\t\tif (_dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(" != value)");
            sb.AppendLine("\t\t\t\t{");
            sb.Append("\t\t\t\t\t_dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(" = value;");
            sb.Append("\t\t\t\t\tOn");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine("Changed();");
            sb.AppendLine("\t\t\t\t\tNotifyPropertyChanged();");
            //            sb.AppendLine("\t\t\t\t\tValidateProperty();");
            sb.AppendLine("\t\t\t\t}");
            sb.AppendLine("\t\t\t}");
            sb.Append("\t\t\tget { return _dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine("; }");
            sb.AppendLine("\t\t}");

            sb.Append("\t\tpartial void On");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine("Changed();");
          }
          else
          {
            sb.Append("\t\tpublic ");
            sb.Append(tt.FieldType.ToTypeCs());
            sb.Append(" ");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine();
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tset");
            sb.AppendLine("\t\t\t{");
            sb.Append("\t\t\t\tif (_dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(" != value)");
            sb.AppendLine("\t\t\t\t{");
            sb.Append("\t\t\t\t\t_dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine(" = value;");
            sb.Append("\t\t\t\t\tOn");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine("Changed();");
            sb.AppendLine("\t\t\t\t\tNotifyPropertyChanged();");
            //            sb.AppendLine("\t\t\t\t\tValidateProperty();");
            sb.AppendLine("\t\t\t\t}");
            sb.AppendLine("\t\t\t}");
            sb.Append("\t\t\tget { return _dto.");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine("; }");
            sb.AppendLine("\t\t}");

            sb.Append("\t\tpartial void On");
            sb.Append(tt.Name.ToNameCs());
            sb.AppendLine("Changed();");
          }
        }
        sb.AppendLine("\t}");
      }
      sb.AppendLine("}");
      return sb.ToString();
    }
    public static List<ClassDesc> GetProtoClassDescs(string asmpath, string nmspace)
    {
      List<ClassDesc> res = null;
      var file = new FileInfo(asmpath);
      var source = File.ReadAllBytes(asmpath);
      var asm = Assembly.Load(source);
      res = new List<ClassDesc>();
      var types = asm.GetTypes().Where(t => t.Namespace == nmspace && t.IsPublic && !t.Name.EndsWith("Reflection")).OrderBy(n => n.Name);
      foreach (var t in types)
      {
        var cd = new ClassDesc();
        cd.Name = t.Name;
        cd.TypeNamespace = t.Namespace;
        res.Add(cd);
        var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).OrderBy(n => n.Name);
        foreach (var tt in properties)
        {
          //if (tt.PropertyType.Name == "MessageDescriptor")
          //  continue;
          var p = new PropertyDesc();
          p.Name = tt.Name;
          switch (tt.PropertyType.Name)
          {
            case "int":
            case "long":
            case "float":
            case "double":
            case "decimal":
            case "string":
              p.TypeName = tt.PropertyType.Name;
              break;
            case "Int32":
              p.TypeName = "int";
              break;
            case "Bool":
            case "String":
            case "Decimal":
              p.TypeName = tt.PropertyType.Name.ToLower();
              break;
            default:
              if (tt.PropertyType.IsGenericType)
              {
                p.IsGeneric = true;
                foreach (var ttt in tt.PropertyType.GenericTypeArguments)
                {
                  if (!string.IsNullOrEmpty(p.GenericParameterType))
                    throw new NotSupportedException();
                  p.GenericParameterType = ttt.Name;
                }
              }
              else
              {
                if (tt.PropertyType.Name.StartsWith("List"))
                  p.IsList = true;
                p.IsComplex = true;
              }
              p.TypeName = tt.PropertyType.Name;
              break;
          }
          cd.ListProperties.Add(p);
        }
      }
      return res;
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
  [Serializable]
  public class ClassDesc
  {
    public ClassDesc()
    {
      ListProperties = new List<PropertyDesc>();
    }
    public string Name;
    public string NameCs => Name.StartsWith("proto_") ? char.ToUpper(Name[6]) + Name.Remove(0, 7) : Name;
    public string TypeNamespace;
    public List<PropertyDesc> ListProperties;
  }
  [Serializable]
  public class PropertyDesc
  {
    public string Name;
    public string TypeName;
    public string TypeNameCs
    {
      get
      {
        if (TypeName.StartsWith("proto_"))
          return char.ToUpper(TypeName[6]) + TypeName.Remove(0, 7);
        if (IsGeneric)
        {
          if (TypeName.StartsWith("RepeatedField"))
            return "ObservableCollection<" + GenericParameterTypeCs + ">";
        }
        return TypeName;
      }
    }
    public string TypeNamespace;

    public bool IsGeneric;
    public string GenericParameterType;
    public string GenericParameterTypeCs => GenericParameterType.StartsWith("proto_") ? char.ToUpper(GenericParameterType[6]) + GenericParameterType.Remove(0, 7) : GenericParameterType;

    public bool IsComplex;
    public bool IsEnum;
    public bool IsList;
  }
}
