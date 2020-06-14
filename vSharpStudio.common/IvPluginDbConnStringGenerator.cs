using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.common
{
    // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
    // https://docs.microsoft.com/en-us/dotnet/framework/mef/
    /// <summary>
    /// Interface for DbDesign type of plugins
    /// </summary>
    public interface IvPluginDbConnStringGenerator : IvPluginGenerator, IvPluginGeneratorSettings
    {
        string ProviderName { get; }
        //string ConnectionString { get; set; }
        IvPluginDbGenerator DbGenerator { get; }
    }
}
