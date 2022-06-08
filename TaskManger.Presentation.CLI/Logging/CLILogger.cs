using Spectre.Console.Cli;
using TaskManager.Domain;
using TaskManager.Domain.Pipeline;

namespace TaskManger.Presentation.CLI.Logging;

public class CLILogger : IMiddleware<string[], int>
{
    public int Next(string[] req, Func<string[], int> func)
    {
        try
        {
            var res = func(req);
            Console.WriteLine(string.Join(' ', req));
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }
}