using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace vSharpStudio.core
{
    public class ExtCore
    {
        // https://stackoverflow.com/questions/46940710/getting-value-from-appsettings-json-in-net-core
        public static class ConfigurationManagerJson
        {
            public static IConfiguration AppSetting { get; }
            static ConfigurationManagerJson()
            {
                AppSetting = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
            }
        }
    }
}
