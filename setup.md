## Dotnet tool
Pour installer le projet faire les commandes suivantes :



Package cli project :
```shell
Sur windows ->
dotnet pack  .\TaskManager.Presentation.CLI

Ailleurs ->
dotnet pack  TaskManager.Presentation.CLI
```
Install tool :
````shell
dotnet tool install --global --add-source ./nupkg TaskManager.Presentation.CLI
````
Uninstall tool :
````shell
dotnet tool uninstall --global  TaskManager.Presentation.CLI
````