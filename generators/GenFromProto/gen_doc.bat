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

SET prname=plugin_sample
echo **** plugin_sample html
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\%prname%.proto  
echo **** plugin_sample markdown
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\%prname%.proto  
echo **** plugin_sample json
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\%prname%.proto  

SET gen=%2
rem echo *** %gen%
echo **** model vsharpstudio
%gen% model vsharpstudio %1vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs vSharpStudio.vm.ViewModels %1doc\ ConfigObjectSubBase
echo **** interface vsharpstudio
%gen% interface vsharpstudio %1vSharpStudio.common\ModelInterfaces.cs vSharpStudio.common %1doc\

echo **** model plugin_sample
%gen% model plugin_sample %1vPlugin.Sample\SettingsViewModels.cs vPlugin.Sample %1doc\ 
echo **** interface plugin_sample
%gen% interface plugin_sample %1vPlugin.Sample\SettingsInterfaces.cs vPlugin.Sample %1doc\


