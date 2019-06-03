using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class IVisitorProto
    {
        FileDescriptor root;
        string nameSpace;

        public IVisitorProto(FileDescriptor root, string destNS)
        {
            this.root = root;
            this.nameSpace = destNS;
        }
    }
}
