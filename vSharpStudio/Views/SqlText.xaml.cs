using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for SqlText.xaml
    /// </summary>
    public partial class SqlText : UserControl
    {
        public SqlText()
        {
            InitializeComponent();
        }
        public sealed class ViewModel
        {
            public ObservableCollection<TabItem> Tabs { get; set; }
            public ViewModel(Dictionary<string, string> dicResults)
            {
                Tabs = new ObservableCollection<TabItem>();
                foreach (var t in dicResults)
                {
                    Tabs.Add(new TabItem { Header = t.Key, Content = t.Value });
                }
            }
        }
        public sealed class TabItem
        {
            public string Header { get; set; } = "";
            public string Content { get; set; } = "";
        }
    }
}
