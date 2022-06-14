using Spectre.Console.Cli;
using TaskManager.Domain.UseCases;
using TaskManager.Infrastructure.Storage.contracts.Tasks;

namespace TaskManger.Presentation.CLI.Commands;

public class ListTasksCommand : Command
{
    public override int Execute(CommandContext context)
    {
        var taskViews = new GetTasks(new TaskRepository())
            .Query().Result;
        
        
        
        return 0;
    }
}