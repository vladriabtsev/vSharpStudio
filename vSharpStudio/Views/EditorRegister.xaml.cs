using System.Windows.Controls;

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
