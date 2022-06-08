using TaskManager.Infrastructure.Storage.contracts;

namespace TaskManager.Infrastructure.Storage;

public class TaskWriteAdapter: EntityAdapter<TaskDto, TaskEntity>
{
    public TaskEntity Adapt(TaskDto dto) =>
        new()
        {
            Id = dto.Id,
            CloseCreated = dto.CloseCreated,
            Created = dto.Created,
            Description = dto.Description,
            DueDate = dto.DueDate,
            State = dto.State,
            Tag = dto.Tag,
            SubTasks = dto.SubTasks.Count() > 0 
                ? dto.SubTasks.Select(Adapt)
                : new List<TaskEntity>()
        };
}