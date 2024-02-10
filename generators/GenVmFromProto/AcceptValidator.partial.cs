﻿using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenVmFromProto
{
    public partial class AcceptValidator
    {
        readonly FileDescriptor root;
        readonly MessageDescriptor message;
        readonly MessageDoc Doc;
        public AcceptValidator(FileDescriptor root, MessageDescriptor message)
        {
            this.root = root;
            this.message = message;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
        }
    }
}