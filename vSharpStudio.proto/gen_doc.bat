
SET solpath=%1
echo **** solution directory %solpath%
rem %1 - solution directory
rem %2 - GenFromProto.exe path
rem %3 - protoc.exe path
rem https://www.tutorialspoint.com/batch_script/batch_script_variables.htm
rem https://github.com/pseudomuto/protoc-gen-doc

SET protoc=%1protoc\protoc.exe
SET protdoc=%1protoc\protoc-gen-doc.exe

EXIT

SET prname=vsharpstudio
echo **** vsharpstudio html
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\%prname%.proto  
echo **** vsharpstudio markdown
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\%prname%.proto  
echo **** vsharpstudio json
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\%prname%.proto  

SET prname=vsharpstudioshared
echo **** vsharpstudioshared html
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\%prname%.proto  
echo **** vsharpstudioshared markdown
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\%prname%.proto  
echo **** vsharpstudioshared json
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\%prname%.proto  

SET prname=plugin_sample
echo **** plugin_sample html
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\%prname%.proto  
echo **** plugin_sample markdown
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\%prname%.proto  
echo **** plugin_sample json
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\%prname%.proto  

SET prname=plugin_sample2
echo **** plugin_sample html
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\%prname%.proto  
echo **** plugin_sample markdown
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\%prname%.proto  
echo **** plugin_sample json
%protoc%  -I=%1proto --plugin=protoc-gen-doc=%protdoc% --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\%prname%.proto  

SET gen=%2
rem echo *** %gen%
echo **** model vsharpstudio
%gen% -r -m -p vsharpstudio -o %1vSharpStudio.vm\ViewModels\Generated\ProtoViewModels.cs -n vSharpStudio.vm.ViewModels -d %1doc\ -b ConfigObjectVmGenSettings
echo **** interface vsharpstudio
%gen% -r -i -p vsharpstudio -o %1vSharpStudio.common\ModelInterfaces.cs -n vSharpStudio.common -d %1doc\

echo **** model vsharpstudioshared
%gen% -r -m -p vsharpstudioshared -o %1vSharpStudio.vm\ViewModels\Generated\ProtoViewModelsShared.cs -n vSharpStudio.vm.ViewModels.Shared -d %1doc\ -b ConfigObjectVmGenSettings
echo **** interface vsharpstudio
%gen% -r -i -p vsharpstudioshared -o %1vSharpStudio.common\ModelInterfacesShared.cs -n vSharpStudio.common -d %1doc\

echo **** model plugin_sample
%gen% -m -p plugin_sample -o %1vPlugin.Sample\SettingsViewModels.cs -n vPlugin.Sample -d %1doc\ 
echo **** interface plugin_sample
%gen% -i -p plugin_sample -o %1vPlugin.Sample\SettingsInterfaces.cs -n vPlugin.Sample -d %1doc\

echo **** model plugin_sample2
%gen% -m -p plugin_sample2 -o %1vPlugin.Sample2\SettingsViewModels.cs -n vPlugin.Sample2 -d %1doc\ 
echo **** interface plugin_sample
%gen% -i -p plugin_sample2 -o %1vPlugin.Sample2\SettingsInterfaces.cs -n vPlugin.Sample2 -d %1doc\

