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
        string ClassName;
        string FieldName;
        string FieldType;
        //bool isSpecial = false;
        public Property(FileDescriptor root, MessageDescriptor message, FieldDescriptor field)
        {
            this.root = root;
            this.message = message;
            this.field = field;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name].Fields[field.Name];
            this.DocMes = JsonDoc.Files[root.Name].Messages[message.Name];
            this.ClassName = message.Name.ToNameCs();
            this.FieldName = field.Name.ToNameCs();
            this.FieldType = field.ToTypeCs();
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
        private bool IsCollection { get { return field.IsRepeated; } }
        private bool IsObservable { get { return field.IsRepeated && (field.IsCsSimple() || field.IsAny() || (field.IsMessage() && !field.IsDefaultBase())); } }
        private bool IsDictionary { get { return field.IsRepeated && field.IsMap; } }
        private string CollectionName()
        {
            if (field.IsRepeated && (field.IsCsSimple() || field.IsAny() || (field.IsMessage() && !field.IsDefaultBase())))
                return "ObservableCollection";
            return "ConfigNodesCollection";
        }
        private bool IsSelfCollection { get { return message.Name.EndsWith(field.Name); } }
        private bool IsNullable { get { return !field.IsCsSimple() && field.IsMessage() && field.IsNullable(); } }
        private bool IsMessage { get { return !field.IsCsSimple() && field.IsMessage(); } }
        private bool IsNotSkip
        {
            get
            {
                if (this.DocMes.IsBaseWithParent)
                {
                    if (field.Name == "guid" || field.Name == "name" || field.Name == "sorting_value" || field.Name == "name_ui")
                        return false;
                }
                return true;
            }
        }
        private bool IsSimple { get { return field.IsCsSimple(); } }
    }
}
