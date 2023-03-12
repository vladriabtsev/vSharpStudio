
// gRPC NameSpace.tt

// EnumVmType.ClientGrpcPoco
#nullable enable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// Auto generated. GRPC Client
/// </summary>
namespace vPlugins.GRPC.Client.PostgreSql // NameSpace.tt Line: 34
{
	using System.Text;
	using System.Linq;
	using System.Data;
    using Grpc.Net.Client;
    using Google.Protobuf.WellKnownTypes;
    using Google.Protobuf.Collections;
    using M = vPlugins.GRPC.Client.PostgreSql.Model; // NameSpace.tt Line: 42
    using VPlugins.GRPC.PostgreSql;
    using System.Diagnostics;
    using Polly;
    using System.Runtime.CompilerServices;
    using Microsoft.Extensions.DependencyInjection;
	//using CommonCodeAPI;

    public class Op // NameSpace.tt Line: 63
    {
        public enum EnumOpType { None, Insert, Update, Delete, Remove, Sql }
        public Op(IEntityBase entity, EnumOpType opType, string file, string member, int line)
        {
            this.Entity = entity;
            this.OpType = opType;
            this.File = file;
            this.Member = member;
            this.Line = line;
        }
        public Op(params_where sql, string file, string member, int line)
        {
            this.Sql = sql;
            this.OpType = EnumOpType.Sql;
            this.File = file;
            this.Member = member;
            this.Line = line;
        }
        public EnumOpType OpType { get; private set; }
        public IEntityBase? Entity { get; private set; }
        public params_where? Sql { get; private set; }
        public string File { get; private set; }
        public string Member { get; private set; }
        public int Line { get; private set; }
    }
    public static class GrpcUtils // NameSpace.tt Line: 89
    {
        public static void Pack(this transaction_data_insert_update_delete req, Op op) // NameSpace.tt Line: 92
        {
            transaction_db_operation? tbdop = null;
            switch (op.OpType)
            {
                case Op.EnumOpType.Insert:
                    tbdop = req.PackInsert(op.Entity!.GetDto());
                    break;
                case Op.EnumOpType.Update:
                    tbdop = req.PackUpdate(op.Entity!.GetDto());
                    break;
                case Op.EnumOpType.Remove:
                    tbdop = req.PackRemove(op.Entity!.GetDto());
                    break;
                case Op.EnumOpType.Delete:
                    tbdop = req.PackDelete(op.Entity!.GetDto());
                    break;
                case Op.EnumOpType.Sql:
                    tbdop = req.PackSql(op.Sql!);
                    break;
                case Op.EnumOpType.None:
                    return;
                default:
                    Debug.Assert(false);
                    break;
            }
            tbdop.File = op.File;
            tbdop.Member = op.Member;
            tbdop.Line = op.Line;
        }
        public static void PackInsertUpdate(this transaction_data_insert_update_delete req, IEntityBase ent) // NameSpace.tt Line: 122
        {
            if (ent.IsNeedInsert())
            {
                req.PackInsert(ent.GetDto());
            }
            else if (ent.IsNeedUpdate())
            {
                req.PackUpdate(ent.GetDto());
            }
        }
        public static void PackRemove(this transaction_data_insert_update_delete req, IEntityBase ent) // NameSpace.tt Line: 133
        {
            req.PackRemove(ent.GetDto());
        }
        public static transaction_db_operation PackInsert(this transaction_data_insert_update_delete req, Google.Protobuf.IMessage mes) // NameSpace.tt Line: 144
        {
            var op = new transaction_db_operation
            {
                DbInsert = new transaction_db_insert()
                {
                    DbRecord = Google.Protobuf.WellKnownTypes.Any.Pack(mes)
                }
            };
            req.ListOperations.Add(op);
            return op;
        }
        public static transaction_db_operation PackUpdate(this transaction_data_insert_update_delete req, Google.Protobuf.IMessage mes) // NameSpace.tt Line: 155
        {
            var op = new transaction_db_operation
            {
                DbUpdate = new transaction_db_update()
                {
                    DbRecord = Google.Protobuf.WellKnownTypes.Any.Pack(mes)
                }
            };
            req.ListOperations.Add(op);
            return op;
        }
        public static transaction_db_operation PackDelete(this transaction_data_insert_update_delete req, Google.Protobuf.IMessage mes) // NameSpace.tt Line: 122
        {
            var op = new transaction_db_operation
            {
                DbDelete = new transaction_db_delete()
                {
                    DbRecord = Google.Protobuf.WellKnownTypes.Any.Pack(mes)
                }
            };
            req.ListOperations.Add(op);
            return op;
        }
        public static transaction_db_operation PackRemove(this transaction_data_insert_update_delete req, Google.Protobuf.IMessage mes) // NameSpace.tt Line: 177
        {
            var op = new transaction_db_operation
            {
                DbRemove = new transaction_db_remove()
                {
                    DbRecord = Google.Protobuf.WellKnownTypes.Any.Pack(mes)
                }
            };
            req.ListOperations.Add(op);
            return op;
        }
        public static transaction_db_operation PackSql(this transaction_data_insert_update_delete req, Google.Protobuf.IMessage mes) // NameSpace.tt Line: 177
        {
            var op = new transaction_db_operation
            {
                DbSql = new transaction_db_sql()
                {
                    ParamsWhere = Google.Protobuf.WellKnownTypes.Any.Pack(mes)
                }
            };
            req.ListOperations.Add(op);
            return op;
        }
    }
    public partial class Model // NameSpace.tt Line: 204
    {
        public static bool LOGGING_EXTENTIONS { get; } = false; // NameSpace.tt Line: 239
        public static bool LOGGING_NLOG { get; } = false;
        public static bool LOGGING_SERILOG { get; } = false;
        public Action<server_result> ValidateResponce { get; set; } = (result) =>
        {
            if (!result.IsSuccess)
            {
                if (result.IsConcurrencyOptimisticException)
                {
                    if (result.IsDeleted)
                    {
                        throw new ConcurrencyOptimisticException(result.Message, true);
                    }
                    throw new ConcurrencyOptimisticException(result.Message, false);
                }
                throw new Exception(result.Message);
            }
        };
        private static readonly Model modelInstance = new();
        #region partial methods
        #endregion partial methods

