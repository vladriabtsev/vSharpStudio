﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Google.Protobuf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.vm.Migration;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
//using System.Linq.Expressions;
//using System.Linq.Dynamic.Core;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigModel : ITreeModel, IMigration, ICanGoLeft, INodeGenDicSettings, IEditableNodeGroup, INodeGenSettings
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as Config;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        // public static readonly string DefaultName = "Config";
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        protected IMigration _migration { get; set; }
        [Browsable(false)]
        new public string IconName { get { return "icon3DScene"; } }
        //protected override string GetNodeIconName() { return "icon3DScene"; }
        partial void OnInit()
        {
            this._Name = "Model";
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.GroupConstants.Parent = this;
            this.GroupEnumerations.Parent = this;
            this.GroupCatalogs.Parent = this;
            this.GroupDocuments.Parent = this;
            this.GroupJournals.Parent = this;
            this.RefillChildren();
            // TODO validate, Id generator table, use in db names
            this.CompositeNameMaxLength = 100;
            // TODO validate
            this.IsUseCompositeNames = true;
            // TODO validate
            this.IsUseGroupPrefix = true;

            //this.DbSettings.DbSchema = "dbo";
            //this.DbSettings.IdGenerator = DbIdGeneratorMethod.HiLo;
            //this.DbSettings.PKeyFieldGuid= System.Guid.NewGuid().ToString();
            //this.DbSettings.PKeyName = "Id";
            //this.DbSettings.PKeyType = EnumPrimaryKeyType.INT;
            //this.DbSettings.VersionFieldGuid = System.Guid.NewGuid().ToString();
            //this.DbSettings.VersionFieldName = "Version";
        }

        protected override void OnInitFromDto()
        {
            this._Name = "ConfigModel";
            //this.RefillChildren();
        }
        void RefillChildren()
        {
            this.Children.Clear();
            this.Children.Add(this.GroupCommon, 6);
            this.Children.Add(this.GroupConstants, 7);
            this.Children.Add(this.GroupEnumerations, 8);
            this.Children.Add(this.GroupCatalogs, 9);
            this.Children.Add(this.GroupDocuments, 10);
            this.Children.Add(this.GroupJournals, 11);
        }

        #region Validation

        private CancellationTokenSource cancellationSourceForValidatingFullConfig = null;

        public async Task ValidateSubTreeFromNodeAsync(ITreeConfigNode node)
        {
            // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
            // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
            // https://devblogs.microsoft.com/pfxteam/asynclazyt/
            // https://github.com/StephenCleary/AsyncEx
            // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
            await Task.Run(() =>
            {
                this.ValidateSubTreeFromNode(node);
            }).ConfigureAwait(false); // not keeping context because doing nothing after await
        }

        public void ValidateSubTreeFromNode(ITreeConfigNode node, ILogger logger = null)
        {
            if (node == null)
            {
                return;
            }

            if (this.cancellationSourceForValidatingFullConfig != null)
            {
                this.cancellationSourceForValidatingFullConfig.Cancel();
                // if (logger != null && logger.IsEnabled)
                if (logger != null)
                {
                    logger.LogInformation("=== Cancellation request ===");
                }
            }
            this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
            var token = this.cancellationSourceForValidatingFullConfig.Token;

            var visitor = new ValidationConfigVisitor(token, logger);
            visitor.UpdateSubstructCounts(node);
            (node as IConfigAcceptVisitor).AcceptConfigNodeVisitor(visitor);
            if (!token.IsCancellationRequested)
            {
                // update for UI from another Thread (if from async version) (it is not only update, many others including CountErrors, CountWarnings ...
                node.ValidationCollection.Clear();
                node.ValidationCollection = visitor.Result;
            }
            else
            {
                logger.LogInformation("=== Cancelled ===");
            }
        }

        #endregion Validation

        #region IMigration

        public bool IsDatabaseServiceOn()
        {
            return this._migration.IsDatabaseServiceOn();
        }

        public Task<bool> IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            return this._migration.IsDatabaseServiceOnAsync(cancellationToken);
        }

        public bool IsDatabaseExists()
        {
            return this._migration.IsDatabaseExists();
        }

        public Task<bool> IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            return this._migration.IsDatabaseExistsAsync(cancellationToken);
        }

        public void CreateDatabase()
        {
            this._migration.CreateDatabase();
        }

        public Task CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            return this._migration.CreateDatabaseAsync(cancellationToken);
        }

        public void DropDatabase()
        {
            this._migration.DropDatabase();
        }

        public Task DropDatabaseAsync(CancellationToken cancellationToken)
        {
            return this._migration.DropDatabaseAsync(cancellationToken);
        }

        #endregion IMigration

        #region ITreeNode


        //partial void OnGroupConstantsChanged()
        //{
        //    this.RefillChildren();
        //}

        //partial void OnGroupCatalogsChanged()
        //{
        //    this.RefillChildren();
        //}

        //partial void OnGroupEnumerationsChanged()
        //{
        //    this.RefillChildren();
        //}

        #endregion ITreeNode

        #region Objects
        public IEnumerable<ITreeConfigNode> GetAllNodes()
        {
            yield return this.GroupEnumerations;
            foreach (var t in this.GroupEnumerations.ListEnumerations)
            {
                yield return t;
            }

            yield return this.GroupConstants;
            foreach (var t in this.GroupConstants.ListConstants)
            {
                yield return t;
            }

            yield return this.GroupCatalogs;
            foreach (var t in this.GroupCatalogs.ListCatalogs)
            {
                yield return t;
                yield return t.GroupProperties;
                foreach (var tt in t.GroupProperties.ListProperties)
                {
                    yield return tt;
                }

                yield return t.GroupPropertiesTabs;
                foreach (var tt in t.GroupPropertiesTabs.ListPropertiesTabs)
                {
                    yield return tt;
                    yield return tt.GroupProperties;
                    foreach (var ttt in tt.GroupProperties.ListProperties)
                    {
                        yield return ttt;
                    }
                }
                yield return t.GroupForms;
                foreach (var tt in t.GroupForms.ListForms)
                {
                    yield return tt;
                }

                yield return t.GroupReports;
                foreach (var tt in t.GroupReports.ListReports)
                {
                    yield return tt;
                }
            }
            yield return this.GroupDocuments;
            yield return this.GroupDocuments.GroupSharedProperties;
            foreach (var t in this.GroupDocuments.GroupSharedProperties.ListProperties)
            {
                yield return t;
            }

            yield return this.GroupDocuments.GroupListDocuments;
            foreach (var t in this.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                yield return t;
                yield return t.GroupProperties;
                foreach (var tt in t.GroupProperties.ListProperties)
                {
                    yield return tt;
                }

                yield return t.GroupPropertiesTabs;
                foreach (var tt in this.GetTabNodes(t.GroupPropertiesTabs as GroupListPropertiesTabs))
                {
                    yield return tt;
                }

                yield return t.GroupForms;
                foreach (var tt in t.GroupForms.ListForms)
                {
                    yield return tt;
                }

                yield return t.GroupReports;
                foreach (var tt in t.GroupReports.ListReports)
                {
                    yield return tt;
                }
            }
            yield return this.GroupJournals;
            foreach (var t in this.GroupJournals.ListJournals)
            {
                yield return t;
            }
        }

        private IEnumerable<ITreeConfigNode> GetTabNodes(GroupListPropertiesTabs tab)
        {
            foreach (var tt in tab.ListPropertiesTabs)
            {
                yield return tt;
                yield return tt.GroupProperties;
                foreach (var ttt in tt.GroupProperties.ListProperties)
                {
                    yield return ttt;
                }

                yield return tt.GroupPropertiesTabs;
                foreach (var ttt in this.GetTabNodes(tt.GroupPropertiesTabs as GroupListPropertiesTabs))
                {
                    yield return tt;
                }
            }
        }
        #endregion Objects

        //[PropertyOrderAttribute(12)]
        //[ExpandableObjectAttribute()]
        //[ReadOnly(true)]
        //[DisplayName("Defaults")]
        //[Description("Default nodes settings for generator")]
        //public object DynamicNodeDefaultSettings
        //{
        //    get
        //    {
        //        return this._DynamicNodeDefaultSettings;
        //    }
        //    set
        //    {
        //        if (this._DynamicNodeDefaultSettings != value)
        //        {
        //            this._DynamicNodeDefaultSettings = value;
        //            this.NotifyPropertyChanged();
        //            this.ValidateProperty();
        //        }
        //    }
        //}
        //private object _DynamicNodeDefaultSettings;

        [BrowsableAttribute(false)]
        public ITreeConfigNode SelectedNode
        {
            get
            {
                return this._SelectedNode;
            }

            set
            {
                if (this._SelectedNode != value)
                {
                    this._SelectedNode = value;
                    this.NotifyPropertyChanged();
                }
                if (this.OnSelectedNodeChanged != null)
                {
                    this.OnSelectedNodeChanged();
                }
            }
        }

        private ITreeConfigNode _SelectedNode;
        [BrowsableAttribute(false)]
        public Action OnSelectedNodeChanged { get; set; }

        #region Connection string editor

        // public Action<string> OnProviderSelectionChanged = null;
        // public List<ConnStringVM> ListConnectionStringVMs { get; set; }
        // public List<string> ListDbProviders { get; set; }
        // public string SelectedDbProvider
        // {
        //    get { return _SelectedDbProvider; }
        //    set
        //    {
        //        _SelectedDbProvider = value;
        //        OnProviderSelectionChanged(value);
        //    }
        // }
        // private string _SelectedDbProvider;

        #endregion Connection string editor

        public IDataType GetIdDataType()
        {
            DataType dt = default(DataType);
            switch (this.DbSettings.PKeyType)
            {
                case EnumPrimaryKeyType.INT:
                    dt = new DataType(int.MaxValue);
                    break;
                case EnumPrimaryKeyType.LONG:
                    dt = new DataType(long.MaxValue);
                    break;
                default:
                    throw new ArgumentException();
            }
            dt.IsPKey = true;
            return dt;
        }
        public string GetIdFieldName(IvPluginDbGenerator dbGen)
        {
            string fieldName = null;
            if (string.IsNullOrWhiteSpace(dbGen.PKeyName))
            {
                if (string.IsNullOrWhiteSpace(this.DbSettings.PKeyName))
                    return null;
                fieldName = this.DbSettings.PKeyName;
            }
            else
            {
                fieldName = dbGen.PKeyName;
            }
            return fieldName;
        }
        public string GetIdFieldGuid()
        {
            return this.DbSettings.PKeyFieldGuid;
        }
        //public string GetVersionFieldName(IvPluginDbGenerator dbGen)
        //{
        //    string fieldName = null;
        //    if (string.IsNullOrWhiteSpace(dbGen.VersionFieldName))
        //    {
        //        if (string.IsNullOrWhiteSpace(this.DbSettings.VersionFieldName))
        //            return null;
        //        fieldName = this.DbSettings.VersionFieldName;
        //    }
        //    else
        //    {
        //        fieldName = dbGen.VersionFieldName;
        //    }
        //    return fieldName;
        //}
        //public string GetVersionFieldGuid()
        //{
        //    return this.DbSettings.VersionFieldGuid;
        //}

        private IProperty GetIdProperty(IvPluginDbGenerator dbGen)
        {
            string fieldName = null;
            if (string.IsNullOrWhiteSpace(dbGen.PKeyName))
            {
                if (string.IsNullOrWhiteSpace(this.DbSettings.PKeyName))
                    return null;
                fieldName = this.DbSettings.PKeyName;
            }
            else
            {
                fieldName = dbGen.PKeyName;
            }
            var dt = (DataType)this.GetIdDataType();
            dt.IsPKey = true;
            if (string.IsNullOrWhiteSpace(this.DbSettings.PKeyFieldGuid))
                (this.DbSettings as DbSettings).PKeyFieldGuid = System.Guid.NewGuid().ToString();
            var res = new Property(default(ITreeConfigNode), this.DbSettings.PKeyFieldGuid, fieldName, dt);
            return res;
        }
        public IProperty GetRefParentProperty(IvPluginDbGenerator dbGen, ICompositeName parent)
        {
            var dt = (DataType)GetIdDataType();
            dt.IsRefParent = true;
            var res = new Property(default(ITreeConfigNode), (parent as IGuid).Guid, "Ref" + parent.CompositeName, dt);
            return res;
        }
        private List<IProperty> GetGroupProperties(IGroupListProperties g, string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            foreach (var tt in g.ListProperties)
            {
                if (!tt.IsIncluded(guidAppPrjGen))
                    continue;
                res.Add(tt);
            }
            return res;
        }
        private List<IConstant> GetGroupProperties(IGroupListConstants g, string guidAppPrjGen)
        {
            var res = new List<IConstant>();
            foreach (var tt in g.ListConstants)
            {
                if (!tt.IsIncluded(guidAppPrjGen))
                    continue;
                res.Add(tt);
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetListDocSharedProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            var cfg = (Config)this.Parent;
            res.AddRange(this.GetGroupProperties(cfg.Model.GroupDocuments.GroupSharedProperties, guidAppPrjGen));
            return res;
        }
        public IReadOnlyList<IProperty> GetListProperties(ITreeConfigNode node, string guidAppPrjGen)
        {
            var cfg = (Config)this.GetConfig();
            var apg = (AppProjectGenerator)cfg.DicNodes[guidAppPrjGen];
            IvPluginDbGenerator dbGen = apg.PluginDbGenerator;
            var lst = new List<IProperty>();
            if (node is IPropertiesTab)
            {
                var p = node as IPropertiesTab;
                lst.Add(this.GetIdProperty(dbGen));
                lst.Add(this.GetRefParentProperty(dbGen, p.Parent.Parent as ICompositeName));
                lst.AddRange(this.GetGroupProperties(p.GroupProperties, guidAppPrjGen));
            }
            else if (node is ICatalog)
            {
                var p = node as ICatalog;
                lst.Add(this.GetIdProperty(dbGen));
                lst.AddRange(this.GetGroupProperties(p.GroupProperties, guidAppPrjGen));
            }
            else if (node is IDocument)
            {
                var p = node as IDocument;
                lst.Add(this.GetIdProperty(dbGen));
                lst.AddRange(this.GetGroupProperties((p.Parent.Parent as IGroupDocuments).GroupSharedProperties, guidAppPrjGen));
                lst.AddRange(this.GetGroupProperties(p.GroupProperties, guidAppPrjGen));
            }
            //else if (node is IGroupListConstants)
            //{
            //    var p = node as IGroupListConstants;
            //    lst.Add(this.GetIdProperty(dbGen));
            //    lst.AddRange(this.GetGroupProperties(p, guidAppPrjGen));
            //}
            else
                Debug.Assert(false);
            return lst;
        }
        public IReadOnlyList<IPropertiesTab> GetListTabs(ITreeConfigNode node, string guidAppPrjGen)
        {
            var lst = new List<IPropertiesTab>();
            if (node is IPropertiesTab)
            {
                var p = node as IPropertiesTab;
                foreach (var tt in p.GroupPropertiesTabs.ListPropertiesTabs)
                {
                    if (tt.IsIncluded(guidAppPrjGen))
                    {
                        lst.Add(tt);
                    }
                }
            }
            else if (node is ICatalog)
            {
                var p = node as ICatalog;
                foreach (var tt in p.GroupPropertiesTabs.ListPropertiesTabs)
                {
                    if (tt.IsIncluded(guidAppPrjGen))
                    {
                        lst.Add(tt);
                    }
                }
            }
            else if (node is IDocument)
            {
                var p = node as IDocument;
                foreach (var tt in p.GroupPropertiesTabs.ListPropertiesTabs)
                {
                    if (tt.IsIncluded(guidAppPrjGen))
                    {
                        lst.Add(tt);
                    }
                }
            }
            else
                Debug.Assert(false);
            return lst;
        }
        public IReadOnlyList<IEnumeration> GetListEnumerations(string guidAppPrjGen)
        {
            var lst = new List<IEnumeration>();
            var cfg = this.Parent as Config;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in cfg.Model.GroupEnumerations.ListEnumerations)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        public IReadOnlyList<IEnumerationPair> GetListEnumerationPairs(IEnumeration node, string guidAppPrjGen)
        {
            var lst = new List<IEnumerationPair>();
            var cfg = this.Parent as Config;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in node.ListEnumerationPairs)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        public IReadOnlyList<IConstant> GetListConstants(string guidAppPrjGen)
        {
            var lst = new List<IConstant>();
            var cfg = this.Parent as Config;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in cfg.Model.GroupConstants.ListConstants)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        public IReadOnlyList<ICatalog> GetListCatalogs(string guidAppPrjGen)
        {
            var lst = new List<ICatalog>();
            var cfg = this.Parent as Config;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in cfg.Model.GroupCatalogs.ListCatalogs)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        public IReadOnlyList<IDocument> GetListDocuments(string guidAppPrjGen)
        {
            var lst = new List<IDocument>();
            var cfg = this.Parent as Config;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        //public IProperty GetVersionProperty(IvPluginDbGenerator dbGen)
        //{
        //    string fieldName = null;
        //    if (string.IsNullOrWhiteSpace(dbGen.VersionFieldName))
        //    {
        //        if (string.IsNullOrWhiteSpace(this.DbSettings.VersionFieldName))
        //            return null;
        //        fieldName = this.DbSettings.VersionFieldName;
        //    }
        //    else
        //    {
        //        fieldName = dbGen.VersionFieldName;
        //    }
        //    var dt = new DataType(EnumDataType.STRING);
        //    if (string.IsNullOrWhiteSpace(this.DbSettings.VersionFieldGuid))
        //        this.DbSettings.VersionFieldGuid = System.Guid.NewGuid().ToString();
        //    var res = new Property(default(ITreeConfigNode), this.DbSettings.VersionFieldGuid, fieldName, dt);
        //    return res;
        //}

        //[BrowsableAttribute(false)]
        //public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> DicPlugins { get; set; }
        //[ExpandableObjectAttribute()]
        //public object TestSettings { get { return _TestSettings; } }
        //private Test1 _TestSettings = new Test1();
        ////[ExpandableObjectAttribute()]
        //public List<object> DicPlugins { get { return _DicPlugins; } }
        //private List<object> _DicPlugins = new List<object>() { new Test1(), new Test2() };
        //[ExpandableObjectAttribute()]
        //public DynamicClass TestSettings2 { get { return _TestSettings2; } }
        //private DynamicClass _TestSettings2 = new DynamicClass(new List<Test1>()
        //{
        //    new Test1()
        //});
        //[ExpandableObjectAttribute()]
        //public dynamic TestSettings3
        //{
        //    get
        //    {
        //        if (_TestSettings3 == null)
        //        {
        //            _TestSettings3 = new System.Dynamic.ExpandoObject();
        //            _TestSettings3.EmployeeID = 42;
        //            _TestSettings3.Designation = "unknown";
        //            _TestSettings3.EmployeeName = "curt";
        //        }
        //        return _TestSettings3;
        //    }
        //}
        //private dynamic _TestSettings3;
        //      [ExpandableObjectAttribute()]
        //      public object TestSettings4
        //      {
        //          get
        //          {
        //              if (_TestSettings4 == null)
        //              {
        //                  var list = new Dictionary<string, string> {
        //  {
        //    "EmployeeID",
        //    "int"
        //  }, {
        //    "EmployeeName",
        //    "String"
        //  }, {
        //    "Birthday",
        //    "DateTime"
        //  }
        //};
        //                  IEnumerable<DynamicProperty> props = list.Select(property => new DynamicProperty(property.Key, Type.GetType(property.Value))).ToList();

        //                  Type t = DynamicExpression.CreateClass(props);
        //                  object obj = Activator.CreateInstance(t);
        //                  t.GetProperty("EmployeeID").SetValue(obj, 34, null);
        //                  t.GetProperty("EmployeeName").SetValue(obj, "Albert", null);
        //                  t.GetProperty("Birthday").SetValue(obj, new DateTime(1976, 3, 14), null);
        //              }
        //              return _TestSettings4;
        //          }
        //      }
        //      private object _TestSettings4;
    }
    //public class Test1
    //{
    //    public Test1()
    //    {
    //        this.Prop1 = new Test2();
    //        this.Prop2 = new Test2();
    //    }
    //    [ExpandableObjectAttribute()]
    //    public object Prop1 { get; set; }
    //    [ExpandableObjectAttribute()]
    //    public object Prop2 { get; set; }
    //}
    //public class Test2
    //{
    //    public string Prop21 { get; set; }
    //    public int Prop22 { get; set; }
    //}
    // https://stackoverflow.com/questions/3862226/how-to-dynamically-create-a-class
    //public class DynamicClass : System.Dynamic.DynamicObject
    //{
    //    private Dictionary<string, KeyValuePair<Type, object>> _fields;

    //    //public DynamicClass(List<Test1> fields)
    //    //{
    //    //    Contract.Requires(fields != null);
    //    //    _fields = new Dictionary<string, KeyValuePair<Type, object>>();
    //    //    fields.ForEach(x => _fields.Add(x.GetType().Name,
    //    //        new KeyValuePair<Type, object>(typeof(object), x)));
    //    //}
    //    public DynamicClass(List<PluginGeneratorMainSettings> fields)
    //    {
    //        Contract.Requires(fields != null);
    //        _fields = new Dictionary<string, KeyValuePair<Type, object>>();
    //        fields.ForEach(x => _fields.Add(x.Name,
    //            new KeyValuePair<Type, object>(typeof(object), x.SettingsVm)));
    //    }

    //    public override bool TrySetMember(System.Dynamic.SetMemberBinder binder, object value)
    //    {
    //        Contract.Requires(binder != null);
    //        Contract.Requires(value != null);
    //        if (_fields.ContainsKey(binder.Name))
    //        {
    //            var type = _fields[binder.Name].Key;
    //            if (value.GetType() == type)
    //            {
    //                _fields[binder.Name] = new KeyValuePair<Type, object>(type, value);
    //                return true;
    //            }
    //            else throw new Exception("Value " + value + " is not of type " + type.Name);
    //        }
    //        return false;
    //    }

    //    public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
    //    {
    //        Contract.Requires(binder != null);
    //        result = _fields[binder.Name].Value;
    //        return true;
    //    }
    //}
}
