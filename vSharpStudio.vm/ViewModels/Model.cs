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
using Google.Protobuf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.vm.Migration;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
//using System.Linq.Expressions;
//using System.Linq.Dynamic.Core;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Model:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Model : ITreeModel, IMigration, ICanGoLeft, INodeGenDicSettings, IEditableNodeGroup, INodeGenSettings //, IRoleAccess
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Config ParentConfig { get { Debug.Assert(this.Parent != null); return (Config)this.Parent; } }
        [Browsable(false)]
        public IConfig ParentConfigI { get { Debug.Assert(this.Parent != null); return (IConfig)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            var p = this.ParentConfig;
            return p.Children;
        }

        #endregion ITree

        protected IMigration? _migration { get; set; }
        [Browsable(false)]
        public new string IconName { get { return "icon3DScene"; } }
        //protected override string GetNodeIconName() { return "icon3DScene"; }
        partial void OnCreated()
        {
            this._Name = Defaults.ModelName;
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
            this.PKeyName = "Id";
            this.PKeyGuid = System.Guid.NewGuid().ToString();
            this.PKeyType = EnumPrimaryKeyType.INT;
            this.RecordVersionFieldName = "ReCoRdVeRsIoN";
            this.RecordVersionFieldGuid = System.Guid.NewGuid().ToString();
            this.RecordVersionFieldType = EnumVersionFieldType.VER_INT;

            this.PropertyCodeName = "Code";
            this.PropertyNameName = "Name";
            this.PropertyDescriptionName = "Description";
            this.PropertyIsFolderName = "IsFolder";
            this.PropertyDocCodeName = "DocNumber";
            this.PropertyDocDateName = "DocDate";

            this.UseCodeProperty = true;
            this.UseNameProperty = true;
            this.UseDocCodeProperty = true;
            this.UseDocDateProperty = true;

            Init();
            //this.InitRoles();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this._Name = "Model";
            if (this.Children.Count > 0)
                return;
            VmBindable.IsNotifyingStatic = false;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupCommon, 6);
            children.Add(this.GroupConstantGroups, 7);
            children.Add(this.GroupEnumerations, 8);
            children.Add(this.GroupCatalogs, 9);
            children.Add(this.GroupDocuments, 10);
            children.Add(this.GroupJournals, 11);
            VmBindable.IsNotifyingStatic = true;
            //this.ListMainViewForms.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListMainViewForms.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListMainViewForms.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListMainViewForms.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }

        #region Validation

        private readonly CancellationTokenSource? cancellationSourceForValidatingFullConfig = null;
        //public async Task ValidateSubTreeFromNodeAsync(ITreeConfigNode node)
        //{
        //    // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        //    // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
        //    // https://devblogs.microsoft.com/pfxteam/asynclazyt/
        //    // https://github.com/StephenCleary/AsyncEx
        //    // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
        //    await Task.Run(() =>
        //    {
        //        this.ValidateSubTreeFromNode(node);
        //    }).ConfigureAwait(false); // not keeping context because doing nothing after await
        //}
        //public void ValidateSubTreeFromNode(ITreeConfigNode node, ILogger? logger = null)
        //{
        //    if (node == null)
        //    {
        //        return;
        //    }

        //    if (this.cancellationSourceForValidatingFullConfig != null)
        //    {
        //        this.cancellationSourceForValidatingFullConfig.Cancel();
        //        // if (logger != null && logger.IsEnabled)
        //        if (logger != null)
        //        {
        //            logger.LogInformation("=== Cancellation request ===");
        //        }
        //    }
        //    this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
        //    var token = this.cancellationSourceForValidatingFullConfig.Token;

        //    var visitor = new ValidationConfigVisitor(token, logger);
        //    visitor.UpdateSubstructCounts(node);
        //    (node as IConfigAcceptVisitor)!.AcceptConfigNodeVisitor(visitor);
        //    if (!token.IsCancellationRequested)
        //    {
        //        // update for UI from another Thread (if from async version) (it is not only update, many others including CountErrors, CountWarnings ...
        //        node.ValidationCollection.Clear();
        //        node.ValidationCollection = visitor.Result;
        //    }
        //    else
        //    {
        //        logger?.LogInformation("=== Cancelled ===");
        //    }
        //}

        #endregion Validation

        #region IMigration

        public bool IsDatabaseServiceOn()
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseServiceOn();
        }

        public Task<bool> IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseServiceOnAsync(cancellationToken);
        }

        public bool IsDatabaseExists()
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseExists();
        }

        public Task<bool> IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseExistsAsync(cancellationToken);
        }

        public void CreateDatabase()
        {
            Debug.Assert(this._migration != null);
            this._migration.CreateDatabase();
        }

        public Task CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
            return this._migration.CreateDatabaseAsync(cancellationToken);
        }

        public void DropDatabase()
        {
            Debug.Assert(this._migration != null);
            this._migration.DropDatabase();
        }

        public Task DropDatabaseAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
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

                yield return t.GroupDetails;
                foreach (var tt in t.GroupDetails.ListDetails)
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

                yield return t.GroupDetails;
                foreach (var tt in this.GetTabNodes(t.GroupDetails as GroupListDetails))
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

        private IEnumerable<ITreeConfigNode> GetTabNodes(GroupListDetails tab)
        {
            foreach (var tt in tab.ListDetails)
            {
                yield return tt;
                yield return tt.GroupProperties;
                foreach (var ttt in tt.GroupProperties.ListProperties)
                {
                    yield return ttt;
                }

                yield return tt.GroupDetails;
                foreach (var ttt in this.GetTabNodes(tt.GroupDetails as GroupListDetails))
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

        [Browsable(false)]
        public ITreeConfigNode? SelectedNode
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

        private ITreeConfigNode? _SelectedNode;
        [Browsable(false)]
        public Action? OnSelectedNodeChanged { get; set; }

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
        public IDataType GetDataType(ITreeConfigNode? parent, int enumDataType, uint length, uint accuracy, bool isPositive, string objectGuid)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = Enum.Parse<EnumDataType>(enumDataType.ToString());
            dt.Length = length;
            dt.Accuracy = accuracy;
            dt.IsPositive = isPositive;
            dt.ObjectGuid = objectGuid;
            return dt;
        }
        public IDataType GetDataType(ITreeConfigNode? parent, EnumDataType enumDataType, uint length, bool isPositive)
        {
            DataType dt = new DataType(parent);
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
        public IDataType GetDataTypeFromMaxValue(ITreeConfigNode? parent, System.Numerics.BigInteger maxValue, bool isPositive, bool isPKey = false)
        {
            uint length = this.GetLengthFromMaxValue(maxValue);
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.IsPositive = isPositive;
            dt.IsPKey = isPKey;
            return dt;
        }
        // numerical
        public IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, uint accuracy)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.Accuracy = accuracy;
            return dt;
        }
        // numerical
        public IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, bool isPositive)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.IsPositive = isPositive;
            return dt;
        }
        // string
        public IDataType GetDataTypeString(ITreeConfigNode? parent, uint length)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.STRING;
            dt.Length = length;
            return dt;
        }
        // catalog
        public IDataType GetDataType(ITreeConfigNode? parent, ICatalog obj)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.CATALOG;
            dt.ObjectGuid = obj.Guid;
            return dt;
        }
        // document
        public IDataType GetDataType(ITreeConfigNode? parent, IDocument obj)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DOCUMENT;
            dt.ObjectGuid = obj.Guid;
            return dt;
        }
        public IDataType GetDataTypeBool(ITreeConfigNode? parent)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.BOOL;
            return dt;
        }
        public IDataType GetDataTypeDate(ITreeConfigNode? parent)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DATE;
            return dt;
        }
        public IDataType GetDataTypeDateTimeUtc(ITreeConfigNode? parent)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DATETIMEUTC;
            return dt;
        }
        public IDataType GetDataTypeDateTimeLocal(ITreeConfigNode? parent)
        {
            DataType dt = new DataType(parent);
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
        public IDataType GetDataTypeTime(ITreeConfigNode? parent)
        {
            DataType dt = new DataType(parent);
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

        [Browsable(false)]
        public string PKeyTypeStr
        {
            get
            {
                switch (this.PKeyType)
                {
                    case EnumPrimaryKeyType.INT:
                        return "int";
                    case EnumPrimaryKeyType.LONG:
                        return "long";
                }
                throw new NotImplementedException();
            }
        }
        private IProperty? GetPropertyId(ITreeConfigNode parent, IvPluginDbGenerator dbGen)
        {
            string fieldName;
            if (string.IsNullOrWhiteSpace(dbGen.PKeyName))
            {
                if (string.IsNullOrWhiteSpace(this.PKeyName))
                    return null;
                fieldName = this.PKeyName;
            }
            else
            {
                fieldName = dbGen.PKeyName;
            }
            if (string.IsNullOrWhiteSpace(this.PKeyGuid))
                this.PKeyGuid = System.Guid.NewGuid().ToString();
            var res = new Property(parent, this.PKeyGuid, fieldName, true);
            res.DataType = (DataType)this.GetIdDataType(res);
            res.DataType.IsPKey = true;
            return res;
        }
        public IDataType GetIdDataType(ITreeConfigNode? parent)
        {
            IDataType dt;
            switch (this.PKeyType)
            {
                case EnumPrimaryKeyType.INT:
                    dt = this.GetDataTypeFromMaxValue(parent, int.MaxValue, false, true);
                    break;
                case EnumPrimaryKeyType.LONG:
                    dt = this.GetDataTypeFromMaxValue(parent, long.MaxValue, false, true);
                    break;
                default:
                    throw new ArgumentException();
            }
            return dt;
        }
        public IDataType GetIdRefDataType(ITreeConfigNode? parent)
        {
            switch (this.PKeyType)
            {
                case EnumPrimaryKeyType.INT:
                    return this.GetDataTypeFromMaxValue(parent, int.MaxValue, false);
                case EnumPrimaryKeyType.LONG:
                    return this.GetDataTypeFromMaxValue(parent, long.MaxValue, false);
                default:
                    throw new ArgumentException();
            }
        }
        public IProperty GetPropertyId(ITreeConfigNode parent, string idGuid)
        {
            var res = new Property(parent, idGuid, this.PKeyName, true);
            res.DataType = (DataType)this.GetIdDataType(res);
            res.DataType.IsPKey = true;
            res.IsHidden = true;
            res.Position = 6;
            return res;
        }
        public IProperty GetPropertyVersion(ITreeConfigNode parent, string guid)
        {
            var res = new Property(parent, guid, this.RecordVersionFieldName, true);
            res.DataType = (DataType)this.GetDataTypeFromMaxValue(res, int.MaxValue, false);
            res.IsRecordVersion = true;
            res.IsHidden = true;
            res.IsNullable = false;
            res.Position = 7;
            return res;
        }
        public IProperty GetPropertyRefParent(ITreeConfigNode parent, string guid, string name, bool isNullable = false)
        {
            var res = new Property(parent, guid, name, true);
            res.DataType = (DataType)this.GetIdRefDataType(res);
            res.DataType.IsRefParent = true;
            res.IsHidden = true;
            res.Position = 8;
            res.IsNullable = isNullable;
            return res;
        }
        public IProperty GetPropertyCatalogCode(ITreeConfigNode parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.PropertyCodeName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length);
            res.Position = 9;
            return res;
        }
        public IProperty GetPropertyCatalogCodeInt(ITreeConfigNode parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.PropertyCodeName, true);
            res.DataType = (DataType)this.GetDataTypeFromMaxValue(res, int.MaxValue, false);
            res.Position = 9;
            return res;
        }
        public IProperty GetPropertyCatalogName(ITreeConfigNode parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.PropertyNameName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length);
            res.Position = 10;
            return res;
        }
        public IProperty GetPropertyCatalogDescription(ITreeConfigNode parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.PropertyDescriptionName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length);
            res.Position = 11;
            return res;
        }
        public IProperty GetPropertyIsFolder(ITreeConfigNode parent, string guid)
        {
            var res = new Property(parent, guid, this.PropertyIsFolderName, true);
            res.DataType = new DataType(res) { DataTypeEnum = EnumDataType.BOOL };
            res.IsHidden = true;
            res.IsNullable = false;
            res.Position = 12;
            return res;
        }
        public IProperty GetPropertyDocumentDate(ITreeConfigNode parent, string guid)
        {
            var res = new Property(parent, guid, this.PropertyDocDateName, true);
            res.DataType = (DataType)this.GetDataTypeDateTimeUtc(res);
            res.AccuracyForTime = EnumTimeAccuracyType.MAX;
            res.Position = 8;
            return res;
        }
        public IProperty GetPropertyDocNumberString(ITreeConfigNode parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.PropertyDocCodeName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length);
            res.Position = 9;
            return res;
        }
        public IProperty GetPropertyDocNumberInt(ITreeConfigNode parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.PropertyDocCodeName, true);
            res.DataType = (DataType)this.GetDataTypeFromMaxValue(res, int.MaxValue, false);
            res.Position = 9;
            return res;
        }
        public IProperty GetPropertyDocNumberUniqueScopeHelper(ITreeConfigNode parent, string guid)
        {
            var res = new Property(parent, guid, this.PropertyDocCodeName+"UniqueScopeHelper", true);
            res.DataType = (DataType)this.GetDataTypeFromMaxValue(res, int.MaxValue, false);
            res.Position = 10;
            res.IsNullable = true;
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
            var cfg = this.ParentConfig;
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
            var cfg = this.ParentConfig;
            //var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
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
            var cfg = this.ParentConfig;
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
            var cfg = this.ParentConfig;
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
            var cfg = this.ParentConfig;
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
            var cfg = this.ParentConfig;
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
        public void VisitTabs(string appGenGuig, bool isOptimistic, EnumVisitType typeOp, ITreeConfigNode p, Action<IReadOnlyList<TableInfo>> action)
        {
            if (p is IDetail dt)
            {
                this.VisitTabs(appGenGuig, isOptimistic, dt, action, typeOp);
            }
            else if (p is ICatalog c)
            {
                this.VisitTabs(appGenGuig, isOptimistic, c, action, typeOp);
            }
            else if (p is ICatalogFolder cf)
            {
                this.VisitTabs(appGenGuig, isOptimistic, cf, action, typeOp);
            }
            else if (p is IDocument d)
            {
                this.VisitTabs(appGenGuig, isOptimistic, d, action, typeOp);
            }
            else if (p is IGroupListConstants)
            {
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private void TabsRecursive(string appGenGuig, bool isOptimistic, IReadOnlyList<IDetail> lstt, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo> lst)
        {
            foreach (var t in lstt)
            {
                Debug.Assert(t.Parent != null);
                Debug.Assert(t.Parent.Parent != null);
                var ti = new TableInfo(t.Name, t.CompositeName, (t.Parent.Parent as ICompositeName)!.CompositeName, t, t.GetIncludedProperties(appGenGuig, isOptimistic));
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
                    var lstt2 = t.GetIncludedDetails(appGenGuig);
                    TabsRecursive(appGenGuig, isOptimistic, lstt2, action, typeOp, lst);
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
                    var lstt2 = t.GetIncludedDetails(appGenGuig);
                    TabsRecursive(appGenGuig, isOptimistic, lstt2, action, typeOp, lst);
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
        private void VisitTabs(string appGenGuig, bool isOptimistic, ICatalog p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo>? lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedDetails(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, isOptimistic, lstt, action, typeOp, lst);
        }
        private void VisitTabs(string appGenGuig, bool isOptimistic, ICatalogFolder p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo>? lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedDetails(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, isOptimistic, lstt, action, typeOp, lst);
        }
        private void VisitTabs(string appGenGuig, bool isOptimistic, IDocument p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo>? lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedDetails(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, isOptimistic, lstt, action, typeOp, lst);
        }
        private void VisitTabs(string appGenGuig, bool isOptimistic, IDetail p, Action<List<TableInfo>> action, EnumVisitType typeOp, List<TableInfo>? lst = null)
        {
            if (lst == null)
                lst = new List<TableInfo>();
            var lstt = p.GetIncludedDetails(appGenGuig);
            if (lstt.Count == 0)
                return;
            TabsRecursive(appGenGuig, isOptimistic, lstt, action, typeOp, lst);
        }
        #endregion Utils

        public void Remove()
        {
            throw new NotImplementedException();
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }

    }
    // https://stackoverflow.com/questions/3862226/how-to-dynamically-create-a-class
    //public class DynamicClass : System.Dynamic.DynamicObject
    //{
    //    private Dictionary<string, KeyValuePair<Type, object>> _fields;

    //    //public DynamicClass(List<Test1> fields)
    //    //{
    //    //    Debug.Assert(fields != null);
    //    //    _fields = new Dictionary<string, KeyValuePair<Type, object>>();
    //    //    fields.ForEach(x => _fields.Add(x.GetType().Name,
    //    //        new KeyValuePair<Type, object>(typeof(object), x)));
    //    //}
    //    public DynamicClass(List<PluginGeneratorMainSettings> fields)
    //    {
    //        Debug.Assert(fields != null);
    //        _fields = new Dictionary<string, KeyValuePair<Type, object>>();
    //        fields.ForEach(x => _fields.Add(x.Name,
    //            new KeyValuePair<Type, object>(typeof(object), x.SettingsVm)));
    //    }

    //    public override bool TrySetMember(System.Dynamic.SetMemberBinder binder, object value)
    //    {
    //        Debug.Assert(binder != null);
    //        Debug.Assert(value != null);
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
    //        Debug.Assert(binder != null);
    //        result = _fields[binder.Name].Value;
    //        return true;
    //    }
    //}
}