        // TODO retry on channel
        // https://docs.microsoft.com/en-us/aspnet/core/grpc/retries?view=aspnetcore-6.0
        public static GrpcChannel? Channel { get; set; }
        public static void AddServices(IServiceCollection services, Uri currAddr) // NameSpace.tt Line: 265
        {
            Model.Settings.InitDefault();
            services.AddGrpcClient<repository_basePostgreSqlGrpc.repository_basePostgreSqlGrpcClient>((s, o) => { o.Address = currAddr; }); //.EnableCallContextPropagation();
            services.AddGrpcClient<CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient>((s, o) => { o.Address = currAddr; }); //.EnableCallContextPropagation();
            services.AddGrpcClient<CtlgCatalog1ViewListNarrowPostgreSqlGrpc.CtlgCatalog1ViewListNarrowPostgreSqlGrpcClient>((s, o) => { o.Address = currAddr; }); //.EnableCallContextPropagation();
            services.AddGrpcClient<CtlgCatalog1ViewListWidePostgreSqlGrpc.CtlgCatalog1ViewListWidePostgreSqlGrpcClient>((s, o) => { o.Address = currAddr; }); //.EnableCallContextPropagation();
        }
		/// <include file='model_doc.xml' path='Doc/Model/Catalogs/Desc[@name="catalogs"]/*' />
		public partial class Catalogs // Catalogs.tt Line: 8, called from NameSpace.tt Line: 281
		{
			// PocoCatalogs.tt Line: 7, called from Catalogs.tt Line: 13
			public partial class Catalog1 : RepoEntityBaseSyncAsync<Catalog1>, IEntityBaseExplicit<Catalog1>, ISameById<Catalog1>, IEntityBase // ModelCatalogClass.tt Line: 18
			{
			    #region ctor // ModelCtor.tt Line: 8, called from ModelCatalogClass.tt Line: 25
			    public IEnumerable<IEntityBase> GetChildren() // ModelCtor.tt Line: 17
			    {
			        return new List<IEntityBase>();
			    }
			    private readonly CtlgCatalog1 dto = new(); // ModelCtor.tt Line: 174
			    public CtlgCatalog1 Dto { get { return dto; } }
			    public Google.Protobuf.IMessage GetDto() { return dto; }
			#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			#if DEBUG
			    private Catalog1() : base("c0") {}
			#else
			    public Catalog1() : base("c0") {}
			#endif
			    public Catalog1(CtlgCatalog1 dto) : this()
			    {
			        this.dto = dto;
			        this.InitTabChanges();
			    }
			#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			    private void InitTabChanges()
			    {
			    }
			    #endregion ctor
			    #region Properties // ModelProperty.tt Line: 8, called from ModelCatalogClass.tt Line: 28
				public int Id // ModelProperty.tt Line: 19 - Utils.cs Line: 501 Utils.cs Line: 1175
				{
					get { return dto.Id; } // ModelProperty.tt Line: 19 - Utils.cs Line: 558
					set
					{
						System.Diagnostics.Debug.Assert(Math.Abs(value) <= 999999999); // based on Length Utils.cs Line: 701
						dto.Id = value; // ModelProperty.tt Line: 19 - Utils.cs Line: 612
						this.IsNeedInsert(true); // ModelProperty.tt Line: 19 - Utils.cs Line: 617
					}
				}
				public string Property1 // ModelProperty.tt Line: 19 - Utils.cs Line: 501 Utils.cs Line: 1155
				{
					get { return dto.Property1; } // ModelProperty.tt Line: 19 - Utils.cs Line: 517
					set
					{
						System.Diagnostics.Debug.Assert(value.Length <= 10); // Length in DB Utils.cs Line: 651
						dto.Property1 = value; // ModelProperty.tt Line: 19 - Utils.cs Line: 612
						this.IsNeedUpdate(true); // ModelProperty.tt Line: 19 - Utils.cs Line: 619
					}
				}
			
				#region Fields // ModelProperty.tt Line: 21
				public const string F_ID = "Id";
				public const string F_PROPERTY1 = "Property1";
				#endregion Fields // ModelProperty.tt Line: 28
			    #endregion Properties // ModelProperty.tt Line: 29
			    #region Special // ModelProperty.tt Line: 30
				public const string T_GUID = "95b20a95-1a31-4e61-96ef-f56e192f9f4d";
				public string GetGuid() { return T_GUID; }
				public const string T_NAME = "CtlgCatalog1";
			    public string GetDbTableName() { return T_NAME; }
			    public override bool IsNeedInsert(bool? isNeed = null) { if (isNeed.HasValue) { this.dto.IsNeedInsert = isNeed ?? false; } return this.dto.IsNeedInsert; }
			    public override bool IsNeedUpdate(bool? isNeed = null) { if (isNeed.HasValue) { this.dto.IsNeedUpdate = isNeed ?? false; } return this.dto.IsNeedUpdate; }
			    public override bool IsRemoved(bool? isRemove = null) { if (isRemove.HasValue) { this.dto.IsRemoved = isRemove ?? false; } return this.dto.IsRemoved; }
			    //public bool IsMarkedForDeletion(bool? isMarkedForDeletion = null) { if (isMarkedForDeletion.HasValue) { this.___isMarkedForDeletion = isMarkedForDeletion ?? false; } return this.___isMarkedForDeletion; }
			    public bool SameById(Catalog1 other) { return other != null && this.Id == other.Id; } // ModelProperty.tt Line: 45
			    #endregion Special // ModelProperty.tt Line: 46
				#region Operations // PocoCatalogOperations.tt Line: 7, called from ModelCatalogClass.tt Line: 39
				#endregion Operations
				#region Repository // CatalogRepository.tt Line: 7, called from ModelCatalogClass.tt Line: 66
				// Repository.tt Line: 8, called from CatalogRepository.partial.cs Line: 25
				 
