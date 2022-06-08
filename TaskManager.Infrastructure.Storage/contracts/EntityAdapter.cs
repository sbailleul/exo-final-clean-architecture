namespace TaskManager.Infrastructure.Storage.contracts;

public interface EntityAdapter<From, To>
{
    To Adapt(From entity);
}