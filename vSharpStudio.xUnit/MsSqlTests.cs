using System;
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
      var v = new MsSqlServerSchemaReader();
      //MsSqlModel v = new MsSqlModel();
      Assert.True(false);
    }
  }
}
