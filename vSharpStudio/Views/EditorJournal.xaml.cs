using System;
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
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Views
{
    public partial class EditorJournal : UserControl
    {
        public EditorJournal()
        {
            InitializeComponent();
        }
        private void DocumentCollectionChanged(object sender, RoutedEventArgs e)
        {
            var journal = this.DataContext as Journal;
            if (journal != null)
            {
                var dic = new Dictionary<string, DocInJournal>();
                foreach (var tt in journal.ListSelectedDocsWithProperties)
                {
                    dic[tt.Guid] = tt;
                }
                journal.ListSelectedDocsWithProperties.Clear();
                foreach (var tt in journal.ListIncludedDocuments)
                {
                    var guid = ((IGuid)tt).Guid;
                    if (dic.ContainsKey(guid))
                    {
                        journal.ListSelectedDocsWithProperties.Add(dic[guid]);
                    }
                    else
                    {
                        journal.ListSelectedDocsWithProperties.Add(new DocInJournal() { Guid = guid });
                    }
                }
            }
        }
        private void PropertyCollectionChanged(object sender, RoutedEventArgs e)
        {
            var journal = this.DataContext as Journal;
            if (journal != null)
            {
                if (journal.SelectedIncludedDocument != null)
                {
                    DocInJournal? docj = null;
                    foreach (var tt in journal.ListSelectedDocsWithProperties)
                    {
                        if (tt.Guid== journal.SelectedIncludedDocument.Guid)
                        {
                            docj = tt;
                        }
                    }
                    Debug.Assert(docj != null);
                    docj.ListPropertyGuids.Clear();
                    foreach(var tt in journal.ListIncludedProperties)
                    {
                        docj.ListPropertyGuids.Add(((IGuid)tt).Guid);
                    }
                }
            }
        }
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is Journal journal)
            {
                var hashDoc = new HashSet<string>();
                foreach (var tt in journal.ListSelectedDocsWithProperties)
                {
                    hashDoc.Add(tt.Guid);
                }
                var listNotIncludedDocuments = new ObservableCollection<ISortingValue>();
                foreach (var t in journal.ParentGroupListJournals.ParentModel.GroupDocuments.GroupListDocuments.ListDocuments)
                {
                    if (hashDoc.Contains(t.Guid))
                        continue;
                    listNotIncludedDocuments.Add(t);
                }
                journal.ListNotIncludedDocuments = listNotIncludedDocuments;

                var listIncludedDocuments = new SortedObservableCollection<ISortingValue>();
                foreach (var t in journal.ParentGroupListJournals.ParentModel.GroupDocuments.GroupListDocuments.ListDocuments)
                {
                    if (!hashDoc.Contains(t.Guid))
                        continue;
                    listIncludedDocuments.Add(t);
                }
                journal.ListIncludedDocuments = listIncludedDocuments;
            }
        }
    }
}
