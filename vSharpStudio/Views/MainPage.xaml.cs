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
using ViewModelBase;
using vSharpStudio.ViewModels;
using vSharpStudio.wpf;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public static MainPageVM MainPageVM;
        private MainPageVM _model = null;
        public MainPage()
        {
            // https://www.abhishekshukla.com/wpf/advanced-wpf-part-4-threading-in-windows-presentation-foundation/
            ViewModelBindable.AppDispatcher = UIDispatcher.Current;
            InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
#if DEBUG
            _model = new MainPageVM(true);
            _model.Compose();
            this.DataContext = _model;
#else
            _model = new MainPageVM();
            Task task = Task.Run(() =>
            {
                _model.Compose();
                this.DataContext = _model;
            });
#endif
            MainPage.MainPageVM = _model;
        }
    }
}
