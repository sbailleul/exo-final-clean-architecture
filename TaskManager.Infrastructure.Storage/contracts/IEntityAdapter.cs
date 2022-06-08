namespace TaskManager.Infrastructure.Storage.contracts;

public interface IEntityAdapter<From, To>
{
    To Adapt(From entity);
}