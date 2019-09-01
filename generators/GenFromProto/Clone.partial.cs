using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class Clone
    {
        string nameSpace;
        string protoNameSpace;
        FileDescriptor root;
        MessageDescriptor message;
        MessageDoc Doc;
        public Clone(FileDescriptor root, MessageDescriptor message, string destNS, string protoNS)
        {
            this.root = root;
            this.message = message;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
        }
    }
}
