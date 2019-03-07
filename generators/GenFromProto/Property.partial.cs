using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
  public partial class Property
  {
    FileDescriptor root;
    MessageDescriptor message;
    FieldDescriptor field;
    public Property(FileDescriptor root, MessageDescriptor message, FieldDescriptor field)
    {
      this.root = root;
      this.message = message;
      this.field = field;
    }
  }
}
