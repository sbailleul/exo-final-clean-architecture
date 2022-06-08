using TaskManager.Infrastructure.Storage.contracts;

namespace TaskManager.Infrastructure.Storage;

public class TaskReadAdapter : EntityAdapter<TaskEntity, TaskDto>
{
    public TaskDto Adapt(TaskEntity entity) =>
        new()
        {
            Id = entity.Id,
            CloseCreated = entity.CloseCreated,
            Created = entity.Created,
            Description = entity.Description,
            DueDate = entity.DueDate,
            State = entity.State,
            Tag = entity.Tag,
            SubTasks = entity.SubTasks.Any() 
                ? entity.SubTasks.Select(Adapt)
                : new List<TaskDto>()
        };
}