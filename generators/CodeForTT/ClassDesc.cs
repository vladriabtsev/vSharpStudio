using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForTT
{
  [Serializable]
  public class ClassDesc
  {
    public ClassDesc()
    {
      ListProperties = new List<PropertyDesc>();
    }
    public string Name;
    public string TypeNamespace;
    public List<PropertyDesc> ListProperties;
  }
  [Serializable]
  public class PropertyDesc
  {
    public string Name;
    public string TypeName;
    public string TypeNamespace;

    public bool IsComplex;
    public bool IsEnum;
    public bool IsList;
  }
}
