namespace TaskManager.Domain.Pipeline;

public interface IMiddleware<TReq,TRes>
{
    public TRes Next(TReq args, Func<TReq, TRes> next);
}