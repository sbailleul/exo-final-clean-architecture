using Spectre.Console.Cli;

namespace TaskManger.Presentation.CLI.Commands;

public class RemoveTaskSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")] public string Id { get; set; }
}

public class RemoveTaskCommand : Command<RemoveTaskSettings>
{
    public override int Execute(CommandContext context, RemoveTaskSettings settings)
    {
        return 0;
    }
}