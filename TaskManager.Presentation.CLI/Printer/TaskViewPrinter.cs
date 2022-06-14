using TaskManager.Domain.UseCases.Results;
using TaskManger.Presentation.CLI.Contracts;

namespace TaskManger.Presentation.CLI.Printer;

public class TaskViewPrinter: IPrinter<TaskView>
{
    public void Print(TaskView printable)
    {
        Console.WriteLine($"Tâche {printable.Id}:");
        PrintOnPresent("Description", printable.Description);
        PrintOnPresent("Tag", printable.Tag);
        PrintOnPresent("Date de création", printable.CreationDate);
        PrintOnPresent("Échéance", printable.DueDate);
        PrintOnPresent("Date de validation", printable.CloseDate);
        PrintOnUnempty("Taches enfantes", printable.Children);
    }

    public void PrintOnPresent(string name, Object? printable)
    {
        if (printable is not null) Console.WriteLine($"\t {name}: {printable}.");
    }
    
    public void PrintOnUnempty(string name, List<int> printables)
    {
        if (printables.Count > 0) Console.WriteLine($"\t {name}: {string.Join(",", printables)}.");
    }
}
