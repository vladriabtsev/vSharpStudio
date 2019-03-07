using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
  public partial class NameSpace
  {
    FileDescriptor root;
    public NameSpace(FileDescriptor root)
    {
      this.root = root;
    }
  }
}
