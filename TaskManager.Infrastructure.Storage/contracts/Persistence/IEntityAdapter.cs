namespace TaskManager.Infrastructure.Storage.contracts.Persistence;

public interface IEntityAdapter<From, To>
{
    To Adapt(From entity);
}