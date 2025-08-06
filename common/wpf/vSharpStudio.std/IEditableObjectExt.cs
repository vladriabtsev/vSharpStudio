using System.ComponentModel;

namespace ViewModelBase
{
    public interface IEditableObjectExt : IEditableObject
    {
        static bool IsTraceChanges { get; set; } = true;
        bool IsChanged { get; set; }
        bool IsInEdit { get; }
    }
}
