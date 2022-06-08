namespace TaskManager.Domain;

public interface IMiddleware<TRequest, TResult>
{
    public TResult Next(TRequest request);
}