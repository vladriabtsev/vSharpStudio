rem %1 - solution directory
rem https://www.tutorialspoint.com/batch_script/batch_script_variables.htm

SET protoc=%1protoc\bin\protoc.exe
SET protdoc=%1protoc\bin\protoc-gen-doc.exe


SET prname=vsharpstudio
echo **** vsharpstudio html
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\%prname%.proto  
echo **** vsharpstudio markdown
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\%prname%.proto  
echo **** vsharpstudio json
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\%prname%.proto  

SET prname=conn_mssql
echo **** conn_mssql html
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\%prname%.proto  
echo **** conn_mssql markdown
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\%prname%.proto  
echo **** conn_mssql json
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\%prname%.proto  

rem SET gen=%2
rem echo *** %gen%
rem echo **** model vsharpstudio
rem %gen% model vsharpstudio %1vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs vSharpStudio.vm.ViewModels %1doc\
rem echo **** interface vsharpstudio
rem %gen% interface vsharpstudio %1vSharpStudio.common\ModelInterfaces.cs vSharpStudio.common %1doc\

rem echo **** model conn_mssql
rem %gen% model conn_mssql %1dbmodels\DbModel.MsSql\SettingsViewModels.cs vPlugin.DbModel.MsSql %1doc\
rem echo **** interface conn_mssql
rem %gen% interface conn_mssql %1dbmodels\DbModel.MsSql\SettingsInterfaces.cs vPlugin.DbModel.MsSql %1doc\


