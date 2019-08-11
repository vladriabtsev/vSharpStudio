using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proto.Config.Connection;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;

// https://www.learnentityframeworkcore.com/migrations

// https://fluentmigrator.github.io/articles/quickstart.html?tabs=runner-in-process
// https://github.com/fluentmigrator/fluentmigrator

// https://blog.oneunicorn.com/2016/10/24/ef-core-1-1-creating-dbcontext-instances/
// https://blog.oneunicorn.com/2016/10/27/dependency-injection-in-ef-core-1-1/
// https://blog.oneunicorn.com/2016/11/09/ef-core-1-1-metadata-overview/
// https://blog.oneunicorn.com/2016/11/10/implementing-provider-extension-methods-in-ef-core-1-1/
// https://blog.oneunicorn.com/tag/ef-core-provider/
namespace vPlugin.DbModel.MsSql
{
    public class MsSqlDesignGenerator : IvPluginGenerator, IvPluginDbGenerator
    {
        //private IServiceProvider service_provider = null;
        private IServiceCollection services = null;
        public MsSqlDesignGenerator()
        {
            this.Guid = new Guid("0D85CF93-9134-45C6-B3B9-6BFAF4A12183");
            this.Name = "Design";
            this.DefaultSettingsName = "Setting";
            this.Description = "DB structure creation and migration";
            this.PluginGeneratorType = vPluginLayerTypeEnum.DbDesign;

            services = new ServiceCollection()
                .AddEntityFrameworkSqlServer();
            //.AddEntityFrameworkDesignTimeServices();
            new SqlServerDesignTimeServices().ConfigureDesignTimeServices(services);
            services
                .AddSingleton<IOperationReporter, OperationReporter>()
                .AddSingleton<IOperationReportHandler, OperationReportHandler>()
                .AddSingleton<ICandidateNamingService, CandidateNamingService>()
                .AddSingleton<IPluralizer, NullPluralizer>()
                .AddSingleton<ICSharpUtilities, CSharpUtilities>()
                .AddSingleton<IScaffoldingTypeMapper, ScaffoldingTypeMapper>()
                .AddSingleton<IScaffoldingModelFactory, RelationalScaffoldingModelFactory>();

        }
        static DiagnosticSource MsSqlMigratorDiagnostic = new DiagnosticListener("vPlugin.MsSqlMigrator");
        public ILogger Logger;
        public Guid Guid { get; protected set; }
        public string Name { get; protected set; }
        public string DefaultSettingsName { get; protected set; }
        public string Description { get; protected set; }
        public vPluginLayerTypeEnum PluginGeneratorType { get; }
        public IvPluginGeneratorSettingsVM GetSettingsMvvm(string settings)
        {
            if (settings == null)
                return new MsSqlDesignGeneratorSettings();
            proto_ms_sql_design_generator_settings proto = proto_ms_sql_design_generator_settings.Parser.ParseJson(settings);
            MsSqlDesignGeneratorSettings res = MsSqlDesignGeneratorSettings.ConvertToVM(proto);
            return res;
        }
        public ILoggerFactory LoggerFactory
        {
            get { return _LoggerFactory; }
            set
            {
                _LoggerFactory = value;
                Logger = _LoggerFactory.CreateLogger<MsSqlDesignGenerator>();
            }
        }
        private ILoggerFactory _LoggerFactory;
        public void SetServices(ServiceProvider serviceProvider)
        {
            //this.LoggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //reporter = serviceProvider.GetService<IOperationReporter>();
            //candidateNamingService = serviceProvider.GetService<ICandidateNamingService>();
            //pluralizer = serviceProvider.GetService<IPluralizer>();
            //cSharpUtilities = serviceProvider.GetService<ICSharpUtilities>();
            //scaffoldingTypeMapper = serviceProvider.GetService<IScaffoldingTypeMapper>();
            //changeDetector = serviceProvider.GetService<IChangeDetector>();
        }

        //IOperationReporter reporter;
        //ICandidateNamingService candidateNamingService;
        //IPluralizer pluralizer;
        //ICSharpUtilities cSharpUtilities;
        //IScaffoldingTypeMapper scaffoldingTypeMapper;
        //IChangeDetector changeDetector;

        public int GetMigrationVersion()
        {
            throw new NotImplementedException();
        }
        //public DatabaseModel GetDbModel(List<string> schemas, List<string> tables)
        //{
        //    var databaseModelFactory = new SqlServerDatabaseModelFactory(
        //        new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
        //            _LoggerFactory,
        //            new LoggingOptions(),
        //            MsSqlMigratorDiagnostic));

        //    var databaseModel = databaseModelFactory.Create(this.ConnectionString, tables, schemas);
        //    return databaseModel;
        //}
        public string GetLastModel()
        {
            string res = null;
            return res;
        }

