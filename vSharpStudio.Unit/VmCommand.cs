using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModelBase;

namespace vSharpStudio.Unit
{
    //public static class AssertExtensions
    //{
    //    public static Task<T> ThrowsExceptionAsync<T>(Func<Task> action)
    //        where T : Exception
    //    {
    //        return ThrowsExceptionAsync<T>(action, string.Empty, null);
    //    }

    //    public static Task<T> ThrowsExceptionAsync<T>(Func<Task> action, string message)
    //        where T : Exception
    //    {
    //        return ThrowsExceptionAsync<T>(action, message, null);
    //    }

    //    public async static Task<T> ThrowsExceptionAsync<T>(Func<Task> action, string message, object[] parameters)
    //        where T : Exception
    //    {
    //        try
    //        {
    //            await action();
    //        }
    //        catch (Exception ex)
    //        {
    //            if (ex.GetType() == typeof(T))
    //            {
    //                return ex as T;
    //            }

    //            var objArray = new object[] {
    //            "AssertExtensions.ThrowsExceptionAsync",
    //            string.Format(CultureInfo.CurrentCulture, FrameworkMessages.WrongExceptionThrown, message, typeof(T).Name, ex.GetType().Name, ex.Message, ex.StackTrace)
    //        };

    //            throw new AssertFailedException(string.Format(CultureInfo.CurrentCulture, FrameworkMessages.AssertionFailed, objArray));
    //        }

    //        var objArray2 = new object[] {
    //        "AssertExtensions.ThrowsExceptionAsync",
    //        string.Format(CultureInfo.CurrentCulture, FrameworkMessages.NoExceptionThrown, message, typeof(T).Name)
    //    };

    //        throw new AssertFailedException(string.Format(CultureInfo.CurrentCulture, FrameworkMessages.AssertionFailed, objArray2));
    //    }
    //}
    [TestClass]
    public class VmCommand
    {
        private readonly ILogger _logger = AppLogger.CreateLogger(nameof(VmCommand));
        static VmCommand()
        {
        }
        // public VmTests(ITestOutputHelper output)
        public VmCommand()
        {
            VmBindable.isUnitTests = true;
            // ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            // loggerFactory.AddProvider(new DebugLoggerProvider());
            // _logger = loggerFactory.CreateLogger<VmTests>();
            // _logger.LogInformation("======================  Start VmTests tests ===============================");
        }

        #region AsyncCommand

