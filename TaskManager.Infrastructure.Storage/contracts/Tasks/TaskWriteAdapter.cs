using TaskManager.Domain.Task.WriteDtos;
using TaskManager.Infrastructure.Storage.contracts.Persistence;

namespace TaskManager.Infrastructure.Storage.contracts.Tasks;

public class TaskWriteAdapter: IEntityAdapter<TaskWriteDto, TaskEntity>
{
    public TaskEntity Adapt(TaskWriteDto dto) =>
        new()
        {
            Id = dto.Id,
            CloseDate = dto.CloseDate,
            Created = dto.CreationDate,
            Description = dto.Description,
            DueDate = dto.DueDate,
            State = dto.State,
            Tag = dto.Tag,
            SubTasks = dto.Children.Any() 
                ? dto.Children.Select(Adapt)
                : new List<TaskEntity>()
        };
}