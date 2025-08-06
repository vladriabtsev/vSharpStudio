namespace vSharpStudio.common
{
    public partial interface IMainViewForm : ITreeConfigNodeSortable
    {
        IGroupListMainViewForms ParentGroupListMainViewFormsI { get; }
    }
}
