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

echo **** model vsharpstudio
%2 model vsharpstudio %1vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs vSharpStudio.vm.ViewModels %1doc\
echo **** interface vsharpstudio
%2 interface vsharpstudio %1vSharpStudio.common\ModelInterfaces.cs vSharpStudio.common %1doc\

echo **** model conn_mssql
%2 model conn_mssql %1dbmodels\DbModel.MsSql\SettingsViewModels.cs vPlugin.DbModel.MsSql %1doc\
echo **** interface conn_mssql
%2 interface conn_mssql %1dbmodels\DbModel.MsSql\SettingsInterfaces.cs vPlugin.DbModel.MsSql %1doc\


