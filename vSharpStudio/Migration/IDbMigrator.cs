using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace vSharpStudio.Migration
{
    public interface IDbMigrator
    {
        DatabaseModel GetDbModel();
        int GetMigrationVersion();
    }
}
