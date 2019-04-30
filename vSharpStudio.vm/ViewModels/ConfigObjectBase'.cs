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
            //            this.PropertyChanged += ConfigObjectWithGuidBase_PropertyChanged;
        }
        //private void ConfigObjectWithGuidBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "SortingValue")
        //    {
        //        ITreeConfigNode p = (ITreeConfigNode)this;
        //        if (p.Parent != null)
        //            p.Parent.Sort(this.GetType());
        //    }
        //}
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
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        protected override void OnCountWarningsChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        protected override void OnCountInfosChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        [BrowsableAttribute(false)]
        public string StatusIcon
        {
            get
            {
                string iconName = null;
                if (this.IsExpanded)
                {
                    if (this.CountErrors > 0)
                        iconName = "iconFolderOpenError";
                    else
                    {
                        if (this.CountWarnings > 0)
                            iconName = "iconFolderOpenWarning";
                        else
                        {
                            if (this.CountInfos > 0)
                                iconName = "iconFolderOpenInformation";
                            else
                                iconName = "iconFolderOpen";
                        }
                    }
                }
                else
                {
                    if (this.CountErrors > 0)
                        iconName = "iconFolderError";
                    else
                    {
                        if (this.CountWarnings > 0)
                            iconName = "iconFolderWarning";
                        else
                        {
                            if (this.CountInfos > 0)
                                iconName = "iconFolderInformation";
                            else
                                iconName = "iconFolder";
                        }
                    }
                }
                return iconName;
            }
        }

        public int CompareTo(T other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region ITreeConfigNode

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
        partial void OnSortingValueChanging();
        partial void OnSortingValueChanged();
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
        public ITreeConfigNode Parent { get; set; }
        [BrowsableAttribute(false)]
        public string NodeText { get { return this.Name; } }
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
                }
            }
            get { return _IsExpanded; }
        }
        private bool _IsExpanded;
        public virtual void Sort(Type type)
        {
            throw new NotImplementedException();
        }
        public bool NodeCanAddNew()
        {
            if (this.Parent != null)
            {
                foreach (var t in this.Parent.GetType().GetInterfaces())
                {
                    if (t.Name.StartsWith("IListNodes`"))
                        return true;
                }
            }
            return false;
        }
        public ITreeConfigNode NodeAddNew()
        {
            string tname = this.GetType().Name;
            switch (tname)
            {
                case "Constant":
                    var res = new Constant();
                    res.Parent = this.Parent;
                    (this.Parent as GroupListConstants).ListConstants.Add(res);
                    GetUniqueName(Constant.DefaultName, res, (this.Parent as GroupListConstants).ListConstants);
                    (this.Parent.Parent as Config).SelectedNode = res;
                    return res;
                case "Enumeration":
                    var enumeration = new Enumeration();
                    enumeration.Parent = this.Parent;
                    (this.Parent as GroupListEnumerations).ListEnumerations.Add(enumeration);
                    GetUniqueName(Enumeration.DefaultName, enumeration, (this.Parent as GroupListEnumerations).ListEnumerations);
                    (this.Parent.Parent as Config).SelectedNode = enumeration;
                    return enumeration;
                case "Catalog":
                    var catalog = new Catalog();
                    catalog.Parent = this.Parent;
                    (this.Parent as GroupListCatalogs).ListCatalogs.Add(catalog);
                    GetUniqueName(Catalog.DefaultName, catalog, (this.Parent as GroupListCatalogs).ListCatalogs);
                    (this.Parent.Parent as Config).SelectedNode = catalog;
                    return catalog;
                case "Document":
                    var doc = new Document();
                    doc.Parent = this.Parent;
                    (this.Parent as GroupListDocuments).ListDocuments.Add(doc);
                    GetUniqueName(Document.DefaultName, doc, (this.Parent as GroupListDocuments).ListDocuments);
                    (this.Parent.Parent as Config).SelectedNode = doc;
                    return doc;
                case "Property":
                    var prop = new Property();
                    prop.Parent = this.Parent;
                    if (this.Parent is Catalog)
                    {
                        var pp = this.Parent as Catalog;
                        pp.GroupProperties.ListProperties.Add(prop);
                        GetUniqueName(Property.DefaultName, prop, pp.GroupProperties.ListProperties);
                    }
                    else if (this.Parent is Catalog)
                    {
                        throw new Exception();
                        //var pp = this.Parent as Catalog;
                        //pp.GroupProperties.ListProperties.Add(prop);
                        //GetUniqueName(Property.DefaultName, prop, pp.GroupProperties.ListProperties);
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
                    journal.Parent = this.Parent;
                    (this.Parent as GroupListJournals).ListJournals.Add(journal);
                    GetUniqueName(Enumeration.DefaultName, journal, (this.Parent as GroupListJournals).ListJournals);
                    (this.Parent.Parent as Config).SelectedNode = journal;
                    return journal;
            }
            throw new Exception();
        }
        public bool NodeCanAddNewSubNode()
        {
            foreach (var t in this.GetType().GetInterfaces())
            {
                if (t.Name.StartsWith("IListNodes`"))
                    return true;
                if (t.Name.StartsWith("IListGroupNodes"))
                    return true;
            }
            return false;
        }
        public ITreeConfigNode NodeAddNewSubNode()
        {
            string tname = this.GetType().Name;
            switch (tname)
            {
                case "GroupListConstants":
                    var constant = new Constant();
                    constant.Parent = this;
                    var cnp = (this as GroupListConstants);
                    cnp.ListConstants.Add(constant);
                    GetUniqueName(Constant.DefaultName, constant, cnp.ListConstants);
                    (this.Parent as Config).SelectedNode = constant;
                    return constant;
                case "GroupListEnumerations":
                    var enumeration = new Enumeration();
                    enumeration.Parent = this;
                    var cep = (this as GroupListEnumerations);
                    cep.ListEnumerations.Add(enumeration);
                    GetUniqueName(Enumeration.DefaultName, enumeration, cep.ListEnumerations);
                    (this.Parent as Config).SelectedNode = enumeration;
                    return enumeration;
                case "GroupListProperties":
                    var prop = new Property();
                    prop.Parent = this;
                    var pp = (this as GroupListProperties);
                    pp.ListProperties.Add(prop);
                    GetUniqueName(Property.DefaultName, prop, pp.ListProperties);
                    ITreeConfigNode config = this.Parent;
                    while (config.Parent != null)
                        config = config.Parent;
                    (config as Config).SelectedNode = prop;
                    return prop;
                case "GroupListCatalogs":
                    var catalog = new Catalog();
                    catalog.Parent = this;
                    var cp = (this as GroupListCatalogs);
                    cp.ListCatalogs.Add(catalog);
                    GetUniqueName(Catalog.DefaultName, catalog, cp.ListCatalogs);
                    (this.Parent as Config).SelectedNode = catalog;
                    return catalog;
                case "Catalog":
                    var prop2 = new Property();
                    prop2.Parent = this;
                    var ppc = (this as Catalog);
                    ppc.GroupProperties.ListProperties.Add(prop2);
                    GetUniqueName(Property.DefaultName, prop2, ppc.GroupProperties.ListProperties);
                    ITreeConfigNode config2 = this.Parent;
                    while (config2.Parent != null)
                        config2 = config2.Parent;
                    (config2 as Config).SelectedNode = prop2;
                    return prop2;
                case "GroupListJournals":
                    var journal = new Journal();
                    journal.Parent = this;
                    var jp = (this as GroupListJournals);
                    jp.ListJournals.Add(journal);
                    GetUniqueName(Journal.DefaultName, journal, jp.ListJournals);
                    (this.Parent as Config).SelectedNode = journal;
                    return journal;
            }
            throw new Exception();
        }
        public bool NodeCanAddClone()
        {
            foreach (var t in this.GetType().GetInterfaces())
            {
                if (t.Name.StartsWith("IListNodes`"))
                    return false;
                if (t.Name.StartsWith("IListGroupNodes"))
                    return false;
            }
            return true;
        }
        public ITreeConfigNode NodeAddClone()
        {
            string tname = this.GetType().Name;
            switch (tname)
            {
                case "Catalog":
                    var catalog = Catalog.Clone(this.Parent, this as Catalog, true, true);
                    catalog.Parent = this.Parent;
                    (this.Parent as GroupListCatalogs).ListCatalogs.Add(catalog);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = catalog;
                    return catalog;
                case "Constant":
                    var constant = Constant.Clone(this.Parent, this as Constant, true, true);
                    constant.Parent = this.Parent;
                    (this.Parent as GroupListConstants).ListConstants.Add(constant);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = constant;
                    return constant;
                case "Enumeration":
                    var enumeration = Enumeration.Clone(this.Parent, this as Enumeration, true, true);
                    enumeration.Parent = this.Parent;
                    (this.Parent as GroupListEnumerations).ListEnumerations.Add(enumeration);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = enumeration;
                    return enumeration;
                case "Journal":
                    var journal = Journal.Clone(this.Parent, this as Journal, true, true);
                    journal.Parent = this.Parent;
                    (this.Parent as GroupListJournals).ListJournals.Add(journal);
                    this.Name = this.Name + "2";
                    (this.Parent.Parent as Config).SelectedNode = journal;
                    return journal;
                case "Property":
                    var prop = Property.Clone(this.Parent, this as Property, true, true);
                    prop.Parent = this.Parent;
                    if (this.Parent is GroupListProperties)
                    {
                        var pp = this.Parent as GroupListProperties;
                        pp.ListProperties.Add(prop);
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
            return NodeCanDown();
        }
        public void NodeMoveDown()
        {
            if (this.Parent is IListGroupNodes)
                (this.Parent as IListGroupNodes).ListNodes.MoveDown(this);
            if (this.Parent is IListNodes<T>)
                (this.Parent as IListNodes<T>).ListNodes.MoveDown(this);
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = this;
        }
        public bool NodeCanMoveUp()
        {
            return NodeCanUp();
        }
        public void NodeMoveUp()
        {
            if (this.Parent is IListGroupNodes)
                (this.Parent as IListGroupNodes).ListNodes.MoveUp(this);
            if (this.Parent is IListNodes<T>)
                (this.Parent as IListNodes<T>).ListNodes.MoveUp(this);
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = this;
        }
        public bool NodeCanRemove()
        {
            foreach (var t in this.GetType().GetInterfaces())
            {
                if (t.Name.StartsWith("IListNodes`"))
                    return false;
                if (t.Name.StartsWith("IListGroupNodes"))
                    return false;
            }
            return true;
        }
        public void NodeRemove()
        {
            if (this.Parent is IListNodes<T>)
                (this.Parent as IListNodes<T>).ListNodes.Remove((T)this);
            else
                throw new Exception();
        }
        public bool NodeCanLeft()
        {
            string tname = this.Parent.GetType().Name;
            switch (tname)
            {
                case "ConfigRoot":
                case "Config":
                    return false;
            }
            return true;
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
            if ((this is IListGroupNodes) && (this as IListGroupNodes).ListNodes.Count > 0)
                return true;
            if ((this is IGroupListSubNodes) && (this as IGroupListSubNodes).Count > 0)
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
            if (this is IListGroupNodes)
            {
                var p = (this as IListGroupNodes).ListNodes[0];
                ITreeConfigNode config = this.Parent;
                while (config.Parent != null)
                    config = config.Parent;
                (config as Config).SelectedNode = p;
            }
            else if (this is IGroupListSubNodes)
            {
                var p = (this as IGroupListSubNodes).GetNode(0);
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
                if ((this.Parent is IListGroupNodes) && (this.Parent as IListGroupNodes).ListNodes.CanDown(this))
                    return true;
                if ((this.Parent is IListNodes<T>) && (this.Parent as IListNodes<T>).ListNodes.CanDown(this))
                    return true;
            }
            return false;
        }
        public void NodeDown()
        {
            T next = null;
            if (this.Parent is IListGroupNodes)
                next = (this.Parent as IListGroupNodes).ListNodes.GetNext(this) as T;
            if (this.Parent is IListNodes<T>)
                next = (this.Parent as IListNodes<T>).ListNodes.GetNext(this) as T;
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
                if ((this.Parent is IListGroupNodes) && (this.Parent as IListGroupNodes).ListNodes.CanUp(this))
                    return true;
                if ((this.Parent is IListNodes<T>) && (this.Parent as IListNodes<T>).ListNodes.CanUp(this))
                    return true;
            }
            return false;
        }
        public void NodeUp()
        {
            T prev = null;
            if (this.Parent is IListGroupNodes)
                prev = (this.Parent as IListGroupNodes).ListNodes.GetPrev(this) as T;
            if (this.Parent is IListNodes<T>)
                prev = (this.Parent as IListNodes<T>).ListNodes.GetPrev(this) as T;
            if (prev == null)
                return;
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = prev;
        }

        #endregion ITreeConfigNode
    }
}
