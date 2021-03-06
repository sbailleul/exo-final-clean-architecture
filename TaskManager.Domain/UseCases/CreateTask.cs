using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Task;
using TaskManager.Domain.Task.dtos;
using TaskManager.Domain.Time;

namespace TaskManager.Domain.UseCases;

public class CreateTask
{
    private readonly ITasks _tasks;
    private readonly ITimeGenerator _timeGenerator;

    public CreateTask(ITasks tasks, ITimeGenerator timeGenerator)
    {
        _tasks = tasks;
        _timeGenerator = timeGenerator;
    }

    public async Task<int> Execute(CreateTaskCommandDto request)
    {
        var id = _tasks.GetNextId();
        var state = request.State is not null ? new State(request.State) : null;

        var newTask = Task.Task.CreateNew(
            new TaskId(id),
            state,
            request.Description,
            request.Tag,
            _timeGenerator.Now(),
            request.DueDate,
            null
        );

        await _tasks.SetAsync(newTask.ToWriteDto());

        return id;
        
    }
}