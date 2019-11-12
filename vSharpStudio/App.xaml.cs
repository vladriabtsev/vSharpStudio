using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using vSharpStudio.Views;

namespace vSharpStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceCollection ServiceCollection;
        private ILogger logger;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            this.logger = loggerFactory.CreateLogger<App>();
            this.logger.LogInformation("Application is starting");

            #region DI services

            // https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff921152(v=pandp.10)
            // https://msdn.microsoft.com/en-us/magazine/mt707534.aspx
            App.ServiceCollection = new ServiceCollection();
            App.ServiceCollection.Add(ServiceDescriptor.Singleton<ILoggerFactory>(loggerFactory));
            this.logger.LogInformation("ILoggerFactory is added in servce collection");

            #endregion DI services

            MainWindow wnd = new MainWindow();
            wnd.Show();
            if (e.Args.Length == 1)
            {
                this.logger.LogInformation("Application solution path from command parameter:" + e.Args[0]);
                MainPage.MainPageVM.Config.SolutionPath = e.Args[0];
            }
        }
    }
}
