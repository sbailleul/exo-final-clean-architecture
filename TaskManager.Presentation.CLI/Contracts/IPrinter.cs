namespace TaskManger.Presentation.CLI.Contracts;

public interface IPrinter<Printable>
{
    void Print(Printable printable);
}