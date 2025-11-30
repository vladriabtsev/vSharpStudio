using Google.Protobuf.Reflection;

namespace GenVmFromProto
{
    public partial class AcceptNodeVisitor(FileDescriptor root, MessageDescriptor message)
    {
        readonly FileDescriptor root = root;
        readonly MessageDescriptor message = message;
        readonly MessageDoc Doc = JsonDoc.Files[root.Name].Messages[message.Name];
    }
}
