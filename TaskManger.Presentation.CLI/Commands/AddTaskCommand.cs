using Spectre.Console.Cli;

namespace TaskManger.Presentation.CLI.Commands;

public class AddTaskSettings : CommandSettings
{
    [CommandOption("-c|--content <CONTENT>")]
    public string Content { get; set; }
    
    [CommandOption("-p|--parent-id <PARENT-ID>")]
    public string ParentTaskId { get; set; }
    
    [CommandOption("-d")]
    public DateTime DueDate { get; set; }
}
public class AddTaskCommand: Command<AddTaskSettings>
{
    public override int Execute(CommandContext context, AddTaskSettings settings)
    {
        return 0;
    }
}