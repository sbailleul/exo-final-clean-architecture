// See https://aka.ms/new-console-template for more information

using TaskManager.Infrastructure;
using TaskMananger.Infrastructure.Logging;
using TaskManger.Presentation.CLI;

var consoleDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".consoleagenda");
Directory.CreateDirectory(consoleDirectory);

var logger = new Logger(Path.Join(consoleDirectory, "log.txt"), new TimeGenerator());

var parser = new CommandParser();

logger.Next(Environment.GetCommandLineArgs(), args => parser.Run(args.Skip(1).ToArray()));