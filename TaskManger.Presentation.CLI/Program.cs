// See https://aka.ms/new-console-template for more information

using TaskManger.Presentation.CLI;
using TaskManger.Presentation.CLI.Logging;

var logger = new CLILogger();
var parser = new CommandParser();
logger.Next(Environment.GetCommandLineArgs(), args => parser.Run(args));

