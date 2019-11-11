using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.Migration
{
    internal class SchemaReader
    {
        private string projectPathWithDbConnectionString;
        private string connectionStringName;
        internal SchemaReader(string projectPathWithDbConnectionString, string connectionStringName = null)
        {
            this.projectPathWithDbConnectionString = projectPathWithDbConnectionString;
            this.connectionStringName = connectionStringName;
        }
    }
    internal abstract class SchemaReaderBase
    {
        public abstract Tables ReadSchema(DbConnection connection, DbProviderFactory factory);
        public void WriteLine(string o)
        {
            System.Diagnostics.Trace.WriteLine(o);
        }
        public void Warning(string o)
        {
            System.Diagnostics.Trace.WriteLine(o);
        }
        // https://github.com/DotNetPlus/ReswPlus/wiki/Features:-Pluralization-support
        internal static string Singularize(string word)
        {
            var singularword = word; // System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(System.Globalization.CultureInfo.GetCultureInfo("en-us")).Singularize(word);
            return singularword;
        }
        internal static string Pluralize(string word)
        {
            var pluralWord = word; // System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(System.Globalization.CultureInfo.GetCultureInfo("en-us")).Pluralize(word);
            return pluralWord;
        }
        internal static string RemoveTablePrefixes(string word)
        {
            var cleanword = word;
            if (cleanword.StartsWith("tbl_")) cleanword = cleanword.Replace("tbl_", "");
            if (cleanword.StartsWith("tbl")) cleanword = cleanword.Replace("tbl", "");
            cleanword = cleanword.Replace("_", "");
            return cleanword;
        }
        static string[] cs_keywords = { "abstract", "event", "new", "struct", "as", "explicit", "null",
      "switch", "base", "extern", "object", "this", "bool", "false", "operator", "throw",
      "break", "finally", "out", "true", "byte", "fixed", "override", "try", "case", "float",
      "params", "typeof", "catch", "for", "private", "uint", "char", "foreach", "protected",
      "ulong", "checked", "goto", "public", "unchecked", "class", "if", "readonly", "unsafe",
      "const", "implicit", "ref", "ushort", "continue", "in", "return", "using", "decimal",
      "int", "sbyte", "virtual", "default", "interface", "sealed", "volatile", "delegate",
      "internal", "short", "void", "do", "is", "sizeof", "while", "double", "lock",
      "stackalloc", "else", "long", "static", "enum", "namespace", "string" };
        protected static Func<string, string> CleanUp = (str) =>
        {
            str = rxCleanUp.Replace(str, "_");

            if (char.IsDigit(str[0]) || cs_keywords.Contains(str))
                str = "@" + str;

            return str;
        };
        static Regex rxCleanUp = new Regex(@"[^\w\d_]", RegexOptions.Compiled);
    }
}
