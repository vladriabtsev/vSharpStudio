using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
    public interface IMigration
    {
        //void InitMigration();
        bool IsDatabaseServiceOn();
        Task<bool> IsDatabaseServiceOnAsync(CancellationToken cancellationToken = default);
        bool IsDatabaseExists();
        Task<bool> IsDatabaseExistsAsync(CancellationToken cancellationToken = default);
        void CreateDatabase();
        Task CreateDatabaseAsync(CancellationToken cancellationToken = default);
        void DropDatabase();
        Task DropDatabaseAsync(CancellationToken cancellationToken = default);
        //DatabaseModel GetDatabaseModel();
        //void UpdateDb();
    }
}
