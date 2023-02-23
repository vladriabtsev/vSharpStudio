
// gRPC NameSpace.tt

// EnumVmType.DapperToGrpcServer
#nullable enable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using M = vPlugins.DapperModels.PostgreSql.Model; // NameSpace.tt Line: 412
/// <summary>
/// Auto generated. GRPC Server
/// </summary>
namespace vPlugins.GRPC.Server // NameSpace.tt Line: 416
{
    using vPlugins.DapperModels;
	using System.Text;
	using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Data;
    using System.Runtime.CompilerServices;
    using Grpc.Core;
    using VPlugins.GRPC.PostgreSql;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using System.Diagnostics;
	//using CommonCodeAPI;

    namespace PostgreSql // NameSpace.tt Line: 446
    {
        public static class GrpcUtils // NameSpace.tt Line: 448
        {
            public static Dapper.DynamicParameters? GetDynamicParameters(this Google.Protobuf.Collections.RepeatedField<CustomTypesGrpc.parameter> lst_params)
            {
                if (lst_params.Count == 0)
                    return null;
                var p = new Dapper.DynamicParameters();
                foreach (var t in lst_params)
                {
                    object? obj = null;
                    DbType? dbType = null;
                    switch (t.Type)
                    {
                        case CustomTypesGrpc.param_type.Bool:
                            obj = bool.Parse(t.Val);
                            dbType = DbType.Boolean;
                            break;
                        case CustomTypesGrpc.param_type.Byte:
                            obj = byte.Parse(t.Val);
                            dbType = DbType.Byte;
                            break;
                        case CustomTypesGrpc.param_type.Char:
                            obj = char.Parse(t.Val);
                            dbType = DbType.StringFixedLength;
                            break;
                        case CustomTypesGrpc.param_type.Dateonly:
                            obj = DateOnly.Parse(t.Val);
                            dbType = DbType.Date;
                            break;
                        case CustomTypesGrpc.param_type.Datetime:
                            obj = DateTime.Parse(t.Val);
                            dbType = DbType.DateTime2;
                            break;
                        case CustomTypesGrpc.param_type.Datetimeoffset:
                            obj = DateTimeOffset.Parse(t.Val);
                            dbType = DbType.DateTimeOffset;
                            break;
                        case CustomTypesGrpc.param_type.Decimal:
                            obj = decimal.Parse(t.Val);
                            dbType = DbType.Decimal;
                            break;
                        case CustomTypesGrpc.param_type.Double:
                            obj = double.Parse(t.Val);
                            dbType = DbType.Double;
                            break;
                        case CustomTypesGrpc.param_type.Float:
                            obj = float.Parse(t.Val);
                            //dbType = DbType..DateTime2;
                            break;
                        case CustomTypesGrpc.param_type.Guid:
                            obj = Guid.Parse(t.Val);
                            dbType = DbType.Guid;
                            break;
                        case CustomTypesGrpc.param_type.Int:
                            obj = int.Parse(t.Val);
                            dbType = DbType.Int32;
                            break;
                        case CustomTypesGrpc.param_type.Long:
                            obj = long.Parse(t.Val);
                            dbType = DbType.Int64;
                            break;
                        case CustomTypesGrpc.param_type.Null:
                            obj = null;
                            //dbType = DbType.Guid;
                            break;
                        case CustomTypesGrpc.param_type.Sbyte:
                            obj = sbyte.Parse(t.Val);
                            dbType = DbType.SByte;
                            break;
                        case CustomTypesGrpc.param_type.Short:
                            obj = short.Parse(t.Val);
                            dbType = DbType.Int16;
                            break;
                        case CustomTypesGrpc.param_type.String:
                            obj = t.Val;
                            dbType = DbType.String;
                            break;
                        case CustomTypesGrpc.param_type.Timeonly:
                            obj = TimeOnly.Parse(t.Val);
                            dbType = DbType.Time;
                            break;
                        case CustomTypesGrpc.param_type.Uint:
                            obj = uint.Parse(t.Val);
                            dbType = DbType.UInt32;
                            break;
                        case CustomTypesGrpc.param_type.Ulong:
                            obj = ulong.Parse(t.Val);
                            dbType = DbType.UInt64;
                            break;
                        case CustomTypesGrpc.param_type.Ushort:
                            obj = ushort.Parse(t.Val);
                            dbType = DbType.UInt16;
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                    p.Add(t.Name, obj, dbType);
                }
                return p;
            }
			public static M.TransactionOnCommit ToTransactionOnCommit(this transaction_data_insert_update_delete request) // GrpcServiceUtils.cs Line: 85
			{
				var dt = new M.TransactionOnCommit();
				foreach (var t in request.ListOperations)
				{
					switch (t.DbOperationCase)
					{
						case transaction_db_operation.DbOperationOneofCase.DbInsert: // GrpcServiceUtils.cs Line: 95
							if (t.DbInsert.DbRecord.Is(CtlgCatalog1.Descriptor))
								dt.Insert(t.DbInsert.DbRecord.Unpack<CtlgCatalog1>().ToModel(), t.File, t.Member, t.Line);
							else Debug.Assert(false);
							break;
						case transaction_db_operation.DbOperationOneofCase.DbUpdate: // GrpcServiceUtils.cs Line: 107
							if (t.DbUpdate.DbRecord.Is(CtlgCatalog1.Descriptor))
								dt.Update(t.DbUpdate.DbRecord.Unpack<CtlgCatalog1>().ToModel(), t.File, t.Member, t.Line);
							else Debug.Assert(false);
							break;
						case transaction_db_operation.DbOperationOneofCase.DbDelete: // GrpcServiceUtils.cs Line: 119
							if (t.DbDelete.DbRecord.Is(CtlgCatalog1.Descriptor))
								dt.Delete(t.DbDelete.DbRecord.Unpack<CtlgCatalog1>().ToModel(), t.File, t.Member, t.Line);
							else Debug.Assert(false);
							break;
						case transaction_db_operation.DbOperationOneofCase.DbRemove: // GrpcServiceUtils.cs Line: 131
							if (t.DbRemove.DbRecord.Is(CtlgCatalog1.Descriptor))
								dt.Remove(t.DbRemove.DbRecord.Unpack<CtlgCatalog1>().ToModel(), t.File, t.Member, t.Line);
							else Debug.Assert(false);
							break;
						case transaction_db_operation.DbOperationOneofCase.DbSql: // GrpcServiceUtils.cs Line: 143
							var pw = t.DbSql.ParamsWhere.Unpack<params_where>();
							dt.AddCommand(pw.Where, pw.Parameters.GetDynamicParameters());
							break;
						default:
							Debug.Assert(false);
							break;
					}
				}
				return dt;
			}
			#region Catalog Catalog1 // GrpcServiceUtils.cs Line: 207
			public static M.Catalogs.Catalog1 ToModel(this CtlgCatalog1 mes) // GrpcServiceUtils.cs Line: 289
			{
				return ToDbDto(mes);
			}
			public static M.Catalogs.Catalog1 ToDbDto(this CtlgCatalog1 mes) // GrpcServiceUtils.cs Line: 295
			{
				#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
				#if DEBUG
				var dto = (M.Catalogs.Catalog1)Activator.CreateInstance(typeof(M.Catalogs.Catalog1), true);
				#else
				var dto = new M.Catalogs.Catalog1();
				#endif
				#pragma warning restore CS8600
				#pragma warning disable CS8602 // Dereference of a possibly null reference.
				dto.Id = mes.Id; // GrpcServiceUtils.cs Line: 349
				#pragma warning restore CS8602
				dto.IsNeedInsert(mes.IsNeedInsert); // GrpcServiceUtils.cs Line: 356
				dto.IsNeedUpdate(mes.IsNeedUpdate);
				return dto;
			}
			public static CtlgCatalog1 ToMessage(this M.Catalogs.Catalog1 model) // GrpcServiceUtils.cs Line: 375
			{
				var res = new CtlgCatalog1()
				{
					Id = model.Id, // GrpcServiceUtils.cs Line: 435
				};
				return res;
			}
			#endregion Catalog Catalog1 // GrpcServiceUtils.cs Line: 241
        }
        public static partial class StartupUtils // NameSpace.tt Line: 560
        {
            public static void MapGrpcServices(IEndpointRouteBuilder endpoints) // NameSpace.tt Line: 562
            {
                M.Settings.InitDefault();
                endpoints.MapGrpcService<ModelPostgreSqlService>();
                endpoints.MapGrpcService<repository_basePostgreSqlService>();
                endpoints.MapGrpcService<CtlgCatalog1PostgreSqlService>();
            }
        }
#region Services
        public partial class ModelPostgreSqlService : ModelGrpc.ModelGrpcBase // NameSpace.tt Line: 572
        {
            //private readonly ILogger<ModelPostgreSqlService> _logger;
            //public ModelPostgreSqlService(ILogger<ModelPostgreSqlService> logger) { _logger = logger; }
            public override async Task<reply_> Commit(transaction_data_insert_update_delete request, ServerCallContext context)
            {
                try  // NameSpace.tt Line: 580
                {
                    var dt = request.ToTransactionOnCommit();
                    await dt.CommitAsync();
                    var res = new reply_() { Server = new server_result() { IsSuccess = true } };
                    return res;
                }
                catch (ConcurrencyOptimisticException ex)
                {
                    return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true } };
                }
                catch (Exception ex)
                {
                    return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message } };
                }
            }
        }
        public partial class repository_basePostgreSqlService : repository_basePostgreSqlGrpc.repository_basePostgreSqlGrpcBase // NameSpace.tt Line: 621
        {
            //private readonly ILogger<repository_basePostgreSqlService> _logger;
            //public repository_basePostgreSqlService(ILogger<repository_basePostgreSqlService> logger) { _logger = logger; }
			#region Services
			public override async Task<reply_int_value> get_next_id(params_next_id request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 198
				{
					var res = await M.IdGenerator.GetHiLoAsync(request.Guid, request.RequestedQty); // GrpcServiceUtils.cs Line: 735
					return new reply_int_value() { Result = res, Server = new server_result() { IsSuccess = true }};
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_int_value() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_int_value() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			#endregion Services
        }
        public partial class CtlgCatalog1PostgreSqlService : CtlgCatalog1PostgreSqlGrpc.CtlgCatalog1PostgreSqlGrpcBase // NameSpace.tt Line: 621
        {
            //private readonly ILogger<CtlgCatalog1PostgreSqlService> _logger;
            //public CtlgCatalog1PostgreSqlService(ILogger<CtlgCatalog1PostgreSqlService> logger) { _logger = logger; }
			#region Services
			public override async Task<reply_CtlgCatalog1> Load(int_value request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 589
				{
					var m = await M.Catalogs.Catalog1.LoadAsync(request.Value); // GrpcServiceUtils.cs Line: 539
					var res = m?.ToMessage();
					return new reply_CtlgCatalog1() { Result = res, Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 543
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_CtlgCatalog1() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_CtlgCatalog1() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_> Save(transaction_data_insert_update_delete request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 590
				{
					await request.ToTransactionOnCommit().CommitAsync(); // GrpcServiceUtils.cs Line: 581
					return new reply_() { Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 584
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_> RemoveById(int_value request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 591
				{
					await M.Catalogs.Catalog1.RemoveAsync(request.Value); // GrpcServiceUtils.cs Line: 697
					return new reply_() { Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 719
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_CtlgCatalog1_array> Select(params_where_sort_page request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 600
				{
					var p = request.Parameters.GetDynamicParameters(); // GrpcServiceUtils.cs Line: 591
					var lst = await M.Catalogs.Catalog1.SelectAsync(request.Where, p, request.Sort, request.Page, request.Pagesize);
					var res = new reply_CtlgCatalog1_array() { Server = new server_result() { IsSuccess = true } };
					foreach (var t in lst) { res.Result.Add(t.ToMessage()); }
					return res;
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_CtlgCatalog1_array() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_CtlgCatalog1_array() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_int_value> Count(params_where request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 601
				{
					var p = request.Parameters.GetDynamicParameters();
					var res = await M.Catalogs.Catalog1.CountAsync(request.Where, p);
					return new reply_int_value() { Result = res, Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 608
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_int_value() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_int_value() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_> Update(CtlgCatalog1 request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 602
				{
					await request.ToModel().UpdateAsync();
					return new reply_() { Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 641
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_int_value> Insert(CtlgCatalog1 request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 603
				{
					await request.ToModel().InsertAsync();
					return new reply_int_value() { Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 616
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_int_value() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_int_value() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_> DeleteById(int_value request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 604
				{
					await M.Catalogs.Catalog1.DeleteAsync(request.Value);
					return new reply_() { Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 689
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_> DeleteWhere(params_where request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 605
				{
					var p = request.Parameters.GetDynamicParameters(); // GrpcServiceUtils.cs Line: 654
					await M.Catalogs.Catalog1.DeleteAsync(request.Where, p);
					return new reply_() { Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 658
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			public override async Task<reply_> RemoveWhere(params_where request, ServerCallContext context) // GrpcServiceUtils.cs Line: 517
			{
				try // service method model is created from ProtoMain.partial.cs Line: 606
				{
					var p = request.Parameters.GetDynamicParameters(); // GrpcServiceUtils.cs Line: 726
					await M.Catalogs.Catalog1.RemoveAsync(request.Where, p);
					return new reply_() { Server = new server_result() { IsSuccess = true }}; // GrpcServiceUtils.cs Line: 729
				}
				catch (ConcurrencyOptimisticException ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message, IsDeleted = ex.IsDeleted, IsConcurrencyOptimisticException = true }};
				}
				catch (Exception ex)
				{
					return new reply_() { Server = new server_result() { Exception = ex.ToString(), Message = ex.Message }};
				}
			}
			#endregion Services
        }
#endregion Services
		public static class ConvertionUtils // FlipUtils.tt Line: 10, called from NameSpace.tt Line: 641
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
    }
}
