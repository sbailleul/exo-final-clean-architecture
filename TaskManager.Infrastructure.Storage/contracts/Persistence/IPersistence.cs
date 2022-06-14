namespace TaskManager.Infrastructure.Storage.contracts.Persistence;

public interface IPersistence<T>: IReader<T>, IWriter<T> where T: IEntity
{ }