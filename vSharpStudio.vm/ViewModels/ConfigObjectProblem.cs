using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public struct ConfigObjectProblem
    {
        public IValidatableWithSeverity EntityObject;
        public string Message;
    }
}
