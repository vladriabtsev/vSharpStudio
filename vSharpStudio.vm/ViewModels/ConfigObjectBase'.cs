using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using FluentValidation;
using ViewModelBase;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigObjectBase<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>, IComparable<T>, ISortingValue, ITreeConfigNode
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectBase<T, TValidator>, IComparable<T>, ISortingValue //, ITreeConfigNode
    {
        public ConfigObjectBase(TValidator validator)
            : base(validator)
        {
        }
        protected virtual void OnInitFromDto()
        {
        }

        private static int _maxlen = 0;
        public override int CompareToById(T other)
        {
            ITreeConfigNode p = (ITreeConfigNode)this;
            return p.Guid.CompareTo(other.Guid);
        }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
        protected override void OnCountErrorsChanged()
        {
            NotifyPropertyChanged(p => p.IconStatus);
        }
        protected override void OnCountWarningsChanged()
        {
            NotifyPropertyChanged(p => p.IconStatus);
        }
        protected override void OnCountInfosChanged()
        {
            NotifyPropertyChanged(p => p.IconStatus);
        }
        [BrowsableAttribute(false)]
        public string IconStatus
        {
            get
            {
                string iconName = null;
                if (this.CountErrors > 0)
                    iconName = "iconStatusCriticalError";
                else
                {
                    if (this.CountWarnings > 0)
                        iconName = "iconStatusWarning";
                    else
                    {
                        if (this.CountInfos > 0)
                            iconName = "iconStatusInformation";
                        else
                            iconName = null;
                    }
                }
                return iconName;
            }
        }
        [BrowsableAttribute(false)]
        public string IconFolder
        {
            get
            {
                string iconName = null;
                if (this.IsExpanded)
                    iconName = "iconFolderOpen";
                else
                    iconName = "iconFolder";
                return iconName;
            }
        }

        public int CompareTo(T other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region Sort

        [BrowsableAttribute(false)]
        public ulong SortingWeight { get; set; }
        [BrowsableAttribute(false)]
        public ulong SortingValue
        {
            set
            {
                if (_SortingValue != value)
                {
                    OnSortingValueChanging();
                    _SortingValue = value;
                    OnSortingValueChanged();
                    NotifyPropertyChanged();
                    //ValidateProperty();
                    ITreeConfigNode p = (ITreeConfigNode)this;
                    if (p.Parent != null)
                        p.Parent.Sort(this.GetType());
                }
            }
            get { return _SortingValue; }
        }
        private ulong _SortingValue;
        public virtual void Sort(Type type)
        {
            throw new NotImplementedException();
        }
        partial void OnSortingValueChanging();
        partial void OnSortingValueChanged();

        #endregion Sort

        [BrowsableAttribute(false)]
        public string Guid
        {
            get
            {
                if (_Guid == null)
                {
                    SetNewGuid();
                    NotifyPropertyChanged(); // to recognize object was changed
                }
                return _Guid;
            }
            protected set
            {
                _Guid = value;
                NotifyPropertyChanged();
            }
        }
        private string _Guid = null;
        protected void SetNewGuid()
        {
            _Guid = System.Guid.NewGuid().ToString();
        }
        [PropertyOrder(0)]
        public string Name
        {
            set
            {
                if (_Name != value)
                {
                    _Name = value.Trim();
                    NotifyPropertyChanged();
                    if (ValidateProperty())
                    {
                        this.SortingValue = EncodeNameToUlong(this._Name) + this.SortingWeight;
                        ITreeConfigNode p = (ITreeConfigNode)this;
                        if (p.Parent != null)
                            p.Parent.Sort(this.GetType());
                        if (this.Parent != null)
                        {
                            ITreeConfigNode config = this.Parent;
                            while (config.Parent != null)
                                config = config.Parent;
                            (config as Config).SelectedNode = this;
                        }
                    }
                }
            }
            get { return _Name; }
        }
        private string _Name = "";
        [PropertyOrder(1)]
        [DisplayName("UI Name")]
        public string NameUi
        {
            set
            {
                if (_NameUi != value)
                {
                    _NameUi = value.Trim();
                    NotifyPropertyChanged();
                    ValidateProperty();
                }
            }
            get
            {
                if (string.IsNullOrWhiteSpace(_NameUi) && !string.IsNullOrEmpty(_Name))
                    return _Name;
                return _NameUi;
            }
        }
        private string _NameUi = "";
        protected ulong EncodeNameToUlong(string name)
        {
            const int step = 1 + '9' - '0' + 1 + 'Z' - 'A' + 1; // first is '_'
            if (_maxlen == 0)
            {
                _maxlen = (int)Math.Log(ViewModelBindable.SortingWeightBase, step);
                ulong val = 1;
                for (int i = 0; i < _maxlen; i++)
                {
                    val *= step;
                }
            }
            int len = Math.Min(_maxlen, name.Length);
            ulong res = 0;
            for (int i = 0; i < len; i++)
            {
                var c = char.ToUpper(name[i]);
                int ci = 0;
                if (char.IsDigit(c))
                    ci = c - '0' + 1;
                else if (c == '_')
                    ci = 0;
                else if (c >= 'A' && c <= 'Z')
                    ci = c - 'A' + 11;
                else
                    throw new ArgumentException("Unexpected char value: '" + c + "'");
                ulong pow = 1;
                for (int j = 0; j < _maxlen - i - 1; j++)
                {
                    pow *= step;
                }
                res += (ulong)ci * pow;
            }
            return res;
        }
        protected void GetUniqueName(string defName, ITreeConfigNode configObject, IEnumerable<ITreeConfigNode> lst)
        {
            if (!string.IsNullOrWhiteSpace(configObject.Name))
                return;
            int i = 0;
            foreach (var tt in lst)
            {
                if (tt == configObject)
                    continue;
                if (tt.Name.StartsWith(defName))
                {
                    string s = tt.Name.Remove(0, defName.Length);
                    int ii;
                    if (int.TryParse(s, out ii))
                    {
                        if (ii > i) i = ii;
                    }
                }
            }
            i++;
            configObject.Name = defName + i;
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent
        {
            get { return _Parent; }
            set
            {
                _Parent = value;
                OnParentChanged();
            }
        }
        private ITreeConfigNode _Parent;
        protected virtual void OnParentChanged() { }
        [BrowsableAttribute(false)]
        public bool IsSelected
        {
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _IsSelected; }
        }
        private bool _IsSelected;
        [BrowsableAttribute(false)]
        public bool IsExpanded
        {
            set
            {
                if (_IsExpanded != value)
                {
                    _IsExpanded = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(p => p.IconFolder);
                }
            }
            get { return _IsExpanded; }
        }
        private bool _IsExpanded;

        #region Commands

        public bool NodeCanAddNew()
        {
            if (this is ICanAddNode)
                return true;
            return false;
        }
        public ITreeConfigNode NodeAddNew()
        {
            string tname = this.GetType().Name;
            switch (tname)
            {
                case "Constant":
                    var res = new Constant();
                    //res.Parent = this.Parent;
                    (this.Parent as GroupListConstants).Add(res);
                    GetUniqueName(Constant.DefaultName, res, (this.Parent as GroupListConstants).Children);
                    (this.Parent.Parent as Config).SelectedNode = res;
                    return res;
                case "Enumeration":
                    var enumeration = new Enumeration();
                    //enumeration.Parent = this.Parent;
                    (this.Parent as GroupListEnumerations).Add(enumeration);
                    GetUniqueName(Enumeration.DefaultName, enumeration, (this.Parent as GroupListEnumerations).Children);
                    (this.Parent.Parent as Config).SelectedNode = enumeration;
                    return enumeration;
                case "Catalog":
                    var catalog = new Catalog();
                    //catalog.Parent = this.Parent;
                    (this.Parent as GroupListCatalogs).Add(catalog);
                    GetUniqueName(Catalog.DefaultName, catalog, (this.Parent as GroupListCatalogs).Children);
                    (this.Parent.Parent as Config).SelectedNode = catalog;
                    return catalog;
                case "Document":
                    var doc = new Document();
                    //doc.Parent = this.Parent;
                    (this.Parent as GroupListDocuments).Add(doc);
                    GetUniqueName(Document.DefaultName, doc, (this.Parent as GroupListDocuments).Children);
                    (this.Parent.Parent as Config).SelectedNode = doc;
                    return doc;
                case "Property":
                    var prop = new Property();
                    if (this.Parent is GroupListProperties)
                    {
                        var pp = this.Parent as GroupListProperties;
                        pp.Add(prop);
                        GetUniqueName(Property.DefaultName, prop, pp.Children);
                    }
                    else
                        throw new Exception();
                    ITreeConfigNode config = this.Parent;
                    while (config.Parent != null)
                        config = config.Parent;
                    (config as Config).SelectedNode = prop;
                    return prop;
                case "Journal":
                    var journal = new Journal();
                    (this.Parent as GroupListJournals).Add(journal);
                    GetUniqueName(Enumeration.DefaultName, journal, (this.Parent as GroupListJournals).Children);
                    (this.Parent.Parent as Config).SelectedNode = journal;
                    return journal;
            }
            throw new Exception();
        }
        public bool NodeCanAddNewSubNode()
        {
            if (this is ICanAddSubNode)
                return true;
            return false;
        }
        public ITreeConfigNode NodeAddNewSubNode()
        {
            string tname = this.GetType().Name;
            switch (tname)
            {
                case "GroupListConstants":
                    var constant = new Constant();
                    var cnp = (this as GroupListConstants);
                    cnp.Add(constant);
                    GetUniqueName(Constant.DefaultName, constant, cnp.Children);
                    (this.Parent as Config).SelectedNode = constant;
                    return constant;
                case "GroupListEnumerations":
                    var enumeration = new Enumeration();
                    var cep = (this as GroupListEnumerations);
                    cep.Add(enumeration);
                    GetUniqueName(Enumeration.DefaultName, enumeration, cep.Children);
                    (this.Parent as Config).SelectedNode = enumeration;
                    return enumeration;
                case "GroupListProperties":
                    var prop = new Property();
                    var pp = (this as GroupListProperties);
                    pp.Add(prop);
                    GetUniqueName(Property.DefaultName, prop, pp.Children);
                    return UpdateSelectedNode(prop);
                case "GroupListPropertiesTabs":
                    var propt = new GroupPropertiesTab();
                    var ppropt = (this as GroupListPropertiesTabs);
                    ppropt.Add(propt);
                    GetUniqueName(GroupPropertiesTab.DefaultName, propt, ppropt.Children);
                    return UpdateSelectedNode(propt);
                case "GroupListCatalogs":
                    var catalog = new Catalog();
                    var cp = (this as GroupListCatalogs);
                    cp.Add(catalog);
                    GetUniqueName(Catalog.DefaultName, catalog, cp.Children);
                    (this.Parent as Config).SelectedNode = catalog;
                    return catalog;
                case "Catalog":
                    var prop2 = new Property();
                    var ppc = (this as Catalog);
                    ppc.GroupProperties.Add(prop2);
                    GetUniqueName(Property.DefaultName, prop2, ppc.GroupProperties.Children);
                    return UpdateSelectedNode(prop2);
                case "GroupListForms":
                    var form = new Form();
                    var pform = (this as GroupListForms);
                    pform.Add(form);
                    GetUniqueName(Form.DefaultName, form, pform.Children);
                    return UpdateSelectedNode(form);
                case "GroupListReports":
                    var rpt = new Report();
                    var prpt = (this as GroupListReports);
                    prpt.Add(rpt);
                    GetUniqueName(Report.DefaultName, rpt, prpt.Children);
                    return UpdateSelectedNode(rpt);
                case "GroupListDocuments":
                    var doc = new Document();
                    var pdoc = (this as GroupListDocuments);
                    pdoc.Add(doc);
                    GetUniqueName(Document.DefaultName, doc, pdoc.Children);
                    return UpdateSelectedNode(doc);
                case "GroupListJournals":
                    var journal = new Journal();
                    var jp = (this as GroupListJournals);
                    jp.Add(journal);
                    GetUniqueName(Journal.DefaultName, journal, jp.Children);
                    (this.Parent as Config).SelectedNode = journal;
                    return journal;
            }
            throw new Exception();
        }
        private ITreeConfigNode UpdateSelectedNode(ITreeConfigNode p)
        {
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = p;
            return p;
        }
        public bool NodeCanAddClone()
        {
            if (this is ICanAddNode)
                return true;
            return false;
        }
        public ITreeConfigNode NodeAddClone()
        {
            string tname = this.GetType().Name;
            switch (tname)
            {
                case "Catalog":
                    var catalog = Catalog.Clone(this.Parent, this as Catalog, true, true);
                    catalog.Parent = this.Parent;
                    (this.Parent as GroupListCatalogs).Add(catalog);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = catalog;
                    return catalog;
                case "Constant":
                    var constant = Constant.Clone(this.Parent, this as Constant, true, true);
                    constant.Parent = this.Parent;
                    (this.Parent as GroupListConstants).Add(constant);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = constant;
                    return constant;
                case "Enumeration":
                    var enumeration = Enumeration.Clone(this.Parent, this as Enumeration, true, true);
                    enumeration.Parent = this.Parent;
                    (this.Parent as GroupListEnumerations).Add(enumeration);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = enumeration;
                    return enumeration;
                case "Journal":
                    var journal = Journal.Clone(this.Parent, this as Journal, true, true);
                    journal.Parent = this.Parent;
                    (this.Parent as GroupListJournals).Add(journal);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = journal;
                    return journal;
                case "Property":
                    var prop = Property.Clone(this.Parent, this as Property, true, true);
                    prop.Parent = this.Parent;
                    if (this.Parent is GroupListProperties)
                    {
                        var pp = this.Parent as GroupListProperties;
                        pp.Add(prop);
                    }
                    else
                        throw new Exception();
                    this.Name = this.Name + "2";
                    ITreeConfigNode config = this.Parent;
                    while (config.Parent != null)
                        config = config.Parent;
                    (config as Config).SelectedNode = prop;
                    return prop;
            }
            throw new Exception();
        }
        public bool NodeCanMoveDown()
        {
            if (!(this is ICanAddNode))
                return false;
            return NodeCanDown();
        }
        public void NodeMoveDown()
        {
            if (this.Parent is IChildren)
                (this.Parent as IChildren).Children.MoveDown(this);
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = this;
        }
        public bool NodeCanMoveUp()
        {
            if (!(this is ICanAddNode))
                return false;
            return NodeCanUp();
        }
        public void NodeMoveUp()
        {
            if (this.Parent is IChildren)
                (this.Parent as IChildren).Children.MoveUp(this);
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = this;
        }
        public bool NodeCanRemove()
        {
            if (this is ICanAddNode)
                return true;
            return false;
        }
        public void NodeRemove()
        {
            if (this.Parent is IChildren)
            {
                (this.Parent as IChildren).Children.Remove((T)this);
                this.Parent = null;
            }
            else
                throw new Exception();
        }
        public bool NodeCanLeft()
        {
            if (this is ICanGoLeft)
                return true;
            return false;
        }
        public void NodeLeft()
        {
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = this.Parent;
        }
        public bool NodeCanRight()
        {
            if (this is ICanGoRight)
                return true;
            return false;
        }
        private bool IsIListNodesGen(object obj)
        {
            bool res = false;
            foreach (var t in obj.GetType().GetInterfaces())
            {
                if (t.Name.StartsWith("IListNodes`"))
                    return true;
            }
            return res;
        }
        public void NodeRight()
        {
            if (this is IChildren)
            {
                var p = (this as IChildren).Children[0];
                ITreeConfigNode config = this.Parent;
                while (config.Parent != null)
                    config = config.Parent;
                (config as Config).SelectedNode = p;
            }
        }
        public bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent is IChildren) && (this.Parent as IChildren).Children.CanDown(this))
                    return true;
            }
            return false;
        }
        public void NodeDown()
        {
            T next = null;
            if (this.Parent is IChildren)
                next = (this.Parent as IChildren).Children.GetNext(this) as T;
            if (next == null)
                return;
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = next;
        }
        public bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent is IChildren) && (this.Parent as IChildren).Children.CanUp(this))
                    return true;
            }
            return false;
        }
        public void NodeUp()
        {
            T prev = null;
            if (this.Parent is IChildren)
                prev = (this.Parent as IChildren).Children.GetPrev(this) as T;
            if (prev == null)
                return;
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = prev;
        }

        #endregion Commands
    }
}
