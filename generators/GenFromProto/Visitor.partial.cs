using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
  public partial class Visitor
  {
    FileDescriptor root;
    public Visitor(FileDescriptor root)
    {
      this.root = root;
    }
  }
}
