using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using vSharpStudio.ViewModels;
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
            this.InitializeComponent();
            this.dic["DynamicNodesSettings"] = null;
        }

        //private void PropertyGrid_IsPropertyBrowsable(object sender, Xceed.Wpf.Toolkit.PropertyGrid.IsPropertyBrowsableArgs e)
        //{
        //    var grd = (PropertyGrid)sender;
        //    switch (e.PropertyDescriptor.Name)
        //    {
        //        case "Description":
        //        case "Guid":
        //        case "NameUi":
        //            if (
        //                grd.SelectedObject is GroupListBaseConfigLinks ||
        //                grd.SelectedObject is GroupDocuments ||
        //                grd.SelectedObject is GroupListCatalogs ||
        //                grd.SelectedObject is GroupListConstants ||
        //                grd.SelectedObject is GroupListDocuments ||
        //                grd.SelectedObject is GroupListEnumerations ||
        //                grd.SelectedObject is GroupListForms ||
        //                grd.SelectedObject is GroupListJournals ||
        //                grd.SelectedObject is GroupListPlugins ||
        //                grd.SelectedObject is GroupListProperties ||
        //                grd.SelectedObject is GroupListDetails ||
        //                grd.SelectedObject is GroupListReports)
        //            {
        //                e.IsBrowsable = false;
        //            }
        //            break;
        //        case "Parent":
        //        case "Children":
        //            e.IsBrowsable = false;
        //            break;
        //        default:
        //            break;
        //    }
        //    if (!(grd.SelectedObject is Model))
        //    {
        //        if (!(e.IsBrowsable ?? false) && grd.SelectedObject is IGetNodeSetting && grd.SelectedPropertyItem != null && grd.SelectedPropertyItem != null)
        //        {
        //            var ptype = ((Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem)grd.SelectedPropertyItem).PropertyType;
        //            var ns = grd.SelectedObject as IGetNodeSetting;
        //            if (ns.DicVmExclProps.ContainsKey(ptype.Name))
        //            {
        //                if (ns.DicVmExclProps[ptype.Name].ContainsKey(e.PropertyDescriptor.Name))
        //                {
        //                    e.IsBrowsable = false;
        //                }
        //            }
        //        }
        //    }
        //}

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((MainPageVM)this.DataContext).propertyGrid = this.propertyGrid;
        }

        private void propertyGrid_SelectedObjectChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var grid = (PropertyGrid)sender;
            foreach (PropertyItem prop in grid.Properties)
            {
                if (prop.IsExpandable && !prop.IsExpanded) //Only expand things marked as Expandable, otherwise it will expand everything possible, such as strings, which you probably don't want.
                {
                    if (this.dic.ContainsKey(prop.PropertyName))
                    {
                        prop.IsExpanded = true; //This will expand the property.
                                                //prop.IsExpandable = false; //This will remove the ability to toggle the expanded state.
                    }
                }
            }
        }
        private readonly Dictionary<string, string?> dic = new Dictionary<string, string?>();
        private void propertyGrid_SelectedPropertyItemChanged(object sender, RoutedPropertyChangedEventArgs<PropertyItemBase> e)
        {
            //var prop = e.NewValue as PropertyItem;
            //if (prop == null)
            //    return;
            ////Trace.WriteLine($"*** Selected: {prop.PropertyName}");
            //if (prop.IsExpandable)
            //{
            //    if (prop.IsExpanded)
            //    {
            //        dic[prop.PropertyName] = null;
            //    }
            //    else
            //    {
            //        if (dic.ContainsKey(prop.PropertyName))
            //            dic.Remove(prop.PropertyName);
            //    }
            //}
            //prop = e.OldValue as PropertyItem;
            //if (prop == null)
            //    return;
            //if (prop.IsExpandable)
            //{
            //    if (prop.IsExpanded)
            //    {
            //        dic[prop.PropertyName] = null;
            //    }
            //    else
            //    {
            //        if (dic.ContainsKey(prop.PropertyName))
            //            dic.Remove(prop.PropertyName);
            //    }
            //}
        }

        //private void propertyGrid_Initialized(object sender, EventArgs e)
        //{
        //    var grid = sender as PropertyGrid;
        //    grid.ExpandAllProperties();
        //}
    }
}
