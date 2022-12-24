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
using vSharpStudio.vm.ViewModels;
using vSharpStudio.wpf;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static MainPageVM MainPageVM { get; set; }
        private MainPageVM _model;

        public MainPage()
        {
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            // https://www.abhishekshukla.com/wpf/advanced-wpf-part-4-threading-in-windows-presentation-foundation/
            VmBindable.AppDispatcher = UIDispatcher.Current;
            this.InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }
            this._model = new MainPageVM(true);
            MainPage.MainPageVM = this._model;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }
#if DEBUG
            //InitConfig(this.Config);
            //if (this.Config.PrevStableConfig != null)
            //    InitConfig((Config)this.Config.PrevStableConfig);
            //if (this.Config.PrevCurrentConfig != null)
            //    InitConfig((Config)this.Config.PrevCurrentConfig);
            this._model.Compose();
            this._model.OnFormLoaded();
            this.DataContext = this._model;
            FrameworkElement p = (FrameworkElement)this.Parent;
            while (!(p is MainWindow))
            {
                p = (FrameworkElement)p.Parent;
            }
            p.DataContext = this._model;
#else
            _model = new MainPageVM();
            Task task = Task.Run(() =>
            {
                this._model.OnFormLoaded();
                this._model.Compose();
                this.DataContext = this._model;
            });
#endif

        }
    }
}
