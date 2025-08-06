using System;

namespace vSharpStudio.common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NodeParameter : Attribute
    {
        public string Path { get; private set; }
        public NodeParameter(string path)
        {
            this.Path = path;
        }
    }
}
