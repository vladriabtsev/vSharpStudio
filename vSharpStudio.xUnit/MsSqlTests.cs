using OpenDBDiff.Front;
using System;
using System.IO;
using vSharpStudio.Migration;
using vSharpStudio.vm.ViewModels;
using Xunit;

namespace vSharpStudio.xUnit
{
  public class MsSqlTests
  {
    [Fact]
    public void MsSql001GuidInit()
    {
      //IDatabaseComparer
      var cfg = new Config();
      cfg.ConnectionStringName = "MsSql";
      cfg.PathToProjectWithConnectionString = Directory.GetCurrentDirectory()+@"..\..\app.config";
      var v = new MsSqlServerSchemaReader();
      //MsSqlModel v = new MsSqlModel();
      Assert.True(false);
    }
    }
  }
}