        [TestMethod]
        public async Task ExecuteFuncAsync()
        {
            int i = 0;
            await vCommandAsync.ExecuteActionAsync(() =>
            {
                i = 5;
            });
            Assert.AreEqual(5, i);
        }
        [TestMethod]
        public async Task ExecuteFuncAsyncInt()
        {
            var res = await vCommandAsync<int>.ExecuteFuncAsync(() =>
                {
                    return 5;
                });
            Assert.AreEqual(5, res);
        }
        [TestMethod]
        public async Task ExecuteFuncAsyncWithException()
        {
            await Assert.ThrowsExactlyAsync<Exception>(async () =>
            {
                await vCommandAsync.ExecuteActionAsync(() =>
                {
                    throw new Exception("test");
                });
            });
        }
        [TestMethod]
        public async Task ExecuteFuncAsyncIntWithException()
        {
            await Assert.ThrowsExactlyAsync<Exception>(async () =>
            {
                await vCommandAsync<int>.ExecuteFuncAsync(() =>
                {
                    throw new Exception("test");
                });
            });
        }
        [TestMethod]
        public async Task ExecuteFuncAsyncWithProgress()
        {
            var localProgress = new ProgressVM();
            int i = 0;
            await vCommandAsync.ExecuteActionAsync(localProgress, (progress, onprogress) =>
            {
                for (; i < 10; i++)
                {
                    progress.Progress = i;
                    onprogress();
                }
            });
            Assert.AreEqual(10, i);
            Assert.AreEqual(9, localProgress.Progress);
        }
        [TestMethod]
        public async Task ExecuteFuncAsyncWithProgressInt()
        {
            var localProgress = new ProgressVM();
            int i = 0;
            var res = await vCommandAsync<int>.ExecuteFuncAsync(localProgress, (progress, onprogress) =>
            {
                for (; i < 10; i++)
                {
                    progress.Progress = i;
                    onprogress();
                }
                return i;
            });
            Assert.AreEqual(10, i);
            Assert.AreEqual(9, localProgress.Progress);
        }
        [TestMethod]
        public async Task AsyncCommandIntWithProgress()
        {
            var localProgress = new ProgressVM();
            int i = 0;
            var command = vCommandAsync<int>.Create(localProgress, (token, progress, onprogress) =>
            {
                for (; i < 10; i++)
                {
                    progress.Progress = i;
                    onprogress();
                }
                return i;
            }, (o) => { return true; });
            await command.ExecuteAsync(null);
            Assert.AreEqual(10, i);
            Assert.AreEqual(9, localProgress.Progress);
        }
        [TestMethod]
        public async Task AsyncCommandIntExecuteAsync()
        {
            var command = vCommandAsync<int>.Create((c) =>
            {
                return 5;
            }, (o) => { return true; });
            await command.ExecuteAsync(null);
            Assert.AreEqual(5, command.Execution.Result);
            Assert.IsTrue(string.IsNullOrEmpty(command.Execution.ErrorMessage));
            Assert.IsFalse(command.Execution.IsFaulted);
            Assert.IsFalse(command.Execution.IsNotCompleted);
            Assert.IsTrue(command.Execution.IsCompleted);
            Assert.IsFalse(command.Execution.IsCanceled);
            Assert.IsNull(command.Execution.Exception);
            Assert.IsNull(command.Execution.InnerException);
            Assert.IsTrue(command.Execution.IsSuccessfullyCompleted);
        }
        [TestMethod]
        public void AsyncCommandIntExecute()
        {
            var command = vCommandAsync<int>.Create((c) =>
            {
                return 5;
            }, (o) => { return true; });
            command.Execute(null);
            Assert.AreEqual(5, command.Execution.Result);
            Assert.IsTrue(string.IsNullOrEmpty(command.Execution.ErrorMessage));
            Assert.IsFalse(command.Execution.IsFaulted);
            Assert.IsFalse(command.Execution.IsNotCompleted);
            Assert.IsTrue(command.Execution.IsCompleted);
            Assert.IsFalse(command.Execution.IsCanceled);
            Assert.IsNull(command.Execution.Exception);
            Assert.IsNull(command.Execution.InnerException);
            Assert.IsTrue(command.Execution.IsSuccessfullyCompleted);
        }
        [TestMethod]
        public async Task AsyncCommandIntWithException()
        {
            await Assert.ThrowsExactlyAsync<Exception>(async () =>
            {
                var command = vCommandAsync<int>.Create((c) =>
                {
                    throw new Exception("test");
                }, (o) => { return true; });
                await command.ExecuteAsync(null, false);
            });
        }
        [TestMethod]
        public async Task AsyncCommandIntWithExceptionCatch()
        {
            var command = vCommandAsync<int>.Create((c) =>
            {
                throw new Exception("test");
            }, (o) => { return true; });
            await command.ExecuteAsync(null, true);
            Assert.AreEqual("test", command.Execution.ErrorMessage);
            Assert.IsTrue(command.Execution.IsFaulted);
            Assert.IsFalse(command.Execution.IsNotCompleted);
            Assert.IsTrue(command.Execution.IsCompleted);
            Assert.IsFalse(command.Execution.IsCanceled);
            Assert.IsNotNull(command.Execution.Exception);
            Assert.IsNotNull(command.Execution.InnerException);
            Assert.IsFalse(command.Execution.IsSuccessfullyCompleted);
        }
        [TestMethod]
        public async Task AsyncCommandIntExecuteAsyncWithCancellation()
        {
            var command = vCommandAsync<int>.Create((cancellation) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    if (cancellation.IsCancellationRequested)
                        return -1;
                }
                return 5;
            }, (o) => { return true; });
            command.CancelCommand.Execute(null);
            await command.ExecuteAsync(null);

            Assert.IsTrue(command.IsCanceled);
            Assert.IsFalse(command.IsFaulted);
            Assert.IsNull(command.Exception);
            Assert.IsNull(command.InnerException);
            Assert.IsTrue(string.IsNullOrEmpty(command.ErrorMessage));

            Assert.AreEqual(-1, command.Execution.Result);
            Assert.IsTrue(string.IsNullOrEmpty(command.Execution.ErrorMessage));
            Assert.IsFalse(command.Execution.IsFaulted);
            Assert.IsFalse(command.Execution.IsNotCompleted);
            Assert.IsTrue(command.Execution.IsCompleted);
            Assert.IsFalse(command.Execution.IsCanceled);
            Assert.IsNull(command.Execution.Exception);
            Assert.IsNull(command.Execution.InnerException);
            Assert.IsTrue(command.Execution.IsSuccessfullyCompleted);
        }

        #endregion AsyncCommand
    }
}
