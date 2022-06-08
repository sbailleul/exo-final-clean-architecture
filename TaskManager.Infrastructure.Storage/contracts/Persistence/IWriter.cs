namespace TaskManager.Infrastructure.Storage.contracts.Persistence;

public interface IWriter<T>
{
    void Set(T toWrite);
    void Delete(int toDelete);
}