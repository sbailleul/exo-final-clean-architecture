using TaskManager.Domain.Task.WriteDtos;
using TaskManager.Infrastructure.Storage.contracts.Persistence;

namespace TaskManager.Infrastructure.Storage.contracts.Tasks;

public class TaskReadAdapter : IEntityAdapter<TaskEntity, TaskReadDto>
{
    public TaskReadDto Adapt(TaskEntity entity) =>
        new(entity.Id, 
            entity.Description,
            entity.Tag, 
            entity.State,
            entity.Created, 
            entity.DueDate,
            entity.CloseDate, 
            entity.SubTasks.Any()
                ? entity.SubTasks.Select(Adapt)
                : new List<TaskReadDto>());
}