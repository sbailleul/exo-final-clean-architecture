using Spectre.Console.Cli;

namespace TaskManger.Presentation.CLI.Commands;

public class UpdateTaskSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")] public string Id { get; set; }

    [CommandOption("-c|--content <CONTENT>")]
    public string Content { get; set; }

    [CommandOption("-d|--due-date <DUE-DATE>")]
    public DateTime DueDate { get; set; }

    [CommandOption("-s|--status <STATUS>")]
    public string Status { get; set; }
}

public class UpdateTaskCommand : Command<UpdateTaskSettings>
{
    public override int Execute(CommandContext context, UpdateTaskSettings settings)
    {
        return 1;
    }
}