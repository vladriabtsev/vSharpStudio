using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.WebSockets;
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
    public partial class Model : ITreeModel, IMigration, ICanGoLeft, INodeGenDicSettings, IEditableNodeGroup, INodeGenSettings
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
            this.GroupConstantGroups.Parent = this;
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

            this.DbSettings.Parent = this;

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
            this._Name = "Model";
            //this.RefillChildren();
        }
        void RefillChildren()
        {
            VmBindable.IsNotifyingStatic = false;
            this.Children.Clear();
            this.Children.Add(this.GroupCommon, 6);
            this.Children.Add(this.GroupConstantGroups, 7);
            this.Children.Add(this.GroupEnumerations, 8);
            this.Children.Add(this.GroupCatalogs, 9);
            this.Children.Add(this.GroupDocuments, 10);
            this.Children.Add(this.GroupJournals, 11);
            this.DbSettings.Parent = this;
            VmBindable.IsNotifyingStatic = true;
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

            yield return this.GroupConstantGroups;
            foreach (var t in this.GroupConstantGroups.ListConstantGroups)
            {
                yield return t;
                foreach (var tt in t.ListConstants)
                {
                    yield return tt;
                }
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

        #region Utils
        public IDataType GetDataType(int enumDataType, uint length, uint accuracy, bool isPositive, string objectGuid)
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = Enum.Parse<EnumDataType>(enumDataType.ToString());
            dt.Length = length;
            dt.Accuracy = accuracy;
            dt.IsPositive = isPositive;
            dt.ObjectGuid = objectGuid;
            return dt;
        }
        public IDataType GetDataType(EnumDataType enumDataType, uint length, bool isPositive)
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = enumDataType;
            dt.Length = length;
            dt.IsPositive = isPositive;
            return dt;
        }
        public uint GetLengthFromMaxValue(System.Numerics.BigInteger maxValue)
        {
            uint length = 0;
            System.Numerics.BigInteger m = maxValue;
            while (m > 10)
            {
                m = m / 10;
                length++;
            }
            return length;
        }
        // numerical
        public IDataType GetDataTypeFromMaxValue(System.Numerics.BigInteger maxValue, bool isPositive)
        {
            uint length = this.GetLengthFromMaxValue(maxValue);
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.IsPositive = isPositive;
            return dt;
        }
        // numerical
        public IDataType GetDataTypeNumerical(uint length, uint accuracy)
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.Accuracy = accuracy;
            return dt;
        }
        // numerical
        public IDataType GetDataTypeNumerical(uint length, bool isPositive)
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.IsPositive = isPositive;
            return dt;
        }
        // string
        public IDataType GetDataTypeString(uint length)
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.STRING;
            dt.Length = length;
            return dt;
        }
        // catalog
        public IDataType GetDataType(ICatalog obj)
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.CATALOG;
            dt.ObjectGuid = obj.Guid;
            return dt;
        }
        // document
        public IDataType GetDataType(IDocument obj)
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.DOCUMENT;
            dt.ObjectGuid = obj.Guid;
            return dt;
        }
        public IDataType GetDataTypeBool()
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.BOOL;
            return dt;
        }
        public IDataType GetDataTypeDate()
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.DATE;
            return dt;
        }
        public IDataType GetDataTypeDateTimeUtc()
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.DATETIMEUTC;
            return dt;
        }
        public IDataType GetDataTypeDateTimeLocal()
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.DATETIMELOCAL;
            return dt;
        }
        //public IDataType GetDataTypeDateTime(bool isNullable = true)
        //{
        //    DataType dt = new DataType();
        //    dt.DataTypeEnum = EnumDataType.DATETIME;
        //    dt.IsNullable = isNullable;
        //    return dt;
        //}
        //public IDataType GetDataTypeDateTimeZ()
        //{
        //    DataType dt = new DataType();
        //    dt.DataTypeEnum = EnumDataType.DATETIMEZ;
        //    return dt;
        //}
        public IDataType GetDataTypeTime()
        {
            DataType dt = new DataType();
            dt.DataTypeEnum = EnumDataType.TIME;
            return dt;
        }
        //public IDataType GetDataTypeTimeZ(bool isNullable = true)
        //{
        //    DataType dt = new DataType();
        //    dt.DataTypeEnum = EnumDataType.TIMEZ;
        //    dt.IsNullable = isNullable;
        //    return dt;
        //}
        public IDataType GetIdRefDataType()
        {
            var dt = (DataType)GetIdDataType();
            return dt;
        }
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
        //public string GetIdFieldName(IvPluginDbGenerator dbGen)
        //{
        //    string fieldName = null;
        //    if (string.IsNullOrWhiteSpace(dbGen.PKeyName))
        //    {
        //        if (string.IsNullOrWhiteSpace(this.DbSettings.PKeyName))
        //            return null;
        //        fieldName = this.DbSettings.PKeyName;
        //    }
        //    else
        //    {
        //        fieldName = dbGen.PKeyName;
        //    }
        //    return fieldName;
        //}
        //public string GetIdFieldGuid()
        //{
        //    return this.DbSettings.PKeyFieldGuid;
        //}
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

        //private IProperty GetPropertyId(IvPluginDbGenerator dbGen)
        //{
        //    string fieldName = null;
        //    if (string.IsNullOrWhiteSpace(dbGen.PKeyName))
        //    {
        //        if (string.IsNullOrWhiteSpace(this.DbSettings.PKeyName))
        //            return null;
        //        fieldName = this.DbSettings.PKeyName;
        //    }
        //    else
        //    {
        //        fieldName = dbGen.PKeyName;
        //    }
        //    var dt = (DataType)this.GetIdDataType();
        //    dt.IsPKey = true;
        //    if (string.IsNullOrWhiteSpace(this.DbSettings.PKeyFieldGuid))
        //        this.DbSettings.PKeyFieldGuid = System.Guid.NewGuid().ToString();
        //    var res = new Property(default(ITreeConfigNode), this.DbSettings.PKeyFieldGuid, fieldName, dt);
        //    res.IsPKey = true;
        //    return res;
        //}
        public IProperty GetPropertyId(string idGuid)
        {
            var dt = (DataType)this.GetIdDataType();
            var res = new Property(default(ITreeConfigNode), idGuid, this.DbSettings.PKeyName, dt);
            res.Position = 6;
            res.IsPKey = true;
            return res;
        }
        public IProperty GetPropertyRefParent(string guid, string name, bool isNullable = false)
        {
            var dt = (DataType)this.GetIdDataType();
            dt.IsRefParent = true;
            var res = new Property(default(ITreeConfigNode), guid, name, dt);
            res.Position = 7;
            res.IsNullable = isNullable;
            return res;
        }
        public IProperty GetPropertyCatalogCode(string guid, uint length)
        {
            var dt = (DataType)this.GetDataTypeString(length);
            var res = new Property(default(ITreeConfigNode), guid, this.GroupCatalogs.PropertyCodeName, dt);
            res.Position = 8;
            return res;
        }
        public IProperty GetPropertyCatalogCodeInt(string guid, uint length)
        {
            var dt = (DataType)this.GetDataTypeFromMaxValue(int.MaxValue, false);
            var res = new Property(default(ITreeConfigNode), guid, this.GroupCatalogs.PropertyCodeName, dt);
            res.Position = 8;
            return res;
        }
        public IProperty GetPropertyCatalogName(string guid, uint length)
        {
            var dt = (DataType)this.GetDataTypeString(length);
            var res = new Property(default(ITreeConfigNode), guid, this.GroupCatalogs.PropertyNameName, dt);
            res.Position = 9;
            return res;
        }
        public IProperty GetPropertyCatalogDescription(string guid, uint length)
        {
            var dt = (DataType)this.GetDataTypeString(length);
            var res = new Property(default(ITreeConfigNode), guid, this.GroupCatalogs.PropertyDescriptionName, dt);
            res.Position = 10;
            return res;
        }
        public IProperty GetPropertyIsFolder(string guid)
        {
            var dt = new DataType();
            dt.DataTypeEnum = EnumDataType.BOOL;
            var res = new Property(default(ITreeConfigNode), guid, this.GroupCatalogs.PropertyIsFolderName, dt);
            res.IsNullable = false;
            res.Position = 11;
            return res;
        }
        public IProperty GetPropertyIsOpen(string guid)
        {
            var dt = new DataType();
            dt.DataTypeEnum = EnumDataType.BOOL;
            var res = new Property(default(ITreeConfigNode), guid, this.GroupCatalogs.PropertyIsOpenName, dt);
            res.IsNullable = false;
            res.Position = 12;
            return res;
        }
        public IProperty GetPropertyDocumentDate(string guid)
        {
            var dt = (DataType)this.GetDataTypeDateTimeUtc();
            var res = new Property(default(ITreeConfigNode), guid, this.GroupDocuments.PropertyDateName, dt);
            res.AccuracyForTime = EnumTimeAccuracyType.MAX;
            res.Position = 7;
            return res;
        }
        public IProperty GetPropertyDocumentCodeString(string guid, uint length)
        {
            var dt = (DataType)this.GetDataTypeString(length);
            var res = new Property(default(ITreeConfigNode), guid, this.GroupDocuments.PropertyCodeName, dt);
            res.Position = 8;
            return res;
        }
        public IProperty GetPropertyDocumentCodeInt(string guid, uint length)
        {
            var dt = (DataType)this.GetDataTypeFromMaxValue(int.MaxValue, false);
            var res = new Property(default(ITreeConfigNode), guid, this.GroupDocuments.PropertyCodeName, dt);
            res.Position = 8;
            return res;
        }
        //public IProperty GetPropertyString(string guid, uint length, string name)
        //{
        //    var dt = (DataType)this.GetDataType(length);
        //    var res = new Property(default(ITreeConfigNode), guid, name, dt);
        //    return res;
        //}
        //public IProperty GetPropertyBool(string guid, string name, bool isNullable)
        //{
        //    var dt = new DataType();
        //    dt.DataTypeEnum = EnumDataType.BOOL;
        //    dt.IsNullable = isNullable;
        //    var res = new Property(default(ITreeConfigNode), guid, name, dt);
        //    return res;
        //}
        //public IProperty GetPropertyInt(string guid, uint length, string name)
        //{
        //    var dt = (DataType)this.GetDataTypeFromMaxValue(int.MaxValue, false);
        //    var res = new Property(default(ITreeConfigNode), guid, name, dt);
        //    return res;
        //}

        public IReadOnlyList<IEnumeration> GetListEnumerations(string guidAppPrjGen)
        {
            var lst = new List<IEnumeration>();
            var cfg = this.Parent as Config;
            //var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
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
        public IReadOnlyList<IGroupListConstants> GetListConstantGroups(string guidAppPrjGen)
        {
            var lst = new List<IGroupListConstants>();
            var cfg = this.Parent as Config;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in cfg.Model.GroupConstantGroups.ListConstantGroups)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        public IReadOnlyList<IConstant> GetListConstants(IGroupListConstants group, string guidAppPrjGen)
        {
            var lst = new List<IConstant>();
            var cfg = this.Parent as Config;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in group.ListConstants)
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
        public void VisitTabs(string appGenGuig, EnumVisitType typeOp, ITreeConfigNode p, Action<IReadOnlyList<TableInfo>> action)
        {
            if (p is IPropertiesTab)
            {
                this.VisitTabs(appGenGuig, p as IPropertiesTab, action, typeOp);
            }
            else if (p is ICatalog)
            {
                this.VisitTabs(appGenGuig, p as ICatalog, action, typeOp);
            }
            else if (p is ICatalogFolder)
            {
                this.VisitTabs(appGenGuig, p as ICatalogFolder, action, typeOp);
            }
            else if (p is IDocument)
            {
                this.VisitTabs(appGenGuig, p as IDocument, action, typeOp);
            }
            else if (p is IGroupListConstants)
            {
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private void TabsRecursive(string appGenGuig, IReadOnlyList<IPropertiesTab> lstt, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo> lst)
        {
            foreach (var t in lstt)
            {
                var ti = new TableInfo()
                {
                    Node = t,
                    List = t.GetIncludedProperties(appGenGuig),
                    ClassName = t.Name,
                    TableName = t.CompositeName,
                    TableParent = (t.Parent.Parent as ICompositeName).CompositeName
                };
                if (typeOp == EnumVisitType.Load) // from current to top
                {
                    lst.Add(ti);
                    List<TableInfo> lstReverse = new List<TableInfo>();
                    for (int i = lst.Count - 1; i > -1; i--)
                    {
                        lstReverse.Add(lst[i]);
                    }
                    if (lstReverse.Count > 0)
                        action(lstReverse);
                    var lstt2 = t.GetIncludedPropertiesTabs(appGenGuig);
                    TabsRecursive(appGenGuig, lstt2, action, typeOp, lst);
                    lst.Remove(ti);
                }
                else if (typeOp == EnumVisitType.Remove)
                {
                    lst.Add(ti);
                    List<TableInfo> lstReverse = new List<TableInfo>();
                    for (int i = lst.Count - 1; i > -1; i--)
                    {
                        lstReverse.Add(lst[i]);
                    }
                    var lstt2 = t.GetIncludedPropertiesTabs(appGenGuig);
                    TabsRecursive(appGenGuig, lstt2, action, typeOp, lst);
                    if (lstReverse.Count > 0)
                        action(lstReverse);
                    lst.Remove(ti);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        private void VisitTabs(string appGenGuig, ICatalog p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo> lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedPropertiesTabs(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, lstt, action, typeOp, lst);
        }
        private void VisitTabs(string appGenGuig, ICatalogFolder p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo> lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedPropertiesTabs(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, lstt, action, typeOp, lst);
        }
        private void VisitTabs(string appGenGuig, IDocument p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo> lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedPropertiesTabs(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, lstt, action, typeOp, lst);
        }
        private void VisitTabs(string appGenGuig, IPropertiesTab p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo> lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedPropertiesTabs(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, lstt, action, typeOp, lst);
        }
        #endregion Utils

        public void Remove()
        {
            throw new NotImplementedException();
        }
    }
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
