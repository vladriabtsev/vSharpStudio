using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf.Reflection;

namespace vSharpStudio.proto
{
    public static class Extentions
    {
        public static string ToLeadingComments(this DescriptorDeclaration d, string prefix = "")
        {
            if (d == null)
                return "";
            StringBuilder sb = new StringBuilder();
            string sep = "\r\n";
            string[] lines = null;
            foreach (var tt in d.LeadingDetachedComments)
            {
                lines = tt.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (var t in lines)
                {
                    sb.Append(prefix);
                    sb.Append("/// ");
                    sb.Append(t);
                }
                sb.Append(sep);
            }
            lines = d.LeadingComments.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var t in lines)
            {
                sb.Append(prefix);
                sb.Append("/// ");
                sb.Append(t);
            }
            return sb.ToString();
        }
        public static string ToTrailingComments(this DescriptorDeclaration d, string prefix = "")
        {
            if (d == null)
                return "";
            StringBuilder sb = new StringBuilder();
            var lines = d.TrailingComments.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var t in lines)
            {
                sb.Append(prefix);
                sb.Append("/// ");
                sb.Append(t);
            }
            return sb.ToString();
        }
        public static string ToLeadingDetachedComments(this DescriptorDeclaration d, string prefix = "")
        {
            if (d == null)
                return "";
            StringBuilder sb = new StringBuilder();
            string sep = "\r\n";
            foreach (var tt in d.LeadingDetachedComments)
            {
                var lines = tt.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (var t in lines)
                {
                    sb.Append(prefix);
                    sb.Append("/// ");
                    sb.Append(t);
                }
                sb.Append(sep);
            }
            return sb.ToString();
        }
        public static string ToLeadingAttachedComments(this DescriptorDeclaration d, string prefix = "")
        {
            if (d == null)
                return "";
            StringBuilder sb = new StringBuilder();
            var lines = d.LeadingComments.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var t in lines)
            {
                sb.Append(prefix);
                sb.Append("/// ");
                sb.Append(t);
            }
            return sb.ToString();
        }
    }
}
