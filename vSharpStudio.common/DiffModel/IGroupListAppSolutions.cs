namespace vSharpStudio.common
{
    public partial interface IGroupListAppSolutions : ITreeConfigNodeSortable
    {
        IConfig ParentConfigI { get; }
        //string? TableNameValidation(string name);
        //string? FieldNameValidation(string name);
    }
}
