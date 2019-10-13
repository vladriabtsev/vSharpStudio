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
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for TreeSubModels.xaml
    /// </summary>
    public partial class TreeSubModels : UserControl
    {
        public TreeSubModels()
        {
            InitializeComponent();
        }
        bool isShowLines = false;
        int splitw = 3;
        Thickness mlr = new Thickness(3, 0, 3, 0);
        Thickness mtb = new Thickness(0, 3, 0, 3);
        GridLength headerHeight = new GridLength(100);
        GridLength rowHeight = new GridLength(20);
        GridLength chkbWidth = new GridLength(20);
        int btnbWidth = 12;
        Thickness mbtn = new Thickness(12, 0, 0, 0);
        ConfigModel cm = null;
        Config cfg = null;
        bool isLst = false;

        private Grid CreateSplittedGrid()
        {
            var grd = new Grid();
            if (isShowLines)
                grd.ShowGridLines = true;
            grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            //grdSplit.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
            grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            grd.RowDefinitions.Add(new RowDefinition());
            grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
            //grdSplit.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
            grd.ColumnDefinitions.Add(new ColumnDefinition());

            var splith = new GridSplitter();
            splith.ResizeDirection = GridResizeDirection.Rows;
            splith.Height = splitw;
            splith.HorizontalAlignment = HorizontalAlignment.Stretch;
            splith.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(splith, 1);
            Grid.SetColumnSpan(splith, 3);
            grd.Children.Add(splith);

            var splitv = new GridSplitter();
            splitv.ResizeDirection = GridResizeDirection.Columns;
            splitv.Width = splitw;
            splitv.VerticalAlignment = VerticalAlignment.Stretch;
            splitv.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(splitv, 1);
            Grid.SetRowSpan(splitv, 3);
            grd.Children.Add(splitv);
            return grd;
        }
        private void AddToGrid(Grid grid, FrameworkElement el, int row, int col)
        {
            Grid.SetRow(el, row);
            Grid.SetColumn(el, col);
            grid.Children.Add(el);
        }
        private Grid CreateTopHeader()
        {
            var grd = new Grid();
            if (isShowLines)
                grd.ShowGridLines = true;
            if (isLst)
            {
                foreach (var t in cfg.GroupSubModels.ListSubModels)
                {
                    grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = chkbWidth });
                }
                //this.grd.RowDefinitions.Add(new RowDefinition() { Height = headerHeight });
                grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                int ic = 0;
                foreach (var t in cfg.GroupSubModels.ListSubModels)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = t.Name;
                    tb.Margin = mtb;
                    TextOptions.SetTextFormattingMode(tb, TextFormattingMode.Display);
                    Grid.SetColumn(tb, ic);
                    tb.VerticalAlignment = VerticalAlignment.Bottom;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.LayoutTransform = new RotateTransform(-90);
                    grd.Children.Add(tb);
                    ic++;
                }
            }
            else
            {
                grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = chkbWidth });
            }
            return grd;
        }
        private Grid CreateLeftHeader()
        {
            var grd = new Grid();
            if (isShowLines)
                grd.ShowGridLines = true;
            int ir = 0;
            foreach (var t in cfg.Model.Children)
            {
                var np = new StackPanel() { Orientation = Orientation.Horizontal };
                grd.RowDefinitions.Add(new RowDefinition() { Height = rowHeight });
                var btn = new Button();
                btn.Content = "+";
                btn.Width = btnbWidth;
                btn.Height = btnbWidth;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.VerticalContentAlignment = VerticalAlignment.Center;
                btn.HorizontalContentAlignment = HorizontalAlignment.Center;
                switch (t.Name)
                {
                    case "Common":
                        np.Children.Add(btn);
                        break;
                    case Defaults.ConstantsGroupName:
                        if (cfg.Model.GroupCatalogs.ListCatalogs.Count > 0)
                            np.Children.Add(btn);
                        else
                            np.Margin = mbtn;
                        break;
                    case Defaults.EnumerationsGroupName:
                        if (cfg.Model.GroupEnumerations.ListEnumerations.Count > 0)
                            np.Children.Add(btn);
                        else
                            np.Margin = mbtn;
                        break;
                    case Defaults.CatalogsGroupName:
                        if (cfg.Model.GroupCatalogs.ListCatalogs.Count > 0)
                            np.Children.Add(btn);
                        else
                            np.Margin = mbtn;
                        break;
                    case Defaults.DocumentsGroupName:
                        if (cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments.Count > 0)
                            np.Children.Add(btn);
                        else
                            np.Margin = mbtn;
                        break;
                    case Defaults.JournalsGroupName:
                        if (cfg.Model.GroupJournals.ListJournals.Count > 0)
                            np.Children.Add(btn);
                        else
                            np.Margin = mbtn;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                TextBlock tb = new TextBlock();
                tb.Text = t.Name;
                //tb.FontWeight = FontWeights.Bold;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.Margin = mlr;
                Grid.SetRow(np, ir);
                np.Children.Add(tb);
                grd.Children.Add(np);
                ir++;
            }
            return grd;
        }
        private Grid CreateGridCbx()
        {
            var grd = new Grid();
            if (isShowLines)
                grd.ShowGridLines = true;
            int ir = 0, ic = 0;
            foreach (var tt in cfg.GroupSubModels.ListSubModels)
            {
                grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = chkbWidth });
            }
            foreach (var t in cfg.Model.Children)
            {
                grd.RowDefinitions.Add(new RowDefinition() { Height = rowHeight });
                ic = 0;
                foreach (var tt in cfg.GroupSubModels.ListSubModels)
                {
                    CheckBox cb = new CheckBox();
                    cb.IsThreeState = true;
                    cb.VerticalAlignment = VerticalAlignment.Center;
                    cb.HorizontalAlignment = HorizontalAlignment.Center;
                    cb.Tag = tt;
                    cb.Checked += Cb_Checked;
                    this.AddToGrid(grd, cb, ir, ic);
                    ic++;
                }
                ir++;
            }
            return grd;
        }

        private void Cb_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                var cb = sender as CheckBox;
                SubModel sm = (SubModel)cb.Tag;
                if (cb.IsChecked == null)
                {
                    //sm.DicGuids
                }
                else if (cb.IsChecked ?? false)
                {
                }
                else
                {

                }
            }
        }

        private void grd_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                if (this.cont != null)
                {
                    var svl = (ScrollViewer)this.cont.Content;
                    var grdl = (Grid)svl.Content;
                    grdl.Children.Clear();
                    grdl.ColumnDefinitions.Clear();
                    grdl.RowDefinitions.Clear();
                    this.cont = null;
                }
            }
            else
            {
                if (e.NewValue is SubModel)
                {
                    var sm = e.NewValue as SubModel;
                    cfg = (Config)sm.Parent.Parent;
                }
                else if (e.NewValue is GroupListSubModels)
                {
                    var gsm = e.NewValue as GroupListSubModels;
                    cfg = (Config)gsm.Parent;
                    isLst = true;
                }
                cm = cfg.Model;

                //int ic, ir;
                //var grd = new Grid();
                //grd.ShowGridLines = true;
                //grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                //if (isLst)
                //{
                //    foreach (var t in cfg.GroupSubModels.ListSubModels)
                //    {
                //        grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = chkbWidth });
                //    }
                //    //this.grd.RowDefinitions.Add(new RowDefinition() { Height = headerHeight });
                //    grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                //    ir = 0;
                //    ic = 1;
                //    foreach (var t in cfg.GroupSubModels.ListSubModels)
                //    {
                //        TextBlock tb = new TextBlock();
                //        tb.Text = t.Name;
                //        tb.Margin = mtb;
                //        TextOptions.SetTextFormattingMode(tb, TextFormattingMode.Display);
                //        Grid.SetColumn(tb, ic);
                //        tb.VerticalAlignment = VerticalAlignment.Bottom;
                //        tb.HorizontalAlignment = HorizontalAlignment.Center;
                //        tb.LayoutTransform = new RotateTransform(-90);
                //        grd.Children.Add(tb);
                //        ic++;
                //    }
                //}
                //else
                //{
                //    grd.ColumnDefinitions.Add(new ColumnDefinition() { Width = chkbWidth });
                //}
                //ir = 1;
                //foreach (var t in cfg.Model.Children)
                //{
                //    ic = 0;
                //    var np = new StackPanel() { Orientation = Orientation.Horizontal };
                //    grd.RowDefinitions.Add(new RowDefinition() { Height = rowHeight });
                //    var btn = new Button();
                //    btn.Content = "+";
                //    btn.Width = btnbWidth;
                //    btn.Height = btnbWidth;
                //    btn.VerticalAlignment = VerticalAlignment.Center;
                //    btn.VerticalContentAlignment = VerticalAlignment.Center;
                //    btn.HorizontalContentAlignment = HorizontalAlignment.Center;
                //    switch (t.Name)
                //    {
                //        case "Common":
                //            np.Children.Add(btn);
                //            break;
                //        case Defaults.ConstantsGroupName:
                //            if (cfg.Model.GroupCatalogs.ListCatalogs.Count > 0)
                //                np.Children.Add(btn);
                //            else
                //                np.Margin = mbtn;
                //            break;
                //        case Defaults.EnumerationsGroupName:
                //            if (cfg.Model.GroupEnumerations.ListEnumerations.Count > 0)
                //                np.Children.Add(btn);
                //            else
                //                np.Margin = mbtn;
                //            break;
                //        case Defaults.CatalogsGroupName:
                //            if (cfg.Model.GroupCatalogs.ListCatalogs.Count > 0)
                //                np.Children.Add(btn);
                //            else
                //                np.Margin = mbtn;
                //            break;
                //        case Defaults.DocumentsGroupName:
                //            if (cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments.Count > 0)
                //                np.Children.Add(btn);
                //            else
                //                np.Margin = mbtn;
                //            break;
                //        case Defaults.JournalsGroupName:
                //            if (cfg.Model.GroupJournals.ListJournals.Count > 0)
                //                np.Children.Add(btn);
                //            else
                //                np.Margin = mbtn;
                //            break;
                //        default:
                //            throw new NotImplementedException();
                //    }
                //    TextBlock tb = new TextBlock();
                //    tb.Text = t.Name;
                //    //tb.FontWeight = FontWeights.Bold;
                //    tb.VerticalAlignment = VerticalAlignment.Center;
                //    tb.HorizontalAlignment = HorizontalAlignment.Left;
                //    tb.Margin = mlr;
                //    Grid.SetRow(np, ir);
                //    Grid.SetColumn(np, ic);
                //    np.Children.Add(tb);
                //    grd.Children.Add(np);
                //    ic++;
                //    foreach (var tt in cfg.GroupSubModels.ListSubModels)
                //    {
                //        CheckBox cb = new CheckBox();
                //        Grid.SetRow(cb, ir);
                //        Grid.SetColumn(cb, ic);
                //        cb.VerticalAlignment = VerticalAlignment.Center;
                //        cb.HorizontalAlignment = HorizontalAlignment.Center;
                //        grd.Children.Add(cb);
                //        ic++;
                //    }
                //    ir++;
                //}
                //grd.ColumnDefinitions.Add(new ColumnDefinition()); // extra * column
                //grd.RowDefinitions.Add(new RowDefinition()); // extra * row

                var grdSplit = CreateSplittedGrid();

                var topHeader = CreateTopHeader();
                topHeader.VerticalAlignment = VerticalAlignment.Bottom;
                this.AddToGrid(grdSplit, topHeader, 0, 2);

                var leftHeader = CreateLeftHeader();
                this.AddToGrid(grdSplit, leftHeader, 2, 0);

                var gridCbx = CreateGridCbx();
                this.AddToGrid(grdSplit, gridCbx, 2, 2);



                var sv = new ScrollViewer();
                sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv.Content = grdSplit;
                this.cont.Content = sv;
            }
        }
    }
}
