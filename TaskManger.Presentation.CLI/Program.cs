// See https://aka.ms/new-console-template for more information

using TaskManger.Presentation.CLI;

var parser = new CommandParser();
parser.Run(Environment.GetCommandLineArgs());