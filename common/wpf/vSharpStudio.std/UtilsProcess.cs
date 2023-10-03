using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;

// https://stackoverflow.com/questions/10788982/is-there-any-async-equivalent-of-process-start
namespace ViewModelBase
{
    public static class UtilsProcess
    {
        public static async Task AwaitWithTimeout<TResult>(this Task<TResult> task, int timeout, Action<TResult> onFinishAction, Action timeoutAction)
        {
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                onFinishAction(task.Result);
            }
            else
            {
                timeoutAction();
            }
        }
        public static async Task AwaitWithTimeout(this Task task, int timeout, Action onFinishAction, Action timeoutAction)
        {
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                onFinishAction();
            }
            else
            {
                timeoutAction();
            }
        }

        //public static Task RunProcessAsync(string processPath)
        //{
        //    var tcs = new TaskCompletionSource<object>();
        //    var process = new Process
        //    {
        //        EnableRaisingEvents = true,
        //        StartInfo = new ProcessStartInfo(processPath)
        //        {
        //            RedirectStandardError = true,
        //            UseShellExecute = false
        //        }
        //    };
        //    process.Exited += (sender, args) =>
        //    {
        //        if (process.ExitCode != 0)
        //        {
        //            var errorMessage = process.StandardError.ReadToEnd();
        //            tcs.SetException(new InvalidOperationException("The process did not exit correctly. " +
        //                "The corresponding error message was: " + errorMessage));
        //        }
        //        else
        //        {
        //            tcs.SetResult(null);
        //        }
        //        process.Dispose();
        //    };
        //    process.Start();
        //    return tcs.Task;
        //}
        public static async Task<int> RunProcessAsync(string fileName, string args)
        {
            using (var process = new Process
            {
                StartInfo =
                {
                    FileName = fileName, Arguments = args,
                    UseShellExecute = false, CreateNoWindow = true,
                    RedirectStandardOutput = true, RedirectStandardError = true
                },
                EnableRaisingEvents = true
            })
            {
                return await RunProcessAsync(process).ConfigureAwait(false);
            }
        }
        private static Task<int> RunProcessAsync(Process process)
        {
            var tcs = new TaskCompletionSource<int>();
            process.Exited += (s, ea) =>
            {
                tcs.SetResult(process.ExitCode);
                if (process.ExitCode != 0)
                {
                    var errorMessage = process.StandardError.ReadToEnd();
                    tcs.SetException(new InvalidOperationException(errorMessage));
                }
            };
            //process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
            //process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

            bool started = process.Start();
            if (!started)
            {
                //you may allow for the process to be re-used (started = false) 
                //but I'm not sure about the guarantees of the Exited event in such a case
                ThrowHelper.ThrowInvalidOperationException("Could not start process: " + process);
            }

            //process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }
    }
}
