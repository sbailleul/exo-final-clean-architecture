namespace TaskManager.Infrastructure.Storage.contracts.Persistence;

public interface IReader<T>
{
    IEnumerable<T> FindAll();
    T? Find(int id);
}