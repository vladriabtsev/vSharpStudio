using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicationLogging;
using CommunityToolkit.Diagnostics;
using Google.Protobuf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.vm.Migration;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
//using System.Linq.Expressions;
//using System.Linq.Dynamic.Core;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
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
            this._CompositeNameMaxLength = 100;
            // TODO validate
            this._IsUseNameComposition = true;

            //this.DbSettings.DbSchema = "dbo";
            //this.DbSettings.IdGenerator = DbIdGeneratorMethod.HiLo;
            //this.DbSettings.PKeyFieldGuid= System.Guid.NewGuid().ToString();
            //this.DbSettings.PKeyName = "Id";
            //this.DbSettings.PKeyType = EnumPrimaryKeyType.INT;
            //this.DbSettings.VersionFieldGuid = System.Guid.NewGuid().ToString();
            //this.DbSettings.VersionFieldName = "Version";
            this._PropertyCtlgCodeGuid = System.Guid.NewGuid().ToString();
            this._PropertyCtlgDescriptionGuid = System.Guid.NewGuid().ToString();
            this._PropertyCtlgIsFolderGuid = System.Guid.NewGuid().ToString();
            this._PropertyCtlgNameGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocDateGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocIsPostedGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocNumberGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocShortTypeIdGuid = System.Guid.NewGuid().ToString();
            this._PropertyIdGuid = System.Guid.NewGuid().ToString();
            this._PropertyVersionGuid = System.Guid.NewGuid().ToString();

            this._PKeyName = "Id";
            this._PKeyType = EnumPrimaryKeyType.INT;
            this._RecordVersionFieldName = "ReCoRdVeRsIoN";
            this._RecordVersionFieldType = EnumVersionFieldType.VER_INT;

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
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupCommon, 6);
            children.Add(this.GroupEnumerations, 7);
            children.Add(this.GroupConstantGroups, 8);
            children.Add(this.GroupCatalogs, 9);
            children.Add(this.GroupRelations, 10);
            children.Add(this.GroupDocuments, 11);
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
        //public IEnumerable<ITreeConfigNode> GetAllNodes()
        //{
        //    yield return this.GroupEnumerations;
        //    foreach (var t in this.GroupEnumerations.ListEnumerations)
        //    {
        //        yield return t;
        //    }

        //    yield return this.GroupConstantGroups;
        //    foreach (var t in this.GroupConstantGroups.ListConstantGroups)
        //    {
        //        yield return t;
        //        foreach (var tt in t.ListConstants)
        //        {
        //            yield return tt;
        //        }
        //    }

        //    yield return this.GroupCatalogs;
        //    foreach (var t in this.GroupCatalogs.ListCatalogs)
        //    {
        //        yield return t;
        //        yield return t.GroupProperties;
        //        foreach (var tt in t.GroupProperties.ListProperties)
        //        {
        //            yield return tt;
        //        }

        //        yield return t.GroupDetails;
        //        foreach (var tt in t.GroupDetails.ListDetails)
        //        {
        //            yield return tt;
        //            yield return tt.GroupProperties;
        //            foreach (var ttt in tt.GroupProperties.ListProperties)
        //            {
        //                yield return ttt;
        //            }
        //        }
        //        yield return t.GroupForms;
        //        foreach (var tt in t.GroupForms.ListForms)
        //        {
        //            yield return tt;
        //        }

        //        yield return t.GroupReports;
        //        foreach (var tt in t.GroupReports.ListReports)
        //        {
        //            yield return tt;
        //        }
        //    }
        //    yield return this.GroupDocuments;
        //    yield return this.GroupDocuments.GroupSharedProperties;
        //    foreach (var t in this.GroupDocuments.GroupSharedProperties.ListProperties)
        //    {
        //        yield return t;
        //    }

        //    yield return this.GroupDocuments.GroupListDocuments;
        //    foreach (var t in this.GroupDocuments.GroupListDocuments.ListDocuments)
        //    {
        //        yield return t;
        //        yield return t.GroupProperties;
        //        foreach (var tt in t.GroupProperties.ListProperties)
        //        {
        //            yield return tt;
        //        }

        //        yield return t.GroupDetails;
        //        foreach (var tt in this.GetTabNodes(t.GroupDetails as GroupListDetails))
        //        {
        //            yield return tt;
        //        }

        //        yield return t.GroupForms;
        //        foreach (var tt in t.GroupForms.ListForms)
        //        {
        //            yield return tt;
        //        }

        //        yield return t.GroupReports;
        //        foreach (var tt in t.GroupReports.ListReports)
        //        {
        //            yield return tt;
        //        }
        //    }
        //    yield return this.GroupJournals;
        //    foreach (var t in this.GroupJournals.ListJournals)
        //    {
        //        yield return t;
        //    }
        //}

        //private IEnumerable<ITreeConfigNode> GetTabNodes(GroupListDetails tab)
        //{
        //    foreach (var tt in tab.ListDetails)
        //    {
        //        yield return tt;
        //        yield return tt.GroupProperties;
        //        foreach (var ttt in tt.GroupProperties.ListProperties)
        //        {
        //            yield return ttt;
        //        }

        //        yield return tt.GroupDetails;
        //        foreach (var ttt in this.GetTabNodes(tt.GroupDetails as GroupListDetails))
        //        {
        //            yield return tt;
        //        }
        //    }
        //}
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
                if (SetProperty(ref this._SelectedNode, value))
                {
                    this.OnSelectedNodeChanged?.Invoke();
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
        public IDataType GetDataType(ITreeConfigNode? parent, int enumDataType, uint length, uint accuracy, bool isPositive, string objectGuid, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = Enum.Parse<EnumDataType>(enumDataType.ToString());
            dt.Length = length;
            dt.Accuracy = accuracy;
            dt.IsPositive = isPositive;
            dt.ObjectRef.ForeignObjectGuid = objectGuid;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataType(ITreeConfigNode? parent, EnumDataType enumDataType, uint length, bool isPositive, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = enumDataType;
            dt.Length = length;
            dt.IsPositive = isPositive;
            dt.IsNullable = isNullable;
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
        public IDataType GetDataTypeFromMaxValue(ITreeConfigNode? parent, System.Numerics.BigInteger maxValue, bool isPositive, bool isNullable, bool isPKey = false)
        {
            uint length = this.GetLengthFromMaxValue(maxValue);
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.IsPositive = isPositive;
            dt.IsNullable = isNullable;
            dt.IsPKey = isPKey;
            return dt;
        }
        // numerical
        public IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, uint accuracy, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.Accuracy = accuracy;
            dt.IsNullable = isNullable;
            return dt;
        }
        // numerical
        public IDataType GetDataTypeNumerical(ITreeConfigNode? parent, uint length, bool isPositive, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Length = length;
            dt.IsPositive = isPositive;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeInt(ITreeConfigNode? parent, bool isPositive, bool isNullable)
        {
            return this.GetDataTypeFromMaxValue(parent, int.MaxValue, isPositive, isNullable);
        }
        // string
        public IDataType GetDataTypeString(ITreeConfigNode? parent, uint length, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.STRING;
            dt.Length = length;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeStringGuid(ITreeConfigNode? parent, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.STRING;
            dt.Length = 36;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeCatalog(ITreeConfigNode? parent, string catGuid, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.ObjectRef.ForeignObjectGuid = catGuid;
            dt.DataTypeEnum = EnumDataType.CATALOG;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeDocument(ITreeConfigNode? parent, string docGuid, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.ObjectRef.ForeignObjectGuid = docGuid;
            dt.DataTypeEnum = EnumDataType.DOCUMENT;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeTimeline(ITreeConfigNode? parent, string timelineTableGuid, bool isNullable, bool isPKey)
        {
            DataType dt = new DataType(parent);
            dt.ObjectRef.ForeignObjectGuid = timelineTableGuid;
            dt.DataTypeEnum = EnumDataType.REF_TIMELINE;
            dt.IsNullable = isNullable;
            dt.IsPKey = isPKey;
            return dt;
        }
        public IDataType GetDataTypeAny(ITreeConfigNode? parent, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.ANY;
            dt.IsNullable = isNullable;
            return dt;
        }
        // catalog
        public IDataType GetDataType(ITreeConfigNode? parent, ICatalog obj, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.ObjectRef.ForeignObjectGuid = obj.Guid;
            dt.IsNullable = isNullable;
            dt.DataTypeEnum = EnumDataType.CATALOG;
            return dt;
        }
        // document
        public IDataType GetDataType(ITreeConfigNode? parent, IDocument obj, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.ObjectRef.ForeignObjectGuid = obj.Guid;
            dt.IsNullable = isNullable;
            dt.DataTypeEnum = EnumDataType.DOCUMENT;
            return dt;
        }
        public IDataType GetDataTypeBool(ITreeConfigNode? parent, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.BOOL;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeDate(ITreeConfigNode? parent, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DATE;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeDateTimeUtc(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable, bool isPKey = false)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DATETIMEUTC;
            dt.IsNullable = isNullable;
            dt.IsPKey = isPKey;
            dt.AccuracyForTime = accuracyForTime;
            return dt;
        }
        public IDataType GetDataTypeDateTimeLocal(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DATETIMELOCAL;
            dt.IsNullable = isNullable;
            dt.AccuracyForTime = accuracyForTime;
            return dt;
        }
        //public IDataType GetDataTypeDateTime(bool isNullable = true)
        //{
        //    DataType dt = new DataType();
        //    dt.DataTypeEnum = EnumDataType.DATETIME;
        //    dt.IsNullable = isNullable;
        //    return dt;
        //}
        public IDataType GetDataTypeDateTimeZ(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DATETIMEZ;
            dt.IsNullable = isNullable;
            dt.AccuracyForTime = accuracyForTime;
            return dt;
        }
        public IDataType GetDataTypeTime(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.TIME;
            dt.IsNullable = isNullable;
            dt.AccuracyForTime = accuracyForTime;
            return dt;
        }
        public IDataType GetDataTypeDateTimeOffset(ITreeConfigNode? parent, EnumTimeAccuracyType accuracyForTime, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.DataTypeEnum = EnumDataType.DATETIMEOFFSET;
            dt.IsNullable = isNullable;
            dt.AccuracyForTime = accuracyForTime;
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
        private IProperty? GetPropertyId(IGroupListProperties parent, IvPluginDbGenerator dbGen)
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
            if (string.IsNullOrWhiteSpace(this.PropertyIdGuid))
                this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            var res = new Property(parent, this.PropertyIdGuid, fieldName, true);
            res.DataType = (DataType)this.GetIdDataType(res, false);
            res.DataType.IsPKey = true;
            return res;
        }
        public IDataType GetIdDataType(ITreeConfigNode? parent, bool isNullable)
        {
            IDataType dt;
            switch (this.PKeyType)
            {
                case EnumPrimaryKeyType.INT:
                    dt = this.GetDataTypeFromMaxValue(parent, int.MaxValue, false, isNullable);
                    break;
                case EnumPrimaryKeyType.LONG:
                    dt = this.GetDataTypeFromMaxValue(parent, long.MaxValue, false, isNullable);
                    break;
                default:
                    throw new ArgumentException();
            }
            return dt;
        }
        public IDataType GetIdRefDataType(ITreeConfigNode? parent, bool isNullable)
        {
            switch (this.PKeyType)
            {
                case EnumPrimaryKeyType.INT:
                    return this.GetDataTypeFromMaxValue(parent, int.MaxValue, false, isNullable);
                case EnumPrimaryKeyType.LONG:
                    return this.GetDataTypeFromMaxValue(parent, long.MaxValue, false, isNullable);
                default:
                    throw new ArgumentException();
            }
        }
        public IProperty GetPropertyGuid(ITreeConfigNode parent, string guid, string name, bool isNullable)
        {
            var res = new Property(parent, guid, name, false);
            res.DataType = (DataType)this.GetDataTypeStringGuid(res, isNullable);
            res.IsCsNullable = true;
            return res;
        }
        public IProperty GetPropertyDateTimeUtc(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable, EnumTimeAccuracyType enumTimeAccuracyType = EnumTimeAccuracyType.MKS_TIME_ACC)
        {
            var res = new Property(parent, guid, name, false);
            res.DataType = (DataType)this.GetDataTypeDateTimeUtc(res, enumTimeAccuracyType, isNullable);
            res.Position = position;
            res.IsCsNullable = true;
            return res;
        }
        public IProperty GetPropertyPkId(ITreeConfigNode parent, string idGuid)
        {
            var res = new Property(parent, idGuid, this.PKeyName, true);
            res.DataType = (DataType)this.GetIdDataType(res, false);
            res.DataType.IsPKey = true;
            res.IsHidden = true;
            res.Position = 6;
            return res;
        }
        public IProperty GetPropertyId(ITreeConfigNode parent, string guid, string name, bool isNullable)
        {
            var res = new Property(parent, guid, name + this.PKeyName, true);
            res.DataType = (DataType)this.GetIdDataType(res, isNullable);
            res.IsHidden = true;
            return res;
        }
        public IProperty GetPropertyBool(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, name, false);
            res.DataType = (DataType)this.GetDataTypeBool(res, isNullable);
            res.IsHidden = false;
            res.Position = position;
            return res;
        }
        public IProperty GetPropertyNumber(ITreeConfigNode parent, string guid, string name, uint length, uint accuracy, bool isNullable)
        {
            var res = new Property(parent, guid, name, false);
            res.DataType = (DataType)this.GetDataTypeNumerical(res, length, accuracy, isNullable);
            res.IsHidden = false;
            return res;
        }
        public IProperty GetPropertyInt(ITreeConfigNode parent, string guid, string name, bool isPositive, bool isNullable)
        {
            var res = new Property(parent, guid, name, false);
            res.DataType = (DataType)this.GetDataTypeInt(res, isPositive, isNullable);
            res.IsHidden = false;
            return res;
        }
        public IProperty GetPropertyVersion(ITreeConfigNode parent, string guid)
        {
            var res = new Property(parent, guid, this.RecordVersionFieldName, true);
            res.DataType = (DataType)this.GetDataTypeFromMaxValue(res, int.MaxValue, false, false);
            res.IsRecordVersion = true;
            res.IsHidden = true;
            res.IsNullable = false;
            res.Position = 7;
            return res;
        }
        public IProperty GetPropertyRefDimension(IRegister parent, string guid, string name, uint position, bool isNullable = false)
        {
            var res = new Property(parent.GroupProperties, guid, name, true);
            res.DataType = (DataType)this.GetIdRefDataType(res, isNullable);
            res.IsHidden = true;
            res.Position = position;
            //res.Position = 10 + relPosition;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRefCatalog(ITreeConfigNode parent, string guid, ICatalog c, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, "Ref" + c.CompositeName, true);
            res.Position = position;
            res.IsCsNullable = true;
            res.DataType = (DataType)this.GetDataType(parent, c, isNullable);
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyCatalogCode(IGroupListProperties parent, string guid, uint length, bool isNullable)
        {
            var res = new Property(parent, guid, this.GroupCatalogs.PropertyCodeName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length, isNullable);
            res.Position = 9;
            res.IsCsNullable = true;
            return res;
        }
        public IProperty GetPropertyCatalogCodeInt(IGroupListProperties parent, string guid, uint length, bool isNullable)
        {
            var res = new Property(parent, guid, this.GroupCatalogs.PropertyCodeName, true);
            res.DataType = (DataType)this.GetDataTypeNumerical(res, length, true, isNullable);
            res.Position = 9;
            res.IsCsNullable = true;
            return res;
        }
        public IProperty GetPropertyCatalogName(IGroupListProperties parent, string guid, uint length, bool isNullable)
        {
            var res = new Property(parent, guid, this.GroupCatalogs.PropertyNameName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length, isNullable);
            res.Position = 10;
            return res;
        }
        public IProperty GetPropertyCatalogDescription(IGroupListProperties parent, string guid, uint length, bool isNullable)
        {
            var res = new Property(parent, guid, this.GroupCatalogs.PropertyDescriptionName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length, isNullable);
            res.Position = 11;
            return res;
        }
        public IProperty GetPropertyIsFolder(IGroupListProperties parent, string guid, bool isNullable)
        {
            var res = new Property(parent, guid, this.GroupCatalogs.PropertyIsFolderName, true);
            res.DataType = new DataType(res) { DataTypeEnum = EnumDataType.BOOL };
            res.IsHidden = true;
            res.IsNullable = isNullable;
            res.Position = 12;
            return res;
        }
        public IProperty GetPropertyRefDocument(IGroupListProperties parent, string guid, IDocument d, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, "Ref" + d.CompositeName, true);
            res.Position = position;
            res.IsCsNullable = true;
            res.DataType = (DataType)this.GetDataType(parent, d, isNullable);
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyDocumentDate(IGroupListProperties parent, string guid, bool isPKey = false)
        {
            var res = new Property(parent, guid, this.GroupDocuments.DocumentDocDateTimePropertyName, true);
            res.DataType = (DataType)this.GetDataTypeDateTimeUtc(res, EnumTimeAccuracyType.MAX_TIME_ACC, false, isPKey);
            res.Position = 8;
            res.IsCsNullable = true;
            return res;
        }
        public IProperty GetPropertyDocNumberString(IGroupListProperties parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.GroupDocuments.PropertyDocNumberName, true);
            res.DataType = (DataType)this.GetDataTypeString(res, length, false);
            res.Position = 9;
            res.IsCsNullable = true;
            return res;
        }
        public IProperty GetPropertyDocNumberInt(IGroupListProperties parent, string guid, uint length)
        {
            var res = new Property(parent, guid, this.GroupDocuments.PropertyDocNumberName, true);
            res.DataType = (DataType)this.GetDataTypeFromMaxValue(res, int.MaxValue, true, false);
            res.DataType.IsNullable = false;
            res.Position = 9;
            res.IsCsNullable = true;
            return res;
        }
        public IProperty GetPropertyRef(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable = false, bool is_pkey = false)
        {
            var res = new Property(parent, guid, name, true);
            res.DataType = (DataType)this.GetIdRefDataType(res, isNullable);
            res.DataType.IsRefParent = true;
            res.IsHidden = true;
            res.Position = position;
            res.DataType.IsPKey = is_pkey;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(IDetail fromObject, IDetail toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_DETAIL_TO_PARENT_DETAIL;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(IDetail fromObject, ICatalog toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_DETAIL_TO_PARENT_CATALOG;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(IDetail fromObject, ICatalogFolder toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(IDetail fromObject, IDocument toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_DETAIL_TO_PARENT_DOCUMENT;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(ICatalog fromObject, ICatalog toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_TO_SELF_TREE_CATALOG_PARENT;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(ICatalog fromObject, ICatalogFolder toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(ICatalogFolder fromObject, ICatalogFolder toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyRef(IRegister fromObject, IDocumentTimeline toObject, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(fromObject.GroupProperties, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = new DataType(fromObject);
            res.DataType.ObjectRef.ForeignObjectGuid = toObject.Guid;
            res.DataType.DataTypeEnum = EnumDataType.REF_TIMELINE;
            res.DataType.IsNullable = isNullable;
            res.IsComplex = true;
            return res;
        }

        public IProperty GetPropertyCatalog(ITreeConfigNode parent, string guid, string name, string catGuid, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = (DataType)this.GetDataTypeCatalog(parent, catGuid, isNullable);
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyDocument(ITreeConfigNode parent, string guid, string name, string docGuid, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = (DataType)this.GetDataTypeDocument(parent, docGuid, isNullable);
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyTimeline(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable, bool isPKey)
        {
            var res = new Property(parent, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = (DataType)this.GetDataTypeTimeline(parent, this.GroupDocuments.DocumentTimeline.Guid, isNullable, isPKey);
            res.IsComplex = true;
            return res;
        }
        public IDataType GetDataTypeCatalogs(ITreeConfigNode? parent, IEnumerable<ComplexRef> lstCatGuids, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.ListObjectRefs.AddRange(lstCatGuids);
            dt.DataTypeEnum = EnumDataType.CATALOGS;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeDocuments(ITreeConfigNode? parent, IEnumerable<ComplexRef> lstDocGuids, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.ListObjectRefs.AddRange(lstDocGuids);
            dt.DataTypeEnum = EnumDataType.DOCUMENTS;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IDataType GetDataTypeCatalogsDocuments(ITreeConfigNode? parent, IEnumerable<ComplexRef> lstCatDocGuids, bool isNullable)
        {
            DataType dt = new DataType(parent);
            dt.ListObjectRefs.AddRange(lstCatDocGuids);
            dt.DataTypeEnum = EnumDataType.ANY;
            dt.IsNullable = isNullable;
            return dt;
        }
        public IProperty GetPropertyCatalogs(IGroupListProperties parent, string guid, string name, IEnumerable<ComplexRef> lstCatGuids, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = (DataType)this.GetDataTypeCatalogs(parent, lstCatGuids, isNullable);
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyDocuments(ITreeConfigNode parent, string guid, string name, IEnumerable<ComplexRef> lstDocGuids, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = (DataType)this.GetDataTypeDocuments(parent, lstDocGuids, isNullable);
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyCatalogsDocuments(IGroupListProperties parent, string guid, string name, IEnumerable<ComplexRef> lstCatOrDocGuids, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = (DataType)this.GetDataTypeCatalogsDocuments(parent, lstCatOrDocGuids, isNullable);
            res.IsComplex = true;
            return res;
        }
        public IProperty GetPropertyAny(ITreeConfigNode parent, string guid, string name, uint position, bool isNullable)
        {
            var res = new Property(parent, guid, name, true);
            res.Position = position;
            res.IsCsNullable = isNullable;
            res.DataType = (DataType)this.GetDataTypeAny(parent, isNullable);
            res.IsComplex = true;
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
        public IReadOnlyList<IRegister> GetListRegisters(string guidAppPrjGen)
        {
            var lst = new List<IRegister>();
            var cfg = this.ParentConfig;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in cfg.Model.GroupDocuments.GroupRegisters.ListRegisters)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        public IReadOnlyList<IJournal> GetListJournals(string guidAppPrjGen)
        {
            var lst = new List<IJournal>();
            var cfg = this.ParentConfig;
            var g = cfg.DicActiveAppProjectGenerators[guidAppPrjGen];
            foreach (var tt in cfg.Model.GroupDocuments.GroupJournals.ListJournals)
            {
                if (tt.IsIncluded(guidAppPrjGen))
                {
                    lst.Add(tt);
                }
            }
            return lst;
        }
        public string GetUniquePropertyShortID(IProperty p)
        {
            //use numerical short id
            return $"p{p.ParentGroupListPropertiesI.IndexOf(p).ToString()}";
        }
        public string GetUniquePropertyFullShortID(IProperty p)
        {
            //use numerical short id
            Debug.Assert(p.ParentGroupListPropertiesI.Parent != null);
            var parenId = GetUniqueStringShortID(p.ParentGroupListPropertiesI.Parent);
            return $"{parenId}p{p.ParentGroupListPropertiesI.IndexOf(p).ToString()}";
        }
        public string GetUniqueStringShortID(ITreeConfigNode node)
        {
            if (node is IConstant cn)
            {
                return $"ct{cn.ShortId.ToString()}";
            }
            if (node is ICatalog c)
            {
                return $"c{c.ShortId.ToString()}";
            }
            else if (node is ICatalogFolder cf)
            {
                return $"cf{cf.ParentCatalogI.ShortId.ToString()}";
            }
            else if (node is IDocument d)
            {
                return $"d{d.ShortId.ToString()}";
            }
            else if (node is IDetail t)
            {
                return GetTypeCacheIdWithParents(t, $"t{t.ShortId.ToString()}");
            }
            else if (node is IRegister r)
            {
                return $"r{r.ShortId.ToString()}";
            }
            else if (node is IRelationManyToMany rlc)
            {
                return $"rc{rlc.ShortId.ToString()}";
            }
            else if (node is IRelationOneToOne rld)
            {
                return $"rd{rld.ShortId.ToString()}";
            }
            else if (node is IGroupListConstants cts)
            {
                return $"cg{cts.ShortId.ToString()}";
            }
            else if (node is IDocumentTimeline tl)
            {
                return "tl"; // only one timeline
            }
            else if (node is IGroupDocuments gd)
            {
                return $"gr_docs"; // only one
            }
            ThrowHelper.ThrowInvalidOperationException();
            return "";
            string GetTypeCacheIdWithParents(IDetail node, string res)
            {
                if (node is IModel)
                    return res;
                Debug.Assert(node.Parent != null);
                if (node.Parent is IDetail t)
                {
                    return GetTypeCacheIdWithParents(t, $"t{t.ShortId.ToString()}{res}");
                }
                else if (node.Parent is ICatalog c)
                {
                    var gc = c.ParentGroupListCatalogsI;
                    return $"c{c.ShortId.ToString()}{res}";
                }
                else if (node.Parent is IDocument d)
                {
                    var gd = d.ParentGroupListDocumentsI;
                    return $"d{d.ShortId.ToString()}{res}";
                }
                return res;
            }
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
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
        public void SavePluginGroupsModels()
        {
            this.ListPluginGroupsModelExtensions.Clear();
            var hash = new HashSet<string>();
            foreach (var t in this.ParentConfig.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (ttt.Plugin != null)
                        {
                            if (hash.Contains(ttt.Plugin.PluginGroupGuid))
                                continue;
                            hash.Add(ttt.Plugin.PluginGroupGuid);
                            var settings = ttt.Plugin.PluginGroupModelToJson();
                            var p = new PluginGroupModelExtensions(this)
                            {
                                Guid = ttt.Plugin.PluginGroupGuid,
                                Settings = settings,
                            };
                            this.ListPluginGroupsModelExtensions.Add(p);
                        }
                    }
                }
            }
        }
        public void RestorePluginGroupsModels()
        {
            var dic = new Dictionary<string, PluginGroupModelExtensions>();
            foreach (var t in this.ListPluginGroupsModelExtensions)
            {
                dic[t.Guid] = t;
            }
            foreach (var t in this.ParentConfig.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (ttt.Plugin != null)
                        {
                            if (dic.TryGetValue(ttt.Plugin.PluginGroupGuid, out var p))
                            {
                                ttt.Plugin.PluginGroupModelFromJson(p.Settings);
                                dic.Remove(ttt.Plugin.PluginGroupGuid);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Get reference types for EnumDataType.CATALOGS or EnumDataType.DOCUMENTS
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Dictionary of type GUID and Tuple of type name and composite name</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public Dictionary<string, Tuple<string, string>> GetRefTypeNames(IDataType dt)
        {
            var res = new Dictionary<string, Tuple<string, string>>();
            switch (dt.DataTypeEnum)
            {
                case EnumDataType.ENUMERATION:
                case EnumDataType.CATALOG:
                case EnumDataType.DOCUMENT:
                case EnumDataType.BOOL:
                case EnumDataType.DATE:
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                //case EnumDataType.DATETIME:
                case EnumDataType.DATETIMEZ:
                case EnumDataType.DATETIMEOFFSET:
                case EnumDataType.NUMERICAL:
                case EnumDataType.STRING:
                case EnumDataType.TIME:
                    throw new ArgumentException("Unexpected EnumDataType type");
                case EnumDataType.CATALOGS:
                    if (dt.ListObjectRefs.Count > 0)
                    {
                        foreach (var t in dt.ListObjectRefs)
                        {
                            Debug.Assert(!string.IsNullOrWhiteSpace(t.ForeignObjectGuid));
                            Debug.Assert(this.ParentConfig.DicNodes.ContainsKey(t.ForeignObjectGuid));
                            var node = this.ParentConfig.DicNodes[t.ForeignObjectGuid];
                            if (node is ICatalog c)
                            {
                                Debug.Assert(!res.ContainsKey(c.Guid));
                                res[c.Guid] = new Tuple<string, string>($"Catalogs.{c.Name}", c.CompositeName);
                            }
                            else if (node is IDocument d)
                            {
                                throw new ArgumentException("EnumDataType.CATALOGS can't reference Document");
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                    }
                    else
                    {
                        foreach (var t in Cfg.Model.GroupCatalogs.ListCatalogs)
                        {
                            Debug.Assert(!res.ContainsKey(t.Guid));
                            res[t.Guid] = new Tuple<string, string>($"Catalogs.{t.Name}", t.CompositeName);
                        }
                    }
                    break;
                case EnumDataType.DOCUMENTS:
                    if (dt.ListObjectRefs.Count > 0)
                    {
                        foreach (var t in dt.ListObjectRefs)
                        {
                            Debug.Assert(!string.IsNullOrWhiteSpace(t.ForeignObjectGuid));
                            Debug.Assert(this.ParentConfig.DicNodes.ContainsKey(t.ForeignObjectGuid));
                            var node = this.ParentConfig.DicNodes[t.ForeignObjectGuid];
                            if (node is ICatalog c)
                            {
                                throw new ArgumentException("EnumDataType.DOCUMENTS can't reference Catalog");
                            }
                            else if (node is IDocument d)
                            {
                                Debug.Assert(!res.ContainsKey(d.Guid));
                                res[d.Guid] = new Tuple<string, string>($"Documents.{d.Name}", d.CompositeName);
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                    }
                    else
                    {
                        foreach (var t in Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                        {
                            Debug.Assert(!res.ContainsKey(t.Guid));
                            res[t.Guid] = new Tuple<string, string>($"Documents.{t.Name}", t.CompositeName);
                        }
                    }
                    break;
                case EnumDataType.ANY:
                    if (dt.ListObjectRefs.Count > 0)
                    {
                        foreach (var t in dt.ListObjectRefs)
                        {
                            Debug.Assert(!string.IsNullOrWhiteSpace(t.ForeignObjectGuid));
                            Debug.Assert(this.ParentConfig.DicNodes.ContainsKey(t.ForeignObjectGuid));
                            var node = this.ParentConfig.DicNodes[t.ForeignObjectGuid];
                            if (node is ICatalog c)
                            {
                                Debug.Assert(!res.ContainsKey(c.Guid));
                                res[c.Guid] = new Tuple<string, string>($"Catalogs.{c.Name}", c.CompositeName);
                            }
                            else if (node is IDocument d)
                            {
                                Debug.Assert(!res.ContainsKey(d.Guid));
                                res[d.Guid] = new Tuple<string, string>($"Documents.{d.Name}", d.CompositeName);
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                    }
                    else
                    {
                        foreach (var t in Cfg.Model.GroupCatalogs.ListCatalogs)
                        {
                            Debug.Assert(!res.ContainsKey(t.Guid));
                            res[t.Guid] = new Tuple<string, string>($"Catalogs.{t.Name}", t.CompositeName);
                        }
                        foreach (var t in Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                        {
                            Debug.Assert(!res.ContainsKey(t.Guid));
                            res[t.Guid] = new Tuple<string, string>($"Documents.{t.Name}", t.CompositeName);
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            return res;
        }
        public string GetRefTypeNamesString(IDataType dt)
        {
            var sb = new StringBuilder();
            var sep = string.Empty;
            foreach (var t in this.GetRefTypeNames(dt))
            {
                sb.Append(sep);
                sb.Append(t.Value.Item1);
                sep = ", ";
            }
            return sb.ToString();
        }
        public uint LastTypeShortIdForNode()
        {
            uint res = ++this.LastTypeShortRefId;
            return res;
        }
        public EnumRefType RefTypeForNode(ITreeConfigNode n)
        {
            if (n is Catalog)
            {
                return EnumRefType.REF_TYPE_CATALOG;
            }
            else if (n is CatalogFolder)
            {
                return EnumRefType.REF_TYPE_CATALOG_FOLDER;
            }
            else if (n is Document)
            {
                return EnumRefType.REF_TYPE_DOCUMENT;
            }
            else if (n is Detail)
            {
                Debug.Assert(n.Parent != null);
                var p = n.Parent.Parent;
                while (p is Detail)
                {
                    Debug.Assert(p.Parent != null);
                    p = p.Parent.Parent;
                }
                if (p is Catalog)
                {
                    return EnumRefType.REF_TYPE_CATALOG_DETAIL;
                }
                else if (p is Document)
                {
                    return EnumRefType.REF_TYPE_DOCUMENT_DETAIL;
                }
                else if (p is CatalogFolder)
                {
                    return EnumRefType.REF_TYPE_CATALOG_FOLDER_DETAIL;
                }
                else
                    ThrowHelper.ThrowNotSupportedException();
            }
            else if (n is Constant)
            {
                return EnumRefType.REF_TYPE_CONSTANT;
            }
            else if (n is RelationManyToMany)
            {
                return EnumRefType.REF_TYPE_MANY_TO_MANY_CATALOGS;
            }
            else if (n is RelationOneToOne)
            {
                return EnumRefType.REF_TYPE_MANY_TO_MANY_DOCUMENTS;
            }
            else if (n is GroupListConstants)
            {
                return EnumRefType.REF_TYPE_CONSTANT_GROUP;
            }
            return EnumRefType.REF_TYPE_NOT_SELECTED;
        }
        public uint LastTypeShortRefIdForNode(ITreeConfigNode n, uint shortId)
        {
            uint res = shortId;
            const int nbits = 26; // bits for short ID
            uint imax = (0x1 << (nbits + 1)) - 1;
            uint id = 0; // 32 - nbits = 6 bits, up to 32 different reference types
            if (res > imax)
            {
                ThrowHelper.ThrowNotSupportedException($"Amount of types with short reference ID exceeded {imax}.");
            }
            switch (this.RefTypeForNode(n))
            {
                case EnumRefType.REF_TYPE_CONSTANT:
                    id = 1u;
                    break;
                case EnumRefType.REF_TYPE_CATALOG:
                    id = 2u;
                    break;
                case EnumRefType.REF_TYPE_CATALOG_DETAIL:
                    id = 3u;
                    break;
                case EnumRefType.REF_TYPE_MANY_TO_MANY_CATALOGS:
                    id = 4u;
                    break;
                case EnumRefType.REF_TYPE_CATALOG_FOLDER:
                    id = 5u;
                    break;
                case EnumRefType.REF_TYPE_CATALOG_FOLDER_DETAIL:
                    id = 6u;
                    break;
                case EnumRefType.REF_TYPE_DOCUMENT:
                    id = 7u;
                    break;
                case EnumRefType.REF_TYPE_DOCUMENT_DETAIL:
                    id = 8u;
                    break;
                case EnumRefType.REF_TYPE_MANY_TO_MANY_DOCUMENTS:
                    id = 9u;
                    break;
                default:
                    ThrowHelper.ThrowNotSupportedException();
                    break;
            }
            Debug.Assert(id < (0x1 << (32 - nbits)));
            res = res + (id << nbits);
            return res;
        }
        public IForm CreateForm(IGroupListForms groupForms, FormType formType, List<IProperty> lst)
        {
            return new Form(groupForms, formType, lst);
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
