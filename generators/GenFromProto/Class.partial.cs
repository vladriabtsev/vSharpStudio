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
        MessageDoc Doc;
        string nameSpace;
        string protoNameSpace;

        Dictionary<string, List<MessageDescriptor>> dicParents;
        public Class(FileDescriptor root, MessageDescriptor message, Dictionary<string, List<MessageDescriptor>> dicParents, string destNS, string protoNS)
        {
            this.root = root;
            this.message = message;
            this.dicParents = dicParents;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
            if (!JsonDoc.Files.ContainsKey(root.Name))
            {
            }
            if (!JsonDoc.Files[root.Name].Messages.ContainsKey(message.Name))
            {
            }
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
            if (this.Doc.BaseClass == "")
            {
                this.Doc.BaseClass = " : ConfigObjectBase<" + message.Name.ToNameCs() + ", " + message.Name.ToNameCs() + "." +
                    message.Name.ToNameCs() + "Validator>, IComparable<" + message.Name.ToNameCs() + ">, I" + root.Package.ToNameCs() + "AcceptVisitor";
            }
            else
            {
            }
        }
    }
}
