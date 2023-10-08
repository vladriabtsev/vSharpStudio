using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vSharpStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        double _defaultWidth = 1000;
        double _defaultHeight = 700;        

        #endregion

        #region Events

        public MainWindow()
        {
            InitializeComponent();
            ResizeAndPosition();

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }

            // vSharpStudio.std.ApplicationLogging.LoggerFactory.AddProvider(new Serilog.Sinks.File.PeriodicFlushToDiskSink()
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Helpers

        void ResizeAndPosition()
        {
            WpfScreen s = WpfScreen.GetScreenFrom(this);

            double margin = 20;
            double maxHeight = s.WorkingArea.Height - 2 * margin;
            double maxWidth = s.WorkingArea.Width - 2 * margin;

            if (maxHeight < _defaultHeight) Height = maxHeight;
            else Height = _defaultHeight;

            if (maxWidth < _defaultWidth) Width = maxWidth;
            else Width = _defaultWidth;

            double magicXNumber = 120.0 / 1920.0;
            Left = magicXNumber * s.WorkingArea.Width;

            Top = s.WorkingArea.Height / 2 - Height / 2;
        }

        #endregion        
    }
}
