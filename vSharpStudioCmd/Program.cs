using System;
using System.Collections.Generic;
using CommandLine;
using vSharpStudio.ViewModels;

namespace vSharpStudioCmd
{
    // https://github.com/commandlineparser/commandline
    // https://github.com/Tyrrrz/CliFx
    class Program
    {
        public class Options
        {
            [Option('c', "config", Required = true, HelpText = "Input config files to be processed.")]
            public IEnumerable<string> InputFiles { get; set; }
        }
        static int Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
                {
                    foreach (var t in o.InputFiles)
                    {
                        var vm = new MainPageVM(true, t);
                        vm.OnFormLoaded();
                        vm.CommandConfigCurrentUpdate.Execute(null);
                    }
                });
            }
            catch(Exception ex)
            {
                return -1;
            }
            return 0;
        }
    }
}
