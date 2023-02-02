
// Repository NameSpace.tt

// EnumVmType.Dapper
#nullable enable
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

/// <summary>
/// Auto generated
/// </summary>
#pragma warning disable IDE1006 // Naming Styles
namespace vPlugins.DapperModels // NameSpace.tt Line: 37
{
	//using CommonCodeAPI;
	using System;
	using System.Text;
	using System.Linq;
	using System.Data;
    using System.Transactions;
    using System.IO;
	using Dapper;
    using Dapper.Contrib.Extensions;
	using Polly;
    using Microsoft.Extensions.Configuration;

    using System.Diagnostics;

    namespace Sqlite // NameSpace.tt Line: 92
    {
        //using DTO = vPlugins.Models.Sqlite.Model; // NameSpace.tt Line: 95
        //using DTO = vPlugins.DapperModels.Sqlite.Model; // NameSpace.tt Line: 96
        //using CMD = vPlugins.DapperModels; // NameSpace.tt Line: 97
        using Microsoft.Data.Sqlite;
        /// <include file='model_doc.xml' path='Doc/Model/Desc[@name="model"]/*' />
        public partial class Model // NameSpace.tt Line: 110
        {
            public static bool LOGGING_EXTENTIONS { get; } = false; // NameSpace.tt Line: 145
            public static bool LOGGING_NLOG { get; } = false;
            public static bool LOGGING_SERILOG { get; } = false;
			/// <include file='model_doc.xml' path='Doc/Model/Constants/Desc[@name="constants"]/*' />
			public partial class Constants // Constants.tt Line: 14, called from NameSpace.tt Line: 164
			{
			    /// <include file='model_doc.xml' path='Doc/Model/Constants/Desc[@name="LoadValues"]/*' />
			    public static void LoadValues(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 19*/) // Constants.tt Line: 19
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        Group1.LoadValues(utc);
			    }
			    /// <include file='model_doc.xml' path='Doc/Model/Constants/Desc[@name="Reset"]/*' />
			    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 31*/)
			    {
			        Group1.Reset();
			    }
				public class Group1 // Constants.tt Line: 44
				{
				    #region Methods // Constants.tt Line: 48
				    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Desc[@name="LoadValues"]/*' />
				    public static void LoadValues(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 50*/) // Constants.tt Line: 50
				    {
						System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
				        if (utc.Kind == DateTimeKind.Local)
				            utc = utc.ToUniversalTime();
				        CONSTANT1.LoadValue(utc);
				        CONSTANT2.LoadValue(utc);
				    }
				    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Desc[@name="CleanOlder"]/*' />
				    public static void CleanOlder(DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 62*/) // Constants.tt Line: 62
				    {
						System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
				        if (utcTo.Kind == DateTimeKind.Local)
				            utcTo = utcTo.ToUniversalTime();
				        CONSTANT1.CleanOlder(utcTo);
				        CONSTANT2.CleanOlder(utcTo);
				    }
				    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Desc[@name="Reset"]/*' />
				    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 74*/) // Constants.tt Line: 74
				    {
				        CONSTANT1.Reset();
				        CONSTANT2.Reset();
				    }
				    #endregion Methods // Constants.tt Line: 82
					// PocoConstants.tt Line: 9, called from Constants.tt Line: 85
					public class CONSTANT1 // PocoConstants.tt Line: 16
					{
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="GUID"]/*' />
					    public const string GUID = "a59b9a9d-5627-43d1-92a8-a5c610d52d78";
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Value"]/*' />
					    public static string? Value { get; private set; } // PocoConstants.tt Line: 25
						internal static Constants.Group1.CONSTANT1.History_Constant1 Create() // PocoConstants.tt Line: 26
						{
							var res = new Constants.Group1.CONSTANT1.History_Constant1
					        {
							    id = GetNextId(_history.T_GUID)
					        };
							return res;
						}
					    private static int _constantId; // PocoConstants.tt Line: 34
					    private static int constantId { get { if (_constantId == 0) { _constantId = _history_objects_ids.GetObjectId(GUID); } return _constantId; }}
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="LoadValue"]/*' />
					    public static void LoadValue(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 52*/) // PocoConstants.tt Line: 52
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        Value = Load(utc); // PocoConstants.tt Line: 63
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Load"]/*' />
					    public static string? Load(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 70*/) // PocoConstants.tt Line: 70
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = null; // PocoConstants.tt Line: 79
					        var h = _history.Load(constantId, utc);
					        if (h != null)
					            res = h; // PocoConstants.tt Line: 96
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Save"]/*' />
					    public static void Save(string val, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 130*/) // PocoConstants.tt Line: 130
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = val; // PocoConstants.tt Line: 148
					        _history.Save(constantId, res, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectBefore"]/*' />
					    public static List<History_Constant1> SelectBefore(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 174*/) // PocoConstants.tt Line: 174
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant1.ConvertToListOfConstants(_history.SelectBefore(constantId, utc)); // PocoConstants.tt Line: 181
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectAfter"]/*' />
					    public static List<History_Constant1> SelectAfter(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 197*/) // PocoConstants.tt Line: 197
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant1.ConvertToListOfConstants(_history.SelectAfter(constantId, utc)); // PocoConstants.tt Line: 204
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select"]/*' />
					    public static List<History_Constant1> Select(DateTime utcFrom, DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 220*/) // PocoConstants.tt Line: 220
					    {
							System.Diagnostics.Debug.Assert(utcFrom.Kind != DateTimeKind.Unspecified);
							System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
					        if (utcFrom.Kind == DateTimeKind.Local)
					            utcFrom = utcFrom.ToUniversalTime();
					        if (utcTo.Kind == DateTimeKind.Local)
					            utcTo = utcTo.ToUniversalTime();
					        var res = History_Constant1.ConvertToListOfConstants(_history.Select(constantId, utcFrom, utcTo)); // PocoConstants.tt Line: 230
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select()"]/*' />
					    public static List<History_Constant1> Select([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 246*/) // PocoConstants.tt Line: 246
					    {
					        var res = History_Constant1.ConvertToListOfConstants(_history.Select(constantId)); // PocoConstants.tt Line: 250
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="CleanOlder"]/*' />
					    public static void CleanOlder(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 266*/) // PocoConstants.tt Line: 266
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        _history.CleanOlder(constantId, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Delete"]/*' />
					    public static void Delete(History_Constant1 rec, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 287*/) // PocoConstants.tt Line: 287
					    {
					        _history.Delete(rec.id);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Reset"]/*' />
					    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 305*/) // PocoConstants.tt Line: 305
					    {
					        _history.Reset(constantId);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="History_"]/*' />
					    public struct History_Constant1 // PocoConstants.tt Line: 328
					    {
					        public override string ToString() { return $"{this.Value} on date: {this.DateTime}"; }
					        internal History_Constant1(_history t)
					        {
					            this.id = t.Id;
					            this.DateTime = _history.ToDateTime(t.date_time);
					            if (t.val != null)
					            {
					                this.Value = t.val; // PocoConstants.tt Line: 348
					            } // PocoConstants.tt Line: 350
					            else
					                this.Value = null;
					        }
					        internal static List<History_Constant1> ConvertToListOfConstants(IEnumerable<_history> lst) // PocoConstants.tt Line: 354
					        {
					            var res = new List<History_Constant1>();
					            if (lst != null)
					                foreach (var t in lst)
					                    res.Add(new History_Constant1(t));
					            return res;
					        }
					        public int id;
					        public DateTime DateTime;
					        public string? Value;
					    }
					}
					public class CONSTANT2 // PocoConstants.tt Line: 16
					{
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="GUID"]/*' />
					    public const string GUID = "e1f0ae82-9268-4bc8-96b0-75492411e07e";
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Value"]/*' />
					    public static string? Value { get; private set; } // PocoConstants.tt Line: 25
						internal static Constants.Group1.CONSTANT2.History_Constant2 Create() // PocoConstants.tt Line: 26
						{
							var res = new Constants.Group1.CONSTANT2.History_Constant2
					        {
							    id = GetNextId(_history.T_GUID)
					        };
							return res;
						}
					    private static int _constantId; // PocoConstants.tt Line: 34
					    private static int constantId { get { if (_constantId == 0) { _constantId = _history_objects_ids.GetObjectId(GUID); } return _constantId; }}
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="LoadValue"]/*' />
					    public static void LoadValue(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 52*/) // PocoConstants.tt Line: 52
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        Value = Load(utc); // PocoConstants.tt Line: 63
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Load"]/*' />
					    public static string? Load(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 70*/) // PocoConstants.tt Line: 70
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = null; // PocoConstants.tt Line: 79
					        var h = _history.Load(constantId, utc);
					        if (h != null)
					            res = h; // PocoConstants.tt Line: 96
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Save"]/*' />
					    public static void Save(string val, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 130*/) // PocoConstants.tt Line: 130
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = val; // PocoConstants.tt Line: 148
					        _history.Save(constantId, res, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectBefore"]/*' />
					    public static List<History_Constant2> SelectBefore(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 174*/) // PocoConstants.tt Line: 174
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant2.ConvertToListOfConstants(_history.SelectBefore(constantId, utc)); // PocoConstants.tt Line: 181
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectAfter"]/*' />
					    public static List<History_Constant2> SelectAfter(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 197*/) // PocoConstants.tt Line: 197
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant2.ConvertToListOfConstants(_history.SelectAfter(constantId, utc)); // PocoConstants.tt Line: 204
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select"]/*' />
					    public static List<History_Constant2> Select(DateTime utcFrom, DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 220*/) // PocoConstants.tt Line: 220
					    {
							System.Diagnostics.Debug.Assert(utcFrom.Kind != DateTimeKind.Unspecified);
							System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
					        if (utcFrom.Kind == DateTimeKind.Local)
					            utcFrom = utcFrom.ToUniversalTime();
					        if (utcTo.Kind == DateTimeKind.Local)
					            utcTo = utcTo.ToUniversalTime();
					        var res = History_Constant2.ConvertToListOfConstants(_history.Select(constantId, utcFrom, utcTo)); // PocoConstants.tt Line: 230
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select()"]/*' />
					    public static List<History_Constant2> Select([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 246*/) // PocoConstants.tt Line: 246
					    {
					        var res = History_Constant2.ConvertToListOfConstants(_history.Select(constantId)); // PocoConstants.tt Line: 250
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="CleanOlder"]/*' />
					    public static void CleanOlder(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 266*/) // PocoConstants.tt Line: 266
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        _history.CleanOlder(constantId, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Delete"]/*' />
					    public static void Delete(History_Constant2 rec, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 287*/) // PocoConstants.tt Line: 287
					    {
					        _history.Delete(rec.id);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Reset"]/*' />
					    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 305*/) // PocoConstants.tt Line: 305
					    {
					        _history.Reset(constantId);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="History_"]/*' />
					    public struct History_Constant2 // PocoConstants.tt Line: 328
					    {
					        public override string ToString() { return $"{this.Value} on date: {this.DateTime}"; }
					        internal History_Constant2(_history t)
					        {
					            this.id = t.Id;
					            this.DateTime = _history.ToDateTime(t.date_time);
					            if (t.val != null)
					            {
					                this.Value = t.val; // PocoConstants.tt Line: 348
					            } // PocoConstants.tt Line: 350
					            else
					                this.Value = null;
					        }
					        internal static List<History_Constant2> ConvertToListOfConstants(IEnumerable<_history> lst) // PocoConstants.tt Line: 354
					        {
					            var res = new List<History_Constant2>();
					            if (lst != null)
					                foreach (var t in lst)
					                    res.Add(new History_Constant2(t));
					            return res;
					        }
					        public int id;
					        public DateTime DateTime;
					        public string? Value;
					    }
					}
				}
			}
			/// <include file='model_doc.xml' path='Doc/Model/Catalogs/Desc[@name="catalogs"]/*' />
			public partial class Catalogs // Catalogs.tt Line: 8, called from NameSpace.tt Line: 169
			{
				// PocoCatalogs.tt Line: 7, called from Catalogs.tt Line: 13
				[Dapper.Contrib.Extensions.Table("CtlgCatalog1")]
				public partial class Catalog1 : RepoEntityBaseSync<Catalog1>, IEntityBaseExplicit<Catalog1>, ISameById<Catalog1>, IEntityBase // ModelCatalogClass.tt Line: 12, called from PocoCatalogs.tt Line: 10
				{
				    #region ctor // ModelCtor.tt Line: 8, called from ModelCatalogClass.tt Line: 25
				    public IEnumerable<IEntityBase> GetChildren() // ModelCtor.tt Line: 17
				    {
				        return new List<IEntityBase>();
				    }
				    static Catalog1() 
				    { 
				    }
				#if DEBUG
				    private Catalog1() : base("c0")
				#else
				    public Catalog1() : base("c0")
				#endif
				    {
				    }
				    #endregion ctor
				    #region Properties // ModelProperty.tt Line: 8, called from ModelCatalogClass.tt Line: 28
					[Dapper.Contrib.Extensions.Key] // ModelProperty.tt Line: 19 - Utils.cs Line: 279
					public int Id // ModelProperty.tt Line: 19 - Utils.cs Line: 315 Utils.cs Line: 1174
					{
						get { return _Id; } // ModelProperty.tt Line: 19 - Utils.cs Line: 458
						set { _Id = value; ___isNeedUpdate = true;}
					}
					private int _Id; // ModelProperty.tt Line: 19 - Utils.cs Line: 466
					public int Code // ModelProperty.tt Line: 19 - Utils.cs Line: 315 Utils.cs Line: 1174
					{
						get { return _Code; } // ModelProperty.tt Line: 19 - Utils.cs Line: 458
						set { _Code = value; ___isNeedUpdate = true;}
					}
					private int _Code; // ModelProperty.tt Line: 19 - Utils.cs Line: 466
					public string Name // ModelProperty.tt Line: 19 - Utils.cs Line: 315 Utils.cs Line: 1154
					{
						get { return _Name; } // ModelProperty.tt Line: 19 - Utils.cs Line: 458
						set { _Name = value; ___isNeedUpdate = true;}
					}
					private string _Name = string.Empty; // ModelProperty.tt Line: 19 - Utils.cs Line: 466
				
					#region Fields // ModelProperty.tt Line: 21
					public const string F_ID = "Id";
					public const string F_CODE = "Code";
					public const string F_NAME = "Name";
					#endregion Fields // ModelProperty.tt Line: 28
				    #endregion Properties // ModelProperty.tt Line: 29
				    #region Special // ModelProperty.tt Line: 30
					public const string T_GUID = "5cc54978-0abe-4eef-a97a-f48713978731";
					public string GetGuid() { return T_GUID; }
					public const string T_NAME = "CtlgCatalog1";
				    public string GetDbTableName() { return T_NAME; }
				    public bool IsMarkedForDeletion(bool? isMarkedForDeletion = null) { if (isMarkedForDeletion.HasValue) { this.___isMarkedForDeletion = isMarkedForDeletion ?? false; } return this.___isMarkedForDeletion; }
				    private bool ___isMarkedForDeletion = false;
				    public bool SameById(Catalog1 other) { return other != null && this.Id == other.Id; } // ModelProperty.tt Line: 45
				    #endregion Special // ModelProperty.tt Line: 46
					#region Command Definition Data // ModelEntityCmd.tt Line: 9, called from ModelCatalogClass.tt Line: 53
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionInsert() // ModelEntityCmd.tt Line: 32
					{
					    var cmd = new CommandDefinitionData(
							"INSERT INTO CtlgCatalog1 (" + // ModelEntityCmd.tt Line: 48
								"Id"+
								",Code"+
								",Name"+
							") VALUES(" + // ModelEntityCmd.tt Line: 52
								"@Id"+
								",@Code"+
								",@Name"+
							");", // SELECT SCOPE_IDENTITY();", // ModelEntityCmd.tt Line: 56
					        new 
					        {
					    		this.Id, 
					    		this.Code, 
					    		this.Name, 
					        }, CommandType.Text) // ModelEntityCmd.tt Line: 66 
					        { Entity = this }; // ModelEntityCmd.tt Line: 68
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionLoadById(int id) // ModelEntityCmd.tt Line: 71
					{
					    var sql = @"SELECT Id, Code, Name FROM CtlgCatalog1 WHERE Id = @pid;"
					; // ModelEntityCmd.tt Line: 84
					    var cmd = new CommandDefinitionData(sql, new { pid = id }, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionSelect(string? where, object? param, string? sort, 
					    int page, int pagesize) // ModelEntityCmd.tt Line: 90
					{
						var sql = CreateQuery(null, Model.Catalogs.Catalog1.T_NAME, null, where, sort, page, pagesize);
					    var cmd = new CommandDefinitionData(sql, param, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionCountWhere(string? where, object? param) // ModelEntityCmd.tt Line: 104
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("SELECT Count(*) FROM CtlgCatalog1");
						if (where != null)
						{
							sb.Append(" WHERE ");
							sb.Append(where);
						}
						sb.Append(';');
					    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
					    return cmd;
					}
					List<CommandDefinitionData> IEntityBaseExplicit.GetCommandDefinitionSave() // ModelEntityCmd.tt Line: 125
					{
					    Dictionary<string, Dictionary<int, string?>>? dicInsertedUpdatedGuidId = null;
					    var lstCmd = new List<CommandDefinitionData>();
					    CommandDefinitionData cd;
					    bool isCanInsert = true; 
					    if (dicInsertedUpdatedGuidId != null)
					    {
					        if (!dicInsertedUpdatedGuidId.ContainsKey(Model.Catalogs.Catalog1.T_GUID))
					            dicInsertedUpdatedGuidId[Model.Catalogs.Catalog1.T_GUID] = new Dictionary<int, string?>();
					        var dic = dicInsertedUpdatedGuidId[Model.Catalogs.Catalog1.T_GUID];
					        if (dic.ContainsKey(this.Id))
					            isCanInsert = false;
					        else
					            dic[this.Id] = null;
					    }
					    if (this.IsNeedInsert() && isCanInsert)
					    {
					        cd = ((IEntityBaseExplicit)this).GetCommandDefinitionInsert();
					        lstCmd.Add(cd);
					    } 
					    else if (this.IsNeedUpdate())
					    {
					        cd = ((IEntityBaseExplicit)this).GetCommandDefinitionUpdate();
					        lstCmd.Add(cd);
					    }
					    return lstCmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionUpdate() // ModelEntityCmd.tt Line: 183
					{
					    var cmd = new CommandDefinitionData(
					        "UPDATE CtlgCatalog1 SET "+
								"Code = @Code" + 
								",Name = @Name" + 
					        " WHERE Id = @Id;", // ModelEntityCmd.tt Line: 212
							new {
					    		this.Code, 
					    		this.Name, 
								this.Id // ModelEntityCmd.tt Line: 227
							}, CommandType.Text) 
					        { Entity = this };
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionDeleteById(int id) // ModelEntityCmd.tt Line: 233
					{
					    var sql = "DELETE FROM CtlgCatalog1 WHERE Id = @pid;";
					    var cmd = new CommandDefinitionData(sql, new { pid = id }, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionRemoveById(int id) // ModelEntityCmd.tt Line: 244
					{
					    var sql = "" +
					    "DELETE FROM CtlgCatalog1 WHERE Id = @pid;\n"; // ModelEntityCmd.tt Line: 261
					    var cmd = new CommandDefinitionData(sql, new { pid = id }, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionDeleteWhere(string? where, object? param) // ModelEntityCmd.tt Line: 266
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("DELETE FROM CtlgCatalog1");
						if (!string.IsNullOrWhiteSpace(where))
						{
							sb.Append(" WHERE ");
							sb.Append(where);
						}
						sb.Append(';');
					    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionRemoveWhere(string? where, object? param) // ModelEntityCmd.tt Line: 287
					{
						var sb = new StringBuilder();
						if (!string.IsNullOrWhiteSpace(where))
						{
					        sb.Append("DELETE FROM CtlgCatalog1 WHERE "); // ModelEntityCmd.tt Line: 309
					        sb.Append(where);
					    	sb.AppendLine(";");
						}
					    else
					    {
					        sb.AppendLine("DELETE FROM CtlgCatalog1;"); // ModelEntityCmd.tt Line: 318
					    }
					    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionMoveTo(int id, int idGroupTo) { throw new NotImplementedException(); } // ModelEntityCmd.tt Line: 376
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionLoadSubTree(int id, int deep) { throw new NotImplementedException(); } // ModelEntityCmd.tt Line: 377
					#endregion Command Definition Data // ModelEntityCmd.tt Line: 381
					#region Repository // CatalogRepository.tt Line: 7, called from ModelCatalogClass.tt Line: 66
					// Repository.tt Line: 8, called from CatalogRepository.partial.cs Line: 25
					Catalogs.Catalog1 IEntityBaseExplicit<Catalog1>.CreateDto(int id) // Repository.tt Line: 15
					{
					    var dto = new Catalogs.Catalog1
					    {
					        Id = id,
					        Name = string.Empty,
					        ___isNeedInsert = true,
					    };
					    return dto;
					}
					protected override Catalogs.Catalog1 GetThis() { return this; }
					Catalog1? IEntityBaseExplicit<Catalog1>.LoadUtil(SqlMapper.GridReader multi) // Repository.tt Line: 236
					{
					    Catalogs.Catalog1? resCatalog1 = null;
					    var lstCtlgCatalog1 = new List<Catalogs.Catalog1>();
					    resCatalog1 = multi.Read<Catalogs.Catalog1>().First();
					    lstCtlgCatalog1.Add(resCatalog1);
					    return resCatalog1;
					}
					#endregion Repository // CatalogRepository.tt Line: 51
					#region Views // PocoView.tt Line: 9, called from ModelCatalogClass.tt Line: 82
					[Dapper.Contrib.Extensions.Table("CtlgCatalog1")]
					public partial class ViewListNarrow : ViewEntityBaseSync<ViewListNarrow>, IViewPlainBaseExplicit<ViewListNarrow>, IViewItem, ISameById<ViewListNarrow>  // PocoView.tt Line: 49
					{
					    public bool SameById(ViewListNarrow other) { return other != null && this.Id == other.Id; } // PocoView.tt Line: 51
					    public string GetName() { return this.Name; }
					#if DEBUG
					    private ViewListNarrow()
					#else
					    public ViewListNarrow()
					#endif
					    {
					        this.Name = string.Empty;
					    }
					    #region View Properties
						[Dapper.Contrib.Extensions.Key] // PocoView.tt Line: 103 - Utils.cs Line: 279
						public int Id // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Id; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Id = value; }
						}
						private int _Id; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public int Code // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Code; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Code = value; }
						}
						private int _Code; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public string Name // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1154
						{
							get { return _Name; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Name = value; }
						}
						private string _Name = string.Empty; // PocoView.tt Line: 103 - Utils.cs Line: 466
					    #endregion View Properties
						#region Views // CmdDefinitionsCatalogView.tt Line: 7, called from PocoView.tt Line: 119
						CommandDefinitionData IViewPlainBaseExplicit<ViewListNarrow>.GetCommandDefinitionCountWhere(string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 9
						{
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT Count(*) FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						CommandDefinitionData IViewPlainBaseExplicit<ViewListNarrow>.GetCommandDefinitionView(int pagesize, int page, string? sort, string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 302
						{
						    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0));
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT ");
						    sb.Append("Id, Code, Name FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							if (sort != null)
							{
								sb.Append(" ORDER BY ");
								sb.Append(sort);
							}
							if (page > 0 && pagesize > 0)
							{
								sb.Append(" LIMIT ");
								sb.Append(pagesize);
								sb.Append(" OFFSET ");
								sb.Append((page-1)*pagesize);
						    }
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						#endregion Views // CmdDefinitionsCatalogView.tt Line: 346
					}
					[Dapper.Contrib.Extensions.Table("CtlgCatalog1")]
					public partial class ViewListWide : ViewEntityBaseSync<ViewListWide>, IViewPlainBaseExplicit<ViewListWide>, IViewItem, ISameById<ViewListWide>  // PocoView.tt Line: 49
					{
					    public bool SameById(ViewListWide other) { return other != null && this.Id == other.Id; } // PocoView.tt Line: 51
					    public string GetName() { return this.Name; }
					#if DEBUG
					    private ViewListWide()
					#else
					    public ViewListWide()
					#endif
					    {
					        this.Name = string.Empty;
					    }
					    #region View Properties
						[Dapper.Contrib.Extensions.Key] // PocoView.tt Line: 103 - Utils.cs Line: 279
						public int Id // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Id; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Id = value; }
						}
						private int _Id; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public int Code // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Code; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Code = value; }
						}
						private int _Code; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public string Name // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1154
						{
							get { return _Name; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Name = value; }
						}
						private string _Name = string.Empty; // PocoView.tt Line: 103 - Utils.cs Line: 466
					    #endregion View Properties
						#region Views // CmdDefinitionsCatalogView.tt Line: 7, called from PocoView.tt Line: 119
						CommandDefinitionData IViewPlainBaseExplicit<ViewListWide>.GetCommandDefinitionCountWhere(string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 9
						{
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT Count(*) FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						CommandDefinitionData IViewPlainBaseExplicit<ViewListWide>.GetCommandDefinitionView(int pagesize, int page, string? sort, string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 302
						{
						    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0));
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT ");
						    sb.Append("Id, Code, Name FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							if (sort != null)
							{
								sb.Append(" ORDER BY ");
								sb.Append(sort);
							}
							if (page > 0 && pagesize > 0)
							{
								sb.Append(" LIMIT ");
								sb.Append(pagesize);
								sb.Append(" OFFSET ");
								sb.Append((page-1)*pagesize);
						    }
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						#endregion Views // CmdDefinitionsCatalogView.tt Line: 346
					}
					#endregion Views // PocoView.tt Line: 289
				}
			}
			public partial class Documents // Documents.tt Line: 7, called from NameSpace.tt Line: 173
			{
				// PocoDocuments.tt Line: 7, called from Documents.tt Line: 12
			}
			public interface IAttachForUpdates // BaseClasses.tt Line: 8, called from ModelGlobal.tt Line: 9
			{
			    void AttachForUpdates(TransactionOnCommit tx);
			}
			public partial class RepoBase // BaseClasses.tt Line: 12, called from ModelGlobal.tt Line: 9
			{
			    public bool IsNeedInsert(bool? isNeedInsert = null) { if (isNeedInsert.HasValue) { this.___isNeedInsert = isNeedInsert ?? false; } return this.___isNeedInsert; }
			    protected bool ___isNeedInsert = false;
			    public bool IsNeedUpdate(bool? isNeedUpdate = null) { if (isNeedUpdate.HasValue) { this.___isNeedUpdate = isNeedUpdate ?? false; } return this.___isNeedUpdate; }
			    protected bool ___isNeedUpdate = false;
			    public bool IsRemoved(bool? isRemove = null) { if (isRemove.HasValue) { this.___isRemoved = isRemove ?? false; } return this.___isRemoved; }
			    protected bool ___isRemoved = false;
			}
			public partial class RepoBaseSync<T> : RepoBase, // BaseClasses.tt Line: 113, called from ModelGlobal.tt Line: 9
			    IRepository<T>
			    where T : class, IEntityBaseExplicit<T>, ISameById<T>, IEntityBase
			#if !DEBUG
			    , new()
			#endif
			{
			    /// <summary>
			    /// Unique string for current version
			    /// </summary>
				public string TYPE_CACHE_ID { get; private set; }
				public RepoBaseSync(string typeId)
				{
					this.TYPE_CACHE_ID = typeId;
				}
				// BaseRepository.tt Line: 8, called from BaseClasses.tt Line: 131
				#if DEBUG
				protected static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				protected static T instance = new T();
				#endif
				protected virtual T GetThis() { throw new NotImplementedException(); } // BaseRepository.tt Line: 14
				public static T Create([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 44*/) // BaseRepository.tt Line: 44
				{
					var id = GetNextId(instance.GetGuid()); // BaseRepository.tt Line: 48
					var res = instance.CreateDto(id);
				    res.IsNeedInsert(true);
					return res;
				}
				public static IEnumerable<T> Select(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 96*/) // BaseRepository.tt Line: 96
				{
				    var cd = instance.GetCommandDefinitionSelect(where, param, sort, page, pagesize); // BaseRepository.tt Line: 102
				    var lst_dto = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return lst_dto;
				}
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 153*/) // BaseRepository.tt Line: 153
				{
					int res; // BaseRepository.tt Line: 159
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				protected virtual int CountUtil(string? where, object? param) { throw new NotImplementedException(); } // BaseRepository.tt Line: 175
			}
			public partial class RepoEntityBaseSync<T> : RepoBaseSync<T>, // BaseClasses.tt Line: 136, called from ModelGlobal.tt Line: 9
			    IRepositoryEntity<T>, IAttachForUpdates
			    where T : class, IEntityBaseExplicit<T>, ISameById<T>, IEntityBase
			#if !DEBUG
			    , new()
			#endif
			{
				public RepoEntityBaseSync(string typeId) : base(typeId)	{ }
				// BaseRepositoryEntity.tt Line: 8, called from BaseClasses.tt Line: 147
				protected TransactionOnCommit? transactionOnCommit = null;
				public void AttachForUpdates(TransactionOnCommit tx)
				{
				    this.transactionOnCommit = tx;
				}
				public void Save([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 50*/) // BaseRepositoryEntity.tt Line: 50
				{
				    Debug.Assert(this.transactionOnCommit == null, "Entity already attached to transaction on commit");
				    Debug.Assert(!this.IsRemoved());
				    var dft = new TransactionOnCommit();
				    var entity = this.GetThis();
				    dft.Save(entity);
				    dft.Commit();
				}
				public void Remove([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 141*/) // BaseRepositoryEntity.tt Line: 141
				{
				    Debug.Assert(this.transactionOnCommit == null, "Entity already attached to transaction on commit");
				    Debug.Assert(!this.IsNeedInsert());
				    Debug.Assert(!this.IsRemoved());
					Remove(this.GetThis().Id);
				    this.IsRemoved(true);
					foreach (var t in this.GetThis().GetChildren())
						t.IsRemoved(true);
				}
				public static void Remove(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 153*/) // BaseRepositoryEntity.tt Line: 153
				{
				    var cd = instance.GetCommandDefinitionRemoveById(id); // BaseRepositoryEntity.tt Line: 160
				    ConnExecute((conn) => { conn.Execute(cd.GetCommandDefinition()); }, RetryPolicy);
				}
				public static void Remove(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 176*/) // BaseRepositoryEntity.tt Line: 176
				{
				    var cd = instance.GetCommandDefinitionRemoveWhere(where, param); // BaseRepositoryEntity.tt Line: 183
				    ConnExecute((conn) => { conn.Execute(cd.GetCommandDefinition()); }, RetryPolicy);
				}
				protected static bool isEntityWithTabs;
				public static T? Load(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 257*/) // BaseRepositoryEntity.tt Line: 257
				{
				    var cd = instance.GetCommandDefinitionLoadById(id); // BaseRepositoryEntity.tt Line: 264
				    T? dto = null;
				    dto = ConnSelectSingle<T>((conn) =>  // BaseRepositoryEntity.tt Line: 276
				    { 
				        if (isEntityWithTabs) // BaseRepositoryEntity.tt Line: 278
				        {
				            using(var multi = conn.QueryMultiple(cd.GetCommandDefinition()))
				            {
				                return instance.LoadUtil(multi);
				            }
				        }
				        else // BaseRepositoryEntity.tt Line: 285
				        {
				            return conn.Query<T>(cd.GetCommandDefinition()).Single(); 
				        }
				    }, RetryPolicy); // BaseRepositoryEntity.tt Line: 289
					return dto;
				}
			}
			public partial class ViewEntityBaseSync<T> // BaseClasses.tt Line: 152, called from ModelGlobal.tt Line: 9
			    where T : class, IViewPlainBaseExplicit<T>, ISameById<T>
			#if !DEBUG
			    , new()
			#endif
			{
				// BaseView.tt Line: 8, called from BaseClasses.tt Line: 161
				#if DEBUG
				private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				private static T instance = new T();
				#endif
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
				{
					int res; // BaseView.tt Line: 277
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionView(pagesize, page, sort, where, param); // BaseView.tt Line: 301
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
			}
			public partial class ViewTreeBaseSync<T> // BaseClasses.tt Line: 166, called from ModelGlobal.tt Line: 9
			    where T : class, IViewSelfTreeBaseExplicit<T>, ISameById<T>
			#if !DEBUG
			    , new()
			#endif
			{
				// BaseView.tt Line: 8, called from BaseClasses.tt Line: 175
				#if DEBUG
				private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				private static T instance = new T();
				#endif
				public static IEnumerable<T> GetSubTreeView(int? parentId, int deep = 2, string? sort = null, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 101*/) // BaseView.tt Line: 101
				{
				    var cd = instance.GetCommandDefinitionSubTreeView(parentId, deep, sort, where, param); // BaseView.tt Line: 105
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetTreeListView(int? selectedId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 121*/) // BaseView.tt Line: 121
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var res = new List<T>();
				    var cd = instance.GetCommandDefinitionTreeListSubView(selectedId, sort, where, param); // BaseView.tt Line: 130
				    var resItems = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
				    if (selectedId.HasValue)
				    {
				        cd = instance.GetCommandDefinitionTreeListView(selectedId, where, param); // BaseView.tt Line: 136
				        res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy).ToList();
				    }
				    res.AddRange(resItems);
				    if (pagesize > 0)
				        res = res.AsQueryable().Skip((page - 1) * pagesize).Take(pagesize).ToList();
					return res;
				}
				public static IEnumerable<T> GetViewByParentId(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 153*/) // BaseView.tt Line: 153
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionSubItemsView(parentId, pagesize, page, sort, where, param); // BaseView.tt Line: 161
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
				{
					int res; // BaseView.tt Line: 277
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionView(pagesize, page, sort, where, param); // BaseView.tt Line: 301
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
			}
			public partial class ViewDetailBaseSync<T> // BaseClasses.tt Line: 180, called from ModelGlobal.tt Line: 9
			    where T : class, IViewPlainForRefTreeBaseExplicit<T>, ISameById<T>
			#if !DEBUG
			    , new()
			#endif
			{
				// BaseView.tt Line: 8, called from BaseClasses.tt Line: 189
				#if DEBUG
				private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				private static T instance = new T();
				#endif
				public static IEnumerable<T> GetViewByParentId(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 202*/) // BaseView.tt Line: 202
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionSubItemsView(parentId, pagesize, page, sort, where, param); // BaseView.tt Line: 210
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
				{
					int res; // BaseView.tt Line: 277
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionView(pagesize, page, sort, where, param); // BaseView.tt Line: 301
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
			}
			public class ListChildren<T> : List<T> // ModelGlobal.tt Line: 12, called from NameSpace.tt Line: 176
			    where T : class, IEntityBase
			{
			    /// <summary>
			    /// Init collection of children entities with actions for ADD and REMOVE child.
			    /// Typical action for CREATE is assign ID of created entity.
			    /// For ADD is update reference to parent entity.
			    /// </summary>
			    /// <param name="onCreate">Function to create child</param>
			    /// <param name="onAdd">Action for added child</param>
			    /// <param name="onRemove">Action for removed child</param>
			    public ListChildren(Func<T> onCreate, Action<T> onAdd, Action<T> onRemove) // ModelGlobal.tt Line: 41
			    {
			        this.onCreate = onCreate;
			        this.onAdd = onAdd;
			        this.onRemove = onRemove;
			    }
			    readonly Func<T> onCreate;
			    readonly Action<T> onAdd;
			    readonly Action<T> onRemove;
			    /// <summary>
			    /// Create and add new child instance
			    /// </summary>
			    /// <returns>New created child instance</returns>
			    public T AddNew() // ModelGlobal.tt Line: 71
			    {
			        Debug.Assert(this.onCreate != null);
			        Debug.Assert(this.onAdd != null);
			        var item = onCreate();
			        this.onAdd(item);
			        base.Add(item);
			        return item;
			    }
			    public new void Add(T item) // ModelGlobal.tt Line: 80
			    {
			        Debug.Assert(this.onAdd != null);
			        Debug.Assert(item.Id > 0);
			        this.onAdd(item);
			        base.Add(item);
			    }
			    public new void AddRange(IEnumerable<T> collection) // ModelGlobal.tt Line: 87
			    {
			        Debug.Assert(this.onAdd != null);
			        foreach (var t in collection)
			        {
			            Debug.Assert(t.Id > 0);
			            this.onAdd(t);
			        }
			        base.AddRange(collection);
			    }
			    public new void Clear() // ModelGlobal.tt Line: 97
			    {
			        Debug.Assert(this.onRemove != null);
			        foreach (var t in this)
			        {
			            this.onRemove(t);
			        }
			        base.Clear();
			    }
			    public new void Insert(int index, T item) // ModelGlobal.tt Line: 106
			    {
			        Debug.Assert(this.onAdd != null);
			        Debug.Assert(item.Id > 0);
			        this.onAdd(item);
			        base.Insert(index, item);
			    }
			    public new void InsertRange(int index, IEnumerable<T> collection) // ModelGlobal.tt Line: 113
			    {
			        Debug.Assert(this.onAdd != null);
			        foreach (var t in collection)
			        {
			            Debug.Assert(t.Id > 0);
			            this.onAdd(t);
			        }
			        base.InsertRange(index, collection);
			    }
			    public new void Remove(T item) // ModelGlobal.tt Line: 123
			    {
			        Debug.Assert(this.onRemove != null);
			        this.onRemove(item);
			        base.Remove(item);
			    }
			    public new void RemoveAll(Predicate<T> match) // ModelGlobal.tt Line: 129
			    {
			        Debug.Assert(this.onRemove != null);
			        foreach (var t in this)
			        {
			            this.onRemove(t);
			        }
			        base.RemoveAll(match);
			    }
			    public new void RemoveAt(int index) // ModelGlobal.tt Line: 138
			    {
			        Debug.Assert(this.onRemove != null);
			        this.onRemove(this[index]);
			        base.RemoveAt(index);
			    }
			    public new void RemoveRange(int index, int count) // ModelGlobal.tt Line: 144
			    {
			        Debug.Assert(this.onRemove != null);
			        for (int i = 0; i < count; i++)
			        {
			            this.onRemove(this[i + index]);
			        }
			        base.RemoveRange(index, count);
			    }
			}
			
			
			#region Id Generator
			public class IdGenerator // IdGenerator.tt Line: 8, called from NameSpace.tt Line: 187
			{
			    // connStr, table name, IdItem
			    private static readonly Dictionary<string /* GUID */, string /* table name */> dicTableNames = new()
			    {
			        { "9468ACEC-52F0-4049-9A31-CC1A92F4EA4B", "_history_objects_ids" },
			        { "433FBC18-BAF3-400C-BC77-1D5FC7C43C1F", "_history" },
			        { "a59b9a9d-5627-43d1-92a8-a5c610d52d78", "CnstGroup1Constant1" },
			        { "e1f0ae82-9268-4bc8-96b0-75492411e07e", "CnstGroup1Constant2" },
			        { "5cc54978-0abe-4eef-a97a-f48713978731", "CtlgCatalog1" },
			        { "2d30325d-5000-4a41-a464-cab89db5f4b5", "CtlgCatalog1Folder" },
			    };
			    public static int GetHiLo(string guid, int qtyId, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from IdGenerator.tt Line: 28*/) // IdGenerator.tt Line: 28
			    {
			        var tableName = IdGenerator.dicTableNames[guid];
			        int id = 0; // IdGenerator.tt Line: 64
			        int last_id = 0;
			        UnitOfWorkBase? uow = null;
			        if (uow != null)
			        {
			            int? prev_id = uow.Connection.ExecuteScalar<int?>(
			                "SELECT LastId FROM _id_generator WHERE Guid = @g;", new { g = guid });
			            if (prev_id == null)
			            {
			                prev_id = uow.Connection.ExecuteScalar<int?>($"SELECT Max(Id) FROM {tableName};", new { });
			                if (prev_id == null) prev_id = 0;
			                last_id = (prev_id ?? 0) + qtyId;
			                uow.Connection.Execute("INSERT INTO _id_generator(Guid, LastId, TableName) VALUES(@g, @i, @t);",
			                    new { g = guid, i = last_id, t = tableName });
			            }
			            last_id = (prev_id ?? 0) + qtyId;
			            uow.Connection.Execute("UPDATE _id_generator SET LastId=@i WHERE Guid = @g;",
			                new { i = last_id, g = guid });
			            id = (prev_id ?? 0) + 1;
			        }
			        else // IdGenerator.tt Line: 85
			        {
			            int? prev_id = Model.ConnExecuteScalar<int?>((conn) => 
			                { return conn.ExecuteScalar<int?>("SELECT LastId FROM _id_generator WHERE Guid = @g;", new { g = guid }); }, Model.RetryPolicy);
			            if (prev_id == null)
			            {
			                prev_id = Model.ConnExecuteScalar<int?>((conn) => 
			                    { return conn.ExecuteScalar<int?>($"SELECT Max(Id) FROM {tableName};", new { }); }, Model.RetryPolicy);
			                if (prev_id == null) prev_id = 0;
			                last_id = (prev_id ?? 0) + qtyId;
			                Model.ConnExecute((conn) => 
			                    { conn.Execute("INSERT INTO _id_generator(Guid, LastId, TableName) VALUES(@g, @i, @t);",
			                    new { g = guid, i = last_id, t = tableName }); }, Model.RetryPolicy);
			            }
			            last_id = (prev_id ?? 0) + qtyId;
			            Model.ConnExecute((conn) => 
			                { conn.Execute("UPDATE _id_generator SET LastId=@i WHERE Guid = @g;", new { i = last_id, g = guid }); }, Model.RetryPolicy);
			            id = (prev_id ?? 0) + 1;
			        }
			        return id;
			    }
			}
			public class HiLoService : IHiLoService // ModelCacheId.tt Line: 9, called from NameSpace.tt Line: 190
			{
			    public HiLoResult GetHiLo(HiLoRequest request)
			    {
			        var nextId = IdGenerator.GetHiLo(request.Guid, request.RequestedQty);
			        return new HiLoResult() { NextId = nextId, ReturnedQty = request.RequestedQty };
			    }
			}
			public enum EnumHiType { Fixed, PIDController }; // ModelCacheId.tt Line: 50
			private static ICacheId? cacheId;
			public static void InitCacheId(EnumHiType type, IHiLoService? serv) // ModelCacheId.tt Line: 52
			{
			    if (serv == null)
			        serv = new HiLoService();
			    switch (type)
			    {
			        case EnumHiType.Fixed:
			            Model.cacheId = new CacheIdHiLo(serv);
			            break;
			        case EnumHiType.PIDController:
			            Model.cacheId = new CacheIdHiLoPid(serv);
			            break;
			    }
			}
			public static CacheIdHiLo CacheIdHiLo // ModelCacheId.tt Line: 66
			{
			    get
			    {
			        System.Diagnostics.Debug.Assert(Model.cacheId != null);
			        System.Diagnostics.Debug.Assert(Model.cacheId is CacheIdHiLo);
			        return (CacheIdHiLo)Model.cacheId;
			    }
			}
			public static CacheIdHiLoPid CacheIdHiLoPid // ModelCacheId.tt Line: 75
			{
			    get
			    {
			        System.Diagnostics.Debug.Assert(Model.cacheId != null);
			        System.Diagnostics.Debug.Assert(Model.cacheId is CacheIdHiLoPid);
			        return (CacheIdHiLoPid)Model.cacheId;
			    }
			}
			public static int GetNextId(string guid) // ModelCacheId.tt Line: 85
			{
			    System.Diagnostics.Debug.Assert(Model.cacheId != null);
			    return Model.cacheId.GetNextId(guid);
			}
			#endregion Id Generator
			
			#region History // Model.tt Line: 19, called from NameSpace.tt Line: 194
			[Dapper.Contrib.Extensions.Table("_history_objects_ids")]
			public partial class _history_objects_ids // Model.tt Line: 23
			{
			    [Dapper.Contrib.Extensions.Key]
			    public string object_guid { get { return _object_guid; } set { _object_guid = value; } }
			    private string _object_guid = string.Empty;
			    public int Id { get { return _Id; } set { _Id = value; } }
			    private int _Id;
			
			    public const string T_GUID = "9468ACEC-52F0-4049-9A31-CC1A92F4EA4B";
			    public const string T_NAME = "_history_objects_ids";
			    public const string F_ID = "object_guid";
			    public const string F_OBJECT_ID = "object_id";
			    /// <summary>
			    /// Get object int ID
			    /// </summary>
			    /// <returns>
			    /// int value
			    /// </returns>
			    /// <param name="guid">Object guid</param>
			    public static int GetObjectId(string guid, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 46*/) // Model.tt Line: 46
			    {
			        int res = 0;
			        ConnExecute((conn) =>
			        {
			            var rec = conn.QuerySingleOrDefault<_history_objects_ids>("SELECT * FROM _history_objects_ids WHERE object_guid=@g;",
			                new { g = guid }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            if (rec == null)
			            {
			                rec = new _history_objects_ids
						    {
			                    Id = (int)Model.GetNextId(_history_objects_ids.T_GUID),
			                    object_guid = guid
			                };
			                conn.Execute("INSERT INTO _history_objects_ids (Id,object_guid) VALUES (@i,@g);",
			                    new { i=rec.Id, g=rec.object_guid }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            }
			            res = rec.Id;
			        }, Model.RetryPolicy);
			        return res;
			    }
			}
			[Dapper.Contrib.Extensions.Table("_history")]
			public class _history // Model.tt Line: 122
			{
			    [Dapper.Contrib.Extensions.Key]
				public int Id { get { return _Id; } set { _Id = value; } }
				private int _Id;
				public int object_id { get { return _object_id; } set { _object_id = value; } }
				private int _object_id;
				public int date_time { get { return _date_time; } set { _date_time = value; } }
				private int _date_time;
				public string? val { get { return _val; } set { _val = value; } }
				private string? _val;
			
			    public const string T_GUID = "433FBC18-BAF3-400C-BC77-1D5FC7C43C1F";
			    public const string T_NAME = "_history";
				public const string F_ID = "id";
				public const string F_OBJECT_ID = "object_id";
				public const string F_DATE_TIME = "date_time";
				public const string F_VAL = "val";
			
			    /// <summary>
			    /// Conversion from DateTime to integer format. 
			    /// Accuracy is one minute. Supported year range from 1000 to 3048
			    /// </summary>
			    /// <returns>
			    /// int value of DateTime
			    /// </returns>
			    /// <param name="utc">UTC Date and time in DateTime format</param>
			    public static int FromDateTime(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 152*/) // Model.tt Line: 152
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        System.Diagnostics.Debug.Assert(utc.Year >= 1000);
			        System.Diagnostics.Debug.Assert(utc.Year < 3040);
			        int res = utc.Year - 1000;
			        res <<= 9;
			        res += utc.DayOfYear;
			        res <<= 5;
			        res += utc.Hour;
			        res <<= 6;
			        res += utc.Minute;
			        System.Diagnostics.Debug.Assert(res > 0);
			        return res;
			    }
			    /// <summary>
			    /// Conversion from integer to DateTime format. 
			    /// Accuracy is one minute. Supported year range from 1000 to 3048
			    /// </summary>
			    /// <returns>
			    /// int value of DateTime
			    /// </returns>
			    /// <param name="dt">Date and time in integer format</param>
			    public static DateTime ToDateTime(int dt, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 179*/) // Model.tt Line: 179
			    {
			        System.Diagnostics.Debug.Assert(dt > 0);
			        int m = 0b_0000_0000_0000_0000_0000_0000_0011_1111;
			        int mm = dt & m;
			        int h = 0b_0000_0000_0000_0000_0000_0000_0001_1111;
			        dt >>= 6;
			        int hh = dt & h;
			        int d = 0b_0000_0000_0000_0000_0000_0001_1111_1111;
			        dt >>= 5;
			        int dd = dt & d;
			        dt >>= 9;
			        int yy = dt + 1000;
			        var res = new DateTime(yy, 1, 1, hh, mm, 0, DateTimeKind.Utc).AddDays(dd - 1);
			        return res;
			    }
			    /// <summary>
			    /// Get constant history string value
			    /// </summary>
			    /// <returns>
			    /// string value
			    /// </returns>
			    /// <param name="constantId">Constant ID</param>
			    /// <param name="utc">UTC date and time</param>
			    public static string? Load(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 207*/) // Model.tt Line: 207
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        string? res = null;
			        ConnExecute((conn) =>
			        {
			            var h = conn.QuerySingleOrDefault<_history>(
			                "SELECT * FROM _history WHERE object_id=@i  AND date_time<@d ORDER BY date_time DESC LIMIT 1;", 
			                new { i = constantId, d = _history.FromDateTime(utc) },
						    commandTimeout: Model.CommandTimeout, commandType: CommandType.Text
						);
			            if (h != null) res = h.val;
			        }, Model.RetryPolicy);
			        return res;
			    }
			    /// <summary>
			    /// Save constant string value in history
			    /// </summary>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="val">New value for constant.</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static void Save(int constantId, string? val, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 339*/) // Model.tt Line: 339
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc);
			        ConnExecute((conn) =>
			        {
			            var lst = conn.Query<_history>("SELECT * FROM _history WHERE " +
			                "object_id=@i AND date_time=@d",
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text).ToList();
			            if (lst.Count == 1)
			            {
			                var rec = lst[0];
			                rec.val = val;
			                conn.Execute("UPDATE _history SET val=@v WHERE Id=@i;",
			                    new { v = val, i = rec.Id }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            }
			            else if (lst.Count == 0)
			            {
			                var rec = new _history
			                {
			                    Id = (int)Model.GetNextId(_history.T_GUID),
			                    date_time = dt,
			                    val = val
			                };
			                conn.Execute("INSERT INTO _history (Id,object_id,date_time,val) VALUES (@i,@o,@d,@v);",
			                    new { i = rec.Id, o = constantId, d = dt, v = val }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            }
			            else
			            {
			                System.Diagnostics.Debug.Assert(false);
			            }
			        }, Model.RetryPolicy);
			    }
			    /// <summary>
			    /// Select list constant history records before utc date and time.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static IEnumerable<_history> SelectBefore(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 446*/) // Model.tt Line: 446
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc);
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i AND date_time<@d ORDER BY date_time DESC;",
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Select list constant history records after utc date and time.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static IEnumerable<_history> SelectAfter(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 512*/) // Model.tt Line: 512
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc);
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i AND date_time>@d ORDER BY date_time DESC;",
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Select list constant history records for period.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utcTo">UTC DateTime</param>
			    /// <param name="utcFrom">UTC DateTime</param>
			    public static IEnumerable<_history> Select(int constantId, DateTime utcFrom, DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 579*/) // Model.tt Line: 579
			    {
					System.Diagnostics.Debug.Assert(utcFrom.Kind != DateTimeKind.Unspecified);
					System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
			        if (utcFrom.Kind == DateTimeKind.Local)
			            utcFrom = utcFrom.ToUniversalTime();
			        if (utcTo.Kind == DateTimeKind.Local)
			            utcTo = utcTo.ToUniversalTime();
			        var dt = _history.FromDateTime(utcFrom);
			        var dt2 = _history.FromDateTime(utcTo);
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i AND date_time>@d AND date_time<@t ORDER BY date_time DESC;",
			                new { i = constantId, d = dt, t = dt2 }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Select list constant all history records.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    public static IEnumerable<_history> Select(int constantId, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 653*/) // Model.tt Line: 653
			    {
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i ORDER BY date_time DESC;",
			                new { i = constantId }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Clean history records older utc.
			    /// Arter cleaning only one record older utc will be kept.
			    /// </summary>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static void CleanOlder(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 710*/) // Model.tt Line: 710
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc) + 1;
			        ConnExecute((conn) => // Model.tt Line: 719
			        {
			            var dt2 = conn.ExecuteScalar("SELECT date_time FROM _history WHERE object_id=@i AND date_time<@d ORDER BY date_time DESC LIMIT 1;", 
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text); // Model.tt Line: 737
			            if (dt2 != null)
			                conn.Execute("DELETE FROM _history WHERE object_id=@i AND date_time<@d;", 
			                    new { i = constantId, d = dt2 }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			    }
			    /// <summary>
			    /// Remove history record
			    /// </summary>
			    public static void Delete(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 861*/) // Model.tt Line: 861
			    {
			        ConnExecute((conn) =>
			        {
			            conn.Execute("DELETE FROM _history WHERE id=@i;", new { i = id }, 
			                commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			    }
			    /// <summary>
			    /// Remove all history records for constant
			    /// </summary>
			    public static void Reset(int constantId, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 907*/) // Model.tt Line: 907
			    {
			        ConnExecute((conn) =>
			        {
			            conn.Execute("DELETE FROM _history WHERE object_id=@i;", new { i = constantId }, 
			                commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			    }
			}
			#endregion History
			public partial class UnitOfWorkBase : IUnitOfWork // UnitOfWork.tt Line: 10, called from NameSpace.tt Line: 197
			{
			    // TODO https://docs.microsoft.com/en-us/dotnet/api/system.transactions?view=dotnet-plat-ext-3.1
			    // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.begintransaction?view=dotnet-plat-ext-3.1
			    #region General // UnitOfWork.tt Line: 15
				private bool _disposed;
			    protected readonly List<IEntityBase> listResetFlags = new();
			    public System.Data.Common.DbConnection Connection { get; private set; }
			    public System.Data.Common.DbTransaction Transaction { get; private set; }
			    #if DEBUG
			    public static bool IsInTransaction;
			    #endif
				public UnitOfWorkBase() // UnitOfWork.tt Line: 25
				{
			        this.Connection = new SqliteConnection(Model.ConnectionString);
			        this.Connection.Open();
			        this.Transaction = this.Connection.BeginTransaction();
			        #if DEBUG
			        UnitOfWorkBase.IsInTransaction = true;
			        #endif
				}
			    private Exception? exTransaction = null;
				public void Commit() // UnitOfWork.tt Line: 42
				{
					try
					{
			            this.Transaction.Commit();
					}
					catch(Exception ex)
					{
			            exTransaction = ex;
			            Transaction.Rollback();
						throw;
					}
					finally
					{
			            if (exTransaction != null)
			            {
			                Debug.Assert(false);
			            }
			            else
			            {
			                foreach(var t in listResetFlags)
			                {
			                    t.IsNeedInsert(false);
			                    t.IsNeedUpdate(false);
			                }
			            }
			            Transaction.Dispose();
			            Transaction = Connection.BeginTransaction();
					}
				}
				public void Rollback() // UnitOfWork.tt Line: 75
				{
			        Transaction.Rollback();
				}
				public void Dispose() // UnitOfWork.tt Line: 80
				{
					if (Connection.State != ConnectionState.Closed)
			            Connection.Close();
					dispose(true);
					GC.SuppressFinalize(this);
			        #if DEBUG
			        UnitOfWorkBase.IsInTransaction = false;
			        #endif
				}
				private void dispose(bool disposing)
				{
					if (!_disposed)
					{
						if(disposing)
						{
							if (Transaction != null)
							{
			                    Transaction.Dispose();
							}
							if(Connection != null)
							{
			                    Connection.Dispose();
							}
						}
						_disposed = true;
					}
				}
				~UnitOfWorkBase()
				{
					dispose(false);
				}
			    #endregion General // UnitOfWork.tt Line: 114
			    public long Insert<T>(T entityToInsert, int? commandTimeout = null) where T : class
			    {
			        return this.Connection.Insert<T>(entityToInsert, this.Transaction, commandTimeout);
			    }
			    public bool Update<T>(T entityToUpdate, int? commandTimeout = null) where T : class
			    {
			        return this.Connection.Update<T>(entityToUpdate, this.Transaction, commandTimeout);
			    }
			    public bool Delete<T>(T entityToDelete, int? commandTimeout = null) where T : class
			    {
			        return this.Connection.Delete<T>(entityToDelete, this.Transaction, commandTimeout);
			    }
			    public int Execute(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
			    {
			        return this.Connection.Execute(sql, param, this.Transaction, commandTimeout, commandType);
			    }
			}
			public partial class UnitOfWork : UnitOfWorkBase // UnitOfWork.tt Line: 141
			{
				// UnitOfWorkRepository.tt Line: 9, called from UnitOfWork.tt Line: 144
				/// <summary>
				/// Insert record of entity in a DB (without tab records)
				/// </summary>
				/// <returns>
				/// Return ID of inserted record
				/// </returns>
				public int Insert(IEntityBase entity) // UnitOfWorkRepository.tt Line: 19
				{
				    System.Diagnostics.Debug.Assert(entity.IsNeedInsert());
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionInsert(); // UnitOfWorkRepository.tt Line: 25
				    this.listResetFlags.Add(entity);
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
					return entity.Id;
				}
				/// <summary>
				/// Load entity full object (with all tab objects) by ID
				/// </summary>
				/// <returns>
				/// Return entity full object with all tab objects
				/// </returns>
				public T Load<T>(int id) // UnitOfWorkRepository.tt Line: 41
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionLoadById(id); // UnitOfWorkRepository.tt Line: 48
				    return this.Connection.Query<T>(cd.GetCommandDefinition(this.Transaction)).Single();
				}
				/// <summary>
				/// Select records from DB and map them to entity objects
				/// </summary>
				/// <returns>
				/// IEnumerable collection of entity objects
				/// Use string.Empty for 'where' parameter, if you need select all records 
				/// </returns>
				/// <example>
				/// <code>
				/// // select all
				/// var lst = Catalogs.Catalog1.Select(null); 
				/// // select up to 25 objets for second page
				/// lst = Catalogs.Catalog1.Select(null, null, 2, 25); 
				/// // select one record
				/// int id = 7;
				/// lst = Catalogs.Catalog1.Select(Catalogs.Catalog1.F_ID+"=@pid", new { pid = id }); 
				/// </code>
				/// </example>    /// <param name="where">A SQL text for WHERE clause.</param>
				/// <param name="param">An array of parameters.</param>
				/// <param name="sort">An array of DB field names for sorting.</param>
				/// <param name="page">Page number (starting with 1).</param>
				/// <param name="pagesize">Page size. Unlimited if equal zero.</param>
				public IEnumerable<T> Select<T>(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0) // UnitOfWorkRepository.tt Line: 78
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionSelect(where, param, sort, page, pagesize); // UnitOfWorkRepository.tt Line: 85
				    return this.Connection.Query<T>(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Select records from DB and map them to entity objects
				/// Use string.Empty for 'where' parameter, if you need select all records 
				/// </summary>
				/// <returns>
				/// IReadOnlyList collection of entity objects
				/// </returns>
				/// See <see cref="Select(string,object,string,int,int)"/> for sample.
				public IReadOnlyList<T> SelectList<T>(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0) // UnitOfWorkRepository.tt Line: 101
				    where T : IEntityBase, new()
				{
					IEnumerable<T> lst = Select<T>(where, param, sort, page, pagesize); // UnitOfWorkRepository.tt Line: 107
					List<T> res = new List<T>();
					foreach(var t in lst) { res.Add(t);	}
					return res;
				}
				/// <summary>
				/// Count selected records from DB
				/// </summary>
				/// <returns>
				/// Return type is 'int?'
				/// </returns>
				/// See <see cref="Select(string,object,string,int,int)"/> for sample.
				public int? Count<T>(string? where, object? param = null) // UnitOfWorkRepository.tt Line: 124
				    where T : IEntityBase, new()
				{
					int? res = null; // UnitOfWorkRepository.tt Line: 130
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionCountWhere(where, param); // UnitOfWorkRepository.tt Line: 132
				    this.Connection.ExecuteScalar<int?>(cd.GetCommandDefinition(this.Transaction));
					return res;
				}
				/// <summary>
				/// Update entity record in DB (without tab records)
				/// </summary>
				public void Update<T>(T entity) // UnitOfWorkRepository.tt Line: 144
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsNeedInsert());
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionUpdate(); // UnitOfWorkRepository.tt Line: 152
				    this.listResetFlags.Add(entity);
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Save entity in DB (with tabs)
				/// </summary>
				/// <returns>
				/// Return ID of inserted object
				/// </returns>
				public void Save<T>(T entity) // UnitOfWorkRepository.tt Line: 167
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    var lstCmd = ((IEntityBaseExplicit)entity).GetCommandDefinitionSave(); // UnitOfWorkRepository.tt Line: 174
				    foreach(var d in lstCmd)
				    {
				        System.Diagnostics.Debug.Assert(d.Entity != null);
				        System.Diagnostics.Debug.Assert(!d.Entity.IsRemoved());
				        this.listResetFlags.Add(d.Entity);
				        this.Connection.Execute(d.GetCommandDefinition(this.Transaction));
				    }
				}
				/// <summary>
				/// Delete entity record in DB (without tabs records)
				/// </summary>
				public void Delete<T>(T entity) // UnitOfWorkRepository.tt Line: 191
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    System.Diagnostics.Debug.Assert(!entity.IsNeedInsert());
					var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionDeleteById(entity.Id);
					this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Delete entity record in DB (without tabs records)
				/// </summary>
				public void Delete<T>(int id) // UnitOfWorkRepository.tt Line: 202
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionDeleteById(id); // UnitOfWorkRepository.tt Line: 209
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Delete entity objects in DB (without tabs records)
				/// Use string.Empty for 'where' parameter, if you need delete all records 
				/// </summary>
				public void Delete<T>(string? where, object? param = null) // UnitOfWorkRepository.tt Line: 221
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionDeleteWhere(where, param); // UnitOfWorkRepository.tt Line: 228
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Remove entity in DB (with tabs records)
				/// </summary>
				public void Remove<T>(T entity) // UnitOfWorkRepository.tt Line: 239
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    System.Diagnostics.Debug.Assert(!entity.IsNeedInsert());
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionRemoveById(entity.Id); // UnitOfWorkRepository.tt Line: 244
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Remove entity by ID in DB (with tabs records)
				/// </summary>
				public void Remove<T>(int id) // UnitOfWorkRepository.tt Line: 250
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionRemoveById(id); // UnitOfWorkRepository.tt Line: 257
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Remove selected entity records in DB (with tabs records)
				/// Use string.Empty for 'where' parameter, if you need delete all records 
				/// </summary>
				public void Remove<T>(string? where, object? param = null) // UnitOfWorkRepository.tt Line: 269
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionRemoveWhere(where, param); // UnitOfWorkRepository.tt Line: 276
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
			}
			public partial class TransactionOnCommit // DeferredTransaction.tt Line: 9
			{
			    #region General // DeferredTransaction.tt Line: 12 
			    private readonly List<Op> ListOperations;
			    private readonly Dictionary<string, Dictionary<int, string?>> dicInsertedUpdatedGuidId;
				public TransactionOnCommit() // DeferredTransaction.tt Line: 18
				{
			        this.ListOperations = new List<Op>();
			        this.dicInsertedUpdatedGuidId = new Dictionary<string, Dictionary<int, string?>>();
				}
			    private System.Data.Common.DbTransaction? transaction;
			    public void Commit() // DeferredTransaction.tt Line: 35 
			    {
			        ConnExecute((conn) =>
			        {
						var curr_op = default(Op);
			            try
			            {
			                conn.Open();
			                this.transaction = conn.BeginTransaction();
			                foreach (var op in this.ListOperations)
			                {
			                    var cd = op.Cd;
			                    var cmd = new CommandDefinition(cd.CommandText, cd.Parameters, transaction, CommandTimeout, cd.CommandType);
						        curr_op = op;
			                    conn.Execute(cmd);
			                }
			                transaction.Commit();
			                curr_op = null;
					        foreach(var t in this.ListOperations)
					        {
			                    if (t.Entity == null)
			                        continue;
					            if (t.OpType == Op.EnumOpType.Delete)
					            {
					                t.Entity.IsRemoved(true);
					            }
					            else if (t.OpType == Op.EnumOpType.Remove)
							    {
							        t.Entity.IsRemoved(true);
							        foreach(var tt in t.Entity.GetChildren())
							        {
							            tt.IsRemoved(true);
							        }
							    }
					            else if (t.OpType == Op.EnumOpType.Sql) { }
					            else
					            {
					                t.Entity.IsNeedInsert(false);
					                t.Entity.IsNeedUpdate(false);
					            }
					        }
			                this.ListOperations.Clear();
			            }
			            catch (Exception ex)
			            {
			                if (transaction != null)
			                {
			                    transaction.Rollback();
			                }
			                if (curr_op != null)
			                    throw new Exception($"{ex.Message}  File:{curr_op.File}, Line:{curr_op.Line}, Member:{curr_op.Member}. See inner exception.", ex);
			                else
			                    throw;
			            }
			            finally
			            {
			                transaction?.Dispose();
			                if (conn.State != ConnectionState.Closed)
			                    conn.Close();
			            }
			        }, RetryPolicy);
			    }
			    //TODO commandTimeout from this
			    public void AddCommand(string commandText, object? parameters = null,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 250 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 250
			    {
			        var cd = new CommandDefinitionData(commandText, parameters, CommandType.Text);
			        this.ListOperations.Add(new Op(cd, file, member, line));
			    }
			    /// <summary>
			    /// When entity child collections are changed, model will generate operation to insert or remove child records from DB.
			    /// If entity is attached to transaction on commit, appropriate commands will be added to this transaction instead of immediate execution. 
			    /// </summary>
			    public void AttachForUpdates(IAttachForUpdates entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 277 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 277
			    {
			        entity.AttachForUpdates(this);
			    }
			    /// <summary>
			    /// Insert record in DB on Commit() call (without tab records)
			    /// </summary>
			    public void Insert(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 287 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 287
			    {
					Debug.Assert(!entity.IsRemoved());
					Debug.Assert(entity.IsNeedInsert());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Insert, file, member, line));
			    }
			    /// <summary>
			    /// Update record in DB on Commit() call (without tab records)
			    /// </summary>
			    public void Update(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 299 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 299
			    {
					Debug.Assert(!entity.IsRemoved());
					Debug.Assert(entity.IsNeedUpdate());
					Debug.Assert(!entity.IsNeedInsert());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Update, file, member, line));
			    }
			    /// <summary>
			    /// Delete record in DB on Commit() call (without tab records)
			    /// </summary>
			    public void Delete(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 312 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 312
			    {
					Debug.Assert(!entity.IsRemoved());
					Debug.Assert(!entity.IsNeedInsert());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Delete, file, member, line));
			    }
			    /// <summary>
			    /// Remove object in DB on Commit() call (with tab records)
			    /// </summary>
			    public void Remove(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 324 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 324
			    {
					Debug.Assert(!entity.IsRemoved());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Remove, file, member, line));
			    }
			    /// <summary>
			    /// Insert or Update object in DB on Commit() call (with Insert or Update tab records)
			    /// </summary>
			    public void Save(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 335 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 262
			    {
			        Debug.Assert(!entity.IsRemoved());
			        if (entity.IsNeedInsert())
			            this.ListOperations.Add(new Op(entity, Op.EnumOpType.Insert, file, member, line));
			        else if (entity.IsNeedUpdate())
			            this.ListOperations.Add(new Op(entity, Op.EnumOpType.Update, file, member, line));
			        foreach (var t in entity.GetChildren())
			        {
			            if (t.IsNeedInsert())
			                this.ListOperations.Add(new Op(t, Op.EnumOpType.Insert, file, member, line));
			            else if (t.IsNeedUpdate())
			                this.ListOperations.Add(new Op(t, Op.EnumOpType.Update, file, member, line));
			        }
			    }
			    #endregion General // DeferredTransaction.tt Line: 350 
			}
			
			#region Connection // Connection.tt Line: 9, called from NameSpace.tt Line: 203
			public static bool IsBuffered = true;
			public static int? CommandTimeout = 100; // seconds
			private static readonly object lock_object = new();
			
			public static Policy RetryPolicy = Policy // Connection.tt Line: 17
			    .Handle<Exception>(ex => false)
			    // deadlock only
			    // timeout excluded  || ex.Number == -2
			    // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlerror.number?redirectedfrom=MSDN&view=dotnet-plat-ext-5.0#System_Data_SqlClient_SqlError_Number
			    // https://docs.microsoft.com/en-ca/sql/connect/ado-net/step-4-connect-resiliently-to-sql-with-ado-net?view=sql-server-2017
			    // https://docs.microsoft.com/en-us/azure/sql-database/sql-database-develop-error-messages
			    // https://blog.sqlxdetails.com/commandtimeout-how-to-handle-it-properly/
			    // https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-error-severities?view=sql-server-2014
			    // https://stackoverflow.com/questions/24041062/know-when-to-retry-or-fail-when-calling-sql-server-from-c
			    // https://www.codeproject.com/Articles/42547/SQL-SERVER-How-To-Handle-Deadlock
			    .WaitAndRetry(new[]
			    {
			        TimeSpan.FromSeconds(5),
			        TimeSpan.FromSeconds(10),
			        TimeSpan.FromSeconds(20)
			    }, (exception, timeSpan) =>
			    {
			        if (exception.InnerException != null)
			        {
			        }
			    });
			public static Polly.Retry.AsyncRetryPolicy RetryPolicyAsync = Policy // Connection.tt Line: 41
			    .Handle<Exception>(ex => false)
			    .WaitAndRetryAsync(new[]
			    {
			        TimeSpan.FromSeconds(5),
			        TimeSpan.FromSeconds(10),
			        TimeSpan.FromSeconds(20)
			    }, (exception, timeSpan) =>
			    {
			        if (exception.InnerException != null)
			        {
			        }
			    });
			private static string? ConnectionString // Connection.tt Line: 160
			{
			    get
			    {
			        if (__connString == null)
			        {
			            __connString = ConfigurationManagerJson.GetConnectionString("Sqlite");
			        }
			        return __connString;
			    }
			}
			 
			private static string? __connString;
			#endregion Connection // Connection.tt Line: 173
			
			#region Connection Wrapper // ConnectionWrapper.tt Line: 8, called from NameSpace.tt Line: 206
			/// <summary>
			/// Execute any methods by using open db connection. Expecting return IEnumerable collection of T objects or null.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <typeparam name="T">Any type for appropriate database table. For example from Catalogs class</typeparam>
			/// <param name="onConnection">function which accept DbConnection and return IEnumerable collection of T objects or null </param>
			/// <returns>IEnumerable collection of T objects or null</returns>
			/// <example>
			/// <code>
			/// var lst2 = Db.ConnSelectMany((conn) => { return conn.GetAll&lt;Catalogs.Simple&gt;(); });
			/// </code>
			/// </example>    
			public static IEnumerable<T> ConnSelectMany<T>(Func<System.Data.Common.DbConnection, IEnumerable<T>> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 48
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    IEnumerable<T> res = new List<T>();
			    policy.Execute(() =>
			    {
			        using var conn = new SqliteConnection(Model.ConnectionString);
			        lock(lock_object) // multi-thread connection is not supported 
			        {
			            res = onConnection(conn);
			        }
			    });
			    return res;
			}
			/// <summary>
			/// Execute any methods by using open db connection. Expecting return one T objects or null.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <typeparam name="T">Any type for appropriate database table. For example from Catalogs class</typeparam>
			/// <param name="onConnection">function which accept DbConnection and return one T object or null </param>
			/// <returns>one T object or null</returns>
			public static T? ConnSelectSingle<T>(Func<System.Data.Common.DbConnection, T?> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 95
			    where T : class
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    T? res = null;
			    policy.Execute(() =>
			    {
			        using var conn = new SqliteConnection(Model.ConnectionString);
			        lock(lock_object) // multi-thread connection is not supporting 
			        {
			            res = onConnection(conn);
			        }
			    });
			    return res;
			}
			/// <summary>
			/// Execute any methods by using open db connection.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <param name="onConnection">action which accept DbConnection</param>
			public static void ConnExecute(Action<System.Data.Common.DbConnection> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 137
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    policy.Execute(() =>
			    {
			        using var conn = new SqliteConnection(Model.ConnectionString);
			        lock(lock_object) // multi-thread connection is not supported 
			        {
			            onConnection(conn);
			        }
			    });
			}
			/// <summary>
			/// Execute any methods returning scalar value by using open db connection.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <param name="onConnection">function which accept DbConnection</param>
			/// <returns>one T object (int or long)</returns>
			public static T? ConnExecuteScalar<T>(Func<System.Data.Common.DbConnection, T> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 180
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    T? res = default;
			    policy.Execute(() =>
			    {
			        using var conn = new SqliteConnection(Model.ConnectionString);
			        lock(lock_object) // multi-thread connection is not supporting 
			        {
			            res = onConnection(conn);
			        }
			    });
			    return res;
			}
			#endregion Connection Wrapper // ConnectionWrapper.tt Line: 199
			
			#region Dapper // ModelDapper.tt Line: 9, called from NameSpace.tt Line: 209
			public static void DapperInit() // ModelDapper.tt Line: 10
			{
			
			
			    //SqlMapper.AddTypeHandler(typeof(DateTime), DateTimeHandler.Default);
			    //SqlMapper.AddTypeHandler(typeof(DateTime?), DateTimeHandler.Default);
			    //SqlMapper.AddTypeHandler(typeof(DateTimeOffset), DateTimeOffsetHandler.Default);
			    //SqlMapper.AddTypeHandler(typeof(DateTimeOffset?), DateTimeOffsetHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(DateOnly), DateOnlyHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(DateOnly?), DateOnlyHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(TimeOnly), TimeOnlyHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(TimeOnly?), TimeOnlyHandler.Default);
			}
			/*public class DateTimeHandler : Dapper.SqlMapper.TypeHandler<DateTime> // ModelDapper.tt Line: 58
			{
			    private DateTimeHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new DateTimeHandler();
			    public override DateTime Parse(object value)
			    {
			        //if (value is string res)
			        //    return DateTime.Parse(res);
			        //else 
			        if (value is DateTime dt)
			        {
			            if (dt.Kind == DateTimeKind.Utc)
			                return dt;
			            if (dt.Kind == DateTimeKind.Local)
			                return dt.ToUniversalTime();
			        }
			        throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, DateTime value)
			    {
			        parameter.DbType = DbType.DateTime2;
			        parameter.Value = value;
			    }
			}*/
			/*public class DateTimeOffsetHandler : Dapper.SqlMapper.TypeHandler<DateTimeOffset> // ModelDapper.tt Line: 82
			{
			    private DateTimeOffsetHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new DateTimeOffsetHandler();
			    public override DateTimeOffset Parse(object value)
			    {
			        if (value is string res)
			            return DateTimeOffset.Parse(res);
			        else if (value is DateTimeOffset df)
			            return df;
			        else if (value is DateTime dt)
			            return new DateTimeOffset(dt);
			        else
			            throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
			    {
			        parameter.DbType = DbType.DateTimeOffset;
			        parameter.Value = value;
			    }
			}*/
			public class DateOnlyHandler : Dapper.SqlMapper.TypeHandler<DateOnly> // ModelDapper.tt Line: 103
			{
			    private DateOnlyHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new DateOnlyHandler();
			    public override DateOnly Parse(object value)
			    {
			        if (value is string str)
			        {
			            // https://docs.microsoft.com/en-us/dotnet/api/system.datetime.parse?view=net-5.0
			            DateTime d = DateTime.Parse(str);
			            return new DateOnly(d.Year, d.Month, d.Day);
			        }
			        else if (value is DateTime d)
			        {
			            return new DateOnly(d.Year, d.Month, d.Day);
			        }
			        else
			            throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, DateOnly value)
			    {
			        parameter.DbType = DbType.DateTime;
			        parameter.Value = new DateTime(value.Year, value.Month, value.Day);
			    }
			}
			public class TimeOnlyHandler : Dapper.SqlMapper.TypeHandler<TimeOnly> // ModelDapper.tt Line: 128
			{
			    private TimeOnlyHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new TimeOnlyHandler();
			    public override TimeOnly Parse(object value)
			    {
			        if (value is string str)
			        {
			            var d = TimeSpan.Parse(str);
			            return new TimeOnly(d.Hours, d.Minutes, d.Seconds, d.Milliseconds);
			        }
			        else if (value is TimeSpan d)
			        {
			            return new TimeOnly(d.Hours, d.Minutes, d.Seconds, d.Milliseconds);
			        }
			        else
			            throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
			    {
			        parameter.DbType = DbType.Time;
			        parameter.Value = new TimeSpan(0, value.Hour, value.Minute, value.Second, value.Millisecond);
			    }
			}
			private static string CreateQuery(string? selectFrom, string? table, string? where1 = null, string? where2 = null,
			    string? sort = null, int page = 0, int pagesize = 0) // ModelDapper.tt Line: 153
			{
			    System.Diagnostics.Debug.Assert(!(selectFrom == null && table == null));
			    StringBuilder sb = new();
			    if (!string.IsNullOrWhiteSpace(selectFrom))
			    {
			        sb.Append(selectFrom);
			    }
			    else
			    {
			        sb.Append("SELECT * FROM ");
			        sb.Append(table);
			        sb.Append("");
			    }
			    sb.Append(' ');
				if (!string.IsNullOrWhiteSpace(where1) || !string.IsNullOrWhiteSpace(where2))
				{
					sb.Append(" WHERE ");
				}
			    bool btw = !string.IsNullOrWhiteSpace(where1) && !string.IsNullOrWhiteSpace(where2);
				if (!string.IsNullOrWhiteSpace(where1))
				{
				    if (btw)
					    sb.Append('(');
					sb.Append(where1);
				    if (btw)
					    sb.Append(')');
				    if (where2 != null)
				    {
					    sb.Append(" AND ");
				    }
				}
				if (!string.IsNullOrWhiteSpace(where2))
				{
				    if (btw)
					    sb.Append('(');
					sb.Append(where2);
				    if (btw)
					    sb.Append(')');
				}
				if (pagesize > 0 && string.IsNullOrWhiteSpace(sort))
					throw new Exception("To use paging sort parameter has to be provided");
				if (!string.IsNullOrWhiteSpace(sort))
				{
					sb.Append(" ORDER BY ");
					sb.Append(sort);
				}
				if (page > 0 && pagesize > 0)
				{
					sb.Append(" LIMIT ");
					sb.Append(pagesize);
					sb.Append(" OFFSET ");
			        sb.Append((page - 1) * pagesize);
				}
				sb.Append(';');
			    return sb.ToString();
			}
			#endregion Dapper
			
			#region Utils // Utils.tt Line: 8, called from NameSpace.tt Line: 212
			// https://stackoverflow.com/questions/46940710/getting-value-from-appsettings-json-in-net-core
			public static class ConfigurationManagerJson // Utils.tt Line: 10
			{
			    public static IConfiguration AppSetting { get; }
			    static ConfigurationManagerJson()
			    {
			        AppSetting = new ConfigurationBuilder()
			                .SetBasePath(Directory.GetCurrentDirectory())
			                .AddJsonFile("app-settings.json")
			                .Build();
			    }
			    public static string GetConnectionString(string connStrName)
			    {
			        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?view=dotnet-plat-ext-3.1
			        var connString = ConfigurationManagerJson.AppSetting[$"db_conns:{connStrName}:connection_string"];
			        //connPrv = ExtCore.ConfigurationManagerJson.AppSetting["db_conns:" + connStringName + ":provider"];
			        if (connString == null)
			            connString = System.Configuration.ConfigurationManager.ConnectionStrings[connStrName].ConnectionString;
			        return connString;
			    }
			}
			#endregion Utils
			public static ModelSettings Settings { get { if (_Settings == null) _Settings = new ModelSettings(); return _Settings; } }
			private static ModelSettings? _Settings;
			public class ModelSettings // ModelSettings.tt Line: 10
			{
			    public ModelSettings InitDefault()
			    {
			        DapperInit();
			        InitHilo(EnumHiType.Fixed);
			        return this;
			    }
			    public ModelSettings InitHilo(EnumHiType type, IHiLoService? serv = null)
			    {
			        InitCacheId(type, serv);
			        return this;
			    }
			}
        }
            } // Sqlite // NameSpace.tt Line: 252
    namespace PostgreSql // NameSpace.tt Line: 92
    {
        //using DTO = vPlugins.Models.PostgreSql.Model; // NameSpace.tt Line: 95
        //using DTO = vPlugins.DapperModels.PostgreSql.Model; // NameSpace.tt Line: 96
        //using CMD = vPlugins.DapperModels; // NameSpace.tt Line: 97
        using global::Npgsql;
        /// <include file='model_doc.xml' path='Doc/Model/Desc[@name="model"]/*' />
        public partial class Model // NameSpace.tt Line: 110
        {
            public static bool LOGGING_EXTENTIONS { get; } = false; // NameSpace.tt Line: 145
            public static bool LOGGING_NLOG { get; } = false;
            public static bool LOGGING_SERILOG { get; } = false;
			/// <include file='model_doc.xml' path='Doc/Model/Constants/Desc[@name="constants"]/*' />
			public partial class Constants // Constants.tt Line: 14, called from NameSpace.tt Line: 164
			{
			    /// <include file='model_doc.xml' path='Doc/Model/Constants/Desc[@name="LoadValues"]/*' />
			    public static void LoadValues(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 19*/) // Constants.tt Line: 19
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        Group1.LoadValues(utc);
			    }
			    /// <include file='model_doc.xml' path='Doc/Model/Constants/Desc[@name="Reset"]/*' />
			    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 31*/)
			    {
			        Group1.Reset();
			    }
				public class Group1 // Constants.tt Line: 44
				{
				    #region Methods // Constants.tt Line: 48
				    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Desc[@name="LoadValues"]/*' />
				    public static void LoadValues(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 50*/) // Constants.tt Line: 50
				    {
						System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
				        if (utc.Kind == DateTimeKind.Local)
				            utc = utc.ToUniversalTime();
				        CONSTANT1.LoadValue(utc);
				        CONSTANT2.LoadValue(utc);
				    }
				    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Desc[@name="CleanOlder"]/*' />
				    public static void CleanOlder(DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 62*/) // Constants.tt Line: 62
				    {
						System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
				        if (utcTo.Kind == DateTimeKind.Local)
				            utcTo = utcTo.ToUniversalTime();
				        CONSTANT1.CleanOlder(utcTo);
				        CONSTANT2.CleanOlder(utcTo);
				    }
				    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Desc[@name="Reset"]/*' />
				    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Constants.tt Line: 74*/) // Constants.tt Line: 74
				    {
				        CONSTANT1.Reset();
				        CONSTANT2.Reset();
				    }
				    #endregion Methods // Constants.tt Line: 82
					// PocoConstants.tt Line: 9, called from Constants.tt Line: 85
					public class CONSTANT1 // PocoConstants.tt Line: 16
					{
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="GUID"]/*' />
					    public const string GUID = "a59b9a9d-5627-43d1-92a8-a5c610d52d78";
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Value"]/*' />
					    public static string? Value { get; private set; } // PocoConstants.tt Line: 25
						internal static Constants.Group1.CONSTANT1.History_Constant1 Create() // PocoConstants.tt Line: 26
						{
							var res = new Constants.Group1.CONSTANT1.History_Constant1
					        {
							    id = GetNextId(_history.T_GUID)
					        };
							return res;
						}
					    private static int _constantId; // PocoConstants.tt Line: 34
					    private static int constantId { get { if (_constantId == 0) { _constantId = _history_objects_ids.GetObjectId(GUID); } return _constantId; }}
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="LoadValue"]/*' />
					    public static void LoadValue(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 52*/) // PocoConstants.tt Line: 52
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        Value = Load(utc); // PocoConstants.tt Line: 63
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Load"]/*' />
					    public static string? Load(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 70*/) // PocoConstants.tt Line: 70
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = null; // PocoConstants.tt Line: 79
					        var h = _history.Load(constantId, utc);
					        if (h != null)
					            res = h; // PocoConstants.tt Line: 96
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Save"]/*' />
					    public static void Save(string val, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 130*/) // PocoConstants.tt Line: 130
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = val; // PocoConstants.tt Line: 148
					        _history.Save(constantId, res, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectBefore"]/*' />
					    public static List<History_Constant1> SelectBefore(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 174*/) // PocoConstants.tt Line: 174
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant1.ConvertToListOfConstants(_history.SelectBefore(constantId, utc)); // PocoConstants.tt Line: 181
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectAfter"]/*' />
					    public static List<History_Constant1> SelectAfter(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 197*/) // PocoConstants.tt Line: 197
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant1.ConvertToListOfConstants(_history.SelectAfter(constantId, utc)); // PocoConstants.tt Line: 204
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select"]/*' />
					    public static List<History_Constant1> Select(DateTime utcFrom, DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 220*/) // PocoConstants.tt Line: 220
					    {
							System.Diagnostics.Debug.Assert(utcFrom.Kind != DateTimeKind.Unspecified);
							System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
					        if (utcFrom.Kind == DateTimeKind.Local)
					            utcFrom = utcFrom.ToUniversalTime();
					        if (utcTo.Kind == DateTimeKind.Local)
					            utcTo = utcTo.ToUniversalTime();
					        var res = History_Constant1.ConvertToListOfConstants(_history.Select(constantId, utcFrom, utcTo)); // PocoConstants.tt Line: 230
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select()"]/*' />
					    public static List<History_Constant1> Select([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 246*/) // PocoConstants.tt Line: 246
					    {
					        var res = History_Constant1.ConvertToListOfConstants(_history.Select(constantId)); // PocoConstants.tt Line: 250
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="CleanOlder"]/*' />
					    public static void CleanOlder(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 266*/) // PocoConstants.tt Line: 266
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        _history.CleanOlder(constantId, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Delete"]/*' />
					    public static void Delete(History_Constant1 rec, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 287*/) // PocoConstants.tt Line: 287
					    {
					        _history.Delete(rec.id);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Reset"]/*' />
					    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 305*/) // PocoConstants.tt Line: 305
					    {
					        _history.Reset(constantId);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="History_"]/*' />
					    public struct History_Constant1 // PocoConstants.tt Line: 328
					    {
					        public override string ToString() { return $"{this.Value} on date: {this.DateTime}"; }
					        internal History_Constant1(_history t)
					        {
					            this.id = t.Id;
					            this.DateTime = _history.ToDateTime(t.date_time);
					            if (t.val != null)
					            {
					                this.Value = t.val; // PocoConstants.tt Line: 348
					            } // PocoConstants.tt Line: 350
					            else
					                this.Value = null;
					        }
					        internal static List<History_Constant1> ConvertToListOfConstants(IEnumerable<_history> lst) // PocoConstants.tt Line: 354
					        {
					            var res = new List<History_Constant1>();
					            if (lst != null)
					                foreach (var t in lst)
					                    res.Add(new History_Constant1(t));
					            return res;
					        }
					        public int id;
					        public DateTime DateTime;
					        public string? Value;
					    }
					}
					public class CONSTANT2 // PocoConstants.tt Line: 16
					{
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="GUID"]/*' />
					    public const string GUID = "e1f0ae82-9268-4bc8-96b0-75492411e07e";
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Value"]/*' />
					    public static string? Value { get; private set; } // PocoConstants.tt Line: 25
						internal static Constants.Group1.CONSTANT2.History_Constant2 Create() // PocoConstants.tt Line: 26
						{
							var res = new Constants.Group1.CONSTANT2.History_Constant2
					        {
							    id = GetNextId(_history.T_GUID)
					        };
							return res;
						}
					    private static int _constantId; // PocoConstants.tt Line: 34
					    private static int constantId { get { if (_constantId == 0) { _constantId = _history_objects_ids.GetObjectId(GUID); } return _constantId; }}
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="LoadValue"]/*' />
					    public static void LoadValue(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 52*/) // PocoConstants.tt Line: 52
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        Value = Load(utc); // PocoConstants.tt Line: 63
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Load"]/*' />
					    public static string? Load(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 70*/) // PocoConstants.tt Line: 70
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = null; // PocoConstants.tt Line: 79
					        var h = _history.Load(constantId, utc);
					        if (h != null)
					            res = h; // PocoConstants.tt Line: 96
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Save"]/*' />
					    public static void Save(string val, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 130*/) // PocoConstants.tt Line: 130
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        string? res = val; // PocoConstants.tt Line: 148
					        _history.Save(constantId, res, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectBefore"]/*' />
					    public static List<History_Constant2> SelectBefore(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 174*/) // PocoConstants.tt Line: 174
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant2.ConvertToListOfConstants(_history.SelectBefore(constantId, utc)); // PocoConstants.tt Line: 181
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="SelectAfter"]/*' />
					    public static List<History_Constant2> SelectAfter(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 197*/) // PocoConstants.tt Line: 197
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        var res = History_Constant2.ConvertToListOfConstants(_history.SelectAfter(constantId, utc)); // PocoConstants.tt Line: 204
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select"]/*' />
					    public static List<History_Constant2> Select(DateTime utcFrom, DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 220*/) // PocoConstants.tt Line: 220
					    {
							System.Diagnostics.Debug.Assert(utcFrom.Kind != DateTimeKind.Unspecified);
							System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
					        if (utcFrom.Kind == DateTimeKind.Local)
					            utcFrom = utcFrom.ToUniversalTime();
					        if (utcTo.Kind == DateTimeKind.Local)
					            utcTo = utcTo.ToUniversalTime();
					        var res = History_Constant2.ConvertToListOfConstants(_history.Select(constantId, utcFrom, utcTo)); // PocoConstants.tt Line: 230
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Select()"]/*' />
					    public static List<History_Constant2> Select([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 246*/) // PocoConstants.tt Line: 246
					    {
					        var res = History_Constant2.ConvertToListOfConstants(_history.Select(constantId)); // PocoConstants.tt Line: 250
					        return res;
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="CleanOlder"]/*' />
					    public static void CleanOlder(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 266*/) // PocoConstants.tt Line: 266
					    {
							System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
					        if (utc.Kind == DateTimeKind.Local)
					            utc = utc.ToUniversalTime();
					        _history.CleanOlder(constantId, utc);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Delete"]/*' />
					    public static void Delete(History_Constant2 rec, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 287*/) // PocoConstants.tt Line: 287
					    {
					        _history.Delete(rec.id);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="Reset"]/*' />
					    public static void Reset([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from PocoConstants.tt Line: 305*/) // PocoConstants.tt Line: 305
					    {
					        _history.Reset(constantId);
					    }
					    /// <include file='model_doc.xml' path='Doc/Model/Constants/ConstantGroup/Constant/Desc[@name="History_"]/*' />
					    public struct History_Constant2 // PocoConstants.tt Line: 328
					    {
					        public override string ToString() { return $"{this.Value} on date: {this.DateTime}"; }
					        internal History_Constant2(_history t)
					        {
					            this.id = t.Id;
					            this.DateTime = _history.ToDateTime(t.date_time);
					            if (t.val != null)
					            {
					                this.Value = t.val; // PocoConstants.tt Line: 348
					            } // PocoConstants.tt Line: 350
					            else
					                this.Value = null;
					        }
					        internal static List<History_Constant2> ConvertToListOfConstants(IEnumerable<_history> lst) // PocoConstants.tt Line: 354
					        {
					            var res = new List<History_Constant2>();
					            if (lst != null)
					                foreach (var t in lst)
					                    res.Add(new History_Constant2(t));
					            return res;
					        }
					        public int id;
					        public DateTime DateTime;
					        public string? Value;
					    }
					}
				}
			}
			/// <include file='model_doc.xml' path='Doc/Model/Catalogs/Desc[@name="catalogs"]/*' />
			public partial class Catalogs // Catalogs.tt Line: 8, called from NameSpace.tt Line: 169
			{
				// PocoCatalogs.tt Line: 7, called from Catalogs.tt Line: 13
				[Dapper.Contrib.Extensions.Table("CtlgCatalog1")]
				public partial class Catalog1 : RepoEntityBaseSync<Catalog1>, IEntityBaseExplicit<Catalog1>, ISameById<Catalog1>, IEntityBase // ModelCatalogClass.tt Line: 12, called from PocoCatalogs.tt Line: 10
				{
				    #region ctor // ModelCtor.tt Line: 8, called from ModelCatalogClass.tt Line: 25
				    public IEnumerable<IEntityBase> GetChildren() // ModelCtor.tt Line: 17
				    {
				        return new List<IEntityBase>();
				    }
				    static Catalog1() 
				    { 
				    }
				#if DEBUG
				    private Catalog1() : base("c0")
				#else
				    public Catalog1() : base("c0")
				#endif
				    {
				    }
				    #endregion ctor
				    #region Properties // ModelProperty.tt Line: 8, called from ModelCatalogClass.tt Line: 28
					[Dapper.Contrib.Extensions.Key] // ModelProperty.tt Line: 19 - Utils.cs Line: 279
					public int Id // ModelProperty.tt Line: 19 - Utils.cs Line: 315 Utils.cs Line: 1174
					{
						get { return _Id; } // ModelProperty.tt Line: 19 - Utils.cs Line: 458
						set { _Id = value; ___isNeedUpdate = true;}
					}
					private int _Id; // ModelProperty.tt Line: 19 - Utils.cs Line: 466
					public int Code // ModelProperty.tt Line: 19 - Utils.cs Line: 315 Utils.cs Line: 1174
					{
						get { return _Code; } // ModelProperty.tt Line: 19 - Utils.cs Line: 458
						set { _Code = value; ___isNeedUpdate = true;}
					}
					private int _Code; // ModelProperty.tt Line: 19 - Utils.cs Line: 466
					public string Name // ModelProperty.tt Line: 19 - Utils.cs Line: 315 Utils.cs Line: 1154
					{
						get { return _Name; } // ModelProperty.tt Line: 19 - Utils.cs Line: 458
						set { _Name = value; ___isNeedUpdate = true;}
					}
					private string _Name = string.Empty; // ModelProperty.tt Line: 19 - Utils.cs Line: 466
				
					#region Fields // ModelProperty.tt Line: 21
					public const string F_ID = "Id";
					public const string F_CODE = "Code";
					public const string F_NAME = "Name";
					#endregion Fields // ModelProperty.tt Line: 28
				    #endregion Properties // ModelProperty.tt Line: 29
				    #region Special // ModelProperty.tt Line: 30
					public const string T_GUID = "5cc54978-0abe-4eef-a97a-f48713978731";
					public string GetGuid() { return T_GUID; }
					public const string T_NAME = "CtlgCatalog1";
				    public string GetDbTableName() { return T_NAME; }
				    public bool IsMarkedForDeletion(bool? isMarkedForDeletion = null) { if (isMarkedForDeletion.HasValue) { this.___isMarkedForDeletion = isMarkedForDeletion ?? false; } return this.___isMarkedForDeletion; }
				    private bool ___isMarkedForDeletion = false;
				    public bool SameById(Catalog1 other) { return other != null && this.Id == other.Id; } // ModelProperty.tt Line: 45
				    #endregion Special // ModelProperty.tt Line: 46
					#region Command Definition Data // ModelEntityCmd.tt Line: 9, called from ModelCatalogClass.tt Line: 53
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionInsert() // ModelEntityCmd.tt Line: 32
					{
					    var cmd = new CommandDefinitionData(
							"INSERT INTO CtlgCatalog1 (" + // ModelEntityCmd.tt Line: 48
								"Id"+
								",Code"+
								",Name"+
							") VALUES(" + // ModelEntityCmd.tt Line: 52
								"@Id"+
								",@Code"+
								",@Name"+
							");", // SELECT SCOPE_IDENTITY();", // ModelEntityCmd.tt Line: 56
					        new 
					        {
					    		this.Id, 
					    		this.Code, 
					    		this.Name, 
					        }, CommandType.Text) // ModelEntityCmd.tt Line: 66 
					        { Entity = this }; // ModelEntityCmd.tt Line: 68
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionLoadById(int id) // ModelEntityCmd.tt Line: 71
					{
					    var sql = @"SELECT Id, Code, Name FROM CtlgCatalog1 WHERE Id = @pid;"
					; // ModelEntityCmd.tt Line: 84
					    var cmd = new CommandDefinitionData(sql, new { pid = id }, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionSelect(string? where, object? param, string? sort, 
					    int page, int pagesize) // ModelEntityCmd.tt Line: 90
					{
						var sql = CreateQuery(null, Model.Catalogs.Catalog1.T_NAME, null, where, sort, page, pagesize);
					    var cmd = new CommandDefinitionData(sql, param, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionCountWhere(string? where, object? param) // ModelEntityCmd.tt Line: 104
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("SELECT Count(*) FROM CtlgCatalog1");
						if (where != null)
						{
							sb.Append(" WHERE ");
							sb.Append(where);
						}
						sb.Append(';');
					    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
					    return cmd;
					}
					List<CommandDefinitionData> IEntityBaseExplicit.GetCommandDefinitionSave() // ModelEntityCmd.tt Line: 125
					{
					    Dictionary<string, Dictionary<int, string?>>? dicInsertedUpdatedGuidId = null;
					    var lstCmd = new List<CommandDefinitionData>();
					    CommandDefinitionData cd;
					    bool isCanInsert = true; 
					    if (dicInsertedUpdatedGuidId != null)
					    {
					        if (!dicInsertedUpdatedGuidId.ContainsKey(Model.Catalogs.Catalog1.T_GUID))
					            dicInsertedUpdatedGuidId[Model.Catalogs.Catalog1.T_GUID] = new Dictionary<int, string?>();
					        var dic = dicInsertedUpdatedGuidId[Model.Catalogs.Catalog1.T_GUID];
					        if (dic.ContainsKey(this.Id))
					            isCanInsert = false;
					        else
					            dic[this.Id] = null;
					    }
					    if (this.IsNeedInsert() && isCanInsert)
					    {
					        cd = ((IEntityBaseExplicit)this).GetCommandDefinitionInsert();
					        lstCmd.Add(cd);
					    } 
					    else if (this.IsNeedUpdate())
					    {
					        cd = ((IEntityBaseExplicit)this).GetCommandDefinitionUpdate();
					        lstCmd.Add(cd);
					    }
					    return lstCmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionUpdate() // ModelEntityCmd.tt Line: 183
					{
					    var cmd = new CommandDefinitionData(
					        "UPDATE CtlgCatalog1 SET "+
								"Code = @Code" + 
								",Name = @Name" + 
					        " WHERE Id = @Id;", // ModelEntityCmd.tt Line: 212
							new {
					    		this.Code, 
					    		this.Name, 
								this.Id // ModelEntityCmd.tt Line: 227
							}, CommandType.Text) 
					        { Entity = this };
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionDeleteById(int id) // ModelEntityCmd.tt Line: 233
					{
					    var sql = "DELETE FROM CtlgCatalog1 WHERE Id = @pid;";
					    var cmd = new CommandDefinitionData(sql, new { pid = id }, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionRemoveById(int id) // ModelEntityCmd.tt Line: 244
					{
					    var sql = "" +
					    "DELETE FROM CtlgCatalog1 WHERE Id = @pid;\n"; // ModelEntityCmd.tt Line: 261
					    var cmd = new CommandDefinitionData(sql, new { pid = id }, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionDeleteWhere(string? where, object? param) // ModelEntityCmd.tt Line: 266
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("DELETE FROM CtlgCatalog1");
						if (!string.IsNullOrWhiteSpace(where))
						{
							sb.Append(" WHERE ");
							sb.Append(where);
						}
						sb.Append(';');
					    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionRemoveWhere(string? where, object? param) // ModelEntityCmd.tt Line: 287
					{
						var sb = new StringBuilder();
						if (!string.IsNullOrWhiteSpace(where))
						{
					        sb.Append("DELETE FROM CtlgCatalog1 WHERE "); // ModelEntityCmd.tt Line: 309
					        sb.Append(where);
					    	sb.AppendLine(";");
						}
					    else
					    {
					        sb.AppendLine("DELETE FROM CtlgCatalog1;"); // ModelEntityCmd.tt Line: 318
					    }
					    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
					    return cmd;
					}
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionMoveTo(int id, int idGroupTo) { throw new NotImplementedException(); } // ModelEntityCmd.tt Line: 376
					CommandDefinitionData IEntityBaseExplicit.GetCommandDefinitionLoadSubTree(int id, int deep) { throw new NotImplementedException(); } // ModelEntityCmd.tt Line: 377
					#endregion Command Definition Data // ModelEntityCmd.tt Line: 381
					#region Repository // CatalogRepository.tt Line: 7, called from ModelCatalogClass.tt Line: 66
					// Repository.tt Line: 8, called from CatalogRepository.partial.cs Line: 25
					Catalogs.Catalog1 IEntityBaseExplicit<Catalog1>.CreateDto(int id) // Repository.tt Line: 15
					{
					    var dto = new Catalogs.Catalog1
					    {
					        Id = id,
					        Name = string.Empty,
					        ___isNeedInsert = true,
					    };
					    return dto;
					}
					protected override Catalogs.Catalog1 GetThis() { return this; }
					Catalog1? IEntityBaseExplicit<Catalog1>.LoadUtil(SqlMapper.GridReader multi) // Repository.tt Line: 236
					{
					    Catalogs.Catalog1? resCatalog1 = null;
					    var lstCtlgCatalog1 = new List<Catalogs.Catalog1>();
					    resCatalog1 = multi.Read<Catalogs.Catalog1>().First();
					    lstCtlgCatalog1.Add(resCatalog1);
					    return resCatalog1;
					}
					#endregion Repository // CatalogRepository.tt Line: 51
					#region Views // PocoView.tt Line: 9, called from ModelCatalogClass.tt Line: 82
					[Dapper.Contrib.Extensions.Table("CtlgCatalog1")]
					public partial class ViewListNarrow : ViewEntityBaseSync<ViewListNarrow>, IViewPlainBaseExplicit<ViewListNarrow>, IViewItem, ISameById<ViewListNarrow>  // PocoView.tt Line: 49
					{
					    public bool SameById(ViewListNarrow other) { return other != null && this.Id == other.Id; } // PocoView.tt Line: 51
					    public string GetName() { return this.Name; }
					#if DEBUG
					    private ViewListNarrow()
					#else
					    public ViewListNarrow()
					#endif
					    {
					        this.Name = string.Empty;
					    }
					    #region View Properties
						[Dapper.Contrib.Extensions.Key] // PocoView.tt Line: 103 - Utils.cs Line: 279
						public int Id // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Id; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Id = value; }
						}
						private int _Id; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public int Code // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Code; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Code = value; }
						}
						private int _Code; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public string Name // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1154
						{
							get { return _Name; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Name = value; }
						}
						private string _Name = string.Empty; // PocoView.tt Line: 103 - Utils.cs Line: 466
					    #endregion View Properties
						#region Views // CmdDefinitionsCatalogView.tt Line: 7, called from PocoView.tt Line: 119
						CommandDefinitionData IViewPlainBaseExplicit<ViewListNarrow>.GetCommandDefinitionCountWhere(string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 9
						{
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT Count(*) FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						CommandDefinitionData IViewPlainBaseExplicit<ViewListNarrow>.GetCommandDefinitionView(int pagesize, int page, string? sort, string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 302
						{
						    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0));
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT ");
						    sb.Append("Id, Code, Name FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							if (sort != null)
							{
								sb.Append(" ORDER BY ");
								sb.Append(sort);
							}
							if (page > 0 && pagesize > 0)
							{
								sb.Append(" OFFSET ");
								sb.Append((page-1)*pagesize);
								sb.Append(" ROWS FETCH NEXT ");
								sb.Append(pagesize);
								sb.Append(" ROWS ONLY");
						    }
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						#endregion Views // CmdDefinitionsCatalogView.tt Line: 346
					}
					[Dapper.Contrib.Extensions.Table("CtlgCatalog1")]
					public partial class ViewListWide : ViewEntityBaseSync<ViewListWide>, IViewPlainBaseExplicit<ViewListWide>, IViewItem, ISameById<ViewListWide>  // PocoView.tt Line: 49
					{
					    public bool SameById(ViewListWide other) { return other != null && this.Id == other.Id; } // PocoView.tt Line: 51
					    public string GetName() { return this.Name; }
					#if DEBUG
					    private ViewListWide()
					#else
					    public ViewListWide()
					#endif
					    {
					        this.Name = string.Empty;
					    }
					    #region View Properties
						[Dapper.Contrib.Extensions.Key] // PocoView.tt Line: 103 - Utils.cs Line: 279
						public int Id // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Id; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Id = value; }
						}
						private int _Id; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public int Code // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1174
						{
							get { return _Code; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Code = value; }
						}
						private int _Code; // PocoView.tt Line: 103 - Utils.cs Line: 466
						public string Name // PocoView.tt Line: 103 - Utils.cs Line: 315 Utils.cs Line: 1154
						{
							get { return _Name; } // PocoView.tt Line: 103 - Utils.cs Line: 458
							set { _Name = value; }
						}
						private string _Name = string.Empty; // PocoView.tt Line: 103 - Utils.cs Line: 466
					    #endregion View Properties
						#region Views // CmdDefinitionsCatalogView.tt Line: 7, called from PocoView.tt Line: 119
						CommandDefinitionData IViewPlainBaseExplicit<ViewListWide>.GetCommandDefinitionCountWhere(string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 9
						{
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT Count(*) FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						CommandDefinitionData IViewPlainBaseExplicit<ViewListWide>.GetCommandDefinitionView(int pagesize, int page, string? sort, string? where, object? param) // CmdDefinitionsCatalogView.tt Line: 302
						{
						    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0));
							StringBuilder sb = new StringBuilder();
							sb.Append("SELECT ");
						    sb.Append("Id, Code, Name FROM CtlgCatalog1");
							if (where != null)
							{
								sb.Append(" WHERE ");
								sb.Append(where);
							}
							if (sort != null)
							{
								sb.Append(" ORDER BY ");
								sb.Append(sort);
							}
							if (page > 0 && pagesize > 0)
							{
								sb.Append(" OFFSET ");
								sb.Append((page-1)*pagesize);
								sb.Append(" ROWS FETCH NEXT ");
								sb.Append(pagesize);
								sb.Append(" ROWS ONLY");
						    }
							sb.Append(';');
						    var cmd = new CommandDefinitionData(sb.ToString(), param, CommandType.Text);
						    return cmd;
						}
						#endregion Views // CmdDefinitionsCatalogView.tt Line: 346
					}
					#endregion Views // PocoView.tt Line: 289
				}
			}
			public partial class Documents // Documents.tt Line: 7, called from NameSpace.tt Line: 173
			{
				// PocoDocuments.tt Line: 7, called from Documents.tt Line: 12
			}
			public interface IAttachForUpdates // BaseClasses.tt Line: 8, called from ModelGlobal.tt Line: 9
			{
			    void AttachForUpdates(TransactionOnCommit tx);
			}
			public partial class RepoBase // BaseClasses.tt Line: 12, called from ModelGlobal.tt Line: 9
			{
			    public bool IsNeedInsert(bool? isNeedInsert = null) { if (isNeedInsert.HasValue) { this.___isNeedInsert = isNeedInsert ?? false; } return this.___isNeedInsert; }
			    protected bool ___isNeedInsert = false;
			    public bool IsNeedUpdate(bool? isNeedUpdate = null) { if (isNeedUpdate.HasValue) { this.___isNeedUpdate = isNeedUpdate ?? false; } return this.___isNeedUpdate; }
			    protected bool ___isNeedUpdate = false;
			    public bool IsRemoved(bool? isRemove = null) { if (isRemove.HasValue) { this.___isRemoved = isRemove ?? false; } return this.___isRemoved; }
			    protected bool ___isRemoved = false;
			}
			public partial class RepoBaseSync<T> : RepoBase, // BaseClasses.tt Line: 113, called from ModelGlobal.tt Line: 9
			    IRepository<T>
			    where T : class, IEntityBaseExplicit<T>, ISameById<T>, IEntityBase
			#if !DEBUG
			    , new()
			#endif
			{
			    /// <summary>
			    /// Unique string for current version
			    /// </summary>
				public string TYPE_CACHE_ID { get; private set; }
				public RepoBaseSync(string typeId)
				{
					this.TYPE_CACHE_ID = typeId;
				}
				// BaseRepository.tt Line: 8, called from BaseClasses.tt Line: 131
				#if DEBUG
				protected static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				protected static T instance = new T();
				#endif
				protected virtual T GetThis() { throw new NotImplementedException(); } // BaseRepository.tt Line: 14
				public static T Create([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 44*/) // BaseRepository.tt Line: 44
				{
					var id = GetNextId(instance.GetGuid()); // BaseRepository.tt Line: 48
					var res = instance.CreateDto(id);
				    res.IsNeedInsert(true);
					return res;
				}
				public static IEnumerable<T> Select(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 96*/) // BaseRepository.tt Line: 96
				{
				    var cd = instance.GetCommandDefinitionSelect(where, param, sort, page, pagesize); // BaseRepository.tt Line: 102
				    var lst_dto = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return lst_dto;
				}
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 153*/) // BaseRepository.tt Line: 153
				{
					int res; // BaseRepository.tt Line: 159
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				protected virtual int CountUtil(string? where, object? param) { throw new NotImplementedException(); } // BaseRepository.tt Line: 175
			}
			public partial class RepoEntityBaseSync<T> : RepoBaseSync<T>, // BaseClasses.tt Line: 136, called from ModelGlobal.tt Line: 9
			    IRepositoryEntity<T>, IAttachForUpdates
			    where T : class, IEntityBaseExplicit<T>, ISameById<T>, IEntityBase
			#if !DEBUG
			    , new()
			#endif
			{
				public RepoEntityBaseSync(string typeId) : base(typeId)	{ }
				// BaseRepositoryEntity.tt Line: 8, called from BaseClasses.tt Line: 147
				protected TransactionOnCommit? transactionOnCommit = null;
				public void AttachForUpdates(TransactionOnCommit tx)
				{
				    this.transactionOnCommit = tx;
				}
				public void Save([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 50*/) // BaseRepositoryEntity.tt Line: 50
				{
				    Debug.Assert(this.transactionOnCommit == null, "Entity already attached to transaction on commit");
				    Debug.Assert(!this.IsRemoved());
				    var dft = new TransactionOnCommit();
				    var entity = this.GetThis();
				    dft.Save(entity);
				    dft.Commit();
				}
				public void Remove([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 141*/) // BaseRepositoryEntity.tt Line: 141
				{
				    Debug.Assert(this.transactionOnCommit == null, "Entity already attached to transaction on commit");
				    Debug.Assert(!this.IsNeedInsert());
				    Debug.Assert(!this.IsRemoved());
					Remove(this.GetThis().Id);
				    this.IsRemoved(true);
					foreach (var t in this.GetThis().GetChildren())
						t.IsRemoved(true);
				}
				public static void Remove(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 153*/) // BaseRepositoryEntity.tt Line: 153
				{
				    var cd = instance.GetCommandDefinitionRemoveById(id); // BaseRepositoryEntity.tt Line: 160
				    ConnExecute((conn) => { conn.Execute(cd.GetCommandDefinition()); }, RetryPolicy);
				}
				public static void Remove(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 176*/) // BaseRepositoryEntity.tt Line: 176
				{
				    var cd = instance.GetCommandDefinitionRemoveWhere(where, param); // BaseRepositoryEntity.tt Line: 183
				    ConnExecute((conn) => { conn.Execute(cd.GetCommandDefinition()); }, RetryPolicy);
				}
				protected static bool isEntityWithTabs;
				public static T? Load(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 257*/) // BaseRepositoryEntity.tt Line: 257
				{
				    var cd = instance.GetCommandDefinitionLoadById(id); // BaseRepositoryEntity.tt Line: 264
				    T? dto = null;
				    dto = ConnSelectSingle<T>((conn) =>  // BaseRepositoryEntity.tt Line: 276
				    { 
				        if (isEntityWithTabs) // BaseRepositoryEntity.tt Line: 278
				        {
				            using(var multi = conn.QueryMultiple(cd.GetCommandDefinition()))
				            {
				                return instance.LoadUtil(multi);
				            }
				        }
				        else // BaseRepositoryEntity.tt Line: 285
				        {
				            return conn.Query<T>(cd.GetCommandDefinition()).Single(); 
				        }
				    }, RetryPolicy); // BaseRepositoryEntity.tt Line: 289
					return dto;
				}
			}
			public partial class ViewEntityBaseSync<T> // BaseClasses.tt Line: 152, called from ModelGlobal.tt Line: 9
			    where T : class, IViewPlainBaseExplicit<T>, ISameById<T>
			#if !DEBUG
			    , new()
			#endif
			{
				// BaseView.tt Line: 8, called from BaseClasses.tt Line: 161
				#if DEBUG
				private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				private static T instance = new T();
				#endif
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
				{
					int res; // BaseView.tt Line: 277
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionView(pagesize, page, sort, where, param); // BaseView.tt Line: 301
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
			}
			public partial class ViewTreeBaseSync<T> // BaseClasses.tt Line: 166, called from ModelGlobal.tt Line: 9
			    where T : class, IViewSelfTreeBaseExplicit<T>, ISameById<T>
			#if !DEBUG
			    , new()
			#endif
			{
				// BaseView.tt Line: 8, called from BaseClasses.tt Line: 175
				#if DEBUG
				private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				private static T instance = new T();
				#endif
				public static IEnumerable<T> GetSubTreeView(int? parentId, int deep = 2, string? sort = null, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 101*/) // BaseView.tt Line: 101
				{
				    var cd = instance.GetCommandDefinitionSubTreeView(parentId, deep, sort, where, param); // BaseView.tt Line: 105
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetTreeListView(int? selectedId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 121*/) // BaseView.tt Line: 121
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var res = new List<T>();
				    var cd = instance.GetCommandDefinitionTreeListSubView(selectedId, sort, where, param); // BaseView.tt Line: 130
				    var resItems = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
				    if (selectedId.HasValue)
				    {
				        cd = instance.GetCommandDefinitionTreeListView(selectedId, where, param); // BaseView.tt Line: 136
				        res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy).ToList();
				    }
				    res.AddRange(resItems);
				    if (pagesize > 0)
				        res = res.AsQueryable().Skip((page - 1) * pagesize).Take(pagesize).ToList();
					return res;
				}
				public static IEnumerable<T> GetViewByParentId(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 153*/) // BaseView.tt Line: 153
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionSubItemsView(parentId, pagesize, page, sort, where, param); // BaseView.tt Line: 161
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
				{
					int res; // BaseView.tt Line: 277
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionView(pagesize, page, sort, where, param); // BaseView.tt Line: 301
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
			}
			public partial class ViewDetailBaseSync<T> // BaseClasses.tt Line: 180, called from ModelGlobal.tt Line: 9
			    where T : class, IViewPlainForRefTreeBaseExplicit<T>, ISameById<T>
			#if !DEBUG
			    , new()
			#endif
			{
				// BaseView.tt Line: 8, called from BaseClasses.tt Line: 189
				#if DEBUG
				private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
				#else
				private static T instance = new T();
				#endif
				public static IEnumerable<T> GetViewByParentId(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 202*/) // BaseView.tt Line: 202
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionSubItemsView(parentId, pagesize, page, sort, where, param); // BaseView.tt Line: 210
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
				{
					int res; // BaseView.tt Line: 277
				    var cd = instance.GetCommandDefinitionCountWhere(where, param);
				    res = ConnExecuteScalar<int>((conn) => { return conn.ExecuteScalar<int>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
				public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
				{
				    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
				    var cd = instance.GetCommandDefinitionView(pagesize, page, sort, where, param); // BaseView.tt Line: 301
				    var res = ConnSelectMany<T>((conn) => { return conn.Query<T>(cd.GetCommandDefinition()); }, RetryPolicy);
					return res;
				}
			}
			public class ListChildren<T> : List<T> // ModelGlobal.tt Line: 12, called from NameSpace.tt Line: 176
			    where T : class, IEntityBase
			{
			    /// <summary>
			    /// Init collection of children entities with actions for ADD and REMOVE child.
			    /// Typical action for CREATE is assign ID of created entity.
			    /// For ADD is update reference to parent entity.
			    /// </summary>
			    /// <param name="onCreate">Function to create child</param>
			    /// <param name="onAdd">Action for added child</param>
			    /// <param name="onRemove">Action for removed child</param>
			    public ListChildren(Func<T> onCreate, Action<T> onAdd, Action<T> onRemove) // ModelGlobal.tt Line: 41
			    {
			        this.onCreate = onCreate;
			        this.onAdd = onAdd;
			        this.onRemove = onRemove;
			    }
			    readonly Func<T> onCreate;
			    readonly Action<T> onAdd;
			    readonly Action<T> onRemove;
			    /// <summary>
			    /// Create and add new child instance
			    /// </summary>
			    /// <returns>New created child instance</returns>
			    public T AddNew() // ModelGlobal.tt Line: 71
			    {
			        Debug.Assert(this.onCreate != null);
			        Debug.Assert(this.onAdd != null);
			        var item = onCreate();
			        this.onAdd(item);
			        base.Add(item);
			        return item;
			    }
			    public new void Add(T item) // ModelGlobal.tt Line: 80
			    {
			        Debug.Assert(this.onAdd != null);
			        Debug.Assert(item.Id > 0);
			        this.onAdd(item);
			        base.Add(item);
			    }
			    public new void AddRange(IEnumerable<T> collection) // ModelGlobal.tt Line: 87
			    {
			        Debug.Assert(this.onAdd != null);
			        foreach (var t in collection)
			        {
			            Debug.Assert(t.Id > 0);
			            this.onAdd(t);
			        }
			        base.AddRange(collection);
			    }
			    public new void Clear() // ModelGlobal.tt Line: 97
			    {
			        Debug.Assert(this.onRemove != null);
			        foreach (var t in this)
			        {
			            this.onRemove(t);
			        }
			        base.Clear();
			    }
			    public new void Insert(int index, T item) // ModelGlobal.tt Line: 106
			    {
			        Debug.Assert(this.onAdd != null);
			        Debug.Assert(item.Id > 0);
			        this.onAdd(item);
			        base.Insert(index, item);
			    }
			    public new void InsertRange(int index, IEnumerable<T> collection) // ModelGlobal.tt Line: 113
			    {
			        Debug.Assert(this.onAdd != null);
			        foreach (var t in collection)
			        {
			            Debug.Assert(t.Id > 0);
			            this.onAdd(t);
			        }
			        base.InsertRange(index, collection);
			    }
			    public new void Remove(T item) // ModelGlobal.tt Line: 123
			    {
			        Debug.Assert(this.onRemove != null);
			        this.onRemove(item);
			        base.Remove(item);
			    }
			    public new void RemoveAll(Predicate<T> match) // ModelGlobal.tt Line: 129
			    {
			        Debug.Assert(this.onRemove != null);
			        foreach (var t in this)
			        {
			            this.onRemove(t);
			        }
			        base.RemoveAll(match);
			    }
			    public new void RemoveAt(int index) // ModelGlobal.tt Line: 138
			    {
			        Debug.Assert(this.onRemove != null);
			        this.onRemove(this[index]);
			        base.RemoveAt(index);
			    }
			    public new void RemoveRange(int index, int count) // ModelGlobal.tt Line: 144
			    {
			        Debug.Assert(this.onRemove != null);
			        for (int i = 0; i < count; i++)
			        {
			            this.onRemove(this[i + index]);
			        }
			        base.RemoveRange(index, count);
			    }
			}
			
			
			#region Id Generator
			public class IdGenerator // IdGenerator.tt Line: 8, called from NameSpace.tt Line: 187
			{
			    // connStr, table name, IdItem
			    private static readonly Dictionary<string /* GUID */, string /* table name */> dicTableNames = new()
			    {
			        { "9468ACEC-52F0-4049-9A31-CC1A92F4EA4B", "_history_objects_ids" },
			        { "433FBC18-BAF3-400C-BC77-1D5FC7C43C1F", "_history" },
			        { "a59b9a9d-5627-43d1-92a8-a5c610d52d78", "cnstgroup1constant1" },
			        { "e1f0ae82-9268-4bc8-96b0-75492411e07e", "cnstgroup1constant2" },
			        { "5cc54978-0abe-4eef-a97a-f48713978731", "ctlgcatalog1" },
			        { "2d30325d-5000-4a41-a464-cab89db5f4b5", "ctlgcatalog1folder" },
			    };
			    public static int GetHiLo(string guid, int qtyId, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from IdGenerator.tt Line: 28*/) // IdGenerator.tt Line: 28
			    {
			        var tableName = IdGenerator.dicTableNames[guid];
			        // https://www.npgsql.org/doc/basic-usage.html#stored-functions-and-procedures
			        var p = new DynamicParameters(); // IdGenerator.tt Line: 38
			        p.Add("@gd", guid);
			        p.Add("@tbl", tableName);
			        p.Add("@range", qtyId);
			
			        //p.Add("@id", dbType: System.Data.DbType.Int32, direction: ParameterDirection.Output);
			        //TODO Retry and connection close. SP is not ready?
			        //RepositoryBase.RetryPolicyWithoutTransaction.Execute(() =>
			        //{
			        using var cn = new NpgsqlConnection(Model.ConnectionString);
			        cn.Open();
			        var lst = cn.Query<int>("SELECT _get_id_range(@gd, @tbl, @range)", p,
			            commandTimeout: Model.CommandTimeout,
			            commandType: CommandType.Text
			        );
			        //});
			        Debug.Assert(lst.Count() == 1);
			        var en = lst.GetEnumerator();
			        en.MoveNext();
			        var id = en.Current;
			        return id;
			    }
			}
			public class HiLoService : IHiLoService // ModelCacheId.tt Line: 9, called from NameSpace.tt Line: 190
			{
			    public HiLoResult GetHiLo(HiLoRequest request)
			    {
			        var nextId = IdGenerator.GetHiLo(request.Guid, request.RequestedQty);
			        return new HiLoResult() { NextId = nextId, ReturnedQty = request.RequestedQty };
			    }
			}
			public enum EnumHiType { Fixed, PIDController }; // ModelCacheId.tt Line: 50
			private static ICacheId? cacheId;
			public static void InitCacheId(EnumHiType type, IHiLoService? serv) // ModelCacheId.tt Line: 52
			{
			    if (serv == null)
			        serv = new HiLoService();
			    switch (type)
			    {
			        case EnumHiType.Fixed:
			            Model.cacheId = new CacheIdHiLo(serv);
			            break;
			        case EnumHiType.PIDController:
			            Model.cacheId = new CacheIdHiLoPid(serv);
			            break;
			    }
			}
			public static CacheIdHiLo CacheIdHiLo // ModelCacheId.tt Line: 66
			{
			    get
			    {
			        System.Diagnostics.Debug.Assert(Model.cacheId != null);
			        System.Diagnostics.Debug.Assert(Model.cacheId is CacheIdHiLo);
			        return (CacheIdHiLo)Model.cacheId;
			    }
			}
			public static CacheIdHiLoPid CacheIdHiLoPid // ModelCacheId.tt Line: 75
			{
			    get
			    {
			        System.Diagnostics.Debug.Assert(Model.cacheId != null);
			        System.Diagnostics.Debug.Assert(Model.cacheId is CacheIdHiLoPid);
			        return (CacheIdHiLoPid)Model.cacheId;
			    }
			}
			public static int GetNextId(string guid) // ModelCacheId.tt Line: 85
			{
			    System.Diagnostics.Debug.Assert(Model.cacheId != null);
			    return Model.cacheId.GetNextId(guid);
			}
			#endregion Id Generator
			
			#region History // Model.tt Line: 19, called from NameSpace.tt Line: 194
			[Dapper.Contrib.Extensions.Table("_history_objects_ids")]
			public partial class _history_objects_ids // Model.tt Line: 23
			{
			    [Dapper.Contrib.Extensions.Key]
			    public string object_guid { get { return _object_guid; } set { _object_guid = value; } }
			    private string _object_guid = string.Empty;
			    public int Id { get { return _Id; } set { _Id = value; } }
			    private int _Id;
			
			    public const string T_GUID = "9468ACEC-52F0-4049-9A31-CC1A92F4EA4B";
			    public const string T_NAME = "_history_objects_ids";
			    public const string F_ID = "object_guid";
			    public const string F_OBJECT_ID = "object_id";
			    /// <summary>
			    /// Get object int ID
			    /// </summary>
			    /// <returns>
			    /// int value
			    /// </returns>
			    /// <param name="guid">Object guid</param>
			    public static int GetObjectId(string guid, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 46*/) // Model.tt Line: 46
			    {
			        int res = 0;
			        ConnExecute((conn) =>
			        {
			            var rec = conn.QuerySingleOrDefault<_history_objects_ids>("SELECT * FROM _history_objects_ids WHERE object_guid=@g;",
			                new { g = guid }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            if (rec == null)
			            {
			                rec = new _history_objects_ids
						    {
			                    Id = (int)Model.GetNextId(_history_objects_ids.T_GUID),
			                    object_guid = guid
			                };
			                conn.Execute("INSERT INTO _history_objects_ids (Id,object_guid) VALUES (@i,@g);",
			                    new { i=rec.Id, g=rec.object_guid }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            }
			            res = rec.Id;
			        }, Model.RetryPolicy);
			        return res;
			    }
			}
			[Dapper.Contrib.Extensions.Table("_history")]
			public class _history // Model.tt Line: 122
			{
			    [Dapper.Contrib.Extensions.Key]
				public int Id { get { return _Id; } set { _Id = value; } }
				private int _Id;
				public int object_id { get { return _object_id; } set { _object_id = value; } }
				private int _object_id;
				public int date_time { get { return _date_time; } set { _date_time = value; } }
				private int _date_time;
				public string? val { get { return _val; } set { _val = value; } }
				private string? _val;
			
			    public const string T_GUID = "433FBC18-BAF3-400C-BC77-1D5FC7C43C1F";
			    public const string T_NAME = "_history";
				public const string F_ID = "id";
				public const string F_OBJECT_ID = "object_id";
				public const string F_DATE_TIME = "date_time";
				public const string F_VAL = "val";
			
			    /// <summary>
			    /// Conversion from DateTime to integer format. 
			    /// Accuracy is one minute. Supported year range from 1000 to 3048
			    /// </summary>
			    /// <returns>
			    /// int value of DateTime
			    /// </returns>
			    /// <param name="utc">UTC Date and time in DateTime format</param>
			    public static int FromDateTime(DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 152*/) // Model.tt Line: 152
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        System.Diagnostics.Debug.Assert(utc.Year >= 1000);
			        System.Diagnostics.Debug.Assert(utc.Year < 3040);
			        int res = utc.Year - 1000;
			        res <<= 9;
			        res += utc.DayOfYear;
			        res <<= 5;
			        res += utc.Hour;
			        res <<= 6;
			        res += utc.Minute;
			        System.Diagnostics.Debug.Assert(res > 0);
			        return res;
			    }
			    /// <summary>
			    /// Conversion from integer to DateTime format. 
			    /// Accuracy is one minute. Supported year range from 1000 to 3048
			    /// </summary>
			    /// <returns>
			    /// int value of DateTime
			    /// </returns>
			    /// <param name="dt">Date and time in integer format</param>
			    public static DateTime ToDateTime(int dt, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 179*/) // Model.tt Line: 179
			    {
			        System.Diagnostics.Debug.Assert(dt > 0);
			        int m = 0b_0000_0000_0000_0000_0000_0000_0011_1111;
			        int mm = dt & m;
			        int h = 0b_0000_0000_0000_0000_0000_0000_0001_1111;
			        dt >>= 6;
			        int hh = dt & h;
			        int d = 0b_0000_0000_0000_0000_0000_0001_1111_1111;
			        dt >>= 5;
			        int dd = dt & d;
			        dt >>= 9;
			        int yy = dt + 1000;
			        var res = new DateTime(yy, 1, 1, hh, mm, 0, DateTimeKind.Utc).AddDays(dd - 1);
			        return res;
			    }
			    /// <summary>
			    /// Get constant history string value
			    /// </summary>
			    /// <returns>
			    /// string value
			    /// </returns>
			    /// <param name="constantId">Constant ID</param>
			    /// <param name="utc">UTC date and time</param>
			    public static string? Load(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 207*/) // Model.tt Line: 207
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        string? res = null;
			        ConnExecute((conn) =>
			        {
			            var h = conn.QuerySingleOrDefault<_history>(
			                "SELECT * FROM _history WHERE object_id=@i  AND date_time<@d ORDER BY date_time DESC FETCH FIRST 1 ROWS ONLY;", 
			                new { i = constantId, d = _history.FromDateTime(utc) },
						    commandTimeout: Model.CommandTimeout, commandType: CommandType.Text
						);
			            if (h != null) res = h.val;
			        }, Model.RetryPolicy);
			        return res;
			    }
			    /// <summary>
			    /// Save constant string value in history
			    /// </summary>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="val">New value for constant.</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static void Save(int constantId, string? val, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 339*/) // Model.tt Line: 339
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc);
			        ConnExecute((conn) =>
			        {
			            var lst = conn.Query<_history>("SELECT * FROM _history WHERE " +
			                "object_id=@i AND date_time=@d",
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text).ToList();
			            if (lst.Count == 1)
			            {
			                var rec = lst[0];
			                rec.val = val;
			                conn.Execute("UPDATE _history SET val=@v WHERE Id=@i;",
			                    new { v = val, i = rec.Id }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            }
			            else if (lst.Count == 0)
			            {
			                var rec = new _history
			                {
			                    Id = (int)Model.GetNextId(_history.T_GUID),
			                    date_time = dt,
			                    val = val
			                };
			                conn.Execute("INSERT INTO _history (Id,object_id,date_time,val) VALUES (@i,@o,@d,@v);",
			                    new { i = rec.Id, o = constantId, d = dt, v = val }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			            }
			            else
			            {
			                System.Diagnostics.Debug.Assert(false);
			            }
			        }, Model.RetryPolicy);
			    }
			    /// <summary>
			    /// Select list constant history records before utc date and time.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static IEnumerable<_history> SelectBefore(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 446*/) // Model.tt Line: 446
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc);
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i AND date_time<@d ORDER BY date_time DESC;",
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Select list constant history records after utc date and time.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static IEnumerable<_history> SelectAfter(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 512*/) // Model.tt Line: 512
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc);
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i AND date_time>@d ORDER BY date_time DESC;",
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Select list constant history records for period.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utcTo">UTC DateTime</param>
			    /// <param name="utcFrom">UTC DateTime</param>
			    public static IEnumerable<_history> Select(int constantId, DateTime utcFrom, DateTime utcTo, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 579*/) // Model.tt Line: 579
			    {
					System.Diagnostics.Debug.Assert(utcFrom.Kind != DateTimeKind.Unspecified);
					System.Diagnostics.Debug.Assert(utcTo.Kind != DateTimeKind.Unspecified);
			        if (utcFrom.Kind == DateTimeKind.Local)
			            utcFrom = utcFrom.ToUniversalTime();
			        if (utcTo.Kind == DateTimeKind.Local)
			            utcTo = utcTo.ToUniversalTime();
			        var dt = _history.FromDateTime(utcFrom);
			        var dt2 = _history.FromDateTime(utcTo);
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i AND date_time>@d AND date_time<@t ORDER BY date_time DESC;",
			                new { i = constantId, d = dt, t = dt2 }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Select list constant all history records.
			    /// </summary>
			    /// <returns>Return list of '_history' objects</returns>
			    /// <param name="constantId">Constant int ID</param>
			    public static IEnumerable<_history> Select(int constantId, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 653*/) // Model.tt Line: 653
			    {
			        IEnumerable<_history>? lst = null;
			        ConnExecute((conn) =>
			        {
			            lst = conn.Query<_history>("SELECT * FROM _history WHERE object_id=@i ORDER BY date_time DESC;",
			                new { i = constantId }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			        System.Diagnostics.Debug.Assert(lst != null);
			        return lst;
			    }
			    /// <summary>
			    /// Clean history records older utc.
			    /// Arter cleaning only one record older utc will be kept.
			    /// </summary>
			    /// <param name="constantId">Constant int ID</param>
			    /// <param name="utc">UTC DateTime</param>
			    public static void CleanOlder(int constantId, DateTime utc, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 710*/) // Model.tt Line: 710
			    {
					System.Diagnostics.Debug.Assert(utc.Kind != DateTimeKind.Unspecified);
			        if (utc.Kind == DateTimeKind.Local)
			            utc = utc.ToUniversalTime();
			        var dt = _history.FromDateTime(utc) + 1;
			        ConnExecute((conn) => // Model.tt Line: 719
			        {
			            var dt2 = conn.ExecuteScalar("SELECT date_time FROM _history WHERE object_id=@i AND date_time<@d ORDER BY date_time DESC FETCH FIRST 1 ROWS ONLY;", 
			                new { i = constantId, d = dt }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text); // Model.tt Line: 748
			            if (dt2 != null)
			                conn.Execute("DELETE FROM _history WHERE object_id=@i AND date_time<@d;", 
			                    new { i = constantId, d = dt2 }, commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			    }
			    /// <summary>
			    /// Remove history record
			    /// </summary>
			    public static void Delete(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 861*/) // Model.tt Line: 861
			    {
			        ConnExecute((conn) =>
			        {
			            conn.Execute("DELETE FROM _history WHERE id=@i;", new { i = id }, 
			                commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			    }
			    /// <summary>
			    /// Remove all history records for constant
			    /// </summary>
			    public static void Reset(int constantId, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 907*/) // Model.tt Line: 907
			    {
			        ConnExecute((conn) =>
			        {
			            conn.Execute("DELETE FROM _history WHERE object_id=@i;", new { i = constantId }, 
			                commandTimeout: Model.CommandTimeout, commandType: CommandType.Text);
			        }, Model.RetryPolicy);
			    }
			}
			#endregion History
			public partial class UnitOfWorkBase : IUnitOfWork // UnitOfWork.tt Line: 10, called from NameSpace.tt Line: 197
			{
			    // TODO https://docs.microsoft.com/en-us/dotnet/api/system.transactions?view=dotnet-plat-ext-3.1
			    // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.begintransaction?view=dotnet-plat-ext-3.1
			    #region General // UnitOfWork.tt Line: 15
				private bool _disposed;
			    protected readonly List<IEntityBase> listResetFlags = new();
			    public System.Data.Common.DbConnection Connection { get; private set; }
			    public System.Data.Common.DbTransaction Transaction { get; private set; }
				public UnitOfWorkBase() // UnitOfWork.tt Line: 25
				{
			        this.Connection = new NpgsqlConnection(Model.ConnectionString);
			        this.Connection.Open();
			        this.Transaction = this.Connection.BeginTransaction();
				}
			    private Exception? exTransaction = null;
				public void Commit() // UnitOfWork.tt Line: 42
				{
					try
					{
			            this.Transaction.Commit();
					}
					catch(Exception ex)
					{
			            exTransaction = ex;
			            Transaction.Rollback();
						throw;
					}
					finally
					{
			            if (exTransaction != null)
			            {
			                Debug.Assert(false);
			            }
			            else
			            {
			                foreach(var t in listResetFlags)
			                {
			                    t.IsNeedInsert(false);
			                    t.IsNeedUpdate(false);
			                }
			            }
			            Transaction.Dispose();
			            Transaction = Connection.BeginTransaction();
					}
				}
				public void Rollback() // UnitOfWork.tt Line: 75
				{
			        Transaction.Rollback();
				}
				public void Dispose() // UnitOfWork.tt Line: 80
				{
					if (Connection.State != ConnectionState.Closed)
			            Connection.Close();
					dispose(true);
					GC.SuppressFinalize(this);
				}
				private void dispose(bool disposing)
				{
					if (!_disposed)
					{
						if(disposing)
						{
							if (Transaction != null)
							{
			                    Transaction.Dispose();
							}
							if(Connection != null)
							{
			                    Connection.Dispose();
							}
						}
						_disposed = true;
					}
				}
				~UnitOfWorkBase()
				{
					dispose(false);
				}
			    #endregion General // UnitOfWork.tt Line: 114
			    public long Insert<T>(T entityToInsert, int? commandTimeout = null) where T : class
			    {
			        return this.Connection.Insert<T>(entityToInsert, this.Transaction, commandTimeout);
			    }
			    public bool Update<T>(T entityToUpdate, int? commandTimeout = null) where T : class
			    {
			        return this.Connection.Update<T>(entityToUpdate, this.Transaction, commandTimeout);
			    }
			    public bool Delete<T>(T entityToDelete, int? commandTimeout = null) where T : class
			    {
			        return this.Connection.Delete<T>(entityToDelete, this.Transaction, commandTimeout);
			    }
			    public int Execute(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
			    {
			        return this.Connection.Execute(sql, param, this.Transaction, commandTimeout, commandType);
			    }
			}
			public partial class UnitOfWork : UnitOfWorkBase // UnitOfWork.tt Line: 141
			{
				// UnitOfWorkRepository.tt Line: 9, called from UnitOfWork.tt Line: 144
				/// <summary>
				/// Insert record of entity in a DB (without tab records)
				/// </summary>
				/// <returns>
				/// Return ID of inserted record
				/// </returns>
				public int Insert(IEntityBase entity) // UnitOfWorkRepository.tt Line: 19
				{
				    System.Diagnostics.Debug.Assert(entity.IsNeedInsert());
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionInsert(); // UnitOfWorkRepository.tt Line: 25
				    this.listResetFlags.Add(entity);
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
					return entity.Id;
				}
				/// <summary>
				/// Load entity full object (with all tab objects) by ID
				/// </summary>
				/// <returns>
				/// Return entity full object with all tab objects
				/// </returns>
				public T Load<T>(int id) // UnitOfWorkRepository.tt Line: 41
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionLoadById(id); // UnitOfWorkRepository.tt Line: 48
				    return this.Connection.Query<T>(cd.GetCommandDefinition(this.Transaction)).Single();
				}
				/// <summary>
				/// Select records from DB and map them to entity objects
				/// </summary>
				/// <returns>
				/// IEnumerable collection of entity objects
				/// Use string.Empty for 'where' parameter, if you need select all records 
				/// </returns>
				/// <example>
				/// <code>
				/// // select all
				/// var lst = Catalogs.Catalog1.Select(null); 
				/// // select up to 25 objets for second page
				/// lst = Catalogs.Catalog1.Select(null, null, 2, 25); 
				/// // select one record
				/// int id = 7;
				/// lst = Catalogs.Catalog1.Select(Catalogs.Catalog1.F_ID+"=@pid", new { pid = id }); 
				/// </code>
				/// </example>    /// <param name="where">A SQL text for WHERE clause.</param>
				/// <param name="param">An array of parameters.</param>
				/// <param name="sort">An array of DB field names for sorting.</param>
				/// <param name="page">Page number (starting with 1).</param>
				/// <param name="pagesize">Page size. Unlimited if equal zero.</param>
				public IEnumerable<T> Select<T>(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0) // UnitOfWorkRepository.tt Line: 78
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionSelect(where, param, sort, page, pagesize); // UnitOfWorkRepository.tt Line: 85
				    return this.Connection.Query<T>(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Select records from DB and map them to entity objects
				/// Use string.Empty for 'where' parameter, if you need select all records 
				/// </summary>
				/// <returns>
				/// IReadOnlyList collection of entity objects
				/// </returns>
				/// See <see cref="Select(string,object,string,int,int)"/> for sample.
				public IReadOnlyList<T> SelectList<T>(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0) // UnitOfWorkRepository.tt Line: 101
				    where T : IEntityBase, new()
				{
					IEnumerable<T> lst = Select<T>(where, param, sort, page, pagesize); // UnitOfWorkRepository.tt Line: 107
					List<T> res = new List<T>();
					foreach(var t in lst) { res.Add(t);	}
					return res;
				}
				/// <summary>
				/// Count selected records from DB
				/// </summary>
				/// <returns>
				/// Return type is 'int?'
				/// </returns>
				/// See <see cref="Select(string,object,string,int,int)"/> for sample.
				public int? Count<T>(string? where, object? param = null) // UnitOfWorkRepository.tt Line: 124
				    where T : IEntityBase, new()
				{
					int? res = null; // UnitOfWorkRepository.tt Line: 130
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionCountWhere(where, param); // UnitOfWorkRepository.tt Line: 132
				    this.Connection.ExecuteScalar<int?>(cd.GetCommandDefinition(this.Transaction));
					return res;
				}
				/// <summary>
				/// Update entity record in DB (without tab records)
				/// </summary>
				public void Update<T>(T entity) // UnitOfWorkRepository.tt Line: 144
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsNeedInsert());
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionUpdate(); // UnitOfWorkRepository.tt Line: 152
				    this.listResetFlags.Add(entity);
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Save entity in DB (with tabs)
				/// </summary>
				/// <returns>
				/// Return ID of inserted object
				/// </returns>
				public void Save<T>(T entity) // UnitOfWorkRepository.tt Line: 167
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    var lstCmd = ((IEntityBaseExplicit)entity).GetCommandDefinitionSave(); // UnitOfWorkRepository.tt Line: 174
				    foreach(var d in lstCmd)
				    {
				        System.Diagnostics.Debug.Assert(d.Entity != null);
				        System.Diagnostics.Debug.Assert(!d.Entity.IsRemoved());
				        this.listResetFlags.Add(d.Entity);
				        this.Connection.Execute(d.GetCommandDefinition(this.Transaction));
				    }
				}
				/// <summary>
				/// Delete entity record in DB (without tabs records)
				/// </summary>
				public void Delete<T>(T entity) // UnitOfWorkRepository.tt Line: 191
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    System.Diagnostics.Debug.Assert(!entity.IsNeedInsert());
					var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionDeleteById(entity.Id);
					this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Delete entity record in DB (without tabs records)
				/// </summary>
				public void Delete<T>(int id) // UnitOfWorkRepository.tt Line: 202
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionDeleteById(id); // UnitOfWorkRepository.tt Line: 209
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Delete entity objects in DB (without tabs records)
				/// Use string.Empty for 'where' parameter, if you need delete all records 
				/// </summary>
				public void Delete<T>(string? where, object? param = null) // UnitOfWorkRepository.tt Line: 221
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionDeleteWhere(where, param); // UnitOfWorkRepository.tt Line: 228
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Remove entity in DB (with tabs records)
				/// </summary>
				public void Remove<T>(T entity) // UnitOfWorkRepository.tt Line: 239
				    where T : IEntityBase
				{
				    System.Diagnostics.Debug.Assert(!entity.IsRemoved());
				    System.Diagnostics.Debug.Assert(!entity.IsNeedInsert());
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionRemoveById(entity.Id); // UnitOfWorkRepository.tt Line: 244
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Remove entity by ID in DB (with tabs records)
				/// </summary>
				public void Remove<T>(int id) // UnitOfWorkRepository.tt Line: 250
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionRemoveById(id); // UnitOfWorkRepository.tt Line: 257
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
				/// <summary>
				/// Remove selected entity records in DB (with tabs records)
				/// Use string.Empty for 'where' parameter, if you need delete all records 
				/// </summary>
				public void Remove<T>(string? where, object? param = null) // UnitOfWorkRepository.tt Line: 269
				    where T : IEntityBase, new()
				{
				    var entity = new T();
				    var cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionRemoveWhere(where, param); // UnitOfWorkRepository.tt Line: 276
				    this.Connection.Execute(cd.GetCommandDefinition(this.Transaction));
				}
			}
			public partial class TransactionOnCommit // DeferredTransaction.tt Line: 9
			{
			    #region General // DeferredTransaction.tt Line: 12 
			    private readonly List<Op> ListOperations;
			    private readonly Dictionary<string, Dictionary<int, string?>> dicInsertedUpdatedGuidId;
				public TransactionOnCommit() // DeferredTransaction.tt Line: 18
				{
			        this.ListOperations = new List<Op>();
			        this.dicInsertedUpdatedGuidId = new Dictionary<string, Dictionary<int, string?>>();
				}
			    private System.Data.Common.DbTransaction? transaction;
			    public void Commit() // DeferredTransaction.tt Line: 35 
			    {
			        ConnExecute((conn) =>
			        {
						var curr_op = default(Op);
			            try
			            {
			                conn.Open();
			                this.transaction = conn.BeginTransaction();
			                foreach (var op in this.ListOperations)
			                {
			                    var cd = op.Cd;
			                    var cmd = new CommandDefinition(cd.CommandText, cd.Parameters, transaction, CommandTimeout, cd.CommandType);
						        curr_op = op;
			                    conn.Execute(cmd);
			                }
			                transaction.Commit();
			                curr_op = null;
					        foreach(var t in this.ListOperations)
					        {
			                    if (t.Entity == null)
			                        continue;
					            if (t.OpType == Op.EnumOpType.Delete)
					            {
					                t.Entity.IsRemoved(true);
					            }
					            else if (t.OpType == Op.EnumOpType.Remove)
							    {
							        t.Entity.IsRemoved(true);
							        foreach(var tt in t.Entity.GetChildren())
							        {
							            tt.IsRemoved(true);
							        }
							    }
					            else if (t.OpType == Op.EnumOpType.Sql) { }
					            else
					            {
					                t.Entity.IsNeedInsert(false);
					                t.Entity.IsNeedUpdate(false);
					            }
					        }
			                this.ListOperations.Clear();
			            }
			            catch (Exception ex)
			            {
			                if (transaction != null)
			                {
			                    transaction.Rollback();
			                }
			                if (curr_op != null)
			                    throw new Exception($"{ex.Message}  File:{curr_op.File}, Line:{curr_op.Line}, Member:{curr_op.Member}. See inner exception.", ex);
			                else
			                    throw;
			            }
			            finally
			            {
			                transaction?.Dispose();
			                if (conn.State != ConnectionState.Closed)
			                    conn.Close();
			            }
			        }, RetryPolicy);
			    }
			    //TODO commandTimeout from this
			    public void AddCommand(string commandText, object? parameters = null,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 250 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 250
			    {
			        var cd = new CommandDefinitionData(commandText, parameters, CommandType.Text);
			        this.ListOperations.Add(new Op(cd, file, member, line));
			    }
			    /// <summary>
			    /// When entity child collections are changed, model will generate operation to insert or remove child records from DB.
			    /// If entity is attached to transaction on commit, appropriate commands will be added to this transaction instead of immediate execution. 
			    /// </summary>
			    public void AttachForUpdates(IAttachForUpdates entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 277 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 277
			    {
			        entity.AttachForUpdates(this);
			    }
			    /// <summary>
			    /// Insert record in DB on Commit() call (without tab records)
			    /// </summary>
			    public void Insert(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 287 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 287
			    {
					Debug.Assert(!entity.IsRemoved());
					Debug.Assert(entity.IsNeedInsert());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Insert, file, member, line));
			    }
			    /// <summary>
			    /// Update record in DB on Commit() call (without tab records)
			    /// </summary>
			    public void Update(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 299 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 299
			    {
					Debug.Assert(!entity.IsRemoved());
					Debug.Assert(entity.IsNeedUpdate());
					Debug.Assert(!entity.IsNeedInsert());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Update, file, member, line));
			    }
			    /// <summary>
			    /// Delete record in DB on Commit() call (without tab records)
			    /// </summary>
			    public void Delete(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 312 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 312
			    {
					Debug.Assert(!entity.IsRemoved());
					Debug.Assert(!entity.IsNeedInsert());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Delete, file, member, line));
			    }
			    /// <summary>
			    /// Remove object in DB on Commit() call (with tab records)
			    /// </summary>
			    public void Remove(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 324 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 324
			    {
					Debug.Assert(!entity.IsRemoved());
					this.ListOperations.Add(new Op(entity, Op.EnumOpType.Remove, file, member, line));
			    }
			    /// <summary>
			    /// Insert or Update object in DB on Commit() call (with Insert or Update tab records)
			    /// </summary>
			    public void Save(IEntityBase entity,
			                [CallerFilePath] string file = "",
			                [CallerMemberName] string member = "",
			                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 335 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 262
			    {
			        Debug.Assert(!entity.IsRemoved());
			        if (entity.IsNeedInsert())
			            this.ListOperations.Add(new Op(entity, Op.EnumOpType.Insert, file, member, line));
			        else if (entity.IsNeedUpdate())
			            this.ListOperations.Add(new Op(entity, Op.EnumOpType.Update, file, member, line));
			        foreach (var t in entity.GetChildren())
			        {
			            if (t.IsNeedInsert())
			                this.ListOperations.Add(new Op(t, Op.EnumOpType.Insert, file, member, line));
			            else if (t.IsNeedUpdate())
			                this.ListOperations.Add(new Op(t, Op.EnumOpType.Update, file, member, line));
			        }
			    }
			    #endregion General // DeferredTransaction.tt Line: 350 
			}
			
			#region Connection // Connection.tt Line: 9, called from NameSpace.tt Line: 203
			public static bool IsBuffered = true;
			public static int? CommandTimeout = 100; // seconds
			
			public static Policy RetryPolicy = Policy // Connection.tt Line: 17
			    .Handle<Exception>(ex => false)
			    // deadlock only
			    // timeout excluded  || ex.Number == -2
			    // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlerror.number?redirectedfrom=MSDN&view=dotnet-plat-ext-5.0#System_Data_SqlClient_SqlError_Number
			    // https://docs.microsoft.com/en-ca/sql/connect/ado-net/step-4-connect-resiliently-to-sql-with-ado-net?view=sql-server-2017
			    // https://docs.microsoft.com/en-us/azure/sql-database/sql-database-develop-error-messages
			    // https://blog.sqlxdetails.com/commandtimeout-how-to-handle-it-properly/
			    // https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-error-severities?view=sql-server-2014
			    // https://stackoverflow.com/questions/24041062/know-when-to-retry-or-fail-when-calling-sql-server-from-c
			    // https://www.codeproject.com/Articles/42547/SQL-SERVER-How-To-Handle-Deadlock
			    .WaitAndRetry(new[]
			    {
			        TimeSpan.FromSeconds(5),
			        TimeSpan.FromSeconds(10),
			        TimeSpan.FromSeconds(20)
			    }, (exception, timeSpan) =>
			    {
			        if (exception.InnerException != null)
			        {
			        }
			    });
			public static Polly.Retry.AsyncRetryPolicy RetryPolicyAsync = Policy // Connection.tt Line: 41
			    .Handle<Exception>(ex => false)
			    .WaitAndRetryAsync(new[]
			    {
			        TimeSpan.FromSeconds(5),
			        TimeSpan.FromSeconds(10),
			        TimeSpan.FromSeconds(20)
			    }, (exception, timeSpan) =>
			    {
			        if (exception.InnerException != null)
			        {
			        }
			    });
			private static string? ConnectionString // Connection.tt Line: 160
			{
			    get
			    {
			        if (__connString == null)
			        {
			            __connString = ConfigurationManagerJson.GetConnectionString("PostgreSql");
			        }
			        return __connString;
			    }
			}
			 
			private static string? __connString;
			#endregion Connection // Connection.tt Line: 173
			
			#region Connection Wrapper // ConnectionWrapper.tt Line: 8, called from NameSpace.tt Line: 206
			/// <summary>
			/// Execute any methods by using open db connection. Expecting return IEnumerable collection of T objects or null.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <typeparam name="T">Any type for appropriate database table. For example from Catalogs class</typeparam>
			/// <param name="onConnection">function which accept DbConnection and return IEnumerable collection of T objects or null </param>
			/// <returns>IEnumerable collection of T objects or null</returns>
			/// <example>
			/// <code>
			/// var lst2 = Db.ConnSelectMany((conn) => { return conn.GetAll&lt;Catalogs.Simple&gt;(); });
			/// </code>
			/// </example>    
			public static IEnumerable<T> ConnSelectMany<T>(Func<System.Data.Common.DbConnection, IEnumerable<T>> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 48
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    IEnumerable<T> res = new List<T>();
			    policy.Execute(() =>
			    {
			        using var conn = new NpgsqlConnection(Model.ConnectionString);
			        res = onConnection(conn);
			    });
			    return res;
			}
			/// <summary>
			/// Execute any methods by using open db connection. Expecting return one T objects or null.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <typeparam name="T">Any type for appropriate database table. For example from Catalogs class</typeparam>
			/// <param name="onConnection">function which accept DbConnection and return one T object or null </param>
			/// <returns>one T object or null</returns>
			public static T? ConnSelectSingle<T>(Func<System.Data.Common.DbConnection, T?> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 95
			    where T : class
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    T? res = null;
			    policy.Execute(() =>
			    {
			        using var conn = new NpgsqlConnection(Model.ConnectionString);
			        res = onConnection(conn);
			    });
			    return res;
			}
			/// <summary>
			/// Execute any methods by using open db connection.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <param name="onConnection">action which accept DbConnection</param>
			public static void ConnExecute(Action<System.Data.Common.DbConnection> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 137
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    policy.Execute(() =>
			    {
			        using var conn = new NpgsqlConnection(Model.ConnectionString);
			        onConnection(conn);
			    });
			}
			/// <summary>
			/// Execute any methods returning scalar value by using open db connection.
			/// Execution will proceed with retry policy
			/// </summary>
			/// <param name="onConnection">function which accept DbConnection</param>
			/// <returns>one T object (int or long)</returns>
			public static T? ConnExecuteScalar<T>(Func<System.Data.Common.DbConnection, T> onConnection, Policy? policy = null) // ConnectionWrapper.tt Line: 180
			{
			    if (policy == null) policy = Model.RetryPolicy;
			    T? res = default;
			    policy.Execute(() =>
			    {
			        using var conn = new NpgsqlConnection(Model.ConnectionString);
			        res = onConnection(conn);
			    });
			    return res;
			}
			#endregion Connection Wrapper // ConnectionWrapper.tt Line: 199
			
			#region Dapper // ModelDapper.tt Line: 9, called from NameSpace.tt Line: 209
			public static void DapperInit() // ModelDapper.tt Line: 10
			{
			    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			
			
			    //SqlMapper.AddTypeHandler(typeof(DateTime), DateTimeHandler.Default);
			    //SqlMapper.AddTypeHandler(typeof(DateTime?), DateTimeHandler.Default);
			    //SqlMapper.AddTypeHandler(typeof(DateTimeOffset), DateTimeOffsetHandler.Default);
			    //SqlMapper.AddTypeHandler(typeof(DateTimeOffset?), DateTimeOffsetHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(DateOnly), DateOnlyHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(DateOnly?), DateOnlyHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(TimeOnly), TimeOnlyHandler.Default);
			    SqlMapper.AddTypeHandler(typeof(TimeOnly?), TimeOnlyHandler.Default);
			}
			/*public class DateTimeHandler : Dapper.SqlMapper.TypeHandler<DateTime> // ModelDapper.tt Line: 58
			{
			    private DateTimeHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new DateTimeHandler();
			    public override DateTime Parse(object value)
			    {
			        //if (value is string res)
			        //    return DateTime.Parse(res);
			        //else 
			        if (value is DateTime dt)
			        {
			            if (dt.Kind == DateTimeKind.Utc)
			                return dt;
			            if (dt.Kind == DateTimeKind.Local)
			                return dt.ToUniversalTime();
			        }
			        throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, DateTime value)
			    {
			        parameter.DbType = DbType.DateTime2;
			        parameter.Value = value;
			    }
			}*/
			/*public class DateTimeOffsetHandler : Dapper.SqlMapper.TypeHandler<DateTimeOffset> // ModelDapper.tt Line: 82
			{
			    private DateTimeOffsetHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new DateTimeOffsetHandler();
			    public override DateTimeOffset Parse(object value)
			    {
			        if (value is string res)
			            return DateTimeOffset.Parse(res);
			        else if (value is DateTimeOffset df)
			            return df;
			        else if (value is DateTime dt)
			            return new DateTimeOffset(dt);
			        else
			            throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
			    {
			        parameter.DbType = DbType.DateTimeOffset;
			        parameter.Value = value;
			    }
			}*/
			public class DateOnlyHandler : Dapper.SqlMapper.TypeHandler<DateOnly> // ModelDapper.tt Line: 103
			{
			    private DateOnlyHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new DateOnlyHandler();
			    public override DateOnly Parse(object value)
			    {
			        if (value is string str)
			        {
			            // https://docs.microsoft.com/en-us/dotnet/api/system.datetime.parse?view=net-5.0
			            DateTime d = DateTime.Parse(str);
			            return new DateOnly(d.Year, d.Month, d.Day);
			        }
			        else if (value is DateTime d)
			        {
			            return new DateOnly(d.Year, d.Month, d.Day);
			        }
			        else
			            throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, DateOnly value)
			    {
			        parameter.DbType = DbType.DateTime;
			        parameter.Value = new DateTime(value.Year, value.Month, value.Day);
			    }
			}
			public class TimeOnlyHandler : Dapper.SqlMapper.TypeHandler<TimeOnly> // ModelDapper.tt Line: 128
			{
			    private TimeOnlyHandler() { }
			    public static readonly SqlMapper.ITypeHandler Default = new TimeOnlyHandler();
			    public override TimeOnly Parse(object value)
			    {
			        if (value is string str)
			        {
			            var d = TimeSpan.Parse(str);
			            return new TimeOnly(d.Hours, d.Minutes, d.Seconds, d.Milliseconds);
			        }
			        else if (value is TimeSpan d)
			        {
			            return new TimeOnly(d.Hours, d.Minutes, d.Seconds, d.Milliseconds);
			        }
			        else
			            throw new NotImplementedException();
			    }
			    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
			    {
			        parameter.DbType = DbType.Time;
			        parameter.Value = new TimeSpan(0, value.Hour, value.Minute, value.Second, value.Millisecond);
			    }
			}
			private static string CreateQuery(string? selectFrom, string? table, string? where1 = null, string? where2 = null,
			    string? sort = null, int page = 0, int pagesize = 0) // ModelDapper.tt Line: 153
			{
			    System.Diagnostics.Debug.Assert(!(selectFrom == null && table == null));
			    StringBuilder sb = new();
			    if (!string.IsNullOrWhiteSpace(selectFrom))
			    {
			        sb.Append(selectFrom);
			    }
			    else
			    {
			        sb.Append("SELECT * FROM ");
			        sb.Append(table);
			        sb.Append("");
			    }
			    sb.Append(' ');
				if (!string.IsNullOrWhiteSpace(where1) || !string.IsNullOrWhiteSpace(where2))
				{
					sb.Append(" WHERE ");
				}
			    bool btw = !string.IsNullOrWhiteSpace(where1) && !string.IsNullOrWhiteSpace(where2);
				if (!string.IsNullOrWhiteSpace(where1))
				{
				    if (btw)
					    sb.Append('(');
					sb.Append(where1);
				    if (btw)
					    sb.Append(')');
				    if (where2 != null)
				    {
					    sb.Append(" AND ");
				    }
				}
				if (!string.IsNullOrWhiteSpace(where2))
				{
				    if (btw)
					    sb.Append('(');
					sb.Append(where2);
				    if (btw)
					    sb.Append(')');
				}
				if (pagesize > 0 && string.IsNullOrWhiteSpace(sort))
					throw new Exception("To use paging sort parameter has to be provided");
				if (!string.IsNullOrWhiteSpace(sort))
				{
					sb.Append(" ORDER BY ");
					sb.Append(sort);
				}
				if (page > 0 && pagesize > 0)
				{
					sb.Append(" OFFSET ");
			        sb.Append((page - 1) * pagesize);
					sb.Append(" ROWS FETCH NEXT ");
					sb.Append(pagesize);
					sb.Append(" ROWS ONLY");
				}
				sb.Append(';');
			    return sb.ToString();
			}
			#endregion Dapper
			
			#region Utils // Utils.tt Line: 8, called from NameSpace.tt Line: 212
			// https://stackoverflow.com/questions/46940710/getting-value-from-appsettings-json-in-net-core
			public static class ConfigurationManagerJson // Utils.tt Line: 10
			{
			    public static IConfiguration AppSetting { get; }
			    static ConfigurationManagerJson()
			    {
			        AppSetting = new ConfigurationBuilder()
			                .SetBasePath(Directory.GetCurrentDirectory())
			                .AddJsonFile("app-settings.json")
			                .Build();
			    }
			    public static string GetConnectionString(string connStrName)
			    {
			        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?view=dotnet-plat-ext-3.1
			        var connString = ConfigurationManagerJson.AppSetting[$"db_conns:{connStrName}:connection_string"];
			        //connPrv = ExtCore.ConfigurationManagerJson.AppSetting["db_conns:" + connStringName + ":provider"];
			        if (connString == null)
			            connString = System.Configuration.ConfigurationManager.ConnectionStrings[connStrName].ConnectionString;
			        return connString;
			    }
			}
			#endregion Utils
			public static ModelSettings Settings { get { if (_Settings == null) _Settings = new ModelSettings(); return _Settings; } }
			private static ModelSettings? _Settings;
			public class ModelSettings // ModelSettings.tt Line: 10
			{
			    public ModelSettings InitDefault()
			    {
			        DapperInit();
			        InitHilo(EnumHiType.Fixed);
			        return this;
			    }
			    public ModelSettings InitHilo(EnumHiType type, IHiLoService? serv = null)
			    {
			        InitCacheId(type, serv);
			        return this;
			    }
			}
        }
            } // PostgreSql // NameSpace.tt Line: 252
	#region ID Cache // CacheIdHiLo.tt Line: 8, called from NameSpace.tt Line: 258
	public interface ICacheId // CacheIdHiLo.tt Line: 10
	{
	    int GetNextId(string guid);
	    void SetHi(string guid, ushort range);
	    void AddTemporaryRangeExpectation(string guid, ushort expectedRange);
	    ushort GetLo(string guid);
	}
	public interface ICacheIdAsync // CacheIdHiLo.tt Line: 17
	{
		Task<int> GetNextIdAsync(string guid);
	    Task SetHiAsync(string guid, ushort range);
	    Task AddTemporaryRangeExpectationAsync(string guid, ushort expectedRange);
	    Task<ushort> GetLoAsync(string guid);
	}
	public interface ICacheIdSyncAsync // CacheIdHiLo.tt Line: 24
	{
	    int GetNextId(string guid);
	    void SetHi(string guid, ushort range);
	    void AddTemporaryRangeExpectation(string guid, ushort expectedRange);
	    ushort GetLo(string guid);
		Task<int> GetNextIdAsync(string guid);
	    Task SetHiAsync(string guid, ushort range);
	    Task AddTemporaryRangeExpectationAsync(string guid, ushort expectedRange);
	    Task<ushort> GetLoAsync(string guid);
	}
	public struct HiLoRequest // CacheIdHiLo.tt Line: 35
	{
	    public ushort RequestedQty;
	    public string Guid;
	}
	public struct HiLoResult // CacheIdHiLo.tt Line: 40
	{
	    public int NextId;
	    public ushort ReturnedQty;
	}
	public interface IHiLoService // CacheIdHiLo.tt Line: 45
	{
	    HiLoResult GetHiLo(HiLoRequest request)
	    {
	        System.Diagnostics.Debug.Assert(false);
	        throw new NotImplementedException();
	    }
	    Task<HiLoResult> GetHiLoAsync(HiLoRequest request)
	    {
	        System.Diagnostics.Debug.Assert(false);
	        throw new NotImplementedException();
	    }
	}
	/// <summary>
	/// HiLo Id cache
	/// </summary>
	public class CacheIdHiLo : ICacheId, ICacheIdAsync, ICacheIdSyncAsync // CacheIdHiLo.tt Line: 61
	{
	    public ushort InitialHiValue = 1;
	    private readonly SemaphoreSlim semaphoreSlim = new(1, 1);
	    private readonly IHiLoService? serv;
	    private readonly Dictionary<string, HiLo> dic;
	    public CacheIdHiLo(IHiLoService serv) // CacheIdHiLo.tt Line: 67
	    {
	        this.dic = new Dictionary<string, HiLo>();
	        this.serv = serv;
	    }
	    /// <summary>
	    /// Get next Id for object with specified quid
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>next Id value</returns>
	    public int GetNextId(string guid) // CacheIdHiLo.tt Line: 77
	    {
	        System.Diagnostics.Debug.Assert(this.serv != null);
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        semaphoreSlim.Wait();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            if (h.Lo == 0)
	            {
	                var requestedQty = h.Hi;
	                if (h.NextCallTempHi > requestedQty)
	                    requestedQty = h.NextCallTempHi;
	                System.Diagnostics.Trace.WriteLine($"Hi={requestedQty}");
	                var request = new HiLoRequest() { Guid = guid, RequestedQty = requestedQty };
	                var res = this.serv.GetHiLo(request);
	                h.LastId = res.NextId - 1;
	                h.Lo = res.ReturnedQty;
	                h.NextCallTempHi = 0;
	            }
	            h.Lo--;
	            h.LastId++;
	            return h.LastId;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Asynchronous method to get next Id for object with specified quid
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>awaitable next Id value</returns>
	    public async Task<int> GetNextIdAsync(string guid) // CacheIdHiLo.tt Line: 114
	    {
	        System.Diagnostics.Debug.Assert(this.serv != null);
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        await semaphoreSlim.WaitAsync();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            if (h.Lo == 0)
	            {
	                var requestedQty = h.Hi;
	                if (h.NextCallTempHi > requestedQty)
	                    requestedQty = h.NextCallTempHi;
	                System.Diagnostics.Trace.WriteLine($"Hi={requestedQty}");
	                var request = new HiLoRequest() { Guid = guid, RequestedQty = requestedQty };
	                var res = await this.serv.GetHiLoAsync(request);
	                h.LastId = res.NextId - 1;
	                h.Lo = res.ReturnedQty;
	                h.NextCallTempHi = 0;
	            }
	            h.Lo--;
	            h.LastId++;
	            return h.LastId;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Set size Hi portion range which Hi-Lo algoritm is using to fetch next prtion of Ids from lower layer when cache is empty.
	    /// Lo value is amount Id remaining in cache.
	    /// Not applicable for Hi portion regulated by PID controller.
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <param name="range">size of Hi portion</param>
	    public void SetHi(string guid, ushort range) // CacheIdHiLo.tt Line: 153
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        System.Diagnostics.Debug.Assert(range > 0);
	        semaphoreSlim.Wait();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            h.Hi = range;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Set size Hi portion range which Hi-Lo algoritm is using to fetch next prtion of Ids from lower layer when cache is empty.
	    /// Lo value is amount Id remaining in cache.
	    /// Not applicable for Hi portion regulated by PID controller.
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <param name="range">size of Hi portion</param>
	    public async Task SetHiAsync(string guid, ushort hiValue) // CacheIdHiLo.tt Line: 178
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        System.Diagnostics.Debug.Assert(hiValue > 0);
	        await semaphoreSlim.WaitAsync();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            h.Hi = hiValue;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Increase expected range of Id for object guid
	    /// </summary>
	    /// <param name="guid"></param>
	    /// <param name="expectationIdUsage"></param>
	    /// <remarks>
	    /// On each next object Id request expectation will be reduced by one.
	    /// On next call for Id ranges if current expected range greater Hi value, current expectation value will be used instead Hi value
	    /// Maximum one call is expected for next Id ranges if conditions are not changed.
	    /// </remarks>
	    public void AddTemporaryRangeExpectation(string guid, ushort expectationIdUsage) // CacheIdHiLo.tt Line: 206
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        System.Diagnostics.Debug.Assert(expectationIdUsage > 0);
	        semaphoreSlim.Wait();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            h.NextCallTempHi += (ushort)(expectationIdUsage - h.Lo);
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Asynchronous method to increase expected range of Id for object guid
	    /// </summary>
	    /// <param name="guid"></param>
	    /// <param name="expectationIdUsage"></param>
	    /// <remarks>
	    /// On each next object Id request expectation will be reduced by one.
	    /// On next call for Id ranges if current expected range greater Hi value, current expectation value will be used instead Hi value
	    /// Maximum one call is expected for next Id ranges if conditions are not changed.
	    /// </remarks>
	    public async Task AddTemporaryRangeExpectationAsync(string guid, ushort expectationIdUsage) // CacheIdHiLo.tt Line: 234
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        System.Diagnostics.Debug.Assert(expectationIdUsage > 0);
	        await semaphoreSlim.WaitAsync();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            h.NextCallTempHi += (ushort)(expectationIdUsage - h.Lo);
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Get amount Id remaining in cache.
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>amount Id</returns>
	    public ushort GetLo(string guid) // CacheIdHiLo.tt Line: 257
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        semaphoreSlim.Wait();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            return h.Lo;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Get amount Id remaining in cache.
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>amount Id</returns>
	    public async Task<ushort> GetLoAsync(string guid) // CacheIdHiLo.tt Line: 279
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        await semaphoreSlim.WaitAsync();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            return h.Lo;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    private class HiLo // CacheIdHiLo.tt Line: 296
	    {
	        public HiLo(ushort hiValue)
	        {
	            LastId = 0;
	            Hi = hiValue;
	            Lo = 0;
	        }
	        public int LastId;
	        public ushort Hi;
	        public ushort Lo;
	        public ushort NextCallTempHi;
	    }
	}
	/// <summary>
	/// HiLo Id cache with PID controller for Hi request period
	/// </summary>
	public class CacheIdHiLoPid : ICacheId, ICacheIdAsync, ICacheIdSyncAsync // CacheIdHiLo.tt Line: 314
	{
	    public int ExpectedCacheUpdatePeriodSeconds = 5;
	    public int GainProportianal = 220; // Weight=1/100000
	    public int GainIntegral = 300; // Weight=1/100
	    public int GainDerivate = 300; // Weight=1/1000
	
	    public ushort InitialHiValue = 1;
	    private readonly SemaphoreSlim semaphoreSlim = new(1, 1);
	    private readonly IHiLoService? serv;
	    private readonly Dictionary<string, HiLo> dic;
	    public CacheIdHiLoPid(IHiLoService serv) // CacheIdHiLo.tt Line: 325
	    {
	        this.dic = new Dictionary<string, HiLo>();
	        this.serv = serv;
	    }
	    /// <summary>
	    /// Get next Id for object with specified quid
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>next Id value</returns>
	    public int GetNextId(string guid) // CacheIdHiLo.tt Line: 335
	    {
	        System.Diagnostics.Debug.Assert(this.serv != null);
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        semaphoreSlim.Wait();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            if (h.Lo == 0)
	            {
	                var request = PrepareRequest(guid, h);
	                if (h.NextCallTempHi > h.Hi)
	                    request.RequestedQty = h.NextCallTempHi;
	                var res = this.serv.GetHiLo(request);
	                h.LastId = res.NextId - 1;
	                h.Lo = res.ReturnedQty;
	            }
	            h.Lo--;
	            h.LastId++;
	            return h.LastId;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Asynchronous method to get next Id for object with specified quid
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>awaitable next Id value</returns>
	    public async Task<int> GetNextIdAsync(string guid) // CacheIdHiLo.tt Line: 369
	    {
	        System.Diagnostics.Debug.Assert(this.serv != null);
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        await semaphoreSlim.WaitAsync();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            if (h.Lo == 0)
	            {
	                var request = PrepareRequest(guid, h);
	                if (h.NextCallTempHi > h.Hi)
	                    request.RequestedQty = h.NextCallTempHi;
	                var res = await this.serv.GetHiLoAsync(request);
	                h.LastId = res.NextId - 1;
	                h.Lo = res.ReturnedQty;
	            }
	            h.Lo--;
	            h.LastId++;
	            return h.LastId;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    private HiLoRequest PrepareRequest(string guid, HiLo h) // CacheIdHiLo.tt Line: 398
	    {
	        var currTime = DateTime.Now;
	        int msecFromLastCall;
	        if (h.LastCallDateTime != null) // not first call
	        {
	            // https://en.wikipedia.org/wiki/PID_controller
	            // https://www.electronicshub.org/pid-controller-working-and-tuning-methods/
	            /* 1. get current position */
	            msecFromLastCall = (int)(currTime - h.LastCallDateTime.Value).TotalMilliseconds;
	            /* 2. calculate error */
	            var err = ExpectedCacheUpdatePeriodSeconds * 1000 - msecFromLastCall;
	            /* 3. calculate the output */
	            //proportional = Kp * err;
	            //integral = Ki * (last_sum + err);
	            //derivative = Kd * (err - last_err);
	            //output = proportional + derivative + integral;
	            h.Integral += err;
	            var derivate = err - h.LastErr;
	            var dhi = (GainProportianal * err + GainProportianal * GainIntegral / 100 * h.Integral + GainProportianal * GainDerivate / 1000 * derivate) / 100000;
	            //System.Diagnostics.Debug.Assert(dhi < ushort.MaxValue);
	            //System.Diagnostics.Debug.Assert(dhi > 0);
	            //System.Diagnostics.Debug.Assert(dhi < h.Hi * 100.0);
	            if (dhi > 0)
	                h.Hi = Math.Max(InitialHiValue, (ushort)dhi);
	            else
	                h.Hi = InitialHiValue;
	            /* 4. keep history */
	            //last_sum += err;
	            //last_err = err;
	            h.LastErr = err;
	            System.Diagnostics.Trace.WriteLine($"Hi={h.Hi}, msecFromLastCall={msecFromLastCall}, Er={err}, dhi={dhi}, I={h.Integral}, D={derivate}, PErr={GainProportianal * err / 1000000}, IErr={GainProportianal * GainIntegral / 1000 * h.Integral / 1000000 }, DErrInt={GainProportianal * GainDerivate / 1000 * derivate / 1000000}");
	        }
	        h.LastCallDateTime = currTime;
	        /* 5. apply control output */
	        //apply_control_output(output);
	        var res = new HiLoRequest() { Guid = guid, RequestedQty = h.Hi };
	        return res;
	    }
	    /// <summary>
	    /// Dummy method. No need Hi range regulated by PID controller
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <param name="range">size of Hi portion</param>
	    public void SetHi(string guid, ushort range) // CacheIdHiLo.tt Line: 442
	    {
	    }
	    /// <summary>
	    /// Dummy method. No need Hi range regulated by PID controller
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <param name="range">size of Hi portion</param>
	    public Task SetHiAsync(string guid, ushort hiValue) // CacheIdHiLo.tt Line: 450
	    {
	        return Task.CompletedTask;
	    }
	    /// <summary>
	    /// Increase expected range of Id for object guid
	    /// </summary>
	    /// <param name="guid"></param>
	    /// <param name="expectationIdUsage"></param>
	    /// <remarks>
	    /// On each next object Id request expectation will be reduced by one.
	    /// On next call for Id ranges if current expected range greater Hi value, current expectation value will be used instead Hi value
	    /// Maximum one call is expected for next Id ranges if conditions are not changed.
	    /// </remarks>
	    public void AddTemporaryRangeExpectation(string guid, ushort expectationIdUsage) // CacheIdHiLo.tt Line: 464
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        System.Diagnostics.Debug.Assert(expectationIdUsage > 0);
	        semaphoreSlim.Wait();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            h.NextCallTempHi += (ushort)(expectationIdUsage - h.Lo);
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Asynchronous method to increase expected range of Id for object guid
	    /// </summary>
	    /// <param name="guid"></param>
	    /// <param name="expectationIdUsage"></param>
	    /// <remarks>
	    /// On each next object Id request expectation will be reduced by one.
	    /// On next call for Id ranges if current expected range greater Hi value, current expectation value will be used instead Hi value
	    /// Maximum one call is expected for next Id ranges if conditions are not changed.
	    /// </remarks>
	    public async Task AddTemporaryRangeExpectationAsync(string guid, ushort expectationIdUsage) // CacheIdHiLo.tt Line: 492
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        System.Diagnostics.Debug.Assert(expectationIdUsage > 0);
	        await semaphoreSlim.WaitAsync();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            h.NextCallTempHi += (ushort)(expectationIdUsage - h.Lo);
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Get amount Id remaining in cache.
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>amount Id</returns>
	    public ushort GetLo(string guid) // CacheIdHiLo.tt Line: 515
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        semaphoreSlim.Wait();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            return h.Lo;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    /// <summary>
	    /// Get amount Id remaining in cache.
	    /// </summary>
	    /// <param name="guid">object guid</param>
	    /// <returns>amount Id</returns>
	    public async Task<ushort> GetLoAsync(string guid) // CacheIdHiLo.tt Line: 537
	    {
	        System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(guid));
	        await semaphoreSlim.WaitAsync();
	        try
	        {
	            HiLo h;
	            if (!this.dic.ContainsKey(guid))
	            {
	                h = new HiLo(InitialHiValue);
	                this.dic[guid] = h;
	            }
	            h = this.dic[guid];
	            return h.Lo;
	        }
	        finally { semaphoreSlim.Release(); }
	    }
	    private class HiLo // CacheIdHiLo.tt Line: 554
	    {
	        public HiLo(ushort hiValue)
	        {
	            LastId = 0;
	            Hi = hiValue;
	            Lo = 0;
	            LastErr = 0;
	            Integral = 0;
	            LastCallDateTime = null;
	        }
	        public int LastId;
	        public ushort Hi;
	        public ushort Lo;
	        public int LastErr;
	        public int Integral;
		    public ushort NextCallTempHi;
	        public DateTime? LastCallDateTime;
	    }
	}
	#endregion ID Cache // CacheIdHiLo.tt Line: 576, called from NameSpace.tt Line: 258
    public interface IEntityBaseExplicit // NameSpace.tt Line: 263
    {
        CommandDefinitionData GetCommandDefinitionInsert();
        CommandDefinitionData GetCommandDefinitionUpdate();
        CommandDefinitionData GetCommandDefinitionLoadById(int id);
        List<CommandDefinitionData> GetCommandDefinitionSave();
        CommandDefinitionData GetCommandDefinitionDeleteById(int id);
        CommandDefinitionData GetCommandDefinitionCountWhere(string? where, object? param);
        CommandDefinitionData GetCommandDefinitionDeleteWhere(string? where, object? param);
        CommandDefinitionData GetCommandDefinitionRemoveById(int id);
        CommandDefinitionData GetCommandDefinitionRemoveWhere(string? where, object? param);
        CommandDefinitionData GetCommandDefinitionSelect(string? where, object? param, string? sort, int page, int pagesize);
        CommandDefinitionData GetCommandDefinitionMoveTo(int id, int idGroupTo);
        CommandDefinitionData GetCommandDefinitionLoadSubTree(int id, int deep);
    }
    public interface IRecordId // NameSpace.tt Line: 278
    {
        int Id { get; }
    }
    public interface IEntityBase : IRecordId // NameSpace.tt Line: 282
    {
        string GetGuid();
        string GetDbTableName();
        bool IsNeedInsert(bool? isNeed = null);
        bool IsNeedUpdate(bool? isNeed = null);
        bool IsRemoved(bool? isRemoved = null);
        IEnumerable<IEntityBase> GetChildren();
        string TYPE_CACHE_ID { get; }
    }
    public interface IEntityBaseExplicit<T> : IEntityBaseExplicit, IEntityBase // NameSpace.tt Line: 295
    {
        T CreateDto(int id);
        T? LoadUtil(SqlMapper.GridReader multi);
    }
    public interface IViewPlainBaseExplicit<T> // NameSpace.tt Line: 300
    {
        CommandDefinitionData GetCommandDefinitionCountWhere(string? where, object? param);
        CommandDefinitionData GetCommandDefinitionView(int pagesize, int page, string? sort, string? where, object? param);
    }
    public interface IViewSelfTreeBaseExplicit<T> : IViewPlainBaseExplicit<T> // NameSpace.tt Line: 305
    {
        CommandDefinitionData GetCommandDefinitionSubTreeView(int? parentId, int deep, string? sort, string? where, object? param);
        CommandDefinitionData GetCommandDefinitionSubItemsView(int? folderId, int pagesize, int page, string? sort, string? where, object? param);
        CommandDefinitionData GetCommandDefinitionTreeListView(int? selectedId, string? where, object? param);
        CommandDefinitionData GetCommandDefinitionTreeListSubView(int? selectedId, string? sort, string? where, object? param);
    }
    public interface IViewPlainForRefTreeBaseExplicit<T> : IViewPlainBaseExplicit<T> // NameSpace.tt Line: 312
    {
        CommandDefinitionData GetCommandDefinitionSubItemsView(int? folderId, int pagesize, int page, string? sort, string? where, object? param);
    }
    public class Op // NameSpace.tt Line: 316
    {
        public enum EnumOpType { None, Insert, Update, Delete, Remove, Sql }
        public Op(IEntityBase entity, EnumOpType opType, string file, string member, int line)
        {
            switch(opType)
            {
                case EnumOpType.Delete:
                    this.Cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionDeleteById(entity.Id);
                    break;
                case EnumOpType.Insert:
                    this.Cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionInsert();
                    break;
                case EnumOpType.Remove:
                    this.Cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionRemoveById(entity.Id);
                    break;
                case EnumOpType.Update:
                    this.Cd = ((IEntityBaseExplicit)entity).GetCommandDefinitionUpdate();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            this.Entity = entity;
            this.OpType = opType;
            this.File = file;
            this.Member = member;
            this.Line = line;
        }
        public Op(CommandDefinitionData cd, string file, string member, int line)
        {
            this.Cd = cd;
            this.OpType = EnumOpType.Sql;
            this.File = file;
            this.Member = member;
            this.Line = line;
        }
        public Op(string sql, object? param, string file, string member, int line)
        {
            this.Cd = new CommandDefinitionData(sql, param);
            this.OpType = EnumOpType.Sql;
            this.File = file;
            this.Member = member;
            this.Line = line;
        }
        public EnumOpType OpType { get; private set; }
        public IEntityBase? Entity { get; private set; }
        internal CommandDefinitionData Cd { get; private set; }
        public string File { get; private set; }
        public string Member { get; private set; }
        public int Line { get; private set; }
    }
    /// <summary>
    /// Interface for UnitOfWork
    /// </summary>
    public partial interface IUnitOfWork : IDisposable // NameSpace.tt Line: 371
    {
        /// <summary>
        /// Commit all changes as atomic operation. 
        /// Rollback will happen automatically if any exception happened.
        /// </summary>
	    void Commit();
        /// <summary>
        /// Rollback current transaction and start new one. 
        /// </summary>
	    void Rollback();
    }
	public struct CommandDefinitionData // DapperGlobal.tt Line: 8, called from NameSpace.tt Line: 390
	{
	    public CommandDefinitionData(string commandText, object? parameters = null, CommandType? commandType = null)
	    {
	        this.Entity = null;
	        this.CommandTimeout = null;
	        this.CommandText = commandText;
	        this.Parameters = parameters;
	        this.CommandType = commandType;
	        this.IsBuffered = true;
	    }
	    public CommandDefinition GetCommandDefinition(System.Data.Common.DbTransaction? transaction = null)
	    {
	        var res = new CommandDefinition(this.CommandText, this.Parameters, transaction, this.CommandTimeout, this.CommandType);
	        return res;
	    }
	    public string CommandText { get; private set; }
	    public object? Parameters { get; private set; }
	    public CommandType? CommandType { get; private set; }
	    public int? CommandTimeout { get; set; }
	    public bool IsBuffered { get; private set; }
	    public IEntityBase? Entity { get; set; }
	}
	public static class AnonymousTypeExtensions
	{
	    // makes properties of object accessible 
	    public static Dictionary<string, object?> UnanonymizeProperties(object obj) // DapperGlobal.tt Line: 34
	    {
	        return obj.UnanonymizePropertiesExt();
	    }
	    public static Dictionary<string, object?> UnanonymizePropertiesExt(this object obj) // DapperGlobal.tt Line: 38
	    {
	        Type type = obj.GetType();
	        var properties = type.GetProperties()
	                .Select(n => n.Name)
	                .ToDictionary(k => k, k => type.GetProperty(k)?.GetValue(obj, null));
	        return properties;
	    }
	
	    // converts object list into list of properties that meet the filterCriteria
	    public static List<Dictionary<string, object?>> UnanonymizeListItems(List<object> objectList, // DapperGlobal.tt Line: 48
	                    Func<Dictionary<string, object?>, bool>? filterCriteria = default)
	    {
	        return objectList.UnanonymizeListItemsExt(filterCriteria);
	    }
	    public static List<Dictionary<string, object?>> UnanonymizeListItemsExt(this List<object> objectList, // DapperGlobal.tt Line: 53
	                    Func<Dictionary<string, object?>, bool>? filterCriteria = default)
	    {
	        var accessibleList = new List<Dictionary<string, object?>>();
	        foreach (object obj in objectList)
	        {
	            var props = obj.UnanonymizePropertiesExt();
	            if (filterCriteria == default
	                || filterCriteria((Dictionary<string, object?>)props) == true)
	            { accessibleList.Add(props); }
	        }
	        return accessibleList;
	    }
	
	    public static object? GetProp(object obj, string propertyName, bool treatNotFoundAsNull = false) // DapperGlobal.tt Line: 67
	    {
	        return obj.GetPropExt(propertyName, treatNotFoundAsNull);
	    }
	    public static object? GetPropExt(this object obj, string propertyName, bool treatNotFoundAsNull = false) // DapperGlobal.tt Line: 71
	    {
	        try
	        {
	            return ((Dictionary<string, object>)obj)
	                    ?[propertyName];
	        }
	        catch (KeyNotFoundException)
	        {
	            if (treatNotFoundAsNull) return default(object); else throw;
	        }
	    }
	    public static DynamicParameters ToDynamicParameters(object? prms, Action<DynamicParameters> AdditionalParameters, string[]? paramNames = null) // DapperGlobal.tt Line: 83
	    {
	        var res = new DynamicParameters();
	        AdditionalParameters(res);
	        if (prms != null)
	        {
	            var dic = AnonymousTypeExtensions.UnanonymizeProperties(prms);
	            #if DEBUG
	            if (paramNames != null)
	            {
	                foreach (var pname in paramNames)
	                {
	                    foreach (var nam in dic.Keys)
	                    {
	                        Debug.Assert(pname != nam);
	                    }
	                }
	            }
	            #endif
	            foreach (var nam in dic.Keys)
	            {
	                var val = dic[nam];
	                res.Add(nam, val);
	            }
	        }
	        return res;
	    }
	}
	public static class DbUtils // DapperGlobal.tt Line: 111
	{
		public static List<T> ToList<T>(this List<object> lst)
		{
			List<T> res = new List<T>();
			foreach(var t in lst)
			{
				T p = (T)t;
				res.Add(p);
			}
			return res;
		}
	    /*
	    public static void TraceSql(CommandDefinitionData cd) // DapperGlobal.tt Line: 124
	    {
	        Trace.WriteLine("####################");
	        TraceSqlInt(cd);
	        Trace.WriteLine("####################");
	    }
	    public static void TraceSql(List<CommandDefinitionData> lst) // DapperGlobal.tt Line: 130
	    {
	        Trace.WriteLine("####################");
	        foreach (var cd in lst)
	        {
	            Trace.WriteLine(cd.CommandText);
	            if (cd.Parameters is Dictionary<string, object>)
	            {
	                TraceSqlInt(cd);
	            }
	        }
	        Trace.WriteLine("####################");
	    }
	    private static void TraceSqlInt(CommandDefinitionData cd) // DapperGlobal.tt Line: 143
	    {
	        Trace.WriteLine(cd.CommandText);
	        //TODO add parameter reader
	        if (cd.Parameters is Dictionary<string, object> p)
	        {
	            StringBuilder sb = new StringBuilder();
	            sb.Append("Parameters: ");
	            foreach (var t in p)
	            {
	                sb.Append(t.Key);
	                sb.Append('=');
	                sb.Append(t.Value);
	                sb.Append(' ');
	            }
	            Trace.WriteLine(sb.ToString());
	        }
	    }
	    */
	}
    public static class CacheUtils
    {
        public static T? GetFromCache<T>(this IMemoryCache mc, string modelKey, string catType, int catId)
            where T : IEntityBase
        {
            var key = $"{modelKey}{catType}i{catId}";
            mc.TryGetValue<T>(key, out var obj);
            return obj;
        }
        public static void PlaceInCache<T>(this IMemoryCache mc, string modelKey, string catType, T cat)
            where T : IEntityBase
        {
            var key = $"{modelKey}{catType}i{cat.Id}";
            mc.Set<T>(key, cat, DateTimeOffset.FromUnixTimeSeconds(10));
        }
        public static void RemoveFromCache<T>(this IMemoryCache mc, string modelKey, string catType, int catId)
            where T : IEntityBase
        {
            var key = $"{modelKey}{catType}i{catId}";
            mc.Remove(key);
        }
    }
    public class ConcurrencyOptimisticException : System.Exception // NameSpace.tt Line: 546
    {
        public ConcurrencyOptimisticException(string? message) : base(message) { }
        public ConcurrencyOptimisticException(string? message, bool isDeleted) : base(message)
        {
            this.IsDeleted = isDeleted;
        }
        public bool IsDeleted { get; private set; }
    }
}
namespace vPlugins // NameSpace.tt Line: 556
{
	// called from NameSpace.tt Line: 560
	#region Interfaces // Api.tt Line: 9
	public interface ICount
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Count"]/*' />
	    abstract static int Count(string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	public interface ICountAsync // Api.tt Line: 16
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="CountAsync"]/*' />
	    abstract static Task<int> CountAsync(string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	public interface IViewItem // Api.tt Line: 22
	{
	    int Id { get; }
	    string GetName();
	}
	public interface ISelfTreeIViewItem // Api.tt Line: 27
	{
	    int Id { get; }
	    int? RefTreeParent { get; }
	    string GetName();
	}
	public interface IRefToTreeIViewItem // Api.tt Line: 33
	{
	    int Id { get; }
	    int GetRefToTree();
	    string GetName();
	}
	public interface ISameById<T> // Api.tt Line: 39
	    where T : class
	{
	    bool SameById(T other);
	}
	
	#region Catalogs and Docs Repositories
	/// <summary>
	/// Repository interface for catalogs and documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepositoryEntity<T> : IRepository<T> // Api.tt Line: 50
	    where T : class
	{
	    #region Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="RemoveById"]/*' />
	    abstract static void Remove(int id,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="RemoveSelect"]/*' />
	    abstract static void Remove(string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Load"]/*' />
	    abstract static T? Load(int id,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Static
	
	    #region Non Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Save"]/*' />
	    void Save([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Remove"]/*' />
	    void Remove([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Non Static
	}
	/// <summary>
	/// Repository interface for details in catalogs or documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepository<T> : ICount // Api.tt Line: 76
	    where T : class
	{
	    #region Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Create"]/*' />
	    abstract static T Create([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Select"]/*' />
	    abstract static IEnumerable<T> Select(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="DeleteById"]/*' />
	    abstract static void Delete(int id,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="DeleteSelect"]/*' />
	    abstract static void Delete(string? where, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Static
	
	    #region Non Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Update"]/*' />
	    void Update([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Insert"]/*' />
	    void Insert([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="Delete"]/*' />
	    void Delete([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Non Static
	}
	/// <summary>
	/// Async repository interface for catalogs and documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepositoryEntityAsync<T> : IRepositoryAsync<T> // Api.tt Line: 106
	    where T : class
	{
	    #region Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="RemoveAsyncById"]/*' />
	    abstract static Task RemoveAsync(int id,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="RemoveAsyncSelect"]/*' />
	    abstract static Task RemoveAsync(string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="LoadAsync"]/*' />
	    abstract static Task<T?> LoadAsync(int id,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Static
	
	    #region Non Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="SaveAsync"]/*' />
	    Task SaveAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="RemoveAsync"]/*' />
	    Task RemoveAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Non Static
	}
	/// <summary>
	/// Async repository interface for details in catalogs or documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepositoryAsync<T> : ICountAsync // Api.tt Line: 132
	    where T : class
	{
	    #region Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="CreateAsync"]/*' />
	    abstract static Task<T> CreateAsync([CallerFilePath] string file = "",
	        [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="SelectAsync"]/*' />
	    abstract static Task<IEnumerable<T>> SelectAsync(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="DeleteAsyncById"]/*' />
	    abstract static Task DeleteAsync(int id,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="DeleteAsyncSelect"]/*' />
	    abstract static Task DeleteAsync(string? where, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Static
	
	    #region Non Static
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="UpdateAsync"]/*' />
	    Task UpdateAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="InsertAsync"]/*' />
	    Task InsertAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/Desc[@name="DeleteAsync"]/*' />
	    Task DeleteAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    #endregion Non Static
	}
	#endregion Catalogs and Docs Repositories
	
	#region Views
	/// <summary>
	/// Interface for plain view for catalogs, details, or documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IViewEntity<T> : ICount // Api.tt Line: 166
	    where T : class
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetView"]/*' />
	    abstract static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	/// <summary>
	/// Async interface for plain view for catalogs, details, or documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IViewEntityAsync<T> : ICountAsync // Api.tt Line: 177
	    where T : class
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetViewAsync"]/*' />
	    abstract static Task<IEnumerable<T>> GetViewAsync(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	
	/// <summary>
	/// Detail view for catalogs and documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IViewDetail<T> : ICount // Api.tt Line: 189
	    where T : class
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetViewDetail"]/*' />
	    abstract static IEnumerable<T> GetViewByParentId(int? parentId, string? sort, int pagesize, int page, string? where, object? param,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	/// <summary>
	/// Detail view for catalogs and documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IViewDetailAsync<T> : ICountAsync // Api.tt Line: 200
	    where T : class
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetViewDetailAsync"]/*' />
	    abstract static Task<IEnumerable<T>> GetViewByParentIdAsync(int? parentId, string? sort, int pagesize, int page, string? where, object? param,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	
	/// <summary>
	/// Catalog with self tree structure or folder class view of catalog with separate properies for tree
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IViewTree<T> : ICount // Api.tt Line: 212
	    where T : class
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetTreeView"]/*' />
	    abstract static IEnumerable<T> GetSubTreeView(int? parentId, int deep = 2, string? sort = null, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetView"]/*' />
	    abstract static IEnumerable<T> GetViewByParentId(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetTreeListView"]/*' />
	    abstract static IEnumerable<T> GetTreeListView(int? selectedId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetView"]/*' />
	    abstract static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	/// <summary>
	/// Catalog with self tree structure or folder class view of catalog with separate properies for tree
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IViewTreeAsync<T> : ICountAsync // Api.tt Line: 232
	    where T : class
	{
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetViewTreeAsync"]/*' />
	    abstract static Task<IEnumerable<T>> GetSubTreeViewAsync(int? parentId, int deep = 2, string? sort = null, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetViewAsync"]/*' />
	    abstract static Task<IEnumerable<T>> GetViewByParentIdAsync(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetTreeListViewAsync"]/*' />
	    abstract static Task<IEnumerable<T>> GetTreeListViewAsync(int? selectedId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	    /// <include file='model_doc.xml' path='Doc/Model/Catalogs/Catalog/View/Desc[@name="GetViewAsync"]/*' />
	    abstract static Task<IEnumerable<T>> GetViewAsync(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null,
	        [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
	}
	#endregion Views
	#endregion Interfaces
	
}
#if !NET6_0 && !NET7_0 // Additional.tt Line: 8, called from NameSpace.tt Line: 566
/*
namespace System 
{
    public struct TimeOnly : IComparable<TimeOnly>, IEquatable<TimeOnly> // Additional.tt Line: 12
    {
        public TimeOnly(long ticks)
        {
            this.Ticks = ticks;
            this.Hour = 0;
            this.Minute = 0;
            this.Second = 0;
            this.Millisecond = 0;
            this.CalcFromTicks();
        }
        public TimeOnly(int hour, int minute)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Second = 0;
            this.Millisecond = 0;
            this.Ticks = 0;
            this.CalcToTicks();
        }
        public TimeOnly(int hour, int minute, int second)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Second = second;
            this.Millisecond = 0;
            this.Ticks = 0;
            this.CalcToTicks();
        }
        public TimeOnly(int hour, int minute, int second, int millisecond)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Second = second;
            this.Millisecond = millisecond;
            this.Ticks = 0;
            this.CalcToTicks();
        }
        public static TimeOnly FromTimeSpan(TimeSpan ts)
        {
            return new TimeOnly(ts.Hours, ts.Minutes, ts.Seconds);
        }
        public static TimeOnly FromDateTime(DateTime dt)
        {
            Diagnostics.Debug.Assert(dt.Kind == DateTimeKind.Utc);
            return new TimeOnly(dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
        private void CalcToTicks()
        {
            this.Ticks = (((this.Hour * 60L + this.Minute) * 60L + this.Second) * 1000L + this.Millisecond) * 10000L;
        }
        private void CalcFromTicks()
        {
            var l = this.Ticks / 10000L;
            this.Millisecond = (int)(l % 1000);
            l -= this.Millisecond / 1000;
            this.Second = (int)(l % 60);
            l -= this.Second / 60;
            this.Minute = (int)(l % 60);
            l -= this.Minute / 60;
            this.Hour = (int)(l % 60);
        }
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }
        public int Millisecond { get; private set; }
        public long Ticks { get; private set; }

        public int CompareTo(TimeOnly other)
        {
            if (this.Equals(other))
                return 0;
            var ov = (other.Hour << 11) + (other.Minute << 6) + other.Second;
            var tv = (this.Hour << 11) + (this.Minute << 6) + this.Second;
            if (tv > ov)
                return 1;
            return -1;
        }
        public bool Equals(TimeOnly other)
        {
            if (other.Ticks == this.Ticks)
                return true;
            return false;
        }
        public static TimeOnly MinValue = new(0, 0, 0);
        public static TimeOnly MaxValue = new(23,59,59);
    }
    public struct DateOnly : IComparable<DateOnly>, IEquatable<DateOnly> // Additional.tt Line: 99
    {
        public DateOnly(int year, int month, int day)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
        }
        public static DateOnly FromDateTime(DateTime dt)
        {
            return new DateOnly(dt.Year, dt.Month, dt.Day);
        }
        public DateTime ToDateTime(TimeOnly time)
        {
            return new DateTime(this.Year, this.Month, this.Day, time.Hour, time.Minute, time.Second);
        }
        public DateTime ToDateTime(TimeOnly time, DateTimeKind kind)
        {
            return new DateTime(this.Year, this.Month, this.Day, time.Hour, time.Minute, time.Second, time.Millisecond, kind);
        }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int CompareTo(DateOnly other)
        {
            if (this.Equals(other))
                return 0;
            var ov = (other.Year << 13) + (other.Month << 9) + other.Day;
            var tv = (this.Year << 13) + (this.Month << 9) + this.Day;
            if (tv > ov)
                return 1;
            return -1;
        }
        public bool Equals(DateOnly other)
        {
            if (other.Year == this.Year && other.Month == this.Month && other.Day == this.Day)
                return true;
            return false;
        }
        public static DateOnly MinValue = new(1, 1, 1);
        public static DateOnly MaxValue = new(9998, 12, 31);
    }
}
*/
#endif
