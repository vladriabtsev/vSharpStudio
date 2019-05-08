using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using ViewModelBase;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;
using Xunit;
using Xunit.Abstractions;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.xUnit
{
    public class VmTests
    {
        ILogger _logger = null;
        public VmTests(ITestOutputHelper output)
        {
            ViewModelBindable.isUnitTests = true;
            ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            loggerFactory.AddProvider(new DebugLoggerProvider());
            _logger = loggerFactory.CreateLogger<VmTests>();
            _logger.LogInformation("======================  Start VmTests tests ===============================");
        }

        #region Async
        // https://blog.stephencleary.com/2012/02/async-and-await.html
        // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        [Fact]
        public void Async001_CanHandleException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => AsyncWithOneException());
        }
        Task AsyncWithOneException()
        {
            Task task = Task.Run(() =>
            {
                throw new ArgumentException("test 1");
            });
            return task;
        }
        //[Fact]
        //async public void Async002_TestAsync()
        //{
        //    await AsyncWithOneException();
        //}
        #endregion Async
    }
}