        //private static T ExecuteScalar<T>(DbConnection connection, string sql, params object[] parameters)
        //    => Execute(connection, command => (T)command.ExecuteScalar(), sql, false, parameters);
        //private static T Execute<T>(
        //    DbConnection connection, Func<DbCommand, T> execute, string sql,
        //    bool useTransaction = false, object[] parameters = null)
        //    => TestEnvironment.IsSqlAzure
        //        ? new TestSqlServerRetryingExecutionStrategy().Execute(
        //            new
        //            {
        //                connection,
        //                execute,
        //                sql,
        //                useTransaction,
        //                parameters
        //            },
        //            state => ExecuteCommand(state.connection, state.execute, state.sql, state.useTransaction, state.parameters))
        //        : ExecuteCommand(connection, execute, sql, useTransaction, parameters);

        //private static T ExecuteCommand<T>(
        //    DbConnection connection, Func<DbCommand, T> execute, string sql, bool useTransaction, object[] parameters)
        //{
        //    if (connection.State != ConnectionState.Closed)
        //    {
        //        connection.Close();
        //    }

        //    connection.Open();
        //    try
        //    {
        //        using (var transaction = useTransaction ? connection.BeginTransaction() : null)
        //        {
        //            T result;
        //            using (var command = CreateCommand(connection, sql, parameters))
        //            {
        //                command.Transaction = transaction;
        //                result = execute(command);
        //            }

        //            transaction?.Commit();

