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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = (MappingRow)((System.Windows.FrameworkElement)sender).DataContext;
            if (e.AddedItems.Count > 0)
            {
                var prop = (Property)e.AddedItems[0];
                if (row.Dimension != null)
                    row.Dimension.MappedRegisterDimensionToDocPropertyGuid = prop.Guid;
                else if (row.AttachedProperty != null)
                    row.AttachedProperty.MappedRegisterAttachedPropertyToDocPropertyGuid = prop.Guid;
                else if (!string.IsNullOrEmpty(row.AccumulatorGuid))
                {
                    if (row.Reg.TableTurnoverPropertyMoneyAccumulatorGuid == row.AccumulatorGuid)
                        row.Reg.MappedMoneyAccumulatorPropertyToDocPropertyGuid = prop.Guid;
                    else if (row.Reg.TableTurnoverPropertyQtyAccumulatorGuid == row.AccumulatorGuid)
                        row.Reg.MappedQtyAccumulatorPropertyToDocPropertyGuid = prop.Guid;
                    else
                        throw new NotImplementedException();
                }
                else
                    throw new NotImplementedException();
            }
        }
    }
}