				Catalog1 IEntityBaseExplicit<Catalog1>.CreateDto(int id) // Repository.tt Line: 36
				{
				    var dto = new CtlgCatalog1
				    {
				        Id = id,
				        Property1 = string.Empty,
				        IsNeedInsert = true,
				    };
				    return new Catalog1(dto);
				}
				protected override Catalogs.Catalog1 GetThis() { return this; }
				protected override async Task SaveUtilAsync() // Repository.tt Line: 59
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 61
				    var request = new transaction_data_insert_update_delete();
				    request.PackInsertUpdate(this);
				    var response = await client.SaveAsync(request);
				    modelInstance.ValidateResponce(response.Server);
				    this.IsNeedInsert(false);
				    this.IsNeedUpdate(false);
				}
				protected override void SaveUtil() // Repository.tt Line: 100
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 102
				    var request = new transaction_data_insert_update_delete();
				    request.PackInsertUpdate(this);
				    var response = client.Save(request);
				    modelInstance.ValidateResponce(response.Server);
				    this.IsNeedInsert(false);
				    this.IsNeedUpdate(false);
				}
				async Task IEntityBaseExplicit<Catalog1>.RemoveUtilAsync(int id) // Repository.tt Line: 150
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 152
				    var response = await client.RemoveByIdAsync(new int_value(){ Value = id });
				    modelInstance.ValidateResponce(response.Server);
				}
				async Task IEntityBaseExplicit<Catalog1>.RemoveUtilAsync(string? where, object? param) // Repository.tt Line: 162
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 164
				    var pp = new params_where() { Where = where };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = await client.RemoveWhereAsync(pp);;
				    modelInstance.ValidateResponce(response.Server);
				}
				void IEntityBaseExplicit<Catalog1>.RemoveUtil(int id) // Repository.tt Line: 178
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 180
				    var response = client.RemoveById(new int_value(){ Value = id });
				    modelInstance.ValidateResponce(response.Server);
				}
				void IEntityBaseExplicit<Catalog1>.RemoveUtil(string? where, object? param) // Repository.tt Line: 190
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 192
				    var pp = new params_where() { Where = where };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = client.RemoveWhere(pp);
				    modelInstance.ValidateResponce(response.Server);
				}
				async Task<Catalog1?> IEntityBaseExplicit<Catalog1>.LoadUtilAsync(int id) // Repository.tt Line: 209
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 211
				    var response = await client.LoadAsync(new int_value(){ Value = id });
				    modelInstance.ValidateResponce(response.Server);
				    return new Catalog1(response.Result);
				}
				Catalog1? IEntityBaseExplicit<Catalog1>.LoadUtil(int id) // Repository.tt Line: 224
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 226
				    var response = client.Load(new int_value(){ Value = id });
				    modelInstance.ValidateResponce(response.Server);
				    return new Catalog1(response.Result);
				}
				async Task<IEnumerable<Catalog1>> IEntityBaseExplicit<Catalog1>.SelectUtilAsync(string? where, object? param, string? sort, int page, int pagesize) // Repository.tt Line: 286
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 288
				    var pp = new params_where_sort_page() { Where = where, Sort = sort, Page = page, Pagesize = pagesize };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = await client.SelectAsync(pp);
				    modelInstance.ValidateResponce(response.Server);
					var res = new List<Catalog1>(); 
					foreach(var t in response.Result) { res.Add(new Catalog1(t));	}
				    return res;
				}
				IEnumerable<Catalog1> IEntityBaseExplicit<Catalog1>.SelectUtil(string? where, object? param, string? sort, int page, int pagesize) // Repository.tt Line: 305
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 307
				    var pp = new params_where_sort_page() { Where = where, Sort = sort, Page = page, Pagesize = pagesize };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = client.Select(pp);
				    modelInstance.ValidateResponce(response.Server);
					var res = new List<Catalog1>(); 
					foreach(var t in response.Result) { res.Add(new Catalog1(t));	}
				    return res;
				}
				async Task<int> IEntityBaseExplicit<Catalog1>.CountUtilAsync(string? where, object? param) // Repository.tt Line: 327
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 329
				    var pp = new params_where() { Where = where };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = await client.CountAsync(pp);
				    modelInstance.ValidateResponce(response.Server);
				    return response.Result;
				}
				int IEntityBaseExplicit<Catalog1>.CountUtil(string? where, object? param) // Repository.tt Line: 344
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 346
				    var pp = new params_where() { Where = where };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = client.Count(pp);
				    modelInstance.ValidateResponce(response.Server);
				    return response.Result;
				}
				protected override async Task UpdateUtilAsync() // Repository.tt Line: 364
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 366
				    var response = await client.UpdateAsync(this.Dto);
				    modelInstance.ValidateResponce(response.Server);
				}
				protected override void UpdateUtil() // Repository.tt Line: 382
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 384
				    var response = client.Update(this.Dto);
				    modelInstance.ValidateResponce(response.Server);
				}
				protected override async Task InsertUtilAsync() // Repository.tt Line: 403
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 405
				    var response = await client.InsertAsync(this.Dto);
				    modelInstance.ValidateResponce(response.Server);
				}
				protected override void InsertUtil() // Repository.tt Line: 417
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 419
				    var response = client.Insert(this.Dto);
				    modelInstance.ValidateResponce(response.Server);
				}
				async Task IEntityBaseExplicit<Catalog1>.DeleteUtilAsync(int id) // Repository.tt Line: 434
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 436
				    var response = await client.DeleteByIdAsync(new int_value() { Value = id });
				    modelInstance.ValidateResponce(response.Server);
				}
				async Task IEntityBaseExplicit<Catalog1>.DeleteUtilAsync(string? where, object? param) // Repository.tt Line: 446
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 448
				    var pp = new params_where() { Where = where };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = await client.DeleteWhereAsync(pp);
				    modelInstance.ValidateResponce(response.Server);
				}
				void IEntityBaseExplicit<Catalog1>.DeleteUtil(int id) // Repository.tt Line: 462
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 464
				    var response = client.DeleteById(new int_value() { Value = id });
				    modelInstance.ValidateResponce(response.Server);
				}
				void IEntityBaseExplicit<Catalog1>.DeleteUtil(string? where, object? param) // Repository.tt Line: 468
				{
				    var client = new CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcClient(Model.Channel); // Repository.tt Line: 470
				    var pp = new params_where() { Where = where };
				    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
				    var response = client.DeleteWhere(pp);
				    modelInstance.ValidateResponce(response.Server);
				}
				#endregion Repository // CatalogRepository.tt Line: 51
				#region Views // PocoView.tt Line: 9, called from ModelCatalogClass.tt Line: 82
				public partial class ViewListNarrow : ViewEntityBaseSyncAsync<ViewListNarrow>, IViewPlainBaseExplicit<ViewListNarrow>, IViewItem, ISameById<ViewListNarrow>  // PocoView.tt Line: 54
				{
				    public bool SameById(ViewListNarrow other) { return other != null && this.Id == other.Id; } // PocoView.tt Line: 56
				    public string GetName() { return this.Property1; } // PocoView.tt Line: 61
				    private readonly CtlgCatalog1ViewListNarrow dto = new(); // PocoView.tt Line: 95
				    #if DEBUG
				    private ViewListNarrow() { }
				    #else
				    public ViewListNarrow() { }
				    #endif
				    public ViewListNarrow(CtlgCatalog1ViewListNarrow dto) : this()
				    {
				        this.dto = dto;
				    }
				    #region View Properties
					public string Property1 // PocoView.tt Line: 108 - Utils.cs Line: 501 Utils.cs Line: 1155
					{
						get { return dto.Property1; } // PocoView.tt Line: 108 - Utils.cs Line: 517
					}
					public int Id // PocoView.tt Line: 108 - Utils.cs Line: 501 Utils.cs Line: 1175
					{
						get { return dto.Id; } // PocoView.tt Line: 108 - Utils.cs Line: 558
					}
				    #endregion View Properties
					async Task<int> IViewPlainBaseExplicit<ViewListNarrow>.CountUtilAsync(string? where, object? param) // PocoView.tt Line: 147
					{
					    var pp = new params_where() { Where = where };
					    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
					    var client = new CtlgCatalog1ViewListNarrowPostgreSqlGrpc.CtlgCatalog1ViewListNarrowPostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 151
					    var response = await client.CountAsync(pp);
					    modelInstance.ValidateResponce(response.Server);
					    return response.Result;
					}
					async Task<IEnumerable<ViewListNarrow>> IViewPlainBaseExplicit<ViewListNarrow>.GetViewUtilAsync(int pagesize, int page, string? sort, string? where, object? param) // PocoView.tt Line: 203
					{
					    var request = new params_where_sort_page() { Pagesize = pagesize, Page = page, Sort = sort, Where = where };
					    CustomTypesGrpc.parameter.Translate(param, request.Parameters);
					    var client = new CtlgCatalog1ViewListNarrowPostgreSqlGrpc.CtlgCatalog1ViewListNarrowPostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 207
					    var response = await client.GetViewAsync(request);
					    modelInstance.ValidateResponce(response.Server);
					    var res = new List<ViewListNarrow>();
					    foreach (var t in response.Result) { res.Add(new ViewListNarrow(t)); }
					    return res;
					}
					int IViewPlainBaseExplicit<ViewListNarrow>.CountUtil(string? where, object? param) // PocoView.tt Line: 222
					{
					    var pp = new params_where() { Where = where };
					    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
					    var client = new CtlgCatalog1ViewListNarrowPostgreSqlGrpc.CtlgCatalog1ViewListNarrowPostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 226
					    var response = client.Count(pp);
					    modelInstance.ValidateResponce(response.Server);
					    return response.Result;
					}
					IEnumerable<ViewListNarrow> IViewPlainBaseExplicit<ViewListNarrow>.GetViewUtil(int pagesize, int page, string? sort, string? where, object? param) // PocoView.tt Line: 278
					{
					    var request = new params_where_sort_page() { Pagesize = pagesize, Page = page, Sort = sort, Where = where };
					    CustomTypesGrpc.parameter.Translate(param, request.Parameters);
					    var client = new CtlgCatalog1ViewListNarrowPostgreSqlGrpc.CtlgCatalog1ViewListNarrowPostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 282
					    var response = client.GetView(request);
					    modelInstance.ValidateResponce(response.Server);
					    var res = new List<ViewListNarrow>();
					    foreach (var t in response.Result) { res.Add(new ViewListNarrow(t)); }
					    return res;
					}
				}
				public partial class ViewListWide : ViewEntityBaseSyncAsync<ViewListWide>, IViewPlainBaseExplicit<ViewListWide>, IViewItem, ISameById<ViewListWide>  // PocoView.tt Line: 54
				{
				    public bool SameById(ViewListWide other) { return other != null && this.Id == other.Id; } // PocoView.tt Line: 56
				    public string GetName() { return this.Property1; } // PocoView.tt Line: 61
				    private readonly CtlgCatalog1ViewListWide dto = new(); // PocoView.tt Line: 95
				    #if DEBUG
				    private ViewListWide() { }
				    #else
				    public ViewListWide() { }
				    #endif
				    public ViewListWide(CtlgCatalog1ViewListWide dto) : this()
				    {
				        this.dto = dto;
				    }
				    #region View Properties
					public string Property1 // PocoView.tt Line: 108 - Utils.cs Line: 501 Utils.cs Line: 1155
					{
						get { return dto.Property1; } // PocoView.tt Line: 108 - Utils.cs Line: 517
					}
					public int Id // PocoView.tt Line: 108 - Utils.cs Line: 501 Utils.cs Line: 1175
					{
						get { return dto.Id; } // PocoView.tt Line: 108 - Utils.cs Line: 558
					}
				    #endregion View Properties
					async Task<int> IViewPlainBaseExplicit<ViewListWide>.CountUtilAsync(string? where, object? param) // PocoView.tt Line: 147
					{
					    var pp = new params_where() { Where = where };
					    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
					    var client = new CtlgCatalog1ViewListWidePostgreSqlGrpc.CtlgCatalog1ViewListWidePostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 151
					    var response = await client.CountAsync(pp);
					    modelInstance.ValidateResponce(response.Server);
					    return response.Result;
					}
					async Task<IEnumerable<ViewListWide>> IViewPlainBaseExplicit<ViewListWide>.GetViewUtilAsync(int pagesize, int page, string? sort, string? where, object? param) // PocoView.tt Line: 203
					{
					    var request = new params_where_sort_page() { Pagesize = pagesize, Page = page, Sort = sort, Where = where };
					    CustomTypesGrpc.parameter.Translate(param, request.Parameters);
					    var client = new CtlgCatalog1ViewListWidePostgreSqlGrpc.CtlgCatalog1ViewListWidePostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 207
					    var response = await client.GetViewAsync(request);
					    modelInstance.ValidateResponce(response.Server);
					    var res = new List<ViewListWide>();
					    foreach (var t in response.Result) { res.Add(new ViewListWide(t)); }
					    return res;
					}
					int IViewPlainBaseExplicit<ViewListWide>.CountUtil(string? where, object? param) // PocoView.tt Line: 222
					{
					    var pp = new params_where() { Where = where };
					    CustomTypesGrpc.parameter.Translate(param, pp.Parameters);
					    var client = new CtlgCatalog1ViewListWidePostgreSqlGrpc.CtlgCatalog1ViewListWidePostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 226
					    var response = client.Count(pp);
					    modelInstance.ValidateResponce(response.Server);
					    return response.Result;
					}
					IEnumerable<ViewListWide> IViewPlainBaseExplicit<ViewListWide>.GetViewUtil(int pagesize, int page, string? sort, string? where, object? param) // PocoView.tt Line: 278
					{
					    var request = new params_where_sort_page() { Pagesize = pagesize, Page = page, Sort = sort, Where = where };
					    CustomTypesGrpc.parameter.Translate(param, request.Parameters);
					    var client = new CtlgCatalog1ViewListWidePostgreSqlGrpc.CtlgCatalog1ViewListWidePostgreSqlGrpcClient(Model.Channel); // PocoView.tt Line: 282
					    var response = client.GetView(request);
					    modelInstance.ValidateResponce(response.Server);
					    var res = new List<ViewListWide>();
					    foreach (var t in response.Result) { res.Add(new ViewListWide(t)); }
					    return res;
					}
				}
				#endregion Views // PocoView.tt Line: 294
			}
		}
		public partial class Documents // Documents.tt Line: 7, called from NameSpace.tt Line: 284
		{
			// PocoDocuments.tt Line: 7, called from Documents.tt Line: 12
		}
		public partial class TransactionOnCommit // DeferredTransaction.tt Line: 9
		{
		    #region General // DeferredTransaction.tt Line: 12 
		    private readonly transaction_data_insert_update_delete request;
		    private readonly List<Op> ListOperations;
		    public TransactionOnCommit()
		    {
		        this.request = new();
		        this.ListOperations = new List<Op>();
		    }
		    public void Commit() // DeferredTransaction.tt Line: 35 
		    {
		        foreach(var t in this.ListOperations)
		        {
		            this.request.Pack(t);
		        }
		        var client = new ModelGrpc.ModelGrpcClient(Model.Channel); // DeferredTransaction.tt Line: 110
		        var response = client.Commit(this.request);
		        modelInstance.ValidateResponce(response.Server);
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
		    public async Task CommitAsync() // DeferredTransaction.tt Line: 141 
		    {
		        foreach(var t in this.ListOperations)
		        {
		            this.request.Pack(t);
		        }
		        var client = new ModelGrpc.ModelGrpcClient(Model.Channel); // DeferredTransaction.tt Line: 216
		        var response = await client.CommitAsync(this.request);
		        modelInstance.ValidateResponce(response.Server);
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
		    //TODO commandTimeout from this
		    public void AddCommand(string commandText, object? parameters = null,
		                [CallerFilePath] string file = "",
		                [CallerMemberName] string member = "",
		                [CallerLineNumber] int line = 0) // DeferredTransaction.tt Line: 250 //, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default) // DeferredTransaction.tt Line: 250
		    {
				var p = new params_where() { Where = commandText };
				CustomTypesGrpc.parameter.Translate(parameters, p.Parameters);
		        this.ListOperations.Add(new Op(p, file, member, line));
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
		public interface IAttachForUpdates // BaseClasses.tt Line: 8, called from ModelGlobal.tt Line: 9
		{
		    void AttachForUpdates(TransactionOnCommit tx);
		}
		public partial class RepoBase // BaseClasses.tt Line: 12, called from ModelGlobal.tt Line: 9
		{
		    public virtual bool IsNeedInsert(bool? isNeedInsert = null) { throw new NotImplementedException(); }
		    public virtual bool IsNeedUpdate(bool? isNeedUpdate = null) { throw new NotImplementedException(); }
		    public virtual bool IsRemoved(bool? isRemove = null) { throw new NotImplementedException(); }
		}
		public partial class RepoBaseSyncAsync<T> : RepoBase,  // BaseClasses.tt Line: 31, called from ModelGlobal.tt Line: 9
		    IRepository<T>, IRepositoryAsync<T>
		    where T : class, IEntityBaseExplicit<T>, ISameById<T>, IEntityBase
		#if !DEBUG
		    , new()
		#endif
		{
		    /// <summary>
		    /// Unique string for current version
		    /// </summary>
			public string TYPE_CACHE_ID { get; private set; }
			public RepoBaseSyncAsync(string typeId)
			{
				this.TYPE_CACHE_ID = typeId;
			}
			// BaseRepository.tt Line: 8, called from BaseClasses.tt Line: 49
			#if DEBUG
			protected static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
			#else
			protected static T instance = new T();
			#endif
			protected virtual T GetThis() { throw new NotImplementedException(); } // BaseRepository.tt Line: 14
			 
			public static async Task<T> CreateAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 25*/) // BaseRepository.tt Line: 25
			{
				var id = await GetNextIdAsync(instance.GetGuid()); // BaseRepository.tt Line: 35
				var res = instance.CreateDto(id);
			    res.IsNeedInsert(true);
				return res;
			}
			public static T Create([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 44*/) // BaseRepository.tt Line: 44
			{
				var id = GetNextId(instance.GetGuid()); // BaseRepository.tt Line: 54
				var res = instance.CreateDto(id);
			    res.IsNeedInsert(true);
				return res;
			}
			public static async Task<IEnumerable<T>> SelectAsync(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 66*/) // BaseRepository.tt Line: 66
			{
			    var res = await instance.SelectUtilAsync(where, param, sort, page, pagesize); // BaseRepository.tt Line: 89
			    return res;
			}
			public static IEnumerable<T> Select(string? where, object? param = null, string? sort = null, int page = 0, int pagesize = 0, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 96*/) // BaseRepository.tt Line: 96
			{
			    var res = instance.SelectUtil(where, param, sort, page, pagesize); // BaseRepository.tt Line: 119
			    return res;
			}
			public static async Task<int> CountAsync(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 129*/) // BaseRepository.tt Line: 129
			{
			    var res = await instance.CountUtilAsync(where, param); // BaseRepository.tt Line: 146
			    return res;
			}
			public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 153*/) // BaseRepository.tt Line: 153
			{
			    var res = instance.CountUtil(where, param); // BaseRepository.tt Line: 170
			    return res;
			}
			protected virtual int CountUtil(string? where, object? param) { throw new NotImplementedException(); } // BaseRepository.tt Line: 175
			public async Task UpdateAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 182*/) // BaseRepository.tt Line: 182
			{
			    Debug.Assert(!this.IsNeedInsert());
			    Debug.Assert(this.IsNeedUpdate());
			    Debug.Assert(!this.IsRemoved());
			    await UpdateUtilAsync(); // BaseRepository.tt Line: 212
			    this.IsNeedUpdate(false);
			}
			protected virtual Task UpdateUtilAsync() { throw new NotImplementedException(); } // BaseRepository.tt Line: 217
			public void Update([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 220*/) // BaseRepository.tt Line: 220
			{
			    Debug.Assert(!this.IsNeedInsert());
			    Debug.Assert(this.IsNeedUpdate());
			    Debug.Assert(!this.IsRemoved());
			    UpdateUtil(); // BaseRepository.tt Line: 249
			    this.IsNeedUpdate(false);
			}
			protected virtual void UpdateUtil() { throw new NotImplementedException(); } // BaseRepository.tt Line: 254
			public async Task InsertAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 260*/) // BaseRepository.tt Line: 260
			{
			    Debug.Assert(!this.IsRemoved());
			    Debug.Assert(this.IsNeedInsert());
			    await InsertUtilAsync(); // BaseRepository.tt Line: 278
			    this.IsNeedInsert(false);
			    this.IsNeedUpdate(false);
			}
			protected virtual Task InsertUtilAsync() { throw new NotImplementedException(); } // BaseRepository.tt Line: 284
			public void Insert([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 287*/) // BaseRepository.tt Line: 287
			{
			    Debug.Assert(!this.IsRemoved());
			    Debug.Assert(this.IsNeedInsert());
			    InsertUtil(); // BaseRepository.tt Line: 305
				this.IsNeedInsert(false);
			    this.IsNeedUpdate(false);
			}
			protected virtual void InsertUtil() { throw new NotImplementedException(); } // BaseRepository.tt Line: 311
			public async Task DeleteAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 332*/) // BaseRepository.tt Line: 332
			{
			    Debug.Assert(!this.IsNeedInsert());
			    Debug.Assert(!this.IsRemoved());
			    await instance.DeleteUtilAsync(this.GetThis().Id); // BaseRepository.tt Line: 337
				this.IsNeedInsert(false);
				this.IsNeedUpdate(false);
				this.IsRemoved(true);
			}
			public static async Task DeleteAsync(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 359*/) // BaseRepository.tt Line: 359
			{
			    await instance.DeleteUtilAsync(id); // BaseRepository.tt Line: 362
			}
			public static async Task DeleteAsync(string? where, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 381*/) // BaseRepository.tt Line: 381
			{
			    await instance.DeleteUtilAsync(where, param); // BaseRepository.tt Line: 384
			}
			public void Delete([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 390*/) // BaseRepository.tt Line: 390
			{
			    Debug.Assert(!this.IsNeedInsert());
			    Debug.Assert(!this.IsRemoved());
			    instance.DeleteUtil(this.GetThis().Id); // BaseRepository.tt Line: 405
			    this.IsRemoved(true);
			}
			public static void Delete(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 410*/) // BaseRepository.tt Line: 410
			{
			    instance.DeleteUtil(id); // BaseRepository.tt Line: 425
			}
			public static void Delete(string? where, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepository.tt Line: 429*/) // BaseRepository.tt Line: 429
			{
			    instance.DeleteUtil(where, param); // BaseRepository.tt Line: 444
			}
		}
		public partial class RepoEntityBaseSyncAsync<T> : RepoBaseSyncAsync<T>,  // BaseClasses.tt Line: 54, called from ModelGlobal.tt Line: 9
		    IRepositoryEntity<T>, IRepositoryEntityAsync<T>, IAttachForUpdates
		    where T : class, IEntityBaseExplicit<T>, ISameById<T>, IEntityBase
		#if !DEBUG
		    , new()
		#endif
		{
			public RepoEntityBaseSyncAsync(string typeId) : base(typeId) { }
			// BaseRepositoryEntity.tt Line: 8, called from BaseClasses.tt Line: 65
			protected TransactionOnCommit? transactionOnCommit = null;
			public void AttachForUpdates(TransactionOnCommit tx)
			{
			    this.transactionOnCommit = tx;
			}
			public async Task SaveAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 18*/) // BaseRepositoryEntity.tt Line: 18
			{
			    Debug.Assert(this.transactionOnCommit == null, "Entity already attached to transaction on commit");
			    Debug.Assert(!this.IsRemoved());
			    await this.SaveUtilAsync(); // BaseRepositoryEntity.tt Line: 43
			}
			protected virtual Task SaveUtilAsync() { throw new NotImplementedException(); } // BaseRepositoryEntity.tt Line: 46
			public void Save([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 50*/) // BaseRepositoryEntity.tt Line: 50
			{
			    Debug.Assert(this.transactionOnCommit == null, "Entity already attached to transaction on commit");
			    Debug.Assert(!this.IsRemoved());
			    this.SaveUtil(); // BaseRepositoryEntity.tt Line: 75
			}
			protected virtual void SaveUtil() { throw new NotImplementedException(); } // BaseRepositoryEntity.tt Line: 78
			public async Task RemoveAsync([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 85*/) // BaseRepositoryEntity.tt Line: 85
			{
			    Debug.Assert(this.transactionOnCommit == null, "Entity already attached to transaction on commit");
			    Debug.Assert(!this.IsNeedInsert());
			    Debug.Assert(!this.IsRemoved());
				await RemoveAsync(this.GetThis().Id);
			    this.IsRemoved(true);
				foreach (var t in this.GetThis().GetChildren())
					t.IsRemoved(true);
			}
			public static async Task RemoveAsync(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 97*/) // BaseRepositoryEntity.tt Line: 97
			{
			    await instance.RemoveUtilAsync(id); // BaseRepositoryEntity.tt Line: 116
			}
			public static async Task RemoveAsync(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 120*/) // BaseRepositoryEntity.tt Line: 120
			{
			    await instance.RemoveUtilAsync(where, param); // BaseRepositoryEntity.tt Line: 135
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
			    instance.RemoveUtil(id); // BaseRepositoryEntity.tt Line: 172
			}
			public static void Remove(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 176*/) // BaseRepositoryEntity.tt Line: 176
			{
			    instance.RemoveUtil(where, param); // BaseRepositoryEntity.tt Line: 191
			}
			protected static bool isEntityWithTabs;
			public static async Task<T?> LoadAsync(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 201*/) // BaseRepositoryEntity.tt Line: 201
			{
			    var res = await instance.LoadUtilAsync(id); // BaseRepositoryEntity.tt Line: 250
			    return res;
			}
			public static T? Load(int id, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseRepositoryEntity.tt Line: 257*/) // BaseRepositoryEntity.tt Line: 257
			{
			    var res = instance.LoadUtil(id); // BaseRepositoryEntity.tt Line: 305
			    return res;
			}
		}
		public partial class ViewEntityBaseSyncAsync<T> : IViewEntity<T>, IViewEntityAsync<T> // BaseClasses.tt Line: 70, called from ModelGlobal.tt Line: 9
		    where T : class, IViewPlainBaseExplicit<T>, ISameById<T>
		#if !DEBUG
		    , new()
		#endif
		{
			// BaseView.tt Line: 8, called from BaseClasses.tt Line: 79
			#if DEBUG
			private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
			#else
			private static T instance = new T();
			#endif
			public static async Task<int> CountAsync(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 226*/) // BaseView.tt Line: 226
			{
			    var res = await instance.CountUtilAsync(where, param); // BaseView.tt Line: 243
			    return res;
			}
			public static async Task<IEnumerable<T>> GetViewAsync(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 248*/) // BaseView.tt Line: 248
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = await instance.GetViewUtilAsync(pagesize, page, sort, where, param);
			    return res;
			}
			public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
			{
			    var res = instance.CountUtil(where, param); // BaseView.tt Line: 288
			    return res;
			}
			public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = instance.GetViewUtil(pagesize, page, sort, where, param);
			    return res;
			}
		}
		public partial class ViewTreeBaseSyncAsync<T> : IViewTree<T>, IViewTreeAsync<T> // BaseClasses.tt Line: 84, called from ModelGlobal.tt Line: 9
		    where T : class, IViewSelfTreeBaseExplicit<T>, ISameById<T>
		#if !DEBUG
		    , new()
		#endif
		{
			// BaseView.tt Line: 8, called from BaseClasses.tt Line: 93
			#if DEBUG
			private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
			#else
			private static T instance = new T();
			#endif
			public static async Task<IEnumerable<T>> GetSubTreeViewAsync(int? parentId, int deep = 2, string? sort = null, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 34*/) // BaseView.tt Line: 34
			{
			    var res = await instance.GetSubTreeViewUtilAsync(parentId, deep, sort, where, param); // BaseView.tt Line: 37
			    return res;
			}
			public static async Task<IEnumerable<T>> GetTreeListViewAsync(int? selectedId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 42*/) // BaseView.tt Line: 42
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = await instance.GetSubItemsViewUtilAsync(selectedId, pagesize, page, sort, where, param); // BaseView.tt Line: 70
			    return res;
			}
			public static async Task<IEnumerable<T>> GetViewByParentIdAsync(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 75*/) // BaseView.tt Line: 75
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = await instance.GetSubItemsViewUtilAsync(parentId, pagesize, page, sort, where, param); // BaseView.tt Line: 91
			    return res;
			}
			public static IEnumerable<T> GetSubTreeView(int? parentId, int deep = 2, string? sort = null, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 113*/) // BaseView.tt Line: 113
			{
			    var res = instance.GetSubTreeViewUtil(parentId, deep, sort, where, param);
			    return res;
			}
			public static IEnumerable<T> GetTreeListView(int? selectedId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 121*/) // BaseView.tt Line: 121
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = instance.GetTreeListViewUtil(selectedId, pagesize, page, sort, where, param);
			    return res;
			}
			public static IEnumerable<T> GetViewByParentId(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 153*/) // BaseView.tt Line: 153
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = instance.GetSubItemsViewUtil(parentId, pagesize, page, sort, where, param);
			    return res;
			}
			public static async Task<int> CountAsync(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 226*/) // BaseView.tt Line: 226
			{
			    var res = await instance.CountUtilAsync(where, param); // BaseView.tt Line: 243
			    return res;
			}
			public static async Task<IEnumerable<T>> GetViewAsync(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 248*/) // BaseView.tt Line: 248
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = await instance.GetViewUtilAsync(pagesize, page, sort, where, param);
			    return res;
			}
			public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
			{
			    var res = instance.CountUtil(where, param); // BaseView.tt Line: 288
			    return res;
			}
			public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = instance.GetViewUtil(pagesize, page, sort, where, param);
			    return res;
			}
		}
		public partial class ViewDetailBaseSyncAsync<T> : IViewDetail<T>, IViewDetailAsync<T> // BaseClasses.tt Line: 98, called from ModelGlobal.tt Line: 9
		    where T : class, IViewPlainForRefTreeBaseExplicit<T>, ISameById<T>
		#if !DEBUG
		    , new()
		#endif
		{
			// BaseView.tt Line: 8, called from BaseClasses.tt Line: 107
			#if DEBUG
			private static T instance = (T)Activator.CreateInstance(typeof(T), true)!;
			#else
			private static T instance = new T();
			#endif
			public static async Task<IEnumerable<T>> GetViewByParentIdAsync(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 179*/) // BaseView.tt Line: 179
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = await instance.GetSubItemsViewUtilAsync(parentId, pagesize, page, sort, where, param);
			    return res;
			}
			public static IEnumerable<T> GetViewByParentId(int? parentId, string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 202*/) // BaseView.tt Line: 202
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = instance.GetSubItemsViewUtil(parentId, pagesize, page, sort, where, param);
			    return res;
			}
			public static async Task<int> CountAsync(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 226*/) // BaseView.tt Line: 226
			{
			    var res = await instance.CountUtilAsync(where, param); // BaseView.tt Line: 243
			    return res;
			}
			public static async Task<IEnumerable<T>> GetViewAsync(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 248*/) // BaseView.tt Line: 248
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = await instance.GetViewUtilAsync(pagesize, page, sort, where, param);
			    return res;
			}
			public static int Count(string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 271*/) // BaseView.tt Line: 271
			{
			    var res = instance.CountUtil(where, param); // BaseView.tt Line: 288
			    return res;
			}
			public static IEnumerable<T> GetView(string? sort = null, int pagesize = 0, int page = 0, string? where = null, object? param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from BaseView.tt Line: 293*/) // BaseView.tt Line: 293
			{
			    Debug.Assert((pagesize == 0 && page == 0) || (pagesize > 0 && page > 0 && sort != null && sort.Count() > 0)); // if page is selected then pagesize and sort parameters has to be selected
			    var res = instance.GetViewUtil(pagesize, page, sort, where, param);
			    return res;
			}
		}
		public class ListChildren<T> : List<T> // ModelGlobal.tt Line: 12, called from NameSpace.tt Line: 290
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
		    public ListChildren(Func<T> onCreate, Func<Task<T>> onCreateAsync, Action<T> onAdd, Action<T> onRemove, Func<T, Task> onRemoveAsync) // ModelGlobal.tt Line: 24
		    {
		        this.onCreate = onCreate;
		        this.onAdd = onAdd;
		        this.onRemove = onRemove;
		        this.onCreateAsync = onCreateAsync;
		        this.onRemoveAsync = onRemoveAsync;
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
		    readonly Func<Task<T>> onCreateAsync;
		    readonly Func<T, Task> onRemoveAsync;
		    /// <summary>
		    /// Create and add new child instance
		    /// </summary>
		    /// <returns>New created child instance</returns>
		    public async Task<T> AddNewAsync() // ModelGlobal.tt Line: 161
		    {
		        Debug.Assert(this.onCreateAsync != null);
		        Debug.Assert(this.onAdd != null);
		        var item = await this.onCreateAsync();
		        this.onAdd(item);
		        base.Add(item);
		        return item;
		    }
		    public async Task ClearAsync() // ModelGlobal.tt Line: 170
		    {
		        Debug.Assert(this.onRemoveAsync != null);
		        foreach (var t in this)
		        {
		            await this.onRemoveAsync(t);
		        }
		        base.Clear();
		    }
		    public async Task RemoveAsync(T item) // ModelGlobal.tt Line: 179
		    {
		        Debug.Assert(this.onRemoveAsync != null);
		        await this.onRemoveAsync(item);
		        base.Remove(item);
		    }
		    public async Task RemoveAllAsync(Predicate<T> match) // ModelGlobal.tt Line: 185
		    {
		        Debug.Assert(this.onRemoveAsync != null);
		        foreach (var t in this)
		        {
		            await this.onRemoveAsync(t);
		        }
		        base.RemoveAll(match);
		    }
		    public async Task RemoveAtAsync(int index) // ModelGlobal.tt Line: 194
		    {
		        Debug.Assert(this.onRemoveAsync != null);
		        await this.onRemoveAsync(this[index]);
		        base.RemoveAt(index);
		    }
		    public async Task RemoveRangeAsync(int index, int count) // ModelGlobal.tt Line: 200
		    {
		        Debug.Assert(this.onRemoveAsync != null);
		        for (int i = 0; i < count; i++)
		        {
		            await this.onRemoveAsync(this[i + index]);
		        }
		        base.RemoveRange(index, count);
		    }
		}
		
		#region ID Cache
		public class HiLoService : IHiLoService // ModelCacheId.tt Line: 9, called from NameSpace.tt Line: 296
		{
		    public HiLoResult GetHiLo(HiLoRequest request)
		    {
		        var client = new repository_basePostgreSqlGrpc.repository_basePostgreSqlGrpcClient(Model.Channel);
		        var response = client.get_next_id(new params_next_id() { Guid = request.Guid, RequestedQty = request.RequestedQty });
		        modelInstance.ValidateResponce(response.Server);
		        return new HiLoResult() { NextId = response.Result, ReturnedQty = request.RequestedQty };
		    }
		    public async Task<HiLoResult> GetHiLoAsync(HiLoRequest request)
		    {
		        var client = new repository_basePostgreSqlGrpc.repository_basePostgreSqlGrpcClient(Model.Channel);
		        var response = await client.get_next_idAsync(new params_next_id() { Guid = request.Guid, RequestedQty = request.RequestedQty });
		        modelInstance.ValidateResponce(response.Server);
		        return new HiLoResult() { NextId = response.Result, ReturnedQty = request.RequestedQty };
		    }
		}
		public enum EnumHiType { Fixed, PIDController }; // ModelCacheId.tt Line: 50
		private static ICacheIdSyncAsync? cacheId;
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
		public static Task<int> GetNextIdAsync(string guid) // ModelCacheId.tt Line: 92
		{
		    System.Diagnostics.Debug.Assert(Model.cacheId != null);
		    return Model.cacheId.GetNextIdAsync(guid);
		}
		#endregion ID Cache
		
		
		
		#region History // Model.tt Line: 19, called from NameSpace.tt Line: 303
		public partial class _history_objects_ids // Model.tt Line: 23
		{
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
		        return res;
		    }
		    /// <summary>
		    /// Get object int ID
		    /// </summary>
		    /// <returns>
		    /// int value
		    /// </returns>
		    /// <param name="guid">Object guid</param>
		    public async static Task<int> GetObjectIdAsync(string guid, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0 /*NameSpace.partial.shared.cs Line: 316, call from Model.tt Line: 86*/) // Model.tt Line: 86
		    {
		        int res = 0;
		        return res;
		    }
		}
		public class _history // Model.tt Line: 122
		{
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
		}
		#endregion History
		public static ModelSettings Settings { get { if (_Settings == null) _Settings = new ModelSettings(); return _Settings; } }
		private static ModelSettings? _Settings;
		public class ModelSettings // ModelSettings.tt Line: 10
		{
		    public ModelSettings InitDefault()
		    {
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
	#region ID Cache // CacheIdHiLo.tt Line: 8, called from NameSpace.tt Line: 319
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
	#endregion ID Cache // CacheIdHiLo.tt Line: 576, called from NameSpace.tt Line: 319
	public static class ConvertionUtils // FlipUtils.tt Line: 10, called from NameSpace.tt Line: 324
	{
	    public static DateTimeOffset ToDateTimeOffset(this Google.Protobuf.WellKnownTypes.Timestamp mes)
	    {
	        return mes.ToDateTimeOffset();
	    }
	    public static DateTime ToDateTime(this Google.Protobuf.WellKnownTypes.Timestamp mes)
	    {
	        return mes.ToDateTime();
	    }
	    public static DateOnly ToDateOnly(this Google.Protobuf.WellKnownTypes.Timestamp mes)
	    {
	        return DateOnly.FromDateTime(mes.ToDateTime());
	    }
	    public static TimeOnly ToTimeOnly(this Google.Protobuf.WellKnownTypes.Timestamp mes)
	    {
	        return TimeOnly.FromDateTime(mes.ToDateTime());
	    }
	    public static TimeOnly ToTimeOnly(this Google.Protobuf.WellKnownTypes.Duration mes)
	    {
	        return TimeOnly.FromTimeSpan(mes.ToTimeSpan());
	    }
	    public static TimeSpan ToTimeSpan(this Google.Protobuf.WellKnownTypes.Duration mes)
	    {
	        return mes.ToTimeSpan();
	    }
	    public static Google.Protobuf.WellKnownTypes.Timestamp ToMessage(this DateTimeOffset d)
	    {
	        return Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(d);
	    }
	    public static Google.Protobuf.WellKnownTypes.Timestamp ToMessage(this DateTime d)
	    {
	        return Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(d);
	    }
	    public static Google.Protobuf.WellKnownTypes.Timestamp ToMessage(this DateOnly d)
	    {
	        return Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(d.ToDateTime(new TimeOnly(), DateTimeKind.Utc));
	    }
	    public static Google.Protobuf.WellKnownTypes.Duration ToMessage(this TimeOnly d)
	    {
		    return Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(d.ToTimeSpan());
	    }
	    public static CustomTypesGrpc.DurationNullable ToMessage(this TimeOnly? d)
	    {
	        if (d.HasValue)
	            return new CustomTypesGrpc.DurationNullable() { HasValue = true, Value = d.Value.ToMessage() };
	        return new CustomTypesGrpc.DurationNullable() { HasValue = false };
	    }
	    public static Google.Protobuf.WellKnownTypes.Duration ToMessage(this TimeSpan d)
	    {
	        return Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(d);
	    }
	}
	public static class FlipUtils // FlipUtils.tt Line: 64
	{
	}
    
    public interface IEntityBaseExplicit // NameSpace.tt Line: 330
    {
    }
    public interface IEntityBase // NameSpace.tt Line: 333
    {
        string GetGuid();
        int Id { get; }
        //string GetDbTableName();
        bool IsNeedInsert(bool? isNeed = null);
        bool IsNeedUpdate(bool? isNeed = null);
        bool IsRemoved(bool? isRemoved = null);
        Google.Protobuf.IMessage GetDto();
        IEnumerable<IEntityBase> GetChildren();
    }
    public interface IEntityBaseExplicit<T> : IEntityBaseExplicit, IEntityBase // NameSpace.tt Line: 347
    {
        T CreateDto(int id);
        //T? LoadUtil(SqlMapper.GridReader multi);
        Task<T?> LoadUtilAsync(int id);
        Task RemoveUtilAsync(int id);
        Task RemoveUtilAsync(string? where, object? param);
        Task<IEnumerable<T>> SelectUtilAsync(string? where, object? param, string? sort, int page, int pagesize);
        Task<int> CountUtilAsync(string? where, object? param);
        Task DeleteUtilAsync(int id);
        Task DeleteUtilAsync(string? where, object? param);
        T? LoadUtil(int id);
        void RemoveUtil(int id);
        void RemoveUtil(string? where, object? param);
        IEnumerable<T> SelectUtil(string? where, object? param, string? sort, int page, int pagesize);
        int CountUtil(string? where, object? param);
        void DeleteUtil(int id);
        void DeleteUtil(string? where, object? param);
    }
    public interface IViewPlainBaseExplicit<T> // NameSpace.tt Line: 370
    {
        Task<int> CountUtilAsync(string? where, object? param);
        Task<IEnumerable<T>> GetViewUtilAsync(int pagesize, int page, string? sort, string? where, object? param);
        int CountUtil(string? where, object? param);
        IEnumerable<T> GetViewUtil(int pagesize, int page, string? sort, string? where, object? param);
    }
    public interface IViewSelfTreeBaseExplicit<T> : IViewPlainBaseExplicit<T> // NameSpace.tt Line: 381
    {
        Task<IEnumerable<T>> GetSubTreeViewUtilAsync(int? parentId, int deep, string? sort, string? where, object? param);
        Task<IEnumerable<T>> GetSubItemsViewUtilAsync(int? folderId, int pagesize, int page, string? sort, string? where, object? param);
        Task<IEnumerable<T>> GetTreeListViewUtilAsync(int? selectedId, int pagesize, int page, string? sort, string? where, object? param);
        IEnumerable<T> GetSubTreeViewUtil(int? parentId, int deep, string? sort, string? where, object? param);
        IEnumerable<T> GetSubItemsViewUtil(int? folderId, int pagesize, int page, string? sort, string? where, object? param);
        IEnumerable<T> GetTreeListViewUtil(int? selectedId, int pagesize, int page, string? sort, string? where, object? param);
    }
    public interface IViewPlainForRefTreeBaseExplicit<T> : IViewPlainBaseExplicit<T> // NameSpace.tt Line: 394
    {
        Task<IEnumerable<T>> GetSubItemsViewUtilAsync(int? folderId, int pagesize, int page, string? sort, string? where, object? param);
        IEnumerable<T> GetSubItemsViewUtil(int? folderId, int pagesize, int page, string? sort, string? where, object? param);
    }
}
namespace vPlugins // AdditionalClientCode.tt Line: 9, called from NameSpace.tt Line: 405
{
	// called from AdditionalClientCode.tt Line: 13
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
	
    public class ConcurrencyOptimisticException : System.Exception // AdditionalClientCode.tt Line: 17
    {
        public ConcurrencyOptimisticException(string? message) : base(message) { }
        public ConcurrencyOptimisticException(string? message, bool isDeleted) : base(message)
        {
            this.IsDeleted = isDeleted;
        }
        public bool IsDeleted { get; private set; }
    }
    public static class AnonymousTypeExtensions
    {
        // makes properties of object accessible 
        public static Dictionary<string, object?> UnanonymizeProperties(object obj)
        {
            return obj.UnanonymizePropertiesExt();
        }
        public static Dictionary<string, object?> UnanonymizePropertiesExt(this object obj)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties()
                   .Select(n => n.Name)
                   .ToDictionary(k => k, k => type.GetProperty(k)?.GetValue(obj, null));
            return properties;
        }

        // converts object list into list of properties that meet the filterCriteria
        public static List<Dictionary<string, object?>> UnanonymizeListItems(List<object> objectList,
                        Func<Dictionary<string, object?>, bool>? filterCriteria = default)
        {
            return objectList.UnanonymizeListItemsExt(filterCriteria);
        }
        public static List<Dictionary<string, object?>> UnanonymizeListItemsExt(this List<object> objectList,
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

        public static object? GetProp(object obj, string propertyName,
                                     bool treatNotFoundAsNull = false)
        {
            return obj.GetPropExt(propertyName, treatNotFoundAsNull);
        }
        public static object? GetPropExt(this object obj, string propertyName,
                                     bool treatNotFoundAsNull = false)
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
    }
}
#if !NET6_0 && !NET7_0 // Additional.tt Line: 8, called from NameSpace.tt Line: 408
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
