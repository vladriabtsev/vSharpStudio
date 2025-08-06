using System.Windows.Controls;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for GenSettings.xaml
    /// </summary>
    public partial class GenSettings : UserControl
    {
        public GenSettings()
        {
            InitializeComponent();
            this.ViewVm = new GenSettingsVm();
        }
        public GenSettingsVm? ViewVm { get { return _ViewVm; } set { _ViewVm = value; this.DataContext = value; } }
        private GenSettingsVm? _ViewVm;
    }
}
