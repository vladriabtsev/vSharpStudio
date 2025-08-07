using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ApplicationLogging;
using AsyncAwaitBestPractices;
using Renamer;
using ViewModelBase;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static MainPageVM MainPageVM { get; set; }
        private readonly MainPageVM _model;

        public MainPage()
        {
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            // https://www.abhishekshukla.com/wpf/advanced-wpf-part-4-threading-in-windows-presentation-foundation/
            UIDispatcher.Initialize();
            this.InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }

            AppLogger.LogLevel= Microsoft.Extensions.Logging.LogLevel.Trace;
            AppLogger.UseDebug = false;

            this._model = new MainPageVM(this);
            MainPage.MainPageVM = this._model;
            this.DataContext = this._model;
            //this._modalBaseSettingsEditWindow.DataContext = this._model;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }
            //InitConfig(this.Config);
            //if (this.Config.PrevStableConfig != null)
            //    InitConfig((Config)this.Config.PrevStableConfig);
            //if (this.Config.PrevCurrentConfig != null)
            //    InitConfig((Config)this.Config.PrevCurrentConfig);
            this._model.Compose();
            this._model.OnFormLoaded();
            FrameworkElement p = (FrameworkElement)this.Parent;
            while (!(p is MainWindow))
            {
                p = (FrameworkElement)p.Parent;
            }
            p.DataContext = this._model;
        }

        private void PropertiesList_LostFocus(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                return this._model.ValidateSelectedNodeAsync();
            }).SafeFireAndForget();
        }

        private void MenuItem_ContextMenu_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var mi = sender as MenuItem;
            Debug.Assert(mi != null);
            Debug.Assert(this._model.UserSettings != null);
            this._model.UserSettings.SelectedConfigHistory = mi.DataContext as UserSettingsOpenedConfig;
        }
    }
}
