rem %1 - GenFromProto.exe
rem %2 - interface/model
rem %3 - solution directory
rem %4 - Namespace
rem https://www.tutorialspoint.com/batch_script/batch_script_variables.htm

echo **** model vsharpstudio
%1 model vsharpstudio %2vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs Proto.Config %2doc\
echo **** interface vsharpstudio
%1 interface vsharpstudio %2vSharpStudio.common\ModelInterfaces.cs Proto.Config %2doc\

echo **** model conn_mssql
%1 model conn_mssql %2dbmodels\DbModel.MsSql\SettingsViewModels.cs Proto.Config.Connection %2doc\
echo **** interface conn_mssql
%1 interface conn_mssql %2dbmodels\DbModel.MsSql\SettingsInterfaces.cs Proto.Config.Connection %2doc\

rem model vsharpstudio ..\..\..\..\vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs Proto.Config
rem interface vsharpstudio ..\..\..\..\vSharpStudio.common\ModelInterfaces.cs Proto.Config