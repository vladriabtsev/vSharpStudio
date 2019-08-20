using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.Migration;
using vSharpStudio.std;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    //TODO report based on FlowDocument https://github.com/rodrigovedovato/FlowDocumentReporting
    public partial class ConfigRoot2 : Config
    {
        public static ILogger Logger = ApplicationLogging.CreateLogger<ConfigRoot2>();
        public ConfigRoot2()
        {
            // https://msdn.microsoft.com/en-us/magazine/mt830355.aspx
            // https://msdn.microsoft.com/en-us/magazine/mt694089.aspx
            Logger.LogInformation("");
        }
        public ConfigRoot2(string pathToProjectWithConnectionString, string connectionStringName)
        {
            this.DbSettings.PathToProjectWithConnectionString = pathToProjectWithConnectionString;
//            this.ConnectionStringName = connectionStringName;
            //InitMigration();
        }
        public ConfigRoot2(string configJson) : base(configJson)
        {
            //InitMigration();
        }

        #region Full Validation

        //async public Task ValidateSubTreeFromNode(IAccept node)
        //{
        //    if (cancellationSourceForValidatingFullConfig != null)
        //    {
        //        cancellationSourceForValidatingFullConfig.Cancel();
        //    }
        //    this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
        //    var token = cancellationSourceForValidatingFullConfig.Token;
        //    // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        //    // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
        //    // https://devblogs.microsoft.com/pfxteam/asynclazyt/
        //    // https://github.com/StephenCleary/AsyncEx
        //    // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
        //    try
        //    {
        //        var res = await ValidateSubTreeFromNode(node, token).ConfigureAwait(continueOnCapturedContext: false);
        //        if (!token.IsCancellationRequested)
        //            ListValidationMessages = res;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //Task<SortedObservableCollection<ValidationMessage>> ValidateSubTreeFromNode(IAccept node, CancellationToken cancellationToken = default)
        //{
        //    var task = new Task<SortedObservableCollection<ValidationMessage>>(() =>
        //    {
        //        var visitor = new TreeNodeValidatorVisitor(cancellationToken);
        //        node.Accept(visitor);
        //        return visitor.Result;
        //    });
        //    return task;
        //}

        private CancellationTokenSource cancellationSourceForValidatingFullConfig = null;
        public new async Task ValidateSubTreeFromNodeAsync(ITreeConfigNode node)
        {
            // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
            // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
            // https://devblogs.microsoft.com/pfxteam/asynclazyt/
            // https://github.com/StephenCleary/AsyncEx
            // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
            await Task.Run(() =>
            {
                ValidateSubTreeFromNode(node);
            }).ConfigureAwait(false); // not keeping context because doing nothing after await
        }
        public new void ValidateSubTreeFromNode(ITreeConfigNode node, ILogger logger = null)
        {
            if (node == null)
                return;
            if (cancellationSourceForValidatingFullConfig != null)
            {
                cancellationSourceForValidatingFullConfig.Cancel();
                //                if (logger != null && logger.IsEnabled)
                if (logger != null)
                    logger.LogInformation("=== Cancellation request ===");
            }
            this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
            var token = cancellationSourceForValidatingFullConfig.Token;

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
        //public SortedObservableCollection<ValidationMessage> ListAllValidationMessages
        //{
        //    set
        //    {
        //        if (_ListAllValidationMessages != null)
        //            _ListAllValidationMessages.Clear();
        //        _ListAllValidationMessages = value;
        //        NotifyPropertyChanged();
        //    }
        //    get { return _ListAllValidationMessages; }
        //}
        //private SortedObservableCollection<ValidationMessage> _ListAllValidationMessages = null;

        #endregion Full Validation


    }
}
