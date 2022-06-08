using TaskManager.Domain.Task;
using TaskManager.Domain.Task.WriteDtos;
using TaskManager.Infrastructure.Storage.contracts.JsonPersistence;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Storage.contracts.Tasks;

public class TaskRepository : ITasks
{
    private JsonPersistence<TaskEntity> tasks = new(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.consoleagenda/data.json");
    private TaskReadAdapter _readAdapter = new();
    private TaskWriteAdapter _writeAdapter = new();

    public int GetNextId()
    {
        return tasks.FindAll().Select(entity => entity.Id).Max() + 1;
    }

    public Task<int> SetAsync(TaskWriteDto dto)
    {
        var entity = tasks.Find(dto.Id);

        return Task.FromResult(entity == null ? Create(dto) : Update(dto, entity));
    }

    private int Create(TaskWriteDto dto)
    {
        tasks.Set(_writeAdapter.Adapt(dto));
        return dto.Id;
    }

    private int Update(TaskWriteDto dto, TaskEntity entity)
    {
        entity.State = dto.State;
        entity.Description = dto.Description;
        entity.Tag = dto.Tag;
        entity.CloseDate = dto.CloseDate;
        entity.SubTasks = dto.Children.Select(_writeAdapter.Adapt);
        entity.Created = dto.CreationDate;
        entity.DueDate = dto.DueDate;
        
        tasks.Set(entity);
        return dto.Id;
    }

    public Task<IEnumerable<TaskReadDto>> GetAll()
    {
        return Task.FromResult(tasks.FindAll().Select(_readAdapter.Adapt));
    }

    public Task<TaskReadDto?> FindOne(int id)
    {
        var task = tasks.Find(id);
        return Task.FromResult(task == null ? null : _readAdapter.Adapt(task));
    }

    public void Delete(TaskWriteDto taskDto)
    {
        var task = tasks.Find(taskDto.Id);
        if (task != null)
        {
            tasks.Delete(taskDto.Id);
        }
    }
}