using TaskManager.Domain;

namespace TaskManger.Presentation.CLI.Logging;

public class LoggerMiddleware<TTaskRequest, TInnerRequest, TResult> : IMiddleware<TTaskRequest, TResult>
    where TTaskRequest : TaskRequest<TInnerRequest>
{
    private readonly IMiddleware<TInnerRequest, TResult> _next;

    public LoggerMiddleware(IMiddleware<TInnerRequest, TResult> next)
    {
        _next = next;
    }

    public TResult Next(TTaskRequest request)
    {
        try
        {
            var result = _next.Next(request.InnerRequest);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }
}