using System;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
  public class DiffToMsSql : IDiffToDb
  {
    void IDiffToDb.BackupDb()
    {
      throw new NotImplementedException();
    }

    bool IDiffToDb.CompareConfigWithDb(Config oldConfig)
    {
      throw new NotImplementedException();
    }

    void IDiffToDb.CreateIndexes(Config newConfig)
    {
      throw new NotImplementedException();
    }

    void IDiffToDb.MigrateDbStructure(Config newConfig)
    {
      throw new NotImplementedException();
    }

    void IDiffToDb.RemoveIndexes()
    {
      throw new NotImplementedException();
    }

    void IDiffToDb.RestoreDb()
    {
      throw new NotImplementedException();
    }
  }
}
