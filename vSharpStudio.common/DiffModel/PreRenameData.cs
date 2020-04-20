using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common.DiffModel
{
    public class PreRenameData
    {
        public PreRenameData(string nmsp, string classNameBeforeRename, string classNameNew)
        {
            this.Namespace = nmsp;
            this.ClassName = classNameBeforeRename;
            this.ClassNameNew = classNameNew;
            this.ListRenamedProperties = new List<RenamePropertyData>();
        }
        public string Namespace { get; private set; }
        public string ClassName { get; private set; }
        public string ClassNameNew { get; private set; }
        public List<RenamePropertyData> ListRenamedProperties { get; private set; }
    }
    public class RenamePropertyData
    {
        public RenamePropertyData(string propNamePrev, string propNameNew)
        {
            this.PropName = propNamePrev;
            this.PropNameNew = propNameNew;
        }
        public string PropName { get; private set; }
        public string PropNameNew { get; private set; }
    }
}
