using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;

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
                var gvc = new GridViewColumn
                {
                    Header = t.Name,
                    Width = 110
                };
                var editorRoleCell = new FrameworkElementFactory(typeof(EditorRoleCell));
                var bindProp = $"ListRoleColumns[{i}]";
                editorRoleCell.SetBinding(EditorRoleCell.DataContextProperty, new Binding(bindProp));
                var dataTemplate = new DataTemplate
                {
                    VisualTree = editorRoleCell
                };
                gvc.CellTemplate = dataTemplate;
                gridView.Columns.Add(gvc);
                i++;
            }
            _tree.Model = new EditorRoleTreeVm(roles.ListRoles, roles.ParentGroupListCommon.ParentModel);
        }
    }
}
