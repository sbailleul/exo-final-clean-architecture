using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Task;
using TaskManager.Domain.Task.dtos;
using TaskManager.Domain.Time;

namespace TaskManager.Domain.UseCases;

public class AddSubTask
{
    private readonly ITasks _tasks;
    private readonly ITimeGenerator _timeGenerator;

    public AddSubTask(ITasks tasks, ITimeGenerator timeGenerator)
    {
        _tasks = tasks;
        _timeGenerator = timeGenerator;
    }

    public async Task<int> Execute(AddSubTaskCommandDto request)
    {
        var compositeId = new CompleteParentId(request.CompleteParentId);
        var parentDto = await _tasks.FindOne(compositeId.GetFirstId());

        if (parentDto is null) throw new ParentNotFoundException();
        
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

        var parent = Task.Task.From(parentDto);
        
        parent.AddChild(newTask, new CompleteParentId(request.CompleteParentId));

        await _tasks.SetAsync(parent.ToWriteDto());

        return id;
    }
}