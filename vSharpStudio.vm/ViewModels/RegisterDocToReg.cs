namespace vSharpStudio.vm.ViewModels
{
    //[DebuggerDisplay("RegisterDocToReg: Doc:{RelativeAppProjectPath,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class RegisterDocToReg
    {
        partial void OnCreated()
        {
            this._Guid = System.Guid.NewGuid().ToString();
        }
    }
}
