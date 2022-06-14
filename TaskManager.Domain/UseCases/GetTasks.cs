using TaskManager.Domain.Task;
using TaskManager.Domain.UseCases.Results;
using TaskManager.Domain.UseCases.Results.Mappers;

namespace TaskManager.Domain.UseCases;

public class GetTasks
{
    private readonly ITasks _tasks;

    public GetTasks(ITasks tasks)
    {
        _tasks = tasks;
    }
    
    public async Task<IEnumerable<TaskView>> Query()
    {
        var tasks = (await _tasks.GetAll()).Select(Task.Task.From);
        var adapter = new TasksToTasksViewsMapper(new TaskViewAdapter());

        return adapter.Map(tasks);
    }
}