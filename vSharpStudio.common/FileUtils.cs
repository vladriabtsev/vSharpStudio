using System;
using System.IO;
using System.Text;
using Polly;

namespace vSharpStudio.common
{
    public static class FileUtils
    {
        public static Policy RetryPolicy = Policy
            .Handle<IOException>()
            .WaitAndRetry(new[]
            {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20)
            }, (exception, timeSpan) =>
            {
                //if (exception.InnerException != null)
                //{
                //}
            });
        public static void WriteToFileIfNotExist(string code, string path, string fileRelativePath, string fileName)
        {
            string outFolder = Path.Combine(path, fileRelativePath);
            string outFile;
            if (Path.EndsInDirectorySeparator(outFolder))
                outFile = $"{outFolder}{fileName}";
            else
                outFile = $"{outFolder}\\{fileName}";
            if (!File.Exists(outFile))
            {
                var encod = new UTF8Encoding(true);
                byte[] bytes = encod.GetBytes(code);
                FileUtils.WritesAllBytesWithRetryIfTextIsChanged(outFile, bytes);
            }
        }
        public static void WriteToFile(string code, string path, string fileRelativePath, string fileName)
        {
            var encod = new UTF8Encoding(true);
            byte[] bytes = encod.GetBytes(code);
            string outFolder = Path.Combine(path, fileRelativePath ?? "");
            string outFile;
            if (Path.EndsInDirectorySeparator(outFolder))
                outFile = $"{outFolder}{fileName}";
            else
                outFile = $"{outFolder}\\{fileName}";
            FileUtils.WritesAllBytesWithRetryIfTextIsChanged(outFile, bytes);
        }
        public static void WriteToFile(string code, string path, string fileName)
        {
            var encod = new UTF8Encoding(true);
            byte[] bytes = encod.GetBytes(code);
            string outFile = path;
            if (Path.EndsInDirectorySeparator(path))
                outFile = $"{path}{fileName}";
            else
                outFile = $"{path}\\{fileName}";
            FileUtils.WritesAllBytesWithRetryIfTextIsChanged(outFile, bytes);
        }
        public static void WriteToFile(string code, string outFilePath)
        {
            var encod = new UTF8Encoding(true);
            byte[] bytes = encod.GetBytes(code);
            FileUtils.WritesAllBytesWithRetryIfTextIsChanged(outFilePath, bytes);
        }
        public static void WriteToFile(byte[] bytes, string path, string fileRelativePath, string fileName)
        {
            string outFolder = Path.Combine(path, fileRelativePath);
            string outFile;
            if (Path.EndsInDirectorySeparator(outFolder))
                outFile = $"{outFolder}{fileName}";
            else
                outFile = $"{outFolder}\\{fileName}";
            FileUtils.WritesAllBytesWithRetryIfTextIsChanged(outFile, bytes);
        }
        public static void WritesAllBytesWithRetryIfTextIsChanged(string outFile, byte[] bytes)
        {
            bool isRewrite = false;
            var bytesCurrent = new byte[0];
            if (File.Exists(outFile))
            {
                bytesCurrent = File.ReadAllBytes(outFile);
                isRewrite = bytesCurrent.Length != bytes.Length;
            }
            else
                isRewrite = true;
            if (!isRewrite)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] != bytesCurrent[i])
                    {
                        isRewrite = true;
                        break;
                    }
                }
            }
            if (!isRewrite)
                return;
            FileUtils.RetryPolicy.Execute(() =>
            {
                File.WriteAllBytes(outFile, bytes);
            });
        }
        public static void WriteAllTextWithRetryIfTextIsChanged(string outFile, string? contents, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            bool isRewrite = false;
            byte[] bytes = [];
            if (contents != null)
            {
                bytes = encoding.GetBytes(contents);
            }
            var bytesCurrent = new byte[0];
            if (File.Exists(outFile))
            {
                bytesCurrent = File.ReadAllBytes(outFile);
                isRewrite = bytesCurrent.Length != bytes.Length;
            }
            else
                isRewrite = true;
            if (!isRewrite)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] != bytesCurrent[i])
                    {
                        isRewrite = true;
                        break;
                    }
                }
            }
            if (!isRewrite)
                return;
            FileUtils.RetryPolicy.Execute(() =>
            {
                File.WriteAllText(outFile, contents, encoding);
            });
        }
    }
}
