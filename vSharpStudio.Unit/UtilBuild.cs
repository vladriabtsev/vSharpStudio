﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

// https://josephwoodward.co.uk/2016/12/in-memory-c-sharp-compilation-using-roslyn
// https://joshvarty.com/learn-roslyn-now/
// https://www.codeproject.com/Articles/850799/Implementing-Adapter-Pattern-and-Imitating-Multipl
// https://www.codeproject.com/Articles/857156/Roslyn-based-Simulated-Multiple-Inheritance-Usage
// https://www.codeproject.com/Articles/857480/Roslyn-based-Simulated-Multiple-Inheritance-Usag
namespace vSharpStudio.Unit
{
    class UtilBuild
    {
        void Kuku(string[] args)
        {
            string solutionUrl = "C:\\Dev\\Roslyn.TryItOut\\Roslyn.TryItOut.sln";
            string outputDir = "C:\\Dev\\Roslyn.TryItOut\\output";

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            bool success = CompileSolution(solutionUrl, outputDir);

            if (success)
            {
                Console.WriteLine("Compilation completed successfully.");
                Console.WriteLine("Output directory:");
                Console.WriteLine(outputDir);
            }
            else
            {
                Console.WriteLine("Compilation failed.");
            }

            Console.WriteLine("Press the any key to exit.");
            Console.ReadKey();
        }

        private static bool CompileSolution(string solutionUrl, string outputDir)
        {
            bool success = true;

            MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            Solution solution = workspace.OpenSolutionAsync(solutionUrl).Result;
            ProjectDependencyGraph projectGraph = solution.GetProjectDependencyGraph();
            Dictionary<string, Stream> assemblies = new Dictionary<string, Stream>();

            foreach (ProjectId projectId in projectGraph.GetTopologicallySortedProjects())
            {
                Compilation projectCompilation = solution.GetProject(projectId).GetCompilationAsync().Result;
                if (null != projectCompilation && !string.IsNullOrEmpty(projectCompilation.AssemblyName))
                {
                    using (var stream = new MemoryStream())
                    {
                        Microsoft.CodeAnalysis.Emit.EmitResult result = projectCompilation.Emit(stream);
                        if (result.Success)
                        {
                            string fileName = string.Format("{0}.dll", projectCompilation.AssemblyName);

                            using (FileStream file = File.Create(outputDir + '\\' + fileName))
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.CopyTo(file);
                            }
                        }
                        else
                        {
                            success = false;
                        }
                    }
                }
                else
                {
                    success = false;
                }
            }

            return success;
        }
    }
}
