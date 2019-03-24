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
        Dictionary<string, List<MessageDescriptor>> dicParents;
        public Class(FileDescriptor root, MessageDescriptor message, Dictionary<string, List<MessageDescriptor>> dicParents)
        {
            this.root = root;
            this.message = message;
            this.dicParents = dicParents;
            //foreach (var t in message.Fields.InDeclarationOrder())
            //{
            //    t.Declaration.ToLeadingDetachedComments("");
            //}
        }
    }
}
