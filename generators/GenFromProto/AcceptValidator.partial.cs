﻿using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class AcceptValidator
    {
        FileDescriptor root;
        MessageDescriptor message;
        MessageDoc Doc;
        public AcceptValidator(FileDescriptor root, MessageDescriptor message)
        {
            this.root = root;
            this.message = message;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
        }
    }
}
