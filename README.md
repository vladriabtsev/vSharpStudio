# Business model editor and code generator
## Installation

Download latest release [here](https://github.com/vladriabtsev/vSharpStudio/releases). 
Unzip content of downloaded file and run vSharpStudio.exe.
![vSharpStudio initial screen](readme/images/install-first-start.png)

## Quick Start

### Create new Visual Studio solution

Create Visual Studio 'AQuickStartSample' solution with C# console 'AQuickStartSample' 
project and C# class library 'DbLayer' project.

![Initial sample solution](readme/images/sample-solution1.png).

For 'AQuickStartSample' project add reference to 'DbLayer' project.

### Setup vSharpStudio for code generation

Business model editor is storing model in a file with 'vcfg' extension.

Click create new configuration button ![Create new configuration](readme/images/button-new-configuration.png).
Save as dialog will be opened to select folder for new configuration file.

![vSharpStudio initial screen](readme/images/save-configuration-dialog.png)

Choose solution folder, enter file name for configuration, and click button Save to save it.

![vSharpStudio initial screen](readme/images/configuration-editor-empty.png)

Inside business editor select node 'Apps'

![vSharpStudio initial screen](readme/images/configuration-app-empty.png)

Click insert child object ![Create new configuration](readme/images/model-add-child.png) to create Solution object for model. 

![vSharpStudio initial screen](readme/images/configuration-solution-empty.png)

Select solution file. Create child object of model solution for 'DbLayer' project.

![vSharpStudio initial screen](readme/images/configuration-app-sol-prj-gen.png)

### Model Editing and Code Generation

