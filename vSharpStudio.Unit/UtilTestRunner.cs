using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

// https://www.chriswirz.com/software/programmatically-run-unit-tests-in-c-sharp
// https://stackoverflow.com/questions/195061/how-to-run-nunit-programmatically
// https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2008/ms182524(v=vs.90)?redirectedfrom=MSDN
// https://www.codeproject.com/Tips/715891/Compiling-Csharp-Code-at-Runtime
// https://support.microsoft.com/en-ca/help/304655/how-to-programmatically-compile-code-using-c-compiler
// https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/may/net-core-cross-platform-code-generation-with-roslyn-and-net-core
// https://github.com/dotnet/roslyn/wiki/Runtime-code-generation-using-Roslyn-compilations-in-.NET-Core-App
// https://laurentkempe.com/2019/02/18/dynamically-compile-and-run-code-using-dotNET-Core-3.0/
// https://carlos.mendible.com/2017/01/29/net-core-roslyn-and-code-generation/
namespace vSharpStudio.Unit
{
    //[TestClass]
    //public class MyTests
    //{
    //    [ClassInitialize] // runs at the beginning of testing
    //    public void SetupClass()
    //    {
    //    }
    //    [TestInitialize] // runs at the beginning of each test
    //    public void SetupTest()
    //    {
    //    }
    //    [TestMethod]
    //    [Priority(0)]
    //    public void RunFirstTest()
    //    {
    //        Assert.IsTrue(1 == 1);
    //    }
    //    [TestMethod]
    //    [Priority(1)]
    //    public void RunSecondTest()
    //    {
    //        Assert.IsFalse(1 == 2);
    //    }
    //    [TestCleanup] // runs at the end of each test
    //    public void CleanupTest()
    //    {
    //    }
    //    [ClassCleanup] // runs after all tests
    //    public void CleanupClass()
    //    {
    //    }
    //}
    public class UtilTestRunner
    {
        public void Test()
        {
            var asm = Assembly.Load("MyTestAssembly.dll");

            // Get all the test classes in the assembly
            var testClassTypes = asm.GetTypes()
                .Where(t => t.CustomAttributes.Any(a => a.AttributeType.Name == "TestClassAttribute"));

            foreach (var t in testClassTypes)
            {

                // Get all the test methods in each test class
                var methods = t.GetMethods().Where(m => m.CustomAttributes
                    .Any(a => a.AttributeType.Name == "TestMethodAttribute")
                        && m.CustomAttributes.Any(a => a.AttributeType.Name == "PriorityAttribute"))
                    .ToList();

                // Order the methods by priority
                var orderedMethods = methods
                    .OrderBy(m =>
                        m.CustomAttributes.Last(a => a.AttributeType.Name == "PriorityAttribute")
                        .ConstructorArguments.First().Value
                    );

                // A test class should have a parameterless constructor
                var tc = Activator.CreateInstance(t, null);

                // Run the ordered test methods
                foreach (var m in orderedMethods)
                {
                    // Invoke the test method
                    // An error will be thrown if it fails
                    m.Invoke(tc, null);
                }
            }

        }
    }
}
