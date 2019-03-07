using System;
using Xunit;

namespace CodeForTT.xUnit
{
  public class UnitTests
  {
    [Fact]
    public void TestGetProtoClasses()
    {
      //C:\Ext\dev\vsharpstudio\generators\CodeForTT.xUnit\bin\Debug\netcoreapp2.2
      //C:\Ext\dev\vsharpstudio\vSharpStudio.proto\bin\Debug\netstandard2.0
      var lst = Utils.GetProtoClassDescs(@"..\..\..\..\..\vSharpStudio.proto\bin\Debug\netstandard2.0\vSharpStudio.proto.dll", "Proto.Config");
    }
  }
}
