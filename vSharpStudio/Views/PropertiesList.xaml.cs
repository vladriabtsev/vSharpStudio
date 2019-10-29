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
using vSharpStudio.vm.ViewModels;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for PropertiesList.xaml
    /// </summary>
    public partial class PropertiesList : UserControl
    {
        public PropertiesList()
        {
            InitializeComponent();
        }

        private void PropertyGrid_IsPropertyBrowsable(object sender, Xceed.Wpf.Toolkit.PropertyGrid.IsPropertyBrowsableArgs e)
        {
           switch(e.PropertyDescriptor.Name)
            {
                case "Description":
                case "Guid":
                case "NameUi":
                    var grd = (PropertyGrid)sender;
                    if (
                        grd.SelectedObject is GroupListBaseConfigLinks ||
                        grd.SelectedObject is GroupDocuments ||
                        grd.SelectedObject is GroupListCatalogs ||
                        grd.SelectedObject is GroupListConstants ||
                        grd.SelectedObject is GroupListDocuments ||
                        grd.SelectedObject is GroupListEnumerations ||
                        grd.SelectedObject is GroupListForms ||
                        grd.SelectedObject is GroupListJournals ||
                        grd.SelectedObject is GroupListPlugins ||
                        grd.SelectedObject is GroupListProperties ||
                        grd.SelectedObject is GroupListPropertiesTabs ||
                        grd.SelectedObject is GroupListReports ||
                        grd.SelectedObject is PropertiesTab
                        )
                    {
                        e.IsBrowsable = false;
                    }
                    break;
                case "Parent":
                case "Children":
                    e.IsBrowsable = false;
                    break;
                default:
                    break;
            }
        }
    }
}
