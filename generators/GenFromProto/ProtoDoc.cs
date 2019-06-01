using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public class ProtoDoc
    {
        public static void CreateDoc(string path)
        {
            Proto.Doc.json_doc jsonDoc = new Proto.Doc.json_doc();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                jsonDoc = Proto.Doc.json_doc.Parser.ParseJson(json);
                JsonDoc.CreateDesc(jsonDoc);
            }
        }
    }
    public class JsonDoc
    {
        public static void CreateDesc(Proto.Doc.json_doc jsonDoc)
        {
            foreach (var t in jsonDoc.Files)
            {
                Files[t.Name] = new FileDoc(t);
            }

        }
        public static Dictionary<string, FileDoc> Files = new Dictionary<string, FileDoc>();
    }
    public class FileDoc
    {
        public FileDoc() { }
        public FileDoc(Proto.Doc.file file)
        {
            this.file = file;
            foreach (var t in file.Enums)
            {
                Enums[t.Name] = new EnumDoc(t);
            }
            foreach (var t in file.Messages)
            {
                Messages[t.Name] = new MessageDoc(t);
            }
        }
        public Proto.Doc.file file;
        public Dictionary<string, EnumDoc> Enums = new Dictionary<string, EnumDoc>();
        public Dictionary<string, MessageDoc> Messages = new Dictionary<string, MessageDoc>();
    }
    public class EnumDoc
    {
        public EnumDoc(Proto.Doc.enums enums)
        {
            this.enums = enums;
            foreach (var t in enums.Values)
            {
                Values[t.Name] = new ValueDoc(t);
            }
            string base0;
            this.Comments = MessageDoc.GetComments(enums.Name, enums.Description, out this.Attributes, out base0);
            if (!string.IsNullOrEmpty(base0))
                throw new Exception("Base class name is unexpected for enum value:" + enums.Name);

        }
        public Proto.Doc.enums enums;
        public string Attributes = null;
        public string Comments = null;
        public Dictionary<string, ValueDoc> Values = new Dictionary<string, ValueDoc>();
    }
    public class ValueDoc
    {
        public ValueDoc(Proto.Doc.value value)
        {
            this.value = value;
        }
        public Proto.Doc.value value;
    }
    public class MessageDoc
    {
        public MessageDoc(Proto.Doc.message message)
        {
            this.message = message;
            foreach (var t in message.Fields)
            {
                Fields[t.Name] = new FieldDoc(t);
            }
            this.Comments = MessageDoc.GetComments(message.Name, message.Description, out this.Attributes, out this.BaseClass);
            if (!string.IsNullOrEmpty(this.BaseClass))
                this.IsDefaultBase = false;
        }

        public static string GetComments(string name, string description, out string attributes, out string baseClass)
        {
            StringBuilder comments = new StringBuilder();
            StringBuilder attrs = new StringBuilder();
            string base0 = "";
            if (!string.IsNullOrWhiteSpace(description))
            {
                string s = description;
                var lines = s.Split('\n');
                foreach (var t in lines)
                {
                    if (t.StartsWith("@attr"))
                    {
                        string ss = t.Substring(5).Trim();
                        var atrar = ss.Split(']');
                        foreach (var tt in atrar)
                        {
                            if (tt == "")
                                continue;
                            attrs.Append(tt.Trim());
                            attrs.AppendLine("]");
                        }

                    }
                    else if (t.StartsWith("@base"))
                    {
                        if (base0 != "")
                            throw new Exception("@base is specified more than one time for message:" + name);
                        base0 = t.Substring(5);
                    }
                    else
                    {
                        if (comments.Length == 0)
                        {
                            comments.AppendLine();
                            comments.AppendLine("///////////////////////////////////////////////////");
                        }
                        comments.Append("/// ");
                        comments.AppendLine(t);
                    }
                }
                if (comments.Length > 0)
                {
                    comments.AppendLine("///////////////////////////////////////////////////");
                }
            }
            baseClass = base0;
            attributes = attrs.ToString();
            return comments.ToString();
        }

        public Proto.Doc.message message;
        public bool IsDefaultBase = true;
        public string BaseClass = null;
        public string Attributes = null;
        public string Comments = null;
        public Dictionary<string, FieldDoc> Fields = new Dictionary<string, FieldDoc>();
    }
    public class FieldDoc
    {
        public FieldDoc(Proto.Doc.field field)
        {
            this.field = field;
            string base0;
            this.Comments = MessageDoc.GetComments(field.Name, field.Description, out this.Attributes, out base0);
            if (!string.IsNullOrEmpty(base0))
                throw new Exception("Base class name is unexpected for field:" + field.Name);
        }
        public Proto.Doc.field field;
        public string Attributes = null;
        public string Comments = null;
    }
}
