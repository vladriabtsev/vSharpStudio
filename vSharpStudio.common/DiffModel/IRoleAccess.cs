using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common.DiffModel
{
    public interface IRoleAccess
    {
        object GetRoleAccess(IRole role);
    }
}
