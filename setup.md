## Dotnet tool
Pour installer le projet faire les commandes suivantes :

La video explicative se trouve a cet url : [https://www.youtube.com/watch?v=_MDr0wsa-l8

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
Une commande devrait s'afficher pour permettre d'ajouter l'outil au filepath. Executer la commande pour ensuite pour appeler le CLI.


Uninstall tool :
````shell
dotnet tool uninstall --global  TaskManager.Presentation.CLI
````

