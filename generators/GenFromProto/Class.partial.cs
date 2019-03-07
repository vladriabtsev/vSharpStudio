using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
  public partial class Class
  {
    FileDescriptor root;
    MessageDescriptor message;
    public Class(FileDescriptor root, MessageDescriptor message)
    {
      this.root = root;
      this.message = message;
    }
  }
}
