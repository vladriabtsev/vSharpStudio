using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for RolesSettings.xaml
    /// </summary>
    public partial class RolesSettings : Window
    {
        public RolesSettings()
        {
            InitializeComponent();
        }
        public RolesSettings(Model model, ITreeConfigNode node) : this()
        {
            var lst = new List<ITreeConfigNode>();
            ITreeConfigNode? parent = node;
            do
            {
                lst.Add(parent);
                if (parent is Model)
                    break;
                parent = parent.Parent;
            } while (parent != null);
            var lstNodes = new List<ITreeConfigNode>();
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                lstNodes.Add(lst[i]);
            }

            TypeBuilder tbRec = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tbRec.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            SettingsTypeBuilder.CreateProperty(tbRec, "Role", typeof(string), "Role", "");
            // Create class for record
            foreach (var t in lstNodes)
            {
                SettingsTypeBuilder.CreateProperty(tbRec, t.Name, typeof(object), t.Name, "");
            }
            Type? recType = tbRec.CreateType();
            // Fill list of records
            foreach (var tt in model.GroupCommon.GroupRoles.ListRoles)
            {
                object? objRec = Activator.CreateInstance(recType);
                recType.InvokeMember("Role", BindingFlags.SetProperty, null, objRec, new object[] { tt.Name });
                foreach (var t in lstNodes)
                {
                   // recType.InvokeMember( t. dt.Key, BindingFlags.SetProperty, null, objRec, new object[] { dt.Value });

                }
            }
            // Bind

        }
    }
}
