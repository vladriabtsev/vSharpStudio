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
using System.Xaml;
using vSharpStudio.vm.ViewModels;
using vSharpStudio.wpf;
using vSharpStudio.wpf.Converters;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for TreeModels.xaml
    /// </summary>
    public partial class TreeModels : UserControl
    {
        ConfigModel cm = null;
        Config cfg = null;
        Thickness mtb = new Thickness(0, 3, 0, 3);
        public TreeModels()
        {
            InitializeComponent();
            this.DataContextChanged += TreeModels_DataContextChanged;
        }
        private void TreeModels_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
            }
            else
            {
                List<Model> lst = new List<Model>();
                if (e.NewValue is Model)
                {
                    var sm = e.NewValue as Model;
                    cfg = (Config)sm.Parent.Parent;
                    lst.Add(sm);
                }
                else if (e.NewValue is GroupListModels)
                {
                    var gsm = e.NewValue as GroupListModels;
                    cfg = (Config)gsm.Parent;
                    foreach (var t in cfg.GroupModels.ListModels)
                    {
                        lst.Add(t);
                    }
                }
                cm = cfg.Model;
                this._tree.Model = cm;

                GridView gv = (GridView)_tree.View;
                for (int i = gv.Columns.Count - 1; i > 0; i--)
                {
                    gv.Columns.RemoveAt(i);
                }
                GridViewColumn gvc = null;
                foreach (var t in lst)
                {
                    gvc = new GridViewColumn();
                    gvc.Width = 27;
                    // Header
                    TextBlock tb = new TextBlock();
                    tb.Text = t.Name;
                    tb.Margin = mtb;
                    TextOptions.SetTextFormattingMode(tb, TextFormattingMode.Display);
                    tb.VerticalAlignment = VerticalAlignment.Bottom;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.LayoutTransform = new RotateTransform(-90);
                    gvc.Header = tb;
                    // Cell
                    // https://www.codeproject.com/Tips/808808/Create-Data-and-Control-Templates-using-Delegates                        
                    DataTemplate template = DataTemplateExt.CreateDataTemplate
                        (() =>
                            {
                                CheckBox cb = new CheckBox();
                                cb.IsThreeState = true;
                                cb.VerticalAlignment = VerticalAlignment.Center;
                                cb.HorizontalAlignment = HorizontalAlignment.Center;
                                //cb.Tag = tt;
                                cb.SetBinding(CheckBox.IsCheckedProperty, "BindingPathHere");
                                var bnd = new Binding("IsIncludableInModels");
                                bnd.Converter = new ConverterBoolToVisible();
                                cb.SetBinding(CheckBox.VisibilityProperty, bnd);
                                return cb;
                            }
                      );
                    gvc.CellTemplate = template;
                    gv.Columns.Add(gvc);
                }
            }
        }
    }
}
