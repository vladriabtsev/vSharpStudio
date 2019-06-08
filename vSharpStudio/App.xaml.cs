using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using vSharpStudio.Views;

namespace vSharpStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            if (e.Args.Length == 1)
            {
                MainPage.MainPageVM.Model.SolutionPath = e.Args[0];
            }
        }
    }
}
