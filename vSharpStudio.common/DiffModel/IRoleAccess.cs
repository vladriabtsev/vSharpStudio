using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common.DiffModel
{
    public interface IRoleAccess
    {
        void InitRoles();
        void InitRoleAdd(IRole role);
        void InitRoleRemove(IRole role);
        object GetRoleAccess(IRole role);
    }
    public interface IConstantAccessRoles
    {
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumConstantAccess access);
    }
    public interface IPropertyAccessRoles
    {
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPropertyAccess access);
    }
    public interface ICatalogDetailAccessRoles
    {
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access);
    }
    public interface IDocumentAccessRoles
    {
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumDocumentAccess access);
    }
}
