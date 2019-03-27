using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class ProtoToVM
    {
        FileDescriptor root;
        List<MessageDescriptor> messages;
        public ProtoToVM(FileDescriptor root, List<MessageDescriptor> messages)
        {
            this.root = root;
            this.messages = messages;
        }
    }
}
