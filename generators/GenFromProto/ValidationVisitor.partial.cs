﻿using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class ValidationVisitor
    {
        FileDescriptor root;
        List<MessageDescriptor> messages = new List<MessageDescriptor>();
        public ValidationVisitor(FileDescriptor root)
        {
            this.root = root;
            CollectMessages(root);
        }
        private void CollectMessages(FileDescriptor root)
        {
            foreach (var t in root.MessageTypes)
            {
                if (t.Name.EndsWith("_nullable"))
                    continue;
                //if (!JsonDoc.Files[root.Name].Messages[t.Name].BaseClass.StartsWith(" : ConfigObjectBase"))
                //    continue;
                this.messages.Add(t);
            }
        }
        private bool IsBaseWithParent(string mesName)
        {
            var res = JsonDoc.Files[root.Name].Messages[mesName].IsConfigObjectBase;
            return res;
        }
    }
}
