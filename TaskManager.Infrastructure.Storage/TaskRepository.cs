using TaskManager.Infrastructure.Storage.contracts;
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

    public void Set(TaskDto dto)
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

    private void Create(TaskDto dto)
    {
        tasks.Set(_writeAdapter.Adapt(dto));
    }

    private void Update(TaskDto dto, TaskEntity entity)
    {
        tasks.Set(entity);
    }

    private IEnumerable<TaskDto> GetAll()
    {
        return tasks.FindAll().Select(_readAdapter.Adapt);
    }
    
    private TaskDto? Get(int id)
    {
        var task = tasks.Find(id);
        return task == null ? null : _readAdapter.Adapt(task);
    }
}