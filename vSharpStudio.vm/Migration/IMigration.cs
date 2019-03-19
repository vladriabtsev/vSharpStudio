using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
    public interface IMigration
    {
        void InitMigration();
        bool DatabaseExist();
        bool CreateDatabase();
        DatabaseModel GetDatabaseModel();
        List<EntityObjectProblem> GetUpdateDbProblems();
        void UpdateDb();
    }
}
