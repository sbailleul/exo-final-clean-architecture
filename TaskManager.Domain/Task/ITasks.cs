using TaskManager.Domain.Task.WriteDtos;

namespace TaskManager.Domain.Task;

public interface ITasks
{
    Task<IEnumerable<TaskReadDto>> GetAll();

    public int GetNextId();

    Task<TaskReadDto?> FindOne(int id);

    Task<int> SetAsync(TaskWriteDto taskWriteDto);
    void Delete(TaskWriteDto taskDto);
}