        //            return result;
        //        }
        //    }
        //    finally
        //    {
        //        if (connection.State != ConnectionState.Closed)
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
        protected static string EOL => Environment.NewLine;
        public void UpdateToModel(string connectionString, MigrationOperation[] operations, DiffModel diffModel, Func<bool> onNeedDbCreate, Action<Exception> onError)
        {
            try
            {

                //if (_LoggerFactory == null)
                //    throw new Exception();
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new ArgumentException("connection string is empty");
                if (onNeedDbCreate == null)
                    throw new ArgumentException("onDbCreate Action is null");
                //if (onCreatePrevIConfig == null)
                //    throw new ArgumentException("onCreatePrevIConfig Func is null");
                //if (model == null)
                //    throw new ArgumentException("model is null");
                var options = new DbContextOptionsBuilder().UseSqlServer(connectionString,(b)=> {
                    b.EnableRetryOnFailure();
                }).Options;

                var service_provider = services.BuildServiceProvider();


                using (var context = new vDbContext(connectionString, service_provider, options))
                {
                    if (!context.Database.CanConnect())
                    {
                        if (!onNeedDbCreate())
                            return;
                        context.Database.EnsureCreated();
                    }

                    //context.Database.EnsureClean();
                    // IModificationCommandBatchFactory

                    var creator = context.Database.GetService<IRelationalDatabaseCreator>();
                    var sqlGenerator = context.Database.GetService<IMigrationsSqlGenerator>();
                    var executor = context.Database.GetService<IMigrationCommandExecutor>();
                    var connection = context.Database.GetService<IRelationalConnection>();
                    var sqlBuilder = context.Database.GetService<IRawSqlCommandBuilder>();
                    var loggerFactory = context.Database.GetService<ILoggerFactory>();

                    //var databaseModelFactory = context.Database.GetService<IDatabaseModelFactory>();
                    var databaseModelFactory = service_provider.GetRequiredService<IDatabaseModelFactory>();
                    var modelFactory = service_provider.GetRequiredService<IScaffoldingModelFactory>();
                    var differ = context.Database.GetService<IMigrationsModelDiffer>();
                    //var differ = service_provider.GetRequiredService<IMigrationsModelDiffer>();

                    // Database Model
                    List<string> schemas = new List<string>();
                    List<string> tables = new List<string>();
                    var databaseModel = databaseModelFactory.Create(connectionString, tables, schemas);

                    var source_model = modelFactory.Create(databaseModel, true);
                    DbModelCreator target = new DbModelCreator();
                    target.Visit(diffModel, new ModelBuilder(new ConventionSet()));

                    var diffs = differ.GetDifferences(source_model, target.Model);
                    var commands = sqlGenerator.Generate(diffs);
                    executor.ExecuteNonQuery(commands, connection);


                    // ModificationCommand - data modification
                    //var batchFactory = service_provider
                    //    .GetRequiredService<IModificationCommandBatchFactory>();
                    //var batch = batchFactory.Create();
                    //foreach (var t in diffs)
                    //{
                    //    if (t.IsDestructiveChange)
                    //        throw new Exception();
                    //    batch.AddCommand();
                    //        }


                    //    modelBuilder => modelBuilder.Entity("Person").Property<byte[]>("RowVersion"),
                    //new AddColumnOperation
                    //{
                    //    Table = "Person",
                    //    Name = "RowVersion",
                    //    ClrType = typeof(byte[]),
                    //    IsRowVersion = true,
                    //    IsNullable = true
                    //});


                    //((Model)model).SetProductVersion("1.1.2");

                    //var entityType = builder.Entity("Blog").Metadata;
                    //var property = builder.Entity("Blog").Property(typeof(int), "Id").Metadata;
                    //var key = builder.Entity("Blog").HasKey("Id").Metadata;

                    //builder.Entity("Post").Property(typeof(int), "BlogId");
                    //var foreignKey = builder.Entity("Blog").HasMany("Posts").WithOne("Blog").HasForeignKey("BlogId").Metadata;
                    //var nav1 = foreignKey.DependentToPrincipal;
                    //var nav2 = foreignKey.PrincipalToDependent;

                    //var index = builder.Entity("Post").HasIndex("BlogId").Metadata;

                    //var opers = new List<MigrationOperation>();
                    //opers.Add(new CreateTableOperation() { Name = "Blog" });

                    //var batch = service_provider.GetRequiredService<IMigrationsSqlGenerator>()
                    //                .Generate(opers, model);

                    //Sql = string.Join(
                    //    "GO" + EOL + EOL,
                    //    batch.Select(b => b.CommandText));


                    //context.Database.Migrate();

                    //var creator = (SqlServerDatabaseCreator)service_provider.GetService<IRelationalDatabaseCreator>();
                    //if (creator.CanConnect())
                    //{

                    //}
                    //else
                    //{

                    //}
                }


                //using (var context = new BloggingContext(Fixture.TestStore.AddProviderOptions(new DbContextOptionsBuilder()).Options))
                //{
                //    var creator = (SqlServerDatabaseCreator)context.GetService<IRelationalDatabaseCreator>();
                //    creator.RetryTimeout = TimeSpan.FromMinutes(10);

                //    await context.Database.MigrateAsync();

                //    Assert.True(creator.Exists());
                //}



                //var sqlServerServiceProvider = new ServiceCollection()
                //    .AddEntityFrameworkSqlServer()
                //    .BuildServiceProvider();

                //var Connection = new SqlConnection(connectionString);


                //using (var connection = new SqlConnection(CreateDependencies(options)))
                //{
                //    using (var master = connection.CreateMasterConnection())
                //    {
                //        Assert.Equal(@"Host=localhost;Database=template0;Username=some_user;Password=some_password;Pooling=False", master.ConnectionString);
                //    }
                //}
                //// check if DB need to be created
                //#region DB creation

                //var db_creator = sqlServerServiceProvider.GetService<SqlServerDatabaseCreator>();

                //db_creator.

                //db_creator.CanConnect();



                //SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(connectionString);
                //csb.InitialCatalog = "master";
                //csb.ConnectTimeout = 30;
                //string master_connection = csb.ConnectionString;
                //using (var master = new SqlConnection(master_connection))
                //{
                //    if (ExecuteScalar<int>(master, $"SELECT COUNT(*) FROM sys.databases WHERE name = N'{Name}'") == 0)
                //    {
                //        if (!onDbCreate())
                //            return;
                //        // Delete the database to ensure it's recreated with the correct file path
                //        //DeleteDatabase();
                //    }
                //    //ExecuteNonQuery(master, GetCreateDatabaseStatement(Name, _fileName));
                //    //WaitForExists((SqlConnection)Connection);
                //}

                //string json = null;
                //IConfig prev_model = onCreatePrevIConfig(json);







                //var dbModelFactory = new SqlServerDatabaseModelFactory(
                //                    new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                //                        _LoggerFactory,
                //                        new LoggingOptions(),
                //                        MsSqlMigratorDiagnostic));

                //var typeMappingSource = new SqlServerTypeMappingSource(
                //    new TypeMappingSourceDependencies(
                //        new ValueConverterSelector(new ValueConverterSelectorDependencies()),
                //        Array.Empty<ITypeMappingSourcePlugin>()
                //    ),
                //    new RelationalTypeMappingSourceDependencies(Array.Empty<IRelationalTypeMappingSourcePlugin>())
                //);

                //var dbModel = dbModelFactory.Create(connectionString, new List<string>(), new List<string>());
                //RelationalScaffoldingModelFactory relFactory = new RelationalScaffoldingModelFactory(
                //    reporter,
                //    candidateNamingService,
                //    pluralizer,
                //    cSharpUtilities,
                //    scaffoldingTypeMapper);
                //var modelSource = relFactory.Create(dbModel, true);

                //var ctx = TestHelpers.CreateContext(
                //    TestHelpers.AddProviderOptions(new DbContextOptionsBuilder())
                //        .UseModel(model).EnableSensitiveDataLogging().Options);

                //var differ = new MigrationsModelDiffer(
                //    new SqlServerTypeMappingSource(
                //            new TypeMappingSourceDependencies(
                //                new ValueConverterSelector(new ValueConverterSelectorDependencies()),
                //                Array.Empty<ITypeMappingSourcePlugin>()
                //            ),
                //            new RelationalTypeMappingSourceDependencies(Array.Empty<IRelationalTypeMappingSourcePlugin>())),
                //    new SqlServerMigrationsAnnotationProvider(
                //        new MigrationsAnnotationProviderDependencies()),
                //    changeDetector,
                //    ctx.GetService<StateManagerDependencies>(),
                //    ctx.GetService<CommandBatchPreparerDependencies>());
            }
            catch (Exception ex)
            {
                if (onError != null)
                    onError(ex);
                else
                    throw;
            }
        }

    }
}
