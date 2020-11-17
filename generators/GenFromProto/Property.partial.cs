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
        FieldDoc Doc;
        MessageDoc DocMes;
        //bool isSpecial = false;
        public Property(FileDescriptor root, MessageDescriptor message, FieldDescriptor field)
        {
            this.root = root;
            this.message = message;
            this.field = field;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name].Fields[field.Name];
            this.DocMes = JsonDoc.Files[root.Name].Messages[message.Name];
            //if (field.Name == "guid") // || field.Name == "name" || field.Name == "name_ui" || field.Name == "sorting_value")
            //    isSpecial = true;
        }
        private bool IsNotSpecial(string fieldName)
        {
            if (fieldName == "is_new")
                return false;
            if (fieldName == "is_has_new")
                return false;
            if (fieldName == "is_marked_for_deletion")
                return false;
            if (fieldName == "is_has_marked_for_deletion")
                return false;
            if (fieldName == "is_has_changed")
                return false;
            return true;
        }
    }
}
