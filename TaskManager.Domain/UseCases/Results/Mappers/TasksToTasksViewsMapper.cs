using TaskManager.Domain.Contracts;

namespace TaskManager.Domain.UseCases.Results.Mappers;

public class TasksToTasksViewsMapper: Mapper<IEnumerable<Task.Task>, IEnumerable<TaskView>>
{

    private readonly TaskViewAdapter _taskViewAdapter;

    public TasksToTasksViewsMapper(TaskViewAdapter taskViewAdapter)
    {
        _taskViewAdapter = taskViewAdapter;
    }

    public IEnumerable<TaskView> Map(IEnumerable<Task.Task> tasks)
    {
        var flattenedTasks = new List<Task.Task>();

        foreach (var taskWithChild in tasks)
        {
            flattenedTasks.AddRange(Flatten(taskWithChild).Append(taskWithChild));
        }

        return flattenedTasks.Select(_taskViewAdapter.Map);
    }


    private IEnumerable<Task.Task> Flatten(Task.Task task)
    {
        if (task.Children.Count > 0)
        {
            foreach (var t in task.Children)
            {
                yield return t;

                foreach (var sub in Flatten(t))
                {
                    yield return sub;
                }
            }
        }
    }
}