using Spectre.Console.Cli;
using TaskManger.Presentation.CLI.Commands;

namespace TaskManger.Presentation.CLI;

public class CommandParser
{
    private readonly CommandApp _app;

    public CommandParser()
    {
        _app = new CommandApp();
        Configure();

    }
    private void Configure()
    {
        _app.Configure(config =>
        {
            config.AddCommand<ListTasksCommand>("list");
            config.AddCommand<AddTaskCommand>("add");
        });
    }

    public int Run(string[] args)
    {
        return _app.Run(args);
    }
}