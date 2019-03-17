using System;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
    public interface IMigration
    {
        List<EntityObjectProblem> GetUpdateDbProblems();
        void UpdateDb();
    }
}
