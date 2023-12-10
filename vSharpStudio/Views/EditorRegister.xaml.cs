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

namespace vSharpStudio.Views
{
    public partial class EditorRegister : UserControl
    {
        public EditorRegister()
        {
            InitializeComponent();
        }

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var row = (MappingRow)((System.Windows.FrameworkElement)sender).DataContext;
        //    foreach (var t in e.RemovedItems)
        //    {
        //        //var prop = (Property)t;
        //        row.Reg.MappingRegPropertyRemove(row.Doc.Guid, row.RegPropertyGuid);
        //    }
        //    foreach (var t in e.AddedItems)
        //    {
        //        var prop = (Property)t;
        //        row.Reg.MappingRegPropertyAdd(row.Doc.Guid, row.RegPropertyGuid, prop.Guid);
        //    }
        //}
    }
}
