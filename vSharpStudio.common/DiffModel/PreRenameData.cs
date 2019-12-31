using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common.DiffModel
{
    public class PreRenameData
    {
        public PreRenameData(string nmsp, string classNameBeforeRename)
        {
            this.Namespace = nmsp;
            this.ClassNameBeforeRename = classNameBeforeRename;
            this.ListRenamedProperties = new List<RenamePropertyData>();
        }
        public string Namespace { get; private set; }
        public string ClassNameBeforeRename { get; private set; }
        public List<RenamePropertyData> ListRenamedProperties { get; private set; }
    }
    public class RenamePropertyData
    {
        public RenamePropertyData(string propName, string propNameNew)
        {
            this.PropName = propName;
            this.PropNameNew = propNameNew;
        }
        public string PropName { get; private set; }
        public string PropNameNew { get; private set; }
    }
}
