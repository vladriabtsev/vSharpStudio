using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.common
{
    // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
    // https://docs.microsoft.com/en-us/dotnet/framework/mef/
    /// <summary>
    /// Interface for connection string generator model
    /// </summary>
    public interface IvPluginDbConnStringGenerator : IvPluginGenerator, IvPluginGeneratorSettings
    {
        // Connection string generator is providing latest DB Connection String after 
        // appropriate generator model settings are changed
        Action<string> OnConnectionStringChanged { set; }
        // Update generator model settings from connection string
        INotifyPropertyChanged ConnectionStringToVM(string connString);
        // Provider name for connection string
        string ProviderName { get; }
        // DB schema generator for provider
        IvPluginDbGenerator DbGenerator { get; }
    }
}
