namespace TaskManager.Domain.Contracts;

public interface Mapper<From, To>
{
    To Map(From source);
}