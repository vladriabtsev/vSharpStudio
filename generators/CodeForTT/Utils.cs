using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeForTT
{
  public class Utils
  {
    private static Dictionary<string, DateTime> _dicAsmUpdate = new Dictionary<string, DateTime>();
    private static Dictionary<string, List<ClassDesc>> _dicClassDesc = new Dictionary<string, List<ClassDesc>>();
    public static List<ClassDesc> GetProtoClassDescs(string asmpath, string nmspace, bool isReload = false)
    {
      List<ClassDesc> res = null;
      if (!_dicAsmUpdate.ContainsKey(asmpath))
        _dicAsmUpdate[asmpath] = DateTime.Now.AddYears(-10);
      var file = new FileInfo(asmpath);
      if (isReload || _dicAsmUpdate[asmpath] < file.LastWriteTimeUtc)
      {
        var source = File.ReadAllBytes(asmpath);
        var asm = Assembly.Load(source);
        _dicAsmUpdate[asmpath] = file.LastWriteTimeUtc;
        res = new List<ClassDesc>();
        var types = asm.GetTypes().Where(t => t.Namespace == nmspace && t.IsPublic).OrderBy(n => n.Name);
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
                if (tt.PropertyType.Name.StartsWith("List"))
                  p.IsList = true;
                p.IsComplex = true;
                break;
            }
            cd.ListProperties.Add(p);
          }
        }
        _dicClassDesc[asmpath] = res;
      }
      res = _dicClassDesc[asmpath];
      return res;
    }
    public static string TypeName(Type memType, bool subtype = false)
    {
      //System.Diagnostics.Debugger.Launch();
      if (memType.ContainsGenericParameters)
      {
        return "kuku";
      }
      if (memType.IsGenericType)
      {
        string nam = memType.Name;
        nam = nam.Substring(0, nam.Length - 2) + "<";
        bool first = true;
        Type[] typeParameters = memType.GetGenericArguments();
        string pname = null;
        foreach (Type tParam in typeParameters)
        {
          if (!tParam.IsGenericParameter)
          {
            if (!first)
            {
              pname = pname + ", ";
            }
            pname = pname + TypeName(tParam);
          }
        }
        if (subtype)
        {
          return pname;
        }
        nam = nam + pname + ">";
        //System.Diagnostics.Debugger.Launch();
        return nam;
      }
      //		if (!string.IsNullOrEmpty(Type.GetEnumName(memType)))
      //		{
      //			return Type.GetEnumName(memType);
      //		}
      Type nultype = Nullable.GetUnderlyingType(memType);
      Type type = null;
      if (nultype == null)
        type = memType;
      else
        type = nultype;
      string res = null;
      switch (type.Name)
      {
        case "Int32":
          res = "int";
          break;
        case "Int64":
          res = "long";
          break;
        case "Object":
        case "Enum":
        case "Bool":
        case "String":
        case "Decimal":
          res = type.Name.ToLower();
          break;
        default:
          res = type.Name;
          break;
      }
      if (nultype != null)
        res = res + "?";
      return res;
    }
    public static List<MethodInfo> GetMethods(Type type)
    {
      List<MethodInfo> lst = new List<MethodInfo>();
      MethodInfo[] methods = type.GetMethods();
      foreach (MethodInfo m in methods)
      {
        lst.Add(m);
      }
      return lst;
    }
  }
}
