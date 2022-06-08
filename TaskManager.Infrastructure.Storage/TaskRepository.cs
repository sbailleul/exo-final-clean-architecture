using TaskManager.Domain.Task.WriteDtos;
using TaskManager.Infrastructure.Storage.contracts.JsonPersistence;

namespace TaskManager.Infrastructure.Storage;

public class TaskRepository
{
    private JsonPersistence<TaskEntity> tasks = new ();
    private TaskReadAdapter _readAdapter = new ();
    private TaskWriteAdapter _writeAdapter = new ();

    public int GetNextId()
    {
        return tasks.FindAll().Select(entity => entity.Id).Max() + 1;
    }

    public void Set(TaskWriteDto dto)
    {
        var entity = tasks.Find(dto.Id);
        
        if (entity == null)
        {
            Create(dto);
        }
        else
        {
            Update(dto, entity);
        }
    }

    private void Create(TaskWriteDto dto)
    {
        tasks.Set(_writeAdapter.Adapt(dto));
    }

    private void Update(TaskWriteDto dto, TaskEntity entity)
    {
        tasks.Set(entity);
    }

    private IEnumerable<TaskWriteDto> GetAll()
    {
        return tasks.FindAll().Select(_readAdapter.Adapt);
    }
    
    private TaskWriteDto? Get(int id)
    {
        var task = tasks.Find(id);
        return task == null ? null : _readAdapter.Adapt(task);
    }
    
    private void Delete(TaskWriteDto taskDto)
    {
        var task = tasks.Find(taskDto.Id);
        if (task != null)
        {
            tasks.Delete(taskDto.Id);
        }
    }
    
}