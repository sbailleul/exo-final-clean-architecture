using TaskManager.Domain.Task.WriteDtos;
using TaskManager.Infrastructure.Storage.contracts;

namespace TaskManager.Infrastructure.Storage;

public class TaskReadAdapter : EntityAdapter<TaskEntity, TaskWriteDto>
{
    public TaskWriteDto Adapt(TaskEntity entity) =>
        new(entity.Id, entity.Description,
            entity.Tag, entity.State,
            entity.Created, entity.DueDate,
            entity.CloseCreated, entity.SubTasks.Any()
                ? entity.SubTasks.Select(Adapt)
                : new List<TaskWriteDto>());
}