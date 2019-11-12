using System;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
  interface IDiffToDb
  {
    // Only to test(or not?)
    // return true if OK
    bool CompareConfigWithDb(Config oldConfig);

    void MigrateDbStructure(Config newConfig);

    void RemoveIndexes();

    void CreateIndexes(Config newConfig);

    // TODO rename, create new, migrate data ???
    // TODO data to protobuf format in file ???
    void BackupDb();

    void RestoreDb();
  }
}
