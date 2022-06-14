using TaskManager.Domain.Contracts;

namespace TaskManager.Domain.UseCases.Results.Mappers;

public class TaskViewAdapter: Mapper<Task.Task, TaskView>
{
    public TaskView Map(Task.Task source)
    {
        return new TaskView(
            source.Id.Value,
            source.Description,
            source.Tag,
            source.CreationDate,
            source.DueDate,
            source.CloseDate,
            source.Children.Select(child => child.Id.Value).ToList()
            );
    }
}