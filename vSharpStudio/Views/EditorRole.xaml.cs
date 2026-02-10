using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Views
{
    public partial class EditorRole : UserControl
    {
        public EditorRole()
        {
            InitializeComponent();
        }
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null)
                return;
            var gridView = (GridView)_tree.View;
            var role = (Role)this.DataContext;
            var gvc = new GridViewColumn
            {
                Header = role.Name,
                //Width = 110
            };
            var editorSettings = new FrameworkElementFactory(typeof(EditorRoleSettings));
            var bindProp = $"ListRoleColumns[0]";
            editorSettings.SetBinding(EditorRoleCell.DataContextProperty, new Binding(bindProp));
            var dataTemplate = new DataTemplate
            {
                VisualTree = editorSettings
            };
            gvc.CellTemplate = dataTemplate;
            gridView.Columns.Add(gvc);
            _tree.Model = new EditorRoleTreeVm(role, role.ParentGroupListRoles.ParentGroupListCommon.ParentModel);
        }
    }
}
