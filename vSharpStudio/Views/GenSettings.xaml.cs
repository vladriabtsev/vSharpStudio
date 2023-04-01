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
using CommunityToolkit.Mvvm.ComponentModel;
using vSharpStudio.common;
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
