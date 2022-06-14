using Spectre.Console.Cli;
using TaskManager.Domain.UseCases;
using TaskManager.Infrastructure.Storage.contracts.Tasks;
using TaskManger.Presentation.CLI.Printer;

namespace TaskManger.Presentation.CLI.Commands;

public class ListTasksCommand : Command
{
    public override int Execute(CommandContext context)
    {
        var taskViews = new GetTasks(new TaskRepository())
            .Query().Result;

        var printer = new TaskViewPrinter();

        foreach (var view in taskViews)
        {
            printer.Print(view);
        }
        
        return 0;
    }
}