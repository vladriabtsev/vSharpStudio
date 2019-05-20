rem %1 - solution directory
rem https://www.tutorialspoint.com/batch_script/batch_script_variables.htm

SET prname="vsharpstudio"
%1protoc\bin\protoc.exe  -I=%1proto --plugin=protoc-gen-doc=%1protoc\bin\protoc-gen-doc.exe --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\\%prname%.proto  
%1protoc\bin\protoc.exe  -I=%1proto --plugin=protoc-gen-doc=%1protoc\bin\protoc-gen-doc.exe --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\\%prname%.proto  
%1protoc\bin\protoc.exe  -I=%1proto --plugin=protoc-gen-doc=%1protoc\bin\protoc-gen-doc.exe --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\\%prname%.proto  


SET prname="conn_mssql"
%1protoc\bin\protoc.exe  -I=%1proto --plugin=protoc-gen-doc=%1protoc\bin\protoc-gen-doc.exe --doc_out=%1doc --doc_opt=html,%prname%.html %1proto\\%prname%.proto  
%1protoc\bin\protoc.exe  -I=%1proto --plugin=protoc-gen-doc=%1protoc\bin\protoc-gen-doc.exe --doc_out=%1doc --doc_opt=markdown,%prname%.md %1proto\\%prname%.proto  
%1protoc\bin\protoc.exe  -I=%1proto --plugin=protoc-gen-doc=%1protoc\bin\protoc-gen-doc.exe --doc_out=%1doc --doc_opt=json,%prname%.json %1proto\\%prname%.proto  
