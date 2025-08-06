using Google.Protobuf.Reflection;

namespace GenVmFromProto
{
    public partial class AcceptNodeVisitor
    {
        readonly FileDescriptor root;
        readonly MessageDescriptor message;
        readonly MessageDoc Doc;
        public AcceptNodeVisitor(FileDescriptor root, MessageDescriptor message)
        {
            this.root = root;
            this.message = message;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
        }
    }
}
