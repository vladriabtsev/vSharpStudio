namespace vSharpStudio.common
{
    public partial interface IGroupListProperties : ITreeConfigNodeSortable
    {
        uint GetNextPosition();
        int IndexOf(IProperty p);
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        EnumPrintAccess GetRolePropertyPrint(IRole role);
    }
}
