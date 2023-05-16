using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;
using Microsoft.Win32;
using Serilog.Parsing;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.Migration;
using vSharpStudio.vm.ViewModels;
using vSharpStudio.wpf.Controls;
using static System.Resources.ResXFileRef;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for EditorRoles.xaml
    /// </summary>
    public partial class EditorRoles : UserControl
    {
        public EditorRoles()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null)
                return;
            var gridView = (GridView)_tree.View;
            var roles = (GroupListRoles)this.DataContext;
            foreach (var t in roles.ListRoles)
            {
                var gvc = new GridViewColumn();
                //gvc.DisplayMemberBinding = new Binding("Kuku");
                gvc.Header = t.Name;
                gvc.Width = 100;

                var editorRoleCell = new FrameworkElementFactory(typeof(EditorRoleCell));
                //txtBlock.SetBinding(TextBlock.TextProperty, new Binding("Street1"));
                var dataTemplate = new DataTemplate();
                dataTemplate.VisualTree = editorRoleCell;
                gvc.CellTemplate = dataTemplate;


                //var txtBlock = new FrameworkElementFactory(typeof(TextBlock));
                //txtBlock.SetBinding(TextBlock.TextProperty, new Binding("Street1"));
                //template.VisualTree.AppendChild(txtBlock);

                // databinding & converter
                //var frameworkElementFactory = new FrameworkElementFactory(typeof(TextBlock));
                //var dataTemplate = new DataTemplate();
                //dataTemplate.VisualTree = frameworkElementFactory;
                //gvc.CellTemplate = dataTemplate;
                //var binding = new Binding(assignment.Id.ToString() + key);
                //binding.Converter = converter;

                //gridView.Columns.Add(gvc);
            }
            _tree.Model = new EditorRolesVm(roles.ListRoles, roles.ParentGroupListCommon.ParentModel);
            //_tree.Model = new ModelNodeRoles(roles.ListRoles, roles.ParentGroupListCommon.ParentModel);
        }
    }
    //public class ModelNodeForeRoles
    //{
    //    public ModelNodeForeRoles(ITreeConfigNode node)
    //    {
    //        this.Node = node;
    //    }
    //    public ITreeConfigNode Node { get; private set; }
    //    public object? Kuku { get; private set; }
    //}
}
