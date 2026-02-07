using System.Collections.Generic;
using System.Diagnostics;
using ApplicationLogging;
using Google.Protobuf.Reflection;
using Microsoft.Extensions.Logging;
using Proto.Doc;

namespace GenVmFromProto
{
    public partial class ModelInterfaces
    {
        private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(ModelInterfaces));
        MessageDoc? MessageDoc;
        FieldDoc? FieldDoc;
        readonly FileDescriptor root;
        readonly string nameSpace;
        readonly string protoNameSpace;
        readonly List<MessageDescriptor> messages = new List<MessageDescriptor>();
        readonly Dictionary<string, List<MessageDescriptor>> dicParents;
        readonly string Setter = "";

        public ModelInterfaces(FileDescriptor root, List<MessageDescriptor> messages,
            Dictionary<string, List<MessageDescriptor>> dicParents,
            string destNS, string protoNS)
        {
            _logger?.Debug("Create interfaces for '{0}'", root.Name);
            this.root = root;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
            this.messages = messages;
            this.dicParents = dicParents;
            Debug.Assert(Program.RunOptions != null);
            if (!Program.RunOptions.IsReadonly)
                //    Setter = "internal set; ";
                //else
                Setter = "set; ";
            //var files = JsonDoc.Files;

            //var dic = JsonDoc.Files[root.Name].Enums;

            //foreach(var t in dic)
            //{
            //    foreach (var tt in t.Value.Values)
            //    {
            //        var s = tt.Value.value.Number;
            //    }
            //}

            //foreach (var t in FieldDoc.en)
            //foreach(var t in root.EnumTypes)
            //{
            //    t.
            //}

            //foreach (var message in messages)
            //{
            //    this.MessageDoc = JsonDoc.Files[root.Name].Messages[message.Name];
            //    foreach (var field in message.Fields.InDeclarationOrder())
            //    {
            //        if (IsSkip(field)) continue;
            //        this.FieldDoc = JsonDoc.Files[root.Name].Messages[message.Name].Fields[field.Name];
            //        if (field.IsRepeated)
            //        {
            //            if (field.IsMap())
            //            {
            //                var lst = field.MessageType.Fields.InDeclarationOrder();
            //                Debug.Assert(lst.Count == 2);
            //                foreach (var f in lst)
            //                {
            //                    if (f.Name == "key")
            //                    {

            //                    }
            //                    else if (f.Name == "value")
            //                    {

            //                    }
            //                    else
            //                    {

            //                    }
            //                }
            //                //new Google.Protobuf.Reflection.MessageDescriptor.FieldCollection.FieldCollectionDebugView(field.MessageType.Fields).Items[0]
            //                var key = field.MessageType.Fields[0].FieldType.FieldTypeSimpleToTypeCs();
            //                var val = field.MessageType.Fields[1].FieldType.FieldTypeSimpleToTypeCs();
            //            }
            //        }
            //    }
            //}
            t4.SrcCs();
        }
        bool IsSkip(FieldDescriptor field)
        {
            Debug.Assert(MessageDoc != null);
            if (MessageDoc.IsConfigObjectBase)
            {
                switch (field.Name)
                {
                    case "guid":
                    case "name":
                    case "name_ui":
                    case "sorting_value":
                        _logger?.Trace("Skipping field '{field}'", field.Name);
                        return true;
                }
            }
            return false;
        }
    }
}
