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
            int i = 0;
            foreach (var t in roles.ListRoles)
            {
                var gvc = new GridViewColumn();
                gvc.Header = t.Name;
                gvc.Width = 110;
                var editorRoleCell = new FrameworkElementFactory(typeof(EditorRoleCell));
                var bindProp = $"ListRoleColumns[{i}]";
                editorRoleCell.SetBinding(EditorRoleCell.DataContextProperty, new Binding(bindProp));
                var dataTemplate = new DataTemplate();
                dataTemplate.VisualTree = editorRoleCell;
                gvc.CellTemplate = dataTemplate;
                gridView.Columns.Add(gvc);
                i++;
            }
            _tree.Model = new EditorRoleTreeVm(roles.ListRoles, roles.ParentGroupListCommon.ParentModel);
        }
    }
}
