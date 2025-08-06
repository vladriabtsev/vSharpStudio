using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace vSharpStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceCollection? ServiceCollection;
        //private Microsoft.Extensions.Logging.ILogger? logger;

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            //#region DI services

            //// https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff921152(v=pandp.10)
            //// https://msdn.microsoft.com/en-us/magazine/mt707534.aspx
            //App.ServiceCollection = new ServiceCollection();
            //App.ServiceCollection.Add(ServiceDescriptor.Singleton<ILoggerFactory>(loggerFactory));
            //this.logger.LogInformation("ILoggerFactory is added in servce collection");

            //#endregion DI services

            MainWindow wnd = new MainWindow();
#if DEBUG
            // https://docs.microsoft.com/en-us/dotnet/desktop-wpf/data/data-binding-overview#debugging-mechanism
            //PresentationTraceSources.SetTraceLevel(wnd, PresentationTraceLevel.High);
#endif
            try
            {
                wnd.Show();
            }
            catch (Exception ex)
            {
                var res = MessageBox.Show($"Unhandled Exception\n{ex.Message}", "Error", System.Windows.MessageBoxButton.OK);
                throw;
            }
            //if (e.Args.Length == 1)
            //{
            //    this.logger.LogInformation("Application solution path from command parameter:" + e.Args[0]);
            //    MainPage.MainPageVM.Config.SolutionPath = e.Args[0];
            //}
        }
    }
}
