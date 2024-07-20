using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vSharpStudio.ViewModels;
using Application = System.Windows.Application;
using WpfScreenHelper;
using Screen = WpfScreenHelper.Screen;

namespace vSharpStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainPageVM? mainPageVM;
        public MainWindow()
        {
            this.InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }
#if DEBUG
            this.LocationChanged += MainWindow_LocationChanged;
            this.SizeChanged += MainWindow_LocationChanged;
#endif

            // vSharpStudio.std.ApplicationLogging.LoggerFactory.AddProvider(new Serilog.Sinks.File.PeriodicFlushToDiskSink()
            int minWidth = 1200;
            int minHeight = 800;

            var userSettings = MainPageVM.UserSettingsRead();
            var lstScreens = Screen.AllScreens; // can find additional restrictions for Left, Top, Width, Height
            double w = 0, h = 0;
            foreach (var t in lstScreens)
            {
                w = Math.Max(w, t.WpfBounds.Right);
                h = Math.Max(h, t.WpfBounds.Bottom);
            }
            if (userSettings.LastVirtualScreenWidth == w && userSettings.LastVirtualScreenHeight == h) // can use last app main window position
            {
                var r = userSettings.LastAppMainWindowRectOnVirtualScreen;
                Application.Current.MainWindow.Left = Math.Min(w - 200, Math.Max(0, r.X));
                Application.Current.MainWindow.Top = Math.Min(h - 150, Math.Max(0, r.Y));
                Application.Current.MainWindow.Width = r.Width;
                Application.Current.MainWindow.Height = r.Height;
            }
            else // virtual scree was changed
            {
                this.Width = minWidth;
                this.Height = minHeight;
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var lstScreens = Screen.AllScreens; // can find additional restrictions for Left, Top, Width, Height
            double w = 0, h = 0;
            foreach (var t in lstScreens)
            {
                w = Math.Max(w, t.WpfBounds.Right); //  / t.ScaleFactor
                h = Math.Max(h, t.WpfBounds.Bottom);
            }
            Debug.Assert(MainPageVM.Instance.UserSettings != null);
            MainPageVM.Instance.UserSettings.LastVirtualScreenWidth = w;
            MainPageVM.Instance.UserSettings.LastVirtualScreenHeight = h;
            MainPageVM.Instance.UserSettings.LastAppMainWindowRectOnVirtualScreen.X = Application.Current.MainWindow.Left;
            MainPageVM.Instance.UserSettings.LastAppMainWindowRectOnVirtualScreen.Y = Application.Current.MainWindow.Top;
            MainPageVM.Instance.UserSettings.LastAppMainWindowRectOnVirtualScreen.Width = Application.Current.MainWindow.Width;
            MainPageVM.Instance.UserSettings.LastAppMainWindowRectOnVirtualScreen.Height = Application.Current.MainWindow.Height;
            MainPageVM.Instance.UserSettingsSave();
        }
#if DEBUG
        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            var x = (int)Application.Current.MainWindow.Left;
            var y = (int)Application.Current.MainWindow.Top;
            var width = (int)Application.Current.MainWindow.Width;
            var height = (int)Application.Current.MainWindow.Height;
            MainPageVM.Instance.WindowPosition = $"X:{x} Y:{y} W:{width} h:{height}   ";
        }
#endif
    }
}
