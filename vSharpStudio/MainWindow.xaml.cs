using System;
using System.Collections.Generic;
using System.Drawing;
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

        #endregion

        #region Helpers

        void ResizeAndPosition()
        {
            WpfScreen s = WpfScreen.GetScreenFrom(this);

            double margin = 20;
            double maxWidth = s.WorkingAreaPx.Width - 2 * margin;
            double maxHeight = s.WorkingAreaPx.Height - 2 * margin;
            
            using Graphics g = WpfScreen.CreateConverter();
            maxWidth = WpfScreen.XPixelsToUnits(g, maxWidth);
            maxHeight = WpfScreen.YPixelsToUnits(g, maxHeight);

            if (maxHeight < _defaultHeight)
            {
                Height = maxHeight;
                WindowState = WindowState.Maximized;
            }
            else Height = _defaultHeight;

            if (maxWidth < _defaultWidth) Width = maxWidth;
            else Width = _defaultWidth;

            double magicXNumber = 120.0 / 1920.0;
            double left = magicXNumber * Width;

            Left = left;
            Top = WpfScreen.XPixelsToUnits(g, s.WorkingAreaPx.Height) / 2 - Height / 2;
        }

        #endregion        
    }
}
