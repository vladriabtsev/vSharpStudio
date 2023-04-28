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
}
