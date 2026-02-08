using Google.Protobuf.Reflection;

namespace GenVmFromProto
{
    public partial class AcceptNodeVisitor(FileDescriptor root, MessageDescriptor message)
    {
        readonly FileDescriptor root = root;
        readonly MessageDescriptor message = message;
        readonly MessageDoc Doc = JsonDoc.Files[root.Name].Messages[message.Name];
        //readonly MyDictionary<string, FileDoc> DicMessages = JsonDoc.Files[root.Name];
        //private void test()
        //{
        //    foreach (var field in this.message.Fields.InDeclarationOrder())
        //    {
        //        field.GetMessageDoc().IsValidatableBase
        //    }
        //}
    }
}
