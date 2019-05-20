rem %1 - GenFromProto.exe
rem %2 - solution directory
rem https://www.tutorialspoint.com/batch_script/batch_script_variables.htm

%1 vsharpstudio %2vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs Proto.Config
%1 conn_mssql %2dbmodels\DbModel.MsSql\ParametersViewModels.cs Proto.Config.Connection
